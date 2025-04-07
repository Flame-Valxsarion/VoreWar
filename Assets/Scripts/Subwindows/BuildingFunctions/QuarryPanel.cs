using MapObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuarryPanel : MonoBehaviour
{

    public Button StandardActionPlanButton;
    public Button SearchActionPlanButton; 
    public Button GroupedActionPlanButton;
    public Button RiskyActionPlanButton;
    public Button FocusActionButton;
    public Button ProspectActionPlanButton;

    public TextMeshProUGUI ActionPlanDescription;
    public TextMeshProUGUI StoneText;
    public TextMeshProUGUI OresText;
    public TextMeshProUGUI MSText;
    public TextMeshProUGUI GoldText;

    Quarry Quarry;

    public void Open(ConstructibleBuilding building)
    {
        Quarry = (Quarry)building;
        UpdateVisibility();
        StandardActionPlanButton.onClick.AddListenerOnce(() =>
        {
            SetActionPlan(0);
        });
        SearchActionPlanButton.onClick.AddListenerOnce(() =>
        {
            SetActionPlan(1);
        });
        GroupedActionPlanButton.onClick.AddListenerOnce(() =>
        {
            SetActionPlan(2);
        });
        RiskyActionPlanButton.onClick.AddListenerOnce(() =>
        {
            SetActionPlan(3);
        });
        FocusActionButton.onClick.AddListenerOnce(() =>
        {
            SetActionPlan(4);
        });
        ProspectActionPlanButton.onClick.AddListenerOnce(() =>
        {
            SetActionPlan(5);
        });
    }

    void SetActionPlan(int planNumber)
    {
        Quarry.ActionPlan = planNumber;
        UpdateVisibility();
    }

    void UpdateVisibility()
    {
        switch (Quarry.ActionPlan)
        {
            case 0:
                StandardActionPlanButton.interactable = false;
                SearchActionPlanButton.interactable = true;
                GroupedActionPlanButton.interactable = true;
                RiskyActionPlanButton.interactable = true;
                FocusActionButton.interactable = true;
                ProspectActionPlanButton.interactable = true;
                break;
            case 1:
                StandardActionPlanButton.interactable = true;
                SearchActionPlanButton.interactable = false;
                GroupedActionPlanButton.interactable = true;
                RiskyActionPlanButton.interactable = true;
                FocusActionButton.interactable = true;
                ProspectActionPlanButton.interactable = true;
                break;
            case 2:
                StandardActionPlanButton.interactable = true;
                SearchActionPlanButton.interactable = true;
                GroupedActionPlanButton.interactable = false;
                RiskyActionPlanButton.interactable = true;
                FocusActionButton.interactable = true;
                ProspectActionPlanButton.interactable = true;
                break;
            case 3:
                StandardActionPlanButton.interactable = true;
                SearchActionPlanButton.interactable = true;
                GroupedActionPlanButton.interactable = true;
                RiskyActionPlanButton.interactable = false;
                FocusActionButton.interactable = true;
                ProspectActionPlanButton.interactable = true;
                break;
            case 4:
                StandardActionPlanButton.interactable = true;
                SearchActionPlanButton.interactable = true;
                GroupedActionPlanButton.interactable = true;
                RiskyActionPlanButton.interactable = true;
                FocusActionButton.interactable = false;
                ProspectActionPlanButton.interactable = true;
                break;
            case 5:
                StandardActionPlanButton.interactable = true;
                SearchActionPlanButton.interactable = true;
                GroupedActionPlanButton.interactable = true;
                RiskyActionPlanButton.interactable = true;
                FocusActionButton.interactable = true;
                ProspectActionPlanButton.interactable = false;
                break;
            default:
                break;
        }

        int Oresmin = Quarry.deepUpgrade.built ? Config.BuildConfig.QuarryOreMin : 0;
        int Oresmax = Quarry.deepUpgrade.built ? Config.BuildConfig.QuarryOreMax: 0;
        int MSmin = Quarry.leyUpgrade.built ? Config.BuildConfig.QuarryMSMin: 0;
        int MSmax = Quarry.leyUpgrade.built ? Config.BuildConfig.QuarryMSMax: 0;

        if (!Quarry.improveUpgrade.built)
        {
            ProspectActionPlanButton.interactable = false;
            FocusActionButton.interactable = false;
            RiskyActionPlanButton.interactable = false;

            switch (Quarry.ActionPlan)
            {
                case 0:
                    ActionPlanDescription.text = $"The quarry operates normally, providing no benefits or drawbacks.";
                    StoneText.text = $"{Config.BuildConfig.QuarryStoneMin} - {Config.BuildConfig.QuarryStoneMax}";
                    OresText.text = $"{Oresmin} - {Oresmax}";
                    MSText.text = $"{MSmin} - {MSmax}";
                    GoldText.text = $"0";
                    break;
                case 1:
                    ActionPlanDescription.text = $"Workers search for new deposits while ignoring old ones, increases the max and reduces the min by 50%.";
                    StoneText.text = $"{(int)Math.Floor(Config.BuildConfig.QuarryStoneMin * 0.5f)} - {(int)Math.Ceiling(Config.BuildConfig.QuarryStoneMax * 1.5f)}";
                    OresText.text = $"{(int)Math.Ceiling(Oresmin * 0.5f)} - {(int)Math.Ceiling(Oresmax * 1.5f)}";
                    MSText.text = $"{(int)Math.Floor(Config.BuildConfig.QuarryGoldMin * 0.5f)} - {(int)Math.Ceiling(Config.BuildConfig.QuarryGoldMax * 1.5f)}";
                    GoldText.text = $"0";
                    break;
                case 2:
                    ActionPlanDescription.text = $"Workers group together, focusing on mining as much as possible from one kind of material.";
                    StoneText.text = $"25% for {Config.BuildConfig.QuarryStoneMax}";
                    OresText.text = $"25% for {Oresmax}";
                    MSText.text = $"25% for {MSmax}";
                    GoldText.text = $"25% for {Config.BuildConfig.QuarryGoldMax}";
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (Quarry.ActionPlan)
            {
                case 0:
                    ActionPlanDescription.text = $"The quarry operates normally, providing no benefits or drawbacks";
                    StoneText.text = $"{Config.BuildConfig.QuarryStoneMin + 1} - {Config.BuildConfig.QuarryStoneMax + 1}";
                    OresText.text = $"{Oresmin + 1} - {Oresmax + 1}";
                    MSText.text = $"{MSmin + 1} - {MSmax + 1}";
                    GoldText.text = $"0";
                    break;
                case 1:
                    ActionPlanDescription.text = $"Workers search for new deposits while ignoring old ones, increases the max and reduces the min by 50%.";
                    StoneText.text = $"{(int)Math.Floor(Config.BuildConfig.QuarryStoneMin * 0.5f)} - {(int)Math.Ceiling(Config.BuildConfig.QuarryStoneMax * 1.5f)}";
                    OresText.text = $"{(int)Math.Ceiling(Oresmin * 0.5f)} - {(int)Math.Ceiling(Oresmax * 1.5f)}";
                    MSText.text = $"{(int)Math.Floor(Config.BuildConfig.QuarryGoldMin * 0.5f)} - {(int)Math.Ceiling(Config.BuildConfig.QuarryGoldMax * 1.5f)}";
                    GoldText.text = $"0";
                    break;
                case 2:
                    ActionPlanDescription.text = $"Workers group together, focusing on mining as much as possible from one kind of material. Rolls twice.";
                    StoneText.text = $"25% for {Config.BuildConfig.QuarryStoneMax}";
                    OresText.text = $"25% for {Oresmax}";
                    MSText.text = $"25% for {MSmax}";
                    GoldText.text = $"25% for {Config.BuildConfig.QuarryGoldMax}";
                    break;
                case 3:
                    ActionPlanDescription.text = $"Quarry removes all saftey restrictions. The new minimum is set to 0 and the old minimum is added to the maximum.";
                    StoneText.text = $"0 - {Config.BuildConfig.QuarryStoneMax + Config.BuildConfig.QuarryStoneMin}";
                    OresText.text = $"0 - {Oresmax + Oresmin}";
                    MSText.text = $"0 - {MSmax + MSmin}";
                    GoldText.text = $"0 - {Config.BuildConfig.QuarryGoldMax + Config.BuildConfig.QuarryGoldMin}";
                    break;
                case 4:
                    ActionPlanDescription.text = $"Quarry focuses on only gathering high quality materials. Gold and standard stone are ignored in favore of Ore and Manastones.";
                    StoneText.text = $"0";
                    OresText.text = $"60% for {Oresmin * 2} - {Oresmax * 2}";
                    MSText.text = $"40% for {MSmin * 2} - {MSmax * 2}";
                    GoldText.text = $"0";
                    break;
                case 5:
                    ActionPlanDescription.text = $"Quarry prospects for gold, results vary and are less consistent than a gold mine.";
                    StoneText.text = $"0";
                    OresText.text = $"0";
                    MSText.text = $"0";
                    GoldText.text = $"0 - {Config.BuildConfig.QuarryGoldMax * 2}";
                    break;
                default:
                    break;
            }
        }

        if (!Quarry.deepUpgrade.built)
        {
            if (Quarry.ActionPlan == 2)
            {
                OresText.text = "25% for 0";
            }
            OresText.text = "0";
        }

        if (!Quarry.leyUpgrade.built)
        {
            if (Quarry.ActionPlan == 2)
            {
                MSText.text = "25% for 0";
            }
            MSText.text = "0";
        }
    }

}
