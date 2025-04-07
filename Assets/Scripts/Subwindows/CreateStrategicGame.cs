using Assets.Scripts.Utility.Stored;
using MapObjects;
using OdinSerializer;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public enum TacticalAIType
{
    None,
    Legacy,
    Full,
}

public enum StrategyAIType
{
    None,
    Passive,
    Legacy,
    Basic,
    Advanced,
    Cheating1,
    Cheating2,
    Cheating3,
    Monster = 100,
    Goblin,

}

public struct StrategicCreationArgs
{
    internal Empire.ConstructionArgs[] empireArgs;
    internal bool[] CanVore;
    internal int[] TurnOrder;
    internal bool crazyBuildings;
    internal int[] Team;
    internal int MercCamps;
    internal int GoldMines;
    internal int AncientTeleporters;

    internal WorldGenerator.MapGenArgs MapGen;

    public StrategicCreationArgs(int length)
    {
        empireArgs = new Empire.ConstructionArgs[length];
        CanVore = new bool[length];
        TurnOrder = new int[length];
        Team = new int[length];
        crazyBuildings = false;
        MercCamps = 0;
        GoldMines = 0;
        AncientTeleporters = 0;

        MapGen = new WorldGenerator.MapGenArgs();

    }
}

public class CreateStrategicGame : MonoBehaviour
{
    public StartEmpireUI AllEmpires;

    public StartEmpireUI[] Empires;

    public Dropdown StrategicAutoSize;
    public InputField StrategicX;
    public InputField StrategicY;
    public InputField TacticalX;
    public InputField TacticalY;
    public Toggle AutoScaleTactical;
    public Toggle CrazyBuildings;
    public InputField BaseExpRequired;
    public InputField ExpIncreaseRate;
    public InputField VillageIncomeRate;
    public InputField VillagersPerFarm;
    public InputField SoftLevelCap;
    public InputField HardLevelCap;
    public Toggle FactionLeaders;
    public Toggle LeadersAutoGainLeadership;
    public Dropdown VictoryCondition;
    public InputField StartingVillagePopulation;

    public InputField LeaderLossLevels;
    public Slider LeaderLossExpPct;

    public InputField GoldMines;
    public InputField MercenaryHouses;
    public InputField AncientTeleporters;

    public Toggle SpawnTeamsTogether;
    public Toggle FirstTurnArmiesIdle;
    public Toggle CapitalGarrisonCapped;

    public Button ClearPickedMap;

    public Button AddRaces;

    public InputField StartingGold;

    public InputField GoldMineIncome;

    public GameObject EmpiresTab;
    public GameObject GameSettingsTab;
    public GameObject MapGenTab;

    public Dropdown MapGenType;
    public Toggle MapGenPoles;
    public Toggle MapGenExcessBridges;
    public Slider MapGenWaterPct;
    public Slider MapGenTemperature;
    public Slider MapGenForests;
    public Slider MapGenSwamps;
    public Slider MapGenHills;

    public InputField ArmyUpkeep;

    public InputField AbandonedVillages;


    public RacePanel RaceUI;

    public Text TooltipText;

    internal Map map;

    string mapString;

    public void ClearState()
    {
        State.World = null;
        foreach (var emp in Empires)
        {
            emp.PrimaryColor.GetComponent<Image>().color = ColorFromIndex(emp.PrimaryColor.value);
            emp.SecondaryColor.GetComponent<Image>().color = GetDarkerColor(ColorFromIndex(emp.SecondaryColor.value));
        }
    }

    public void SaveSettings(int slot)
    {
        CreateStrategicStored stored = new CreateStrategicStored();
        FieldInfo[] fields = GetType().GetFields();
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(InputField))
            {
                stored.InputFields[field.Name] = ((InputField)field.GetValue(this)).text;
            }
            if (field.FieldType == typeof(Dropdown))
            {
                stored.Dropdowns[field.Name] = ((Dropdown)field.GetValue(this)).value;
            }
            if (field.FieldType == typeof(Toggle))
            {
                stored.Toggles[field.Name] = ((Toggle)field.GetValue(this)).isOn;
            }
            if (field.FieldType == typeof(Slider))
            {
                stored.Sliders[field.Name] = ((Slider)field.GetValue(this)).value;
            }
        }
        for (int i = 0; i < Empires.Length; i++)
        {
            var data = new EmpireData
            {
                AIPlayer = Empires[i].AIPlayer.isOn,
                CanVore = Empires[i].CanVore.isOn,
                MaxArmySize = Empires[i].MaxArmySize.value,
                MaxGarrisonSize = Empires[i].MaxGarrisonSize.value,
                PrimaryColor = Empires[i].PrimaryColor.value,
                SecondaryColor = Empires[i].SecondaryColor.value,
                Team = Empires[i].Team.text,
                TurnOrder = Empires[i].TurnOrder.text,
                VillageCount = Empires[i].VillageCount.text,
                StrategicAI = Empires[i].StrategicAI.value,
                TacticalAI = Empires[i].TacticalAI.value,

            };

            stored.Empires[i] = data;
        }
        byte[] bytes = SerializationUtility.SerializeValue(stored, DataFormat.JSON);
        File.WriteAllBytes($"{State.StorageDirectory}createsettings{slot}.txt", bytes);
    }

    public void LoadSettings(int slot)
    {
        if (File.Exists($"{State.StorageDirectory}createsettings{slot}.txt") == false)
            return;
        byte[] bytes = File.ReadAllBytes($"{State.StorageDirectory}createsettings{slot}.txt");
        CreateStrategicStored stored = SerializationUtility.DeserializeValue<CreateStrategicStored>(bytes, DataFormat.JSON);
        FieldInfo[] fields = GetType().GetFields();
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(InputField))
            {
                if (stored.InputFields.TryGetValue(field.Name, out var value))
                    ((InputField)field.GetValue(this)).text = value;
            }
            if (field.FieldType == typeof(Dropdown))
            {
                if (stored.Dropdowns.TryGetValue(field.Name, out var value))
                    ((Dropdown)field.GetValue(this)).value = value;
            }
            if (field.FieldType == typeof(Toggle))
            {
                if (stored.Toggles.TryGetValue(field.Name, out var value))
                    ((Toggle)field.GetValue(this)).isOn = value;
            }
            if (field.FieldType == typeof(Slider))
            {
                if (stored.Sliders.TryGetValue(field.Name, out var value))
                    ((Slider)field.GetValue(this)).value = value;
            }
        }

        for (int i = 0; i < Empires.Length; i++)
        {
            if (stored.Empires.TryGetValue(i, out var value))
            {
                Empires[i].AIPlayer.isOn = value.AIPlayer;
                Empires[i].CanVore.isOn = value.CanVore;
                Empires[i].MaxArmySize.value = value.MaxArmySize;
                Empires[i].MaxGarrisonSize.value = value.MaxGarrisonSize;
                Empires[i].PrimaryColor.value = value.PrimaryColor;
                Empires[i].SecondaryColor.value = value.SecondaryColor;
                Empires[i].Team.text = value.Team;
                Empires[i].TurnOrder.text = value.TurnOrder;
                Empires[i].VillageCount.text = value.VillageCount;
                Empires[i].StrategicAI.value = value.StrategicAI;
                Empires[i].TacticalAI.value = value.TacticalAI;
                if (int.TryParse(value.VillageCount, out int result))
                {
                    Empires[i].gameObject.SetActive(result > 0);
                }
                else
                    Empires[i].gameObject.SetActive(false);
            }
        }

        if (map != null)
            PickMap(mapString);
    }

    public static Color GetDarkerColor(Color color)
    {
        color.r *= .6f;
        color.g *= .6f;
        color.b *= .6f;
        return color;
    }

    public void MapGenTypeChanged()
    {
        MapGenPoles.interactable = MapGenType.value == 1;
        MapGenTemperature.interactable = MapGenType.value == 1;
        MapGenWaterPct.interactable = MapGenType.value == 1;
    }

    public void VillageCountUpdated()
    {
        for (int i = 0; i < Empires.Length; i++)
        {
            if (Empires[i].gameObject.activeSelf == false)
                continue;
            Empires[i].VillageCount.text = AllEmpires.VillageCount.text;
        }
    }

    public void StrategicAIUpdated()
    {
        for (int i = 0; i < Empires.Length; i++)
        {
            if (Empires[i].gameObject.activeSelf == false)
                continue;
            Empires[i].StrategicAI.value = AllEmpires.StrategicAI.value;
        }
    }

    public void TacticalAIUpdated()
    {
        for (int i = 0; i < Empires.Length; i++)
        {
            if (Empires[i].gameObject.activeSelf == false)
                continue;
            Empires[i].TacticalAI.value = AllEmpires.TacticalAI.value;
        }
    }

    public void UpdateAIBoxes()
    {
        for (int i = 0; i < Empires.Length; i++)
        {
            Empires[i].StrategicAI.interactable = Empires[i].AIPlayer.isOn;
            Empires[i].TacticalAI.interactable = Empires[i].AIPlayer.isOn;
        }
    }
    public void MaxGarrisonSizeUpdated()
    {
        for (int i = 0; i < Empires.Length; i++)
        {
            if (Empires[i].gameObject.activeSelf == false)
                continue;
            Empires[i].MaxGarrisonSize.value = AllEmpires.MaxGarrisonSize.value;
        }
    }

    public void MaxArmySizeUpdated()
    {
        for (int i = 0; i < Empires.Length; i++)
        {
            if (Empires[i].gameObject.activeSelf == false)
                continue;
            Empires[i].MaxArmySize.value = AllEmpires.MaxArmySize.value;
        }
    }

    public void StrategicAutoSizeChanged()
    {
        StrategicX.transform.parent.gameObject.SetActive(StrategicAutoSize.value == 0);
        StrategicY.transform.parent.gameObject.SetActive(StrategicAutoSize.value == 0);
    }

    public void SwitchToEmpires()
    {
        EmpiresTab.SetActive(true);
        GameSettingsTab.SetActive(false);
        MapGenTab.SetActive(false);
    }

    public void SwitchToGameSettings()
    {
        EmpiresTab.SetActive(false);
        GameSettingsTab.SetActive(true);
        MapGenTab.SetActive(false);
    }

    public void SwitchToMapGenSettings()
    {
        EmpiresTab.SetActive(false);
        GameSettingsTab.SetActive(false);
        MapGenTab.SetActive(true);
    }

    public void PickSaveForContentSettingsDialog()
    {
        var ui = Instantiate(State.GameManager.LoadPicker).GetComponent<FileLoaderUI>();
        new SimpleFileLoader(State.SaveDirectory, "sav", ui, false, SimpleFileLoader.LoaderType.PickSaveForContentSettings);
    }

    public void PickMapDialog()
    {
        var ui = Instantiate(State.GameManager.LoadPicker).GetComponent<FileLoaderUI>();
        new SimpleFileLoader(State.MapDirectory, "map", ui, false, SimpleFileLoader.LoaderType.PickMap);
    }

    internal void PickSaveForContentData(string filename)
    {
        try
        {
            byte[] bytes = File.ReadAllBytes(filename);
            var tempWorld = SerializationUtility.DeserializeValue<World>(bytes, DataFormat.Binary);
            Config.World = tempWorld.ConfigStorage;
        }
        catch
        {
            State.GameManager.CreateMessageBox("Couldn't read the content settings correctly");
        }
    }

    internal void PickMap(string filename)
    {
        mapString = filename;
        for (int i = 0; i < Empires.Length; i++)
        {
            Empires[i].gameObject.SetActive(false);
        }
        map = Map.Get(filename);
        for (int i = 0; i < Empires.Length; i++)
        {
            Empires[i].VillageCount.interactable = false;
            int count = map.storedVillages.Where(s => s.Race == (Race)i).Count();
            Empires[i].VillageCount.text = count.ToString();
            if (count > 0)
            {
                Empires[i].gameObject.SetActive(true);
                //Empires[i].TurnOrder.text = "1";
            }

        }
        AllEmpires.VillageCount.interactable = false;
        StrategicX.interactable = false;
        StrategicX.text = map.Tiles.GetLength(0).ToString();
        StrategicY.interactable = false;
        StrategicY.text = map.Tiles.GetLength(1).ToString();
        ClearPickedMap.interactable = true;
        AddRaces.interactable = false;
    }

    public void ClearMap()
    {
        map = null;
        for (int i = 0; i < Empires.Length; i++)
        {
            Empires[i].VillageCount.interactable = true;
        }
        AllEmpires.VillageCount.interactable = true;
        StrategicX.interactable = true;
        StrategicY.interactable = true;
        ClearPickedMap.interactable = false;
        AddRaces.interactable = true;
    }

    internal void UpdateColor(StartEmpireUI emp)
    {
        emp.PrimaryColor.GetComponent<Image>().color = ColorFromIndex(emp.PrimaryColor.value);
    }

    internal void UpdateSecondaryColor(StartEmpireUI emp)
    {
        emp.SecondaryColor.GetComponent<Image>().color = GetDarkerColor(ColorFromIndex(emp.SecondaryColor.value));
    }

    string TestTacticalSize()
    {
        int x = Convert.ToInt32(TacticalX.text);
        int y = Convert.ToInt32(TacticalY.text);

        int MaxUnitsOnHalf = 50;

        if (x < 8 || y < 8)
            return "Can't have a tactical dimension less than 8";

        if (MaxUnitsOnHalf > x * y / 14)
            return "Not enough space to comfortably fit a full army";

        return "";
    }

    string TestStrategicSize()
    {
        int x = Convert.ToInt32(StrategicX.text);
        int y = Convert.ToInt32(StrategicY.text);

        int villageCount = 0;
        int empireCount = 0;
        for (int i = 0; i < Empires.Length; i++)
        {
            if (Convert.ToInt32(Empires[i].VillageCount.text) < 0)
                return "Can't have negative villages";
            villageCount += Convert.ToInt32(Empires[i].VillageCount.text);
            if (Convert.ToInt32(Empires[i].VillageCount.text) > 0)
                empireCount++;
        }

        if (empireCount < 1 || (empireCount < 2 && VictoryCondition.value != 3))
            return "Need at least 2 empires with villages (or 1 with no victory condition)";

        if (x < 7 || y < 7)
            return "Can't have a tactical dimension less than 7";

        int useableSpace = (x - 2) * (y - 2);

        if (useableSpace < 32 * villageCount)
            return "Not enough space to comfortably fit all the villages";

        return "";

    }

    string SetStrategicSizeAutomatically()
    {

        int villageCount = 0;
        int empireCount = 0;
        for (int i = 0; i < Empires.Length; i++)
        {
            if (Convert.ToInt32(Empires[i].VillageCount.text) < 0)
                return "Can't have negative villages";
            villageCount += Convert.ToInt32(Empires[i].VillageCount.text);
            if (Convert.ToInt32(Empires[i].VillageCount.text) > 0)
                empireCount++;
        }

        villageCount += Convert.ToInt32(AbandonedVillages.text);

        if (empireCount < 1 || (empireCount < 2 && VictoryCondition.value != 3))
            return "Need at least 2 empires with villages (or 1 with no victory condition)";

        int minimumSpace = 64;
        if (StrategicAutoSize.value == 1)
            minimumSpace = 32 * villageCount;
        else if (StrategicAutoSize.value == 2)
            minimumSpace = 64 * villageCount;
        else if (StrategicAutoSize.value == 3)
            minimumSpace = 128 * villageCount;
        StrategicX.text = (1 + (int)Math.Sqrt(minimumSpace)).ToString();
        StrategicY.text = (1 + (int)Math.Sqrt(minimumSpace)).ToString();

        return "";
    }

    void AssignUnusedTurnOrders()
    {
        int lastIndex = Empires.Length - 1;

        for (int i = Empires.Length - 1; i >= 0; i--)
        {
            if (int.TryParse(Empires[i].VillageCount.text, out int result))
            {
                if (result <= 0)
                {
                    lastIndex--;
                }
            }

        }


    }


    public void CreateWorld()
    {
        State.GameManager.Menu.Options.LoadFromStored();
        State.GameManager.Menu.CheatMenu.LoadFromStored();
        Config.World.VillagesPerEmpire = new int[Empires.Length];
        try
        {
            for (int i = 0; i < Empires.Length; i++)
            {
                Config.VillagesPerEmpire[i] = Convert.ToInt32(Empires[i].VillageCount.text);
            }
            Config.World.SoftLevelCap = Convert.ToInt32(SoftLevelCap.text);
            Config.World.HardLevelCap = Convert.ToInt32(HardLevelCap.text);
        }
        catch
        {
            State.GameManager.CreateMessageBox("There's a blank textbox, if you want it to be 0 it should say 0");
            return;
        }
        try
        {
            string errorText = "";
            if (AutoScaleTactical.isOn == false)
                errorText = TestTacticalSize();
            if (errorText != "")
            {
                State.GameManager.CreateMessageBox(errorText);
                return;
            }
            if (map == null && StrategicAutoSize.value == 0)
            {
                errorText = TestStrategicSize();
                if (errorText != "")
                {
                    State.GameManager.CreateMessageBox(errorText);
                    return;
                }
            }
            else if (map == null && StrategicAutoSize.value > 0)
            {
                errorText = SetStrategicSizeAutomatically();
                if (errorText != "")
                {
                    State.GameManager.CreateMessageBox(errorText);
                    return;
                }
            }
            Config.World.ExperiencePerLevel = Convert.ToInt32(BaseExpRequired.text);
            Config.World.AdditionalExperiencePerLevel = Convert.ToInt32(ExpIncreaseRate.text);
            Config.StartingGold = Convert.ToInt32(StartingGold.text);
            if (Config.ExperiencePerLevel < 1)
            {
                State.GameManager.CreateMessageBox("Levels should require at least 1 exp");
                return;
            }
            if (Config.AdditionalExperiencePerLevel < 0)
            {
                State.GameManager.CreateMessageBox("Additonal Levels should be at least 0 (new levels shouldn't be cheaper)");
                return;
            }
        }
        catch
        {
            State.GameManager.CreateMessageBox("At least one of the textboxes is blank, and needs to be filled in");
            return;
        }

        Config.World.ItemSlots = Config.NewItemSlots;

        StrategicCreationArgs args = new StrategicCreationArgs(Empires.Length);
        Config.CenteredEmpire = new bool[Config.NumberOfRaces];
        try
        {
            for (int i = 0; i < Empires.Length; i++)
            {
                if (Empires[i].AIPlayer.isOn)
                {
                    args.empireArgs[i].strategicAI = (StrategyAIType)Empires[i].StrategicAI.value + 1;
                    args.empireArgs[i].tacticalAI = (TacticalAIType)Empires[i].TacticalAI.value + 1;
                    Config.CenteredEmpire[i] = ((StrategyAIType)Empires[i].StrategicAI.value + 1) == StrategyAIType.Passive;
                }
                else
                {
                    args.empireArgs[i].strategicAI = 0;
                    args.empireArgs[i].tacticalAI = 0;
                    Config.CenteredEmpire[i] = false;
                }
                args.CanVore[i] = Empires[i].CanVore.isOn;
                args.empireArgs[i].team = Convert.ToInt32(Empires[i].Team.text);
                args.Team[i] = args.empireArgs[i].team;
                args.empireArgs[i].color = ColorFromIndex(Empires[i].PrimaryColor.value);
                args.empireArgs[i].secColor = GetDarkerColor(ColorFromIndex(Empires[i].SecondaryColor.value));
                args.TurnOrder[i] = Convert.ToInt32(Empires[i].TurnOrder.text);
                args.empireArgs[i].maxArmySize = (int)Empires[i].MaxArmySize.value;
                args.empireArgs[i].maxGarrisonSize = (int)Empires[i].MaxGarrisonSize.value;
                args.empireArgs[i].side = i;
                //args.empireArgs[i].bannerType = (i % 2 == 1) ? 1 : 3;

            }
            args.MercCamps = Convert.ToInt32(MercenaryHouses.text);
            args.AncientTeleporters = Convert.ToInt32(AncientTeleporters.text);
            args.GoldMines = Convert.ToInt32(GoldMines.text);
            args.crazyBuildings = CrazyBuildings.isOn;

            args.MapGen.UsingNewGenerator = MapGenType.value == 1;
            args.MapGen.ExcessBridges = MapGenExcessBridges.isOn;
            args.MapGen.Poles = MapGenPoles.isOn;
            args.MapGen.Temperature = MapGenTemperature.value;
            args.MapGen.WaterPct = MapGenWaterPct.value;
            args.MapGen.Hilliness = MapGenHills.value;
            args.MapGen.Swampiness = MapGenSwamps.value;
            args.MapGen.ForestPct = MapGenForests.value;
            args.MapGen.AbandonedVillages = Convert.ToInt32(AbandonedVillages.text);

            Config.World.StartingPopulation = Convert.ToInt32(StartingVillagePopulation.text);
            Config.World.LeaderLossExpPct = LeaderLossExpPct.value;
            Config.World.LeaderLossLevels = Convert.ToInt32(LeaderLossLevels.text);
            Config.World.GoldMineIncome = Convert.ToInt32(GoldMineIncome.text);
            Config.World.StrategicWorldSizeX = Convert.ToInt32(StrategicX.text);
            Config.World.StrategicWorldSizeY = Convert.ToInt32(StrategicY.text);
            Config.World.TacticalSizeX = Convert.ToInt32(TacticalX.text);
            Config.World.TacticalSizeY = Convert.ToInt32(TacticalY.text);
            Config.World.FactionLeaders = FactionLeaders.isOn;
            Config.World.VictoryCondition = (Config.VictoryType)VictoryCondition.value;
            Config.World.VillageIncomePercent = Convert.ToInt32(VillageIncomeRate.text);
            Config.World.VillagersPerFarm = Convert.ToInt32(VillagersPerFarm.text);
            Config.World.ArmyUpkeep = Convert.ToInt32(ArmyUpkeep.text);
            Config.World.CapMaxGarrisonIncrease = CapitalGarrisonCapped.isOn;
            Config.World.Toggles["FirstTurnArmiesIdle"] = FirstTurnArmiesIdle.isOn;
            Config.World.Toggles["LeadersAutoGainLeadership"] = LeadersAutoGainLeadership.isOn;


            Config.PutTeamsTogether = SpawnTeamsTogether.isOn;


        }
        catch
        {
            State.GameManager.CreateMessageBox("At least one of the textboxes is blank, and needs to be filled in \n *Could possibly be a maximum race issue.*  \n*Check Config.cs NumberOfRaces and make sure it's value is correct.*");
            return;
        }

        State.World = new World(args, map);
        Config.World.AutoScaleTactical = AutoScaleTactical.isOn;

        State.GameManager.SwitchToStrategyMode();
        gameObject.SetActive(false);
    }

    public void AutoScaleChanged()
    {
        TacticalX.interactable = !AutoScaleTactical.isOn;
        TacticalY.interactable = !AutoScaleTactical.isOn;
    }

    public void ClearRaces()
    {
        for (int i = 0; i < Empires.Length; i++)
        {
            RemoveRace(Empires[i]);
        }
        BuildRaceDisplay();
    }

    internal void RemoveRace(StartEmpireUI empire)
    {
        empire.VillageCount.text = 0.ToString();
        empire.gameObject.SetActive(false);
    }

    public void AllAddRaces()
    {
        for (int i = 0; i < Empires.Length; i++)
        {
            if (Empires[i].gameObject.activeSelf == false)
            {
                Empires[i].gameObject.SetActive(true);
                Empires[i].VillageCount.text = AllEmpires.VillageCount.text;
                Empires[i].StrategicAI.value = AllEmpires.StrategicAI.value;
                Empires[i].TacticalAI.value = AllEmpires.TacticalAI.value;
                Empires[i].MaxArmySize.value = AllEmpires.MaxArmySize.value;
                Empires[i].MaxGarrisonSize.value = AllEmpires.MaxGarrisonSize.value;
                Empires[i].TurnOrder.text = "1";
            }

        }
        RaceUI.gameObject.SetActive(false);
    }


    public void BuildRaceDisplay()
    {
        int children = RaceUI.RaceFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(RaceUI.RaceFolder.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < Config.NumberOfRaces; i++)
        {

            if (Empires[i].gameObject.activeSelf)
                continue;
            GameObject obj = Instantiate(RaceUI.RaceUnitPanel, RaceUI.RaceFolder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            Actor_Unit actor = new Actor_Unit(new Vec2i(0, 0), new Unit(1, (Race)i, 0, true));
            TextMeshProUGUI text = obj.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI dcosttext = obj.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI upkeeptext = obj.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
            obj.GetComponentInChildren<UnitInfoPanel>().Unit = actor.Unit;
            var racePar = RaceParameters.GetTraitData(actor.Unit);
            text.text = $"{(Race)i}\nBody Size: {State.RaceSettings.GetBodySize(actor.Unit.Race)}\nBase Stomach Size: {State.RaceSettings.GetStomachSize(actor.Unit.Race)}\nFavored Stat: {State.RaceSettings.GetFavoredStat(actor.Unit.Race)}\nDefault Traits:\n{State.RaceSettings.ListTraits(actor.Unit.Race)}";
            sprite.UpdateSprites(actor);
            dcosttext.text = (State.RaceSettings.GetDeployCost(actor.Unit.Race) * actor.Unit.TraitBoosts.DeployCostMult).ToString();
            upkeeptext.text = (State.RaceSettings.GetUpkeep(actor.Unit.Race) * actor.Unit.TraitBoosts.UpkeepMult).ToString();
            Button button = obj.GetComponentInChildren<Button>();
            int temp = i;
            button.onClick.AddListener(() => AddRace(temp));
            button.onClick.AddListener(() => Destroy(obj));
        }
        RaceUI.gameObject.SetActive(true);
    }

    void AddRace(int race)
    {
        Empires[race].gameObject.SetActive(true);
        Empires[race].VillageCount.text = AllEmpires.VillageCount.text;
        Empires[race].StrategicAI.value = AllEmpires.StrategicAI.value;
        Empires[race].TacticalAI.value = AllEmpires.TacticalAI.value;
        Empires[race].MaxArmySize.value = AllEmpires.MaxArmySize.value;
        Empires[race].MaxGarrisonSize.value = AllEmpires.MaxGarrisonSize.value;
        Empires[race].TurnOrder.text = "1";
        AssignUnusedTurnOrders();

    }


    internal static Color ColorFromIndex(int index)
    {
        if (index == 0) return Color.blue;
        else if (index == 1) return Color.red;
        else if (index == 2) return new Color(.9f, .6f, 0f);
        else if (index == 3) return Color.yellow;
        else if (index == 4) return Color.magenta;
        else if (index == 5) return new Color(0, .45f, 0f);
        else if (index == 6) return Color.cyan;
        else if (index == 7) return new Color(.9f, .4f, .4f);
        else if (index == 8) return new Color(.7f, .4f, .9f);
        else if (index == 9) return Color.green;
        else if (index == 10) return Color.white;
        else if (index == 11) return new Color(.2f, .2f, .2f);
        else if (index == 12) return new Color(.52f, .66f, 1);
        else if (index == 13) return new Color(.96f, .94f, .7f);
        else if (index == 14) return new Color(.45f, .17f, .11f);
        else if (index == 15) return new Color(.61f, .65f, 0);
        else if (index == 16) return new Color(.7f, .7f, .7f);
        return Color.black;

    }

    internal static int IndexFromColor(Color color)
    {
        if (color == Color.blue) return 0;
        else if (color == Color.red) return 1;
        else if (color == new Color(.9f, .6f, 0f)) return 2;
        else if (color == Color.yellow) return 3;
        else if (color == Color.magenta) return 4;
        else if (color == new Color(0, .45f, 0f)) return 5;
        else if (color == Color.cyan) return 6;
        else if (color == new Color(.9f, .4f, .4f)) return 7;
        else if (color == new Color(.7f, .4f, .9f)) return 8;
        else if (color == Color.green) return 9;
        else if (color == Color.white) return 10;
        else if (color == new Color(.2f, .2f, .2f)) return 11;
        else if (color == new Color(.52f, .66f, 1)) return 12;
        else if (color == new Color(.96f, .94f, .7f)) return 13;
        else if (color == new Color(.45f, .17f, .11f)) return 14;
        else if (color == new Color(.61f, .65f, 0)) return 15;
        else if (color == new Color(.7f, .7f, .7f)) return 16;
        return 0;

    }

    public void ChangeToolTip(int type)
    {
        TooltipText.text = DefaultTooltips.Tooltip(type);
    }

    public void UpdateLeaderExpLoss()
    {
        LeaderLossExpPct.GetComponentInChildren<Text>().text = $"Leader Exp lost on Death: {Math.Round(LeaderLossExpPct.value * 100, 2)}%";
    }


}

