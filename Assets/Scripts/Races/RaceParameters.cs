using OdinSerializer;
using System.Collections.Generic;

static class RaceParameters
{
    static readonly RaceTraits Default;
    static readonly RaceTraits Cats;
    static readonly RaceTraits Dogs;
    static readonly RaceTraits Foxes;
    static readonly RaceTraits Youko;
    static readonly RaceTraits Wolves;
    static readonly RaceTraits Bunnies;
    static readonly RaceTraits Lizards;
    static readonly RaceTraits Slimes;
    static readonly RaceTraits Scylla;
    static readonly RaceTraits Harpies;
    static readonly RaceTraits Imps;
    static readonly RaceTraits Humans;
    static readonly RaceTraits Crypters;
    static readonly RaceTraits Lamia;
    static readonly RaceTraits Kangaroos;
    static readonly RaceTraits Taurus;
    static readonly RaceTraits Crux;
    static readonly RaceTraits Equines;
    static readonly RaceTraits Sergal;
    static readonly RaceTraits Bees;
    static readonly RaceTraits Driders;
    static readonly RaceTraits Alraune;
    static readonly RaceTraits Demibats;
    static readonly RaceTraits Panthers;
    static readonly RaceTraits Mermen;
    static readonly RaceTraits Avians;
    static readonly RaceTraits Demiants;
    static readonly RaceTraits Demifrogs;
    static readonly RaceTraits Demisharks;
    static readonly RaceTraits Deer;
    static readonly RaceTraits Aabayx;
    static readonly RaceTraits Mice;
    static readonly RaceTraits Gnolls;
    static readonly RaceTraits MainlandElves;
    static readonly RaceTraits Bears;
    static readonly RaceTraits Umbreon;
    static readonly RaceTraits Eevee;
    static readonly RaceTraits Centaur;
    static readonly RaceTraits Succubi;
    static readonly RaceTraits Tigers;
    static readonly RaceTraits Goblins;
    static readonly RaceTraits Alligators;
    static readonly RaceTraits Puca;
    static readonly RaceTraits Kobolds;
    static readonly RaceTraits DewSprite;
    static readonly RaceTraits Hippos;
    static readonly RaceTraits Vipers;
    static readonly RaceTraits Komodos;
    static readonly RaceTraits Cockatrice;
    static readonly RaceTraits Vargul;
    static readonly RaceTraits Hamsters;
    static readonly RaceTraits RwuMercenaries;
    static readonly RaceTraits Vagrants;
    static readonly RaceTraits Serpents;
    static readonly RaceTraits Wyvern;
    static readonly RaceTraits Compy;
    static readonly RaceTraits Sharks;
    static readonly RaceTraits FeralWolves;
    static readonly RaceTraits BlackSwallowers;
    static readonly RaceTraits Cake;
    static readonly RaceTraits Harvesters;
    static readonly RaceTraits Collectors;
    static readonly RaceTraits Voilin;
    static readonly RaceTraits Bats;
    static readonly RaceTraits Frogs;
    static readonly RaceTraits Dragons;
    static readonly RaceTraits Dragonfly;
    static readonly RaceTraits Plants;
    static readonly RaceTraits Fairies;
    static readonly RaceTraits FeralAnts;
    static readonly RaceTraits Gryphons;
    static readonly RaceTraits SpitterSlugs;
    static readonly RaceTraits SpringSlugs;
    static readonly RaceTraits RockSlugs;
    static readonly RaceTraits CoralSlugs;
    static readonly RaceTraits Salamanders;
    static readonly RaceTraits Mantis;
    static readonly RaceTraits EasternDragon;
    static readonly RaceTraits Catfish;
    static readonly RaceTraits Raptor;
    static readonly RaceTraits WarriorAnts;
    static readonly RaceTraits Gazelle;
    static readonly RaceTraits FeralLions;
    static readonly RaceTraits Earthworms;
    static readonly RaceTraits FeralLizards;
    static readonly RaceTraits Monitors;
    static readonly RaceTraits Schiwardez;
    static readonly RaceTraits Terrorbirds;
    static readonly RaceTraits Dratopyr;
    static readonly RaceTraits Selicia;
    static readonly RaceTraits Vision;
    static readonly RaceTraits Ki;
    static readonly RaceTraits Scorch;
    static readonly RaceTraits Asura;
    static readonly RaceTraits DRACO;
    static readonly RaceTraits Zoey;
    static readonly RaceTraits Cierihaka;
    static readonly RaceTraits Zera;
    static readonly RaceTraits Auri;
    static readonly RaceTraits Erin;
    static readonly RaceTraits Goodra;
    static readonly RaceTraits Whisp;
    static readonly RaceTraits Salix;
    static readonly RaceTraits Bella;
    static readonly RaceTraits FeralHorses;
    static readonly RaceTraits Abakhanskya;
    static readonly RaceTraits MatronsMinions;
    static readonly RaceTraits WyvernMatron;
    static readonly RaceTraits Singularity;
    static readonly RaceTraits Feit;
    static readonly RaceTraits FeralFox;
    static readonly RaceTraits Terminid;
    static readonly RaceTraits FeralOrcas;
    static readonly RaceTraits Taraluxia;
    static readonly RaceTraits Xelhilde;
    static readonly RaceTraits BoomBunnies;
    static readonly RaceTraits FeralSlime;
    static readonly RaceTraits Olivia;
    static readonly RaceTraits ViraeUltimae;
    static readonly RaceTraits Equaleon;
    static readonly RaceTraits Viisels;
    static readonly RaceTraits FeralEevee;
    static readonly RaceTraits FeralUmbreon;
    static readonly RaceTraits FeralEqualeon;
    static readonly RaceTraits Skapa;
    static readonly RaceTraits Tatltuae;
    static readonly RaceTraits Lupine;
    static readonly RaceTraits Jackals;
    static readonly RaceTraits Firefly;
    static readonly RaceTraits Otachi;
    static readonly RaceTraits Raiju;
    static readonly RaceTraits Smudger;
    static readonly RaceTraits SpaceCroach;
    static readonly RaceTraits Trex;
    static readonly RaceTraits Utahraptor;
    static readonly RaceTraits Ghosts;
    static readonly RaceTraits WoodDryad;
    static readonly RaceTraits EarthDryad;
    static readonly RaceTraits RiverDryad;
    static readonly RaceTraits FungalDryad;

    static Unit tempUnit;


    /// <summary>
    /// Mostly used as a helper function, for the village race population growth
    /// </summary>
    /// <param name="race"></param>
    /// <returns></returns>
    internal static RaceTraits GetRaceTraits(Race race)
    {
        if (tempUnit == null)
            tempUnit = new Unit(race);
        tempUnit.Race = race;
        return GetTraitData(tempUnit);
    }

    internal static RaceTraits GetTraitData(Unit unit)
    {
        if (Config.RaceTraitsEnabled == false)
        {
            return Default;
        }
        switch (unit.Race)
        {
            case Race.Cats:
                return Cats;
            case Race.Dogs:
                return Dogs;
            case Race.Foxes:
                return Foxes;
            case Race.Youko:
                return Youko;
            case Race.Wolves:
                return Wolves;
            case Race.Bunnies:
                return Bunnies;
            case Race.Lizards:
                return Lizards;
            case Race.Slimes:
                return Slimes;
            case Race.Scylla:
                return Scylla;
            case Race.Harpies:
                return Harpies;
            case Race.Imps:
                return Imps;
            case Race.Humans:
                return Humans;
            case Race.Crypters:
                return Crypters;
            case Race.Lamia:
                return Lamia;
            case Race.Kangaroos:
                return Kangaroos;
            case Race.Crux:
                return Crux;
            case Race.Equines:
                return Equines;
            case Race.Sergal:
                return Sergal;
            case Race.Bees:
                return Bees;
            case Race.Driders:
                return Driders;
            case Race.Alraune:
                return Alraune;
            case Race.Bats:
                return Demibats;
            case Race.Merfolk:
                return Mermen;
            case Race.Avians:
                return Avians;
            case Race.Ants:
                return Demiants;
            case Race.Frogs:
                return Demifrogs;
            case Race.Sharks:
                return Demisharks;
            case Race.Deer:
                return Deer;
            case Race.Aabayx:
                return Aabayx;
            case Race.Mice:
                return Mice;
            case Race.Succubi:
                return Succubi;
            case Race.Tigers:
                return Tigers;
            case Race.Goblins:
                return Goblins;
            case Race.Puca:
                return Puca;
            case Race.Kobolds:
                return Kobolds;
            case Race.Hippos:
                return Hippos;
            case Race.Vipers:
                return Vipers;
            case Race.Komodos:
                return Komodos;
            case Race.Cockatrice:
                return Cockatrice;
            case Race.Vargul:
                return Vargul;
            case Race.Hamsters:
                return Hamsters;
            case Race.RwuMercenaries:
                return RwuMercenaries;
            case Race.Equaleon:
                return Equaleon;
            case Race.Vagrants:
                return Vagrants;
            case Race.Serpents:
                return Serpents;
            case Race.Wyvern:
                return Wyvern;
            case Race.Compy:
                return Compy;
            case Race.FeralSharks:
                return Sharks;
            case Race.FeralWolves:
                return FeralWolves;
            case Race.FeralLions:
                return FeralLions;
            case Race.Alligators:
                return Alligators;
            case Race.Taurus:
                return Taurus;
            case Race.DarkSwallower:
                return BlackSwallowers;
            case Race.Cake:
                return Cake;
            case Race.Harvesters:
                return Harvesters;
            case Race.Collectors:
                return Collectors;
            case Race.Voilin:
                return Voilin;
            case Race.FeralBats:
                return Bats;
            case Race.FeralFrogs:
                return Frogs;
            case Race.Dragon:
                return Dragons;
            case Race.Dragonfly:
                return Dragonfly;
            case Race.TwistedVines:
                return Plants;
            case Race.Fairies:
                return Fairies;
            case Race.FeralAnts:
                return FeralAnts;
            case Race.Gryphons:
                return Gryphons;
            case Race.SpitterSlugs:
                return SpitterSlugs;
            case Race.SpringSlugs:
                return SpringSlugs;
            case Race.RockSlugs:
                return RockSlugs;
            case Race.CoralSlugs:
                return CoralSlugs;
            case Race.Salamanders:
                return Salamanders;
            case Race.Mantis:
                return Mantis;
            case Race.EasternDragon:
                return EasternDragon;
            case Race.Catfish:
                return Catfish;
            case Race.Raptor:
                return Raptor;
            case Race.WarriorAnts:
                return WarriorAnts;
            case Race.Gazelle:
                return Gazelle;
            case Race.Earthworms:
                return Earthworms;
            case Race.FeralLizards:
                return FeralLizards;
            case Race.Monitors:
                return Monitors;
            case Race.Schiwardez:
                return Schiwardez;
            case Race.Terrorbird:
                return Terrorbirds;
            case Race.Dratopyr:
                return Dratopyr;
            case Race.Selicia:
                return Selicia;
            case Race.Vision:
                return Vision;
            case Race.Ki:
                return Ki;
            case Race.Scorch:
                return Scorch;
            case Race.Asura:
                return Asura;
            case Race.DRACO:
                return DRACO;
            case Race.Zoey:
                return Zoey;
            case Race.Cierihaka:
                return Cierihaka;
            case Race.Zera:
                return Zera;
            case Race.Panthers:
                return Panthers;
            case Race.DewSprites:
                return DewSprite;
            case Race.Auri:
                return Auri;
            case Race.Erin:
                return Erin;
            case Race.Goodra:
                return Goodra;
            case Race.Whisp:
                return Whisp;
            case Race.Salix:
                return Salix;
            case Race.Bella:
                return Bella;
            case Race.FeralHorses:
                return FeralHorses;
            case Race.Abakhanskya:
                return Abakhanskya;
            case Race.MatronsMinions:
                return MatronsMinions;
            case Race.WyvernMatron:
                return WyvernMatron;
            case Race.Singularity:
                return Singularity;
            case Race.Feit:
                return Feit;
            case Race.FeralFox:
                return FeralFox;
            case Race.Terminid:
                return Terminid;
            case Race.FeralOrcas:
                return FeralOrcas;
            case Race.Taraluxia:
                return Taraluxia;
            case Race.Xelhilde:
                return Xelhilde;
            case Race.BoomBunnies:
                return BoomBunnies;
            case Race.Gnolls:
                return Gnolls;
            case Race.Centaur:
                return Centaur;
            case Race.FeralSlime:
                return FeralSlime;
            case Race.Olivia:
                return Olivia;
            case Race.MainlandElves:
                return MainlandElves;
            case Race.Bears:
                return Bears;
            case Race.Eevee:
                return Eevee;
            case Race.Umbreon:
                return Umbreon;
            case Race.ViraeUltimae:
                return ViraeUltimae;
            case Race.Viisels:
                return Viisels;
            case Race.FeralEevee:
                return FeralEevee;
            case Race.FeralUmbreon:
                return FeralUmbreon;
            case Race.FeralEqualeon:
                return FeralEqualeon;
            case Race.Skapa:
                return Skapa;
            case Race.Tatltuae:
                return Tatltuae;
            case Race.Lupine:
                return Lupine;
            case Race.Jackals:
                return Jackals;
            case Race.Firefly:
                return Firefly;
            case Race.Otachi:
                return Otachi;
            case Race.Raiju:
                return Raiju;
            case Race.Smudger:
                return Smudger;
            case Race.SpaceCroach:
                return SpaceCroach;
            case Race.Ghosts:
                return Ghosts;
            case Race.WoodDryad:
                return WoodDryad;
            case Race.EarthDryad:
                return EarthDryad;
            case Race.RiverDryad:
                return RiverDryad;
            case Race.FungalDryad:
                return FungalDryad;
            case Race.Trex:
                return Trex;
            case Race.Utahraptor:
                return Utahraptor;
            case (Race)700: //Singled out so that it doesn't make the debug message
                return Default;
            case (Race)701:
                return Default;
            default:
                UnityEngine.Debug.Log("Couldn't find race, substituting the Default");
                return Default;
        }
    }

    static RaceParameters()
    {
        Default = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 12,
            HasTail = false,
            FavoredStat = Stat.None,
            RacialTraits = new List<Traits>(),
            RaceDescription = "Whip, whup, brrr. I'm a Default. Phear me!",
        };

        Cats = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
            {
                Traits.Pounce,
                Traits.EscapeArtist,
                Traits.Pathfinder,
            },
            RaceDescription = "Natives to the realm, the Cats are skilled at pouncing on their enemy with a sudden burst of speed. Many a wounded warrior has found themselves devoured by a feline jumping over a wall of their fellow warriors, while the Cat's allies defy their enemies by somehow squirming out of their stomach's.",
        };

        Dogs = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.PackWill,
            Traits.PackDefense,
            Traits.Intimidating,
        },
            RaceDescription = "Natives to the realm, the Dogs embody the principle of standing together. Ranked up, they guard each other's backs, making it harder for any enemy to land a strike at them or succeed at eating them.",
        };

        Foxes = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Mind,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.ArtfulDodge,
            Traits.ThrillSeeker
        },
            RaceDescription = "Natives of this realm, the Foxes seem incapable of taking danger seriously. They dodge attacks at the last second and only seem to grow ever bolder as death approaches them. Entire armies have fallen exhausted as a group of foxes dances among them, ready to be devoured once the time is right.",
        };

        Youko = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Mind,
            DeployCost = 1,
            Upkeep = 21f,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.BreastVore, VoreType.Anal, VoreType.TailVore },
            RacialTraits = new List<Traits>()
            {
                Traits.Charmer,
                Traits.Temptation,
                Traits.Possession,
                Traits.ForceFeeder,
                Traits.ManaDrain,
                Traits.CreateSpawn,
            },
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(6, 14),
                Dexterity = new RaceStats.StatRange(6, 14),
                Endurance = new RaceStats.StatRange(6, 14),
                Mind = new RaceStats.StatRange(14, 22),
                Will = new RaceStats.StatRange(8, 16),
                Agility = new RaceStats.StatRange(8, 18),
                Voracity = new RaceStats.StatRange(12, 18),
                Stomach = new RaceStats.StatRange(12, 18),
            },
            SpawnRace = Race.Whisp,
            ConversionRace = Race.Foxes,
            RaceDescription = "Foxes that were changed by spirit energy",
        };

        Wolves = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Strength,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.PackStrength,
            Traits.PackVoracity,
			Traits.Tenacious,
        },
            RaceDescription = "Natives of this realm, the Wolves have a history of hunting in packs extending beyond the crafting of their first weapons. While a lone Wolf can still be a worthy adversary, their true strength comes from working with their kin.",
        };


        Bunnies = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Dexterity,
            DeployCost = 1,
            Upkeep = 2f,
            RacialTraits = new List<Traits>()
        {
            Traits.ProlificBreeder,
            Traits.EasyToVore,
            Traits.ArtfulDodge,
            Traits.EvasiveBattler
        },
            RaceDescription = "Among the weaker but more numerous of the native sapient species, the Bunnies are on the verge of turning predators themselves. While lacking in sheer strength they make up for it with agility and numbers, having much fun ensuring the latter.",
        };



        Lizards = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            DeployCost = 1,
            Upkeep = 4f,
            RacialTraits = new List<Traits>()
        {
            Traits.Resilient,
            Traits.Intimidating,
            Traits.GiantSlayer,
        },

            RaceDescription = "Emerging from dense jungles, the Lizards are eager to expand their presence in the world. Their hard scales offered them great protection from the thorns and insects of their former home, and still offer natural resistance from harm.",
        };



        Slimes = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 20,
            HasTail = false,
            FavoredStat = Stat.Stomach,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.BoggingSlime,
            Traits.GelatinousBody,
            Traits.SoftBody,
        },
            RaceDescription = "A puddle of goo given form by the power of their core, the Slimes have a need to act as if they had solid bodies. Their true form is still almost liquid though, lacking organs or other features of note, and thus very hard to damage by normal means.",
        };


        Scylla = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = false,
            FavoredStat = Stat.Will,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            //Traits.Aquatic,
            Traits.TentacleHarassment,
            Traits.KeenReflexes,
            Traits.AwkwardShape,
        },
            RaceDescription = "Trapped under the surface at their old world, the Scylla surged forth when the appearance of mystical portals gave them passage to lands above water. Their many tentacles seem to act as if having minds of their own, hindering and harassing their enemies.",
        };


        Harpies = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 2f,
            RacialTraits = new List<Traits>()
        {
            Traits.Flight,
            Traits.Pathfinder,
            Traits.KeenReflexes
        },
            RaceDescription = "Emerging from a portal high in the sky, the Harpyia saw a whole new land beneath them and descended looking for fresh prey. While unable to fly and hold weapons at their claws at the same time, the harpy are quite adept at fighting with their strong talons, as well as at dropping things from high above instead of using more prevalent ranged weapons.",
        };


        Imps = new RaceTraits()
        {
            BodySize = 8,
            StomachSize = 12,
            HasTail = true,
            FavoredStat = Stat.Will,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.PackStomach,
            Traits.MagicResistance,
            Traits.AstralCall,
        },
		RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(6, 14),
                Dexterity = new RaceStats.StatRange(6, 14),
                Endurance = new RaceStats.StatRange(6, 14),
                Mind = new RaceStats.StatRange(14, 22),
                Will = new RaceStats.StatRange(8, 16),
                Agility = new RaceStats.StatRange(8, 18),
                Voracity = new RaceStats.StatRange(12, 18),
                Stomach = new RaceStats.StatRange(20, 30),
            },
            RaceDescription = "Following the scent of new lands to torment, these beings erupted forth from the underworld. So eager are they that at the promise of battle some of the Imps still in the infernal realm may manifest just for a chance at carnage.",
        };


        Humans = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 12,
            HasTail = false,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.AdeptLearner,
            Traits.Clever
        },
            RaceDescription = "These nearly hairless, soft skinned creatures suffer from a lack in the way of physical abilities, but have proven capable of improving their skills at a great speed.",
        };


        Crypters = new RaceTraits()
        {
            BodySize = 15,
            StomachSize = 15,
            HasTail = false,
            FavoredStat = Stat.Endurance,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.SlowBreeder,
            Traits.MetalBody,
            Traits.Resilient
        },
            RaceDescription = "Arriving from a realm long dead, the Crypters shambled forth when the smell of the living beckoned them from their ancient tombs. Cold, hard metal resists both damage and attempts to eat it, but the strange powers of this realm provide no aid in crafting new automatons for the ancient spirits to inhabit.",
        };


        Lamia = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Stomach,
            DeployCost = 1,
            Upkeep = 7f,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.BreastVore, VoreType.Anal, VoreType.TailVore },
            RacialTraits = new List<Traits>()
        {
            Traits.Ravenous,
            Traits.Biter,
            Traits.DualStomach,
			Traits.StrongGullet
        },
            RaceDescription = "Natives to this realm, these legless beings were once the strongest and largest hunters of the land. The sudden emergence of many new species left the Lamia uncertain at first, but soon their dual stomachs won and they focused on testing the taste of the new arrivals.",
        };

        Kangaroos = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.BornToMove,
            Traits.Resourceful,
        },
            RaceDescription = "Their old home turning ever drier and hotter, the Kangaroo tribes did not hesitate when mysterious portals opened and granted them passage to greener lands. Nomadic by nature, the Kangaroos are very adept at carrying plenty of gear with them and aren't unused to traveling with a full belly either.",
        };

        Taurus = new RaceTraits()
        {
            BodySize = 15,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Strength,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
                Traits.StrongMelee,
                Traits.ForcefulBlow,
                Traits.StretchyInsides,
        },
            RaceDescription = "Once mere cattle, a drop of minotaur blood slumbered in their veins. Rising and butchering their \"masters\", the Taurus took what they could from their old ranches and fled through mysterious portals that had heralded their rise. While intelligent, the Taurus trust in their physical might and great size, tossing their enemies aside as they trample on.",
        };

        Crux = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Will,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
                Traits.KeenReflexes,
                Traits.EscapeArtist,
                Traits.MadScience
        },
            RaceDescription = "Their own world having risen and fallen, the Crux arrived to this one almost by accident. While they initially thought it rather a boring place, they soon realised its potential and were eager to try and shape it according to their own ever shifting ideals.",
        };

        Equines = new RaceTraits()
        {
            BodySize = 15,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Strength,
            DeployCost = 1,
            Upkeep = 4f,
            RacialTraits = new List<Traits>()
        {
                Traits.Charge,
                Traits.StrongMelee,
                Traits.RangedIneptitude,
        },
            RaceDescription = "Mighty horsefolk from another world, the Equines are a proud people, and while from the outside they can seem to move and act with remarkable unity, within their ranks is a game of constant power struggles and jockeying for position and influence.",
        };

        Sergal = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 4f,
            RacialTraits = new List<Traits>()
        {
                Traits.KeenReflexes,
                Traits.StrongMelee,
                Traits.EscapeArtist
        },
            RaceDescription = "When the portal appeared over Lake ColVilous in the year RC102, the city of Gold Ring quickly fell into a panic.  Nothing hostile came from the rift, however, and that initial panic changed into curiosity.  After the progressive minds determined that it was a gateway to another world, and a world full of green plants at that, the only thing to do was form a Scouting Regiment to secure a foothold and see what might be gained from this mysterious place.  But just as that first settlement was established, the portal blinked out of existence, cutting off passage between Tal and this new world.  The changes began a mere week into isolation: People became more violent and sexually aggressive, with physical changes to match the mental ones shortly afterward.  And then... Then the hunger set in.",
        };

        Bees = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 4f,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.BreastVore, VoreType.Anal, VoreType.TailVore },
            RacialTraits = new List<Traits>()
        {
                Traits.Flight,
                Traits.KeenReflexes,
                Traits.PackDefense,
                Traits.Stinger
        },
            RaceDescription = "The Bees exist in a sort of loose hive mind, and tend to care little for the loss of individual members of their kind. They tend to be ruthless when it comes to the defence of what they already own, though they tend to only strike outwards when they feel that there is something to be gained by doing so. Or when they're hungry. At their size, the typical diet of smaller bees, pollen, nectar, and honey, just doesn't quite cut it.",
        };

        Driders = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 15,
            HasTail = false,
            FavoredStat = Stat.Strength,
            DeployCost = 1,
            Upkeep = 7f,
            RacialTraits = new List<Traits>()
        {
                Traits.StrongMelee,
                Traits.NimbleClimber,
                Traits.Webber,
        },
            RaceDescription = "The Driders are a mysterious folk, with lots of conflicting information on what they are, where they came from, and what they want. This actually seems to be deliberate on their part, as Driders often spread misinformation on themselves to confuse their potential future prey and trick them into making mistakes. This has actually worked too well, as the Driders have actually forgotten their true history. Without a solid account on them, almost all stories and rumors of the Driders can be considered equally untrue, and therefore equally true.",
        };

        Alraune = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 16,
            HasTail = false,
            FavoredStat = Stat.Endurance,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
                Traits.Tempered,
                Traits.SlowAbsorption,
                Traits.PollenProjector
        },
            RaceDescription = "The Alraunes are a deeply spiritual people, with a deep connection to nature, especially plants. Unfortunately, they tend to take an overprotective stance towards plants and will ruthlessly attack and devour any who dare harm plant life at all. That includes walking on grass, by the way. Tread lightly!",
        };

        Demibats = new RaceTraits()
        {
            BodySize = 8,
            StomachSize = 15,
            HasTail = false,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
                Traits.Flight,
                Traits.ArtfulDodge,
                Traits.Vampirism
        },
            RaceDescription = "Winged half-humans, the Bats generally prefer to live in caves, or, failing that, stick to being mostly active at night. Of the so called \"civilized\" races, the bats are one of the more aggressive towards outsiders, their habit of near constant flight while landing less than they should demanding lots of nutrients to support, resulting in them being far more eager to devour their foes than most.",
        };

        Panthers = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Strength,
            DeployCost = 1,
            Upkeep = 4f,
            RacialTraits = new List<Traits>()
        {
                Traits.TasteForBlood,
                Traits.Pounce,
                Traits.Frenzy,
        },
            RaceDescription = "Long before \"elder races\" walked among the stars, the Panthers thrived. Long before first words of power were uttered, they have carved their homes into the lightning-struck bark, feeding off its power. And long after the last bastion of so-called civilization will fall to onslaught of wings, claws and fangs, they will thrive in the darkness, pouncing on unsuspecting prey.",
        };

        Mermen = new RaceTraits()
        {
            BodySize = 15,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Will,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
                Traits.MagicResistance,
                Traits.HealingBlood,
                Traits.Slippery,
                Traits.WaterWalker,
        },
            RaceDescription = "The Merfolk are a tricky people, as they combine magic and deception to lure and manipulate their foes, often directing them to monster threats. Whoever wins at that point, the Merfolk come in to eat up the remaining threats.",
        };

        Avians = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 14,
            HasTail = true,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
                Traits.KeenShot,
                Traits.Featherweight
        },
            RaceDescription = "A highly eclectic people, large amounts of \"Avian\" culture are actually derived from other peoples and races. This can largely be credited to their having started late. By the time the Avians properly entered the civilizational stage, most other races had already entered. Now, the Avians are eager to continue adding others to themselves. And if the Avians can't merge your culture into their own, they may instead opt to merge your body into their body fat.",
        };

        Demiants = new RaceTraits()
        {
            BodySize = 9,
            StomachSize = 15,
            HasTail = false,
            FavoredStat = Stat.Strength,
            DeployCost = 1,
            Upkeep = 4f,
            RacialTraits = new List<Traits>()
        {
                Traits.PackStrength,
                Traits.RangedIneptitude,
                Traits.AntPheromones
        },
            RaceDescription = "The Ants exist in a sort of loose hive mind, and tend to care little for the loss of individual members of their swarms. Ants have an innate and insatiable drive to expand their territory and conquer all there is. Ants often control lesser ant species through complex pheromones, which are also generally used for ant-to-ant communication.",
        };

        Demifrogs = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 20,
            HasTail = false,
            FavoredStat = Stat.Voracity,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
                Traits.Pounce,
                Traits.HeavyPounce,
                Traits.RangedVore,
                Traits.Clumsy,
        },
            RaceDescription = "Native to the marshlands of this world, Frogs are well known for their highly territorial behavior, with Frog chiefdoms going to war with each other over perceived slights just as often as they go to war with others. Their wide mouths and big bellies aren't just for show, and many an unfortunate soul have found themselves on the wrong side of a Frog's flesh. Namely, the inside.",
        };

        Demisharks = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            DeployCost = 1,
            Upkeep = 4f,
            RacialTraits = new List<Traits>()
        {
                Traits.Biter,
                Traits.SenseWeakness,
                Traits.StrongGullet,
                Traits.WaterWalker,
        },
            RaceDescription = "The Sharks are not natives of this world. In their own world, they are the only sapient people, and had a wide variety of cultures and lifestyles. However, all Sharks in this world came through when a pirate ship in their realm accidently went through a portal to this one. Even today, most Sharks are pirates, and a majority of pirates are Sharks.",
        };

        Deer = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
                Traits.EvasiveBattler,
                Traits.ArtfulDodge,
                Traits.PackDefense,
        },
            RaceDescription = "The deer are one of the Realm's native peoples, and one with a long history of being labeled as \"prey.\" Whether historical Deer couldn't vore or simply didn't know how to vore is largely unknown, but in recent times anyone who calls a Deer prey in front of them often gets a personal tour of the cervid digestive system.",
        };

        Aabayx = new RaceTraits()
        {
            BodySize = 6,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Strength,
            DeployCost = 1,
            Upkeep = 3f,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.Anal },
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(10, 18),
                Dexterity = new RaceStats.StatRange(10, 18),
                Endurance = new RaceStats.StatRange(6, 10),
                Mind = new RaceStats.StatRange(7, 9),
                Will = new RaceStats.StatRange(10, 15),
                Agility = new RaceStats.StatRange(9, 17),
                Voracity = new RaceStats.StatRange(8, 14),
                Stomach = new RaceStats.StatRange(8, 16),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.ViralBiology,
                Traits.SlowBreeder,
                Traits.FastDigestion,
        },
            RaceDescription = "The Aabayx are a species of virosapiens who recently revealed themselves to the world and were quick to commit to the stage of war.  Strangely enough, they are not new arrivals to the realm, but rather have been in extreme isolation in an unknown location and were waiting for the exact right time to resurface and conquer the masses.  That time is now.",
        };

        Mice = new RaceTraits()
        {
            BodySize = 8,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 2f,
            RacialTraits = new List<Traits>()
        {
                Traits.ProlificBreeder,
                Traits.Timid,
                Traits.SwiftStrike,
                Traits.Clever,
        },
            RaceDescription = "Originally ordinary lab mice, tucked away in a secret laboratory, the gift of sentience and a heightened sense of intellect was suddenly bestowed upon them. No mouse knows the identity of this mysterious actor or the reason they were given such a boon, but all are grateful for their improved status.",
        };

        Abakhanskya = new RaceTraits()
        {
            BodySize = 200,
            StomachSize = 200,
            HasTail = true,
            FavoredStat = Stat.Endurance,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal },
            ExpMultiplier = 20f,
            PowerAdjustment = 100f,
            DeployCost = 8,
            Upkeep = 100f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(40, 50),
                Dexterity = new RaceStats.StatRange(25, 35),
                Endurance = new RaceStats.StatRange(50, 70),
                Mind = new RaceStats.StatRange(40, 50),
                Will = new RaceStats.StatRange(40, 50),
                Agility = new RaceStats.StatRange(15, 20),
                Voracity = new RaceStats.StatRange(40, 50),
                Stomach = new RaceStats.StatRange(40, 50),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Legendary,
                Traits.ForcefulBlow,
                Traits.Cruel,
                Traits.SlowAbsorption,
                Traits.Ravenous,
        },
            RaceDescription = "Abakhanskya is an ancient dragoness hailing from another realm.  Despite this fact, she has been here for generations upon generations and has quite the body to show for it, absolutely brimming with the nutrients stolen from countless prey throughout the years.  \n<b>She is an unstoppable force of predatory nature, it is unwise to face her on fair terms.</b>",
        };

        MatronsMinions = new RaceTraits()
        {
            BodySize = 8,
            StomachSize = 12,
            HasTail = true,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 2f,
            RacialTraits = new List<Traits>()
            {
                Traits.ProlificBreeder,
                Traits.EasyToVore,
            },
            LeaderRace = Race.Abakhanskya,
            RaceDescription = "A tribe of kobolds who are faithfully serving their goddess, Abakhanskya.",
            RaceAI = RaceAI.ServantRace
        };

        Gnolls = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            DeployCost = 1,
            Upkeep = 4f,
            RacialTraits = new List<Traits>()
            {
                Traits.PackVoracity,
                Traits.SenseWeakness,
                Traits.Biter,
                Traits.TasteForBlood,
            },
            RaceDescription = "A race of brutish and cunning beings hailing from the mountainous regions of the east.  Normally tribal, these hyena-like mammals are prone to in-fighting and cannibalism over petty disputes-- such as whose shadow is larger.  The Gnolls were most often seen around mercenary camps in small packs.  Much to the dismay of the other nations of this world, however, a powerful leader has appeared and managed to amass them into a warlike nation of their own.",
        };

        MainlandElves = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = false,
            FavoredStat = Stat.Will,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.AdeptLearner,
            Traits.MagicProwess,
            Traits.GiantSlayer,
        },
            RaceDescription = "A mix of many races of elves from various regions. Due to their strong diversity, their ideologies are rather similar to the humans instead of any specific elven race.",
        };

        Bears = new RaceTraits()
        {
            BodySize = 15,
            StomachSize = 20,
            HasTail = false,
            FavoredStat = Stat.Endurance,
            DeployCost = 1,
            Upkeep = 4f,
            RacialTraits = new List<Traits>()
        {
            Traits.Intimidating,
            Traits.HardSkin,
            Traits.Berserk,
        },
            RaceDescription = "When Fimbulwinter decended upon Bjornheim, the home realm of the Bears, many thought they were doomed. But then, a gateway formed in the snow coated landscape, and a band of raiders was able to escape the end times of one world by simply moving to another. Now in this realm, they raid and pillage, taking the valuables of other peoples for themselves, while devouring any who fail to fight well enough to ward these ursine vikings away.",
        };

        Eevee = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 12,
            FavoredStat = Stat.Will,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.AdeptLearner,
            Traits.PackWill,
            Traits.Eeveeolutionist,
        },
            RaceDescription = "Not too long ago, a portal opened up to a universe inhabited by various anthro pokémon species. Of the many inhabitants, the Eeveelutions were one of the only groups who came through. While very few Eevee's were brought through directly, all Eeveelutions are born as Eevees, building a local population. After their initial entry into the Realm, the Eeveelutions founded first the \"Eeveelution Empire,\" a police state which quickly collapsed into several smaller groups built around the different Eeveelutions, but Eevees, inherently, make up a significant portion of all Eeveelution nations.",
        };

        Umbreon = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 12,
            FavoredStat = Stat.Strength,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.Intimidating,
            Traits.PackStrength,
            Traits.EscapeArtist,
        },
            ConversionRace = Race.Eevee,
            RaceDescription = "After the fall of the \"Eeveelution Empire\", The Umbreons took the first opportunity they had to attack the Sylveons, Before eventually running off. Unlike most Eeveelutions, They got away with very little resources due to the fact they were too busy filling their bellies with Sylveons to gather anything. So they resorted to taking the weapons and armor from those they defeated and melted it down to form their weapons and armor. Also unlike most Eeveelutions, They deny any resemblance to their feral counter parts. They are also quite intelligent... When they aren't drunk having discovered various forms of alcohol.",
        };

        Lupine = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 20,
            HasTail = false,
            FavoredStat = Stat.Strength,
            DeployCost = 1,
            Upkeep = 4f,
            RacialTraits = new List<Traits>()
        {
            Traits.PackStrength,
            Traits.Pounce,
            Traits.Biter,
        },
            RaceDescription = "Canine people from another world, the Lupines are a curious people, with unique views on vore and the realm they find themselves trapped within. On the whole, they view vore as highly taboo, but they're also seemingly more prone to becoming addicted to it, resulting in a very strange society.",
        };

        Jackals = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 20,
            HasTail = false,
            FavoredStat = Stat.Endurance,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.PackMind,
            Traits.DexterousDefense,
            Traits.Finesse,
        },
            RaceDescription = "A race of desert dwelling canine, in recent times, the constant warfare of the realm has brought them out of their isolation. Where they traditionally stood guard against any fools who sought to plunder their sacred pyramids, now they venture out to practice a doctrine of \"Defense by Offense.\".",
        };

        Ghosts = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 20,
            HasTail = false,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 4f,
            RacialTraits = new List<Traits>()
        {
            Traits.FocusedDodge,
            Traits.ArtfulDodge,
            Traits.LightFrame,
        },
            RaceDescription = "The spooky spirits of the dead, come back in new incorporeal forms to haunt and feast upon the living. Formed when a soul takes five or more years to cross to the other side, these poltergeists, being dead, have no need for nutrition, and hunt for the fun of it and to spite the living.",
        };

        Centaur = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Endurance,
            DeployCost = 1,
            Upkeep = 4f,
            RacialTraits = new List<Traits>()
            {
                Traits.DualStomach,
                Traits.Charge,
                Traits.AwkwardShape,
            },
            RaceDescription = "Half horse, half human!",
        };

        Succubi = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 24,
            HasTail = true,
            FavoredStat = Stat.Will,
            DeployCost = 1,
            Upkeep = 4f,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.Anal, VoreType.TailVore, VoreType.CockVore },
            RacialTraits = new List<Traits>()
            {
                Traits.Dazzle,
                Traits.Flight,
                Traits.EnthrallingDepths,
                Traits.PleasurableTouch,
                Traits.Charmer
            },
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(3, 6),
                Dexterity = new RaceStats.StatRange(8, 14),
                Endurance = new RaceStats.StatRange(8, 14),
                Mind = new RaceStats.StatRange(8, 14),
                Will = new RaceStats.StatRange(12, 20),
                Agility = new RaceStats.StatRange(6, 14),
                Voracity = new RaceStats.StatRange(8, 14),
                Stomach = new RaceStats.StatRange(8, 14),
            },
            RaceDescription = "You know what she is.  A demon from Hell who will suck the life force right out of you.  But, nobody ever said they got their fix exclusively through sex...  There are much more conventional ways to steal life force on this world and the succubi had no trouble adapting to those methods.  Be careful you don't mistake them for their less-hungry/more-lusty siblings from biblical lore lest you end up dissolving away within their bellies or tails...",
            RaceAI = RaceAI.Hedonist,
        };

        Tigers = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 18,
            HasTail = true,
            FavoredStat = Stat.Strength,
            PowerAdjustment = 1.3f,
            DeployCost = 1,
            Upkeep = 7f,
            RacialTraits = new List<Traits>()
        {
            Traits.Maul,
            Traits.Frenzy,
        },
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(16, 24),
                Dexterity = new RaceStats.StatRange(10, 18),
                Endurance = new RaceStats.StatRange(22, 28),
                Mind = new RaceStats.StatRange(12, 20),
                Will = new RaceStats.StatRange(10, 22),
                Agility = new RaceStats.StatRange(18, 28),
                Voracity = new RaceStats.StatRange(12, 20),
                Stomach = new RaceStats.StatRange(12, 18),
            },
            RaceDescription = "Somewhat enigmatic, it is uncertain if the Tigers are native to this realm or came from elsewhere. They do not seem interested in settling down though, joining armies to test their considerable skills in battle instead.",
        };

        Goblins = new RaceTraits()
        {
            BodySize = 8,
            StomachSize = 14,
            HasTail = false,
            FavoredStat = Stat.Mind,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.Clever,
            Traits.Tempered,
            Traits.ArtfulDodge,
            Traits.EscapeArtist,
        },
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(6, 14),
                Dexterity = new RaceStats.StatRange(10, 18),
                Endurance = new RaceStats.StatRange(8, 16),
                Mind = new RaceStats.StatRange(8, 16),
                Will = new RaceStats.StatRange(10, 16),
                Agility = new RaceStats.StatRange(10, 16),
                Voracity = new RaceStats.StatRange(8, 16),
                Stomach = new RaceStats.StatRange(10, 15),
            },
            RaceDescription = "Small and physically unintimidating, the Goblins came from a realm far ahead in technology. Were it not for the lack of materials to replicate their greatest inventions and the small size of their weapons, the Goblins might have claimed the entire land. As it is, they learned to be good at dodging and escaping the maws and guts of predators, through one end or another.",
        };

        Alligators = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Strength,
            CanUseRangedWeapons = false,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.CockVore, VoreType.Unbirth, VoreType.Anal },
            ExpMultiplier = 1.25f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 5f,
            RaceStats = new RaceStats() // Stronger, tougher, slower moving and with slower digestion. (Crocodilians would normally have a very strong digestion, but that reguires focusing on it, not going on fighting.)
            // Wider, shorter throats also make eating easier, but also make prey's escape easier. (Not in RL, obviously. Or perhaps they would, if crocodilians had a habit of swallowing sizeable living prey.)
            {
                Strength = new RaceStats.StatRange(16, 22),
                Dexterity = new RaceStats.StatRange(4, 7),
                Endurance = new RaceStats.StatRange(12, 22),
                Mind = new RaceStats.StatRange(5, 10),
                Will = new RaceStats.StatRange(8, 14),
                Agility = new RaceStats.StatRange(6, 10),
                Voracity = new RaceStats.StatRange(13, 19),
                Stomach = new RaceStats.StatRange(8, 14),
            },
            RacialTraits = new List<Traits>() // Alligator = Lizard+
            {
                Traits.Ravenous, // Bonus to voracity before eating
                Traits.Resilient, // Damage decrease
                Traits.Intimidating, // Penalty to enemies in melee range
				Traits.Crusher,
            },
            RaceDescription = "Natives to great swamps on another dimension, the Alligators emerge sporadically from portals across the land. Either unwilling or unable to settle this realm, they instead work as mercenaries for hire. Large, tough and intimidating, they make great bruisers, but seem totally unable to understand the principle of ranged weapons.",
        };

        Puca = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 12,
            HasTail = true,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 2f,
            RacialTraits = new List<Traits>()
        {
            Traits.ArtfulDodge,
            Traits.Pounce,
        },
            RaceDescription = "A race of burrowers very true to their heritage, the Puca trust their shovels and feet above advanced technology. Many a foe has found themselves swallowed up by their deep dark tunnels.",
        };

        Equaleon = new RaceTraits()
        {
            BodySize = 7,
            StomachSize = 14,
            HasTail = true,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 2f,
            RacialTraits = new List<Traits>()
        {
            Traits.ArtfulDodge,
            Traits.AdeptLearner,
            Traits.PackWill,
            Traits.FastDigestion,
        },
            ConversionRace = Race.Eevee,
            RaceDescription = "After the arrival of the anthro Eeveelutions into this Realm, they have begun to experience a number of new Eeveelutions, seemingly some odd effect this Realm has on how Eevees evolve. Equaleons are one of these new forms, essentially a larger, more powerful Eevee that seemingly is the result of evolving without meeting the criteria for the other Eeveelutions. Given their somewhat smaller population, rather than host a nation of their own, most Equaleons prefer to sell their strength as mercenaries.",
        };

        Kobolds = new RaceTraits()
        {
            BodySize = 8,
            StomachSize = 12,
            HasTail = true,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 2f,
            RacialTraits = new List<Traits>()
            {
                Traits.ProlificBreeder,
                Traits.EasyToVore,
                Traits.Replaceable,
            },
            RaceDescription = "Typical servants to dragon overlords, they slavishly carry out their desires, even to their own detriments.",
            RaceAI = RaceAI.ServantRace
        };

        DewSprite = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 16,
            HasTail = false,
            FavoredStat = Stat.Endurance,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
                Traits.Resilient,
                Traits.IronGut,
                Traits.EnthrallingDepths
        },
            RaceDescription = "Dew Sprites are extremely odd, with most believing them to be some form of corrupted Fairies made of dew, though no one has ever had a long enough conversation to verify this. Obviously, there's the issue of them trying to eat you, but even friendly Dew Sprites are rather unintelligent, and speaking with them can feel like speaking to someone with a concussion.",
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.Anal },
        };

        Hippos = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Endurance,
            PowerAdjustment = 1.3f,
            DeployCost = 1,
            Upkeep = 5f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(12, 20),
                Dexterity = new RaceStats.StatRange(6, 14),
                Endurance = new RaceStats.StatRange(18, 28),
                Mind = new RaceStats.StatRange(6, 12),
                Will = new RaceStats.StatRange(8, 16),
                Agility = new RaceStats.StatRange(8, 14),
                Voracity = new RaceStats.StatRange(12, 20),
                Stomach = new RaceStats.StatRange(12, 18),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.StrongMelee,
                Traits.HardSkin,
                Traits.Crusher,
            },
            RaceDescription = "The Hippos come from the same universe as the Feral Lions, and within that world, they either were or still are the dominant power of that world. Here, they act as mercenaries, believing fully that the portals will someday reopen, and that they'll go home rich.",
        };

        Vipers = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 24,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.BreastVore, VoreType.Anal, VoreType.TailVore },
            PowerAdjustment = 1.4f,
            DeployCost = 1,
            Upkeep = 6f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(10, 16),
                Dexterity = new RaceStats.StatRange(14, 20),
                Endurance = new RaceStats.StatRange(8, 14),
                Mind = new RaceStats.StatRange(10, 16),
                Will = new RaceStats.StatRange(10, 16),
                Agility = new RaceStats.StatRange(14, 20),
                Voracity = new RaceStats.StatRange(18, 28),
                Stomach = new RaceStats.StatRange(14, 20),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.ArtfulDodge,
                Traits.DualStomach,
                Traits.RangedVore,
                Traits.KeenShot,
                Traits.SlowMetabolism,
                Traits.PoisonSpit
            },
            RaceDescription = "A race of shapeshifting snakes hailing from beyond the stars. Free from former masters, they pursue whatever pleasure they can get in this new world.",
        };

        Komodos = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Strength,
            CanUseRangedWeapons = false,
            PowerAdjustment = 1.4f,
            DeployCost = 1,
            Upkeep = 7f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(16, 24),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(14, 24),
                Mind = new RaceStats.StatRange(6, 12),
                Will = new RaceStats.StatRange(8, 16),
                Agility = new RaceStats.StatRange(10, 16),
                Voracity = new RaceStats.StatRange(16, 24),
                Stomach = new RaceStats.StatRange(12, 18),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.Biter,
                Traits.VenomousBite,
                Traits.Intimidating,
                Traits.Resilient,
                Traits.Crusher,
            },
            RaceDescription = "While the frequency with which Komodos and Monitors can be found together would lead you to think them related, in truth, they just happen to have very compatible cultures, and enjoy one another's company. Unlike the Monitors, the Komodos are natives of this world, and as such often act as the \"face\" whenever they are forced to do diplomacy with other groups.",
        };

        Cockatrice = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Mind,
            ExpMultiplier = 1.25f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 4f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(10, 16),
                Dexterity = new RaceStats.StatRange(8, 14),
                Endurance = new RaceStats.StatRange(8, 14),
                Mind = new RaceStats.StatRange(12, 20),
                Will = new RaceStats.StatRange(8, 14),
                Agility = new RaceStats.StatRange(8, 14),
                Voracity = new RaceStats.StatRange(8, 14),
                Stomach = new RaceStats.StatRange(12, 15),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.Intimidating,
                Traits.Petrifier,
            },
            RaceDescription = "Strong and powerful, ever since the collapse of Tricia, the Cockatrice primarily live as mercenaries, selling their considerable might to the highest bidder. While the specifics of what lead to the Cockatrice aren't fully known, they appear to have begun as the hybrid offspring of Harpies and some sort of draconic race, though which one exactly is lost to time.",
        };

        Vargul = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Strength,
            ExpMultiplier = 1.25f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 8f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(14, 20),
                Dexterity = new RaceStats.StatRange(14, 20),
                Endurance = new RaceStats.StatRange(14, 20),
                Mind = new RaceStats.StatRange(8, 16),
                Will = new RaceStats.StatRange(8, 16),
                Agility = new RaceStats.StatRange(14, 20),
                Voracity = new RaceStats.StatRange(14, 20),
                Stomach = new RaceStats.StatRange(12, 18),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.Intimidating,
                Traits.SenseWeakness,
                Traits.StrongGullet,
                Traits.Berserk,
                Traits.Maul,
            },
            RaceDescription = "A long time ago, a Dragon captured a Wolf village. He demanded the women as tribute, and before long, the first Vargul were born. Their draconic father ruled as a tyrant, and the Vargul rebelled and slew their father. Initially, the Varguls remained as protectors. But eventually, according to the Varguls, the Wolves came to fear them, and the Varguls were kicked out, becoming mercenaries. According to the Wolves, the Varguls grew lazy and didn't want to protect the village anymore, and left of their own accord. Most scholars believe the truth is some combination of the two versions of events.",
        };

        Hamsters = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Strength,
            DeployCost = 1,
            Upkeep = 5f,
            PowerAdjustment = 1.4f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(18, 26),
                Dexterity = new RaceStats.StatRange(10, 14),
                Endurance = new RaceStats.StatRange(17, 23),
                Mind = new RaceStats.StatRange(8, 16),
                Will = new RaceStats.StatRange(8, 16),
                Agility = new RaceStats.StatRange(8, 10),
                Voracity = new RaceStats.StatRange(8, 12),
                Stomach = new RaceStats.StatRange(12, 18),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.StrongMelee,
            Traits.PackStrength,
            Traits.Resilient,
            Traits.Biter,
        },
            RaceDescription = "A race renowned for their excellent smithing and startling strength despite their stature. Their settlements have rarely been seen above ground however, they are known to have ginormous kingdoms underground that dwarf most other faction's capitals.",
        };

        RwuMercenaries = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 18,
            HasTail = true,
            FavoredStat = Stat.Dexterity,
            DeployCost = 1,
            Upkeep = 6f,
            PowerAdjustment = 1.1f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(12, 18),
                Dexterity = new RaceStats.StatRange(14, 20),
                Endurance = new RaceStats.StatRange(9, 13),
                Mind = new RaceStats.StatRange(8, 12),
                Will = new RaceStats.StatRange(16, 24),
                Agility = new RaceStats.StatRange(7, 12),
                Voracity = new RaceStats.StatRange(5, 8),
                Stomach = new RaceStats.StatRange(12, 15),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.QuickShooter,
            Traits.ForcefulBlow,
            Traits.PackWill,
            Traits.PackDefense,
            Traits.Respawner,
            Traits.Competitive,
        },
            InnateSpells = new List<SpellTypes>() { SpellTypes.FireBomb },
            RaceDescription = "A highly trained soldier of the Red Wolf United Mercenary Company. It is unknown which faction or race first started this company due to the technology they boast and open recruitment policy. However, one thing is certain; these soldiers are no slouches when it comes to combat and are quite a formidable force on the battlefield.",
        };

        Vagrants = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 18,
            HasTail = false,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.25f,
            PowerAdjustment = 1f,
            DeployCost = 1,
            Upkeep = 4f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(10, 26),
                Dexterity = new RaceStats.StatRange(6, 14),
                Endurance = new RaceStats.StatRange(12, 20),
                Mind = new RaceStats.StatRange(10, 22),
                Will = new RaceStats.StatRange(4, 12),
                Agility = new RaceStats.StatRange(8, 20),
                Voracity = new RaceStats.StatRange(10, 18),
                Stomach = new RaceStats.StatRange(20, 30),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.DoubleAttack,
            Traits.Paralyzer
        },
            RaceDescription = "It is a matter of argument whether these beings emerged from the ocean or fell from the skies, or are even a mix of both, but they are among the first and oldest native threats the people who settled this realm faced. Their many tentacles paralyze those they touch and their rubbery bodies easily expand to engulf their prey.",
        };

        Serpents = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.25f,
            PowerAdjustment = .9f,
            DeployCost = 1,
            Upkeep = 4f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 20),
                Dexterity = new RaceStats.StatRange(6, 14),
                Endurance = new RaceStats.StatRange(10, 18),
                Mind = new RaceStats.StatRange(6, 14),
                Will = new RaceStats.StatRange(4, 8),
                Agility = new RaceStats.StatRange(6, 12),
                Voracity = new RaceStats.StatRange(18, 26),
                Stomach = new RaceStats.StatRange(18, 26),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.SlowAbsorption,
                Traits.Biter,
                Traits.Ravenous,
                Traits.StrongGullet,
            },
            RaceDescription = "When the lizard folk emerged from their portal to this land, some young snakes from their old world managed to slip along. Growing fast under the effect of this new realm, the Serpents soon emerged as a ravenous horde.",
        };

        Wyvern = new RaceTraits()
        {
            BodySize = 30,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.Anal },
            ExpMultiplier = 1.5f,
            PowerAdjustment = 2f,
            DeployCost = 1,
            Upkeep = 6f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 20),
                Dexterity = new RaceStats.StatRange(6, 14),
                Endurance = new RaceStats.StatRange(22, 32),
                Mind = new RaceStats.StatRange(12, 28),
                Will = new RaceStats.StatRange(6, 14),
                Agility = new RaceStats.StatRange(10, 22),
                Voracity = new RaceStats.StatRange(10, 18),
                Stomach = new RaceStats.StatRange(8, 16),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Flight,
        },
            RaceDescription = "Fast, winged and ravenous. These lesser cousins of dragons do not have the magical abilities of true dragons, but they are still a dangerous force. They are often followed by their younger kin, but their care only extends as far as not snacking on the weaklings themselves. ",

        };

        WyvernMatron = new RaceTraits()
        {
            BodySize = 40,
            StomachSize = 40,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.Anal },
            ExpMultiplier = 1.6f,
            PowerAdjustment = 2.1f,
            DeployCost = 1,
            Upkeep = 15f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(12, 30),
                Dexterity = new RaceStats.StatRange(9, 21),
                Endurance = new RaceStats.StatRange(30, 42),
                Mind = new RaceStats.StatRange(21, 44),
                Will = new RaceStats.StatRange(12, 27),
                Agility = new RaceStats.StatRange(16, 33),
                Voracity = new RaceStats.StatRange(15, 27),
                Stomach = new RaceStats.StatRange(28, 39),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Flight,
        },
            RaceDescription = "The beings called Wyvern Matrons are a rare, larger, hermaphroditic variant of the wyvern. Why exactly some wyverns turn into such is unknown, but the resulting being is an even greater threat than average wyverns are.",
        };

        Compy = new RaceTraits()
        {
            BodySize = 3,
            StomachSize = 13,
            HasTail = false,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.CockVore },
            ExpMultiplier = .75f,
            PowerAdjustment = .5f,
            DeployCost = 1,
            Upkeep = 1f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(2, 6),
                Dexterity = new RaceStats.StatRange(2, 4),
                Endurance = new RaceStats.StatRange(6, 10),
                Mind = new RaceStats.StatRange(2, 6),
                Will = new RaceStats.StatRange(4, 8),
                Agility = new RaceStats.StatRange(8, 18),
                Voracity = new RaceStats.StatRange(6, 14),
                Stomach = new RaceStats.StatRange(8, 20),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.ArtfulDodge,
        },
            RaceDescription = "No-one is certain where these tiny beings appeared from, but everyone agrees that they aren't much of a threat, though not for a lack of trying from their part. All travelers should be aware though, a small dinosaur humping your leg likely means there are more nearby.",

        };

        Sharks = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.75f,
            PowerAdjustment = 1.75f,
            DeployCost = 2,
            Upkeep = 8f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 30),
                Dexterity = new RaceStats.StatRange(4, 8),
                Endurance = new RaceStats.StatRange(14, 24),
                Mind = new RaceStats.StatRange(6, 10),
                Will = new RaceStats.StatRange(6, 12),
                Agility = new RaceStats.StatRange(18, 26),
                Voracity = new RaceStats.StatRange(14, 22),
                Stomach = new RaceStats.StatRange(6, 12),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Flight,
                Traits.Biter,
                Traits.Berserk,
                Traits.TasteForBlood,
        },
            RaceDescription = "When the Scylla left their old realm the creatures that used to hunt them were left hungry. The Sharks, with their strong sense of smell, were able to track down the portals the Scylla used and followed close behind.",

        };

        FeralWolves = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Strength,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal },
            ExpMultiplier = 1.75f,
            PowerAdjustment = 1.75f,
            DeployCost = 1,
            Upkeep = 5f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(14, 22),
                Dexterity = new RaceStats.StatRange(9, 13),
                Endurance = new RaceStats.StatRange(17, 26),
                Mind = new RaceStats.StatRange(6, 12),
                Will = new RaceStats.StatRange(12, 18),
                Agility = new RaceStats.StatRange(17, 26),
                Voracity = new RaceStats.StatRange(19, 27),
                Stomach = new RaceStats.StatRange(21, 29),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Biter,
                Traits.PackTactics,
                Traits.Pounce,
        },
            RaceDescription = "Natives of this realm, the wolves were more than happy for a chance to welcome the newcomers to their bellies. While likely related to their bipedal cousins, the ferals only consider them as familiar smelling food.",
        };

        BlackSwallowers = new RaceTraits()
        {
            BodySize = 6,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.5f,
            DeployCost = 1,
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(6, 14),
                Dexterity = new RaceStats.StatRange(4, 8),
                Endurance = new RaceStats.StatRange(10, 16),
                Mind = new RaceStats.StatRange(6, 10),
                Will = new RaceStats.StatRange(8, 14),
                Agility = new RaceStats.StatRange(8, 12),
                Voracity = new RaceStats.StatRange(12, 20),
                Stomach = new RaceStats.StatRange(6, 12),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.Flight,
                Traits.Ravenous
            },
            RaceDescription = "As the Scylla arrived in the new lands they brought some of their pets along. Not a year later the strange properties of the new realm had caused the fish to breed out of control, soon escaping and going wild.",

        };

        Cake = new RaceTraits()
        {
            BodySize = 30,
            StomachSize = 20,
            HasTail = false,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 2.5f,
            PowerAdjustment = 2.5f,
            DeployCost = 1,
            Upkeep = 8f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(6, 12),
                Dexterity = new RaceStats.StatRange(4, 8),
                Endurance = new RaceStats.StatRange(20, 30),
                Mind = new RaceStats.StatRange(4, 10),
                Will = new RaceStats.StatRange(6, 12),
                Agility = new RaceStats.StatRange(6, 10),
                Voracity = new RaceStats.StatRange(10, 18),
                Stomach = new RaceStats.StatRange(8, 16),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.Frenzy,
                Traits.Tasty,
                Traits.SoftBody,
                Traits.SlowDigestion

            },
            RaceDescription = "A wizard baking a cake cut his hand and a drop of blood fell in the batter. His guests arrived while the cake was in the oven, eagerly waiting for their treat. But having got a taste of him, the Cake, once baked, ate the wizard and his guests instead.",

        };

        Harvesters = new RaceTraits()
        {
            BodySize = 30,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.5f,
            PowerAdjustment = 2.2f,
            DeployCost = 1,
            Upkeep = 6f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(12, 22),
                Dexterity = new RaceStats.StatRange(6, 14),
                Endurance = new RaceStats.StatRange(18, 24),
                Mind = new RaceStats.StatRange(6, 10),
                Will = new RaceStats.StatRange(10, 22),
                Agility = new RaceStats.StatRange(18, 28),
                Voracity = new RaceStats.StatRange(18, 24),
                Stomach = new RaceStats.StatRange(10, 14),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.SlowDigestion,
                Traits.Intimidating,
                Traits.BornToMove,
                Traits.NimbleClimber,
                Traits.Crusher,
        },
            RaceDescription = "A lifeform from far beyond the stars, the Harvesters saw the empty lands fill and felt rising hunger. How they made their way here is unknown, but their mission is readily understood. They are here to feed until the land is empty once more.",

        };

        Collectors = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Stomach,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.5f,
            PowerAdjustment = 2.2f,
            DeployCost = 1,
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(11, 20),
                Dexterity = new RaceStats.StatRange(8, 16),
                Endurance = new RaceStats.StatRange(16, 22),
                Mind = new RaceStats.StatRange(6, 12),
                Will = new RaceStats.StatRange(8, 16),
                Agility = new RaceStats.StatRange(12, 20),
                Voracity = new RaceStats.StatRange(14, 18),
                Stomach = new RaceStats.StatRange(16, 24),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.SlowDigestion,
                Traits.SlowAbsorption,
                Traits.BornToMove,
                Traits.NimbleClimber,
        },
            RaceDescription = "These large, long limbed creatures seem to be pets or beasts of burden for the Harvesters. While very capable of hunting on their own, they mostly collect the prey the Harvesters have already brought low, filling their low hanging saggy bellies with dozens of prey.",

        };

        Voilin = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 12,
            HasTail = true,
            FavoredStat = Stat.Stomach,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.1f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 12),
                Dexterity = new RaceStats.StatRange(8, 12),
                Endurance = new RaceStats.StatRange(10, 14),
                Mind = new RaceStats.StatRange(4, 6),
                Will = new RaceStats.StatRange(6, 10),
                Agility = new RaceStats.StatRange(12, 16),
                Voracity = new RaceStats.StatRange(8, 12),
                Stomach = new RaceStats.StatRange(8, 12),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Resilient,
                Traits.Disgusting
        },
            RaceDescription = "This small creature is the basic grunt of the Mass, a disposable, nearly mindless slave to throw at potential prey to tire them down for worthier beings to devour.",
        };

        Bats = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 12,
            HasTail = false,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 1f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 12),
                Dexterity = new RaceStats.StatRange(12, 16),
                Endurance = new RaceStats.StatRange(8, 12),
                Mind = new RaceStats.StatRange(6, 8),
                Will = new RaceStats.StatRange(8, 12),
                Agility = new RaceStats.StatRange(12, 16),
                Voracity = new RaceStats.StatRange(10, 14),
                Stomach = new RaceStats.StatRange(8, 12),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Flight,
                Traits.EvasiveBattler
        },
            RaceDescription = "A species with large difference in size between genders, the male bats being barely half the female's size. This has led many to believe that the tendency of the females to hunt both for sustenance and pleasure is due to the males being unable to satisfy some of the female's needs.",
        };


        Frogs = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 12,
            HasTail = false,
            FavoredStat = Stat.Stomach,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 1f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(12, 16),
                Dexterity = new RaceStats.StatRange(4, 8),
                Endurance = new RaceStats.StatRange(12, 16),
                Mind = new RaceStats.StatRange(6, 8),
                Will = new RaceStats.StatRange(8, 12),
                Agility = new RaceStats.StatRange(12, 16),
                Voracity = new RaceStats.StatRange(10, 14),
                Stomach = new RaceStats.StatRange(8, 12),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.RangedVore,
                Traits.Pounce,
                Traits.HeavyPounce,
                Traits.Clumsy,
        },
            RaceDescription = "You know those stories about kissing a frog to turn it into a prince? A long time ago, a witch grew frustrated that her frog-kissing had failed to produce a prince for her, so she brewed a special potion. After pouring most of it on an innocent little frog and then downing the rest herself, she kissed the frog. In an instant, they became the first Feral Frog, a mindless creature of infinite hunger and lust. If the universe were kind, this would've been the only Feral Frog ever, but Feral Frogs actually possess the ability to reproduce through parthenogenesis."
        };

        Dragons = new RaceTraits()
        {
            BodySize = 100,
            StomachSize = 80,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
            ExpMultiplier = 6f,
            PowerAdjustment = 12f,
            DeployCost = 4,
            Upkeep = 4f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(24, 32),
                Dexterity = new RaceStats.StatRange(10, 18),
                Endurance = new RaceStats.StatRange(30, 42),
                Mind = new RaceStats.StatRange(24, 32),
                Will = new RaceStats.StatRange(12, 24),
                Agility = new RaceStats.StatRange(16, 22),
                Voracity = new RaceStats.StatRange(20, 28),
                Stomach = new RaceStats.StatRange(12, 24),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Flight,
                Traits.Maul,
                Traits.Greedy,
                Traits.Cruel,
                Traits.AdeptLearner,

        },
            RaceDescription = "Among the most feared things in this world, Dragons are creatures of immense power and cruelty, who often delight in tormenting their prey before devouring them. To summarize: Avoid."
        };

        Dragonfly = new RaceTraits()
        {
            BodySize = 9,
            StomachSize = 9,
            HasTail = false,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.4f,
            DeployCost = 1,
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 10),
                Dexterity = new RaceStats.StatRange(15, 20),
                Endurance = new RaceStats.StatRange(8, 10),
                Mind = new RaceStats.StatRange(6, 8),
                Will = new RaceStats.StatRange(8, 12),
                Agility = new RaceStats.StatRange(15, 20),
                Voracity = new RaceStats.StatRange(8, 12),
                Stomach = new RaceStats.StatRange(8, 10),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Flight,
                Traits.Tempered
        },
            RaceDescription = "The ambient energies that abound in this world sometimes cause normal creatures to grow to abnormal sizes. These dragonflies have adapted their diet to suit their new size and abilities, and are a terror to face unprepared.",
        };


        Plants = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 12,
            HasTail = false,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.4f,
            DeployCost = 1,
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(10, 16),
                Dexterity = new RaceStats.StatRange(6, 8),
                Endurance = new RaceStats.StatRange(8, 14),
                Mind = new RaceStats.StatRange(4, 6),
                Will = new RaceStats.StatRange(6, 12),
                Agility = new RaceStats.StatRange(4, 8),
                Voracity = new RaceStats.StatRange(8, 15),
                Stomach = new RaceStats.StatRange(8, 13),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Tempered,
                Traits.SlowDigestion
        },
            RaceDescription = "Strange and enigmatic, the Twisted Vines are, unsurprisingly, not natives of this world, instead emerging during a particularly harsh period where innumerable portals connecting to realms of chaos opened and closed at random. The Twisted Vines are the native people of one of these chaotic planes. Despite what some think, they are sapient, they just generally prefer to live as nomadic bandits, swallowing any unlucky travelers they come across, and then coughing up or passing their valuables."
        };

        Fairies = new RaceTraits()
        {
            BodySize = 7,
            StomachSize = 10,
            HasTail = false,
            FavoredStat = Stat.Mind,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.CockVore, VoreType.Anal },
            ExpMultiplier = 1.1f,
            PowerAdjustment = 1.2f,
            DeployCost = 1,
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(4, 8),
                Dexterity = new RaceStats.StatRange(9, 14),
                Endurance = new RaceStats.StatRange(6, 12),
                Mind = new RaceStats.StatRange(14, 22),
                Will = new RaceStats.StatRange(8, 14),
                Agility = new RaceStats.StatRange(14, 22),
                Voracity = new RaceStats.StatRange(8, 14),
                Stomach = new RaceStats.StatRange(5, 10),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.ArtfulDodge,
                Traits.Flight,
                Traits.EscapeArtist,
                Traits.KeenReflexes,
                Traits.EasyToVore
        },
            RaceDescription = "Fairies are small nature spirits. They spend most of their time \"correcting\" disruptions to the natural order(normally by eating them). Fairies typically spend their down time having sex. Lots of sex. It's part of how they burn off all the extra calories from eating so many people."
        };

        Whisp = new RaceTraits()
        {
            BodySize = 7,
            StomachSize = 10,
            HasTail = false,
            FavoredStat = Stat.Mind,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.CockVore, VoreType.Anal },
            ExpMultiplier = 1.1f,
            PowerAdjustment = 1.2f,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
            {
                Traits.Whispers,
                Traits.ForceFeeder,
                Traits.ForcedMetamorphosis,
                Traits.GreaterChangeling,
            },
            SpawnRace = Race.Youko,
            RaceDescription = "Little more than swirls of chaotic magic with a soul in the middle, Whisps are most often produced as a byproduct of the Youko, though they can also sometimes form on their own. Without a true \"form,\" Whisps instead copy the appearance of their prey in order to eat them. They will also often shove themselves into others in an attempt to take them over from the inside. Without someone to give them commands, they go insane and attack virtually anything they encounter, so it is important to keep them busy."
        };

        FeralAnts = new RaceTraits()
        {
            BodySize = 3,
            StomachSize = 12,
            HasTail = false,
            FavoredStat = Stat.Dexterity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.0f,
            PowerAdjustment = 1.2f,
            DeployCost = 1,
            Upkeep = 1f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(4, 6),
                Dexterity = new RaceStats.StatRange(12, 15),
                Endurance = new RaceStats.StatRange(6, 8),
                Mind = new RaceStats.StatRange(6, 10),
                Will = new RaceStats.StatRange(6, 10),
                Agility = new RaceStats.StatRange(8, 12),
                Voracity = new RaceStats.StatRange(6, 10),
                Stomach = new RaceStats.StatRange(8, 10),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.AcidResistant,
                Traits.BornToMove,
                Traits.SlowDigestion
        },
            RaceDescription = "Tiny insects grown to a slightly larger size, the Ants still wouldn't be considered dangerous were it not for their ability to swallow and carry things hundred times their own size.",
        };

        Gryphons = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 22,
            HasTail = true,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
            ExpMultiplier = 1.75f,
            PowerAdjustment = 2.5f,
            DeployCost = 1,
            Upkeep = 5f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(18, 28),
                Dexterity = new RaceStats.StatRange(8, 16),
                Endurance = new RaceStats.StatRange(12, 16),
                Mind = new RaceStats.StatRange(8, 16),
                Will = new RaceStats.StatRange(8, 16),
                Agility = new RaceStats.StatRange(22, 29),
                Voracity = new RaceStats.StatRange(8, 16),
                Stomach = new RaceStats.StatRange(8, 14),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Flight,
                Traits.Intimidating,
                Traits.Charge,
                Traits.Greedy,
                Traits.Pathfinder,
        },
            RaceDescription = "Originally the hybrid offspring of the Avians and Feral Lions, Gryphons are a proud and shockingly intelligent race, who could easily found an empire of their own if they were able to work together in larger groups. Instead, they prefer to spend most of their time hunting travelers, raiding small settlements, and stalking mountain passes. If you ever find yourself at a mountain pass with a Gryphon telling you to do something before they let you pass, don't bother. They just want something, and will most likely eat you after you're done to avoid the embarrassment of admitting they needed help."
        };

        FeralLions = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
            ExpMultiplier = 1.75f,
            PowerAdjustment = 3f,
            DeployCost = 1,
            Upkeep = 4f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(14, 20),
                Dexterity = new RaceStats.StatRange(8, 16),
                Endurance = new RaceStats.StatRange(12, 20),
                Mind = new RaceStats.StatRange(6, 12),
                Will = new RaceStats.StatRange(12, 18),
                Agility = new RaceStats.StatRange(14, 20),
                Voracity = new RaceStats.StatRange(18, 24),
                Stomach = new RaceStats.StatRange(18, 24),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.Biter,
                Traits.Pounce,
                Traits.Ravenous,
                Traits.TasteForBlood,
                Traits.PleasurableTouch,
            },
            RaceDescription = $"Big hedonistic felines. They were probably following a migration of gazelle before they came upon this land.\nMuch older texts claim they are the children of Raha, another world's godess of pleasure. She spread her blessing to this realm, and in exchange, these kitties are feeling right at home digesting the natives.",
            RaceAI = RaceAI.Hedonist,
        };

        SpitterSlugs = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 20,
            HasTail = false,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(6, 8),
                Dexterity = new RaceStats.StatRange(8, 12),
                Endurance = new RaceStats.StatRange(10, 15),
                Mind = new RaceStats.StatRange(4, 6),
                Will = new RaceStats.StatRange(8, 12),
                Agility = new RaceStats.StatRange(4, 6),
                Voracity = new RaceStats.StatRange(20, 30),
                Stomach = new RaceStats.StatRange(8, 16),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.BoggingSlime,
            Traits.GelatinousBody, // or resilient
            Traits.SoftBody,
            Traits.SlowMovement,
            Traits.GlueBomb
        },
            RaceDescription = "One of the four species of giant slug that can be found in the Realm, the Spitter Slugs are named for their tendency to spew, or spit, a goopy glue-like mucus to trap their prey. While, like all giant slugs, they aren't especially quick, they are noticeably quicker than their cousins, and this should be kept in mind when facing them, since even if you think you're safe, one miscalculation will lead to them glueing you in place, and then swallowing you down."
        };

        SpringSlugs = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 12,
            HasTail = false,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = .9f,
            PowerAdjustment = .9f,
            DeployCost = 1,
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 12),
                Dexterity = new RaceStats.StatRange(6, 8),
                Endurance = new RaceStats.StatRange(8, 12),
                Mind = new RaceStats.StatRange(4, 6),
                Will = new RaceStats.StatRange(8, 12),
                Agility = new RaceStats.StatRange(4, 6),
                Voracity = new RaceStats.StatRange(16, 24),
                Stomach = new RaceStats.StatRange(8, 16),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.BoggingSlime,
            Traits.EasyToVore,
            Traits.Replaceable,
            Traits.Pounce,
            Traits.SoftBody,
            Traits.SlowMovement
        },
            RaceDescription = "One of the four species of giant slug that can be found in the Realm, the Spring Slugs are rather weak compared to the other giant slugs, though they do have one noteworthy trick; by building up tension in a special organ inside their body and then releasing, the Spring Slugs can fling, or spring(hence the name), themselves across a battlefield. If you notice one arching its \"back,\" get ready to dodge."
        };

        RockSlugs = new RaceTraits()
        {
            BodySize = 32,
            StomachSize = 50,
            HasTail = false,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 2.5f,
            PowerAdjustment = 3.0f,
            DeployCost = 1,
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(16, 24),
                Dexterity = new RaceStats.StatRange(6, 8),
                Endurance = new RaceStats.StatRange(32, 40),
                Mind = new RaceStats.StatRange(4, 6),
                Will = new RaceStats.StatRange(8, 12),
                Agility = new RaceStats.StatRange(4, 6),
                Voracity = new RaceStats.StatRange(32, 40),
                Stomach = new RaceStats.StatRange(12, 24),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Resilient,
            Traits.SoftBody,
            Traits.VerySlowMovement,
            Traits.HardSkin
        },
            RaceDescription = "One of the four species of giant slug that can be found in the Realm, the Rock Slugs are known for their high resilience, being able to withstand some of the strongest beasts in the Realm. They would be far more difficult to deal with were it not for their speed, which makes the other already slow species of giant slug look like speed demons in comparasine."
        };

        CoralSlugs = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 16,
            HasTail = false,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 5f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 12),
                Dexterity = new RaceStats.StatRange(6, 8),
                Endurance = new RaceStats.StatRange(10, 15),
                Mind = new RaceStats.StatRange(4, 6),
                Will = new RaceStats.StatRange(8, 12),
                Agility = new RaceStats.StatRange(4, 6),
                Voracity = new RaceStats.StatRange(16, 24),
                Stomach = new RaceStats.StatRange(8, 16),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Paralyzer,
            Traits.Stinger,
            Traits.GelatinousBody, // or resilient
            Traits.SoftBody,
            Traits.SlowMovement,
            Traits.Toxic
        },
            InnateSpells = new List<SpellTypes>() { SpellTypes.Poison },
            RaceDescription = "One of the four species of giant slug that can be found in the Realm, the Coral Slugs are mostly known for their unique appearance compared to the other slugs and their highly toxic bodies and stingers. Like all giant slugs, they are rather slow, but that makes them no less dangerous."
        };

        Salamanders = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 18,
            HasTail = true,
            FavoredStat = Stat.Mind,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(10, 16),
                Dexterity = new RaceStats.StatRange(8, 14),
                Endurance = new RaceStats.StatRange(8, 14),
                Mind = new RaceStats.StatRange(12, 20),
                Will = new RaceStats.StatRange(8, 16),
                Agility = new RaceStats.StatRange(8, 14),
                Voracity = new RaceStats.StatRange(12, 20),
                Stomach = new RaceStats.StatRange(10, 16),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Biter,
            Traits.HotBlooded
        },
            InnateSpells = new List<SpellTypes>() { SpellTypes.Fireball },
            RaceDescription = "The fire on the tail isn't for show. Despite being dumber than most plants, Salamanders are shockingly good at claiming prey. Avoid if you don't want to become fat on a creature that can't tell its own reflection from another Salamander."
        };

        Mantis = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 20,
            HasTail = false,
            FavoredStat = Stat.Strength,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 6f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 28),
                Dexterity = new RaceStats.StatRange(12, 20),
                Endurance = new RaceStats.StatRange(8, 12),
                Mind = new RaceStats.StatRange(6, 10),
                Will = new RaceStats.StatRange(6, 10),
                Agility = new RaceStats.StatRange(18, 26),
                Voracity = new RaceStats.StatRange(6, 10),
                Stomach = new RaceStats.StatRange(6, 10),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Pounce,
            Traits.SenseWeakness,
            Traits.BladeDance,
            Traits.LightFrame
        },
            RaceDescription = "Despite being native to the warmer, wetter regions of this world, the Mantises can be seen virtually anywhere, and they are not a good sight. In battle, they tend to leap around unpredictably, and those scythe arms aren't just for show. While it's best to avoid them, if battle is inevitable, it's imperative that you maintain as much distance as possible."
        };

        EasternDragon = new RaceTraits()
        {
            BodySize = 80,
            StomachSize = 80,
            HasTail = true,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.CockVore, VoreType.Unbirth },
            ExpMultiplier = 1.6f,
            PowerAdjustment = 1.9f,
            DeployCost = 2,
            Upkeep = 10f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(12, 22),
                Dexterity = new RaceStats.StatRange(8, 16),
                Endurance = new RaceStats.StatRange(22, 23),
                Mind = new RaceStats.StatRange(14, 30),
                Will = new RaceStats.StatRange(8, 18),
                Agility = new RaceStats.StatRange(14, 28),
                Voracity = new RaceStats.StatRange(22, 32),
                Stomach = new RaceStats.StatRange(32, 40),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Flight,
                Traits.Ravenous,
                Traits.AdeptLearner,
                Traits.StrongGullet,
                Traits.Maul,
        },
            InnateSpells = new List<SpellTypes>() { SpellTypes.Fireball },
            RaceDescription = "A variety of dragons especially attuned to magic, the Eastern Dragons, or Lung Dragons as they are also known as, are able to fly without wings. Reminiscent of snakes, the Eastern Dragons are readily able to prove that the resemblance is more than skin deep, devouring large prey with ease.",

        };

        Catfish = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 16,
            HasTail = true,
            FavoredStat = Stat.Stomach,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 5f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 12),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(16, 24),
                Mind = new RaceStats.StatRange(8, 12),
                Will = new RaceStats.StatRange(8, 12),
                Agility = new RaceStats.StatRange(10, 16),
                Voracity = new RaceStats.StatRange(20, 28),
                Stomach = new RaceStats.StatRange(12, 20),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Slippery,
            Traits.Ravenous,
            Traits.Nauseous,
            Traits.SlowDigestion
        },
            RaceDescription = "These large and incredibly stupid fish long ago figured out that much larger and tastier \"food\"(people) exists on land, and so they figured out how to haphazardly leap, slide, and slither on land, adapting their gills into pseudo-lungs capable of breathing air. With the much larger meals available on land, they've been able to get much larger, though this has only increased their appetite."
        };

        Raptor = new RaceTraits()
        {
            BodySize = 6,
            StomachSize = 12,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.CockVore },
            ExpMultiplier = .85f,
            PowerAdjustment = .75f,
            DeployCost = 1,
            Upkeep = 1f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(3, 7),
                Dexterity = new RaceStats.StatRange(3, 5),
                Endurance = new RaceStats.StatRange(6, 10),
                Mind = new RaceStats.StatRange(5, 8),
                Will = new RaceStats.StatRange(5, 8),
                Agility = new RaceStats.StatRange(8, 16),
                Voracity = new RaceStats.StatRange(6, 14),
                Stomach = new RaceStats.StatRange(8, 20),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.ArtfulDodge,
                Traits.Pounce,
                Traits.SlowDigestion
        },
            RaceDescription = "Bigger cousins of the Compy, these rarer creatures often appear in smaller numbers among their lesser kin. While still relatively harmless compared to most monsters, the Raptors are at the edge of being a real danger to unprepared travelers, not least because they are at times known to be clever.",

        };

        WarriorAnts = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 12,
            HasTail = false,
            FavoredStat = Stat.Strength,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.1f,
            PowerAdjustment = 1.3f,
            DeployCost = 1,
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 12),
                Dexterity = new RaceStats.StatRange(12, 15),
                Endurance = new RaceStats.StatRange(8, 12),
                Mind = new RaceStats.StatRange(16, 20),
                Will = new RaceStats.StatRange(6, 10),
                Agility = new RaceStats.StatRange(8, 12),
                Voracity = new RaceStats.StatRange(8, 12),
                Stomach = new RaceStats.StatRange(8, 12),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.AcidResistant,
                Traits.PackStrength,
                Traits.SlowDigestion
        },
            RaceDescription = "The Warrior Ants are specialized members of the Feral Ant species, whose pheromones control and coordinate their lesser kin. When in the presence of the larger Ants, they are overwhelmed by the much stronger pheromones they exude, and join them without second thought."
        };

        Gazelle = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 12,
            HasTail = true,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
            ExpMultiplier = 1.1f,
            PowerAdjustment = 1.3f,
            DeployCost = 1,
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 12),
                Dexterity = new RaceStats.StatRange(10, 16),
                Endurance = new RaceStats.StatRange(10, 16),
                Mind = new RaceStats.StatRange(6, 10),
                Will = new RaceStats.StatRange(6, 10),
                Agility = new RaceStats.StatRange(20, 30),
                Voracity = new RaceStats.StatRange(10, 16),
                Stomach = new RaceStats.StatRange(10, 16),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Charge,
            Traits.ForcefulBlow,
        },
            RaceDescription = "Gazelles are native to the same universe as the Feral Lions. Despite first impressions, Gazelles are sapient, and actually have a rich storytelling tradition. They came through the portals, hoping to be free of the Feral Lions, only to get trapped in a world with things far worse than the Feral Lions and where the Gazelles' lack of hands is very important. Also, the Feral Lions followed them in, rendering it a moot point anyway."
        };

        Earthworms = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 20,
            HasTail = false,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.0f,
            PowerAdjustment = 1.0f,
            DeployCost = 1,
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 28),
                Dexterity = new RaceStats.StatRange(8, 12),
                Endurance = new RaceStats.StatRange(10, 16),
                Mind = new RaceStats.StatRange(6, 10),
                Will = new RaceStats.StatRange(6, 10),
                Agility = new RaceStats.StatRange(10, 16),
                Voracity = new RaceStats.StatRange(20, 28),
                Stomach = new RaceStats.StatRange(16, 24),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.EasyToVore,
            Traits.SteadyStomach,
            Traits.AllOutFirstStrike
        },
            RaceDescription = "Long slimy invertebrates, the giant Earthworms of this world dig and burrow through the upper layers of soil, following the vibrations of those aboveground. Once they are close enough, they burst from the ground in an attempt to disable their prey before they can act. While this is no false strike, and can definitely wound or even kill, if it's survived, the Earthworm has few means to truly threaten people beyond this. When an Earthworm is sufficiently sated, they typically dig back down to rest until they need food once more."
        };

        FeralLizards = new RaceTraits()
        {
            BodySize = 17,
            StomachSize = 16,
            HasTail = true,
            FavoredStat = Stat.Strength,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
            ExpMultiplier = 1.5f,
            PowerAdjustment = 1.75f,
            DeployCost = 1,
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(12, 20),
                Dexterity = new RaceStats.StatRange(8, 16),
                Endurance = new RaceStats.StatRange(10, 18),
                Mind = new RaceStats.StatRange(8, 16),
                Will = new RaceStats.StatRange(8, 16),
                Agility = new RaceStats.StatRange(8, 16),
                Voracity = new RaceStats.StatRange(10, 18),
                Stomach = new RaceStats.StatRange(8, 16),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Intimidating,
            Traits.Biter,
            Traits.Resilient
        },
            RaceDescription = "Hailing from the wetter, warmer parts of this world, the Feral Lizards are common predators. It is widely understood that the Lizards evolved from the Feral Lizards. Despite no actual relation, their reptilian nature makes them vaguely resemble dragons, and as such many cultures view Feral Lizards as a bad omen."
        };

        Monitors = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
            PowerAdjustment = 1.3f,
            DeployCost = 1,
            Upkeep = 6f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(12, 20),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(20, 28),
                Mind = new RaceStats.StatRange(6, 12),
                Will = new RaceStats.StatRange(8, 16),
                Agility = new RaceStats.StatRange(10, 16),
                Voracity = new RaceStats.StatRange(16, 24),
                Stomach = new RaceStats.StatRange(12, 18),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.HardSkin,
                Traits.Resilient,
                Traits.VenomShock,
            },
            RaceDescription = "While the frequency with which Monitors and Komodos can be found together would lead you to think them related, in truth, they just happen to have very compatible cultures, and enjoy one another's company. Unlike the Komodos, the Monitors are not natives of this world, instead hailing from the Feral Lion home dimension.",
        };

        Schiwardez = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 10,
            HasTail = true,
            FavoredStat = Stat.Endurance,
            AllowedVoreTypes = new List<VoreType> { VoreType.CockVore },
            ExpMultiplier = 1.3f,
            PowerAdjustment = 1.6f,
            DeployCost = 1,
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(10, 14),
                Dexterity = new RaceStats.StatRange(8, 12),
                Endurance = new RaceStats.StatRange(12, 16),
                Mind = new RaceStats.StatRange(4, 6),
                Will = new RaceStats.StatRange(8, 12),
                Agility = new RaceStats.StatRange(10, 14),
                Voracity = new RaceStats.StatRange(20, 24),
                Stomach = new RaceStats.StatRange(20, 24),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Resilient,
                Traits.Disgusting,
                Traits.Ravenous,
        },
            RaceDescription = "A tough, twisted creature. Hunts for pleasure rather than sustenance.",
        };

        Terrorbirds = new RaceTraits()
        {
            BodySize = 18,
            StomachSize = 18,
            HasTail = true,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.TailVore, VoreType.Unbirth },
            ExpMultiplier = 1.5f,
            PowerAdjustment = 1.75f,
            DeployCost = 1,
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(12, 20),
                Dexterity = new RaceStats.StatRange(8, 16),
                Endurance = new RaceStats.StatRange(8, 16),
                Mind = new RaceStats.StatRange(6, 12),
                Will = new RaceStats.StatRange(6, 12),
                Agility = new RaceStats.StatRange(12, 20),
                Voracity = new RaceStats.StatRange(10, 18),
                Stomach = new RaceStats.StatRange(8, 16),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Intimidating,
                Traits.ArtfulDodge,
                Traits.Tenacious
        },
            RaceDescription = "Terrorbirds are highly aggressive, and possess high strength. While they aren't particularly clever, they are able to use brute force to great effect."
        };

        Dratopyr = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.CockVore, VoreType.Oral, VoreType.Unbirth },
            ExpMultiplier = .95f,
            PowerAdjustment = .95f,
            DeployCost = 1,
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 12),
                Dexterity = new RaceStats.StatRange(6, 8),
                Endurance = new RaceStats.StatRange(8, 12),
                Mind = new RaceStats.StatRange(7, 9),
                Will = new RaceStats.StatRange(10, 15),
                Agility = new RaceStats.StatRange(9, 17),
                Voracity = new RaceStats.StatRange(8, 14),
                Stomach = new RaceStats.StatRange(8, 16),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.ArtfulDodge,
                Traits.Flight,
                Traits.Cruel
        },
            RaceDescription = "With an appearance reminiscent of a reptilian bat, the Dratopyr are likely a hybrid race. Smaller than most monsters but just as fierce, the Dratopyr specialize in weakening their prey while avoiding attempts to fend them off. Dratopyr are very fast breeders and would thus be a major threat to everyone, were it not for their tendency toward cannibalism.",
        };

        FeralHorses = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
            ExpMultiplier = 1.1f,
            PowerAdjustment = 1.3f,
            DeployCost = 1,
            Upkeep = 4f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 28),
                Dexterity = new RaceStats.StatRange(10, 16),
                Endurance = new RaceStats.StatRange(16, 20),
                Mind = new RaceStats.StatRange(6, 10),
                Will = new RaceStats.StatRange(6, 10),
                Agility = new RaceStats.StatRange(16, 24),
                Voracity = new RaceStats.StatRange(10, 16),
                Stomach = new RaceStats.StatRange(10, 16),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Charge,
            Traits.ForcefulBlow,
            Traits.BornToMove,

        },
            RaceDescription = "It's a horse!  Go ahead, try to ride one.  We dare you!",
        };

        FeralFox = new RaceTraits()
        {
            BodySize = 16,
            StomachSize = 24,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.Anal },
            ExpMultiplier = 1.6f,
            PowerAdjustment = 1.3f,
            DeployCost = 1,
            Upkeep = 4f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(11, 15),
                Dexterity = new RaceStats.StatRange(9, 13),
                Endurance = new RaceStats.StatRange(17, 23),
                Mind = new RaceStats.StatRange(7, 11),
                Will = new RaceStats.StatRange(12, 18),
                Agility = new RaceStats.StatRange(13, 19),
                Voracity = new RaceStats.StatRange(19, 27),
                Stomach = new RaceStats.StatRange(21, 29),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Pounce,
            Traits.Ravenous,
        },
            RaceDescription = "Abnormally large foxes with a voracious appetite.",
        };

        Terminid = new RaceTraits()
        {
            BodySize = 16,
            StomachSize = 24,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.6f,
            PowerAdjustment = 1.3f,
            DeployCost = 1,
            Upkeep = 4f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(11, 15),
                Dexterity = new RaceStats.StatRange(9, 13),
                Endurance = new RaceStats.StatRange(18, 24),
                Mind = new RaceStats.StatRange(7, 11),
                Will = new RaceStats.StatRange(12, 18),
                Agility = new RaceStats.StatRange(13, 19),
                Voracity = new RaceStats.StatRange(19, 27),
                Stomach = new RaceStats.StatRange(21, 29),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Disgusting,
            Traits.Pounce,
            Traits.FireVulnerable,
        },
            RaceDescription = "A vicious and territorial race of voracious insects prized for their ability to produce an element known as E-710, though some say it's just oil.  They are very dangerous, especially so in larger numbers and will often attempt to swarm their prey.  No known portal signified their arrival in the realm so how they ended up here is anybody's guess, though a few conspiracy rumors claim they were brought here by someone else.  There is a curious phenomenon surrounding these insects: When struck by them in combat, there is a high likelyhood that the wounded will shout \"No pain, no freedom!\"",
        };

        FeralOrcas = new RaceTraits()
        {
            BodySize = 30,
            StomachSize = 30,
            FavoredStat = Stat.Strength,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.6f,
            PowerAdjustment = 2.1f,
            DeployCost = 2,
            Upkeep = 9f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(9, 27),
                Dexterity = new RaceStats.StatRange(7, 12),
                Endurance = new RaceStats.StatRange(17, 29),
                Mind = new RaceStats.StatRange(14, 28),
                Will = new RaceStats.StatRange(15, 21),
                Agility = new RaceStats.StatRange(13, 23),
                Voracity = new RaceStats.StatRange(16, 22),
                Stomach = new RaceStats.StatRange(19, 27),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Flight,
                Traits.WaterWalker,
                Traits.PackTactics,
                Traits.StrongGullet,
                Traits.BornToMove,
        },
            RaceDescription = "Orcas that have mutated to fly and breathe air on their homewrold. They've also developed a ravenous appetite.  The Skysharks followed the Scylla to this world, and the Orcas followed the Skysharks.",
        };

        ViraeUltimae = new RaceTraits()
        {
            BodySize = 6,
            StomachSize = 15,
            HasTail = false,
            FavoredStat = Stat.Strength,
            DeployCost = 1,
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(18, 24),
                Dexterity = new RaceStats.StatRange(4, 7),
                Endurance = new RaceStats.StatRange(17, 20),
                Mind = new RaceStats.StatRange(1, 2),
                Will = new RaceStats.StatRange(4, 8),
                Agility = new RaceStats.StatRange(4, 6),
                Voracity = new RaceStats.StatRange(12, 15),
                Stomach = new RaceStats.StatRange(12, 15),
            },
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            RacialTraits = new List<Traits>()
        {
                Traits.ViralBiology,
                Traits.Fearless,
                Traits.Stinger,
                Traits.InfectiousReproduction,
                Traits.DireInfection,
                Traits.Brainless,
        },
            RaceDescription = "How the Virae Ultimae are able to function at all with no brain is an enigma. Hearing and perpetual humming are their only ways of interacting with the world minus attacking and attempting to reproduce more of themselves by infecting hosts with their viral injectors. These things operate as brainless biological automata, and their sheer refusal to cease their attack under any conditions can be quite scary.",
        };

        Viisels = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 17,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.CockVore, VoreType.Unbirth },
            ExpMultiplier = 1f,
            PowerAdjustment = 1f,
            DeployCost = 1,
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(5, 8),
                Dexterity = new RaceStats.StatRange(5, 8),
                Endurance = new RaceStats.StatRange(7, 11),
                Mind = new RaceStats.StatRange(8, 12),
                Will = new RaceStats.StatRange(6, 13),
                Agility = new RaceStats.StatRange(15, 18),
                Voracity = new RaceStats.StatRange(12, 15),
                Stomach = new RaceStats.StatRange(12, 15),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.ArtfulDodge,
                Traits.StretchyInsides,
                Traits.Ravenous,
                Traits.EasyToVore,
        },
            RaceDescription = "The Viisels, after first arriving in this world, used to live in burrows and only hunted those who got too close. But after one of their own, Ki, proved himself a capable battler, they've been forced onto the offensive as the people of the world are no longer willing to leave them be, though as the Viisels have gained a taste for other people, the small sapients grow increasingly bold... And hungry.",
        };

        Goodra = new RaceTraits()
        {
            BodySize = 32,
            StomachSize = 50,
            HasTail = true,
            FavoredStat = Stat.Stomach,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 6f,
            PowerAdjustment = 12f,
            DeployCost = 4,
            Upkeep = 10f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 28),
                Dexterity = new RaceStats.StatRange(12, 18),
                Endurance = new RaceStats.StatRange(36, 44),
                Mind = new RaceStats.StatRange(18, 24),
                Will = new RaceStats.StatRange(22, 28),
                Agility = new RaceStats.StatRange(10, 16),
                Voracity = new RaceStats.StatRange(22, 30),
                Stomach = new RaceStats.StatRange(36, 46),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.BoggingSlime,
            Traits.FriendlyStomach,
            Traits.Resilient,
            Traits.SoftBody,
            Traits.VerySlowMovement,
            Traits.HardSkin
        },
            RaceDescription = "Goodra, the Slug Dragon Pokemon. Goodra are large soft dragon type pokemon coated in slime. They love to give hugs and often confuse friends from food.",
        };

        FeralEevee = new RaceTraits()
        {
            BodySize = 5,
            StomachSize = 10,
            HasTail = true,
            FavoredStat = Stat.Will,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth },
            ExpMultiplier = 1f,
            PowerAdjustment = .75f,
            DeployCost = 1,
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(6, 10),
                Dexterity = new RaceStats.StatRange(2, 4),
                Endurance = new RaceStats.StatRange(5, 8),
                Mind = new RaceStats.StatRange(6, 8),
                Will = new RaceStats.StatRange(4, 8),
                Agility = new RaceStats.StatRange(10, 18),
                Voracity = new RaceStats.StatRange(10, 18),
                Stomach = new RaceStats.StatRange(8, 15),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.AdeptLearner,
                Traits.ArtfulDodge,
                Traits.PackWill,
                Traits.Timid,
                Traits.Eeveeolutionist,
        },
            RaceDescription = "Eevee, the Evolution Pokémon. Small, generally friendly and eager to play, though they can often get over eager in their playing, especially if they're hungry. While their cute, fluffy appearance may suggest a slightly less resilient creature, they are quite adaptable, and under the right conditions can evolve into a number of more specialized forms.",

        };

        FeralUmbreon = new RaceTraits()
        {
            BodySize = 5,
            StomachSize = 10,
            HasTail = true,
            FavoredStat = Stat.Strength,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth },
            ExpMultiplier = 1f,
            PowerAdjustment = .75f,
            DeployCost = 1,
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(6, 10),
                Dexterity = new RaceStats.StatRange(2, 4),
                Endurance = new RaceStats.StatRange(5, 8),
                Mind = new RaceStats.StatRange(6, 8),
                Will = new RaceStats.StatRange(4, 8),
                Agility = new RaceStats.StatRange(10, 18),
                Voracity = new RaceStats.StatRange(10, 18),
                Stomach = new RaceStats.StatRange(8, 15),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Intimidating,
                Traits.NightEye,
                Traits.Pounce,
                Traits.PackStrength,
        },
            ConversionRace = Race.FeralEevee,
            RaceDescription = "Umbreon, the Moonlight Pokémon. A medium sized canine, Feral Umbreons also seemed to come from the same place as Feral Eevees, Also gaining the same hunger Feral Eevees acquired. They will mostly live in packs with other Feral Umbreons and some Feral Eevees. Some claim they have even seen them adopting the Feral Eevees anthro counter part, mistaking it as one of their own. But this has yet to be proven true. Feral Umbreons, like their anthro counter part, share their hatred for Sylveons. Fights between Feral Umbreons and Feral Sylveons have been recorded several times.",

        };

        FeralEqualeon = new RaceTraits()
        {
            BodySize = 5,
            StomachSize = 10,
            HasTail = true,
            FavoredStat = Stat.Will,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth },
            ExpMultiplier = 1f,
            PowerAdjustment = .75f,
            DeployCost = 1,
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(6, 10),
                Dexterity = new RaceStats.StatRange(2, 4),
                Endurance = new RaceStats.StatRange(5, 8),
                Mind = new RaceStats.StatRange(6, 8),
                Will = new RaceStats.StatRange(4, 8),
                Agility = new RaceStats.StatRange(10, 18),
                Voracity = new RaceStats.StatRange(10, 18),
                Stomach = new RaceStats.StatRange(8, 15),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.AdeptLearner,
                Traits.ArtfulDodge,
                Traits.PackWill,
        },
            ConversionRace = Race.FeralEevee,
            RaceDescription = "Equaleon, the Equalized Pokémon. Evolving from Eevee under unique conditions, Equaleon represents a unique form of eeveelution that retains for more of the traits of Eevee than normal, simply scaled up to a larger size and power. They are often just as friendly as Eevees, but a lot better at not accidentally eating those they're playing with. Instead, they mostly consume anything they deem a threat to themselves or the ones they care about. But so long as you aren't either of those, they're actually quite playful and nice to be around. They're seemingly unique to this Realm.",

        };

        FeralSlime = new RaceTraits()
        {
            BodySize = 7,
            StomachSize = 15,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            FavoredStat = Stat.Endurance,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(6, 10),
                Dexterity = new RaceStats.StatRange(2, 4),
                Endurance = new RaceStats.StatRange(10, 15),
                Mind = new RaceStats.StatRange(6, 8),
                Will = new RaceStats.StatRange(4, 8),
                Agility = new RaceStats.StatRange(5, 8),
                Voracity = new RaceStats.StatRange(10, 18),
                Stomach = new RaceStats.StatRange(10, 15),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.BoggingSlime,
                Traits.SoftBody,
                Traits.GelatinousBody,

            },
            RaceDescription = "One of the most basic monsters. The humble slime pursues anything that moves. They may or may not taste like assorted friuts.",
        };

        BoomBunnies = new RaceTraits()
        {
            BodySize = 5,
            StomachSize = 10,
            HasTail = true,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth },
            ExpMultiplier = 6f,
            PowerAdjustment = .75f,
            DeployCost = 1,
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(6, 10),
                Dexterity = new RaceStats.StatRange(2, 4),
                Endurance = new RaceStats.StatRange(5, 8),
                Mind = new RaceStats.StatRange(6, 8),
                Will = new RaceStats.StatRange(4, 8),
                Agility = new RaceStats.StatRange(10, 18),
                Voracity = new RaceStats.StatRange(10, 18),
                Stomach = new RaceStats.StatRange(8, 15),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Replaceable,
                Traits.ProlificBreeder,
                Traits.Pounce,
        },
            InnateSpells = new List<SpellTypes>() { SpellTypes.ExplosiveHug },
            RaceDescription = "Exploding rabbits of unknown origin. Many believe that Boom Bunnies are the result of some science experiment left unchecked. Despite their tendency of exploding they are exceedingly friendly and benign once tamed.",

        };

        WoodDryad = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 16,
            HasTail = true,
            FavoredStat = Stat.Endurance,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.BreastVore },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 4f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(10, 14),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(24, 27),
                Mind = new RaceStats.StatRange(8, 12),
                Will = new RaceStats.StatRange(8, 12),
                Agility = new RaceStats.StatRange(10, 16),
                Voracity = new RaceStats.StatRange(15, 20),
                Stomach = new RaceStats.StatRange(12, 20),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Resilient,
            Traits.VerySlowMovement,
            Traits.SlowDigestion,
            Traits.BlessingOfNature,
        },
            RaceDescription = "Devoted protectors of plants and trees, these Dryads are living incarnations of the plants all around them. They prefer to remain rooted in place, their natural magics healing and soothing the natural world around them, but they often move from place to place to find those who defile nature, and turn them to mulch.\r\n"
        };

        EarthDryad = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 16,
            HasTail = true,
            FavoredStat = Stat.Endurance,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.BreastVore },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 4f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 12),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(20, 24),
                Mind = new RaceStats.StatRange(15, 20),
                Will = new RaceStats.StatRange(17, 22),
                Agility = new RaceStats.StatRange(10, 16),
                Voracity = new RaceStats.StatRange(10, 15),
                Stomach = new RaceStats.StatRange(12, 20),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.BoggingSlime,
            Traits.WeaponChanneler,
            Traits.BlessingOfEarth,
            Traits.SlowMovement
        },
            RaceDescription = "Living incarnations of the land itself, Earth Dryads are both gentle and hardy, showing mercy to those who respect the soil, and adding those don't to the soil. They often employ stone spikes to attack those they deem a threat to the natural world. Touching one is generally not a good idea."
        };

        RiverDryad = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 16,
            HasTail = true,
            FavoredStat = Stat.Mind,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.BreastVore },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 4f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 12),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(16, 24),
                Mind = new RaceStats.StatRange(8, 12),
                Will = new RaceStats.StatRange(8, 12),
                Agility = new RaceStats.StatRange(10, 16),
                Voracity = new RaceStats.StatRange(20, 28),
                Stomach = new RaceStats.StatRange(12, 20),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Slippery,
            Traits.BlessingOfWater,
            Traits.ManaRich,
            Traits.WeaponChanneler,
        },
            RaceDescription = "Playful and curious, River Dryads are the living incarnations of clear water. They spend much of their time guiding rivers and streams, helping to keep nature watered. While they can seem friendly, be warned, they possess the same power of the rushing rapids."
        };

        FungalDryad = new RaceTraits()
        {
            BodySize = 15,
            StomachSize = 16,
            HasTail = true,
            FavoredStat = Stat.Strength,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.BreastVore },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 5f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(18, 24),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(13, 18),
                Mind = new RaceStats.StatRange(8, 12),
                Will = new RaceStats.StatRange(8, 12),
                Agility = new RaceStats.StatRange(10, 16),
                Voracity = new RaceStats.StatRange(20, 28),
                Stomach = new RaceStats.StatRange(12, 20),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.StrongMelee,
            Traits.Frenzy,
            Traits.MetabolicSurge,
            Traits.BlessingOfFerocity
        },
            RaceDescription = "When a Wood Dryad dies, sometimes their spirit fails to leave, and they are reborn in a new, fungal form. Any mercy they may once have had is gone, their deceptively sharp claws cutting through any that dare try to stand up to the mushroom menace. These erratic Dryads are always eager to showing those they find their favorite part of the life cycle.\r\n"
        };

        Otachi = new RaceTraits()
        {
            BodySize = 80,
            StomachSize = 80,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.CockVore, VoreType.Unbirth },
            ExpMultiplier = 6f,
            PowerAdjustment = 1.3f,
            DeployCost = 2,
            Upkeep = 20f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 28),
                Dexterity = new RaceStats.StatRange(14, 20),
                Endurance = new RaceStats.StatRange(20, 30),
                Mind = new RaceStats.StatRange(10, 20),
                Will = new RaceStats.StatRange(10, 20),
                Agility = new RaceStats.StatRange(20, 28),
                Voracity = new RaceStats.StatRange(40, 50),
                Stomach = new RaceStats.StatRange(30, 40),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.TailStrike,
            Traits.Flight,
            Traits.HealingBlood,
            Traits.PoisonSpit,
        },
            //InnateSpells = new List<SpellTypes>() { SpellTypes.Poison },
            RaceDescription = "Somehow a Kaiju, with poison!",
        };

        Raiju = new RaceTraits()
        {
            BodySize = 60,
            StomachSize = 50,
            FavoredStat = Stat.Endurance,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.CockVore, VoreType.Unbirth },
            ExpMultiplier = 6f,
            PowerAdjustment = 1.3f,
            DeployCost = 2,
            Upkeep = 20f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 28),
                Dexterity = new RaceStats.StatRange(14, 20),
                Endurance = new RaceStats.StatRange(20, 30),
                Mind = new RaceStats.StatRange(10, 20),
                Will = new RaceStats.StatRange(10, 20),
                Agility = new RaceStats.StatRange(20, 28),
                Voracity = new RaceStats.StatRange(40, 50),
                Stomach = new RaceStats.StatRange(30, 40),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Intimidating,
            Traits.Resilient,
            Traits.Grounded,
            Traits.MetabolicSurge,
            Traits.Biter,
        },
            InnateSpells = new List<SpellTypes>() { SpellTypes.LightningBolt, SpellTypes.CrossShock },
            RaceDescription = "Somehow another Kaiju, but lightning this time!",
        };

        Smudger = new RaceTraits()
        {
            BodySize = 25,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore, },
            ExpMultiplier = 1f,
            PowerAdjustment = 2f,
            DeployCost = 1,
            Upkeep = 10f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(12, 20),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(20, 28),
                Mind = new RaceStats.StatRange(6, 12),
                Will = new RaceStats.StatRange(8, 16),
                Agility = new RaceStats.StatRange(10, 16),
                Voracity = new RaceStats.StatRange(16, 24),
                Stomach = new RaceStats.StatRange(12, 18),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.SlowMovement,
                Traits.EnthrallingDepths,
                Traits.SteadyStomach,
                Traits.Ravenous,
                Traits.ExtraNutritious,
                Traits.Tasty,
            },
            RaceDescription = "A race of slug-like beings from somewhere beyond the portals.  Their friendly and soft shape is but a ploy to lure in the more gullible prey of our world, and those that aren't fooled by it will discover that they are no pushovers in a struggle either.  Those who find themselves as prey to these creatures will find they don't wish to leave their insides, though if a predator can manage to devour one they will find a delectable meal as their bodies are incredibly nutritious.",
        };

        SpaceCroach = new RaceTraits()
        {
            BodySize = 40,
            StomachSize = 30,
            HasTail = false,
            FavoredStat = Stat.Strength,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 4f,
            PowerAdjustment = 2f,
            DeployCost = 1,
            Upkeep = 12f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 28),
                Dexterity = new RaceStats.StatRange(12, 20),
                Endurance = new RaceStats.StatRange(15, 20),
                Mind = new RaceStats.StatRange(6, 10),
                Will = new RaceStats.StatRange(6, 10),
                Agility = new RaceStats.StatRange(18, 26),
                Voracity = new RaceStats.StatRange(6, 10),
                Stomach = new RaceStats.StatRange(12, 20),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.SenseWeakness,
            Traits.LightFrame,
            Traits.BoggingSlime,
            Traits.Ravenous,
            Traits.Pounce,
            Traits.NimbleClimber,
        },
            RaceDescription = "A mysterious race of roach-like beings from another universe."
        };

        Trex = new RaceTraits()
        {
            BodySize = 90,
            StomachSize = 50,
            HasTail = false,
            FavoredStat = Stat.Strength,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.CockVore, VoreType.Unbirth },
            ExpMultiplier = 12f,
            PowerAdjustment = 12f,
            DeployCost = 2,
            Upkeep = 12f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(30, 42),
                Dexterity = new RaceStats.StatRange(8, 12),
                Endurance = new RaceStats.StatRange(24, 30),
                Mind = new RaceStats.StatRange(10, 14),
                Will = new RaceStats.StatRange(10, 14),
                Agility = new RaceStats.StatRange(12, 18),
                Voracity = new RaceStats.StatRange(24, 32),
                Stomach = new RaceStats.StatRange(24, 32),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Berserk,
            Traits.Biter,
            Traits.Ravenous,
            Traits.IronGut,
        },
            RaceDescription = "They're big, they're scaled, they're hungry, and they got little arms.  Woe betide those who find themselves trapped behind their powerful jaws, for this is the ultimate predator whose iconic roar is known far and wide and strikes fear in all those who hear it."
        };

        Utahraptor = new RaceTraits()
        {
            BodySize = 30,
            StomachSize = 30,
            HasTail = false,
            FavoredStat = Stat.Strength,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.CockVore, VoreType.Unbirth },
            ExpMultiplier = 4f,
            PowerAdjustment = 2f,
            DeployCost = 2,
            Upkeep = 5f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(15, 20),
                Dexterity = new RaceStats.StatRange(8, 12),
                Endurance = new RaceStats.StatRange(15, 20),
                Mind = new RaceStats.StatRange(10, 14),
                Will = new RaceStats.StatRange(10, 14),
                Agility = new RaceStats.StatRange(12, 18),
                Voracity = new RaceStats.StatRange(14, 20),
                Stomach = new RaceStats.StatRange(14, 20),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Charge,
            Traits.Pounce,
            Traits.Biter,
            Traits.BornToMove,
        },
            RaceDescription = "Utahraptors are much faster and more powerful thatn the usual raptors you see here and there.  More hungry too."
        };


        Selicia = new RaceTraits()
        {
            BodySize = 60,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth },
            ExpMultiplier = 4f,
            PowerAdjustment = 7f,
            DeployCost = 4,
            Upkeep = 30f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(22, 26),
                Dexterity = new RaceStats.StatRange(10, 14),
                Endurance = new RaceStats.StatRange(30, 36),
                Mind = new RaceStats.StatRange(16, 20),
                Will = new RaceStats.StatRange(6, 8),
                Agility = new RaceStats.StatRange(20, 24),
                Voracity = new RaceStats.StatRange(14, 18),
                Stomach = new RaceStats.StatRange(12, 16),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.KeenReflexes,
                Traits.StrongGullet,
                Traits.NimbleClimber,
                Traits.ColdTolerance,
            },
            InnateSpells = new List<SpellTypes>() { SpellTypes.IceBlast },
            RaceDescription = "A hybrid between a dragon and salamander whom excels in climbing and swimming but lacks any wings for flight.",

        };

        Vision = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 3f,
            PowerAdjustment = 4f,
            DeployCost = 1,
            Upkeep = 14f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 22),
                Dexterity = new RaceStats.StatRange(16, 18),
                Endurance = new RaceStats.StatRange(24, 26),
                Mind = new RaceStats.StatRange(14, 18),
                Will = new RaceStats.StatRange(12, 16),
                Agility = new RaceStats.StatRange(18, 24),
                Voracity = new RaceStats.StatRange(14, 20),
                Stomach = new RaceStats.StatRange(12, 16),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.Ravenous,
                Traits.StrongGullet,
                Traits.Intimidating,

            },
            RaceDescription = "A Xeno-Spinosaurid about the size of a small horse or large dog. They eat about half or even double their body weight at minimum a day, but have been known to eat things larger than themselves. Because of their huge appetite, their digestive tract is mostly stomach, what they can't digest they regurgitate as an owl-like pellet",

        };

        Ki = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 22,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.CockVore },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(5, 7),
                Dexterity = new RaceStats.StatRange(5, 7),
                Endurance = new RaceStats.StatRange(9, 11),
                Mind = new RaceStats.StatRange(8, 12),
                Will = new RaceStats.StatRange(18, 22),
                Agility = new RaceStats.StatRange(20, 24),
                Voracity = new RaceStats.StatRange(18, 22),
                Stomach = new RaceStats.StatRange(18, 22),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.ArtfulDodge,
                Traits.KeenReflexes,
                Traits.StrongGullet,
        },
            RaceDescription = "A member of a race that uses its small size and unthreathening appearance to lure in potential prey, Ki decided that becoming a mercenary suited him fine. After all, he'd be paid for getting free meals!",
        };

        Scorch = new RaceTraits()
        {
            BodySize = 32,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 4f,
            PowerAdjustment = 4f,
            DeployCost = 1,
            Upkeep = 32f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 24),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(24, 36),
                Mind = new RaceStats.StatRange(40, 50),
                Will = new RaceStats.StatRange(12, 18),
                Agility = new RaceStats.StatRange(16, 28),
                Voracity = new RaceStats.StatRange(36, 40),
                Stomach = new RaceStats.StatRange(24, 40),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Flight,
                Traits.Biter,
                Traits.StrongGullet,
                Traits.Cruel,
                Traits.FastAbsorption,
        },
            InnateSpells = new List<SpellTypes>() { SpellTypes.Pyre },
            RaceDescription = "A cruel, gluttonous red wyvern",
        };


        Asura = new RaceTraits()
        {
            BodySize = 15,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Strength,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.Anal },
            ExpMultiplier = 1.4f,
            PowerAdjustment = 3f,
            DeployCost = 1,
            Upkeep = 15f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 24),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(16, 20),
                Mind = new RaceStats.StatRange(8, 12),
                Will = new RaceStats.StatRange(12, 16),
                Agility = new RaceStats.StatRange(10, 16),
                Voracity = new RaceStats.StatRange(18, 24),
                Stomach = new RaceStats.StatRange(8, 12),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Maul,
                Traits.Frenzy,
                Traits.ShunGokuSatsu
        },
            RaceDescription = "A wandering warrior who seeks to further her strength by engaging in mortal combat with the incredibly strong creatures of this realm.  Not even dragons are safe from the gluttonous embrace and the earth-shattering blows from her Musaboriken fighting style.",
            CanUseRangedWeapons = false,
        };

        DRACO = new RaceTraits()
        {
            BodySize = 80,
            StomachSize = 60,
            HasTail = true,
            FavoredStat = Stat.Endurance,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 4f,
            PowerAdjustment = 4f,
            DeployCost = 1,
            Upkeep = 14f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 24),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(32, 40),
                Mind = new RaceStats.StatRange(16, 20),
                Will = new RaceStats.StatRange(12, 18),
                Agility = new RaceStats.StatRange(28, 40),
                Voracity = new RaceStats.StatRange(16, 24),
                Stomach = new RaceStats.StatRange(18, 26),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.MetalBody,
                Traits.Resilient,
                Traits.KeenReflexes,
                Traits.BornToMove,
                Traits.Intimidating,
        },
            RaceDescription = "A corrupted D.r.a.c.o unit. Unlike other units from his line 008 has tampered with his coding and removed the safety on his stomach allowing him to digest his prisoners.",
        };

        Zoey = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 40,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal },
            ExpMultiplier = 1.6f,
            PowerAdjustment = 3f,
            DeployCost = 1,
            Upkeep = 14f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(14, 20),
                Dexterity = new RaceStats.StatRange(8, 10),
                Endurance = new RaceStats.StatRange(18, 20),
                Mind = new RaceStats.StatRange(6, 10),
                Will = new RaceStats.StatRange(12, 18),
                Agility = new RaceStats.StatRange(14, 18),
                Voracity = new RaceStats.StatRange(14, 18),
                Stomach = new RaceStats.StatRange(14, 18),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Maul,
                Traits.StrongGullet,
                Traits.Biter,
                Traits.Greedy,
                Traits.BornToMove,
                Traits.TailStrike,
        },
            RaceDescription = "An anthropomorphic tiger shark from another world.  Zoey is typically a lazy girl who loves watching movies and being a general couch-potato.  However, upon realizing she'd been isekai'd into the realm, her gluttony left her interested in trying to stomach the local warriors and monsters with some basic martial arts, joining whichever side would pay her first.",
        };

        Cierihaka = new RaceTraits()
        {
            BodySize = 150,
            StomachSize = 100,
            HasTail = true,
            FavoredStat = Stat.Strength,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth },
            ExpMultiplier = 7f,
            PowerAdjustment = 7f,
            DeployCost = 4,
            Upkeep = 30f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(32, 40),
                Dexterity = new RaceStats.StatRange(18, 22),
                Endurance = new RaceStats.StatRange(36, 42),
                Mind = new RaceStats.StatRange(12, 16),
                Will = new RaceStats.StatRange(16, 20),
                Agility = new RaceStats.StatRange(20, 24),
                Voracity = new RaceStats.StatRange(16, 20),
                Stomach = new RaceStats.StatRange(24, 28),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.ForcefulBlow,
                Traits.StrongGullet,
                Traits.Pounce,
                Traits.HeavyPounce,
        },
            RaceDescription = "This girthy dragoness hails from a far away arid land, and excels at pressing the attack, with a great pair of skewers in place of where most dragons would have wings. With considerable grace despite her size, she exercises vigilance on the battlefield. ",
        };

        Zera = new RaceTraits()
        {
            BodySize = 24,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.CockVore },
            ExpMultiplier = 2.4f,
            PowerAdjustment = 4f,
            DeployCost = 1,
            Upkeep = 16f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(24, 30),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(24, 30),
                Mind = new RaceStats.StatRange(16, 20),
                Will = new RaceStats.StatRange(12, 18),
                Agility = new RaceStats.StatRange(20, 32),
                Voracity = new RaceStats.StatRange(16, 24),
                Stomach = new RaceStats.StatRange(16, 24),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.StrongGullet,
                Traits.ArtfulDodge,
                Traits.NimbleClimber,
                Traits.BornToMove,
                Traits.TailStrike,
                Traits.GiantSlayer,
        },
            RaceDescription = "A devious and voracious wyvern. Known for his agility and cunning, don't ever turn your back to him or you might find yourself in trouble.",
        };

        Auri = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.Anal },
            ExpMultiplier = 2.4f,
            PowerAdjustment = 3.2f,
            DeployCost = 1,
            Upkeep = 20f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(16, 16),
                Dexterity = new RaceStats.StatRange(20, 20),
                Endurance = new RaceStats.StatRange(16, 20),
                Mind = new RaceStats.StatRange(20, 20),
                Will = new RaceStats.StatRange(14, 20),
                Agility = new RaceStats.StatRange(24, 26),
                Voracity = new RaceStats.StatRange(16, 20),
                Stomach = new RaceStats.StatRange(12, 16),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.ArtfulDodge,
                Traits.ThrillSeeker,
                Traits.FastCaster
            },
            InnateSpells = new List<SpellTypes>()
            { SpellTypes.Mending, SpellTypes.Summon },
            RaceDescription = "A fox-woman priestess and self-proclaimed avatar of a creator of the world.",
        };

        Erin = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 10,
            FavoredStat = Stat.Endurance,
            AllowedVoreTypes = new List<VoreType> { },
            ExpMultiplier = 2.4f,
            PowerAdjustment = 2f,
            DeployCost = 1,
            Upkeep = 6f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(5, 10),
                Dexterity = new RaceStats.StatRange(5, 10),
                Endurance = new RaceStats.StatRange(20, 25),
                Mind = new RaceStats.StatRange(20, 25),
                Will = new RaceStats.StatRange(20, 25),
                Agility = new RaceStats.StatRange(24, 26),
                Voracity = new RaceStats.StatRange(10, 15),
                Stomach = new RaceStats.StatRange(9, 10),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.Tasty,
                Traits.Prey,
                Traits.EasyToVore,
                Traits.Flight,
                Traits.TheGreatEscape
            },
            InnateSpells = new List<SpellTypes>()
            { SpellTypes.DivinitysEmbrace },
            RaceDescription = "Erin belongs to a very rare species known as a Nyangel, the lovechild of an angel and a catgirl.  Thanks to this divine heritage they aremostly all incredible healers... But they're also incredibly tasty.  Every Nyangel has a unique trait to set them apart from eachother, and Erin is no exception to this rule.  Her quirk is total acid resistance, the perfect defense against the raveous predators of this realm.  That doesn't stop her from being devoured, however, and that is unfortunately an all-too-common outcome for the girl.  Regardless of how many times she ends up eaten, the loveable Nyangel still tries her best to heal those she can.",
        };

        

        Salix = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Dexterity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.Anal, VoreType.CockVore },
            ExpMultiplier = 2.4f,
            PowerAdjustment = 5f,
            DeployCost = 1,
            Upkeep = 20f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(6, 10),
                Dexterity = new RaceStats.StatRange(10, 15),
                Endurance = new RaceStats.StatRange(15, 20),
                Mind = new RaceStats.StatRange(25, 30),
                Will = new RaceStats.StatRange(20, 25),
                Agility = new RaceStats.StatRange(24, 26),
                Voracity = new RaceStats.StatRange(16, 20),
                Stomach = new RaceStats.StatRange(11, 16),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.ArcaneMagistrate,
                Traits.SpellBlade,
                Traits.ManaAttuned,
                Traits.ManaRich
            },
            InnateSpells = new List<SpellTypes>()
            { SpellTypes.AmplifyMagic, SpellTypes.Evocation, SpellTypes.ManaFlux, SpellTypes.UnstableMana},
            RaceDescription = "A demi-mouse mage from a different, mana rich dimension. Has had trouble adapting to the absence of mana here, but makes do.",
        };


        Bella = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 15,
            FavoredStat = Stat.Endurance,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.Anal },
            ExpMultiplier = 1.5f,
            PowerAdjustment = 1.2f,
            DeployCost = 1,
            Upkeep = 20f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(6, 10),
                Dexterity = new RaceStats.StatRange(10, 15),
                Endurance = new RaceStats.StatRange(25, 30),
                Mind = new RaceStats.StatRange(15, 20),
                Will = new RaceStats.StatRange(20, 25),
                Agility = new RaceStats.StatRange(10, 15),
                Voracity = new RaceStats.StatRange(20, 25),
                Stomach = new RaceStats.StatRange(15, 20),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.Tenacious,
                Traits.ManaBarrier,
                Traits.EfficientGuts,
                Traits.Unflinching,
                Traits.ArcaneMagistrate,
                Traits.ManaRich,
                Traits.SpellBlade,
                Traits.Clumsy
            },
            InnateSpells = new List<SpellTypes>()
            { SpellTypes.Mending, SpellTypes.Fireball},
            RaceDescription = "\"A shy cowgirl ^o^\" - Made by AgentAmbi",
        };

        Singularity = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 30,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.Anal },
            ExpMultiplier = 2f,
            PowerAdjustment = 4f,
            DeployCost = 1,
            Upkeep = 15f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(10, 15),
                Dexterity = new RaceStats.StatRange(14, 21),
                Endurance = new RaceStats.StatRange(12, 19),
                Mind = new RaceStats.StatRange(10, 16),
                Will = new RaceStats.StatRange(16, 21),
                Agility = new RaceStats.StatRange(7, 11),
                Voracity = new RaceStats.StatRange(12, 18),
                Stomach = new RaceStats.StatRange(16, 21),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.DualStomach,
                Traits.Ravenous,
                Traits.AwkwardShape,
                Traits.StrongGullet,
        },
            RaceDescription = "A ravenous species seemingly mirrored from the deer, this herbivore enjoys showing just how quickly she can gobble a warrior down.",
        };

        Feit = new RaceTraits()
        {
            BodySize = 30,
            StomachSize = 25,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 4f,
            PowerAdjustment = 4f,
            DeployCost = 3,
            Upkeep = 25f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(15, 22),
                Dexterity = new RaceStats.StatRange(12, 18),
                Endurance = new RaceStats.StatRange(18, 23),
                Mind = new RaceStats.StatRange(8, 12),
                Will = new RaceStats.StatRange(19, 24),
                Agility = new RaceStats.StatRange(18, 24),
                Voracity = new RaceStats.StatRange(18, 22),
                Stomach = new RaceStats.StatRange(16, 21),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.StrongGullet,
                Traits.Pounce,
                Traits.KeenReflexes,
                Traits.Growth,
                Traits.MinorGrowth,
                Traits.Tasty,
        },
            RaceDescription = "A strange, almost draconic looking raptor. She seems to grow larger and stronger with each victim consumed. Despite having wings, they seem to only allow her to leap great distances instead of fly.",
        };

        Taraluxia = new RaceTraits()
        {
            BodySize = 100,
            StomachSize = 100,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal },
            ExpMultiplier = 7f,
            PowerAdjustment = 15f,
            DeployCost = 4,
            Upkeep = 40f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(18, 24),
                Dexterity = new RaceStats.StatRange(20, 30),
                Endurance = new RaceStats.StatRange(30, 42),
                Mind = new RaceStats.StatRange(20, 32),
                Will = new RaceStats.StatRange(40, 55),
                Agility = new RaceStats.StatRange(20, 30),
                Voracity = new RaceStats.StatRange(32, 44),
                Stomach = new RaceStats.StatRange(25, 35),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.QueenOfFrost,
                Traits.Intimidating,
                Traits.TailStrike,
            },
            InnateSpells = new List<SpellTypes>() { SpellTypes.IceBlast, SpellTypes.Icicle },
            RaceDescription = "An ice dragoness claiming to hail from a distant city. She seems a bit nicer than the wild dragons, but even hungrier. Her deep experience with her element let's her frequently cast ice breath attacks, and chills the air around nearby foes.",

        };

        Xelhilde = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            FavoredStat = Stat.Strength,
            HasTail = true,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth },
            ExpMultiplier = 1.4f,
            PowerAdjustment = 2f,
            DeployCost = 1,
            Upkeep = 13f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(23, 28),
                Dexterity = new RaceStats.StatRange(8, 16),
                Endurance = new RaceStats.StatRange(21, 25),
                Mind = new RaceStats.StatRange(8, 14),
                Will = new RaceStats.StatRange(12, 16),
                Agility = new RaceStats.StatRange(16, 21),
                Voracity = new RaceStats.StatRange(8, 12),
                Stomach = new RaceStats.StatRange(10, 13),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.DoubleAttack,
                Traits.DefensiveStance,
                Traits.KeenReflexes,
                Traits.AdeptLearner,
        },
            RaceDescription = "A canine knight from the Kingdom of Mondfeld that wields a cobalt zweihänder. She roams the realm in search of battle to bring glory to Mondfeld!",
        };

        Skapa = new RaceTraits()
        {
            BodySize = 60,
            StomachSize = 40,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.CockVore , VoreType.Unbirth },
            ExpMultiplier = 2f,
            PowerAdjustment = 4f,
            DeployCost = 1,
            Upkeep = 15f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(10, 15),
                Dexterity = new RaceStats.StatRange(14, 21),
                Endurance = new RaceStats.StatRange(12, 19),
                Mind = new RaceStats.StatRange(10, 16),
                Will = new RaceStats.StatRange(16, 21),
                Agility = new RaceStats.StatRange(7, 11),
                Voracity = new RaceStats.StatRange(12, 18),
                Stomach = new RaceStats.StatRange(16, 21),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Biter,
                Traits.Pounce,
        },
            RaceDescription = "A seductive herm barioth",
        };

        Olivia = new RaceTraits()
        {
            BodySize = 9,
            StomachSize = 12,
            FavoredStat = Stat.Mind,
            HasTail = true,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore, VoreType.BreastVore },
            ExpMultiplier = 1.4f,
            PowerAdjustment = 2f,
            DeployCost = 1,
            Upkeep = 9f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(6, 12),
                Dexterity = new RaceStats.StatRange(6, 12),
                Endurance = new RaceStats.StatRange(8, 13),
                Mind = new RaceStats.StatRange(15, 23),
                Will = new RaceStats.StatRange(14, 18),
                Agility = new RaceStats.StatRange(12, 16),
                Voracity = new RaceStats.StatRange(7, 11),
                Stomach = new RaceStats.StatRange(12, 15),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.FriendlyStomach,
                Traits.Pounce,
                Traits.Timid,
                Traits.Submissive,
        },
            InnateSpells = new List<SpellTypes>() { SpellTypes.CrossShock, SpellTypes.ArcBolt },
            RaceDescription = "A small fox with surprisingly strong lightning magic.",
        };

        Tatltuae = new RaceTraits()
        {
            BodySize = 11,
            StomachSize = 27,
            FavoredStat = Stat.Mind,
            HasTail = true,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.CockVore },
            ExpMultiplier = 1.4f,
            PowerAdjustment = 2f,
            DeployCost = 1,
            Upkeep = 9f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(4, 12),
                Dexterity = new RaceStats.StatRange(4, 10),
                Endurance = new RaceStats.StatRange(8, 13),
                Mind = new RaceStats.StatRange(15, 21),
                Will = new RaceStats.StatRange(12, 16),
                Agility = new RaceStats.StatRange(8, 11),
                Voracity = new RaceStats.StatRange(12, 18),
                Stomach = new RaceStats.StatRange(12, 15),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Cartography,
                Traits.BoundWeapon,
                Traits.BookEater,
                Traits.Featherweight,
                Traits.Flight,
                Traits.ManaDynamo,
                Traits.PleasurableTouch,
        },
            RaceDescription = "Tatltuae is a curious raven. While he seemingly is one of the many who entered this world through a portal, he's taken quite well to his new home, becoming known as a mage, pred, cartographer, and selling his skills as a mercenary. While his hollow bones and spellcasting generally put him in the backlines, he is always eager to add people to his waistline, when given the chance. His main spell seems to be based on chaotic magic, and he definitely seems to enjoy causing chaos where he can. Tatltuae learned some time ago to create pockets of intense chaotic entropy. Interestingly, the spell began as a healing spell, but the raven learned it wrong to the point it harms instead of heals.",
        };

        Firefly = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 12,
            FavoredStat = Stat.Dexterity,
            HasTail = true,
            AllowedVoreTypes = new List<VoreType> { },//Perma-prey until vore sprites added
            DeployCost = 1,
            Upkeep = 9f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 15),
                Dexterity = new RaceStats.StatRange(8, 15),
                Endurance = new RaceStats.StatRange(8, 13),
                Mind = new RaceStats.StatRange(6, 11),
                Will = new RaceStats.StatRange(6, 13),
                Agility = new RaceStats.StatRange(8, 12),
                Voracity = new RaceStats.StatRange(5, 10),
                Stomach = new RaceStats.StatRange(12, 15),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.PackStrength,
            Traits.EscapeArtist,
            Traits.ArtfulDodge,
            Traits.Charge,
            Traits.DefensiveStance,
            Traits.Fit,
            Traits.UnpleasantDigestion,
            Traits.QuickShooter,
            Traits.Prey,//Perma-prey until vore sprites added
        },
            RaceDescription = "An unnatural Umbreon from a different universe from the others. His body shape seems different than the Umbreons, Having a differently shaped head, digigrade legs, and a fox like appearance. He claims to come from a realm where the world was much more advanced, having been transported with what he calls a \"HND15\" some kind of \"handgun\" similar to the tarous' revolvers. Being from another realm these bullets are in short supply, forcing him to save the magazines and to forge new bullets to load into them. He also carries around a knife for backup, it seems extra sharp compared to the others of it's kind. Although he prefers to stick along side them, He claims to not be related to the Anthro Umbreons that roam around.",
        };


    }

}



internal class RaceTraits
{
    /// <summary>
    /// Controls the size of the body, used for determining how much space they take up in a belly
    /// </summary>
    internal int BodySize;

    internal RaceAI RaceAI;
    /// <summary>
    /// Controls the base stomach size, at 12 stomach, the capacity will equal this value, and increases or decreases linearly
    /// </summary>
    internal int StomachSize;
    internal bool HasTail;
    internal Stat FavoredStat;
    internal List<Traits> RacialTraits;
    internal List<Traits> LeaderTraits;
    internal List<Traits> SpawnTraits;
    internal List<int> RacialTags = new List<int>();
    //internal List<Traits> RandomTraits;
    internal List<VoreType> AllowedVoreTypes = new List<VoreType> { VoreType.Anal, VoreType.Oral, VoreType.CockVore, VoreType.BreastVore, VoreType.Unbirth };
    internal Race SpawnRace = Race.none;
    internal Race ConversionRace = Race.none;
    internal Race LeaderRace = Race.none;
    internal Race MorphRace = Race.none;
    internal List<SpellTypes> InnateSpells = new List<SpellTypes>();
    internal RaceStats RaceStats = new RaceStats();

    /// <summary>
    /// Controls the amount of unit slots unit takes up in an army.
    /// </summary>
    internal int DeployCost = 1;

    /// <summary>
    /// Controls the upkeep a unit requires.
    /// </summary>
    internal float Upkeep = 1f;

    /// <summary>
    /// Attacks against this race will have their experience gained modified by this ratio
    /// </summary>
    internal float ExpMultiplier = 1f;
    /// <summary>
    /// A straight multiplier to a unit's perceived power
    /// Eventually, this may be incorporated as reading stats instead
    /// </summary>
    internal float PowerAdjustment = 1f;
    internal bool CanUseRangedWeapons = true;
    internal string RaceDescription = "";

}

internal enum VoreType
{
    All,
    Oral,
    Unbirth,
    CockVore,
    BreastVore,
    TailVore,
    Anal,


}

internal class RaceStats
{
    /// <summary>
    /// Sets the minimum and maximum stats
    /// </summary>
    internal struct StatRange
    {
        [OdinSerialize]
        internal int Minimum;
        [OdinSerialize]
        internal int Roll;
        /// <summary>
        /// Sets up the stat range
        /// </summary>
        /// <param name="minimum">the lowest the stat will be</param>
        /// <param name="maximum">the highest, inclusive</param>
        public StatRange(int minimum, int maximum)
        {
            Minimum = minimum;
            Roll = 1 + maximum - minimum;
            if (Roll < 1)
            {
                UnityEngine.Debug.LogWarning("Maximum is less than minimum for one of the Stat Ranges");
                Roll = 1;
            }
        }

        public StatRange(bool junk, int minimum, int roll)
        {
            Minimum = minimum;
            Roll = roll;
        }
    }
    [OdinSerialize]
    internal StatRange Strength;
    [OdinSerialize]
    internal StatRange Dexterity;
    [OdinSerialize]
    internal StatRange Voracity;
    [OdinSerialize]
    internal StatRange Mind;
    [OdinSerialize]
    internal StatRange Agility;
    [OdinSerialize]
    internal StatRange Stomach;
    [OdinSerialize]
    internal StatRange Endurance;
    [OdinSerialize]
    internal StatRange Will;

    public RaceStats() //Default Stats
    {
        Strength = new StatRange(6, 14);
        Dexterity = new StatRange(6, 14);
        Endurance = new StatRange(8, 13);
        Mind = new StatRange(6, 13);
        Will = new StatRange(6, 13);
        Agility = new StatRange(6, 10);
        Voracity = new StatRange(6, 11);
        Stomach = new StatRange(12, 15);
    }
}
