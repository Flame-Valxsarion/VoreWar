using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class VariableEditor : MonoBehaviour
{
    object EditingObject;

    public Transform Folder;

    public GameObject Toggle;
    public GameObject InputField;
    public GameObject Slider;
    public GameObject Dropdown;

    public TextMeshProUGUI TooltipText;

    internal Dictionary<Traits, bool> TempDictionary;
    internal List<Toggle> DictToggleList;

    internal const BindingFlags Bindings = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

    internal void Open<T>(T obj)
    {
        if (obj == null)
        {
            Debug.Log("Tried to open on null object!");
            return;
        }

        gameObject.SetActive(true);
        EditingObject = obj;

        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }

        ProcessFields(obj);
        //ProcessProperties(obj);
    }

    private void ProcessFields<T>(T obj)
    {
        FieldInfo[] fields = obj.GetType().GetFields(Bindings);
        foreach (FieldInfo field in fields)
        {
            if (Attribute.GetCustomAttribute(field, typeof(AllowEditing)) == null)
                continue;
            if (field.FieldType == typeof(bool))
            {
                var newObj = Instantiate(Toggle, Folder);
                var toggle = newObj.GetComponent<Toggle>();
                newObj.name = field.Name;
                toggle.isOn = (bool)field.GetValue(obj);
                toggle.GetComponentInChildren<Text>().text = field.Name; //Designed to be overwritten by proper
                foreach (Attribute attr in Attribute.GetCustomAttributes(field))
                {
                    if (attr is ProperNameAttribute proper)
                    {
                        toggle.GetComponentInChildren<Text>().text = proper.Name;
                    }
                    if (attr is DescriptionAttribute desc)
                    {
                        toggle.gameObject.AddComponent<VariableScreenTooltip>();
                        toggle.GetComponent<VariableScreenTooltip>().text = desc.Description;
                    }
                }
            }
            if (field.FieldType == typeof(string))
            {
                var newObj = Instantiate(InputField, Folder);
                var input = newObj.GetComponent<CombinedInputfield>();
                newObj.name = field.Name;
                input.Inputfield.text = (string)field.GetValue(obj);
                newObj.GetComponent<CombinedInputfield>().Text.text = field.Name; //Designed to be overwritten by proper
                foreach (Attribute attr in Attribute.GetCustomAttributes(field))
                {
                    if (attr is ProperNameAttribute proper)
                    {
                        newObj.GetComponent<CombinedInputfield>().Text.text = proper.Name;
                    }
                    if (attr is DescriptionAttribute desc)
                    {
                        newObj.gameObject.AddComponent<VariableScreenTooltip>();
                        newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
                    }
                }
            }
            if (field.FieldType == typeof(int))
            {
                var newObj = Instantiate(InputField, Folder);
                var input = newObj.GetComponent<CombinedInputfield>();
                newObj.name = field.Name;
                input.Inputfield.text = field.GetValue(obj).ToString();
                input.Inputfield.characterValidation = TMP_InputField.CharacterValidation.Integer;
                input.Inputfield.characterLimit = 5;
                newObj.GetComponent<CombinedInputfield>().Text.text = field.Name; //Designed to be overwritten by proper
                foreach (Attribute attr in Attribute.GetCustomAttributes(field))
                {
                    if (attr is ProperNameAttribute proper)
                    {
                        newObj.GetComponent<CombinedInputfield>().Text.text = proper.Name;
                    }
                    if (attr is DescriptionAttribute desc)
                    {
                        newObj.gameObject.AddComponent<VariableScreenTooltip>();
                        newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
                    }
                }
            }
            if (field.FieldType == typeof(float))
            {
                var newObj = Instantiate(Slider, Folder);
                var slider = newObj.GetComponentInChildren<Slider>();
                newObj.name = field.Name;
                newObj.GetComponentInChildren<TextMeshProUGUI>().text = field.Name; //Designed to be overwritten by proper
                foreach (Attribute attr in Attribute.GetCustomAttributes(field))
                {
                    if (attr is ProperNameAttribute proper)
                    {
                        newObj.GetComponentInChildren<TextMeshProUGUI>().text = proper.Name;
                    }
                    if (attr is DescriptionAttribute desc)
                    {
                        newObj.gameObject.AddComponent<VariableScreenTooltip>();
                        newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
                    }
                    if (attr is FloatRangeAttribute range)
                    {
                        slider.minValue = range.Min;
                        slider.maxValue = range.Max;
                    }
                }
                slider.value = (float)field.GetValue(obj); // Must be set after the min and max are set
            }
            if (field.FieldType.BaseType == typeof(Enum))
            {
                var newObj = Instantiate(Dropdown, Folder);
                var dropdown = newObj.GetComponentInChildren<TMP_Dropdown>();
                newObj.name = field.Name;
                Type enumType = field.FieldType;
                var values = Enum.GetValues(enumType);
                dropdown.ClearOptions();
                for (int i = 0; i < values.Length; i++)
                {
                    dropdown.options.Add(new TMP_Dropdown.OptionData(values.GetValue(i).ToString()));
                }
                dropdown.RefreshShownValue();
                dropdown.value = (int)field.GetValue(obj);
                newObj.GetComponentInChildren<TextMeshProUGUI>().text = field.Name; //Designed to be overwritten by proper
                foreach (Attribute attr in Attribute.GetCustomAttributes(field))
                {
                    if (attr is ProperNameAttribute proper)
                    {
                        newObj.GetComponentInChildren<TextMeshProUGUI>().text = proper.Name;
                    }
                    if (attr is DescriptionAttribute desc)
                    {
                        newObj.gameObject.AddComponent<VariableScreenTooltip>();
                        newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
                    }
                }
            }

            if (field.FieldType == typeof(Dictionary<Traits, bool>))
            {
                TempDictionary = (Dictionary<Traits, bool>)field.GetValue(obj);
                if (TempDictionary != null)
                {
                    var newObject = Instantiate(Toggle, Folder);
                    var allToggle = newObject.GetComponent<Toggle>();
                    newObject.name = $"ALL";
                    allToggle.GetComponentInChildren<Text>().text = "ALL";
                    DictToggleList = new List<Toggle>();
                    foreach (var entry in TempDictionary.OrderBy(s =>
                    {
                        if (s.Key >= (Traits)1000)
                            return "AAA" + s.Key.ToString();
                           return s.Key >= Traits.LightningSpeed ? "ZZZ" + s.Key.ToString() : s.Key.ToString();
                       }))
                    {
                        var newObj = Instantiate(Toggle, Folder);
                        var toggle = newObj.GetComponent<Toggle>();
                        if (entry.Key >= (Traits)6000)
                        {
                            var rlName = State.ConditionalTraitList.Find(r => (Traits)r.id == entry.Key)?.name ?? entry.Key.ToString();
                            newObj.name = $"UsingDictionary^{rlName}";
                            toggle.GetComponentInChildren<Text>().text = rlName;
                            toggle.gameObject.AddComponent<VariableScreenTooltip>();
                            toggle.GetComponent<VariableScreenTooltip>().text = "A conditional Trait. Use the conditional trait editor to view and edit.";
                        }
                        else if (entry.Key >= (Traits)3000)
                        {
                            var rlName = State.CustomTraitList.Find(r => (Traits)r.id == entry.Key)?.name ?? entry.Key.ToString();
                            newObj.name = $"UsingDictionary^{rlName}";
                            toggle.GetComponentInChildren<Text>().text = rlName;
                            toggle.gameObject.AddComponent<VariableScreenTooltip>();
                            toggle.GetComponent<VariableScreenTooltip>().text = State.CustomTraitList.Find(r => (Traits)r.id == entry.Key)?.description ?? entry.Key.ToString();
                        }
                        else if (entry.Key >= (Traits)1000)
                        {
                            var rlName = State.RandomizeLists.Find(r => (Traits)r.id == entry.Key)?.name ?? entry.Key.ToString();
                            newObj.name = $"UsingDictionary^{rlName}";
                            toggle.GetComponentInChildren<Text>().text = rlName;
                            toggle.gameObject.AddComponent<VariableScreenTooltip>();
                            toggle.GetComponent<VariableScreenTooltip>().text = "A Random Trait.";
                        }
                        else
                        {
                            newObj.name = $"UsingDictionary^{entry.Key}";
                            toggle.GetComponentInChildren<Text>().text = entry.Key.ToString();
                            toggle.gameObject.AddComponent<VariableScreenTooltip>();
                            toggle.GetComponent<VariableScreenTooltip>().text = HoveringTooltip.GetTraitData(entry.Key);
                        }
                        toggle.isOn = entry.Value;
                        DictToggleList.Add(toggle);
                    }
                    allToggle.isOn = DictToggleList.All(t => t.isOn);
                    allToggle.onValueChanged.AddListener(delegate { CheckAll(allToggle.isOn); });
                }
                //var newObj = Instantiate(Toggle, Folder);
                //var toggle = newObj.GetComponent<Toggle>();
                //newObj.name = field.Name;
                //toggle.isOn = (bool)field.GetValue(obj);
                //toggle.GetComponentInChildren<Text>().text = field.Name; //Designed to be overwritten by proper
                //foreach (Attribute attr in Attribute.GetCustomAttributes(field))
                //{
                //    if (attr is ProperNameAttribute proper)
                //    {
                //        toggle.GetComponentInChildren<Text>().text = proper.Name;
                //    }
                //    if (attr is DescriptionAttribute desc)
                //    {
                //        toggle.gameObject.AddComponent<VariableScreenTooltip>();
                //        toggle.GetComponent<VariableScreenTooltip>().text = desc.Description;
                //    }
                //}
            }


        }
    }

    private void CheckAll(bool isOn)
    {
        DictToggleList.ForEach(t => t.isOn = isOn);
    }


    //private void ProcessProperties<T>(T obj)
    //{
    //    PropertyInfo[] properties = obj.GetType().GetProperties(Bindings);
    //    foreach (PropertyInfo property in properties)
    //    {
    //        if (property.PropertyType == typeof(bool))
    //        {
    //            var newObj = Instantiate(Toggle, Folder);
    //            var toggle = newObj.GetComponent<Toggle>();
    //            newObj.name = property.Name;
    //            toggle.isOn = (bool)property.GetValue(obj);
    //            toggle.GetComponentInChildren<TextMeshProUGUI>().text = property.Name; //Designed to be overwritten by proper
    //            foreach (Attribute attr in Attribute.GetCustomAttributes(property))
    //            {
    //                if (attr is ProperNameAttribute proper)
    //                {
    //                    toggle.GetComponentInChildren<TextMeshProUGUI>().text = proper.Name;
    //                }
    //                if (attr is DescriptionAttribute desc)
    //                {
    //                    toggle.gameObject.AddComponent<VariableScreenTooltip>();
    //                    toggle.GetComponent<VariableScreenTooltip>().text = desc.Description;
    //                }
    //            }
    //        }
    //        if (property.PropertyType == typeof(string))
    //        {
    //            var newObj = Instantiate(InputField, Folder);
    //            var input = newObj.GetComponent<CombinedInputfield>();
    //            newObj.name = property.Name;
    //            input.Inputfield.text = (string)property.GetValue(obj);
    //            newObj.GetComponent<CombinedInputfield>().Text.text = property.Name; //Designed to be overwritten by proper
    //            foreach (Attribute attr in Attribute.GetCustomAttributes(property))
    //            {
    //                if (attr is ProperNameAttribute proper)
    //                {
    //                    newObj.GetComponent<CombinedInputfield>().Text.text = proper.Name;
    //                }
    //                if (attr is DescriptionAttribute desc)
    //                {
    //                    newObj.gameObject.AddComponent<VariableScreenTooltip>();
    //                    newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
    //                }
    //            }
    //        }
    //        //if (field.FieldType == typeof(int))
    //        //{
    //        //    var newObj = Instantiate(InputField, Folder);
    //        //    var input = newObj.GetComponent<CombinedInputfield>();
    //        //    newObj.name = field.Name;
    //        //    input.Inputfield.text = ((int)field.GetValue(obj)).ToString();
    //        //    foreach (Attribute attr in Attribute.GetCustomAttributes(field))
    //        //    {
    //        //        if (attr is ProperNameAttribute proper)
    //        //        {
    //        //            newObj.GetComponentInChildren<TextMeshProUGUI>().text = proper.Name;
    //        //        }
    //        //        if (attr is DescriptionAttribute desc)
    //        //        {
    //        //            newObj.gameObject.AddComponent<VariableScreenTooltip>();
    //        //            newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
    //        //        }
    //        //    }
    //        //}
    //        if (property.PropertyType == typeof(float))
    //        {
    //            var newObj = Instantiate(Slider, Folder);
    //            var slider = newObj.GetComponentInChildren<Slider>();
    //            newObj.name = property.Name;
    //            newObj.GetComponentInChildren<TextMeshProUGUI>().text = property.Name; //Designed to be overwritten by proper
    //            foreach (Attribute attr in Attribute.GetCustomAttributes(property))
    //            {
    //                if (attr is ProperNameAttribute proper)
    //                {
    //                    newObj.GetComponentInChildren<TextMeshProUGUI>().text = proper.Name;
    //                }
    //                if (attr is DescriptionAttribute desc)
    //                {
    //                    newObj.gameObject.AddComponent<VariableScreenTooltip>();
    //                    newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
    //                }
    //                if (attr is FloatRangeAttribute range)
    //                {
    //                    slider.minValue = range.Min;
    //                    slider.maxValue = range.Max;
    //                }
    //            }
    //            slider.value = (float)property.GetValue(obj); // Must be set after the min and max are set
    //        }
    //        if (property.PropertyType.BaseType == typeof(Enum))
    //        {
    //            var newObj = Instantiate(Dropdown, Folder);
    //            var dropdown = newObj.GetComponentInChildren<TMP_Dropdown>();
    //            newObj.name = property.Name;
    //            Type enumType = property.PropertyType;
    //            var values = Enum.GetValues(enumType);
    //            dropdown.ClearOptions();
    //            for (int i = 0; i < values.Length; i++)
    //            {
    //                dropdown.options.Add(new TMP_Dropdown.OptionData(values.GetValue(i).ToString()));
    //            }
    //            dropdown.RefreshShownValue();
    //            dropdown.value = (int)property.GetValue(obj);
    //            newObj.GetComponentInChildren<TextMeshProUGUI>().text = property.Name; //Designed to be overwritten by proper
    //            foreach (Attribute attr in Attribute.GetCustomAttributes(property))
    //            {
    //                if (attr is ProperNameAttribute proper)
    //                {
    //                    newObj.GetComponentInChildren<TextMeshProUGUI>().text = proper.Name;
    //                }
    //                if (attr is DescriptionAttribute desc)
    //                {
    //                    newObj.gameObject.AddComponent<VariableScreenTooltip>();
    //                    newObj.GetComponent<VariableScreenTooltip>().text = desc.Description;
    //                }
    //            }
    //        }
    //    }
    //}

    internal void ChangeToolTip(string text)
    {
        TooltipText.text = text;
    }

    public void SaveAndClose()
    {
        bool needSave = false;
        int children = Folder.childCount;
        for (int i = 0; i < children; i++)
        {
            var obj = Folder.GetChild(i).gameObject;
            var drop = obj.GetComponentInChildren<TMP_Dropdown>();
            if (drop != null)
            {
                EditingObject.GetType().GetField(obj.name, Bindings)?.SetValue(EditingObject, drop.value);
                continue;
            }
            var toggle = obj.GetComponentInChildren<Toggle>();
            if (toggle != null)
            {
                EditingObject.GetType().GetField(obj.name, Bindings)?.SetValue(EditingObject, toggle.isOn);
                if (obj.name.Contains("UsingDictionary"))
                {
                    var split = obj.name.Split('^');
                    if (Enum.TryParse(split[1], out Traits trait))
                    {
                        TempDictionary[trait] = obj.GetComponentInChildren<Toggle>().isOn;
                    } else
                    {
                        var rt = State.RandomizeLists.Find(r => r.name == split[1]);
                        var ct = State.CustomTraitList.Find(r => r.name == split[1]);
                        var cdt = State.ConditionalTraitList.Find(r => r.name == split[1]);

                        if (rt != null)
                            TempDictionary[(Traits)rt.id] = obj.GetComponentInChildren<Toggle>().isOn;
                        if (ct != null)
                            TempDictionary[(Traits)ct.id] = obj.GetComponentInChildren<Toggle>().isOn;
                        if (cdt != null)
                            TempDictionary[(Traits)ct.id] = obj.GetComponentInChildren<Toggle>().isOn;
                    }
                    needSave = true;
                }
                continue;
            }
            var slider = obj.GetComponentInChildren<Slider>();
            if (slider != null)
            {
                EditingObject.GetType().GetField(obj.name, Bindings)?.SetValue(EditingObject, slider.value);
                continue;
            }
            var input = obj.GetComponentInChildren<TMP_InputField>();
            if (input != null)
            {
                if (int.TryParse(input.text, out int result))
                {
                    var attr = EditingObject.GetType().GetField(obj.name)?.GetCustomAttribute(typeof(IntegerRangeAttribute));
                    if (attr != null)
                    {
                        var range = (IntegerRangeAttribute)attr;
                        result = Mathf.Clamp(result, range.Min, range.Max);
                    }

                    EditingObject.GetType().GetField(obj.name, Bindings)?.SetValue(EditingObject, result);
                }

                continue;
            }
            Debug.LogWarning("Couldn't handle object!");
        }
        gameObject.SetActive(false);
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }
        if (State.GameManager.Menu.WorldSettingsUI.gameObject.activeSelf)
        {
            State.GameManager.Menu.WorldSettingsUI.ShowSettings();
        }
        if (needSave)
        {
            if (State.AssimilateList.Initialized)
                State.AssimilateList.Save();
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }
    }


}
