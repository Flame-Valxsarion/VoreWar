using System;
using System.Collections.Generic;
using System.Linq;

static class Config
{
    public enum AutoAdvanceType
    {
        DoNothing,
        AdvanceTurns,
        SkipToEnd
    }

    public enum VictoryType
    {
        AllCapitals,
        AllCities,
        CompleteElimination,
        NeverEnd,
    }

    public enum MonsterConquestType
    {
        IgnoreTowns = -1,
        DevourAndDisperse,
        DevourAndHold,
        CompleteDevourAndHold,
        CompleteDevourAndRepopulate,
        CompleteDevourAndMoveOn,
        CompleteDevourAndRepopulateFortify,
    }
    public enum DayNightMovemntType
    {
        Off,
        Day,
        Night,
    }

    public enum SeasonalType
    {
        Automatic,
        AlwaysOn,
        Disabled
    }

    public const int NumberOfRaces = 32;

    public const int NewItemSlots = 2;

    public const int GarrisonCost = 8;
    public const int ArmyCost = 10;

    public const int MaximumPossibleArmy = 48;

    public static bool PutTeamsTogether = false;

    public static float StrategicAIMoveDelay = 0.3f;
    public static float TacticalPlayerMovementDelay = 0.1f;
    public static float TacticalFriendlyAIMovementDelay = 0.1f;
    public static float TacticalAIMovementDelay = 0.1f;
    public static float TacticalAttackDelay = 0.2f;
    public static float TacticalVoreDelay = 0.3f;

    public static int MaxVillages => World.VillagesPerEmpire.Sum();

    public static AutoAdvanceType AutoAdvance = AutoAdvanceType.AdvanceTurns;
    public static bool StopAtEndOfBattle = false;
    public static bool WatchAIBattles = false;
    public static bool ShowStatsForSkippedBattles = false;
    public static bool SkipAIOnlyStats = false;
    public static bool TimedAIStats = false;
    public static bool EdgeScrolling = false;
    public static bool HardLava = false;
    public static bool DontFixLeaders = false;

    public static bool AutoUseAI = false;

    public static bool DisplayEndOfTurnText = false;

    public static float BannerScale = 1;

    public static bool Notifications = false;
    public static bool StrategicCenterCameraOnAction = false;
    public static bool TacticalCenterCameraOnAction = false;
    public static bool StrategicCameraActionPanel = false;
    public static bool TacticalCameraActionPanel = false;

    public static bool SimpleFarms = false;
    public static bool SimpleForests = false;
    public static bool BattleReport = true;

    public static bool HideUnitViewer = false;

    public static bool IgnoreMonsterBattles = false;

    public static bool ExtraTacticalInfo = false;
    public static bool CheatExtraStrategicInfo = false;
    public static bool CheatUnitEditorEnabled = false;
    public static bool CheatViewHostileArmies = false;
    public static bool CheatAddUnitButton = false;
    public static bool CheatPopulation = false;
    public static bool CloseInDigestionNoises = false;

    public static bool DamageNumbers = true;
    public static bool RightClickMenu = false;
    public static bool HideBaseStats = false;

    public static bool DesaturatedTiles = false;

    public static bool ShowLevelText = true;
    public static bool AltFriendlyColor = false;
    public static bool PromptEndTurn = true;
    public static bool ScrollToBattleLocation = true;

    public static int AllianceSquaresDarkness = 1;

    public static bool AIToAIMessagesForever = false;
    public static string LetterBeforeLeaderNames = "";

    public static bool KuroTenkoConvertsAllTypes = false;

    public static bool[] CenteredEmpire = new bool[NumberOfRaces];

    public static int StartingGold;

    //Everything below this line should be mirrored in WorldConfigStorage to ensure proper saving

    internal static WorldConfig World = new WorldConfig();


    internal static int[] VillagesPerEmpire => World.VillagesPerEmpire;

    internal static MonsterConquestType MonsterConquest => World.MonsterConquest;
    internal static int MonsterConquestTurns => World.MonsterConquestTurns;

    internal static bool FirstTurnArmiesIdle => World.GetValue("FirstTurnArmiesIdle");

    internal static Orientation MalesLike => World.MalesLike;
    internal static Orientation FemalesLike => World.FemalesLike;

    internal static DiplomacyScale DiplomacyScale => World.DiplomacyScale;

    internal static bool AllowInfighting => World.GetValue("AllowInfighting");

    internal static bool RaceSpecificVoreGraphicsDisabled => World.GetValue("RaceSpecificVoreGraphicsDisabled");


    internal static bool FriendlyRegurgitation => World.GetValue("FriendlyRegurgitation");//Units will vomit up friendly units if they're in the main stomach    
    internal static int StrategicWorldSizeX => World.StrategicWorldSizeX;
    internal static int StrategicWorldSizeY => World.StrategicWorldSizeY;
    internal static bool AutoScaleTactical => World.AutoScaleTactical;
    internal static int TacticalSizeX => World.TacticalSizeX;
    internal static int TacticalSizeY => World.TacticalSizeY;
    internal static int ExperiencePerLevel => World.ExperiencePerLevel;
    internal static int AdditionalExperiencePerLevel => World.AdditionalExperiencePerLevel;
    internal static int VillageIncomePercent => World.VillageIncomePercent;
    internal static int VillagersPerFarm => World.VillagersPerFarm;
    internal static int SoftLevelCap => World.SoftLevelCap;
    internal static int HardLevelCap => World.HardLevelCap;

    internal static bool CapMaxGarrisonIncrease => World.CapMaxGarrisonIncrease;

    internal static int VoreRate => World.VoreRate;
    internal static int EscapeRate => World.EscapeRate;
    internal static int RandomEventRate => World.RandomEventRate;
    internal static int RandomAIEventRate => World.RandomAIEventRate;
    internal static bool EventsRepeat => World.GetValue("EventsRepeat");
    internal static FairyBVType FairyBVType => World.FairyBVType;
    internal static FeedingType FeedingType => World.FeedingType;
    internal static FourthWallBreakType FourthWallBreakType => World.FourthWallBreakType;
    internal static UBConversion UBConversion => World.UBConversion;
    internal static GoddessMercy GoddessMercy => World.GoddessMercy;
    internal static SucklingPermission SucklingPermission => World.SucklingPermission;

    internal static int StartingPopulation => World.StartingPopulation;

    internal static bool NewGraphics => true;


    internal static bool MultiRaceFlip => World.GetValue("MultiRaceFlip");
    internal static bool AdventurersDisabled => World.GetValue("AdventurersDisabled");
    internal static bool MercenariesDisabled => World.GetValue("MercenariesDisabled");
    internal static bool TroopScatter => World.GetValue("TroopScatter");

    internal static bool HideViperSlits => World.GetValue("HideViperSlit");


    internal static bool AlwaysRandomizeConverted => World.GetValue("AlwaysRandomizeConverted");
    internal static bool SpecialMercsCanConvert => World.GetValue("SpecialMercsCanConvert");

    internal static bool LeadersRerandomizeOnDeath => World.GetValue("LeadersRerandomizeOnDeath");

    internal static float MaleFraction => World.MaleFraction;
    internal static float HermFraction => World.HermFraction;
    internal static float HermNameFraction => World.HermNameFraction;
    internal static float ClothedFraction => World.ClothedFraction;
    internal static float WeightLossFractionBreasts => World.WeightLossFractionBreasts;
    internal static float WeightLossFractionBody => World.WeightLossFractionBody;
    internal static float WeightLossFractionDick => World.WeightLossFractionDick;
    internal static float GrowthDecayIncreaseRate => World.GrowthDecayIncreaseRate;
    internal static float GrowthDecayOffset => World.GrowthDecayOffset;
    internal static float GrowthMod => World.GrowthMod;
    internal static float GrowthCap => World.GrowthCap;
    internal static float FurryFraction => World.FurryFraction;
    internal static float CustomEventFrequency => World.CustomEventFrequency;
    internal static float TacticalWaterValue => World.TacticalWaterValue;
    internal static float TacticalTerrainFrequency => World.TacticalTerrainFrequency;

    internal static float AutoSurrenderChance => World.AutoSurrenderChance;
    internal static float AutoSurrenderDefectChance => World.AutoSurrenderDefectChance;

    internal static float OverallMonsterSpawnRateModifier => World.OverallMonsterSpawnRateModifier;
    internal static float OverallMonsterCapModifier => World.OverallMonsterCapModifier;

    internal static float LeaderLossExpPct => World.LeaderLossExpPct;
    internal static int LeaderLossLevels => World.LeaderLossLevels;

    public static bool LamiaUseTailAsSecondBelly => World.GetValue("LamiaUseTailAsSecondBelly");

    internal static SpawnerInfo SpawnerInfo(Race race) => World.GetSpawner(race);
    internal static SpawnerInfo SpawnerInfoWithoutGeneration(Race race) => World.GetSpawnerWithoutGeneration(race);

    internal static VictoryType VictoryCondition => World.VictoryCondition;
    internal static int BreastSizeModifier => World.BreastSizeModifier;
    internal static int HermBreastSizeModifier => World.HermBreastSizeModifier;
    internal static int CockSizeModifier => World.CockSizeModifier;
    internal static int DefaultStartingWeight => World.DefaultStartingWeight;
    internal static int GoldMineIncome => World.GoldMineIncome;
    internal static int MaxSpellLevelDrop => World.MaxSpellLevelDrop;

    internal static int ArmyMP => World.ArmyMP;
    internal static int ScoutMP => World.ScoutMP;
    internal static int ScoutMax => World.ScoutMax;
    internal static float ArmyCreationMPMod => World.ArmyCreationMPMod;
    internal static float ArmyCreationMPCurve => World.ArmyCreationMPCurve;
    internal static int MaxArmies => World.MaxArmies;
    internal static bool RaceTraitsEnabled => World.GetValue("RaceTraitsEnabled");
    internal static bool RagsForSlaves => World.GetValue("RagsForSlaves");
    internal static bool VisibleCorpses => World.GetValue("VisibleCorpses");
    internal static bool EdibleCorpses => World.GetValue("EdibleCorpses");

    internal static List<Traits> LeaderTraits => World.LeaderTraits;
    internal static List<Traits> MaleTraits => World.MaleTraits;
    internal static List<Traits> FemaleTraits => World.FemaleTraits;
    internal static List<Traits> HermTraits => World.HermTraits;
    internal static List<Traits> SpawnTraits => World.SpawnTraits;

    internal static bool NoAIRetreat => World.GetValue("NoAIRetreat");
    internal static bool AICanCheatSpecialMercs => World.GetValue("AICanCheatSpecialMercs");
    internal static bool AICanHireSpecialMercs => World.GetValue("AICanHireSpecialMercs");

    internal static bool GoblinCaravans => World.GetValue("GoblinCaravans");
    internal static bool MonstersDropSpells => World.GetValue("MonstersDropSpells");

    internal static bool ExtraRandomHairColors => World.GetValue("ExtraRandomHairColors");

    internal static bool WeightGain => World.GetValue("WeightGain");
    internal static bool WeightLoss => World.GetValue("WeightLoss");

    internal static bool FurryHandsAndFeet => World.GetValue("FurryHandsAndFeet");
    internal static bool FurryFluff => World.GetValue("FurryFluff");
    internal static bool FurryGenitals => World.GetValue("FurryGenitals");
    internal static bool AllowHugeBreasts => World.GetValue("AllowHugeBreasts");
    internal static bool AllowHugeDicks => World.GetValue("AllowHugeDicks");
    internal static bool HairMatchesFur => World.GetValue("HairMatchesFur");
    internal static bool MaleHairForFemales => World.GetValue("MaleHairForFemales");
    internal static bool FemaleHairForMales => World.GetValue("FemaleHairForMales");
    internal static bool HermsOnlyUseFemaleHair => World.GetValue("HermsOnlyUseFemaleHair");
    internal static bool HideBreasts => World.GetValue("HideBreasts");
    internal static bool HideCocks => World.GetValue("HideCocks");
    internal static bool ErectionsFromVore => World.GetValue("ErectionsFromVore");
    internal static bool ErectionsFromCockVore => World.GetValue("ErectionsFromCockVore");
    internal static bool LewdDialog => World.GetValue("LewdDialog");
    internal static bool HardVoreDialog => World.GetValue("HardVoreDialog");


    internal static bool MonstersCanReform => World.GetValue("MonstersCanReform");

    internal static bool AltVoreOralGain => World.GetValue("AltVoreOralGain");
    internal static bool BreastVore => World.GetValue("BreastVore");
    internal static bool AnalVore => World.GetValue("AnalVore");
    internal static bool TailVore => World.GetValue("TailVore");
    internal static bool Unbirth => World.GetValue("Unbirth");
    internal static bool HermsCanUB => World.GetValue("HermsCanUB");
    internal static bool CockVore => World.GetValue("CockVore");
    internal static bool CockVoreHidesClothes => World.GetValue("CockVoreHidesClothes");
    internal static bool KuroTenkoEnabled => World.GetValue("KuroTenkoEnabled");
    internal static bool OverhealEXP => World.GetValue("OverhealEXP");
    internal static bool TransferAllowed => World.GetValue("TransferAllowed");
    internal static bool CumGestation => World.GetValue("CumGestation");
    internal static bool NoScatForDeadTransfers => World.GetValue("NoScatForDeadTransfers");

    internal static int OralWeight => World.OralWeight;
    internal static int UnbirthWeight => World.UnbirthWeight;
    internal static int AnalWeight => World.AnalWeight;
    internal static int BreastWeight => World.BreastWeight;
    internal static int CockWeight => World.CockWeight;
    internal static int TailWeight => World.TailWeight;

    internal static bool CanUseStomachRubOnEnemies => World.GetValue("CanUseStomachRubOnEnemies");
    internal static bool BoostedAccuracy => World.GetValue("BoostedAccuracy");
    internal static bool AllowTopless => World.GetValue("AllowTopless");
    internal static bool FactionLeaders => World.FactionLeaders;
    internal static int ItemSlots => World.ItemSlots;

    internal static bool FlatExperience => World.GetValue("FlatExperience");
    internal static bool FogOfWar => World.GetValue("FogOfWar");

    internal static bool DayNightEnabled => World.GetValue("DayNightEnabled");
    internal static bool DayNightCosmetic => World.GetValue("DayNightCosmetic");
    internal static bool DayNightSchedule => World.GetValue("DayNightSchedule");
    internal static int NightRounds => World.NightRounds;
    internal static bool DayNightRandom => World.GetValue("DayNightRandom");
    internal static float BaseNightChance => World.BaseNightChance;
    internal static float NightChanceIncrease => World.NightChanceIncrease;
    internal static bool NightMonsters => World.GetValue("NightMonsters");
    internal static bool NightMoveMonsters => World.GetValue("NightMoveMonsters");
    internal static int DefualtTacticalSightRange => World.DefualtTacticalSightRange;
    internal static int NightStrategicSightReduction => World.NightStrategicSightReduction;
    internal static int RevealTurn => World.RevealTurn;
    internal static DayNightMovemntType DayNightMonsterMovemnt => World.DayNightMonsterMovemnt;


    internal static bool CombatComplicationsEnabled => World.GetValue("CombatComplicationsEnabled");
    internal static bool StatCrit => World.GetValue("StatCrit");
    internal static float BaseCritChance => World.BaseCritChance;
    internal static float CritDamageMod => World.CritDamageMod;
    internal static bool StatGraze => World.GetValue("StatGraze");
    internal static float BaseGrazeChance => World.BaseGrazeChance;
    internal static float GrazeDamageMod => World.GrazeDamageMod;
    internal static int FogDistance => World.FogDistance;
    internal static bool LeadersUseCustomizations => World.GetValue("LeadersUseCustomizations");
    internal static bool LeadersAutoGainLeadership => World.GetValue("LeadersAutoGainLeadership");
    internal static bool LizardsHaveNoBreasts => World.GetValue("LizardsHaveNoBreasts");
    internal static bool RaceSizeLimitsWeightGain => World.GetValue("RaceSizeLimitsWeightGain");

    internal static bool MultiRaceVillages => World.GetValue("MultiRaceVillages");


    internal static bool Defections => World.GetValue("Defections");
    internal static bool Diplomacy => World.GetValue("Diplomacy");
    internal static bool LockedAIRelations => World.GetValue("LockedAIRelations");
    internal static bool DisorientedPrey => World.GetValue("DisorientedPrey");

    public static bool AnimatedBellies => World.GetValue("AnimatedBellies");
    public static bool DigestionSkulls => World.GetValue("DigestionSkulls");
    public static bool Bones => World.GetValue("Bones");
    public static bool CleanDisposal => World.GetValue("CleanDisposal");
    public static bool Scat => World.GetValue("Scat");
    public static bool ScatV2 => World.GetValue("ScatV2");
    public static bool ScatBones => World.GetValue("ScatBones");
    public static bool CondomsForCV => World.GetValue("CondomsForCV");
    public static bool ClothingDiscards => World.GetValue("ClothingDiscards");
    public static bool AutoSurrender => World.GetValue("AutoSurrender");
    public static bool SurrenderedCanConvert => World.GetValue("SurrenderedCanConvert");
    public static bool EatSurrenderedAllies => World.GetValue("EatSurrenderedAllies");
    public static bool Cumstains => World.GetValue("Cumstains");
    public static float BurpFraction => World.BurpFraction;
    public static float FartFraction => World.FartFraction;
    public static float WeightGainFraction => World.WeightGainFraction;
    internal static bool BurpOnDigest => World.GetValue("BurpOnDigest");
    public static bool FartOnAbsorb => World.GetValue("FartOnAbsorb");

    public static bool StatBoostsAffectMaxHP => World.GetValue("StatBoostsAffectMaxHP");
    public static bool OverfeedingDamage => World.GetValue("OverfeedingDamage");

    public static float DigestionSpeedMult => World.DigestionSpeedMult;
    public static float AbsorbSpeedMult => World.AbsorbSpeedMult;
    public static float BellyRubEffMult => World.BellyRubEffMult;
    public static int BellyRubsPerTurn => World.BellyRubsPerTurn;
    public static float DigestionRamp => World.DigestionRamp;   
    public static int DigestionRampTurn => World.DigestionRampTurn;
    public static float DigestionRampLoss => World.DigestionRampLoss;
    public static int DigestionRampCap => World.DigestionRampCap;
    public static float AbsorbRamp => World.AbsorbRamp;
    public static float AbsorbResourceMod => World.AbsorbResourceMod;
    public static bool AbsorbLoss => World.GetValue("AbsorbLoss");
    public static bool AbsorbBoostDeadOnly => World.GetValue("AbsorbBoostDeadOnly");
    public static float DigestionCap => World.DigestionCap;
    public static float DigestionFlatDmg => World.DigestionFlatDmg;
    public static int DigestionGraceTurns => World.DigestionGraceTurns;
    internal static bool DigestionDamageDivision => World.GetValue("DigestionDamageDivision");
    internal static bool AbsorbRateDivision => World.GetValue("AbsorbRateDivision");
    internal static int AbsorbResourceModBoost => World.AbsorbResourceModBoost;
    public static float SurrenderedPredEscapeMult => World.SurrenderedPredEscapeMult;
    public static float SurrenderedPredAutoRegur => World.SurrenderedPredAutoRegur;
    public static int NumberOfBuildings => World.GetBuildingInfoCount();
    public static int TacticalMovementSoftCap => World.TacticalMovementSoftCap;
    public static int TacticalMovementHardCap => World.TacticalMovementHardCap;
    public static float SizeAccuracyMod => World.SizeAccuracyMod;
    public static float SizeAccuracyLowerBound => World.SizeAccuracyLowerBound;
    public static int SizeAccuracyInterval => World.SizeAccuracyInterval;
    public static float SizeDamageMod => World.SizeDamageMod;
    public static float SizeDamageLowerBound => World.SizeDamageLowerBound;
    public static int SizeDamageInterval => World.SizeDamageInterval;
    public static List<ConstructibleBuilding> Buildings=> World.GetBuildingInfo();

    //Everything below this line should be mirrored in BuildingConfig to ensure proper saving

    internal static BuildingConfig BuildConfig = new BuildingConfig();


    internal static bool WinterActive()
    {
        if (World.WinterStuff == SeasonalType.AlwaysOn)
            return true;
        else if (World.WinterStuff == SeasonalType.Disabled)
            return false;
        if (DateTime.UtcNow.Month == 12 || DateTime.UtcNow.DayOfYear < 15)
            return true;
        return false;
    }

}

