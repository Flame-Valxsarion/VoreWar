
public enum TacticalTileType
{
    grass,
    tree1,
    tree2,
    wall,
    house1,
    house2,
    house3,
    CobbleComplete,
    CobbleHorizontal,
    CobbleVertical,
    CobbleIntersection,
    CobbleVerticalLeftEdge,
    CobbleVerticalRightEdge,
    CobbleIntersectionLeftEdge,
    CobbleIntersectionRightEdge,
    WallStart,

    RockOverTar = 80,
    RockOverSand = 81,
    GrassOverWater = 82,

    VolcanicOverGravel = 83,
    VolcanicOverLava = 84,

    BeachOverOcean = 85,

    SwampOverBog = 86,

    greengrass = 100,
    grassBush,
    grassFlower,
    grassMediumRock,
    grassSmallRock,
}

static class TacticalTileInfo
{
    static internal int TileCost(Vec2i location)
    {
        var effects = State.GameManager.TacticalMode.ActiveEffects;
        if (effects != null)
        {
            if (effects.TryGetValue(location, out var effect))
            {
                if (effect.Type == TileEffectType.IcePatch)
                    return 3;
            }
        }
        return 1;
    }

    static internal bool CanWalkInto(TacticalTileType type, Actor_Unit actor)
    {
        if (type >= (TacticalTileType)2300 && type < (TacticalTileType)2399)
            return true;
        if (type >= (TacticalTileType)2000 && type < (TacticalTileType)2099)
            return true;
        if (type >= (TacticalTileType)700 && type < (TacticalTileType)799)
            return true;
        if (type >= (TacticalTileType)600 && type < (TacticalTileType)699)
            return true;
        if (type >= (TacticalTileType)500 && type < (TacticalTileType)599)
            return true;
        if (type >= (TacticalTileType)400 && type < (TacticalTileType)499)
            return true;
        if (type >= (TacticalTileType)300 && type < (TacticalTileType)399)
            return true;
        if (type >= (TacticalTileType)200 && type < (TacticalTileType)299)
            return true;
        if (type >= (TacticalTileType)100 && type < (TacticalTileType)199)
            return true;

        switch (type)
        {
            case TacticalTileType.grass:
                return true;
            case TacticalTileType.tree1:
                return actor?.Unit.HasTrait(Traits.NimbleClimber) ?? false;
            case TacticalTileType.tree2:
                return actor?.Unit.HasTrait(Traits.NimbleClimber) ?? false;
            case TacticalTileType.CobbleComplete:
                return true;
            case TacticalTileType.CobbleVertical:
                return true;
            case TacticalTileType.CobbleHorizontal:
                return true;
            case TacticalTileType.CobbleIntersection:
                return true;
            case TacticalTileType.CobbleVerticalLeftEdge:
                return true;
            case TacticalTileType.CobbleVerticalRightEdge:
                return true;
            case TacticalTileType.CobbleIntersectionLeftEdge:
                return true;
            case TacticalTileType.CobbleIntersectionRightEdge:
                return true;
            case TacticalTileType.grassBush:
            case TacticalTileType.grassFlower:
            case TacticalTileType.grassSmallRock:
                return true;
            default:
                return false;
        }
    }
}
