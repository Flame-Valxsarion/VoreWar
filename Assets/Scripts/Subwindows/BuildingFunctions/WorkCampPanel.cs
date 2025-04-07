using MapObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkCampPanel : MonoBehaviour
{

    public Button BuyWood;
    public Button SellWood;
    public Button BuyStone;
    public Button SellStone;
    public Button BuyNM;
    public Button SellNM;
    public Button BuyOres;
    public Button SellOres;
    public Button BuyPrefabs;
    public Button SellPrefabs;
    public Button BuyMS;
    public Button SellMS;

    public TextMeshProUGUI GoldCount;

    public TextMeshProUGUI CampGold;


    public TextMeshProUGUI[] OwnedItems;
    public TextMeshProUGUI[] InStockItems;
    public TextMeshProUGUI[] ItemPrice;

    public void Open(ConstructibleBuilding building)
    {
        RefreshUI(building);
        BuyWood.onClick.AddListenerOnce(() =>
        {
            PurchaceItem(ConstructionResourceType.wood, (WorkCamp)building);
            RefreshUI(building);
        });
        SellWood.onClick.AddListenerOnce(() =>
        {
            SellItem(ConstructionResourceType.wood, (WorkCamp)building);
            RefreshUI(building);
        });
        BuyStone.onClick.AddListenerOnce(() =>
        {
            PurchaceItem(ConstructionResourceType.stone, (WorkCamp)building);
            RefreshUI(building);
        });
        SellStone.onClick.AddListenerOnce(() =>
        {
            SellItem(ConstructionResourceType.stone, (WorkCamp)building);
            RefreshUI(building);
        });
        BuyNM.onClick.AddListenerOnce(() =>
        {
            PurchaceItem(ConstructionResourceType.naturalmaterials, (WorkCamp)building);
            RefreshUI(building);
        });
        SellNM.onClick.AddListenerOnce(() =>
        {
            SellItem(ConstructionResourceType.naturalmaterials, (WorkCamp)building);
            RefreshUI(building);
        });
        BuyOres.onClick.AddListenerOnce(() =>
        {
            PurchaceItem(ConstructionResourceType.ores, (WorkCamp)building);
            RefreshUI(building);
        });
        SellOres.onClick.AddListenerOnce(() =>
        {
            SellItem(ConstructionResourceType.ores, (WorkCamp)building);
            RefreshUI(building);
        });
        BuyPrefabs.onClick.AddListenerOnce(() =>
        {
            PurchaceItem(ConstructionResourceType.prefabs, (WorkCamp)building);
            RefreshUI(building);
        });
        SellPrefabs.onClick.AddListenerOnce(() =>
        {
            SellItem(ConstructionResourceType.prefabs, (WorkCamp)building);
            RefreshUI(building);
        });
        BuyMS.onClick.AddListenerOnce(() =>
        {
            PurchaceItem(ConstructionResourceType.manastones, (WorkCamp)building);
            RefreshUI(building);
        });
        BuyMS.onClick.AddListenerOnce(() =>
        {
            SellItem(ConstructionResourceType.manastones, (WorkCamp)building);
            RefreshUI(building);
        });
    }

    void RefreshUI(ConstructibleBuilding building)
    {
        if (((WorkCamp)building).postUpgrade.built && building.active)
        {
            BuyWood.interactable = true;
            SellWood.interactable = true;
            BuyStone.interactable = true;
            SellStone.interactable = true;
        }

        if (((WorkCamp)building).merchantUpgrade.built && building.active)
        {
            BuyNM.interactable = true;
            SellNM.interactable = true;
            BuyOres.interactable = true;
            SellOres.interactable = true;
            BuyPrefabs.interactable = true;
            SellPrefabs.interactable = true;
            BuyMS.interactable = true;
            SellMS.interactable = true;
        }

        GoldCount.text = building.Owner.Gold.ToString();

        OwnedItems[0].text = building.Owner.constructionResources.Wood.ToString();
        OwnedItems[1].text = building.Owner.constructionResources.Stone.ToString();
        OwnedItems[2].text = building.Owner.constructionResources.NaturalMaterials.ToString();
        OwnedItems[3].text = building.Owner.constructionResources.Ores.ToString();
        OwnedItems[4].text = building.Owner.constructionResources.Prefabs.ToString();
        OwnedItems[5].text = building.Owner.constructionResources.ManaStones.ToString();


        InStockItems[0].text = ((WorkCamp)building).inStockItems.Wood.ToString();
        InStockItems[1].text = ((WorkCamp)building).inStockItems.Stone.ToString();
        InStockItems[2].text = ((WorkCamp)building).inStockItems.NaturalMaterials.ToString();
        InStockItems[3].text = ((WorkCamp)building).inStockItems.Ores.ToString();
        InStockItems[4].text = ((WorkCamp)building).inStockItems.Prefabs.ToString();
        InStockItems[5].text = ((WorkCamp)building).inStockItems.ManaStones.ToString();


        ItemPrice[0].text = Config.BuildConfig.WorkCampItemPrice.Wood.ToString();
        ItemPrice[1].text = Config.BuildConfig.WorkCampItemPrice.Stone.ToString();
        ItemPrice[2].text = Config.BuildConfig.WorkCampItemPrice.NaturalMaterials.ToString();
        ItemPrice[3].text = Config.BuildConfig.WorkCampItemPrice.Ores.ToString();
        ItemPrice[4].text = Config.BuildConfig.WorkCampItemPrice.Prefabs.ToString();
        ItemPrice[5].text = Config.BuildConfig.WorkCampItemPrice.ManaStones.ToString();

        if (((WorkCamp)building).inStockItems.Wood <= 0 ||  GetItemPrice(ConstructionResourceType.wood, (WorkCamp)building) > building.Owner.Gold)
        {
            BuyWood.interactable = false;
        }
        if (building.Owner.constructionResources.Wood <= 0 || ((WorkCamp)building).currentGold < GetItemPrice(ConstructionResourceType.wood, (WorkCamp)building))
        {
            SellWood.interactable = false;
        }

        if (((WorkCamp)building).inStockItems.Stone <= 0 ||  GetItemPrice(ConstructionResourceType.stone, (WorkCamp)building) > building.Owner.Gold)
        {
            BuyStone.interactable = false;
        }
        if (building.Owner.constructionResources.Stone <= 0 || ((WorkCamp)building).currentGold < GetItemPrice(ConstructionResourceType.stone, (WorkCamp)building))
        {
            SellStone.interactable = false;
        }

        if (((WorkCamp)building).inStockItems.NaturalMaterials <= 0 ||  GetItemPrice(ConstructionResourceType.naturalmaterials, (WorkCamp)building) > building.Owner.Gold)
        {
            BuyNM.interactable = false;
        }
        if (building.Owner.constructionResources.NaturalMaterials <= 0 || ((WorkCamp)building).currentGold < GetItemPrice(ConstructionResourceType.naturalmaterials, (WorkCamp)building))
        {
            SellNM.interactable = false;
        }

        if (((WorkCamp)building).inStockItems.Ores <= 0 ||  GetItemPrice(ConstructionResourceType.ores, (WorkCamp)building) > building.Owner.Gold)
        {
            BuyOres.interactable = false;
        }
        if (building.Owner.constructionResources.Ores <= 0 || ((WorkCamp)building).currentGold < GetItemPrice(ConstructionResourceType.ores, (WorkCamp)building))
        {
            SellOres.interactable = false;
        }

        if (((WorkCamp)building).inStockItems.Prefabs <= 0 ||  GetItemPrice(ConstructionResourceType.prefabs, (WorkCamp)building) > building.Owner.Gold)
        {
            BuyPrefabs.interactable = false;
        }
        if (building.Owner.constructionResources.Prefabs <= 0 || ((WorkCamp)building).currentGold < GetItemPrice(ConstructionResourceType.prefabs, (WorkCamp)building))
        {
            SellPrefabs.interactable = false;
        }

        if (((WorkCamp)building).inStockItems.ManaStones <= 0 ||  GetItemPrice(ConstructionResourceType.manastones, (WorkCamp)building) > building.Owner.Gold)
        {
            BuyMS.interactable = false;
        }
        if (building.Owner.constructionResources.ManaStones <= 0 || ((WorkCamp)building).currentGold < GetItemPrice(ConstructionResourceType.manastones, (WorkCamp)building))
        {
            SellMS.interactable = false;
        }
    }

    void PurchaceItem(ConstructionResourceType type, WorkCamp building)
    {
        building.inStockItems.SpendResource(type, 1);
        building.Owner.SpendGold(GetItemPrice(type, building));
        building.Owner.constructionResources.AddResource(type, 1);
    }
    
    void SellItem(ConstructionResourceType type, WorkCamp building)
    {
        building.inStockItems.AddResource(type, 1);
        building.Owner.AddGold(GetItemPrice(type, building));
        building.Owner.constructionResources.SpendResource(type, 1);
    }
    int GetItemPrice(ConstructionResourceType type, WorkCamp building)
    {
        switch (type)
        {
            case ConstructionResourceType.wood:
                return Config.BuildConfig.WorkCampItemPrice.Wood;
            case ConstructionResourceType.stone:
                return Config.BuildConfig.WorkCampItemPrice.Stone;
            case ConstructionResourceType.ores:
                return Config.BuildConfig.WorkCampItemPrice.Ores;
            case ConstructionResourceType.naturalmaterials:
                return Config.BuildConfig.WorkCampItemPrice.NaturalMaterials;
            case ConstructionResourceType.prefabs:
                return Config.BuildConfig.WorkCampItemPrice.Prefabs;
            case ConstructionResourceType.manastones:
                return Config.BuildConfig.WorkCampItemPrice.ManaStones;
            default:
                break;
        }
        return 0;
    }
}
