using UnityEngine;

public class Shop
{
    Empire empire;
    Unit unit;
    readonly Village village;
    Army army;
    readonly int unitItemSlots = 2;
    readonly bool inTown = true;

    const int maxSellSlots = 5;

    ShopPanel shopUI;

    public Shop(Empire empire, Village village, Unit unit, Army army, ShopPanel newShopUI, bool inTown)
    {
        this.empire = empire;
        this.village = village;
        this.unit = unit;
        this.army = army;
        this.inTown = inTown;
        shopUI = newShopUI;
        unitItemSlots = unit.Items.Length;
        if (shopUI.SellPanels.Length == 0)
        {
            shopUI.SellPanels = new ShopSellPanel[maxSellSlots];
            for (int x = 0; x < maxSellSlots; x++)
            {
                shopUI.SellPanels[x] = Object.Instantiate(shopUI.SellPrefab, new Vector3(0, 0), new Quaternion(), shopUI.ButtonFolder).GetComponent<ShopSellPanel>();
                int slot = x;
                shopUI.SellPanels[x].SellButton.onClick.AddListener(() => State.GameManager.Recruit_Mode.ShopSellItem(slot)); //These are done this way to avoid tying it to the first shop instance
                shopUI.SellPanels[x].MoveToInventoryButton.onClick.AddListener(() => State.GameManager.Recruit_Mode.ShopTransferToInventory(slot));
            }
        }
        if (shopUI.BuyPanels.Length == 0)
        {
            shopUI.BuyPanels = new ShopBuyPanel[State.World.ItemRepository.NumItems];

            for (int x = 0; x < State.World.ItemRepository.NumItems; x++)
            {
                shopUI.BuyPanels[x] = Object.Instantiate(shopUI.BuyPrefab, new Vector3(0, 0), new Quaternion(), shopUI.ButtonFolder).GetComponent<ShopBuyPanel>();
                //shopUI.BuyItemButton[x].GetComponent<RectTransform>().sizeDelta = new Vector2(600, 60);
                int type = x;
                shopUI.BuyPanels[x].BuyButton.onClick.AddListener(() => State.GameManager.Recruit_Mode.ShopGenerateBuyButton(type)); //These are done this way to avoid tying it to the first shop instance
                shopUI.BuyPanels[x].TakeFromInventoryButton.onClick.AddListener(() => State.GameManager.Recruit_Mode.ShopTransferItemToCharacter(type));
                shopUI.BuyPanels[x].SellFromInventoryButton.onClick.AddListener(() => State.GameManager.Recruit_Mode.ShopSellItemFromInventory(type));
                Item item = State.World.ItemRepository.GetItem(x);
                if (item is SpellBook book)
                    shopUI.BuyPanels[x].Description.text = $"{item.Name} - cost {item.Cost} - {book.DetailedDescription().Replace('\n', ' ')}";
                else
                    shopUI.BuyPanels[x].Description.text = $"{item.Name} - cost {item.Cost} - {item.Description}";
            }
        }
        RegenButtonTextAndClickability();


    }

    public void TransferItemToInventory(int slot)
    {
        army.ItemStock.AddItem(State.World.ItemRepository.GetItemType(unit.GetItem(slot)));
        unit.SetItem(null, slot);
        RegenButtonTextAndClickability();
    }

    public void TransferItemToCharacter(int type)
    {
        int slot = -1;
        for (int i = 0; i < unit.Items.Length; i++)
        {
            if (unit.Items[i] == null)
            {
                slot = i;
                break;
            }
        }
        if (slot == -1)
            return;
        if (army.ItemStock.TakeItem((ItemType)type))
        {
            unit.SetItem(State.World.ItemRepository.GetItem(type), slot);
        }
        RegenButtonTextAndClickability();
    }

    public void SellItemFromInventory(int type)
    {
        if (army.ItemStock.TakeItem((ItemType)type))
        {
            empire.AddGold(State.World.ItemRepository.GetItem(type).Cost / 2);
        }
        RegenButtonTextAndClickability();
    }

    public void SellItem(int slot)
    {
        SellItem(empire, unit, slot);
        RegenButtonTextAndClickability();
    }

    public static void SellItem(Empire empire, Unit unit, int slot)
    {
        if (unit.GetItem(slot) != null)
        {
            empire.AddGold(unit.GetItem(slot).Cost / 2);
            unit.SetItem(null, slot);
        }
    }

    public bool BuyItem(int type)
    {
        bool bought = BuyItem(empire, unit, State.World.ItemRepository.GetItem(type));
        if (bought)
            RegenButtonTextAndClickability();
        return bought;
    }

    public static bool BuyItem(Empire empire, Unit unit, Item type)
    {
        int freeItemSlot = -1;
        for (int j = 0; j < unit.Items.Length; j++)
        {
            if (unit.GetItem(j) == null)
            {
                freeItemSlot = j;
                break;
            }
        }

        if (freeItemSlot == -1 || empire.Gold < type.Cost)
        {
            return false;
        }

        empire.SpendGold(type.Cost);
        State.World.Stats.SpentGoldOnArmyEquipment(type.Cost, empire.Side);
        unit.SetItem(type, freeItemSlot);
        return true;
    }

    internal int BuyForAllCost(int type)
    {
        var item = State.World.ItemRepository.GetItem(type);
        int cost = 0;
        foreach (Unit unit in army?.Units)
        {
            if (unit.HasFreeItemSlot() == false || unit.FixedGear)
                continue;
            if (unit.GetItemSlot(item) != -1)
                continue;
            cost += item.Cost;
        }
        return cost;
    }

    internal void BuyForAll(int type)
    {
        var item = State.World.ItemRepository.GetItem(type);
        foreach (Unit unit in army?.Units)
        {
            if (unit.HasFreeItemSlot() == false || unit.FixedGear)
                continue;
            if (unit.GetItemSlot(item) != -1)
                continue;
            BuyItem(empire, unit, item);
        }
        RegenButtonTextAndClickability();
    }

    void RegenButtonTextAndClickability()
    {
        RegenBuyClickable();
        RegenSellText();
    }

    void RegenBuyClickable()
    {
        for (int i = 0; i < shopUI.BuyPanels.Length; i++)
        {
            if (shopUI.BuyPanels[i] == null)
                continue;
            var racePar = RaceParameters.GetTraitData(unit);
            if (racePar.CanUseRangedWeapons == false && racePar.CanUseMeleeWeapons == true  && State.World.ItemRepository.ItemIsRangedWeapon(i))
            {
                shopUI.BuyPanels[i].gameObject.SetActive(false);
                continue;
            }
            if (racePar.CanUseRangedWeapons == true && racePar.CanUseMeleeWeapons == false  && State.World.ItemRepository.ItemIsMeleeWeapon(i))
            {
                shopUI.BuyPanels[i].gameObject.SetActive(false);
                continue;
            }
            Item item = State.World.ItemRepository.GetItem(i);
            if (racePar.CanUseRangedWeapons == false && racePar.CanUseMeleeWeapons == false && item is Weapon)
            {
                shopUI.BuyPanels[i].gameObject.SetActive(false);
                continue;
            }
            if ((unit.HasTrait(Traits.Feral) || unit.FixedGear) && item is Weapon)
            {
                shopUI.BuyPanels[i].gameObject.SetActive(false);
                continue;
            }

            shopUI.BuyPanels[i].gameObject.SetActive(true);

            shopUI.BuyPanels[i].BuyButton.interactable = inTown;
            shopUI.BuyPanels[i].SellFromInventoryButton.interactable = inTown && army.ItemStock.HasItem((ItemType)i);

            if (item is SpellBook book)
            {
                if (book.Tier > ((village?.NetBoosts.SpellLevels ?? -5) + 1))
                {
                    if (army.ItemStock.HasItem((ItemType)i) == false)
                    {
                        shopUI.BuyPanels[i].gameObject.SetActive(false);
                        continue;
                    }
                    else
                    {
                        shopUI.BuyPanels[i].BuyButton.interactable = false;
                    }

                }

            }
            if (item is Equipment equipment)
            {
                if (equipment.Tier > ((village?.NetBoosts.EquipmentLevels ?? -5) + 1) && equipment.Tier > 0)
                {
                    if (army.ItemStock.HasItem((ItemType)i) == false)
                    {
                        shopUI.BuyPanels[i].gameObject.SetActive(false);
                        continue;
                    }
                    else
                    {
                        shopUI.BuyPanels[i].BuyButton.interactable = false;
                    }

                }
            }

            if (item is Potion)
            {
                shopUI.BuyPanels[i].gameObject.SetActive(false);
            }


            shopUI.BuyPanels[i].TakeFromInventoryButton.interactable = army.ItemStock.HasItem((ItemType)i);
            shopUI.BuyPanels[i].InventoryButtonText.text = $"Take from army inventory (You have {army.ItemStock.ItemCount((ItemType)i)})";
            for (int j = 0; j < unit.Items.Length; j++)
            {
                if (unit.Items[j] == item)
                {
                    shopUI.BuyPanels[i].BuyButton.interactable = false;
                    shopUI.BuyPanels[i].TakeFromInventoryButton.interactable = false;
                }
            }
            if (item.Cost > empire.Gold)
                shopUI.BuyPanels[i].BuyButton.interactable = false;


        }
    }

    void RegenSellText()
    {
        //rebuild sell buttons
        for (int i = 0; i < maxSellSlots; i++)
        {
            shopUI.SellPanels[i].gameObject.SetActive(i < unitItemSlots);
            if (i >= unitItemSlots)
                continue; //continue instead of break so it will hide the rest
            if (unit.GetItem(i) != null)
            {
                shopUI.SellPanels[i].Description.text = $"{unit.GetItem(i).Name} -- sells for {unit.GetItem(i).Cost / 2}";
                shopUI.SellPanels[i].SellButton.interactable = inTown && unit.GetItem(i).LockedItem == false;
                shopUI.SellPanels[i].MoveToInventoryText.text = $"Move to army inventory (You have {army.ItemStock.ItemCount(State.World.ItemRepository.GetItemType(unit.GetItem(i)))})";
                shopUI.SellPanels[i].MoveToInventoryButton.interactable = unit.GetItem(i).LockedItem == false;
            }
            else
            {
                shopUI.SellPanels[i].Description.text = "empty";
                shopUI.SellPanels[i].MoveToInventoryText.text = $"Move to inventory";
                shopUI.SellPanels[i].SellButton.interactable = false;
                shopUI.SellPanels[i].MoveToInventoryButton.interactable = false;
            }
        }

    }

}
