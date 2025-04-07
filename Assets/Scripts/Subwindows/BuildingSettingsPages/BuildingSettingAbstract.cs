using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BuildingSettingAbstract : MonoBehaviour
{
    public InputField WoodCost;
    public InputField NaturalMaterialsCost;
    public InputField PrefabsCost;
    public InputField StoneCost;
    public InputField OresCost;
    public InputField ManaStonesCost;

    internal virtual void Save()
    {

    }
    internal virtual void Load()
    {

    }
}
