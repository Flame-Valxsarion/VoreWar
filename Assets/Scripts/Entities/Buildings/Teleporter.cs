using OdinSerializer;
using System;
using System.Collections.Generic;

class Teleporter : ConstructibleBuilding
{
    public BuildingUpgrade stoneUpgrade;
    public BuildingUpgrade capacityUpgrade;
    public BuildingUpgrade ancientUpgrade;

    [OdinSerialize]
    internal float TeleportCapacity;
    public Teleporter(Vec2i location) : base(location)
    {
        Name = "Teleporter";
        Desc = "The teleporter helps move armies across the map quickly.";
        spriteID = 72;
        buildingType = ConstructibleType.Teleporter;

        ApplyConfigStats(Config.BuildConfig.Teleporter);
        stoneUpgrade = AddUpgrade(stoneUpgrade, Config.BuildConfig.TeleporterStoneUpgrade);
        capacityUpgrade = AddUpgrade(capacityUpgrade, Config.BuildConfig.TeleporterCapacityUpgrade);
        ancientUpgrade = AddUpgrade(ancientUpgrade, Config.BuildConfig.TeleporterAncientUpgrade);
        TeleportCapacity = Config.BuildConfig.TeleporterMaxCapacity;
    }
    internal override void RunBuildingFunction()
    {
        TeleportCapacity += Config.BuildConfig.TeleporterCapacityRegen;
        if (TeleportCapacity > Config.BuildConfig.TeleporterMaxCapacity && !capacityUpgrade.built)
        {
            TeleportCapacity = Config.BuildConfig.TeleporterMaxCapacity;
        }
    }

    public bool CanTeleportArmy(Army army, bool useCap = false)
    {
        if (Config.BuildConfig.TeleporterPerUnitCapacity && TeleportCapacity >= 1)
        {
            if (useCap)
            {
                TeleportCapacity -= 1;
            }
            return true;
        }
        float CapUse = 0;
        foreach (Unit unit in army.Units)
        {
            CapUse += State.RaceSettings.GetDeployCost(unit.Race) * unit.TraitBoosts.DeployCostMult * Config.BuildConfig.TeleporterPerUnitCapacityMod;
        }
        if (CapUse > TeleportCapacity)
        {
            return false;
        }
        if (useCap)
        {
            TeleportCapacity -= CapUse;
        }
        return true;
    }
}

