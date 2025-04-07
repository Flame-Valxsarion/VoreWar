using OdinSerializer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RandomizerTraitEditor : MonoBehaviour
{
    public Transform Folder;
    public GameObject RandomizerTraitPrefab;
    internal List<RandomizerTrait> RandomizerTags;
    public Button AddBtn;
    internal Button AddBtnInstance;

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
       RandomizerTags = new List<RandomizerTrait>();
        foreach (var entry in State.RandomizeLists)
        {
            var randomizerTrait = CreateRandomizerTrait(entry);
            RandomizerTags.Add(randomizerTrait);
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
            var created = CreateRandomizerTrait();
            RandomizerTags.Add(created);
            CreateAddButton();
        });
    }

    private RandomizerTrait CreateRandomizerTrait(RandomizeList savedCustom = null)
    {
        if (savedCustom != null)
        {
            var obj = Instantiate(RandomizerTraitPrefab, Folder);
            var rt = obj.GetComponent<RandomizerTrait>();
            rt.name.text = savedCustom.name;
            rt.chance.text = (savedCustom.chance * 100).ToString();
            rt.id = savedCustom.id;
            rt.level.text = savedCustom.level.ToString();
            rt.count.text = savedCustom.count.ToString();
            var ranTraits = new Dictionary<Traits, bool>();
            foreach (Traits r in State.RandomizeLists.ConvertAll(r => (Traits)r.id))
            {
                if (savedCustom.RandomTraits.Contains(r))
                    ranTraits[r] = true;
                else
                    ranTraits[r] = false;
            }
            foreach (Traits c in State.CustomTraitList.ConvertAll(r => (Traits)r.id))
            {
                if (savedCustom.RandomTraits.Contains(c))
                    ranTraits[c] = true;
                else
                    ranTraits[c] = false;
            }
            foreach (Traits cd in State.ConditionalTraitList.ConvertAll(r => (Traits)r.id))
            {
                if (savedCustom.RandomTraits.Contains(cd))
                    ranTraits[cd] = true;
                else
                    ranTraits[cd] = false;
            }
            foreach (Traits trait in (Traits[])Enum.GetValues(typeof(Traits)))
            {
                if (savedCustom.RandomTraits.Contains(trait))
                    ranTraits[trait] = true;
                else
                    ranTraits[trait] = false;
            }
            rt.TraitDictionary = ranTraits;
            rt.CloneBtn.onClick.AddListener(() =>
            {
                var clone = CreateRandomizerTrait(savedCustom);
                clone.id = FindNewId();
                clone.name.text = "new" + clone.name.text;
                RandomizerTags.Add(clone);
                CreateAddButton();
            });
            rt.RemoveBtn.onClick.AddListener(() =>
            {
                Remove(rt);
                Destroy(rt.gameObject);
            });
            return rt;
        }
        else
        {
            var newItemTemplate = Instantiate(RandomizerTraitPrefab, Folder);
            var rt = newItemTemplate.GetComponent<RandomizerTrait>();
            rt.name.text = "";
            rt.chance.text = "100";
            rt.level.text = "0";
            rt.count.text = "1";
            var last = RandomizerTags.LastOrDefault();
            rt.id = last == null ? 1001 : FindNewId();
            var ranTraits = new Dictionary<Traits, bool>();
            foreach (Traits r in State.RandomizeLists.ConvertAll(r => (Traits)r.id))
            {
                ranTraits[r] = false;
            }
            foreach (Traits c in State.CustomTraitList.ConvertAll(r => (Traits)r.id))
            {
                ranTraits[c] = false;
            }
            foreach (Traits cd in State.ConditionalTraitList.ConvertAll(r => (Traits)r.id))
            {
                ranTraits[cd] = false;
            }
            foreach (Traits trait in (Traits[])Enum.GetValues(typeof(Traits)))
            {
                ranTraits[trait] = false;
            }
            rt.TraitDictionary = ranTraits;
            rt.CloneBtn.onClick.AddListener(() =>
            {
                var clone = CreateRandomizerTrait(savedCustom);
                clone.id = FindNewId();
                clone.name.text = "new" + clone.name.text;
                RandomizerTags.Add(clone);
                CreateAddButton();
            });
            rt.RemoveBtn.onClick.AddListener(() =>
            {
                Remove(rt);
                Destroy(rt.gameObject);
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
            taken = RandomizerTags.Any(rt => rt.id == (1000 + index));
        }
        return 1000 + index;
    }

    private void Remove(RandomizerTrait rt)
    {
        foreach (Race race in (Race[])Enum.GetValues(typeof(Race)))
        {
            RaceSettingsItem item = State.RaceSettings.Get(race);
            item.RaceTraits.Remove((Traits)rt.id);
        }
        RandomizerTags.Remove(rt);
    }

    public void Persist()
    {
        List<RandomizeList> randomizeLists = new List<RandomizeList>();
        bool valid = true;
        RandomizerTags.ForEach(tag =>
        {
            if (!Validate(tag))
            {
                State.GameManager.CreateMessageBox("Saving failed: Trait with name \"" + tag.name.text + "\" is incomplete or invalid.");
                valid = false;
            }
            RandomizeList newCustom = new RandomizeList();
            newCustom.id = tag.id;
            newCustom.name = tag.name.text;
            newCustom.chance = int.Parse(tag.chance.text) /100f;
            newCustom.level = tag.level.text.Length < 1 ? 0 : int.Parse(tag.level.text);
            newCustom.count = tag.count.text.Length < 1 ? 0 : int.Parse(tag.count.text);
            newCustom.RandomTraits = new List<Traits>();
            foreach (var trait in tag.TraitDictionary)
            {
                if (trait.Value) newCustom.RandomTraits.Add(trait.Key);
            }
            randomizeLists.Add(newCustom);
        });
        if (valid)
        {
        State.RandomizeLists = new List<RandomizeList>();
        State.RandomizeLists.AddRange(randomizeLists);
        string[] printable = randomizeLists.ConvertAll(item => item.ToString()).ToArray();
        File.WriteAllLines($"{State.StorageDirectory}customTraits.txt", printable);
        }
        Close();
    }

    public bool Validate(RandomizerTrait randomizerTrait)
    {
        int res;
        if (randomizerTrait.name.text.Length < 1) return false;
        if (randomizerTrait.chance.text.Length < 1 || !int.TryParse(randomizerTrait.chance.text, out res) || res < 0) return false;
        if (!int.TryParse(randomizerTrait.level.text, out res) || res < 0) return false;
        if (randomizerTrait.TraitDictionary.Where(i => i.Value).Count() < 1) return false;
        if (RandomizerTags.Where(rt => rt.name.text == randomizerTrait.name.text).Count() > 1) return false;
        if (!int.TryParse(randomizerTrait.count.text, out res) || res > randomizerTrait.ExposeCount() + 1) return false;
        return true;
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
