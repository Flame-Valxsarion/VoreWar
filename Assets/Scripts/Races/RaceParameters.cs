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
    static readonly RaceTraits Otachi;
    static readonly RaceTraits Xelhilde;
    static readonly RaceTraits BoomBunnies;

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
            case Race.Otachi:
                return Otachi;
            case Race.Xelhilde:
                return Xelhilde;
            case Race.BoomBunnies:
                return BoomBunnies;
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
            BodySize = 10,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
            {
                Traits.Pounce,
                Traits.EscapeArtist
            },
            RaceDescription = "Natives to the realm, the Cats are skilled at pouncing on their enemy with a sudden burst of speed. Many a wounded warrior has found themselves devoured by a feline jumping over a wall of their fellow warriors, while the Cat's allies defy their enemies by somehow squirming out of their stomach's.",
        };

        Dogs = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Agility,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.PackWill,
            Traits.PackDefense
        },
            RaceDescription = "Natives to the realm, the Dogs embody the principle of standing together. Ranked up, they guard each other's backs, making it harder for any enemy to land a strike at them or succeed at eating them.",
        };

        Foxes = new RaceTraits()
        {
            BodySize = 10,
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
            LeaderRace = Race.Youko,
            RaceDescription = "Natives of this realm, the Foxes seem unable of taking danger seriously. They dodge attacks at the last second and only seem to grow ever bolder as death approaches them. Entire armies have fallen exhausted as a group of foxes dances among them, ready to be devoured once the time is right.",
        };
       
        Youko = new RaceTraits()
        {
            BodySize = 10,
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
            BodySize = 10,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Strength,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.PackStrength,
            Traits.PackVoracity
        },
            RaceDescription = "Natives of this realm, the Wolves have a history of hunting in packs extending beyond the crafting of their first weapons. While a lone Wolf can still be a worthy adversary, their true strength comes from working with their kin.",
        };


        Bunnies = new RaceTraits()
        {
            BodySize = 10,
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
            StomachSize = 18,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            DeployCost = 1,
            Upkeep = 4f,
            RacialTraits = new List<Traits>()
        {
            Traits.Resilient,
            Traits.Intimidating
        },

            RaceDescription = "Emerging from dense jungles, the Lizards are eager to expand their presence in the universe. Their hard scales offered them great protection from the thorns and insects of their former home, and still offer natural resistance from harm.",
        };



        Slimes = new RaceTraits()
        {
            BodySize = 7,
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
            RaceDescription = "A puddle of goo given form by the power of their core, the Slimes have a need to act as if they had solid bodies. Their true from is still almost liquid though, lacking organs or other features of note, and thus very hard to damage by normal means.",
        };


        Scylla = new RaceTraits()
        {
            BodySize = 15,
            StomachSize = 15,
            HasTail = false,
            FavoredStat = Stat.Will,
            DeployCost = 1,
            Upkeep = 2f,
            RacialTraits = new List<Traits>()
        {
            //Traits.Aquatic,
            Traits.TentacleHarassment
        },
            RaceDescription = "Trapped under the surface at their old world, the Scylla surged forth when the appearance of mystical portals gave them passage to lands above water. Their many tentacles seem to act as if having minds of their own, hindering and harassing their enemies.",
        };


        Harpies = new RaceTraits()
        {
            BodySize = 8,
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
            BodySize = 6,
            StomachSize = 12,
            HasTail = true,
            FavoredStat = Stat.Will,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
            Traits.PackStomach,
            Traits.AstralCall,
        },
            RaceDescription = "Following the scent of new lands to torment, these beings erupted forth from the underworld. So eager are they that at the promise of battle some of the Imps still in the infernal realm may manifest just for a chance at carnage.",
        };


        Humans = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 15,
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
            BodySize = 10,
            StomachSize = 18,
            HasTail = false,
            FavoredStat = Stat.Agility,
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
            BodySize = 15,
            StomachSize = 25,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            DeployCost = 1,
            Upkeep = 3f,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.BreastVore, VoreType.Anal, VoreType.TailVore },
            RacialTraits = new List<Traits>()
        {
            Traits.Ravenous,
            Traits.Biter,
            Traits.DualStomach
        },
            RaceDescription = "Natives to this realm, these legless beings were once the strongest and largest hunters of the land. The sudden emergence of many new species left the Lamia uncertain at first, but soon their dual stomachs won and they focused on testing the taste of the new arrivals.",
        };

        Kangaroos = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 15,
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
                Traits.ForcefulBlow
        },
            RaceDescription = "Once mere cattle, a drop of minotaur blood slumbered in their veins. Rising and butchering their \"masters\", the Taurus took what they could from their old ranches and fled through mysterious portals that had heralded their rise. While intelligent, the Taurus trust in their physical might and great size, tossing their enemies aside as they trample on.",
        };

        Crux = new RaceTraits()
        {
            BodySize = 9,
            StomachSize = 14,
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
            BodySize = 10,
            StomachSize = 16,
            HasTail = true,
            FavoredStat = Stat.Strength,
            DeployCost = 1,
            Upkeep = 4f,
            RacialTraits = new List<Traits>()
        {
                Traits.Charge,
                Traits.StrongMelee
        },
            RaceDescription = "",
        };

        Sergal = new RaceTraits()
        {
            BodySize = 11,
            StomachSize = 14,
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
            RaceDescription = "",
        };

        Bees = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 14,
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
            RaceDescription = "",
        };

        Driders = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 20,
            HasTail = false,
            FavoredStat = Stat.Strength,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
                //Traits.StrongMelee,
                Traits.NimbleClimber,
                Traits.Webber,
        },
            RaceDescription = "",
        };

        Alraune = new RaceTraits()
        {
            BodySize = 14,
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
            RaceDescription = "",
        };

        Demibats = new RaceTraits()
        {
            BodySize = 8,
            StomachSize = 13,
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
            RaceDescription = "",
        };

        Panthers = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Strength,
            DeployCost = 1,
            Upkeep = 4f,
            RacialTraits = new List<Traits>()
        {
                Traits.TasteForBlood,
                Traits.Pounce
        },
            RaceDescription = "Long before \"elder races\" walked among the stars, the Panthers thrived. Long before first words of power were uttered, they have carved their homes into the lightning-struck bark, feeding off its power. And long after the last bastion of so-called civilization will fall to onslaught of wings, claws and fangs, they will thrive in the darkness, pouncing on unsuspecting prey.",
        };

        Mermen = new RaceTraits()
        {
            BodySize = 13,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Will,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
                Traits.MagicResistance,
                Traits.HealingBlood,
                Traits.Slippery
        },
            RaceDescription = "",
        };

        Avians = new RaceTraits()
        {
            BodySize = 10,
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
            RaceDescription = "",
        };

        Demiants = new RaceTraits()
        {
            BodySize = 9,
            StomachSize = 12,
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
            RaceDescription = "",
        };

        Demifrogs = new RaceTraits()
        {
            BodySize = 14,
            StomachSize = 18,
            HasTail = false,
            FavoredStat = Stat.Voracity,
            DeployCost = 1,
            Upkeep = 3f,
            RacialTraits = new List<Traits>()
        {
                Traits.Pounce,
                Traits.HeavyPounce,
                Traits.RangedVore,
                Traits.Clumsy
        },
            RaceDescription = "",
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
        },
            RaceDescription = "",
        };

        Deer = new RaceTraits()
        {
            BodySize = 10,
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
            RaceDescription = "",
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
            RacialTraits = new List<Traits>()
        {
                Traits.ViralDigestion,
                Traits.AwkwardShape,
                Traits.SlowAbsorption,
                Traits.SlowBreeder,
        },
            RaceDescription = "The Aabayx are a species of virosapiens who recently revealed themselves to the world and were quick to commit to the stage of war.  Strangely enough, they are not new arrivals to the realm, but rather have been in extreme isolation in an unknown location and were waiting for the exact right time to resurface and conquer the masses.  That time is now.",
        };

        Mice = new RaceTraits()
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
            FavoredStat = Stat.Stomach,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.Anal },
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
            BodySize = 9,
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
            LeaderRace = Race.Abakhanskya,
            RaceDescription = "A tribe of kobolds who are faithfully serving their goddess, Abakhanskya.",
            RaceAI = RaceAI.ServantRace
        };

        Succubi = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 12,
            HasTail = true,
            FavoredStat = Stat.Will,
            DeployCost = 1,
            Upkeep = 3f,
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
            RaceAI = RaceAI.Hedonist,
        };


        Tigers = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 18,
            HasTail = true,
            FavoredStat = Stat.Strength,
            PowerAdjustment = 1.3f,
            DeployCost = 1,
            Upkeep = 5f,
            RacialTraits = new List<Traits>()
        {
            Traits.Maul,
            Traits.Frenzy
        },
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(16, 24),
                Dexterity = new RaceStats.StatRange(10, 18),
                Endurance = new RaceStats.StatRange(12, 22),
                Mind = new RaceStats.StatRange(12, 20),
                Will = new RaceStats.StatRange(10, 22),
                Agility = new RaceStats.StatRange(8, 18),
                Voracity = new RaceStats.StatRange(12, 20),
                Stomach = new RaceStats.StatRange(12, 18),
            },
            RaceDescription = "Somewhat enigmatic, it is uncertain if the Tigers are native to this realm or came from elsewhere. They do not seem interested in settling down though, joining armies to test their considerable skills in battle instead.",
        };



        Goblins = new RaceTraits()
        {
            BodySize = 7,
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
            BodySize = 15,
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
                Strength = new RaceStats.StatRange(10, 18),
                Dexterity = new RaceStats.StatRange(4, 7),
                Endurance = new RaceStats.StatRange(12, 18),
                Mind = new RaceStats.StatRange(5, 10),
                Will = new RaceStats.StatRange(8, 14),
                Agility = new RaceStats.StatRange(6, 10),
                Voracity = new RaceStats.StatRange(7, 14),
                Stomach = new RaceStats.StatRange(8, 14),
            },
            RacialTraits = new List<Traits>() // Alligator = Lizard+
            {
                Traits.Ravenous, // Bonus to voracity before eating
                Traits.Resilient, // Damage decrease
                Traits.Intimidating, // Penalty to enemies in melee range
            },
            RaceDescription = "Natives to great swamps on another dimension, the Alligators emerge sporadically from portals across the land. Either unwilling or unable to settle this realm, they instead work as mercenaries for hire. Large, tough and intimidating, they make great bruisers, but seem totally unable to understand the principle of ranged weapons.",
        };

        Puca = new RaceTraits()
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
            Traits.Pounce,
        },
            RaceDescription = "A race of burrowers very true to their heritage, the Puca trust their shovels and feet above advanced technology. Many a foe has found themselves swallowed up by their deep dark tunnels.",
        };

        Kobolds = new RaceTraits()
        {
            BodySize = 9,
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
            RaceDescription = "",
            RaceAI = RaceAI.ServantRace
        };

        DewSprite = new RaceTraits()
        {
            BodySize = 14,
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
            RaceDescription = "",
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.Anal },
        };

        Hippos = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 22,
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
            },
            RaceDescription = "",
        };

        Vipers = new RaceTraits()
        {
            BodySize = 18,
            StomachSize = 25,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.BreastVore, VoreType.Anal, VoreType.TailVore },
            PowerAdjustment = 1.4f,
            DeployCost = 1,
            Upkeep = 4f,
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
            RaceDescription = "",
        };

        Komodos = new RaceTraits()
        {
            BodySize = 18,
            StomachSize = 18,
            HasTail = true,
            FavoredStat = Stat.Strength,
            CanUseRangedWeapons = false,
            PowerAdjustment = 1.4f,
            DeployCost = 1,
            Upkeep = 4f,
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
            },
            RaceDescription = "",
        };

        Cockatrice = new RaceTraits()
        {
            BodySize = 14,
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
            RaceDescription = "",
        };

        Vargul = new RaceTraits()
        {
            BodySize = 17,
            StomachSize = 17,
            HasTail = true,
            FavoredStat = Stat.Strength,
            ExpMultiplier = 1.25f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 5f,
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
            },
            RaceDescription = "",
        };

        Hamsters = new RaceTraits()
        {
            BodySize = 10,
            StomachSize = 18,
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
            BodySize = 10,
            StomachSize = 18,
            HasTail = true,
            FavoredStat = Stat.Dexterity,
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
            BodySize = 8,
            StomachSize = 13,
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
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.25f,
            PowerAdjustment = .9f,
            DeployCost = 1,
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 20),
                Dexterity = new RaceStats.StatRange(6, 14),
                Endurance = new RaceStats.StatRange(10, 18),
                Mind = new RaceStats.StatRange(6, 14),
                Will = new RaceStats.StatRange(4, 8),
                Agility = new RaceStats.StatRange(6, 12),
                Voracity = new RaceStats.StatRange(8, 16),
                Stomach = new RaceStats.StatRange(8, 16),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.SlowAbsorption,
                Traits.Biter
            },
            RaceDescription = "When the lizard folk emerged from their portal to this land, some young snakes from their old world managed to slip along. Growing fast under the effect of this new realm, the Serpents soon emerged as a ravenous horde.",
        };

        Wyvern = new RaceTraits()
        {
            BodySize = 18,
            StomachSize = 18,
            HasTail = true,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.Anal },
            ExpMultiplier = 1.5f,
            PowerAdjustment = 2f,
            DeployCost = 1,
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 20),
                Dexterity = new RaceStats.StatRange(6, 14),
                Endurance = new RaceStats.StatRange(12, 20),
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
            BodySize = 22,
            StomachSize = 18,
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
                Endurance = new RaceStats.StatRange(18, 30),
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
            BodySize = 16,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.75f,
            PowerAdjustment = 1.75f,
            DeployCost = 1,
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 20),
                Dexterity = new RaceStats.StatRange(4, 8),
                Endurance = new RaceStats.StatRange(14, 24),
                Mind = new RaceStats.StatRange(6, 10),
                Will = new RaceStats.StatRange(6, 12),
                Agility = new RaceStats.StatRange(8, 16),
                Voracity = new RaceStats.StatRange(14, 22),
                Stomach = new RaceStats.StatRange(6, 12),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Flight,
                Traits.Biter
        },
            RaceDescription = "When the Scylla left their old realm the creatures that used to hunt them were left hungry. The Sharks, with their strong sense of smell, were able to track down the portals the Scylla used and followed close behind.",

        };

        FeralWolves = new RaceTraits()
        {
            BodySize = 15,
            StomachSize = 18,
            HasTail = true,
            FavoredStat = Stat.Strength,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal },
            ExpMultiplier = 1.75f,
            PowerAdjustment = 1.75f,
            DeployCost = 1,
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(14, 22),
                Dexterity = new RaceStats.StatRange(9, 13),
                Endurance = new RaceStats.StatRange(17, 26),
                Mind = new RaceStats.StatRange(6, 12),
                Will = new RaceStats.StatRange(12, 18),
                Agility = new RaceStats.StatRange(9, 18),
                Voracity = new RaceStats.StatRange(19, 27),
                Stomach = new RaceStats.StatRange(21, 29),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Biter,
                Traits.PackStrength,
        },
            RaceDescription = "Natives of this realm, the wolves were more than happy for a chance to welcome the newcomers to their bellies. While likely related to their bipedal cousins, the ferals only consider them as familiar smelling food.",
        };

        BlackSwallowers = new RaceTraits()
        {
            BodySize = 10,
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
                Strength = new RaceStats.StatRange(10, 26),
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
            BodySize = 18,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.5f,
            PowerAdjustment = 2.2f,
            DeployCost = 1,
            Upkeep = 3f,
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
        },
            RaceDescription = "A lifeform from far beyond the stars, the Harvesters saw the empty lands fill and felt rising hunger. How they made their way here is unknown, but their mission is readily understood. They are here to feed until the land is empty once more.",

        };

        Collectors = new RaceTraits()
        {
            BodySize = 20,
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
            BodySize = 6,
            StomachSize = 10,
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
            RaceDescription = "A species with large difference in size between genders, the male bats being barely half the female's size. This has led many to believe that the tendency of the females to hunt both for sustenance and pleasure is due to the males being unable to satisfy some of the female's needs."
        };


        Frogs = new RaceTraits()
        {
            BodySize = 15,
            StomachSize = 14,
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
            RaceDescription = ""
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
            RaceDescription = ""
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
            RaceDescription = "The ambient energies that abound in this world sometimes cause normal creatures to grow to abnormal sizes. These dragonflies have adapted their diet to suit their new size and abilities, and are a terror to face unprepared."
        };


        Plants = new RaceTraits()
        {
            BodySize = 8,
            StomachSize = 11,
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
            RaceDescription = ""
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
            RaceDescription = ""
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
            RaceDescription = ""
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
                Mind = new RaceStats.StatRange(20, 24),
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
            RaceDescription = "Tiny insects grown to a slightly larger size, the Ants still wouldn't be considered dangerous were it not for their ability to swallow and carry things hundred times their own size."
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
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 18),
                Dexterity = new RaceStats.StatRange(8, 16),
                Endurance = new RaceStats.StatRange(12, 16),
                Mind = new RaceStats.StatRange(8, 16),
                Will = new RaceStats.StatRange(8, 16),
                Agility = new RaceStats.StatRange(12, 20),
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
            RaceDescription = ""
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
                Endurance = new RaceStats.StatRange(16, 24),
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
            RaceDescription = ""
        };

        SpringSlugs = new RaceTraits()
        {
            BodySize = 6,
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
            RaceDescription = ""
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
            RaceDescription = ""
        };

        CoralSlugs = new RaceTraits()
        {
            BodySize = 10,
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
            RaceDescription = ""
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
            RaceDescription = ""
        };

        Mantis = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 12,
            HasTail = false,
            FavoredStat = Stat.Strength,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth },
            ExpMultiplier = 1.2f,
            PowerAdjustment = 1.5f,
            DeployCost = 1,
            Upkeep = 5f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 28),
                Dexterity = new RaceStats.StatRange(12, 20),
                Endurance = new RaceStats.StatRange(8, 12),
                Mind = new RaceStats.StatRange(6, 10),
                Will = new RaceStats.StatRange(6, 10),
                Agility = new RaceStats.StatRange(12, 20),
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
            RaceDescription = ""
        };

        EasternDragon = new RaceTraits()
        {
            BodySize = 20,
            StomachSize = 20,
            HasTail = true,
            FavoredStat = Stat.Mind,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.CockVore, VoreType.Unbirth },
            ExpMultiplier = 1.6f,
            PowerAdjustment = 1.9f,
            DeployCost = 1,
            Upkeep = 1f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(12, 22),
                Dexterity = new RaceStats.StatRange(8, 16),
                Endurance = new RaceStats.StatRange(22, 23),
                Mind = new RaceStats.StatRange(14, 30),
                Will = new RaceStats.StatRange(8, 18),
                Agility = new RaceStats.StatRange(14, 28),
                Voracity = new RaceStats.StatRange(12, 22),
                Stomach = new RaceStats.StatRange(12, 22),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Flight,
                Traits.Ravenous,
                Traits.AdeptLearner,
                Traits.StrongGullet,
                Traits.Maul,
        },
            RaceDescription = "A variety of dragons especially attuned to magic, the Eastern Dragons, or Lung Dragons as they are also known as, are able to fly without wings. Reminiscent of snakes, the Eastern Dragons are readily able to prove that the resemblance is more than skin deep, devouring large prey with ease.",

        };

        Catfish = new RaceTraits()
        {
            BodySize = 16,
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
            RaceDescription = ""
        };

        Raptor = new RaceTraits()
        {
            BodySize = 5,
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
            BodySize = 8,
            StomachSize = 14,
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
            RaceDescription = ""
        };

        Gazelle = new RaceTraits()
        {
            BodySize = 18,
            StomachSize = 16,
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
                Agility = new RaceStats.StatRange(16, 24),
                Voracity = new RaceStats.StatRange(10, 16),
                Stomach = new RaceStats.StatRange(10, 16),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Charge,
            Traits.ForcefulBlow,
        },
            RaceDescription = ""
        };

        Earthworms = new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 16,
            HasTail = false,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.0f,
            PowerAdjustment = 1.0f,
            DeployCost = 1,
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 12),
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
            RaceDescription = ""
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
            RaceDescription = ""
        };

        Monitors = new RaceTraits()
        {
            BodySize = 17,
            StomachSize = 17,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
            PowerAdjustment = 1.3f,
            DeployCost = 1,
            Upkeep = 4f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(12, 20),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(12, 20),
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
            RaceDescription = "",
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
            Upkeep = 2f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(10, 14),
                Dexterity = new RaceStats.StatRange(8, 12),
                Endurance = new RaceStats.StatRange(12, 16),
                Mind = new RaceStats.StatRange(4, 6),
                Will = new RaceStats.StatRange(8, 12),
                Agility = new RaceStats.StatRange(10, 14),
                Voracity = new RaceStats.StatRange(10, 14),
                Stomach = new RaceStats.StatRange(10, 14),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Resilient,
                Traits.Disgusting
        },
            RaceDescription = "A tough, twisted creature. Hunts for pleasure rather than sustenance.",
        };

        Terrorbirds = new RaceTraits()
        {
            BodySize = 18,
            StomachSize = 18,
            HasTail = true,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.TailVore },
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
            RaceDescription = ""
        };

        Dratopyr = new RaceTraits()
        {
            BodySize = 9,
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
            StomachSize = 16,
            HasTail = true,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
            ExpMultiplier = 1.1f,
            PowerAdjustment = 1.3f,
            DeployCost = 1,
            Upkeep = 3f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(14, 18),
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
            RaceDescription = "It's a horse!  Go ahead, try to ride one.  I dare you!"
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
            Traits.Resilient,
        },
            RaceDescription = "Abnormally large foxes with a voracious appetite."
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
                Endurance = new RaceStats.StatRange(17, 23),
                Mind = new RaceStats.StatRange(7, 11),
                Will = new RaceStats.StatRange(12, 18),
                Agility = new RaceStats.StatRange(13, 19),
                Voracity = new RaceStats.StatRange(19, 27),
                Stomach = new RaceStats.StatRange(21, 29),
            },
            RacialTraits = new List<Traits>()
        {
            Traits.Disgusting,
            Traits.Resilient,
            Traits.Pounce,
            Traits.FireVulnerable,
        },
            RaceDescription = "A vicious and territorial race of voracious insects prized for their ability to produce an element known as E-710, though some say it's just oil.  They are very dangerous, especially so in larger numbers and will often attempt to swarm their prey.  No known portal signified their arrival in the realm so how they ended up here is anybody's guess, though a few conspiracy rumors claim they were brought here by someone else.  There is a curious phenomenon surrounding these insects: When struck by them in combat, there is a high likelyhood that the wounded will shout \"No pain, no freedom!\""
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
            Upkeep = 6f,
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
            RaceDescription = "Orcas that have mutated to fly and breathe air on their homewrold. They've also developed a ravenous appetite.  The Skysharks followed the Scylla to this world, and the Orcas followed the Skyharks.",
        };

        Otachi = new RaceTraits()
        {
            BodySize = 16,
            StomachSize = 24,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.CockVore, VoreType.Unbirth },
            ExpMultiplier = 1.6f,
            PowerAdjustment = 1.3f,
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
            Traits.Disgusting,
            Traits.Resilient,
            Traits.Pounce,
            Traits.FireVulnerable,
        },
            RaceDescription = "Somehow a Kaiju! (Stats copied from Terminids for now)"
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

            },
            InnateSpells = new List<SpellTypes>() { SpellTypes.IceBlast },
            RaceDescription = "A hybrid between a dragon and salamander whom excels in climbing and swimming but lacks any wings for flight.",

        };

        Vision = new RaceTraits()
        {
            BodySize = 16,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 2f,
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
            BodySize = 24,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 2.4f,
            PowerAdjustment = 4f,
            DeployCost = 1,
            Upkeep = 24f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 24),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(16, 24),
                Mind = new RaceStats.StatRange(16, 20),
                Will = new RaceStats.StatRange(12, 18),
                Agility = new RaceStats.StatRange(16, 28),
                Voracity = new RaceStats.StatRange(16, 24),
                Stomach = new RaceStats.StatRange(16, 24),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.Flight,
                Traits.Biter,
                Traits.FastAbsorption
        },
            InnateSpells = new List<SpellTypes>() { SpellTypes.Pyre },
            RaceDescription = "A cruel gluttonous red wyvern",
        };


        Asura = new RaceTraits()
        {
            BodySize = 18,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Strength,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.Anal },
            ExpMultiplier = 2.4f,
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
            CanUseRangedWeapons = false,
        };

        DRACO = new RaceTraits()
        {
            BodySize = 22,
            StomachSize = 30,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 2.4f,
            PowerAdjustment = 4f,
            DeployCost = 1,
            Upkeep = 14f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(20, 24),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(16, 24),
                Mind = new RaceStats.StatRange(16, 20),
                Will = new RaceStats.StatRange(12, 18),
                Agility = new RaceStats.StatRange(16, 28),
                Voracity = new RaceStats.StatRange(16, 24),
                Stomach = new RaceStats.StatRange(18, 26),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.MetalBody,
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
            BodySize = 90,
            StomachSize = 40,
            HasTail = true,
            FavoredStat = Stat.Stomach,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth },
            ExpMultiplier = 3.2f,
            PowerAdjustment = 7f,
            DeployCost = 4,
            Upkeep = 30f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(24, 28),
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
                Strength = new RaceStats.StatRange(20, 24),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(16, 24),
                Mind = new RaceStats.StatRange(16, 20),
                Will = new RaceStats.StatRange(12, 18),
                Agility = new RaceStats.StatRange(16, 28),
                Voracity = new RaceStats.StatRange(16, 24),
                Stomach = new RaceStats.StatRange(16, 24),
            },
            RacialTraits = new List<Traits>()
        {
                Traits.StrongGullet,
                Traits.ArtfulDodge,
                Traits.NimbleClimber,
                Traits.BornToMove
        },
            RaceDescription = "A devious and voracious wyvern. Known for his agility and cunning, don't ever turn your back to him or you might find yourself in trouble.",
        };

        Auri = new RaceTraits()
        {
            BodySize = 16,
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
            BodySize = 13,
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
		
		Goodra = new RaceTraits()
        {
            BodySize = 32,
            StomachSize = 50,
            HasTail = true,
            FavoredStat = Stat.Stomach,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 6f,
            PowerAdjustment = 12f,
            DeployCost = 1,
            Upkeep = 10f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(16, 24),
                Dexterity = new RaceStats.StatRange(8, 14),
                Endurance = new RaceStats.StatRange(32, 40),
                Mind = new RaceStats.StatRange(12, 20),
                Will = new RaceStats.StatRange(16, 24),
                Agility = new RaceStats.StatRange(6, 10),
                Voracity = new RaceStats.StatRange(16, 24),
                Stomach = new RaceStats.StatRange(32, 40),
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
            RaceDescription = "Goodra, the Slug Dragon Pokemon. Goodra are large soft dragon type pokemon coated in slime. They love to give hugs and often confuse friends from food."
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
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.Anal},
            ExpMultiplier = 1.2f,
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
            BodySize = 20,
            StomachSize = 25,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 2f,
            PowerAdjustment = 4f,
            DeployCost = 1,
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
                Traits.Tasty,
        },
            RaceDescription = "A strange, almost draconic looking raptor. She seems to grow larger and stronger with each victim consumed. Despite having wings, they seem to only allow her to leap great distances instead of fly.",
        };

        Taraluxia = new RaceTraits()
        {
            BodySize = 120,
            StomachSize = 100,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal },
            ExpMultiplier = 6f,
            PowerAdjustment = 15f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(35, 45),
                Dexterity = new RaceStats.StatRange(20, 30),
                Endurance = new RaceStats.StatRange(44, 62),
                Mind = new RaceStats.StatRange(20, 32),
                Will = new RaceStats.StatRange(90, 110),
                Agility = new RaceStats.StatRange(20, 30),
                Voracity = new RaceStats.StatRange(40, 50),
                Stomach = new RaceStats.StatRange(25, 35),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.Legendary,
                Traits.Maul,
                Traits.Flight,
                Traits.Intimidating,
                Traits.ColdTolerance,
                Traits.TailStrike,

            },
            InnateSpells = new List<SpellTypes>() { SpellTypes.IceBlast },
            RaceDescription = "Hehe, Ice dragon goes brr",

        };

        Xelhilde = new RaceTraits()
        {
            BodySize = 11,
            StomachSize = 15,
            FavoredStat = Stat.Strength,
            HasTail = true,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal},
            ExpMultiplier = 1.4f,
            PowerAdjustment = 2f,
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

        BoomBunnies = new RaceTraits()
        {
            BodySize = 5,
            StomachSize = 10,
            HasTail = true,
            FavoredStat = Stat.Agility,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth},
            ExpMultiplier = 1f,
            PowerAdjustment = .75f,
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
    //internal List<Traits> RandomTraits;
    internal List<VoreType> AllowedVoreTypes = new List<VoreType> { VoreType.Anal, VoreType.Oral, VoreType.CockVore, VoreType.BreastVore, VoreType.Unbirth };
    internal Race SpawnRace = Race.none;
    internal Race ConversionRace = Race.none;
    internal Race LeaderRace = Race.none;
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
