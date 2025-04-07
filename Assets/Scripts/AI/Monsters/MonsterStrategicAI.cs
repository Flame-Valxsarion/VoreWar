using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


class MonsterStrategicAI : IStrategicAI
{
    [OdinSerialize]
    MonsterEmpire empire;

    List<PathNode> path;
    Army pathIsFor;

    public MonsterStrategicAI(MonsterEmpire empire)
    {
        this.empire = empire;
    }

    public bool RunAI()
    {
        return GiveOrder();
    }

    internal bool GiveOrder()
    {
        foreach (Army army in empire.Armies.ToList())
        {
            army.IsMonsterArmy = true;
            if (army.RemainingMP < 1)
                continue;
            if (path != null && pathIsFor == army)
            {

                if (path.Count == 0)
                {
                    GenerateTaskForArmy(army);
                    return true;
                }

                Vec2i newLoc = new Vec2i(path[0].X, path[0].Y);
                Vec2i position = army.Position;
                path.RemoveAt(0);

                if (army.MoveTo(newLoc))
                    StrategicUtilities.StartBattle(army);
                else if (position == army.Position)
                    army.RemainingMP = 0; //This prevents the army from wasting time trying to move into a forest with 1 mp repeatedly
                return true;

            }
            else
            {
                GenerateTaskForArmy(army);
                if (path == null || path.Count == 0)
                    army.RemainingMP = 0;
                return true;
            }
        }
        foreach (Army army in empire.Armies)
        {
            foreach (Unit unit in army.Units)
            {
                StrategicUtilities.SpendLevelUps(unit);
            }
        }
        path = null;
        return false;
    }

    public bool TurnAI()
    {
        SpawnerInfo spawner = Config.SpawnerInfo(empire.Race);
        if (spawner == null)
            return false;
        empire.MaxArmySize = spawner.MaxArmySize;
        empire.Team = spawner.Team;
        int highestExp = State.GameManager.StrategyMode.ScaledExp;
        int baseXp = (int)(highestExp * spawner.scalingFactor / 100);

        if (spawner.GetConquestType() == Config.MonsterConquestType.CompleteDevourAndRepopulateFortify)
            empire.MaxGarrisonSize = spawner.MaxArmySize;
        else
            empire.MaxGarrisonSize = 0;



        //if (empire.Gold < 10000) empire.AddGold(8000);
        for (int j = 0; j < spawner.SpawnAttempts; j++)
        {
            if (spawner.MaxArmies == 0 || (Config.NightMonsters && !State.World.IsNight && Config.DayNightEnabled))
                break;
            if (empire.Armies.Count() < (int)Math.Max(spawner.MaxArmies * Config.OverallMonsterCapModifier, 1) && State.Rand.NextDouble() < spawner.spawnRate * Config.OverallMonsterSpawnRateModifier)
            {
                int x = 0;
                int y = 0;
                bool foundSpot = false;
                var spawners = State.GameManager.StrategyMode.Spawners.Where(s => s.Race == empire.Race).ToArray();
                for (int i = 0; i < 10; i++)
                {

                    if (spawners != null && spawners.Length > 0)
                    {
                        int num = State.Rand.Next(spawners.Length);
                        x = spawners[num].Location.x + State.Rand.Next(-2, 3);
                        y = spawners[num].Location.y + State.Rand.Next(-2, 3);
                    }
                    else
                    {
                        x = State.Rand.Next(Config.StrategicWorldSizeX);
                        y = State.Rand.Next(Config.StrategicWorldSizeY);
                    }
                    if (StrategicUtilities.IsTileClear(new Vec2i(x, y)))
                    {
                        foundSpot = true;
                        break;
                    }
                }
                if (foundSpot == false)
                    continue;
                var army = new Army(empire, new Vec2i(x, y), empire.Side);
                empire.Armies.Add(army);

                army.RemainingMP = 0;

                int count;
                if (spawner.MinArmySize > spawner.MaxArmySize)
                    count = spawner.MaxArmySize;
                else
                    count = State.Rand.Next(spawner.MinArmySize, spawner.MaxArmySize + 1);

                if (count <= 0)
                    continue;

                if (empire.ReplacedRace == Race.Wyvern)
                {
                    if (spawner.AddOnRace)
                    {
                        army.Units.Add(new Leader(empire.Side, Race.WyvernMatron, RandXp(baseXp * 2)));
                        for (int i = 1; i < count; i++)
                        {
                            army.Units.Add(new Unit(empire.Side, Race.Wyvern, RandXp(baseXp), true));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < count; i++)
                        {
                            army.Units.Add(new Unit(empire.Side, Race.Wyvern, RandXp(baseXp), true));
                        }
                    }
                }
                else if (empire.ReplacedRace == Race.FeralSharks)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (spawner.AddOnRace && State.Rand.Next(2) == 0)
                            army.Units.Add(new Unit(empire.Side, Race.DarkSwallower, RandXp(baseXp), true));
                        else
                            army.Units.Add(new Unit(empire.Side, Race.FeralSharks, RandXp(baseXp), true));
                    }
                }
                else if (empire.ReplacedRace == Race.Harvesters)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (spawner.AddOnRace && State.Rand.Next(3) == 0)
                            army.Units.Add(new Unit(empire.Side, Race.Collectors, RandXp(baseXp), true));
                        else
                            army.Units.Add(new Unit(empire.Side, Race.Harvesters, RandXp(baseXp), true));
                    }
                }
                else if (empire.ReplacedRace == Race.Dragon)
                {
                    army.Units.Add(new Unit(empire.Side, Race.Dragon, RandXp(baseXp), true));
                    for (int i = 1; i < count; i++)
                    {
                        Unit unit = new Unit(empire.Side, Race.Kobolds, RandXp(baseXp), true);
                        if (unit.BestSuitedForRanged())
                            unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.CompoundBow);
                        else
                            unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.Axe);
                        army.Units.Add(unit);
                    }
                }
                else if (empire.ReplacedRace == Race.RockSlugs)
                {
                    const float rockFraction = .08f;
                    const float spitterFraction = .25f;
                    const float coralFraction = .25f;
                    int rockCount = Math.Max((int)(rockFraction * count), 1);
                    int spitterCount = Math.Max((int)(spitterFraction * count), 1);
                    int coralCount = Math.Max((int)(coralFraction * count), 1);
                    int springCount = count - rockCount - spitterCount - coralCount;
                    for (int i = 0; i < rockCount; i++)
                        army.Units.Add(new Unit(empire.Side, Race.RockSlugs, RandXp(baseXp), true));
                    for (int i = 0; i < spitterCount; i++)
                        army.Units.Add(new Unit(empire.Side, Race.SpitterSlugs, RandXp(baseXp), true));
                    for (int i = 0; i < coralCount; i++)
                        army.Units.Add(new Unit(empire.Side, Race.CoralSlugs, RandXp(baseXp), true));
                    for (int i = 0; i < springCount; i++)
                        army.Units.Add(new Unit(empire.Side, Race.SpringSlugs, RandXp(baseXp), true));
                }
                else if (empire.ReplacedRace == Race.Compy)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (spawner.AddOnRace && State.Rand.Next(2) == 0)
                            army.Units.Add(new Unit(empire.Side, Race.Raptor, RandXp(baseXp), true));
                        else
                            army.Units.Add(new Unit(empire.Side, Race.Compy, RandXp(baseXp), true));
                    }
                }
                else if (empire.ReplacedRace == Race.Monitors)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (spawner.AddOnRace && State.Rand.Next(5) == 0)
                            army.Units.Add(new Unit(empire.Side, Race.Komodos, RandXp(baseXp), true));
                        else
                            army.Units.Add(new Unit(empire.Side, Race.Monitors, RandXp(baseXp), true));
                    }
                }
                else if (empire.ReplacedRace == Race.FeralLions)
                {
                    army.Units.Add(new Leader(empire.Side, Race.FeralLions, RandXp(baseXp * 2)));
                    for (int i = 1; i < count; i++)
                    {
                        army.Units.Add(new Unit(empire.Side, Race.FeralLions, RandXp(baseXp), true));
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        army.Units.Add(new Unit(empire.Side, empire.ReplacedRace, RandXp(baseXp), true));
                    }
                }

                if (Config.MonstersDropSpells)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (State.Rand.Next(3) == 0)
                        {
                            if (army.Units[0].Level < 3)
                                army.ItemStock.AddItem((ItemType)State.World.ItemRepository.GetRandomBookType(1, 2));
                            else if (army.Units[0].Level < 5)
                                army.ItemStock.AddItem((ItemType)State.World.ItemRepository.GetRandomBookType(1, 3));
                            else if (army.Units[0].Level < 7)
                                army.ItemStock.AddItem((ItemType)State.World.ItemRepository.GetRandomBookType(1, 4));
                            else if (army.Units[0].Level < 9)
                                army.ItemStock.AddItem((ItemType)State.World.ItemRepository.GetRandomBookType(2, 4));
                            else
                                army.ItemStock.AddItem((ItemType)State.World.ItemRepository.GetRandomBookType(2, 4));
                        }
                    }
                }


                foreach (Unit unit in army.Units)
                {
                    StrategicUtilities.SetAIClass(unit);
                }
            }
        }


        foreach (Army army in empire.Armies)
        {
            if (army.Units.Count == 0)
                continue;
            foreach (Unit unit in army.Units)
            {
                if (unit.Experience < .5f * baseXp)
                {
                    unit.SetExp(3 + (unit.Experience * 1.1f));
                }
                StrategicUtilities.SpendLevelUps(unit);
            }
            if (army.Units.Count > 0 && army.InVillageIndex != -1 && State.World.Villages[army.InVillageIndex].Race == army.Units[0].Race)
            {
                Village village = State.World.Villages[army.InVillageIndex];
                for (int i = 0; i < 8; i++)
                {
                    if (army.Units.Count() >= spawner.MaxArmySize || village.Population < 5)
                        break;
                    army.Units.Add(new Unit(empire.Side, empire.ReplacedRace, RandXp(baseXp), true));
                    village.SubtractPopulation(1);
                }
            }
        }

        return true;

        int RandXp(int exp)
        {
            if (exp < 1)
                exp = 1;
            return (int)(exp * .8f) + State.Rand.Next(10 + (int)(exp * .4));
        }
    }


    private void GenerateTaskForArmy(Army army)
    {
        pathIsFor = army;

        SpawnerInfo spawner = Config.SpawnerInfo(empire.Race);
        Config.MonsterConquestType spawnerType;
        Config.DayNightMovemntType timedMovementType;
        if (spawner != null)
        {
            spawnerType = spawner.GetConquestType();
            timedMovementType = spawner.GetDNMoveType();
        }
        else
        {
            spawnerType = Config.MonsterConquest;
            timedMovementType = Config.NightMoveMonsters ? Config.DayNightMovemntType.Night : Config.DayNightMovemntType.Off;

        }

        if (army.InVillageIndex != -1)
        {
            bool inAlliedVillage = false;
            bool inEnemyVillage = false;
            bool inOwnVillage = false;
            if (State.World.Villages[army.InVillageIndex].Empire.IsEnemy(empire))
                inEnemyVillage = true;
            else if (State.World.Villages[army.InVillageIndex].Side == empire.Side)
                inOwnVillage = true;
            else
                inAlliedVillage = true;

            if ((spawnerType != Config.MonsterConquestType.CompleteDevourAndMoveOn || State.World.Villages[army.InVillageIndex].Population > 0) &&
            (inEnemyVillage || (inOwnVillage && army.Units.Where(s => s.Race == State.World.Villages[army.InVillageIndex].Race).Any() == false)))
            {
                if (spawnerType == Config.MonsterConquestType.CompleteDevourAndMoveOn || spawnerType == Config.MonsterConquestType.CompleteDevourAndRepopulate || spawnerType == Config.MonsterConquestType.CompleteDevourAndRepopulateFortify || spawnerType == Config.MonsterConquestType.CompleteDevourAndHold)
                {
                    if (army.MonsterTurnsRemaining <= 1)
                        State.GameManager.StrategyMode.Devour(army, State.World.Villages[army.InVillageIndex].GetTotalPop());
                    else
                    {
                        State.GameManager.StrategyMode.Devour(army, State.World.Villages[army.InVillageIndex].GetTotalPop() / army.MonsterTurnsRemaining);
                        army.MonsterTurnsRemaining--;
                    }
                }
                if (spawnerType == Config.MonsterConquestType.DevourAndHold)
                {
                    Village village = State.World.Villages[army.InVillageIndex];
                    if (village.GetTotalPop() > village.Maxpop / 2)
                    {
                        State.GameManager.StrategyMode.Devour(army, village.GetTotalPop() - village.Maxpop / 2);
                    }
                }
                army.RemainingMP = 0;
                return;
            }

            if (inAlliedVillage == false && (spawnerType == Config.MonsterConquestType.CompleteDevourAndRepopulate || spawnerType == Config.MonsterConquestType.CompleteDevourAndRepopulateFortify))
            {
                army.RemainingMP = 0;
                return;
            }
        }
        if ((!spawner.MonsterScoutMP) && army.RemainingMP > Config.ArmyMP)
            army.RemainingMP = Config.ArmyMP;
        if(timedMovementType == Config.DayNightMovemntType.Night && !State.World.IsNight && Config.DayNightEnabled) //DayNight Modification (zero's out monster AP based on their settings)
        {
            army.RemainingMP = 0;
            return;
        }
        if (timedMovementType == Config.DayNightMovemntType.Day && State.World.IsNight && Config.DayNightEnabled)
        {
            army.RemainingMP = 0;
            return;
        }
        Attack(army, spawner.Confidence);
    }



    void Attack(Army army, float MaxDefenderStrength)
    {
        Village[] villages = State.World.Villages;

        List<Vec2i> potentialTargets = new List<Vec2i>();
        List<int> potentialTargetValue = new List<int>();

        foreach (Army hostileArmy in StrategicUtilities.GetAllHostileArmies(empire, true))
        {
            if (!hostileArmy.Units.All(u => u.HasTrait(Traits.Infiltrator)) && StrategicUtilities.ArmyPower(hostileArmy) < MaxDefenderStrength * StrategicUtilities.ArmyPower(army) && hostileArmy.InVillageIndex == -1)
            {
                potentialTargets.Add(hostileArmy.Position);
                potentialTargetValue.Add(0);
            }
        }
        SpawnerInfo spawner = Config.SpawnerInfo(empire.Race);
        Config.MonsterConquestType spawnerType;
        if (spawner != null)
            spawnerType = spawner.GetConquestType();
        else
            spawnerType = Config.MonsterConquest;
        if (spawnerType == Config.MonsterConquestType.IgnoreTowns)
        {
            SetClosestMonsterPath(army, potentialTargets.ToArray());
            return;
        }

        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (villages[i].Empire.IsEnemy(empire))
            {
                if (StrategicUtilities.TileThreat(villages[i].Position) < MaxDefenderStrength * StrategicUtilities.ArmyPower(army) && villages[i].Population > 0)
                {
                    potentialTargets.Add(villages[i].Position);
                    potentialTargetValue.Add(-8);
                }
            }
        }

        foreach (ConstructibleBuilding construct in State.World.Constructibles)
        {
            if (empire.IsEnemy(construct.Owner) && !construct.ruined && Config.BuildConfig.MonsterBuildingCapture != 0)
            {
                Army defender = StrategicUtilities.ArmyAt(construct.Position);
                if (defender != null && StrategicUtilities.ArmyPower(defender) > MaxDefenderStrength * StrategicUtilities.ArmyPower(army))
                    continue;
                potentialTargets.Add(construct.Position);
                int value = -6;
                // Stay on building to trigger capture effect
                if (construct.Position == army.Position)
                {
                    value = 100;
                }
                potentialTargetValue.Add(value);
            }
        }

        SetClosestPathWithPriority(army, potentialTargets.ToArray(), potentialTargetValue.ToArray());
    }

    private void SetPath(Army army, Vec2i targetPosition)
    {
        if (targetPosition != null)
        {
            path = StrategyPathfinder.GetArmyPath(empire, army, targetPosition, army.RemainingMP, army.movementMode == Army.MovementMode.Flight);
            return;
        }
        army.RemainingMP = 0;
    }

    private void SetClosestMonsterPath(Army army, Vec2i[] targetPositions, int maxDistance = 999)
    {
        if (targetPositions != null && targetPositions.Length > 1)
        {
            path = StrategyPathfinder.GetMonsterPathToClosestObject(empire, army, targetPositions, army.RemainingMP, maxDistance, army.movementMode == Army.MovementMode.Flight);
            return;
        }
        else if (targetPositions.Length == 1)
        {
            if (targetPositions[0] != null)
            {
                path = StrategyPathfinder.GetMonsterPath(empire, army, targetPositions[0], army.RemainingMP, army.movementMode == Army.MovementMode.Flight);
                return;
            }
            army.RemainingMP = 0;
        }
        else
            army.RemainingMP = 0;
    }

    private void SetClosestPathWithPriority(Army army, Vec2i[] targetPositions, int[] targetPriorities, int maxDistance = 999)
    {
        if (targetPositions != null && targetPositions.Length > 1)
        {
            path = StrategyPathfinder.GetPathToClosestObject(empire, army, targetPositions, army.RemainingMP, maxDistance, army.movementMode == Army.MovementMode.Flight, targetPriorities);
            return;
        }
        else if (targetPositions.Length == 1)
        {
            SetPath(army, targetPositions[0]);
        }
        else
            army.RemainingMP = 0;
    }
}

