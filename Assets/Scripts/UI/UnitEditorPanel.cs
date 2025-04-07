using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitEditorPanel : CustomizerPanel
{
    public TMP_Dropdown RaceDropdown;
    public TMP_Dropdown TraitDropdown;
    public TMP_Dropdown[] ItemDropdown;
    public TMP_Dropdown[] SpellDropdown;
    public TMP_Dropdown AlignmentDropdown;
    public Toggle HiddenToggle;
    public UnitInfoPanel InfoPanel;
    public TextMeshProUGUI TraitList;
    public Slider ExpBar;
    public Slider HealthBar;
    public Slider BarrierBar;
    public Slider ManaBar;
    public EditStatButton EditStatButtonPrefab;
    public GameObject StatButtonPanel;
    internal UnitEditor UnitEditor;



    Dictionary<Race, int> raceDict;
    Dictionary<Traits, int> traitDict;
    Dictionary<int, string> itemDict;
    Dictionary<string, int> itemReverseDict;
    Dictionary<int, Empire> empireDict;

    public TMP_InputField TraitsText;

    public Button SwapAlignment;


    protected EditStatButton[] buttons;


    void SetUpRaces()
    {
        raceDict = new Dictionary<Race, int>();
        int val = 0;
        foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).Where(s => (int)s >= 0).OrderBy((s) => s.ToString()))
        {
            raceDict[race] = val;
            val++;
            RaceDropdown.options.Add(new TMP_Dropdown.OptionData(race.ToString()));
        }
        buttons = new EditStatButton[10];
        foreach (Stat stat in (Stat[])Enum.GetValues(typeof(Stat)))
        {
            if (stat == Stat.None)
                break;
            buttons[(int)stat] = CreateNewButton(stat, UnitEditor.ChangeStat, UnitEditor.ChangeLevel, ManualChangeStat);
        }

        traitDict = new Dictionary<Traits, int>();
        int val2 = 0;
        foreach (RandomizeList rl in State.RandomizeLists)
        {
            traitDict[(Traits)rl.id] = val2;
            val2++;
            TraitDropdown.options.Add(new TMP_Dropdown.OptionData(rl.name.ToString()));
        }
        foreach (CustomTraitBoost ct in State.CustomTraitList)
        {
            traitDict[(Traits)ct.id] = val2;
            val2++;
            TraitDropdown.options.Add(new TMP_Dropdown.OptionData(ct.name.ToString()));
        }
        foreach (ConditionalTraitContainer cdt in State.ConditionalTraitList)
        {
            traitDict[(Traits)cdt.id] = val2;
            val2++;
            TraitDropdown.options.Add(new TMP_Dropdown.OptionData(cdt.name.ToString()));
        }
        foreach (Traits traitId in ((Traits[])Enum.GetValues(typeof(Traits))).OrderBy(s =>
       {
           return s >= Traits.LightningSpeed ? "ZZZ" + s.ToString() : s.ToString();
       }))
        {
            traitDict[traitId] = val2;
            val2++;
            TraitDropdown.options.Add(new TMP_Dropdown.OptionData(traitId.ToString()));
        }
        itemDict = new Dictionary<int, string>();
        itemReverseDict = new Dictionary<string, int>();
        itemDict[0] = "Empty";
        var AllItems = State.World.ItemRepository.GetAllItems();
        for (int j = 0; j < ItemDropdown.Length; j++)
        {
            ItemDropdown[j].options.Add(new TMP_Dropdown.OptionData("Empty"));
        }
        for (int i = 0; i < AllItems.Count; i++)
        {
            for (int j = 0; j < ItemDropdown.Length; j++)
            {
                ItemDropdown[j].options.Add(new TMP_Dropdown.OptionData(AllItems[i].Name));
            }
            itemDict[i + 1] = AllItems[i].Name;
            itemReverseDict[AllItems[i].Name] = 1 + i;
        }
        for (int i = 0; i < SpellDropdown.Length; i++)
        {
            foreach (SpellTypes type in ((SpellTypes[])Enum.GetValues(typeof(SpellTypes))).Where(s => (int)s < 100))
            {
                SpellDropdown[i].options.Add(new TMP_Dropdown.OptionData(type.ToString()));
            }
            SpellDropdown[i].RefreshShownValue();
        }
        SetupAllignment();
    }

    private void SetupAllignment()
    {
        empireDict = new Dictionary<int, Empire>();
        AlignmentDropdown.options.Add(new TMP_Dropdown.OptionData("Default"));
        if (State.World?.MainEmpires != null)
        {
            var mainEmps = State.World.MainEmpires;
            for (int i = 0; i < mainEmps.Count; i++)
            {
                if (mainEmps[i].Side >= 700)
                    continue;
                AlignmentDropdown.options.Add(new TMP_Dropdown.OptionData(mainEmps[i].Name));
                empireDict[i + 1] = mainEmps[i];
            }

            if (State.World.MonsterEmpires != null)
            {
                var monsterEmps = State.World.MonsterEmpires;
                for (int i = 0; i < monsterEmps.Count(); i++)
                {
                    if (monsterEmps[i].Side >= 700)
                        continue;
                    AlignmentDropdown.options.Add(new TMP_Dropdown.OptionData(monsterEmps[i].Name));
                    empireDict[i + mainEmps.Count - 1] = monsterEmps[i];
                }
            }
        }
        else
        {
            AlignmentDropdown.options.Add(new TMP_Dropdown.OptionData("Defender"));
            AlignmentDropdown.options.Add(new TMP_Dropdown.OptionData("Attacker"));
        }
        AlignmentDropdown.RefreshShownValue();
    }

    EditStatButton CreateNewButton(Stat stat, Action<Stat, int> statAction, Action<Stat, int> levelAction, Action<Stat, int> manualSetAction)
    {
        EditStatButton button = Instantiate(EditStatButtonPrefab, StatButtonPanel.transform).GetComponent<EditStatButton>();
        button.SetData(stat, UnitEditor.Unit.GetStat(stat), statAction, levelAction, manualSetAction, UnitEditor.Unit, this);
        return button;
    }

    public void ChangeUnitButtons(Unit unit)
    {
        foreach (EditStatButton button in buttons)
        {
            //This if condition serves to fix a bug where using the stat change buttons would cause an exception were a button in-code was not assigned to a GameObject.
            if (button)
                button.Unit = unit;
        }
    }

    public void UpdateButtons()
    {
        UnitEditor.RefreshStats();
        foreach (EditStatButton button in buttons)
        {
            //This if condition serves to fix a bug where using the stat change buttons would cause an exception were a button in-code was not assigned to a GameObject.
            if (button)
                button.UpdateLabel();
        }
    }

    public void Open(Actor_Unit actor)
    {
        gameObject.SetActive(true);
        if (UnitEditor == null)
        {
            InfoPanel.ExpBar = ExpBar;
            InfoPanel.HealthBar = HealthBar;
            InfoPanel.BarrierBar = BarrierBar;
            InfoPanel.ManaBar = ManaBar;
            UnitEditor = new UnitEditor(actor, this, InfoPanel);
            SetUpRaces();
        }
        else
        {
            UnitEditor.SetActor(actor);
            UnitEditor.RefreshStats();
        }
        if (raceDict.TryGetValue(actor.Unit.Race, out int race))
        {
            RaceDropdown.value = race;
        }
        else
            RaceDropdown.value = 0;
        RaceDropdown.captionText.text = actor.Unit.Race.ToString();
        AlignmentDropdown.captionText.text = DetermineAllignment(actor.Unit);
        HiddenToggle.isOn = actor.Unit.hiddenFixedSide;
        PopulateItems();
        TraitList.text = UnitEditor.Unit.ListTraits();
        SwapAlignment.gameObject.SetActive(State.GameManager.CurrentScene == State.GameManager.TacticalMode);
        ChangeUnitButtons(actor.Unit);
        UpdateButtons();

    }

    private string DetermineAllignment(Unit unit)
    {
        if (!unit.HasFixedSide())
            return "Default";
        if (State.World?.MainEmpires != null)
        {
            return State.World.GetEmpireOfSide(unit.FixedSide)?.Name ?? unit.Race.ToString();
        }
        else
            return unit.FixedSide == State.GameManager.TacticalMode.GetDefenderSide() ? "Defender" : "Attacker";
    }

    public void Open(Unit unit)
    {
        gameObject.SetActive(true);
        if (UnitEditor == null)
        {
            InfoPanel.ExpBar = ExpBar;
            InfoPanel.HealthBar = HealthBar;
            InfoPanel.BarrierBar = BarrierBar;
            InfoPanel.ManaBar = ManaBar;
            UnitEditor = new UnitEditor(unit, this, InfoPanel);
            SetUpRaces();
        }
        else
        {
            UnitEditor.SetUnit(unit);
            UnitEditor.RefreshStats();
        }
        if (raceDict.TryGetValue(unit.Race, out int race))
        {
            RaceDropdown.value = race;
        }
        else
            RaceDropdown.value = 0;
        AlignmentDropdown.captionText.text = DetermineAllignment(unit);
        HiddenToggle.isOn = unit.hiddenFixedSide;
        PopulateItems();
        TraitList.text = UnitEditor.Unit.ListTraits();
        SwapAlignment.gameObject.SetActive(State.GameManager.CurrentScene == State.GameManager.TacticalMode);
        ChangeUnitButtons(unit);
        UpdateButtons();
    }

    public void ClearStatus()
    {
        UnitEditor.ClearStatus();
    }

    public void Close()
    {
        for (int i = 0; i < SpellDropdown.Length; i++)
        {
            int spellIndex = SpellDropdown[i].value;
            SpellTypes spell = (SpellTypes)Enum.GetValues(typeof(SpellTypes)).GetValue(spellIndex);
            //if (spell > SpellTypes.Resurrection)
            //    spell = spell - SpellTypes.Resurrection + SpellTypes.AlraunePuff - 1;
            if (spell != SpellTypes.None)
            {
                if (UnitEditor.Unit.InnateSpells.Count > i)
                    UnitEditor.Unit.InnateSpells[i] = (spell);
                else if (!UnitEditor.Unit.InnateSpells.Contains(spell))
                    UnitEditor.Unit.InnateSpells.Add(spell);
            }
            else if (UnitEditor.Unit.InnateSpells.Count > i)
            {
                UnitEditor.Unit.InnateSpells.RemoveAt(i);

            }
        }
        UnitEditor.Unit.UpdateSpells();
        gameObject.SetActive(false);
        if (State.GameManager.CurrentScene == State.GameManager.Recruit_Mode)
        {
            State.GameManager.Recruit_Mode.ButtonCallback(10);
        }
        else if (State.GameManager.CurrentScene == State.GameManager.TacticalMode)
        {
            State.GameManager.TacticalMode.RebuildInfo();
            State.GameManager.TacticalMode.UpdateHealthBars();
        }
        var ownerEmp = State.World.GetEmpireOfSide(UnitEditor.Unit.Side);
        if (ownerEmp != null && ownerEmp.StrategicAI != null)
        {
            StrategicUtilities.SetAIClass(UnitEditor.Unit);
        }

    }

    public override void ChangeGender()
    {
        UnitEditor?.ChangeGender();
    }

    public override void ChangePronouns()
    {
        UnitEditor?.ChangePronouns();
    }

    public void ChangeRace()
    {
        if (UnitEditor.Unit == null)
            return;
        if (Enum.TryParse(RaceDropdown.options[RaceDropdown.value].text, out Race race))
        {
            if (UnitEditor.Unit.Race == race)
                return;
            UnitEditor.Unit.Race = race;
            UnitEditor.Unit.RandomizeGender(race, Races.GetRace(UnitEditor.Unit));
            UnitEditor.Unit.SetGear(race);
            UnitEditor.ClearAnimations();
            RandomizeUnit();
            UnitEditor.Unit.ReloadTraits();
            UnitEditor.Unit.InitializeTraits();
            UnitEditor.RefreshActor();
            PopulateItems();
            TraitList.text = UnitEditor.Unit.ListTraits();
        }
    }

    public void ChangeSide()
    {
        UnitEditor.ChangeSide();
    }

    public void PopulateItems()
    {
        var AllItems = State.World.ItemRepository.GetAllItems();
        int maxIndex = Math.Min(ItemDropdown.Length, UnitEditor.Unit.Items.Length);
        for (int i = 0; i < ItemDropdown.Length; i++)
        {
            ItemDropdown[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < maxIndex; i++)
        {
            ItemDropdown[i].gameObject.SetActive(true);
            if (UnitEditor.Unit.Items[i] == null)
            {
                ItemDropdown[i].value = 0;
                ItemDropdown[i].captionText.text = "Empty";
                continue;
            }
            int value = 0;
            for (int j = 0; j < AllItems.Count; j++)
            {
                if (AllItems[j].Name == UnitEditor.Unit.Items[i].Name)
                {
                    value = j;
                    break;
                }
            }
            ItemDropdown[i].value = value + 1;

        }
        for (int i = 0; i < SpellDropdown.Length; i++)
        {
            if (UnitEditor.Unit.InnateSpells == null)
                UnitEditor.Unit.InnateSpells = new List<SpellTypes>();
            if (UnitEditor.Unit.InnateSpells.Count > i)
            {
                var value = Array.IndexOf(Enum.GetValues(typeof(SpellTypes)), UnitEditor.Unit.InnateSpells[i]);
                //if (value > SpellTypes.Resurrection)
                //    value = value - SpellTypes.AlraunePuff + SpellTypes.Resurrection + 1;
                SpellDropdown[i].value = (int)value;
            }
            else
                SpellDropdown[i].value = 0;
            SpellDropdown[i].RefreshShownValue();
        }

    }

    public void ChangeItem(int slot)
    {
        if (UnitEditor.Unit == null)
            return;
        Item newItem = null;

        if (itemDict.TryGetValue(ItemDropdown[slot].value, out string value))
        {
            newItem = State.World.ItemRepository.GetAllItems().Where(s => s.Name == value).FirstOrDefault();
        }


        UnitEditor.ChangeItem(slot, newItem);
        UnitEditor.RefreshView();

    }

    public void ChangeAlignment()
    {
        if (UnitEditor.Unit == null)
            return;

        if (AlignmentDropdown.options[AlignmentDropdown.value].text == "Default")
            UnitEditor.Unit.FixedSide = -1;
        else if (AlignmentDropdown.options[AlignmentDropdown.value].text == "Defender")
            UnitEditor.Unit.FixedSide = State.GameManager.TacticalMode.GetDefenderSide();
        else if (AlignmentDropdown.options[AlignmentDropdown.value].text == "Attacker")
            UnitEditor.Unit.FixedSide = State.GameManager.TacticalMode.GetAttackerSide();
        else if (State.World?.MainEmpires != null)
        {
            UnitEditor.Unit.FixedSide = empireDict[AlignmentDropdown.value].Side;
        }
        ToggleHidden();
    }

    public void ToggleHidden()
    {
        if (UnitEditor.Unit == null)
            return;
        UnitEditor.Unit.hiddenFixedSide = HiddenToggle.isOn;
        UnitEditor.RefreshView();
    }

    public void RandomizeUnit()
    {
        Races.GetRace(UnitEditor.Unit).RandomCustom(UnitEditor.Unit);
        UnitEditor.RefreshView();
        UnitEditor.RefreshGenderSelector();
    }

    public void AddTrait()
    {
        if (UnitEditor.Unit == null)
            return;
        if (State.RandomizeLists.Any(rl => rl.name == TraitDropdown.options[TraitDropdown.value].text))
        {
            RandomizeList randomizeList = State.RandomizeLists.Single(rl => rl.name == TraitDropdown.options[TraitDropdown.value].text);
             if (randomizeList.level > UnitEditor.Unit.Level)
                {
                    UnitEditor.Unit.AddPermanentTrait((Traits)randomizeList.id);
                } else
                {
                    var resTraits = UnitEditor.Unit.RandomizeOne(randomizeList);
                    foreach (Traits resTrait in resTraits)
                    {
                        UnitEditor.AddTrait(resTrait);
                        if (resTrait == Traits.Resourceful || resTrait == Traits.BookWormI || resTrait == Traits.BookWormII || resTrait == Traits.BookWormIII)
                        {
                            UnitEditor.Unit.SetMaxItems();
                            PopulateItems();
                        }
                    }
                }
            UnitEditor.RefreshActor();
            TraitList.text = UnitEditor.Unit.ListTraits();
        }
        if (State.CustomTraitList.Any(ct => ct.name == TraitDropdown.options[TraitDropdown.value].text))
        {
            CustomTraitBoost customTrait = State.CustomTraitList.Single(ct => ct.name == TraitDropdown.options[TraitDropdown.value].text);
            UnitEditor.Unit.AddPermanentTrait((Traits)customTrait.id);
            UnitEditor.RefreshActor();
            TraitList.text = UnitEditor.Unit.ListTraits();
        }
        if (State.ConditionalTraitList.Any(ct => ct.name == TraitDropdown.options[TraitDropdown.value].text))
        {
            ConditionalTraitContainer conditionalTrait = State.ConditionalTraitList.Single(ct => ct.name == TraitDropdown.options[TraitDropdown.value].text);
            UnitEditor.Unit.AddPermanentTrait((Traits)conditionalTrait.id);
            UnitEditor.RefreshActor();
            TraitList.text = UnitEditor.Unit.ListTraits();
        }
        if (Enum.TryParse(TraitDropdown.options[TraitDropdown.value].text, out Traits trait))
        {
            UnitEditor.AddTrait(trait);
            if (trait == Traits.Resourceful || trait == Traits.BookWormI || trait == Traits.BookWormII || trait == Traits.BookWormIII)
            {
                UnitEditor.Unit.SetMaxItems();
                PopulateItems();
            }
            UnitEditor.RefreshActor();
            TraitList.text = UnitEditor.Unit.ListTraits();

        }
    }

    public void ManualChangeStat(Stat stat, int dummy)
    {
        if (UnitEditor.Unit == null)
            return;
        var input = Instantiate(State.GameManager.InputBoxPrefab).GetComponent<InputBox>();
        input.SetData((s) =>
        {
            UnitEditor.Unit.ModifyStat(stat, s - UnitEditor.Unit.GetStatBase(stat));
            UnitEditor.RefreshStats();
            UpdateButtons();
        }, "Change", "Cancel", $"Modify {stat}?", 6);
    }

    public void AddTraitsText()
    {
        if (UnitEditor.Unit == null)
            return;
        foreach (RandomizeList rl in (State.RandomizeLists))
        {
            if (TraitsText.text.ToLower().Contains(rl.name.ToString().ToLower()))
            {
                var resTraits = UnitEditor.Unit.RandomizeOne(rl);
                foreach (Traits resTrait in resTraits)
                {
                    UnitEditor.AddTrait(resTrait);
                    if (resTrait == Traits.Resourceful || resTrait == Traits.BookWormI || resTrait == Traits.BookWormII || resTrait == Traits.BookWormIII)
                    {
                        UnitEditor.Unit.SetMaxItems();
                        PopulateItems();
                    }
                }
                UnitEditor.RefreshActor();
                TraitList.text = UnitEditor.Unit.ListTraits();

            }
        }
        foreach (CustomTraitBoost ct in (State.CustomTraitList))
        {
            if (TraitsText.text.ToLower().Contains(ct.name.ToString().ToLower()))
            {
                UnitEditor.AddTrait((Traits)ct.id);

                UnitEditor.RefreshActor();
                TraitList.text = UnitEditor.Unit.ListTraits();

            }
        }
        foreach (ConditionalTraitContainer cdt in (State.ConditionalTraitList))
        {
            if (TraitsText.text.ToLower().Contains(cdt.name.ToString().ToLower()))
            {
                UnitEditor.AddTrait((Traits)cdt.id);

                UnitEditor.RefreshActor();
                TraitList.text = UnitEditor.Unit.ListTraits();

            }
        }
        foreach (Traits trait in (Stat[])Enum.GetValues(typeof(Traits)))
        {
            if (TraitsText.text.ToLower().Contains(trait.ToString().ToLower()))
            {
                UnitEditor.AddTrait(trait);
                if (trait == Traits.Resourceful || trait == Traits.BookWormI || trait == Traits.BookWormII || trait == Traits.BookWormIII)
                {
                    UnitEditor.Unit.SetMaxItems();
                    PopulateItems();
                }
                UnitEditor.RefreshActor();
                TraitList.text = UnitEditor.Unit.ListTraits();
            }
        }

    }

    public void RemoveTrait()
    {
        if (UnitEditor.Unit == null)
            return;

        RandomizeList rList = State.RandomizeLists.Where(rl => rl.name == TraitDropdown.options[TraitDropdown.value].text).FirstOrDefault();
        if (rList != null)
        {
            UnitEditor.RemoveTrait((Traits)rList.id);
            UnitEditor.RefreshActor();
            TraitList.text = UnitEditor.Unit.ListTraits();
            if ((Traits)rList.id == Traits.Resourceful)
            {
                UnitEditor.Unit.SetMaxItems();
                PopulateItems();
            }
        }
        
        CustomTraitBoost cBoost = State.CustomTraitList.Where(ct => ct.name == TraitDropdown.options[TraitDropdown.value].text).FirstOrDefault();
        if (cBoost != null)
        {
            UnitEditor.RemoveTrait((Traits)cBoost.id);
            UnitEditor.RefreshActor();
            TraitList.text = UnitEditor.Unit.ListTraits();
        }
        
        ConditionalTraitContainer condCont = State.ConditionalTraitList.Where(ct => ct.name == TraitDropdown.options[TraitDropdown.value].text).FirstOrDefault();
        if (condCont != null)
        {
            UnitEditor.RemoveTrait((Traits)condCont.id);
            UnitEditor.RefreshActor();
            TraitList.text = UnitEditor.Unit.ListTraits();
        }

        if (Enum.TryParse(TraitDropdown.options[TraitDropdown.value].text, out Traits trait))
        {
            UnitEditor.RemoveTrait(trait);
            UnitEditor.RefreshActor();
            TraitList.text = UnitEditor.Unit.ListTraits();
            if (trait == Traits.Resourceful)
            {
                UnitEditor.Unit.SetMaxItems();
                PopulateItems();
            }
        }
    }

    public void RestoreHealth()
    {
        if (UnitEditor.Unit == null)
            return;
        UnitEditor.RestoreHealth();
    }

    public void RestoreMana()
    {
        if (UnitEditor.Unit == null)
            return;
        UnitEditor.RestoreMana();
    }

    public void RestoreMovement()
    {
        if (UnitEditor.Unit == null)
            return;
        UnitEditor.RestoreMovement();
    }

    public void ShowLevelSetter()
    {
        var inputBox = Instantiate(State.GameManager.InputBoxPrefab).GetComponent<InputBox>();

        inputBox.SetData(UnitEditor.SetLevelTo, "Update Level", "Cancel", "Use this to set the units current level, automatically applying level ups or downs", 5);
    }

}

