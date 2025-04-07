using CruxClothing;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CustomTraitComp
{
    ExpRequired,
    ExpGain,
    ExpGainFromVore,
    ExpGainFromAbsorption,
    PassiveHeal,
    CapacityMult,
    OutgoingChanceToEscape,
    OutgoingMeleeDamage,
    OutgoingRangedDamage,
    OutgoingMagicDamage,
    OutgoingDigestionRate,
    OutgoingAbsorptionRate,
    OutgoingNutrition,
    OutgoingManaAbsorbHundreths,
    OutgoingMeleeShift,
    OutgoingRangedShift,
    OutgoingMagicShift,
    OutgoingVoreOddsMult,
    OutgoingGrowthRate,
    OutgoingCritRateShift,
    OutgoingCritDamageMult,
    OutgoingGrazeRateShift,
    OutgoingGrazeDamageMult,
    IncomingChanceToEscape,
    IncomingMeleeDamage,
    IncomingRangedDamage,
    IncomingMagicDamage,
    IncomingDigestionRate,
    IncomingAbsorptionRate,
    IncomingNutrition,
    IncomingManaAbsorbHundreths,
    IncomingMeleeShift,
    IncomingRangedShift,
    IncomingMagicShift,
    IncomingVoreOddsMult,
    IncomingGrowthRate,
    IncomingCritRateShift,
    IncomingCritDamageMult,
    IncomingGrazeRateShift,
    IncomingGrazeDamageMult,
    FlatHitReduction,
    SpeedLossFromWeightMultiplier,
    DodgeLossFromWeightMultiplier,
    BulkMultiplier,
    SpeedMultiplier,
    MinSpeed,
    SpeedBonus,
    MeleeAttacks,
    RangedAttacks,
    VoreAttacks,
    SpellAttacks,
    HealthMultiplier,
    ManaMultiplier,
    StaminaMultiplier,   
    TurnCanFlee,
    DigestionImmunityTurns,
    HealthRegen,
    ManaRegen,
    OnLevelUpBonusToAllStats,
    OnLevelUpBonusToGiveToTwoRandomStats,
    OnLevelUpAllowAnyStat,
    Scale,
    StatMult,
    StrengthMult,
    DexterityMult,
    VoracityMult,
    AgilityMult,
    WillMult,
    MindMult,
    EnduranceMult,
    StomachMult,
    VirtualDexMult,
    VirtualStrMult,
    FireDamageTaken,
    GrowthDecayRate,
    SightRangeBoost,
    DeployCostMult,
    UpkeepMult,

    enumcounter, // should always be last
}

public class CustomTrait : MonoBehaviour
{
    int current_id;
    CustomTraitBoost trait;
    public CustomTraitEditor CustomTraitEditor;
    public CustomTraitComponentMenu compMenu;
    public Transform Folder;

    public TextMeshProUGUI ToolTipName;
    public TextMeshProUGUI ToolTipDesc;

    public InputField name;
    public InputField description;
    public InputField tags;
    public TMP_Dropdown tier;

    public Button modifyComps;
    
    public CustomTraitCompMod CompPrefab;
    public List<CustomTraitCompMod> ActiveCompsList;

    internal void Open(int id)
    {
        gameObject.SetActive(true);
        CustomTraitEditor.gameObject.SetActive(false);
        current_id = id;
        ActiveCompsList= new List<CustomTraitCompMod>();
        trait = State.CustomTraitList.Where(x => x.id == current_id).FirstOrDefault();
        LoadTrait();
        RefreshActive();
        modifyComps.onClick.AddListener(() =>
        {
            compMenu.Open(current_id);
            int children = Folder.childCount;
            for (int i = children - 1; i >= 0; i--)
            {
                Destroy(Folder.GetChild(i).gameObject);
            }
        });
    }

    public void LoadTrait()
    {   
        name.text = trait.name;
        description.text = trait.description;       
        tier.value = (int)trait.tier;
        tier.RefreshShownValue();
        string tagsCombine = "";
        foreach (string tag in trait.tags) 
        {
            tagsCombine += tag + ",";
        }
        tags.text = tagsCombine;
    }

    public void SaveTrait()
    {
        trait.name = name.text;
        trait.description = description.text;
        List<string> seperatedTags = new List<string>();
        foreach (var item in tags.text.Split(','))
        {
            if (item.Length > 0)
            {
                seperatedTags.Add(item.Trim());
            }
        }
        trait.tags = seperatedTags;

        foreach (var cmp in ActiveCompsList)
        {
            if (IsToggle(cmp.comp))
            {
                trait.comps[cmp.comp] = cmp.Toggle.enabled ? 1 : 0;
            }
            else 
            {
                trait.comps[cmp.comp] = float.TryParse(cmp.Inputfield.text, out float output) ? output : 1;
            }
        }

        ExternalTraitHandler.CustomTraitSaver(trait);
    }

    public void SaveClose()
    {
        SaveTrait();
        DiscardClose();
    }

    public void DiscardClose()
    {
        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }
        gameObject.SetActive(false);
        CustomTraitEditor.Open();

    }

    public void Remove()
    {
        var rem = State.CustomTraitList.Where(x => current_id == x.id).FirstOrDefault();
        ExternalTraitHandler.CustomTraitRemover(rem);
        if (State.CustomTraitList.Contains(rem))
        {
            State.CustomTraitList.Remove(rem);  
        }
        DiscardClose();
    }
    public void RefreshActive()
    {
        foreach (var item in trait.comps.Keys)
        {
            var obj = Instantiate(CompPrefab, Folder);
            var cmp = obj.GetComponent<CustomTraitCompMod>();
            if (IsToggle(item))
            {
                cmp.Inputfield.gameObject.SetActive(false);
                cmp.Toggle.gameObject.SetActive(true);
                cmp.Toggle.enabled = trait.comps[item] >= 1;
            }
            else 
            {
                cmp.Inputfield.text = trait.comps[item].ToString();
            }
            cmp.Text.text = item.ToString();
            cmp.comp = item;
            ActiveCompsList.Add(cmp);
        }
    }

    public void ChangeToolTip(CustomTraitComp value)
    {
        switch (value)
        {

            case CustomTraitComp.ExpRequired:
                ToolTipName.text = "Exp Required to Level Multiplier";
                ToolTipDesc.text = "The amount of experience required to level.";
                break;
            case CustomTraitComp.ExpGain:
                ToolTipName.text = "Exp Gain Multiplier";
                ToolTipDesc.text = "The amount of experience gained from all sources.";
                break;
            case CustomTraitComp.ExpGainFromVore:
                ToolTipName.text = "Exp Gain From Vore Multiplier";
                ToolTipDesc.text = "The amount of experience gained from a successful vore attempt.";
                break;
            case CustomTraitComp.ExpGainFromAbsorption:
                ToolTipName.text = "Exp Gain From Absorption Multiplier";
                ToolTipDesc.text = "The amount of experience gained from absorbing prey";
                break;
            case CustomTraitComp.PassiveHeal:
                ToolTipName.text = "Passive Healing Multiplier";
                ToolTipDesc.text = "The healing done out of combat healing.";
                break;
            case CustomTraitComp.CapacityMult:
                ToolTipName.text = "Capacity Multiplier";
                ToolTipDesc.text = "The capacity of a unit, independent of the stomach stat.";
                break;
            case CustomTraitComp.OutgoingChanceToEscape:
                ToolTipName.text = "Chance to Excape Multiplier (Outgoing)";
                ToolTipDesc.text = "The chance a unit escapes from a unit.";
                break;
            case CustomTraitComp.OutgoingMeleeDamage:
                ToolTipName.text = "Melee Damage Multiplier (Outgoing)";
                ToolTipDesc.text = "The damage dealt by a unit's melee attacks.";
                break;
            case CustomTraitComp.OutgoingRangedDamage:
                ToolTipName.text = "Ranged Damage Multiplier (Outgoing)";
                ToolTipDesc.text = "The damage dealt by a unit's ranged attacks.";
                break;
            case CustomTraitComp.OutgoingMagicDamage:
                ToolTipName.text = "Magic Damage Multiplier (Outgoing)";
                ToolTipDesc.text = "The damage dealt by a unit's spells.";
                break;
            case CustomTraitComp.OutgoingDigestionRate:
                ToolTipName.text = "Digestion Rate Multiplier (Outgoing)";
                ToolTipDesc.text = "The damage dealt by a unit's digestion.";
                break;
            case CustomTraitComp.OutgoingAbsorptionRate:
                ToolTipName.text = "Absorption Rate Multiplier (Outgoing)";
                ToolTipDesc.text = "The speed a unit absorbs another.";
                break;
            case CustomTraitComp.OutgoingNutrition:
                ToolTipName.text = "Nutrition Multiplier (Outgoing)";
                ToolTipDesc.text = "The amount of health a unit provides when being absorbed.";
                break;
            case CustomTraitComp.OutgoingManaAbsorbHundreths:
                ToolTipName.text = "Mana Absobtion (Outgoing)";
                ToolTipDesc.text = "The amount of mana a unit provides when being absorbed.";
                break;
            case CustomTraitComp.OutgoingMeleeShift:
                ToolTipName.text = "Melee Accuracy Addition (Outgoing)";
                ToolTipDesc.text = "Adds this amount to a unit's melee accuracy. \n Note: 1 is equivalent to 100%";
                break;
            case CustomTraitComp.OutgoingRangedShift:
                ToolTipName.text = "Ranged Accuracy Addition (Outgoing)";
                ToolTipDesc.text = "Adds this amount to a unit's ranged accuracy. \n Note: 1 is equivalent to 100%";
                break;
            case CustomTraitComp.OutgoingMagicShift:
                ToolTipName.text = "Magic Accuracy Addition (Outgoing)";
                ToolTipDesc.text = "Adds this amount to a unit's magic accuracy. \n Note: 1 is equivalent to 100%";
                break;
            case CustomTraitComp.OutgoingVoreOddsMult:
                ToolTipName.text = "Vore Odds Multiplier (Outgoing)";
                ToolTipDesc.text = "The chance a unit successful vores another.";
                break;
            case CustomTraitComp.OutgoingGrowthRate:
                ToolTipName.text = "Growth Rate Multiplier (Outgoing)";
                ToolTipDesc.text = "The amount of growth a unit provides, assuming it's predator can grow.";
                break;
            case CustomTraitComp.OutgoingCritRateShift:
                ToolTipName.text = "Critical Rate Addition (Outgoing)";
                ToolTipDesc.text = "Adds this amount to a unit's critical chance. \n Note: 1 is equivalent to 100%";
                break;
            case CustomTraitComp.OutgoingCritDamageMult:
                ToolTipName.text = "Critical Damage (Outgoing)";
                ToolTipDesc.text = "The critical damage a unit deals by the value set in content settings.";
                break;
            case CustomTraitComp.OutgoingGrazeRateShift:
                ToolTipName.text = "Graze Rate Addition (Outgoing)";
                ToolTipDesc.text = "Adds this amount to a unit's graze chance. \n Note: 1 is equivalent to 100%";
                break;
            case CustomTraitComp.OutgoingGrazeDamageMult:
                ToolTipName.text = "Graze Damage Multiplier (Outgoing)";
                ToolTipDesc.text = "The graze damage a unit deals by the value set in content settings.";
                break;
            case CustomTraitComp.IncomingChanceToEscape:
                ToolTipName.text = "Chance to Excape Multiplier (Incoming)";
                ToolTipDesc.text = "The chance to a unit escapes another.";
                break;
            case CustomTraitComp.IncomingMeleeDamage:
                ToolTipName.text = "Melee Damage Multiplier (Incoming)";
                ToolTipDesc.text = "The damage dealt to a unit by melee.";
                break;
            case CustomTraitComp.IncomingRangedDamage:
                ToolTipName.text = "Ranged Damage Multiplier (Incoming)";
                ToolTipDesc.text = "The damage dealt to a unit by ranged.";
                break;
            case CustomTraitComp.IncomingMagicDamage:
                ToolTipName.text = "Magic Damage Multiplier (Incoming)";
                ToolTipDesc.text = "The damage dealt to a unit by spells.";
                break;
            case CustomTraitComp.IncomingDigestionRate:
                ToolTipName.text = "Digestion Rate Multiplier (Incoming)";
                ToolTipDesc.text = "The damage dealt to a unit by digestion.";
                break;
            case CustomTraitComp.IncomingAbsorptionRate:
                ToolTipName.text = "Absorption Rate Multiplier (Incoming)";
                ToolTipDesc.text = "The speed a unit is absorbed.";
                break;
            case CustomTraitComp.IncomingNutrition:
                ToolTipName.text = "Nutrition Multiplier (Incoming)";
                ToolTipDesc.text = "The healing a unit provides when absorbed.";
                break;
            case CustomTraitComp.IncomingManaAbsorbHundreths:
                ToolTipName.text = "Mana Absobtion (Incoming)";
                ToolTipDesc.text = "The healing a unit provides when absorbed.";
                break;
            case CustomTraitComp.IncomingMeleeShift:
                ToolTipName.text = "Melee Accuracy Addition (Incoming)";
                ToolTipDesc.text = "Adds a unit's chance to be hit by melee. \n Note: 1 is equivalent to 100%";
                break;
            case CustomTraitComp.IncomingRangedShift:
                ToolTipName.text = "Ranged Accuracy Addition (Incoming)";
                ToolTipDesc.text = "Adds a unit's chance to be hit by ranged. \n Note: 1 is equivalent to 100%";
                break;
            case CustomTraitComp.IncomingMagicShift:
                ToolTipName.text = "Magic Accuracy Addition (Incoming)";
                ToolTipDesc.text = "Adds a unit's chance to be hit by spells. \n Note: 1 is equivalent to 100%";
                break;
            case CustomTraitComp.IncomingVoreOddsMult:
                ToolTipName.text = "Vore Odds Multiplier (Incoming)";
                ToolTipDesc.text = "Adds a unit's chance to be eaten. \n Note: 1 is equivalent to 100%";
                break;
            case CustomTraitComp.IncomingGrowthRate:
                ToolTipName.text = "Growth Rate Multiplier (Incoming)";
                ToolTipDesc.text = "The growth a unit is given when absorbing another, assuming it can grow";
                break;
            case CustomTraitComp.IncomingCritRateShift:
                ToolTipName.text = "Critical Rate Addition (Incoming)";
                ToolTipDesc.text = "Adds a unit's chance to be critically hit by another. \n Note: 1 is equivalent to 100%";
                break;
            case CustomTraitComp.IncomingCritDamageMult:
                ToolTipName.text = "Critical Damage Multiplier (Incoming)";
                ToolTipDesc.text = "The critical damage a unit takes by the value set in content settings";
                break;
            case CustomTraitComp.IncomingGrazeRateShift:
                ToolTipName.text = "Graze Rate Addition (Incoming)";
                ToolTipDesc.text = "Adds a unit's chance to be grazed by another. \n Note: 1 is equivalent to 100";
                break;
            case CustomTraitComp.IncomingGrazeDamageMult:
                ToolTipName.text = "Graze Damage (Incoming)";
                ToolTipDesc.text = "The graze damage a unit takes by the value set in content settings";
                break;
            case CustomTraitComp.FlatHitReduction:
                ToolTipName.text = "Flat Hit Reduction";
                ToolTipDesc.text = "The chance a unit is hit by anything. \n Note: A value of 0.9 is 10% reduction.";
                break;
            case CustomTraitComp.SpeedLossFromWeightMultiplier:
                ToolTipName.text = "Speed Loss From Weight Multiplier";
                ToolTipDesc.text = "The amount of AP lost based on unit's prey.";
                break;
            case CustomTraitComp.DodgeLossFromWeightMultiplier:
                ToolTipName.text = "Dodge Loss From Weight Multiplier";
                ToolTipDesc.text = "The amount of dodge lost based on unit's prey";
                break;
            case CustomTraitComp.BulkMultiplier:
                ToolTipName.text = "Bulk Multiplier";
                ToolTipDesc.text = "Modifies how much bulk a unit has.";
                break;
            case CustomTraitComp.SpeedMultiplier:
                ToolTipName.text = "Speed Multiplier";
                ToolTipDesc.text = "The amount of AP a unit has, independent of agility.";
                break;
            case CustomTraitComp.MinSpeed:
                ToolTipName.text = "Minimum Speed";
                ToolTipDesc.text = "The minimum AP a unit can have, useful if using reduction components. \n Note: This is a whole number. Decimals will be truncated.";
                break;
            case CustomTraitComp.SpeedBonus:
                ToolTipName.text = "Speed Bonus";
                ToolTipDesc.text = "Adds to the AP a unit is given per turn. \n Note: This is a whole number. Decimals will be truncated.";
                break;
            case CustomTraitComp.MeleeAttacks:
                ToolTipName.text = "Melee Attacks Per Turn";
                ToolTipDesc.text = "Unit uses this fraction of AP per melee attack. \n Note: This is a whole number. Decimals will be truncated";
                break;
            case CustomTraitComp.RangedAttacks:
                ToolTipName.text = "Ranged Attacks Per Turn";
                ToolTipDesc.text = "Unit uses this fraction of AP per ranged attack. \n Note: This is a whole number. Decimals will be truncated";
                break;
            case CustomTraitComp.VoreAttacks:
                ToolTipName.text = "Vore Attacks Per Turn";
                ToolTipDesc.text = "Unit uses this fraction of AP per vore attempt. \n Note: This is a whole number. Decimals will be truncated";
                break;
            case CustomTraitComp.SpellAttacks:
                ToolTipName.text = "Spell Attacks Per Turn";
                ToolTipDesc.text = "Unit uses this fraction of AP per spell cast. \n Note: This is a whole number. Decimals will be truncated";
                break;
            case CustomTraitComp.HealthMultiplier:
                ToolTipName.text = "Health Multiplier";
                ToolTipDesc.text = "Unit's health is multiplied by this value.";
                break;
            case CustomTraitComp.ManaMultiplier:
                ToolTipName.text = "Mana Multiplier";
                ToolTipDesc.text = "Unit's mana is multiplied by this value.";
                break;
            case CustomTraitComp.StaminaMultiplier:
                ToolTipName.text = "Stamina Multiplier";
                ToolTipDesc.text = "Unit's stamina is multiplied by this value.";
                break;
            case CustomTraitComp.TurnCanFlee:
                ToolTipName.text = "Turn Unit Can Flee";
                ToolTipDesc.text = "The turn a unit can flee. \n Note: This is a whole number. Decimals will be truncated.";
                break;
            case CustomTraitComp.DigestionImmunityTurns:
                ToolTipName.text = "Turns of Digestion Immunity";
                ToolTipDesc.text = "The amount of turns a unit recieves no damage when eaten, refreshing when they are released. \n Note: This value is added to the related setting in content settings. \n Note2: This is a whole number. Decimals will be truncated.";
                break;
            case CustomTraitComp.HealthRegen:
                ToolTipName.text = "Health Regen";
                ToolTipDesc.text = "The health a unit regenerates on a per turn basis. \n Note: This is a whole number. Decimals will be truncated";
                break;
            case CustomTraitComp.ManaRegen:
                ToolTipName.text = "Mana Regen Multiplier";
                ToolTipDesc.text = "The mana a unit regenerates on a per turn basis. \n Note: This is a whole number. Decimals will be truncated";
                break;
            case CustomTraitComp.OnLevelUpBonusToAllStats:
                ToolTipName.text = "LevelUp Bonus to All Stats";
                ToolTipDesc.text = "Additionaly grant this number to every stat on level up. \n Note: This is a whole number. Decimals will be truncated";
                break;
            case CustomTraitComp.OnLevelUpBonusToGiveToTwoRandomStats:
                ToolTipName.text = "LevelUp Bouns to Two Stats";
                ToolTipDesc.text = "Additionaly grant this number to two random stat on level up. \n Note: This is a whole number. Decimals will be truncated";
                break;
            case CustomTraitComp.OnLevelUpAllowAnyStat:
                ToolTipName.text = "LevelUp any Stat";
                ToolTipDesc.text = "Allows any stat to be leveled when enabled.";
                break;
            case CustomTraitComp.Scale:
                ToolTipName.text = "Scale Multiplier";
                ToolTipDesc.text = "Unit's size and bulk is multiplied by this value.";
                break;
            case CustomTraitComp.StatMult:
                ToolTipName.text = "Stat Multiplier";
                ToolTipDesc.text = "Unit's stats are multiplied by this value.";
                break;
            case CustomTraitComp.StrengthMult:
                ToolTipName.text = "Strength Multiplier";
                ToolTipDesc.text = "Unit's strength is multiplied by this value.";
                break;
            case CustomTraitComp.DexterityMult:
                ToolTipName.text = "Dexterity Multiplier";
                ToolTipDesc.text = "Unit's dexterity is multiplied by this value.";
                break;
            case CustomTraitComp.VoracityMult:
                ToolTipName.text = "Voracity Multiplier";
                ToolTipDesc.text = "Unit's voracity is multiplied by this value.";
                break;
            case CustomTraitComp.StomachMult:
                ToolTipName.text = "Stomach Multiplier";
                ToolTipDesc.text = "Unit's stomach is multiplied by this value.";
                break;
            case CustomTraitComp.AgilityMult:
                ToolTipName.text = "Agility Multiplier";
                ToolTipDesc.text = "Unit's agility is multiplied by this value.";
                break;
            case CustomTraitComp.EnduranceMult:
                ToolTipName.text = "Endurance Multiplier";
                ToolTipDesc.text = "Unit's endurance is multiplied by this value.";
                break;
            case CustomTraitComp.MindMult:
                ToolTipName.text = "Mind Multiplier";
                ToolTipDesc.text = "Unit's mind is multiplied by this value.";
                break;
            case CustomTraitComp.WillMult:
                ToolTipName.text = "Will Multiplier";
                ToolTipDesc.text = "Unit's will is multiplied by this value.";
                break;
            case CustomTraitComp.VirtualDexMult:
                ToolTipName.text = "Virtual Dexterity Multiplier";
                ToolTipDesc.text = "Adjusts a units perceived dexterity stat without modifying the value. Used in AI item decision-making. \n Note: This is a whole number. Decimals will be truncated";
                break;
            case CustomTraitComp.VirtualStrMult:
                ToolTipName.text = "Virtual Strength Multiplier";
                ToolTipDesc.text = "Adjusts a units perceived strength stat without modifying the value. Used in AI item decision-making. \n Note: This is a whole number. Decimals will be truncated";
                break;
            case CustomTraitComp.FireDamageTaken:
                ToolTipName.text = "Incoming Fire Damage Multiplier";
                ToolTipDesc.text = "The amount of fire damage a unit takes.";
                break;
            case CustomTraitComp.GrowthDecayRate:
                ToolTipName.text = "Growth Rate Decay Multiplier";
                ToolTipDesc.text = "The speed a unit's growth will decay.";
                break;
            case CustomTraitComp.SightRangeBoost:
                ToolTipName.text = "Sight Range";
                ToolTipDesc.text = "The tile range a unit will have. \n Note: This is a whole number. Decimals will be truncated";
                break;
            case CustomTraitComp.DeployCostMult:
                ToolTipName.text = "Deployment Cost Multiplier";
                ToolTipDesc.text = "Multiplies the value of a unit's deployment cost, adjusting how many spots it takes in an army. Take caution setting this to a low decimal value.";
                break;
            case CustomTraitComp.UpkeepMult:
                ToolTipName.text = "Upkeep Multiplier";
                ToolTipDesc.text = "Multiplies the value of a unit's upkeep";
                break;
            default:
                ToolTipName.text = "";
                ToolTipDesc.text = "";
                break;
        }
    }

    bool IsToggle(CustomTraitComp comp)
    {
        switch (comp)
        {
            case CustomTraitComp.OnLevelUpAllowAnyStat:
                return true;
            default:
                return false;
        }
    }
}