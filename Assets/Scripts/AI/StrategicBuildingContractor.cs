using System;
using System.Collections.Generic;
using System.Linq;

class StrategicBuildingContractor
{
    Empire empire;
    int goldBank;

    BuildingUpgrade upgradeWanted;
    ConstructibleBuilding upgradeBuilding;
    ConstructibleType buildingWanted;
    Dictionary<ConstructibleType, GeneralBuildingConfig> buildingInfo;
    ConstructionResources resourceWanted;
    ConstructibleBuilding activeBuilding;

    int switchTimer;
    float switchChance => switchTimer * (-0.1f);
    int activeConstructions => empire.Buildings.Where(b => b.active).Count();

    public StrategicBuildingContractor(Empire empire)
    {
        this.empire = empire;
        goldBank = 0;

        buildingInfo = new Dictionary<ConstructibleType, GeneralBuildingConfig>
        {
            [ConstructibleType.WorkCamp] = Config.BuildConfig.WorkCamp,
            [ConstructibleType.LumberSite] = Config.BuildConfig.LumberSite,
            [ConstructibleType.Quarry] = Config.BuildConfig.Quarry,
            [ConstructibleType.CasterTower] = Config.BuildConfig.CasterTower,
            [ConstructibleType.BarrierTower] = Config.BuildConfig.BarrierTower,
            [ConstructibleType.DefEncampment] = Config.BuildConfig.DefenseEncampment,
            [ConstructibleType.Academy] = Config.BuildConfig.Academy,
            [ConstructibleType.DarkMagicTower] = Config.BuildConfig.DarkMagicTower,
            [ConstructibleType.TemporalTower] = Config.BuildConfig.TemporalTower,
            [ConstructibleType.Laboratory] = Config.BuildConfig.Laboratory,
            [ConstructibleType.Teleporter] = Config.BuildConfig.Teleporter,
            [ConstructibleType.TownHall] = Config.BuildConfig.TownHall,
        };

        GetNewProject();
    }

    internal bool AssessBuildStatus()
    {
        //Debug.Log(goldBank);

        RunGoldStashing();
        RunAllBuildingActives();
        CheckForSwitch();
        if (activeConstructions >= 3)
        {
            return true;
        }
        if (upgradeWanted != null)
        {
            BuildUpgrades();
        }
        if (buildingWanted >= 0 && State.World.Turn >= Config.BuildConfig.BuildingSystemTurnLockout && Config.BuildConfig.BuildingSystemEnabled)
        {
            Vec2i loc = LocateGoodLocation();
            if (loc != null)
            {
                ConstructBuilding(loc, buildingWanted);
            }
        }

        return true;
    }

    internal void RunGoldStashing()
    {
        // No need to bank, likely have other problems that require remaining gold
        if (empire.Income <= 0)
        {
            return;
        }
        // Stashes 0% to 30% of current gold
        int transact = (empire.Gold / 10) * State.Rand.Next(4);
        //Debug.Log($"Adding {transact} to bank.");
        TransactGold(transact);
    }


    internal Vec2i LocateGoodLocation()
    {
        if (empire.OwnedTiles == null || empire.OwnedTiles.Count <= 0)
        {
            return null;
        }
        List<Vec2i> locs = empire.OwnedTiles.Distinct().ToList();

        // player can't build on following, so nor should AI
        foreach (var vil in State.World.Villages)
        {
            locs.Remove(vil.Position);
        }
        foreach (var con in State.World.Constructibles)
        {
            locs.Remove(con.Position);
        }
        foreach (var cla in State.World.Claimables)
        {
            locs.Remove(cla.Position);
        }
        foreach (var tele in State.World.AncientTeleporters)
        {
            locs.Remove(tele.Position);
        }
        foreach (var mer in State.World.MercenaryHouses)
        {
            locs.Remove(mer.Position);
        }
        foreach (var loc in locs)
        {
            if (!StrategicTileInfo.CanWalkInto(State.World.Tiles[loc.x, loc.y]))
            {
                locs.Remove(loc);
            }
        }

        return locs[State.Rand.Next(locs.Count)];
    }

    internal void DecideBuilding()
    {
        Dictionary<ConstructibleType, GeneralBuildingConfig> AIBuilding = new Dictionary<ConstructibleType, GeneralBuildingConfig>();
        foreach (var item in buildingInfo)
        {
            if (item.Value.AICanBuild && empire.WithinBuildLimit(item.Key))
                AIBuilding.Add(item.Key, item.Value);
        }

        if (AIBuilding.Count <= 0)
        {
            // All buildings are off, or constructed, return banked gold so it can be used on other things
            TransactGold(goldBank * -1);
            return;
        }
        // Ensure generation buildings are constructed first
        if (!empire.Buildings.Where(b => b is WorkCamp).Any() && AIBuilding.Keys.Contains(ConstructibleType.WorkCamp))
        {
            buildingWanted = ConstructibleType.WorkCamp;
            return;
        }
        if (!empire.Buildings.Where(b => b is LumberSite).Any() && AIBuilding.Keys.Contains(ConstructibleType.LumberSite))
        {
            buildingWanted = ConstructibleType.LumberSite;
            return;

        }
        if (!empire.Buildings.Where(b => b is Quarry).Any() && AIBuilding.Keys.Contains(ConstructibleType.Quarry))
        {
            buildingWanted = ConstructibleType.Quarry;
            return;
        }
        //If producion is created, allow Any kind of building to be constructed
        buildingWanted = AIBuilding.ElementAt(State.Rand.Next(AIBuilding.Count)).Key;
    }

    internal void DecideUpgrade()
    {
        Dictionary<BuildingUpgrade, ConstructibleBuilding> upgradeDict = new Dictionary<BuildingUpgrade, ConstructibleBuilding>();
        foreach (var building in empire.Buildings)
        {
            foreach (var upgrade in building.Upgrades.Where(u => !u.built))
            {
                upgradeDict.Add(upgrade, building);
            }
        }
        int index = State.Rand.Next(0, upgradeDict.Count);
        upgradeWanted = upgradeDict.ElementAt(index).Key;
        upgradeBuilding = upgradeDict.ElementAt(index).Value;
        resourceWanted = upgradeWanted.ResourceToUpgrade;
    }

    internal void RunAllBuildingActives()
    {
        foreach (var building in empire.Buildings)
        {
            activeBuilding = building;
            switch (building.buildingType)
            {
                case ConstructibleType.WorkCamp:
                    RunWorkCamp();
                    break;
                case ConstructibleType.LumberSite:
                    RunLumberSite();
                    break;
                case ConstructibleType.Quarry:
                    RunQuarry();
                    break;
                case ConstructibleType.CasterTower:
                    RunCasterTower();
                    break;
                case ConstructibleType.BarrierTower:
                    RunBarrierTower();
                    break;
                case ConstructibleType.DefEncampment:
                    RunDefEncampment();
                    break;
                case ConstructibleType.Academy:
                    RunAcademy();
                    break;
                case ConstructibleType.DarkMagicTower:
                    RunDarkMagicTower();
                    break;
                case ConstructibleType.TemporalTower:
                    break;
                case ConstructibleType.Laboratory:
                    RunLaboratory();
                    break;
                case ConstructibleType.Teleporter:
                    RunTeleporter();
                    break;
                case ConstructibleType.TownHall:
                    break;
                default:
                    break;
            }
        }
    }

    internal void RunWorkCamp()
    {
        WorkCamp workCamp = activeBuilding as WorkCamp;
        // If we want an upgrade, buy resources
        if (resourceWanted != null)
        {
            // Bank a bit more gold to buy resources
            TransactGold(empire.Gold / 10);
            //get count of needed resource
            Dictionary<ConstructionResourceType, int> neededResource = empire.constructionResources.NeededResources(resourceWanted);
            // get how many we generate per turn
            Dictionary<ConstructionResourceType, int> generatedResource = GetGeneration();
            foreach (var item in neededResource)
            {
                if (!workCamp.postUpgrade.built && (item.Key == ConstructionResourceType.wood || item.Key == ConstructionResourceType.stone))
                {
                    continue;
                }
                if (!workCamp.merchantUpgrade.built && (item.Key == ConstructionResourceType.naturalmaterials || item.Key == ConstructionResourceType.ores|| item.Key == ConstructionResourceType.prefabs || item.Key == ConstructionResourceType.manastones))
                {
                    continue;
                }
                // skip buying if we reach goal with two turns of generation
                if (generatedResource[item.Key] * 2 >= neededResource[item.Key])
                {
                    continue;
                }
                int count = item.Value;
                // While we have enough gold, and item is in stock, and we still need the item
                while (goldBank > Config.BuildConfig.WorkCampItemPrice.ResourceCountFromType(item.Key) && AIBuildingUtilityFunctions.WorkCampUtility.PurchaseResource(item.Key, workCamp) && count >= 1)
                {
                    count--;
                }
            }
        }
    }
    internal void RunLumberSite()
    {
        LumberSite lumberSite = activeBuilding as LumberSite;
        int availWorker = Config.BuildConfig.LumberSiteWorkerCap * (lumberSite.lodgeUpgrade.built ? 2 : 1);
        //set an even spread for each availible worker
        if (lumberSite.greenHouseUpgrade.built)
        {
            lumberSite.natureWorkers = availWorker/3;
        }
        if (lumberSite.carpenterUpgrade.built)
        {
            lumberSite.carpenterWorkers = availWorker / 6;
        }
        lumberSite.woodWorkers = availWorker/3;

    }
    internal void RunQuarry()
    {
        Quarry quarry = activeBuilding as Quarry;
        int plan = State.Rand.Next(2);
        if (quarry.improveUpgrade.built)
        {
            plan = State.Rand.Next(4);
        }
        if (!quarry.leyUpgrade.built && !quarry.deepUpgrade.built && plan == 2)
        {
            plan = 1;
        }
        quarry.ActionPlan = plan;
    }
    internal void RunCasterTower()
    {
        CasterTower casterTower = activeBuilding as CasterTower;       
    }
    internal void RunBarrierTower()
    {
        BarrierTower barrierTower = activeBuilding as BarrierTower;       
        barrierTower.BarrierMagnitude = State.Rand.Next(6);
        barrierTower.EmpowerMagnitude = barrierTower.buffUpgrade.built ? State.Rand.Next(6) : 0;
        barrierTower.MendingMagnitude = barrierTower.healUpgrade.built ? State.Rand.Next(6) :0;
    }
    internal void RunDefEncampment()
    {
        DefenseEncampment defenseEncampment = activeBuilding as DefenseEncampment;       
    }
    internal void RunAcademy()
    {
        Academy academy = activeBuilding as Academy;
        academy.DistributedEXP = academy.improveUpgrade.built ? 0.2f : 0.1f;
        academy.Income1 = empire.Income / 10;
        if (academy.Income1 > 100)
        {
            academy.Income1 = 100;
        }
        academy.Income2 = academy.improveUpgrade.built ? empire.Income / 10 : 0;
        if (academy.Income2 > 100)
        {
            academy.Income2 = 100;
        }
        if (academy.StoredEXP >= empire.AcademyUpgradeEXPCost)
        {
            AcademyResearchType type;
            type = (AcademyResearchType)State.Rand.Next((int)AcademyResearchType.ResearchTypeCounter);
            if (empire.AcademyResearchCompleted.Keys.Contains(type))
            {
                int counter = 0;
                while (empire.AcademyResearchCompleted[type] >= Config.BuildConfig.AcademyMaximumUpgrades)
                {                    
                    if (counter >= 50)
                    {
                        bool foundUnUpgraded = false;
                        // Go through all upgrades in order to try and find one not maxed
                        for (int i = 0; (int)AcademyResearchType.ResearchTypeCounter > i; i++)
                        {
                            if (empire.AcademyResearchCompleted.Keys.Contains((AcademyResearchType)i))
                            {
                                if (Config.BuildConfig.AcademyMaximumUpgrades >= empire.AcademyResearchCompleted[(AcademyResearchType)i] )
                                {
                                    type = (AcademyResearchType)i;
                                    foundUnUpgraded = true;
                                    break;
                                }
                            }
                            else
                            {
                                type = (AcademyResearchType)i;
                                foundUnUpgraded = true;
                                break;
                            }
                        }
                        if (!foundUnUpgraded)
                        {
                            //No need to store EXP for upgrades and should distribute as much as possible.
                            academy.DistributedEXP = academy.improveUpgrade.built ? 1f : 0.1f;
                        }
                        break;
                    }
                    type = (AcademyResearchType)State.Rand.Next((int)AcademyResearchType.ResearchTypeCounter);
                    counter++;
                }
            }
            empire.AcademyResearchCompleted[type] = empire.AcademyResearchCompleted[type] + 1;
            academy.StoredEXP -= empire.AcademyUpgradeEXPCost;
            empire.AcademyUpgradeEXPCost = (int)(empire.AcademyUpgradeEXPCost * Config.BuildConfig.AcademyCostIncreaseMultPerUpgrade);
        }
    }
    internal void RunDarkMagicTower()
    {
        BlackMagicTower darkMagicTower = activeBuilding as BlackMagicTower;
        if (darkMagicTower.PactLevel >= 9 && State.Rand.Next(3) == 0)
        {
            darkMagicTower.Affliction = StatusEffectType.Agony;
        }
        else if (darkMagicTower.PactLevel >= 6 && State.Rand.Next(3) == 0)
        {
            darkMagicTower.Affliction = StatusEffectType.Lethargy;
        }
        else if (darkMagicTower.PactLevel >= 3 && State.Rand.Next(3) == 0)
        {
            darkMagicTower.Affliction = StatusEffectType.Errosion;        
        }
        else
        {
            darkMagicTower.Affliction = StatusEffectType.Necrosis;
        }
    }
    internal void RunLaboratory()
    {
        Laboratory laboratory = activeBuilding as Laboratory;
        if (empire.Gold > Config.BuildConfig.LaboratoryBaseUnitPrice * 4 && State.Rand.Next(3) == 0) 
        {
            int budget = empire.Gold / 2;
            int potionCost = Config.BuildConfig.LaboratoryBaseUnitPrice;
            int ramainingPicks = State.Rand.Next(Config.BuildConfig.LaboratoryBaseRollCount * (laboratory.improveUpgrade.built ? 2 : 1)) + 1;
            double TraitChanceValue = Config.BuildConfig.LaboratoryBaseTraitChance;
            int baselineCost = Config.BuildConfig.LaboratoryBaseUnitPrice;
            LaboratoryPotion newPotion = new LaboratoryPotion();
            int[] PotionRollStats = new int[9];        
            
            while (ramainingPicks > 0)
            {
                int roll1 = State.Rand.Next(laboratory.ingredientUpgrade.built ? (int)PotionIngredient.PotionIngredientCounter : (int)PotionIngredient.Powerful);
                int roll2 = State.Rand.Next(laboratory.ingredientUpgrade.built ? (int)PotionIngredient.PotionIngredientCounter : (int)PotionIngredient.Powerful);
                PotionIngredient ingRoll = (PotionIngredient)Math.Max(roll1,roll2);
                double randRoll = State.Rand.NextDouble();
                switch (ingRoll)
                {
                    case PotionIngredient.Grievous:
                        potionCost += baselineCost / -2;
                        if (randRoll >= .5)
                            IncRollValue(0);
                        else if (randRoll >= .25)
                            IncRollValue(1);
                        else
                            IncRollValue(2);
                        break;
                    case PotionIngredient.Dangerous:
                        potionCost += (int)Math.Round(baselineCost / -1.5);
                        if (randRoll >= .5)
                            IncRollValue(1);
                        else if (randRoll >= .25)
                            IncRollValue(0);
                        else
                            IncRollValue(2);
                        break;
                    case PotionIngredient.Experimental:
                        if (randRoll >= .25)
                            IncRollValue(1);
                        else if (randRoll >= .5)
                            IncRollValue(2);
                        else if (randRoll >= .75)
                            IncRollValue(3);
                        else
                            IncRollValue(4);
                        break;
                    case PotionIngredient.Unstable:
                        potionCost += (int)Math.Round(baselineCost * .15);
                        if (randRoll >= .1)
                            IncRollValue(1);
                        else if (randRoll >= .2)
                            IncRollValue(2);
                        else if (randRoll >= .6)
                            IncRollValue(3);
                        else
                            IncRollValue(4);
                        break;
                    case PotionIngredient.Stable:
                        potionCost += (int)Math.Round(baselineCost * .3);
                        if (randRoll >= .1)
                            IncRollValue(2);
                        else if (randRoll >= .3)
                            IncRollValue(3);
                        else if (randRoll >= .7)
                            IncRollValue(4);
                        else
                            IncRollValue(5);
                        break;
                    case PotionIngredient.Simple:
                        potionCost += (int)Math.Round(baselineCost * .5);
                        if (randRoll >= .3)
                            IncRollValue(3);
                        else if (randRoll >= .6)
                            IncRollValue(4);
                        else
                            IncRollValue(5);
                        break;
                    case PotionIngredient.Standard:
                        potionCost += (int)Math.Round(baselineCost * .75);
                        if (randRoll >= .3)
                            IncRollValue(4);
                        else if (randRoll >= .7)
                            IncRollValue(5);
                        else if (randRoll >= .9)
                            IncRollValue(6);
                        else
                            IncRollValue(7);
                        break;
                    case PotionIngredient.Premium:
                        potionCost += baselineCost;
                        if (randRoll >= .2)
                            IncRollValue(5);
                        else if (randRoll >= .6)
                            IncRollValue(6);
                        else if (randRoll >= .9)
                            IncRollValue(7);
                        else
                            IncRollValue(8);
                        break;
                    case PotionIngredient.Superior:
                        potionCost += (int)Math.Round(baselineCost * 1.5);
                        if (randRoll >= .4)
                            IncRollValue(6);
                        else if (randRoll >= .7)
                            IncRollValue(7);
                        else
                            IncRollValue(8);
                        break;
                    case PotionIngredient.Powerful:
                        potionCost += baselineCost * 2;
                        if (randRoll >= .5)
                            IncRollValue(7);
                        else
                            IncRollValue(8);
                        break;
                    case PotionIngredient.Legendary:
                        potionCost += baselineCost * 2;
                        if (randRoll >= .3)
                            IncRollValue(7);
                        else
                            IncRollValue(8);
                        break;
                    case PotionIngredient.Sterilizing:
                        potionCost += (int)Math.Round(baselineCost * 0.5);
                        DeleteEffect(1);
                        break;
                    case PotionIngredient.Purifying:
                        potionCost += (int)Math.Round(baselineCost * 1.5);
                        DeleteEffect(0);
                        DeleteEffect(1);
                        break;
                    case PotionIngredient.Solute:
                        potionCost += (int)Math.Round(baselineCost * .25);
                        TraitChanceValue += 0.05;
                        TraitChanceValue++;
                        break;
                    case PotionIngredient.Solvent:
                        potionCost += (int)Math.Round(baselineCost * .05);
                        TraitChanceValue -= 0.1;
                        TraitChanceValue++;
                        break;
                    case PotionIngredient.Coagulate:
                        potionCost += (int)Math.Round(baselineCost * .5);
                        ramainingPicks += 2;
                        break;
                    default:
                        break;
                }
                if (potionCost >= budget)
                {
                    break;
                }
                ramainingPicks--;
            }

            for (int i = 0; i < PotionRollStats.Count(); i++)
            {
                int ingQuality = i;
                int trait_count = PotionRollStats[ingQuality];
                for (int j = 0; j < trait_count; j++)
                {
                    if (laboratory.boostUpgrade.built)
                    {
                        if (State.Rand.Next(4) == 0 && ingQuality <= 4)
                        {
                            ingQuality += 2;
                        }
                    }
                    if (State.Rand.NextDouble() > TraitChanceValue)
                    {
                        int value = State.Rand.Next(5, 10);
                        if (ingQuality <= 1)
                        {
                            newPotion.StatModifiers[(Stat)State.Rand.Next(8)] += ingQuality == 0 ? value * -2 : -value;
                        }
                        else if (ingQuality == 2)
                        {
                            newPotion.StatModifiers[(Stat)State.Rand.Next(8)] += value;
                            newPotion.StatModifiers[(Stat)State.Rand.Next(8)] -= value;
                        }
                        else
                        {
                            newPotion.StatModifiers[(Stat)State.Rand.Next(8)] += value * ingQuality;
                        }
                    }
                    else
                    {
                        Traits incTrait = TaggedTraitUtilities.GetRandomTraitInTier((TraitTier)ingQuality);
                        if (incTrait <= 0)
                        {
                            int value = State.Rand.Next(5, 10);
                            if (ingQuality <= 1)
                            {
                                newPotion.StatModifiers[(Stat)State.Rand.Next(8)] += ingQuality == 0 ? value * -2 : -value;
                            }
                            else if (ingQuality == 2)
                            {
                                newPotion.StatModifiers[(Stat)State.Rand.Next(8)] += value;
                                newPotion.StatModifiers[(Stat)State.Rand.Next(8)] -= value;
                            }
                            else
                            {
                                newPotion.StatModifiers[(Stat)State.Rand.Next(8)] += value * ingQuality;
                            }
                        }
                        if (ingQuality <= 2)
                        {
                            newPotion.NegativeTraits.Add(incTrait);
                        }
                        else
                        {
                            newPotion.PositiveTraits.Add(incTrait);
                        }
                    }
                }
            }

            // if we made a good potion, try to make as many as possible
            int good_picks = PotionRollStats[8] + PotionRollStats[8] + PotionRollStats[6] + PotionRollStats[5] + PotionRollStats[4] + PotionRollStats[3];
            int bad_picks = PotionRollStats[0] + PotionRollStats[1];
            if (good_picks > bad_picks)
            {
                empire.EmpirePotions.Add(newPotion, (int)Math.Floor((decimal)(budget / potionCost)) * Config.BuildConfig.LaboratoryAIPotionMult);
            }
            else
            {
                empire.EmpirePotions.Add(newPotion, 1 * Config.BuildConfig.LaboratoryAIPotionMult);
            }

            //Use potions on units
            List<Unit> empireUnits = new List<Unit>();
            foreach (var army in empire.Armies)
            {
                foreach (var unit in army.Units)
                    empireUnits.Add(unit);
            }
            foreach (var potionItem in empire.EmpirePotions)
            {
                var potion = potionItem.Key;
                for (int i = 0; i < potionItem.Value; i++)
                {
                    Unit unit = empireUnits[State.Rand.Next(empireUnits.Count())];
                    foreach (Traits trait in potion.PositiveTraits)
                    {
                        unit.AddTrait(trait);
                    }
                    foreach (Traits trait in potion.NegativeTraits)
                    {
                        unit.AddTrait(trait);
                    }
                    foreach (var statShift in potion.StatModifiers)
                    {
                        unit.SpecificStatIncrease(statShift.Value, (int)statShift.Key);
                    }
                }
                empire.EmpirePotions.Remove(potion);
            }

            //Helper functions
            void IncRollValue(int id)
            {
                PotionRollStats[id] = PotionRollStats[id] + 1;
            }
            void DeleteEffect(int id)
            {
                PotionRollStats[id] = 0;
            }
        }
        
    }
    internal void RunTeleporter()
    {
        Teleporter teleporter = activeBuilding as Teleporter;
        foreach (Army army in StrategicUtilities.GetOwnerArmyWithinXTiles(teleporter,1))
        {
            //link this army to teleporter if able
            if (teleporter.stoneUpgrade.built)
            {
                army.LinkedTeleporter = teleporter;
            }
            // Teleport to an ancient teleporter, will most likely give a more forward position.
            if (teleporter.ancientUpgrade.built && State.Rand.Next(4) != 0)
            {
                army.SetPosition(State.World.AncientTeleporters[State.Rand.Next(State.World.AncientTeleporters.Count())].Position);
                army.Destination = null;
                army.teleportCoolDown = 3;
            }
        }
        
    }

    /// <summary>
    /// Easy way to simplify contractor reserving gold.
    /// Adds gold to bank from empire's stash.
    /// Removes from stash and adds back to empire in some cases.
    /// </summary>
    internal void TransactGold(int gold)
    {
        goldBank += gold;
        empire.RemoveGold(gold);
    }

    /// <summary>
    /// Easy way to simplify contractor gold spending.
    /// Adds gold from bank to empire, then spend it properly.
    /// </summary>
    internal void SpendGold(int gold)
    {
        goldBank -= gold;
        empire.RemoveGold(gold * -1);
        empire.SpendGold(gold);
    }

    internal void CheckForSwitch()
    {
        switchTimer--;
        if (switchTimer > 0)
        {
            return;
        }
        if (ShouldSwitch())
        {
            GetNewProject();
        }
    }
    /// <summary>
    /// Switches the desired project if it gets stuck.
    /// </summary>
    internal bool ShouldSwitch()
    {
        if (State.Rand.NextDouble() > switchChance)
        {
            return false;
        }
        switchTimer = empire.Buildings.Count();
        return true;
    }

    internal void GetNewProject()
    {
        upgradeWanted = null;
        upgradeBuilding = null;
        buildingWanted = (ConstructibleType)(-1);
        activeBuilding = null;
        resourceWanted = null;
        switchTimer = 5;
        int buidlingsConstructed = empire.Buildings.Count;
        int upgradesNeeded = 0;
        foreach (var item in empire.Buildings)
        {
            upgradesNeeded += item.Upgrades.Where(u => !u.built).Count();
        }
        // chance upgrade is chosen increases the more upgrades there are to buildings
        double chanceForUpgrade = (upgradesNeeded / 10) / Math.Pow(buidlingsConstructed, 0.5f);
        if (chanceForUpgrade > State.Rand.NextDouble())
        {
            DecideUpgrade();
        }
        else
        {
            DecideBuilding();          
        }
    }
    /// <summary>
    /// Performs all required generation calculations
    /// </summary>
    internal Dictionary<ConstructionResourceType, int> GetGeneration()
    {
        Dictionary<ConstructionResourceType, int> perTurnGen = new Dictionary<ConstructionResourceType, int>
        {
            [ConstructionResourceType.wood] = 0,
            [ConstructionResourceType.stone] = 0,
            [ConstructionResourceType.naturalmaterials] = 0,
            [ConstructionResourceType.ores] = 0,
            [ConstructionResourceType.prefabs] = 0,
            [ConstructionResourceType.manastones] = 0,
        };

        foreach (var building in empire.Buildings)
        {
            switch (building.buildingType)
            {
                case ConstructibleType.WorkCamp:
                    WorkCamp camp = (WorkCamp)building;
                    perTurnGen[ConstructionResourceType.wood] += Config.BuildConfig.WorkCampGenerationPerTurn;
                    perTurnGen[ConstructionResourceType.stone] += Config.BuildConfig.WorkCampGenerationPerTurn;
                    break;
                case ConstructibleType.LumberSite:
                    LumberSite site = (LumberSite)building;
                    perTurnGen[ConstructionResourceType.wood] += site.woodWorkers;
                    perTurnGen[ConstructionResourceType.naturalmaterials] += site.natureWorkers;
                    perTurnGen[ConstructionResourceType.prefabs] += site.carpenterWorkers;
                    break;
                case ConstructibleType.Quarry:
                    Quarry quarry = (Quarry)building;
                    if (quarry.ActionPlan == 0)
                    {
                        perTurnGen[ConstructionResourceType.stone] += ((Config.BuildConfig.QuarryStoneMin + Config.BuildConfig.QuarryStoneMax) / 2) + (quarry.improveUpgrade.built ? 1 : 0);
                        perTurnGen[ConstructionResourceType.ores] += ((Config.BuildConfig.QuarryOreMin + Config.BuildConfig.QuarryOreMax) / 2) + (quarry.improveUpgrade.built ? 1 : 0) * (quarry.deepUpgrade.built ? 1 : 0);
                        perTurnGen[ConstructionResourceType.manastones] += ((Config.BuildConfig.QuarryMSMin + Config.BuildConfig.QuarryMSMax) / 2) + (quarry.improveUpgrade.built ? 1 : 0) * (quarry.leyUpgrade.built ? 1 : 0);
                    }
                    else if (quarry.ActionPlan == 1)
                    {
                        perTurnGen[ConstructionResourceType.stone] += ((Config.BuildConfig.QuarryStoneMin / 2 + (int)(Config.BuildConfig.QuarryStoneMax * (quarry.improveUpgrade.built ? 1.75f : 1.5f))) / 2);
                        perTurnGen[ConstructionResourceType.ores] += ((Config.BuildConfig.QuarryOreMin / 2 + (int)(Config.BuildConfig.QuarryOreMax * (quarry.improveUpgrade.built ? 1.75f : 1.5f))) / 2) * (quarry.deepUpgrade.built ? 1 : 0);
                        perTurnGen[ConstructionResourceType.manastones] += ((Config.BuildConfig.QuarryMSMin / 2 + (int)(Config.BuildConfig.QuarryMSMax * (quarry.improveUpgrade.built ? 1.75f : 1.5f))) / 2) * (quarry.leyUpgrade.built ? 1 : 0);
                    }
                    else if (quarry.ActionPlan == 2)
                    {
                        perTurnGen[ConstructionResourceType.stone] += Config.BuildConfig.QuarryStoneMax / (quarry.improveUpgrade.built ? 2 : 4);
                        perTurnGen[ConstructionResourceType.ores] += Config.BuildConfig.QuarryOreMax / (quarry.improveUpgrade.built ? 2 : 4);
                        perTurnGen[ConstructionResourceType.manastones] += Config.BuildConfig.QuarryMSMax / (quarry.improveUpgrade.built ? 2 : 4);
                    }
                    else if (quarry.ActionPlan == 3)
                    {
                        perTurnGen[ConstructionResourceType.stone] += ((Config.BuildConfig.QuarryStoneMin + Config.BuildConfig.QuarryStoneMax) / 2);
                        perTurnGen[ConstructionResourceType.ores] += ((Config.BuildConfig.QuarryOreMin + Config.BuildConfig.QuarryOreMax) / 2) * (quarry.deepUpgrade.built ? 1 : 0);
                        perTurnGen[ConstructionResourceType.manastones] += ((Config.BuildConfig.QuarryMSMin + Config.BuildConfig.QuarryMSMax) / 2) * (quarry.leyUpgrade.built ? 1 : 0);
                    }
                    else if (quarry.ActionPlan == 4)
                    {
                        perTurnGen[ConstructionResourceType.ores] += (int)Math.Round((Config.BuildConfig.QuarryOreMin + Config.BuildConfig.QuarryOreMax) * 0.6f) * (quarry.deepUpgrade.built ? 1 : 0);
                        perTurnGen[ConstructionResourceType.manastones] += (int)Math.Round((Config.BuildConfig.QuarryMSMin + Config.BuildConfig.QuarryMSMax) * 0.4f) * (quarry.leyUpgrade.built ? 1 : 0);
                    }
                    break;
                default:
                    break;
            }
        }
        return perTurnGen;
    }
    void ConstructBuilding(Vec2i position, ConstructibleType SelectedConstruction)
    {
        // If not enough resource, don't build
        if (!empire.constructionResources.CanBuildWithCurrentResources(buildingInfo[buildingWanted].Resources) || !(goldBank >= buildingInfo[buildingWanted].Gold))
        {
            return;
        }
        ConstructibleBuilding newBuilding;
        switch (SelectedConstruction)
        {
            case ConstructibleType.WorkCamp:
                TransactGold(Config.BuildConfig.WorkCamp.Gold * -1);
                newBuilding = new WorkCamp(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.LumberSite:
                TransactGold(Config.BuildConfig.LumberSite.Gold * -1);
                newBuilding = new LumberSite(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.Quarry:
                TransactGold(Config.BuildConfig.Quarry.Gold * -1);
                newBuilding = new Quarry(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.CasterTower:
                TransactGold(Config.BuildConfig.CasterTower.Gold * -1);
                newBuilding = new CasterTower(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.BarrierTower:
                TransactGold(Config.BuildConfig.BarrierTower.Gold * -1);
                newBuilding = new BarrierTower(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.DefEncampment:
                TransactGold(Config.BuildConfig.DefenseEncampment.Gold * -1);
                newBuilding = new DefenseEncampment(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.Academy:
                TransactGold(Config.BuildConfig.Academy.Gold * -1);
                newBuilding = new Academy(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.DarkMagicTower:
                TransactGold(Config.BuildConfig.DarkMagicTower.Gold * -1);
                newBuilding = new BlackMagicTower(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.TemporalTower:
                TransactGold(Config.BuildConfig.Teleporter.Gold * -1);
                newBuilding = new TemporalTower(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.Laboratory:
                TransactGold(Config.BuildConfig.Laboratory.Gold * -1);
                newBuilding = new Laboratory(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.Teleporter:
                TransactGold(Config.BuildConfig.Teleporter.Gold * -1);
                newBuilding = new Teleporter(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.TownHall:
                TransactGold(Config.BuildConfig.TownHall.Gold * -1);
                newBuilding = new TownHall(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
        }
        GetNewProject();
    }

    internal void BuildUpgrades()
    {
        if (upgradeWanted != null)
        {
            // 20% chance to skip upgrade and save
            if (State.Rand.Next(5) == 0)
            {
                return;
            }
            // If all resource ready, build upgrade
            if (empire.constructionResources.CanBuildWithCurrentResources(upgradeWanted.ResourceToUpgrade) && goldBank >= upgradeWanted.GoldCost)
            {
                upgradeBuilding.turnsToUpgrade += upgradeWanted.upgradeTime;
                empire.constructionResources.SpendProvidedResources(upgradeWanted.ResourceToUpgrade);
                SpendGold(upgradeWanted.GoldCost);
                State.GameManager.StrategyMode.RedrawVillages();
                GetNewProject();
            }
        }
    }
}
