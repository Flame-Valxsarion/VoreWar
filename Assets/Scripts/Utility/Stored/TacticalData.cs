using OdinSerializer;
using System.Collections.Generic;
using TacticalBuildings;

public class TacticalData
{
    [OdinSerialize]
    public List<Actor_Unit> units;
    [OdinSerialize]
    public Army[] armies;
    [OdinSerialize]
    public Village village;

    [OdinSerialize]
    internal List<ClothingDiscards> DiscardedClothing;

    [OdinSerialize]
    public TacticalTileType[,] tiles;

    [OdinSerialize]
    public Actor_Unit selectedUnit;

    [OdinSerialize]
    public int defenderSide;
    [OdinSerialize]
    public int attackerSide;

    [OdinSerialize]
    public int currentTurn;

    [OdinSerialize]
    public bool attackersTurn;
    [OdinSerialize]
    public bool isAPlayerTurn;
    [OdinSerialize]
    public int activeSide;

    [OdinSerialize]
    public bool AIAttacker;
    [OdinSerialize]
    public bool AIDefender;

    [OdinSerialize]
    public string AttackerName;
    [OdinSerialize]
    public string DefenderName;


    [OdinSerialize]
    public TacticalStats TacticalStats;

    [OdinSerialize]
    public ITacticalAI currentAI;
    [OdinSerialize]
    public ITacticalAI attackerAI;
    [OdinSerialize]
    public ITacticalAI defenderAI;

    [OdinSerialize]
    public bool runningFriendlyAI;

    [OdinSerialize]
    public List<Actor_Unit> extraAttackers;

    [OdinSerialize]
    public List<Actor_Unit> extraDefenders;

    [OdinSerialize]
    public List<Unit> retreatedAttackers;

    [OdinSerialize]
    public List<Unit> retreatedDefenders;

    [OdinSerialize]
    public TacticalMessageLog log;

    [OdinSerialize]
    internal List<MiscDiscard> MiscDiscards;

    [OdinSerialize]
    public int LastDiscard;

    [OdinSerialize]
    internal List<Actor_Unit> garrison;

    [OdinSerialize]
    internal Dictionary<Traits,List<Actor_Unit>> linkedUnits;

    [OdinSerialize]
    public double StartingAttackerPower;

    [OdinSerialize]
    public double StartingDefenderPower;

    [OdinSerialize]
    internal TacticalBuilding[] buildings;
    [OdinSerialize]
    internal Dictionary<Vec2, TileEffect> activeEffects;

    [OdinSerialize]
    internal DecorationStorage[] decorationStorage;
}

