using UnityEngine.UI;

public static class ButtonExtensions
{
    public static void AddListenerOnce(this Button.ButtonClickedEvent buttonClickedEvent,
            UnityEngine.Events.UnityAction action)
    {
        buttonClickedEvent.RemoveListener(action);
        buttonClickedEvent.AddListener(action);
    }
}