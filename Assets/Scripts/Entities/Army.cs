using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Army
{
    [OdinSerialize]
    internal List<StrategicTileType> impassables = new List<StrategicTileType>() 
    { StrategicTileType.mountain, StrategicTileType.snowMountain, StrategicTileType.water, StrategicTileType.lava, StrategicTileType.ocean, StrategicTileType.brokenCliffs};
    internal enum MovementMode
    {
        Standard,
        Flight,
        Aquatic

    }

    internal enum TileAction
    {
        Impassible,
        OneMP,
        TwoMP,
        Attack,
        AttackTwoMP

    }
    [OdinSerialize]
    Empire empire;
    [OdinSerialize]
    public AIMode AIMode; //May eventually split this out, but we'll see
    [OdinSerialize]
    public int Side;
    [OdinSerialize]
    public List<Unit> Units;
    [OdinSerialize]
    private Vec2i position;

    [OdinSerialize]
    public string Name;

    public bool JustCreated = false;

    [OdinSerialize]
    public int RemainingMP { get; set; }
    [OdinSerialize]
    internal float MPMod = Config.ArmyCreationMPMod;
    [OdinSerialize]
    internal float MPCurve = Config.ArmyCreationMPCurve;
    [OdinSerialize]
    internal float SCooldown;
    [OdinSerialize]
    internal float SCooldownOffset;



    [OdinSerialize]
    public int InVillageIndex { get; private set; } = -1;
    [OdinSerialize]
    internal MovementMode movementMode = MovementMode.Standard;

    [OdinSerialize]
    internal BountyGoods BountyGoods;

    [OdinSerialize]
    internal int MonsterTurnsRemaining;
    [OdinSerialize]
    internal Teleporter LinkedTeleporter = null;
    [OdinSerialize]
    internal int teleportStoneCoolDown = 0;
    [OdinSerialize]
    internal int teleportCoolDown = 0;

    public bool DevourThisTurn { get; private set; } = false;

    [OdinSerialize]
    public Vec2i Position
    {
        get
        {
            return position;
        }

        private set
        {
            position = value;
            if (position.x != 0 && position.y != 0)
                GetTileHealRate();
        }
    }

    [OdinSerialize]
    public Vec2i Destination;
    [OdinSerialize]
    public float HealRate;

    [OdinSerialize]
    public float UsedSize;
    public float RemainnigSize => MaxSize - UsedSize;
    public float PercentFull => UsedSize / MaxSize;
    public bool MostlyFull => RemainnigSize - Empire.GetAvgDeployCost() <= 0; //Checks if army can fit any more units on average

    [OdinSerialize]
    private ItemStock itemStock;

    [OdinSerialize]
    public bool IsMonsterArmy = false;

    internal ItemStock ItemStock
    {
        get { if (itemStock == null) itemStock = new ItemStock(); return itemStock; }
        set { itemStock = value; }
    }


    internal double ArmyPower => StrategicUtilities.ArmyPower(this);
    internal int MaxSize => empire.MaxArmySize;

    internal Empire Empire => empire;

    [OdinSerialize]
    internal int BannerStyle = 0;

    internal SpriteRenderer Sprite;

    public void SetEmpire(Empire empire) => this.empire = empire;

    internal MultiStageBanner Banner { get; set; }

    public Unit LeaderIfInArmy() => Units.Where(s => s.Type == UnitType.Leader).FirstOrDefault();

    internal void SetPosition(Vec2i pos)
    {
        Position = new Vec2i(pos.x, pos.y);
    }


    public Army(Empire empire, Vec2i p, int side)
    {
        this.empire = empire;
        Side = side;
        HealRate = 0.02f;
        RemainingMP = Config.ArmyMP + (int)(Config.ArmyMP * MPMod);
        Position = p;
        Units = new List<Unit>();
        JustCreated = true;
        UsedSize = 0;

        NameArmy(empire);
        if (empire.Side < 32)
            BannerStyle = empire.BannerType;
        if ((State.World.Turn == 1 && Config.FirstTurnArmiesIdle) || 0 > RemainingMP)
            RemainingMP = 0;
    }

    internal void NameArmy(Empire empire)
    {
        string newName;
        if (State.World?.Villages != null)
            newName = State.NameGen.GetArmyName(empire.Race, StrategicUtilities.GetVillageAt(Position));
        else
            newName = State.NameGen.GetArmyName(empire.Race, null);

        empire.ArmiesCreated++;
        if (newName == "")
            Name = $"{empire.Name} Army {empire.ArmiesCreated}";
        else
            Name = newName;
    }

    public void Refresh()
    {
        foreach (Unit unit in Units)
        {
            unit.HealPercentage(HealRate);
            unit.RestoreManaPct(.6f);
            if (Config.WeightGain && Config.WeightLoss && unit.Predator)
            {
                if (unit.BodySize > 0 && State.Rand.NextDouble() < Config.WeightLossFractionBody)
                    unit.BodySize--;
                if (unit.DefaultBreastSize > 0 && State.Rand.NextDouble() < Config.WeightLossFractionBreasts)
                    unit.SetDefaultBreastSize(unit.DefaultBreastSize - 1);
                if (unit.DickSize > 0 && State.Rand.NextDouble() < Config.WeightLossFractionDick)
                    unit.DickSize--;

            }
            if (unit.HasTrait(Traits.Growth) && unit.BaseScale > 1 && !unit.HasTrait(Traits.PermanentGrowth))
            {
                float extremum = -(Config.GrowthDecayOffset - Config.GrowthDecayIncreaseRate - 1 / 2 * Config.GrowthDecayIncreaseRate);
                if (unit.BaseScale > extremum)
                    unit.BaseScale -= extremum - (extremum * ((1 - Config.GrowthDecayOffset) - Config.GrowthDecayIncreaseRate * (extremum - 1)));     // force the decay function to be monotonous
                else
                    unit.BaseScale = Math.Max(1, unit.BaseScale * ((1 - Config.GrowthDecayOffset) - Config.GrowthDecayIncreaseRate * (unit.BaseScale - 1)));  // default decayIncreaseRate = 0.04f
            }
        }
        RefreshMovementMode();

        if (empire.StrategicAI != null)
        {
            SortSpells();
        }

        RemainingMP = GetMaxMovement();
        DevourThisTurn = false;
        GetTileHealRate();
        ProcessInVillageOnTurn();
        SCooldown = 0;
        teleportStoneCoolDown = teleportStoneCoolDown >= 0 ? teleportStoneCoolDown - 1 : 0;
        teleportCoolDown = teleportCoolDown >= 0 ? teleportCoolDown - 1 : 0;
    }

    public int GetMaxMovement()
    {
        int movement = 0;
        if (Units.Count <= Config.ScoutMax)
        {
            SCooldownOffset = ((Config.ArmyMP + Config.ScoutMP) + (int)(Config.ArmyMP * MPMod) - (int)SCooldown);
            MPMod = Mathf.MoveTowards(MPMod, 0, MPCurve);
            if (-1f > MPMod)
                movement = 0;
            else if (Config.ArmyMP < (int)SCooldown)
            {
                if ((int)SCooldownOffset > 0f)
                    movement = ((int)SCooldownOffset);
                else
                    movement = 1;
            }
            movement = (Config.ArmyMP + Config.ScoutMP) + (int)(Config.ArmyMP * MPMod);
        }
        else
        {
            MPMod = Mathf.MoveTowards(MPMod, 0, MPCurve);
           if (-1f > MPMod)
                movement = 0;
           if (SCooldown > (Config.ArmyMP + (int)(Config.ArmyMP * MPMod)))
                movement = 1;
            movement = Config.ArmyMP + (int)(Config.ArmyMP * MPMod) - (int)SCooldown;
        }
        var temporalTowers = StrategicUtilities.GetActiveEmpireBuildingsWithinXTiles(Position, empire, Config.BuildConfig.BuildingPassiveRange).Where(b => b is TemporalTower);
        if (temporalTowers != null)
        {
            foreach (var building in temporalTowers)
            {
                TemporalTower tower = building as TemporalTower;
                if (Empire.IsEnemy(tower.Owner))
                {
                    if (IsMonsterArmy)
                    {
                        if (tower.disruptUpgrade.built)
                        {
                            movement = 1;
                            break;
                        }
                        else 
                        {
                            movement -= 1;
                        }
                    }
                    else if (tower.tuneUpgrade.built)
                    {
                        movement -= 1;

                    }
                }
                else if (tower.improveUpgrade.built)
                {
                    movement += 1;
                }
            }
        }
        if (movement < 0)
            { movement = 0; }

        return movement + (int)Math.Floor(AcademyResearch.GetValueFromEmpire(empire, AcademyResearchType.ArmyMP));
    }

    public void RefreshMovementMode()
    {
        int flying = 0;
        int noHill = 0;
        int yesLava = 0;
        int noSnow = 0;
        int yesMountain = 0;
        int noVolcanic = 0;
        int yesWater = 0;
        int noDesert = 0;
        int noSwamp = 0;
        int noForest = 0;
        int noGrass = 0;
        //int aquatic = 0;

        foreach (Unit unit in Units)
        {
            if (unit.HasTrait(Traits.Pathfinder))
                flying++;
            if (unit.HasTrait(Traits.HillImpedence))
                noHill++;
            if (unit.HasTrait(Traits.LavaWalker)) 
                yesLava++;
            if (unit.HasTrait(Traits.SnowImpedence))
                noSnow++;
            if (unit.HasTrait(Traits.MountainWalker))
                yesMountain++;
            if (unit.HasTrait(Traits.VolcanicImpedence))
                noVolcanic++;
            if (unit.HasTrait(Traits.WaterWalker))
                yesWater++;
            if (unit.HasTrait(Traits.DesertImpedence))
                noDesert++;
            if (unit.HasTrait(Traits.SwampImpedence))
                noSwamp++;
            if (unit.HasTrait(Traits.ForestImpedence))
                noForest++;
            if (unit.HasTrait(Traits.GrassImpedence))
                noGrass++;
            //if (unit.HasTrait(Traits.Shapeshifter) || unit.HasTrait(Traits.Skinwalker))
            //{
            //    yesLava++; yesMountain++; yesWater++; flying++;
            //}

            //else if (unit.HasTrait(Traits.Aquatic))
            //    aquatic++;
        }
        if (noHill > 0 && noHill > Units.Count / 2)
        {
            if (!impassables.Contains(StrategicTileType.hills))
            {
                impassables.Add(StrategicTileType.hills);
            }
            if (!impassables.Contains(StrategicTileType.snowHills))
            {
                impassables.Add(StrategicTileType.snowHills);
            }
            if (!impassables.Contains(StrategicTileType.sandHills))
            {
                impassables.Add(StrategicTileType.sandHills);
            }
        }
        else
        {
            impassables.Remove(StrategicTileType.hills);
            impassables.Remove(StrategicTileType.snowHills);
            impassables.Remove(StrategicTileType.sandHills);

        }

        if (noSnow > 0 && noSnow > Units.Count / 2)
        {
            if (!impassables.Contains(StrategicTileType.snow))
            {
                impassables.Add(StrategicTileType.snow);
            }
            if (!impassables.Contains(StrategicTileType.snowHills))
            {
                impassables.Add(StrategicTileType.snowHills);
            }
        }
        else
        {
            impassables.Remove(StrategicTileType.snow);
            impassables.Remove(StrategicTileType.snowHills);
        }

        if (yesLava > 0 && yesLava >= Units.Count / 2)
            impassables.Remove(StrategicTileType.lava);
        else if (!impassables.Contains(StrategicTileType.lava))
            impassables.Add(StrategicTileType.lava);

        if (yesMountain > 0 && yesMountain >= Units.Count / 2)
        {
            impassables.Remove(StrategicTileType.mountain);
            impassables.Remove(StrategicTileType.snowMountain);
            impassables.Remove(StrategicTileType.brokenCliffs);
        }
        else 
        {  
            if (!impassables.Contains(StrategicTileType.snowMountain))
                impassables.Add(StrategicTileType.snowMountain);
            if (!impassables.Contains(StrategicTileType.mountain))
                 impassables.Add(StrategicTileType.mountain);
            if (!impassables.Contains(StrategicTileType.brokenCliffs))
                impassables.Add(StrategicTileType.brokenCliffs);
        }

        if (noVolcanic > 0 && noVolcanic > Units.Count / 2)
        {
            if (!impassables.Contains(StrategicTileType.volcanic))
                impassables.Add(StrategicTileType.volcanic);
        }
        else impassables.Remove(StrategicTileType.volcanic);

        if (yesWater > 0 && yesWater >= Units.Count / 2)
        {
            impassables.Remove(StrategicTileType.ocean);
            impassables.Remove(StrategicTileType.water);
        }
        else
        {
            if (!impassables.Contains(StrategicTileType.ocean))
                impassables.Add(StrategicTileType.ocean);
            if (!impassables.Contains(StrategicTileType.water))
                impassables.Add(StrategicTileType.water);
        }

        if (noDesert > 0 && noDesert > Units.Count / 2)
        {
            if (!impassables.Contains(StrategicTileType.desert))
            {
                impassables.Add(StrategicTileType.desert);
            }
            if (!impassables.Contains(StrategicTileType.sandHills))
            {
                impassables.Add(StrategicTileType.sandHills);
            }
        }
        else
        {
            impassables.Remove(StrategicTileType.desert);
            impassables.Remove(StrategicTileType.sandHills);
        }

        if (noSwamp > 0 && noSwamp > Units.Count / 2)
        {
            if (!impassables.Contains(StrategicTileType.swamp))
            {
                impassables.Add(StrategicTileType.swamp);
            }
            if (!impassables.Contains(StrategicTileType.purpleSwamp))
            {
                impassables.Add(StrategicTileType.purpleSwamp);
            }
        }
        else
        {
            impassables.Remove(StrategicTileType.swamp);
            impassables.Remove(StrategicTileType.purpleSwamp);
        }

        if (noForest > 0 && noForest > Units.Count / 2)
        {
            if (!impassables.Contains(StrategicTileType.forest))
            {
                impassables.Add(StrategicTileType.forest);
            }
            if (!impassables.Contains(StrategicTileType.snowTrees))
            {
                impassables.Add(StrategicTileType.snowTrees);
            }
        }
        else
        {
            impassables.Remove(StrategicTileType.forest);
            impassables.Remove(StrategicTileType.snowTrees);
        }

        if (noGrass > 0 && noGrass > Units.Count / 2)
        {
            if (!impassables.Contains(StrategicTileType.grass))
                impassables.Add(StrategicTileType.grass);
        }
        else impassables.Remove(StrategicTileType.grass);


        if (flying > 0 && flying >= Units.Count / 2)
            movementMode = MovementMode.Flight;
        //else if (aquatic >= Units.Count / 2)
        //    movementMode = MovementMode.Aquatic;
        else
            movementMode = MovementMode.Standard;
    }

    /// <summary>
    /// Removes dead troops, and deletes the army itself if needed
    /// </summary>
    public void Prune()
    {
        foreach (Unit unit in Units.ToList())
        {
            if (unit.Type == UnitType.Summon)
            {
                Units.Remove(unit);
            }
            if (unit.IsDead)
            {
                Units.Remove(unit);
                State.World.Stats.SoldiersLost(1, unit.Side);
            }
        }

        if (Units.Count == 0)
        {
            empire.Armies.Remove(this);
            if (JustCreated)
                empire.ArmiesCreated--;
        }
    }

    public int GetHealthPercentage()
    {
        float hp = 0;
        float maxhp = 0;
        foreach (Unit unit in Units)
        {
            hp += unit.Health;
            maxhp += unit.MaxHealth;
        }
        float pct = 100 * hp / maxhp;
        return (int)pct;
    }

    public int GetAbsHealth()
    {
        int hp = 0;
        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i] != null)
            {
                hp += Units[i].Health;
            }
        }
        return hp;
    }

    public bool MoveTo(Vec2i pos)
    {
        return CheckMove(pos);
    }

    public bool Move(int direction)
    {
        Vec2i pos = GetPos(direction);
        return CheckMove(pos);
    }

    Vec2i GetPos(int i)
    {
        switch (i)
        {
            case 0:
                return new Vec2i(Position.x, Position.y + 1);
            case 1:
                return new Vec2i(Position.x + 1, Position.y + 1);
            case 2:
                return new Vec2i(Position.x + 1, Position.y);
            case 3:
                return new Vec2i(Position.x + 1, Position.y - 1);
            case 4:
                return new Vec2i(Position.x, Position.y - 1);
            case 5:
                return new Vec2i(Position.x - 1, Position.y - 1);
            case 6:
                return new Vec2i(Position.x - 1, Position.y);
            case 7:
                return new Vec2i(Position.x - 1, Position.y + 1);
        }
        return Position;
    }

    bool CheckMove(Vec2i pos)
    {
        TileAction act = CheckTileForActionType(pos);

        if (act == TileAction.Impassible)
        {
            return false;
        }

        if (act == TileAction.TwoMP)
        {
            if (RemainingMP > 1)
            {
                if (Banner != null && Banner.gameObject.activeSelf)
                    State.GameManager.StrategyMode.Translator?.SetTranslator(Banner.transform, Position, pos, Config.StrategicAIMoveDelay, State.GameManager.StrategyMode.IsPlayerTurn);
                else if (Sprite != null)
                    State.GameManager.StrategyMode.Translator?.SetTranslator(Sprite.transform, Position, pos, Config.StrategicAIMoveDelay, State.GameManager.StrategyMode.IsPlayerTurn);
                if (State.GameManager.StrategyMode.IsPlayerTurn)
                    State.GameManager.StrategyMode.UndoMoves.Add(new StrategicMoveUndo(this, RemainingMP, (int)SCooldown, new Vec2i(Position.x, Position.y)));
                Position = pos;
                StrategicUtilities.TryClaim(pos, empire);
                if (Units.Count <= Config.ScoutMax)
                {
                    SCooldown += 2f;
                }
                RemainingMP -= 2;
                return false;
            }
            return false;
        }

        if ((act == TileAction.Attack && RemainingMP > 0) || (act == TileAction.AttackTwoMP && RemainingMP > 1))
        {
            RemainingMP = 0;
            if (Banner != null && Banner.gameObject.activeSelf)
                State.GameManager.StrategyMode.Translator?.SetTranslator(Banner.transform, Position, pos, Config.StrategicAIMoveDelay, State.GameManager.StrategyMode.IsPlayerTurn);
            else if (Sprite != null)
                State.GameManager.StrategyMode.Translator?.SetTranslator(Sprite.transform, Position, pos, Config.StrategicAIMoveDelay, State.GameManager.StrategyMode.IsPlayerTurn);
            State.GameManager.StrategyMode.UndoMoves.Clear();
            Position = pos;
            return true;
        }
        if (act == TileAction.OneMP && RemainingMP > 0)
        {
            if (State.GameManager.StrategyMode.IsPlayerTurn)
                State.GameManager.StrategyMode.UndoMoves.Add(new StrategicMoveUndo(this, RemainingMP, (int)SCooldown, new Vec2i(Position.x, Position.y)));
                if (Units.Count <= Config.ScoutMax)
                {
                    SCooldown += 1f;
                }
            RemainingMP -= 1;
            if (Banner != null && Banner.gameObject.activeSelf)
                State.GameManager.StrategyMode.Translator?.SetTranslator(Banner.transform, Position, pos, Config.StrategicAIMoveDelay, State.GameManager.StrategyMode.IsPlayerTurn);
            else if (Sprite != null)
                State.GameManager.StrategyMode.Translator?.SetTranslator(Sprite.transform, Position, pos, Config.StrategicAIMoveDelay, State.GameManager.StrategyMode.IsPlayerTurn);
            Position = pos;
            StrategicUtilities.TryClaim(pos, empire);
            return false;
        }
        return false;
    }

    internal TileAction CheckTileForActionType(Vec2i p)
    {
        bool flyer = movementMode == MovementMode.Flight;
        foreach (Army army in StrategicUtilities.GetAllArmies())
        {
            if (p.Matches(army.position))
            {
                if (army.empire.IsEnemy(empire) == false)
                {
                    return TileAction.Impassible;
                }
                else
                {
                    if (StrategicTileInfo.WalkCost(p.x, p.y) == 2 && flyer == false)
                        return TileAction.AttackTwoMP;
                    return TileAction.Attack;
                }
            }
        }

        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (p.Matches(State.World.Villages[i].Position))
            {
                if (State.World.Villages[i].Empire.IsAlly(empire))
                {
                    return TileAction.OneMP;
                }
                else if (State.World.Villages[i].Empire.IsEnemy(empire))
                {
                    return TileAction.Attack;
                }
                else
                    return TileAction.Impassible;
            }
        }
        if (StrategicTileInfo.WalkCost(p.x, p.y) == 2 && flyer == false)
        {
            return TileAction.TwoMP;
        }
        if (StrategyPathfinder.CanEnter(p, this) == false)
        {
            return TileAction.Impassible;
        }
        return TileAction.OneMP;
    }

    internal void UpdateInVillage()
    {
        InVillageIndex = -1;
        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (position.Matches(State.World.Villages[i].Position))
            {
                InVillageIndex = i;
                State.World.Villages[i].ItemStock.TransferAllItems(ItemStock);
            }
        }
    }

    internal void GetTileHealRate()
    {
        UpdateInVillage();

        HealRate = 0.02f;
        if (InVillageIndex > -1)
        {
            HealRate = State.World.Villages[InVillageIndex].Healrate();
        }
        HealRate *= 1 + 0.25f * AcademyResearch.GetValueFromEmpire(Empire, AcademyResearchType.ArmyHealRate);
    }

    internal void ProcessInVillageOnTurn()
    {
        UpdateInVillage();
        if (InVillageIndex > -1)
        {
            var village = State.World.Villages[InVillageIndex];

            var minExp = village.GetStartingXp();
            foreach (Unit unit in Units)
            {
                if (unit.Experience < minExp)
                {
                    unit.SetExp(minExp);
                }
            }

            foreach (Unit unit in Units)
            {
                unit.AddTraits(village.GetTraitsToAdd());
            }
        }
    }
    public int GetDevourmentCapacity(int minimumHealAmount)
    {
        int cap = 0;

        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].Predator == false)
                continue;
            cap += (10 - minimumHealAmount + Units[i].MaxHealth - Units[i].Health) / 10;
        }
        return cap;
    }

    public void DevourHeal(int numprey)
    {
        int remainingPrey = numprey;
        Unit target;
        if (numprey > 0)
        {
            RemainingMP = 0;
            DevourThisTurn = true;
            State.GameManager.StrategyMode.UndoMoves.Clear();
        }
        while (remainingPrey > 0)
        {
            target = Units.Where(s => s.Predator).OrderByDescending(l => l.MaxHealth - l.Health).ThenBy(l => State.Rand.Next()).FirstOrDefault();
            if (target == null)
                break;
            target.Feed();
            remainingPrey--;
        }

    }


    internal int TrainingGetCost(int level)
    {
        int cost = 2;
        for (int i = 1; i <= level; i++)
        {
            cost += 12 * i;
        }
        cost *= Units.Count;
        return cost;
    }

    internal int TrainingGetExpValue(int level)
    {
        int xpGain = (int)Mathf.Ceil(Config.ExperiencePerLevel / 2f);
        xpGain += ((int)Mathf.Ceil(Config.ExperiencePerLevel / 2f) + Config.AdditionalExperiencePerLevel) * level;
        return xpGain;
    }

    internal void Train(int level)
    {
        int xpGain = TrainingGetExpValue(level);
        xpGain += (int)Math.Round(xpGain * 0.25f * AcademyResearch.GetValueFromEmpire(empire, AcademyResearchType.TrainingEXP));
        int cost = TrainingGetCost(level);

        if (empire.Gold >= cost)
        {
            foreach (Unit unit in Units)
            {
                unit.GiveExp(xpGain);
            }
            empire.SpendGold(cost);
            State.World.Stats.SpentGoldOnArmyTraining(cost, Side);
        }

    }

    internal void SortSpells()
    {
        var spellbookTypes = ItemStock.GetAllSpellBooks();
        if (spellbookTypes.Count > 0)
        {
            int casters = Units.Where(s => s.AIClass == AIClass.MagicMelee || s.AIClass == AIClass.MagicRanged && s.FixedGear == false).Count();
            {
                int shortfall = (Units.Count / 2) - casters;
                if (shortfall > 0)
                {
                    var possibleUnits = Units.Where(s => s.AIClass != AIClass.MagicMelee && s.AIClass != AIClass.MagicRanged || s.FixedGear == false).ToList();
                    int possibleCount = Math.Min(Math.Min(shortfall, spellbookTypes.Count), possibleUnits.Count);
                    for (int i = 0; i < possibleCount; i++)
                    {
                        if (possibleUnits[i].GetBestRanged() != null)
                            possibleUnits[i].AIClass = AIClass.MagicRanged;
                        else
                            possibleUnits[i].AIClass = AIClass.MagicMelee;
                    }
                }
            }
        }

        if (spellbookTypes.Count == 0)
            return;
        List<SpellBook> books = new List<SpellBook>();
        foreach (var bookType in spellbookTypes)
        {
            books.Add((SpellBook)State.World.ItemRepository.GetItem(bookType));
        }
        books.OrderByDescending(s => s.Tier);

        var magicUsers = Units.Where(s => s.FixedGear == false && s.AIClass == AIClass.MagicMelee || s.AIClass == AIClass.MagicRanged).OrderByDescending(s => s.GetStatBase(Stat.Mind));



        foreach (Unit unit in magicUsers)
        {
            if (books.Count == 0)
                break;
            if (unit.Items[1] == null)
            {
                if (ItemStock.TakeItem(State.World.ItemRepository.GetItemType(books[0])))
                {
                    unit.SetItem(books[0], 1);
                    books.RemoveAt(0);
                }
            }
            else if (unit.Items[1] is SpellBook == false)
            {
                if (ItemStock.TakeItem(State.World.ItemRepository.GetItemType(books[0])))
                {
                    ItemStock.AddItem(State.World.ItemRepository.GetItemType(unit.Items[1]));
                    unit.SetItem(books[0], 1);
                    books.RemoveAt(0);
                }
            }
            else
            {
                if (unit.Items[1] is SpellBook book)
                {
                    if (books[0].Tier > book.Tier)
                    {
                        if (ItemStock.TakeItem(State.World.ItemRepository.GetItemType(books[0])))
                        {
                            ItemStock.AddItem(State.World.ItemRepository.GetItemType(book));
                            unit.SetItem(books[0], 1);
                            books.RemoveAt(0);
                        }

                    }
                }
            }
        }

    }

    internal void RecalculateSizeValue()
    {
        UsedSize = 0;
        foreach (Unit unit in Units)
        {
            UsedSize += State.RaceSettings.GetDeployCost(unit.Race) * unit.TraitBoosts.DeployCostMult;
        }
    }

    internal float GetAverageArmyDeployment()
    {
        float avgDeploy = 0;
        foreach (Unit unit in Units)
        {
            avgDeploy += State.RaceSettings.GetDeployCost(unit.Race) * unit.TraitBoosts.DeployCostMult;
        }
        avgDeploy /= Units.Count;
        return avgDeploy;
    }
}
