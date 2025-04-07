using UnityEngine;
using UnityEngine.EventSystems;

public class MapEditorBuilding : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public MapEditor.MapBuildingType Type;

    public void OnPointerClick(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetBuildingType(Type, transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetBuildingTooltip(Type);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        State.GameManager.MapEditor.SetBlankTooltip();
    }
}

