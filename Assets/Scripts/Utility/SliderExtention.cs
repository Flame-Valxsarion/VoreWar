using UnityEngine.UI;

public static class SliderExtensions
{
    public static void AddListenerOnce(this Slider.SliderEvent buttonClickedEvent,
            UnityEngine.Events.UnityAction<float> action)
    {
        buttonClickedEvent.RemoveListener(action);
        buttonClickedEvent.AddListener(action);
    }
}