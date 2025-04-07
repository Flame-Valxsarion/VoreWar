using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class Recruit_Mode : SceneBase
{
    Unit leader;
    Village village;
    MercenaryHouse mercenaryHouse;
    Army army;
    Empire empire;
    internal UnitCustomizer Customizer;
    Shop shop;
    VillageView villageView;

    List<Unit> dismissList;
    Dictionary<Button,Unit> buttonList = new Dictionary<Button,Unit>();

    int selectedIndex;

    ActivatingEmpire activatingEmpire;

    InfoPanel infoPanel;

    public RecruitPanel RecruitUI;
    public ArmySectionsPanel ArmyUI;
    public ShopPanel ShopUI;
    public RenamePanel RenameUI;
    public LevelUpPanel LevelUpUI;
    public HirePanel HireUI;
    public MercenaryPanel MercenaryPanelUI;
    public MercenaryHousePanel MercenaryScreenUI;
    public CustomizerPanel CustomizerUI;
    public VillageViewPanel VillageUI;
    public GameObject BlockerUI;
    public BulkBuyPanel BulkBuyUI;
    public WeaponStocker WeaponStockerUI;
    public ConfigAutoLevelUpPanel ConfigAutoLevelUpUI;
    public RecruitUI RaceUI;
    public RecruitUI PopUI;
    public PotionInv potionInv;

    public RecruitCheatsPanel CheatMenu;

    public Button ShowBanner;
    public TMP_Dropdown BannerType;

    Actor_Unit[] displayUnits;
    Unit[] displayCreatedUnit;

    bool failedToMakeFriendlyArmy = false;

    public enum ActivatingEmpire
    {
        Self,
        Ally,
        Observer,
        Infiltrator
    }

    public void BeginWithoutVillage(Empire actingEmpire, Army startArmy, ActivatingEmpire activatingEmpire)
    {
        this.activatingEmpire = activatingEmpire;
        army = startArmy;
        InitializeBanners();
        village = null;
        mercenaryHouse = null;
        empire = actingEmpire;
        ArmyUI.ArmyName.text = army.Name;
        RecruitUI.gameObject.SetActive(false);
        MercenaryPanelUI.gameObject.SetActive(false);
        leader = army.LeaderIfInArmy();
        ArmyUI.AlliedArmyText.gameObject.SetActive(false);
        ArmyUI.ShopText.text = "Inventory";
        SetUpDisplay();
        dismissList = new List<Unit>();
    }

    public void BeginWithMercenaries(MercenaryHouse merc, Empire actingEmpire, Army startArmy, ActivatingEmpire activatingEmpire)
    {
        mercenaryHouse = merc;
        this.activatingEmpire = activatingEmpire;
        army = startArmy;
        ArmyUI.ArmyName.text = army.Name;
        InitializeBanners();
        village = null;
        empire = actingEmpire;
        RecruitUI.gameObject.SetActive(false);
        MercenaryPanelUI.gameObject.SetActive(true);
        MercenaryPanelUI.SpecialMercenariesButton.gameObject.SetActive(MercenaryHouse.UniqueMercs.Any());
        leader = army.LeaderIfInArmy();
        ArmyUI.AlliedArmyText.gameObject.SetActive(false);
        ArmyUI.ShopText.text = "Inventory";
        SetUpDisplay();
        dismissList = new List<Unit>();
    }

    public void Begin(Village village, Empire empire, ActivatingEmpire activatingEmpire)
    {
        this.activatingEmpire = activatingEmpire;
        this.village = village;
        this.empire = empire;
        failedToMakeFriendlyArmy = false;
        mercenaryHouse = null;
        InitializeBanners();
        SetArmy();
        ArmyUI.RecruitSoldier.text = "Recruit Soldier (" + Config.ArmyCost + "G)";
        ArmyUI.ShopText.text = "Shop";
        RecruitUI.TownName.text = this.village.Name;
        ArmyUI.ArmyName.text = army?.Name ?? "";
        RecruitUI.gameObject.SetActive(true);
        MercenaryPanelUI.gameObject.SetActive(false);
        leader = army?.LeaderIfInArmy();
        if (army != null)
            SetUpDisplay();
        else
            RecruitUI.AddUnit.gameObject.SetActive(Config.CheatAddUnitButton);
        GenText();
    }

    void SetArmy()
    {
        army = null;
        //find an army
        ArmyUI.AlliedArmyText.gameObject.SetActive(false);

        for (int i = 0; i < empire.Armies.Count; i++)
        {
            if (empire.Armies[i].Position.Matches(village.Position))
            {
                army = empire.Armies[i];
                break;
            }
        }
        if (army == null)
        {
            if (StrategicUtilities.ArmyAt(village.Position) == null)
            {
                if (activatingEmpire != ActivatingEmpire.Observer && empire.Armies.Count() < Config.MaxArmies)
                {
                    army = new Army(empire, new Vec2i(village.Position.x, village.Position.y), empire.Side);
                    empire.Armies.Add(army);
                    army.HealRate = village.Healrate();
                }
                else
                {
                    activatingEmpire = ActivatingEmpire.Observer;
                    failedToMakeFriendlyArmy = true;
                }
            }
            else
            {
                if (activatingEmpire != ActivatingEmpire.Observer)
                    ArmyUI.AlliedArmyText.gameObject.SetActive(true);
                foreach (Army armyCheck in StrategicUtilities.GetAllArmies())
                {
                    if (armyCheck.Position.Matches(village.Position))
                    {
                        army = armyCheck;
                        if (army.Empire.IsEnemy(village.Empire))
                            activatingEmpire = ActivatingEmpire.Observer;
                        break;
                    }
                }


            }

        }
        if (army != null)
        {
            army.RecalculateSizeValue();
        }
        BannerType.gameObject.SetActive(army != null);
        InitializeBanners();

    }

    void InitializeBanners()
    {
        if (army != null)
        {
            if (BannerType.options.Count < 4)
            {
                foreach (BannerTypes type in (BannerTypes[])Enum.GetValues(typeof(BannerTypes)))
                {
                    BannerType.options.Add(new TMP_Dropdown.OptionData(type.ToString()));
                }
                for (int i = 0; i < CustomBannerTest.Sprites.Length; i++)
                {
                    if (CustomBannerTest.Sprites[i] == null)
                        break;
                    BannerType.options.Add(new TMP_Dropdown.OptionData($"Custom {i + 1}"));
                }
                BannerType.onValueChanged.AddListener((s) => UpdateArmyBanner());
            }
            BannerType.value = army.BannerStyle;
        }
    }

    void UpdateArmyBanner()
    {
        if (army == null)
            return;
        army.BannerStyle = BannerType.value;
    }

    public void Select(int num)
    {
        selectedIndex = num;
        UpdateUnitInfoPanel();
        RefreshUnitPanelButtons();
        if (ArmyUI.UnitInfoArea.Length == 0)
            return;
    }

    public void RefreshUnitPanelButtons()
    {
        bool validUnit = army?.Units.Count > selectedIndex && selectedIndex != -1;
        Unit unit = null;
        if (selectedIndex != -1 && army?.Units.Count() > selectedIndex)
            unit = army?.Units[selectedIndex];
        ArmyUI.Rename.interactable = validUnit && unit.Type != UnitType.SpecialMercenary;
        ArmyUI.Shop.interactable = activatingEmpire < ActivatingEmpire.Observer && validUnit && unit != null && (unit.FixedGear == false || unit.HasTrait(Traits.BookEater));
        var dismissText = ArmyUI.Dismiss.gameObject.GetComponentInChildren(typeof(Text)) as Text;

        if (unit != null && unit.FixedSide == empire.Side && unit.IsInfiltratingSide(unit.Side) && activatingEmpire > ActivatingEmpire.Ally)
        {
            dismissText.text = "Exfiltrate";
            ArmyUI.Dismiss.interactable = State.GameManager.StrategyMode.IsPlayerTurn;
        }
        else
        {
            dismissText.text = "Dismiss";
            ArmyUI.Dismiss.interactable = activatingEmpire < ActivatingEmpire.Observer && validUnit && unit != null && unit != army?.Empire.Leader;
        }
        ArmyUI.ConfigAutoLevelUp.interactable = activatingEmpire < ActivatingEmpire.Observer && validUnit;
        ArmyUI.Customizer.interactable = validUnit;
        if (village != null)
            RecruitUI.ImprintUnit.interactable = validUnit && activatingEmpire == ActivatingEmpire.Self && unit.Type != UnitType.SpecialMercenary && unit != army?.Empire.Leader;
        RecruitUI.ApplyPotion.interactable = validUnit && empire.EmpirePotions.Count() > 0;
    }

    public void RefreshRecruitPanelButtons()
    {
        RecruitUI.CheapUpgrade.gameObject.SetActive(activatingEmpire < ActivatingEmpire.Observer);
        RecruitUI.HireSoldier.gameObject.SetActive(activatingEmpire < ActivatingEmpire.Observer);
        RecruitUI.HireVillageMerc.gameObject.SetActive(activatingEmpire < ActivatingEmpire.Observer);
        RecruitUI.RecruitSoldier.gameObject.SetActive(activatingEmpire < ActivatingEmpire.Observer);
        RecruitUI.StockWeapons.interactable = (activatingEmpire < ActivatingEmpire.Observer || failedToMakeFriendlyArmy) && village.Empire == empire;
        RecruitUI.CheapUpgrade.interactable = activatingEmpire == ActivatingEmpire.Self && army.Units.Count > 0;
        RecruitUI.RecruitSoldier.interactable = activatingEmpire == ActivatingEmpire.Self && (village.GetTotalPop() > 3) && army.RemainnigSize - State.RaceSettings.GetDeployCost(village.Race) >= 0;
        RecruitUI.HireSoldier.interactable = activatingEmpire == ActivatingEmpire.Self && village.GetRecruitables().Count > 0 && (village.GetTotalPop() > 3) && army.RemainnigSize > 0;
        RecruitUI.HireVillageMerc.interactable = activatingEmpire == ActivatingEmpire.Self && (village.Mercenaries?.Count > 0 || village.Adventurers?.Count > 0) && army.RemainnigSize > 0;
        RecruitUI.VillageView.interactable = (activatingEmpire < ActivatingEmpire.Observer || failedToMakeFriendlyArmy) && village.GetTotalPop() > 0 && village.Empire == empire;

        RecruitUI.ResurrectLeader.gameObject.SetActive(activatingEmpire != ActivatingEmpire.Observer && empire.Leader != null && empire.Leader.Health <= 0);
    }


    public override void CleanUp()
    {
        if (dismissList != null && dismissList.Count > 0)
        {
            StrategicUtilities.ProcessTravelingUnits(dismissList, army);
            dismissList.Clear();
        }

        army = null;
        selectedIndex = -1;
        for (int x = 0; x < Config.MaximumPossibleArmy; x++)
        {
            if (ArmyUI.UnitInfoArea.Length > x)
                ArmyUI.UnitInfoArea[x].gameObject.SetActive(false);
        }
    }

    public void Recruit()
    {
        if (village != null && village.VillagePopulation.Population.Count > 1 && village.VillagePopulation.Population.Where(s => s.Population > s.Hireables).Count() > 1)
        {
            ButtonCallback(1);
        }
        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            var unit = village.RecruitPlayerUnit(empire, army);
            for (int i = 0; i < 48; i++)
            {
                if (unit != null)
                {
                    unit = village.RecruitPlayerUnit(empire, army);
                }
                else
                    break;
            }
            UpdateActorList();
            GenText();
            if (army.Units.Count > Config.ScoutMax && army.RemainingMP > Config.ArmyMP)
            {
                army.RemainingMP = Config.ArmyMP;
            }
        }
        else
        {
            if (village.RecruitPlayerUnit(empire, army) != null)
            {
                UpdateActorList();
                GenText();
                if (army.Units.Count > Config.ScoutMax && army.RemainingMP > Config.ArmyMP)
                {
                    army.RemainingMP = Config.ArmyMP;
                }
            }
        }
        Select(army.Units.Count - 1);
    }

    void GenText()
    {
        RecruitUI.Gold.text = empire.Gold + " gold ";

        if (village.GetTotalPop() > 0)
        {

            RecruitUI.Population.text = village.VillagePopulation.GetPopReport();
        }
        else
        {
            RecruitUI.Population.text = "Empty";
        }
        RecruitUI.DefenderCount.text = $"{village.Garrison} / {village.MaxGarrisonSize} defenders";
        RecruitUI.Income.text = village.GetIncome() + " income";
        RefreshRecruitPanelButtons();
        RefreshUnitPanelButtons();
    }

    void UpdateUnitInfoPanel()
    {
        if (army?.Units.Count > selectedIndex && selectedIndex != -1)
        {
            infoPanel.RefreshStrategicUnitInfo(army.Units[selectedIndex]);
            ArmyUI.LevelUp.interactable = activatingEmpire < ActivatingEmpire.Observer && army.Units[selectedIndex].HasEnoughExpToLevelUp();
            ArmyUI.AutoLevelUp.interactable = activatingEmpire < ActivatingEmpire.Observer && army.Units.Where(s => s.HasEnoughExpToLevelUp()).Any();
        }
        else
        {
            ArmyUI.InfoPanel.InfoText.text = "";
            ArmyUI.LevelUp.interactable = false;
            ArmyUI.AutoLevelUp.interactable = false;
        }

    }

    public override void ReceiveInput()
    {
        if (WeaponStockerUI.gameObject.activeSelf)
        {
            UpdateWeaponStocker();
        }
        if (selectedIndex == -1 || selectedIndex >= army?.Units.Count || ArmyUI.UnitInfoArea.Length == 0)
            ArmyUI.Selector.SetActive(false);
        else
        {
            ArmyUI.Selector.SetActive(true);
            ArmyUI.Selector.transform.position = ArmyUI.UnitInfoArea[selectedIndex].transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (VillageUI.gameObject.activeSelf || CustomizerUI.gameObject.activeSelf || ShopUI.gameObject.activeSelf || BulkBuyUI.gameObject.activeSelf ||
                WeaponStockerUI.gameObject.activeSelf || ConfigAutoLevelUpUI.gameObject.activeSelf || HireUI.gameObject.activeSelf || RenameUI.gameObject.activeSelf)
                ButtonCallback(10);
            else if (MercenaryScreenUI.gameObject.activeSelf)
                ButtonCallback(81);
            else if (BlockerUI.activeSelf == false && MercenaryScreenUI.gameObject.activeSelf == false && HireUI.gameObject.activeSelf == false &&
            CustomizerUI.gameObject.activeSelf == false && VillageUI.gameObject.activeSelf == false && VillageUI.gameObject.activeSelf == false && FindObjectOfType<DialogBox>() == false)
                State.GameManager.SwitchToStrategyMode();
        }



        if (Input.GetKeyDown(KeyCode.Escape) && CheatMenu.gameObject.activeSelf)
        {
            ButtonCallback(86);
        }

    }




    public void ButtonCallback(int ID)
    {

        switch (ID)
        {
            case 1:
                BlockerUI.SetActive(true);
                BuildRaceDisplay();
                break;
            case 2:
                BlockerUI.SetActive(true);
                BuildHiringView();
                break;
            case 3:
                BuildVillageView();
                break;
            case 4:
                BuildCustomizer();
                break;
            case 5:
                BlockerUI.SetActive(true);
                BuildRename();
                break;
            case 6:
                BlockerUI.SetActive(true);
                BuildLevelUp();
                break;
            case 7:
                Dismiss();
                break;
            case 8:
                State.GameManager.SwitchToStrategyMode();
                break;
            case 9:
                BlockerUI.SetActive(true);
                BuildShop();
                break;
            case 10:
                VillageUI.gameObject.SetActive(false);
                CustomizerUI.gameObject.SetActive(false);
                ShopUI.gameObject.SetActive(false);
                potionInv.gameObject.SetActive(false);
                HireUI.gameObject.SetActive(false);
                BulkBuyUI.gameObject.SetActive(false);
                RenameUI.gameObject.SetActive(false);
                WeaponStockerUI.gameObject.SetActive(false);
                ConfigAutoLevelUpUI.gameObject.SetActive(false);
                BlockerUI.SetActive(false);
                shop = null;
                if (selectedIndex != -1 && displayUnits?.Length > selectedIndex && displayUnits[selectedIndex] != null)
                    displayUnits[selectedIndex].UpdateBestWeapons();
                UpdateUnitInfoPanel();
                UpdateDrawnActors();
                if (village != null)
                    GenText();
                break;
            case 11:
                if (army == null)
                    return;
                if (army.Units.Count <= selectedIndex)
                {
                    break;
                }
                Unit unit = army.Units[selectedIndex];
                if (RenameUI.NewName.text.Length > 0)
                {
                    unit.Name = RenameUI.NewName.text;
                }
                RenameUI.gameObject.SetActive(false);
                BlockerUI.SetActive(false);
                UpdateUnitInfoPanel();
                UpdateDrawnActors();
                break;
            case 12:
                RenameUI.gameObject.SetActive(false);
                BlockerUI.SetActive(false);
                break;
            case 13:
                HireUI.gameObject.SetActive(false);
                BlockerUI.SetActive(false);
                break;
            case 14:
                BulkBuyUI.gameObject.SetActive(true);
                BlockerUI.SetActive(true);
                RefreshBulkBuy();
                break;
            case 15:
                WeaponStockerUI.gameObject.SetActive(true);
                BlockerUI.SetActive(true);
                BuildWeaponStocker();
                break;
            case 16:
                if (army == null)
                    return;
                if (army.Units.Count <= selectedIndex)
                {
                    break;
                }
                ConfigAutoLevelUpUI.gameObject.SetActive(true);
                ConfigAutoLevelUpUI.Open(army.Units[selectedIndex]);
                BlockerUI.SetActive(true);
                break;
            case 17:
                if (selectedIndex != -1 && displayUnits?.Length > selectedIndex && displayUnits[selectedIndex] != null)
                    BuildClone(displayUnits[selectedIndex].Unit);
                break;
            case 18:
                RaceUI.gameObject.SetActive(false);
                BlockerUI.SetActive(false);
                break;
            case 19:
                if (Config.CheatPopulation && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
                {
                    var box1 = State.GameManager.CreateInputBox();
                    box1.SetData(SetPopulation, "Set new population", "Cancel change", "Cheat to set the village population?  (In multi-race villages, lowering kills randomly, and raising acts like breeding)", 5);

                    break;
                }
                SetUpPopUI();
                BlockerUI.SetActive(true);
                break;
            case 40:
                BlockerUI.SetActive(true);
                BuildPotionInventory();
                break;

            case 50:
                UpdateUnitInfoPanel();
                break;

            case 60:
                DialogBox box = GameObject.Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
                box.SetData(AutoLevelUp, "Spend them!", "Cancel", "This will spend all remaining level ups for this army.   It may not do as good a job as manually picking, and it may not pick exactly what you want, but it is fast.");
                break;
            case 80:
                BuildMercenaryView(false);
                break;
            case 81:
                MercenaryScreenUI.gameObject.SetActive(false);
                break;
            case 82:
                BuildMercenaryView(true);
                break;
            case 83:
                BuildVillageMercenaryView();
                break;
            case 84:
                PopUI.gameObject.SetActive(false);
                BlockerUI.SetActive(false);
                break;
            case 85:
                if (army == null)
                {
                    State.GameManager.CreateMessageBox("Can't enter this screen without an army");
                    break;
                }
                CheatMenu.gameObject.SetActive(true);
                CheatMenu.Setup(army);
                BlockerUI.SetActive(true);
                break;
            case 86:
                CheatMenu.gameObject.SetActive(false);
                BlockerUI.SetActive(false);
                break;
            case 2001:
                BlockerUI.SetActive(true);
                BuildHiringView("STR");
                break;
            case 2002:
                BlockerUI.SetActive(true);
                BuildHiringView("DEX");
                break;
            case 2003:
                BlockerUI.SetActive(true);
                BuildHiringView("MND");
                break;
            case 2004:
                BlockerUI.SetActive(true);
                BuildHiringView("WLL");
                break;
            case 2005:
                BlockerUI.SetActive(true);
                BuildHiringView("END");
                break;
            case 2006:
                BlockerUI.SetActive(true);
                BuildHiringView("AGI");
                break;
            case 2007:
                BlockerUI.SetActive(true);
                BuildHiringView("VOR");
                break;
            case 2008:
                BlockerUI.SetActive(true);
                BuildHiringView("STM");
                break;
            case 4000:
                BannerType.gameObject.SetActive(true);
                ShowBanner.gameObject.SetActive(false);
                break;
            case 4001:
                CheatAddUnit();
                break;


        }
        if (ID > 19 && ID < 30)
        {
            if (army == null)
                return;
            if (army.Units.Count <= selectedIndex)
            {
                return;
            }
            Unit unit = army.Units[selectedIndex];
            unit.LevelUp((Stat)ID - 20);
            LevelUpUI.gameObject.SetActive(false);
            BlockerUI.SetActive(false);
            UpdateUnitInfoPanel();
            UpdateDrawnActors();
        }

        if (ID == 70)
        {
            if (army == null)
                return;
            if (army.RemainnigSize <= 0)
            {
                State.GameManager.CreateMessageBox("Army is already maximum size");
                return;
            }
            if (empire.Gold < 100)
            {
                State.GameManager.CreateMessageBox("You need at least 100 gold to resurrect the leader");
                return;
            }

            DialogBox box = GameObject.Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
            box.SetData(ResurrectLeader, "Yes", "No", "Resurrect Leader at this town for 100 gold?");
        }
    }

    void SetPopulation(int p)
    {
        village.SetPopulation(p);
        GenText();
    }
    private void SetUpPopUI()
    {

        int children = PopUI.ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(PopUI.ActorFolder.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < village.VillagePopulation.Population.Count; i++)
        {
            if (village.VillagePopulation.Population[i].Population > 0)
            {
                GameObject obj = Instantiate(PopUI.RecruitPanel, PopUI.ActorFolder);
                UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
                Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), new Unit(1, village.VillagePopulation.Population[i].Race, 0, true));
                TextMeshProUGUI text = obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                var racePar = RaceParameters.GetTraitData(actor.Unit);
                text.text = $"{village.VillagePopulation.Population[i].Race}\nTotal: {village.VillagePopulation.Population[i].Population}\nFavored Stat: {State.RaceSettings.GetFavoredStat(actor.Unit.Race)}\nDefault Traits:\n{State.RaceSettings.ListTraits(actor.Unit.Race)}";
                sprite.UpdateSprites(actor);
            }

        }

        PopUI.gameObject.SetActive(true);


    }

    internal void RenameVillage(string name)
    {
        if (village != null)
            village.Name = name;
        RecruitUI.TownName.text = village.Name;
    }

    internal void RenameArmy(string name)
    {
        if (army != null)
            army.Name = name;
        ArmyUI.ArmyName.text = army.Name;
    }

    internal void BuildClone(Unit unit)
    {
        int baseXP = village.GetStartingXp();
        int effectiveXP = Math.Max((int)(unit.Experience - baseXP), 10);
        int diff = Math.Max((int)(effectiveXP - (unit.SavedCopy?.Experience ?? 0)), 10);
        int cost = 20 + (int)(effectiveXP * 0.1f + 0.2f * diff);
        cost = (int)Math.Round(cost - (cost * (0.125f * AcademyResearch.GetValueFromEmpire(empire, AcademyResearchType.ImprintCost))));

        string previous = "";

        if (unit.SavedCopy != null)
        {
            previous = $"Previous imprint --  Level: {unit.SavedCopy.Level}  Exp: {unit.SavedCopy.Experience}";
        }
        else
        {
            previous = "This unit has no saved imprint";
        }

        var box = State.GameManager.CreateDialogBox();
        box.SetData(() => MakeClone(unit, cost), "Imprint", "Cancel", $"Imprint this soul? (Costs {cost})\nAllows unit to respawn with saved stats at this location if it dies." +
            $"\nCost is based on experience of other active imprint, total experience, and is reduced by the innate exp for this town\n{previous}");

    }

    void MakeClone(Unit unit, int cost)
    {
        if (empire.Gold < cost)
            return;
        empire.SpendGold(cost);
        var clonedUnit = unit.Clone();
        unit.SavedCopy = clonedUnit;
        unit.SavedVillage = village;
        GenText();
    }

    void ResurrectLeader()
    {
        empire.SpendGold(100);
        empire.Leader.LeaderLevelDown();
        empire.Leader.Health = empire.Leader.MaxHealth;
        empire.Leader.FixedSide = empire.Side;
        empire.Leader.Type = UnitType.Leader;
        if (village.GetStartingXp() > empire.Leader.Experience)
        {
            empire.Leader.SetExp(village.GetStartingXp());
        }
        State.World.Stats.ResurrectedLeader(empire.Side);
        army.Units.Add(empire.Leader);
        if (Config.LeadersRerandomizeOnDeath)
        {
            empire.Leader.TotalRandomizeAppearance();
            empire.Leader.ReloadTraits();
            empire.Leader.InitializeTraits();
        }

        UpdateActorList();
        GenText();
        if (army.Units.Count > Config.ScoutMax && army.RemainingMP > Config.ArmyMP)
        {
            army.RemainingMP = Config.ArmyMP;
        }
    }

    void BuildRename()
    {
        if (army.Units.Count > selectedIndex)
        {
            RenameUI.gameObject.SetActive(true);
        }
    }

    void BuildWeaponStocker()
    {
        WeaponStockerUI.BuyMace.onClick.RemoveAllListeners();
        WeaponStockerUI.BuyAxe.onClick.RemoveAllListeners();
        WeaponStockerUI.BuyBow.onClick.RemoveAllListeners();
        WeaponStockerUI.BuyCompoundBow.onClick.RemoveAllListeners();
        WeaponStockerUI.SellMace.onClick.RemoveAllListeners();
        WeaponStockerUI.SellAxe.onClick.RemoveAllListeners();
        WeaponStockerUI.SellBow.onClick.RemoveAllListeners();
        WeaponStockerUI.SellCompoundBow.onClick.RemoveAllListeners();
        WeaponStockerUI.BuyMace.onClick.AddListener(() => village.BuyWeaponPotentiallyBulk(ItemType.Mace, empire));
        WeaponStockerUI.BuyAxe.onClick.AddListener(() => village.BuyWeaponPotentiallyBulk(ItemType.Axe, empire));
        WeaponStockerUI.BuyBow.onClick.AddListener(() => village.BuyWeaponPotentiallyBulk(ItemType.Bow, empire));
        WeaponStockerUI.BuyCompoundBow.onClick.AddListener(() => village.BuyWeaponPotentiallyBulk(ItemType.CompoundBow, empire));
        WeaponStockerUI.SellMace.onClick.AddListener(() => village.SellWeaponPotentiallyBulk(ItemType.Mace, empire));
        WeaponStockerUI.SellAxe.onClick.AddListener(() => village.SellWeaponPotentiallyBulk(ItemType.Axe, empire));
        WeaponStockerUI.SellBow.onClick.AddListener(() => village.SellWeaponPotentiallyBulk(ItemType.Bow, empire));
        WeaponStockerUI.SellCompoundBow.onClick.AddListener(() => village.SellWeaponPotentiallyBulk(ItemType.CompoundBow, empire));
        UpdateWeaponStocker();
    }

    void UpdateWeaponStocker()
    {
        int maces = village.Weapons.Where(s => s == ItemType.Mace).Count();
        int axes = village.Weapons.Where(s => s == ItemType.Axe).Count();
        int bows = village.Weapons.Where(s => s == ItemType.Bow).Count();
        int compoundBows = village.Weapons.Where(s => s == ItemType.CompoundBow).Count();
        WeaponStockerUI.Maces.text = $"Maces: {maces}";
        WeaponStockerUI.Axes.text = $"Axes: {axes}";
        WeaponStockerUI.Bows.text = $"Bows: {bows}";
        WeaponStockerUI.CompoundBows.text = $"Compound Bows: {compoundBows}";
        WeaponStockerUI.SellMace.interactable = maces > 0;
        WeaponStockerUI.SellAxe.interactable = axes > 0;
        WeaponStockerUI.SellBow.interactable = bows > 0;
        WeaponStockerUI.SellCompoundBow.interactable = compoundBows > 0;
        WeaponStockerUI.RemainingGold.text = $"Remaining Gold: {empire.Gold}";
    }

    public void FinalizeAutoLevelUI(bool copy)
    {
        ButtonCallback(10);
        ApplyTo(army.Units[selectedIndex]);

        if (copy)
        {
            foreach (Actor_Unit actor in displayUnits)
            {
                if (actor != null)
                    ApplyTo(actor.Unit);
            }
        }



        void ApplyTo(Unit unit)
        {
            if (ConfigAutoLevelUpUI.Custom)
            {
                unit.AIClass = AIClass.Custom;
                unit.StatWeights = new StatWeights()
                {
                    Weight = new float[(int)Stat.None]
                {
                ConfigAutoLevelUpUI.Sliders[0].value,
                ConfigAutoLevelUpUI.Sliders[1].value,
                ConfigAutoLevelUpUI.Sliders[2].value,
                ConfigAutoLevelUpUI.Sliders[3].value,
                ConfigAutoLevelUpUI.Sliders[4].value,
                ConfigAutoLevelUpUI.Sliders[5].value,
                ConfigAutoLevelUpUI.Sliders[6].value,
                ConfigAutoLevelUpUI.Sliders[7].value,
                ConfigAutoLevelUpUI.Sliders[8].value,
                    }
                };
            }
            unit.AutoLeveling = ConfigAutoLevelUpUI.AutoSpend.isOn;
            unit.AIClass = (AIClass)ConfigAutoLevelUpUI.Dropdown.value;
        }
    }

    void BuildLevelUp()
    {
        if (army == null)
        {
            BlockerUI.SetActive(false);
            return;
        }

        if (army.Units.Count > selectedIndex && selectedIndex != -1)
        {
            if (army.Units[selectedIndex].HasEnoughExpToLevelUp())
            {
                LevelUpUI.gameObject.SetActive(true);
                BuildLvlButtons();
            }

        }
        else
        {
            BlockerUI.SetActive(false);
            return;
        }

    }

    void BuildLvlButtons()
    {
        Stat[] r = army.Units[selectedIndex].GetLevelUpPossibilities(army.Units[selectedIndex].Predator);
        for (int i = 0; i < LevelUpUI.StatButtons.Length; i++)
        {
            LevelUpUI.StatButtons[i].gameObject.SetActive(i < r.Length);
        }
        for (int i = 0; i < r.Length; i++)
        {
            int statInt = (int)r[i];
            Stat stat = r[i];
            int currentValue = army.Units[selectedIndex].GetStatBase(stat);
            int increase = 5;
            LevelUpUI.StatButtons[i].onClick.RemoveAllListeners();
            LevelUpUI.StatButtons[i].GetComponentInChildren<Text>().text = $"{stat} ({currentValue} -> {currentValue + increase})";
            LevelUpUI.StatButtons[i].onClick.AddListener(() =>
            {
                ButtonCallback(20 + statInt);

                if (LevelUpUI.KeepOpen.isOn && army.Units[selectedIndex].HasEnoughExpToLevelUp())
                {
                    ButtonCallback(6);
                }
                else
                    LevelUpUI.gameObject.SetActive(false);
            });
        }


    }

    public void RefreshBulkBuy()
    {
        int cheapCost = CheapFitCost();
        int expensiveCost = ExpensiveFitCost();
        int accessoryCost = AccessoryFitCost();
        BulkBuyUI.BuyCheap.interactable = empire.Gold >= cheapCost && cheapCost > 0;
        BulkBuyUI.BuyExpensive.interactable = empire.Gold >= expensiveCost && expensiveCost > 0;
        BulkBuyUI.BuyAccessories.interactable = empire.Gold >= accessoryCost && accessoryCost > 0;
        BulkBuyUI.BuyCheap.GetComponentInChildren<Text>().text = $"Purchase Basic Weapons\nCost : {cheapCost} gold";
        BulkBuyUI.BuyExpensive.GetComponentInChildren<Text>().text = $"Purchase Advanced Weapons\nCost : {expensiveCost} gold";
        BulkBuyUI.BuyAccessories.GetComponentInChildren<Text>().text = $"Purchase Accessories\nCost : {accessoryCost} gold\nIt only buys for units with weapons\nIt might have different priorities than you";
    }

    void AutoLevelUp()
    {
        if (army?.Units == null)
            return;
        foreach (Unit unit in army.Units)
        {
            StrategicUtilities.SpendLevelUps(unit);
        }
        UpdateUnitInfoPanel();
        UpdateDrawnActors();
    }



    int CheapFitCost()
    {
        int cost = 0;
        foreach (Actor_Unit actor in displayUnits)
        {
            if (actor != null)
            {
                if (actor.Unit.HasFreeItemSlot() == false || actor.Unit.FixedGear || (actor.Unit.Items[0]?.LockedItem ?? false))
                    continue;
                if (actor.Unit.GetBestMelee().Damage == 2 && actor.Unit.GetBestRanged() == null)
                {
                    if (actor.Unit.BestSuitedForRanged())
                        cost += State.World.ItemRepository.GetItem(ItemType.Bow).Cost;
                    else
                        cost += State.World.ItemRepository.GetItem(ItemType.Mace).Cost;
                }
            }
        }
        return cost;
    }

    public void CheapFit()
    {
        foreach (Actor_Unit actor in displayUnits)
        {
            if (actor != null)
            {
                if (actor.Unit.HasFreeItemSlot() == false || actor.Unit.FixedGear || (actor.Unit.Items[0]?.LockedItem ?? false))
                    continue;
                if (actor.Unit.GetBestMelee().Damage == 2 && actor.Unit.GetBestRanged() == null)
                {
                    if (actor.Unit.BestSuitedForRanged())
                        Shop.BuyItem(empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Bow));
                    else
                        Shop.BuyItem(empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Mace));
                    actor.UpdateBestWeapons();
                }
            }
        }
        UpdateDrawnActors();
        UpdateUnitInfoPanel();
        GenText();
    }

    int ExpensiveFitCost()
    {
        int cost = 0;
        foreach (Actor_Unit actor in displayUnits)
        {
            if (actor != null)
            {
                if ((actor.Unit.HasFreeItemSlot() || actor.Unit.HasSpecificWeapon(ItemType.Bow, ItemType.Mace)) == false || actor.Unit.FixedGear || (actor.Unit.Items[0]?.LockedItem ?? false))
                    continue;
                if (actor.Unit.GetBestMelee().Damage == 2 && actor.Unit.GetBestRanged() == null)
                {
                    if (actor.Unit.BestSuitedForRanged())
                        cost += State.World.ItemRepository.GetItem(ItemType.CompoundBow).Cost;
                    else
                        cost += State.World.ItemRepository.GetItem(ItemType.Axe).Cost;
                }
                else if (BulkBuyUI.SellAndBuy.isOn)
                {
                    if (actor.Unit.GetBestMelee() == State.World.ItemRepository.GetItem(ItemType.Mace))
                    {
                        cost -= State.World.ItemRepository.GetItem(ItemType.Mace).Cost / 2;
                        cost += State.World.ItemRepository.GetItem(ItemType.Axe).Cost;
                    }
                    if (actor.Unit.GetBestRanged() == State.World.ItemRepository.GetItem(ItemType.Bow))
                    {
                        cost -= State.World.ItemRepository.GetItem(ItemType.Bow).Cost / 2;
                        cost += State.World.ItemRepository.GetItem(ItemType.CompoundBow).Cost;
                    }
                }

            }
        }
        return cost;
    }

    public void ExpensiveFit()
    {
        foreach (Actor_Unit actor in displayUnits)
        {
            if (actor != null)
            {
                if ((actor.Unit.HasFreeItemSlot() || actor.Unit.HasSpecificWeapon(ItemType.Bow, ItemType.Mace)) == false || actor.Unit.FixedGear || (actor.Unit.Items[0]?.LockedItem ?? false))
                    continue;
                if (actor.Unit.GetBestMelee().Damage == 2 && actor.Unit.GetBestRanged() == null)
                {
                    if (actor.Unit.BestSuitedForRanged())
                        Shop.BuyItem(empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.CompoundBow));
                    else
                        Shop.BuyItem(empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Axe));
                    actor.UpdateBestWeapons();
                }
                else if (BulkBuyUI.SellAndBuy.isOn)
                {
                    if (actor.Unit.GetBestMelee() == State.World.ItemRepository.GetItem(ItemType.Mace))
                    {
                        Shop.SellItem(empire, actor.Unit, actor.Unit.GetItemSlot(State.World.ItemRepository.GetItem(ItemType.Mace)));
                        Shop.BuyItem(empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Axe));
                        actor.UpdateBestWeapons();
                    }
                    if (actor.Unit.GetBestRanged() == State.World.ItemRepository.GetItem(ItemType.Bow))
                    {
                        Shop.SellItem(empire, actor.Unit, actor.Unit.GetItemSlot(State.World.ItemRepository.GetItem(ItemType.Bow)));
                        Shop.BuyItem(empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.CompoundBow));
                        actor.UpdateBestWeapons();
                    }
                }
            }
        }
        UpdateDrawnActors();
        UpdateUnitInfoPanel();
        GenText();
    }

    int AccessoryFitCost()
    {
        int cost = 0;
        foreach (Actor_Unit actor in displayUnits)
        {
            if (actor != null)
            {
                if (actor.Unit.HasFreeItemSlot() == false || actor.Unit.FixedGear)
                    continue;
                if (actor.Unit.GetBestRanged() != null)
                {
                    if (actor.Unit.HasSpecificWeapon(ItemType.Gloves) == false)
                        cost += State.World.ItemRepository.GetItem(ItemType.Gloves).Cost;
                    else
                        cost += State.World.ItemRepository.GetItem(ItemType.Shoes).Cost;
                }
                else if (actor.Unit.GetBestMelee().Damage != 2)
                {
                    if (actor.Unit.GetStat(Stat.Agility) < 12 || actor.Unit.HasSpecificWeapon(ItemType.Helmet))
                        cost += State.World.ItemRepository.GetItem(ItemType.Shoes).Cost;
                    else
                        cost += State.World.ItemRepository.GetItem(ItemType.Helmet).Cost;
                }
            }
        }
        return cost;
    }

    public void AccessoryFit()
    {
        foreach (Actor_Unit actor in displayUnits)
        {
            if (actor != null)
            {
                if (actor.Unit.HasFreeItemSlot() == false || actor.Unit.FixedGear)
                    continue;
                if (actor.Unit.GetBestRanged() != null)
                {
                    if (actor.Unit.HasSpecificWeapon(ItemType.Gloves) == false)
                        Shop.BuyItem(empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Gloves));
                    else
                        Shop.BuyItem(empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Shoes));

                }
                else if (actor.Unit.GetBestMelee().Damage != 2)
                {
                    if (actor.Unit.GetStat(Stat.Agility) < 12 || actor.Unit.HasSpecificWeapon(ItemType.Helmet))
                        Shop.BuyItem(empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Shoes));
                    else
                        Shop.BuyItem(empire, actor.Unit, State.World.ItemRepository.GetItem(ItemType.Helmet));
                }
            }
        }
        UpdateDrawnActors();
        UpdateUnitInfoPanel();
        GenText();
    }

    public void AccessoryBulkSell()
    {
        foreach (Actor_Unit actor in displayUnits)
        {
            if (actor != null)
            {
                for (int i = 0; i < actor.Unit.Items.Length; i++)
                {
                    if (actor.Unit.Items[i] != null)
                    {
                        if (actor.Unit.Items[i] is Accessory)
                        {
                            actor.Unit.SetItem(null, i);
                        }
                    }
                }
            }
        }
        UpdateUnitInfoPanel();
        GenText();
    }

    void BuildVillageView()
    {
        if (village != null)
        {
            if (villageView == null)
            {
                villageView = new VillageView(village, VillageUI);
                villageView.Open(village, empire);
            }
            else
                villageView.Open(village, empire);
        }
    }

    public void ForceRefreshVillageView()
    {
        villageView?.Refresh();
    }

    void BuildCustomizer()
    {
        if (army.Units.Count > selectedIndex)
        {
            if (Customizer == null)
                Customizer = new UnitCustomizer(army.Units[selectedIndex], CustomizerUI);
            else
                Customizer.SetUnit(army.Units[selectedIndex]);
            CustomizerUI.gameObject.SetActive(true);
        }
    }
    public void CopySkintoneFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in army.Units.Where(s => s.Race == source.Race && s.Type != UnitType.Leader))
        {
            unit.SkinColor = source.SkinColor;
        }
    }
    public void CopyHairColorFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in army.Units.Where(s => s.Race == source.Race && s.Type != UnitType.Leader))
        {
            unit.HairColor = source.HairColor;
        }
    }
    public void CopyHairStyleFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in army.Units.Where(s => s.Race == source.Race && s.Type != UnitType.Leader))
        {
            unit.HairStyle = source.HairStyle;
        }
    }
    public void CopyBodyColorFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in army.Units.Where(s => s.Race == source.Race && s.Type != UnitType.Leader))
        {
            unit.AccessoryColor = source.AccessoryColor;
        }
    }

    public void CopyBreastSizeFromCustomizer()
    {
        if (Customizer.Unit.HasBreasts == false)
            return;
        Unit source = Customizer.Unit;
        foreach (Unit unit in army.Units.Where(s => s.Race == source.Race && s.HasBreasts == source.HasBreasts && s.Type != UnitType.Leader))
        {
            unit.SetDefaultBreastSize(source.BreastSize);
        }
    }

    public void CopyClothingTypeFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in army.Units.Where(s => s.Race == source.Race &&
        (s.Type == source.Type || (source.Race != Race.Slimes && source.Type == UnitType.Leader && (s.Type == UnitType.Soldier || s.Type == UnitType.Mercenary || s.Type == UnitType.Adventurer)))))
        {
            unit.ClothingType = source.ClothingType;
            unit.ClothingType2 = source.ClothingType2;
            unit.ClothingHatType = source.ClothingHatType;
            unit.ClothingAccessoryType = source.ClothingAccessoryType;
            unit.ClothingExtraType1 = source.ClothingExtraType1;
            unit.ClothingExtraType2 = source.ClothingExtraType2;
            unit.ClothingExtraType3 = source.ClothingExtraType3;
            unit.ClothingExtraType4 = source.ClothingExtraType4;
            unit.ClothingExtraType5 = source.ClothingExtraType5;
        }
    }

    public void CopyClothingColorFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in army.Units.Where(s => (s.Race == Race.Panthers) == (source.Race == Race.Panthers))) //Since panthers use different color systems
        {
            unit.ClothingColor = source.ClothingColor;
            unit.ClothingColor2 = source.ClothingColor2;
            unit.ClothingColor3 = source.ClothingColor3;
        }
    }

    public void CopyEyeColorFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in army.Units.Where(s => s.Race == source.Race))
        {
            unit.EyeColor = source.EyeColor;
        }
    }

    public void CopyEyeTypeFromCustomizer()
    {
        Unit source = Customizer.Unit;
        foreach (Unit unit in army.Units.Where(s => s.Race == source.Race))
        {
            unit.EyeType = source.EyeType;
        }
    }

    public void RandomizeUnit()
    {
        Races.GetRace(Customizer.Unit).RandomCustom(Customizer.Unit);
        Customizer.RefreshView();
        Customizer.RefreshGenderSelector();
    }

    void BuildShop()
    {
        if (army.Units.Count > selectedIndex)
        {
            Unit unit = army.Units[selectedIndex];
            if (unit != null)
            {
                shop = new Shop(empire, village, unit, army, ShopUI, village != null);
                ShopUI.gameObject.SetActive(true);
            }
        }
    }

    void BuildPotionInventory()
    {
        if (army.Units.Count > selectedIndex)
        {
            Unit unit = army.Units[selectedIndex];
            if (unit != null)
            {
                potionInv.gameObject.SetActive(true);
                potionInv.Open(empire, unit);
            }
        }
    }

    internal void ShopSellItem(int slot) => shop.SellItem(slot);
    internal void ShopTransferToInventory(int slot) => shop.TransferItemToInventory(slot);
    internal void ShopTransferItemToCharacter(int type) => shop.TransferItemToCharacter(type);
    internal void ShopSellItemFromInventory(int type) => shop.SellItemFromInventory(type);
    internal void ShopGenerateBuyButton(int type)
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
            box.SetData(() => { shop.BuyForAll(type); State.GameManager.Recruit_Mode.SetUpDisplay(); }, "Buy", "Cancel", $"Buy item for all units in army? Cost : {shop.BuyForAllCost(type)}  (you were holding shift)");
        }
        else
            shop.BuyItem(type);
    }



    void BuildMercenaryView(bool special)
    {
        int children = MercenaryScreenUI.Folder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(MercenaryScreenUI.Folder.transform.GetChild(i).gameObject);
        }
        List<MercenaryContainer> list;
        if (special)
            list = MercenaryHouse.UniqueMercs;
        else
            list = mercenaryHouse.Mercenaries;
        foreach (var mercRaw in list)
        {
            MercenaryContainer merc = new MercenaryContainer();
            merc.Unit = mercRaw.Unit;
            merc.Title = mercRaw.Title;
            merc.Cost = mercRaw.Cost - (int)Math.Round(mercRaw.Cost * (0.1f * AcademyResearch.GetValueFromEmpire(empire, AcademyResearchType.MercRecruitCost)));
            GameObject obj = Instantiate(MercenaryScreenUI.HireableObject, MercenaryScreenUI.Folder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), merc.Unit);
            Text GenderText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            Text EXPText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
            GameObject EquipRow = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(2).gameObject;
            GameObject StatRow1 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(3).gameObject;
            GameObject StatRow2 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(4).gameObject;
            GameObject StatRow3 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(5).gameObject;
            GameObject StatRow4 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(6).gameObject;
            GameObject DeployCost = obj.transform.GetChild(2).GetChild(0).GetChild(2).gameObject;
            Text TraitList = obj.transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
            Text HireButton = obj.transform.GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<Text>();

            string gender;

            if (merc.Unit.GetGender() == Gender.None)
            {
                GenderText.text += $"{merc.Title}";
            }
            else
            {
                if (merc.Unit.GetGender() == Gender.Hermaphrodite)
                    gender = "Herm";

                else
                    gender = merc.Unit.GetGender().ToString();
                GenderText.text = $"{gender} {merc.Title}";
            }
            EXPText.text = $"Level {merc.Unit.Level} ({(int)merc.Unit.Experience} EXP)";

            EquipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(0)?.Name;
            EquipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(1)?.Name;
            if (actor.Unit.HasTrait(Traits.Resourceful))
            {
                EquipRow.transform.GetChild(2).gameObject.SetActive(true);
                EquipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(0)?.Name;
                EquipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(1)?.Name;
                EquipRow.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(2)?.Name;
            }
            else
            {
                EquipRow.transform.GetChild(2).gameObject.SetActive(false);
                EquipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(0)?.Name;
                EquipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(1)?.Name;
            }

            StatRow1.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Strength).ToString();
            StatRow1.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Dexterity).ToString();
            StatRow2.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Mind).ToString();
            StatRow2.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Will).ToString();
            StatRow3.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Endurance).ToString();
            StatRow3.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Agility).ToString();
            DeployCost.transform.GetChild(1).GetComponent<Text>().text = (State.RaceSettings.GetDeployCost(merc.Unit.Race) * merc.Unit.TraitBoosts.DeployCostMult).ToString();
            if (actor.PredatorComponent != null)
            {
                StatRow4.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Voracity).ToString();
                StatRow4.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Stomach).ToString();
            }
            else
                StatRow4.SetActive(false);
            HireButton.text = "Hire Unit (" + merc.Cost.ToString() + "G)" + " + (" + State.RaceSettings.GetUpkeep(merc.Unit.Race) * merc.Unit.TraitBoosts.UpkeepMult + "G/turn)";
            TraitList.text = RaceEditorPanel.TraitListToText(merc.Unit.GetTraits, true).Replace(", ", "\n");

            actor.UpdateBestWeapons();
            sprite.UpdateSprites(actor);
            sprite.Name.text = merc.Unit.Name;
            Button button = obj.GetComponentInChildren<Button>();
            if (StrategicUtilities.ArmyCanFitUnit(army, actor.Unit))
                button.interactable = true;
            else
                button.interactable = false;
            button.onClick.AddListener(() => HireMercenary(merc, obj));
            button.onClick.AddListener(() => CheckButtonStatus());
            buttonList.Add(button, actor.Unit);
        }
        UpdateMercenaryScreenText();
        MercenaryScreenUI.gameObject.SetActive(true);
    }

    void HireMercenary(MercenaryContainer merc, GameObject obj)
    {
        if (empire.Gold >= merc.Cost)
        {
            if (StrategicUtilities.ArmyCanFitUnit(army, merc.Unit))
            {
                army.Units.Add(merc.Unit);
                merc.Unit.Side = army.Side;
                empire.SpendGold(merc.Cost);
                mercenaryHouse.Mercenaries.Remove(merc);
                MercenaryHouse.UniqueMercs.Remove(merc);
                Destroy(obj);
                UpdateActorList();
                UpdateMercenaryScreenText();
                if (army.Units.Count > Config.ScoutMax && army.RemainingMP > Config.ArmyMP)
                {
                    army.RemainingMP = Config.ArmyMP;
                }

            }
        }
    }

    void HireVillageMercenary(MercenaryContainer merc, GameObject obj)
    {
        if (village.HireSpecialUnit(empire, army, merc))
        {
            Destroy(obj);
            UpdateActorList();
            UpdateMercenaryScreenText();
            if (army.Units.Count > Config.ScoutMax && army.RemainingMP > Config.ArmyMP)
            {
                army.RemainingMP = Config.ArmyMP;
            }
        }

    }

    void BuildVillageMercenaryView()
    {
        int children = MercenaryScreenUI.Folder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(MercenaryScreenUI.Folder.transform.GetChild(i).gameObject);
        }
        List<MercenaryContainer> list;
        list = village.Mercenaries.Concat(village.Adventurers).ToList();
        foreach (var mercRaw in list)
        {
            MercenaryContainer merc = new MercenaryContainer();
            merc.Unit = mercRaw.Unit;
            merc.Title = mercRaw.Title;
            merc.Cost = mercRaw.Cost - (int)Math.Round(mercRaw.Cost * (0.1f * AcademyResearch.GetValueFromEmpire(empire, AcademyResearchType.MercRecruitCost)));
            GameObject obj = Instantiate(MercenaryScreenUI.HireableObject, MercenaryScreenUI.Folder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), merc.Unit);
            //Text text = obj.transform.GetChild(3).GetComponent<Text>();
            Text GenderText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            Text EXPText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
            GameObject EquipRow = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(2).gameObject;
            GameObject StatRow1 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(3).gameObject;
            GameObject StatRow2 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(4).gameObject;
            GameObject StatRow3 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(5).gameObject;
            GameObject StatRow4 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(6).gameObject;
            GameObject DeployCost = obj.transform.GetChild(2).GetChild(0).GetChild(2).gameObject;
            Text TraitList = obj.transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
            Text HireButton = obj.transform.GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<Text>();

            string gender;

            if (merc.Unit.GetGender() == Gender.None)
            {
                GenderText.text += $"{merc.Title}";
            }
            else
            {
                if (merc.Unit.GetGender() == Gender.Hermaphrodite)
                    gender = "Herm";

                else
                    gender = merc.Unit.GetGender().ToString();
                GenderText.text = $"{gender} {merc.Title}";
            }
            TraitList.text = RaceEditorPanel.TraitListToText(merc.Unit.GetTraits, true).Replace(", ","\n");
            EXPText.text = $"Level {merc.Unit.Level} ({(int)merc.Unit.Experience} EXP)";
            if (actor.Unit.HasTrait(Traits.Resourceful))
            {
                EquipRow.transform.GetChild(2).gameObject.SetActive(true);
                EquipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(0)?.Name;
                EquipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(1)?.Name;
                EquipRow.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(2)?.Name;
            }
            else
            {
                EquipRow.transform.GetChild(2).gameObject.SetActive(false);
                EquipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(0)?.Name;
                EquipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = merc.Unit.GetItem(1)?.Name;
            }
            StatRow1.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Strength).ToString();
            StatRow1.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Dexterity).ToString();
            StatRow2.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Mind).ToString();
            StatRow2.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Will).ToString();
            StatRow3.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Endurance).ToString();
            StatRow3.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Agility).ToString();
            DeployCost.transform.GetChild(1).GetComponent<Text>().text = (State.RaceSettings.GetDeployCost(merc.Unit.Race) * merc.Unit.TraitBoosts.DeployCostMult).ToString();
            if (actor.PredatorComponent != null)
            {
                StatRow4.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Voracity).ToString();
                StatRow4.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = merc.Unit.GetStatBase(Stat.Stomach).ToString();
            }
            else
                StatRow4.SetActive(false);
            HireButton.text = "Hire Unit (" + merc.Cost.ToString() + "G)" + " + (" + State.RaceSettings.GetUpkeep(merc.Unit.Race) * merc.Unit.TraitBoosts.UpkeepMult + "G/turn)";

            //text.text = $"{merc.Title}\nLevel: {merc.Unit.Level} Exp: {(int)merc.Unit.Experience}\n" +
            //    $"Items: {merc.Unit.GetItem(0)?.Name} {merc.Unit.GetItem(1)?.Name}\n" +
            //     $"Str: {merc.Unit.GetStatBase(Stat.Strength)} Dex: {merc.Unit.GetStatBase(Stat.Dexterity)} Agility: {merc.Unit.GetStatBase(Stat.Agility)}\n" +
            //    $"Mind: {merc.Unit.GetStatBase(Stat.Mind)} Will: {merc.Unit.GetStatBase(Stat.Will)} Endurance: {merc.Unit.GetStatBase(Stat.Endurance)}\n";
            //if (actor.Unit.Predator)
            //    text.text += $"Vore: {merc.Unit.GetStatBase(Stat.Voracity)} Stomach: {merc.Unit.GetStatBase(Stat.Stomach)}";

            actor.UpdateBestWeapons();
            sprite.UpdateSprites(actor);
            sprite.Name.text = merc.Unit.Name;
            Button button = obj.GetComponentInChildren<Button>();
            if (StrategicUtilities.ArmyCanFitUnit(army, actor.Unit))
                button.interactable = true;
            else
                button.interactable = false;
            button.onClick.AddListener(() => HireVillageMercenary(merc, obj));
            button.onClick.AddListener(() => CheckButtonStatus());
            buttonList.Add(button, actor.Unit);
        }
        UpdateMercenaryScreenText();
        MercenaryScreenUI.gameObject.SetActive(true);
    }

    void UpdateMercenaryScreenText()
    {
        MercenaryScreenUI.ArmySize.text = $"Army Size {army.UsedSize} / {army.MaxSize}";
        MercenaryScreenUI.RemainingGold.text = $"Remaining Gold: {empire.Gold}";
    }

    void BuildHiringView(string sorting = "")
    {

        int children = HireUI.ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(HireUI.ActorFolder.transform.GetChild(i).gameObject);
        }
        List<Unit> units = village.VillagePopulation.GetRecruitables().OrderByDescending(t => t.Experience).OrderByDescending(t => t.Level).ToList();
        if (sorting == "STR")
            units = units.OrderByDescending(t => t.GetStat(Stat.Strength)).ToList();
        else if (sorting == "DEX")
            units = units.OrderByDescending(t => t.GetStat(Stat.Dexterity)).ToList();
        else if (sorting == "MND")
            units = units.OrderByDescending(t => t.GetStat(Stat.Mind)).ToList();
        else if (sorting == "WLL")
            units = units.OrderByDescending(t => t.GetStat(Stat.Will)).ToList();
        else if (sorting == "END")
            units = units.OrderByDescending(t => t.GetStat(Stat.Endurance)).ToList();
        else if (sorting == "AGI")
            units = units.OrderByDescending(t => t.GetStat(Stat.Agility)).ToList();
        else if (sorting == "VOR")
            units = units.OrderByDescending(t => t.GetStat(Stat.Voracity)).ToList();
        else if (sorting == "STM")
            units = units.OrderByDescending(t => t.GetStat(Stat.Stomach)).ToList();
        foreach (Unit unit in units)
        {
            GameObject obj = Instantiate(HireUI.HiringUnitPanel, HireUI.ActorFolder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), unit);
            //Text text = obj.transform.GetChild(3).GetComponent<Text>();
            Text GenderText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            Text EXPText = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
            GameObject EquipRow = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(2).gameObject;
            GameObject StatRow1 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(3).gameObject;
            GameObject StatRow2 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(4).gameObject;
            GameObject StatRow3 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(5).gameObject;
            GameObject StatRow4 = obj.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(6).gameObject;
            Text TraitList = obj.transform.GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
            Text DeployCost = obj.transform.GetChild(2).GetChild(0).GetChild(2).GetChild(1).gameObject.GetComponent<Text>();

            string gender;
            if (actor.Unit.GetGender() != Gender.None)
            {
                if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                    gender = "Herm";
                else
                    gender = actor.Unit.GetGender().ToString();
                GenderText.text = $"{gender}";
            }
            EXPText.text = $"Level {unit.Level} ({(int)unit.Experience} EXP)";
            DeployCost.text = (State.RaceSettings.GetDeployCost(unit.Race) * unit.TraitBoosts.DeployCostMult).ToString();
            if (actor.Unit.HasTrait(Traits.Resourceful))
            {
                EquipRow.transform.GetChild(2).gameObject.SetActive(true);
                EquipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = unit.GetItem(0)?.Name;
                EquipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = unit.GetItem(1)?.Name;
                EquipRow.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = unit.GetItem(2)?.Name;
            }
            else
            {
                EquipRow.transform.GetChild(2).gameObject.SetActive(false);
                EquipRow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = unit.GetItem(0)?.Name;
                EquipRow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = unit.GetItem(1)?.Name;
            }
            StatRow1.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Strength).ToString();
            StatRow1.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Dexterity).ToString();
            StatRow2.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Mind).ToString();
            StatRow2.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Will).ToString();
            StatRow3.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Endurance).ToString();
            StatRow3.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Agility).ToString();
            if (actor.PredatorComponent != null)
            {
                StatRow4.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Voracity).ToString();
                StatRow4.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = unit.GetStatBase(Stat.Stomach).ToString();
            }
            else
                StatRow4.SetActive(false);
            TraitList.text = RaceEditorPanel.TraitListToText(unit.GetTraits, true).Replace(", ", "\n");
            //text.text += $"STR: {unit.GetStatBase(Stat.Strength)} DEX: { unit.GetStatBase(Stat.Dexterity)}\n" +
            //    $"MND: {unit.GetStatBase(Stat.Mind)} WLL: { unit.GetStatBase(Stat.Will)} \n" +
            //    $"END: {unit.GetStatBase(Stat.Endurance)} AGI: {unit.GetStatBase(Stat.Agility)}\n";
            //if (actor.PredatorComponent != null)
            //    text.text += $"VOR: {unit.GetStatBase(Stat.Voracity)} STM: { unit.GetStatBase(Stat.Stomach)}";
            actor.UpdateBestWeapons();
            sprite.UpdateSprites(actor);
            sprite.Name.text = unit.Name;
            Button button = obj.GetComponentInChildren<Button>();
            if (StrategicUtilities.ArmyCanFitUnit(army, actor.Unit))
                button.interactable = true;
            else
                button.interactable = false;
            button.onClick.AddListener(() => Hire(unit));
            button.onClick.AddListener(() => CheckButtonStatus());
            button.onClick.AddListener(() => buttonList.Remove(button));
            button.onClick.AddListener(() => Destroy(obj));
            buttonList.Add(button, actor.Unit);
        }
        HireUI.ActorFolder.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 300 * (1 + (village.VillagePopulation.GetRecruitables().Count) / 3));
        HireUI.gameObject.SetActive(true);

    }



    void Hire(Unit unit)
    {
        if (village.HireUnit(empire, army, unit))
        {
            if (unit.HasTrait(Traits.Infiltrator) && !unit.IsInfiltratingSide(unit.Side))
            {
                unit.OnDiscard = () =>
                {
                    village.VillagePopulation.AddHireable(unit);
                    Debug.Log(unit.Name + " is returning to " + village.Name);
                };
            }
            UpdateActorList();
            GenText();
            if (army.Units.Count > Config.ScoutMax && army.RemainingMP > Config.ArmyMP)
            {
                army.RemainingMP = Config.ArmyMP;
            }
        }
        else
        {
            ButtonCallback(13);
            if (empire.Gold > 9)
                State.GameManager.CreateMessageBox("Population too low to hire additional units");
            else
                State.GameManager.CreateMessageBox("Not enough gold to hire additional units");
        }
    }

    void Dismiss()
    {
        if (army.Units.Count > selectedIndex)
        {
            Unit unit = army.Units[selectedIndex];
            var dismissText = ArmyUI.Dismiss.gameObject.GetComponentInChildren(typeof(Text)) as Text;
            if (dismissText.text == "Exfiltrate")
            {
                Exfiltrate(unit);
                return;
            }
            if (unit != null)
            {
                army.Units.Remove(unit);
                army.RecalculateSizeValue();
                UpdateActorList();
                if (village != null)
                {
                    if (unit.Race < Race.Selicia)
                    {
                        if (village.GetTotalPop() == 0)
                        {
                            if (unit.Race >= Race.Selicia)
                                village.Race = State.World.GetEmpireOfSide(army.Side).ReplacedRace;
                            else
                                village.Race = unit.Race;
                        }
                        village.VillagePopulation.AddHireable(unit);
                    }
                    GenText();
                }
                else
                {
                    dismissList.Add(unit);
                    RefreshUnitPanelButtons();
                }
            }
        }
        if (selectedIndex > 0)
            Select(selectedIndex - 1);
        else
            Select(0);
    }

    private void Exfiltrate(Unit unit)
    {
        unit.Side = unit.FixedSide;
        Army destinationArmy = null;
        foreach (Army a in empire.Armies)
        {
            if (a.Position.GetDistance(army.Position) < 2 && StrategicUtilities.ArmyCanFitUnit(a, unit))
            {
                destinationArmy = a;
            }
        }
        if (destinationArmy == null)
        {
            if (empire.Armies.Count() >= Config.MaxArmies)
            {
                State.GameManager.CreateMessageBox("You already have the maximum number of armies and no existing one with free space is adjacent.");
                return;
            }
            bool foundSpot = false;
            int x = 0;
            int y = 0;
            for (int i = 0; i < 50; i++)
            {
                x = State.Rand.Next(army.Position.x - 1, army.Position.x + 2);
                y = State.Rand.Next(army.Position.y - 1, army.Position.y + 2);

                if (StrategicUtilities.IsTileClear(new Vec2i(x, y)))
                {
                    foundSpot = true;
                    break;
                }
            }
            if (foundSpot)
            {
                Vec2i destLoc = new Vec2i(x, y);
                destinationArmy = new Army(empire, new Vec2i(destLoc.x, destLoc.y), unit.FixedSide);
            }
            else
            {
                State.GameManager.CreateMessageBox("Couldn't find a free space for the unit to exfiltrate to.");
                return;
            }
        }
        army.Units.Remove(unit);
        army.RecalculateSizeValue();
        destinationArmy.Units.Add(unit);
        empire.Armies.Add(destinationArmy);
        State.GameManager.SwitchToStrategyMode();
    }

    public void SetUpDisplay()
    {
        RecruitUI.CheapUpgrade.gameObject.SetActive(false);
        RecruitUI.HireSoldier.gameObject.SetActive(false);
        RecruitUI.HireVillageMerc.gameObject.SetActive(false);
        RecruitUI.RecruitSoldier.gameObject.SetActive(false);
        RecruitUI.ResurrectLeader.gameObject.SetActive(false);
        if (ArmyUI.UnitInfoArea.Length != Config.MaximumPossibleArmy)
        {
            //for (int i = ArmyUI.UnitFolder.childCount - 1; i >= 0; i--)
            //{
            //    Destroy(ArmyUI.UnitFolder.GetChild(i).gameObject);
            //}
            ArmyUI.UnitInfoArea = new UIUnitSprite[Config.MaximumPossibleArmy];
            for (int i = 0; i < Config.MaximumPossibleArmy; i++)
            {
                ArmyUI.UnitInfoArea[i] = Instantiate(State.GameManager.UIUnit, ArmyUI.UnitFolder).GetComponent<UIUnitSprite>();
                ArmyUI.UnitInfoArea[i].gameObject.SetActive(false);
            }
        }
        ArmyUI.Selector.transform.SetAsLastSibling();
        if (infoPanel == null)
        {
            infoPanel = new InfoPanel(ArmyUI.InfoPanel);
        }
        infoPanel.ClearText();
        displayUnits = new Actor_Unit[Config.MaximumPossibleArmy];
        displayCreatedUnit = new Unit[Config.MaximumPossibleArmy];
        for (int x = 0; x < Config.MaximumPossibleArmy; x++)
        {
            ArmyUI.UnitInfoArea[x].SetIndex(x);
        }
        UpdateActorList();
        if (army.Units.Count > 0)
            Select(0);
        else
        {
            selectedIndex = -1;
            UpdateUnitInfoPanel();
        }
        BannerType.gameObject.SetActive(false);
        ShowBanner.gameObject.SetActive(true);
        RecruitUI.AddUnit.gameObject.SetActive(Config.CheatAddUnitButton);
        RefreshUnitPanelButtons();

    }

    public void UpdateActorList()
    {
        leader = army.LeaderIfInArmy();
        Vec2i noLoc = new Vec2i(0, 0);
        for (int x = 0; x < Config.MaximumPossibleArmy; x++)
        {
            if (x >= army.Units.Count)
            {
                displayUnits[x] = null;
                displayCreatedUnit[x] = null;
                continue;
            }
            else if (army.Units[x] != displayCreatedUnit[x])
            {
                displayUnits[x] = new Actor_Unit(noLoc, army.Units[x]);
                displayCreatedUnit[x] = army.Units[x];
                displayUnits[x].UpdateBestWeapons();
                army.Units[x].CurrentLeader = leader;
            }
            //else it already exists and is correct, so we do nothing
        }
        //ArmyUI.UnitInfoAreaSize.sizeDelta = new Vector2(1400, Mathf.Max((5 + army.Units.Count()) / 6 * 240, 900));
        UpdateDrawnActors();
    }

    void CheatAddUnit()
    {
        if (army == null)
        {
            State.GameManager.CreateMessageBox("Can't create new enemy armies with this function");
            return;
        }

        Empire thisEmpire = State.World.GetEmpireOfSide(army.Side);
        thisEmpire = thisEmpire ?? empire;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            for (int i = 0; i < 48; i++)
            {
                Unit unit = new Unit(thisEmpire.Side, thisEmpire.ReplacedRace, thisEmpire.StartingXP, thisEmpire.CanVore);
                if (StrategicUtilities.ArmyCanFitUnit(army, unit))
                {
                    army.Units.Add(unit);
                }
            }
        }
        else
        {
            Unit unit = new Unit(thisEmpire.Side, thisEmpire.ReplacedRace, thisEmpire.StartingXP, thisEmpire.CanVore);
            if (StrategicUtilities.ArmyCanFitUnit(army, unit))
            {
                army.Units.Add(unit);
            }
        }

        if (village != null)
            GenText();
        UpdateActorList();
        if (army.Units.Count > Config.ScoutMax && army.RemainingMP > Config.ArmyMP)
        {
            army.RemainingMP = Config.ArmyMP;
        }
    }

    /// <summary>
    /// Auto-called by UpdateActorList for now
    /// </summary>
    void UpdateDrawnActors()
    {
        if (army == null)
            return;

        for (int x = 0; x < Config.MaximumPossibleArmy; x++)
        {
            if (displayUnits[x] == null)
            {
                ArmyUI.UnitInfoArea[x].gameObject.SetActive(false);
                continue;
            }
            ArmyUI.UnitInfoArea[x].gameObject.SetActive(true);
            ArmyUI.UnitInfoArea[x].UpdateSprites(displayUnits[x]);
            ArmyUI.UnitInfoArea[x].Name.text = army.Units[x].Name;
        }

    }

    public void BuildRaceDisplay()
    {
        int children = RaceUI.ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(RaceUI.ActorFolder.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < village.VillagePopulation.Population.Count; i++)
        {
            if (village.VillagePopulation.Population[i].Population - village.VillagePopulation.Population[i].Hireables > 0)
            {
                GameObject obj = Instantiate(RaceUI.RecruitPanel, RaceUI.ActorFolder);
                UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
                Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), new Unit(1, village.VillagePopulation.Population[i].Race, 0, true));
                TextMeshProUGUI text = obj.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
                var racePar = RaceParameters.GetTraitData(actor.Unit);
                text.text = $"{village.VillagePopulation.Population[i].Race}\nAvailable: {village.VillagePopulation.Population[i].Population - village.VillagePopulation.Population[i].Hireables}\nFavored Stat: {State.RaceSettings.GetFavoredStat(actor.Unit.Race)}\nDefault Traits:\n{State.RaceSettings.ListTraits(actor.Unit.Race)}";
                sprite.UpdateSprites(actor);
                Button button = obj.GetComponentInChildren<Button>();
                Race tempRace = village.VillagePopulation.Population[i].Race;
                button.onClick.AddListener(() => Recruit(tempRace));
                button.onClick.AddListener(() => BuildRaceDisplay());

            }

        }

        RaceUI.gameObject.SetActive(true);

    }

    private void Recruit(Race race)
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            var unit = village.RecruitPlayerUnit(empire, army, race);
            for (int i = 0; i < 48; i++)
            {
                if (unit != null)
                {
                    if (unit.HasTrait(Traits.Infiltrator) && !unit.IsInfiltratingSide(unit.Side))
                    {
                        unit.OnDiscard = () =>
                        {
                            village.VillagePopulation.AddHireable(unit);
                            Debug.Log(unit.Name + " is returning to " + village.Name);
                        };
                    }
                    unit = village.RecruitPlayerUnit(empire, army, race);
                }
                else
                    break;
            }
            UpdateActorList();
            GenText();
            if (army.Units.Count > Config.ScoutMax && army.RemainingMP > Config.ArmyMP)
            {
                army.RemainingMP = Config.ArmyMP;
            }
        }
        else
        {
            Unit unit = village.RecruitPlayerUnit(empire, army, race);
            if ( unit != null)
            {
                if (unit.HasTrait(Traits.Infiltrator) && !unit.IsInfiltratingSide(unit.Side))
                {
                    unit.OnDiscard = () =>
                    {
                        village.VillagePopulation.AddHireable(unit);
                        Debug.Log(unit.Name + " is returning to " + village.Name);
                    };
                }
                UpdateActorList();
                GenText();
                if (army.Units.Count > Config.ScoutMax && army.RemainingMP > Config.ArmyMP)
                {
                    army.RemainingMP = Config.ArmyMP;
                }
            }
        }

    }

    private void CheckButtonStatus()
    {
        foreach (var item in buttonList)
        {
            if (item.Key == null)
            {
                continue;
            }
            if (StrategicUtilities.ArmyCanFitUnit(army, item.Value))
                item.Key.interactable = true;
            else
                item.Key.interactable = false;
        }
    }

}
