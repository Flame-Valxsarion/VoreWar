using OdinSerializer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CustomTraitEditor : MonoBehaviour
{
    public Transform Folder;
    public GameObject CustomTraitListItemPrefab;
    internal List<CustomTraitListItem> CustomTags;
    public Button AddBtn;
    internal Button AddBtnInstance;

    public CustomTrait CustomTrait;

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
        CustomTags = new List<CustomTraitListItem>();
        foreach (var entry in State.CustomTraitList)
        {
            var usererTrait = CreateCustomTraitListItem(entry);
            CustomTags.Add(usererTrait);
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
            var created = CreateCustomTraitListItem();
            CustomTags.Add(created);
            CreateAddButton();
        });
    }

    private CustomTraitListItem CreateCustomTraitListItem(CustomTraitBoost savedCustom = null)
    {
        if (savedCustom != null)
        {
            var obj = Instantiate(CustomTraitListItemPrefab, Folder);
            var rt = obj.GetComponent<CustomTraitListItem>();
            rt.name.text = savedCustom.name;
            rt.id = savedCustom.id;
            rt.CloneBtn.onClick.AddListener(() =>
            {
                var clone = CreateCustomTraitListItem(savedCustom);
                clone.id = FindNewId();
                clone.name.text = "new" + clone.name.text;
                CreateAddButton();
            });
            rt.PickTagsBtn.onClick.AddListener(() =>
            {
                CustomTrait.Open(rt.id);
            });
            return rt;
        }
        else
        {
            var obj = Instantiate(CustomTraitListItemPrefab, Folder);
            var rt = obj.GetComponent<CustomTraitListItem>();
            rt.name.text = "CustomTrait";
            var last = CustomTags.LastOrDefault();
            rt.id = last == null ? 3001 : FindNewId();
            CustomTraitBoost customTrait = new CustomTraitBoost();
            customTrait.name = "CustomTrait";
            customTrait.id = rt.id;
            customTrait.comps = new Dictionary<CustomTraitComp, float>();
            customTrait.tags = new List<string>();
            customTrait.tier = TraitTier.Neutral;
            State.CustomTraitList.Add(customTrait);
            rt.CloneBtn.onClick.AddListener(() =>
            {
                var clone = CreateCustomTraitListItem(savedCustom);
                clone.id = FindNewId();
                clone.name.text = "new" + clone.name.text;
                CustomTags.Add(clone);
                CreateAddButton();
            });
            rt.PickTagsBtn.onClick.AddListener(() =>
            {
                CustomTrait.Open(rt.id);
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
            taken = CustomTags.Any(rt => rt.id == (3000 + index));
        }
        return 3000 + index;
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
