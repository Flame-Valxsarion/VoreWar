using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoveringTooltip : MonoBehaviour
{
    TextMeshProUGUI text;
    RectTransform rect;
    int remainingFrames = 0;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (remainingFrames > 0)
            remainingFrames--;
        else
            gameObject.SetActive(false);
    }

    public void UpdateInformationTraitsOnly(string[] words)
    {
        string description = GetTraitDescription(words);
        InfoUpdate(description);
    }

    public void UpdateInformationSpellsOnly(string[] words)
    {
        string description = GetSpellDescription(words);
        InfoUpdate(description);
    }

    public void UpdateInformationAIOnly(string[] words)
    {
        string description = GetAIDescription(words);
        InfoUpdate(description);
    }

    public void UpdateInformation(string[] words, Unit unit, Actor_Unit actor)
    {
        string description = GetDescription(words, unit, actor);
        InfoUpdate(description);
    }

    public void UpdateInformation(Slider slider)
    {
        //rect.sizeDelta = new Vector2(350, 80);
        string description = $"Slider Value: {Math.Round(slider.value, 3)}\nRight Click to type in the number.";
        InfoUpdate(description);
    }

    public void HoveringValidName()
    {
        string description = "Click to show the information for this consumed unit";
        InfoUpdate(description);
    }

    string GetTraitDescription(string[] words)
    {
        if (Enum.TryParse(words[2], out Traits trait))
        {
            return GetTraitData(trait);
        }
        if (State.CustomTraitList.Where(t=>t.name == words[2]).Any())
        {
            return GetTraitData((Traits)State.CustomTraitList.Where(t => t.name == words[2]).First().id);
        }
        if (State.ConditionalTraitList.Where(t=>t.name == words[2]).Any())
        {
            return GetTraitData((Traits)State.ConditionalTraitList.Where(t => t.name == words[2]).First().id);
        }
        return "";
    }

    string GetSpellDescription(string[] words)
    {
        if (Enum.TryParse(words[2], out SpellTypes spell))
        {
            List<Spell> AllSpells = SpellList.SpellDict.Select(s => s.Value).ToList();
            string complete = $"{words[0]} {words[1]} {words[2]} {words[3]} {words[4]}";
            for (int i = 0; i < AllSpells.Count; i++)
            {
                if (words[2] == AllSpells[i].SpellType.ToString() || (complete.Contains(AllSpells[i].SpellType.ToString()) && AllSpells[i].SpellType.ToString().Contains(words[2]))) //Ensures that the phrase contains the highlighed word
                {
                    return $"{AllSpells[i].Description}\nRange: {AllSpells[i].Range.Min}-{AllSpells[i].Range.Max}\nMana Cost: {AllSpells[i].ManaCost}\nTargets: {string.Join(", ", AllSpells[i].AcceptibleTargets)}";
                }
            }
        }
        return "";
    }

    string GetAIDescription(string[] words)
    {
        if (Enum.TryParse(words[2], out RaceAI ai))
        {
            return GetAIData(ai);
        }
        return "";
    }

    string GetDescription(string[] words, Unit unit, Actor_Unit actor = null)
    {
        if (int.TryParse(words[2], out int temp))
        {
            return "";
        }
        string STRDef = $"Affects melee accuracy and damage, also has a lesser impact on health, has minor effects on vore defense and vore escape\n{StatData(Stat.Strength)}";
        string DEXDef = $"Affects ranged accuracy and damage, has minor effect on vore escape\n{StatData(Stat.Dexterity)}";
        string VORDef = $"Affects vore odds, also has a minor effect on keeping prey down, also affects digestion damage to a minor degree\n{StatData(Stat.Voracity)}";
        string AGIDef = $"Affects melee and ranged evasion and movement speed\n{StatData(Stat.Agility)}\nMovement: {actor?.MaxMovement() ?? Mathf.Max(3 + ((int)Mathf.Pow(unit.GetStat(Stat.Agility) / 4, .8f)), 1)} tiles";
        string WLLDef = $"Affects vore defense, escape rate, mana capacity, and magic defense\n{StatData(Stat.Will)}";
        string MNDDef = $"Affects spell damage, success odds, and duration with a minor amount of mana capacity\n{StatData(Stat.Mind)}";
        string ENDDef = $"Affects total health, also reduces damage from acid, has a minor role in escape chance.\n{StatData(Stat.Endurance)}";
        string STMDef = $"Affects stomach capacity and digestion rate.  Also helps keep prey from escaping.\n{StatData(Stat.Stomach)}\n" +
                     (State.World?.ItemRepository == null ? $"" : $"{(!unit.Predator ? "" : $"Capacity: {(actor?.PredatorComponent != null ? $"{Math.Round(actor.PredatorComponent.GetBulkOfPrey(), 2)} / " : "")}{Math.Round(State.RaceSettings.GetStomachSize(unit.Race) * (unit.GetStat(Stat.Stomach) / 12f * unit.TraitBoosts.CapacityMult), 1)}")}");
        string LDRDef = $"Provides a stat boost for all friendly units\nStat value: {unit.GetStatBase(Stat.Leadership)}";
        if (Enum.TryParse(words[2], out Stat stat) && unit != null)
        {
            switch (stat)
            {
                case Stat.Strength:
                    return STRDef;
                case Stat.Dexterity:
                    return DEXDef;
                case Stat.Voracity:
                    return VORDef;
                case Stat.Agility:
                    return AGIDef;
                case Stat.Will:
                    return WLLDef;
                case Stat.Mind:
                    return MNDDef;
                case Stat.Endurance:
                    return ENDDef;
                case Stat.Stomach:
                    return STMDef;
                case Stat.Leadership:
                    return LDRDef;
            }
        }
        if (Enum.TryParse(words[2], out Race race))
        {
            if (unit == null) //Protector for the add a race screen
                return "";
            var racePar = RaceParameters.GetTraitData(unit);
            var bodySize = State.RaceSettings.GetBodySize(race);
            var stomachSize = State.RaceSettings.GetStomachSize(race);
            //return $"{race}\n{racePar.RaceDescription}\nBody Size: {State.RaceSettings.GetBodySize(race)}\nBase Stomach Size: {State.RaceSettings.GetStomachSize(race)}\nFavored Stat: {racePar.FavoredStat}\nDefault Traits:\n{State.RaceSettings.ListTraits(race)}";
            return $"{race}\n{racePar.RaceDescription}\nRace Body Size: {bodySize}\nCurrent Bulk: {actor?.Bulk()}\nBase Stomach Size: {stomachSize}\nFavored Stat: {State.RaceSettings.GetFavoredStat(race)}\nDeployment Cost: {State.RaceSettings.GetDeployCost(race) * unit.TraitBoosts.DeployCostMult}\nUpkeep: {State.RaceSettings.GetUpkeep(race) * unit.TraitBoosts.UpkeepMult}";
        }

        if (unit != null && words[2] == InfoPanel.RaceSingular(unit))
        {
            race = unit.Race;
            var racePar = RaceParameters.GetTraitData(unit);
            var bodySize = State.RaceSettings.GetBodySize(race);
            var stomachSize = State.RaceSettings.GetStomachSize(race);
            //return $"{race}\n{racePar.RaceDescription}\nBody Size: {State.RaceSettings.GetBodySize(race)}\nBase Stomach Size: {State.RaceSettings.GetStomachSize(race)}\nFavored Stat: {racePar.FavoredStat}\nDefault Traits:\n{State.RaceSettings.ListTraits(race)}";
            return $"{race}\n{racePar.RaceDescription}\nRace Body Size: {bodySize}\nCurrent Bulk: {actor?.Bulk()}\nBase Stomach Size: {stomachSize}\nFavored Stat: {State.RaceSettings.GetFavoredStat(race)}\nDeployment Cost: {State.RaceSettings.GetDeployCost(race) * unit.TraitBoosts.DeployCostMult}\nUpkeep: {State.RaceSettings.GetUpkeep(race) * unit.TraitBoosts.UpkeepMult}";
        }

        if (Enum.TryParse(words[2], out Traits trait))
        {
            return GetTraitData(trait);
        }
        if (Enum.TryParse(words[2], out UnitType unitType))
        {
            switch (unitType)
            {
                case UnitType.Soldier:
                    return "A generic soldier, does anything and everything they are tasked with";
                case UnitType.Leader:
                    return "The leader of an empire, inspires friendly troops through leadership";
                case UnitType.Mercenary:
                    return "A hired mercenary from the mercenary camp";
                case UnitType.Summon:
                    return "A summoned unit.  It will vanish at the end of the battle";
                case UnitType.SpecialMercenary:
                    return "A unique mercenary, only one of each can exist in the world at once, can not retreat and will return to the merc camp if dismissed";
                case UnitType.Adventurer:
                    return "An adventurer, recruited not from the village population, but from an inn";
                case UnitType.Spawn:
                    return "A weaker unit, created under certain conditions";
                case UnitType.Boss:
                    return "One of the most fierce beings found throughout the realm.  Are you sure you're prepared?";
            }
        }



        if (Enum.TryParse(words[2], out StatusEffectType effectType))
        {
            var effect = unit.GetLongestStatusEffect(effectType);
            if (effect != null)
            {
                switch (effectType)
                {
                    case StatusEffectType.Shielded:
                        return $"(Spell) Unit has resistance against incoming damage\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Mending:
                        return $"(Spell) Unit heals a medium amount every turn.\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Fast:
                        return $"(Spell) Unit moves faster\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Valor:
                        return $"(Spell) Unit does additional damage\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Predation:
                        return $"(Spell) Unit has increased voracity and stomach\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Poisoned:
                        return $"(Spell) Unit is taking damage over time\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.WillingPrey:
                        return $"(Spell) Unit wants to be prey (is easier to eat, and less likely to escape)\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Diminished:
                        return $"(Spell) Unit is tiny, with decreased stats and easy to eat\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Enlarged:
                        return $"(Spell) Unit has increased size and stats\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Clumsiness:
                        return $"Unit is easier to hit\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Empowered:
                        return $"Unit's stats are temporarily boosted\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Shaken:
                        return $"Unit's stats are temporarily lowered\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Snared:
                        return $"Unit only gets 1 AP per turn\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Webbed:
                        return $"Unit only gets 1 AP per turn, and stats are temporarily lowered\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Petrify:
                        return $"Unit cannot perform any actions, but is easy to hit, takes half damage from attacks and is bulky to eat.\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Frozen:
                        return $"Unit cannot perform any actions, but is easy to hit, takes half damage from attacks and is slightly bulky to eat.\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Static:
                        return $"Unit receives more electric damage from attacks (+50%).\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Berserk:
                        return $"Unit is berserk, its strength and voracity are greatly increased for a brief period\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Charmed:
                        return $"Unit fights for the unit that charmed it.";
                    case StatusEffectType.Sleeping:
                        return $"Unit is fast asleep and cannot perform any actions, are easy to hit and eat, and can't struggle.\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Focus:
                        return $"Unit has its mind increased by {effect.Duration} + {effect.Duration}%. Lose 3 stacks when hit by an attack.";
                    case StatusEffectType.SpellForce:
                        return $"Unit has its mind increased by {effect.Duration} + {effect.Duration * 10}%, but its mana costs are increased by {effect.Duration * 10}%.";
                    case StatusEffectType.Staggering:
                        return $"Unit has lost balance, increasing damage taken by 20% and halving MP recovery. 1 stack removed per hit.\nCurrent Stacks: {effect.Duration}";
                    case StatusEffectType.Virus:
                        return $"(Spell) Unit is taking damage over time\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.DivineShield:
                        return $"(Spell) Unit was embraced by a divine being, providing damage mitigation\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Weakness:
                        return $"Unit's stats are lowered by {effect.Duration}% for the rest of the battle.";
                    case StatusEffectType.Bolstered:
                        return $"Unit's stats are boosted by {effect.Duration}% for the rest of the battle.";
                    case StatusEffectType.Necrosis:
                        return $"Unit's healing is reduced by {effect.Strength * 25}% \nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Errosion:
                        return $"Unit's weapon taken is increased by {effect.Strength * 20}% and digstion damage taken is increased by {effect.Strength * 50}%\nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Lethargy:
                        return $"Unit's Str,Dex, and Agi are reduced by {(effect.Strength * effect.Duration / 50)*100}% \nTurns Remaining: {effect.Duration}";
                    case StatusEffectType.Agony:
                        return $"Unit takes an additional 35% weapon damage, which is dealt over the duration of the effect.\n Stored damage: {(int)Math.Round(effect.Strength)}\nIncoming damage: {(int)Math.Round(effect.Strength / effect.Duration)}\n Turns Remaining: {effect.Duration}";
                    case StatusEffectType.Fractured:
                        return $"Unit takes 150% damage from all sources until the end of combat.";
                }
            }
        }

        if (State.CustomTraitList.Where(t => t.name == words[2]).Any())
        {
            return GetTraitData((Traits)State.CustomTraitList.Where(t => t.name == words[2]).First().id);
        }
        if (State.ConditionalTraitList.Where(t => t.name == words[2]).Any())
        {
            return GetTraitData((Traits)State.ConditionalTraitList.Where(t => t.name == words[2]).First().id);
        }

        if (State.World?.ItemRepository != null)
        {
            List<Item> AllItems = State.World.ItemRepository.GetAllItems();
            string complete = $"{words[0]} {words[1]} {words[2]} {words[3]} {words[4]}";
            for (int i = 0; i < AllItems.Count; i++)
            {
                if (words[2] == AllItems[i].Name || (complete.Contains(AllItems[i].Name) && AllItems[i].Name.Contains(words[2]))) //Ensures that the phrase contains the highlighed word
                {
                    if (AllItems[i] is Weapon weapon)
                    {
                        return $"{weapon.Description}\nDamage:{weapon.Damage}\nRange:{weapon.Range}\nAccuracy:{weapon.AccuracyModifier}";
                    }
                    if (AllItems[i] is Accessory accessory)
                    {
                        return $"{accessory.Description}"; // \n+{accessory.StatBonus} to {(Stat)accessory.ChangedStat}";
                    }
                    if (AllItems[i] is SpellBook book)
                    {
                        return $"{book.Description}\n{book.DetailedDescription()}";
                    }
                    if (AllItems[i] is Equipment equipment)
                    {
                        return $"{equipment.Description}\n{equipment.DetailedDescription()}";
                    }
                }
            }
        }

        {
            List<Spell> AllSpells = SpellList.SpellDict.Select(s => s.Value).ToList();
            string complete = $"{words[0]} {words[1]} {words[2]} {words[3]} {words[4]}";
            for (int i = 0; i < AllSpells.Count; i++)
            {
                if (words[2] == AllSpells[i].Name || (complete.Contains(AllSpells[i].Name) && AllSpells[i].Name.Contains(words[2]))) //Ensures that the phrase contains the highlighed word
                {
                    return $"{AllSpells[i].Description}\nRange: {AllSpells[i].Range.Min}-{AllSpells[i].Range.Max}\nMana Cost: {AllSpells[i].ManaCost}\nTargets: {string.Join(", ", AllSpells[i].AcceptibleTargets)}";
                }
            }
        }




        switch (words[2])
        {
            case "surrendered":
                return "This unit has surrendered, all units have a 100% chance to eat it, and it only costs 2 AP to eat it.";

            case "Imprinted":
                return $"This unit is imprinted in the village of {unit.SavedVillage.Name}, at level {unit.SavedCopy?.Level ?? 0} with {Math.Round(unit.SavedCopy?.Experience ?? 0)} exp.  " +
                    $"Unit will automatically resurrect there at that power, assuming the village is controlled by friendlies when the unit dies";

            case "STR":
                return STRDef;
            case "DEX":
                return DEXDef;
            case "MND":
                return MNDDef;
            case "WLL":
                return WLLDef;
            case "END":
                return ENDDef;
            case "AGI":
                return AGIDef;
            case "VOR":
                return VORDef;
            case "STM":
                return STMDef;
            case "LDR":
                return LDRDef;

            default:
                return "";
        }



        string StatData(Stat Stat)
        {

            string leader = "";
            int leaderBonus = unit.GetLeaderBonus();
            if (leaderBonus > 0) leader = $"+{leaderBonus} from leader\n";
            string traits = "";
            int traitBonus = unit.GetTraitBonus(Stat);
            if (traitBonus > 0) traits = $"+{traitBonus} from traits\n";
            string effects = "";
            int effectBonus = unit.GetEffectBonus(Stat);
            if (effectBonus > 0) effects = $"+{effectBonus} from effects\n";
            else if (effectBonus < 0) effects = $"{effectBonus} from effects\n";
            return $"{unit.GetStatBase(Stat)} base {Stat}\n{leader}{traits}{effects}Final Stat: {unit.GetStat(Stat)}";
        }


    }

    public static string GetTraitData(Traits trait)
    {
        if (trait >= (Traits)6000)
        {
            var cond = State.ConditionalTraitList.Where(ct => trait == (Traits)ct.id).FirstOrDefault();

            string constructed = "";
            switch (cond.classification)
            {
                case TraitConditionalClassification.Conditional:
                    constructed = $"This trait provides the trait <b>{GetTraitName(cond.associatedTrait)}</b> while the following condition is fulfilled:\n";
                    break;
                case TraitConditionalClassification.Permanent:
                    constructed = $"This trait becomes <b>{GetTraitName(cond.associatedTrait)}</b> if the following condition is fulfilled:\n";
                    break;
                case TraitConditionalClassification.Temporary:
                    constructed = $"This trait provides the trait <b>{GetTraitName(cond.associatedTrait)}</b>. Both this trait and <b>{GetTraitName(cond.associatedTrait)}</b> will be removed if the following condition is unfulfilled:\n";
                    break;
                default:
                    break;
            }
            foreach (var item in cond.OperationBlocks)
            {
                if (item.Key.conditionVariable.Count <= 0)
                {
                    continue;
                }
                TraitCondition leadCondition = item.Key.conditionVariable.First();
                constructed += item.Key.summary;
                if (TraitCondition.Male > leadCondition)
                {
                    switch (item.Key.compareOp)
                    {
                        case TraitConditionCompareOperator.eq:
                            constructed += " = ";
                            break;
                        case TraitConditionCompareOperator.geq:
                            constructed += " >= ";
                            break;
                        case TraitConditionCompareOperator.leq:
                            constructed += " <= ";
                            break;
                        case TraitConditionCompareOperator.none:
                            break;
                        default:
                            break;
                    }
                    constructed += item.Key.compareValue;
                }
                constructed += "\n";
                if (item.Value != TraitConditionLogicalOperator.none)
                {
                    switch (item.Value)
                    {
                        case TraitConditionLogicalOperator.and:
                            constructed += "AND\n";
                            break;
                        case TraitConditionLogicalOperator.or:
                            constructed += "OR\n";
                            break;
                        case TraitConditionLogicalOperator.not:
                            constructed += "AND NOT\n";
                            break;
                        default:
                            break;
                    }
                }
            }
            return constructed;
        }

        if (trait >= (Traits)3000)
        {
            return State.CustomTraitList.Where(ct => trait == (Traits)ct.id).FirstOrDefault().description;
        }
        
        if (trait >= (Traits)1000)
        {
            return "A Random Trait";
        }

        Trait traitClass = TraitList.GetTrait(trait);
        if (traitClass != null)
            return traitClass.Description;

        switch (trait)
        {
            case Traits.Resilient:
                return "Takes less damage from attacks";
            case Traits.FastDigestion:
                return "Unit digests prey faster than normal";
            case Traits.SlowDigestion:
                return "Unit digests prey slower than normal";
            case Traits.Intimidating:
                return "Enemies within 1 tile get a penalty to accuracy against all targets";
            case Traits.AdeptLearner:
                return "All stats are favored, randomly get 1 point in 2 different stats with level up";
            case Traits.SlowBreeder:
                return "Race produces new population at a slower rate than normal";
            case Traits.ProlificBreeder:
                return "Race produces new population at a faster rate than normal";
            case Traits.Flight:
                return "Unit can pass through obstacles and other units in tactical mode.\nMust end turn on solid ground\nIf you try to take an action or end your turn in an invalid place, it will automatically undo your movement";
            case Traits.Pounce:
                return "Unit spends a minimum of 2 AP to jump next to a target that is within 2-4 tiles (if there is space) and melee attack or vore it";
            case Traits.Biter:
                return "A failed vore attack will result in an attack attempt with a weak melee weapon";
            case Traits.Pathfinder:
                return "Passes through all terrain at a movement cost of 1.\nMore than half of the army has to have this trait to have an effect";
            case Traits.AstralCall:
                return "Unit has a chance at the beginning of battle to summon a weaker unit from its race\nThey return to their own dimension after the battle";
            case Traits.TentacleHarassment:
                return "Shifting tentacles distract and harass opponents within 1 tile.\nLowers enemy stats by a small amount  (8%)";
            case Traits.BornToMove:
                return "Experienced at carrying extra weight.\nUnit suffers no defense penalty and no movement penalty from units it is carrying.";
            case Traits.Resourceful:
                return "Unit has an additional item slot";
            case Traits.ForcefulBlow:
                return "Unit will knock enemy units back in melee (straight back or one diagonal to the side).\nIf a unit is blocked in those directions, it will take extra damage";
            case Traits.NimbleClimber:
                return "Unit is a strong climber and can pass through trees unhindered";
            case Traits.Dazzle:
                return "Units attacking this unit have to run a check based on the comparison of the will values, units failing the check will simply end their turn without attacking and suffer a stat penalty until their next turn (chance caps at 20% at 5x will, and is reflected in shown hit odds)";
            case Traits.Charge:
                return "Unit has a signficant boost to movement speed for the first two turns of every battle";
            case Traits.Feral:
                return "Unit can't use weapons or books, but does a considerable amount of melee damage (6 base) (AI will still try to buy stuff for them)";
            case Traits.DualStomach:
                return "Unit has two stomachs, the second digests faster than the first and is harder to escape from (units slowly migrate to the second)";
            case Traits.MadScience:
                return "Allows casting of a random spell for normal mana cost once per battle";
            case Traits.ShunGokuSatsu:
                return "Allows usage of a powerful ability that does attacks and vore.  Can only be used every 3 turns";
            case Traits.Eternal:
                return "(Cheat Trait) - This unit can never die.  If it is killed during a battle, it will be set to full hp and act as though it fled (will rejoin if the army wins, otherwise sets off for the closest town)";
            case Traits.Revenant:
                return "(Cheat Trait) - This unit can never die from weapons or spells, though digestion can kill it permanently.  If it is 'killed' during a battle, it will be set to full hp and act as though it fled (will rejoin if the army wins, otherwise sets off for the closest town) Note that these don't give immunity to digestion conversion unlike the pure eternal trait";
            case Traits.Reformer:
                return "(Cheat Trait) - This unit can never die from being digested, but spells and weapons can kill it permanently.  If it is killed during a battle, it will be set to full hp and act as though it fled (will rejoin if the army wins, otherwise sets off for the closest town) Note that these don't give immunity to digestion conversion unlike the pure eternal trait";
            case Traits.LuckySurvival:
                return "Unit has an 80% chance of acting like an eternal unit on death (coming back to life after the battle), with a 20% chance of dying normally.";
            case Traits.Replaceable:
                return "If the unit dies and its side wins, the unit will be replaced by a rather similar unit from the same race";
            case Traits.Greedy:
                return "The unit will avoid giving up prey at all costs -- will not auto regurgitate friendlies regardless of settings, and the regurgitate command is disabled";
            case Traits.RangedVore:
                return "Unit can perform vore actions at a range of up to 4 tiles, but chance of success drops per tile, but doesn't drop against flying units.";
            case Traits.HeavyPounce:
                return "Pounce does extra melee damage based on the weight of prey, but defense is also lowered for one turn after pouncing based on the weight of prey (requires the pounce trait to be useable)";
            case Traits.Cruel:
                return "Unit can attempt to eat non-surrendered allied units at normal odds";
            case Traits.MetabolicSurge:
                return "Unit gains a burst of power after digesting a victim";
            case Traits.EnthrallingDepths:
                return "Prey is afflicted with the Prey's Curse effect";
            case Traits.FearsomeAppetite:
                return "Consuming a victim frightens nearby allies of the prey, temporarily reducing their stats";
            case Traits.FriendlyStomach:
                return "Can vore friendly units, friendly units that are vored take no digestion damage \nThey do not try to escape, but can be regurgitated or are freed at the end of battle\nHas 100% chance to eat allies, and only costs 2 AP, like eating surrendered units.  May cause battles to not automatically end if used with TheGreatEscape";
            case Traits.TailStrike:
                return "An attack that does less damage, but attacks the tile and the 2 tiles adjacent to it that are within reach";
            case Traits.HealingBelly:
                return "An accessory trait to endosoma that makes friendly prey receive healing each turn.  (Does nothing without the endosoma trait)\n(Cheat Trait)";
            case Traits.Assimilate:
                return "If the unit has less than 5 traits, upon finishing absorption of an enemy unit, will take a random trait from them that the unit doesn't currently have.  If the unit has 5 traits, the random trait will replace this trait. Transferable via Endosoma.\n(Cheat Trait)";
            case Traits.AdaptiveBiology:
                return "Upon finishing absorption of an enemy unit, will take a random trait from them that the unit doesn't currently have and add it to a list of 3 rotating traits.  If the list already has 3 rotating traits, the oldest trait is removed.  This can't trigger on the same kill as Assimilate. Transferable via Endosoma.\n(Cheat Trait)";
            case Traits.KillerKnowledge:
                return "Every four weapon / spell kills (but not vore kills), the unit will get a permanent +1 to all stats\n(Cheat Trait)";
            case Traits.PollenProjector:
                return "Allows using the pollen cloud ability once per battle, which attempts to inflict a few status effects in a 3x3 area for a small mana cost.  This trait also makes the unit immune to most of the statuses from this ability.";
            case Traits.Webber:
                return "Allows using the web ability once per battle, which attempts to inflict the webbed status for 3 turns, which lowers AP to 1, and reduces stats.";
            case Traits.Camaraderie:
                return "Prevents the unit from defecting to rejoin its race if that option is enabled.";
            case Traits.RaceLoyal:
                return "Unit will defect to rejoin its race at every opportunity if that option is enabled.";
            case Traits.WillingRace:
                return "Gives the whole race the willing prey spell effect permanently, which makes them easier to eat, and changes some of the dialogue.";
            case Traits.InfiniteAssimilation:
                return "Upon finishing absorption of an enemy unit, will take a random trait from them that the unit doesn't currently have. This version has no cap, so it can be a little bit of a text mess. Transferable via Endosoma.\n(Cheat Trait)";
            case Traits.GlueBomb:
                return "Gives access to a single use ability that applies the glued status effect to a 3x3 group.  Glued units are very slow, and it takes a while to get it off.";
            case Traits.TasteForBlood:
                return "After digesting or killing someone, the unit will get a random positive buff for 5 turns.";
            case Traits.PleasurableTouch:
                return "This unit's belly rub actions are more effective on others (doubled effect).";
            case Traits.PoisonSpit:
                return "Allows using the poison spit ability once per battle, which does damage in a 3x3 and attempts to apply a strong short term poison as well.  This trait also makes the unit immune to poison damage.";
            case Traits.DigestionConversion:
                return "When a unit finishes digesting someone, there's a 50% chance they will then convert to the predator's side, and be healed to half of their max life.  Can't convert leaders, summons, or units with saved copies.\n(Cheat Trait)";
            case Traits.DigestionRebirth:
                return "When a unit finishes digesting someone, there's a 50% chance they will then convert to the predator's side and change race to the predator's race, and be healed to half of their max life.  Can't convert leaders, summons, or units with saved copies.\n(Cheat Trait)";
            case Traits.SenseWeakness:
                return "Unit does more melee/ranged damage against targets with less health, and also increases for every negative status effect the target has.";
            case Traits.BladeDance:
                return "Unit gains a stack of blade dance every time they successfully hit their opponent with melee, and lose a stack every time they are hit with melee.  Each stack gives +2 strength and +1 agility.";
            case Traits.EssenceAbsorption:
                return "Every four vore digestions, the unit will get a permanent +1 to all stats\n(Cheat Trait)";
            case Traits.AntPheromones:
                return "Unit will summon some friendly ants, depending on how many units have this trait.";
            case Traits.Fearless:
                return "Unit can not flee nor surrender (also applies to auto-surrender).  If something does happen to make the unit surrender, it will automatically recover on the next turn.";
            case Traits.Inedible:
                return "Unit can not be vored by other units.  (It makes their effective size so big that no one has the capacity to eat them)\n(Cheat Trait)";
            case Traits.AllOutFirstStrike:
                return "Unit starts battle in a protected state, with high dodge rate.  On their first attack or vore attempt of the battle, they get a significant bonus to damage or vore chance.  After that they become vulnerable, and move slower and have a dodge penalty.";
            case Traits.VenomousBite:
                return "A missed bite from the biter trait will also poison an enemy, and give them the shaken debuff.";
            case Traits.Petrifier:
                return "Gives access to a single use ability that applies the petrified status effect to a target.  It prevents the target from acting, but also makes them resistant to damage and bulky to swallow.";
            case Traits.VenomShock:
                return "Gives this unit significantly increased melee damage and vore odds against targets who are poisoned.";
            case Traits.Tenacious:
                return "Unit gains a stack of tenacity every time they are hit or miss an attack, and lose five stacks every time they hit or vore an enemy.  Each stack gives +10% str/agi/vor.";
            case Traits.PredConverter:
                return "This unit will always convert unbirthed prey to their side upon full digestion regardless of KuroTenko settings, putting this together with PredRebirther or PredGusher on same unit is not recommended";
            case Traits.PredRebirther:
                return "This unit will always rebirth unbirthed prey as their race upon full absorption regardless of KuroTenko settings, putting this together with PredConverter or PredGusher on same unit is not recommended.";
            case Traits.PredGusher:
                return "This unit will always turn unbirthed units into a sticky puddle, they will not be converted/rebirthed. (Basically cancels out certain game settings or traits)";
            case Traits.SeductiveTouch:
                return "Unit's belly rub action can make enemies pause for a turn, or even switch sides, as long as they haven't taken damage for two turns.\n(Cheat Trait)";
            case Traits.TheGreatEscape:
                return "This unit cannot be digested, but the battle will end if only units with this remain and they're all eaten.  The prey are assumed to escape sometime later, and are count as fled units. (Note that you'd need end of battle review checked to see the escape messages as they happen at the very end of battle)";
            case Traits.Growth:
                return "Each absorbtion makes this unit grow in size, but the effect slowly degrades outside battle.\n(Cheat Trait)";
            case Traits.PermanentGrowth:
                return "An accessory trait to Growth that makes growth gained permanent.  (Does nothing without the Growth trait)\n(Cheat Trait)";
            case Traits.Berserk:
                return "If the unit is reduced below half health by an attack, will go berserk, greatly increasing its strength and voracity for 3 turns.\nCan only occur once per battle.";
            case Traits.SynchronizedEvolution:
                return "Any trait this unit assimilates is received by all members of their race. (requires Assimilate or InfiniteAssimilation)\n(Cheat Trait)";
            case Traits.Charmer:
                return "Allows the casting of the Charm spell once per battle";
            case Traits.HypnoticGas:
                return "Can emit Gas that turns foes into subservient non-combatants that are easy to vore, use buff spells if they have any, and rub bellies. Units of identical alignment are unaffected.";
            case Traits.ForceFeeder:
                return "Allows unit to attempt force-feeding itself to another unit at will.";
            case Traits.Possession:
				return "Temporarily control a Pred unit while digesting inside\n (Cheat Hidded Trait)";    
            case Traits.Corruption:
                return "If a currupted unit is digested, the pred will build up corruption as a hidden status. Once corrupted prey with a stat total equal to that of the pred has been digested, they are under control of the side of the last-digested corrupted.\n(Hidden Trait)";
            case Traits.Reanimator:
                return "Allows unit to use <b>Reanimate</b>, an attack that brings any unit back to life as the caster's summon, once per battle";
            case Traits.Reincarnation:
                return "Soon after this unit dies, one of the new Units that come into being will be a reincarnation of them.\n(Hidden Trait)";
            case Traits.Transmigration:
                return "Soon after this unit is digested, one of the new Units that come into being as the pred's race will be a reincarnation of them.\n(Hidden Trait)";
            case Traits.InfiniteReincarnation:
                return "Soon after this unit dies, one of the new Units that come into being will be a reincarnation of them.\nReincarnations will also have this trait (Hidden Trait)(Cheat Trait)";
            case Traits.InfiniteTransmigration:
                return "Soon after this unit is digested, one of the new Units that come into being as the pred's race will be a reincarnation of them.\nReincarnations will also have this trait (Hidden Trait)(Cheat Trait)";
            case Traits.Untamable:
                return "No matter which army this unit is in, it is only ever truly aligned with its race. Only vore-based types of conversion are really effective\n(Hidden Trait)";
            case Traits.Binder:
                return "Allows unit to either take control of any summon, or re-summon the most recently bound one once a battle.";
            case Traits.Infiltrator:
                return "Armies fully consisting of infiltrators are invisible to the enemy. Using 'Exchange' on an enemy village or a Mercenary camp will infiltrate it (For Player villages, infiltrating as a Mercenary will be preferred, otherwise as recruitables).\nWill also use conventional changes of allignment to go undercover\n(Hidden Trait)";
            case Traits.BookWormI:
                return "Unit generates with a random Tier 1 Book.";
            case Traits.BookWormII:
                return "Unit generates with a random Tier 2 Book.";
            case Traits.BookWormIII:
                return "Unit generates with a random Tier 3-4 Book.";
            case Traits.Temptation:
               return "Units that are put under a mindcontrol (e.g. Charm, Hypnosis) effect by this unit want to force-feed themselves to it or its close allies.";
            case Traits.Infertile:
                return "Unit cannot contribute to village population growth.";
            case Traits.HillImpedence:
                return "Unit treats all hills as impassable.\nMore than half of the army has to have this trait to have an effect";
            case Traits.GrassImpedence:
                return "Unit treats grass as impassable.\nMore than half of the army has to have this trait to have an effect";
            case Traits.MountainWalker:
                return "Unit can cross over mountains and broken cliffs (but not stop on one).\nAt least half of the army has to have this trait to have an effect";
            case Traits.WaterWalker:
                return "Unit can cross over water (but not stop on it).\nAt least half of the army has to have this trait to have an effect";
            case Traits.LavaWalker:
                return "Unit can cross over lava (but not stop on it).\nAt least half of the army has to have this trait to have an effect";
            case Traits.SwampImpedence:
                return "Unit treats swamps as impassable.\nMore than half of the army has to have this trait to have an effect";
            case Traits.ForestImpedence:
                return "Unit treats forests as impassable.\nMore than half of the army has to have this trait to have an effect";
            case Traits.DesertImpedence:
                return "Unit treats deserts and sand hills as impassable.\nMore than half of the army has to have this trait to have an effect";
            case Traits.SnowImpedence:
                return "Unit treats snow and snow hills as impassable.\nMore than half of the army has to have this trait to have an effect";
            case Traits.VolcanicImpedence:
                return "Unit treats volcanic ground as impassable.\nMore than half of the army has to have this trait to have an effect";
            case Traits.Donor:
                return "Upon being absorbed, this unit bestows all traits that are listed below \"Donor\" in its trait list.";
            case Traits.Extraction:
                return "Every time digestion progresses, this unit steals one trait from each prey inside them, if only duplicates (or non-assimilable traits) remain, they are turned into exp. Absorbtion steals any that are left. Endoed units instead gain traits.\n(Cheat Trait)";
            //case Traits.Shapeshifter:
            //    return "Gives the ability to change into different races after acquiring them via absorbing, being reborn, reincarnating, being endoed or infiltrating. Also Allows Traversal of all terrain at normal speed.";
            //case Traits.Skinwalker:
            //    return "Gives the ability to change into specific units after absorbing them or being endoed or infiltrating. Or into the alternate selves acquired by being reborn or reincarnated. Also Allows Traversal of all terrain at normal speed.";
            case Traits.BookEater:
                return "When this unit would equip a book, it is instead consumed and the spell becomes innate. Does not consume already equipped books, but does consume one if the unit would gain more than it could carry via BookWorm.";
            case Traits.Whispers:
                return "When eaten, Predator is afflicted by Prey's curse, and has a chance to be charmed each round";
            case Traits.TraitBorrower:
                return "While digesting, the Predator is able to use prey's normal traits";
            case Traits.Changeling:
                return "While absorbing a prey, Becomes that prey's Race until absorption is complete";
            case Traits.GreaterChangeling:
                return "While digesting a prey, Becomes that prey's Race until absorption";
            case Traits.SpiritPossession:
                return "Units soul continues to possess pred after death";
            case Traits.ForcedMetamorphosis:
                return "Pred Unit will gain the metamorphosis trait on Prey death";
            case Traits.Metamorphosis:
                return "Unit changes Race upon digestion";
            case Traits.MetamorphicConversion:
                return "Unit changes Race and side upon digestion";
            case Traits.Perseverance:
                return "Unit heals after not taking damage for a 3 turns, scaling higer with each turn without damage thereafter.";
            case Traits.ManaAttuned:
                return "Unit thrives on mana, uses 10% of their max mana every turn. Unit falls asleep for 2 turns if they don't have enough mana, but regenerate 50% max mana every turn they are asleep.";
            case Traits.NightEye:
                return "Increases night time vision range by +1 in Tactical battles.";
            case Traits.AccuteDodge:
                return "Unit gains +10% graze chance.";
            case Traits.KeenEye:
                return "Unit gains +10% critical strike chance.";
            case Traits.SpellBlade:
                return "Unit's weapon damage also scales with mind. (Half as effectively as weapons main stat)";
            case Traits.ArcaneMagistrate:
                return "Unit gains 1 focus when it hits a spell, unit gains 4 more if the spell kills the target. Focus: Unit has its mind increased.";
            case Traits.SwiftStrike:
                return "Unit deals up 1% more weapon damage per agility it has over it's target, up to 25%, tripled when using light weapons.";
            case Traits.Timid:
                return "Unit is shaken if the turn ends and there are more enemies than allies are nearby. (This unit counts as it's own ally)";
            case Traits.Cowardly:
                return "Unit has a chance based on it's missing health to surrender.";
            case Traits.TurnCoat:
                return "Unit has a chance based on it's missing health to change sides.";
            case Traits.MagicSynthesis:
                return "When Unit is hit by a damaging magic spell, it gains a single cast of that spell and restores 75% of it's cost.";
            case Traits.ManaBarrier:
                return "Up to 50% of damage taken by unit instead spends mana, this trait loses 1% effectivity for every 1% missing mana percentage.";
            case Traits.Unflinching:
                return "Unit's BladeDance, Tenacity, and Focus stack loss is reduced by if stacks are below 10% current HP.";
            case Traits.Annihilation:
                return "Every time digestion progresses, this unit digests one level from each prey inside them, gaining its experience value. If a unit hits level 0 this way, it dies if it was still alive and cannot be revived.\n(Cheat Trait)";
            case Traits.WeaponChanneler:
                return "Unit deals extra melee or ranged damage at the cost of each strike consuming 6 mana. No bonus is received if mana is under 6.";
            case Traits.Respawner:
                return "Upon getting killed, this unit will be brought back to life within a 6 tile radius of where they were killed once per battle.";
            case Traits.RespawnerIII:
                return "Upon getting killed, this unit will be brought back to life within a 6 tile radius of where they were killed 3 times per battle!";
            case Traits.DeathCheater:
                return "Unit has set chance to return to army after dying in battle regardless of outcome. Chance starts at 100% then decreases 10% with each death, bottoms out at 10%";
            case Traits.Endosoma:
                return "Units that are vored take no digestion damage \n Enemies lose stamina instead of health, enemies with no stamina no longer try to escape and are considered defeated at the end of battle, but not if freed. \n Has 100% chance to eat allies.Can vore friendly units, they do not try to escape. \n May cause battles to not automatically end if used with TheGreatEscape";
            case Traits.Friendosoma:
                return "Enemies defeated by defeated by the Endosoma trait will now be recruited instead at the end of battle."; 
            case Traits.Duelist:
                return "Melee damage is increased by 100%, but Melee damage is divided by the number of adjacent enemy units.";
            case Traits.Fervor:
                return "Melee damage is reduced to 25%, but Melee damage is multiplied by the number of adjacent enemy units.";
            case Traits.Farsighted:
                return "Accuracy is reduced against targets within 5 tiles. Closer targets are even harder to hit";
            case Traits.EasilySatisfied:
                return "Unit has 50% reduced vore chance when it has prey.";
            case Traits.AwfulAim:
                return "Unit's ranged attacks have an even chance to target any unit within 2 spaces of the target.";
            case Traits.Slacker:
                return "Unit's MP regeneration is delayed by one turn after it regenerates MP.";
            case Traits.Juggernaut:
                return "Unit's stats are increased by 100%, but MP regeneration is delayed by one turn after it regenerates MP.";
            case Traits.PoorConstitution:
                return "When attacked in melee, unit has a 10% chance to be afflicted with sleep for 2 turns";
            case Traits.IntrusiveAppetite:
                return "At the start of each turn, this unit has a 10% chance to spend MP and attempt to eat a random adjacent unit.";
            case Traits.ExtraNutritious:
                return "When digested, unit will permanently increase one of predator's stats by one for each of this unit's levels.";
            case Traits.FoodComaProne:
                return "While full, at the start of turn, unit has a chance based on current fullness to fall asleep.";
            case Traits.SleepItOff:
                return "While asleep, unit's digestion damage and absorption rate is doubled. Unit has a chance based on fullness to extend it's own sleep status by a turn.";
            case Traits.HaplessPrey:
                return "Unit has a 10% chance of force-feeding themselves to their melee attack target instead of attacking, if possible. Chance is increased by 5% per difference in level.";
            case Traits.PleasantDigestion:
                return "While being digested, unit will heal it's predator each turn.";
            case Traits.AllIn:
                return "Unit gains the ability to make a vore attempt at increased odds, if it fails their target vores them instead, if possible.";
            case Traits.SiphoningAura:
                return "At the start of turn, Unit applies 1 stack of Weakened to all adjacent allies and Unit gainst 1 stack of Bolstered for every ally afflicted.";
            case Traits.EnviousPrey:
                return "Unit has a 10% chance to gain Temptation each time an ally within 3 spaces is eaten.";
            case Traits.RoughMassage:
                return "When this unit rubs a unit's belly, the effect is doubled and 1 stack of Weakness is applied to the target.";
            case Traits.SlowStart:
                return "For the first 5 turns of battle, unit's MP is reduced by 50%.";
            case Traits.CurseOfImmolation:
                return "At start of turn, Unit deals it's level in fire damage to itself and all units around it or it's predator, if this unit has been consumed. This damage can not kill. Effect does not activate if unit has surrendered.";
            case Traits.CurseOfSacrifice:
                return "When a unit within 3 spaces is consumed, this unit has a 10% chance to trade places with them and be consumed instead.";          
            case Traits.CurseOfEquivalency:
                return "At the start of each turn, this unit's highest stat is reduced by 1 and this unit's lowest stat is increased by 1.";
            case Traits.CurseOfPhasing:
                return "When hit by an attack, unit has a 50% chance to teleport to a random space within 3 spaces. If a unit occupies that space, this unit is consumed by the occupier.";
            case Traits.CurseOfCraving:
                return "At the start of battle, this unit has a 50% chance to have eaten one of it's allies.";
            case Traits.CurseOfPreyportaion:
                return "At the start of battle, this unit has a 25% chance to teleported into a random predator.";       
            case Traits.Competitive:
                return "Unit deals bonus ranged and melee damage to members of the same race.";      
            case Traits.CompetetivePredator:
                return "When an another nearby unit is eaten, this unit has a 10% chance to eat a random adjacent unit.";
            case Traits.PassThrough:
                return "Unit can move past (but not stop on) allied units. Not recommended to use with Blitz or SpectralStep.";
            case Traits.Blitz:
                return "Unit can move past (but not stop on) enemy units. Not recommended to use with PassThrough or SpectralStep.";
            case Traits.SpectralStep:
                return "Unit can move past (but not stop on) any units. Not recommended to use with Blitz or PassThrough.";
            case Traits.VoreObsession:
                return "Prevents the AI from using weapons. AI will still buy weapons.";
            case Traits.InfectiousReproduction:
                return "Upon landing a killing blow on a poisoned target the target will create 1 of this unit's spawn race.";
            case Traits.DireInfection:
                return "A melee attack that debilitates the target for 1 turn reducing movement to 1 and badly poisons them for 6 turns.";
            case Traits.GiantSlayer:
                return Config.SizeDamageMod > 0 && Config.SizeDamageInterval > 0 ? "Bonus damage from larger units is reduced by 75% and Unit's damage against larger targets is not reduced" : "Units damage is increased by 1% for every 1 size larger the target is. (capped at 25)";
            case Traits.Crusher:
                return Config.SizeDamageMod > 0 && Config.SizeDamageInterval > 0 ? "Bonus damage against smaller targets is increased by 50% and unit ignores size bounds." : "Units damage is increased by 1% for every 1 size smaller the target is. (capped at 25)";
            case Traits.Cartography:
                return "This unit provides the army it's in with +1 MP and allows transversal of all terrain at a movement cost of 1";
            case Traits.BoundWeapon:
                return "Unit's weapon damage scales purely with mind stat; ignoring the weapon's main stat in damage calculations";
            case Traits.Finesse:
                return "This unit's melee attacks scale with 80% Strength and 30% Dexterity, instead of with 100% Strength.";
            case Traits.DexterousDefense:
                return "If this unit is wielding a melee weapon, melee attacks against this unit may deal half damage based on attacker's and unit's dexterity score (max 70% block chance). A successful block sets the attacker's MP to 0.";
            case Traits.Eeveeolutionist:
                return "This unit will evolve into a different race based on the current composition of its army at the start of turn if over level 5. This trait is then removed.";
            case Traits.FocusedDodge:
                return "Unit gains 20% increased dodge. This bonus is disabled for 3 turns after taking damage.";
            case Traits.BlessingOfNature:
                return "This unit heals any unit that buffs it, equal to 5% of Endurance every turn while the buff persists. This unit gives 'Mendinig' to an ally within 2 spaces every 4th turn, duration scaling with level.";
            case Traits.BlessingOfEarth:
                return "This unit grants barrier to any unit that buffs it, equal to 10% of Will every turn while the buff persists. This unit gives 'Shield' to an ally within 2 spaces every other turn, duration scaling with level.";
            case Traits.BlessingOfWater:
                return "This unit restores mana to any unit that buffs it, for 10% of Mind every turn while the buff persists. This unit gives 'Focus' to an ally within 2 spaces every other turn, stacks scaling with level.";
            case Traits.BlessingOfFerocity:
                return "This unit grants sharpness to any unit that buffs it, equal to 10% of Strength every turn while the buff persists. This unit gives 'Valor' to an ally within 2 spaces every other turn, duration scaling with level.";
            case Traits.TotalAbsorption:
                return "This unit does not produce waste or remains when digesting absorbing prey.";
            case Traits.Crystaline:
                return "Unit takes 25% reduced damage from all sources, but each time it is hit it has a 1/4 chance to gain the 'Fractured' status effect, canceling out this trait and greatly increasing damage from all sources. 'Fractured' lasts until the battle is over.";
            case Traits.DyingStrike:
                return "When unit is killed in melee or digested does has a 1/3 chance of hiting with a melee attack against the agressor, 3/4 hit chance if vored.";
            case Traits.DimensionalAntilock:
                return "This unit is not completely fixed to the space around it. \n(Allows using the Dimension Shift ability once per battle, which attempts to teleport the User to a random open tile within 20 tiles.)";
        }  
        return "<b>This trait needs a tooltip!</b>";
    }

    private static string GetTraitName(Traits trait)
    {
        if (trait >= (Traits)6000)
        {
            var lis = State.ConditionalTraitList.Where(t => t.id == (int)trait);
            if (lis.Any())
            {
                return lis.First().name;
            }
        }
        if (trait >= (Traits)3000)
        {
            var lis = State.CustomTraitList.Where(t => t.id == (int)trait);
            if (lis.Any())
            {
                return lis.First().name;
            }
        }
        if (trait >= (Traits)1000)
        {
            var lis = State.RandomizeLists.Where(t => t.id == (int)trait);
            if (lis.Any())
            {
                return lis.First().name;
            }
        }
        return trait.ToString();
    }
    public static string GetAIData(RaceAI ai)
    {
        switch (ai)
        {
            case RaceAI.Standard:
                return "Straightforward battlers";
            case RaceAI.Hedonist:
                return "Will try to find the time for massaging any prey-filled parts on their comrades or their own body.\nDon't be fooled – this is deceptively efficient.";
            case RaceAI.ServantRace:
                return "Acts Subservient towards units of the most powerful race on their side, flocking to rub those individuals.\n" +
                    "Racial superiority is based on eminence.";
            //case RaceAI.NonCombatant:
            //    return "Won't use weapons or offensive spells, but supports combatants with beneficial spells and bodily services.";

        }
        return "<b>This AI needs a tooltip!</b>";
    }

    internal void UpdateInformationDefaultTooltip(int value)
    {

        string description = DefaultTooltips.Tooltip(value);
        InfoUpdate(description, true);
    }

    internal void InfoUpdate(string description, bool linger = false)
    {
        if (description == "")
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        remainingFrames = linger ? 999 : 3;
        text.text = description;
        float xAdjust = 10;
        float exceeded = Input.mousePosition.x + (rect.rect.width * Screen.width / 1920) - Screen.width;
        if (exceeded > 0)
            xAdjust = -exceeded;
        transform.position = Input.mousePosition + new Vector3(xAdjust, 0, 0);
    }
}
