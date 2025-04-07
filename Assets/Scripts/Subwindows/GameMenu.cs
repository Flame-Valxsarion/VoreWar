using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public Options Options;
    public GameObject UIPanel;
    public WorldSettings WorldSettingsUI;

    public SaveLoad SaveLoadScreen;
    public CheatMenu CheatMenu;
    public RaceEditorPanel RaceEditor;
    public RandomizerTraitEditor RandomizerTraitEditor;
    public CustomTraitEditor CustomTraitEditor;
    public ConditionalTraitsEditor CondTraitEditor;

    public HelpPanel HelpUI;

    public Button SaveLoadButton;
    public Button OpenMapEditorButton;
    public Button WorldSettingsButton;

    public GameObject HelpScreen;

    public ContentSettings ContentSettings;

    public void UpdateButtons()
    {
        if (State.GameManager.CurrentScene == State.GameManager.MapEditor)
        {
            OpenMapEditorButton.interactable = false;
            WorldSettingsButton.interactable = false;
        }
        else
        {
            OpenMapEditorButton.interactable = true;
            WorldSettingsButton.interactable = true;
        }
        SaveLoadButton.interactable = State.TutorialMode == false;
    }

    public void OpenSaveLoadMenu()
    {
        if (State.GameManager.CurrentScene == State.GameManager.MapEditor)
        {
            State.GameManager.CreateMessageBox("Can't access normal game save/load from within the map editor, use the map editor save/load on the left panel");
            return;
        }
        SaveLoadScreen.gameObject.SetActive(true);
        SaveLoadScreen.ListSlots();
    }

    public void OpenCheats()
    {
        CheatMenu.gameObject.SetActive(true);
        CheatMenu.Open();
    }

    public void OpenRaceEditor()
    {
        RaceEditor.gameObject.SetActive(true);
        RaceEditor.ShowPanel();
    }

    public void OpenOptions()
    {
        Options.gameObject.SetActive(true);
        Options.Open();
    }

    public void OpenRandomizerTraits()
    {
        RandomizerTraitEditor.gameObject.SetActive(true);
        RandomizerTraitEditor.Open();
    }

    public void OpenCustomTraits()
    {
        CustomTraitEditor.gameObject.SetActive(true);
        CustomTraitEditor.Open();
    }

    public void OpenCondTraits()
    {
        CondTraitEditor.gameObject.SetActive(true);
        CondTraitEditor.Open();
    }
    public void LoadHelp()
    {
        HelpUI.GenerateButtonsIfNeeded();
        HelpScreen.SetActive(true);
    }

    public void CloseHelp()
    {
        HelpScreen.SetActive(false);
    }

    public void ShowGameSettings()
    {
        if (State.World.MainEmpires == null)
        {
            State.GameManager.CreateMessageBox("No world is currently loaded, you're in a pure tactical game");
            return;
        }

        WorldSettingsUI.gameObject.SetActive(true);
        WorldSettingsUI.Open();
    }

    public void UpdateDisplay()
    {
        Options.RefreshText();
        if (Input.GetButtonDown("Menu"))
        {
            if (HelpScreen.activeSelf)
                CloseHelp();
            else if (State.GameManager.VariableEditor.gameObject.activeSelf)
                State.GameManager.VariableEditor.SaveAndClose();
            else if (SaveLoadScreen.gameObject.activeSelf)
                SaveLoadScreen.gameObject.SetActive(false);
            else if (Options.gameObject.activeSelf)
                Options.CloseAndSave();
            else if (ContentSettings.gameObject.activeSelf)
                ContentSettings.Exit();
            else if (CheatMenu.gameObject.activeSelf)
                CheatMenu.CloseAndSave();
            else if (WorldSettingsUI.gameObject.activeSelf)
                WorldSettingsUI.ExitAndSave();
            else
                CloseMenu();
        }

    }

    public void MainMenu()
    {
        if (State.GameManager.CurrentScene == State.GameManager.StrategyMode)
        {
            State.GameManager.StrategyMode.ClearData();
        }
        State.GameManager.SwitchToMainMenu();
        CloseMenu();
    }

    public void ExitGame()
    {
        Application.Quit();
        CloseMenu();
    }

    public void CloseMenu()
    {
        State.GameManager.CloseMenu();
        if (State.GameManager.CurrentScene == State.GameManager.StrategyMode)
        {
            State.GameManager.StrategyMode.Regenerate();
        }
        if (State.GameManager.CurrentScene == State.GameManager.TacticalMode)
        {
            State.GameManager.TacticalMode.RebuildInfo();
        }
    }



}
