using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;

public enum PotionIngredient
{
    //Standard Rolls
    Grievous,
    Dangerous,
    Experimental,
    Unstable,
    Stable,
    Simple, 
    Standard,
    Premium,
    Superior,

    //Special Rolls - Add all below Powerful
    Powerful, // Improved odds for strong trait
    Legendary, // Greatly improved odds for strong trait
    Sterilizing, // Removes a negative triat
    Purifying, // Removes all negative and harmful effects
    Solute, // Improves chance of effects becoming traits
    Solvent, // Improves chance of effects becoming stats
    Coagulate, // Restores one roll
    PotionIngredientCounter,
}


public class LaboratoryPotion
{
    [OdinSerialize]
    public List<Traits> PositiveTraits;
    [OdinSerialize]
    public List<Traits> NegativeTraits;
    [OdinSerialize]
    public Dictionary<Stat, int> StatModifiers;

    public LaboratoryPotion()
    {
        PositiveTraits = new List<Traits>();
        NegativeTraits = new List<Traits>();
        StatModifiers = new Dictionary<Stat, int>();
        StatModifiers.Add(Stat.Strength,0);
        StatModifiers.Add(Stat.Dexterity, 0);
        StatModifiers.Add(Stat.Endurance, 0);
        StatModifiers.Add(Stat.Mind, 0);
        StatModifiers.Add(Stat.Will, 0);
        StatModifiers.Add(Stat.Agility, 0);
        StatModifiers.Add(Stat.Voracity, 0);
        StatModifiers.Add(Stat.Stomach, 0);
    }

    internal void AddPositiveTrait(Traits trait)
    {
        if (!PositiveTraits.Contains(trait))
        {
            PositiveTraits.Add(trait);
        }
    }
    internal void AddNegativeTrait(Traits trait)
    {
        if (!NegativeTraits.Contains(trait))
        {
            NegativeTraits.Add(trait);
        }
    }
    internal void AddStatMod(Stat stat, int mod)
    {
        if (StatModifiers.Keys.Contains(stat))
            StatModifiers[stat] = StatModifiers[stat] + mod;
        else
            StatModifiers.Add(stat, mod);

    }
}

