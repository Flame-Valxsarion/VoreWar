using MapObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPrefab : MonoBehaviour
{
    public TextMeshProUGUI BuildingName;
    public TextMeshProUGUI BuildingDesc;
    public TextMeshProUGUI BuildTurns;
    public TextMeshProUGUI BuildLimit;
    public TextMeshProUGUI GoldCost;

    public TextMeshProUGUI Wood;
    public TextMeshProUGUI NaturalMaterials;
    public TextMeshProUGUI Prefabs;
    public TextMeshProUGUI Stone;
    public TextMeshProUGUI Ores;
    public TextMeshProUGUI ManaStones;

    public Button Construct;

    internal ConstructibleType linkedBuilding;
}
