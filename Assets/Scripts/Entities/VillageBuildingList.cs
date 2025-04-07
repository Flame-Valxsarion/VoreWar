using System.Collections.Generic;
using System.Linq;

public enum VillageBuilding : int
{
    mill,
    manor,
    irrigation,
    inn,
    wall,
    trainer,
    empty,
    CapitalDefenses,
    mill2,
    mill3,
    power1,
    power2,
    manor2,
    manor3,
    inn2,
    inn3,
    wall2,
    trainer2,
    trainer3,
    subjugationPen2,
    capitalPalace,
    hangingGardens,
    trainer4,
    market1,
    market2,
    market3,
    MagicGuild,
    Cathedral,
    builder1,
    builder2,
    builder3,
    LastIndex, //Keep this last

}


public static class VillageBuildingList
{
    public static void SetBuildings(bool crazy)
    {
        if (crazy)
            Buildings = crazyBuildings;
        else
            Buildings = normalBuildings;
    }



    public static VillageBuildingDefinition GetBuildingDefinition(VillageBuilding building)
    {
        if (Buildings == null)
            Buildings = normalBuildings;
        Buildings.TryGetValue(building, out VillageBuildingDefinition retBuilding);
        return retBuilding;
    }

    public static List<VillageBuilding> GetListOfBuildingEnum()
    {
        return Buildings.Keys.ToList();
    }

    private static Dictionary<VillageBuilding, VillageBuildingDefinition> _buildings = normalBuildings;

    public static Dictionary<VillageBuilding, VillageBuildingDefinition> Buildings { get => _buildings ?? normalBuildings; set => _buildings = value; }

    public static Dictionary<VillageBuilding, VillageBuildingDefinition> normalBuildings = new Dictionary<VillageBuilding, VillageBuildingDefinition>()
    {
        [VillageBuilding.mill] = new VillageBuildingDefinition(
            VillageBuilding.mill, "Mill",
            "Wealth +20%, Wealth +10"
        )
        {
            Cost = new BuildingCost() { Wealth = 200 },
            Boosts = new VillageBoosts() { WealthMult = 0.2f, WealthAdd = 10 }
        },


        [VillageBuilding.manor] = new VillageBuildingDefinition(
            VillageBuilding.manor, "Manor",
            "Wealth generation +10%, +10% population growth"
        )
        {
            Cost = new BuildingCost() { Wealth = 100 },
            Boosts = new VillageBoosts() { WealthMult = 0.10f, PopulationGrowthMult = .1f }
        },

        [VillageBuilding.irrigation] = new VillageBuildingDefinition(
            VillageBuilding.irrigation, "Irrigated Farms",
            "Better farm yields increase maximum population by 40%"
        )
        {
            Cost = new BuildingCost() { Wealth = 400 },
            Boosts = new VillageBoosts() { PopulationMaxMult = 0.4f }
        },

        [VillageBuilding.inn] = new VillageBuildingDefinition(
            VillageBuilding.inn, "Inn",
            "+100% the heal rate, Allows Mercenaries and Adventurers to visit and be hired"
        )
        {
            Cost = new BuildingCost() { Wealth = 120 },
            Boosts = new VillageBoosts() { HealRateMult = 1.0f, MaxMercsAdd = 2, MercsPerTurnAdd = 1, MaxAdventurersAdd = 6, AdventurersPerTurnAdd = 2, }
        },

        [VillageBuilding.wall] = new VillageBuildingDefinition(
            VillageBuilding.wall, "Wall",
            "Reduces access"
        )
        {
            Cost = new BuildingCost() { Wealth = 60 },
            Boosts = new VillageBoosts() { hasWall = true }
        },

        [VillageBuilding.trainer] = new VillageBuildingDefinition(
            VillageBuilding.trainer, "Trainer",
            "+20 XP to new units trained here. +3 Training level."
        )
        {
            Cost = new BuildingCost() { Wealth = 180 },
            Boosts = new VillageBoosts() { MaximumTrainingLevelAdd = 3, StartingExpAdd = 20 }
        },

        [VillageBuilding.trainer2] = new VillageBuildingDefinition(
            VillageBuilding.trainer2, "Warrior Guild",
            "+40 XP to new units trained here. +5 XP for team units on recruit. +2 Training level."
        )
        {
            Cost = new BuildingCost() { Wealth = 500 },
            Boosts = new VillageBoosts() { StartingExpAdd = 40, TeamStartingExpAdd = 5, MaximumTrainingLevelAdd = 2 },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.trainer }
        },


        [VillageBuilding.CapitalDefenses] = new VillageBuildingDefinition(
            VillageBuilding.CapitalDefenses, "Capital",
            "The capital city."
        )
        {
            Cost = new BuildingCost() { Wealth = 0 },
            Boosts = new VillageBoosts() { GarrisonMaxAdd = 8, WealthMult = 0.25f, PopulationMaxMult = 0.5f },
            CanBeBuilt = false,
            RemovedOnOwnerChange = true,
            RequiresRaceCapitol = true,
            AddedOnOriginalOwner = true
        },


        [VillageBuilding.MagicGuild] = new VillageBuildingDefinition(
            VillageBuilding.MagicGuild, "Magic Guild",
            "Allows purchasing of tier 2 spells"
        )
        {
            Cost = new BuildingCost() { Wealth = 400 },
            Boosts = new VillageBoosts() { SpellLevels = 1, },
        },

        [VillageBuilding.Cathedral] = new VillageBuildingDefinition(
            VillageBuilding.Cathedral, "Cathedral",
            "Raises the maximum happiness of occupied villages"
        )
        {
            RequiresSubjugatedRace = true,
            Cost = new BuildingCost() { Wealth = 180 },
            Boosts = new VillageBoosts() { MaxHappinessAdd = 15, },
        },

    };

    public static Dictionary<VillageBuilding, VillageBuildingDefinition> crazyBuildings = new Dictionary<VillageBuilding, VillageBuildingDefinition>()
    {
        [VillageBuilding.mill] = new VillageBuildingDefinition(
            VillageBuilding.mill, "Mill",
            "Wealth +50%, Wealth +20"
        )
        {
            Cost = new BuildingCost() { Wealth = 250 },
            Boosts = new VillageBoosts()
            {
                WealthMult = 0.5f,
                WealthAdd = 20
            }
        },

        [VillageBuilding.mill2] = new VillageBuildingDefinition(
            VillageBuilding.mill2, "Powered Mill",
            "Wealth +100%, Wealth +50"
        )
        {
            Cost = new BuildingCost() { Wealth = 1500 },
            Boosts = new VillageBoosts()
            {
                WealthMult = 1.0f,
                WealthAdd = 50
            },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.mill, VillageBuilding.power1 }
        },

        [VillageBuilding.mill3] = new VillageBuildingDefinition(
            VillageBuilding.mill3, "Factory",
            "Wealth +100%, Wealth +350"
        )
        {
            Cost = new BuildingCost() { Wealth = 5000 },
            Boosts = new VillageBoosts()
            {
                WealthMult = 1.0f,
                WealthAdd = 350
            },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.mill2, VillageBuilding.power2 }
        },

        [VillageBuilding.power1] = new VillageBuildingDefinition(
            VillageBuilding.power1, "Clockwork Engine",
            "Allows for more advanced buildings. +50% population growth speed."
        )
        {
            Cost = new BuildingCost() { Wealth = 1000 },
            Boosts = new VillageBoosts()
            {
                PopulationGrowthMult = 0.5f
            }
        },

        [VillageBuilding.power2] = new VillageBuildingDefinition(
            VillageBuilding.power2, "Rune Imbued Clockwork Engine",
            "Allows for highly advanced buildings. +100% population growth speed."
        )
        {
            Cost = new BuildingCost() { Wealth = 4000 },
            Boosts = new VillageBoosts()
            {
                PopulationGrowthMult = 1.0f
            },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.power1 }
        },

        [VillageBuilding.manor] = new VillageBuildingDefinition(
            VillageBuilding.manor, "Manor",
            "Wealth generation +15%, +0.5 Farms"
        )
        {
            Cost = new BuildingCost() { Wealth = 80 },
            Boosts = new VillageBoosts()
            {
                WealthMult = 0.15f,
                FarmsEquivalent = 0.5f
            }
        },

        [VillageBuilding.irrigation] = new VillageBuildingDefinition(
            VillageBuilding.irrigation, "Irrigated Farms",
            "Better farm yields increase maximum population by 40%"
        )
        {
            Cost = new BuildingCost() { Wealth = 400 },
            Boosts = new VillageBoosts() { PopulationMaxMult = 0.4f }
        },

        [VillageBuilding.manor2] = new VillageBuildingDefinition(
            VillageBuilding.manor2, "Farm Manors",
            "Wealth generation +15%, +30% population growth speed, +5 Farms"
        )
        {
            Cost = new BuildingCost() { Wealth = 400 },
            Boosts = new VillageBoosts()
            {
                WealthMult = 0.15f,
                PopulationGrowthMult = 0.3f,
                FarmsEquivalent = 5.0f
            },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.manor, VillageBuilding.power1 }
        },

        [VillageBuilding.manor3] = new VillageBuildingDefinition(
            VillageBuilding.manor3, "Enchanted Farms",
            "Wealth generation +15%. +50% population growth speed. +100% maximum population."
        )
        {
            Cost = new BuildingCost() { Wealth = 700 },
            Boosts = new VillageBoosts()
            {
                WealthMult = 0.15f,
                PopulationGrowthMult = 0.5f,
                PopulationMaxMult = 1.0f
            },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.manor2, VillageBuilding.power2 }
        },

        [VillageBuilding.inn] = new VillageBuildingDefinition(
            VillageBuilding.inn, "Inn",
            "+100% the heal rate, Allows Mercenaries and Adventurers to visit and be hired."
        )
        {
            Cost = new BuildingCost() { Wealth = 40 },
            Boosts = new VillageBoosts()
            {
                HealRateMult = 1.0f,
                MaxMercsAdd = 2,
                MercsPerTurnAdd = 1,
                MaxAdventurersAdd = 6,
                AdventurersPerTurnAdd = 2,
            }
        },

        [VillageBuilding.inn2] = new VillageBuildingDefinition(
            VillageBuilding.inn2, "Grand Inn",
            "+300% the heal rate. +40 gold. +8 Team XP. Increases Mercenaries/Adventurers per turn and the cap."
        )
        {
            Cost = new BuildingCost() { Wealth = 800 },
            Boosts = new VillageBoosts()
            {
                HealRateMult = 3.0f,
                WealthAdd = 40,
                TeamStartingExpAdd = 8
                ,
                MaxMercsAdd = 1,
                MercsPerTurnAdd = 1,
                MaxAdventurersAdd = 6,
                AdventurersPerTurnAdd = 1,
            },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.inn, VillageBuilding.power1 }
        },

        [VillageBuilding.inn3] = new VillageBuildingDefinition(
            VillageBuilding.inn3, "Alchemy Brewery",
            "+700% the heal rate. +10 gold. +32 Team XP. Increases Mercenaries/Adventurers per turn and the cap."
        )
        {
            Cost = new BuildingCost() { Wealth = 2000 },
            Boosts = new VillageBoosts()
            {
                HealRateMult = 7.0f,
                WealthAdd = 10,
                TeamStartingExpAdd = 32,
                MaxMercsAdd = 3,
                MercsPerTurnAdd = 1,
                MaxAdventurersAdd = 6,
                AdventurersPerTurnAdd = 2,
            },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.inn2, VillageBuilding.power2 }
        },

        [VillageBuilding.wall] = new VillageBuildingDefinition(
            VillageBuilding.wall, "Wall",
            "Reduces access"
        )
        {
            Cost = new BuildingCost() { Wealth = 60 },
            Boosts = new VillageBoosts()
            {
                hasWall = true
            }
        },

        [VillageBuilding.wall2] = new VillageBuildingDefinition(
            VillageBuilding.wall2, "Guardhouse",
            "Increases maximum garrison size by +8"
        )
        {
            Cost = new BuildingCost() { Wealth = 300 },
            Boosts = new VillageBoosts()
            {
                GarrisonMaxAdd = 8
            },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.wall }
        },

        [VillageBuilding.trainer] = new VillageBuildingDefinition(
            VillageBuilding.trainer, "Trainer",
            "+8 Village XP. +3 Training level."
        )
        {
            Cost = new BuildingCost() { Wealth = 80 },
            Boosts = new VillageBoosts()
            {
                MaximumTrainingLevelAdd = 3,
                StartingExpAdd = 8
            }
        },

        [VillageBuilding.trainer2] = new VillageBuildingDefinition(
            VillageBuilding.trainer2, "Warrior Guild",
            "+50 Village XP. +50 Team XP. +2 Training level."
        )
        {
            Cost = new BuildingCost() { Wealth = 1500, LeaderExperience = 500 },
            Boosts = new VillageBoosts()
            {
                StartingExpAdd = 50,
                TeamStartingExpAdd = 50,
                MaximumTrainingLevelAdd = 2
            },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.trainer }
        },

        [VillageBuilding.trainer3] = new VillageBuildingDefinition(
            VillageBuilding.trainer3, "Archives of Heroes",
            "+100 Village XP. +100 Team XP. +2 Training level."
        )
        {
            Cost = new BuildingCost() { Wealth = 5000, LeaderExperience = 3000 },
            Boosts = new VillageBoosts()
            {
                StartingExpAdd = 100,
                TeamStartingExpAdd = 250,
                MaximumTrainingLevelAdd = 2
            },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.trainer2, VillageBuilding.power2 }
        },


        [VillageBuilding.subjugationPen2] = new VillageBuildingDefinition(
            VillageBuilding.subjugationPen2, "Belly Slave Palace",
            "+100 Team XP. Wealth +70%, Wealth +40."
        )
        {
            Cost = new BuildingCost() { Wealth = 2500 },
            Boosts = new VillageBoosts()
            {
                TeamStartingExpAdd = 100,
                WealthMult = 0.70f,
                WealthAdd = 40
            },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.power1 },
            RequiresSubjugatedRace = true,
            RemovedOnOwnerChange = true
        },

        [VillageBuilding.CapitalDefenses] = new VillageBuildingDefinition(
            VillageBuilding.CapitalDefenses, "Capital",
            "The capital city."
        )
        {
            Cost = new BuildingCost() { Wealth = 0 },
            Boosts = new VillageBoosts()
            {
                GarrisonMaxAdd = 8,
                WealthMult = 0.25f,
                PopulationMaxMult = 0.5f
            },
            CanBeBuilt = false,
            RemovedOnOwnerChange = true,
            RequiresRaceCapitol = true,
            AddedOnOriginalOwner = true
        },

        [VillageBuilding.capitalPalace] = new VillageBuildingDefinition(
            VillageBuilding.capitalPalace, "Capital Palace",
            "A grand fortress palace. +16 Max Garrison, +50% wealth generation, +100 wealth +50% max population, +20 farms"
        )
        {
            Cost = new BuildingCost() { Wealth = 10000 },
            Boosts = new VillageBoosts()
            {
                GarrisonMaxAdd = 16,
                WealthMult = 0.5f,
                WealthAdd = 100,
                PopulationMaxMult = 0.5f,
                FarmsEquivalent = 20.0f
            },
            RemovedOnOwnerChange = true,
            RequiresRaceCapitol = true
        },

        [VillageBuilding.hangingGardens] = new VillageBuildingDefinition(
            VillageBuilding.hangingGardens, "Hanging Gardens",
            "Multi-story wonder of crops, herbs and gardens. +50 Farms. +250 Team XP."
        )
        {
            Cost = new BuildingCost() { Wealth = 25000 },
            Boosts = new VillageBoosts()
            {
                FarmsEquivalent = 50.0f,
                TeamStartingExpAdd = 250
            },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.power2 },
            RemovedOnOwnerChange = true
        },

        [VillageBuilding.trainer4] = new VillageBuildingDefinition(
            VillageBuilding.trainer4, "Fortress City",
            "+1000 Village XP. +4000 Team XP. +200 Farms. +16 Garrison. +1000% Pop Growth."
        )
        {
            Cost = new BuildingCost() { Wealth = 100000, LeaderExperience = 20000 },
            Boosts = new VillageBoosts()
            {
                StartingExpAdd = 1000,
                TeamStartingExpAdd = 4000,
                FarmsEquivalent = 200.0f,
                GarrisonMaxAdd = 16,
                PopulationGrowthMult = 10.0f
            },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.inn3, VillageBuilding.trainer3, VillageBuilding.power2, VillageBuilding.hangingGardens }
        },

        [VillageBuilding.market1] = new VillageBuildingDefinition(
            VillageBuilding.market1, "Market",
            "+5 Wealth. +10 Team Wealth."
        )
        {
            Cost = new BuildingCost() { Wealth = 500 },
            Boosts = new VillageBoosts()
            {
                WealthAdd = 5,
                TeamWealthAdd = 10
            }
        },

        [VillageBuilding.market2] = new VillageBuildingDefinition(
            VillageBuilding.market2, "Merchant Guild",
            "+20 Wealth. +50 Team Wealth."
        )
        {
            Cost = new BuildingCost() { Wealth = 2500 },
            Boosts = new VillageBoosts()
            {
                WealthAdd = 20,
                TeamWealthAdd = 50
            },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.market1, VillageBuilding.power1 }
        },

        [VillageBuilding.market3] = new VillageBuildingDefinition(
            VillageBuilding.market3, "Skygalleon Shipping",
            "+50 Wealth. +150 Team Wealth. +100 Team XP."
        )
        {
            Cost = new BuildingCost() { Wealth = 12500 },
            Boosts = new VillageBoosts()
            {
                WealthAdd = 50,
                TeamWealthAdd = 150,
                TeamStartingExpAdd = 100
            },
            RequiredBuildings = new List<VillageBuilding>() { VillageBuilding.market2, VillageBuilding.power2 }
        },

        [VillageBuilding.MagicGuild] = new VillageBuildingDefinition(
            VillageBuilding.MagicGuild, "Magic Guild",
            "Allows purchasing of tier 2 spells"
        )
        {
            Cost = new BuildingCost() { Wealth = 400 },
            Boosts = new VillageBoosts()
            {
                SpellLevels = 1,
            },
        },

        [VillageBuilding.Cathedral] = new VillageBuildingDefinition(
            VillageBuilding.Cathedral, "Cathedral",
            "Raises the maximum happiness of occupied villages"
        )
        {
            RequiresSubjugatedRace = true,
            Cost = new BuildingCost() { Wealth = 180 },
            Boosts = new VillageBoosts() { MaxHappinessAdd = 15, },
        },

        [VillageBuilding.builder1] = new VillageBuildingDefinition(
            VillageBuilding.builder1, "Builder Workshop",
            "A place for builders to work. Max Builders +1"
        )
        {
            RequiresSubjugatedRace = true,
            Cost = new BuildingCost() { Wealth = 75 },
            Boosts = new VillageBoosts() { BuilderCount = 1, },
        },

    };


}
