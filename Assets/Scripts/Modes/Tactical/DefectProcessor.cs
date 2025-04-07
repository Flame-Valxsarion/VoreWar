using System.Collections.Generic;
using static UnityEngine.UI.CanvasScaler;



class DefectProcessor
{
    internal Army attacker;
    internal Army defender;
    internal Village village;

    internal int DefectedAttackers;
    internal int DefectedDefenders;
    internal int DefectedGarrison;

    internal int newAttackers;
    internal int newDefenders;
    //internal int newGarrison;

    internal List<Actor_Unit> extraDefenders;
    internal List<Actor_Unit> extraAttackers;

    public DefectProcessor(Army attacker, Army defender, Village village)
    {
        this.attacker = attacker;
        this.defender = defender;
        this.village = village;
        extraAttackers = new List<Actor_Unit>();
        extraDefenders = new List<Actor_Unit>();
    }

    internal void DefectReport()
    {
        if (DefectedAttackers == 0 && DefectedDefenders == 0 && DefectedGarrison == 0)
            return;
        string msg = DefectedAttackers > 0 ? $"{DefectedAttackers} attackers have defected to rejoin their race\n" : "";
        msg += (DefectedDefenders > 0 || DefectedGarrison > 0) ? $"{DefectedDefenders + DefectedGarrison} defenders have defected to rejoin their race\n" : "";
        State.GameManager.CreateMessageBox(msg);
    }

    internal void AttackerDefectCheck(Actor_Unit actor, Race otherRace)
    {
        if (actor.Unit.Race != otherRace || actor.Unit.ImmuneToDefections || actor.Unit.HasTrait(Traits.Camaraderie) || (actor.Unit.HasFixedSide() && !actor.Unit.HasTrait(Traits.Infiltrator)))
            return;
        if (actor.Unit.HasTrait(Traits.RaceLoyal) || State.Rand.NextDouble() < .15f - (.05f * (actor.Unit.GetStat(Stat.Will) - 10) / 10))
        {
            DefectedAttackers++;

            attacker.Units.Remove(actor.Unit);
            if (defender != null && StrategicUtilities.ArmyCanFitUnit(defender, actor.Unit))
            {
                actor.Unit.Side = defender.Side;
                defender.Units.Add(actor.Unit);
            }
            else
            {
                actor.Unit.Side = State.GameManager.TacticalMode.GetDefenderSide();
                newDefenders++;
                extraDefenders.Add(actor);
            }
        }
    }

    internal void DefenderDefectCheck(Actor_Unit actor, Race otherRace)
    {
        if (actor.Unit.Race != otherRace || actor.Unit.ImmuneToDefections || actor.Unit.HasTrait(Traits.Camaraderie) || (actor.Unit.HasFixedSide() && !actor.Unit.HasTrait(Traits.Infiltrator)))
            return;
        if (actor.Unit.HasTrait(Traits.RaceLoyal) || State.Rand.NextDouble() < .15f - (.05f * (actor.Unit.GetStat(Stat.Will) - 10) / 10))
        {
            actor.Unit.Side = attacker.Side;
            DefectedDefenders++;
            defender.Units.Remove(actor.Unit);
            if (StrategicUtilities.ArmyCanFitUnit(attacker, actor.Unit))
            {
                attacker.Units.Add(actor.Unit);
            }
            else
            {
                newAttackers++;
                extraAttackers.Add(actor);
            }
        }
    }

    internal void GarrisonDefectCheck(Actor_Unit actor, Race otherRace)
    {
        if (actor.Unit.Race != otherRace || actor.Unit.ImmuneToDefections || actor.Unit.HasTrait(Traits.Camaraderie) || (actor.Unit.HasFixedSide() && !actor.Unit.HasTrait(Traits.Infiltrator)))
            return;
        if (actor.Unit.HasTrait(Traits.RaceLoyal) || State.Rand.NextDouble() < (2 - village.Happiness / 66) * .15f - (.05f * (actor.Unit.GetStat(Stat.Will) - 10) / 10))
        {
            actor.Unit.Side = attacker.Side;
            village.GetRecruitables().Remove(actor.Unit);
            DefectedGarrison++;
            if (StrategicUtilities.ArmyCanFitUnit(attacker, actor.Unit))
            {
                attacker.Units.Add(actor.Unit);
            }
            else
            {
                newAttackers++;
                extraAttackers.Add(actor);
            }
        }
    }
}

