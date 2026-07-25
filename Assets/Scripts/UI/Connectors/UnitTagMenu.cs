using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum UnitTagModifierEffect
{    
    None,
    ChanceToEscapeMult,
    MeleeDamageMult,
    RangedDamageMult,
    MagicDamageMult,
    DigestionRateMult,
    AbsorptionRateMult,
    NutritionMult,
    ManaAbsorbHundreths,
    MeleeShift,
    RangedShift,
    MagicShift,
    VoreOddsMult,
    GrowthRateMult,
    CritRateShift,
    CritDamageMult,
    GrazeRateShift,
    GrazeDamageMult,
}
public enum UnitTagModifierCase
{
    None,
    AgainstTargetIf,
    AgainstTargetIfNot,
    AgainstAnyTarget, // ignores target
    AgainstAnyIfSelfIs, // target is owner of tag
    AgainstAnyIfSelfIsNot, // target is owner of tag
    FromTargetIf,
    FromTargetIfNot,
    FromAnyTarget, // ignores target
    FromAnyIfSelfIs, // target is owner of tag
    FromAnyIfSelfIsNot, // target is owner of tag
}
public enum UnitTagModifierTarget
{
    All,
    IsRace,
    HasTrait,
    HasTag,
    IsGender,
    HasStatusEffect,
    HasAnyStatusEffect,
    IsUnitType,
    IsEnemy,
    IsAlly,
    HasRanged,
    HasMelee,
    HasSpells,
    IsEmpireRace,
    IsMercenaryRace,
    IsMonsterRace,
    IsSpecialMercenaryRace,
    IsPredator,
    IsFull,
    IsDigesting,
    IsAbsorbing,
    IsGreaterLevel,
    IsGreaterBodySize,
    HasGreaterStatTotal,
    HasGreaterHealthPct,
    HasGreaterMandPct,
    IsOverHealthPct,
    IsOverManaPct,
    HasBarrier,
    HasGreaterBarrier,
}

public class UnitTagMenu : MonoBehaviour
{
    int current_id;
    UnitTag unitTag;
    public UnitTagEditor UnitTagEditor;
    public Transform Folder;

    public InputField name;

    public Button AddBtn;
    internal Button newModButton;

    public UnitTagModifierItem ModifierPrefab;
    internal List<UnitTagModifierItem> ModifierList;

    List<Traits> CurrentTraits;
    public TMP_Dropdown TraitDropdown;
    public TextMeshProUGUI TraitList;

    internal void Open(int id)
    {
        gameObject.SetActive(true);
        UnitTagEditor.gameObject.SetActive(false);
        current_id = id;
        ModifierList = new List<UnitTagModifierItem>();
        unitTag = State.UnitTagList.Where(x => x.id == current_id).FirstOrDefault();


        TraitDropdown.options.Clear();
        foreach (RandomizeList rl in State.RandomizeLists)
        {
            TraitDropdown.options.Add(new TMP_Dropdown.OptionData(rl.name.ToString()));
        }
        foreach (CustomTraitBoost ct in State.CustomTraitList)
        {
            TraitDropdown.options.Add(new TMP_Dropdown.OptionData(ct.name.ToString()));
        }
        foreach (ConditionalTraitContainer cdt in State.ConditionalTraitList)
        {
            TraitDropdown.options.Add(new TMP_Dropdown.OptionData(cdt.name.ToString()));
        }
        foreach (Traits traitId in ((Traits[])Enum.GetValues(typeof(Traits))).OrderBy(s =>
        {
            return s >= Traits.LightningSpeed ? "ZZZ" + s.ToString() : s.ToString();
        }))
        {
            TraitDropdown.options.Add(new TMP_Dropdown.OptionData(traitId.ToString()));
        }
        TraitDropdown.RefreshShownValue();

        LoadTag();
        CreateAddButton();
        UpdateInteractable();
    }

    private void CreateAddButton()
    {
        if (newModButton != null)
        {
            newModButton.onClick.RemoveAllListeners();
            Destroy(newModButton.gameObject);
        }
        newModButton = Instantiate(AddBtn, Folder);
        var btn = newModButton.GetComponent<Button>();
        var btnTxt = btn.GetComponentInChildren<Text>();
        btnTxt.text = "Add";
        btn.onClick.AddListener(() =>
        {
            var created = CreateModifierItem();
            ModifierList.Add(created);
            CreateAddButton();
        });
    }
    private UnitTagModifierItem CreateModifierItem()
    {
        var obj = Instantiate(ModifierPrefab, Folder);
        var ob = obj.GetComponent<UnitTagModifierItem>();

        // Populate effect dropdown
        ob.tagEffect.options.Clear();
        foreach (UnitTagModifierEffect tagEff in ((UnitTagModifierEffect[])Enum.GetValues(typeof(UnitTagModifierEffect))))
        {
            ob.tagEffect.options.Add(new TMP_Dropdown.OptionData(tagEff.ToString()));
        }
        ob.tagEffect.RefreshShownValue();

        // Populate case dropdown
        ob.tagCase.options.Clear();
        foreach (UnitTagModifierCase tagCase in ((UnitTagModifierCase[])Enum.GetValues(typeof(UnitTagModifierCase))))
        {
            ob.tagCase.options.Add(new TMP_Dropdown.OptionData(tagCase.ToString()));
        }
        ob.tagCase.RefreshShownValue();

        // Populate target dropdown
        ob.tagTarget.options.Clear();
        foreach (UnitTagModifierTarget tagTarget in ((UnitTagModifierTarget[])Enum.GetValues(typeof(UnitTagModifierTarget))))
        {
            ob.tagTarget.options.Add(new TMP_Dropdown.OptionData(tagTarget.ToString()));
        }
        ob.tagTarget.RefreshShownValue();

        ob.duplicateButton.onClick.AddListenerOnce(() =>
        {
            UnitTagModifierItem dupe = CreateModifierItem();
            dupe.tagEffect.value = ob.tagEffect.value;
            dupe.tagCase.value = ob.tagCase.value;
            dupe.tagTarget.value = ob.tagTarget.value;
            dupe.targetValue.value = ob.targetValue.value;
        });
        ob.DeleteButton.onClick.AddListenerOnce(() =>
        {
            ModifierList.Remove(ob);
            Destroy(ob.gameObject);
        });

        ob.tagTarget.onValueChanged.AddListener((int val) =>
        {
            UnitTagModifierTarget targetType = (UnitTagModifierTarget)val;
            ob.targetValue.gameObject.SetActive(false);
            ob.targetSlider.gameObject.SetActive(false);
            switch (targetType)
            {
                case UnitTagModifierTarget.IsRace:
                    ob.targetValue.gameObject.SetActive(true);
                    // Populate target race dropdown
                    ob.targetValue.options.Clear();
                    foreach (Race targetRace in ((Race[])Enum.GetValues(typeof(Race))))
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(targetRace.ToString()));
                    }
                    ob.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.HasTrait:
                    ob.targetValue.gameObject.SetActive(true);
                    // Populate Trait Dropdown
                    ob.targetValue.options.Clear();
                    foreach (RandomizeList rl in State.RandomizeLists)
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(rl.name.ToString()));
                    }
                    foreach (CustomTraitBoost ct in State.CustomTraitList)
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(ct.name.ToString()));
                    }
                    foreach (ConditionalTraitContainer cdt in State.ConditionalTraitList)
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(cdt.name.ToString()));
                    }
                    foreach (Traits traitId in ((Traits[])Enum.GetValues(typeof(Traits))).OrderBy(s =>
                    {
                        return s >= Traits.LightningSpeed ? "ZZZ" + s.ToString() : s.ToString();
                    }))
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(traitId.ToString()));
                    }
                    ob.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.HasTag:
                    ob.targetValue.gameObject.SetActive(true);
                    // Populate target tag dropdown
                    ob.targetValue.options.Clear();
                    foreach (UnitTag uTag in State.UnitTagList)
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(uTag.name.ToString()));
                    }
                    ob.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.IsGender:
                    ob.targetValue.gameObject.SetActive(true);
                    // Populate target gender dropdown
                    ob.targetValue.options.Clear();
                    foreach (Gender targetGender in ((Gender[])Enum.GetValues(typeof(Gender))))
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(targetGender.ToString()));
                    }
                    ob.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.HasStatusEffect:
                    ob.targetValue.gameObject.SetActive(true);
                    // Populate target status effect dropdown
                    ob.targetValue.options.Clear();
                    foreach (StatusEffectType statusEffectType in ((StatusEffectType[])Enum.GetValues(typeof(StatusEffectType))))
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(statusEffectType.ToString()));
                    }
                    ob.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.IsUnitType:
                    ob.targetValue.gameObject.SetActive(true);
                    // Populate target status effect dropdown
                    ob.targetValue.options.Clear();
                    foreach (UnitType unitType in ((UnitType[])Enum.GetValues(typeof(UnitType))))
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(unitType.ToString()));
                    }
                    ob.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.IsOverHealthPct:
                case UnitTagModifierTarget.IsOverManaPct:
                    ob.targetSlider.gameObject.SetActive(true);
                    ob.RefreshSliderText();
                    ob.targetValue.options.Clear();
                    break;
                default:
                    ob.targetValue.options.Clear();
                    break;
            }
        });
        
        ob.tagCase.onValueChanged.AddListener((int val) =>
        {
            if (val == (int)UnitTagModifierCase.AgainstAnyTarget || val == (int)UnitTagModifierCase.FromAnyTarget)
            {
                ob.tagTarget.gameObject.SetActive(false);
                ob.targetValue.gameObject.SetActive(false);
                ob.targetSlider.gameObject.SetActive(false);
            }
            else
            {
                ob.tagTarget.gameObject.SetActive(true);
                ob.targetValue.gameObject.SetActive(ob.targetValue.IsActive());
                ob.targetSlider.gameObject.SetActive(ob.targetSlider.IsActive());
            }
        });
        ob.targetValue.gameObject.SetActive(false);
        ob.targetSlider.gameObject.SetActive(false);
        return ob;
    }


    public void LoadTag()
    {
        name.text = unitTag.name;
        CurrentTraits = unitTag.AssociatedTraits;
        foreach (var item in unitTag.modifiers)
        {
            UnitTagModifierItem loadMod = CreateModifierItem();
            loadMod.tagEffect.value = (int)item.tagEffect;
            loadMod.tagCase.value = (int)item.tagCase;
            loadMod.tagTarget.value = (int)item.target;
            loadMod.targetValue.gameObject.SetActive(false);
            loadMod.targetSlider.gameObject.SetActive(false);
            switch (item.target)
            {
                case UnitTagModifierTarget.IsRace:
                    loadMod.targetValue.gameObject.SetActive(true);
                    // Populate target race dropdown
                    loadMod.targetValue.options.Clear();
                    foreach (Race targetRace in ((Race[])Enum.GetValues(typeof(Race))))
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(targetRace.ToString()));
                        if (targetRace == (Race)item.targetValue)
                        {
                            loadMod.targetValue.value = loadMod.targetValue.options.Count();
                        }
                    }
                    loadMod.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.HasTrait:
                    loadMod.targetValue.gameObject.SetActive(true);
                    // Populate Trait Dropdown
                    loadMod.targetValue.options.Clear();
                    foreach (RandomizeList rl in State.RandomizeLists)
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(rl.name.ToString()));
                        if (rl.id == item.targetValue)
                        {
                            loadMod.targetValue.value = loadMod.targetValue.options.Count();
                        }
                    }
                    foreach (CustomTraitBoost ct in State.CustomTraitList)
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(ct.name.ToString()));
                        if (ct.id == item.targetValue)
                        {
                            loadMod.targetValue.value = loadMod.targetValue.options.Count();
                        }
                    }
                    foreach (ConditionalTraitContainer cdt in State.ConditionalTraitList)
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(cdt.name.ToString()));
                        if (cdt.id == item.targetValue)
                        {
                            loadMod.targetValue.value = loadMod.targetValue.options.Count();
                        }
                    }
                    foreach (Traits traitId in ((Traits[])Enum.GetValues(typeof(Traits))).OrderBy(s =>
                    {
                        return s >= Traits.LightningSpeed ? "ZZZ" + s.ToString() : s.ToString();
                    }))
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(traitId.ToString()));
                        if (traitId == (Traits)item.targetValue)
                        {
                            loadMod.targetValue.value = loadMod.targetValue.options.Count();
                        }

                    }
                    loadMod.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.HasTag:
                    loadMod.targetValue.gameObject.SetActive(true);
                    // Populate target tag dropdown
                    loadMod.targetValue.options.Clear();
                    foreach (UnitTag uTag in State.UnitTagList)
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(uTag.name.ToString()));
                        if (uTag.id == item.targetValue)
                        {
                            loadMod.targetValue.value = loadMod.targetValue.options.Count();
                        }
                    }
                    loadMod.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.IsGender:
                    loadMod.targetValue.gameObject.SetActive(true);
                    // Populate target gender dropdown
                    loadMod.targetValue.options.Clear();
                    foreach (Gender targetGender in ((Gender[])Enum.GetValues(typeof(Gender))))
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(targetGender.ToString()));
                        if (targetGender == (Gender)item.targetValue)
                        {
                            loadMod.targetValue.value = loadMod.targetValue.options.Count();
                        }
                    }
                    loadMod.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.HasStatusEffect:
                    loadMod.targetValue.gameObject.SetActive(true);
                    // Populate target status effect dropdown
                    loadMod.targetValue.options.Clear();
                    foreach (StatusEffectType statusEffectType in ((StatusEffectType[])Enum.GetValues(typeof(StatusEffectType))))
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(statusEffectType.ToString()));
                        if (statusEffectType == (StatusEffectType)item.targetValue)
                        {
                            loadMod.targetValue.value = loadMod.targetValue.options.Count();
                        }
                    }
                    loadMod.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.IsUnitType:
                    loadMod.targetValue.gameObject.SetActive(true);
                    // Populate target status effect dropdown
                    loadMod.targetValue.options.Clear();
                    foreach (UnitType unitType in ((UnitType[])Enum.GetValues(typeof(UnitType))))
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(unitType.ToString()));
                        if (unitType == (UnitType)item.targetValue)
                        {
                            loadMod.targetValue.value = loadMod.targetValue.options.Count();
                        }
                    }
                    loadMod.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.IsOverHealthPct:
                case UnitTagModifierTarget.IsOverManaPct:
                    loadMod.targetSlider.gameObject.SetActive(true);
                    loadMod.targetSlider.value = item.targetValue;
                    loadMod.RefreshSliderText();
                    loadMod.targetValue.gameObject.SetActive(false);
                    loadMod.targetValue.options.Clear();
                    break;
                default:
                    loadMod.targetValue.gameObject.SetActive(false);
                    loadMod.targetSlider.gameObject.SetActive(false);
                    loadMod.targetValue.options.Clear();
                    break;
            }
            loadMod.modifierValue.text = item.modifierValue.ToString();
            ModifierList.Add(loadMod);
        }
    }

    public void SaveTag()
    {
        unitTag.name = name.text;
        unitTag.AssociatedTraits = CurrentTraits;
        unitTag.modifiers.Clear();

        foreach (var mod in ModifierList)
        {
            UnitTagModifier newMod = new UnitTagModifier();
            newMod.id = unitTag.modifiers.Count();
            newMod.modifierValue = float.TryParse(mod.modifierValue.text, out float mv) ? mv : 1;
            newMod.target = (UnitTagModifierTarget)mod.tagTarget.value;
            newMod.tagEffect = (UnitTagModifierEffect)mod.tagEffect.value;
            newMod.tagCase = (UnitTagModifierCase)mod.tagCase.value;
            switch (newMod.target)
            {
                case UnitTagModifierTarget.IsRace:
                    if (Enum.TryParse(mod.targetValue.options[mod.targetValue.value].text, out Race race))
                    {
                        newMod.targetValue = (int)race;
                    }
                    break;
                case UnitTagModifierTarget.HasTrait:
                    if (State.RandomizeLists.Any(rl => rl.name == mod.targetValue.options[mod.targetValue.value].text))
                    {
                        newMod.targetValue = (int)State.RandomizeLists.Where(rl => rl.name == mod.targetValue.options[mod.targetValue.value].text).FirstOrDefault()?.id;
                    }
                    if (State.CustomTraitList.Any(ct => ct.name == mod.targetValue.options[mod.targetValue.value].text))
                    {
                        newMod.targetValue = (int)State.CustomTraitList.Where(ct => ct.name == mod.targetValue.options[mod.targetValue.value].text).FirstOrDefault()?.id;
                    }
                    if (State.ConditionalTraitList.Any(cdt => cdt.name == mod.targetValue.options[mod.targetValue.value].text))
                    {
                        newMod.targetValue = (int)State.ConditionalTraitList.Where(cdt => cdt.name == mod.targetValue.options[mod.targetValue.value].text).FirstOrDefault()?.id;
                    }
                    if (Enum.TryParse(mod.targetValue.options[mod.targetValue.value].text, out Traits trait))
                    {
                        newMod.targetValue = (int)trait;
                    }
                    break;
                case UnitTagModifierTarget.HasTag:
                    newMod.targetValue = mod.targetValue.value;
                    break;
                case UnitTagModifierTarget.IsGender:
                    if (Enum.TryParse(mod.targetValue.options[mod.targetValue.value].text, out Gender gender))
                    {
                        newMod.targetValue = (int)gender;
                    }
                    break;
                case UnitTagModifierTarget.HasStatusEffect:
                    if (Enum.TryParse(mod.targetValue.options[mod.targetValue.value].text, out StatusEffectType statusEffect))
                    {
                        newMod.targetValue = (int)statusEffect;
                    }
                    break;
                case UnitTagModifierTarget.IsUnitType:
                    if (Enum.TryParse(mod.targetValue.options[mod.targetValue.value].text, out UnitType unitType))
                    {
                        newMod.targetValue = (int)unitType;
                    }
                    break;
                case UnitTagModifierTarget.IsOverHealthPct:
                case UnitTagModifierTarget.IsOverManaPct:
                    newMod.targetValue = mod.targetValue.value;
                    break;
                default:
                    break;
            }
            unitTag.modifiers.Add(newMod);
        }
        ExternalTraitHandler.UnitTagSaver(unitTag);

    }

    public void AddTrait()
    {
        if (State.RandomizeLists.Any(rl => rl.name == TraitDropdown.options[TraitDropdown.value].text))
        {
            CurrentTraits.Add((Traits)State.RandomizeLists.Where(rl => rl.name == TraitDropdown.options[TraitDropdown.value].text).FirstOrDefault()?.id);
        }
        if (State.CustomTraitList.Any(ct => ct.name == TraitDropdown.options[TraitDropdown.value].text))
        {
            CurrentTraits.Add((Traits)State.CustomTraitList.Where(ct => ct.name == TraitDropdown.options[TraitDropdown.value].text).FirstOrDefault()?.id);
        }
        if (State.ConditionalTraitList.Any(cdt => cdt.name == TraitDropdown.options[TraitDropdown.value].text))
        {
            CurrentTraits.Add((Traits)State.ConditionalTraitList.Where(cdt => cdt.name == TraitDropdown.options[TraitDropdown.value].text).FirstOrDefault()?.id);
        }
        if (Enum.TryParse(TraitDropdown.options[TraitDropdown.value].text, out Traits trait))
        {
            if (CurrentTraits.Contains(trait) == false)
                CurrentTraits.Add(trait);
        }
        UpdateInteractable();
    }
    public void RemoveTrait()
    {
        if (State.RandomizeLists.Any(rl => rl.name == TraitDropdown.options[TraitDropdown.value].text))
        {
            CurrentTraits.Remove((Traits)State.RandomizeLists.Where(rl => rl.name == TraitDropdown.options[TraitDropdown.value].text).FirstOrDefault()?.id);
        }
        if (State.CustomTraitList.Any(ct => ct.name == TraitDropdown.options[TraitDropdown.value].text))
        {
            CurrentTraits.Remove((Traits)State.CustomTraitList.Where(ct => ct.name == TraitDropdown.options[TraitDropdown.value].text).FirstOrDefault()?.id);
        }
        if (State.ConditionalTraitList.Any(ct => ct.name == TraitDropdown.options[TraitDropdown.value].text))
        {
            CurrentTraits.Remove((Traits)State.ConditionalTraitList.Where(ct => ct.name == TraitDropdown.options[TraitDropdown.value].text).FirstOrDefault()?.id);
        }
        if (Enum.TryParse(TraitDropdown.options[TraitDropdown.value].text, out Traits trait))
        {
            CurrentTraits.Remove(trait);
        }
        UpdateInteractable();
    }

    public void UpdateInteractable()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Traits:");
        if (CurrentTraits == null)
            CurrentTraits = new List<Traits>();
        foreach (Traits trait in CurrentTraits)
        {
            if (State.RandomizeLists.Any(rl => (Traits)rl.id == trait))
            {
                sb.AppendLine(State.RandomizeLists.Where(rl => (Traits)rl.id == trait).FirstOrDefault().name);
            }
            else if (State.CustomTraitList.Any(ct => (Traits)ct.id == trait))
            {
                sb.AppendLine(State.CustomTraitList.Where(ct => (Traits)ct.id == trait).FirstOrDefault().name);
            }
            else if (State.ConditionalTraitList.Any(ct => (Traits)ct.id == trait))
            {
                sb.AppendLine(State.ConditionalTraitList.Where(ct => (Traits)ct.id == trait).FirstOrDefault().name);
            }
            else
                sb.AppendLine(trait.ToString());
        }
        TraitList.text = sb.ToString();
    }

    public void SaveClose()
    {
        SaveTag();
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
        ModifierList.Clear();
        UnitTagEditor.Open();
    }

    public void Remove()
    {
        var rem = State.UnitTagList.Where(x => current_id == x.id).FirstOrDefault();
        ExternalTraitHandler.UnitTagRemover(rem);
        if (State.UnitTagList.Contains(rem))
        {
            State.UnitTagList.Remove(rem);  
        }
        ModifierList.Clear();
        DiscardClose();        
    }
}