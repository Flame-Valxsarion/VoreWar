using System.Linq;

public class ConditionalTraitConditionChecker
{
    public static bool TacticalTraitConditionActive(Actor_Unit unit, ConditionalTraitContainer conditionalTrait)
    {
        bool condition = false;
        TraitConditionLogicalOperator op = TraitConditionLogicalOperator.none;
        foreach (var item in conditionalTrait.OperationBlocks)
        {
            switch (op)
            {
                case TraitConditionLogicalOperator.and:
                    condition = condition && TacticalCheckConditionBlocks(unit, item.Key);
                    break;
                case TraitConditionLogicalOperator.or:
                    condition = condition || TacticalCheckConditionBlocks(unit, item.Key);
                    break;
                case TraitConditionLogicalOperator.not:
                    condition = condition && !TacticalCheckConditionBlocks(unit, item.Key);
                    break;
                default:
                    condition = TacticalCheckConditionBlocks(unit, item.Key);
                    break;
            }            
            op = item.Value;
            if (op == TraitConditionLogicalOperator.none)
            {
                return condition;
            }
        }
        return condition;
    }

    public static bool TacticalCheckConditionBlocks(Actor_Unit unit, ConditionalTraitOperationBlock opBlock)
    {
        TraitCondition leadCondition = opBlock.conditionVariable.First();
        if (leadCondition >= TraitCondition.Male)
        {
            switch (leadCondition)
            {
                case TraitCondition.Male:
                    if (unit.Unit.GetGender() == Gender.Male || unit.Unit.GetGender() == Gender.Gynomorph)
                        return true;
                    break;
                case TraitCondition.Female:
                    if (unit.Unit.GetGender() == Gender.Female || unit.Unit.GetGender() == Gender.Andromorph)
                        return true;
                    break;
                case TraitCondition.NonBinary:
                    if (unit.Unit.GetGender() == Gender.Male || unit.Unit.GetGender() == Gender.Gynomorph || unit.Unit.GetGender() == Gender.Female || unit.Unit.GetGender() == Gender.Andromorph)
                        return false;
                    return true;
                case TraitCondition.Predator:
                    if (unit.PredatorComponent != null)
                    {
                        if (unit.PredatorComponent.PreyCount >= 1)
                            return true;
                    }
                    break;
                case TraitCondition.Digesting:
                    if (unit.PredatorComponent != null)
                    {
                        if (unit.PredatorComponent.AlivePrey >= 1)
                            return true;
                    }
                    break;
                case TraitCondition.Absorbing:
                    if (unit.PredatorComponent != null)
                    {
                        if (unit.PredatorComponent.PreyCount > unit.PredatorComponent.AlivePrey)
                            return true;
                    }
                    break;
                case TraitCondition.Prey:
                    if (unit.SelfPrey != null)
                        return true;
                    break;
                default:
                    break;
            }
            return false;
        }

        float total = 0;
        int counter = 0;
        foreach (var cond in opBlock.conditionVariable)
        {
            float value = 0;
            switch (cond)
            {
                case TraitCondition.Level:
                    value = unit.Unit.Level;
                    break;
                case TraitCondition.Health:
                    value = unit.Unit.Health;
                    break;
                case TraitCondition.HealthPercent:
                    value = unit.Unit.HealthPct;
                    break;
                case TraitCondition.Mana:
                    value = unit.Unit.Mana;
                    break;
                case TraitCondition.ManaPercent:
                    value = unit.Unit.ManaPct;
                    break;
                case TraitCondition.Strength:
                    value = unit.Unit.GetStat(Stat.Strength);
                    break;
                case TraitCondition.Dexterity:
                    value = unit.Unit.GetStat(Stat.Dexterity);
                    break;
                case TraitCondition.Endurance:
                    value = unit.Unit.GetStat(Stat.Endurance);
                    break;
                case TraitCondition.Mind:
                    value = unit.Unit.GetStat(Stat.Mind);
                    break;
                case TraitCondition.Will:
                    value = unit.Unit.GetStat(Stat.Will);
                    break;
                case TraitCondition.Agility:
                    value = unit.Unit.GetStat(Stat.Agility);
                    break;
                case TraitCondition.Voracity:
                    value = unit.Unit.GetStat(Stat.Voracity);
                    break;
                case TraitCondition.Stomach:
                    value = unit.Unit.GetStat(Stat.Stomach);
                    break;
                case TraitCondition.StatTotal:
                    value = unit.Unit.GetStatTotal();
                    break;
                case TraitCondition.TotalDigestions:
                    value = unit.Unit.DigestedUnits;
                    break;
                case TraitCondition.TotalKills:
                    value = unit.Unit.KilledUnits;
                    break;
                case TraitCondition.TimesKilled:
                    value = unit.Unit.TimesKilled;
                    break;
                case TraitCondition.TacticalTurn:
                    value = State.GameManager.TacticalMode.currentTurn;
                    break;
                case TraitCondition.StrategicTurn:
                    value = State.World.Turn;
                    break;
                case TraitCondition.AliveAllies:
                    value = State.GameManager.TacticalMode.ExposeRemainingUnits(unit.Unit.GetApparentSide() == State.GameManager.TacticalMode.GetDefenderSide());
                    break;
                case TraitCondition.AliveEnemies:
                    value = State.GameManager.TacticalMode.ExposeRemainingUnits(!(unit.Unit.GetApparentSide() == State.GameManager.TacticalMode.GetDefenderSide()));
                    break;
                case TraitCondition.Growth:
                    value = (float)unit.Unit.BaseScale;
                    break;
                case TraitCondition.Fullness:
                    if (unit.PredatorComponent != null)
                        value = unit.PredatorComponent.Fullness;
                    else
                        value = 0;
                    break;
                default:
                    break;
            }

            if (counter <= 0)
            {
                counter++;
                total += value;
                continue;
            }

            switch (opBlock.arithmeticOperator[counter-1])
            {
                case TraitConditionArithmeticOperator.none:
                    break;
                case TraitConditionArithmeticOperator.add:
                    total += value;
                    break;
                case TraitConditionArithmeticOperator.sub:
                    total -= value;
                    break;
                case TraitConditionArithmeticOperator.mul:
                    total *= value;
                    break;
                case TraitConditionArithmeticOperator.div:
                    total /= value;
                    break;
                default:
                    break;
            }
        }

        switch (opBlock.compareOp)
        {
            case TraitConditionCompareOperator.eq:
                return total == opBlock.compareValue;
            case TraitConditionCompareOperator.geq:
                return total >= opBlock.compareValue;
            case TraitConditionCompareOperator.leq:
                return total <= opBlock.compareValue;
            case TraitConditionCompareOperator.none:
                break;
            default:
                return false;
        }
        return false;
    }

    public static bool StrategicTraitConditionActive(Unit unit, ConditionalTraitContainer conditionalTrait)
    {
        bool condition = false;
        TraitConditionLogicalOperator op = TraitConditionLogicalOperator.none;
        foreach (var item in conditionalTrait.OperationBlocks)
        {
            switch (op)
            {
                case TraitConditionLogicalOperator.and:
                    condition = condition && StrategicCheckConditionBlocks(unit, item.Key);
                    break;
                case TraitConditionLogicalOperator.or:
                    condition = condition || StrategicCheckConditionBlocks(unit, item.Key);
                    break;
                case TraitConditionLogicalOperator.not:
                    condition = condition && !StrategicCheckConditionBlocks(unit, item.Key);
                    break;
                default:
                    condition = StrategicCheckConditionBlocks(unit, item.Key);
                    break;
            }
            op = item.Value;
            if (op == TraitConditionLogicalOperator.none)
            {
                return condition;
            }
        }
        return condition;
    }

    public static bool StrategicCheckConditionBlocks(Unit unit, ConditionalTraitOperationBlock opBlock)
    {
        TraitCondition leadCondition = opBlock.conditionVariable.First();
        if (leadCondition >= TraitCondition.Male)
        {
            switch (leadCondition)
            {
                case TraitCondition.Male:
                    if (unit.GetGender() == Gender.Male || unit.GetGender() == Gender.Gynomorph)
                        return true;
                    break;
                case TraitCondition.Female:
                    if (unit.GetGender() == Gender.Female || unit.GetGender() == Gender.Andromorph)
                        return true;
                    break;
                case TraitCondition.NonBinary:
                    if (unit.GetGender() == Gender.Male || unit.GetGender() == Gender.Gynomorph || unit.GetGender() == Gender.Female || unit.GetGender() == Gender.Andromorph)
                        return false;
                    return true;
                case TraitCondition.Predator:
                    return false;
                case TraitCondition.Digesting:
                    return false;
                case TraitCondition.Absorbing:
                    return false;
                case TraitCondition.Prey:
                    return false;
                default:
                    break;
            }
            return false;
        }

        float total = 0;
        int counter = 0;
        foreach (var cond in opBlock.conditionVariable)
        {
            float value = 0;
            switch (cond)
            {
                case TraitCondition.Level:
                    value = unit.Level;
                    break;
                case TraitCondition.Health:
                    value = unit.Health;
                    break;
                case TraitCondition.HealthPercent:
                    value = unit.HealthPct;
                    break;
                case TraitCondition.Mana:
                    value = unit.Mana;
                    break;
                case TraitCondition.ManaPercent:
                    value = unit.ManaPct;
                    break;
                case TraitCondition.Strength:
                    value = unit.GetStat(Stat.Strength);
                    break;
                case TraitCondition.Dexterity:
                    value = unit.GetStat(Stat.Dexterity);
                    break;
                case TraitCondition.Endurance:
                    value = unit.GetStat(Stat.Endurance);
                    break;
                case TraitCondition.Mind:
                    value = unit.GetStat(Stat.Mind);
                    break;
                case TraitCondition.Will:
                    value = unit.GetStat(Stat.Will);
                    break;
                case TraitCondition.Agility:
                    value = unit.GetStat(Stat.Agility);
                    break;
                case TraitCondition.Voracity:
                    value = unit.GetStat(Stat.Voracity);
                    break;
                case TraitCondition.Stomach:
                    value = unit.GetStat(Stat.Stomach);
                    break;
                case TraitCondition.StatTotal:
                    value = unit.GetStatTotal();
                    break;
                case TraitCondition.TotalDigestions:
                    value = unit.DigestedUnits;
                    break;
                case TraitCondition.TotalKills:
                    value = unit.KilledUnits;
                    break;
                case TraitCondition.TimesKilled:
                    value = unit.TimesKilled;
                    break;
                case TraitCondition.TacticalTurn:
                    value = 0;
                    break;
                case TraitCondition.StrategicTurn:
                    value = State.World.Turn;
                    break;
                case TraitCondition.AliveAllies:
                    value = 0;
                    break;
                case TraitCondition.AliveEnemies:
                    value = 0;
                    break;
                case TraitCondition.Growth:
                    value = (float)unit.BaseScale;
                    break;
                case TraitCondition.Fullness:
                    value = 0;
                    break;
                default:
                    break;
            }

            if (counter <= 0)
            {
                counter++;
                total += value;
                continue;
            }

            switch (opBlock.arithmeticOperator[counter - 1])
            {
                case TraitConditionArithmeticOperator.none:
                    break;
                case TraitConditionArithmeticOperator.add:
                    total += value;
                    break;
                case TraitConditionArithmeticOperator.sub:
                    total -= value;
                    break;
                case TraitConditionArithmeticOperator.mul:
                    total *= value;
                    break;
                case TraitConditionArithmeticOperator.div:
                    total /= value;
                    break;
                default:
                    break;
            }
        }

        switch (opBlock.compareOp)
        {
            case TraitConditionCompareOperator.eq:
                return total == opBlock.compareValue;
            case TraitConditionCompareOperator.geq:
                return total >= opBlock.compareValue;
            case TraitConditionCompareOperator.leq:
                return total <= opBlock.compareValue;
            case TraitConditionCompareOperator.none:
                break;
            default:
                return false;
        }
        return false;
    }
}