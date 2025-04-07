using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSettings : MonoBehaviour
{
    public Toggle BuildingSystemEnabled;
    public InputField BuildingSystemTurnLockout;
    public InputField BuildingPassiveRange;
    public InputField BuildingCaptureTurns;

    public TMP_Dropdown BuildingDropdown;
    public TMP_Dropdown EmpireCaptureDropdown;
    public TMP_Dropdown MonsterCaptureDropdown;

    public InputField WoodCost;
    public InputField NaturalMaterialsCost;
    public InputField PrefabsCost;
    public InputField StoneCost;
    public InputField OresCost;
    public InputField ManaStonesCost;
    public InputField GoldCost;

    public InputField ConstructionTurns;
    public InputField BuildLimit;
    public Toggle AICanBuild;

    public GameObject UpgradeEditPrefab;
    public Transform UpgradeFolder;

    public GameObject NoneSettings;
    public WorkCampSettings WorkCampSettings;
    public LumberSiteSettings LumberSiteSettings;
    public QuarrySettings QuarrySettings;
    public CasterTowerSettings CasterTowerSettings;
    public BarrierTowerSettings BarrierTowerSettings;
    public DefEncampSettings DefEncampSettings;
    public AcademySettings AcademySettings;
    public DarkMagicTowerSettings DarkMagicTowerSettings;
    public TemporalTowerSettings TemporalTowerSettings;
    public LaboratorySettings LaboratorySettings;
    public TeleporterSettings TeleporterSettings;
    public TownHallSettings TownHallSettings;


    List<BuildingUpgrade> buildingUpgrades = new List<BuildingUpgrade>();
    int currentDropdownValue = 0;

    public void DropdownUpdate()
    {
        GeneralBuildingConfig currentPage = new GeneralBuildingConfig();
        switch (currentDropdownValue)
        {
            case 0: // None
                WorkCampSettings.gameObject.SetActive(false);
                LumberSiteSettings.gameObject.SetActive(false);
                QuarrySettings.gameObject.SetActive(false);
                CasterTowerSettings.gameObject.SetActive(false);
                BarrierTowerSettings.gameObject.SetActive(false);
                WorkCampSettings.gameObject.SetActive(false);
                DefEncampSettings.gameObject.SetActive(false);
                AcademySettings.gameObject.SetActive(false);
                DarkMagicTowerSettings.gameObject.SetActive(false);
                TemporalTowerSettings.gameObject.SetActive(false);
                LaboratorySettings.gameObject.SetActive(false);
                LaboratorySettings.gameObject.SetActive(false);
                TownHallSettings.gameObject.SetActive(false);
                break;
            case 1: // Work Camp
                SaveText(Config.BuildConfig.WorkCamp.Resources);
                currentPage = Config.BuildConfig.WorkCamp;
                WorkCampSettings.gameObject.SetActive(false);
                WorkCampSettings.Save();
                break;
            case 2: // Lumber Site
                SaveText(Config.BuildConfig.LumberSite.Resources);
                currentPage = Config.BuildConfig.LumberSite;
                LumberSiteSettings.gameObject.SetActive(false);
                LumberSiteSettings.Save();
                break;
            case 3: // Quarry
                SaveText(Config.BuildConfig.Quarry.Resources);
                currentPage = Config.BuildConfig.Quarry;
                QuarrySettings.gameObject.SetActive(false);
                QuarrySettings.Save();
                break;
            case 4: // Caster Tower
                SaveText(Config.BuildConfig.CasterTower.Resources);
                currentPage = Config.BuildConfig.CasterTower;
                CasterTowerSettings.gameObject.SetActive(false);
                CasterTowerSettings.Save();
                break;
            case 5: // Barrier Tower
                SaveText(Config.BuildConfig.BarrierTower.Resources); 
                currentPage = Config.BuildConfig.BarrierTower;
                BarrierTowerSettings.gameObject.SetActive(false);
                BarrierTowerSettings.Save();
                break;
            case 6: // DefEncampSettings
                SaveText(Config.BuildConfig.DefenseEncampment.Resources);
                currentPage = Config.BuildConfig.DefenseEncampment;
                DefEncampSettings.gameObject.SetActive(false);
                DefEncampSettings.Save();
                break;
            case 7: // Academy
                SaveText(Config.BuildConfig.Academy.Resources);
                currentPage = Config.BuildConfig.Academy;
                AcademySettings.gameObject.SetActive(false);
                AcademySettings.Save();
                break;
            case 8: // DarkMagicTower
                SaveText(Config.BuildConfig.DarkMagicTower.Resources);
                currentPage = Config.BuildConfig.DarkMagicTower;
                DarkMagicTowerSettings.gameObject.SetActive(false);
                DarkMagicTowerSettings.Save();
                break;
            case 9: // TemporalTower
                SaveText(Config.BuildConfig.TemporalTower.Resources);
                currentPage = Config.BuildConfig.TemporalTower;
                TemporalTowerSettings.gameObject.SetActive(false);
                TemporalTowerSettings.Save();
                break;
            case 10: // Laboratory
                SaveText(Config.BuildConfig.Laboratory.Resources); 
                currentPage = Config.BuildConfig.Laboratory;
                LaboratorySettings.gameObject.SetActive(false);
                LaboratorySettings.Save();
                break;
            case 11: // Teleporter
                SaveText(Config.BuildConfig.Teleporter.Resources);
                currentPage = Config.BuildConfig.Teleporter;
                TeleporterSettings.gameObject.SetActive(false);
                TeleporterSettings.Save();
                break;
            case 12: // TownHall
                SaveText(Config.BuildConfig.TownHall.Resources);
                currentPage = Config.BuildConfig.TownHall;
                TownHallSettings.gameObject.SetActive(false);
                TownHallSettings.Save();
                break;
            default:
                break;
        }

        currentPage.Gold = int.TryParse(GoldCost.text, out int wcg) ? wcg : 15;
        currentPage.BuildTime = int.TryParse(ConstructionTurns.text, out int wcct) ? wcct : 2;
        currentPage.BuildLimit = int.TryParse(BuildLimit.text, out int wcbl) ? wcbl : -1;
        currentPage.AICanBuild = AICanBuild.isOn;

        buildingUpgrades.Clear();
        ClearFolder();

        switch (BuildingDropdown.value)
        {
            case 0: // None
                break;
            case 1: // Work Camp
                LoadBuildingSetting(WorkCampSettings.gameObject, Config.BuildConfig.WorkCamp, new List<BuildingUpgrade> { Config.BuildConfig.WorkCampImproveUpgrade, Config.BuildConfig.WorkCampTradeUpgrade, Config.BuildConfig.WorkCampMerchantUpgrade});
                WorkCampSettings.Load();
                break;
            case 2: // LumberSite
                LoadBuildingSetting(LumberSiteSettings.gameObject, Config.BuildConfig.LumberSite, new List<BuildingUpgrade> { Config.BuildConfig.LumberSiteLodgeUpgrade, Config.BuildConfig.LumberSiteCarpenterUpgrade, Config.BuildConfig.LumberSiteGreenHouseUpgrade });
                LumberSiteSettings.Load();
                break;
            case 3: // Quarry
                LoadBuildingSetting(QuarrySettings.gameObject, Config.BuildConfig.Quarry, new List<BuildingUpgrade> { Config.BuildConfig.QuarryImproveUpgrade, Config.BuildConfig.QuarryDeepUpgrade, Config.BuildConfig.QuarryLeyLineUpgrade});
                QuarrySettings.Load();
                break;
            case 4: // CasterTower
                LoadBuildingSetting(CasterTowerSettings.gameObject, Config.BuildConfig.CasterTower, new List<BuildingUpgrade> { Config.BuildConfig.CasterTowerImproveUpgrade, Config.BuildConfig.CasterTowerForceUpgrade, Config.BuildConfig.CasterTowerBuffUpgrade });
                CasterTowerSettings.Load();
                break;
            case 5: // BarrierTower
                LoadBuildingSetting(BarrierTowerSettings.gameObject, Config.BuildConfig.BarrierTower, new List<BuildingUpgrade> { Config.BuildConfig.BarrierTowerImproveUpgrade, Config.BuildConfig.BarrierTowerHealUpgrade, Config.BuildConfig.BarrierTowerBuffUpgrade });
                BarrierTowerSettings.Load();
                break;
            case 6: // DefenseEncampment
                LoadBuildingSetting(DefEncampSettings.gameObject, Config.BuildConfig.DefenseEncampment, new List<BuildingUpgrade> { Config.BuildConfig.DefenseEncampmentImproveUpgrade, Config.BuildConfig.DefenseEncampmentUnitsUpgrade, Config.BuildConfig.DefenseEncampmentLevelUpgrade });
                DefEncampSettings.Load();
                break;
            case 7: // Academy
                LoadBuildingSetting(AcademySettings.gameObject, Config.BuildConfig.Academy, new List<BuildingUpgrade> { Config.BuildConfig.AcademyImproveUpgrade, Config.BuildConfig.AcademyResearchUpgrade, Config.BuildConfig.AcademyEfficiencyUpgrade });
                AcademySettings.Load();
                break;
            case 8: // DarkMagicTower
                LoadBuildingSetting(DarkMagicTowerSettings.gameObject, Config.BuildConfig.DarkMagicTower, new List<BuildingUpgrade> { Config.BuildConfig.DarkMagicTowerImproveUpgrade, Config.BuildConfig.DarkMagicTowerSoulUpgrade, Config.BuildConfig.DarkMagicTowerAfflictionUpgrade });
                DarkMagicTowerSettings.Load();
                break;
            case 9: // TemporalTower
                LoadBuildingSetting(TemporalTowerSettings.gameObject, Config.BuildConfig.TemporalTower, new List<BuildingUpgrade> { Config.BuildConfig.TemporalTowerImproveUpgrade, Config.BuildConfig.TemporalTowerTuneUpgrade, Config.BuildConfig.TemporalTowerDisruptUpgrade });
                TemporalTowerSettings.Load();
                break;
            case 10: // Laboratory
                LoadBuildingSetting(LaboratorySettings.gameObject, Config.BuildConfig.Laboratory, new List<BuildingUpgrade> { Config.BuildConfig.LaboratoryImproveUpgrade, Config.BuildConfig.LaboratoryIngredientUpgrade, Config.BuildConfig.LaboratoryBoostUpgrade });
                LaboratorySettings.Load();
                break;
            case 11: // Teleporter
                LoadBuildingSetting(TeleporterSettings.gameObject, Config.BuildConfig.Teleporter, new List<BuildingUpgrade> { Config.BuildConfig.TeleporterStoneUpgrade, Config.BuildConfig.TeleporterAncientUpgrade, Config.BuildConfig.TeleporterCapacityUpgrade });
                TeleporterSettings.Load();
                break;
            case 12: // TownHall
                LoadBuildingSetting(TownHallSettings.gameObject, Config.BuildConfig.TownHall, new List<BuildingUpgrade> { Config.BuildConfig.TownHallManualUpgrade, Config.BuildConfig.TownHallPrefabUpgrade, Config.BuildConfig.TownHallManaStoneUpgrade });
                TownHallSettings.Load();
                break;
            default:
                break;
        }
        LoadUpgrades();
        currentDropdownValue = BuildingDropdown.value;
    }

    private void SetText(ConstructionResources resources)
    {
        WoodCost.text = resources.Wood.ToString();
        StoneCost.text = resources.Wood.ToString();
        WoodCost.text = resources.Stone.ToString();
        NaturalMaterialsCost.text = resources.NaturalMaterials.ToString();
        OresCost.text = resources.Ores.ToString();
        PrefabsCost.text = resources.Prefabs.ToString();
        ManaStonesCost.text = resources.ManaStones.ToString();
    }
    private void SaveText(ConstructionResources resources)
    {
        resources.SetResources(
            int.TryParse(WoodCost.text, out int wc) ? wc : 0,
            int.TryParse(StoneCost.text, out int sc) ? sc : 0,
            int.TryParse(NaturalMaterialsCost.text, out int nmc) ? nmc : 0,
            int.TryParse(OresCost.text, out int oc) ? oc : 0,
            int.TryParse(PrefabsCost.text, out int pc) ? pc : 0,
            int.TryParse(ManaStonesCost.text, out int msc) ? msc : 0
            );
    }

    private void LoadUpgrades()
    {
        foreach (var upgrade in buildingUpgrades)
        {
            var obj = Instantiate(UpgradeEditPrefab, UpgradeFolder);
            var currentPrefab = obj.GetComponent<UpgradeEditorPrefab>();
            currentPrefab.UpgradeTurns.text = upgrade.upgradeTime.ToString();
            currentPrefab.UpgradeName.text = upgrade.Name.ToString();
            currentPrefab.UpgradeDesc.text = upgrade.Desc.ToString();
            currentPrefab.GoldCost.text = upgrade.GoldCost.ToString();
            currentPrefab.WoodCost.text = upgrade.ResourceToUpgrade.Wood.ToString();
            currentPrefab.NaturalMaterialsCost.text = upgrade.ResourceToUpgrade.NaturalMaterials.ToString();
            currentPrefab.PrefabsCost.text = upgrade.ResourceToUpgrade.Prefabs.ToString();
            currentPrefab.StoneCost.text = upgrade.ResourceToUpgrade.Stone.ToString();
            currentPrefab.OresCost.text = upgrade.ResourceToUpgrade.Ores.ToString();
            currentPrefab.ManaStonesCost.text = upgrade.ResourceToUpgrade.ManaStones.ToString();
            currentPrefab.AssociatedUpgrade = upgrade;
        }

    }
    private void SaveUpgrades()
    {
        int children = UpgradeFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            var currentPrefab = UpgradeFolder.GetChild(i).gameObject.GetComponent<UpgradeEditorPrefab>();
            currentPrefab.AssociatedUpgrade.upgradeTime = int.TryParse(currentPrefab.UpgradeTurns.text, out int ut) ? ut : 2;
            currentPrefab.AssociatedUpgrade.GoldCost = int.TryParse(currentPrefab.GoldCost.text, out int gc) ? gc : 50;
            currentPrefab.AssociatedUpgrade.ResourceToUpgrade.Wood = int.TryParse(currentPrefab.WoodCost.text, out int wc) ? wc : 50;
            currentPrefab.AssociatedUpgrade.ResourceToUpgrade.NaturalMaterials = int.TryParse(currentPrefab.NaturalMaterialsCost.text, out int nmc) ? nmc : 50;
            currentPrefab.AssociatedUpgrade.ResourceToUpgrade.Prefabs = int.TryParse(currentPrefab.PrefabsCost.text, out int pc) ? pc : 50;
            currentPrefab.AssociatedUpgrade.ResourceToUpgrade.Stone = int.TryParse(currentPrefab.StoneCost.text, out int sc) ? sc : 50;
            currentPrefab.AssociatedUpgrade.ResourceToUpgrade.Ores = int.TryParse(currentPrefab.OresCost.text, out int oc) ? oc : 50;
            currentPrefab.AssociatedUpgrade.ResourceToUpgrade.ManaStones = int.TryParse(currentPrefab.ManaStonesCost.text, out int msc) ? msc : 50;

        }
    }
    private void ClearFolder()
    {
        int children = UpgradeFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(UpgradeFolder.GetChild(i).gameObject);
        }
    }

    private void LoadBuildingSetting(GameObject SettingsPage, GeneralBuildingConfig building, List<BuildingUpgrade> upgrades)
    {
        SetText(building.Resources);
        GoldCost.text = building.Gold.ToString();
        ConstructionTurns.text = building.BuildTime.ToString();
        BuildLimit.text = building.BuildLimit.ToString();
        AICanBuild.isOn = building.AICanBuild;
        foreach (BuildingUpgrade upgrade in upgrades)
        {
            buildingUpgrades.Add(upgrade);
        }
        SettingsPage.SetActive(true);
    }
    internal void SoftSave(){
        Config.BuildConfig.BuildingSystemEnabled = BuildingSystemEnabled.isOn;
        Config.BuildConfig.BuildingSystemTurnLockout = int.TryParse(BuildingSystemTurnLockout.text, out int bstl) ? bstl : 5;
        Config.BuildConfig.BuildingPassiveRange = int.TryParse(BuildingPassiveRange.text, out int bpr) ? bpr : 3;
        Config.BuildConfig.BuildingCaptureTurns = int.TryParse(BuildingCaptureTurns.text, out int bct) ? bct : 2;
        Config.BuildConfig.EmpireBuildingCapture = EmpireCaptureDropdown.value;
        Config.BuildConfig.MonsterBuildingCapture = MonsterCaptureDropdown.value;
    }

    internal void HardSave()
    {
        var rootObject = new RootObject();

        rootObject.BuildingSystemEnabled = BuildingSystemEnabled.isOn;
        rootObject.BuildingSystemTurnLockout = int.TryParse(BuildingSystemTurnLockout.text, out int bstl) ? bstl : 5;
        rootObject.BuildingPassiveRange = int.TryParse(BuildingPassiveRange.text, out int bpr) ? bpr : 3;
        rootObject.CaptureTurns = int.TryParse(BuildingCaptureTurns.text, out int bct) ? bct : 2;
        rootObject.EmpireCapture = EmpireCaptureDropdown.value;
        rootObject.MonsterCapture = MonsterCaptureDropdown.value;

        rootObject.workCamp = new WorkCampTempClass(Config.BuildConfig.WorkCamp, Config.BuildConfig.WorkCampGoldPerTurn);
        rootObject.workCamp.stock = new ConstructionResourcesTempClass(Config.BuildConfig.WorkCampTurnStock);
        rootObject.workCamp.price = new ConstructionResourcesTempClass(Config.BuildConfig.WorkCampItemPrice);
        rootObject.workCamp.tradeUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.WorkCampTradeUpgrade);
        rootObject.workCamp.merchantUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.WorkCampMerchantUpgrade);
        rootObject.workCamp.improveUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.WorkCampImproveUpgrade);

        rootObject.lumberSite = new LumberCampTempClass(Config.BuildConfig.LumberSite, Config.BuildConfig.LumberSiteWorkerCap);
        rootObject.lumberSite.lodgeUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.LumberSiteLodgeUpgrade);
        rootObject.lumberSite.greenhouseUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.LumberSiteGreenHouseUpgrade);
        rootObject.lumberSite.carpenterUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.LumberSiteCarpenterUpgrade);
        
        rootObject.lumberSite = new LumberCampTempClass(Config.BuildConfig.LumberSite, Config.BuildConfig.LumberSiteWorkerCap);
        rootObject.lumberSite.lodgeUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.LumberSiteLodgeUpgrade);
        rootObject.lumberSite.greenhouseUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.LumberSiteGreenHouseUpgrade);
        rootObject.lumberSite.carpenterUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.LumberSiteCarpenterUpgrade);

        rootObject.quarry = new QuarryTempClass(Config.BuildConfig.Quarry, Config.BuildConfig.QuarryStoneMin, Config.BuildConfig.QuarryStoneMax, Config.BuildConfig.QuarryOreMin, Config.BuildConfig.QuarryOreMax, Config.BuildConfig.QuarryMSMin, Config.BuildConfig.QuarryMSMax, Config.BuildConfig.QuarryGoldMin, Config.BuildConfig.QuarryGoldMax);
        rootObject.quarry.improveUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.QuarryImproveUpgrade);
        rootObject.quarry.deepUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.QuarryDeepUpgrade);
        rootObject.quarry.leyLineUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.QuarryLeyLineUpgrade);

        rootObject.casterTower = new CasterTowerTempClass(Config.BuildConfig.CasterTower, Config.BuildConfig.CasterTowerManaChargesMax, Config.BuildConfig.CasterTowerManaChargesRegen, Config.BuildConfig.CasterTowerBaseChargeCost, Config.BuildConfig.CasterTowerBetterTierChargeCost, Config.BuildConfig.CasterTowerBuffChargeCost);
        rootObject.casterTower.improveUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.CasterTowerImproveUpgrade);
        rootObject.casterTower.forceUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.CasterTowerForceUpgrade);
        rootObject.casterTower.buffUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.CasterTowerBuffUpgrade);

        rootObject.barrierTower = new BarrierTowerTempClass(Config.BuildConfig.BarrierTower, Config.BuildConfig.BarrierTowerBaseBarrierStrength, Config.BuildConfig.BarrierTowerIgnoreDowntime);
        rootObject.barrierTower.improveUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.BarrierTowerImproveUpgrade);
        rootObject.barrierTower.healUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.BarrierTowerHealUpgrade);
        rootObject.barrierTower.buffUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.BarrierTowerBuffUpgrade);

        rootObject.defEncamp = new DefEncampTempClass(Config.BuildConfig.DefenseEncampment, Config.BuildConfig.DefenseEncampmentArmyPercentage, Config.BuildConfig.DefenseEncampmentUnitScale, Config.BuildConfig.DefenseEncampmentMaxGarrisonSizeScale, Config.BuildConfig.DefenseEncampmentTrainTime);
        rootObject.defEncamp.improveUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.DefenseEncampmentImproveUpgrade);
        rootObject.defEncamp.levelUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.DefenseEncampmentLevelUpgrade);
        rootObject.defEncamp.unitUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.DefenseEncampmentUnitsUpgrade);

        rootObject.academy = new AcademyTempClass(Config.BuildConfig.Academy, Config.BuildConfig.AcademyEXPPerGold, Config.BuildConfig.AcademyMaximumUpgrades, Config.BuildConfig.AcademyUpgradeCost, Config.BuildConfig.AcademyCostIncreaseMultPerUpgrade);
        rootObject.academy.improveUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.AcademyImproveUpgrade);
        rootObject.academy.effUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.AcademyEfficiencyUpgrade);
        rootObject.academy.researchUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.AcademyResearchUpgrade);

        rootObject.darkMagicTower = new DarkMagicTowerTempClass(Config.BuildConfig.DarkMagicTower, Config.BuildConfig.DarkMagicTowerDurationImprovement, Config.BuildConfig.DarkMagicTowerAccImprovement, Config.BuildConfig.DarkMagicTowerSoulPointBase, Config.BuildConfig.DarkMagicTowerSoulPointMult);
        rootObject.darkMagicTower.improveUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.DarkMagicTowerImproveUpgrade);
        rootObject.darkMagicTower.soulUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.DarkMagicTowerSoulUpgrade);
        rootObject.darkMagicTower.afflictUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.DarkMagicTowerAfflictionUpgrade);

        rootObject.temporalTower = new TemporalTowerTempClass(Config.BuildConfig.TemporalTower);
        rootObject.temporalTower.improveUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.TemporalTowerImproveUpgrade);
        rootObject.temporalTower.disruptUpgrade= new BuildingUpgradeTempClass(Config.BuildConfig.TemporalTowerDisruptUpgrade);
        rootObject.temporalTower.tuneUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.TemporalTowerTuneUpgrade);

        rootObject.laboratory = new LaboratoryTempClass(Config.BuildConfig.Laboratory, Config.BuildConfig.LaboratoryUpfrontCost, Config.BuildConfig.LaboratoryBaseUnitPrice, Config.BuildConfig.LaboratoryBulkDiscount, Config.BuildConfig.LaboratoryBulkMin, Config.BuildConfig.LaboratoryBulkMax, Config.BuildConfig.LaboratoryBaseRollCount, Config.BuildConfig.LaboratoryBaseTraitChance, Config.BuildConfig.LaboratoryAIPotionMult);
        rootObject.laboratory.improveUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.TemporalTowerImproveUpgrade);
        rootObject.laboratory.ingredientUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.LaboratoryIngredientUpgrade);
        rootObject.laboratory.boostUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.LaboratoryBoostUpgrade);

        rootObject.teleporter = new TeleporterTempClass(Config.BuildConfig.Teleporter, Config.BuildConfig.TeleporterMaxCapacity, Config.BuildConfig.TeleporterCapacityRegen, Config.BuildConfig.TeleporterPerUnitCapacity, Config.BuildConfig.TeleporterPerUnitCapacityMod);
        rootObject.teleporter.stoneUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.TeleporterStoneUpgrade);
        rootObject.teleporter.ancientUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.TeleporterAncientUpgrade);
        rootObject.teleporter.capacityUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.TeleporterCapacityUpgrade);

        rootObject.townHall = new TownHallTempClass(Config.BuildConfig.TownHall);
        rootObject.townHall.manualUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.TownHallManualUpgrade);
        rootObject.townHall.prefabUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.TownHallPrefabUpgrade);
        rootObject.townHall.manaStoneUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.TownHallManaStoneUpgrade);

        using (StreamWriter file = new StreamWriter($"{State.StorageDirectory}buildingConfig.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, rootObject);
        }

    }
    internal void HardLoad()
    {
        string json = File.ReadAllText($"{State.StorageDirectory}buildingConfig.json");
        JObject results = new JObject();
        try
        {
            results = JObject.Parse(json);
        }
        catch (Exception)
        {
            Debug.Log("Empty Building Settings JSON");
            return;
        }

        Config.BuildConfig.BuildingSystemEnabled = results["BuildingSystemEnabled"].ToObject<bool>();
        BuildingSystemEnabled.isOn = Config.BuildConfig.BuildingSystemEnabled;
        Config.BuildConfig.BuildingSystemTurnLockout = int.TryParse(results["BuildingSystemTurnLockout"].ToString(), out int wcbstl) ? wcbstl : 1;
        BuildingSystemTurnLockout.text = Config.BuildConfig.BuildingSystemTurnLockout.ToString();
        Config.BuildConfig.BuildingPassiveRange = int.TryParse(results["BuildingPassiveRange"].ToString(), out int bpr) ? bpr: 3;
        BuildingPassiveRange.text = Config.BuildConfig.BuildingPassiveRange.ToString();
        Config.BuildConfig.EmpireBuildingCapture = results["EmpireCapture"].ToObject<int>();
        EmpireCaptureDropdown.value = Config.BuildConfig.EmpireBuildingCapture;
        Config.BuildConfig.MonsterBuildingCapture = results["MonsterCapture"].ToObject<int>();
        MonsterCaptureDropdown.value = Config.BuildConfig.MonsterBuildingCapture;
        Config.BuildConfig.BuildingCaptureTurns = results["CaptureTurns"].ToObject<int>();
        BuildingCaptureTurns.text = Config.BuildConfig.BuildingCaptureTurns.ToString();


        LoadBuilding("workCamp", Config.BuildConfig.WorkCamp);
        Config.BuildConfig.WorkCampGoldPerTurn = results["workCamp"]["goldPerTurn"].ToObject<int>();
        LoadResource("workCamp", "stock", Config.BuildConfig.WorkCampTurnStock);
        LoadResource("workCamp", "price", Config.BuildConfig.WorkCampItemPrice);
        LoadUpgrade("workCamp", "tradeUpgrade", Config.BuildConfig.WorkCampTradeUpgrade);
        LoadUpgrade("workCamp", "merchantUpgrade", Config.BuildConfig.WorkCampMerchantUpgrade);
        LoadUpgrade("workCamp", "improveUpgrade", Config.BuildConfig.WorkCampImproveUpgrade);

        LoadBuilding("lumberSite", Config.BuildConfig.LumberSite);
        Config.BuildConfig.LumberSiteWorkerCap = results["lumberSite"]["workerCap"].ToObject<int>();
        LoadUpgrade("lumberSite", "lodgeUpgrade", Config.BuildConfig.LumberSiteLodgeUpgrade);
        LoadUpgrade("lumberSite", "greenhouseUpgrade", Config.BuildConfig.LumberSiteGreenHouseUpgrade);
        LoadUpgrade("lumberSite", "carpenterUpgrade", Config.BuildConfig.LumberSiteCarpenterUpgrade);

        LoadBuilding("quarry", Config.BuildConfig.LumberSite);
        Config.BuildConfig.QuarryStoneMin = results["quarry"]["stoneMin"].ToObject<int>();
        Config.BuildConfig.QuarryStoneMax = results["quarry"]["stoneMax"].ToObject<int>();
        Config.BuildConfig.QuarryOreMin = results["quarry"]["oreMin"].ToObject<int>();
        Config.BuildConfig.QuarryOreMax = results["quarry"]["oreMax"].ToObject<int>();
        Config.BuildConfig.QuarryMSMin = results["quarry"]["msMin"].ToObject<int>();
        Config.BuildConfig.QuarryMSMax = results["quarry"]["msMax"].ToObject<int>();
        Config.BuildConfig.QuarryGoldMin = results["quarry"]["goldMin"].ToObject<int>();
        Config.BuildConfig.QuarryGoldMax = results["quarry"]["goldMax"].ToObject<int>();
        LoadUpgrade("quarry", "improveUpgrade", Config.BuildConfig.QuarryImproveUpgrade);
        LoadUpgrade("quarry", "deepUpgrade", Config.BuildConfig.QuarryDeepUpgrade);
        LoadUpgrade("quarry", "leyLineUpgrade", Config.BuildConfig.QuarryLeyLineUpgrade);

        LoadBuilding("casterTower", Config.BuildConfig.CasterTower);
        Config.BuildConfig.CasterTowerManaChargesMax= results["casterTower"]["chargeMax"].ToObject<int>();
        Config.BuildConfig.CasterTowerManaChargesRegen= results["casterTower"]["chargeRegen"].ToObject<int>();
        Config.BuildConfig.CasterTowerBaseChargeCost= results["casterTower"]["chargeBaseCost"].ToObject<int>();
        Config.BuildConfig.CasterTowerBetterTierChargeCost= results["casterTower"]["chargeBetterCost"].ToObject<int>();
        Config.BuildConfig.CasterTowerBuffChargeCost= results["casterTower"]["chargeBuffCost"].ToObject<int>();
        LoadUpgrade("casterTower", "improveUpgrade", Config.BuildConfig.CasterTowerImproveUpgrade);
        LoadUpgrade("casterTower", "forceUpgrade", Config.BuildConfig.CasterTowerForceUpgrade);
        LoadUpgrade("casterTower", "buffUpgrade", Config.BuildConfig.CasterTowerBuffUpgrade);

        LoadBuilding("barrierTower", Config.BuildConfig.BarrierTower);
        Config.BuildConfig.BarrierTowerBaseBarrierStrength = results["barrierTower"]["baseBarrierStrength"].ToObject<int>();
        Config.BuildConfig.BarrierTowerIgnoreDowntime = results["barrierTower"]["ignoreDowntime"].ToObject<bool>();
        LoadUpgrade("barrierTower", "improveUpgrade", Config.BuildConfig.BarrierTowerImproveUpgrade);
        LoadUpgrade("barrierTower", "healUpgrade", Config.BuildConfig.BarrierTowerHealUpgrade);
        LoadUpgrade("barrierTower", "buffUpgrade", Config.BuildConfig.BarrierTowerBuffUpgrade);

        LoadBuilding("defEncamp", Config.BuildConfig.DefenseEncampment);
        Config.BuildConfig.DefenseEncampmentArmyPercentage = results["defEncamp"]["armyPercentage"].ToObject<float>();
        Config.BuildConfig.DefenseEncampmentUnitScale = results["defEncamp"]["unitScale"].ToObject<float>();
        Config.BuildConfig.DefenseEncampmentMaxGarrisonSizeScale = results["defEncamp"]["garrisonSizeScale"].ToObject<float>();
        Config.BuildConfig.DefenseEncampmentTrainTime = results["defEncamp"]["trainTime"].ToObject<int>();
        LoadUpgrade("defEncamp", "improveUpgrade", Config.BuildConfig.DefenseEncampmentImproveUpgrade);
        LoadUpgrade("defEncamp", "levelUpgrade", Config.BuildConfig.DefenseEncampmentLevelUpgrade);
        LoadUpgrade("defEncamp", "unitUpgrade", Config.BuildConfig.DefenseEncampmentUnitsUpgrade);

        LoadBuilding("academy", Config.BuildConfig.Academy);
        Config.BuildConfig.AcademyEXPPerGold = results["academy"]["expGold"].ToObject<int>();
        Config.BuildConfig.AcademyMaximumUpgrades = results["academy"]["maxUpgrades"].ToObject<int>();
        Config.BuildConfig.AcademyUpgradeCost = results["academy"]["upgradeCost"].ToObject<int>();
        Config.BuildConfig.AcademyCostIncreaseMultPerUpgrade = results["academy"]["costInc"].ToObject<float>();
        LoadUpgrade("academy", "improveUpgrade", Config.BuildConfig.AcademyImproveUpgrade);
        LoadUpgrade("academy", "researchUpgrade", Config.BuildConfig.AcademyResearchUpgrade);
        LoadUpgrade("academy", "effUpgrade", Config.BuildConfig.AcademyEfficiencyUpgrade);

        LoadBuilding("darkMagicTower", Config.BuildConfig.DarkMagicTower);
        Config.BuildConfig.DarkMagicTowerDurationImprovement = results["darkMagicTower"]["durImprovement"].ToObject<int>();
        Config.BuildConfig.DarkMagicTowerAccImprovement = results["darkMagicTower"]["accImprovement"].ToObject<int>();
        Config.BuildConfig.DarkMagicTowerSoulPointBase= results["darkMagicTower"]["soulPointBase"].ToObject<int>();
        Config.BuildConfig.DarkMagicTowerSoulPointMult = results["darkMagicTower"]["soulPointMult"].ToObject<float>();
        LoadUpgrade("darkMagicTower", "improveUpgrade", Config.BuildConfig.DarkMagicTowerImproveUpgrade);
        LoadUpgrade("darkMagicTower", "soulUpgrade", Config.BuildConfig.DarkMagicTowerSoulUpgrade);
        LoadUpgrade("darkMagicTower", "afflictUpgrade", Config.BuildConfig.DarkMagicTowerAfflictionUpgrade);

        LoadBuilding("temporalTower", Config.BuildConfig.TemporalTower);
        LoadUpgrade("temporalTower", "improveUpgrade", Config.BuildConfig.TemporalTowerImproveUpgrade);
        LoadUpgrade("temporalTower", "disruptUpgrade", Config.BuildConfig.TemporalTowerDisruptUpgrade);
        LoadUpgrade("temporalTower", "tuneUpgrade", Config.BuildConfig.TemporalTowerTuneUpgrade);

        LoadBuilding("laboratory", Config.BuildConfig.Laboratory);
        Config.BuildConfig.LaboratoryUpfrontCost = results["laboratory"]["upfrontCost"].ToObject<int>();
        Config.BuildConfig.LaboratoryBaseUnitPrice = results["laboratory"]["baseUnitPrice"].ToObject<int>();
        Config.BuildConfig.LaboratoryBulkDiscount = results["laboratory"]["bulkDiscount"].ToObject<float>();
        Config.BuildConfig.LaboratoryBulkMin = results["laboratory"]["bulkMin"].ToObject<int>();
        Config.BuildConfig.LaboratoryBulkMax = results["laboratory"]["bulkMax"].ToObject<int>();
        Config.BuildConfig.LaboratoryBaseRollCount = results["laboratory"]["baseRollCount"].ToObject<int>();
        Config.BuildConfig.LaboratoryBaseTraitChance = results["laboratory"]["baseTraitChance"].ToObject<float>();
        LoadUpgrade("laboratory", "improveUpgrade", Config.BuildConfig.LaboratoryImproveUpgrade);
        LoadUpgrade("laboratory", "ingredientUpgrade", Config.BuildConfig.LaboratoryIngredientUpgrade);
        LoadUpgrade("laboratory", "boostUpgrade", Config.BuildConfig.LaboratoryBoostUpgrade);

        LoadBuilding("teleporter", Config.BuildConfig.Teleporter);
        Config.BuildConfig.TeleporterMaxCapacity = results["teleporter"]["maxCapacity"].ToObject<float>();
        Config.BuildConfig.TeleporterCapacityRegen = results["teleporter"]["capRegen"].ToObject<float>();
        Config.BuildConfig.TeleporterPerUnitCapacity = results["teleporter"]["perUnitCap"].ToObject<bool>();
        Config.BuildConfig.TeleporterPerUnitCapacityMod = results["teleporter"]["perUnitCapMod"].ToObject<float>();
        LoadUpgrade("teleporter", "stoneUpgrade", Config.BuildConfig.TeleporterStoneUpgrade);
        LoadUpgrade("teleporter", "capacityUpgrade", Config.BuildConfig.TeleporterCapacityUpgrade);
        LoadUpgrade("teleporter", "ancientUpgrade", Config.BuildConfig.TeleporterAncientUpgrade);

        LoadBuilding("townHall", Config.BuildConfig.TownHall);
        LoadUpgrade("townHall", "manualUpgrade", Config.BuildConfig.TownHallManualUpgrade);
        LoadUpgrade("townHall", "prefabUpgrade", Config.BuildConfig.TownHallPrefabUpgrade);
        LoadUpgrade("townHall", "manaStoneUpgrade", Config.BuildConfig.TownHallManaStoneUpgrade);

        void LoadBuilding(string buildingName, GeneralBuildingConfig BuildingObect)
        {
            BuildingObect.Gold = results[buildingName]["standardInfo"]["gold"].ToObject<int>();
            BuildingObect.BuildTime = results[buildingName]["standardInfo"]["time"].ToObject<int>();
            BuildingObect.BuildLimit = results[buildingName]["standardInfo"]["limit"].ToObject<int>();
            BuildingObect.AICanBuild = results[buildingName]["standardInfo"]["aicanbuild"].ToObject<bool>();
            BuildingObect.Resources.SetResources(
                results[buildingName]["standardInfo"]["resource"]["Wood"].ToObject<int>(),
                results[buildingName]["standardInfo"]["resource"]["Stone"].ToObject<int>(),
                results[buildingName]["standardInfo"]["resource"]["NaturalMaterials"].ToObject<int>(),
                results[buildingName]["standardInfo"]["resource"]["Ores"].ToObject<int>(),
                results[buildingName]["standardInfo"]["resource"]["Prefabs"].ToObject<int>(),
                results[buildingName]["standardInfo"]["resource"]["ManaStones"].ToObject<int>());
        }

        void LoadResource(string building, string substring, ConstructionResources resources)
        {
            resources.SetResources(
                results[building][substring]["Wood"].ToObject<int>(),
                results[building][substring]["Stone"].ToObject<int>(),
                results[building][substring]["NaturalMaterials"].ToObject<int>(),
                results[building][substring]["Ores"].ToObject<int>(),
                results[building][substring]["Prefabs"].ToObject<int>(),
                results[building][substring]["ManaStones"].ToObject<int>());
        }

        void LoadUpgrade(string building, string substring, BuildingUpgrade upgrade)
        {
            upgrade.GoldCost = results[building][substring]["gold"].ToObject<int>();
            upgrade.upgradeTime = results[building][substring]["time"].ToObject<int>();
            upgrade.ResourceToUpgrade.SetResources(
                results[building][substring]["resource"]["Wood"].ToObject<int>(),
                results[building][substring]["resource"]["Stone"].ToObject<int>(),
                results[building][substring]["resource"]["NaturalMaterials"].ToObject<int>(),
                results[building][substring]["resource"]["Ores"].ToObject<int>(),
                results[building][substring]["resource"]["Prefabs"].ToObject<int>(),
                results[building][substring]["resource"]["ManaStones"].ToObject<int>());
        }

    }



    class RootObject
    {
        public bool BuildingSystemEnabled { get; set; }
        public int BuildingSystemTurnLockout { get; set; }
        public int BuildingPassiveRange { get; set; }
        public int EmpireCapture { get; set; }
        public int MonsterCapture { get; set; }
        public int CaptureTurns { get; set; }
        public WorkCampTempClass workCamp { get; set; }
        public LumberCampTempClass lumberSite { get; set; }
        public QuarryTempClass quarry { get; set; }
        public CasterTowerTempClass casterTower { get; set; }
        public BarrierTowerTempClass barrierTower { get; set; }
        public DefEncampTempClass defEncamp { get; set; }
        public AcademyTempClass academy { get; set; }
        public DarkMagicTowerTempClass darkMagicTower { get; set; }
        public TemporalTowerTempClass temporalTower { get; set; }
        public LaboratoryTempClass laboratory { get; set; }
        public TeleporterTempClass teleporter { get; set; }
        public TownHallTempClass townHall { get; set; }

    }

    class BuildingStandardInherit
    {
        public BuildingStandardTempClass standardInfo { get; set; }
    }

    class BuildingStandardTempClass
    {
        public int time { get; set; }
        public int gold { get; set; }
        public int limit { get; set; }
        public bool aicanbuild { get; set; }
        public ConstructionResourcesTempClass resource { get; set; }

        internal BuildingStandardTempClass(int time, int gold, int limit, bool aibuild, ConstructionResources resource)
        {
            this.time = time;
            this.gold = gold;
            this.limit = limit;
            this.aicanbuild = aibuild;
            this.resource = new ConstructionResourcesTempClass(resource);
        }
    }

    class WorkCampTempClass : BuildingStandardInherit
    {
        public int goldPerTurn { get; set; }
        public ConstructionResourcesTempClass stock { get; set; }
        public ConstructionResourcesTempClass price { get; set; }
        public BuildingUpgradeTempClass tradeUpgrade { get; set; }
        public BuildingUpgradeTempClass merchantUpgrade { get; set; }
        public BuildingUpgradeTempClass improveUpgrade { get; set; }
        internal WorkCampTempClass(GeneralBuildingConfig configClass, int goldPerTurn)
        {
            standardInfo = new BuildingStandardTempClass(configClass.BuildTime, configClass.Gold, configClass.BuildLimit, configClass.AICanBuild, configClass.Resources);
            this.goldPerTurn = goldPerTurn;
        }
    }
    class LumberCampTempClass : BuildingStandardInherit
    {
        public int workerCap { get; set; }
        public BuildingUpgradeTempClass lodgeUpgrade { get; set; }
        public BuildingUpgradeTempClass greenhouseUpgrade { get; set; }
        public BuildingUpgradeTempClass carpenterUpgrade { get; set; }
        internal LumberCampTempClass(GeneralBuildingConfig configClass, int cap)
        {
            standardInfo = new BuildingStandardTempClass(configClass.BuildTime, configClass.Gold, configClass.BuildLimit, configClass.AICanBuild, configClass.Resources);
            this.workerCap = cap;
        }
    }
    class QuarryTempClass : BuildingStandardInherit
    {
        public int stoneMin { get; set; }
        public int stoneMax { get; set; }
        public int oreMin { get; set; }
        public int oreMax { get; set; }
        public int msMin { get; set; }
        public int msMax { get; set; }
        public int goldMin { get; set; }
        public int goldMax { get; set; }
        public BuildingUpgradeTempClass improveUpgrade { get; set; }
        public BuildingUpgradeTempClass deepUpgrade { get; set; }
        public BuildingUpgradeTempClass leyLineUpgrade { get; set; }
        internal QuarryTempClass(GeneralBuildingConfig configClass, int sMin, int sMax, int oMin, int oMax, int mMin, int mMax, int gMin, int gMax)
        {
            standardInfo = new BuildingStandardTempClass(configClass.BuildTime, configClass.Gold, configClass.BuildLimit, configClass.AICanBuild, configClass.Resources);
            stoneMin = sMin;
            stoneMax = sMax;
            oreMin = oMin;
            oreMax = oMax;
            msMin = mMin;
            msMax = mMax;
            goldMin = gMin;
            goldMax = gMax;
        }
    }
    class CasterTowerTempClass : BuildingStandardInherit
    {
        public int chargeMax { get; set; }
        public int chargeRegen { get; set; }
        public int chargeBaseCost { get; set; }
        public int chargeBetterCost { get; set; }
        public int chargeBuffCost { get; set; }
        public BuildingUpgradeTempClass improveUpgrade { get; set; }
        public BuildingUpgradeTempClass forceUpgrade { get; set; }
        public BuildingUpgradeTempClass buffUpgrade { get; set; }
        internal CasterTowerTempClass(GeneralBuildingConfig configClass, int cMax,int cRegen,int cBaseCost, int cBetterCost, int cBuffCost)
        {
            standardInfo = new BuildingStandardTempClass(configClass.BuildTime, configClass.Gold, configClass.BuildLimit, configClass.AICanBuild, configClass.Resources);
            chargeMax = cMax;
            chargeRegen = cRegen;
            chargeBaseCost = cBaseCost;
            chargeBetterCost = cBetterCost;
            chargeBuffCost = cBuffCost;
        }
    }
    class BarrierTowerTempClass : BuildingStandardInherit
    {
        public int baseBarrierStrength { get; set; }
        public bool ignoreDowntime { get; set; }
        public BuildingUpgradeTempClass improveUpgrade { get; set; }
        public BuildingUpgradeTempClass healUpgrade { get; set; }
        public BuildingUpgradeTempClass buffUpgrade { get; set; }
        internal BarrierTowerTempClass(GeneralBuildingConfig configClass, int baseStr,bool ignoreDT)
        {
            standardInfo = new BuildingStandardTempClass(configClass.BuildTime, configClass.Gold, configClass.BuildLimit, configClass.AICanBuild, configClass.Resources);
            baseBarrierStrength = baseStr;
            ignoreDowntime = ignoreDT;
        }
    }
    class DefEncampTempClass : BuildingStandardInherit
    {
        public float armyPercentage { get; set; }
        public float unitScale { get; set; }
        public float garrisonSizeScale { get; set; }
        public int trainTime { get; set; }
        public BuildingUpgradeTempClass improveUpgrade { get; set; }
        public BuildingUpgradeTempClass unitUpgrade { get; set; }
        public BuildingUpgradeTempClass levelUpgrade { get; set; }
        internal DefEncampTempClass(GeneralBuildingConfig configClass, float armyPct, float uScale, float garScale, int tTime)
        {
            standardInfo = new BuildingStandardTempClass(configClass.BuildTime, configClass.Gold, configClass.BuildLimit, configClass.AICanBuild, configClass.Resources);
            armyPercentage = armyPct;
            unitScale = uScale;
            garrisonSizeScale = garScale;
            trainTime = tTime;
        }
    }
    class AcademyTempClass : BuildingStandardInherit
    {
        public int expGold { get; set; }
        public int maxUpgrades { get; set; }
        public int upgradeCost { get; set; }
        public float costInc { get; set; }
        public BuildingUpgradeTempClass improveUpgrade { get; set; }
        public BuildingUpgradeTempClass researchUpgrade { get; set; }
        public BuildingUpgradeTempClass effUpgrade { get; set; }
        internal AcademyTempClass(GeneralBuildingConfig configClass, int expG, int mUpgrade, int upgCost, float costMult)
        {
            standardInfo = new BuildingStandardTempClass(configClass.BuildTime, configClass.Gold, configClass.BuildLimit, configClass.AICanBuild, configClass.Resources);
            expGold = expG;
            maxUpgrades = mUpgrade;
            upgradeCost = upgradeCost;
            costInc = costMult;
        }
    }
    class DarkMagicTowerTempClass : BuildingStandardInherit
    {
        public int durImprovement { get; set; }
        public int accImprovement { get; set; }
        public int soulPointBase{ get; set; }
        public float soulPointMult { get; set; }
        public BuildingUpgradeTempClass improveUpgrade { get; set; }
        public BuildingUpgradeTempClass afflictUpgrade { get; set; }
        public BuildingUpgradeTempClass soulUpgrade { get; set; }
        internal DarkMagicTowerTempClass(GeneralBuildingConfig configClass, int durImp, int accImp, int spBase, float spMult)
        {
            standardInfo = new BuildingStandardTempClass(configClass.BuildTime, configClass.Gold, configClass.BuildLimit, configClass.AICanBuild, configClass.Resources);
            durImprovement = durImp;
            accImprovement = accImp;
            soulPointBase = spBase;
            soulPointMult = spMult;
        }
    }
    class TemporalTowerTempClass : BuildingStandardInherit
    {
        public BuildingUpgradeTempClass improveUpgrade { get; set; }
        public BuildingUpgradeTempClass tuneUpgrade { get; set; }
        public BuildingUpgradeTempClass disruptUpgrade { get; set; }
        internal TemporalTowerTempClass(GeneralBuildingConfig configClass)
        {
            standardInfo = new BuildingStandardTempClass(configClass.BuildTime, configClass.Gold, configClass.BuildLimit, configClass.AICanBuild, configClass.Resources);
        }
    }
    class LaboratoryTempClass : BuildingStandardInherit
    {
        public int upfrontCost { get; set; }
        public int baseUnitPrice { get; set; }
        public float bulkDiscount { get; set; }
        public int bulkMin { get; set; }
        public int bulkMax { get; set; }
        public int baseRollCount { get; set; }
        public float baseTraitChance { get; set; }
        public int AIPotionMult { get; set; }
        public BuildingUpgradeTempClass improveUpgrade { get; set; }
        public BuildingUpgradeTempClass ingredientUpgrade { get; set; }
        public BuildingUpgradeTempClass boostUpgrade { get; set; }
        internal LaboratoryTempClass(GeneralBuildingConfig configClass, int upCost, int uPrice, float disc, int bMin, int bMax, int rCount, float tChance, int aim)
        {
            standardInfo = new BuildingStandardTempClass(configClass.BuildTime, configClass.Gold, configClass.BuildLimit, configClass.AICanBuild, configClass.Resources);
            upfrontCost = upCost;
            baseUnitPrice = uPrice;
            bulkDiscount = disc;
            bulkMin = bMin;
            bulkMax = bMax;
            baseRollCount = rCount;
            baseTraitChance = tChance;
            AIPotionMult = aim;
        }
    }
    class TeleporterTempClass : BuildingStandardInherit
    {
        public float maxCapacity { get; set; }
        public float capRegen { get; set; }
        public bool perUnitCap { get; set; }
        public float perUnitCapMod { get; set; }
        public BuildingUpgradeTempClass stoneUpgrade { get; set; }
        public BuildingUpgradeTempClass ancientUpgrade { get; set; }
        public BuildingUpgradeTempClass capacityUpgrade { get; set; }
        internal TeleporterTempClass(GeneralBuildingConfig configClass, float maxCap, float capReg, bool pUnit, float cMod)
        {
            standardInfo = new BuildingStandardTempClass(configClass.BuildTime, configClass.Gold, configClass.BuildLimit, configClass.AICanBuild, configClass.Resources);
            maxCapacity = maxCap;
            capRegen = capReg;
            perUnitCap = pUnit;
            perUnitCapMod = cMod;
        }
    }
    class TownHallTempClass : BuildingStandardInherit
    {
        public BuildingUpgradeTempClass manualUpgrade { get; set; }
        public BuildingUpgradeTempClass prefabUpgrade { get; set; }
        public BuildingUpgradeTempClass manaStoneUpgrade { get; set; }
        internal TownHallTempClass(GeneralBuildingConfig configClass)
        {
            standardInfo = new BuildingStandardTempClass(configClass.BuildTime, configClass.Gold, configClass.BuildLimit, configClass.AICanBuild, configClass.Resources);
        }
    }
    class BuildingUpgradeTempClass
    {
        public string name { get; set; }
        public string desc { get; set; }
        public int time { get; set; }
        public int gold { get; set; }
        public ConstructionResourcesTempClass resource { get; set; }
        internal BuildingUpgradeTempClass(BuildingUpgrade buildingUpgrade)
        {
            this.name = buildingUpgrade.Name;
            this.desc = buildingUpgrade.Desc;
            this.time = buildingUpgrade.upgradeTime;
            this.gold = buildingUpgrade.GoldCost;
            this.resource = new ConstructionResourcesTempClass(buildingUpgrade.ResourceToUpgrade);
        }
    }
    class ConstructionResourcesTempClass
    {
        public int Wood { get; set; }
        public int Stone { get; set; }
        public int NaturalMaterials { get; set; }
        public int Ores { get; set; }
        public int Prefabs { get; set; }
        public int ManaStones { get; set; }

        internal ConstructionResourcesTempClass(ConstructionResources incoming)
        {
            Wood = incoming.Wood;
            Stone = incoming.Stone;
            NaturalMaterials = incoming.NaturalMaterials;
            Ores = incoming.Ores;
            Prefabs = incoming.Prefabs;
            ManaStones = incoming.ManaStones;
        }
    }
}
