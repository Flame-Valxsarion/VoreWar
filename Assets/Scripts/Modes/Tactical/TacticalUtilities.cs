using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

static class TacticalUtilities
{
    static Army[] armies;
    static Village village;
    static List<Actor_Unit> garrison;
    static TacticalTileType[,] tiles;
    static bool[,] blockedTiles;
    static bool[,] blockedClimberTiles;
    static HirePanel UnitPickerUI;

    static internal TacticalMessageLog Log => State.GameManager.TacticalMode.Log;

    static internal List<Actor_Unit> Units { get; private set; }

    static internal void ResetData()
    {
        armies = null;
        village = null;
        Units = null;
        tiles = null;
        garrison = null;
        blockedTiles = null;
        blockedClimberTiles = null;
    }

    static internal void ResetData(Army[] larmies, Village lvillage, List<Actor_Unit> lunits, List<Actor_Unit> lgarrison, TacticalTileType[,] ltiles, bool[,] lblockedTiles, bool[,] lblockedClimberTiles)
    {
        armies = larmies;
        village = lvillage;
        Units = lunits;
        tiles = ltiles;
        garrison = lgarrison;
        blockedTiles = lblockedTiles;
        blockedClimberTiles = lblockedClimberTiles;
        UnitPickerUI = State.GameManager.TacticalMode.UnitPickerUI;
    }

    static internal void ProcessTravelingUnits(List<Unit> travelingUnits)
    {
        if (State.World.Villages == null)
        {
            return;
        }
        if (travelingUnits[0].Side < 60)
        {
            if (Config.TroopScatter)
            {
                foreach (var unit in travelingUnits.ToList())
                {
                    var villageList = State.World.Villages.Where(s => travelingUnits[0].Side == s.Side && s != village).ToList();
                    Village friendlyVillage;
                    if (villageList.Count() == 0)
                        continue;
                    if (villageList.Count() == 1)
                        friendlyVillage = villageList[0];
                    else
                        friendlyVillage = villageList[State.Rand.Next(villageList.Count())];
                    var loc = StrategyPathfinder.GetArmyPath(null, armies[0], friendlyVillage.Position, 3, false, 999);
                    int turns = 9999;
                    int flightTurns = 9999;
                    Vec2i destination = null;
                    bool flyersExist = unit.HasTrait(Traits.Pathfinder);
                    if (loc != null && loc.Count > 0)
                    {
                        destination = new Vec2i(loc.Last().X, loc.Last().Y);
                        turns = StrategyPathfinder.TurnsToReach(null, armies[0], destination, Config.ArmyMP, false);
                        if (flyersExist)
                            flightTurns = StrategyPathfinder.TurnsToReach(null, armies[0], destination, Config.ArmyMP, true);
                    }
                    if (turns < 999)
                    {
                        if (flyersExist)
                            StrategicUtilities.CreateInvisibleTravelingArmy(unit, StrategicUtilities.GetVillageAt(destination), flightTurns);
                        else
                            StrategicUtilities.CreateInvisibleTravelingArmy(unit, StrategicUtilities.GetVillageAt(destination), turns);
                    }
                    travelingUnits.Remove(unit);
                }
            }
            else
            {
                var loc = StrategyPathfinder.GetPathToClosestObject(null, armies[0], State.World.Villages.Where(s => travelingUnits[0].Side == s.Side && s != village).Select(s => s.Position).ToArray(), 3, 999, false);
                int turns = 9999;
                int flightTurns = 9999;
                Vec2i destination = null;
                bool flyersExist = travelingUnits.Where(s => s.HasTrait(Traits.Pathfinder)).Count() > 0;
                if (loc != null && loc.Count > 0)
                {
                    destination = new Vec2i(loc.Last().X, loc.Last().Y);
                    turns = StrategyPathfinder.TurnsToReach(null, armies[0], destination, Config.ArmyMP, false);
                    if (flyersExist)
                        flightTurns = StrategyPathfinder.TurnsToReach(null, armies[0], destination, Config.ArmyMP, true);
                }
                if (turns < 999)
                {
                    if (flyersExist)
                        StrategicUtilities.CreateInvisibleTravelingArmy(travelingUnits.Where(s => s.HasTrait(Traits.Pathfinder)).ToList(), StrategicUtilities.GetVillageAt(destination), flightTurns);
                    StrategicUtilities.CreateInvisibleTravelingArmy(travelingUnits.Where(s => s.HasTrait(Traits.Pathfinder) == false).ToList(), StrategicUtilities.GetVillageAt(destination), turns);
                }
            }


        }
        else if (travelingUnits[0].Side > 500)
        {
            //Bandits and rebels that flee simply vanish
        }
        else
        {
            GenerateFleeingArmy(travelingUnits);
        }

    }



    static void GenerateFleeingArmy(List<Unit> fleeingUnits)
    {
        if (fleeingUnits.Any() == false)
            return;
        if (Config.MonstersCanReform == false)
            return;
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                Vec2i loc = new Vec2i(armies[0].Position.x + x, armies[0].Position.y + y);
                if (loc.x < 0 || loc.y < 0 || loc.x >= Config.StrategicWorldSizeX || loc.y >= Config.StrategicWorldSizeY)
                    continue;
                if (StrategicUtilities.IsTileClear(loc))
                {
                    MonsterEmpire monsterEmp = (MonsterEmpire)State.World.GetEmpireOfSide(fleeingUnits[0].Side);
                    if (monsterEmp == null)
                        return;
                    var army = new Army(monsterEmp, loc, fleeingUnits[0].Side);
                    army.RemainingMP = 0;
                    monsterEmp.Armies.Add(army);
                    army.Units.AddRange(fleeingUnits);
                    return;
                }
            }
        }
    }

    static internal void GenerateAnotherArmy(List<Unit> leftoverUnits)
    {
        if (leftoverUnits.Any() == false)
            return;
        if (Config.MonstersCanReform == false)
            return;
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                Vec2i loc = new Vec2i(armies[0].Position.x + x, armies[0].Position.y + y);
                if (loc.x < 0 || loc.y < 0 || loc.x >= Config.StrategicWorldSizeX || loc.y >= Config.StrategicWorldSizeY)
                    continue;
                if (StrategicUtilities.IsTileClear(loc))
                {
                    MonsterEmpire monsterEmp = (MonsterEmpire)State.World.GetEmpireOfSide(leftoverUnits[0].Side);
                    if (monsterEmp == null)
                        return;
                    var army = new Army(monsterEmp, loc, leftoverUnits[0].Side);
                    army.RemainingMP = 0;
                    monsterEmp.Armies.Add(army);
                    army.Units.AddRange(leftoverUnits);
                    return;
                }
            }
        }
    }

    static internal void CleanVillage(int remainingAttackers)
    {
        bool MonsterAttacker = armies[0].Side >= 100;
        SpawnerInfo spawner = Config.SpawnerInfo((Race)armies[0]?.Side);
        Config.MonsterConquestType spawnerType;
        if (spawner != null)
            spawnerType = spawner.GetConquestType();
        else
            spawnerType = Config.MonsterConquest;
        //clean up missing garrison units
        if (garrison != null)
        {
            foreach (Actor_Unit garrison in garrison)
            {
                if (garrison.Unit.IsDead || garrison.Fled || garrison.Unit.Side == armies[0].Side)
                {
                    village.VillagePopulation.RemoveHireable(garrison.Unit);
                }
            }
        }
        if (village.Side != armies[0].Side && remainingAttackers > 0 && (MonsterAttacker == false || spawnerType != Config.MonsterConquestType.DevourAndDisperse))
        {
            village.ChangeOwner(armies[0].Side);
        }
        else if (remainingAttackers > 0 && MonsterAttacker && spawnerType != Config.MonsterConquestType.DevourAndDisperse)
        {
            if (State.World.GetEmpireOfRace(village.Race)?.IsAlly(armies[0].Empire) ?? false)
                village.ChangeOwner(armies[0].Side);
        }

        if (MonsterAttacker && remainingAttackers > 0 && village.Empire.IsEnemy(armies[0].Empire))
        {
            if (spawnerType == Config.MonsterConquestType.DevourAndDisperse)
            {
                armies[0].RemainingMP = 1;
                State.GameManager.StrategyMode.Devour(armies[0], Mathf.Min(2 * armies[0].Units.Count, village.Population - 2));
                armies[0].Units = new List<Unit>();
            }
            else if (spawnerType == Config.MonsterConquestType.DevourAndHold)
            {
                if (village.GetTotalPop() > village.Maxpop / 2)
                {
                    armies[0].RemainingMP = 1;
                    State.GameManager.StrategyMode.Devour(armies[0], village.GetTotalPop() - village.Maxpop / 2);
                }
            }
            else //if (Config.MonsterConquest == Config.MonsterConquestType.CompleteDevourAndHold || Config.MonsterConquest == Config.MonsterConquestType.CompleteDevourAndMoveOn)
            {
                if (village.GetTotalPop() > 0)
                {
                    armies[0].RemainingMP = 1;
                    if (Config.MonsterConquestTurns > 1)
                    {
                        armies[0].MonsterTurnsRemaining = Config.MonsterConquestTurns;
                    }
                    else
                        State.GameManager.StrategyMode.Devour(armies[0], village.GetTotalPop() / Config.MonsterConquestTurns);
                }

            }


        }

    }
    internal static bool IsUnitControlledByPlayer(Unit unit)
    {
        if (GetMindControlSide(unit) != -1)  // Charmed units may fight for the player, but they are always AI controlled
            return false;
        int defenderSide = State.GameManager.TacticalMode.GetDefenderSide();
        int attackerSide = State.GameManager.TacticalMode.GetAttackerSide();
        bool aiDefender = State.GameManager.TacticalMode.AIDefender;
        bool aiAttacker = State.GameManager.TacticalMode.AIAttacker;
        if (State.GameManager.TacticalMode.CheatAttackerControl && unit.Side == attackerSide)
            return true;
        if (State.GameManager.TacticalMode.CheatDefenderControl && unit.Side == defenderSide)
            return true;

        if (State.GameManager.PureTactical)
        {
            return !aiAttacker && attackerSide == unit.FixedSide || !aiDefender && defenderSide == unit.FixedSide;
        }
        else
        {
            if (State.World.GetEmpireOfSide(unit.FixedSide) != null && State.World.GetEmpireOfSide(unit.FixedSide)?.StrategicAI == null)
            {
                return true;
            }
            bool prefSideHuman = !aiDefender && defenderSide == GetPreferredSide(unit, defenderSide, attackerSide) || !aiAttacker && attackerSide == GetPreferredSide(unit, attackerSide, defenderSide);
            bool currentSideHuman = !aiDefender && defenderSide == unit.Side || !aiAttacker && attackerSide == unit.Side;
            return prefSideHuman && currentSideHuman && !PlayerCanSeeTrueSide(unit); // "sleeping" infiltrators follow your orders while it doesn't go against their agenda.
        }
    }

    static internal bool AppropriateVoreTarget(Actor_Unit pred, Actor_Unit prey)
    {
        if (pred == prey)
            return false;
        if (pred.Unit.Side == prey.Unit.Side)
        {
            if (prey.Surrendered || pred.Unit.HasTrait(Traits.Cruel) || Config.AllowInfighting || pred.Unit.HasTrait(Traits.FriendlyStomach) || pred.Unit.HasTrait(Traits.Endosoma) || !(prey.Unit.GetApparentSide(pred.Unit) == pred.Unit.FixedSide && prey.Unit.GetApparentSide(pred.Unit) == pred.Unit.GetApparentSide()) || GetMindControlSide(prey.Unit) != -1 || GetMindControlSide(pred.Unit) != -1 )
                return true;
            return false;
        }
        return true;
    }

    static public int GetPreferredSide(Unit actor, int sideA, int sideB) // If equally aligned with both, should default to A
    {
        int effectiveActorSide = GetMindControlSide(actor) != -1 ? GetMindControlSide(actor) : actor.FixedSide;
        if (State.GameManager.PureTactical)
        {
            return effectiveActorSide;
        }

        int aISideHostility = 0;
        int enemySideHostility = 0;
        if (effectiveActorSide != sideA)
        {
            if (effectiveActorSide != sideB)
            {
                switch (RelationsManager.GetRelation(effectiveActorSide, sideB).Type)
                {
                    case RelationState.Allied:
                        {
                            enemySideHostility = 1;
                            break;
                        }
                    case RelationState.Neutral:
                        {
                            enemySideHostility = 2;
                            break;
                        }
                    case RelationState.Enemies:
                        {
                            enemySideHostility = 3;
                            break;
                        }
                }
            }
            switch (RelationsManager.GetRelation(effectiveActorSide, sideA).Type)
            {
                case RelationState.Allied:
                    {
                        aISideHostility = 1;
                        break;
                    }
                case RelationState.Neutral:
                    {
                        aISideHostility = 2;
                        break;
                    }
                case RelationState.Enemies:
                    {
                        aISideHostility = 3;
                        break;
                    }
            }
            return enemySideHostility >= aISideHostility ? sideA : sideB;
        }
        else
        {
            return sideA;
        }
    }

    static public bool TreatAsHostile(Actor_Unit actor, Actor_Unit target)
    {
        if (actor == target) return false;
        if (actor.Unit.Side == actor.Unit.FixedSide && !(target.sidesAttackedThisBattle?.Contains(actor.Unit.FixedSide) ?? false) && target.Unit.Side == actor.Unit.Side && GetMindControlSide(actor.Unit) == -1)
            return false;
        int friendlySide = actor.Unit.Side;
        int defenderSide = State.GameManager.TacticalMode.GetDefenderSide();
        int opponentSide = friendlySide == defenderSide ? State.GameManager.TacticalMode.GetAttackerSide() : defenderSide;
        int effectiveTargetSide = target.Unit.GetApparentSide(actor.Unit);
        int effectiveActorSide = GetMindControlSide(actor.Unit) != -1 ? GetMindControlSide(actor.Unit) : actor.Unit.FixedSide;
        if (GetMindControlSide(target.Unit) == effectiveActorSide)
            return false;
        if (State.GameManager.PureTactical)
        {
            return effectiveTargetSide != effectiveActorSide;
        }
        if (effectiveActorSide == effectiveTargetSide)
            return false;
        int aISideHostility = 0;
        int enemySideHostility = 0;
        int preferredSide;
        int unpreferredSide;
        if (effectiveActorSide != friendlySide)
        {
            if (effectiveActorSide != opponentSide)
            {
                switch (RelationsManager.GetRelation(effectiveActorSide, opponentSide).Type)
                {
                    case RelationState.Allied:
                        {
                            enemySideHostility = 1;
                            break;
                        }
                    case RelationState.Neutral:
                        {
                            enemySideHostility = 2;
                            break;
                        }
                    case RelationState.Enemies:
                        {
                            enemySideHostility = 3;
                            break;
                        }
                }
            }
            switch (RelationsManager.GetRelation(effectiveActorSide, friendlySide).Type)
            {
                case RelationState.Allied:
                    {
                        aISideHostility = 1;
                        break;
                    }
                case RelationState.Neutral:
                    {
                        aISideHostility = 2;
                        break;
                    }
                case RelationState.Enemies:
                    {
                        aISideHostility = 3;
                        break;
                    }
            }
            preferredSide = enemySideHostility >= aISideHostility ? friendlySide : opponentSide;
            unpreferredSide = preferredSide == friendlySide ? opponentSide : friendlySide;
        }
        else
        {
            preferredSide = friendlySide;
            unpreferredSide = opponentSide;
        }

        int targetSideHostilityP = 0;
        int targetSideHostilityUP = 0;
        if (preferredSide != effectiveTargetSide)
        {
            switch (RelationsManager.GetRelation(preferredSide, effectiveTargetSide).Type)
            {
                case RelationState.Allied:
                    {
                        targetSideHostilityP = 1;
                        break;
                    }
                case RelationState.Neutral:
                    {
                        targetSideHostilityP = 2;
                        break;
                    }
                case RelationState.Enemies:
                    {
                        targetSideHostilityP = 3;
                        break;
                    }
            }

        }
        if (unpreferredSide != effectiveTargetSide)
        {
            switch (RelationsManager.GetRelation(unpreferredSide, effectiveTargetSide).Type)
            {
                case RelationState.Allied:
                    {
                        targetSideHostilityUP = 1;
                        break;
                    }
                case RelationState.Neutral:
                    {
                        targetSideHostilityUP = 2;
                        break;
                    }
                case RelationState.Enemies:
                    {
                        targetSideHostilityUP = 3;
                        break;
                    }
            }

        }
        return targetSideHostilityP >= targetSideHostilityUP || (target.sidesAttackedThisBattle?.Contains(preferredSide) ?? false) || (target.sidesAttackedThisBattle?.Contains(actor.Unit.FixedSide) ?? false);
    }

    static public bool SneakAttackCheck(Unit attacker, Unit target)
    {
        if (GetMindControlSide(attacker) != -1) return false;
        return attacker.GetApparentSide(target) == target.GetApparentSide() && attacker.IsInfiltratingSide(target.GetApparentSide());
    }

    static public int GetMindControlSide(Unit unit)
    {
        if (unit.GetStatusEffect(StatusEffectType.Hypnotized) != null)
            return (int)(unit.GetStatusEffect(StatusEffectType.Hypnotized).Strength);
        if (unit.GetStatusEffect(StatusEffectType.Charmed) != null)
            return (int)(unit.GetStatusEffect(StatusEffectType.Charmed).Strength);
        return -1;
    }

    static public bool OpenTile(Vec2i vec, Actor_Unit actor) => OpenTile(vec.x, vec.y, actor);

    static public bool FreeSpaceAroundTarget(Vec2i targetLocation, Actor_Unit actor)
    {
        for (int x = targetLocation.x - 1; x < targetLocation.x + 2; x++)
        {
            for (int y = targetLocation.y - 1; y < targetLocation.y + 2; y++)
            {
                if (x == targetLocation.x && y == targetLocation.y)
                    continue;
                if (OpenTile(x, y, actor))
                {
                    return true;
                }
            }
        }
        return false;
    }

    static public bool OpenTile(int x, int y, Actor_Unit actor)
    {
        if (x < 0 || y < 0 || x > tiles.GetUpperBound(0) || y > tiles.GetUpperBound(1))
            return false;
        if (blockedTiles != null)
        {
            if (actor?.Unit.HasTrait(Traits.NimbleClimber) ?? false)
            {
                if (x <= blockedClimberTiles.GetUpperBound(0) || y <= blockedClimberTiles.GetUpperBound(1))
                {
                    if (blockedClimberTiles[x, y])
                        return false;
                }
            }
            else
            {
                if (x <= blockedTiles.GetUpperBound(0) || y <= blockedTiles.GetUpperBound(1))
                {
                    if (blockedTiles[x, y])
                        return false;
                }
            }

        }

        if (TacticalTileInfo.CanWalkInto(tiles[x, y], actor))
        {
            for (int i = 0; i < Units.Count; i++)
            {
                if (Units[i].Targetable == true && !Units[i].Hidden)
                {
                    if (Units[i].Position.x == x && Units[i].Position.y == y)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        return false;
    }

    static public bool PassableOpenTile(Vec2i vec, Actor_Unit actor) => PassableOpenTile(vec.x, vec.y, actor);

    static public bool PassableFreeSpaceAroundTarget(Vec2i targetLocation, Actor_Unit actor)
    {
        for (int x = targetLocation.x - 1; x < targetLocation.x + 2; x++)
        {
            for (int y = targetLocation.y - 1; y < targetLocation.y + 2; y++)
            {
                if (x == targetLocation.x && y == targetLocation.y)
                    continue;
                if (OpenTile(x, y, actor))
                {
                    return true;
                }
            }
        }
        return false;
    }

    static public bool PassableOpenTile(int x, int y, Actor_Unit actor)
    {
        if (x < 0 || y < 0 || x > tiles.GetUpperBound(0) || y > tiles.GetUpperBound(1))
            return false;
        if (blockedTiles != null)
        {
            if (actor?.Unit.HasTrait(Traits.NimbleClimber) ?? false)
            {
                if (x <= blockedClimberTiles.GetUpperBound(0) || y <= blockedClimberTiles.GetUpperBound(1))
                {
                    if (blockedClimberTiles[x, y])
                        return false;
                }
            }
            else
            {
                if (x <= blockedTiles.GetUpperBound(0) || y <= blockedTiles.GetUpperBound(1))
                {
                    if (blockedTiles[x, y])
                        return false;
                }
            }

        }

        if (TacticalTileInfo.CanWalkInto(tiles[x, y], actor))
        {
            for (int i = 0; i < Units.Count; i++)
            {
                if ((Units[i].Targetable == true && !Units[i].Hidden && Units[i].Unit.Side != actor.Unit.Side && actor.Unit.HasTrait(Traits.PassThrough)) || (Units[i].Targetable == true && !Units[i].Hidden && Units[i].Unit.Side == actor.Unit.Side && actor.Unit.HasTrait(Traits.Blitz)))
                {
                    if (Units[i].Position.x == x && Units[i].Position.y == y)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        return false;
    }

    static public bool TileContainsMoreThanOneUnit(int x, int y)
    {
        if (x < 0 || y < 0 || x > tiles.GetUpperBound(0) || y > tiles.GetUpperBound(1))
            return false;
        if (Units == null)
        {
            Debug.Log("This shouldn'targetPred have happened");
            return false;
        }
        int count = 0;
        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].Targetable == true)
            {
                if (Units[i].Position.x == x && Units[i].Position.y == y)
                {
                    count++;
                }
            }
        }

        return count > 1;
    }


    static public bool IsWalkable(int x, int y, Actor_Unit actor)
    {
        return TacticalTileInfo.CanWalkInto(tiles[x, y], actor);
    }


    static public bool FlyableTile(int x, int y)
    {
        if (x < 0 || y < 0 || x > tiles.GetUpperBound(0) || y > tiles.GetUpperBound(1))
            return false;
        return true;
    }


    static internal void CheckKnockBack(Actor_Unit Attacker, Actor_Unit Target, ref float damage)
    {
        int xDiff = Target.Position.x - Attacker.Position.x;
        int yDiff = Target.Position.y - Attacker.Position.y;
        int direction = Attacker.DiffToDirection(xDiff, yDiff);
        if (OpenTile(Attacker.GetTile(Target.Position, direction), Target))
            return;
        if (OpenTile(Attacker.GetTile(Target.Position, (direction + 1) % 8), Target))
            return;
        if (OpenTile(Attacker.GetTile(Target.Position, (direction + 7) % 8), Target))
            return;
        damage *= 1.2f;
        return;
    }

    static internal void KnockBack(Actor_Unit Attacker, Actor_Unit Target)
    {
        int xDiff = Target.Position.x - Attacker.Position.x;
        int yDiff = Target.Position.y - Attacker.Position.y;
        int direction = Attacker.DiffToDirection(xDiff, yDiff);

        Target.Movement += 1;
        if (Target.Move(direction, tiles))
            return;
        else if (Target.Move((direction + 1) % 8, tiles))
            return;
        else if (Target.Move((direction + 7) % 8, tiles))
            return;
        Target.Movement -= 1;

        return;
    }

    static internal void CheckSpellKnockBack(Vec2i Knocker, Actor_Unit Attacker, Actor_Unit Target, ref float damage)
    {
        int xDiff = Target.Position.x - Knocker.x;
        int yDiff = Target.Position.y - Knocker.y;
        int direction = Attacker.DiffToDirection(xDiff, yDiff);
        if (OpenTile(Attacker.GetTile(Target.Position, direction), Target))
            return;
        if (OpenTile(Attacker.GetTile(Target.Position, (direction + 1) % 8), Target))
            return;
        if (OpenTile(Attacker.GetTile(Target.Position, (direction + 7) % 8), Target))
            return;
        Target.Unit.TraitBoosts.Incoming.MagicDamage += ((int)(damage));

        return;
    }

    static internal void SpellKnockBack(Vec2i Knocker, Actor_Unit Attacker, Actor_Unit Target)
    {
        int xDiff = Target.Position.x - Knocker.x;
        int yDiff = Target.Position.y - Knocker.y;
        int direction = Attacker.DiffToDirection(xDiff, yDiff);

        Target.Movement += 1;
        if (Target.Move(direction, tiles))
            return;
        else if (Target.Move((direction + 1) % 8, tiles))
            return;
        else if (Target.Move((direction + 7) % 8, tiles))
            return;
        Target.Movement -= 1;

        return;
    }

    static internal PredatorComponent GetPredatorComponentOfUnit(Unit unit)
    {
        foreach (Actor_Unit actor in Units)
        {
            if (actor.Unit == unit)
                return actor.PredatorComponent;
        }
        return null;
    }

    static internal Actor_Unit FindPredator(Actor_Unit searcher)
    {
        foreach (Actor_Unit Unit in Units)
        {
            if (Unit.PredatorComponent?.IsActorInPrey(searcher) ?? false)
                return Unit;
        }
        return null;
    }

    static internal void UpdateActorLocations()
    {
        foreach (Actor_Unit unit in Units)
        {
            if (unit.UnitSprite == null)
                continue;
            Vec2i vec = unit.Position;
            Vector2 vector2 = new Vector2(vec.x, vec.y);
            unit.UnitSprite.transform.position = vector2;
        }
    }


    static internal void RefreshUnitGraphicType()
    {
        if (Units == null)
            return;
        foreach (Actor_Unit actor in Units)
        {
            if (actor.Unit.Race != Race.Imps && actor.Unit.Race != Race.Lamia && actor.Unit.Race != Race.Tigers)
            {
                Races.GetRace(actor.Unit).RandomCustom(actor.Unit);
            }
        }
    }

    static internal void UpdateVersion()
    {
        foreach (Actor_Unit actor in Units)
        {
            actor.PredatorComponent?.UpdateVersion();
        }
    }

    static internal List<Actor_Unit> UnitsWithinTiles(Vec2 target, int tiles)
    {
        List<Actor_Unit> unitList = new List<Actor_Unit>();
        foreach (Actor_Unit actor in Units)
        {
            
            if (actor.Visible && actor.Targetable)
            {
                if (actor.Position.GetNumberOfMovesDistance(target) <= tiles)
                {
                    unitList.Add(actor);
                }
            }
        }
        return unitList;
    }
    static internal Actor_Unit UnitOnTile(Vec2 target)
    {
        List<Actor_Unit> unitList = new List<Actor_Unit>();
        foreach (Actor_Unit actor in Units)
        {
            if (actor.Position == target)
            {
                return actor;
            }
        }
        return null;
    }

    static internal List<Vec2i> TilesOnPattern(Vec2i location, int[,] TargetTiles, int rows)
    {
        List<Vec2i> tile_positions = new List<Vec2i>();
        int outer_matrix_cursor = 0;
        for (int y = location.y + rows; y >= location.y - rows; y--)
        {
            int inner_matrix_cursor = 0;
            for (int x = location.x + rows; x >= location.x - rows; x--)
            {
                if (x < 0 || y < 0 || x > tiles.GetUpperBound(0) || y > tiles.GetUpperBound(1))
                {
                    inner_matrix_cursor++;
                    continue;
                }
                if (TargetTiles[outer_matrix_cursor,inner_matrix_cursor] >= 1)
                    tile_positions.Add(new Vec2i(x, y));
                inner_matrix_cursor++;
            }
            outer_matrix_cursor++;
        }
        return tile_positions;
    }
    static internal int GetRotatingOctant(Vec2i unit, Vec2i target)
    {
        /// Octant positions are as follows
        /// 7 0 1
        /// 6 X 2
        /// 5 4 3
        /// Where X is this unit's position
        float dist = unit.GetNumberOfMovesDistance(target);
        bool target_above_unit = unit.y < target.y;
        bool target_about_even_with_unit_vert = unit.y == target.y || dist > Math.Abs(unit.y - target.y) + 0.5 * dist;
        bool target_about_even_with_unit_horz = unit.x == target.x || dist > Math.Abs(unit.x - target.x) + 0.5 * dist;
        bool target_to_right_of_unit = unit.x <= target.x;

        if (target_about_even_with_unit_horz)
        {
            if (target_above_unit)
                return 0;
            else
                return 4;
        }
        if (target_about_even_with_unit_vert)
        {
            if (target_to_right_of_unit)
                return 6;
            else
                return 2;
        }
        if (target_above_unit)
        {
            if (target_to_right_of_unit)
            {
                return 1;
            }
            return 3;
        }
        else
        {
            if (target_to_right_of_unit)
            {
                return 7;
            }
            return 5;
        }
        
    }
    static internal Vec2i RoundIndexToPoint(int index, int radius)
    {
        if (radius == 0)
            return new Vec2i(0, 0);
        Vec2i result = new Vec2i(-radius, -radius);

        while (index < 0) index += radius * 8;
        index = index % (radius * 8);

        int edgeLen = radius * 2;

        if (index < edgeLen)
        {
            result.x += index;
        }
        else if ((index -= edgeLen) < edgeLen)
        {
            result.x = radius;
            result.y += index;
        }
        else if ((index -= edgeLen) < edgeLen)
        {
            result.x = radius - index;
            result.y = radius;
        }
        else if ((index -= edgeLen) < edgeLen)
        {
            result.y = radius - index;
        }

        return result;
    }

    static internal int[,] Rotate45(int[,] array)
    {
        int dim = array.GetLength(0);
        int[,] result = new int[dim, dim];

        Vec2i center = new Vec2i((result.GetLength(0) - 1) / 2, (result.GetLength(1) - 1) / 2);
        Vec2i center2 = new Vec2i((array.GetLength(0) - 1) / 2, (array.GetLength(1) - 1) / 2);
        for (int r = 0; r <= (dim - 1) / 2; r++)
        {
            for (int i = 0; i <= r * 8; i++)
            {
                Vec2i source = RoundIndexToPoint(i, r);
                Vec2i target = RoundIndexToPoint(i + r, r);

                if (!(center2.x + source.x < 0 || center2.y + source.y < 0 || center2.x + source.x >= array.GetLength(0) || center2.y + source.y >= array.GetLength(1)))
                    result[center.x + target.x, center.y + target.y] = array[center2.x + source.x, center2.y + source.y];
            }
        }
        return result;
    }
    static internal int[,] Rotate90(int[,] array, bool clockwise)
    {
        // printing the matrix on the basis of
        // observations made on indices.
        int dim = array.GetLength(0);
        int[,] result = new int[dim, dim];
        int outer_cursor = 0;
        if (clockwise)
        {
            for (int j = 0; j < dim; j++)
            {
                int inner_cursor = 0;
                for (int i = dim - 1; i >= 0; i--)
                {
                    result[outer_cursor, inner_cursor] = array[i, j];
                    inner_cursor++;
                }
                outer_cursor++;
            }
        }
        else
        {
            for (int j = dim - 1; j >= 0; j--)
            {
                int inner_cursor = 0;
                for (int i = 0; i < dim; i++)
                {
                    result[outer_cursor, inner_cursor] = array[i, j];
                    inner_cursor++;
                }
                outer_cursor++;
            }
        }

        return result;
    }
    static internal int[,] Rotate180(int[,] array)
    {
        // printing the matrix on the basis of
        // observations made on indices.
        int dim = array.GetLength(0);
        int[,] result = new int[dim, dim];
        int outer_cursor = 0;
        for (int i = dim - 1; i  >= 0; i--)
        {
            int inner_cursor = 0;
            for (int j = dim - 1; j >= 0; j--)
            {
                result[outer_cursor, inner_cursor] = array[i, j];
                inner_cursor++;
            }
            outer_cursor++;
        }
        return result;
    }
    static internal List<Vec2i> rotateTilePattern(Vec2i location, int[,] TargetTiles, int aoe_size, int octant)
    {
        int dim = TargetTiles.GetLength(0);
        int[,] rotated_tiles = TargetTiles;
        switch (octant)
        {
            case 0: // 0 deg
                break;
            case 1: // 45 deg
                rotated_tiles = Rotate45(rotated_tiles);
                break;
            case 2: // 90 deg
                rotated_tiles = Rotate90(rotated_tiles, true);
                break;
            case 3: // 135 deg
                rotated_tiles = Rotate90(rotated_tiles, true);
                rotated_tiles = Rotate45(rotated_tiles);
                break;
            case 4: // 180 deg
                rotated_tiles = Rotate180(rotated_tiles);
                break;
            case 5: // 225 deg
                rotated_tiles = Rotate180(rotated_tiles);
                rotated_tiles = Rotate45(rotated_tiles);
                break;
            case 6: // 270 deg
                rotated_tiles = Rotate90(rotated_tiles, false);
                break;
            case 7: // 315 deg
                rotated_tiles = Rotate90(rotated_tiles, false);
                rotated_tiles = Rotate45(rotated_tiles);
                break;
            default:
                Debug.Log("Incorrect Octant Error");
                break;
        }
        return TilesOnPattern(location, rotated_tiles, aoe_size);
    }

    static internal List<Actor_Unit> UnitsWithinPattern(Vec2i location, int[,] TargetTiles)
    {
        int target_box = (int)((Math.Sqrt(TargetTiles.Length) / 2) - 0.5);
        List<Actor_Unit> pruned_unitList = new List<Actor_Unit>();
        List<Actor_Unit> unitList = UnitsWithinTiles(new Vec2(location.x, location.y), target_box);
        List<Vec2i> tile_positions = TilesOnPattern(location, TargetTiles, target_box);
        foreach (Actor_Unit unit in unitList)
        {
            foreach (Vec2i target_tile in tile_positions)
            {
                if (unit.Position.x == target_tile.x && unit.Position.y == target_tile.y)
                {
                    pruned_unitList.Add(unit);
                }
            }
        }
        return pruned_unitList;
    }
    static internal List<Actor_Unit> UnitsWithinRotatingPattern(Vec2i location, int[,] TargetTiles, int octant)
    {
        int target_box = (int)((Math.Sqrt(TargetTiles.Length) / 2) - 0.5);
        List<Actor_Unit> pruned_unitList = new List<Actor_Unit>();
        List<Actor_Unit> unitList = UnitsWithinTiles(new Vec2(location.x, location.y), target_box);
        List<Vec2i> true_tile_positions = rotateTilePattern(location, TargetTiles, target_box, octant);
        foreach (Actor_Unit unit in unitList)
        {
            foreach (Vec2i target_tile in true_tile_positions)
            {
                if (unit.Position.x == target_tile.x && unit.Position.y == target_tile.y)
                {
                    pruned_unitList.Add(unit);
                }
            }
        }
        return pruned_unitList;
    }


    static internal Actor_Unit FindUnitToResurrect(Actor_Unit caster)
    {
        Actor_Unit actor = Units.Where(s => s.Unit.Side == caster.Unit.Side && s.Unit.IsDead && s.Unit.Type != UnitType.Summon && s.Unit.Level > 0).OrderByDescending(s => s.Unit.Experience).FirstOrDefault();
        return actor;
    }

    static internal Actor_Unit FindUnitToReanimate(Actor_Unit caster)
    {
        Actor_Unit actor = Units.Where(s => s.Unit.IsDead && s.Unit.Level > 0).OrderByDescending(s => s.Unit.Experience).FirstOrDefault();
        return actor;
    }


    internal static void CreateResurrectionPanel(Vec2i loc, int side)
    {
        int children = UnitPickerUI.ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            UnityEngine.Object.Destroy(UnitPickerUI.ActorFolder.transform.GetChild(i).gameObject);
        }
        Actor_Unit[] list = Units.Where(s => s.Unit.Side == side && s.Unit.IsDead && s.Unit.Type != UnitType.Summon && s.Unit.Level > 0).OrderByDescending(s => s.Unit.Experience).ToArray();
        foreach (Actor_Unit actorUnit in list)
        {
            Unit unit = actorUnit.Unit;
            GameObject obj = GameObject.Instantiate(UnitPickerUI.HiringUnitPanel, UnitPickerUI.ActorFolder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), unit);
            //Text text = obj.transform.GetChild(3).GetComponent<Text>();
            Text GenderText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            Text EXPText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
            GameObject EquipRow = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(2).gameObject;
            GameObject StatRow1 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(3).gameObject;
            GameObject StatRow2 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(4).gameObject;
            GameObject StatRow3 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(5).gameObject;
            GameObject StatRow4 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(6).gameObject;
            Text TraitList = obj.transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Text>();

            string gender;
            if (actor.Unit.GetGender() != Gender.None)
            {
                if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                    gender = "Herm";
                else
                    gender = actor.Unit.GetGender().ToString();
                GenderText.text = $"{gender}";
            }
            EXPText.text = $"Level {unit.Level} ({(int)unit.Experience} EXP)";
            if (actor.Unit.HasTrait(Traits.Resourceful))
            {
                EquipRow.transform.GetChild(2).gameObject.SetActive(true);
                EquipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = unit.GetItem(0)?.Name;
                EquipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = unit.GetItem(1)?.Name;
                EquipRow.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = unit.GetItem(2)?.Name;
            }
            else
            {
                EquipRow.transform.GetChild(2).gameObject.SetActive(false);
                EquipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = unit.GetItem(0)?.Name;
                EquipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = unit.GetItem(1)?.Name;
            }
            StatRow1.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Strength).ToString();
            StatRow1.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Dexterity).ToString();
            StatRow2.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Mind).ToString();
            StatRow2.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Will).ToString();
            StatRow3.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Endurance).ToString();
            StatRow3.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Agility).ToString();
            if (actor.PredatorComponent != null)
            {
                StatRow4.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Voracity).ToString();
                StatRow4.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Stomach).ToString();
            }
            else
                StatRow4.SetActive(false);
            TraitList.text = RaceEditorPanel.TraitListToText(unit.GetTraits, true).Replace(", ", "\n");
            //text.text += $"STR: {unit.GetStatBase(Stat.Strength)} DEX: { unit.GetStatBase(Stat.Dexterity)}\n" +
            //    $"MND: {unit.GetStatBase(Stat.Mind)} WLL: { unit.GetStatBase(Stat.Will)} \n" +
            //    $"END: {unit.GetStatBase(Stat.Endurance)} AGI: {unit.GetStatBase(Stat.Agility)}\n";
            //if (actor.PredatorComponent != null)
            //    text.text += $"VOR: {unit.GetStatBase(Stat.Voracity)} STM: { unit.GetStatBase(Stat.Stomach)}";
            actor.UpdateBestWeapons();
            sprite.UpdateSprites(actor);
            sprite.Name.text = unit.Name;
            Button button = obj.GetComponentInChildren<Button>();
            button.GetComponentInChildren<Text>().text = "Resurrect";
            button.onClick.AddListener(() => Resurrect(loc, actorUnit));
            button.onClick.AddListener(() => UnitPickerUI.gameObject.SetActive(false));
        }
        UnitPickerUI.ActorFolder.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * (1 + (children) / 3));
        UnitPickerUI.gameObject.SetActive(true);
    }

    internal static void CreateReanimationPanel(Vec2i loc, Unit unit)
    {
        int children = UnitPickerUI.ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            UnityEngine.Object.Destroy(UnitPickerUI.ActorFolder.transform.GetChild(i).gameObject);
        }
        Actor_Unit[] list = Units.Where(s => s.Unit.IsDead && s.Unit.Level > 0).OrderByDescending(s => s.Unit.Experience).ToArray();
        foreach (Actor_Unit actor in list)
        {
            GameObject obj = UnityEngine.Object.Instantiate(UnitPickerUI.HiringUnitPanel, UnitPickerUI.ActorFolder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            Text GenderText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            Text EXPText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
            GameObject EquipRow = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(2).gameObject;
            GameObject StatRow1 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(3).gameObject;
            GameObject StatRow2 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(4).gameObject;
            GameObject StatRow3 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(5).gameObject;
            GameObject StatRow4 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(6).gameObject;
            Text TraitList = obj.transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
            Text HireButton = obj.transform.GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<Text>();

            string gender;
                if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                    gender = "Herm";

                else
                    gender = actor.Unit.GetGender().ToString();
                GenderText.text = $"{gender}";
            TraitList.text = RaceEditorPanel.TraitListToText(actor.Unit.GetTraits, true).Replace(", ","\n");
            EXPText.text = $"Level {actor.Unit.Level} ({(int)actor.Unit.Experience} EXP)";
            if (actor.Unit.HasTrait(Traits.Resourceful))
            {
                EquipRow.transform.GetChild(2).gameObject.SetActive(true);
                EquipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = actor.Unit.GetItem(0)?.Name;
                EquipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = actor.Unit.GetItem(1)?.Name;
                EquipRow.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = actor.Unit.GetItem(2)?.Name;
            }
            else
            {
                EquipRow.transform.GetChild(2).gameObject.SetActive(false);
                EquipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = actor.Unit.GetItem(0)?.Name;
                EquipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = actor.Unit.GetItem(1)?.Name;
            }
            StatRow1.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Strength).ToString();
            StatRow1.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Dexterity).ToString();
            StatRow2.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Mind).ToString();
            StatRow2.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Will).ToString();
            StatRow3.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Endurance).ToString();
            StatRow3.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Agility).ToString();
            if (actor.PredatorComponent != null)
            {
                StatRow4.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Voracity).ToString();
                StatRow4.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = actor.Unit.GetStatBase(Stat.Stomach).ToString();
            }
            else
                StatRow4.SetActive(false);
            actor.UpdateBestWeapons();
            sprite.UpdateSprites(actor);
            sprite.Name.text = actor.Unit.Name;
            Button button = obj.GetComponentInChildren<Button>();
            button.GetComponentInChildren<Text>().text = "Reanimate";
            button.onClick.AddListener(() => {
                State.GameManager.SoundManager.PlaySpellCast(SpellList.Summon, actor);
                Reanimate(loc, actor, unit);
            });
            button.onClick.AddListener(() => UnitPickerUI.gameObject.SetActive(false));
        }
        UnitPickerUI.ActorFolder.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * (1 + (list.Length / 3)));
        UnitPickerUI.gameObject.SetActive(true);
    }

    static internal List<Vec2i> TilesWithinRange(Vec2i location, int range)
    {
        List<Vec2i> tile_positions = new List<Vec2i>();
        int outer_matrix_cursor = 0;
        for (int y = location.y + range; y >= location.y - range; y--)
        {
            int inner_matrix_cursor = 0;
            for (int x = location.x + range; x >= location.x - range; x--)
            {
                if (x < 0 || y < 0 || x > tiles.GetUpperBound(0) || y > tiles.GetUpperBound(1))
                {
                    inner_matrix_cursor++;
                    continue;
                }
                tile_positions.Add(new Vec2i(x, y));
            }
            outer_matrix_cursor++;
        }
        return tile_positions;
    }

 static internal Vec2i GetRandomTileForActor(Actor_Unit actor)
 {
     Vec2i[] tile_positions = TilesWithinRange(actor.Position, 6).Where(tile => OpenTile(tile, actor)).ToArray();
     if (tile_positions.Length == 0) { return null; }
     return tile_positions[UnityEngine.Random.Range(0, tile_positions.Length - 1)];
 }

    internal static void Resurrect(Vec2i loc, Actor_Unit target)
    {
        var pred = FindPredator(target);
        if (pred != null)
            pred.PredatorComponent.FreeUnit(target, true);
        target.Position.x = loc.x;
        target.Position.y = loc.y;
        target.Unit.Health = target.Unit.MaxHealth * 3 / 4;
        target.Visible = true;
        target.Targetable = true;
        target.SelfPrey = null;
        target.Surrendered = false;
        UpdateActorLocations();
        if (target.UnitSprite != null)
        {
            target.UnitSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            target.UnitSprite.LevelText.gameObject.SetActive(true);
            target.UnitSprite.FlexibleSquare.gameObject.SetActive(true);
            target.UnitSprite.HealthBar.gameObject.SetActive(true);
        }
    }

    internal static void Reanimate(Vec2i loc, Actor_Unit target, Unit caster)
    {
        var pred = FindPredator(target);
        if (pred != null)
            pred.PredatorComponent.FreeUnit(target, true);
        target.Position.x = loc.x;
        target.Position.y = loc.y;
        target.Unit.Health = target.Unit.MaxHealth * 3 / 4;
        target.Visible = true;
        target.Targetable = true;
        target.SelfPrey = null;
        target.Surrendered = false;
        target.sidesAttackedThisBattle = new List<int>();
        target.Unit.Type = UnitType.Summon;
        State.GameManager.TacticalMode.HandleReanimationSideEffects(caster, target);
        if (!target.Unit.HasTrait(Traits.Untamable))
            target.Unit.FixedSide = caster.FixedSide;

        var actorCharm = caster.GetStatusEffect(StatusEffectType.Charmed) ?? caster.GetStatusEffect(StatusEffectType.Hypnotized);
        if (actorCharm != null)
        {
            target.Unit.ApplyStatusEffect(StatusEffectType.Charmed, actorCharm.Strength, actorCharm.Duration);
        }
        UpdateActorLocations();
        if (target.UnitSprite != null)
        {
            target.UnitSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            target.UnitSprite.LevelText.gameObject.SetActive(true);
            target.UnitSprite.FlexibleSquare.gameObject.SetActive(true);
            target.UnitSprite.HealthBar.gameObject.SetActive(true);
        }
        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{caster.Name}</b> brought back <b>{target.Unit.Name}</b> as a summon.");
    }

    static internal bool MeetsQualifier(List<AbilityTargets> targets, Actor_Unit actor, Actor_Unit target)
    {
        if ((target.Unit.GetApparentSide() != actor.Unit.FixedSide) && targets.Contains(AbilityTargets.Enemy))
            return true;
        if ((target.Unit.GetApparentSide() == actor.Unit.GetApparentSide() || target.Unit.GetApparentSide() == actor.Unit.FixedSide) && targets.Contains(AbilityTargets.Ally))
            return true;
        if ((target.Unit.GetApparentSide() == actor.Unit.GetApparentSide() || target.Unit.GetApparentSide() == actor.Unit.FixedSide) && target.Surrendered && targets.Contains(AbilityTargets.SurrenderedAlly))
            return true;
        if (targets.Contains(AbilityTargets.Enemy) && Config.AllowInfighting)
            return true;
        if (targets.Contains(AbilityTargets.Self) && actor.Unit == target.Unit)
            return true;
        return false;

    }

    static internal Actor_Unit GetActorAt(Vec2 location)
    {
        foreach (Actor_Unit actor in Units)
        {
            if (actor == null)
                continue;
            if (actor.Position.x == location.x && actor.Position.y == location.y)
                return actor;
        }
        return null;
    }

    static internal Actor_Unit GetActorOf(Unit unit)
    {
        return Units.FirstOrDefault(actor => actor.Unit == unit);
    }

    static internal void CreateEffect(Vec2i location, TileEffectType type, int areaOfEffect, float strength, int duration)
    {
        for (int x = location.x - areaOfEffect; x <= location.x + areaOfEffect; x++)
        {
            for (int y = location.y - areaOfEffect; y <= location.y + areaOfEffect; y++)
            {
                if (x < 0 || y < 0 || x > tiles.GetUpperBound(0) || y > tiles.GetUpperBound(1))
                    continue;
                Vec2 position = new Vec2(x, y);
                TileEffect effect = new TileEffect(duration, strength, type);
                State.GameManager.TacticalMode.ActiveEffects[position] = effect;
                switch (type)
                {
                    case TileEffectType.Fire:
                        State.GameManager.TacticalMode.EffectTileMap.SetTile(new Vector3Int(position.x, position.y, 0), State.GameManager.TacticalMode.Pyre);
                        break;
                    case TileEffectType.IcePatch:
                        State.GameManager.TacticalMode.EffectTileMap.SetTile(new Vector3Int(position.x, position.y, 0), State.GameManager.TacticalMode.Ice);
                        break;
                }
            }
        }

    }

    static internal void CreateEffectWithPattern(Vec2i location, Vec2i unit, TileEffectType type, float strength, int duration, int[,] pattern, AreaOfEffectType aoeType)
    {
        List<Vec2i> tile_positions;
        int target_box = (int)((Math.Sqrt(pattern.Length) / 2) - 0.5);
        if (aoeType == AreaOfEffectType.RotatablePattern)
        {
            tile_positions = rotateTilePattern(location, pattern, target_box, GetRotatingOctant(unit, location));
        }
        else
            tile_positions = TilesOnPattern(location, pattern, target_box);
        foreach (Vec2i position in tile_positions)
        {
            TileEffect effect = new TileEffect(duration, strength, type);
            State.GameManager.TacticalMode.ActiveEffects[position] = effect;
            switch (type)
            {
                case TileEffectType.Fire:
                    State.GameManager.TacticalMode.EffectTileMap.SetTile(new Vector3Int(position.x, position.y, 0), State.GameManager.TacticalMode.Pyre);
                    break;
                case TileEffectType.IcePatch:
                    State.GameManager.TacticalMode.EffectTileMap.SetTile(new Vector3Int(position.x, position.y, 0), State.GameManager.TacticalMode.Ice);
                    break;
            }
        }
        

    }

    static public bool IsUnitControlledBySide(Unit unit, int side)
    {
        if (GetMindControlSide(unit) != -1)  // Charmed units may fight for a specific side, but for targeting purposes we'll consider them driven by separate forces
            return false;
        if (side == unit.FixedSide)
            return true;
        else if (State.GameManager.PureTactical)
            return false;
        if (unit.IsInfiltratingSide(side))
            return true;                    // hidden and compliant
        return false;
    }

    static public bool PlayerCanSeeTrueSide(Unit unit)
    {
        if (!unit.hiddenFixedSide || unit.FixedSide == unit.Side) return true;

        if (State.World.MainEmpires == null)
            return unit.FixedSide == (!State.GameManager.TacticalMode.AIAttacker ?
                State.GameManager.TacticalMode.GetAttackerSide() : (!State.GameManager.TacticalMode.AIDefender ? State.GameManager.TacticalMode.GetDefenderSide() : unit.FixedSide));

        if (StrategicUtilities.GetAllHumanSides().Count > 1) return false;
        if (StrategicUtilities.GetAllHumanSides().Count < 1) return true;



        if (State.GameManager.StrategyMode.LastHumanEmpire?.Side == unit.FixedSide)
            return true;

        if (RelationsManager.GetRelation(unit.FixedSide, State.GameManager.StrategyMode.LastHumanEmpire.Side).Type == RelationState.Allied)
         {
            return true;
         }
        
        return false;
    }

    static public bool UnitCanSeeTrueSideOfTarget(Unit viewer, Unit target)
    {
        if (!target.hiddenFixedSide || target.FixedSide == target.Side) return true;

        if (State.World.MainEmpires == null) return false;

        if (target.FixedSide == viewer.FixedSide)
            return true;

        if (RelationsManager.GetRelation(target.FixedSide, viewer.FixedSide).Type == RelationState.Allied)
        {
            return true;
        }
        // I was thinking about also giving units the insight of whatever side they're pretending to be on, but I think it' a good idea to "accidentally" kill "friendly" infiltrators
        // on the opposing side anyway, if you can get away with it. And you can, since by the current logic only obvious and direct betrayal uncovers someone's guise.
        // Well, and AOE, but that's due to cheese protection.

        return false;
    }

    internal static void ForceFeed(Actor_Unit actor, Actor_Unit targetPred, bool DisablecontrolBypass = true)
    {
        float r = (float)State.Rand.NextDouble();
        if (targetPred.Unit.Predator)
        {
            PreyLocation preyLocation = PreyLocation.stomach;
            var possibilities = new Dictionary<string, PreyLocation>();
            possibilities.Add("Maw", PreyLocation.stomach);

            if (targetPred.Unit.CanAnalVore && State.RaceSettings.GetVoreTypes(targetPred.Unit.Race).Contains(VoreType.Anal)) possibilities.Add("Anus", PreyLocation.anal);
            if (targetPred.Unit.CanBreastVore && State.RaceSettings.GetVoreTypes(targetPred.Unit.Race).Contains(VoreType.BreastVore)) possibilities.Add("Breast", PreyLocation.breasts);
            if (targetPred.Unit.CanCockVore && State.RaceSettings.GetVoreTypes(targetPred.Unit.Race).Contains(VoreType.CockVore)) possibilities.Add("Cock", PreyLocation.balls);
            if (targetPred.Unit.CanUnbirth && State.RaceSettings.GetVoreTypes(targetPred.Unit.Race).Contains(VoreType.Unbirth)) possibilities.Add("Pussy", PreyLocation.womb);

            if (State.GameManager.TacticalMode.IsPlayerInControl && State.GameManager.CurrentScene == State.GameManager.TacticalMode && possibilities.Count > 1 && DisablecontrolBypass)
            {
                var box = State.GameManager.CreateOptionsBox();
                box.SetData($"Which way do you want to enter?", "Maw", () => targetPred.PredatorComponent.ForceConsume(actor, preyLocation), possibilities.Keys.ElementAtOrDefault(1), () => targetPred.PredatorComponent.ForceConsume(actor, possibilities.Values.ElementAtOrDefault(1)), possibilities.Keys.ElementAtOrDefault(2), () => targetPred.PredatorComponent.ForceConsume(actor, possibilities.Values.ElementAtOrDefault(2)), possibilities.Keys.ElementAtOrDefault(3), () => targetPred.PredatorComponent.ForceConsume(actor, possibilities.Values.ElementAtOrDefault(3)), possibilities.Keys.ElementAtOrDefault(4), () => targetPred.PredatorComponent.ForceConsume(actor, possibilities.Values.ElementAtOrDefault(4)));
                actor.Movement = 0;
            }
            else
            {
                preyLocation = possibilities.Values.ToList()[State.Rand.Next(possibilities.Count)];
                actor.Movement = 0;
                targetPred.PredatorComponent.ForceConsumeAuto(actor);
            }
        }
        else
        {
            switch (State.Rand.Next(6))//Credits to Cartography for additional logs
            {
                case 0:
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> couldn't force feed {LogUtilities.GPPHimself(actor.Unit)} to <b>{targetPred.Unit.Name}</b>.");
                    break;
                case 1:
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"As <b>{actor.Unit.Name}</b> attempts to pry open <b>{LogUtilities.ApostrophizeWithOrWithoutS(targetPred.Unit.Name)}</b> mouth, {LogUtilities.GPPHeIs(actor.Unit)} rather surprised to find that the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(targetPred.Unit))} mouth seems incapable of stretching far enough.");
                    break;
                case 2:
                    if (targetPred.Unit.HasDick)
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> strokes <b>{LogUtilities.ApostrophizeWithOrWithoutS(targetPred.Unit.Name)}</b> {PreyLocStrings.ToCockSyn()}, getting it nice and {LogUtilities.GetRandomStringFrom("hard", "erect")}, before attempting to pry open the tip to get inside, only to find that the tip barely opens wide enough to fit a blade of grass, let alone a whole {LogUtilities.GetRaceDescSingl(actor.Unit)}.");
                    else if (targetPred.Unit.HasVagina)
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> sticks {LogUtilities.GPPHis(actor.Unit)} hand up <b>{LogUtilities.ApostrophizeWithOrWithoutS(targetPred.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}, getting it nice and wet, before attempting to push further in, only to find that {LogUtilities.GPPHeIsAbbr(actor.Unit)} already about as far in as {LogUtilities.GPPHe(actor.Unit)} can go, and that <b>{LogUtilities.ApostrophizeWithOrWithoutS(targetPred.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)} isn't budging any further.");
                    else
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> couldn't force feed {LogUtilities.GPPHimself(actor.Unit)} to <b>{targetPred.Unit.Name}</b>.");
                    break;
                case 3:
                    if (targetPred.Unit.HasVagina)
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> sticks {LogUtilities.GPPHis(actor.Unit)} hand up <b>{LogUtilities.ApostrophizeWithOrWithoutS(targetPred.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}, getting it nice and wet, before attempting to push further in, only to find that {LogUtilities.GPPHeIsAbbr(actor.Unit)} already about as far in as {LogUtilities.GPPHe(actor.Unit)} can go.");
                    else if (targetPred.Unit.HasDick)
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> strokes <b>{LogUtilities.ApostrophizeWithOrWithoutS(targetPred.Unit.Name)}</b> {PreyLocStrings.ToCockSyn()}, getting it nice and {LogUtilities.GetRandomStringFrom("hard", "erect")}, before attempting to pry open the tip to get inside, only to find that the tip barely opens wide enough to fit a blade of grass, let alone a whole {LogUtilities.GetRaceDescSingl(actor.Unit)}.");
                    else
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"As <b>{actor.Unit.Name}</b> attempts to pry open <b>{LogUtilities.ApostrophizeWithOrWithoutS(targetPred.Unit.Name)}</b> mouth, {LogUtilities.GPPHeIs(actor.Unit)} rather surprised to find that <b>{LogUtilities.ApostrophizeWithOrWithoutS(targetPred.Unit.Name)}</b> mouth seems incapable of stretching far enough.");
                    break;
                case 4:
                    if (Config.AnalVore && LogUtilities.ActorHumanoid(targetPred.Unit))
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> attempts to stick {LogUtilities.GPPHis(actor.Unit)} head up <b>{LogUtilities.ApostrophizeWithOrWithoutS(targetPred.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.anal)}, only to find that it refuses to open anywhere near large enough for that. <b>{targetPred.Unit.Name}</b> kindly asks the {LogUtilities.GetRaceDescSingl(actor.Unit)} to stop.");
                    else if (Config.AnalVore)
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> attempts to stick {LogUtilities.GPPHis(actor.Unit)} head up <b>{LogUtilities.ApostrophizeWithOrWithoutS(targetPred.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.anal)}, only to find that it refuses to open anywhere near large enough for that.");
                    else
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{LogUtilities.ApostrophizeWithOrWithoutS(actor.Unit.Name)}</b> attempt to force-feed {LogUtilities.GPPHimself(actor.Unit)} into <b>{targetPred.Unit.Name}</b> has been thwarted by the refusal of any holes on the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(targetPred.Unit))} body to open anywhere near wide enough.");
                    break;
                case 5:
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{LogUtilities.ApostrophizeWithOrWithoutS(actor.Unit.Name)}</b> attempt to force-feed {LogUtilities.GPPHimself(actor.Unit)} into <b>{targetPred.Unit.Name}</b> has been thwarted by the refusal of any holes on the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(targetPred.Unit))} body to open anywhere near wide enough.");
                    break;
                default:
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> couldn't force feed {LogUtilities.GPPHimself(actor.Unit)} to <b>{targetPred.Unit.Name}</b>.");
                    break;
            }
            actor.Movement = 0;
        }
    }
    internal static void AssumeForm(Actor_Unit actor, Actor_Unit target)
    {
        actor.ChangeRacePrey();
    }
    internal static void RevertForm(Actor_Unit actor, Actor_Unit target)
    {
        actor.RevertRace();
    }

    internal static void ShapeshifterPanel(Actor_Unit selectedUnit)
    {
        //int children = UnitPickerUI.ActorFolder.transform.childCount;
        //for (int i = children - 1; i >= 0; i--)
        //{
        //    UnityEngine.Object.Destroy(UnitPickerUI.ActorFolder.transform.GetChild(i).gameObject);
        //}
        //foreach (Unit shape in selectedUnit.Unit.ShifterShapes)
        //{
        //    GameObject obj = UnityEngine.Object.Instantiate(UnitPickerUI.HiringUnitPanel, UnitPickerUI.ActorFolder);
        //    UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
        //    Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), shape);
        //    sprite.UpdateSprites(actor);
        //    Text text = obj.transform.GetChild(3).GetComponent<Text>();
        //    text.text = 
        //        $"Items: {shape.GetItem(0)?.Name} {shape.GetItem(1)?.Name}" + (shape.HasTrait(Traits.Resourceful) ? $" { shape.GetItem(2)?.Name}" : "") + "\n" +
        //        $"Str: {shape.GetStatBase(Stat.Strength)} Dex: {shape.GetStatBase(Stat.Dexterity)} Agility: {shape.GetStatBase(Stat.Agility)}\n" +
        //        $"Mind: {shape.GetStatBase(Stat.Mind)} Will: {shape.GetStatBase(Stat.Will)} Endurance: {shape.GetStatBase(Stat.Endurance)}\n";
        //    if (shape.Predator)
        //        text.text += $"Vore: {shape.GetStatBase(Stat.Voracity)} Stomach: {shape.GetStatBase(Stat.Stomach)}";
        //    sprite.Name.text = InfoPanel.RaceSingular(shape);
        //    Button button = obj.GetComponentInChildren<Button>();
        //    button.GetComponentInChildren<Text>().text = "Transform";
        //    button.onClick.AddListener(() => selectedUnit.Shapeshift(shape));
        //    button.onClick.AddListener(() => UnitPickerUI.gameObject.SetActive(false));
        //}
        //UnitPickerUI.ActorFolder.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * (1 + (selectedUnit.Unit.ShifterShapes.Count / 3)));
        //UnitPickerUI.GetComponentInChildren<HirePanel>().GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Cancel";
        //UnitPickerUI.gameObject.SetActive(true);
    }

    internal static bool IsPreyEndoTargetForUnit(Prey preyUnit, Unit unit)
    {
        return (unit.HasTrait(Traits.FriendlyStomach) && (preyUnit.Unit.FixedSide == unit.GetApparentSide(preyUnit.Unit))) || unit.HasTrait(Traits.Endosoma) && preyUnit.Unit.IsDead == false;
    }
}

