using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using static UnityEngine.UI.CanvasScaler;

public enum EquipmentActivator
{
    OnTacticalBattleStart, // ActorUnit, Army, null
    OnTacticalBattleEnd, // ActorUnit, Army, null
    OnTacticalTurnStart, // ActorUnit, army, null
    OnStrategicTurnStart, // Unit, army, null
    OnMeleeAttack, // ActorUnit, target, damage
    OnMeleeHit, // ActorUnit, target, damage
    OnMeleeMiss, // ActorUnit, target, damage
    WhenMeleeAttacked, // ActorUnit, attacker, damage
    WhenMeleeHit, // ActorUnit, attacker, damage
    WhenMeleeMissed, // ActorUnit, attacker, damage
    OnRangedAttack, // ActorUnit, target, damage
    OnRangedHit, // ActorUnit, target, damage
    OnRangedMiss, // ActorUnit, target, damage
    WhenRangedAttacked, // ActorUnit, attacker, damage
    WhenRangedHit, // ActorUnit, attacker, damage
    WhenRangedMissed, // ActorUnit, attacker, damage
    OnSpellCast, // ActorUnit, Position, damage
    WhenTargetedBySpellDamage, // ActorUnit, attacker, damage
    WhenMissedBySpellDamage,  // ActorUnit, attacker, damage
    WhenHitBySpellDamage,  // ActorUnit, attacker, damage
    WhenTargetedBySpellStatus,  // ActorUnit, attacker, spell
    WhenMissedBySpellStatus, // ActorUnit, attacker, spell
    WhenHitBySpellStatus, // ActorUnit, attacker, spell
    OnHeal, // Unit, amount, null
    OnDamage, // ActorUnit, amount, null
}
public enum EquipmentType
{
    Normal, // No effect, item can be used any amount of times with no cooldown
    Uses, // item has no cooldown, but will be removed after uses specified by ItemUses

    //Tactical recharging
    RechargeTactical = 10, // Item refreshes uses after defined tactical turns, cooldown begins after first use
    RestockTactical, // Item refreshes uses after defined tactical turns, cooldown begins after last use
    RestockTacticalBattle, // Item refreshes uses at the start of a tactical battle

    //Strategic recharging
    RechargeStrategy = 20, // Item refreshes uses after defined strategic turns, cooldown begins after first use
    RestockStrategy, // Item refreshes uses after defined strategic turns, cooldown begins after last use
}

public class Equipment : Item
{
    [OdinSerialize]
    internal Dictionary<EquipmentActivator, Func<object, object, object, bool>> EquipmentFunction;
    [OdinSerialize]
    internal int Tier { get; private set; }
    [OdinSerialize]
    internal EquipmentType useType;
    [OdinSerialize]
    internal int ItemUses;
    [OdinSerialize]
    internal int ItemCooldown;
    [OdinSerialize]
    internal bool[] TriggersCooldown; // used for multi-function equipment, not required. Count should match EquipmentFunction, a value of true triggers uses and/or cooldowns, a value of false ignores uses / cooldowns, 
    public Equipment(string name, string description, int cost, int tier, Dictionary<EquipmentActivator, Func<object, object, object, bool>> func, EquipmentType type = EquipmentType.Normal, int uses = 1, int itemCD = 0, bool[] triggersCD = null)
    {
        Name = name;
        Description = description;
        Cost = cost;
        Tier = tier;
        EquipmentFunction = func;
        useType = type;
        ItemUses = uses;
        ItemCooldown = itemCD + 1;
        TriggersCooldown = triggersCD;
    }

    internal void InvokeFunction(EquipmentActivator activator, object[] args, Unit unit, int slot)
    {
        if (unit.ItemUses[slot] > 0)
        {
            if (EquipmentFunction[activator].Invoke(args[0], args[1], args[2]))
            {
                if (useType != EquipmentType.Normal)
                {
                    if (TriggersCooldown != null)
                        if (TriggersCooldown.Count() == EquipmentFunction.Count())
                            if (!TriggersCooldown[EquipmentFunction.Keys.ToList().IndexOf(activator)])
                                return;

                    unit.ItemUses[slot] = unit.ItemUses[slot] - 1;
                    if (useType == EquipmentType.RechargeTactical || useType == EquipmentType.RechargeStrategy)
                    {
                        if (unit.ItemCooldowns[slot] <= 0)
                        {
                            unit.ItemCooldowns[slot] = ItemCooldown;
                        }
                    }
                    if (useType == EquipmentType.RestockTactical || useType == EquipmentType.RestockStrategy)
                    {
                        if (unit.ItemCooldowns[slot] <= 0 && unit.ItemUses[slot] <= 0)
                        {
                            unit.ItemCooldowns[slot] = ItemCooldown;
                        }
                    }
                    if (useType == EquipmentType.Uses)
                    {
                        if (unit.ItemUses[slot] <= 0)
                        {
                            unit.SetItem(null, slot);
                        }
                    }
                }
            }

        }
        else
        {
            if (useType != EquipmentType.Normal)
                if (TriggersCooldown != null)
                    if (TriggersCooldown.Count() == EquipmentFunction.Count())
                        if (!TriggersCooldown[EquipmentFunction.Keys.ToList().IndexOf(activator)])
                            EquipmentFunction[activator].Invoke(args[0], args[1], args[2]);
        }
    }

    public string DetailedDescription()
    {
        return "";
    }

}
