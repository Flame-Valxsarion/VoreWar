using System.Collections.Generic;
using UnityEngine;
using System.Linq;


enum Neighbor
{
    North,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest
}

enum Wanted
{
    Yes,
    No,
    DontCare
}

struct DirectionalInfo
{
    internal Wanted north;
    internal Wanted northeast;
    internal Wanted east;
    internal Wanted southeast;
    internal Wanted south;
    internal Wanted southwest;
    internal Wanted west;
    internal Wanted northwest;

    public DirectionalInfo(Wanted north = Wanted.DontCare, Wanted northeast = Wanted.DontCare, Wanted east = Wanted.DontCare, Wanted southeast = Wanted.DontCare, Wanted south = Wanted.DontCare, Wanted southwest = Wanted.DontCare, Wanted west = Wanted.DontCare, Wanted northwest = Wanted.DontCare)
    {
        this.north = north;
        this.northeast = northeast;
        this.east = east;
        this.southeast = southeast;
        this.south = south;
        this.southwest = southwest;
        this.west = west;
        this.northwest = northwest;
    }
}

class TacticalTileLogic
{
    TacticalTileType[,] tiles;
    TacticalTileType[,] newtiles;

    internal TacticalTileType[,] ApplyLogic(TacticalTileType[,] tiles)
    {
        this.tiles = tiles;
        newtiles = new TacticalTileType[tiles.GetLength(0), tiles.GetLength(1)];
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                switch (tiles[x, y])
                {
                    case TacticalTileType.RockOverSand:
                        {
                            int type = DetermineType(new Vec2(x, y), TacticalTileType.RockOverSand);
                            newtiles[x, y] = (TacticalTileType)(2000 + type);
                            break;
                        }
                    case TacticalTileType.RockOverTar:
                        {
                            int type = DetermineType(new Vec2(x, y), TacticalTileType.RockOverTar);
                            newtiles[x, y] = (TacticalTileType)(2100 + type);
                            break;
                        }
                    case TacticalTileType.GrassOverWater:
                        {
                            int type = DetermineType(new Vec2(x, y), TacticalTileType.GrassOverWater);
                            newtiles[x, y] = (TacticalTileType)(2200 + type);
                            break;
                        }
                    case TacticalTileType.VolcanicOverGravel:
                        {
                            int type = DetermineType(new Vec2(x, y), TacticalTileType.VolcanicOverGravel);
                            newtiles[x, y] = (TacticalTileType)(2300 + type);
                            break;
                        }
                    case TacticalTileType.VolcanicOverLava:
                        {
                            int type = DetermineType(new Vec2(x, y), TacticalTileType.VolcanicOverLava);
                            newtiles[x, y] = (TacticalTileType)(2400 + type);
                            break;
                        }
                    default:
                        newtiles[x, y] = tiles[x, y];
                        continue;

                }
            }
        }
        return newtiles;
    }

    internal int DetermineType(Vec2 pos, TacticalTileType type)
    {
        Wanted yes = Wanted.Yes;
        Wanted no = Wanted.No;
        Wanted any = Wanted.DontCare;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, any, no, any))) return 0;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, yes, yes, any))) return 1;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, yes, yes, any))) return 2;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, any, no, any, no, any))) return 3;
        if (AreaCheck(pos, type, new DirectionalInfo(north: no, west: yes, south: no, east: yes))) return 4;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, yes, any))) return 5;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, yes, yes, yes))) return 6;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, no, yes, yes))) return 7;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, any, no, any))) return 8;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, yes, yes, yes))) return 9;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, yes, yes, yes))) return 10;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, no, yes, no))) return 11;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, any, no, any))) return 12;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, no, any))) return 13;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, yes, yes, yes))) return 14;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, yes, yes, no))) return 15;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, no, any))) return 16;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, yes, yes))) return 17;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, yes, yes))) return 18;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, no, any))) return 19;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, any, no, any))) return 20;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, any, no, any))) return 21;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, no, yes, any))) return 22;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, no, yes, yes))) return 23;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, yes, yes, any))) return 24;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, no, yes, any))) return 25;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, any, no, any))) return 26;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, no, yes, yes))) return 27;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, no, any))) return 28;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, no, any))) return 29;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, yes, no))) return 30;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, yes, yes, no))) return 31;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, yes, yes))) return 32;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, yes, no))) return 33;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, any, no, any))) return 34;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, yes, yes, no))) return 35;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, any, no, any))) return 36;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, no, yes, any))) return 37;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, yes, no))) return 38;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, no, yes, no))) return 39;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, no, yes, no))) return 40;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, yes, yes, yes))) return 41;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, no, yes, yes))) return 42;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, yes, yes, no))) return 43;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, no, yes, no))) return 44;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, no, yes, yes))) return 45;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, no, yes, no))) return 46;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, yes, yes, no))) return 47;
        UnityEngine.Debug.Log("fallthrough");
        return 9;

    }


    internal bool AreaCheck(Vec2 pos, TacticalTileType type, DirectionalInfo info)
    {

        if (Fails(info.north, Neighbor.North))
            return false;
        if (Fails(info.northeast, Neighbor.NorthEast))
            return false;
        if (Fails(info.east, Neighbor.East))
            return false;
        if (Fails(info.southeast, Neighbor.SouthEast))
            return false;
        if (Fails(info.south, Neighbor.South))
            return false;
        if (Fails(info.southwest, Neighbor.SouthWest))
            return false;
        if (Fails(info.west, Neighbor.West))
            return false;
        if (Fails(info.northwest, Neighbor.NorthWest))
            return false;

        return true;

        bool Fails(Wanted direction, Neighbor neighbor)
        {
            if (direction == Wanted.DontCare)
                return false;
            if (direction == Wanted.Yes && IsTileType(GetPos(pos, neighbor), type) == false)
                return true;
            if (direction == Wanted.No && IsTileType(GetPos(pos, neighbor), type))
                return true;
            return false;
        }
    }

    internal Vec2 GetPos(Vec2 startingPos, Neighbor direction)
    {
        switch (direction)
        {
            case Neighbor.North:
                return new Vec2(startingPos.x, startingPos.y + 1);
            case Neighbor.NorthEast:
                return new Vec2(startingPos.x + 1, startingPos.y + 1);
            case Neighbor.East:
                return new Vec2(startingPos.x + 1, startingPos.y);
            case Neighbor.SouthEast:
                return new Vec2(startingPos.x + 1, startingPos.y - 1);
            case Neighbor.South:
                return new Vec2(startingPos.x, startingPos.y - 1);
            case Neighbor.SouthWest:
                return new Vec2(startingPos.x - 1, startingPos.y - 1);
            case Neighbor.West:
                return new Vec2(startingPos.x - 1, startingPos.y);
            case Neighbor.NorthWest:
                return new Vec2(startingPos.x - 1, startingPos.y + 1);
            default:
                return startingPos;
        }
    }

    internal bool IsTileType(Vec2 pos, TacticalTileType type)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x > tiles.GetUpperBound(0) || pos.y > tiles.GetUpperBound(1))
            return false;
        return tiles[pos.x, pos.y] == type;
    }
}

class StrategicTileLogic
{
    StrategicTileType[,] tiles;
    StrategicTileType[,] newtiles;
    int type;

    internal StrategicTileType[,] ApplyLogic(StrategicTileType[,] originalTiles, out StrategicTileType[,] overTiles, out StrategicTileType[,] underTiles)
    {
        tiles = new StrategicTileType[originalTiles.GetLength(0), originalTiles.GetLength(1)];
        StrategicTileType[,] temptiles = new StrategicTileType[originalTiles.GetLength(0), originalTiles.GetLength(1)];
        underTiles = new StrategicTileType[tiles.GetLength(0), tiles.GetLength(1)];
        for (int x = 0; x < originalTiles.GetLength(0); x++)
        {
            for (int y = 0; y < originalTiles.GetLength(1); y++)
            {
                temptiles[x, y] = originalTiles[x, y];
                tiles[x, y] = originalTiles[x, y];
                underTiles[x, y] = (StrategicTileType)99;
            }
        }
        newtiles = new StrategicTileType[tiles.GetLength(0), tiles.GetLength(1)];
        overTiles = new StrategicTileType[tiles.GetLength(0), tiles.GetLength(1)];

        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (StrategicTileInfo.ConsideredLiquid.Contains(tiles[x, y]))
                {
                    type = DetermineType(new Vec2(x, y), tiles[x, y]);
                    overTiles[x, y] = (StrategicTileType)(2000 + type);
                    if (type == 19)
                        overTiles[x, y] = tiles[x, y];
                    Dictionary<StrategicTileType, int> greatest_near = DirectBorderTiles(x, y);
                    if (greatest_near.Count == 0)
                    {
                        greatest_near = DirectBorderTiles(x, y, true);
                    }
                    StrategicTileType keyOfMaxValue = greatest_near.Aggregate((a, b) => a.Value > b.Value ? a : b).Key;
                    //Debug.Log(keyOfMaxValue);
                    temptiles[x, y] = (StrategicTileType)StrategicTileInfo.GetTileType(keyOfMaxValue, x, y);
                    


                    continue;
                }

            }
        }
        tiles = temptiles;
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (overTiles[x, y] != 0)
                {
                    underTiles[x, y] = tiles[x, y];
                    type = DetermineType(new Vec2(x, y), tiles[x, y]);
                    newtiles[x, y] = (StrategicTileType)(2100 + type);
                    //newtiles[x, y] = tiles[x, y];
                    continue;
                }
                else if (Config.HardLava)
                {
                    if (tiles[x, y] == StrategicTileType.lava)
                        overTiles[x, y] = StrategicTileType.lava;
                    if (tiles[x, y] == StrategicTileType.volcanic)
                        overTiles[x, y] = StrategicTileType.volcanic;

                }
                type = DetermineType(new Vec2(x, y), tiles[x, y]);
                newtiles[x, y] = (StrategicTileType)(2100 + type);
            }
        }
        return newtiles;

    }
    internal Dictionary<StrategicTileType, int> DirectBorderTiles(int x, int y, bool liquid_check = false)
    {
        Dictionary<StrategicTileType, int> greatest_near = new Dictionary<StrategicTileType, int>(); ;
        StrategicTileType north = GetTileType(new Vec2(x, y + 1));
        StrategicTileType south = GetTileType(new Vec2(x, y + 1));
        StrategicTileType west = GetTileType(new Vec2(x - 1, y));
        StrategicTileType east = GetTileType(new Vec2(x + 1, y));
        StrategicTileType northeast = GetTileType(new Vec2(x + 1, y + 1));
        StrategicTileType southeast = GetTileType(new Vec2(x + 1, y - 1));
        StrategicTileType northwest = GetTileType(new Vec2(x - 1, y + 1));
        StrategicTileType southwest = GetTileType(new Vec2(x - 1, y - 1));

        if (north != (StrategicTileType)8000)
            greatest_near.Add(north, 3);
        if (south != (StrategicTileType)8000)
            if (greatest_near.ContainsKey(south))
            {
                greatest_near[south] += 3;
            }
            else
            {
                greatest_near.Add(south, 3);
            }
        if (west != (StrategicTileType)8000)
            if (greatest_near.ContainsKey(west))
            {
                greatest_near[west] += 3;
            }
            else
            {
                greatest_near.Add(west, 3);
            }
        if (east != (StrategicTileType)8000)
            if (greatest_near.ContainsKey(east))
            {
                greatest_near[east] += 3;
            }
            else
            {
                greatest_near.Add(east, 3);
            }
        if (northeast != (StrategicTileType)8000)
            if (greatest_near.ContainsKey(northeast))
            {
                greatest_near[northeast] += 1;
            }
            else
            {
                greatest_near.Add(northeast, 1);
            }
        if (southeast != (StrategicTileType)8000)
            if (greatest_near.ContainsKey(southeast))
            {
                greatest_near[southeast] += 1;
            }
            else
            {
                greatest_near.Add(southeast, 1);
            }
        if (northwest != (StrategicTileType)8000)
            if (greatest_near.ContainsKey(northwest))
            {
                greatest_near[northwest] += 1;
            }
            else
            {
                greatest_near.Add(northwest, 1);
            }
        if (southwest != (StrategicTileType)8000)
            if (greatest_near.ContainsKey(southwest))
            {
                greatest_near[southwest] += 1;
            }
            else
            {
                greatest_near.Add(southwest, 1);
            }
        if (!liquid_check)
        {
            foreach (StrategicTileType i in StrategicTileInfo.ConsideredLiquid)
            {
                if (greatest_near.ContainsKey(i))
                {
                    greatest_near.Remove(i);
                }
            }
        }
        return greatest_near;
    }
    internal int DetermineType(Vec2 pos, StrategicTileType type, bool inverted = false)
    {
        Wanted yes;
        Wanted no;
        if (inverted)
        {
            yes = Wanted.No;
            no = Wanted.Yes;
        }
        else
        {
            yes = Wanted.Yes;
            no = Wanted.No;
        }


        Wanted any = Wanted.DontCare;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, any, no, any))) return 0;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, yes, yes, any))) return 1;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, yes, yes, any))) return 2;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, any, no, any, no, any))) return 3;
        if (AreaCheck(pos, type, new DirectionalInfo(north: no, west: yes, south: no, east: yes))) return 4;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, yes, any))) return 5;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, yes, yes, yes))) return 6;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, no, yes, yes))) return 7;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, any, no, any))) return 8;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, yes, yes, yes))) return 9;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, yes, yes, yes))) return 10;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, no, yes, no))) return 11;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, any, no, any))) return 12;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, no, any))) return 13;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, yes, yes, yes))) return 14;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, yes, yes, no))) return 15;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, no, any))) return 16;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, yes, yes))) return 17;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, yes, yes))) return 18;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, no, any))) return 19;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, any, no, any))) return 20;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, any, no, any))) return 21;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, no, yes, any))) return 22;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, no, yes, yes))) return 23;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, yes, yes, any))) return 24;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, no, yes, any))) return 25;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, any, no, any))) return 26;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, no, yes, yes))) return 27;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, no, any))) return 28;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, no, any))) return 29;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, yes, no))) return 30;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, yes, yes, no))) return 31;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, yes, yes))) return 32;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, yes, no))) return 33;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, any, no, any))) return 34;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, yes, yes, no))) return 35;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, any, no, any))) return 36;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, no, yes, any))) return 37;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, yes, no))) return 38;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, no, yes, no))) return 39;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, no, yes, no))) return 40;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, yes, yes, yes))) return 41;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, no, yes, yes))) return 42;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, yes, yes, no))) return 43;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, no, yes, no))) return 44;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, no, yes, yes))) return 45;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, no, yes, no))) return 46;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, yes, yes, no))) return 47;
        UnityEngine.Debug.Log("fallthrough");
        return 9;

    }


    internal bool AreaCheck(Vec2 pos, StrategicTileType type, DirectionalInfo info)
    {

        if (Fails(info.north, Neighbor.North))
            return false;
        if (Fails(info.northeast, Neighbor.NorthEast))
            return false;
        if (Fails(info.east, Neighbor.East))
            return false;
        if (Fails(info.southeast, Neighbor.SouthEast))
            return false;
        if (Fails(info.south, Neighbor.South))
            return false;
        if (Fails(info.southwest, Neighbor.SouthWest))
            return false;
        if (Fails(info.west, Neighbor.West))
            return false;
        if (Fails(info.northwest, Neighbor.NorthWest))
            return false;

        return true;

        bool Fails(Wanted direction, Neighbor neighbor)
        {
            if (direction == Wanted.DontCare)
                return false;
            if (direction == Wanted.Yes && (IsTileType(GetPos(pos, neighbor), type) == false))
                return true;
            if (direction == Wanted.No && IsTileType(GetPos(pos, neighbor), type))
                return true;
            return false;
        }
    }

    internal Vec2 GetPos(Vec2 startingPos, Neighbor direction)
    {
        switch (direction)
        {
            case Neighbor.North:
                return new Vec2(startingPos.x, startingPos.y + 1);
            case Neighbor.NorthEast:
                return new Vec2(startingPos.x + 1, startingPos.y + 1);
            case Neighbor.East:
                return new Vec2(startingPos.x + 1, startingPos.y);
            case Neighbor.SouthEast:
                return new Vec2(startingPos.x + 1, startingPos.y - 1);
            case Neighbor.South:
                return new Vec2(startingPos.x, startingPos.y - 1);
            case Neighbor.SouthWest:
                return new Vec2(startingPos.x - 1, startingPos.y - 1);
            case Neighbor.West:
                return new Vec2(startingPos.x - 1, startingPos.y);
            case Neighbor.NorthWest:
                return new Vec2(startingPos.x - 1, startingPos.y + 1);
            default:
                return startingPos;
        }
    }

    internal StrategicTileType GetTileType(Vec2 pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x > tiles.GetUpperBound(0) || pos.y > tiles.GetUpperBound(1))
            return (StrategicTileType)8000;
        StrategicTileType type = State.World.Tiles[pos.x, pos.y];
        if (StrategicTileInfo.ConsideredLiquid.Contains(type))
        {
            return type;
        }
        if (StrategicTileInfo.SandFamily.Contains(type))
        {
            return StrategicTileType.desert;
        }
        if (StrategicTileInfo.GrassFamily.Contains(type))
        {
            return StrategicTileType.grass;
        }
        if (StrategicTileInfo.SnowFamily.Contains(type))
        {
            return StrategicTileType.snow;
        }
        if (StrategicTileInfo.AshenFamily.Contains(type))
        {
            return StrategicTileType.ashen;
        }
        if (StrategicTileInfo.ShallowWaterFamily.Contains(type))
        {
            return StrategicTileType.smallIslands;
        }
        if (StrategicTileInfo.SavannahFamily.Contains(type))
        {
            return StrategicTileType.savannah;
        }
        //if (StrategicTileInfo.WaterFamily.Contains(type))
        //{
        //    return StrategicTileInfo.WaterFamily.Contains(tiles[pos.x, pos.y]);
        //}
        return type;
    }

    internal bool IsTileType(Vec2 pos, StrategicTileType type)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x > tiles.GetUpperBound(0) || pos.y > tiles.GetUpperBound(1))
            return true;
        if (StrategicTileInfo.ConsideredLiquid.Contains(type))
        {
            return StrategicTileInfo.ConsideredLiquid.Contains(tiles[pos.x, pos.y]) || tiles[pos.x, pos.y] == StrategicTileType.smallIslands;
        }
        if (StrategicTileInfo.SandFamily.Contains(type))
        {
            return StrategicTileInfo.SandFamily.Contains(tiles[pos.x, pos.y]);
        }
        if (StrategicTileInfo.GrassFamily.Contains(type))
        {
            return StrategicTileInfo.GrassFamily.Contains(tiles[pos.x, pos.y]);
        }
        if (StrategicTileInfo.SnowFamily.Contains(type))
        {
            return StrategicTileInfo.SnowFamily.Contains(tiles[pos.x, pos.y]);
        }
        if (StrategicTileInfo.AshenFamily.Contains(type))
        {
            return StrategicTileInfo.AshenFamily.Contains(tiles[pos.x, pos.y]);
        }
        if (StrategicTileInfo.ShallowWaterFamily.Contains(type))
        {
            if (StrategicTileInfo.ConsideredLiquid.Contains(tiles[pos.x, pos.y]))
            {
                return true;
            }
            return StrategicTileInfo.ShallowWaterFamily.Contains(tiles[pos.x, pos.y]);
        }
        if (StrategicTileInfo.SavannahFamily.Contains(type))
        {
            return StrategicTileInfo.SavannahFamily.Contains(tiles[pos.x, pos.y]);
        }
        //if (StrategicTileInfo.WaterFamily.Contains(type))
        //{
        //    return StrategicTileInfo.WaterFamily.Contains(tiles[pos.x, pos.y]);
        //}
        return tiles[pos.x, pos.y] == type;
    }

    internal bool AreTypesSame(Vec2 pos1, Vec2 pos2)
    {
        StrategicTileType postion1 = State.World.Tiles[pos1.x, pos1.y];
        StrategicTileType postion2 = State.World.Tiles[pos2.x, pos2.y];



        if ((StrategicTileInfo.SandFamily.Contains(postion1) && StrategicTileInfo.SandFamily.Contains(postion2)) ||
            (StrategicTileInfo.GrassFamily.Contains(postion1) && StrategicTileInfo.GrassFamily.Contains(postion2)) ||
            (StrategicTileInfo.SnowFamily.Contains(postion1) && StrategicTileInfo.SnowFamily.Contains(postion2)) ||
            (StrategicTileInfo.AshenFamily.Contains(postion1) && StrategicTileInfo.AshenFamily.Contains(postion2)) ||
            (StrategicTileInfo.ShallowWaterFamily.Contains(postion1) && StrategicTileInfo.ShallowWaterFamily.Contains(postion2)) ||
            (StrategicTileInfo.SavannahFamily.Contains(postion1) && StrategicTileInfo.SavannahFamily.Contains(postion2)))
        {
            return true;
        }

        return postion1 == postion2;
    }

    internal IEnumerable<KeyValuePair<int, StrategicTileType>> DetermineOverlay(int x, int y)
    {
        Vec2 pos = new Vec2(x, y);
        int dir = DetermineType(pos, State.World.Tiles[x,y]);
        List<StrategicTileType> adj_types = new List<StrategicTileType>();



        var temp_Dict = from entry in GetOverlayTypes(dir) orderby entry.Value ascending select entry;

        Vec2 temp_vec;
        Vec2 comp_vec;

        List<int> north_flat = new List<int> { 0, 1, 2, 3, 4, 5, 12, 21, 22, 24, 25, 37 }; // 1
        List<int> east_flat = new List<int> { 2, 5, 6, 10, 12, 18, 20, 22, 27, 28, 30, 35, 39 }; // 10
        List<int> south_flat = new List<int> { 3, 4, 5, 16, 17, 18, 28, 29, 30, 32, 33, 38 }; // 17
        List<int> west_flat = new List<int> { 0, 3, 8, 12, 16, 20, 21, 26, 27, 28, 34, 36, }; // 8
        IEnumerable<KeyValuePair<int, StrategicTileType>> conat_list = temp_Dict;

        bool has_flat_north = north_flat.Contains(dir);
        bool has_flat_east = east_flat.Contains(dir);
        bool has_flat_south = south_flat.Contains(dir);
        bool has_flat_west = west_flat.Contains(dir);
        
        //Debug.Log(dir + ": " + x + ", " + y);

        if (pos.x > 1 && pos.y < State.World.Tiles.GetUpperBound(1) - 1)
        {

            if (!AreTypesSame(GetPos(pos, Neighbor.NorthWest), GetPos(pos, Neighbor.North)) && !AreTypesSame(GetPos(pos, Neighbor.NorthWest), GetPos(pos, Neighbor.West)) && (AreTypesSame(GetPos(pos, Neighbor.North), GetPos(pos, Neighbor.West)) || (has_flat_north || has_flat_west)))
            {
                temp_vec = GetPos(pos, Neighbor.NorthWest);
                comp_vec = GetPos(pos, has_flat_west ? has_flat_north ? Neighbor.NorthWest : Neighbor.West : Neighbor.North);
                if (State.World.Tiles[temp_vec.x, temp_vec.y] > State.World.Tiles[comp_vec.x, comp_vec.y])
                {
                    KeyValuePair<int, StrategicTileType> corner = new KeyValuePair<int, StrategicTileType>(15, State.World.Tiles[temp_vec.x, temp_vec.y]);
                    conat_list = temp_Dict.Append(corner);
                }
            }
        }
        if (pos.x < State.World.Tiles.GetUpperBound(0) - 1 && pos.y < State.World.Tiles.GetUpperBound(1) - 1)
        {
            if (!AreTypesSame(GetPos(pos, Neighbor.NorthEast), GetPos(pos, Neighbor.North)) && !AreTypesSame(GetPos(pos, Neighbor.NorthEast), GetPos(pos, Neighbor.East)) && (AreTypesSame(GetPos(pos, Neighbor.North), GetPos(pos, Neighbor.East)) || (has_flat_north || has_flat_east)))
            {
                temp_vec = GetPos(pos, Neighbor.NorthEast);
                comp_vec = GetPos(pos, has_flat_east ? has_flat_north ? Neighbor.NorthEast : Neighbor.East : Neighbor.North);
                if (State.World.Tiles[temp_vec.x, temp_vec.y] > State.World.Tiles[comp_vec.x, comp_vec.y])
                {
                    KeyValuePair<int, StrategicTileType> corner = new KeyValuePair<int, StrategicTileType>(14, State.World.Tiles[temp_vec.x, temp_vec.y]);
                    conat_list = conat_list.Append(corner);
                }

            }
        }
        if (pos.x > 1 && pos.y > 1)
        {
            if (!AreTypesSame(GetPos(pos, Neighbor.SouthWest), GetPos(pos, Neighbor.South)) && !AreTypesSame(GetPos(pos, Neighbor.SouthWest), GetPos(pos, Neighbor.West)) && (AreTypesSame(GetPos(pos, Neighbor.South), GetPos(pos, Neighbor.West)) || (has_flat_south || has_flat_west)))
            {
                temp_vec = GetPos(pos, Neighbor.SouthWest);
                comp_vec = GetPos(pos, has_flat_west ? has_flat_south ? Neighbor.SouthWest : Neighbor.West : Neighbor.South);
                if (State.World.Tiles[temp_vec.x, temp_vec.y] > State.World.Tiles[comp_vec.x, comp_vec.y])
                {
                    KeyValuePair<int, StrategicTileType> corner = new KeyValuePair<int, StrategicTileType>(7, State.World.Tiles[temp_vec.x, temp_vec.y]);
                    conat_list = conat_list.Append(corner);
                }
            }
        }
        if (pos.x < State.World.Tiles.GetUpperBound(0) - 1 && pos.y > 1)
        {
            if (!AreTypesSame(GetPos(pos, Neighbor.SouthEast), GetPos(pos, Neighbor.East)) && !AreTypesSame(GetPos(pos, Neighbor.SouthEast), GetPos(pos, Neighbor.South)) && (AreTypesSame(GetPos(pos, Neighbor.East), GetPos(pos, Neighbor.South)) || (has_flat_south || has_flat_east)))
            {
                temp_vec = GetPos(pos, Neighbor.SouthEast);
                comp_vec = GetPos(pos, has_flat_east ? has_flat_south ? Neighbor.SouthEast : Neighbor.East : Neighbor.South);
                if (State.World.Tiles[temp_vec.x, temp_vec.y] > State.World.Tiles[comp_vec.x, comp_vec.y])
                {
                    KeyValuePair<int, StrategicTileType> corner = new KeyValuePair<int, StrategicTileType>(6, State.World.Tiles[temp_vec.x, temp_vec.y]);
                    conat_list = conat_list.Append(corner);
                }

            }
        }

        return conat_list;

        Dictionary<int, StrategicTileType> GetOverlayTypes(int direction)
        {
            Dictionary<int, StrategicTileType> true_types = new Dictionary<int, StrategicTileType>();

            Vec2 temp_vec1;
            Vec2 temp_vec2;
            Vec2 temp_vec3;
            Vec2 temp_vec4;

            switch (direction)
            {
                case 0:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.West);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(0, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(1, GetTileType(temp_vec1));
                        true_types.Add(8, GetTileType(temp_vec2));
                    }
                    break;
                case 1:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    true_types.Add(1, GetTileType(temp_vec1));
                    break;
                case 2:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.East);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(2, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(1, GetTileType(temp_vec1));
                        true_types.Add(10, GetTileType(temp_vec2));
                    }
                    break;
                case 3:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.West);
                    temp_vec3 = GetPos(pos, Neighbor.South);
                    bool t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    bool t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    bool t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(3, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(0, GetTileType(temp_vec1));
                            true_types.Add(17, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(4, GetTileType(temp_vec1));
                            true_types.Add(8, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(1, GetTileType(temp_vec1));
                            true_types.Add(16, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(1, GetTileType(temp_vec1));
                            true_types.Add(8, GetTileType(temp_vec2));
                            true_types.Add(17, GetTileType(temp_vec3));
                        }
                    }
                    break;
                case 4:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.South);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(4, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(1, GetTileType(temp_vec1));
                        true_types.Add(17, GetTileType(temp_vec2));
                    }
                    break;
                case 5:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.East);
                    temp_vec3 = GetPos(pos, Neighbor.South);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(5, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(2, GetTileType(temp_vec1));
                            true_types.Add(17, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(4, GetTileType(temp_vec1));
                            true_types.Add(10, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(1, GetTileType(temp_vec1));
                            true_types.Add(18, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(1, GetTileType(temp_vec1));
                            true_types.Add(10, GetTileType(temp_vec2));
                            true_types.Add(17, GetTileType(temp_vec3));
                        }
                    }
                    break;
                case 6:
                    temp_vec1 = GetPos(pos, Neighbor.SouthEast);
                    true_types.Add(6, GetTileType(temp_vec1));
                    break;
                case 7:
                    temp_vec1 = GetPos(pos, Neighbor.SouthWest);
                    true_types.Add(7, GetTileType(temp_vec1));
                    break;
                case 8:
                    temp_vec1 = GetPos(pos, Neighbor.West);
                    true_types.Add(8, GetTileType(temp_vec1));
                    break;
                case 9:
                    //true_types.Add(State.World.Tiles[x,y]); // This is an empty space on the sprite sheet
                    break;
                case 10:
                    temp_vec1 = GetPos(pos, Neighbor.East);
                    true_types.Add(10, GetTileType(temp_vec1));
                    break;
                case 11:
                    temp_vec1 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec2 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec3 = GetPos(pos, Neighbor.SouthEast);
                    temp_vec4 = GetPos(pos, Neighbor.SouthWest);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    bool t1_t4_same = AreTypesSame(temp_vec1, temp_vec4);
                    bool t2_t4_same = AreTypesSame(temp_vec2, temp_vec4);
                    bool t3_t4_same = AreTypesSame(temp_vec3, temp_vec4);
                    if (t1_t2_same && t1_t3_same && t1_t4_same) //all same
                    {
                        true_types.Add(11, GetTileType(temp_vec1));
                    }
                    else if (t1_t2_same && t1_t3_same) // t4 not same
                    {
                        true_types.Add(47, GetTileType(temp_vec1));
                        true_types.Add(7, GetTileType(temp_vec4));
                    }
                    else if (t1_t3_same && t1_t4_same) // t2 not same
                    {
                        true_types.Add(45, GetTileType(temp_vec1));
                        true_types.Add(15, GetTileType(temp_vec4));
                    }
                    else if (t1_t2_same && t1_t4_same) // t3 not same
                    {
                        true_types.Add(46, GetTileType(temp_vec1));
                        true_types.Add(6, GetTileType(temp_vec4));
                    }
                    else if (t2_t3_same && t2_t4_same)
                    {
                        true_types.Add(44, GetTileType(temp_vec1));
                        true_types.Add(14, GetTileType(temp_vec4));
                    }
                    else if (t1_t2_same && t3_t4_same)
                    {
                        true_types.Add(43, GetTileType(temp_vec1));
                        true_types.Add(42, GetTileType(temp_vec4));
                    }
                    else if (t1_t3_same && t2_t4_same)
                    {
                        true_types.Add(41, GetTileType(temp_vec1));
                        true_types.Add(40, GetTileType(temp_vec4));
                    }
                    else if (t1_t4_same && t2_t3_same)
                    {
                        true_types.Add(23, GetTileType(temp_vec1));
                        true_types.Add(31, GetTileType(temp_vec2));
                    }
                    else if (t1_t2_same)
                    {
                        true_types.Add(43, GetTileType(temp_vec1));
                        true_types.Add(6, GetTileType(temp_vec3));
                        true_types.Add(7, GetTileType(temp_vec4));
                    }
                    else if (t1_t3_same)
                    {
                        true_types.Add(42, GetTileType(temp_vec1));
                        true_types.Add(15, GetTileType(temp_vec2));
                        true_types.Add(17, GetTileType(temp_vec4));
                    }
                    else if (t2_t3_same)
                    {
                        true_types.Add(15, GetTileType(temp_vec1));
                        true_types.Add(31, GetTileType(temp_vec3));
                        true_types.Add(6, GetTileType(temp_vec4));
                    }
                    else if (t1_t4_same)
                    {
                        true_types.Add(23, GetTileType(temp_vec1));
                        true_types.Add(15, GetTileType(temp_vec2));
                        true_types.Add(6, GetTileType(temp_vec3));
                    }
                    else if (t2_t4_same)
                    {
                        true_types.Add(15, GetTileType(temp_vec1));
                        true_types.Add(6, GetTileType(temp_vec3));
                        true_types.Add(41, GetTileType(temp_vec4));
                    }
                    else if (t3_t4_same)
                    {
                        true_types.Add(15, GetTileType(temp_vec1));
                        true_types.Add(14, GetTileType(temp_vec2));
                        true_types.Add(42, GetTileType(temp_vec4));
                    }
                    else // none same
                    {
                        true_types.Add(14, GetTileType(temp_vec1));
                        true_types.Add(15, GetTileType(temp_vec2));
                        true_types.Add(6, GetTileType(temp_vec3));
                        true_types.Add(7, GetTileType(temp_vec4));
                    }
                    break;
                case 12:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.East);
                    temp_vec3 = GetPos(pos, Neighbor.West);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(12, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(2, GetTileType(temp_vec1));
                            true_types.Add(8, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(20, GetTileType(temp_vec1));
                            true_types.Add(10, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(1, GetTileType(temp_vec1));
                            true_types.Add(20, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(1, GetTileType(temp_vec1));
                            true_types.Add(10, GetTileType(temp_vec2));
                            true_types.Add(8, GetTileType(temp_vec3));
                        }
                    }
                    break;
                case 13:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.East);
                    temp_vec3 = GetPos(pos, Neighbor.South);
                    temp_vec4 = GetPos(pos, Neighbor.West);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    t1_t4_same = AreTypesSame(temp_vec1, temp_vec4);
                    t2_t4_same = AreTypesSame(temp_vec2, temp_vec4);
                    t3_t4_same = AreTypesSame(temp_vec3, temp_vec4);
                    if (t1_t2_same && t1_t3_same && t1_t4_same) //all same
                    {
                        true_types.Add(13, GetTileType(temp_vec1));
                    }
                    else if (t1_t2_same && t1_t3_same) // t4 not same
                    {
                        true_types.Add(5, GetTileType(temp_vec1));
                        true_types.Add(8, GetTileType(temp_vec4));
                    }
                    else if (t1_t3_same && t1_t4_same) // t2 not same
                    {
                        true_types.Add(3, GetTileType(temp_vec1));
                        true_types.Add(10, GetTileType(temp_vec2));
                    }
                    else if (t1_t2_same && t1_t4_same) // t3 not same
                    {
                        true_types.Add(12, GetTileType(temp_vec1));
                        true_types.Add(17, GetTileType(temp_vec3));
                    }
                    else if (t2_t3_same && t2_t4_same)
                    {
                        true_types.Add(1, GetTileType(temp_vec1));
                        true_types.Add(28, GetTileType(temp_vec4));
                    }
                    else if (t1_t2_same && t3_t4_same)
                    {
                        true_types.Add(2, GetTileType(temp_vec1));
                        true_types.Add(16, GetTileType(temp_vec4));
                    }
                    else if (t1_t3_same && t2_t4_same)
                    {
                        true_types.Add(4, GetTileType(temp_vec1));
                        true_types.Add(20, GetTileType(temp_vec4));
                    }
                    else if (t1_t4_same && t2_t3_same)
                    {
                        true_types.Add(0, GetTileType(temp_vec1));
                        true_types.Add(18, GetTileType(temp_vec2));
                    }
                    else if (t1_t2_same)
                    {
                        true_types.Add(2, GetTileType(temp_vec1));
                        true_types.Add(17, GetTileType(temp_vec3));
                        true_types.Add(8, GetTileType(temp_vec4));
                    }
                    else if (t1_t3_same)
                    {
                        true_types.Add(4, GetTileType(temp_vec1));
                        true_types.Add(10, GetTileType(temp_vec2));
                        true_types.Add(8, GetTileType(temp_vec4));
                    }
                    else if (t2_t3_same)
                    {
                        true_types.Add(1, GetTileType(temp_vec1));
                        true_types.Add(18, GetTileType(temp_vec3));
                        true_types.Add(8, GetTileType(temp_vec4));
                    }
                    else if (t1_t4_same)
                    {
                        true_types.Add(0, GetTileType(temp_vec1));
                        true_types.Add(10, GetTileType(temp_vec2));
                        true_types.Add(17, GetTileType(temp_vec3));
                    }
                    else if (t2_t4_same)
                    {
                        true_types.Add(1, GetTileType(temp_vec1));
                        true_types.Add(17, GetTileType(temp_vec3));
                        true_types.Add(20, GetTileType(temp_vec4));
                    }
                    else if (t3_t4_same)
                    {
                        true_types.Add(1, GetTileType(temp_vec1));
                        true_types.Add(10, GetTileType(temp_vec2));
                        true_types.Add(16, GetTileType(temp_vec4));
                    }
                    else // none same
                    {
                        true_types.Add(1, GetTileType(temp_vec1));
                        true_types.Add(10, GetTileType(temp_vec2));
                        true_types.Add(17, GetTileType(temp_vec3));
                        true_types.Add(8, GetTileType(temp_vec4));
                    }
                    break;
                case 14:
                    temp_vec1 = GetPos(pos, Neighbor.NorthEast);
                    true_types.Add(14, GetTileType(temp_vec1));
                    break;
                case 15:
                    temp_vec1 = GetPos(pos, Neighbor.NorthWest);
                    true_types.Add(15, GetTileType(temp_vec1));
                    break;
                case 16:
                    temp_vec1 = GetPos(pos, Neighbor.West);
                    temp_vec2 = GetPos(pos, Neighbor.South);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(16, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(8, GetTileType(temp_vec1));
                        true_types.Add(17, GetTileType(temp_vec2));
                    }
                    break;
                case 17:
                    temp_vec1 = GetPos(pos, Neighbor.South);
                    true_types.Add(17, GetTileType(temp_vec1));
                    break;
                case 18:
                    temp_vec1 = GetPos(pos, Neighbor.South);
                    temp_vec2 = GetPos(pos, Neighbor.East);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(18, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(17, GetTileType(temp_vec1));
                        true_types.Add(10, GetTileType(temp_vec2));
                    }
                    break;
                case 19:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    true_types.Add(19, GetTileType(temp_vec1));
                    break; // Full tile, no clue which way
                case 20:
                    temp_vec1 = GetPos(pos, Neighbor.West);
                    temp_vec2 = GetPos(pos, Neighbor.East);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(20, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(8, GetTileType(temp_vec1));
                        true_types.Add(10, GetTileType(temp_vec2));
                    }
                    break;
                case 21:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.West);
                    temp_vec3 = GetPos(pos, Neighbor.SouthEast);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(21, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(0, GetTileType(temp_vec1));
                            true_types.Add(6, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(24, GetTileType(temp_vec1));
                            true_types.Add(8, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(1, GetTileType(temp_vec1));
                            true_types.Add(26, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(1, GetTileType(temp_vec1));
                            true_types.Add(8, GetTileType(temp_vec2));
                            true_types.Add(6, GetTileType(temp_vec3));
                        }
                    }
                    break;
                case 22:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.East);
                    temp_vec3 = GetPos(pos, Neighbor.SouthWest);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(22, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(2, GetTileType(temp_vec1));
                            true_types.Add(7, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(25, GetTileType(temp_vec1));
                            true_types.Add(10, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(1, GetTileType(temp_vec1));
                            true_types.Add(27, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(1, GetTileType(temp_vec1));
                            true_types.Add(10, GetTileType(temp_vec2));
                            true_types.Add(7, GetTileType(temp_vec3));
                        }
                    }
                    break;
                case 23:
                    temp_vec1 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec2 = GetPos(pos, Neighbor.SouthWest);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(23, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(14, GetTileType(temp_vec1));
                        true_types.Add(7, GetTileType(temp_vec2));
                    }
                    break;
                case 24:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.SouthEast);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(24, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(1, GetTileType(temp_vec1));
                        true_types.Add(6, GetTileType(temp_vec2));
                    }
                    break;
                case 25:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.SouthWest);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(25, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(1, GetTileType(temp_vec1));
                        true_types.Add(7, GetTileType(temp_vec2));
                    }
                    break;
                case 26:
                    temp_vec1 = GetPos(pos, Neighbor.West);
                    temp_vec2 = GetPos(pos, Neighbor.SouthEast);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(26, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(8, GetTileType(temp_vec1));
                        true_types.Add(6, GetTileType(temp_vec2));
                    }
                    break;
                case 27:
                    temp_vec1 = GetPos(pos, Neighbor.East);
                    temp_vec2 = GetPos(pos, Neighbor.SouthWest);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(27, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(10, GetTileType(temp_vec1));
                        true_types.Add(7, GetTileType(temp_vec2));
                    }
                    break;
                case 28:
                    temp_vec1 = GetPos(pos, Neighbor.East);
                    temp_vec2 = GetPos(pos, Neighbor.South);
                    temp_vec3 = GetPos(pos, Neighbor.West);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(28, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(18, GetTileType(temp_vec1));
                            true_types.Add(8, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(20, GetTileType(temp_vec1));
                            true_types.Add(32, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(10, GetTileType(temp_vec1));
                            true_types.Add(16, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(10, GetTileType(temp_vec1));
                            true_types.Add(17, GetTileType(temp_vec2));
                            true_types.Add(8, GetTileType(temp_vec3));
                        }
                    }
                    break;
                case 29:
                    temp_vec1 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec2 = GetPos(pos, Neighbor.South);
                    temp_vec3 = GetPos(pos, Neighbor.West);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(29, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(32, GetTileType(temp_vec1));
                            true_types.Add(8, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(34, GetTileType(temp_vec1));
                            true_types.Add(17, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(14, GetTileType(temp_vec1));
                            true_types.Add(16, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(14, GetTileType(temp_vec1));
                            true_types.Add(17, GetTileType(temp_vec2));
                            true_types.Add(8, GetTileType(temp_vec3));
                        }
                    }
                    break;
                case 30:
                    temp_vec1 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec2 = GetPos(pos, Neighbor.South);
                    temp_vec3 = GetPos(pos, Neighbor.East);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(30, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(33, GetTileType(temp_vec1));
                            true_types.Add(10, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(35, GetTileType(temp_vec1));
                            true_types.Add(17, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(15, GetTileType(temp_vec1));
                            true_types.Add(18, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(15, GetTileType(temp_vec1));
                            true_types.Add(17, GetTileType(temp_vec2));
                            true_types.Add(10, GetTileType(temp_vec3));
                        }
                    }
                    break;
                case 31:
                    temp_vec1 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec2 = GetPos(pos, Neighbor.SouthEast);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(31, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(15, GetTileType(temp_vec1));
                        true_types.Add(6, GetTileType(temp_vec2));
                    }
                    break;
                case 32:
                    temp_vec1 = GetPos(pos, Neighbor.South);
                    temp_vec2 = GetPos(pos, Neighbor.NorthEast);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(32, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(17, GetTileType(temp_vec1));
                        true_types.Add(14, GetTileType(temp_vec2));
                    }
                    break;
                case 33:
                    temp_vec1 = GetPos(pos, Neighbor.South);
                    temp_vec2 = GetPos(pos, Neighbor.NorthWest);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(33, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(17, GetTileType(temp_vec1));
                        true_types.Add(15, GetTileType(temp_vec2));
                    }
                    break;
                case 34:
                    temp_vec1 = GetPos(pos, Neighbor.West);
                    temp_vec2 = GetPos(pos, Neighbor.NorthEast);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(34, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(8, GetTileType(temp_vec1));
                        true_types.Add(14, GetTileType(temp_vec2));
                    }
                    break;
                case 35:
                    temp_vec1 = GetPos(pos, Neighbor.East);
                    temp_vec2 = GetPos(pos, Neighbor.NorthWest);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(35, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(10, GetTileType(temp_vec1));
                        true_types.Add(15, GetTileType(temp_vec2));
                    }
                    break;
                case 36:
                    temp_vec1 = GetPos(pos, Neighbor.West);
                    temp_vec2 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec3 = GetPos(pos, Neighbor.SouthEast);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(36, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(33, GetTileType(temp_vec1));
                            true_types.Add(6, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(26, GetTileType(temp_vec1));
                            true_types.Add(14, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(8, GetTileType(temp_vec1));
                            true_types.Add(41, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(8, GetTileType(temp_vec1));
                            true_types.Add(14, GetTileType(temp_vec2));
                            true_types.Add(6, GetTileType(temp_vec3));
                        }
                    }
                    break;
                case 37:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.SouthWest);
                    temp_vec3 = GetPos(pos, Neighbor.SouthEast);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(37, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(25, GetTileType(temp_vec1));
                            true_types.Add(6, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(24, GetTileType(temp_vec1));
                            true_types.Add(7, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(1, GetTileType(temp_vec1));
                            true_types.Add(42, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(1, GetTileType(temp_vec1));
                            true_types.Add(7, GetTileType(temp_vec2));
                            true_types.Add(6, GetTileType(temp_vec3));
                        }
                    }
                    break;
                case 38:
                    temp_vec1 = GetPos(pos, Neighbor.South);
                    temp_vec2 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec3 = GetPos(pos, Neighbor.NorthEast);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(38, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(33, GetTileType(temp_vec1));
                            true_types.Add(14, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(32, GetTileType(temp_vec1));
                            true_types.Add(15, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(17, GetTileType(temp_vec1));
                            true_types.Add(43, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(17, GetTileType(temp_vec1));
                            true_types.Add(15, GetTileType(temp_vec2));
                            true_types.Add(14, GetTileType(temp_vec3));
                        }
                    }
                    break;
                case 39:
                    temp_vec1 = GetPos(pos, Neighbor.East);
                    temp_vec2 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec3 = GetPos(pos, Neighbor.SouthWest);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(39, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(35, GetTileType(temp_vec1));
                            true_types.Add(7, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(27, GetTileType(temp_vec1));
                            true_types.Add(15, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(10, GetTileType(temp_vec1));
                            true_types.Add(39, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(10, GetTileType(temp_vec1));
                            true_types.Add(15, GetTileType(temp_vec2));
                            true_types.Add(7, GetTileType(temp_vec3));
                        }
                    }
                    break;
                case 40:
                    temp_vec1 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec2 = GetPos(pos, Neighbor.SouthWest);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(40, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(15, GetTileType(temp_vec1));
                        true_types.Add(7, GetTileType(temp_vec2));
                    }
                    break;
                case 41:
                    temp_vec1 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec2 = GetPos(pos, Neighbor.SouthEast);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(41, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(14, GetTileType(temp_vec1));
                        true_types.Add(6, GetTileType(temp_vec2));
                    }
                    break;
                case 42:
                    temp_vec1 = GetPos(pos, Neighbor.SouthWest);
                    temp_vec2 = GetPos(pos, Neighbor.SouthEast);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(42, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(7, GetTileType(temp_vec1));
                        true_types.Add(6, GetTileType(temp_vec2));
                    }
                    break;
                case 43:
                    temp_vec1 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec2 = GetPos(pos, Neighbor.NorthWest);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(43, GetTileType(temp_vec1));
                    }
                    else
                    {
                        true_types.Add(14, GetTileType(temp_vec1));
                        true_types.Add(15, GetTileType(temp_vec2));
                    }
                    break;
                case 44:
                    temp_vec1 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec2 = GetPos(pos, Neighbor.SouthWest);
                    temp_vec3 = GetPos(pos, Neighbor.SouthEast);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(44, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(40, GetTileType(temp_vec1));
                            true_types.Add(6, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(31, GetTileType(temp_vec1));
                            true_types.Add(7, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(15, GetTileType(temp_vec1));
                            true_types.Add(42, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(15, GetTileType(temp_vec1));
                            true_types.Add(7, GetTileType(temp_vec2));
                            true_types.Add(6, GetTileType(temp_vec3));
                        }
                    }
                    break;
                case 45:
                    temp_vec1 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec2 = GetPos(pos, Neighbor.SouthWest);
                    temp_vec3 = GetPos(pos, Neighbor.SouthEast);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(45, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(23, GetTileType(temp_vec1));
                            true_types.Add(6, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(41, GetTileType(temp_vec1));
                            true_types.Add(7, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(14, GetTileType(temp_vec1));
                            true_types.Add(42, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(14, GetTileType(temp_vec1));
                            true_types.Add(7, GetTileType(temp_vec2));
                            true_types.Add(6, GetTileType(temp_vec3));
                        }
                    }
                    break;
                case 46:
                    temp_vec1 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec2 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec3 = GetPos(pos, Neighbor.SouthWest);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(46, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(43, GetTileType(temp_vec1));
                            true_types.Add(7, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(40, GetTileType(temp_vec1));
                            true_types.Add(6, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(15, GetTileType(temp_vec1));
                            true_types.Add(23, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(15, GetTileType(temp_vec1));
                            true_types.Add(14, GetTileType(temp_vec2));
                            true_types.Add(7, GetTileType(temp_vec3));
                        }
                    }
                    break;
                case 47:
                    temp_vec1 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec2 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec3 = GetPos(pos, Neighbor.SouthEast);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(47, GetTileType(temp_vec1));
                        }
                        else
                        {
                            true_types.Add(43, GetTileType(temp_vec1));
                            true_types.Add(6, GetTileType(temp_vec3));
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(31, GetTileType(temp_vec1));
                            true_types.Add(14, GetTileType(temp_vec2));
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(15, GetTileType(temp_vec1));
                            true_types.Add(41, GetTileType(temp_vec2));
                        }
                        else
                        {
                            true_types.Add(15, GetTileType(temp_vec1));
                            true_types.Add(14, GetTileType(temp_vec2));
                            true_types.Add(6, GetTileType(temp_vec3));
                        }
                    }
                    break;
                default:
                    Debug.Log("Tile Logic Fell Through");
                    break;
            }

            return true_types;
        }

    }

    internal IEnumerable<KeyValuePair<int, StrategicTileType>> GetSurroundingLiquid(int direction, StrategicTileType type, Vec2 pos) 
    {
        Dictionary<int, StrategicTileType> overlay_tiles = new Dictionary<int, StrategicTileType>();

        StrategicTileType north = StrategicTileType.grass;
        StrategicTileType south = StrategicTileType.grass;
        StrategicTileType east = StrategicTileType.grass;
        StrategicTileType west = StrategicTileType.grass;

        List<int> not_from_north = new List<int> {0,1,2,3,4,5,12,13,19,21,22,24,25,37};
        List<int> not_from_east = new List<int> {2,5,10,12,13,19,20,27,28,30,35,39};
        List<int> not_from_south = new List<int> {3,4,5,13,16,17,18,19,22,28,29,30,32,33,38};
        List<int> not_from_west = new List<int> {0,3,8,12,13,16,18,19,20,21,26,28,29,34,36};

        if (!not_from_north.Contains(direction))
            north = GetTileType(GetPos(pos, Neighbor.North));
        if (!not_from_east.Contains(direction))
            east = GetTileType(GetPos(pos, Neighbor.East));
        if (!not_from_south.Contains(direction))
            south = GetTileType(GetPos(pos, Neighbor.South));
        if (!not_from_west.Contains(direction))
            west = GetTileType(GetPos(pos, Neighbor.West));

        switch (direction)
        { 
            case 0:
                overlay_tiles.Add(0,south);
                overlay_tiles.Add(1,east);
                break;
            case 1:
                overlay_tiles.Add(2, west);
                overlay_tiles.Add(3, south);
                overlay_tiles.Add(4, east);
                break;
            case 2:
                overlay_tiles.Add(5, south);
                overlay_tiles.Add(6, west);
                break;
            case 3:
                overlay_tiles.Add(7, east);
                break;
            case 4:
                overlay_tiles.Add(8, west);
                overlay_tiles.Add(9, east);
                break;
            case 5:
                overlay_tiles.Add(10, west);
                break;
            case 6:
                overlay_tiles.Add(11, north);
                overlay_tiles.Add(12, east);
                overlay_tiles.Add(13, west);
                overlay_tiles.Add(14, south);
                break;
            case 7:
                overlay_tiles.Add(11, north);
                overlay_tiles.Add(15, west);
                overlay_tiles.Add(16, east);
                overlay_tiles.Add(17, south);
                break;
            case 8:
                overlay_tiles.Add(16, east);
                overlay_tiles.Add(18, north);
                overlay_tiles.Add(19, south);
                break;
            case 9:
                overlay_tiles.Add(3, south);
                overlay_tiles.Add(11, north);
                overlay_tiles.Add(13, west);
                overlay_tiles.Add(16, east);
                break;
            case 10:
                overlay_tiles.Add(13, west);
                overlay_tiles.Add(20, north);
                overlay_tiles.Add(21, south);
                break;
            case 11:
                overlay_tiles.Add(22, north);
                overlay_tiles.Add(23, east);
                overlay_tiles.Add(24, west);
                overlay_tiles.Add(25, south);
                break;
            case 12:
                overlay_tiles.Add(26, south);
                break;
            case 13:
                break;
            case 14:
                overlay_tiles.Add(3, south);
                overlay_tiles.Add(13, west);
                overlay_tiles.Add(27, north);
                overlay_tiles.Add(28, east);
                break;
            case 15:
                overlay_tiles.Add(3, south);
                overlay_tiles.Add(16, east);
                overlay_tiles.Add(29, north);
                overlay_tiles.Add(30, west);
                break;
            case 16:
                overlay_tiles.Add(31, north);
                overlay_tiles.Add(32, east);
                break;
            case 17:
                overlay_tiles.Add(11, north);
                overlay_tiles.Add(33, west);
                overlay_tiles.Add(34, east);
                break;
            case 18:
                overlay_tiles.Add(35, north);
                overlay_tiles.Add(36, west);
                break;
            case 19:
                break;
            case 20:
                overlay_tiles.Add(37, north);
                overlay_tiles.Add(38, south);
                break;
            case 21:
                overlay_tiles.Add(39, south);
                overlay_tiles.Add(40, east);
                break;
            case 22:
                overlay_tiles.Add(41, west);
                overlay_tiles.Add(42, south);
                break;
            case 23:
                overlay_tiles.Add(43, west);
                overlay_tiles.Add(44, north);
                overlay_tiles.Add(45, east);
                overlay_tiles.Add(46, south);
                break;
            case 24:
                overlay_tiles.Add(47, south);
                overlay_tiles.Add(48, east);
                overlay_tiles.Add(49, west);
                break;
            case 25:
                overlay_tiles.Add(50, west);
                overlay_tiles.Add(51, south);
                overlay_tiles.Add(52, east);
                break;
            case 26:
                overlay_tiles.Add(53, north);
                overlay_tiles.Add(54, east);
                overlay_tiles.Add(55, south);
                break;
            case 27:
                overlay_tiles.Add(56, west);
                overlay_tiles.Add(57, south);
                overlay_tiles.Add(58, north);
                break;
            case 28:
                overlay_tiles.Add(59, north);
                break;
            case 29:
                overlay_tiles.Add(60, north);
                overlay_tiles.Add(61, east);
                break;
            case 30:
                overlay_tiles.Add(62, west);
                overlay_tiles.Add(63, north);
                break;
            case 31:
                overlay_tiles.Add(64, north);
                overlay_tiles.Add(65, west);
                overlay_tiles.Add(66, south);
                overlay_tiles.Add(67, east);
                break;
            case 32:
                overlay_tiles.Add(68, east);
                overlay_tiles.Add(69, north);
                overlay_tiles.Add(70, west);
                break;
            case 33:
                overlay_tiles.Add(71, east);
                overlay_tiles.Add(72, north);
                overlay_tiles.Add(73, west);
                break;
            case 34:
                overlay_tiles.Add(74, north);
                overlay_tiles.Add(75, east);
                overlay_tiles.Add(76, south);
                break;
            case 35:
                overlay_tiles.Add(77, north);
                overlay_tiles.Add(78, west);
                overlay_tiles.Add(79, south);
                break;
            case 36:
                overlay_tiles.Add(80, north);
                overlay_tiles.Add(81, east);
                overlay_tiles.Add(82, south);
                break;
            case 37:
                overlay_tiles.Add(83, west);
                overlay_tiles.Add(84, south);
                overlay_tiles.Add(85, east);
                break;
            case 38:
                overlay_tiles.Add(86, north);
                overlay_tiles.Add(87, west);
                overlay_tiles.Add(88, east);
                break;
            case 39:
                overlay_tiles.Add(89, north);
                overlay_tiles.Add(90, west);
                overlay_tiles.Add(91, south);
                break;
            case 40:
                overlay_tiles.Add(16, east);
                overlay_tiles.Add(92, north);
                overlay_tiles.Add(93, west);
                overlay_tiles.Add(94, south);
                break;
            case 41:
                overlay_tiles.Add(13, west);
                overlay_tiles.Add(95, north);
                overlay_tiles.Add(96, south);
                overlay_tiles.Add(97, east);
                break;
            case 42:
                overlay_tiles.Add(11, north);
                overlay_tiles.Add(98, west);
                overlay_tiles.Add(99, east);
                overlay_tiles.Add(100, south);
                break;
            case 43:
                overlay_tiles.Add(3, south);
                overlay_tiles.Add(101, north);
                overlay_tiles.Add(102, west);
                overlay_tiles.Add(103, east);
                break;
            case 44:
                overlay_tiles.Add(104, north);
                overlay_tiles.Add(105, east);
                overlay_tiles.Add(106, west);
                overlay_tiles.Add(107, south);
                break;
            case 45:
                overlay_tiles.Add(108, north);
                overlay_tiles.Add(109, west);
                overlay_tiles.Add(110, south);
                overlay_tiles.Add(111, east);
                break;
            case 46:
                overlay_tiles.Add(112, north);
                overlay_tiles.Add(113, east);
                overlay_tiles.Add(114, south);
                overlay_tiles.Add(115, west);
                break;
            case 47:
                overlay_tiles.Add(116, north);
                overlay_tiles.Add(117, west);
                overlay_tiles.Add(118, south);
                overlay_tiles.Add(119, east);
                break;
            default: 
                break;
        }


        List<int> can_have_NW_corner = new List<int> {6,7,9,10,14,17,18,23,27,32,41,42,45};
        List<int> can_have_NE_corner = new List<int> {6,7,8,9,15,16,17,26,31,33,40,42,44};
        List<int> can_have_SW_corner = new List<int> {1,2,6,9,10,14,15,24,31,35,41,43,47};
        List<int> can_have_SE_corner = new List<int> {0,1,7,8,9,14,15,23,25,34,40,43,46};

        if (pos.x > 1 && pos.y < State.World.Tiles.GetUpperBound(1) - 1 && can_have_NW_corner.Contains(direction))
            if(type != GetTileType(GetPos(pos, Neighbor.NorthWest)))
                overlay_tiles.Add(120, GetTileType(GetPos(pos, Neighbor.NorthWest)));
        if(pos.x < State.World.Tiles.GetUpperBound(0) - 1 && pos.y < State.World.Tiles.GetUpperBound(1) - 1 && can_have_NE_corner.Contains(direction))
            if (type != GetTileType(GetPos(pos, Neighbor.NorthEast)))
                overlay_tiles.Add(122, GetTileType(GetPos(pos, Neighbor.NorthEast)));
        if(pos.x > 1 && pos.y > 1 && can_have_SW_corner.Contains(direction))
            if (type != GetTileType(GetPos(pos, Neighbor.SouthWest)))
                overlay_tiles.Add(121, GetTileType(GetPos(pos, Neighbor.SouthWest)));
        if(pos.x < State.World.Tiles.GetUpperBound(0) - 1 && pos.y > 1 && can_have_SE_corner.Contains(direction))
            if (type != GetTileType(GetPos(pos, Neighbor.SouthEast)))
                overlay_tiles.Add(123, GetTileType(GetPos(pos, Neighbor.SouthEast)));

        var temp_Dict = from entry in overlay_tiles orderby entry.Value ascending select entry;
        IEnumerable<KeyValuePair<int, StrategicTileType>> fixed_tiles = temp_Dict;

        return fixed_tiles;
    }
}

