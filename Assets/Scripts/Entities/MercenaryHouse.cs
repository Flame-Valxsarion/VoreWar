using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MercenaryHouse
{
    [OdinSerialize]
    internal List<MercenaryContainer> Mercenaries;

    static internal List<MercenaryContainer> UniqueMercs;

    [OdinSerialize]
    internal Vec2i Position;

    static List<Race> AvailableRaces;
    static int TurnRefreshed;

    public MercenaryHouse(Vec2i position)
    {
        Position = position;
        Mercenaries = new List<MercenaryContainer>();
    }

    static internal void UpdateStaticStock()
    {
        UniqueMercs = new List<MercenaryContainer>();
        Dictionary<Race, int> raceQuantities = new Dictionary<Race, int>();
        int highestExp = 0;
        if (State.World.Turn == 1)
            highestExp = 4;
        foreach (Race race in ((Race[])System.Enum.GetValues(typeof(Race))).Where(s => (int)s >= 0))
        {
            raceQuantities[race] = 0;
        }
        foreach (Unit unit in StrategicUtilities.GetAllUnits())
        {
            if (raceQuantities.TryGetValue(unit.Race, out int val))
                raceQuantities[unit.Race]++;
            else
                raceQuantities[unit.Race] = 1;
        }
        foreach (Race race in ((Race[])System.Enum.GetValues(typeof(Race))).Where(s => (int)s >= 0))
        {
            if (race < Race.Selicia)
                continue;
            if (Config.World.GetValue($"Merc {race}") == false)
                continue;
            if (raceQuantities[race] < 1)
            {
                if (highestExp == 0) highestExp = State.GameManager.StrategyMode.ScaledExp;
                UniqueMercs.Add(CreateUniqueMercenary(highestExp, race));
            }
        }
    }

    internal void UpdateStock()
    {
        const int minimumReplacedPerTurn = 4;
        const int maxStock = 12;
        if (State.World.Turn != TurnRefreshed)
        {
            TurnRefreshed = State.World.Turn;
            AvailableRaces = new List<Race>();
            foreach (Race race in ((Race[])System.Enum.GetValues(typeof(Race))).Where(s => (int)s >= 0))
            {
                if (race < Race.Selicia && Config.World.GetValue($"Merc {race}"))
                    AvailableRaces.Add(race);
            }
        }
        int highestExp = State.GameManager.StrategyMode.ScaledExp;

        if (Mercenaries.Count > maxStock - minimumReplacedPerTurn)
        {
            List<Unit> units = Mercenaries.ConvertAll(merc => merc.Unit);
            Mercenaries.RemoveRange(0, Mercenaries.Count - (maxStock - minimumReplacedPerTurn));
            foreach (Unit u in units)
            {
                if (!Mercenaries.Any(mer => mer.Unit == u) && u.OnDiscard != null)
                {
                    u.OnDiscard();
                }
            }
        }
        for (int i = 0; i < 20; i++)
        {
            if (Mercenaries.Count < maxStock && AvailableRaces.Count > 0)
            {
                Mercenaries.Add(CreateMercenary(highestExp));
            }
            else
                break;
        }
    }

    internal MercenaryContainer CreateMercenary(int highestExp)
    {
        MercenaryContainer merc = new MercenaryContainer();
        Race race;
        race = AvailableRaces[State.Rand.Next(AvailableRaces.Count())];

        int exp = (int)(highestExp * .8f) + State.Rand.Next(10);
        merc.Unit = new Unit((int)race, race, exp, true, UnitType.Mercenary, true);
        if (race < Race.Vagrants && merc.Unit.FixedGear == false)
        {
            if (merc.Unit.Items[0] == null)
            {
                if (merc.Unit.BestSuitedForRanged())
                    merc.Unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.CompoundBow);
                else
                    merc.Unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.Axe);
            }

            int r = State.Rand.Next(4);
            switch (r)
            {
                case 0:
                    merc.Unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Shoes), 1);
                    break;
                case 1:
                    merc.Unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Helmet), 1);
                    break;
                case 2:
                    merc.Unit.SetItem(State.World.ItemRepository.GetItem(ItemType.BodyArmor), 1);
                    break;
                case 3:
                    if (merc.Unit.BestSuitedForRanged())
                        merc.Unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Gloves), 1);
                    else
                        merc.Unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Gauntlet), 1);
                    break;
            }

            if (State.Rand.Next(10) == 0)
            {
                merc.Unit.AIClass = merc.Unit.BestSuitedForRanged() ? AIClass.MagicRanged : AIClass.MagicMelee;
                merc.Unit.SetItem(State.World.ItemRepository.GetRandomBook(), 1);
            }
        }

        var power = State.RaceSettings.Get(merc.Unit.Race).PowerAdjustment;
        if (power == 0)
        {
            power = RaceParameters.GetRaceTraits(merc.Unit.Race).PowerAdjustment;
        }
        StrategicUtilities.SetAIClass(merc.Unit);
        StrategicUtilities.SpendLevelUps(merc.Unit);
        merc.Cost = (int)((25 + State.Rand.Next(15) + (.12 * exp)) * Random.Range(0.8f, 1.2f) * power);
        merc.Title = $"{InfoPanel.RaceSingular(merc.Unit)} - Mercenary";

        return merc;
    }

    static internal MercenaryContainer CreateUniqueMercenary(int highestExp, Race race)
    {
        MercenaryContainer merc = new MercenaryContainer();

        int exp = (int)(highestExp * .8f);
        merc.Unit = new Unit((int)race, race, exp, true, UnitType.SpecialMercenary, true)
        {
            FixedGear = true
        };

        var power = State.RaceSettings.Get(merc.Unit.Race).PowerAdjustment;
        if (power == 0)
        {
            power = RaceParameters.GetRaceTraits(merc.Unit.Race).PowerAdjustment;
        }
        StrategicUtilities.SetAIClass(merc.Unit);
        StrategicUtilities.SpendLevelUps(merc.Unit);
        merc.Cost = (int)((20 + State.Rand.Next(15) + (.12 * exp)) * power);
        return merc;
    }
}
