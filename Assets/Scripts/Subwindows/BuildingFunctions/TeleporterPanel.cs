using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeleporterPanel : MonoBehaviour
{
    public TextMeshProUGUI TeleportCapacity;
    public TextMeshProUGUI TeleportRegen;
    public GameObject NearbyHolder;
    public GameObject StandardFunctionHolder;
    public Transform NearbyFolder;
    public Transform StandardFolder;
    public TeleporterPanelArmyNearbyPrefab NearbyPrefab;
    public TeleporterPanelArmyStandardPrefab StandardPrefab;
    internal TeleporterPanelArmyNearbyPrefab NearbyPrefabInstance;
    internal TeleporterPanelArmyStandardPrefab StandardPrefabInstance;


    Teleporter Teleporter;
    List<Army> StandardArmies;
    List<Army> NeabyArmies;
    List<Button> TeleportButtons;
    public void Open(ConstructibleBuilding building)
    {
        Teleporter = (Teleporter)building;
        NearbyHolder.SetActive(Teleporter.stoneUpgrade.built);
        StandardArmies = new List<Army>();
        TeleportButtons = new List<Button>();
        int children = NearbyFolder.childCount;
        TeleportCapacity.text = $"Capacity: {Teleporter.TeleportCapacity} {(Teleporter.capacityUpgrade.built ? "" : $"/ {Config.BuildConfig.TeleporterMaxCapacity}")}";
        TeleportRegen.text = $"+{Config.BuildConfig.TeleporterCapacityRegen} / turn";
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(NearbyFolder.GetChild(i).gameObject);
        }
        children = StandardFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(StandardFolder.GetChild(i).gameObject);
        }
        foreach (var tele in Teleporter.Owner.Buildings.Where(b => b is Teleporter && b.active))
        {
            if (tele == Teleporter)
            {
                continue;
            }
            StandardArmies = StandardArmies.Union(StrategicUtilities.GetOwnerArmyWithinXTiles(tele, 1)).ToList();
        }
        if (Teleporter.ancientUpgrade.built)
        {
            foreach (var anctele in State.World.AncientTeleporters)
            {
                StandardArmies = StandardArmies.Union(StrategicUtilities.GetAllyArmyWithinXTiles(anctele.Position, 1, Teleporter.Owner)).ToList();
            }
        }    
        NeabyArmies = StrategicUtilities.GetOwnerArmyWithinXTiles(Teleporter, 1);
        if (Teleporter.stoneUpgrade.built)
        {
            foreach (var item in Teleporter.Owner.Armies.Where(a => a.LinkedTeleporter == Teleporter))
            {
                if (!StandardArmies.Contains(item) && !NeabyArmies.Contains(item))
                {
                    StandardArmies.Add(item);
                }
            }
        }
        foreach (var army in StandardArmies)
        {
            StandardPrefabInstance = Instantiate(StandardPrefab, StandardFolder);
            TeleporterPanelArmyStandardPrefab newAvailPrefab = StandardPrefabInstance.GetComponent<TeleporterPanelArmyStandardPrefab>();
            float CapUse = 0;
            foreach (Unit unit in army.Units)
            {
                CapUse += State.RaceSettings.GetDeployCost(unit.Race) * unit.TraitBoosts.DeployCostMult * Config.BuildConfig.TeleporterPerUnitCapacityMod;
            }
            if (Config.BuildConfig.TeleporterPerUnitCapacity)
            {
                CapUse = 1;
            }
            newAvailPrefab.ArmyName.text = army.Name;
            newAvailPrefab.CapacityUse.text = $"Requires: {CapUse} Capacity";
            newAvailPrefab.WarpButton.onClick.AddListenerOnce(() =>
            { 
                TeleportArmy(army, CapUse);
            });
            if (CapUse > Teleporter.TeleportCapacity)
            {
                newAvailPrefab.WarpButton.interactable = false;
            }
            TeleportButtons.Add(newAvailPrefab.WarpButton);

        }
        foreach (var army in NeabyArmies) 
        {

            NearbyPrefabInstance = Instantiate(NearbyPrefab, NearbyFolder);
            TeleporterPanelArmyNearbyPrefab newNearbyPrefab = NearbyPrefabInstance.GetComponent<TeleporterPanelArmyNearbyPrefab>();
            newNearbyPrefab.ArmyName.text = army.Name;
            newNearbyPrefab.SyncStoneButton.onClick.AddListenerOnce(() =>
            {
                army.LinkedTeleporter = Teleporter;
                newNearbyPrefab.SyncStoneButton.interactable = false;
            });
            if (army.LinkedTeleporter == Teleporter)
            {
                newNearbyPrefab.SyncStoneButton.interactable = false;
            }
        }
    }

    void TeleportArmy(Army army, float CapacityUse)
    {
        army.SetPosition(Teleporter.Position);
        army.Destination = null;
        Teleporter.TeleportCapacity -= CapacityUse;
        foreach (var item in TeleportButtons)
        {
            item.interactable = false;
        }
    }
}