using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;

class TownHall : ConstructibleBuilding
{
    public BuildingUpgrade standardUpgrade;
    public BuildingUpgrade prefabUpgrade;
    public BuildingUpgrade manaStoneUpgrade;
    
    public TownHall(Vec2i location) : base(location)
    {
        Name = "Town Hall";
        Desc = "The town hall is used to construct a new village.";
        spriteID = 88;
        buildingType = ConstructibleType.TownHall;

        ApplyConfigStats(Config.BuildConfig.CasterTower);
        standardUpgrade = AddUpgrade(standardUpgrade, Config.BuildConfig.TownHallManualUpgrade);
        prefabUpgrade = AddUpgrade(prefabUpgrade, Config.BuildConfig.TownHallPrefabUpgrade);
        manaStoneUpgrade = AddUpgrade(manaStoneUpgrade, Config.BuildConfig.TownHallManaStoneUpgrade);
    }
    internal override void RunBuildingFunction()
    {
        if ((standardUpgrade.built || prefabUpgrade.built || manaStoneUpgrade.built) && active)
        {
            var contstruct = State.World.Constructibles.ToList();
            contstruct.Remove(this);
            State.World.Constructibles = contstruct.ToArray();
            Owner.Buildings.Remove(this);

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (j != 0 || i != 0)
                    {
                        StrategicTileType current = State.World.Tiles[Position.x + i, Position.y + j];
                        StrategicTileType replace = StrategicTileType.field;
                        if (current == StrategicTileType.field ||
                            current == StrategicTileType.fieldAshen ||
                            current == StrategicTileType.fieldDesert ||
                            current == StrategicTileType.fieldsavannah ||
                            current == StrategicTileType.fieldSmallIslands ||
                            current == StrategicTileType.fieldSnow||
                            !StrategicTileInfo.CanWalkInto(current))
                        {
                            continue;
                        }
                        else if (current == StrategicTileType.ashen || current == StrategicTileType.ashenHills || current == StrategicTileType.volcanic)
                        {
                            replace = StrategicTileType.fieldAshen;
                        }
                        else if (current == StrategicTileType.desert || current == StrategicTileType.sandHills)
                        {
                            replace = StrategicTileType.fieldDesert;
                        }
                        else if (current == StrategicTileType.savannah)
                        {
                            replace = StrategicTileType.fieldsavannah;
                        }
                        else if (current == StrategicTileType.smallIslands || current == StrategicTileType.shallowWater)
                        {
                            replace = StrategicTileType.fieldSmallIslands;
                        }
                        else if (current == StrategicTileType.snow || current == StrategicTileType.ice || current == StrategicTileType.snowHills || current == StrategicTileType.snowTrees)
                        {
                            replace = StrategicTileType.fieldSnow;
                        }
                        
                        State.World.Tiles[Position.x + i, Position.y + j] = replace;
                    }
                    //DestroyVillagesAtTile(new Vec2i(x + i, y + j));
                }
            }
            State.GameManager.StrategyMode.RedrawTiles();

            var villages = State.World.Villages.ToList();
            villages.Add(new Village("New Village", Position, 1, Owner.Race, false));
            State.World.Villages = villages.ToArray();
        }
    }
}

