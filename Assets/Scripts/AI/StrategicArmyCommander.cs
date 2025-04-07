using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum AIMode
{
    Default,
    Sneak,
    Heal,
    Resupply,
    //HuntStrong,
    //HeavyTraining
}


class StrategicArmyCommander
{
    Village[] Villages => State.World.Villages;
    Empire empire;
    int maxArmySize;

    bool smarterAI;

    List<PathNode> path;
    Army pathIsFor;

    internal float StrongestArmyRatio { get; private set; }

    int AISide => empire.Side;

    public StrategicArmyCommander(Empire empire, int maxSize, bool smarterAI)
    {
        this.empire = empire;
        maxArmySize = maxSize;
        this.smarterAI = smarterAI;
    }

    internal void UpdateStrongestArmyRatio()
    {
        if (empire.Armies.Count < 2)
            return;
        var strongestArmy = empire.Armies.OrderByDescending(s => s.ArmyPower).FirstOrDefault();
        if (strongestArmy == null) return;

        var strongestEnemy = StrategicUtilities.GetAllHostileArmies(empire).OrderByDescending(s => s.ArmyPower).FirstOrDefault();
        if (strongestEnemy != null)
        {
            StrongestArmyRatio = (float)(strongestArmy.ArmyPower / strongestEnemy.ArmyPower);
        }
    }

    internal void ResetPath() => pathIsFor = null; //If there is only one army, this forces it to generate a new path each turn

    internal bool GiveOrder()
    {
        foreach (Army army in empire.Armies.ToList())
        {
            if (army.RemainingMP < 1)
                continue;
            DevourCheck(army);
            if (army.RemainingMP < 1)
                continue;
            if (path != null && pathIsFor == army)
            {
                if (army.InVillageIndex > -1)
                {
                    UpdateEquipmentAndRecruit(army);
                }
                if (path.Count == 0)
                {
                    GenerateTaskForArmy(army);
                    return true;
                }

                Vec2i newLoc = new Vec2i(path[0].X, path[0].Y);
                Vec2i position = army.Position;
#if UNITY_EDITOR
                if (newLoc.GetNumberOfMovesDistance(position) != 1)
                {
                    UnityEngine.Debug.LogWarning($"Army tried to move from {position.x} {position.y} to {newLoc.x} {newLoc.y}");
                }

#endif
                path.RemoveAt(0);

                if (army.MoveTo(newLoc))
                    StrategicUtilities.StartBattle(army);
                else if (position == army.Position)
                {
                    army.RemainingMP = 0; //This prevents the army from wasting time trying to move into a forest with 1 mp repeatedly
                }
                return true;

            }
            else
            {
                //Teleport army if target is Ancient Teleporter to a random one
                List<AncientTeleporter> target_Tele = StrategicUtilities.GetAncientTeleportersWithinXTiles(army.Position,1);
                if (target_Tele != null)
                {
                    Debug.Log(State.World.AncientTeleporters.Count());
                    if (target_Tele.Count() >= 1)
                    {
                        if (StrategicUtilities.GetAllyArmyWithinXTiles(target_Tele.First().Position, 1, army.Empire).ToList().Contains(army))
                        {
                            List<AncientTeleporter> tp = State.World.AncientTeleporters.Where((t) => t != target_Tele.First()).ToList();
                            army.SetPosition(tp[State.Rand.Next(tp.Count())].Position);
                            army.Destination = null;
                        }
                    }

                }
                GenerateTaskForArmy(army);
                if (path == null || path.Count == 0)
                    army.RemainingMP = 0;
                return true;
            }
        }
        SpendExpAndRecruit(); //At the end of the turn, restock troops
        return false;
    }

    internal void SpendExpAndRecruit()
    {
        foreach (Army army in empire.Armies)
        {
            var infiltrators = new List<Unit>();
            foreach (Unit unit in army.Units)
            {
                StrategicUtilities.SpendLevelUps(unit);
                if (unit.HasTrait(Traits.Infiltrator) && unit.Type != UnitType.Leader && unit.FixedSide == army.Side)
                    infiltrators.Add(unit);
            }
            infiltrators.ForEach(u => StrategicUtilities.TryInfiltrateRandom(army, u));

            if (army.InVillageIndex > -1)
            {
                UpdateEquipmentAndRecruit(army);
            }
            if (army.AIMode == AIMode.Resupply && army.MostlyFull && StrategicUtilities.NumberOfDesiredUpgrades(army) == 0)
                army.AIMode = AIMode.Default;
        }
    }




    private void GenerateTaskForArmy(Army army)
    {
        path = null;
        pathIsFor = army;
        UpdateArmyStatus(army);
        switch (army.AIMode)
        {
            case AIMode.Default:
                Attack(army, 1.6f);
                break;
            case AIMode.Sneak:
                Attack(army, 0.8f);
                break;
            case AIMode.Heal:
                if (army.InVillageIndex == -1)
                {
                    if (army.LinkedTeleporter != null)
                    {
                        if (army.teleportStoneCoolDown <= 0)
                        {
                            if (army.LinkedTeleporter.CanTeleportArmy(army, true))
                            {
                                army.SetPosition(army.LinkedTeleporter.Position);
                                army.Destination = null;
                                army.teleportStoneCoolDown = 3;
                            }
                        }
                    }
                    if (NavigateToFriendlyVillage(army, false))
                        break;
                    Attack(army, 1);
                    break;
                }
                else
                {
                    DevourCheck(army);
                    army.RemainingMP = 0;
                    break;
                }
            case AIMode.Resupply:
                if (empire.Income < 20)
                    army.AIMode = AIMode.Default;

                Village villageArmyIsIn = null;
                if (army.InVillageIndex != -1)
                {
                    villageArmyIsIn = State.World.Villages[army.InVillageIndex];
                }


                if (army.InVillageIndex != -1 && empire.Gold > 4500 && Config.AICanHireSpecialMercs && MercenaryHouse.UniqueMercs.Count > 0 && !army.MostlyFull && NavigateToMercenaries(army, (int)(3f * army.GetMaxMovement())))
                {
                    break;
                }

                if (army.InVillageIndex != -1 && empire.Gold > 1500 && !army.MostlyFull && NavigateToMercenaries(army, (int)(2f * army.GetMaxMovement())))
                {
                    break;
                }


                if (army.InVillageIndex != -1 && empire.Gold > 500 && !army.MostlyFull && NavigateToMercenaries(army, (int)(1f * army.GetMaxMovement())))
                {
                    break;
                }

                MercenaryHouse mercHouseArmyIsIn = StrategicUtilities.GetMercenaryHouseAt(army.Position);
                if (mercHouseArmyIsIn != null)
                {
                    if (Config.AICanHireSpecialMercs)
                    {
                        foreach (var mercRaw in MercenaryHouse.UniqueMercs.OrderByDescending(s => s.Cost))
                        {

                            MercenaryContainer merc = new MercenaryContainer();
                            merc.Unit = mercRaw.Unit;
                            merc.Title = mercRaw.Title;
                            merc.Cost = mercRaw.Cost - (int)Math.Round(mercRaw.Cost * (0.1f * AcademyResearch.GetValueFromEmpire(empire, AcademyResearchType.MercRecruitCost)));
                            HireSpecialMerc(army, merc);
                        }
                    }
                    foreach (var mercRaw in mercHouseArmyIsIn.Mercenaries.OrderByDescending(s => s.Unit.Experience / s.Cost))
                    {
                        MercenaryContainer merc = new MercenaryContainer();
                        merc.Unit = mercRaw.Unit;
                        merc.Title = mercRaw.Title;
                        merc.Cost = mercRaw.Cost - (int)Math.Round(mercRaw.Cost * (0.1f * AcademyResearch.GetValueFromEmpire(empire, AcademyResearchType.MercRecruitCost)));
                        HireMerc(army, mercHouseArmyIsIn, merc);
                    }
                }

                if (villageArmyIsIn == null || villageArmyIsIn.GetTotalPop() < 12)
                {
                    if (NavigateToFriendlyVillage(army, !army.MostlyFull))
                        break;
                    Attack(army, 1);
                    break;
                }
                UpdateEquipmentAndRecruit(army);
                if (army.MostlyFull && StrategicUtilities.NumberOfDesiredUpgrades(army) == 0)
                    army.AIMode = AIMode.Default;
                else
                    army.RemainingMP = 0;
                break;
                //       case AIMode.HuntStrong:
                //           {
                //               AttackStrongestArmy(army);
                //               break;
                //           }
                //       case AIMode.HeavyTraining:
                //           {
                //if (army.InVillageIndex > -1)
                //{
                //	Village trainingVillage = State.World.Villages[army.InVillageIndex];
                //	var maxTrainLevel = trainingVillage.NetBoosts.MaximumTrainingLevelAdd;
                //	if (trainingVillage != null && maxTrainLevel > 0)
                //	{
                //		Train(army);
                //		double highestPower = 0;
                //		foreach (Army hostileArmy in StrategicUtilities.GetAllHostileArmies(AITeam))
                //		{
                //			double p = hostileArmy.ArmyPower;
                //			if (p > highestPower)
                //			{
                //				highestPower = p;
                //			}

                //		}
                //		if (highestPower * 1.3f + 256 < army.ArmyPower)
                //			army.AIMode = AIMode.HuntStrong;
                //	}
                //	else
                //		NavigateToTrainArmy(army);
                //}
                //               break;
                //           }
        }
    }

    void HireSpecialMerc(Army army, MercenaryContainer merc)
    {
        if (empire.Gold >= merc.Cost * 2)
        {
            if (StrategicUtilities.ArmyCanFitUnit(army, merc.Unit))
            {
                army.Units.Add(merc.Unit);
                merc.Unit.Side = army.Side;
                empire.SpendGold(merc.Cost);
                MercenaryHouse.UniqueMercs.Remove(merc);
                army.RecalculateSizeValue();
            }
        }
    }

    void HireMerc(Army army, MercenaryHouse house, MercenaryContainer merc)
    {
        if (empire.Gold >= merc.Cost)
        {
            if (StrategicUtilities.ArmyCanFitUnit(army, merc.Unit))
            {
                army.Units.Add(merc.Unit);
                merc.Unit.Side = army.Side;
                empire.SpendGold(merc.Cost);
                house.Mercenaries.Remove(merc);
                MercenaryHouse.UniqueMercs.Remove(merc);
                army.RecalculateSizeValue();
            }
        }
    }

    //void Train(Army army)
    //{
    //    if (army.RemainingMP > 0)
    //    {
    //        for (int i = 5 - 1; i >= 0; i--)
    //        {
    //var trainingCost = army.TrainingGetCost(i);
    //            if (empire.Gold > trainingCost)
    //            {
    //                State.World.Stats.SpentGoldOnArmyTraining(trainingCost, empire.Side);
    //                army.Train(i);
    //                army.RemainingMP = 0;
    //                return;
    //            }
    //        }

    //    }
    //}

    //void NavigateToTrainArmy(Army army)
    //{
    //    Vec2i[] locations = State.World.Villages.Where(s => s.Side == army.Side && s.NetBoosts.MaximumTrainingLevelAdd > 0).Select(s => s.Position).ToArray();
    //    if (locations != null && locations.Length > 0)
    //    {
    //        SetClosestPath(army, locations);
    //    }
    //    else
    //    {
    //        army.AIMode = AIMode.Sneak;
    //        return;
    //    }


    //}

    //void AttackStrongestArmy(Army army)
    //{
    //    Vec2i targetPosition = null;
    //    double highestPower = 0;
    //    foreach (Army hostileArmy in StrategicUtilities.GetAllHostileArmies(AITeam))
    //    {
    //        double p = hostileArmy.ArmyPower;
    //        if (p > highestPower)
    //        {
    //            highestPower = p;
    //            targetPosition = hostileArmy.Position;
    //        }
    //        if (p > army.ArmyPower * 1.1f)
    //            army.AIMode = AIMode.HeavyTraining;
    //    }

    //    SetPath(army, targetPosition);
    //}

    void Attack(Army army, float MaxDefenderStrength)
    {
        foreach (Army hostileArmy in StrategicUtilities.GetAllHostileArmies(empire).Where(s => s.ArmyPower > 2 * army.ArmyPower).Where(s => s.Position.GetNumberOfMovesDistance(army.Position) < 4 && !s.Units.All(u => u.HasTrait(Traits.Infiltrator))))
        {
            Vec2i[] closeVillagePositions = Villages.Where(s => s.Position.GetNumberOfMovesDistance(army.Position) < 7 && StrategicUtilities.ArmyAt(s.Position) == null).Select(s => s.Position).ToArray();
            if (closeVillagePositions != null && closeVillagePositions.Length > 0)
            {
                int oldMp = army.RemainingMP; //If there's no close town, then ignore it, instead of eating remaining MP
                SetClosestPath(army, closeVillagePositions, 6);
                if (path != null)
                    return;
                army.RemainingMP = oldMp;
            }

        }

        Vec2i capitalPosition = empire.CapitalCity?.Position ?? army.Position; //Shouldn't really ever be null, but just in case

        List<Vec2i> potentialTargets = new List<Vec2i>();
        List<int> potentialTargetValue = new List<int>();

        for (int i = 0; i < State.World.Villages.Length; i++)
        {
            if (Villages[i].Empire.IsEnemy(empire))
            {
                if (StrategicUtilities.TileThreat(Villages[i].Position) < MaxDefenderStrength * StrategicUtilities.ArmyPower(army))
                {
                    potentialTargets.Add(Villages[i].Position);
                    int value = Villages[i].Race == empire.ReplacedRace ? 45 : ((State.World.GetEmpireOfRace(Villages[i].Race)?.IsAlly(empire) ?? false) ? 40 : 35);
                    if (Villages[i].GetTotalPop() == 0)
                        value = 30;
                    value -= Villages[i].Position.GetNumberOfMovesDistance(capitalPosition) / 3;
                    potentialTargetValue.Add(value);
                }
            }
        }

        foreach (Army hostileArmy in StrategicUtilities.GetAllHostileArmies(empire))
        {
            if (!hostileArmy.Units.All(u => u.HasTrait(Traits.Infiltrator)) && StrategicUtilities.ArmyPower(hostileArmy) < MaxDefenderStrength * StrategicUtilities.ArmyPower(army) && hostileArmy.InVillageIndex == -1)
            {
                potentialTargets.Add(hostileArmy.Position);
                if (hostileArmy.Side >= 100 || hostileArmy.Side == (int)Race.Goblins) //If Monster
                    potentialTargetValue.Add(12);
                else
                    potentialTargetValue.Add(42 - hostileArmy.Position.GetNumberOfMovesDistance(capitalPosition) / 3);
            }
        }

        foreach (ClaimableBuilding claimable in State.World.Claimables)
        {
            if (claimable.Owner == null || empire.IsEnemy(claimable.Owner))
            {
                Army defender = StrategicUtilities.ArmyAt(claimable.Position);
                if (defender != null && StrategicUtilities.ArmyPower(defender) > MaxDefenderStrength * StrategicUtilities.ArmyPower(army))
                    continue;
                potentialTargets.Add(claimable.Position);
                int value = 38;
                value -= claimable.Position.GetNumberOfMovesDistance(capitalPosition) / 3;
                potentialTargetValue.Add(value);
            }
        }

        //Here for maps that are linked via teleporter or something, idk, just need AI to be able to use it, even it priority is low
        foreach (AncientTeleporter tele in StrategicUtilities.GetUnoccupiedAncientTeleporter(army.Empire))
        {
            Army defender = StrategicUtilities.ArmyAt(tele.Position);
            if (defender != null && StrategicUtilities.ArmyPower(defender) > MaxDefenderStrength * StrategicUtilities.ArmyPower(army))
                continue;
            potentialTargets.Add(tele.Position);
            int value = 18;
            value -= tele.Position.GetNumberOfMovesDistance(capitalPosition) / 3;
            potentialTargetValue.Add(value);
        }

        foreach (ConstructibleBuilding construct in State.World.Constructibles)
        {
            if (construct.Owner == null || (empire.IsEnemy(construct.Owner) && !construct.ruined && Config.BuildConfig.EmpireBuildingCapture != 0))
            {
                Army defender = StrategicUtilities.ArmyAt(construct.Position);
                if (defender != null && StrategicUtilities.ArmyPower(defender) > MaxDefenderStrength * StrategicUtilities.ArmyPower(army))
                    continue;
                potentialTargets.Add(construct.Position);
                int value = 38;
                value -= construct.Position.GetNumberOfMovesDistance(capitalPosition) / 3;
                // Stay on building to trigger capture effect
                if (construct.Position == army.Position)
                {
                    value = 100;
                }
                potentialTargetValue.Add(value);
            }

            // ReEnable disabled buildings if nearby
            if (construct.Owner == empire && construct.ruined)
            {
                potentialTargets.Add(construct.Position);
                int value = 38;
                value -= construct.Position.GetNumberOfMovesDistance(capitalPosition) / 3;
                potentialTargetValue.Add(value);
            }

            if (construct.Owner == empire && construct is Teleporter && construct.active && army.teleportCoolDown <= 0)
            {
                //Needs to have another teleporter to be a valid target
                if ((((Teleporter)construct).ancientUpgrade.built && State.World.AncientTeleporters.Count() >= 1)|| empire.Buildings.Where(b => b is Teleporter).Count() >= 2)
                {
                    //Target Teleport needs to also have enough capacity to take army if ancient teleporting is not acceptable
                    if ((((Teleporter)construct).ancientUpgrade.built || State.World.AncientTeleporters.Count() == 0) && empire.Buildings.Where(b => b is Teleporter && ((Teleporter)b).CanTeleportArmy(army)).Count() >= 1)
                    {
                        potentialTargets.Add(construct.Position);
                        int value = 25;
                        value += (army.LinkedTeleporter == null & ((Teleporter)construct).stoneUpgrade.built) ? 5 : 0;
                        value += (((Teleporter)construct).ancientUpgrade.built) ? 5 : 0;
                        value -= construct.Position.GetNumberOfMovesDistance(capitalPosition) / 3;
                        potentialTargetValue.Add(value);
                    }
                }
            }
        }

        SetClosestPathWithPriority(army, potentialTargets.ToArray(), potentialTargetValue.ToArray());
    }



    void UpdateArmyStatus(Army army)
    {
        var healthPct = army.GetHealthPercentage();
        if (healthPct < 60)
            army.AIMode = AIMode.Heal;

        if (army.InVillageIndex != -1 && healthPct < 80)
            army.AIMode = AIMode.Heal;

        if (army.AIMode == AIMode.Heal && healthPct > 95)
            if ((army.InVillageIndex > -1 && StrategicUtilities.NumberOfDesiredUpgrades(army) > 0) == false)
                army.AIMode = AIMode.Default;

        float need = 32 * (1-army.PercentFull) + StrategicUtilities.NumberOfDesiredUpgrades(army);
        if (need > 4 && empire.Gold >= 40 && empire.Income > 25)
        {
            var path = StrategyPathfinder.GetPathToClosestObject(empire, army, Villages.Where(s => s.Side == army.Side).Select(s => s.Position).ToArray(), army.RemainingMP, 5, army.movementMode == Army.MovementMode.Flight);
            if (path != null && path.Count() < need / 2)
                army.AIMode = AIMode.Resupply;

        }


    }

    bool NavigateToMercenaries(Army army, int maxRange)
    {
        Vec2i[] mercPositions = StrategicUtilities.GetUnoccupiedMercCamp(empire).Select(s => s.Position).ToArray();
        if (mercPositions == null || mercPositions.Count() < 1)
        {
            return false;
        }
        else
        {
            SetClosestPath(army, mercPositions, maxRange);
            if (path == null)
                return false;
            return true;
        }

    }


    bool NavigateToFriendlyVillage(Army army, bool canRecruitFrom)
    {
        Vec2i[] villagePositions = StrategicUtilities.GetUnoccupiedFriendlyVillages(empire).Select(s => s.Position).ToArray();
        if (villagePositions == null || villagePositions.Count() < 1)
        {
            return false;
        }
        else
        {
            SetClosestPath(army, villagePositions);
            return true;
        }

    }

    void DevourCheck(Army army)
    {
        if (army.GetHealthPercentage() > 88)
            return;
        if (army.Units.Where(s => s.Predator && 100 * s.HealthPct < 70).Any() == false)
            return;
        if (army.RemainingMP < 1)
            return;
        if (army.InVillageIndex > -1)
        {
            Village village = State.World.Villages[army.InVillageIndex];
            int range;
            int minimumheal;
            //Could check the relative strength but it's probably fine for now.
            if (village.Empire.IsAlly(empire))
            {
                minimumheal = 7;
                range = 8;
                if (village.GetTotalPop() < 22)
                {
                    minimumheal = 9;
                }
            }
            else
            {
                minimumheal = 2;
                range = 20;
            }

            if (StrategicUtilities.EnemyArmyWithinXTiles(army, range))
            {
                State.GameManager.StrategyMode.Devour(army, Math.Min(army.GetDevourmentCapacity(minimumheal), village.GetTotalPop() - 3)); //Don't completely devour villages
            }

        }
    }

    private void UpdateEquipmentAndRecruit(Army army)
    {
        Village village = State.World.Villages[army.InVillageIndex];
        army.ItemStock.SellAllWeaponsAndAccessories(empire);
        StrategicUtilities.UpgradeUnitsIfAtLeastLevel(army, village, 4);
        if (!army.MostlyFull)
        {
            int goldPerTroop = (int)(empire.Gold / (army.Units.Count() * army.GetAverageArmyDeployment()));
            while (!army.MostlyFull)
            {
                army.RecalculateSizeValue();
                Unit newUnit = null;
                if (smarterAI && empire.Gold > 40)
                    newUnit = RecruitUnitAndEquip(army, village, 2);
                else if (goldPerTroop > 40 && village.GetTotalPop() > 3 && empire.Income > 15)
                    newUnit = RecruitUnitAndEquip(army, village, 2);
                else if (empire.Gold > 16 && village.GetTotalPop() > 3 && empire.Income > 5)
                    newUnit = RecruitUnitAndEquip(army, village, 1);
                else
                    break;
                if (newUnit == null)
                {
                    break;
                }
            }
            if (army.AIMode == AIMode.Resupply && army.MostlyFull)
                army.AIMode = AIMode.Default;

        }
    }


    internal Unit RecruitUnitAndEquip(Army army, Village village, int tier)
    {
        if (village.GetTotalPop() < 4)
            return null;
        if (army.RemainnigSize <= 0)
            return null;
        if (empire.Leader?.Health <= 0)
            return ResurrectLeader(army, village);
        if (tier == 2 && empire.Gold < Config.ArmyCost + State.World.ItemRepository.GetItem(ItemType.CompoundBow).Cost)
            return null;
        if (tier == 1 && empire.Gold < Config.ArmyCost + State.World.ItemRepository.GetItem(ItemType.Bow).Cost)
            return null;
        Unit unit = village.RecruitAIUnit(empire, army);
        if (unit == null) //Catches army size
            return null;
        if (unit.HasTrait(Traits.Infiltrator) && !unit.IsInfiltratingSide(unit.Side))
        {
            unit.OnDiscard = () =>
            {
                village.VillagePopulation.AddHireable(unit);
                UnityEngine.Debug.Log(unit.Name + " is returning to " +  village.Name);
            };
        }
        if (unit.FixedGear == false)
        {
            if (unit.HasTrait(Traits.Feral))
            {
                Shop.BuyItem(empire, unit, State.World.ItemRepository.GetItem(ItemType.Gauntlet));
            }
            else if (unit.Items[0] == null)
            {
                if (tier == 1)
                {
                    if (unit.BestSuitedForRanged())
                        Shop.BuyItem(empire, unit, State.World.ItemRepository.GetItem(ItemType.Bow));
                    else
                        Shop.BuyItem(empire, unit, State.World.ItemRepository.GetItem(ItemType.Mace));
                }
                else if (tier == 2)
                {
                    if (unit.BestSuitedForRanged())
                        Shop.BuyItem(empire, unit, State.World.ItemRepository.GetItem(ItemType.CompoundBow));
                    else
                        Shop.BuyItem(empire, unit, State.World.ItemRepository.GetItem(ItemType.Axe));
                }
            }
        }

        StrategicUtilities.SetAIClass(unit, .1f);

        StrategicUtilities.SpendLevelUps(unit);
        army.RefreshMovementMode();
        return unit;
    }

    Unit ResurrectLeader(Army army, Village village)
    {
        empire.SpendGold(100);
        empire.Leader.Side = AISide;
        empire.Leader.FixedSide = AISide;
        empire.Leader.Type = UnitType.Leader;
        empire.Leader.LeaderLevelDown();
        empire.Leader.Health = empire.Leader.MaxHealth;
        if (village.GetStartingXp() > empire.Leader.Experience)
        {
            empire.Leader.SetExp(village.GetStartingXp());
            StrategicUtilities.SpendLevelUps(empire.Leader);

        }
        army.Units.Add(empire.Leader);
        army.RefreshMovementMode();
        State.World.Stats.ResurrectedLeader(empire.Side);
        if (Config.LeadersRerandomizeOnDeath)
        {
            empire.Leader.TotalRandomizeAppearance();
            empire.Leader.ReloadTraits();
            empire.Leader.InitializeTraits();
        }

        return empire.Leader;
    }

    private void SetPath(Army army, Vec2i targetPosition, int maxDistance)
    {
        if (targetPosition != null)
        {
            path = StrategyPathfinder.GetArmyPath(empire, army, targetPosition, army.RemainingMP, army.movementMode == Army.MovementMode.Flight, maxDistance);
            return;
        }
        army.RemainingMP = 0;
    }

    private void SetClosestPath(Army army, Vec2i[] targetPositions, int maxDistance = 999)
    {
        if (targetPositions != null && targetPositions.Length > 1)
        {
            path = StrategyPathfinder.GetPathToClosestObject(empire, army, targetPositions, army.RemainingMP, maxDistance, army.movementMode == Army.MovementMode.Flight);
            return;
        }
        else if (targetPositions.Length == 1)
        {
            SetPath(army, targetPositions[0], maxDistance);
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
            SetPath(army, targetPositions[0], maxDistance);
        }
        else
            army.RemainingMP = 0;
    }

}

