using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public static class TacticalPathfinder
{


    internal static List<PathNode> GetPath(Vec2i origin, Vec2i destination, int howClose, Actor_Unit actor, int maxDistance = 999)
    {
        if (origin.GetNumberOfMovesDistance(destination) > maxDistance) //Can't possibly get it in under this distance
            return null;
        bool flight = actor.Unit.HasTrait(Traits.Flight);
        int flightMP = actor.Movement;
        bool goodPath = false;
        PathNode current = null;
        var start = new PathNode { X = origin.x, Y = origin.y };
        var target = new PathNode { X = destination.x, Y = destination.y };
        var openList = new List<PathNode>();
        var closedList = new List<PathNode>();
        int g = 0;

        // start by adding the original position to the open list
        openList.Add(start);

        while (openList.Count > 0)
        {
            // get the square with the lowest F score
            var lowest = openList.Min(l => l.F);
            current = openList.OrderBy(l => TotalManhattan(l.X, l.Y, target.X, target.Y)).First(l => l.F == lowest); //The total manhattan is to have it favor normal looking paths

            if (flight && current.G == flightMP && CheckTile(current.X, current.Y, actor) == false)
            {
                openList.Remove(current);
                continue;
            }

            if (TacticalUtilities.IsUnitControlledByPlayer(actor.Unit) == false && (actor.Unit.HasTrait(Traits.Blitz) || actor.Unit.HasTrait(Traits.SpectralStep) || actor.Unit.HasTrait(Traits.PassThrough)) && current.G != target.G && CheckTile(current.X, current.Y, actor) == false)
            {
                openList.Remove(current);
//                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"WORK darn it!"); //Testing line
                continue;
            }

            if (flight == false || CheckTile(current.X, current.Y, actor)) //Can't stop on a occupied tile, short circuited to save slightly on speed
            {
                if (howClose != -1)
                {
                    // In tactical we are looking for a specific distance from target
                    if (Mathf.Abs(current.X - target.X) <= howClose && Mathf.Abs(current.Y - target.Y) <= howClose && (current.X != origin.x || current.Y != origin.y))
                    {
                        goodPath = true;
                        break;
                    }
                }
                else
                {
                    // If you made it to melee range, stop, otherwise keep going to find the closest point later
                    if (Mathf.Abs(current.X - target.X) <= 1 && Mathf.Abs(current.Y - target.Y) <= 1)
                    {
                        goodPath = true;
                        break;
                    }
                }
            }



            // add the current square to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);
            List<PathNode> adjacentSquares;
            if (flight)
                adjacentSquares = GetFlyableAdjacentSquares(current.X, current.Y);
            else
                adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, actor);
            g = current.G + 1;
            if (g > maxDistance)
                continue;

            foreach (var adjacentSquare in adjacentSquares)
            {
                int additionalCost = flight ? 0 : (TacticalTileInfo.TileCost(new Vec2(adjacentSquare.X, adjacentSquare.Y)) - 1);
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X && l.Y == adjacentSquare.Y) != null)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.X == adjacentSquare.X && l.Y == adjacentSquare.Y) == null)
                {
                    // compute its score, set the parent
                    adjacentSquare.G = g + additionalCost;
                    adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                    adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                    adjacentSquare.Parent = current;

                    // and add it to the open list
                    openList.Insert(0, adjacentSquare);
                }

            }
        }

        if (howClose == -1 && goodPath == false) //Get the closest possible path
        {
            for (int i = 0; i < 50; i++)
            {
                if (closedList.Count == 0)
                    break;
                current = closedList.OrderBy(s => s.H).FirstOrDefault();
                if (flight)
                {
                    if (Mathf.Abs(current.X - target.X) > flightMP && Mathf.Abs(current.Y - target.Y) > flightMP)
                    {
                        closedList.Remove(current);
                        continue;
                    }

                }
                if (CheckTile(current.X, current.Y, actor))
                {
                    goodPath = true;
                    break;
                }
                else
                    closedList.Remove(current);
            }


        }

        if (goodPath == false)
            return null;

        List<PathNode> path = new List<PathNode>();

        while (current != null)
        {
            path.Add(current);
            current = current.Parent;
        }

        path.Reverse();

        path.RemoveAt(0);//Get rid of the node where it is currently located

        return path;

    }

    internal static List<PathNode> GetPathToY(Vec2i origin, bool flight, int y, Actor_Unit actor)
    {

        bool goodPath = false;
        PathNode current = null;
        var start = new PathNode { X = origin.x, Y = origin.y };
        //var target = new PathNode { X = destination.x, Y = destination.y };
        var openList = new List<PathNode>();
        var closedList = new List<PathNode>();
        int g = 0;

        // start by adding the original position to the open list
        openList.Add(start);

        while (openList.Count > 0)
        {
            // get the square with the lowest F score
            var lowest = openList.Min(l => l.F);
            current = openList.OrderBy(l => TotalManhattan(l.X, l.Y, l.X, y)).First(l => l.F == lowest); //The total manhattan is to have it favor normal looking paths


            if (current.Y == y && CheckTile(current.X, current.Y, actor))
            {
                goodPath = true;
                break;
            }

            // add the current square to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);
            List<PathNode> adjacentSquares;
            if (flight)
                adjacentSquares = GetFlyableAdjacentSquares(current.X, current.Y);
            else
                adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, actor);
            g = current.G + 1;

            foreach (var adjacentSquare in adjacentSquares)
            {
                int additionalCost = flight ? 0 : (TacticalTileInfo.TileCost(new Vec2(adjacentSquare.X, adjacentSquare.Y)) - 1);
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X && l.Y == adjacentSquare.Y) != null)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.X == adjacentSquare.X && l.Y == adjacentSquare.Y) == null)
                {
                    // compute its score, set the parent
                    adjacentSquare.G = g + additionalCost;
                    adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, adjacentSquare.X, y);
                    adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                    adjacentSquare.Parent = current;

                    // and add it to the open list
                    openList.Insert(0, adjacentSquare);
                }

            }
        }

        if (goodPath == false) //Get the closest possible path
        {
            current = closedList.OrderBy(s => s.H).FirstOrDefault();
            goodPath = true;
        }

        if (goodPath == false)
            return null;

        List<PathNode> path = new List<PathNode>();

        while (current != null)
        {
            path.Add(current);
            current = current.Parent;
        }

        path.Reverse();

        path.RemoveAt(0);//Get rid of the node where it is currently located

        return path;

    }

    internal static bool[,] GetGrid(Vec2i origin, bool flight, int moveDistance, Actor_Unit actor)
    {
        bool[,] walkable = new bool[Config.TacticalSizeX, Config.TacticalSizeY];
        PathNode current = null;
        var start = new PathNode { X = origin.x, Y = origin.y };
        var openList = new List<PathNode>();
        var closedList = new List<PathNode>();
        int g = 0;

        // start by adding the original position to the open list
        openList.Add(start);

        while (openList.Count > 0)
        {

            var lowest = openList.Min(l => l.G);
            current = openList.First(l => l.G == lowest); //The total manhattan is to have it favor normal looking paths

            // add the current square to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);
            List<PathNode> adjacentSquares;
            if (flight)
                adjacentSquares = GetFlyableAdjacentSquares(current.X, current.Y);
            else
                adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, actor);
            g = current.G + 1;
            walkable[current.X, current.Y] = true;

            if (g >= moveDistance)
                continue;

            foreach (var adjacentSquare in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X && l.Y == adjacentSquare.Y) != null)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.X == adjacentSquare.X && l.Y == adjacentSquare.Y) == null)
                {
                    // compute its score, set the parent

                    adjacentSquare.G = g;

                    adjacentSquare.F = adjacentSquare.G;
                    adjacentSquare.Parent = current;

                    // and add it to the open list
                    if (adjacentSquare.G <= moveDistance)
                        openList.Insert(0, adjacentSquare);
                }

            }
        }

        return walkable;

    }

    static List<PathNode> GetWalkableAdjacentSquares(int x, int y, Actor_Unit actor)
    {
        var proposedLocations = new List<PathNode>()
            {
                new PathNode { X = x, Y = y - 1 },
                new PathNode { X = x, Y = y + 1 },
                new PathNode { X = x - 1, Y = y },
                new PathNode { X = x + 1, Y = y },
                new PathNode { X = x + 1, Y = y + 1 },
                new PathNode { X = x + 1, Y = y - 1 },
                new PathNode { X = x - 1, Y = y + 1 },
                new PathNode { X = x - 1, Y = y - 1 },
            };

        return proposedLocations.Where(l => CheckPassableTile(l.X, l.Y, actor) == true).ToList();
    }

    static List<PathNode> GetFlyableAdjacentSquares(int x, int y)
    {
        var proposedLocations = new List<PathNode>()
            {
                new PathNode { X = x, Y = y - 1 },
                new PathNode { X = x, Y = y + 1 },
                new PathNode { X = x - 1, Y = y },
                new PathNode { X = x + 1, Y = y },
                new PathNode { X = x + 1, Y = y + 1 },
                new PathNode { X = x + 1, Y = y - 1 },
                new PathNode { X = x - 1, Y = y + 1 },
                new PathNode { X = x - 1, Y = y - 1 },
            };

        return proposedLocations.Where(l => CheckTileFlight(l.X, l.Y) == true).ToList();
    }

    static int ComputeHScore(int x, int y, int targetX, int targetY)
    {
        int dx = Mathf.Abs(x - targetX);
        int dy = Mathf.Abs(y - targetY);
        return Mathf.Max(dx, dy);
    }

    static int TotalManhattan(int x, int y, int targetX, int targetY)
    {
        int dx = Mathf.Abs(x - targetX);
        int dy = Mathf.Abs(y - targetY);
        return (dx + dy);
    }

    static bool CheckTile(int x, int y, Actor_Unit actor)
    {
        if (TacticalUtilities.OpenTile(x, y, actor))
            return true;
        return false;
    }

    static bool CheckTileFlight(int x, int y)
    {
        if (TacticalUtilities.FlyableTile(x, y))
            return true;
        return false;
    }

    static bool CheckPassableTile(int x, int y, Actor_Unit actor)
    {
        if ((TacticalUtilities.OpenTile(x, y, actor)) || ((TacticalUtilities.PassableOpenTile(x, y, actor)) && (actor.Unit.HasTrait(Traits.PassThrough) || actor.Unit.HasTrait(Traits.Blitz) || actor.Unit.HasTrait(Traits.SpectralStep))))
            return true;
        return false;
    }

}
