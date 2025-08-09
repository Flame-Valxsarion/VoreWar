using System;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class InfoPanel
{
    UnitInfoPanel UnitInfoPanel;
    string parentMenu;
    public InfoPanel(UnitInfoPanel infoPanel, string menu = "unknown")
    {
        UnitInfoPanel = infoPanel;
        parentMenu = menu;
    }

    public Actor_Unit lastActor;

    public void RefreshLastUnitInfo()
    {
        if (lastActor == null)
            return;
        UpdatePanel(lastActor);
    }

    internal void HidePanel()
    {
        ClearText();
        UnitInfoPanel.gameObject.SetActive(false);
        UnitInfoPanel.Sprite?.transform.parent.gameObject.SetActive(false);
    }

    public void RefreshTacticalUnitInfo(Actor_Unit actor)
    {
        if (actor != null)
            lastActor = actor;
        if (actor == null)
        {
            //ClearText();
            return;
        }
        UnitInfoPanel.gameObject.SetActive(true);
        UnitInfoPanel.Sprite?.transform.parent.gameObject.SetActive(Config.HideUnitViewer == false);
        UpdatePanel(actor);

    }

    private void UpdatePanel(Actor_Unit actor)
    {
        if (actor.Hidden)
            return;
        UpdateBars(actor.Unit);
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        BuildStatus(sb2, actor.Unit);
        BuildStats(sb, actor.Unit, actor.Unit.Predator, actor);
        if (actor.Unit.Predator)
            BuildPredStat(sb, actor);
        if (parentMenu == "unitEditor")
        {
            UnitInfoPanel.InfoText.text = sb2.ToString();
        }
        else
        {
            UnitInfoPanel.InfoText.text = sb.ToString();
            UnitInfoPanel.BasicInfo.text = sb2.ToString();
        }
        UnitInfoPanel.Unit = actor.Unit;
        UnitInfoPanel.Actor = actor;
        if (Config.HideUnitViewer == false)
        {
            UnitInfoPanel.Sprite?.ResetBellyScale(actor);
            UnitInfoPanel.Sprite?.UpdateSprites(actor, false);
        }
    }

    private void UpdateBars(Unit actor, bool showNextText = false)
    {
        UnitInfoPanel.ExpBar.GetComponentInChildren<Text>().text = $"EXP: {(int)actor.Experience} ";
        if (showNextText)
            UnitInfoPanel.ExpBar.GetComponentInChildren<Text>().text += $"(To Next: {actor.ExperienceRequiredForNextLevel - (int)actor.Experience})";
        UnitInfoPanel.HealthBar.GetComponentInChildren<Text>().text = $"Health: {actor.Health}/{actor.MaxHealth}" + (actor.Barrier > 0 ? $" + {actor.Barrier}" : "");
        UnitInfoPanel.ManaBar.GetComponentInChildren<Text>().text = $"Mana: {actor.Mana}/{actor.MaxMana}";
        if (actor.ExperienceRequiredForNextLevel != 0)
            UnitInfoPanel.ExpBar.value = (actor.Experience - actor.GetExperienceRequiredForLevel(actor.Level - 1)) / (actor.ExperienceRequiredForNextLevel - actor.GetExperienceRequiredForLevel(actor.Level - 1));
        else
            UnitInfoPanel.ExpBar.value = 1;
        UnitInfoPanel.HealthBar.value = actor.HealthPct;
        UnitInfoPanel.BarrierBar.value = actor.BarrierPct;
        UnitInfoPanel.ManaBar.value = (float)actor.Mana / actor.MaxMana;
    }

    public void RefreshStrategicUnitInfo(Unit unit)
    {
        if (unit == null)
        {
            ClearText();
            return;
        }
        UpdateBars(unit, true);
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        BuildStats(sb, unit, unit.Predator, null);
        UnitInfoPanel.InfoText.text = sb.ToString();
        BuildStatus(sb2, unit);
        UnitInfoPanel.BasicInfo.text = sb2.ToString();
        UnitInfoPanel.Unit = unit;
        UnitInfoPanel.Actor = null;
    }

    internal void AddCorpse(string name)
    {
        if (name == "")
            return;
        UnitInfoPanel.InfoText.text = UnitInfoPanel.InfoText.text += $"Corpse of {name}\n";
    }

    internal void AddClothes(string name)
    {
        if (name == "")
            return;
        UnitInfoPanel.InfoText.text = UnitInfoPanel.InfoText.text += $"Clothes from {name}\n";
    }

    internal void AddLine(string description)
    {
        if (description == "")
            return;
        UnitInfoPanel.InfoText.text = UnitInfoPanel.InfoText.text += $"{description}\n";
    }

    public void ClearText()
    {
        UnitInfoPanel.InfoText.text = "";
    }

    public void ClearPicture()
    {
        UnitInfoPanel.Sprite.transform.parent.gameObject.SetActive(false);
    }


    public static string RaceSingular(Unit unit)
    {
        if (unit.Race >= Race.Selicia)
            return unit.Race.ToString();
        switch (unit.Race)
        {
            case Race.Cats:
                return "Cat";
            case Race.Dogs:
                return "Dog";
            case Race.Foxes:
                return "Fox";
            case Race.Youko:
                return "Youko";
            case Race.Wolves:
                return "Wolf";
            case Race.Bunnies:
                return "Bunny";
            case Race.Lizards:
                return "Lizard";
            case Race.Slimes:
                return "Slime";
            case Race.Scylla:
                return "Scylla";
            case Race.Harpies:
                return "Harpy";
            case Race.Imps:
                return "Imp";
            case Race.Humans:
                return "Human";
            case Race.Crypters:
                return "Crypter";
            case Race.Lamia:
                return "Lamia";
            case Race.Kangaroos:
                return "Kangaroo";
            case Race.Taurus:
                return "Cow";
            case Race.Crux:
                return "Crux";
            case Race.Equines:
                return "Equine";
            case Race.Sergal:
                return "Sergal";
            case Race.Bees:
                return "Bee";
            case Race.Driders:
                return "Drider";
            case Race.Alraune:
                return "Alraune";
            case Race.Bats:
                return "Bat";
            case Race.Tigers:
                return "Tiger";
            case Race.Goblins:
                return "Goblin";
            case Race.Succubi:
                if (unit.GetGender() == Gender.Female)
                    return "Succubus";
                else if (unit.GetGender() == Gender.Male)
                    return "Incubus";
                else
                    return "Concubus";
            case Race.Alligators:
                return "Alligator";
            case Race.Puca:
                return "Puca";
            case Race.Hamsters:
                return "Hamster";
            case Race.Hippos:
                return "Hippo";
            case Race.Vipers:
                return "Viper";
            case Race.Komodos:
                return "Komodo";
            case Race.Vagrants:
                return "Vagrant";
            case Race.Serpents:
                return "Serpent";
            case Race.Wyvern:
                return "Wyvern";
            case Race.WyvernMatron:
                return "Wyvern Matron";
            case Race.Compy:
                return "Compy";
            case Race.FeralWolves:
                return "Wolf";
            case Race.FeralSharks:
                return "Shark";
            case Race.DarkSwallower:
                return "Swallower";
            case Race.Cake:
                return "Cake";
            case Race.Harvesters:
                return "Harvester";
            case Race.Collectors:
                return "Collector";
            case Race.Vision:
                return "Vision";
            case Race.Voilin:
                return "Voilin";
            case Race.FeralBats:
                return "Bat";
            case Race.FeralFrogs:
                return "Frog";
            case Race.Dragon:
                return "Dragon";
            case Race.Dragonfly:
                return "Dragonfly";
            case Race.TwistedVines:
                return "Plant";
            case Race.Fairies:
                return "Fairy";
            case Race.DRACO:
                return "D.R.A.C.O";
            case Race.FeralAnts:
                return "Ant";
            case Race.Gryphons:
                return "Gryphon";
            case Race.SpitterSlugs:
                return "SpitterSlug";
            case Race.SpringSlugs:
                return "SpringSlug";
            case Race.RockSlugs:
                return "RockSlug";
            case Race.CoralSlugs:
                return "CoralSlug";
            case Race.DewSprites:
                return "Sprite";
            case Race.Panthers:
                return "Panther";
            case Race.Salamanders:
                return "Salamander";
            case Race.EasternDragon:
                return "Eastern Dragon";
            case Race.Merfolk:
                return "Merfolk";
            case Race.Mantis:
                return "Mantis";
            case Race.Avians:
                return "Avian";
            case Race.Catfish:
                return "Catfish";
            case Race.Raptor:
                return "Raptor";
            case Race.Ants:
                return "Ant";
            case Race.WarriorAnts:
                return "Warrior Ants";
            case Race.Frogs:
                return "Frog";
            case Race.Gazelle:
                return "Gazelle";
            case Race.Sharks:
                return "Shark";
            case Race.Earthworms:
                return "Earthworm";
            case Race.FeralLizards:
                return "Lizard";
            case Race.Cockatrice:
                return "Cockatrice";
            case Race.Monitors:
                return "Monitor";
            case Race.Deer:
                return "Deer";
            case Race.Mice:
                return "Mouse";
            case Race.Schiwardez:
                return "Schiwardez";
            case Race.Terrorbird:
                return "Terrorbird";
            case Race.Dratopyr:
                return "Dratopyr";
            case Race.FeralLions:
                return "Lion";
            case Race.Kobolds:
                return "Kobold";
            case Race.Whisp:
                return "Whisp";
            case Race.FeralHorses:
                return "Horse";
            case Race.Abakhanskya:
                return "Khatunlao";
            case Race.Singularity:
                return "Faun";
            case Race.Feit:
                return "Draconic Raptor";
            case Race.FeralFox:
                return "Fox";
            case Race.Terminid:
                return "Terminid";
            case Race.FeralOrcas:
                return "Orca";
            case Race.BoomBunnies:
                return "Boom Bunnies";
            case Race.Gnolls:
                return "Gnoll";
            case Race.Centaur:
                return "Centaur";
            case Race.FeralSlime:
                return "Slime";
            case Race.MainlandElves:
                return "Elf";
            case Race.ViraeUltimae:
                return "ViraeUltimae";
            case Race.Viisels:
                return "Viisel";
            case Race.Umbreon:
                return "Umbreon";
            case Race.Eevee:
                return "Eevee";
            case Race.Equaleon:
                return "Equaleon";
            case Race.FeralUmbreon:
                return "Umbreon";
            case Race.FeralEevee:
                return "Eevee";
            case Race.FeralEqualeon:
                return "Equaleon";
            case Race.Ghosts:
                return "Ghost";
            case Race.WoodDryad:
                return "WoodDryad";
            case Race.EarthDryad:
                return "EarthDryad";
            case Race.RiverDryad:
                return "RiverDryad";
            case Race.FungalDryad:
                return "FungalDryad";
            case Race.Otachi:
                return "Otachi";
            case Race.Raiju:
                return "Raiju";
            case Race.Smudger:
                return "Smudger";    
            case Race.SpaceCroach:
                return "SpaceRoach";
            case Race.Trex:
                return "T-Rex";
            case Race.Utahraptor:
                return "Utahraptor";
            case Race.none:
                return "None";
        }
        return unit.Race.ToString(); //Updated this so a new race will return the race's name, instead of nothing
    }

    public static string RaceSingular(Empire empire)
    {
        if (empire?.ReplacedRace >= Race.Selicia)
            return "";
        switch (empire?.ReplacedRace)
        {
            case Race.Cats:
                return "Cat";
            case Race.Dogs:
                return "Dog";
            case Race.Foxes:
                return "Fox";
            case Race.Wolves:
                return "Wolf";
            case Race.Bunnies:
                return "Bunny";
            case Race.Lizards:
                return "Lizard";
            case Race.Slimes:
                return "Slime";
            case Race.Scylla:
                return "Scylla";
            case Race.Harpies:
                return "Harpy";
            case Race.Imps:
                return "Imp";
            case Race.Humans:
                return "Human";
            case Race.Crypters:
                return "Crypter";
            case Race.Lamia:
                return "Lamia";
            case Race.Kangaroos:
                return "Kangaroo";
            case Race.Taurus:
                return "Cow";
            case Race.Crux:
                return "Crux";
            case Race.Equines:
                return "Equine";
            case Race.Sergal:
                return "Sergal";
            case Race.Bees:
                return "Bee";
            case Race.Driders:
                return "Drider";
            case Race.Alraune:
                return "Alraune";
            case Race.Bats:
                return "Bat";
            case Race.Tigers:
                return "Tiger";
            case Race.Goblins:
                return "Goblin";
            case Race.Succubi:
                return "Succubus";
            case Race.Alligators:
                return "Alligator";
            case Race.Puca:
                return "Puca";
            case Race.Hamsters:
                return "Hamster";
            case Race.Hippos:
                return "Hippo";
            case Race.Vipers:
                return "Viper";
            case Race.Komodos:
                return "Komodo";
            case Race.Vagrants:
                return "Vagrant";
            case Race.Serpents:
                return "Serpent";
            case Race.Wyvern:
                return "Wyvern";
            case Race.WyvernMatron:
                return "Wyvern Matron";
            case Race.Compy:
                return "Compy";
            case Race.FeralWolves:
                return "Feral Wolf";
            case Race.FeralSharks:
                return "Shark";
            case Race.DarkSwallower:
                return "Dark Swallower";
            case Race.Cake:
                return "Cake";
            case Race.Harvesters:
                return "Harvester";
            case Race.Collectors:
                return "Collector";
            case Race.Vision:
                return "Vision";
            case Race.Voilin:
                return "Voilin";
            case Race.FeralBats:
                return "Bat";
            case Race.Kobolds:
                return "Kobolds";
            case Race.FeralFrogs:
                return "Feral Frog";
            case Race.Dragon:
                return "Dragon";
            case Race.Dragonfly:
                return "Dragonfly";
            case Race.TwistedVines:
                return "Plant";
            case Race.Fairies:
                return "Fairy";
            case Race.DRACO:
                return "D.R.A.C.O";
            case Race.FeralAnts:
                return "Ant";
            case Race.Gryphons:
                return "Gryphon";
            case Race.SpitterSlugs:
                return "Spitter Slug";
            case Race.SpringSlugs:
                return "Spring Slug";
            case Race.RockSlugs:
                return "Rock Slug";
            case Race.CoralSlugs:
                return "Coral Slug";
            case Race.DewSprites:
                return "Dew Sprite";
            case Race.Panthers:
                return "Panther";
            case Race.Salamanders:
                return "Salamander";
            case Race.EasternDragon:
                return "Eastern Dragon";
            case Race.Merfolk:
                return "Merfolk";
            case Race.Mantis:
                return "Mantis";
            case Race.Avians:
                return "Avian";
            case Race.Catfish:
                return "Catfish";
            case Race.Raptor:
                return "Raptor";
            case Race.Ants:
                return "Ant";
            case Race.WarriorAnts:
                return "Warrior Ant";
            case Race.Frogs:
                return "Frog";
            case Race.Gazelle:
                return "Gazelle";
            case Race.Sharks:
                return "Shark";
            case Race.Earthworms:
                return "Earthworm";
            case Race.FeralLizards:
                return "Feral Lizard";
            case Race.Cockatrice:
                return "Cockatrice";
            case Race.Monitors:
                return "Monitor";
            case Race.Deer:
                return "Deer";
            case Race.Mice:
                return "Mouse";
            case Race.Schiwardez:
                return "Schiwardez";
            case Race.Terrorbird:
                return "Terrorbird";
            case Race.Dratopyr:
                return "Dratopyr";
            case Race.FeralLions:
                return "Lion";
            case Race.FeralHorses:
                return "Horse";
            case Race.Whisp:
                return "Whisp";
            case Race.Abakhanskya:
                return "Khatunlao";
            case Race.Singularity:
                return "Faun";
            case Race.Feit:
                return "Draconic Raptor";
            case Race.FeralFox:
                return "Fox";
            case Race.Terminid:
                return "Terminid";
            case Race.FeralOrcas:
                return "Feral Orca";
            case Race.BoomBunnies:
                return "Boom Bunny";
            case Race.Gnolls:
                return "Gnoll";
            case Race.Centaur:
                return "Centaur";
            case Race.FeralSlime:
                return "Slime";
            case Race.MainlandElves:
                return "Mainland Elf";
            case Race.ViraeUltimae:
                return "Virae Ultimae";
            case Race.Viisels:
                return "Viisel";
            case Race.Umbreon:
                return "Umbreon";
            case Race.Eevee:
                return "Eevee";
            case Race.Equaleon:
                return "Equaleon";
            case Race.FeralUmbreon:
                return "Umbreon";
            case Race.FeralEevee:
                return "Eevee";
            case Race.FeralEqualeon:
                return "Equaleon";
            case Race.Ghosts:
                return "Ghost";
            case Race.WoodDryad:
                return "Wood Dryad";
            case Race.EarthDryad:
                return "Earth Dryad";
            case Race.RiverDryad:
                return "River Dryad";
            case Race.FungalDryad:
                return "Fungal Dryad";
            case Race.Otachi:
                return "Otachi";
            case Race.Raiju:
                return "Raiju";
            case Race.Smudger:
                return "Smudger";    
            case Race.SpaceCroach:
                return "Space Roach";
            case Race.Trex:
                return "T-Rex";
            case Race.Utahraptor:
                return "Utahraptor";
        }
        return empire.ReplacedRace.ToString();
    }

    void BuildStatus(StringBuilder sb, Unit unit)
    {
        // Add Name
        if (unit.Type == UnitType.Summon)
            sb.AppendLine(unit.Name + "(Summon)");
        else
            sb.AppendLine(unit.Name);

        // Add Level and Race
        sb.AppendLine($"Level {unit.Level} {RaceSingular(unit)}");

        // Add Gender and Type
        if (unit.Race >= Race.Selicia)
            sb.AppendLine($"{unit.Type}");
        else
        {
            var race = Races.GetRace(unit.Race);
            if (race != null && race.CanBeGender.Contains(Gender.None))
                sb.AppendLine($"{unit.Type}");
            else if (unit.GetGender() == Gender.Hermaphrodite)
                sb.AppendLine($"Herm {unit.Type}");
            else if (unit.GetGender() == Gender.Andromorph)
                sb.AppendLine($"Andro {unit.Type}");
            else
                sb.AppendLine($"{unit.GetGender()} {unit.Type}");
        }
    }

    void BuildStats(StringBuilder sb, Unit unit, bool CanVore, Actor_Unit actor)
    {
        if (parentMenu != "unitEditor")
        {
            if (unit.HasFixedSide() && TacticalUtilities.PlayerCanSeeTrueSide(unit))
            {
                if (State.World.MainEmpires == null)
                {
                    sb.AppendLine($"Special Allegiance: {(unit.FixedSide == State.GameManager.TacticalMode.GetDefenderSide() ? "Defender" : "Attacker")}");
                }
                else
                    sb.AppendLine($"Special Allegiance: {State.World.GetEmpireOfSide(unit.FixedSide)?.Name ?? "Unkown"}");
            }
            UnityEngine.Transform EquipRow = UnitInfoPanel.StatBlock.transform.GetChild(5);

            if (unit.GetItem(0)?.Name == null)
                EquipRow.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            else
                EquipRow.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = unit.GetItem(0)?.Name;
            if (unit.GetItem(1)?.Name == null)
                EquipRow.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            else
                EquipRow.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = unit.GetItem(1)?.Name;
            if (unit.HasTrait(Traits.Resourceful))
            {
                EquipRow.transform.GetChild(2).gameObject.SetActive(true);
            if (unit.GetItem(2)?.Name == null)
                EquipRow.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            else
                EquipRow.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = unit.GetItem(2)?.Name;
            }
            else
                EquipRow.transform.GetChild(2).gameObject.SetActive(false);

            // Add Equipment
            //for (int i = 0; i < unit.Items.Length; i++)
            //{
            //    sb.AppendLine(unit.GetItem(i)?.Name ?? "empty");
            //}

            Text STRVal = UnitInfoPanel.StatBlock.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
            Text DEXVal = UnitInfoPanel.StatBlock.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>();
            Text MNDVal = UnitInfoPanel.StatBlock.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Text>();
            Text WLLVal = UnitInfoPanel.StatBlock.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Text>();
            Text ENDVal = UnitInfoPanel.StatBlock.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Text>();
            Text AGIVal = UnitInfoPanel.StatBlock.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>();
            Text VORVal = UnitInfoPanel.StatBlock.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<Text>();
            Text STMVal = UnitInfoPanel.StatBlock.transform.GetChild(3).GetChild(1).GetChild(1).GetComponent<Text>();
            Text LDRVal = UnitInfoPanel.StatBlock.transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<Text>();

            // Add Battle Stats
            STRVal.text = unit.GetStatInfo(Stat.Strength);
            DEXVal.text = unit.GetStatInfo(Stat.Dexterity);
            ENDVal.text = unit.GetStatInfo(Stat.Endurance);
            AGIVal.text = unit.GetStatInfo(Stat.Agility);
            if (unit.HasTrait(Traits.Brainless))
            {
                MNDVal.text = "<color=red>1</color>";
            }
            else
                MNDVal.text = unit.GetStatInfo(Stat.Mind);
            WLLVal.text = unit.GetStatInfo(Stat.Will);
            if (CanVore)
            {
                UnitInfoPanel.StatBlock.transform.GetChild(3).gameObject.SetActive(true);
                VORVal.text = unit.GetStatInfo(Stat.Voracity);
                STMVal.text = unit.GetStatInfo(Stat.Stomach);
            }
            else
                UnitInfoPanel.StatBlock.transform.GetChild(3).gameObject.SetActive(false);

            UnityEngine.Transform FifthLine = UnitInfoPanel.StatBlock.transform.GetChild(4);

            // Add Leadership Stat if unit is a Leader
            if (unit.Type == UnitType.Leader)
            {
                FifthLine.gameObject.SetActive(true);
                FifthLine.GetChild(0).gameObject.SetActive(true);
                LDRVal.text = unit.GetStatInfo(Stat.Leadership);
            }
            else
                FifthLine.GetChild(0).gameObject.SetActive(false);

            UnityEngine.Transform killSection = FifthLine.GetChild(1).GetChild(0);
            UnityEngine.Transform digestSection = FifthLine.GetChild(1).GetChild(1);
            UnityEngine.Transform deathSection = FifthLine.GetChild(1).GetChild(2);
            // Set kill counter, hide otherwise
            if (unit.KilledUnits > 0)
            {
                FifthLine.gameObject.SetActive(true);
                FifthLine.GetChild(1).gameObject.SetActive(true);
                killSection.gameObject.SetActive(true);
                killSection.GetChild(1).GetComponent<Text>().text = unit.KilledUnits.ToString();
            }
            else
                killSection.gameObject.SetActive(false);
            // Set digestion counter, hide otherwise
            if (unit.DigestedUnits > 0)
            {
                FifthLine.gameObject.SetActive(true);
                FifthLine.GetChild(1).gameObject.SetActive(true);
                digestSection.gameObject.SetActive(true);
                digestSection.GetChild(1).GetComponent<Text>().text = unit.DigestedUnits.ToString();
            }
            else
                digestSection.gameObject.SetActive(false);
            // Set death counter, hide otherwise
            if (unit.TimesKilled > 0)
            {
                FifthLine.gameObject.SetActive(true);
                FifthLine.GetChild(1).gameObject.SetActive(true);
                deathSection.gameObject.SetActive(true);
                deathSection.GetChild(1).GetComponent<Text>().text = unit.TimesKilled.ToString();
            }
            else
                deathSection.gameObject.SetActive(false);
            if (!killSection.gameObject.activeSelf && !digestSection.gameObject.activeSelf && !deathSection.gameObject.activeSelf)
                FifthLine.GetChild(1).gameObject.SetActive(false);
            if (!FifthLine.GetChild(0).gameObject.activeSelf && !FifthLine.GetChild(1).gameObject.activeSelf)
                FifthLine.gameObject.SetActive(false);

            if (Config.ShowUnitSides && actor?.Unit.Side == State.GameManager.TacticalMode.GetDefenderSide())
                sb.AppendLine("DEFENDER");
            if (Config.ShowUnitSides && actor?.Unit.Side == State.GameManager.TacticalMode.GetAttackerSide())
                sb.AppendLine("ATTACKER");
            if (unit.SavedCopy != null && unit.SavedVillage != null)
                sb.AppendLine($"Imprinted");
            if (actor?.Surrendered ?? false)
                sb.AppendLine("Unit has surrendered!");
            string traits = unit.ListTraits(!(TacticalUtilities.IsUnitControlledByPlayer(unit) && TacticalUtilities.PlayerCanSeeTrueSide(unit)));
            if (traits != "")
                sb.AppendLine("Traits:\n" + traits);
            StringBuilder sbSecond = new StringBuilder();
            sbSecond.AppendLine("Status:");
            if (unit.HasTrait(Traits.Frenzy) && unit.EnemiesKilledThisBattle > 0)
                sbSecond.AppendLine($"Frenzy ({unit.EnemiesKilledThisBattle})");
            if (unit.HasTrait(Traits.Growth) && unit.BaseScale > 1)
                sbSecond.AppendLine($"Growth ({Math.Round(unit.BaseScale, 2)}x)");
            if (actor?.Slimed ?? false)
                sbSecond.AppendLine("Slimed");
            if (actor?.Paralyzed ?? false)
                sbSecond.AppendLine("Paralyzed");
            if (actor?.Corruption > 0 && !TacticalUtilities.IsUnitControlledByPlayer(unit))
                sbSecond.AppendLine($"Corruption ({actor.Corruption}/{unit.GetStatTotal() + unit.GetStat(Stat.Will)})");
            if (actor?.Possessed > 0 && !TacticalUtilities.IsUnitControlledByPlayer(unit))
                sbSecond.AppendLine($"Possessed ({actor.Corruption + actor.Possessed}/{unit.GetStatTotal() + unit.GetStat(Stat.Will)})");
            if (actor?.Infected ?? false)
                sbSecond.AppendLine($"Infected");
            if (unit.StatusEffects?.Any() ?? false)
            {
                foreach (StatusEffectType type in (StatusEffectType[])Enum.GetValues(typeof(StatusEffectType)))
                {
                    var effect = unit.GetLongestStatusEffect(type);
                    if (unit.GetLongestStatusEffect(type) != null)
                        sbSecond.AppendLine($"{type} ({effect.Duration})");
                }
            }

            if (sbSecond.Length > 10)
                sb.Append($"{sbSecond}");

            var racePar = RaceParameters.GetTraitData(unit);

            if ((unit.InnateSpells?.Any() ?? false) || (racePar.InnateSpells?.Any() ?? false))
            {
                sb.AppendLine("Innate Spells:");
                if (unit.InnateSpells != null)
                {
                    foreach (var spellType in unit.InnateSpells)
                    {
                        if (SpellList.SpellDict.TryGetValue(spellType, out Spell spell))
                        {
                            sb.AppendLine(spell.Name);
                        }
                        else
                        {
                            sb.AppendLine("Invalid Spell");
                        }
                    }
                }
                if (racePar.InnateSpells != null)
                {
                    foreach (var spellType in racePar.InnateSpells)
                    {
                        if (SpellList.SpellDict.TryGetValue(spellType, out Spell spell))
                        {
                            sb.AppendLine(spell.Name);
                        }
                        else
                        {
                            sb.AppendLine("Invalid Spell");
                        }
                    }
                }
                var grantedSpell = State.RaceSettings.GetInnateSpell(unit.Race);
                if (grantedSpell != SpellTypes.None)
                {
                    if (SpellList.SpellDict.TryGetValue(grantedSpell, out Spell spell))
                    {
                        sb.AppendLine(spell.Name);
                    }
                    else
                    {
                        sb.AppendLine("Invalid Spell");
                    }
                }
            }
            if (Config.CheatUnitEditorEnabled)
                sb.AppendLine("<color=#AB5200ff>UnitEditor</color>");
        }
    }

    void BuildPredStat(StringBuilder sb, Actor_Unit unit)
    {
        sb.Append(unit.PredatorComponent.GetPreyInformation());

    }


}
