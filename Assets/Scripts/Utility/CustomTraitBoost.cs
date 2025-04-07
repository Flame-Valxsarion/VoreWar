using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class CustomTraitBoost
{
    public int id = -1;
    public string name;
    public string description;
    public List<string> tags;
    public TraitTier tier;
    public Dictionary<CustomTraitComp,float> comps;

    internal Booster ToBooster()
    {
        Booster customBoost = new Booster(description,
            (s) =>
            {
                foreach (var item in comps)
                {
                    float value = comps[item.Key];
                    switch (item.Key)
                    {
                        case CustomTraitComp.ExpRequired:
                            s.ExpRequired *= value;
                            break;
                        case CustomTraitComp.ExpGain:
                            s.ExpGain *= value;
                            break;
                        case CustomTraitComp.ExpGainFromVore:
                            s.ExpGainFromVore *= value;
                            break;
                        case CustomTraitComp.ExpGainFromAbsorption:
                            s.ExpGainFromAbsorption *= value;
                            break;
                        case CustomTraitComp.PassiveHeal:
                            s.PassiveHeal *= value;
                            break;
                        case CustomTraitComp.CapacityMult:
                            s.CapacityMult *= value;
                            break;
                        case CustomTraitComp.OutgoingChanceToEscape:
                            s.Outgoing.ChanceToEscape *= value;
                            break;
                        case CustomTraitComp.OutgoingMeleeDamage:
                            s.Outgoing.MeleeDamage *= value;
                            break;
                        case CustomTraitComp.OutgoingRangedDamage:
                            s.Outgoing.RangedDamage *= value;
                            break;
                        case CustomTraitComp.OutgoingMagicDamage:
                            s.Outgoing.MagicDamage *= value;
                            break;
                        case CustomTraitComp.OutgoingDigestionRate:
                            s.Outgoing.DigestionRate *= value;
                            break;
                        case CustomTraitComp.OutgoingAbsorptionRate:
                            s.Outgoing.AbsorptionRate *= value;
                            break;
                        case CustomTraitComp.OutgoingNutrition:
                            s.Outgoing.Nutrition *= value;
                            break;
                        case CustomTraitComp.OutgoingManaAbsorbHundreths:
                            s.Outgoing.ManaAbsorbHundreths *= (int)value;
                            break;
                        case CustomTraitComp.OutgoingMeleeShift:
                            s.Outgoing.MeleeShift *= value;
                            break;
                        case CustomTraitComp.OutgoingRangedShift:
                            s.Outgoing.RangedShift *= value;
                            break;
                        case CustomTraitComp.OutgoingMagicShift:
                            s.Outgoing.MagicShift *= value;
                            break;
                        case CustomTraitComp.OutgoingVoreOddsMult:
                            s.Outgoing.VoreOddsMult *= value;
                            break;
                        case CustomTraitComp.OutgoingGrowthRate:
                            s.Outgoing.GrowthRate *= value;
                            break;
                        case CustomTraitComp.OutgoingCritRateShift:
                            s.Outgoing.CritRateShift *= value;
                            break;
                        case CustomTraitComp.OutgoingCritDamageMult:
                            s.Outgoing.CritDamageMult *= value;
                            break;
                        case CustomTraitComp.OutgoingGrazeRateShift:
                            s.Outgoing.GrazeRateShift *= value;
                            break;
                        case CustomTraitComp.OutgoingGrazeDamageMult:
                            s.Outgoing.GrazeDamageMult *= value;
                            break;
                        case CustomTraitComp.IncomingChanceToEscape:
                            s.Incoming.ChanceToEscape *= value;
                            break;
                        case CustomTraitComp.IncomingMeleeDamage:
                            s.Incoming.MeleeDamage *= value;
                            break;
                        case CustomTraitComp.IncomingRangedDamage:
                            s.Incoming.RangedDamage *= value;
                            break;
                        case CustomTraitComp.IncomingMagicDamage:
                            s.Incoming.MagicDamage *= value;
                            break;
                        case CustomTraitComp.IncomingDigestionRate:
                            s.Incoming.DigestionRate *= value;
                            break;
                        case CustomTraitComp.IncomingAbsorptionRate:
                            s.Incoming.AbsorptionRate *= value;
                            break;
                        case CustomTraitComp.IncomingNutrition:
                            s.Incoming.Nutrition *= value;
                            break;
                        case CustomTraitComp.IncomingManaAbsorbHundreths:
                            s.Incoming.ManaAbsorbHundreths *= (int)value;
                            break;
                        case CustomTraitComp.IncomingMeleeShift:
                            s.Incoming.MeleeShift *= value;
                            break;
                        case CustomTraitComp.IncomingRangedShift:
                            s.Incoming.RangedShift *= value;
                            break;
                        case CustomTraitComp.IncomingMagicShift:
                            s.Incoming.MagicShift *= value;
                            break;
                        case CustomTraitComp.IncomingVoreOddsMult:
                            s.Incoming.VoreOddsMult *= value;
                            break;
                        case CustomTraitComp.IncomingGrowthRate:
                            s.Incoming.GrowthRate *= value;
                            break;
                        case CustomTraitComp.IncomingCritRateShift:
                            s.Incoming.CritRateShift *= value;
                            break;
                        case CustomTraitComp.IncomingCritDamageMult:
                            s.Incoming.CritDamageMult *= value;
                            break;
                        case CustomTraitComp.IncomingGrazeRateShift:
                            s.Incoming.GrazeRateShift *= value;
                            break;
                        case CustomTraitComp.IncomingGrazeDamageMult:
                            s.Incoming.GrazeDamageMult *= value;
                            break;
                        case CustomTraitComp.FlatHitReduction:
                            s.FlatHitReduction *= value;
                            break;
                        case CustomTraitComp.SpeedLossFromWeightMultiplier:
                            s.SpeedLossFromWeightMultiplier *= value;
                            break;
                        case CustomTraitComp.DodgeLossFromWeightMultiplier:
                            s.DodgeLossFromWeightMultiplier *= value;
                            break;
                        case CustomTraitComp.BulkMultiplier:
                            s.BulkMultiplier *= value;
                            break;
                        case CustomTraitComp.SpeedMultiplier:
                            s.SpeedMultiplier *= value;
                            break;
                        case CustomTraitComp.MinSpeed:
                            s.MinSpeed *= (int)value;
                            break;
                        case CustomTraitComp.SpeedBonus:
                            s.SpeedBonus *= (int)value;
                            break;
                        case CustomTraitComp.MeleeAttacks:
                            s.MeleeAttacks *= (int)value;
                            break;
                        case CustomTraitComp.RangedAttacks:
                            s.RangedAttacks *= (int)value;
                            break;
                        case CustomTraitComp.VoreAttacks:
                            s.VoreAttacks *= (int)value;
                            break;
                        case CustomTraitComp.SpellAttacks:
                            s.SpellAttacks *= (int)value;
                            break;
                        case CustomTraitComp.HealthMultiplier:
                            s.HealthMultiplier *= value;
                            break;
                        case CustomTraitComp.ManaMultiplier:
                            s.ManaMultiplier *= value;
                            break;
                        case CustomTraitComp.StaminaMultiplier:
                            s.StaminaMultiplier *= value;
                            break;
                        case CustomTraitComp.TurnCanFlee:
                            s.DigestionImmunityTurns *= (int)value;
                            break;
                        case CustomTraitComp.DigestionImmunityTurns:
                            s.DigestionImmunityTurns *= (int)value;
                            break;
                        case CustomTraitComp.HealthRegen:
                            s.HealthRegen *= (int)value;
                            break;
                        case CustomTraitComp.ManaRegen:
                            s.ManaRegen *= (int)value;
                            break;
                        case CustomTraitComp.OnLevelUpBonusToAllStats:
                            s.OnLevelUpBonusToAllStats *= (int)value;
                            break;
                        case CustomTraitComp.OnLevelUpBonusToGiveToTwoRandomStats:
                            s.OnLevelUpBonusToGiveToTwoRandomStats *= (int)value;
                            break;
                        case CustomTraitComp.OnLevelUpAllowAnyStat:
                            s.OnLevelUpAllowAnyStat = value >= 1 ? true : false;
                            break;
                        case CustomTraitComp.Scale:
                            s.Scale *= value;
                            break;
                        case CustomTraitComp.StatMult:
                            s.StatMult *= value;
                            break;
                        case CustomTraitComp.StrengthMult:
                            s.StrengthMult *= value;
                            break;
                        case CustomTraitComp.DexterityMult:
                            s.DexterityMult *= value;
                            break;
                        case CustomTraitComp.VoracityMult:
                            s.VoracityMult *= value;
                            break;
                        case CustomTraitComp.AgilityMult:
                            s.AgilityMult *= value;
                            break;
                        case CustomTraitComp.WillMult:
                            s.WillMult *= value;
                            break;
                        case CustomTraitComp.MindMult:
                            s.MindMult *= value;
                            break;
                        case CustomTraitComp.EnduranceMult:
                            s.EnduranceMult *= value;
                            break;
                        case CustomTraitComp.StomachMult:
                            s.StomachMult *= value;
                            break;
                        case CustomTraitComp.VirtualDexMult:
                            s.VirtualDexMult *= value;
                            break;
                        case CustomTraitComp.VirtualStrMult:
                            s.VirtualStrMult *= value;
                            break;
                        case CustomTraitComp.FireDamageTaken:
                            s.FireDamageTaken *= value;
                            break;
                        case CustomTraitComp.GrowthDecayRate:
                            s.GrowthDecayRate *= value;
                            break;
                        case CustomTraitComp.SightRangeBoost:
                            s.SightRangeBoost += (int)value;
                            break;
                        case CustomTraitComp.DeployCostMult:
                            s.DeployCostMult *= value;
                            break;
                        case CustomTraitComp.UpkeepMult:
                            s.UpkeepMult *= value;
                            break;
                        default:
                            break;
                    }
                }
            });
        return customBoost;
    }
}