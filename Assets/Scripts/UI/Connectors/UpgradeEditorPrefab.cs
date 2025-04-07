using MapObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeEditorPrefab : MonoBehaviour
{
    public TextMeshProUGUI UpgradeName;
    public TextMeshProUGUI UpgradeDesc;

    public InputField UpgradeTurns;
    public InputField GoldCost;
    public InputField WoodCost;
    public InputField NaturalMaterialsCost;
    public InputField PrefabsCost;
    public InputField StoneCost;
    public InputField OresCost;
    public InputField ManaStonesCost;

    public BuildingUpgrade AssociatedUpgrade;
}
