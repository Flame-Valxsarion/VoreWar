using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public static class State
{
    static int saveErrors = 0;
    public const string Version = "44A";
    public static World World;
    public static Rand Rand = new Rand();
    public static NameGenerator NameGen;
    public static GameManager GameManager;
    public static AssimilateList AssimilateList;
    public static Dictionary<Traits,TaggedTrait> TieredTraitsList;
    public static List<String> TieredTraitsTagsList;
    public static List<RandomizeList> RandomizeLists;
    public static List<CustomTraitBoost> CustomTraitList;
    public static List<ConditionalTraitContainer> ConditionalTraitList;
    public static List<UnitTag> UnitTagList;
    public static Dictionary<Traits, List<int>> UnitTagAssociatedTraitDictionary;
    public static Dictionary<TaggedTrait,bool> UntaggedTraits;

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

    public static int RaceSlot;
    public static string RaceSaveDataName;

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
        }
        else
        {
            SaveDirectory = $"UserData{Path.DirectorySeparatorChar}Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = $"UserData{Path.DirectorySeparatorChar}";
            MapDirectory = $"UserData{Path.DirectorySeparatorChar}Maps{Path.DirectorySeparatorChar}";
            CustomTraitDirectory = $"UserData{Path.DirectorySeparatorChar}CustomTraits{Path.DirectorySeparatorChar}";
            ConditionalTraitDirectory = $"UserData{Path.DirectorySeparatorChar}ConditionalTraits{Path.DirectorySeparatorChar}";
            UnitTagDirectory = $"UserData{Path.DirectorySeparatorChar}UnitTags{Path.DirectorySeparatorChar}";
        }
        try
        {
            Directory.CreateDirectory(StorageDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(MapDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(SaveDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(CustomTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(ConditionalTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(UnitTagDirectory.TrimEnd(new char[] { '\\', '/' }));
        }
        catch
        {
            SaveDirectory = Application.persistentDataPath + $"Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar;
            MapDirectory = Application.persistentDataPath + $"Maps{Path.DirectorySeparatorChar}";
            CustomTraitDirectory = Application.persistentDataPath + $"CustomTraits{Path.DirectorySeparatorChar}";
            ConditionalTraitDirectory = Application.persistentDataPath + $"ConditionalTraits{Path.DirectorySeparatorChar}";
            UnitTagDirectory = Application.persistentDataPath + $"UnitTags{Path.DirectorySeparatorChar}";
            Directory.CreateDirectory(StorageDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(MapDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(SaveDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(CustomTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(ConditionalTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(UnitTagDirectory.TrimEnd(new char[] { '\\', '/' }));
        }


        try
        {
            if (File.Exists($"{StorageDirectory}males.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}males.txt", $"{StorageDirectory}males.txt");
            if (File.Exists($"{StorageDirectory}females.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}females.txt", $"{StorageDirectory}females.txt");
            if (File.Exists($"{StorageDirectory}monsters.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}monsters.txt", $"{StorageDirectory}monsters.txt");
            if (File.Exists($"{StorageDirectory}events.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}events.txt", $"{StorageDirectory}events.txt");
            if (File.Exists($"{StorageDirectory}armyNames.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}armyNames.txt", $"{StorageDirectory}armyNames.txt");
            if (File.Exists($"{StorageDirectory}femaleFeralLions.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleFeralLions.txt", $"{StorageDirectory}femaleFeralLions.txt");
            if (File.Exists($"{StorageDirectory}maleFeralLions.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleFeralLions.txt", $"{StorageDirectory}maleFeralLions.txt");
            if (File.Exists($"{StorageDirectory}customTraits.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}customTraits.txt", $"{StorageDirectory}customTraits.txt");
            if (File.Exists($"{StorageDirectory}femaleAabayx.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleAabayx.txt", $"{StorageDirectory}femaleAabayx.txt");
            if (File.Exists($"{StorageDirectory}maleAabayx.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleAabayx.txt", $"{StorageDirectory}maleAabayx.txt");
            if (File.Exists($"{StorageDirectory}Cake.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}Cake.txt", $"{StorageDirectory}Cake.txt");
            if (File.Exists($"{StorageDirectory}Collectors.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}Collectors.txt", $"{StorageDirectory}Collectors.txt");
            if (File.Exists($"{StorageDirectory}Compy.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}Compy.txt", $"{StorageDirectory}Compy.txt");
            if (File.Exists($"{StorageDirectory}CoralSlugs.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}CoralSlugs.txt", $"{StorageDirectory}CoralSlugs.txt");
            if (File.Exists($"{StorageDirectory}DarkSwallower.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}DarkSwallower.txt", $"{StorageDirectory}DarkSwallower.txt");
            if (File.Exists($"{StorageDirectory}Dragonfly.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}Dragonfly.txt", $"{StorageDirectory}Dragonfly.txt");
            if (File.Exists($"{StorageDirectory}Earthworms.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}Earthworms.txt", $"{StorageDirectory}Earthworms.txt");
            if (File.Exists($"{StorageDirectory}femaleAlligators.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleAlligators.txt", $"{StorageDirectory}femaleAlligators.txt");
            if (File.Exists($"{StorageDirectory}maleAlligators.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleAlligators.txt", $"{StorageDirectory}maleAlligators.txt");
            if (File.Exists($"{StorageDirectory}femaleAlraune.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleAlraune.txt", $"{StorageDirectory}femaleAlraune.txt");
            if (File.Exists($"{StorageDirectory}maleAlraune.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleAlraune.txt", $"{StorageDirectory}maleAlraune.txt");
            if (File.Exists($"{StorageDirectory}femaleAnts.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleAnts.txt", $"{StorageDirectory}femaleAnts.txt");
            if (File.Exists($"{StorageDirectory}maleAnts.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleAnts.txt", $"{StorageDirectory}maleAnts.txt");
            if (File.Exists($"{StorageDirectory}femaleAvians.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleAvians.txt", $"{StorageDirectory}femaleAvians.txt");
            if (File.Exists($"{StorageDirectory}maleAvians.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleAvians.txt", $"{StorageDirectory}maleAvians.txt");
            if (File.Exists($"{StorageDirectory}femaleBats.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleBats.txt", $"{StorageDirectory}femaleBats.txt");
            if (File.Exists($"{StorageDirectory}maleBats.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleBats.txt", $"{StorageDirectory}maleBats.txt");
            if (File.Exists($"{StorageDirectory}femaleBees.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleBees.txt", $"{StorageDirectory}femaleBees.txt");
            if (File.Exists($"{StorageDirectory}maleBees.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleBees.txt", $"{StorageDirectory}maleBees.txt");
            if (File.Exists($"{StorageDirectory}femaleBunnies.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleBunnies.txt", $"{StorageDirectory}femaleBunnies.txt");
            if (File.Exists($"{StorageDirectory}maleBunnies.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleBunnies.txt", $"{StorageDirectory}maleBunnies.txt");
            if (File.Exists($"{StorageDirectory}femaleCats.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleCats.txt", $"{StorageDirectory}femaleCats.txt");
            if (File.Exists($"{StorageDirectory}maleCats.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleCats.txt", $"{StorageDirectory}maleCats.txt");
            if (File.Exists($"{StorageDirectory}femaleCockatrice.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleCockatrice.txt", $"{StorageDirectory}femaleCockatrice.txt");
            if (File.Exists($"{StorageDirectory}maleCockatrice.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleCockatrice.txt", $"{StorageDirectory}maleCockatrice.txt");
            if (File.Exists($"{StorageDirectory}femaleCrux.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleCrux.txt", $"{StorageDirectory}femaleCrux.txt");
            if (File.Exists($"{StorageDirectory}maleCrux.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleCrux.txt", $"{StorageDirectory}maleCrux.txt");
            if (File.Exists($"{StorageDirectory}femaleCrypters.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleCrypters.txt", $"{StorageDirectory}femaleCrypters.txt");
            if (File.Exists($"{StorageDirectory}maleCrypters.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleCrypters.txt", $"{StorageDirectory}maleCrypters.txt");
            if (File.Exists($"{StorageDirectory}femaleDeer.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleDeer.txt", $"{StorageDirectory}femaleDeer.txt");
            if (File.Exists($"{StorageDirectory}maleDeer.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleDeer.txt", $"{StorageDirectory}maleDeer.txt");
            if (File.Exists($"{StorageDirectory}femaleDewSprites.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleDewSprites.txt", $"{StorageDirectory}femaleDewSprites.txt");
            if (File.Exists($"{StorageDirectory}femaleDogs.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleDogs.txt", $"{StorageDirectory}femaleDogs.txt");
            if (File.Exists($"{StorageDirectory}maleDogs.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleDogs.txt", $"{StorageDirectory}maleDogs.txt");
            if (File.Exists($"{StorageDirectory}femaleDragon.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleDragon.txt", $"{StorageDirectory}femaleDragon.txt");
            if (File.Exists($"{StorageDirectory}maleDragon.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleDragon.txt", $"{StorageDirectory}maleDragon.txt");
            if (File.Exists($"{StorageDirectory}femaleDratopyr.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleDratopyr.txt", $"{StorageDirectory}femaleDratopyr.txt");
            if (File.Exists($"{StorageDirectory}maleDratopyr.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleDratopyr.txt", $"{StorageDirectory}maleDratopyr.txt");
            if (File.Exists($"{StorageDirectory}femaleDriders.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleDriders.txt", $"{StorageDirectory}femaleDriders.txt");
            if (File.Exists($"{StorageDirectory}maleDriders.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleDriders.txt", $"{StorageDirectory}maleDriders.txt");
            if (File.Exists($"{StorageDirectory}femaleEasternDragon.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleEasternDragon.txt", $"{StorageDirectory}femaleEasternDragon.txt");
            if (File.Exists($"{StorageDirectory}maleEasternDragon.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleEasternDragon.txt", $"{StorageDirectory}maleEasternDragon.txt");
            if (File.Exists($"{StorageDirectory}femaleEquines.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleEquines.txt", $"{StorageDirectory}femaleEquines.txt");
            if (File.Exists($"{StorageDirectory}maleEquines.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleEquines.txt", $"{StorageDirectory}maleEquines.txt");
            if (File.Exists($"{StorageDirectory}femaleFairies.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleFairies.txt", $"{StorageDirectory}femaleFairies.txt");
            if (File.Exists($"{StorageDirectory}maleFairies.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleFairies.txt", $"{StorageDirectory}maleFairies.txt");
            if (File.Exists($"{StorageDirectory}femaleFeralBats.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleFeralBats.txt", $"{StorageDirectory}femaleFeralBats.txt");
            if (File.Exists($"{StorageDirectory}maleFeralBats.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleFeralBats.txt", $"{StorageDirectory}maleFeralBats.txt");
            if (File.Exists($"{StorageDirectory}femaleFeralFox.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleFeralFox.txt", $"{StorageDirectory}femaleFeralFox.txt");
            if (File.Exists($"{StorageDirectory}maleFeralFox.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleFeralFox.txt", $"{StorageDirectory}maleFeralFox.txt");
            if (File.Exists($"{StorageDirectory}femaleFeralHorses.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleFeralHorses.txt", $"{StorageDirectory}femaleFeralHorses.txt");
            if (File.Exists($"{StorageDirectory}maleFeralHorses.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleFeralHorses.txt", $"{StorageDirectory}maleFeralHorses.txt");
            if (File.Exists($"{StorageDirectory}femaleFeralLizards.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleFeralLizards.txt", $"{StorageDirectory}femaleFeralLizards.txt");
            if (File.Exists($"{StorageDirectory}maleFeralLizards.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleFeralLizards.txt", $"{StorageDirectory}maleFeralLizards.txt");
            if (File.Exists($"{StorageDirectory}femaleFoxes.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleFoxes.txt", $"{StorageDirectory}femaleFoxes.txt");
            if (File.Exists($"{StorageDirectory}maleFoxes.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleFoxes.txt", $"{StorageDirectory}maleFoxes.txt");
            if (File.Exists($"{StorageDirectory}femaleFrogs.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleFrogs.txt", $"{StorageDirectory}femaleFrogs.txt");
            if (File.Exists($"{StorageDirectory}maleFrogs.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleFrogs.txt", $"{StorageDirectory}maleFrogs.txt");
            if (File.Exists($"{StorageDirectory}femaleGazelle.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleGazelle.txt", $"{StorageDirectory}femaleGazelle.txt");
            if (File.Exists($"{StorageDirectory}maleGazelle.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleGazelle.txt", $"{StorageDirectory}maleGazelle.txt");
            if (File.Exists($"{StorageDirectory}femaleGoblins.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleGoblins.txt", $"{StorageDirectory}femaleGoblins.txt");
            if (File.Exists($"{StorageDirectory}maleGoblins.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleGoblins.txt", $"{StorageDirectory}maleGoblins.txt");
            if (File.Exists($"{StorageDirectory}femaleGryphons.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleGryphons.txt", $"{StorageDirectory}femaleGryphons.txt");
            if (File.Exists($"{StorageDirectory}maleGryphons.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleGryphons.txt", $"{StorageDirectory}maleGryphons.txt");
            if (File.Exists($"{StorageDirectory}femaleHamsters.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleHamsters.txt", $"{StorageDirectory}femaleHamsters.txt");
            if (File.Exists($"{StorageDirectory}maleHamsters.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleHamsters.txt", $"{StorageDirectory}maleHamsters.txt");
            if (File.Exists($"{StorageDirectory}femaleHarpies.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleHarpies.txt", $"{StorageDirectory}femaleHarpies.txt");
            if (File.Exists($"{StorageDirectory}maleHarpies.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleHarpies.txt", $"{StorageDirectory}maleHarpies.txt");
            if (File.Exists($"{StorageDirectory}femaleHippos.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleHippos.txt", $"{StorageDirectory}femaleHippos.txt");
            if (File.Exists($"{StorageDirectory}maleHippos.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleHippos.txt", $"{StorageDirectory}maleHippos.txt");
            if (File.Exists($"{StorageDirectory}femaleHumans.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleHumans.txt", $"{StorageDirectory}femaleHumans.txt");
            if (File.Exists($"{StorageDirectory}maleHumans.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleHumans.txt", $"{StorageDirectory}maleHumans.txt");
            if (File.Exists($"{StorageDirectory}femaleImps.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleImps.txt", $"{StorageDirectory}femaleImps.txt");
            if (File.Exists($"{StorageDirectory}maleImps.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleImps.txt", $"{StorageDirectory}maleImps.txt");
            if (File.Exists($"{StorageDirectory}femaleKangaroos.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleKangaroos.txt", $"{StorageDirectory}femaleKangaroos.txt");
            if (File.Exists($"{StorageDirectory}maleKangaroos.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleKangaroos.txt", $"{StorageDirectory}maleKangaroos.txt");
            if (File.Exists($"{StorageDirectory}femaleKobolds.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleKobolds.txt", $"{StorageDirectory}femaleKobolds.txt");
            if (File.Exists($"{StorageDirectory}maleKobolds.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleKobolds.txt", $"{StorageDirectory}maleKobolds.txt");
            if (File.Exists($"{StorageDirectory}femaleKomodos.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleKomodos.txt", $"{StorageDirectory}femaleKomodos.txt");
            if (File.Exists($"{StorageDirectory}maleKomodos.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleKomodos.txt", $"{StorageDirectory}maleKomodos.txt");
            if (File.Exists($"{StorageDirectory}femaleLamia.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleLamia.txt", $"{StorageDirectory}femaleLamia.txt");
            if (File.Exists($"{StorageDirectory}maleLamia.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleLamia.txt", $"{StorageDirectory}maleLamia.txt");
            if (File.Exists($"{StorageDirectory}femaleLizards.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleLizards.txt", $"{StorageDirectory}femaleLizards.txt");
            if (File.Exists($"{StorageDirectory}maleLizards.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleLizards.txt", $"{StorageDirectory}maleLizards.txt");
            if (File.Exists($"{StorageDirectory}femaleMantis.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleMantis.txt", $"{StorageDirectory}femaleMantis.txt");
            if (File.Exists($"{StorageDirectory}maleMantis.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleMantis.txt", $"{StorageDirectory}maleMantis.txt");
            if (File.Exists($"{StorageDirectory}femaleMerfolk.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleMerfolk.txt", $"{StorageDirectory}femaleMerfolk.txt");
            if (File.Exists($"{StorageDirectory}maleMerfolk.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleMerfolk.txt", $"{StorageDirectory}maleMerfolk.txt");
            if (File.Exists($"{StorageDirectory}femaleMonitors.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleMonitors.txt", $"{StorageDirectory}femaleMonitors.txt");
            if (File.Exists($"{StorageDirectory}maleMonitors.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleMonitors.txt", $"{StorageDirectory}maleMonitors.txt");
            if (File.Exists($"{StorageDirectory}femalePanthers.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femalePanthers.txt", $"{StorageDirectory}femalePanthers.txt");
            if (File.Exists($"{StorageDirectory}malePanthers.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}malePanthers.txt", $"{StorageDirectory}malePanthers.txt");
            if (File.Exists($"{StorageDirectory}femalePuca.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femalePuca.txt", $"{StorageDirectory}femalePuca.txt");
            if (File.Exists($"{StorageDirectory}malePuca.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}malePuca.txt", $"{StorageDirectory}malePuca.txt");
            if (File.Exists($"{StorageDirectory}femaleScylla.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleScylla.txt", $"{StorageDirectory}femaleScylla.txt");
            if (File.Exists($"{StorageDirectory}maleScylla.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleScylla.txt", $"{StorageDirectory}maleScylla.txt");
            if (File.Exists($"{StorageDirectory}femaleSergal.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleSergal.txt", $"{StorageDirectory}femaleSergal.txt");
            if (File.Exists($"{StorageDirectory}maleSergal.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleSergal.txt", $"{StorageDirectory}maleSergal.txt");
            if (File.Exists($"{StorageDirectory}femaleSharks.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleSharks.txt", $"{StorageDirectory}femaleSharks.txt");
            if (File.Exists($"{StorageDirectory}maleSharks.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleSharks.txt", $"{StorageDirectory}maleSharks.txt");
            if (File.Exists($"{StorageDirectory}femaleSlimes.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleSlimes.txt", $"{StorageDirectory}femaleSlimes.txt");
            if (File.Exists($"{StorageDirectory}maleSlimes.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleSlimes.txt", $"{StorageDirectory}maleSlimes.txt");
            if (File.Exists($"{StorageDirectory}femaleSuccubi.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleSuccubi.txt", $"{StorageDirectory}femaleSuccubi.txt");
            if (File.Exists($"{StorageDirectory}maleSuccubi.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleSuccubi.txt", $"{StorageDirectory}maleSuccubi.txt");
            if (File.Exists($"{StorageDirectory}femaleTaurus.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleTaurus.txt", $"{StorageDirectory}femaleTaurus.txt");
            if (File.Exists($"{StorageDirectory}maleTaurus.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleTaurus.txt", $"{StorageDirectory}maleTaurus.txt");
            if (File.Exists($"{StorageDirectory}femaleTerrorbird.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleTerrorbird.txt", $"{StorageDirectory}femaleTerrorbird.txt");
            if (File.Exists($"{StorageDirectory}maleTerrorbird.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleTerrorbird.txt", $"{StorageDirectory}maleTerrorbird.txt");
            if (File.Exists($"{StorageDirectory}femaleTigers.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleTigers.txt", $"{StorageDirectory}femaleTigers.txt");
            if (File.Exists($"{StorageDirectory}maleTigers.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleTigers.txt", $"{StorageDirectory}maleTigers.txt");
            if (File.Exists($"{StorageDirectory}femaleVargul.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleVargul.txt", $"{StorageDirectory}femaleVargul.txt");
            if (File.Exists($"{StorageDirectory}maleVargul.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleVargul.txt", $"{StorageDirectory}maleVargul.txt");
            if (File.Exists($"{StorageDirectory}femaleVipers.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleVipers.txt", $"{StorageDirectory}femaleVipers.txt");
            if (File.Exists($"{StorageDirectory}maleVipers.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleVipers.txt", $"{StorageDirectory}maleVipers.txt");
            if (File.Exists($"{StorageDirectory}femaleWolves.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleWolves.txt", $"{StorageDirectory}femaleWolves.txt");
            if (File.Exists($"{StorageDirectory}maleWolves.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleWolves.txt", $"{StorageDirectory}maleWolves.txt");
            if (File.Exists($"{StorageDirectory}femaleWyvern.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleWyvern.txt", $"{StorageDirectory}femaleWyvern.txt");
            if (File.Exists($"{StorageDirectory}maleWyvern.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleWyvern.txt", $"{StorageDirectory}maleWyvern.txt");
            if (File.Exists($"{StorageDirectory}femaleYouko.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleYouko.txt", $"{StorageDirectory}femaleYouko.txt");
            if (File.Exists($"{StorageDirectory}maleYouko.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleYouko.txt", $"{StorageDirectory}maleYouko.txt");
            if (File.Exists($"{StorageDirectory}FeralAnts.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}FeralAnts.txt", $"{StorageDirectory}FeralAnts.txt");
            if (File.Exists($"{StorageDirectory}FeralFrogs.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}FeralFrogs.txt", $"{StorageDirectory}FeralFrogs.txt");
            if (File.Exists($"{StorageDirectory}FeralSharks.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}FeralSharks.txt", $"{StorageDirectory}FeralSharks.txt");
            if (File.Exists($"{StorageDirectory}FeralWolves.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}FeralWolves.txt", $"{StorageDirectory}FeralWolves.txt");
            if (File.Exists($"{StorageDirectory}Harvesters.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}Harvesters.txt", $"{StorageDirectory}Harvesters.txt");
            if (File.Exists($"{StorageDirectory}Raptor.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}Raptor.txt", $"{StorageDirectory}Raptor.txt");
            if (File.Exists($"{StorageDirectory}RockSlugs.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}RockSlugs.txt", $"{StorageDirectory}RockSlugs.txt");
            if (File.Exists($"{StorageDirectory}Salamanders.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}Salamanders.txt", $"{StorageDirectory}Salamanders.txt");
            if (File.Exists($"{StorageDirectory}Schiwardez.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}Schiwardez.txt", $"{StorageDirectory}Schiwardez.txt");
            if (File.Exists($"{StorageDirectory}Serpents.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}Serpents.txt", $"{StorageDirectory}Serpents.txt");
            if (File.Exists($"{StorageDirectory}SpitterSlugs.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}SpitterSlugs.txt", $"{StorageDirectory}SpitterSlugs.txt");
            if (File.Exists($"{StorageDirectory}SpringSlugs.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}SpringSlugs.txt", $"{StorageDirectory}SpringSlugs.txt");
            if (File.Exists($"{StorageDirectory}Voilin.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}Voilin.txt", $"{StorageDirectory}Voilin.txt");
            if (File.Exists($"{StorageDirectory}WarriorAnts.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}WarriorAnts.txt", $"{StorageDirectory}WarriorAnts.txt");
            if (File.Exists($"{StorageDirectory}Whisp.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}Whisp.txt", $"{StorageDirectory}Whisp.txt");
            if (File.Exists($"{StorageDirectory}femaleBoomBunnies.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleBoomBunnies.txt", $"{StorageDirectory}femaleBoomBunnies.txt");
            if (File.Exists($"{StorageDirectory}maleBoomBunnies.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleBoomBunnies.txt", $"{StorageDirectory}maleBoomBunnies.txt");
            if (File.Exists($"{StorageDirectory}WyvernMatron.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}WyvernMatron.txt", $"{StorageDirectory}WyvernMatron.txt");
            if (File.Exists($"{StorageDirectory}maleFeralOrcas.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}maleFeralOrcas.txt", $"{StorageDirectory}maleFeralOrcas.txt");
            if (File.Exists($"{StorageDirectory}femaleFeralOrcas.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}femaleFeralOrcas.txt", $"{StorageDirectory}femaleFeralOrcas.txt");
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
                    line = new string(line
                       .Where(c => !Char.IsWhiteSpace(c)).ToArray());
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
                    } else if (strings.Length == 6)
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


            VillageBuildingList.SetBuildings(World.crazyBuildings);
            if (version < 12)
            {
                World = null;
                GameManager.CreateMessageBox("This save file is from before version 12.  I took the liberty of doing a clean sweep when I added the new garrisons to improve the code quality. Sorry.  You can still load .map files from before version 12 though.");
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
                /*         if (World.GetEmpireOfSide(702) == null)
                        {
                            World.MainEmpires.Add(new Empire(new Empire.ConstructionArgs(702, Color.red, new Color(.6f, 0, 0), 5, StrategyAIType.Basic, TacticalAIType.Full, 702, 16, 16)));
                            World.RefreshEmpires();
                        }
                        else
                        {
                            World.GetEmpireOfSide(702).Name = "Outcasts";
                            if (World.EmpireOrder.Where(s => s.Side == 702).Any() == false)
                                World.EmpireOrder.Add(World.GetEmpireOfSide(702));
                        } */
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
                        foreach(Army army in emp.Armies)
                        {
                            army.impassables = new List<StrategicTileType>()
    { StrategicTileType.mountain, StrategicTileType.snowMountain, StrategicTileType.water, StrategicTileType.lava, StrategicTileType.ocean, StrategicTileType.brokenCliffs};
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


