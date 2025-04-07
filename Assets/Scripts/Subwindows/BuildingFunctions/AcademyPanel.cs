using MapObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AcademyPanel : MonoBehaviour
{
    public GameObject ResearchHolder;
    public GameObject ExtraFundsHolder;
    public Transform Folder;
    public AcademyPanelResearchItemPrefab Prefab;
    internal AcademyPanelResearchItemPrefab PrefabInstance;

    public TextMeshProUGUI StoredEXP;
    public TextMeshProUGUI EXPCost;
    public TextMeshProUGUI CurrentEXPRate;
    public TextMeshProUGUI IncomeSliderValue;
    public Slider IncomeSlider;
    public TextMeshProUGUI BonusIncomeSliderValue;
    public Slider BonusIncomeSlider;
    public TextMeshProUGUI EXPDistributeSliderValue;
    public Slider EXPDistributeSlider;
    
    Academy Academy;

    public void Open(ConstructibleBuilding building)
    {
        Academy = (Academy)building;
        StoredEXP.text = Academy.StoredEXP.ToString();
        EXPCost.text = Academy.Owner.AcademyUpgradeEXPCost.ToString();
        CurrentEXPRate.text = (Academy.totalIncome * Config.BuildConfig.AcademyEXPPerGold).ToString();
        IncomeSliderValue.text = Academy.Income1.ToString();
        IncomeSlider.value = Academy.Income1;
        BonusIncomeSliderValue.text = Academy.Income2.ToString();
        BonusIncomeSlider.value = Academy.Income2;
        EXPDistributeSliderValue.text = Academy.DistributedEXP.ToString();
        EXPDistributeSlider.value = Academy.DistributedEXP;
        if (!Academy.efficiencyUpgrade.built)
        {
            EXPDistributeSlider.maxValue = 0.1f;
            if (EXPDistributeSlider.value > 0.1)
                EXPDistributeSlider.value = 0.1f;
        }
        else
            EXPDistributeSlider.maxValue = 1f;

        IncomeSlider.onValueChanged.AddListenerOnce((float newVal) =>
        {
            Academy.Income1 = (int)newVal;
            IncomeSliderValue.text = newVal.ToString();
            CurrentEXPRate.text = (Academy.totalIncome * Config.BuildConfig.AcademyEXPPerGold).ToString();
        });
        BonusIncomeSlider.onValueChanged.AddListenerOnce((float newVal) =>
        {
            Academy.Income2 = (int)newVal;
            BonusIncomeSliderValue.text = newVal.ToString();
            CurrentEXPRate.text = (Academy.totalIncome * Config.BuildConfig.AcademyEXPPerGold).ToString();
        });
        EXPDistributeSlider.onValueChanged.AddListenerOnce((float newVal) =>
        {
            Academy.DistributedEXP = newVal;
            EXPDistributeSliderValue.text = newVal.ToString();
            CurrentEXPRate.text = (Academy.totalIncome * Config.BuildConfig.AcademyEXPPerGold).ToString();
        });

        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }
        ExtraFundsHolder.SetActive(Academy.improveUpgrade.built);
        ResearchHolder.SetActive(Academy.researchUpgrade.built);
        PopulateFolder();
    }

    public void PopulateFolder()
    {
        int maxvalue = (int)AcademyResearchType.ResearchTypeCounter;
        for (int j = 0; maxvalue - 1 >= j; j++)
        {
            AcademyResearchType type = (AcademyResearchType)j;
            PrefabInstance = Instantiate(Prefab, Folder);
            AcademyPanelResearchItemPrefab newPrefab = PrefabInstance.GetComponent<AcademyPanelResearchItemPrefab>();
            newPrefab.functinoCall = null;
            switch (type)
            {
                case AcademyResearchType.ArmyMP:
                    newPrefab.Name.text = "Conditioning";
                    newPrefab.Effect.text = "+0.5 ArmyMP";
                    break;
                case AcademyResearchType.Happiness:
                    newPrefab.Name.text = "Quality Of Life";
                    newPrefab.Effect.text = "+1 Happieness Per Turn";
                    break;
                case AcademyResearchType.ImprintCost:
                    newPrefab.Name.text = "Reconstruction";
                    newPrefab.Effect.text = "-12.5% Imprint Cost";
                    break;
                case AcademyResearchType.MercRecruitCost:
                    newPrefab.Name.text = "Rhetoric";
                    newPrefab.Effect.text = "-10% Mercenary/Adventurer Cost";
                    break;
                case AcademyResearchType.VilPopMax:
                    newPrefab.Name.text = "Housing Strategies";
                    newPrefab.Effect.text = "+10% Max Village Population";
                    break;
                case AcademyResearchType.PopBreedInc:
                    newPrefab.Name.text = "Biology";
                    newPrefab.Effect.text = "+10% Population Growth";
                    break;
                case AcademyResearchType.ArmySize:
                    newPrefab.Name.text = "Ancient Formations";
                    newPrefab.Effect.text = "+5% Maximum Army Size";
                    newPrefab.functinoCall = (e) => e.MaxArmySize = e.OrigMaxArmySize + (int)Math.Ceiling(e.OrigMaxArmySize * 0.05f * (Academy.Owner.AcademyResearchCompleted.Keys.Contains(type) ? Academy.Owner.AcademyResearchCompleted[type] : 0));
                    break;
                case AcademyResearchType.GarrisonSize:
                    newPrefab.Name.text = "Militia Operation";
                    newPrefab.Effect.text = "+5% Maximum Garrison Size";
                    newPrefab.functinoCall = (e) => e.MaxArmySize = e.OrigMaxGarrisonSize + (int)Math.Ceiling(e.OrigMaxGarrisonSize * 0.05f * (Academy.Owner.AcademyResearchCompleted.Keys.Contains(type) ? Academy.Owner.AcademyResearchCompleted[type] : 0));
                    break;
                case AcademyResearchType.FOWSightRange:
                    newPrefab.Name.text = "Low Vision Scouting";
                    newPrefab.Effect.text = "+0.5 Fog Sight Range";
                    break;
                case AcademyResearchType.ArmyHealRate:
                    newPrefab.Name.text = "Sustained Healing";
                    newPrefab.Effect.text = "+25% Army Healing Rate";
                    break;
                case AcademyResearchType.GoldMineIncome:
                    newPrefab.Name.text = "Fair Quota";
                    newPrefab.Effect.text = "+12.5% Gold Mine Income";
                    break;
                case AcademyResearchType.TrainingEXP:
                    newPrefab.Name.text = "Training Subsidies";
                    newPrefab.Effect.text = "+25% Training EXP";
                    break;
                case AcademyResearchType.TeamEXP:
                    newPrefab.Name.text = "Outsourced Training";
                    newPrefab.Effect.text = "+10 Team EXP";
                    break;
                case AcademyResearchType.StartingEXP:
                    newPrefab.Name.text = "Public Preparedness";
                    newPrefab.Effect.text = "+20 Starting EXP";
                    break;
                case AcademyResearchType.UpkeepReduction:
                    newPrefab.Name.text = "Contract Development";
                    newPrefab.Effect.text = "-7.5% upkeep";
                    break;
                default:
                    break;
            }
            newPrefab.Maximum.text = Config.BuildConfig.AcademyMaximumUpgrades.ToString();
            if (Academy.Owner.AcademyResearchCompleted.Keys.Contains(type))
            {
                newPrefab.Current.text = Academy.Owner.AcademyResearchCompleted[type].ToString();
                newPrefab.ConductButton.onClick.AddListenerOnce(() =>
                {
                    Academy.Owner.AcademyResearchCompleted[type] = Academy.Owner.AcademyResearchCompleted[type] + 1;
                    Academy.Owner.AcademyUpgradeEXPCost = (int)(Academy.Owner.AcademyUpgradeEXPCost * Config.BuildConfig.AcademyCostIncreaseMultPerUpgrade);
                    EXPCost.text = Academy.Owner.AcademyUpgradeEXPCost.ToString();
                    if (Academy.Owner.AcademyResearchCompleted[type] >= Config.BuildConfig.AcademyMaximumUpgrades || Academy.Owner.AcademyUpgradeEXPCost >= Academy.StoredEXP)
                    {
                        newPrefab.ConductButton.interactable = false;
                    }
                    if (newPrefab.functinoCall != null)
                    {
                        newPrefab.functinoCall.Invoke(Academy.Owner);
                    }
                });
                if (Academy.Owner.AcademyResearchCompleted[type] >= Config.BuildConfig.AcademyMaximumUpgrades || Academy.Owner.AcademyUpgradeEXPCost >= Academy.StoredEXP)
                {
                    newPrefab.ConductButton.interactable = false;
                }
            }
            else
            {
                newPrefab.Current.text = "0";
                newPrefab.ConductButton.onClick.AddListenerOnce(() =>
                {
                    Academy.Owner.AcademyResearchCompleted.Add(type, 1);
                    Academy.Owner.AcademyUpgradeEXPCost = (int)(Academy.Owner.AcademyUpgradeEXPCost * Config.BuildConfig.AcademyCostIncreaseMultPerUpgrade);
                    EXPCost.text = Academy.Owner.AcademyUpgradeEXPCost.ToString();
                    if (Academy.Owner.AcademyResearchCompleted[type] >= Config.BuildConfig.AcademyMaximumUpgrades || Academy.Owner.AcademyUpgradeEXPCost >= Academy.StoredEXP)
                    {
                        newPrefab.ConductButton.interactable = false;
                    }
                });
                if (Academy.Owner.AcademyUpgradeEXPCost >= Academy.StoredEXP)
                {
                    newPrefab.ConductButton.interactable = false;
                }

            }
            newPrefab.Owner = Academy.Owner;
            newPrefab.associatedResearch = type;
        }
    }
}