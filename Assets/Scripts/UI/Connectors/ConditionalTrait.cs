using CruxClothing;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TraitCondition
{
    // Arithmetic variables: Can be paired with other arithmetic variables
    Level,
    Health,
    HealthPercent,
    Mana,
    ManaPercent,
    Strength,
    Dexterity,
    Endurance,
    Mind,
    Will,
    Agility,
    Voracity,
    Stomach,
    StatTotal,
    TotalDigestions,
    TotalKills,
    TimesKilled,
    TacticalTurn,
    StrategicTurn,
    AliveAllies,
    AliveEnemies,
    Growth,
    Fullness,

    // singleton variables: Can't be paired with any other variables
    Male,
    Female,
    NonBinary,
    Predator,
    Digesting,
    Absorbing,
    Prey,

    conditionCounter, // Should always be last
}
public enum TraitConditionArithmeticOperator
{
    none,
    add,
    sub,
    mul,
    div,
}
public enum TraitConditionCompareOperator
{
    eq,
    geq,
    leq,
    none
    
}
public enum TraitConditionLogicalOperator
{
    none,
    and,
    or,
    not,
}

public enum TraitConditionTrigger
{
    none,
    OnDeath,
    OnLevelUp,
    OnStrategicTurnEnd,
    OnTacticalTurnStart,
    All,
}
public enum TraitConditionalClassification
{
    Conditional,
    Permanent,
    Temporary,
}

public class ConditionalTrait : MonoBehaviour
{
    int current_id;
    ConditionalTraitContainer trait;

    public ConditionalTraitsEditor ConditionalTraitsEditor;
    public Transform Folder;

    public InputField name;
    public TMP_Dropdown AssociatedTrait;
    public TMP_Dropdown Trigger;
    public TMP_Dropdown Classification;


    public Button AddBtn;
    internal Button newCondition;

    public ConditionalTraitArithmeticPanel ArithmeticPanel;

    public ConditionalTraitInfoBlock HelpBlock;

    public ConditionalTraitOpBlockPrefab opBlockPrefab;

    internal List<ConditionalTraitOpBlockPrefab> OpBlocksPrefabList;
    internal Dictionary<ConditionalTraitOperationBlock, TraitConditionLogicalOperator> OpBlocks;

    internal void Open(int id)
    {
        gameObject.SetActive(true);
        ConditionalTraitsEditor.gameObject.SetActive(false);
        HelpBlock.HelpPanel.gameObject.SetActive(false);
        HelpBlock.helpOn = false;
        OpBlocks = new Dictionary<ConditionalTraitOperationBlock, TraitConditionLogicalOperator>();
        OpBlocksPrefabList = new List<ConditionalTraitOpBlockPrefab>();
        trait = State.ConditionalTraitList.Where(x => x.id == id).FirstOrDefault();
        current_id = id;
        name.text = trait.name;
        foreach (var item in trait.OperationBlocks)
        {
            var obj = Instantiate(opBlockPrefab, Folder);
            var ob = obj.GetComponent<ConditionalTraitOpBlockPrefab>();
            ob.arithmeticSummary.text = item.Key.summary;
            ob.logicOp.value = (int)item.Value;
            ob.conditionValue.text = item.Key.compareValue.ToString();
            switch (item.Key.compareOp)
            {
                case TraitConditionCompareOperator.leq:
                    ob.conditionOp.text = "<=";
                    break;
                case TraitConditionCompareOperator.eq:
                    ob.conditionOp.text = "=";
                    break;  
                case TraitConditionCompareOperator.geq:
                    ob.conditionOp.text = ">=";
                    break;  
                case TraitConditionCompareOperator.none:
                    ob.conditionOp.text = "";
                    ob.conditionValue.text = "";
                    break;
                default:
                    ob.conditionOp.text = item.Key.compareOp.ToString();
                    break;
            }          
            ob.logicOp.value = (int)item.Value;
            ob.associetedBlock = item.Key;
            OpBlocks.Add(ob.associetedBlock, item.Value);
            ob.editCondition.onClick.AddListener(() =>
            {
                ArithmeticPanel.Open(ob);
            });
            ob.logicOp.onValueChanged.AddListener((int val) =>
            {
                OpBlocks[item.Key] = (TraitConditionLogicalOperator)val;
            });
            OpBlocksPrefabList.Add(ob);
        }

        Trigger.options.Clear();
        foreach (TraitConditionTrigger trigger in ((TraitConditionTrigger[])Enum.GetValues(typeof(TraitConditionTrigger))))
        {
            Trigger.options.Add(new TMP_Dropdown.OptionData(trigger.ToString()));
        }
        Trigger.value = (int)trait.trigger;

        Classification.options.Clear();
        foreach (TraitConditionalClassification classification in ((TraitConditionalClassification[])Enum.GetValues(typeof(TraitConditionalClassification))))
        {
            Classification.options.Add(new TMP_Dropdown.OptionData(classification.ToString()));
        }
        Classification.value = (int)trait.classification;

        Dictionary<Traits, int> traitDict = new Dictionary<Traits, int>();
        int val2 = 0;
        AssociatedTrait.options.Clear();
        foreach (RandomizeList rl in State.RandomizeLists)
        {
            traitDict[(Traits)rl.id] = val2;
            val2++;
            AssociatedTrait.options.Add(new TMP_Dropdown.OptionData(rl.name.ToString()));
            if (trait.associatedTrait == (Traits)rl.id)
            {
                AssociatedTrait.value = AssociatedTrait.options.Count();
            }
        }
        foreach (CustomTraitBoost ct in State.CustomTraitList)
        {
            traitDict[(Traits)ct.id] = val2;
            val2++;
            AssociatedTrait.options.Add(new TMP_Dropdown.OptionData(ct.name.ToString()));
            if (trait.associatedTrait == (Traits)ct.id)
            {
                AssociatedTrait.value = AssociatedTrait.options.Count();
            }
        }
        foreach (Traits traitId in ((Traits[])Enum.GetValues(typeof(Traits))).OrderBy(s =>
        {
            return s >= Traits.LightningSpeed ? "ZZZ" + s.ToString() : s.ToString();
        }))
        {
            traitDict[traitId] = val2;
            val2++;
            AssociatedTrait.options.Add(new TMP_Dropdown.OptionData(traitId.ToString()));
            if (trait.associatedTrait == traitId)
            {
                AssociatedTrait.value = AssociatedTrait.options.Count();
            }
        }

        LoadOperationBlockLogicals();
        CreateAddButton();
    }

    private void GetAssociatedTrait()
    {
        if (State.RandomizeLists.Any(rl => rl.name == AssociatedTrait.options[AssociatedTrait.value].text))
        {
            RandomizeList randomizeList = State.RandomizeLists.Single(rl => rl.name == AssociatedTrait.options[AssociatedTrait.value].text);
            trait.associatedTrait = (Traits)randomizeList.id;
            
        }
        if (State.CustomTraitList.Any(ct => ct.name == AssociatedTrait.options[AssociatedTrait.value].text))
        {
            CustomTraitBoost customTrait = State.CustomTraitList.Single(ct => ct.name == AssociatedTrait.options[AssociatedTrait.value].text);
            trait.associatedTrait = (Traits)customTrait.id;
        }
        if (Enum.TryParse(AssociatedTrait.options[AssociatedTrait.value].text, out Traits traitout))
        {
            trait.associatedTrait = traitout;
        }
    }

    private void CreateAddButton()
    {
        if (newCondition != null)
        {
            newCondition.onClick.RemoveAllListeners();
            Destroy(newCondition.gameObject);
        }
        newCondition = Instantiate(AddBtn, Folder);
        var btn = newCondition.GetComponent<Button>();
        var btnTxt = btn.GetComponentInChildren<Text>();
        btnTxt.text = "Add";
        btn.onClick.AddListener(() =>
        {
            var created = CreateConditionOperationBlock();
            OpBlocksPrefabList.Add(created);
            CreateAddButton();
            LoadOperationBlockLogicals();
        });
    }

    private ConditionalTraitOpBlockPrefab CreateConditionOperationBlock()
    {
        var obj = Instantiate(opBlockPrefab, Folder);
        var ob = obj.GetComponent<ConditionalTraitOpBlockPrefab>();
        ob.arithmeticSummary.text = "";
        ob.conditionOp.text = "";
        ob.conditionValue.text = "";
        ob.logicOp.value = 0;
        ob.associetedBlock = new ConditionalTraitOperationBlock();
        OpBlocks.Add(ob.associetedBlock, 0);
        ob.editCondition.onClick.AddListener(() =>
        {
            ArithmeticPanel.Open(ob);
        });
        ob.logicOp.onValueChanged.AddListener((int val) =>
        {
            OpBlocks[ob.associetedBlock] = (TraitConditionLogicalOperator)val;
        });
        return ob;
    }

    private void LoadOperationBlockLogicals()
    {
        if (OpBlocksPrefabList.Count() > 1)
        {
            foreach (var opBlock in OpBlocksPrefabList)
            {
                opBlock.logicOp.gameObject.SetActive(true);
            }
            OpBlocksPrefabList.Last().logicOp.gameObject.SetActive(false);
        }
    }

    public void SaveTrait()
    {
        trait.id = current_id;
        trait.name = name.text;
        trait.trigger = (TraitConditionTrigger)Trigger.value;
        GetAssociatedTrait();

        trait.classification = (TraitConditionalClassification)Classification.value;

        // Due to their nature of changing values, Random traits can only be Permanent to prevent issues.
        if (trait.associatedTrait >= (Traits)1000 && trait.associatedTrait <= (Traits)2999)
        {
            trait.classification = TraitConditionalClassification.Permanent;
        }
        trait.OperationBlocks.Clear();
        foreach (var opBlock in OpBlocks)
        {
            trait.OperationBlocks.Add(opBlock.Key, opBlock.Value);
        }

        ExternalTraitHandler.ConditionalTraitSaver(trait);
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
        ConditionalTraitsEditor.Open();

    }

    public void Refresh()
    {
        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }
        foreach (var item in OpBlocks)
        {
            var obj = Instantiate(opBlockPrefab, Folder);
            var ob = obj.GetComponent<ConditionalTraitOpBlockPrefab>();
            ob.arithmeticSummary.text = item.Key.summary;
            ob.logicOp.value = (int)item.Value;
            ob.conditionValue.text = item.Key.compareValue.ToString();
            switch (item.Key.compareOp)
            {
                case TraitConditionCompareOperator.leq:
                    ob.conditionOp.text = "<=";
                    break;
                case TraitConditionCompareOperator.eq:
                    ob.conditionOp.text = "=";
                    break;
                case TraitConditionCompareOperator.geq:
                    ob.conditionOp.text = ">=";
                    break;
                default:
                    ob.conditionOp.text = "Unconfigured";
                    ob.conditionValue.text = "";
                    break;
            }
            ob.associetedBlock = item.Key;
            OpBlocksPrefabList.Add(obj);
            ob.editCondition.onClick.AddListener(() =>
            {
                ArithmeticPanel.Open(ob);
            });
            ob.logicOp.onValueChanged.AddListener((int val) =>
            {
                OpBlocks[ob.associetedBlock] = (TraitConditionLogicalOperator)val;
            });
        }
        LoadOperationBlockLogicals();
        CreateAddButton();

    }

    public void Remove()
    {
        var rem = State.ConditionalTraitList.Where(x => current_id == x.id).FirstOrDefault();
        //ExternalTraitHandler.CustomTraitRemover(rem);
        ExternalTraitHandler.ConditionalTraitRemover(rem);
        if (State.ConditionalTraitList.Contains(rem))
        {
            State.ConditionalTraitList.Remove(rem);
        }
        DiscardClose();
    }
}