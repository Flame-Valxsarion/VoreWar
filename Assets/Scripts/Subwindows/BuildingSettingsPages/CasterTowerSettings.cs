using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CasterTowerSettings : MonoBehaviour
{
    public InputField ChargesMax;
    public InputField ChargesRegen;
    public InputField BaseChargeCost;
    public InputField BetterTierChargeCost;
    public InputField BuffChargeCost;

    internal void Save()
    {
        Config.BuildConfig.CasterTowerManaChargesMax = int.TryParse(ChargesMax.text, out int smi) ? smi : 20;
        Config.BuildConfig.CasterTowerManaChargesRegen = int.TryParse(ChargesRegen.text, out int sma) ? sma : 4;
        Config.BuildConfig.CasterTowerBaseChargeCost = int.TryParse(BaseChargeCost.text, out int omi) ? omi : 1;
        Config.BuildConfig.CasterTowerBetterTierChargeCost = int.TryParse(BetterTierChargeCost.text, out int oma) ? oma : 3;
        Config.BuildConfig.CasterTowerBuffChargeCost = int.TryParse(BuffChargeCost.text, out int mmi) ? mmi : 2;

    }

    internal void Load()
    {
        ChargesMax.text = Config.BuildConfig.CasterTowerManaChargesMax.ToString();
        ChargesRegen.text = Config.BuildConfig.CasterTowerManaChargesRegen.ToString();
        BaseChargeCost.text = Config.BuildConfig.CasterTowerBaseChargeCost.ToString();
        BetterTierChargeCost.text = Config.BuildConfig.CasterTowerBetterTierChargeCost.ToString();
        BuffChargeCost.text = Config.BuildConfig.CasterTowerBuffChargeCost.ToString();
    }
}
