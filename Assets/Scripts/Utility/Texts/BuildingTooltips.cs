static class BuildingTooltips
{
    internal static string Tooltip(int tooltip)
    {
        switch (tooltip)
        {
            case 1:
                return "Enables building button and allows the AI to construct enabled buildings. Does not disable buildings placed in the map editor.";
            case 2:
                return "Prevents buildings from being built until this turn. Buildings already placed on the map can still be used and upgraded.";
            case 3:
                return "Affects the range of some buildings effects. Recommended: 2-4.";
            case 4:
                return "The cost in Wood required to construct this building.";
            case 5:
                return "The cost in Stone required to construct this building.";
            case 6:
                return "The cost in Natural Materials required to construct this building.";
            case 7:
                return "The cost in Ores required to construct this building.";
            case 8:
                return "The cost in Prefabs required to construct this building.";
            case 9:
                return "The cost in Mana Stones required to construct this building.";
            case 10:
                return "The cost in Gold required to construct this building.";
            case 11:
                return "How many strategic turns it takes to construct this building.";
            case 12:
                return "The maximum number of buildings of this type an empire can construct.";
            case 13:
                return "Determines if the AI can construct this building.";
            case 14:
                return "Gold the Work Camp stores per turn, stacks. The Work camp won't purchase materials if it is out of gold.";
            case 15:
                return "The price of Wood.";
            case 16:
                return "The price of Stone.";
            case 17:
                return "The price of Natural Materials.";
            case 18:
                return "The price of Ores";
            case 19:
                return "The price of Prefabs.";
            case 20:
                return "The price of Mana Stones";
            case 21:
                return "The stock of Wood available per turn.";
            case 22:
                return "The stock of Stone available per turn.";
            case 23:
                return "The stock of Natural Materials available per turn.";
            case 24:
                return "The stock of Ores available per turn.";
            case 25:
                return "The stock of Prefabs available per turn.";
            case 26:
                return "The stock of Mana Stones available per turn.";
            case 27:
                return "The maximum workers an unupgraded Lumber Site has.";
            case 28:
                return "Minimum for Stone used in standard rolls. Action plan values are effected by this setting.";
            case 29:
                return "Maximum for Stone used in standard rolls. Action plan values are effected by this setting.";
            case 30:
                return "Minimum for Ores used in standard rolls. Action plan values are effected by this setting.";
            case 31:
                return "Maximum for Ores used in standard rolls. Action plan values are effected by this setting.";
            case 32:
                return "Minimum for Mana Stones used in standard rolls. Action plan values are effected by this setting.";
            case 33:
                return "Maximum for Mana Stones used in standard rolls. Action plan values are effected by this setting.";
            case 34:
                return "Minimum for Gold used in standard rolls. Action plan values are effected by this setting.";
            case 35:
                return "Minimum for Gold used in standard rolls. Action plan values are effected by this setting.";
            case 36:
                return "Maximum Mana Charges.";
            case 37:
                return "Mana charge regenerated per strategic turn.";
            case 38:
                return "Mana Charge cost of Basic Spells, per spell";
            case 39:
                return "Mana Charge cost of Advanced Spells, per spell";
            case 40:
                return "Mana Charge cost of Buffing Spells, per spell";
            case 41:
                return "Value of the barrier granted. \n Note: This value can be multiplied by up to five times depending on Barrier tower settings.";
            case 42:
                return "Toggles if Barrier Towers should ignore the cooldown mechanic.";
            case 43:
                return "How much an attacking army's unit count affects the count of summoned defensive units. \n Example: 10 Attackers at 0.5 will summon 5 defenders.";
            case 44:
                return "How well summoned units scale with the leader of their empire. \nExample: A level 10 leader at 0.5 will summon level 5 defenders.";
            case 45:
                return "How much an empires Max Garrison size affects the maximum number of units a Defense Camp can hold. \nExample: A garrison cap of 16 at 0.5 will result in a max of 8.";
            case 46:
                return "How many strategic turns it takes to train a single unit. \nNote: Surviving summoned units will be returned to the Defense Camp.";
            case 47:
                return "How much EXP stored per one gold spent. \n Note an Academy can use up to 100 income, 200 with upgrades.";
            case 48:
                return "How many times a single upgrade can be stacked.";
            case 49:
                return "The base cost of a single Academy upgrade.";
            case 50:
                return "The multiplier used to increase the amount of EXP to purchase the next upgrade.\nExample: A base of 1000 at 1.5 will require 1500 for the second upgrade and 2250 for the third.";
            case 51:
                return "How much the Duration Improvements increase the duration of their affliction. In Tactical turns.";
            case 52:
                return "How much more accurate afflictions become when they gain their improvemnt. \nNote: Without upgrades chance is 5/20. 20 is 100%";
            case 53:
                return "The amount of soul points required to gain a level.";
            case 54:
                return "Multiplies how many soul points are required per level.";
            case 55:
                return "Upfront cost to begin brewing a potion.";
            case 56:
                return "Basline cost of a potion. \nNote: This value is modifed by selected ingredients.";
            case 57:
                return "Maximum discount for brewing many potions at the same time. The value of discount is based on the below values.";
            case 58:
                return "Minimum value for any amount of discount to come into effect.";
            case 59:
                return "Brewing this many or higher potions will grant the maximum value of discount.";
            case 60:
                return "Maximum number of ingredients that can be selected for a potion.";
            case 61:
                return "The chance an ingredient effect will become a trait. \nExample: a value of 0.65 will cause 65% of effects to be traits and the remaining 35% to be stats.";
            case 62:
                return "Maximum capacity a Teleporter can hold at one time. \nNote: this value can be a decimal.";
            case 63:
                return "How much capacity a Teleporter regenerates per strategic turn.";
            case 64:
                return "Determines if an army should use its units deployment cost to determine capacity use, or just one capacity.";
            case 65:
                return "The multiplier converting a unit's deployment cost to capacity.\nExample: A value of 0.1 will cause an army of 15 units with 1 deployment cost to require 1.5 capacity.";
            case 66:
                return "Decides what happens to a building when an enemy empire army moves onto it.\nNothing - Nothing will happen and AI will ignore buildings\nCapture - Building will switch ownership\nDisable - Building will be disabled and unable to activate passive or active effects until an ally army starts the turn on it\nDestroy - Building will be removed and have it's";
           case 67:
                return "Decides what happens to a building when an monster army moves onto it.\nNothing - Nothing will happen and AI will ignore buildings\nCapture - Building will switch ownership\nDisable - Building will be disabled and unable to activate passive or active effects until an ally army starts the turn on it\nDestroy - Building will be removed and have it's";
           case 68:
                return "Determines how many consecutive turns an army needs to stay ontop of an enemy building to trigger the abore effect.\nThis is counted at the start of the building owner's turn.\n A value of 0 will trigger the effect immediately.";
            default:
                return "";
        }
    }
}
