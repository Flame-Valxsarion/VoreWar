using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

internal class EventList
{
    EventScreen UI;
    internal CustomEventList CustomEvents;

    public EventList()
    {
        CustomEvents = new CustomEventList();
        CustomEvents.Initialize();
    }

    internal void SetUI(EventScreen eventScreen) => UI = eventScreen;

    internal void CheckStartEvent(Empire empire)
    {
        float odds = 0;
        switch (Config.RandomEventRate)
        {
            case 1:
                odds = .1f;
                break;
            case 2:
                odds = .25f;
                break;
            case 3:
                odds = .5f;
                break;
            case 4:
                odds = .99f;
                break;
        }
        if (odds < State.Rand.NextDouble())
            return;
        UI.FirstChoice.onClick.RemoveAllListeners();
        UI.SecondChoice.onClick.RemoveAllListeners();
        UI.ThirdChoice.onClick.RemoveAllListeners();



        if (Config.CustomEventFrequency > State.Rand.NextDouble())
        {
            StartCustomEvent(empire);

            return;
        }


        bool triggered = false;
        if (empire.RecentEvents == null)
            empire.RecentEvents = new List<int>();
        for (int i = 0; i < 20; i++)
        {
            int val = State.Rand.Next(36);
            if (i < 17 && empire.RecentEvents.Contains(val))
                continue;
            if (Config.EventsRepeat == false && empire.EventHappened.ContainsKey(val))
                continue;
            try
            {
                if (State.EventList.StartEvent(val, empire))
                {
                    empire.EventHappened[val] = true;
                    empire.RecentEvents.Add(val);
                    if (Config.EventsRepeat && empire.RecentEvents.Count() > 8)
                        empire.RecentEvents.RemoveAt(0);
                    triggered = true;
                    break;
                }
            }
            catch
            {
                State.GameManager.CreateMessageBox($"An event managed to create an exception, let the devs know there's a problem with event {val}");
            }

        }

        if (triggered == false)
        {
            StartCustomEvent(empire);
            return;
        }

        return;
    }

    void StartCustomEvent(Empire empire)
    {
        if (CustomEvents.AnyEvents == false)
            return;
        var custom = CustomEvents.GetRandom();

        var village = GetRandomVillage(empire.Side);
        var otherEmpire = GetRandomEmpire(empire);
        if (village == null || otherEmpire == null)
            return;

        UI.gameObject.SetActive(true);
        UI.RaceText.text = $"{empire.Name} -- Gold : {empire.Gold}";

        custom.MainText = custom.MainText.Replace("[village]", village.Name).Replace("[Village]", village.Name);
        custom.Option1Choice = custom.Option1Choice.Replace("[village]", village.Name).Replace("[Village]", village.Name);
        custom.Option1Result = custom.Option1Result.Replace("[village]", village.Name).Replace("[Village]", village.Name);
        custom.Option2Choice = custom.Option2Choice.Replace("[village]", village.Name).Replace("[Village]", village.Name);
        custom.Option2Result = custom.Option2Result.Replace("[village]", village.Name).Replace("[Village]", village.Name);
        custom.MainText = custom.MainText.Replace("[empire]", otherEmpire.Name).Replace("[Empire]", otherEmpire.Name);
        custom.Option1Choice = custom.Option1Choice.Replace("[empire]", otherEmpire.Name).Replace("[Empire]", otherEmpire.Name);
        custom.Option1Result = custom.Option1Result.Replace("[empire]", otherEmpire.Name).Replace("[Empire]", otherEmpire.Name);
        custom.Option2Choice = custom.Option2Choice.Replace("[empire]", otherEmpire.Name).Replace("[Empire]", otherEmpire.Name);
        custom.Option2Result = custom.Option2Result.Replace("[empire]", otherEmpire.Name).Replace("[Empire]", otherEmpire.Name);
        string leaderName;
        if (empire.Leader != null)
            leaderName = empire.Leader.Name;
        else
        {
            if (empire.FakeLeaderName == null || empire.FakeLeaderName.Length < 2)
            {
                Unit unit = new Unit(empire.ReplacedRace);
                empire.FakeLeaderName = unit.Name;
            }
            leaderName = empire.FakeLeaderName;
        }
        custom.MainText = custom.MainText.Replace("[leader]", leaderName).Replace("[Leader]", leaderName);
        custom.Option1Choice = custom.Option1Choice.Replace("[leader]", leaderName).Replace("[Leader]", leaderName);
        custom.Option1Result = custom.Option1Result.Replace("[leader]", leaderName).Replace("[Leader]", leaderName);
        custom.Option2Choice = custom.Option2Choice.Replace("[leader]", leaderName).Replace("[Leader]", leaderName);
        custom.Option2Result = custom.Option2Result.Replace("[leader]", leaderName).Replace("[Leader]", leaderName);


        bool firstGold = empire.Gold + custom.Option1Gold >= 0;
        bool secondGold = firstGold == false || (empire.Gold + custom.Option2Gold >= 0);
        UI.MainText.text = custom.MainText;
        if (custom.Option1Happiness != 0 || custom.Option1Population != 0 || custom.Option2Happiness != 0 || custom.Option2Population != 0)
            UI.MainText.text += AddVillageInfo(village);
        UI.FirstChoice.GetComponentInChildren<Text>().text = custom.Option1Choice;
        UI.FirstChoice.onClick.AddListener(() =>
        {
            string result = "(";
            if (custom.Option1Gold != 0)
            {
                result += $"{custom.Option1Gold} gold, ";
                if (custom.Option1Gold > 0)
                    empire.AddGold(custom.Option1Gold);
                else
                {
                    empire.SpendGold(-custom.Option1Gold);
                }
            }
            if (custom.Option1Happiness != 0)
            {
                result += $"{custom.Option1Happiness} happiness for {village.Name}, ";
                village.Happiness += custom.Option1Happiness;
            }
            if (custom.Option1HappinessAll != 0)
            {
                result += $"{custom.Option1HappinessAll} happiness across the {empire.Name}, ";
                ChangeAllVillageHappiness(empire, custom.Option1HappinessAll);
            }
            if (custom.Option1Population != 0)
            {
                result += $"{custom.Option1Population} population for {village.Name}, ";
                if (custom.Option1Population > 0)
                    village.AddPopulation(custom.Option1Population);
                else
                {
                    village.SubtractPopulation(-custom.Option1Population);
                    village.SetPopulationToAtleastTwo();
                }
            }
            if (custom.Option1Relations != 0)
            {
                result += $"{otherEmpire.Name} opinion of you has changed by {custom.Option1Relations}, ";
                RelationsManager.ChangeRelations(empire, otherEmpire, custom.Option1Relations);
            }
            if (result.EndsWith(", "))
                result = result.TrimEnd(new char[] { ',', ' ' });
            result += ")";
            if (result == "()")
                result = "";
            State.GameManager.CreateMessageBox($"{custom.Option1Result} {result}");
        });
        UI.FirstChoice.interactable = firstGold;
        UI.SecondChoice.GetComponentInChildren<Text>().text = custom.Option2Choice;
        UI.SecondChoice.onClick.AddListener(() =>
        {
            string result = "(";
            if (custom.Option2Gold != 0)
            {
                result += $"{custom.Option2Gold} gold, ";
                if (custom.Option2Gold > 0)
                    empire.AddGold(custom.Option2Gold);
                else
                {
                    empire.SpendGold(-custom.Option2Gold);
                }
            }
            if (custom.Option2Happiness != 0)
            {
                result += $"{custom.Option2Happiness} happiness for {village.Name}, ";
                village.Happiness += custom.Option2Happiness;
            }
            if (custom.Option2HappinessAll != 0)
            {
                result += $"{custom.Option2HappinessAll} happiness across the {empire.Name}, ";
                ChangeAllVillageHappiness(empire, custom.Option2HappinessAll);
            }
            if (custom.Option2Population != 0)
            {
                result += $"{custom.Option2Population} population for {village.Name}, ";
                if (custom.Option2Population > 0)
                    village.AddPopulation(custom.Option2Population);
                else
                {
                    village.SubtractPopulation(-custom.Option2Population);
                    village.SetPopulationToAtleastTwo();
                }
            }
            if (custom.Option2Relations != 0)
            {
                result += $"{otherEmpire.Name} opinion of you has changed by {custom.Option2Relations}, ";
                RelationsManager.ChangeRelations(empire, otherEmpire, custom.Option2Relations);
            }
            if (result.EndsWith(", "))
                result = result.TrimEnd(new char[] { ',', ' ' });
            result += ")";
            if (result == "()")
                result = "";
            State.GameManager.CreateMessageBox($"{custom.Option2Result} {result}");
        });
        UI.SecondChoice.interactable = secondGold;
        UI.ThirdChoice.GetComponentInChildren<Text>().text = "";
        UI.ThirdChoice.interactable = false;
    }

    bool StartEvent(int num, Empire empire)
    {
        switch (num)
        {
            case 0:
                {
                    var villages = GetTwoRandomVillages(empire.Side);
                    if (villages == null || empire.Leader == null)
                        return false;
                    UI.MainText.text = $"Villagers from {villages[0].Name} have petitioned the government to intercede on their behalf. It would seem that a group from {villages[1].Name} have stolen a compy herd from their rightful owners. This indeed seems to be the case. What would you like to do?";
                    UI.MainText.text += AddVillageInfo(villages[0]);
                    UI.MainText.text += AddVillageInfo(villages[1]);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = "Offer compensation to the affected town. [200 Gold]";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"{villages[0].Name} +15 happiness, {villages[1].Name} +5 happiness,");
                        villages[0].Happiness += 15;
                        villages[1].Happiness += 5;
                        empire.SpendGold(200);
                    });
                    UI.FirstChoice.interactable = empire.Gold >= 200;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = "Force the other town to return the herd.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"{villages[0].Name} +20 happiness, {villages[1].Name} -20 happiness,");
                        villages[0].Happiness += 20;
                        villages[1].Happiness -= 20;
                    });
                    UI.SecondChoice.interactable = true;
                    if(empire.Leader.HasTrait(Traits.Prey))
                        UI.ThirdChoice.GetComponentInChildren<Text>().text = "Neither town had asked for permission to raise livestock in our domain. Our leader shall devour the herd. [Leader cannot vore]";
                    else
                        UI.ThirdChoice.GetComponentInChildren<Text>().text = "Neither town had asked for permission to raise livestock in our domain. Our leader shall devour the herd.";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"{villages[0].Name} -25 happiness, {villages[1].Name} -25 happiness, leader gains some exp, small chance of rebels");
                        villages[0].Happiness -= 25;
                        villages[1].Happiness -= 25;
                        GiveExp(empire.Leader, 75, .04f);
                        if (State.Rand.Next(100) < 5) CreateRebels(RebelDifficulty.Easy, villages[0]);
                        if (State.Rand.Next(100) < 5) CreateRebels(RebelDifficulty.Easy, villages[1]);
                    });
                    UI.ThirdChoice.interactable = !empire.Leader.HasTrait(Traits.Prey);
                }
                break;
            case 1:
                {
                    var village = GetRandomVillage(empire.Side);
                    if (village == null || empire.Leader == null || empire.CapitalCity == null)
                        return false;
                    UI.MainText.text = $"Our vassal governing the territory near {village.Name} has become increasingly aggressive towards the capital. They believe that their accomplishments in managing the region have gone unrewarded. What should be done?";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = "They’re right. Their contributions to our people have been ignored for too long. [300 Gold]";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"{village.Name} +15 happiness");
                        village.Happiness += 15;
                        empire.SpendGold(300);
                    });
                    UI.FirstChoice.interactable = empire.Gold >= 300;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = "Ridiculous. Their rights as a vassal are more than just compensation.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"Large chance of spawning rebels at {village.Name}");
                        if (State.Rand.Next(100) < 95) CreateRebels(RebelDifficulty.Hard, village);
                    });
                    UI.SecondChoice.interactable = true;
                    if(empire.Leader.HasTrait(Traits.Prey))
                        UI.ThirdChoice.GetComponentInChildren<Text>().text = "Excellent! Inform them to come to the capital immediately, for a grand feast {empire.Leader.Name}'s treat for thier \"accomplishments\"…";
                    else
                        UI.ThirdChoice.GetComponentInChildren<Text>().text = "Excellent! Inform them to come to the capital immediately, a new position has recently opened up. They need only pass an oral examination…";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        if(empire.Leader.HasTrait(Traits.Prey))
                        {
                                if (State.Rand.Next(100) < 90)
                            {
                                State.GameManager.CreateMessageBox($"{empire.Leader.Name} tolerates many things as a frontline soldier, but this vassal's ego will not be one of them. The moment the vassal entered the capital's grand dining room they were greeted by {empire.Leader.Name}... and the empire's inquisition. \n\nLeader gains exp");
                                GiveExp(empire.Leader, 75, .04f);
                            }
                            else
                            {
                                State.GameManager.CreateMessageBox($"The vassal figured out something was wrong and formed a rebel army outside the capital.");
                                CreateRebels(RebelDifficulty.Hard, empire.CapitalCity);
                            }
                        }
                        else
                        {
                            if (State.Rand.Next(100) < 90)
                            {
                                State.GameManager.CreateMessageBox($"The vassal was successfully eaten, the leader gains exp");
                                GiveExp(empire.Leader, 75, .04f);
                            }
                            else
                            {
                                State.GameManager.CreateMessageBox($"The vassal figured out something was wrong and formed a rebel army outside the capital.");
                                CreateRebels(RebelDifficulty.Hard, empire.CapitalCity);
                            }
                        }
                    });
                    UI.ThirdChoice.interactable = empire.CapitalCity != null && empire.CapitalCity.Side == empire.Side;
                }
                break;
            case 2:
                {
                    {
                        Empire otherEmpire = null;
                        Empire otherEmpire2 = null;
                        for (int i = 0; i < 10; i++)
                        {
                            otherEmpire = GetRandomEmpire(empire);
                            if (otherEmpire != null)
                            {
                                otherEmpire2 = GetRandomHostileEmpire(otherEmpire);
                                break;
                            }
                        }
                        if (empire.VillageCount < 3 || otherEmpire2 == null)
                            return false;

                        var villages = State.World.Villages.Where(s => s.Side == empire.Side && s.Race != otherEmpire.ReplacedRace).OrderBy(s => s.Population).ToArray();
                        UI.MainText.text = $"The war between {otherEmpire.Name} and {otherEmpire2.Name} has intensified considerably. As such a number of {otherEmpire.Name} refugees have found their way to our border. The infirm and young make up the majority of this group. There are far too many to integrate easily into our communities. They are begging our border guards for entry into our nation. What shall be done?";
                        UI.MainText.text += AddVillageInfo(villages[0]); UI.MainText.text += AddVillageInfo(villages[0]);
                        UI.FirstChoice.GetComponentInChildren<Text>().text = $"Though it will be difficult for us, we must accommodate those in need. Move the villagers from the town with lowest population to another town. We shall give the town to these people in need.";
                        UI.FirstChoice.onClick.AddListener(() =>
                        {
                            if (villages.Length < 2)
                            {
                                State.GameManager.CreateMessageBox($"The refugees mysteriously vanish (there were very few villages that weren't already the race of the refugees, so it cancels, instead of performing a nonsensical conversion on a village that was already that race)");
                                return;
                            }
                            ChangeAllVillageHappiness(empire, -15);
                            RelationsManager.ChangeRelations(empire, otherEmpire, 1.5f);

                            int pop = villages[0].Population;
                            for (int i = 0; i < villages.Length; i++)
                            {
                                int diff = villages[i].Maxpop - villages[i].Population;
                                if (diff > pop)
                                    diff = pop;
                                villages[i].AddPopulation(diff);
                                pop -= diff;
                                if (pop == 0)
                                    break;
                            }

                            villages[0].SubtractPopulation(99999);
                            villages[0].Race = otherEmpire.CapitalCity?.OriginalRace ?? otherEmpire.ReplacedRace;
                            villages[0].AddPopulation(3 * villages[0].Maxpop / 4);
                            villages[0].Happiness = 200;
                            State.GameManager.CreateMessageBox($"Though it will be difficult for us, we must accommodate those in need. Move the villagers from {villages[0].Name} to another town. We shall give the town to these people in need. (The town switches race, the refugees are very happy, but other towns happiness falls some)");

                        });
                        UI.FirstChoice.interactable = true;
                        UI.SecondChoice.GetComponentInChildren<Text>().text = $"This is not a concern of ours. Tell the refugees they must go elsewhere";
                        UI.SecondChoice.onClick.AddListener(() =>
                        {

                        });
                        UI.SecondChoice.interactable = true;
                        if (empire.Leader != null && empire.Leader.HasTrait(Traits.Prey))
                            UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Excellent! We already have a place set aside for them! [Leader cannot vore]";
                        else
                            UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Excellent! We already have a place set aside for them! [Leader Rubs Belly]";
                        UI.ThirdChoice.onClick.AddListener(() =>
                        {
                            GiveExp(empire.Leader, 80, .05f);
                            RelationsManager.ChangeRelations(empire, otherEmpire, -2);
                            State.GameManager.CreateMessageBox($"The {otherEmpire.Name} are outraged at your decision to eat all of the refugees! (Leader gains exp)");

                        });
                        UI.ThirdChoice.interactable = empire.Leader != null && !empire.Leader.HasTrait(Traits.Prey);
                    }
                }
                break;
            case 3:
                {
                    var village = GetRandomConqueredVillage(empire.Side, empire.ReplacedRace);
                    var selfVillages = State.World.Villages.Where(s => s.Side == empire.Side && s.Race == empire.ReplacedRace);
                    var occupiedVillages = State.World.Villages.Where(s => s.Side == empire.Side && s.Race != empire.ReplacedRace);
                    if (village == null)
                        return false;
                    UI.MainText.text = $"Non-{empire.ReplacedRace} Citizens within our borders have become discontent with the way they’ve been treated and are demanding equal liberties. {empire.ReplacedRace} across the empire are violently opposed to such action. What shall be done?";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = "All beneath our banner deserve equal respect. It shall be done.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"-40 happiness for all {empire.ReplacedRace} villages, +30 for all others");
                        foreach (Village vill in selfVillages)
                        {
                            vill.Happiness -= 40;
                        }
                        foreach (Village vill in occupiedVillages)
                        {
                            vill.Happiness += 30;
                        }
                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = "The way things are is the way things are meant to be. Nothing shall change.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"+10 happiness for all {empire.ReplacedRace} villages, -50 for all others");
                        foreach (Village vill in selfVillages)
                        {
                            vill.Happiness += 10;
                        }
                        foreach (Village vill in occupiedVillages)
                        {
                            vill.Happiness -= 50;
                        }
                    });
                    UI.SecondChoice.interactable = true;
                    if(!empire.CanVore)
                        UI.ThirdChoice.GetComponentInChildren<Text>().text = "If they are not comfortable within our borders, perhaps they shall be more so in our bellies! Issue an order to devour all malcontents throughout the nation.[Empire cannot vore]";
                    else
                        UI.ThirdChoice.GetComponentInChildren<Text>().text = "If they are not comfortable within our borders, perhaps they shall be more so in our bellies! Issue an order to devour all malcontents throughout the nation.";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"+15 happiness for all {empire.ReplacedRace} villages, Population of non {empire.ReplacedRace} cities is cut in half, chance of rebellions");
                        foreach (Village vill in selfVillages)
                        {
                            vill.Happiness += 15;
                        }
                        foreach (Village vill in occupiedVillages)
                        {
                            if (State.Rand.Next(3) == 0 && StrategicUtilities.ArmyAt(vill.Position) == null)
                            {
                                CreateRebels(RebelDifficulty.Medium, vill);
                                vill.ChangeOwner(700);
                            }
                            else vill.SubtractPopulation(vill.Population / 2);
                        }
                    });
                    UI.ThirdChoice.interactable = empire.CanVore;
                }
                break;
            case 4:
                {
                    var village = GetRandomVillage(empire.Side);
                    if (village == null || village.Garrison == 0)
                        return false;
                    if(!empire.CanVore)
                        UI.MainText.text = $"A riot has broken out in {village.Name} after one of our guards got drunk and got in a brawl injuring several citizens. They are forming a lynch mob at this very moment to take justice into their own hands. If we do nothing, this may spiral out of control.";
                    else
                        UI.MainText.text = $"A riot has broken out in {village.Name} after one of our guards devoured the daughter of a local cobbler. They are forming a lynch mob at this very moment to take justice into their own maws. If we do nothing, this may spiral out of control.";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = "Put the guard on trial, uncover the truth, and act accordingly.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(3) != 0)
                        {
                            State.GameManager.CreateMessageBox($"The guard was guilty and executed");
                            var garrison = village.PrepareAndReturnGarrison();
                            if (garrison.Count > 0)
                            {
                                village.GetRecruitables().Remove(garrison[State.Rand.Next(garrison.Count)]);
                                village.SubtractPopulation(1);
                            }
                        }
                        else
                        {
                            State.GameManager.CreateMessageBox($"The guard was found innocent, the riot was stopped but village happiness falls by 20");
                            village.Happiness -= 20;
                        }
                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = "Ignore the issue.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(5) != 0)
                        {
                            State.GameManager.CreateMessageBox($"The enraged citizens have killed the entire garrison, and are less happy");
                            var garrison = village.PrepareAndReturnGarrison();
                            foreach (var unit in garrison)
                            {
                                village.VillagePopulation.RemoveHireable(unit);
                                village.SubtractPopulation(1);
                            }
                            village.Happiness -= 30;
                        }
                        else
                        {
                            State.GameManager.CreateMessageBox($"The garrison has fought back and killed all of the villagers except themselves");
                            var garrison = village.PrepareAndReturnGarrison();
                            village.SubtractPopulation(village.Population - garrison.Count);
                        }

                    });
                    UI.SecondChoice.interactable = true;
                    if(!empire.CanVore)
                        UI.ThirdChoice.GetComponentInChildren<Text>().text = "How dare they raise their arms against our people! Stand with our soldiers!";
                    else
                        UI.ThirdChoice.GetComponentInChildren<Text>().text = "How dare they raise their arms against our people! Devour the lot of them!";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"-40 happiness for {village.Name}, Population quartered");
                        village.SubtractPopulation(village.Population * 3 / 4);
                        village.Happiness -= 40;
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 5:
                {
                    var village = GetRandomVillage(empire.Side);
                    if (village == null || empire.Leader == null)
                        return false;
                    UI.MainText.text = $"A number clergy in {village.Name} have expressed concern in the religious fervor of their congregation. They request additional funds to construct a new temple in order to raise their flock’s devotion. What shall be done?";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = "Of course, our religious leaders ask for little and we are glad to help. [500 gold]";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"+25 happiness for {village.Name}");
                        empire.SpendGold(500);
                        village.Happiness += 25;

                    });
                    UI.FirstChoice.interactable = empire.Gold >= 500;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = "Remind the priest that it is their sacred duty to adhere to the needs of the faithful. Not ours. ";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"-10 happiness for {village.Name}");
                        village.Happiness -= 10;
                    });
                    UI.SecondChoice.interactable = true;
                    if (empire.Leader.HasTrait(Traits.Prey))
                        UI.ThirdChoice.GetComponentInChildren<Text>().text = $"HERETICS? Within our own borders! Inform the priest that there shall be no need to construct a temple. The malcontents shall find the goddess soon enough! She is at the bottom of {empire.Leader.Name}'s gullet! [Leader cannot vore]";
                    else
                        UI.ThirdChoice.GetComponentInChildren<Text>().text = $"HERETICS? Within our own borders! Inform the priest that there shall be no need to construct a temple. The malcontents shall find the goddess soon enough! She is at the bottom of {empire.Leader.Name}'s gullet!";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"-10 villagers, -40 happiness for village, Hero gains exp");
                        village.SubtractPopulation(10);
                        village.SetPopulationToAtleastTwo();
                        village.Happiness -= 40;
                        GiveExp(empire.Leader, 40, .02f);
                    });
                    UI.ThirdChoice.interactable = !empire.Leader.HasTrait(Traits.Prey);
                }
                break;
            case 6:
                {
                    var hostileEmpire = GetRandomHostileEmpire(empire);
                    if (hostileEmpire == null || empire.Leader == null || Config.Diplomacy == false)
                        return false;
                    UI.MainText.text = $"An envoy from the {hostileEmpire.Name} has arrived. They have grown tired and it would seem that they desire peace between {empire.Name} and {hostileEmpire.Name}. While their people as a whole do not particularly care for peace it would seem that {empire.Leader.Name} has become an object of obsession for one of their royal family members. Their greatest desire is to be wed with our leader. What shall be done?";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"An alliance between {hostileEmpire.Name} and {empire.Name} would be of great benefit to all parties. Let the wedding commence immediately!";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"Alliance declared");
                        RelationsManager.SetAlly(empire, hostileEmpire);
                        RelationsManager.MakeLike(empire, hostileEmpire);


                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"We desire an alliance, but {empire.Leader.Name} does not desire to be wed at this time. Perhaps we could work something out?";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(5) == 0)
                        {
                            State.GameManager.CreateMessageBox($"An alliance was declared");
                            RelationsManager.SetAlly(empire, hostileEmpire);
                            RelationsManager.MakeLike(empire, hostileEmpire, .75f);
                        }
                        else
                        {
                            State.GameManager.CreateMessageBox($"You weren't able to work out a deal");
                        }
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Kill the envoy and send their remains back in whatever form that may take.";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"They are outraged at your actions, and the spurned lover may seek vengeance");
                        RelationsManager.ArmyAttacked(empire, hostileEmpire);
                        RelationsManager.ArmyAttacked(empire, hostileEmpire);
                        RelationsManager.VillageAttacked(empire, hostileEmpire);
                        var village = GetRandomVillage(hostileEmpire.Side);
                        if (village != null)
                        {
                            var unit = new Unit(hostileEmpire.Side, hostileEmpire.ReplacedRace, (int)empire.Leader.Experience, true);
                            unit.Name = "Spurned Lover";
                            village.VillagePopulation.AddHireable(unit);
                        }
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 7:
                {
                    var hostileEmpire = GetRandomHostileEmpire(empire);
                    if (hostileEmpire == null)
                        hostileEmpire = GetRandomEmpire(empire);
                    Village village;
                    if (empire.CapitalCity?.Side == empire.Side)
                        village = empire.CapitalCity;
                    else
                        village = GetRandomVillage(empire.Side);
                    if (hostileEmpire == null || empire.Leader == null || village == null)
                        return false;
                    int rand = State.Rand.Next(100);
                    UI.MainText.text = $"There is no doubt. Spies from {hostileEmpire.Name} have infiltrated {village.Name}. What shall be done?";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Confront the {hostileEmpire.Name}. Tell them such actions are unacceptable and that they must withdraw their spies immediately.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        if (rand < 20)
                        {
                            State.GameManager.CreateMessageBox($"The enemy spies have fled");
                        }
                        else
                        {
                            int gold = empire.Gold / 10;
                            State.GameManager.CreateMessageBox($"The enemy spies stole {gold} from you");
                        }

                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Find the spies. It can’t be terribly difficult to find a {hostileEmpire.ReplacedRace} in a cloak.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        if (rand < 40)
                        {
                            if(!empire.CanVore)
                                State.GameManager.CreateMessageBox($"The enemy spies have been dealt with");
                            else
                                State.GameManager.CreateMessageBox($"The enemy spies have been eaten");
                            GiveExp(empire.Leader, 50, .03f);
                        }
                        else
                        {
                            int gold = empire.Gold / 10;
                            State.GameManager.CreateMessageBox($"The enemy spies stole {gold} from you");
                        }
                    });
                    UI.SecondChoice.interactable = true;
                    if(empire.Leader.HasTrait(Traits.Prey))
                        UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Let {empire.Leader.Name} and the inquisition deal the spies. Our ranks cannot afford to be compromised the officials shall be... questioned.";
                    else
                        UI.ThirdChoice.GetComponentInChildren<Text>().text = $"There is only one true determiner of innocence. The belly. Consume all high ranking officials. Only the guilty shall fully digest. ";
                   UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        if (rand < 90)
                        {
                            State.GameManager.CreateMessageBox($"Population decreased by 10, but the spies were taken care of, and the leader gains exp");
                            village.SubtractPopulation(10);
                            village.SetPopulationToAtleastTwo();
                            GiveExp(empire.Leader, 50, .03f);
                        }
                        else
                        {
                            int gold = empire.Gold / 10;
                            village.SubtractPopulation(10);
                            village.SetPopulationToAtleastTwo();
                            State.GameManager.CreateMessageBox($"Population decreased by 10, but the spies still stole {gold} from you");

                        }
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;

            case 8:
                {
                    Empire otherEmpire = GetRandomAlliedEmpire(empire);
                    if (otherEmpire == null)
                        return false;
                    UI.MainText.text = $"Our allies, the {otherEmpire.Name} have become upset at our recent actions. It seems their leader’s daughter believes our friendship to be naught but a lie, being used to get closer to their people before we inevitably attempt to gobble up both their territory and people. We have sent envoys to attempt to remedy the situation. What should they do?";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Apologize for any possible slights and offer a gift [200 Gold]";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        empire.SpendGold(200);
                        if (State.Rand.Next(10) == 0)
                        {
                            State.GameManager.CreateMessageBox($"They still don't quite trust you, relations fall slightly");
                            RelationsManager.ArmyAttacked(empire, otherEmpire);
                        }
                        else
                        {
                            State.GameManager.CreateMessageBox($"Relations have returned to normal");
                        }

                    });
                    UI.FirstChoice.interactable = empire.Gold >= 200;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Ensure them that our people have no such plans and that the accusations are foolish.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(10) == 0)
                        {
                            State.GameManager.CreateMessageBox($"They don't trust your empty words, relations fall moderately");
                            RelationsManager.VillageAttacked(empire, otherEmpire);
                            RelationsManager.ArmyAttacked(empire, otherEmpire);
                        }
                        else
                        {
                            State.GameManager.CreateMessageBox($"They believe you and relations return to normal");
                        }
                    });
                    UI.SecondChoice.interactable = true;
                    if (!empire.CanVore)
                        UI.ThirdChoice.GetComponentInChildren<Text>().text = $"The “Envoys” are in actuality assassins. Curse them that nosey girl for discovering our ruse. Silence her before she can reveal our secrets.";
                    else
                        UI.ThirdChoice.GetComponentInChildren<Text>().text = $"The “Envoys” are in actuality assassins. Curse them that nosey girl for discovering our ruse. Devour her before she can reveal our secrets.";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(2) == 0)
                        {
                            State.GameManager.CreateMessageBox($"You weren't able to silence her in time, now that they know the truth they declared war");
                            RelationsManager.VillageAttacked(empire, otherEmpire);
                            RelationsManager.ArmyAttacked(empire, otherEmpire);
                        }
                        else
                        {
                            if (!empire.CanVore)
                            State.GameManager.CreateMessageBox($"She was dealt with and relations stay the same");
                            else
                            State.GameManager.CreateMessageBox($"She was eaten and relations stay the same");
                        }
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;

            case 9:
                {
                    Village village = GetRandomVillage(empire.Side);
                    if (village == null)
                        return false;
                    UI.MainText.text = $"A villager from {village.Name} has apparently been eaten by a local wyvern. A fairly common occurrence in that region as this wyvern has been there for quite some time. The town militia was able to capture the wyvern in its engorged state and had plans of turning it into a war beast. However, the villagers report that the villager is still very much alive within the wyvern’s stomach despite being in there for several days! It seems both the villager and wyvern have interesting properties. What should be done?";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Cut the wyvern open immediately and save the villager! Such endurance should serve us well.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        Unit unit = new Unit(empire.Side, village.Race, village.GetStartingXp(), State.World.GetEmpireOfRace(village.Race)?.CanVore ?? true);
                        unit.ModifyStat(Stat.Endurance, 30);
                        unit.AddPermanentTrait(Traits.AcidImmunity);
                        village.VillagePopulation.AddHireable(unit);
                        State.GameManager.CreateMessageBox($"{unit.Name} is available for recruitment at {village.Name}, featuring strong endurance and acid immunity");


                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Attempting to save the villager is too risky, as is attempting to tame such a dangerous beast. Release the creature and forget the villager.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {

                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Fantastic news! Such a long-lived creature will be of great use. Utilize the extra time gained by its stubborn meal to train the beast to fight for us.";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        Unit unit = new Unit(empire.Side, Race.Wyvern, village.GetStartingXp(), true);
                        unit.AddPermanentTrait(Traits.IronGut);
                        unit.AddPermanentTrait(Traits.Intimidating);
                        village.VillagePopulation.AddHireable(unit);
                        State.GameManager.CreateMessageBox($"{unit.Name} the wyvern is available for recruitment at {village.Name}");
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 10:
                {
                    Village village = GetRandomVillage(empire.Side);
                    if (village == null || empire.CanVore)
                        return false;
                    UI.MainText.text = $"News from the province of {village.Name}. It would seem that a traveling \"hero\" known as has devoured the local noble managing the area. The hero attests that the vassal was a petty tyrant whom tormented the local populace. The noble, over the sounds of their own digestion, vehemently denied these accusations with a wobble. What shall be done?";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"The hero did the right thing. Rumors of the vassal’s corruption have long circulated. Applaud the hero and have them join the army.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        var race = GetRandomEmpire(empire).ReplacedRace;
                        Unit unit = new Unit(empire.Side, race, village.GetStartingXp(), State.World.GetEmpireOfRace(race)?.CanVore ?? true);
                        unit.DigestedUnits = 1;
                        unit.Name = $"Hero of {village.Name}";
                        village.VillagePopulation.AddHireable(unit);
                        State.GameManager.CreateMessageBox($"The {unit.Name} is available for recruitment at {village.Name}, +25 village happiness for {village.Name}");
                        village.Happiness += 25;



                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"This is a clear violation of the law. However, the opportunity presented in getting rid of a corrupt vassal is too good to pass up. Arrest the hero, but allow them to keep their meal.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"+5 village happiness for {village.Name}");
                        village.Happiness += 5;
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"We are well aware of that vassal’s corruption. It’s why we put her there. Rescue the noble and have them deal with the hero in front of the entire village. ";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        var race = GetRandomEmpire(empire).ReplacedRace;
                        Unit unit = new Unit(empire.Side, race, village.GetStartingXp(), State.World.GetEmpireOfRace(race)?.CanVore ?? true);
                        unit.DigestedUnits = 1;
                        unit.Name = $"Tyrant of {village.Name}";
                        State.GameManager.CreateMessageBox($"The {unit.Name} is available for recruitment at {village.Name}, -10 village happiness for {village.Name}");
                        village.Happiness -= 10;
                        village.VillagePopulation.AddHireable(unit);
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 11:
                {
                    Village village = GetRandomVillage(empire.Side);
                    var race = GetRandomEmpire(empire).ReplacedRace;
                    if (village == null || empire.CapitalCity == null || empire.CapitalCity.Side != empire.Side || empire.CanVore)
                        return false;
                    UI.MainText.text = $"A {race} bandit has taken up residence in {village.Name}. They have been devouring the most beautiful women throughout town. What shall be done?";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"We must obviously save the villagers. Send in the guard and deal with this bandit. Save as many villagers as we can!";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"+10 village happiness, +5 all village happiness, -3 villagers");
                        village.Happiness += 10;
                        ChangeAllVillageHappiness(empire, 5);
                        village.SubtractPopulation(3);
                        village.SetPopulationToAtleastTwo();

                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Such a go getter attitude needs to be cultivated, not punished. Invite the bandit to the capital, provide a cart heavy enough to carry her bulging gut.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {

                        Unit unit = new Unit(empire.Side, race, village.GetStartingXp(), State.World.GetEmpireOfRace(race)?.CanVore ?? true);
                        unit.DigestedUnits = State.Rand.Next(10, 15);
                        unit.AddPermanentTrait(Traits.EnthrallingDepths);
                        village.Happiness -= 10;
                        empire.CapitalCity.VillagePopulation.AddHireable(unit);
                        ChangeAllVillageHappiness(empire, -5);
                        village.SubtractPopulation(10);
                        village.SetPopulationToAtleastTwo();
                        State.GameManager.CreateMessageBox($"-10 village happiness, -5 all village happiness, -10 villagers, {unit.Name} available for recruitment at {empire.CapitalCity.Name}");
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                    });
                    UI.ThirdChoice.interactable = false;
                }
                break;
            case 12:
                {
                    Village village = GetRandomVillage(empire.Side);
                    Empire Wolves = State.World.GetEmpireOfRace(Race.Wolves);
                    Empire Bunnies = State.World.GetEmpireOfRace(Race.Bunnies);
                    if (village == null || Wolves.VillageCount == 0 || Bunnies.VillageCount == 0)
                        return false;
                    UI.MainText.text = $"Three bunny girls have settled near {village.Name}. Allegedly they have built three houses made of various materials. They have petitioned for aid as a traveling wolf merchant has apparently been bothering the girls with constant soliciting efforts. What shall be done?";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"How terrible! The state must put an end to unwanted solicitation! Inform the wolf merchant to leave immediately and tell the girls their traumatic experience is over.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"+10 village happiness, Bunnies like you more, wolves like you less");
                        village.Happiness += 10;
                        RelationsManager.ChangeRelations(empire, Bunnies, .4f);
                        RelationsManager.ChangeRelations(empire, Wolves, -.4f);
                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Inform the bunnies that they have settled without the proper registration permits. Have them pay a settling fee and force the merchant to purchase a trading permit.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"+400 gold, bunnies like you slightly less");
                        empire.AddGold(400);
                        RelationsManager.ChangeRelations(empire, Bunnies, -.3f);
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"How dare these insolent bunny girls! They make life difficult for a hard working carnivore. Console the poor merchant by tearing down those terrible houses and stuffing the bunny girls into the wolf girl’s gullet. ";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"bunnies like you significantly less, wolves like you more");
                        RelationsManager.ChangeRelations(empire, Bunnies, -.8f);
                        RelationsManager.ChangeRelations(empire, Wolves, .6f);
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;

            case 13:
                {
                    Village village = GetRandomVillage(empire.Side);
                    if (village == null)
                        return false;
                    UI.MainText.text = $"One of our scouts has discovered a strange structure. Apparently it is a huge maze that is home to a tribe of Taurus. Legend has it that a great warrior is within this tribe. What shall be done?";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Explore the structure and appeal to the Taurus within to join us. Despite the risk to the scout, it’s more than worth it.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(3) == 0)
                        {
                            Unit unit = new Unit(empire.Side, Race.Taurus, 20 + (int)(1.2f * State.GameManager.StrategyMode.ScaledExp), true);
                            unit.AddPermanentTrait(Traits.Large);
                            village.VillagePopulation.AddHireable(unit);
                            State.GameManager.CreateMessageBox($"The scout was able to convince the unit to join us, it is waiting at {village.Name}");
                        }
                        else
                        {
                            State.GameManager.CreateMessageBox($"The unit never returned");
                        }
                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Such an exploration is not worth our time.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {

                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Send the warrior into the labyrinth. She is to find the Taurus and put an end to their foul machinations!";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(3) == 0)
                        {
                            Unit unit = new Unit(empire.Side, empire.ReplacedRace, 20 + (int)(1.2f * State.GameManager.StrategyMode.ScaledExp), State.World.GetEmpireOfRace(empire.Race)?.CanVore ?? true);
                            unit.AddPermanentTrait(Traits.AcidResistant);
                            village.VillagePopulation.AddHireable(unit);
                            State.GameManager.CreateMessageBox($"The scout emerged from the maze with tons of experience, and is waiting at {village.Name}");
                        }
                        else
                        {
                            State.GameManager.CreateMessageBox($"The unit never returned");
                        }
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;

            case 14:
                {
                    Village village = empire.CapitalCity;
                    var villages = State.World.Villages.Where(s => s.Side == empire.Side);
                    if (village == null || village.Side != empire.Side || empire.Leader == null)
                        return false;
                    UI.MainText.text = $"A musician has come to {village.Name}. Their songs are horrendously lewd and bawdy. Most of them center on objectifying and complementing {empire.Leader.Name} body. The city’s aristocracy finds the songs distasteful and want the bard to leave. What shall be done?";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Remove the bard from {village.Name}. There is no need to listen to such uncultured music.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"Capital +25 happiness");
                        village.Happiness += 25;
                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"There is no need to concern ourselves with this situation. Charge the bard a fee for their poor choice of lyrics.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"Gained 100 gold");
                        empire.AddGold(100);
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Distasteful? Have the aristocrats insulting the bard’s music fed to {empire.Leader.Name}. It’s about time people started appreciating just how beautiful {empire.Leader.Name} is! Spread the songs throughout the nation!";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        GiveExp(empire.Leader, 75, .04f);
                        State.GameManager.CreateMessageBox($"Capital -20 happiness, all villages -5 happiness, leader gains exp ");
                        village.Happiness -= 25;
                        foreach (Village vill in villages)
                        {
                            vill.Happiness -= 5;
                        }
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 15:
                {
                    Village village = GetRandomVillage(empire.Side);
                    if (village == null)
                        return false;
                    UI.MainText.text = $"Word has come in from {village.Name}. It would appear that an individual Tiger has moved into the nearby wilderness. The villagers are worried and already has several missing person reports. What shall be done?";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Let’s not jump to conclusions. Perhaps this is all a misunderstanding. Surely predator and prey can live alongside one another!";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {


                        if (State.Rand.Next(8) == 0)
                        {
                            State.GameManager.CreateMessageBox($"{village.Name} - 10 happiness");
                            village.Happiness -= 10;
                        }
                        else
                        {
                            State.GameManager.CreateMessageBox($"{village.Name} - 15 happiness, 4 villagers die");
                            village.Happiness -= 15;
                            village.SubtractPopulation(4);
                            village.SetPopulationToAtleastTwo();
                        }

                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"We aren’t savages, but neither are we fools. Demand the predator register with the local authorities or leave the area.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(3) == 0)
                        {
                            Unit unit = new Unit(empire.Side, Race.Tigers, village.GetStartingXp(), true);
                            unit.DigestedUnits = State.Rand.Next(5, 15);
                            village.VillagePopulation.AddHireable(unit);
                            State.GameManager.CreateMessageBox($"The Tiger is available for hire at {village.Name}");
                        }
                        else
                        {
                            State.GameManager.CreateMessageBox($"Things have quieted down at {village.Name}");
                        }
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Suffer not the predator to live. Find the monster’s dwelling and burn it to the ground.";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"Village +10 happiness");
                        village.Happiness += 10;
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;

            case 16:
                {
                    Empire otherEmpire = GetRandomEmpire(empire);
                    if (otherEmpire == null || empire.Leader == null || empire.Leader.HasTrait(Traits.Prey) || Config.Diplomacy == false)
                        return false;
                    UI.MainText.text = $"A recent diplomatic event between our nation and the {otherEmpire.Name} has been going very well! It seems that good relations are likely! Well, were likely. One thing apparently led to another and the chief {otherEmpire.ReplacedRace} envoy is now sitting at the bottom of {empire.Leader.Name}'s gut! What shall be done!";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"It was an accident! They’ve already gone soft so we must apologize profusely and hope they understand the situation!";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {

                        if (State.Rand.Next(2) == 0)
                        {
                            State.GameManager.CreateMessageBox($"They are understanding and you suffer a minor diplomatic hit");
                            RelationsManager.ChangeRelations(empire, otherEmpire, -.2f);

                        }
                        else
                        {
                            State.GameManager.CreateMessageBox($"They are angry, diplomatic relations have fallen significantly");
                            RelationsManager.ChangeRelations(empire, otherEmpire, -.8f);
                        }

                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Cover up the embarrassing event. Hide our gluttonous leader from the delegation until they can fully digest their meal.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(2) == 0)
                        {
                            State.GameManager.CreateMessageBox($"The ruse goes undiscovered, relations are unaffected");

                        }
                        else
                        {
                            State.GameManager.CreateMessageBox($"They are angry at the loss and the lying and armies are moving our way");
                            RelationsManager.MakeHate(empire, otherEmpire);
                        }
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"By the Goddess! These people are DELICIOUS! How could we not see this earlier? Devour the rest of the delegation and prepare the army for vore! I mean war! Both? Both.";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"Obviously, this means war");
                        RelationsManager.MakeHate(empire, otherEmpire);
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 17:
                {
                    Empire otherEmpire = null;
                    Empire otherEmpire2 = null;
                    for (int i = 0; i < 10; i++)
                    {
                        otherEmpire = GetRandomEmpire(empire);
                        if (otherEmpire != null)
                        {
                            otherEmpire2 = GetRandomHostileEmpire(otherEmpire);
                            break;
                        }
                    }
                    if (otherEmpire2 == empire)
                        return false;
                    if (otherEmpire2 == null || empire.Leader == null || Config.Diplomacy == false || Config.LockedAIRelations)
                        return false;
                    UI.MainText.text = $"We have received a request from the {otherEmpire.Name}. It seems that they have been unable to deal the deathblow to their hated rivals, the {otherEmpire2.Name}. They desire our aid and wish for us to kill their leader for them. Funnily enough, the {otherEmpire2.Name} has sent us a similar request. What shall be done?";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Side with the {otherEmpire.Name}";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"You are now at war with the {otherEmpire2.Name} and allied with the {otherEmpire.Name}");
                        RelationsManager.MakeHate(empire, otherEmpire2);
                        RelationsManager.MakeLike(empire, otherEmpire);

                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Side with the {otherEmpire2.Name}";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"You are now at war with the {otherEmpire.Name} and allied with the {otherEmpire2.Name}");
                        RelationsManager.MakeLike(empire, otherEmpire2);
                        RelationsManager.MakeHate(empire, otherEmpire);
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Refuse to interfere";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"There is no change in your relations with either empire");
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 18:
                {
                    Village village = GetRandomVillage(empire.Side);

                    if (village == null)
                        return false;
                    UI.MainText.text = $"A group of our warriors were passing through {village.Name} when a local woman asked our troops to find a thief that’s been stealing animals from her corral. Her prized stud has gone missing only recently so she knows the thief is nearby. Our troops agree and search the nearby woods. In no time they begin to hear distressed mewling following the source, they eventually find a young woman, her gut finishing off the prized stud just as our troops arrive. Once apprehended, the young woman explains she's been starving out in the woods. What should be done?";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"She may be inexperienced but the fact that she was able to devour a beast whole means they’ll become a fine warrior. Pay the cattle owner for damages and employ the thief as a warrior.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        Unit unit = new Unit(empire.Side, Race.Foxes, village.GetStartingXp(), true, immuneToDefectons: true)
                        {
                            DickSize = -1,
                            BreastSize = 0,
                            HasVagina = true,
                            Name = "Isabella"
                        };
                        unit.ModifyStat(Stat.Stomach, 20);
                        unit.AddPermanentTrait(Traits.Ravenous);
                        village.VillagePopulation.AddHireable(unit);
                        State.GameManager.CreateMessageBox($"{unit.Name} is greatly appreciative for our leniency, she swears to serve our empire until her dying breath.");

                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Reprimand the troops for interfering in local affairs. This is an issue for the local magistrates and nobility. Hand the girl over to them.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"After our soldiers hand over the girl the town authorities bumble the situation up and allow her to escape. The cattle thief will likely return to her thieving ways shortly. (-10 village happiness)");
                        village.Happiness -= 10;
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"There is only one punishment for cattle rustling. Return to the corral owner and offer the thief to her.";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        if (!empire.CanVore)
                            State.GameManager.CreateMessageBox($"The coral owner is surprisingly understanding of the young girl's situation and puts her to work on the farm, both as punishment but also a way she could EARN her food and not steal it. Within a week we receive a letter along with some gold reporting that the young girl is enjoying working on the farm and is not only earning her wages but actively helping the farm thrive. The gold is apparently the bonus income that she's helped raise since working there. (100 gold received) ");
                        else
                            State.GameManager.CreateMessageBox($"The coral owner pats her engorged tummy as it digests the young girl. “I appreciate you finding this scrumptious morsel, my herd will be a lot safer now. That she’s, *Urrrp*” the coral owner sooths her struggling tummy, “gurgling away. Here’s some money I saved up. Consider it a payment for the meal!” (100 gold received) ");
                        empire.AddGold(100);
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 19:
                {
                    Empire otherEmpire = GetRandomEmpire(empire);

                    if (otherEmpire == null || !otherEmpire.CanVore || !empire.CanVore)
                        return false;
                    UI.MainText.text = $"The meeting room is silent aside from a loud sloshing as the {otherEmpire.ReplacedRace} envoy waddles into the court with their entourage. Their stomach is ludicrously swollen and full of various court servants. In an impressive power play, the envoy plans to negotiate with {empire.ReplacedRace} meat surging through their intestines. The statement is clear. What shall be done? ";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Demand that the envoy let their prey go. We shall not suffer our people to end up as pudge. ";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(3) == 0)
                        {
                            RelationsManager.ChangeRelations(empire, otherEmpire, 2);
                            State.GameManager.CreateMessageBox($"The envoy lets out a sigh of relief, “Thank goodness, this act was giving me a horrid case of indigestion! It is good to see that you care for the lives of your people now that we understand this we may speak further.” The envoy quickly expunges all the court servants and talks go off well!");
                        }
                        else
                        {
                            RelationsManager.ChangeRelations(empire, otherEmpire, -2);
                            RelationsManager.ChangeRelations(otherEmpire, empire, -4);
                            State.GameManager.CreateMessageBox($"The envoy yawns and idly squeezes their squirming tummy as it digests. “The prey filling my gut is just that, PREY. They have no chance of escaping my belly. Asides, this is the natural state of your kind. A meal for us!”");
                        }


                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"The negotiations must continue. The lives of some meagre servants is nothing compared to the safety of our nation.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"Our negotiators are distracted by the screams for help as the {otherEmpire.ReplacedRace} envoy’s belly melts them away. By the end of the talks it is clear they’ve had the better of us and the rival envoy’s outfit can barely contain their new layers of fat.");
                        RelationsManager.ChangeRelations(empire, otherEmpire, 1);
                        RelationsManager.ChangeRelations(otherEmpire, empire, -1);
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Our people are not weak willed. Before their envoy can even sit down our lead negotiator pounces their envoy and quickly swallows them whole.";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(5) == 0)
                        {
                            RelationsManager.ChangeRelations(empire, otherEmpire, 2);
                            RelationsManager.ChangeRelations(otherEmpire, empire, 4);
                            State.GameManager.CreateMessageBox($"The lead negotiator is met with applause from the {otherEmpire.ReplacedRace} entourage as the envoy, massive gut and all, slides down their throat. Once the disrespectful diplomat rests squarely in the pit of our negotiator’s tummy the entourage is impressed by the display and are glad to be dealing with fellow predators. Their party returns home one envoy short but with a great respect for our people.");
                        }
                        else
                        {
                            RelationsManager.ChangeRelations(empire, otherEmpire, -5);
                            RelationsManager.ChangeRelations(otherEmpire, empire, 1);
                            State.GameManager.CreateMessageBox($"The envoy’s entourage attempts to save their leader but quickly find themselves in tummy prisons of their own as our guards step in. The envoy’s party is digested in short order and their empire denounces us for eating their diplomats. While the encounter may have experienced their relations with us, our pudgy negotiators report that {otherEmpire.ReplacedRace} are even more delicious than previously thought!");
                        }
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 20:
                if (Config.MaleFraction < .5f)
                {
                    Village village = empire.CapitalCity;
                    if (village == null || village.Side != empire.Side || !empire.CanVore)
                        return false;
                    UI.MainText.text = $"An eccentric noble woman renowned for her beauty and wealth has opened up a grand tournament to select her next mate as the last one mysteriously became a layer of fat on her bosom. As such she has no desire for another weak partner and in order to gain a more stable relationship has spread word of this event across the empire. Single men and women across our nation rally to partake. How does the event pan out?";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"The event goes off without a hitch.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        ChangeAllVillageHappiness(empire, 10);
                        State.GameManager.CreateMessageBox($"The noble lady finds a mate strong enough to survive her gluttonous desires and the empire celebrates the union. Many expect an heir both strong and voracious to come from the union.");

                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"None of the participants are suitable partners but the revenue from the event fills our coffers.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"The participants that partake are either too meek or simply not beautiful enough to match the noble lady’s desires. As such none win her hand. However, the trade and fees generated from the event benefit the empire’s treasury. ");
                        empire.AddGold(500);
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"It was all a delicious ruse!";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"As the tournament proceeded each defeated opponent would mysteriously vanish and the noble lady’s waistline miraculously expand. The winner, bloody and beaten by the contest’s end stood no chance against the voracious noble and soon joined the other contestants in the pit of her stomach! The clever noble has arrived in the capital and has offered her services to our army in an attempt to sate her ravenous hunger!");
                        Unit unit = new Unit(empire.Side, village.Race, village.GetStartingXp(), State.World.GetEmpireOfRace(village.Race)?.CanVore ?? true);
                        unit.SetGenderRandomizeName(empire.ReplacedRace, Gender.Female);
                        unit.AddPermanentTrait(Traits.Clever);
                        village.VillagePopulation.AddHireable(unit);
                    });
                    UI.ThirdChoice.interactable = true;
                }
                else
                {
                    Village village = empire.CapitalCity;
                    if (village == null || village.Side != empire.Side || !empire.CanVore)
                        return false;
                    UI.MainText.text = $"An eccentric noble man renowned for his beauty and wealth has opened up a grand tournament to select his next mate as the last one mysteriously became a layer of fat on his ass. As such he has no desire for another weak partner and in order to gain a more stable relationship has spread word of this event across the empire. Single men and women across our nation rally to partake. How does the event pan out?";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"The event goes off without a hitch.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        ChangeAllVillageHappiness(empire, 5);
                        State.GameManager.CreateMessageBox($"The noble man finds a mate strong enough to survive his gluttonous desires and the empire celebrates the union. Many expect an heir both strong and voracious to come from the union.");

                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"None of the participants are suitable partners but the revenue from the event fills our coffers.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"The participants that partake are either too meek or simply not beautiful enough to match the noble man’s desires. As such none win his hand. However, the trade and fees generated from the event benefit the empire’s treasury. ");
                        empire.AddGold(500);
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"It was all a delicious ruse!";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"As the tournament proceeded each defeated opponent would mysteriously vanish and the noble man’s waistline miraculously expand. The winner, bloody and beaten by the contest’s end stood no chance against the voracious noble and soon joined the other contestants in the pit of his stomach! The clever noble has arrived in the capital and has offered his services to our army in an attempt to sate his ravenous hunger!");
                        Unit unit = new Unit(empire.Side, village.Race, village.GetStartingXp(), State.World.GetEmpireOfRace(village.Race)?.CanVore ?? true);
                        unit.SetGenderRandomizeName(empire.ReplacedRace, Gender.Male);
                        unit.AddPermanentTrait(Traits.Clever);
                        village.VillagePopulation.AddHireable(unit);
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;

            case 21:
                {
                    Empire otherEmpire = GetRandomEmpire(empire);
                    Village village = GetRandomVillage(empire.Side);
                    if (otherEmpire == null || village == null || !otherEmpire.CanVore)
                        return false;
                    UI.MainText.text = $"Our border guards have come across a pudgy assailant attempting to enter our empire. After arresting them they share their story. It seems they’re from {otherEmpire.Name} lands and that they’re being hunted down by a noble family there. It would seem they, in a fit of anger and hunger, swallowed and digested the noble family’s matriarch for marrying and then eating their brother. The assailant asks for asylum in our country. What should be done?";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Offer asylum. Corrupt nobles cannot be allowed to do as they please, no matter the country.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"The asylum seeker, grateful for our protection, has decided to take up arms in our name. However, the {otherEmpire.Name} noble house has sworn to take revenge for this slight and have used their influence to sway their government against us.");
                        RelationsManager.ChangeRelations(empire, otherEmpire, -1.75f);
                        Unit unit = new Unit(empire.Side, otherEmpire.ReplacedRace, village.GetStartingXp(), otherEmpire.CanVore);
                        unit.AddPermanentTrait(Traits.StrongGullet);
                        village.VillagePopulation.AddHireable(unit);

                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Deny entry. This matter is not our concern and we won’t involve ourselves.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"The asylum seeker flees our border in hopes of avoiding those hunting them and starts heading towards another nation.");
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Arrest the asylum seeker and hand them over to the noble house.";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"It doesn’t take long before the noble house’s search parties arrive. They thank our guards for the apprehension of this criminal and a noble from the house devours them in short order. They waddle back into their country and inform their government of our assistance in this matter, painting us in a positive light.");
                        RelationsManager.ChangeRelations(empire, otherEmpire, 1.25f);
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 22:
                {
                    Empire otherEmpire = GetRandomEmpire(empire);
                    if (otherEmpire == null || !otherEmpire.CanVore || !empire.CanVore)
                        return false;
                    UI.MainText.text = $"Our most recent diplomatic endeavor with the {otherEmpire.Name} has become strained with the most recent talk ending with a bulging gut and digesting ambassador. Escalation of tensions seemed inevitable. However, in a surprise move, the lead negotiator of the opposing nation has sent their mother to our camp. The middle aged woman is extravagant to a fault and her attitude is overbearing. Her motherly figure clad in gaudy trinkets and overly-tight clothes. We are uncertain how to deal with this strange political maneuver. What shall be done?";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"We shall treat her as we would our own mother. Lavish the woman in compliments and gifts.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(3) != 0)
                        {
                            RelationsManager.ChangeRelations(empire, otherEmpire, 4);
                            State.GameManager.CreateMessageBox($"The older woman basks in our elevated attention to her needs. Our diplomat in charge of dealing with her even reports that they attended to her more “delicate needs” throughout her stay with us. \nWhile their head negotiator is furious at us for quite literally fucking their mother, the bombastic woman herself has given a glowing, albeit bawdy, endorsement of our people. ");
                        }
                        else
                        {
                            RelationsManager.ChangeRelations(empire, otherEmpire, -.5f);
                            RelationsManager.ChangeRelations(otherEmpire, empire, -.5f);
                            State.GameManager.CreateMessageBox($"After a few days of pampering the maternal envoy she begins to idly muse that she never wanted to leave. This was thought to be a simple complement to our hospitality at first however it has become clear that the woman very much means what she’s been saying and intends to join our nation thus forsaking her country and her children.  This has slightly damaged relations with the {otherEmpire.Name}.");
                        }

                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Tolerate this strange maneuver. Treat the woman as we would any other envoy.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(5) != 0)
                        {
                            RelationsManager.ChangeRelations(empire, otherEmpire, -2);
                            RelationsManager.ChangeRelations(otherEmpire, empire, -4);
                            State.GameManager.CreateMessageBox($"It does not take long before the motherly envoy devours her way through much of our larders. The woman’s frame balloons to ridiculous portions during her stay, some servants even go missing and many diplomats swear to hear muffled screams beneath her hefty fat folds.  After her stay she begins spreading rumors that while our people are certainly boring our taste is simply divine. ");
                        }
                        else
                        {
                            RelationsManager.ChangeRelations(empire, otherEmpire, 3);
                            RelationsManager.ChangeRelations(otherEmpire, empire, -2);
                            int gold = empire.Gold / 5;
                            State.GameManager.CreateMessageBox($"As she stays within our royal suite, the woman’s ensemble becomes more and more extravagant and priceless jewels begin to disappear. This however is nothing compared to the cost of her bottomless diet. It would seem to maintain her bodacious, motherly curves the woman quite literally eats her own weight in food each day. \nThe entire affair seems less like a diplomatic mission and more like a well - planned burglary.While this may have been disastrous to our finances, the {otherEmpire.Name} at least seem pleased. ");
                        }
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"This is simply too much to bear. Not only have they insulted us by sending an untrained envoy, the woman is grating and far too annoying. Have her brought before the senior most official and devour her!";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"Between her flabby curves and multitude of trinkets, swallowing the rude guest proves difficult for our official. However with the help of their fellow diplomats the massive ass is pushed past the lips.  \nOur senior official, awaiting a furious response from the {otherEmpire.Name} party on account of the diplomatic incident of a meal, finds with surprise that the other group is not entirely put out by the affair.In fact, a sizable payment of gratitude arrives from the digesting mother’s offspring. It seems she was not a very good mother. While relations are damaged somewhat it certainly could have been worse.");
                        empire.AddGold(1000);
                        RelationsManager.ChangeRelations(empire, otherEmpire, -.45f);
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 23:
                {
                    Empire otherEmpire = GetRandomEmpire(empire);
                    if (otherEmpire == null || !otherEmpire.CanVore || !empire.CanVore)
                        return false;
                    UI.MainText.text = $"One of our top infiltrators have been able to ingratiate themselves with the {otherEmpire.Name} elites. Through this we’ve been made aware that a local magistrate that’s staying in the capital has been embezzling state finances and keeping this ill-begotten fortune spread throughout their rural fief. This corrupt official assuredly knows where the treasure is but in order to interrogate them we must first smuggle them out of the capital. What shall our infiltrator do?";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Reveal themselves to the local peacekeepers and make them aware of the situation we’ve uncovered.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(3) != 0)
                        {
                            empire.AddGold(1000);
                            otherEmpire.AddGold(1000);
                            RelationsManager.ChangeRelations(empire, otherEmpire, 5);
                            RelationsManager.ChangeRelations(otherEmpire, empire, 5);
                            State.GameManager.CreateMessageBox($"While initially concerned with the aspect of a foreign agent infiltrating their city, {otherEmpire.Name} appreciates the gesture of coming to them with the information damming the corrupt magistrate. It doesn’t take long before torture unveils the location of the hidden funds. While the locals take most of their stolen money back to their treasury, they do give a sizable sum to us as thanks for our contribution in finding the assailant.");
                        }
                        else
                        {
                            otherEmpire.AddGold(1200);
                            RelationsManager.ChangeRelations(empire, otherEmpire, -5);
                            RelationsManager.ChangeRelations(otherEmpire, empire, -5);
                            State.GameManager.CreateMessageBox($"After being informed to reveal themselves to the {otherEmpire.Name} authorities communications with our infiltrator go dark. Several days later we receive a package containing the partially digested skull of our agent. It would seem that our neighbors wish us to have no illusions to the fate of spies.");
                        }

                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Take no further action on the matter.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"Meddling with their internal affairs, no matter how lucrative, is far too dangerous. Best to play it safe.");
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"That treasure must be ours! Order our infiltrator to smuggle the magistrate out of the city within their belly.";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(3) == 0)
                        {
                            empire.AddGold(1000);
                            State.GameManager.CreateMessageBox($"In a stroke of luck, our agent was able to enter the magistrate’s home. While they slept our clever agent managed to silently swallow the corrupt official whole. Utilizing tightfitting garb, the infiltrator manages to compress their sloshing belly enough to make them seem heavily pregnant.  \nUpon reaching the city gates, they convince the guards that they are a spurned lover that has been banished from their partner’s estate. The sob story convinces them and they’re allowed to pass. Once far enough away the magistrate is regurgitated, still asleep and only suffering minor acid burns.Their interrogation leads our troops to a small fortune! (1,000 gold) ");
                        }
                        else
                        {
                            RelationsManager.ChangeRelations(empire, otherEmpire, -6);
                            State.GameManager.CreateMessageBox($"Swallowing the magistrate proved surprisingly easy for our infiltrator. Their meal even seemed docile within their belly as they waddled towards the city’s gate. However, as the city guards interrogated our agent their bulging midsection suddenly came to life, screaming and thrashing wildly. It didn’t take long for the soldiers to realize what was going on. \nOur agent attempted to flee but due to their widened form got stuck between two parked carts.The guards promptly split our agent open and freed the magistrate.Needless to say, the {otherEmpire.Name} is less than pleased to find one of our people attempting to kidnap one of their powerful elites.");
                        }
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 24:
                {
                    Empire otherEmpire = GetRandomEmpire(empire);
                    if (otherEmpire == null || !otherEmpire.CanVore)
                        return false;
                    UI.MainText.text = $"A powerful noble family within the {otherEmpire.Name} controls a staggering 1/5th of their territory. This house is traditionally a force of stability within the nation. However, with the recent death of the family head the myriad of scions to the house have begun squabbling over land rights. What shall be done?";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Encourage cooperation between the scions. Stability is crucial for our plans in the region.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(5) != 0)
                        {
                            ChangeAllVillageHappiness(otherEmpire, 10);
                            RelationsManager.ChangeRelations(empire, otherEmpire, 4);
                            RelationsManager.ChangeRelations(otherEmpire, empire, 4);
                            State.GameManager.CreateMessageBox($"Acting as an unbiased mediator interested in only stability, our government along with {otherEmpire.Name} officials, managed to bring the family to the meeting table. Despite a few scions being lost to a gurgling demise, a new house head has been chosen and our neighboring nation seems safe.  At least for now.");
                        }
                        else
                        {
                            otherEmpire.AddGold(1000);
                            RelationsManager.ChangeRelations(empire, otherEmpire, -3);
                            ChangeAllVillageHappiness(otherEmpire, 20);
                            State.GameManager.CreateMessageBox($"Our attempts to intercede have been mistakenly interpreted as an aggressive maneuver to take over the house’s territory. The scions immediately rally under the leadership of their most martial sibling and begin pressuring their state to wage war against us.  \nWhile not what we wanted, the schism seems unlikely now.");
                        }

                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Let the savages do as they will. This does not affect us.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(3) != 0)
                        {
                            foreach (Village vill in State.World.Villages.Where(s => s.Side == otherEmpire.Side))
                            {
                                vill.Happiness -= 30;
                                vill.SubtractPopulation(10);
                                vill.SetPopulationToAtleastTwo();
                            }
                            State.GameManager.CreateMessageBox($"The bloodlust across the border reaches an unprecedented high. Massacres have become common place there as the personal levies of the scions struggle against one another proves disastrous for the countryside. ");
                        }
                        else
                        {
                            ChangeAllVillageHappiness(empire, -15);
                            ChangeAllVillageHappiness(otherEmpire, -30);
                            State.GameManager.CreateMessageBox($"The struggle across the border proves contagious. As the powerful {otherEmpire.Name} family tears itself apart, many of our own powerful elites begin to take notice. Some have become enticed to begin coups of their own against their betters. While the situation is currently being handled, should it be exacerbated our nation may find itself in trouble.");
                        }
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Finally, an opportunity to weaken those arrogant creatures. Inform all of our agents within their borders to work on exacerbating the conflict.";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        if (State.Rand.Next(3) == 0)
                        {
                            ChangeAllVillageHappiness(otherEmpire, -80);
                            State.GameManager.CreateMessageBox($"Our agents have managed to complete a most devious scheme. The largest peace-brokering coalition amongst the scions was meeting over a dinner on what should be done. A single goblet filled with a serum of voracious madness turned the affair into a meeting of gnashing teeth. The coalition found itself closely bound indeed within the stomach of their leader. This act of betrayal has sent the remaining scions into a frenzy and war seems inevitable within {otherEmpire.Name} borders.");
                        }
                        else if (State.Rand.Next(2) == 0)
                        {
                            RelationsManager.ChangeRelations(empire, otherEmpire, -4);
                            ChangeAllVillageHappiness(otherEmpire, -40);
                            State.GameManager.CreateMessageBox($"One of our agents have managed to become the chief consort of one of the main players in the struggle between the scions. They have managed to exacerbate the more militaristic aspects of their significant other’s character.  \nWhile this has indeed increased tension within {otherEmpire.Name} territory an unfortunate consequence has come about.Other scions have taken to blaming their sibling’s foul lover as the source of discourse in their family. This propaganda has spread throughout their nation and has become the leading belief in the matter. This has significantly affected our relationship. ");
                        }
                        else
                        {
                            RelationsManager.ChangeRelations(empire, otherEmpire, -4);
                            RelationsManager.ChangeRelations(otherEmpire, empire, -4);
                            empire.SpendGold(empire.Gold / 5);
                            State.GameManager.CreateMessageBox($"Sadly, it appears the reports of struggle within the family has proven highly inflated. As such our agents have done little more than spend money in vain and antagonized the {otherEmpire.Name} government for attempting to promote a rebellion within their borders.");
                        }
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 25:
                {
                    Village village = GetRandomVillage(empire.Side);
                    if (village == null)
                        return false;
                    UI.MainText.text = $"Two events have transpired within a suspiciously narrow timeframe. First, a surge of reports have been come in from across the empire of missing merchants and villagers. Around the same time a large group of fox “mercenaries” has appeared. Our advisory council believes these mercenaries have likely been ravaging the countryside. What shall be done?";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Stand with our people. Call out these criminals for what they are! Bandits! Demand they leave immediately.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        ChangeAllVillageHappiness(empire, 15);
                        CreateBandits(RebelDifficulty.Easy, village, Race.Foxes);
                        CreateBandits(RebelDifficulty.Easy, village, Race.Foxes);
                        State.GameManager.CreateMessageBox($"The people are pleased with the decision to rid our lands of the marauders. However, the mercenaries have refused to leave and have gathered their army near {village.Name}. They threaten to seize the town unless we act quickly.");

                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Ignore the situation. The damage is minor and dealing with these mercenaries will prove too costly.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        ChangeAllVillageHappiness(empire, -10);
                        foreach (Village vill in State.World.Villages.Where(s => s.Side == empire.Side))
                        {
                            vill.SubtractPopulation(5);
                            vill.SetPopulationToAtleastTwo();
                        }
                        State.GameManager.CreateMessageBox($"As the mercenaries cross our territory more and more reports of missing citizens pour in. The mercenaries after eating their fill of our people move on to the next nation.");
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Our army could use such voracious troops. Buy up their contract and send them to the front line. [500 Gold]";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        ChangeAllVillageHappiness(empire, -5);
                        empire.SpendGold(500);
                        int highestExp = State.GameManager.StrategyMode.ScaledExp;
                        int baseXp = (int)(highestExp * 50 / 100);
                        for (int i = 0; i < 6; i++)
                        {
                            Unit unit = new Unit(empire.Side, Race.Foxes, baseXp, true, UnitType.Mercenary, true);
                            village.VillagePopulation.AddHireable(unit);
                        }
                        State.GameManager.CreateMessageBox($"The mercenaries are glad to take our coin to fight our enemies. Our civilians however are outraged that the gluttonous criminals have been brought into our prestigious army instead of being punished as they rightfully should have. (available at {village.Name})");

                    });
                    UI.ThirdChoice.interactable = empire.Gold >= 500;
                }
                break;
            case 26:
                {
                    Village village = GetRandomVillage(empire.Side);
                    if (village == null)
                        return false;
                    UI.MainText.text = $"{village.Name} has reportedly been suffering mysterious dissapearances, the cause unknown until now. A large feral wolf has taken up residence nearby, and has been feeding on the locals, having been found with a stomach working away at the remains of two innocents, far beyond saving. The people of {village.Name} call for action, should we mobilise the local garrison to deal with the issue, or should we try and gain this beasts favour with a meal? It voracity would certainly prove useful, but such a large beast is not easily tempted.";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Suffer not this beast, slay it and bring justice to the names of those it has already devoured.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        var garrison = village.PrepareAndReturnGarrison();
                        if ((State.Rand.Next(5) >= 1) && (garrison.Count > 0))
                        {
                            State.GameManager.CreateMessageBox($"The canid, although great was successfully slain by the city's garrison. A day after the beast's demise we receive a gift of gold from the village as thanks for our intervention.\n\nVillage Happiness +10, Gold +160");
                            village.Happiness += 10;
                            empire.AddGold(160);
                        }
                        else
                        {
                            State.GameManager.CreateMessageBox($"The sizable canid has devoured the entire garrison with relative ease, snapping up and gulping down each and every soldier, one by one. It's gurgling stomach heard from nearby over the entire night. At sunrise, it pads out of its domicile, fattened up generously by its latest meal, and it heads off for less fattening lands. Whilst there was a grevious loss, at least the beast was driven away.");
                            foreach (var unit in garrison)
                            {
                                village.VillagePopulation.RemoveHireable(unit);
                                village.SubtractPopulation(1);
                            }
                            village.SubtractPopulation(6);
                            village.Happiness -= 10;
                        }


                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Such a matter is beneath you. Ignore their cries for help and attend to more important matters";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        village.SubtractPopulation(20);
                        village.SetPopulationToAtleastTwo();
                        village.Happiness -= 35;
                        CreateRebels(RebelDifficulty.Easy, village);
                        CreateRebels(RebelDifficulty.Easy, village);
                        State.GameManager.CreateMessageBox($"{village.Name} is left to be ravaged by the hungers of the local giant. It gorges itself on the locals, has its fill, and eventually leaves to sample the flavours of other nations. Your inactivity speaks volumes and the people are raising up in arms due to your lack of compassion, or what's left of them anyway.");
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Such a fine example of voracity would make a fine war beast! Round up some civilians to sacrifice as a grand feast to earn the wolf's favour.";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        Unit unit = new Unit(empire.Side, Race.FeralWolves, village.GetStartingXp() * 3, true);
                        unit.AddPermanentTrait(Traits.Large);
                        unit.AddPermanentTrait(Traits.IronGut);
                        unit.AddPermanentTrait(Traits.StrongGullet);
                        unit.DigestedUnits = State.Rand.Next(15, 35);
                        village.VillagePopulation.AddHireable(unit);
                        village.SubtractPopulation(20);
                        village.SetPopulationToAtleastTwo();
                        village.Happiness -= 40;
                        State.GameManager.CreateMessageBox($"{unit.Name} the feral wolf is available for recruitment at {village.Name} after gorging itself to immobility on the sacrificed villagers. Whilst the people look at such an act with much distaste, adding such a powerful beast to our ranks will surely pay off with time.");
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 27:
                {
                    Village village = GetRandomVillage(empire.Side);
                    if (village == null)
                        return false;
                    UI.MainText.text = $"Concerned citizens from {village.Name} have reached out with stories about a dangerous animal in their midst. After many odd tales about a \"friendly big kitten\" from one of the children, adults decided to take a look into the shed in question. There were only those that did not return and those that were then kept out by the ominous gurgling noises. How does one react to this?";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"The safety of our citizens is our utmost duty. Have the garrison bait and ambush the creature.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        {
                            var garrison = village.PrepareAndReturnGarrison();
                            if ((State.Rand.Next(2) == 0) && (garrison.Count > 0))
                            {
                                State.GameManager.CreateMessageBox($"The garrison successfully baits the creature out of it's den, once out they slowly lure it away from the village into the wilderness. The villagers are relieved to have the predator dealt with. The child, although sad that their \"kitty\" is gone, is happy knowing that it wasn’t harmed and is free to eat all it wants.\n\nVillage Happiness +20, Gold +100");
                                empire.AddGold(100);
                                village.Happiness += 20;
                            }
                            else
                            {
                                foreach (var unit in garrison)
                                {
                                    village.VillagePopulation.RemoveHireable(unit);
                                    village.SubtractPopulation(1);
                                }
                                village.SubtractPopulation(village.Maxpop / 6);
                                village.SetPopulationToAtleastTwo();
                                empire.AddGold(50);
                                village.Happiness += 10;
                                Unit youth = new Unit(empire.Side, empire.ReplacedRace, village.GetStartingXp(), empire.CanVore && !State.RaceSettings.Get(empire.ReplacedRace).RaceTraits.Contains(Traits.Prey));
                                youth.Items[0] = State.World.ItemRepository.GetItem(ItemType.CompoundBow);
                                Unit lion = new Unit(empire.Side, Race.FeralLions, 0, true);
                                lion.AddPermanentTrait(Traits.Large);
                                lion.AddPermanentTrait(Traits.IronGut);
                                lion.AddPermanentTrait(Traits.StrongGullet);
                                youth.AddPermanentTrait(Traits.PleasurableTouch);
                                lion.Name = "Kitty";
                                lion.DigestedUnits = State.Rand.Next(village.Maxpop / 6, (int)(village.Maxpop / 4));
                                lion.GiveExp(lion.DigestedUnits * 10);
                                State.GameManager.CreateMessageBox($"Dear government,\n\nKitty would like to say thanks for the treat, but I think something about soldier equipment makes {(lion.GetPronoun(2))} gassy, heheh. Anyways, it's been feeling pretty noisy here for Kitty so we'll leave!\nXOXO Kitty and {youth.Name} \n\nOn the bright side, we could recover all the victims' valuables and the remaining citizens are thankful for our efforts.\n\nVillage Happiness +10, Gold +50, Garrison wiped out, Population -{(village.Maxpop / 6)}, The duo is still out there");
                                var armyName = $"Kitty and {youth.Name}";
                                Empire kittyEmp = CreateFactionlessArmy(village, 57, new[] { lion, youth }, 10, armyName);
                                kittyEmp.Name = armyName;
                                youth.Side = kittyEmp.Side;
                                lion.Side = kittyEmp.Side;
                            }
                        }

                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Fables, superstition, and vaguery, without a single witness. We cannot be bothered.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        village.SubtractPopulation(village.Maxpop / 3);
                        village.SetPopulationToAtleastTwo();
                        village.Happiness -= 15;
                        Unit youth = new Unit(empire.Side, empire.ReplacedRace, village.GetStartingXp(), empire.CanVore && !State.RaceSettings.Get(empire.ReplacedRace).RaceTraits.Contains(Traits.Prey));
                        youth.Items[0] = State.World.ItemRepository.GetItem(ItemType.CompoundBow);
                        Unit lion = new Unit(empire.Side, Race.FeralLions, 0, true);
                        lion.AddPermanentTrait(Traits.Large);
                        lion.AddPermanentTrait(Traits.IronGut);
                        lion.AddPermanentTrait(Traits.StrongGullet);
                        youth.AddPermanentTrait(Traits.PleasurableTouch);
                        lion.Name = "Kitty";
                        lion.DigestedUnits = State.Rand.Next(village.Maxpop / 3, (int)(village.Maxpop / 2));
                        lion.GiveExp(lion.DigestedUnits * 10);
                        var raceSingular = InfoPanel.RaceSingular(youth);
                        State.GameManager.CreateMessageBox($"The villagers aren't thrilled, but the so-called \"kitten\" and their little {raceSingular} friend continue to grow up alongside the people of {village.Name}. While only some of them learn to be merry with the ever more grown-up, less feisty feline in their midst, everyone knows not to question the disappearing neighbors or the well-rounded lion gut. Eventually, the duo leaves and goes on adventures in the surrounding lands.\n\n{village.Name} Population -{village.Maxpop / 3}, Happiness -15, The duo stays neutral to the empire");
                        var armyName = $"Kitty and {youth.Name}";
                        Empire kittyEmp = CreateFactionlessArmy(village, 57, new[] { lion, youth }, 10, armyName);
                        kittyEmp.Name = armyName;
                        youth.Side = kittyEmp.Side;
                        lion.Side = kittyEmp.Side;
                        RelationsManager.SetPeace(kittyEmp, empire);
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Seek a favorable standing with the child and convince them to enroll once they're old enough. They can bring their pet, of course.";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        Unit youth = new Unit(empire.Side, empire.ReplacedRace, village.GetStartingXp(), empire.CanVore && !State.RaceSettings.Get(empire.ReplacedRace).RaceTraits.Contains(Traits.Prey));
                        Unit lion = new Unit(empire.Side, Race.FeralLions, village.GetStartingXp(), true);
                        lion.AddPermanentTrait(Traits.Large);
                        lion.AddPermanentTrait(Traits.IronGut);
                        lion.AddPermanentTrait(Traits.StrongGullet);
                        lion.Name = "Kitty";
                        youth.AddPermanentTrait(Traits.PleasurableTouch);
                        youth.ModifyStat(Stat.Dexterity, 20);
                        lion.DigestedUnits = State.Rand.Next(village.Maxpop / 2, (int)(village.Maxpop / 1.5));
                        village.SubtractPopulation(village.Maxpop / 2);
                        village.SetPopulationToAtleastTwo();
                        village.VillagePopulation.AddHireable(lion);
                        village.VillagePopulation.AddHireable(youth);
                        village.Happiness -= 40;
                        State.GameManager.CreateMessageBox($"The people feel betrayed with how their government seems to extend its protection towards the beast and its keeper rather than them, as it devours more and more of them unhindered. On the bright side, Kitty grows up big and strong, and the duo can be recruited in {village.Name}.\n\n Happiness -40, Population -{village.Maxpop / 2}");
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;

			case 28:
                {
                    Village village = GetRandomVillage(empire.Side);
                    if (village == null)
                        return false;
                    UI.MainText.text = $"This morning it was discovered that a rancher at a {village.Name} terrorbird ranch has gone missing. This afternoon they were located inside the pens, unfortunately inside a terrorbird. The large squirming gut informs us they were ingested after feeding the birds ten days ago and that magic prevents them from digesting whilst on the job. The ranch needs some help with the regurgitation process. What should we do?";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Aid the ranch immediately. Free the rancher from the terrorbird and if that can't be done cleanly prepare the rotissery spits.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        village.Happiness += 20;
                        State.GameManager.CreateMessageBox($"The rancher is pleased and unharmed although their clothes were disolved over the week long digestion. {village.Name} is pleased!");
                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Undo the magic. The rancher proved they were weak by getting swallowed in the first place while the bird has earned its place in my empire!";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        Unit unit = new Unit(empire.Side, Race.Terrorbird, village.GetStartingXp(), true);
                        unit.AddPermanentTrait(Traits.IronGut);
                        unit.DigestedUnits = 1;
                        village.VillagePopulation.AddHireable(unit);
                        village.SubtractPopulation(1);
                        village.Happiness -= 5;
                        State.GameManager.CreateMessageBox($"The engorged terrorbird is calmly taken back to the garrison where they are made comfortable. The ingested rancher is informed that they will be releases shortly and they curl up awaiting the enchantment needed to escape. After the spell is cast all is still for a few minutes util the rancher begins to feel soft. They squirm and writhe about inside the bird's stomach much to its pleasure. After 20 minutes of futile pleading the rancher grows silent and the terrorbird's stomach smooths out. The bird lets out a satisfied chirp with its belly sloshing the digested rancher inside. {unit.Name} the Terrorbird is available for recruitment at {village.Name}");
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Do not free him and offer to buy the terrorbird and the rancher's contract. We will keep the bird in a zoo as an attraction! Terrobirds are adorable when they aren't hungry!";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        village.SubtractPopulation(1);
                        empire.AddGold(500);
                        ChangeAllVillageHappiness(empire, 10);
                        State.GameManager.CreateMessageBox($"The gurgling terrorbird stomach kindly asks you to reconsider but our pens are already signing the contracts. We recieve a modest up front fee and the bird with ingested rancher are shipped off to the {empire.CapitalCity} zoo. The Terror bird is well taken care of and enjoys being an attraction. She presents her belly to visitors so they can see the hand prints of her most prized catch. The Rancher will be sloshing in her stomach until the magic wears out and the rancher will be mashed into a nutricious sludge when the contract expires in 5 years.");
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 29:
                {
                    Village village = GetRandomVillage(empire.Side);
                    if (village == null)
                        return false;
                    UI.MainText.text = $"A patron of a local alchemist store was surprised to find that the cheerful clerk had been replaced with a small group of ineffective kobolds. The customer was extremely dissatisfied with the service and went looking for the store owner in the back. Instead of the kangaroo she instead found a very bloated kobold and the muffled voice of the alchemist calling out for help. The kobold has been aprehended but we're unsure as to the appropriate action to take on predatory business aquisitions.";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Free the alchemist and send the kobold to whatever doom they may have earned.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        village.Happiness += 20;
                        State.GameManager.CreateMessageBox($"The soft and partially digested alchemist is extremely greatful to not be kobold pudge. He returns home and the {village.Name} is pleased with your decision.");
                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"As long as the kobold pays taxes on the property exchange it's not our business how they deal with the competition.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        village.SubtractPopulation(1);
                        village.Happiness -= 5;
                        empire.AddGold(500);
                        State.GameManager.CreateMessageBox($"The kobold grows a toothy grin upon hearing the news. At first he though himself in trouble with the law, now finds it on his side. The alchemist shouts in protest after hearing our decree and squirms and struggles inside the bulging kobold stomach. The guards that brough the kobold in are now helping him carry his massive stomach out of the room while the alchemist begs for help from anyone. The next day we recieve a very large payment in property tax as well as a gift of a bleached alchemist skull. It will go nicely on our wall as will the alchemist go nicely on the kobold after the nutricious alchemist chyme is absorbed. For now the kobold just jiggles.");
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"This kobold and their cohorts were able to swallow something much larger than themselves individually. This is the kind of talent we need!";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        Unit unit;
                        for (int x = 0; x < 3; x++)
                        {
                            unit = new Unit(empire.Side, Race.Kobolds, village.GetStartingXp(), true);
                            unit.AddPermanentTrait(Traits.PackVoracity);
                            unit.AddPermanentTrait(Traits.PackStomach);
                            if (x == 0)
                            {
                                unit.DigestedUnits = 1;
                            }
                            village.VillagePopulation.AddHireable(unit);
                        }
                        village.SubtractPopulation(1);
                        village.Happiness -= 5;
                        State.GameManager.CreateMessageBox($"Three kobolds are available for hire at {village.Name}");
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            //Event Idea by Dr.Fetish from discord
            case 30:
                {
                    if ((empire.Leader == null) || (empire.Leader.Race >= Race.Vagrants))
                        return false;
                    UI.MainText.text = $"A wandering smith of great renown has come from a far in search of {empire.Leader.Name}. The smith approaches {empire.Leader.Name} saying they wish to forge an enchanted weapon for {(empire.Leader.GetPronoun(1))} to support {(empire.Leader.GetPronoun(2))} cause. What does {empire.Leader.Name} do?";
                    UI.FirstChoice.GetComponentInChildren<Text>().text = "Requests an enchanted ranged weapon";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"The smith appears slightly dissapointed to not make a melee weapon, but nonetheless begins to hold their hands out in front of themselves as a bright white orb materializes in front of them, then they clap their hands together; the orb disappearing within. Then, as the smith pulls their hands apart, {empire.Leader.Name} watches the magical weapon levitate from the smith directly into {(empire.Leader.GetPronoun(2))} hands.\n\nLeader Gains Tier 3 Ranged weapon the \"Omni Launcher\"");
                        empire.Leader.Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.OmniLauncher);
                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = "Requests an enchanted melee Weapon";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"The smith gets a big grin on their face then begins to hold their hands out in front of themselves as a bright white orb materializes in front of them, then they clap their hands together; the orb disappearing within. Then, as the smith pulls their hands apart, {empire.Leader.Name} watches the magical weapon levitate from the smith directly into {(empire.Leader.GetPronoun(2))} hands.\n\nLeader Gains Tier 3 Melee weapon the \"Omni Buster\"");
                        empire.Leader.Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.OmniBuster);
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = "Asks the smith for a \"Special request.\"";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        if (empire.Leader.HasTrait(Traits.Prey))
                            State.GameManager.CreateMessageBox($"{empire.Leader.Name} requests a set of armor explaining they are content with their current weapons, the smith thinks for a moment then gets an idea and asks {empire.Leader.Name} to outstretch  {(empire.Leader.GetPronoun(2))} hands. Clasping {empire.Leader.Name}'s hands in theirs the smith's hands glow a bright white which flows into {empire.Leader.Name} leaving {(empire.Leader.GetPronoun(2))} feeling greatly empowered. Before {empire.Leader.Name} can say a word to the smith {(empire.Leader.GetPronoun(0))} realizes they're gone.\n\nLeader gains EXP");
                        else
                            State.GameManager.CreateMessageBox($"The smith leans in, excited to hear what unique weapon {empire.Leader.Name} has in mind for them to make. However, just as the smith enters whispering range {empire.Leader.Name} easily swallows them whole! \"Don't worry. Look at it like this, you ARE still helping my cause just... in a different way!\" {empire.Leader.Name} says as the smith wobbles in {(empire.Leader.GetPronoun(2))} belly. However, almost in reaction to the leader's remark {(empire.Leader.GetPronoun(2))} belly glows in a bright light then stops leaving {empire.Leader.Name} feeling as though {(empire.Leader.GetPronoun(0))} just had a grand feast, despite only having one meal.\n\nLeader gains EXP");
                        GiveExp(empire.Leader, 80, .09f);
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
            case 31:
                {
                    Village village = GetRandomVillage(empire.Side);
                    if (village == null)
                        return false;
                    UI.MainText.text = $"One of our scouts from {village.Name} returned from their patrols reporting the presence of a group of fox bandits forming just on the outskirts of the settlement for a surprise attack. What shall be done?";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Let the people know the situation and that the bandits shall be swifty dispatched.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        village.Happiness += 15;
                        CreateBandits(RebelDifficulty.Medium, village, Race.Foxes);
                        State.GameManager.CreateMessageBox($"The people can rest easy knowing the situation shall be resolved. The bandits gather to seize {village.Name}, now is our time to prepare or go in for a preemptive strike. \n\nHappiness +15");
                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Demand the people of {village.Name} to provide us additional gold so that we may shore up adequate defences.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        int payout = 170;
                        village.Happiness -= 10;
                        if (State.Rand.Next(2) == 0)
                        {
                            empire.AddGold(170);
                        }
                        else
                        {
                            empire.AddGold(250);
                            payout = 250;
                        }
                        CreateBandits(RebelDifficulty.Medium, village, Race.Foxes);
                        State.GameManager.CreateMessageBox($"Althogh unhappy the people willingly scrounge together what gold they can spare. To assist in the settlement's defence. \n\nHappiness -10, Gold +{payout}.");
                    });
                    UI.SecondChoice.interactable = true;
                }
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                    });
                    UI.ThirdChoice.interactable = false;
                break;
            //Event from Ryan The Sergal on discord
            case 32:
                {
                    Village village = GetRandomVillage(empire.Side);
                    if (village == null)
                        return false;
                    UI.MainText.text = $"The war has caused many guards who would otherwise patrol and defend the region around {village.Name} to join the army or become mercenaries instead. This has given a nearby gang of bandits the opportunity to act and grow freely. Recent reports indicate that they may be growing big enough to launch a raid on the city itself. How must we respond?";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"These reports are overblown. The local garrison is more than strong enough to handle it on their own.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        village.Happiness -= 10;
                        CreateBandits(RebelDifficulty.Medium, village, Race.Humans);
                        State.GameManager.CreateMessageBox($"After some time of raiding the local trade routes the bandits muster their forces for an attack on {village.Name}. \n\nHappiness -10");
                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"The garrison can't handle this right now. We must invest gold to have some of the local adventurers deal with this threat before it becomes a problem. [200 gold]";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        village.Happiness += 10;
                        empire.SpendGold(200);
                        State.GameManager.CreateMessageBox($"It didn’t take long after the bounties were made for the bandits, for adventurer and mercenary alike to wipeout the bandits making the roads safe yet again. \n\nHappiness +10, Gold -200");
                    });
                    UI.SecondChoice.interactable = empire.Gold >= 200;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"Our garrison must handle this threat before it becomes a problem. Have them sally out and deal with them.";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        var garrison = village.PrepareAndReturnGarrison();
                        if ((State.Rand.Next(5) >= 1) && (garrison.Count > 0))
                        {
                            village.Happiness += 10;
                            State.GameManager.CreateMessageBox($"The garrison returns after not long reporting that they were successful in weakening the raiding party but were unable to finish them off before the rest scattered.\n\nHappiness +10, Bandit forces weakened");
                            CreateBandits(RebelDifficulty.Easy, village, Race.Humans);
                        }
                        else if (garrison.Count == 0)
                        {
                            village.Happiness -= 10;
                            State.GameManager.CreateMessageBox($"After the order is given a letter quickly returns from the city simply reading. \n\n\"Ah, yes allow us to send in our garrison, armed with the weapons that our great and mighty government has given us. Oh wait, I'm sorry. I've just been informed that we DON'T HAVE ONE!\"\n\nHappiness -10, The bandits prepare for their raid");
                            CreateBandits(RebelDifficulty.Medium, village, Race.Humans);
                        }
                        else
                        {
                            State.GameManager.CreateMessageBox($"The raiders were warned beforehand of our plans. Because of this, they had the element of surprise on their side. Our soldiers fought bravely and fearlessly, but in the end, they have all been slain or devoured by these merciless raiders. We must finish what our fallen heroes have started and crush the raiders now that they are in a weakened state.\n\nGarrison Lost, Bandit forces weakened");
                            CreateBandits(RebelDifficulty.Easy, village, Race.Humans);
                            foreach (var unit in garrison)
                            {
                                village.VillagePopulation.RemoveHireable(unit);
                                village.SubtractPopulation(1);
                            }
                        }
                    });
                }
                    UI.ThirdChoice.interactable = true;
                break;
            case 33://Prey event|Event from Ryan The Sergal on discord
                {
                    var occupiedVillages = State.World.Villages.Where(s => s.Side == empire.Side && s.Race != empire.ReplacedRace);
                    var selfVillages = State.World.Villages.Where(s => s.Side == empire.Side && s.Race == empire.ReplacedRace);
                    Village village = empire.CapitalCity;
                    if (village == null || empire.CanVore || occupiedVillages == null || village.Side != empire.Side)
                        return false;
                    UI.MainText.text = $"Whether by recruitment or natural migration, a recent census has determined that we now have a substantial population of voracious predators among our general populous. Some of them have begun to devour our people as well, whether it be willing or unwilling. The people are looking to us for an official response.";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Let them live as they always have, but enforce some restrictions on eating the unwilling.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        foreach (Village vill in occupiedVillages)
                        {
                            vill.Happiness += 25;
                            vill.SubtractPopulation(10);
                            vill.SetPopulationToAtleastTwo();
                        }
                        State.GameManager.CreateMessageBox($"The new voracious population is grateful for your acceptance of their way of life. They’ve fully integrated into our society and live happily. Those who want to be devoured are happier as well, as they can now live out their fantasies. However, this has resulted in people all over the empire to go ‘missing’. Leading to an overall decrease in population. \n\nHappiness +25, Population -10 for all foreign towns");
                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"Devouring other sapient beings whole is unacceptable. Force these preds to convert at once.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        foreach (Village vill in occupiedVillages)
                        {
                            vill.Happiness -= 15;
                            vill.AddPopulation(20);
                        }
                        State.GameManager.CreateMessageBox($"We have managed to convert their people to our way of life, whether it be by force or by free will. We have saved many lives through this, no doubt. This, combined with the continued immigration and conversion of pred species has led to an overall increase in population. However, these former preds have expressed unhappiness and vocal disagreement with this policy as they’re forced to abandon their natural way of life. \n\nHappiness -15, Population +20 for all foreign towns");
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"These monsters infiltrate our lands and devour our people?! Drive them out before they can eat any more innocents!";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        foreach (Village vill in occupiedVillages)
                        {
                            vill.Happiness -= 30;
                            vill.SubtractPopulation(40);
                            vill.SetPopulationToAtleastTwo();
                        }
                        foreach (Village vill in selfVillages)
                        {
                            vill.Happiness -= 10;
                            vill.SubtractPopulation(5);
                        }
                        empire.AddGold(600);
                        State.GameManager.CreateMessageBox($"Our response was brutal and swift. The voracious monsters have been fully driven from our lands, giving our people much-needed peace of mind, knowing they will no longer end up as some beast's next meal. Our response was so quick, that we’ve managed to loot a good number of their belongings, which have fetched us a decent amount of gold to fund our empire. However, this was not without loss of life on our part. The loss of life, combined with our rather brutal approach has left our people with decreased happiness as they mourn their losses and fear our way of governing. \n\nHappiness -30, Population -40 for all foreign towns \nHappiness -10, Population -5 for all non-foreign towns \n+600 Gold");
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;
/*            case 34://Test for Empire wide Garrison modification.
                {
                    var totalVillages = State.World.Villages.Where(s => s.Side == empire.Side);
                    var village = GetRandomVillage(empire.Side);
                    if (village == null || village.Garrison == 0)
                        return false;
                    UI.MainText.text = $"Test for Garrison EXP.";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = "ALL Red shirts shall be executed.";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"All garrisons exterminated");
                        foreach (Village vill in totalVillages)
                        {
                            var garrison = vill.PrepareAndReturnGarrison();
                            foreach (var unit in garrison)
                            {
                                vill.VillagePopulation.RemoveHireable(unit);
                                vill.SubtractPopulation(1);
                            }
                        }
                    });
                    UI.FirstChoice.interactable = true;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = "Invest in militry training for all garrisons. [500 Gold]";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"All garrisons gain +100xp");
                        empire.SpendGold(500);
                        foreach (Village vill in totalVillages)
                        {
                            var garrison = vill.PrepareAndReturnGarrison();
                            foreach (var unit in garrison)
                            {
                            GiveExp(unit, 100, 0f);
                            }
                        }
                    });
                    UI.SecondChoice.interactable = empire.Gold >= 500;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = "Leave them be";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"The garrisons remain as is");
                    });
                    UI.ThirdChoice.interactable = true;
                }
                break;*/
                //Event from Ryan The Sergal on discord
            case 34:
                {
                    var village = GetRandomVillage(empire.Side);
                    if (village == null || village.Garrison == 0)
                        return false;
                    UI.MainText.text = $"The state of our garrison at {village.Name} has been deemed inadequate by most of our officer corps, several of them citing a lack of good quality training and discipline, giving them no experience when fighting on the field. We have been suggested to enact some military reforms at that city, giving our garrison there much better quality training than they had before. While this will no doubt make them much more skilled and formidable and thus give them much higher survivability, it will be quite expensive to put into action. How should we proceed?";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = "The safety and strength of our people and soldiers are much more important than some excess wealth. Go through with the reforms!  [250 Gold]";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"While the better gear and extra training for both the local officers and soldiers was expensive, we have already seen a dramatic increase in discipline, strategic understanding and general combat effectiveness. The soldiers are now much better trained than they were before and thus much more capable to withstand invasion of any kind without the immediate aid of our army. \n\n-250 Gold, Garrison gains +100 EXP");
                        empire.SpendGold(250);
                        var garrison = village.PrepareAndReturnGarrison();
                        foreach (var unit in garrison)
                        {
                            GiveExp(unit, 100, 0.05f);
                        }
                    });
                    UI.FirstChoice.interactable = empire.Gold >= 250;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = "While tempting and no doubt important, we simply don't have the funds for this. We must stay our current course for now.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"The local garrison has not received its special training. This has no immediate negative consequences, as we keep our money and don't change anything about their training regiment. However, they will be just as ill-prepared for future conflicts as they are now, likely relying more on our army's direct support to maintain the safety and control over their city.");
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = "We are low on funds as it is, and now these worthless leeches need more to do their jobs? Have the officers devour them all! Let's make those useless worms actually useful!";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        State.GameManager.CreateMessageBox($"The local garrison was devoured by the officer corps for their incompetence. One by one, they were forced down the gullets of their superiors, ending up as nothing but padding on their hips. While at first they showed some concern regarding this decision, they had their minds quickly changed when they got to enjoy this grand and delicious meal, some even donating personal funds to our treasury in thanks. Perhaps now that the incompetent old guard is gone, they can be replaced with more capable, deadly soldiers and mercenaries instead. \n\nGarrison wiped out, +300 Gold");
                        var garrison = village.PrepareAndReturnGarrison();
                        foreach (var unit in garrison)
                        {
                            village.VillagePopulation.RemoveHireable(unit);
                            village.SubtractPopulation(1);
                        }
                    });
                    UI.ThirdChoice.interactable = empire.CanVore;
                }
                break;
                //Event Idea from Alex discord
            case 35:
                {
                    Village village = GetRandomVillage(empire.Side);
                    if (village == null)
                        return false;
                    UI.MainText.text = $"A dragon that has been harassing {village.Name} for some time has grown tired of the town's meagre offerings to placate them, their last visit they demanded that a \"substantial tribute\" would be expected next time. What should the town do?";
                    UI.MainText.text += AddVillageInfo(village);
                    UI.FirstChoice.GetComponentInChildren<Text>().text = $"Dragons are fierce creatures we cannot offord to provoke one. Take money from our coffers to make sure the dragon is appeased. [500]";
                    UI.FirstChoice.onClick.AddListener(() =>
                    {
                        village.Happiness += 25;
                        empire.SpendGold(500);
                        State.GameManager.CreateMessageBox($"The dragon gladly accepts the tribute and leaves the town alone for now... The people of {village.Name} are grateful for our assistance in having the dragon dealt with even if temporarily. \n\nHappiness +25, -500 Gold");
                    });
                    UI.FirstChoice.interactable = empire.Gold >= 500;
                    UI.SecondChoice.GetComponentInChildren<Text>().text = $"The last thing we want is an angry dragon roaming across our lands, unfortunately we don't have the funds for a larger offering. Propose giving some villagers as servants to the dragon.";
                    UI.SecondChoice.onClick.AddListener(() =>
                    {
                        village.Happiness -= 25;
                        village.SubtractPopulation(village.Population / 2);
                        village.SetPopulationToAtleastTwo();
                        State.GameManager.CreateMessageBox($"The dragon liked the proposition, perhaps too much... The dragon first chose their servants the dragon then ate a substantial amount of the town's population threatening to raise the whole town to the ground should anyone object. Once the dragon ate their fill they left with newfound servants in tow. Let's hope the dragon won’t return soon. \n\nHappiness -25, Population halved");
                    });
                    UI.SecondChoice.interactable = true;
                    UI.ThirdChoice.GetComponentInChildren<Text>().text = $"We shall suffer this dragon no longer. Send some scouts to steal from their hoard!";
                    UI.ThirdChoice.onClick.AddListener(() =>
                    {
                        int payout = 500;
                        if (State.Rand.Next(2) == 0)
                        {
                            empire.AddGold(500);
                        }
                        else
                        {
                            empire.AddGold(800);
                            payout = 800;
                        }
                        State.GameManager.CreateMessageBox($"Our scouts return in success, managing to escape the dragon with sacks full of gems and gold! However the scouts also report that dragon is currently on the way to the town, let's hope we can fend it off.\n\n+{payout} Gold, The dragon about to attack the town!");
                        int highestExp = State.GameManager.StrategyMode.ScaledExp;
                        int baseXp = (int)(highestExp * 50 / 100);
                        Unit dragon = new Unit(114, Race.Dragon, baseXp, true);
                        dragon.AddPermanentTrait(Traits.Large);
                        var armyName = $"{dragon.Name} The Vengeful";
                        Empire dragonEmp = CreateFactionlessArmy(village, 35, new[] {dragon}, 1, armyName);
                        dragonEmp.Name = armyName;
                        dragon.Side = dragonEmp.Side;
                    });
                }
                    UI.ThirdChoice.interactable = true;
                break;

            default:
                return false;
        }
        UI.gameObject.SetActive(true);
        UI.RaceText.text = $"{empire.Name} -- Gold : {empire.Gold}";
        return true;
    }

    string AddVillageInfo(Village village)
    {
        return $"\n{village.Name} ({village.Race})\nVillage Garrison : {village.Garrison}\nVillage Happiness : {village.Happiness}\nVillage Population : {village.VillagePopulation.GetPopReport()}";
    }

    internal void CheckStartAIEvent()
    {
        if (Config.RandomAIEventRate > 0)
        {
            float odds = 0;
            switch (Config.RandomAIEventRate)
            {
                case 1:
                    odds = .1f;
                    break;
                case 2:
                    odds = .25f;
                    break;
                case 3:
                    odds = .5f;
                    break;
                case 4:
                    odds = .95f;
                    break;
            }
            if (odds < State.Rand.NextDouble())
                return;
        }
        else
            return;
        Empire[] empires = GetTwoRandomAIEmpires();
        if (empires == null)
            return;
        Empire first = empires[0];
        Empire second = empires[1];
        int num = State.Rand.Next(32);
        int time = 90;
        if (State.GameManager.StrategyMode.OnlyAIPlayers)
            time = 12;
        if (Config.AIToAIMessagesForever)
            time = 0;
        switch (num)
        {
            case 0:
                State.GameManager.CreateMessageBox($"After a strange series of events, a general from the {first.Name} has fallen in love and proposed to a brave warrior loyal to the {second.Name}. This event is sure to deepen the two nation’s bonds.", time);
                RelationsManager.ChangeRelations(first, second, 1);
                RelationsManager.ChangeRelations(second, first, 1);
                break;
            case 1:
                State.GameManager.CreateMessageBox($"A young warrior from the {first.Name} has eloped with a dashing rogue from the {second.Name}! Stealing away in their lover’s belly, the two made it back to the other’s home country. The {first.Name} are furious about this.", time);
                RelationsManager.ChangeRelations(first, second, -1);
                RelationsManager.ChangeRelations(second, first, 1);
                break;
            case 2:
                State.GameManager.CreateMessageBox($"Troops from the {first.Name} committed an unsanctioned raid against several small {second.Name} communities along the border, returning with loot and full bellies. This plundering is sure to hamper relations between the two.", time);
                RelationsManager.ChangeRelations(first, second, -2);
                first.AddGold(400);
                break;
            case 3:
                State.GameManager.CreateMessageBox($"Eating slaves originating from {second.Name} territories has become all the rage amongst {first.Name} aristocracy. This voracious act is certain to garner tension.", time);
                RelationsManager.ChangeRelations(first, second, -1);
                break;
            case 4:
                State.GameManager.CreateMessageBox($"A rousing story of comradely and even romance between a brave warrior from the {first.Name} and a noble enchantress originating from the {second.Name} has begun spreading throughout the land. People from both empires become enamored by the story.", time);
                RelationsManager.ChangeRelations(first, second, 1);
                RelationsManager.ChangeRelations(second, first, 1);
                break;
            case 5:
                State.GameManager.CreateMessageBox($"A friendly tourney of skill within the {second.Name} territory turned disastrous when their champion ended up gurgling away in the gut of the competitor from the {first.Name}. A riot soon broke out and many attended up sharing a similar fate.", time);
                RelationsManager.ChangeRelations(first, second, -1);
                break;
            case 6:
                State.GameManager.CreateMessageBox($"Envoys from the {first.Name} came to visit their counterparts within the {second.Name}. However, after a horrendous miscommunication the {second.Name} representatives believed the envoys themselves were a gift. Soon after, the envoys were devoured. Both are furious. One for the consumption of their envoys and the other for the struggling from their surpassingly willing meal.", time);
                RelationsManager.ChangeRelations(first, second, -1);
                RelationsManager.ChangeRelations(second, first, -1);
                break;
            case 7:
                State.GameManager.CreateMessageBox($"Despite the {first.Name} princess being sent out to find a suitable mate they return with nothing but a fattened ass, enormous breast and angry letters from the {second.Name}. It seems they let loose their inhibitions while touring the world.", time);
                RelationsManager.ChangeRelations(first, second, -1);
                break;
            case 8:
                State.GameManager.CreateMessageBox($"{first.Name} publically denounces the {second.Name} as little more than gut sluts begging to be consumed.", time);
                RelationsManager.ChangeRelations(first, second, -1.5f);
                RelationsManager.ChangeRelations(second, first, -1.5f);
                break;
            case 9:
                State.GameManager.CreateMessageBox($"{first.Name} takes credit for a recent attack resulting in the consumption of a pilgrimage group attempting to reach {second.Name} territory. The event has sent ripples of rage throughout the religious communities within the {second.Name}.", time);
                RelationsManager.ChangeRelations(first, second, -2f);
                break;
            case 10:
                State.GameManager.CreateMessageBox($"A cache of rare gems has been found within the belly of a wyvern slain within {first.Name} territory. Their state has of course taken a portion of the gems for their coffers.", time);
                first.AddGold(200);
                break;
            case 11:
                State.GameManager.CreateMessageBox($"The construction of a food slave market within {first.Name} has resulted in predators from across the world travelling to sample a taste of their wares. Coin flows as fast as one can swallow within their nation.", time);
                first.AddGold(200);
                break;
            case 12:
                State.GameManager.CreateMessageBox($"A famine has struck within {first.Name} territory, driving many to attempt to satiate their hunger through banditry.", time);
                {
                    Village village = GetRandomVillage(first.Side);
                    CreateBandits(RebelDifficulty.Easy, village, village.Race);
                }
                break;
            case 13:
                State.GameManager.CreateMessageBox($"{first.Name} has sent a number of lavishly expensive gifts both gaudy and delicious to the {second.Name}. The act will likely strengthen their bonds.", time);
                RelationsManager.ChangeRelations(first, second, 1);
                RelationsManager.ChangeRelations(second, first, 1);
                break;
            case 14:
                State.GameManager.CreateMessageBox($"Leaders from the {first.Name} compliment the people of the {second.Name} for their deep culture, beautiful women, and exquisite taste. While flattered initially, the comments caused more worry than ease amongst {second.Name} peoples.", time);
                RelationsManager.ChangeRelations(first, second, -.5f);
                RelationsManager.ChangeRelations(second, first, 1);
                break;
            case 15:
                State.GameManager.CreateMessageBox($"Our spies report an interesting rumor. Apparently a beautiful seductress from the {first.Name} got her claws into and her lips around the head of the husband of a powerful {second.Name} noble. The noble matriarch is furious at both the infidelity of their spouse and subsequent devouring. Relations are tense between the two nations.", time);
                RelationsManager.ChangeRelations(first, second, -.75f);
                break;
            case 16:
                State.GameManager.CreateMessageBox($"News from the {second.Name}. Apparently a wealthy, curvaceous merchant from the {first.Name} called for suitors from the {second.Name} as she had a ‘preference’ for their people. Scores of people answered the call. However, it was soon revealed that her curves were from voracious pursuits and that all of her suitors became fat on her breast.", time);
                RelationsManager.ChangeRelations(first, second, -.75f);
                break;
            case 17:
                State.GameManager.CreateMessageBox($"News from the {first.Name} territories. It would seems that a power struggle has erupted within a noble family after a scion of the family attempted to devour their siblings. Unfortunately for them their ambition proved larger than their stomach and most siblings escaped the gurgling fate. Now they’ve assembled small armies of their own and seek to gain revenge.", time);
                {
                    Village village = GetRandomVillage(first.Side);
                    CreateBandits(RebelDifficulty.Easy, village, village.Race);
                }
                {
                    Village village = GetRandomVillage(first.Side);
                    CreateBandits(RebelDifficulty.Easy, village, village.Race);
                }
                break;
            case 18:
                State.GameManager.CreateMessageBox($"The leader of the {first.Name} sends an ominous message to the rules of the {second.Name} stating in no uncertain terms that it is their fate to melt away into their curves.", time);
                RelationsManager.ChangeRelations(first, second, -1);
                RelationsManager.ChangeRelations(second, first, -1);
                break;
            case 19:
                State.GameManager.CreateMessageBox($"Brutal fighting on the international market! A party of merchants from the {first.Name} have seized the assets of their rivals from the {second.Name}. They did so through a series of bribes, wise investments, and by quite literally eating the competition. While the {first.Name} state has capitalized upon the predator mercantilism, the {second.Name} are rather angry because of the whole affair.", time);
                RelationsManager.ChangeRelations(first, second, -2.5f);
                first.AddGold(700);
                break;
            case 20:
                State.GameManager.CreateMessageBox($"A number of prey rights activist living in the {second.Name} were, of course, eaten today by counter protestors from the {first.Name}. The predators were spared the devour penalty and released back to their nation of origin in remembrance of those activist gurgling away in their guts, but the whole situation has left the {second.Name} displeased.", time);
                RelationsManager.ChangeRelations(first, second, -1);
                break;
            case 21:
                State.GameManager.CreateMessageBox($"A new trend has taken hold of young people within the {first.Name}. Apparently they have started to wildly fetishize the {second.Name} people. While initially flattered and intrigued by the infatuation it soon turned to the strange when their suitors started to attempt to eat them. Apparently people from the {second.Name} make both great lovers and meals.", time);
                RelationsManager.ChangeRelations(first, second, -1);
                RelationsManager.ChangeRelations(second, first, 1);
                break;
            case 22:
                State.GameManager.CreateMessageBox($"A lawmaker from the {first.Name} recently attempted to implement a vore ban within their nation. The policy brought about immediate upheaval and several rebel groups rose up. The lawmaker themselves were fittingly devoured by a political opponent and the ban repealed but the damage was already done. Now rebels roam the country side looking to stuff their bellies with innocent citizens.", time);
                {
                    Village village = GetRandomVillage(first.Side);
                    CreateBandits(RebelDifficulty.Easy, village, village.Race);
                }
                {
                    Village village = GetRandomVillage(first.Side);
                    CreateBandits(RebelDifficulty.Easy, village, village.Race);
                }
                break;
            case 23:
                State.GameManager.CreateMessageBox($"Several youths from the {second.Name} went missing, prompting a search for them. Sadly it was found that the ones who stole them were instructors at the {first.Name} military academy wishing to introduce their students to vore before putting them on the battlefield. The students gained experience and a few extra pounds at the expense of the {second.Name} people.", time);
                RelationsManager.ChangeRelations(first, second, -1);
                break;
            case 24:
                State.GameManager.CreateMessageBox($"A plot formulated by the {first.Name} has been uncovered too late! Intentionally starving a number of refugees attempting to enter their nation, they waited for their hunger to become extreme, armed them, and then sent them towards the {second.Name}. Now bandits, starving and armed are roaming the countryside.", time);
                {
                    Village village = GetRandomVillage(second.Side);
                    RelationsManager.ChangeRelations(first, second, -2);
                    CreateBandits(RebelDifficulty.Medium, village, village.Race);
                }
                break;
            case 25:
                State.GameManager.CreateMessageBox($"An ideology of equity between all peoples originating from philosophers within {second.Name} intellectual circles recently found its way into the educated within the {first.Name}. They attempted to mount an insurrection but were quickly suppressed and both their ideology and bodies were consumed by the authorities. The {first.Name} state and {second.Name} intellectuals are both furious at one another.", time);
                RelationsManager.ChangeRelations(first, second, -1);
                RelationsManager.ChangeRelations(second, first, -1);
                break;
            case 26:
                State.GameManager.CreateMessageBox($"A wealthy landholder’s daughter from the {second.Name} recently claimed sanctuary within the {first.Name} after devouring her own mother. A tense standoff has arisen on the border as the {second.Name} demands to be allowed to send troops to apprehend the criminal.", time);
                RelationsManager.ChangeRelations(first, second, -1);
                RelationsManager.ChangeRelations(second, first, -1);
                break;
            case 27:
                State.GameManager.CreateMessageBox($"A rebel group claiming to serve their god “The Patriarch of Prey” has risen up within {first.Name} borders and is attempting to liberate those they view as oppressed. {second.Name} calls for solidarity against this radical group and calls for all empires to crush such insurrectionist.", time);
                RelationsManager.ChangeRelations(first, second, 2);
                RelationsManager.ChangeRelations(second, first, 2);
                {
                    Village village = GetRandomVillage(first.Side);
                    CreateBandits(RebelDifficulty.Easy, village, village.Race);
                }
                {
                    Village village = GetRandomVillage(first.Side);
                    CreateBandits(RebelDifficulty.Easy, village, village.Race);
                }
                break;
            case 28:
                State.GameManager.CreateMessageBox($"A famous singer from the {first.Name} has recently begun a tour within the {second.Name} despite the differences between the two nations. Amazingly, the star wasn’t eaten on the tour and the {second.Name} people now have more of a taste for {first.Name} music rather than their flesh.", time);
                RelationsManager.ChangeRelations(first, second, 1);
                RelationsManager.ChangeRelations(second, first, 1);
                break;
            case 29:
                State.GameManager.CreateMessageBox($"{second.Name} settlers setting up near the {first.Name} border went missing recently. The {second.Name} accuses the {first.Name} of devouring their people but their border guard refuses whole heartedly, saying they’d never go so low as to eat something so disgusting.", time);
                RelationsManager.ChangeRelations(first, second, -1);
                RelationsManager.ChangeRelations(second, first, -1);
                break;
            case 30:
                {
                    Village village = GetRandomVillage(first.Side);
                    State.GameManager.CreateMessageBox($"A group of our scouts have reported that the {first.Name} have heavily fortified their defences at {village.Name}, we should take caution if we are to attack this city.", time);
                    var garrison = village.PrepareAndReturnGarrison();
                    foreach (var unit in garrison)
                    {
                        GiveExp(unit, 130, 0.05f);
                    }
                }
                break;
            case 31:
                {
                    Village village1 = GetRandomVillage(first.Side);
                    Village village2 = GetRandomVillage(second.Side);
                    State.GameManager.CreateMessageBox($"Our spies report that escalated tensions between the {second.Name} and the {first.Name} have resulted in them investing in their defences at {village1.Name} and {village2.Name}, this could be problematic if we are planning an invasion on either location.", time);
                    var garrison1 = village1.PrepareAndReturnGarrison();
                    foreach (var unit in garrison1)
                    {
                        GiveExp(unit, 100, 0.05f);
                    }
                    var garrison2 = village2.PrepareAndReturnGarrison();
                    foreach (var unit in garrison2)
                    {
                        GiveExp(unit, 100, 0.05f);
                    }
                }
                break;
        }
    }

    void GiveExp(Unit unit, int baseXP, float pct)
    {
        if (unit != null)
            unit.GiveExp(baseXP + (int)unit.Experience * pct);
    }

    enum RebelDifficulty
    {
        Easy,
        Medium,
        Hard
    }

    void CreateBandits(RebelDifficulty diff, Village village, Race race)
    {
        int count = 6;
        switch (diff)
        {
            case RebelDifficulty.Easy:
                count = village.MaxGarrisonSize / 4 + State.Rand.Next(village.MaxGarrisonSize / 4);
                break;
            case RebelDifficulty.Medium:
                count = village.MaxGarrisonSize / 2 + State.Rand.Next(village.MaxGarrisonSize / 2);
                break;
            case RebelDifficulty.Hard:
                count = village.MaxGarrisonSize - village.MaxGarrisonSize / 4 + State.Rand.Next(village.MaxGarrisonSize - village.MaxGarrisonSize / 4);
                break;
        }
        if (count == 0)
            return;
        if (count >= village.Empire.MaxArmySize)
            count = village.Empire.MaxArmySize;
        Empire banditEmp = State.World.GetEmpireOfSide(701);
        if (banditEmp == null)
        {
            Debug.Log("Bandit empire doesn't exist");
            return;
        }

        Vec2 loc = new Vec2(0, 0);
        CheckTile(village.Position + new Vec2(-1, 0));
        CheckTile(village.Position + new Vec2(0, 1));
        CheckTile(village.Position + new Vec2(1, 0));
        CheckTile(village.Position + new Vec2(-1, -1));
        CheckTile(village.Position + new Vec2(-1, 1));
        CheckTile(village.Position + new Vec2(0, -1));
        CheckTile(village.Position + new Vec2(1, -1));
        CheckTile(village.Position + new Vec2(1, 1));

        int highestExp = State.GameManager.StrategyMode.ScaledExp;
        int baseXp = (int)(highestExp * 40 / 100);

        var army = new Army(banditEmp, new Vec2i(loc.x, loc.y), banditEmp.Side);
        banditEmp.Armies.Add(army);

        for (int i = 0; i < count; i++)
        {
            Unit unit = new Unit(banditEmp.Side, race, RandXp(baseXp), true);
            if (unit.BestSuitedForRanged())
                unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.CompoundBow);
            else
                unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.Axe);
            army.Units.Add(unit);
        }

        int RandXp(int exp)
        {
            if (exp < 1)
                exp = 1;
            return (int)(exp * .8f) + State.Rand.Next(10 + (int)(exp * .4));
        }
        void CheckTile(Vec2 spot)
        {
            if (StrategicUtilities.IsTileClear(spot) == false)
                return;
            if (loc.x == 0 && loc.y == 0)
                loc = spot;
            else if (State.Rand.Next(4) == 0)
                loc = spot;
        }
    }

    void CreateRebels(RebelDifficulty diff, Village village)
    {
        int count = 6;
        switch (diff)
        {
            case RebelDifficulty.Easy:
                count = village.MaxGarrisonSize / 4 + State.Rand.Next(village.MaxGarrisonSize / 4);
                break;
            case RebelDifficulty.Medium:
                count = village.MaxGarrisonSize / 2 + State.Rand.Next(village.MaxGarrisonSize / 2);
                break;
            case RebelDifficulty.Hard:
                count = village.MaxGarrisonSize - village.MaxGarrisonSize / 4 + State.Rand.Next(village.MaxGarrisonSize - village.MaxGarrisonSize / 4);
                break;
        }
        if (count >= village.Population)
            count = village.Population;
        if (count >= village.Empire.MaxArmySize)
            count = village.Empire.MaxArmySize;
        if (count == 0)
            return;
        Empire rebelEmp = State.World.GetEmpireOfSide(700);
        if (rebelEmp == null)
        {
            Debug.Log("Rebel empire doesn't exist");
            return;
        }
        Vec2 loc = new Vec2(0, 0);
        CheckTile(village.Position + new Vec2(-1, 0));
        CheckTile(village.Position + new Vec2(0, 1));
        CheckTile(village.Position + new Vec2(1, 0));
        CheckTile(village.Position + new Vec2(-1, -1));
        CheckTile(village.Position + new Vec2(-1, 1));
        CheckTile(village.Position + new Vec2(0, -1));
        CheckTile(village.Position + new Vec2(1, -1));
        CheckTile(village.Position + new Vec2(1, 1));

        int highestExp = State.GameManager.StrategyMode.ScaledExp;
        int baseXp = (int)(highestExp * 40 / 100);

        village.SubtractPopulation(count);


        var army = new Army(rebelEmp, new Vec2i(loc.x, loc.y), rebelEmp.Side);
        rebelEmp.Armies.Add(army);

        for (int i = 0; i < count; i++)
        {
            Unit unit = new Unit(rebelEmp.Side, village.Race, RandXp(baseXp), true);
            if (unit.BestSuitedForRanged())
                unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.Bow);
            else
                unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.Mace);
            army.Units.Add(unit);
        }

        int RandXp(int exp)
        {
            if (exp < 1)
                exp = 1;
            return (int)(exp * .8f) + State.Rand.Next(10 + (int)(exp * .4));
        }
        void CheckTile(Vec2 spot)
        {
            if (StrategicUtilities.IsTileClear(spot) == false)
                return;
            if (loc.x == 0 && loc.y == 0)
                loc = spot;
            else if (State.Rand.Next(4) == 0)
                loc = spot;
        }
    }

    Empire CreateFactionlessArmy(Village village, int bannerType, Unit[] units, int distance, string armyName)
    {

        Vec2 loc = new Vec2(0, 0);
        CheckTile(village.Position + new Vec2(-distance, 0));
        CheckTile(village.Position + new Vec2(0, distance));
        CheckTile(village.Position + new Vec2(distance, 0));
        CheckTile(village.Position + new Vec2(-distance, -distance));
        CheckTile(village.Position + new Vec2(-distance, distance));
        CheckTile(village.Position + new Vec2(0, -distance));
        CheckTile(village.Position + new Vec2(distance, -distance));
        CheckTile(village.Position + new Vec2(distance, distance));

        if (loc.x == 0 && loc.y == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                loc.x = State.Rand.Next(Config.StrategicWorldSizeX);
                loc.y = State.Rand.Next(Config.StrategicWorldSizeY);

                if (StrategicUtilities.IsTileClear(loc))
                    break;

            }
            Debug.Log("Could not place army");
            return null;
        }
        void CheckTile(Vec2 spot)
        {
            if (StrategicUtilities.IsTileClear(spot) == false)
                return;
            if (loc.x == 0 && loc.y == 0)
                loc = spot;
            else if (State.Rand.Next(4) == 0)
                loc = spot;
        }

        int unusedSide = 702;
        while (State.World.AllActiveEmpires.Any(emp => emp.Side == unusedSide))
        {
            unusedSide++;
        }
        Empire pseudoEmp = new MonsterEmpire(new Empire.ConstructionArgs(unusedSide, UnityEngine.Color.white, UnityEngine.Color.white, bannerType, StrategyAIType.Monster, TacticalAIType.Full, 2000 + unusedSide, 32, 0));
        Army army = new Army(pseudoEmp, new Vec2i(loc.x, loc.y), unusedSide);
        army.Units = units.ToList();
        army.Name = armyName;
        pseudoEmp.ReplacedRace = army.Units[0].Race;
        pseudoEmp.Armies.Add(army);
        pseudoEmp.TurnOrder = 1337;
        Config.World.SpawnerInfo[(Race)unusedSide] = new SpawnerInfo(true, 1, 0, 0.4f, pseudoEmp.Team, 0, false, 9999, 2, pseudoEmp.MaxArmySize, pseudoEmp.TurnOrder, false);
        State.World.AllActiveEmpires.Add(pseudoEmp);
        State.World.RefreshTurnOrder();
        return pseudoEmp;
    }

    void ChangeAllVillageHappiness(Empire empire, int value)
    {
        var villages = State.World.Villages.Where(s => s.Side == empire.Side);
        foreach (Village vill in villages)
        {
            vill.Happiness += value;
        }
    }

    Empire GetRandomHostileEmpire(Empire empire)
    {
        var hostileEmpires = State.World.MainEmpires.Where(s => s.IsEnemy(empire) && s.Side < 100 && s.VillageCount > 0).ToArray();
        if (hostileEmpires.Count() == 0)
            return null;
        return hostileEmpires[State.Rand.Next(hostileEmpires.Count())];
    }

    Empire GetRandomAlliedEmpire(Empire empire)
    {
        var alliedEmpires = State.World.MainEmpires.Where(s => s.IsAlly(empire) && s.VillageCount > 0 && s.Side != empire.Side && s.Side < 100).ToArray();
        if (alliedEmpires.Count() == 0)
            return null;
        return alliedEmpires[State.Rand.Next(alliedEmpires.Count())];
    }

    Empire[] GetTwoRandomEmpires(Empire empire)
    {
        var hostileEmpires = State.World.MainEmpires.Where(s => s.VillageCount > 0 && s.Side != empire.Side && s.Side < 100).ToArray();
        if (hostileEmpires.Length <= 1)
            return null;
        int first = State.Rand.Next(hostileEmpires.Length);
        int second = first;
        while (second == first)
        {
            second = State.Rand.Next(hostileEmpires.Length);
        }
        Empire[] ret = new Empire[2] { hostileEmpires[first], hostileEmpires[second] };
        return ret;
    }

    Empire[] GetTwoRandomAIEmpires()
    {
        var hostileEmpires = State.World.MainEmpires.Where(s => s.VillageCount > 0 && s.StrategicAI != null && s.Side < 100).ToArray();
        if (hostileEmpires.Length <= 1)
            return null;
        int first = State.Rand.Next(hostileEmpires.Length);
        int second = first;
        while (second == first)
        {
            second = State.Rand.Next(hostileEmpires.Length);
        }
        Empire[] ret = new Empire[2] { hostileEmpires[first], hostileEmpires[second] };
        return ret;
    }



    Empire GetRandomEmpire(Empire empire)
    {
        var empires = State.World.MainEmpires.Where(s => s.VillageCount > 0 && s.Side != empire.Side && s.Side < 100).ToArray();
        if (empires.Length == 0)
            return null;
        return empires[State.Rand.Next(empires.Length)];
    }

    Village GetRandomConqueredVillage(int side, Race homeRace)
    {
        var villages = State.World.Villages.Where(s => s.Side == side && s.Race != homeRace).ToArray();
        if (villages.Length == 0)
            return null;
        return villages[State.Rand.Next(villages.Length)];
    }

    Village GetRandomVillage(int side)
    {
        var villages = State.World.Villages.Where(s => s.Side == side).ToArray();
        if (villages.Length == 0)
            return null;
        return villages[State.Rand.Next(villages.Length)];
    }

    Village[] GetTwoRandomVillages(int side)
    {
        var villages = State.World.Villages.Where(s => s.Side == side).ToArray();
        if (villages.Length <= 1)
            return null;
        int first = State.Rand.Next(villages.Length);
        int second = first;
        while (second == first)
        {
            second = State.Rand.Next(villages.Length);
        }
        Village[] ret = new Village[2] { villages[first], villages[second] };
        return ret;
    }


}
