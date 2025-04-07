using System.Collections.Generic;

public class VillageBoosts
{
    internal float WealthMult = 0.0f;
    internal int WealthAdd = 0;

    internal int TeamWealthAdd = 0;

    internal float PopulationGrowthMult = 0.0f;

    internal float PopulationMaxMult = 0.0f;
    internal int PopulationMaxAdd = 0;

    internal float FarmsEquivalent = 0.0f;

    internal float GarrisonMaxMult = 0.0f;
    internal int GarrisonMaxAdd = 0;

    internal float HealRateMult = 0.0f;

    internal int StartingExpAdd = 0;

    internal int TeamStartingExpAdd = 0;

    internal int MaximumTrainingLevelAdd = 0;

    internal bool hasWall = false;

    internal bool allowsSubjugation = false;

    internal int MercsPerTurnAdd = 0;
    internal int MaxMercsAdd = 0;

    internal int AdventurersPerTurnAdd = 0;
    internal int MaxAdventurersAdd = 0;

    internal int MaxHappinessAdd = 0;

    internal int SpellLevels = 0;

    internal int BuilderCount = 0;


    internal List<Traits> AddTraits = new List<Traits>();

    public void ResetValues()
    {
        WealthMult = 0.0f;
        WealthAdd = 0;
        TeamWealthAdd = 0;
        PopulationGrowthMult = 0.0f;
        PopulationMaxMult = 0.0f;
        PopulationMaxAdd = 0;
        FarmsEquivalent = 0.0f;
        GarrisonMaxMult = 0.0f;
        GarrisonMaxAdd = 0;
        HealRateMult = 0.0f;
        StartingExpAdd = 0;
        TeamStartingExpAdd = 0;
        MaximumTrainingLevelAdd = 0;
        hasWall = false;
        allowsSubjugation = false;
        MercsPerTurnAdd = 0;
        MaxMercsAdd = 0;
        AdventurersPerTurnAdd = 0;
        MaxAdventurersAdd = 0;
        SpellLevels = 0;
        MaxHappinessAdd = 0;
        BuilderCount = 0;
        AddTraits = new List<Traits>();
    }

    public VillageBoosts MergeBoosts(VillageBoosts otherBoost)
    {
        WealthMult += otherBoost.WealthMult;
        WealthAdd += otherBoost.WealthAdd;
        TeamWealthAdd += otherBoost.TeamWealthAdd;
        PopulationGrowthMult += otherBoost.PopulationGrowthMult;
        PopulationMaxMult += otherBoost.PopulationMaxMult;
        PopulationMaxAdd += otherBoost.PopulationMaxAdd;
        FarmsEquivalent += otherBoost.FarmsEquivalent;
        GarrisonMaxMult += otherBoost.GarrisonMaxMult;
        GarrisonMaxAdd += otherBoost.GarrisonMaxAdd;
        HealRateMult += otherBoost.HealRateMult;
        StartingExpAdd += otherBoost.StartingExpAdd;
        TeamStartingExpAdd += otherBoost.TeamStartingExpAdd;
        MaximumTrainingLevelAdd += otherBoost.MaximumTrainingLevelAdd;
        hasWall = hasWall || otherBoost.hasWall;
        allowsSubjugation = allowsSubjugation || otherBoost.allowsSubjugation;
        MercsPerTurnAdd += otherBoost.MercsPerTurnAdd;
        MaxMercsAdd += otherBoost.MaxMercsAdd;
        AdventurersPerTurnAdd += otherBoost.AdventurersPerTurnAdd;
        MaxAdventurersAdd += otherBoost.MaxAdventurersAdd;
        MaxHappinessAdd += otherBoost.MaxHappinessAdd;
        BuilderCount += otherBoost.BuilderCount;
        AddTraits.AddRange(otherBoost.AddTraits);
        SpellLevels += otherBoost.SpellLevels;

        return this;
    }
}


public class BuildingCost
{
    internal int Wealth = 0;
    internal float LeaderExperience = 0.0f;

    public BuildingCost(int wealth = 0, float leaderXp = 0.0f)
    {
        Wealth = wealth;
        LeaderExperience = leaderXp;
    }
}
