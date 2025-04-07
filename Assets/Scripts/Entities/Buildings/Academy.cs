using OdinSerializer;
using System;
using System.Collections.Generic;

class Academy : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade researchUpgrade;
    public BuildingUpgrade efficiencyUpgrade;

    [OdinSerialize]
    internal int StoredEXP;
    [OdinSerialize]
    internal int Income1;
    [OdinSerialize]
    internal int Income2;
    [OdinSerialize]
    internal float DistributedEXP;

    internal int totalIncome => Income1 + Income2;
    public Academy(Vec2i location) : base(location)
    {
        Name = "Academy";
        Desc = "The academy turns gold into EXP. It can also apply buffs to the empire when upgraded.";
        spriteID = 48;
        buildingType = ConstructibleType.Academy;

        ApplyConfigStats(Config.BuildConfig.Academy);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildConfig.AcademyImproveUpgrade);
        researchUpgrade = AddUpgrade(researchUpgrade, Config.BuildConfig.AcademyResearchUpgrade);
        efficiencyUpgrade = AddUpgrade(efficiencyUpgrade, Config.BuildConfig.AcademyEfficiencyUpgrade);
        StoredEXP = 0;
        Income1 = 0;
        Income2 = 0;
        DistributedEXP = 0.1f;
    }
    internal override void RunBuildingFunction()
    {
        if (Owner.Gold + Owner.Income > 0)
        {
            StoredEXP += totalIncome * Config.BuildConfig.AcademyEXPPerGold;
        }
        if (DistributedEXP > 0)
        {
            int outgoingEXP = (int)(StoredEXP * DistributedEXP);
            StoredEXP -= outgoingEXP;
            var armies = StrategicUtilities.GetOwnerArmyWithinXTiles(this, Config.BuildConfig.BuildingPassiveRange);
            int unitCount = 0;
            foreach (Army army in armies)
            {
                foreach (var unit in army.Units)
                {
                    unitCount++;
                }
            }
            float perUnitEXP = (outgoingEXP / unitCount);
            float returnedEXP = 0;
            foreach (Army army in armies)
            {
                foreach (var unit in army.Units)
                {
                    int maxEXP = unit.GetExperienceRequiredForLevel(unit.Level + 1);
                    if (perUnitEXP > maxEXP)
                    {
                        unit.GiveExp(maxEXP);
                        returnedEXP += perUnitEXP - maxEXP;

                    }
                    else
                        unit.GiveExp(perUnitEXP);
                }
            }
            StoredEXP += (int)returnedEXP;
        }
    }
}

