using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.Experimental.UIElements;

public struct Range
{
    internal int Min;
    internal int Max;

    public Range(int min, int max)
    {
        Min = min;
        Max = max;
    }
    public Range(int max)
    {
        Min = 0;
        Max = max;
    }
}

public enum AbilityTargets
{
    Enemy,
    Ally,
    Self,
    SurrenderedAlly,
    Tile,
}

public enum AreaOfEffectType
{
    /// <summary> Full(default) - used for non AOE and full box AOE spells</summary>
    Full,
    /// <summary> FixedPattern - Used for patterns that don't move</summary>
    FixedPattern,
    /// <summary> RotatablePattern - Used for patterns that rotate in relation to target and unit position</summary>
    RotatablePattern,
}

static class SpellList
{
    static internal readonly DamageSpell Fireball;
    static internal readonly DamageSpell PowerBolt;
    static internal readonly DamageSpell Icicle;
    static internal readonly DamageSpell LightningBolt;
    static internal readonly DamageSpell JoltCrash;
    static internal readonly StatusSpell Meditate;
    static internal readonly StatusSpell Shield;
    static internal readonly StatusSpell Mending;
    static internal readonly StatusSpell Speed;
    static internal readonly StatusSpell Valor;
    static internal readonly StatusSpell Predation;
    static internal readonly StatusSpell Charm;

    static internal readonly DamageSpell IceBlast;
    static internal readonly DamageSpell Pyre;
    static internal readonly DamageSpell CrossShock;
    static internal readonly DamageSpell ExplosiveHug;
    static internal readonly DamageSpell Explode;
    static internal readonly DamageSpell Flamberge;
    static internal readonly DamageSpell ForkLightning;
    //static internal readonly Spell Warp;
    //static internal readonly DamageSpell MagicWall;
    static internal readonly StatusSpell Poison;
    static internal readonly DamageSpell ForcePulse;
    static internal readonly StatusSpell Bloodrite;
    static internal readonly StatusSpell Trance;
    static internal readonly DamageSpell FlameWave;
    static internal readonly DamageSpell FireBomb;
    static internal readonly StatusSpell Bolas;
    static internal readonly Spell CaptureNet;
    static internal readonly Spell SummonDoppelganger;
    static internal readonly Spell SummonSpawn;

    //Quicksand
    static internal readonly StatusSpell PreysCurse;
    static internal readonly StatusSpell Enlarge;
    static internal readonly Spell Maw;
    static internal readonly Spell Summon;

    static internal readonly StatusSpell Diminishment;
    //Raze
    static internal readonly Spell GateMaw;
    static internal readonly Spell Reanimate;
    static internal readonly Spell Resurrection;

    static internal readonly Spell AmplifyMagic;
    static internal readonly Spell Evocation;
    static internal readonly DamageSpell ManaFlux;
    static internal readonly DamageSpell UnstableMana;
    static internal readonly DamageSpell ManaExpolsion;

    static internal readonly DamageSpell Conduit;

    static internal readonly StatusSpell AlraunePuff;
    static internal readonly StatusSpell Web;
    static internal readonly StatusSpell GlueBomb;
    static internal readonly StatusSpell Petrify;
    static internal readonly StatusSpell HypnoGas;
    static internal readonly StatusSpell Whispers;
    static internal readonly Spell ForceFeed;
    static internal readonly Spell AssumeForm;
    static internal readonly Spell RevertForm;
    static internal readonly Spell Bind;

    static internal readonly DamageSpell ViperPoisonDamage;
    static internal readonly StatusSpell ViperPoisonStatus;
    static internal readonly StatusSpell ViralInfection;
    static internal readonly StatusSpell DivinitysEmbrace;

    static internal Dictionary<SpellTypes, Spell> SpellDict;


    static SpellList()
    {
        SpellDict = new Dictionary<SpellTypes, Spell>();
        Fireball = new DamageSpell()
        {
            Name = "Fireball",
            Id = "fireball",
            SpellType = SpellTypes.Fireball,
            Description = "Deals damage in an area",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(6),
            AreaOfEffect = 1,
            Tier = 1,
            Resistable = true,
            DamageType = DamageTypes.Fire,
            Damage = (a, t) => 5 + a.Unit.GetStat(Stat.Mind) / 10,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(Fireball, t);
                TacticalGraphicalEffects.CreateFireBall(a.Position, t.Position, t);
            },
            OnExecuteTile = (a, l) =>
            {
                a.CastOffensiveSpell(Fireball, null, l);
                TacticalGraphicalEffects.CreateFireBall(a.Position, l, null);
            },
        };
        SpellDict[SpellTypes.Fireball] = Fireball;

        PowerBolt = new DamageSpell()
        {
            Name = "Power Bolt",
            Id = "power-bolt",
            SpellType = SpellTypes.PowerBolt,
            Description = "Deals damage to a single target, not resistable",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(6),
            AreaOfEffect = 0,
            Tier = 1,
            Resistable = false,
            Damage = (a, t) => 1 + a.Unit.GetStat(Stat.Mind) / 5,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(PowerBolt, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t);
            },
        };
        SpellDict[SpellTypes.PowerBolt] = PowerBolt;

        Icicle = new DamageSpell()
        {
            Name = "Icicle",
            Id = "icicle",
            SpellType = SpellTypes.Icicle,
            Description = "Deals ice damage to a single target, 1 in 3 chance to freeze target. Frozen units cannot take any actions or dodge, but take reduced damage and are slightly harder to eat.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(8),
            AreaOfEffect = 0,
            Tier = 1,
            Resistable = true,
            DamageType = DamageTypes.Ice,
            Damage = (a, t) => 5 + a.Unit.GetStat(Stat.Mind) / 10,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(Icicle, t);
                TacticalGraphicalEffects.CreateIcicle(a.Position, t.Position, t);
                State.GameManager.SoundManager.PlaySpellCast(PowerBolt, a);
                if (State.Rand.Next(3) == 0)
                {
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{t.Unit.Name} Was frozen solid!");
                    t.Unit.ApplyStatusEffect(StatusEffectType.Frozen, 1f, 2);
                }
            },
        };
        SpellDict[SpellTypes.Icicle] = Icicle;

        LightningBolt = new DamageSpell()
        {
            Name = "Lightning Bolt",
            Id = "lightning-bolt",
            SpellType = SpellTypes.LightningBolt,
            Description = "Deals electric damage to a single target, long range",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(40),
            AreaOfEffect = 0,
            Tier = 1,
            Resistable = true,
            DamageType = DamageTypes.Elec,
            Damage = (a, t) => 5 + a.Unit.GetStat(Stat.Mind) / 10,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(LightningBolt, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t);
            },
        };
        SpellDict[SpellTypes.LightningBolt] = LightningBolt;

        JoltCrash = new DamageSpell()
        {
            Name = "Jolt Crash",
            Id = "joltcrash",
            SpellType = SpellTypes.JoltCrash,
            Description = "Deals electric damage to a single target knocking them back and applying static, which increases all electric damage they recive",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(1),
            AreaOfEffect = 0,
            Tier = 1,
            Resistable = true,
            ResistanceMult = .90f,
            DamageType = DamageTypes.Elec,
            Damage = (a, t) => 6 + a.Unit.GetStat(Stat.Mind) / 10,
            OnExecute = (a, t) =>
            {
                float crashDamage = 1.2f;
                int curr = t.Unit.Health;
                a.CastOffensiveSpell(JoltCrash, t);
                bool didSpellHit = curr != t.Unit.Health;
                if (didSpellHit)
                {
                    TacticalUtilities.CheckKnockBack(a, t, ref crashDamage);
                    TacticalUtilities.KnockBack(a, t);
                    t.Unit.ApplyStatusEffect(StatusEffectType.Static, 1f, 2);
                    State.GameManager.SoundManager.PlaySpellCast(LightningBolt, a);
                }
                else
                {State.GameManager.SoundManager.PlaySwing(a);}
            },
        };
        SpellDict[SpellTypes.JoltCrash] = JoltCrash;

        Meditate = new StatusSpell()
        {
            Name = "Meditate",
            Id = "meditate",
            SpellType = SpellTypes.Meditate,
            Description = "Unit recovers 10 Mana and gains a stack of focus",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Self },
            Range = new Range(1),
            Tier = -1,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                a.CastSpell(Meditate, t);
                t.Unit.RestoreMana(10);
                t.Unit.AddFocus(1);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Managen);
            },
        };
        SpellDict[SpellTypes.Meditate] = Meditate;

        Shield = new StatusSpell()
        {
            Name = "Shield",
            Id = "shield",
            SpellType = SpellTypes.Shield,
            Description = "Causes target to take reduced damage",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(6),
            Duration = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => .25f,
            Type = StatusEffectType.Shielded,
            Tier = 1,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Shield, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Buff);
            },
        };
        SpellDict[SpellTypes.Shield] = Shield;

        Mending = new StatusSpell()
        {
            Name = "Mending",
            Id = "mending",
            SpellType = SpellTypes.Mending,
            Description = "Heals target over time",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(4),
            Duration = (a, t) => 1 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => a.Unit.GetStat(Stat.Mind),
            Type = StatusEffectType.Mending,
            Tier = 1,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Mending, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Heal);
            },
        };
        SpellDict[SpellTypes.Mending] = Mending;

        Speed = new StatusSpell()
        {
            Name = "Speed",
            Id = "speed",
            SpellType = SpellTypes.Speed,
            Description = "Makes target faster",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(4),
            Duration = (a, t) => 3 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => .3f + a.Unit.GetStat(Stat.Mind) / 1000,
            Type = StatusEffectType.Fast,
            Tier = 1,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Speed, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Buff);
            },
        };
        SpellDict[SpellTypes.Speed] = Speed;

        Valor = new StatusSpell()
        {
            Name = "Valor",
            Id = "valor",
            SpellType = SpellTypes.Valor,
            Description = "Target does more damage",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(6),
            Duration = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => .25f,
            Type = StatusEffectType.Valor,
            Tier = 1,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Valor, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Buff);
            },
        };
        SpellDict[SpellTypes.Valor] = Valor;

        Predation = new StatusSpell()
        {
            Name = "Predation",
            Id = "predation",
            SpellType = SpellTypes.Predation,
            Description = "Target is better at vore",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(6),
            Duration = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => a.Unit.GetStat(Stat.Mind) / 4,
            Type = StatusEffectType.Predation,
            Tier = 1,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Predation, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Buff);
            },
        };
        SpellDict[SpellTypes.Predation] = Predation;

        IceBlast = new DamageSpell()
        {
            Name = "Ice Blast",
            Id = "ice-blast",
            SpellType = SpellTypes.IceBlast,
            Description = "Area of effect cold damage and lays down ice",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(6),
            AreaOfEffect = 1,
            Tier = 2,
            Resistable = true,
            DamageType = DamageTypes.Ice,
            Damage = (a, t) => 5 + a.Unit.GetStat(Stat.Mind) / 10,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(IceBlast, t);
                TacticalUtilities.CreateEffect(t.Position, TileEffectType.IcePatch, 1, 2, 10 + a.Unit.GetStat(Stat.Mind) / 5);
                TacticalGraphicalEffects.CreateIceBlast(t.Position);
            },
            OnExecuteTile = (a, l) =>
            {
                a.CastOffensiveSpell(IceBlast, null, l);
                TacticalUtilities.CreateEffect(l, TileEffectType.IcePatch, 1, 2, 10 + a.Unit.GetStat(Stat.Mind) / 5);
                TacticalGraphicalEffects.CreateIceBlast(l);
            },
        };
        SpellDict[SpellTypes.IceBlast] = IceBlast;

        Pyre = new DamageSpell()
        {
            Name = "Pyre",
            Id = "pyre",
            SpellType = SpellTypes.Pyre,
            Description = "Deals damage in an area, sets the ground on fire for a few turns",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(6),
            AreaOfEffect = 1,
            Tier = 2,
            Resistable = true,
            DamageType = DamageTypes.Fire,
            Damage = (a, t) => 7 + a.Unit.GetStat(Stat.Mind) / 10,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(Pyre, t);
                TacticalUtilities.CreateEffect(t.Position, TileEffectType.Fire, 1, 1 + a.Unit.GetStat(Stat.Mind) / 30, 4);
                State.GameManager.SoundManager.PlaySpellCast(Fireball, a);
            },
            OnExecuteTile = (a, l) =>
            {
                a.CastOffensiveSpell(Pyre, null, l);
                TacticalUtilities.CreateEffect(l, TileEffectType.Fire, 1, 1 + a.Unit.GetStat(Stat.Mind) / 30, 4);
                State.GameManager.SoundManager.PlaySpellCast(Fireball, a);
            },
        };
        SpellDict[SpellTypes.Pyre] = Pyre;

        CrossShock = new DamageSpell()
        {
            Name = "Cross Shock",
            Id = "crossshock",
            SpellType = SpellTypes.CrossShock,
            Description = "Deals electric damage in a 'X' pattern and applies 'static' to hit units, which increases all electric damage they recive",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(6),
            AOEType = AreaOfEffectType.FixedPattern,
            Tier = 2,
            Pattern = new int[3, 3] { { 1, 0, 1 }, { 0, 1, 0 }, { 1, 0, 1 } },
            Resistable = true,
            DamageType = DamageTypes.Elec,
            Damage = (a, t) => 8 + a.Unit.GetStat(Stat.Mind) / 10,
            OnExecute = (a, t) =>
            {
                int curr = 0;
                TacticalGraphicalEffects.CreateCrossShock(t.Position);
                State.GameManager.SoundManager.PlaySpellCast(LightningBolt, a);
                foreach (var splashTarget in TacticalUtilities.UnitsWithinPattern(t.Position, CrossShock.Pattern))
                {
                    curr = splashTarget.Unit.Health;
                }
                a.CastOffensiveSpell(CrossShock, t);
                foreach (var splashTarget in TacticalUtilities.UnitsWithinPattern(t.Position, CrossShock.Pattern))
                {
                    bool didSpellHit = (curr != splashTarget.Unit.Health);
                    if (didSpellHit)
                    {
                        splashTarget.Unit.ApplyStatusEffect(StatusEffectType.Static, 1f, 2);
                    }
                }
            },
            OnExecuteTile = (a, l) =>
            {
                int curr = 0;
                TacticalGraphicalEffects.CreateCrossShock(l);
                State.GameManager.SoundManager.PlaySpellCast(LightningBolt, a);
                foreach (var splashTarget in TacticalUtilities.UnitsWithinPattern(l, CrossShock.Pattern))
                {
                    curr = splashTarget.Unit.Health;
                }
                a.CastOffensiveSpell(CrossShock, null, l);
                foreach (var splashTarget in TacticalUtilities.UnitsWithinPattern(l, CrossShock.Pattern))
                {
                    bool didSpellHit = (curr != splashTarget.Unit.Health);
                    if (didSpellHit)
                    {
                        splashTarget.Unit.ApplyStatusEffect(StatusEffectType.Static, 1f, 2);
                    }
                }
            },
        };
        SpellDict[SpellTypes.CrossShock] = CrossShock;

        ExplosiveHug = new DamageSpell()
        {
            Name = "Explosive Hug",
            Id = "explosivehug",
            SpellType = SpellTypes.ExplosiveHug,
            Description = "Unit detonates, killing itself and dealing 2/3 of it's current HP in damage to targeted unit",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(1),
            Tier = -1,
            Resistable = true,
            ResistanceMult = .50f,
            Damage = (a, t) => (a.Unit.Health / 3) * 2,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(ExplosiveHug, t);
                if(a.Unit.HasTrait(Traits.Fearless))//Auto-surrender prevention
                    a.CastOffensiveSpell(Explode, a);
                else
                {
                    a.Unit.AddPermanentTrait(Traits.Fearless);
                    a.CastOffensiveSpell(Explode, a);
                    a.Unit.RemoveTrait(Traits.Fearless);
                }
                State.GameManager.SoundManager.PlaySpellCast(Fireball, a);
            },
        };
        SpellDict[SpellTypes.ExplosiveHug] = ExplosiveHug;

        Explode = new DamageSpell()
        {
            Name = "Explosive Hug",
            Id = "explode",
            SpellType = SpellTypes.Explode,
            Description = "Used to kill the exploding unit.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Self },
            Range = new Range(1),
            Tier = -1,
            Resistable = false,
            Damage = (a, t) => a.Unit.Health + 5,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(Explode, a);
            },
        };
        SpellDict[SpellTypes.Explode] = Explode;

        //Warp = new Spell() //Implemented this and forgot it was supposed to be target and then location, only the caster makes it highly situational
        //{
        //    Name = "Warp",
        //    SpellType = SpellTypes.Warp,
        //    Description = "Teleports the Unit to the target tile",
        //    AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Tile },
        //    Range = new Range(16),
        //    Tier = 2,
        //    Resistable = false,
        //    OnExecuteTile = (a, loc) =>
        //    {
        //        if (TacticalUtilities.OpenTile(loc, null) && a.CastSpell(Warp, null))
        //        {
        //            a.Position.x = loc.x;
        //            a.Position.y = loc.y;
        //            TacticalUtilities.UpdateActorLocations();
        //        }               
        //    },
        //};
        //SpellDict[SpellTypes.Warp] = Warp;

        Poison = new StatusSpell()
        {
            Name = "Poison",
            Id = "poison",
            SpellType = SpellTypes.Poison,
            Description = "Deals damage over time, can not kill targets",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(8),
            Duration = (a, t) => 4 + a.Unit.GetStat(Stat.Mind) / 5,
            Effect = (a, t) => 1 + a.Unit.GetStat(Stat.Mind) / 20,
            Type = StatusEffectType.Poisoned,
            Tier = 2,
            Resistable = true,
            OnExecute = (a, t) =>
            {
                if (a.CastStatusSpell(Poison, t))
                    TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Poison);
            },
        };
        SpellDict[SpellTypes.Poison] = Poison;

        Trance = new StatusSpell()
        {
            Name = "Trance",
            Id = "trance",
            SpellType = SpellTypes.Trance,
            Description = "Puts target to sleep, duration scales with mind",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(6),
            Duration = (a, t) => 1 + a.Unit.GetStat(Stat.Mind) / 20,
            Effect = (a, t) => 1,
            Type = StatusEffectType.Sleeping,
            Tier = 2,
            Resistable = true,
            ResistanceMult = 1.10f,
            OnExecute = (a, t) =>
            {
                if (a.CastStatusSpell(Trance, t))
                    TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Debuff);
            },
        };
        SpellDict[SpellTypes.Trance] = Trance;

        FlameWave = new DamageSpell()
        {
            Name = "Flame Wave",
            Id = "FlameWave",
            SpellType = SpellTypes.FlameWave,
            Description = "Creates a 3 tile wide wall of flame next to the user",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(1),
            AOEType = AreaOfEffectType.RotatablePattern,
            Tier = 1,
            Pattern = new int[3, 3] { { 0, 0, 0 }, { 1, 1, 1 }, { 0, 0, 0 } },
            Resistable = true,
            DamageType = DamageTypes.Fire,
            Damage = (a, t) => 5 + a.Unit.GetStat(Stat.Mind) / 7,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(FlameWave, t);
                TacticalUtilities.CreateEffectWithPattern(t.Position, a.Position, TileEffectType.Fire, 1 + a.Unit.GetStat(Stat.Mind) / 30, 4, FlameWave.Pattern, FlameWave.AOEType);
                State.GameManager.SoundManager.PlaySpellCast(Fireball, a);
            },
            OnExecuteTile = (a, l) =>
            {
                a.CastOffensiveSpell(FlameWave, null, l);
                TacticalUtilities.CreateEffectWithPattern(l, a.Position, TileEffectType.Fire, 1 + a.Unit.GetStat(Stat.Mind) / 30, 4, FlameWave.Pattern, FlameWave.AOEType);
                State.GameManager.SoundManager.PlaySpellCast(Fireball, a);
            },
        };
        SpellDict[SpellTypes.FlameWave] = FlameWave;

        FireBomb = new DamageSpell()
        {
            Name = "Fire Bomb",
            Id = "FireBomb",
            SpellType = SpellTypes.FireBomb,
            Description = "A heavy incendiary explosive that detonates in a cross pattern(Damage scales with Dexterity)",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(5),
            AOEType = AreaOfEffectType.FixedPattern,
            Tier = 3,
            Pattern = new int[3, 3] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } },
            Resistable = true,
            ResistanceMult = .80f,
            DamageType = DamageTypes.Fire,
            Damage = (a, t) => 8 + a.Unit.GetStat(Stat.Dexterity) / 9,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(FireBomb, t);
                TacticalGraphicalEffects.CreateFireBomb(a.Position, t.Position, t);
            },
            OnExecuteTile = (a, l) =>
            {
                a.CastOffensiveSpell(FireBomb, null, l);
                TacticalGraphicalEffects.CreateFireBomb(a.Position, l, null);
            },
        };
        SpellDict[SpellTypes.FireBomb] = FireBomb;

        Bolas = new StatusSpell()
        {
            Name = "Bolas",
            Id = "bolas",
            SpellType = SpellTypes.Bolas,
            Description = "Ensnares the target, lowering their movement to 1 for a few turns",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(7),
            Duration = (a, t) => 2,
            Effect = (a, t) => 1,
            Type = StatusEffectType.Snared,
            Tier = 2,
            Resistable = true,
            ResistanceMult = 0.4f,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Bolas, t);
                TacticalGraphicalEffects.CreateBola(a.Position, t.Position, t);
                State.GameManager.SoundManager.PlaySwing(a);
            },

        };
        SpellDict[SpellTypes.Bolas] = Bolas;

        CaptureNet = new Spell()
        {
            Name = "Capture Net",
            Id = "captureNet",
            SpellType = SpellTypes.CaptureNet,
            Description = "Attempts to capture the target with a net converting them to the user's side. Target must be at 5 health to capture otherwise will deal chip damage. (Doesn't work on summons or leaders)",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(5),
            Tier = -1,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                a.CastSpell(CaptureNet, t);
                TacticalGraphicalEffects.CreateCaptureNet(a.Position, t.Position, t);
                State.GameManager.SoundManager.PlaySwing(a);
                if (t.Unit.Health <= 5 && t.Unit.CanBeConverted())
                {
                    if (State.Rand.Next(10) == 1)
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{t.Unit.Name} barely managed to shake off the net!");
                    else
                    {
                        State.GameManager.TacticalMode.SwitchAlignment(t);
                        t.Surrendered = true;
                        t.Movement = 0;
                        t.AIAvoidEat = 2;
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{a.Unit.Name} captured {t.Unit.Name}!");
                    }
                }
                else
                {
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{a.Unit.Name} hit {t.Unit.Name} in the face with the net dealing minimal damage.");
                    t.Unit.Heal(-2);
                }
            },
        };
        SpellDict[SpellTypes.CaptureNet] = CaptureNet;

        ForcePulse = new DamageSpell()
        {
            Name = "Force Pulse",
            Id = "forcepulse",
            SpellType = SpellTypes.ForcePulse,
            Description = "Deals damage in an area and knocks back enemies. ALL enemies are knocked back even if the attack misses.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(6),
            AreaOfEffect = 1,
            Tier = 1,
            Resistable = true,
            Damage = (a, t) => 3 + a.Unit.GetStat(Stat.Mind) / 10,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(ForcePulse, t);
                float pulseDamage = 1.2f;
                foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(t.Position, ForcePulse.AreaOfEffect))
                {
                    TacticalUtilities.CheckSpellKnockBack(t.Position, a, splashTarget, ref pulseDamage);
                    TacticalUtilities.SpellKnockBack(t.Position, a, splashTarget);
                }
                TacticalUtilities.CheckKnockBack(a, t, ref pulseDamage);
                TacticalUtilities.KnockBack(a, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t);
                State.GameManager.SoundManager.PlaySpellCast(PowerBolt, a);
            },
            OnExecuteTile = (a, l) =>
            {
                a.CastOffensiveSpell(ForcePulse, null, l);
                float pulseDamage = 1.2f;
                foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(l, ForcePulse.AreaOfEffect))
                {
                    TacticalUtilities.CheckSpellKnockBack(l, a, splashTarget, ref pulseDamage);
                    TacticalUtilities.SpellKnockBack(l, a, splashTarget);
                }
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, l, null);
                State.GameManager.SoundManager.PlaySpellCast(PowerBolt, a);
            },
        };
        SpellDict[SpellTypes.ForcePulse] = ForcePulse;

        Bloodrite = new StatusSpell()
        {
            Name = "Bloodrite",
            Id = "bloodrite",
            SpellType = SpellTypes.Bloodrite,
            Description = "Target sacrifices 1/2 of their current hp for 150% bonus melee damage and a 10% bonus ranged damage for 10 turns",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Self },
            Range = new Range(1),
            Tier = 0,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                a.CastSpell(Bloodrite, t);
                a.Unit.UseableSpells.Remove(SpellList.Bloodrite);
                t.Unit.StatusEffects.Add(new StatusEffect(StatusEffectType.Bloodrite, 1f, 11));
                t.Unit.Heal(-(t.Unit.Health) / 2);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Buff);
            },
        };
        SpellDict[SpellTypes.Bloodrite] = Bloodrite;

        PreysCurse = new StatusSpell()
        {
            Name = "Prey's Curse",
            Id = "preys-curse",
            SpellType = SpellTypes.PreysCurse,
            Description = "Makes target into a semi-willing prey, increasing vore chance on them (and changing some flavor text)",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(6),
            Duration = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => 0,
            Type = StatusEffectType.WillingPrey,
            Tier = 3,
            Resistable = true,
            OnExecute = (a, t) =>
            {
                if (a.CastStatusSpell(PreysCurse, t))
                    TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Debuff);
            },
        };
        SpellDict[SpellTypes.PreysCurse] = PreysCurse;

        Maw = new Spell()
        {
            Name = "Maw",
            Id = "maw",
            SpellType = SpellTypes.Maw,
            Description = "Attempts to teleport the target into caster's belly, adding caster's Power to Voracity.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(4),
            Tier = 3,
            Resistable = true,
            OnExecute = (a, t) => a.CastMawWithLocation(Maw, t),
        };
        SpellDict[SpellTypes.Maw] = Maw;

        Charm = new StatusSpell()
        {
            Name = "Charm",
            Id = "charm",
            SpellType = SpellTypes.Charm,
            Description = "Has a chance to temporarily make an enemy fight for the caster.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(6),
            Duration = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => a.Unit.GetApparentSide(t.Unit),         // the unit will act to the best of its knowledge
            Type = StatusEffectType.Charmed,
            Tier = 3,
            Resistable = true,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Charm, t);
                TacticalGraphicalEffects.CreateHeartProjectile(a.Position, t.Position, t);
            },
        };
        SpellDict[SpellTypes.Charm] = Charm;

        Summon = new Spell()
        {
            Name = "Summon",
            Id = "summon",
            SpellType = SpellTypes.Summon,
            Description = "Summons a random monster at 50 % of caster’s experience (Available monsters depend on what monsters are available to hire as mercs, or set to spawn, so that monsters you aren't interested in don't spawn.)",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Tile },
            Range = new Range(4),
            Tier = 3,
            Resistable = false,
            OnExecuteTile = (a, loc) =>
            {
                if (TacticalUtilities.OpenTile(loc, null) && a.CastSpell(Summon, null))
                {
                    var AvailableRaces = new List<Race>();
                    foreach (Race race in (Race[])System.Enum.GetValues(typeof(Race)))
                    {
                        if (race >= Race.Vagrants && race < Race.Selicia && (Config.World.GetValue($"Merc {race}") || (Config.SpawnerInfoWithoutGeneration(race)?.Enabled ?? false)))
                            AvailableRaces.Add(race);
                    }
                    AvailableRaces.Remove(Race.Dragon);
                    if (AvailableRaces.Any() == false)
                        AvailableRaces.Add(Race.Vagrants);
                    Race summonRace = AvailableRaces[State.Rand.Next(AvailableRaces.Count())];
                    Unit unit = new Unit(a.Unit.Side, summonRace, (int)(a.Unit.Experience * .50f), true, UnitType.Summon);
                    var actorCharm = a.Unit.GetStatusEffect(StatusEffectType.Charmed) ?? a.Unit.GetStatusEffect(StatusEffectType.Hypnotized);
                    if (actorCharm != null)
                    {
                        unit.ApplyStatusEffect(StatusEffectType.Charmed, actorCharm.Strength, actorCharm.Duration);
                    }

                    StrategicUtilities.SpendLevelUps(unit);
                    State.GameManager.TacticalMode.AddUnitToBattle(unit, loc);
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{a.Unit.Name}</b> has summoned the {InfoPanel.RaceSingular(unit)} <b>{unit.Name}</b>.");
                }
            },
        };
        SpellDict[SpellTypes.Summon] = Summon;


        SummonDoppelganger = new Spell()
        {
            Name = "Summon Doppelganger",
            Id = "summondoppelganger",
            SpellType = SpellTypes.SummonDoppelganger,
            Description = "Summons a feral copy of the caster at 50 % of the real caster’s experience.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Tile },
            Range = new Range(4),
            Tier = 3,
            Resistable = false,
            OnExecuteTile = (a, loc) =>
            {
                if (TacticalUtilities.OpenTile(loc, null) && a.CastSpell(SummonDoppelganger, null))
                {
                    Unit unit = new Unit(a.Unit.Side, a.Unit.Race, (int)(a.Unit.Experience * .50f), true, UnitType.Summon);
                    unit.Name = a.Unit.Name;
                    foreach (Traits trait in a.Unit.GetTraits)
                    {
                        unit.AddTrait(trait);
                    }
                    unit.CopyAppearance(a.Unit);
                    unit.AddTrait(Traits.Feral);
                    var actorCharm = a.Unit.GetStatusEffect(StatusEffectType.Charmed) ?? a.Unit.GetStatusEffect(StatusEffectType.Hypnotized);
                    if (actorCharm != null)
                    {
                        unit.ApplyStatusEffect(StatusEffectType.Charmed, actorCharm.Strength, actorCharm.Duration);
                    }
                    StrategicUtilities.SpendLevelUps(unit);
                    State.GameManager.TacticalMode.AddUnitToBattle(unit, loc);
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{a.Unit.Name}</b> has summoned a Doppelganger!");
                    State.GameManager.SoundManager.PlaySpellCast(Summon, a);
                }
            },
        };
        SpellDict[SpellTypes.SummonDoppelganger] = SummonDoppelganger;

        SummonSpawn = new Spell()
        {
            Name = "Summon Spawn",
            Id = "summonspawn",
            SpellType = SpellTypes.SummonSpawn,
            Description = "Summons a feral spawn of the caster's race at 50 % of the caster’s experience.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Tile },
            Range = new Range(4),
            Tier = 3,
            Resistable = false,
            OnExecuteTile = (a, loc) =>
            {
                if (TacticalUtilities.OpenTile(loc, null) && a.CastSpell(SummonSpawn, null))
                {
                    Race spawnRace = a.Unit.DetermineSpawnRace();
                    Race truespawnRace = a.Unit.HiddenUnit.DetermineSpawnRace();
                    if (spawnRace != truespawnRace)
                        spawnRace = a.Unit.HiddenUnit.DetermineSpawnRace();
                    Unit unit = new Unit(a.Unit.Side, spawnRace, (int)(a.Unit.Experience * .50f), true, UnitType.Summon);
                    unit.AddTrait(Traits.Feral);
                    var actorCharm = a.Unit.GetStatusEffect(StatusEffectType.Charmed) ?? a.Unit.GetStatusEffect(StatusEffectType.Hypnotized);
                    if (actorCharm != null)
                    {
                        unit.ApplyStatusEffect(StatusEffectType.Charmed, actorCharm.Strength, actorCharm.Duration);
                    }
                    StrategicUtilities.SpendLevelUps(unit);
                    State.GameManager.TacticalMode.AddUnitToBattle(unit, loc);
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{a.Unit.Name}</b> has summoned a spawn.");
                    State.GameManager.SoundManager.PlaySpellCast(Summon, a);
                }
            },
        };
        SpellDict[SpellTypes.SummonSpawn] = SummonSpawn;


        Reanimate = new Spell()
        {
            Name = "Reanimate",
            Id = "Reanimate",
            SpellType = SpellTypes.Reanimate,
            Description = "Brings back any unit that died this battle as a summon under the caster's control",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Tile },
            Range = new Range(4),
            Tier = 3,
            Resistable = false,
            OnExecuteTile = (a, loc) =>
            {
                var target = TacticalUtilities.FindUnitToReanimate(a);
                if (target != null)
                {
                    if (TacticalUtilities.OpenTile(loc, null) && a.CastSpell(Reanimate, null))
                    {
                        if (State.GameManager.TacticalMode.IsPlayerInControl && State.GameManager.CurrentScene == State.GameManager.TacticalMode)
                        {
                            TacticalUtilities.CreateReanimationPanel(loc, a.Unit);
                        }
                        else
                        {
                            State.GameManager.SoundManager.PlaySpellCast(Summon, a);
                            TacticalUtilities.Reanimate(loc, target, a.Unit);
                        }
                    }
                }

            },
        };
        SpellDict[SpellTypes.Reanimate] = Reanimate;

        Enlarge = new StatusSpell()
        {
            Name = "Enlarge",
            Id = "enlarge",
            SpellType = SpellTypes.Enlarge,
            Description = "Makes target unit 20% bigger, increasing stats and increasing body size and stomach size",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(4),
            Tier = 3,
            Resistable = false,
            Duration = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 15,
            Type = StatusEffectType.Enlarged,
            Effect = (a, t) => .2f,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Enlarge, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Buff);
            },
        };
        SpellDict[SpellTypes.Enlarge] = Enlarge;


        Diminishment = new StatusSpell()
        {
            Name = "Diminishment",
            Id = "diminishment",
            SpellType = SpellTypes.Diminishment,
            Description = "Reduces target in size greatly, cutting all its statistics (including size) by 75%.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(6),
            Duration = (a, t) => 1 + a.Unit.GetStat(Stat.Mind) / 20,
            Effect = (a, t) => .75f,
            Type = StatusEffectType.Diminished,
            Tier = 4,
            Resistable = true,
            ResistanceMult = 1.5f,
            OnExecute = (a, t) => a.CastStatusSpell(Diminishment, t),
        };
        SpellDict[SpellTypes.Diminishment] = Diminishment;

        GateMaw = new Spell()
        {
            Name = "Gate Maw",
            Id = "gate-maw",
            SpellType = SpellTypes.GateMaw,
            Description = "Attempts to teleport all targets in radius into caster's belly, adding caster's Power to Voracity. If caster doesn't have enough stomach space, targets are unaffected.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(4),
            Tier = 4,
            AreaOfEffect = 1,
            Resistable = true,
            OnExecute = (a, t) => a.CastMawWithLocation(GateMaw, t),
            OnExecuteTile = (a, l) => a.CastMawWithLocation(GateMaw, null, l),
        };
        SpellDict[SpellTypes.GateMaw] = GateMaw;


        Resurrection = new Spell()
        {
            Name = "Resurrection",
            Id = "resurrection",
            SpellType = SpellTypes.Resurrection,
            Description = "Resurrects the highest exp unit that died during this battle",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Tile },
            Range = new Range(4),
            Tier = 4,
            Resistable = false,
            OnExecuteTile = (a, loc) =>
            {
                var target = TacticalUtilities.FindUnitToResurrect(a);
                if (target != null)
                {
                    if (TacticalUtilities.OpenTile(loc, null) && a.CastSpell(Resurrection, null))
                    {
                        if (State.GameManager.TacticalMode.IsPlayerInControl && State.GameManager.CurrentScene == State.GameManager.TacticalMode)
                        {
                            TacticalUtilities.CreateResurrectionPanel(loc, a.Unit.Side);
                        }
                        else
                        {
                            TacticalUtilities.Resurrect(loc, target);
                        }
                    }
                }

            },
        };
        SpellDict[SpellTypes.Resurrection] = Resurrection;

        ViperPoisonDamage = new DamageSpell()
        {
            Name = "Viper Spit",
            Id = "viperDamage",
            SpellType = SpellTypes.ViperDamage,
            Description = "(Designed as part of the Viper spit spell)",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(6),
            AreaOfEffect = 1,
            Tier = 0,
            Resistable = true,
            DamageType = DamageTypes.Poison,
            Damage = (a, t) => 5 + a.Unit.GetStat(Stat.Mind) / 10,
            OnExecute = (a, t) =>
            {

            },
            OnExecuteTile = (a, l) =>
            {

            },
        };
        SpellDict[SpellTypes.ViperDamage] = ViperPoisonDamage;

        ViperPoisonStatus = new StatusSpell()
        {
            Name = "Poison Spit",
            Id = "viper-poison",
            SpellType = SpellTypes.ViperPoison,
            Description = "Deals damage and applies a strong short lived poison in a 3x3 area",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(6),
            Duration = (a, t) => 4,
            Effect = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 10,
            AreaOfEffect = 1,
            Type = StatusEffectType.Poisoned,
            Tier = 0,
            Resistable = true,
            ResistanceMult = 1f,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(ViperPoisonStatus, t);
                a.Unit.RestoreMana(2);
                a.CastOffensiveSpell(ViperPoisonDamage, t);
                TacticalGraphicalEffects.CreatePoisonCloud(t.Position);
            },
            OnExecuteTile = (a, loc) =>
            {
                a.CastStatusSpell(ViperPoisonStatus, null, loc);
                a.Unit.RestoreMana(2);
                a.CastOffensiveSpell(ViperPoisonDamage, null, loc);
                TacticalGraphicalEffects.CreatePoisonCloud(loc);
            }

        };
        SpellDict[SpellTypes.ViperPoison] = ViperPoisonStatus;

        AlraunePuff = new StatusSpell()
        {
            Name = "Pollen Cloud",
            Id = "alraune-spell",
            SpellType = SpellTypes.AlraunePuff,
            Description = "Applies a variety of status effects in a 3x3 area",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(8),
            Duration = (a, t) => 3,
            Effect = (a, t) => .15f,
            AreaOfEffect = 1,
            Type = StatusEffectType.Shaken,
            Tier = 0,
            Resistable = true,
            ResistanceMult = 1f,
            Alraune = true,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(AlraunePuff, t);
                TacticalGraphicalEffects.CreatePollenCloud(t.Position);
            },
            OnExecuteTile = (a, loc) =>
            {
                a.CastStatusSpell(AlraunePuff, null, loc);
                TacticalGraphicalEffects.CreatePollenCloud(loc);
            }

        };
        SpellDict[SpellTypes.AlraunePuff] = AlraunePuff;

        Web = new StatusSpell()
        {
            Name = "Web",
            Id = "web",
            SpellType = SpellTypes.Web,
            Description = "Webs the target, lowering their movement to 1 for a few turns and lowering their stats",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(4),
            Duration = (a, t) => 3,
            Effect = (a, t) => 1,
            Type = StatusEffectType.Webbed,
            Tier = 0,
            Resistable = true,
            ResistanceMult = 0.55f,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Web, t);
                TacticalGraphicalEffects.CreateSpiderWeb(a.Position, t.Position, t);
            },

        };
        SpellDict[SpellTypes.Web] = Web;

        GlueBomb = new StatusSpell()
        {
            Name = "Glue Bomb",
            Id = "glueBomb",
            SpellType = SpellTypes.GlueBomb,
            Description = "Glues a 3x3 area, greatly slowing them down for a significant number of turns.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(8),
            Duration = (a, t) => 8,
            Effect = (a, t) => 1,
            AreaOfEffect = 1,
            Type = StatusEffectType.Glued,
            Tier = 0,
            Resistable = true,
            ResistanceMult = 0.45f,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(GlueBomb, t);
                TacticalGraphicalEffects.CreateGlueBomb(a.Position, t.Position);
            },
            OnExecuteTile = (a, loc) =>
            {
                a.CastStatusSpell(GlueBomb, null, loc);
                TacticalGraphicalEffects.CreateGlueBomb(a.Position, loc);
            }

        };
        SpellDict[SpellTypes.GlueBomb] = GlueBomb;

        Petrify = new StatusSpell()
        {
            Name = "Petrify",
            Id = "petrify",
            SpellType = SpellTypes.Petrify,
            Description = "Petrifies the target, preventing them from taking any action or dodging, but reducing damage taken and making them harder to eat.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(4),
            Duration = (a, t) => 4,
            Effect = (a, t) => 1,
            Type = StatusEffectType.Petrify,
            Tier = 0,
            Resistable = true,
            ResistanceMult = .9f,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Petrify, t);
            },

        };
        SpellDict[SpellTypes.Petrify] = Petrify;

        HypnoGas = new StatusSpell()
        {
            Name = "Hypnotic Gas",
            Id = "hypno-fart",
            SpellType = SpellTypes.HypnoGas,
            Description = "Applies Hypnotized in a 4x4 area near the caster. Hypnotized units become noncombatants that serve the caster's side.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Tile, AbilityTargets.Enemy },
            Range = new Range(1),
            Duration = (a, t) => 5,
            Effect = (a, t) => a.Unit.GetApparentSide(t.Unit),
            AreaOfEffect = 1,
            Type = StatusEffectType.Hypnotized,
            Tier = 0,
            Resistable = true,
            ResistanceMult = 0.5f,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(HypnoGas, t, null, Stat.Voracity);
                if (Config.FartOnAbsorb)
                {
                    a.SetPredMode(PreyLocation.anal);
                    State.GameManager.SoundManager.PlayFart(a);
                }
                else if (Config.BurpFraction > 0)
                {
                    a.SetPredMode(PreyLocation.stomach);
                    State.GameManager.SoundManager.PlayBurp(a);
                }
                TacticalGraphicalEffects.CreateGasCloud(t.Position);
            },
            OnExecuteTile = (a, loc) =>
            {
                a.CastStatusSpell(HypnoGas, null, loc, Stat.Voracity);
                if (Config.FartOnAbsorb)
                {
                    a.SetPredMode(PreyLocation.anal);
                    State.GameManager.SoundManager.PlayFart(a);
                }
                else if (Config.BurpFraction > 0)
                {
                    a.SetPredMode(PreyLocation.stomach);
                    State.GameManager.SoundManager.PlayBurp(a);
                }
                TacticalGraphicalEffects.CreateGasCloud(loc);
            }

        };
        SpellDict[SpellTypes.HypnoGas] = HypnoGas;

        Bind = new Spell()
        {
            Name = "Bind/Resummon",
            Id = "Bind",
            SpellType = SpellTypes.Bind,
            Description = "Allows to either take control of any summon, or re-summon the most recently bound one by targeting an empty space.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Ally, AbilityTargets.Tile },
            Range = new Range(4),
            Tier = 2,
            Resistable = false,
            OnExecute = (a, t) => a.CastBind(t),
            OnExecuteTile = (a, l) => a.SummonBound(l),
        };
        SpellDict[SpellTypes.Bind] = Bind;

        ForceFeed = new Spell()
        {
            Name = "Force Feed",
            Id = "force-feed",
            SpellType = SpellTypes.ForceFeed,
            Description = "",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(1),
            Tier = 0,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                TacticalUtilities.ForceFeed(a, t);
            },
        };
        SpellDict[SpellTypes.ForceFeed] = ForceFeed;

        RevertForm = new Spell()
        {
            Name = "Revert Form",
            Id = "revert-form",
            SpellType = SpellTypes.RevertForm,
            Description = "",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Self },
            Range = new Range(1),
            Tier = 0,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                TacticalUtilities.RevertForm(a, t);
            },
        };
        SpellDict[SpellTypes.RevertForm] = RevertForm;

        AssumeForm = new Spell()
        {
            Name = "Assume Form",
            Id = "assume-form",
            SpellType = SpellTypes.AssumeForm,
            Description = "Assumes the form of a random Prey",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Self },
            Range = new Range(1),
            Tier = 0,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                TacticalUtilities.AssumeForm(a, t);
            },
        };
        SpellDict[SpellTypes.AssumeForm] = AssumeForm;


        Whispers = new StatusSpell()
        {
            Name = "Whispers",
            Id = "whispers-spell",
            SpellType = SpellTypes.Whispers,
            Description = "Applies Charm, Prey's Curse, and Temptation for 3 rounds",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(1),
            Duration = (a, t) => 3,
            Effect = (a, t) => a.Unit.GetApparentSide(t.Unit),
            Type = StatusEffectType.Charmed,
            Tier = 3,
            Resistable = true,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(Whispers, t);
            },

        };
        SpellDict[SpellTypes.Whispers] = Whispers;

        ViralInfection = new StatusSpell()
        {
            Name = "ViralInfection",
            Id = "viralinfection",
            SpellType = SpellTypes.ViralInfection,
            Description = "Applies virus to enemy, dealing damage over time.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally, AbilityTargets.Enemy, AbilityTargets.Self},
            Range = new Range(8),
            Duration = (a, t) => 4 + a.Unit.GetStat(Stat.Mind) / 5,
            Effect = (a, t) => 1 + a.Unit.GetStat(Stat.Mind) / 10,
            Type = StatusEffectType.Virus,
            Tier = 2,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                if (a.CastStatusSpell(ViralInfection, t))
                    TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Poison);
            },
        };
        SpellDict[SpellTypes.ViralInfection] = ViralInfection;

        AmplifyMagic = new Spell()
        {
            Name = "Amplify Magic",
            Id = "amplify-magic",
            SpellType = SpellTypes.AmplifyMagic,
            Description = "Grant nearby units stacks of Focus, scaling with will.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(1),
            Tier = 0,
            AreaOfEffect = 1,
            Resistable = false,
            IsFree = true,
            OnExecute = (a, t) =>
            {
                a.CastSpell(AmplifyMagic, t);
                int amt = a.Unit.GetStat(Stat.Will) / 25;
                foreach (var ally in TacticalUtilities.UnitsWithinTiles(t.Position, 1).Where(s => s.Unit.IsDead == false && s.Unit.Side == a.Unit.Side))
                {
                    ally.Unit.AddFocus(((amt > 1) ? amt : 1));
                    TacticalGraphicalEffects.CreateGenericMagic(a.Position, ally.Position, ally, TacticalGraphicalEffects.SpellEffectIcon.Buff);
                }
            }
        };
        SpellDict[SpellTypes.AmplifyMagic] = AmplifyMagic;

        DivinitysEmbrace = new StatusSpell()
        {
            Name = "Divinity's Embrace",
            Id = "divinitysembrace",
            SpellType = SpellTypes.DivinitysEmbrace,
            Description = "Shower an ally in divine light, providing instant healing as well as damage mitigation for a few turns.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(4),
            Duration = (a, t) => 2 + a.Unit.GetStat(Stat.Mind) / 10,
            Effect = (a, t) => .25f,
            Type = StatusEffectType.DivineShield,
            Tier = 3,
            Resistable = false,
            OnExecute = (a, t) =>
            {
                a.CastStatusSpell(DivinitysEmbrace, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.Buff);
                t.Damage(-5 - a.Unit.GetStat(Stat.Mind));
            },
        };
        SpellDict[SpellTypes.DivinitysEmbrace] = DivinitysEmbrace;

        Evocation = new Spell()
        {
            Name = "Evocation",
            Id = "evocation",
            SpellType = SpellTypes.Evocation,
            Description = "Draw all nearby Unit's Focus onto a target, inspiring it. Grants the target equivalent SpellForce stacks and half as much MP.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Ally },
            Range = new Range(1),
            Tier = 0,
            AreaOfEffect = 1,
            Resistable = false,
            IsFree = true,
            OnExecute = (a, t) =>
            {
                a.CastSpell(Evocation, a);
                int stacks = 0;
                foreach (var ally in TacticalUtilities.UnitsWithinTiles(t.Position, 1).Where(s => s.Unit.GetStatusEffect(StatusEffectType.Focus) != null))
                {
                    for (int i = 0; i < ally.Unit.GetStatusEffect(StatusEffectType.Focus).Duration; i++)
                    {
                        t.Unit.AddSpellForce();
                        stacks++;
                    }
                    ally.Unit.StatusEffects.Remove(ally.Unit.GetStatusEffect(StatusEffectType.Focus));
                    TacticalGraphicalEffects.CreateGenericMagic(ally.Position, t.Position, t);
                }
                t.Movement += stacks/2;
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t, TacticalGraphicalEffects.SpellEffectIcon.PurplePlus);
            }
        };
        SpellDict[SpellTypes.Evocation] = Evocation;

        ManaFlux = new DamageSpell()
        {
            Name = "Mana Flux",
            Id = "mana-flux",
            SpellType = SpellTypes.ManaFlux,
            Description = "Deals damage which is increased by missing mana.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(6),
            Tier = 1,
            AreaOfEffect = 0,
            Damage = (a, t) => (a.Unit.GetStat(Stat.Mind) / 5) + ((a.Unit.MaxMana - a.Unit.Mana)/10),
            Resistable = true,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(ManaFlux, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t);
            }
        };
        SpellDict[SpellTypes.ManaFlux] = ManaFlux;
        UnstableMana = new DamageSpell()
        {
            Name = "Unstable Mana",
            Id = "unstable-mana",
            SpellType = SpellTypes.UnstableMana,
            Description = "Deals damage increased by unit's current mana, then uses it all. The target explodes, dealing half of their current mana as damage around them.",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(6),
            Tier = 0,
            AreaOfEffect = 0,
            Damage = (a, t) => (a.Unit.Mana / 4) + a.Unit.GetStat(Stat.Mind) / 5,
            Resistable = true,
            OnExecute = (a, t) =>
            {
                int curr = t.Unit.Health;
                a.CastOffensiveSpell(UnstableMana, t);
                bool didSpellHit = curr != t.Unit.Health;
                TacticalGraphicalEffects.CreateHugeMagic(a.Position, t.Position, t, didSpellHit);
                if (didSpellHit)
                {
                    a.CastOffensiveSpell(ManaExpolsion, t);
                }
                a.Unit.SpendMana(a.Unit.Mana);
            }
        };
        SpellDict[SpellTypes.UnstableMana] = UnstableMana;

        ManaExpolsion = new DamageSpell()
        {
            Name = "Mana Expolsion",
            Id = "mana-expolsion",
            SpellType = SpellTypes.ManaExpolsion,
            Description = "(Second half of Unstable Mana Spell)",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy },
            Range = new Range(6),
            Tier = 0,
            AreaOfEffect = 1,
            Damage = (a, t) => t.Unit.Mana / 2,
            Resistable = true,
            ResistanceMult = 0.2f,
            OnExecute = (a, t) =>
            {
            }
        };
        SpellDict[SpellTypes.ManaExpolsion] = ManaExpolsion;

        Conduit = new DamageSpell()
        {
            Name = "Conduit",
            Id = "conduit",
            SpellType = SpellTypes.Conduit,
            Description = "Deals damage around a target, but not to it. Scales with both Unit's mind and Target's",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Ally},
            Range = new Range(6),
            Tier = 1,
            AOEType = AreaOfEffectType.FixedPattern,
            Pattern = new int[3,3] { { 1, 1, 1 }, { 1, 0, 1 }, { 1, 1, 1 } },
            Damage = (a, t) => (a.Unit.GetStat(Stat.Mind) / 10) + (t.Unit.GetStat(Stat.Mind) / 10),
            Resistable = true,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(Conduit, t);
                TacticalGraphicalEffects.CreateGenericMagic(a.Position, t.Position, t);
            }
        };
        SpellDict[SpellTypes.Conduit] = Conduit;

        Flamberge = new DamageSpell()
        {
            Name = "Flamberge",
            Id = "flamberge",
            SpellType = SpellTypes.Flamberge,
            Description = "Deals damage to a target and targets behind, sets ground on fire for a few turns",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy, AbilityTargets.Tile },
            Range = new Range(6),
            AOEType = AreaOfEffectType.RotatablePattern,
            Tier = 4,
            Pattern = new int[5, 5] { { 1, 1, 1, 1, 1 }, { 0, 1, 1, 1, 0 }, { 0, 0, 1, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } },
            Resistable = true,
            DamageType = DamageTypes.Fire,
            Damage = (a, t) => 10 + a.Unit.GetStat(Stat.Mind) / 7,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(Flamberge, t);
                TacticalGraphicalEffects.CreateFireBall(a.Position, t.Position, t);
                TacticalUtilities.CreateEffectWithPattern(t.Position, a.Position, TileEffectType.Fire, 1 + a.Unit.GetStat(Stat.Mind) / 30, 4, Flamberge.Pattern, Flamberge.AOEType);
            },
            OnExecuteTile = (a, l) =>
            {
                a.CastOffensiveSpell(Flamberge, null, l);
                TacticalGraphicalEffects.CreateFireBall(a.Position, l, null);
                TacticalUtilities.CreateEffectWithPattern(l, a.Position, TileEffectType.Fire, 1 + a.Unit.GetStat(Stat.Mind) / 30, 4, Flamberge.Pattern, Flamberge.AOEType);
            },
        };
        SpellDict[SpellTypes.Flamberge] = Flamberge;

        ForkLightning = new DamageSpell()
        {
            Name = "ForkLightning",
            Id = "fork-lightning",
            SpellType = SpellTypes.ForkLightning,
            Description = "Deals damage to a target and targets behind, sets ground on fire for a few turns",
            AcceptibleTargets = new List<AbilityTargets>() { AbilityTargets.Enemy},
            Range = new Range(6),
            AOEType = AreaOfEffectType.Full,
            AreaOfEffect = 2,
            Tier = 4,
            Resistable = true,
            Damage = (a, t) => 10 + a.Unit.GetStat(Stat.Mind) / 7,
            OnExecute = (a, t) =>
            {
                a.CastOffensiveSpell(ForkLightning, t);
                TacticalGraphicalEffects.CreateFireBall(a.Position, t.Position, t);
                TacticalUtilities.CreateEffectWithPattern(t.Position, a.Position, TileEffectType.Fire, 1 + a.Unit.GetStat(Stat.Mind) / 30, 4, Flamberge.Pattern, Flamberge.AOEType);
            },
        };
        SpellDict[SpellTypes.ForkLightning] = ForkLightning;
    }
}


public class Spell
{
    internal string Name;
    internal string Id;
    internal string Description;
    internal List<AbilityTargets> AcceptibleTargets;
    internal SpellTypes SpellType;
    internal Range Range;
    internal int Tier;
    /// <summary>The number of tiles away a target is affected (i.e. 1 = 9 total tiles)</summary>
    internal int AreaOfEffect;
    /// <summary> The type of AOE the spell will use, tells the code which functions to implement so it runs faster:
    /// Full - The normal AOE type
    /// FixedPattern - Will cause AreaOfEffect to be ignored and will use Pattern instead
    /// RotatablePattern - Same as above, but will apply the rotation to the patetrn based on target's position
    /// </summary>
    internal AreaOfEffectType AOEType = AreaOfEffectType.Full;
    /// <summary>The vector representing the spell's pattern
    /// The game reads tiles from top left to bottom right so
    /// [[0,0,0],[0,1,0],[1,1,1]] OR
    /// [[0,0,0]
    ///  [0,1,0]
    ///  [1,1,1]]
    ///  will have of shape
    ///  O O O
    ///  O X O
    ///  X X X
    ///  Length MUST be a odd perfect square (9, 25, 49, 81, ...) As the spell is centered on the target
    /// </summary>
    internal int[,] Pattern;
    internal Action<Actor_Unit, Vec2i> OnExecuteTile;
    internal bool Resistable;
    internal float ResistanceMult = 1;
    internal Action<Actor_Unit, Actor_Unit> OnExecute;
    internal bool IsFree = false;
    

    internal bool TryCast(Actor_Unit actor, Vec2i location)
    {
        if ((actor.Unit.Mana >= ManaCost || IsFree) && actor.Movement > 0)
        {
            int startMana = actor.Unit.Mana;
            OnExecuteTile(actor, location);
            if ((actor.Unit.SingleUseSpells?.Contains(SpellType) ?? false) && actor.Unit.Mana != startMana)
            {
                actor.Unit.SingleUseSpells.Remove(SpellType);
                actor.Unit.UpdateSpells();
            }
            return true;
        }
        return false;
    }
    internal bool TryCast(Actor_Unit actor, Actor_Unit target)
    {
        if ((actor.Unit.Mana >= ManaCost || IsFree) && actor.Movement > 0)
        {
            int startMana = actor.Unit.Mana;
            OnExecute(actor, target);
            if ((actor.Unit.SingleUseSpells?.Contains(SpellType) ?? false) && actor.Unit.Mana != startMana)
            {
                actor.Unit.SingleUseSpells.Remove(SpellType);
                actor.Unit.UpdateSpells();
            }
            return true;
        }
        return false;
    }

    internal int ManaCost => 2 * (int)Math.Pow(2, Tier);


}

//May redo this with interfaces later on

class DamageSpell : Spell
{

    internal Func<Actor_Unit, Actor_Unit, int> Damage;
    internal DamageTypes DamageType = DamageTypes.Generic;
}

class StatusSpell : Spell
{
    internal Func<Actor_Unit, Actor_Unit, int> Duration;
    internal Func<Actor_Unit, Actor_Unit, float> Effect;
    internal StatusEffectType Type;
    internal bool Alraune = false;
}

//class MultiTargetSpell : Spell
//{
//    internal Range SecondRange;
//}
