using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class ConstructibleBuilding
{
    [OdinSerialize]
    internal Empire Owner;

    [OdinSerialize]
    internal Vec2i Position;

    [OdinSerialize]
    internal List<BuildingUpgrade> Upgrades; 

    [OdinSerialize]
    internal int GoldCost; 
    [OdinSerialize]
    internal ConstructionResources ResourceToBuild;

    [OdinSerialize]
    internal int turnsToCompletion;
    [OdinSerialize]
    internal int turnsToUpgrade;
    [OdinSerialize]
    internal int baseBuildTurns;
    [OdinSerialize]
    internal bool ruined;
    public int upgradeStage => Upgrades.Where(u=> u.built).Count();
    public bool constructing => turnsToCompletion > 0;
    public bool upgrading => turnsToUpgrade > 0;
    public bool enemyOnTop => StrategicUtilities.GetEnemyArmyWithinXTiles(this,0).Any();
    public bool allyOnTop => StrategicUtilities.GetOwnerArmyWithinXTiles(this,0).Any();
    public bool active => !constructing && !upgrading && !enemyOnTop && !ruined;

    [OdinSerialize]
    internal bool enabled = true;

    [OdinSerialize]
    internal string Name;

    [OdinSerialize]
    internal string Desc;

    [OdinSerialize]
    internal int spriteID;

    [OdinSerialize]
    internal int CaptureTime;

    [OdinSerialize]
    internal ConstructibleType buildingType;
    protected ConstructibleBuilding(Vec2i location)
    {
        Position = location;
        GoldCost = 0;
        ResourceToBuild = new ConstructionResources();
        baseBuildTurns = -1;
        Upgrades = new List<BuildingUpgrade>();
        CaptureTime = Config.BuildConfig.BuildingCaptureTurns;
    }

    internal abstract void RunBuildingFunction();
    internal void ConstructBuilding() 
    {
        Owner.Buildings.Add(this);
        turnsToCompletion = baseBuildTurns;
        Owner.constructionResources.SpendProvidedResources(ResourceToBuild);
        int max_of_type = -1;
        switch (buildingType)
        {
            case ConstructibleType.WorkCamp:
                max_of_type = Config.BuildConfig.WorkCamp.BuildLimit;
                break;
            case ConstructibleType.LumberSite:
                max_of_type = Config.BuildConfig.LumberSite.BuildLimit;
                break;
            case ConstructibleType.Quarry:
                max_of_type = Config.BuildConfig.Quarry.BuildLimit;
                break;
            case ConstructibleType.CasterTower:
                max_of_type = Config.BuildConfig.CasterTower.BuildLimit;
                break;
            case ConstructibleType.BarrierTower:
                max_of_type = Config.BuildConfig.BarrierTower.BuildLimit;
                break;
            case ConstructibleType.DefEncampment:
                max_of_type = Config.BuildConfig.DefenseEncampment.BuildLimit;
                break;
            case ConstructibleType.Academy:
                max_of_type = Config.BuildConfig.Academy.BuildLimit;
                break;
            case ConstructibleType.DarkMagicTower:
                max_of_type = Config.BuildConfig.DarkMagicTower.BuildLimit;
                break;
            case ConstructibleType.TemporalTower:
                max_of_type = Config.BuildConfig.TemporalTower.BuildLimit;
                break;
            case ConstructibleType.Laboratory:
                max_of_type = Config.BuildConfig.Laboratory.BuildLimit;
                break;
            case ConstructibleType.Teleporter:
                max_of_type = Config.BuildConfig.Teleporter.BuildLimit;
                break;
            case ConstructibleType.TownHall:
                max_of_type = Config.BuildConfig.TownHall.BuildLimit;
                break;
            default:
                break;
        }
        if (max_of_type > Owner.EmpireBuildingLimit[buildingType])
        {
            Owner.EmpireBuildingLimit[buildingType] = Owner.EmpireBuildingLimit[buildingType] + 1;
        }
        Owner.SpendGold(GoldCost);
        var contstruct = State.World.Constructibles.ToList();
        contstruct.Add(this);
        State.World.Constructibles = contstruct.ToArray();
        State.GameManager.StrategyMode.RedrawVillages();
        State.GameManager.StrategyMode.StatusBarUI.Gold.text = "Gold:" + Owner.Gold;
        
    }

    public void RunBuildingTurn()
    {
        if (enemyOnTop)
        {
            CaptureTime--;
            if (CaptureTime <= 0)
            {
                if (StrategicUtilities.GetEnemyArmyWithinXTiles(this, 0).First().Empire.Race >= Race.Vagrants)
                {
                    switch (Config.BuildConfig.MonsterBuildingCapture)
                    {
                        case 0:
                            break;
                        case 1:
                            StrategicUtilities.TryClaim(Position, StrategicUtilities.GetEnemyArmyWithinXTiles(this, 0).First().Empire);
                            break;
                        case 2:
                            ruined = true;
                            CaptureTime = Config.BuildConfig.BuildingCaptureTurns;
                            break;
                        case 3:
                            Owner.Buildings.Remove(this);
                            Owner.EmpireBuildingLimit[buildingType] = Owner.EmpireBuildingLimit[buildingType] - 1;
                            List<ConstructibleBuilding> bLis = State.World.Constructibles.ToList();
                            bLis.Remove(this);
                            State.World.Constructibles = bLis.ToArray();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (Config.BuildConfig.EmpireBuildingCapture)
                    {
                        case 0:
                            break;
                        case 1:
                            StrategicUtilities.TryClaim(Position, StrategicUtilities.GetEnemyArmyWithinXTiles(this, 0).First().Empire);
                            break;
                        case 2:
                            ruined = true;
                            CaptureTime = Config.BuildConfig.BuildingCaptureTurns;
                            break;
                        case 3:
                            Owner.Buildings.Remove(this);
                            Owner.EmpireBuildingLimit[buildingType] = Owner.EmpireBuildingLimit[buildingType] - 1;
                            List<ConstructibleBuilding> bLis = State.World.Constructibles.ToList();
                            bLis.Remove(this);
                            State.World.Constructibles = bLis.ToArray();
                            break;
                        default:
                            break;
                    }
                }
            }
            return;
        }
        else
        {
            CaptureTime = Config.BuildConfig.BuildingCaptureTurns;
        }
        if (allyOnTop)
        {
            ruined = false;
        }
        if (constructing)
        {
            turnsToCompletion--;
            return;
        }
        if (upgrading)
        {
            turnsToUpgrade--;
            return;
        }
        if (Owner != null)
        {
            RunBuildingFunction();
        }
    }

    internal void ApplyConfigStats(GeneralBuildingConfig buildingConfig)
    {
        ResourceToBuild = buildingConfig.Resources;
        GoldCost = buildingConfig.Gold;
        baseBuildTurns = buildingConfig.BuildTime;
    }

    internal BuildingUpgrade AddUpgrade(BuildingUpgrade upgrade, BuildingUpgrade configUpgrade)
    {
        upgrade = new BuildingUpgrade();
        upgrade.Name = configUpgrade.Name;
        upgrade.Desc = configUpgrade.Desc;
        upgrade.GoldCost = configUpgrade.GoldCost;
        upgrade.ResourceToUpgrade = configUpgrade.ResourceToUpgrade;
        upgrade.upgradeTime = configUpgrade.upgradeTime;
        Upgrades.Add(upgrade);
        return upgrade;
    }

}

