using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;

public class WorldConfig
{

    [OdinSerialize]
    internal Dictionary<string, bool> Toggles = new Dictionary<string, bool>();

    [OdinSerialize]
    internal Dictionary<Race, SpawnerInfo> SpawnerInfo = new Dictionary<Race, SpawnerInfo>();

    [OdinSerialize]
    internal List<ConstructibleBuilding> BuildingInfo = new List<ConstructibleBuilding>();

    [OdinSerialize]
    internal int[] VillagesPerEmpire = new int[Config.NumberOfRaces];


    [OdinSerialize]
    internal int StrategicWorldSizeX = 32;
    [OdinSerialize]
    internal int StrategicWorldSizeY = 32;
    [OdinSerialize]
    internal bool AutoScaleTactical = true;
    [OdinSerialize]
    internal int TacticalSizeX = 24;
    [OdinSerialize]
    internal int TacticalSizeY = 24;
    [OdinSerialize, AllowEditing, ProperName("Experience Per Level"), IntegerRange(0, 9999), Description("Base Exp required per level")]
    internal int ExperiencePerLevel = 20;
    [OdinSerialize, AllowEditing, ProperName("Additional Experience Per Level"), IntegerRange(0, 9999), Description("How much extra exp is required per level")]
    internal int AdditionalExperiencePerLevel = 1;
    [OdinSerialize, AllowEditing, ProperName("Village Income Percent"), IntegerRange(0, 9999), Description("Multiplier to Village income")]
    internal int VillageIncomePercent = 100;
    [OdinSerialize, AllowEditing, ProperName("Villagers Per Farm"), IntegerRange(0, 9999), Description("Doesn't take effect until a new turn")]
    internal int VillagersPerFarm = 6;
    [OdinSerialize, AllowEditing, ProperName("Soft Level Cap"), IntegerRange(0, 9999), Description("After this level exp required spikes sharply")]
    internal int SoftLevelCap = 0;
    [OdinSerialize, AllowEditing, ProperName("Hard Level Cap"), IntegerRange(0, 9999), Description("After this level there are no more levels")]
    internal int HardLevelCap = 0;

    [OdinSerialize, AllowEditing, ProperName("Army Maintenance"), IntegerRange(0, 999), Description("Each unit in an active army costs this much gold per turn.  Default is 3.")]
    internal int ArmyUpkeep = 0;

    [OdinSerialize, AllowEditing, ProperName("Cap Max Garrison Increase"), IntegerRange(0, 9999), Description("Will make it so that the maximum increase to a garrison's size from buildings or a capital is 150% of the base value.   Basically designed to better support small army/garrison sizes, so if your max garrison size is 4, the capital will be a reasonable 6, instead of getting the full +8 and becoming 12.")]
    internal bool CapMaxGarrisonIncrease = true;

    [OdinSerialize]
    internal int MaxSpellLevelDrop = 4;

    [OdinSerialize]
    internal int ArmyMP = 3;
    [OdinSerialize]
    internal int MaxArmies = 32;
    [OdinSerialize]
    internal float ArmyCreationMPMod = 0;
    [OdinSerialize]
    internal float ArmyCreationMPCurve = 1f;
    [OdinSerialize]
    internal int ScoutMP = 3;
    [OdinSerialize]
    internal int ScoutMax = 4;

    [OdinSerialize, AllowEditing, ProperName("Gold Mine Income"), IntegerRange(0, 9999), Description("Gold provided by gold mines")]
    internal int GoldMineIncome = 40;

    [OdinSerialize]
    internal int VoreRate = 1;
    [OdinSerialize]
    internal int EscapeRate = 1;
    [OdinSerialize]
    internal int RandomEventRate = 0;
    [OdinSerialize]
    internal int RandomAIEventRate = 0;

    [OdinSerialize]
    internal int FogDistance = 2;


    [OdinSerialize]
    internal float WeightLossFractionBreasts = 0;
    [OdinSerialize]
    internal float WeightLossFractionBody = 0;
    [OdinSerialize]
    internal float WeightLossFractionDick = 0;
    [OdinSerialize]
    internal float GrowthDecayIncreaseRate = 0.04f;
    [OdinSerialize]
    internal float GrowthDecayOffset = 0f;
    [OdinSerialize]
    internal float GrowthMod = 1f;
    [OdinSerialize]
    internal float GrowthCap = 5f;

    [OdinSerialize]
    internal float AutoSurrenderChance = 1;
    [OdinSerialize]
    internal float AutoSurrenderDefectChance = 0.25f;

    [OdinSerialize]
    internal float MaleFraction = 0;
    [OdinSerialize]
    internal float HermFraction = 0;
    [OdinSerialize]
    internal float ClothedFraction = 0;
    [OdinSerialize]
    internal float FurryFraction = 0;
    [OdinSerialize]
    internal float HermNameFraction = 0;

    [OdinSerialize]
    internal float OverallMonsterSpawnRateModifier = 1;
    [OdinSerialize]
    internal float OverallMonsterCapModifier = 1;

    [OdinSerialize]
    internal float TacticalWaterValue = 0;
    [OdinSerialize]
    internal float TacticalTerrainFrequency = 0;

    [OdinSerialize]
    internal int StartingPopulation = 99999;

    [OdinSerialize]
    internal List<Traits> LeaderTraits;
    [OdinSerialize]
    internal List<Traits> MaleTraits;
    [OdinSerialize]
    internal List<Traits> FemaleTraits;
    [OdinSerialize]
    internal List<Traits> HermTraits;
    [OdinSerialize]
    internal List<Traits> SpawnTraits;

    [OdinSerialize]
    internal float CustomEventFrequency = 0;

    [OdinSerialize]
    internal Orientation MalesLike = 0;
    [OdinSerialize]
    internal Orientation FemalesLike = 0;
    [OdinSerialize]
    internal FairyBVType FairyBVType = 0;
    [OdinSerialize]
    internal FeedingType FeedingType = 0;
    [OdinSerialize]
    internal FourthWallBreakType FourthWallBreakType = 0;
    [OdinSerialize]
    internal UBConversion UBConversion = 0;
    [OdinSerialize]
    internal GoddessMercy GoddessMercy = 0;
    [OdinSerialize]
    internal SucklingPermission SucklingPermission = 0;

    [OdinSerialize]
    internal DiplomacyScale DiplomacyScale = 0;

    [OdinSerialize]
    internal Config.SeasonalType WinterStuff = 0;

    [OdinSerialize, AllowEditing, ProperName("Victory Condition"), Description("The condition required for victory")]
    internal Config.VictoryType VictoryCondition;

    [OdinSerialize]
    internal Config.MonsterConquestType MonsterConquest;
    [OdinSerialize]
    internal int MonsterConquestTurns;

    [OdinSerialize]
    internal int BreastSizeModifier = 0;
    [OdinSerialize]
    internal int HermBreastSizeModifier = 0;
    [OdinSerialize]
    internal int CockSizeModifier = 0;
    [OdinSerialize]
    internal int DefaultStartingWeight = 3;
	
    // DayNight configuration
    [OdinSerialize, AllowEditing, IntegerRange(1, 10), Description("It will be night for the entire round every X round. (Set to 1 for every round, 2 for every other, etc.)")]
    internal int NightRounds = 2;
    [OdinSerialize, AllowEditing, ProperName("Base Night Chance"), FloatRange(0, 1), Description("The % chance that it will be night on a given turn. Night will only last that Empire's turn and can occur multiple times per round.")]
    internal float BaseNightChance = 0.01f;
    [OdinSerialize, AllowEditing, ProperName("Night Chance Increase"), FloatRange(0, 1), Description("The increase of the % chance it will be night on a given turn, increasing every turn it is not night.  (% chance resets to the base chance after a night turn)")]
    internal float NightChanceIncrease = 0.01f;
    [OdinSerialize, AllowEditing, ProperName("Defualt Vision Radius"), IntegerRange(1,5), Description("Radius of a unit's vision at night. Things like traits can also increase this.")]
    internal int DefualtTacticalSightRange = 1;
    [OdinSerialize, AllowEditing, ProperName("Defualt Vision Radius"), IntegerRange(0, 7), Description("Radius of a unit's vision at night. Things like traits can also increase this.")]
    internal int NightStrategicSightReduction = 1;
    [OdinSerialize, AllowEditing, ProperName("Reveal Turn"), Description("The tactical turn where every unit is revealed.")]
    internal int RevealTurn = 50;
    [OdinSerialize]
    internal Config.DayNightMovemntType DayNightMonsterMovemnt = Config.DayNightMovemntType.Off;

    // CombatComplications configuration
    // Critical strikes
    [OdinSerialize, AllowEditing, ProperName("Base Critical Chance"), FloatRange(0, 1), Description("Base chance for a critical strike if not calculated from stats. If 'Stat Based Crit' is enabled with this, the chance will never be lower than this percentage, but it can be higher. Set to 0 to disable.")]
    internal float BaseCritChance = 0.05f;
    [OdinSerialize, AllowEditing, ProperName("Critical Damage Multiplier"), FloatRange(0, 1), Description("Damage is multiplied by this number. At default value (1.5), 10 damage is modified to 15")]
    internal float CritDamageMod = 1.5f;
    // Graze
    [OdinSerialize, AllowEditing, ProperName("Base Graze Chance"), FloatRange(0, 1), Description("Base chance for a graze if not calculated from stats. If 'Stat Based Graze' is enabled, the chance will never be lower than this percentage, but it can be higher. Set to 0 to disable.")]
    internal float BaseGrazeChance = 0.10f;
    [OdinSerialize, AllowEditing, ProperName("Graze Damage Multiplier"), FloatRange(0, 1), Description("Damage is multiplied by this number. At default value (0.3), 10 damage is modified to 3")]
    internal float GrazeDamageMod = 0.30f;

    [OdinSerialize]
    internal bool FactionLeaders;
    [OdinSerialize]
    internal int ItemSlots;

    [OdinSerialize]
    internal float BurpFraction = .1f;

    [OdinSerialize]
    internal float FartFraction = .1f;
    [OdinSerialize]
    internal float WeightGainFraction = .1f;

    [OdinSerialize, AllowEditing, FloatRange(0, 1), ProperName("Leader death exp loss Percentage"), Description("On death they will lose this % of their total experience")]
    internal float LeaderLossExpPct = 0;
    [OdinSerialize, AllowEditing, ProperName("Leader levels lost on death"), IntegerRange(0, 9999), Description("On death they will this many levels")]
    internal int LeaderLossLevels = 1;

    [OdinSerialize]
    internal int OralWeight = 1;
    [OdinSerialize]
    internal int BreastWeight = 1;
    [OdinSerialize]
    internal int UnbirthWeight = 1;
    [OdinSerialize]
    internal int CockWeight = 1;
    [OdinSerialize]
    internal int TailWeight = 1;
    [OdinSerialize]
    internal int AnalWeight = 1;

    [OdinSerialize]
    internal float DigestionSpeedMult = 1f;
    [OdinSerialize]
    internal float AbsorbSpeedMult = 1f;
    [OdinSerialize]
    internal float BellyRubEffMult = 1f;
    [OdinSerialize]
    internal int BellyRubsPerTurn = 1;
    [OdinSerialize]
    internal float DigestionRamp = .1f;
    [OdinSerialize]
    internal int DigestionRampTurn = 1;
    [OdinSerialize]
    internal int DigestionRampCap = -1;
    [OdinSerialize]
    internal float DigestionRampLoss = 1;
    [OdinSerialize]
    internal float AbsorbRamp = .1f;
    [OdinSerialize]
    internal float AbsorbResourceMod = 1;
    [OdinSerialize]
    internal float DigestionFlatDmg = -.01f;
    [OdinSerialize]
    internal int AbsorbResourceModBoost = 0;
    [OdinSerialize]
    internal float DigestionCap = 0;
    [OdinSerialize]
    internal int DigestionGraceTurns = 0;
    [OdinSerialize]
    internal float SurrenderedPredEscapeMult = 1;
    [OdinSerialize]
    internal float SurrenderedPredAutoRegur = 0;

    [OdinSerialize]
    internal int TacticalMovementSoftCap = -1;
    [OdinSerialize]
    internal int TacticalMovementHardCap = -1;
    [OdinSerialize]
    internal float SizeAccuracyMod = 0;
    [OdinSerialize]
    internal float SizeAccuracyLowerBound = 10;
    [OdinSerialize]
    internal int SizeAccuracyInterval = 5;
    [OdinSerialize]
    internal float SizeDamageMod = 0f;
    [OdinSerialize]
    internal float SizeDamageLowerBound = 10;
    [OdinSerialize]
    internal int SizeDamageInterval = 5;

    internal bool GetValue(string name)
    {
        if (Toggles == null)
        {
            ResetDictionary();
        }

        if (Toggles.TryGetValue(name, out bool value))
            return value;
        if (name == "ClothingDiscards")
        {
            Toggles[name] = true;
            return true;
        }
        else
            Toggles[name] = false;
        return false;
    }

    internal SpawnerInfo GetSpawner(Race race)
    {
        if (SpawnerInfo == null)
        {
            ResetSpawnerDictionary();
        }

        if (SpawnerInfo.TryGetValue(race, out SpawnerInfo value))
        {
            if (value.SpawnAttempts == 0) value.SpawnAttempts = 1;
            return value;
        }

        var obj = new SpawnerInfo(false, 4, .15f, 40, 900 + (int)race, 1, true, 6f, 8, 12, 40, false);
        SpawnerInfo[race] = obj;
        return obj;
    }

    internal SpawnerInfo GetSpawnerWithoutGeneration(Race race)
    {

        if (SpawnerInfo.TryGetValue(race, out SpawnerInfo value))
        {
            if (value.SpawnAttempts == 0) value.SpawnAttempts = 1;
            return value;
        }

        return null;
    }

    internal void ResetSpawnerDictionary()
    {
        SpawnerInfo = new Dictionary<Race, SpawnerInfo>();
        foreach (Race race in (Race[])Enum.GetValues(typeof(Race)))
        {
            if (race >= Race.Vagrants && race < Race.Selicia)
            {
                SpawnerInfo[race] = new SpawnerInfo(false, 4, .15f, 40, 900 + (int)race, 1, true, 6f, 8, 12, 40, false);
            }
        }
    }


    internal List<ConstructibleBuilding> GetBuildingInfo()
    {
        if(BuildingInfo == null)
            ReloadBuildingInfo();
        if(BuildingInfo.Count() <= 0)
            ReloadBuildingInfo();
        return BuildingInfo;
    }

    internal int GetBuildingInfoCount()
    {
        if(BuildingInfo == null)
            ReloadBuildingInfo();
        return BuildingInfo.Count();
    }

    internal void ReloadBuildingInfo()
    {
        BuildingInfo = new List<ConstructibleBuilding>
        { 
            new WorkCamp(null),
            new LumberSite(null),
            new Quarry(null),
            new CasterTower(null),
            new BarrierTower(null),
            new DefenseEncampment(null),
            new Academy(null),
            new BlackMagicTower(null),
            new TemporalTower(null),
            new Laboratory(null),
            new Teleporter(null),
            new TownHall(null),
        };
    }

    internal void ResetDictionary()
    {

        Toggles = new Dictionary<string, bool>
        {
            ["RaceTraitsEnabled"] = true,
            ["FriendlyRegurgitation"] = true,
            ["Unbirth"] = false,
            ["CockVore"] = false,
            ["CockVoreHidesClothes"] = false,
            ["BreastVore"] = false,
            ["TailVore"] = false,
            ["KuroTenkoEnabled"] = false,
            ["OverhealEXP"] = true,
            ["TransferAllowed"] = true,
            ["CumGestation"] = true,
            ["RagsForSlaves"] = true,
            ["VisibleCorpses"] = true,
            ["EdibleCorpses"] = false,
            ["WeightGain"] = true,
            ["FurryFluff"] = true,
            ["FurryHandsAndFeet"] = true,
            ["FurryGenitals"] = false,
            ["AllowHugeBreasts"] = false,
            ["AllowHugeDicks"] = false,
            ["HairMatchesFur"] = false,
            ["MaleHairForFemales"] = true,
            ["FemaleHairForMales"] = true,
            ["HideBreasts"] = false,
            ["HideCocks"] = false,
            ["LamiaUseTailAsSecondBelly"] = false,
            ["AllowTopless"] = false,
            ["VagrantsEnabled"] = false,
            ["AnimatedBellies"] = true,
            ["DigestionSkulls"] = true,
            ["Bones"] = true,
            ["CleanDisposal"] = false,
            ["Scat"] = false,
            ["ScatBones"] = false,
            ["CondomsForCV"] = false,
            ["AutoSurrender"] = false,
            ["EatSurrenderedAllies"] = false,
            ["NewGraphics"] = true,
            ["ErectionsFromVore"] = false,
            ["ErectionsFromCockVore"] = false,
            ["FogOfWar"] = false,
            ["LeadersUseCustomizations"] = false,
            ["HermsCanUB"] = false,
            ["FlatExperience"] = false,
            ["BoostedAccuracy"] = false,
            ["ClothingDiscards"] = true,
            ["HideViperSlit"] = false,
            ["BurpOnDigest"] = false,
            ["FartOnAbsorb"] = false,
            ["StatBoostsAffectMaxHP"] = false,
            ["OverfeedingDamage"] = false,
            ["DayNightEnabled"] = true,
            ["DayNightCosmetic"] = false,
            ["DayNightSchedule"] = true,
            ["DayNightRandom"] = true,
            ["NightMonsters"] = false,
            ["NightMoveMonsters"] = false,
            ["CombatComplicationsEnabled"] = false,
            ["StatCrit"] = false,
            ["StatGraze"] = false,
            ["DigestionDamageDivision"] = false,
            ["AbsorbRateDivision"] = false,
            ["AbsorbLoss"] = false,
            ["AbsorbBoostDeadOnly"] = false,
        };

        foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).Where(s => (int)s >= 0))
        {
            Toggles[$"Merc {race}"] = true;
        }
        //Disable any not-implemented races
        //Toggles[$"Merc {Race.Succubi}"] = false;
    }
}

