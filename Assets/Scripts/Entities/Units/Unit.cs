using OdinSerializer;
using OdinSerializer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum UnitType
{
    Soldier,
    Leader,
    Mercenary,
    Summon,
    SpecialMercenary,
    Adventurer,
    Spawn,
    Boss,
    Reinforcement,
}

public enum AIClass
{
    Default,
    Melee,
    MeleeVore,
    Ranged,
    RangedVore,
    PureVore,
    MagicMelee,
    MagicRanged,
    Custom
}

public class Unit
{

    [OdinSerialize]
    public int Side;
    [OdinSerialize]
    internal Unit _controller = null;

    internal Unit Controller
    {
        get
        {
            if(_controller != null)
                if (_controller.Controller != null)
                    return _controller.Controller;
            return _controller;
        }
        set
        {
            if (value == null)
                _controller = value;
            else if ((_controller == null) || (_controller.GetStat(Stat.Mind) < value.GetStat(Stat.Mind)))
                if (value == this || value.Controller == this)
                    return;
                else
                    _controller = value;
        }
    }
    [OdinSerialize]
    private int _fixedSide = -1;

    internal bool HasFixedSide() => _fixedSide != -1;
    public int FixedSide
    {
        get
        {
            if (Controller != null)
                return _controller.FixedSide;
            return (_fixedSide == -1) ? Side : _fixedSide;
        }
        set => _fixedSide = value;
    }
    [OdinSerialize]
    public bool hiddenFixedSide = false;

    public static List<Traits> secretTags = new List<Traits>() { Traits.Infiltrator, Traits.Corruption, Traits.Parasite, Traits.Metamorphosis,
        Traits.Possession, Traits.Changeling, Traits.Reincarnation, Traits.InfiniteReincarnation, Traits.Transmigration, Traits.InfiniteTransmigration,
        Traits.Untamable, Traits.GreaterChangeling, Traits.SpiritPossession, Traits.ForcedMetamorphosis};

    [OdinSerialize]
    public Race Race;
    [OdinSerialize]
    public int Health;
    [OdinSerialize]
    protected int[] Stats;
    [OdinSerialize]
    protected float experience;
    [OdinSerialize]
    protected int level;
    [OdinSerialize]
    private double _baseScale = 1;
    internal double BaseScale
    {
        get
        {
            if (_baseScale < 1 || HasTrait(Traits.Growth) == false)
                return 1;
            return _baseScale;
        }
        set => _baseScale = value;
    }
    [OdinSerialize]
    public float ExpMultiplier { get; protected set; } = 1;

    [OdinSerialize]
    internal int Mana { get; private set; }

    [OdinSerialize]
    internal int Barrier { get; private set; }
    [OdinSerialize]
    internal int MaxBarrier { get; private set; }
    internal int MaxMana => (int)(GetStatBase(Stat.Mind) + GetStatBase(Stat.Will) * 2 * TraitBoosts.ManaMultiplier);
    internal int MaxStamina => (int)((GetStatBase(Stat.Strength) + GetStatBase(Stat.Endurance) * 2) * TraitBoosts.StaminaMultiplier);

    private int _maxHealth = 99999;

    public int MaxHealth
    {
        get // Ah yes, a simple getter function u?u. Keeps health percentage consistent after gaining/losing stats mid battle, doesn't break Thrillseeker, doesn't cause prey orphans, doesn't break on save/load... etc.
        {
            if (Stats == null) return 1;
            if (!Config.StatBoostsAffectMaxHP) {
                _maxHealth = Stats[(int)Stat.Endurance] * 2 + Stats[(int)Stat.Strength];
                 return (int)(_maxHealth * TraitBoosts.HealthMultiplier);
            }

            int oldMax = _maxHealth;
            _maxHealth = GetStat(Stat.Endurance) * 2 + GetStat(Stat.Strength);
            if (oldMax > 1 && oldMax != _maxHealth && _healthPct > 0)
            {
                int lowestHP = 0;
                if ((_maxHealth - oldMax) * _healthPct > 0.4)
                    lowestHP = 1;
                int healthChange = (int)Math.Round((_maxHealth - oldMax) * _healthPct);
                Health = Math.Min(_maxHealth,Math.Max(lowestHP, Health + healthChange));
            }
            return (int)(_maxHealth * TraitBoosts.HealthMultiplier);
        }
        set => _maxHealth = value;
    }
    private int GetHealthBoosts()
    {
        throw new NotImplementedException();
    }

    [OdinSerialize]
    internal int DeployCost;
    [OdinSerialize]
    internal float Upkeep;

    [OdinSerialize]
    internal AIClass AIClass;
    [OdinSerialize]
    internal StatWeights StatWeights;
    [OdinSerialize]
    internal bool AutoLeveling;

    #region Customizations
    [OdinSerialize]
    public int HairColor;
    [OdinSerialize]
    public int HairStyle;
    [OdinSerialize]
    public int BeardStyle;
    [OdinSerialize]
    public int SkinColor;
    [OdinSerialize]
    public int AccessoryColor;
    [OdinSerialize]
    public int EyeColor;
    [OdinSerialize]
    public int ExtraColor1;
    [OdinSerialize]
    public int ExtraColor2;
    [OdinSerialize]
    public int ExtraColor3;
    [OdinSerialize]
    public int ExtraColor4;
    [OdinSerialize]
    public int EyeType;
    [OdinSerialize]
    public int MouthType;
    [OdinSerialize]
    public int BreastSize;
    [OdinSerialize]
    public int DickSize;
    [OdinSerialize]
    public bool HasVagina;
    [OdinSerialize]
    internal int BodySize;
    [OdinSerialize]
    internal int SpecialAccessoryType;
    [OdinSerialize]
    internal bool BodySizeManuallyChanged;
    [OdinSerialize]
    internal int DefaultBreastSize;
    [OdinSerialize]
    internal int ClothingType;
    [OdinSerialize]
    internal int ClothingType2;
    [OdinSerialize]
    internal int ClothingHatType;
    [OdinSerialize]
    internal int ClothingAccessoryType;
    [OdinSerialize]
    internal int ClothingExtraType1;
    [OdinSerialize]
    internal int ClothingExtraType2;
    [OdinSerialize]
    internal int ClothingExtraType3;
    [OdinSerialize]
    internal int ClothingExtraType4;
    [OdinSerialize]
    internal int ClothingExtraType5;
    [OdinSerialize]
    internal int ClothingColor;
    [OdinSerialize]
    internal int ClothingColor2;
    [OdinSerialize]
    internal int ClothingColor3;
    [OdinSerialize]
    internal bool Furry;
    [OdinSerialize]
    public int HeadType;
    [OdinSerialize]
    public int TailType;
    [OdinSerialize]
    public int FurType;
    [OdinSerialize]
    public int EarType;
    [OdinSerialize]
    public int BodyAccentType1;
    [OdinSerialize]
    public int BodyAccentType2;
    [OdinSerialize]
    public int BodyAccentType3;
    [OdinSerialize]
    public int BodyAccentType4;
    [OdinSerialize]
    public int BodyAccentType5;
    [OdinSerialize]
    public int BallsSize;
    [OdinSerialize]
    public int VulvaType;
    [OdinSerialize]
    public int BasicMeleeWeaponType;
    [OdinSerialize]
    public int AdvancedMeleeWeaponType;
    [OdinSerialize]
    public int BasicRangedWeaponType;
    [OdinSerialize]
    public int AdvancedRangedWeaponType;
    #endregion

    [OdinSerialize]
    internal int DigestedUnits;
    [OdinSerialize]
    internal int KilledUnits;

    [OdinSerialize]
    internal int TimesKilled;


    [OdinSerialize]
    public Item[] Items;
    [OdinSerialize]
    public int[] ItemUses;
    [OdinSerialize]
    public int[] ItemCooldowns;
    [OdinSerialize]
    public Dictionary<int, int[]> EquippedPotions;
    [OdinSerialize]
    public string Name { get; set; }
    [OdinSerialize]
    public List<string> Pronouns;

    [OdinSerialize]
    public Action OnDiscard;

    public string GetPronoun(int num)
    {
        if (Pronouns == null)
            GeneratePronouns();
        return Pronouns[num];
    }
    [OdinSerialize]
    public UnitType Type;
    [OdinSerialize]
    public bool Predator;
    [OdinSerialize]
    public bool ImmuneToDefections;
    [OdinSerialize]
    public bool FixedGear;

    [OdinSerialize]
    public Unit AttractedTo;

    [OdinSerialize]
    internal VoreType PreferredVoreType;

    [OdinSerialize]
    internal Unit SavedCopy;
    [OdinSerialize]
    internal Village SavedVillage;

    [OdinSerialize]
    private List<StatusEffect> _statusEffects;

    [OdinSerialize]
    public bool EarnedMask = false;

    [OdinSerialize]
    public Unit KilledBy;

    [OdinSerialize]
    public List<Unit> ShifterShapes;

    [OdinSerialize]
    public Unit MorphUnit = null;

    public override string ToString() => Name;

    /// <summary>
    /// Unit was manually changed to/from pred so it should not be overwritten
    /// </summary>
    [OdinSerialize]
    public bool fixedPredator;

    internal List<StatusEffect> StatusEffects
    {
        get
        {
            if (_statusEffects == null)
                _statusEffects = new List<StatusEffect>();
            return _statusEffects;
        }
        set => _statusEffects = value;
    }

    [OdinSerialize]
    public List<SpellTypes> InnateSpells;

    private List<Spell> _useableSpells;

    [OdinSerialize]
    internal List<SpellTypes> SingleUseSpells = new List<SpellTypes>();

    [OdinSerialize]
    internal List<SpellTypes> MultiUseSpells = new List<SpellTypes>();  // This is so much more straightforward than adding Special Actions

    [OdinSerialize]
    internal Unit _hiddenUnit = null;
    public Unit HiddenUnit
    {
        get
        {
            return (_hiddenUnit == null) ? this : _hiddenUnit;
        }
    }

    public Race HiddenRace
    {
        get
        {
            return (_hiddenUnit == null) ? Race : _hiddenUnit.Race;
        }
    }

    public int[] HiddenStats
    {
        get
        {
            return (_hiddenUnit == null) ? Stats : _hiddenUnit.Stats;
        }
    }

    [OdinSerialize]
    private Race _spawnRace;
    public Race SpawnRace
    {
        get
        {
            return (_spawnRace == Race.none) ? Race : _spawnRace;
        }
        set => _spawnRace = value;
    }
    [OdinSerialize]
    private Race _conversionRace;
    public Race ConversionRace
    {
        get
        {
            return (_conversionRace == Race.none) ? Race : _conversionRace;
        }
        set => _conversionRace = value;
    }

    internal List<Spell> UseableSpells
    {
        get
        {
            if (_useableSpells == null)
            {
                _useableSpells = new List<Spell>();
            }
            return _useableSpells;
        }
        set => _useableSpells = value;
    }

    internal bool HasDick => DickSize > -1;
    internal bool HasBreasts => DefaultBreastSize > -1;


    public bool IsInfiltratingSide(int side)
    {
        return side == Side && Side != FixedSide && hiddenFixedSide;
    }

    internal Gender GetGender()
    {
        if (HasBreasts && HasDick && (HasVagina || Config.HermsCanUB == false))
            return Gender.Hermaphrodite;
        else if (HasBreasts && HasDick && !HasVagina)
            return Gender.Gynomorph;
        else if (HasBreasts && !HasDick && HasVagina)
            return Gender.Female;
        else if (HasBreasts && !HasDick && !HasVagina)
            return Gender.Agenic;
        else if (!HasBreasts && HasDick && HasVagina)
            return Gender.Maleherm;
        else if (!HasBreasts && HasDick && !HasVagina)
            return Gender.Male;
        else if (!HasBreasts && !HasDick && HasVagina)
            return Gender.Andromorph;
        return Gender.None;
    }

    internal bool CanBeConverted()
    {
        return Type != UnitType.Summon && Type != UnitType.Leader && Type != UnitType.SpecialMercenary && HasTrait(Traits.Eternal) == false && SavedCopy == null && Level > 0;
    }

    internal bool AtypicalBiology()
    {
        return HasTrait(Traits.AcellularBody) || HasTrait(Traits.ViralBiology);
    }

    internal bool CanUnbirth => Config.Unbirth && HasVagina;
    internal bool CanCockVore => Config.CockVore && HasDick;
    internal bool CanBreastVore => Config.BreastVore && HasBreasts;
    internal bool CanAnalVore => Config.AnalVore;
    internal bool CanTailVore => Config.TailVore;

    public bool CanVore(PreyLocation location)
    {
        switch (location)
        {
            case PreyLocation.womb:
                return CanUnbirth;
            case PreyLocation.balls:
                return CanCockVore;
            case PreyLocation.breasts:
                return CanBreastVore;
            case PreyLocation.anal:
                return CanAnalVore;
            case PreyLocation.tail:
                return CanTailVore;
            default:
                return true;
        }
    }

    internal bool HasWeapon
    {
        get
        {
            if (Items == null)
                return false;
            if (HasTrait(Traits.Feral))
                return false;
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] is Weapon)
                    return true;
            }
            return false;
        }
    }

    internal bool HasSpecificWeapon(params ItemType[] types)
    {
        if (Items == null)
            return false;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
                continue;
            if (types.Contains(State.World.ItemRepository.GetItemType(Items[i])))
                return true;
        }
        return false;
    }

    internal bool HasBook
    {
        get
        {
            if (Items == null)
                return false;
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] is SpellBook)
                    return true;
            }
            return false;
        }
    }


    [OdinSerialize]
    internal Race attunedEeveeRace = Race.none;

    public bool BestSuitedForRanged() => Stats[(int)Stat.Dexterity] * TraitBoosts.VirtualDexMult > Stats[(int)Stat.Strength] * TraitBoosts.VirtualStrMult;

    public void SetLevel(int level) => this.level = level;

    internal bool SpendMana(int amount)
    {
        int ModifiedManaCost = amount + (amount * (GetStatusEffect(StatusEffectType.SpellForce) != null ? GetStatusEffect(StatusEffectType.SpellForce).Duration/10 : 0));
        if (Mana >= ModifiedManaCost)
        {
            Mana -= ModifiedManaCost;
            return true;
        }
        return false;
    }

    internal void RestoreManaPct(float pct)
    {
        Mana += (int)(MaxMana * pct);
        if (Mana > MaxMana)
            Mana = MaxMana;
    }

    internal void RestoreMana(int amt)
    {
        Mana += amt;
        if (Mana > MaxMana)
            Mana = MaxMana;
    }

    internal bool SpendStam(int amount)
    {
        if (Stamina >= amount)
        {
            Stamina -= amount;
            return true;
        }
        return false;
    }
    internal void RestoreStamPct(float pct)
    {
        Stamina += (int)(MaxStamina * pct);
        if (Stamina > MaxStamina)
            Stamina = MaxStamina;
    }
    internal int DamageBarrier(int amount)
    {
        int remaining = amount;
        if (remaining > Barrier)
        {
            remaining = amount - Barrier;
            Barrier = 0;
            MaxBarrier = 1;
        }
        else
        {
            remaining = 0;
            Barrier -= amount;
            if (Barrier > MaxBarrier)
                MaxBarrier = Barrier;
        }
        return remaining;
    }
    internal void RestoreBarrier(int amt)
    {
        Barrier += amt;
        if (Barrier > MaxBarrier)
            MaxBarrier = Barrier;
    }
    internal void RestoreBarrierPct(float pct)
    {
        Barrier += (int)(Barrier * pct);
        if (Barrier > MaxBarrier)
            MaxBarrier = Barrier;
    }
    internal void SetBarrier(int amount)
    {
        Barrier = amount;
        MaxBarrier = amount;
        if (MaxBarrier <= 0)
        {
            MaxBarrier = 1;
        }
    }

    internal void RestoreStam(int amt)
    {
        Stamina += amt;
        if (Stamina > MaxStamina)
            Stamina = MaxStamina;
    }


    internal int NearbyFriendlies = 0;
    internal int NearbyEnemies = 0;
    internal bool Harassed = false;

    public bool IsDead => (Health < 1);
    private PermanentBoosts _traitBoosts;
    internal PermanentBoosts TraitBoosts
    {
        get
        {
            if (_traitBoosts == null)
                InitializeTraits();
            return _traitBoosts;
        }
        set => _traitBoosts = value;
    }

    [OdinSerialize]
    protected List<Traits> Tags; //For some reason, renaming this to anything else results in an infinite loop in serialization, so it is staying tags for now

    [OdinSerialize]
    protected List<Traits> TemporaryTraits;
    [OdinSerialize]
    protected List<Traits> SharedTraits;
    [OdinSerialize]
    protected List<Traits> PersistentSharedTraits;
    [OdinSerialize]
    protected List<Traits> ActiveConditionalTraits;

    /// <summary>
    /// Traits that are considered to be permanent, i.e. do not disappear during refreshes
    /// </summary>
    [OdinSerialize]
    protected List<Traits> PermanentTraits;

    /// <summary>
    /// Traits that are explicitly removed, done so that they aren't added back in at some point by version changes or the like.
    /// </summary>
    [OdinSerialize]
    protected List<Traits> RemovedTraits;

    /// <summary>
    /// Holds All of a units conditional traits.
    /// Key: Trait, Value: If Trait is active
    /// </summary>
    [OdinSerialize]
    internal Dictionary<ConditionalTraitContainer, bool> AllConditionalTraits;

    //internal List<Trait> TraitsList = new List<Trait>();
    internal List<IStatBoost> StatBoosts;
    internal List<IAttackStatusEffect> AttackStatusEffects;
    internal List<IVoreAttackOdds> VoreAttackOdds;
    internal List<IVoreDefenseOdds> VoreDefenseOdds;
    internal List<IPhysicalDefenseOdds> PhysicalDefenseOdds;

    [OdinSerialize]
    internal int EnemiesKilledThisBattle;

    internal Unit CurrentLeader;

    [OdinSerialize]
    public Actor_Unit BoundUnit;

    [OdinSerialize]
    internal int Stamina;

    /// <summary>
    /// Creates an empty unit for various purposes
    /// </summary>
    public Unit(Race race)
    {
        Race = race;
        Stats = new int[(int)Stat.None];
        Stats[(int)Stat.Strength] = 6 + State.Rand.Next(9);
        Stats[(int)Stat.Dexterity] = 6 + State.Rand.Next(9);
        Stats[(int)Stat.Endurance] = 8 + State.Rand.Next(6);
        Stats[(int)Stat.Mind] = 6 + State.Rand.Next(8);
        Stats[(int)Stat.Will] = 6 + State.Rand.Next(8);
        Stats[(int)Stat.Agility] = 6 + State.Rand.Next(5);
        Stats[(int)Stat.Voracity] = 5 + State.Rand.Next(7);
        Stats[(int)Stat.Stomach] = 12 + State.Rand.Next(4);
        Health = MaxHealth;
        Mana = MaxMana;
        Stamina = MaxStamina;
        Barrier = 0;
    }

    public Unit(int side, Race race, int startingXP, bool predator, UnitType type = UnitType.Soldier, bool immuneToDefectons = false)
    {
        Stats = new int[(int)Stat.None];
        Race = race;
        Side = side;
        experience = startingXP;
        level = 1;
        Tags = new List<Traits>();
        PermanentTraits = new List<Traits>();
        RemovedTraits = new List<Traits>();
        AllConditionalTraits = new Dictionary<ConditionalTraitContainer, bool>();
        Type = type;

        Predator = predator;
        if (predator == false)
            fixedPredator = true;
        ImmuneToDefections = immuneToDefectons;


        var raceData = Races.GetRace(this);
        RandomizeNameAndGender(race, raceData, true);

        DefaultBreastSize = BreastSize;
        Items = new Item[Config.ItemSlots];
        ItemUses = new int[] {1, 1, 1};
        ItemCooldowns = new int[3];
        EquippedPotions = new Dictionary<int, int[]>();



        ReloadTraits();
        raceData.RandomCustom(this);
        InitializeTraits();
        RefreshSecrecy();
        InitializeFixedSide(side);

        if (HasTrait(Traits.Resourceful))
        {
            Items = new Item[3];
        }

        SetGear(race);

        InnateSpells = new List<SpellTypes>();
        ShifterShapes = new List<Unit>();

        if (race == Race.Dragon)
        {
            int rand = State.Rand.Next(3);
            if (rand == 0) InnateSpells.Add(SpellTypes.IceBlast);
            if (rand == 1) InnateSpells.Add(SpellTypes.Pyre);
            if (rand == 2) InnateSpells.Add(SpellTypes.LightningBolt);
        }
        if (race == Race.Fairies)
        {
            FairyUtil.SetSeason(this, FairyUtil.GetSeason(this)); //To establish the spell properly
        }

        RandomSkills();
        Health = MaxHealth;
        Mana = MaxMana;
        Stamina = MaxStamina;
        Barrier = 0;

        if (UniformDataStorer.GetUniformOdds(race) >= State.Rand.NextDouble())
        {
            var available = UniformDataStorer.GetCompatibleCustomizations(this);
            if (available != null && available.Any())
            {
                UniformDataStorer.ExternalCopyToUnit(available[State.Rand.Next(available.Count)], this);
            }
        }
        _spawnRace = Race.none;
        _conversionRace = Race.none;

        ReincarnateCheck();
        //if (HasTrait(Traits.Shapeshifter) || HasTrait(Traits.Skinwalker))
        //{
        //    AcquireShape(this, true);
        //}
        SetForcedPermanentTraits();
    }

    private void SetForcedPermanentTraits()
    {
        if (HasTrait(Traits.InfiniteAssimilation))
        {           
            RemoveTrait(Traits.InfiniteAssimilation);
            AddPermanentTrait(Traits.InfiniteAssimilation);
        }
        if (HasTrait(Traits.InfiniteReincarnation))
        {
            RemoveTrait(Traits.InfiniteReincarnation);
            AddPermanentTrait(Traits.InfiniteReincarnation);
        }
        if (HasTrait(Traits.InfiniteTransmigration))
        {
            RemoveTrait(Traits.InfiniteTransmigration);
            AddPermanentTrait(Traits.InfiniteTransmigration);
        }
        //if (HasTrait(Traits.Shapeshifter))
        //{
        //    RemoveTrait(Traits.Shapeshifter);
        //    AddPermanentTrait(Traits.Shapeshifter);
        //}
        //if (HasTrait(Traits.Skinwalker))
        //{
        //    RemoveTrait(Traits.Skinwalker);
        //    AddPermanentTrait(Traits.Skinwalker);
        //}
        if (HasTrait(Traits.Extraction))
        {
            RemoveTrait(Traits.Extraction);
            AddPermanentTrait(Traits.Extraction);
        }
    }

    private void ReincarnateCheck()
    {
        if (State.World != null && State.World.Reincarnators != null && State.World.Reincarnators?.Count > 0 && Type != UnitType.SpecialMercenary && Type != UnitType.Summon)
        {
            if (State.World.Reincarnators.Where(r => r.Race == Race).Count() > 0 && State.Rand.Next(3) == 0)
                Reincarnate(State.World.Reincarnators.Where(r => r.Race == Race).First());
        }
    }

    private void Reincarnate(Reincarnator reinc)
    {
        Unit Unit = reinc.PastLife;
        Name = Unit.Name;
        experience = Unit.Experience;
        foreach (Traits trait in Unit.PermanentTraits)
        {
            AddPermanentTrait(trait);
        }
        InnateSpells.AddRange(Unit.InnateSpells);
        ShifterShapes = Unit.ShifterShapes;
        FixedSide = Unit.FixedSide;
        hiddenFixedSide = true;
        SavedCopy = Unit.SavedCopy;
        SavedVillage = Unit.SavedVillage;
        Race race = reinc.Race;
        OnDiscard = () =>
        {
            Race targetRace = race;
            if (!reinc.RaceLocked)
            {
                List<Race> activeRaces = StrategicUtilities.GetAllUnits(false).ConvertAll(u => u.Race)
                .Distinct().ToList();
                if (activeRaces.Any())
                {
                    targetRace = activeRaces[State.Rand.Next(activeRaces.Count)];
                }
            }
            State.World.Reincarnators?.Add(new Reincarnator(Unit, targetRace, reinc.RaceLocked));
            Debug.Log(Unit.Name + " is re-reincarnating");
        };
        State.World.Reincarnators?.Remove(reinc);
        Debug.Log(Unit.Name + " reincarnated as a " + Race);
        StrategicUtilities.SpendLevelUps(this);
    }

    internal void SetGear(Race race, bool skipTraitItems = false)
    {
        if (race >= Race.Vagrants && race < Race.Selicia)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetMonsterItem((int)Race - 100);
        }
        else if (race == Race.Selicia)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.SeliciaWeapon);
        }
        else if (race == Race.Vision)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.VisionWeapon);
        }
        else if (race == Race.Ki)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.KiWeapon);
        }
        else if (race == Race.Scorch)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.ScorchWeapon);
        }
        else if (race == Race.Succubi)
        {
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.SuccubusWeapon);
        }
        else if (race == Race.Asura)
        {
            Items[0] = State.World.ItemRepository.GetItem(ItemType.Axe);
        }
        else if (race == Race.DRACO)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.DRACOWeapon);
        }
        else if (race == Race.Zoey)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.ZoeyWeapon);
        }
        else if (race == Race.Cierihaka)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.CierihakaWeapon);
        }
        else if (race == Race.Zera)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.ZeraWeapon);
        }
        else if (race == Race.Auri)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.AurilikaWeapon);
        }
        else if (race == Race.Salix)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.SalixWeapon);
        }
        else if (race == Race.Erin)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.ErinWeapon);
            Items[1] = State.World.ItemRepository.GetSpecialItem(SpecialItems.ErinWings);
        }
        else if (race == Race.Abakhanskya)
        {
            try
            {
                FixedGear = true;
                Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.AbaWeapon);
                Items[1] = State.World.ItemRepository.GetSpecialItem(SpecialItems.AbaArmor);
            }
            catch { }
        }
        else if (race == Race.Bella)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.BellaWeapon);
        }
        else if (race == Race.Singularity)
        {
            try
            {
                FixedGear = true;
                Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.SingularityWeapon);
                Items[1] = State.World.ItemRepository.GetSpecialItem(SpecialItems.SingularityArmor);
            }
            catch { }
        }
        else if (race == Race.Feit)
        {
            try
            {
                FixedGear = true;
                Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.FeitWeapon);
                Items[1] = State.World.ItemRepository.GetSpecialItem(SpecialItems.FeitArmor);
            }
            catch { }
        }
        else if (race == Race.Taraluxia)
        {
            try
            {
                FixedGear = true;
                Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.TaraWeapon);
            }
            catch { }
        }
        else if (race == Race.Xelhilde)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.XelhildeWeapon);
        }
        else if (race == Race.Olivia)
        {
            FixedGear = true;
            Items[0] = null;
        }
        else if (race == Race.Skapa)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.SkapaWeapon);
        }
        else if (race == Race.Tatltuae)
        {
            FixedGear = false;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.TatltuaeWeapon);
        }
        else if (race == Race.Firefly)
        {
            try
            {
                FixedGear = true;
                Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.FireflyMelee);
                Items[1] = State.World.ItemRepository.GetSpecialItem(SpecialItems.FireflyRange);
            }
            catch { }
        }
        else if (race == Race.Nectar)
        {
            FixedGear = false;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.NectarWeapon);
        }
        else if (race == Race.Ryan)
        {
            FixedGear = true;
            Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.RyanWeapon);
        }
        else
        {
            FixedGear = false;
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] != null && State.World.ItemRepository.ItemIsUnique(Items[i]))
                    Items[i] = null;
            }
            if (RaceParameters.GetRaceTraits(race).CanUseRangedWeapons == false)
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    if (Items[i] != null && State.World.ItemRepository.ItemIsRangedWeapon(Items[i]))
                    {
                        if (Items[i] is Weapon weapon)
                        {
                            if (weapon.Damage > 4)
                                Items[i] = State.World.ItemRepository.GetItem(ItemType.Axe);
                            else
                                Items[i] = State.World.ItemRepository.GetItem(ItemType.Mace);
                        }
                    }
                }
            }
            if (!skipTraitItems)
                GiveTraitBooks();
        }
    } 

    internal void GiveTraitBooks()
    {
        if (State.World?.ItemRepository == null) return;
        var tiers = new List<int>();
        if (HasTrait(Traits.BookWormI))
        {
            tiers.Add(1);
        }
        if (HasTrait(Traits.BookWormII))
        {
            tiers.Add(2);
        }
        if (HasTrait(Traits.BookWormIII))
        {
            tiers.Add(3);
        }
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null && Items[i] is SpellBook)
            {
                if(((SpellBook)Items[i]).Tier == 4)
                        tiers.Remove(3);
                else
                tiers.Remove(((SpellBook)Items[i]).Tier);
            }
        }

        for (int i = 0; i < Items.Length; i++)
        {
            if (tiers.Count > 0)
            {
                if (Items[i] == null)
                {
                    int t = tiers.Count > 1 ? tiers[State.Rand.Next(tiers.Count - 1)] : tiers[0];
                    Items[i] = State.World.ItemRepository.GetRandomBook(t, t == 3 ? 4 : t, true);
                    tiers.Remove(t);
                }
            }
        }
        if (tiers.Count > 0 && HasTrait(Traits.BookEater))
        {
            foreach (int t in tiers)
            {
                SpellTypes spell = ((SpellBook)State.World.ItemRepository.GetRandomBook(t, t == 3 ? 4 : t, true)).ContainedSpell;
                if (!InnateSpells.Contains(spell))
                    InnateSpells.Add(spell);
            }
        }
    }

    internal Unit Clone()
    {
        var clone = (Unit)MemberwiseClone();
        clone.Stats = new int[(int)Stat.None];
        for (int i = 0; i < Stats.Length; i++)
        {
            clone.Stats[i] = Stats[i];
        }
        //clone.Items = (Item[])Items.Clone();
        //for (int i = 0; i < Items.Length; i++)
        //{
        //    clone.SetItem(null, i);
        //}
        return clone;
    }

    internal void CopyAppearance(Unit appearance)
    {
        this.HairColor = appearance.HairColor;
        this.HairStyle = appearance.HairStyle;
        this.BeardStyle = appearance.HairStyle;
        this.SkinColor = appearance.SkinColor;
        this.AccessoryColor = appearance.AccessoryColor;
        this.EyeColor = appearance.EyeColor;
        this.ExtraColor1 = appearance.ExtraColor1;
        this.ExtraColor2 = appearance.ExtraColor2;
        this.ExtraColor3 = appearance.ExtraColor3;
        this.ExtraColor4 = appearance.ExtraColor4;
        this.EyeType = appearance.EyeType;
        this.MouthType = appearance.MouthType;
        this.BreastSize = appearance.BreastSize;
        this.DickSize = appearance.DickSize;
        this.HasVagina = appearance.HasVagina;
        this.BodySize = appearance.BodySize;
        this.SpecialAccessoryType = appearance.SpecialAccessoryType;
        this.BodySizeManuallyChanged = appearance.BodySizeManuallyChanged;
        this.DefaultBreastSize = appearance.DefaultBreastSize;
        this.ClothingType = appearance.ClothingType;
        this.ClothingType2 = appearance.ClothingType2;
        this.ClothingHatType = appearance.ClothingHatType;
        this.ClothingAccessoryType = appearance.ClothingAccessoryType;
        this.ClothingExtraType1 = appearance.ClothingExtraType1;
        this.ClothingExtraType2 = appearance.ClothingExtraType2;
        this.ClothingExtraType3 = appearance.ClothingExtraType3;
        this.ClothingExtraType4 = appearance.ClothingExtraType4;
        this.ClothingExtraType5 = appearance.ClothingExtraType5;
        this.ClothingColor = appearance.ClothingColor;
        this.ClothingColor2 = appearance.ClothingColor2;
        this.ClothingColor3 = appearance.ClothingColor3;
        this.Furry = appearance.Furry;
        this.HeadType = appearance.HeadType;
        this.TailType = appearance.TailType;
        this.FurType = appearance.FurType;
        this.EarType = appearance.EarType;
        this.BodyAccentType1 = appearance.BodyAccentType1;
        this.BodyAccentType2 = appearance.BodyAccentType2;
        this.BodyAccentType3 = appearance.BodyAccentType3;
        this.BodyAccentType4 = appearance.BodyAccentType4;
        this.BodyAccentType5 = appearance.BodyAccentType5;
        this.BallsSize = appearance.BallsSize;
        this.VulvaType = appearance.VulvaType;
        this.BasicMeleeWeaponType = appearance.BasicMeleeWeaponType;
        this.AdvancedMeleeWeaponType = appearance.AdvancedMeleeWeaponType;
        this.BasicRangedWeaponType = appearance.BasicRangedWeaponType;
        this.AdvancedRangedWeaponType = appearance.AdvancedRangedWeaponType;
}

internal void SetGenderRandomizeName(Race race, Gender gender)
    {
        var raceData = Races.GetRace(this);
        var isMale = false;
        if (raceData.CanBeGender.Count == 0 || (raceData.CanBeGender.Contains(Gender.None) && raceData.CanBeGender.Count == 1))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            isMale = State.Rand.NextDouble() > Config.HermNameFraction;
        }
        else if (gender == Gender.Hermaphrodite && raceData.CanBeGender.Contains(Gender.Hermaphrodite))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            isMale = State.Rand.NextDouble() > Config.HermNameFraction;
        }
        else if ((gender == Gender.Male && raceData.CanBeGender.Contains(Gender.Male)) || raceData.CanBeGender.Contains(Gender.Female) == false)
        {
            DickSize = 0;
            SetDefaultBreastSize(-1);
            isMale = true;
        }
        else
        {
            SetDefaultBreastSize(0);
            DickSize = -1;
            isMale = false;
        }

        if (race >= Race.Vagrants)
        {
            Name = State.NameGen.GetMonsterName(isMale, race);
        }
        else
        {
            Name = State.NameGen.GetName(isMale, race);
        }



        ReloadTraits();
        raceData.RandomCustom(this);
        InitializeTraits();
    }

    internal void TotalRandomizeAppearance()
    {
        var raceData = Races.GetRace(this);
        RandomizeNameAndGender(Race, raceData, true);
        raceData.RandomCustom(this);

    }

    internal void RandomizeAppearance()
    {
        var raceData = Races.GetRace(this);
        raceData.RandomCustom(this);
    }

    internal void RandomizeNameAndGender(Race race, DefaultRaceData raceData, bool skipTraits = false)
    {
        var raceStats = State.RaceSettings.Get(race);
        float maleFraction;
        float hermFraction;
        bool isMale = false;
        if (raceStats.OverrideGender)
        {
            maleFraction = raceStats.maleFraction;
            hermFraction = raceStats.hermFraction;
        }
        else
        {
            maleFraction = Config.MaleFraction;
            hermFraction = Config.HermFraction;
        }

        if (raceData.CanBeGender.Count == 0 || (raceData.CanBeGender.Contains(Gender.None) && raceData.CanBeGender.Count == 1))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            HasVagina = false;
            Pronouns = new List<string> { "they", "them", "their", "theirs", "themself", "plural" };
            isMale = State.Rand.NextDouble() > Config.HermNameFraction;
        }
        else if (State.Rand.NextDouble() < hermFraction && raceData.CanBeGender.Contains(Gender.Hermaphrodite))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            HasVagina = Config.HermsCanUB;
            Pronouns = new List<string> { "they", "them", "their", "theirs", "themself", "plural" };
            isMale = State.Rand.NextDouble() > Config.HermNameFraction;
        }
        else if ((State.Rand.NextDouble() < maleFraction && raceData.CanBeGender.Contains(Gender.Male)) || raceData.CanBeGender.Contains(Gender.Female) == false)
        {
            DickSize = 0;
            SetDefaultBreastSize(-1);
            HasVagina = false;
            Pronouns = new List<string> { "he", "him", "his", "his", "himself", "singular" };
            isMale = true;
        }
        else
        {
            SetDefaultBreastSize(0);
            DickSize = -1;
            HasVagina = true;
            Pronouns = new List<string> { "she", "her", "her", "hers", "herself", "singular" };
            isMale = false;
        }

        if (race >= Race.Vagrants)
        {
            Name = State.NameGen.GetMonsterName(isMale, race);
        }
        else
        {
            Name = State.NameGen.GetName(isMale, race);
        }

        if (skipTraits == false)
        {
            ReloadTraits();
            InitializeTraits();
        }

    }

    internal void GeneratePronouns()
    {
        if (GetGender() == Gender.Female)
            Pronouns = new List<string> { "she", "her", "her", "hers", "herself", "singular" };
        else if (GetGender() == Gender.Male)
            Pronouns = new List<string> { "he", "him", "his", "his", "himself", "singular" };
        else
            Pronouns = new List<string> { "they", "them", "their", "theirs", "themself", "plural" };
    }

    internal void RandomizeGender(Race race, DefaultRaceData raceData, bool skipTraits = false)
    {
        var raceStats = State.RaceSettings.Get(race);
        float maleFraction;
        float hermFraction;
        if (raceStats.OverrideGender)
        {
            maleFraction = raceStats.maleFraction;
            hermFraction = raceStats.hermFraction;
        }
        else
        {
            maleFraction = Config.MaleFraction;
            hermFraction = Config.HermFraction;
        }

        if (raceData.CanBeGender.Count == 0 || (raceData.CanBeGender.Contains(Gender.None) && raceData.CanBeGender.Count == 1))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            HasVagina = false;
            Pronouns = new List<string> { "they", "them", "their", "theirs", "themself", "plural" };
        }
        else if (State.Rand.NextDouble() < hermFraction && raceData.CanBeGender.Contains(Gender.Hermaphrodite))
        {
            DickSize = 0;
            SetDefaultBreastSize(0);
            HasVagina = true;
            Pronouns = new List<string> { "they", "them", "their", "theirs", "themself", "plural" };
        }
        else if ((State.Rand.NextDouble() < maleFraction && raceData.CanBeGender.Contains(Gender.Male)) || raceData.CanBeGender.Contains(Gender.Female) == false)
        {
            DickSize = 0;
            SetDefaultBreastSize(-1);
            HasVagina = false;
            Pronouns = new List<string> { "he", "him", "his", "his", "himself", "singular" };
        }
        else
        {
            SetDefaultBreastSize(0);
            DickSize = -1;
            HasVagina = true;
            Pronouns = new List<string> { "she", "her", "her", "hers", "herself", "singular" };
        }

        if (skipTraits == false)
        {
            ReloadTraits();
            InitializeTraits();
        }

    }

    public void GiveExp(float exp, bool voreSource = false, bool isKill = false)
    {
        exp *= TraitBoosts.ExpGain;

        if (State.World.GetEmpireOfSide(Side)?.StrategicAI is StrategicAI ai)
        {
            if (ai.CheatLevel > 0)
                exp *= 1 + .25f * ai.CheatLevel;
        }

        if (voreSource) exp *= TraitBoosts.ExpGainFromVore;
        if (voreSource && isKill) exp *= TraitBoosts.ExpGainFromAbsorption;

        experience += exp;
    }
	
    public float CalcGiveExp(float exp, bool voreSource = false, bool isKill = false)// Used for calculations
    {
        exp *= TraitBoosts.ExpGain;

        if (State.World.GetEmpireOfSide(Side)?.StrategicAI is StrategicAI ai)
        {
            if (ai.CheatLevel > 0)
                exp *= 1 + .25f * ai.CheatLevel;
        }

        if (voreSource) exp *= TraitBoosts.ExpGainFromVore;
        if (voreSource && isKill) exp *= TraitBoosts.ExpGainFromAbsorption;

        return exp;
    }
    public void GiveRawExp(int exp) => experience += exp;

    public bool IsDeadAndOverkilledBy(int overkill)
    {
        return Health < (1 - overkill);
    }

    public bool IsThisCloseToDeath(int lessThanThisDamageAwayFromDeath)
    {
        return Health < (1 + lessThanThisDamageAwayFromDeath) && !IsDead;
    }


    public void Kill()
    {
        TimesKilled++;
        if (SavedCopy != null)
            SavedCopy.TimesKilled++;
    }

    public void DrainExp(float exp)
    {
        experience -= exp;
    }

    public void GiveScaledExp(float exp, int attackerLevelAdvantage, bool voreSource = false, bool isKill = false)
    {
        if (Config.FlatExperience)
        {
            GiveExp(exp, voreSource, isKill);
            return;
        }
        if (State.World.GetEmpireOfSide(Side)?.StrategicAI is StrategicAI ai)
        {
            if (ai.CheatLevel > 0)
                exp *= 1 + .25f * ai.CheatLevel;
        }
        exp *= TraitBoosts.ExpGain;
        if (voreSource) exp *= TraitBoosts.ExpGainFromVore;
        if (voreSource && isKill) exp *= TraitBoosts.ExpGainFromAbsorption;

        if (attackerLevelAdvantage > 0)
            exp = Math.Max(exp * (1 - ((float)Math.Pow(attackerLevelAdvantage, 1.2) / 24f)), .3f * exp);
        else if (attackerLevelAdvantage < 0)
        {
            exp = Math.Min(exp * (1 + ((float)Math.Pow(-attackerLevelAdvantage, 1.2) / 12f)), 6f * exp);
        }

        experience += exp;
    }

    public float CalcScaledExp(float exp, int attackerLevelAdvantage, bool voreSource = false, bool isKill = false)//Used for calculations only
    {
        if (Config.FlatExperience)
        {
            return CalcGiveExp(exp, voreSource, isKill);
        }
        if (State.World.GetEmpireOfSide(Side)?.StrategicAI is StrategicAI ai)
        {
            if (ai.CheatLevel > 0)
                exp *= 1 + .25f * ai.CheatLevel;
        }
        exp *= TraitBoosts.ExpGain;
        if (voreSource) exp *= TraitBoosts.ExpGainFromVore;
        if (voreSource && isKill) exp *= TraitBoosts.ExpGainFromAbsorption;

        if (attackerLevelAdvantage > 0)
            exp = Math.Max(exp * (1 - ((float)Math.Pow(attackerLevelAdvantage, 1.2) / 24f)), .3f * exp);
        else if (attackerLevelAdvantage < 0)
        {
            exp = Math.Min(exp * (1 + ((float)Math.Pow(-attackerLevelAdvantage, 1.2) / 12f)), 6f * exp);
        }

        return exp;
    }

    public static int GetExperienceRequiredForLevel(int level, float expRequiredMod)
    {
        if (level >= Config.HardLevelCap)
            return 99999999;
        return (int)(expRequiredMod * level * Config.ExperiencePerLevel + ((level >= Config.SoftLevelCap ? 8 : 0) + (level * Config.AdditionalExperiencePerLevel * (level - 1) / 2)) *
            (level >= Config.SoftLevelCap ? (int)Math.Pow(2, level + 1 - Config.SoftLevelCap) : 1));
    }

    public static int GetLevelFromExperience(int experience)
    {
        for (int i = 0; i < 200; i++)
        {
            if (GetExperienceRequiredForLevel(i, 1) < experience)
                return i;
        }
        return 200;
    }

    public float Experience => experience;
    public int Level => level;
    public int ExperienceRequiredForNextLevel => GetExperienceRequiredForLevel(level);
    public int GetExperienceRequiredForLevel(int lvl) => GetExperienceRequiredForLevel(lvl, ExpRequiredMod);
    float ExpRequiredMod => TraitBoosts.ExpRequired;
    public bool HasEnoughExpToLevelUp() => Experience >= ExperienceRequiredForNextLevel;

    private float _healthPct = 100f;
    public float HealthPct
    {
        get
        {
             _healthPct = (float) Health / MaxHealth;
            return _healthPct;
        }
    }

    private float _manaPct = 1f;
    public float ManaPct
    {
        get
        {
            _manaPct = (float)Mana / MaxMana;
            return _manaPct;
        }
    }
    private float _stamPct = 100f;
    public float StamPct
    {
        get
        {
            _stamPct = (float)Stamina / MaxStamina;
            return _stamPct;
        }
    }
    private float _barrierPct = 1f;
    public float BarrierPct
    {
        get
        {
            _barrierPct = (float)Barrier / MaxBarrier;
            return _barrierPct;
        }
    }

    internal float GetHealthPctWithoutUpdating() // Important for calculating stat boosts that depend on health percentages, otherwise it's circular.
    {
        return _healthPct;
    }
    internal void ClearAllTraits()
    {
        Tags = new List<Traits>();
        InitializeTraits();
    }

    internal List<Traits> GetTraits
    {
        get
        {
            if (PermanentTraits == null)
                return Tags.ToList();
            if (SharedTraits == null)
            {
                if (ActiveConditionalTraits == null)              
                    return Tags.Concat(PermanentTraits).ToList();
                return Tags.Concat(PermanentTraits).ToList().Concat(ActiveConditionalTraits).ToList();
            }
            if (ActiveConditionalTraits == null)
            {
                return Tags.Concat(PermanentTraits).ToList().Concat(SharedTraits).ToList();
            }
            return Tags.Concat(PermanentTraits).ToList().Concat(SharedTraits).ToList().Concat(ActiveConditionalTraits).ToList();
        }
    }

    internal int BaseTraitsCount => Tags.Count + PermanentTraits.Count;

    public int GetStatBase(Stat stat) => Stats[(int)stat];
    public void SetStatBase(Stat stat, int value) => Stats[(int)stat] = value;
    public void SetStatBaseAll(int value)
    {
        for (int i = 0; i < Stats.Length; i++)
        {
            Stats[i] = value;
        }
    }
    public int GetLeaderBonus()
    {
        if (CurrentLeader == null)
            return 0;
        if (CurrentLeader.IsDead)
            return 0;
        return CurrentLeader.Stats[(int)Stat.Leadership] / 10;
    }
    public int GetTraitBonus(Stat stat)
    {
        if (StatBoosts == null)
            InitializeTraits();
        int bonus = 0;

        if (Harassed)
            bonus -= (int)(GetStatBase(stat) * 0.08f);

        foreach (IStatBoost trait in StatBoosts)
        {
            bonus += trait.StatBoost(this, stat);
        }

        return bonus;
    }
    public int GetEffectBonus(Stat stat)
    {
        float bonus = 0;
        if (stat == Stat.Voracity)
        {
            var effect = GetStatusEffect(StatusEffectType.Predation);
            if (effect != null)
            {
                bonus += (GetStatBase(Stat.Voracity) * .25f);
            }
        }
        else if (stat == Stat.Stomach)
        {
            var effect = GetStatusEffect(StatusEffectType.Predation);
            if (effect != null)
            {
                bonus += (effect.Strength);
            }
        }

        // todo: store the original duration of the effect, maybe?
        // can have it wear off over time, no matter the total duration
        if (GetStatusEffect(StatusEffectType.Empowered) != null)
        {
            StatusEffect eff = GetStatusEffect(StatusEffectType.Empowered);
            bonus += GetStatBase(stat) * eff.Strength * eff.Duration / 5;
        }

        if (GetStatusEffect(StatusEffectType.Berserk) != null)
        {
            if (stat == Stat.Voracity)
            {
                bonus += GetStatBase(Stat.Voracity);
            }
            if (stat == Stat.Strength)
            {
                bonus += GetStatBase(Stat.Strength);
            }
        }

        if (stat == Stat.Strength && GetStatusEffect(StatusEffectType.BladeDance) != null)
            bonus += 2 * GetStatusEffect(StatusEffectType.BladeDance).Duration;
        if (stat == Stat.Agility && GetStatusEffect(StatusEffectType.BladeDance) != null)
            bonus += 1 * GetStatusEffect(StatusEffectType.BladeDance).Duration;

        if (stat == Stat.Strength && GetStatusEffect(StatusEffectType.Tenacious) != null)
            bonus += GetStatBase(Stat.Strength) * .1f * GetStatusEffect(StatusEffectType.Tenacious).Duration;
        if (stat == Stat.Agility && GetStatusEffect(StatusEffectType.Tenacious) != null)
            bonus += GetStatBase(Stat.Agility) * .1f * GetStatusEffect(StatusEffectType.Tenacious).Duration;
        if (stat == Stat.Voracity && GetStatusEffect(StatusEffectType.Tenacious) != null)
            bonus += GetStatBase(Stat.Voracity) * .1f * GetStatusEffect(StatusEffectType.Tenacious).Duration;
        if (stat == Stat.Mind && GetStatusEffect(StatusEffectType.Focus) != null)
        {
            int stacks = GetStatusEffect(StatusEffectType.Focus).Duration;
            bonus += stacks + (GetStatBase(Stat.Mind) * (stacks / 100));
        }
        if (stat == Stat.Mind && GetStatusEffect(StatusEffectType.SpellForce) != null)
        {
            int stacks = GetStatusEffect(StatusEffectType.SpellForce).Duration;
            bonus += stacks + (GetStatBase(Stat.Mind) * (stacks/10));
        }

        if (GetStatusEffect(StatusEffectType.Bolstered) != null)
        {
            int stacks = GetStatusEffect(StatusEffectType.Bolstered).Duration;
            bonus += stacks + (GetStatBase(stat) * (stacks / 100));
        }

        bonus -= GetStatBase(stat) * (GetStatusEffect(StatusEffectType.Shaken)?.Strength ?? 0);

        bonus -= GetStatBase(stat) * (GetStatusEffect(StatusEffectType.Weakness)?.Duration/33 ?? 0);

        if (GetStatusEffect(StatusEffectType.Webbed) != null)
            bonus -= GetStatBase(stat) * .3f;

        if (GetStatusEffect(StatusEffectType.Lethargy) != null)
        {
            StatusEffect eff = GetStatusEffect(StatusEffectType.Lethargy);
            if (stat == Stat.Strength)
            {
                bonus -= GetStatBase(stat) * (eff.Strength * eff.Duration / 50);
            }
            if (stat == Stat.Dexterity)
            {
                bonus -= GetStatBase(stat) * (eff.Strength * eff.Duration / 50);
            }
            if (stat == Stat.Agility)
            {
                bonus -= GetStatBase(stat) * (eff.Strength * eff.Duration / 50);
            }
        }

        return Mathf.RoundToInt(bonus);

    }
    public int GetStat(Stat stat)
    {
        float total = GetStatBase(stat) + GetLeaderBonus() + GetTraitBonus(stat) + GetEffectBonus(stat);

        total *= GetScale();

        total *= TraitBoosts.StatMult;
        switch (stat)
        {
            case Stat.Strength:
                total *= TraitBoosts.StrengthMult;
                break;
            case Stat.Dexterity:
                total *= TraitBoosts.DexterityMult;
                break;
            case Stat.Voracity:
                total *= TraitBoosts.VoracityMult;
                break;
            case Stat.Agility:
                total *= TraitBoosts.AgilityMult;
                break;
            case Stat.Will:
                total *= TraitBoosts.WillMult;
                break;
            case Stat.Mind:
                total *= TraitBoosts.MindMult;
                break;
            case Stat.Endurance:
                total *= TraitBoosts.EnduranceMult;
                break;
            case Stat.Stomach:
                total *= TraitBoosts.StomachMult;
                break;
            default:
                break;
        }
        if (total < 1)
            return 1;

        return Mathf.RoundToInt(total);
    }

    public float GetScale(int power = 1)
    {
        float scale = (float)BaseScale;

        scale *= 1.0f + (GetStatusEffect(StatusEffectType.Enlarged)?.Strength ?? 0f);
        scale *= 1.0f - (GetStatusEffect(StatusEffectType.Diminished)?.Strength ?? 0f);

        scale *= TraitBoosts.Scale;

        return Mathf.Pow(scale, power);
    }

    public string GetStatInfo(Stat stat)
    {
        int modStat = GetStat(stat);
        int baseStat = GetStatBase(stat);
        if (modStat > baseStat)
        {
            if (Config.HideBaseStats)
                return $"<color=#007000ff>{modStat}</color>";
            return $"<color=#007000ff>{modStat}</color> ({baseStat})";
        }
        else if (modStat < baseStat)
        {
            if (Config.HideBaseStats)
                return $"<color=red>{modStat}</color>";
            return $"<color=red>{modStat}</color> ({baseStat})";
        }
        else
            return $"{modStat}";
    }

    public void SetExp(float exp) => experience = exp;

    internal void ModifyStat(int stat, int amount) => ModifyStat((Stat)stat, amount);

    internal void ModifyStat(Stat stat, int amount)
    {
        Stats[(int)stat] += amount;
        if (stat == Stat.Endurance)
            Health += 2 * amount;
        if (stat == Stat.Strength)
            Health += 1 * amount;

    }

    internal void InitializeTraits()
    {
        //foreach (IAttackStatusEffect trait in Unit.TraitsList.Where(s => s is IAttackStatusEffect))
        TraitBoosts = new PermanentBoosts();
        //TraitsList = new List<Trait>();
        StatBoosts = new List<IStatBoost>();
        VoreAttackOdds = new List<IVoreAttackOdds>();
        VoreDefenseOdds = new List<IVoreDefenseOdds>();
        AttackStatusEffects = new List<IAttackStatusEffect>();
        PhysicalDefenseOdds = new List<IPhysicalDefenseOdds>();

        RecalculateStatBoosts();
    }

    internal void RefreshSecrecy()
    {
        if (HasTrait(Traits.Infiltrator) || HasTrait(Traits.Corruption))
            hiddenFixedSide = true;
    }
    internal void InitializeFixedSide(int side)
    {
        if (State.World?.ItemRepository == null) return; //protection for the create strat screen
        if (_fixedSide > -1) return;
        if (HasTrait(Traits.Untamable))
        {
            FixedSide = State.World.GetEmpireOfRace(Race)?.Side ?? side;
            return;
        }
        if (HasTrait(Traits.Infiltrator) || HasTrait(Traits.Corruption))
        {
            FixedSide = side;
            return;
        }
        FixedSide = -1;
    }

    public bool HasTrait(Traits tag)
    {
        if (tag == Traits.TheGreatEscape && Race == Race.Erin)
            return true;
        if (Tags != null)
            return Tags.Contains(tag) || (PermanentTraits?.Contains(tag) ?? false);
        return false;
    }

    public void HealPercentageCap(float rate, int cap)
    {
        int heal = (int)(MaxHealth * rate);
        if (heal <= 0) { heal = 1; }
        heal = Math.Min(heal, cap);
        if (heal > MaxHealth - Health)
            heal = MaxHealth - Health;
        var actor = TacticalUtilities.Units.FirstOrDefault(s => s.Unit == this);
        int modAmount = heal;
        if (GetStatusEffect(StatusEffectType.Necrosis) != null)
        {
            float effect = 0.25f * GetStatusEffect(StatusEffectType.Necrosis).Strength;
            modAmount -= (int)Math.Round(modAmount * effect);
        }
        if (actor != null && heal != 0)
            actor.UnitSprite.DisplayDamage(-modAmount);
        Health += modAmount;
        EquipmentFunctions.CheckEquipment(this, EquipmentActivator.OnHeal, new object[] { this, heal, null });
    }

    public void HealPercentage(float rate)
    {
        rate *= TraitBoosts.PassiveHeal;
        int h = (int)(MaxHealth * rate);
        if (h <= 0) { h = 1; }
        int modAmount = h;
        if (GetStatusEffect(StatusEffectType.Necrosis) != null)
        {
            float effect = 0.25f * GetStatusEffect(StatusEffectType.Necrosis).Strength;
            modAmount -= (int)Math.Round(modAmount * effect);
        }
        Health += modAmount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        EquipmentFunctions.CheckEquipment(this, EquipmentActivator.OnHeal, new object[] { this, h, null });
    }

    public int Heal(int amount)
    {
        int diff = MaxHealth - Health;
        int modAmount = amount;
        if (GetStatusEffect(StatusEffectType.Necrosis) != null)
        {
            float effect = 0.25f * GetStatusEffect(StatusEffectType.Necrosis).Strength;
            modAmount -= (int)Math.Round(modAmount * effect);
        }      
        Health += modAmount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        int actualHeal = Math.Min(diff, modAmount);
        EquipmentFunctions.CheckEquipment(this, EquipmentActivator.OnHeal, new object[] { this, actualHeal, null });
        State.GameManager.TacticalMode?.TacticalStats?.RegisterHealing(actualHeal, Side);
        return actualHeal;
    }

    void NonFatalDamage(int amount, string type)
    {
        if (Health <= 0)
            return;
        int actual = Math.Min(Health - 1, amount);
        Health -= actual;
        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{Name}</b> took <color=red>{actual}</color> points of {type} damage");

    }

    internal string ListTraits(bool hideSecret = false)
    {
        if (Tags.Count == 0 && (PermanentTraits == null || PermanentTraits.Count == 0))
            return "";
        string ret = "";
        for (int i = 0; i < Tags.Count; i++)
        {
            if (!(hideSecret && secretTags.Contains(Tags[i])))
            {
                bool color = false;
                string tagString = "";
                if (ret != "")
                    ret += "\n";
                if (TemporaryTraits != null && TemporaryTraits.Count > 0 && TemporaryTraits.Contains(Tags[i]))
                {
                    if (PermanentTraits != null && PermanentTraits.Count > 0 && !PermanentTraits.Contains(Tags[i]))
                        color = true;
                }
                if (State.RandomizeLists.Any(rl => (Traits)rl.id == Tags[i]))
                    tagString = State.RandomizeLists.Where(rl => (Traits)rl.id == Tags[i]).FirstOrDefault().name;
                else if (State.CustomTraitList.Any(ct => (Traits)ct.id == Tags[i]))
                    tagString = State.CustomTraitList.Where(ct => (Traits)ct.id == Tags[i]).FirstOrDefault().name;
                else if (State.ConditionalTraitList.Any(ct => (Traits)ct.id == Tags[i]))
                    tagString = State.ConditionalTraitList.Where(ct => (Traits)ct.id == Tags[i]).FirstOrDefault().name;
                else
                    tagString = Tags[i].ToString();

                ret += color ? "<color=#402B8Dff>" + tagString + "</color>" : tagString;
            }
        }
        if (PermanentTraits != null && PermanentTraits.Count > 0)
        {
            for (int i = 0; i < PermanentTraits.Count; i++)
            {
                if (Tags.Contains(PermanentTraits[i]))
                    continue;
                if (!(hideSecret && secretTags.Contains(PermanentTraits[i])))
                {
                    if (ret != "")
                        ret += "\n";
                    if (State.RandomizeLists.Any(rl => (Traits)rl.id == PermanentTraits[i]))
                        ret += State.RandomizeLists.Where(rl => (Traits)rl.id == PermanentTraits[i]).FirstOrDefault().name;
                    else if (State.CustomTraitList.Any(ct => (Traits)ct.id == PermanentTraits[i]))
                        ret += State.CustomTraitList.Where(ct=> (Traits)ct.id == PermanentTraits[i]).FirstOrDefault().name;
                    else if (State.ConditionalTraitList.Any(ct => (Traits)ct.id == PermanentTraits[i]))
                        ret += State.ConditionalTraitList.Where(ct=> (Traits)ct.id == PermanentTraits[i]).FirstOrDefault().name;
                    else
                        ret += PermanentTraits[i].ToString();
                    }
            }
        }
        return ret;
    }

    public bool IsEnemyOfSide(int side)
    {
        return (Side != side);
    }

    public void AddTraits(List<Traits> traitIdsToAdd)
    {
        foreach (var traitId in traitIdsToAdd)
        {
            AddTrait(traitId);
        }

        return;
    }

    public void AddTrait(Traits traitIdToAdd)
    {
        if (Tags == null)
            Tags = new List<Traits>();

        if (HasTrait(traitIdToAdd))
            return;

        if (traitIdToAdd >= (Traits)6000)
        {
            var newTrait = State.ConditionalTraitList.Where(t => t.id == (int)traitIdToAdd).First();
            AllConditionalTraits.Add(newTrait, ConditionalTraitConditionChecker.StrategicTraitConditionActive(this, newTrait));
        }

        Tags.Add(traitIdToAdd);
        RecalculateStatBoosts();
    }

    public bool AddPermanentTrait(Traits traitIdToAdd)
    {
        if (PermanentTraits == null)
            PermanentTraits = new List<Traits>();

        if (traitIdToAdd == Traits.Prey)
        {
            Predator = false;
            fixedPredator = true;
        }

        if (HasTrait(traitIdToAdd))
            return false;

        if (traitIdToAdd >= (Traits)6000)
        {
            var newTrait = State.ConditionalTraitList.Where(t => t.id == (int)traitIdToAdd).First();
            AllConditionalTraits.Add(newTrait, ConditionalTraitConditionChecker.StrategicTraitConditionActive(this,newTrait));
        }

        PermanentTraits.Add(traitIdToAdd);
        RecalculateStatBoosts();
        return true;
    }

    public void RemoveTrait(Traits traitToRemove)
    {
        if (Tags == null)
            return;

        if (RemovedTraits == null)
            RemovedTraits = new List<Traits>();

        if (traitToRemove == Traits.Prey)
        {
            if (RaceParameters.GetTraitData(this).AllowedVoreTypes.Any())
            {
                Predator = true;
                fixedPredator = true;
            }
            else //Cancel removal in this case
                return;

        }

        RemovedTraits.Add(traitToRemove);

        if (HasTrait(traitToRemove))
        {
            Tags.Remove(traitToRemove);
            PermanentTraits?.Remove(traitToRemove);
            RecalculateStatBoosts();
        }

    }

    protected void RecalculateStatBoosts()
    {
        float healthBefore = HealthPct;
        RefreshSecrecy();
        InitializeFixedSide(Side);
        if (Tags == null)
            return;

        TraitBoosts = new PermanentBoosts();
        StatBoosts.Clear();
        VoreAttackOdds.Clear();
        VoreDefenseOdds.Clear();
        PhysicalDefenseOdds.Clear();
        AttackStatusEffects.Clear();
        if (PermanentTraits == null)
        {
            foreach (var trait in Tags)
            {
                Trait ITrait;
                if (trait >= (Traits)3001 && trait <= (Traits)5999)
                    ITrait = State.CustomTraitList.Where(ct => trait == (Traits)ct.id).FirstOrDefault().ToBooster();
                else
                    ITrait = TraitList.GetTrait(trait);              
                if (ITrait is IStatBoost boost)
                    StatBoosts.Add(boost);
                if (ITrait is IVoreAttackOdds voreAttackOdds)
                    VoreAttackOdds.Add(voreAttackOdds);
                if (ITrait is IVoreDefenseOdds voreDefenseOdds)
                    VoreDefenseOdds.Add(voreDefenseOdds);
                if (ITrait is IPhysicalDefenseOdds physicalDefenseOdds)
                    PhysicalDefenseOdds.Add(physicalDefenseOdds);
                if (ITrait is IAttackStatusEffect attackStatusEffect)
                    AttackStatusEffects.Add(attackStatusEffect);
                if (ITrait is AbstractBooster booster)
                    booster.Boost(TraitBoosts);
            }
        }

        if (PermanentTraits != null)
        {
            foreach (var trait in Tags.Concat(PermanentTraits).Distinct())
            {
                Trait ITrait;
                if (trait >= (Traits)3001 && trait <= (Traits)5999)
                    ITrait = State.CustomTraitList.Where(ct => trait == (Traits)ct.id).FirstOrDefault().ToBooster();
                else
                    ITrait = TraitList.GetTrait(trait);
                if (ITrait is IStatBoost boost)
                    StatBoosts.Add(boost);
                if (ITrait is IVoreAttackOdds voreAttackOdds)
                    VoreAttackOdds.Add(voreAttackOdds);
                if (ITrait is IVoreDefenseOdds voreDefenseOdds)
                    VoreDefenseOdds.Add(voreDefenseOdds);
                if (ITrait is IPhysicalDefenseOdds physicalDefenseOdds)
                    PhysicalDefenseOdds.Add(physicalDefenseOdds);
                if (ITrait is IAttackStatusEffect attackStatusEffect)
                    AttackStatusEffects.Add(attackStatusEffect);
                if (ITrait is AbstractBooster booster)
                    booster.Boost(TraitBoosts);
            }
        }

        if (SharedTraits != null)
        {
            foreach (var trait in SharedTraits)
            {
                Trait ITrait;
                if (trait >= (Traits)3000 && trait <= (Traits)5999)
                    ITrait = State.CustomTraitList.Where(ct => trait == (Traits)ct.id).FirstOrDefault().ToBooster();
                else
                    ITrait = TraitList.GetTrait(trait);   
                if (ITrait is IStatBoost boost)
                    StatBoosts.Add(boost);
                if (ITrait is IVoreAttackOdds voreAttackOdds)
                    VoreAttackOdds.Add(voreAttackOdds);
                if (ITrait is IVoreDefenseOdds voreDefenseOdds)
                    VoreDefenseOdds.Add(voreDefenseOdds);
                if (ITrait is IPhysicalDefenseOdds physicalDefenseOdds)
                    PhysicalDefenseOdds.Add(physicalDefenseOdds);
                if (ITrait is IAttackStatusEffect attackStatusEffect)
                    AttackStatusEffects.Add(attackStatusEffect);
                if (ITrait is AbstractBooster booster)
                    booster.Boost(TraitBoosts);
            }
        }
    }

    internal void SetMaxItems()
    {

        if (HasTrait(Traits.Resourceful) == false)
        {
            if (Items.Count() == 3)
                SetItem(null, 2);
            if (Items.Count() == 2)
                return;
            if (Items.Count() == 0)
            {
                Items = new Item[2];
                return;
            }
            Item[] tempItems = new Item[]
            {
                    Items[0],
                    Items[1],
            };
            Items = tempItems;
        }
        else
        {
            if (Items.Count() == 3)
                return;
            if (Items.Count() == 2)
            {
                Item[] tempItems = new Item[]
                {
                    Items[0],
                    Items[1],
                    null
                };
                Items = tempItems;
            }
            else
                Items = new Item[3];
        }

    }

    internal void AddTemporaryTrait(Traits trait)
    {
        if (TemporaryTraits == null)
            TemporaryTraits = new List<Traits>();
        if (TemporaryTraits.Contains(trait) == false)
            TemporaryTraits.Add(trait);
        if (TemporaryTraits.Count >= 4)
            TemporaryTraits.RemoveAt(0);
    }

    public bool HasSharedTrait(Traits trait)
    {
        if (SharedTraits == null)
            SharedTraits = new List<Traits>();
        return (SharedTraits.Contains(trait));
    }

    public void AddSharedTrait(Traits trait)
    {
        if (SharedTraits == null)
            SharedTraits = new List<Traits>();
        if (!SharedTraits.Contains(trait) && !HasTrait(trait))
            SharedTraits.Add(trait);
            AddTrait(trait);

    }

    public void ResetSharedTraits()
    {
        if (SharedTraits == null)
            SharedTraits = new List<Traits>();
        foreach (Traits trait in SharedTraits)
        {
            RemoveTrait(trait);
        }
        SharedTraits.Clear();
    }

    public void RemoveSharedTrait(Traits trait)
    {
        if (SharedTraits == null)
            SharedTraits = new List<Traits>();
        if (SharedTraits.Contains(trait) && HasTrait(trait))
            SharedTraits.Remove(trait);
            RemoveTrait(trait);
    }

    public bool HasPersistentSharedTrait(Traits trait)
    {
        if (PersistentSharedTraits == null)
            PersistentSharedTraits = new List<Traits>();
        return (PersistentSharedTraits.Contains(trait));
    }

    public void AddPersistentSharedTrait(Traits trait)
    {
        if (PersistentSharedTraits == null)
            PersistentSharedTraits = new List<Traits>();
        if (!PersistentSharedTraits.Contains(trait) && !HasTrait(trait))
            PersistentSharedTraits.Add(trait);
        AddTrait(trait);

    }

    public void ResetPersistentSharedTraits()
    {
        if (PersistentSharedTraits == null)
            PersistentSharedTraits = new List<Traits>();
        foreach (Traits trait in PersistentSharedTraits)
        {
            RemoveTrait(trait);
        }
        PersistentSharedTraits.Clear();
    }

    public void RemovePersistentSharedTrait(Traits trait)
    {
        if (PersistentSharedTraits == null)
            PersistentSharedTraits = new List<Traits>();
        if (PersistentSharedTraits.Contains(trait) && HasTrait(trait))
            PersistentSharedTraits.Remove(trait);
        RemoveTrait(trait);
    }

    public bool HasActiveConditionalTrait(Traits trait)
    {
        if (ActiveConditionalTraits == null)
            ActiveConditionalTraits = new List<Traits>();
        return (ActiveConditionalTraits.Contains(trait));
    }

    public void AddActiveConditionalTrait(Traits trait)
    {
        if (ActiveConditionalTraits == null)
            ActiveConditionalTraits = new List<Traits>();
        if (!ActiveConditionalTraits.Contains(trait) && !HasTrait(trait))
            ActiveConditionalTraits.Add(trait);
        AddTrait(trait);

    }

    public void ResetConditionalTraits()
    {
        if (ActiveConditionalTraits == null)
            ActiveConditionalTraits = new List<Traits>();
        foreach (Traits trait in ActiveConditionalTraits)
        {
            RemoveTrait(trait);
        }
        ActiveConditionalTraits.Clear();
    }

    public void RemoveActiveConditionalTrait(Traits trait)
    {
        if (ActiveConditionalTraits == null)
            ActiveConditionalTraits = new List<Traits>();
        if (ActiveConditionalTraits.Contains(trait) && HasTrait(trait))
            ActiveConditionalTraits.Remove(trait);
        RemoveTrait(trait);
    }

    public void ActivateConditionalTrait(int id)
    {
        if (AllConditionalTraits == null)
            AllConditionalTraits = new Dictionary<ConditionalTraitContainer, bool>();
        if (AllConditionalTraits.Keys.Where(t => t.id == id).Any())
        {
            var toAdd = AllConditionalTraits.Keys.Where(t => t.id == id).First();
            AllConditionalTraits[toAdd] =  true;
            AddActiveConditionalTrait(toAdd.associatedTrait);
            if (toAdd.classification == TraitConditionalClassification.Permanent)
            {
                if (Tags.Contains((Traits)id))
                {
                    Tags.Remove((Traits)id);
                }
                if (PermanentTraits.Contains((Traits)id))
                {
                    PermanentTraits.Remove((Traits)id);
                }
                if (SharedTraits != null)
                {
                    if (SharedTraits.Contains((Traits)id))
                        SharedTraits.Remove((Traits)id);
                }
                AllConditionalTraits.Remove(toAdd);
            }
            RecalculateStatBoosts();
            PreyCheck();
        }

    }
    public void DeactivateConditionalTrait(int id)
    {
        if (AllConditionalTraits == null)
            AllConditionalTraits = new Dictionary<ConditionalTraitContainer, bool>();
        if (AllConditionalTraits.Keys.Where(t => t.id == id).Any())
        {
            var toRemove = AllConditionalTraits.Keys.Where(t => t.id == id).First();
            AllConditionalTraits[toRemove] = false;
            RemoveActiveConditionalTrait(toRemove.associatedTrait);
            if (toRemove.classification == TraitConditionalClassification.Temporary)
            {
                if (Tags.Contains((Traits)id))
                {
                    Tags.Remove((Traits)id);
                }
                if (PermanentTraits.Contains((Traits)id))
                {
                    PermanentTraits.Remove((Traits)id);
                }
                if (SharedTraits != null)
                {
                    if (SharedTraits.Contains((Traits)id))
                        SharedTraits.Remove((Traits)id);
                }
            }
            RecalculateStatBoosts();
            PreyCheck();
        }
    }

    internal void ReloadTraits()//Add unit-based null checks for newly added internal(s) or protected(s) to this void so that on loading an older version saves, units will recive them
    {
        Tags = new List<Traits>();
        if (AllConditionalTraits == null)
            AllConditionalTraits = new Dictionary<ConditionalTraitContainer, bool>();
        if (Config.RaceTraitsEnabled)
            Tags.AddRange(State.RaceSettings.GetRaceTraits(HiddenUnit.Race));
        if (HiddenUnit.HasBreasts && HiddenUnit.HasDick == false)
        {
            var femaleTraits = State.RaceSettings.GetFemaleRaceTraits(HiddenUnit.Race);
            if (femaleTraits != null) Tags.AddRange(femaleTraits);
            femaleTraits = Config.FemaleTraits;
            if (femaleTraits != null) Tags.AddRange(femaleTraits);
        }
        else if (!HiddenUnit.HasBreasts && HiddenUnit.HasDick)
        {
            var maleTraits = State.RaceSettings.GetMaleRaceTraits(HiddenUnit.Race);
            if (maleTraits != null) Tags.AddRange(maleTraits);
            maleTraits = Config.MaleTraits;
            if (maleTraits != null) Tags.AddRange(maleTraits);
        }
        else
        {
            var hermTraits = State.RaceSettings.GetHermRaceTraits(HiddenUnit.Race);
            if (hermTraits != null) Tags.AddRange(hermTraits);
            hermTraits = Config.HermTraits;
            if (hermTraits != null) Tags.AddRange(hermTraits);
        }
        if (Type == UnitType.Leader)
        {
            var leaderTraits = State.RaceSettings.GetLeaderRaceTraits(HiddenUnit.Race);
            if (leaderTraits != null) Tags.AddRange(leaderTraits);
            if (Config.LeaderTraits != null) Tags.AddRange(Config.LeaderTraits);
        }
        else if (Type == UnitType.Spawn)
        {
            var spawnTraits = State.RaceSettings.GetSpawnRaceTraits(HiddenUnit.Race);
            if (spawnTraits != null) Tags.AddRange(spawnTraits);
            spawnTraits = Config.SpawnTraits;
            if (spawnTraits != null) Tags.AddRange(spawnTraits);
        }
        if (TemporaryTraits != null)
            Tags.AddRange(TemporaryTraits);
        if (SharedTraits != null)
            Tags.AddRange(SharedTraits);
        if (PersistentSharedTraits != null)
            Tags.AddRange(PersistentSharedTraits);
        if (!State.GameManager.PureTactical && (State.GameManager.CurrentScene == State.GameManager.StrategyMode || State.GameManager.CurrentScene == State.GameManager.TacticalMode || State.GameManager.CurrentScene == State.GameManager.Recruit_Mode))//Execption fix due to how saves are loaded
        {
            if (State.World.GetEmpireOfSide(HiddenUnit.Side) == null) {}
            else if (State.World.GetEmpireOfSide(HiddenUnit.Side).EmpTraits != null)
                foreach (Traits trait in State.World.GetEmpireOfSide(HiddenUnit.Side).EmpTraits)
                {
                    AddPermanentTrait(trait);
                }
        }
        if (RemovedTraits != null)
        {
            foreach (Traits trait in RemovedTraits)
            {
                Tags.Remove(trait);
            }
        }
        if (!State.TutorialMode)
            RandomizeTraits();
         Tags = Tags.Distinct().ToList();
        foreach (var t in Tags)
        {
            if (t >= (Traits)6000)
            {
                if (State.ConditionalTraitList.Count <= 0)
                {
                    continue;
                }
                var newTrait = State.ConditionalTraitList.Where(x => x.id == (int)t).First();
                if (!AllConditionalTraits.ContainsKey(newTrait))
                {
                    AllConditionalTraits.Add(newTrait, ConditionalTraitConditionChecker.StrategicTraitConditionActive(this, newTrait));
                }
            }
        }
        PreyCheck();
        SetMaxItems();
        if (EquippedPotions == null)
            EquippedPotions = new Dictionary<int, int[]>();
        //if (HasTrait(Traits.Shapeshifter) || HasTrait(Traits.Skinwalker))
        //{
        //    if (ShifterShapes == null)
        //        ShifterShapes = new List<Unit>();
        //    if (!ShifterShapes.Contains(this))
        //        AcquireShape(this, true);
        //}
    }

    public void PreyCheck()
    {
        if (HasTrait(Traits.Prey))
            Predator = false;
        else if (fixedPredator == false)
            Predator = State.World?.GetEmpireOfRace(HiddenUnit.Race)?.CanVore ?? true;
        Tags.RemoveAll(s => s == Traits.Prey);
        if (RaceParameters.GetTraitData(HiddenUnit).AllowedVoreTypes.Any() == false)
            Predator = false;
        if (HiddenUnit.Predator == false && !HasTrait(Traits.Prey))
            Tags.Add(Traits.Prey);
    }

    public void ChangeRace(Race race)
    {
        Race = race;
        fixedPredator = false;
    }

    public void HideRace(Race race, Unit appearance = null)
    {
        _hiddenUnit = Clone();
        Race = race;
        fixedPredator = false;
        if (appearance != null)
            CopyAppearance(appearance);
        else
        {
            var NewRace = Races.GetRace(race);
            NewRace.RandomCustom(this);
        }

    }

    public void UnhideRace()
    {
        Race = HiddenUnit.Race;
        fixedPredator = false;
        CopyAppearance(HiddenUnit);
        _hiddenUnit = null;
    }

    private void RandomizeTraits()
    {
        while (true)
        {
            var customs = Tags.Where(t => State.RandomizeLists.Any(rl => (Traits)rl.id == t && rl.level <= level)).ToList();
            customs.AddRange(PermanentTraits.Where(t => State.RandomizeLists.Any(rl => (Traits)rl.id == t && rl.level <= level)));
            if (!customs.Any())
                break;
            customs.ForEach(ct =>
            {
                    RandomizeList randomizeList = State.RandomizeLists.Single(rl => (Traits)rl.id == ct);
                    var chance = randomizeList.chance;
                    var rolls = UnityEngine.Random.Range(0,randomizeList.count);
                    while (chance > 0 && State.Rand.NextDouble() < randomizeList.chance)
                    {
                        List<Traits> gainable = randomizeList.RandomTraits.Where(rt => !Tags.Contains(rt) && !PermanentTraits.Contains(rt)).ToList();
                        if (gainable.Count() > 0)
                        {                           
                            var randomPick = gainable[State.Rand.Next(gainable.Count())];
                            PermanentTraits.Add(randomPick);
                            RemovedTraits?.Remove(randomPick); // Even if manually removed before, rng-sus' word is law
                            gainable.Remove(randomPick);
                            GivePrerequisiteTraits(randomPick);
                        }
                        if(!(rolls > 0))
                            chance -= 1;
                        rolls--;
                    }
                    if (RemovedTraits == null)
                        RemovedTraits = new List<Traits>();
                    RemovedTraits.Add(ct);
                    foreach (Traits trait in RemovedTraits)
                    {
                        Tags.Remove(trait);
                        PermanentTraits.Remove(trait);
                    }
                
            });
        }

    }

    private void GivePrerequisiteTraits(Traits randomPick)
    {
        Traits prereq = (Traits)(-1);
        if (randomPick > Traits.Growth && randomPick <= Traits.FleetingGrowth)
        {
            prereq = Traits.Growth;
        }
        if ( randomPick == Traits.HealingBelly)
        {
            prereq = Traits.FriendlyStomach;
        }if ( randomPick == Traits.Friendosoma)
        {
            prereq = Traits.Endosoma;
        }
        if (randomPick == Traits.VenomousBite)
        {
            prereq = Traits.Biter;
        }
        if (randomPick == Traits.SynchronizedEvolution)
        {
            prereq = Traits.Assimilate;
        }
        if (randomPick == Traits.PredConverter || randomPick == Traits.PredRebirther || randomPick == Traits.PredGusher)
        {
            if (RaceParameters.GetRaceTraits(Race).AllowedVoreTypes.Contains(VoreType.Unbirth))
                HasVagina = true;
        }
        if (randomPick == Traits.HeavyPounce)
        {
            prereq = Traits.Pounce;
        }
        if (prereq != (Traits)(-1) && !Tags.Contains(prereq) && !PermanentTraits.Contains(prereq))
        {
            PermanentTraits.Add(prereq);
            RemovedTraits?.Remove(prereq);
        }
    }

    public void SetSizeToDefault() => BreastSize = DefaultBreastSize;

    internal void SetDefaultBreastSize(int size, bool update = true)
    {
        DefaultBreastSize = size;
        if (update) BreastSize = DefaultBreastSize;
    }

    public void SetBreastSize(int size)
    {
        if (size > Races.GetRace(this).BreastSizes - 1)
            size = Races.GetRace(this).BreastSizes - 1;
        if (size <= DefaultBreastSize)
            size = DefaultBreastSize;
        BreastSize = size;
    }

    public Stat[] GetLevelUpPossibilities(bool canVore)
    {
        if (Type == UnitType.Leader)
            return Leader.GetLevelUpPossibilities();
        int[] stats = new int[(int)Stat.None - 1];
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = i;
        }



        if (canVore == false)
        {
            stats[(int)Stat.Voracity] = -1;
            stats[(int)Stat.Stomach] = -1;
        }

        if (HasTrait(Traits.Brainless))
        {
            stats[(int)Stat.Mind] = -1;
        }
        //stats[(int)Stat.Leadership] = -1; unneeded as the stats already cuts it
        if (TraitBoosts.OnLevelUpAllowAnyStat)
        {
            Stat[] ret2 = new Stat[stats.Length];
            for (int i = 0; i < ret2.Length; i++)
            {
                ret2[i] = (Stat)i;
            }
            return ret2;
        }

        var traits = RaceParameters.GetTraitData(this);
        var favored = State.RaceSettings.GetFavoredStat(Race);

        if (favored != Stat.None)
            stats[(int)favored] = -1;

        stats = stats.Where(s => s >= 0).ToArray();

        for (int i = 0; i < stats.GetUpperBound(0); i++) //Randomize the order
        {
            int j = State.Rand.Next(i, stats.GetUpperBound(0) + 1);
            int temp = stats[i];
            stats[i] = stats[j];
            stats[j] = temp;
        }
        if (favored == Stat.None) //If no favored stat
        {
            Stat[] ret3 = new Stat[3];

            for (int i = 0; i < 3; i++)
            {
                ret3[i] = (Stat)stats[i];
            }
            return ret3;
        }
        Stat[] ret = new Stat[4];

        ret[0] = favored;
        for (int i = 1; i < 4; i++)
        {
            ret[i] = (Stat)stats[i];
        }
        return ret;

    }

    public void GeneralStatIncrease(int amount)
    {
        for (int x = 0; x < Stats.Length; x++)
        {
            if (Stats[x] > 0)
                Stats[x] += amount;
        }
        Health += 3 * amount;
    }
    public void RandomStatIncrease(int amount)
    {
        int x = State.Rand.Next(Stats.Length);
        if (Stats[x] > 0)
            Stats[x] += amount;      
    }
    public void SpecificStatIncrease(int amount, int index)
    {
        Stats[index] += amount;
    }

    public void LevelUp(Stat stat)
    {
        GeneralStatIncrease(1);
        if (TraitBoosts.OnLevelUpBonusToAllStats > 0)
        {
            GeneralStatIncrease(TraitBoosts.OnLevelUpBonusToAllStats);
        }

        if (TraitBoosts.OnLevelUpBonusToGiveToTwoRandomStats > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                int bonusStat = State.Rand.Next((int)Stat.None - 1);
                ModifyStat(bonusStat, TraitBoosts.OnLevelUpBonusToGiveToTwoRandomStats);
            }
        }

        if (Config.LeadersAutoGainLeadership)
        {
            ModifyStat((int)Stat.Leadership, 2);
        }
        level++;
        Stats[(int)stat] += 4;
        if (stat == Stat.Endurance)
        {
            Health += 8;
        }
        if (stat == Stat.Strength)
        {
            Health += 4;
        }

        foreach (var item in AllConditionalTraits.Keys.Where(t => t.trigger == TraitConditionTrigger.OnLevelUp || t.trigger == TraitConditionTrigger.All).ToList())
        {
            if (ConditionalTraitConditionChecker.StrategicTraitConditionActive(this, item))
            {
                ActivateConditionalTrait(item.id);
            }
            else
            {
                DeactivateConditionalTrait(item.id);
            }
        }

        RandomizeTraits();
    }

    public void LeaderLevelDown()
    {
        SubtractLevels(Config.LeaderLossLevels);
        SubtractFraction(Config.LeaderLossExpPct);
        if (Config.WeightGain)
        {
            if (DefaultBreastSize > 0)
                DefaultBreastSize -= 1;
            if (DickSize > 0)
                DickSize -= 1;
            if (BodySize > 0)
                BodySize -= 1;
            if (BodySize > 0)
                BodySize -= 1;
        }
    }

    /// <summary>
    /// Subtracts this fraction from the total unit experience, leveling down as needed (i.e. .25 would cause a unit to lose 25% of its total XP)
    /// </summary>
    internal void SubtractFraction(float fraction)
    {
        if (fraction > 1 || fraction < 0)
        {
            UnityEngine.Debug.LogWarning("Invalid amount passed to subtractFraction");
            return;
        }
        if (fraction == 0)
            return;
        int startingLevel = level;
        if (Config.LeaderLossExpPct > 0)
        {
            experience *= (1 - fraction);

            for (int i = 0; i < startingLevel - 1; i++)
            {
                if (experience < GetExperienceRequiredForLevel(level - 1))
                {
                    LevelDown();
                }
            }

        }
    }

    /// <summary>
    /// Subtracts this many levels from the unit while keeping its % of xp to the next level constant
    /// </summary>
    internal void SubtractLevels(int levelsLost)
    {
        if (levelsLost < 0)
        {
            UnityEngine.Debug.LogWarning("Invalid amount passed to subtractFraction");
            return;
        }
        if (levelsLost == 0)
            return;
        if (levelsLost >= 1 && level > 1)
        {
            for (int i = 0; i < levelsLost; i++)
            {
                experience -= GetExperienceRequiredForLevel(level) - GetExperienceRequiredForLevel(level - 1);
                LevelDown();
                if (level <= 1)
                    break;
            }
            if (experience < 0)
                experience = 0;
        }
    }

    public virtual int GetStatTotal()
    {
        return GetStat(Stat.Agility) + GetStat(Stat.Will) + GetStat(Stat.Mind)
            + GetStat(Stat.Dexterity) + GetStat(Stat.Endurance) + GetStat(Stat.Strength)
            + GetStat(Stat.Voracity) + GetStat(Stat.Stomach);
    }

    public int GetHighestStatIndex()
    {
        int highestType = 0;
        for (int i = 0; i < Stats.Length; i++)
        {
            if (Stats[i] > Stats[highestType])
                highestType = i;
        }
        return highestType;
    }
    public int GetLowestStatIndex()
    {
        int lowestType = 0;
        for (int i = 0; i < Stats.Length; i++)
        {
            if (Stats[lowestType] > Stats[i])
                lowestType = i;
        }
        return lowestType;
    }
    
    public void LevelDown()
    {
        if (level <= 1)
            return;
        int highestType = 0;
        for (int i = 0; i < Stats.Length; i++)
        {
            if (Stats[i] > Stats[highestType])
                highestType = i;
        }
        LevelDown((Stat)highestType);
    }

    public void LevelDown(Stat stat)
    {
        if (level <= 1)
            return;
        GeneralStatIncrease(-1);
        if (TraitBoosts.OnLevelUpBonusToAllStats > 0)
        {
            GeneralStatIncrease(-1 * TraitBoosts.OnLevelUpBonusToAllStats);
        }

        level--;
        int loweredStat = (int)stat;

        if (TraitBoosts.OnLevelUpBonusToGiveToTwoRandomStats > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                int bonusStat = State.Rand.Next((int)Stat.None - 1);
                ModifyStat(bonusStat, -1 * TraitBoosts.OnLevelUpBonusToGiveToTwoRandomStats);
            }
        }

        Stats[loweredStat] -= 4;
        if (loweredStat == (int)Stat.Endurance)
        {
            Health -= 8;
        }

        if (loweredStat == (int)Stat.Strength)
        {
            Health -= 4;
        }


    }

    public void Feed()
    {
        GiveExp(1, true);
        Health += 10;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    void RandomSkills()
    {
        var raceStats = State.RaceSettings.GetRaceStats(Race);
        if (raceStats != null)
        {
            Stats[(int)Stat.Strength] = raceStats.Strength.Minimum + State.Rand.Next(raceStats.Strength.Roll);
            Stats[(int)Stat.Dexterity] = raceStats.Dexterity.Minimum + State.Rand.Next(raceStats.Dexterity.Roll);
            Stats[(int)Stat.Endurance] = raceStats.Endurance.Minimum + State.Rand.Next(raceStats.Endurance.Roll);
            Stats[(int)Stat.Mind] = raceStats.Mind.Minimum + State.Rand.Next(raceStats.Mind.Roll);
            Stats[(int)Stat.Will] = raceStats.Will.Minimum + State.Rand.Next(raceStats.Will.Roll);
            Stats[(int)Stat.Agility] = raceStats.Agility.Minimum + State.Rand.Next(raceStats.Agility.Roll);
            Stats[(int)Stat.Voracity] = raceStats.Voracity.Minimum + State.Rand.Next(raceStats.Voracity.Roll);
            Stats[(int)Stat.Stomach] = raceStats.Stomach.Minimum + State.Rand.Next(raceStats.Stomach.Roll);
        }
        else
        {
            //These should not trigger under almost all circumstances, it uses the data at the bottom of RaceParameters now
            Stats[(int)Stat.Strength] = 6 + State.Rand.Next(9);
            Stats[(int)Stat.Dexterity] = 6 + State.Rand.Next(9);
            Stats[(int)Stat.Endurance] = 8 + State.Rand.Next(6);
            Stats[(int)Stat.Mind] = 6 + State.Rand.Next(8);
            Stats[(int)Stat.Will] = 6 + State.Rand.Next(8);
            Stats[(int)Stat.Agility] = 6 + State.Rand.Next(5);
            Stats[(int)Stat.Voracity] = 5 + State.Rand.Next(7);
            Stats[(int)Stat.Stomach] = 12 + State.Rand.Next(4);
        }


    }

    public Weapon GetBestMelee()
    {
        if (HasTrait(Traits.Feral))
        {
            return State.World.ItemRepository.Claws;
        }
        Weapon best = null;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] is Weapon weapon)
            {
                if (weapon.Range <= 1)
                {
                    if (best == null)
                    {
                        best = weapon;
                    }
                    else if (weapon.Damage > best.Damage)
                        best = weapon;
                }
            }
        }

        if (best == null)
        {
            best = State.World.ItemRepository.Claws;
        }
        return best;
    }

    public Weapon GetBestRanged()
    {
        if (HasTrait(Traits.Feral))
        {
            return null;
        }
        Weapon best = null;

        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] is Weapon weapon)
            {
                if (weapon.Range > 1)
                {
                    if (best == null)
                    {
                        best = weapon;
                    }
                    else if (weapon.Range > best.Range)
                        best = weapon;
                }
            }
        }
        return best;
    }

    public Item GetItem(int i) => Items[i];
    public int GetItemSlot(Item item)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == item)
                return i;
        }
        return -1;
    }

    public bool HasFreeItemSlot()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
                return true;
        }
        return false;
    }

    public void UpdateItems(ItemRepository NewRepository)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null)
                SetItem(NewRepository.GetNewItemType(Items[i]), i);
        }
    }

    void AddAccessory(Accessory accessory)
    {
        Stats[accessory.ChangedStat] += accessory.StatBonus;
        if (accessory.ChangedStat == (int)Stat.Endurance)
            Health += 2 * accessory.StatBonus;
    }

    void RemoveAccessory(Accessory accessory)
    {
        Stats[accessory.ChangedStat] -= accessory.StatBonus;
        if (accessory.ChangedStat == (int)Stat.Endurance)
        {
            Health -= 2 * accessory.StatBonus;
            if (Health < 1)
                Health = 1;
        }
    }

    public void SetItem(Item item, int i, bool fromUnitEditor = false)
    {
        if (item == null && (ShifterShapes?.Any() ?? false))
        {
            ShifterShapes.Where(shape => shape.GetItem(i) == GetItem(i)).ForEach(s => // test what happens if main has resourceful but shape has not
            {
                s.Items[i] = null;
            });
        }
        if (Items.Length <= i)
        {
            UnityEngine.Debug.LogWarning("Tried to assign item to a non-existent slot!");
            return;
        }
        if (item is SpellBook && HasTrait(Traits.BookEater) && !fromUnitEditor)
        {
            InnateSpells.Add(((SpellBook)item).ContainedSpell);
            return;
        }
        if (Items[i] != null)
        {
            if (Items[i] is Accessory)
            {
                RemoveAccessory((Accessory)Items[i]);
            }
            if (Items[i] is Equipment)
            {
                ItemCooldowns[i] = 0;
                ItemUses[i] = 0;
            }
        }
        Items[i] = item;
        if (Items[i] != null)
        {
            if (item is Accessory)
            {
                AddAccessory((Accessory)item);
            }
            if (Items[i] is Equipment)
            {
                Equipment equipment = (Equipment)Items[i];
                ItemUses[i] = equipment.ItemUses;
            }
        }
    }

    public void UpdateSpells()
    {
        UseableSpells = new List<Spell>();

        if (InnateSpells != null)
        {
            foreach (SpellTypes type in InnateSpells)
            {
                if (SpellList.SpellDict.TryGetValue(type, out Spell spell))
                {
                    UseableSpells.Add(spell);
                    if (GetStatusEffect(StatusEffectType.Bloodrite) != null) UseableSpells.Remove(SpellList.Bloodrite);
                }
            }
        }

        if (SingleUseSpells?.Any() ?? false)
        {
            foreach (var spellType in SingleUseSpells)
            {
                if (SpellList.SpellDict.TryGetValue(spellType, out Spell spell))
                {
                    UseableSpells.Add(spell);
                }
            }

        }

        if (MultiUseSpells?.Any() ?? false)
        {
            foreach (var spellType in MultiUseSpells)
            {
                if (SpellList.SpellDict.TryGetValue(spellType, out Spell spell))
                {
                    UseableSpells.Add(spell);
                }
            }

        }

        var racePar = RaceParameters.GetTraitData(this);

        if (racePar.InnateSpells?.Any() ?? false)
        {
            foreach (SpellTypes type in racePar.InnateSpells)
            {
                var thisType = type;
                //if (thisType > SpellTypes.Resurrection)
                //    thisType = thisType - SpellTypes.Resurrection + SpellTypes.AlraunePuff - 1;
                if (SpellList.SpellDict.TryGetValue(thisType, out Spell spell))
                {
                    UseableSpells.Add(spell);
                }
            }
        }

        var grantedSpell = State.RaceSettings.GetInnateSpell(Race);
        if (grantedSpell != SpellTypes.None)
        {
            if (SpellList.SpellDict.TryGetValue(grantedSpell, out Spell spell))
            {
                UseableSpells.Add(spell);
            }
        }

        foreach (Item item in Items)
        {
            if (item == null)
                continue;
            if (item is SpellBook book)
            {
                if (HasTrait(Traits.Feral))
                {
                    continue;
                }
                else if (SpellList.SpellDict.TryGetValue(book.ContainedSpell, out Spell spell))
                {
                    UseableSpells.Add(spell);
                    if (GetStatusEffect(StatusEffectType.Bloodrite) != null) UseableSpells.Remove(SpellList.Bloodrite);
                }

            }
        }
    }

    public void ApplyStatusEffect(StatusEffectType type, float strength, int duration, Unit applicator = null, StatusEffect expireEffect = null)
    {
        if (type == StatusEffectType.Poisoned && HasTrait(Traits.PoisonSpit))
            return;
        if (type == StatusEffectType.Morphed && strength != 123)
        {
            TriggerMorph(duration);
            return;
        }
        StatusEffects.Remove(GetStatusEffect(type));                    // if null, nothing happens, otherwise status is effectively overwritten
        StatusEffects.Add(new StatusEffect(type, strength, duration, applicator, expireEffect));
    }

    internal StatusEffect GetStatusEffect(StatusEffectType type)
    {
        if (type == StatusEffectType.WillingPrey && HasTrait(Traits.WillingRace))
            return new StatusEffect(StatusEffectType.WillingPrey, 0, 100);
        return StatusEffects.Where(s => s.Type == type).OrderByDescending(s => s.Strength).ThenByDescending(s => s.Duration).FirstOrDefault();
    }

    internal int GetNegativeStatusEffects()
    {
        int ret = 0;
        if (HasEffect(StatusEffectType.Clumsiness)) ret++;
        if (HasEffect(StatusEffectType.Diminished)) ret++;
        if (HasEffect(StatusEffectType.Glued)) ret++;
        if (HasEffect(StatusEffectType.Poisoned)) ret++;
        if (HasEffect(StatusEffectType.Shaken)) ret++;
        if (HasEffect(StatusEffectType.Webbed)) ret++;
        if (HasEffect(StatusEffectType.WillingPrey)) ret++;
        if (HasEffect(StatusEffectType.Charmed)) ret++;
        if (HasEffect(StatusEffectType.Hypnotized)) ret++;
        if (HasEffect(StatusEffectType.Sleeping)) ret++;
        if (HasEffect(StatusEffectType.Staggering)) ret++;
        if (HasEffect(StatusEffectType.Virus)) ret++;
        if (HasEffect(StatusEffectType.Weakness)) ret++;

        bool HasEffect(StatusEffectType type)
        {
            return GetStatusEffect(type) != null;
        }
        return ret;
    }

    public int GetApparentSide(Unit viewer = null)
    {
        if (viewer != null && TacticalUtilities.UnitCanSeeTrueSideOfTarget(viewer, this))
            return FixedSide;
        return hiddenFixedSide ? Side : FixedSide;
    }

    public void CreateRaceShape(Race race)
    {
        var shape = new Unit(Side, race, (int)Experience, true, Type, ImmuneToDefections);
        foreach (Traits trait in ShifterShapes[0].PermanentTraits)
        {
            shape.AddPermanentTrait(trait);
        }
        if (Races.GetRace(shape.Race).CanBeGender.Contains(GetGender()))
        {
            shape.SetGenderRandomizeName(race,GetGender());
        }
        shape.Name = Name;
        shape.InnateSpells.AddRange(InnateSpells);
        hiddenFixedSide = false;
        shape._fixedSide = ShifterShapes[0]._fixedSide;
        shape.SavedCopy = ShifterShapes[0].SavedCopy;
        shape.SavedVillage = ShifterShapes[0].SavedVillage;
        shape.BoundUnit = ShifterShapes[0].BoundUnit;
        ShifterShapes[0].ShifterShapes.Add(shape);
        shape.ShifterShapes = ShifterShapes[0].ShifterShapes;
    }

    internal void AddBladeDance()
    {
        var dance = GetStatusEffect(StatusEffectType.BladeDance);
        if (dance != null)
        {
            dance.Duration++;
            dance.Strength++;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.BladeDance, 1, 1);
        }

    }

    internal void RemoveBladeDance()
    {
        var dance = GetStatusEffect(StatusEffectType.BladeDance);
        if (dance != null)
        {
            if (!(HasTrait(Traits.Unflinching) && Health * .1f > dance.Strength)) 
            {
                dance.Duration--;
                dance.Strength--;
            }
            if (dance.Duration == 0)
                StatusEffects.Remove(dance);
        }

    }

    internal void AddTenacious()
    {
        var ten = GetStatusEffect(StatusEffectType.Tenacious);
        if (ten != null)
        {
            ten.Duration++;
            ten.Strength++;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.Tenacious, 1, 1);
        }

    }

    internal void RemoveTenacious()
    {
        var ten = GetStatusEffect(StatusEffectType.Tenacious);
        if (ten != null)
        {
            int reduction = 5 - (HasTrait(Traits.Unflinching) && Health * .1f > ten.Strength? 3 : 0);
            ten.Duration -= reduction;
            ten.Strength -= reduction;
            if (ten.Duration <= 0)
                StatusEffects.Remove(ten);
        }

    }

    internal void AddSpellForce()
    {
        var force = GetStatusEffect(StatusEffectType.SpellForce);
        if (force != null)
        {
            force.Duration++;
            force.Strength++;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.SpellForce, 1, 1);
        }

    }

    internal void AddFocus(int amount)
    {
        var foc = GetStatusEffect(StatusEffectType.Focus);
        if (foc != null)
        {
            foc.Duration += amount;
            foc.Strength += amount;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.Focus, amount, amount);
        }

    }

    internal void RemoveFocus()
    {
        var foc = GetStatusEffect(StatusEffectType.Focus);
        if (foc != null)
        {
            int reduction = 3 - (HasTrait(Traits.Unflinching) && Health * .1f > foc.Strength ? 3 : 0);
            foc.Duration -= reduction;
            foc.Strength -= reduction;
            if (foc.Duration <= 0)
                StatusEffects.Remove(foc);
        }

    }

    internal void AddRespawns(int amount)
    {
        var resp = GetStatusEffect(StatusEffectType.Respawns);
        if (resp != null)
        {
            resp.Duration += amount;
            resp.Strength += amount;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.Respawns, amount, amount);
        }

    }

    internal void RemoveRespawns()
    {
        var resp = GetStatusEffect(StatusEffectType.Respawns);
        if (resp != null)
        {
            int reduction = 1;
            resp.Duration -= reduction;
            resp.Strength -= reduction;
            if (resp.Duration <= 0)
                StatusEffects.Remove(resp);
        }

    }

    internal void AddStagger()
    {
        var stag = GetStatusEffect(StatusEffectType.Staggering);
        if (stag != null)
        {
            stag.Duration++;
            stag.Strength++;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.Staggering, 1, 1);
        }

    }

    internal void RemoveStagger()
    {
        var stag = GetStatusEffect(StatusEffectType.Staggering);
        if (stag != null)
        {
            stag.Duration--;
            stag.Strength--;
            if (stag.Duration == 0)
                StatusEffects.Remove(stag);
        }
    }

    internal void AddBolster(int ammount)
    {
        var bol = GetStatusEffect(StatusEffectType.Bolstered);
        if (bol != null)
        {
            bol.Duration += ammount;
            bol.Strength += ammount;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.Bolstered, ammount, ammount);
        }

    }

    internal void RemoveBolster()
    {
        var bol = GetStatusEffect(StatusEffectType.Bolstered);
        if (bol != null)
        {
            bol.Duration--;
            bol.Strength--;
            if (bol.Duration == 0)
                StatusEffects.Remove(bol);
        }
    }
    

    internal void AddWeakness()
    {
        var wkns = GetStatusEffect(StatusEffectType.Weakness);
        if (wkns != null)
        {
            wkns.Duration++;
            wkns.Strength++;
        }
        else
        {
            ApplyStatusEffect(StatusEffectType.Weakness, 1, 1);
        }

    }

    internal void RemoveWeakness()
    {
        var wkns = GetStatusEffect(StatusEffectType.Weakness);
        if (wkns != null)
        {
            wkns.Duration--;
            wkns.Strength--;
            if (wkns.Duration == 0)
                StatusEffects.Remove(wkns);
        }
    }

    internal void AddStackStatus(StatusEffectType type, int amnt)
    {
        var stat = GetStatusEffect(type);
        if (stat != null)
        {
            stat.Duration += amnt;
            stat.Strength += amnt;
        }
        else
        {
            ApplyStatusEffect(type, amnt, amnt);
        }
    }

    internal void RemoveStackStatus(StatusEffectType type, int amnt)
    {
        var stat = GetStatusEffect(type);
        if (stat != null)
        {
            stat.Duration -= amnt;
            stat.Strength -= amnt;
            if (stat.Duration == 0)
                StatusEffects.Remove(stat);
        }
    }
    internal void SetStackStatus(StatusEffectType type, int amnt)
    {
        var stat = GetStatusEffect(type);
        if (stat != null)
        {
            stat.Duration = amnt;
            stat.Strength = amnt;
            if (stat.Duration == 0)
                StatusEffects.Remove(stat);
        }
        else
        {
            ApplyStatusEffect(type, amnt, amnt);
        }
    }

    internal void TriggerMorph(int duration)
    {
        var wkns = GetStatusEffect(StatusEffectType.Morphed);
        if (wkns == null)
        {
            Unit clone = Clone();
            clone.Tags = new List<Traits>(Tags);
            clone.PermanentTraits = new List<Traits>(PermanentTraits);
            clone.RemovedTraits = new List<Traits>(RemovedTraits);
            ApplyStatusEffect(StatusEffectType.Morphed, 123, duration, clone);
            if (MorphUnit == null)
            {
                Race = State.RaceSettings.GetMorphRace(Race);
                RandomizeAppearance();
                ClearAllTraits();
                PermanentTraits.Clear();
                RemovedTraits.Clear();
                ReloadTraits();
            }
            else
            {
                Race = MorphUnit.Race;
                CopyAppearance(MorphUnit);
                ClearAllTraits();
                PermanentTraits.Clear();
                RemovedTraits.Clear();
                Tags = new List<Traits>(MorphUnit.Tags);
                PermanentTraits = new List<Traits>(MorphUnit.PermanentTraits);
                RemovedTraits = new List<Traits>(MorphUnit.RemovedTraits);
                InitializeTraits();
                SetMaxItems();
            }
        }
    }

    internal void RevertMorph(Unit unit)
    {
        MorphUnit = Clone();
        MorphUnit.Tags = new List<Traits>(Tags);
        MorphUnit.PermanentTraits = new List<Traits>(PermanentTraits);
        MorphUnit.RemovedTraits = new List<Traits>(RemovedTraits);

        Race = unit.Race;
        CopyAppearance(unit);
        ClearAllTraits();
        PermanentTraits.Clear();
        RemovedTraits.Clear();
        Name = unit.Name;

        Tags = new List<Traits>(unit.Tags);
        PermanentTraits = new List<Traits>(unit.PermanentTraits);
        RemovedTraits = new List<Traits>(unit.RemovedTraits);
        AllConditionalTraits = unit.AllConditionalTraits;

        InitializeTraits();
        SetMaxItems();
    }

    internal StatusEffect GetLongestStatusEffect(StatusEffectType type)
    {
        return StatusEffects.Where(s => s.Type == type).OrderByDescending(s => s.Duration).FirstOrDefault();
    }

    internal void TickStatusEffects()
    {
        var effect = GetStatusEffect(StatusEffectType.Mending);
        if (effect != null)
            HealPercentageCap(.2f, (int)(2 + effect.Strength / 4));
        effect = GetStatusEffect(StatusEffectType.Poisoned);
        if (effect != null)
            NonFatalDamage((int)effect.Strength, "poison");
        effect = GetStatusEffect(StatusEffectType.Virus);
        if (effect != null)
            NonFatalDamage((int)effect.Strength, "virus");
        foreach (var eff in StatusEffects.ToList())
        {
            if (eff.Type == StatusEffectType.Fractured || eff.Type == StatusEffectType.Respawns || eff.Type == StatusEffectType.BladeDance || eff.Type == StatusEffectType.Tenacious || eff.Type == StatusEffectType.Focus || eff.Type == StatusEffectType.Weakness || eff.Type == StatusEffectType.Bolstered)
                continue;
            var actor = TacticalUtilities.Units.Where(s => s.Unit == this).FirstOrDefault();
            var pred = actor.SelfPrey?.Predator;
            if (pred != null && eff.Type == StatusEffectType.Diminished)
            {
                if (pred.Unit.HasTrait(Traits.TightNethers) && (actor.SelfPrey.Location == PreyLocation.balls || actor.SelfPrey.Location == PreyLocation.womb))
                {
                    continue;
                }
            }

            if (eff.Type == StatusEffectType.Staggering || eff.Type == StatusEffectType.SpellForce)
                StatusEffects.Remove(eff);

            if (eff.Type == StatusEffectType.Sleeping && HasTrait(Traits.SleepItOff))
            {
                if (actor.PredatorComponent.UsageFraction >= State.Rand.NextDouble())
                {
                    continue;
                }
            }              

            eff.Duration -= 1;
            if (eff.Duration <= 0)
            {
                if (eff.Type == StatusEffectType.Morphed)
                {
                    RevertMorph(eff.Applicator);
                }
                if (eff.ExpireEffect != null)
                {
                    ApplyStatusEffect(eff.ExpireEffect.Type, eff.ExpireEffect.Strength, eff.ExpireEffect.Duration, eff.ExpireEffect.Applicator, eff.ExpireEffect.ExpireEffect);
                }

                StatusEffects.Remove(eff);
                if (eff.Type == StatusEffectType.Diminished)
                {
                    var still = GetStatusEffect(StatusEffectType.Diminished);
                    if (still == null)
                    {
                        if (actor != null)
                        {
                            if (pred != null)
                            {
                                State.GameManager.TacticalMode.Log.RegisterDiminishmentExpiration(pred.Unit, this, actor.SelfPrey.Location);
                            }

                        }

                    }
                }
                if (eff.Type == StatusEffectType.WillingPrey)
                {
                    var still = GetStatusEffect(StatusEffectType.WillingPrey);
                    if (still == null)
                    {
                        if (actor != null)
                        {
                            if (pred != null)
                            {
                                State.GameManager.TacticalMode.Log.RegisterCurseExpiration(pred.Unit, this, actor.SelfPrey.Location);
                            }

                        }

                    }
                }
                if (eff.Type == StatusEffectType.Warping)
                {
                    var still = GetStatusEffect(StatusEffectType.WillingPrey);
                    if (still == null)
                    {
                        if (actor != null)
                        {
                            if (pred != null)
                            {
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> warped out of the battle, and out of <b>{pred.Unit.Name}</b>.");
                                pred.PredatorComponent.FreePrey(actor.SelfPrey, true);
                            }
                            else
                            {
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> warped out of the battle.");
                            }
                            State.GameManager.TacticalMode.AttemptRetreat(actor,false,true);
                        }
                    }
                }
            }
        }
    }

    internal List<Traits> RandomizeOne(RandomizeList randomizeList)
    {
        if (randomizeList.level > Level)
            {
                return new List<Traits>() { (Traits)randomizeList.id };
            }
        var chance = randomizeList.chance;
        var rolls = UnityEngine.Random.Range(0, randomizeList.count);
        var traitsToAdd = new List<Traits>();
        List<Traits> gainable = randomizeList.RandomTraits.Where(rt => !Tags.Contains(rt) && !PermanentTraits.Contains(rt)).ToList();
        while (State.Rand.NextDouble() < chance)
        {
            if (gainable.Count() > 0)
            {
                var randomPick = gainable[State.Rand.Next(gainable.Count())];
                GivePrerequisiteTraits(randomPick);
                if (randomPick >= (Traits)1000)
                {
                    RandomizeList recursiveRl = State.RandomizeLists.Find(re => (Traits)re.id == randomPick);
                    if (recursiveRl != null)
                    {
                        traitsToAdd.AddRange(RandomizeOne(recursiveRl));
                    }
                } else
                    traitsToAdd.Add(randomPick);
                gainable.Remove(randomPick);
            }
            if (!(rolls > 0))
                chance -= 1;
            rolls--;
        }
        return traitsToAdd;
    }

    internal void AcquireShape(Unit unit, bool forceDirect = false)
    {
        //if (ShifterShapes.Any(shape => shape.Race == unit.Race) && !forceDirect) return;
        //if (HasTrait(Traits.Skinwalker) || forceDirect)
        //{
        //    Unit referenceUnit = ShifterShapes.Count > 0 ? ShifterShapes[0] : this;
        //    Unit shape = unit.Clone();
        //    shape.Side = Side;
        //    shape._fixedSide = referenceUnit._fixedSide;
        //    if (referenceUnit.HasTrait(Traits.Skinwalker))
        //        shape.AddPermanentTrait(Traits.Skinwalker);
        //    shape.hiddenFixedSide = referenceUnit.hiddenFixedSide;
        //    shape.SavedCopy = referenceUnit.SavedCopy;
        //    shape.SavedVillage = referenceUnit.SavedVillage; 
        //    shape.BoundUnit = referenceUnit.BoundUnit;
        //    referenceUnit.ShifterShapes.Add(shape);
        //    shape.ShifterShapes = ShifterShapes[0].ShifterShapes;
        //}
        //else if (HasTrait(Traits.Shapeshifter))
        //{
        //    CreateRaceShape(unit.Race);
        //}
    }

    internal List<Traits> GetPermanentTraits()
    {
        return PermanentTraits;
    }

    internal void UpdateShapeExpAndItems()
    {
        //if (!HasTrait(Traits.Skinwalker))
        //{
        //    ShifterShapes.ForEach(shape =>
        //    {
        //        shape.SetExp(experience);
        //        StrategicUtilities.SpendLevelUps(shape);
        //    });
        //}
        //ShifterShapes.ForEach(shape =>
        //{
        //    if (!shape.FixedGear)
        //    {
        //        shape.Items.ForEach((slot, index) =>
        //        {
        //           if( slot == null)
        //            {
        //                slot = GetItem(index);
        //            }
        //        });
        //    }
        //});
    }
    public void Shrink()
    {
        if (HasTrait(Traits.Titanic) && HasTrait(Traits.Colossal) && HasTrait(Traits.Huge) && HasTrait(Traits.Large))
            RemoveTrait(Traits.Large);
        else if (HasTrait(Traits.Titanic) && HasTrait(Traits.Colossal) && HasTrait(Traits.Huge))
            RemoveTrait(Traits.Huge);
        else if (HasTrait(Traits.Titanic) && HasTrait(Traits.Colossal))
        {
            RemoveTrait(Traits.Titanic);
            AddTrait(Traits.Huge);
        }
        else if (HasTrait(Traits.Colossal) && HasTrait(Traits.Huge))
        {
            RemoveTrait(Traits.Colossal);
            RemoveTrait(Traits.Huge);
            AddTrait(Traits.Titanic);
        }
        else if (HasTrait(Traits.Titanic))
        {
            AddTrait(Traits.Colossal);
            RemoveTrait(Traits.Titanic);
        }
        else if (HasTrait(Traits.Colossal))
        {
            RemoveTrait(Traits.Colossal);
            AddTrait(Traits.Huge);
        }
        else if (HasTrait(Traits.Huge))
        {
            RemoveTrait(Traits.Huge);
            AddTrait(Traits.Large);
        }
        else if (HasTrait(Traits.Large))
            RemoveTrait(Traits.Large);
        else if (HasTrait(Traits.Small))
        {
            RemoveTrait(Traits.Small);
            AddTrait(Traits.Tiny);
        }
        else
            AddTrait(Traits.Small);
    }

    public Race DetermineSpawnRace()
    {
        if (SpawnRace != Race)
        {
            return SpawnRace;
        }

        else
            return State.RaceSettings.GetSpawnRace(Race);
    }

    public Race DetermineConversionRace()
    {
        if (ConversionRace != Race)
            return ConversionRace;
        else
            return State.RaceSettings.GetConversionRace(Race);
    }

    public void TriggerEeveelution()
    {
        if (Race == attunedEeveeRace)
        {
            return;
        }
        Race = attunedEeveeRace;
        RandomizeAppearance();
        ClearAllTraits();
        ReloadTraits();
        if (HasTrait(Traits.Eeveeolutionist))
        {
            RemoveTrait(Traits.Eeveeolutionist);
        }
    }
}
