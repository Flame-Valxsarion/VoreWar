using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;


public class Village
{
    [OdinSerialize]
    public List<VillageBuilding> buildings;

    [OdinSerialize]
    public bool Capital { get; set; }

    [OdinSerialize]
    public Race OriginalRace { get; set; }

    [OdinSerialize]
    public int Side { get; private set; }

    [OdinSerialize]
    public Race Race { get; set; }
    [OdinSerialize]
    public VillagePopulation VillagePopulation;

    public int Population => VillagePopulation.GetTotalPop();
    [OdinSerialize]
    public int Maxpop { get; set; }
    [OdinSerialize]
    public int FarmCount { get; set; }
    [OdinSerialize]
    public string Name { get; set; }
    [OdinSerialize]
    public Vec2i Position { get; set; }

    [OdinSerialize] int TurnDestroyed = 0;

    [OdinSerialize] internal float Happiness = 100;

    [OdinSerialize]
    internal List<InvisibleTravelingUnit> travelers;

    [OdinSerialize]
    internal List<MercenaryContainer> Mercenaries;
    [OdinSerialize]
    internal List<MercenaryContainer> Adventurers;

    [OdinSerialize]
    private ItemStock itemStock;

    internal ItemStock ItemStock
    {
        get { if (itemStock == null) itemStock = new ItemStock(); return itemStock; }
        set { itemStock = value; }
    }


    public VillageBoosts NetBoosts { get; set; }

    static List<Race> AvailableRaces;
    static int TurnRefreshed;


    public int Garrison
    {
        get
        {
            int usefulRecruitables = 0;
            if (VillagePopulation.GetRecruitables() != null)
                usefulRecruitables = VillagePopulation.GetRecruitables().Where(s => s.HasWeapon || s.HasBook || s.HasTrait(Traits.Feral)).Count();
            var majority = VillagePopulation.GetMostPopulousRace();
            if (RaceParameters.GetRaceTraits(majority).RacialTraits.Contains(Traits.Feral))
                usefulRecruitables += VillagePopulation.GetRacePop(majority);
            return Math.Min(Math.Min(VillagePopulation.GetTotalPop(), usefulRecruitables + Weapons.Count), MaxGarrisonSize);
        }
    }

    [OdinSerialize]
    public List<ItemType> Weapons { get; private set; }

    internal Empire Empire => State.World.GetEmpireOfSide(Side);

    [OdinSerialize]
    [Obsolete]
    public List<Unit> NamedRecruitables;

    public int MaxGarrisonSize = 0;

    public Village(string name, Vec2i p, int fields, Race race, bool capital)
    {
        buildings = new List<VillageBuilding>();
        Position = p;
        Name = name;
        if (capital)
        {
            buildings.Add(VillageBuilding.inn);
            buildings.Add(VillageBuilding.mill);
            buildings.Add(VillageBuilding.wall);
            buildings.Add(VillageBuilding.manor);
            buildings.Add(VillageBuilding.trainer);
            buildings.Add(VillageBuilding.CapitalDefenses);
            Capital = capital;
        }
        Race = race;
        OriginalRace = race;
        Side = (int)race;
        FarmCount = fields;
        Maxpop = FarmCount * Config.VillagersPerFarm;
        Weapons = new List<ItemType>();
        if (capital)
        {
            Weapons = new List<ItemType> { ItemType.Axe, ItemType.Axe, ItemType.Axe, ItemType.Axe, ItemType.CompoundBow, ItemType.CompoundBow, ItemType.CompoundBow, ItemType.CompoundBow };
        }

        int effectiveMax = Math.Min(Maxpop, Config.StartingPopulation);
        VillagePopulation = new VillagePopulation(Race, effectiveMax, this);
        NetBoosts = new VillageBoosts();
        MaxGarrisonSize = State.World.GetEmpireOfSide(Side)?.MaxGarrisonSize ?? 0;
    }


    internal void TutorialWeapons()
    {
        Weapons = new List<ItemType> { ItemType.Mace, ItemType.Mace, ItemType.Bow, ItemType.Bow, };
    }

    public void UpdateNetBoosts()
    {
        if (NetBoosts == null)
            NetBoosts = new VillageBoosts();
        NetBoosts.ResetValues();

        foreach (var building in buildings)
        {
            var buildingDef = VillageBuildingList.GetBuildingDefinition(building);
            if (buildingDef != null)
                NetBoosts = NetBoosts.MergeBoosts(buildingDef.Boosts);
        }

        NetBoosts.WealthMult = ConvertZeroBasedFloatToMultiplierOrDivider(NetBoosts.WealthMult);
        NetBoosts.PopulationGrowthMult = ConvertZeroBasedFloatToMultiplierOrDivider(NetBoosts.PopulationGrowthMult);
        NetBoosts.PopulationMaxMult = ConvertZeroBasedFloatToMultiplierOrDivider(NetBoosts.PopulationMaxMult);
        NetBoosts.GarrisonMaxMult = ConvertZeroBasedFloatToMultiplierOrDivider(NetBoosts.GarrisonMaxMult);
        NetBoosts.HealRateMult = ConvertZeroBasedFloatToMultiplierOrDivider(NetBoosts.HealRateMult);

        float maxPopCalc = (float)FarmCount + NetBoosts.FarmsEquivalent;
        maxPopCalc *= (float)Config.VillagersPerFarm;
        Maxpop = (int)(maxPopCalc * NetBoosts.PopulationMaxMult);
        Maxpop += (int)(Maxpop * (0.1f * AcademyResearch.GetValueFromEmpire(Empire, AcademyResearchType.VilPopMax)));
        VillagePopulation.CheckMaxpop(Maxpop);

        int baseGarrisonSize = (State.World.GetEmpireOfSide(Side)?.MaxGarrisonSize ?? 0);

        MaxGarrisonSize = (int)(baseGarrisonSize * NetBoosts.GarrisonMaxMult) + NetBoosts.GarrisonMaxAdd;

        if (Config.CapMaxGarrisonIncrease && MaxGarrisonSize > baseGarrisonSize * 1.5f)
        {
            MaxGarrisonSize = (int)(baseGarrisonSize * 1.5f);
        }
    }

    static private float ConvertZeroBasedFloatToMultiplierOrDivider(float value)
    {
        if (value >= 0.0f)
        {
            return value + 1.0f;
        }
        else
        {
            return 1.0f / (1.0f + Math.Abs(value));
        }
    }

    internal void UpdateFarms(int farms)
    {
        FarmCount = farms;
        UpdateNetBoosts();
    }

    static internal BuildingCost GetCost(VillageBuilding building)
    {
        var buildingDef = VillageBuildingList.GetBuildingDefinition(building);
        return buildingDef.Cost;
    }

    internal BuildingCost GetCostAllBuildings()
    {
        BuildingCost summedCost = new BuildingCost();
        foreach (var building in VillageBuildingList.GetListOfBuildingEnum())
        {
            var buildingDef = VillageBuildingList.GetBuildingDefinition(building);
            if (buildings.Contains(building) == false && buildingDef.CanBuild(this))
            {
                var thisCost = VillageBuildingDefinition.GetCost(building);
                summedCost.Wealth += thisCost.Wealth;
                summedCost.LeaderExperience += thisCost.LeaderExperience;
            }
        }
        return summedCost;
    }

    internal void BuyAllBuildings(Empire buyingEmpire)
    {
        foreach (var building in VillageBuildingList.GetListOfBuildingEnum())
        {
            var buildingDef = VillageBuildingList.GetBuildingDefinition(building);
            if (buildings.Contains(building) == false && buildingDef.CanBuild(this))
            {
                Build(building, buyingEmpire);
            }
        }
    }

    public void Build(VillageBuilding building, Empire buyingEmpire)
    {
        var buildingDef = VillageBuildingList.GetBuildingDefinition(building);
        if (buildings.Contains(building))
            return;

        if (buildingDef.CanBuild(this) == false)
            return;

        if (buildingDef.CanAfford(buyingEmpire) == false)
            return;

        if (buildingDef.Cost.Wealth > 0)
        {
            buyingEmpire.SpendGold(buildingDef.Cost.Wealth);
            State.World.Stats.SpentGoldOnBuildings(buildingDef.Cost.Wealth, Side);
        }

        if (buildingDef.Cost.LeaderExperience > 0)
        {
            buyingEmpire.Leader?.DrainExp(buildingDef.Cost.LeaderExperience);
        }

        buildings.Add(building);
        UpdateNetBoosts();
    }

    public void ChangeOwner(int side)
    {

        int previousSide = Side;
        if (side == Side)
            return;
        Side = side;

        if (State.World?.Villages == null)
            return;

        if (State.GameManager.CurrentScene != State.GameManager.MapEditor)
            NotificationSystem.VillageOwnerChanged(this, previousSide, side);



        Empire tempOwner = State.World.GetEmpireOfSide(Side);

        if (tempOwner != null && Name.Contains("Abandoned town") && tempOwner.Race < (Race)170)
        {
            for (int i = 1; i < 100; i++)
            {
                string tempName = State.NameGen.GetTownName(tempOwner.Race, i);
                if (State.World.Villages.Where(s => s.Name == tempName).Any() == false)
                {
                    Name = tempName;
                    break;
                }
            }
        }

        if (tempOwner != null && tempOwner.StrategicAI == null)
        {
            if (State.World.GetEmpireOfRace(Race)?.IsAlly(Empire) ?? false)
            {
                var raceEmp = State.World.GetEmpireOfRace(Race);
                if (raceEmp != tempOwner && raceEmp != null)
                {
                    float currentHappiness = Happiness;
                    var box = State.GameManager.CreateDialogBox();
                    box.SetData(() => { Happiness = currentHappiness; ChangeOwner(raceEmp.Side); RelationsManager.CityReturned(tempOwner, raceEmp); }, "Give it back", "Keep it", $"This village is of the race of {raceEmp.Name} (Ally). If you give it back to them they will be pleased with you");
                }

            }
            if (State.World.GetEmpireOfRace(Race)?.IsNeutral(Empire) ?? false)
            {
                var raceEmp = State.World.GetEmpireOfRace(Race);
                if (raceEmp != tempOwner && raceEmp != null)
                {
                    float currentHappiness = Happiness;
                    var box = State.GameManager.CreateDialogBox();
                    box.SetData(() => { Happiness = currentHappiness; ChangeOwner(raceEmp.Side); RelationsManager.CityReturned(tempOwner, raceEmp); }, "Give it back", "Keep it", $"This village is of the race of {raceEmp.Name} (Peace). If you give it back to them they will be pleased with you");
                }

            }
        }
        else if (tempOwner != null)
        {
            var raceEmp = State.World.GetEmpireOfRace(Race);
            if (raceEmp != tempOwner && raceEmp != null && State.World.GetEmpireOfRace(Race).IsAlly(Empire))
            {
                float currentHappiness = Happiness;
                ChangeOwner(raceEmp.Side);
                Happiness = currentHappiness;
                RelationsManager.CityReturned(tempOwner, raceEmp);
                return;
            }
        }


        if (State.World.Villages.Where(s => s.Side == previousSide).Count() == 0)
        {
            var previousEmp = State.World.GetEmpireOfSide(previousSide);
            if (previousEmp != null)
                NotificationSystem.ShowNotification($"{previousEmp.Name} have lost their last village");
        }

        if (Empire.ReplacedRace == Race)
            Happiness = Mathf.Lerp(Happiness, 100, .5f);
        else
            Happiness *= .8f;

        foreach (Unit unit in VillagePopulation.GetRecruitables().ToList())
        {
            if (unit.Type == UnitType.Leader)
            {
                unit.Health = 0;
            }
            if (unit.Health <= 0)
                VillagePopulation.RemoveHireable(unit);
        }

        foreach (var building in VillageBuildingList.GetListOfBuildingEnum())
        {
            var buildingDef = VillageBuildingList.GetBuildingDefinition(building);
            if (buildingDef.RemovedOnOwnerChange)
            {
                buildings.Remove(building);
            }
            if (buildingDef.AddedOnOriginalOwner && side == (int)OriginalRace)
            {
                if (buildingDef.RequiresRaceCapitol == false ||
                    (buildingDef.RequiresRaceCapitol && Capital))
                {
                    buildings.Add(building);
                }
            }

        }

        UpdateNetBoosts();
        if (State.GameManager.CurrentScene == State.GameManager.MapEditor)
            State.GameManager.MapEditor.RedrawVillages();
        else if (State.GameManager.CurrentScene == State.GameManager.StrategyMode)
            State.GameManager.StrategyMode.RedrawVillages();
    }

    public bool IsSubjugated()
    {
        return Side != (int)Race;
    }

    public bool IsOriginalOwner()
    {
        return Side == (int)OriginalRace;
    }


    public int GetImageNum(int max)
    {
        if (VillagePopulation.GetTotalPop() < 1)
        {
            if (State.World.Turn == TurnDestroyed)
                return 1;
            return 0;
        }
        if (Race >= Race.Vagrants && Race < Race.Selicia)
        {
            int image = 4;
            if (buildings.Contains(VillageBuilding.wall))
                image++;
            return image;
        }
        int ret = (int)Race * 3 + 9;
        if (ret > max - 1)
            ret = 6;
        if (buildings.Contains(VillageBuilding.wall))
            ret++;
        return ret;
    }

    public int GetColoredImageNum(int max)
    {
        if (VillagePopulation.GetTotalPop() < 1)
        {
            return 0;
        }
        if (Race >= Race.Vagrants && Race < Race.Selicia)
        {
            return 0;
        }
        int ret = (int)Race * 3 + 8;
        if (ret > max)
            return 0;
        return ret;

    }

    public bool HasWalls()
    {
        return NetBoosts.hasWall;
    }

    public float Healrate()
    {
        float rate = 0.125F;

        rate *= NetBoosts.HealRateMult;

        return rate;
    }

    public int GetIncome()
    {
        float v = VillagePopulation.GetTotalPop() * .5f * Config.VillageIncomePercent / 100 * (Happiness / 100);

        v = (v * NetBoosts.WealthMult) + NetBoosts.WealthAdd;

        return (int)v;
    }

    internal void ConvertToMultiRace()
    {
        VillagePopulation = new VillagePopulation(Race, Maxpop, this);
#pragma warning disable CS0612 // Type or member is obsolete
        foreach (var recruitable in NamedRecruitables)
#pragma warning restore CS0612 // Type or member is obsolete
        {
            VillagePopulation.RemoveRacePop(Race, 1);
            VillagePopulation.AddHireable(recruitable);
        }
#pragma warning disable CS0612 // Type or member is obsolete
        NamedRecruitables.Clear();
#pragma warning restore CS0612 // Type or member is obsolete
    }

    public void NewTurn()
    {
        if (Config.MultiRaceVillages && Config.MultiRaceFlip)
        {
            var populousRace = VillagePopulation.GetMostPopulousRace();
            if (populousRace != Race)
                Race = populousRace;
        }
        if (Happiness < .2f && Side < 700)
        {
            float chance = .2f - Happiness;
            if (Config.RandomEventRate > 0 && State.Rand.NextDouble() < chance)
            {
                Empire emp = State.World.GetEmpireOfRace(Race);
                Army army = StrategicUtilities.ArmyAt(Position);
                if (emp != null && emp.VillageCount > 0)
                {
                    if (State.Rand.Next(2) == 0 && army == null)
                        ChangeOwner(emp.Side);
                    else
                        ChangeOwner(700);

                }
                else
                {
                    ChangeOwner(700);
                }

                if (army != null && Side != army.Side && Garrison > 0)
                {
                    if (Garrison > 0)
                    {
                        StrategicUtilities.StartBattle(army);
                    }
                    else
                        ChangeOwner(army.Side);
                }

            }
        }

        float targetHappy = Race == Empire.ReplacedRace ? 100 : 80;
        if (targetHappy == 80)
            targetHappy += NetBoosts.MaxHappinessAdd;

        if (Happiness < targetHappy)
            Happiness += .2f + ((targetHappy - Happiness) * .03f);
        else
            Happiness -= ((Happiness - targetHappy) * .06f);
        Happiness += AcademyResearch.GetValueFromEmpire(Empire, AcademyResearchType.Happiness);
        UpdateNetBoosts();
        Growth();
        HealStandbyUnits();
        UpdateTravelers();
        UpdateMercenaries();
    }

    void HealStandbyUnits()
    {
        float healRate = Healrate();
        foreach (Unit unit in VillagePopulation.GetRecruitables().ToList())
        {
            if (unit.Type == UnitType.Leader && unit.Side != Side)
            {
                unit.Health = 0;
                VillagePopulation.RemoveHireable(unit);
            }
            if (unit.IsDead)
                VillagePopulation.RemoveHireable(unit);
            else
            {
                unit.HealPercentage(healRate);
                unit.RestoreManaPct(.6f);
            }
        }
    }

    void Growth()
    {
        double namedBreeders  = 0;
        Army army = StrategicUtilities.ArmyAt(Position);
        SpawnerInfo spawner = null;
        if (army != null)
            spawner = Config.SpawnerInfo((Race)army.Side);
        Config.MonsterConquestType spawnerType;
        if (spawner != null)
            spawnerType = spawner.GetConquestType();
        else
            spawnerType = Config.MonsterConquest;
        if ((army != null && army.Side < 100) || (army != null && (spawnerType == Config.MonsterConquestType.CompleteDevourAndRepopulate || spawnerType == Config.MonsterConquestType.CompleteDevourAndRepopulateFortify)))
        {
            army.Units.ForEach(u =>
            {
                if (!u.HasTrait(Traits.Infertile))
                {
                    namedBreeders += 1;
                    if (u.HasTrait(Traits.ProlificBreeder))
                    {
                        namedBreeders += 0.75;
                    }
                    if (u.HasTrait(Traits.SlowBreeder))
                    {
                        namedBreeders -= 0.30;
                    }
                
                }
            });
        }
        GetRecruitables().ForEach(u =>
        {
            if (!u.HasTrait(Traits.Infertile))
            {
                namedBreeders += 1;
                if (u.HasTrait(Traits.ProlificBreeder))
                {
                    namedBreeders += 0.75;
                }
                if (u.HasTrait(Traits.SlowBreeder))
                {
                    namedBreeders -= 0.30;
                }
            }
        });
        

        if (VillagePopulation.GetTotalPop() == 0 && namedBreeders > 1)
        {
            if (army != null)
            {
                Dictionary<Race, int> count = new Dictionary<Race, int>();
                foreach (Unit unit in army.Units)
                {
                  
                    if (unit.Race >= Race.Selicia && Empire.ReplacedRace != unit.Race)
                        continue;
                    if (State.RaceSettings.GetRaceTraits(unit.Race).Contains(Traits.Infertile))
                        continue;
                    if (count.ContainsKey(unit.Race) == false)
                        count[unit.Race] = 1;
                    else
                        count[unit.Race]++;
                    
                }
                var final = count.OrderByDescending(s => s.Value).ToArray();
                if (final.Length > 0)
                {
                    Race = final[0].Key;
                    VillagePopulation.AddRacePop(final[0].Key, 0);
                }

                else if (State.World.GetEmpireOfSide(army.Side)?.ReplacedRace != null)
                {
                    Race = State.World.GetEmpireOfSide(army.Side).ReplacedRace;
                    if (!State.RaceSettings.GetRaceTraits(Race).Contains(Traits.Infertile))
                        VillagePopulation.AddRacePop(Race, 0);
                }

                else
                    return;

            }
            else
            {
                Debug.Log("Failed second growth check");
                return;
            }
        }
        if ((VillagePopulation.GetTotalPop() + namedBreeders) > 1 && VillagePopulation.GetTotalPop() < Maxpop)
        {
            float growthPct = 0.10F;

            float happyMult = Mathf.Lerp(.2f, 1, Happiness / 100);

            growthPct *= happyMult;

            growthPct *= NetBoosts.PopulationGrowthMult;

            growthPct *= 1 + 0.1f * AcademyResearch.GetValueFromEmpire(Empire, AcademyResearchType.PopBreedInc);

            double unnamedContrib = 0;

            VillagePopulation.Population.ForEach(pop =>
            {
                var traits = State.RaceSettings.GetRaceTraits(pop.Race);
                double breedingContrib = 1;
                if (traits.Contains(Traits.ProlificBreeder))
                    breedingContrib *= 1.75f;
                if (traits.Contains(Traits.SlowBreeder))
                    breedingContrib *= .7f;
                if (traits.Contains(Traits.Infertile))
                    breedingContrib *= 0;
                unnamedContrib += (pop.Population-pop.Hireables) * breedingContrib;
            });
            double totalBreeders = unnamedContrib + namedBreeders;
            int incr;
            int Population = VillagePopulation.GetTotalPop();
            if (totalBreeders == 0)
                incr = 0;
            else
            {
                incr = (int)(1 + (totalBreeders * growthPct));
                if (Population == 0)
                    incr = Math.Max(2, incr);
            }


            if (Maxpop - Population < incr)
            {
                incr = Maxpop - Population;
            }
            VillagePopulation.AddRandomPop(incr);


        }
        else if (VillagePopulation.GetTotalPop() > Maxpop)
        {
            do
            {
                VillagePopulation.DecrementRandom();
            } while (VillagePopulation.GetTotalPop() > Maxpop);
        }
        VillagePopulation.CleanHirables();
    }

    void UpdateTravelers()
    {
        if (travelers == null)
            return;
        foreach (InvisibleTravelingUnit unit in travelers.ToList())
        {

            unit.remainingTurns -= 1;
            if (unit.remainingTurns <= 0)
            {
                travelers.Remove(unit);
                if (unit.unit.IsDead)
                    continue;
                if (unit.unit.Side != Side)
                {
                    var closestFriendlyVillage = State.World.Villages.Where(s => s.Side == unit.unit.Side).OrderBy(s => s.Position.GetNumberOfMovesDistance(Position)).FirstOrDefault();
                    if (closestFriendlyVillage == null)
                        closestFriendlyVillage = State.World.Villages.Where(s => s.Empire.IsAlly(State.World.GetEmpireOfSide(unit.unit.Side))).OrderBy(s => s.Position.GetNumberOfMovesDistance(Position)).FirstOrDefault();
                    if (closestFriendlyVillage != null)
                    {
                        StrategicUtilities.CreateInvisibleTravelingArmy(unit.unit, closestFriendlyVillage, closestFriendlyVillage.Position.GetNumberOfMovesDistance(Position) / Config.ArmyMP);
                        continue;
                    }
                    else if (unit.unit == State.World.GetEmpireOfSide(unit.unit.Side).Leader)
                    {
                        unit.unit.Health = -9999;
                        continue;
                    }
                }
                if (unit.unit == Empire.Leader)
                {
                    var localArmy = StrategicUtilities.ArmyAt(Position);
                    if (localArmy != null && localArmy.Side == Side && StrategicUtilities.ArmyCanFitUnit(localArmy, unit.unit))
                    {
                        Empire.Reports.Add(new StrategicReport($"{unit.unit.Name} (Leader) has arrived at {Name} and auto-joined the army there", new Vec2(Position.x, Position.y)));
                        localArmy.Units.Add(unit.unit);
                        localArmy.RecalculateSizeValue();
                    }
                    else if (localArmy == null)
                    {
                        Empire.Reports.Add(new StrategicReport($"{unit.unit.Name} (Leader) has arrived at {Name} and created a new army there", new Vec2(Position.x, Position.y)));
                        Army army = new Army(Empire, new Vec2i(Position.x, Position.y), Side);
                        Empire.Armies.Add(army);
                        army.Units.Add(unit.unit);
                        army.RecalculateSizeValue();
                    }
                    else
                    {
                        VillagePopulation.AddHireable(unit.unit);
                        Empire.Reports.Add(new StrategicReport($"{unit.unit.Name} (Leader) has arrived at {Name}", new Vec2(Position.x, Position.y)));

                    }
                    continue;
                }
                VillagePopulation.AddHireable(unit.unit);
                Empire.Reports.Add(new StrategicReport($"{unit.unit.Name} has arrived at {Name}", new Vec2(Position.x, Position.y)));
            }
        }
    }

    public void AddPopulation(int p)
    {
        int incr = p;
        int Population = VillagePopulation.GetTotalPop();
        if (Maxpop - Population < p)
        {
            incr = Maxpop - Population;
        }
        VillagePopulation.AddRandomPop(incr);
    }

    public void AddPopulation(Race race)
    {

        if (VillagePopulation.GetTotalPop() < Maxpop)
        {
            VillagePopulation.AddRacePop(race, 1);
        }
    }

    public void AddPopulation(Race race, int pop)
    {
        int incr = pop;
        int Population = VillagePopulation.GetTotalPop();
        if (Maxpop - Population < pop)
        {
            incr = Maxpop - Population;
        }
        for (int x = 0; x < incr; x++)
        {
            AddPopulation(race);
        }
    }

    public void SetPopulation(int p)
    {
        var change = p - Population;
        if (change > 0)
        {
            change = Math.Min(change, Maxpop - Population);
            AddPopulation(change);
        }
        else if (change < 0)
        {
            if (p <= 0)
            {
                VillagePopulation.RemoveAllPop();
                Happiness = 100;
                TurnDestroyed = State.World.Turn;
            }
            else
            {
                VillagePopulation.RemoveRandomPop(-change);
            }

        }
    }

    public void SubtractPopulation(int p)
    {
        int Population = VillagePopulation.GetTotalPop();
        if (Population - p <= 0)
        {
            VillagePopulation.RemoveAllPop();
            Happiness = 100;
            TurnDestroyed = State.World.Turn;
        }
        else
        {
            VillagePopulation.RemoveRandomPop(p);
        }



        VillagePopulation.CleanHirables();
    }

    public void SubtractPopulation(int p, Race race)
    {
        int Population = VillagePopulation.GetTotalPop();
        if (Population - p <= 0)
        {
            VillagePopulation.RemoveAllPop();
            Happiness = 100;
            TurnDestroyed = State.World.Turn;
        }
        else
        {
            VillagePopulation.RemoveRacePop(race, p);
        }



        VillagePopulation.CleanHirables();
    }

    //public void SubtractRandomPopulation(int pop)
    //{
    //    VillagePopulation.RemoveRandomPop(pop);
    //}

    //public void AddHireable(Unit unit)
    //{
    //    VillagePopulation.AddHireable(unit);
    //}

    //public void RemoveHireable(Unit unit)
    //{
    //    VillagePopulation.RemoveHireable(unit);
    //}

    public List<Unit> GetRecruitables()
    {
        return VillagePopulation.GetRecruitables();
    }







    public void DevouredPercentage(float pct)
    {
        Happiness *= 1 - pct;
    }

    public int GetTotalPop()
    {
        return VillagePopulation.GetTotalPop();
    }



    internal void BuyWeaponPotentiallyBulk(ItemType weapon, Empire buyingEmpire)
    {
        int purchaseAmount = 1;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            purchaseAmount = 4;
        if (Input.GetKey(KeyCode.LeftControl))
            purchaseAmount = 10;
        for (int i = 0; i < purchaseAmount; i++)
        {
            BuyWeapon(weapon, buyingEmpire);
        }
    }

    internal void SellWeaponPotentiallyBulk(ItemType weapon, Empire sellingEmpire)
    {
        int purchaseAmount = 1;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            purchaseAmount = 4;
        if (Input.GetKey(KeyCode.LeftControl))
            purchaseAmount = 10;
        for (int i = 0; i < purchaseAmount; i++)
        {
            SellWeapon(weapon, sellingEmpire);
        }
    }

    internal void BuyWeapon(ItemType weapon, Empire buyingEmpire = null)
    {
        if (buyingEmpire == null)
            buyingEmpire = Empire;
        if (buyingEmpire.Gold < State.World.ItemRepository.GetItem(weapon).Cost)
            return;
        buyingEmpire.SpendGold(State.World.ItemRepository.GetItem(weapon).Cost);
        State.World.Stats.SpentGoldOnArmyEquipment(State.World.ItemRepository.GetItem(weapon).Cost, buyingEmpire.Side);
        Weapons.Add(weapon);
    }

    internal void SellWeapon(ItemType weapon, Empire sellingEmpire = null)
    {
        if (sellingEmpire == null)
            sellingEmpire = Empire;
        if (Weapons.Contains(weapon))
        {
            sellingEmpire.AddGold(State.World.ItemRepository.GetItem(weapon).Cost / 2);
            Weapons.Remove(weapon);
        }

    }

    public int GetStartingXp()
    {
        var total = 0;
        if (NetBoosts != null) total += NetBoosts.StartingExpAdd;
        if (Empire != null)
        {
            Empire.RecalculateBoosts(State.World.Villages);
            total += Empire.StartingXP;
        }
        total += 20 * (int)AcademyResearch.GetValueFromEmpire(Empire, AcademyResearchType.StartingEXP);
        return total;
    }

    public List<Traits> GetTraitsToAdd()
    {
        if (NetBoosts == null || NetBoosts.AddTraits == null)
            return new List<Traits>();

        return NetBoosts.AddTraits;
    }

    internal List<Unit> PrepareAndReturnGarrison()
    {
        if (State.World.GetEmpireOfSide(Side) is MonsterEmpire && Race >= Race.Vagrants && Race < Race.Selicia)
        {
            return PrepareAndReturnMonsterGarrison();
        }
        var startingExp = GetStartingXp();
        foreach (Unit unit in VillagePopulation.GetRecruitables())
        {
            if (unit.Experience < startingExp)
            {
                unit.SetExp(startingExp);
            }
            if (unit.HasEnoughExpToLevelUp())
                StrategicUtilities.SpendLevelUps(unit);

            unit.AddTraits(GetTraitsToAdd());
        }
        List<Unit> ActiveGarrison = VillagePopulation.GetRecruitables().Where(s => s.HasWeapon || s.HasBook || s.HasTrait(Traits.Feral)).OrderByDescending(s => s.Level).Take(Math.Min(MaxGarrisonSize, VillagePopulation.GetTotalPop())).ToList();
        List<Unit> InactiveGarrison = VillagePopulation.GetRecruitables().Where(s => s.HasWeapon == false && s.HasBook == false && s.HasTrait(Traits.Feral) == false).OrderByDescending(s => s.Level).ToList();
        for (int i = 0; i < 48; i++)
        {
            if (ActiveGarrison.Count >= VillagePopulation.GetTotalPop())
                break;


            if (ActiveGarrison.Count() < MaxGarrisonSize)
            {
                Unit unit;
                if (InactiveGarrison.Count > 0)
                {
                    unit = InactiveGarrison[0];
                    if (unit.Items[0] != null)
                        Shop.SellItem(Empire, unit, 0);
                    InactiveGarrison.Remove(unit);
                }
                else
                {
                    Race nextRace = VillagePopulation.RandomRaceByWeight();
                    if (Weapons.Count == 0 && State.RaceSettings.GetRaceTraits(nextRace).Contains(Traits.Feral) == false && (nextRace >= Race.Vagrants && nextRace < Race.Selicia) == false)
                        continue;
                    bool found = false;
                    for (int j = 0; j < 15; j++)
                    {
                        if (VillagePopulation.GetRacePop(nextRace) <= ActiveGarrison.Where(s => s.Race == nextRace).Count())
                        {
                            nextRace = VillagePopulation.RandomRaceByWeight();
                            continue;
                        }
                        found = true;
                    }
                    if (found == false)
                        continue;

                    if (nextRace >= Race.Vagrants && nextRace < Race.Selicia)
                    {
                        CreateMonster(startingExp, ActiveGarrison);
                        continue;
                    }
                    else
                        unit = new Unit(Side, nextRace, startingExp, State.World.GetEmpireOfRace(nextRace)?.CanVore ?? true);
                }
                if (unit.HasEnoughExpToLevelUp())
                {
                    StrategicUtilities.SpendLevelUps(unit);
                }
                DefaultRaceData race = Races.GetRace(unit);
                if (unit.ClothingType != 0)
                {
                    if (unit.Race == Race.Lizards)
                    {
                        if (race.AllowedMainClothingTypes.Contains(RaceSpecificClothing.LizardPeasant))
                            unit.ClothingType = 1 + race.AllowedMainClothingTypes.IndexOf(RaceSpecificClothing.LizardPeasant);
                    }
                    else if (unit.Race == Race.Lamia)
                    {
                        if (race.AllowedMainClothingTypes.Contains(RaceSpecificClothing.Toga))
                            unit.ClothingType = 1 + race.AllowedMainClothingTypes.IndexOf(RaceSpecificClothing.Toga);
                    }
                    else
                    {
                        if (unit.HasBreasts)
                        {
                            if (race.AllowedMainClothingTypes.Contains(ClothingTypes.FemaleVillager))
                                unit.ClothingType = 1 + race.AllowedMainClothingTypes.IndexOf(ClothingTypes.FemaleVillager);
                        }
                        else
                        {
                            if (race.AllowedMainClothingTypes.Contains(ClothingTypes.MaleVillager))
                                unit.ClothingType = 1 + race.AllowedMainClothingTypes.IndexOf(ClothingTypes.MaleVillager);
                        }
                    }

                }
                ActiveGarrison.Add(unit);
                if (VillagePopulation.GetRecruitables().Contains(unit) == false)
                {
                    VillagePopulation.AddHireableFromCurrentPop(unit);

                }

                if (unit.BestSuitedForRanged())
                {
                    TryEquipWeapon(unit, ItemType.CompoundBow);
                    if (unit.Items[0] != null) continue;
                    TryEquipWeapon(unit, ItemType.Bow);
                    if (unit.Items[0] != null) continue;
                }
                TryEquipWeapon(unit, ItemType.Axe);
                if (unit.Items[0] != null) continue;
                TryEquipWeapon(unit, ItemType.Mace);
                if (unit.Items[0] != null) continue;
                TryEquipWeapon(unit, ItemType.CompoundBow);
                if (unit.Items[0] != null) continue;
                TryEquipWeapon(unit, ItemType.Bow);
            }
            else
                break;
        }
        return ActiveGarrison;
    }

    internal List<Unit> PrepareAndReturnMonsterGarrison()
    {
        int startingExp = 0;
        SpawnerInfo spawner = Config.SpawnerInfo(Empire.Race);
        if (spawner != null)
        {
            int highestExp = State.GameManager.StrategyMode.ScaledExp;
            int baseXp = (int)(highestExp * spawner.scalingFactor / 100);
            startingExp = baseXp;
        }

        foreach (Unit unit in VillagePopulation.GetRecruitables())
        {
            if (unit.Experience < startingExp)
            {
                unit.SetExp(startingExp);
            }
            if (unit.HasEnoughExpToLevelUp())
                StrategicUtilities.SpendLevelUps(unit);
        }
        List<Unit> ActiveGarrison = VillagePopulation.GetRecruitables().Where(s => s.HasWeapon || s.HasBook || s.HasTrait(Traits.Feral)).OrderByDescending(s => s.Level).Take(Math.Min(MaxGarrisonSize, VillagePopulation.GetTotalPop())).ToList();

        for (int i = 0; i < 48; i++)
        {
            if (ActiveGarrison.Count >= VillagePopulation.GetTotalPop())
                break;
            if (ActiveGarrison.Count() < MaxGarrisonSize)
            {
                CreateMonster(startingExp, ActiveGarrison);
            }
            else
                break;
        }
        return ActiveGarrison;
    }

    private void CreateMonster(int startingExp, List<Unit> ActiveGarrison)
    {
        Unit unit = new Unit(Side, Race, startingExp, State.World.GetEmpireOfRace(Race)?.CanVore ?? true);
        if (unit.HasEnoughExpToLevelUp())
        {
            StrategicUtilities.SpendLevelUps(unit);
        }
        ActiveGarrison.Add(unit);
        VillagePopulation.AddHireableFromCurrentPop(unit);
    }

    private void TryEquipWeapon(Unit unit, ItemType weap)
    {
        Item weapon = State.World.ItemRepository.GetItem(weap);
        if (Weapons.Contains(weap))
        {
            unit.Items[0] = weapon;
            Weapons.Remove(weap);
        }
    }

    /// <summary>
    /// Only for Player units, will result in random message boxes if used for AI
    /// </summary>
    internal Unit RecruitPlayerUnit(Empire empire, Army army)
    {
        if (VillagePopulation.GetTotalPop() <= VillagePopulation.GetRecruitables().Count())
        {
            State.GameManager.CreateMessageBox("You can't recruit, all of the villagers in this village already exist as units, use the hire menu instead");
        }
        else if (VillagePopulation.GetTotalPop() > 3)
        {
            if (empire.Gold >= Config.ArmyCost)
            {
                Race race = VillagePopulation.GetMostPopulousRace(); //There should only be one race if this is triggered, so this should be safe
                Unit unit = new Unit(empire.Side, race, GetStartingXp(), State.World.GetEmpireOfRace(race)?.CanVore ?? true);
                if (StrategicUtilities.ArmyCanFitUnit(army, unit))
                {
                    unit.AddTraits(GetTraitsToAdd());
                    army.Units.Add(unit);
                    State.World.Stats.SoldiersRecruited(1, Side);
                    empire.SpendGold(Config.ArmyCost);
                    VillagePopulation.RemoveRacePop(unit.Race, 1);
                    army.RecalculateSizeValue();
                    return unit;
                }
            }
        }
        return null;
    }

    internal Unit RecruitPlayerUnit(Empire empire, Army army, Race race)
    {
        if (VillagePopulation.GetTotalPop() <= VillagePopulation.GetRecruitables().Count())
        {
            State.GameManager.CreateMessageBox("You can't recruit, all of the villagers in this village already exist as units, use the hire menu instead");
        }
        else if (VillagePopulation.GetTotalPop() > 3 && VillagePopulation.GetRacePop(race) > 0)
        {
            if (empire.Gold >= Config.ArmyCost)
            {
                Unit unit = new Unit(empire.Side, race, GetStartingXp(), State.World.GetEmpireOfRace(Race)?.CanVore ?? true);
                if (StrategicUtilities.ArmyCanFitUnit(army, unit))
                {
                    unit.AddTraits(GetTraitsToAdd());
                    army.Units.Add(unit);
                    State.World.Stats.SoldiersRecruited(1, Side);
                    empire.SpendGold(Config.ArmyCost);
                    VillagePopulation.RemoveRacePop(unit.Race, 1);
                    army.RecalculateSizeValue();
                    return unit;
                }
            }
        }
        return null;
    }

    internal void SetPopulationToAtleastTwo()
    {
        if (VillagePopulation.GetTotalPop() < 2)
        {
            if (VillagePopulation.GetTotalPop() < 1)
            {
                VillagePopulation.AddRacePop(Race, 2);
            }
            else
            {
                VillagePopulation.AddRacePop(Race, 1);
            }
        }

    }

    /// <summary>
    /// The same as Recruit Player unit, only it automatically tries hiring from the infiltrators/adventurers/mercs/hireables first
    /// </summary>
    internal Unit RecruitAIUnit(Empire empire, Army army)
    {
        if (VillagePopulation.GetTotalPop() <= 3)
            return null;
        if (empire.Gold >= Config.ArmyCost)
        {
            if (army.RemainnigSize > 0)
            {
                if ((army.Units.Count + 1) > Config.ScoutMax && army.RemainingMP > Config.ArmyMP)
                {
                    army.RemainingMP = Config.ArmyMP;
                }
                if (VillagePopulation.GetRecruitables().Where(rec => rec.IsInfiltratingSide(Side)).Count() > 0 && army.Side == Side && State.Rand.Next(2) < 1)
                {
                    Unit unit = VillagePopulation.GetRecruitables().Where(rec => rec.IsInfiltratingSide(Side)).OrderByDescending(s => s.Experience).First();
                    if (StrategicUtilities.ArmyCanFitUnit(army, unit))
                    {
                        var startingExp = GetStartingXp();
                        if (unit.Experience < startingExp)
                            unit.SetExp(startingExp);
                        unit.AddTraits(GetTraitsToAdd());
                        army.Units.Add(unit);
                        unit.Side = army.Side;
                        empire.SpendGold(Config.ArmyCost);
                        VillagePopulation.RemoveHireable(unit);
                        army.RecalculateSizeValue();
                        return unit;
                    }
                }
                if (Adventurers?.Count > 0)
                {
                    MercenaryContainer merc = Adventurers.OrderByDescending(s => s.Unit.Experience).First();
                    if (empire.Gold > merc.Cost && StrategicUtilities.ArmyCanFitUnit(army, merc.Unit))
                    {
                        HireSpecialUnit(empire, army, merc);
                        return merc.Unit;
                    }
                }
                if (Mercenaries?.Count > 0 && empire.Gold > 600)
                {
                    MercenaryContainer merc = Mercenaries.OrderByDescending(s => s.Unit.Experience).First();
                    if (empire.Gold > merc.Cost && StrategicUtilities.ArmyCanFitUnit(army, merc.Unit))
                    {
                        HireSpecialUnit(empire, army, merc);
                        return merc.Unit;
                    }
                }
                if (VillagePopulation.GetRecruitables().Count > 0 && army.Side == Side)
                {
                    Unit unit = VillagePopulation.GetRecruitables().OrderByDescending(s => s.Experience).First();
                    if (StrategicUtilities.ArmyCanFitUnit(army, unit))
                    {
                        var startingExp = GetStartingXp();
                        if (unit.Experience < startingExp)
                            unit.SetExp(startingExp);
                        unit.AddTraits(GetTraitsToAdd());
                        army.Units.Add(unit);
                        unit.Side = army.Side;
                        empire.SpendGold(Config.ArmyCost);
                        VillagePopulation.RemoveHireable(unit);
                        army.RecalculateSizeValue();
                        return unit;
                    }
                }
                else
                {
                    var unitRace = VillagePopulation.RandomRaceByWeight();
                    Unit unit = new Unit(empire.Side, unitRace, empire.StartingXP, State.World.GetEmpireOfRace(unitRace)?.CanVore ?? true);
                    if (StrategicUtilities.ArmyCanFitUnit(army, unit))
                    {
                        unit.AddTraits(GetTraitsToAdd());
                        army.Units.Add(unit);
                        State.World.Stats.SoldiersRecruited(1, Side);
                        empire.SpendGold(Config.ArmyCost);
                        VillagePopulation.RemoveRacePop(unit.Race, 1);
                        return unit;
                    }
                }
            }
        }
        return null;
    }

    internal bool HireUnit(Empire empire, Army army, Unit unit)
    {
        if (VillagePopulation.GetTotalPop() > 3)
        {
            if (empire.Gold >= Config.ArmyCost)
            {
                if (StrategicUtilities.ArmyCanFitUnit(army, unit))
                {
                    var startingExp = GetStartingXp();
                    if (unit.Experience < startingExp)
                        unit.SetExp(startingExp);
                    unit.AddTraits(GetTraitsToAdd());
                    army.Units.Add(unit);
                    unit.Side = army.Side;
                    empire.SpendGold(10);
                    VillagePopulation.RemoveHireable(unit);
                    army.RecalculateSizeValue();
                    return true;
                }
            }
        }
        return false;
    }

    void UpdateMercenaries()
    {
        if (Mercenaries == null)
            Mercenaries = new List<MercenaryContainer>();
        if (Adventurers == null)
            Adventurers = new List<MercenaryContainer>();
        if (VillagePopulation.GetTotalPop() == 0)
        {
            Adventurers.Clear();
            Mercenaries.Clear();
        }
        if (State.World.Turn != TurnRefreshed)
        {
            TurnRefreshed = State.World.Turn;
            AvailableRaces = new List<Race>();
            foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).Where(s => (int)s >= 0))
            {
                if (race < Race.Selicia && Config.World.GetValue($"Merc {race}"))
                    AvailableRaces.Add(race);
            }
        }
        int highestExp = State.GameManager.StrategyMode.ScaledExp;
        if (Config.MercenariesDisabled == false)
        {
            if (AvailableRaces.Count > 0)
            {
                if (Mercenaries.Count > NetBoosts.MaxMercsAdd - NetBoosts.MercsPerTurnAdd)
                {
                    List<Unit> units = Mercenaries.ConvertAll(merc => merc.Unit);
                    Mercenaries.RemoveRange(0, Mercenaries.Count - (NetBoosts.MaxMercsAdd - NetBoosts.MercsPerTurnAdd));
                    foreach (Unit u in units)
                    {
                        if (!Mercenaries.Any(mer => mer.Unit == u) && u.OnDiscard != null)
                        {
                            u.OnDiscard();
                        }
                    }
                }
                for (int i = 0; i < NetBoosts.MercsPerTurnAdd; i++)
                {
                    Mercenaries.Add(CreateMercenary(highestExp));
                }
            }
        }
        else
            Mercenaries.Clear();


        if (Config.AdventurersDisabled == false)
        {
            if (Adventurers.Count > NetBoosts.MaxAdventurersAdd - NetBoosts.AdventurersPerTurnAdd)
            {
                List<Unit> units = Adventurers.ConvertAll(merc => merc.Unit);
                Adventurers.RemoveRange(0, Adventurers.Count - (NetBoosts.MaxAdventurersAdd - NetBoosts.AdventurersPerTurnAdd));
                foreach (Unit u in units)
                {
                    if (!Adventurers.Any(mer => mer.Unit == u) && u.OnDiscard != null)
                    {
                        u.OnDiscard();
                    }
                }
            }
            for (int i = 0; i < NetBoosts.AdventurersPerTurnAdd; i++)
            {
                Adventurers.Add(CreateAdventurer(highestExp));
            }

        }
        else
            Adventurers.Clear();



    }

    MercenaryContainer CreateAdventurer(int highestExp)
    {
        MercenaryContainer merc = new MercenaryContainer();
        Race race = Race.Cats;

        if (Side >= 700)
        {
            race = Race;
        }
        else if (State.Rand.Next(16) != 0)
        {
            if (State.Rand.Next(3) != 0 || State.World.AllActiveEmpires == null)
            {
                if (State.Rand.Next(3) != 0)
                    race = Race;
                else
                    race = State.World.GetEmpireOfSide(Side)?.CapitalCity?.OriginalRace ?? State.World.GetEmpireOfSide(Side)?.ReplacedRace ?? Race;
            }
            else
            {
                var emps = State.World.AllActiveEmpires.Where(s => s.IsAlly(Empire) && s.Race < Race.Selicia && s.Race != Race.Goblins).ToArray();

                if (emps.Length > 0)
                {
                    int random = State.Rand.Next(emps.Length);
                    race = emps[random].CapitalCity?.OriginalRace ?? emps[random].ReplacedRace;
                }

            }

        }
        else
        {
            var possibleRaces = AvailableRaces.Where((s) => s < Race.Vagrants).Concat(State.World.MainEmpires.Where(t => t.KnockedOut == false).Select((i) => i.CapitalCity?.OriginalRace ?? i.Race)).ToArray();
            race = possibleRaces[State.Rand.Next(possibleRaces.Count())];
        }

        bool canVore = true;
        if (State.World.GetEmpireOfRace(race)?.CanVore == false)
            canVore = false;
        int extraCost = 0;
        int exp = GetStartingXp() + (int)(highestExp * .3f) + State.Rand.Next(10);
        merc.Unit = new Unit((int)race, race, exp, canVore, UnitType.Adventurer, true);
        if (race < Race.Vagrants && merc.Unit.FixedGear == false)
        {
            if (merc.Unit.Items[0] == null)
            {
                if (merc.Unit.BestSuitedForRanged())
                    merc.Unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.CompoundBow);
                else
                    merc.Unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.Axe);
            }

            if (State.Rand.Next(10) == 0)
            {
                var book = State.World.ItemRepository.GetRandomBook();
                extraCost = book.Cost / 2;
                merc.Unit.SetItem(book, 1);
                merc.Unit.AIClass = merc.Unit.BestSuitedForRanged() ? AIClass.MagicRanged : AIClass.MagicMelee;
            }
        }

        var power = State.RaceSettings.Get(merc.Unit.Race).PowerAdjustment;
        if (power == 0)
        {
            power = RaceParameters.GetTraitData(merc.Unit).PowerAdjustment;
        }
        StrategicUtilities.SetAIClass(merc.Unit);
        StrategicUtilities.SpendLevelUps(merc.Unit);
        merc.Cost = (int)((25 + extraCost + State.Rand.Next(15) + (.04 * exp)) * UnityEngine.Random.Range(0.8f, 1.2f) * power);
        merc.Title = $"{InfoPanel.RaceSingular(merc.Unit)} - Adventurer";
        return merc;
    }

    MercenaryContainer CreateMercenary(int highestExp)
    {
        MercenaryContainer merc = new MercenaryContainer();
        Race race;
        race = AvailableRaces[State.Rand.Next(AvailableRaces.Count())];

        int extraCost = 0;
        int exp = (int)(highestExp * .8f) + State.Rand.Next(10);
        merc.Unit = new Unit((int)race, race, exp, true, UnitType.Mercenary, true);
        if (race < Race.Vagrants && merc.Unit.FixedGear == false)
        {
            if (merc.Unit.Items[0] == null)
            {
                if (merc.Unit.BestSuitedForRanged())
                    merc.Unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.CompoundBow);
                else
                    merc.Unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.Axe);
            }

            int r = State.Rand.Next(4);
            switch (r)
            {
                case 0:
                    merc.Unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Shoes), 1);
                    break;

                case 1:
                    merc.Unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Helmet), 1);
                    break;

                case 2:
                    merc.Unit.SetItem(State.World.ItemRepository.GetItem(ItemType.BodyArmor), 1);
                    break;

                case 3:
                    if (merc.Unit.BestSuitedForRanged())
                        merc.Unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Gloves), 1);
                    else
                        merc.Unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Gauntlet), 1);
                    break;

            }
            if (State.Rand.Next(10) == 0)
            {
                var book = State.World.ItemRepository.GetRandomBook();
                extraCost = book.Cost * 3 / 4;

                merc.Unit.SetItem(book, 1);
                merc.Unit.AIClass = merc.Unit.BestSuitedForRanged() ? AIClass.MagicRanged : AIClass.MagicMelee;
            }
        }

        var power = State.RaceSettings.Get(merc.Unit.Race).PowerAdjustment;
        if (power == 0)
        {
            power = RaceParameters.GetTraitData(merc.Unit).PowerAdjustment;
        }
        StrategicUtilities.SetAIClass(merc.Unit);
        StrategicUtilities.SpendLevelUps(merc.Unit);
        merc.Cost = (int)((25 + extraCost + State.Rand.Next(15) + (.12 * exp)) * UnityEngine.Random.Range(0.8f, 1.2f) * power);
        merc.Title = $"{InfoPanel.RaceSingular(merc.Unit)} - Mercenary";
        return merc;
    }

    internal bool HireSpecialUnit(Empire empire, Army army, MercenaryContainer merc)
    {
        int cost = merc.Cost - (int)Math.Round(merc.Cost * (0.1f * AcademyResearch.GetValueFromEmpire(empire, AcademyResearchType.MercRecruitCost)));
        if (empire.Gold >= cost)
        {
            if (StrategicUtilities.ArmyCanFitUnit(army,merc.Unit))
            {
                var startingExp = GetStartingXp();
                if (merc.Unit.Experience < startingExp)
                    merc.Unit.SetExp(startingExp);
                merc.Unit.AddTraits(GetTraitsToAdd());
                army.Units.Add(merc.Unit);
                merc.Unit.Side = army.Side;
                empire.SpendGold(cost);
                Adventurers.Remove(merc);
                Mercenaries.Remove(merc);
                army.RecalculateSizeValue();
                return true;
            }
        }
        return false;
    }

}
