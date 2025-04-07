using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class ConstructionWants
{
    public bool Wealth = false;
    public bool Population = false;
    public bool Garrison = false;
    public bool HealRate = false;
    public bool StartingExperience = false;
    public bool Defenses = false;
    public bool Mercenaries = false;
    public bool Magic = false;
}

public class StrategicAI : IStrategicAI
{
    [OdinSerialize]
    Empire empire;

    [OdinSerialize]
    [Obsolete]
    public bool strongerAI = false;

    [OdinSerialize]
    public int CheatLevel = 0;

    [OdinSerialize]
    public bool smarterAI = false;

    StrategicArmyCommander ArmyCommander;

    StrategicBuildingContractor BuildingContractor;

    int idealArmySize;

    int AISide => empire.Side;

    public StrategicAI(Empire empire, int cheatLevel, bool smarterAI)
    {
        this.empire = empire;
        CheatLevel = cheatLevel;
        this.smarterAI = smarterAI;
    }

    void RegenArmyCommander()
    {
        ArmyCommander = new StrategicArmyCommander(empire, empire.MaxArmySize, smarterAI);
    }

    void RegenBuildingContractor()
    {
        BuildingContractor = new StrategicBuildingContractor(empire);
    }

    public bool RunAI()
    {
        if (ArmyCommander == null)
            RegenArmyCommander();
        return ArmyCommander.GiveOrder();
    }


    public bool TurnAI()
    {
        if (CheatLevel >= 1)
        {
            empire.AddGold(100 * CheatLevel);
        }
        bool boughtWeapons = false;

        if (Config.Diplomacy)
            ProcessRelations();

        // Don't run buildings unless system is enabled or one is captured off of map.
        if (Config.BuildConfig.BuildingSystemEnabled || empire.Buildings.Count() > 0)
        {
            if (BuildingContractor == null)
                RegenBuildingContractor();
            BuildingContractor.AssessBuildStatus();
        }

        if (ArmyCommander == null)
            RegenArmyCommander();
        ArmyCommander.ResetPath();
        ArmyCommander.SpendExpAndRecruit();

        int currentUnitCount = 0;
        foreach (Army army in empire.Armies)
        {
            currentUnitCount += army.Units.Count;
        }



        int totalIncome = empire.Income;
        for (int i = 0; i < empire.Armies.Count; i++)
        {
            totalIncome += empire.GetUpkeep();
        }

        int idealUnitCount;

        idealUnitCount = (int)Math.Round(totalIncome / Math.Max(empire.GetUpkeepCoefficient() * 3 / 5, 1));
        if (empire.Gold > 3000)
            idealUnitCount = (int)Math.Round(totalIncome / Math.Max(empire.GetUpkeepCoefficient() * 4 / 5, 1));
        int minArmySize = (int)Math.Round((empire.MaxArmySize / empire.GetAvgDeployCost()) * 3 / 4);
        if (currentUnitCount > 40 || ArmyCommander.StrongestArmyRatio < .7f)
            minArmySize = (int)Math.Round((empire.MaxArmySize / empire.GetAvgDeployCost()));

        if (currentUnitCount > 32)
        {
            for (int i = 0; i < State.World.Villages.Length; i++)
            {
                BuyGarrisonWeapons(i);
            }
            boughtWeapons = true;
        }

        bool purchasedArmy = false;
        int unitCost = Config.ArmyCost + 6;
        bool forcedHeavyWeapon = false;
        int minThreshold = 0;
        if (empire.Armies.Count > 3)
        {
            minThreshold = 100 * (empire.Armies.Count - 3);
        }
        if (smarterAI)
        {
            unitCost = Config.ArmyCost + State.World.ItemRepository.GetItem(ItemType.CompoundBow).Cost;
            forcedHeavyWeapon = true;
        }
        if (empire.Gold > 50 + minThreshold + minArmySize * unitCost && empire.Income > 10 && (idealUnitCount - currentUnitCount >= minArmySize || currentUnitCount == 0) && empire.Armies.Count() < Config.MaxArmies)
        {

            purchasedArmy = PurchaseArmy(unitCost, ref currentUnitCount, forcedHeavyWeapon);
            for (int i = 0; i < 10; i++) //Can purchase additional armies if absolutely loaded with cash
            {
                if (empire.Gold > minThreshold + (i * 100) + 40 * unitCost && (idealUnitCount - currentUnitCount > 12) && empire.Armies.Count() < Config.MaxArmies)
                    PurchaseArmy(unitCost, ref currentUnitCount, forcedHeavyWeapon);
                else
                    break;
            }
        }

        if (empire.Gold < 150 && empire.Income < 0 && empire.Side < 50)
        {
            DismissWeakestArmy();
        }

        if (boughtWeapons == false)
        {
            for (int i = 0; i < State.World.Villages.Length; i++)
            {
                BuyGarrisonWeapons(i);
            }
        }

        if (currentUnitCount >= idealUnitCount || empire.Gold > 700 || empire.Armies.Count > 2)
        {
            PurchaseBuildings();
        }

        return purchasedArmy;
    }

    public void ProcessRelations()
    {
        int neutralCount = 0;
        Empire lowestNeutral = null;
        float lowestRel = 0;
        int enemyCount = 0;
        foreach (Empire otherEmp in State.World.MainEmpires)
        {
            if (otherEmp == empire)
                continue;
            if (otherEmp.KnockedOut)
                continue;
            var relation = RelationsManager.GetRelation(empire.Side, otherEmp.Side);
            switch (relation.Type)
            {
                case RelationState.Neutral:
                    ProcessNeutral(ref neutralCount, ref lowestNeutral, ref lowestRel, otherEmp, relation);
                    break;
                case RelationState.Allied:
                    ProcessAllied(otherEmp, relation);
                    break;
                case RelationState.Enemies:
                    enemyCount = ProcessEnemies(enemyCount, otherEmp, relation);
                    break;
            }

        }
        if (enemyCount == 0 && neutralCount > 0 && lowestNeutral != null)
        {
            if (Config.LockedAIRelations == false)
                RelationsManager.SetWar(empire, lowestNeutral);
        }

    }

    private int ProcessEnemies(int enemyCount, Empire otherEmp, Relationship relation)
    {
        enemyCount++;
        if (relation.Attitude > .2f)
        {
            if (otherEmp.StrategicAI == null)
            {
                if (relation.TurnsSinceAsked == -1 || relation.TurnsSinceAsked > 20) //Done this way to avoid pestering the player
                    RelationsManager.AskPlayerForPeace(empire, otherEmp);
            }
            else
            {
                if (Config.LockedAIRelations)
                    return enemyCount;
                var counterRelation = RelationsManager.GetRelation(otherEmp.Side, empire.Side);
                if (counterRelation.Attitude > -.2f)
                {
                    RelationsManager.SetPeace(empire, otherEmp);
                    NotificationSystem.ShowNotification($"{empire.Name} have entered peace with the {otherEmp.Name}", 3);
                    foreach (Empire playerEmp in State.World.MainEmpires.Where(s => s.StrategicAI == null))
                    {
                        if (playerEmp != otherEmp)
                        {
                            playerEmp.Reports.Add(new StrategicReport($"{empire.Name} have entered peace with the {otherEmp.Name}!", otherEmp.CapitalCity?.Position ?? new Vec2(0, 0)));
                        }
                    }
                }
            }

        }

        return enemyCount;
    }

    private void ProcessAllied(Empire otherEmp, Relationship relation)
    {
        if (otherEmp.VillageCount == 0)
        {
            relation.Attitude *= .6f;
        }
        if (relation.Attitude < .4f)
        {
            if (Config.LockedAIRelations && otherEmp.StrategicAI != null)
                return;

            RelationsManager.SetPeace(empire, otherEmp);
            NotificationSystem.ShowNotification($"{empire.Name} have terminated their alliance with {otherEmp.Name}", 3);
            foreach (Empire playerEmp in State.World.MainEmpires.Where(s => s.StrategicAI == null))
            {
                if (playerEmp != otherEmp)
                {
                    playerEmp.Reports.Add(new StrategicReport($"{empire.Name} have nullified their alliance with the {otherEmp.Name}!", otherEmp.CapitalCity?.Position ?? new Vec2(0, 0)));
                }
            }
            if (otherEmp.StrategicAI == null)
            {
                otherEmp.Reports.Add(new StrategicReport($"{empire.Name} have nullified their alliance with you!", otherEmp.CapitalCity?.Position ?? new Vec2(0, 0)));
            }
        }
    }

    private void ProcessNeutral(ref int neutralCount, ref Empire lowestNeutral, ref float lowestRel, Empire otherEmp, Relationship relation)
    {
        neutralCount++;
        if (lowestNeutral == null)
        {
            lowestNeutral = otherEmp;
            lowestRel = relation.Attitude;
        }
        else if (relation.Attitude < lowestRel)
        {
            lowestNeutral = otherEmp;
            lowestRel = relation.Attitude;
        }
        if (relation.Attitude < -.75f)
        {
            if (Config.LockedAIRelations && otherEmp.StrategicAI != null)
                return;
            RelationsManager.SetWar(empire, otherEmp);
            NotificationSystem.ShowNotification($"{empire.Name} have declared war on {otherEmp.Name}", 3);
            foreach (Empire playerEmp in State.World.MainEmpires.Where(s => s.StrategicAI == null))
            {
                if (playerEmp != otherEmp)
                {
                    playerEmp.Reports.Add(new StrategicReport($"{empire.Name} have declared war on the {otherEmp.Name}!", otherEmp.CapitalCity?.Position ?? new Vec2(0, 0)));
                }
            }
            if (otherEmp.StrategicAI == null)
            {
                otherEmp.Reports.Add(new StrategicReport($"{empire.Name} have declared war on you!", otherEmp.CapitalCity?.Position ?? new Vec2(0, 0)));
            }
        }
        if (relation.Attitude > 1.25f)
        {

            if (otherEmp.StrategicAI == null)
            {
                if (relation.TurnsSinceAsked == -1 || relation.TurnsSinceAsked > 20) //Done this way to avoid pestering the player
                    RelationsManager.AskPlayerForAlliance(empire, otherEmp);
            }
            else
            {
                if (Config.LockedAIRelations && otherEmp.StrategicAI != null)
                    return;
                var counterRelation = RelationsManager.GetRelation(otherEmp.Side, empire.Side);
                if (counterRelation.Attitude > .7f)
                {
                    RelationsManager.SetAlly(empire, otherEmp);
                    NotificationSystem.ShowNotification($"{empire.Name} have allied with {otherEmp.Name}", 3);
                    foreach (Empire playerEmp in State.World.MainEmpires.Where(s => s.StrategicAI == null))
                    {
                        if (playerEmp != otherEmp)
                        {
                            playerEmp.Reports.Add(new StrategicReport($"{empire.Name} have allied with the {otherEmp.Name}!", otherEmp.CapitalCity?.Position ?? new Vec2(0, 0)));
                        }
                    }
                }
            }

        }
    }

    public void JustTryToBuildAnything(Village village)
    {
        TryVillageConstruction(village, empire, new ConstructionWants()
        {
            Wealth = true
        });
        TryVillageConstruction(village, empire, new ConstructionWants()
        {
            Population = true
        });
        TryVillageConstruction(village, empire, new ConstructionWants()
        {
            StartingExperience = true
        });
        TryVillageConstruction(village, empire, new ConstructionWants()
        {
            Mercenaries = true,
            Garrison = true,
            HealRate = true,
            Defenses = true,
            Magic = true,
        });
    }

    static public void TryVillageConstruction(Village village, Empire empire, ConstructionWants wants)
    {
        var validBuildings = new Dictionary<VillageBuilding, VillageBuildingDefinition>();

        if (wants.Wealth)
        {
            var resultList = VillageBuildingList.Buildings.Where(pair =>
                pair.Value.Boosts.WealthMult > 0.0f
                || pair.Value.Boosts.WealthAdd > 0
                || pair.Value.Boosts.MaxHappinessAdd > 0
            );
            foreach (var result in resultList)
                validBuildings.Add(result.Key, result.Value);
        }
        else if (wants.Population)
        {
            var resultList = VillageBuildingList.Buildings.Where(pair =>
                pair.Value.Boosts.PopulationGrowthMult > 0.0f
                || pair.Value.Boosts.PopulationMaxMult > 0.0f
                || pair.Value.Boosts.FarmsEquivalent > 0.0f
                || pair.Value.Boosts.PopulationMaxAdd > 0
            );
            foreach (var result in resultList)
                validBuildings.Add(result.Key, result.Value);
        }
        else if (wants.Garrison)
        {
            var resultList = VillageBuildingList.Buildings.Where(pair =>
                pair.Value.Boosts.GarrisonMaxMult > 0.0f
                || pair.Value.Boosts.GarrisonMaxAdd > 0
            );
            foreach (var result in resultList)
                validBuildings.Add(result.Key, result.Value);
        }
        else if (wants.HealRate)
        {
            var resultList = VillageBuildingList.Buildings.Where(pair =>
                pair.Value.Boosts.HealRateMult > 0.0f
            );
            foreach (var result in resultList)
                validBuildings.Add(result.Key, result.Value);
        }
        else if (wants.StartingExperience)
        {
            var resultList = VillageBuildingList.Buildings.Where(pair =>
                pair.Value.Boosts.StartingExpAdd > 0
                || pair.Value.Boosts.TeamStartingExpAdd > 0
                || pair.Value.Boosts.MaximumTrainingLevelAdd > 0
            );
            foreach (var result in resultList)
                validBuildings.Add(result.Key, result.Value);
        }
        else if (wants.Defenses)
        {
            var resultList = VillageBuildingList.Buildings.Where(pair =>
                pair.Value.Boosts.hasWall == true
            );
            foreach (var result in resultList)
                validBuildings.Add(result.Key, result.Value);
        }
        else if (wants.Mercenaries)
        {
            var resultList = VillageBuildingList.Buildings.Where(pair =>
                pair.Value.Boosts.MercsPerTurnAdd > 0
                || pair.Value.Boosts.MaxMercsAdd > 0
                || pair.Value.Boosts.MaxAdventurersAdd > 0
                || pair.Value.Boosts.AdventurersPerTurnAdd > 0
            );
            foreach (var result in resultList)
                validBuildings.Add(result.Key, result.Value);
        }
        else if (wants.Magic)
        {
            var resultList = VillageBuildingList.Buildings.Where(pair =>
                pair.Value.Boosts.SpellLevels > 0
            );
            foreach (var result in resultList)
                validBuildings.Add(result.Key, result.Value);
        }


        foreach (var possibleBuilding in validBuildings)
        {
            var considerBuilding = possibleBuilding.Value;
            if (considerBuilding.CanBuild(village) && considerBuilding.CanAfford(empire))
            {
                village.Build(considerBuilding.Id, empire);
            }
        }
    }

    private void DismissWeakestArmy()
    {
        if (empire.Armies.Count > 1)
        {
            Army weakestArmy = empire.Armies.OrderBy(s => s.Units.Sum(t => t.Experience) / s.Units.Count()).FirstOrDefault();
            StrategicUtilities.ProcessTravelingUnits(weakestArmy.Units, weakestArmy);
            foreach (Unit unit in weakestArmy.Units.ToList())
            {
                weakestArmy.Units.Remove(unit);
            }
            empire.Armies.Remove(weakestArmy);
        }
        else if (empire.Armies.Count == 1) //If there's only one army, just remove the weakest unit
        {
            Unit weakest = empire.Armies.First().Units.Where(s => s.Type != UnitType.Leader).OrderBy(s => s.Experience).FirstOrDefault();
            if (weakest != null)
            {
                StrategicUtilities.ProcessTravelingUnit(weakest, empire.Armies.First());
                empire.Armies[0].Units.Remove(weakest);
                if (empire.Armies[0].Units.Any() == false)
                    empire.Armies.RemoveAt(0);
            }
        }
    }

    void PurchaseBuildings()
    {
        Village[] ownVillages = State.World.Villages.Where(s => s.Side == AISide && s.GetTotalPop() > 30).Where(s => StrategicUtilities.EnemyArmyWithinXTiles(s, 4) == false).OrderByDescending(s => s.GetTotalPop()).ToArray();
        foreach (Village village in ownVillages)
        {
            TryVillageConstruction(village, empire, new ConstructionWants()
            {
                Mercenaries = true
            });

            if (empire.CanVore == false)
            {
                TryVillageConstruction(village, empire, new ConstructionWants()
                {
                    HealRate = true
                });
            }
            JustTryToBuildAnything(village);

            if (empire.Gold < 80)
                break;
        }
    }


    private bool PurchaseArmy(int unitCost, ref int currentUnitCount, bool ForceAdvancedWeapons)
    {
        idealArmySize = empire.Gold / unitCost;
        if (idealArmySize > empire.MaxArmySize / empire.GetAvgDeployCost())
        {
            idealArmySize = (int)Math.Round(empire.MaxArmySize / empire.GetAvgDeployCost());
        }
        int tier = 1;
        if (empire.Gold > idealArmySize * 40)
            tier = 2;
        if (ForceAdvancedWeapons)
            tier = 2;
        Village v = GetBestVillageToMakeArmy(empire);
        if (v != null)
        {
            Army army = MakeArmy(v, tier);
            if (army == null)
                return false;
            if (State.Rand.Next(0, 4) == 0)
                army.AIMode = AIMode.Sneak;
            empire.Armies.Add(army);
            currentUnitCount += army.Units.Count;
            return true;
        }
        return false;
    }

    Army MakeArmy(Village village, int tier)
    {
        Army army = new Army(empire, new Vec2i(village.Position.x, village.Position.y), village.Side);
        if (idealArmySize > village.GetTotalPop() - 3)
            idealArmySize = village.GetTotalPop() - 3;
        if (idealArmySize < 4 && Math.Floor(empire.MaxArmySize / empire.GetAvgDeployCost()) > idealArmySize)
            return null;
        
        if (Config.AICanCheatSpecialMercs && MercenaryHouse.UniqueMercs?.Count > 0)
        {
            foreach (var merc in MercenaryHouse.UniqueMercs)
            {
                if (State.Rand.Next(40 * MercenaryHouse.UniqueMercs.Count) == 0)
                {
                    if (StrategicUtilities.ArmyCanFitUnit(army, merc.Unit))
                    {
                        army.Units.Add(merc.Unit);
                        merc.Unit.Side = army.Side;
                        MercenaryHouse.UniqueMercs.Remove(merc);
                        break;
                    }
                }

            }
        }


        for (int i = 0; i < idealArmySize; i++)
        {
            ArmyCommander.RecruitUnitAndEquip(army, village, tier);
        }

        return army;
    }


    private void BuyGarrisonWeapons(int i)
    {
        if (State.World.Villages[i].Side == AISide)
        {
            StrategicUtilities.BuyBasicWeapons(State.World.Villages[i]);
        }
    }


    Village GetBestVillageToMakeArmy(Empire empire)
    {
        int index = -1;
        int bestScore = -80;
        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (State.World.Villages[i].Side == empire.Side)
            {
                bool occupied = StrategicUtilities.IsVillageOccupied(empire, i);
                if (occupied == false)
                {
                    int score = VillageArmyCreationPriority(empire, State.World.Villages[i]);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        index = i;
                    }
                }
            }
        }

        return index > -1 ? State.World.Villages[index] : null;
    }

    int VillageArmyCreationPriority(Empire empire, Village village)
    {
        int baseLevel = Unit.GetLevelFromExperience(empire.StartingXP);
        double expBonus = 0;
        if (village.GetRecruitables().Count > 0)
        {
            Unit unit;
            int possibles = Math.Min(Math.Min(village.GetRecruitables().Count, idealArmySize), village.GetTotalPop() - 3);
            for (int i = 0; i < possibles; i++)
            {
                unit = village.GetRecruitables()[i];
                if (unit.Level > baseLevel)
                    expBonus += Math.Pow(unit.Level, 1.2f);
            }
        }
        int villageSizePenalty; //avoid placing armies where the population is low or the entire army can't be bought.
        int hostileArmyPriority = 0;
        int hostileVillagePriority = 0;
        int friendlyArmyPriority = 0;

        int surplusVillagers = village.GetTotalPop() + 3 - idealArmySize;

        if (surplusVillagers > 0)
            villageSizePenalty = -1 * surplusVillagers / 2;
        else
            villageSizePenalty = -10 * surplusVillagers;

        foreach (Army enemyArmy in StrategicUtilities.GetAllHostileArmies(empire))
        {
            int d = village.Position.GetNumberOfMovesDistance(enemyArmy.Position);
            if (d < 12)
                hostileArmyPriority += (24 - 2 * d);
        }

        int distance = 99;
        foreach (Village enemyVillage in StrategicUtilities.GetAllHostileVillages(empire))
        {
            int d = village.Position.GetNumberOfMovesDistance(enemyVillage.Position);
            if (d <= distance)
                distance = d;
        }
        if (distance <= 16)
            hostileVillagePriority += (30 - distance);
        else if (distance < 32)
            hostileVillagePriority += 3;
        else if (distance < 64)
            hostileVillagePriority += 2;
        else
            hostileVillagePriority += 1;

        foreach (Army army in empire.Armies)
        {
            int d = village.Position.GetNumberOfMovesDistance(army.Position);
            if (d < 8)
                friendlyArmyPriority += (16 - d * 2);
        }

        int offRacePenalty = 0;
        if (village.Race != empire.ReplacedRace)
            offRacePenalty = 5;

        return hostileArmyPriority + hostileVillagePriority + friendlyArmyPriority - villageSizePenalty - offRacePenalty + (int)(expBonus / 4);
    }


}
