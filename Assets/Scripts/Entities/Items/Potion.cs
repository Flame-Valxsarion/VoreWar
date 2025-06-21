using OdinSerializer;
using System;
using UnityEngine;

public class Potion : Item
{
    [OdinSerialize]
    internal Action<object, object> PotionFunction; //Target, Unit; This only applies if the potion hits the target
    [OdinSerialize]
    internal Action<object, object> TileFunction; // Applies to the tile under the target regardless of if the potion hits or not
    [OdinSerialize]
    internal int Tier { get; private set; }
    [OdinSerialize]
    internal bool NegativeEffect;

    public Potion(string name, string description, int cost, int tier, bool negative, Action<object, object> func, Action<object, object> tileFunc = null)
    {
        Name = name;
        Description = description;
        Cost = cost;
        Tier = tier;
        NegativeEffect = negative;
        PotionFunction = func;
        TileFunction = tileFunc;
    }

    public bool ActivatePotion(Actor_Unit user, Actor_Unit target)
    {
        int type = (int)State.World.ItemRepository.GetItemType(this);
        if (user.Movement <= 0 || user.Unit.EquippedPotions[type][0] <= 0) return false;
        user.Unit.EquippedPotions[type][0] = user.Unit.EquippedPotions[type][0] - 1;
        Debug.Log(user.Unit.EquippedPotions[type][0]);
        if (TileFunction != null)
        {
            TileFunction.Invoke(target.Position, user);
        }
        if (target.Unit.IsEnemyOfSide(user.Unit.Side))
        {
            if (target.GetAttackChance(user, true) <= State.Rand.NextDouble())
            {
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{user.Unit.Name}</b> threw a {Name} at <b>{target.Unit.Name}</b>, but missed.");
                return false;
            }
        }
        if (target == user)
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{target.Unit.Name}</b> used a {Name}.");
        else
        {
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{user.Unit.Name}</b> threw a {Name} at <b>{target.Unit.Name}</b>.");
            TacticalGraphicalEffects.CreatePotion(user.Position, target.Position, target);
        }
        PotionFunction.Invoke(target, user);
        if (user.Unit.TraitBoosts.PotionAttacks > 1)
        {
            int movementFraction = 1 + user.MaxMovement() / user.Unit.TraitBoosts.PotionAttacks;
            if (user.Movement > movementFraction)
                user.Movement -= movementFraction;
            else
                user.Movement = 0;
        }
        else
            user.Movement = 0;
        return true;
    }
}
