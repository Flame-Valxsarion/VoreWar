using System;
using System.Collections.Generic;
using TacticalBuildings;
using TacticalDecorations;
using UnityEngine;

class TacticalMapGenerator
{
    enum TerrainType
    {
        Grass,
        Snow,
        Forest,
        Desert,
        Volcanic,
        Beach,
        Swamp,
    }

    TerrainType terrainType;
    TacticalTileType defaultType;
    Village village;

    int attempt;
    int maxAttempts;
    bool wasWiped;

    bool[,] blockedTile;

    public TacticalMapGenerator(StrategicTileType stratTiletype, Village village)
    {
        this.village = village;

        if (stratTiletype == StrategicTileType.forest)
        {
            terrainType = TerrainType.Forest;
            defaultType = TacticalTileType.greengrass;
        }
        else if (village == null && (stratTiletype == StrategicTileType.snow || stratTiletype == StrategicTileType.snowHills || stratTiletype == StrategicTileType.fieldSnow))
        {
            terrainType = TerrainType.Snow;
            defaultType = (TacticalTileType)400;
        }
        else if (stratTiletype == StrategicTileType.desert || stratTiletype == StrategicTileType.sandHills || stratTiletype == StrategicTileType.fieldDesert)
        {
            terrainType = TerrainType.Desert;
            defaultType = TacticalTileType.RockOverSand;
        }
        else if (stratTiletype == StrategicTileType.volcanic || stratTiletype == StrategicTileType.ashen || stratTiletype == StrategicTileType.ashenHills || stratTiletype == StrategicTileType.fieldAshen)
        {
            terrainType = TerrainType.Volcanic;
            defaultType = TacticalTileType.VolcanicOverGravel;
        }
        else if (stratTiletype == StrategicTileType.shallowWater || stratTiletype == StrategicTileType.fieldSmallIslands || stratTiletype == StrategicTileType.smallIslands)
        {
            terrainType = TerrainType.Beach;
            defaultType = (TacticalTileType)600;
        }
        else if (stratTiletype == StrategicTileType.swamp || stratTiletype == StrategicTileType.drySwamp)
        {
            terrainType = TerrainType.Swamp;
            defaultType = (TacticalTileType)700;
        }
        else
        {
            terrainType = TerrainType.Grass;
            defaultType = TacticalTileType.greengrass;
        }
    }

    internal TacticalTileType[,] GenMap(bool wall)
    {
        List<TacticalBuilding> buildings = new List<TacticalBuilding>();
        int centerY = Config.TacticalSizeY / 2;
        int HalfX = Config.TacticalSizeX / 2;
        maxAttempts = 5;
        if (centerY < 14)
            maxAttempts = 10;
        TacticalTileType[,] tiles;
        int[,] decTilesUsed = new int[Config.TacticalSizeX, Config.TacticalSizeY];
        List<DecorationStorage> placedDecorations = new List<DecorationStorage>();

        blockedTile = new bool[Config.TacticalSizeX, Config.TacticalSizeY];

        he_seed = new Vector2(UnityEngine.Random.Range(0, 200), UnityEngine.Random.Range(0, 200));

        tiles = new TacticalTileType[Config.TacticalSizeX, Config.TacticalSizeY];
        MakeArrays();
        if (terrainType == TerrainType.Snow)
        {
            for (int i = 0; i < Config.TacticalSizeX; i++)
            {
                for (int j = 0; j < Config.TacticalSizeY; j++)
                {
                    tiles[i, j] = defaultType;

                    if (State.Rand.Next(6) == 0 && decTilesUsed[i, j] == 0)
                    {
                        TacticalDecoration decoration;
                        TacDecType decType;
                        if (Config.WinterActive())
                        {
                            int rand = State.Rand.Next(TacticalDecorationList.HolidaySnowEnvironment.Length);
                            if (rand == 4 || rand == 12 || rand == 13) //Make the bears and hidden behind rocks rare
                                rand = State.Rand.Next(TacticalDecorationList.HolidaySnowEnvironment.Length);
                            decType = TacticalDecorationList.HolidaySnowEnvironment[rand];
                            decoration = TacticalDecorationList.DecDict[decType];
                            TryToPlaceDecoration(i, j, decoration, decType);
                        }
                        else
                        {
                            int rand = State.Rand.Next(TacticalDecorationList.SnowEnvironment.Length);
                            if (rand == 4) //Make the bears rare
                                rand = State.Rand.Next(TacticalDecorationList.SnowEnvironment.Length);
                            decType = TacticalDecorationList.SnowEnvironment[rand];
                            decoration = TacticalDecorationList.DecDict[decType];
                            TryToPlaceDecoration(i, j, decoration, decType);
                        }
                    }
                }
            }
        }
        else if (terrainType == TerrainType.Desert)
        {
            for (int i = 0; i < Config.TacticalSizeX; i++)
            {
                for (int j = 0; j < Config.TacticalSizeY; j++)
                {
                    if (he_array[i, j] < Config.TacticalWaterValue - (0.01f * attempt))
                        tiles[i, j] = TacticalTileType.RockOverTar;
                    else if (he_array[i, j] < .65f)
                        tiles[i, j] = TacticalTileType.RockOverSand;
                    else
                        tiles[i, j] = (TacticalTileType)201;

                    if (tiles[i, j] != TacticalTileType.RockOverTar && State.Rand.Next(6) == 0 && decTilesUsed[i, j] == 0)
                    {
                        TacticalDecoration decoration;
                        TacDecType decType;
                        if (State.Rand.Next(3) == 0)
                            decType = TacticalDecorationList.Cactus[State.Rand.Next(TacticalDecorationList.Cactus.Length)];
                        else if (State.Rand.Next(2) == 0)
                            decType = TacticalDecorationList.Rocks[State.Rand.Next(TacticalDecorationList.Rocks.Length)];
                        else
                            decType = TacticalDecorationList.Bones[State.Rand.Next(TacticalDecorationList.Bones.Length)];
                        decoration = TacticalDecorationList.DecDict[decType];
                        if (j > Config.TacticalSizeY / 2 || village == null)
                            TryToPlaceDecoration(i, j, decoration, decType);
                        else
                        {
                            if (State.Rand.Next(3) == 0)
                                TryToPlaceDecoration(i, j, decoration, decType);
                        }
                    }
                }
            }
            if (village != null)
            {
                PlaceRowOfBuildings(tiles, buildings, HalfX - 2, Config.TacticalSizeY / 4 + 1, -1);
                PlaceRowOfBuildings(tiles, buildings, HalfX - 2, Config.TacticalSizeY / 4 - 2, -1);
                PlaceRowOfBuildings(tiles, buildings, HalfX + 1, Config.TacticalSizeY / 4 + 1, 1);
                PlaceRowOfBuildings(tiles, buildings, HalfX + 1, Config.TacticalSizeY / 4 - 2, 1);
            }
        }
        else if (terrainType == TerrainType.Volcanic)
        {
            for (int i = 0; i < Config.TacticalSizeX; i++)
            {
                for (int j = 0; j < Config.TacticalSizeY; j++)
                {
                    if (he_array[i, j] < Config.TacticalWaterValue - (0.01f * attempt))
                        tiles[i, j] = TacticalTileType.VolcanicOverLava;
                    else if (he_array[i, j] < .5f)
                        tiles[i, j] = TacticalTileType.VolcanicOverGravel;
                    else
                        tiles[i, j] = (TacticalTileType)501;

                    if (tiles[i, j] != TacticalTileType.VolcanicOverLava && State.Rand.Next(6) == 0 && decTilesUsed[i, j] == 0)
                    {
                        TacticalDecoration decoration;
                        TacDecType decType;
                        if (State.Rand.Next(3) == 0)
                            decType = TacticalDecorationList.VolcanicMagmaRocks[State.Rand.Next(TacticalDecorationList.Cactus.Length)];
                        else if (State.Rand.Next(2) == 0)
                            decType = TacticalDecorationList.VolcanicRocks[State.Rand.Next(TacticalDecorationList.Rocks.Length)];
                        else
                            decType = TacticalDecorationList.CharredBones[State.Rand.Next(TacticalDecorationList.Bones.Length)];
                        decoration = TacticalDecorationList.DecDict[decType];
                        if (j > Config.TacticalSizeY / 2 || village == null)
                            TryToPlaceDecoration(i, j, decoration, decType);
                        else
                        {
                            if (State.Rand.Next(3) == 0)
                                TryToPlaceDecoration(i, j, decoration, decType);
                        }
                    }
                }
            }
            if (village != null)
            {
                PlaceRowOfBuildings(tiles, buildings, HalfX - 2, Config.TacticalSizeY / 4 + 1, -1);
                PlaceRowOfBuildings(tiles, buildings, HalfX - 2, Config.TacticalSizeY / 4 - 2, -1);
                PlaceRowOfBuildings(tiles, buildings, HalfX + 1, Config.TacticalSizeY / 4 + 1, 1);
                PlaceRowOfBuildings(tiles, buildings, HalfX + 1, Config.TacticalSizeY / 4 - 2, 1);
            }
        }
        else if (terrainType == TerrainType.Beach)
        {
            if (village != null)
            {
                for (int i = 0; i < Config.TacticalSizeX; i++)
                {
                    for (int j = 0; j < centerY; j++)
                    {
                        tiles[i, j] = RandomBeach(i, j);
                    }
                }
                PlaceRowOfBuildings(tiles, buildings, HalfX - 2, Config.TacticalSizeY / 4 + 1, -1);
                PlaceRowOfBuildings(tiles, buildings, HalfX - 2, Config.TacticalSizeY / 4 - 2, -1);
                PlaceRowOfBuildings(tiles, buildings, HalfX + 1, Config.TacticalSizeY / 4 + 1, 1);
                PlaceRowOfBuildings(tiles, buildings, HalfX + 1, Config.TacticalSizeY / 4 - 2, 1);

                for (int i = 0; i < Config.TacticalSizeX; i++)
                {
                    for (int j = 0; j < centerY; j++)
                    {
                        if (State.Rand.Next(3) == 0)
                            PlaceBeachDecoration(i, j);
                    }
                }
            }
            else
            {
                for (int i = 0; i < Config.TacticalSizeX; i++)
                {
                    for (int j = 0; j < centerY; j++)
                    {
                        tiles[i, j] = RandomBeach(i, j);
                        PlaceBeachDecoration(i, j);
                    }
                }
            }

            for (int i = 0; i < Config.TacticalSizeX; i++)
            {
                for (int j = centerY; j < Config.TacticalSizeY; j++)
                {
                    tiles[i, j] = RandomBeach(i, j);
                    PlaceBeachDecoration(i, j);
                }
            }
        }
        else if (terrainType == TerrainType.Swamp)
        {
            if (village != null)
            {
                for (int i = 0; i < Config.TacticalSizeX; i++)
                {
                    for (int j = 0; j < centerY; j++)
                    {
                        tiles[i, j] = RandomSwamp(i, j);
                    }
                }
                PlaceRowOfBuildings(tiles, buildings, HalfX - 2, Config.TacticalSizeY / 4 + 1, -1);
                PlaceRowOfBuildings(tiles, buildings, HalfX - 2, Config.TacticalSizeY / 4 - 2, -1);
                PlaceRowOfBuildings(tiles, buildings, HalfX + 1, Config.TacticalSizeY / 4 + 1, 1);
                PlaceRowOfBuildings(tiles, buildings, HalfX + 1, Config.TacticalSizeY / 4 - 2, 1);

                for (int i = 0; i < Config.TacticalSizeX; i++)
                {
                    for (int j = 0; j < centerY; j++)
                    {
                        if (State.Rand.Next(3) == 0)
                            PlaceSwampDecoration(i, j);
                    }
                }
            }
            else
            {
                for (int i = 0; i < Config.TacticalSizeX; i++)
                {
                    for (int j = 0; j < centerY; j++)
                    {
                        tiles[i, j] = RandomSwamp(i, j);
                        PlaceSwampDecoration(i, j);
                    }
                }
            }

            for (int i = 0; i < Config.TacticalSizeX; i++)
            {
                for (int j = centerY; j < Config.TacticalSizeY; j++)
                {
                    tiles[i, j] = RandomSwamp(i, j);
                    PlaceSwampDecoration(i, j);
                }
            }
        }
        else
        {
            if (village != null)
            {
                for (int i = 0; i < Config.TacticalSizeX; i++)
                {
                    for (int j = 0; j < centerY; j++)
                    {
                        tiles[i, j] = RandomGrass(i, j);
                    }
                }
                PlaceRowOfBuildings(tiles, buildings, HalfX - 2, Config.TacticalSizeY / 4 + 1, -1);
                PlaceRowOfBuildings(tiles, buildings, HalfX - 2, Config.TacticalSizeY / 4 - 2, -1);
                PlaceRowOfBuildings(tiles, buildings, HalfX + 1, Config.TacticalSizeY / 4 + 1, 1);
                PlaceRowOfBuildings(tiles, buildings, HalfX + 1, Config.TacticalSizeY / 4 - 2, 1);

                for (int i = 0; i < Config.TacticalSizeX; i++)
                {
                    for (int j = 0; j < centerY; j++)
                    {
                        if (State.Rand.Next(3) == 0)
                            PlaceGrassDecoration(i, j);
                    }
                }
            }
            else
            {
                for (int i = 0; i < Config.TacticalSizeX; i++)
                {
                    for (int j = 0; j < centerY; j++)
                    {
                        tiles[i, j] = RandomGrass(i, j);
                        PlaceGrassDecoration(i, j);
                    }
                }
            }

            for (int i = 0; i < Config.TacticalSizeX; i++)
            {
                for (int j = centerY; j < Config.TacticalSizeY; j++)
                {
                    tiles[i, j] = RandomGrass(i, j);
                    PlaceGrassDecoration(i, j);
                }
            }
        }

        if (wall)
        {
            //generate wall and clear a good path through it
            int wallLeftOpening = (Config.TacticalSizeX / 2) - 2;
            int wallRightOpening = (Config.TacticalSizeX / 2) + 1;

            for (int i = 0; i < Config.TacticalSizeX; i++)
            {
                if (i < wallLeftOpening || i > wallRightOpening)
                {
                    tiles[i, centerY] = TacticalTileType.wall;
                    decTilesUsed[i, centerY] = 0;
                }
                else
                {
                    tiles[i, centerY] = defaultType;
                }
            }
            for (int i = wallLeftOpening - 1; i <= wallRightOpening + 1; i++)
            {
                tiles[i, centerY - 1] = defaultType;
                tiles[i, centerY + 1] = defaultType;
            }
        }

        if (village != null)
        {
            int baseTile;
            if (terrainType == TerrainType.Volcanic)
            {
                baseTile = 332;
            }
            else if (terrainType == TerrainType.Desert)
                baseTile = 316;
            else
                baseTile = 300;

            for (int y = 0; y < Config.TacticalSizeY; y++)
            {
                tiles[HalfX - 1, y] = (TacticalTileType)baseTile + 4;
                tiles[HalfX, y] = (TacticalTileType)baseTile + 5;
            }
            for (int x = 0; x < Config.TacticalSizeX; x++)
            {
                tiles[x, Config.TacticalSizeY / 4] = (TacticalTileType)baseTile + 1;
            }
            tiles[HalfX - 1, Config.TacticalSizeY / 4] = (TacticalTileType)baseTile + 6;
            tiles[HalfX, Config.TacticalSizeY / 4] = (TacticalTileType)baseTile + 7;

            TacticalTileType[,] tempTiles = new TacticalTileType[Config.TacticalSizeX, Config.TacticalSizeY / 2];
            for (int x = 0; x < Config.TacticalSizeX; x++)
            {
                for (int y = 0; y < Config.TacticalSizeY / 2; y++)
                {
                    tempTiles[x, y] = tiles[x, y];
                }
            }
            foreach (TacticalBuilding building in buildings)
            {
                for (int y = 0; y < building.Height; y++)
                {
                    for (int x = 0; x < building.Width; x++)
                    {
                        tiles[building.LowerLeftPosition.x + x, building.LowerLeftPosition.y + y] = defaultType;
                        tempTiles[building.LowerLeftPosition.x + x, building.LowerLeftPosition.y + y] = TacticalTileType.house1;
                    }
                }
            }

            for (int x = 1; x < Config.TacticalSizeX; x++)
            {
                for (int y = 1; y < Config.TacticalSizeY / 2; y++)
                {
                    if (State.Rand.Next(100) == 1)
                    {
                        if (tempTiles[x - 1, y - 1] == defaultType && tempTiles[x, y - 1] == defaultType && tempTiles[x - 1, y] == defaultType && tempTiles[x, y] == defaultType && decTilesUsed[x, y] == 0)
                        {
                            buildings.Add(RandomBuilding(x, y));
                            tempTiles[x, y] = TacticalTileType.house1;
                        }
                    }
                    else if ((terrainType == TerrainType.Grass || terrainType == TerrainType.Forest) && State.Rand.Next(12) == 0)
                    {
                        if (tempTiles[x - 1, y - 1] == defaultType && tempTiles[x, y - 1] == defaultType && tempTiles[x - 1, y] == defaultType && tempTiles[x, y] == defaultType)
                        {
                            tempTiles[x, y] = RandomGrass(x, y);
                        }
                    }
                }
            }
        }
        State.GameManager.TacticalMode.Buildings = buildings.ToArray();
        State.GameManager.TacticalMode.DecorationStorage = placedDecorations.ToArray();

        if (buildings != null)
        {
            foreach (var building in buildings)
            {
                for (int y = 0; y < building.Height; y++)
                {
                    for (int x = 0; x < building.Width; x++)
                    {
                        blockedTile[building.LowerLeftPosition.x + x, building.LowerLeftPosition.y + y] = true;
                    }
                }
            }
        }

        State.GameManager.TacticalMode.SetBlockedTiles(blockedTile);
        State.GameManager.TacticalMode.DecorationStorage = placedDecorations.ToArray();
        TacticalTileLogic tileLogic = new TacticalTileLogic();
        tiles = tileLogic.ApplyLogic(tiles);

        if (wasWiped)
        {
            wasWiped = false;
            attempt++;
            tiles = GenMap(wall);
        }

        return tiles;

        TacticalTileType RandomGrass(int x, int y)
        {
            TacticalTileType ret;

            if (he_array[x, y] < Config.TacticalWaterValue - (0.01f * attempt))
            {
                ret = TacticalTileType.GrassOverWater;
                return ret;
            }

            ret = TacticalTileType.greengrass;

            return ret;
        }

        void PlaceGrassDecoration(int i, int j)
        {
            if (tiles[i, j] != TacticalTileType.GrassOverWater && terrainType == TerrainType.Forest && State.Rand.Next(7) == 0 && decTilesUsed[i, j] == 0)
            {
                TacticalDecoration decoration;
                TacDecType decType = TacticalDecorationList.GrassPureTrees[State.Rand.Next(TacticalDecorationList.GrassPureTrees.Length)];
                decoration = TacticalDecorationList.DecDict[decType];
                TryToPlaceDecoration(i, j, decoration, decType);
            }
            else if (tiles[i, j] != TacticalTileType.GrassOverWater && State.Rand.Next(8) == 0 && decTilesUsed[i, j] == 0)
            {
                TacticalDecoration decoration;
                TacDecType decType = TacticalDecorationList.GrassEnvironment[State.Rand.Next(TacticalDecorationList.GrassEnvironment.Length)];
                decoration = TacticalDecorationList.DecDict[decType];
                TryToPlaceDecoration(i, j, decoration, decType);
            }
        }

        TacticalTileType RandomBeach(int x, int y)
        {
            TacticalTileType ret;

            if (he_array[x, y] < Config.TacticalWaterValue - (0.01f * attempt))
            {
                ret = TacticalTileType.BeachOverOcean;
                return ret;
            }

            ret = (TacticalTileType)600;

            return ret;
        }

        void PlaceBeachDecoration(int i, int j)
        {
            if (tiles[i, j] != TacticalTileType.BeachOverOcean && State.Rand.Next(8) == 0 && decTilesUsed[i, j] == 0)
            {
                TacticalDecoration decoration;
                TacDecType decType = TacticalDecorationList.BeachEnvironment[State.Rand.Next(TacticalDecorationList.BeachEnvironment.Length)];
                decoration = TacticalDecorationList.DecDict[decType];
                TryToPlaceDecoration(i, j, decoration, decType);
            }
        }
        TacticalTileType RandomSwamp(int x, int y)
        {
            TacticalTileType ret;

            if (he_array[x, y] < Config.TacticalWaterValue - (0.01f * attempt))
            {
                ret = TacticalTileType.SwampOverBog;
                return ret;
            }

            ret = (TacticalTileType)700;

            return ret;
        }

        void PlaceSwampDecoration(int i, int j)
        {
            if (tiles[i, j] != TacticalTileType.SwampOverBog && State.Rand.Next(8) == 0 && decTilesUsed[i, j] == 0)
            {
                TacticalDecoration decoration;
                TacDecType decType = TacticalDecorationList.SwampEnvironment[State.Rand.Next(TacticalDecorationList.SwampEnvironment.Length)];
                decoration = TacticalDecorationList.DecDict[decType];
                TryToPlaceDecoration(i, j, decoration, decType);
            }
            if (tiles[i, j] == TacticalTileType.SwampOverBog && State.Rand.Next(8) == 0 && decTilesUsed[i, j] == 0)
            {
                TacticalDecoration decoration;
                TacDecType decType = TacticalDecorationList.SwampWaterEnvironment[State.Rand.Next(TacticalDecorationList.SwampWaterEnvironment.Length)];
                decoration = TacticalDecorationList.DecDict[decType];
                TryToPlaceDecoration(i, j, decoration, decType);
            }
        }

        TacticalBuilding RandomBuilding(int x, int y)
        {
            switch (State.Rand.Next(6))
            {
                case 0:
                case 1:
                    return new Well(new Vec2(x, y));
                case 2:
                case 3:
                    return new Barrels(new Vec2(x, y));
                case 4:
                    return new LogPile(new Vec2(x, y));
                default:
                    return new Log1x1(new Vec2(x, y));
            }
        }

        void TryToPlaceDecoration(int i, int j, TacticalDecoration decoration, TacDecType decorationType)
        {
            if (wall) //Can't obstruct the wall
            {
                if (j == centerY)
                    return;
                if (j < centerY && j + decoration.Tile.GetUpperBound(1) >= centerY)
                    return;
            }
            if (village != null) //Can't obstruct the path
            {
                if (i <= HalfX && i + decoration.Tile.GetLength(0) >= HalfX - 1)
                    return;
                if (j <= Config.TacticalSizeY / 4 && j + decoration.Tile.GetLength(1) >= Config.TacticalSizeY / 4)
                    return;
            }
            for (int x = 0; x < decoration.Tile.GetLength(0); x++)
            {
                for (int y = 0; y < decoration.Tile.GetLength(1); y++)
                {
                    if (x + i >= decTilesUsed.GetLength(0) || y + j >= decTilesUsed.GetLength(1))
                        continue;
                    if (decTilesUsed[x + i, y + j] != 0)
                        return;
                    if (blockedTile[x + i, y + j])
                        return;
                }
            }
            placedDecorations.Add(new DecorationStorage(new Vec2(i, j), decorationType));
            for (int x = 0; x < decoration.Tile.GetLength(0); x++)
            {
                for (int y = 0; y < decoration.Tile.GetLength(1); y++)
                {
                    if (x + i >= decTilesUsed.GetLength(0) || y + j >= decTilesUsed.GetLength(1))
                        continue;
                    decTilesUsed[x + i, y + j] = decoration.Tile[x, y];
                }
            }
            for (int x = 0; x < decoration.Width; x++)
            {
                for (int y = 0; y < decoration.Height; y++)
                {
                    if (x + i >= blockedTile.GetLength(0) || y + j >= blockedTile.GetLength(1))
                        continue;
                    blockedTile[x + i, y + j] = true;
                }
            }
        }
    }

    internal void PlaceRowOfBuildings(TacticalTileType[,] tiles, List<TacticalBuilding> buildings, int x, int y, int change)
    {
        int lastWidth = 0;
        for (int i = 0; i < 10; i++)
        {
            var building = RandomBuilding();
            if (building != null)
            {
                buildings.Add(building);
                lastWidth = building.Width;
            }
            else
            {
                lastWidth = 1;
            }
            if (building.Width == 2 && change < 0)
            {
                building.LowerLeftPosition.x--;
            }

            for (int xx = 0; xx < building.Width; xx++)
            {
                for (int yy = 0; yy < building.Height; yy++)
                {
                    blockedTile[building.LowerLeftPosition.x + xx, building.LowerLeftPosition.y + yy] = true;
                }
            }

            if (State.Rand.Next(3) == 0)
                lastWidth += 1;
            x += (lastWidth + 1) * change;
            if (x < 1 || x + 2 > tiles.GetUpperBound(0))
                break;
        }

        TacticalBuilding RandomBuilding()
        {
            Vec2 loc = new Vec2(x, y);
            switch (village.Race)
            {
                case Race.Harpies:
                    return GetRandomBuildingFrom(loc, typeof(HarpyNest), typeof(HarpyNestCanopy));
                case Race.Lamia:
                    return GetRandomBuildingFrom(loc, typeof(StoneHouse), typeof(LamiaTemple), typeof(FancyStoneHouse));
                case Race.Cats:
                    if (State.Rand.Next(2) == 0)
                        return GetRandomBuildingFrom(loc, typeof(CatHouse), typeof(YellowCobbleStoneHouse));
                    break;
                case Race.Youko:
                case Race.Foxes:
                    if (State.Rand.Next(3) == 0)
                        return new FoxStoneHouse(loc);
                    break;
                case Race.Crux:
                case Race.Kangaroos:
                    return GetRandomBuildingFrom(loc, typeof(LogCabin), typeof(Log1x2), typeof(Log1x1));
            }

            switch (State.Rand.Next(7))
            {
                case 0:
                case 1:
                    return new LogCabin(loc);
                case 2:
                    return new Log1x2(loc);
                case 3:
                    return new StoneHouse(loc);
                case 4:
                    return new CobbleStoneHouse(loc);
                case 5:
                    return new FancyStoneHouse(loc);
                default:
                    return new Log1x1(loc);
            }
        }
    }

    TacticalBuilding GetRandomBuildingFrom(Vec2 location, params Type[] buildings)
    {
        var rand = UnityEngine.Random.Range(0, buildings.Length);
        return (TacticalBuilding)Activator.CreateInstance(buildings[rand], location);
    }

    public float he_zoom = Config.TacticalTerrainFrequency;
    public float he_factor = 3; //1.8 to 4 look good
    public Vector2 he_seed = new Vector2(0, 0);

    float[,] he_array;

    //calculate the value of an element of the array based on noise and location
    float FractalNoise(int i, int j, float zoom, float factor, Vector2 seed)
    {
        i = i + Mathf.RoundToInt(seed.x * zoom);
        j = j + Mathf.RoundToInt(seed.y * zoom);
        //fractal behavior occurs here. Everything else is parameter fine-tuning
        return 0
        + Mathf.PerlinNoise(i / zoom, j / zoom) / 3
        + Mathf.PerlinNoise(i / (zoom / factor), j / (zoom / factor)) / 3
        + Mathf.PerlinNoise(i / (zoom / factor * factor), j / (zoom / factor * factor)) / 3;
    }

    void MakeArrays()
    {
        he_array = new float[Config.TacticalSizeX, Config.TacticalSizeY];
        RecalculateArray();
    }

    void RecalculateArray()
    {
        for (int i = 0; i < Config.TacticalSizeX; i++)
        {
            for (int j = 0; j < Config.TacticalSizeY; j++)
            {
                he_array[i, j] = FractalNoise(i, j, he_zoom, he_factor, he_seed);
            }
        }
    }
}
