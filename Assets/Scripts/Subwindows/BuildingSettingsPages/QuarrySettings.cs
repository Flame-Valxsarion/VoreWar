using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuarrySettings : MonoBehaviour
{
    public InputField StoneMin;
    public InputField StoneMax;
    public InputField OreMin;
    public InputField OreMax;
    public InputField MSMin;
    public InputField MSMax;
    public InputField GoldMin;
    public InputField GoldMax;

    internal void Save()
    {
        Config.BuildConfig.QuarryStoneMin = int.TryParse(StoneMin.text, out int smi) ? smi : 1;
        Config.BuildConfig.QuarryStoneMax = int.TryParse(StoneMax.text, out int sma) ? sma : 3;
        Config.BuildConfig.QuarryOreMin = int.TryParse(OreMin.text, out int omi) ? omi : 1;
        Config.BuildConfig.QuarryOreMax = int.TryParse(OreMax.text, out int oma) ? oma : 3;
        Config.BuildConfig.QuarryMSMin = int.TryParse(MSMin.text, out int mmi) ? mmi : 1;
        Config.BuildConfig.QuarryMSMax = int.TryParse(MSMax.text, out int mma) ? mma : 3;
        Config.BuildConfig.QuarryGoldMin = int.TryParse(GoldMin.text, out int gmi) ? gmi : 0;
        Config.BuildConfig.QuarryGoldMax = int.TryParse(GoldMax.text, out int gma) ? gma : 20;
    }

    internal void Load()
    {
        StoneMax.text = Config.BuildConfig.QuarryStoneMin.ToString();
        StoneMin.text = Config.BuildConfig.QuarryStoneMax.ToString();
        OreMin.text = Config.BuildConfig.QuarryOreMin.ToString();
        OreMax.text = Config.BuildConfig.QuarryOreMax.ToString();
        MSMin.text = Config.BuildConfig.QuarryMSMin.ToString();
        MSMax.text = Config.BuildConfig.QuarryMSMax.ToString();
        GoldMin.text = Config.BuildConfig.QuarryGoldMin.ToString();
        GoldMax.text = Config.BuildConfig.QuarryGoldMax.ToString();
    }
}
