using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkCampSettings : MonoBehaviour
{
    public InputField GoldPerTurn;
    
    public InputField WoodPrice;
    public InputField NaturalMaterialsPrice;
    public InputField PrefabsPrice;
    public InputField StonePrice;
    public InputField OresPrice;
    public InputField ManaStonesPrice;

    public InputField WoodStock;
    public InputField NaturalMaterialsStock;
    public InputField PrefabsStock;
    public InputField StoneStock;
    public InputField OresStock;
    public InputField ManaStonesStock;

    internal void Save()
    {
        Config.BuildConfig.WorkCampGoldPerTurn = int.TryParse(GoldPerTurn.text, out int gpt) ? gpt : 100;
        Config.BuildConfig.WorkCampTurnStock.SetResources(
            int.TryParse(WoodStock.text, out int ws) ? ws : 10,
            int.TryParse(StoneStock.text, out int ss) ? ss : 10,
            int.TryParse(NaturalMaterialsStock.text, out int nms) ? nms : 5,
            int.TryParse(OresStock.text, out int os) ? os : 5,
            int.TryParse(PrefabsStock.text, out int ps) ? ps : 3,
            int.TryParse(ManaStonesStock.text, out int mss) ?  mss : 3
            );

        Config.BuildConfig.WorkCampItemPrice.SetResources(
            int.TryParse(WoodPrice.text, out int wp) ? wp : 10,
            int.TryParse(StonePrice.text, out int sp) ? sp : 10,
            int.TryParse(NaturalMaterialsPrice.text, out int nmp) ? nmp : 25,
            int.TryParse(OresPrice.text, out int op) ? op : 25,
            int.TryParse(PrefabsPrice.text, out int pp) ? pp : 50,
            int.TryParse(ManaStonesPrice.text, out int msp) ?  msp : 50
            );
    }

    internal void Load()
    {
        GoldPerTurn.text = Config.BuildConfig.WorkCampGoldPerTurn.ToString();

        WoodStock.text = Config.BuildConfig.WorkCampTurnStock.Wood.ToString();
        StoneStock.text = Config.BuildConfig.WorkCampTurnStock.Wood.ToString();
        NaturalMaterialsStock.text = Config.BuildConfig.WorkCampTurnStock.Stone.ToString();
        OresStock.text = Config.BuildConfig.WorkCampTurnStock.Ores.ToString();
        PrefabsStock.text = Config.BuildConfig.WorkCampTurnStock.Prefabs.ToString();
        ManaStonesStock.text = Config.BuildConfig.WorkCampTurnStock.ManaStones.ToString();

        WoodPrice.text = Config.BuildConfig.WorkCampItemPrice.Wood.ToString();
        StonePrice.text = Config.BuildConfig.WorkCampItemPrice.Stone.ToString();
        NaturalMaterialsPrice.text = Config.BuildConfig.WorkCampItemPrice.NaturalMaterials.ToString();
        OresPrice.text = Config.BuildConfig.WorkCampItemPrice.Ores.ToString();
        PrefabsPrice.text = Config.BuildConfig.WorkCampItemPrice.Prefabs.ToString();
        ManaStonesPrice.text = Config.BuildConfig.WorkCampItemPrice.ManaStones.ToString();
    }
}
