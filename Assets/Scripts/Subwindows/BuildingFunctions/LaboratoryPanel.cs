using MapObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaboratoryPanel : MonoBehaviour
{
    public TextMeshProUGUI BrewCount;
    public TextMeshProUGUI UnitPrice;
    public TextMeshProUGUI Discount;
    public TextMeshProUGUI TotalCost;
    public TextMeshProUGUI GoldCount;
    public Button StartPotion;
    public Button BrewPotion;
    public Button IncCount;
    public Button DecCount;

    public GameObject BrewPanel;
    public TextMeshProUGUI RemainingIngredientPicks;
    public TextMeshProUGUI TraitChance;

    public TextMeshProUGUI[] OptionTitle;
    public TextMeshProUGUI[] OptionStats;
    public Button[] OptionSelect;
    public TextMeshProUGUI[] RolledStats;

    PotionIngredient[] OptionIngredients;
    int[] UnitCostList;

    int BrewCountValue;
    int UnitPriceValue;
    float DiscountValue;
    double TraitChanceValue;
    int RemainingRolls;
    int[] PotionRollStats;

    LaboratoryPotion newPotion;
    Laboratory Laboratory;

    int TotalCostValue => (int)Math.Round(UnitPriceValue * BrewCountValue * DiscountValue);


    public void Open(ConstructibleBuilding building)
    {
        Laboratory = (Laboratory)building;

        BrewCountValue = 1;
        UnitCostList = new int[4];
        OptionIngredients = new PotionIngredient[4];

        StartPotion.GetComponentInChildren<Text>().text = $"New Potion\n ({Config.BuildConfig.LaboratoryUpfrontCost} G)";
        if (Laboratory.Owner.Gold < Config.BuildConfig.LaboratoryUpfrontCost)
        {
            StartPotion.interactable = false;
        }
        BrewPanel.gameObject.SetActive(false);
        StartPotion.onClick.AddListenerOnce(() =>
        {
            Laboratory.Owner.SpendGold(Config.BuildConfig.LaboratoryUpfrontCost);
            BeginPotion();
        });
        StartPotion.interactable = Laboratory.Owner.Gold >= Config.BuildConfig.LaboratoryUpfrontCost;
        BrewPotion.onClick.AddListenerOnce(() =>
        {
            EndPotion();
        });
        IncCount.onClick.AddListenerOnce(() =>
        {
            BrewCountValue++;
            BrewCount.text = BrewCountValue.ToString();
            DiscountValue = GetDiscount();
            Discount.text = ((int)((1 - DiscountValue) * 100)).ToString();
            TotalCost.text = TotalCostValue.ToString();
            if (BrewCountValue >= 1)
            {
                DecCount.interactable = true;
            }
            if (TotalCostValue > Laboratory.Owner.Gold)
            {
                BrewPotion.interactable = false;
            }
        });
        DecCount.onClick.AddListenerOnce(() =>
        {
            BrewCountValue--;
            BrewCount.text = BrewCountValue.ToString();
            DiscountValue = GetDiscount();
            Discount.text = ((int)((1 - DiscountValue) * 100)).ToString();
            TotalCost.text = TotalCostValue.ToString();
            if (TotalCostValue <= Laboratory.Owner.Gold)
            {
                BrewPotion.interactable = true;
            }
            if (BrewCountValue <= 0)
            {
                DecCount.interactable = false;
                BrewPotion.interactable = false;
            }
        });

        UnitPriceValue = Config.BuildConfig.LaboratoryBaseUnitPrice;
        UnitPrice.text = UnitPriceValue.ToString();
        TotalCost.text = TotalCostValue.ToString();
        TraitChanceValue = Config.BuildConfig.LaboratoryBaseTraitChance;
        DiscountValue = GetDiscount();
        Discount.text = ((int)((1 - DiscountValue) * 100)).ToString();
        BrewCount.text = BrewCountValue.ToString();
        OptionSelect[0].onClick.AddListenerOnce(() =>
        {
            UseOptions(0);
        });
        OptionSelect[1].onClick.AddListenerOnce(() =>
        {
            UseOptions(1);
        });
        OptionSelect[2].onClick.AddListenerOnce(() =>
        {
            UseOptions(2);
        });
        OptionSelect[3].onClick.AddListenerOnce(() =>
        {
            UseOptions(3);
        });
        GoldCount.text = Laboratory.Owner.Gold.ToString();
        BrewPotion.interactable = false;
        IncCount.interactable = false;
        DecCount.interactable = false;
    }

    void BeginPotion()
    {
        BrewPanel.gameObject.SetActive(true);
        BrewPotion.interactable = true;
        IncCount.interactable = true;
        DecCount.interactable = true;
        StartPotion.interactable = false;
        TraitChance.text = "Trait from Effect: " + ((int)(TraitChanceValue * 100)).ToString() + " %";
        RemainingIngredientPicks.text = $"Remaining Picks: {RemainingRolls}";
        PotionRollStats = new int[9];
        UnitPriceValue = Config.BuildConfig.LaboratoryBaseUnitPrice;
        UnitPrice.text = UnitPriceValue.ToString();
        newPotion = new LaboratoryPotion();
        TraitChanceValue = Config.BuildConfig.LaboratoryBaseTraitChance;
        DiscountValue = 1f;
        Discount.text = ((int)((1 - DiscountValue) * 100)).ToString();
        RemainingRolls = Config.BuildConfig.LaboratoryBaseRollCount * (Laboratory.improveUpgrade.built ? 2 : 1);
        for (int i = 0; i < RolledStats.Count(); i++)
        {
            RolledStats[i].text = ((TraitTier)i).ToString() + $" Effect: {PotionRollStats[i]}";
        }
        GoldCount.text = Laboratory.Owner.Gold.ToString();
        GenerateOptions(0);
        GenerateOptions(1);
        GenerateOptions(2);
        GenerateOptions(3);
    }

    void GenerateOptions(int id)
    {
        PotionIngredient ingRoll = (PotionIngredient)State.Rand.Next(Laboratory.ingredientUpgrade.built ? (int)PotionIngredient.PotionIngredientCounter : (int)PotionIngredient.Powerful);
        OptionIngredients[id] = ingRoll;
        OptionTitle[id].text = $"{ingRoll.ToString()} Ingredient";
        int baselineCost = Config.BuildConfig.LaboratoryBaseUnitPrice;
        int CostAdd = 0;
        switch (ingRoll)
        {
            case PotionIngredient.Grievous:
                OptionStats[id].text = $"Harmful Effect:50%\nNegative Effect:25%\nNeutral Effect:25%";
                CostAdd = baselineCost / -2;
                break;
            case PotionIngredient.Dangerous:
                OptionStats[id].text = $"Harmful Effect:25%\nNegative Effect:50%\nNeutral Effect:25%";
                CostAdd = (int)Math.Round(baselineCost / -1.5);
                break;
            case PotionIngredient.Experimental:
                OptionStats[id].text = $"Negative Effect:25%\n Neutral Effect:25%\n Common Effect:25%\n Uncommon Effect:25%";
                break;
            case PotionIngredient.Unstable:
                OptionStats[id].text = $"Negative Effect:10%\n Neutral Effect:10%\n Common Effect:40%\n Uncommon Effect:40%";
                CostAdd = (int)Math.Round(baselineCost * .15);
                break;
            case PotionIngredient.Stable:
                OptionStats[id].text = $"Neutral Effect:10%\n Common Effect:20%\n Uncommon Effect:40%\n Rare Effect:30%";
                CostAdd = (int)Math.Round(baselineCost * .3);
                break;
            case PotionIngredient.Simple:
                OptionStats[id].text = $"Common Effect:30%\n Uncommon Effect:30%\n Rare Effect:40%";
                CostAdd = (int)Math.Round(baselineCost * .5);
                break;
            case PotionIngredient.Standard:
                OptionStats[id].text = $"Uncommon Effect:30%\n Rare Effect:40%\n Epic Effect:20%\n Elite Effect:10%";
                CostAdd = (int)Math.Round(baselineCost * .75);
                break;
            case PotionIngredient.Premium:
                OptionStats[id].text = $"Rare Effect:20%\n Epic Effect:40%\n Elite Effect:30%\n Legendary Effect:10%";
                CostAdd = baselineCost;
                break;
            case PotionIngredient.Superior:
                OptionStats[id].text = $"Epic Effect:40%\n Elite Effect:30%\n Legendary Effect:30%";
                CostAdd = (int)Math.Round(baselineCost * 1.5);
                break;
            case PotionIngredient.Powerful:
                OptionStats[id].text = $"Elite Effect:50%\n Legendary Effect:50%";
                CostAdd = baselineCost * 2;
                break;
            case PotionIngredient.Legendary:
                OptionStats[id].text = $"Elite Effect:30%\n Legendary Effect:70%";
                CostAdd = baselineCost * 2;
                break;
            case PotionIngredient.Sterilizing:
                OptionStats[id].text = $"Removes all negative effects";
                CostAdd = (int)Math.Round(baselineCost * 0.5);
                break;
            case PotionIngredient.Purifying:
                OptionStats[id].text = $"Removes all current negative and harmful effects";
                CostAdd = (int)Math.Round(baselineCost * 1.5);
                break;
            case PotionIngredient.Solute:
                OptionStats[id].text = $"Increases chance effects become a trait.\nDoes not consume roll";
                CostAdd = (int)Math.Round(baselineCost * .25);
                break;
            case PotionIngredient.Solvent:
                OptionStats[id].text = $"Improves chance effects become a stat modifier.\nDoes not consume roll";
                CostAdd = (int)Math.Round(baselineCost * .05);
                break;
            case PotionIngredient.Coagulate:
                OptionStats[id].text = $"Grants +1 roll";
                CostAdd = (int)Math.Round(baselineCost * .5);
                break;
            default:
                break;
        }
        UnitCostList[id] = CostAdd;
        OptionSelect[id].interactable = (RemainingRolls > 0 || ingRoll == PotionIngredient.Coagulate) && Laboratory.Owner.Gold > UnitPriceValue + CostAdd;
        OptionStats[id].text += $"\nUnit Cost {(CostAdd > 0 ? '+' : '-')} {CostAdd}";
    }

    void UseOptions(int id)
    {
        RemainingRolls--;
        RemainingIngredientPicks.text = $"Remaining Picks: {RemainingRolls}";
        PotionIngredient ingRoll = OptionIngredients[id];
        double randRoll = State.Rand.NextDouble();
        switch (ingRoll)
        {
            case PotionIngredient.Grievous:
                if (randRoll >= .5)
                    IncRollValue(0);
                else if (randRoll >= .25)
                    IncRollValue(1);
                else
                    IncRollValue(2);
                break;
            case PotionIngredient.Dangerous:
                if (randRoll >= .5)
                    IncRollValue(1);
                else if (randRoll >= .25)
                    IncRollValue(0);
                else
                    IncRollValue(2);
                break;
            case PotionIngredient.Experimental:
                if (randRoll >= .25)
                    IncRollValue(1);
                else if (randRoll >= .5)
                    IncRollValue(2);
                else if (randRoll >= .75)
                    IncRollValue(3);
                else
                    IncRollValue(4);
                break;
            case PotionIngredient.Unstable:
                if (randRoll >= .1)
                    IncRollValue(1);
                else if (randRoll >= .2)
                    IncRollValue(2);
                else if (randRoll >= .6)
                    IncRollValue(3);
                else
                    IncRollValue(4);
                break;
            case PotionIngredient.Stable:
                if (randRoll >= .1)
                    IncRollValue(2);
                else if (randRoll >= .3)
                    IncRollValue(3);
                else if (randRoll >= .7)
                    IncRollValue(4);
                else
                    IncRollValue(5);
                break;
            case PotionIngredient.Simple:
                if (randRoll >= .3)
                    IncRollValue(3);
                else if (randRoll >= .6)
                    IncRollValue(4);
                else
                    IncRollValue(5);
                break;
            case PotionIngredient.Standard:
                if (randRoll >= .3)
                    IncRollValue(4);
                else if (randRoll >= .7)
                    IncRollValue(5);
                else if (randRoll >= .9)
                    IncRollValue(6);
                else
                    IncRollValue(7);
                break;
            case PotionIngredient.Premium:
                if (randRoll >= .2)
                    IncRollValue(5);
                else if (randRoll >= .6)
                    IncRollValue(6);
                else if (randRoll >= .9)
                    IncRollValue(7);
                else
                    IncRollValue(8);
                break;
            case PotionIngredient.Superior:
                if (randRoll >= .4)
                    IncRollValue(6);
                else if (randRoll >= .7)
                    IncRollValue(7);
                else
                    IncRollValue(8);
                break;
            case PotionIngredient.Powerful:
                if (randRoll >= .5)
                    IncRollValue(7);
                else
                    IncRollValue(8);
                break;
            case PotionIngredient.Legendary:
                if (randRoll >= .3)
                    IncRollValue(7);
                else
                    IncRollValue(8);
                break;
            case PotionIngredient.Sterilizing:
                DeleteEffect(1);
                break;
            case PotionIngredient.Purifying:
                DeleteEffect(0);
                DeleteEffect(1);
                break;
            case PotionIngredient.Solute:
                TraitChanceValue += 0.05;
                TraitChance.text = "Trait from Effect: " + ((int)(TraitChanceValue * 100)).ToString() + " %";
                RemainingRolls++;
                break;
            case PotionIngredient.Solvent:
                TraitChanceValue -= 0.1;
                TraitChance.text = "Trait from Effect: " + ((int)(TraitChanceValue * 100)).ToString() + " %";
                RemainingRolls++;
                break;
            case PotionIngredient.Coagulate:
                RemainingRolls += 2;
                break;
            default:
                break;
        }
        Debug.Log(UnitCostList[id]);
        UnitPriceValue += UnitCostList[id];
        UnitPrice.text = UnitPriceValue.ToString();
        TotalCost.text = TotalCostValue.ToString();

        GenerateOptions(0);
        GenerateOptions(1);
        GenerateOptions(2);
        GenerateOptions(3);
        for (int i = 0; i < RolledStats.Count(); i++)
        {
            RolledStats[i].text = ((TraitTier)i).ToString() + $" Effect: {PotionRollStats[i]}";
        }
        if (TotalCostValue > Laboratory.Owner.Gold)
        {
            BrewPotion.interactable = false;
        }
        else
        {
            BrewPotion.interactable = true;
        }
    }

    void IncRollValue(int id)
    {
        PotionRollStats[id] = PotionRollStats[id] + 1;
    }
    void DecRollValue(int id)
    {
        if (PotionRollStats[id] >= 1)
        {
            PotionRollStats[id] = PotionRollStats[id] - 1;
        }
    }

    void DeleteEffect(int id)
    {
        PotionRollStats[id] = 0;
    }

    void EndPotion()
    {
        BrewPanel.gameObject.SetActive(false);
        BrewPotion.interactable = false;
        IncCount.interactable = false;
        DecCount.interactable = false;
        StartPotion.interactable = Laboratory.Owner.Gold >= Config.BuildConfig.LaboratoryUpfrontCost;
        if (UnitPriceValue < 0)
        {
            UnitPriceValue = 0;
        }
        for (int i = 0; i < PotionRollStats.Count(); i++)
        {
            int ingQuality = i;
            int trait_count = PotionRollStats[ingQuality];
            for (int j = 0; j < trait_count; j++)
            {
                if (Laboratory.boostUpgrade.built)
                {
                    if (State.Rand.Next(4) == 0 && ingQuality <= 4)
                    {
                        ingQuality += 2;
                    }
                }
                if (State.Rand.NextDouble() > TraitChanceValue)
                {
                    int value = State.Rand.Next(5, 10);                   
                    if (ingQuality <= 1)
                    {
                        newPotion.StatModifiers[(Stat)State.Rand.Next(8)] += ingQuality == 0 ? value * -2 : -value;
                    }
                    else if (ingQuality == 2)
                    {
                        newPotion.StatModifiers[(Stat)State.Rand.Next(8)] += value;
                        newPotion.StatModifiers[(Stat)State.Rand.Next(8)] -= value;
                    }
                    else 
                    {
                        newPotion.StatModifiers[(Stat)State.Rand.Next(8)] += value * ingQuality;
                    }
                }
                else
                {
                    Traits incTrait = TaggedTraitUtilities.GetRandomTraitInTier((TraitTier)ingQuality);
                    if (incTrait <= 0)
                    {
                        int value = State.Rand.Next(5, 10);
                        if (ingQuality <= 1)
                        {
                            newPotion.StatModifiers[(Stat)State.Rand.Next(8)] += ingQuality == 0 ? value * -2 : -value;
                        }
                        else if (ingQuality == 2)
                        {
                            newPotion.StatModifiers[(Stat)State.Rand.Next(8)] += value;
                            newPotion.StatModifiers[(Stat)State.Rand.Next(8)] -= value;
                        }
                        else
                        {
                            newPotion.StatModifiers[(Stat)State.Rand.Next(8)] += value * ingQuality;
                        }
                    }
                    if (ingQuality <= 2)
                    {
                        newPotion.NegativeTraits.Add(incTrait);
                    }
                    else
                    {
                        newPotion.PositiveTraits.Add(incTrait);
                    }
                }                
            }
        }

        Laboratory.Owner.SpendGold(TotalCostValue);
        GoldCount.text = Laboratory.Owner.Gold.ToString();
        Laboratory.Owner.EmpirePotions.Add(newPotion, BrewCountValue);
    }

    float GetDiscount()
    {
        float discountTotal = 1f;
        if (BrewCountValue >= Config.BuildConfig.LaboratoryBulkMax)
        {
            return Config.BuildConfig.LaboratoryBulkDiscount;
        }
        if (BrewCountValue >= Config.BuildConfig.LaboratoryBulkMin)
        {
            return 1f - (Config.BuildConfig.LaboratoryBulkDiscount * ((float)BrewCountValue / (float)Config.BuildConfig.LaboratoryBulkMax));
        }
        return discountTotal;
    }
}