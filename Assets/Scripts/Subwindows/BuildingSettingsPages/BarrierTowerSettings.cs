using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarrierTowerSettings : MonoBehaviour
{
    public InputField BaseBarrierStrength;
    public Toggle IgnoreDowntime;

    internal void Save()
    {
        Config.BuildConfig.BarrierTowerBaseBarrierStrength = int.TryParse(BaseBarrierStrength.text, out int smi) ? smi : 10;
        Config.BuildConfig.BarrierTowerIgnoreDowntime = IgnoreDowntime.isOn;


    }

    internal void Load()
    {
        BaseBarrierStrength.text = Config.BuildConfig.BarrierTowerBaseBarrierStrength.ToString();
        IgnoreDowntime.isOn = Config.BuildConfig.BarrierTowerIgnoreDowntime;
    }
}
