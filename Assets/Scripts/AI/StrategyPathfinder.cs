using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class StrategyPathfinder
{
    public static bool Initialized = false;


    struct Cell
    {
        public bool FriendlyOccupied;
        public bool EnemyOccupied;
        public bool WalkableSurface;
        public int MovementCost;
        public StrategicTileType TileType;
    }

    struct QueueNode : IComparable<QueueNode>
    {
        public Vec2 Value;
        public int Dist;
        public int Diagonals;

        public QueueNode(Vec2 value, int dist, int diagonals)
        {
            Value = value;
            Dist = dist;
            Diagonals = diagonals;
        }

        public int CompareTo(QueueNode other)
        {
            if (Dist != other.Dist)
            {
                return Dist.CompareTo(other.Dist);
            }
            else if (Diagonals != other.Diagonals)
            {
                return Diagonals.CompareTo(other.Diagonals);
            }
            else
                return Value.CompareTo(other.Value);
        }
    }

    class Vec2Comparer : IEqualityComparer<Vec2>
    {
        public bool Equals(Vec2 a, Vec2 b)
        {
            return a == b;
        }

        public int GetHashCode(Vec2 obj)
        {
            return ((IntegerHash(obj.x)
                    ^ (IntegerHash(obj.y) << 1)) >> 1);
        }

        static int IntegerHash(int a)
        {
            // fmix32 from murmurhash
            uint h = (uint)a;
            h ^= h >> 16;
            h *= 0x85ebca6bU;
            h ^= h >> 13;
            h *= 0xc2b2ae35U;
            h ^= h >> 16;
            return (int)h;
        }
    }

    static bool flyingPath = false;

    struct EndPoint
    {
        internal QueueNode node;
        internal int priority;

        public EndPoint(QueueNode node, int priority)
        {
            this.node = node;
            this.priority = priority;
        }
    }

    private static Cell[,] Grid = null;

    private static List<Vec2> GetNeighbours(Vec2 cell, List<Vec2> neighbours)
    {
        neighbours.Clear();

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                var coord = cell + new Vec2(dx, dy);

                bool notSelf = !(dx == 0 && dy == 0);
                bool connectivity = Math.Abs(dx) + Math.Abs(dy) <= 2;
                bool withinGrid = coord.x >= 0 && coord.y >= 0 && coord.x < Config.StrategicWorldSizeX && coord.y < Config.StrategicWorldSizeY;

                if (notSelf && connectivity && withinGrid)
                {
                    neighbours.Add(coord);
                }
            }
        }

        return neighbours;
    }

    private static List<Vec2> FindPath(Vec2 start, Vec2 end, Army army, int MPPerTurn, int maxDistance)
    {

        Vec2Comparer comparer = new Vec2Comparer();
        var dist = new Dictionary<Vec2, int>(comparer);
        var prev = new Dictionary<Vec2, Vec2?>(comparer);

        var Q = new SortedSet<QueueNode>();

        for (int x = 0; x < Config.StrategicWorldSizeX; x++)
        {
            for (int y = 0; y < Config.StrategicWorldSizeY; y++)
            {
                var coord = new Vec2(x, y);
                if (CanEnter(coord, army) == false)
                {
                    dist[coord] = int.MaxValue;
                }
            }
        }

        dist[start] = 0;
        prev[start] = null;
        Q.Add(new QueueNode(start, 0, 0));

        List<Vec2> neighbours = new List<Vec2>();

        // Search loop
        while (Q.Count > 0)
        {
            var u = Q.Min;
            Q.Remove(Q.Min);

            // Old priority queue value
            if (u.Dist != dist[u.Value])
            {
                continue;
            }

            if (u.Value == end)
            {
                break;
            }

            foreach (var v in GetNeighbours(u.Value, neighbours))
            {
                if (CanEnter(v, army))
                {
                    int walkCost = Grid[v.x, v.y].MovementCost;
                    if (flyingPath)
                        walkCost = 1;
                    if (dist[u.Value] + walkCost > maxDistance)
                        continue;
                    if (Grid[v.x, v.y].EnemyOccupied && end != v) //avoid targets you're not looking for
                        walkCost += 6;
                    int remainingMP = Config.ArmyMP - (dist[u.Value] % MPPerTurn);
                    if (walkCost > remainingMP)
                        walkCost = walkCost + remainingMP;
                    int alt = dist[u.Value] + walkCost;
                    if (dist.TryGetValue(v, out int distInt) == false || alt < dist[v])
                    {
                        dist[v] = alt;
                        bool diagonal = u.Value.x != v.x && u.Value.y != v.y;
                        Q.Add(new QueueNode(v, alt, u.Diagonals + (diagonal ? 1 : 0)));

                        prev[v] = u.Value;
                    }
                }
            }
        }

        // Trace path - if there is one
        var path = new List<Vec2>();

        if (prev.ContainsKey(end))
        {
            Vec2? current = end;

            while (current != null)
            {
                path.Add(current.Value);
                if (prev.ContainsKey(current.Value))
                    current = prev[current.Value];
            }

            path.Reverse();
        }

        return path;
    }

    private static int TurnsToReach(Vec2 start, Vec2 end, Army army, int MPPerTurn)
    {
        Vec2Comparer comparer = new Vec2Comparer();
        var dist = new Dictionary<Vec2, int>(comparer);
        var prev = new Dictionary<Vec2, Vec2?>(comparer);

        var Q = new SortedSet<QueueNode>();

        for (int x = 0; x < Config.StrategicWorldSizeX; x++)
        {
            for (int y = 0; y < Config.StrategicWorldSizeY; y++)
            {
                var coord = new Vec2(x, y);
                if (CanEnter(coord, army) == false)
                {
                    dist[coord] = int.MaxValue;
                }
            }
        }

        dist[start] = 0;
        prev[start] = null;
        Q.Add(new QueueNode(start, 0, 0));

        List<Vec2> neighbours = new List<Vec2>();

        // Search loop
        while (Q.Count > 0)
        {
            var u = Q.Min;
            Q.Remove(Q.Min);

            // Old priority queue value
            if (u.Dist != dist[u.Value])
            {
                continue;
            }

            if (u.Value == end)
            {
                break;
            }

            foreach (var v in GetNeighbours(u.Value, neighbours))
            {
                if (CanEnter(v, army))
                {
                    int walkCost = Grid[v.x, v.y].MovementCost;
                    if (flyingPath)
                        walkCost = 1;
                    int remainingMP = MPPerTurn - (dist[u.Value] % MPPerTurn);
                    if (walkCost > remainingMP)
                        walkCost = walkCost + remainingMP;
                    int alt = dist[u.Value] + walkCost;
                    if (dist.TryGetValue(v, out int distInt) == false || alt < dist[v])
                    {
                        dist[v] = alt;
                        bool diagonal = u.Value.x != v.x && u.Value.y != v.y;
                        Q.Add(new QueueNode(v, alt, u.Diagonals + (diagonal ? 1 : 0)));

                        prev[v] = u.Value;
                    }
                }
            }
        }
        if (prev.ContainsKey(end))
        {
            return (dist[end] + 2) / MPPerTurn;
        }
        return 9999;
    }

    private static List<Vec2> FindPathMany(Vec2 start, Vec2[] ends, Army army, int MPPerTurn, int[] priorities, int maxDistance)
    {
        List<EndPoint> endNodes = new List<EndPoint>();
        Vec2Comparer comparer = new Vec2Comparer();
        var dist = new Dictionary<Vec2, int>(comparer);
        var prev = new Dictionary<Vec2, Vec2?>(comparer);

        var Q = new SortedSet<QueueNode>();

        for (int x = 0; x < Config.StrategicWorldSizeX; x++)
        {
            for (int y = 0; y < Config.StrategicWorldSizeY; y++)
            {
                var coord = new Vec2(x, y);
                if (CanEnter(coord, army) == false)
                {
                    dist[coord] = int.MaxValue;
                }
            }
        }

        dist[start] = 0;
        prev[start] = null;
        Q.Add(new QueueNode(start, 0, 0));

        List<Vec2> neighbours = new List<Vec2>();

        // Search loop
        while (Q.Count > 0)
        {
            var u = Q.Min;
            Q.Remove(Q.Min);

            // Old priority queue value
            if (u.Dist != dist[u.Value])
            {
                continue;
            }

            if (ends.Any(s => s == u.Value))
            {
                if (priorities != null)
                    endNodes.Add(new EndPoint(u, dist[u.Value] - priorities[Array.IndexOf(ends, u.Value)]));
                else
                    endNodes.Add(new EndPoint(u, dist[u.Value]));
                if (endNodes.Count > 10)
                    break;

            }

            foreach (var v in GetNeighbours(u.Value, neighbours))
            {
                if (CanEnter(v, army))
                {
                    int walkCost = Grid[v.x, v.y].MovementCost;
                    if (flyingPath)
                        walkCost = 1;
                    if (dist[u.Value] + walkCost > maxDistance)
                        continue;
                    if (Grid[v.x, v.y].EnemyOccupied && ends.Any(s => s == v) == false) //avoid targets you're not looking for
                        walkCost += 6;
                    int remainingMP = Config.ArmyMP - (dist[u.Value] % MPPerTurn);
                    if (walkCost > remainingMP)
                        walkCost = walkCost + remainingMP;
                    int alt = dist[u.Value] + walkCost;
                    if (dist.TryGetValue(v, out int distInt) == false || alt < dist[v])
                    {
                        dist[v] = alt;
                        bool diagonal = u.Value.x != v.x && u.Value.y != v.y;
                        Q.Add(new QueueNode(v, alt, u.Diagonals + (diagonal ? 1 : 0)));

                        prev[v] = u.Value;
                    }
                }
            }
        }

        // Trace path - if there is one
        var path = new List<Vec2>();

        var end = endNodes.OrderBy(s => s.priority).FirstOrDefault();

        if (end.node.Dist <= 0)
            return null;

        Vec2? current = end.node.Value;
        while (current != null)
        {
            path.Add(current.Value);
            if (prev.ContainsKey(current.Value))
                current = prev[current.Value];
        }

        path.Reverse();

        return path;
    }

    internal static bool CanEnter(Vec2 pos, Army army)
    {
        List<StrategicTileType> impassables = new List<StrategicTileType>() 
        { StrategicTileType.mountain, StrategicTileType.snowMountain, StrategicTileType.water, StrategicTileType.lava, StrategicTileType.ocean, StrategicTileType.brokenCliffs };
        if (State.World.Doodads != null && State.World.Doodads[pos.x, pos.y] == StrategicDoodadType.wall)
            return false;
        if (State.World.Doodads != null && State.World.Doodads[pos.x, pos.y] >= StrategicDoodadType.bridgeVertical && State.World.Doodads[pos.x, pos.y] <= StrategicDoodadType.virtualBridgeIntersection)
            return true;
        if (Grid[pos.x, pos.y].FriendlyOccupied)
            return false;
        if (army == null && impassables.Contains(Grid[pos.x, pos.y].TileType))
            return false; 
        if (army != null && army.impassables.Contains(Grid[pos.x, pos.y].TileType)) 
            return false;
        return true;
    }


    internal static List<PathNode> GetArmyPath(Empire empire, Army army, Vec2i destination, int startingMP, bool flight, int maxDistance = 999)
    {
        var origin = army.Position;
        //Quick sanity check to make sure that tile is actually passable
        if (TileCheck(empire, destination) == false)
            return null;
        flyingPath = flight;
        InitializeGrid();
        NormalOccupancy(empire);

        var otherPath = FindPath(new Vec2(origin.x, origin.y), new Vec2(destination.x, destination.y), army, Config.ArmyMP, maxDistance);

        if (otherPath == null)
            return null;

        List<PathNode> path = new List<PathNode>();
        for (int i = 0; i < otherPath.Count(); i++)
        {
            path.Add(new PathNode(otherPath[i].x, otherPath[i].y));
        }

        if (path.Count <= 1)
        {
            return null;
        }

        path.RemoveAt(0);

        return path;
    }

    internal static List<PathNode> GetPath(Vec2i origin, Vec2i destination, int startingMP, bool flight, int maxDistance = 999)
    {
        //Quick sanity check to make sure that tile is actually passable
        if (TileCheck(null, destination) == false)
            return null;
        flyingPath = flight;
        InitializeGrid();

        var otherPath = FindPath(new Vec2(origin.x, origin.y), new Vec2(destination.x, destination.y), null, Config.ArmyMP, maxDistance);

        if (otherPath == null)
            return null;

        List<PathNode> path = new List<PathNode>();
        for (int i = 0; i < otherPath.Count(); i++)
        {
            path.Add(new PathNode(otherPath[i].x, otherPath[i].y));
        }

        if (path.Count <= 1)
        {
            return null;
        }

        path.RemoveAt(0);

        return path;
    }


    /// <summary>
    /// Similar to normal path but can't go through towns
    /// </summary>
    /// <returns></returns>
    internal static List<PathNode> GetMonsterPath(Empire empire, Army army, Vec2i destination, int startingMP, bool flight)
    {
        var origin = army.Position;
        //Quick sanity check to make sure that tile is actually passable
        if (TileCheck(empire, destination) == false)
            return null;
        flyingPath = flight;
        InitializeGrid();
        MonsterOccupancy(empire);

        var otherPath = FindPath(new Vec2(origin.x, origin.y), new Vec2(destination.x, destination.y), army, Config.ArmyMP, 9999);

        if (otherPath == null)
            return null;

        List<PathNode> path = new List<PathNode>();
        for (int i = 0; i < otherPath.Count(); i++)
        {
            path.Add(new PathNode(otherPath[i].x, otherPath[i].y));
        }
        if (path.Count > 0)
            path.RemoveAt(0);
        else return null;

        return path;
    }


    private static void MonsterOccupancy(Empire empire)
    {
        for (int x = 0; x < Config.StrategicWorldSizeX; x++)
        {
            for (int y = 0; y < Config.StrategicWorldSizeY; y++)
            {
                Grid[x, y].FriendlyOccupied = false;
                Grid[x, y].EnemyOccupied = false;
            }
        }

        foreach (var army in StrategicUtilities.GetAllArmies())
        {
            if (empire != null)
            {
                if (army.Empire.IsEnemy(empire))
                    Grid[army.Position.x, army.Position.y].EnemyOccupied = true;
                else
                    Grid[army.Position.x, army.Position.y].FriendlyOccupied = true;
            }
        }
        foreach (var village in State.World.Villages)
        {
            Grid[village.Position.x, village.Position.y].FriendlyOccupied = true;
        }
    }

    private static void NormalOccupancy(Empire empire)
    {
        for (int x = 0; x < Config.StrategicWorldSizeX; x++)
        {
            for (int y = 0; y < Config.StrategicWorldSizeY; y++)
            {
                Grid[x, y].FriendlyOccupied = false;
                Grid[x, y].EnemyOccupied = false;
            }
        }

        foreach (var army in StrategicUtilities.GetAllArmies())
        {
            if (empire != null)
            {
                if (army.Empire.IsEnemy(empire))
                    Grid[army.Position.x, army.Position.y].EnemyOccupied = true;
                else
                    Grid[army.Position.x, army.Position.y].FriendlyOccupied = true;
            }
        }

        foreach (var village in State.World.Villages)
        {
            if (empire != null)
            {
                if (village.Empire.IsEnemy(empire))
                    Grid[village.Position.x, village.Position.y].EnemyOccupied = true;
                else if (village.Empire.IsNeutral(empire)) //If it's neither an enemy or an ally, it should be blocked
                    Grid[village.Position.x, village.Position.y].FriendlyOccupied = true;
            }
        }
    }

    private static void InitializeGrid()
    {
        if (Initialized == false)
        {
            Grid = new Cell[Config.StrategicWorldSizeX, Config.StrategicWorldSizeY];
            for (int x = 0; x < Config.StrategicWorldSizeX; x++)
            {
                for (int y = 0; y < Config.StrategicWorldSizeY; y++)
                {
                    Cell cell = new Cell
                    {
                        WalkableSurface = StrategicTileInfo.CanWalkInto(x, y),
                        MovementCost = StrategicTileInfo.WalkCost(x, y),
                        TileType = State.World.Tiles[x, y]
                    };

                    Grid[x, y] = cell;
                }
            }
            Initialized = true;
        }
    }

    internal static int TurnsToReach(Empire empire, Army army, Vec2i destination, int MPPerTurn, bool flight)
    {
        var origin = army.Position;
        //Quick sanity check to make sure that tile is actually passable
        if (TileCheck(empire, destination) == false)
            return 9999;
        flyingPath = flight;
        InitializeGrid();
        NormalOccupancy(empire);

        int turns = TurnsToReach(new Vec2(origin.x, origin.y), new Vec2(destination.x, destination.y), army, MPPerTurn);

        return turns;
    }

    internal static List<PathNode> GetPathToClosestObject(Empire empire, Army army, Vec2i[] destinations, int startingMP, int maxDistance, bool flight, int[] priorities = null)
    {
        var origin = army.Position;
        flyingPath = flight;
        InitializeGrid();
        NormalOccupancy(empire);

        Vec2[] ends = new Vec2[destinations.Length];
        for (int i = 0; i < destinations.Length; i++)
        {
            ends[i].x = destinations[i].x;
            ends[i].y = destinations[i].y;
        }

        var otherPath = FindPathMany(new Vec2(origin.x, origin.y), ends, army, Config.ArmyMP, priorities, maxDistance);

        if (otherPath == null)
            return null;
        List<PathNode> path = new List<PathNode>();

        for (int i = 0; i < otherPath.Count(); i++)
        {
            path.Add(new PathNode(otherPath[i].x, otherPath[i].y));
        }

        path.RemoveAt(0);

        return path;

    }

    internal static List<PathNode> GetMonsterPathToClosestObject(Empire empire, Army army, Vec2i[] destinations, int startingMP, int maxDistance, bool flight)
    {
        var origin = army.Position;
        //float time = Time.realtimeSinceStartup;
        flyingPath = flight;
        InitializeGrid();
        MonsterOccupancy(empire);

        Vec2[] ends = new Vec2[destinations.Length];
        for (int i = 0; i < destinations.Length; i++)
        {
            ends[i].x = destinations[i].x;
            ends[i].y = destinations[i].y;
        }

        var otherPath = FindPathMany(new Vec2(origin.x, origin.y), ends, army, Config.ArmyMP, null, maxDistance);

        if (otherPath == null)
            return null;
        List<PathNode> path = new List<PathNode>();

        for (int i = 0; i < otherPath.Count(); i++)
        {
            path.Add(new PathNode(otherPath[i].x, otherPath[i].y));
        }

        //Debug.Log(Time.realtimeSinceStartup - time);

        path.RemoveAt(0);

        return path;

    }




    static List<PathNode> GetWalkableAdjacentSquares(Empire empire, int x, int y)
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

        return proposedLocations.Where(l => TileCheck(empire, new Vec2i(l.X, l.Y)) == true).ToList();
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




    public static bool TileCheck(Empire empire, Vec2i p)
    {
        //check tile
        if (p.x < 0 || p.y < 0 || p.x >= Config.StrategicWorldSizeX || p.y >= Config.StrategicWorldSizeY)
            return false;
        if (StrategicTileInfo.CanWalkInto(p.x, p.y))
        {
            if (empire != null)
            {
                foreach (Army army in StrategicUtilities.GetAllArmies())
                {
                    if (army.Empire.IsAlly(empire) && army.Position.Matches(p))
                        return false;
                }
            }

            return true;
        }
        return false;
    }

}

