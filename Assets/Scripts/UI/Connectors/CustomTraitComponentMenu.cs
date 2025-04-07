using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CustomTraitComponentMenu : MonoBehaviour
{
    CustomTraitBoost trait;
    public CustomTrait CustomTrait;
    public Transform SelectedFolder;
    public Transform AvailFolder;
    internal Dictionary<CustomTraitComp,Button> SelectedComps;
    internal Dictionary<CustomTraitComp, Button> AvailibleComps;
    internal Button AddBtnInstance;
    public Button CompBtn;

    internal void Open(int id)
    {
        gameObject.SetActive(true);
        CustomTrait.gameObject.SetActive(false);
        SelectedComps = new Dictionary<CustomTraitComp, Button>();
        AvailibleComps = new Dictionary<CustomTraitComp, Button>();
        trait = trait = State.CustomTraitList.Where(x => x.id == id).FirstOrDefault();
        int children = SelectedFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(SelectedFolder.GetChild(i).gameObject);
            Destroy(AvailFolder.GetChild(i).gameObject);
        }
        Setup();
    }

    private void Setup()
    {
        int maxvalue = (int)CustomTraitComp.enumcounter;
        for (int j = 0; maxvalue - 1 >= j; j++)
        {
            SelectedComps.Add((CustomTraitComp)j, CreateSelectedButton(((CustomTraitComp)j).ToString(), (CustomTraitComp)j));
            AvailibleComps.Add((CustomTraitComp)j, CreateAvaildButton(((CustomTraitComp)j).ToString(), (CustomTraitComp)j));
        }
        CheckVis();
    }

    private Button CreateSelectedButton(string name, CustomTraitComp comp)
    {
        AddBtnInstance = Instantiate(CompBtn, SelectedFolder);
        var btn = AddBtnInstance.GetComponent<Button>();
        var btnTxt = btn.GetComponentInChildren<Text>();
        btnTxt.text = name;
        btn.onClick.AddListener(() =>
        {
            trait.comps.Remove(comp);
            CheckVis();
        });
        return btn;
    }
    private Button CreateAvaildButton(string name, CustomTraitComp comp)
    {
        AddBtnInstance = Instantiate(CompBtn, AvailFolder);
        var btn = AddBtnInstance.GetComponent<Button>();
        var btnTxt = btn.GetComponentInChildren<Text>();
        btnTxt.text = name;
        btn.onClick.AddListener(() =>
        {
            trait.comps.Add(comp,0);
            CheckVis();
        });
        return btn;
    }

    private void CheckVis()
    {
        foreach (var comp in SelectedComps.Keys)
        {
            if (trait.comps.Keys.Contains(comp))
            {
                SelectedComps[comp].gameObject.SetActive(true);
                AvailibleComps[comp].gameObject.SetActive(false);
            }
            else
            {
                AvailibleComps[comp].gameObject.SetActive(true);
                SelectedComps[comp].gameObject.SetActive(false);
            }
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
        CustomTrait.RefreshActive();
        CustomTrait.gameObject.SetActive(true);
    }
}