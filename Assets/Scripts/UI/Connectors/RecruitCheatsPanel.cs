using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecruitCheatsPanel : MonoBehaviour
{
    public TMP_Dropdown ArmyPicker;
    public TMP_Dropdown TraitPicker;

    public Button SwapArmyButton;
    public Button AddTraitButton;
    public Button RemoveTraitButton;

    public Button RefreshButton;

    bool init = false;

    Army Army;

    internal void Setup(Army army)
    {
        Army = army;
        if (init == false)
        {
            foreach (Traits traitId in ((Traits[])Enum.GetValues(typeof(Traits))).OrderBy(s =>
            {
                return s >= Traits.LightningSpeed ? "ZZZ" + s.ToString() : s.ToString();
            }))
            {
                TraitPicker.options.Add(new TMP_Dropdown.OptionData(traitId.ToString()));
            }
            TraitPicker.RefreshShownValue();
            SwapArmyButton.onClick.AddListener(MoveToAnotherEmpire);
            AddTraitButton.onClick.AddListener(AddTrait);
            RemoveTraitButton.onClick.AddListener(RemoveTrait);
            RefreshButton.onClick.AddListener(Refresh);
            init = true;
        }


        ArmyPicker.ClearOptions();

        foreach (Empire empire in State.World.MainEmpires.Where(s => s.KnockedOut == false))
        {
            ArmyPicker.options.Add(new TMP_Dropdown.OptionData(empire.Name));
        }
        ArmyPicker.RefreshShownValue();
    }

    void Refresh()
    {
        Army.Refresh();
    }

    void MoveToAnotherEmpire()
    {
        if (StrategicUtilities.GetVillageAt(Army.Position) != null)
        {
            State.GameManager.CreateMessageBox("Can't switch sides in villages, it generates bugs.");
            return;
        }
        if (State.World.MainEmpires.Where(s => s.Name == ArmyPicker.captionText.text).Any() == false)
        {
            State.GameManager.CreateMessageBox("Invalid Empire, try repicking from the dropdown.");
            return;
        }
        var emp = State.World.MainEmpires.Where(s => s.Name == ArmyPicker.captionText.text).First();
        if (Army.Units.Where(s => s.Type == UnitType.Leader).Any())
        {
            State.GameManager.CreateMessageBox("That army had a leader in it, unexpected behavior may occur when the leader dies.");
        }
        State.GameManager.Recruit_Mode.ButtonCallback(86);
        State.GameManager.SwitchToStrategyMode();
        Army.Units.ForEach(unit =>
        {
            unit.Side = emp.Side;
        });
        State.GameManager.StrategyMode.RedrawArmies();
    }

    void AddTrait()
    {
        foreach (var unit in Army.Units)
        {
            if (State.RandomizeLists.Any(rl => rl.name == TraitPicker.captionText.text))
            {
                RandomizeList randomizeList = State.RandomizeLists.Single(rl => rl.name == TraitPicker.captionText.text);
                if (randomizeList.level > unit.Level)
                {
                    unit.AddPermanentTrait((Traits)randomizeList.id);
                } else
                {
                    var resTraits = unit.RandomizeOne(randomizeList);
                    foreach (Traits resTrait in resTraits)
                    {
                        unit.AddPermanentTrait(resTrait);
                        if (resTrait == Traits.Resourceful || resTrait == Traits.BookWormI || resTrait == Traits.BookWormII || resTrait == Traits.BookWormIII)
                        {
                            unit.SetMaxItems();
                        }
                    
                    }
            
                }
            }
            if (State.CustomTraitList.Any(ct => ct.name == TraitPicker.captionText.text))
            {
                CustomTraitBoost customTrait = State.CustomTraitList.Single(rl => rl.name == TraitPicker.captionText.text);
                unit.AddPermanentTrait((Traits)customTrait.id);
            }
            if (State.ConditionalTraitList.Any(ct => ct.name == TraitPicker.captionText.text))
            {
                ConditionalTraitContainer customTrait = State.ConditionalTraitList.Single(rl => rl.name == TraitPicker.captionText.text);
                unit.AddPermanentTrait((Traits)customTrait.id);
            }
            if (Enum.TryParse(TraitPicker.captionText.text, out Traits trait))
            {
                unit.AddPermanentTrait(trait);
                if (trait == Traits.Resourceful || trait == Traits.BookWormI || trait == Traits.BookWormII || trait == Traits.BookWormIII)
                {
                    unit.SetMaxItems();
                }

            }
        }

    }

    void RemoveTrait()
    {
        if (Enum.TryParse(TraitPicker.captionText.text, out Traits trait))
        {
            foreach (var unit in Army.Units)
            {
            unit.RemoveTrait(trait);
            if (trait == Traits.Resourceful)
            {
                unit.SetMaxItems();
            }

            }
        }
    }

}
