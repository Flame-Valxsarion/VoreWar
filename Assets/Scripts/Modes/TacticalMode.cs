using LegacyAI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TacticalDecorations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Networking.Types;
using UnityEngine.Tilemaps;
using static UnityEngine.UI.CanvasScaler;

public class TacticalMode : SceneBase
{
    enum WallType
    {
        Fence,
        Stone,
        WoodenPallisade,
        Cat,
        Lizard,
        Scylla,
        Bunny,
        Crux,
        Crypter,
        Lamia,
        Fox,
        Imp,
        Slime,
        SlimeExtra,
    }

    enum NextUnitType
    {
        Any,
        Melee,
        Ranged
    }

    WallType wallType;

    List<Actor_Unit> units;
    List<MiscDiscard> miscDiscards;
    List<ClothingDiscards> discardedClothing;
    Army[] armies;
    Village village;

    List<Actor_Unit> RetreatedDigestors;

    internal FogSystemTactical FogSystem;
    public Translator Translator;

    public TacticalTileDictionary TileDictionary;

    public Tilemap FogOfWar;
    public TileBase FogTile;
    public Tilemap Tilemap;
    public Tilemap UnderTilemap;
    public Tilemap FrontTilemap;
    public Tilemap FrontColorTilemap;
    public Tilemap EffectTileMap;
    public Tilemap FrontSpriteTilemap;

    Tile[] BuildingTileTypes;


    public AnimatedTile Pyre;
    public Tile Ice;

    bool[,] BlockedTile;
    bool[,] BlockedClimberTile;

    internal void SetBlockedTiles(bool[,] tiles) => BlockedTile = tiles;

    private Dictionary<Vec2, TileEffect> _activeEffects;

    internal Dictionary<Vec2, TileEffect> ActiveEffects
    {
        get
        {
            if (_activeEffects == null)
                _activeEffects = new Dictionary<Vec2, TileEffect>();
            return _activeEffects;
        }
        set => _activeEffects = value;
    }

    public Transform TerrainFolder;

    public Tilemap MovementGrid;
    public TileBase[] MovementGridTileTypes;

    public Transform SelectionBox;
    public Transform ActorFolder;

    public TacticalStatusPanel StatusUI;
    public UnitInfoPanel InfoUI;
    public SkipBattleUI SkipUI;
    public HirePanel UnitPickerUI;
    public AdvancedUnitCommands CommandsUI;
    public GameObject EnemyTurnText;
    public GameObject AutoAdvanceText;
    public GameObject PausedText;
    public GameObject BattleReviewText;

    public GameObject ArrowPrefab;
    public GameObject HitEffectPrefab;
    public GameObject SkullPrefab;
    public GameObject HandPrefab;

    public GameObject SwipeEffectPrefab;

    public GameObject SpellHelperText;

    public TacticalMessageLog Log;
    public MessageLogPanel LogUI;

    public RightClickMenu RightClickMenu;

    bool manualSkip = false;
    bool blockActive = true;

    public TacticalStats TacticalStats;

    internal InfoPanel InfoPanel;

    TacticalTileType[,] tiles;
    internal TacticalBuildings.TacticalBuilding[] Buildings;

    internal DecorationStorage[] DecorationStorage;
    internal PlacedDecoration[] Decorations;

    int defenderSide;
    int attackerSide; // because sides just got a lot more complex.


    internal bool DirtyPack = true;

    internal bool TacticalLogUpdated;

    internal ChoiceOption FledReturn;
    bool waitingForDialog;

    public bool PseudoTurn = false;
    public bool IgnorePseudo = false;
    public bool SkipPseudo = false;

    internal int currentTurn = 1;

    int lastDiscard = 5;

    int corpseCount = 0;

    bool repeatingTurn = false;
    internal Vector2Int Wins;

    Spell CurrentSpell;
    Potion CurrentPotion;

    bool attackersTurn;
    public bool attackersTurnCheck;
    internal bool IsPlayerTurn;
    internal bool IsPlayerInControl => PseudoTurn || (IsPlayerTurn && RunningFriendlyAI == false && foreignAI == null && !SpectatorMode);
    int activeSide;

    public bool AIAttacker;

    public bool CheatAttackerControl;

    public bool AIDefender;

    public bool CheatDefenderControl;

    internal bool SpectatorMode;

    internal string AttackerName = null;
    internal string DefenderName = null;

    bool paused;

    float remainingLockedPanelTime = 0;

    internal float AITimer;
    ITacticalAI currentAI;
    ITacticalAI attackerAI;
    ITacticalAI defenderAI;
    ITacticalAI foreignAI;

    public double StartingAttackerPower;
    public double StartingDefenderPower;

    internal bool RunningFriendlyAI;

    bool[,] Walkable;

    bool reviewingBattle = false;

    Vec2i currentPathDestination;

    float autoAdvanceTimer;
    bool autoAdvancing;
    const float AutoAdvanceRate = .4f;

    PathNodeManager arrowManager;
    List<PathNode> queuedPath;

    Vec2i startingLocation;
    int startingMP;

    internal bool turboMode;

    public List<Actor_Unit> extraAttackers;
    public List<Actor_Unit> extraDefenders;

    List<Unit> retreatedAttackers;
    List<Unit> retreatedDefenders;

    List<Actor_Unit> garrison;

    internal List<ConstructibleBuilding> attackerBuildingsInRange;
    internal List<ConstructibleBuilding> defenderBuildingsInRange;

    SpecialAction specialType;
    SpecialAction lastSpecial;
    int _mode;
    public int ActionMode
    {
        get
        {
            return _mode;
        }

        set
        {
            if (value != 0 && tiles != null && SelectedUnit != null)
            {
                if (TacticalUtilities.TileContainsMoreThanOneUnit(SelectedUnit.Position.x, SelectedUnit.Position.y))
                {
                    UndoMovement();
                }
            }
            arrowManager.ClearNodes();
            MovementGrid.ClearAllTiles();
            if (value == 0) specialType = SpecialAction.None;
            CommandsUI.SelectorIcon.transform.position = new Vector2(2000f, 2000f);
            if (units != null)
                RemoveHitPercentages();
            switch (value)
            {
                case 1:
                    if (SelectedUnit != null && SelectedUnit.Targetable)
                        ShowMeleeHitPercentages(SelectedUnit);
                    StatusUI.ButtonSelection.transform.position = StatusUI.MeleeButton.transform.position;
                    break;
                case 2:
                    StatusUI.ButtonSelection.transform.position = StatusUI.RangedButton.transform.position;
                    if (SelectedUnit != null && SelectedUnit.Targetable)
                        ShowRangedHitPercentages(SelectedUnit);
                    break;
                case 3:
                    if (SelectedUnit != null && SelectedUnit.Targetable)
                        ShowVoreHitPercentages(SelectedUnit);
                    StatusUI.ButtonSelection.transform.position = StatusUI.VoreButton.transform.position;
                    break;
                case 4:
                    if (SelectedUnit != null && SelectedUnit.Targetable)
                        ShowSpecialHitPercentages(SelectedUnit);
                    StatusUI.ButtonSelection.transform.position = new Vector2(2000f, 2000f);
                    break;
                case 5:
                    CreateMovementGrid();
                    StatusUI.ButtonSelection.transform.position = StatusUI.MovementButton.transform.position;
                    break;
                case 6:
                    if (SelectedUnit != null && SelectedUnit.Targetable)
                        ShowMagicPercentages(SelectedUnit);
                    StatusUI.ButtonSelection.transform.position = new Vector2(2000f, 2000f);
                    break;
                case 7:
                    if (SelectedUnit != null && SelectedUnit.Targetable)
                        ShowPotionThrowPercentages(SelectedUnit);
                    StatusUI.ButtonSelection.transform.position = new Vector2(2000f, 2000f);
                    break;
                default:
                    StatusUI.ButtonSelection.transform.position = new Vector2(2000f, 2000f);
                    break;
            }
            _mode = value;
        }
    }

    internal int GetDefenderSide()
    {
        return defenderSide;
    }

    internal int GetAttackerSide()
    {
        return attackerSide;
    }

    private Actor_Unit _selectedUnit;

    public Actor_Unit SelectedUnit
    {
        get
        {
            return _selectedUnit;
        }

        set
        {
            if (tiles != null && _selectedUnit != null)
            {
                if (TacticalUtilities.TileContainsMoreThanOneUnit(_selectedUnit.Position.x, _selectedUnit.Position.y) || TacticalUtilities.OpenTile(new Vec2i(_selectedUnit.Position.x, _selectedUnit.Position.y), null))
                {
                    UndoMovement();
                }
            }
            _selectedUnit = value;
            if (value != null)
            {
                RebuildInfo();
                PlaceUndoMarker();
            }

        }
    }
    
    private void Start()
    {
        var allSprites = State.GameManager.TacticalBuildingSpriteDictionary.AllSprites;
        BuildingTileTypes = new Tile[allSprites.Length];
        for (int i = 0; i < allSprites.Length; i++)
        {
            BuildingTileTypes[i] = ScriptableObject.CreateInstance<Tile>();
            BuildingTileTypes[i].sprite = allSprites[i];
        }

    }

    void PlaceUndoMarker()
    {
        if (SelectedUnit == null)
            return;
        startingLocation = SelectedUnit.Position;
        startingMP = SelectedUnit.Movement;
    }

    void UndoMovement()
    {
        if (SelectedUnit == null)
            return;
        SelectedUnit.Movement = startingMP;
        Translator.SetTranslator(SelectedUnit.UnitSprite.transform, SelectedUnit.Position, startingLocation, .2f, State.GameManager.TacticalMode.IsPlayerTurn || PseudoTurn);
        SelectedUnit.SetPos(startingLocation);
        DirtyPack = true;
        RebuildInfo();
    }

    internal void ForceUpdate()
    {
        ItemRepository repo = State.World.ItemRepository;
        foreach (Unit unit in armies[0].Units)
        {
            UpdateUnit(unit);
        }
        if (armies[1] != null)
        {
            foreach (Unit unit in armies[1].Units)
            {
                UpdateUnit(unit);
            }
        }
        if (garrison != null)
        {
            foreach (Actor_Unit actor in garrison)
            {
                UpdateUnit(actor.Unit);
            }
        }

        void UpdateUnit(Unit unit)
        {
            unit.UpdateItems(repo);
            unit.UpdateSpells();
            unit.ReloadTraits();
        }
    }


    internal bool TacticalSoundBlocked()
    {
        bool ret = false;
        if (turboMode)
            ret = true;
        else if (autoAdvancing && Config.AutoAdvance == Config.AutoAdvanceType.SkipToEnd)
            ret = true;
        return ret;
    }

    public void Begin(StrategicTileType tiletype, Village village, Army invader, Army defender, TacticalAIType AIinvader, TacticalAIType AIdefender, TacticalBattleOverride tacticalBattleOverride = TacticalBattleOverride.Ignore)
    {
        BattleReviewText.gameObject.SetActive(false);
        CheatAttackerControl = false;
        CheatDefenderControl = false;
        SpectatorMode = false;
        if (arrowManager == null)
            arrowManager = FindObjectOfType<PathNodeManager>();
        if (Config.AutoScaleTactical)
        {
            int tempCount = (village?.Garrison ?? 0) + invader.Units.Count() + (defender?.Units.Count() ?? 0);
            int size = 16 + (int)(2 * (Mathf.Sqrt(tempCount)));
            Config.World.TacticalSizeX = size;
            Config.World.TacticalSizeY = size;
        }

        RetreatedDigestors = new List<Actor_Unit>();

        armies = new Army[2];
        armies[0] = invader;
        armies[1] = defender;
        this.village = village;
        attackersTurn = true;
        attackersTurnCheck = true;

        currentTurn = 1;
        corpseCount = 0;

        units = new List<Actor_Unit>();
        discardedClothing = new List<ClothingDiscards>();
        miscDiscards = new List<MiscDiscard>();

        TacticalMapGenerator mapGen = new TacticalMapGenerator(tiletype, village);
        tiles = mapGen.GenMap(village?.HasWalls() ?? false);

        defenderSide = defender?.Side ?? village.Side;
        attackerSide = invader.Side;



        DefectProcessor defectors = new DefectProcessor(armies[0], armies[1], village);

        //convert armies	

        List<Actor_Unit> attackers = new List<Actor_Unit>();
        List<Actor_Unit> defenders = new List<Actor_Unit>();
        garrison = new List<Actor_Unit>();
        List<Unit> grabbedGarrison = village?.PrepareAndReturnGarrison();
        Unit AttackerLeader = null;
        Unit DefenderLeader = null;
        AttackerLeader = armies[0].LeaderIfInArmy();
        for (int i = 0; i < armies[0].Units.Count; i++)
        {
            Actor_Unit unit = new Actor_Unit(mapGen.RandomActorPosition(tiles, BlockedTile, units, TacticalMapGenerator.SpawnLocation.upper, armies[0].Units[i].GetBestRanged() == null), armies[0].Units[i]);
            units.Add(unit);
            unit.Unit.Side = armies[0].Side;
			unit.InSight = true; // All units visible by default, for daytime
            unit.Unit.CurrentLeader = AttackerLeader;
            attackers.Add(unit);
        }
        if (armies[1] != null)
        {
            DefenderLeader = armies[1].LeaderIfInArmy();
            for (int i = 0; i < armies[1].Units.Count; i++)
            {
                Actor_Unit unit = new Actor_Unit(mapGen.RandomActorPosition(tiles, BlockedTile, units, TacticalMapGenerator.SpawnLocation.lower, armies[1].Units[i].GetBestRanged() == null), armies[1].Units[i]);
                units.Add(unit);
                unit.Unit.Side = defenderSide;
				unit.InSight = true; //All units visible by default, for daytime
                unit.Unit.CurrentLeader = DefenderLeader;
                defenders.Add(unit);
            }
        }
        if (this.village != null && grabbedGarrison != null)
        {
            for (int i = 0; i < grabbedGarrison.Count; i++)
            {
                Actor_Unit unit = new Actor_Unit(mapGen.RandomActorPosition(tiles, BlockedTile, units, TacticalMapGenerator.SpawnLocation.lower, grabbedGarrison[i].GetBestRanged() == null), grabbedGarrison[i]);
                units.Add(unit);
                unit.Unit.Side = defenderSide;
				unit.InSight = true; //All units visible by default, for daytime
                unit.Unit.CurrentLeader = DefenderLeader;
                garrison.Add(unit);
            }
        }

        StartingAttackerPower = StrategicUtilities.UnitValue(armies[0].Units);
        StartingDefenderPower = StrategicUtilities.UnitValue(defenders.Concat(garrison).Select(s => s.Unit).ToList());

        Race defenderRace = defender?.Empire?.ReplacedRace ?? village.Empire?.Race ?? (Race)defenderSide;
        Race attackerRace = invader.Empire?.ReplacedRace ?? (Race)invader.Side;
        if (Config.Defections && !State.GameManager.PureTactical)
        {
            

            foreach (Actor_Unit actor in attackers)
            {
                defectors.AttackerDefectCheck(actor, defenderRace);
            }
            foreach (Actor_Unit actor in defenders)
            {
                defectors.DefenderDefectCheck(actor, attackerRace);
            }
            foreach (Actor_Unit actor in garrison.ToList())
            {
                defectors.GarrisonDefectCheck(actor, attackerRace);
                if (actor.Unit.Side != defenderSide)
                    garrison.Remove(actor);
            }
        }

        extraAttackers = defectors.extraAttackers;
        extraDefenders = defectors.extraDefenders;

        retreatedAttackers = new List<Unit>();
        retreatedDefenders = new List<Unit>();

        foreach (Actor_Unit actor in units)
        {
            actor.Unit.EnemiesKilledThisBattle = 0;
            actor.allowedToDefect = !actor.DefectedThisTurn && TacticalUtilities.GetPreferredSide(actor.Unit, actor.Unit.Side, actor.Unit.Side == attackerSide ? defenderSide : attackerSide) != actor.Unit.Side;
            actor.DefectedThisTurn = false;
            actor.Unit.Heal(actor.Unit.GetLeaderBonus() * 3); // mainly for the new Stat boosts => maxHealth option, but eh why not have it for everyone anyway?
            EquipmentFunctions.CheckEquipment(actor.Unit, EquipmentActivator.OnTacticalBattleStart, new object[] { actor, armies[actor.Unit.Side == attackerSide ? 0 : 1], null });
            EquipmentFunctions.TickCoolDown(actor.Unit, EquipmentType.RechargeTactical, true);  
        }


        int summonedUnits = SummonUnits(mapGen, AttackerLeader, DefenderLeader);
        int antSummonedUnits = SummonAnts(mapGen, AttackerLeader, DefenderLeader);
        int DefCampSummonedUnits = 0;


        activeSide = armies[0].Side;

        TacticalStats = new TacticalStats();
        TacticalStats.SetInitialUnits(armies[0].Units.Count + defectors.newAttackers + defectors.DefectedGarrison,
            armies[1]?.Units.Count ?? 0 + defectors.newDefenders,
            garrison.Count - defectors.DefectedGarrison,
            armies[0].Side, defenderSide);


        AIDefender = AIdefender != TacticalAIType.None;
        AIAttacker = AIinvader != TacticalAIType.None;

        if (AttackerName == null)
            AttackerName = $"{armies[0].Empire?.Name ?? ((Race)armies[0].Side).ToString()}";
        if (DefenderName == null)
            DefenderName = $"{armies[1]?.Empire?.Name ?? village?.Empire?.Name ?? ((Race)defenderSide).ToString()}";

        IsPlayerTurn = !AIAttacker;

        GeneralSetup();

        if ((armies[1]?.Units.Count ?? 0) <= 0 && garrison.Count <= 0)
        {
            VictoryCheck();
            string msg = $"All defenders have defected to rejoin their race, attackers win by default.";
            State.GameManager.CreateMessageBox(msg);
            return;
        }
        if (armies[0].Units.Count <= 0)
        {
            VictoryCheck();
            string msg = $"All attackers have defected to rejoin their race, defenders win by default.";
            State.GameManager.CreateMessageBox(msg);
            return;
        }

        if (AIinvader == TacticalAIType.Legacy)
            attackerAI = new LegacyTacticalAI(units, tiles, armies[0].Side);
        else
        {
            object[] argArray = { units, tiles, armies[0].Side, false };
            RaceAI rai = State.RaceSettings.GetRaceAI(attackerRace);
            attackerAI = Activator.CreateInstance(RaceAIType.Dict[rai], args: argArray) as TacticalAI;
            InitRetreatConditions(attackerAI, attackers, invader.Empire, AIAttacker);
        }

        if (AIdefender == TacticalAIType.Legacy)
            defenderAI = new LegacyTacticalAI(units, tiles, defenderSide);
        else
        {
            object[] argArray = { units, tiles, defenderSide, village != null };
            RaceAI rai = State.RaceSettings.GetRaceAI(defenderRace);
            defenderAI = Activator.CreateInstance(RaceAIType.Dict[rai], args: argArray) as TacticalAI;

            var defenderEmp = defender?.Empire ?? village.Empire;
            InitRetreatConditions(defenderAI, defenders, defenderEmp, AIDefender);

        }

        currentAI = attackerAI;


        Log.RegisterNewTurn(AttackerName, 1);

        bool skip = (!Config.WatchAIBattles || (Config.IgnoreMonsterBattles && armies[0].Side >= 100 && defenderSide >= 100)) && AIAttacker && AIDefender;

        if (tacticalBattleOverride == TacticalBattleOverride.ForceWatch)
            skip = false;
        if (tacticalBattleOverride == TacticalBattleOverride.ForceSkip && AIAttacker && AIDefender)
            skip = true;
        if (units.Any(actor => State.World.AllActiveEmpires != null && State.World.GetEmpireOfSide(actor.Unit.FixedSide)?.StrategicAI == null))
            skip = false;

        ActivatePreBattleTraits();

        if (!State.GameManager.PureTactical)//Apply Empire Traits to units that don't have them at the start of combat
        {
            foreach (Actor_Unit actor in attackers)
            {
                if (State.World.GetEmpireOfSide(actor.Unit.HiddenUnit.Side).EmpTraits != null)
                foreach (Traits trait in State.World.GetEmpireOfSide(actor.Unit.HiddenUnit.Side).EmpTraits)
                {
                    actor.Unit.AddPermanentTrait(trait);
                }
            }
            foreach (Actor_Unit actor in defenders)
            {
                if (State.World.GetEmpireOfSide(actor.Unit.HiddenUnit.Side).EmpTraits != null)
                foreach (Traits trait in State.World.GetEmpireOfSide(actor.Unit.HiddenUnit.Side).EmpTraits)
                {
                    actor.Unit.AddPermanentTrait(trait);
                }
            }
            foreach (Actor_Unit actor in garrison.ToList())
            {
                if (State.World.GetEmpireOfSide(actor.Unit.HiddenUnit.Side).EmpTraits != null)
                foreach (Traits trait in State.World.GetEmpireOfSide(actor.Unit.HiddenUnit.Side).EmpTraits)
                {
                    actor.Unit.AddPermanentTrait(trait);
                }
            }
        }

        if (!State.GameManager.PureTactical)
        {
            foreach (ConstructibleBuilding building in attackerBuildingsInRange)
            {
                if (building is BlackMagicTower)
                {
                    BlackMagicTower darkMagicTower = building as BlackMagicTower;
                    int acc = 5;
                    int durr = 3;
                    int eff = 1 + (darkMagicTower.afflictUpgrade.built ? darkMagicTower.PactLevel : 0);
                    switch (darkMagicTower.Affliction)
                    {
                        case StatusEffectType.Necrosis:
                            if (darkMagicTower.PactLevel >= 1)
                                acc = Config.BuildConfig.DarkMagicTowerAccImprovement;
                            if (darkMagicTower.PactLevel >= 2)
                                durr += Config.BuildConfig.DarkMagicTowerDurationImprovement;
                            break;
                        case StatusEffectType.Errosion:
                            if (darkMagicTower.PactLevel >= 4)
                                acc = Config.BuildConfig.DarkMagicTowerAccImprovement;
                            if (darkMagicTower.PactLevel >= 5)
                                durr += Config.BuildConfig.DarkMagicTowerDurationImprovement;
                            break;
                        case StatusEffectType.Lethargy:
                            if (darkMagicTower.PactLevel >= 7)
                                acc = Config.BuildConfig.DarkMagicTowerAccImprovement;
                            if (darkMagicTower.PactLevel >= 8)
                                durr += Config.BuildConfig.DarkMagicTowerDurationImprovement;
                            break;
                        case StatusEffectType.Agony:
                            if (darkMagicTower.PactLevel >= 10)
                                acc = Config.BuildConfig.DarkMagicTowerAccImprovement;
                            eff = 0;
                            durr *= 2;
                            break;
                        default:
                            break;
                    }
                    foreach (Actor_Unit unit in defenders)
                    {
                        if (acc > State.Rand.Next(20))
                        {
                            unit.Unit.ApplyStatusEffect(darkMagicTower.Affliction, eff, durr);
                        }
                    }
                }
                if (building is BarrierTower)
                {
                    BarrierTower barrierTower = (BarrierTower)building;
                    if (barrierTower.AvailCores <= 0)
                    {
                        continue;
                    }
                    if (barrierTower.CoreProtection && !AIAttacker)
                    {
                        bool shouldskip = false;
                        StartCoroutine(WaitForBuildingInput());
                        var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
                        Action action = delegate ()
                        {
                            shouldskip = true;
                            blockActive = false;
                        };
                        box.SetData(action, "No", "Yes", $"Would you like to activate the barrier tower for this battle?\nAvailable Cores: {barrierTower.AvailCores}\nA {barrierTower.CurrentDowntimeValue} turn downtime will be incured if used.", ()=>blockActive = false);
                        if (shouldskip)
                        {                      
                            continue;
                        }
                    }
                    if (!Config.BuildConfig.BarrierTowerIgnoreDowntime)
                    {
                        if (barrierTower.DowntimeSlot1 <= 0)
                        {
                            barrierTower.DowntimeSlot1 = barrierTower.CurrentDowntimeValue;
                        }
                        else if (barrierTower.DowntimeSlot2 <= 0 && barrierTower.improveUpgrade.built)
                        {
                            barrierTower.DowntimeSlot2 = barrierTower.CurrentDowntimeValue;
                        }
                        else if (barrierTower.DowntimeSlot3 <= 0 && barrierTower.improveUpgrade.built)
                        {
                            barrierTower.DowntimeSlot3 = barrierTower.CurrentDowntimeValue;
                        }
                    }
                    foreach (Actor_Unit unit in attackers)
                    {
                        unit.Unit.RestoreBarrier(Config.BuildConfig.BarrierTowerBaseBarrierStrength * barrierTower.BarrierMagnitude);
                        if (barrierTower.healUpgrade.built && barrierTower.MendingMagnitude > 0)
                        {
                            unit.Unit.ApplyStatusEffect(StatusEffectType.Mending, barrierTower.MendingMagnitude, 4 + barrierTower.MendingMagnitude);
                        }
                        if (barrierTower.buffUpgrade.built && barrierTower.EmpowerMagnitude > 0)
                        {
                            unit.Unit.ApplyStatusEffect(StatusEffectType.Empowered, 3f, barrierTower.EmpowerMagnitude);
                        }
                    }
                }

                if (building is CasterTower)
                {
                    CasterTower casterTower = (CasterTower)building;
                    Unit newUnit = new NPC_unit(10, false, 2, armies[0].Side, Race.Fairies, 0, false);
                    newUnit.Type = UnitType.Summon;
                    newUnit.Name = $"{casterTower.Owner.Name} Tower Mage";
                    Actor_Unit unit = new Actor_Unit(new Vec2i(Config.TacticalSizeX / 2, Config.TacticalSizeY - 1), newUnit);
                    unit.Visible = false;
                    unit.Targetable = false;
                    foreach (var spellCasts in casterTower.spellCasts)
                    {
                        if (casterTower.ManaCharges <= 0)
                        {
                            break;
                        }                        
                        int counter = 0;
                        switch (spellCasts.Key)
                        {
                            case SpellTypes.Fireball:
                                counter = casterTower.spellCasts[spellCasts.Key];
                                while (counter > 0)
                                {
                                    SpellList.Fireball.TryCast(unit, defenders[State.Rand.Next(defenders.Count())]);
                                    newUnit.RestoreMana(100); unit.Movement = 1;
                                    casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBaseChargeCost;
                                    counter--;
                                }
                                break;
                            case SpellTypes.PowerBolt:
                                counter = casterTower.spellCasts[spellCasts.Key];
                                while (counter > 0)
                                {
                                    SpellList.PowerBolt.TryCast(unit, defenders[State.Rand.Next(defenders.Count())]);
                                    newUnit.RestoreMana(100); unit.Movement = 1;
                                    casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBaseChargeCost;
                                    counter--;
                                }
                                break;
                            case SpellTypes.LightningBolt:
                                counter = casterTower.spellCasts[spellCasts.Key];
                                while (counter > 0)
                                {
                                    SpellList.LightningBolt.TryCast(unit, defenders[State.Rand.Next(defenders.Count())]);
                                    newUnit.RestoreMana(100); unit.Movement = 1;
                                    casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBaseChargeCost;
                                    counter--;
                                }
                                break;
                            case SpellTypes.Shield:
                                if (casterTower.buffUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.Shield.TryCast(unit, attackers[State.Rand.Next(attackers.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBuffChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            case SpellTypes.Speed:
                                if (casterTower.buffUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.Speed.TryCast(unit, attackers[State.Rand.Next(attackers.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBuffChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            case SpellTypes.Valor:
                                if (casterTower.buffUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.Valor.TryCast(unit, attackers[State.Rand.Next(attackers.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBuffChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            case SpellTypes.Predation:
                                if (casterTower.buffUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.Predation.TryCast(unit, attackers[State.Rand.Next(attackers.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBuffChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            case SpellTypes.IceBlast:
                                if (casterTower.forceUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.IceBlast.TryCast(unit, defenders[State.Rand.Next(defenders.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBetterTierChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            case SpellTypes.Pyre:
                                if (casterTower.forceUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.Pyre.TryCast(unit, defenders[State.Rand.Next(defenders.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBetterTierChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            case SpellTypes.Flamberge:
                                if (casterTower.forceUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.Flamberge.TryCast(unit, defenders[State.Rand.Next(defenders.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBetterTierChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            case SpellTypes.ForkLightning:
                                if (casterTower.forceUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.ForkLightning.TryCast(unit, defenders[State.Rand.Next(defenders.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBetterTierChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    unit.UnitSprite.GraphicsFolder.gameObject.SetActive(false);
                    unit.UnitSprite.OtherFolder.gameObject.SetActive(false);
                }
            }

            foreach (ConstructibleBuilding building in defenderBuildingsInRange)
            {
                if (building is DefenseEncampment)
                {
                    DefenseEncampment defenseEncampment = (DefenseEncampment)building;
                    int summonCount = (int)Math.Ceiling(attackers.Count() * (Config.BuildConfig.DefenseEncampmentArmyPercentage * (defenseEncampment.unitUpgrade.built ? 1.5f : 1)));
                    while (summonCount > 0 && defenseEncampment.AvailibleDefenders > 0)
                    {
                        Empire empire = armies[1].Empire;
                        float advancedChance = 0.2f * (defenseEncampment.improveUpgrade.built ? 4f : 1);
                        float unitScale = Config.BuildConfig.DefenseEncampmentUnitScale * (defenseEncampment.levelUpgrade.built ? 1.5f : 1);
                        Unit newUnit = new NPC_unit((int)Math.Max(Mathf.Floor(empire.Leader.Level * unitScale),1), advancedChance >= State.Rand.NextDouble(), 2, armies[1].Side, empire.Race, 0, empire.CanVore);
                        newUnit.Type = UnitType.Reinforcement;
                        Actor_Unit unit = new Actor_Unit(mapGen.RandomActorPosition(tiles, BlockedTile, units, TacticalMapGenerator.SpawnLocation.lower, newUnit.GetBestRanged() == null), newUnit);
                        if (defenseEncampment.improveUpgrade.built)
                        {
                            switch (State.Rand.Next(5))
                            {
                                case 0:
                                    newUnit.Items[1] = State.World.ItemRepository.GetItem(ItemType.Helmet);
                                    break;
                                case 1:
                                    newUnit.Items[1] = State.World.ItemRepository.GetItem(ItemType.BodyArmor);
                                    break;
                                case 2:
                                    newUnit.Items[1] = State.World.ItemRepository.GetItem(ItemType.Shoes);
                                    break;
                                case 3:
                                    newUnit.Items[1] = State.World.ItemRepository.GetItem(ItemType.Bolas);
                                    break;
                                case 4:
                                    if (newUnit.GetBestRanged().Range > 2)
                                    {
                                        newUnit.Items[1] = State.World.ItemRepository.GetItem(ItemType.Gloves);
                                    }
                                    else
                                    {
                                        newUnit.Items[1] = State.World.ItemRepository.GetItem(ItemType.Gauntlet);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        unit.UpdateBestWeapons();
                        units.Add(unit);
                        unit.Unit.CurrentLeader = DefenderLeader;
                        DefCampSummonedUnits++;
                        defenseEncampment.AvailibleDefenders--;
                        summonCount--;
                    }
                }
                if (building is BarrierTower)
                {
                    BarrierTower barrierTower = (BarrierTower)building;
                    if (barrierTower.AvailCores <= 0)
                    {
                        continue;
                    }
                    if (barrierTower.CoreProtection && !AIDefender)
                    {
                        bool shouldskip = false;
                        StartCoroutine(WaitForBuildingInput());
                        var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
                        Action action = delegate ()
                        {
                            shouldskip = true;
                            blockActive = false;
                        };
                        box.SetData(action, "No", "Yes", $"Would you like to activate the barrier tower for this battle?\nAvailable Cores: {barrierTower.AvailCores}\nA {barrierTower.CurrentDowntimeValue} turn downtime will be incured if used.", () => blockActive = false);
                        if (shouldskip)
                        {
                            continue;
                        }
                    }
                    if (!Config.BuildConfig.BarrierTowerIgnoreDowntime)
                    {
                        if (barrierTower.DowntimeSlot1 <= 0)
                        {
                            barrierTower.DowntimeSlot1 = barrierTower.CurrentDowntimeValue;
                        }
                        else if (barrierTower.DowntimeSlot2 <= 0 && barrierTower.improveUpgrade.built)
                        {
                            barrierTower.DowntimeSlot2 = barrierTower.CurrentDowntimeValue;
                        }
                        else if (barrierTower.DowntimeSlot3 <= 0 && barrierTower.improveUpgrade.built)
                        {
                            barrierTower.DowntimeSlot3 = barrierTower.CurrentDowntimeValue;
                        }
                    }
                    foreach (Actor_Unit unit in defenders)
                    {
                        unit.Unit.RestoreBarrier(Config.BuildConfig.BarrierTowerBaseBarrierStrength * barrierTower.BarrierMagnitude);
                        if (barrierTower.healUpgrade.built && barrierTower.MendingMagnitude > 0)
                        {
                            unit.Unit.ApplyStatusEffect(StatusEffectType.Mending, barrierTower.MendingMagnitude, 4 + barrierTower.MendingMagnitude);
                        }
                        if (barrierTower.buffUpgrade.built && barrierTower.EmpowerMagnitude > 0)
                        {
                            unit.Unit.ApplyStatusEffect(StatusEffectType.Empowered, 3f, barrierTower.EmpowerMagnitude);
                        }
                    }
                }
                if (building is BlackMagicTower)
                {
                    BlackMagicTower darkMagicTower = building as BlackMagicTower;
                    int acc = 5;
                    int durr = 3;
                    int eff = 1 + (darkMagicTower.afflictUpgrade.built ? darkMagicTower.PactLevel : 0);
                    switch (darkMagicTower.Affliction)
                    {
                        case StatusEffectType.Necrosis:
                            if (darkMagicTower.PactLevel >= 1)
                                acc = Config.BuildConfig.DarkMagicTowerAccImprovement;
                            if (darkMagicTower.PactLevel >= 2)
                                durr += Config.BuildConfig.DarkMagicTowerDurationImprovement;
                            break;
                        case StatusEffectType.Errosion:
                            if (darkMagicTower.PactLevel >= 4)
                                acc = Config.BuildConfig.DarkMagicTowerAccImprovement;
                            if (darkMagicTower.PactLevel >= 5)
                                durr += Config.BuildConfig.DarkMagicTowerDurationImprovement;
                            break;
                        case StatusEffectType.Lethargy:
                            if (darkMagicTower.PactLevel >= 7)
                                acc = Config.BuildConfig.DarkMagicTowerAccImprovement;
                            if (darkMagicTower.PactLevel >= 8)
                                durr += Config.BuildConfig.DarkMagicTowerDurationImprovement;
                            break;
                        case StatusEffectType.Agony:
                            if (darkMagicTower.PactLevel >= 10)
                                acc = Config.BuildConfig.DarkMagicTowerAccImprovement;
                            eff = 0;
                            break;
                        default:
                            break;
                    }
                    foreach (Actor_Unit unit in attackers)
                    {
                        if (acc > State.Rand.Next(20))
                        {
                            unit.Unit.ApplyStatusEffect(darkMagicTower.Affliction, eff, durr);
                        }
                    }
                }
                if (building is CasterTower)
                {
                    CasterTower casterTower = (CasterTower)building;
                    Unit newUnit = new NPC_unit(10, false, 2, armies[1].Side, Race.Fairies, 0, false);
                    newUnit.Type = UnitType.Summon;
                    newUnit.Name = $"{casterTower.Owner.Name} Tower Mage";
                    Actor_Unit unit = new Actor_Unit(new Vec2i(Config.TacticalSizeX / 2, 0), newUnit);
                    unit.Visible = false;
                    unit.Targetable = false;
                    foreach (var spellCasts in casterTower.spellCasts)
                    {
                        if (casterTower.ManaCharges <= 0)
                        {
                            break;
                        }
                        casterTower.ManaCharges--;
                        int counter = 0;
                        switch (spellCasts.Key)
                        {
                            case SpellTypes.Fireball:
                                counter = casterTower.spellCasts[spellCasts.Key];
                                while (counter > 0)
                                {
                                    SpellList.Fireball.TryCast(unit, attackers[State.Rand.Next(attackers.Count())]);
                                    newUnit.RestoreMana(100); unit.Movement = 1;
                                    casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBaseChargeCost;
                                    counter--;
                                }
                                break;
                            case SpellTypes.PowerBolt:
                                counter = casterTower.spellCasts[spellCasts.Key];
                                while (counter > 0)
                                {
                                    SpellList.PowerBolt.TryCast(unit, attackers[State.Rand.Next(attackers.Count())]);
                                    newUnit.RestoreMana(100); unit.Movement = 1;
                                    casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBaseChargeCost;
                                    counter--;
                                }
                                break;
                            case SpellTypes.LightningBolt:
                                counter = casterTower.spellCasts[spellCasts.Key];
                                while (counter > 0)
                                {
                                    SpellList.LightningBolt.TryCast(unit, attackers[State.Rand.Next(attackers.Count())]);
                                    newUnit.RestoreMana(100); unit.Movement = 1;
                                    casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBaseChargeCost;
                                    counter--;
                                }
                                break;
                            case SpellTypes.Shield:
                                if (casterTower.buffUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.Shield.TryCast(unit, defenders[State.Rand.Next(defenders.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBuffChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            case SpellTypes.Speed:
                                if (casterTower.buffUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.Speed.TryCast(unit, defenders[State.Rand.Next(defenders.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBuffChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            case SpellTypes.Valor:
                                if (casterTower.buffUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.Valor.TryCast(unit, defenders[State.Rand.Next(defenders.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBuffChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            case SpellTypes.Predation:
                                if (casterTower.buffUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.Predation.TryCast(unit, defenders[State.Rand.Next(defenders.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBuffChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            case SpellTypes.IceBlast:
                                if (casterTower.forceUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.IceBlast.TryCast(unit, attackers[State.Rand.Next(attackers.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBetterTierChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            case SpellTypes.Pyre:
                                if (casterTower.forceUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.Pyre.TryCast(unit, attackers[State.Rand.Next(attackers.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBetterTierChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            case SpellTypes.Flamberge:
                                if (casterTower.forceUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.Flamberge.TryCast(unit, attackers[State.Rand.Next(attackers.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBetterTierChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            case SpellTypes.ForkLightning:
                                if (casterTower.forceUpgrade.built)
                                {
                                    counter = casterTower.spellCasts[spellCasts.Key];
                                    while (counter > 0)
                                    {
                                        SpellList.ForkLightning.TryCast(unit, attackers[State.Rand.Next(attackers.Count())]);
                                        newUnit.RestoreMana(100); unit.Movement = 1;
                                        casterTower.ManaCharges -= Config.BuildConfig.CasterTowerBetterTierChargeCost;
                                        counter--;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    unit.UnitSprite.GraphicsFolder.gameObject.SetActive(false);
                    unit.UnitSprite.OtherFolder.gameObject.SetActive(false);
                }
            }
        }       

        if (skip)
        {
            TurboMode();
        }
        else
        {
            turboMode = false;
            State.Save($"{State.SaveDirectory}Autosave_Battle.sav");
            defectors.DefectReport();
            if (summonedUnits > 0 || antSummonedUnits > 0 || DefCampSummonedUnits > 0)
            {
                string message = "";
                if (summonedUnits > 0)
                    message += $"{summonedUnits} units were summoned by astral call\n";
                if (antSummonedUnits > 0)
                    message += $"{antSummonedUnits} units were summoned by ant pheromones\n";
                if (DefCampSummonedUnits > 0)
                    message += $"{DefCampSummonedUnits} units join as reinforcements";
                if (AIDefender && AIAttacker)
                    State.GameManager.CreateMessageBox(message, 4);
                else
                    State.GameManager.CreateMessageBox(message);
            }
        }

    }

    private void InitRetreatConditions(ITacticalAI AI, List<Actor_Unit> fighters, Empire empire, bool nonPlayer)
    {
        if (State.GameManager.PureTactical == false && Config.NoAIRetreat == false)
        {
            if (empire != null && empire?.Race == Race.Vagrants)
            {
                AI.RetreatPlan = new TacticalAI.RetreatConditions(.2f, fighters.Count + 2);
            }
            if (empire != null && empire?.ReplacedRace == Race.FeralLions)
            {
                AI.RetreatPlan = new TacticalAI.RetreatConditions(0, fighters.Count * 3, 0.9f);
            }
            else if (empire != null && empire is MonsterEmpire)
            {
                if (fighters.Where(s => s.Unit.HasTrait(Traits.EvasiveBattler)).Count() / (float)fighters.Count > .8f) //If more than 80% has fast flee
                    AI.RetreatPlan = new TacticalAI.RetreatConditions(.05f, 0);
                else
                    AI.RetreatPlan = new TacticalAI.RetreatConditions(.025f, 0);
            }
            else if (nonPlayer) //Don't set retreat for players
            {
                if (fighters.Where(s => s.Unit.HasTrait(Traits.EvasiveBattler)).Count() / (float)fighters.Count > .8f) //If more than 80% has fast flee
                    AI.RetreatPlan = new TacticalAI.RetreatConditions(.3f, 0);
                else
                    AI.RetreatPlan = new TacticalAI.RetreatConditions(.15f, 0);
            }
        }
    }

    private int SummonUnits(TacticalMapGenerator mapGen, Unit AttackerLeader, Unit DefenderLeader)
    {
        int summonedUnits = 0;
        int attackerSummoners = 0;
        int defenderSummoners = 0;
        int defenderLevels = 0;
        int attackerLevels = 0;
        bool attackerPred = false;
        bool defenderPred = false;
        List<Race> attackerRaces = new List<Race>();
        List<Race> defenderRaces = new List<Race>();
        foreach (Actor_Unit actor in units.ToList())
        {


            if (actor.Unit.HasTrait(Traits.AstralCall))
            {
                if (actor.Unit.Side == defenderSide)
                {
                    defenderSummoners += 1;
                    defenderLevels += actor.Unit.Level;
                    defenderPred = actor.Unit.Predator;
                    defenderRaces.Add(actor.Unit.Race);
                }
                else
                {
                    attackerSummoners += 1;
                    attackerLevels += actor.Unit.Level;
                    attackerPred = actor.Unit.Predator;
                    attackerRaces.Add(actor.Unit.Race);
                }
            }
        }

        if (attackerSummoners > 0)
        {
            int summons = attackerSummoners / 8;
            float extraFraction = attackerSummoners % 8 / 8f;
            if (State.Rand.NextDouble() < extraFraction)
                summons += 1;
            for (int i = 0; i < summons; i++)
            {
                Race race = attackerRaces[State.Rand.Next(attackerRaces.Count())];
                attackerRaces.Remove(race);
                Unit newUnit = new NPC_unit((int)Mathf.Max((0.9f * attackerLevels / attackerSummoners) - 2, 1), false, 2, armies[0].Side, race, 0, attackerPred);
                newUnit.Type = UnitType.Summon;
                Actor_Unit unit = new Actor_Unit(mapGen.RandomActorPosition(tiles, BlockedTile, units, TacticalMapGenerator.SpawnLocation.upperMiddle, newUnit.GetBestRanged() == null), newUnit);
                units.Add(unit);
                summonedUnits++;
                unit.Unit.CurrentLeader = AttackerLeader;
            }
        }

        if (defenderSummoners > 0)
        {
            int summons = defenderSummoners / 8;
            float extraFraction = defenderSummoners % 8 / 8f;
            if (State.Rand.NextDouble() < extraFraction)
                summons += 1;
            for (int i = 0; i < summons; i++)
            {
                Race race = defenderRaces[State.Rand.Next(defenderRaces.Count())];
                defenderRaces.Remove(race);
                Unit newUnit = new NPC_unit((int)Mathf.Max((0.9f * defenderLevels / defenderSummoners) - 2, 1), false, 2, defenderSide, race, 0, defenderPred);
                newUnit.Type = UnitType.Summon;
                Actor_Unit unit = new Actor_Unit(mapGen.RandomActorPosition(tiles, BlockedTile, units, TacticalMapGenerator.SpawnLocation.lowerMiddle, newUnit.GetBestRanged() == null), newUnit);
                units.Add(unit);
                summonedUnits++;
                unit.Unit.CurrentLeader = DefenderLeader;
            }
        }




        return summonedUnits;
    }

    private int SummonAnts(TacticalMapGenerator mapGen, Unit AttackerLeader, Unit DefenderLeader)
    {
        int summonedUnits = 0;
        int attackerSummoners = 0;
        int defenderSummoners = 0;
        int defenderLevels = 0;
        int attackerLevels = 0;
        bool attackerPred = false;
        bool defenderPred = false;
        foreach (Actor_Unit actor in units.ToList())
        {


            if (actor.Unit.HasTrait(Traits.AntPheromones))
            {
                if (actor.Unit.Side == defenderSide)
                {
                    defenderSummoners += 1;
                    defenderLevels += actor.Unit.Level;
                    defenderPred = actor.Unit.Predator;
                }
                else
                {
                    attackerSummoners += 1;
                    attackerLevels += actor.Unit.Level;
                    attackerPred = actor.Unit.Predator;
                }
            }
        }

        if (attackerSummoners > 0)
        {
            int summons = attackerSummoners / 4;
            float extraFraction = attackerSummoners % 4 / 4f;
            if (State.Rand.NextDouble() < extraFraction)
                summons += 1;
            for (int i = 0; i < summons; i++)
            {
                Race race;
                int level = (int)Mathf.Max((0.4f * attackerLevels / attackerSummoners) - 2, 1);
                if (State.Rand.Next(3) == 0)
                    race = Race.WarriorAnts;
                else
                    race = Race.FeralAnts;
                Unit newUnit = new NPC_unit(level, false, 2, armies[0].Side, race, 0, attackerPred);
                newUnit.Type = UnitType.Summon;
                Actor_Unit unit = new Actor_Unit(mapGen.RandomActorPosition(tiles, BlockedTile, units, TacticalMapGenerator.SpawnLocation.upperMiddle, newUnit.GetBestRanged() == null), newUnit);
                units.Add(unit);
                summonedUnits++;
                unit.Unit.CurrentLeader = AttackerLeader;
            }
        }

        if (defenderSummoners > 0)
        {
            int summons = defenderSummoners / 4;
            float extraFraction = defenderSummoners % 4 / 4f;
            if (State.Rand.NextDouble() < extraFraction)
                summons += 1;
            for (int i = 0; i < summons; i++)
            {
                Race race;
                int level = (int)Mathf.Max((0.4f * defenderLevels / defenderSummoners) - 2, 1);
                if (State.Rand.Next(3) == 0)
                    race = Race.WarriorAnts;
                else
                    race = Race.FeralAnts;
                Unit newUnit = new NPC_unit(level, false, 2, defenderSide, race, 0, defenderPred);
                newUnit.Type = UnitType.Summon;
                Actor_Unit unit = new Actor_Unit(mapGen.RandomActorPosition(tiles, BlockedTile, units, TacticalMapGenerator.SpawnLocation.lowerMiddle, newUnit.GetBestRanged() == null), newUnit);
                units.Add(unit);
                summonedUnits++;
                unit.Unit.CurrentLeader = DefenderLeader;
            }
        }




        return summonedUnits;
    }

    internal void DisableAttackerAI()
    {
        AIAttacker = false;
        CheatAttackerControl = true;
    }
    internal void DisableDefenderAI()
    {
        AIDefender = false;
        CheatDefenderControl = true;
    }

    internal void ClearNames()
    {
        AttackerName = null;
        DefenderName = null;
    }

    internal void RefreshPureTacticalTraits()
    {
        foreach (Actor_Unit unit in units)
        {
            unit.Unit.ReloadTraits();
            unit.Unit.InitializeTraits();
        }
        ForceUpdate();
    }

    internal int GetNextCorpseLayer()
    {
        corpseCount += 1;
        return -20020 + (20 * corpseCount);
    }

    private void TurboMode()
    {
        float time = Time.realtimeSinceStartup;
        turboMode = true;

        RefreshAIIfNecessary();

        while (!VictoryCheck() && currentTurn < 2000)
        {
            if (waitingForDialog)
                return;
            if (DirtyPack)
                UpdateAreaTraits();
            if (currentAI.RunAI() == false)
            {
                EndTurn();
            }
        }
        if (currentTurn >= 2000)
            turboMode = false;
        if (State.GameManager.CurrentScene == State.GameManager.TacticalMode && State.GameManager.StatScreen.gameObject.activeSelf == false)
            Log.RefreshListing();
        if (Time.realtimeSinceStartup - time > .5f)
            Debug.Log($"{AttackerName} vs {DefenderName} - {Time.realtimeSinceStartup - time} - {currentTurn}");
        if (State.Warned == false && Time.realtimeSinceStartup - time > 4f)
        {
            State.Warned = true;
            State.GameManager.CreateMessageBox($@"Just had a quick simulated battle take more than 4 seconds to resolve (I.e. one your settings are set to not show).
Smaller army sizes will be faster to process, so if the wait bothers you try playing with smaller max armies.
This warning will only appear once per session.  

Misc Info:
Battle took {System.Math.Round(Time.realtimeSinceStartup - time, 2)} seconds
{units.Count()} Total units {AttackerName} vs {DefenderName}.
Turns: {currentTurn}
");


        }
    }

    private void RefreshAIIfNecessary()
    {
        List<Actor_Unit> fighters = units.Where(actor => actor.Targetable == true && actor.Unit.Side == activeSide && actor.Movement > 0).ToList();
        Actor_Unit nextUnit = fighters.Count() > 0 ? fighters[0] : null;
        Type desiredAIType;
        if (nextUnit != null)
        {
            desiredAIType = TacticalUtilities.GetMindControlSide(nextUnit.Unit) != -1 ? GetAITypeForMindControledUnit(nextUnit.Unit) : RaceAIType.Dict[State.RaceSettings.GetRaceAI(nextUnit.Unit.Race)];
        }
        else
            desiredAIType = typeof(StandardTacticalAI);
        if (currentAI == null || (currentAI.GetType() != desiredAIType))
        {
            object[] argArray = { units, tiles, activeSide, false };
            currentAI = Activator.CreateInstance(desiredAIType, args: argArray) as TacticalAI;
            InitRetreatConditions(currentAI, fighters, State.World.GetEmpireOfSide(activeSide), true);
            if (attackersTurn)
            {
                attackerAI = currentAI;
            }
            else
            {
                defenderAI = currentAI;
            }
        }
    }

    internal void LoadData(TacticalData data)
    {
        RetreatedDigestors = new List<Actor_Unit>();
        if (arrowManager == null)
            arrowManager = FindObjectOfType<PathNodeManager>();
        Import(data);
        if (Config.AutoScaleTactical)
        {
            Config.World.TacticalSizeX = tiles.GetUpperBound(0) + 1;
            Config.World.TacticalSizeY = tiles.GetUpperBound(1) + 1;
        }
        if (miscDiscards == null)
            miscDiscards = new List<MiscDiscard>();
        Unit AttackerLeader = armies[0].LeaderIfInArmy();
        Unit DefenderLeader = null;
        ItemRepository newRepo = new ItemRepository();
        State.World.ItemRepository = newRepo;
        if (armies[1] != null) DefenderLeader = armies[1].LeaderIfInArmy();
        foreach (Actor_Unit actor in units)
        {
            if (actor.Unit.Side == defenderSide)
                actor.Unit.CurrentLeader = DefenderLeader;
            else
                actor.Unit.CurrentLeader = AttackerLeader;
            actor.Unit.ReloadTraits();

        }
        foreach (Actor_Unit unit in units)
        {
            unit.PredatorComponent?.UpdateAlivePrey();
        }
        GeneralSetup();
        Log.RefreshListing();
        TacticalUtilities.UpdateVersion();
        if (State.TutorialMode)
            State.GameManager.TutorialScript.InitializeTactical(units);
        foreach (var actor in units)
        {
            if (actor.Fled && actor.PredatorComponent?.PreyCount > 0)
                RetreatedDigestors.Add(actor);
        }
        if (!Config.WatchAIBattles && AIAttacker && AIDefender)
        {
            TurboMode();
        }
        else
        {
            turboMode = false;
        }
        if (Config.VisibleCorpses)
        {
            foreach (Actor_Unit actor in units)
            {
                if (actor.Targetable == false && actor.Visible == true && actor.Surrendered)
                {
                    float angle = 40 + State.Rand.Next(280);
                    if (actor.UnitSprite != null) actor.UnitSprite.transform.rotation = Quaternion.Euler(0, 0, angle);
                    corpseCount += 1;
                }
            }
        }

    }

    void GeneralSetup()
    {
        if (Log == null)
        {
            Log = new TacticalMessageLog();
        }

        LogUI.SetBase();

        DirtyPack = true;
        paused = false;
        AITimer = 0;
        RebuildDecorations();
        RebuildBlockedTiles();

        if (village != null && State.World.MainEmpires != null)
        {
            FrontColorTilemap.color = State.World.GetEmpireOfSide(village.Side)?.UnityColor ?? Color.white;
        }

        FledReturn = ChoiceOption.Default;
        waitingForDialog = false;


        reviewingBattle = false;

        if (village != null)
        {
            switch (village.Race)
            {
                case Race.Taurus:
                case Race.Kangaroos:
                    wallType = WallType.WoodenPallisade;
                    break;
                case Race.Cats:
                    wallType = WallType.Cat;
                    break;
                case Race.Lizards:
                    wallType = WallType.Lizard;
                    break;
                case Race.Scylla:
                    wallType = WallType.Scylla;
                    break;
                case Race.Bunnies:
                    wallType = WallType.Bunny;
                    break;
                case Race.Crux:
                    wallType = WallType.Crux;
                    break;
                case Race.Crypters:
                    wallType = WallType.Crypter;
                    break;
                case Race.Lamia:
                    wallType = WallType.Lamia;
                    break;
                case Race.Youko:
                case Race.Foxes:
                    wallType = WallType.Fox;
                    break;
                case Race.Imps:
                    wallType = WallType.Imp;
                    break;
                case Race.Slimes:
                    wallType = WallType.Slime;
                    break;
                case Race.Gnolls:
                    wallType = WallType.WoodenPallisade;
                    break;
                default:
                    wallType = WallType.Stone;
                    break;
            }
        }

        RebuildInfo();

        InfoPanel = new InfoPanel(InfoUI);
        Translator = new Translator();
        UpdateEndTurnButtonText();
        StatusUI.EndTurn.interactable = IsPlayerTurn;
        manualSkip = false;
        StatusUI.SkipToEndButton.interactable = true;
        EnemyTurnText.SetActive(!IsPlayerTurn);



        RedrawTiles();
        CreateActors();

        ActionMode = 0;
        string attackerController = AIAttacker == false ? "Player(Atk)" : "AI(Atk)";
        string defenderController = AIDefender == false ? "Player(Def)" : "AI(Def)";
        StatusUI.AttackerText.text = $"{AttackerName} - {attackerController}";
        StatusUI.DefenderText.text = $"{DefenderName} - {defenderController}";

        if (attackersTurn)
        {
            StatusUI.AttackerText.fontStyle = FontStyle.Bold;
            StatusUI.DefenderText.fontStyle = FontStyle.Normal;
        }
        else
        {
            StatusUI.AttackerText.fontStyle = FontStyle.Normal;
            StatusUI.DefenderText.fontStyle = FontStyle.Bold;
        }

        TacticalUtilities.ResetData(armies, village, units, garrison, tiles, BlockedTile, BlockedClimberTile);
    }

    void RebuildDecorations()
    {
        if (DecorationStorage == null)
        {
            Decorations = new PlacedDecoration[0];
            return;
        }
        Decorations = new PlacedDecoration[DecorationStorage.Length];
        for (int i = 0; i < Decorations.Length; i++)
        {
            Decorations[i] = new PlacedDecoration(DecorationStorage[i].Position, TacticalDecorationList.DecDict[DecorationStorage[i].Type]);
        }

    }

    void RebuildBlockedTiles()
    {
        BlockedTile = new bool[tiles.GetUpperBound(0) + 1, tiles.GetUpperBound(1) + 1];
        BlockedClimberTile = new bool[tiles.GetUpperBound(0) + 1, tiles.GetUpperBound(1) + 1];
        if (Buildings != null)
        {
            foreach (var building in Buildings)
            {
                if (building == null)
                    continue;
                for (int y = 0; y < building.Height; y++)
                {
                    for (int x = 0; x < building.Width; x++)
                    {
                        BlockedTile[building.LowerLeftPosition.x + x, building.LowerLeftPosition.y + y] = true;
                        BlockedClimberTile[building.LowerLeftPosition.x + x, building.LowerLeftPosition.y + y] = true;
                    }
                }
            }
        }

        if (Decorations != null)
        {
            foreach (var decoration in Decorations)
            {
                if (decoration == null)
                    continue;
                if (decoration.TacDec == null)
                    continue;
                for (int y = 0; y < decoration.TacDec.Height; y++)
                {
                    for (int x = 0; x < decoration.TacDec.Width; x++)
                    {
                        if (decoration.LowerLeftPosition.x + x < BlockedTile.GetLength(0) && decoration.LowerLeftPosition.y + y < BlockedTile.GetLength(1))
                        {
                            BlockedTile[decoration.LowerLeftPosition.x + x, decoration.LowerLeftPosition.y + y] = true;
                            if (decoration.TacDec.PathType != PathType.Tree)
                                BlockedClimberTile[decoration.LowerLeftPosition.x + x, decoration.LowerLeftPosition.y + y] = true;
                        }
                    }
                }
            }
        }
    }

    internal TacticalData Export()
    {
        TacticalData data = new TacticalData
        {
            units = units,
            armies = armies,
            village = village,

            buildings = Buildings,

            tiles = tiles,
            decorationStorage = DecorationStorage,

            garrison = garrison,

            selectedUnit = SelectedUnit,

            activeEffects = ActiveEffects,

            defenderSide = defenderSide,

            attackersTurn = attackersTurn,
            isAPlayerTurn = IsPlayerTurn,
            activeSide = activeSide,

            AIAttacker = AIAttacker,
            AIDefender = AIDefender,

            StartingAttackerPower = StartingAttackerPower,
            StartingDefenderPower = StartingDefenderPower,


            AttackerName = AttackerName,
            DefenderName = DefenderName,

            currentTurn = currentTurn,

            currentAI = currentAI,
            attackerAI = attackerAI,
            defenderAI = defenderAI,
            TacticalStats = TacticalStats,

            extraAttackers = extraAttackers,
            extraDefenders = extraDefenders,
            retreatedAttackers = retreatedAttackers,
            retreatedDefenders = retreatedDefenders,

            DiscardedClothing = discardedClothing,
            MiscDiscards = miscDiscards,
            LastDiscard = lastDiscard,

            log = Log,

            runningFriendlyAI = RunningFriendlyAI
        };
        return data;
    }

    void Import(TacticalData data)
    {
        units = data.units;
        armies = data.armies;
        village = data.village;

        Buildings = data.buildings;

        garrison = data.garrison;

        tiles = data.tiles;
        DecorationStorage = data.decorationStorage;

        ActiveEffects = data.activeEffects;

        SelectedUnit = data.selectedUnit;

        defenderSide = data.defenderSide;

        currentTurn = data.currentTurn;

        attackersTurn = data.attackersTurn;
        attackersTurnCheck = data.attackersTurn;
        IsPlayerTurn = data.isAPlayerTurn;
        activeSide = data.activeSide;

        AIAttacker = data.AIAttacker;
        AIDefender = data.AIDefender;

        StartingAttackerPower = data.StartingAttackerPower;
        StartingDefenderPower = data.StartingDefenderPower;

        AttackerName = data.AttackerName;
        DefenderName = data.DefenderName;

        currentAI = data.currentAI;
        attackerAI = data.attackerAI;
        defenderAI = data.defenderAI;
        TacticalStats = data.TacticalStats;

        miscDiscards = data.MiscDiscards;
        discardedClothing = data.DiscardedClothing;
        lastDiscard = data.LastDiscard;


        extraAttackers = data.extraAttackers;
        extraDefenders = data.extraDefenders;
        retreatedAttackers = data.retreatedAttackers;
        retreatedDefenders = data.retreatedDefenders;

        if (retreatedAttackers == null)
            retreatedAttackers = new List<Unit>();
        if (retreatedDefenders == null)
            retreatedDefenders = new List<Unit>();

        Log = data.log;

        RunningFriendlyAI = data.runningFriendlyAI;
    }

    void UpdateAreaTraits()
    {

        for (int i = 0; i < units.Count; i++)
        {
            units[i].Intimidated = false;
            units[i].Unit.Harassed = false;
            units[i].Unit.NearbyFriendlies = 0;
            units[i].Unit.NearbyEnemies = 0;
        }

        for (int i = 0; i < units.Count; i++)
        {
            if (units[i].Targetable == false)
                continue;

            for (int j = i + 1; j < units.Count; j++)
            {
                if (units[j].Targetable == false)
                    continue;

                if (units[i].Position.GetNumberOfMovesDistance(units[j].Position) == 1)
                {
                    if (units[i].Unit.Side == units[j].Unit.Side && units[i].Surrendered == false && units[j].Surrendered == false)
                    {
                        units[i].Unit.NearbyFriendlies++;
                        units[j].Unit.NearbyFriendlies++;
                    }
                    else
                    {
                        ApplyEnemyTags(units[i], units[j]);
                        ApplyEnemyTags(units[j], units[i]);
                        units[i].Unit.NearbyEnemies++;
                        units[j].Unit.NearbyEnemies++;
                    }
                }
            }
        }
        void ApplyEnemyTags(Actor_Unit source, Actor_Unit target)
        {
            if (source.Unit.HasTrait(Traits.Intimidating))
                target.Intimidated = true;
            if (source.Unit.HasTrait(Traits.TentacleHarassment))
                target.Unit.Harassed = true;
        }

        DirtyPack = false;
    }

    internal void CreateBloodHitEffect(Vector2 location)
    {
        if (turboMode == false)
            Instantiate(HitEffectPrefab, location, new Quaternion());
    }

    internal void CreateSwipeHitEffect(Vector2 location, int angle = 0)
    {
        if (turboMode == false)
        {
            Quaternion quat = Quaternion.Euler(0, 0, angle);
            Instantiate(SwipeEffectPrefab, location, quat);
        }

    }


    void RedrawTiles()
    {
        Tilemap.ClearAllTiles();
        UnderTilemap.ClearAllTiles();
        FrontTilemap.ClearAllTiles();
        FogOfWar.ClearAllTiles();
        FrontColorTilemap.ClearAllTiles();
        FrontSpriteTilemap.ClearAllTiles();
        EffectTileMap.ClearAllTiles();
        int children = TerrainFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(TerrainFolder.GetChild(i).gameObject);
        }
        for (int i = 0; i <= tiles.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= tiles.GetUpperBound(1); j++)
            {
                UnderTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassEnviroment[0]);
                switch (tiles[i, j])
                {
                    case TacticalTileType.wall:
                        if (wallType != WallType.Fence)
                        {
                            int startIndex = (int)TacticalTileType.WallStart + (((int)(wallType) - 1) * 4);
                            if (i < tiles.GetUpperBound(0) - 2 && tiles[i + 1, j] != TacticalTileType.wall)
                                FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.TileTypes[startIndex + 2]);
                            else if (i > 0 && tiles[i - 1, j] != TacticalTileType.wall)
                                FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.TileTypes[startIndex + 3]);
                            else
                                FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.TileTypes[startIndex + State.Rand.Next(2)]);
                            if ((tiles[i, j + 1] >= (TacticalTileType)500 && tiles[i, j + 1] < (TacticalTileType)600) || (tiles[i, j + 1] >= (TacticalTileType)2300 && tiles[i, j + 1] < (TacticalTileType)2400))
                                Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.VolcanicTileTypes[1]);
                            else if ((tiles[i, j+1] >= (TacticalTileType)200 && tiles[i, j + 1] < (TacticalTileType)300) || (tiles[i, j + 1] >= (TacticalTileType)2000 && tiles[i, j + 1] < (TacticalTileType)2200))
                                Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.DesertTileTypes[1]);
                            else
                                Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassEnviroment[0]);
                        }
                        else
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.TileTypes[(int)TacticalTileType.wall]);
                        }
                        break;
                    default:
                        if (tiles[i, j] >= (TacticalTileType)2400)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.VolcanicOverLava[(int)tiles[i, j] - 2400]);
                        }
                        else if (tiles[i, j] >= (TacticalTileType)2300)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.VolcanicOverGravel[(int)tiles[i, j] - 2300]);
                        }
                        else if (tiles[i, j] >= (TacticalTileType)2200)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassOverWater[(int)tiles[i, j] - 2200]);
                        }
                        else if (tiles[i, j] >= (TacticalTileType)2100)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.RocksOverTar[(int)tiles[i, j] - 2100]);
                        }
                        else if (tiles[i, j] >= (TacticalTileType)2000)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.RocksOverSand[(int)tiles[i, j] - 2000]);
                        }
                        else if (tiles[i, j] >= (TacticalTileType)500)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.VolcanicTileTypes[(int)tiles[i, j] - 500]);
                        }
                        else if (tiles[i, j] >= (TacticalTileType)400)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.SnowEnviroment[(int)tiles[i, j] - 400]);
                        }
                        else if (tiles[i, j] >= (TacticalTileType)300)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.Paths[(int)tiles[i, j] - 300]);
                        }
                        else if (tiles[i, j] >= (TacticalTileType)200)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.DesertTileTypes[(int)tiles[i, j] - 200]);
                        }
                        else if (tiles[i, j] >= TacticalTileType.greengrass)
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassEnviroment[(int)tiles[i, j] - 100]);
                        }
                        else
                        {
                            Tilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.TileTypes[(int)tiles[i, j]]);
                        }

                        break;
                }

            }
        }

        if (Decorations != null)
        {
            foreach (var decoration in Decorations)
            {
                for (int x = 0; x < decoration.TacDec.Tile.GetLength(0); x++)
                {
                    for (int y = 0; y < decoration.TacDec.Tile.GetLength(1); y++)
                    {
                        int i = decoration.LowerLeftPosition.x + x;
                        int j = decoration.LowerLeftPosition.y + y;
                        if (i >= tiles.GetLength(0) || j >= tiles.GetLength(1))
                            continue;
                        int type = decoration.TacDec.Tile[x, y];
                        if (type >= 500)
                        {
                            if (type < 507)
                                FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.VolcanicTileTypes[type - 500]);
                            else
                            {
                                var obj = Instantiate(State.GameManager.SpriteRendererPrefab, TerrainFolder).GetComponent<SpriteRenderer>();
                                obj.sprite = TileDictionary.VolcanicTileSprites[type - 500];
                                obj.sortingOrder = 20000 - (30 * (i + (j * 3)));
                                obj.transform.position = new Vector3(i, j, 0);
                                if (y >= decoration.TacDec.Height)
                                    obj.sortingOrder += 30;
                            }
                        }
                        else if (type >= 400)
                        {
                            FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.SnowEnviroment[type - 400]);
                        }
                        else if (type >= 200)
                        {
                            if (type < 207)
                                FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.DesertTileTypes[type - 200]);
                            else
                            {
                                var obj = Instantiate(State.GameManager.SpriteRendererPrefab, TerrainFolder).GetComponent<SpriteRenderer>();
                                obj.sprite = TileDictionary.DesertTileSprites[type - 200];
                                obj.sortingOrder = 20000 - (30 * (i + (j * 3)));
                                obj.transform.position = new Vector3(i, j, 0);
                                if (y >= decoration.TacDec.Height)
                                    obj.sortingOrder += 30;
                            }
                        }
                        else if (type >= 100)
                        {
                            if (decoration.TacDec.Height == 0 && decoration.TacDec.Width == 0)
                            {
                                FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassEnviroment[type - 100]);
                            }
                            else
                            {
                                var obj = Instantiate(State.GameManager.SpriteRendererPrefab, TerrainFolder).GetComponent<SpriteRenderer>();
                                obj.sprite = TileDictionary.GrassEnviromentSprites[type - 100];
                                obj.sortingOrder = 20000 - (30 * (i + (j * 3)));
                                obj.transform.position = new Vector3(i, j, 0);
                                if (y >= decoration.TacDec.Height)
                                    obj.sortingOrder += 30;
                            }
                        }

                    }
                }

            }



            //int decNum = Decorations[i, j];
            //if (decNum >= 200)
            //{

            //}
            //else if (Decorations[i, j] >= 100)
            //{
            //    if (DecorationTypes.)
            //    {
            //        FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassEnviroment[Decorations[i, j] - 100]);
            //    }
            //    else
            //    {
            //        var obj = Instantiate(State.GameManager.SpriteRendererPrefab, TerrainFolder).GetComponent<SpriteRenderer>();
            //        obj.sprite = TileDictionary.GrassEnviromentSprites[Decorations[i, j] - 100];
            //        obj.sortingOrder = 20000 - (30 * (i + (j * 3)));
            //        obj.transform.position = new Vector3(i, j, 0);
            //        if (Decorations[i, j] == 105 || Decorations[i, j] == 111)
            //            obj.sortingOrder += 30;
            //    }

            //    //if (Decorations[i, j] == 105 || Decorations[i, j] == 111)
            //    //    FrontSpriteTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassEnviroment[Decorations[i, j] - 100]);
            //    //else
            //    //    FrontTilemap.SetTile(new Vector3Int(i, j, 0), TileDictionary.GrassEnviroment[Decorations[i, j] - 100]);
            //}
        }

        if (Buildings != null)
        {
            foreach (var building in Buildings)
            {
                for (int y = 0; y < building.Tile.GetLength(0); y++)
                {
                    for (int x = 0; x < building.Tile.GetLength(1); x++)
                    {
                        if (y >= building.Height || x >= building.Width)
                            FrontSpriteTilemap.SetTile(new Vector3Int(building.LowerLeftPosition.x + x, building.LowerLeftPosition.y + y, 0), BuildingTileTypes[building.Tile[y, x]]);
                        else
                            FrontTilemap.SetTile(new Vector3Int(building.LowerLeftPosition.x + x, building.LowerLeftPosition.y + y, 0), BuildingTileTypes[building.Tile[y, x]]);
                    }
                }
                for (int y = 0; y < building.FrontColoredTile.GetLength(0); y++)
                {
                    for (int x = 0; x < building.FrontColoredTile.GetLength(1); x++)
                    {
                        if (building.FrontColoredTile[y, x] != -1)
                        {
                            FrontColorTilemap.SetTile(new Vector3Int(building.LowerLeftPosition.x + x, building.LowerLeftPosition.y + y, 0), BuildingTileTypes[building.FrontColoredTile[y, x]]);
                        }

                    }
                }
            }
        }

    }

    internal Actor_Unit AddUnitToBattle(Unit unit,  Actor_Unit reciepient)
    {
        Actor_Unit actor = new Actor_Unit(unit, reciepient);
        units.Add(actor);
        actor.UpdateBestWeapons();
        UpdateActorColor(actor);
        if (actor.UnitSprite != null)
        {
            actor.UnitSprite.HitPercentagesDisplayed(false);
            actor.UnitSprite.DisplaySummoned();
        }

        if (actor.Unit.Side == defenderSide)
            DefenderConvert(actor);
        else if (actor.Unit.Side == attackerSide)
            AttackerConvert(actor);
        return actor;
    }

    internal Actor_Unit AddUnitToBattle(Unit unit, Vec2i position)
    {
        Actor_Unit actor = new Actor_Unit(position, unit);
        units.Add(actor);
        actor.UpdateBestWeapons();
        UpdateActorColor(actor);
        if (actor.UnitSprite != null)
        {
            actor.UnitSprite.HitPercentagesDisplayed(false);
            actor.UnitSprite.DisplaySummoned();
        }
        if (actor.Unit.Side == defenderSide)
            DefenderConvert(actor);
        else if (actor.Unit.Side == attackerSide)
            AttackerConvert(actor);
        return actor;
    }

    void UpdateActorColor(Actor_Unit actor)
    {
        if (!AIAttacker && AIDefender) //If there's one human, he's blue, otherwise the defender is blue
            actor.UnitSprite.BlueColored = (defenderSide != actor.Unit.Side);
        else
            actor.UnitSprite.BlueColored = (defenderSide == actor.Unit.Side);
    }

    void CreateActors()
    {

        int children = ActorFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(ActorFolder.GetChild(i).gameObject);
        }
        for (int i = 0; i < units.Count; i++)
        {
            Actor_Unit unit = units[i];
            unit.UpdateBestWeapons();

            if (!AIAttacker && AIDefender) //If there's one human, he's blue, otherwise the defender is blue
                unit.UnitSprite.BlueColored = (defenderSide != unit.Unit.Side);
            else
                unit.UnitSprite.BlueColored = (defenderSide == unit.Unit.Side);
        }

        for (int i = 0; i < discardedClothing.Count; i++)
        {
            discardedClothing[i].GenerateSpritePrefab(ActorFolder);
        }
        for (int i = 0; i < miscDiscards.Count; i++)
        {
            miscDiscards[i].GenerateSpritePrefab(ActorFolder);
        }
    }

    internal void CreateDiscardedClothing(Vec2i location, Race race, int type, int color, string name)
    {
        if (type == 0 || turboMode)
            return;
        if (location == null)
            return;
        lastDiscard++;
        int sortOrder = lastDiscard;
        discardedClothing.Add(new ClothingDiscards(location, race, type, color, sortOrder, name));
        discardedClothing.Last().GenerateSpritePrefab(ActorFolder);
    }

    internal void CreateScat(Vec2i location, ScatInfo scatInfo)
    {
        if (turboMode)
            return;
        if (location == null)
            return;
        lastDiscard++;
        int sortOrder = lastDiscard;

        if (Config.ScatV2 == true)
        {
            lastDiscard += (1 + scatInfo.bonesInfos.Count); //scatback + scatfront + bones
            miscDiscards.Add(new ScatV2Discard(location, sortOrder, scatInfo));
        }
        else if (Config.CleanDisposal == true)
        {
            lastDiscard += (1 + scatInfo.bonesInfos.Count); //scatback + scatfront + bones
            miscDiscards.Add(new DiaperDiscard(location, sortOrder, scatInfo));
        }
        else if (Config.BirdScat && (scatInfo.predRace == Race.Avians || scatInfo.predRace == Race.Terrorbird || scatInfo.predRace == Race.Gryphons))
        {
            lastDiscard += (1 + scatInfo.bonesInfos.Count); //scatback + scatfront + bones
            miscDiscards.Add(new BirdScat(location, sortOrder, scatInfo));
        }
        else
        {
            int spriteNum;
            int offset;
            if (scatInfo.predRace == Race.Slimes)
            {offset = 2;}
            else if (scatInfo.predRace == Race.Aabayx || scatInfo.predRace == Race.ViraeUltimae)
            {offset = 4;}
            else
            {offset = 0;}

            if (Config.ScatBones == false)
            {
                spriteNum = 0 + offset;
            }
            else
            {
                if (scatInfo.preyRace == Race.Slimes)
                    spriteNum = 0 + offset;
                else
                    spriteNum = offset + State.Rand.Next(2);
            }
            miscDiscards.Add(new MiscDiscard(location, MiscDiscardType.Scat, spriteNum, sortOrder, scatInfo.color, scatInfo.GetDescription()));
        }
        miscDiscards.Last().GenerateSpritePrefab(ActorFolder);
    }

    internal void CreateMiscDiscard(Vec2i location, BoneTypes type, string name, int color = -1)
    {
        if (turboMode)
            return;
        if (location == null)
            return;
        lastDiscard++;
        int sortOrder = lastDiscard;
        int spriteNum = (int)type;


        string description = $"Remains of {name}";
        if (type == BoneTypes.CumPuddle)
            miscDiscards.Add(new MiscDiscard(location, MiscDiscardType.Cum, spriteNum, sortOrder, color, description));
        else if (type == BoneTypes.DisposedCondom)
            miscDiscards.Add(new MiscDiscard(location, MiscDiscardType.DisposedCondom, spriteNum, sortOrder, color, description));
        else
            miscDiscards.Add(new MiscDiscard(location, MiscDiscardType.Bones, spriteNum, sortOrder, color, description));
        miscDiscards.Last().GenerateSpritePrefab(ActorFolder);
    }


    void ShowVoreHitPercentages(Actor_Unit actor, PreyLocation location = PreyLocation.stomach)
    {
        foreach (Actor_Unit target in units)
        {
            if (TacticalUtilities.AppropriateVoreTarget(actor, target) == false)
                continue;
            if ((Config.EdibleCorpses == false && target.Targetable == false && target.Visible) || target.Visible == false)
                continue;
            Vec2i pos = target.Position;
            target.UnitSprite.HitPercentagesDisplayed(true);
            if (actor.PredatorComponent.FreeCap() < target.Bulk() || (actor.BodySize() < target.BodySize() * 3 && actor.Unit.HasTrait(Traits.TightNethers) && PreyLocationMethods.IsGenital(location)))
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true), Color.yellow);
            else if (actor.Unit.CanVore(location) != actor.PredatorComponent.CanVore(location,target))
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true), Color.yellow);
            else if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true), Color.red);
            else if (actor.Unit.HasTrait(Traits.RangedVore) && actor.Position.GetNumberOfMovesDistance(target.Position) < 5)
            {
                int dist = target.Position.GetNumberOfMovesDistance(actor.Position);
                if (target.Unit.HasTrait(Traits.Flight))
                    dist = 1;
                int boost = -3 * (dist - 1);
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true, skillBoost: boost), Color.red);
            }
            else
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true), Color.black);
        }
    }

    void ShowBoostedVoreHitPercentages(Actor_Unit actor, int skillBoost, PreyLocation location = PreyLocation.stomach)
    {
        foreach (Actor_Unit target in units)
        {
            if (TacticalUtilities.AppropriateVoreTarget(actor, target) == false)
                continue;
            if ((Config.EdibleCorpses == false && target.Targetable == false && target.Visible) || target.Visible == false)
                continue;
            Vec2i pos = target.Position;
            target.UnitSprite.HitPercentagesDisplayed(true);
            if (actor.PredatorComponent.FreeCap() < target.Bulk() || (actor.BodySize() < target.BodySize() * 3 && actor.Unit.HasTrait(Traits.TightNethers) && PreyLocationMethods.IsGenital(location)))
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true, skillBoost), Color.yellow);
            else if (actor.Unit.CanVore(location) != actor.PredatorComponent.CanVore(location,target))
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true, skillBoost), Color.yellow);
            else if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true, skillBoost), Color.red);
            else
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true, skillBoost), Color.black);
        }
    }


    internal bool TakeSpecialAction(SpecialAction type, Actor_Unit actor, Actor_Unit target)
    {

        switch (type)
        {
            case SpecialAction.PounceVore:
                return actor.VorePounce(target, lastSpecial);
        }

        if (actor.Unit.Race == Race.Ki && type == SpecialAction.CockVore && target.Unit.Race != Race.Selicia)
        {
            State.GameManager.CreateMessageBox("Ki will only acccept Selicia into his cock, and nothing else");
            return false;
        }

        if (TacticalActionList.TargetedDictionary.TryGetValue(type, out var targetedAction))
        {
            if (targetedAction.RequiresPred && actor.Unit.Predator == false)
                return false;
            if (targetedAction.OnExecute == null)
                return false;
            targetedAction.OnExecute(actor, target);
            return true;
        }



        return false;
    }

    internal bool TakeSpecialActionLocation(SpecialAction type, Actor_Unit actor, Vec2i location)
    {

        if (TacticalActionList.TargetedDictionary.TryGetValue(type, out var targetedAction))
        {
            if (targetedAction.OnExecuteLocation != null)
                return targetedAction.OnExecuteLocation(actor, location);
        }
        return false;
    }


    void ShowSpecialHitPercentages(Actor_Unit actor)
    {
        switch (specialType)
        {
            case SpecialAction.Unbirth:
                ShowVoreHitPercentages(actor, PreyLocation.womb);
                break;
            case SpecialAction.CockVore:
                ShowVoreHitPercentages(actor, PreyLocation.balls);
                break;
            case SpecialAction.TailVore:
                ShowVoreHitPercentages(actor, PreyLocation.tail);
                break;
            case SpecialAction.AnalVore:
                ShowVoreHitPercentages(actor, PreyLocation.anal);
                break;
            case SpecialAction.BreastVore:
                ShowVoreHitPercentages(actor, PreyLocation.breasts);
                break;
            case SpecialAction.Transfer:
                ShowCockVoreTransferPercentages(actor);
                break;
            case SpecialAction.KissTransfer:
                ShowKissVoreTransferPercentages(actor);
                break;
            case SpecialAction.StealVore:
                ShowVoreStealPercentages(actor);
                break;
            case SpecialAction.BreastFeed:
                ShowBreastFeedPercentages(actor);
                break;
            case SpecialAction.CumFeed:
                ShowCumFeedPercentages(actor);
                break;
            case SpecialAction.Suckle:
                ShowSucklePercentages(actor);
                break;
            case SpecialAction.BellyRub:
                ShowRubHitPercentages(actor);
                break;
            case SpecialAction.PounceMelee:
                if (actor.Movement > 1)
                    ShowPounceMeleeHitPercentages(actor);
                break;
            case SpecialAction.PounceVore:
                if (actor.Movement > 1)
                    ShowPounceVoreHitPercentages(actor);
                break;
            case SpecialAction.ShunGokuSatsu:
                ShowMeleeHitPercentages(actor, 2);
                break;
            case SpecialAction.TailStrike:
                ShowMeleeHitPercentages(actor, .66f);
                break;
            case SpecialAction.SweepingSwallow:
                ShowVoreHitPercentages(actor, PreyLocation.stomach);
                UpdateOTargetGrid(actor.Position);
                break;
            case SpecialAction.GiantSweep:
                ShowMeleeHitPercentages(actor, .66f);
                UpdateOTargetGrid(actor.Position);
                break;
            case SpecialAction.AllInVore:
                ShowBoostedVoreHitPercentages(actor, 50);
                break;
            case SpecialAction.DireInfection:
                ShowMeleeHitPercentages(actor, .75f);
                break;
        }

    }

    void ShowRubHitPercentages(Actor_Unit actor)
    {
        foreach (Actor_Unit target in units)
        {
            if (target.Unit.GetApparentSide(actor.Unit) != actor.Unit.FixedSide && target.Unit.GetApparentSide(actor.Unit) != actor.Unit.GetApparentSide() &&
                !(actor.Unit.HasTrait(Traits.SeductiveTouch) || Config.CanUseStomachRubOnEnemies))
                continue;
            if ((target.Targetable == false && target.Visible) || target.Visible == false)
                continue;
            if (target.PredatorComponent?.Fullness <= 0)
                continue;
            if (target.ReceivedRub)
                continue;
            Vec2i pos = target.Position;
            target.UnitSprite.HitPercentagesDisplayed(true);
            if (actor.Unit.HasTrait(Traits.SeductiveTouch) && target.Unit.Side != actor.Unit.Side)
            {
                float charmChance = target.GetPureStatClashChance(actor.Unit.GetStat(Stat.Dexterity), target.Unit.GetStat(Stat.Will), .1f);
                if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                    target.UnitSprite.DisplayHitPercentage(charmChance, Color.magenta);
                else
                    target.UnitSprite.DisplayHitPercentage(charmChance, Color.black);
            }
            else
            {
                if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                    target.UnitSprite.DisplayHitPercentage(1f, Color.red);
                else
                    target.UnitSprite.DisplayHitPercentage(1f, Color.black);
            }
        }
    }

    void ShowCockVoreTransferPercentages(Actor_Unit actor)
    {
        if (actor.Unit.Predator == false)
            return;
        foreach (Actor_Unit target in units)
        {
            if (target.Unit.Predator == false)
                continue;
            if (target.Unit.Side == actor.Unit.Side && target.Surrendered == false)
            {
                if (actor.PredatorComponent.CanTransfer())
                {
                    if (target.PredatorComponent.FreeCap() < actor.PredatorComponent.TransferBulk() && !(target.Unit == actor.Unit))
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.Transfer), Color.yellow);
                    else if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.Transfer), Color.red);
                    else
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.Transfer), Color.black);
                }
                continue;
            }
        }

    }
    void ShowKissVoreTransferPercentages(Actor_Unit actor)
    {
        if (actor.Unit.Predator == false)
            return;
        foreach (Actor_Unit target in units)
        {
            if (target.Unit.Predator == false)
                continue;
            if (target.Unit.Side == actor.Unit.Side && target.Surrendered == false)
            {
                if (actor.PredatorComponent.CanKissTransfer())
                {
                    if (target.PredatorComponent.FreeCap() < actor.PredatorComponent.KissTransferBulk() && !(target.Unit == actor.Unit))
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.KissTransfer), Color.yellow);
                    else if ((actor.Position.GetNumberOfMovesDistance(target.Position) < 2) && !(target.Unit == actor.Unit))
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.KissTransfer), Color.red);
                    else
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.KissTransfer), Color.black);
                }
                continue;
            }
        }

    }

    void ShowVoreStealPercentages(Actor_Unit actor)
    {
        foreach (Actor_Unit target in units)
        {
            if (!actor.PredatorComponent.CanVoreSteal(target))
                continue;
            if (actor.PredatorComponent.FreeCap() < target.PredatorComponent.StealBulk() && (target.Unit != actor.Unit))
                target.UnitSprite.DisplayHitPercentage(target.PredatorComponent.GetVoreStealChance(actor), Color.yellow);
            else if ((actor.Position.GetNumberOfMovesDistance(target.Position) < 2) && (target.Unit != actor.Unit))
                target.UnitSprite.DisplayHitPercentage(target.PredatorComponent.GetVoreStealChance(actor), Color.red);
            else
                target.UnitSprite.DisplayHitPercentage(target.PredatorComponent.GetVoreStealChance(actor), Color.black);
            continue;
        }

    }


    void ShowBreastFeedPercentages(Actor_Unit actor)
    {
        if (actor.Unit.Predator == false)
            return;
        foreach (Actor_Unit target in units)
        {
            if ((target.Unit.GetApparentSide(actor.Unit) == actor.Unit.FixedSide || target.Unit.GetApparentSide(actor.Unit) == actor.Unit.GetApparentSide() || target.Unit == actor.Unit) && target.Surrendered == false)
            {
                if (actor.PredatorComponent.CanFeed())
                {
                    if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.BreastFeed), Color.red);
                    else
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.BreastFeed), Color.black);
                }
                continue;
            }
        }
    }


    void ShowCumFeedPercentages(Actor_Unit actor)
    {
        if (actor.Unit.Predator == false)
            return;
        foreach (Actor_Unit target in units)
        {
            if ((target.Unit.GetApparentSide(actor.Unit) == actor.Unit.FixedSide || target.Unit.GetApparentSide(actor.Unit) == actor.Unit.GetApparentSide() || target.Unit == actor.Unit) && target.Surrendered == false)
            {
                if (actor.PredatorComponent.CanFeedCum())
                {
                    if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.CumFeed), Color.red);
                    else
                        target.UnitSprite.DisplayHitPercentage(target.GetSpecialChance(SpecialAction.CumFeed), Color.black);
                }
                continue;
            }
        }
    }

    void ShowSucklePercentages(Actor_Unit actor)
    {
        if (actor.Unit.Predator == false)
            return;
        foreach (Actor_Unit target in units)
        {
            if (actor.PredatorComponent.GetSuckle(target)[0] == 0)
                continue;
            else if (actor.Position.GetNumberOfMovesDistance(target.Position) == 1)
                target.UnitSprite.DisplayHitPercentage(actor.PredatorComponent.GetSuckleChance(target), Color.red);
            else
                target.UnitSprite.DisplayHitPercentage(actor.PredatorComponent.GetSuckleChance(target), Color.black);
        }

    }

    void ShowPounceMeleeHitPercentages(Actor_Unit actor)
    {
        foreach (Actor_Unit target in units)
        {
            if ((TacticalUtilities.IsUnitControlledByPlayer(target.Unit) && Config.AllowInfighting == false) && !(!AIDefender && !AIAttacker) || actor == target)
                continue;
            if (target.Targetable == false || target.Visible == false)
                continue;
            if (TacticalUtilities.FreeSpaceAroundTarget(target.Position, actor) == false)
                continue;
            int weaponDamage = actor.WeaponDamageAgainstTarget(target, false);
            if (actor.Unit.HasTrait(Traits.HeavyPounce))
                weaponDamage = (int)Mathf.Min((weaponDamage + ((weaponDamage * actor.PredatorComponent?.Fullness ?? 0) / 4)), weaponDamage * 2);
            Vec2i pos = target.Position;
            if (actor.Position.GetNumberOfMovesDistance(target.Position) <= 4 && actor.Position.GetNumberOfMovesDistance(target.Position) >= 2)
                target.UnitSprite.DisplayHitPercentage(target.GetAttackChance(actor, false, true), Color.red, weaponDamage);
            else
                target.UnitSprite.DisplayHitPercentage(target.GetAttackChance(actor, false, true), Color.black, weaponDamage);
        }
    }

    void ShowPounceVoreHitPercentages(Actor_Unit actor)
    {
        foreach (Actor_Unit target in units)
        {
            if (TacticalUtilities.AppropriateVoreTarget(actor, target) == false)
                continue;
            if ((Config.EdibleCorpses == false && target.Targetable == false && target.Visible) || target.Visible == false)
                continue;
            if (TacticalUtilities.FreeSpaceAroundTarget(target.Position, actor) == false)
                continue;
            Vec2i pos = target.Position;
            target.UnitSprite.HitPercentagesDisplayed(true);
            if (actor.PredatorComponent.FreeCap() < target.Bulk())
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true), Color.yellow);
            else if (actor.Position.GetNumberOfMovesDistance(target.Position) <= 4 && actor.Position.GetNumberOfMovesDistance(target.Position) >= 2)
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true), Color.red);
            else
                target.UnitSprite.DisplayHitPercentage(target.GetDevourChance(actor, true), Color.black);
        }
    }

    void ShowMeleeHitPercentages(Actor_Unit actor, float multiplier = 1)
    {
        foreach (Actor_Unit target in units)
        {
            if ((TacticalUtilities.IsUnitControlledByPlayer(target.Unit) && Config.AllowInfighting == false && !(!AIDefender && !AIAttacker)) || actor == target)
                continue;
            if (target.Targetable == false || target.Visible == false)
                continue;
            int weaponDamage = (int)(multiplier * actor.WeaponDamageAgainstTarget(target, false));
            Vec2i pos = target.Position;
            if (actor.Position.GetNumberOfMovesDistance(target.Position) < 2)
                target.UnitSprite.DisplayHitPercentage(target.GetAttackChance(actor, false, true), Color.red, weaponDamage);
            else
                target.UnitSprite.DisplayHitPercentage(target.GetAttackChance(actor, false, true), Color.black, weaponDamage);
        }
    }


    void ShowRangedHitPercentages(Actor_Unit actor)
    {
        foreach (Actor_Unit target in units)
        {
            if ((TacticalUtilities.IsUnitControlledByPlayer(target.Unit) && Config.AllowInfighting == false && !(!AIDefender && !AIAttacker)) || actor == target)
                continue;
            if (target.Targetable == false || target.Visible == false)
                continue;
            int weaponDamage = actor.WeaponDamageAgainstTarget(target, true);

            Vec2i pos = target.Position;
            if (actor.Position.GetNumberOfMovesDistance(target.Position) <= actor.BestRanged.Range && (actor.Position.GetNumberOfMovesDistance(target.Position) > 1 || (actor.BestRanged.Omni && actor.Position.GetNumberOfMovesDistance(target.Position) > 0)))
                target.UnitSprite.DisplayHitPercentage(target.GetAttackChance(actor, true, true), Color.red, weaponDamage);
            else
                target.UnitSprite.DisplayHitPercentage(target.GetAttackChance(actor, true, true), Color.black, weaponDamage);
        }
    }

    void ShowMagicPercentages(Actor_Unit actor)
    {
        foreach (Actor_Unit target in units)
        {
            if (CurrentSpell.AcceptibleTargets.Contains(AbilityTargets.Self) == true && actor.Unit != target.Unit)
                continue;
            if (CurrentSpell.AcceptibleTargets.Contains(AbilityTargets.Ally) == false && (TacticalUtilities.IsUnitControlledByPlayer(target.Unit) && Config.AllowInfighting == false && !(!AIDefender && !AIAttacker)))
                continue;
            if (CurrentSpell.AcceptibleTargets.Contains(AbilityTargets.Enemy) == false && !(TacticalUtilities.IsUnitControlledByPlayer(target.Unit) || target.Unit.Side == actor.Unit.Side) && !(!AIDefender && !AIAttacker))
                continue;

            if (target.Targetable == false || target.Visible == false)
                continue;
            int spellDamage = 0;
            if (CurrentSpell is DamageSpell damageSpell)
            {
                spellDamage = damageSpell.Damage(actor, target);
                if (TacticalUtilities.SneakAttackCheck(actor.Unit, target.Unit)) // sneakAttack
                {
                    spellDamage *= 3;
                }
            }
             
            float magicChance = CurrentSpell.Resistable ? target.GetMagicChance(actor, CurrentSpell) : 1;

            if (CurrentSpell == SpellList.Maw || CurrentSpell == SpellList.GateMaw)
                magicChance *= target.GetDevourChance(actor, skillBoost: actor.Unit.GetStat(Stat.Mind));

            if (CurrentSpell == SpellList.Bind && target.Unit.Type != UnitType.Summon)
                magicChance = 0;

            Vec2i pos = target.Position;
            if (actor.Position.GetNumberOfMovesDistance(target.Position) <= CurrentSpell.Range.Max && (actor.Position.GetNumberOfMovesDistance(target.Position) >= CurrentSpell.Range.Min))
                target.UnitSprite.DisplayHitPercentage(magicChance, Color.red, spellDamage);
            else
                target.UnitSprite.DisplayHitPercentage(magicChance, Color.black, spellDamage);
        }
    }

    void ShowPotionThrowPercentages(Actor_Unit actor, bool onSelf = false)
    {
        foreach (Actor_Unit target in units)
        {
            if (target.Targetable == false || target.Visible == false)
                continue;
            int weaponDamage = actor.WeaponDamageAgainstTarget(target, true);

            Vec2i pos = target.Position;
            if (target.Unit.IsEnemyOfSide(actor.Unit.Side))
            {
                if (actor.Position.GetNumberOfMovesDistance(target.Position) <= 3)
                    target.UnitSprite.DisplayHitPercentage(target.GetAttackChance(actor, true, true), Color.red);
                else
                    target.UnitSprite.DisplayHitPercentage(target.GetAttackChance(actor, true, true), Color.black);
            }
            else
            {
                if (actor.Position.GetNumberOfMovesDistance(target.Position) <= 3)
                    target.UnitSprite.DisplayHitPercentage(1, Color.red);
                else
                    target.UnitSprite.DisplayHitPercentage(1, Color.black);
            }
        }
    }

    void RemoveHitPercentages()
    {
        foreach (Actor_Unit target in units)
        {
            target.UnitSprite?.HitPercentagesDisplayed(false);
        }
    }

    void CreateMovementGrid()
    {
        Walkable = TacticalPathfinder.GetGrid(SelectedUnit.Position, SelectedUnit.Unit.HasTrait(Traits.Flight), SelectedUnit.Movement, SelectedUnit);
        UpdateMovementGrid();
    }

    void UpdateMovementGrid()
    {
        MovementGrid.ClearAllTiles();
        for (int x = 0; x <= tiles.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= tiles.GetUpperBound(1); y++)
            {
                if (Walkable[x, y])
                    MovementGrid.SetTile(new Vector3Int(x, y, 0), MovementGridTileTypes[0]);
            }
        }
    }

    void UpdateAreaOfEffectGrid(Vec2i mouseLocation)
    {
        MovementGrid.ClearAllTiles();

        int radius = CurrentSpell.AreaOfEffect;
        bool outOfRange = mouseLocation.GetNumberOfMovesDistance(SelectedUnit.Position) > CurrentSpell.Range.Max;

        for (int x = mouseLocation.x - radius; x <= mouseLocation.x + radius; x++)
        {
            for (int y = mouseLocation.y - radius; y <= mouseLocation.y + radius; y++)
            {
                if (outOfRange)
                    MovementGrid.SetTile(new Vector3Int(x, y, 0), MovementGridTileTypes[0]);
                else
                    MovementGrid.SetTile(new Vector3Int(x, y, 0), MovementGridTileTypes[1]);
            }
        }

    }

    void UpdateTailStrikeGrid(Vec2i mouseLocation)
    {
        MovementGrid.ClearAllTiles();

        if (SelectedUnit.Position.GetNumberOfMovesDistance(mouseLocation.x, mouseLocation.y) != 1)
            return;

        Vec2 pos = mouseLocation;
        TestTile(pos);
        TestTile(pos + new Vec2(1, 0));
        TestTile(pos + new Vec2(0, 1));
        TestTile(pos + new Vec2(-1, 0));
        TestTile(pos + new Vec2(0, -1));

        void TestTile(Vec2 p)
        {
            if (SelectedUnit.Position.GetNumberOfMovesDistance(p.x, p.y) == 1)
                MovementGrid.SetTile(new Vector3Int(p.x, p.y, 0), MovementGridTileTypes[1]);
        }

    }
    void UpdateOTargetGrid(Vec2i location)
    {
        MovementGrid.ClearAllTiles();
        foreach (var item in TacticalUtilities.TilesOnPattern(location, new int[3, 3] { { 1, 1, 1 }, { 1, 0, 1 }, { 1, 1, 1 } }, 1))
        {
            Vec2 pos = item;
            TestTile(item);
        }
        void TestTile(Vec2 p)
        {
            if (SelectedUnit.Position.GetNumberOfMovesDistance(p.x, p.y) == 1)
                MovementGrid.SetTile(new Vector3Int(p.x, p.y, 0), MovementGridTileTypes[1]);
        }

    }

    void UpdateFixedCustomeGrid(Vec2i mouseLocation, int[,] targettiles, int range)
    {
        MovementGrid.ClearAllTiles();

        int radius = CurrentSpell.AreaOfEffect;
        bool outOfRange = mouseLocation.GetNumberOfMovesDistance(SelectedUnit.Position) > CurrentSpell.Range.Max;

        foreach (Vec2 tile_pos in TacticalUtilities.TilesOnPattern(mouseLocation, targettiles, (int)((Math.Sqrt(targettiles.Length) / 2) - 0.5)))
        {
            if (mouseLocation.GetNumberOfMovesDistance(new Vec2i(mouseLocation.x, mouseLocation.y)) <= range)
            {
                if (outOfRange)
                    MovementGrid.SetTile(new Vector3Int(tile_pos.x, tile_pos.y, 0), MovementGridTileTypes[0]);
                else
                    MovementGrid.SetTile(new Vector3Int(tile_pos.x, tile_pos.y, 0), MovementGridTileTypes[1]);
            }
        }
        
    }

    void UpdateRotatingCustomeGrid(Vec2i mouseLocation, int[,] targettiles, int range)
    {
        MovementGrid.ClearAllTiles();

        int radius = CurrentSpell.AreaOfEffect;
        bool outOfRange = mouseLocation.GetNumberOfMovesDistance(SelectedUnit.Position) > CurrentSpell.Range.Max;

        foreach (Vec2 tile_pos in TacticalUtilities.rotateTilePattern(mouseLocation, targettiles, (int)((Math.Sqrt(targettiles.Length) / 2) - 0.5), TacticalUtilities.GetRotatingOctant(SelectedUnit.Position, mouseLocation)))
        {
            if (mouseLocation.GetNumberOfMovesDistance(new Vec2i(mouseLocation.x, mouseLocation.y)) <= range)
            {
                if (outOfRange)
                    MovementGrid.SetTile(new Vector3Int(tile_pos.x, tile_pos.y, 0), MovementGridTileTypes[0]);
                else
                    MovementGrid.SetTile(new Vector3Int(tile_pos.x, tile_pos.y, 0), MovementGridTileTypes[1]);
            }
        }

    }

    void UpdateAttackGrid(Vec2i source)
    {
        int range = SelectedUnit.BestRanged?.Range ?? 1;
        for (int x = 0; x <= tiles.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= tiles.GetUpperBound(1); y++)
            {
                if (source.GetNumberOfMovesDistance(new Vec2i(x, y)) <= range)
                {
                    if (MovementGrid.GetTile(new Vector3Int(x, y, 0)) == MovementGridTileTypes[0])
                        MovementGrid.SetTile(new Vector3Int(x, y, 0), MovementGridTileTypes[2]);
                    else
                        MovementGrid.SetTile(new Vector3Int(x, y, 0), MovementGridTileTypes[1]);
                }

            }
        }
    }

    bool ButtonsInteractable => (IsPlayerTurn || PseudoTurn) == true && (RunningFriendlyAI || foreignAI != null) == false && queuedPath == null && paused == false;



    public void ButtonCallback(int ID)
    {
        RightClickMenu.CloseAll();
        if (ButtonsInteractable)
        {
            switch (ID)
            {
                case 0:
                    if (SelectedUnit != null && SelectedUnit.Targetable)
                        if (SelectedUnit.BestMelee != null && SelectedUnit.Movement > 0)
                        {
                            ActionMode = 1;
                        }
                    break;
                case 1:
                    if (SelectedUnit != null && SelectedUnit.Targetable)
                        if (SelectedUnit.BestRanged != null && SelectedUnit.Movement > 0)
                        {
                            ActionMode = 2;
                        }
                    break;
                case 2:
                    if (SelectedUnit != null && SelectedUnit.Targetable && SelectedUnit.Unit.Predator)
                        if (SelectedUnit.Movement > 0)
                        {
                            ActionMode = 3;
                        }
                    break;
                case 3:
                    if (State.TutorialMode && State.GameManager.TutorialScript.step < 6)
                        return;
                    if (PseudoTurn)
                    {
                        IgnorePseudo = true;
                    }
                    else
                    RunningFriendlyAI = true;
                    break;
                case 4:
                    PromptEndTurn();
                    break;
                case 7:
                    if (SelectedUnit != null && SelectedUnit.Targetable)
                        if (SelectedUnit.Movement > 0)
                        {
                            currentPathDestination = null;
                            ActionMode = 5;
                        }
                    break;
                case 9:
                    if (State.TutorialMode && State.GameManager.TutorialScript.step < 6)
                        return;
                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        bool curAttacker = attackersTurn;
                        var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
                        box.SetData(() => SurrenderAll(curAttacker), "Yes", "No", "Are you sure you want to surrender all your units (you were holding left shift)");
                    }
                    else if (SelectedUnit != null && SelectedUnit.Targetable)
                    {
                        if (SelectedUnit.Surrendered && SelectedUnit.SurrenderedThisTurn)
                        {
                            SelectedUnit.Surrendered = false;
                            SelectedUnit.SurrenderedThisTurn = false;
                            RebuildInfo();
                        }
                        else if (SelectedUnit.Surrendered == false)
                        {
                            SelectedUnit.Surrendered = true;
                            if (State.Rand.NextDouble() <= Config.SurrenderedPredAutoRegur)
                            {
                                SelectedUnit.PredatorComponent?.FreeAnyAlivePrey();
                            }
                            SelectedUnit.SurrenderedThisTurn = true;
                            RebuildInfo();
                        }

                    }
                    break;
                case 10:
                    if (SelectedUnit != null && SelectedUnit.Targetable)
                    {
                        AttemptRetreat(SelectedUnit, false);
                        RebuildInfo();
                    }
                    break;

                //case 11-13: Handled Below As they are now independent of player turns                  

                case 40:
                    UndoMovement();
                    break;
                case 95:
                    if (SelectedUnit != null)
                    {
                        if (State.TutorialMode && State.GameManager.TutorialScript.step < 6)
                        {
                            State.GameManager.CreateMessageBox("Can't use this in this tutorial battle");
                            break;
                        }

                        SelectedUnit.Movement = 0;
                        RebuildInfo();
                    }
                    break;

            }
        }
        else if (reviewingBattle)
        {
            if (ID == 4)
                PromptEndTurn();
        }
        if (ID == 11)
        {
            if (AIDefender && AIAttacker)
            {
                manualSkip = true;
                TurboMode();
                return;
            }
            if (State.TutorialMode && State.GameManager.TutorialScript.step < 6)
            {
                State.GameManager.CreateMessageBox("Can't use this skip in the tutorial");
                return;
            }
            SkipUI.gameObject.SetActive(true);
            if (SkipUI.KeepSettings.isOn == false)
            {
                SkipUI.WatchRest.isOn = false;
                SkipUI.AllowRetreat.isOn = false;
                SkipUI.Surrender.isOn = false;
            }
        }
        if (ID == 12)
        {
            SkipUI.gameObject.SetActive(false);
        }
        if (ID == 13)
        {
            SkipUI.gameObject.SetActive(false);
            ProcessSkip(SkipUI.Surrender.isOn, SkipUI.WatchRest.isOn);

        }
        if (ID == 14)
        {
            if (SelectedUnit != null && SelectedUnit.Targetable)
                SwitchAlignment(SelectedUnit);

        }
        if (ID == 15)
        {
            if (SelectedUnit != null && SelectedUnit.Targetable)
                SelectedUnit.Unit.hiddenFixedSide = false;

        }
        if (ID == 16)
        {
            if (SelectedUnit != null && SelectedUnit.Targetable)
                TacticalUtilities.ShapeshifterPanel(SelectedUnit);
        }
    }

    internal void ProcessSkip(bool surrender, bool watchRest)
    {
        var defenderRace = armies[1]?.Empire?.ReplacedRace ?? village?.Empire?.ReplacedRace ?? ((Race)defenderSide);
        var attackerRace = armies[0]?.Empire?.ReplacedRace;

        manualSkip = true;
        if (IsPlayerTurn == false)
        {
            if (AIDefender == false)
            {
                object[] argArray = { units, tiles, defenderSide, false };
                RaceAI rai = State.RaceSettings.GetRaceAI(defenderRace);
                defenderAI = Activator.CreateInstance(RaceAIType.Dict[rai], args: argArray) as TacticalAI;
                if (SkipUI.AllowRetreat.isOn)
                    defenderAI.RetreatPlan = new TacticalAI.RetreatConditions(.2f, 0);
                AIDefender = true;
                if (surrender)
                {
                    foreach (Actor_Unit actor in units.Where(s => s.Unit.Side == defenderSide && unitControllableBySide(s, defenderSide)))
                    {
                        actor.Surrendered = true;
                        actor.Movement = 0;
                    }
                }
            }
            else if (AIAttacker == false)
            {
                object[] argArray = { units, tiles, attackerSide, false };
                RaceAI rai = State.RaceSettings.GetRaceAI((Race)attackerRace);
                attackerAI = Activator.CreateInstance(RaceAIType.Dict[rai], args: argArray) as TacticalAI;
                if (SkipUI.AllowRetreat.isOn)
                    attackerAI.RetreatPlan = new TacticalAI.RetreatConditions(.2f, 0);
                AIAttacker = true;
                if (surrender)
                {
                    foreach (Actor_Unit actor in units.Where(s => s.Unit.Side != defenderSide && unitControllableBySide(s, s.Unit.Side)))
                    {
                        actor.Surrendered = true;
                        actor.Movement = 0;
                    }
                }
            }
            if (watchRest == false)
                TurboMode();
            else SpectatorMode = true;
            return;
        }
        if (attackersTurn)
        {
            object[] argArray = { units, tiles, activeSide, false };
            RaceAI rai = State.RaceSettings.GetRaceAI((Race)attackerRace);
            attackerAI = Activator.CreateInstance(RaceAIType.Dict[rai], args: argArray) as TacticalAI;
            if (SkipUI.AllowRetreat.isOn)
                attackerAI.RetreatPlan = new TacticalAI.RetreatConditions(.2f, 0);
            AIAttacker = true;
            currentAI = attackerAI;
        }
        else
        {
            object[] argArray = { units, tiles, activeSide, false };
            RaceAI rai = State.RaceSettings.GetRaceAI(defenderRace);
            defenderAI = Activator.CreateInstance(RaceAIType.Dict[rai], args: argArray) as TacticalAI;
            if (SkipUI.AllowRetreat.isOn)
                defenderAI.RetreatPlan = new TacticalAI.RetreatConditions(.2f, 0);
            AIDefender = true;
            currentAI = defenderAI;
        }
        AITimer = 1;
        IsPlayerTurn = false;
        if (surrender)
        {
            foreach (Actor_Unit actor in units.Where(s => s.Unit.Side == activeSide && unitControllableBySide(s, activeSide)))
            {
                actor.Surrendered = true;
                actor.Movement = 0;
            }
        }
        if (watchRest == false)
            TurboMode();
        else SpectatorMode = true;
    }

    internal void SetMagicMode(Spell spell)
    {
        if (ButtonsInteractable && spell != null && SelectedUnit != null && SelectedUnit.Targetable && SelectedUnit.Movement > 0)
        {
            CurrentSpell = spell;
            ActionMode = 6;
        }
    }

    internal void SetPotionMode(Potion potion)
    {
        if (ButtonsInteractable && potion != null && SelectedUnit != null && SelectedUnit.Targetable && SelectedUnit.Movement > 0)
        {
            CurrentPotion = potion;
            ActionMode = 7;
        }
    }

    internal void TrySetSpecialMode(SpecialAction mode)
    {

        if (TacticalActionList.TargetedDictionary.TryGetValue(mode, out var targetedAction))
        {
            if (SelectedUnit == null || SelectedUnit.Targetable == false || SelectedUnit.Movement < targetedAction.MinimumMP)
                return;
            if (targetedAction.RequiresPred && SelectedUnit.Unit.Predator == false)
                return;
            lastSpecial = specialType;
            specialType = mode;
            ActionMode = 4;
        }

    }

    void PromptEndTurn()
    {
        if (Config.PromptEndTurn == false)
        {
            EndTurn();
            return;
        }
        bool canStillMove = false;
        for (int i = 0; i < units.Count; i++)
        {
            if (TacticalUtilities.IsUnitControlledByPlayer(units[i].Unit) && units[i].Targetable && units[i].Movement > 0)
            {
                canStillMove = true;
                break;
            }
        }
        if (canStillMove)
        {
            var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
            box.SetData(EndTurn, "Yes", "No", "You still have units left that are capable of moving, end turn anyway?");
        }
        else
            EndTurn();
    }

    internal void SurrenderAll(bool attacker)
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (attacker ? units[i].Unit.Side != defenderSide : units[i].Unit.Side == defenderSide)
            {
                units[i].Surrendered = true;
                units[i].SurrenderedThisTurn = true;
            }
        }
        RebuildInfo();
    }
    internal void KillAll(bool attacker)
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (attacker ? units[i].Unit.Side != defenderSide : units[i].Unit.Side == defenderSide)
            {
                units[i].Unit.Health = -1;
            }
        }
        RebuildInfo();
    }

    internal void AttemptRetreat(Actor_Unit actor, bool silent, bool fromWarp = false)
    {
        if (!fromWarp)
        {

            if (currentTurn < actor.Unit.TraitBoosts.TurnCanFlee)
            {
                if (silent == false) State.GameManager.CreateMessageBox($"Can't retreat before the {actor.Unit.TraitBoosts.TurnCanFlee}th turn");
                return;
            }

            if (actor.Movement <= 0)
            {
                if (silent == false) State.GameManager.CreateMessageBox("Unit needs at least 1 AP to flee");
                return;
            }

            if (actor.Unit.Type == UnitType.Summon)
            {
                if (silent == false) State.GameManager.CreateMessageBox("A summoned unit can not flee");
                return;
            }

            if (actor.Unit.HasTrait(Traits.Fearless))
            {
                if (silent == false) State.GameManager.CreateMessageBox("A unit with the fearless trait can not flee");
                return;
            }

            if (actor.Unit.Type == UnitType.SpecialMercenary)
            {
                if (silent == false) State.GameManager.CreateMessageBox($"{actor.Unit.Name}'s pride prevents them from fleeing (Special merc)");
                return;
            }
        }

        if (actor.Unit.Side == defenderSide)
        {
            if (actor.Position.y == 0 || fromWarp)
            {
                RetreatUnit(actor, true);
            }
            else if (silent == false)
                State.GameManager.CreateMessageBox("Unit must be on the bottom edge of the map to flee");
        }
        else
        {
            if (actor.Position.y == tiles.GetUpperBound(1) || fromWarp)
            {
                RetreatUnit(actor, false);
            }
            else if (silent == false)
                State.GameManager.CreateMessageBox("Unit must be on the top edge of the map to flee");
        }
    }

    void RetreatUnit(Actor_Unit actor, bool defender)
    {
        if (defender)
        {
            if (armies[1] != null)
                armies[1].Units.Remove(actor.Unit);
            retreatedDefenders.Add(actor.Unit);
        }
        else
        {
            armies[0].Units.Remove(actor.Unit);
            retreatedAttackers.Add(actor.Unit);
        }
        if (actor.PredatorComponent?.PreyCount > 0)
            RetreatedDigestors.Add(actor);
        actor.Visible = false;
        actor.Targetable = false;
        actor.UnitSprite.gameObject.SetActive(false);
        actor.Fled = true;
        actor.Unit.RefreshSecrecy();
        SelectedUnit = null;
    }

    void UpdateStatus(float dt)
    {
        if (SpectatorMode) RunningFriendlyAI = true;

        Translator?.UpdateLocation();

        if (State.World.IsNight)
        {
            UpdateFog();
        }
        SpellHelperText.SetActive(ActionMode == 6 && CurrentSpell.AcceptibleTargets.Contains(AbilityTargets.Tile));

        if (SelectedUnit != null)
        {
            if (SelectedUnit.Targetable)
            {
                if (SelectedUnit.UnitSprite != null)
                    SelectionBox.position = SelectedUnit.UnitSprite.transform.position;
                else
                    SelectionBox.position = new Vector2(SelectedUnit.Position.x, SelectedUnit.Position.y);
                SelectionBox.gameObject.SetActive(true);
            }
            else
            {
                SelectionBox.gameObject.SetActive(false);
                SelectedUnit = null;
            }
        }

        foreach (var unit in units)
        {
            if (unit.Visible != unit.UnitSprite.GraphicsFolder.gameObject.activeSelf)
            {
                unit.UnitSprite.GraphicsFolder.gameObject.SetActive(unit.Visible);
                unit.UnitSprite.OtherFolder.gameObject.SetActive(unit.Visible);
            }

            if (unit.Visible)
            {
                unit.Update(dt);
                unit.AnimationController?.UpdateTimes(dt);
                unit.UnitSprite.UpdateSprites(unit, unit.Unit.Side == activeSide);

                // TODO discriminate between living and dead prey

                if (Config.CloseInDigestionNoises && unit.PredatorComponent?.PreyCount > 0)
                {
                    if (unit.PredatorComponent?.AlivePrey > 0)
                    {
                        foreach (var loc in PredatorComponent.preyLocationOrder)
                        {
                            if (unit.PredatorComponent.PreyInLocation(loc, true) > 0)
                            {
                                State.GameManager.SoundManager.PlayDigestLoop(loc, unit);
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (var loc in PredatorComponent.preyLocationOrder)
                        {
                            if (unit.PredatorComponent.PreyInLocation(loc, false) > 0)
                            {
                                State.GameManager.SoundManager.PlayAbsorbLoop(loc, unit);
                                break;
                            }
                        }
                    }
                }


                if (unit.PredatorComponent?.AlivePrey > 0 && unit.PredatorComponent?.Fullness > 0)
                {
                    if (unit.Unit.Race == Race.EasternDragon)
                        unit.UnitSprite.AnimateBelly(unit.PredatorComponent.PreyInLocation(PreyLocation.stomach, true) * 0.0022f);
                    else
                        unit.UnitSprite.AnimateBelly(unit.PredatorComponent.PreyNearLocation(PreyLocation.stomach, true) * 0.0022f);
                }
                if (unit.PredatorComponent?.Stomach2ndFullness > 0 && unit.PredatorComponent?.AlivePrey > 0)
                {
                    unit.UnitSprite.AnimateSecondBelly(unit.PredatorComponent.PreyNearLocation(PreyLocation.stomach2, true) * 0.0022f);
                }
                if (unit.PredatorComponent?.BallsFullness > 0 && unit.PredatorComponent?.AlivePrey > 0)
                {
                    unit.UnitSprite.AnimateBalls(unit.PredatorComponent.PreyNearLocation(PreyLocation.balls, true) * 0.0022f);
                }
                if (unit.PredatorComponent?.BreastFullness > 0 && unit.PredatorComponent?.AlivePrey > 0)
                {
                    unit.UnitSprite.AnimateBoobs(unit.PredatorComponent.PreyNearLocation(PreyLocation.breasts, true) * 0.0022f);
                }

                if (unit.PredatorComponent?.LeftBreastFullness > 0 && unit.PredatorComponent?.AlivePrey > 0)
                {
                    if (Config.FairyBVType == FairyBVType.Shared)
                    {
                    unit.UnitSprite.AnimateBoobs(unit.PredatorComponent.PreyNearLocation(PreyLocation.leftBreast, true) * 0.022f);
                    unit.UnitSprite.AnimateSecondBoobs(unit.PredatorComponent.PreyNearLocation(PreyLocation.leftBreast, true) * 0.022f);
                    }
                    else
                    unit.UnitSprite.AnimateBoobs(unit.PredatorComponent.PreyNearLocation(PreyLocation.leftBreast, true) * 0.0022f);
                }

                if (unit.PredatorComponent?.RightBreastFullness > 0 && unit.PredatorComponent?.AlivePrey > 0)
                {
                    unit.UnitSprite.AnimateSecondBoobs(unit.PredatorComponent.PreyNearLocation(PreyLocation.rightBreast, true) * 0.0022f);
                }
            }
        }
        if (waitingForDialog)
            return;
        var foreignUnits = units.Where(unit => unit.Unit.Side == activeSide && !TacticalUtilities.IsUnitControlledByPlayer(unit.Unit) && unit.Movement > 0).ToList();
        if (IsPlayerTurn)
        {
            if (RunningFriendlyAI)
            {
                if (AITimer > 0)
                {
                    AITimer -= dt;
                }
                else
                {
                    RefreshAIIfNecessary();
                    //do AI processing
                    if (currentAI.RunAI() == false)
                    {
                        RunningFriendlyAI = false;
                        EndTurn();
                    }
                    if (AITimer <= 0)
                        AITimer = Config.TacticalPlayerMovementDelay;
                }
            }
            else if (foreignAI != null || foreignUnits.Count() > 0)
            {
                Type desiredAIType;
                if (foreignUnits.Count() > 0)
                    desiredAIType = TacticalUtilities.GetMindControlSide(foreignUnits[0].Unit) != -1 ? GetAITypeForMindControledUnit(foreignUnits[0].Unit) : RaceAIType.Dict[State.RaceSettings.GetRaceAI(foreignUnits[0].Unit.Race)];
                else
                    desiredAIType = typeof(StandardTacticalAI);
                if (foreignAI == null || (foreignAI.GetType() != desiredAIType))
                {
                    object[] argArray = { units, tiles, activeSide, false };
                    foreignAI = Activator.CreateInstance(desiredAIType, args: argArray) as TacticalAI;
                }
                foreignAI.ForeignTurn = true;
                if (AITimer > 0)
                {
                    AITimer -= dt;
                }
                else
                {
                    //do AI processing
                    if (foreignAI.RunAI() == false)
                    {
                        foreignAI = null;
                    }
                    if (AITimer <= 0)
                        AITimer = Config.TacticalPlayerMovementDelay;
                }
            }
        }
        else
        {
            if (autoAdvancing == false)
                AI(dt);

        }
        if (autoAdvancing)
        {
            autoAdvanceTimer -= Time.deltaTime;
            if (autoAdvanceTimer <= 0)
            {
                EndTurn();
                if (Config.AutoAdvance == Config.AutoAdvanceType.SkipToEnd)
                {
                    while (autoAdvancing && currentTurn < 2000)
                    {
                        if (waitingForDialog)
                            return;
                        EndTurn();
                    }
                }
                else
                    autoAdvanceTimer = AutoAdvanceRate;
            }
        }

        if (queuedPath != null)
        {
            if (SelectedUnit == null)
                queuedPath = null;
            else if (SelectedUnit.Movement <= 0)
            {
                queuedPath = null;
                SelectedUnit.ClearMovement();
            }
            else if (Translator.IsActive == false)
            {
                if (queuedPath.Count > 0)
                {
                    if (SelectedUnit.MoveTo(new Vec2i(queuedPath[0].X, queuedPath[0].Y), tiles, Config.TacticalPlayerMovementDelay))
                    {
                        queuedPath.RemoveAt(0);
                        RebuildInfo();
                    }
                    else
                        queuedPath = null;

                }
                else
                    queuedPath = null;
            }
        }

    }

    public Type GetAITypeForMindControledUnit(Unit unit)
    {
        if (unit.GetStatusEffect(StatusEffectType.Hypnotized) != null)
            return typeof(NonCombatantTacticalAI);
        if (unit.GetStatusEffect(StatusEffectType.Charmed) != null)
            return typeof(HedonistTacticalAI);
        return typeof(HedonistTacticalAI);
    }

    void AI(float dt)
    {
        if (AITimer > 0)
        {
            AITimer -= dt;
        }
        else
        {
            RefreshAIIfNecessary();
            if (currentAI.RunAI() == false)
            {
                EndTurn();
            }
            if (AITimer <= 0)
                AITimer = Config.TacticalAIMovementDelay;
        }
    }

    void MouseOver(int x, int y)
    {
        Vec2i mouseLocation = new Vec2i(x, y);
        StatusUI.HitRate.text = "";
        InfoPanel.RefreshTacticalUnitInfo(null);
        bool refreshed = false;

        List<string> ClothesFound = new List<string>();
        CheckPath(mouseLocation);

        for (int i = 0; i < units.Count; i++)
        {
            Actor_Unit actor = units[i];
            if (actor.Position.GetDistance(mouseLocation) < 1 && actor.Targetable)
            {
                refreshed = true;
                if (remainingLockedPanelTime <= 0)
                    InfoPanel.RefreshTacticalUnitInfo(actor);

                if (!TacticalUtilities.IsUnitControlledByPlayer(actor.Unit) && SelectedUnit != null && SelectedUnit.Targetable)
                {
                    //write chance
                    switch (ActionMode)
                    {
                        case 1:

                            if (actor.Position.GetNumberOfMovesDistance(SelectedUnit.Position) < 2)
                            {
                                int weaponDamage = SelectedUnit.WeaponDamageAgainstTarget(actor, false);
                                string str = System.Math.Round(actor.GetAttackChance(SelectedUnit, false) * 100, 1) + "%\n-" + weaponDamage;
                                StatusUI.HitRate.text = str;
                                actor.UnitSprite.ShowDamagedHealthBar(actor, weaponDamage);
                            }
                            break;
                        case 2:
                            if (SelectedUnit.BestRanged.Range > 1
                                    && actor.Position.GetNumberOfMovesDistance(SelectedUnit.Position) > 1
                                    && actor.Position.GetNumberOfMovesDistance(SelectedUnit.Position) <= SelectedUnit.BestRanged.Range)
                            {
                                int weaponDamage = SelectedUnit.WeaponDamageAgainstTarget(actor, true);
                                string str = System.Math.Round(actor.GetAttackChance(SelectedUnit, true) * 100, 1) + "%\n-" + weaponDamage;
                                StatusUI.HitRate.text = str;
                                actor.UnitSprite.ShowDamagedHealthBar(actor, weaponDamage);
                            }
                            break;
                        case 3:
                            if (actor.Position.GetNumberOfMovesDistance(SelectedUnit.Position) < 2)
                            {
                                if (SelectedUnit.PredatorComponent?.FreeCap() >= actor.Bulk())
                                {
                                    string str = System.Math.Round(actor.GetDevourChance(SelectedUnit) * 100, 1) + "%";
                                    StatusUI.HitRate.text = str;
                                }
                                else
                                    StatusUI.HitRate.text = "Not enough room";

                            }
                            break;
                        case 4:
                            if (actor.Position.GetNumberOfMovesDistance(SelectedUnit.Position) < 2)
                            {
                                if (SelectedUnit.PredatorComponent?.FreeCap() >= actor.Bulk())
                                {
                                    string str = System.Math.Round(actor.GetDevourChance(SelectedUnit) * 100, 1) + "%";
                                    StatusUI.HitRate.text = str;
                                }
                                else
                                    StatusUI.HitRate.text = "Not enough room";

                            }
                            else if (specialType == SpecialAction.PounceMelee)
                            {
                                int weaponDamage = SelectedUnit.WeaponDamageAgainstTarget(actor, false);
                                if (SelectedUnit.Unit.HasTrait(Traits.HeavyPounce))
                                    weaponDamage = (int)Mathf.Min((weaponDamage + ((weaponDamage * SelectedUnit.PredatorComponent?.Fullness ?? 0) / 4)), weaponDamage * 2);
                                string str = System.Math.Round(actor.GetAttackChance(SelectedUnit, false) * 100, 1) + "%\n-" + weaponDamage;
                                StatusUI.HitRate.text = str;
                                actor.UnitSprite.ShowDamagedHealthBar(actor, weaponDamage);
                            }
                            if (specialType == SpecialAction.TailStrike)
                            {
                                UpdateTailStrikeGrid(mouseLocation);
                            }
                            break;
                            if (specialType == SpecialAction.DireInfection)
                            {
                                UpdateOTargetGrid(mouseLocation);
                            }
                            break;
                    }
                }
            }
            else if (actor.Position.GetDistance(mouseLocation) < 1 && actor.Targetable == false && actor.Visible)
            {
                if (refreshed == false)
                {
                    InfoPanel.ClearPicture();
                    InfoPanel.ClearText();
                    refreshed = true;
                }

                InfoPanel.AddCorpse(actor.Unit.Name);
            }
        }
        foreach (ClothingDiscards discards in discardedClothing)
        {
            if (discards.location.GetDistance(mouseLocation) < 1)
            {
                if (ClothesFound.Contains(discards.name))
                    continue;
                if (refreshed == false)
                {
                    InfoPanel.ClearText();
                    refreshed = true;
                }
                ClothesFound.Add(discards.name);
                InfoPanel.AddClothes(discards.name);
            }
        }
        foreach (MiscDiscard discard in miscDiscards)
        {
            if (discard.location.GetDistance(mouseLocation) < 1)
            {
                if (refreshed == false)
                {
                    InfoPanel.ClearText();
                    refreshed = true;
                }
                InfoPanel.AddLine(discard.description);
            }
        }

        if (ActionMode == 6)
        {
            if (CurrentSpell?.AOEType == AreaOfEffectType.FixedPattern)
                UpdateFixedCustomeGrid(mouseLocation, CurrentSpell?.Pattern, SelectedUnit.Position.GetNumberOfMovesDistance(mouseLocation.x, mouseLocation.y));
            if (CurrentSpell?.AOEType == AreaOfEffectType.RotatablePattern)
                UpdateRotatingCustomeGrid(mouseLocation, CurrentSpell?.Pattern, SelectedUnit.Position.GetNumberOfMovesDistance(mouseLocation.x, mouseLocation.y));
            else if (CurrentSpell?.AreaOfEffect > 0)
                UpdateAreaOfEffectGrid(mouseLocation);

            if (CurrentSpell is DamageSpell spell)
            {
                if (spell.AreaOfEffect == 0 && spell.AOEType == AreaOfEffectType.Full)
                {
                    for (int i = 0; i < units.Count; i++)
                    {
                        Actor_Unit actor = units[i];
                        if (actor.Position.GetDistance(mouseLocation) < 1 && actor.Targetable)
                        {
                            if (actor != null)
                            {
                                int spellDamage = spell.Damage(SelectedUnit, actor);
                                if (TacticalUtilities.SneakAttackCheck(SelectedUnit.Unit, actor.Unit)) // sneakAttack
                                {
                                    spellDamage *= 3;
                                }
                                actor.UnitSprite.ShowDamagedHealthBar(actor, spellDamage);
                                string str = System.Math.Round(actor.GetMagicChance(SelectedUnit, CurrentSpell) * 100, 1) + "%\n-" + spellDamage;
                                StatusUI.HitRate.text = str;
                            }
                        }
                    }
                }
                else if (spell.AOEType == AreaOfEffectType.FixedPattern)
                {
                    foreach (var splashTarget in TacticalUtilities.UnitsWithinPattern(mouseLocation, spell.Pattern))
                    {
                        int spellDamage = spell.Damage(SelectedUnit, splashTarget);
                        if (TacticalUtilities.SneakAttackCheck(SelectedUnit.Unit, splashTarget.Unit)) // sneakAttack
                        {
                            spellDamage *= 3;
                        }
                        splashTarget.UnitSprite.ShowDamagedHealthBar(splashTarget, spellDamage);
                    }
                }
                else if (spell.AOEType == AreaOfEffectType.RotatablePattern)
                {
                    foreach (var splashTarget in TacticalUtilities.UnitsWithinRotatingPattern(mouseLocation, spell.Pattern, TacticalUtilities.GetRotatingOctant(SelectedUnit.Position, mouseLocation)))
                    {
                        int spellDamage = spell.Damage(SelectedUnit, splashTarget);
                        if (TacticalUtilities.SneakAttackCheck(SelectedUnit.Unit, splashTarget.Unit)) // sneakAttack
                        {
                            spellDamage *= 3;
                        }
                        splashTarget.UnitSprite.ShowDamagedHealthBar(splashTarget, spellDamage);
                    }
                }
                else if (mouseLocation != null)
                {
                    foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(mouseLocation, spell.AreaOfEffect))
                    {
                        int spellDamage = spell.Damage(SelectedUnit, splashTarget);
                        if (TacticalUtilities.SneakAttackCheck(SelectedUnit.Unit, splashTarget.Unit)) // sneakAttack
                        {
                            spellDamage *= 3;
                        }
                        splashTarget.UnitSprite.ShowDamagedHealthBar(splashTarget, spellDamage);
                    }
                }


            }
        }
    }

    private void CheckPath(Vec2i mouseLocation)
    {
        if (ActionMode != 5)
        {
            arrowManager.ClearNodes();
            return;
        }

        if (SelectedUnit == null)
            return;

        if (currentPathDestination != null && mouseLocation.Matches(currentPathDestination))
            return;
        if (TacticalUtilities.OpenTile(mouseLocation, SelectedUnit) == false)
            return;
        currentPathDestination = mouseLocation;
        var path = TacticalPathfinder.GetPath(SelectedUnit.Position, mouseLocation, 0, SelectedUnit);
        arrowManager.ClearNodes();
        if (path == null || path.Count == 0)
            return;
        int remainingMP = SelectedUnit.Movement;

        for (int i = 0; i < path.Count; i++)
        {
            remainingMP -= TacticalTileInfo.TileCost(new Vec2(path[i].X, path[i].Y));
            if (remainingMP > 1)
                arrowManager.PlaceNode(Color.green, new Vec2i(path[i].X, path[i].Y));
            else if (remainingMP == 1)
                arrowManager.PlaceNode(Color.yellow, new Vec2i(path[i].X, path[i].Y));
            else if (remainingMP == 0)
                arrowManager.PlaceNode(Color.red, new Vec2i(path[i].X, path[i].Y));
            else
                arrowManager.PlaceNode(Color.gray, new Vec2i(path[i].X, path[i].Y));

        }

        UpdateMovementGrid();
        UpdateAttackGrid(mouseLocation);

    }

    internal void RebuildInfo()
    {
        if (SelectedUnit != null && SelectedUnit.Targetable)
        {
            var voreTypes = State.RaceSettings.GetVoreTypes(SelectedUnit.Unit.Race);
            StatusUI.UnitName.text = SelectedUnit.Unit.Name;
            StatusUI.HP.text = "HP:" + SelectedUnit.Unit.Health + "/" + SelectedUnit.Unit.MaxHealth;
            StatusUI.MP.text = "AP:" + SelectedUnit.Movement;

            StatusUI.ZeroAPButton.interactable = true;
            StatusUI.MeleeButton.interactable = SelectedUnit.BestMelee != null;
            StatusUI.RangedButton.interactable = SelectedUnit.BestRanged != null;
            StatusUI.VoreButton.interactable = SelectedUnit.Unit.Predator;
            StatusUI.UndoMovement.interactable = startingLocation != null && (startingLocation != SelectedUnit.Position || startingMP > SelectedUnit.Movement);
            if (voreTypes.Count == 1)
            {
                var type = voreTypes[0];
                switch (type)
                {
                    case VoreType.Unbirth:
                        StatusUI.VoreButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Unbirth";
                        break;
                    case VoreType.CockVore:
                        StatusUI.VoreButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Cock Vore";
                        break;
                    case VoreType.BreastVore:
                        StatusUI.VoreButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Breast Vore";
                        break;
                    case VoreType.TailVore:
                        StatusUI.VoreButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Tail Vore";
                        break;
                    default:
                        StatusUI.VoreButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Vore";
                        break;
                }
            }
            else
            {
                StatusUI.VoreButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Vore";
            }


            CommandsUI.gameObject.SetActive(true);
            CommandsUI.SetUpButtons(SelectedUnit);


        }
        else if (SelectedUnit == null)
        {
            StatusUI.UnitName.text = "No unit selected";
            StatusUI.HP.text = "";
            StatusUI.MP.text = "";
            StatusUI.VoreButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Vore";
            StatusUI.UndoMovement.interactable = false;
            StatusUI.ZeroAPButton.interactable = false;
            CommandsUI.gameObject.SetActive(false);
        }
    }

    internal void UpdateHealthBars()
    {
        foreach (Actor_Unit actor in units)
        {
            actor.UnitSprite?.UpdateHealthBar(actor);
        }
    }




    void NextActor(NextUnitType type)
    {
        int startingIndex = units.IndexOf(SelectedUnit);
        int currentIndex = startingIndex + 1;
        ActionMode = 0;
        for (int i = 0; i < units.Count; i++)
        {
            if (currentIndex >= units.Count)
                currentIndex -= units.Count;
            if (units[currentIndex].Unit.Side == activeSide && TacticalUtilities.IsUnitControlledByPlayer(units[currentIndex].Unit) && units[currentIndex].Targetable && units[currentIndex].Movement > 0)
            {
                if (type == NextUnitType.Any || (type == NextUnitType.Melee && units[currentIndex].BestMelee.Damage > 2) || (type == NextUnitType.Ranged && units[currentIndex].BestRanged != null))
                {
                    SelectedUnit = units[currentIndex];
                    RebuildInfo();
                    State.GameManager.CenterCameraOnTile(units[currentIndex].Position.x, units[currentIndex].Position.y);
                    break;
                }

            }
            currentIndex++;
        }

    }

    void MoveActor()
    {
        if (Input.GetButtonDown("Melee"))
            ButtonCallback(0);
        if (Input.GetButtonDown("Ranged"))
            ButtonCallback(1);
        if (Input.GetButtonDown("Vore"))
            ButtonCallback(2);
        if (Input.GetButtonDown("Movement Mode"))
            ButtonCallback(7);

        if (SelectedUnit != null && SelectedUnit.Targetable && SelectedUnit.Movement > 0)
        {
            if (Input.GetButtonDown("Move Southwest"))
                ProcessMovement(SelectedUnit, 5);
            else if (Input.GetButtonDown("Move South"))
                ProcessMovement(SelectedUnit, 4);
            else if (Input.GetButtonDown("Move Southeast"))
                ProcessMovement(SelectedUnit, 3);
            else if (Input.GetButtonDown("Move East"))
                ProcessMovement(SelectedUnit, 2);
            else if (Input.GetButtonDown("Move Northeast"))
                ProcessMovement(SelectedUnit, 1);
            else if (Input.GetButtonDown("Move North"))
                ProcessMovement(SelectedUnit, 0);
            else if (Input.GetButtonDown("Move Northwest"))
                ProcessMovement(SelectedUnit, 7);
            else if (Input.GetButtonDown("Move West"))
                ProcessMovement(SelectedUnit, 6);
        }
        //if (Input.GetButtonDown("Special"))
        //    ButtonCallback(5);
    }

    void ProcessMovement(Actor_Unit unit, int moveCode)
    {
        if (unit.Move(moveCode, tiles))
        {
            RemoveHitPercentages();
            RebuildInfo();
            if (unit.Movement == 0)
                ActionMode = 0;
            if (ActionMode == 1)
                ShowMeleeHitPercentages(unit);
            else if (ActionMode == 2)
                ShowRangedHitPercentages(unit);
            else if (ActionMode == 3)
                ShowVoreHitPercentages(unit);
            else if (ActionMode == 4)
                ShowSpecialHitPercentages(unit);
        }
    }

    void ProcessLeftClick(int x, int y)
    {
        RightClickMenu.CloseAll();
        if ((!IsPlayerTurn && !PseudoTurn) || queuedPath != null)
            return;



        Vec2i clickLocation = new Vec2i(x, y);

        for (int i = 0; i < units.Count; i++)
        {
            Actor_Unit unit = units[i];

            if (unit.Position.GetDistance(clickLocation) < 1 && unit.Targetable == true)
            {

                if (ActionMode == 0)
                {
                    if (TacticalUtilities.IsUnitControlledByPlayer(unit.Unit) && unit.Unit.Side == activeSide)
                    {

                        if (SelectedUnit != units[i])
                        {
                            SelectedUnit = units[i];
                            RebuildInfo();
                        }
                        break;
                    }
                }
                if (SelectedUnit == null)
                    continue;
                if (ActionMode == 1)
                {
                    if ((!TacticalUtilities.IsUnitControlledByPlayer(unit.Unit) || (Config.AllowInfighting || (!AIDefender && !AIAttacker)) && unit != SelectedUnit) && !(!unit.UnitSprite.isActiveAndEnabled && State.World.IsNight))
                    {
                        MeleeAttack(SelectedUnit, unit);
                        return;
                    }

                }
                if (ActionMode == 2)
                {
                    if ((!TacticalUtilities.IsUnitControlledByPlayer(unit.Unit) || (Config.AllowInfighting || (!AIDefender && !AIAttacker)) && unit != SelectedUnit) && !(!unit.UnitSprite.isActiveAndEnabled && State.World.IsNight))
                    {
                        RangedAttack(SelectedUnit, unit);
                        return;
                    }

                }
                if (ActionMode == 3)
                {
                    if (TacticalUtilities.AppropriateVoreTarget(SelectedUnit, unit))
                    {
                        VoreAttack(SelectedUnit, unit);
                        return;
                    }

                }
                if (ActionMode == 4)
                {
                    if (TakeSpecialAction(specialType, SelectedUnit, unit))
                    {
                        RemoveHitPercentages();
                        ActionDone();
                        return;
                    }

                }
                if (ActionMode == 6)
                {

                    int distance = SelectedUnit.Position.GetNumberOfMovesDistance(unit.Position);
                    if (TacticalUtilities.MeetsQualifier(CurrentSpell.AcceptibleTargets, SelectedUnit, unit) && CurrentSpell.Range.Max >= distance && CurrentSpell.Range.Min <= distance)
                    {
                        CurrentSpell.TryCast(SelectedUnit, unit);
                        RemoveHitPercentages();
                        ActionDone();
                        return;
                    }

                }
                if (ActionMode == 7)
                {

                    int distance = SelectedUnit.Position.GetNumberOfMovesDistance(unit.Position);
                    if (3 >= distance)
                    {
                        CurrentPotion.ActivatePotion(SelectedUnit, unit);
                        RemoveHitPercentages();
                        ActionDone();
                        return;
                    }

                }
            }

        }
        for (int i = 0; i < units.Count; i++)
        {
            Actor_Unit unit = units[i];
            if (Config.EdibleCorpses && ActionMode == 3 && unit.Position.GetDistance(clickLocation) < 1 && unit.Targetable == false && unit.Visible && unit.Bulk() <= SelectedUnit.PredatorComponent.FreeCap())
            {
                var voreTypes = State.RaceSettings.GetVoreTypes(SelectedUnit.Unit.Race);
                if (voreTypes.Contains(VoreType.Oral))
                    SelectedUnit.PredatorComponent.Devour(unit);
                else
                    SelectedUnit.PredatorComponent.UsePreferredVore(unit);
                ActionDone();
            }
            else if (Config.EdibleCorpses && ActionMode == 4 && unit.Position.GetDistance(clickLocation) < 1 && unit.Targetable == false && unit.Visible && unit.Bulk() <= SelectedUnit.PredatorComponent.FreeCap())
            {
                if (TakeSpecialAction(specialType, SelectedUnit, unit))
                {
                    RemoveHitPercentages();
                    ActionDone();
                    return;
                }
            }
        }

        if (ActionMode == 4 && specialType == SpecialAction.Regurgitate)
        {
            if (TakeSpecialActionLocation(specialType, SelectedUnit, clickLocation))
            {
                RemoveHitPercentages();
                ActionDone();
                return;
            }
        }

        if (ActionMode == 5)
        {
            OrderSelectedUnitToMoveTo(x, y);
        }

        if (ActionMode == 6 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            int distance = SelectedUnit.Position.GetNumberOfMovesDistance(clickLocation);
            if (CurrentSpell.Range.Max >= distance && CurrentSpell.Range.Min <= distance && CurrentSpell.AcceptibleTargets.Contains(AbilityTargets.Tile))
            {
                CurrentSpell.TryCast(SelectedUnit, clickLocation);
                RemoveHitPercentages();
                ActionDone();
                return;
            }



        }


    }

    private bool unitControllableBySide(Actor_Unit unit, int side)
    {
        bool correctSide = unit.Unit.Side == side;
        bool controlOverridden = TacticalUtilities.GetMindControlSide(unit.Unit) != -1 || (unit.Unit.FixedSide != side && !unit.Unit.IsInfiltratingSide(side));
        return correctSide && !controlOverridden;
    }

    internal void VoreAttack(Actor_Unit actor, Actor_Unit unit)
    {
        var voreTypes = State.RaceSettings.GetVoreTypes(SelectedUnit.Unit.Race);
        if (voreTypes.Contains(VoreType.Oral))
            actor.PredatorComponent.Devour(unit);
        else
            actor.PredatorComponent.UsePreferredVore(unit);
        ActionDone();
    }

    internal void ActionDone()
    {
        ActionMode = 0;
        PlaceUndoMarker();
        RebuildInfo();
    }

    internal void RangedAttack(Actor_Unit actor, Actor_Unit unit)
    {
        actor.Attack(unit, true);
        ActionDone();
    }

    internal void MeleeAttack(Actor_Unit actor, Actor_Unit unit)
    {
        actor.Attack(unit, false);
        ActionDone();
    }

    void ProcessRightClick(int x, int y)
    {
        if (queuedPath != null)
            return;

        Vec2i clickLocation = new Vec2i(x, y);

        for (int i = 0; i < units.Count; i++)
        {
            Actor_Unit unit = units[i];

            if (unit.Position.GetDistance(clickLocation) < 1 && unit.Targetable == true)
            {
                if (TacticalUtilities.TileContainsMoreThanOneUnit(SelectedUnit.Position.x, SelectedUnit.Position.y))
                {
                    UndoMovement();
                    return;
                }
                RightClickMenu.Open(SelectedUnit, unit);
                return;
            }
        }

        if (Config.RightClickMenu || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (TacticalUtilities.TileContainsMoreThanOneUnit(SelectedUnit.Position.x, SelectedUnit.Position.y))
            {
                UndoMovement();
                return;
            }

            RightClickMenu.OpenWithNoTarget(SelectedUnit, new Vec2i(x, y));
        }
        else
        {
            OrderSelectedUnitToMoveTo(x, y);
        }





    }

    internal void OrderSelectedUnitToMoveTo(int x, int y)
    {
        arrowManager.ClearNodes();
        Vec2i clickLoc = new Vec2i(x, y);
        queuedPath = TacticalPathfinder.GetPath(SelectedUnit.Position, clickLoc, 0, SelectedUnit);
        ActionMode = 0;
    }

    //public void AddUnit(Actor_Unit newUnit)
    //{
    //    units.Add(newUnit);
    //}

    internal void SwitchAlignment(Actor_Unit actor)
    {
        int startingSide = actor.Unit.Side;
        if (actor.Unit.Side == defenderSide)
            AttackerConvert(actor);
        else
            DefenderConvert(actor);
        actor.DefectedThisTurn = startingSide != actor.Unit.Side;
        actor.sidesAttackedThisBattle = new List<int>();
    }

    internal bool IsDefender(Actor_Unit actor)
    {
        return actor.Unit.Side == defenderSide;
    }


    //public void CheckAlignment(Actor_Unit childUnit, Actor_Unit parentUnit)
    //{
    //    if (parentUnit.Unit.Side == defenderSide)
    //        DefenderConvert(childUnit);
    //    else
    //        AttackerConvert(childUnit);
    //}

    //void CheckAlignment(Actor_Unit childUnit, Actor_Unit parentUnit)
    //{
    //    if (!units.Contains(childUnit))
    //    {
    //        units.Add(childUnit);
    //    }
    //    if (childUnit.Unit.Side != parentUnit.Unit.Side && !childUnit.Unit.ImmuneToDefections)
    //    {
    //        childUnit.Unit.Side = parentUnit.Unit.Side;
    //    }
    //    if (armies[0].Units.Contains(parentUnit.Unit))
    //    {
    //        AttackerConvert(childUnit);
    //    }
    //    else
    //    {
    //        DefenderConvert(childUnit);
    //    }
    //    childUnit.UnitSprite.BlueColored = parentUnit.UnitSprite.BlueColored;
    //}

    internal void DefenderConvert(Actor_Unit actor)
    {
        if (armies[1]?.Units.Contains(actor.Unit) ?? false || extraDefenders.Contains(actor))
            return;
        armies[0].Units.Remove(actor.Unit);
        extraAttackers.Remove(actor);
        actor.Unit.Side = defenderSide;
        UpdateActorColor(actor);
        if (actor.Unit.Type != UnitType.Summon)
        {
            if (armies[1] != null && StrategicUtilities.ArmyCanFitUnit(armies[1], actor.Unit))
            {
                armies[1].Units.Add(actor.Unit);
            }
            else
            {
                extraDefenders.Add(actor);
            }
        }
    }

    internal void AttackerConvert(Actor_Unit actor)
    {
        if (armies[0].Units.Contains(actor.Unit) || extraAttackers.Contains(actor))
            return;
        armies[1]?.Units.Remove(actor.Unit);
        extraDefenders.Remove(actor);
        village?.GetRecruitables().Remove(actor.Unit);
        actor.Unit.Side = armies[0].Side;
        UpdateActorColor(actor);
        if (actor.Unit.Type != UnitType.Summon)
        {
            if (armies[0] != null && StrategicUtilities.ArmyCanFitUnit(armies[0], actor.Unit))
            {
                armies[0].Units.Add(actor.Unit);
            }
            else
            {
                extraAttackers.Add(actor);
            }
        }
        garrison.Remove(actor);
    }


    void EndTurn()
    {
        if (waitingForDialog)
            return;
        if (PseudoTurn)
        {
            SkipPseudo = true;
            PseudoTurn = false;
            StatusUI.EndTurn.interactable = false;
            return;
        }
        SkipPseudo = false;
        if (Config.AutoUseAI && IsPlayerInControl && repeatingTurn == false)
        {
            repeatingTurn = true;
            ButtonCallback(3);
            return;
        }
        repeatingTurn = false;
        IgnorePseudo = false;
        RightClickMenu.CloseAll();
        if (State.TutorialMode && State.GameManager.TutorialScript.step < 6)
            return;
        ActionMode = 0;
        SelectedUnit = null;
        SkipUI.gameObject.SetActive(false);
        SelectionBox.gameObject.SetActive(false);
        IsPlayerTurn = true;
        if (attackersTurn)
        {
            attackersTurn = false;
            attackersTurnCheck = false;
            activeSide = defenderSide;
            currentAI = defenderAI;
            NewTurn();
            if (AIDefender)
                IsPlayerTurn = false;
        }
        else
        {
            attackersTurn = true;
            attackersTurnCheck = true;
            currentTurn++;
            activeSide = armies[0].Side;
            currentAI = attackerAI;
            NewTurn();
            if (AIAttacker)
                IsPlayerTurn = false;
        }

        ProcessTileEffects();

        StatusUI.SkipToEndButton.interactable = true;
        UpdateEndTurnButtonText();
        StatusUI.EndTurn.interactable = IsPlayerTurn;
        EnemyTurnText.SetActive(!IsPlayerTurn);
        if (IsPlayerTurn == false)
            AITimer = .2f;
        if (attackersTurn)
        {
            StatusUI.AttackerText.fontStyle = FontStyle.Bold;
            StatusUI.DefenderText.fontStyle = FontStyle.Normal;
        }
        else
        {
            StatusUI.AttackerText.fontStyle = FontStyle.Normal;
            StatusUI.DefenderText.fontStyle = FontStyle.Bold;
        }
        if (reviewingBattle)
            StatusUI.EndTurn.interactable = true;
    }

    internal void UpdateEndTurnButtonText()
    {
        if (Config.DisplayEndOfTurnText)
        {
            string str = "";
            if (Config.AutoAdvance == Config.AutoAdvanceType.DoNothing)
                str = "Manual";
            else if (Config.AutoAdvance == Config.AutoAdvanceType.AdvanceTurns)
                str = "Quick";
            else if (Config.AutoAdvance == Config.AutoAdvanceType.SkipToEnd)
                str = "Skip";

            StatusUI.EndTurn.GetComponentInChildren<UnityEngine.UI.Text>().text = $"End Turn ({str})";
        }
        else
            StatusUI.EndTurn.GetComponentInChildren<UnityEngine.UI.Text>().text = "End Turn";
    }

    public bool CanDefect(Actor_Unit unit)
    {
        if (unit.Possessed > 0 || unit.DefectedThisTurn) return false;
        return TacticalUtilities.GetPreferredSide(unit.Unit, activeSide, attackersTurn ? defenderSide : attackerSide) != activeSide
                    || units.Any(u => u.Unit.Side != unit.Unit.Side && u.Targetable && u.Visible && !u.Fled) && !units.Any(u => TacticalUtilities.TreatAsHostile(unit, u) && u.Targetable && u.Visible && !u.Fled);
                
    }

    void NewTurn()
    {
        AllSurrenderedCheck();
        Log.RegisterNewTurn(attackersTurn ? AttackerName : DefenderName, currentTurn);

        for (int i = 0; i < units.Count; i++)
        {
            if (units[i].Unit.IsDead == false && units[i].Unit.Side != activeSide)
            {
                //You seem to be causing issues, but I may need you for reference later.
                /*if (Config.KuroTenkoEnabled)
                {
                    List<Actor_Unit> released;
                    released = units[i].BirthCheck();
                    foreach (Actor_Unit child in released)
                    {
                        CheckAlignment(child, units[i]);
                    }
                }*/
                units[i].RubCount = 0; // Hedonists now get just as much benefit out of mind-control effects
                units[i].DigestCheck(); //Done first so that freed units are checked properly below

            }
            if (units[i].SelfPrey != null)
                units[i].SelfPrey.TurnsSinceLastDamage++;

            foreach (var item in units[i].Unit.AllConditionalTraits.Keys.Where(t => t.trigger == TraitConditionTrigger.OnTacticalTurnStart || t.trigger == TraitConditionTrigger.All).ToList())
            {
                if (ConditionalTraitConditionChecker.TacticalTraitConditionActive(units[i], item))
                {
                    units[i].Unit.ActivateConditionalTrait(item.id);
                }
                else
                {
                    units[i].Unit.DeactivateConditionalTrait(item.id);
                }
            }

        }
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i].Unit.IsDead == false && units[i].Unit.Side == activeSide)
            {
                units[i].allowedToDefect = CanDefect(units[i]);
                units[i].DefectedThisTurn = false;
                units[i].NewTurn();
            }
            if (units[i].Unit.IsDead && units[i].Unit.Side == activeSide)
            {
                if (units[i].Targetable)
                    units[i].Damage(0);
            }
        }
        for (int i = 0; i < units.Count; i++)
        {
            units[i].PredatorComponent?.UpdateFullness(); //Catches any sizes changed by expiration of shrink / expand
        }
        for (int i = 0; i < RetreatedDigestors.Count; i++)
        {
            if (RetreatedDigestors[i].Unit.IsDead == false)
            {
                if (RetreatedDigestors[i].Unit.Side == activeSide)
                {
                    RetreatedDigestors[i].DigestCheck();
                }
                if (RetreatedDigestors[i].Unit.HasTrait(Traits.FriendlyStomach) || RetreatedDigestors[i].Unit.HasTrait(Traits.Endosoma))
                {
                    foreach (var prey in RetreatedDigestors[i].PredatorComponent.GetAllPrey())
                    {
                        if (!TacticalUtilities.TreatAsHostile(RetreatedDigestors[i], prey.Actor) && prey.Actor.Fled == false)
                        {
                            RetreatUnit(prey.Actor, prey.Unit.Side == defenderSide);
                        }
                    }
                }
            }
        }
        if (turboMode == false)
        {
            RebuildInfo();
            autoAdvancing = Config.AutoAdvance > 0 && IsOnlyOneSideVisible();
            VictoryCheck();
        }
    }

    void AllSurrenderedCheck()
    {
        bool allSurrendered = true;
        foreach (Actor_Unit actor in units)
        {
            if (actor.Unit.IsDead == false && actor.Surrendered == false && actor.Visible && actor.Fled == false)
                allSurrendered = false;
        }
        if (allSurrendered)
        {
            bool flipped = false;
            foreach (Actor_Unit actor in units)
            {
                if (actor.Surrendered)
                    flipped = true;
                actor.Surrendered = false;
            }
            if (flipped)
            {
                if (turboMode == false && State.GameManager.CurrentScene == State.GameManager.TacticalMode)
                    State.GameManager.CreateMessageBox("After several minutes of staring at each other doing nothing, all of the surrendered units decided to fight again");
                else
                    Debug.Log("The AI ended up needing an all surrendered reset... this probably shouldn't have happened");
            }
            else
                Debug.Log("All units had apparently surrendered without any units surrendered.  I'm guessing it has to do with fleeing units");

        }
    }

    internal bool IsOnlyOneSideVisible()
    {
        List<Actor_Unit> visibleAttackers = new List<Actor_Unit>();
        List<Actor_Unit> visibleDefenders = new List<Actor_Unit>();

        for (int i = 0; i < units.Count; i++)
        {
            if (units[i] != null && units[i].Fled == false)
            {
                Actor_Unit actor = units[i];
                if (actor.Targetable)
                {
                    if (actor.Unit.Side == armies[0].Side)
                    {
                        visibleAttackers.Add(actor);
                    }
                    else
                    {
                        visibleDefenders.Add(actor);
                    }
                }
            }
        }
        bool oneSideLeft = false;
        if (visibleAttackers.Count() == 0)
        {
            oneSideLeft = !visibleDefenders.Any(vd => !vd.Unit.hiddenFixedSide && TacticalUtilities.GetPreferredSide(vd.Unit, defenderSide, attackerSide) == attackerSide); // They are probably still fighting in this case
        }
        if (visibleDefenders.Count() == 0)
        {
            oneSideLeft = !visibleAttackers.Any(vd => !vd.Unit.hiddenFixedSide && TacticalUtilities.GetPreferredSide(vd.Unit, attackerSide, defenderSide) == defenderSide); // They are probably still fighting in this case
        }
        autoAdvanceTimer = AutoAdvanceRate;
        AutoAdvanceText.SetActive(oneSideLeft && Config.AutoAdvance > Config.AutoAdvanceType.DoNothing);
        return oneSideLeft;

    }


    bool VictoryCheck()
    {
        if (waitingForDialog)
            return false;
        int remainingAttackers = 0;
        int remainingDefenders = 0;
        CalculateRemaining(ref remainingAttackers, ref remainingDefenders);

        if (remainingAttackers == 0 || remainingDefenders == 0)
        {
            foreach (Actor_Unit actor in units.ToList())
            {
                if (actor.Targetable && actor.Visible && !actor.Fled && !actor.Surrendered && (actor.TurnsSinceLastDamage < 2 & !actor.Unit.HasTrait(Traits.CurseOfImmolation))) return false;
                if (actor.Targetable && actor.Visible && !actor.Fled && !actor.Surrendered && !actor.Unit.hiddenFixedSide && units.Any(u => u.Targetable && !u.Fled && u.Visible && TacticalUtilities.TreatAsHostile(actor, u))) return false;
                if (actor.Unit.Predator == false)
                    continue;
                foreach (var prey in actor.PredatorComponent.GetDirectPrey().Where(s => s.Unit.HasTrait(Traits.TheGreatEscape)).ToList())
                {
                    actor.PredatorComponent.FreeGreatEscapePrey(prey);
                    RetreatUnit(prey.Actor, prey.Unit.Side == defenderSide);
                }
                if (actor.Unit.HasTrait(Traits.Endosoma))
                {
                    foreach (var prey in actor.PredatorComponent.GetDirectPrey().Where(s => s.Unit.Stamina <= 0).ToList())
                    {
                        actor.PredatorComponent.FreeEndoPrey(prey);
                        if (actor.Unit.HasTrait(Traits.Friendosoma))
                        {
                            SwitchAlignment(prey.Actor);
                            RetreatUnit(prey.Actor, prey.Unit.Side == defenderSide);
                        }
                        else
                        {
                            prey.Actor.Unit.Health = 0;
                            prey.Actor.Unit.Kill();
                            foreach (var item in prey.Actor.Unit.AllConditionalTraits.Keys.Where(t => t.trigger == TraitConditionTrigger.OnDeath || t.trigger == TraitConditionTrigger.All).ToList())
                            {
                                if (ConditionalTraitConditionChecker.TacticalTraitConditionActive(prey.Actor, item))
                                {
                                    prey.Actor.Unit.ActivateConditionalTrait(item.id);
                                }
                                else
                                {
                                    prey.Actor.Unit.DeactivateConditionalTrait(item.id);
                                }
                            }
                        }

                    }
                }
                if (actor.Fled == false)
                    continue;
                for (int i = 0; i < 1000; i++)
                {
                    if (actor.PredatorComponent.DigestingUnitCount < 1)
                        break;
                    actor.DigestCheck();
                }
            }
            remainingAttackers = 0;
            remainingDefenders = 0;
            CalculateRemaining(ref remainingAttackers, ref remainingDefenders);
            if (units.Where(s => s.Unit.IsDead == false && s.PredatorComponent?.DigestingUnitCount > 0).FirstOrDefault() != null)
            {
                return false;
            }
            if (remainingAttackers > 0 && remainingDefenders > 0)
            {
                Debug.Log("It's a battle again, an escaped unit came back to the battlefield - going back to minimize undefined behavior");
                return false;
            }

            if (remainingAttackers > 0 && AIAttacker == false)
            {
                if (FledReturn == ChoiceOption.Default)
                {
                    int fledAttackers = units.Where(s => s.Unit.Side == armies[0].Side && s.Fled).Count();
                    if (fledAttackers > 0)
                    {
                        var box = State.GameManager.CreateDialogBox();
                        waitingForDialog = true;
                        box.SetData(() => { FledReturn = ChoiceOption.Yes; waitingForDialog = false; VictoryCheck(); }, "Return to army", "Flee to a town", "You've won the battle, should your units that retreated rejoin your forces, or keep retreating to a town?", () => { FledReturn = ChoiceOption.No; waitingForDialog = false; VictoryCheck(); });
                        return false;
                    }
                }
            }

            if (remainingDefenders > 0 && AIDefender == false)
            {
                if (FledReturn == ChoiceOption.Default)
                {
                    int fledDefenders = units.Where(s => s.Unit.Side == defenderSide && s.Fled).Count();
                    if (fledDefenders > 0)
                    {
                        var box = State.GameManager.CreateDialogBox();
                        waitingForDialog = true;
                        box.SetData(() => { FledReturn = ChoiceOption.Yes; waitingForDialog = false; VictoryCheck(); }, "Return to army", "Flee to a town", "You've won the battle, should your units that retreated rejoin your forces, or keep retreating to a town?", () => { FledReturn = ChoiceOption.No; waitingForDialog = false; VictoryCheck(); });
                        return false;
                    }
                }
            }

            if (Config.StopAtEndOfBattle && reviewingBattle == false && turboMode == false)
            {
                reviewingBattle = true;
                autoAdvancing = false;
                BattleReviewText.SetActive(true);
                foreach (Actor_Unit actor in units)
                {
                    actor.Movement = 0;
                    actor.DigestCheck();
                    actor.Update(2);
                    actor.UnitSprite.UpdateSprites(actor, actor.Unit.Side == activeSide);
                }
                StatusUI.EndTurn.interactable = true;
                return false;
            }
            ProcessReplaceable(remainingAttackers);

            foreach (Actor_Unit actor in units)
            {
                actor.Unit.GiveExp(4);
                if (actor.Unit.TraitBoosts.HealthRegen > 0 && actor.Unit.IsDead == false)
                    actor.Unit.HealPercentage(1);
                if (actor.Unit.GetStatusEffect(StatusEffectType.Morphed) != null)
                {
                    actor.Unit.RevertMorph(actor.Unit.GetStatusEffect(StatusEffectType.Morphed).Applicator);
                }
                actor.Unit.StatusEffects.Clear();

                actor.Unit.SetBarrier(0);
                
                EquipmentFunctions.CheckEquipment(actor.Unit, EquipmentActivator.OnTacticalBattleEnd, new object[] { actor, armies[actor.Unit.Side == attackerSide ? 0 : 1], null });

                // Refill used potions
                foreach (var potion in actor.Unit.EquippedPotions)
                {
                    int WantedPots = potion.Value[1];
                    while (WantedPots > actor.Unit.EquippedPotions[potion.Key][0] && armies[actor.Unit.Side == attackerSide ? 0 : 1].ItemStock.HasItem((ItemType)potion.Key))
                    {
                        armies[actor.Unit.Side == attackerSide ? 0 : 1].ItemStock.TakeItem((ItemType)potion.Key);
                        actor.Unit.EquippedPotions[potion.Key][0] = actor.Unit.EquippedPotions[potion.Key][0] + 1;
                    }
                }

            }
            BattleReviewText.SetActive(false);
            foreach (Actor_Unit actor in units.ToList())
            {

                if (actor.Unit.IsDead)
                {
                    if (defenderBuildingsInRange != null)
                    {
                        IEnumerable<BlackMagicTower> possible_BlackTowers = defenderBuildingsInRange.Where(b => b is BlackMagicTower).Cast<BlackMagicTower>();
                        foreach (BlackMagicTower tower in possible_BlackTowers)
                        {
                            tower.SoulPower += actor.Unit.Level * (tower.soulUpgrade.built ? 2 : 1);
                        }
                    }
                    if (attackerBuildingsInRange != null)
                    {
                        IEnumerable<BlackMagicTower> possible_BlackTowers = attackerBuildingsInRange.Where(b => b is BlackMagicTower).Cast<BlackMagicTower>();
                        foreach (BlackMagicTower tower in possible_BlackTowers)
                        {
                            tower.SoulPower += actor.Unit.Level * (tower.soulUpgrade.built ? 2 : 1);
                        }
                    }
                }

                if (actor.Unit.Type == UnitType.Reinforcement && actor.Unit.IsDead == false)
                {
                    IEnumerable<DefenseEncampment> possible_camps = defenderBuildingsInRange.Where(b => b is DefenseEncampment).Cast<DefenseEncampment>();
                    if (possible_camps != null)
                    {
                        DefenseEncampment camp = possible_camps.Where(d => d.maxDefenders > d.AvailibleDefenders).First();
                        if (camp != null)
                            camp.AvailibleDefenders++;
                    }
                    units.Remove(actor);
                    continue;
                }
                actor.Unit.SetSizeToDefault();
                actor.Unit.EnemiesKilledThisBattle = 0;
                if (actor.Unit.IsDead && actor.Unit.Type != UnitType.Summon &&
                    (actor.Unit.HasTrait(Traits.Eternal) || (actor.Unit.HasTrait(Traits.LuckySurvival) && State.Rand.Next(5) != 0) ||
                    (actor.Unit.HasTrait(Traits.Reformer) && actor.KilledByDigestion) ||
                    (actor.Unit.HasTrait(Traits.Revenant) && actor.KilledByDigestion == false) ||
                    (actor.Unit.HasTrait(Traits.DeathCheater) && actor.Unit.TimesKilled <= 9 && (State.Rand.Next(10) >= (actor.Unit.TimesKilled - 1))) ||
                    (actor.Unit.HasTrait(Traits.DeathCheater) && actor.Unit.TimesKilled >= 10 && (State.Rand.Next(10) == 0))
                    ))
                {
                    actor.Surrendered = false;
                    actor.Unit.Health = actor.Unit.MaxHealth;
                    if (actor.Unit.Side == defenderSide)
                    {
                        if (garrison.Contains(actor) && remainingDefenders > 0)
                        {
                            actor.Unit.Health = actor.Unit.MaxHealth;
                        }
                        else
                        {
                            retreatedDefenders.Add(actor.Unit);
                            armies[1]?.Units.Remove(actor.Unit);
                            village?.GetRecruitables().Remove(actor.Unit);
                        }

                    }
                    else
                    {
                        retreatedAttackers.Add(actor.Unit);
                        armies[0].Units.Remove(actor.Unit);
                    }
                    actor.PredatorComponent?.PurgePrey();
                    units.Remove(actor);
                }
                else if ((actor.Unit.HasTrait(Traits.Transmigration) || actor.Unit.HasTrait(Traits.InfiniteTransmigration)) && actor.KilledByDigestion && actor.Unit.IsDead
                    && actor.Unit.Type != UnitType.Summon && actor.Unit.Type != UnitType.Leader && actor.Unit.Type != UnitType.SpecialMercenary)
                {
                    if (State.World.MainEmpires != null)
                    {
                        Race race = actor.Unit.KilledBy.Race;
                        if (State.World.Reincarnators == null)
                            State.World.Reincarnators = new List<Reincarnator>();
                        if (!State.World.Reincarnators.Any(rc => rc.PastLife == actor.Unit))
                        {
                            actor.Unit.RemoveTrait(Traits.Transmigration);
                            State.World.Reincarnators.Add(new Reincarnator(actor.Unit, race, true));
                            State.World.GetEmpireOfSide(actor.Unit.Side)?.Reports.Add(new StrategicReport($"{actor.Unit.Name} will reincarnate as a {InfoPanel.RaceSingular(actor.Unit.KilledBy)}.", new Vec2(0, 0)));

                        }
                    }
                }
                else if ((actor.Unit.HasTrait(Traits.Reincarnation) || actor.Unit.HasTrait(Traits.InfiniteReincarnation)) && actor.Unit.IsDead 
                    && actor.Unit.Type != UnitType.Summon && actor.Unit.Type != UnitType.Leader && actor.Unit.Type != UnitType.SpecialMercenary)
                {
                    if (State.World.MainEmpires != null)
                    {
                        List<Race> activeRaces = StrategicUtilities.GetAllUnits(false).ConvertAll(u => u.Race).Distinct()
                            .ToList();
                        if (activeRaces.Any())
                        {
                            Race race = activeRaces[State.Rand.Next(activeRaces.Count)];
                            if (State.World.Reincarnators == null)
                                State.World.Reincarnators = new List<Reincarnator>();
                            if (!State.World.Reincarnators.Any(rc => rc.PastLife == actor.Unit))
                            {
                                actor.Unit.RemoveTrait(Traits.Reincarnation);
                                State.World.Reincarnators.Add(new Reincarnator(actor.Unit, race));
                                State.World.GetEmpireOfSide(actor.Unit.Side)?.Reports.Add(new StrategicReport($"{actor.Unit.Name} will reincarnate as a random race.", new Vec2(0, 0)));
                        }
                        }
                    } 
                }
                else if (actor.Fled)
                    units.Remove(actor);
                else if (actor.Unit.IsDead && actor.Unit.SavedCopy != null && ((!State.World.Reincarnators?.Any(rc => rc.PastLife == actor.Unit)) ?? true))
                {
                    var emp = State.World.GetEmpireOfSide(actor.Unit.Side);
                    var vill = actor.Unit.SavedVillage;
                    if (actor.Unit.Side == defenderSide)
                    {
                        armies[1]?.Units.Remove(actor.Unit);
                        village?.GetRecruitables().Remove(actor.Unit);
                    }
                    else
                    {
                        armies[0].Units.Remove(actor.Unit);
                    }
                    armies[1]?.Units.Remove(actor.Unit);
                    units.Remove(actor);
                    if (vill != null && vill.Empire.IsAlly(emp))
                    {
                        actor.Unit.SavedVillage.AddPopulation(1);
                        actor.Unit.SavedVillage.GetRecruitables().Add(actor.Unit.SavedCopy);
                        actor.Unit.SavedCopy.SavedCopy = null;
                        actor.Unit.SavedCopy.SavedVillage = null;
                        emp.Reports.Add(new StrategicReport($"{actor.Unit.Name}'s soul has returned the imprinted copy in the village of {vill.Name}", vill.Position));
                        if (State.GameManager.StrategyMode.IsPlayerTurn)
                        {
                            State.GameManager.StrategyMode.NewReports = true;
                        }
                    }
                }
            }
            EatSurrenderedAllies();
            SelectedUnit = null;
            if (remainingAttackers > 0)
            {
                State.World.Stats?.BattleResolution(armies[0].Side, defenderSide);
                if ((FledReturn == ChoiceOption.Yes || FledReturn == ChoiceOption.Default) && retreatedAttackers != null) //Default is to catch eternal units when no units fled
                {
                    if (armies[0] != null) armies[0].Units.AddRange(retreatedAttackers);
                    retreatedAttackers.Clear();
                }

                StrategicUtilities.TryClaim(armies[0].Position, armies[0].Empire);
            }
            else
            {
                State.World.Stats?.BattleResolution(defenderSide, armies[0].Side);
                State.World.Stats?.LostArmy(armies[0].Side);
                if (FledReturn == ChoiceOption.Yes || FledReturn == ChoiceOption.Default && retreatedDefenders != null)//Default is to catch eternal units when no units fled)
                {
                    if (armies[1] != null) armies[1].Units.AddRange(retreatedDefenders);
                    else if (village != null) village.GetRecruitables().AddRange(retreatedDefenders);
                    retreatedDefenders.Clear();
                }
            }
            if (remainingAttackers > 0 && extraAttackers != null && extraAttackers.Any())
            {
                AssignLeftoverTroops(armies[0], extraAttackers);
            }
            else if (remainingDefenders > 0 && extraDefenders != null && extraDefenders.Any())
            {
                AssignLeftoverTroops(armies[1], extraDefenders);
            }
                

            ProcessFledUnits();
            remainingAttackers = 0;
            remainingDefenders = 0;
            CalculateRemaining(ref remainingAttackers, ref remainingDefenders);
            if (remainingAttackers > 0 && remainingDefenders > 0)
            {
                //This is just a back-up incase there's some other method of causing the issue I fixed
                VictoryCheck();
                return false;
            }
            ProcessConsumedCorpses();
            if (village != null)
            {
                TacticalUtilities.CleanVillage(remainingAttackers);
            }
            bool skipStats = false;
            if (AIAttacker && AIDefender && Config.SkipAIOnlyStats)
                skipStats = true;
            string attackerReceives = "";
            string defenderReceives = "";
            if (remainingAttackers > 0)
            {
                if (armies[1]?.BountyGoods != null)
                {
                    attackerReceives = armies[1].BountyGoods.ApplyToArmyOrVillage(armies[0]);
                }
                Wins.x++;
            }
            else
            {
                if (armies[0]?.BountyGoods != null)
                {
                    defenderReceives = armies[0].BountyGoods.ApplyToArmyOrVillage(armies[1], village);
                }
                Wins.y++;
            }

            LootItems(remainingDefenders, ref attackerReceives, ref defenderReceives);

            if (skipStats == false && (turboMode == false || Config.ShowStatsForSkippedBattles || manualSkip || (Config.BattleReport && State.GameManager.CurrentPreviewSkip == GameManager.PreviewSkip.SkipWithStats)))
            {
                int remainingGarrison = 0;
                if (garrison != null)
                    remainingGarrison = garrison.Where(s => s.Unit.IsDead == false).Count();
                string defenderNames = "";
                if (armies[1] != null)
                    defenderNames += $"{armies[1].Name}\n";
                if (village != null && (garrison?.Any() ?? false))
                    defenderNames += $"{village.Name} Garrison\n";
                State.GameManager.StatScreen.AttackerTitle.text = $"{AttackerName} - Attacker\n{armies[0].Name}";
                State.GameManager.StatScreen.DefenderTitle.text = $"{DefenderName} - Defender\n{defenderNames}";
                List<Unit> defenders = new List<Unit>();
                if (armies[1] != null) defenders.AddRange(armies[1].Units);
                if (garrison != null) defenders.AddRange(garrison.Select(s => s.Unit));
                double endingAttackerPower = StrategicUtilities.UnitValue(armies[0].Units.Where(s => s.IsDead == false).ToList());
                double endingDefenderPower = StrategicUtilities.UnitValue(defenders.Where(s => s.IsDead == false).ToList());
                State.GameManager.StatScreen.VictoryType.text = TacticalStats.OverallSummary(StartingAttackerPower, endingAttackerPower, StartingDefenderPower, endingDefenderPower, remainingAttackers > 0);
                State.GameManager.StatScreen.AttackerText.text = TacticalStats.AttackerSummary(remainingAttackers) + attackerReceives;
                State.GameManager.StatScreen.DefenderText.text = TacticalStats.DefenderSummary(remainingDefenders - remainingGarrison, remainingGarrison) + defenderReceives;
                State.GameManager.StatScreen.Open(AIAttacker && AIDefender);

            }
            else
                State.GameManager.SwitchToStrategyMode();
            autoAdvancing = false;
            return true;
        }
        return false;
    }

    private void ProcessReplaceable(int remainingAttackers)
    {
        if (remainingAttackers > 0)
        {
            foreach (Actor_Unit actor in units.Where(s => s.Unit.Side != defenderSide))
            {
                if (actor.Unit.HasTrait(Traits.Replaceable) && actor.Unit.IsDead)
                {
                    actor.Unit.SetExp(actor.Unit.Experience * .5f);
                    if (actor.Unit.Experience < armies[0].Empire.StartingXP)
                        actor.Unit.SetExp(armies[0].Empire.StartingXP);
                    RunReplace(actor);
                }
            }
        }
        else
        {
            foreach (Actor_Unit actor in units.Where(s => s.Unit.Side == defenderSide))
            {
                if (actor.Unit.HasTrait(Traits.Replaceable) && actor.Unit.IsDead)
                {
                    actor.Unit.SetExp(actor.Unit.Experience * .5f);
                    var defEmp = State.World.GetEmpireOfSide(defenderSide);
                    var startingExp = defEmp?.StartingXP ?? 0;
                    if (actor.Unit.Experience < startingExp)
                        actor.Unit.SetExp(startingExp);
                    RunReplace(actor);
                }
            }
        }
        void RunReplace(Actor_Unit actor)
        {
            for (int i = 0; i < 20; i++)
            {
                if (actor.Unit.Experience < actor.Unit.GetExperienceRequiredForLevel(actor.Unit.Level - 1))
                    actor.Unit.LevelDown();
                else
                    break;
            }
            var raceData = Races.GetRace(actor.Unit);
            actor.Unit.RandomizeNameAndGender(actor.Unit.Race, raceData);
            raceData.RandomCustom(actor.Unit);
            actor.Unit.DigestedUnits = 0;
            actor.Unit.KilledUnits = 0;
            actor.Unit.Health = actor.Unit.MaxHealth;
            actor.PredatorComponent?.PurgePrey();
        }
    }

    private void LootItems(int remainingDefenders, ref string attackerReceives, ref string defenderReceives)
    {
        bool attackerFoundSpell = false;
        bool defenderFoundSpell = false;
        List<Item> items = new List<Item>();
        foreach (Actor_Unit actor in units)
        {

            if (actor.Unit.IsDead && actor.Unit.Type != UnitType.Leader)
            {
                foreach (Item item in actor.Unit.Items)
                {
                    if (item is SpellBook)
                    {
                        if (remainingDefenders > 0)
                        {
                            if (armies[1] != null)
                            {
                                armies[1].ItemStock.AddItem(State.World.ItemRepository.GetItemType(item));
                                items.Add(item);
                                defenderFoundSpell = true;
                            }
                            else
                            {
                                village.ItemStock.AddItem(State.World.ItemRepository.GetItemType(item));
                                items.Add(item);
                                defenderFoundSpell = true;
                            }
                        }
                        else
                        {
                            armies[0].ItemStock.AddItem(State.World.ItemRepository.GetItemType(item));
                            items.Add(item);
                            attackerFoundSpell = true;
                        }


                    }
                }

            }

        }
        if (remainingDefenders > 0)
        {
            if (armies[1] != null)
            {
                if (armies[0].ItemStock.TransferAllItems(armies[1].ItemStock, ref items))
                    defenderFoundSpell = true;
            }
            else
            {
                if (armies[0].ItemStock.TransferAllItems(village.ItemStock, ref items))
                    defenderFoundSpell = true;
            }
        }
        else
        {
            if (armies[1] != null)
            {
                if (armies[1].ItemStock.TransferAllItems(armies[0].ItemStock, ref items))
                    attackerFoundSpell = true;
            }
            if (village?.ItemStock.TransferAllItems(armies[0].ItemStock, ref items) ?? false)
                attackerFoundSpell = true;
        }
        string itemString = "";
        if (items.Count > 0)
        {
            itemString = string.Join(", ", items.Distinct().Select(s => s.Name));
        }
        if (defenderFoundSpell)
            defenderReceives += $"<color=yellow>Received Items</color>\n{itemString}";
        if (attackerFoundSpell)
            attackerReceives += $"<color=yellow>Received Items</color>\n{itemString}";
    }

    private void CalculateRemaining(ref int remainingAttackers, ref int remainingDefenders)
    {
        int surrenderedAttackers = 0;
        int surrenderedDefenders = 0;

        for (int i = 0; i < units.Count; i++)
        {
            if (units[i] != null && units[i].Fled == false)
            {
                Actor_Unit actor = units[i];
                if (actor.Unit.IsDead == false)
                {
                    //if (actor.SelfPrey == null && actor.Visible == false || actor.Targetable == false)
                    //{
                    //    actor.Visible = true;
                    //    actor.Targetable = true;
                    //}
                    if ((actor.SelfPrey?.Predator == null || actor.SelfPrey?.Predator.PredatorComponent?.IsActorInPrey(actor) == false || actor.SelfPrey.TurnsSinceLastDamage > 3 && (actor.SelfPrey.Predator.Unit.HasTrait(Traits.FriendlyStomach) || actor.SelfPrey.Predator.Unit.HasTrait(Traits.Endosoma))) && actor.Unit.IsDead == false && actor.Visible == false && actor.Targetable == false)
                    {
                        actor.SelfPrey = null;
                        Debug.Log("Prey orphan found, fixing");
                        actor.Targetable = true;
                        actor.Visible = true;
                    }
                    if (actor.SelfPrey?.Predator != null && (actor.SelfPrey.Predator.Unit?.IsDead ?? true))
                    {
                        actor.SelfPrey.Predator.PredatorComponent.FreeAnyAlivePrey();
                    }
                    if (actor.Unit.Side == armies[0].Side)
                    {
                        remainingAttackers++;
                        if ((actor.SelfPrey != null && actor.Unit.HasTrait(Traits.TheGreatEscape) || (actor.SelfPrey != null && actor.Unit.Stamina <= 0)))
                            remainingAttackers--;
                        if (actor.Surrendered)
                            surrenderedAttackers++;
                        var preyCount = actor.PredatorComponent?.PreyCount ?? 0;
                        if (preyCount > 0)
                        {
                            remainingDefenders += preyCount;
                            if ((actor.Unit.HasTrait(Traits.FriendlyStomach) || actor.Unit.HasTrait(Traits.Endosoma)))
                            {
                                remainingDefenders -= actor.PredatorComponent.GetDirectPrey().Where(s => actor.Unit.Side == s.Unit.Side || s.Unit.HasTrait(Traits.TheGreatEscape) || s.Unit.Stamina <= 0).Count();
                            }
                            else
                            {
                                remainingDefenders -= actor.PredatorComponent.GetDirectPrey().Where(s => s.Unit.HasTrait(Traits.TheGreatEscape)).Count(); 
                            }
                        }
                    }
                    else
                    {
                        remainingDefenders++;
                        if ((actor.SelfPrey != null && actor.Unit.HasTrait(Traits.TheGreatEscape) || (actor.SelfPrey != null && actor.Unit.Stamina <= 0)))
                            remainingDefenders--;
                        if (actor.Surrendered)
                            surrenderedDefenders++;
                        var preyCount = actor.PredatorComponent?.PreyCount ?? 0;
                        if (preyCount > 0)
                        {
                            remainingAttackers += preyCount;
                            if (actor.Unit.HasTrait(Traits.FriendlyStomach) || actor.Unit.HasTrait(Traits.Endosoma))
                            {
                                remainingAttackers -= actor.PredatorComponent.GetDirectPrey().Where(s => actor.Unit.Side == s.Unit.Side || s.Unit.HasTrait(Traits.TheGreatEscape) || s.Unit.Stamina <= 0).Count();
                            }
                            else
                            {
                                remainingAttackers -= actor.PredatorComponent.GetDirectPrey().Where(s => s.Unit.HasTrait(Traits.TheGreatEscape)).Count();
                            }
                        }
                    }
                }
            }
        }
        if (currentTurn > 500)
        {
            if (surrenderedAttackers == remainingAttackers)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    if (units[i] != null && units[i].Fled == false)
                    {
                        Actor_Unit actor = units[i];
                        if (actor.Unit.IsDead == false)
                        {
                            if (actor.Unit.Side == armies[0].Side)
                            {
                                actor.Unit.Health = 0;
                            }
                        }

                    }
                }
            }
            else if (surrenderedDefenders == remainingDefenders)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    if (units[i] != null && units[i].Fled == false)
                    {
                        Actor_Unit actor = units[i];
                        if (actor.Unit.IsDead == false)
                        {
                            if (actor.Unit.Side != armies[0].Side)
                            {
                                actor.Unit.Health = 0;
                            }
                        }

                    }
                }
            }
        }
    }

    void AssignLeftoverTroops(Army army, List<Actor_Unit> actors)
    {
        foreach (Actor_Unit actor in actors.ToList())
        {
            if (actor.Unit.IsDead)
            {
                actors.Remove(actor);
            }
            else
            {  //Extra safety to eliminate the possibility of doubled units
                retreatedAttackers.Remove(actor.Unit);
                retreatedDefenders.Remove(actor.Unit);
            }
        }
        List<Actor_Unit> leftover = new List<Actor_Unit>();
        if (army != null)
        {
            foreach (Actor_Unit actor in actors.ToList()) //Prevent doubling up of retreated defected units
            {
                if (army.Units.Contains(actor.Unit))
                {
                    actors.Remove(actor);
                }
            }

            foreach (Unit unit in army.Units.ToList())
            {
                if (unit.IsDead)
                {
                    army.Units.Remove(unit);
                    State.World.Stats?.SoldiersLost(1, unit.Side);
                }
            }
            if (actors.Any())
            {
                while (StrategicUtilities.ArmyCanFitUnit(army, actors[0].Unit))
                {
                    army.Units.Add(actors[0].Unit);
                    actors.RemoveAt(0);
                    if (!actors.Any())
                        break;
                }
            }
            while (StrategicUtilities.ArmyCanFitUnit(army, army.Units.OrderByDescending(u => State.RaceSettings.GetDeployCost(u.Race) * u.TraitBoosts.DeployCostMult).Last()))
            {
                var last = army.Units.OrderByDescending(u => State.RaceSettings.GetDeployCost(u.Race) * u.TraitBoosts.DeployCostMult).Last();
                army.Units.Remove(last);
                actors.Add(new Actor_Unit(last));
                if (!army.Units.Any())
                    break;
            }
        }

        if (village != null && actors.Any())
        {

            foreach (var unit in actors.Select(s => s.Unit))
            {
                if (village.GetRecruitables().Contains(unit) == false)
                    village.VillagePopulation.AddHireable(unit);
            }

            actors.Clear();
        }
        if (actors.Any())
        {
            TacticalUtilities.ProcessTravelingUnits(actors.Select(s => s.Unit).ToList());            
        }
    }

    void ProcessFledUnits()
    {
        retreatedAttackers.RemoveAll(s => s.IsDead);
        retreatedDefenders.RemoveAll(s => s.IsDead);
        if (retreatedAttackers.Any())
        {
            TacticalUtilities.ProcessTravelingUnits(retreatedAttackers);
        }
        if (retreatedDefenders.Any())
        {
            TacticalUtilities.ProcessTravelingUnits(retreatedDefenders);
        }
    }

    void ProcessConsumedCorpses()
    {
        if (Config.EdibleCorpses == false)
            return;
        Actor_Unit[] survivingPredators = units.Where(s => s.Unit.IsDead == false && s.Unit.Predator).ToArray();
        if (survivingPredators.Length == 0)
            return;
        for (int i = 0; i < 500; i++)
        {
            Actor_Unit preyUnit = units.Where(s => s.Unit.IsDead && s.Unit.Health > s.Unit.MaxHealth * -1).FirstOrDefault();
            if (preyUnit == null)
                break;
            Actor_Unit predatorUnit = survivingPredators.Where(s => s.Unit.Health < s.Unit.MaxHealth).OrderByDescending(s => s.Unit.MaxHealth - s.Unit.Health).FirstOrDefault();
            if (predatorUnit == null)
                predatorUnit = survivingPredators[State.Rand.Next(survivingPredators.Length)];
            predatorUnit.Unit.GiveScaledExp(4, predatorUnit.Unit.Level - preyUnit.Unit.Level, true);
            predatorUnit.Unit.Heal((preyUnit.Unit.MaxHealth + preyUnit.Unit.Health) / 2);
            //Weight gain disabled for consuming corpses
            preyUnit.Unit.Health = -999999;
        }

    }

    void EatSurrenderedAllies()
    {
        if (Config.EatSurrenderedAllies == false)
            return;
        Actor_Unit[] survivingPredators = units.Where(s => s.Unit.IsDead == false && s.Unit.Predator && s.Unit.Predator).ToArray();
        if (survivingPredators.Length == 0)
            return;
        for (int i = 0; i < 500; i++)
        {
            Actor_Unit preyUnit = units.Where(s => s.Unit.IsDead == false && s.Surrendered).FirstOrDefault();
            if (preyUnit == null)
                break;
            Actor_Unit predatorUnit = survivingPredators.Where(s => s.Unit.Health < s.Unit.MaxHealth).OrderByDescending(s => s.Unit.MaxHealth - s.Unit.Health).FirstOrDefault();
            if (predatorUnit == null)
                predatorUnit = survivingPredators[State.Rand.Next(survivingPredators.Length)];
            predatorUnit.Unit.GiveScaledExp(6, predatorUnit.Unit.Level - preyUnit.Unit.Level, true);
            predatorUnit.Unit.Heal((preyUnit.Unit.MaxHealth + preyUnit.Unit.Health) / 2);
            //Weight gain disabled for consuming allies
            TacticalStats.RegisterAllyVore(predatorUnit.Unit.Side);
            predatorUnit.Unit.DigestedUnits++;
            preyUnit.Unit.Kill();
            foreach (var item in preyUnit.Unit.AllConditionalTraits.Keys.Where(t => t.trigger == TraitConditionTrigger.OnDeath || t.trigger == TraitConditionTrigger.All).ToList())
            {
                if (ConditionalTraitConditionChecker.TacticalTraitConditionActive(preyUnit, item))
                {
                    preyUnit.Unit.ActivateConditionalTrait(item.id);
                }
                else
                {
                    preyUnit.Unit.DeactivateConditionalTrait(item.id);
                }
            }
            if (predatorUnit.Unit.HasTrait(Traits.EssenceAbsorption) && predatorUnit.Unit.DigestedUnits % 4 == 0)
                predatorUnit.Unit.GeneralStatIncrease(1);
            preyUnit.Unit.Health = -999999;
        }

    }



    public void ProcessTileEffects()
    {
        if (ActiveEffects == null)
            return;
        foreach (var key in ActiveEffects.ToList())
        {
            key.Value.RemainingDuration -= 1;

            if (key.Value.Type == TileEffectType.Fire)
            {
                var actor = TacticalUtilities.GetActorAt(key.Key);
                if (actor != null)
                {
                    int damage = Mathf.RoundToInt(key.Value.Strength * actor.Unit.TraitBoosts.FireDamageTaken);
                    if (actor.Damage(damage, true))
                    {
                        Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> took <color=red>{damage}</color> points of fire damage");
                    }
                }
            }
            if (key.Value.RemainingDuration <= 0)
            {
                EffectTileMap.SetTile(new Vector3Int(key.Key.x, key.Key.y, 0), null);
                ActiveEffects.Remove(key.Key);
            }
        }
    }


    public override void ReceiveInput()
    {
        remainingLockedPanelTime -= Time.deltaTime;
        if (Input.GetButtonDown("Hide"))
        {
            InfoPanel.RefreshTacticalUnitInfo(null);
            InfoPanel.HidePanel();
        }
        else
        {
            InfoPanel.RefreshLastUnitInfo();
        }
        if (TacticalLogUpdated) //Done this way so the unity auto-size will adjust between the change of the window and this movement
        {
            var transform = LogUI.Text.transform;
            transform.localPosition = new Vector3(0, transform.GetComponent<RectTransform>().rect.height, 0);
            TacticalLogUpdated = false;
        }
        if (State.GameManager.StatScreen.gameObject.activeSelf)
            return;
        if (DirtyPack)
            UpdateAreaTraits();
        if (Input.GetButtonDown("Menu"))
        {
            State.GameManager.OpenMenu();
        }
        if (turboMode)
            return;
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
            PausedText.SetActive(paused);
        }

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Z))
        {
            UndoMovement();
        }

        if (Input.GetButtonDown("Menu"))
        {
            State.GameManager.OpenMenu();
        }
        if (Input.GetButtonDown("Quicksave"))
        {
            State.Save($"{State.SaveDirectory}Quicksave.sav");
        }
        else if (Input.GetButtonDown("Quickload"))
        {
            State.GameManager.AskQuickLoad();
        }
        if (EventSystem.current.IsPointerOverGameObject() == false) //Makes sure mouse isn't over a UI element
        {
            Vector2 currentMousePos = State.GameManager.Camera.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)(currentMousePos.x + 0.5f);
            int y = (int)(currentMousePos.y + 0.5f);
            if (x >= 0 && x <= tiles.GetUpperBound(0) && y >= 0 && y <= tiles.GetUpperBound(1))
            {
                MouseOver(x, y);
                if (Input.GetMouseButtonDown(0))
                    ProcessLeftClick(x, y);
                if (Input.GetMouseButtonDown(1) && SelectedUnit != null && SelectedUnit.Movement > 0 && (IsPlayerTurn || PseudoTurn))
                    ProcessRightClick(x, y);
                if (Input.GetMouseButtonDown(2))
                    remainingLockedPanelTime = 1.5f;

            }
            else
            {
                arrowManager.ClearNodes();
                currentPathDestination = null;
            }

        }
        if (reviewingBattle)
        {
            if (Input.GetButtonDown("Submit"))
                EndTurn();
            return;
        }
        if (paused || State.GameManager.UnitEditor.gameObject.activeSelf)
            return;
        UpdateStatus(Time.deltaTime);
        if (IsPlayerTurn || PseudoTurn)
        {
            if (queuedPath != null)
                return;

            if (RunningFriendlyAI)
                return;
            if (foreignAI != null)
                return;

            if (SelectedUnit != null)
                MoveActor();
            if (Input.GetButtonDown("Cancel"))
                ActionMode = 0;
            if (Input.GetButtonDown("Next Unit"))
                NextActor(NextUnitType.Any);
            if (Input.GetButtonDown("Next Melee Unit"))
                NextActor(NextUnitType.Melee);
            if (Input.GetButtonDown("Next Ranged Unit"))
                NextActor(NextUnitType.Ranged);
            if (Input.GetButtonDown("Submit"))
            {
                var box = FindObjectOfType<DialogBox>();
                if (box != null)
                    box.YesClicked();
                else
                    PromptEndTurn();
            }


        }

    }
	
    void UpdateFog()
    {
        FogOfWar.gameObject.SetActive(true);
        if (FogSystem == null)
            FogSystem = new FogSystemTactical(FogOfWar, FogTile);
        FogSystem.UpdateFog(units, defenderSide, attackersTurn, AIAttacker, AIDefender, currentTurn);
    }

    public override void CleanUp()
    {
        ActionMode = 0;
        autoAdvancing = false;
        turboMode = false;
        AutoAdvanceText.SetActive(false);
        RunningFriendlyAI = false;
        SelectedUnit = null;
        SelectionBox.gameObject.SetActive(false);

        Decorations = new PlacedDecoration[0];

        Log.Clear();

        foreach (Actor_Unit actor in units)
        {
            actor.UnitSprite = null;
        }
        int children = TerrainFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(TerrainFolder.GetChild(i).gameObject);
        }
        children = ActorFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(ActorFolder.GetChild(i).gameObject);
        }
        Tilemap.ClearAllTiles();
        UnderTilemap.ClearAllTiles();
        FrontTilemap.ClearAllTiles();
        FrontSpriteTilemap.ClearAllTiles();
        FrontColorTilemap.ClearAllTiles();
        EffectTileMap.ClearAllTiles();
        FogOfWar.ClearAllTiles();
        RightClickMenu.CloseAll();
        TacticalUtilities.ResetData();
    }

    internal void HandleReanimationSideEffects(Unit caster, Actor_Unit target)
    {
        armies[0].Units.Remove(target.Unit);
        State.GameManager.TacticalMode.extraAttackers.Remove(target);
        garrison.Remove(target);
        armies[1]?.Units.Remove(target.Unit);
        State.GameManager.TacticalMode.extraDefenders.Remove(target);
        village?.GetRecruitables().Remove(target.Unit);
        target.Unit.Side = caster.Side;
        State.GameManager.TacticalMode.UpdateActorColor(target);
    }

    internal int ExposeRemainingUnits(bool attackers)
    {
        int remainingAttackers = 0;
        int remainingDefenders = 0;
        CalculateRemaining(ref remainingAttackers, ref remainingDefenders);
        if (attackers)
        {
            return remainingAttackers;
        }
        return remainingDefenders;
    }

    internal void ActivatePreBattleTraits()
    {
        foreach (var actor in units)
        {
            if (actor.Unit.HasTrait(Traits.CurseOfCraving))
            {
                if (actor.Unit.Predator)
                {
                    if (State.Rand.Next(2) == 0)
                    {
                        var possible_targets = units.Where(u => !u.Unit.IsEnemyOfSide(actor.Unit.Side) && u != actor && u.SelfPrey == null).ToList();
                        if (possible_targets.Any())
                        {
                            actor.PredatorComponent.ForceConsumeAuto(possible_targets[State.Rand.Next(0, possible_targets.Count())]);
                        }
                    }

                }
            }

            if (actor.Unit.HasTrait(Traits.CurseOfPreyportaion))
            {
                if (State.Rand.Next(4) == 0)
                {
                    var possible_targets = units.Where(u => u.Unit.Predator && u != actor && u.SelfPrey == null).ToList();
                    if (possible_targets.Any())
                    {
                        possible_targets[State.Rand.Next(0, possible_targets.Count())].PredatorComponent.ForceConsumeAuto(actor);
                    }

                }
            }
        }
    }

    private IEnumerator WaitForBuildingInput()
    {
        paused = true;

        yield return BuildingInputUnpause();

        paused = false;
    }
    private IEnumerator BuildingInputUnpause()
    {
        while (blockActive)
        {
            yield return null;
        }
    }
}
