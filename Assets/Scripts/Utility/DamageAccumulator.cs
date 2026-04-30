using System;

public static class DamageAccumulator
{
    // I can't get Unity to work with C# union types, so this small class implements the functionality needed.
    private class TargetUnion
    {
        public int type { get; private set; }
        private Unit unittarget;
        private Actor_Unit actortarget;

        public Unit Unit => type == 0 ? unittarget : actortarget.Unit;
        public Actor_Unit Actor => type == 1 ? actortarget : null;

        public TargetUnion(Unit unit)
        {
            type = 0;
            unittarget = unit;
        }
        public TargetUnion(Actor_Unit actor)
        {
            type = 1;
            actortarget = actor;
        }
    }
    private static TargetUnion Target;
    private static double Damage;

    public static bool ActiveFlag { get; private set; } = false;
    public static DamageLethality Lethality { get; private set; }

    // Rendering the damage to an integer: 0 if accumulated total is less than 1/10th point of damage/healing; otherwise, rounded toward zero, but with a minimum of 1.
    public static int FinalDamage => Math.Abs(Damage) <= 0.1 ? 0 : (Math.Abs(Damage) >= 1 ? (int)Damage : (Damage > 0 ? 1 : -1));
    
    public static Unit Unit => ActiveFlag ? Target.Unit : null;
    public static Actor_Unit Actor => ActiveFlag ? Target.Actor : null;

    public static void Activate(Unit unit)
    {
        Activate(new TargetUnion(unit));
    }
    public static void Activate(Actor_Unit actor)
    {
        Activate(new TargetUnion(actor));
    }
    private static void Activate(TargetUnion newtarget)
    {
        if (ActiveFlag)
            throw new InvalidOperationException("Attempted to Activate DamageAccumulator when it was already active.");

        ActiveFlag = true;
        Target = newtarget;
        Damage = 0;
        Lethality = DamageLethality.NonLethal;
    }

    public static void AddDamage(double damage)
    {
        Damage += damage;
    }

    public static void SetLethality(DamageLethality newlethality)
    {
        // Damage can be made more lethal at any time during accumulation, but cannot be made less lethal.
        if (newlethality > Lethality)
            Lethality = newlethality;
    }

    public static void Execute()
    {
        if (!ActiveFlag)
            throw new InvalidOperationException("Attempted to Execute DamageAccumulator when it was not active.");

        // Second step: Apply damage to unit.
        if (Target.type == 0)
        {
            // Reminder: Target is a Unit.

            // Applying damage, with consideration for Lethality.
            if (FinalDamage >= Unit.Health)
                switch (Lethality)
                {
                    case DamageLethality.Lethal:
                        Unit.Health = 0;
                        Unit.Kill();
                        Unit.Army()?.Units.Remove(Unit);
                        break;
                    default: // Since Target is a Unit, this is out of battle, so there is nobody to surrender to; therefore, ForceSurrender is treated as NonLethal.
                        Unit.Health = 1;
                        break;
                }
            else
                Unit.Health -= FinalDamage;
        }
        else
        {
            // Reminder: Target is an Actor_Unit.

            if (FinalDamage != 0)
            {
                if (FinalDamage > 0)
                {
                    Actor.UnitSprite.DisplayDamage(FinalDamage);
                    Actor.Damage(FinalDamage, false, Lethality);
                }
                else if (Unit.Health < Unit.MaxHealth)
                {
                    Actor.UnitSprite.DisplayDamage(FinalDamage);
                    Actor.Unit.Heal(-FinalDamage);
                }
            }
        }

        ActiveFlag = false;
    }

    public static void Deactivate()
    {
        ActiveFlag = false;
    }
}
