using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaggedTraitPrefab : MonoBehaviour
{
    public int id;
    public Text traitName;
    public Text traitTier;
    public Text description;
    public Text tags;

    public Button addButton;
    public Button removeButton;

    public Button edit;
    public Button save;
    public Button discard;
    
    public InputField tagInput;
    public TMP_Dropdown tier;

    internal TaggedTrait taggedTrait;

    string oldTags;
    string oldTier;

    bool addstatus;
    bool removestatus;

    public void EditTaggedTrait()
    {
        oldTags = tags.text;
        oldTier = traitTier.text;

        save.gameObject.SetActive(true);
        discard.gameObject.SetActive(true);
        tagInput.gameObject.SetActive(true);
        tier.gameObject.SetActive(true);

        addstatus = addButton.gameObject.activeInHierarchy;
        removestatus = removeButton.gameObject.activeInHierarchy;

        edit.gameObject.SetActive(false);
        addButton.gameObject.SetActive(false);
        removeButton.gameObject.SetActive(false);

        tagInput.text = "";
        foreach (string item in tags.text.Split('\n'))
        {
            if (item.Length >= 1)
            {
                tagInput.text += item;
                tagInput.text += ",";
            }
        }
        if (!(tier.value == (int)TraitTier.Hidden))
        {
            tier.value = (int)Enum.Parse(typeof(TraitTier), traitTier.text);
        }
    }
    public void SaveTaggedTraitEdit()
    {
        save.gameObject.SetActive(false);
        discard.gameObject.SetActive(false);
        tagInput.gameObject.SetActive(false);
        tier.gameObject.SetActive(false);

        edit.gameObject.SetActive(true);
        addButton.gameObject.SetActive(addstatus);
        removeButton.gameObject.SetActive(removestatus);

        List<string> newTags = new List<string>();
        tags.text = "";
        foreach (var item in tagInput.text.Split(','))
        {
            newTags.Add(item);
            if (item.Length > 0)
            {
                if (item.Length >= 1)
                {
                    tags.text += item.Trim();
                    tags.text += "\n";
                }
            }
        }


        if (!((int)Enum.Parse(typeof(TraitTier), traitTier.text) == (int)TraitTier.Hidden))
        {
            traitTier.text = ((TraitTier)tier.value).ToString();
        }

        string json = File.ReadAllText(State.StorageDirectory + "\\taggedTraits.json");
        var rootObject = new RootObject();
        JsonConvert.PopulateObject(json, rootObject);
        TaggedTraitTempClass toEdit = rootObject.traits.Where(tt => tt.name == traitName.text).FirstOrDefault();
        toEdit.tags = newTags;
        toEdit.tier = traitTier.text;
        using (StreamWriter file = File.CreateText(State.StorageDirectory + "\\taggedTraits.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, rootObject);
        }

    }

    public void DiscardChanges()
    {
        tags.text = oldTags;
        traitTier.text = oldTier;
        save.gameObject.SetActive(false);
        discard.gameObject.SetActive(false);
        tagInput.gameObject.SetActive(false);
        tier.gameObject.SetActive(false);

        edit.gameObject.SetActive(true);
        addButton.gameObject.SetActive(addstatus);
        removeButton.gameObject.SetActive(removestatus);
    }

    class RootObject
    {
        public List<TaggedTraitTempClass> traits { get; set; }
    }
    class TaggedTraitTempClass
    {
        public string name { get; set; }
        public string tier { get; set; }
        public List<string> tags { get; set; }
    }
}