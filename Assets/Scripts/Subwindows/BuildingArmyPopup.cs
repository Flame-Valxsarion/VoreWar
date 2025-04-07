using Boo.Lang;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuildingArmyPopup : MonoBehaviour
{
    Army clickedArmy;
    ConstructibleBuilding clickedBuilding;

    public void Open(Army army, ConstructibleBuilding building)
    {
        gameObject.SetActive(true);
        clickedArmy = army;
        clickedBuilding = building;
    }  
    public void SelectArmy()
    {
        gameObject.SetActive(false);
        State.GameManager.ActivateRecruitMode(clickedArmy.Empire, clickedArmy, Recruit_Mode.ActivatingEmpire.Ally);
    }
    public void SelectBuilding()
    {
        gameObject.SetActive(false);
        State.GameManager.StrategyMode.UpgradeMenu.Open(clickedBuilding);

    }
}