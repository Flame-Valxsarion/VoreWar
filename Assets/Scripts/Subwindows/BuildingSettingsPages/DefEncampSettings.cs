using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefEncampSettings : MonoBehaviour
{
    public InputField ArmyPercentage;
    public InputField UnitScale;
    public InputField MaxGarrisonSizeScale;
    public InputField TrainTime;

    internal void Save()
    {
        Config.BuildConfig.DefenseEncampmentArmyPercentage = float.TryParse(ArmyPercentage.text, out float smi) ? smi : 0.2f;
        Config.BuildConfig.DefenseEncampmentUnitScale = float.TryParse(UnitScale.text, out float sma) ? sma : 0.5f;
        Config.BuildConfig.DefenseEncampmentMaxGarrisonSizeScale = float.TryParse(MaxGarrisonSizeScale.text, out float omi) ? omi : 0.5f;
        Config.BuildConfig.DefenseEncampmentTrainTime = int.TryParse(TrainTime.text, out int oma) ? oma : 4;

    }

    internal void Load()
    {
        ArmyPercentage.text = Config.BuildConfig.DefenseEncampmentArmyPercentage.ToString();
        UnitScale.text = Config.BuildConfig.DefenseEncampmentUnitScale.ToString();
        MaxGarrisonSizeScale.text = Config.BuildConfig.DefenseEncampmentMaxGarrisonSizeScale.ToString();
        TrainTime.text = Config.BuildConfig.DefenseEncampmentTrainTime.ToString();
    }
}
