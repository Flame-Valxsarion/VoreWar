using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

public static class State
{
    static int saveErrors = 0;
    public const string Version = "45E";
    public static World World;
    public static Rand Rand = new Rand();
    public static NameGenerator NameGen;
    public static GameManager GameManager;
    public static AssimilateList AssimilateList;
    public static Dictionary<Traits, TaggedTrait> TieredTraitsList;
    public static List<String> TieredTraitsTagsList;
    public static List<RandomizeList> RandomizeLists;
    public static List<CustomTraitBoost> CustomTraitList;
    public static List<ConditionalTraitContainer> ConditionalTraitList;
    public static List<UnitTag> UnitTagList;
    public static Dictionary<Traits, List<int>> UnitTagAssociatedTraitDictionary;
    public static Dictionary<TaggedTrait, bool> UntaggedTraits;

    internal static EventList EventList;

    internal static RaceSettings RaceSettings;

    public static bool TutorialMode;
    public static bool Warned = false;

    public static string SaveDirectory;
    public static string StorageDirectory;
    public static string MapDirectory;
    public static string CustomTraitDirectory;
    public static string ConditionalTraitDirectory;
    public static string UnitTagDirectory;
    public static string NameFileDirectory;

    public static int RaceSlot;
    public static string RaceSaveDataName;
    public static string[] nameTextFileNames = new string[] {"armyNames", "males","females","monsters","femaleFeralLions","maleFeralLions","femaleAabayx","maleAabayx","Cake","Collectors","Compy","CoralSlugs","DarkSwallower","Dragonfly","Catfish","Earthworms","Vagrants","femaleAlligators"
            ,"maleAlligators","femaleAlraune","maleAlraune","femaleAnts","maleAnts","femaleAvians","maleAvians","femaleBats","maleBats","femaleBees","maleBees","femaleBunnies","maleBunnies","femaleCats","maleCats","femaleCockatrice","maleCockatrice","femaleCrux","maleCrux","femaleCrypters"
            ,"maleCrypters","femaleDeer","maleDeer","femaleDewSprites","femaleDogs","maleDogs","femaleDragon","maleDragon","femaleDratopyr","maleDratopyr","femaleDriders","maleDriders","femaleEasternDragon","maleEasternDragon","femaleEquines","maleEquines","femaleFairies","maleFairies"
            ,"femaleFeralBats","maleFeralBats","femaleFeralFox","maleFeralFox","femaleFeralHorses","maleFeralHorses","femaleFeralLizards","maleFeralLizards","femaleFoxes","maleFoxes","femaleFrogs","maleFrogs","femaleGazelle","maleGazelle","femaleGoblins","maleGoblins"
            ,"femaleGryphons","maleGryphons","femaleHamsters","maleHamsters","femaleHarpies","maleHarpies","femaleHippos","maleHippos","femaleHumans","maleHumans","femaleImps","maleImps","femaleKangaroos","maleKangaroos","femaleKobolds","maleKobolds","femaleKomodos"
            ,"maleKomodos","femaleLamia","maleLamia","femaleLizards","maleLizards","femaleMantis","maleMantis","femaleMerfolk","maleMerfolk","femaleMonitors","maleMonitors","femalePanthers","malePanthers","femalePuca","malePuca","femaleScylla","maleScylla","femaleSergal"
            ,"maleSergal","femaleSharks","maleSharks","femaleSlimes","maleSlimes","femaleSuccubi","maleSuccubi","femaleTaurus","maleTaurus","femaleTerrorbird","maleTerrorbird","femaleTigers","maleTigers","femaleVargul","maleVargul","femaleVipers","maleVipers","femaleWolves","maleWolves"
            ,"femaleWyvern","maleWyvern","femaleYouko","maleYouko","FeralAnts","FeralFrogs","FeralSharks","FeralWolves","Harvesters","Raptor","RockSlugs","Salamanders","Schiwardez","Serpents","SpitterSlugs","SpringSlugs","Voilin","WarriorAnts","Whisp","femaleBoomBunnies"
            ,"maleBoomBunnies","WyvernMatron","maleFeralOrcas","femaleFeralOrcas","femaleBears","maleBears","femaleCentaur","maleCentaur","femaleGnolls","maleGnolls","femaleMainlandElves","maleMainlandElves","femaleViisels","maleViisels","FeralSlimes","femaleEevee","maleEevee","femaleEqualeon"
            ,"maleEqualeon","femaleUmbreon","maleUmbreon","maleLupine","femaleLupine","femaleMatronsMinions","maleMatronsMinions","femaleJackals","maleJackals","femaleRwuMercenaries","maleRwuMercenaries","TwistedVines","femaleOtachi","maleOtachi","femaleRaiju","maleRaiju","femaleSmudger","maleSmudger","femaleBadgers","maleBadgers"
            ,"WoodDryad","RiverDryad","EarthDryad","FungalDryad","maleGhosts","femaleGhosts","femaleUtahraptor","maleUtahraptor","femaleTrex","maleTrex","femaleSpaceCroach","maleSpaceCroach","femaleMice","maleMice","Terminid","femaleFeralEevee","maleFeralEevee","femaleFeralEqualeon","maleFeralEqualeon","femaleFeralUmbreon","maleFeralUmbreon","Iliijiith","maleRenamon","femaleRenamon"
            ,"maleOoviKat","femaleOoviKat","maleDraconians","femaleDraconians","maleYordles","femaleYordles","Pudding","maleBlackWidow","femaleBlackWidow"};

    static State()
    {
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            SaveDirectory = Application.persistentDataPath + $"Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar;
            MapDirectory = Application.persistentDataPath + $"Maps{Path.DirectorySeparatorChar}";
            CustomTraitDirectory = Application.persistentDataPath + $"CustomTraits{Path.DirectorySeparatorChar}";
            ConditionalTraitDirectory = Application.persistentDataPath + $"ConditionalTraits{Path.DirectorySeparatorChar}";
            UnitTagDirectory = Application.persistentDataPath + $"UnitTags{Path.DirectorySeparatorChar}";
            NameFileDirectory = Application.persistentDataPath + $"NameFiles{Path.DirectorySeparatorChar}";
        }
        else
        {
            SaveDirectory = $"UserData{Path.DirectorySeparatorChar}Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = $"UserData{Path.DirectorySeparatorChar}";
            MapDirectory = $"UserData{Path.DirectorySeparatorChar}Maps{Path.DirectorySeparatorChar}";
            CustomTraitDirectory = $"UserData{Path.DirectorySeparatorChar}CustomTraits{Path.DirectorySeparatorChar}";
            ConditionalTraitDirectory = $"UserData{Path.DirectorySeparatorChar}ConditionalTraits{Path.DirectorySeparatorChar}";
            UnitTagDirectory = $"UserData{Path.DirectorySeparatorChar}UnitTags{Path.DirectorySeparatorChar}";
            NameFileDirectory = $"UserData{Path.DirectorySeparatorChar}NameFiles{Path.DirectorySeparatorChar}";
        }
        try
        {
            Directory.CreateDirectory(StorageDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(MapDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(SaveDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(CustomTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(ConditionalTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(UnitTagDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(NameFileDirectory.TrimEnd(new char[] { '\\', '/' }));
        }
        catch
        {
            SaveDirectory = Application.persistentDataPath + $"Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar;
            MapDirectory = Application.persistentDataPath + $"Maps{Path.DirectorySeparatorChar}";
            CustomTraitDirectory = Application.persistentDataPath + $"CustomTraits{Path.DirectorySeparatorChar}";
            ConditionalTraitDirectory = Application.persistentDataPath + $"ConditionalTraits{Path.DirectorySeparatorChar}";
            UnitTagDirectory = Application.persistentDataPath + $"UnitTags{Path.DirectorySeparatorChar}";
            NameFileDirectory = Application.persistentDataPath + $"NameFiles{Path.DirectorySeparatorChar}";
            Directory.CreateDirectory(StorageDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(MapDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(SaveDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(CustomTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(ConditionalTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(UnitTagDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(NameFileDirectory.TrimEnd(new char[] { '\\', '/' }));
        }

        string[] systemTextFileNames = new string[] { "customTraits", "events" };

        try
        {
            foreach (string text in systemTextFileNames)
            {
                if (File.Exists($"{StorageDirectory}{text}.txt") == false)
                    File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}{text}.txt", $"{StorageDirectory}{text}.txt");
            }
        }
        catch
        {
            Debug.Log("Initial setup failed!");
        }
        try
        {
            foreach (string nameList in nameTextFileNames)
            {
                if (File.Exists($"{NameFileDirectory}{nameList}.txt") == false)
                    File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}{nameList}.txt", $"{NameFileDirectory}{nameList}.txt");
            }
        }
        catch
        {
            Debug.Log("Name setup failed!");
        }

        try
        {
            if (File.Exists($"{StorageDirectory}taggedTraits.json") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}taggedTraits.json", $"{StorageDirectory}taggedTraits.json");
            if (File.Exists($"{StorageDirectory}buildingConfig.json") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}buildingConfig.json", $"{StorageDirectory}buildingConfig.json");
        }
        catch
        {
            Debug.Log("Initial setup failed!");
        }

        string bundlePath = Path.Combine($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}AssetBundles", "abakhanskya");

        AssetBundleLoader.LoadAssetBundles();

        FlagLoader.FlagLoader flagLoader = new FlagLoader.FlagLoader();
        flagLoader.LoadFlags();
        NameGen = new NameGenerator();
        EventList = new EventList();
        AssimilateList = new AssimilateList();
        CustomTraitList = new List<CustomTraitBoost>();
        ConditionalTraitList = new List<ConditionalTraitContainer>();
        UnitTagList = new List<UnitTag>();
        UnitTagAssociatedTraitDictionary = new Dictionary<Traits, List<int>>();
        UntaggedTraits = new Dictionary<TaggedTrait, bool>();

        TieredTraitsList = ExternalTraitHandler.TaggedTraitParser(); 
        TieredTraitsList = ExternalTraitHandler.TaggedTraitUpdater();
        TieredTraitsTagsList = new List<string>();
        ExternalTraitHandler.CustomTraitParser();
        ExternalTraitHandler.ConditionalTraitParser();
        ExternalTraitHandler.UnitTagParser();
        TagConditionChecker.CompileTraitTagAssociateDict();
        Encoding encoding = Encoding.GetEncoding("iso-8859-1");
        List<string> lines;
        RandomizeLists = new List<RandomizeList>();
        if (File.Exists($"{State.StorageDirectory}customTraits.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}customTraits.txt", encoding);
            if (logFile.Any())
            {
                lines = new List<string>(logFile);
                int count = 0;
                lines.ForEach(line =>
                {
                    count++;
                    RandomizeList custom = new RandomizeList();
                    line = new string(line.Where(c => !Char.IsWhiteSpace(c)).ToArray());
                    string[] strings = line.Split(',');
                    if (strings.Length == 4)
                    {
                        custom.id = int.Parse(strings[0]);
                        custom.name = strings[1];
                        custom.chance = float.Parse(strings[2], new CultureInfo("en-US"));
                        custom.level = 0;
                        custom.count = 1;
                        custom.RandomTraits = strings[3].Split('|').ToList().ConvertAll(s => (Traits)int.Parse(s));
                        RandomizeLists.Add(custom);
                    }
                    else if (strings.Length == 6)
                    {
                        custom.id = int.Parse(strings[0]);
                        custom.name = strings[1];
                        custom.chance = float.Parse(strings[2], new CultureInfo("en-US"));
                        custom.count = int.Parse(strings[3]);
                        custom.level = int.Parse(strings[4]);
                        custom.RandomTraits = strings[5].Split('|').ToList().ConvertAll(s => (Traits)int.Parse(s));
                        RandomizeLists.Add(custom);
                    }
                });
            }
        }

        foreach (Traits trait in (Traits[])Enum.GetValues(typeof(Traits)))
        {
            if (TieredTraitsList.Keys.Contains(trait))
            {
                if (TieredTraitsList[trait].tags == null)
                {
                    if (UntaggedTraits.ContainsKey(TieredTraitsList[trait]))
                    {
                        continue;
                    }
                    UntaggedTraits.Add(TieredTraitsList[trait], true);
                    continue;
                }
                if (TieredTraitsList[trait].tags.Count <= 0)
                {
                    UntaggedTraits.Add(TieredTraitsList[trait], true);
                }
            }
            else
            {
                TaggedTrait newTrait = new TaggedTrait();
                newTrait.name = trait.ToString();
                newTrait.tierValue = TraitTier.Neutral;
                newTrait.tier = newTrait.tierValue.ToString();
                newTrait.traitEnum = trait;
                UntaggedTraits.Add(newTrait, false);
            }
        }

        List<TaggedTrait> newTraits = new List<TaggedTrait>();
        foreach (var newTrait in UntaggedTraits)
        {
            if (newTrait.Value)
            {
                continue;
            }
            newTraits.Add(newTrait.Key);
        }

        ExternalTraitHandler.AppendTaggedTrait(newTraits);
    }

    public static void WipeUserdata()
    {
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            if (Directory.Exists(Application.persistentDataPath))
                Directory.Delete(Application.persistentDataPath, true);
            SaveDirectory = Application.persistentDataPath + $"Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar;
            MapDirectory = Application.persistentDataPath + $"Maps{Path.DirectorySeparatorChar}";
            CustomTraitDirectory = Application.persistentDataPath + $"CustomTraits{Path.DirectorySeparatorChar}";
            ConditionalTraitDirectory = Application.persistentDataPath + $"ConditionalTraits{Path.DirectorySeparatorChar}";
            UnitTagDirectory = Application.persistentDataPath + $"UnitTags{Path.DirectorySeparatorChar}";
            NameFileDirectory = Application.persistentDataPath + $"NameFiles{Path.DirectorySeparatorChar}";
        }
        else
        {
            if (Directory.Exists($"UserData"))
                Directory.Delete($"UserData", true);
            SaveDirectory = $"UserData{Path.DirectorySeparatorChar}Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = $"UserData{Path.DirectorySeparatorChar}";
            MapDirectory = $"UserData{Path.DirectorySeparatorChar}Maps{Path.DirectorySeparatorChar}";
            CustomTraitDirectory = $"UserData{Path.DirectorySeparatorChar}CustomTraits{Path.DirectorySeparatorChar}";
            ConditionalTraitDirectory = $"UserData{Path.DirectorySeparatorChar}ConditionalTraits{Path.DirectorySeparatorChar}";
            UnitTagDirectory = $"UserData{Path.DirectorySeparatorChar}UnitTags{Path.DirectorySeparatorChar}";
            NameFileDirectory = $"UserData{Path.DirectorySeparatorChar}NameFiles{Path.DirectorySeparatorChar}";
        }
        try
        {
            Directory.CreateDirectory(StorageDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(MapDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(SaveDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(CustomTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(ConditionalTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(UnitTagDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(NameFileDirectory.TrimEnd(new char[] { '\\', '/' }));
        }
        catch
        {
            SaveDirectory = Application.persistentDataPath + $"Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar;
            MapDirectory = Application.persistentDataPath + $"Maps{Path.DirectorySeparatorChar}";
            CustomTraitDirectory = Application.persistentDataPath + $"CustomTraits{Path.DirectorySeparatorChar}";
            ConditionalTraitDirectory = Application.persistentDataPath + $"ConditionalTraits{Path.DirectorySeparatorChar}";
            UnitTagDirectory = Application.persistentDataPath + $"UnitTags{Path.DirectorySeparatorChar}";
            NameFileDirectory = Application.persistentDataPath + $"NameFiles{Path.DirectorySeparatorChar}";
            Directory.CreateDirectory(StorageDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(MapDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(SaveDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(CustomTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(ConditionalTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(UnitTagDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(NameFileDirectory.TrimEnd(new char[] { '\\', '/' }));
        }

        string[] systemTextFileNames = new string[] { "customTraits", "events" };

        try
        {
            foreach (string text in systemTextFileNames)
            {
                if (File.Exists($"{StorageDirectory}{text}.txt") == false)
                    File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}{text}.txt", $"{StorageDirectory}{text}.txt");
            }
        }
        catch
        {
            Debug.Log("Initial setup failed!");
        }
        try
        {
            foreach (string nameList in nameTextFileNames)
            {
                if (File.Exists($"{NameFileDirectory}{nameList}.txt") == false)
                    File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}{nameList}.txt", $"{NameFileDirectory}{nameList}.txt");
            }
        }
        catch
        {
            Debug.Log("Name setup failed!");
        }

        try
        {
            if (File.Exists($"{StorageDirectory}taggedTraits.json") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}taggedTraits.json", $"{StorageDirectory}taggedTraits.json");
            if (File.Exists($"{StorageDirectory}buildingConfig.json") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}buildingConfig.json", $"{StorageDirectory}buildingConfig.json");
        }
        catch
        {
            Debug.Log("Initial setup failed!");
        }

        FlagLoader.FlagLoader flagLoader = new FlagLoader.FlagLoader();
        flagLoader.LoadFlags();
        NameGen = new NameGenerator();
        EventList = new EventList();
        AssimilateList = new AssimilateList();
        CustomTraitList = new List<CustomTraitBoost>();
        ConditionalTraitList = new List<ConditionalTraitContainer>();
        UnitTagList = new List<UnitTag>();
        UnitTagAssociatedTraitDictionary = new Dictionary<Traits, List<int>>();
        UntaggedTraits = new Dictionary<TaggedTrait, bool>();

        TieredTraitsList = ExternalTraitHandler.TaggedTraitParser();
        TieredTraitsTagsList = new List<string>();
        ExternalTraitHandler.CustomTraitParser();
        ExternalTraitHandler.ConditionalTraitParser();
        ExternalTraitHandler.UnitTagParser();
        TagConditionChecker.CompileTraitTagAssociateDict();
        Encoding encoding = Encoding.GetEncoding("iso-8859-1");
        List<string> lines;
        RandomizeLists = new List<RandomizeList>();
        if (File.Exists($"{State.StorageDirectory}customTraits.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}customTraits.txt", encoding);
            if (logFile.Any())
            {
                lines = new List<string>(logFile);
                int count = 0;
                lines.ForEach(line =>
                {
                    count++;
                    RandomizeList custom = new RandomizeList();
                    line = new string(line.Where(c => !Char.IsWhiteSpace(c)).ToArray());
                    string[] strings = line.Split(',');
                    if (strings.Length == 4)
                    {
                        custom.id = int.Parse(strings[0]);
                        custom.name = strings[1];
                        custom.chance = float.Parse(strings[2], new CultureInfo("en-US"));
                        custom.level = 0;
                        custom.count = 1;
                        custom.RandomTraits = strings[3].Split('|').ToList().ConvertAll(s => (Traits)int.Parse(s));
                        RandomizeLists.Add(custom);
                    }
                    else if (strings.Length == 6)
                    {
                        custom.id = int.Parse(strings[0]);
                        custom.name = strings[1];
                        custom.chance = float.Parse(strings[2], new CultureInfo("en-US"));
                        custom.count = int.Parse(strings[3]);
                        custom.level = int.Parse(strings[4]);
                        custom.RandomTraits = strings[5].Split('|').ToList().ConvertAll(s => (Traits)int.Parse(s));
                        RandomizeLists.Add(custom);
                    }
                });
            }
        }

        foreach (Traits trait in (Traits[])Enum.GetValues(typeof(Traits)))
        {
            if (TieredTraitsList.Keys.Contains(trait))
            {
                if (TieredTraitsList[trait].tags == null)
                {
                    UntaggedTraits.Add(TieredTraitsList[trait], true);
                    continue;
                }
                if (TieredTraitsList[trait].tags.Count <= 0)
                {
                    UntaggedTraits.Add(TieredTraitsList[trait], true);
                }
            }
            else
            {
                TaggedTrait newTrait = new TaggedTrait();
                newTrait.name = trait.ToString();
                newTrait.tierValue = TraitTier.Neutral;
                newTrait.tier = newTrait.tierValue.ToString();
                newTrait.traitEnum = trait;
                UntaggedTraits.Add(newTrait, false);
            }
        }

        List<TaggedTrait> newTraits = new List<TaggedTrait>();
        foreach (var newTrait in UntaggedTraits)
        {
            if (newTrait.Value)
            {
                continue;
            }
            newTraits.Add(newTrait.Key);
        }

        ExternalTraitHandler.AppendTaggedTrait(newTraits);
    }

    public static void SaveEditedRaces()
    {
        try
        {
            byte[] bytes = SerializationUtility.SerializeValue(RaceSettings, DataFormat.Binary);
            File.WriteAllBytes(RaceSaveDataName, bytes);
        }
        catch
        {
            Debug.LogWarning("Failed to properly save edited races!");
        }
    }

    public static void LoadRaceData()
    {
        RaceSlot = PlayerPrefs.GetInt("RaceEditorSlot", 1);
        ChangeRaceSlotUsed(RaceSlot);
    }

    public static void ChangeRaceSlotUsed(int num)
    {
        RaceSlot = num;
        PlayerPrefs.SetInt("RaceEditorSlot", num);
        if (RaceSlot <= 1)
            RaceSaveDataName = $"{StorageDirectory}EditedRaces.dat";
        else if (RaceSlot == 2)
            RaceSaveDataName = $"{StorageDirectory}EditedRaces2.dat";
        else if (RaceSlot == 3)
            RaceSaveDataName = $"{StorageDirectory}EditedRaces3.dat";
        LoadEditedRaces();
    }

    public static void LoadEditedRaces()
    {
        try
        {
            if (File.Exists(RaceSaveDataName))
            {
                byte[] bytes = File.ReadAllBytes(RaceSaveDataName);
                RaceSettings = SerializationUtility.DeserializeValue<RaceSettings>(bytes, DataFormat.Binary);
                GameManager.Start_Mode.miscText.text = "Successfully read race settings";
                RaceSettings.Sanitize();
            }
            else
            {
                RaceSettings = new RaceSettings();
                GameManager.Start_Mode.miscText.text = "No modified race settings found, using default";
            }
        }
        catch
        {
            RaceSettings = new RaceSettings();
            GameManager.Start_Mode.miscText.text = "Failed to properly read race settings";
        }
    }

    public static void ResetNamelists()
    {
        try
        {
            foreach (string nameList in nameTextFileNames)
            {
                if (File.Exists($"{NameFileDirectory}{nameList}.txt") == true)
                {
                    File.Delete($"{NameFileDirectory}{nameList}.txt");
                    File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}{nameList}.txt", $"{NameFileDirectory}{nameList}.txt");
                }
                else
                {
                    File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}{nameList}.txt", $"{NameFileDirectory}{nameList}.txt");
                }
            }
            NameGen = new NameGenerator();
        }
        catch
        {
            Debug.LogWarning("Namelist refresh failed!");
        }
    }

    public static void ReloadNamelists()
    {
        NameGen = new NameGenerator();
    }

    public static void Save(string filename)
    {
        try
        {
            for (int i = 0; i < 3; i++)
            {
                if (filename.EndsWith("/") || filename.EndsWith("\\"))
                    filename = filename.Remove(filename.Length - 1, 1);
                else
                    break;
            }

            World.SaveVersion = Version;
            if (GameManager.CurrentScene == GameManager.TacticalMode)
            {
                GameManager.CameraController.SaveTacticalCamera();
                World.TacticalData = GameManager.TacticalMode.Export();
            }
            else
                World.TacticalData = null;
            byte[] bytes = SerializationUtility.SerializeValue(World, DataFormat.Binary);
            File.WriteAllBytes(filename, bytes);
        }
        catch
        {
            saveErrors++;
            if (saveErrors < 3)
            {
                GameManager.CreateMessageBox($"Unable to save properly, {filename} didn't work (will only warn 3 times in a single session)");
            }
            else if (saveErrors == 3)
            {
                GameManager.CreateMessageBox($"Unable to save properly, {filename} didn't work (will no longer warn you this session)");
            }
        }
    }

    public static World PreviewSave(string filename)
    {
        if (filename.EndsWith("/") || filename.EndsWith("\\"))
            filename = filename.Remove(filename.Length - 1, 1);
        if (!File.Exists(filename))
        {
            return null;
        }
        World tempWorld;
        try
        {
            byte[] bytes = File.ReadAllBytes(filename);
            tempWorld = SerializationUtility.DeserializeValue<World>(bytes, DataFormat.Binary);
            return tempWorld;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static void Load(string filename, bool tutorial = false)
    {
        if (filename.EndsWith("/") || filename.EndsWith("\\"))
            filename = filename.Remove(filename.Length - 1, 1);
        if (!File.Exists(filename))
        {
            GameManager.CreateMessageBox("Couldn't find the saved file");
            return;
        }
        try
        {
            GameManager.StrategyMode.ClearData();
            GameManager.StrategyMode.CleanUpLingeringWindows();
            if (tutorial == false)
                GameManager.SwitchToMainMenu();
            byte[] bytes = File.ReadAllBytes(filename);
            World = SerializationUtility.DeserializeValue<World>(bytes, DataFormat.Binary);

            if (World.Empires != null)
            {
                World.MainEmpires = World.Empires.ToList();
                World.RefreshEmpires();
            }

            if (tutorial)
            {
                var catEmp = World.GetEmpireOfRace(Race.Cats);
                var impEmp = World.GetEmpireOfRace(Race.Imps);

                catEmp.Armies[0].SetEmpire(catEmp);
                impEmp.Armies[0].SetEmpire(impEmp);
                TutorialMode = true;
            }
            else
            {
                TutorialMode = false;
            }

            // New version check. Initially considered making an array of applicable versions to bridge gaps, but just grabbing the version number should be plenty
            string versionStr = System.Text.RegularExpressions.Regex.Match(World.SaveVersion, @"\d+").Value;
            int version = int.Parse(versionStr);
            string versionUpdateMessage = "";

            VillageBuildingList.SetBuildings(World.crazyBuildings);
            if (version < 12)
            {
                World = null;
                GameManager.CreateMessageBox("This save file is from before version 12. I took the liberty of doing a clean sweep when I added the new garrisons to improve the code quality. Sorry. You can still load .map files from before version 12 though.");
                return;
            }
            Config.World = World.ConfigStorage;
            if (World.BuildingConfigStorage != null)
            {
                Config.BuildConfig = World.BuildingConfigStorage;
            }
            GameManager.Menu.Options.LoadFromStored();
            GameManager.Menu.CheatMenu.LoadFromStored();

            if (World.MercenaryHouses == null)
                World.MercenaryHouses = new MercenaryHouse[0];

            foreach (MercenaryHouse house in World.MercenaryHouses)
            {
                if (house.Mercenaries != null)
                {
                    foreach (var merc in house.Mercenaries)
                    {
                        merc.Unit.InitializeTraits();
                    }
                }
            }

            if (World.AncientTeleporters == null)
                World.AncientTeleporters = new AncientTeleporter[0];

            foreach (MercenaryHouse house in World.MercenaryHouses)
            {
                if (house.Mercenaries != null)
                {
                    foreach (var merc in house.Mercenaries)
                    {
                        merc.Unit.InitializeTraits();
                    }
                }
            }

            if (World.Claimables == null)
                World.Claimables = new ClaimableBuilding[0];
            if (World.Constructibles == null)
                World.Constructibles = new ConstructibleBuilding[0];
            ItemRepository newRepo = new ItemRepository();
            World.ItemRepository = newRepo;
            //Always runs for new versions
            if (World.SaveVersion != Version && World.AllActiveEmpires != null)
            {
                if (World.GetEmpireOfSide(700) == null)
                {
                    World.MainEmpires.Add(new Empire(new Empire.ConstructionArgs(700, Color.red, new Color(.6f, 0, 0), 5, StrategyAIType.Basic, TacticalAIType.Full, 700, 16, 16)));
                    World.RefreshEmpires();
                }
                else
                {
                    World.GetEmpireOfSide(700).Name = "Rebels";
                    if (World.EmpireOrder.Where(s => s.Side == 700).Any() == false)
                        World.EmpireOrder.Add(World.GetEmpireOfSide(700));
                }
                if (World.GetEmpireOfSide(701) == null)
                {
                    World.MainEmpires.Add(new Empire(new Empire.ConstructionArgs(701, Color.red, new Color(.6f, 0, 0), 7, StrategyAIType.Basic, TacticalAIType.Full, 701, 16, 16)));
                    World.RefreshEmpires();
                }
                else
                {
                    World.GetEmpireOfSide(701).Name = "Bandits";
                }
                /*
                if (World.GetEmpireOfSide(702) == null)
                {
                    World.MainEmpires.Add(new Empire(new Empire.ConstructionArgs(702, Color.red, new Color(.6f, 0, 0), 5, StrategyAIType.Basic, TacticalAIType.Full, 702, 16, 16)));
                    World.RefreshEmpires();
                }
                else
                {
                    World.GetEmpireOfSide(702).Name = "Outcasts";
                    if (World.EmpireOrder.Where(s => s.Side == 702).Any() == false)
                        World.EmpireOrder.Add(World.GetEmpireOfSide(702));
                }
                */
                if (version < 30 + 1)
                {
                    if (World.AllActiveEmpires != null)
                    {
                        foreach (Village village in World.Villages)
                        {
                            village.ConvertToMultiRace();
                        }
                    }
                }

                foreach (var unit in StrategicUtilities.GetAllUnits())
                {
                    unit.UpdateItems(newRepo);
                    unit.ReloadTraits();
                }
                foreach (Empire empire in World.AllActiveEmpires)
                {
                    foreach (Army army in empire.Armies)
                    {
                        foreach (Unit unit in army.Units)
                        {
                            if (unit.Side != army.Side)
                                unit.Side = army.Side;
                            if (unit.BodySize < 0) //Can take this out later, was a fix for 14H
                                unit.BodySize = 0;
                        }
                    }
                }

                if (Config.MaxSpellLevelDrop == 0)
                    Config.World.MaxSpellLevelDrop = 4;
                if (Config.MaxEquipmentLevelDrop == 0)
                    Config.World.MaxEquipmentLevelDrop = 4;
            }

            if (version < 18 + 1)
            {
                if (Config.LeaderLossLevels == 0)
                    Config.World.LeaderLossLevels = 1;
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (unit.Race == Race.Goblins) //Re-randomize because the number of options has dropped
                            unit.EyeType = Rand.Next(3);
                    }
                }
            }

            if (version < 20 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        int oldSpeed = unit.GetStatBase(Stat.Mind);
                        unit.ModifyStat(Stat.Agility, Math.Max(oldSpeed - 10, 0));
                        unit.ModifyStat(Stat.Mind, unit.Level + 10 - oldSpeed);
                    }
                }
            }

            if (version < 21 + 1)
            {
                if (World.Villages != null)
                {
                    foreach (Village village in World.Villages)
                    {
                        if (village.buildings.Contains(VillageBuilding.empty)) //Removes Sub pens
                        {
                            village.buildings.Remove(VillageBuilding.empty);
                        }
                    }
                }
                if (World.Relations != null)
                {
                    RelationsManager.ResetRelations();
                }
            }

            if (version < 21D + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        if (empire.StrategicAI == null)
                            continue;
                        foreach (Army army in empire.Armies)
                        {
                            foreach (Unit unit in army.Units)
                            {
                                StrategicUtilities.SetAIClass(unit);
                            }
                        }
                    }

                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        foreach (Army army in empire.Armies)
                        {
                            foreach (Unit unit in army.Units)
                            {
                                if (unit.Race == Race.Lizards) //Adjustment for the added clothing item
                                    if (unit.ClothingType == 4)
                                        unit.ClothingType = 5;
                                    else if (unit.ClothingType == 5)
                                        unit.ClothingType = 6;
                            }
                        }
                    }
                }
            }

            if (version < 22 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire emp in World.AllActiveEmpires)
                    {
                        emp.Name = emp.Race.ToString();
                    }
                }
            }

            if (version < 26 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        foreach (Army army in empire.Armies)
                        {
                            foreach (Unit unit in army.Units)
                            {
                                if (unit.Race == Race.Lizards) //Adjustment for the added clothing item
                                    if (unit.ClothingType >= 5)
                                        unit.ClothingType++;
                            }
                        }
                    }
                }
            }

            if (version < 26 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        foreach (Army army in empire.Armies)
                        {
                            foreach (Unit unit in army.Units)
                            {
                                if (unit.Race == Race.Cierihaka) //Adjustment for the added clothing item
                                {
                                    unit.FixedGear = true;
                                    unit.Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.CierihakaWeapon);
                                }
                            }
                        }
                    }
                }
            }

            if (version < 26D + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        if (empire.Leader?.Race == Race.Bees)
                            empire.Leader.ClothingType = 6;
                    }
                }
            }

            if (version < 27 + 1)
            {
                Config.World.Toggles["Defections"] = true;
            }

            if (version < 28 + 1)
            {
                Config.World.OralWeight = 40;
                Config.World.BreastWeight = 40;
                Config.World.CockWeight = 40;
                Config.World.TailWeight = 40;
                Config.World.UnbirthWeight = 40;
                Config.World.AnalWeight = 40;
                Config.World.BladderWeight = 40;
            }

            if (version < 28 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        foreach (Army army in empire.Armies)
                        {
                            foreach (Unit unit in army.Units)
                            {
                                if (unit.Race == Race.Succubi)
                                {
                                    if (unit.ClothingType2 == 3)
                                        unit.ClothingType2 = 2;
                                }
                            }
                        }
                    }
                }
            }

            if (version < 28 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        foreach (Army army in empire.Armies)
                        {
                            army.NameArmy(empire);
                        }
                    }
                }
            }

            if (version < 29 + 1)
            {
                World.ConfigStorage.OverallMonsterCapModifier = 1;
                World.ConfigStorage.OverallMonsterSpawnRateModifier = 1;
            }

            if (version < 30 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    var raceData = Races.GetRace(Race.Bees);
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (unit.Race == Race.Bees)
                            raceData.RandomCustom(unit);
                    }
                }
            }

            if (version < 30 + 1)
            {
                Config.World.AutoSurrenderChance = 1;
            }

            if (version < 31 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        if (empire.CapitalCity != null)
                            empire.ReplacedRace = empire.CapitalCity.OriginalRace;
                        else
                            empire.ReplacedRace = empire.Race;
                    }
                }
            }

            if (version < 32 + 1)
            {
                World.ConfigStorage.StartingPopulation = 99999;
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        if (empire.StrategicAI != null && empire.StrategicAI is StrategicAI ai)
                        {
#pragma warning disable CS0612 // Type or member is obsolete
                            if (ai.strongerAI)
#pragma warning restore CS0612 // Type or member is obsolete
                                ai.CheatLevel = 1;
                        }
                        if (empire.CapitalCity != null)
                            empire.ReplacedRace = empire.CapitalCity.OriginalRace;
                        else
                            empire.ReplacedRace = empire.Race;
                    }
                }
            }

            if (version < 34 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (unit.Race == Race.Bats || unit.Race == Race.Equines)
                        {
                            unit.TotalRandomizeAppearance();
                        }
                    }
                }
            }

            if (version < 34 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        if (empire.CapitalCity != null)
                            empire.ReplacedRace = empire.CapitalCity.OriginalRace;
                        else
                            empire.ReplacedRace = empire.Race;
                    }
                }
            }

            if (version < 35 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    World.UpdateBanditLimits();
                }
            }

            if (version < 37 + 1)
            {
                foreach (var unit in StrategicUtilities.GetAllUnits())
                {
                    if (unit.HasVagina == false)
                    {
                        if (unit.HasBreasts && !unit.HasDick)
                            unit.HasVagina = true;
                        else if (!unit.HasBreasts && unit.HasDick)
                            unit.HasVagina = false;
                        else if (Config.World.GetValue("HermsCanUB"))
                            unit.HasVagina = true;
                        else
                            unit.HasVagina = false;
                    }
                }
            }

            if (version < 38 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (unit.Pronouns == null)
                        {
                            unit.GeneratePronouns();
                        }
                    }
                }
                else
                {
                    foreach (var unit in World.TacticalData.units)
                    {
                        if (unit.Unit.Pronouns == null)
                        {
                            unit.Unit.GeneratePronouns();
                        }
                    }
                }
            }

            if (version < 38 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (unit.GetGender() == Gender.Hermaphrodite || unit.GetGender() == Gender.Gynomorph)
                        {
                            unit.HasVagina = Config.HermsCanUB;
                        }
                    }
                }
            }

            if (version < 39 + 1)
            {
                World.ConfigStorage.FogDistance = 2;

                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (unit.Race == Race.Humans)
                        {
                            unit.RandomizeAppearance();
                        }
                    }
                }
            }

            if (version < 40 + 1)
            {
                if (World.TacticalData != null)
                {
                    foreach (var unit in World.TacticalData.units)
                    {
                        unit.modeQueue = new List<KeyValuePair<int, float>>();
                        unit.Unit.FixedSide = -1;
                    }
                }

                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        unit.FixedSide = -1;
                    }
                }
            }

            if (version < 41 + 1)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire emp in World.AllActiveEmpires)
                    {
                        foreach (Army army in emp.Armies)
                        {
                            army.impassables = new List<StrategicTileType>() { StrategicTileType.mountain, StrategicTileType.snowMountain, StrategicTileType.water, StrategicTileType.lava, StrategicTileType.ocean, StrategicTileType.brokenCliffs};
                        }
                    }
                }
            }

            if (version <= 42)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (unit.Race != Race.Cats)
                        {
                            unit.SpawnRace = RaceSettings.Get(unit.Race).SpawnRace;
                            unit.ConversionRace = RaceSettings.Get(unit.Race).ConversionRace;
                        }
                    }
                }
                if (World.TacticalData != null)
                {
                    foreach (var unit in World.TacticalData.units)
                    {
                        if (unit.Unit.Race != Race.Cats)
                        {
                            unit.Unit.SpawnRace = RaceSettings.Get(unit.Unit.Race).SpawnRace;
                            unit.Unit.ConversionRace = RaceSettings.Get(unit.Unit.Race).ConversionRace;
                        }
                    }
                }
            }

            if (version < 44 + 1 || World.SaveVersion.Equals("45A") || World.SaveVersion.Equals("45B") || World.SaveVersion.Equals("45C") || World.SaveVersion.Equals("45D"))
            {
                // Herein are the updates specific to version 45E.
                
                // Since DefenseEncampment.AvailibleDefenders was renamed to AvailableDefenders, they will not have loaded correctly (if present).
                bool defcampsfound = false;
                bool lumsitefound = false;
                foreach (ConstructibleBuilding constructible in World.Constructibles)
                {
                    if (constructible is DefenseEncampment defcamp)
                    {
                        defcampsfound = true;
                        defcamp.AvailableDefenders = defcamp.maxDefenders / 2;
                    }
                    if (constructible is LumberSite lumsite)
                    {
                        // Corrects an issue where workers were being double allocated to creating prefabs
                        if (((LumberSite)constructible).carpenterUpgrade.built)
                        {
                            lumsitefound = true;
                            lumsite.carpenterWorkers /= 2;
                        }
                    }
                }
                if (defcampsfound)
                {
                    versionUpdateMessage += "Version 45: Changes to DefenseEncampments lost data for their available defenders. All DefenseEncampments have been assigned an arbitrary number of available defenders (half of their maxima).\n";
                }
                if (lumsitefound)
                {
                    versionUpdateMessage += "Version 45: A fix to the Lumber Site's carpentry workers has been applied. Double check your Lumber Sites are set correctly.\n";
                }

                // Switches EmpireBuildingLimit to count up instead of down, allowing the build limit to be changed mid game and not break
                foreach (Empire empire in World.AllActiveEmpires)
                {
                    empire.EmpireBuildingLimit = new Dictionary<ConstructibleType, int>
                    {
                        [ConstructibleType.WorkCamp] = Config.BuildConfig.WorkCamp.BuildLimit - empire.EmpireBuildingLimit[ConstructibleType.WorkCamp],
                        [ConstructibleType.LumberSite] = Config.BuildConfig.LumberSite.BuildLimit - empire.EmpireBuildingLimit[ConstructibleType.LumberSite],
                        [ConstructibleType.Quarry] = Config.BuildConfig.Quarry.BuildLimit - empire.EmpireBuildingLimit[ConstructibleType.Quarry],
                        [ConstructibleType.CasterTower] = Config.BuildConfig.CasterTower.BuildLimit - empire.EmpireBuildingLimit[ConstructibleType.CasterTower],
                        [ConstructibleType.BarrierTower] = Config.BuildConfig.BarrierTower.BuildLimit - empire.EmpireBuildingLimit[ConstructibleType.BarrierTower],
                        [ConstructibleType.DefEncampment] = Config.BuildConfig.DefenseEncampment.BuildLimit - empire.EmpireBuildingLimit[ConstructibleType.DefEncampment],
                        [ConstructibleType.Academy] = Config.BuildConfig.Academy.BuildLimit - empire.EmpireBuildingLimit[ConstructibleType.Academy],
                        [ConstructibleType.DarkMagicTower] = Config.BuildConfig.DarkMagicTower.BuildLimit - empire.EmpireBuildingLimit[ConstructibleType.DarkMagicTower],
                        [ConstructibleType.TemporalTower] = Config.BuildConfig.TemporalTower.BuildLimit - empire.EmpireBuildingLimit[ConstructibleType.TemporalTower],
                        [ConstructibleType.Teleporter] = Config.BuildConfig.Teleporter.BuildLimit - empire.EmpireBuildingLimit[ConstructibleType.Teleporter],
                        [ConstructibleType.Laboratory] = Config.BuildConfig.Laboratory.BuildLimit - empire.EmpireBuildingLimit[ConstructibleType.Laboratory],
                        [ConstructibleType.TownHall] = Config.BuildConfig.TownHall.BuildLimit - empire.EmpireBuildingLimit[ConstructibleType.TownHall],
                    };
                }

                // The tutorial needs a special fix for its Units (in addition to fixing their _position values, like in the else block below).
                if (tutorial == true)
                {
                    // One problem (introduced by Version 45) is the loss of position data, hereunder recreated manually.
                    // Second problem (introduced further back) is the lack of PermanentTraits in the tutorial save, hereunder fixed by calling a function that re-initializes PermanentTraits.
                    List<Actor_Unit> actors = World.TacticalData.units;
                    actors[0].SetPos(new Vec2i(14, 15));
                    actors[0].Unit.AddPermanentTrait(Traits.Bulky); // ...Unit.PermanentTraits = new (...) would work better, but PermanentTraits is protected; let's respect that, and use a workaround.
                    actors[0].Unit.RemoveTrait(Traits.Bulky);
                    actors[1].SetPos(new Vec2i(16, 12));
                    actors[1].Unit.AddPermanentTrait(Traits.Bulky);
                    actors[1].Unit.RemoveTrait(Traits.Bulky);
                    actors[2].SetPos(new Vec2i(3, 14));
                    actors[2].Unit.AddPermanentTrait(Traits.Bulky);
                    actors[2].Unit.RemoveTrait(Traits.Bulky);
                    actors[3].SetPos(new Vec2i(6, 5));
                    for (int i = 4; i < actors.Count; ++i)
                    {
                        actors[i].SetPos(new Vec2i(i, 1)); // Some actors that aren't visible (but can still generate errors).
                    }
                    
                    versionUpdateMessage += "Version 45: The tutorial had a problem, when it came time to give your unit a level up. The tutorial's data has been fixed to prevent the error (and also, a fix for the actor positions, introduced by this version update).\n";
                }
                
                // Since Actor_Unit.Position was renamed to Actor_Unit._position, no tactical battle will have loaded correctly.
                else if (World.TacticalData != null)
                {
                    // I made TacticalMode.DropAllUnits() specifically for this purpose, only to learn that loading the TacticalData into TacticalMode at all (without fixing it first) will cause disastrous errors anyway.
                    // Now, I recreate the whole algorithm for use on TacticalData.
                    
                    TacticalData data = World.TacticalData;
                    TacticalTileType[,] tiles = data.tiles;
                    int width = data.tiles.GetUpperBound(0) + 1;
                    int height = data.tiles.GetUpperBound(1) + 1;
                    bool[,] validlocations = new bool[width, height];
                    
                    int x;
                    int y;
                    for (x = 0; x < width; ++x)
                        for (y = 0; y < height; ++y)
                            validlocations[x, y] = TacticalTileInfo.CanWalkInto(data.tiles[x, y], null);
                    foreach (TacticalBuildings.TacticalBuilding bldg in data.buildings)
                        for (x = 0; x < bldg.Width && bldg.LowerLeftPosition.x + x < width; ++x)
                            for (y = 0; y < bldg.Height && bldg.LowerLeftPosition.y + y < height; ++y)
                                validlocations[bldg.LowerLeftPosition.x + x, bldg.LowerLeftPosition.y + y] = false;
                    foreach (DecorationStorage dec in data.decorationStorage)
                        for (x = 0; x < TacticalDecorations.TacticalDecorationList.DecDict[dec.Type].Width && dec.Position.x + x < width; ++x)
                            for (y = 0; y < TacticalDecorations.TacticalDecorationList.DecDict[dec.Type].Height && dec.Position.x + x < height; ++y)
                                validlocations[dec.Position.x + x, dec.Position.y + y] = false;
                    
                    bool[,] visitedtiles = new bool[width, height];
                    int remainingunvisitedtiles = width * height;
                    bool[,] bestnetwork = new bool[width, height];
                    int bestnetworksize = 0;
                    for (x = 0; x < width; ++x)
                    {
                        for (y = 0; y < height; ++y)
                        {
                            if (visitedtiles[x, y] != true)
                            {
                                visitedtiles[x, y] = true;
                                --remainingunvisitedtiles;
                                
                                if (validlocations[x, y])
                                {
                                    bool[,] currentnetwork = new bool[width, height];
                                    currentnetwork[x, y] = true;
                                    int currentnetworksize = 1;
                                    Stack<Vec2i> tilestack = new Stack<Vec2i>();
                                    Vec2i pos = new Vec2i(x, y);
                                    
                                    void AddTileToStack(Vec2i tile)
                                    {
                                        if (tile.x < 0) return;
                                        if (tile.x >= width) return;
                                        if (tile.y < 0) return;
                                        if (tile.y >= height) return;
                                        if (visitedtiles[tile.x, tile.y]) return;
                                        tilestack.Push(tile);
                                        visitedtiles[tile.x, tile.y] = true; // Not literally visited at this time, but queued for an inevitable visit (and we don't want this tile on the stack again).
                                        --remainingunvisitedtiles;
                                    }
                                    
                                    void AddAdjacentTilesToStack()
                                    {
                                        AddTileToStack(new Vec2i(pos.x - 1, pos.y - 1));
                                        AddTileToStack(new Vec2i(pos.x - 1, pos.y));
                                        AddTileToStack(new Vec2i(pos.x - 1, pos.y + 1));
                                        AddTileToStack(new Vec2i(pos.x, pos.y - 1));
                                        AddTileToStack(new Vec2i(pos.x, pos.y + 1));
                                        AddTileToStack(new Vec2i(pos.x + 1, pos.y - 1));
                                        AddTileToStack(new Vec2i(pos.x + 1, pos.y));
                                        AddTileToStack(new Vec2i(pos.x + 1, pos.y + 1));
                                    }
                                    
                                    AddAdjacentTilesToStack();
                                    while (tilestack.Count > 0)
                                    {
                                        pos = tilestack.Pop();
                                        if (validlocations[pos.x, pos.y])
                                        {
                                            currentnetwork[pos.x, pos.y] = true;
                                            ++currentnetworksize;
                                            AddAdjacentTilesToStack();
                                        }
                                    }
                                    
                                    if (currentnetworksize > bestnetworksize)
                                    {
                                        for (int tmpx = 0; tmpx < width; ++tmpx)
                                            for (int tmpy = 0; tmpy < height; ++tmpy)
                                                bestnetwork[tmpx, tmpy] = currentnetwork[tmpx, tmpy];
                                        bestnetworksize = currentnetworksize;
                                    }
                                }
                            }
                            
                            if (bestnetworksize > remainingunvisitedtiles) break;
                        }
                        if (bestnetworksize > remainingunvisitedtiles) break;
                    }
                    
                    List<Vec2i> atkMeleePrimary;
                    List<Vec2i> atkMeleeSecondary;
                    List<Vec2i> atkRangedPrimary;
                    List<Vec2i> atkRangedSecondary;
                    List<Vec2i> atkSummonPrimary;
                    List<Vec2i> atkSummonSecondary;
                    List<Vec2i> atkTertiary;
                    List<Vec2i> atkFinal;
                    List<Vec2i> defMeleePrimary;
                    List<Vec2i> defMeleeSecondary;
                    List<Vec2i> defRangedPrimary;
                    List<Vec2i> defRangedSecondary;
                    List<Vec2i> defSummonPrimary;
                    List<Vec2i> defSummonSecondary;
                    List<Vec2i> defTertiary;
                    List<Vec2i> defFinal;
                    
                    void PopulateDropZones()
                    {
                        atkMeleePrimary = new List<Vec2i>();
                        atkMeleeSecondary = new List<Vec2i>();
                        atkRangedPrimary = new List<Vec2i>();
                        atkRangedSecondary = new List<Vec2i>();
                        atkSummonPrimary = new List<Vec2i>();
                        atkSummonSecondary = new List<Vec2i>();
                        atkTertiary = new List<Vec2i>();
                        atkFinal = new List<Vec2i>();
                        defMeleePrimary = new List<Vec2i>();
                        defMeleeSecondary = new List<Vec2i>();
                        defRangedPrimary = new List<Vec2i>();
                        defRangedSecondary = new List<Vec2i>();
                        defSummonPrimary = new List<Vec2i>();
                        defSummonSecondary = new List<Vec2i>();
                        defTertiary = new List<Vec2i>();
                        defFinal = new List<Vec2i>();
                        
                        void XTraverse(List<Vec2i> outerZone, List<Vec2i> midZone, List<Vec2i> innerZone)
                        {
                            Vec2i pos;
                            x = 0;
                            while (x < width / 8)
                            {
                                pos = new Vec2i(x, y);
                                if (bestnetwork[pos.x, pos.y])
                                    outerZone.Add(pos);
                                pos = new Vec2i(width - 1 - x, y);
                                if (bestnetwork[pos.x, pos.y])
                                    outerZone.Add(pos);
                                
                                ++x;
                            }
                            while (x < width / 4)
                            {
                                pos = new Vec2i(x, y);
                                if (bestnetwork[pos.x, pos.y])
                                    midZone.Add(pos);
                                pos = new Vec2i(width - 1 - x, y);
                                if (bestnetwork[pos.x, pos.y])
                                    midZone.Add(pos);
                                
                                ++x;
                            }
                            while (x < width / 2)
                            {
                                pos = new Vec2i(x, y);
                                if (bestnetwork[pos.x, pos.y])
                                    innerZone.Add(pos);
                                pos = new Vec2i(width - 1 - x, y);
                                if (bestnetwork[pos.x, pos.y])
                                    innerZone.Add(pos);
                                
                                ++x;
                            }
                            if (2 * x + 1 == width) // This conditional will resolve to TRUE if and only if the width of the tactical board is odd, and x is the coordinate of the middle-most column.
                            {
                                pos = new Vec2i(x, y);
                                if (bestnetwork[pos.x, pos.y])
                                    innerZone.Add(pos);
                            }
                        }
                        
                        y = 0;
                        int yloopcount = 0;
                        while (yloopcount < height / 8)
                        {
                            XTraverse(defTertiary, defTertiary, defTertiary);
                            ++y;
                            ++yloopcount;
                        }
                        while (yloopcount < height / 4)
                        {
                            XTraverse(defTertiary, defRangedSecondary, defRangedPrimary);
                            ++y;
                            ++yloopcount;
                        }
                        while (yloopcount < height * 3 / 8)
                        {
                            XTraverse(defTertiary, defMeleeSecondary, defMeleePrimary);
                            ++y;
                            ++yloopcount;
                        }
                        while (yloopcount < height / 2)
                        {
                            XTraverse(defFinal, defSummonSecondary, defSummonPrimary);
                            ++y;
                            ++yloopcount;
                        }
                        
                        y = height - 1;
                        yloopcount = 0;
                        while (yloopcount < height / 8)
                        {
                            XTraverse(atkTertiary, atkTertiary, atkTertiary);
                            --y;
                            ++yloopcount;
                        }
                        while (yloopcount < height / 4)
                        {
                            XTraverse(atkTertiary, atkRangedSecondary, atkRangedPrimary);
                            --y;
                            ++yloopcount;
                        }
                        while (yloopcount < height * 3 / 8)
                        {
                            XTraverse(atkTertiary, atkMeleeSecondary, atkMeleePrimary);
                            --y;
                            ++yloopcount;
                        }
                        while (yloopcount < height / 2)
                        {
                            XTraverse(atkFinal, atkSummonSecondary, atkSummonPrimary);
                            --y;
                            ++yloopcount;
                        }
                    }
                    
                    void Drop(Actor_Unit actor, int type)
                    {
                        // Assign a dummy position to actors that are prey. They shouldn't need a position of their own until escape or regurgitation, but assign it, to be safe.
                        if (actor.SelfPrey?.Predator != null)
                        {
                            actor.SetPos(new Vec2i(0, 0));
                            return;
                        }
                        
                        List<List<Vec2i>> droporder;
                        switch (type)
                        {
                            // type can't be a DropType from inside TacticalMode, so we'll use integers. Type meanings are noted.
                            case 1: // Attacker Melee actors.
                                droporder = new List<List<Vec2i>>() { atkMeleePrimary, atkMeleeSecondary, atkRangedPrimary, atkRangedSecondary, atkTertiary, atkSummonPrimary, atkSummonSecondary, atkFinal };
                                break;
                            case 2: // Attacker Ranged actors.
                                droporder = new List<List<Vec2i>>() { atkRangedPrimary, atkRangedSecondary, atkMeleePrimary, atkMeleeSecondary, atkTertiary, atkSummonPrimary, atkSummonSecondary, atkFinal };
                                break;
                            case 3: // Attacker Summon actors.
                                droporder = new List<List<Vec2i>>() { atkSummonPrimary, atkSummonSecondary, atkMeleePrimary, atkMeleeSecondary, atkRangedPrimary, atkRangedSecondary, atkTertiary, atkFinal };
                                break;
                            case 4: // Defender Melee actors.
                                droporder = new List<List<Vec2i>>() { defMeleePrimary, defMeleeSecondary, defRangedPrimary, defRangedSecondary, defTertiary, defSummonPrimary, defSummonSecondary, defFinal };
                                break;
                            case 5: // Defender Ranged actors.
                                droporder = new List<List<Vec2i>>() { defRangedPrimary, defRangedSecondary, defMeleePrimary, defMeleeSecondary, defTertiary, defSummonPrimary, defSummonSecondary, defFinal };
                                break;
                            case 6: // Defender Summon actors.
                                droporder = new List<List<Vec2i>>() { defSummonPrimary, defSummonSecondary, defMeleePrimary, defMeleeSecondary, defRangedPrimary, defRangedSecondary, defTertiary, defFinal };
                                break;
                            case 7: // Neutral actors.
                            default:
                                droporder = new List<List<Vec2i>>() { atkFinal, defFinal, defSummonSecondary, atkSummonSecondary, atkSummonPrimary, defSummonPrimary, defTertiary, atkTertiary };
                                break;
                        }
                        
                        foreach (List<Vec2i> dropzone in droporder)
                        {
                            int count = dropzone.Count();
                            if (count > 0)
                            {
                                int index = State.Rand.Next(count);
                                actor.SetPos(dropzone[index]);
                                dropzone.RemoveAt(index);
                                return;
                            }
                        }
                        
                        // Failsafe. Kill it, and place it in the corner.
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous("Killing actor " + actor.Unit.Name + " because there is no place to drop him.");
                        actor.SetPos(new Vec2i(0, 0));
                        actor.Unit.Health = 0;
                        actor.Targetable = false;
                        actor.Surrendered = true;
                        actor.Visible = false;
                        actor.PredatorComponent?.FreeAnyAlivePrey();
                        actor.Unit.Kill(); // Why do we need to execute so many statements to kill a unit?
                        return;
                    }
                    
                    PopulateDropZones();
                    foreach (Actor_Unit actor in data.units)
                    {
                        if (actor.Unit.GetApparentSide() == World.TacticalData.attackerSide)
                        {
                            if (actor.Unit.Type == UnitType.Summon)
                                Drop(actor, 3);
                            else if (actor.Unit.GetBestRanged() == null)
                                Drop(actor, 1);
                            else
                                Drop(actor, 2);
                        }
                        else if (actor.Unit.GetApparentSide() == World.TacticalData.defenderSide)
                        {
                            if (actor.Unit.Type == UnitType.Summon)
                                Drop(actor, 6);
                            else if (actor.Unit.GetBestRanged() == null)
                                Drop(actor, 4);
                            else
                                Drop(actor, 5);
                        }
                        else
                        {
                            Drop(actor, 7);
                        }
                    }
                    
                    versionUpdateMessage += "Version 45: Changes to Actor_Unit lost data for their locations, in tactical battles. All Actor_Units have been replaced to locations appropriate for beginning-of-battle.\n";
                }
            }

            if (World.TacticalData != null)
            {
                foreach (var unit in World.TacticalData.units)
                {
                    if (unit.modeQueue == null)
                        unit.modeQueue = new List<KeyValuePair<int, float>>();
                }
            }

            if (World.AllActiveEmpires != null)
            {
                foreach (Empire emp in World.AllActiveEmpires)
                {
                    if (emp.Side > 300)
                        continue;
                    var raceFlags = RaceSettings.GetRaceTraits(emp.Race);
                    if (raceFlags != null)
                    {
                        if (raceFlags.Contains(Traits.Prey))
                            emp.CanVore = false;
                    }
                }

                foreach (Empire emp in World.MainEmpires)
                {
                    if (emp.Side > 300)
                        continue;
                    if (RaceSettings.Exists(emp.Race))
                    {
                        emp.BannerType = RaceSettings.Get(emp.Race).BannerType;
                    }
                    else
                        emp.BannerType = 0;
                }

                foreach (var unit in StrategicUtilities.GetAllUnits())
                {
                    unit.InitializeTraits();
                }
            }
            if (World.Villages != null)
            {
                foreach (var village in World.Villages)
                {
                    if (village.FarmCount <= 0) village.UpdateFarms(8);
                    village.UpdateNetBoosts();
                }
            }

            if (Config.World.ArmyMP == 0)
                Config.World.ArmyMP = 3;

            if (Config.World.MaxArmies == 0)
                Config.World.MaxArmies = 12;

            if (Config.World.VillagersPerFarm == 0)
                Config.World.VillagersPerFarm = 6;

            if (Config.World.VillagerDevourEXP == 0)
                Config.World.VillagerDevourEXP = 1;

            if (Config.World.SoftLevelCap == 0)
                Config.World.SoftLevelCap = 999999;

            if (Config.World.HardLevelCap == 0)
                Config.World.HardLevelCap = 999999;

            if (Config.World.GoldMineIncome == 0)
                Config.World.GoldMineIncome = 40;

            if (Config.World.TacticalTerrainFrequency == 0)
                Config.World.TacticalTerrainFrequency = 10;

            if (Config.World.TacticalWaterValue == 0)
                Config.World.TacticalWaterValue = .29f;

            World.ItemRepository = new ItemRepository();

            if (version < 41 + 1)
            {
                if (Config.World.BaseCritChance == 0)
                    Config.World.BaseCritChance = .05f;

                if (Config.World.CritDamageMod == 0)
                    Config.World.CritDamageMod = 1.5f;

                if (Config.World.BaseGrazeChance == 0)
                    Config.World.BaseGrazeChance = .05f;

                if (Config.World.GrazeDamageMod == 0)
                    Config.World.GrazeDamageMod = .3f;
            }

            bool pureTactical = false;
            if (World.MainEmpires != null) //Is the detector for a pure tactical game.
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        empire.LoadFix(); //Compatibility Temporary fix to bridge the gap between versions; add your null checks here in Empire.cs
                    }
                }
                else
                {
                    foreach (Empire empire in World.MainEmpires)
                    {
                        empire.LoadFix(); //Compatibility Temporary fix to bridge the gap between versions; add your null checks here in Empire.cs
                    }
                }

                foreach (Empire empire in World.AllActiveEmpires)
                {
                    foreach (Army army in empire.Armies)
                    {
                        foreach (Unit unit in army.Units)
                        {
                            unit.ReloadTraits();//Add unit-based null checks for newly added internal(s) or protected(s) to this void in Unit.cs so that on loading an older version saved units will recive them
                        }
                    }
                }

                if (World.Relations == null)
                    RelationsManager.ResetRelations();
                GameManager.ClearPureTactical();
                GameManager.SwitchToStrategyMode(true);
                GameManager.StrategyMode.GenericSetup();
                GameManager.StrategyMode.CheckIfOnlyAIPlayers();

                MercenaryHouse.UpdateStaticStock();
            }
            else //If Pure Tactical
            {
                Config.WatchAIBattles = true;
                pureTactical = true;
            }

            if (World.TacticalData != null)
            {
                GameManager.SwitchToTacticalOnLoadedGame();
                GameManager.TacticalMode.LoadData(World.TacticalData);
                if (pureTactical)
                {
                    GameManager.TacticalMode.RefreshPureTacticalTraits();
                    GameManager.TacticalMode.ForceUpdate();
                }
                GameManager.TacticalMode.RefreshTileEffects();
            }
            
            if (versionUpdateMessage != "")
            {
                GameManager.CreateMessageBox("Updates to the game state due to game version updates are as follows:\n\n" + versionUpdateMessage);
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            GameManager.CreateMessageBox("Encountered an error when trying to load the save");
            return;
        }
    }
}
