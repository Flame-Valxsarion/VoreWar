using MapObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TemporalTowerPanel : MonoBehaviour
{
    TemporalTower TemporalTower;

    public void Open(ConstructibleBuilding building)
    {
        TemporalTower = (TemporalTower)building;

    }
}