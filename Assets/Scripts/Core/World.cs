using MapObjects;
using OdinSerializer;
using System.Collections.Generic;
using System.Linq;


public class World
{
    internal const int MonsterCount = 37;
    [OdinSerialize]
    public int Turn = 1;
    [OdinSerialize]
    public string SaveVersion;
    [OdinSerialize]
    public List<Empire> EmpireOrder;
    public StrategicTileType[,] Tiles;
    public StrategicDoodadType[,] Doodads;
    public Village[] Villages;

    public List<Empire> MainEmpires;
    /// <summary>
    /// Deprecated, only left in for compatibility
    /// </summary>
    public Empire[] Empires;
    public Empire ActingEmpire;
    public ItemRepository ItemRepository;
    public WorldConfig ConfigStorage;
    public StrategicStats Stats;
    public TacticalData TacticalData;

    [OdinSerialize]
    internal Dictionary<int, Dictionary<int, Relationship>> Relations;



    [OdinSerialize]
    public bool crazyBuildings = false;

    [OdinSerialize]
    internal SavedCameraState SavedCameraState;

    public MonsterEmpire[] MonsterEmpires;

    public MercenaryHouse[] MercenaryHouses;
    [OdinSerialize]
    internal ClaimableBuilding[] Claimables;

    public List<Empire> AllActiveEmpires;

    [OdinSerialize]
    public List<Reincarnator> Reincarnators;

    [OdinSerialize]
    public bool IsNight = false;
	
    public World(bool MapEditorVersion)
    {

        Config.World.VillagesPerEmpire = new int[Config.NumberOfRaces];
        Config.CenteredEmpire = new bool[Config.NumberOfRaces];
        State.World = this;
        ConfigStorage = Config.World;
        ItemRepository = new ItemRepository();
        if (MapEditorVersion)
        {
            MainEmpires = new List<Empire>();
            Villages = new Village[0];
            for (int i = 0; i < Config.NumberOfRaces; i++)
            {
                int bannerType = State.RaceSettings.Exists((Race)i) ? State.RaceSettings.Get((Race)i).BannerType : 1;
                MainEmpires.Add(new Empire(new Empire.ConstructionArgs(i, CreateStrategicGame.ColorFromIndex(i), UnityEngine.Color.white, bannerType, StrategyAIType.None, TacticalAIType.None, 0, 16, 16)));
            }
            WorldGenerator worldGen = new WorldGenerator();
            worldGen.GenerateOnlyTerrain(ref Tiles);
            Doodads = new StrategicDoodadType[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
        }
        AllActiveEmpires = MainEmpires;
        MercenaryHouses = new MercenaryHouse[0];
        Claimables = new ClaimableBuilding[0];
    }

    internal World(StrategicCreationArgs args, Map map)
    {
        State.World = this;
        StrategyPathfinder.Initialized = false;
        ConfigStorage = Config.World;

        if (map == null)
        {
            WorldGenerator worldGen = new WorldGenerator();
            int empireCount = Config.VillagesPerEmpire.Where(s => s > 0).Count();
            worldGen.GenerateWorld(ref Tiles, ref Villages, args.Team, args.MapGen);
            Claimables = new ClaimableBuilding[0];
            worldGen.PlaceMercenaryHouses(args.MercCamps);
            worldGen.PlaceGoldMines(args.GoldMines);
            Doodads = new StrategicDoodadType[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
            WorldGenerator.ClearVillagePaths(args.MapGen);
        }
        else
        {
            Tiles = map.Tiles;
            Doodads = map.Doodads;
            MapVillagePopulator pop = new MapVillagePopulator(Tiles);
            pop.PopulateVillages(map, ref Villages);
            pop.PopulateMercenaryHouses(map, ref MercenaryHouses);
            pop.PopulateClaimables(map, ref Claimables);
        }


        MainEmpires = new List<Empire>();
        for (int i = 0; i < Config.NumberOfRaces; i++)
        {
            MainEmpires.Add(new Empire(args.empireArgs[i]));
        }
        for (int i = 0; i < MainEmpires.Count; i++)
        {
            MainEmpires[i].CalcIncome(Villages);
            MainEmpires[i].CanVore = args.CanVore[i];
            MainEmpires[i].TurnOrder = args.TurnOrder[i];
        }
        MainEmpires.Add(new Empire(new Empire.ConstructionArgs(700, UnityEngine.Color.red, new UnityEngine.Color(.6f, 0, 0), 5, StrategyAIType.Basic, TacticalAIType.Full, 700, 16, 16)));
        MainEmpires.Last().Name = "Rebels";
        MainEmpires.Last().ReplacedRace = Race.Tigers;
        MainEmpires.Add(new Empire(new Empire.ConstructionArgs(701, UnityEngine.Color.red, new UnityEngine.Color(.6f, 0, 0), 5, StrategyAIType.Basic, TacticalAIType.Full, 701, 16, 16)));
        MainEmpires.Last().Name = "Bandits";
        /*         MainEmpires.Add(new Empire(new Empire.ConstructionArgs(702, UnityEngine.Color.red, new UnityEngine.Color(.6f, 0, 0), 5, StrategyAIType.Basic, TacticalAIType.Full, 702, 16, 16)));
                MainEmpires.Last().Name = "Outcasts";
                MainEmpires.Last().ReplacedRace = Race.Tigers; */
        UpdateBanditLimits();
        crazyBuildings = args.crazyBuildings;
        VillageBuildingList.SetBuildings(crazyBuildings);


        ItemRepository = new ItemRepository();
        Stats = new StrategicStats();
        InitializeMonsters();

        //Added to get Bandits and Rebels to generate correctly
        RefreshEmpires();
        RefreshTurnOrder();

        State.GameManager.StrategyMode.Setup();
        State.GameManager.StrategyMode.RedrawTiles();
        State.GameManager.StrategyMode.RedrawVillages();
        RelationsManager.ResetRelations();
        State.GameManager.StrategyMode.BeginTurn();
        State.GameManager.StrategyMode.RebuildSpawners();
        MercenaryHouse.UpdateStaticStock();
        RenameBunnyTownsAsPreyTowns();


    }

    internal void UpdateBanditLimits()
    {
        int minGarrison = 48;
        int minArmySize = 48;
        foreach (var empire in MainEmpires)
        {


            if (empire.Side < 100)
            {
                if (empire.KnockedOut)
                    continue;
                if (empire.MaxGarrisonSize < minGarrison)
                    minGarrison = empire.MaxGarrisonSize;
                if (empire.MaxArmySize < minArmySize)
                    minArmySize = empire.MaxArmySize;
            }
            else
            {
                empire.MaxArmySize = minArmySize;
                empire.MaxGarrisonSize = minGarrison;
            }



        }
    }

    internal void RefreshMonstersKeepingArmies()
    {
        var monsterArmies = StrategicUtilities.GetAllArmies();
        var oldMons = MonsterEmpires;
        InitializeMonsters();
        if (oldMons != null && oldMons.Length > 0)
        {
            int i = 0;
            foreach (var empire in MonsterEmpires)
            {
                foreach (var army in monsterArmies)
                {
                    if (army.Side == empire.Side)
                    {
                        empire.Armies.Add(army);
                        army.SetEmpire(empire);
                    }
                }
                if (oldMons.Length < i) //Not sure how this would trigger, but this conditional is intended to stop an exception that's happening.  
                {
                    empire.ReplacedRace = oldMons[i].ReplacedRace;
                    i++;
                }

            }
        }

    }

    internal void InitializeMonsters()
    {
        MonsterEmpires = new MonsterEmpire[MonsterCount];
        MonsterEmpires[0] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Vagrants, UnityEngine.Color.white, UnityEngine.Color.white, 9, StrategyAIType.Monster, TacticalAIType.Full, 996, 32, 0));
        MonsterEmpires[1] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Serpents, UnityEngine.Color.white, UnityEngine.Color.white, 22, StrategyAIType.Monster, TacticalAIType.Full, 997, 32, 0));
        MonsterEmpires[2] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Wyvern, UnityEngine.Color.white, UnityEngine.Color.white, 23, StrategyAIType.Monster, TacticalAIType.Full, 998, 32, 0));
        MonsterEmpires[3] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Compy, UnityEngine.Color.white, UnityEngine.Color.white, 25, StrategyAIType.Monster, TacticalAIType.Full, 999, 32, 0));
        MonsterEmpires[4] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.FeralSharks, UnityEngine.Color.white, UnityEngine.Color.white, 26, StrategyAIType.Monster, TacticalAIType.Full, 1000, 32, 0));
        MonsterEmpires[5] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.FeralWolves, UnityEngine.Color.white, UnityEngine.Color.white, 27, StrategyAIType.Monster, TacticalAIType.Full, 1001, 32, 0));
        MonsterEmpires[6] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Cake, UnityEngine.Color.white, UnityEngine.Color.white, 28, StrategyAIType.Monster, TacticalAIType.Full, 1002, 32, 0));
        MonsterEmpires[7] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Goblins, UnityEngine.Color.white, UnityEngine.Color.white, 30, StrategyAIType.Goblin, TacticalAIType.Full, -200, 32, 0));
        MonsterEmpires[8] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Harvesters, UnityEngine.Color.white, UnityEngine.Color.white, 31, StrategyAIType.Monster, TacticalAIType.Full, 1003, 32, 0));
        MonsterEmpires[9] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Voilin, UnityEngine.Color.white, UnityEngine.Color.white, 32, StrategyAIType.Monster, TacticalAIType.Full, 1004, 32, 0));
        MonsterEmpires[10] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.FeralBats, UnityEngine.Color.white, UnityEngine.Color.white, 33, StrategyAIType.Monster, TacticalAIType.Full, 1005, 32, 0));
        MonsterEmpires[11] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.FeralFrogs, UnityEngine.Color.white, UnityEngine.Color.white, 34, StrategyAIType.Monster, TacticalAIType.Full, 1006, 32, 0));
        MonsterEmpires[12] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Dragon, UnityEngine.Color.white, UnityEngine.Color.white, 35, StrategyAIType.Monster, TacticalAIType.Full, 1007, 32, 0));
        MonsterEmpires[13] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Dragonfly, UnityEngine.Color.white, UnityEngine.Color.white, 36, StrategyAIType.Monster, TacticalAIType.Full, 1008, 32, 0));
        MonsterEmpires[14] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.TwistedVines, UnityEngine.Color.white, UnityEngine.Color.white, 41, StrategyAIType.Monster, TacticalAIType.Full, 1009, 32, 0));
        MonsterEmpires[15] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Fairies, UnityEngine.Color.white, UnityEngine.Color.white, 42, StrategyAIType.Monster, TacticalAIType.Full, 1010, 32, 0));
        MonsterEmpires[16] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.FeralAnts, UnityEngine.Color.white, UnityEngine.Color.white, 43, StrategyAIType.Monster, TacticalAIType.Full, 1011, 32, 0));
        MonsterEmpires[17] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Gryphons, UnityEngine.Color.white, UnityEngine.Color.white, 44, StrategyAIType.Monster, TacticalAIType.Full, 1012, 32, 0));
        MonsterEmpires[18] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.RockSlugs, UnityEngine.Color.white, UnityEngine.Color.white, 45, StrategyAIType.Monster, TacticalAIType.Full, 1013, 32, 0));
        MonsterEmpires[19] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Salamanders, UnityEngine.Color.white, UnityEngine.Color.white, 46, StrategyAIType.Monster, TacticalAIType.Full, 1014, 32, 0));
        MonsterEmpires[20] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Mantis, UnityEngine.Color.white, UnityEngine.Color.white, 47, StrategyAIType.Monster, TacticalAIType.Full, 1015, 32, 0));
        MonsterEmpires[21] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.EasternDragon, UnityEngine.Color.white, UnityEngine.Color.white, 48, StrategyAIType.Monster, TacticalAIType.Full, 1016, 32, 0));
        MonsterEmpires[22] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Catfish, UnityEngine.Color.white, UnityEngine.Color.white, 49, StrategyAIType.Monster, TacticalAIType.Full, 1017, 32, 0));
        MonsterEmpires[23] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Gazelle, UnityEngine.Color.white, UnityEngine.Color.white, 50, StrategyAIType.Monster, TacticalAIType.Full, 1018, 32, 0));
        MonsterEmpires[24] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Earthworms, UnityEngine.Color.white, UnityEngine.Color.white, 51, StrategyAIType.Monster, TacticalAIType.Full, 1019, 32, 0));
        MonsterEmpires[25] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.FeralLizards, UnityEngine.Color.white, UnityEngine.Color.white, 52, StrategyAIType.Monster, TacticalAIType.Full, 1020, 32, 0));
        MonsterEmpires[26] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Monitors, UnityEngine.Color.white, UnityEngine.Color.white, 53, StrategyAIType.Monster, TacticalAIType.Full, 1021, 32, 0));
        MonsterEmpires[27] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Schiwardez, UnityEngine.Color.white, UnityEngine.Color.white, 54, StrategyAIType.Monster, TacticalAIType.Full, 1022, 32, 0));
        MonsterEmpires[28] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Terrorbird, UnityEngine.Color.white, UnityEngine.Color.white, 55, StrategyAIType.Monster, TacticalAIType.Full, 1023, 32, 0));
        MonsterEmpires[29] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Dratopyr, UnityEngine.Color.white, UnityEngine.Color.white, 56, StrategyAIType.Monster, TacticalAIType.Full, 1024, 32, 0));
        MonsterEmpires[30] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.FeralLions, UnityEngine.Color.white, UnityEngine.Color.white, 57, StrategyAIType.Monster, TacticalAIType.Full, 1337, 32, 0));
        MonsterEmpires[31] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Goodra, UnityEngine.Color.white, UnityEngine.Color.white, 58, StrategyAIType.Monster, TacticalAIType.Full, 1025, 32, 0));
        MonsterEmpires[32] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.FeralHorses, UnityEngine.Color.white, UnityEngine.Color.white, 59, StrategyAIType.Monster, TacticalAIType.Full, 1026, 32, 0));
		MonsterEmpires[33] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.FeralFox, UnityEngine.Color.white, UnityEngine.Color.white, 60, StrategyAIType.Monster, TacticalAIType.Full, 1027, 32, 0));
        MonsterEmpires[34] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.Terminid, UnityEngine.Color.white, UnityEngine.Color.white, 61, StrategyAIType.Monster, TacticalAIType.Full, 1028, 32, 0));
        MonsterEmpires[35] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.FeralOrcas, UnityEngine.Color.white, UnityEngine.Color.white, 62, StrategyAIType.Monster, TacticalAIType.Full, 1029, 32, 0));
        MonsterEmpires[36] = new MonsterEmpire(new Empire.ConstructionArgs((int)Race.BoomBunnies, UnityEngine.Color.white, UnityEngine.Color.white, 64, StrategyAIType.Monster, TacticalAIType.Full, 1030, 32, 0));
        foreach (var emp in MonsterEmpires)
        {
            SpawnerInfo spawner = Config.SpawnerInfo(emp.Race);
            if (spawner == null)
                continue;
            emp.Team = spawner.Team;
        }
        List<Empire> allEmps = MainEmpires.ToList();
        allEmps.AddRange(MonsterEmpires);
        AllActiveEmpires = allEmps;
    }

    internal void RefreshEmpires()
    {
        if (MonsterEmpires == null)
        {
            InitializeMonsters();
            return;
        }
        List<Empire> allEmps = MainEmpires.ToList();
        allEmps.AddRange(MonsterEmpires);
        AllActiveEmpires = allEmps;
    }

    internal void PopulateMonsterTurnOrders()
    {
        foreach (var empire in MonsterEmpires)
        {
            SpawnerInfo spawner = Config.SpawnerInfo(empire.Race);
            if (spawner == null)
                continue;
            empire.TurnOrder = spawner.TurnOrder;
            empire.Team = spawner.Team;
        }

    }

    internal void RefreshTurnOrder()
    {
        EmpireOrder = AllActiveEmpires.OrderBy(s => s.TurnOrder).ThenBy(s => s.Race).ToList();
    }

    internal Empire GetEmpireOfRace(Race race)
    {
        if (AllActiveEmpires == null)
            return null;
        for (int i = 0; i < AllActiveEmpires.Count; i++)
        {
            if (AllActiveEmpires[i].Race == race)
                return AllActiveEmpires[i];
        }
        for (int i = 0; i < AllActiveEmpires.Count; i++)
        {
            if (AllActiveEmpires[i].ReplacedRace == race)
                return AllActiveEmpires[i];
        }
        return null;
    }

    internal Empire GetEmpireOfSide(int side)
    {
        if (AllActiveEmpires == null)
            return null;
        for (int i = 0; i < AllActiveEmpires.Count; i++)
        {
            if (AllActiveEmpires[i].Side == side)
                return AllActiveEmpires[i];
        }
        return null;
    }

    private void RenameBunnyTownsAsPreyTowns()
    {
        int nameIndex = 1;

        foreach (Village village in Villages.Where(s => s.Race == Race.Bunnies && s.Empire.CanVore == false))
        {
            if (village.Capital)
                village.Name = State.NameGen.GetAlternateTownName(Race.Bunnies, 0);
            else
            {
                village.Name = State.NameGen.GetAlternateTownName(Race.Bunnies, nameIndex);
                nameIndex++;
            }
        }
    }
}
