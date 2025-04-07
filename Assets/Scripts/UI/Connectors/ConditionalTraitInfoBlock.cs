using CruxClothing;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConditionalTraitInfoBlock : MonoBehaviour
{
    public Button ToggleButton;

    public Button TriggersButton;
    public Button ClassificationsButton;
    public Button TraitsButton;
    public Button OperationBlocksButton;
    public Button BlockEditorButton;

    public GameObject HelpPanel;
    public GameObject TriggersPanel;
    public GameObject ClassificationsPanel;
    public GameObject TraitsPanel;
    public GameObject OperationBlocksPanel;
    public GameObject BlockEditorPanel;

    internal bool helpOn = false;

    public void ToggleHelp()
    {
        HelpPanel.SetActive(!helpOn);
        helpOn = !helpOn;
    }

    public void SwitchTriggers()
    {
        TriggersButton.interactable = false;
        ClassificationsButton.interactable = true;
        TraitsButton.interactable = true;
        OperationBlocksButton.interactable = true;
        BlockEditorButton.interactable = true;

        TriggersPanel.gameObject.SetActive(true);
        ClassificationsPanel.gameObject.SetActive(false);
        TraitsPanel.gameObject.SetActive(false);
        OperationBlocksPanel.gameObject.SetActive(false);
        BlockEditorPanel.gameObject.SetActive(false);
    }
    public void SwitchClass()
    {
        TriggersButton.interactable = true;
        ClassificationsButton.interactable = false;
        TraitsButton.interactable = true;
        OperationBlocksButton.interactable = true;
        BlockEditorButton.interactable = true;

        TriggersPanel.gameObject.SetActive(false);
        ClassificationsPanel.gameObject.SetActive(true);
        TraitsPanel.gameObject.SetActive(false);
        OperationBlocksPanel.gameObject.SetActive(false);
        BlockEditorPanel.gameObject.SetActive(false);
    }
    public void SwitchTraits()
    {
        TriggersButton.interactable = true;
        ClassificationsButton.interactable = true;
        TraitsButton.interactable = false;
        OperationBlocksButton.interactable = true;
        BlockEditorButton.interactable = true;

        TriggersPanel.gameObject.SetActive(false);
        ClassificationsPanel.gameObject.SetActive(false);
        TraitsPanel.gameObject.SetActive(true);
        OperationBlocksPanel.gameObject.SetActive(false);
        BlockEditorPanel.gameObject.SetActive(false);
    }
    public void SwitchOpBlock()
    {
        TriggersButton.interactable = true;
        ClassificationsButton.interactable = true;
        TraitsButton.interactable = true;
        OperationBlocksButton.interactable = false;
        BlockEditorButton.interactable = true;

        TriggersPanel.gameObject.SetActive(false);
        ClassificationsPanel.gameObject.SetActive(false);
        TraitsPanel.gameObject.SetActive(false);
        OperationBlocksPanel.gameObject.SetActive(true);
        BlockEditorPanel.gameObject.SetActive(false);
    }
    public void SwitchBlockEdit()
    {
        TriggersButton.interactable = true;
        ClassificationsButton.interactable = true;
        TraitsButton.interactable = true;
        OperationBlocksButton.interactable = true;
        BlockEditorButton.interactable = false;

        TriggersPanel.gameObject.SetActive(false);
        ClassificationsPanel.gameObject.SetActive(false);
        TraitsPanel.gameObject.SetActive(false);
        OperationBlocksPanel.gameObject.SetActive(false);
        BlockEditorPanel.gameObject.SetActive(true);
    }
}