using LegacyAI;
using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class Empire
{
    [OdinSerialize]
    public bool KnockedOut;
    [OdinSerialize]
    public int Team;
    [OdinSerialize]
    public int Side { get; private set; }
    [OdinSerialize]
    public Race Race;
    [OdinSerialize]
    public Race ReplacedRace;
    [OdinSerialize]
    int gold;
    [OdinSerialize]
    public ConstructionResources constructionResources;
    [OdinSerialize]
    public List<Vec2i> OwnedTiles;
    [OdinSerialize]
    public int Income { get; private set; }
    [OdinSerialize]
    public List<Army> Armies;
    [OdinSerialize]
    public List<ConstructibleBuilding> Buildings;

    [OdinSerialize]
    public int MaxArmySize;
    [OdinSerialize]
    public int OrigMaxArmySize;
    [OdinSerialize]
    public int MaxGarrisonSize;
    [OdinSerialize]
    public int OrigMaxGarrisonSize;

    [OdinSerialize]
    public string Name;

    [OdinSerialize]
    public int ArmiesCreated;

    [OdinSerialize]
    internal List<StrategicReport> Reports;

    [OdinSerialize]
    public Leader Leader;

    [OdinSerialize]
    public string FakeLeaderName;

    private Village _capitalCity;

    [OdinSerialize]
    public Color UnityColor { get; set; }
    [OdinSerialize]
    public Color UnitySecondaryColor { get; set; }
    [OdinSerialize]
    public int BannerType;
    [OdinSerialize]
    public IStrategicAI StrategicAI;
    [OdinSerialize]
    public TacticalAIType TacticalAIType;

    [OdinSerialize]
    public Dictionary<int, bool> EventHappened;

    [OdinSerialize]
    public Dictionary<AcademyResearchType, int> AcademyResearchCompleted;
    [OdinSerialize]
    public int AcademyUpgradeEXPCost;

    [OdinSerialize]
    public Dictionary<ConstructibleType, int> EmpireBuildingLimit;
    [OdinSerialize]
    public Dictionary<LaboratoryPotion, int> EmpirePotions;

    [OdinSerialize]
    public List<int> RecentEvents;

    [OdinSerialize]
    public int TurnOrder;

    [OdinSerialize]
    public bool CanVore = true;

    public int VillageCount => State.World.Villages.Where(s => s.Side == Side).Count();

    public bool IsAlly(Empire empire)
    {
        if (empire == null)
            return false;
        if (Side == empire.Side || RelationsManager.GetRelation(Side, empire.Side).Type == RelationState.Allied)
            return true;
        return false;
    }

    public bool IsEnemy(Empire empire)
    {
        if (empire == null)
            return false;
        if (Side == empire.Side)
            return false;
        if (RelationsManager.GetRelation(Side, empire.Side).Type == RelationState.Enemies)
            return true;
        return false;
    }

    public bool IsNeutral(Empire empire)
    {
        if (empire == null)
            return false;
        if (Side == empire.Side)
            return false;
        if (RelationsManager.GetRelation(Side, empire.Side).Type == RelationState.Neutral)
            return true;
        return false;
    }

    public int StartingXP
    {
        get
        {
            if (Boosts == null)
                RecalculateBoosts(State.World.Villages);
            if (StrategicAI != null && StrategicAI is StrategicAI ai)
            {
                if (ai.CheatLevel == 2)
                    return Mathf.Max(StrategicUtilities.Get80thExperiencePercentile() / 4, (int)(Boosts.StartingExpAdd * 1.2f));
                if (ai.CheatLevel == 3)
                    return Mathf.Max((int)(StrategicUtilities.Get80thExperiencePercentile() / 1.5f), (int)(Boosts.StartingExpAdd * 1.5f));
                return Boosts.StartingExpAdd;
            }
            else
                return Boosts.StartingExpAdd;
        }
    }

    public struct ConstructionArgs
    {
        internal int side;
        internal Color color;
        internal Color secColor;
        internal int bannerType;
        internal StrategyAIType strategicAI;
        internal TacticalAIType tacticalAI;
        internal int team;
        internal int maxArmySize;
        internal int maxGarrisonSize;

        public ConstructionArgs(int side, Color color, Color secColor, int bannerType, StrategyAIType strategicAI, TacticalAIType tacticalAI, int team, int maxArmySize, int maxGarrisonSize)
        {
            this.side = side;
            this.color = color;
            this.secColor = secColor;
            this.bannerType = bannerType;
            this.strategicAI = strategicAI;
            this.tacticalAI = tacticalAI;
            this.team = team;
            this.maxArmySize = maxArmySize;
            this.maxGarrisonSize = maxGarrisonSize;
        }
    }

    public EmpireBoosts Boosts = new EmpireBoosts();

    public Empire(ConstructionArgs args)
    {
        Reports = new List<StrategicReport>();
        KnockedOut = false;
        BannerType = args.bannerType;
        UnityColor = args.color;
        UnitySecondaryColor = args.secColor;
        gold = Config.StartingGold;
        Income = 0;
        Race = (Race)args.side;
        ReplacedRace = Race;
        Side = args.side;
        Team = args.team;
        MaxArmySize = args.maxArmySize;
        OrigMaxArmySize = args.maxArmySize;
        MaxGarrisonSize = args.maxGarrisonSize;
        OrigMaxGarrisonSize = args.maxGarrisonSize;
        Armies = new List<Army>();
        Buildings = new List<ConstructibleBuilding>();
        InitBuildLimit();
        OwnedTiles = new List<Vec2i>();
        AcademyResearchCompleted = new Dictionary<AcademyResearchType, int>();
        EmpirePotions = new Dictionary<LaboratoryPotion, int>();
        constructionResources = new ConstructionResources();
        constructionResources.Reset();
        Name = Race.ToString();
        if (args.strategicAI == StrategyAIType.None)
            StrategicAI = null;
        else if (args.strategicAI == StrategyAIType.Passive)
            StrategicAI = new PassiveAI(args.side);
        else if (args.strategicAI == StrategyAIType.Basic)
            StrategicAI = new StrategicAI(this, 0, false);
        else if (args.strategicAI == StrategyAIType.Advanced)
            StrategicAI = new StrategicAI(this, 0, true);
        else if (args.strategicAI == StrategyAIType.Cheating1)
            StrategicAI = new StrategicAI(this, 1, true);
        else if (args.strategicAI == StrategyAIType.Cheating2)
            StrategicAI = new StrategicAI(this, 2, true);
        else if (args.strategicAI == StrategyAIType.Cheating3)
            StrategicAI = new StrategicAI(this, 3, true);
        else if (args.strategicAI == StrategyAIType.Legacy)
            StrategicAI = new LegacyStrategicAI(args.side);
        TacticalAIType = args.tacticalAI;
        Boosts = new EmpireBoosts();
        EventHappened = new Dictionary<int, bool>();
        AcademyUpgradeEXPCost = Config.BuildConfig.AcademyUpgradeCost;
        RecentEvents = new List<int>();

        var raceFlags = State.RaceSettings.GetRaceTraits(Race);
        if (raceFlags != null)
        {
            if (raceFlags.Contains(Traits.Prey))
                CanVore = false;
        }
    }

    //Dummy implementation for the pure tactical
    public Empire()
    {
        Boosts = new EmpireBoosts();

    }

    public void LoadFix()
    {
        if (Reports == null)
            Reports = new List<StrategicReport>();
        if (EventHappened == null)
            EventHappened = new Dictionary<int, bool>();
        if (AcademyResearchCompleted == null)
            AcademyResearchCompleted = new Dictionary<AcademyResearchType, int>();
        if (EmpirePotions == null)
            EmpirePotions = new Dictionary<LaboratoryPotion, int>();
    }

    internal void CheckEvent()
    {
        State.EventList.CheckStartEvent(this);
    }



    public void CalcIncome(Village[] villages, bool AddToStats = false)
    {
        RecalculateBoosts(villages);

        Income = 0;
        for (int i = 0; i < villages.Length; i++)
        {
            if (villages[i].Side == Side)
            {
                villages[i].UpdateNetBoosts();
                int value = villages[i].GetIncome();
                Income = Income + value;
            }
        }
        if (Side < 50)
        {
            for (int i = 0; i < Armies.Count; i++)
            {
                int upkeep = GetUpkeep();
                Income = Income - (int)(upkeep - (upkeep * 0.125f * AcademyResearch.GetValueFromEmpire(this, AcademyResearchType.GoldMineIncome)));
                if (AddToStats)
                {
                    State.World.Stats.CollectedGold(Armies[i].Units.Count * 2, Armies[i].Side);
                    State.World.Stats.SpentGoldOnArmyMaintenance(Armies[i].Units.Count * 2, Armies[i].Side);
                }
            }
        }
        foreach (ClaimableBuilding claimable in State.World.Claimables)
        {
            if (claimable is GoldMine && claimable.Owner == this)
            {
                Income += Config.GoldMineIncome + (int)(Config.GoldMineIncome * 0.125f * AcademyResearch.GetValueFromEmpire(this, AcademyResearchType.GoldMineIncome));
            }
        }
        foreach (ConstructibleBuilding constructible in Buildings)
        {
            // Only add building here if it generates or removes income.
            if (constructible is WorkCamp)
            {
                Income += 10;
            }
            if (constructible is Academy)
            {
                Academy academy = constructible as Academy;
                Income -= academy.Income1;
                Income -= academy.Income2;
            }
        }
        Income += Boosts.WealthAdd;
    }

    public void SpendGold(int gold)
    {
        this.gold -= gold;
        State.World.Stats.SpentGold(gold, Side);
    }

    /// <summary>
    /// Not to be confuese with SpendGold.
    /// Made for storage in the bulding AI.
    /// </summary>
    public void RemoveGold(int gold)
    {
        this.gold -= gold;
    }

    public void AddGold(int gold)
    {
        this.gold += gold;
        if (gold > 0)
            State.World.Stats.CollectedGold(gold, Side);
        else
            State.World.Stats.SpentGold(-gold, Side);


    }

    public int Gold => gold;

    public Village CapitalCity
    {
        get
        {
            if (_capitalCity == null)
            {
                _capitalCity = State.World.Villages.Where(s => s.Capital && s.OriginalRace == Race).FirstOrDefault();
                if (_capitalCity == null)
                {
                    _capitalCity = State.World.Villages.Where(s => s.OriginalRace == Race).FirstOrDefault();
                }
            }


            return _capitalCity;
        }
    }

    public void ArmyCleanup()
    {
        foreach (Army army in Armies.ToList())
        {
            army.Prune();
            army.JustCreated = false;
        }

    }

    public void Regenerate()
    {
        for (int i = 0; i < Armies.Count; i++)
        {
            Armies[i].Refresh();
        }
        if (Config.FactionLeaders && Side < 50)
        {
            if (Leader == null)
                GenerateLeader();
            if (Leader != null)
            {
                if (Leader.IsDead == false && GetAllUnitsIncludingTravelersAndStandby().Contains(Leader) == false && Config.DontFixLeaders == false)
                    Leader.Health = -99999;
                if (StrategicAI != null && Leader.IsDead == false)
                {
                    if (Leader.Items[0] == null)
                    {
                        if (State.Rand.Next(2) == 0)
                            Shop.BuyItem(this, Leader, State.World.ItemRepository.GetItem(ItemType.CompoundBow));
                        else
                            Shop.BuyItem(this, Leader, State.World.ItemRepository.GetItem(ItemType.Axe));
                    }
                    else if (Leader.Items[0] is Weapon == false)
                    {
                        Debug.Log("Sold invalid leader item");
                        Shop.SellItem(this, Leader, 0);

                        if (State.Rand.Next(2) == 0)
                            Shop.BuyItem(this, Leader, State.World.ItemRepository.GetItem(ItemType.CompoundBow));
                        else
                            Shop.BuyItem(this, Leader, State.World.ItemRepository.GetItem(ItemType.Axe));
                    }
                    else if (Leader.Items[1] == null)
                        Shop.BuyItem(this, Leader, State.World.ItemRepository.GetItem(ItemType.Helmet));
                    else if (Leader.Items.Length > 2 && Leader.Items[2] == null)
                        Shop.BuyItem(this, Leader, State.World.ItemRepository.GetItem(ItemType.BodyArmor));
                }
            }



        }

    }

    void GenerateLeader()
    {
        var capital = CapitalCity;
        if (capital == null)
            capital = State.World.Villages.Where(s => s.Side == Side).FirstOrDefault();
        if (capital == null)
            return;
        Leader = new Leader(Side, State.RaceSettings.GetLeaderRace(Race), 0);
        PlaceLeader(capital);
    }

    private void PlaceLeader(Village capital)
    {
        var CapitalArmy = StrategicUtilities.ArmyAt(capital.Position);
        if (CapitalArmy != null && StrategicUtilities.ArmyCanFitUnit(CapitalArmy, Leader) && CapitalArmy.Side == Side)
            CapitalArmy.Units.Add(Leader);
        else
        {
            if (CapitalArmy == null)
            {
                var army = new Army(this, new Vec2i(capital.Position.x, capital.Position.y), Side);
                Armies.Add(army);
                army.Units.Add(Leader);
            }
            else
            {
                Leader.Health = -1;
            }

        }
    }


    public List<Unit> GetAllUnits()
    {
        List<Unit> units = new List<Unit>();
        foreach (Army army in Armies)
        {
            foreach (Unit unit in army.Units)
            {
                units.Add(unit);
            }
        }
        return units;
    }

    public List<Unit> GetAllUnitsIncludingTravelersAndStandby()
    {
        List<Unit> units = new List<Unit>();
        foreach (Army army in Armies)
        {
            foreach (Unit unit in army.Units)
            {
                units.Add(unit);
            }
        }
        foreach (Village village in State.World.Villages)
        {
            foreach (var unit in village.GetRecruitables())
            {
                units.Add(unit);
            }
            if (village.travelers != null)
            {
                foreach (var unit in village.travelers)
                {
                    units.Add(unit.unit);
                }
            }

        }
        return units;
    }

    public EmpireBoosts RecalculateBoosts(Village[] villages)
    {
        if (Boosts == null) Boosts = new EmpireBoosts();
        Boosts.ResetValues();

        var teamVillageList = new List<Village>();
        for (var i = 0; i < villages.Length; i++)
        {
            var theVillage = villages[i];
            if (theVillage != null && (theVillage.Empire?.IsAlly(this) ?? false))
            {
                teamVillageList.Add(theVillage);
            }
        }
        List<Empire> checkedEmpire = new List<Empire>();
        foreach (var village in teamVillageList)
        {
            Boosts.WealthAdd += village.NetBoosts.TeamWealthAdd;
            Boosts.StartingExpAdd += village.NetBoosts.TeamStartingExpAdd;
            if (!checkedEmpire.Contains(village.Empire))
            {
                Boosts.StartingExpAdd += 10 * (int)AcademyResearch.GetValueFromEmpire(village.Empire, AcademyResearchType.TeamEXP);
                checkedEmpire.Add(village.Empire);
            }
        }
        return Boosts;
    }

    internal void CheckAutoLevel()
    {
        foreach (Army army in Armies)
        {
            foreach (Unit unit in army.Units)
            {
                if (unit.AutoLeveling)
                {
                    StrategicUtilities.SpendLevelUps(unit);
                }
            }
        }
    }

    internal int GetUpkeep()
    {
        float totalCost = 0;
        foreach (Army army in Armies)
        {
            foreach (Unit unit in army.Units)
            {
                if (Config.World.ArmyUpkeep >= 0)
                    totalCost += Config.World.ArmyUpkeep;
                else
                    totalCost += State.RaceSettings.GetUpkeep(unit.Race) * unit.TraitBoosts.UpkeepMult;
            }
        }
        return (int)Math.Round(totalCost);
    }
    internal float GetUpkeepCoefficient()
    {

        if (Config.World.ArmyUpkeep >= 0)
            return Config.World.ArmyUpkeep;

        float currentCoeff = 0;
        int unitCount = 0;
        foreach (Army army in Armies)
        {
            foreach(Unit unit in army.Units)
            {
                currentCoeff += State.RaceSettings.GetUpkeep(unit.Race) * unit.TraitBoosts.UpkeepMult;
                unitCount++;
            }
        }
        if (unitCount == 0)
        {
            return 3;
        }
        return currentCoeff/unitCount;
    }
    internal float GetAvgDeployCost()
    {
        float ideal_size_Mult = 0;
        foreach (var army in Armies)
        {
            ideal_size_Mult += army.GetAverageArmyDeployment();
        }
        if (ideal_size_Mult == 0)
            State.RaceSettings.GetDeployCost(Race);
        ideal_size_Mult /= Armies.Count;
        return ideal_size_Mult;
    }

    internal void InitBuildLimit()
    {
        EmpireBuildingLimit = new Dictionary<ConstructibleType, int>
        {
            [ConstructibleType.WorkCamp] = Config.BuildConfig.WorkCamp.BuildLimit,
            [ConstructibleType.LumberSite] = Config.BuildConfig.LumberSite.BuildLimit,
            [ConstructibleType.Quarry] = Config.BuildConfig.Quarry.BuildLimit,
            [ConstructibleType.CasterTower] = Config.BuildConfig.CasterTower.BuildLimit,
            [ConstructibleType.BarrierTower] = Config.BuildConfig.BarrierTower.BuildLimit,
            [ConstructibleType.DefEncampment] = Config.BuildConfig.DefenseEncampment.BuildLimit,
            [ConstructibleType.Academy] = Config.BuildConfig.Academy.BuildLimit,
            [ConstructibleType.DarkMagicTower] = Config.BuildConfig.DarkMagicTower.BuildLimit,
            [ConstructibleType.TemporalTower] = Config.BuildConfig.TemporalTower.BuildLimit,
            [ConstructibleType.Teleporter] = Config.BuildConfig.Teleporter.BuildLimit,
            [ConstructibleType.Laboratory] = Config.BuildConfig.Laboratory.BuildLimit,
            [ConstructibleType.TownHall] = Config.BuildConfig.TownHall.BuildLimit,
        };
    }

    internal bool WithinBuildLimit(ConstructibleType type)
    {
        if (EmpireBuildingLimit[type] == 0)
        {
            return false;
        }
        return true;
    }
}
