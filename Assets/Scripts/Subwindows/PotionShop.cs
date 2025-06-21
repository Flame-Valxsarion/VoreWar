using System.Linq;
using UnityEngine;

public class PotionShop
{
    Empire empire;
    Unit unit;
    readonly Village village;
    Army army;
    readonly int unitItemSlots = 2;
    readonly bool inTown = true;

    ItemPotionPanel PotionUI;

    public PotionShop(Empire empire, Village village, Unit unit, Army army, ItemPotionPanel newPotionUI, bool inTown)
    {
        this.empire = empire;
        this.village = village;
        this.unit = unit;
        this.army = army;
        this.inTown = inTown;
        PotionUI = newPotionUI;
        unitItemSlots = unit.Items.Length;
        int potionCount = ItemType.OmniPotion - ItemType.HealthPotion + 1;
        if (PotionUI.EquippedPotions.Length == 0)
        {
            PotionUI.EquippedPotions = new EquippedPotionPanel[potionCount];
            for (int x = 0; x < potionCount; x++)
            {
                PotionUI.EquippedPotions[x] = Object.Instantiate(PotionUI.EquippedPrefab, new Vector3(0, 0), new Quaternion(), PotionUI.ButtonFolder).GetComponent<EquippedPotionPanel>();
                int slot = x + (int)ItemType.HealthPotion;
                PotionUI.EquippedPotions[x].SellButton.onClick.AddListener(() => State.GameManager.Recruit_Mode.PotionShopSellItem(slot)); //These are done this way to avoid tying it to the first shop instance
                PotionUI.EquippedPotions[x].MoveToInventoryButton.onClick.AddListener(() => State.GameManager.Recruit_Mode.PotionShopTransferToInventory(slot));
                PotionUI.EquippedPotions[x].IncButton.onClick.AddListener(() => State.GameManager.Recruit_Mode.PotionShopIncreaseCount(unit, slot));
                PotionUI.EquippedPotions[x].DecButton.onClick.AddListener(() => State.GameManager.Recruit_Mode.PotionShopDecCount(unit, slot));
                PotionUI.EquippedPotions[x].gameObject.SetActive(false);
            }
            RegenEquipClickable();
        }
        if (PotionUI.BuyPotionPanels.Length == 0)
        {
            PotionUI.BuyPotionPanels = new BuyPotionPanel[potionCount];

            for (int x = 0; x < potionCount ; x++)
            {
                PotionUI.BuyPotionPanels[x] = Object.Instantiate(PotionUI.BuyPrefab, new Vector3(0, 0), new Quaternion(), PotionUI.ButtonFolder).GetComponent<BuyPotionPanel>();
                //shopUI.BuyItemButton[x].GetComponent<RectTransform>().sizeDelta = new Vector2(600, 60);
                int type = x + (int)ItemType.HealthPotion;
                PotionUI.BuyPotionPanels[x].BuyButton.onClick.AddListener(() => State.GameManager.Recruit_Mode.PotionShopGenerateBuyButton(type)); //These are done this way to avoid tying it to the first shop instance
                PotionUI.BuyPotionPanels[x].BuyTenButton.onClick.AddListener(() => State.GameManager.Recruit_Mode.PotionShopGenerateBuyTenButton(type)); //These are done this way to avoid tying it to the first shop instance
                PotionUI.BuyPotionPanels[x].EquipFromFromInventoryButton.onClick.AddListener(() => State.GameManager.Recruit_Mode.PotionShopTransferItemToCharacter(type));
                PotionUI.BuyPotionPanels[x].EquipToAllButton.onClick.AddListener(() => State.GameManager.Recruit_Mode.PotionShopTransferItemToAll(type));
                PotionUI.BuyPotionPanels[x].SellFromInventoryButton.onClick.AddListener(() => State.GameManager.Recruit_Mode.PotionShopSellItemFromInventory(type));
                Item item = State.World.ItemRepository.GetItem(type);
                PotionUI.BuyPotionPanels[x].Description.text = $"{item.Name} - cost {item.Cost} - {item.Description}";
            }
        }
        RegenButtonTextAndClickability();
    }

    public void TransferItemToInventory(int slot)
    {
        ItemType item = State.World.ItemRepository.GetItemType(PotionUI.EquippedPotions[slot - (int)ItemType.HealthPotion].associatedPotion);

        army.ItemStock.AddItem(item, unit.EquippedPotions[(int)item][0]);
        unit.EquippedPotions.Remove((int)item);
        PotionUI.EquippedPotions[slot - (int)ItemType.HealthPotion].gameObject.SetActive(false);
        RegenButtonTextAndClickability();
    }

    public void TransferItemToCharacter(int type)
    {
        if (unit.EquippedPotions.ContainsKey(type))
        {
            IncreaseCount(unit, type);
        }
        else
        {
            unit.EquippedPotions.Add(type, new int[] { 1, 1 });
            army.ItemStock.TakeItem((ItemType)type);
        }
        RegenButtonTextAndClickability();
    }

    public void TransferItemToAll(int type)
    {
        Item item = State.World.ItemRepository.GetItem(type);

        foreach (var soldier in army.Units)
        {
            if (!army.ItemStock.HasItem(State.World.ItemRepository.GetItemType(item)))
                break;
            int totalEquippedPotions = 0;
            foreach (var potion in soldier.EquippedPotions)
            {
                totalEquippedPotions += potion.Value[1];
            }
            if (!(Config.PotionSlots - 1 >= totalEquippedPotions))
                continue;
            if (soldier.EquippedPotions.ContainsKey(type))
            {
                IncreaseCount(soldier, type);
            }
            else
            {
                soldier.EquippedPotions.Add(type, new int[] { 1, 1 });
                army.ItemStock.TakeItem(State.World.ItemRepository.GetItemType(item));
            }
        }
        RegenButtonTextAndClickability();
    }

    public static void TransferItemToAllInArmy(ItemType type, Army army)
    {
        Item item = State.World.ItemRepository.GetItem(type);

        foreach (var soldier in army.Units)
        {
            if (!army.ItemStock.HasItem(State.World.ItemRepository.GetItemType(item)))
                break;
            int totalEquippedPotions = 0;
            foreach (var potion in soldier.EquippedPotions)
            {
                totalEquippedPotions += potion.Value[1];
            }
            if (!(Config.PotionSlots - 1 >= totalEquippedPotions))
                continue;
            if (soldier.EquippedPotions.ContainsKey((int)type))
            {
                soldier.EquippedPotions.TryGetValue((int)type, out var currentCount);
                soldier.EquippedPotions[(int)type][0] = currentCount[1] + 1;
                if (soldier.EquippedPotions[(int)type][0] > soldier.EquippedPotions[(int)type][1])
                {
                    soldier.EquippedPotions[(int)type][1] = currentCount[1] + 1;
                }
                army.ItemStock.TakeItem(State.World.ItemRepository.GetItemType(item));
            }
            else
            {
                soldier.EquippedPotions.Add((int)type, new int[] { 1, 1 });
                army.ItemStock.TakeItem(State.World.ItemRepository.GetItemType(item));
            }
        }
    }

    public void SellItemFromInventory(int type)
    {
        if (army.ItemStock.TakeItem((ItemType)type))
        {
            empire.AddGold(State.World.ItemRepository.GetItem(type).Cost);
        }
        RegenButtonTextAndClickability();
    }

    public void SellItem(int slot)
    {
        SellItem(empire, unit, slot);
        RegenButtonTextAndClickability();
    }

    public void SellItem(Empire empire, Unit unit, int slot)
    {
        Potion item = PotionUI.EquippedPotions[slot].associatedPotion;
        PotionUI.EquippedPotions[slot].gameObject.SetActive(false);
        unit.EquippedPotions.Remove(slot);
        empire.AddGold(unit.GetItem(slot).Cost);
    }

    public bool BuyItem(int type, int count)
    {
        bool bought = BuyItem(empire, unit, State.World.ItemRepository.GetItem(type), count);
        if (bought)
        {
            RegenButtonTextAndClickability();
        }
        return bought;
    }

    public bool BuyItem(Empire empire, Unit unit, Item type, int count)
    {
        if (empire.Gold < type.Cost * count)
        {
            return false;
        }

        empire.SpendGold(type.Cost * count);
        State.World.Stats.SpentGoldOnArmyEquipment(type.Cost, empire.Side);
        army.ItemStock.AddItem(State.World.ItemRepository.GetItemType(type), count);

        return true;
    }

    public static int BuyItemForArmy(Empire empire, Army army, Item type, int count)
    {
        if (empire.Gold < type.Cost * count)
        {
            return -1;
        }

        empire.SpendGold(type.Cost * count);
        State.World.Stats.SpentGoldOnArmyEquipment(type.Cost, empire.Side);
        army.ItemStock.AddItem(State.World.ItemRepository.GetItemType(type), count);

        return type.Cost * count;
    }

    internal int MultCost(int type, int count)
    {
        var item = State.World.ItemRepository.GetItem(type);
        int cost = item.Cost;
        return cost * count;
    }

    public bool IncreaseCount(Unit unit, int type)
    {
        unit.EquippedPotions.TryGetValue(type, out var currentCount);
        unit.EquippedPotions[type][0] = currentCount[1] + 1;
        if (unit.EquippedPotions[type][0] > unit.EquippedPotions[type][1])
        {
            unit.EquippedPotions[type][1] = currentCount[1] + 1;
        }
        army.ItemStock.TakeItem((ItemType)type);
        RegenButtonTextAndClickability();
        return true;
    }

    public bool DecreaseCount(Unit unit, int type)
    {
        unit.EquippedPotions.TryGetValue(type, out var currentCount);
        if (currentCount[0] > 0)
        {
            unit.EquippedPotions[type][0] = currentCount[1] - 1;
            army.ItemStock.AddItem((ItemType)type);
        }
        unit.EquippedPotions[type][1] = currentCount[1] - 1;
        RegenButtonTextAndClickability();
        return true;
    }

    void RegenButtonTextAndClickability()
    {
        RegenBuyClickable();
        RegenEquipClickable();
    }

    void RegenBuyClickable()
    {
        for (int i = 0; i < PotionUI.BuyPotionPanels.Length; i++)
        {
            if (PotionUI.BuyPotionPanels[i] == null)
                continue;

            int type = i + (int)ItemType.HealthPotion;

            Item item = State.World.ItemRepository.GetItem(type);

            bool townHasTier = ((Potion)item).Tier > ((village?.NetBoosts.PotionLevel ?? -5) + 1) && ((Potion)item).Tier > 0;

            if (townHasTier &&!army.ItemStock.HasItem((ItemType)type))
            {
                PotionUI.BuyPotionPanels[i].gameObject.SetActive(false);
                continue;
            }

            PotionUI.BuyPotionPanels[i].gameObject.SetActive(true);

            PotionUI.BuyPotionPanels[i].BuyButton.interactable = inTown && !townHasTier;
            PotionUI.BuyPotionPanels[i].BuyTenButton.interactable = inTown && !townHasTier;
            PotionUI.BuyPotionPanels[i].SellFromInventoryButton.interactable = inTown && army.ItemStock.HasItem((ItemType)type);
            int totalEquippedPotions = 0;
            foreach (var potion in unit.EquippedPotions)
            {
                totalEquippedPotions += potion.Value[1];
            }
            PotionUI.BuyPotionPanels[i].EquipFromFromInventoryButton.interactable = army.ItemStock.HasItem((ItemType)type) && Config.PotionSlots - 1 >= totalEquippedPotions;
            PotionUI.BuyPotionPanels[i].EquipToAllButton.interactable = army.ItemStock.HasItem((ItemType)type) && Config.PotionSlots - 1 >= totalEquippedPotions;
            PotionUI.BuyPotionPanels[i].InventoryButtonText.text = $"Equip To Unit\n(You have {army.ItemStock.ItemCount((ItemType)type)})";
  
            if (item.Cost > empire.Gold)
                PotionUI.BuyPotionPanels[i].BuyButton.interactable = false;
  
            if (item.Cost * 10 > empire.Gold)
                PotionUI.BuyPotionPanels[i].BuyTenButton.interactable = false;
        }
    }

    void RegenEquipClickable()
    {
        for (int i = 0; i < PotionUI.EquippedPotions.Length; i++)
        {
            if (PotionUI.EquippedPotions[i] == null)
                continue;
            PotionUI.EquippedPotions[i].gameObject.SetActive(false);
            int type = i + (int)ItemType.HealthPotion;
            Item item = State.World.ItemRepository.GetItem(type);

            if (unit.EquippedPotions.Count <= 0)
                continue;
            if (!unit.EquippedPotions.Keys.Any(p => p == type))
                continue;
            Debug.Log(item.Name);
            PotionUI.EquippedPotions[i].gameObject.SetActive(true);

            PotionUI.EquippedPotions[i].SellButton.interactable = inTown;

            PotionUI.EquippedPotions[i].DecButton.interactable = unit.EquippedPotions[type][1] > 1;
            int totalEquippedPotions = 0;
            foreach (var potion in unit.EquippedPotions) 
            {
                totalEquippedPotions += potion.Value[1];
            }
            PotionUI.EquippedPotions[i].IncButton.interactable = Config.PotionSlots - 1 >= totalEquippedPotions && army.ItemStock.ItemCount((ItemType)type) > 0;

            PotionUI.EquippedPotions[i].MoveToInventoryText.text = $"Move to inventory ({army.ItemStock.ItemCount((ItemType)type)})";
            
            PotionUI.EquippedPotions[i].Description.text = $"{item.Name} -- sells for {item.Cost}";
            PotionUI.EquippedPotions[i].EquippedCount.text = $"{unit.EquippedPotions[type][0]} / {unit.EquippedPotions[type][1]} ";
            PotionUI.EquippedPotions[i].associatedPotion = (Potion)item;
        }
    }

}
