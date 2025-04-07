using MapObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AcademyPanelResearchItemPrefab : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Effect;
    public TextMeshProUGUI Current;
    public TextMeshProUGUI Maximum;
    public Button ConductButton;

    internal Empire Owner;
    internal AcademyResearchType associatedResearch;
    internal Func<Empire, int> functinoCall;
}