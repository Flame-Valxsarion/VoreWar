using MapObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LumberSitePanel : MonoBehaviour
{

    public Button AddWoodWorker;
    public Button RemoveWoodWorker;
    
    public Button AddGreenhouseWorker;
    public Button RemoveGreenhouseWorker;

    public Button AddCarpenterWorker;
    public Button RemoveCarpenterWorker;


    public TextMeshProUGUI IdleWorkers;
    public TextMeshProUGUI WoodWorkers;
    public TextMeshProUGUI NMWorkers;
    public TextMeshProUGUI PrefabWorkers;

    LumberSite LumberSite;

    public void Open(ConstructibleBuilding building)
    {
        LumberSite = (LumberSite)building;
        UpdateVisibility();
    }

    public void AddWoodWorkerButton()
    {
        LumberSite.woodWorkers++;
        LumberSite.IdleWorkers--;
        UpdateVisibility();
    }

    public void RemoveWoodWorkerButton()
    {
        LumberSite.woodWorkers--;
        LumberSite.IdleWorkers++;
        UpdateVisibility();
    }

    public void AddNMWorkerButton()
    {
        LumberSite.natureWorkers++;
        LumberSite.IdleWorkers--;
        UpdateVisibility();
    }

    public void RemovNMWorkerButton()
    {
        LumberSite.natureWorkers--;
        LumberSite.IdleWorkers++;
        UpdateVisibility();
    }

    public void AddPrefabWorkerButton()
    {
        LumberSite.carpenterWorkers += 2;
        LumberSite.IdleWorkers -= 2;
        UpdateVisibility();
    }

    public void RemovPrefabWorkerButton()
    {
        LumberSite.carpenterWorkers -= 2;
        LumberSite.IdleWorkers += 2;
        UpdateVisibility();
    }

    void UpdateVisibility()
    {
        RemoveWoodWorker.interactable = LumberSite.woodWorkers > 0;
        AddWoodWorker.interactable = LumberSite.IdleWorkers > 0;
        RemoveGreenhouseWorker.interactable = LumberSite.natureWorkers > 0 && LumberSite.greenHouseUpgrade.built;
        AddGreenhouseWorker.interactable = LumberSite.IdleWorkers > 0 && LumberSite.greenHouseUpgrade.built;
        RemoveCarpenterWorker.interactable = LumberSite.carpenterWorkers > 0 && LumberSite.carpenterUpgrade.built;
        AddCarpenterWorker.interactable = LumberSite.IdleWorkers >= 2 && LumberSite.carpenterUpgrade.built;

        IdleWorkers.text = LumberSite.IdleWorkers.ToString();
        WoodWorkers.text = LumberSite.woodWorkers.ToString();
        NMWorkers.text = LumberSite.natureWorkers.ToString();
        PrefabWorkers.text = LumberSite.carpenterWorkers.ToString();

    }

}
