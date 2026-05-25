public class TempBoosts
{
    internal int HealthBoost = 0;
    internal int ManaBoost = 0;

    internal int ExtraAttacks = 0;
    

    internal void ResetAll()
    {
        HealthBoost = 0;
        ManaBoost = 0;
        ExtraAttacks = 0;
    }
    internal int ModifyExtraAttacks(int amnt)
    {
        ExtraAttacks += amnt;
        if (ExtraAttacks < 0)
            ExtraAttacks = 0;
        return ExtraAttacks;
    }
}