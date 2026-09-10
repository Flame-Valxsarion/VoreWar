using MapObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Experimental.UIElements.StyleEnums;

public class BuildMenu : MonoBehaviour
{


    public Button HelpButton;
    public GameObject HelpHolder;
    public GameObject[] HelpPanels;
    public TMP_Dropdown PageSelector;

    public GameObject BuildItemPrefab;
    public Transform BuildingFolder;

    public TextMeshProUGUI CurrentGold;
    public TextMeshProUGUI CurrentWood;
    public TextMeshProUGUI CurrentStone;
    public TextMeshProUGUI CurrentNM;
    public TextMeshProUGUI CurrentOres;
    public TextMeshProUGUI CurrentPrefabs;
    public TextMeshProUGUI CurrentMS;

    public void Open(Empire empire)
    {
        ClearFolder();
        State.GameManager.StrategyMode.Paused = true;
        gameObject.SetActive(true);
        HelpHolder.SetActive(false);
        Config.World.ReloadBuildingInfo();
        var enabledBuildings = Config.World.GetBuildingInfo();
        CurrentGold.text = empire.Gold.ToString();
        CurrentWood.text = empire.constructionResources.Wood.ToString();
        CurrentStone.text = empire.constructionResources.Stone.ToString();
        CurrentNM.text = empire.constructionResources.NaturalMaterials.ToString();
        CurrentOres.text = empire.constructionResources.Ores.ToString();
        CurrentPrefabs.text = empire.constructionResources.Prefabs.ToString();
        CurrentMS.text = empire.constructionResources.ManaStones.ToString();
        if (Config.BuildConfig.BuildingSystemTurnLockout >= State.World.Turn)
        {
            return;
        }
        foreach (var building in enabledBuildings)
        {
            if (!building.enabled)
                continue;
            var obj = Instantiate(BuildItemPrefab, BuildingFolder);
            var currentPrefab = obj.GetComponent<BuildingPrefab>();
            currentPrefab.BuildingName.text = building.Name;
            currentPrefab.BuildingDesc.text = building.Desc;
            currentPrefab.BuildTurns.text = building.baseBuildTurns.ToString();
            currentPrefab.GoldCost.text = building.GoldCost.ToString();
            currentPrefab.Wood.text = building.ResourceToBuild.Wood.ToString();
            currentPrefab.NaturalMaterials.text = building.ResourceToBuild.NaturalMaterials.ToString();
            currentPrefab.Prefabs.text = building.ResourceToBuild.Prefabs.ToString();
            currentPrefab.Stone.text = building.ResourceToBuild.Stone.ToString();
            currentPrefab.Ores.text = building.ResourceToBuild.Ores.ToString();
            currentPrefab.ManaStones.text = building.ResourceToBuild.ManaStones.ToString();
            currentPrefab.linkedBuilding = building.buildingType;
            int remaining = 0;
            switch (building.buildingType)
            {
                case ConstructibleType.WorkCamp:
                    remaining = Config.BuildConfig.WorkCamp.BuildLimit;
                    break;
                case ConstructibleType.LumberSite:
                    remaining = Config.BuildConfig.LumberSite.BuildLimit;
                    break;
                case ConstructibleType.Quarry:
                    remaining = Config.BuildConfig.Quarry.BuildLimit;
                    break;
                case ConstructibleType.CasterTower:
                    remaining = Config.BuildConfig.CasterTower.BuildLimit;
                    break;
                case ConstructibleType.BarrierTower:
                    remaining = Config.BuildConfig.BarrierTower.BuildLimit;
                    break;
                case ConstructibleType.DefEncampment:
                    remaining = Config.BuildConfig.DefenseEncampment.BuildLimit;
                    break;
                case ConstructibleType.Academy:
                    remaining = Config.BuildConfig.Academy.BuildLimit;
                    break;
                case ConstructibleType.DarkMagicTower:
                    remaining = Config.BuildConfig.DarkMagicTower.BuildLimit;
                    break;
                case ConstructibleType.TemporalTower:
                    remaining = Config.BuildConfig.TemporalTower.BuildLimit;
                    break;
                case ConstructibleType.Laboratory:
                    remaining = Config.BuildConfig.Laboratory.BuildLimit;
                    break;
                case ConstructibleType.Teleporter:
                    remaining = Config.BuildConfig.Teleporter.BuildLimit;
                    break;
                case ConstructibleType.TownHall:
                    remaining = Config.BuildConfig.TownHall.BuildLimit;
                    break;
                default:
                    break;
            }
            currentPrefab.BuildLimit.text = $"{remaining - empire.EmpireBuildingLimit[building.buildingType]} Remaining";
            if (empire.EmpireBuildingLimit[building.buildingType] <= -1)
            {
                currentPrefab.BuildLimit.gameObject.SetActive(false);
            }
            if (!empire.constructionResources.CanBuildWithCurrentResources(building.ResourceToBuild) || building.GoldCost > empire.Gold || !empire.WithinBuildLimit(building.buildingType))
            {
                currentPrefab.Construct.interactable = false;
            }
            currentPrefab.Construct.onClick.AddListener(() =>
            {
                Close();
                State.GameManager.StrategyMode.InitiateBuildMode(currentPrefab.linkedBuilding);
            });
        }
    }

    private void ClearFolder()
    {
        int children = BuildingFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(BuildingFolder.GetChild(i).gameObject);
        }
    }
    public void Close()
    {
        ClearFolder();
        gameObject.SetActive(false);
        State.GameManager.StrategyMode.Paused = false;
        State.GameManager.StrategyMode.BuildMode = false;
    }

    public void OpenHelp()
    {
        HelpHolder.SetActive(true);
    }
    public void CloseHelp()
    {
        HelpHolder.SetActive(false);
    }

    public void ActivatePage()
    {
        for (int i = 0; i < HelpPanels.Length; i++)
        {
            HelpPanels[i].SetActive(false);
        }
        HelpPanels[PageSelector.value].SetActive(true);
    }
}
