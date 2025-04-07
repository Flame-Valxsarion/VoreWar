using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomTraitListItem : MonoBehaviour
{
    internal int id;
    public TextMeshProUGUI name;
    public Button PickTagsBtn;
    public Button CloneBtn;

    public void OpenTraitsDict()
    {
        State.GameManager.VariableEditor.Open(this);
    }
}