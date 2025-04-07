using Boo.Lang;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class PotionInv : MonoBehaviour
{

    public Transform potionFolder;
    public PotionListItem potionListItem;
    public Recruit_Mode Recruit_Mode;
    //Empire empire;
    Unit unit;


    public void Open(Empire empire, Unit unit)
    {
        ClearFolder();
        //this.empire = empire;
        this.unit = unit;
        foreach (var item in empire.EmpirePotions)
        {
            LaboratoryPotion potion = item.Key;
            int count = item.Value;
            if (count <= 0)
            {
                empire.EmpirePotions.Remove(potion);
                continue;
            }
            var obj = Instantiate(potionListItem, potionFolder);
            var currentPrefab = obj.GetComponent<PotionListItem>();
            currentPrefab.RemainingStock.text = empire.EmpirePotions[potion].ToString();
            currentPrefab.PositiveDescription.text = FormatPotionPositives(potion);
            currentPrefab.NegativeDescription.text = FormatPotionNegative(potion);
            currentPrefab.ApplyButton.onClick.AddListener(() =>
            {
                empire.EmpirePotions[potion] = empire.EmpirePotions[potion] - 1;
                currentPrefab.RemainingStock.text = empire.EmpirePotions[potion].ToString();
                ApplyPotion(potion);
                Recruit_Mode.ButtonCallback(50);
                if (empire.EmpirePotions[potion] <= 0)
                {
                    empire.EmpirePotions.Remove(potion);
                    currentPrefab.gameObject.SetActive(false);
                }
            });
            currentPrefab.DiscardButton.onClick.AddListener(() =>
            {
                empire.EmpirePotions.Remove(potion);
                currentPrefab.gameObject.SetActive(false);
            });
        }
    }
    public void ApplyPotion(LaboratoryPotion potion)
    {
        foreach (Traits trait in potion.PositiveTraits)
        {
            unit.AddTrait(trait);
        }
        foreach (Traits trait in potion.NegativeTraits)
        {
            unit.AddTrait(trait);
        }
        foreach (var statShift in potion.StatModifiers)
        {
            unit.SpecificStatIncrease(statShift.Value, (int)statShift.Key);
        }
    }

    private string FormatPotionPositives(LaboratoryPotion potion)
    {
        string returnString = "";
        var last = potion.PositiveTraits.LastOrDefault();
        foreach (Traits item in potion.PositiveTraits)
        {
            returnString += $"Grants {item.ToString()}";
            if (!item.Equals(last))
            {
                returnString += ",";
            }
            returnString += " ";

        }
        returnString += "\n";
        foreach (var item in potion.StatModifiers.Where(s => s.Value > 0))
        {
            returnString += $"Grants +{item.Value.ToString()} {item.Key.ToString()}";
            if (!item.Equals(last))
            {
                returnString += ",";
            }
            returnString += " ";
        }
        if (potion.PositiveTraits.Count <= 0 && potion.StatModifiers.Where(s => s.Value > 0).Count() <= 0)
        {
            returnString += "None.";
        }
        return returnString;
    }

    private string FormatPotionNegative(LaboratoryPotion potion)
    {
        string returnString = "";
        var last = potion.PositiveTraits.LastOrDefault();
        foreach (Traits item in potion.NegativeTraits)
        {
            returnString += $"Grants {item.ToString()}";
            if (!item.Equals(last))
            {
                returnString += ",";
            }
            returnString += " ";
        }
        returnString += "\n";
        foreach (var item in potion.StatModifiers.Where(s => s.Value < 0))
        {
            returnString += $"Causes {item.Value.ToString()} {item.Key.ToString()}";
            if (!item.Equals(last))
            {
                returnString += ",";
            }
            returnString += " ";
        }
        if (potion.NegativeTraits.Count <= 0 && potion.StatModifiers.Where(s => s.Value < 0).Count() <= 0)
        {
            returnString += "None.";
        }
        return returnString;
    }

    private void ClearFolder()
    {
        int children = potionFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(potionFolder.GetChild(i).gameObject);
        }
    }
}
