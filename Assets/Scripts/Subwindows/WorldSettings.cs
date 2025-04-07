using LegacyAI;
using System;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class WorldSettings : MonoBehaviour
{
    public GameObject EditEmpirePrefab;
    public Transform folder;
    EditEmpireUI[] Empires;

    public Text RightText;

    public void Open()
    {
        ShowSettings();
        if (Empires != null)
        {
            foreach (var item in Empires)
            {
                Destroy(item.gameObject);
            }
        }
        Empires = new EditEmpireUI[State.World.MainEmpires.Count];
        for (int i = 0; i < Empires.Length; i++)
        {
            Empires[i] = Instantiate(EditEmpirePrefab, folder).GetComponent<EditEmpireUI>();
            Empires[i].Name.text = State.World.MainEmpires[i].Name.ToString();
            Empires[i].AIPlayer.isOn = State.World.MainEmpires[i].StrategicAI != null;
            if (State.World.MainEmpires[i].StrategicAI is PassiveAI)
                Empires[i].StrategicAI.value = 0;
            else if (State.World.MainEmpires[i].StrategicAI is LegacyStrategicAI)
                Empires[i].StrategicAI.value = 1;
            else if (State.World.MainEmpires[i].StrategicAI is StrategicAI)
            {
                StrategicAI ai = (StrategicAI)State.World.MainEmpires[i].StrategicAI;
                if (ai.CheatLevel > 0)
                    Empires[i].StrategicAI.value = 3 + ai.CheatLevel;
                else if (ai.smarterAI)
                    Empires[i].StrategicAI.value = 3;
                else
                    Empires[i].StrategicAI.value = 2;
            }
            if (Empires[i].AIPlayer.isOn)
                Empires[i].TacticalAI.value = ((int)State.World.MainEmpires[i].TacticalAIType) - 1;
            else
                Empires[i].TacticalAI.value = 1;
            Empires[i].CanVore.isOn = State.World.MainEmpires[i].CanVore;
            Empires[i].Team.text = State.World.MainEmpires[i].Team.ToString();
            Empires[i].PrimaryColor.value = CreateStrategicGame.IndexFromColor(State.World.MainEmpires[i].UnityColor);
            Color secColor = State.World.MainEmpires[i].UnitySecondaryColor;
            Empires[i].SecondaryColor.value = CreateStrategicGame.IndexFromColor(GetLighterColor(State.World.MainEmpires[i].UnitySecondaryColor));
            Empires[i].PrimaryColor.onValueChanged.AddListener((s) => UpdateColors());
            Empires[i].SecondaryColor.onValueChanged.AddListener((s) => UpdateColors());
            Empires[i].MaxArmySize.value = State.World.MainEmpires[i].MaxArmySize;
            Empires[i].MaxGarrisonSize.value = State.World.MainEmpires[i].MaxGarrisonSize;
            Empires[i].MaxArmySize.GetComponentInChildren<SetMeToValue>().Set(Empires[i].MaxArmySize);
            Empires[i].MaxGarrisonSize.GetComponentInChildren<SetMeToValue>().Set(Empires[i].MaxGarrisonSize);
            Empires[i].TurnOrder.text = State.World.MainEmpires[i].TurnOrder.ToString();
            if (State.World.MainEmpires[i].KnockedOut || State.World.MainEmpires[i].Side > 600)
                Empires[i].gameObject.SetActive(false);
        }
        UpdateColors();
        RelationsManager.ResetMonsterRelations();
    }

    public static Color GetLighterColor(Color color)
    {
        color.r /= .6f;
        color.g /= .6f;
        color.b /= .6f;
        return color;
    }

    public void EditWorldSettings()
    {
        State.GameManager.VariableEditor.Open(Config.World);
    }

    public void ExitAndSave()
    {
        for (int i = 0; i < Empires.Length; i++)
        {
            if (State.World.MainEmpires[i].Side > 500)
                continue;
            if (Empires[i].AIPlayer.isOn)
            {
                StrategyAIType strat = (StrategyAIType)(Empires[i].StrategicAI.value + 1);
                if (strat == StrategyAIType.Passive)
                    State.World.MainEmpires[i].StrategicAI = new PassiveAI(State.World.MainEmpires[i].Side);
                else if (strat == StrategyAIType.Basic)
                    State.World.MainEmpires[i].StrategicAI = new StrategicAI(State.World.MainEmpires[i], 0, false);
                else if (strat == StrategyAIType.Advanced)
                    State.World.MainEmpires[i].StrategicAI = new StrategicAI(State.World.MainEmpires[i], 0, true);
                else if (strat == StrategyAIType.Cheating1)
                    State.World.MainEmpires[i].StrategicAI = new StrategicAI(State.World.MainEmpires[i], 1, true);
                else if (strat == StrategyAIType.Cheating2)
                    State.World.MainEmpires[i].StrategicAI = new StrategicAI(State.World.MainEmpires[i], 2, true);
                else if (strat == StrategyAIType.Cheating3)
                    State.World.MainEmpires[i].StrategicAI = new StrategicAI(State.World.MainEmpires[i], 3, true);
                else if (strat == StrategyAIType.Legacy)
                    State.World.MainEmpires[i].StrategicAI = new LegacyStrategicAI(State.World.MainEmpires[i].Side);
                State.World.MainEmpires[i].TacticalAIType = (TacticalAIType)Empires[i].TacticalAI.value + 1;
            }
            else
            {
                State.World.MainEmpires[i].StrategicAI = null;
                State.World.MainEmpires[i].TacticalAIType = TacticalAIType.None;
            }
            if (State.World.MainEmpires[i].CanVore != Empires[i].CanVore.isOn)
            {
                State.World.MainEmpires[i].CanVore = Empires[i].CanVore.isOn;
                foreach (Unit unit in StrategicUtilities.GetAllUnits().Where(s => s.Race == State.World.MainEmpires[i].Race))
                {
                    if (unit.Type == UnitType.Soldier)
                    {
                        if (unit.fixedPredator == false)
                        {
                            unit.Predator = State.World.MainEmpires[i].CanVore;
                            unit.ReloadTraits(); //To make sure it takes into account gender traits as well
                        }
                    }

                }
            }

            if (State.World.MainEmpires[i].Team != Convert.ToInt32(Empires[i].Team.text))
            {
                State.World.MainEmpires[i].Team = Convert.ToInt32(Empires[i].Team.text);
                RelationsManager.TeamUpdated(State.World.MainEmpires[i]);
            }

            State.World.MainEmpires[i].UnityColor = CreateStrategicGame.ColorFromIndex(Empires[i].PrimaryColor.value);
            State.World.MainEmpires[i].UnitySecondaryColor = CreateStrategicGame.GetDarkerColor(CreateStrategicGame.ColorFromIndex(Empires[i].SecondaryColor.value));
            State.World.MainEmpires[i].MaxArmySize = (int)Empires[i].MaxArmySize.value;
            State.World.MainEmpires[i].OrigMaxArmySize = (int)Empires[i].MaxArmySize.value;
            State.World.MainEmpires[i].MaxGarrisonSize = (int)Empires[i].MaxGarrisonSize.value;
            State.World.MainEmpires[i].OrigMaxGarrisonSize = (int)Empires[i].MaxGarrisonSize.value;
            State.World.MainEmpires[i].TurnOrder = Convert.ToInt32(Empires[i].TurnOrder.text);
        }
        if (Config.Diplomacy == false)
            RelationsManager.ResetRelationTypes();
        State.World.RefreshTurnOrder();
        State.World.UpdateBanditLimits();
        State.GameManager.StrategyMode?.CheckIfOnlyAIPlayers();
        gameObject.SetActive(false);
    }

    public void ExitWithoutSaving()
    {
        gameObject.SetActive(false);
    }

    public void ChangeAllArmySizes()
    {
        var box = State.GameManager.CreateInputBox();
        box.SetData(ChangeArmySizes, "Change them", "Cancel", "Change all army max sizes? (1-48)", 2);
    }

    void ChangeArmySizes(int size)
    {
        foreach (var empire in Empires)
        {
            empire.MaxArmySize.value = size;
        }
    }

    public void ChangeAllGarrisonSizes()
    {
        var box = State.GameManager.CreateInputBox();
        box.SetData(ChangeGarrisonSizes, "Change them", "Cancel", "Change all max garrison sizes? (1-48)", 2);
    }

    void ChangeGarrisonSizes(int size)
    {
        foreach (var empire in Empires)
        {
            empire.MaxGarrisonSize.value = size;
        }
    }

    public void ShowSettings()
    {
        StringBuilder right = new StringBuilder();

        right.AppendLine($"Max Item Slots: {Config.ItemSlots}");
        //right.AppendLine($"Regurgitate friendly units: {(Config.FriendlyRegurgitation ? "Yes" : "No")}");
        right.AppendLine($"Exp required per level (base): {Config.ExperiencePerLevel}");
        right.AppendLine($"Additional exp required per level: {Config.AdditionalExperiencePerLevel}");
        right.AppendLine($"Level Soft Cap: {Config.SoftLevelCap}");
        right.AppendLine($"Level Hard Cap: {Config.HardLevelCap}");
        right.AppendLine($"Victory Type: {Config.VictoryCondition}");
        right.AppendLine($"Gold Mine Income : {Config.GoldMineIncome}");
        right.AppendLine($"Leader Exp % lost on Death : {Math.Round(Config.LeaderLossExpPct * 100, 2)}");
        right.AppendLine($"Leader Levels lost on Death : {Config.LeaderLossLevels}");
        if (State.World.crazyBuildings)
            right.AppendLine($"Annoynimouse's crazy buildings are on...");

        RightText.text = right.ToString();
    }

    public void UpdateColors()
    {
        foreach (var emp in Empires)
        {
            emp.PrimaryColor.GetComponent<Image>().color = CreateStrategicGame.ColorFromIndex(emp.PrimaryColor.value);
            emp.SecondaryColor.GetComponent<Image>().color = CreateStrategicGame.GetDarkerColor(CreateStrategicGame.ColorFromIndex(emp.SecondaryColor.value));
        }
    }

}
