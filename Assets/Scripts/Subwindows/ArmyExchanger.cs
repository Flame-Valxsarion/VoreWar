using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ArmyExchanger : MonoBehaviour
{
    UIUnitSprite[] leftUnitSprites;
    UIUnitSprite[] rightUnitSprites;

    public Transform LeftFolder;
    public Transform RightFolder;

    internal Army LeftArmy;
    internal Army RightArmy;

    internal bool LeftReceived;
    internal bool RightReceived;

    InfoPanel infoPanel;
    public UnitInfoPanel info;

    Actor_Unit[] leftActors;
    Actor_Unit[] rightActors;
    Unit[] leftUnits;
    Unit[] rightUnits;

    int leftSelected;
    int rightSelected;

    public GameObject LeftSelector;
    public GameObject RightSelector;

    public Button MoveToRight;
    public Button MoveToLeft;

    public GameObject ItemExchangeSet;
    public Transform ItemExchangeFolder;


    public GameObject ItemExchangePanel;

    ItemExchangeControl[] ItemExchangers;

    bool fullArmies;

    internal void Initialize(Army left, Army right)
    {
        if (infoPanel == null)
        {
            infoPanel = new InfoPanel(info);
        }
        else
            Select(0);
        fullArmies = left.RemainnigSize <= 0 && right.RemainnigSize <= 0;
        if (fullArmies)
        {
            MoveToLeft.GetComponentInChildren<Text>().text = "Exchange Selected Units";
            MoveToRight.GetComponentInChildren<Text>().text = "Exchange Selected Units";
        }
        else
        {
            MoveToLeft.GetComponentInChildren<Text>().text = "Transfer Unit to Left Army";
            MoveToRight.GetComponentInChildren<Text>().text = "Transfer Unit to Right Army";
        }
        if (leftUnitSprites == null || leftUnitSprites.Length != Config.MaximumPossibleArmy)
        {
            leftUnitSprites = new UIUnitSprite[Config.MaximumPossibleArmy];
            rightUnitSprites = new UIUnitSprite[Config.MaximumPossibleArmy];
            for (int i = 0; i < Config.MaximumPossibleArmy; i++)
            {
                leftUnitSprites[i] = Instantiate(State.GameManager.UIUnit, LeftFolder).GetComponent<UIUnitSprite>();
            }
            for (int i = 0; i < Config.MaximumPossibleArmy; i++)
            {
                rightUnitSprites[i] = Instantiate(State.GameManager.UIUnit, RightFolder).GetComponent<UIUnitSprite>();
            }
        }
        LeftArmy = left;
        RightArmy = right;

        LeftReceived = false;
        RightReceived = false;

        leftSelected = 0;
        rightSelected = 0;

        leftActors = new Actor_Unit[Config.MaximumPossibleArmy];
        leftUnits = new Unit[Config.MaximumPossibleArmy];
        rightActors = new Actor_Unit[Config.MaximumPossibleArmy];
        rightUnits = new Unit[Config.MaximumPossibleArmy];

        for (int x = 0; x < Config.MaximumPossibleArmy; x++)
        {
            leftUnitSprites[x].SetIndex(x);
            rightUnitSprites[x].SetIndex(x + Config.MaximumPossibleArmy);
        }
        UpdateActorList();
    }

    private void Update()
    {
        if (gameObject.activeSelf == false)
            return;
        if (infoPanel != null && LeftSelector.gameObject.activeSelf == false)
            Select(0);
        RefreshSelectors();
    }

    public void UpdateActorList()
    {
        Vec2i noLoc = new Vec2i(0, 0);
        for (int x = 0; x < Config.MaximumPossibleArmy; x++)
        {
            if (x >= LeftArmy.Units.Count)
            {
                leftUnits[x] = null;
                leftActors[x] = null;
            }
            else if (LeftArmy.Units[x] != leftUnits[x])
            {
                leftActors[x] = new Actor_Unit(noLoc, LeftArmy.Units[x]);
                leftUnits[x] = LeftArmy.Units[x];
                leftActors[x].UpdateBestWeapons();
                leftActors[x].Unit.Side = LeftArmy.Side;
            }
            if (x >= RightArmy.Units.Count)
            {
                rightUnits[x] = null;
                rightActors[x] = null;
            }
            else if (RightArmy.Units[x] != rightUnits[x])
            {
                rightActors[x] = new Actor_Unit(noLoc, RightArmy.Units[x]);
                rightUnits[x] = RightArmy.Units[x];
                rightActors[x].UpdateBestWeapons();
                rightActors[x].Unit.Side = RightArmy.Side;
            }
            //else it already exists and is correct, so we do nothing
        }
        UpdateDrawnActors();
    }

    void UpdateDrawnActors()
    {

        for (int x = 0; x < Config.MaximumPossibleArmy; x++)
        {
            if (leftActors[x] == null)
            {
                leftUnitSprites[x].gameObject.SetActive(false);
            }
            else
            {
                leftUnitSprites[x].gameObject.SetActive(true);
                leftUnitSprites[x].UpdateSprites(leftActors[x]);
                leftUnitSprites[x].Name.text = LeftArmy.Units[x].Name;
            }
            if (rightActors[x] == null)
            {
                rightUnitSprites[x].gameObject.SetActive(false);
            }
            else
            {
                rightUnitSprites[x].gameObject.SetActive(true);
                rightUnitSprites[x].UpdateSprites(rightActors[x]);
                rightUnitSprites[x].Name.text = RightArmy.Units[x].Name;
            }
        }
        LeftFolder.GetComponent<RectTransform>().sizeDelta = new Vector2(760, Mathf.Max((3 + LeftArmy.Units.Count) / 4 * 240, 1020));
        RightFolder.GetComponent<RectTransform>().sizeDelta = new Vector2(760, Mathf.Max((3 + RightArmy.Units.Count) / 4 * 240, 1020));
    }

    public void Select(int num)
    {
        if (num < Config.MaximumPossibleArmy)
            leftSelected = num;
        else
            rightSelected = num - Config.MaximumPossibleArmy;
        RefreshSelectors();

    }

    private void RefreshSelectors()
    {
        if (leftSelected < LeftArmy.Units.Count)
        {
            LeftSelector.SetActive(true);
            LeftSelector.transform.position = leftUnitSprites[leftSelected].transform.position;
        }
        else
            LeftSelector.SetActive(false);

        if (rightSelected < RightArmy.Units.Count)
        {
            RightSelector.SetActive(true);
            RightSelector.transform.position = rightUnitSprites[rightSelected].transform.position;
        }
        else
            RightSelector.SetActive(false);
    }

    public void UpdateInfo(int num)
    {
        info.InfoText.text = "";
        if (num < 0)
            return;
        Unit hoveredUnit;
        if (num < Config.MaximumPossibleArmy)
            hoveredUnit = leftUnits[num];
        else
            hoveredUnit = rightUnits[num - Config.MaximumPossibleArmy];
        if (hoveredUnit != null)
        {
            infoPanel.RefreshStrategicUnitInfo(hoveredUnit);
        }
    }

    public void TransferToLeft()
    {
        if (fullArmies)
        {
            Exchange();
            return;
        }

        if (rightSelected >= RightArmy.Units.Count)
            return;

        if(!StrategicUtilities.ArmyCanFitUnit(LeftArmy, RightArmy.Units[rightSelected]))
            return;

        if (RightArmy.Units[rightSelected] == RightArmy.Empire.Leader && LeftArmy.Side != RightArmy.Side)
        {
            State.GameManager.CreateMessageBox("Can't trade heroes between races");
            return;
        }
        LeftArmy.Units.Add(RightArmy.Units[rightSelected]);
        RightArmy.Units.RemoveAt((rightSelected));
        UpdateActorList();
        LeftReceived = true;
        if (rightSelected >= RightArmy.Units.Count)
        {
            rightSelected = Mathf.Max(rightSelected - 1, 0);
            RefreshSelectors();
        }
    }
    public void TransferToRight()
    {
        if (fullArmies)
        {
            Exchange();
            return;
        }


        if (leftSelected >= LeftArmy.Units.Count)
            return;

        if (!StrategicUtilities.ArmyCanFitUnit(RightArmy, LeftArmy.Units[rightSelected]))
            return;

        if (LeftArmy.Units[leftSelected] == LeftArmy.Empire.Leader && LeftArmy.Side != RightArmy.Side)
        {
            State.GameManager.CreateMessageBox("Can't trade heroes between races");
            return;
        }
        var village = StrategicUtilities.GetVillageAt(RightArmy.Position);
        if (village != null && RightArmy.Empire != null && village.Empire.IsEnemy(RightArmy.Empire) && LeftArmy.Units[leftSelected] == LeftArmy.Empire.Leader)
        {
            State.GameManager.CreateMessageBox("Leaders can't infiltrate");
            return;
        }
        RightArmy.Units.Add(LeftArmy.Units[leftSelected]);
        LeftArmy.Units.RemoveAt((leftSelected));
        UpdateActorList();
        RightReceived = true;
        if (leftSelected >= LeftArmy.Units.Count)
        {
            leftSelected = Mathf.Max(leftSelected - 1, 0);
            RefreshSelectors();
        }
    }

    public void TransferItemToLeft(ItemType type)
    {
        if (RightArmy.ItemStock.HasItem(type))
        {
            RightArmy.ItemStock.TakeItem(type);
            LeftArmy.ItemStock.AddItem(type);
        }
        RefreshItemCounts();
    }

    public void TransferItemToRight(ItemType type)
    {
        if (LeftArmy.ItemStock.HasItem(type))
        {
            LeftArmy.ItemStock.TakeItem(type);
            RightArmy.ItemStock.AddItem(type);
        }
        RefreshItemCounts();
    }

    void RefreshItemCounts()
    {
        for (int i = 0; i < ItemExchangers.Length; i++)
        {
            ItemExchangers[i].UpdateValues(LeftArmy.ItemStock.ItemCount(ItemExchangers[i].type), RightArmy.ItemStock.ItemCount(ItemExchangers[i].type));
            ItemExchangers[i].gameObject.SetActive(LeftArmy.ItemStock.HasItem(ItemExchangers[i].type) || RightArmy.ItemStock.HasItem(ItemExchangers[i].type));
        }
    }

    public void OpenItemExchanger()
    {
        if (ItemExchangers == null)
        {
            ItemExchangers = new ItemExchangeControl[(int)ItemType.Resurrection + 1];
            for (int i = 0; i < ItemExchangers.Length; i++)
            {
                ItemExchangers[i] = Instantiate(ItemExchangeSet, ItemExchangeFolder).GetComponent<ItemExchangeControl>();
                ItemExchangers[i].type = (ItemType)i;
            }
        }

        RefreshItemCounts();
        ItemExchangePanel.SetActive(true);
    }

    public void Exchange()
    {
        if (rightSelected >= RightArmy.Units.Count || leftSelected >= LeftArmy.Units.Count)
            return;

        if (LeftArmy.Units[rightSelected] == LeftArmy.Empire.Leader && LeftArmy.Side != RightArmy.Side)
        {
            State.GameManager.CreateMessageBox("Can't trade heroes between races");
            return;
        }

        if (RightArmy.Units[rightSelected] == RightArmy.Empire.Leader && LeftArmy.Side != RightArmy.Side)
        {
            State.GameManager.CreateMessageBox("Can't trade heroes between races");
            return;
        }
        Unit LeftTemp = LeftArmy.Units[leftSelected];
        Unit RightTemp = RightArmy.Units[rightSelected];
        LeftArmy.Units.Remove(LeftTemp);
        RightArmy.Units.Remove(RightTemp);
        LeftArmy.Units.Add(RightTemp);
        RightArmy.Units.Add(LeftTemp);
        UpdateActorList();
        LeftReceived = true;
        RightReceived = true;
    }

    public void Close()
    {
        if (RightArmy.Units.Count == 0)
            RightArmy.ItemStock.TransferAllItems(LeftArmy.ItemStock);
        else if (LeftArmy.Units.Count == 0)
            LeftArmy.ItemStock.TransferAllItems(RightArmy.ItemStock);
    }
}
