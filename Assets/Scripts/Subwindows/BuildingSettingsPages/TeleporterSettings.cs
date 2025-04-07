using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeleporterSettings : MonoBehaviour
{
    public InputField MaxCapacity;
    public InputField CapacityRegen;
    public Toggle PerUnitCapacity;
    public InputField PerUnitCapacityMod;

    internal void Save()
    {
        Config.BuildConfig.TeleporterMaxCapacity = float.TryParse(MaxCapacity.text, out float smi) ? smi : 3f;
        Config.BuildConfig.TeleporterCapacityRegen = float.TryParse(CapacityRegen.text, out float sma) ? sma : 0.5f;
        Config.BuildConfig.TeleporterPerUnitCapacity = PerUnitCapacity.isOn;
        Config.BuildConfig.TeleporterPerUnitCapacityMod = float.TryParse(PerUnitCapacityMod.text, out float oma) ? oma : 0.1f;

    }

    internal void Load()
    {
        MaxCapacity.text = Config.BuildConfig.TeleporterMaxCapacity.ToString();
        CapacityRegen.text = Config.BuildConfig.TeleporterCapacityRegen.ToString();
        PerUnitCapacity.isOn = Config.BuildConfig.TeleporterPerUnitCapacity;
        PerUnitCapacityMod.text = Config.BuildConfig.TeleporterPerUnitCapacityMod.ToString();
    }
}
