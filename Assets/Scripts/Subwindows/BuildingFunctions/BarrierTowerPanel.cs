using MapObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarrierTowerPanel : MonoBehaviour
{

    public GameObject MendingHolder;
    public GameObject EnhanceHolder;
    public GameObject Core2Holder;
    public GameObject Core3Holder;
    
    public TextMeshProUGUI CurrentPotentialDowntime;
    public TextMeshProUGUI SlotOneDowntime;
    public TextMeshProUGUI SlotTwoDowntime;
    public TextMeshProUGUI SlotThreeDowntime;

    public TextMeshProUGUI BarrierMagnitude;
    public Slider BarrierMagnitudeSlider;
    public TextMeshProUGUI MendingMagnitude;
    public Slider MendingMagnitudeSlider;
    public TextMeshProUGUI EmpowerMagnitude;
    public Slider EmpowerMagnitudeSlider;

    public Toggle CoreProtection;

    BarrierTower BarrierTower;

    public void Open(ConstructibleBuilding building)
    {
        BarrierTower = (BarrierTower)building;
        HandleTowerCores();
        BarrierMagnitude.text = BarrierTower.BarrierMagnitude.ToString();
        BarrierMagnitudeSlider.value = BarrierTower.BarrierMagnitude;
        MendingMagnitude.text = BarrierTower.MendingMagnitude.ToString();
        MendingMagnitudeSlider.value = BarrierTower.MendingMagnitude;
        EmpowerMagnitude.text = BarrierTower.EmpowerMagnitude.ToString();
        EmpowerMagnitudeSlider.value = BarrierTower.EmpowerMagnitude;
        CoreProtection.isOn = BarrierTower.CoreProtection;

        BarrierMagnitudeSlider.onValueChanged.AddListenerOnce((float newVal) =>
        {
            BarrierTower.BarrierMagnitude = (int)newVal;
            BarrierMagnitude.text = newVal.ToString();
            RefreshDowntimeCalc();
        });
        MendingMagnitudeSlider.onValueChanged.AddListenerOnce((float newVal) =>
        {
            BarrierTower.MendingMagnitude = (int)newVal;
            MendingMagnitude.text = newVal.ToString();
            RefreshDowntimeCalc();
        });
        EmpowerMagnitudeSlider.onValueChanged.AddListenerOnce((float newVal) =>
        {
            BarrierTower.EmpowerMagnitude = (int)newVal;
            EmpowerMagnitude.text = newVal.ToString();
            RefreshDowntimeCalc();
        });
        CoreProtection.onValueChanged.AddListener((bool val) =>
        {
            BarrierTower.CoreProtection = val;
        });
        if (!BarrierTower.healUpgrade.built)
            MendingHolder.gameObject.SetActive(false);
        else
            MendingHolder.gameObject.SetActive(true);
        if (!BarrierTower.buffUpgrade.built)
            EnhanceHolder.gameObject.SetActive(false);
        else
            EnhanceHolder.gameObject.SetActive(true);
        if (!BarrierTower.improveUpgrade.built)
        {
            Core2Holder.gameObject.SetActive(false);
            Core3Holder.gameObject.SetActive(false);
        }
        else
        {
            Core2Holder.gameObject.SetActive(true);
            Core3Holder.gameObject.SetActive(true);
        }
    }

    public void RefreshDowntimeCalc()
    {
        CurrentPotentialDowntime.text = BarrierTower.CurrentDowntimeValue.ToString();
    }

    public void HandleTowerCores()
    {
        if (BarrierTower.DowntimeSlot1 > 0)
            SlotOneDowntime.text = $"Inactive for {BarrierTower.DowntimeSlot1} turn(s)";
        else
            SlotOneDowntime.text = "Ready";

        if (BarrierTower.DowntimeSlot2 > 0)
            SlotTwoDowntime.text = $"Inactive for {BarrierTower.DowntimeSlot2} turn(s)";
        else
            SlotTwoDowntime.text = "Ready";

        if (BarrierTower.DowntimeSlot3 > 0)
            SlotThreeDowntime.text = $"Inactive for {BarrierTower.DowntimeSlot3} turn(s)";
        else
            SlotThreeDowntime.text = "Ready";

        if (!BarrierTower.improveUpgrade.built)
        {
            SlotTwoDowntime.text = "Unavailable (Requires Improvement Upgrade)";
            SlotThreeDowntime.text = "Unavailable (Requires Improvement Upgrade)";

        }
    }
}
