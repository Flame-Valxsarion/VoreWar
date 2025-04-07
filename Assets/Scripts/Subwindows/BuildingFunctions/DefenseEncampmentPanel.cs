using MapObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefenseEncampmentPanel : MonoBehaviour
{ 
    public TextMeshProUGUI CurrentLevel;
    public TextMeshProUGUI CurrentDefenders;
    public TextMeshProUGUI MaxDefenders;
    public TextMeshProUGUI CurrentTrainTime;

    DefenseEncampment DefenseEncampment;

    public void Open(ConstructibleBuilding building)
    {
        DefenseEncampment = (DefenseEncampment)building;
        float unitScale = Config.BuildConfig.DefenseEncampmentUnitScale * (DefenseEncampment.levelUpgrade.built ? 1.5f : 1);
        CurrentLevel.text = ((int)Mathf.Max(Mathf.Floor(building.Owner.Leader.Level * unitScale), 1)).ToString();
        CurrentDefenders.text = DefenseEncampment.AvailibleDefenders.ToString();
        MaxDefenders.text = DefenseEncampment.maxDefenders.ToString();
        CurrentTrainTime.text = DefenseEncampment.TrainTimer.ToString();
    }

}
