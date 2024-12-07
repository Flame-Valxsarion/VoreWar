using System;
using System.Collections.Generic;

abstract class Trait
{
    internal string Description;
}

abstract class VoreTrait : Trait, IVoreCallback
{
    public virtual int ProcessingPriority => 0;

    public abstract bool IsPredTrait { get; }

    public virtual bool OnAbsorption(Prey preyUnit, Actor_Unit predUnit, PreyLocation location) => true;

    public virtual bool OnDigestion(Prey preyUnit, Actor_Unit predUnit, PreyLocation location) => true;

    public virtual bool OnDigestionKill(Prey preyUnit, Actor_Unit predUnit, PreyLocation location) => true;

    public virtual bool OnFinishAbsorption(Prey preyUnit, Actor_Unit predUnit, PreyLocation location) => true;

    public virtual bool OnFinishDigestion(Prey preyUnit, Actor_Unit predUnit, PreyLocation location) => true;

    public virtual bool OnRemove(Prey preyUnit, Actor_Unit predUnit, PreyLocation location) => true;

    public virtual bool OnSwallow(Prey preyUnit, Actor_Unit predUnit, PreyLocation location) => true;
}

class PermanentBoosts
{
    internal float ExpRequired = 1.0f;
    internal float ExpGain = 1.0f;
    internal float ExpGainFromVore = 1.0f;
    internal float ExpGainFromAbsorption = 1.0f;
    internal float PassiveHeal = 1.0f;
    internal float CapacityMult = 1;
    internal DirectionalStat Incoming = new DirectionalStat();
    internal DirectionalStat Outgoing = new DirectionalStat();
    internal float FlatHitReduction = 1.0f;
    internal float SpeedLossFromWeightMultiplier = 1.0f;
    internal float DodgeLossFromWeightMultiplier = 1.0f;
    internal float BulkMultiplier = 1.0f;
    internal float SpeedMultiplier = 1.0f;
    internal int MinSpeed = 1;
    internal int SpeedBonus = 0;
    internal int MeleeAttacks = 1;
    internal int RangedAttacks = 1;
    internal int VoreAttacks = 1;
    internal int SpellAttacks = 1;
    internal float ManaMultiplier = 1.0f;
    internal int VoreMinimumOdds = 0;
    internal int TurnCanFlee = 8;
    internal int DigestionImmunityTurns = Config.DigestionGraceTurns;
    internal int HealthRegen = 0;
    internal int ManaRegen = 0;
    internal int OnLevelUpBonusToAllStats = 0;
    internal int OnLevelUpBonusToGiveToTwoRandomStats = 0;
    internal bool OnLevelUpAllowAnyStat = false;
    internal float Scale = 1f;
    internal float StatMult = 1f;
    internal float VirtualDexMult = 1;
    internal float VirtualStrMult = 1;
    internal float FireDamageTaken = 1;
    internal float IceDamageTaken = 1;
    internal float ElecDamageTaken = 1;
    internal float GrowthDecayRate = 1;
    internal int SightRangeBoost = 0;
}

class DirectionalStat
{
    internal float ChanceToEscape = 1;
    internal float MeleeDamage = 1;
    internal float RangedDamage = 1;
    internal float MagicDamage = 1;
    internal float DigestionRate = 1;
    internal float AbsorptionRate = 1;
    internal float Nutrition = 1;
    internal int ManaAbsorbHundreths = 0;
    /// <summary>Positive favors defender, negative favors attacker</summary>
    internal float MeleeShift = 0;
    internal float RangedShift = 0;
    internal float MagicShift = 0;
    internal float VoreOddsMult = 1;
    internal float GrowthRate = 1;

    internal float CritRateShift = 0;
    internal float CritDamageMult = 1;
    internal float GrazeRateShift = 0;
    internal float GrazeDamageMult = 1;
}


interface IStatBoost
{
    int StatBoost(Unit unit, Stat stat);
}

interface IAttackStatusEffect
{
    void ApplyStatusEffect(Actor_Unit actor, Actor_Unit target, bool ranged, int damage);
}

interface IVoreDefenseOdds
{
    void VoreDefense(Actor_Unit defender, ref float voreMult);
}

interface IVoreAttackOdds
{
    void VoreAttack(Actor_Unit attacker, ref float voreMult);
}

interface IPhysicalDefenseOdds
{
    void PhysicalDefense(Actor_Unit defender, ref float defMult);
}

interface IProvidesSingleSpell
{
    List<SpellTypes> GetSingleSpells(Unit unit);
}

interface IProvidesMultiSpell
{
    List<SpellTypes> GetMultiSpells(Unit unit);
}

interface INoAutoEscape
{
    bool CanEscape(Prey preyUnit, Actor_Unit predUnit);
}

abstract class AbstractBooster : Trait
{
    internal Action<PermanentBoosts> Boost;
}

class Booster : AbstractBooster
{
    public Booster(string desc, Action<PermanentBoosts> boost)
    {
        Description = desc;
        Boost = boost;
    }
}

abstract class VoreTraitBooster : AbstractBooster, IVoreCallback
{
    public virtual int ProcessingPriority => 0;

    public abstract bool IsPredTrait { get; }

    public virtual bool OnAbsorption(Prey preyUnit, Actor_Unit predUnit, PreyLocation location) => true;

    public virtual bool OnDigestion(Prey preyUnit, Actor_Unit predUnit, PreyLocation location) => true;

    public virtual bool OnDigestionKill(Prey preyUnit, Actor_Unit predUnit, PreyLocation location) => true;

    public virtual bool OnFinishAbsorption(Prey preyUnit, Actor_Unit predUnit, PreyLocation location) => true;

    public virtual bool OnFinishDigestion(Prey preyUnit, Actor_Unit predUnit, PreyLocation location) => true;

    public virtual bool OnRemove(Prey preyUnit, Actor_Unit predUnit, PreyLocation location) => true;

    public virtual bool OnSwallow(Prey preyUnit, Actor_Unit predUnit, PreyLocation location) => true;
}


static class TraitList
{

    static internal Trait GetTrait(Traits trait)
    {
        traits.TryGetValue(trait, out Trait retTrait);
        return retTrait;
    }

    static Dictionary<Traits, Trait> traits = new Dictionary<Traits, Trait>()
    {
        [Traits.Frenzy] = new Frenzy(),
        [Traits.ThrillSeeker] = new ThrillSeeker(),
        [Traits.PackStrength] = new PackStat(Stat.Strength),
        [Traits.PackDexterity] = new PackStat(Stat.Dexterity),
        [Traits.PackDefense] = new PackStat(Stat.Agility),
        [Traits.PackWill] = new PackStat(Stat.Will),
        [Traits.PackMind] = new PackStat(Stat.Mind),
        [Traits.PackVoracity] = new PackStat(Stat.Voracity),
        [Traits.PackStomach] = new PackStat(Stat.Stomach),
        [Traits.PackTactics] = new PackTactics(),
        [Traits.Paralyzer] = new Paralyzer(),
        [Traits.BoggingSlime] = new BoggingSlime(),
        [Traits.Vampirism] = new Vampirism(),
        [Traits.Stinger] = new Stinger(),
        [Traits.DefensiveStance] = new DefensiveStance(),
        [Traits.Ravenous] = new Ravenous(),
        [Traits.Possession] = new Possession(),
        [Traits.UnpleasantDigestion] = new UnpleasantDigestion(),
        [Traits.Parasite] = new Parasite(),
        [Traits.Whispers] = new Whispers(),
        [Traits.Metamorphosis] = new Metamorphosis(),
        [Traits.Changeling] = new Changeling(),
        [Traits.GreaterChangeling] = new GreaterChangeling(),
        [Traits.Symbiote] = new Symbiote(),
        [Traits.TraitBorrower] = new TraitBorrower(),
        [Traits.CreateSpawn] = new CreateSpawn(),
        [Traits.SpiritPossession] = new SpiritPossession(),
        [Traits.ForcedMetamorphosis] = new ForcedMetamorphosis(),
        [Traits.MetamorphicConversion] = new MetamorphicConversion(),
        [Traits.Tempered] = new Booster("Reduces damage taken from ranged attacks.\nIncreases damage taken from melee attacks", (s) => { s.Incoming.RangedDamage *= .7f; s.Incoming.MeleeDamage *= 1.3f; s.VirtualDexMult *= 1.1f; }),
        [Traits.GelatinousBody] = new Booster("Takes less damage from attacks, but is easier to vore", (s) => { s.Incoming.RangedDamage *= .75f; s.Incoming.MeleeDamage *= 0.8f; s.Incoming.VoreOddsMult *= 1.15f; }),
        [Traits.MetalBody] = new Booster("Provides vore resistance, and their remains are only worth half as much healing", (s) => { s.Incoming.VoreOddsMult *= .7f; s.Outgoing.Nutrition *= .5f; }),
        [Traits.EasyToVore] = new Booster("Unit is easier to vore than normal", (s) => s.Incoming.VoreOddsMult *= 1.25f),
        [Traits.Defenseless] = new Booster("Unit is incredibly easy to vore", (s) => s.Incoming.VoreOddsMult *= 1000),
        [Traits.BornToMove] = new Booster("Experienced at carrying extra weight.\nUnit suffers a smaller defense penalty and no movement penalty from units it is carrying.", (s) => { s.SpeedLossFromWeightMultiplier = 0; s.DodgeLossFromWeightMultiplier = 0.2f; }),
        [Traits.Maul] = new Booster("Allows unit to attack twice in melee.\nEach attack uses half of max AP", (s) => { s.MeleeAttacks += 1; s.VirtualStrMult *= 1.8f; }),
        [Traits.DoubleAttack] = new Booster("Allows unit to attack twice in melee or ranged.\nEach attack uses half of max AP", (s) => { s.MeleeAttacks += 1; s.RangedAttacks += 1; }),
        [Traits.Nauseous] = new Booster("This unit has a higher than average chance of having prey escape", (s) => s.Outgoing.ChanceToEscape *= 2),
        [Traits.EscapeArtist] = new Booster("Unit is much more likely to escape from predators", (s) => s.Incoming.ChanceToEscape *= 2),
        [Traits.Submissive] = new Booster("Unit does not try to escape", (s) => s.Incoming.ChanceToEscape *= 0),
        [Traits.EvasiveBattler] = new Booster("This unit can flee from battles on its fourth turn", (s) => s.TurnCanFlee = 4),
        [Traits.Prey] = new Booster("Unit can not vore other units.\nReceives 15% more exp, and heals twice as fast in towns", (s) => { s.ExpGain *= 1.15f; s.PassiveHeal *= 2; }),
        [Traits.Clever] = new Booster("Requires less experience to level up", (s) => s.ExpRequired *= 0.7f),
        [Traits.Foolish] = new Booster("Requires additional experience to level up", (s) => s.ExpRequired *= 1.4f),
        [Traits.StrongMelee] = new Booster("Does additional damage in melee", (s) => { s.Outgoing.MeleeDamage *= 1.2f; s.VirtualStrMult *= 1.2f; }),
        [Traits.WeakAttack] = new Booster("Does reduced damage in melee", (s) => { s.Outgoing.MeleeDamage *= 0.8f; s.VirtualDexMult *= 1.2f; }),
        [Traits.FastDigestion] = new Booster("Does additional acid damage to prey. (150%)", (s) => s.Outgoing.DigestionRate *= 1.5f),
        [Traits.SlowDigestion] = new Booster("Does reduced acid damage to prey. (50%)", (s) => s.Outgoing.DigestionRate *= 0.5f),
        [Traits.Tasty] = new Booster("Provides additonal healing when consumed", (s) => s.Outgoing.Nutrition *= 2f),
        [Traits.Disgusting] = new Booster("Provides very little healing when consumed", (s) => s.Outgoing.Nutrition *= 0.2f),
        [Traits.ArtfulDodge] = new Booster("Flat 10% chance to dodge all attacks or vore attacks", (s) => s.FlatHitReduction *= 0.9f),
        [Traits.AcidResistant] = new Booster("Takes less damage from acid, and gets a 1 turn grace period before taking damage when prey", (s) => { s.Incoming.DigestionRate *= 0.5f; s.DigestionImmunityTurns += 1; }),
        [Traits.SoftBody] = new Booster("Unit takes additional acid damage", (s) => s.Incoming.DigestionRate *= 2f),
        [Traits.KeenReflexes] = new Booster("Gains a significant amount of dodge chance against ranged attacks", (s) => s.Incoming.RangedShift += 1.5f),
        [Traits.StrongGullet] = new Booster("Allows unit to do 2 vore attempts.\nEach vore uses half of max AP", (s) => s.VoreAttacks += 1),
        [Traits.AdeptLearner] = new Booster("Allows picking of any stat on level up and gives +1 to 2 random stats on level up", (s) => { s.OnLevelUpBonusToGiveToTwoRandomStats += 1; s.OnLevelUpAllowAnyStat = true; }),
        [Traits.Lethargic] = new Booster("Unit is significantly less likely to escape from predators", (s) => s.Incoming.ChanceToEscape *= 0.5f),
        [Traits.AcidImmunity] = new Booster("Unit is immune to acid damage.  To avoid never ending battles, the immunity only lasts for 20 turns in the same belly", (s) => s.DigestionImmunityTurns += 20),
        [Traits.Large] = new Booster("Unit is larger than normal", (s) => s.Scale *= 1.5f),
        [Traits.Small] = new Booster("Unit is smaller than normal", (s) => s.Scale *= 2.0f / 3.0f),
        [Traits.MagicResistance] = new Booster("Unit is harder to hit with magic", (s) => s.Incoming.MagicShift += 0.2f),
        [Traits.MagicProwess] = new Booster("Unit's spells are more accurate", (s) => s.Outgoing.MagicShift -= 0.2f),
        [Traits.FastAbsorption] = new Booster("Unit absorbs dead prey more quickly. (150%)", (s) => s.Outgoing.AbsorptionRate *= 1.5f),
        [Traits.SlowAbsorption] = new Booster("Unit absorbs dead prey more slowly. (50%)", (s) => s.Outgoing.AbsorptionRate *= 0.5f),
        [Traits.IronGut] = new Booster("This unit is hard to escape from", (s) => s.Outgoing.ChanceToEscape *= 0.5f),
        [Traits.SteadyStomach] = new Booster("Unit keeps prey down slightly better than average", (s) => s.Outgoing.ChanceToEscape *= 0.85f),
        [Traits.Bulky] = new Booster("Unit is bulkier than normal (increased size, making them harder to swallow, without actually giving other bonuses or making them bigger like the scale traits)", (s) => s.BulkMultiplier *= 1.75f),
        [Traits.SlowMovement] = new Booster("Unit is slower than normal, but suffers reduced prey penalties to speed.", (s) => { s.SpeedMultiplier *= 0.75f; s.MinSpeed = 2; s.SpeedLossFromWeightMultiplier = 0.5f; }),
        [Traits.VerySlowMovement] = new Booster("Unit is considerably slower than normal, but suffers no prey penalties to speed.", (s) => { s.SpeedMultiplier *= 0.65f; s.MinSpeed = 2; s.SpeedLossFromWeightMultiplier = 0; }),
        [Traits.Toxic] = new Booster("Melee attackers have a chance to be poisoned on hitting this unit, and it provides no healing when absorbed.", (s) => s.Outgoing.Nutrition = 0),
        [Traits.HardSkin] = new Booster("Its skin is hard and ranged weapons have a harder time penetrating it.", (s) => { s.Incoming.RangedDamage *= .75f; s.VirtualDexMult *= 1.1f; }),
        [Traits.QuickShooter] = new Booster("Unit is quite adept with ranged weapons and is able to attack twice with them.", (s) => { s.RangedAttacks += 1; s.VirtualDexMult *= 2.2f; }),
        [Traits.FastCaster] = new Booster("Unit is quite adept with spells and is able to cast two spells per turn.", (s) => s.SpellAttacks += 1),
        [Traits.RangedIneptitude] = new Booster("Unit's ranged weapons do less damage than normal", (s) => {s.Outgoing.RangedDamage *= 0.8f; s.VirtualStrMult *= 1.2f; }),
        [Traits.KeenShot] = new Booster("Unit's ranged weapons do additional damage", (s) => { s.Outgoing.RangedDamage *= 1.2f; s.VirtualDexMult *= 1.2f;}),
        [Traits.HotBlooded] = new Booster("Unit is used to the heat and takes significantly less damage from fire spells", (s) => s.FireDamageTaken *= .25f),
        [Traits.Grounded] = new Booster("Unit is resistant to electricity and takes significantly less damage from electric spells", (s) => s.ElecDamageTaken *= .25f),
        [Traits.ColdTolerance] = new Booster("Unit is used to the cold and takes significantly less damage from ice spells", (s) => s.IceDamageTaken *= .25f),
        [Traits.FocusedDevelopment] = new Booster("Allows picking of any stat on level up", (s) => { s.OnLevelUpAllowAnyStat = true; }),
        [Traits.ManaRich] = new Booster("Unit has higher mana cap, units absorbing this unit will recover some of their mana", (s) => { s.ManaMultiplier *= 1.5f; s.Outgoing.ManaAbsorbHundreths += 40; }),
        [Traits.ManaDrain] = new Booster("This unit will recover some mana while absorbing others, but it also receives less health", (s) => { s.Incoming.ManaAbsorbHundreths += 40; s.Incoming.Nutrition *= .6f; }),
        [Traits.CursedMark] = new Booster("This unit gets various combat boosts (increased damage, accuracy and damage resistance), but is slower and significantly easier to eat and keep down, and whoever digests this unit inherits this trait (applied at time of death)", (s) =>
        {
            s.Outgoing.MeleeDamage *= 1.4f; s.Outgoing.RangedDamage *= 1.4f; s.Outgoing.MagicDamage *= 1.4f; s.Outgoing.MeleeShift -= 1; s.Outgoing.RangedShift -= 1; s.Outgoing.MagicShift -= 1;
            s.Incoming.MeleeDamage *= .75f; s.Incoming.RangedDamage *= .75f; s.Incoming.VoreOddsMult *= 2.5f; s.SpeedMultiplier *= .8f; s.Incoming.ChanceToEscape *= .25f;
        }),
        [Traits.Slippery] = new Booster("Unit is harder to eat, but has a hard time escaping once eaten", (s) => { s.Incoming.VoreOddsMult *= .8f; s.Incoming.ChanceToEscape *= .4f; }),
        [Traits.HealingBlood] = new Booster("Unit heals 2 HP per turn (and fully outside of battles), but is also worth a lot more healing to its predator", (s) => { s.HealthRegen += 2; s.Outgoing.Nutrition *= 3f; }),
        [Traits.ManaDynamo] = new Booster("Unit generates 3 mana per turn but units absorbing this unit will recover most of their mana", (s) => { s.ManaRegen += 3; s.Outgoing.ManaAbsorbHundreths += 70; }),


        [Traits.LightningSpeed] = new Booster("Unit moves very fast, and can perform actions many times per turn. \n(Cheat Trait)", (s) => { s.SpeedBonus += 10; s.MeleeAttacks += 5; s.RangedAttacks += 5; s.VoreAttacks += 5; s.SpellAttacks += 5; }),
        [Traits.DivineBloodline] = new Booster("Descendent from supernatural beings, this individual is a force of nature. \n(Cheat Trait) - gives damage reduction, extra attacks, stat bonuses on level up)", (s) =>
        {
            s.Incoming.DigestionRate *= 0.25f; s.DigestionImmunityTurns += 5; s.Incoming.RangedDamage *= .75f;
            s.Incoming.MeleeDamage *= 0.75f; s.PassiveHeal *= 10; s.VoreAttacks += 2; s.MeleeAttacks += 2; s.RangedAttacks += 2; s.SpellAttacks += 2; s.OnLevelUpBonusToAllStats += 3; s.OnLevelUpBonusToGiveToTwoRandomStats += 7; s.OnLevelUpAllowAnyStat = true;
        }),
        [Traits.GeneEater] = new Booster("Capable of absorbing the best genes from devoured prey. Gains significant bonus experience from absorbing prey. \n(Cheat Trait)", (s) => { s.ExpGainFromVore *= 5.0f; s.ExpGainFromAbsorption *= 50.0f; }),
        [Traits.InstantDigestion] = new Booster("Instantly digests non-acid-immune prey. \n(Cheat Trait)", (s) => s.Outgoing.DigestionRate *= 1000000f),
        [Traits.InstantAbsorption] = new Booster("Instantly absorbs dead prey. \n(Cheat Trait)", (s) => s.Outgoing.AbsorptionRate *= 1000000f),
        [Traits.Inescapable] = new Booster("Prey can never escape. \n(Cheat Trait)", (s) => s.Outgoing.ChanceToEscape /= 1000f), //Handled seperately now.  
        [Traits.Irresistable] = new Booster("Prey is always devoured. \n(Cheat Trait)", (s) => s.Outgoing.VoreOddsMult *= 1000000f),
        [Traits.Titanic] = new Booster("Unit is practically a giant. (Size × 3). \n(Cheat Trait)", (s) => s.Scale *= 3f),
        [Traits.Colossal] = new Booster("Unit is far larger than normal (Size × 2.5). \n(Cheat Trait)", (s) => s.Scale *= 2.5f),
        [Traits.Huge] = new Booster("Unit is considerably larger than normal (Size × 2). \n(Cheat Trait)", (s) => s.Scale *= 2f),
        [Traits.Tiny] = new Booster("Unit is far smaller than normal. \n(Cheat Trait)", (s) => s.Scale /= 3.0f),
        [Traits.AdaptiveTactics] = new Booster("Unit earns double the normal amount of experience from actions.\n(Cheat Trait)", (s) => s.ExpGain *= 2),
        [Traits.SlowMetabolism] = new Booster("Unit digests and absorbs prey very slowly (50%)", (s) => { s.Outgoing.AbsorptionRate *= 0.5f; s.Outgoing.DigestionRate *= 0.5f; }),
        [Traits.LightFrame] = new Booster("Unit can melee attack twice in a turn, though it loses this ability while it contains any prey.  Unit also takes 25% more damage from all sources", (s) => { s.Incoming.MeleeDamage *= 1.25f; s.Incoming.RangedDamage *= 1.25f; s.Incoming.MagicDamage *= 1.25f; s.VirtualStrMult *= 1.7f; }),
        [Traits.Featherweight] = new Booster("Unit moves slightly faster (+1 AP) and gets a melee/vore dodge bonus, but takes extra damage from melee.", (s) => { s.SpeedBonus += 1; s.Incoming.MeleeShift += .75f; s.Incoming.VoreOddsMult *= 0.75f; s.Incoming.MeleeDamage *= 1.2f; }),
        [Traits.Elite] = new Booster("Unit is skilled and trained in advanced tactics but requires more Exp to level ( All stats +120% but 2x Exp required)", (s) => {s.StatMult *= 2.2f; s.ExpRequired *= 2.0f; }),
        [Traits.PeakCondition] = new Booster("Unit is at the height of their physical condition (All stats × 1.5)", (s) => s.StatMult *= 1.5f),
        [Traits.Fit] = new Booster("Unit is in better shape than the average unit (All stats × 1.2)", (s) => s.StatMult *= 1.2f),
        [Traits.Illness] = new Booster("Unit is sick and is in poor shape (All stats × 0.8)", (s) => s.StatMult *= 0.8f),
        [Traits.Diseased] = new Booster("Unit is in terrible shape, and is only a shadow of its former self (All stats × 0.5)", (s) => s.StatMult *= 0.5f),
        [Traits.StretchyInsides] = new Booster("This unit can hold twice as many units as normal.", (s) => s.CapacityMult *= 2),
        [Traits.UnlimitedCapacity] = new Booster("This unit effectively has no limit on consumed prey.\n(Cheat Trait)", (s) => s.CapacityMult *= 1000),
        [Traits.Clumsy] = new Booster("This unit moves slower, and has slightly reduced melee accuracy and damage.", (s) => { s.SpeedMultiplier *= 0.85f; s.Outgoing.MeleeDamage *= .9f; s.Outgoing.MeleeShift += .3f; s.VirtualDexMult *= 1.1f; }),
        [Traits.Honeymaker] = new Booster("When feeding another unit, more exprience is given and prey is absorbed slower.", (s) => { s.Incoming.RangedDamage *= 1.0f; }),
        [Traits.WetNurse] = new Booster("When feeding another unit, more health is healed and less AP is consumed.", (s) => { s.Incoming.RangedDamage *= 1.0f; }),
        [Traits.HighlyAbsorbable] = new Booster("This unit's body gets absorbed more readily. (Absorption rate × 1.5)", (s) => { s.Incoming.AbsorptionRate *= 1.5f; }),
        [Traits.IdealSustenance] = new Booster("This unit's body gets absorbed efficiently and very fast.(+50% nutrition, +100% absorb speed)", (s) => { s.Outgoing.Nutrition *= 1.5f; s.Incoming.AbsorptionRate = 2.0f; }),
        [Traits.DoubleGrowth] = new Booster("Unit grows twice as much from absorbing prey (Requires the Growth trait)", (s) => { s.Incoming.GrowthRate *= 2f; }),
        [Traits.IncreasedGrowth] = new Booster("Unit grows 50% more from absorbing prey (Requires the Growth trait)", (s) => { s.Incoming.GrowthRate *= 1.5f; }),
        [Traits.MinorGrowth] = new Booster("Unit grows 50% less from absorbing prey (Requires the Growth trait)", (s) => { s.Incoming.GrowthRate *= 0.5f; }),
        [Traits.SlowedGrowth] = new Booster("Unit grows 20% less from absorbing prey (Requires the Growth trait)", (s) => { s.Incoming.GrowthRate *= 0.8f; }),
        [Traits.FleetingGrowth] = new Booster("Unit loses its gained growth more quickly (Requires the Growth trait)", (s) => { s.GrowthDecayRate *= 2f; }),
        [Traits.PersistentGrowth] = new Booster("Unit loses its gained growth less quickly (Requires the Growth trait)", (s) => { s.GrowthDecayRate *= 0.5f; }),
        [Traits.ProteinRich] = new Booster("Absorbing this unit yields more (2×) healing and (with the growth trait) more growth than usual (1.5×)", (s) => { s.Outgoing.GrowthRate *= 1.5f; s.Outgoing.Nutrition *= 2f; }),
        [Traits.EfficientGuts] = new Booster("Unit receives 50% more healing from absorbing prey", (s) => { s.Incoming.Nutrition *= 1.5f; }),
        [Traits.WastefulProcessing] = new Booster("Unit can't get as much healing out of prey, but they are done with it quicker. (Absorption rate × 1.5, Nutrition received × 0.5 )", (s) => { s.Incoming.Nutrition *= 0.5f; s.Outgoing.AbsorptionRate *= 1.5f; }),
        [Traits.TightNethers] = new Booster("This unit can only take much smaller units into their nethers, but their prey will not enlarge while inside their genitals.", (s) => { s.Incoming.RangedDamage *= 1.0f; }),
		[Traits.NightEye] = new Booster("Increases night time vision range by +1 in Tactical battles and by +1 in stratigic if half of the units in an army have this trait.", (s) => { s.SightRangeBoost += 1;}),
        [Traits.KeenEye] = new Booster("Unit has a 10% chance to deal increased damage when attacking.", (s) => { s.Outgoing.CritRateShift += 0.1f; }),
        [Traits.AccuteDodge] = new Booster("Unit has a 10% chance to minimise recieved damage when being attacked. (Excludes spells and vore damage).", (s) => { s.Outgoing.GrazeRateShift += 0.1f; }),
        [Traits.ViralDigestion] = new ViralDigestion(),
        [Traits.AwkwardShape] = new Booster("This unit has a very strange body type, making them harder to swallow and providing less sustenance as prey.", (s) => { s.Incoming.VoreOddsMult *= 0.75f; s.Outgoing.Nutrition *= 0.25f; }),
        [Traits.Legendary] = new Booster("<b>This unit is a legendary predator renowned throughout the realm, possessing a wide array of skills learned from generations upon generations of experiences.</b> \n<b>StrongGullet:</b> May attempt <b>2</b> vore attacks per turn, each using half of max AP. \n<b>BornToMove:</b> Total prey does not affect unit's movement speed. \n<b>IronGut:</b> It is much harder for prey to escape this unit's innards once devoured. \n<b>MagicResistance:</b> This unit is harder to hit with magic. \n<b>GreatlyTempered:</b>Recieves less damage from ranged attacks, but full damage from melee attacks. \nCheat Trait", (s) => { s.VoreAttacks += 1; s.SpeedLossFromWeightMultiplier = 0; s.DodgeLossFromWeightMultiplier = 0.2f; s.Outgoing.ChanceToEscape *= 0.5f; s.Incoming.MagicShift += 0.2f; s.Incoming.RangedDamage *= .7f; }),
        [Traits.FireVulnerable] = new Booster("Unit takes extra damage from all sources of fire. (150%)", (s) => s.FireDamageTaken *= 1.5f),
        [Traits.IceVulnerable] = new Booster("Unit takes extra damage from all sources of ice. (150%)", (s) => s.IceDamageTaken *= 1.5f),
        [Traits.ElecVulnerable] = new Booster("Unit takes extra damage from all sources of electricity. (150%)", (s) => s.ElecDamageTaken *= 1.5f),
        [Traits.SlowerDigestion] = new Booster("Does reduced acid damage to prey (25%)", (s) => s.Outgoing.DigestionRate *= 0.25f),
        [Traits.FasterDigestion] = new Booster("Does additional acid damage to prey. (200%)", (s) => s.Outgoing.DigestionRate *= 2f),
        [Traits.FasterAbsorption] = new Booster("Unit absorbs dead prey even more quickly. (200%)", (s) => s.Outgoing.AbsorptionRate *= 2f),
        [Traits.SlowerAbsorption] = new Booster("Unit absorbs dead prey even more slowly. (25%)", (s) => s.Outgoing.AbsorptionRate *= 0.25f),
        [Traits.SlowerMetabolism] = new Booster("Unit digests and absorbs prey very slowly. (25%)", (s) => { s.Outgoing.AbsorptionRate *= 0.25f; s.Outgoing.DigestionRate *= 0.25f; }),
    };

}

internal class Frenzy : Trait, IStatBoost
{
    public Frenzy() => Description = "Unit's stats increase for every kill it gets.\nResets at the end of the battle";

    public int StatBoost(Unit unit, Stat stat)
    {
        int ret = 0;
        if (unit.EnemiesKilledThisBattle > 0)
        {
            ret = (int)(unit.GetStatBase(stat) * 0.1f * unit.EnemiesKilledThisBattle);
        }
        return ret;
    }
}

internal class ThrillSeeker : Trait, IStatBoost
{
    public ThrillSeeker() => Description = "Stats increase as health falls";

    public int StatBoost(Unit unit, Stat stat)
    {
        int ret = 0;
        ret = (int)(unit.GetStatBase(stat) * (0.11f - (0.11f * unit.GetHealthPctWithoutUpdating())));
        if (ret < 0) return 0;
        return ret;
    }
}

internal class PackStat : Trait, IStatBoost
{
    Stat usedStat;

    public PackStat(Stat usedStat)
    {
        Description = $"Boosts {usedStat} if friendly units are nearby.\nEffect is capped at 2 friendly units";
        this.usedStat = usedStat;
    }

    public int StatBoost(Unit unit, Stat stat)
    {
        int ret = 0;
        if (stat == usedStat)
        {
            ret = Math.Min(unit.NearbyFriendlies, 2) * (1 + unit.Level) / 2;
        }
        return ret;
    }
}

internal class PackTactics : Trait, IStatBoost
{
    public PackTactics()
    {
        Description = $"A combination of all the other pack stat traits";
    }

    public int StatBoost(Unit unit, Stat stat)
    {
        int ret = 0;
        if (stat != Stat.Endurance && stat != Stat.Leadership)
        {
            ret = Math.Min(unit.NearbyFriendlies, 2) * (1 + unit.Level) / 2;
        }
        return ret;
    }
}

internal class Paralyzer : Trait, IAttackStatusEffect
{
    public Paralyzer() => Description = "Unit has a small chance to stun an enemy for 1 turn when it attacks";

    public void ApplyStatusEffect(Actor_Unit actor, Actor_Unit target, bool ranged, int damage)
    {
        if (State.Rand.Next(10 + 3 * target.TimesParalyzed) == 0)
            target.Paralyzed = true;
    }
}

internal class BoggingSlime : Trait, IAttackStatusEffect
{
    public BoggingSlime() => Description = "Attacks from this unit have a chance to slime enemy units for 1 turn.\n(effect lowers AP to 50% of max)\nDoes not stack.";

    public void ApplyStatusEffect(Actor_Unit actor, Actor_Unit target, bool ranged, int damage)
    {
        if (State.Rand.Next(10) < 4)
            target.Slimed = true;
    }
}

internal class Vampirism : Trait, IAttackStatusEffect
{
    public Vampirism() => Description = "Melee attacks from this unit will heal the attacker by a small amount (12% of damage done, to a minimum of 1).";

    public void ApplyStatusEffect(Actor_Unit actor, Actor_Unit target, bool ranged, int damage)
    {
        if (ranged == false)
        {
            int heal = Math.Max((int)(damage * .12f), 1);
            actor.Unit.Heal(heal);
        }

    }
}

internal class Stinger : Trait, IAttackStatusEffect
{
    public Stinger() => Description = "Melee attacks from this unit have a chance to poison enemy units. Poison deals damage over time but doesn't kill enemy units.\nDoes not stack with itself or the poison spell.";

    public void ApplyStatusEffect(Actor_Unit actor, Actor_Unit target, bool ranged, int damage)
    {
        if (ranged)
            return;
        if (State.Rand.Next(10) < 2)
        {
            damage = Math.Max(damage / 2, 3);
            target.Unit.ApplyStatusEffect(StatusEffectType.Poisoned, damage, 3);
        }
    }
}

internal class DefensiveStance : Trait, IVoreDefenseOdds, IPhysicalDefenseOdds
{
    public DefensiveStance() => Description = "Unit gets a bonus to defense if it has at least 1 AP remaining";

    public void PhysicalDefense(Actor_Unit defender, ref float defMult) => defMult += defender.Movement > 0 ? 0.8f : 0;

    public void VoreDefense(Actor_Unit defender, ref float voreMult) => voreMult *= defender.Movement > 0 ? 1.333f : 1;
}


internal class Ravenous : Trait, IVoreAttackOdds
{
    public Ravenous() => Description = "Unit gets a bonus to vore chance if no units are in its stomach";

    public void VoreAttack(Actor_Unit attacker, ref float voreMult)
    {
        if (attacker.PredatorComponent.Fullness == 0)
        {
            voreMult *= 1.75f;
        }
    }
}

internal class UnpleasantDigestion : VoreTrait
{
    public UnpleasantDigestion()
    {
        Description = "While digesting, prey deals damage to predator";
    }

    public override bool IsPredTrait => false;

    public override bool OnDigestion(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        predUnit.Damage(1);
        return true;
    }
}

internal class Whispers : VoreTrait, IProvidesSingleSpell
{
    public Whispers()
    {
        Description = "Predator has a chance to be charmed and afflicted by Prey's Curse each round";
    }

    public override bool IsPredTrait => false;

    public List<SpellTypes> GetSingleSpells(Unit unit) => new List<SpellTypes> { SpellList.Whispers.SpellType };

    public override bool OnDigestion(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        if(predUnit.Unit.FixedSide != preyUnit.Unit.FixedSide)
            preyUnit.Actor.CastStatusSpell(SpellList.Whispers, predUnit);
        return true;
    }
}

internal class Parasite : VoreTraitBooster
{
    public Parasite()
    {
        Description = "A parasite prey will give the host CreateSpawn and set infection after digestion, host takes minor damage on prey absorption and major damage when creating spawn";
        Boost = (s) => s.Incoming.ChanceToEscape *= 0;
    }

    public override bool IsPredTrait => false;

    public override bool OnFinishDigestion(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        predUnit.Infected = true;
        predUnit.InfectedRace = preyUnit.Unit.DetermineSpawnRace();
        if (!preyUnit.Unit.HasSharedTrait(Traits.Parasite))
            predUnit.InfectedRace = preyUnit.Unit.HiddenUnit.DetermineSpawnRace();
        predUnit.InfectedSide = preyUnit.Unit.FixedSide;
        return true;
    }
}

internal class Metamorphosis : VoreTrait, INoAutoEscape
{
    public Metamorphosis()
    {
        Description = "Unit is revived and changes race upon being digested";
    }

    public override bool IsPredTrait => false;

    public override int ProcessingPriority => 50;

    public bool CanEscape(Prey preyUnit, Actor_Unit predUnit)
    {
        return false;
    }

    public override bool OnFinishDigestion(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        Race conversionRace = preyUnit.Unit.HiddenUnit.DetermineSpawnRace();
        preyUnit.Unit.Health = preyUnit.Unit.MaxHealth;
        preyUnit.Unit.GiveExp(predUnit.Unit.Experience / 2);
        if (preyUnit.Unit.Race != conversionRace)
        {
            preyUnit.ChangeRace(conversionRace);
        }
        preyUnit.Unit.RemoveTrait(Traits.Metamorphosis);//no metamorphosis loops
        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{preyUnit.Unit.Name} changed form within {predUnit.Unit.Name}'s body.");
        predUnit.PredatorComponent.UpdateFullness();
        return false;
    }
}

internal class MetamorphicConversion : Metamorphosis
{
    public MetamorphicConversion()
    {
        Description = "Unit is revived, changes side and race upon being digested";
    }

    public override bool IsPredTrait => false;

    public override int ProcessingPriority => 49;

    public override bool OnFinishDigestion(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        preyUnit.ChangeSide(predUnit.Unit.FixedSide);
        base.OnFinishDigestion(preyUnit, predUnit, location);
        preyUnit.Unit.RemoveTrait(Traits.MetamorphicConversion);
        return false;
    }
}

internal class Possession : VoreTraitBooster, INoAutoEscape
{
    public Possession()
    {
        Description = "When unit is eaten, pred will be possessed as a hidden status. " +
        "Once possessed prey's stat total plus the pred's corruption is equal to that of the pred, they are under control of the side of the last-eaten possessed.";
        Boost = (s) => s.Incoming.ChanceToEscape *= 0;
    }

    public override bool IsPredTrait => false;

    public override bool OnRemove(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        if (!preyUnit.Unit.IsDead)
        {
            predUnit.RemovePossession(preyUnit.Actor);
        }
        return true;
    }

    public override bool OnDigestionKill(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        predUnit.RemovePossession(preyUnit.Actor);
        return true;
    }

    public override bool OnDigestion(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        predUnit.CheckPossession(preyUnit.Actor);
        return true;
    }

    public override bool OnSwallow(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        if (!preyUnit.Unit.IsDead)
        {
            predUnit.AddPossession(preyUnit.Actor);
        }
        return true;
    }

    public bool CanEscape(Prey preyUnit, Actor_Unit predUnit)
    {
        return !predUnit.CheckPossession(preyUnit.Actor);
    }
}

internal class Changeling : VoreTrait, IProvidesMultiSpell
{
    public Changeling()
    {
        Description = "While absorbing prey, changes to prey's race";
    }

    public override bool IsPredTrait => true;

    public override int ProcessingPriority => 51;

    public virtual bool TargetIsDead => true;

    public override bool OnRemove(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        if (preyUnit == predUnit.PredatorComponent.template)
        {
            predUnit.RevertRace();
            predUnit.PredatorComponent.ChangeRaceAuto(TargetIsDead, false);
            return false;
        }
        return true;
    }

    public override bool OnDigestionKill(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        predUnit.PredatorComponent.TryChangeRace(preyUnit);
        return true;
    }

    public List<SpellTypes> GetMultiSpells(Unit unit) => new List<SpellTypes> { SpellList.AssumeForm.SpellType, SpellList.RevertForm.SpellType };
}

internal class GreaterChangeling : Changeling
{
    public GreaterChangeling()
    {
        Description = "While digesting prey, changes to prey's race";
    }

    public override bool IsPredTrait => true;

    public override int ProcessingPriority => 50;

    public override bool TargetIsDead => false;

    public override bool OnSwallow(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        return !predUnit.PredatorComponent.TryChangeRace(preyUnit);
    }
}

internal class Symbiote : VoreTrait
{
    public Symbiote()
    {
        Description = "Shares generic traits with pred";
    }

    public override bool IsPredTrait => false;

    public override int ProcessingPriority => 100;

    public override bool OnSwallow(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        foreach (Traits trait in preyUnit.Unit.GetTraits)
        {
            if ((trait != Traits.Prey) && (trait != Traits.Small))
                predUnit.PredatorComponent.ShareTrait(trait, preyUnit);
        }
        predUnit.Unit.ReloadTraits();
        predUnit.Unit.InitializeTraits();
        predUnit.ReloadSpellTraits();
        return true;
    }

    public override bool OnRemove(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        foreach (Traits trait in preyUnit.SharedTraits)
            predUnit.Unit.RemoveSharedTrait(trait);
        predUnit.PredatorComponent.RefreshSharedTraits();
        predUnit.Unit.InitializeTraits();
        predUnit.ReloadSpellTraits();
        return true;
    }
}
internal class TraitBorrower : VoreTrait
{
    public TraitBorrower()
    {
        Description = "Borrows generic traits from prey";
    }

    public override bool IsPredTrait => true;

    public override int ProcessingPriority => 100;

    public override bool OnSwallow(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        foreach (Traits trait in preyUnit.Unit.GetTraits)
        {
            if ((trait != Traits.Prey) && (trait != Traits.Small))
                predUnit.PredatorComponent.ShareTrait(trait, preyUnit);
        }
        predUnit.Unit.ReloadTraits();
        predUnit.Unit.InitializeTraits();
        predUnit.ReloadSpellTraits();
        return true;
    }

    public override bool OnRemove(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        foreach (Traits trait in preyUnit.SharedTraits)
            predUnit.Unit.RemoveSharedTrait(trait);
        predUnit.PredatorComponent.RefreshSharedTraits();
        predUnit.Unit.InitializeTraits();
        predUnit.ReloadSpellTraits();
        return true;
    }
}
internal class CreateSpawn : VoreTrait
{
    public CreateSpawn()
    {
        Description = "Creates a spawn unit on prey absorption";
    }

    public override bool IsPredTrait => true;

    public override int ProcessingPriority => 0;

    public override bool OnFinishAbsorption(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        Race spawnRace = predUnit.Unit.DetermineSpawnRace();
        if (!predUnit.Unit.HasSharedTrait(Traits.CreateSpawn))
            spawnRace = predUnit.Unit.HiddenUnit.DetermineSpawnRace();
        // use source race IF changeling already had this ability before transforming
        predUnit.PredatorComponent.CreateSpawn(spawnRace, predUnit.Unit.Side, predUnit.Unit.Experience / 2);
        return true;
    }
}

internal class SpiritPossession : Possession
{
    public SpiritPossession()
    {
        Description = "Unit's soul continues to possess pred after death";
        Boost = (s) => s.Incoming.ChanceToEscape *= 0;
    }

    public override bool IsPredTrait => false;

    public override bool OnDigestionKill(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        var possessed = predUnit.CheckPossession(preyUnit.Actor);
        predUnit.RemovePossession(preyUnit.Actor);
        if(possessed)
        {
            if (predUnit.Unit.Side != preyUnit.Unit.Side)
                State.GameManager.TacticalMode.SwitchAlignment(predUnit);
            //TODO: This game needs some form of true fusion mechanic. 
            //this is an approximation of fusion with the result taking the appearance of the pred, and the side of the prey
            if (predUnit.Unit.Side == preyUnit.Unit.Side)
                predUnit.Unit.FixedSide = -1;
            predUnit.Unit.Name = preyUnit.Unit.Name;
            predUnit.Unit.GiveExp(preyUnit.Unit.Experience);
        }
        return true;
    }
}
internal class ForcedMetamorphosis : VoreTraitBooster, INoAutoEscape
{
    public ForcedMetamorphosis()
    {
        Description = "Pred will gain the Metamorphosis trait upon digesting this unit.";
        Boost = (s) => s.Incoming.ChanceToEscape *= 0;
    }

    public override int ProcessingPriority => 10;

    public override bool IsPredTrait => false;

    public bool CanEscape(Prey preyUnit, Actor_Unit predUnit)
    {
        return false;
    }

    public override bool OnDigestionKill(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        //TODO: Make this a status effect instead
        if((predUnit.Unit.FixedSide == preyUnit.Unit.FixedSide) && (predUnit.Unit.FixedSide == predUnit.Unit.Side))
            predUnit.Unit.AddPermanentTrait(Traits.Metamorphosis);
        else predUnit.Unit.AddPermanentTrait(Traits.MetamorphicConversion);
        predUnit.Unit.SpawnRace = preyUnit.Unit.HiddenUnit.DetermineSpawnRace();
        return true;
    }
}
internal class ViralDigestion : VoreTrait
{
    public ViralDigestion()
    {
        Description = "Unit has powerful viruses within them, which cause any prey to take additional damage for a few turns even after escaping.";
    }

    public override bool IsPredTrait => true;
    public override bool OnRemove(Prey preyUnit, Actor_Unit predUnit, PreyLocation location)
    {
        preyUnit.Unit.ApplyStatusEffect(StatusEffectType.Virus, 3, 3);
        return true;
    }
}