using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayCustomTraitCompTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CustomTraitComp value;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (State.GameManager.Menu.CustomTraitEditor.CustomTrait.gameObject.activeSelf)
        {
            value = GetComponent<CustomTraitCompMod>().comp;
            State.GameManager.Menu.CustomTraitEditor.CustomTrait.ChangeToolTip(value);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (State.GameManager.Menu.CustomTraitEditor.CustomTrait.gameObject.activeSelf)
            State.GameManager.Menu.CustomTraitEditor.CustomTrait.ChangeToolTip(CustomTraitComp.enumcounter);
    }
}
