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
        int level = 0;
        if (building.Owner.Leader == null)        
            level = State.GameManager.StrategyMode.ScaledExp / 5;        
        else
            level = building.Owner.Leader.Level;
        CurrentLevel.text = ((int)Mathf.Max(Mathf.Floor(level * unitScale), 1)).ToString();
        CurrentDefenders.text = DefenseEncampment.AvailableDefenders.ToString();
        MaxDefenders.text = DefenseEncampment.maxDefenders.ToString();
        CurrentTrainTime.text = DefenseEncampment.TrainTimer.ToString();
    }

}
