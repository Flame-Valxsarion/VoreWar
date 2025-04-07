using OdinSerializer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ConditionalTraitsEditor : MonoBehaviour
{
    public Transform Folder;
    public GameObject CondTraitListItemPrefab;
    internal List<ConditionalTraitListItem> CreatedConditionals;

    public Button AddBtn;
    internal Button AddBtnInstance;

    public ConditionalTrait ConditionalTrait;

    internal void Open()
    {
        gameObject.SetActive(true);

        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }

        Setup();
    }

    private void Setup()
    {
        CreatedConditionals = new List<ConditionalTraitListItem>();
        foreach (var entry in State.ConditionalTraitList)
        {
            var usererTrait = CreateConditionalTraitListItem(entry);
            CreatedConditionals.Add(usererTrait);
        }
        CreateAddButton();
    }

    private void CreateAddButton()
    {
        if (AddBtnInstance != null)
        {
            AddBtnInstance.onClick.RemoveAllListeners();
            Destroy(AddBtnInstance.gameObject);
        }
        AddBtnInstance = Instantiate(AddBtn, Folder);
        var btn = AddBtnInstance.GetComponent<Button>();
        var btnTxt = btn.GetComponentInChildren<Text>();
        btnTxt.text = "Add";
        btn.onClick.AddListener(() =>
        {
            var created = CreateConditionalTraitListItem();
            CreatedConditionals.Add(created);
            CreateAddButton();
        });
    }

    private ConditionalTraitListItem CreateConditionalTraitListItem(ConditionalTraitContainer condTrait = null)
    {
        if (condTrait != null)
        {
            var obj = Instantiate(CondTraitListItemPrefab, Folder);
            var ct = obj.GetComponent<ConditionalTraitListItem>();
            ct.name.text = condTrait.name;
            ct.id = condTrait.id;
            ct.modifybtn.onClick.AddListener(() =>
            {
                ConditionalTrait.Open(ct.id);
            });
            return ct;
        }
        else
        {
            var obj = Instantiate(CondTraitListItemPrefab, Folder);
            var rt = obj.GetComponent<ConditionalTraitListItem>();
            rt.name.text = "ConditionalTrait";
            var last = CreatedConditionals.LastOrDefault();
            rt.id = last == null ? 6001 : FindNewId();
            ConditionalTraitContainer conditionalTrait = new ConditionalTraitContainer();
            conditionalTrait.name = "ConditionalTrait";
            conditionalTrait.id = rt.id;
            conditionalTrait.OperationBlocks = new Dictionary<ConditionalTraitOperationBlock, TraitConditionLogicalOperator>();            
            conditionalTrait.trigger = TraitConditionTrigger.none;
            conditionalTrait.associatedTrait = Traits.DoubleAttack;
            conditionalTrait.classification = TraitConditionalClassification.Conditional;
            State.ConditionalTraitList.Add(conditionalTrait);
            rt.modifybtn.onClick.AddListener(() =>
            {
                ConditionalTrait.Open(rt.id);
            });
            return rt;
        }
    }

    private int FindNewId()
    {
        bool taken = true;
        int index = 0;
        while (taken)
        {
            index++;
            taken = CreatedConditionals.Any(rt => rt.id == (6000 + index));
        }
        return 6000 + index;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }
    }
}
