using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
#if UNITY_EDITOR == false
    bool confirmedQuit = false;
#endif

    private SceneBase currentScene;

    public SceneBase CurrentScene
    {
        get { return currentScene; }
        private set
        {
            if (currentScene != null)
                currentScene.UI.SetActive(false);
            currentScene = value;
            currentScene.UI.SetActive(true);
        }
    }

    public Camera Camera;
    public Camera_Controller CameraController;

    public PipCamera PipCamera;
    public StartMode Start_Mode;
    public StrategyMode StrategyMode;
    public TacticalMode TacticalMode;
    public Recruit_Mode Recruit_Mode;
    public EndScene EndScene;
    public MapEditor MapEditor;

    public VariableEditor VariableEditor;


    public StatScreenPanel StatScreen;

    public HoveringTooltip HoveringTooltip;
    public HoveringRacePicture HoveringRacePicture;

    public GameObject SavePrompt;
    public GameObject DialogBoxPrefab;
    public GameObject OptionsBoxPrefab;
    public GameObject MessageBoxPrefab;
    public GameObject InputBoxPrefab;
    public GameObject FullScreenMessageBoxPrefab;
    public GameObject UIUnit;

    public GameObject ParticleSystem;

    public Material ColorSwapMaterial;

    public GameObject LoadPicker;

    public GameObject DiscardedClothing;

    public GameObject SpriteRendererPrefab;
    public GameObject SpriteRenderAnimatedPrefab;
    public GameObject ImagePrefab;
    public BattleReportPanel BattleReportPanel;

    public GameObject CornerCameraView;

    public GameMenu Menu;

    public TutorialScript TutorialScript;

    bool menuOpen = false;

    public bool PureTactical = false;

    public GameObject UnitBase;
    public UnitEditorPanel UnitEditor;
    public SpriteDictionary SpriteDictionary;
    public TacticalBuildingDictionary TacticalBuildingSpriteDictionary;
    public PaletteDictionary PaletteDictionary;
    public SoundManager SoundManager;
    public TacticalEffectPrefabList TacticalEffectPrefabList;
    public bool StrategicControlsLocked { get; private set; }
    public bool CameraTransitioning { get; private set; }
    Vector3 oldCameraPosition;
    Vector3 newCameraPosition;
    float cameraTransitionTime;
    float cameraCurrentTransitionTime;

    StrategicTileType queuedTiletype;
    Village queuedVillage;
    Army queuedInvader;
    Army queuedDefender;
    internal bool queuedTactical;

    float remainingCameraTime;

    internal bool ActiveInput;

    public enum PreviewSkip
    {
        Watch,
        SkipWithStats,
        SkipNoStats
    }

    internal PreviewSkip CurrentPreviewSkip;


    void Start()
    {
        Start_Mode.UI.SetActive(true);
        currentScene = Start_Mode;
        State.GameManager = this;
        Application.wantsToQuit += () => WantsToQuit();
    }

    void Quit()
    {
#if UNITY_EDITOR == false
        confirmedQuit = true;
#endif
        Application.Quit();
    }

    bool WantsToQuit()
    {
#if UNITY_EDITOR
        {
            return true;
        }
#else
        var box = Instantiate(DialogBoxPrefab).GetComponent<DialogBox>();
        box.SetData(() => Quit(), "Quit Game", "Cancel", "Are you sure you want to quit?");
        return confirmedQuit;
#endif

    }

    //private void CopyFilesOfType(string extension, string destination)
    //{
    //    string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());

    //    foreach (string file in files)
    //    {
    //        if (!File.Exists(file)) return;

    //        if (file.EndsWith("." + extension))
    //        {
    //            string fileName = Path.GetFileName(file);
    //            File.Move(file, destination + fileName);

    //        }
    //    }
    //}

    /// <summary>
    /// must be manually initialized
    /// </summary>
    public DialogBox CreateDialogBox()
    {
        return Instantiate(DialogBoxPrefab).GetComponent<DialogBox>();
    }

    public OptionsBox CreateOptionsBox()
    {
        return Instantiate(OptionsBoxPrefab).GetComponent<OptionsBox>();
    }

    public void CreateMessageBox(string text, int timedLife = 0)
    {
        MessageBox box = Instantiate(MessageBoxPrefab).GetComponent<MessageBox>();
        box.InfoText.text = text;
        if (timedLife > 0)
        {
            box.gameObject.AddComponent<TimedLife>();
            var timer = box.GetComponent<TimedLife>();
            timer.Life = timedLife;
        }
    }

    public void CreateFullScreenMessageBox(string text)
    {
        MessageBox box = Instantiate(FullScreenMessageBoxPrefab).GetComponent<MessageBox>();
        box.InfoText.text = text;
    }

    public InputBox CreateInputBox()
    {
        InputBox box = Instantiate(InputBoxPrefab).GetComponent<InputBox>();
        return box;
    }

    public void OpenMenu()
    {
        Menu.UpdateButtons();
        Menu.UIPanel.SetActive(true);
        menuOpen = true;
    }

    public void CloseMenu()
    {
        Menu.UIPanel.SetActive(false);
        menuOpen = false;
        if (currentScene == StrategyMode)
        {
            StrategyMode.ButtonCallback(70);
            StrategyMode.StatusBarUI.Build.gameObject.SetActive(Config.BuildConfig.BuildingSystemEnabled);
        }
    }

    public void CloseStatsScreen()
    {
        StatScreen.AutoClose = false;
        StatScreen.gameObject.SetActive(false);
        SwitchToStrategyMode();
    }

    public void CenterCameraOnTile(int x, int y)
    {
        Camera.transform.position = new Vector3(x, y, Camera.transform.position.z);
    }

    public void SlideCameraToTile(int x, int y)
    {
        oldCameraPosition = Camera.transform.position;
        newCameraPosition = new Vector3(x, y, Camera.transform.position.z);
        float distance = Vector3.Distance(oldCameraPosition, newCameraPosition);
        cameraTransitionTime = .2f + (0.1f * Mathf.Sqrt(distance));
        cameraCurrentTransitionTime = 0;
        CameraTransitioning = true;
    }

    public void SwitchToMainMenu()
    {
        PureTactical = false;
        CurrentScene.CleanUp();
        CurrentScene = Start_Mode;
        Start_Mode.ReturnToStart();
        State.GameManager.StrategyMode.CleanUpLingeringWindows();
    }

    private void Update()
    {
        if (ActiveInput)
            return;
        if (remainingCameraTime > 0)
            remainingCameraTime -= Time.deltaTime;
        if (remainingCameraTime <= 0)
            CornerCameraView.gameObject.SetActive(false);
        if (CameraTransitioning)
            UpdateSlidingCamera();
        if (menuOpen)
            Menu.UpdateDisplay();
        else if (CurrentScene != null)
            CurrentScene.ReceiveInput();
        if (State.TutorialMode)
            TutorialScript.CheckStatus();
    }

    void UpdateSlidingCamera()
    {
        cameraCurrentTransitionTime += Time.deltaTime;
        Camera.transform.position = Vector3.Lerp(oldCameraPosition, newCameraPosition, cameraCurrentTransitionTime / cameraTransitionTime);
        if (cameraCurrentTransitionTime > cameraTransitionTime)
        {
            CameraTransitioning = false;
            ActivateTacticalMode(queuedTiletype, queuedVillage, queuedInvader, queuedDefender);
        }

    }

    internal void CameraCall(Vector3 location)
    {
        if ((Config.TacticalCenterCameraOnAction && CurrentScene == TacticalMode) || (Config.StrategicCenterCameraOnAction && CurrentScene == StrategyMode))
        {
            Camera.transform.position = location;
        }
        else if ((Config.TacticalCameraActionPanel && CurrentScene == TacticalMode) || (Config.StrategicCameraActionPanel && CurrentScene == StrategyMode))
        {
            CornerCameraView.gameObject.SetActive(true);
            PipCamera.SetLocation(location, 5);
            remainingCameraTime = 1;
        }
    }

    internal void CameraCall(Vec2i location) => CameraCall(new Vector3(location.x, location.y, 0));

    public void SwitchToStrategyMode(bool initialLoad = false)
    {
        if (PureTactical)
        {
            CurrentScene.CleanUp();
            PureTactical = false;
            SwitchToMainMenu();
            return;
        }
        bool needsCameraRefresh = currentScene == Start_Mode || currentScene == TacticalMode;
        StrategyMode.gameObject.SetActive(true);
        CurrentScene.CleanUp();
        CurrentScene = StrategyMode;
        StrategyMode.UndoMoves.Clear();
        StrategyMode.Regenerate(initialLoad);
        if (needsCameraRefresh)
            CameraController.LoadStrategicCamera();
        queuedTactical = false;
    }

    public void SwitchToMapEditor()
    {
        if (currentScene == StrategyMode)
        {
            CloseMenu();
            StrategyMode.ClearGraphics();
            MapEditor.gameObject.SetActive(true);
            MapEditor.Initialize(true);
            CurrentScene = MapEditor;
        }
        else if (currentScene == Start_Mode)
        {
            StrategyMode.ClearGraphics();
            MapEditor.gameObject.SetActive(true);
            MapEditor.Initialize(false);
            CurrentScene = MapEditor;
        }
        else
        {
            CreateMessageBox("Can't open map editor from here...");
        }

        return;
    }

    public void ActivatePureTacticalMode(StrategicTileType tiletype, Village village, Army invader, Army defender, TacticalAIType attackerAI, TacticalAIType defenderAI)
    {
        PureTactical = true;
        CurrentScene = TacticalMode;
        TacticalMode.Begin(tiletype, village, invader, defender, attackerAI, defenderAI);
        if (currentScene == TacticalMode)
        {
            CenterCameraOnTile((int)(Config.TacticalSizeX * .5f), (int)(Config.TacticalSizeY * .5f));
            CameraController.SetZoom(Config.TacticalSizeX * .5f);
        }
    }

    public void ActivateTacticalWithDelay(StrategicTileType tiletype, Village village, Army invader, Army defender)
    {
        if (Config.BattleReport && State.GameManager.StrategyMode.IsPlayerTurn == false)
        {
            BattleReportPanel.Activate(village, invader, defender);
            StrategicControlsLocked = true;
            queuedTactical = true;
            queuedTiletype = tiletype;
            queuedVillage = village;
            queuedInvader = invader;
            queuedDefender = defender;
        }
        else if (Config.ScrollToBattleLocation)
        {
            SlideCameraToTile(invader.Position.x, invader.Position.y);
            queuedTactical = true;
            StrategicControlsLocked = true;
            queuedTiletype = tiletype;
            queuedVillage = village;
            queuedInvader = invader;
            queuedDefender = defender;
        }
        else
            ActivateTacticalMode(tiletype, village, invader, defender);

    }


    public void ActivateQueuedTacticalMode(PreviewSkip skipType)
    {
        CurrentPreviewSkip = skipType;
        TacticalBattleOverride battleOverride = (skipType == PreviewSkip.Watch) ? TacticalBattleOverride.ForceWatch : TacticalBattleOverride.ForceSkip;
        ActivateTacticalMode(queuedTiletype, queuedVillage, queuedInvader, queuedDefender, battleOverride);
        BattleReportPanel.gameObject.SetActive(false);

    }


    public void ActivateTacticalMode(StrategicTileType tiletype, Village village, Army invader, Army defender, TacticalBattleOverride tacticalBattleOverride = TacticalBattleOverride.Ignore)
    {
        CameraController.SaveStrategicCamera();
        StrategyMode.gameObject.SetActive(false);
        int defenderSide = defender?.Side ?? village.Side;
        CurrentScene = TacticalMode;

        //If a human is either the army or the garrison, it gets to control both.
        Empire defenderEmpire = State.World.GetEmpireOfSide(defenderSide);
        TacticalAIType defenderType = defenderEmpire?.TacticalAIType ?? TacticalAIType.Full;
        if (village != null && State.World.GetEmpireOfSide(village.Side)?.TacticalAIType == TacticalAIType.None)
            defenderType = TacticalAIType.None;

        Empire attackerEmpire = State.World.GetEmpireOfSide(invader.Side);
        if (State.World.Relations != null)
        {
            if (village != null)
                RelationsManager.VillageAttacked(invader.Empire, village.Empire);
            else
                RelationsManager.ArmyAttacked(invader.Empire, defender.Empire);
            if (village != null && defender != null && defender.Side != village.Side)
            {
                RelationsManager.ArmyAttacked(invader.Empire, defender.Empire);
            }

        }
        TacticalMode.attackerBuildingsInRange = StrategicUtilities.GetActiveEmpireBuildingsWithinXTiles(invader.Position, attackerEmpire, Config.BuildConfig.BuildingPassiveRange);
        if (defender != null)
            TacticalMode.defenderBuildingsInRange = StrategicUtilities.GetActiveEmpireBuildingsWithinXTiles(defender.Position, defenderEmpire, Config.BuildConfig.BuildingPassiveRange);
        else
            TacticalMode.defenderBuildingsInRange = StrategicUtilities.GetActiveEmpireBuildingsWithinXTiles(village.Position, defenderEmpire, Config.BuildConfig.BuildingPassiveRange);

        TacticalMode.ClearNames();
        TacticalMode.Begin(tiletype, village, invader, defender, attackerEmpire?.TacticalAIType ?? TacticalAIType.Full, defenderType, tacticalBattleOverride);
        StrategicControlsLocked = false;
        if (currentScene == TacticalMode)
        {
            CenterCameraOnTile((int)(Config.TacticalSizeX * .5f), (int)(Config.TacticalSizeY * .5f));
            CameraController.SetZoom(Config.TacticalSizeX * .5f);
        }
    }

    public void SwitchToTacticalOnLoadedGame()
    {
        StrategyMode.gameObject.SetActive(false);
        CurrentScene = TacticalMode;
        PureTactical = State.World.MainEmpires == null;
        CameraController.LoadTacticalCamera();
    }

    public void ActivateRecruitMode(Village village, Empire empire, Recruit_Mode.ActivatingEmpire activatingEmpire = Recruit_Mode.ActivatingEmpire.Self)
    {
        Recruit_Mode.Begin(village, empire, activatingEmpire);
        CurrentScene = Recruit_Mode;
    }

    public void ActivateRecruitMode(Empire actingEmpire, Army army, Recruit_Mode.ActivatingEmpire activatingEmpire = Recruit_Mode.ActivatingEmpire.Self)
    {
        Recruit_Mode.BeginWithoutVillage(actingEmpire, army, activatingEmpire);
        CurrentScene = Recruit_Mode;
    }

    public void ActivateRecruitMode(MercenaryHouse house, Empire actingEmpire, Army army, Recruit_Mode.ActivatingEmpire activatingEmpire = Recruit_Mode.ActivatingEmpire.Self)
    {
        Recruit_Mode.BeginWithMercenaries(house, actingEmpire, army, activatingEmpire);
        CurrentScene = Recruit_Mode;
    }

    public void ActivateEndSceneWin(int side)
    {
        var winners = State.World.MainEmpires.Where(s => s.IsAlly(State.World.GetEmpireOfSide(side)));
        string winner = "";
        bool first = true;
        foreach (Empire emp in winners)
        {
            if (first)
                winner += $"{emp.Name}";
            else
                winner += $", {emp.Name}";
            first = false;
        }

        EndScene.Win(winner);
        CurrentScene = EndScene;
    }

    public void ActivateEndSceneLose()
    {
        EndScene.Lose(State.World.ActingEmpire.Race);
        CurrentScene = EndScene;
    }

    public void OpenSaveLoadMenu() => Menu.OpenSaveLoadMenu();

    public void AskQuickLoad()
    {
        if (FindObjectOfType<DialogBox>() != null)
            return;
        DialogBox box = GameObject.Instantiate(DialogBoxPrefab).GetComponent<DialogBox>();
        box.SetData(DoQuickLoad, "Load Game", "Cancel", "Are you sure you want to quickload?");
    }

    void DoQuickLoad()
    {
        StatScreen.gameObject.SetActive(false);
        State.Load($"{State.SaveDirectory}Quicksave.sav");
    }

    public void ClearPureTactical() => PureTactical = false;
}
