using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int value;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (State.GameManager.Menu.ContentSettings.SettingsPanels[11].gameObject.activeSelf)
            State.GameManager.Menu.ContentSettings.ChangeBuildingToolTip(value);
        else if (State.GameManager.Menu.ContentSettings.gameObject.activeSelf)
            State.GameManager.Menu.ContentSettings.ChangeToolTip(value);
        else if (State.GameManager.Menu.Options.gameObject.activeSelf)
            State.GameManager.Menu.Options.ChangeToolTip(value);
        else if (State.GameManager.Start_Mode.CreateStrategicGame.gameObject.activeSelf)
            State.GameManager.Start_Mode.CreateStrategicGame.ChangeToolTip(value);
        else if (State.GameManager.Start_Mode.CreateTacticalGame.gameObject.activeSelf)
            State.GameManager.Start_Mode.CreateTacticalGame.ChangeToolTip(value);
        else if (State.GameManager.Menu.RaceEditor.gameObject.activeSelf)
        {
            State.GameManager.HoveringTooltip.UpdateInformationDefaultTooltip(value);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (State.GameManager.Menu.ContentSettings.SettingsPanels[11].gameObject.activeSelf)
            State.GameManager.Menu.ContentSettings.ChangeBuildingToolTip(0);
        else if (State.GameManager.Menu.ContentSettings.gameObject.activeSelf)
            State.GameManager.Menu.ContentSettings.ChangeToolTip(0);
        else if (State.GameManager.Menu.Options.gameObject.activeSelf)
            State.GameManager.Menu.Options.ChangeToolTip(0);
        else if (State.GameManager.Start_Mode.CreateStrategicGame.gameObject.activeSelf)
            State.GameManager.Start_Mode.CreateStrategicGame.ChangeToolTip(0);
        else if (State.GameManager.Start_Mode.CreateTacticalGame.gameObject.activeSelf)
            State.GameManager.Start_Mode.CreateTacticalGame.ChangeToolTip(0);
        else if (State.GameManager.Menu.RaceEditor.gameObject.activeSelf)
        {
            State.GameManager.HoveringTooltip.gameObject.SetActive(false);
        }
    }
}
