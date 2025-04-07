using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using static UnityEngine.UI.CanvasScaler;


public abstract class TacticalAI : ITacticalAI
{

    public class RetreatConditions
    {
        [OdinSerialize]
        protected internal float MinPowerRatio;
        [OdinSerialize]
        protected internal int targetsEaten;
        [OdinSerialize]
        protected internal float acceptableLossRatio;

        public RetreatConditions(float minPowerRatio, int targetsEaten, float lossRatio = 0f)
        {
            MinPowerRatio = minPowerRatio;
            this.targetsEaten = targetsEaten;
            this.acceptableLossRatio = lossRatio;
        }
    }

    protected class PotentialTarget
    {
        internal Actor_Unit actor;
        internal float chance;
        internal int distance;
        internal float utility;

        public PotentialTarget(Actor_Unit actor, float chance, int distance, int damage, float setUtility = 0)
        {
            this.actor = actor;
            this.chance = chance;
            this.distance = distance;
            if (setUtility != 0)
                utility = setUtility;
            else
                utility = chance / Math.Max(actor.Unit.Health / damage, 1);
        }
    }

    protected bool onlySurrenderedEnemies;
    protected bool lackPredators;
    protected bool onlyForeignTroopsLeft;

    protected bool didAction;
    protected bool foundPath;
    [OdinSerialize]
    protected List<Actor_Unit> actors;
    [OdinSerialize]
    protected int enemySide;
    [OdinSerialize]
    protected readonly TacticalTileType[,] tiles;
    [OdinSerialize]
    protected readonly int AISide;
    [OdinSerialize]
    protected int targetsEaten;
    [OdinSerialize]
    public RetreatConditions retreatPlan;
    [OdinSerialize]
    protected bool retreating;
    [OdinSerialize]
    protected readonly bool defendingVillage;
    [OdinSerialize]
    protected int currentTurn = 0;
    [OdinSerialize]
    public bool foreignTurn;

    bool ITacticalAI.ForeignTurn
    {
        get
        {
            return foreignTurn;
        }
        set => foreignTurn = value;
    }

    protected AIPlottedPath path;


    RetreatConditions ITacticalAI.RetreatPlan
    {
        get { return retreatPlan; }
        set
        {
            retreatPlan = value;
        }
    }

    public TacticalAI(List<Actor_Unit> actors, TacticalTileType[,] tiles, int AISide, bool defendingVillage = false)
    {
        this.AISide = AISide;
        this.tiles = tiles;
        this.actors = actors;
        this.defendingVillage = defendingVillage;
        enemySide = State.GameManager.TacticalMode.GetAttackerSide() == AISide ? State.GameManager.TacticalMode.GetDefenderSide() : State.GameManager.TacticalMode.GetAttackerSide();
    }
    public void TurnAI()
    {
        if (actors == null)
            actors = TacticalUtilities.Units;
        path = null;
        var actorsThatMatter = actors.Where(a => a.Targetable && a.Unit.IsDead == false && a.Surrendered == false && a.Unit.Side == AISide);
        onlyForeignTroopsLeft = actorsThatMatter.All(a => TacticalUtilities.GetMindControlSide(a.Unit) == -1 && TacticalUtilities.GetPreferredSide(a.Unit, enemySide, AISide) == enemySide);
        onlySurrenderedEnemies = actors.Where(s => s.Unit.Side != AISide && s.Unit.IsDead == false && s.Surrendered == false && !s.Fled).Any() == false;
        var preds = actors.Where(s => s.Unit.Side == AISide && s.Unit.IsDead == false && s.Unit.Predator);
        lackPredators = preds.Any() == false;
        bool tooBig = true;
        if (onlySurrenderedEnemies)
        {
            //Array of all opposing live units
            var enemies = actors.Where(s => s.Unit.Side != AISide && s.Unit.IsDead == false);
            foreach (var actor in preds)
            {
                if (tooBig == false)
                    break;
                foreach (var target in enemies)
                {
                    if (actor.PredatorComponent.TotalCapacity() > target.Bulk())
                    {
                        tooBig = false;
                        break;
                    }
                }
            }
            lackPredators = tooBig; //Attack if no units can ever eat an enemy unit.
        }

        if (retreatPlan != null && currentTurn >= 4)
        {
            if (currentTurn >= 8 && retreatPlan.targetsEaten > 0 && retreatPlan.targetsEaten <= targetsEaten && onlySurrenderedEnemies == false)
            {
                if (retreating == false)
                {
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=orange>{(actors[0].Unit.Side == AISide ? "Attackers" : "Defenders")} are now fleeing because they've eaten enough units that they're satisfied</color>");
                }
                retreating = true;
            }
            else if (retreatPlan.MinPowerRatio > 0.0001)
            {
                double friendlyPower = StrategicUtilities.ArmyPower(actors.Where(s => s.Unit.Side == AISide && s.Unit.IsDead == false && s.Surrendered == false).Select(s => s.Unit).ToList());
                double enemyPower = StrategicUtilities.ArmyPower(actors.Where(s => s.Unit.Side != AISide && s.Unit.IsDead == false && s.Surrendered == false).Select(s => s.Unit).ToList());

                if (defendingVillage)
                    friendlyPower *= 2;

                if ((enemyPower > 0) && (friendlyPower / enemyPower) < retreatPlan.MinPowerRatio)
                {
                    if (retreating == false)
                    {
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=orange>{(actors[0].Unit.Side == AISide ? "Attackers" : "Defenders")} are now fleeing because they are significantly outmatched</color>");
                    }
                    retreating = true;
                }
                else
                {
                    if (retreating && (enemyPower > 0) && (friendlyPower / enemyPower) > 1.2f * retreatPlan.MinPowerRatio && !onlyForeignTroopsLeft)
                    {
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=orange>{(actors[0].Unit.Side == AISide ? "Attackers" : "Defenders")} are no longer fleeing</color>");
                        retreating = false;
                    }
                }
            }
            else if (retreatPlan.acceptableLossRatio > 0.0001) // Made because it somewhat considers how the battle is developing instead of just a snapshot metric
            {
                bool aIisAttacker = actors[0].Unit.Side == AISide;

                var friendlies = actors.Where(s => s.Unit.Side == AISide && s.Unit.IsDead == false && s.Surrendered == false).Select(s => s.Unit).ToList();
                var enemies = actors.Where(s => s.Unit.Side != AISide && s.Unit.IsDead == false && s.Surrendered == false).Select(s => s.Unit).ToList();

                double friendlyPower = 0d;
                double enemyPower = 0d;

                actors.ForEach(actor =>
                {
                    if (AISide == actor.Unit.Side)
                    {
                        friendlyPower += actor.Unit.HealthPct * StrategicUtilities.ArmyPower(new List<Unit> { actor.Unit });  // More accurately than an average health pct, this correctly considers health loss on fodder units as less important
                    }
                    else
                    {
                        enemyPower += actor.Unit.HealthPct * StrategicUtilities.ArmyPower(new List<Unit> { actor.Unit });
                    }
                });

                double friendLoss;
                double enemyLoss;

                // This fails to accurately consider units that already started with less than full health, but I find that worth it in exchange for recognizing the peril in having many half digested and wounded units
                // Also viable retreat plan for 1 man armies and somesuch 
                // (Would go great with dragon monster packs, now that I think about it) (((they lowkey need their own AI where the kobolds serve and rub them and the dragons sometimes devour them for heals OwO)))
                if (aIisAttacker)
                {
                    enemyLoss = State.GameManager.TacticalMode.StartingDefenderPower - enemyPower * enemies.Count;
                    friendLoss = State.GameManager.TacticalMode.StartingAttackerPower - friendlyPower * friendlies.Count;
                }
                else
                {
                    friendLoss = State.GameManager.TacticalMode.StartingDefenderPower - friendlyPower * friendlies.Count;
                    enemyLoss = State.GameManager.TacticalMode.StartingAttackerPower - enemyPower * enemies.Count;
                }

                if ((enemyLoss > 0) && (friendLoss / enemyLoss) > retreatPlan.acceptableLossRatio || (enemyLoss <= 0 && friendLoss > 0))
                {
                    if (retreating == false)
                    {
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=orange>{(aIisAttacker ? "Attackers" : "Defenders")} are now fleeing because the battle isn't developing favorably enough</color>");
                    }
                    retreating = true;
                }
                else
                {
                    if (retreating && (enemyLoss > 0) && (friendLoss / enemyLoss) < 0.8f * retreatPlan.acceptableLossRatio && !onlyForeignTroopsLeft)
                    {
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=orange>{(aIisAttacker ? "Attackers" : "Defenders")} are no longer fleeing</color>");
                        retreating = false;
                    }
                }
            }
        }
    }

    public virtual bool RunAI()
    {
        if (actors == null)
            actors = TacticalUtilities.Units;
        if (currentTurn != State.GameManager.TacticalMode.currentTurn)
        {
            TurnAI();
            currentTurn = State.GameManager.TacticalMode.currentTurn;
        }
        foreach (Actor_Unit actor in actors)
        {            
            if (State.World.IsNight)
            {
                int unitSightRange = Config.DefualtTacticalSightRange + actor.Unit.TraitBoosts.SightRangeBoost;
                foreach (var seenUnit in TacticalUtilities.UnitsWithinTiles(actor.Position, unitSightRange).Where(s => TacticalUtilities.TreatAsHostile(actor, s)))
                {
                    seenUnit.InSight = true;
                }
            }
            if (actor.Targetable == true && actor.Unit.Side == AISide && (foreignTurn ? !TacticalUtilities.IsUnitControlledByPlayer(actor.Unit) : true) && actor.Movement > 0)
            {
                if (TacticalUtilities.IsUnitControlledByPlayer(actor.Unit) && State.GameManager.TacticalMode.RunningFriendlyAI == false && !State.GameManager.TacticalMode.IgnorePseudo && !State.GameManager.TacticalMode.turboMode)
                {
                    if (State.GameManager.TacticalMode.SkipPseudo)
                    {
                        actor.Movement = 0;
                        return true;
                    }
                    State.GameManager.TacticalMode.PseudoTurn = true;
                    State.GameManager.TacticalMode.StatusUI.EndTurn.interactable = true;
                    return true;
                }
                State.GameManager.TacticalMode.PseudoTurn = false;
                if (path != null && path.Actor == actor)
                {
                    if (retreating && actor.Movement == 1 && TacticalUtilities.TileContainsMoreThanOneUnit(actor.Position.x, actor.Position.y) == false)
                    {
                        FightWithoutMoving(actor);
                        if (actor.Movement == 0)
                            return true;
                    }
                    if (path.Path.Count == 0)
                    {
                        path.Action?.Invoke();
                        if (path.Action == null)
                            actor.Movement = 0;
                        path = null;
                        continue;
                    }
                    Vec2i newLoc = new Vec2i(path.Path[0].X, path.Path[0].Y);
                    path.Path.RemoveAt(0);
                    if (actor.Movement == 1 && TacticalUtilities.OpenTile(newLoc.x, newLoc.y, actor) == false)
                    {
                        actor.Movement = 0;
                        return true;
                    }

                    if (actor.MoveTo(newLoc, tiles, (State.GameManager.TacticalMode.RunningFriendlyAI ? Config.TacticalFriendlyAIMovementDelay : Config.TacticalAIMovementDelay)) == false)
                    {
                        //Can't move -- most likely a multiple movement point tile when on low MP
                        path.Action?.Invoke();
                        actor.Movement = 0;
                        path = null;
                        return true;
                    }
                    if (actor.Movement == 1 && IsRanged(actor) && TacticalUtilities.TileContainsMoreThanOneUnit(actor.Position.x, actor.Position.y) == false)
                    {
                        path = null;
                    }
                    else if (path.Path.Count == 0 || actor.Movement == 0)
                    {
                        path.Action?.Invoke();
                        if (path.Action == null)
                            actor.Movement = 0;
                        path = null;
                    }
                    return true;
                }
                else
                {
                    if (onlyForeignTroopsLeft)
                        HandleLeftoverForeigns(actor);
                    GetNewOrder(actor);
                    return true; 
                }
            }
        }

        return false;
    }

    protected abstract void GetNewOrder(Actor_Unit actor);

    protected virtual int CheckActionEconomyOfActorFromPositionWithAP(Actor_Unit actor, Vec2i position, int ap)
    {
        int apRequired = -1;
        if (actor.Unit.GetStatusEffect(StatusEffectType.Temptation) != null)
        {
            apRequired = CheckForceFeed(actor, position, ap);
            if (apRequired > 0)
                return ap - apRequired;
        }
        if (actor.Unit.HasTrait(Traits.Pounce) && ap >= 2)
        {
            apRequired = CheckVorePounce(actor, position, ap);
            if (apRequired > 0)
                return ap - apRequired;
        }
        apRequired = CheckPred(actor, position, ap);
        if (apRequired > 0)
            return ap - apRequired;

        apRequired = CheckResurrect(actor, position, ap);
        if (apRequired > 0)
            return ap - apRequired;

        apRequired = CheckReanimate(actor, position, ap);
        if (apRequired > 0)
            return ap - apRequired;

        apRequired = CheckBind(actor, position, ap);
        if (apRequired > 0)
            return ap - apRequired;

        if (State.Rand.Next(2) == 0 || actor.Unit.HasWeapon == false)
            apRequired = CheckSpells(actor, position, ap);
        if (apRequired > 0)
            return ap - apRequired;

        if (actor.Unit.HasTrait(Traits.Pounce) && ap >= 2)
        {
            apRequired = CheckMeleePounce(actor, position, ap);
            if (apRequired > 0)
                return ap - apRequired;
        }
        if (IsRanged(actor))
            apRequired = CheckRanged(actor, position, ap);
        else
            apRequired = CheckMelee(actor, position, ap);
        if (apRequired > 0)
            return ap - apRequired;
        // Everything else is less important than belly rubs.
        return ap;
    }

    protected virtual int CheckForceFeed(Actor_Unit actor, Vec2i position, int ap)
    {
        StatusEffect temptation = actor.Unit.GetStatusEffect(StatusEffectType.Temptation);
        List<PotentialTarget> targets = new List<PotentialTarget>();

        foreach (Actor_Unit unit in actors)
        {
            if (unit.Targetable == true && unit.Unit.Predator && unit.Unit.FixedSide == temptation.Strength && TacticalUtilities.GetMindControlSide(unit.Unit) == -1 && !unit.Surrendered)
            {
                int distance = unit.Position.GetNumberOfMovesDistance(position);
                if (distance < ap)
                {
                    if (distance > 1 && TacticalUtilities.FreeSpaceAroundTarget(unit.Position, actor) == false)
                        continue;
                targets.Add(new PotentialTarget(unit, 100, distance, 4, -distance));
                }

            }
        }
        targets = targets.OrderByDescending(t => t.utility).ToList();

        if (!targets.Any())
            return -1;
        Actor_Unit reserveTarget = targets[0].actor;
        while (targets.Any())
        {
            if (targets[0].distance < 2)
            {
                return 1;
            }
            else
            {
                if (targets[0].actor.Position.GetNumberOfMovesDistance(position) < ap) //discard the clearly impossible
                {
                    int distance = CheckMoveTo(actor, position, targets[0].actor.Position, 1, ap);
                    if (distance < ap && distance >= 0)
                        return distance + 1;
                }
            }
            targets.RemoveAt(0);
        }

        return -1;
    }

    protected virtual void RunForceFeed(Actor_Unit actor)
    {
        StatusEffect temptation = actor.Unit.GetStatusEffect(StatusEffectType.Temptation);
        List<PotentialTarget> targets = new List<PotentialTarget>();

        foreach (Actor_Unit unit in actors)
        {
            if (unit.Targetable == true && unit.Unit.Predator && unit.Unit.FixedSide == temptation.Strength && TacticalUtilities.GetMindControlSide(unit.Unit) == -1 && !unit.Surrendered)
            {
                int distance = unit.Position.GetNumberOfMovesDistance(actor.Position);
                if (distance < actor.Movement)
                {
                    if (distance > 1 && TacticalUtilities.FreeSpaceAroundTarget(unit.Position, actor) == false)
                        continue;
                    targets.Add(new PotentialTarget(unit, 100, distance, 4, -distance));
                }

            }
        }
        targets = targets.OrderByDescending(t => t.utility).ToList();

        if (!targets.Any())
            return;
        Actor_Unit reserveTarget = targets[0].actor;
        while (targets.Any())
        {
            if (targets[0].distance < 2)
            {
                TacticalUtilities.ForceFeed(actor, targets[0].actor);
                didAction = true;
                break;
            }
            else
            {
                if (targets[0].actor.Position.GetNumberOfMovesDistance(actor.Position) < actor.Movement) //discard the clearly impossible
                {
                    MoveToAndAction(actor, targets[0].actor.Position, 1, actor.Movement, () => TacticalUtilities.ForceFeed(actor, targets[0].actor));
                    if (foundPath && path.Path.Count() < actor.Movement)
                        return;
                }
            }
            targets.RemoveAt(0);
        }
        if (didAction == false)
        {
            if (reserveTarget != null)
            {
                //Get as close to the target as you can if you can't reach it
                MoveToAndAction(actor, reserveTarget.Position, -1, 999, null);
                if (foundPath)
                    return;
                RandomWalkAndEndTurn(actor);
            }
            else
            {
                RandomWalkAndEndTurn(actor);
            }
        }
    }

    protected virtual int CheckVorePounce(Actor_Unit actor, Vec2i position, int ap)
    {
        if (!actor.Unit.Predator)
            return -1;
        List<PotentialTarget> targets = GetListOfPotentialVorePouncePrey(actor, position, ap);
        if (!targets.Any())
            return -1;
        Actor_Unit reserveTarget = targets[0].actor;
        while (targets.Any())
        {
            if (targets[0].distance == 1)
            {
                return 1;
            }
            if (targets[0].distance <= 4 && targets[0].distance > 1)
            {
                if (ap >= 2)
                {
                    return 2;
                }
            }
            int distance = CheckMoveTo(actor, position, targets[0].actor.Position, 4, 2 + ap);
            if (distance + 1 < ap && distance >= 0)
                return distance + 2;
            targets.RemoveAt(0);
        }
        return -1;
    }

    protected virtual void RunBellyRub(Actor_Unit actor, int spareAP)
    {
        int cost = actor.MaxMovement() / 3;
        List<PotentialTarget> targets = GetListOfPotentialRubTargets(actor, actor.Position, spareAP);

        if (!targets.Any())
        {
            return;
        }


        // no looping for now, due to gameplay concerns
        //while (spareAP > 0)
        //{
        while (targets.Any())
        {
            if (targets[0].distance < 2)
            {
                int before = actor.Movement;
                actor.BellyRub(targets[0].actor);
                if (actor.Movement == before) // The issue that caused this was fixed, so it's now a failsafe that should never actually run.
                {
                    targets.RemoveAt(0);
                    continue;
                }
                didAction = true;
                return;
                //spareAP -= cost;
                //targets.RemoveAt(0);
                //break;
            }
            else
            {
                // at this point we know that we have enough ap for both the distance to an enemy and doing something useful, AND for the distance to an ally and bellyrub, from where we stand. 
                // But could they still reach an enemy after changing position to be adjacent to ally?
                var rubPath = TacticalPathfinder.GetPath(actor.Position, targets[0].actor.Position, 1, actor, spareAP);
                if (rubPath != null && rubPath.Count > 0)
                {
                    PathNode destination = rubPath.Last();
                    var nextToAlly = new Vec2i(destination.X, destination.Y);
                    if (CheckActionEconomyOfActorFromPositionWithAP(actor, nextToAlly, actor.Movement - (rubPath.Count + cost)) >= 0)
                    {
                        MoveToAndAction(actor, nextToAlly, 0, spareAP, () => actor.BellyRub(targets[0].actor));
                        return;
                    }

                }

            }
            targets.RemoveAt(0);
        }
        return;
        //}
    }

    protected virtual List<PotentialTarget> GetListOfPotentialRubTargets(Actor_Unit actor, Vec2i position, int moves)
    {
        List<PotentialTarget> targets = new List<PotentialTarget>();

        foreach (Actor_Unit unit in actors)
        {
            if (unit.Targetable == true && unit.Unit.Predator && !TacticalUtilities.TreatAsHostile(actor, unit) && TacticalUtilities.GetMindControlSide(unit.Unit) == -1 && !unit.Surrendered && unit.PredatorComponent?.PreyCount > 0 && !unit.ReceivedRub) // includes self
            {
                int distance = unit.Position.GetNumberOfMovesDistance(position);
                if (distance - 1 + (actor.MaxMovement() / 3) <= moves)
                {
                    if (distance > 1 && TacticalUtilities.FreeSpaceAroundTarget(unit.Position, actor) == false)
                        continue;
                    if (unit.PredatorComponent.CanFeed() || unit.PredatorComponent.CanFeedCum())
                        continue;
                    targets.Add(new PotentialTarget(unit, 100, distance, 4, 100 - (unit == actor ? 100 - unit.Unit.HealthPct + 10 : 100 - unit.Unit.HealthPct))); // self is weighted a little lower than the rest
                }

            }
        }
        return targets.OrderByDescending(t => t.utility).ToList();
    }

    protected virtual void FightWithoutMoving(Actor_Unit actor)
    {
        List<PotentialTarget> targets;
        if (actor.Unit.Predator)
        {
            targets = GetListOfPotentialPrey(actor, false, actor.Position, actor.Movement);
            while (targets.Any())
            {
                if (targets[0].distance < 2)
                {
                    if (actor.PredatorComponent.UsePreferredVore(targets[0].actor))
                        targetsEaten++;
                    didAction = true;
                    break;
                }
                targets.RemoveAt(0);
            }
        }
        if (didAction == false && IsRanged(actor))
        {
            targets = GetListOfPotentialRangedTargets(actor, actor.Position);
            while (targets.Any())
            {
                if (targets[0].distance <= actor.BestRanged.Range && targets[0].distance > 1)
                {
                    actor.Attack(targets[0].actor, true);
                    didAction = true;
                    break;
                }
                targets.RemoveAt(0);
            }
        }
        if (didAction == false)
        {
            targets = GetListOfPotentialMeleeTargets(actor, actor.Position, actor.Movement);
            while (targets.Any())
            {
                if (targets[0].distance < 2)
                {
                    actor.Attack(targets[0].actor, false);
                    didAction = true;
                    return;
                }
                targets.RemoveAt(0);
            }
        }
    }

    protected virtual int CheckPred(Actor_Unit actor, Vec2i position, int ap, bool anyDistance = false)
    {
        int distance = -1;
        if (!actor.Unit.Predator)
            return -1;
        List<PotentialTarget> targets = GetListOfPotentialPrey(actor, anyDistance, position, ap);
        if (!targets.Any())
            return -1;

        while (targets.Any())
        {
            if (targets[0].distance < 2)
            {
                return 1;
            }
            else
            {
                if (actor.Unit.HasTrait(Traits.RangedVore))
                {
                    distance = CheckMoveTo(actor, position, targets[0].actor.Position, 1, ap);
                    if (distance < ap && distance >= 0)
                        return distance + 1;
                    distance = CheckMoveTo(actor, position, targets[0].actor.Position, 4, ap);
                    if (distance < ap && distance >= 0)
                        return distance + 1;
                }
                else
                    distance = CheckMoveTo(actor, position, targets[0].actor.Position, 1, ap);
                if (distance < ap && distance >= 0)
                    return distance + 1;
            }
            targets.RemoveAt(0);
        }
        return -1;
    }

    protected virtual void RunPred(Actor_Unit actor, bool anyDistance = false)
    {

        if (actor.Unit.Predator == false)
            return;
        List<PotentialTarget> targets = GetListOfPotentialPrey(actor, anyDistance, actor.Position, actor.Movement);
        if (!targets.Any())
            return;

        while (targets.Any())
        {
            if (targets[0].distance < 2)
            {
                if (actor.PredatorComponent.UsePreferredVore(targets[0].actor))
                    targetsEaten++;
                didAction = true;
                break;
            }
            else
            {
                if (actor.Unit.HasTrait(Traits.RangedVore))
                {
                    MoveToAndAction(actor, targets[0].actor.Position, 1, 999, () =>
                    {
                        if (actor.PredatorComponent.UsePreferredVore(targets[0].actor))
                            targetsEaten++;
                    }); //If anydistance is off, this will already be limited to the units move radius
                    if (foundPath && path.Path.Count() < actor.Movement)
                        break;
                    MoveToAndAction(actor, targets[0].actor.Position, 4, 999, () =>
                    {
                        if (actor.PredatorComponent.UsePreferredVore(targets[0].actor))
                            targetsEaten++;
                    }); //If anydistance is off, this will already be limited to the units move radius                                      
                }
                else
                    MoveToAndAction(actor, targets[0].actor.Position, 1, 999, () =>
                    {
                        if (actor.PredatorComponent.UsePreferredVore(targets[0].actor))
                            targetsEaten++;
                    }); //If anydistance is off, this will already be limited to the units move radius
                if (foundPath && path.Path.Count() < actor.Movement)
                {
                    break;
                }
                else
                {
                    if (anyDistance)
                        break;
                    //If you can't get there in one turn, discard it
                    foundPath = false;
                    path = null;
                }
            }
            targets.RemoveAt(0);
        }
    }

    protected virtual List<PotentialTarget> GetListOfPotentialPrey(Actor_Unit actor, bool anyDistance, Vec2i position, int movement)
    {
        List<PotentialTarget> targets = new List<PotentialTarget>();
        if (State.GameManager.TacticalMode.IsOnlyOneSideVisible() && actor.Unit.IsInfiltratingSide(AISide)) // should prevent aggressive action while within a foreign army when the battle is basically over
            return targets;
        //check if we have at least 1 unit of capacity free
        float cap = actor.PredatorComponent.FreeCap();
        if (cap >= 1)
        {
            foreach (Actor_Unit unit in actors)
            {

                if (unit.Targetable && (unit.InSight || !State.World.IsNight) && (TacticalUtilities.TreatAsHostile(actor, unit) || (unit.Unit.GetStatusEffect(StatusEffectType.Hypnotized)?.Duration < 3 && unit != actor)) && unit.Bulk() <= cap)
                {
                    int distance = unit.Position.GetNumberOfMovesDistance(position);
                    if (distance <= movement || anyDistance)
                    {
                        float chance = unit.GetDevourChance(actor, true);
                        if (((chance > .5f || (actor.Unit.HasTrait(Traits.Biter) && chance > .25f && actor.Unit.GetBestMelee().Damage > 2)) || actor.Unit.HasTrait(Traits.VoreObsession)) && unit.AIAvoidEat <= 0)
                        {
                            if (distance > 1 && TacticalUtilities.FreeSpaceAroundTarget(unit.Position, actor) == false)
                                continue;
                            if (unit.Unit.HasTrait(Traits.AcidImmunity)) //More interesting if mostly ignored
                                chance *= .5f;
                            targets.Add(new PotentialTarget(unit, chance, distance, 4, chance));
                        }
                    }
                }
            }
            PotentialTarget primeTarget = targets.Where(t => t.distance < 2).OrderByDescending(s => s.chance).FirstOrDefault();
            if (primeTarget != null)
                return new List<PotentialTarget>() { primeTarget };
            return targets.OrderByDescending(t => t.chance).ToList();
        }
        return targets;
    }

    protected virtual void WalkToYBand(Actor_Unit actor, int y)
    {
        var tempPath = TacticalPathfinder.GetPathToY(actor.Position, actor.Unit.HasTrait(Traits.Flight), y, actor);
        if (tempPath == null || tempPath.Count == 0)
            foundPath = false;
        else
        {
            foundPath = true;
            path = new AIPlottedPath
            {
                Actor = actor,
                Path = tempPath,
                Action = null
            };
        }
    }

    protected virtual void MoveToAndAction(Actor_Unit actor, Vec2i p, int howClose, int maxDistance, Action action)
    {
        var tempPath = TacticalPathfinder.GetPath(actor.Position, p, howClose, actor, maxDistance);
        if (tempPath == null || tempPath.Count == 0)
            foundPath = false;
        else
        {
            foundPath = true;
            path = new AIPlottedPath
            {
                Actor = actor,
                Path = tempPath,
                Action = action
            };
        }
    }

    protected virtual int CheckMoveTo(Actor_Unit actor, Vec2i actorPosition, Vec2i p, int howClose, int maxDistance)
    {
        var tempPath = TacticalPathfinder.GetPath(actorPosition, p, howClose, actor, maxDistance);
        if (tempPath == null || tempPath.Count == 0)
            return -1;
        else
        {
            return tempPath.Count;
        }
    }

    protected virtual void RandomWalkAndEndTurn(Actor_Unit actor)
    {
        RandomWalk(actor);
        actor.ClearMovement();
        didAction = true;
    }

    protected virtual bool RandomWalk(Actor_Unit actor)
    {

        int r = State.Rand.Next(8);
        int d = 8;
        while (!actor.Move(r, tiles))
        {
            r++;
            d--;
            if (r > 7)
            {
                r = 0;
            }
            if (d < 1)
            {
                return false;
            }
        }
        didAction = true;
        return true;
    }

    protected virtual void RunVorePounce(Actor_Unit actor)
    {
        if (actor.Unit.Predator == false)
            return;
        List<PotentialTarget> targets = GetListOfPotentialVorePouncePrey(actor, actor.Position, actor.Movement);
        if (!targets.Any())
            return;
        Actor_Unit reserveTarget = targets[0].actor;
        while (targets.Any())
        {
            if (targets[0].distance == 1)
            {
                if (actor.PredatorComponent.UsePreferredVore(targets[0].actor))
                    targetsEaten++;
                didAction = true;
                break;
            }
            if (targets[0].distance <= 4 && targets[0].distance > 1)
            {
                actor.VorePounce(targets[0].actor, AIAutoPick: true);
                if (!targets[0].actor.Visible)
                {
                    targetsEaten++;
                }
                didAction = true;
                break;
            }
            MoveToAndAction(actor, targets[0].actor.Position, 4, 2 + actor.Movement, () =>
            {
                actor.VorePounce(targets[0].actor, AIAutoPick: true);
                if (!targets[0].actor.Visible)
                {
                    targetsEaten++;
                }
            });
            if (path != null)
                return;
            targets.RemoveAt(0);
        }
    }

    protected virtual List<PotentialTarget> GetListOfPotentialVorePouncePrey(Actor_Unit actor, Vec2i position, int moves)
    {
        List<PotentialTarget> targets = new List<PotentialTarget>();
        //check if we have at least 1 unit of capacity free
        float cap = actor.PredatorComponent.FreeCap();
        if (cap >= 1 && !(State.GameManager.TacticalMode.IsOnlyOneSideVisible() && actor.Unit.IsInfiltratingSide(AISide)))
        {
            foreach (Actor_Unit unit in actors)
            {

                if (unit.Targetable && (unit.InSight || !State.World.IsNight) && TacticalUtilities.TreatAsHostile(actor, unit) && unit.Bulk() <= cap && TacticalUtilities.FreeSpaceAroundTarget(unit.Position, actor))
                {
                    int distance = unit.Position.GetNumberOfMovesDistance(position);
                    if (distance <= 2 + moves)
                    {
                        float chance = unit.GetDevourChance(actor, true);
                        if (chance > .5f)
                        {
                            targets.Add(new PotentialTarget(unit, chance, distance, 4, chance));
                        }
                    }
                }
            }
            PotentialTarget primeTarget = targets.Where(t => t.distance < 2).OrderByDescending(s => s.chance).FirstOrDefault();
            if (primeTarget != null)
                return new List<PotentialTarget>() { primeTarget };
            return targets.OrderByDescending(t => t.chance).ToList();
        }
        return targets;
    }

    protected virtual int CheckMeleePounce(Actor_Unit actor, Vec2i position, int ap)
    {
        List<PotentialTarget> targets = GetListOfPotentialPounceTargets(actor, position, ap);
        if (!targets.Any())
            return -1;
        Actor_Unit reserveTarget = targets[0].actor;
        while (targets.Any())
        {
            if (targets[0].distance == 1)
            {
                return 1;
            }
            if (targets[0].distance <= 4 && targets[0].distance > 1)
            {
                if (ap >= 2)
                {
                    return 2;
                }
            }
            int distance = CheckMoveTo(actor, position, targets[0].actor.Position, 4, 2 + ap);
            if (distance + 1 < ap && distance >= 0)
                return distance + 2;
            targets.RemoveAt(0);
        }
        return -1;
    }

    protected virtual void RunMeleePounce(Actor_Unit actor)
    {
        List<PotentialTarget> targets = GetListOfPotentialPounceTargets(actor, actor.Position, actor.Movement);
        if (!targets.Any())
            return;
        Actor_Unit reserveTarget = targets[0].actor;
        while (targets.Any())
        {
            if (targets[0].distance == 1)
            {
                actor.Attack(targets[0].actor, false);
                didAction = true;
                break;
            }
            if (targets[0].distance <= 4 && targets[0].distance > 1)
            {
                actor.MeleePounce(targets[0].actor);
                didAction = true;
                break;
            }
            MoveToAndAction(actor, targets[0].actor.Position, 4, 2 + actor.Movement, () => actor.Attack(targets[0].actor, false));
            if (path != null)
                return;
            targets.RemoveAt(0);
        }
    }

    protected virtual List<PotentialTarget> GetListOfPotentialPounceTargets(Actor_Unit actor, Vec2i position, int moves)
    {
        List<PotentialTarget> targets = new List<PotentialTarget>();
        if (State.GameManager.TacticalMode.IsOnlyOneSideVisible() && actor.Unit.IsInfiltratingSide(AISide))
            return targets;
        foreach (Actor_Unit unit in actors)
        {
            if (unit.Targetable == true && (unit.InSight || !State.World.IsNight) && TacticalUtilities.FreeSpaceAroundTarget(unit.Position, actor) && TacticalUtilities.TreatAsHostile(actor, unit) && (unit.Surrendered == false || (onlySurrenderedEnemies && lackPredators) || currentTurn > 150))
            {
                int distance = unit.Position.GetNumberOfMovesDistance(position);
                if (distance <= 2 + moves)
                {
                    float chance = unit.GetAttackChance(actor, true, true);
                    targets.Add(new PotentialTarget(unit, chance, distance, 4));
                }
            }
        }
        return targets.OrderByDescending(t => t.utility).ToList();
    }

    protected virtual int CheckRanged(Actor_Unit actor, Vec2i position, int ap)
    {
        List<PotentialTarget> targets = GetListOfPotentialRangedTargets(actor, position);
        if (!targets.Any() || actor.BestRanged == null || actor.Unit.GetBestRanged() == null)
            return -1;
        Actor_Unit reserveTarget = targets[0].actor;
        while (targets.Any())
        {
            if (targets[0].distance <= actor.BestRanged.Range && (targets[0].distance > 1 || (targets[0].distance > 0 && actor.BestRanged.Omni)))
            {
                return 1;
            }
            targets.RemoveAt(0);
        }

        if (reserveTarget != null)
        {
            if (actor.Position.GetNumberOfMovesDistance(reserveTarget.Position) == 1)
            {
                return 1;
            }
            else
            {
                int distance = CheckMoveTo(actor, position, reserveTarget.Position, actor.BestRanged.Range, ap);
                if (distance < ap && distance >= 0)
                    return distance + 1;
            }
        }
        return -1;
    }

    protected virtual void RunRanged(Actor_Unit actor)
    {
        List<PotentialTarget> targets = GetListOfPotentialRangedTargets(actor, actor.Position);
        if (!targets.Any() || actor.BestRanged == null || actor.Unit.GetBestRanged() == null)
            return;
        Actor_Unit reserveTarget = targets[0].actor;
        while (targets.Any())
        {
            if (targets[0].distance <= actor.BestRanged.Range && (targets[0].actor.InSight || !State.World.IsNight) && (targets[0].distance > 1 || (targets[0].distance > 0 && actor.BestRanged.Omni)))
            {
                actor.Attack(targets[0].actor, true);
                didAction = true;
                break;
            }
            targets.RemoveAt(0);
        }
        if (didAction == false)
        {
            if (reserveTarget != null)
            {
                if (actor.Position.GetNumberOfMovesDistance(reserveTarget.Position) == 1)
                {
                    if ((Config.World.DefualtTacticalSightRange + actor.Unit.TraitBoosts.SightRangeBoost == 1) || RandomWalk(actor) == false)
                        RunMelee(actor); //We're surrounded or won't be able to see the target if we move away
                }
                else
                {
                    if (reserveTarget.InSight || !State.World.IsNight)
                    {
                        MoveToAndAction(actor, reserveTarget.Position, actor.BestRanged.Range, 999, () => actor.Attack(reserveTarget, true));
                    }
                    if (foundPath)
                        return;
                    MoveToAndAction(actor, reserveTarget.Position, 15, 999, null); //Just move towards if you can't find a great route
                }
            }
            else
            {
                RandomWalkAndEndTurn(actor);
            }
        }
    }

    protected virtual List<PotentialTarget> GetListOfPotentialRangedTargets(Actor_Unit actor, Vec2i position) // Adapted all the potential target checks to allow for positions aside from the actor's location
    {
        List<PotentialTarget> targets = new List<PotentialTarget>();
        if (actor.BestRanged == null) return targets; //This shouldn't happen, but just in case
        if (State.GameManager.TacticalMode.IsOnlyOneSideVisible() && actor.Unit.IsInfiltratingSide(AISide)) return targets;
        foreach (Actor_Unit target in actors)
        {
            if (target?.Unit == null) //If this doesn't prevent exceptions I might have to just try/catch this function.  
                continue;
            if (target.Targetable == true && TacticalUtilities.TreatAsHostile(actor, target) && (target.Surrendered == false || (onlySurrenderedEnemies && lackPredators) || currentTurn > 150))
            {
                int distance = target.Position.GetNumberOfMovesDistance(position);
                float chance = target.GetAttackChance(actor, true, true);
                int damage = actor.WeaponDamageAgainstTarget(target, true);
                targets.Add(new PotentialTarget(target, chance, distance, damage,((target.InSight&&State.World.IsNight)? 100:0)));
            }
        }
        return targets.OrderByDescending(t => t.utility).ToList();
    }

    protected virtual int CheckMelee(Actor_Unit actor, Vec2i position, int ap)
    {
        List<PotentialTarget> targets = GetListOfPotentialMeleeTargets(actor, position, ap);
        if (!targets.Any())
            return -1;
        Actor_Unit reserveTarget = targets[0].actor;
        while (targets.Any())
        {
            if (targets[0].distance < 2)
            {
                return 1;
            }
            else
            {
                if (targets[0].actor.Position.GetNumberOfMovesDistance(position) < ap) //discard the clearly impossible
                {
                    int distance = CheckMoveTo(actor, position, targets[0].actor.Position, 1, ap);
                    if (distance < ap && distance >= 0)
                        return distance + 1;
                }
            }
            targets.RemoveAt(0);
        }

        return -1;
    }


    protected virtual void RunMelee(Actor_Unit actor)
    {
        List<PotentialTarget> targets = GetListOfPotentialMeleeTargets(actor, actor.Position, actor.Movement);
        if (!targets.Any())
            return;
        Actor_Unit reserveTarget = targets[0].actor;
        while (targets.Any())
        {
            if (targets[0].distance < 2 && (targets[0].actor.InSight || !State.World.IsNight))
            {
                actor.Attack(targets[0].actor, false);
                didAction = true;
                return;
            }
            else
            {
                if (targets[0].actor.Position.GetNumberOfMovesDistance(actor.Position) < actor.Movement && (targets[0].actor.InSight || !State.World.IsNight)) //discard the clearly impossible
                {
                    if (actor.Unit.Race == Race.Asura && TacticalActionList.TargetedDictionary[SpecialAction.ShunGokuSatsu].AppearConditional(actor))
                        MoveToAndAction(actor, targets[0].actor.Position, 1, actor.Movement, () => actor.ShunGokuSatsu(targets[0].actor));
                    else
                        MoveToAndAction(actor, targets[0].actor.Position, 1, actor.Movement, () => actor.Attack(targets[0].actor, false));
                    if (foundPath && path.Path.Count() < actor.Movement)
                        return;
                }
            }
            targets.RemoveAt(0);
        }
        if (didAction == false)
        {
            if (reserveTarget != null)
            {
                //Get as close to the target as you can if you can't reach it
                if (reserveTarget.InSight)
                {
                    MoveToAndAction(actor, reserveTarget.Position, -1, 999, null);
                }
                else
                {
                    if (State.Rand.NextDouble() > 0.6f)
                    {
                        RandomWalkAndEndTurn(actor); //40% chance to hang back
                    }
                    else
                    {
                        int halfdist = (int)Math.Floor(actor.Position.GetDistance(reserveTarget.Position)) / 2;
                        halfdist += State.Rand.Next(-3, 3);
                        MoveToAndAction(actor, reserveTarget.Position, halfdist, 999, null); //Walk near target
                    }                  
                }
                if (foundPath)
                    return;
                RandomWalkAndEndTurn(actor);
            }
            else
            {
                RandomWalkAndEndTurn(actor);
            }
        }
    }

    protected virtual List<PotentialTarget> GetListOfPotentialMeleeTargets(Actor_Unit actor, Vec2i position, int moves)
    {
        List<PotentialTarget> targets = new List<PotentialTarget>();
        if (State.GameManager.TacticalMode.IsOnlyOneSideVisible() && actor.Unit.IsInfiltratingSide(AISide))
            return targets;
        foreach (Actor_Unit unit in actors)
        {
            if (unit.Targetable == true && TacticalUtilities.TreatAsHostile(actor, unit) && (unit.Surrendered == false || (onlySurrenderedEnemies && lackPredators) || currentTurn > 150))
            {

                int distance = unit.Position.GetNumberOfMovesDistance(position);
                if (distance < moves)
                {
                    if (distance > 1 && TacticalUtilities.FreeSpaceAroundTarget(unit.Position, actor) == false)
                        continue;
                }
                int chance = (int)unit.GetAttackChance(actor, false, true);
                int damage = actor.WeaponDamageAgainstTarget(unit, false);
                targets.Add(new PotentialTarget(unit, chance, distance, damage, ((unit.InSight && State.World.IsNight) ? 100 : 0)));


            }
        }

        PotentialTarget primeTarget = targets.Where(t => t.distance < 2).OrderByDescending(s => s.utility).FirstOrDefault();
        if (primeTarget != null)
            return new List<PotentialTarget>() { primeTarget };
        return targets.OrderByDescending(t => t.utility).ToList();
    }

    protected virtual void TryResurrect(Actor_Unit actor)
    {
        if (actor.Unit.UseableSpells == null || actor.Unit.UseableSpells.Any() == false)
            return;
        //var damageSpells = actor.Unit.UseableSpells.Where(s => s is DamageSpell);



        Spell spell = actor.Unit.UseableSpells.Where(s => s.SpellType == SpellTypes.Resurrection).FirstOrDefault();
        if (spell == null)
            return;

        if (spell.ManaCost > actor.Unit.Mana)
            return;
        if (TacticalUtilities.FindUnitToResurrect(actor) == null)
            return;


        for (int i = 0; i < 4; i++)
        {
            int x = State.Rand.Next(actor.Position.x - 2, actor.Position.x + 3);
            int y = State.Rand.Next(actor.Position.y - 2, actor.Position.y + 3);
            Vec2i loc = new Vec2i(x, y);
            if (TacticalUtilities.OpenTile(loc, null))
            {
                if (spell.TryCast(actor, loc))
                {
                    didAction = true;
                    return;
                }
            }
        }
    }

    protected virtual void TryReanimate(Actor_Unit actor)
    {
        if (actor.Unit.UseableSpells == null || actor.Unit.UseableSpells.Any() == false)
            return;
        //var damageSpells = actor.Unit.UseableSpells.Where(s => s is DamageSpell);



        Spell spell = actor.Unit.UseableSpells.Where(s => s.SpellType == SpellTypes.Reanimate).FirstOrDefault();
        if (spell == null)
            return;

        if (spell.ManaCost > actor.Unit.Mana)
            return;
        if (TacticalUtilities.FindUnitToReanimate(actor) == null)
            return;


        for (int i = 0; i < 4; i++)
        {
            int x = State.Rand.Next(actor.Position.x - 2, actor.Position.x + 3);
            int y = State.Rand.Next(actor.Position.y - 2, actor.Position.y + 3);
            Vec2i loc = new Vec2i(x, y);
            if (TacticalUtilities.OpenTile(loc, null))
            {
                if (spell.TryCast(actor, loc))
                {
                    didAction = true;
                    return;
                }
            }
        }
    }

    protected virtual int CheckResurrect(Actor_Unit actor, Vec2i position, int ap)
    {
        if (actor.Unit.UseableSpells == null || actor.Unit.UseableSpells.Any() == false)
            return -1;
        //var damageSpells = actor.Unit.UseableSpells.Where(s => s is DamageSpell);



        Spell spell = actor.Unit.UseableSpells.Where(s => s.SpellType == SpellTypes.Resurrection).FirstOrDefault();
        if (spell == null)
            return -1;

        if (spell.ManaCost > actor.Unit.Mana)
            return -1;
        if (TacticalUtilities.FindUnitToResurrect(actor) == null)
            return -1;


        for (int i = 0; i < 4; i++)
        {
            int x = State.Rand.Next(position.x - 2, position.x + 3);
            int y = State.Rand.Next(position.y - 2, position.y + 3);
            Vec2i loc = new Vec2i(x, y);
            if (TacticalUtilities.OpenTile(loc, null))
            {
                if (actor.Unit.Mana >= spell.ManaCost && ap > 0)
                {
                    return 1;
                }
            }
        }
        return -1;

    }

    protected virtual int CheckReanimate(Actor_Unit actor, Vec2i position, int ap)
    {
        if (actor.Unit.UseableSpells == null || actor.Unit.UseableSpells.Any() == false)
            return -1;
        //var damageSpells = actor.Unit.UseableSpells.Where(s => s is DamageSpell);



        Spell spell = actor.Unit.UseableSpells.Where(s => s.SpellType == SpellTypes.Reanimate).FirstOrDefault();
        if (spell == null)
            return -1;

        if (spell.ManaCost > actor.Unit.Mana)
            return -1;
        if (TacticalUtilities.FindUnitToReanimate(actor) == null)
            return -1;


        for (int i = 0; i < 4; i++)
        {
            int x = State.Rand.Next(position.x - 2, position.x + 3);
            int y = State.Rand.Next(position.y - 2, position.y + 3);
            Vec2i loc = new Vec2i(x, y);
            if (TacticalUtilities.OpenTile(loc, null))
            {
                if (actor.Unit.Mana >= spell.ManaCost && ap > 0)
                {
                    return 1;
                }
            }
        }
        return -1;

    }

    protected virtual void RunBind(Actor_Unit actor)
    {
        var spell = actor.Unit.UseableSpells.Find(s => s.SpellType == SpellTypes.Bind);
        if (spell == null || actor.Unit.Mana < spell.ManaCost) return;

        if (!actors.Any(a => a.Unit.Type == UnitType.Summon) && actor.Unit.BoundUnit == null)
        {
            return;
        }

        if (actor.Unit.BoundUnit != null && !actors.Any(a => !a.Unit.IsDead && a.Unit == actor.Unit.BoundUnit.Unit))
        {
            for (int i = 0; i < 4; i++)
            {
                int x = State.Rand.Next(actor.Position.x - 2, actor.Position.x + 3);
                int y = State.Rand.Next(actor.Position.y - 2, actor.Position.y + 3);
                Vec2i loc = new Vec2i(x, y);
                if (TacticalUtilities.OpenTile(loc, null))
                {
                    if (spell.TryCast(actor, loc))
                    {
                        didAction = true;
                        return;
                    }
                }
            }
        }
        List<PotentialTarget> targets = new List<PotentialTarget>();
        foreach (Actor_Unit unit in actors)
        {
            if (unit.Unit.Type == UnitType.Summon && TacticalUtilities.TreatAsHostile(actor, unit))
            {
                var prevBinder = actors.Where(a => a.Unit.BoundUnit?.Unit == unit.Unit).FirstOrDefault();

                if (unit.Targetable == true && (unit.InSight || !State.World.IsNight) && unit.Surrendered == false && prevBinder?.Unit.GetApparentSide(actor.Unit) != actor.Unit.FixedSide && prevBinder?.Unit.FixedSide != actor.Unit.FixedSide)
                {
                    int distance = unit.Position.GetNumberOfMovesDistance(actor.Position);
                    float chance = unit.GetMagicChance(unit, spell);
                    targets.Add(new PotentialTarget(unit, chance, distance, 4));
                }
            }
        }
        if (!targets.Any())
        {
            foreach (Actor_Unit unit in actors)
            {
                if (unit.Unit.Type == UnitType.Summon && !TacticalUtilities.TreatAsHostile(actor, unit))
                {
                    var prevBinder = actors.Where(a => a.Unit.BoundUnit?.Unit == unit.Unit).FirstOrDefault();

                    if (unit.Targetable == true && (unit.InSight || !State.World.IsNight) && unit.Surrendered == false && prevBinder?.Unit.GetApparentSide(actor.Unit) != actor.Unit.FixedSide && prevBinder?.Unit.FixedSide != actor.Unit.FixedSide)
                    {
                        int distance = unit.Position.GetNumberOfMovesDistance(actor.Position);
                        float chance = unit.GetMagicChance(unit, spell);
                        targets.Add(new PotentialTarget(unit, chance, distance, 4));
                    }
                }
            }
        }
        if (!targets.Any())
            return;
        Actor_Unit reserveTarget = targets[0].actor;
        while (targets.Any())
        {
            if (targets[0].distance <= spell.Range.Max)
            {
                if (spell.TryCast(actor, targets[0].actor))
                    didAction = true;
                return;
            }
            else
            {
                if (targets[0].actor.Position.GetNumberOfMovesDistance(actor.Position) <= actor.Movement + spell.Range.Max) //discard the clearly impossible
                {
                    MoveToAndAction(actor, targets[0].actor.Position, spell.Range.Max, actor.Movement, () => spell.TryCast(actor, targets[0].actor));
                    if (foundPath && path.Path.Count() < actor.Movement)
                        return;
                    else
                    {
                        foundPath = false;
                        path = null;
                    }
                }
            }
            targets.RemoveAt(0);
        }
    }

    protected virtual int CheckBind(Actor_Unit actor, Vec2i position, int ap)
    {
        var spell = actor.Unit.UseableSpells.Find(s => s.SpellType == SpellTypes.Bind);
        if (spell == null || actor.Unit.Mana < spell.ManaCost) return -1;

        if (!actors.Any(a => a.Unit.Type == UnitType.Summon) && actor.Unit.BoundUnit == null)
        {
            return -1;
        }

        if (actor.Unit.BoundUnit != null && !actors.Any(a => !a.Unit.IsDead && a.Unit == actor.Unit.BoundUnit.Unit))
        {
            return 1;
        }
        List<PotentialTarget> targets = new List<PotentialTarget>();
        foreach (Actor_Unit unit in actors)
        {
            if (unit.Unit.Type == UnitType.Summon && TacticalUtilities.TreatAsHostile(actor, unit))
            {
                var prevBinder = actors.Where(a => a.Unit.BoundUnit?.Unit == unit.Unit).FirstOrDefault();

                if (unit.Targetable == true && (unit.InSight || !State.World.IsNight) && unit.Surrendered == false && prevBinder?.Unit.GetApparentSide(actor.Unit) != actor.Unit.FixedSide && prevBinder?.Unit.FixedSide != actor.Unit.FixedSide)
                {
                    int distance = unit.Position.GetNumberOfMovesDistance(actor.Position);
                    float chance = unit.GetMagicChance(unit, spell);
                    targets.Add(new PotentialTarget(unit, chance, distance, 4));
                }
            }
        }
        if (!targets.Any())
        {
            foreach (Actor_Unit unit in actors)
            {
                if (unit.Unit.Type == UnitType.Summon && !TacticalUtilities.TreatAsHostile(actor, unit))
                {
                    var prevBinder = actors.Where(a => a.Unit.BoundUnit?.Unit == unit.Unit).FirstOrDefault();

                    if (unit.Targetable == true && (unit.InSight || !State.World.IsNight) && unit.Surrendered == false && prevBinder?.Unit.GetApparentSide(actor.Unit) != actor.Unit.FixedSide && prevBinder?.Unit.FixedSide != actor.Unit.FixedSide)
                    {
                        int distance = unit.Position.GetNumberOfMovesDistance(actor.Position);
                        float chance = unit.GetMagicChance(unit, spell);
                        targets.Add(new PotentialTarget(unit, chance, distance, 4));
                    }
                }
            }
        }
        if (!targets.Any())
            return -1;
        Actor_Unit reserveTarget = targets[0].actor;
        while (targets.Any())
        {
            if (targets[0].distance <= spell.Range.Max)
            {
                if (actor.Unit.Mana >= spell.ManaCost && ap > 0)
                {
                    return 1;
                }
            }
            else
            {
                if (targets[0].actor.Position.GetNumberOfMovesDistance(actor.Position) <= actor.Movement + spell.Range.Max) //discard the clearly impossible
                {
                    var distance = CheckMoveTo(actor, position, targets[0].actor.Position, 1, actor.Movement);
                    if (distance < ap && distance >= 0)
                        return distance + 1;
                }
            }
            targets.RemoveAt(0);
        }
        return -1;
    }


    protected virtual int CheckSpells(Actor_Unit actor, Vec2i position, int ap)
    {
        if (actor.Unit.UseableSpells == null || actor.Unit.UseableSpells.Any() == false)
            return -1;

        var availableSpells = actor.Unit.UseableSpells.Where(sp => sp != SpellList.Resurrection && sp != SpellList.Reanimate && sp != SpellList.Bind && sp.ManaCost <= actor.Unit.Mana).ToList();

        if (availableSpells == null || availableSpells.Any() == false)
            return -1;

        Spell spell = availableSpells[State.Rand.Next(availableSpells.Count())];

        if (State.GameManager.TacticalMode.IsOnlyOneSideVisible())
            return -1;
        if (spell == SpellList.Summon) 
        {
            
                        return 1;
                    
        }
        List<PotentialTarget> targets = GetListOfPotentialSpellTargets(actor, spell, position);
        if (!targets.Any())
            return -1;
        Actor_Unit reserveTarget = targets[0].actor;
        while (targets.Any())
        {
            if (targets[0].distance <= spell.Range.Max)
            {
                if (actor.Unit.Mana >= spell.ManaCost && ap > 0)
                {
                    return 1;
                }
            }
            else
            {
                if (targets[0].actor.Position.GetNumberOfMovesDistance(actor.Position) < actor.Movement) //discard the clearly impossible
                {
                    var distance = CheckMoveTo(actor, position, targets[0].actor.Position, 1, actor.Movement);
                    if (distance < ap && distance >= 0)
                        return distance + 1;
                }
            }
            targets.RemoveAt(0);
        }
        return -1;
    }

    protected virtual void RunSpells(Actor_Unit actor)
    {
        if (actor.Unit.UseableSpells == null || actor.Unit.UseableSpells.Any() == false)
            return;
        var availableSpells = actor.Unit.UseableSpells.Where(sp => sp != SpellList.Resurrection && sp != SpellList.Reanimate && sp != SpellList.Bind && sp.ManaCost <= actor.Unit.Mana).ToList();

        if (availableSpells == null || availableSpells.Any() == false)
            return;

        Spell spell = availableSpells[State.Rand.Next(availableSpells.Count())];

        if ((spell == SpellList.Charm || spell == SpellList.HypnoGas) && TacticalUtilities.GetMindControlSide(actor.Unit) != -1) // Charmed units should not use charm. Trust me.
            return;

        if (State.GameManager.TacticalMode.IsOnlyOneSideVisible())
            return;
        if (spell == SpellList.Summon) //Replace with better logic later
        {
            for (int i = 0; i < 4; i++)
            {
                int x = State.Rand.Next(actor.Position.x - 2, actor.Position.x + 3);
                int y = State.Rand.Next(actor.Position.y - 2, actor.Position.y + 3);
                Vec2i loc = new Vec2i(x, y);
                if (TacticalUtilities.OpenTile(loc, null))
                {
                    if (spell.TryCast(actor, loc))
                    {
                        didAction = true;
                        return;
                    }
                }
            }
        }
        List<PotentialTarget> targets = GetListOfPotentialSpellTargets(actor, spell, actor.Position);
        if (!targets.Any())
            return;
        Actor_Unit reserveTarget = targets[0].actor;
        while (targets.Any())
        {
            if (targets[0].distance <= spell.Range.Max && (targets[0].actor.InSight || !State.World.IsNight))
            {
                if (spell.TryCast(actor, targets[0].actor))
                    didAction = true;
                return;
            }
            else
            {
                if (targets[0].actor.Position.GetNumberOfMovesDistance(actor.Position) <= actor.Movement + spell.Range.Max && (targets[0].actor.InSight || !State.World.IsNight)) //discard the clearly impossible
                {
                    MoveToAndAction(actor, targets[0].actor.Position, spell.Range.Max, actor.Movement, () => spell.TryCast(actor, targets[0].actor));
                    if (foundPath && path.Path.Count() < actor.Movement)
                        return;
                    else
                    {
                        foundPath = false;
                        path = null;
                    }
                }
            }
            targets.RemoveAt(0);
        }
    }

    protected virtual List<PotentialTarget> GetListOfPotentialSpellTargets(Actor_Unit actor, Spell spell, Vec2i position)
    {
        List<PotentialTarget> targets = new List<PotentialTarget>();
        if (State.GameManager.TacticalMode.IsOnlyOneSideVisible() && actor.Unit.IsInfiltratingSide(AISide))
            return targets;
        foreach (Actor_Unit unit in actors)
        {
            if (spell is StatusSpell statusSpell && unit.Unit.GetStatusEffect(statusSpell.Type) != null)
                continue; //Don't recast the same spell on the same unit
            if (TacticalUtilities.TreatAsHostile(actor, unit) && spell.AcceptibleTargets.Contains(AbilityTargets.Enemy))
            {
                if (spell.AreaOfEffect > 0)
                {
                    int distance = unit.Position.GetNumberOfMovesDistance(actor.Position);
                    float chance = unit.GetMagicChance(unit, spell);
                    int friendlies = 0;
                    int enemies = 0;
                    foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(unit.Position, spell.AreaOfEffect))
                    {

                        if (spell is StatusSpell status && splashTarget.Unit.GetStatusEffect(status.Type) != null)
                            continue;
                        if (!TacticalUtilities.TreatAsHostile(actor, splashTarget))
                            friendlies++;
                        else if (splashTarget.Surrendered == false)
                            enemies++;
                    }
                    int net = enemies - friendlies;
                    if (net < 1)
                        continue;
                    targets.Add(new PotentialTarget(unit, net, distance, 4, net * 1000 + chance));
                }
                if (unit.Targetable == true && (unit.InSight || !State.World.IsNight) && unit.Surrendered == false)
                {
                    int distance = unit.Position.GetNumberOfMovesDistance(position);
                    float chance = unit.GetMagicChance(unit, spell);
                    targets.Add(new PotentialTarget(unit, chance, distance, 4));
                }
            }
            else if (!TacticalUtilities.TreatAsHostile(actor, unit) && spell.AcceptibleTargets.Contains(AbilityTargets.Ally))
            {
                if (spell == SpellList.Mending && (100 * unit.Unit.HealthPct) > 84)
                    continue;
                if (spell is StatusSpell statSpell)
                {
                    if (actor.Unit.GetStatusEffect(statSpell.Type) != null)
                        continue;
                }
                if (unit.Targetable == true && unit.Surrendered == false)
                {
                    int distance = unit.Position.GetNumberOfMovesDistance(position);
                    float chance = unit.GetMagicChance(unit, spell);
                    targets.Add(new PotentialTarget(unit, chance, distance, unit.Unit.Level));

                }
            }
            else if (!TacticalUtilities.TreatAsHostile(actor, unit) && spell.AcceptibleTargets.Contains(AbilityTargets.Self))
            {
                if (actor.Unit != unit.Unit)
                    continue;
                if (spell == SpellList.AssumeForm && unit?.PredatorComponent.PreyCount <= 0)
                    continue;
                if (spell == SpellList.RevertForm && unit.Unit.Race == unit.Unit.HiddenRace)
                    continue;
                if (unit.Targetable == true && unit.Surrendered == false)
                {
                    int distance = unit.Position.GetNumberOfMovesDistance(position);
                    float chance = unit.GetMagicChance(unit, spell);
                    targets.Add(new PotentialTarget(unit, chance, distance, unit.Unit.Level));
                }
            }
        }
        return targets.OrderByDescending(t => t.utility).ToList();
    }



    protected virtual bool IsRanged(Actor_Unit actor)
    {
        return actor.BestRanged != null;
    }

    public virtual void HandleLeftoverForeigns(Actor_Unit actor) 
    {
        actor.Unit.hiddenFixedSide = false;
       if (actor.Unit.HasTrait(Traits.Infiltrator) && actor.Unit.Type != UnitType.Summon) // You were there to cause the enemy a headache, get right back to it!
        {
            if (State.GameManager.TacticalMode.currentTurn < 500) // Someone just got STUCK trying to flee forever. Wow.
            {
                if (!retreating) { 
                    retreating = true;                      // Will hopefully cause inattentive opponents to have these sneaking right back into their cities
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=orange>{(actors[0].Unit.Side == AISide ? "Attackers" : "Defenders")} are now fleeing</color>");
                }
                return;
            }
        }
        if (actor.allowedToDefect || (State.GameManager.TacticalMode.currentTurn > 500 && !actor.DefectedThisTurn))
        {
            State.GameManager.TacticalMode.SwitchAlignment(actor);
            actor.DefectedThisTurn = true;
        }
    }

    public void RunFeed(Actor_Unit actor, string feedType, bool forCombatHealing = false)
    {
        // Fail if no possible targets
        List<PotentialTarget> targets = GetListOfPotentialFeedTargets(actor);
        if (!targets.Any())
            return;
        if (targets[0].chance <= 1.0f && forCombatHealing)
            return;

        // If possible targets present
        while (targets.Any())
        {
            // If target is in range
            if (targets[0].distance < 2)
            {
                if (feedType == "breast")
                    actor.PredatorComponent.Feed(targets[0].actor);
                if (feedType == "cock")
                    actor.PredatorComponent.FeedCum(targets[0].actor);
                didAction = true;
                break;
            }
            else
            {
                if (feedType == "breast")
                    MoveToAndAction(actor, targets[0].actor.Position, 1, actor.Movement - 1, () => actor.PredatorComponent.Feed(targets[0].actor));
                if (feedType == "cock")
                    MoveToAndAction(actor, targets[0].actor.Position, 1, actor.Movement - 1, () => actor.PredatorComponent.FeedCum(targets[0].actor));
                if (foundPath && path.Path.Count() < actor.Movement)
                    break;
                else
                {
                    foundPath = false;
                    path = null;
                }
            }
            targets.RemoveAt(0);
        }
    }

    List<PotentialTarget> GetListOfPotentialFeedTargets(Actor_Unit actor)
    {
        List<PotentialTarget> targets = new List<PotentialTarget>();
        foreach (Actor_Unit unit in actors)
        {
            if (unit.Targetable && unit.Unit.Side == AISide)
            {
                int distance = unit.Position.GetNumberOfMovesDistance(actor.Position);
                if (distance < actor.Movement)
                {
                    if ((distance > 1 && TacticalUtilities.FreeSpaceAroundTarget(unit.Position, actor) == false) || (unit.Unit.HealthPct == 1.0f && !Config.OverhealEXP) || unit == actor)
                        continue;
                    if (Config.OverhealEXP && unit.Unit.HealthPct == 1.0f)
                        targets.Add(new PotentialTarget(unit, 1.0f + unit.Unit.Experience, distance, 4));
                    else
                        targets.Add(new PotentialTarget(unit, unit.Unit.HealthPct, distance, 4));
                }
            }
        }
        PotentialTarget primeTarget = targets.Where(t => t.distance == 1).OrderBy(s => s.chance).FirstOrDefault();
        if (primeTarget != null)
            return new List<PotentialTarget>() { primeTarget };
        return targets.OrderBy(t => t.chance).ToList();
    }

    public void RunSuckle(Actor_Unit actor)
    {
        // Fail if no possible targets
        List<PotentialTarget> targets = GetListOfPotentialSuckleTargets(actor);
        if (!targets.Any())
            return;
        // If unit can vore and possible targets present
        while (targets.Any())
        {
            // If target is in range
            if (targets[0].distance < 2)
            {
                actor.PredatorComponent.Suckle(targets[0].actor);
                didAction = true;
                break;
            }
            else
            {
                MoveToAndAction(actor, targets[0].actor.Position, 1, 999, () => actor.PredatorComponent.Suckle(targets[0].actor));
                if (foundPath && path.Path.Count() < actor.Movement)
                    break;
                else
                {
                    foundPath = false;
                    path = null;
                }
            }
            targets.RemoveAt(0);
        }
    }

    List<PotentialTarget> GetListOfPotentialSuckleTargets(Actor_Unit actor)
    {
        List<PotentialTarget> targets = new List<PotentialTarget>();
        foreach (Actor_Unit unit in actors)
        {
            if (unit.Unit.Predator)
            {
                if (unit.Targetable && unit.Unit.Side == AISide && (unit.PredatorComponent.CanFeed() || unit.PredatorComponent.CanFeedCum()))
                {
                    int distance = unit.Position.GetNumberOfMovesDistance(actor.Position);
                    if (distance < actor.Movement)
                    {
                        if ((distance > 1 && TacticalUtilities.FreeSpaceAroundTarget(unit.Position, actor) == false) || (unit.Unit.HealthPct == 1.0f && !Config.OverhealEXP) || unit == actor)
                            continue;
                        int[] suckling = actor.PredatorComponent.GetSuckle(unit);
                        if (actor.Unit.HealthPct < 1.0f && suckling[0] == 0)
                            targets.Add(new PotentialTarget(unit, suckling[0], distance, 4));
                        if (Config.OverhealEXP && suckling[1] != 0)
                            targets.Add(new PotentialTarget(unit, suckling[1], distance, 4));
                    }
                }
            }
        }
        PotentialTarget primeTarget = targets.Where(t => t.distance == 1).OrderByDescending(s => s.chance).FirstOrDefault();
        if (primeTarget != null)
            return new List<PotentialTarget>() { primeTarget };
        return targets.OrderByDescending(t => t.chance).ToList();
    }

}
