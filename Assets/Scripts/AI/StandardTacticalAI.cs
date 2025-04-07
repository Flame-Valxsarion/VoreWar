using System.Collections.Generic;

public class StandardTacticalAI : TacticalAI
{
    public StandardTacticalAI(List<Actor_Unit> actors, TacticalTileType[,] tiles, int AISide, bool defendingVillage = false) : base(actors, tiles, AISide, defendingVillage)
    {

    }

    protected override void GetNewOrder(Actor_Unit actor)
    {
        foundPath = false;
        didAction = false; // Very important fix: surrounded retreaters sometimes just skipped doing attacks because this was never set to false in or before "fightwithoutmoving"

        path = null;
        if (retreating && actor.Unit.Type != UnitType.Summon && actor.Unit.Type != UnitType.SpecialMercenary && actor.Unit.HasTrait(Traits.Fearless) == false && TacticalUtilities.GetMindControlSide(actor.Unit) == -1 && (TacticalUtilities.GetPreferredSide(actor.Unit, AISide, enemySide) == AISide || onlyForeignTroopsLeft))
        {
            int retreatY;
            if (State.GameManager.TacticalMode.IsDefender(actor) == false)
                retreatY = Config.TacticalSizeY - 1;
            else
                retreatY = 0;
            if (actor.Position.y == retreatY)
            {
                State.GameManager.TacticalMode.AttemptRetreat(actor, true);
                FightWithoutMoving(actor);
                actor.Movement = 0;
                return;
            }
            WalkToYBand(actor, retreatY);
            if (path == null || path.Path.Count == 0)
            {
                FightWithoutMoving(actor);
                actor.Movement = 0;
            }

            return;
        }
        //do action


        if (actor.Unit.HasTrait(Traits.Pounce) && actor.Movement >= 2)
        {
            RunVorePounce(actor);
            if (path != null)
                return;
            if (didAction) return;

        }

        RunPred(actor);
        if (didAction || foundPath)
            return;

        TryResurrect(actor);
        TryReanimate(actor);

        RunBind(actor);

        if (State.Rand.Next(2) == 0 || actor.Unit.HasWeapon == false)
            RunSpells(actor);
        if (path != null)
            return;
        if (!actor.Unit.HasTrait(Traits.VoreObsession))
        {
            if (actor.Unit.HasTrait(Traits.Pounce) && actor.Movement >= 2)
            {
                if (IsRanged(actor) == false)
                {
                    RunMeleePounce(actor);
                    if (didAction) return;
                }
            }
            if (foundPath || didAction) return;
            if (IsRanged(actor))
                RunRanged(actor);
            else
                RunMelee(actor);
        }
        if (foundPath || didAction) return;
        //Search for surrendered targets outside of vore range
        //If no path to any targets, will sit out its turn
        RunPred(actor, true);
        if (foundPath || didAction) return;
        actor.ClearMovement();
    }
}
