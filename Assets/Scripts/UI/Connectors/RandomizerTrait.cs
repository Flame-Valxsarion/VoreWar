using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomizerTrait : MonoBehaviour
{
    internal int id;
    public InputField name;
    public InputField chance;
    public InputField count;
    public InputField level;
    public Button PickTagsBtn;
    public Button CloneBtn;
    public Button RemoveBtn;


    [AllowEditing]
    internal Dictionary<Traits, bool> TraitDictionary;

    public void OpenTraitsDict()
    {
        State.GameManager.VariableEditor.Open(this);
    }
    public int ExposeCount()
    {
        return TraitDictionary.Where(s => s.Value).Count();
    }
}