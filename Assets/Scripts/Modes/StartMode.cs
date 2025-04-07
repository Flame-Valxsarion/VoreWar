using OdinSerializer.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class StartMode : SceneBase
{
    public Text versionNumber;
    public Text miscText;

    public Button Preset1;
    public Button Preset2;
    public Button Preset3;

    public Button TagAlertButton;
    public Transform TaggedTraitFolder;
    public GameObject TaggedTraitObject;

    public TaggedTraitPrefab TaggedTraitPrefab;
    bool trait_tagger_open;

    public void Start()
    {
        State.GameManager.Menu.Options.LoadFromStored();
        State.GameManager.Menu.CheatMenu.LoadFromStored();
        versionNumber.text = $"Version: {State.Version}";
        State.GameManager.Menu.ContentSettings.Refresh();
        Preset1.onClick.AddListener(() => SetPreset(1));
        Preset2.onClick.AddListener(() => SetPreset(2));
        Preset3.onClick.AddListener(() => SetPreset(3));
        State.LoadRaceData();
        RunTaggedTraitCheck();
        trait_tagger_open = false;
        TagAlertButton.onClick.AddListener(() =>
        {
            if (trait_tagger_open)
            {
                CloseTaggedTraitCheck();
                trait_tagger_open = false;
            }
            else 
            {
                OpenTaggedTraitCheck();
                trait_tagger_open = true;
            }
        });
    }

    public CreateStrategicGame CreateStrategicGame;
    public void CreateStrategic()
    {
        UI.SetActive(false);
        CreateStrategicGame.gameObject.SetActive(true);
        CreateStrategicGame.ClearState();
    }

    public CreateTacticalGame CreateTacticalGame;
    public void CreateTactical()
    {
        UI.SetActive(false);
        CreateTacticalGame.gameObject.SetActive(true);
        CreateTacticalGame.Open();

    }

    private void SetPreset(int value)
    {
        State.ChangeRaceSlotUsed(value);
        UpdatePresetsVisible();
    }

    public void UpdatePresetsVisible()
    {
        if (State.RaceSlot <= 1)
        {
            Preset1.interactable = false;
            Preset2.interactable = true;
            Preset3.interactable = true;
        }
        else if (State.RaceSlot == 2)
        {
            Preset1.interactable = true;
            Preset2.interactable = false;
            Preset3.interactable = true;
        }
        if (State.RaceSlot == 3)
        {
            Preset1.interactable = true;
            Preset2.interactable = true;
            Preset3.interactable = false;
        }
    }

    public void ChangeAssimilableTraits()
    {
        State.AssimilateList.Initialize();
        State.GameManager.VariableEditor.Open(State.AssimilateList);
    }

    public void ChangeRandomizableTraits()
    {
        State.GameManager.Menu.OpenRandomizerTraits();
    }

    public void ModifyCustomTraits()
    {
        State.GameManager.Menu.OpenCustomTraits();
    }

    public void ModifyCondTraits()
    {
        State.GameManager.Menu.OpenCondTraits();
    }

    public void ReturnToStart()
    {
        State.TutorialMode = false;
        State.GameManager.TutorialScript = null;
        UI.SetActive(true);
        CreateStrategicGame.gameObject.SetActive(false);
        CreateTacticalGame.gameObject.SetActive(false);
        State.GameManager.Menu.ContentSettings.Refresh();
    }

    public void TutorialMode()
    {
        State.GameManager.TutorialScript = new TutorialScript();
        State.Load($"{Application.streamingAssetsPath}{System.IO.Path.DirectorySeparatorChar}Tutorial.sav", tutorial: true);
    }

    public void LoadGame()
    {
        State.GameManager.OpenSaveLoadMenu();
    }

    public void MapEditor()
    {
        State.World = new World(true);
        State.GameManager.SwitchToMapEditor();
    }

    public override void ReceiveInput()
    {
        //Vector2 currentMousePos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }

    public override void CleanUp()
    {
    }


    public void RunTaggedTraitCheck()
    {
        int children = TaggedTraitFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(TaggedTraitFolder.GetChild(i).gameObject);
        }
        if (State.UntaggedTraits.Count > 0)
        {
            TagAlertButton.gameObject.SetActive(true);

            foreach (var item in State.UntaggedTraits.Keys)
            {
                var obj = Instantiate(TaggedTraitPrefab, TaggedTraitFolder);
                var tt = obj.GetComponent<TaggedTraitPrefab>();
                tt.traitName.text = item.name;
                tt.traitTier.text = item.tier;
                tt.tags.text = "";
                tt.id = 0;
                tt.description.text = HoveringTooltip.GetTraitData(item.traitEnum);
                tt.taggedTrait = item;
                tt.save.onClick.AddListener(() =>
                {
                    if (tt.tag.Length > 0)
                    {
                        State.UntaggedTraits.Remove(item);
                        RunTaggedTraitCheck();
                    }
                });
                tt.addButton.gameObject.SetActive(false);
                tt.removeButton.gameObject.SetActive(false);
            }
        }
        else 
        {
            TagAlertButton.gameObject.SetActive(false);
            TaggedTraitFolder.gameObject.SetActive(false);
            TaggedTraitObject.gameObject.SetActive(false);
        }
    }

    public void OpenTaggedTraitCheck()
    {
        TaggedTraitFolder.gameObject.SetActive(true);
        TaggedTraitObject.gameObject.SetActive(true);
    }

    public void CloseTaggedTraitCheck()
    {
        TaggedTraitFolder.gameObject.SetActive(false);
        TaggedTraitObject.gameObject.SetActive(false);
    }


}
