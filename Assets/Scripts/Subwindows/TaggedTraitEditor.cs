using OdinSerializer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class TaggedTraitEditor : MonoBehaviour
{
    public Transform Folder;
    public Transform AddedFolder;
    public Text raceName;
    public GameObject TaggedTraitPrefab;
    public InputField NameSearch;
    public InputField TagSearch;
    public List<TaggedTrait> AllTaggedTraits;
    List<Traits> CurrentTraits;
    Dictionary<TraitTier, bool> TierSelect;
    RaceSettingsItem item;

    internal void Open(Race race, List<Traits> incomingtraits)
    {
        gameObject.SetActive(true);

        AllTaggedTraits = new List<TaggedTrait>();
        TierSelect = new Dictionary<TraitTier, bool>();
        item = State.RaceSettings.Get(race);
        CurrentTraits = incomingtraits;
        raceName.text = item.SpawnRace.ToString();
        NameSearch.onValueChanged.AddListener((x) =>
        {
            int children = Folder.childCount;
            for (int i = children - 1; i >= 0; i--)
            {
                var obj = Folder.GetChild(i).gameObject;
                var tt = obj.GetComponent<TaggedTraitPrefab>();
                tt.gameObject.SetActive(ShouldBeActive(tt.taggedTrait, false));
            }
            children = AddedFolder.childCount;
            for (int i = children - 1; i >= 0; i--)
            {
                var obj = AddedFolder.GetChild(i).gameObject;
                var tt = obj.GetComponent<TaggedTraitPrefab>();
                tt.gameObject.SetActive(ShouldBeActive(tt.taggedTrait, true));
            }
        });
        TagSearch.onValueChanged.AddListener((x) =>
        {
            int children = Folder.childCount;
            for (int i = children - 1; i >= 0; i--)
            {
                var obj = Folder.GetChild(i).gameObject;
                var tt = obj.GetComponent<TaggedTraitPrefab>();
                tt.gameObject.SetActive(ShouldBeActive(tt.taggedTrait, false));
            }
            children = AddedFolder.childCount;
            for (int i = children - 1; i >= 0; i--)
            {
                var obj = AddedFolder.GetChild(i).gameObject;
                var tt = obj.GetComponent<TaggedTraitPrefab>();
                tt.gameObject.SetActive(ShouldBeActive(tt.taggedTrait, true));
            }
        });
        Setup();
    }

    private void CleanFolders()
    {
        AllTaggedTraits.Clear();
        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }
        children = AddedFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(AddedFolder.GetChild(i).gameObject);
        }
    }
    private void Setup()
    {
        CleanFolders();
        foreach (RandomizeList rl in State.RandomizeLists)
        {
            TaggedTrait traitfromrl = new TaggedTrait();
            traitfromrl.name = rl.name;
            List<string> tags = new List<string>();
            TraitTier tier = TraitTier.Harmful;
            foreach (Traits t in rl.RandomTraits)
            {
                TaggedTrait curr = State.TieredTraitsList[t];
                if (curr.tierValue > tier)
                {
                    tier = curr.tierValue;
                }
                foreach (string tag in curr.tags)
                {
                    if (!tags.Contains(tag))
                    {
                        tags.Add(tag);
                    }
                }
            }
            traitfromrl.tierValue = tier;
            traitfromrl.tier = traitfromrl.tierValue.ToString();
            traitfromrl.tags = tags;
            traitfromrl.tags.Add("Random Trait");
            traitfromrl.traitEnum = (Traits)rl.id;
            AllTaggedTraits.Add(traitfromrl);
        }
        foreach (CustomTraitBoost ct in State.CustomTraitList)
        {
            TaggedTrait traitfromrcustom = new TaggedTrait();
            traitfromrcustom.name = ct.name;
            traitfromrcustom.tierValue = ct.tier;
            traitfromrcustom.tier = traitfromrcustom.tierValue.ToString();
            traitfromrcustom.tags = new List<string>();
            foreach (string tag in ct.tags)
            {
                traitfromrcustom.tags.Add(tag);
            }
            traitfromrcustom.tags.Add("Custom Trait");
            traitfromrcustom.traitEnum = (Traits)ct.id;
            AllTaggedTraits.Add(traitfromrcustom);
        }
        foreach (TaggedTrait trait in State.TieredTraitsList.Values)
        {
            AllTaggedTraits.Add(trait);
        }
        foreach (ConditionalTraitContainer ct in State.ConditionalTraitList)
        {
            TaggedTrait traitfromrcustom = new TaggedTrait();
            traitfromrcustom.name = ct.name;
            traitfromrcustom.tierValue = AllTaggedTraits.Where(x=> x.traitEnum == ct.associatedTrait).First().tierValue;
            traitfromrcustom.tier = AllTaggedTraits.Where(x => x.traitEnum == ct.associatedTrait).First().tier;
            traitfromrcustom.tags = new List<string>();
            foreach (string tag in AllTaggedTraits.Where(x => x.traitEnum == ct.associatedTrait).First().tags)
            {
                traitfromrcustom.tags.Add(tag);
            }
            traitfromrcustom.tags.Add("Conditional Trait");
            traitfromrcustom.traitEnum = (Traits)ct.id;
            AllTaggedTraits.Add(traitfromrcustom);
        }
        foreach (TaggedTrait trait in AllTaggedTraits)
        {
            CreateAddedTaggedTrait(trait);
            CreateUnaddedTaggedTrait(trait);
        }
    }

    private void CreateUnaddedTaggedTrait(TaggedTrait tagged = null)
    {
        var obj = Instantiate(TaggedTraitPrefab, Folder);
        var tt = obj.GetComponent<TaggedTraitPrefab>();
        tt.traitName.text = tagged.name;
        tt.traitTier.text = tagged.tier;
        tt.tags.text = "";
        tt.id = Folder.childCount - 1;
        tt.description.text = HoveringTooltip.GetTraitData(tagged.traitEnum);
        foreach (string tag in tagged.tags)
        {
            tt.tags.text += tag;
            tt.tags.text += "\n";
        }
        tt.taggedTrait = tagged;
        tt.removeButton.gameObject.SetActive(false);
        tt.addButton.gameObject.SetActive(true);
        tt.addButton.onClick.AddListener(() =>
        {
            CurrentTraits.Add(tt.taggedTrait.traitEnum);
            AddedFolder.GetChild(tt.id).gameObject.SetActive(true);
            tt.gameObject.SetActive(ShouldBeActive(tagged, false));
            //UnityEngine.Debug.Log("Added " + tt.trait + " to " + item.SpawnRace);
        });
        tt.gameObject.SetActive(ShouldBeActive(tagged, false));
        if (tagged.traitEnum >= (Traits)1000)
            tt.edit.gameObject.SetActive(false);
    }

    private void CreateAddedTaggedTrait(TaggedTrait tagged = null)
    {
        var obj = Instantiate(TaggedTraitPrefab, AddedFolder);
        var tt = obj.GetComponent<TaggedTraitPrefab>();
        tt.traitName.text = tagged.name;
        tt.traitTier.text = tagged.tier;
        tt.tags.text = "";
        tt.id = AddedFolder.childCount - 1;
        tt.description.text = HoveringTooltip.GetTraitData(tagged.traitEnum);
        foreach (string tag in tagged.tags)
        {
            tt.tags.text += tag;
            tt.tags.text += "\n";
        }
        tt.taggedTrait = tagged;
        tt.removeButton.gameObject.SetActive(true);
        tt.addButton.gameObject.SetActive(false);
        tt.removeButton.onClick.AddListener(() =>
        {
            CurrentTraits.Remove(tt.taggedTrait.traitEnum);
            Folder.GetChild(tt.id).gameObject.SetActive(true);
            tt.gameObject.SetActive(ShouldBeActive(tagged, true));
        });
        tt.gameObject.SetActive(ShouldBeActive(tagged, true));
        if (tagged.traitEnum >= (Traits)1000)
            tt.edit.gameObject.SetActive(false);
    }

    private bool ShouldBeActive(TaggedTrait checkedTrait, bool addedCol)
    {
        if (CurrentTraits.Contains(checkedTrait.traitEnum) && !addedCol)
        {
            return false;
        }
        if (!CurrentTraits.Contains(checkedTrait.traitEnum) && addedCol)
        {
            return false;
        }
        if (!checkedTrait.name.ToLower().Contains(NameSearch.text.ToLower()))
        {
            return false;
        }
        if (TagSearch.text.IsNullOrWhitespace())
        {
            return true;
        }
        foreach (string tag in checkedTrait.tags)
        {
            if (tag.ToLower().Contains(TagSearch.text.ToLower()))
            {
                return true;
            }
        }
        return false;
    }

    public List<Traits> Close()
    {
        NameSearch.text = string.Empty;
        TagSearch.text = string.Empty;
        gameObject.SetActive(false);
        CleanFolders();
        return CurrentTraits;
    }


}
