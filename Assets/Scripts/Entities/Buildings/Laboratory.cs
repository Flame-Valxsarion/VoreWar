using OdinSerializer;
using System;
using System.Collections.Generic;

class Laboratory : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade ingredientUpgrade;
    public BuildingUpgrade boostUpgrade;
    
    public Laboratory(Vec2i location) : base(location)
    {
        Name = "Laboratory";
        Desc = "The laboratory is used to purchase potions that can modify a unit.";
        spriteID = 80;
        buildingType = ConstructibleType.Laboratory;

        ApplyConfigStats(Config.BuildConfig.Laboratory);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildConfig.LaboratoryImproveUpgrade);
        ingredientUpgrade = AddUpgrade(ingredientUpgrade, Config.BuildConfig.LaboratoryIngredientUpgrade);
        boostUpgrade = AddUpgrade(boostUpgrade, Config.BuildConfig.LaboratoryBoostUpgrade);
    }
    internal override void RunBuildingFunction()
    {
        
    }
}

