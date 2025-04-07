using OdinSerializer;
using System;

class Quarry : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade deepUpgrade;
    public BuildingUpgrade leyUpgrade;

    [OdinSerialize]
    internal int ActionPlan;
    
    public Quarry(Vec2i location) : base(location)
    {
        Name = "Quarry";
        Desc = "The quarry generates stone each turn. Its output and resource can be adjusted using set action plans.";
        spriteID = 16;
        buildingType = ConstructibleType.Quarry;

        ApplyConfigStats(Config.BuildConfig.Quarry);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildConfig.QuarryImproveUpgrade);
        deepUpgrade = AddUpgrade(deepUpgrade, Config.BuildConfig.QuarryDeepUpgrade);
        leyUpgrade = AddUpgrade(leyUpgrade, Config.BuildConfig.QuarryLeyLineUpgrade);

        ActionPlan = 0;
    }
    internal override void RunBuildingFunction()
    {
        ConstructionResources ownerResource = Owner.constructionResources;
        int StoneRoll = 0;
        int OreRoll = 0;
        int MSRoll = 0;
        int GoldRoll = 0;
        if (improveUpgrade.built)
        {
            switch (ActionPlan)
            {
                case 0:
                    StoneRoll = State.Rand.Next(Config.BuildConfig.QuarryStoneMin + 1, Config.BuildConfig.QuarryStoneMax + 1) ;
                    OreRoll = State.Rand.Next(Config.BuildConfig.QuarryOreMin + 1, Config.BuildConfig.QuarryOreMax + 1);
                    MSRoll = State.Rand.Next(Config.BuildConfig.QuarryMSMin + 1, Config.BuildConfig.QuarryMSMax + 1);
                    break;
                case 1:
                    StoneRoll = State.Rand.Next((int)Math.Ceiling(Config.BuildConfig.QuarryStoneMin * 0.5f), (int)Math.Ceiling(Config.BuildConfig.QuarryStoneMax * 1.75f));
                    OreRoll = State.Rand.Next((int)Math.Ceiling(Config.BuildConfig.QuarryOreMin * 0.5f), (int)Math.Ceiling(Config.BuildConfig.QuarryOreMax * 1.75f));
                    MSRoll = State.Rand.Next((int)Math.Ceiling(Config.BuildConfig.QuarryMSMin * 0.5f), (int)Math.Ceiling(Config.BuildConfig.QuarryMSMax * 1.75f));
                    break;
                case 2:
                    int mode = 0;
                    int rolls = 0;
                    while (rolls < 2)
                    {
                        mode = State.Rand.Next(4);
                        if (mode == 0)
                        {
                            GoldRoll = Config.BuildConfig.QuarryGoldMax;
                        }
                        else if (mode == 1)
                        {
                            StoneRoll = Config.BuildConfig.QuarryStoneMax;
                        }
                        else if (mode == 2)
                        {
                            OreRoll = Config.BuildConfig.QuarryOreMax;
                        }
                        else
                        {
                            MSRoll = Config.BuildConfig.QuarryMSMax;
                        }
                        rolls++;
                    }
                    break;
                case 3:
                    StoneRoll = State.Rand.Next(0, Config.BuildConfig.QuarryStoneMin + Config.BuildConfig.QuarryStoneMax);
                    OreRoll = State.Rand.Next(0, Config.BuildConfig.QuarryOreMin + Config.BuildConfig.QuarryOreMax);
                    MSRoll = State.Rand.Next(0, Config.BuildConfig.QuarryMSMin + Config.BuildConfig.QuarryMSMax);
                    GoldRoll= State.Rand.Next(0, Config.BuildConfig.QuarryGoldMin + Config.BuildConfig.QuarryGoldMax);
                    break;
                case 4:
                    if (State.Rand.Next(10) >= 6)
                        OreRoll = State.Rand.Next(Config.BuildConfig.QuarryOreMin * 2, Config.BuildConfig.QuarryOreMax * 2);
                    else
                        MSRoll = State.Rand.Next(Config.BuildConfig.QuarryMSMin * 2, Config.BuildConfig.QuarryMSMax * 2);
                    break;
                case 5:
                    GoldRoll = State.Rand.Next(0, Config.BuildConfig.QuarryGoldMax * 2);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (ActionPlan)
            {
                case 0:
                    StoneRoll = State.Rand.Next(Config.BuildConfig.QuarryStoneMin, Config.BuildConfig.QuarryStoneMax);
                    OreRoll = State.Rand.Next(Config.BuildConfig.QuarryOreMin, Config.BuildConfig.QuarryOreMax);
                    MSRoll = State.Rand.Next(Config.BuildConfig.QuarryMSMin, Config.BuildConfig.QuarryMSMax);
                    break;
                case 1:
                    StoneRoll = State.Rand.Next((int)Math.Floor(Config.BuildConfig.QuarryStoneMin * 0.5f), (int)Math.Ceiling(Config.BuildConfig.QuarryStoneMax * 1.5f));
                    OreRoll = State.Rand.Next((int)Math.Floor(Config.BuildConfig.QuarryOreMin * 0.5f), (int)Math.Ceiling(Config.BuildConfig.QuarryOreMax * 1.5f));
                    MSRoll = State.Rand.Next((int)Math.Floor(Config.BuildConfig.QuarryMSMin * 0.5f), (int)Math.Ceiling(Config.BuildConfig.QuarryMSMax * 1.5f));
                    break;
                case 2:
                    int mode = State.Rand.Next(4);
                    if (mode == 0)
                    {
                        GoldRoll = Config.BuildConfig.QuarryGoldMax;
                    }
                    else if (mode == 1)
                    {
                        StoneRoll = Config.BuildConfig.QuarryStoneMax;
                    }
                    else if (mode == 2)
                    {
                        OreRoll = Config.BuildConfig.QuarryOreMax;
                    }
                    else
                    {
                        MSRoll = Config.BuildConfig.QuarryMSMax;
                    }
                    break;
                default:
                    break;
            }
        }

        if (!deepUpgrade.built) 
        {
            OreRoll = 0;
        }
        if (!leyUpgrade.built) 
        {
            MSRoll = 0;
        }

        ownerResource.AddResource(ConstructionResourceType.stone, StoneRoll);
        ownerResource.AddResource(ConstructionResourceType.ores, OreRoll);
        ownerResource.AddResource(ConstructionResourceType.manastones, MSRoll);
        Owner.AddGold(GoldRoll);
    }
}

