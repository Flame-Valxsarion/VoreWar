using OdinSerializer;
using System;

class BarrierTower : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade healUpgrade;
    public BuildingUpgrade buffUpgrade;

    [OdinSerialize]
    internal int DowntimeSlot1;
    [OdinSerialize]
    internal int DowntimeSlot2;
    [OdinSerialize]
    internal int DowntimeSlot3;
    [OdinSerialize]
    internal int BarrierMagnitude = 0;
    [OdinSerialize]
    internal int MendingMagnitude = 0;
    [OdinSerialize]
    internal int EmpowerMagnitude = 0;
    [OdinSerialize]
    internal bool CoreProtection;

    internal int CurrentDowntimeValue => BarrierMagnitude + MendingMagnitude + EmpowerMagnitude;
    internal int AvailCores => (DowntimeSlot1 > 0 ? 0 : 1) + (DowntimeSlot2 > 0 && improveUpgrade.built ? 0 : 1) + (DowntimeSlot3 > 0 && improveUpgrade.built ? 0 : 1);

    public BarrierTower(Vec2i location) : base(location)
    {
        Name = "Barrier Tower";
        Desc = "The barrier tower is used to protect all allied units at the start of a battle.";
        spriteID = 32;
        buildingType = ConstructibleType.BarrierTower;

        ApplyConfigStats(Config.BuildConfig.BarrierTower);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildConfig.BarrierTowerImproveUpgrade);
        healUpgrade = AddUpgrade(healUpgrade, Config.BuildConfig.BarrierTowerHealUpgrade);
        buffUpgrade = AddUpgrade(buffUpgrade, Config.BuildConfig.BarrierTowerBuffUpgrade);

        DowntimeSlot1 = 0;
        DowntimeSlot2 = 0;
        DowntimeSlot3 = 0;
        BarrierMagnitude = 1;
        MendingMagnitude = 0;
        EmpowerMagnitude = 0;

        CoreProtection = false;
    }
    internal override void RunBuildingFunction()
    {
        if (DowntimeSlot1 > 0) 
        {
            DowntimeSlot1--;
        }
        if (DowntimeSlot2 > 0) 
        {
            DowntimeSlot2--;
        }
        if (DowntimeSlot3 > 0) 
        {
            DowntimeSlot3--;
        }
    }
}

