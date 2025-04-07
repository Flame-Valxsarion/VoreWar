using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class StrategyMode : SceneBase
{
    public Translator Translator;

    private bool _armyBarUp;

    private bool ArmyBarUp
    {
        get { return _armyBarUp; }
        set
        {
            _armyBarUp = value;
            ArmyStatusUI.gameObject.SetActive(value);
        }
    }

    bool subWindowUp = false;

    Empire ActingEmpire
    {
        get { return State.World.ActingEmpire; }
        set { State.World.ActingEmpire = value; }
    }

    Empire RenamingEmpire;

    Army SelectedArmy
    {
        get
        {
            return _selectedArmy;
        }

        set
        {
            _selectedArmy = value;
            if (State.World != null)
            {
                arrowManager?.ClearNodes();
                mouseMovementMode = false;
                if (State.World.MainEmpires != null)
                    UpdateArmyLocationsAndSprites();
                ArmyBarUp = value != null;
                if (ArmyBarUp)
                    RegenArmyBar(_selectedArmy);
            }
        }
    }

    Army _selectedArmy;
    float m_timer;

    private int _scaledExp;

    public int ScaledExp
    {
        get
        {
            if (_scaledExp == 0) _scaledExp = StrategicUtilities.Get80thExperiencePercentile();
            return _scaledExp;
        }
        set { _scaledExp = value; }
    }

    internal bool IsPlayerTurn => ActingEmpire.StrategicAI == null;

    internal bool OnlyAIPlayers { get; private set; }

    Empire _lastHumanTeam = null;
    public Empire LastHumanEmpire
    {
        get { if (ActingEmpire?.StrategicAI == null) _lastHumanTeam = ActingEmpire; return _lastHumanTeam; }
        set => _lastHumanTeam = value;
    }

    internal List<PathNode> QueuedPath
    {
        get => queuedPath; set
        {
            if (value == null)
                queuedAttackPermission = false;
            queuedPath = value;
        }
    }

    public bool NewReports { get; internal set; }

    List<GameObject> ShownIFF;

    bool runningQueued = true;

    bool buildingRangeOn = false;

    bool mouseMovementMode = false;
    PathNodeManager arrowManager;
    List<PathNode> queuedPath;
    bool queuedAttackPermission;
    Vec2i currentPathDestination;

    public StrategicTileDictionary TileDictionary;

    internal FogSystem FogSystem;

    List<GameObject> currentVillageTiles;
    List<GameObject> currentClaimableTiles;
    List<GameObject> currentBuildingTiles;

    public Tilemap[] TilemapLayers;
    public Tilemap FogOfWar;
    public TileBase FogTile;
    public TileBase[] BuildIndicator;
    internal Tile[] TileTypes;
    public TileBase[] DoodadTypes;
    public Sprite[] Sprites;
    public Sprite[] Buildings;
    public Sprite[] VillageSprites;
    public Sprite[] WallMultiSprites;
    public GameObject[] SpriteCategories;

    public Sprite[] Banners;

    public Transform VillageFolder;
    public Transform ArmyFolder;
    public Transform WallRoadFolder;

    public ArmyStatusPanel ArmyStatusUI;
    public StatusBarPanel StatusBarUI;
    public DevourPanel DevourUI;
    public TurnReportPanel ReportUI;
    public TrainPanel TrainUI;
    public BuildMenu BuildMenu;
    public UpgradeMenu UpgradeMenu;
    public AncientTeleporterMenu AncientTeleportMenu;
    public BuildingArmyPopup BuildingArmyPopup;
    public GameObject EnemyTurnText;
    public GameObject PausedText;

    public EventScreen EventUI;

    public float NightChance = Config.BaseNightChance;

    public GameObject NotificationWindow;
    public TMPro.TextMeshProUGUI NotificationText;
    float remainingNotificationTime;

    public GameObject ExchangeBlockerPanels;

    public ArmyExchanger ExchangerUI;

    public SimpleTextPanel ArmyTooltip;
    public SimpleTextPanel VillageTooltip;

    public Button UndoButton;

    internal List<MonsterSpawnerLocation> Spawners;

    internal List<StrategicMoveUndo> UndoMoves = new List<StrategicMoveUndo>();

    int[] trainCost;
    int[] trainExp;

    bool pickingExchangeLocation = false;

    internal bool Paused;

    internal bool BuildMode;
    internal ConstructibleType SelectedConstruction;

    public Sprite[] TileSprites;

    public DevourSelectPanel RaceUI;

    private void Start()
    {
        Translator = new Translator();
        arrowManager = FindObjectOfType<PathNodeManager>();

        ShownIFF = new List<GameObject>();

        TileTypes = new Tile[TileSprites.Count()];
        for (int i = 0; i < TileSprites.Count(); i++)
        {
            TileTypes[i] = ScriptableObject.CreateInstance<Tile>();
            TileTypes[i].sprite = TileSprites[i];
        }

        State.EventList.SetUI(EventUI);
    }

    public void Setup()
    {
        ActingEmpire = null;
        OnlyAIPlayers = true;
        State.GameManager.CenterCameraOnTile((int)(Config.StrategicWorldSizeX * .5f), (int)(Config.StrategicWorldSizeY * .5f));
        State.GameManager.CameraController.SetZoom(Config.TacticalSizeX * .5f);
        foreach (var village in State.World.Villages)
        {
            village.UpdateNetBoosts();
        }
        foreach (Empire emp in State.World.MainEmpires)
        {

            if (State.World.Villages.Where(s => s.Side == emp.Side).Any() == false)
            {
                emp.KnockedOut = true;
                continue;
            }
            if (emp.StrategicAI == null)
            {
                OnlyAIPlayers = false;
            }
            emp.Regenerate();
            emp.constructionResources.SetResources(10, 10, 10, 10, 10, 10);
        }
        State.World.PopulateMonsterTurnOrders();
        State.World.RefreshTurnOrder();
        for (int i = 0; i < State.World.EmpireOrder.Count; i++)
        {
            if (State.World.EmpireOrder[i].KnockedOut == false || (State.World.EmpireOrder[i].Side == 701 && State.World.EmpireOrder[i].Armies.Any()))
            {
                ActingEmpire = State.World.EmpireOrder[i];
                break;
            }
        }
        GenericSetup();
        StatusBarUI.RecreateWorld.gameObject.SetActive(true);

    }

    /// <summary>
    /// Designed to be used through NotificationSystem, but can be used manually
    /// </summary>
    /// <param name="message"></param>
    /// <param name="time"></param>
    internal void ShowNotification(string message, float time)
    {
        if (Config.Notifications == false)
            return;
        NotificationWindow.SetActive(true);
        remainingNotificationTime = time;
        NotificationText.text = message;
    }

    public void GenericSetup()
    {
        LastHumanEmpire = State.World.MainEmpires.Where(s => s.StrategicAI == null).FirstOrDefault();
        RedrawTiles();
        RebuildSpawners();
        ResetButtons();
    }


    public void ClearData()
    {
        StrategyPathfinder.Initialized = false;
        if (State.World != null)
            ActingEmpire = null;
        _selectedArmy = null;
        subWindowUp = false;
        arrowManager?.ClearNodes();
        mouseMovementMode = false;
        ClearArmies();
        ClearVillages();
        foreach (Tilemap tilemap in TilemapLayers)
        {
            tilemap.ClearAllTiles();
        }
    }

    public void ClearGraphics()
    {
        ClearArmies();
        ClearVillages();
        foreach (Tilemap tilemap in TilemapLayers)
        {
            tilemap.ClearAllTiles();
        }
    }

    void RecreateObjects()
    {
        RedrawTiles();
        RedrawVillages();
        RedrawArmies();
    }

    public void CheckIfOnlyAIPlayers()
    {
        OnlyAIPlayers = true;
        foreach (Empire emp in State.World.MainEmpires)
        {
            if (emp.StrategicAI == null)
            {
                OnlyAIPlayers = false;
            }
        }
    }

    void PromptArmyPick()
    {
        ExchangeBlockerPanels.SetActive(true);
        pickingExchangeLocation = true;
    }


    void OpenExchangerPanel(Army left, Vec2i location)
    {
        if (left == null)
            return;
        if (left.Position.GetNumberOfMovesDistance(location) != 1)
            return;
        if (left.RemainingMP < 1)
        {
            State.GameManager.CreateMessageBox("Army needs to have at least 1 MP to exchange units");
            return;
        }
        Village village = StrategicUtilities.GetVillageAt(location);
        if (village != null && village.Empire.IsEnemy(left.Empire))
        {
            if (!left.Units.All(u => u.HasTrait(Traits.Infiltrator)))
            {
                State.GameManager.CreateMessageBox("Can't split armies onto an enemy village");
                return;
            }
        }
        Army right = StrategicUtilities.ArmyAt(location);
        if (right == null)
        {
            if (ActingEmpire.Armies.Count() >= Config.MaxArmies)
            {
                State.GameManager.CreateMessageBox("You already have the maximum amount of armies");
                return;
            }

            right = new Army(ActingEmpire, new Vec2i(location.x, location.y), left.Side)
            {
                RemainingMP = left.RemainingMP - 1
            };
            State.World.GetEmpireOfSide(left.Side).Armies.Add(right);
        }
        else
        {
            if (right.Empire.IsAlly(left.Empire) == false)
            {
                State.GameManager.CreateMessageBox("You can't exchange units with a hostile army");
                return;
            }

            if (right.RemainingMP < 1 && right.Side == left.Side)
            {
                State.GameManager.CreateMessageBox("Recieving Army needs to have at least 1 MP to exchange units (0 MP for Allied armies)");
                return;
            }
        }

        subWindowUp = true;
        ExchangeBlockerPanels.SetActive(false);
        pickingExchangeLocation = false;
        ExchangerUI.gameObject.SetActive(true);
        ExchangerUI.Initialize(left, right);
    }


    void ResetButtons()
    {
        StatusBarUI.EndTurn.interactable = ActingEmpire?.StrategicAI == null;
        StatusBarUI.EmpireStatus.interactable = ActingEmpire?.StrategicAI == null || OnlyAIPlayers;
        StatusBarUI.ShowTurnReport.gameObject.SetActive(ActingEmpire?.StrategicAI == null && ActingEmpire.Reports.Count > 0);
        StatusBarUI.RecreateWorld.gameObject.SetActive(false);
        StatusBarUI.Build.gameObject.SetActive(Config.BuildConfig.BuildingSystemEnabled);
        EnemyTurnText.SetActive(ActingEmpire?.StrategicAI != null);
    }

    public void RedrawArmies()
    {
        ClearArmies();
        var armiesToReassign = new List<Army>();
        foreach (Empire empire in State.World.AllActiveEmpires)
        {
            foreach (Army army in empire.Armies)
            {
                if (army.Units.Any() && !army.Units.Any(unit => unit.GetApparentSide() == army.Side))
                {
                    armiesToReassign.Add(army);
                }
                if (army.Side < 32)
                {
                    if (army.BannerStyle > (int)BannerTypes.VoreWar && CustomBannerTest.Sprites[army.BannerStyle - 23] != null)
                    {
                        army.Sprite = Instantiate(SpriteCategories[1], new Vector3(army.Position.x, army.Position.y), new Quaternion(), ArmyFolder).GetComponent<SpriteRenderer>();
                        army.Sprite.sprite = CustomBannerTest.Sprites[army.BannerStyle - 23];
                    }
                    else
                    {
                        army.Banner = Instantiate(SpriteCategories[3], new Vector3(army.Position.x, army.Position.y), new Quaternion(), ArmyFolder).GetComponent<MultiStageBanner>();
                        army.Banner.Refresh(army, army == SelectedArmy);
                    }

                }
                else
                {

                    int tileType = empire.BannerType;
                    if (army.Units.Contains(empire.Leader)) tileType += 4;
                    if (SelectedArmy == army) tileType += 1;
                    army.Sprite = Instantiate(SpriteCategories[1], new Vector3(army.Position.x, army.Position.y), new Quaternion(), ArmyFolder).GetComponent<SpriteRenderer>();
                    army.Sprite.sprite = Sprites[tileType];
                    army.Sprite.color = empire.UnityColor;
                }

            }
        }
        foreach (Army army in armiesToReassign)
        {
            ReassignArmyEmpire(army);
        }
        UpdateFog();
    }

    private void ClearArmies()
    {
        var previousTiles = GameObject.FindGameObjectsWithTag("Army");
        foreach (var tile in previousTiles.ToList())
        {
            Destroy(tile);
        }
    }

    internal void RebuildSpawners()
    {
        Spawners = new List<MonsterSpawnerLocation>();
        StrategicDoodadType[,] doodads = State.World.Doodads;
        if (doodads != null)
        {
            for (int i = 0; i <= doodads.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= doodads.GetUpperBound(1); j++)
                {

                    if (doodads[i, j] >= StrategicDoodadType.SpawnerVagrant)

                    {
                        StrategicDoodadType doodad = doodads[i, j];
                        switch (doodad)
                        {
                            case StrategicDoodadType.SpawnerVagrant:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Vagrants));
                                break;
                            case StrategicDoodadType.SpawnerSerpents:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Serpents));
                                break;
                            case StrategicDoodadType.SpawnerWyvern:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Wyvern));
                                break;
                            case StrategicDoodadType.SpawnerCompy:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Compy));
                                break;
                            case StrategicDoodadType.SpawnerSharks:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.FeralSharks));
                                break;
                            case StrategicDoodadType.SpawnerFeralWolves:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.FeralWolves));
                                break;
                            case StrategicDoodadType.SpawnerCake:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Cake));
                                break;
                            case StrategicDoodadType.SpawnerHarvester:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Harvesters));
                                break;
                            case StrategicDoodadType.SpawnerVoilin:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Voilin));
                                break;
                            case StrategicDoodadType.SpawnerBats:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.FeralBats));
                                break;
                            case StrategicDoodadType.SpawnerFrogs:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.FeralFrogs));
                                break;
                            case StrategicDoodadType.SpawnerDragon:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Dragon));
                                break;
                            case StrategicDoodadType.SpawnerDragonfly:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Dragonfly));
                                break;
                            case StrategicDoodadType.SpawnerTwistedVines:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.TwistedVines));
                                break;
                            case StrategicDoodadType.SpawnerFairy:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Fairies));
                                break;
                            case StrategicDoodadType.SpawnerAnts:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.FeralAnts));
                                break;
                            case StrategicDoodadType.SpawnerGryphon:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Gryphons));
                                break;
                            case StrategicDoodadType.SpawnerSlugs:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.RockSlugs));
                                break;
                            case StrategicDoodadType.SpawnerSalamanders:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Salamanders));
                                break;
                            case StrategicDoodadType.SpawnerMantis:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Mantis));
                                break;
                            case StrategicDoodadType.SpawnerEasternDragon:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.EasternDragon));
                                break;
                            case StrategicDoodadType.SpawnerCatfish:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Catfish));
                                break;
                            case StrategicDoodadType.SpawnerGazelle:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Gazelle));
                                break;
                            case StrategicDoodadType.SpawnerEarthworm:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Earthworms));
                                break;
                            case StrategicDoodadType.SpawnerFeralLizards:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.FeralLizards));
                                break;
                            case StrategicDoodadType.SpawnerMonitor:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Monitors));
                                break;
                            case StrategicDoodadType.SpawnerSchiwardez:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Schiwardez));
                                break;
                            case StrategicDoodadType.SpawnerTerrorbird:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Terrorbird));
                                break;
                            case StrategicDoodadType.SpawnerDratopyr:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Dratopyr));
                                break;
                            case StrategicDoodadType.SpawnerFeralLions:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.FeralLions));
                                break;
                            case StrategicDoodadType.SpawnerGoodra:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Goodra));
                                break;
                            case StrategicDoodadType.SpawnerFeralHorses:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.FeralHorses));
                                break;
                            case StrategicDoodadType.SpawnerFeralFox:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.FeralFox));
                                break;
                            case StrategicDoodadType.SpawnerTerminid:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.Terminid));
                                break;
                            case StrategicDoodadType.SpawnerFeralOrcas:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.FeralOrcas));
                                break;
                            case StrategicDoodadType.SpawnerBoomBunnies:
                                Spawners.Add(new MonsterSpawnerLocation(new Vec2i(i, j), Race.BoomBunnies));
                                break;
                        }
                    }
                }
            }
        }
    }


    public void UpdateArmyLocationsAndSprites()
    {
        var armiesToReassign = new List<Army>();
        foreach (Empire empire in State.World.MainEmpires)
        {
            foreach (Army army in empire.Armies)
            {
                if (army.Banner == null && army.Sprite == null)
                {
                    RedrawArmies();
                    return;
                }
                if (army.Banner != null)
                    army.Banner.Refresh(army, army == SelectedArmy);
            }
        }
        UpdateFog();
    }

    internal void ReassignArmyEmpire(Army army)
    {
        var sidesRepresented = new Dictionary<int, int>();
        army.Units.ForEach(unit =>
        {
            if (sidesRepresented.ContainsKey(unit.FixedSide))
            {
                sidesRepresented[unit.FixedSide]++;
            }
            else
            {
                sidesRepresented.Add(unit.FixedSide, 1);
            }
        });

        var finalSide = sidesRepresented.OrderByDescending(s => s.Value).First();
        var emp = State.World.GetEmpireOfSide(finalSide.Key);
        var pos = army.Position;
        Vec2 loc = pos;
        if (StrategicUtilities.GetVillageAt(pos) != null)
        {
            var distance = 1;
            loc = new Vec2(0, 0);
            CheckTile(pos + new Vec2(-distance, 0));
            CheckTile(pos + new Vec2(0, distance));
            CheckTile(pos + new Vec2(distance, 0));
            CheckTile(pos + new Vec2(-distance, -distance));
            CheckTile(pos + new Vec2(-distance, distance));
            CheckTile(pos + new Vec2(0, -distance));
            CheckTile(pos + new Vec2(distance, -distance));
            CheckTile(pos + new Vec2(distance, distance));

            if (loc.x == 0 && loc.y == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    loc.x = State.Rand.Next(Config.StrategicWorldSizeX);
                    loc.y = State.Rand.Next(Config.StrategicWorldSizeY);

                    if (StrategicUtilities.IsTileClear(loc))
                        break;

                }
                Debug.Log("Could not place army");
                return;
            }
            void CheckTile(Vec2 spot)
            {
                if (StrategicUtilities.IsTileClear(spot) == false)
                    return;
                if (loc.x == 0 && loc.y == 0)
                    loc = spot;
                else if (State.Rand.Next(4) == 0)
                    loc = spot;
            }
        }
        pos = new Vec2i(loc.x, loc.y);
        if (emp != null)
        {
            var newArmy = new Army(emp, pos, emp.Side);
            newArmy.Units = army.Units;
            emp.Armies.Add(newArmy);
            newArmy.Units.ForEach(u => u.Side = newArmy.Side);
        }
        else // we'll literally make up an empire on the spot. Should rarely happen
        {
            var monsterEmp = State.World.MonsterEmpires.Where(e => e.Race == army.Units.Where(u => u.FixedSide == finalSide.Key).FirstOrDefault()?.Race).FirstOrDefault();
            if (monsterEmp != null)
            {
                Empire brandNewEmp = new MonsterEmpire(new Empire.ConstructionArgs(finalSide.Key, UnityEngine.Color.white, UnityEngine.Color.white, monsterEmp.BannerType, StrategyAIType.Monster, TacticalAIType.Full, 2000 + finalSide.Key, monsterEmp.MaxArmySize, 0));
                brandNewEmp.ReplacedRace = monsterEmp.Race;
                brandNewEmp.TurnOrder = 1234;
                brandNewEmp.Name = "Unbound " + monsterEmp.Name;
                var newArmy = new Army(brandNewEmp, pos, brandNewEmp.Side);
                newArmy.Units = army.Units;
                brandNewEmp.Armies.Add(newArmy);
                State.World.AllActiveEmpires.Add(brandNewEmp);
                State.World.RefreshTurnOrder();
                Config.World.SpawnerInfo[(Race)finalSide.Key] = new SpawnerInfo(true, 1, 0, 0.4f, brandNewEmp.Team, 0, false, 9999, 1, monsterEmp.MaxArmySize, brandNewEmp.TurnOrder, false);
            }
            else
            {
                Empire brandNewEmp = new Empire(new Empire.ConstructionArgs(finalSide.Key, UnityEngine.Random.ColorHSV(), UnityEngine.Random.ColorHSV(), 5, StrategyAIType.Advanced, TacticalAIType.Full, 2000 + finalSide.Key, State.World.MainEmpires[0].MaxArmySize, 0));
                brandNewEmp.ReplacedRace = army.Units.Where(u => u.FixedSide == finalSide.Key).First().Race;
                brandNewEmp.TurnOrder = 1432;
                brandNewEmp.Name = "The Free";
                var newArmy = new Army(brandNewEmp, pos, brandNewEmp.Side);
                newArmy.Units = army.Units;
                brandNewEmp.Armies.Add(newArmy);
                State.World.AllActiveEmpires.Add(brandNewEmp);
                State.World.MainEmpires.Add(brandNewEmp);
                State.World.RefreshTurnOrder();
            }
        }
        army.Empire.Armies.Remove(army);
        RedrawArmies();
    }

    void UpdateFog()
    {
        if (Config.FogOfWar == false && State.World.IsNight == false)
        {
            if (FogOfWar.gameObject.activeSelf)
            {
                FogOfWar.ClearAllTiles();
                FogOfWar.gameObject.SetActive(false);
            }
            UpdateVisibility();
            return;
        }

        FogOfWar.gameObject.SetActive(true);

        if (FogSystem == null)
            FogSystem = new FogSystem(FogOfWar, FogTile);
        if (OnlyAIPlayers)
        {
            Config.World.Toggles["FogOfWar"] = false;
            return;
        }
        FogSystem.UpdateFog(LastHumanEmpire, State.World.Villages, StrategicUtilities.GetAllArmies(), currentVillageTiles, currentClaimableTiles);
    }

    void UpdateVisibility()
    {
        if (OnlyAIPlayers) return;
        if (LastHumanEmpire == null) return; // Sometimes when loading, the above may not be enough
        foreach (Army army in StrategicUtilities.GetAllHostileArmies(LastHumanEmpire))
        {
            var spr = army.Banner?.GetComponent<MultiStageBanner>();
            if (spr != null)
                spr.gameObject.SetActive(army.Side == LastHumanEmpire.Side || !army.Units.All(u => u.HasTrait(Traits.Infiltrator)) || (StrategicUtilities.GetAllHumanSides().Count() > 1 ? army.Units.Any(u => u.FixedSide == ActingEmpire.Side) : army.Units.Any(u => u.FixedSide == LastHumanEmpire.Side)));
            var spr2 = army.Sprite;
            if (spr2 != null) spr2.enabled = army.Side == LastHumanEmpire.Side || !army.Units.All(u => u.HasTrait(Traits.Infiltrator)) || (StrategicUtilities.GetAllHumanSides().Count() > 1 ? army.Units.Any(u => u.FixedSide == ActingEmpire.Side) : army.Units.Any(u => u.FixedSide == LastHumanEmpire.Side));
        }
    }

    public void RedrawTiles()
    {
        foreach (Tilemap tilemap in TilemapLayers)
        {
            tilemap.ClearAllTiles();
        }
        StrategicTileLogic logic = new StrategicTileLogic();
        StrategicTileType[,] tiles = logic.ApplyLogic(State.World.Tiles, out var overTiles, out var underTiles);
        for (int i = 0; i <= tiles.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= tiles.GetUpperBound(1); j++)
            {
                //if (overTiles[i, j] >= (StrategicTileType)2300)
                //{
                //    TilemapLayers[2].SetTile(new Vector3Int(i, j, 0), TileDictionary.DeepWaterOverWater[(int)overTiles[i, j] - 2300]);
                //}
                //Debug.Log(underTiles[i, j] + ", " + i + ", " + j);
                int current_layer = 0;
                int liquid_layer = 0;
                if (tiles[i, j] >= (StrategicTileType)2100)
                {
                    current_layer = ApplyFloat(i, j, current_layer);
                    if (underTiles[i, j] != (StrategicTileType)99)
                    {
                        TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)underTiles[i, j]]);
                    }
                    else
                    {
                        TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[StrategicTileInfo.GetTileType(State.World.Tiles[i, j], i, j)]);
                        switch (State.World.Tiles[i, j])
                        {
                            case StrategicTileType.field:
                                TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)StrategicTileType.grass]);
                                break;
                            case StrategicTileType.fieldDesert:
                                TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)StrategicTileType.desert]);
                                break;
                            case StrategicTileType.fieldSnow:
                                TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)StrategicTileType.snow]);
                                break;
                            default:
                                TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[StrategicTileInfo.GetTileType(State.World.Tiles[i, j], i, j)]);
                                break;

                        }

                    }

                    //TilemapLayers[1].SetTile(new Vector3Int(i, j, 0), TileDictionary.IceOverSnow[(int)tiles[i, j] - 2100]);
                }                
                //else if (tiles[i, j] >= (StrategicTileType)2000)
                //{
                //    TilemapLayers[2].SetTile(new Vector3Int(i, j, 0), TileDictionary.WaterFloat[(int)tiles[i, j] - 2000]);
                //    //TilemapLayers[1].SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassFloat[(int)tiles[i, j] - 2000]);
                //    TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[(int)underTiles[i,j]]);
                //}
                else
                {
                    //TilemapLayers[1].SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassFloat[(int)tiles[i, j] - 2100]);
                    TilemapLayers[0].SetTile(new Vector3Int(i, j, 0), TileTypes[StrategicTileInfo.GetTileType(tiles[i, j], i, j)]);
                }
                if (overTiles[i, j] >= (StrategicTileType)2000)
                {
                    current_layer++;
                    liquid_layer = current_layer;
                    switch (State.World.Tiles[i, j])
                    {
                        case StrategicTileType.water:
                            TilemapLayers[current_layer].SetTile(new Vector3Int(i, j, 0), TileDictionary.WaterFloat[(int)overTiles[i, j] - 2000]);
                            break;
                        case StrategicTileType.ocean:
                            TilemapLayers[current_layer].SetTile(new Vector3Int(i, j, 0), TileDictionary.OceanFloat[(int)overTiles[i, j] - 2000]);
                            break;
                        case StrategicTileType.lava:
                            TilemapLayers[current_layer].SetTile(new Vector3Int(i, j, 0), TileDictionary.LavaFloat[(int)overTiles[i, j] - 2000]);
                            break;
                        case StrategicTileType.ice:
                            TilemapLayers[current_layer].SetTile(new Vector3Int(i, j, 0), TileDictionary.IceOverSnow[(int)overTiles[i, j] - 2000]);
                            break;
                        case StrategicTileType.shallowWater:
                            TilemapLayers[current_layer].SetTile(new Vector3Int(i, j, 0), TileDictionary.ShallowWaterFloat[(int)overTiles[i, j] - 2000]);
                            break;
                        case StrategicTileType.smallIslands:
                            TilemapLayers[current_layer].SetTile(new Vector3Int(i, j, 0), TileDictionary.SmallIslandsFloat[(int)overTiles[i, j] - 2000]);
                            break;
                        default:
                            TilemapLayers[current_layer].SetTile(new Vector3Int(i, j, 0), TileDictionary.LavaFloat[(int)overTiles[i, j] - 2000]);
                            break;
                    }
                    foreach (KeyValuePair<int, StrategicTileType> tiletype in logic.GetSurroundingLiquid((int)overTiles[i, j] - 2000, State.World.Tiles[i, j], new Vec2(i, j)))
                    {
                        current_layer++;
                        switch (tiletype.Value)
                        {
                            case StrategicTileType.water:
                                if (StrategicTileType.water > State.World.Tiles[i, j])
                                    TilemapLayers[current_layer].SetTile(new Vector3Int(i, j, 0), TileDictionary.WaterLiquidFloat[tiletype.Key]);
                                break;
                            case StrategicTileType.ocean:
                                if (StrategicTileType.ocean > State.World.Tiles[i, j])
                                    TilemapLayers[current_layer].SetTile(new Vector3Int(i, j, 0), TileDictionary.OceanLiquidFloat[tiletype.Key]);
                                break;
                            case StrategicTileType.lava:
                                if (StrategicTileType.lava > State.World.Tiles[i, j])
                                    TilemapLayers[current_layer].SetTile(new Vector3Int(i, j, 0), TileDictionary.LavaLiquidFloat[tiletype.Key]);
                                break;
                            case StrategicTileType.ice:
                                if (StrategicTileType.ice > State.World.Tiles[i, j])
                                    TilemapLayers[current_layer].SetTile(new Vector3Int(i, j, 0), TileDictionary.IceLiquidFloat[tiletype.Key]);
                                break;
                            case StrategicTileType.shallowWater:
                                if (StrategicTileType.shallowWater > State.World.Tiles[i, j])
                                    TilemapLayers[current_layer].SetTile(new Vector3Int(i, j, 0), TileDictionary.ShallowWaterLiquidFloat[tiletype.Key]);
                                break;
                            case StrategicTileType.smallIslands:
                                if (StrategicTileType.shallowWater > State.World.Tiles[i, j])
                                    TilemapLayers[current_layer].SetTile(new Vector3Int(i, j, 0), TileDictionary.ShallowWaterLiquidFloat[tiletype.Key]);
                                break;
                            default:
                                break;
                        }
                    }

                }
                else if (overTiles[i, j] != 0)
                {
                    TilemapLayers[9].SetTile(new Vector3Int(i, j, 0), TileTypes[StrategicTileInfo.GetTileType(overTiles[i, j], i, j)]);
                }
                else
                {
                    var type = StrategicTileInfo.GetObjectTileType(State.World.Tiles[i, j], i, j);
                    if (type != -1)
                        TilemapLayers[9].SetTile(new Vector3Int(i, j, 0), TileDictionary.Objects[type]);

                }
            }
        }
        StrategicDoodadType[,] doodads = State.World.Doodads;
        if (doodads != null)
        {

            for (int i = 0; i <= doodads.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= doodads.GetUpperBound(1); j++)
                {
                    if (doodads[i, j] > 0 && doodads[i, j] < StrategicDoodadType.SpawnerVagrant)
                    {
                        if (doodads[i, j] == StrategicDoodadType.wall)
                        {
                            bool north = j + 1 <= tiles.GetUpperBound(1) ? doodads[i, j + 1] == StrategicDoodadType.wall : false;
                            bool east = i + 1 <= tiles.GetUpperBound(0) ? doodads[i + 1, j] == StrategicDoodadType.wall : false;
                            bool south = j - 1 >= 0 ? doodads[i, j - 1] == StrategicDoodadType.wall : false;
                            bool west = i - 1 >= 0 ? doodads[i - 1, j] == StrategicDoodadType.wall : false;
                            int spr = 0;

                            if (north && east && south && west)
                                spr = 7;
                            else if (north && east && south)
                                spr = 13;
                            else if (north && east && west)
                                spr = 14;
                            else if (north && south && west)
                                spr = 12;
                            else if (east && south && west)
                                spr = 15;
                            else if (north && east)
                                spr = 11;
                            else if (north && south)
                                spr = 4;
                            else if (north && west)
                                spr = 10;
                            else if (east && south)
                                spr = 9;
                            else if (south && west)
                                spr = 8;
                            else if (east && west)
                                spr = 1;
                            else if (north)
                                spr = 6;
                            else if (east)
                                spr = 2;
                            else if (south)
                                spr = 5;
                            else if (west)
                                spr = 3;

                            GameObject wall = Instantiate(SpriteCategories[4], new Vector3(i, j, 0), new Quaternion(), WallRoadFolder);
                            wall.GetComponent<SpriteRenderer>().sprite = WallMultiSprites[spr];
                            wall.GetComponent<SpriteRenderer>().sortingOrder = 1;
                        }
                        else
                        {
                            TilemapLayers[3].SetTile(new Vector3Int(i, j, 0), DoodadTypes[-1 + (int)doodads[i, j]]);
                        }
                    }
                }
            }
        }

        int ApplyFloat(int x, int y, int curr_layer)
        {
            int counter = curr_layer;
            StrategicTileType type = State.World.Tiles[x,y];
            bool liquid_tile = StrategicTileInfo.ConsideredLiquid.Contains(State.World.Tiles[x, y]);
            foreach (KeyValuePair<int, StrategicTileType> tiletype in logic.DetermineOverlay(x, y))
            {
                counter++;
                switch (tiletype.Value)
                {
                    case (StrategicTileType.grass):
                        if (type <= StrategicTileType.grass || liquid_tile)
                            TilemapLayers[counter].SetTile(new Vector3Int(x, y, 0), TileDictionary.GrassFloat[tiletype.Key]);
                        break;
                    case (StrategicTileType.desert):
                        if (type <= StrategicTileType.desert || liquid_tile)
                            TilemapLayers[counter].SetTile(new Vector3Int(x, y, 0), TileDictionary.DesertFloat[tiletype.Key]);
                        break;
                    case (StrategicTileType.snow):
                        if (type <= StrategicTileType.snow || liquid_tile)
                            TilemapLayers[counter].SetTile(new Vector3Int(x, y, 0), TileDictionary.SnowFloat[tiletype.Key]);
                        break;
                    case (StrategicTileType.ashen):
                        if (type <= StrategicTileType.ashen || liquid_tile)
                            TilemapLayers[counter].SetTile(new Vector3Int(x, y, 0), TileDictionary.AshenFloat[tiletype.Key]);
                        break;
                    case (StrategicTileType.volcanic):
                        if (type <= StrategicTileType.volcanic || liquid_tile)
                            TilemapLayers[counter].SetTile(new Vector3Int(x, y, 0), TileDictionary.VolcanicFloat[tiletype.Key]);
                        break;
                    case (StrategicTileType.swamp):
                        if (type <= StrategicTileType.swamp || liquid_tile)
                            TilemapLayers[counter].SetTile(new Vector3Int(x, y, 0), TileDictionary.SwampFloat[tiletype.Key]);
                        break;
                    case (StrategicTileType.drySwamp):
                        if (type <= StrategicTileType.drySwamp || liquid_tile)
                            TilemapLayers[counter].SetTile(new Vector3Int(x, y, 0), TileDictionary.DrySwampFloat[tiletype.Key]);
                        break;
                    case (StrategicTileType.purpleSwamp):
                        if (type <= StrategicTileType.purpleSwamp || liquid_tile)
                            TilemapLayers[counter].SetTile(new Vector3Int(x, y, 0), TileDictionary.PurpleBogFloat[tiletype.Key]);
                        break;
                    case (StrategicTileType.savannah):
                        if (type <= StrategicTileType.savannah || liquid_tile)
                            TilemapLayers[counter].SetTile(new Vector3Int(x, y, 0), TileDictionary.SavannahFloat[tiletype.Key]);
                        break;
                    case (StrategicTileType.smallIslands):
                        if (type <= StrategicTileType.smallIslands || liquid_tile)
                            TilemapLayers[counter].SetTile(new Vector3Int(x, y, 0), TileDictionary.SmallIslandsFloat[tiletype.Key]);
                        break;
                    case (StrategicTileType.rainforest):
                        if (type <= StrategicTileType.rainforest || liquid_tile)
                            TilemapLayers[counter].SetTile(new Vector3Int(x, y, 0), TileDictionary.RainforestFloat[tiletype.Key]);
                        break;
                    case (StrategicTileType.water):
                        break;
                    case (StrategicTileType.ocean):
                        break;
                    case (StrategicTileType.shallowWater):
                        /*
                        if (liquid_tile && overTiles[x, y] == (StrategicTileType)2009)
                        {
                            TilemapLayers[9 - Math.Min(8, counter)].SetTile(new Vector3Int(x, y, 0), TileDictionary.SmallIslandsFloat[tiletype.Key]);
                        }
                        */
                        break;
                    case (StrategicTileType.ice):
                        break;
                    case (StrategicTileType.lava):
                        break;
                    default:
                        TilemapLayers[counter].SetTile(new Vector3Int(x, y, 0), TileDictionary.GrassFloat[tiletype.Key]);
                        break;
                }
            }
            return counter;
        }
    }

    public void RedrawVillages()
    {
        if (State.World == null)
            return;
        ClearVillages();
        Village[] villages = State.World.Villages;
        currentVillageTiles = new List<GameObject>();
        currentClaimableTiles = new List<GameObject>();
        currentBuildingTiles = new List<GameObject>();
        int highestVillageSprite = VillageSprites.Count() - 1;
        for (int i = 0; i < villages.Length; i++)
        {
            if (villages[i] == null)
                continue;
            GameObject vill = Instantiate(SpriteCategories[2], new Vector3(villages[i].Position.x, villages[i].Position.y), new Quaternion(), VillageFolder);
            vill.GetComponent<SpriteRenderer>().sprite = VillageSprites[villages[i].GetImageNum(highestVillageSprite)];
            vill.GetComponent<SpriteRenderer>().sortingOrder = 1;
            int villageColorSprite = villages[i].GetColoredImageNum(highestVillageSprite);
            GameObject villColored = Instantiate(SpriteCategories[2], new Vector3(villages[i].Position.x, villages[i].Position.y), new Quaternion(), VillageFolder);
            villColored.GetComponent<SpriteRenderer>().sprite = VillageSprites[villageColorSprite];
            villColored.GetComponent<SpriteRenderer>().color = State.World.GetEmpireOfSide(villages[i].Side).UnityColor;
            if (villageColorSprite == 0)
                villColored.GetComponent<SpriteRenderer>().color = Color.clear;
            GameObject villShield = Instantiate(SpriteCategories[2], new Vector3(villages[i].Position.x, villages[i].Position.y), new Quaternion(), VillageFolder);
            villShield.GetComponent<SpriteRenderer>().sprite = Sprites[11];
            villShield.GetComponent<SpriteRenderer>().sortingOrder = 2;
            villShield.GetComponent<SpriteRenderer>().color = State.World.GetEmpireOfSide(villages[i].Side).UnitySecondaryColor;

            GameObject villShieldInner = Instantiate(SpriteCategories[2], new Vector3(villages[i].Position.x, villages[i].Position.y), new Quaternion(), VillageFolder);
            villShieldInner.GetComponent<SpriteRenderer>().sprite = Sprites[10];
            villShieldInner.GetComponent<SpriteRenderer>().sortingOrder = 2;
            villShieldInner.GetComponent<SpriteRenderer>().color = State.World.GetEmpireOfSide(villages[i].Side).UnityColor;
            currentVillageTiles.Add(vill);
            currentVillageTiles.Add(villColored);
            currentVillageTiles.Add(villShield);
            currentVillageTiles.Add(villShieldInner);
        }
        foreach (var mercHouse in State.World.MercenaryHouses)
        {
            GameObject merc = Instantiate(SpriteCategories[2], new Vector3(mercHouse.Position.x, mercHouse.Position.y), new Quaternion(), VillageFolder);
            merc.GetComponent<SpriteRenderer>().sprite = Sprites[14];
            merc.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        foreach (var teleporter in State.World.AncientTeleporters)
        {
            GameObject tele = Instantiate(SpriteCategories[2], new Vector3(teleporter.Position.x, teleporter.Position.y), new Quaternion(), VillageFolder);
            tele.GetComponent<SpriteRenderer>().sprite = Buildings[96];
            tele.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        foreach (var claimable in State.World.Claimables)
        {
            int spr = 0;
            if (claimable is GoldMine)
                spr = 12;
            GameObject vill = Instantiate(SpriteCategories[2], new Vector3(claimable.Position.x, claimable.Position.y), new Quaternion(), VillageFolder);
            vill.GetComponent<SpriteRenderer>().sprite = Sprites[spr];
            vill.GetComponent<SpriteRenderer>().sortingOrder = 1;
            GameObject villColored = Instantiate(SpriteCategories[2], new Vector3(claimable.Position.x, claimable.Position.y), new Quaternion(), VillageFolder);
            villColored.GetComponent<SpriteRenderer>().sprite = Sprites[spr + 1];
            villColored.GetComponent<SpriteRenderer>().color = claimable.Owner?.UnityColor ?? Color.clear;
            GameObject villShield = Instantiate(SpriteCategories[2], new Vector3(claimable.Position.x, claimable.Position.y), new Quaternion(), VillageFolder);
            villShield.GetComponent<SpriteRenderer>().sprite = Sprites[11];
            villShield.GetComponent<SpriteRenderer>().sortingOrder = 2;
            villShield.GetComponent<SpriteRenderer>().color = claimable.Owner?.UnityColor ?? Color.clear;
            GameObject villShieldInner = Instantiate(SpriteCategories[2], new Vector3(claimable.Position.x, claimable.Position.y), new Quaternion(), VillageFolder);
            villShieldInner.GetComponent<SpriteRenderer>().sprite = Sprites[10];
            villShieldInner.GetComponent<SpriteRenderer>().sortingOrder = 2;
            villShieldInner.GetComponent<SpriteRenderer>().color = claimable.Owner?.UnityColor ?? Color.clear;
            currentClaimableTiles.Add(vill);
            currentClaimableTiles.Add(villColored);
            currentClaimableTiles.Add(villShield);
            currentClaimableTiles.Add(villShieldInner);
        }
        foreach(var constructable in State.World.Constructibles)
        {
            int spr = constructable.spriteID;
            spr = Math.Min(constructable.upgradeStage, 3) * 2 + spr;           
            GameObject vill = Instantiate(SpriteCategories[2], new Vector3(constructable.Position.x, constructable.Position.y), new Quaternion(), VillageFolder);
            vill.GetComponent<SpriteRenderer>().sprite = Buildings[spr];
            vill.GetComponent<SpriteRenderer>().sortingOrder = 1;
            GameObject villColored = Instantiate(SpriteCategories[2], new Vector3(constructable.Position.x, constructable.Position.y), new Quaternion(), VillageFolder);
            villColored.GetComponent<SpriteRenderer>().sprite = Buildings[spr + 1];
            villColored.GetComponent<SpriteRenderer>().color = constructable.Owner?.UnityColor ?? Color.clear;
            currentBuildingTiles.Add(vill);
            currentBuildingTiles.Add(villColored);
            if (constructable.ruined)
            {
                GameObject disabled = Instantiate(SpriteCategories[2], new Vector3(constructable.Position.x, constructable.Position.y), new Quaternion(), VillageFolder);
                disabled.GetComponent<SpriteRenderer>().sprite = Buildings[98];
                disabled.GetComponent<SpriteRenderer>().sortingOrder = 2;
            } 
            else if (!constructable.active)
            {
                GameObject hammer = Instantiate(SpriteCategories[2], new Vector3(constructable.Position.x, constructable.Position.y), new Quaternion(), VillageFolder);
                hammer.GetComponent<SpriteRenderer>().sprite = Buildings[97];
                hammer.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
        }


        if (Config.FogOfWar || State.World.IsNight)
            UpdateFog();
        else
            FogOfWar.ClearAllTiles();

    }

    private void ClearVillages()
    {
        currentVillageTiles = null;
        var previousTiles = GameObject.FindGameObjectsWithTag("Village");
        foreach (var tile in previousTiles.ToList())
        {
            Destroy(tile);
        }
    }

    public void Regenerate(bool initialLoad = false)
    {
        if (LastHumanEmpire == null)
        {
            LastHumanEmpire = State.World.MainEmpires.Where(s => s.StrategicAI == null).FirstOrDefault();
        }
        if (!initialLoad)
        {
            foreach (Empire empire in State.World.AllActiveEmpires)
            {
                empire.ArmyCleanup();
            }
        }
        StatusBarUI.Build.onClick.AddListenerOnce(() => BuildMenu.Open(ActingEmpire));
        ActingEmpire.CalcIncome(State.World.Villages);
        if (ActingEmpire.StrategicAI == null || OnlyAIPlayers || Config.CheatExtraStrategicInfo || State.World.MainEmpires.Where(s => s.IsAlly(ActingEmpire) && s.StrategicAI == null).Any())
        {
            StatusBarUI.Side.text = ActingEmpire.Name;
            StatusBarUI.Gold.text = "Gold:" + ActingEmpire.Gold;
            StatusBarUI.Income.text = "Income:" + ActingEmpire.Income;
        }
        else
        {
            StatusBarUI.Side.text = ActingEmpire.Name;
            StatusBarUI.Gold.text = "Gold: hidden";
            StatusBarUI.Income.text = "Income: hidden";
        }

        if (SelectedArmy == null || SelectedArmy.Units.Count == 0)
            ArmyBarUp = false;
        if (SelectedArmy != null)
            SelectedArmy.RefreshMovementMode();
        RegenArmyBar(SelectedArmy);
        RedrawArmies();
        RedrawVillages();

        if (NewReports)
        {
            ShowTurnReport();
            NewReports = false;
        }
        if (ActingEmpire.StrategicAI == null)
            ActingEmpire.CheckAutoLevel();
    }

    void RegenArmyBar(Army army)
    {
        if (army == null || SelectedArmy.Units.Any() == false)
            return;
        if (ArmyBarUp == false)
        {
            ArmyBarUp = true;
        }
        ArmyStatusUI.Soldiers.text = army.Units.Count + " soldiers";
        ArmyStatusUI.Health.text = army.GetHealthPercentage() + "% health";
        ArmyStatusUI.MP.text = army.RemainingMP + " MP";
        if (StrategicUtilities.GetMercenaryHouseAt(SelectedArmy.Position) != null)
            ArmyStatusUI.ArmyStatus.text = "Mercenaries";
        else
            ArmyStatusUI.ArmyStatus.text = "Army Info";
        if (army.InVillageIndex > -1 && army.RemainingMP > 0)
        {
            var village = StrategicUtilities.GetVillageAt(SelectedArmy.Position);
            var setActive = (village != null && village.NetBoosts.MaximumTrainingLevelAdd > 0);

            ArmyStatusUI.Train.gameObject.SetActive(setActive);
            ArmyStatusUI.Devour.gameObject.SetActive(army.Units.Where(s => s.Predator).Any());
        }
        else
        {
            ArmyStatusUI.Train.gameObject.SetActive(false);
            ArmyStatusUI.Devour.gameObject.SetActive(false);
        }
    }

    public void AttemptUndo()
    {
        if (UndoMoves.Any())
        {
            UndoMoves[UndoMoves.Count - 1].Undo();
            UndoMoves.RemoveAt(UndoMoves.Count - 1);
        }
    }

    void NextArmy()
    {
        int startingIndex = ActingEmpire.Armies.IndexOf(SelectedArmy);
        int currentIndex = startingIndex + 1;
        bool found = false;
        for (int i = 0; i < ActingEmpire.Armies.Count; i++)
        {
            if (currentIndex >= ActingEmpire.Armies.Count)
                currentIndex -= ActingEmpire.Armies.Count;
            if (ActingEmpire.Armies[currentIndex].RemainingMP > 0)
            {
                found = true;
                SelectedArmy = ActingEmpire.Armies[currentIndex];
                State.GameManager.CenterCameraOnTile(SelectedArmy.Position.x, SelectedArmy.Position.y);
                break;
            }
            currentIndex++;
        }
        if (found == false)
            SelectedArmy = null;
    }

    void CheckForMovementInput()
    {
        if (SelectedArmy.RemainingMP > 0)
        {
            if (Input.GetButtonDown("Move Southwest"))
            {
                Move(SelectedArmy, 5);
            }
            if (Input.GetButtonDown("Move South"))
            {
                Move(SelectedArmy, 4);
            }
            if (Input.GetButtonDown("Move Southeast"))
            {
                Move(SelectedArmy, 3);
            }
            if (Input.GetButtonDown("Move East"))
            {
                Move(SelectedArmy, 2);
            }
            if (Input.GetButtonDown("Move Northeast"))
            {
                Move(SelectedArmy, 1);
            }
            if (Input.GetButtonDown("Move North"))
            {
                Move(SelectedArmy, 0);
            }
            if (Input.GetButtonDown("Move Northwest"))
            {
                Move(SelectedArmy, 7);
            }
            if (Input.GetButtonDown("Move West"))
            {
                Move(SelectedArmy, 6);
            }

        }
        RegenArmyBar(SelectedArmy);
    }

    private void Move(Army army, int x)
    {
        if (!army.Move(x))
            RegenArmyBar(army);
        else
            StrategicUtilities.StartBattle(army);
    }

    private void MoveTo(Army army, Vec2i pos)
    {
        if (!army.MoveTo(pos))
            RegenArmyBar(army);
        else
            StrategicUtilities.StartBattle(army);
    }

    void AI(float dt)
    {
        if (State.GameManager.queuedTactical)
            return;
        if (m_timer > 0)
        {
            m_timer -= dt;
        }
        else
        {
            //do AI processing
            if (ActingEmpire.StrategicAI.RunAI() == false)
            {
                EndTurn();
            }
            m_timer = Config.StrategicAIMoveDelay;
        }
    }



    public void ButtonCallback(int ID)
    {

        if (ActingEmpire.StrategicAI == null && State.GameManager.StrategicControlsLocked == false)
        {
            if (!subWindowUp)
            {
                switch (ID)
                {
                    case 10:
                        if (runningQueued || QueuedPath != null)
                            return;
                        EndTurn();
                        break;
                    case 11:
                        ArmyInfoSetup();
                        break;
                    case 12:
                        SetupDevour();
                        subWindowUp = true;
                        DevourUI.gameObject.SetActive(true);
                        break;
                    case 15:
                        SetupTrain();
                        subWindowUp = true;
                        TrainUI.gameObject.SetActive(true);
                        break;
                    case 20:
                        PromptArmyPick();
                        break;
                }
            }
            else
            {
                switch (ID)
                {
                    case 13:
                        int numToEat = 0;
                        if (int.TryParse(DevourUI.NumberToDevour.text, out numToEat) == false)
                        {
                            State.GameManager.CreateMessageBox("Please enter a number for the amount of units to devour, or cancel");
                            return;
                        }

                        Devour(SelectedArmy, numToEat);
                        subWindowUp = false;
                        DevourUI.gameObject.SetActive(false);
                        break; 
                    case 14:
                        DevourUI.gameObject.SetActive(false);
                        subWindowUp = false;
                        break;
                    case 16:
                        TrainSelectedArmy();
                        subWindowUp = false;
                        TrainUI.gameObject.SetActive(false);
                        break;
                    case 17:
                        TrainUI.gameObject.SetActive(false);
                        subWindowUp = false;
                        break;
                    case 18:
                        ReportUI.gameObject.SetActive(false);
                        subWindowUp = false;
                        break;
                    case 21:
                        ExchangerUI.gameObject.SetActive(false);
                        int leftStartingMp = ExchangerUI.LeftArmy.RemainingMP;
                        int rightStartingMp = ExchangerUI.RightArmy.RemainingMP;
                        if (ExchangerUI.LeftReceived)
                        {
                            UndoMoves.Clear();
                            int costToEnter = StrategicTileInfo.WalkCost(ExchangerUI.LeftArmy.Position.x, ExchangerUI.LeftArmy.Position.y);
                            if (rightStartingMp - costToEnter < leftStartingMp)
                                ExchangerUI.LeftArmy.RemainingMP = Math.Max(0, rightStartingMp - costToEnter);
                        }

                        if (ExchangerUI.RightReceived)
                        {
                            UndoMoves.Clear();
                            int costToEnter = StrategicTileInfo.WalkCost(ExchangerUI.RightArmy.Position.x, ExchangerUI.RightArmy.Position.y);
                            if (leftStartingMp - costToEnter < rightStartingMp)
                                ExchangerUI.RightArmy.RemainingMP = Math.Max(0, leftStartingMp - costToEnter);
                        }

                        if (ExchangerUI.LeftArmy.Units.Count == 0)
                        {
                            ExchangerUI.LeftArmy.ItemStock.TransferAllItems(ExchangerUI.RightArmy.ItemStock);
                        }
                        else if (ExchangerUI.RightArmy.Units.Count == 0)
                        {
                            ExchangerUI.RightArmy.ItemStock.TransferAllItems(ExchangerUI.LeftArmy.ItemStock);
                        }
                        if (ExchangerUI.RightArmy.Units.All(u => u.HasTrait(Traits.Infiltrator)))
                        {
                            Village village = StrategicUtilities.GetVillageAt(ExchangerUI.RightArmy.Position);
                            var infilitrators = new List<Unit>();


                            ExchangerUI.RightArmy.Units.ForEach(unit =>
                            {
                                infilitrators.Add(unit);
                            });
                            if (village != null && village.Empire.IsEnemy(ExchangerUI.LeftArmy.Empire))
                            {
                                infilitrators.ForEach(inf =>
                                {
                                    StrategicUtilities.TryInfiltrate(ExchangerUI.RightArmy, inf, village);
                                });
                                ExchangerUI.RightArmy.Empire.Armies.Remove(ExchangerUI.RightArmy);
                                ExchangerUI.RightArmy.Empire.ArmiesCreated--;
                            }
                            else
                            {
                                MercenaryHouse mercHouse = StrategicUtilities.GetMercenaryHouseAt(ExchangerUI.RightArmy.Position);
                                if (mercHouse != null)
                                {
                                    infilitrators.ForEach(inf =>
                                    {
                                        StrategicUtilities.TryInfiltrate(ExchangerUI.RightArmy, inf, null, mercHouse);
                                    });
                                    ExchangerUI.RightArmy.Empire.Armies.Remove(ExchangerUI.RightArmy);
                                    ExchangerUI.RightArmy.Empire.ArmiesCreated--;
                                }
                            }
                        }
                        Regenerate();
                        subWindowUp = false;
                        break;

                }
            }
        }
    }


    void SetupDevour()
    {
        Village village = StrategicUtilities.GetVillageAt(SelectedArmy.Position);

        if (village != null && SelectedArmy.RemainingMP > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"this is the village of {village.Name}");
            sb.AppendLine($"the population are {village.Race}");
            sb.AppendLine($"it has a population of {village.GetTotalPop()}");
            sb.AppendLine($"of which {village.Garrison} are garrison units");
            sb.AppendLine($"to completely heal you should eat {SelectedArmy.GetDevourmentCapacity(1)}");
            if (SelectedArmy.Units.Where(s => s.Predator == false).Any())
                sb.AppendLine($"note that some of the units in this army are not predators");
            sb.AppendLine("how many should they devour?");

            if (village.GetRecruitables().Count > 0)
            {
                sb.AppendLine($"(it has {village.GetRecruitables().Count} stored units");
                sb.AppendLine($"if you try to eat too many it will eat these)");
            }
            DevourUI.FullText.text = sb.ToString();
        }

    }

    public void BuildDevourSelectDisplay()
    {
        if (SelectedArmy == null)
            return;
        Village village = StrategicUtilities.GetVillageAt(SelectedArmy.Position);
        if (village == null)
            return;
        int children = RaceUI.ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(RaceUI.ActorFolder.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < village.VillagePopulation.Population.Count; i++)
        {
            if (village.VillagePopulation.Population[i].Population > 0)
            {
                GameObject obj = Instantiate(RaceUI.DevourPanel, RaceUI.ActorFolder);
                UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
                Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), new Unit(1, village.VillagePopulation.Population[i].Race, 0, true));
                TextMeshProUGUI text = obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                var racePar = RaceParameters.GetTraitData(actor.Unit);
                text.text = $"{village.VillagePopulation.Population[i].Race}\nTotal: {village.VillagePopulation.Population[i].Population} \nHireable: {village.VillagePopulation.Population[i].Hireables}\nFavored Stat: {State.RaceSettings.GetFavoredStat(actor.Unit.Race)}\nDefault Traits:\n{State.RaceSettings.ListTraits(actor.Unit.Race)}";
                sprite.UpdateSprites(actor);
                Button button1 = obj.GetComponentsInChildren<Button>()[0];
                Race tempRace = village.VillagePopulation.Population[i].Race;
                button1.onClick.AddListener(() => Devour(SelectedArmy, tempRace));
                button1.onClick.AddListener(() => BuildDevourSelectDisplay());
                Button button2 = obj.GetComponentsInChildren<Button>()[1];
                button2.onClick.AddListener(() => DevourAll(SelectedArmy, tempRace));
                button2.onClick.AddListener(() => BuildDevourSelectDisplay());
            }

        }

        RaceUI.gameObject.SetActive(true);
        if (SelectedArmy.RemainingMP == 0)
        {
            DevourUI.gameObject.SetActive(false);
            subWindowUp = false;
        }
    }

    public void CloseDevourSelectPanel()
    {
        RaceUI.gameObject.SetActive(false);
    }

    public void Devour(Army predArmy, int numToEat)
    {
        if (numToEat <= 0)
            return;
        Village village = StrategicUtilities.GetVillageAt(predArmy.Position);

        if (village != null && predArmy.RemainingMP > 0 && village.GetTotalPop() > 0)
        {

            if (village.GetTotalPop() < numToEat)
                numToEat = village.GetTotalPop();

            village.DevouredPercentage(numToEat / village.GetTotalPop());
            village.SubtractPopulation(numToEat);


            predArmy.DevourHeal(numToEat);

            if (village.GetTotalPop() < 1)
            {
                RedrawVillages();
                if (village.Empire.ReplacedRace != predArmy.Empire.ReplacedRace)
                {
                    RelationsManager.Genocide(predArmy.Empire, village.Empire);
                }
            }
        }

        RegenArmyBar(SelectedArmy);
    }

    public void Devour(Army predArmy, Race race)
    {
        Village village = StrategicUtilities.GetVillageAt(predArmy.Position);

        if (village != null && (predArmy.RemainingMP > 0 || predArmy.DevourThisTurn == true) && village.GetTotalPop() > 0)
        {

            village.DevouredPercentage(1 / village.GetTotalPop());
            village.SubtractPopulation(1, race);

            if (Config.MultiRaceVillages && village.VillagePopulation.GetRacePop(race) <= 0)
                village.VillagePopulation.DirectLinkToNamed().RemoveAll(s => s.Race == race);

            predArmy.DevourHeal(1);

            if (village.GetTotalPop() < 1)
            {
                RedrawVillages();
                if (village.Empire.ReplacedRace != predArmy.Empire.ReplacedRace)
                {
                    RelationsManager.Genocide(predArmy.Empire, village.Empire);
                }
            }
        }

        RegenArmyBar(SelectedArmy);
    }

    public void DevourAll(Army predArmy, Race race)
    {


        Village village = StrategicUtilities.GetVillageAt(predArmy.Position);

        int numToEat = village.VillagePopulation.GetRacePop(race);

        if (village != null && (predArmy.RemainingMP > 0 || predArmy.DevourThisTurn == true) && village.GetTotalPop() > 0)
        {

            village.DevouredPercentage(numToEat / village.GetTotalPop());
            village.SubtractPopulation(numToEat, race);

            if (Config.MultiRaceVillages)
                village.VillagePopulation.DirectLinkToNamed().RemoveAll(s => s.Race == race);

            predArmy.DevourHeal(numToEat);

            if (village.GetTotalPop() < 1)
            {
                RedrawVillages();
                if (village.Empire.ReplacedRace != predArmy.Empire.ReplacedRace)
                {
                    RelationsManager.Genocide(predArmy.Empire, village.Empire);
                }
            }
        }

        RegenArmyBar(SelectedArmy);
    }

    //public void Devour(Army predArmy, int numToEat, Race race)
    //{
    //    if (numToEat <= 0)
    //        return;
    //    Village village = StrategicUtilities.GetVillageAt(predArmy.Position);

    //    if (village != null && (predArmy.RemainingMP > 0 || predArmy.DevourThisTurn == true) && village.GetTotalPop() > 0)
    //    {

    //        if (village.GetTotalPop() < numToEat)
    //            numToEat = village.GetTotalPop();

    //        village.DevouredPercentage(numToEat / village.GetTotalPop());
    //        village.SubtractPopulation(numToEat, race);


    //        predArmy.DevourHeal(numToEat);

    //        if (village.GetTotalPop() < 1)
    //        {
    //            RedrawVillages();
    //            if (village.Empire.Race != predArmy.Empire.Race)
    //            {
    //                RelationsManager.Genocide(predArmy.Empire, village.Empire);
    //            }
    //        }
    //    }

    //    RegenArmyBar(SelectedArmy);
    //}

    private void CheckPath(Vec2i mouseLocation)
    {
        if (!mouseMovementMode)
        {
            arrowManager.ClearNodes();
            return;
        }

        if (SelectedArmy == null)
            return;

        if (currentPathDestination != null && mouseLocation.Matches(currentPathDestination))
            return;
        currentPathDestination = mouseLocation;
        var path = StrategyPathfinder.GetArmyPath(ActingEmpire, SelectedArmy, mouseLocation, SelectedArmy.RemainingMP, SelectedArmy.movementMode == Army.MovementMode.Flight);
        arrowManager.ClearNodes();
        if (path == null || path.Count == 0)
            return;
        int remainingMP = SelectedArmy.RemainingMP;
        for (int i = 0; i < path.Count; i++)
        {
            Vec2i nextNode = new Vec2i(path[i].X, path[i].Y);
            Army.TileAction action = SelectedArmy.CheckTileForActionType(nextNode);
            if (action == Army.TileAction.OneMP)
                remainingMP--;
            else if (action == Army.TileAction.TwoMP)
                remainingMP -= 2;
            else if (action == Army.TileAction.Attack)
            {
                if (remainingMP > 0)
                {
                    arrowManager.PlaceNode(Color.red, nextNode);
                    break;
                }
                remainingMP -= 1;

            }

            if (remainingMP >= 0)
                arrowManager.PlaceNode(Color.green, nextNode);
            else
            {
                ContinuePath(SelectedArmy, path, i);
                break;
            }

        }

    }

    void ShowPathOfArmy(Army army)
    {
        var path = StrategyPathfinder.GetArmyPath(ActingEmpire, army, army.Destination, army.RemainingMP, army.movementMode == Army.MovementMode.Flight);
        arrowManager.ClearNodes();
        if (path == null || path.Count == 0)
            return;
        int remainingMP = army.RemainingMP;

        for (int i = 0; i < path.Count; i++)
        {
            Vec2i nextNode = new Vec2i(path[i].X, path[i].Y);

            Army.TileAction action = army.CheckTileForActionType(nextNode);
            if (action == Army.TileAction.OneMP)
                remainingMP--;
            else if (action == Army.TileAction.TwoMP)
                remainingMP -= 2;
            else if (action == Army.TileAction.Attack)
            {
                if (remainingMP > 0)
                {
                    arrowManager.PlaceNode(Color.red, nextNode);
                    break;
                }
                remainingMP -= 1;

            }

            if (remainingMP >= 0)
                arrowManager.PlaceNode(Color.green, nextNode);
            else
            {
                ContinuePath(army, path, i);
                break;
            }

        }
    }

    void ContinuePath(Army army, List<PathNode> path, int current)
    {
        int lastNumber = 1;
        int remainingBatchMp = army.GetMaxMovement();
        for (int i = current; i < path.Count; i++)
        {
            Vec2i nextNode = new Vec2i(path[i].X, path[i].Y);
            Army.TileAction action = army.CheckTileForActionType(nextNode);

            if (action == Army.TileAction.OneMP)
                remainingBatchMp--;
            else if (action == Army.TileAction.TwoMP)
                remainingBatchMp -= 2;
            else if (action == Army.TileAction.Attack)
            {
                remainingBatchMp = 0;

            }


            if (remainingBatchMp == 1)
            {
                if (path.Count >= i + 2)
                {
                    Army.TileAction nextAction = army.CheckTileForActionType(new Vec2i(path[i + 1].X, path[i + 1].Y));
                    if (nextAction == Army.TileAction.TwoMP)
                        remainingBatchMp = 0;
                }


            }

            if (remainingBatchMp == 0)
            {
                arrowManager.PlaceNode(Color.gray, nextNode, lastNumber.ToString());
                lastNumber++;
                remainingBatchMp = army.GetMaxMovement();
            }
            else
                arrowManager.PlaceNode(Color.gray, nextNode);


        }
    }



    void SetupTrain()
    {
        Village village = StrategicUtilities.GetVillageAt(SelectedArmy.Position);

        if (village != null && SelectedArmy.RemainingMP > 0)
        {
            StringBuilder sb = new StringBuilder();
            int unitCount = SelectedArmy.Units.Count;
            sb.Append($"This army has {unitCount} units");
            trainCost = new int[7];
            trainExp = new int[7];

            for (int i = 0; i < 7; i++)
            {
                trainCost[i] = SelectedArmy.TrainingGetCost(i);
                trainExp[i] = SelectedArmy.TrainingGetExpValue(i);
            }

            var maxTrainLevel = village.NetBoosts.MaximumTrainingLevelAdd;

            List<string> options = new List<string>();

            if (maxTrainLevel >= 1) options.Add($"Steady Training - {trainExp[0]} EXP, {trainCost[0]}G");
            if (maxTrainLevel >= 2) options.Add($"Involved Training - {trainExp[1]} EXP, {trainCost[1]}G");
            if (maxTrainLevel >= 3) options.Add($"Heavy Training - {trainExp[2]} EXP, {trainCost[2]}G");
            if (maxTrainLevel >= 4) options.Add($"Advanced Training - {trainExp[3]} EXP, {trainCost[3]}G");
            if (maxTrainLevel >= 5) options.Add($"Extreme Training - {trainExp[4]} EXP, {trainCost[4]}G");
            if (maxTrainLevel >= 6) options.Add($"Hero Training - {trainExp[5]} EXP, {trainCost[5]}G");
            if (maxTrainLevel >= 7) options.Add($"Godly Training - {trainExp[6]} EXP, {trainCost[6]}G");

            TrainUI.TrainingLevel.ClearOptions();
            TrainUI.TrainingLevel.AddOptions(options);
            if (TrainUI.TrainingLevel.value >= maxTrainLevel)
            {
                TrainUI.TrainingLevel.value = maxTrainLevel - 1;
                TrainUI.TrainingLevel.RefreshShownValue();
            }
            TrainUI.FullText.text = sb.ToString();
            CheckTrainingCost();
        }
    }

    public void CheckTrainingCost()
    {
        if (ActingEmpire.Gold >= trainCost[TrainUI.TrainingLevel.value])
        {
            TrainUI.Train.interactable = true;
            TrainUI.Train.GetComponentInChildren<Text>().text = "Train!";
        }
        else
        {
            TrainUI.Train.interactable = false;
            TrainUI.Train.GetComponentInChildren<Text>().text = "Not enough gold";
        }

    }

    public void TrainSelectedArmy()
    {
        Village village = StrategicUtilities.GetVillageAt(SelectedArmy.Position);

        if (village != null && SelectedArmy.RemainingMP > 0)
        {
            SelectedArmy.Train(TrainUI.TrainingLevel.value);
            SelectedArmy.RemainingMP = 0;
            State.GameManager.StrategyMode.UndoMoves.Clear();
        }

        Regenerate();
    }

    void ArmyInfoSetup()
    {
        Village village = StrategicUtilities.GetVillageAt(SelectedArmy.Position);
        if (village != null)
        {
            State.GameManager.ActivateRecruitMode(village, ActingEmpire);
        }
        else
        {
            MercenaryHouse house = StrategicUtilities.GetMercenaryHouseAt(SelectedArmy.Position);
            if (house != null)
                State.GameManager.ActivateRecruitMode(house, ActingEmpire, SelectedArmy);
            else
                State.GameManager.ActivateRecruitMode(ActingEmpire, SelectedArmy);
        }
    }



    void EndTurn()
    {
        UndoMoves.Clear();
        ActingEmpire.Reports.Clear();
        ActingEmpire.OwnedTiles.Clear();
        if (State.World.EmpireOrder == null || State.World.EmpireOrder.Count != State.World.AllActiveEmpires.Count)
            State.World.RefreshTurnOrder();
        int startingIndex = State.World.EmpireOrder.IndexOf(ActingEmpire);
        SelectedArmy = null;
        StatusBarUI.EndTurn.interactable = false;
        StatusBarUI.ShowTurnReport.gameObject.SetActive(false);
        StatusBarUI.EmpireStatus.interactable = OnlyAIPlayers;
        foreach (Village vil in State.World.Villages.Where(s => s.Side == ActingEmpire.Side))
        {
            ClaimWithinXTilesOf(vil.Position, 2);
        }
        if (startingIndex + 1 >= State.World.EmpireOrder.Count)
        {

            ScaledExp = StrategicUtilities.Get80thExperiencePercentile();
            StatusBarUI.RecreateWorld.gameObject.SetActive(false);
            State.World.Turn++;
            ProcessGrowth();
            if (Config.Diplomacy == false)
            {
                RelationsManager.ResetRelationTypes();
            }
            State.EventList.CheckStartAIEvent();

            RelationsManager.TurnElapsed();
            ActingEmpire = State.World.EmpireOrder[0];
            if (State.World.MonsterEmpires.Count() < World.MonsterCount)
                State.World.RefreshMonstersKeepingArmies();
            foreach (ClaimableBuilding claimable in State.World.Claimables)
            {
                claimable.TurnChanged();
            }
            if (OnlyAIPlayers)
                State.Save($"{State.SaveDirectory}Autosave.sav");
        }
        else
        {
            foreach (var army in ActingEmpire.Armies)
            {
                foreach (var unit in army.Units)
                {
                    if (unit.AllConditionalTraits != null)
                    {
                        foreach (var item in unit.AllConditionalTraits.Keys.Where(t => t.trigger == TraitConditionTrigger.OnStrategicTurnEnd || t.trigger == TraitConditionTrigger.All).ToList())
                        {
                            if (ConditionalTraitConditionChecker.StrategicTraitConditionActive(unit, item))
                            {
                                unit.ActivateConditionalTrait(item.id);
                            }
                            else
                            {
                                unit.DeactivateConditionalTrait(item.id);
                            }
                        }
                    }
                }
            }
            ActingEmpire = State.World.EmpireOrder[startingIndex + 1];
        }

        float NightRoll = (float)State.Rand.NextDouble();
        if (Config.DayNightEnabled)
        {
            if (Config.DayNightSchedule && State.World.Turn % Config.World.NightRounds == 0)
            {
                State.World.IsNight = true;
            }
            else if (Config.DayNightRandom && NightRoll < NightChance)
            {
                State.World.IsNight = true;
                NightChance = Config.BaseNightChance;
            }
            else
            {
                State.World.IsNight = false;
                NightChance += Config.NightChanceIncrease;
            }
        }
        else
        {
            State.World.IsNight = false;
            NightChance = Config.BaseNightChance;
        }
        if (ActingEmpire is MonsterEmpire)
        {
            if (ActingEmpire.Race == Race.Goblins)
            {

            }
            else if (Config.SpawnerInfo(ActingEmpire.Race).Enabled == false)
            {
                EndTurn();
                return;
            }
        }



        VictoryCheck();

        if (ActingEmpire.KnockedOut && (ActingEmpire.Side < 700 || (ActingEmpire.Armies.Count == 0 && ActingEmpire.VillageCount == 0)))
        {
            EndTurn();
            return;
        }

        CheckIfLastHumanPlayerEliminated();
        BeginTurn();

    }

    internal void BeginTurn()
    {
#if UNITY_EDITOR
        var units = StrategicUtilities.GetAllUnits();
        int duplicates = 0;
        for (int i = 0; i < units.Count; i++)
        {
            for (int j = 0; j < units.Count; j++)
            {
                if (i == j)
                    continue;
                if (units[i] == units[j])
                    duplicates++;
            }
        }
        if (duplicates > 0)
        {
            Debug.LogWarning($"{duplicates / 2} duplicated units found!");
        }
#endif
        ProcessIncome(ActingEmpire);
        HandleBuildings(ActingEmpire);
        if (ActingEmpire.StrategicAI == null)
        {
            if (State.World.Turn > 1)
                ActingEmpire.CheckEvent();
            LastHumanEmpire = ActingEmpire;
            State.Save($"{State.SaveDirectory}Autosave.sav");
            StatusBarUI.EndTurn.interactable = true;
            StatusBarUI.ShowTurnReport.gameObject.SetActive(ActingEmpire.Reports.Count > 0);
            StatusBarUI.EmpireStatus.interactable = true;
            EnemyTurnText.SetActive(false);
            ShowTurnReport();
        }
        else
        {
            if (ActingEmpire.StrategicAI.TurnAI()) //If a unit was purchased
                RedrawArmies();
            if (ActingEmpire.Armies.Count > 0)
                m_timer = .3f;
            else
                m_timer = .01f;
            StatusBarUI.EmpireStatus.interactable = ActingEmpire.IsAlly(LastHumanEmpire) || OnlyAIPlayers;
            EnemyTurnText.SetActive(true);
        }
        StatusBarUI.Build.onClick.AddListenerOnce(() => BuildMenu.Open(ActingEmpire));
        runningQueued = true;
        Regenerate();
    }

    internal void CheckForRevivedPlayerFromMapEditor()
    {
        foreach (Empire empire in State.World.MainEmpires)
        {
            if (empire.KnockedOut && State.World.Villages.Where(s => s.Empire.IsAlly(empire)).Count() > 0)
                empire.KnockedOut = false;
        }
    }

    void CheckIfLastHumanPlayerEliminated()
    {
        if (ActingEmpire.KnockedOut == false && StrategicUtilities.GetAllArmies().Where(s => s.Empire.IsAlly(ActingEmpire)).Any() == false && State.World.Villages.Where(s => s.Empire.IsAlly(ActingEmpire)).Count() == 0 && ActingEmpire is MonsterEmpire == false)
        {
            ActingEmpire.KnockedOut = true;
            if (ActingEmpire.Buildings.Any())
            {
                foreach (var item in ActingEmpire.Buildings)
                {
                    item.Owner = null;
                    ActingEmpire.Buildings.Remove(item);
                }
            }
            if (ActingEmpire.StrategicAI == null)
            {
                OnlyAIPlayers = true;
                foreach (Empire emp in State.World.MainEmpires)
                {
                    if (emp.StrategicAI == null && emp.KnockedOut == false)
                    {
                        OnlyAIPlayers = false;
                    }
                }
                if (OnlyAIPlayers && State.GameManager.CurrentScene == State.GameManager.StrategyMode)
                {
                    var box = GameManager.Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
                    box.SetData(State.GameManager.ActivateEndSceneLose,
                        "End Game",
                        "Continue Watching",
                        "The last human player is eradicated, do you want to continue watching or end the game now and show stats?",
                        EndTurn);
                    return;
                }
            }
        }
    }



    void VictoryCheck()
    {

        Dictionary<Empire, bool> survivors = new Dictionary<Empire, bool>();

        switch (Config.VictoryCondition)
        {
            case Config.VictoryType.AllCapitals:
                foreach (Village village in State.World.Villages.Where(s => s.Capital))
                {
                    if (village.GetTotalPop() < 1)
                        continue;
                    survivors[village.Empire] = true;
                }
                break;
            case Config.VictoryType.AllCities:
                foreach (Village village in State.World.Villages)
                {
                    if (village.GetTotalPop() < 1)
                        continue;
                    survivors[village.Empire] = true;
                }
                break;
            case Config.VictoryType.CompleteElimination:
                foreach (Village village in State.World.Villages)
                {
                    if (village.GetTotalPop() < 1)
                        continue;
                    survivors[village.Empire] = true;
                }
                foreach (Army army in StrategicUtilities.GetAllArmies(true))
                {
                    survivors[army.Empire] = true;
                }
                break;
            case Config.VictoryType.NeverEnd:
                return;
        }
        int side = 0;
        foreach (Empire emp in survivors.Keys)
        {
            side = emp.Side;
            foreach (Empire emp2 in survivors.Keys)
            {
                if (emp == emp2)
                    continue;
                if (emp.IsAlly(emp2) == false)
                    return;
            }
        }

        State.GameManager.ActivateEndSceneWin(side);

    }


    void ProcessIncome(Empire empire)
    {

        empire.CalcIncome(State.World.Villages, true);
        empire.AddGold(empire.Income);
        if (empire.Side >= 50)
        {
            if (empire.Gold < 0)
                empire.AddGold(-empire.Gold);
        }
        else if (empire.Gold < 0)
        {
            int income = empire.Income;

            if (income < 0)
            {
                Unit[] dismissOrder = empire.GetAllUnits().Where(s => s.Health > 0).OrderBy(s => s.Experience).ToArray();
                for (int k = 0; k < dismissOrder.Length; k++)
                {
                    if (dismissOrder[k].Type == UnitType.Leader)
                        continue;
                    if (income >= 0)
                        break;
                    foreach (Army army in empire.Armies)
                    {
                        if (army.Units.Contains(dismissOrder[k]))
                        {
                            StrategicUtilities.ProcessTravelingUnit(dismissOrder[k], army);
                            army.Units.Remove(dismissOrder[k]);
                            break;
                        }
                    }
                    dismissOrder[k].Health = 0;
                    income += (int)Math.Round(Config.World.ArmyUpkeep * RaceParameters.GetTraitData(dismissOrder[k]).Upkeep);
                }
            }

            Regenerate();
        }

    }
    
    void HandleBuildings(Empire empire)
    {
        ConstructibleBuilding[] owned_buildings = State.World.Constructibles.Where(x => x.Owner == empire).ToArray();
        foreach (var constructable in owned_buildings)
        {
            constructable.RunBuildingTurn();
        }
    }

    void ProcessGrowth()
    {
        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            State.World.Villages[i].NewTurn();
        }

        for (int i = 0; i < State.World.AllActiveEmpires.Count; i++)
        {
            State.World.AllActiveEmpires[i].CalcIncome(State.World.Villages);
            State.World.AllActiveEmpires[i].Regenerate();
        }
        for (int i = 0; i < State.World.MercenaryHouses.Length; i++)
        {
            State.World.MercenaryHouses[i].UpdateStock();
        }
        MercenaryHouse.UpdateStaticStock();

    }



    void ProcessClick(int x, int y)
    {
        Vec2i clickLocation = new Vec2i(x, y);
        if (mouseMovementMode)
        {
            SetSelectedArmyPathTo(x, y);
        }
        else if (pickingExchangeLocation)
        {
            OpenExchangerPanel(SelectedArmy, clickLocation);
        }
        else
        {
            foreach (Army army in ActingEmpire.Armies.Where(a => a.Side == ActingEmpire.Side))
            {
                if (army.Position.GetDistance(clickLocation) < 1)
                {
                    var building = StrategicUtilities.GetConstructibleAt(army.Position);
                    bool buildingOwned = false;
                    if (building != null)
                    {
                        if (building.Owner == ActingEmpire)
                        {
                            buildingOwned = true;
                        }
                    }
                    else if (buildingOwned)
                    {
                        BuildingArmyPopup.Open(army, building);
                    }
                    if (SelectedArmy != army)
                    {
                        SelectedArmy = army;
                        SelectedArmy.RefreshMovementMode();
                        if (army.Destination != null && army.Destination.GetDistance(army.Position) > 0)
                        {
                            var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
                            box.SetData(() => SelectedArmy.Destination = null, "Clear Orders", "Leave Orders", "This army has a queued movement order, do you want to clear it?");
                        }
                        return;
                    }
                }
            }

            foreach (Army army in StrategicUtilities.GetAllArmies().Where(s => s.Side == ActingEmpire.Side))
            {
                if (army.Position.GetDistance(clickLocation) < 1)
                {
                    var building = StrategicUtilities.GetConstructibleAt(army.Position);
                    bool buildingOwned = false;
                    if (building != null)
                    {
                        if (building.Owner == ActingEmpire)
                        {
                            buildingOwned = true;
                        }
                    }
                    Village village = StrategicUtilities.GetVillageAt(army.Position);
                    if (village != null)
                    {
                        State.GameManager.ActivateRecruitMode(village, ActingEmpire);
                    }
                    else if (buildingOwned)
                    {
                        BuildingArmyPopup.Open(army, building);
                    }
                    else
                    {
                        MercenaryHouse house = StrategicUtilities.GetMercenaryHouseAt(army.Position);
                        if (house != null)
                            State.GameManager.ActivateRecruitMode(house, ActingEmpire, army, Recruit_Mode.ActivatingEmpire.Self);
                        else
                            State.GameManager.ActivateRecruitMode(ActingEmpire, army, Recruit_Mode.ActivatingEmpire.Self);
                    }
                    return;
                }
            }



            foreach (Army army in StrategicUtilities.GetAllArmies().Where(s => s.Side != ActingEmpire.Side && s.Empire.IsAlly(ActingEmpire)))
            {
                if (army.Position.GetDistance(clickLocation) < 1)
                {
                    var building = StrategicUtilities.GetConstructibleAt(army.Position);
                    bool buildingOwned = false;
                    if (building != null)
                    {
                        if (building.Owner == ActingEmpire)
                        {
                            buildingOwned = true;
                        }
                    }
                    Village village = StrategicUtilities.GetVillageAt(army.Position);
                    if (village != null)
                    {
                        State.GameManager.ActivateRecruitMode(village, ActingEmpire);
                    }
                    else if (buildingOwned)
                    {
                        BuildingArmyPopup.Open(army, building);
                    }
                    else
                    {
                        MercenaryHouse house = StrategicUtilities.GetMercenaryHouseAt(army.Position);
                        if (house != null)
                            State.GameManager.ActivateRecruitMode(house, ActingEmpire, army, Recruit_Mode.ActivatingEmpire.Ally);
                        else
                            State.GameManager.ActivateRecruitMode(ActingEmpire, army, Recruit_Mode.ActivatingEmpire.Ally);
                    }
                    return;

                }
            }

            foreach (Army army in StrategicUtilities.GetAllArmies().Where(s => (s.Side != ActingEmpire.Side && s.Empire.IsEnemy(ActingEmpire) || s.Side != ActingEmpire.Side && s.Empire.IsNeutral(ActingEmpire)) && ContainsFriendly(s)))
            {
                if (army.Position.GetDistance(clickLocation) < 1)
                {
                    var building = StrategicUtilities.GetConstructibleAt(army.Position);
                    bool buildingOwned = false;
                    if (building != null)
                    {
                        if (building.Owner == ActingEmpire)
                        {
                            buildingOwned = true;
                        }
                    }
                    else if (buildingOwned)
                    {
                        BuildingArmyPopup.Open(army, building);
                    }
                    Village village = StrategicUtilities.GetVillageAt(army.Position);
                    if (village != null)
                    {
                        State.GameManager.ActivateRecruitMode(village, ActingEmpire, Recruit_Mode.ActivatingEmpire.Infiltrator);
                    }
                    else
                    {
                        MercenaryHouse house = StrategicUtilities.GetMercenaryHouseAt(army.Position);
                        if (house != null)
                            State.GameManager.ActivateRecruitMode(house, ActingEmpire, army, Recruit_Mode.ActivatingEmpire.Infiltrator);
                        else
                            State.GameManager.ActivateRecruitMode(ActingEmpire, army, Recruit_Mode.ActivatingEmpire.Infiltrator);
                    }
                    return;

                }
            }

            for (int i = 0; i < State.World.Villages.Length; i++)
            {
                if (State.World.Villages[i].Position.GetDistance(clickLocation) == 0)
                {
                    if (State.World.Villages[i].Empire.IsAlly(ActingEmpire))
                    {
                        State.GameManager.ActivateRecruitMode(State.World.Villages[i], ActingEmpire);
                        return;
                    }
                }
            }

            for (int i = 0; i < State.World.Constructibles.Length; i++)
            {
                if (State.World.Constructibles[i].Position.GetDistance(clickLocation) == 0)
                {
                    if (State.World.Constructibles[i].Owner == ActingEmpire && State.World.Constructibles[i].active)
                    {
                        UpgradeMenu.Open(State.World.Constructibles[i]);
                        return;
                    }
                }
            }

            if (Config.CheatViewHostileArmies)
            {
                if (ProcessClickWithoutEmpire(x, y))
                    return;
            }

            for (int i = 0; i < State.World.AncientTeleporters.Length; i++)
            {
                if (State.World.AncientTeleporters[i].Position.GetDistance(clickLocation) == 0)
                {
                    AncientTeleportMenu.Open(State.World.AncientTeleporters[i], ActingEmpire);
                    return;
                }
            }
            SelectedArmy = null;
        }
    }

    private bool ContainsFriendly(Army s)
    {
        return s.Units.Any(u =>
        {
            return u.FixedSide == ActingEmpire.Side || (State.World.GetEmpireOfSide(u.FixedSide)?.IsAlly(ActingEmpire) ?? false);
        });
    }

    bool ProcessClickWithoutEmpire(int x, int y)
    {
        Vec2i clickLocation = new Vec2i(x, y);
        foreach (Army army in StrategicUtilities.GetAllArmies())
        {
            if (army.Position.GetDistance(clickLocation) < 1)
            {
                Village village = StrategicUtilities.GetVillageAt(army.Position);
                if (village != null)
                {
                    State.GameManager.ActivateRecruitMode(village, ActingEmpire, Recruit_Mode.ActivatingEmpire.Observer);
                }
                else
                    State.GameManager.ActivateRecruitMode(ActingEmpire, army, Recruit_Mode.ActivatingEmpire.Observer);
                return true;
            }
        }
        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (State.World.Villages[i].Position.GetDistance(clickLocation) == 0)
            {
                State.GameManager.ActivateRecruitMode(State.World.Villages[i], ActingEmpire, Recruit_Mode.ActivatingEmpire.Observer);
                return true;
            }
        }
        return false;
    }

    bool ProcessClickBetweenTurns(int x, int y)
    {
        Vec2i clickLocation = new Vec2i(x, y);
        foreach (Army army in StrategicUtilities.GetAllArmies())
        {
            if (army.Position.GetDistance(clickLocation) < 1 && army.Empire.IsAlly(LastHumanEmpire))
            {
                Village village = StrategicUtilities.GetVillageAt(army.Position);
                if (village != null)
                {
                    State.GameManager.ActivateRecruitMode(village, ActingEmpire, Recruit_Mode.ActivatingEmpire.Observer);
                }
                else
                    State.GameManager.ActivateRecruitMode(ActingEmpire, army, Recruit_Mode.ActivatingEmpire.Observer);
                return true;
            }
        }
        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (State.World.Villages[i].Position.GetDistance(clickLocation) == 0 && State.World.Villages[i].Empire.IsAlly(LastHumanEmpire))
            {
                State.GameManager.ActivateRecruitMode(State.World.Villages[i], ActingEmpire, Recruit_Mode.ActivatingEmpire.Observer);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedArmyPathTo(int x, int y)
    {
        if (SelectedArmy == null)
            return;
        arrowManager.ClearNodes();
        Vec2i clickLoc = new Vec2i(x, y);
        QueuedPath = StrategyPathfinder.GetArmyPath(ActingEmpire, SelectedArmy, clickLoc, SelectedArmy.RemainingMP, SelectedArmy.movementMode == Army.MovementMode.Flight);
        if (QueuedPath == null)
        {
            Army army = StrategicUtilities.ArmyAt(clickLoc);
            Village village = StrategicUtilities.GetVillageAt(clickLoc);
            if (army != null && army.Empire.IsNeutral(SelectedArmy.Empire))
            {
                var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
                box.SetData(() => RelationsManager.SetWar(army.Empire, SelectedArmy.Empire), "Declare War!", "Maintain Peace", $"Declare War against the {army.Empire.Name}?");
            }
            if (village != null && village.Empire.IsNeutral(SelectedArmy.Empire))
            {

                var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
                box.SetData(() => RelationsManager.SetWar(village.Empire, SelectedArmy.Empire), "Declare War!", "Maintain Peace", $"Declare War against the {village.Empire.Name}?");
            }
            return;
        }

        SelectedArmy.Destination = clickLoc;
        if (StrategicUtilities.ArmyAt(clickLoc) != null || StrategicUtilities.GetVillageAt(clickLoc) != null)
        {
            queuedAttackPermission = true;
        }
        else
            queuedAttackPermission = false;
        mouseMovementMode = false;
    }

    void SearchForVillage(string text)
    {
        var village = State.World.Villages.Where(s => s.Name.ToLower().Contains(text.ToLower())).FirstOrDefault();
        if (village != null)
        {
            State.GameManager.CenterCameraOnTile(village.Position.x, village.Position.y);
        }
    }

    string ReturnTextVillageCount(string search)
    {
        var count = State.World.Villages.Where(s => s.Name.ToLower().Contains(search.ToLower())).Count();
        if (count == 1)
            return "1 match";
        return $"{count} matches";
    }


    public override void ReceiveInput()
    {
        if (State.GameManager.ActiveInput)
            return;
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand)) && Input.GetKey(KeyCode.F))
        {
            var box = Instantiate(State.GameManager.InputBoxPrefab).GetComponentInChildren<InputBox>();
            box.SetData(SearchForVillage, "Search", "Cancel", "Search For Village", 20);
            box.ActivateTypeMethod(ReturnTextVillageCount);
        }

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Z))
        {
            if (UndoButton.gameObject.activeSelf)
                UndoButton.onClick.Invoke();
        }

        UndoButton.gameObject.SetActive(UndoMoves.Any());

        if (State.GameManager.TacticalMode.ActorFolder.childCount > 0)
        {
            int children = State.GameManager.TacticalMode.ActorFolder.childCount;
            for (int i = children - 1; i >= 0; i--)
            {
                Destroy(State.GameManager.TacticalMode.ActorFolder.GetChild(i).gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            foreach (Village village in State.World.Villages)
            {
                var obj = Instantiate(SpriteCategories[1]).GetComponent<SpriteRenderer>();
                obj.transform.SetPositionAndRotation(new Vector3(village.Position.x, village.Position.y), new Quaternion());
                obj.sprite = Banners[2];
                obj.sortingOrder = 30000;
                if (village.Side == ActingEmpire.Side)
                    obj.color = new Color(0, 1, 0, .75f);
                else if (village.Empire.IsEnemy(ActingEmpire))
                    obj.color = new Color(1, 0, 0, .75f);
                else if (village.Empire.IsNeutral(ActingEmpire))
                    obj.color = new Color(0, 0, 0, .75f);
                else
                    obj.color = new Color(0, 0, 1, .75f);
                ShownIFF.Add(obj.gameObject);
            }
            foreach (Army army in StrategicUtilities.GetAllArmies())
            {
                var obj = Instantiate(SpriteCategories[1]).GetComponent<SpriteRenderer>();
                obj.transform.SetPositionAndRotation(new Vector3(army.Position.x, army.Position.y), new Quaternion());
                obj.sprite = Banners[2];
                obj.sortingOrder = 30000;
                if (army.Side == ActingEmpire.Side)
                    obj.color = new Color(0, 1, 0, .75f);
                else if (army.Empire.IsEnemy(ActingEmpire))
                    obj.color = new Color(1, 0, 0, .75f);
                else if (army.Empire.IsNeutral(ActingEmpire))
                    obj.color = new Color(0, 0, 0, .75f);
                else
                    obj.color = new Color(0, 0, 1, .75f);
                ShownIFF.Add(obj.gameObject);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            foreach (var obj in ShownIFF.ToList())
            {
                Destroy(obj);
            }

        }


        if (remainingNotificationTime > 0)
            remainingNotificationTime -= Time.deltaTime;
        if (remainingNotificationTime <= 0)
            NotificationWindow.SetActive(false);

        if (Input.GetButtonDown("ChangeBannerSize"))
        {
            Config.BannerScale += .1f;
            if (Config.BannerScale > 1.02f)
                Config.BannerScale = .6f;
            PlayerPrefs.SetFloat("BannerScale", (Config.BannerScale - .5f) * 10);
        }


        if (State.GameManager.CameraTransitioning || State.GameManager.StrategicControlsLocked)
            return;

        if (Input.GetButtonDown("Pause"))
        {
            Paused = !Paused;
            PausedText.SetActive(Paused);
        }

        if (EventSystem.current.IsPointerOverGameObject() == false) //Makes sure mouse isn't over a UI element
        {

            Vector2 currentMousePos = State.GameManager.Camera.ScreenToWorldPoint(Input.mousePosition);

            int x = (int)(currentMousePos.x + 0.5f);
            int y = (int)(currentMousePos.y + 0.5f);
            if (x >= 0 && x < Config.StrategicWorldSizeX && y >= 0 && y < Config.StrategicWorldSizeY)
            {
                if (BuildMode)
                {
                    TilemapLayers[14].ClearAllTiles();
                    TilemapLayers[14].SetTile(new Vector3Int(x, y, 0), BuildIndicator[2]);
                    Vec2i position = new Vec2i(x, y);
                    if (Input.GetMouseButtonDown(0) && StrategicUtilities.IsSpaceOpenForBuild(position) && StrategicTileInfo.CanWalkInto(State.World.Tiles[position.x, position.y]) && TilemapLayers[13].GetTile(new Vector3Int(x, y, 0)) != BuildIndicator[1])
                    {
                        BuildMode = false;
                        TilemapLayers[14].ClearAllTiles();
                        TilemapLayers[13].ClearAllTiles();
                        ConstructibleBuilding newBuilding;
                        switch (SelectedConstruction)
                        {
                            case ConstructibleType.WorkCamp:
                                newBuilding = new WorkCamp(position);
                                newBuilding.Owner = ActingEmpire;
                                newBuilding.ConstructBuilding();
                                break;
                            case ConstructibleType.LumberSite:
                                newBuilding = new LumberSite(position);
                                newBuilding.Owner = ActingEmpire;
                                newBuilding.ConstructBuilding();
                                break;
                            case ConstructibleType.Quarry:
                                newBuilding = new Quarry(position);
                                newBuilding.Owner = ActingEmpire;
                                newBuilding.ConstructBuilding();
                                break;
                            case ConstructibleType.CasterTower:
                                newBuilding = new CasterTower(position);
                                newBuilding.Owner = ActingEmpire;
                                newBuilding.ConstructBuilding();
                                break;
                            case ConstructibleType.BarrierTower:
                                newBuilding = new BarrierTower(position);
                                newBuilding.Owner = ActingEmpire;
                                newBuilding.ConstructBuilding();
                                break;
                            case ConstructibleType.DefEncampment:
                                newBuilding = new DefenseEncampment(position);
                                newBuilding.Owner = ActingEmpire;
                                newBuilding.ConstructBuilding();
                                break;
                            case ConstructibleType.Academy:
                                newBuilding = new Academy(position);
                                newBuilding.Owner = ActingEmpire;
                                newBuilding.ConstructBuilding();
                                break;
                            case ConstructibleType.DarkMagicTower:
                                newBuilding = new BlackMagicTower(position);
                                newBuilding.Owner = ActingEmpire;
                                newBuilding.ConstructBuilding();
                                break;
                            case ConstructibleType.TemporalTower:
                                newBuilding = new TemporalTower(position);
                                newBuilding.Owner = ActingEmpire;
                                newBuilding.ConstructBuilding();
                                break;
                            case ConstructibleType.Laboratory:
                                newBuilding = new Laboratory(position);
                                newBuilding.Owner = ActingEmpire;
                                newBuilding.ConstructBuilding();
                                break;
                            case ConstructibleType.Teleporter:
                                newBuilding = new Teleporter(position);
                                newBuilding.Owner = ActingEmpire;
                                newBuilding.ConstructBuilding();
                                break;
                            case ConstructibleType.TownHall:
                                newBuilding = new TownHall(position);
                                newBuilding.Owner = ActingEmpire;
                                newBuilding.ConstructBuilding();
                                break;
                        }
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        TilemapLayers[14].ClearAllTiles();
                        TilemapLayers[13].ClearAllTiles();
                        BuildMode = false;
                    }
                }
                else
                {
                    UpdateTooltips(x, y);
                    if (Input.GetMouseButtonDown(0) && ActingEmpire.StrategicAI == null && subWindowUp == false && QueuedPath == null)
                        ProcessClick(x, y);
                    else if (Input.GetMouseButtonDown(0) && OnlyAIPlayers)
                        ProcessClickWithoutEmpire(x, y);
                    else if (Input.GetMouseButtonDown(0) && LastHumanEmpire.IsAlly(ActingEmpire))
                        ProcessClickBetweenTurns(x, y);
                    if (Input.GetMouseButtonDown(1) && ActingEmpire.StrategicAI == null && subWindowUp == false && QueuedPath == null && SelectedArmy != null && SelectedArmy.RemainingMP > 0)
                        SetSelectedArmyPathTo(x, y);
                    if (mouseMovementMode)
                        CheckPath(new Vec2i(x, y));
                }

            }
            else
            {
                arrowManager.ClearNodes();
                currentPathDestination = null;
            }

        }
        if (Input.GetButtonDown("Menu"))
        {
            State.GameManager.OpenMenu();
        }

        if (!buildingRangeOn && !BuildMode)
        {
            TilemapLayers[14].ClearAllTiles();
        }
        buildingRangeOn = false;
        if (Paused)
            return;

        Translator.UpdateLocation();

        if (ActingEmpire.StrategicAI != null)
        {
            AI(Time.deltaTime);
        }
        else
        {
            if (subWindowUp)
                return;

            RunQueuedMovement();

            if (Input.GetButtonDown("Next Unit"))
            {
                NextArmy();
            }

            if (Input.GetButtonDown("Cancel"))
            {
                SelectedArmy = null;
                ExchangeBlockerPanels.SetActive(false);
                pickingExchangeLocation = false;
            }

            if (Input.GetButtonDown("Movement Mode") && SelectedArmy != null)
            {
                if (SelectedArmy.RemainingMP > 0)
                    mouseMovementMode = true;
                else
                    SelectedArmy.Destination = null;
            }


            if (Input.GetButtonDown("Quicksave"))
            {
                State.Save($"{State.SaveDirectory}Quicksave.sav");
            }
            else if (Input.GetButtonDown("Quickload"))
            {
                State.GameManager.AskQuickLoad();
            }
            if (Translator.IsActive == false)
                UpdateArmyLocationsAndSprites();
            if (Input.GetButtonDown("Submit"))
                ButtonCallback(10);

        }

    }

    private void RunQueuedMovement()
    {
        MoveQueued();

        if (SelectedArmy != null && QueuedPath == null)
            CheckForMovementInput();

        if (runningQueued && QueuedPath == null)
        {

            bool foundWaiting = false;
            foreach (Army army in ActingEmpire.Armies)
            {
                if (army.RemainingMP > 1 && army.Destination != null)
                {

                    SelectedArmy = army;
                    foundWaiting = true;
                    QueuedPath = StrategyPathfinder.GetArmyPath(ActingEmpire, SelectedArmy, SelectedArmy.Destination, SelectedArmy.RemainingMP, SelectedArmy.movementMode == Army.MovementMode.Flight);
                    if (QueuedPath == null)
                        army.Destination = null;
                    break;
                }
            }
            if (foundWaiting == false)
                runningQueued = false;
        }

    }

    void MoveQueued()
    {
        if (QueuedPath != null)
        {
            if (SelectedArmy == null || SelectedArmy?.RemainingMP <= 0 || SelectedArmy.Destination == null)
            {
                QueuedPath = null;
                return;
            }
            if (QueuedPath.Count > 0 && SelectedArmy.RemainingMP == 1 && (SelectedArmy.CheckTileForActionType(new Vec2i(QueuedPath[0].X, QueuedPath[0].Y)) == Army.TileAction.TwoMP || SelectedArmy.CheckTileForActionType(new Vec2i(QueuedPath[0].X, QueuedPath[0].Y)) == Army.TileAction.AttackTwoMP))
            {
                QueuedPath = null;
                return;
            }

            if (QueuedPath.Count > 0)
            {
                Vec2i nextTile = new Vec2i(QueuedPath[0].X, QueuedPath[0].Y);

                if (SelectedArmy.Destination.Matches(nextTile) == false)
                {
                    if (StrategicUtilities.ArmyAt(nextTile) != null)
                    {
                        QueuedPath = null;
                        SelectedArmy.Destination = null;
                        return;
                    }
                    Village village = StrategicUtilities.GetVillageAt(nextTile);
                    if (village != null)
                    {
                        if (village.Empire.IsEnemy(SelectedArmy.Empire))
                        {
                            QueuedPath = null;
                            SelectedArmy.Destination = null;
                            return;
                        }
                    }
                }
                else
                {
                    if (StrategicUtilities.ArmyAt(nextTile) != null && queuedAttackPermission == false)
                    {
                        QueuedPath = null;
                        SelectedArmy.Destination = null;
                        return;
                    }
                    Village village = StrategicUtilities.GetVillageAt(nextTile);
                    if (village != null)
                    {
                        if (queuedAttackPermission == false && village.Empire.IsEnemy(SelectedArmy.Empire))
                        {
                            QueuedPath = null;
                            SelectedArmy.Destination = null;
                            return;
                        }
                        else if (village.Empire.IsNeutral(SelectedArmy.Empire))
                        {
                            QueuedPath = null;
                            SelectedArmy.Destination = null;
                            return;
                        }
                    }
                }
            }

            if (Translator.IsActive == false)
            {
                if (QueuedPath?.Count > 0)
                {
                    MoveTo(SelectedArmy, new Vec2i(QueuedPath[0].X, QueuedPath[0].Y));
                    if (QueuedPath[0].X != SelectedArmy.Position.x || QueuedPath[0].Y != SelectedArmy.Position.y)
                        QueuedPath = null;
                    else
                        QueuedPath.RemoveAt(0);
                }
                else
                    QueuedPath = null;
            }
        }


    }

    public void OpenRenameEmpire()
    {
        var box = Instantiate(State.GameManager.InputBoxPrefab).GetComponentInChildren<InputBox>();
        RenamingEmpire = ActingEmpire;
        box.SetData(RenameEmpire, "Rename", "Cancel", $"Rename this empire ({ActingEmpire.Name})?", 75);

    }

    internal void RenameEmpire(string name)
    {
        if (RenamingEmpire != null)
            RenamingEmpire.Name = name;
        Regenerate();
    }

    void UpdateTooltips(int ClickX, int ClickY)
    {

        Village villageAtCursor = StrategicUtilities.GetVillageAt(new Vec2i(ClickX, ClickY));
        if (villageAtCursor == null)
        {
            VillageTooltip.gameObject.SetActive(false);
            MercenaryHouse house = StrategicUtilities.GetMercenaryHouseAt(new Vec2i(ClickX, ClickY));
            if (house != null)
            {
                VillageTooltip.gameObject.SetActive(true);
                VillageTooltip.Text.text = $"Mercenary House\nMercs: {house.Mercenaries.Count}\nHas Special? {(MercenaryHouse.UniqueMercs.Count > 0 ? "Yes" : "No")}";
            }
            AncientTeleporter tele = StrategicUtilities.GetTeleAt(new Vec2i(ClickX, ClickY));
            if (tele != null)
            {
                VillageTooltip.gameObject.SetActive(true);
                VillageTooltip.Text.text = $"Ancient Teleporter";
            }
            ClaimableBuilding claimable = StrategicUtilities.GetClaimableAt(new Vec2i(ClickX, ClickY));
            if (claimable != null)
            {
                VillageTooltip.gameObject.SetActive(true);
                if (claimable is GoldMine)
                {
                    VillageTooltip.Text.text = $"Gold Mine\nOwner: {claimable.Owner?.Name}\nGold Per Turn: {Config.GoldMineIncome}";
                }

            }
            ConstructibleBuilding constructible = StrategicUtilities.GetConstructibleAt(new Vec2i(ClickX, ClickY));
            if (constructible != null)
            {
                VillageTooltip.gameObject.SetActive(true);
                string owner_name;
                if (constructible.Owner == null)
                    owner_name = "None";
                else
                    owner_name = constructible.Owner.Name;

                VillageTooltip.Text.text = $"{constructible.Name}\nOwner: {owner_name}";

                if (constructible.ruined)
                {
                    VillageTooltip.Text.text += $"\n Building Disabled" ;
                }
                else if (constructible.constructing)
                {
                    VillageTooltip.Text.text += $"\n Constructing in {constructible.turnsToCompletion} turn(s)" ;
                }
                else if (constructible.upgrading)
                {
                    VillageTooltip.Text.text += $"\n Upgrading for {constructible.turnsToUpgrade} turn(s)";
                }

                switch (constructible.buildingType)
                {
                    case ConstructibleType.CasterTower:
                    case ConstructibleType.BarrierTower:
                    case ConstructibleType.DefEncampment:
                    case ConstructibleType.Academy:
                    case ConstructibleType.DarkMagicTower:
                    case ConstructibleType.TemporalTower:
                        buildingRangeOn = true;
                        foreach (var tile in StrategicUtilities.GetTilesInRange(constructible.Position, Config.BuildConfig.BuildingPassiveRange))
                        {
                            TilemapLayers[14].SetTile(new Vector3Int(tile.x, tile.y, 0), BuildIndicator[1]);
                        }
                        break;
                    default:
                        break;
                }
            }

        }
        else
        {
            if ((Config.FogOfWar || State.World.IsNight) && FogSystem.FoggedTile[villageAtCursor.Position.x, villageAtCursor.Position.y])
                return;
            StringBuilder sb = new StringBuilder();
            VillageTooltip.gameObject.SetActive(true);
            sb.AppendLine($"Village: {villageAtCursor.Name}");
            if (villageAtCursor.Capital)
                sb.AppendLine($"Capital City ({villageAtCursor.OriginalRace})");
            sb.AppendLine($"Owner: {villageAtCursor.Empire.Name}");
            sb.AppendLine($"Race: {villageAtCursor.Race}");
            sb.AppendLine($"Happiness : {villageAtCursor.Happiness}");
            if (villageAtCursor.Empire.IsAlly(LastHumanEmpire) || OnlyAIPlayers || Config.CheatExtraStrategicInfo)
            {
                sb.AppendLine($"Garrison: {villageAtCursor.Garrison}");
                sb.AppendLine($"Population: {villageAtCursor.GetTotalPop()}");
                if (villageAtCursor.buildings.Count > 0)
                {
                    string buildings = "";
                    bool first = true;
                    foreach (VillageBuilding building in villageAtCursor.buildings)
                    {
                        if (!first)
                            buildings += ", ";
                        buildings += building;
                        first = false;
                    }
                    sb.AppendLine($"Buildings: {buildings}");
                }
            }
            else
            {
                sb.AppendLine($"Garrison: {SizeToName.ForTroops(villageAtCursor.Garrison)}");
            }
            VillageTooltip.Text.text = sb.ToString();

        }
        Army armyAtCursor = StrategicUtilities.ArmyAt(new Vec2i(ClickX, ClickY));
        if (armyAtCursor == null)
        {
            ArmyTooltip.gameObject.SetActive(false);
        }
        else
        {
            if ((Config.FogOfWar || State.World.IsNight) && FogSystem.FoggedTile[armyAtCursor.Position.x, armyAtCursor.Position.y] || (armyAtCursor.Banner != null && !armyAtCursor.Banner.gameObject.activeSelf))
                return;
            StringBuilder sb = new StringBuilder();
            sb = ArmyToolTip(armyAtCursor);

            ArmyTooltip.gameObject.SetActive(true);
            ArmyTooltip.Text.text = sb.ToString();

            if (armyAtCursor.Destination != null && armyAtCursor.Empire.IsAlly(ActingEmpire) && ActingEmpire.StrategicAI == null)
            {
                currentPathDestination = null;
                ShowPathOfArmy(armyAtCursor);
            }
        }
        if (mouseMovementMode == false && (armyAtCursor == null || armyAtCursor.Destination == null))
        {
            arrowManager.ClearNodes();
        }
    }

    internal StringBuilder ArmyToolTip(Army armyAtCursor)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Empire: {armyAtCursor.Empire.Name}");
        sb.AppendLine($"Army Name: {armyAtCursor.Name}");
        if (armyAtCursor.Empire.IsAlly(LastHumanEmpire) || OnlyAIPlayers || Config.CheatExtraStrategicInfo)
        {
            sb.AppendLine($"Size: {armyAtCursor.Units.Count}");
            sb.AppendLine($"Strength: {SizeToName.ForArmyStrength(StrategicUtilities.ArmyPower(armyAtCursor))}");
            sb.AppendLine($"Average Health: {Mathf.Round(armyAtCursor.GetHealthPercentage())}%");
            if (armyAtCursor.Units.Count > 0)
                sb.AppendLine($"Average Exp: {Math.Round(armyAtCursor.Units.Sum(s => s.Experience) / armyAtCursor.Units.Count())}");
            if (State.World.GetEmpireOfSide(armyAtCursor.Side).StrategicAI != null)
                sb.AppendLine($"Order: {armyAtCursor.AIMode}");
        }
        else
        {
            sb.AppendLine($"Size: {SizeToName.ForTroops(armyAtCursor.Units.Count)}");
            sb.AppendLine($"Strength: {SizeToName.ForArmyStrength(StrategicUtilities.ArmyPower(armyAtCursor))}");
            if (Config.CheatExtraStrategicInfo)
            {
                sb.AppendLine($"Average Health: {Mathf.Round(armyAtCursor.GetHealthPercentage())}%");
                if (armyAtCursor.Units.Count > 0)
                    sb.AppendLine($"Average Exp: {Math.Round(armyAtCursor.Units.Sum(s => s.Experience) / armyAtCursor.Units.Count())}");
            }
        }
        if (Config.CheatExtraStrategicInfo)
        {
            sb.AppendLine($"Est. Power: {Math.Round(StrategicUtilities.ArmyPower(armyAtCursor), 1)}");

        }

        return sb;
    }

    public void ShowTurnReport()
    {
        if (ActingEmpire.Reports.Count > 0)
        {
            subWindowUp = true;
            ReportUI.Generate(ActingEmpire.Reports);
        }
    }

    public void CleanUpLingeringWindows()
    {
        //Done seperately because I want to make sure it triggers only when desired.  
        subWindowUp = false;
        DevourUI.gameObject.SetActive(false);
        TrainUI.gameObject.SetActive(false);
        ReportUI.gameObject.SetActive(false);
        ExchangerUI.gameObject.SetActive(false);
    }

    public override void CleanUp()
    {


    }

    void ClaimWithinXTilesOf(Vec2i pos, int dist)
    {
        for (int x = pos.x - dist; x <= pos.x + dist; x++)
        {
            for (int y = pos.y - dist; y <= pos.y + dist; y++)
            {
                Vec2i claimPos = new Vec2i(x, y);
                if (!ActingEmpire.OwnedTiles.Contains(claimPos))
                    ActingEmpire.OwnedTiles.Add(claimPos);
            }
        }
    }

    public void InitiateBuildMode(ConstructibleType selected)
    {
        SelectedArmy = null;
        BuildMode = true;
        //List<Vec2i> validtiles = ActingEmpire.OwnedTiles;
        List<Vec2i> invalidtiles = new List<Vec2i>();
        SelectedConstruction = selected;
        foreach (var empire in State.World.MainEmpires)
        {
            if (!ActingEmpire.IsAlly(empire))
            {
                invalidtiles.AddRange(empire.OwnedTiles);
            }
        }
        /*
        foreach (var tile in validtiles)
        {
            TilemapLayers[13].SetTile(new Vector3Int(tile.x, tile.y, 0), BuildIndicator[0]);
        }*/
        foreach (var tile in invalidtiles)
        {
            TilemapLayers[13].SetTile(new Vector3Int(tile.x, tile.y, 0), BuildIndicator[1]);
        }
    }
}

