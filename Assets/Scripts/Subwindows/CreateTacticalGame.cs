using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CreateTacticalGame : MonoBehaviour
{
    public StartTacticalUI Attacker;
    public StartTacticalUI Defender;

    public InputField SizeX;
    public InputField SizeY;
    public Toggle AutoScale;

    public Dropdown AIOverride;
    public Dropdown TerrainType;

    public Text TooltipText;

    public GameObject BulkPanel;
    public Slider Bulk;
    public Toggle AllRaces;

    public Actor_Unit AttackerUnit;
    public Actor_Unit DefenderUnit;

    public UIUnitSprite AttackerSprite;
    public UIUnitSprite DefenderSprite;

    bool batching;

    Race batchingAttackerRace;
    Race batchingDefenderRace;

    Race[] AllRandomRaces;

    public void Start()
    {
        //foreach (Race race in (Race[])Enum.GetValues(typeof(Race)))
        //{
        //    Attacker.Race.options.Add(new Dropdown.OptionData(race.ToString()));
        //    Defender.Race.options.Add(new Dropdown.OptionData(race.ToString()));
        //}
        foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).Where(s => (int)s >= 0).OrderBy((s) => s.ToString()))
        {
            Attacker.Race.options.Add(new Dropdown.OptionData(race.ToString()));
            Defender.Race.options.Add(new Dropdown.OptionData(race.ToString()));
        }

    }

    internal void Open()
    {
        AttackerRaceChanged();
        DefenderRaceChanged();
    }

    public void AttackerRaceChanged()
    {
        if (AttackerUnit == null)
        {
            AttackerUnit = new Actor_Unit(new Unit(Race.Cats));
        }

        Race race;

        if (Enum.TryParse(Attacker.Race.captionText.text, out race))
        {

        }
        else
        {
            race = GetRandomRace();
        }
        AttackerUnit.Unit.Race = race;
        AttackerUnit.Unit.TotalRandomizeAppearance();
        AttackerSprite.UpdateSprites(AttackerUnit);
        AttackerSprite.Name.text = race.ToString();

    }

    public void DefenderRaceChanged()
    {
        if (DefenderUnit == null)
        {
            DefenderUnit = new Actor_Unit(new Unit(Race.Cats));
        }

        if (Enum.TryParse(Defender.Race.captionText.text, out Race race))
        {

        }
        else
        {
            race = GetRandomRace();
        }

        DefenderUnit.Unit.Race = race;
        DefenderUnit.Unit.TotalRandomizeAppearance();
        DefenderSprite.UpdateSprites(DefenderUnit);
        DefenderSprite.Name.text = race.ToString();
    }

    public void UpdateAIBoxes()
    {
        Attacker.TacticalAI.interactable = Attacker.AIPlayer.isOn;
        Defender.TacticalAI.interactable = Defender.AIPlayer.isOn;
        AIOverride.interactable = Attacker.AIPlayer.isOn && Defender.AIPlayer.isOn;
        WatchOptionChanged();
    }

    public void UpdateTextPercentages()
    {
        Attacker.RangedText.text = $"{Attacker.RangedPercentage.value}%";
        Defender.RangedText.text = $"{Defender.RangedPercentage.value}%";
        Attacker.WeaponsText.text = $"{Attacker.HeavyWeaponsPercentage.value}%";
        Defender.WeaponsText.text = $"{Defender.HeavyWeaponsPercentage.value}%";
        Attacker.MagicText.text = $"{Attacker.MagicPercentage.value}%";
        Defender.MagicText.text = $"{Defender.MagicPercentage.value}%";
    }

    public void AutoScaleChanged()
    {
        SizeX.interactable = !AutoScale.isOn;
        SizeY.interactable = !AutoScale.isOn;
    }

    string TestTacticalSize()
    {
        int x = Convert.ToInt32(SizeX.text);
        int y = Convert.ToInt32(SizeY.text);

        int MaxUnitsOnHalf = Math.Max(Convert.ToInt32(Attacker.UnitCount.text), Convert.ToInt32(Defender.UnitCount.text));



        if (x < 8 || y < 8)
            return "Can't have a tactical dimension less than 8";

        if (MaxUnitsOnHalf > x * y / 14)
            return "Not enough space to comfortably fit specified number of units on at least one of the halves";

        return "";

    }

    string TestMisc()
    {
        if (Convert.ToInt32(Attacker.UnitCount.text) < 1 || Convert.ToInt32(Defender.UnitCount.text) < 1)
            return "Both sides need to have units";
        if (Convert.ToInt32(Attacker.Level.text) < 1 || Convert.ToInt32(Defender.Level.text) < 1)
            return "You can't have levels less than 1";
        return "";
    }

    public void UpdateBulkSlider()
    {
        Bulk.GetComponentInChildren<Text>().text = $"Runs per matchup: {Bulk.value}";
    }

    public void WatchOptionChanged()
    {
        BulkPanel.gameObject.SetActive(AIOverride.value == 0 && AIOverride.interactable);
    }

    public void SuperBulk()
    {
        batching = true;
        StartCoroutine(CompleteBatch());
    }

    public void SuperBulkSingleRace()
    {
        batching = true;
        StartCoroutine(SingleRaceBatch());
    }

    internal IEnumerator SingleRaceBatch()
    {
        float time = Time.realtimeSinceStartup;
        List<Race> participants = new List<Race>();
        batching = true;
        foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).Where(s => (int)s >= 0))
        {
            if (AllRaces.isOn == false && race == Race.Succubi)
                break;
            if (race == Race.Selicia)
                break;
            participants.Add(race);

        }


        if (Enum.TryParse(Attacker.Race.options[Attacker.Race.value].text, out Race thisRace))
        {
            batchingAttackerRace = thisRace;
        }


        int[] grid = new int[participants.Count];
        int remaining = 5;

        for (int j = 0; j < participants.Count; j++)
        {
            if (participants[j] == batchingAttackerRace)
                continue;
            State.GameManager.TacticalMode.Wins = new Vector2Int();
            for (int k = 0; k < Bulk.value; k++)
            {
                batchingDefenderRace = participants[j];
                CreateTactical();
                State.GameManager.TacticalMode.CleanUp();
                remaining--;
                if (remaining == 0)
                {
                    remaining = 5;
                    yield return null;
                }
            }
            grid[j] = State.GameManager.TacticalMode.Wins.x;
        }
        StringBuilder sb = new StringBuilder();
        int total = 0;
        for (int y = 0; y < participants.Count; y++)
        {
            if (participants[y] == batchingAttackerRace)
            {
                sb.Append($"-\t");
                continue;
            }
            total += grid[y];
            sb.Append($"{grid[y]}\t");
        }
        sb.AppendLine();
        sb.AppendLine($"Total Wins: {total}");
        State.GameManager.CreateFullScreenMessageBox(sb.ToString());
        Debug.Log($"Batch completed in {Time.realtimeSinceStartup - time}");
        batching = false;
        WriteOutBatch(sb);
    }

    internal IEnumerator CompleteBatch()
    {
        float time = Time.realtimeSinceStartup;
        List<Race> participants = new List<Race>();

        foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).Where(s => (int)s >= 0))
        {
            if (AllRaces.isOn == false && race == Race.Succubi)
                break;
            if (race == Race.Selicia)
                break;
            participants.Add(race);

        }

        if (Enum.TryParse(Attacker.Race.options[Attacker.Race.value].text, out Race thisRace))
        {
            batchingAttackerRace = thisRace;
        }
        int[,] grid = new int[participants.Count, participants.Count];
        int remaining = 5;
        for (int i = 0; i < participants.Count; i++)
        {
            for (int j = i + 1; j < participants.Count; j++)
            {
                State.GameManager.TacticalMode.Wins = new Vector2Int();
                for (int k = 0; k < Bulk.value; k++)
                {
                    batchingAttackerRace = participants[i];
                    batchingDefenderRace = participants[j];
                    CreateTactical();
                    State.GameManager.TacticalMode.CleanUp();
                    remaining--;
                    if (remaining == 0)
                    {
                        remaining = 5;
                        yield return null;
                    }
                }
                grid[i, j] = State.GameManager.TacticalMode.Wins.x;
                grid[j, i] = State.GameManager.TacticalMode.Wins.y;
            }
        }
        StringBuilder sb = new StringBuilder();
        for (int x = 0; x < participants.Count; x++)
        {
            int total = 0;
            sb.Append($"{participants[x]} \t");
            for (int y = 0; y < participants.Count; y++)
            {
                if (x == y)
                {
                    sb.Append($"-\t");
                }
                else
                {
                    total += grid[x, y];
                    sb.Append($"{grid[x, y]}\t");
                }
            }
            sb.AppendLine($"total Wins: {total}");
        }
        State.GameManager.CreateFullScreenMessageBox(sb.ToString());
        Debug.Log($"Batch completed in {Time.realtimeSinceStartup - time}");
        WriteOutBatch(sb);
        batching = false;
    }



    public void CreateTacticalBulk()
    {
        StartCoroutine(SingleBatch());
    }

    internal IEnumerator SingleBatch()
    {
        float time = Time.realtimeSinceStartup;
        batching = true;
        int remaining = 5;
        State.GameManager.TacticalMode.Wins = new Vector2Int();

        if (Enum.TryParse(Attacker.Race.options[Attacker.Race.value].text, out Race attackerRace))
        {
            batchingAttackerRace = attackerRace;
        }

        if (Enum.TryParse(Attacker.Race.options[Defender.Race.value].text, out Race defenderRace))
        {
            batchingDefenderRace = defenderRace;
        }

        for (int i = 0; i < Bulk.value; i++)
        {
            CreateTactical();
            State.GameManager.TacticalMode.CleanUp();
            remaining--;
            if (remaining == 0)
            {
                remaining = 5;
                yield return null;
            }
        }
        State.GameManager.CreateMessageBox($"Attacker Wins:{State.GameManager.TacticalMode.Wins.x} Defender Wins:{State.GameManager.TacticalMode.Wins.y}");
        batching = false;
        Debug.Log($"Batch completed in {Time.realtimeSinceStartup - time}");
    }

    private static void WriteOutBatch(StringBuilder sb)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter($"UserData{Path.DirectorySeparatorChar}LastBatch.txt"))
            {
                sw.Write(sb.ToString());
            }
        }
        catch
        {
            Debug.Log("Write Failed");
        }
    }

    public void CreateTactical()
    {
        int fightersA;
        int fightersB;
        int levelA;
        int levelB;
        int maxMagicA;
        int maxMagicB;
        try
        {
            string errorText = "";
            if (AutoScale.isOn == false)
                errorText = TestTacticalSize();
            if (errorText != "")
            {
                State.GameManager.CreateMessageBox(errorText);
                return;
            }
            errorText = TestMisc();
            if (errorText != "")
            {
                State.GameManager.CreateMessageBox(errorText);
                return;
            }
            if (Attacker.AIPlayer.isOn && Defender.AIPlayer.isOn)
            {
                Config.WatchAIBattles = AIOverride.value == 1;
                Config.ShowStatsForSkippedBattles = AIOverride.value == 0;
            }

            fightersA = Convert.ToInt32(Attacker.UnitCount.text);
            fightersB = Convert.ToInt32(Defender.UnitCount.text);
            levelA = Convert.ToInt32(Attacker.Level.text);
            levelB = Convert.ToInt32(Defender.Level.text);
            maxMagicA = Convert.ToInt32(Attacker.MaxSpellLevel.text);
            maxMagicB = Convert.ToInt32(Defender.MaxSpellLevel.text);
            Config.World.TacticalSizeX = Convert.ToInt32(SizeX.text);
            Config.World.TacticalSizeY = Convert.ToInt32(SizeY.text);
        }
        catch
        {
            State.GameManager.CreateMessageBox("There is a blank textbox that needs to be filled in");
            return;
        }

        Config.World.SoftLevelCap = 999;

        Config.World.ItemSlots = Config.NewItemSlots;


        TacticalAIType attackerAI = Attacker.AIPlayer.isOn ? (TacticalAIType)(1 + Attacker.TacticalAI.value) : TacticalAIType.None;
        TacticalAIType defenderAI = Defender.AIPlayer.isOn ? (TacticalAIType)(1 + Defender.TacticalAI.value) : TacticalAIType.None;

        Config.World.AutoScaleTactical = AutoScale.isOn;

        Vec2i none = new Vec2i(0, 0);

        State.World = new World(false);
        int attackerRangedCount = fightersA * (int)Attacker.RangedPercentage.value / 100;
        int defenderRangedCount = fightersB * (int)Defender.RangedPercentage.value / 100;
        int attackerMeleeHeavyCount = (fightersA - attackerRangedCount) * (int)Attacker.HeavyWeaponsPercentage.value / 100;
        int defenderMeleeHeavyCount = (fightersB - defenderRangedCount) * (int)Defender.HeavyWeaponsPercentage.value / 100;
        int attackerRangedHeavyCount = attackerRangedCount * (int)Attacker.HeavyWeaponsPercentage.value / 100;
        int defenderRangedHeavyCount = defenderRangedCount * (int)Defender.HeavyWeaponsPercentage.value / 100;
        int attackerSide = 0;
        int defenderSide = 1;
        Func<Race> AttackerRace;
        Func<Race> DefenderRace;
        if (batching == false)
        {
            AttackerRace = SetupRace(Attacker.Race);
            DefenderRace = SetupRace(Defender.Race);

        }
        else
        {
            AttackerRace = () => batchingAttackerRace;
            DefenderRace = () => batchingDefenderRace;
        }


        Func<Race> SetupRace(Dropdown tacRace)
        {
            if (Enum.TryParse(tacRace.options[tacRace.value].text, out Race race))
            {
                return () => race;
            }
            return GetRandomRace;
        }
        Army attacker = new Army(new Empire(), none, attackerSide);
        Army defender = new Army(new Empire(), none, defenderSide);
        attacker.Empire.ReplacedRace = AttackerRace();              
        defender.Empire.ReplacedRace = DefenderRace();
        if (Attacker.HasLeader.isOn)
        {
            attacker.Units.Add(new NPC_unit(levelA, Attacker.RangedPercentage.value >= 50, 3, attackerSide, AttackerRace(), 0, Attacker.CanVore.isOn));
            attacker.Units.Last().ImmuneToDefections = true;
            if (State.Rand.NextDouble() < Attacker.MagicPercentage.value / 100)
                attacker.Units.Last().SetItem(State.World.ItemRepository.GetRandomBook(1, maxMagicA, true), 1);
        }
        if (Defender.HasLeader.isOn)
        {
            defender.Units.Add(new NPC_unit(levelB, Defender.RangedPercentage.value >= 50, 3, defenderSide, DefenderRace(), 0, Defender.CanVore.isOn));
            defender.Units.Last().ImmuneToDefections = true;
            if (State.Rand.NextDouble() < Defender.MagicPercentage.value / 100)
                defender.Units.Last().SetItem(State.World.ItemRepository.GetRandomBook(1, maxMagicB, true), 1);
        }
        for (int x = 0; x < fightersA; x++)
        {
            if (x < attackerRangedCount)
            {
                attacker.Units.Add(new NPC_unit(levelA, attackerRangedHeavyCount > 0, 1, attackerSide, AttackerRace(), 0, Attacker.CanVore.isOn));
                attacker.Units.Last().ImmuneToDefections = true;
                attackerRangedHeavyCount--;
                if (State.Rand.NextDouble() < Attacker.MagicPercentage.value / 100)
                    attacker.Units.Last().SetItem(State.World.ItemRepository.GetRandomBook(1, maxMagicA, true), 1);
            }
            else
            {
                attacker.Units.Add(new NPC_unit(levelA, attackerMeleeHeavyCount > 0, 0, attackerSide, AttackerRace(), 0, Attacker.CanVore.isOn));
                attacker.Units.Last().ImmuneToDefections = true;
                attackerMeleeHeavyCount--;
                if (State.Rand.NextDouble() < Attacker.MagicPercentage.value / 100)
                    attacker.Units.Last().SetItem(State.World.ItemRepository.GetRandomBook(1, maxMagicA, true), 1);
            }
        }
        for (int x = 0; x < fightersB; x++)
        {
            if (x < defenderRangedCount)
            {
                defender.Units.Add(new NPC_unit(levelB, defenderRangedHeavyCount > 0, 1, defenderSide, DefenderRace(), 0, Defender.CanVore.isOn));
                defender.Units.Last().ImmuneToDefections = true;
                defenderRangedHeavyCount--;
                if (State.Rand.NextDouble() < Defender.MagicPercentage.value / 100)
                    defender.Units.Last().SetItem(State.World.ItemRepository.GetRandomBook(1, maxMagicB, true), 1);
            }
            else
            {
                defender.Units.Add(new NPC_unit(levelB, defenderMeleeHeavyCount > 0, 0, defenderSide, DefenderRace(), 0, Defender.CanVore.isOn));
                defender.Units.Last().ImmuneToDefections = true;
                defenderMeleeHeavyCount--;
                if (State.Rand.NextDouble() < Defender.MagicPercentage.value / 100)
                    defender.Units.Last().SetItem(State.World.ItemRepository.GetRandomBook(1, maxMagicB, true), 1);
            }
        }
        Village village = null;
        StrategicTileType tileType = StrategicTileType.grass;
        if (TerrainType.value == 1)
            tileType = StrategicTileType.forest;
        else if (TerrainType.value == 2)
            tileType = StrategicTileType.desert;
        else if (TerrainType.value == 3)
            tileType = StrategicTileType.snow;
        else if (TerrainType.value == 4)
            tileType = StrategicTileType.volcanic;
        else if (TerrainType.value == 5)
            tileType = StrategicTileType.swamp;
        else if (TerrainType.value == 6)
            tileType = StrategicTileType.shallowWater;
        else if (TerrainType.value == 7)
            village = new Village("", none, 0, DefenderRace(), false);
        else if (TerrainType.value == 8)
        {
            village = new Village("", none, 0, DefenderRace(), false);
            village.buildings.Add(VillageBuilding.wall);
        }
        else if (TerrainType.value == 9)
        {
            tileType = StrategicTileType.desert;
            village = new Village("", none, 0, DefenderRace(), false);
        }
        else if (TerrainType.value == 10)
        {
            tileType = StrategicTileType.desert;
            village = new Village("", none, 0, DefenderRace(), false);
            village.buildings.Add(VillageBuilding.wall);
        }
        else if (TerrainType.value == 11)
        {
            tileType = StrategicTileType.volcanic;
            village = new Village("", none, 0, DefenderRace(), false);
        }
        else if (TerrainType.value == 12)
        {
            tileType = StrategicTileType.volcanic;
            village = new Village("", none, 0, DefenderRace(), false);
            village.buildings.Add(VillageBuilding.wall);
        }
        village?.UpdateNetBoosts();

        if (Attacker.Race.value == 0 && batching == false)
        {
            State.GameManager.TacticalMode.AttackerName = "Random";
            attacker.Name = "Random";
        }
        else
        {
            State.GameManager.TacticalMode.AttackerName = AttackerRace().ToString();
            attacker.Name = AttackerRace().ToString();
        }

        if (Defender.Race.value == 0 && batching == false)
        {
            State.GameManager.TacticalMode.DefenderName = "Random";
            defender.Name = "Random";
        }
        else
        {
            State.GameManager.TacticalMode.DefenderName = DefenderRace().ToString();
            defender.Name = DefenderRace().ToString();
        }

        State.GameManager.ActivatePureTacticalMode(tileType, village, attacker, defender, attackerAI, defenderAI);
        if (batching == false)
            gameObject.SetActive(false);
    }


    Race GetRandomRace()
    {
        if (AllRandomRaces == null)
            AllRandomRaces = ((Race[])Enum.GetValues(typeof(Race))).Where(s => (int)s >= 0).ToArray();
        for (int i = 0; i < 4; i++)
        {
            Race race = AllRandomRaces[State.Rand.Next(AllRandomRaces.Length)];
        }
        return (Race)State.Rand.Next(Config.NumberOfRaces);

    }

    public void ChangeToolTip(int type)
    {
        TooltipText.text = DefaultTooltips.Tooltip(type);
    }
}

