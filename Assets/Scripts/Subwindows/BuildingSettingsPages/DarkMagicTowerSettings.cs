using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DarkMagicTowerSettings : MonoBehaviour
{
    public InputField DurationImprovement;
    public InputField AccImprovement;
    public InputField SoulPointBase;
    public InputField SoulPointMult;

    internal void Save()
    {
        Config.BuildConfig.DarkMagicTowerDurationImprovement = int.TryParse(DurationImprovement.text, out int smi) ? smi : 5;
        Config.BuildConfig.DarkMagicTowerAccImprovement = int.TryParse(AccImprovement.text, out int sma) ? sma : 10;
        Config.BuildConfig.DarkMagicTowerSoulPointBase = int.TryParse(SoulPointBase.text, out int omi) ? omi : 100;
        Config.BuildConfig.DarkMagicTowerSoulPointMult = float.TryParse(SoulPointMult.text, out float oma) ? oma : 1.5f;

    }

    internal void Load()
    {
        DurationImprovement.text = Config.BuildConfig.DarkMagicTowerDurationImprovement.ToString();
        AccImprovement.text = Config.BuildConfig.DarkMagicTowerAccImprovement.ToString();
        SoulPointBase.text = Config.BuildConfig.DarkMagicTowerSoulPointBase.ToString();
        SoulPointMult.text = Config.BuildConfig.DarkMagicTowerSoulPointMult.ToString();
    }
}
