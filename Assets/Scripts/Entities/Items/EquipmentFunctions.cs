public static class EquipmentFunctions
{
    public static void CheckEquipment(Unit unit, EquipmentActivator activator, object[] args)
    {
        for (int i = 0; i < unit.Items.Length; i++)
        {
            Item item = unit.Items[i];
            if (item is Equipment)
            {
                Equipment equipment = item as Equipment;
                if (equipment.EquipmentFunction.ContainsKey(activator))
                {
                    equipment.InvokeFunction(activator, args, unit, i);
                }
            }
        }
    }

    public static void TickCoolDown(Unit unit, EquipmentType type, bool reset = false)
    {
        for (int i = 0; i < unit.Items.Length; i++)
        {
            Item item = unit.Items[i];
            if (item is Equipment)
            {
                Equipment equipment = item as Equipment;
                if (equipment.useType >= type && equipment.useType < type + 10)
                {
                    if (unit.ItemCooldowns[i] > 0)
                    {
                        unit.ItemCooldowns[i] = reset ? 0 : unit.ItemCooldowns[i] - 1;

                        if (unit.ItemCooldowns[i] <= 0)
                            unit.ItemCooldowns[i] = 0;

                        if (unit.ItemCooldowns[i] <= 0)
                        {
                            unit.ItemUses[i] = equipment.ItemUses;
                        }
                    }
                }
            }
        }
    }

    public static int GetEquipmentSlot(Unit unit, ItemType type)
    {
        for (int i = 0; i < unit.Items.Length; i++)
        {
            if (unit.Items[i] == null)
                continue;
            if (State.World.ItemRepository.GetItemType(unit.Items[i]) == type)
                return i;
        }
        return -1;
    }

    internal static bool UseEquipmentHealthPotion(Unit unit)
    {
        if (unit.HealthPct <= 0.5f)
        {
            unit.Heal(20);
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{unit.Name}</b> drank a health potion, healing <color=green>20</color> HP.");
            return true;
        }
        return false;
    }   
    internal static bool UseEquipmentManaPotion(Unit unit)
    {
        if (unit.ManaPct <= 0.5f)
        {
            unit.RestoreMana(20);
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{unit.Name}</b> drank a mana potion, restoring <color=blue>20</color> mana.");
            return true;
        }
        return false;

    }
    internal static bool UseEquipmentBarrierRing(Unit unit)
    {
        if (unit.Barrier <= 8)
        {
            unit.RestoreBarrier(2);
            return true;
        }
        else if (unit.Barrier <= 9)
        {
            unit.RestoreBarrier(1);
            return true;
        }
        return false;
    }
    internal static bool UseEquipmentBrambleBand(Unit unit, Actor_Unit attacker)
    {
        int damage = unit.GetStat(Stat.Endurance) / 10;
        attacker.Damage(damage, false, false);
        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{attacker.Unit.Name}</b> took <color=red>{damage}</color> damage from {unit.Name}'s Bramble Band.");
        return true;

    }
    internal static bool UseEquipmentWarpStone(Unit unit)
    {
        if (unit.HealthPct <= 0.5f && unit.GetStatusEffect(StatusEffectType.Warping) == null)
        {
            unit.ApplyStatusEffect(StatusEffectType.Warping, 2, 2);
            return true;
        }
        return false;
    }
    internal static bool UseEquipmentGoddessPendantStart(Army army)
    {
        if (army == null)
        {
            return false;
        }
        foreach (var unit in army.Units)
        {
            unit.AddBolster(10);
        }
        return true;
    }
}
