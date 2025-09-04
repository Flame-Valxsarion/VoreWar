using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

static class LogUtilities
{
    static int rand;
    internal static string GetRandomStringFrom(List<string> messages)
    {
        if (messages.Count == 0)
            return "";
        rand = Random.Range(0, messages.Count);
        return messages[rand];
    }

    internal static string GetRandomStringFrom(params string[] messages)
    {
        rand = Random.Range(0, messages.Length);
        return messages[rand];
    }

    internal static string GetGenderString(Unit unit, string female, string male, string mixed)
    {
        if (unit.HasBreasts && (unit.HasDick != unit.HasVagina))
            return female;
        else if (!unit.HasBreasts && (unit.HasDick != unit.HasVagina))
            return male;
        return mixed;
    }

    internal static string Capitalize(String str)
    {
        if (str == null)
            return null;
        return char.ToUpper(str[0]) + str.Substring(1);

    }

    /// <summary>
    /// Returns given unit's nominative pronoun.<br></br>(e.g. he/she/they)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GPPHe(Unit unit) => unit.GetPronoun(0);

    /// <summary>
    /// Returns given unit's nominative pronoun appended with present-tense auxillary.<br></br>(e.g. he is/she is/they are)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GPPHeIs(Unit unit) => unit.GetPronoun(0) + (unit.GetPronoun(5) == "plural" ? " are" : " is");

    /// <summary>
    /// Returns given unit's nominative pronoun appended with present-tense auxillary as a contraction.<br></br>(e.g. he's/she's/they're)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GPPHeIsAbbr(Unit unit) => unit.GetPronoun(0) + (unit.GetPronoun(5) == "plural" ? "'re" : "'s");

    /// <summary>
    /// Returns given unit's nominative pronoun appended with past-tense auxillary.<br></br>(e.g. he was/she was/they were)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GPPHeWas(Unit unit) => unit.GetPronoun(0) + (unit.GetPronoun(5) == "plural" ? " were" : " was");

    /// <summary>
    /// Returns given unit's accusative pronoun.<br></br>(e.g. him/her/them)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GPPHim(Unit unit) => unit.GetPronoun(1);

    /// <summary>
    /// Returns given unit's pronomial possessive pronoun.<br></br>(e.g. ...<u>their</u> belly...)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GPPHis(Unit unit) => unit.GetPronoun(2);

    /// <summary>
    /// Returns given unit's reflexive pronoun.<br></br>(e.g. ...can't help <u>themself</u>...)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GPPHimself(Unit unit) => unit.GetPronoun(4);

    /// <summary>
    /// Returns "s" if given unit is referred to with singular grammar.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string SIfSingular(Unit unit) => (unit.GetPronoun(5) == "plural" ? "" : "s");

    /// <summary>
    /// Returns "es" if given unit is referred to with singular grammar.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string EsIfSingular(Unit unit) => (unit.GetPronoun(5) == "plural" ? "" : "es");

    /// <summary>
    /// Returns "y" or "ies" based on plurality of given unit.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string IesIfSingular(Unit unit) => (unit.GetPronoun(5) == "plural" ? "y" : "ies");

    /// <summary>
    /// Returns "has" or "have" based on plurality of given unit.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string HasHave(Unit unit) => (unit.GetPronoun(5) == "plural" ? "have" : "has");

    /// <summary>
    /// Returns "is" or "are" based on plurality of given unit.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string IsAre(Unit unit) => (unit.GetPronoun(5) == "plural" ? "are" : "is");

    /// <summary>
    /// Returns "was" or "were" based on plurality of given unit.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string WasWere(Unit unit) => (unit.GetPronoun(5) == "plural" ? "were" : "was");

    internal static string PluralForPart(PreyLocation location)
    {
        switch (location)
        {
            case PreyLocation.breasts:
            case PreyLocation.balls:
                return "";
            case PreyLocation.stomach:
            case PreyLocation.stomach2:
            case PreyLocation.womb:
            case PreyLocation.tail:
            case PreyLocation.anal:
                return "s";
        }
        return "";
    }

    internal static string BoyGirl(Unit unit)
    {
        if (unit.DefaultBreastSize >= 0 && unit.DickSize < 0)
            return "girl";
        if (unit.DefaultBreastSize < 0 && unit.DickSize >= 0)
            return "boy";
        return unit.Race.ToString();
    }

    //internal static string FriendlyWarrior(Unit unit)
    //{
    //    var friendlies = TacticalUtilities.Units.Where(s => s.Unit.Side == unit.Side && s.Unit != unit && s.Visible && s.Targetable && s.Unit.IsDead == false).ToArray();
    //    if (friendlies.Length == 0)
    //        return "NULL";
    //    return friendlies[State.Rand.Next(friendlies.Length)].Unit.Name;  
    //}
    internal static Unit RandomAlliedWarrior(Unit unit)//Implementing modified code, but kept original because I'm not sure if someone else was planning on using it.
    {
        var friendlies = TacticalUtilities.Units.Where(s => s.Unit.Side == unit.Side && s.Unit != unit && s.Visible && s.Targetable && s.Unit.IsDead == false).ToArray();
        if (friendlies.Length == 0)
            return null;
        return friendlies[State.Rand.Next(friendlies.Length)].Unit;
    }

    internal static Unit CompetitionWarrior(Unit unit)
    {
        var friendlies = TacticalUtilities.Units.Where(s => s.Unit.Side == unit.Side && s.Unit != unit && s.Visible && s.Targetable && s.Unit.IsDead == false && RomanticTarget(unit, s.Unit) == false).ToArray();
        if (friendlies.Length == 0)
            return null;
        return friendlies[State.Rand.Next(friendlies.Length)].Unit;
    }

    internal static Unit PotentialNextPrey(Unit unit)
    {
        var preyList = TacticalUtilities.Units.Where(s => s.Unit.Side != unit.Side && s.Visible && s.Targetable && !s.Unit.IsDead);
        var preyChanceMap = new Dictionary<Actor_Unit, float>();
        foreach (Actor_Unit prey in preyList)
        {
            float chance = prey.GetDevourChance(TacticalUtilities.Units.Where(actor => actor.Unit == unit)?.FirstOrDefault(), true);
            preyChanceMap.Add(prey, chance);
        }
        var primePrey = preyChanceMap.OrderBy(x => x.Value).LastOrDefault();
        if (!primePrey.Equals(default(KeyValuePair<Actor_Unit, float>)))
        {
            return primePrey.Key.Unit;
        }
        var you = new Unit(Race.Humans);
        you.DefaultBreastSize = -1;
        you.DickSize = -1;
        you.Name = "You, the player";
        return you;
    }

    internal static Unit AttractedWarrior(Unit unit)
    {
        if (unit.AttractedTo != null)
        {
            var actor = TacticalUtilities.Units.Where(s => s.Unit == unit.AttractedTo && s.Unit.Side == unit.Side && s.Unit != unit && RomanticTarget(unit, s.Unit)).FirstOrDefault(); //If this fails, reassign
            if (actor != null)
            {
                if (actor.Visible && actor.Targetable && actor.Unit.IsDead == false)
                {
                    return actor.Unit;
                }
                return null; //Avoid picking a new target during the same battle
            }
        }

        var friendlies = TacticalUtilities.Units.Where(s => s.Unit.Side == unit.Side && s.Unit != unit && s.Visible && s.Targetable && s.Unit.IsDead == false && RomanticTarget(unit, s.Unit)).ToArray();
        if (friendlies.Length == 0)
            return null;
        return friendlies[State.Rand.Next(friendlies.Length)].Unit;
    }

    internal static bool ActorHumanoid(Unit s)
    {
        return s.Race < Race.Vagrants || s.Race >= Race.Selicia;
    }

    internal static bool RomanticTarget(Unit unit, Unit target)
    {
        if (unit.GetGender() == Gender.Hermaphrodite || target.GetGender() == Gender.Hermaphrodite)
            return true;
        if (unit.GetGender() == Gender.Female)
        {
            switch (Config.FemalesLike)
            {
                case Orientation.Straight:
                    return target.GetGender() == Gender.Male;
                case Orientation.Gay:
                    return target.GetGender() == Gender.Female;
                case Orientation.Bi:
                    return true;
            }
        }
        if (unit.GetGender() == Gender.Male)
        {
            switch (Config.MalesLike)
            {
                case Orientation.Straight:
                    return target.GetGender() == Gender.Female;
                case Orientation.Gay:
                    return target.GetGender() == Gender.Male;
                case Orientation.Bi:
                    return true;
            }
        }
        //Should never make it here
        return false;
    }



    /// <summary>
    /// Determines whether the string supplied should have either a or an before it and returns the original string with the right "thing" in front of it.
    /// Done this way since otherwise the string might get randomized again and wouldn't match the returned bit.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    internal static string GetAorAN(string str)
    {
        if (str.StartsWith("a", true, null) || str.StartsWith("e", true, null) || str.StartsWith("i", true, null) || str.StartsWith("o", true, null) || str.StartsWith("u", true, null) || str.StartsWith("y", true, null))
        {
            return "an " + str;
        }
        if (str.StartsWith("b", true, null) || str.StartsWith("c", true, null) || str.StartsWith("d", true, null) || str.StartsWith("f", true, null) || str.StartsWith("g", true, null) || str.StartsWith("h", true, null) ||
            str.StartsWith("j", true, null) || str.StartsWith("k", true, null) || str.StartsWith("l", true, null) || str.StartsWith("m", true, null) || str.StartsWith("n", true, null) || str.StartsWith("p", true, null) ||
            str.StartsWith("q", true, null) || str.StartsWith("r", true, null) || str.StartsWith("s", true, null) || str.StartsWith("t", true, null) || str.StartsWith("v", true, null) || str.StartsWith("w", true, null) || str.StartsWith("x", true, null) || str.StartsWith("z", true, null))
        {
            return "a " + str;
        }
        if (str.StartsWith("'", true, null))
        {
            if (str.StartsWith("'a", true, null) || str.StartsWith("'e", true, null) || str.StartsWith("'i", true, null) || str.StartsWith("'o", true, null) || str.StartsWith("'u", true, null) || str.StartsWith("'y", true, null))
            {
                return "an " + str;
            }
            if (str.StartsWith("'b", true, null) || str.StartsWith("'c", true, null) || str.StartsWith("'d", true, null) || str.StartsWith("'f", true, null) || str.StartsWith("'g", true, null) || str.StartsWith("'h", true, null) ||
                str.StartsWith("'j", true, null) || str.StartsWith("'k", true, null) || str.StartsWith("'l", true, null) || str.StartsWith("'m", true, null) || str.StartsWith("'n", true, null) || str.StartsWith("'p", true, null) ||
                str.StartsWith("'q", true, null) || str.StartsWith("'r", true, null) || str.StartsWith("'s", true, null) || str.StartsWith("'t", true, null) || str.StartsWith("'v", true, null) || str.StartsWith("'w", true, null) || str.StartsWith("'x", true, null) || str.StartsWith("'z", true, null))
            {
                return "a " + str;
            }
        }
        return str;
    }

    internal static string ApostrophizeWithOrWithoutS(string str)
    {
        return str + (str.EndsWith("s") ? "'" : "'s");
    }

    /// <summary>
    /// <para>Gets a descriptive string that fits sentences like "Edmond stuffs Sidney down his maw, enjoying the * morsels squirms on her way down."</para>
    /// <para>Generally meant for the prey/loser/weaker unit. Has mostly demeaning, belittling, weakness indicating or fear portraying terms.</para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetPreyDesc(Unit unit)
    {
        switch (unit.Race)
        {
            case Race.Cats:
                return GetRandomStringFrom("whiskered", "hissing", "bristle tailed");
            case Race.Dogs:
                return GetRandomStringFrom("yelping", "curly tailed", "whining", "domesticated");
            case Race.Youko:
            case Race.Foxes:
                return GetRandomStringFrom("fluffy tailed", "squirming", "whimpering");
            case Race.Wolves:
                return GetRandomStringFrom("wild", "growling", "wet furred");
            case Race.Bunnies:
                return GetRandomStringFrom("long eared", "bushy tailed", "leaf biting");
            case Race.Lizards:
                return GetRandomStringFrom("hairless", "cold-blooded", "wiry");
            case Race.Slimes:
                return GetRandomStringFrom("amorphous", "sludgy", "juicy");
            case Race.Scylla:
                return GetRandomStringFrom("loose limbed", "aquatic", "ten-limbed");
            case Race.Harpies:
                return GetRandomStringFrom("feathered", "keening", "grounded");
            case Race.Imps:
                return GetRandomStringFrom("infernal", "diminutive", "sized");
            case Race.Humans:
                return GetRandomStringFrom("bare skinned", "soft", "shouting");
            case Race.Crypters:
                return GetRandomStringFrom("mechanical", "artifical", "whirring");
            case Race.Lamia:
                return GetRandomStringFrom("scaly", "noodly", "double-tasty");
            case Race.Kangaroos:
                return GetRandomStringFrom("bottom heavy", unit.DefaultBreastSize > 0 ? "pouched" : "pouchless", "long legged");
            case Race.Taurus:
                return GetRandomStringFrom("mooing", "bulky", "hooved");
            case Race.Crux:
                return GetRandomStringFrom("crazy", "curly eared", "complaining"); // ---------------------------------------------------------
            case Race.Succubi:
                return GetRandomStringFrom("devilishly tasty", "beguiling", "batty");
            case Race.Tigers:
                return GetRandomStringFrom("striped", "roaring", "mewling");
            case Race.Goblins:
                return GetRandomStringFrom("diminutive", "cursing", "short");
            case Race.Hamsters:
                return GetRandomStringFrom("stout", "shortstack", "chubby");
            case Race.Alligators:
                return GetRandomStringFrom("crocodilian", "lumbering", "swampy");
            case Race.Vagrants:
                return GetRandomStringFrom("tentacled", "rubbery", "alien");
            case Race.Serpents:
                return GetRandomStringFrom("limbless", "noodly", "slithery");
            case Race.Wyvern:
                return GetRandomStringFrom("winged", "horned", "wiry");
            case Race.WyvernMatron:
                return GetRandomStringFrom("winged", "horned", "wiry");
            case Race.Compy:
                return GetRandomStringFrom("tiny", "chirping", "overambitious");
            case Race.FeralSharks:
                return GetRandomStringFrom("finned", "torpedo shaped", "chompy");
            case Race.FeralWolves:
                return GetRandomStringFrom("shaggy", "gamey", "growling");
            case Race.Selicia:
                return GetRandomStringFrom("mighty tasty", "smooth scaled", "huge", "flexible", "formerly mighty", "surprisingly edible");
            case Race.EasternDragon:
                return GetRandomStringFrom("tasty noodle", "noodle derg", "spaghetti-like", "easily-slurpable"); ////new, many thanks to Flame_Valxsarion
            case Race.Dragon:
                return GetRandomStringFrom("formerly apex predator", "delicious dragon", "ex-predator"); ////new 
            case Race.FeralLions:
                return GetRandomStringFrom("roaring", "once-vicious", "formerly-fearsome");
            case Race.Aabayx:
                return GetRandomStringFrom("strange-headed", "humbled viroid", "awkward-shaped");
            case Race.Mice:
                return GetRandomStringFrom("squeaking", "timid", "cheese-nibbling","skittish");
            case Race.Avians:
                return GetRandomStringFrom("winged", "feathered", "squawking", "chirping");
            case Race.MainlandElves:
                return GetRandomStringFrom("bare skinned", "pointy-eared", "knife-eared");
            case Race.Tatltuae:
                return GetRandomStringFrom("black feathered", "rosemary flavored", "purple eyed", "slightly cowardly", "complaining", "hollow boned");
            default:
                return "tasty";
        }
    }

    /// <summary>
    ///<para>Gets a descriptive string that fits sentences like "Edmond stuffs Sidney down his maw, the prey filling his * body nicely."</para>
    ///<para>Generally meant for the predator/winner/stronger unit. Strength describing, contentment/pleasure indicating, etc. terms.</para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetPredDesc(Unit unit)
    {
        switch (unit.Race)
        {
            case Race.Cats:
                return GetRandomStringFrom("purring", "sharp-toothed", "whiskered");
            case Race.Dogs:
                return GetRandomStringFrom("wagging", "panting");
            case Race.Youko:
            case Race.Foxes:
                return GetRandomStringFrom("cunning", "grinning", "sly");
            case Race.Wolves:
                return GetRandomStringFrom("spirited", "panting", "long furred");
            case Race.Bunnies:
                return GetRandomStringFrom("sharp eared", "strong footed", "chisel-toothed");
            case Race.Lizards:
                return GetRandomStringFrom("thick-scaled", "cold-blooded", "tough");
            case Race.Slimes:
                return GetRandomStringFrom("amorphous", "flowing", "hard-cored");
            case Race.Scylla:
                return GetRandomStringFrom("tentacled", "aquatic", "ten-limbed");
            case Race.Harpies:
                return GetRandomStringFrom("winged", "screeching", "taloned");
            case Race.Imps:
                return GetRandomStringFrom("infernal", "deceptive", "devious");
            case Race.Humans:
                return GetRandomStringFrom("adaptive", "clever", "resourceful");
            case Race.Crypters:
                return GetRandomStringFrom("mechanical", "artifical", "rumbling");
            case Race.Lamia:
                return GetRandomStringFrom("scaly", "long bodied", "sizeable");
            case Race.Kangaroos:
                return GetRandomStringFrom("thick tailed", unit.DefaultBreastSize > 0 ? "pouched" : "long legged", "black clawed");
            case Race.Taurus:
                return GetRandomStringFrom("multi-stomached", "heavy", "strong legged");
            case Race.Crux:
                return GetRandomStringFrom("curly eared", "crazed", "eager"); // ---------------------------------------------------------------------------------
            case Race.Succubi:
                return GetRandomStringFrom("demonic", "beguiling", "bat-winged");
            case Race.Tigers:
                return GetRandomStringFrom("striped", "roaring", "sharp toothed");
            case Race.Goblins:
                return GetRandomStringFrom("stronger than looks", "knee kicking", "smart");
            case Race.Hamsters:
                return GetRandomStringFrom("knee breaking", "deceptively strong", "hammer-loving");
            case Race.Alligators:
                return GetRandomStringFrom("armoured", "large jawed", "swampy");
            case Race.Vagrants:
                return GetRandomStringFrom("alien", "stretchy", "translucent");
            case Race.Serpents:
                return GetRandomStringFrom("scaly", "long bodied", "slithering");
            case Race.Wyvern:
                return GetRandomStringFrom("mighty", "spined", "great-winged");
            case Race.WyvernMatron:
                return GetRandomStringFrom("mighty", "spined", "great-winged");
            case Race.Compy:
                return GetRandomStringFrom("energetic", "tanuki shaming", "ambitious");
            case Race.FeralSharks:
                return GetRandomStringFrom("large jawed", "rough scaled", "sharp finned");
            case Race.FeralWolves:
                return GetRandomStringFrom("long furred", "spirited", "panting");
            case Race.Selicia:
                return GetRandomStringFrom("wide mawed", "smooth scaled", "stretchy", "huge", "impressive", "all-too-eager", "mighty");
            case Race.Dragon:
                return GetRandomStringFrom("apex predator", "hungry dragon", "voracious dragon");
            case Race.FeralLions:
                return GetRandomStringFrom("indulgent", "greedily snarling", "voracious", "capacious", "insatiable", "dominant", "pleased"); ////new 
            case Race.Aabayx:
                return GetRandomStringFrom("zealous", "superior", "dice-like", "math-obsessed"); 
            case Race.Avians:
                return GetRandomStringFrom("winged", "feathered", "swift", "taloned", "hawklike");
            case Race.MainlandElves:
                return GetRandomStringFrom("humble", "cunning", "resourceful");
            case Race.Tatltuae:
                return GetRandomStringFrom("black feathered", "chaotic", "purple eyed", "ominous", "unnerving", "omen-bringing");
            default:
                return "strong";
        }
    }

    /// <summary>
    /// <para>Gets a descriptive string that fits situations like "Jeanne graps Timothy's head, pushing the *'s face in her slit and soon forcing rest of him after it."</para>
    /// <para>This is either the species name, a name of the genus the species belongs to or something similar. Can also be a synonym of the species name.</para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetRaceDescSingl(Unit unit)
    {
        switch (unit.Race)
        {
            case Race.Cats:
                return GetRandomStringFrom("cat", GetGenderString(unit, "queen", "tom", "cat"), "feline");
            case Race.Dogs:
                return GetRandomStringFrom("dog", GetGenderString(unit, "bitch", "dog", "dog"), "canine");
            case Race.Youko:
            case Race.Foxes:
                return GetRandomStringFrom("fox", GetGenderString(unit, "vixen", "tod", "fox"), "vulpine", "canid");
            case Race.Wolves:
                return GetRandomStringFrom("feral", GetGenderString(unit, "wolfess", "wolf", "wolf"), "canine"); ////I changed "wolfen" to "wolfess" 
            case Race.Bunnies:
                return GetRandomStringFrom("bunny", GetGenderString(unit, "doe", "buck", "lagomorph"), "rabbit");
            case Race.Deer:
                return GetRandomStringFrom(GetGenderString(unit, GetRandomStringFrom("doe", "roe"), GetRandomStringFrom("buck", "stag", "hart"), "cervid"), "faun", "deer");
            case Race.Lizards:
                return GetRandomStringFrom("lizard", "reptile", "reptilian");
            case Race.Slimes:
                return GetRandomStringFrom("slime", "ooze", "jelly");
            case Race.Scylla:
                return GetRandomStringFrom("scylla", "octopod", "aquanoid");
            case Race.Harpies:
                return GetRandomStringFrom("harpy", "raptor", "harpyia");
            case Race.Imps:
                return GetRandomStringFrom("imp", "infernal being", "small demon"); ////added "small demon"
            case Race.Humans:
                return GetRandomStringFrom("human", GetGenderString(unit, "woman", "man", "human"), "humanoid");
            case Race.Crypters:
                return GetRandomStringFrom("crypter", "machinoid", "synthetic", "robotic", "metallic", "futuristic", "fabricated");////added "synthetic", "robotic", "metallic", "futuristic", "fabricated" thanks to Flame_Valxsarion
            case Race.Lamia:
                return GetRandomStringFrom("lamia", "serpent", "half-snake");
            case Race.Kangaroos:
                return GetRandomStringFrom("kangaroo", unit.HasBreasts ? "flyer" : "boomer", "'roo", "marsupial");
            case Race.Taurus:
                return GetRandomStringFrom("bovine", GetGenderString(unit, "cow", "bull", "taurus"), "taurus");
            case Race.Crux:
                return GetRandomStringFrom("crux", "lab-critter", "gene-engineered creature"); // --------------------------------------------------------------------------
            case Race.Succubi:
                return GetRandomStringFrom("succubus", "demon", "hellish being");
            case Race.Tigers:
                return GetRandomStringFrom("feline", GetGenderString(unit, "tigress", "tiger", "tiger"), "large feline");
            case Race.Goblins:
                return GetRandomStringFrom("goblin", "goblinoid", "humanoid");
            case Race.Alligators:
                return GetRandomStringFrom("'gator", "alligator", "crocodilian", "reptile");
            case Race.Puca:
                return GetRandomStringFrom("puca", "bunny", "lagomorph", "digger");
            case Race.Hamsters:
                return GetRandomStringFrom("hamster", GetGenderString(unit, "sow", "boar", "hamster"), "rodent");
            case Race.RwuMercenaries:
                return GetRandomStringFrom("mercenary", "trooper", "merc");
            case Race.Xelhilde:
                return GetRandomStringFrom("canine knight", "doberman", "bitch");
            case Race.Vagrants:
                return GetRandomStringFrom("vagrant", "jellyfish", "medusa");
            case Race.Serpents:
                return GetRandomStringFrom("serpent", "snake", "slitherer");
            case Race.Wyvern:
                return GetRandomStringFrom("wyvern", "lesser draconic being", "drake");
            case Race.WyvernMatron:
                return GetRandomStringFrom("wyvern", "lesser draconic being", "drake");
            case Race.Compy:
                return GetRandomStringFrom("compy", "compsognathus", "dinosaur", "tiny dino");
            case Race.FeralSharks:
                return GetRandomStringFrom("skyshark", "shark", "air shark", "flying shark");
            case Race.FeralWolves:
                return GetRandomStringFrom("feral", GetGenderString(unit, "wolfess", "wolf", "wolf"), "canine"); ////I changed "wolfen" to "wolfess"
            case Race.Cake:
                return GetRandomStringFrom("cake", "baked good", "ghostly confectionary", "delicious dessert");
            case Race.Ki:
                return GetRandomStringFrom("small creature", "furry critter");
            case Race.Vision:
                return GetRandomStringFrom("alien", "dinosaur");
            case Race.Harvesters:
                return GetRandomStringFrom("alien", "harvester");
            case Race.Collectors:
                return GetRandomStringFrom("alien", "quadpod");
            case Race.Selicia:
                return GetRandomStringFrom("dragon", "salamander dragon", "derg");
            case Race.Equines:
                return GetRandomStringFrom("equine", GetGenderString(unit, "mare", "stallion", "horse"), "bronco"); ////new
            case Race.Sergal:
                return GetRandomStringFrom("furred", "sergal", "Eltussian"); ////new, many thanks to Flame_Valxsarion
            case Race.Dragon:
                return GetRandomStringFrom("dragon", GetGenderString(unit, "dragoness", "drakon", "dragon"), "draconian"); ////new 
            case Race.EasternDragon:
                return GetRandomStringFrom("oriental dragon", GetGenderString(unit, "eastern dragoness", "eastern dragon", "eastern dragon"), "serpentine dragon");  ////new    
            case Race.Zera:
                return GetRandomStringFrom("nargacuga", "fluffy wyvern", "big kitty"); ////new, many thanks to Selicia for the last two 
            case Race.Hippos:
                return GetRandomStringFrom("hippo", "hippopotamus", "pachyderm"); ////new 
            case Race.Komodos:
                return GetRandomStringFrom("komodo", "komodo dragon", "komodo lizard"); ////new  
            case Race.Cockatrice:
                return GetRandomStringFrom("cockatrice", GetGenderString(unit, "scary hen", "monster cock", "danger chicken"), "terror chicken"); ////new, blame Flame_Valxsarion for encouraging me. Actually don't, I came up with "monster cock" 
            case Race.Bees:
                return GetRandomStringFrom("apid", GetGenderString(unit, "worker bee", "drone", "bee"), "bee"); ////new 
            case Race.Ants:
                return GetRandomStringFrom("formica", GetGenderString(unit, "worker ant", "drone", "ant"), "ant"); ////new 
            case Race.Alraune:
                return GetRandomStringFrom("plant", "demi-plant", "flowery being"); ////new   
            case Race.Bats:
                return GetRandomStringFrom("bat", "chiropter", "demi-bat"); ////new         
            case Race.Merfolk:
                return GetRandomStringFrom("walking fish", GetGenderString(unit, "mermaid", "merman", "merfolk"), "merfolk"); ////new  
            case Race.Sharks:
                return GetRandomStringFrom("demi-shark", "shark", "landshark"); ////new     
            case Race.Gryphons:
                return GetRandomStringFrom("gryphon", "griffin", "griffon"); ////new 
            case Race.Kobolds:
                return GetRandomStringFrom("kobold", "little lizard", "little reptile"); ////new 
            case Race.Frogs:
                return GetRandomStringFrom("demi-frog", "amphibian", "frog"); ////new, many thanks to Flame_Valxsarion             
            case Race.FeralLions:
                return GetRandomStringFrom("feline", GetGenderString(unit, "lioness", "lion", "lion"), "leonine", "kitty");
            case Race.Aabayx:
                return GetRandomStringFrom("viroid", "virosapien"); ////new
            case Race.Mice:
                return GetRandomStringFrom("mouse", GetGenderString(unit, "doe", "buck", "murid"), "rodent");
            case Race.FeralOrcas:
                return GetRandomStringFrom("killer whale", "whale", "orca", "oversized dolphin", "cetacean", "apex dolphin");
            case Race.Feit:
                return GetRandomStringFrom("raptor", "dino", "draco-raptor", "draconic raptor", "raptoress", "she-raptor");
            case Race.Zoey:
                return GetRandomStringFrom("tiger shark", "anthro shark", "demi-shark");
            case Race.Avians:
                return GetRandomStringFrom("bird", "fowl", "bird of prey", "avian", "hawk", "eagle");
            case Race.Taraluxia:
                return GetRandomStringFrom("ice dragon", "dragon", "ice dragoness", "dragoness");
            case Race.BoomBunnies:
                return GetRandomStringFrom("bunny", GetGenderString(unit, "doe", "buck", "danger lagomorph"), "living-explosive", "fused rabbit");
            case Race.MainlandElves:
                return GetRandomStringFrom("elf", GetGenderString(unit, "woman", "man", "elf"), "humanoid");
            case Race.Tatltuae:
                return GetRandomStringFrom("cartograher", "raven", "chaos mage", "corvid", State.Rand.Next(42) == 1 ? "bird" : "birb");
            case Race.Firefly:
                return GetRandomStringFrom("umbreon", "mon", "pilot", "merc", "mercenary");
            case Race.Terrorbird:
                return GetRandomStringFrom("bird", "long-necked avian", "flightless bird", "anger bird");
            case Race.Iliijiith:
                return GetRandomStringFrom("being", "\"thing\"");
            case Race.Nectar:
                return GetRandomStringFrom("sphinx", "deity", "grand cat");
            case Race.Ryan:
                return GetRandomStringFrom("sergal", "veteran", "captain");
            default:
                return "creature";
        }
    }

    /// <summary>
    /// Gets a name that fits the weapon the unit's graphics show it using.
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetWeaponTrueName(Weapon weapon, Unit unit)
    {
        if (unit.Race == Race.Vagrants) return "Stinger";
        else if (unit.Race == Race.Wyvern) return "Claws";
        else if (unit.Race == Race.WyvernMatron) return "Fierce Claws";
        else if (unit.Race == Race.Serpents) return "Tail Blade";
        else if (unit.Race == Race.FeralSharks) return "Jaws";
        else if (unit.Race == Race.FeralWolves) return "Fangs";
        else if (unit.Race == Race.DarkSwallower) return "Jaws";
        else if (unit.Race == Race.Harvesters) return "Scythes";
        else if (unit.Race == Race.Collectors) return "Maw";
        else if (unit.Race == Race.Ki) return "Jaws";
        else if (unit.Race == Race.Selicia) return "Claws";
        else if (unit.Race == Race.Xelhilde) return "Zweihänder";
        else if (unit.Race == Race.Olivia) return "Static Fist";
        else if (unit.Race == Race.Nectar) return "Claws";

        else if (unit.Race == Race.Kangaroos)
        {
            if (weapon.Name == "Mace") return "Club";
            else if (weapon.Name == "Axe") return "Spear";
            else if (weapon.Name == "Simple Bow") return "Boomerang";
            else if (weapon.Name == "Compound Bow") return "Woomera";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.Goblins)
        {
            if (weapon.Name == "Mace") return "Cleaver";
            else if (weapon.Name == "Axe") return "Sharpened Cleaver";
            else if (weapon.Name == "Simple Bow") return "Derringer";
            else if (weapon.Name == "Compound Bow") return "Pepperbox Pistol";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Slimes)
        {
            if (weapon.Name == "Mace") return "Bone Blade";
            else if (weapon.Name == "Axe") return "Whip";
            else if (weapon.Name == "Simple Bow") return "Floating Slimey";
            else if (weapon.Name == "Compound Bow") return "Bioelectricity";
            else if (weapon.Name == "Claw") return "Hardened Lump";
        }
        else if (unit.Race == Race.Imps)
        {
            if (weapon.Name == "Mace") return "Morningstar";
            else if (weapon.Name == "Axe") return "Cleaver";
            else if (weapon.Name == "Simple Bow") return "Bow";
            else if (weapon.Name == "Compound Bow") return "Infernal Bow";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Crypters)
        {
            if (weapon.Name == "Mace") return "Sword";
            else if (weapon.Name == "Axe") return "Power Fist";
            else if (weapon.Name == "Simple Bow") return "Crossbow";
            else if (weapon.Name == "Compound Bow") return "Cannon";
            else if (weapon.Name == "Claw") return "Metal Fist";
        }
        else if (unit.Race == Race.Scylla)
        {
            if (weapon.Name == "Mace") return "Knife";
            else if (weapon.Name == "Axe") return "Trident";
            else if (weapon.Name == "Simple Bow") return "Javelin";
            else if (weapon.Name == "Compound Bow") return "Medusa Launcher";
            else if (weapon.Name == "Claw") return "Tentacle";
        }
        else if (unit.Race == Race.Harpies)
        {
            if (weapon.Name == "Mace") return "Bronze Claws";
            else if (weapon.Name == "Axe") return "Steel Claws";
            else if (weapon.Name == "Simple Bow") return "Simple Bow";
            else if (weapon.Name == "Compound Bow") return "Compound Bow";
            else if (weapon.Name == "Claw") return "Talons";
        }
        else if (unit.Race == Race.Taurus)
        {
            if (weapon.Name == "Mace") return "Hammer";
            else if (weapon.Name == "Axe") return "Glaive";
            else if (weapon.Name == "Simple Bow") return "Revolver";
            else if (weapon.Name == "Compound Bow") return "Shotgun";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Crux)
        {
            if (weapon.Name == "Mace" && unit.BasicMeleeWeaponType == 0) return "Bat";
            else if (weapon.Name == "Mace" && unit.BasicMeleeWeaponType == 1) return "Pipe";
            else if (weapon.Name == "Mace" && unit.BasicMeleeWeaponType == 2) return "Dildo";
            else if (weapon.Name == "Axe" && unit.AdvancedMeleeWeaponType == 0) return "Machete";
            else if (weapon.Name == "Axe" && unit.AdvancedMeleeWeaponType == 1) return "Axe";
            else if (weapon.Name == "Simple Bow") return "Handbow";
            else if (weapon.Name == "Compound Bow") return "Compound Bow";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.Alligators)
        {
            if (weapon.Name == "Mace") return "Turtle Club";
            else if (weapon.Name == "Axe") return "Flint Spear";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.Puca)
        {
            if (weapon.Name == "Mace") return "Shovel";
            else if (weapon.Name == "Axe") return "Shovel";
            else if (weapon.Name == "Simple Bow") return "Slingshot";
            else if (weapon.Name == "Compound Bow") return "Heavy Slingshot";
        }
        else if (unit.Race == Race.Hamsters)
        {
            if (weapon.Name == "Mace") return "Warhammer";
            else if (weapon.Name == "Axe") return "Heavy Warhammer";
            else if (weapon.Name == "Simple Bow") return "Throwing Axe";
            else if (weapon.Name == "Compound Bow") return "Throwing Axe";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.RwuMercenaries)
        {
            if (weapon.Name == "Mace") return "Combat Knife";
            else if (weapon.Name == "Axe" && unit.EyeType == 0) return "Machete";
            else if (weapon.Name == "Axe" && unit.EyeType == 1) return "Ikakalaka";
            else if (weapon.Name == "Axe" && unit.EyeType == 2) return "Sabre";
            else if (weapon.Name == "Axe" && unit.EyeType == 3) return "Cutlass";
            else if (weapon.Name == "Simple Bow") return "Sidearm";
            else if (weapon.Name == "Compound Bow" && unit.EyeType == 0) return "SMG";
            else if (weapon.Name == "Compound Bow" && unit.EyeType == 1) return "LMG";
            else if (weapon.Name == "Compound Bow" && unit.EyeType == 2) return "Sniper Rifle";
            else if (weapon.Name == "Compound Bow" && unit.EyeType == 3) return "Assault Rifle";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Vipers)
        { /*V33B ADDITION*/
            if (weapon.Name == "Mace") return "Arc Blade";
            else if (weapon.Name == "Axe") return "Fusion Blade";
            else if (weapon.Name == "Simple Bow") return "Plasma Pistol";
            else if (weapon.Name == "Compound Bow") return "Plasma Rifle";
        }
        else if (unit.Race == Race.Sergal)
        {
            if (weapon.Name == "Mace") return "Lance";
            else if (weapon.Name == "Axe") return "Twin Glaive";
            else if (weapon.Name == "Simple Bow") return "Speargun";
            else if (weapon.Name == "Compound Bow") return "Prototype Railgun"; ////changed to "prototype railgun", thanks to Flame_Valxsarion
        }
        else if (unit.Race == Race.Bees)
        {
            if (weapon.Name == "Mace") return "Honeycomb Mace";
            else if (weapon.Name == "Axe") return "Quad Punch Claws";
            else if (weapon.Name == "Simple Bow") return "Javelin";
            else if (weapon.Name == "Compound Bow") return "War Javelin";
        }
        else if (unit.Race == Race.Driders)
        {
            if (weapon.Name == "Mace") return "Dagger";
            else if (weapon.Name == "Axe") return "Short Sword";
            else if (weapon.Name == "Simple Bow") return "Pistol Crossbow";
            else if (weapon.Name == "Compound Bow") return "Crossbow";
        }
        else if (unit.Race == Race.Alraune)
        {
            if (weapon.Name == "Mace") return "Vine Whip";
            else if (weapon.Name == "Axe") return "Stem Blade";
            else if (weapon.Name == "Simple Bow") return "Unbloomed Corolla";
            else if (weapon.Name == "Compound Bow") return "Blooming Flower";
        }
        else if (unit.Race == Race.Bats)
        {
            if (weapon.Name == "Mace") return "Push Dagger";
            else if (weapon.Name == "Axe") return "Claw Katar";
            else if (weapon.Name == "Simple Bow") return "Iron Throwing Knife";
            else if (weapon.Name == "Compound Bow") return "Steel Throwing Knife";
        }
        else if (unit.Race == Race.Panthers)
        {
            if (weapon.Name == "Mace") return "Karambit";
            else if (weapon.Name == "Axe") return "Kukri";
            else if (weapon.Name == "Simple Bow") return "Chakram";
            else if (weapon.Name == "Compound Bow") return "Onzil";
        }
        else if (unit.Race == Race.Merfolk)
        {
            if (weapon.Name == "Mace") return "Crude Trident";
            else if (weapon.Name == "Axe") return "Royal Trident";
            else if (weapon.Name == "Simple Bow") return "Scepter";
            else if (weapon.Name == "Compound Bow") return "Orb Staff";
        }
        else if (unit.Race == Race.Ants)
        {
            if (weapon.Name == "Mace") return "Barbed Spear";
            else if (weapon.Name == "Axe") return "Quad Blades";
            else if (weapon.Name == "Simple Bow") return "Simple Bow";
            else if (weapon.Name == "Compound Bow") return "Compound Bow";
        }
        else if (unit.Race == Race.Avians)
        {
            if (weapon.Name == "Mace") return "Knife";
            else if (weapon.Name == "Axe") return "Sword";
            else if (weapon.Name == "Simple Bow") return "Short Bow";
            else if (weapon.Name == "Compound Bow") return "Crossbow";
        }
        else if (unit.Race == Race.Sharks)
        {
            if (weapon.Name == "Mace") return "Sabre";
            else if (weapon.Name == "Axe") return "Cutlass";
            else if (weapon.Name == "Simple Bow") return "Harpoon";
            else if (weapon.Name == "Compound Bow") return "Speargun";
        }
        else if (unit.Race == Race.Frogs)
        {
            if (weapon.Name == "Mace") return "Mace";
            else if (weapon.Name == "Axe") return "Axe";
            else if (weapon.Name == "Simple Bow") return "Slingshot";
            else if (weapon.Name == "Compound Bow") return "Feathered Bow";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Hippos)
        {
            if (weapon.Name == "Mace") return "Tribal Knife";
            else if (weapon.Name == "Axe") return "Axe";
            else if (weapon.Name == "Simple Bow") return "Simple Bow";
            else if (weapon.Name == "Compound Bow") return "Compound Bow";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Kobolds)
        {
            if (weapon.Name == "Mace") return "Pickax";
            else if (weapon.Name == "Axe") return "Pickax";
            else if (weapon.Name == "Simple Bow") return "Dart";
            else if (weapon.Name == "Compound Bow") return "Dart";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Aabayx)
        {
            if (weapon.Name == "Mace") return "Longscalpel";
            else if (weapon.Name == "Axe") return "Personsmasher";
            else if (weapon.Name == "Simple Bow") return "Razorflinger";
            else if (weapon.Name == "Compound Bow") return "Arrowbisector";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Mice)
        {
            if (weapon.Name == "Mace") return "Shortsword";
            else if (weapon.Name == "Axe") return "Bardiche";
            else if (weapon.Name == "Simple Bow") return "Hand Crossbow";
            else if (weapon.Name == "Compound Bow") return "Greatbow";
        }
        else if (unit.Race == Race.Bears)
        {
            if (weapon.Name == "Mace") return "Hand Axe";
            else if (weapon.Name == "Axe") return "Battle Axe";
            else if (weapon.Name == "Simple Bow") return "Spear";
            else if (weapon.Name == "Compound Bow") return "Throwing Axe";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.Umbreon)
        {
            if (weapon.Name == "Mace") return "Claw Gauntlet";
            else if (weapon.Name == "Axe") return "Scythe";
            else if (weapon.Name == "Simple Bow") return "Slingbow";
            else if (weapon.Name == "Compound Bow") return "Mechanical Slingbow";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (weapon.Name == "Claw") return "Claws";
        return weapon.Name;
    }
}

