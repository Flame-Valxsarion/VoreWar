using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AcademySettings : MonoBehaviour
{
    public InputField EXPPerGold;
    public InputField MaximumUpgrades;
    public InputField UpgradeCost;
    public InputField CostIncreaseMultPerUpgrade;

    internal void Save()
    {
        Config.BuildConfig.AcademyEXPPerGold = int.TryParse(EXPPerGold.text, out int smi) ? smi : 5;
        Config.BuildConfig.AcademyMaximumUpgrades = int.TryParse(MaximumUpgrades.text, out int sma) ? sma : 4;
        Config.BuildConfig.AcademyUpgradeCost = int.TryParse(UpgradeCost.text, out int mmi) ? mmi : 1000;
        Config.BuildConfig.AcademyCostIncreaseMultPerUpgrade = float.TryParse(UpgradeCost.text, out float mme) ? mme : 1.5f;

    }

    internal void Load()
    {
        EXPPerGold.text = Config.BuildConfig.AcademyEXPPerGold.ToString();
        MaximumUpgrades.text = Config.BuildConfig.AcademyMaximumUpgrades.ToString();
        UpgradeCost.text = Config.BuildConfig.AcademyUpgradeCost.ToString();
        CostIncreaseMultPerUpgrade.text = Config.BuildConfig.AcademyCostIncreaseMultPerUpgrade.ToString();
    }
}
