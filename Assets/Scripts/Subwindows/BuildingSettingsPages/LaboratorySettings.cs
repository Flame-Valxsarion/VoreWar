using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaboratorySettings : MonoBehaviour
{
    public InputField UpfrontCost;
    public InputField BaseUnitPrice;
    public InputField BulkDiscount;
    public InputField BulkMin;
    public InputField BulkMax;
    public InputField BaseRollCount;
    public InputField BaseTraitChance;
    public InputField AIPotionMult;

    internal void Save()
    {
        Config.BuildConfig.LaboratoryUpfrontCost = int.TryParse(UpfrontCost.text, out int smi) ? smi : 100;
        Config.BuildConfig.LaboratoryBaseUnitPrice = int.TryParse(BaseUnitPrice.text, out int sma) ? sma : 100;
        Config.BuildConfig.LaboratoryBulkDiscount = float.TryParse(BulkDiscount.text, out float omi) ? omi : 0.5f;
        Config.BuildConfig.LaboratoryBulkMin = int.TryParse(BulkMin.text, out int oma) ? oma : 5;
        Config.BuildConfig.LaboratoryBulkMax = int.TryParse(BulkMax.text, out int mmi) ? mmi : 50;
        Config.BuildConfig.LaboratoryBaseRollCount = int.TryParse(BaseRollCount.text, out int mmu) ? mmu : 4;
        Config.BuildConfig.LaboratoryBaseTraitChance = float.TryParse(BaseTraitChance.text, out float mme) ? mme : 0.6f;
        Config.BuildConfig.LaboratoryAIPotionMult = int.TryParse(AIPotionMult.text, out int pm) ? pm: 1;

    }

    internal void Load()
    {
        UpfrontCost.text = Config.BuildConfig.LaboratoryUpfrontCost.ToString();
        BaseUnitPrice.text = Config.BuildConfig.LaboratoryBaseUnitPrice.ToString();
        BulkDiscount.text = Config.BuildConfig.LaboratoryBulkDiscount.ToString();
        BulkMin.text = Config.BuildConfig.LaboratoryBulkMin.ToString();
        BulkMax.text = Config.BuildConfig.LaboratoryBulkMax.ToString();
        BaseRollCount.text = Config.BuildConfig.LaboratoryBaseRollCount.ToString();
        BaseTraitChance.text = Config.BuildConfig.LaboratoryBaseTraitChance.ToString();
        AIPotionMult.text = Config.BuildConfig.LaboratoryAIPotionMult.ToString();
    }
}
