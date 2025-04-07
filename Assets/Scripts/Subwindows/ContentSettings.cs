using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContentSettings : MonoBehaviour
{
    public GameObject[] SettingsPanels;
    public Slider FemaleFraction;
    public Slider HermFraction;
    public Slider HermNameFraction;
    public Slider ClothedFraction;
    public Toggle WeightGain;
    public Toggle WeightLoss;
    public Slider WeightLossFractionBreasts;
    public Slider WeightLossFractionBody;
    public Slider WeightLossFractionDick;
    public InputField GrowthCap;
    public InputField GrowthMod;
    public InputField GrowthDecayOffset;
    public InputField GrowthDecayIncreaseRate;
    public Toggle FurryHandsAndFeet;
    public Toggle FurryFluff;
    public Slider FurryFraction;
    public Toggle FriendlyRegurgitation;

    public Slider FogDistance;

    public Toggle HairMatchesFur;
    public Toggle MaleHairForFemales;
    public Toggle FemaleHairForMales;
    public Toggle HermsOnlyUseFemaleHair;
    public Toggle HideBreasts;
    public Toggle HideCocks;

    public Toggle RaceTraitsEnabled;
    public Toggle NoAIRetreat;
    public Toggle AICanHireSpecialMercs;
    public Toggle AICanCheatSpecialMercs;

    public Toggle MultiRaceVillages;

    public Toggle RagsForSlaves;
    public Toggle VisibleCorpses;
    public Toggle EdibleCorpses;
    public Toggle RaceSizeLimitsWeightGain;

    public Toggle RaceSpecificVoreGraphicsDisabled;

    public Toggle GoblinCaravans;
    public Toggle FogOfWar;
    public Toggle LeadersUseCustomizations;

    public Toggle HermsCanUB;
    public Toggle Unbirth;
    public Toggle CockVore;
    public Toggle BreastVore;
    public Toggle AnalVore;
    public Toggle TailVore;
    public Toggle AltVoreOralGain;

    public Toggle MultiRaceFlip;
    public Toggle AdventurersDisabled;
    public Toggle MercenariesDisabled;
    public Toggle TroopScatter;


    public Toggle AlwaysRandomizeConverted;
    public Toggle SpecialMercsCanConvert;

    public Toggle CanUseStomachRubOnEnemies;

    public Toggle LeadersRerandomizeOnDeath;

    public Toggle DayNightEnabled;
    public Toggle DayNightCosmetic;
    public Toggle DayNightSchedule;
    public Toggle DayNightRandom;
    public Toggle NightMonsters;
    public Toggle NightMoveMonsters;
    public Slider NightRounds;
    public Slider BaseNightChance;
    public Slider NightChanceIncrease;
    public Slider DefualtTacticalSightRange;
    public Slider NightStrategicSightReduction;
    public InputField RevealTurn;
    public Dropdown DayNightMonsterMovemnt;


    public Toggle CombatComplicationsEnabled;
    public Toggle StatCrit;
    public Toggle StatGraze;
    public Slider BaseCritChance;
    public Slider CritDamageMod;
    public Slider BaseGrazeChance;
    public Slider GrazeDamageMod;



    public Toggle AllowHugeBreasts;
    public Toggle AllowHugeDicks;
    public Toggle AllowTopless;
    public Slider BreastSizeModifier;
    public Slider HermBreastSizeModifier;
    public Slider CockSizeModifier;
    public Slider DefaultStartingWeight;

    public Slider AutoSurrenderChance;
    public Slider AutoSurrenderDefectChance;

    public Slider OverallMonsterCapModifier;
    public Slider OverallMonsterSpawnRateModifier;

    public Toggle Diplomacy;

    public Toggle CockVoreHidesClothes;
    public Toggle KuroTenkoEnabled;
    public Toggle OverhealEXP;
    public Toggle TransferAllowed;
    public Toggle CumGestation;
    public Toggle NoScatForDeadTransfers;
    public Toggle LewdDialog;
    public Toggle HardVoreDialog;

    public TMP_Dropdown FemalesLike;
    public TMP_Dropdown MalesLike;
    public TMP_Dropdown FairyBVType;
    public TMP_Dropdown FeedingType;
    public TMP_Dropdown FourthWallBreakType;
    public TMP_Dropdown UBConversion;
    public TMP_Dropdown GoddessMercy;
    public TMP_Dropdown SucklingPermission;
    public TMP_Dropdown WinterStuff;

    public TMP_Dropdown DiplomacyScale;
    public TMP_Dropdown MaxSpellLevelDrop;

    public Slider TacticalWaterValue;
    public Slider TacticalTerrainFrequency;

    public Slider OralWeight;
    public Slider AnalWeight;
    public Slider BreastWeight;
    public Slider CockWeight;
    public Slider UnbirthWeight;
    public Slider TailWeight;

    public Toggle FurryGenitals;

    public Toggle LamiaUseTailAsSecondBelly;

    public Toggle HideViperSlit;

    public Toggle FlatExperience;
    public Toggle MonstersDropSpells;

    public Toggle AnimatedBellies;
    public Toggle DigestionSkulls;
    public Slider BurpFraction;
    public Toggle BurpOnDigest;
    public Slider FartFraction;
    public Slider WeightGainFraction;
    public Toggle FartOnAbsorb;

    public Toggle Bones;
    public Toggle CleanDisposal;
    public Toggle Scat;
    public Toggle ScatV2;
    public Toggle ScatBones;
    public Toggle ClothingDiscards;

    public Toggle ErectionsFromVore;
    public Toggle ErectionsFromCockVore;

    public Toggle AutoSurrender;
    public Toggle EatSurrenderedAllies;

    public Toggle BoostedAccuracy;
    public Toggle DisorientedPrey;

    public Toggle ExtraHairColors;
    public Toggle LizardsHaveNoBreasts;

    public Toggle AllowInfighting;

    public Toggle SurrenderedCanConvert;

    public Toggle LockedAIRelations;
    public Toggle Defections;

    public Toggle Cumstains;
    public Toggle CondomsForCV;

    public Toggle MonstersCanReform;


    public Text TooltipText;

    public TMP_Dropdown PageSelector;

    List<ToggleObject> Toggles;

    public Toggle AllMercs;

    public Transform MercenaryToggleFolder;
    public GameObject TogglePrefab;

    public Transform MonsterSpawnerFolder;
    public GameObject MonsterSpawnerPrefab;

    public Slider ArmyMP;
    public Slider ScoutMP;
    public Slider ScoutMax;
    public Slider ArmyCreationMPMod;
    public Slider ArmyCreationMPCurve;

    public Slider CustomEventFrequency;

    public Dropdown MonsterConquest;

    public TMP_Dropdown VoreRate;
    public TMP_Dropdown EscapeRate;
    public TMP_Dropdown RandomEventRate;
    public TMP_Dropdown RandomAIEventRate;

    public Toggle EventsRepeat;


    public TMP_Dropdown MercSortMethod;
    public Toggle MercSortDirection;

    public InputField MonsterConquestTurns;

    public InputField LeaderTraits;
    public InputField MaleTraits;
    public InputField FemaleTraits;
    public InputField HermTraits;
    public InputField SpawnTraits;

    public Slider MaxArmies;

    public Toggle StatBoostsAffectMaxHP;
    public Toggle OverfeedingDamage;

    public Slider DigestionSpeedMult;
    public Slider AbsorbSpeedMult;
    public Slider BellyRubEffMult;
    public Slider BellyRubsPerTurn;
    public Slider DigestionRamp;
    public Slider DigestionRampTurn;
    public Slider DigestionRampCap;
    public Slider DigestionRampLoss;
    public Slider AbsorbRamp;
    public Slider AbsorbResourceMod;
    public Toggle AbsorbLoss;
    public Toggle AbsorbBoostDeadOnly;
    public TMP_Dropdown AbsorbResourceModBoost;
    public Slider DigestionCap;
    public Slider DigestionFlatDmg;
    public Slider DigestionGraceTurns;
    public Toggle DigestionDamageDivision;
    public Toggle AbsorbRateDivision;
    public Slider SurrenderedPredEscapeMult;
    public Slider SurrenderedPredAutoRegur;

    public InputField TacticalMovementSoftCap;
    public InputField TacticalMovementHardCap;
    public InputField SizeAccuracyMod;
    public InputField SizeAccuracyLowerBound;
    public InputField SizeAccuracyInterval;
    public InputField SizeDamageMod;
    public InputField SizeDamageLowerBound;
    public InputField SizeDamageInterval;



    List<ToggleObject> MercToggles;

    List<MonsterSpawnerPanel> MonsterSpawners;

    public BuildingSettings buildingSettings;

    public void AllMercsCheckedChanged()
    {
        foreach (ToggleObject toggle in MercToggles)
        {
            toggle.Toggle.isOn = AllMercs.isOn;
        }

    }


    void CreateList()
    {
        Toggles = new List<ToggleObject>
        {
            new ToggleObject(WeightGain, "WeightGain", true),
            new ToggleObject(WeightLoss, "WeightLoss", true),
            new ToggleObject(FurryHandsAndFeet, "FurryHandsAndFeet", true),
            new ToggleObject(FurryFluff, "FurryFluff", true),
            new ToggleObject(FriendlyRegurgitation, "FriendlyRegurgitation", true),
            new ToggleObject(HairMatchesFur, "HairMatchesFur",  false),
            new ToggleObject(MaleHairForFemales, "MaleHairForFemales",  true),
            new ToggleObject(FemaleHairForMales, "FemaleHairForMales",  true),
            new ToggleObject(HermsOnlyUseFemaleHair, "HermsOnlyUseFemaleHair",  false),
            new ToggleObject(HideBreasts, "HideBreasts", false),
            new ToggleObject(HideCocks, "HideCocks", false),
            new ToggleObject(RaceTraitsEnabled, "RaceTraitsEnabled", true),
            new ToggleObject(RagsForSlaves, "RagsForSlaves", true),
            new ToggleObject(VisibleCorpses, "VisibleCorpses", true),
            new ToggleObject(EdibleCorpses, "EdibleCorpses", false),
            new ToggleObject(FogOfWar, "FogOfWar", false),
            new ToggleObject(LeadersUseCustomizations, "LeadersUseCustomizations", false),
            new ToggleObject(HermsCanUB, "HermsCanUB", false),
            new ToggleObject(Unbirth, "Unbirth", false),
            new ToggleObject(CockVore, "CockVore",  false),
            new ToggleObject(BreastVore, "BreastVore", false),
            new ToggleObject(AnalVore, "AnalVore", false),
            new ToggleObject(TailVore, "TailVore", false),
            new ToggleObject(CockVoreHidesClothes, "CockVoreHidesClothes", false),
            new ToggleObject(AltVoreOralGain, "AltVoreOralGain", false),
            new ToggleObject(AllowHugeBreasts, "AllowHugeBreasts", false),
            new ToggleObject(AllowHugeDicks, "AllowHugeDicks",  false),
            new ToggleObject(AllowTopless, "AllowTopless", false),
            new ToggleObject(FurryGenitals, "FurryGenitals", false),
            new ToggleObject(LamiaUseTailAsSecondBelly, "LamiaUseTailAsSecondBelly", false),
            new ToggleObject(AnimatedBellies, "AnimatedBellies", true),
            new ToggleObject(DigestionSkulls, "DigestionSkulls",  true),
            new ToggleObject(Bones, "Bones", true),
            new ToggleObject(CleanDisposal, "CleanDisposal", false),
            new ToggleObject(Scat, "Scat", false),
            new ToggleObject(ScatV2, "ScatV2", false),
            new ToggleObject(ScatBones, "ScatBones", false),
            new ToggleObject(CondomsForCV, "CondomsForCV", false),
            new ToggleObject(ErectionsFromVore, "ErectionsFromVore", false),
            new ToggleObject(ErectionsFromCockVore, "ErectionsFromCockVore", false),
            new ToggleObject(AutoSurrender, "AutoSurrender", false),
            new ToggleObject(EatSurrenderedAllies, "EatSurrenderedAllies",false),
            new ToggleObject(FlatExperience, "FlatExperience",false),
            new ToggleObject(BoostedAccuracy, "BoostedAccuracy",false),
            new ToggleObject(ClothingDiscards, "ClothingDiscards",true),
            new ToggleObject(ExtraHairColors, "ExtraRandomHairColors",false),
            new ToggleObject(GoblinCaravans, "GoblinCaravans",true),
            new ToggleObject(Diplomacy, "Diplomacy", false),
            new ToggleObject(LizardsHaveNoBreasts, "LizardsHaveNoBreasts", false),
            new ToggleObject(LewdDialog, "LewdDialog", false),
            new ToggleObject(KuroTenkoEnabled, "KuroTenkoEnabled", false),
            new ToggleObject(OverhealEXP, "OverhealEXP", true),
            new ToggleObject(TransferAllowed, "TransferAllowed", true),
            new ToggleObject(CumGestation, "CumGestation", true),
            new ToggleObject(HardVoreDialog, "HardVoreDialog", false),
            new ToggleObject(NoAIRetreat, "NoAIRetreat", false),
            new ToggleObject(AICanHireSpecialMercs, "AICanHireSpecialMercs", false),
            new ToggleObject(AICanCheatSpecialMercs, "AICanCheatSpecialMercs", false),
            new ToggleObject(DisorientedPrey, "DisorientedPrey", true),
            new ToggleObject(MonstersDropSpells, "MonstersDropSpells", true),
            new ToggleObject(AllowInfighting, "AllowInfighting", false),
            new ToggleObject(EventsRepeat, "EventsRepeat", false),
            new ToggleObject(SurrenderedCanConvert, "SurrenderedCanConvert", false),
            new ToggleObject(LockedAIRelations, "LockedAIRelations", false),
            new ToggleObject(Defections, "Defections", true),
            new ToggleObject(RaceSizeLimitsWeightGain, "RaceSizeLimitsWeightGain", false),
            new ToggleObject(MultiRaceVillages, "MultiRaceVillages", false),
            new ToggleObject(RaceSpecificVoreGraphicsDisabled, "RaceSpecificVoreGraphicsDisabled", false),
            new ToggleObject(MonstersCanReform, "MonstersCanReform", true),
            new ToggleObject(MultiRaceFlip, "MultiRaceFlip", false),
            new ToggleObject(AdventurersDisabled, "AdventurersDisabled", false),
            new ToggleObject(MercenariesDisabled, "MercenariesDisabled", false),
            new ToggleObject(TroopScatter, "TroopScatter", false),
            new ToggleObject(AlwaysRandomizeConverted, "AlwaysRandomizeConverted", false),
            new ToggleObject(SpecialMercsCanConvert, "SpecialMercsCanConvert", false),
            new ToggleObject(LeadersRerandomizeOnDeath, "LeadersRerandomizeOnDeath", false),
            new ToggleObject(NoScatForDeadTransfers, "NoScatForDeadTransfers", false),
            new ToggleObject(HideViperSlit, "HideViperSlit", false),
            new ToggleObject(Cumstains, "Cumstains", true),
            new ToggleObject(BurpOnDigest, "BurpOnDigest", false),
            new ToggleObject(FartOnAbsorb, "FartOnAbsorb", false),
            new ToggleObject(CanUseStomachRubOnEnemies, "CanUseStomachRubOnEnemies", false),
            new ToggleObject(DayNightEnabled, "DayNightEnabled", false),
            new ToggleObject(DayNightCosmetic, "DayNightCosmetic", false),
            new ToggleObject(DayNightSchedule, "DayNightSchedule", true),
            new ToggleObject(DayNightRandom, "DayNightRandom", true),
            new ToggleObject(NightMonsters, "NightMonsters", false),
            new ToggleObject(NightMoveMonsters, "NightMoveMonsters", false),
            new ToggleObject(CombatComplicationsEnabled, "CombatComplicationsEnabled", false),
            new ToggleObject(StatCrit, "StatCrit", false),
            new ToggleObject(StatGraze, "StatGraze", false),
            new ToggleObject(StatBoostsAffectMaxHP, "StatBoostsAffectMaxHP", false),
            new ToggleObject(OverfeedingDamage, "OverfeedingDamage", false),
            new ToggleObject(DigestionDamageDivision, "DigestionDamageDivision", false),
            new ToggleObject(AbsorbRateDivision, "AbsorbRateDivision", false),
            new ToggleObject(AbsorbLoss, "AbsorbLoss", false),
            new ToggleObject(AbsorbBoostDeadOnly, "AbsorbBoostDeadOnly", false),

        };
        MercToggles = new List<ToggleObject>();
        MonsterSpawners = new List<MonsterSpawnerPanel>();
        foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).Where(s => (int)s >= 0).OrderBy((s) => s.ToString()))
        {
            var obj = new ToggleObject(CreateMercToggle(race), $"Merc {race}", true);
            MercToggles.Add(obj);
            Toggles.Add(obj);

        }

        foreach (Race race in (Race[])Enum.GetValues(typeof(Race)))
        { //Done separately to keep their initial order for now
            if (race >= Race.Vagrants && race < Race.Selicia && race != Race.WyvernMatron && race != Race.DarkSwallower && race != Race.Collectors && race != Race.CoralSlugs
                && race != Race.SpitterSlugs && race != Race.SpringSlugs && race != Race.Raptor && race != Race.WarriorAnts)
            {
                var spawner = CreateMonsterPanel(race);
                MonsterSpawners.Add(spawner);
                if (race == Race.Wyvern)
                    spawner.AddonRace.GetComponent<DisplayTooltip>().value = 115;
                else if (race == Race.FeralSharks)
                    spawner.AddonRace.GetComponent<DisplayTooltip>().value = 116;
                else if (race == Race.Harvesters)
                    spawner.AddonRace.GetComponent<DisplayTooltip>().value = 138;
                else if (race == Race.Compy)
                    spawner.AddonRace.GetComponent<DisplayTooltip>().value = 209;
                else if (race == Race.Monitors)
                    spawner.AddonRace.GetComponent<DisplayTooltip>().value = 232;
                else
                    spawner.AddonRace.gameObject.SetActive(false);
            }
        }
    }

    MonsterSpawnerPanel CreateMonsterPanel(Race race)
    {
        var obj = Instantiate(MonsterSpawnerPrefab, MonsterSpawnerFolder);
        var spawner = obj.GetComponent<MonsterSpawnerPanel>();
        spawner.race = race;
        DisplayTooltip tooltip = spawner.SpawnEnabled.GetComponent<DisplayTooltip>();
        spawner.SpawnEnabled.GetComponentInChildren<Text>().text = $"{race} Enabled";

        switch (race)
        {
            case Race.Vagrants:
                tooltip.value = 23;
                break;
            case Race.Serpents:
                tooltip.value = 88;
                break;
            case Race.Wyvern:
                tooltip.value = 89;
                break;
            case Race.Compy:
                tooltip.value = 90;
                break;
            case Race.FeralSharks:
                tooltip.value = 92;
                break;
            case Race.FeralWolves:
                tooltip.value = 102;
                break;
            case Race.Cake:
                tooltip.value = 108;
                break;
            case Race.Harvesters:
                tooltip.value = 129;
                break;
            case Race.Voilin:
                tooltip.value = 144;
                break;
            case Race.FeralBats:
                tooltip.value = 145;
                break;
            case Race.FeralFrogs:
                tooltip.value = 153;
                break;
            case Race.Dragon:
                tooltip.value = 160;
                break;
            case Race.Dragonfly:
                tooltip.value = 161;
                break;
            case Race.TwistedVines:
                tooltip.value = 170;
                break;
            case Race.Fairies:
                tooltip.value = 171;
                break;
            case Race.FeralAnts:
                tooltip.value = 178;
                break;
            case Race.Gryphons:
                tooltip.value = 191;
                break;
            case Race.RockSlugs:
                tooltip.value = 194;
                break;
            case Race.Salamanders:
                tooltip.value = 198;
                break;
            case Race.Mantis:
                tooltip.value = 203;
                break;
            case Race.EasternDragon:
                tooltip.value = 204;
                break;
            case Race.Catfish:
                tooltip.value = 208;
                break;
            case Race.Gazelle:
                tooltip.value = 210;
                break;
            case Race.Earthworms:
                tooltip.value = 225;
                break;
            case Race.FeralLizards:
                tooltip.value = 230;
                break;
            case Race.Monitors:
                tooltip.value = 233;
                break;
            case Race.Schiwardez:
                tooltip.value = 234;
                break;
            case Race.Terrorbird:
                tooltip.value = 238;
                break;
            case Race.Dratopyr:
                tooltip.value = 247;
                break;
            case Race.FeralLions:
                tooltip.value = 248;
                break;
            case Race.Goodra:
                tooltip.value = 257;
                break;
            case Race.Whisp:
                tooltip.value = 305;
                break;
            case Race.FeralHorses:
                tooltip.value = 306;
                break;
            case Race.FeralFox:
                tooltip.value = 307;
                break;
            case Race.Terminid:
                tooltip.value = 308;
                break;
            case Race.FeralOrcas:
                tooltip.value = 309;
                break;
            case Race.BoomBunnies:
                tooltip.value = 314;
                break;
        }
        return spawner;
    }

    Toggle CreateMercToggle(Race race)
    {
        var toggle = Instantiate(TogglePrefab, MercenaryToggleFolder);
        toggle.GetComponentInChildren<Text>().text = race.ToString();
        var comp = toggle.AddComponent<RaceHoverObject>();
        comp.race = race;
        return toggle.GetComponent<Toggle>();
    }

    class ToggleObject
    {
        internal Toggle Toggle;
        internal string Name;
        internal bool DefaultState;

        public ToggleObject(Toggle toggle, string name, bool defaultState)
        {
            Toggle = toggle;
            Name = name;
            DefaultState = defaultState;
        }
    }


    public void ActivatePage()
    {
        for (int i = 0; i < SettingsPanels.Length; i++)
        {
            SettingsPanels[i].SetActive(false);
        }
        SettingsPanels[PageSelector.value].SetActive(true);
    }

    public void ConfirmRefresh()
    {
        var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
        box.SetData(LoadSaved, "Load Data", "Cancel", "This will load all of your saved global settings into this game, useful if you wish to apply your new settings to old saved games quickly.  This is not undoable.");
    }

    void LoadSaved()
    {
        Refresh();
        Open();
    }

    public void Refresh()
    {
        buildingSettings.HardLoad();
        if (Toggles == null)
            CreateList();
        foreach (ToggleObject toggle in Toggles)
        {
            Config.World.Toggles[toggle.Name] = PlayerPrefs.GetInt(toggle.Name, toggle.DefaultState ? 1 : 0) == 1;
        }
        Config.World.MaleFraction = PlayerPrefs.GetFloat("MaleFraction", .5f);
        Config.World.HermFraction = PlayerPrefs.GetFloat("HermFraction", 0);
        Config.World.HermNameFraction = PlayerPrefs.GetFloat("HermNameFraction", .66f);
        Config.World.ClothedFraction = PlayerPrefs.GetFloat("ClothedFraction", .85f);
        Config.World.FurryFraction = PlayerPrefs.GetFloat("FurryFraction", .5f);
        Config.World.WeightLossFractionBreasts = PlayerPrefs.GetFloat("WeightLossFractionBreasts", .2f);
        Config.World.WeightLossFractionBody = PlayerPrefs.GetFloat("WeightLossFractionBody", .2f);
        Config.World.WeightLossFractionDick = PlayerPrefs.GetFloat("WeightLossFractionDick", .2f);
        Config.World.GrowthCap = PlayerPrefs.GetFloat("GrowthCap", 5f);
        Config.World.GrowthMod = PlayerPrefs.GetFloat("GrowthMod", 1f);
        Config.World.GrowthDecayOffset = PlayerPrefs.GetFloat("GrowthDecayOffset", 0);
        Config.World.GrowthDecayIncreaseRate = PlayerPrefs.GetFloat("GrowthDecayIncreaseRate", 0.04f);
        Config.World.TacticalTerrainFrequency = PlayerPrefs.GetFloat("TacticalTerrainFrequency", 10f);
        Config.World.TacticalWaterValue = PlayerPrefs.GetFloat("TacticalWaterValue", .29f);
        Config.World.BreastSizeModifier = PlayerPrefs.GetInt("BreastSizeModifier", 0);
        Config.World.CockSizeModifier = PlayerPrefs.GetInt("CockSizeModifier", 0);
        Config.World.DefaultStartingWeight = PlayerPrefs.GetInt("StartingWeight", 2);
        Config.World.AutoSurrenderChance = PlayerPrefs.GetFloat("AutoSurrenderChance", 1);
        Config.World.AutoSurrenderDefectChance = PlayerPrefs.GetFloat("AutoSurrenderDefectChance", 0.25f);
        Config.World.OralWeight = PlayerPrefs.GetInt("OralWeight", 40);
        Config.World.BreastWeight = PlayerPrefs.GetInt("BreastWeight", 40);
        Config.World.AnalWeight = PlayerPrefs.GetInt("AnalWeight", 40);
        Config.World.UnbirthWeight = PlayerPrefs.GetInt("UnbirthWeight", 40);
        Config.World.CockWeight = PlayerPrefs.GetInt("CockWeight", 40);
        Config.World.TailWeight = PlayerPrefs.GetInt("TailWeight", 40);
        Config.World.MonsterConquest = (Config.MonsterConquestType)PlayerPrefs.GetInt("MonsterConquest", 0);
        Config.World.BurpFraction = PlayerPrefs.GetFloat("BurpFraction", .1f);
        Config.World.FartFraction = PlayerPrefs.GetFloat("FartFraction", .1f);
        Config.World.WeightGainFraction = PlayerPrefs.GetFloat("WeightGainFraction", .5f);
        Config.World.DigestionSpeedMult = PlayerPrefs.GetFloat("DigestionSpeedMult", 1);
        Config.World.AbsorbSpeedMult = PlayerPrefs.GetFloat("AbsorbSpeedMult", 1);
        Config.World.AbsorbResourceMod = PlayerPrefs.GetFloat("AbsorbResourceMod", 1);
        Config.World.BellyRubEffMult = PlayerPrefs.GetFloat("BellyRubEffMult", 1);
        Config.World.BellyRubsPerTurn = PlayerPrefs.GetInt("BellyRubsPerTurn", 1);
        Config.World.DigestionRamp = PlayerPrefs.GetFloat("DigestionRamp", 0);
        Config.World.DigestionRampTurn = PlayerPrefs.GetInt("DigestionRampTurn", 1);
        Config.World.DigestionRampCap = PlayerPrefs.GetInt("DigestionRampCap", -1);
        Config.World.DigestionRampLoss = PlayerPrefs.GetFloat("DigestionRampLoss", 1);
        Config.World.AbsorbResourceModBoost = PlayerPrefs.GetInt("AbsorbResourceModBoost", 0);
        Config.World.AbsorbRamp = PlayerPrefs.GetFloat("AbsorbRamp", 0);
        Config.World.DigestionCap = PlayerPrefs.GetFloat("DigestionCap", 0);
        Config.World.DigestionFlatDmg = PlayerPrefs.GetFloat("DigestionFlatDmg", 0);
        Config.World.DigestionGraceTurns = PlayerPrefs.GetInt("DigestionGraceTurns", 0);
        Config.World.SurrenderedPredEscapeMult = PlayerPrefs.GetFloat("SurrenderedPredEscapeMult", 1);
        Config.World.SurrenderedPredAutoRegur = PlayerPrefs.GetFloat("SurrenderedPredAutoRegur", 0);
        Config.World.ArmyMP = PlayerPrefs.GetInt("ArmyMP", 3);
        Config.World.ScoutMP = PlayerPrefs.GetInt("ScoutMP", 3);
        Config.World.ScoutMax = PlayerPrefs.GetInt("ScoutMax", 4);
        Config.World.ArmyCreationMPMod = PlayerPrefs.GetFloat("ArmyCreationMPMod", 0);
        Config.World.ArmyCreationMPCurve = PlayerPrefs.GetFloat("ArmyCreationMPCurve", 1f);
        Config.World.CustomEventFrequency = PlayerPrefs.GetFloat("CustomEventFrequency", .25f);
        Config.World.MaxArmies = PlayerPrefs.GetInt("MaxArmies", 32);
        Config.World.MonsterConquestTurns = PlayerPrefs.GetInt("MonsterConquestTurns", 1);
        Config.World.MalesLike = (Orientation)PlayerPrefs.GetInt("MalesLike", 0);
        Config.World.FemalesLike = (Orientation)PlayerPrefs.GetInt("FemalesLike", 0);
        Config.World.WinterStuff = (Config.SeasonalType)PlayerPrefs.GetInt("WinterStuff", 0);
        Config.World.VoreRate = PlayerPrefs.GetInt("VoreRate", 0);
        Config.World.FairyBVType = (FairyBVType)PlayerPrefs.GetInt("FairyBVType", 0);
        Config.World.FeedingType = (FeedingType)PlayerPrefs.GetInt("FeedingType", 0);
        Config.World.FourthWallBreakType = (FourthWallBreakType)PlayerPrefs.GetInt("FourthWallBreakType", 0);
        Config.World.UBConversion = (UBConversion)PlayerPrefs.GetInt("UBConversion", 0);
        Config.World.GoddessMercy = (GoddessMercy)PlayerPrefs.GetInt("GoddessMercy", 0);
        Config.World.SucklingPermission = (SucklingPermission)PlayerPrefs.GetInt("SucklingPermission", 0);
        Config.World.EscapeRate = PlayerPrefs.GetInt("EscapeRate", 0);
        Config.World.FogDistance = PlayerPrefs.GetInt("FogDistance", 0);
        Config.World.RandomEventRate = PlayerPrefs.GetInt("RandomEventRate", 0);
        Config.World.RandomAIEventRate = PlayerPrefs.GetInt("RandomAIEventRate", 0);
        Config.World.DiplomacyScale = (DiplomacyScale)PlayerPrefs.GetInt("DiplomacyScale", 0);
        Config.World.MaxSpellLevelDrop = PlayerPrefs.GetInt("MaxSpellLevelDrop", 4);
        Config.World.LeaderTraits = RaceEditorPanel.TextToTraitList(PlayerPrefs.GetString("LeaderTraits", ""));
        Config.World.MaleTraits = RaceEditorPanel.TextToTraitList(PlayerPrefs.GetString("MaleTraits", ""));
        Config.World.FemaleTraits = RaceEditorPanel.TextToTraitList(PlayerPrefs.GetString("FemaleTraits", ""));
        Config.World.HermTraits = RaceEditorPanel.TextToTraitList(PlayerPrefs.GetString("HermTraits", ""));
        Config.World.SpawnTraits = RaceEditorPanel.TextToTraitList(PlayerPrefs.GetString("SpawnTraits", ""));
        Config.World.OverallMonsterCapModifier = PlayerPrefs.GetFloat("OverallMonsterCapModifier", 1);
        Config.World.OverallMonsterSpawnRateModifier = PlayerPrefs.GetFloat("OverallMonsterSpawnRateModifier", 1);
        Config.World.RevealTurn = PlayerPrefs.GetInt("RevealTurn", 50);
        Config.World.TacticalMovementSoftCap = PlayerPrefs.GetInt("TacticalMovementSoftCap", -1);
        Config.World.TacticalMovementHardCap = PlayerPrefs.GetInt("TacticalMovementHardCap", -1);
        Config.World.SizeAccuracyMod = PlayerPrefs.GetFloat("SizeAccuracyMod", 0);
        Config.World.SizeAccuracyLowerBound = PlayerPrefs.GetFloat("SizeAccuracyLowerBound", 10);
        Config.World.SizeAccuracyInterval = PlayerPrefs.GetInt("SizeAccuracyInterval", 5);
        Config.World.SizeDamageMod = PlayerPrefs.GetFloat("SizeDamageMod", 0);
        Config.World.SizeDamageLowerBound = PlayerPrefs.GetFloat("SizeDamageLowerBound", 10);
        Config.World.SizeDamageInterval = PlayerPrefs.GetInt("SizeDamageInterval", 5);
        MonsterDropdownChanged();
        if (Config.World.SpawnerInfo == null)
            Config.World.ResetSpawnerDictionary();
        foreach (MonsterSpawnerPanel spawner in MonsterSpawners)
        {
            Config.World.SpawnerInfo[spawner.race] = new SpawnerInfo(
                PlayerPrefs.GetInt($"{spawner.race} Enabled", 0) == 1,
                PlayerPrefs.GetInt($"{spawner.race} Max Armies", 4),
                PlayerPrefs.GetFloat($"{spawner.race} Spawn Rate", .15f),
                PlayerPrefs.GetInt($"{spawner.race} Scale Factor", 40),
                PlayerPrefs.GetInt($"{spawner.race} Team", 900 + (int)spawner.race),
                PlayerPrefs.GetInt($"{spawner.race} Attempts", 1),
                PlayerPrefs.GetInt($"{spawner.race} Add-On", 1) == 1,
                PlayerPrefs.GetFloat($"{spawner.race} Confidence", 6f),
                PlayerPrefs.GetInt($"{spawner.race} Min Army Size", 8),
                PlayerPrefs.GetInt($"{spawner.race} Max Army Size", 12),
                PlayerPrefs.GetInt($"{spawner.race} Turn Order", 40),
                PlayerPrefs.GetInt($"{spawner.race} Monster Scout MP", 0) == 1
                );
            var type = PlayerPrefs.GetInt($"{spawner.race} Conquest Type", 0);
            if (type != 0)
            {
                Config.World.SpawnerInfo[spawner.race].SetSpawnerType((Config.MonsterConquestType)(type - 2));
            }
            var dn_type = PlayerPrefs.GetInt($"{spawner.race} D/N Move Type", 0);
            if (type != 0)
            {
                Config.World.SpawnerInfo[spawner.race].SetSpawnerCycleMoveType((Config.DayNightMovemntType)(dn_type));
            }
        }


    }

    internal void ChangeToolTip(int value)
    {
        TooltipText.text = DefaultTooltips.Tooltip(value);
    }
    internal void ChangeBuildingToolTip(int value)
    {
        TooltipText.text = BuildingTooltips. Tooltip(value);
    }

    public void CorpsesChanged()
    {
        if (VisibleCorpses.isOn == false)
            EdibleCorpses.isOn = false;
        EdibleCorpses.interactable = VisibleCorpses.isOn;
    }

    public void DiaperChanged()
    {
        if (CleanDisposal.isOn == true)
        {
            ScatBones.isOn = false;
            ScatV2.isOn = false;
            ScatBones.interactable = false;
            ScatV2.interactable = false;
        }
        else if (Scat.isOn == true)
        {
            ScatBones.interactable = true;
            ScatV2.interactable = true;
        }
    }

    public void ScatChanged()
    {
        if (Scat.isOn == false)
        {
            ScatBones.isOn = false;
            ScatV2.isOn = false;
            CleanDisposal.isOn = false;
            ScatBones.interactable = false;
            ScatV2.interactable = false;
            CleanDisposal.interactable = false;
            Bones.interactable = true;
        }
        if (ScatV2.isOn == true)
        {
            CleanDisposal.isOn = false;
            CleanDisposal.interactable = false;
        }
        else if (Scat.isOn == true)
        {
            CleanDisposal.interactable = true;
            ScatBones.interactable = true;
            ScatV2.interactable = true;
            Bones.interactable = false;
            Bones.isOn = false;
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
        foreach (ToggleObject toggle in Toggles)
        {
            toggle.Toggle.isOn = Config.World.GetValue(toggle.Name);
        }
        FemaleFraction.value = 1 - Config.MaleFraction;
        HermFraction.value = Config.HermFraction;
        HermNameFraction.value = Config.HermNameFraction;
        ClothedFraction.value = Config.ClothedFraction;
        WeightLossFractionBreasts.value = Config.WeightLossFractionBreasts;
        WeightLossFractionBody.value = Config.WeightLossFractionBody;
        WeightLossFractionDick.value = Config.WeightLossFractionDick;
        GrowthMod.text = (Config.GrowthMod * 100).ToString();
        GrowthCap.text = (Config.GrowthCap * 100).ToString();
        GrowthDecayOffset.text = (Config.GrowthDecayOffset * 100).ToString();
        GrowthDecayIncreaseRate.text = (Config.GrowthDecayIncreaseRate * 1000).ToString();
        FurryFraction.value = Config.FurryFraction;
        TacticalWaterValue.value = Config.TacticalWaterValue;
        TacticalTerrainFrequency.value = Config.TacticalTerrainFrequency;
        BreastSizeModifier.value = Config.BreastSizeModifier;
        HermBreastSizeModifier.value = Config.HermBreastSizeModifier;
        CockSizeModifier.value = Config.CockSizeModifier;
        FogDistance.value = Config.FogDistance;
        DefualtTacticalSightRange.value = Config.DefualtTacticalSightRange;
        NightStrategicSightReduction.value = Config.NightStrategicSightReduction;
        NightRounds.value = Config.NightRounds;
        BaseNightChance.value = Config.BaseNightChance;
        NightChanceIncrease.value = Config.NightChanceIncrease;
        RevealTurn.text = Config.RevealTurn.ToString();
        BaseCritChance.value = Config.BaseCritChance;
        CritDamageMod.value = Config.CritDamageMod;
        BaseGrazeChance.value = Config.BaseGrazeChance;
        GrazeDamageMod.value = Config.GrazeDamageMod;
        DefaultStartingWeight.value = Config.DefaultStartingWeight;
        OralWeight.value = Config.OralWeight;
        BreastWeight.value = Config.BreastWeight;
        UnbirthWeight.value = Config.UnbirthWeight;
        CockWeight.value = Config.CockWeight;
        AnalWeight.value = Config.AnalWeight;
        TailWeight.value = Config.TailWeight;
        AutoSurrenderChance.value = Config.AutoSurrenderChance;
        AutoSurrenderDefectChance.value = Config.AutoSurrenderDefectChance;
        MonsterConquest.value = (int)Config.MonsterConquest + 1;
        VoreRate.value = Config.VoreRate + 1;
        EscapeRate.value = Config.EscapeRate + 1;
        RandomEventRate.value = Config.RandomEventRate;
        RandomAIEventRate.value = Config.RandomAIEventRate;
        BurpFraction.value = Config.BurpFraction;
        FartFraction.value = Config.FartFraction;
        WeightGainFraction.value = Config.WeightGainFraction;
        DigestionSpeedMult.value = Config.DigestionSpeedMult;
        AbsorbSpeedMult.value = Config.AbsorbSpeedMult;
        DigestionRamp.value = Config.DigestionRamp;
        BellyRubEffMult.value = Config.BellyRubEffMult;
        BellyRubsPerTurn.value = Config.BellyRubsPerTurn;
        DigestionRampTurn.value = Config.DigestionRampTurn;
        DigestionRampCap.value = Config.DigestionRampCap;
        DigestionRampLoss.value = Config.DigestionRampLoss;
        AbsorbRamp.value = Config.AbsorbRamp;
        AbsorbResourceMod.value = Config.AbsorbResourceMod;
        DigestionCap.value = Config.DigestionCap;
        DigestionFlatDmg.value = Config.DigestionFlatDmg;
        DigestionGraceTurns.value = Config.DigestionGraceTurns;
        SurrenderedPredEscapeMult.value = Config.SurrenderedPredEscapeMult;
        SurrenderedPredAutoRegur.value = Config.SurrenderedPredAutoRegur;
        AbsorbResourceModBoost.value = Config.AbsorbResourceModBoost;
        ArmyMP.value = Config.ArmyMP;
        ScoutMP.value = Config.ScoutMP;
        ScoutMax.value = Config.ScoutMax;
        ArmyCreationMPMod.value = Config.ArmyCreationMPMod;
        ArmyCreationMPCurve.value = Config.ArmyCreationMPCurve;
        CustomEventFrequency.value = Config.CustomEventFrequency;
        MaxArmies.value = Config.MaxArmies;
        MonsterConquestTurns.text = Config.MonsterConquestTurns.ToString();
        MercSortMethod.value = PlayerPrefs.GetInt("MercSortMethod", 0);
        FemalesLike.value = (int)Config.FemalesLike;
        MalesLike.value = (int)Config.MalesLike;
        FairyBVType.value = (int)Config.FairyBVType;
        FeedingType.value = (int)Config.FeedingType;
        FourthWallBreakType.value = (int)Config.FourthWallBreakType;
        UBConversion.value = (int)Config.UBConversion;
        GoddessMercy.value = (int)Config.GoddessMercy;
        SucklingPermission.value = (int)Config.SucklingPermission;
        WinterStuff.value = (int)Config.World.WinterStuff;
        DiplomacyScale.value = (int)Config.DiplomacyScale;
        MaxSpellLevelDrop.value = Config.MaxSpellLevelDrop - 1;
        OverallMonsterSpawnRateModifier.value = Config.OverallMonsterSpawnRateModifier;
        OverallMonsterCapModifier.value = Config.OverallMonsterCapModifier;
        MercSortDirection.isOn = PlayerPrefs.GetInt("MercSortDirection", 0) == 1;
        MercSortDirectionChanged();
        WinterStuff.RefreshShownValue();
        MercSortMethod.RefreshShownValue();
        FemalesLike.RefreshShownValue();
        MalesLike.RefreshShownValue();
        FairyBVType.RefreshShownValue();
        FeedingType.RefreshShownValue();
        FourthWallBreakType.RefreshShownValue();
        UBConversion.RefreshShownValue();
        GoddessMercy.RefreshShownValue();
        SucklingPermission.RefreshShownValue();
        DiplomacyScale.RefreshShownValue();
        MaxSpellLevelDrop.RefreshShownValue();
        LeaderTraits.text = RaceEditorPanel.TraitListToText(Config.LeaderTraits);
        MaleTraits.text = RaceEditorPanel.TraitListToText(Config.MaleTraits);
        FemaleTraits.text = RaceEditorPanel.TraitListToText(Config.FemaleTraits);
        HermTraits.text = RaceEditorPanel.TraitListToText(Config.HermTraits);
        SpawnTraits.text = RaceEditorPanel.TraitListToText(Config.SpawnTraits);
        TacticalMovementSoftCap.text = Config.TacticalMovementSoftCap.ToString();
        TacticalMovementHardCap.text = Config.TacticalMovementHardCap.ToString();
        SizeAccuracyMod.text = Config.SizeAccuracyMod.ToString();
        SizeAccuracyLowerBound.text = Config.SizeAccuracyLowerBound.ToString();
        SizeAccuracyInterval.text = Config.SizeAccuracyInterval.ToString();
        SizeDamageMod.text = Config.SizeDamageMod.ToString();
        SizeDamageLowerBound.text = Config.SizeDamageLowerBound.ToString();
        SizeDamageInterval.text = Config.SizeDamageInterval.ToString();
        RefreshSliderText();

        foreach (MonsterSpawnerPanel spawner in MonsterSpawners)
        {
            SpawnerInfo info = Config.SpawnerInfo(spawner.race);
            spawner.SpawnEnabled.isOn = info.Enabled;
            spawner.SpawnRate.value = info.spawnRate;
            spawner.ScalingRate.text = info.scalingFactor.ToString();
            spawner.MaxArmies.text = info.MaxArmies.ToString();
            spawner.Confidence.text = info.Confidence == 0 ? "6" : info.Confidence.ToString();
            spawner.MinArmySize.text = info.MinArmySize.ToString();
            spawner.MaxArmySize.text = info.MaxArmySize.ToString();
            spawner.Team.text = info.Team.ToString();
            spawner.SpawnAttempts.text = info.SpawnAttempts.ToString();
            spawner.TurnOrder.text = info.TurnOrder.ToString();
            spawner.AddonRace.isOn = info.AddOnRace;
            spawner.MonsterScoutMP.isOn = info.MonsterScoutMP;
            if (info.UsingCustomType)
            {
                spawner.ConquestType.value = (int)info.ConquestType + 2;
                spawner.ConquestType.RefreshShownValue();
            }
            else
            {
                spawner.ConquestType.value = 0;
                spawner.ConquestType.RefreshShownValue();
            }

            if (info.DNRestrictOn)
            {
                spawner.DayNightMonsterMovemnt.value = (int)info.DayNightMovemntType;
                spawner.DayNightMonsterMovemnt.RefreshShownValue();
            }
            else
            {
                spawner.DayNightMonsterMovemnt.value = 0;
                spawner.DayNightMonsterMovemnt.RefreshShownValue();
            }
        }

        WeightLoss.interactable = WeightGain.isOn;
        if (WeightGain.isOn == false)
        {
            WeightLoss.isOn = false;
        }
        FeedingType.interactable = KuroTenkoEnabled.isOn;
        OverhealEXP.interactable = KuroTenkoEnabled.isOn && (int)Config.FeedingType != 3;
        UBConversion.interactable = KuroTenkoEnabled.isOn;
        SucklingPermission.interactable = KuroTenkoEnabled.isOn && (int)Config.FeedingType == 0;
        TransferAllowed.interactable = KuroTenkoEnabled.isOn;
        CumGestation.interactable = KuroTenkoEnabled.isOn && TransferAllowed.isOn;
        SpecialMercsCanConvert.interactable = KuroTenkoEnabled.isOn && (Config.UBConversion == global::UBConversion.Both || Config.UBConversion == global::UBConversion.RebirthOnly);
        NoScatForDeadTransfers.interactable = KuroTenkoEnabled.isOn;
    }

    void SetValues()
    {
        bool oldMulti = Config.MultiRaceVillages;
        PlayerPrefs.SetInt("MercSortMethod", MercSortMethod.value);

        PlayerPrefs.SetInt("MercSortDirection", MercSortDirection.isOn ? 1 : 0);
        //if (Config.NewGraphics != NewGraphics.isOn)
        //{
        //    Config.World.Toggles["NewGraphics"] = NewGraphics.isOn;
        //    if (State.World?.MainEmpires != null)
        //    {
        //        foreach (Unit unit in StrategicUtilities.GetAllUnits())
        //        {
        //            if (unit.Race != Race.Imps && unit.Race != Race.Lamia && unit.Race != Race.Tigers)
        //            {
        //                Races.GetRace(unit).RandomCustom(unit);
        //            }
        //        }
        //    }
        //    else
        //        TacticalUtilities.RefreshUnitGraphicType();
        //}

        if (Config.Diplomacy == Diplomacy.isOn && Diplomacy.isOn == false && State.World?.MainEmpires != null)
        {
            RelationsManager.ResetRelationTypes();
        }

        if (Config.RaceTraitsEnabled != RaceTraitsEnabled.isOn)
        {
            Config.World.Toggles["RaceTraitsEnabled"] = RaceTraitsEnabled.isOn;
            if (State.World?.MainEmpires != null)
            {
                foreach (Unit unit in StrategicUtilities.GetAllUnits())
                {
                    if (unit.Race != Race.Vagrants)
                    {
                        unit.ReloadTraits();
                    }
                }
            }
        }
        if (Config.HermsCanUB != HermsCanUB.isOn)
        {
            if (State.World?.MainEmpires != null)
            {
                foreach (Unit unit in StrategicUtilities.GetAllUnits())
                {
                    if (unit.GetGender() == Gender.Hermaphrodite || unit.GetGender() == Gender.Gynomorph)
                    {
                        unit.HasVagina = HermsCanUB.isOn;
                    }
                }
            }
            else
            {
                if (TacticalUtilities.Units != null)
                {
                    foreach (var actor in TacticalUtilities.Units)
                    {
                        if (actor.Unit.GetGender() == Gender.Hermaphrodite || actor.Unit.GetGender() == Gender.Gynomorph)
                        {
                            actor.Unit.HasVagina = HermsCanUB.isOn;
                        }
                    }
                }

            }
        }
        foreach (ToggleObject toggle in Toggles)
        {
            Config.World.Toggles[toggle.Name] = toggle.Toggle.isOn;
        }
        Config.World.MaleFraction = 1 - FemaleFraction.value;
        Config.World.HermFraction = HermFraction.value;
        Config.World.HermNameFraction = HermNameFraction.value;
        Config.World.ClothedFraction = ClothedFraction.value;
        Config.World.FurryFraction = FurryFraction.value;
        Config.World.WeightLossFractionBreasts = WeightLossFractionBreasts.value;
        Config.World.WeightLossFractionBody = WeightLossFractionBody.value;
        Config.World.WeightLossFractionDick = WeightLossFractionDick.value;
        if (int.TryParse(GrowthMod.text, out int gm))
            Config.World.GrowthMod = gm / 100f;
        else
            Config.World.GrowthMod = 1;
        if (int.TryParse(GrowthCap.text, out int gc))
            Config.World.GrowthCap = gc / 100f;
        else
            Config.World.GrowthCap = 5f;
        if (int.TryParse(GrowthDecayIncreaseRate.text, out int gir))
            Config.World.GrowthDecayIncreaseRate = gir / 1000f;
        else
            Config.World.GrowthDecayIncreaseRate = 0.04f;
        if (int.TryParse(GrowthDecayOffset.text, out int gos))
            Config.World.GrowthDecayOffset = gos / 100f;
        else
            Config.World.GrowthDecayOffset = 0f;
        Config.World.TacticalTerrainFrequency = TacticalTerrainFrequency.value;
        Config.World.TacticalWaterValue = TacticalWaterValue.value;
        Config.World.AutoSurrenderChance = AutoSurrenderChance.value;
        Config.World.AutoSurrenderDefectChance = AutoSurrenderDefectChance.value;
        Config.World.HermBreastSizeModifier = (int)HermBreastSizeModifier.value;
        Config.World.BreastSizeModifier = (int)BreastSizeModifier.value;
        Config.World.CockSizeModifier = (int)CockSizeModifier.value;
        Config.World.DefaultStartingWeight = (int)DefaultStartingWeight.value;
        Config.World.MonsterConquest = (Config.MonsterConquestType)MonsterConquest.value - 1;
        Config.World.VoreRate = VoreRate.value - 1;
        Config.World.EscapeRate = EscapeRate.value - 1;
        Config.World.RandomEventRate = RandomEventRate.value;
        Config.World.RandomAIEventRate = RandomAIEventRate.value;
        Config.World.BurpFraction = BurpFraction.value;
        Config.World.FartFraction = FartFraction.value;
        Config.World.WeightGainFraction = WeightGainFraction.value;
        Config.World.DigestionSpeedMult = DigestionSpeedMult.value;
        Config.World.AbsorbSpeedMult = AbsorbSpeedMult.value;
        Config.World.BellyRubEffMult = BellyRubEffMult.value;
        Config.World.BellyRubsPerTurn = (int)BellyRubsPerTurn.value;
        Config.World.DigestionRamp = DigestionRamp.value;
        Config.World.DigestionRampTurn = (int)DigestionRampTurn.value;
        Config.World.DigestionRampCap = (int)DigestionRampCap.value;
        Config.World.DigestionRampLoss = DigestionRampLoss.value;
        Config.World.AbsorbRamp = AbsorbRamp.value;
        Config.World.AbsorbResourceMod = AbsorbResourceMod.value;
        Config.World.DigestionCap = DigestionCap.value;
        Config.World.DigestionFlatDmg = DigestionFlatDmg.value;
        Config.World.DigestionGraceTurns = (int)DigestionGraceTurns.value;
        Config.World.SurrenderedPredEscapeMult = SurrenderedPredEscapeMult.value;
        Config.World.SurrenderedPredAutoRegur = SurrenderedPredAutoRegur.value;
        Config.World.AbsorbResourceModBoost = AbsorbResourceModBoost.value;
        Config.World.ArmyMP = (int)ArmyMP.value;
        Config.World.ScoutMP = (int)ScoutMP.value;
        Config.World.ScoutMax = (int)ScoutMax.value;
        Config.World.ArmyCreationMPMod = ArmyCreationMPMod.value;
        Config.World.ArmyCreationMPCurve = ArmyCreationMPCurve.value;
        Config.World.CustomEventFrequency = CustomEventFrequency.value;
        Config.World.MaxArmies = (int)MaxArmies.value;
        Config.World.MonsterConquestTurns = int.TryParse(MonsterConquestTurns.text, out int monsterTurns) ? monsterTurns : 0;
        Config.World.MalesLike = (Orientation)MalesLike.value;
        Config.World.FemalesLike = (Orientation)FemalesLike.value;
        Config.World.FairyBVType = (FairyBVType)FairyBVType.value;
        Config.World.FourthWallBreakType = (FourthWallBreakType)FourthWallBreakType.value;
        Config.World.FeedingType = (FeedingType)FeedingType.value;
        Config.World.UBConversion = (UBConversion)UBConversion.value;
        Config.World.GoddessMercy = (GoddessMercy)GoddessMercy.value;
        Config.World.SucklingPermission = (SucklingPermission)SucklingPermission.value;
        Config.World.WinterStuff = (Config.SeasonalType)WinterStuff.value;
        Config.World.DiplomacyScale = (DiplomacyScale)DiplomacyScale.value;
        Config.World.MaxSpellLevelDrop = MaxSpellLevelDrop.value + 1;
        Config.World.LeaderTraits = RaceEditorPanel.TextToTraitList(LeaderTraits.text);
        Config.World.MaleTraits = RaceEditorPanel.TextToTraitList(MaleTraits.text);
        Config.World.FemaleTraits = RaceEditorPanel.TextToTraitList(FemaleTraits.text);
        Config.World.HermTraits = RaceEditorPanel.TextToTraitList(HermTraits.text);
        Config.World.SpawnTraits = RaceEditorPanel.TextToTraitList(SpawnTraits.text);
        Config.World.OralWeight = (int)OralWeight.value;
        Config.World.UnbirthWeight = (int)UnbirthWeight.value;
        Config.World.CockWeight = (int)CockWeight.value;
        Config.World.AnalWeight = (int)AnalWeight.value;
        Config.World.TailWeight = (int)TailWeight.value;
        Config.World.BreastWeight = (int)BreastWeight.value;
        Config.World.FogDistance = (int)FogDistance.value;
        Config.World.DefualtTacticalSightRange = (int)DefualtTacticalSightRange.value;
        Config.World.NightStrategicSightReduction = (int)NightStrategicSightReduction.value;
        Config.World.NightRounds = (int)NightRounds.value;
        Config.World.BaseNightChance = BaseNightChance.value;
        Config.World.NightChanceIncrease = NightChanceIncrease.value;
        if (int.TryParse(RevealTurn.text, out int rvl))
            Config.World.RevealTurn = rvl;
        else
            Config.World.RevealTurn = 50;
        Config.World.BaseCritChance = BaseCritChance.value;
        Config.World.CritDamageMod = CritDamageMod.value;
        Config.World.BaseGrazeChance = BaseGrazeChance.value;
        Config.World.GrazeDamageMod = GrazeDamageMod.value;
        Config.World.OverallMonsterCapModifier = OverallMonsterCapModifier.value;
        Config.World.OverallMonsterSpawnRateModifier = OverallMonsterSpawnRateModifier.value;

        if (int.TryParse(TacticalMovementSoftCap.text, out int tmsc))
            Config.World.TacticalMovementSoftCap = tmsc;
        else
            Config.World.TacticalMovementSoftCap = -1;

        if (int.TryParse(TacticalMovementHardCap.text, out int tmhc))
            Config.World.TacticalMovementHardCap = tmhc;
        else
            Config.World.TacticalMovementHardCap = -1;

        if (float.TryParse(SizeAccuracyMod.text, out float sam))
            Config.World.SizeAccuracyMod = sam;
        else
            Config.World.SizeAccuracyMod = 0;

        if (float.TryParse(SizeAccuracyLowerBound.text, out float salb))
            Config.World.SizeAccuracyLowerBound = salb;
        else
            Config.World.SizeAccuracyLowerBound = 10;

        if (int.TryParse(SizeAccuracyInterval.text, out int sai))
            Config.World.SizeAccuracyInterval = sai;
        else
            Config.World.SizeAccuracyInterval = 5;

        if (float.TryParse(SizeDamageMod.text, out float sdm))
            Config.World.SizeDamageMod = sdm;
        else
            Config.World.SizeDamageMod = 0;

        if (float.TryParse(SizeDamageLowerBound.text, out float sdlb))
            Config.World.SizeDamageLowerBound = sdlb;
        else
            Config.World.SizeDamageLowerBound = 10;

        if (int.TryParse(SizeDamageInterval.text, out int sdi))
            Config.World.SizeDamageInterval = sdi;
        else
            Config.World.SizeDamageInterval = 5;

        foreach (MonsterSpawnerPanel spawner in MonsterSpawners)
        {
            SpawnerInfo info = Config.SpawnerInfo(spawner.race);
            if (spawner.SpawnEnabled.isOn == false && State.World != null && State.World.AllActiveEmpires != null)
            {
                var emp = State.World?.GetEmpireOfRace(spawner.race);
                if (emp != null)
                {
                    if (emp.Armies?.Count > 0)
                        emp.Armies = new List<Army>();
                }

            }
            info.Enabled = spawner.SpawnEnabled.isOn;
            info.spawnRate = spawner.SpawnRate.value;
            info.AddOnRace = spawner.AddonRace.isOn;
            info.MonsterScoutMP = spawner.MonsterScoutMP.isOn;
            if (int.TryParse(spawner.ScalingRate.text, out int scaling))
                info.scalingFactor = scaling;
            else
                info.scalingFactor = 40;
            if (int.TryParse(spawner.MaxArmies.text, out int armies))
                info.MaxArmies = armies;
            else
                info.MaxArmies = 4;
            if (int.TryParse(spawner.MinArmySize.text, out int minSize))
            {
                if (minSize > 48) minSize = 48;
                info.MinArmySize = minSize;
            }
            else
                info.MinArmySize = 8;
            if (int.TryParse(spawner.MaxArmySize.text, out int maxSize))
            {
                if (maxSize > 48) maxSize = 48;
                if (maxSize < minSize) maxSize = minSize;
                info.MaxArmySize = maxSize;
            }
            else
                info.MaxArmySize = 12;
            if (int.TryParse(spawner.Team.text, out int team))
                info.Team = team;
            else
                info.Team = 900 + (int)spawner.race;

            if (int.TryParse(spawner.TurnOrder.text, out int turnOrder))
                info.TurnOrder = turnOrder;
            else
                info.TurnOrder = 40;

            if (int.TryParse(spawner.SpawnAttempts.text, out int attempts))
                info.SpawnAttempts = attempts;
            else
                info.SpawnAttempts = 1;

            if (float.TryParse(spawner.Confidence.text, out float confidence))
                info.Confidence = confidence;
            else
                info.Confidence = 6f;

            if (spawner.ConquestType.value > 0)
                info.SetSpawnerType((Config.MonsterConquestType)(spawner.ConquestType.value - 2));
            else
                info.UsingCustomType = false;
            if (spawner.DayNightMonsterMovemnt.value > 0)
                info.SetSpawnerCycleMoveType((Config.DayNightMovemntType)(spawner.DayNightMonsterMovemnt.value));
            else
                info.UsingCustomType = false;
        }
        if (State.World != null && State.World.MonsterEmpires != null)
        {
            State.World.PopulateMonsterTurnOrders();
            State.World.RefreshTurnOrder();
            RelationsManager.ResetMonsterRelations(); //So it will recalculate any changed teams.  
        }

        if (Config.MultiRaceVillages != oldMulti)
        {
            if (State.World?.Villages != null)
            {
                if (oldMulti)
                    foreach (var village in State.World.Villages)
                    {
                        village.VillagePopulation.ConvertToSingleRace();
                    }
                else
                    foreach (var village in State.World.Villages)
                    {
                        village.VillagePopulation.ConvertToMultiRace();
                    }
            }

        }


    }

    void SaveValues()
    {
        foreach (ToggleObject toggle in Toggles)
        {
            PlayerPrefs.SetInt(toggle.Name, toggle.Toggle.isOn ? 1 : 0);
        }
        PlayerPrefs.SetFloat("MaleFraction", 1 - FemaleFraction.value);
        PlayerPrefs.SetFloat("HermFraction", HermFraction.value);
        PlayerPrefs.SetFloat("HermNameFraction", HermNameFraction.value);
        PlayerPrefs.SetFloat("ClothedFraction", ClothedFraction.value);
        PlayerPrefs.SetFloat("FurryFraction", FurryFraction.value);
        PlayerPrefs.SetFloat("WeightLossFractionBreasts", WeightLossFractionBreasts.value);
        PlayerPrefs.SetFloat("WeightLossFractionBody", WeightLossFractionBody.value);
        PlayerPrefs.SetFloat("WeightLossFractionDick", WeightLossFractionDick.value);
        if (int.TryParse(GrowthDecayIncreaseRate.text, out int gir))
            PlayerPrefs.SetFloat("GrowthDecayIncreaseRate", gir / 1000f);
        else
            PlayerPrefs.SetFloat("GrowthDecayIncreaseRate", 0.04f);
        if (int.TryParse(GrowthDecayOffset.text, out int gos))
            PlayerPrefs.SetFloat("GrowthDecayOffset", gos / 100f);
        else
            PlayerPrefs.SetFloat("GrowthDecayOffset", 0);
        if (int.TryParse(GrowthMod.text, out int gm))
            PlayerPrefs.SetFloat("GrowthMod", gm / 100f);
        else
            PlayerPrefs.SetFloat("GrowthMod", 1f);
        if (int.TryParse(GrowthCap.text, out int gc))
            PlayerPrefs.SetFloat("GrowthCap", gc / 100f);
        else
            PlayerPrefs.SetFloat("GrowthCap", 5f);
        PlayerPrefs.SetFloat("TacticalWaterValue", TacticalWaterValue.value);
        PlayerPrefs.SetFloat("TacticalTerrainFrequency", TacticalTerrainFrequency.value);
        PlayerPrefs.SetFloat("OverallMonsterSpawnRateModifier", OverallMonsterSpawnRateModifier.value);
        PlayerPrefs.SetFloat("OverallMonsterCapModifier", OverallMonsterCapModifier.value);
        PlayerPrefs.SetInt("BreastSizeModifier", (int)BreastSizeModifier.value);
        PlayerPrefs.SetInt("HermBreastSizeModifier", (int)HermBreastSizeModifier.value);
        PlayerPrefs.SetInt("CockSizeModifier", (int)CockSizeModifier.value);
        PlayerPrefs.SetInt("StartingWeight", (int)DefaultStartingWeight.value);
        PlayerPrefs.SetInt("MonsterConquest", MonsterConquest.value - 1);
        PlayerPrefs.SetInt("VoreRate", VoreRate.value - 1);
        PlayerPrefs.SetInt("EscapeRate", EscapeRate.value - 1);
        PlayerPrefs.SetInt("RandomEventRate", RandomEventRate.value);
        PlayerPrefs.SetInt("RandomAIEventRate", RandomAIEventRate.value);
        PlayerPrefs.SetFloat("BurpFraction", BurpFraction.value);
        PlayerPrefs.SetFloat("FartFraction", FartFraction.value);
        PlayerPrefs.SetFloat("WeightGainFraction", WeightGainFraction.value);
        PlayerPrefs.SetFloat("DigestionSpeedMult", DigestionSpeedMult.value);
        PlayerPrefs.SetFloat("AbsorbSpeedMult", AbsorbSpeedMult.value);
        PlayerPrefs.SetFloat("BellyRubEffMult", BellyRubEffMult.value);
        PlayerPrefs.SetInt("BellyRubsPerTurn", (int)BellyRubsPerTurn.value);
        PlayerPrefs.SetFloat("DigestionRamp", DigestionRamp.value);
        PlayerPrefs.SetInt("DigestionRampTurn", (int)DigestionRampTurn.value);
        PlayerPrefs.SetInt("DigestionRampCap", (int)DigestionRampCap.value);
        PlayerPrefs.SetFloat("DigestionRampLoss", DigestionRampLoss.value);
        PlayerPrefs.SetFloat("AbsorbRamp", AbsorbRamp.value);
        PlayerPrefs.SetFloat("AbsorbResourceMod", AbsorbResourceMod.value);
        PlayerPrefs.SetFloat("DigestionCap", DigestionCap.value);
        PlayerPrefs.SetFloat("DigestionFlatDmg", DigestionFlatDmg.value);
        PlayerPrefs.SetInt("DigestionGraceTurns", (int)DigestionGraceTurns.value);
        PlayerPrefs.SetFloat("SurrenderedPredEscapeMult", SurrenderedPredEscapeMult.value);
        PlayerPrefs.SetFloat("SurrenderedPredAutoRegur", SurrenderedPredAutoRegur.value);
        PlayerPrefs.SetInt("AbsorbResourceModBoost", AbsorbResourceModBoost.value);
        PlayerPrefs.SetInt("ArmyMP", (int)ArmyMP.value);
        PlayerPrefs.SetInt("ScoutMP", (int)ScoutMP.value);
        PlayerPrefs.SetInt("ScoutMax", (int)ScoutMax.value);
        PlayerPrefs.SetFloat("ArmyCreationMPMod", ArmyCreationMPMod.value);
        PlayerPrefs.SetFloat("ArmyCreationMPCurve", ArmyCreationMPCurve.value);
        PlayerPrefs.SetFloat("CustomEventFrequency", CustomEventFrequency.value);
        PlayerPrefs.SetFloat("AutoSurrenderChance", AutoSurrenderChance.value);
        PlayerPrefs.SetFloat("AutoSurrenderDefectChance", AutoSurrenderDefectChance.value);
        PlayerPrefs.SetInt("MaxArmies", (int)MaxArmies.value);
        PlayerPrefs.SetInt("FemalesLike", FemalesLike.value);
        PlayerPrefs.SetInt("WinterStuff", WinterStuff.value);
        PlayerPrefs.SetInt("MalesLike", MalesLike.value);
        PlayerPrefs.SetInt("FairyBVType", FairyBVType.value);
        PlayerPrefs.SetInt("FeedingType", FeedingType.value);
        PlayerPrefs.SetInt("FourthWallBreakType", FourthWallBreakType.value);
        PlayerPrefs.SetInt("UBConversion", UBConversion.value);
        PlayerPrefs.SetInt("GoddessMercy", GoddessMercy.value);
        PlayerPrefs.SetInt("SucklingPermission", SucklingPermission.value);
        PlayerPrefs.SetInt("DiplomacyScale", DiplomacyScale.value);
        PlayerPrefs.SetInt("MaxSpellLevelDrop", MaxSpellLevelDrop.value + 1);
        PlayerPrefs.SetInt("MonsterConquestTurns", int.TryParse(MonsterConquestTurns.text, out int monsterTurns) ? monsterTurns : 0);
        PlayerPrefs.SetString("LeaderTraits", LeaderTraits.text);
        PlayerPrefs.SetString("MaleTraits", MaleTraits.text);
        PlayerPrefs.SetString("FemaleTraits", FemaleTraits.text);
        PlayerPrefs.SetString("HermTraits", HermTraits.text);
        PlayerPrefs.SetString("SpawnTraits", SpawnTraits.text);
        PlayerPrefs.SetInt("OralWeight", (int)OralWeight.value);
        PlayerPrefs.SetInt("AnalWeight", (int)AnalWeight.value);
        PlayerPrefs.SetInt("BreastWeight", (int)BreastWeight.value);
        PlayerPrefs.SetInt("UnbirthWeight", (int)UnbirthWeight.value);
        PlayerPrefs.SetInt("CockWeight", (int)CockWeight.value);
        PlayerPrefs.SetInt("TailWeight", (int)TailWeight.value);
        PlayerPrefs.SetInt("FogDistance", (int)FogDistance.value);
        PlayerPrefs.SetInt("DefualtTacticalSightRange", (int)DefualtTacticalSightRange.value);
        PlayerPrefs.SetInt("NightStrategicSightReduction", (int)NightStrategicSightReduction.value);
        PlayerPrefs.SetInt("NightRounds", (int)NightRounds.value);
        if (int.TryParse(RevealTurn.text, out int rvl))
            PlayerPrefs.SetFloat("RevealTurn", rvl);
        else
            PlayerPrefs.SetFloat("RevealTurn", 50);
        PlayerPrefs.SetFloat("BaseNightChance", BaseNightChance.value);
        PlayerPrefs.SetFloat("NightChanceIncrease", NightChanceIncrease.value);
        PlayerPrefs.SetFloat("BaseCritChance", BaseCritChance.value);
        PlayerPrefs.SetFloat("CritDamageMod", CritDamageMod.value);
        PlayerPrefs.SetFloat("BaseGrazeChance", BaseGrazeChance.value);
        PlayerPrefs.SetFloat("GrazeDamageMod", GrazeDamageMod.value);


        if (int.TryParse(TacticalMovementSoftCap.text, out int tmsc))
            PlayerPrefs.SetFloat("TacticalMovementSoftCap", tmsc);
        else
            PlayerPrefs.SetFloat("TacticalMovementSoftCap", -1);
        if (int.TryParse(TacticalMovementHardCap.text, out int tmhc))
            PlayerPrefs.SetFloat("TacticalMovementHardCap", tmhc);
        else
            PlayerPrefs.SetFloat("TacticalMovementHardCap", -1);
        if (float.TryParse(SizeAccuracyMod.text, out float sam))
            PlayerPrefs.SetFloat("SizeAccuracyMod", sam);
        else
            PlayerPrefs.SetFloat("SizeAccuracyMod", 0);
        if (float.TryParse(SizeAccuracyLowerBound.text, out float salb))
            PlayerPrefs.SetFloat("SizeAccuracyLowerBound", salb);
        else
            PlayerPrefs.SetFloat("SizeAccuracyLowerBound", 10);
        if (int.TryParse(SizeAccuracyInterval.text, out int sai))
            PlayerPrefs.SetFloat("SizeAccuracyInterval", sai);
        else
            PlayerPrefs.SetFloat("SizeAccuracyInterval", 5);
        if (float.TryParse(SizeDamageMod.text, out float sbm))
            PlayerPrefs.SetFloat("SizeDamageMod", sbm);
        else
            PlayerPrefs.SetFloat("SizeDamageMod", 0);
        if (float.TryParse(SizeDamageLowerBound.text, out float sblb))
            PlayerPrefs.SetFloat("SizeDamageLowerBound", sblb);
        else
            PlayerPrefs.SetFloat("SizeDamageLowerBound", 10);
        if (int.TryParse(SizeDamageInterval.text, out int sbi))
            PlayerPrefs.SetFloat("SizeDamageInterval", sbi);
        else
            PlayerPrefs.SetFloat("SizeDamageInterval", 5);

        foreach (MonsterSpawnerPanel spawner in MonsterSpawners)
        {
            PlayerPrefs.SetInt($"{spawner.race} Enabled", spawner.SpawnEnabled.isOn ? 1 : 0);
            PlayerPrefs.SetInt($"{spawner.race} Add-On", spawner.AddonRace.isOn ? 1 : 0);
            PlayerPrefs.SetFloat($"{spawner.race} Spawn Rate", spawner.SpawnRate.value);

            if (int.TryParse(spawner.ScalingRate.text, out int scaling))
                PlayerPrefs.SetInt($"{spawner.race} Scale Factor", scaling);
            else
                PlayerPrefs.SetInt($"{spawner.race} Scale Factor", 40);
            if (int.TryParse(spawner.MaxArmies.text, out int armies))
                PlayerPrefs.SetInt($"{spawner.race} Max Armies", armies);
            else
                PlayerPrefs.SetInt($"{spawner.race} Max Armies", 4);

            if (float.TryParse(spawner.Confidence.text, out float confidence))
                PlayerPrefs.SetFloat($"{spawner.race} Confidence", confidence);
            else
                PlayerPrefs.SetFloat($"{spawner.race} Confidence", 6f);

            if (int.TryParse(spawner.MinArmySize.text, out int minSize))
                PlayerPrefs.SetInt($"{spawner.race} Min Army Size", minSize);
            else
                PlayerPrefs.SetInt($"{spawner.race} Min Army Size", 8);

            if (int.TryParse(spawner.MaxArmySize.text, out int maxSize))
                PlayerPrefs.SetInt($"{spawner.race} Max Army Size", maxSize);
            else
                PlayerPrefs.SetInt($"{spawner.race} Max Army Size", 12);

            if (int.TryParse(spawner.Team.text, out int team))
                PlayerPrefs.SetInt($"{spawner.race} Team", team);
            else
                PlayerPrefs.SetInt($"{spawner.race} Team", 900 + (int)spawner.race);

            if (int.TryParse(spawner.TurnOrder.text, out int turnOrder))
                PlayerPrefs.SetInt($"{spawner.race} Turn Order", turnOrder);
            else
                PlayerPrefs.SetInt($"{spawner.race} Turn Order", 40);

            if (int.TryParse(spawner.SpawnAttempts.text, out int attempts))
                PlayerPrefs.SetInt($"{spawner.race} Attempts", attempts);
            else
                PlayerPrefs.SetInt($"{spawner.race} Attempts", 1);

            PlayerPrefs.SetInt($"{spawner.race} Conquest Type", spawner.ConquestType.value);

            PlayerPrefs.SetInt($"{spawner.race} D/N Move Type", spawner.DayNightMonsterMovemnt.value);

            if (State.World?.AllActiveEmpires != null)
            {
                var emp = State.World.GetEmpireOfRace(spawner.race);
                if (emp != null)
                    RelationsManager.TeamUpdated(emp);
            }


        }
    }

    public void MonsterDropdownChanged()
    {
        MonsterConquestTurns.interactable = MonsterConquest.value > 2;
    }

    public void DiplomacyChanged()
    {
        DiplomacyScale.interactable = Diplomacy.isOn;
        LockedAIRelations.interactable = Diplomacy.isOn;
    }

    public void SurrenderChanged()
    {
        if (AutoSurrender.isOn == true)
        {
            GoddessMercy.interactable = true;
        }
        else if (AutoSurrender.isOn == false)
        {
            GoddessMercy.interactable = false;
        }
    }

    public void Exit()
    {
        SetValues();
        buildingSettings.SoftSave();
        gameObject.SetActive(false);
    }

    public void ExitAndSave()
    {
        SetValues();
        SaveValues();

        buildingSettings.DropdownUpdate();
        buildingSettings.SoftSave();
        buildingSettings.HardSave();

        gameObject.SetActive(false);
    }

    public void RefreshSliderText()
    {
        ArmyMP.GetComponentInChildren<Text>().text = $"Army MP : {ArmyMP.value}";
        ScoutMP.GetComponentInChildren<Text>().text = $"Scout MP : {ScoutMP.value}";
        ScoutMax.GetComponentInChildren<Text>().text = $"Scout Size : {ScoutMax.value}";
        CustomEventFrequency.GetComponentInChildren<Text>().text = $"Custom % : {Math.Round(100 * CustomEventFrequency.value, 1)}";
        MaxArmies.GetComponentInChildren<Text>().text = $"MaxArmies : {MaxArmies.value}";
        WeightLossFractionBreasts.GetComponentInChildren<Text>().text = $"Breasts: {Math.Round(100 * WeightLossFractionBreasts.value, 1)}% chance per turn";
        WeightLossFractionBody.GetComponentInChildren<Text>().text = $"Body: {Math.Round(100 * WeightLossFractionBody.value, 1)}% chance per turn";
        WeightLossFractionDick.GetComponentInChildren<Text>().text = $"Dick: {Math.Round(100 * WeightLossFractionDick.value, 1)}% chance per turn";
    }

    public void MercSortChanged()
    {
        foreach (ToggleObject toggle in Toggles)
        {
            Config.World.Toggles[toggle.Name] = toggle.Toggle.isOn;
        }
        foreach (var obj in MercToggles.ToList())
        {
            MercToggles.Remove(obj);
            Toggles.Remove(obj);
            Destroy(obj.Toggle.gameObject);
        }
        if (MercSortMethod.value == 0)
        {
            foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).Where(s => (int)s >= 0).OrderBy((s) => s.ToString()))
            {
                var obj = new ToggleObject(CreateMercToggle(race), $"Merc {race}", true);
                MercToggles.Add(obj);
                Toggles.Add(obj);
            }
        }
        if (MercSortMethod.value == 1)
        {
            foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).Where(s => s < Race.Succubi && s >= 0).OrderBy((s) => s.ToString()))
            {
                var obj = new ToggleObject(CreateMercToggle(race), $"Merc {race}", true);
                MercToggles.Add(obj);
                Toggles.Add(obj);
            }
            foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).Where(s => s >= Race.Succubi && s < Race.Vagrants).OrderBy((s) => s.ToString()))
            {
                var obj = new ToggleObject(CreateMercToggle(race), $"Merc {race}", true);
                MercToggles.Add(obj);
                Toggles.Add(obj);
            }
            foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).Where(s => s >= Race.Vagrants && s < Race.Selicia).OrderBy((s) => s.ToString()))
            {
                var obj = new ToggleObject(CreateMercToggle(race), $"Merc {race}", true);
                MercToggles.Add(obj);
                Toggles.Add(obj);
            }
            foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).Where(s => s >= Race.Selicia).OrderBy((s) => s.ToString()))
            {
                var obj = new ToggleObject(CreateMercToggle(race), $"Merc {race}", true);
                MercToggles.Add(obj);
                Toggles.Add(obj);
            }
        }
        if (MercSortMethod.value == 2)
        {
            foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).Where(s => (int)s >= 0))
            {
                var obj = new ToggleObject(CreateMercToggle(race), $"Merc {race}", true);
                MercToggles.Add(obj);
                Toggles.Add(obj);
            }
        }

        foreach (ToggleObject toggle in Toggles)
        {
            toggle.Toggle.isOn = Config.World.GetValue(toggle.Name);
        }

    }

    public void MercSortDirectionChanged()
    {
        var grid = MercenaryToggleFolder.GetComponent<GridLayoutGroup>();
        if (MercSortDirection.isOn)
            grid.startAxis = GridLayoutGroup.Axis.Horizontal;
        else
            grid.startAxis = GridLayoutGroup.Axis.Vertical;

    }

    public void WeightGainChanged()
    {
        WeightLoss.interactable = WeightGain.isOn;
        if (WeightGain.isOn == false)
        {
            WeightLoss.isOn = false;
        }
        WeightLossFractionBreasts.interactable = WeightGain.isOn && WeightLoss.isOn;
        WeightLossFractionBody.interactable = WeightGain.isOn && WeightLoss.isOn;
        WeightLossFractionDick.interactable = WeightGain.isOn && WeightLoss.isOn;
    }

    public void WeightLossChanged()
    {
        WeightLossFractionBreasts.interactable = WeightGain.isOn && WeightLoss.isOn;
        WeightLossFractionBody.interactable = WeightGain.isOn && WeightLoss.isOn;
        WeightLossFractionDick.interactable = WeightGain.isOn && WeightLoss.isOn;
    }

    public void VoreTypesChanged()
    {
        AnalWeight.interactable = AnalVore.isOn;
        TailWeight.interactable = TailVore.isOn;
        BreastWeight.interactable = BreastVore.isOn;
        UnbirthWeight.interactable = Unbirth.isOn;
        CockWeight.interactable = CockVore.isOn;
    }

    public void KuroTenkoChanged()
    {
        //Scarab was here
        FeedingType.interactable = KuroTenkoEnabled.isOn;
        OverhealEXP.interactable = KuroTenkoEnabled.isOn && FeedingType.value != 3;
        UBConversion.interactable = KuroTenkoEnabled.isOn;
        SucklingPermission.interactable = KuroTenkoEnabled.isOn && (int)Config.FeedingType == 0;
        TransferAllowed.interactable = KuroTenkoEnabled.isOn;
        CumGestation.interactable = KuroTenkoEnabled.isOn && TransferAllowed.isOn;
        if (FeedingType.value == 3)
            OverhealEXP.isOn = false;
        if (!TransferAllowed.isOn)
            CumGestation.isOn = false;
        SpecialMercsCanConvert.interactable = KuroTenkoEnabled.isOn && (int)Config.UBConversion <= 1;
        NoScatForDeadTransfers.interactable = KuroTenkoEnabled.isOn;
        AlwaysRandomizeConverted.interactable = KuroTenkoEnabled.isOn;
    }

    public void DayNightCycleChanged()
    {
        //Scarab was here
        DayNightCosmetic.interactable = DayNightEnabled.isOn;
        DayNightSchedule.interactable = DayNightEnabled.isOn;
        DayNightRandom.interactable = DayNightEnabled.isOn;
        NightMonsters.interactable = DayNightEnabled.isOn;
        NightMoveMonsters.interactable = DayNightEnabled.isOn;
        NightRounds.interactable = DayNightEnabled.isOn && DayNightSchedule.isOn;
        BaseNightChance.interactable = DayNightEnabled.isOn && DayNightRandom.isOn;
        NightChanceIncrease.interactable = DayNightEnabled.isOn && DayNightRandom.isOn;
        DefualtTacticalSightRange.interactable = DayNightEnabled.isOn;
        NightStrategicSightReduction.interactable = DayNightEnabled.isOn;
    }

}
