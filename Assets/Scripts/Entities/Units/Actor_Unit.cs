
using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Actor_Unit
{
    enum DisplayMode
    {
        None,
        Attacking,
        OralVore,
        CockVore,
        TailVore,
        BreastVore,
        AnalVore,
        Unbirth,
        FrogPouncing,
        VoreSuccess,
        VoreFail,
        Digesting,
        Absorbing,
        Birthing,
        Suckling,
        Rubbing,
        Suckled,
        Rubbed,
        Injured,
        IdleAnimation,

    }

    int animationStep = 4;
    float animationUpdateTime;
    DisplayMode mode;
    [OdinSerialize]
    public List<KeyValuePair<int, float>> modeQueue;

    private DisplayMode Mode
    {
        get => mode;
        set
        {
            if (State.GameManager.TacticalMode.turboMode && value >= DisplayMode.Attacking && value <= DisplayMode.FrogPouncing)
                HasAttackedThisCombat = true;
            mode = value;
        }
    }


    [OdinSerialize]
    public Unit Unit { get; private set; }
    [OdinSerialize]
    public Vec2i Position { get; private set; }
    [OdinSerialize]
    public int Movement;
    [OdinSerialize]
    public bool Visible;
    [OdinSerialize]
    public bool Targetable;
    public bool ReceivedRub => (RubCount >= Config.BellyRubsPerTurn && Config.BellyRubsPerTurn >= 0);
    [OdinSerialize]
    public int RubCount;

    public bool BeingRubbed;


    [OdinSerialize]
    public bool Surrendered;

    public bool DefectedThisTurn;

    [OdinSerialize]
    public bool WasJustFreed;

    public bool SurrenderedThisTurn = false; //Deliberately not saved

    internal bool Intimidated;

    [OdinSerialize]
    public Weapon BestMelee;
    [OdinSerialize]
    public Weapon BestRanged;

    [OdinSerialize]
    public bool Fled;

    // DayNight addition
    [OdinSerialize]
    public bool Hidden;
    [OdinSerialize]
    public bool InSight = true;
	
    [OdinSerialize]
    internal Prey SelfPrey;

    [OdinSerialize]
    public bool Slimed;
    [OdinSerialize]
    public bool Paralyzed;

    [OdinSerialize]
    public int Corruption;

    [OdinSerialize]
    public int Possessed;

    [OdinSerialize]
    public bool Infected;

    [OdinSerialize]
    public Race InfectedRace;

    [OdinSerialize]
    public int InfectedSide;

    [OdinSerialize]
    public bool KilledByDigestion;

    [OdinSerialize]
    public int TimesParalyzed;

    [OdinSerialize]
    public bool GoneBerserk;

    [OdinSerialize]
    internal int AIAvoidEat;

    [OdinSerialize]
    internal int TurnUsedShun = -5;

    [OdinSerialize]
    internal int TurnsSinceLastDamage = 9999;

    [OdinSerialize]
    internal int TurnsSinceLastParalysis = 9999;

    [OdinSerialize]
    internal float RampStacks = 0;

    [OdinSerialize]
    internal int spriteLayerOffset = 0;

    [OdinSerialize]
    public bool HasAttackedThisCombat = false;

    [OdinSerialize]
    public bool allowedToDefect = false;



    private UnitSprite _unitSprite;
    internal UnitSprite UnitSprite
    {
        get
        {
            if (_unitSprite == null)
                GenerateSpritePrefab(State.GameManager.TacticalMode.ActorFolder);
            return _unitSprite;
        }
        set => _unitSprite = value;
    }

    internal bool SquishedBreasts = false;

    [OdinSerialize]
    public PredatorComponent PredatorComponent;

    Race animationControllerRace;

    [OdinSerialize]
    private AnimationController animationController = new AnimationController();

    public AnimationController AnimationController
    {
        get
        {
            if (animationController == null || animationControllerRace != Unit.Race)
            {
                animationController = new AnimationController();
                animationControllerRace = Unit.Race;
            }

            return animationController;
        }
        set => animationController = value;
    }

    public void ClearMovement() => Movement = 0;
    public void DrainExp(int damage) => Unit.GiveExp(-1 * damage);
    public void SetPos(Vec2i p) => Position = p;

    private void RestoreMP()
    {
        if (WasJustFreed)
        {
            Movement = Mathf.Min(Mathf.Max(2, (int)(.2f * CurrentMaxMovement())), 5);
            WasJustFreed = false;
        }
        if (Paralyzed)
        {
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{Unit.Name}</b> was paralyzed and cannot move this turn.");
            Movement = 0;
            TimesParalyzed++;
            TurnsSinceLastParalysis = 0;
            Paralyzed = false;
            Slimed = false;
        }
        else if ((Unit.GetStatusEffect(StatusEffectType.Petrify) != null) || (Unit.GetStatusEffect(StatusEffectType.Frozen) != null))
        {
            Movement = 0;
            Slimed = false;
        }
        else if (Unit.GetStatusEffect(StatusEffectType.Glued) != null)
        {
            Movement = 2;
            Slimed = false;
        }
        else if ((Unit.GetStatusEffect(StatusEffectType.Webbed) != null) || (Unit.GetStatusEffect(StatusEffectType.Snared) != null))
        {
            Movement = 1;
            Slimed = false;
        }
        else if (Unit.GetStatusEffect(StatusEffectType.Staggering) != null)
        {
            Movement = CurrentMaxMovement() / 2;
            Slimed = false;
        }
        else if (Unit.GetStatusEffect(StatusEffectType.Sleeping) != null)
        {
            Movement = 0;
            Slimed = false;
        }
        else if (Slimed)
        {
            Movement = CurrentMaxMovement() / 2;
            Slimed = false;
        }
        else
            Movement = CurrentMaxMovement();
    }



    public int MaxMovement()
    {
        int bonus = 0;
        if (Unit.HasTrait(Traits.Charge) && State.GameManager.TacticalMode.currentTurn <= 2)
            bonus += 4;
        bonus += Unit.TraitBoosts.SpeedBonus;
        if (State.World?.ItemRepository != null && Unit.Items.Contains(State.World.ItemRepository.GetItem(ItemType.Shoes)))
            bonus += 1;
        int total = Mathf.Max(bonus + 3 + ((int)Mathf.Pow(Unit.GetStat(Stat.Agility) / 4, .8f)), 1);
        var speed = Unit.GetStatusEffect(StatusEffectType.Fast);
        if (speed != null)
        {
            total = (int)(total * (1 + speed.Strength));
        }
        total = (int)(total * Unit.TraitBoosts.SpeedMultiplier);
        if (total < Unit.TraitBoosts.MinSpeed)
            total = Unit.TraitBoosts.MinSpeed;
        return total;
    }

    public int CurrentMaxMovement()
    {
        if (Unit.HasTrait(Traits.Respawner) && (State.GameManager.TacticalMode.currentTurn == 1) && State.GameManager.TacticalMode.attackersTurnCheck == true)
            Unit.ApplyStatusEffect(StatusEffectType.Respawns, 1, 1);
        if (Unit.HasTrait(Traits.RespawnerIII) && (State.GameManager.TacticalMode.currentTurn == 1) && State.GameManager.TacticalMode.attackersTurnCheck == true)
            Unit.ApplyStatusEffect(StatusEffectType.Respawns, 3, 3);
        int sizePenalty = (int)(PredatorComponent?.Fullness ?? 0);
        sizePenalty = (int)(sizePenalty * Unit.TraitBoosts.SpeedLossFromWeightMultiplier);
        int bonus = 0;
        if (State.World?.ItemRepository != null && Unit.Items.Contains(State.World.ItemRepository.GetItem(ItemType.Shoes)))
            bonus += 1;
        bonus += Unit.TraitBoosts.SpeedBonus;
        if (Unit.HasTrait(Traits.Charge) && State.GameManager.TacticalMode.currentTurn <= 2)
            bonus += 4;
        int total = Mathf.Max(bonus + 3 + ((int)Mathf.Pow(Unit.GetStat(Stat.Agility) / 4, .8f)) - sizePenalty, 1);
        var speed = Unit.GetStatusEffect(StatusEffectType.Fast);
        if (speed != null)
        {
            total = (int)(total * (1 + speed.Strength));
        }
        total = (int)(total * Unit.TraitBoosts.SpeedMultiplier);
        if (Unit.HasTrait(Traits.AllOutFirstStrike) && HasAttackedThisCombat)
            total /= 2;
        if (total < Unit.TraitBoosts.MinSpeed)
            total = Unit.TraitBoosts.MinSpeed;
        return total;
    }

    /// <summary>
    /// This one is a dummy for some cases
    /// </summary>
    /// <param name="unit"></param>
    public Actor_Unit(Unit unit)
    {
        unit.SetBreastSize(-1); //Resets to default
        Mode = DisplayMode.None;
        modeQueue = new List<KeyValuePair<int, float>>();
        animationUpdateTime = 0;
        Position = new Vec2i(0, 0);
        Unit = unit;
        Visible = true;
        Targetable = true;
    }

    public Actor_Unit(Unit unit, Actor_Unit reciepient)
    {
        PredatorComponent = new PredatorComponent(this, unit);
        unit.SetBreastSize(-1); //Resets to default
        Mode = DisplayMode.None;
        modeQueue = new List<KeyValuePair<int, float>>();
        animationUpdateTime = 0;
        Position = reciepient.Position;
        Unit = unit;
        Visible = false;
        Targetable = false;
        RestoreMP();
        ReloadSpellTraits();
    }

    public Actor_Unit(Vec2i p, Unit unit)
    {
        PredatorComponent = new PredatorComponent(this, unit);
        unit.SetBreastSize(-1); //Resets to default
        Mode = DisplayMode.None;
        modeQueue = new List<KeyValuePair<int, float>>();
        animationUpdateTime = 0;
        Position = p;
        Unit = unit;
        Visible = true;
        Targetable = true;
        RestoreMP();
        ReloadSpellTraits();
    }

    public void ReloadSpellTraits()
    {
        Unit.SingleUseSpells = new List<SpellTypes>();
        Unit.MultiUseSpells = new List<SpellTypes>();
        if (Unit.HasTrait(Traits.MadScience) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(((SpellBook)State.World.ItemRepository.GetRandomBook(1, 4)).ContainedSpell);
            Unit.UpdateSpells();
        }
        if (Unit.HasTrait(Traits.PollenProjector) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.AlraunePuff.SpellType);
            Unit.UpdateSpells();
        }
        if (Unit.HasTrait(Traits.Webber) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.Web.SpellType);
            Unit.UpdateSpells();
        }
        if (Unit.HasTrait(Traits.GlueBomb) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.GlueBomb.SpellType);
            Unit.UpdateSpells();
        }
        if (Unit.HasTrait(Traits.PoisonSpit) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.ViperPoisonStatus.SpellType);
            Unit.UpdateSpells();
        }
        if (Unit.HasTrait(Traits.Petrifier) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.Petrify.SpellType);
            Unit.UpdateSpells();
        }
        if (Unit.HasTrait(Traits.Charmer) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.Charm.SpellType);
            Unit.UpdateSpells();
        }
        if (Unit.HasTrait(Traits.HypnoticGas) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.HypnoGas.SpellType);
            Unit.UpdateSpells();
        }
        if (Unit.HasTrait(Traits.Reanimator) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.Reanimate.SpellType);
            Unit.UpdateSpells();
        }
        if (Unit.HasTrait(Traits.Binder) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.SingleUseSpells.Add(SpellList.Bind.SpellType);
            Unit.UpdateSpells();
        }
        // Multi-use section
        if (Unit.HasTrait(Traits.ForceFeeder) && State.World?.ItemRepository != null) //protection for the create strat screen
        {
            Unit.MultiUseSpells.Add(SpellList.ForceFeed.SpellType);
            Unit.UpdateSpells();
        }
        if (State.World?.ItemRepository != null) //protection for the create strat screen
        {
            foreach (Traits trait in Unit.GetTraits.Where(t => (TraitList.GetTrait(t) != null) && TraitList.GetTrait(t) is IProvidesSingleSpell))
            {
                IProvidesSingleSpell callback = (IProvidesSingleSpell)TraitList.GetTrait(trait);
                foreach(SpellTypes spell in callback.GetSingleSpells(Unit))
                    Unit.SingleUseSpells.Add(spell);
            }
            foreach (Traits trait in Unit.GetTraits.Where(t => (TraitList.GetTrait(t) != null) && TraitList.GetTrait(t) is IProvidesMultiSpell))
            {
                IProvidesMultiSpell callback = (IProvidesMultiSpell)TraitList.GetTrait(trait);
                foreach (SpellTypes spell in callback.GetMultiSpells(Unit))
                    Unit.MultiUseSpells.Add(spell);
            }
            Unit.UpdateSpells();
        }
    }

    public void GenerateSpritePrefab(Transform folder)
    {
        UnitSprite = Object.Instantiate(State.GameManager.UnitBase, new Vector3(Position.x, Position.y), new Quaternion(), folder).GetComponent<UnitSprite>();
        UnitSprite.UpdateHealthBar(this);
    }

    public void UpdateBestWeapons()
    {
        if (Unit.HasTrait(Traits.Feral))
        {
            BestMelee = State.World.ItemRepository.Claws;
            BestRanged = null;
            return;
        }


        BestMelee = Unit.GetBestMelee();
        BestRanged = Unit.GetBestRanged();
        Unit.UpdateSpells();
    }

    public void SetPredMode(PreyLocation preyType)
    {
        switch (preyType)
        {
            case PreyLocation.breasts:
                Mode = DisplayMode.BreastVore;
                break;
            case PreyLocation.balls:
                Mode = DisplayMode.CockVore;
                break;
            case PreyLocation.stomach:
                Mode = DisplayMode.OralVore;
                break;
            case PreyLocation.womb:
                Mode = DisplayMode.Unbirth;
                break;
            case PreyLocation.tail:
                Mode = DisplayMode.TailVore;
                break;
            case PreyLocation.anal:
                Mode = DisplayMode.AnalVore;
                break;
        }
        animationUpdateTime = 1.5F;
    }

    public void SetBurpMode()
    {
        Mode = DisplayMode.OralVore;
        animationUpdateTime = 1;
    }

    /// <summary>
    /// Used for idle Animations - ignored if the unit is already animating something
    /// </summary>
    /// <param name="frameNum">The Number of Frames of animation, starts at this and counts down to 0</param>
    /// <param name="time">The seconds per step</param>
    public void SetAnimationMode(int frameNum, float time)
    {
        if (Mode == DisplayMode.None)
        {
            Mode = DisplayMode.IdleAnimation;
            animationStep = frameNum;
            animationUpdateTime = time;
        }

    }

    public void SetVoreSuccessMode()
    {
        DisplayMode displayMode = DisplayMode.VoreSuccess;
        float time = 2f;
        modeQueue.Add(new KeyValuePair<int, float>(((int)displayMode), time));
    }

    public void SetVoreFailMode()
    {
        DisplayMode displayMode = DisplayMode.VoreFail;
        float time = 1f;
        modeQueue.Add(new KeyValuePair<int, float>((int)displayMode, time));
    }

    public void SetAbsorbtionMode()
    {
        if (Config.BurpOnDigest || Config.BurpFraction < .1f)
        {
            Mode = DisplayMode.Absorbing;
            animationUpdateTime = 2f;
        }
    }

    public void SetDigestionMode()
    {
        Mode = DisplayMode.Digesting;
        animationUpdateTime = 1.0f;
    }

    public void SetBirthMode()
    {
        Mode = DisplayMode.Birthing;
        animationUpdateTime = 1.0f;
    }
    public void SetSuckleMode()
    {
        Mode = DisplayMode.Suckling;
        animationUpdateTime = 1.0f;
    }

    public void SetSuckledMode()
    {
        Mode = DisplayMode.Suckled;
        animationUpdateTime = 1.0f;
    }

    public void SetRubMode()
    {
        Mode = DisplayMode.Rubbing;
        animationUpdateTime = 1.0f;
    }

    public void SetRubbedMode()
    {
        Mode = DisplayMode.Rubbed;
        animationUpdateTime = 1.0f;
    }

    public int CheckAnimationFrame()
    {
        if (Mode == DisplayMode.IdleAnimation)
            return animationStep;
        return 0;
    }

    internal bool DamagedColors => Mode == DisplayMode.Injured;

    /// <summary>
    /// Splits the chain of sprites evenly based on the ball size (Oral + Unbirth)
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetBallSize(int highestSprite, float multiplier = 1)
    {
        float v = (PredatorComponent?.BallsFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite)
            v = highestSprite;
        return (int)v;
    }

    /// <summary>
    /// Splits the chain of sprites evenly based on the tail size (Oral + Unbirth)
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetTailSize(int highestSprite, float multiplier = 1)
    {
        float v = (PredatorComponent?.TailFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite)
            v = highestSprite;
        return (int)v;
    }


    /// <summary>
    /// Splits the chain of sprites evenly based on the stomach size (Oral + Unbirth)
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetStomachSize(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.VisibleFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite)
            v = highestSprite;
        return (int)v;
    }

    /// <summary>
    /// Splits the chain of sprites evenly based on the stomach size (excludes womb)
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetExclusiveStomachSize(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.ExclusiveStomachFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite)
            v = highestSprite;
        return (int)v;
    }

    /// <summary>
    /// Splits the chain of sprites evenly based on the stomach size (excludes womb)
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetWombSize(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.WombFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite)
            v = highestSprite;
        return (int)v;
    }

    /// <summary>
    /// Splits the chain of sprites unevenly based on the stomach size (Oral + Unbirth)
    /// This one causes the low sprites to be passed through quicker, and the high sprites to be passed through slower.
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetRootedStomachSize(int highestSprite = 15, float multiplier = 1)
    {
        highestSprite = highestSprite * highestSprite;
        float v = (PredatorComponent?.VisibleFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite)
            v = highestSprite;
        v = Mathf.Sqrt(v);
        return (int)v;
    }

    /// <summary>
    /// Splits the chain of sprites evenly based on the left breast size
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetLeftBreastSize(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.LeftBreastFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite)
            v = highestSprite;
        return (int)v;
    }

    /// <summary>
    /// Splits the chain of sprites evenly based on the right breast size
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetRightBreastSize(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.RightBreastFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite)
            v = highestSprite;
        return (int)v;
    }

    /// <summary>
    /// Splits the chain of sprites evenly based on the second stomach's size.
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetStomach2Size(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.Stomach2ndFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite)
            v = highestSprite;
        return (int)v;
    }

    /// <summary>
    /// Splits the chain of sprites evenly based on the combined size of bellies 1 & 2. For units with DualStomach trait.
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4 </param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetCombinedStomachSize(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.CombinedStomachFullness ?? 0) * 4 * ((highestSprite + 1) / 16f) * multiplier;
        if (v > highestSprite)
            v = highestSprite;
        return (int)v;
    }

    /// <summary>
    /// An alternate version of GetStomachSize used for combining all vore types into a single value
    /// </summary>
    /// <param name="highestSprite">the end sprite, 4 would make it 0,1,2,3,4</param>
    /// <param name="multiplier">Controls the speed it moves through the sprites, 2 would be double, 0.5f would be half</param>
    /// <returns></returns>
    public int GetUniversalSize(int highestSprite = 15, float multiplier = 1)
    {
        float v = (PredatorComponent?.Fullness ?? 0) * 4 * multiplier;
        if (v > highestSprite)
            v = highestSprite;
        return (int)v;
    }

    public bool HasBelly => ((Config.LamiaUseTailAsSecondBelly || Unit.HasTrait(Traits.DualStomach) == false) && (PredatorComponent?.VisibleFullness ?? 0) > 0) || (!Config.LamiaUseTailAsSecondBelly && (PredatorComponent?.CombinedStomachFullness ?? 0) > 0);

    public bool HasPreyInBreasts => (PredatorComponent?.BreastFullness > 0 || PredatorComponent?.LeftBreastFullness > 0 || PredatorComponent?.RightBreastFullness > 0);
    public bool HasBodyWeight => Unit.Race != Race.Lizards && Unit.Race != Race.Slimes && Unit.Race != Race.Scylla && Unit.Race != Race.Harpies && Unit.Race != Race.Imps;

    public int GetBodyWeight() => (Config.WeightGain || Unit.BodySizeManuallyChanged) ? Unit.BodySize : Mathf.Min(Config.DefaultStartingWeight, Races.GetRace(Unit).BodySizes);
    public int GetWeaponSprite()
    {
        if (BestRanged != null)
        {
            if (Mode != DisplayMode.Attacking)
                return BestRanged.Graphic;
            else
                return BestRanged.Graphic + 1;
        }
        else if (BestMelee != null && BestMelee != State.World.ItemRepository.Claws)
        {
            if (Mode != DisplayMode.Attacking)
                return BestMelee.Graphic;
            else
                return BestMelee.Graphic + 1;
        }
        return 0;
    }

    public int GetSimpleBodySprite()
    {
        if (Mode == DisplayMode.Attacking)
            return 1;
        if (Mode == DisplayMode.OralVore || Mode == DisplayMode.BreastVore || Mode == DisplayMode.CockVore || Mode == DisplayMode.Unbirth || Mode == DisplayMode.AnalVore)
            return 2;
        return 0;
    }


    public bool IsAttacking => Mode == DisplayMode.Attacking;

    /// <summary>
    /// This one Covers all forms of consuming
    /// </summary>
    public bool IsEating => IsOralVoring || IsCockVoring || IsBreastVoring || IsUnbirthing || IsTailVoring || IsAnalVoring;
    public bool IsOralVoring => Mode == DisplayMode.OralVore;
    public bool IsOralVoringHalfOver => Mode == DisplayMode.OralVore && animationUpdateTime < .75f;

    public bool IsCockVoring => Mode == DisplayMode.CockVore;
    public bool IsBreastVoring => Mode == DisplayMode.BreastVore;
    public bool IsUnbirthing => Mode == DisplayMode.Unbirth;
    public bool IsTailVoring => Mode == DisplayMode.TailVore;
    public bool IsAnalVoring => Mode == DisplayMode.AnalVore;
    public bool IsPouncingFrog => Mode == DisplayMode.FrogPouncing;
    public bool HasJustVored => Mode == DisplayMode.VoreSuccess;
    public bool HasJustFailedToVore => Mode == DisplayMode.VoreFail;
    public bool IsDigesting => Mode == DisplayMode.Digesting;
    public bool IsAbsorbing => Mode == DisplayMode.Absorbing;
    public bool IsBirthing => Mode == DisplayMode.Birthing;
    public bool IsSuckling => Mode == DisplayMode.Suckling;
    public bool IsBeingSuckled => Mode == DisplayMode.Suckled;
    public bool IsRubbing => Mode == DisplayMode.Rubbing;
    public bool IsBeingRubbed => Mode == DisplayMode.Rubbed;
    [OdinSerialize]
    public List<int> sidesAttackedThisBattle { get; set; }

    public float GetSpecialChance(SpecialAction action)
    {
        if (action == SpecialAction.CockVore)
            return .8f;
        else
            return 1f;
    }

    public float GetPureStatClashChance(int attackStat, int defenseStat, float shift) // generic AF
    {
        return (float)attackStat / (attackStat + (defenseStat * (1 + shift)));
    }

    internal float GetMagicChance(Actor_Unit attacker, Spell currentSpell, float modifier = 0, Stat stat = Stat.Mind)
    {
        if (TacticalUtilities.SneakAttackCheck(attacker.Unit, Unit)) // sneakAttack
        {
            modifier -= 0.3f;
        }
        int attackStat = attacker.Unit.GetStat(stat);
        int defenseStat = Unit.GetStat(Stat.Will);
        if (currentSpell?.Resistable ?? false) //Skips if no spell is specified since that only involves AI calcs
        {
            defenseStat = (int)(defenseStat * currentSpell.ResistanceMult);
        }

        float shift = Unit.TraitBoosts.Incoming.MagicShift + attacker.Unit.TraitBoosts.Outgoing.MagicShift + modifier;
        return (float)attackStat / (attackStat + (defenseStat * (1 + shift)));
    }

    //public float GetMeleeChance(Actor_Unit attacker, bool includeSecondaries = false)
    //{
    //    float attackerScore = 2 * attacker.Unit.GetStat(Stat.Strength) + attacker.Unit.GetStat(Stat.Dexterity);
    //    float defenderScore = 2 * Unit.GetStat(Stat.Agility) * (15 / (BodySize() + 5) / (1 + (PredatorComponent?.Fullness ?? 0) * Unit.TraitBoosts.DodgeLossFromWeightMultiplier * 5 / Unit.GetStat(Stat.Strength)));

    //    if (Unit.GetStatusEffect(StatusEffectType.Clumsiness) != null)
    //    {
    //        defenderScore *= Unit.GetStatusEffect(StatusEffectType.Clumsiness).Strength;
    //    }

    //    if (attacker.Intimidated)
    //        attackerScore *= .8f;

    //    foreach (IPhysicalDefenseOdds trait in Unit.PhysicalDefenseOdds)
    //    {
    //        trait.PhysicalDefense(this, ref defenderScore);
    //    }

    //    float odds = attackerScore / (attackerScore + defenderScore);

    //    odds *= Unit.TraitBoosts.FlatHitReduction;

    //    if (odds < 20)
    //        odds = 20;

    //    if (Config.BoostedAccuracy)
    //        odds = 100 - ((100 - odds) * .5f);

    //    if (includeSecondaries)
    //    {
    //        if (Unit.HasTrait(Traits.Dazzle))
    //        {
    //            odds *= 1 - WillCheckOdds(attacker, this);
    //        }
    //    }
    //    return odds / 100;
    //}


    public float GetAttackChance(Actor_Unit attacker, bool ranged, bool includeSecondaries = false, float mod = 0)
    {
        int attack;
        if (ranged)
        {
            attack = attacker.Unit.GetStat(Stat.Dexterity);
            attacker.UpdateBestWeapons();
        }
        else
        {
            attack = attacker.Unit.GetStat(Stat.Strength);
            attacker.UpdateBestWeapons();
        }

        int range = attacker.Position.GetNumberOfMovesDistance(Position);
        if (Surrendered || Unit.GetStatusEffect(StatusEffectType.Petrify) != null || Unit.GetStatusEffect(StatusEffectType.Sleeping) != null || Unit.GetStatusEffect(StatusEffectType.Frozen) != null)
            return 1f;
        const int maximumBoost = 75;
        const int minimumOdds = 25;
        float defenderBonusShift = 0;
        int defense = Unit.GetStat(Stat.Agility);

        if (Unit.GetStatusEffect(StatusEffectType.Clumsiness) != null)
        {
            defenderBonusShift -= Unit.GetStatusEffect(StatusEffectType.Clumsiness).Strength;
        }

        attack += 15;
        defense += 15;

        if (attack <= 0)
            attack = 1;
        if (defense <= 0)
            defense = 1;

        if (range > 2)
            defenderBonusShift += .05f * (range - 2);

        if (ranged)
            defenderBonusShift += Unit.TraitBoosts.Incoming.RangedShift + attacker.Unit.TraitBoosts.Outgoing.RangedShift + mod;
        else
            defenderBonusShift += Unit.TraitBoosts.Incoming.MeleeShift + attacker.Unit.TraitBoosts.Outgoing.MeleeShift + mod;

        if (Unit.HasTrait(Traits.AllOutFirstStrike))
        {
            if (HasAttackedThisCombat)
                defenderBonusShift -= 2;
            else
                defenderBonusShift += 4;
        }
        if (TacticalUtilities.SneakAttackCheck(attacker.Unit, Unit)) // sneakAttack
        {
            defenderBonusShift += 2;
        }

        defenderBonusShift -= Unit.TraitBoosts.DodgeLossFromWeightMultiplier * 0.1f * PredatorComponent?.Fullness ?? 0;


        if (BestRanged == null && Movement > 0 && ranged)
            defenderBonusShift += .2f;

        foreach (IPhysicalDefenseOdds trait in Unit.PhysicalDefenseOdds)
        {
            trait.PhysicalDefense(this, ref defenderBonusShift);
        }

        if (attacker.Intimidated)
            defenderBonusShift += .2f;

        if (ranged)
        {
            if (attacker.BestRanged?.AccuracyModifier > 1)
                attack = (int)(attack * attacker.BestRanged.AccuracyModifier);
            if (attacker.BestRanged?.Magic == true)
                attack = attack + attacker.Unit.GetStat(Stat.Mind);
        }
        else
        {
            if (attacker.BestMelee?.AccuracyModifier > 1)
                attack = (int)(attack * attacker.BestMelee.AccuracyModifier);
            if (attacker.BestMelee?.Magic == true)
                attack = attack + attacker.Unit.GetStat(Stat.Mind);
        }

        float ratio = (float)attack / defense;
        float adjustment;
        if (ratio > 1)
            adjustment = 1 - ratio;
        else
            adjustment = (1 / ratio) - 1;

        float oddsReductionFactor = (3 * adjustment) + defenderBonusShift; //Lower factor is increased odds
        float odds = minimumOdds + (maximumBoost / (1 + Mathf.Pow(2, oddsReductionFactor)));

        odds *= Unit.TraitBoosts.FlatHitReduction;

        if (Config.BoostedAccuracy)
            odds = 100 - ((100 - odds) * .5f);

        if (includeSecondaries)
        {
            if (Unit.HasTrait(Traits.Dazzle))
            {
                odds *= 1 - WillCheckOdds(attacker, this);
            }
        }

        return odds / 100;
    }

    public int WeaponDamageAgainstTarget(Actor_Unit target, bool ranged, float multiplier = 1, bool forceBite = false)
    {
        int damage;
        if (ranged)
        {
            float damageScalar = Unit.TraitBoosts.Outgoing.RangedDamage * target.Unit.TraitBoosts.Incoming.RangedDamage;
			damageScalar *= multiplier;
            if (Unit.HasTrait(Traits.AllOutFirstStrike) && HasAttackedThisCombat == false)
                damageScalar *= 5;
            if ((target.Unit.GetStatusEffect(StatusEffectType.Petrify) != null) || (target.Unit.GetStatusEffect(StatusEffectType.Frozen) != null))
                damageScalar /= 2;
            if (Unit.HasTrait(Traits.Competitive) && Unit.Race == target.Unit.Race)
            {
                damageScalar *= 1.15f;
            }
            if (Unit.GetStatusEffect(StatusEffectType.Valor) != null)
            {
                damageScalar *= 1.25f;
            }
            if (Unit.HasTrait(Traits.WeaponChanneler) && Unit.Mana >= 6)
            {
                damageScalar *= 1.2f;
            }
            if (Unit.GetStatusEffect(StatusEffectType.Bloodrite) != null)
            {
                damageScalar *= 1.1f;
            }
            if (target.Unit.GetStatusEffect(StatusEffectType.Shielded) != null)
            {
                damageScalar *= 1 - target.Unit.GetStatusEffect(StatusEffectType.Shielded).Strength;
            }
            if (target.Unit.GetStatusEffect(StatusEffectType.DivineShield) != null)
            {
                damageScalar *= 1 - target.Unit.GetStatusEffect(StatusEffectType.DivineShield).Strength;
            }
            if (Unit.HasTrait(Traits.SenseWeakness))
            {
                damageScalar *= 1.4f - (target.Unit.HealthPct * .4f) + (0.1f * target.Unit.GetNegativeStatusEffects());
            }
            int statBoost = Unit.GetStat(Stat.Dexterity) + (Unit.HasTrait(Traits.SpellBlade) ? Unit.GetStat(Stat.Mind) / 2 : 0);
            damage = (int)(damageScalar * (BestRanged?.Damage ?? 2) * (60 + statBoost) / 60);
            if (target.Unit.HasTrait(Traits.Resilient))
                damage--;
            if (target.Unit.GetStatusEffect(StatusEffectType.Staggering) != null)
            {
                damage += (int)(damage * 0.2f);
            }

        }
        else
        {

            float damageScalar = Unit.TraitBoosts.Outgoing.MeleeDamage * target.Unit.TraitBoosts.Incoming.MeleeDamage;

            if (Unit.HasTrait(Traits.AllOutFirstStrike) && HasAttackedThisCombat == false)
                damageScalar *= 5;
            damageScalar *= multiplier;

            if ((target.Unit.GetStatusEffect(StatusEffectType.Petrify) != null) || (target.Unit.GetStatusEffect(StatusEffectType.Frozen) != null))
                damageScalar /= 2;

            if (Unit.HasTrait(Traits.Competitive) && Unit.Race == target.Unit.Race)
            {
                damageScalar *= 1.15f;
            }
            if (Unit.GetStatusEffect(StatusEffectType.Valor) != null)
            {
                damageScalar *= 1.25f;
            }
            if (Unit.HasTrait(Traits.WeaponChanneler) && Unit.Mana >= 6)
            {
                damageScalar *= 1.2f;
            }
            if (Unit.GetStatusEffect(StatusEffectType.Bloodrite) != null)
            {
                damageScalar *= 2.5f;
            }
            if (target.Unit.GetStatusEffect(StatusEffectType.Shielded) != null)
            {
                damageScalar *= 1 - target.Unit.GetStatusEffect(StatusEffectType.Shielded).Strength;
            }
            if (target.Unit.GetStatusEffect(StatusEffectType.DivineShield) != null)
            {
                damageScalar *= 1 - target.Unit.GetStatusEffect(StatusEffectType.DivineShield).Strength;
            }
            if (Unit.HasTrait(Traits.ForcefulBlow))
                TacticalUtilities.CheckKnockBack(this, target, ref damageScalar);
            if (Unit.HasTrait(Traits.SenseWeakness))
            {
                damageScalar *= 1.4f - (target.Unit.HealthPct * .4f) + (0.1f * target.Unit.GetNegativeStatusEffects());
            }
            if (Unit.HasTrait(Traits.VenomShock) && target.Unit.GetStatusEffect(StatusEffectType.Poisoned) != null)
            {
                damageScalar *= 1.5f;
            }
            if (forceBite)
            {
                damage = (int)(damageScalar * State.World.ItemRepository.Bite.Damage * (60 + Unit.GetStat(Stat.Strength)) / 60);
            }
            else
            {
                if (Unit.HasTrait(Traits.Feral) && Unit.GetBestMelee() == State.World.ItemRepository.Claws)
                    damageScalar *= 3f;
                int statBoost = Unit.GetStat(Stat.Strength) + (Unit.HasTrait(Traits.SpellBlade) ? Unit.GetStat(Stat.Mind) / 2 : 0);
                damage = (int)(damageScalar * BestMelee.Damage * (60 + statBoost) / 60);
            }



            if (target.Unit.HasTrait(Traits.Resilient))
                damage--;
            if (target.Unit.GetStatusEffect(StatusEffectType.Staggering) != null)
            {
                damage += (int)(damage * 0.2f);
            }
        }
        if (Unit.HasTrait(Traits.SwiftStrike))
        {
            int current_weapon_class = GetWeaponSprite();
            if (current_weapon_class == 0 || current_weapon_class == 1 || current_weapon_class == 4 || current_weapon_class == 5)
                current_weapon_class = 0;
            int stat_diff = Unit.GetStat(Stat.Agility) - target.Unit.GetStat(Stat.Agility);
            if (stat_diff >= 25)
            {
                float ss_bonus = 0.25f * (current_weapon_class == 0 ? 3 : 1);
                damage += (int)(damage * ss_bonus);
            }
            else if (stat_diff > 0)
            {
                damage += (int)(damage * (stat_diff/ (current_weapon_class == 0 ? 33 : 100)));
            }
        }

        if (TacticalUtilities.SneakAttackCheck(Unit, target.Unit)) // sneakAttack
        {
            damage *= 3;
        }
        if (damage < 1)
            damage = 1;
        return damage;
    }

    ///// <summary>
    ///// Tells if the unit is able to move at all.   Attackers should use TacticalUtilities.FreeSpaceAroundTaget
    ///// </summary>
    //public bool Surrounded()
    //{
    //    Vec2i p = new Vec2i(0, 0);
    //    for (int i = 0; i < 8; i++)
    //    {
    //        p = GetPos(i);
    //        if (TacticalUtilities.OpenTile(p.x, p.y, this))
    //        {
    //            return false;

    //        }
    //    }
    //    return true;
    //}


    public Vec2i PounceAt(Actor_Unit target)
    {
        if (Movement < 2 || (!target.UnitSprite.isActiveAndEnabled && State.World.IsNight))
            return null;
        if (Position.GetNumberOfMovesDistance(target.Position) < 2 || Position.GetNumberOfMovesDistance(target.Position) > 4)
            return null;
        for (int i = 0; i < 8; i++)
        {
            Vec2i p = target.GetPos(i);
            if (TacticalUtilities.OpenTile(p.x, p.y, this))
            {
                return p;

            }
        }
        return null;
    }

    public bool MeleePounce(Actor_Unit target)
    {
        if (Movement < 2 || Unit.HasTrait(Traits.Pounce) == false)
            return false;
        var landingZone = PounceAt(target);
        if (landingZone != null)
        {
            State.GameManager.TacticalMode.Translator.SetTranslator(UnitSprite.transform, Position, landingZone, 0.5f, State.GameManager.TacticalMode.IsPlayerTurn);
            State.GameManager.TacticalMode.DirtyPack = true;
            SetPos(landingZone);
            float multiplier = 1;
            if (Unit.HasTrait(Traits.HeavyPounce))
            {
                multiplier = Mathf.Min(1 + ((PredatorComponent?.Fullness ?? 0) / 4), 2);
                Unit.ApplyStatusEffect(StatusEffectType.Clumsiness, Mathf.Min((PredatorComponent?.Fullness ?? 0) / 10, .8f), 1);
            }

            Attack(target, false, damageMultiplier: multiplier);
            if (Unit.Race == Race.FeralFrogs)
                Mode = DisplayMode.FrogPouncing;
            return true;
        }
        return false;
    }

    public bool VorePounce(Actor_Unit target, SpecialAction voreType = SpecialAction.None, bool AIAutoPick = false)
    {
        if (Movement < 2 || Unit.HasTrait(Traits.Pounce) == false)
            return false;
        if (TacticalUtilities.AppropriateVoreTarget(this, target) == false)
            return false;
        if (PredatorComponent.FreeCap() < target.Bulk())
            return false;
        var pounceLandingZone = PounceAt(target);
        if (pounceLandingZone != null)
        {
            Vec2i originalLoc = Position;
            SetPos(pounceLandingZone);
            if (AIAutoPick)
            {
                PredatorComponent.UsePreferredVore(target);
            }
            else
            {
                switch (voreType)
                {
                    case SpecialAction.BreastVore:
                        PredatorComponent.BreastVore(target);
                        break;
                    case SpecialAction.CockVore:
                        PredatorComponent.CockVore(target);
                        break;
                    case SpecialAction.Unbirth:
                        PredatorComponent.Unbirth(target);
                        break;
                    case SpecialAction.TailVore:
                        PredatorComponent.TailVore(target);
                        break;
                    case SpecialAction.AnalVore:
                        PredatorComponent.AnalVore(target);
                        break;
                    default:
                        PredatorComponent.Devour(target);
                        break;
                }
            }

            if (target.Visible == false)
            { //Done this way to avoid doubling up references                        
                pounceLandingZone.x = target.Position.x;
                pounceLandingZone.y = target.Position.y;
                SetPos(pounceLandingZone);
            }
            State.GameManager.TacticalMode.Translator.SetTranslator(UnitSprite.transform, originalLoc, pounceLandingZone, 0.5f, State.GameManager.TacticalMode.IsPlayerTurn);
            State.GameManager.TacticalMode.DirtyPack = true;
            if (Unit.Race == Race.FeralFrogs)
                Mode = DisplayMode.FrogPouncing;
            return true;
        }
        return false;
    }

    public bool ShunGokuSatsu(Actor_Unit target)
    {
        if (Movement < 1 || Unit.HasTrait(Traits.ShunGokuSatsu) == false)
            return false;
        List<AbilityTargets> targetTypes = new List<AbilityTargets>();
        targetTypes.Add(AbilityTargets.Enemy);
        if (!TacticalUtilities.MeetsQualifier(targetTypes, this, target))
            return false;
        if (target.Position.GetNumberOfMovesDistance(Position) > 1)
            return false;

        int damage = 2 * WeaponDamageAgainstTarget(target, false);
        if (damage >= target.Unit.Health)
            damage = target.Unit.Health - 1;
        if (target.Defend(this, ref damage, false, out float chance))
        {
            if (State.GameManager.TacticalMode.TacticalSoundBlocked() == false)
            {
                var obj = Object.Instantiate(State.GameManager.TacticalEffectPrefabList.ShunGokuSatsu);
                obj.transform.SetPositionAndRotation(new Vector3(target.Position.x, target.Position.y), new Quaternion());
                MiscUtilities.DelayedInvoke(() => State.GameManager.SoundManager.PlayArrowHit(null), .06f);
                MiscUtilities.DelayedInvoke(() => State.GameManager.SoundManager.PlayMeleeHit(null), .12f);
                MiscUtilities.DelayedInvoke(() => State.GameManager.SoundManager.PlayArmorHit(null), .18f);
                MiscUtilities.DelayedInvoke(() => State.GameManager.SoundManager.PlayMeleeHit(null), .24f);
                State.GameManager.TacticalMode.CreateSwipeHitEffect(target.Position, 0);
                State.GameManager.TacticalMode.CreateSwipeHitEffect(target.Position, 90);
                State.GameManager.TacticalMode.CreateSwipeHitEffect(target.Position, 180);
                State.GameManager.TacticalMode.CreateSwipeHitEffect(target.Position, 270);
            }
            Unit.GiveScaledExp(2 * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level);
        }

        PredatorComponent?.Devour(target, .3f);
        TurnUsedShun = State.GameManager.TacticalMode.currentTurn;
        Movement = 0;

        return true;
    }
    
    // Hey, wanna see the hackiest code I'll ever cobble together?
    public bool Regurgitate(Actor_Unit a, Vec2i l)
    {
        Prey regurged = a.PredatorComponent.FreeRandomPreyToSquare(l);
        if (regurged != null)
        {
            return true;
        }
        else
            return false;
    }

    public bool TailStrike(Actor_Unit target)
    {
        if (Movement < 1 || Unit.HasTrait(Traits.TailStrike) == false)
            return false;
        List<AbilityTargets> targetTypes = new List<AbilityTargets>();
        targetTypes.Add(AbilityTargets.Enemy);
        if (!TacticalUtilities.MeetsQualifier(targetTypes, this, target))
            return false;
        if (target.Position.GetNumberOfMovesDistance(Position) != 1)
            return false;

        if (Unit.Race == Race.Zoey && animationController?.frameLists != null && animationController.frameLists.Count() > 0)
        {
            animationController.frameLists[0].currentlyActive = true;
        }

        if (Unit.Race == Race.Taraluxia && animationController?.frameLists != null && animationController.frameLists.Count() > 0)
        {
            animationController.frameLists[0].currentlyActive = true;
        }

        Attack(target, false, damageMultiplier: .66f);
        Actor_Unit tempTarget = TacticalUtilities.GetActorAt(target.Position + new Vec2(1, 0));
        TestAttack(tempTarget);
        tempTarget = TacticalUtilities.GetActorAt(target.Position + new Vec2(0, 1));
        TestAttack(tempTarget);
        tempTarget = TacticalUtilities.GetActorAt(target.Position + new Vec2(-1, 0));
        TestAttack(tempTarget);
        tempTarget = TacticalUtilities.GetActorAt(target.Position + new Vec2(0, -1));
        TestAttack(tempTarget);

        Movement = 0;

        return true;

        void TestAttack(Actor_Unit sideTarget)
        {
            if (sideTarget != null && sideTarget.Position.GetNumberOfMovesDistance(Position) == 1)
            {
                Movement = 1;
                Attack(sideTarget, false, damageMultiplier: .66f);
            }
        }
    }

    public bool Attack(Actor_Unit target, bool ranged, bool forceBite = false, float damageMultiplier = 1, bool canKill = true)
    {
        Weapon weapon;
        if (ranged)
            weapon = BestRanged;
        else
            weapon = BestMelee;
        if (forceBite)
            weapon = State.World.ItemRepository.Bite;
        if (Movement == 0 || weapon == null)
        {
            return false;
        }

        if (target.Unit.HasTrait(Traits.Dazzle))
        {
            float chance = WillCheckOdds(this, target);
            if (State.Rand.NextDouble() < chance)
            {
                Movement = 0;
                UnitSprite.DisplayDazzle();
                Unit.ApplyStatusEffect(StatusEffectType.Shaken, 0.3f, 1);
                TacticalUtilities.Log.RegisterDazzle(Unit, target.Unit, chance);
                return false;
            }
        }
        float origDamageMult = damageMultiplier;
        bool grazebool = false;
        bool critbool = false;
        if (Config.CombatComplicationsEnabled)
        {
            // Graze Check
            float grazechance = Config.BaseGrazeChance;
            if (Config.StatGraze)
            {
                grazechance = GrazeCheck(this, target);
            }
            grazechance += Unit.TraitBoosts.Outgoing.GrazeRateShift - target.Unit.TraitBoosts.Incoming.GrazeRateShift;
            if (State.Rand.NextDouble() < grazechance)
            {
                float calculatedGrazeDamage = Unit.TraitBoosts.Outgoing.GrazeDamageMult * target.Unit.TraitBoosts.Incoming.GrazeDamageMult;
                damageMultiplier *= (calculatedGrazeDamage) * Config.GrazeDamageMod;
                grazebool = true;
            }

            //Crit check
            float critchance = Config.BaseCritChance;
            if (Config.StatCrit)
            {
                critchance = CritCheck(this, target);
            }
            critchance += Unit.TraitBoosts.Outgoing.CritRateShift - target.Unit.TraitBoosts.Incoming.CritRateShift;
            if (State.Rand.NextDouble() < critchance)
            {
                float calculatedCritDamage = Unit.TraitBoosts.Outgoing.CritDamageMult * target.Unit.TraitBoosts.Incoming.CritDamageMult;
                damageMultiplier *= (calculatedCritDamage) * Config.CritDamageMod;
                critbool = true;
            }
            // Crit and graze check (returns attack to normal state if both are true)
            if (critbool && grazebool)
            {
                damageMultiplier = origDamageMult;
                critbool = false;
                grazebool = false;
            }
        }
        //check range
        int targetRange = target.Position.GetNumberOfMovesDistance(Position);
        if (weapon.Range > 1)
        {
            if ((targetRange >= 2 || (targetRange >= 1 && weapon.Omni)) && targetRange <= weapon.Range)
            {
                if (Unit.Race == Race.Succubi)
                    TacticalGraphicalEffects.SuccubusSwordEffect(target.Position);
                animationUpdateTime = 1.0F;
                Mode = DisplayMode.Attacking;
                if (Unit.TraitBoosts.RangedAttacks > 1)
                {
                    int movementFraction = 1 + MaxMovement() / Unit.TraitBoosts.RangedAttacks;
                    if (Movement > movementFraction)
                        Movement -= movementFraction;
                    else
                        Movement = 0;
                }
                else
                    Movement = 0;
                int remainingHealth = target.Unit.Health;
                int damage = WeaponDamageAgainstTarget(target, true, multiplier: damageMultiplier);

                State.GameManager.SoundManager.PlaySwing(this);
                if (target.Defend(this, ref damage, true, out float chance, canKill))
                {
                    foreach (IAttackStatusEffect trait in Unit.AttackStatusEffects)
                    {
                        trait.ApplyStatusEffect(this, target, true, damage);
                    }
                    if (Unit.HasTrait(Traits.WeaponChanneler) && Unit.Mana >= 6)
                        Unit.SpendMana(6);
                    if (Unit.HasTrait(Traits.Tenacious))
                        Unit.RemoveTenacious();
                    if (target.Unit.HasTrait(Traits.Tenacious))
                        target.Unit.AddTenacious();
                    if (target.Unit.GetStatusEffect(StatusEffectType.Focus) != null)                  
                        target.Unit.RemoveFocus();

                    TacticalGraphicalEffects.CreateProjectile(this, target);
                    State.GameManager.TacticalMode.TacticalStats.RegisterHit(BestRanged, Mathf.Min(damage, remainingHealth), Unit.Side);
                    TacticalUtilities.Log.RegisterHit(Unit, target.Unit, weapon, damage, chance);
                    if (Unit.FixedSide == TacticalUtilities.GetMindControlSide(target.Unit))
                    {
                        StatusEffect charm = target.Unit.GetStatusEffect(StatusEffectType.Charmed);
                        if (charm != null)
                        {
                            target.Unit.StatusEffects.Remove(charm);                // betrayal dispels charm
                        }
                    }
                    Unit.GiveScaledExp(2 * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level);
                    if (target.Unit.IsDead)
                    {
                        State.GameManager.TacticalMode.TacticalStats.RegisterKill(BestRanged, Unit.Side);
                        KillUnit(target, weapon);
                    }
                    if (critbool)
                        target.UnitSprite.DisplayCrit();
                    else if (grazebool)
                        target.UnitSprite.DisplayGraze();
                    return true;
                }
                else
                {
                    Unit.GiveScaledExp(0.5f * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level);
                    State.GameManager.TacticalMode.TacticalStats.RegisterMiss(Unit.Side);
                    TacticalUtilities.Log.RegisterMiss(Unit, target.Unit, weapon, chance);
                    if (Unit.HasTrait(Traits.Tenacious))
                        Unit.AddTenacious();
                }
            }
        }
        else
        {
            if (targetRange < 2)
            {
                animationUpdateTime = 1.0F;
                Mode = DisplayMode.Attacking;
                int meleeAttacks = Unit.TraitBoosts.MeleeAttacks;
                if (Unit.HasTrait(Traits.LightFrame) && PredatorComponent?.PreyCount == 0)
                    meleeAttacks++;
                if (meleeAttacks > 1)
                {
                    int movementFraction = 1 + MaxMovement() / meleeAttacks;
                    if (Movement > movementFraction)
                        Movement -= movementFraction;
                    else
                        Movement = 0;
                }
                else
                    Movement = 0;
                int remainingHealth = target.Unit.Health;
                int damage = WeaponDamageAgainstTarget(target, false, multiplier: damageMultiplier, forceBite);
                if (target.Defend(this, ref damage, false, out float chance, canKill))
                {
                    foreach (IAttackStatusEffect trait in Unit.AttackStatusEffects)
                    {
                        trait.ApplyStatusEffect(this, target, false, damage);
                    }
                    if (Unit.HasTrait(Traits.WeaponChanneler) && Unit.Mana >= 6)
                        Unit.SpendMana(6);
                    if (Unit.HasTrait(Traits.BladeDance))
                        Unit.AddBladeDance();
                    if (target.Unit.HasTrait(Traits.BladeDance))
                        target.Unit.RemoveBladeDance();
                    if (Unit.HasTrait(Traits.Tenacious))
                        Unit.RemoveTenacious();
                    if (target.Unit.HasTrait(Traits.Tenacious))
                        target.Unit.AddTenacious();
                    if (target.Unit.GetStatusEffect(StatusEffectType.Focus) != null)
                        target.Unit.RemoveFocus();
                    if (target.Unit.HasTrait(Traits.Toxic) && State.Rand.Next(8) == 0)
                        Unit.ApplyStatusEffect(StatusEffectType.Poisoned, 2 + target.Unit.GetStat(Stat.Endurance) / 20, 3);
                    if (Unit.HasTrait(Traits.ForcefulBlow))
                        TacticalUtilities.KnockBack(this, target);
                    State.GameManager.SoundManager.PlayMeleeHit(target);
                    State.GameManager.TacticalMode.TacticalStats.RegisterHit(BestMelee, Mathf.Min(damage, remainingHealth), Unit.Side);
                    TacticalUtilities.Log.RegisterHit(Unit, target.Unit, weapon, damage, chance);
                    if (Unit.FixedSide == TacticalUtilities.GetMindControlSide(target.Unit))
                    {
                        StatusEffect charm = target.Unit.GetStatusEffect(StatusEffectType.Charmed);
                        if (charm != null)
                        {
                            target.Unit.StatusEffects.Remove(charm);                // betrayal dispels charm
                        }
                    }
                    CreateHitEffects(target);
                    Unit.GiveScaledExp(2 * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level);
                    if (target.Unit.IsDead)
                    {
                        State.GameManager.TacticalMode.TacticalStats.RegisterKill(BestMelee, Unit.Side);
                        KillUnit(target, weapon);
                    }
                    if (critbool)
                        target.UnitSprite.DisplayCrit();
                    else if (grazebool)
                        target.UnitSprite.DisplayGraze();
                    return true;
                }
                else
                {
                    Unit.GiveScaledExp(0.5f * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level);
                    State.GameManager.TacticalMode.TacticalStats.RegisterMiss(Unit.Side);
                    TacticalUtilities.Log.RegisterMiss(Unit, target.Unit, weapon, chance);
                    State.GameManager.SoundManager.PlaySwing(this);
                    if (Unit.HasTrait(Traits.Tenacious))
                        Unit.AddTenacious();
                    if (Unit.Race == Race.Xelhilde)
                    {
                        if (Unit.BodyAccentType2 >= 2)
                            Unit.BodyAccentType2 = 0;
                        else
                            Unit.BodyAccentType2++;
                    }
                }
            }

        }
        return false;
    }

    void CreateHitEffects(Actor_Unit target)
    {
        State.GameManager.TacticalMode.CreateBloodHitEffect(target.Position);
        if (Unit.Race == Race.Asura)
            State.GameManager.TacticalMode.CreateSwipeHitEffect(target.Position);
        if (Unit.Race == Race.Xelhilde)
        {
            if (Unit.BodyAccentType2 >= 2)
                Unit.BodyAccentType2 = 0;
            else
                Unit.BodyAccentType2++;
        }
    }

    private void KillUnit(Actor_Unit target, Weapon weapon)
    {
        TacticalUtilities.Log.RegisterKill(Unit, target.Unit, weapon);
        Unit.KilledUnits++;
        target.KilledByDigestion = false;
        if (target.Unit.Race == Race.Asura)
            Unit.EarnedMask = true;
        Unit.EnemiesKilledThisBattle++;
        target.Unit.KilledBy = Unit;
        target.Unit.Kill();
        if (Unit.HasTrait(Traits.KillerKnowledge) && Unit.KilledUnits % 4 == 0)
            Unit.GeneralStatIncrease(1);
        if (Unit.HasTrait(Traits.TasteForBlood))
            GiveRandomBoost();

        Unit.GiveScaledExp(4 * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level);
        if (target.Unit.GetStatusEffect(StatusEffectType.Respawns) != null && (target.Unit.HasTrait(Traits.Respawner) || target.Unit.HasTrait(Traits.RespawnerIII)))
        {
            var spawnLoc = TacticalUtilities.GetRandomTileForActor(target);
            if (spawnLoc == null)
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{target.Unit.Name} was unable to respawn!");
            else
                TacticalUtilities.Resurrect((spawnLoc),target);
                TacticalGraphicalEffects.CreateGenericMagic(spawnLoc, spawnLoc, target, TacticalGraphicalEffects.SpellEffectIcon.Resurrect);
                target.Unit.Health = target.Unit.MaxHealth;
                target.Unit.RemoveRespawns();
                if (target.Unit.Race != Race.RwuMercenaries)
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{target.Unit.Name} has respawned!");
                else
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"Reinforcements have arrived!");
        }
    }

    private void KillUnit(Actor_Unit target, Spell spell)
    {
        TacticalUtilities.Log.RegisterSpellKill(Unit, target.Unit, spell.SpellType);
        Unit.KilledUnits++;
        target.KilledByDigestion = false;
        if (target.Unit.Race == Race.Asura)
            Unit.EarnedMask = true;
        Unit.EnemiesKilledThisBattle++;
        target.Unit.KilledBy = Unit;
        target.Unit.Kill();
        if (Unit.HasTrait(Traits.KillerKnowledge) && Unit.KilledUnits % 4 == 0)
            Unit.GeneralStatIncrease(1);
        if (Unit.HasTrait(Traits.TasteForBlood))
            GiveRandomBoost();
        Unit.GiveScaledExp(4 * target.Unit.ExpMultiplier, Unit.Level - target.Unit.Level);
        if (target.Unit.GetStatusEffect(StatusEffectType.Respawns) != null && (target.Unit.HasTrait(Traits.Respawner) || target.Unit.HasTrait(Traits.RespawnerIII)))
        {
            var spawnLoc = TacticalUtilities.GetRandomTileForActor(target);
            if (spawnLoc == null)
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{target.Unit.Name} was unable to respawn!");
            else
                TacticalUtilities.Resurrect((spawnLoc),target);
                TacticalGraphicalEffects.CreateGenericMagic(spawnLoc, spawnLoc, target, TacticalGraphicalEffects.SpellEffectIcon.Resurrect);
                target.Unit.Health = target.Unit.MaxHealth;
                target.Unit.RemoveRespawns();
                if (target.Unit.Race != Race.RwuMercenaries)
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{target.Unit.Name} has respawned!");
                else
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"Reinforcements have arrived!");
        }
    }

    /// <summary>
    /// Gives a random temporary boost, associated with the Taste for blood trait
    /// </summary>
    internal void GiveRandomBoost()
    {
        int rand = State.Rand.Next(5);
        switch (rand)
        {
            case 0:
                Unit.StatusEffects.Add(new StatusEffect(StatusEffectType.Valor, .25f, 5));
                break;
            case 1:
                Unit.StatusEffects.Add(new StatusEffect(StatusEffectType.Fast, 0.3f, 5));
                break;
            case 2:
                Unit.StatusEffects.Add(new StatusEffect(StatusEffectType.Mending, 24, 5));
                break;
            case 3:
                Unit.StatusEffects.Add(new StatusEffect(StatusEffectType.Shielded, .25f, 5));
                break;
            case 4:
                Unit.StatusEffects.Add(new StatusEffect(StatusEffectType.Predation, Unit.GetStat(Stat.Voracity) / 4, 5));
                break;
        }
    }

    internal bool DefendSpellCheck(Spell spell, Actor_Unit attacker, out float chance, float mod = 0, Stat stat = Stat.Mind)
    {
        State.GameManager.TacticalMode.AITimer = Config.TacticalAttackDelay;
        if (State.GameManager.CurrentScene == State.GameManager.TacticalMode && State.GameManager.TacticalMode.IsPlayerInControl == false && State.GameManager.TacticalMode.turboMode == false)
            State.GameManager.CameraCall(Position);
        chance = spell.Resistable ? GetMagicChance(attacker, spell, mod, stat) : 1;
        float r = (float)State.Rand.NextDouble();
        return r < chance;

    }

    internal bool DefendDamageSpell(DamageSpell spell, Actor_Unit attacker, int damage)
    {
        if (TacticalUtilities.SneakAttackCheck(attacker.Unit, Unit))
        {
            attacker.Unit.hiddenFixedSide = false;
            damage *= 3;
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=purple>{attacker.Unit.Name} sneak-attacked {Unit.Name}!</color>");
        }
        if (attacker.Unit.GetApparentSide() != Unit.GetApparentSide())
        {
            if (attacker.sidesAttackedThisBattle == null)
                attacker.sidesAttackedThisBattle = new List<int>();
            attacker.sidesAttackedThisBattle.Add(Unit.GetApparentSide());
        }
        if (Unit.IsDead)
            return false;
        if (DefendSpellCheck(spell, attacker, out float chance))
        {
            damage = (int)(damage * attacker.Unit.TraitBoosts.Outgoing.MagicDamage * Unit.TraitBoosts.Incoming.MagicDamage);
            State.GameManager.TacticalMode.TacticalStats.RegisterHit(spell, Mathf.Min(damage, Unit.Health), attacker.Unit.Side);
            Damage(damage, true, damageType: spell.DamageType);
            State.GameManager.TacticalMode.Log.RegisterSpellHit(attacker.Unit, Unit, spell.SpellType, damage, chance);
            if (attacker.Unit.FixedSide == TacticalUtilities.GetMindControlSide(Unit))
            {
                StatusEffect charm = Unit.GetStatusEffect(StatusEffectType.Charmed);
                if (charm != null)
                {
                    Unit.StatusEffects.Remove(charm);                // betrayal dispels charm
                }
            }
            if (attacker.Unit.HasTrait(Traits.ArcaneMagistrate))
            {
                attacker.Unit.AddFocus((Unit.IsDead ? 5 : 1));
            }
            if (Unit.HasTrait(Traits.MagicSynthesis))
            {
                Unit.SingleUseSpells.Add(spell.SpellType);
                Unit.RestoreMana((int)(spell.ManaCost * 0.75f));
                Unit.UpdateSpells();
            }
            attacker.Unit.GiveScaledExp(1 * Unit.ExpMultiplier, Unit.Level - Unit.Level);
            if (Unit.IsDead)
            {
                attacker.Unit.GiveScaledExp(4 * Unit.ExpMultiplier, Unit.Level - Unit.Level);
            }
            return true;
        }
        else
        {
            UnitSprite.DisplayDamage(0);
            State.GameManager.TacticalMode.Log.RegisterSpellMiss(attacker.Unit, Unit, spell.SpellType, chance);
            attacker.Unit.GiveScaledExp(.25f * Unit.ExpMultiplier, Unit.Level - Unit.Level);
        }

        return false;
    }

    internal bool DefendStatusSpell(StatusSpell spell, Actor_Unit attacker, Stat stat = Stat.Mind)
    {
        if (spell.Id == "hypno-fart" && Unit.FixedSide == attacker.Unit.FixedSide)
        {
            return false;
        }
        bool sneakAttack = false;
        if (TacticalUtilities.SneakAttackCheck(attacker.Unit, Unit) && !spell.AcceptibleTargets.Contains(AbilityTargets.Ally))     // Replace when there is an unresistable negative status
        {
            attacker.Unit.hiddenFixedSide = false;
            sneakAttack = true;
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=purple>{attacker.Unit.Name} sneak-attacked {Unit.Name}!</color>");
        }
        if (attacker.Unit.GetApparentSide() != Unit.GetApparentSide() && !spell.AcceptibleTargets.Contains(AbilityTargets.Ally))
        {
            if (attacker.sidesAttackedThisBattle == null)
                attacker.sidesAttackedThisBattle = new List<int>();
            attacker.sidesAttackedThisBattle.Add(Unit.GetApparentSide());
        }
        if (DefendSpellCheck(spell, attacker, out float chance, sneakAttack ? -0.3f : 0f, stat))
        {
            State.GameManager.TacticalMode.Log.RegisterSpellHit(attacker.Unit, Unit, spell.SpellType, 0, chance);
            Unit.ApplyStatusEffect(spell.Type, spell.Effect(attacker, this), spell.Duration(attacker, this));
            if (spell.Id == "charm")
            {
                UnitSprite.DisplayCharm();
                if (attacker.Unit.HasTrait(Traits.Temptation))
                {
                    Unit.ApplyStatusEffect(StatusEffectType.Temptation, spell.Effect(attacker, this), spell.Duration(attacker, this));
                }
            }
            if (spell.Id == "hypno-fart")
            {
                UnitSprite.DisplayHypno();
                if (attacker.Unit.HasTrait(Traits.Temptation))
                {
                    Unit.ApplyStatusEffect(StatusEffectType.Temptation, spell.Effect(attacker, this), spell.Duration(attacker, this));
                }
            }
            if (spell.Alraune)
            {
                if (Unit.HasTrait(Traits.PollenProjector) == false)
                {
                    Unit.ApplyStatusEffect(StatusEffectType.Clumsiness, 1.5f, 3);
                    Unit.ApplyStatusEffect(StatusEffectType.Poisoned, 2 + attacker.Unit.GetStat(Stat.Mind) / 10, 3);
                    Unit.ApplyStatusEffect(StatusEffectType.WillingPrey, 0, 3);
                }
            }
            if (Unit.HasTrait(Traits.MagicSynthesis))
            {
                Unit.SingleUseSpells.Add(spell.SpellType);
                Unit.RestoreMana((int)(spell.ManaCost * 0.75f));
                Unit.UpdateSpells();
            }
            if (spell.Id == "whispers-spell")
            {
                UnitSprite.DisplayCharm();
                Unit.ApplyStatusEffect(StatusEffectType.WillingPrey, 0, spell.Duration(attacker, this));
                Unit.ApplyStatusEffect(StatusEffectType.Temptation, 0, spell.Duration(attacker, this));
            }
            if (TacticalUtilities.MeetsQualifier(spell.AcceptibleTargets, attacker, this))
                attacker.Unit.GiveScaledExp(1, attacker.Unit.Level - Unit.Level);
            else
                attacker.Unit.GiveExp(1);
            return true;
        }
        else
        {
            UnitSprite.DisplayDamage(0);
            if (attacker.Unit.Side == Unit.Side)
                attacker.Unit.GiveScaledExp(.25f, attacker.Unit.Level - Unit.Level);
            else
            {
                attacker.Unit.GiveExp(.25f);
                UnitSprite.DisplayResist();
            }

            State.GameManager.TacticalMode.Log.RegisterSpellMiss(attacker.Unit, Unit, spell.SpellType, chance);
        }

        return false;
    }

    public bool Defend(Actor_Unit attacker, ref int damage, bool ranged, out float chance, bool canKill = true)
    {
        if (TacticalUtilities.SneakAttackCheck(attacker.Unit, Unit))
        {
            attacker.Unit.hiddenFixedSide = false;
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=purple>{attacker.Unit.Name} sneak-attacked {Unit.Name}!</color>");
        }
        if (attacker.Unit.GetApparentSide() != Unit.GetApparentSide())
        {
            if (attacker.sidesAttackedThisBattle == null)
                attacker.sidesAttackedThisBattle = new List<int>();
            attacker.sidesAttackedThisBattle.Add(Unit.GetApparentSide());
        }
        State.GameManager.TacticalMode.AITimer = Config.TacticalAttackDelay;
        if (State.GameManager.CurrentScene == State.GameManager.TacticalMode && State.GameManager.TacticalMode.IsPlayerInControl == false && State.GameManager.TacticalMode.turboMode == false)
            State.GameManager.CameraCall(Position);
        chance = GetAttackChance(attacker, ranged);

        float r = (float)State.Rand.NextDouble();
        if (r < chance)
        {
            Damage(damage, canKill: canKill);
            if (canKill == false && attacker.Unit.HasTrait(Traits.VenomousBite))
            {
                Unit.ApplyStatusEffect(StatusEffectType.Poisoned, 3, 3);
                Unit.ApplyStatusEffect(StatusEffectType.Shaken, .2f, 2);
            }
            return true;
        }
        else
            UnitSprite.DisplayDamage(0);
        return false;
    }

    //This is the chance to be devoured, so it belongs here and not in PredatorComponent
    public float GetDevourChance(Actor_Unit attacker, bool includeSecondaries = false, int skillBoost = 0, bool force = false)
    {
        if (attacker?.Unit.Predator == false && !force)
        {
            return 0;
        }
        if (Surrendered || (attacker.Unit.HasTrait(Traits.Endosoma) && (Unit.FixedSide == attacker.Unit.GetApparentSide(Unit)) || Unit.GetStatusEffect(StatusEffectType.Hypnotized)?.Strength == attacker.Unit.FixedSide))
            return 1f;

        float predVoracity = Mathf.Pow(15 + skillBoost + attacker.Unit.GetStat(Stat.Voracity), 1.5f);
        float preyStrength = Mathf.Pow(15 + Unit.GetStat(Stat.Strength), 1.5f);
        float preyWill = Mathf.Pow(15 + Unit.GetStat(Stat.Will), 1.5f);
        float attackerScore = predVoracity * (attacker.Unit.HealthPct + 1) * (30 + attacker.BodySize()) / 16 / (1 + 2 * Mathf.Pow(attacker.PredatorComponent.UsageFraction, 2));
        float defenderHealthPct = Unit.HealthPct;
        float defenderScore = 20 + (2 * (preyStrength + 2 * preyWill) * defenderHealthPct * defenderHealthPct * ((10 + BodySize()) / 2));

        if (Unit.GetStatusEffect(StatusEffectType.WillingPrey) != null || Unit.GetStatusEffect(StatusEffectType.Sleeping) != null)
        {
            defenderScore /= 2;
        }

        switch (Config.VoreRate)
        {
            case -1:
                attackerScore *= .4f;
                break;
            case 1:
                attackerScore *= 2;
                break;
            case 2:
                attackerScore *= 4;
                break;
        }

        defenderScore /= Unit.TraitBoosts.Incoming.VoreOddsMult;
        attackerScore *= attacker.Unit.TraitBoosts.Outgoing.VoreOddsMult;

        if (TacticalUtilities.SneakAttackCheck(attacker.Unit, Unit)) // sneakAttack
        {
            attackerScore *= 3;
        }

        if (attacker.Unit.HasTrait(Traits.VenomShock) && Unit.GetStatusEffect(StatusEffectType.Poisoned) != null)
            attackerScore *= 1.5f;

        if (attacker.Unit.HasTrait(Traits.AllOutFirstStrike) && attacker.HasAttackedThisCombat == false)
            attackerScore *= 3.25f;

        foreach (IVoreAttackOdds trait in attacker.Unit.VoreAttackOdds)
        {
            trait.VoreAttack(attacker, ref attackerScore);
        }

        foreach (IVoreDefenseOdds trait in Unit.VoreDefenseOdds)
        {
            trait.VoreDefense(this, ref defenderScore);
        }

        float odds = attackerScore / (attackerScore + defenderScore) * 100;

        odds *= Unit.TraitBoosts.FlatHitReduction;

        if (includeSecondaries)
        {
            if (Unit.HasTrait(Traits.Dazzle))
            {
                odds *= 1 - WillCheckOdds(attacker, this);
            }
        }
        return odds / 100;


    }

    public bool BellyRub(Actor_Unit target)
    {
        Prey prey;
        if (target.Unit.Predator == false)
            return false;
        List<int> possible = new List<int>();
        int type;
        if (target.PredatorComponent.Fullness <= 0)
        {
            return false;
        }
        if (Position.GetNumberOfMovesDistance(target.Position) > 1)
        {
            return false;
        }
        if (target.PredatorComponent.BreastFullness > 0)
        {
            possible.Add(1);
        }
        if (target.PredatorComponent.LeftBreastFullness > 0)
        {
            possible.Add(1);
        }
        if (target.PredatorComponent.RightBreastFullness > 0)
        {
            possible.Add(1);
        }
        if (target.PredatorComponent.WombFullness > 0 || target.PredatorComponent.CombinedStomachFullness > 0)
        {
            possible.Add(0);
        }
        if (target.PredatorComponent.BallsFullness > 0)
        {
            possible.Add(2);
        }
        if (target.PredatorComponent.TailFullness > 0)
        {
            possible.Add(3);
        }

        if (possible.Count == 0)
            return false;
        if (target.ReceivedRub)
            return false;
        if ((target.Unit.GetApparentSide() != Unit.GetApparentSide() && target.Unit.GetApparentSide() != Unit.FixedSide) && !(Unit.HasTrait(Traits.SeductiveTouch) || Config.CanUseStomachRubOnEnemies || TacticalUtilities.GetMindControlSide(Unit) != -1))
            return false;
        target.RubCount++;
        target.BeingRubbed = true;
        int index = Random.Range(0, possible.Count - 1);
        type = possible[index];
        switch (type)
        {
            case 0:
                prey = target.PredatorComponent.GetDirectPrey().FirstOrDefault(p => p.Location.Equals(PreyLocation.stomach) || p.Location.Equals(PreyLocation.stomach2) || p.Location.Equals(PreyLocation.anal) || p.Location.Equals(PreyLocation.womb));
                if (prey == null) break;
                TacticalUtilities.Log.RegisterBellyRub(Unit, target.Unit, prey.Unit, 1f);
                break;
            case 1:
                prey = target.PredatorComponent.GetDirectPrey().FirstOrDefault(p => p.Location.Equals(PreyLocation.breasts) || p.Location.Equals(PreyLocation.leftBreast) || p.Location.Equals(PreyLocation.rightBreast));
                if (prey == null) break;
                TacticalUtilities.Log.RegisterBreastRub(Unit, target.Unit, prey.Unit, 1f);
                break;
            case 2:
                prey = target.PredatorComponent.GetDirectPrey().FirstOrDefault(p => p.Location.Equals(PreyLocation.balls));
                if (prey == null) break;
                TacticalUtilities.Log.RegisterBallMassage(Unit, target.Unit, prey.Unit, 1f);
                break;
            case 3:
                prey = target.PredatorComponent.GetDirectPrey().FirstOrDefault(p => p.Location.Equals(PreyLocation.tail));
                if (prey == null) break;
                TacticalUtilities.Log.RegisterTailRub(Unit, target.Unit, prey.Unit, 1f);
                break;
            default:
                break;
        }
        if (!State.GameManager.TacticalMode.turboMode)
        {
            SetRubMode();
            target.SetRubbedMode();
            GameObject.Instantiate(State.GameManager.TacticalMode.HandPrefab, new Vector3(target.Position.x + UnityEngine.Random.Range(-0.2F, 0.2F), target.Position.y + 0.1F + UnityEngine.Random.Range(-0.1F, 0.1F)), new Quaternion());
            State.GameManager.TacticalMode.AITimer = Config.TacticalVoreDelay;
        }
        target.DigestCheck();
        if (Unit.HasTrait(Traits.PleasurableTouch))
            target.DigestCheck();
        target.BeingRubbed = false;
        int thirdMovement = MaxMovement() / 3;
        if (Movement > thirdMovement)
            Movement -= thirdMovement;
        else
            Movement = 0;

        if (Unit.HasTrait(Traits.SeductiveTouch) && (target.Unit.FixedSide != Unit.FixedSide) && target.TurnsSinceLastDamage > 1)
        {
            bool seduce = false;
            bool distract = false;
            if (!target.Unit.HasTrait(Traits.Untamable))
                for (int i = 0; i < (Unit.HasTrait(Traits.PleasurableTouch) ? 2 : 1); i++)
                {
                    float r = (float)State.Rand.NextDouble();
                    if (r < GetPureStatClashChance(Unit.GetStat(Stat.Dexterity), target.Unit.GetStat(Stat.Will), .1f))
                    {
                        if (target.TurnsSinceLastParalysis <= 0 || target.Paralyzed)
                        {
                            seduce = true;
                        }
                        else
                        {
                            distract = true;
                        }

                    }
                }
            if (seduce)
            {
                var strings = new[] {$"<b>{target.Unit.Name}</b> decides to swap sides because <b>{Unit.Name}</b>'s rubs are just that sublime!",
                            $"<b>{target.Unit.Name}</b> joins <b>{Unit.Name}</b>'s side to get more of those irresistible scritches later!",
                            $"The way <b>{Unit.Name}</b> touches {LogUtilities.GPPHim(target.Unit)} moves something other than {LogUtilities.GPPHis(target.Unit)} prey-filled innards in <b>{target.Unit.Name}</b>, making {LogUtilities.GPPHim(target.Unit)} join {LogUtilities.GPPHim(Unit)}.",
                            $"<b>{Unit.Name}</b> makes <b>{target.Unit.Name}</b> feel incredible, rearranging {LogUtilities.GPPHis(target.Unit)} priorities in this conflict...",
                            $"<b>{target.Unit.Name}</b> slowly returns from a world of pure bliss and decides to stick with <b>{Unit.Name}</b> after all."};
                if (target.Unit.Race == Race.Dogs)
                    strings.Append($"<b>{Unit.Name}</b>’s attentive massage of <b>{target.Unit.Name}</b>’s stuffed midsection convinces the voracious canine to make {LogUtilities.GPPHim(Unit)} {LogUtilities.GPPHis(target.Unit)} master no matter the cost.");
                target.UnitSprite.DisplaySeduce();
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous(
                        LogUtilities.GetRandomStringFrom(strings)
                );
                if (target.Unit.Side != Unit.Side)
                    State.GameManager.TacticalMode.SwitchAlignment(target);
                target.Unit.FixedSide = Unit.FixedSide;
                target.Unit.hiddenFixedSide = true;
            }
            else if (distract)
            {
                target.UnitSprite.DisplayDistract();
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous(
                        LogUtilities.GetRandomStringFrom(
                        $"<b>{target.Unit.Name}</b> is so enthralled by <b>{Unit.Name}</b>'s touch that {LogUtilities.GPPHe(target.Unit)} forgot what {LogUtilities.GPPHeWas(target.Unit)} gonna do.",
                        $"Despite the battle, <b>{target.Unit.Name}</b> wants to spend {LogUtilities.GPPHis(target.Unit)} turn enjoying <b>{Unit.Name}</b>'s affection.",
                        $"It looks like <b>{Unit.Name}</b>'s rubs take up 100% of <b>{target.Unit.Name}</b>'s attention right now.",
                        $"<b>{target.Unit.Name}</b>'s aggression and tactics melt in <b>{Unit.Name}</b>'s hands",
                        $"<b>{Unit.Name}</b>'s massage really hits the spot, causing <b>{target.Unit.Name}</b> to close {LogUtilities.GPPHis(target.Unit)} eyes and forget about the battle for a moment."
                        )
                );
                target.Paralyzed = true;
            }
        }

        return true;
    }

    public float BodySize()
    {
        float size = State.RaceSettings.GetBodySize(Unit.Race);

        size *= Unit.GetScale(2);

        size *= Unit.TraitBoosts.BulkMultiplier;

        if (Unit.GetStatusEffect(StatusEffectType.Petrify) != null)
            size *= 3;

        if (Unit.GetStatusEffect(StatusEffectType.Frozen) != null)
            size *= 2;

        return size;
    }

    public float Bulk(int count = 0)
    {
        if (Unit.HasTrait(Traits.Inedible))
            return float.MaxValue / 100;
        float bulk = 0;
        bulk += PredatorComponent?.GetBulkOfPrey(count) ?? 0;
        if (Unit.IsDead)
        {
            float myBulk = Unit.Health + Unit.MaxHealth;
            myBulk = myBulk / (Unit.MaxHealth) * BodySize();

            bulk += myBulk;
        }
        else
        {
            bulk += BodySize();
        }
        return bulk;
    }





    public bool Move(int direction, TacticalTileType[,] tiles)
    {
        //check if we can move in that direction
        switch (direction)
        {
            case 0:
                return Move(0, 1, tiles);
            case 1:
                return Move(1, 1, tiles);
            case 2:
                return Move(1, 0, tiles);
            case 3:
                return Move(1, -1, tiles);
            case 4:
                return Move(0, -1, tiles);
            case 5:
                return Move(-1, -1, tiles);
            case 6:
                return Move(-1, 0, tiles);
            case 7:
                return Move(-1, 1, tiles);

        }

        return false;
    }

    internal Vec2i GetTile(Vec2i start, int direction)
    {
        switch (direction)
        {
            case 0:
                return new Vec2i(start.x, start.y + 1);
            case 1:
                return new Vec2i(start.x + 1, start.y + 1);
            case 2:
                return new Vec2i(start.x + 1, start.y);
            case 3:
                return new Vec2i(start.x + 1, start.y - 1);
            case 4:
                return new Vec2i(start.x, start.y - 1);
            case 5:
                return new Vec2i(start.x - 1, start.y - 1);
            case 6:
                return new Vec2i(start.x - 1, start.y);
            case 7:
                return new Vec2i(start.x - 1, start.y + 1);
            default:
                return null;
        }
    }

    internal int DiffToDirection(int x, int y)
    {
        if (x == 0 && y > 0) return 0;
        else if (x > 0 && y > 0) return 1;
        else if (x > 0 && y == 0) return 2;
        else if (x > 0 && y < 0) return 3;
        else if (x == 0 && y < 0) return 4;
        else if (x < 0 && y < 0) return 5;
        else if (x < 0 && y == 0) return 6;
        else if (x < 0 && y > 0) return 7;
        else return 0;
    }


    internal bool MoveTo(Vec2i destination, TacticalTileType[,] tiles, float delay)
    {
        if (destination.x < 0 || destination.y < 0 || destination.x > tiles.GetUpperBound(0) || destination.y > tiles.GetUpperBound(1))
            return false;
        int cost = TacticalTileInfo.TileCost(new Vec2(destination.x, destination.y));
        if (Unit.HasTrait(Traits.Flight))
            cost = 1;
        if (Movement < cost)
            return false;
        if ((TacticalUtilities.PassableOpenTile(destination, this) && ((Unit.HasTrait(Traits.PassThrough) && Movement > 1) || (Unit.HasTrait(Traits.Blitz) && Movement > 1) || (Unit.HasTrait(Traits.SpectralStep) && Movement > 1))) || (Unit.HasTrait(Traits.Flight) && Movement > 1) || TacticalUtilities.OpenTile(destination, this))
        {
            State.GameManager.TacticalMode.Translator.SetTranslator(UnitSprite.transform, Position, destination, delay, State.GameManager.TacticalMode.IsPlayerTurn);
            State.GameManager.TacticalMode.AITimer = delay;
            State.GameManager.TacticalMode.DirtyPack = true;
            Position = destination;
            //State.GameManager.CameraCall(Position);
            Movement -= cost;
            return true;
        }
        return false;
    }



    bool Move(int changeX, int changeY, TacticalTileType[,] tiles)
    {
        //float delay = AI ? Config.TacticalPlayerMovementDelay : Config.TacticalAIMovementDelay;
        Vec2i newLocation = new Vec2i(Position.x + changeX, Position.y + changeY);
        return MoveTo(newLocation, tiles, Config.TacticalPlayerMovementDelay);
    }


    public void Update(float dt)
    {
        if (animationUpdateTime > 0)
        {
            animationUpdateTime -= dt;
            if (animationUpdateTime <= 0)
            {
                if (Mode == DisplayMode.IdleAnimation)
                {
                    animationUpdateTime = 0.25f;
                    animationStep--;
                    if (animationStep == 0)
                    {
                        if (Mode > DisplayMode.Attacking && Mode < DisplayMode.VoreSuccess && modeQueue.Count > 0)
                        {
                            animationUpdateTime = modeQueue.FirstOrDefault().Value;
                            Mode = (DisplayMode)modeQueue.FirstOrDefault().Key;                                        // This casting back and forth saves dealing with accessibility hassles.
                            modeQueue.RemoveAt(0);
                        }
                        else
                        {
                            animationUpdateTime = 0;
                            Mode = 0;
                        }
                    }
                }
                else
                {
                    if (Mode >= DisplayMode.Attacking && Mode <= DisplayMode.FrogPouncing)
                        HasAttackedThisCombat = true;
                    if (modeQueue.Count > 0)
                    {
                        animationUpdateTime = modeQueue.FirstOrDefault().Value;
                        Mode = (DisplayMode)modeQueue.FirstOrDefault().Key;
                        modeQueue.RemoveAt(0);
                    }
                    else
                    {
                        animationUpdateTime = 0;
                        Mode = 0;
                    }

                }
            }
        }
        if (Unit.Predator)
            PredatorComponent.UpdateTransition();
    }

    public void NewTurn()
    {
        if (Surrendered && Unit.HasTrait(Traits.Fearless))
        {
            Surrendered = false;
        }
        else if (SurrenderedThisTurn)
        {
            SurrenderedThisTurn = false;
            Movement = 0;
        }

        AIAvoidEat--;
        if (Unit.HasTrait(Traits.ManaAttuned))
        {
            if (!Unit.SpendMana(Unit.MaxMana / 10))
                if (Unit.Mana > 0)
                    Unit.SpendMana(Unit.Mana); //Zero out mana
                else
                    Unit.ApplyStatusEffect(StatusEffectType.Sleeping, 1, 2);
            if (Unit.GetStatusEffect(StatusEffectType.Sleeping) != null)
                Unit.RestoreMana(Unit.MaxMana / 2);
        }
        Unit.RestoreMana(Unit.TraitBoosts.ManaRegen);
        UnitSprite.UpdateHealthBar(this);
        TurnsSinceLastParalysis++;
        if (Targetable && Visible && Surrendered == false && Fled == false)
            RestoreMP();
        Unit.TickStatusEffects();
        Unit.Heal(Unit.TraitBoosts.HealthRegen);
        if (Unit.HasTrait(Traits.Perseverance) && TurnsSinceLastDamage > 3)
        {
            Unit.HealPercentage(0.03f * TurnsSinceLastDamage);
        }
        if (Unit.HasTrait(Traits.Timid) && ((Unit.NearbyEnemies - 1) > Unit.NearbyFriendlies))
        {
            Unit.ApplyStatusEffect(StatusEffectType.Shaken, .2f, 1);
        }
        if ((Config.AbsorbLoss ? PredatorComponent?.AlivePrey <= 0 : PredatorComponent.Fullness <= 0))
        {
            RampStacks -= Config.DigestionRampLoss;
            if (RampStacks < 0)
            {
                RampStacks = 0;
            }

        }
        else
            RampStacks += (Config.DigestionRampLoss >= 0 ? 1 : -1) / Config.DigestionRampTurn;

        RubCount = 0;
        TurnsSinceLastDamage++;
    }

    public void SubtractHealth(int damage)
    {
        Unit.Health -= damage;
        if (Unit.Health > Unit.MaxHealth)
            Unit.Health = Unit.MaxHealth;
        TurnsSinceLastDamage = -1;
    }

    public int CalculateDamageWithResistance(int damage, DamageTypes damageType)
    {
        switch (damageType)
        {
            case DamageTypes.Fire:
                damage = (int)Mathf.Round(damage * Unit.TraitBoosts.FireDamageTaken);
                break;
            case DamageTypes.Ice:
                damage = (int)Mathf.Round(damage * Unit.TraitBoosts.IceDamageTaken);
                break;
            case DamageTypes.Elec:
                float elecboost = 1f;
                if (Unit.GetStatusEffect(StatusEffectType.Static) != null)
                    elecboost = 1.5f;
                damage = (int)Mathf.Round(damage * (Unit.TraitBoosts.ElecDamageTaken * elecboost));
                break;
            case DamageTypes.Poison:
                if (Unit.HasTrait(Traits.PoisonSpit))
                    damage = 0;
                break;
            default:
                break;
        }
        if (Unit.HasTrait(Traits.ManaBarrier) && Unit.ManaPct >= 0.51f)
        {
            int reduc_dmg = (int)((Unit.ManaPct - .5f) * damage);
            if (Unit.SpendMana(reduc_dmg))
                damage -= reduc_dmg;
        }
        return damage;
    }

    public bool Damage(int damage, bool spellDamage = false, bool canKill = true, DamageTypes damageType = DamageTypes.Generic)
    {
        if (Unit.IsDead)
        {
            //These are thrown in as insurance incase of weirdness - there was a bug report of a unit that had negative health and was not flagged as dead, and didn't die when hit.
            Targetable = false;
            Surrendered = true;
            PredatorComponent?.FreeAnyAlivePrey();
            Debug.Log("Attack performed on target that was already dead");
            return false;
        }        
        int modifiedDamage = CalculateDamageWithResistance(damage, damageType);
        UnitSprite.DisplayDamage(modifiedDamage, spellDamage);
        SubtractHealth(modifiedDamage);
        if ((State.Rand.NextDouble() > Unit.HealthPct))
        {
            if (Unit.HasTrait(Traits.Cowardly))
            {
                Surrendered = true;
                Movement = 0;
                if (State.Rand.NextDouble() <= Config.SurrenderedPredAutoRegur)
                {
                    PredatorComponent?.FreeAnyAlivePrey();
                }
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{Unit.Name} was a coward and surrendered");
            }
            if (Unit.HasTrait(Traits.TurnCoat))
            {
                State.GameManager.TacticalMode.SwitchAlignment(this);
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{Unit.Name} switched sides when they were hit");
            }
        }
        if (Unit.HasTrait(Traits.Berserk) && GoneBerserk == false)
        {
            if (Unit.HealthPct < .5f)
            {
                GoneBerserk = true;
                Unit.ApplyStatusEffect(StatusEffectType.Berserk, 1, 3);
            }
        }
        if ((canKill == false && Unit.IsDead) || (Config.AutoSurrender && Unit.IsDead && State.Rand.NextDouble() < Config.AutoSurrenderChance && Surrendered == false && Unit.HasTrait(Traits.Fearless) == false && !KilledByDigestion && Unit.GetStatusEffect(StatusEffectType.Respawns) == null))
        {
            Unit.Health = 1;
            Surrendered = true;
            Movement = 0;
            if (Config.AutoSurrender && Config.SurrenderedCanConvert && Unit.CanBeConverted())
            {
                if (State.Rand.NextDouble() <= Config.AutoSurrenderDefectChance)
                {
                    State.GameManager.TacticalMode.SwitchAlignment(this);
                    AIAvoidEat = 2;
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{Unit.Name} switched sides when they surrendered");
                    if ((Config.GoddessMercy == GoddessMercy.Both) || (Config.GoddessMercy == GoddessMercy.DefectorOnly))
                    {
                        Unit.Health = Unit.MaxHealth;
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"A light shines from above on {Unit.Name}");
                    }
                }
                else if ((Config.GoddessMercy == GoddessMercy.Both) || (Config.GoddessMercy == GoddessMercy.LoyalOnly))
                {
                    Unit.Health = Unit.MaxHealth;
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"A light shines from above on {Unit.Name} for their loyalty");
                }
            }
            else if ((Config.GoddessMercy == GoddessMercy.Both) || (Config.GoddessMercy == GoddessMercy.LoyalOnly))
            {
                Unit.Health = Unit.MaxHealth;
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"A light shines from above on {Unit.Name} for their loyalty");
            }
            if (State.Rand.NextDouble() <= Config.SurrenderedPredAutoRegur)
            {
                PredatorComponent?.FreeAnyAlivePrey();
            }
        }

        if (Config.DamageNumbers == false && !State.GameManager.TacticalMode.turboMode)
        {
            Mode = DisplayMode.Injured;
            animationUpdateTime = 1.0F;
        }
        if (Unit.IsDead)
        {
            State.GameManager.TacticalMode.DirtyPack = true;
            Targetable = false;
            Surrendered = true;
            if (Config.VisibleCorpses && Unit.Race != Race.Erin)
            {
                float angle = 40 + State.Rand.Next(280);
                UnitSprite.transform.rotation = Quaternion.Euler(0, 0, angle);
                spriteLayerOffset = State.GameManager.TacticalMode.GetNextCorpseLayer();
            }
            else
            {
                Visible = false;
            }

            PredatorComponent?.FreeAnyAlivePrey();
        }
        return true;
    }

    public void DigestCheck(string feedtype = "")
    {
        if (Unit.IsDead == false)
            PredatorComponent?.Digest(feedtype);
    }

    //public List<Actor_Unit> BirthCheck()
    //{
    //    List<Actor_Unit> released = null;
    //    if (Unit.IsDead == false)
    //        released = PredatorComponent?.Birth();
    //    if (released == null)
    //    {
    //        released = new List<Actor_Unit>();
    //    }
    //    return released;
    //}

    public Vec2i GetPos(int i)
    {
        switch (i)
        {
            case 0:
                return new Vec2i(Position.x, Position.y + 1);
            case 1:
                return new Vec2i(Position.x + 1, Position.y + 1);
            case 2:
                return new Vec2i(Position.x + 1, Position.y);
            case 3:
                return new Vec2i(Position.x + 1, Position.y - 1);
            case 4:
                return new Vec2i(Position.x, Position.y - 1);
            case 5:
                return new Vec2i(Position.x - 1, Position.y - 1);
            case 6:
                return new Vec2i(Position.x - 1, Position.y);
            case 7:
                return new Vec2i(Position.x - 1, Position.y + 1);
        }
        return Position;
    }


    public static bool CheckForActor(List<Actor_Unit> actors, Vec2i p)
    {
        for (int i = 0; i < actors.Count; i++)
        {
            if (actors[i].Position.Matches(p) && actors[i].Targetable == true)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsErect()
    {
        if (Unit.DickSize < 0)
            return false;
        if (Config.ErectionsFromVore)
        {
            if (PredatorComponent?.Fullness > 0)
                return true;
        }
        if (Config.ErectionsFromCockVore)
        {
            if (PredatorComponent?.BallsFullness > 0)
                return true;
        }
        return false;
    }

    public float WillCheckOdds(Actor_Unit actor, Actor_Unit target)
    {
        if (target.Unit.IsDead)
        {
            return 0;
        }
        float ratio = (float)target.Unit.GetStat(Stat.Will) / actor.Unit.GetStat(Stat.Will);

        if (ratio > 5)
            ratio = 5;
        return ratio / 25;
    }
    public float CritCheck(Actor_Unit actor, Actor_Unit target)
    {
        if (target.Unit.IsDead)
        {
            return 0;
        }
        float ratio = 0;
        ratio = (float)((actor.Unit.GetStat(Stat.Dexterity) + actor.Unit.GetStat(Stat.Strength)) /
            (target.Unit.GetStat(Stat.Endurance) * target.Unit.GetStat(Stat.Endurance) + target.Unit.GetStat(Stat.Will)));

        if (Config.BaseCritChance > ratio)
            ratio = Config.BaseCritChance;

        return ratio;
    }

    public float GrazeCheck(Actor_Unit actor, Actor_Unit target)
    {
        if (target.Unit.IsDead)
        {
            return 0;
        }

        int actor_stats = (actor.Unit.GetStat(Stat.Dexterity) + actor.Unit.GetStat(Stat.Strength)) / 2;
        float ratio = target.Unit.GetStat(Stat.Agility) / (actor_stats * actor_stats);

        if (Config.BaseGrazeChance > ratio)
            ratio = Config.BaseGrazeChance;

        return ratio;
    }

    internal bool CastSpell(Spell spell, Actor_Unit target)
    {
        if (Unit.SpendMana(spell.ManaCost) == false && spell.IsFree != true)
            return false;
        Unit.GiveExp(1);
        //if (target != null && target.DefendSpellCheck(spell, this, out float chance) == false)
        //    return false;

        State.GameManager.SoundManager.PlaySpellCast(spell, this);
        if (target?.UnitSprite != null)
            State.GameManager.SoundManager.PlaySpellHit(spell, target.UnitSprite.transform.position);

        if (Unit.TraitBoosts.SpellAttacks > 1)
        {
            int movementFraction = 1 + MaxMovement() / Unit.TraitBoosts.SpellAttacks;
            if (Movement > movementFraction)
                Movement -= movementFraction;
            else
                Movement = 0;
        }
        else
            Movement = 0;
        return true;
    }

    internal void CastMawWithLocation(Spell spell, Actor_Unit target, Vec2i targetArea = null)
    {
        PreyLocation preyLocation = PreyLocation.stomach;
        var possibilities = new Dictionary<string, PreyLocation>();
        possibilities.Add("Maw", PreyLocation.stomach);
        if (PredatorComponent.CanAnalVore()) possibilities.Add("Anus", PreyLocation.anal);
        if (PredatorComponent.CanBreastVore()) possibilities.Add("Breast", PreyLocation.breasts);
        if (PredatorComponent.CanCockVore()) possibilities.Add("Cock", PreyLocation.balls);
        if (PredatorComponent.CanUnbirth()) possibilities.Add("Pussy", PreyLocation.womb);

        if (State.GameManager.TacticalMode.IsPlayerInControl && State.GameManager.CurrentScene == State.GameManager.TacticalMode && possibilities.Count > 1)
        {
            var box = State.GameManager.CreateOptionsBox();
            box.SetData($"Where do you want to put your prey?", "Maw", () => CastMaw(spell, target, PreyLocation.stomach, targetArea), possibilities.Keys.ElementAtOrDefault(1), () => CastMaw(spell, target, possibilities.Values.ElementAtOrDefault(1), targetArea), possibilities.Keys.ElementAtOrDefault(2), () => CastMaw(spell, target, possibilities.Values.ElementAtOrDefault(2), targetArea), possibilities.Keys.ElementAtOrDefault(3), () => CastMaw(spell, target, possibilities.Values.ElementAtOrDefault(3), targetArea), possibilities.Keys.ElementAtOrDefault(4), () => CastMaw(spell, target, possibilities.Values.ElementAtOrDefault(4), targetArea));
        }
        else
        {
            preyLocation = possibilities.Values.ToList()[State.Rand.Next(possibilities.Count)];
            CastMaw(spell, target, preyLocation, targetArea);
        }
    }
    internal void CastMaw(Spell spell, Actor_Unit target, PreyLocation preyLocation, Vec2i targetArea = null )
    {
        if (Unit.Predator == false)
            return;
        if (Unit.SpendMana(spell.ManaCost) == false && spell.IsFree != true)
            return;

        State.GameManager.SoundManager.PlaySpellCast(spell, this);
        if (target != null)
        {
            if (spell.AreaOfEffect > 0)
            {
                foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(target.Position, spell.AreaOfEffect))
                {
                    if (PredatorComponent.MagicConsume(spell, splashTarget, preyLocation))
                    {
                        State.GameManager.SoundManager.PlaySpellHit(spell, splashTarget.UnitSprite.transform.position);
                    }
                }
            }
            else
            {
                if (PredatorComponent.MagicConsume(spell, target, preyLocation))
                {
                    State.GameManager.SoundManager.PlaySpellHit(spell, target.UnitSprite.transform.position);
                }
            }
        }
        else if (targetArea != null && spell.AreaOfEffect > 0)
        {
            foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(targetArea, spell.AreaOfEffect))
            {
                if (PredatorComponent.MagicConsume(spell, splashTarget, preyLocation))
                {
                    State.GameManager.SoundManager.PlaySpellHit(spell, splashTarget.UnitSprite.transform.position);
                }
            }
        }
        if (Unit.TraitBoosts.SpellAttacks > 1)
        {
            int movementFraction = 1 + MaxMovement() / Unit.TraitBoosts.SpellAttacks;
            if (Movement > movementFraction)
                Movement -= movementFraction;
            else
                Movement = 0;
        }
        else
            Movement = 0;
    }
    internal void CastOffensiveSpell(DamageSpell spell, Actor_Unit target, Vec2i targetArea = null)
    {
        if (Unit.SpendMana(spell.ManaCost) == false && spell.IsFree != true)
            return;
        State.GameManager.SoundManager.PlaySpellCast(spell, this);

        if (target != null)
        {
            if (spell.AreaOfEffect > 0 && spell.AOEType == AreaOfEffectType.Full)
            {
                foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(target.Position, spell.AreaOfEffect).Where(s => s.Unit.IsDead == false))
                {
                    splashTarget.DefendDamageSpell(spell, this, spell.Damage(this, splashTarget));
                    CheckDead(splashTarget);
                }
                State.GameManager.SoundManager.PlaySpellHit(spell, target.UnitSprite.transform.position);
            }
            else if (spell.AOEType == AreaOfEffectType.FixedPattern)
            {
                foreach (var splashTarget in TacticalUtilities.UnitsWithinPattern(target.Position, spell.Pattern).Where(s => s.Unit.IsDead == false))
                {
                    splashTarget.DefendDamageSpell(spell, this, spell.Damage(this, splashTarget));
                    CheckDead(splashTarget);
                }
                State.GameManager.SoundManager.PlaySpellHit(spell, target.UnitSprite.transform.position);
            }
            else if (spell.AOEType == AreaOfEffectType.RotatablePattern)
            {                             
              
                foreach (var splashTarget in TacticalUtilities.UnitsWithinRotatingPattern(target.Position, spell.Pattern, TacticalUtilities.GetRotatingOctant(Position, target.Position)).Where(s => s.Unit.IsDead == false))
                {
                    splashTarget.DefendDamageSpell(spell, this, spell.Damage(this, splashTarget));
                    CheckDead(splashTarget);
                }
                State.GameManager.SoundManager.PlaySpellHit(spell, target.UnitSprite.transform.position);
            }
            else
            {
                if (target.DefendDamageSpell(spell, this, spell.Damage(this, target)))
                {
                    State.GameManager.SoundManager.PlaySpellHit(spell, target.UnitSprite.transform.position);
                }
                CheckDead(target);
            }
        }
        else if (targetArea != null && spell.AreaOfEffect > 0 && spell.AOEType == AreaOfEffectType.Full)
        {
            foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(targetArea, spell.AreaOfEffect).Where(s => s.Unit.IsDead == false))
            {
                splashTarget.DefendDamageSpell(spell, this, spell.Damage(this, splashTarget));
                CheckDead(splashTarget);
            }
            State.GameManager.SoundManager.PlaySpellHit(spell, targetArea);
        }
        else if (targetArea != null && spell.AOEType == AreaOfEffectType.FixedPattern)
        {
            foreach (var splashTarget in TacticalUtilities.UnitsWithinPattern(targetArea, spell.Pattern).Where(s => s.Unit.IsDead == false))
            {
                splashTarget.DefendDamageSpell(spell, this, spell.Damage(this, splashTarget));
                CheckDead(splashTarget);
            }
            State.GameManager.SoundManager.PlaySpellHit(spell, targetArea);
        }
        else if (targetArea != null && (spell.AOEType == AreaOfEffectType.RotatablePattern))
        {
            foreach (var splashTarget in TacticalUtilities.UnitsWithinRotatingPattern(targetArea, spell.Pattern, TacticalUtilities.GetRotatingOctant(Position, targetArea)).Where(s => s.Unit.IsDead == false))
            {
                splashTarget.DefendDamageSpell(spell, this, spell.Damage(this, splashTarget));
                CheckDead(splashTarget);
            }
            State.GameManager.SoundManager.PlaySpellHit(spell, targetArea);
        }
        if (Unit.TraitBoosts.SpellAttacks > 1)
        {
            int movementFraction = 1 + MaxMovement() / Unit.TraitBoosts.SpellAttacks;
            if (Movement > movementFraction)
                Movement -= movementFraction;
            else
                Movement = 0;
        }
        else
            Movement = 0;
        void CheckDead(Actor_Unit hitTarget)
        {
            if (hitTarget.Unit.IsDead)
            {
                State.GameManager.TacticalMode.TacticalStats.RegisterKill(spell, Unit.Side);
                KillUnit(hitTarget, spell);
            }
        }
    }

    internal bool CastStatusSpell(StatusSpell spell, Actor_Unit target, Vec2i targetArea = null, Stat stat = Stat.Mind)
    {
        if (Unit.SpendMana(spell.ManaCost) == false && spell.IsFree != true)
            return false;

        bool hit = false;

        State.GameManager.SoundManager.PlaySpellCast(spell, this);

        if (target != null)
        {
            if (spell.AreaOfEffect > 0)
            {
                hit = true;
                foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(target.Position, spell.AreaOfEffect).Where(s => s.Unit.IsDead == false))
                {
                    splashTarget.DefendStatusSpell(spell, this);
                }
                State.GameManager.SoundManager.PlaySpellHit(spell, target.UnitSprite.transform.position);
            }
            else
            {
                if (target.DefendStatusSpell(spell, this))
                {
                    hit = true;
                    State.GameManager.SoundManager.PlaySpellHit(spell, target.UnitSprite.transform.position);
                }
            }
        }
        else if (targetArea != null && spell.AreaOfEffect > 0)
        {
            foreach (var splashTarget in TacticalUtilities.UnitsWithinTiles(targetArea, spell.AreaOfEffect).Where(s => s.Unit.IsDead == false))
            {
                hit = true;
                splashTarget.DefendStatusSpell(spell, this);
            }
            State.GameManager.SoundManager.PlaySpellHit(spell, targetArea);
        }

        if (Unit.TraitBoosts.SpellAttacks > 1)
        {
            int movementFraction = 1 + MaxMovement() / Unit.TraitBoosts.SpellAttacks;
            if (Movement > movementFraction)
                Movement -= movementFraction;
            else
                Movement = 0;
        }
        else
            Movement = 0;

        return hit;
    }

    internal bool CastBind(Actor_Unit t)
    {
        var spell = SpellList.Bind;
        if (t.Unit.Type != UnitType.Summon)
            return false;
        var binder = TacticalUtilities.Units.Where(a => a.Unit.BoundUnit?.Unit == t.Unit).FirstOrDefault();
        if (binder?.Unit.FixedSide == Unit.FixedSide) return false;
        if (Unit.SpendMana(spell.ManaCost) == false && spell.IsFree != true) return false;


        if (binder != null)
        {
            if (binder.Unit.GetStat(Stat.Mind) > Unit.GetStat(Stat.Mind))
            {
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{Unit.Name}</b> tries to bind <b>{LogUtilities.ApostrophizeWithOrWithoutS(binder.Unit.Name)}</b> bound summon, <b>{t.Unit.Name}</b>, but <b>{LogUtilities.ApostrophizeWithOrWithoutS(binder.Unit.Name)}</b> magic is stronger");
            }
            else if (binder.Unit.GetStat(Stat.Mind) < Unit.GetStat(Stat.Mind))
            {
                State.GameManager.SoundManager.PlaySpellCast(spell, this);
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"With {LogUtilities.GPPHis(Unit)} superior magic <b>{Unit.Name}</b> wrests control over the summoned {InfoPanel.RaceSingular(t.Unit)} from <b>{binder.Unit.Name}</b>.");
                binder.Unit.BoundUnit = null;
                Unit.BoundUnit = t;

                if (t.Unit.Side != Unit.Side) State.GameManager.TacticalMode.SwitchAlignment(t);
                if (!t.Unit.HasTrait(Traits.Untamable))
                    t.Unit.FixedSide = Unit.FixedSide;
                t.Movement = t.CurrentMaxMovement();
                var actorCharm = Unit.GetStatusEffect(StatusEffectType.Charmed) ?? Unit.GetStatusEffect(StatusEffectType.Hypnotized);
                if (actorCharm != null)
                {
                    t.Unit.ApplyStatusEffect(StatusEffectType.Charmed, actorCharm.Strength, actorCharm.Duration);
                }
            }
            else
            {
                int outcome = State.Rand.Next(4);
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{Unit.Name}</b> and <b>{binder.Unit.Name}</b> both exert their magic for control over the summoned <b>{t.Unit.Name}</b>, but they are evenly matched! The energy crackles...");
                if (outcome == 3)
                {
                    State.GameManager.SoundManager.PlayMisc("unbound", this);
                    var obj = Object.Instantiate(State.GameManager.TacticalEffectPrefabList.ShunGokuSatsu);
                    obj.transform.SetPositionAndRotation(new Vector3(t.Position.x, t.Position.y), new Quaternion());
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"Suddenly, there is a flash of light and both casters stagger for a moment. What happened?.");
                    t.Unit.Type = UnitType.Adventurer;
                    binder.Unit.BoundUnit = null;
                    t.Unit.Name += " The Unbound";
                    t.Unit.AddPermanentTrait(Traits.PeakCondition);
                    int unusedSide = 703;
                    while (State.World.AllActiveEmpires?.Any(emp => emp.Side == unusedSide) ?? false)
                    {
                        unusedSide++;
                    }
                    t.Unit.FixedSide = unusedSide;
                    State.GameManager.TacticalMode.SwitchAlignment(t);
                    t.Unit.AddPermanentTrait(Traits.Untamable);
                    t.Unit.AddPermanentTrait(Traits.Large);
                    t.Unit.GiveRawExp((int)(binder.Unit.Experience * 0.50 + Unit.Experience * 0.50));
                    StrategicUtilities.SpendLevelUps(t.Unit);
                    t.Unit.Health = t.Unit.MaxHealth;
                    t.Unit.RestoreMana(t.Unit.MaxMana);
                    if (binder.sidesAttackedThisBattle == null) binder.sidesAttackedThisBattle = new List<int>();
                    binder.sidesAttackedThisBattle.Add(unusedSide);
                    if (this.sidesAttackedThisBattle == null) this.sidesAttackedThisBattle = new List<int>();
                    this.sidesAttackedThisBattle.Add(unusedSide);
                }
                else if (outcome == 2)
                {
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"...But it looks like the Binding spell worked after all.");
                    State.GameManager.SoundManager.PlaySpellCast(spell, this);
                    binder.Unit.BoundUnit = null;
                    Unit.BoundUnit = t;

                    if (t.Unit.Side != Unit.Side) State.GameManager.TacticalMode.SwitchAlignment(t);
                    if (!t.Unit.HasTrait(Traits.Untamable))
                        t.Unit.FixedSide = Unit.FixedSide;
                    t.Movement = t.CurrentMaxMovement();
                    var actorCharm = Unit.GetStatusEffect(StatusEffectType.Charmed) ?? Unit.GetStatusEffect(StatusEffectType.Hypnotized);
                    if (actorCharm != null)
                    {
                        t.Unit.ApplyStatusEffect(StatusEffectType.Charmed, actorCharm.Strength, actorCharm.Duration);
                    }
                }
                else if (outcome == 1)
                {
                    State.GameManager.SoundManager.PlaySpellCast(spell, this);
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"...But it looks nothing's changed...");
                }
                else if (outcome == 0)
                {
                    State.GameManager.SoundManager.PlayMisc("unbound", this);
                    var obj = Object.Instantiate(State.GameManager.TacticalEffectPrefabList.ShunGokuSatsu);
                    obj.transform.SetPositionAndRotation(new Vector3(t.Position.x, t.Position.y), new Quaternion());
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"Suddenly, there is a flash of light and both casters stagger for a moment. What happened?.");
                    t.Unit.Type = UnitType.Adventurer;
                    t.Surrendered = true;
                    t.Damage(9999999, true, true);
                    t.Visible = false;
                    t.Unit.Name += " The Banished";
                }

            }
        }
        else
        {
            State.GameManager.SoundManager.PlaySpellCast(spell, this);

            Unit.BoundUnit = t;

            if (t.Unit.Side != Unit.Side) State.GameManager.TacticalMode.SwitchAlignment(t);
            if (!t.Unit.HasTrait(Traits.Untamable))
                t.Unit.FixedSide = Unit.FixedSide;
            t.Movement = t.CurrentMaxMovement();
            var actorCharm = Unit.GetStatusEffect(StatusEffectType.Charmed) ?? Unit.GetStatusEffect(StatusEffectType.Hypnotized);
            if (actorCharm != null)
            {
                t.Unit.ApplyStatusEffect(StatusEffectType.Charmed, actorCharm.Strength, actorCharm.Duration);
            }
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{Unit.Name}</b> has bound <b>{t.Unit.Name}</b> to {LogUtilities.GPPHis(Unit)} will.");
        }

        if (Unit.TraitBoosts.SpellAttacks > 1)
        {
            int movementFraction = 1 + MaxMovement() / Unit.TraitBoosts.SpellAttacks;
            if (Movement > movementFraction)
                Movement -= movementFraction;
            else
                Movement = 0;
        }
        else
            Movement = 0;

        return true;
    }

    internal bool SummonBound(Vec2i l)
    {
        var spell = SpellList.Bind;
        if (Unit.BoundUnit == null)
            return false;

        if (TacticalUtilities.Units.Contains(Unit.BoundUnit) && !Unit.BoundUnit.Unit.IsDead)
            return false;
        if (Unit.SpendMana(spell.ManaCost) == false && spell.IsFree != true) return false;

        State.GameManager.SoundManager.PlaySpellCast(SpellList.Summon, this);

        if (TacticalUtilities.Units.Contains(Unit.BoundUnit))
        {
            TacticalUtilities.Reanimate(l, Unit.BoundUnit, Unit);
            Unit.BoundUnit.Unit.Health = Unit.BoundUnit.Unit.MaxHealth;
        }
        else
        {
            StrategicUtilities.SpendLevelUps(Unit.BoundUnit.Unit);
            var target = State.GameManager.TacticalMode.AddUnitToBattle(Unit.BoundUnit.Unit, l);
            target.Visible = true;
            target.Targetable = true;
            target.SelfPrey = null;
            target.Surrendered = false;
            target.Unit.Health = target.Unit.MaxHealth;
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{Unit.Name}</b> re-summoned <b>{Unit.BoundUnit.Unit.Name}</b>.");
        }

        if (Unit.TraitBoosts.SpellAttacks > 1)
        {
            int movementFraction = 1 + MaxMovement() / Unit.TraitBoosts.SpellAttacks;
            if (Movement > movementFraction)
                Movement -= movementFraction;
            else
                Movement = 0;
        }
        else
            Movement = 0;

        return true;
    }

    internal void ShareTrait(Traits trait,Traits maxTrait = Traits.Infiltrator)
    {
        if (trait < maxTrait && !TraitsMethods.IsRaceModifying(trait))
        {
            if (!Unit.HasTrait(trait))
                Unit.AddSharedTrait(trait);
        }
    }

    public bool ChangeRaceAny(Unit template, bool permanent, bool isPrey)
    {
        if (Unit.HiddenRace == Unit.Race)
        {
            TacticalGraphicalEffects.CreateSmokeCloud(Position, Unit.GetScale() / 2);
            Unit.HideRace(template.Race, template);
            foreach (Traits trait in template.GetTraits)
            {
                if ((!Unit.HasTrait(trait) || Unit.HasSharedTrait(trait)) && !TraitsMethods.IsRaceModifying(trait))
                    if (permanent)
                        Unit.AddPermanentTrait(trait);
                    else
                        ShareTrait(trait, TraitsMethods.LastTrait());
            }
            AnimationController = new AnimationController();
            Unit.ReloadTraits();
            Unit.InitializeTraits();
            ReloadSpellTraits();
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{Unit.Name} shifted form to resemble {template.Name}");
            Unit.FixedSide = Unit.Side;
            Unit.Side = template.Side;
            Unit.hiddenFixedSide = true;
            PredatorComponent?.UpdateFullness();
            return true;
        }
        return false;
    }

    public void ChangeRacePrey()
    {
        if (Unit.HiddenRace == Unit.Race)
        {
            bool isDead = true;
            if (Unit.HasTrait(Traits.GreaterChangeling))
            {
                isDead = false;
            }
            PredatorComponent?.ChangeRaceAuto(isDead,true);
        }
    }

    public void RevertRace()
    {
        if (Unit.HiddenRace != Unit.Race)
        {
            TacticalGraphicalEffects.CreateSmokeCloud(Position, Unit.GetScale() / 2);
            Unit.UnhideRace();
            Unit.SpawnRace = Race.none;
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{Unit.Name} shifted back to normal");
            Unit.Side = Unit.FixedSide;
            Unit.FixedSide = -1;
            Unit.hiddenFixedSide = false;
            PredatorComponent?.ResetTemplate();
            Unit.ResetPersistentSharedTraits();
            AnimationController = new AnimationController();
            Unit.ReloadTraits();
            Unit.InitializeTraits();
            ReloadSpellTraits();
            PredatorComponent?.UpdateFullness();
        }
    }
    
    internal void AddCorruption(int amount, int side)
    {
        if (!Unit.HasTrait(Traits.Corruption)) { 
            Corruption += amount;
            if (Corruption >= Unit.GetStatTotal() + Unit.GetStat(Stat.Will))
            {
                Unit.AddPermanentTrait(Traits.Corruption);
                Corruption = 0;
                if (!Unit.HasTrait(Traits.Untamable))
                    Unit.FixedSide = side;
                Unit.hiddenFixedSide = true;
                sidesAttackedThisBattle = new List<int>();
            }
        } else
            Corruption = 0;
    }

    internal void AddPossession(Actor_Unit possessor)
    {
        if (this.Unit.FixedSide != possessor.Unit.FixedSide)
        {
            this.Possessed = this.Possessed + possessor.Unit.GetStatTotal() + possessor.Unit.GetStat(Stat.Mind);
        }
        CheckPossession(possessor);
    }

    internal void RemovePossession(Actor_Unit possessor)
    {
        this.Possessed = this.Possessed - possessor.Unit.GetStatTotal() - possessor.Unit.GetStat(Stat.Mind);
        if (this.Possessed <= 0)
            this.Possessed = 0;
        CheckPossession(possessor);
    }

    internal bool CheckPossession(Actor_Unit possessor)
    {
        if (this.Possessed + this.Corruption >= this.Unit.GetStatTotal() + this.Unit.GetStat(Stat.Will))
        {
            if (this.Unit.Controller != possessor.Unit)
            {
                if (!this.Unit.HasTrait(Traits.Untamable))
                    this.Unit.Controller = possessor.Unit;
                this.Unit.hiddenFixedSide = true;
                sidesAttackedThisBattle = new List<int>();
            }
            return true;
        }
        else
        {
            if (this.Unit.Controller == possessor.Unit)
            {
                this.Unit.Controller = null;
                this.Unit.hiddenFixedSide = false;
            }
        }
        return false;
    }

    internal void Shapeshift(Unit shape)
    {
        if (Unit == shape)
            return;
        TacticalGraphicalEffects.CreateSmokeCloud(Position, Unit.GetScale()/2);
        Unit.UpdateShapeExpAndItems();
        MiscUtilities.DelayedInvoke(() =>
        {
            shape.ShifterShapes = Unit.ShifterShapes;
            shape.Side = Unit.Side;
            Unit = shape;
            Unit.hiddenFixedSide = false;

            UnitSprite.UpdateSprites(this, true);
            AnimationController = new AnimationController();
            Unit.ReloadTraits();
            Unit.InitializeTraits();
        }, 0.4f);
        
    }
}
