using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class NameGenerator
{

    List<string> femaleNames;
    List<string> maleNames;
    List<string> monsterNames;

    List<string> catTownNames;
    List<string> dogTownNames;
    List<string> foxTownNames;
    List<string> youkoTownNames;
    List<string> wolfTownNames;
    List<string> bunnyTownNames;
    List<string> bunnyPreyTownNames;
    List<string> lizardTownNames;
    List<string> slimeTownNames;
    List<string> scyllaTownNames;
    List<string> harpyTownNames;
    List<string> impTownNames;
    List<string> humanTownNames;
    List<string> crypterTownNames;
    List<string> lamiaTownNames;
    List<string> kangarooTownNames;
    List<string> taurusTownNames;
    List<string> cruxTownNames;
    List<string> equinesTownNames;
    List<string> sergalTownNames;
    List<string> beeTownNames;
    List<string> driderTownNames;
    List<string> alrauneTownNames;
    List<string> batTownNames;
    List<string> pantherTownNames;
    List<string> merfolkTownNames;
    List<string> avianTownNames;
    List<string> antTownNames;
    List<string> frogTownNames;
    List<string> sharkTownNames;
    List<string> deerTownNames;
    List<string> aabayxTownNames;
    List<string> miceTownNames;
    List<string> matronsminionsTownNames;

    List<string> compyNames;
    List<string> vagrantNames;
    List<string> serpentNames;
    List<string> wyvernNames;
    List<string> gryphonNames;
    List<string> feralLionNames;
    List<string> feralOrcasNames;
    
	List<string> goodraNames;
	
    Dictionary<Race, List<string>> RaceMaleNames;
    Dictionary<Race, List<string>> RaceFemaleNames;
    Dictionary<Race, List<string>> RaceMonsterNames;

    Dictionary<Race, List<string>> ArmyNames;

    Dictionary<Race, string> ArmyNameDefault;

    public NameGenerator()
    {
        femaleNames = new List<string>
        {
            "Ai",
            "Aika",
            "Aiko",
            "Aimi",
            "Aina",
            "Aisa",
            "Aki",
            "Aoi",
            "Asami",
            "Atsuko",
            "Aya",
            "Ayumi",
            "Chie",
            "Chinami",
            "Chitose",
            "Eiko",
            "Emi",
            "Erika",
            "Fumiko",
            "Fusako",
            "Hana",
            "Haruko",
            "Hikari",
            "Hisa",
            "Honoka",
            "Ichiko",
            "Itsumi",
            "Juria",
            "Junko",
            "Kaho",
            "Kanna",
            "Kaneko",
            "Kaori",
            "Karin",
            "Kasumi",
            "Katsue",
            "Keiki",
            "Kira",
            "Kiyoshi",
            "Kotoe",
            "Kotomi",
            "Kurumi",
            "Kyoko",
            "Madoka",
            "Maiko",
            "Mari",
            "Masae",
            "Mayuko",
            "Miiko",
            "Mimori",
            "Minae",
            "Miu",
            "Miwa",
            "Moeko",
            "Nanae",
            "Natsuko",
            "Natsumi",
            "Nonoka",
            "Orime",
            "Reiko",
            "Reina",
            "Rie",
            "Riho",
            "Rumi",
            "Ryoko",
            "Sachie",
            "Sae",
            "Saeko",
            "Saki",
            "Sakura",
            "Sanae",
            "Saori",
            "Sawako",
            "Shiho",
            "Shizuko",
            "Sumie",
            "Tamani",
            "Terumi",
            "Tomoyo",
            "Toya",
            "Tsuru",
            "Umeko",
            "Yasuko",
            "Yuria",
            "Yoshiko",
            "Yukari",
            "Yuko",
            "Yuri",
        };
        monsterNames = new List<string>
        {
            "No names found",
        };
        maleNames = new List<string>
        {
            "Akihiro",
            "Akio",
            "Daiki",
            "Daishi",
            "Fumihiro",
            "Futoshi",
            "Genta",
            "Hakaru",
            "Haruto",
            "Hideki",
            "Hidemasa",
            "Hirotaka",
            "Hisao",
            "Itaru",
            "Jin",
            "Junpei",
            "Kaiji",
            "Kansuke",
            "Kazuma",
            "Kotaru",
            "Masaichi",
            "Minoru",
            "Munetoki",
            "Nagahide",
            "Naritaka",
            "Nobuo",
            "Noriaki",
            "Ryu",
            "Sadaaki",
            "Sadakazu",
            "Seigo",
            "Shigemi",
            "Shinta",
            "Suguru",
            "Tadafumi",
            "Tadanari",
            "Takayoshi",
            "Taketo",
            "Teiji",
            "Tetsuya",
            "Toru",
            "Tsuneo",
            "Yasuki",
            "Yoriyuki",
            "Yoshimoto",
            "Yukishige",
        };
        compyNames = new List<string>
        {
            "Chicky",
            "Pippi",
            "Peep",
            "Smiley",
            "Micky",
            "Dingy",
            "Dongy",
            "Wonky",
            "Wanky",
            "Ashby",
            "Pop",
            "Pin",
            "Sticky",
            "Smelly",
            "Stinky",
            "Chirp",
            "Chip",
            "Smore",
            "Burpy",
            "Scummy",
            "Donny",
            "Whin",
            "Musti",
            "Reksi",
            "Rexy",
            "Snack",
            "Spot",
            "Rip",
            "Poopy",
        };
        vagrantNames = new List<string>
        {
            "Tenty",
            "Stingy",
            "Rubby",
            "Gulfy",
            "Weedy",
            "Squishy",
            "Waddle",
            "Waddly",
            "Domy",
        };
        serpentNames = new List<string>
        {
            "Snaky",
            "Snoot",
            "Snek",
            "Slither",
            "Viper",
            "Tongy",
            "Bity",
            "Bulgy",
            "Huggy",
            "Nippy",
        };
        wyvernNames = new List<string>
        {
            "Swiftwing",
            "Deathtalon",
            "Sharpbeak",
            "Spineback",
        };
        gryphonNames = new List<string>
        {
            "Aquila",
            "Harpia",
            "Accipiter",
            "Kirkos",
            "Cathartes",
            "Necrosyrtes",
            "Neophron",
            "Sarcogyps",
            "Elanus",
            "Milvus",
            "Haliastur",
            "Pandion",
            "Buteo",
            "Falco",
            "Harpagus",
            "Milvago",
            "Caracara",
            "Ibycter",
            "Daptrius",
            "Ictinia",
            "Minerva",
            "Aegolius",
            "Sagittarius",
            "Lanius",
            "Vultur",
            "Surnia",
            "Strix",
            "Pulsatrix",
            "Ninox",
            "Ealonides",
            "Dryotriorchis",
            "Casuarius",
        };
        feralLionNames = new List<string>
        {
            "Kalahari",
            "Okavangu",
            "Zenobia"
        };
        catTownNames = new List<string>
        {
            "Pyramid of Indulgence",
            "Catro",
            "Meowixandria",
            "Feliyum",
            "Al Bastet",
            "Catazig",
            "Pursia",
            "Palace of Decedance",
            "Yarnodos",
            "Meopolis",
            "Catolomeic Palace",
            "Catopolis",
            "Catville",
            "Nekotown",
            "Meowscow",
            "Caturdayton",
        };
        dogTownNames = new List<string>
        {
            "The Seat",
            "Flockgard",
            "Doggendorf",
            "Barkburg",
            "Mongrelheim",
            "Heel",
            "Loyalgard",
            "Shibustein",
            "Labberdoodle",
            "Pitgard",
            "Doberia",
            "Dogville",
            "Inutown",
            "Dogington",
            "Doghouse",
        };
        foxTownNames = new List<string>
        {
            "Vulpeska",
            "Trickster's Den",
            "Foxsaw",
            "Den of Cunning",
            "Fennecow",
            "Trickstadov",
            "Trapper's Den",
            "Caniska",
            "Yelpitz",
            "Valley of Deceit",
            "Preyland",
            "Den of the Ruthless",
            "Den of Gnarling",
        };
        youkoTownNames = new List<string>
        {
            "Takama-ga-hara",
            "Yamato-no-shi",
            "Inari-no-shi",
            "Zenko-no-machi",
            "Yakan-no-machi",
            "Ooji-no-machi",
            "Onji-no-machi",
        };
        wolfTownNames = new List<string>
        {
            "Pax Lupus",
            "Fort Fang",
            "Tribe of the Moon",
            "Camp Claw",
            "Wolfenburg",
            "Tooth Tribe",
            "Pelt Keep",
            "Lunar Palace",
            "The Howling Castle",
            "The Howling Tribe",
            "Blood Claw Tribe",
            "The Gurgling Steppe",
            "House of the Pack",
            "Gnashing Hill",
            "Wailing Gut Tribe",
            "Famine's End",
        };
        bunnyTownNames = new List<string>
        {
            "Hoppington",
            "Lopdon",
            "Bunburg",
            "Pawdale",
            "Rabiton",
            "Watershed",
            "Cottontail Cove",
        };
        bunnyPreyTownNames = new List<string>
        {
            "The Warren",
            "Underbrush Shelter",
            "Tree Hollow",
            "Hidden Haven",
            "Sanctuary",
            "Bunny Burrow",
            "Felt Burrow",
            "Carrot Burrow",
        };
        lizardTownNames = new List<string>
        {
            "Lizotetca",
            "Reptula",
            "Lair of Bone",
            "Chalscale",
            "Komodoalco",
            "Lair of Venom",
            "Vale of Broken Teeth",
            "Crocotula",
            "Iguatepec",
            "Lair of the Beast",
            "Cult of the Wyrm",
            "Dragon Tongue",
        };
        slimeTownNames = new List<string>
        {
            "The Nucleus",
            "Gootopia",
            "Slimesville",
            "Ectopolis",
            "Blobsburg",
            "Petri Town",
            "Splaton",
        };
        scyllaTownNames = new List<string>
        {
            "City of the Conch",
            "Lost City of Vorantis",
            "The Trident",
            "Seafoama",
            "The Tidepool",
            "Clamonia",
            "Baiae",
            "Olous",
            "Heracleion",
            "Pavlopetri",
            "Ravenser",
            "Atlit Yum",
        };
        harpyTownNames = new List<string>
        {
            "The Erinyes",
            "Strophades",
            "Skycliffe Roost",
            "City of Hargos",
            "Mount Zephyr",
            "Oracle of Arphi",
            "Talontium",
            "Feathesus",
            "Nestoli",
            "Papida",
            "Mount Hesiod",
            "Cult of Aeneid",
            "Cave of Argo",
            "Cave of Orcus",
        };
        impTownNames = new List<string>
        {
            "Demongate",
            "Cinderpool",
            "Ashburn",
            "Hellbreath",
            "Stygia",
            "Blackden",
            "Gehenna",
            "Funtown",
        };
        humanTownNames = new List<string>
        {
            "Bastion",
            "Bulwark",
            "The Iron Stronghold",
            "Forlorn Citadel",
            "Fort Dauntless",
            "Homestead",
            "The Partition",
            "Gold Hand Redoubt",
            "Fort Hera",
            "Boldenholm",
            "Grace",
        };
        crypterTownNames = new List<string>
        {
            "The Eternal Palace",
            "Citadel of Divine Purpose",
            "Citadel of Syn",
            "Ur-Babel Arisen",
            "Citadel of Talmund",
            "Gullet of Shem",
            "Crucible of Consumption",
            "Devouring Catalyst",
            //Sleeping below this, but not implemented separately
            "Tongue of Shem",
            "Crypt of the Undying Queen",
            "Crypt of Testament",
            "Tomb of Doubt",
            "Tomb of Syn",
            "Ruins of Ur-Babel",
            "Tomb of Talmund",
        };
        lamiaTownNames = new List<string>
        {
            "City of Brass",
            "Sthenopoli",
            "Poena",
            "Fields of Elysium",
            "Cult of Bronze",
            "Lair of Gorgon",
            "Echidnadon",
            "Nagapolis",
            "Fountain of Woe"
        };
        kangarooTownNames = new List<string>
        {
            "Roostadt",
            "Pouchbottom",
            "Over'under",
            "Red-dust",
            "Sidney",
            "Marsupia",
            "Ayer",
            "Guardia",
            "Uluroo",
        };
        taurusTownNames = new List<string>
        {
            "Minos",
            "Fourbelly",
            "Beefsburg",
            "Udderlife",
            "Rangeton",
            "Salisbury",
            "Cuddington",
        };
        cruxTownNames = new List<string>
        {
            "The Gate",
            "Facility 789",
            "Ingenuity",
            "Cruxus",
            "Facility Ornd"
        };
        equinesTownNames = new List<string>
        {
            "Cataphracta",
            "Equus",
            "The Ranch",
            "Haciendo",
            "Alfarsan"
        };
        sergalTownNames = new List<string>
        {
            "Glorious City of Eltus",
            "Gold Ring",
            "Astna",
            "Tesae",
            "Col Hazma",
            "Adzma",
            "Nihazama",
            "Lon Sodd",
            "Reono",
            "Etai",
            "Magoe",
            "Salt Outpost"
        };
        beeTownNames = new List<string>
        {
            "Queen's Hive",
            "Honeycomb",
            "Buzzytown",
            "Golden Orchard",
            "Waxville",
            "Bumblebee Grove",
            "Beepolis",
            "Maya's Meadow",
            "Sweet Pollen",
            "Black Swarm",
            "Royal Jelly",
            "Workers Hive",
            "Apiarist Respite"
        };
        driderTownNames = new List<string>
        {
            "Arachnos",
            "Weaverville",
            "Silkroad",
            "Tarantulos",
            "Shelob's Lair",
            "Araneae",
            "Webbington",
            "Dark Caves",
            "Flytrap",
            "Net town",
            "Aragog Forest",
            "Spiderverse",
        };
        alrauneTownNames = new List<string>
        {
            "Yggdrasill",
            "Evergarden",
            "Rosewood",
            "Apple Grove",
            "Gracefields",
            "Edenia",
            "Ivydale",
            "Magnolia",
            "Cedarville",
            "Fiore",
            "Trees of Valinor",
            "Green Haven",
        };

        batTownNames = new List<string>
        {
            "Dark Grotto",
            "Black Hollow",
            "Nightpoint",
            "Echoing Cavern",
            "Gotham",
            "Batville",
            "Crystal Cave",
            "Deep Den",
            "Sylvania",
            "Strygos",
        };
        pantherTownNames = new List<string>
        {
            "Panthera",
            "Wakana",
            "Therishi",
            "Vogoma",
            "Kwa-Duka",
            "Hlobava",
            "Plundi",
            "Pumatra",
            "Khangela",
            "Qweni-Sho",
            "Sitting Belly",
            "Endless Feast",
            "Gurgling Tents",
        };

        merfolkTownNames = new List<string>
        {
            "Neo Atlantis",
            "Rapture",
            "Templemer",
            "Reefsong",
            "Numenor",
            "Thalassa",
            "Amphitrite",
            "Triton",
            "Nautica",
            "Poseidonia",
            "Neptunia",
            "Ulthuan",
            "Nereidia",
            "Pontus",
            "Eurybia"
        };

        avianTownNames = new List<string>
        {
            "Phoenix Nest",
            "Thunderbird City",
            "Swallow Falls",
            "Dovechester",
            "Garuda Rapids",
            "Duck Pond",
            "Cockatricefields",
            "Horus Springs",
            "Rocville",
            "Thothland",
            "Fort Aethon",
            "Siren Song",
            "Tengu Hills",
            "Strixport",
            "Eagleburg",
            "Flamingo Bay"
        };

        antTownNames = new List<string>
        {
            "Queen's Mound",
            "Formicia",
            "Antville",
            "Red Hill",
            "Leafcutter Heap",
            "Sugar Farm",
            "Paraponera City",
            "Myrmidon Point",
            "Bivouac Nest",
            "Fire Ant Fort",
            "Larvaburg",
            "Needlefields",
            "Flik's Town",
            "Lasius Rock"
        };

        frogTownNames = new List<string>
        {
            "Frogpolis",
            "Buzzing Bog",
            "Lily Pond",
            "Evermire",
            "Green Lake",
            "Toadburg",
            "Muddy Pool",
            "Flycatcher Swamp",
            "Tadpole City",
            "Misty Tarn",
            "Froppyville",
            "Willow Wetlands",
            "Loch Bufo",
            "Murky Puddle",
            "Soggy Marsh",
            "Backwater Borough",
            "Sawgrass Slough"
        };

        sharkTownNames = new List<string>
        {
            "Port Royal",
            "Tortuga",
            "New Providence",
            "Black Pearl",
            "Barbary Coast",
            "Isla De Muerta",
            "Barataria Bay",
            "Porto Farina",
            "Shipwreck Cove",
            "Dreaded Stronghold",
            "Jolly Roger",
            "Clew Bay",
            "Whydah",
            "Stormfort",
            "Kraken Keep",
            "Devil's Maw",
            "Sunken Trove",
            "Privateer's Lagoon",
            "Wanderer's Hideout",
            "Blackened Den"
        };

        deerTownNames = new List<string>
        {
            "Artemis Woods",
            "The Golden Stag",
            "Elkfurt",
            "Hoovechester",
            "Buckville",
            "The White Doe",
            "Cernunnos",
            "Dappled Hide",
            "Ceryneia",
            "Hindburg",
            "Peryton",
            "Antlertown",
            "Swiftbrook",
            "Red Hart",
            "Eikthyrnir",
            "Actaeon",
            "Darbywood",
            "Rohit",
            "Furfur",
            "Achlis"
        };
		goodraNames = new List<string>
        {
            "Goodra",
            "Sligoo",
            "Goomy",
            "Gooey",
            "Hugs",
            "Huggy",
            "Slimy",
            "Pudding",
            "Gumdrops",
            "Jelly",
            "Dragooze",
            "Escargoo",
            "Squishy",
            "Goober",
            "Oozy",
            "Goopy",
            "Nuri",
            "Dragoonite",
            "Spygoo",
            "Dragoo",
            "Gooigi",
        };
        aabayxTownNames = new List<string>
        {
            "Akaryocyte",  ///(this one is the capital, by the way)
            "Infection Site Zero",
            "Arenai",
            "Temple of Twenty",
            "Adnaviria",
            "Ysynsr aaz Lextrnl's Domain",
            "Duplodnaviria",
            "Bacteriophage",
            "Monodnaviria",
            "Aychkaynienteeseven",
            "Riboviria",
            "Capsid",
            "Ribozyviria",
            "Lipid Envelope",
            "Varidnaviria",
            "Mimi",
            "Tevenvirinae",
            "Myoviridae",
            "Podoviridae",
            "Autographiviridae",
        };
        miceTownNames = new List<string>
        {
            "Goldhollow",
            "Nibblewood",
            "Mousetail Meadow",
            "Squeakridge",
            "Cheesemere",
            "Furrowfield",
            "Silverwhisker",
            "Squeakstone",
            "Wheatenwood",
            "Thimbleholt",
            "Mousenook",
            "Cheddarcliff",
            "Cottagecreek",
            "Blueburrow",
            "Swisswood",
            "Rustle Warren",
            "Tanglewood",

        };
        matronsminionsTownNames = new List<string>
        {
            "Matron's Rest",
            "Giant's Pass",
            "Her Tranquility",
            "Aged Gate",
            "Fateland",
            "Grave of All",
            "Dragon's Lounge",
            "Beaten Path",
            "Lair of the Lady",
            "Ancestor's Pit",
        };

        Encoding encoding = Encoding.GetEncoding("iso-8859-1");

        if (File.Exists($"{State.StorageDirectory}males.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}males.txt", encoding);
            if (logFile.Any())
                maleNames = new List<string>(logFile);
        }

        if (File.Exists($"{State.StorageDirectory}females.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}females.txt", encoding);
            if (logFile.Any())
                femaleNames = new List<string>(logFile);
        }

        if (File.Exists($"{State.StorageDirectory}monsters.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}monsters.txt", encoding);
            if (logFile.Any())
                monsterNames = new List<string>(logFile);
        }

        ArmyNames = new Dictionary<Race, List<string>>();
        ArmyNameDefault = new Dictionary<Race, string>();

        if (File.Exists($"{State.StorageDirectory}armyNames.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}armyNames.txt", encoding);
            bool expectingdefault = false;
            Race currentRace = Race.Cats;
            foreach (string entry in logFile)
            {
                if (entry.Length > 0)
                {
                    if (entry.StartsWith("The first entry"))
                        continue;
                    if (Enum.TryParse(entry, out Race race))
                    {
                        currentRace = race;
                        expectingdefault = true;
                    }
                    else if (expectingdefault)
                    {
                        ArmyNameDefault[currentRace] = entry;
                        expectingdefault = false;
                    }
                    else
                    {
                        if (ArmyNames.ContainsKey(currentRace))
                        {
                            ArmyNames[currentRace].Add(entry);
                        }
                        else
                        {
                            ArmyNames[currentRace] = new List<string>() { entry };
                        }
                    }
                }
            }
        }

        RaceMaleNames = new Dictionary<Race, List<string>>();
        RaceFemaleNames = new Dictionary<Race, List<string>>();
        RaceMonsterNames = new Dictionary<Race, List<string>>();

        foreach (Race race in (Race[])Enum.GetValues(typeof(Race)))
        {
            if (File.Exists($"{State.StorageDirectory}male{race}.txt"))
            {
                var logFile = File.ReadAllLines($"{State.StorageDirectory}male{race}.txt", encoding);
                var names = new List<string>(logFile);
                RaceMaleNames[race] = names;
            }
            if (File.Exists($"{State.StorageDirectory}female{race}.txt"))
            {
                var logFile = File.ReadAllLines($"{State.StorageDirectory}female{race}.txt", encoding);
                var names = new List<string>(logFile);
                RaceFemaleNames[race] = names;
            }
            if (File.Exists($"{State.StorageDirectory}{race}.txt"))
            {
                var logFile = File.ReadAllLines($"{State.StorageDirectory}{race}.txt", encoding);
                var names = new List<string>(logFile);
                RaceMonsterNames[race] = names;
            }
        }

    }

    public string GetArmyName(Race race, Village village)
    {
        if (State.Rand.Next(10) == 0)
        {
            if (ArmyNames.ContainsKey(race))
            {
                List<string> items = new List<string>();
                foreach (var item in ArmyNames[race])
                {
                    items.Add(item);
                }
                var pick = items[State.Rand.Next(items.Count)];
                if (StrategicUtilities.GetAllArmies().Where(s => s.Name == pick).Any() == false)
                {
                    return pick;
                }
            }
        }
        if (village != null && ArmyNameDefault.ContainsKey(race))
        {
            for (int i = 0; i < 9; i++)
            {
                var name = $"{AddOrdinal(State.Rand.Next(1, 100))} {village.Name} {ArmyNameDefault[race]}";
                if (StrategicUtilities.GetAllArmies().Where(s => s.Name == name).Any() == false)
                    return name;
            }
            return $"{AddOrdinal(State.Rand.Next(1, 100))} {village.Name} {ArmyNameDefault[race]}";
        }
        return "";

        string AddOrdinal(int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }

        }
    }

    public string GetMonsterName(bool male, Race race)
    {
        List<string> list;
        if (male)
        {
            RaceMaleNames.TryGetValue(race, out list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }
        }
        else
        {
            RaceFemaleNames.TryGetValue(race, out list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }
        }
        RaceMonsterNames.TryGetValue(race, out list);
        if (list != null && list.Any())
        {
            return list[State.Rand.Next(list.Count)];
        }
        list = null;
        switch (race)
        {
            case Race.Wyvern:
                list = wyvernNames;
                break;
            case Race.Vagrants:
                list = vagrantNames;
                break;
            case Race.Serpents:
                list = serpentNames;
                break;
            case Race.Compy:
                list = compyNames;
                break;
            case Race.Gryphons:
                list = gryphonNames;
                break;
			case Race.Goodra:
                list = goodraNames;
                break;
        }
        if (list != null)
        {
            if (list.Count > 20)
                return list[State.Rand.Next(list.Count)];
            int rand = State.Rand.Next(20);
            if (rand < list.Count)
                return list[rand];
        }
        return monsterNames[State.Rand.Next(monsterNames.Count)];

    }

    public string GetName(bool male, Race race)
    {

        int r;
        if (male)
        {
            RaceMaleNames.TryGetValue(race, out var list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }
            r = State.Rand.Next(maleNames.Count);
            return maleNames[r];
        }
        else
        {
            RaceFemaleNames.TryGetValue(race, out var list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }
            r = State.Rand.Next(femaleNames.Count);
            return femaleNames[r];
        }


    }

    public string GetTownName(Race race, int i)
    {
        if (race == Race.Cats)
        {
            if (i >= 0 && i < catTownNames.Count)
                return catTownNames[i];
        }
        else if (race == Race.Dogs)
        {
            if (i >= 0 && i < dogTownNames.Count)
                return dogTownNames[i];
        }
        else if (race == Race.Foxes)
        {
            if (i >= 0 && i < foxTownNames.Count)
                return foxTownNames[i];
        }
        else if (race == Race.Youko)
        {
            if (i >= 0 && i < youkoTownNames.Count)
                return youkoTownNames[i];
        }
        else if (race == Race.Wolves)
        {
            if (i >= 0 && i < wolfTownNames.Count)
                return wolfTownNames[i];
        }
        else if (race == Race.Bunnies)
        {
            if (i >= 0 && i < bunnyTownNames.Count)
                return bunnyTownNames[i];
        }
        else if (race == Race.Lizards)
        {
            if (i >= 0 && i < lizardTownNames.Count)
                return lizardTownNames[i];
        }
        else if (race == Race.Slimes)
        {
            if (i >= 0 && i < slimeTownNames.Count)
                return slimeTownNames[i];
        }
        else if (race == Race.Scylla)
        {
            if (i >= 0 && i < scyllaTownNames.Count)
                return scyllaTownNames[i];
        }
        else if (race == Race.Harpies)
        {
            if (i >= 0 && i < harpyTownNames.Count)
                return harpyTownNames[i];
        }
        else if (race == Race.Imps)
        {
            if (i >= 0 && i < impTownNames.Count)
                return impTownNames[i];
        }
        else if (race == Race.Humans)
        {
            if (i >= 0 && i < humanTownNames.Count)
                return humanTownNames[i];
        }
        else if (race == Race.Crypters)
        {
            if (i >= 0 && i < crypterTownNames.Count)
                return crypterTownNames[i];
        }
        else if (race == Race.Lamia)
        {
            if (i >= 0 && i < lamiaTownNames.Count)
                return lamiaTownNames[i];
        }
        else if (race == Race.Kangaroos)
        {
            if (i >= 0 && i < kangarooTownNames.Count)
                return kangarooTownNames[i];
        }
        else if (race == Race.Taurus)
        {
            if (i >= 0 && i < taurusTownNames.Count)
                return taurusTownNames[i];
        }
        else if (race == Race.Crux)
        {
            if (i >= 0 && i < cruxTownNames.Count)
                return cruxTownNames[i];
        }
        else if (race == Race.Equines)
        {
            if (i >= 0 && i < equinesTownNames.Count)
                return equinesTownNames[i];
        }
        else if (race == Race.Sergal)
        {
            if (i >= 0 && i < sergalTownNames.Count)
                return sergalTownNames[i];
        }
        else if (race == Race.Bees)
        {
            if (i >= 0 && i < beeTownNames.Count)
                return beeTownNames[i];
        }
        else if (race == Race.Driders)
        {
            if (i >= 0 && i < driderTownNames.Count)
                return driderTownNames[i];
        }
        else if (race == Race.Alraune)
        {
            if (i >= 0 && i < alrauneTownNames.Count)
                return alrauneTownNames[i];
        }
        else if (race == Race.Bats)
        {
            if (i >= 0 && i < batTownNames.Count)
                return batTownNames[i];
        }
        else if (race == Race.Panthers)
        {
            if (i >= 0 && i < pantherTownNames.Count)
                return pantherTownNames[i];
        }
        else if (race == Race.Merfolk)
        {
            if (i >= 0 && i < merfolkTownNames.Count)
                return merfolkTownNames[i];
        }
        else if (race == Race.Avians)
        {
            if (i >= 0 && i < avianTownNames.Count)
                return avianTownNames[i];
        }
        else if (race == Race.Ants)
        {
            if (i >= 0 && i < antTownNames.Count)
                return antTownNames[i];
        }
        else if (race == Race.Frogs)
        {
            if (i >= 0 && i < frogTownNames.Count)
                return frogTownNames[i];
        }
        else if (race == Race.Sharks)
        {
            if (i >= 0 && i < sharkTownNames.Count)
                return sharkTownNames[i];
        }
        else if (race == Race.Deer)
        {
            if (i >= 0 && i < deerTownNames.Count)
                return deerTownNames[i];
        }
        else if (race == Race.Aabayx)
        {
            if (i >= 0 && i < aabayxTownNames.Count)
                return aabayxTownNames[i];
        }        
        else if (race == Race.Mice)
        {
            if (i >= 0 && i < miceTownNames.Count)
                return miceTownNames[i];
        }
        else if (race == Race.MatronsMinions)
        {
            if (i >= 0 && i < matronsminionsTownNames.Count)
                return matronsminionsTownNames[i];
        }


        else if (race == Race.Vagrants)
        {
            return $"Abandoned town {i + 1}";
        }

        return $"{race} town {i + 1}";

    }

    public string GetAlternateTownName(Race race, int i)
    {
        if (race == Race.Bunnies)
        {
            if (i >= 0 && i < bunnyPreyTownNames.Count)
                return bunnyPreyTownNames[i];
        }

        return $"{race} town {i + 1}";

    }

}
