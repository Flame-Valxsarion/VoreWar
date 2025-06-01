using System;
using System.Collections.Generic;

public enum StrategicTileType
{
    grass = 0,
    forest = 1,
    mountain = 2,
    water = 3,
    field = 4,
    hills = 5,
    desert = 6,
    ocean = 7,
    snow = 8,
    ice = 9,
    lava = 10,
    volcanic = 11,
    purpleSwamp = 12,
    sandHills = 13,
    snowHills = 14,
    swamp = 15,
    fieldSnow = 16,
    fieldDesert = 17,
    snowTrees = 18,
    snowMountain = 19,
    brokenCliffs = 20,
    ashen = 21,
    fieldAshen = 22,
    ashenHills = 23,
    shallowWater = 24,
    smallIslands = 25,
    fieldSmallIslands = 26,
    savannah = 27,
    fieldsavannah = 28,
    drySwamp = 29,
    rainforest = 30,
}

public enum StrategicDoodadType
{
    none = 0,
    bridgeVertical = 1,
    bridgeHorizontal = 2,
    bridgeIntersection = 3,
    virtualBridgeVertical = 4,
    virtualBridgeHorizontal = 5,
    virtualBridgeIntersection = 6,
    road = 7,
    stoneRoad = 8,
    wall = 9,
    SpawnerVagrant = 1001,
    SpawnerSerpents = 1002,
    SpawnerWyvern = 1003,
    SpawnerCompy = 1004,
    SpawnerSharks = 1005,
    SpawnerFeralWolves = 1006,
    SpawnerCake = 1007,
    SpawnerHarvester = 1008,
    SpawnerVoilin = 1009,
    SpawnerBats = 1010,
    SpawnerFrogs = 1011,
    SpawnerDragon = 1012,
    SpawnerDragonfly = 1013,
    SpawnerTwistedVines = 1014,
    SpawnerFairy = 1015,
    SpawnerAnts = 1016,
    SpawnerGryphon = 1017,
    SpawnerSlugs = 1018,
    SpawnerSalamanders = 1019,
    SpawnerMantis = 1020,
    SpawnerEasternDragon = 1021,
    SpawnerCatfish = 1022,
    SpawnerGazelle = 1023,
    SpawnerEarthworm = 1024,
    SpawnerFeralLizards = 1025,
    SpawnerMonitor = 1026,
    SpawnerSchiwardez = 1027,
    SpawnerTerrorbird = 1028,
    SpawnerDratopyr = 1029,
    SpawnerFeralLions = 1030,
	SpawnerGoodra = 1031,
    SpawnerFeralHorses = 1032,
    SpawnerFeralFox = 1033,
    SpawnerTerminid = 1034,
    SpawnerFeralOrcas = 1035,
    SpawnerBoomBunnies = 1036,
    SpawnerFeralSlime = 1037,
    SpawnerViraeUltimae = 1038,
    SpawnerViisels = 1039,
}

public enum MovementType
{
    Ground,
    Water,
    Air,
}

static class StrategicTileInfo
{
    static int[] grasses = new int[] { 0, 13, 14, 15 };
    static int[] forests = new int[] { 1, 16, 17, 18 };
    static int[] waters = new int[] { 3, 19, 20, 21 };
    static int[] fields = new int[] { 4, 22, 23, 24 };
    static int[] sands = new int[] { 6, 25, 27 };
    static int[] hills = new int[] { 5, 26 };
    static int[] swamps = new int[] { 30, 31, 32 };
    static int[] snowFields = new int[] { 33, 34, 35, 36 };
    static int[] desertFields = new int[] { 37, 38, 39, 40 };
    static int[] ashes = new int[] {41, 48};
    static int[] ashFields = new int[] {42, 43, 44, 45};
    static int[] ashHills = new int[] {46, 47};
    static int[] shallowWaters = new int[] {49, 50};
    static int[] smallIslands = new int[] {51, 52, 53};
    static int[] smallIslandFields = new int[] {54, 55, 56, 57};
    static int[] savannahs = new int[] {58, 59, 60, 61};
    static int[] savannahFields = new int[] {62, 63, 64, 65};
    static int[] drySwamps = new int[] {66, 67, 68};
    static int[] rainforests = new int[] {69, 70, 71};

    static internal List<StrategicTileType> SandFamily = new List<StrategicTileType>() { StrategicTileType.desert, StrategicTileType.fieldDesert, StrategicTileType.sandHills, StrategicTileType.brokenCliffs};
    static internal List<StrategicTileType> GrassFamily = new List<StrategicTileType>() { StrategicTileType.grass, StrategicTileType.forest, StrategicTileType.mountain, StrategicTileType.field, StrategicTileType.hills};
    static internal List<StrategicTileType> SnowFamily = new List<StrategicTileType>() { StrategicTileType.snow, StrategicTileType.snowHills, StrategicTileType.fieldSnow, StrategicTileType.snowTrees, StrategicTileType.snowMountain };
    static internal List<StrategicTileType> WaterFamily = new List<StrategicTileType>() { StrategicTileType.water, StrategicTileType.ocean};
    static internal List<StrategicTileType> AshenFamily = new List<StrategicTileType>() {StrategicTileType.ashen, StrategicTileType.fieldAshen, StrategicTileType.ashenHills};
    static internal List<StrategicTileType> ShallowWaterFamily = new List<StrategicTileType>() {StrategicTileType.smallIslands, StrategicTileType.fieldSmallIslands};
    static internal List<StrategicTileType> SavannahFamily = new List<StrategicTileType>() { StrategicTileType.savannah, StrategicTileType.fieldsavannah};
    static internal List<StrategicTileType> ConsideredLiquid = new List<StrategicTileType>() { StrategicTileType.water, StrategicTileType.ocean, StrategicTileType.ice,StrategicTileType.shallowWater, StrategicTileType.lava};


    static Noise.OpenSimplexNoise OpenSimplexNoise = new Noise.OpenSimplexNoise(155);

    static internal int GetTileType(StrategicTileType type, int x, int y)
    {
        Random rand = new Random((int)(65535 * OpenSimplexNoise.Evaluate(16 * x, 16 * y)));

        switch (type)
        {
            case StrategicTileType.grass:
                return grasses[rand.Next(grasses.Length)];
            case StrategicTileType.forest:
                if (Config.SimpleForests)
                    return forests[rand.Next(2)];
                return forests[rand.Next(forests.Length)];
            case StrategicTileType.water:
                return waters[rand.Next(waters.Length)];
            case StrategicTileType.field:
                if (Config.SimpleFarms)
                    return fields[0];
                return fields[rand.Next(fields.Length)];
            case StrategicTileType.fieldSnow:
                if (Config.SimpleFarms)
                    return snowFields[0];
                return snowFields[rand.Next(snowFields.Length)];
            case StrategicTileType.fieldDesert:
                if (Config.SimpleFarms)
                    return desertFields[0];
                return desertFields[rand.Next(desertFields.Length)];
            case StrategicTileType.hills:
                return hills[rand.Next(hills.Length)];
            case StrategicTileType.desert:
                return sands[rand.Next(sands.Length)];
            case StrategicTileType.swamp:
                return swamps[rand.Next(swamps.Length)];
            case StrategicTileType.sandHills:
                return 28;
            case StrategicTileType.snowHills:
                return 29;
            case StrategicTileType.snowTrees:
                return (int)StrategicTileType.snow;
            case StrategicTileType.snowMountain:
                return (int)StrategicTileType.snow;
            case StrategicTileType.brokenCliffs:
                return (int)StrategicTileType.desert;
            case StrategicTileType.ashen:
                return ashes[rand.Next(ashes.Length)];
            case StrategicTileType.fieldAshen:
                if (Config.SimpleFarms)
                    return ashFields[0];
                return ashFields[rand.Next(ashFields.Length)];
            case StrategicTileType.ashenHills:
                return ashHills[rand.Next(ashHills.Length)];
            case StrategicTileType.shallowWater:
                return shallowWaters[rand.Next(shallowWaters.Length)];
            case StrategicTileType.smallIslands:               
                return smallIslands[rand.Next(smallIslands.Length)];            
            case StrategicTileType.fieldSmallIslands:
                if (Config.SimpleFarms)
                    return 57;
                return smallIslandFields[rand.Next(smallIslandFields.Length)];
            case StrategicTileType.savannah:
                return savannahs[rand.Next(savannahs.Length)];
            case StrategicTileType.fieldsavannah:
                if (Config.SimpleFarms)
                    return 65;
                return savannahFields[rand.Next(savannahFields.Length)];
            case StrategicTileType.drySwamp:
                return drySwamps[rand.Next(drySwamps.Length)];
            case StrategicTileType.rainforest:
                return rainforests[rand.Next(rainforests.Length)];
            default:
                return (int)type;
        }
    }

    static internal int GetObjectTileType(StrategicTileType type, int x, int y)
    {
        Random rand = new Random((int)(65535 * OpenSimplexNoise.Evaluate(16 * x, 16 * y)));

        switch (type)
        {
            case StrategicTileType.forest:
                if (Config.SimpleForests)
                    return 4 + rand.Next(2);
                return 4 + rand.Next(4);
            //case StrategicTileType.field:
            //    if (Config.SimpleFarms)
            //        return fields[0];
            //    return fields[rand.Next(fields.Length)];
            //case StrategicTileType.fieldSnow:
            //    if (Config.SimpleFarms)
            //        return snowFields[0];
            //    return snowFields[rand.Next(snowFields.Length)];
            //case StrategicTileType.fieldDesert:
            //    if (Config.SimpleFarms)
            //        return desertFields[0];
            //    return desertFields[rand.Next(desertFields.Length)];
            case StrategicTileType.field:
                if (Config.SimpleFarms)
                    return 16;
                return 16 + rand.Next(4);
            case StrategicTileType.fieldSnow:
                if (Config.SimpleFarms)
                    return 8;
                return 8 + rand.Next(4);
            case StrategicTileType.fieldDesert:
                if (Config.SimpleFarms)
                    return 12;
                return 12 + rand.Next(4);
            case StrategicTileType.hills:
                return 3 - rand.Next(2);
            case StrategicTileType.sandHills:
                return 1;
            case StrategicTileType.snowHills:
                return 0;
            case StrategicTileType.snowTrees:
                return 20 + rand.Next(2);
            case StrategicTileType.snowMountain:
                return 22;
            case StrategicTileType.brokenCliffs:
                return 23;
            default:
                return -1;
        }
    }

    /// <summary>
    /// Don't call from within Map editor, use the local
    /// </summary>
    static internal bool CanWalkInto(int x, int y)
    {
        if ((x >= 0 && x < Config.StrategicWorldSizeX && y >= 0 && y < Config.StrategicWorldSizeY) == false)
            return false;
        if (CanWalkInto(State.World.Tiles[x, y]) == true)
            return true;
        if (State.World.Doodads != null && State.World.Doodads[x, y] >= StrategicDoodadType.bridgeVertical && State.World.Doodads[x, y] <= StrategicDoodadType.virtualBridgeIntersection)
            return true;
        return false;
    }

    static internal bool CanWalkInto(StrategicTileType type)
    {
        switch (type)
        {
            case StrategicTileType.grass:
            case StrategicTileType.forest:
            case StrategicTileType.field:
            case StrategicTileType.fieldDesert:
            case StrategicTileType.fieldSnow:
            case StrategicTileType.hills:
            case StrategicTileType.desert:
            case StrategicTileType.snow:
            case StrategicTileType.ice:
            case StrategicTileType.volcanic:
            case StrategicTileType.swamp:
            case StrategicTileType.purpleSwamp:
            case StrategicTileType.sandHills:
            case StrategicTileType.snowHills:
            case StrategicTileType.snowTrees:
            case StrategicTileType.ashen:
            case StrategicTileType.fieldAshen:
            case StrategicTileType.ashenHills:
            case StrategicTileType.shallowWater:
            case StrategicTileType.smallIslands:
            case StrategicTileType.fieldSmallIslands:
            case StrategicTileType.savannah:
            case StrategicTileType.fieldsavannah:
            case StrategicTileType.drySwamp:
            case StrategicTileType.rainforest:
                return true;

            //case StrategicTileType.mountain:
            //case StrategicTileType.water:
            //case StrategicTileType.ocean:
            //case StrategicTileType.lava:
            //case StrategicTileType.brokenCliffs:
            //case StrategicTileType.snowMountain:
            //    return false;
            default:
                return false;
        }
    }

    static internal int WalkCost(int x, int y)
    {
        if ((x >= 0 && x < Config.StrategicWorldSizeX && y >= 0 && y < Config.StrategicWorldSizeY) == false)
            return 9999;
        if (State.World.Doodads != null && State.World.Doodads[x, y] >= StrategicDoodadType.bridgeVertical && State.World.Doodads[x, y] <= StrategicDoodadType.virtualBridgeIntersection)
            return 1;
        return WalkCost(State.World.Tiles[x, y]);
    }

    static internal int WalkCost(StrategicTileType type)
    {
        switch (type)
        {
            case StrategicTileType.grass:
            case StrategicTileType.field:
            case StrategicTileType.desert:
            case StrategicTileType.snow:
            case StrategicTileType.volcanic:
            case StrategicTileType.fieldSnow:
            case StrategicTileType.fieldDesert:
            case StrategicTileType.ashen:
            case StrategicTileType.fieldAshen:
            case StrategicTileType.savannah:
            case StrategicTileType.fieldsavannah:
            case StrategicTileType.smallIslands:
            case StrategicTileType.fieldSmallIslands:
            case StrategicTileType.drySwamp:

                return 1;

            case StrategicTileType.forest:
            case StrategicTileType.hills:
            case StrategicTileType.ice:
            case StrategicTileType.swamp:
            case StrategicTileType.purpleSwamp:
            case StrategicTileType.sandHills:
            case StrategicTileType.snowHills:
            case StrategicTileType.snowTrees:
            case StrategicTileType.ashenHills:
            case StrategicTileType.shallowWater:
            case StrategicTileType.rainforest:
                return 2;
            default:
                return 1;
        }
    }

}