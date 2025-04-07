using OdinSerializer;
using System;
using System.Collections.Generic;

class CasterTower : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade forceUpgrade;
    public BuildingUpgrade buffUpgrade;

    [OdinSerialize]
    internal int ManaCharges;
    [OdinSerialize]
    internal Dictionary<SpellTypes,int> spellCasts;
    
    public CasterTower(Vec2i location) : base(location)
    {
        Name = "Caster Tower";
        Desc = "The caster tower uses mana to launch a barrage of spells on the first turn of combat that starts in it's range.";
        spriteID = 24;
        buildingType = ConstructibleType.CasterTower;

        ApplyConfigStats(Config.BuildConfig.CasterTower);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildConfig.CasterTowerImproveUpgrade);
        forceUpgrade = AddUpgrade(forceUpgrade, Config.BuildConfig.CasterTowerForceUpgrade);
        buffUpgrade = AddUpgrade(buffUpgrade, Config.BuildConfig.CasterTowerBuffUpgrade);

        ManaCharges = Config.BuildConfig.CasterTowerManaChargesMax;

        spellCasts = new Dictionary<SpellTypes, int> 
        {
            [SpellTypes.Fireball] = 1,
            [SpellTypes.LightningBolt] = 1,
            [SpellTypes.PowerBolt] = 3,

            [SpellTypes.Predation] = 1,
            [SpellTypes.Valor] = 1,
            [SpellTypes.Speed] = 1,
            [SpellTypes.Shield] = 1,

            [SpellTypes.Pyre] = 1,
            [SpellTypes.IceBlast] = 2,
            [SpellTypes.ForkLightning] = 1,
            [SpellTypes.Flamberge] = 1,
        };
    }
    internal override void RunBuildingFunction()
    {
        int ManaChargesMax = Config.BuildConfig.CasterTowerManaChargesMax * (improveUpgrade.built ? 2 : 1);
        int ManaChargesRegen = Config.BuildConfig.CasterTowerManaChargesRegen * (improveUpgrade.built ? 2 : 1);
        if (ManaChargesMax > ManaCharges)
        {
            ManaCharges += ManaChargesRegen;
            if (ManaCharges > ManaChargesMax)
            {
                ManaCharges = ManaChargesMax;
            }
        }
        
    }
}

