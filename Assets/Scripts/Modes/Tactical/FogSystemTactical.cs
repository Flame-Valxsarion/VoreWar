using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

class FogSystemTactical
{
    internal bool[,] FoggedTile;
    internal Tilemap FogOfWar;
    internal TileBase FogTile;

    public FogSystemTactical(Tilemap fogOfWar, TileBase fogTile)
    {
        FogTile = fogTile;
        FoggedTile = new bool[Config.TacticalSizeX, Config.TacticalSizeY];
        FogOfWar = fogOfWar;
    }

    internal void UpdateFog(List<Actor_Unit> all, int defenderSide, bool attackersturn, bool AIAttacker, bool AIDefender, int currentturn)
    {
        //FogOfWar.ClearAllTiles();
        if (FoggedTile.GetUpperBound(0) + 1 != Config.TacticalSizeX || FoggedTile.GetUpperBound(1) + 1 != Config.TacticalSizeY)
            FoggedTile = new bool[Config.TacticalSizeX, Config.TacticalSizeY];
        //StrategicTileType[,] tiles = State.World.Tiles;
        for (int i = 0; i <= FoggedTile.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= FoggedTile.GetUpperBound(1); j++)
            {
                if (FoggedTile[i, j] == false)
                {
                    FoggedTile[i, j] = true;
                }
            }
        }

        //Set all as unseen
        foreach (Actor_Unit unit in all)
        {           
            unit.InSight = true;
            int unitSightRange = Config.DefualtTacticalSightRange + unit.Unit.TraitBoosts.SightRangeBoost;
            if (Config.RevealTurn > currentturn) //Keeps all units revealed after the set ammount of turns have passed or if in turbo mode.
            {
                if (unit.PredatorComponent.PreyCount <= 0 && !Config.DayNightCosmetic)
                {
                    unit.InSight = false;
                }
            }           
            if (unit.Targetable)
            {
                if ((AIAttacker && AIDefender) || Config.DayNightCosmetic == true)
                {
                    ClearWithinSTilesOf(unit.Position, unitSightRange); // Shows all units to player for AI only battles
                }
                foreach (var seenUnit in TacticalUtilities.UnitsWithinTiles(unit.Position, unitSightRange).Where(s => TacticalUtilities.TreatAsHostile(unit, s)))
                {
                    seenUnit.InSight = true;
                }
                if (unit.Unit.GetApparentSide() == defenderSide && ((attackersturn != true && State.GameManager.TacticalMode.IsPlayerTurn) || !AIDefender))
                {
                    ClearWithinSTilesOf(unit.Position, unitSightRange);
                }
                if (unit.Unit.GetApparentSide() != defenderSide && ((attackersturn == true && State.GameManager.TacticalMode.IsPlayerTurn) || !AIAttacker))
                {
                    ClearWithinSTilesOf(unit.Position, unitSightRange);
                }
            }

        }

        // Removes fog from tile if it's not supposed to be there.
        for (int i = 0; i <= FoggedTile.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= FoggedTile.GetUpperBound(1); j++)
            {
                if (FoggedTile[i, j])
                    FogOfWar.SetTile(new Vector3Int(i, j, 0), FogTile);
                else
                    FogOfWar.SetTile(new Vector3Int(i, j, 0), null);
            }
            if (all != null)
            {
                if (all != null)
                {
                    foreach (Actor_Unit unit in all)
                    {
                        if (FoggedTile[unit.Position.x, unit.Position.y] && unit.PredatorComponent.PreyCount == 0)
                        {
                            unit.UnitSprite.gameObject.SetActive(false);
                            unit.UnitSprite.FlexibleSquare.gameObject.SetActive(false);
                            unit.Hidden = true;
                        }
                        else if (unit.Targetable == true)
                        {
                            unit.UnitSprite.gameObject.SetActive(true);
                            unit.UnitSprite.FlexibleSquare.gameObject.SetActive(true);
                            unit.Hidden = false;
                        }
                    }
                }
            }
        }
    }

    void ClearWithinSTilesOf(Vec2i pos, int sight = 1)
    {
        for (int x = pos.x - sight; x <= pos.x + sight; x++)
        {
            for (int y = pos.y - sight; y <= pos.y + sight; y++)
            {
                if (x < 0 || y < 0 || x > FoggedTile.GetUpperBound(0) || y > FoggedTile.GetUpperBound(1))
                    continue;
                FoggedTile[x, y] = false;
            }
        }
    }
}
