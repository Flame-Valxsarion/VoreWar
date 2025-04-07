using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AncientTeleporterMenu : MonoBehaviour
{
    public Transform StandardFolder;
    public TeleporterPanelArmyStandardPrefab StandardPrefab;
    internal TeleporterPanelArmyStandardPrefab StandardPrefabInstance;


    AncientTeleporter Teleporter;
    List<Army> StandardArmies;
    List<Button> TeleportButtons;
    public void Open(AncientTeleporter tele, Empire empire)
    {
        gameObject.SetActive(true);
        Teleporter = tele;
        StandardArmies = new List<Army>();
        TeleportButtons = new List<Button>();
        int children = StandardFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(StandardFolder.GetChild(i).gameObject);
        }
        foreach (var ancientTeleporter in State.World.AncientTeleporters)
        {
            StandardArmies = StandardArmies.Union(StrategicUtilities.GetAllyArmyWithinXTiles(ancientTeleporter.Position, 1, empire)).ToList();
        }
        foreach (var standardTele in empire.Buildings.Where(b => b is Teleporter))
        {
            if (((Teleporter)standardTele).ancientUpgrade.built)
            {
                StandardArmies = StandardArmies.Union(StrategicUtilities.GetOwnerArmyWithinXTiles(standardTele, 1)).ToList();
            }
        }

        foreach (var army in StandardArmies)
        {
            StandardPrefabInstance = Instantiate(StandardPrefab, StandardFolder);
            TeleporterPanelArmyStandardPrefab newAvailPrefab = StandardPrefabInstance.GetComponent<TeleporterPanelArmyStandardPrefab>();
            newAvailPrefab.ArmyName.text = army.Name;
            newAvailPrefab.CapacityUse.gameObject.SetActive(false);
            newAvailPrefab.WarpButton.onClick.AddListenerOnce(() =>
            {
                TeleportArmy(army);
            });
            TeleportButtons.Add(newAvailPrefab.WarpButton);

        }
    }

    void TeleportArmy(Army army)
    {
        army.SetPosition(Teleporter.Position);
        army.Destination = null;
        Exit();
    }
    public void Exit()
    {
        int children = StandardFolder.childCount;
        State.GameManager.StrategyMode.RedrawArmies();
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(StandardFolder.GetChild(i).gameObject);
        }
        gameObject.SetActive(false);
    }
}