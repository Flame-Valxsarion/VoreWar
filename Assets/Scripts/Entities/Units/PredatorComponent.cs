using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

interface IVoreCallback
{
    int ProcessingPriority { get; }//lower runs first
    bool IsPredTrait { get; }//is the trait triggered from the pred or the prey side
    /* 
     * use the boolean return to skip lower priority traits, eg. if the unit has already
     *  been removed and as part of a trait like digestive conversion, it shouldn't run the digestive rebirth trait it stole with extraction 
    */
    bool OnSwallow(Prey preyUnit, Actor_Unit predUnit, PreyLocation location);//any time prey is added (living or dead)
    bool OnRemove(Prey preyUnit, Actor_Unit predUnit, PreyLocation location);//any time prey is removed (living or dead)
    bool OnDigestion(Prey preyUnit, Actor_Unit predUnit, PreyLocation location);//any time living prey is digested but won't die yet
    bool OnFinishDigestion(Prey preyUnit, Actor_Unit predUnit, PreyLocation location);//right before unit dies
    bool OnDigestionKill(Prey preyUnit, Actor_Unit predUnit, PreyLocation location);//right after unit dies
    bool OnAbsorption(Prey preyUnit, Actor_Unit predUnit, PreyLocation location);//any time a dead unit is being absorbed
    bool OnFinishAbsorption(Prey preyUnit, Actor_Unit predUnit, PreyLocation location);//unit is done absorbing, but not removed yet
}

interface IVoreRestrictions
{
    /* Notes: 
     * CheckVore needs to handle a null target
     * Only specialVore types can be affected, disableing Vore overall will take more work
     */
    bool CheckVore(Actor_Unit actor, Actor_Unit target, PreyLocation location);
}

public enum PreyLocation
{
    breasts,
    balls,
    stomach,
    stomach2,
    womb,
    tail,
    anal,
    leftBreast,
    rightBreast
}

static class PreyLocationMethods
{

    static public bool IsGenital(PreyLocation location)
    {
        switch (location)
        {
            case PreyLocation.womb:
            case PreyLocation.balls:
                return true;
            default:
                return false;
        }
    }
}

public class PredatorComponent
{
    [OdinSerialize]
    List<Prey> prey;
    [OdinSerialize]
    List<Prey> womb;
    [OdinSerialize]
    List<Prey> breasts;
    [OdinSerialize]
    List<Prey> leftBreast;
    [OdinSerialize]
    List<Prey> rightBreast;
    [OdinSerialize]
    List<Prey> balls;
    [OdinSerialize]
    List<Prey> stomach;
    [OdinSerialize]
    List<Prey> stomach2;
    [OdinSerialize]
    List<Prey> tail;
    [OdinSerialize]
    List<Prey> deadPrey;

    Transition StomachTransition;
    Transition BallsTransition;
    Transition LeftBreastTransition;
    Transition RightBreastTransition;

    public static PreyLocation[] preyLocationOrder =
    {
        PreyLocation.stomach,
        PreyLocation.stomach2,
        PreyLocation.womb,
        PreyLocation.balls,
        PreyLocation.breasts,
        PreyLocation.leftBreast,
        PreyLocation.rightBreast,
        PreyLocation.tail,
        PreyLocation.anal
    };

    internal struct Transition
    {
        internal float transitionTime;
        internal float transitionLength;
        internal float transitionStart;
        internal float transitionEnd;

        public Transition(float transitionLength, float transitionStart, float transitionEnd)
        {
            transitionTime = 0;
            this.transitionLength = transitionLength;
            this.transitionStart = transitionStart;
            this.transitionEnd = transitionEnd;
        }
    }

    [OdinSerialize]
    public float VisibleFullness { get; set; }
    [OdinSerialize]
    public float Fullness { get; set; }
    [OdinSerialize]
    public float BreastFullness { get; set; }
    [OdinSerialize]
    public float BallsFullness { get; set; }
    [OdinSerialize]
    public float TailFullness { get; set; }
    [OdinSerialize]
    public float Stomach2ndFullness { get; set; } // A second stomach fullness used for units with the trait DualStomach.
    [OdinSerialize]
    public float CombinedStomachFullness { get; set; } // Used when DualStomach unit has no separate sprites for second one or they are turned off.
    [OdinSerialize]
    public float LeftBreastFullness { get; set; }
    [OdinSerialize]
    public float RightBreastFullness { get; set; }
    /// <summary>
    /// Only includes the stomach, and not the womb
    /// </summary>
    [OdinSerialize]
    public float ExclusiveStomachFullness { get; set; }
    /// <summary>
    /// Only includes the womb and not the stomach
    /// </summary>
    [OdinSerialize]
    public float WombFullness { get; set; }
    [OdinSerialize]
    Actor_Unit actor;
    [OdinSerialize]
    Unit unit;
    [OdinSerialize]
    public int birthStatBoost;
    [OdinSerialize]
    internal Prey template = null;


    [OdinSerialize]
    private List<IVoreRestrictions> _voreRestrictions = null;
    internal List<IVoreRestrictions> VoreRestrictions
    {
        get
        {
            if(_voreRestrictions == null)
            {
                _voreRestrictions = new List<IVoreRestrictions>();
                if (Races.GetRace(unit.Race) is IVoreRestrictions restriction1)
                    _voreRestrictions.Add(restriction1);
                foreach (Traits trait in unit.GetTraits.Where(t => (TraitList.GetTrait(t) != null) && TraitList.GetTrait(t) is IVoreRestrictions))
                {
                    IVoreRestrictions restriction = (IVoreRestrictions)TraitList.GetTrait(trait);
                    _voreRestrictions.Add(restriction);
                }
            }
            return _voreRestrictions;
        }
        set
        {
            if(value == null)
                _voreRestrictions = null;
        }
    }
    internal bool CanVore(PreyLocation location, Actor_Unit preyTarget = null)
    {
        if (!unit.CanVore(location))
            return false;
        foreach (IVoreRestrictions callback in VoreRestrictions)
            if (!callback.CheckVore(actor, preyTarget, location))
                return false;
        return true;
    }
    internal bool CanUnbirth(Actor_Unit preyTarget = null)
    {
        if (!unit.CanUnbirth)
            return false;
        foreach (IVoreRestrictions callback in VoreRestrictions)
            if (!callback.CheckVore(actor, preyTarget,PreyLocation.womb))
                return false;
        return true;
    }
    internal bool CanCockVore(Actor_Unit preyTarget = null)
    {
        if (!unit.CanCockVore)
            return false;
        foreach (IVoreRestrictions callback in VoreRestrictions)
            if (!callback.CheckVore(actor, preyTarget, PreyLocation.balls))
                return false;
        return true;
    }
    internal bool CanBreastVore(Actor_Unit preyTarget = null)
    {
        if (!unit.CanBreastVore)
            return false;
        foreach (IVoreRestrictions callback in VoreRestrictions)
            if (!callback.CheckVore(actor, preyTarget, PreyLocation.breasts))
                return false;
        return true;
    }
    internal bool CanAnalVore(Actor_Unit preyTarget = null)
    {
        if (!unit.CanAnalVore)
            return false;
        foreach (IVoreRestrictions callback in VoreRestrictions)
            if (!callback.CheckVore(actor, preyTarget, PreyLocation.anal))
                return false;
        return true;
    }
    internal bool CanTailVore(Actor_Unit preyTarget = null)
    {
        if (!unit.CanTailVore)
            return false;
        foreach (IVoreRestrictions callback in VoreRestrictions)
            if (!callback.CheckVore(actor, preyTarget, PreyLocation.tail))
                return false;
        return true;
    }
    public int AlivePrey { get; set; }

    private List<IVoreCallback> PopulateCallbacks(Prey preyUnit)
    {
        List<IVoreCallback> Callbacks = new List<IVoreCallback>();
        foreach (Traits trait in unit.GetTraits.Where(t => (TraitList.GetTrait(t) != null) && TraitList.GetTrait(t) is IVoreCallback))
        {
            IVoreCallback callback = (IVoreCallback)TraitList.GetTrait(trait);
            if (callback.IsPredTrait == true)
                Callbacks.Add(callback);
        }
        foreach (Traits trait in preyUnit.Unit.GetTraits.Where(t => (TraitList.GetTrait(t) != null) && TraitList.GetTrait(t) is IVoreCallback))
        {
            IVoreCallback callback = (IVoreCallback)TraitList.GetTrait(trait);
            if (callback.IsPredTrait == false)
                Callbacks.Add(callback);
        }
        //future idea: create status effect classes that can ALSO implement vore related callbacks
        return Callbacks;
    }

    // lumps prey in similar locations together

    public int PreyNearLocation(PreyLocation location, bool alive)
    {
        int prey = 0;
        switch (location)
        {
            case PreyLocation.balls:
                foreach (Prey unit in balls)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.stomach:
            case PreyLocation.stomach2:
            case PreyLocation.womb:
            case PreyLocation.anal:
            case PreyLocation.tail:
                foreach (Prey unit in stomach)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                foreach (Prey unit in stomach2)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                foreach (Prey unit in womb)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.leftBreast:
                foreach (Prey unit in leftBreast)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.rightBreast:
                foreach (Prey unit in rightBreast)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.breasts:
                foreach (Prey unit in breasts)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
        }
        return prey;
    }
    public int PreyInLocation(PreyLocation location, bool alive)
    {
        int prey = 0;
        switch (location)
        {
            case PreyLocation.balls:
                foreach (Prey unit in balls)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.stomach:
            case PreyLocation.anal:
                foreach (Prey unit in stomach)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.stomach2:
                foreach (Prey unit in stomach2)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.tail:
                foreach (Prey unit in tail)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.womb:
                foreach (Prey unit in womb)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.leftBreast:
                foreach (Prey unit in leftBreast)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.rightBreast:
                foreach (Prey unit in rightBreast)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
            case PreyLocation.breasts:
                foreach (Prey unit in breasts)
                {
                    if (unit.Unit.IsDead != alive)
                        prey += 1;
                }
                break;
        }
        return prey;
    }


    internal void UpdateAlivePrey()
    {
        AlivePrey = 0;
        foreach (Prey unit in prey)
        {
            if (unit.Unit.IsDead == false)
                AlivePrey += 1;
        }
    }

    internal List<Prey> GetDirectPrey()
    {
        return prey;
    }

    /// <summary>
    /// Gets all prey in this unit, including all sub prey and on down
    /// </summary>
    internal List<Prey> GetAllPrey()
    {
        List<Prey> allPrey = new List<Prey>();
        List<Prey> tempPrey = new List<Prey>();
        tempPrey.AddRange(prey);
        int counter = 0;
        while (tempPrey.Count > 0)
        {
            counter++;
            if (counter > 200)
            {
                Debug.LogWarning("Error Gathering all prey");
                break;
            }
            if (tempPrey[0].SubPrey != null)
                tempPrey.AddRange(tempPrey[0].SubPrey);
            allPrey.Add(tempPrey[0]);
            tempPrey.Remove(tempPrey[0]);
        }
        return allPrey;
    }

    public PredatorComponent(Actor_Unit actor, Unit unit)
    {
        this.actor = actor;
        this.unit = unit;
        Fullness = 0.0F;
        prey = new List<Prey>();
        womb = new List<Prey>();
        breasts = new List<Prey>();
        balls = new List<Prey>();
        stomach = new List<Prey>();
        stomach2 = new List<Prey>();
        tail = new List<Prey>();
        leftBreast = new List<Prey>();
        rightBreast = new List<Prey>();
        deadPrey = new List<Prey>();
        birthStatBoost = 0;

    }

    public void UpdateVersion()
    {
        if (tail == null)
            tail = new List<Prey>();
        if (stomach2 == null)
            stomach2 = new List<Prey>();
        if (leftBreast == null)
            leftBreast = new List<Prey>();
        if (rightBreast == null)
            rightBreast = new List<Prey>();
    }

    public int PreyCount => prey.Count;

    public int DigestingUnitCount
    {
        get
        {
            if (unit.HasTrait(Traits.FriendlyStomach) || unit.HasTrait(Traits.Endosoma))
                return prey.Where(s => actor.Unit.GetApparentSide(s.Unit) != s.Unit.FixedSide || s.Unit.IsDead).Count();
            return prey.Count;
        }
    }

    public bool OnlyOnePreyAndLiving() => prey.Count == 1 && prey[0].Unit.IsDead == false;

    public bool IsActorInPrey(Actor_Unit actor)
    {
        foreach (Prey p in prey)
        {
            if (p.Actor == actor)
                return true;
        }
        return false;
    }
    /// <summary>
    /// Checks if a specific Actor_Unit is in the specified part of the predator.
    /// </summary>
    /// <param name="unit">The Actor_Unit being looked for.</param>
    /// <param name="locations">The specified part of the predator to be checked. Can specify multiple.</param>
    /// <returns></returns>
    public bool IsUnitInPrey(Actor_Unit unit, params PreyLocation[] locations)
    {
        if (locations.Contains(PreyLocation.stomach))
            foreach (Prey p in stomach)
                if (p.Actor == unit) return true;
        if (locations.Contains(PreyLocation.stomach2))
            foreach (Prey p in stomach2)
                if (p.Actor == unit) return true;
        if (locations.Contains(PreyLocation.balls))
            foreach (Prey p in balls)
                if (p.Actor == unit) return true;
        if (locations.Contains(PreyLocation.breasts))
            foreach (Prey p in breasts)
                if (p.Actor == unit) return true;
        if (locations.Contains(PreyLocation.womb))
            foreach (Prey p in womb)
                if (p.Actor == unit) return true;
        if (locations.Contains(PreyLocation.tail))
            foreach (Prey p in tail)
                if (p.Actor == unit) return true;
        if (locations.Contains(PreyLocation.leftBreast))
            foreach (Prey p in leftBreast)
                if (p.Actor == unit) return true;
        if (locations.Contains(PreyLocation.rightBreast))
            foreach (Prey p in rightBreast)
                if (p.Actor == unit) return true;

        return false;
    }

    /// <summary>
    /// Used to check for whether a unit of specified race is present in eaten prey.
    /// </summary>
    /// <param name="race">The race which is being looked for.</param>
    /// <returns></returns>
    //public bool IsUnitOfSpecificationInPrey(Race race)
    //{
    //    foreach (Prey p in prey)
    //    {
    //        if (p.Unit.Race == race)
    //            return true;
    //    }
    //    return false;
    //}

    /// <summary>
    /// Used to check for whether a unit of specified race is present in eaten prey and either alive or dead depending on the boolean given.
    /// </summary>
    /// <param name="race">The race which is being looked for.</param>
    /// <param name="alive">True = Checking for living creatures. False = Checking for dead creatures.</param>
    /// <returns></returns>
    public bool IsUnitOfSpecificationInPrey(Race race, bool alive)
    {
        if (Config.RaceSpecificVoreGraphicsDisabled)
            return false;
        foreach (Prey p in prey)
        {
            if (p.Unit.Race == race)
                if (p.Unit.IsDead != alive)
                    return true;
        }
        return false;
    }

    /// <summary>
    /// Used to check for whether a unit of specified race is present inside specified part of the pred and either alive or dead depending on the boolean given.
    /// </summary>
    /// <param name="race">The race which is being looked for.</param>
    /// <param name="alive">True = Checking for living creatures. False = Checking for dead creatures.</param>
    /// <param name="locations">The specified part of the predator to be checked. Can specify multiple.</param>
    /// <returns></returns>
    public bool IsUnitOfSpecificationInPrey(Race race, bool alive, params PreyLocation[] locations)
    {
        if (Config.RaceSpecificVoreGraphicsDisabled)
            return false;
        if (locations.Contains(PreyLocation.stomach))
            foreach (Prey p in stomach)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        if (locations.Contains(PreyLocation.balls))
            foreach (Prey p in balls)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        if (locations.Contains(PreyLocation.womb))
            foreach (Prey p in womb)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        if (locations.Contains(PreyLocation.breasts))
            foreach (Prey p in breasts)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        if (locations.Contains(PreyLocation.tail))
            foreach (Prey p in tail)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        if (locations.Contains(PreyLocation.stomach2))
            foreach (Prey p in stomach2)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        if (locations.Contains(PreyLocation.leftBreast))
            foreach (Prey p in leftBreast)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        if (locations.Contains(PreyLocation.rightBreast))
            foreach (Prey p in rightBreast)
            {
                if (p.Unit.Race == race)
                    if (p.Unit.IsDead != alive)
                        return true;
            }
        return false;
    }

    public bool IsUnitOfSpecificationInPrey(Race race, params PreyLocation[] locations)
    {
        if (Config.RaceSpecificVoreGraphicsDisabled)
            return false;
        if (locations.Contains(PreyLocation.stomach))
            foreach (Prey p in stomach)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        if (locations.Contains(PreyLocation.balls))
            foreach (Prey p in balls)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        if (locations.Contains(PreyLocation.womb))
            foreach (Prey p in womb)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        if (locations.Contains(PreyLocation.breasts))
            foreach (Prey p in breasts)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        if (locations.Contains(PreyLocation.tail))
            foreach (Prey p in tail)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        if (locations.Contains(PreyLocation.stomach2))
            foreach (Prey p in stomach2)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        if (locations.Contains(PreyLocation.leftBreast))
            foreach (Prey p in leftBreast)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        if (locations.Contains(PreyLocation.rightBreast))
            foreach (Prey p in rightBreast)
            {
                if (p.Unit.Race == race)
                    return true;
            }
        return false;
    }

    public void FreeAnyAlivePrey()
    {
        List<Prey> preyUnits = new List<Prey>();
        preyUnits.AddRange(prey);
        while (preyUnits.Any())
        {
            if (preyUnits[0].Unit.IsDead == false)
            {
                State.GameManager.TacticalMode.TacticalStats.RegisterFreed(unit.Side);
                TacticalUtilities.Log.RegisterFreed(unit, preyUnits[0].Unit, Location(preyUnits[0]));
                preyUnits[0].Predator.PredatorComponent.FreePrey(preyUnits[0], true);
                preyUnits.RemoveAt(0);
            }
            else
            {
                if (preyUnits[0].SubPrey != null)
                    foreach (Prey subUnit in preyUnits[0].SubPrey)
                    {
                        preyUnits.Add(subUnit);
                    }
                preyUnits.RemoveAt(0);
            }
        }
    }

    internal void PurgePrey()
    {
        FreeAnyAlivePrey();
        prey.Clear();
        womb.Clear();
        breasts.Clear();
        leftBreast.Clear();
        rightBreast.Clear();
        balls.Clear();
        stomach.Clear();
        stomach2.Clear();
        tail.Clear();
    }

    public Vec2i GetCurrentLocation()
    {
        Actor_Unit located = actor;
        for (int i = 0; i < 200; i++)
        {
            Actor_Unit newActor = TacticalUtilities.FindPredator(located);
            if (newActor != null)
            {
                located = newActor;
            }
            else
                break;
        }
        return located?.Position;
    }

    public float GetBulkOfPrey(int count = 0)
    {
        count++;
        if (count > 300)
        {
            Debug.LogWarning("Infinite prey chain seemingly detected, handling it but you should check out the source");
            return 0;
        }

        float ret = 0;
        for (int i = 0; i < prey.Count; i++)
        {
            if (prey[i].Unit == actor.Unit)
            {
                Debug.Log("Prey inside of itself!");
                FreePrey(prey[i], true);
                continue;
            }
            ret += prey[i].Actor.Bulk(count);
        }
        return ret;
    }

    public float TotalCapacity()
    {
        float c = State.RaceSettings.GetStomachSize(unit.Race);

        c *= unit.GetStat(Stat.Stomach) / 12f * unit.TraitBoosts.CapacityMult;

        //c *= unit.GetScale(3);  It may be more realistic, but having huge resulting in 81 times the base stomach capacity is just overkill

        return c;
    }

    public float FreeCap()
    {
        float totalBulk = 0;
        for (int i = 0; i < prey.Count; i++)
        {
            totalBulk += prey[i].Actor.Bulk();
        }

        return TotalCapacity() - totalBulk;
    }

    /// <summary>
    /// Designed to be used for manual regurgitation.
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    internal Prey FreeRandomPreyToSquare(Vec2i location)
    {
        if (actor.Position.GetNumberOfMovesDistance(location) > 1)
            return null;
        if (actor.Movement == 0)
            return null;
        if (actor.Unit.Predator == false)
            return null;
        var alives = prey.Where(s => s.Unit.IsDead == false && !TacticalUtilities.TreatAsHostile(actor, s.Actor)).ToArray();
        if (alives.Length == 0)
            alives = prey.Where(s => s.Unit.IsDead == false).ToArray();
        if (alives.Length == 0)
            return null;
        var target = alives[State.Rand.Next(alives.Length)];
        if (TacticalUtilities.OpenTile(location, target.Actor) == false)
            return null;
        if (!(target.Unit.FixedSide == unit.GetApparentSide(target.Unit)) || !(unit.HasTrait(Traits.FriendlyStomach) || unit.HasTrait(Traits.Endosoma)))
            unit.DrainExp((unit.CalcScaledExp(4 * target.Unit.ExpMultiplier, unit.Level - target.Unit.Level, true)));
        target.Actor.SetPos(location);
        target.Actor.Visible = true;
        target.Actor.Targetable = true;
        TacticalUtilities.UpdateActorLocations();
        target.Actor.UnitSprite.DisplayEscape();
        TacticalUtilities.Log.RegisterRegurgitate(actor.Unit, target.Actor.Unit, actor.PredatorComponent.Location(target));
        RemovePrey(target);
        unit.RestoreStamPct(1);
        UpdateFullness();
        return target;
    }

    public void FreeRandomPreyNow()//Code for prey's hex spell. Selects a random living prey that a pred has consumed and releases them, prioritses hostiles units to the pred.
    {
        List<Prey> preyUnits = new List<Prey>();
        preyUnits.AddRange(prey);
        //var alives = preyUnits.Where(s => s.Unit.IsDead == false).ToArray();
        var alives = preyUnits.Where(s => s.Unit.IsDead == false && TacticalUtilities.TreatAsHostile(actor, s.Actor)).ToArray();
        if (alives.Length == 0)
            alives = preyUnits.Where(s => s.Unit.IsDead == false).ToArray();
        Prey target = null;
        if (alives.Any())
        {
            target = alives[State.Rand.Next(alives.Length)]; // Check is required, even though it shouldn't be.
        }
        else
        {
            return; // Prevent NullPointer
        }
        State.GameManager.TacticalMode.TacticalStats.RegisterFreed(unit.Side);
        if (State.Rand.Next(2) == 0)
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> succumbs to the hex and releases <b>{target.Unit.Name}</b>.");
        else
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"The effects from the hex overwhelms <b>{actor.Unit.Name}</b> causing {LogUtilities.GPPHim(actor.Unit)} to release <b>{target.Unit.Name}</b>.");
        target.Predator.PredatorComponent.FreePrey(target, true);
    }

    internal void FreeGreatEscapePrey(Prey preyUnit)
    {
        TacticalUtilities.Log.LogGreatEscapeFlee(unit, preyUnit.Unit, Location(preyUnit));
        RemovePrey(preyUnit);
        preyUnit.Actor.SelfPrey = null;
    }
    internal void FreeEndoPrey(Prey preyUnit)
    {
        //TacticalUtilities.Log.LogGreatEscapeFlee(unit, preyUnit.Unit, Location(preyUnit));
        RemovePrey(preyUnit);
        preyUnit.Actor.SelfPrey = null;
    }

    internal void FreePrey(Prey preyUnit, bool forcePop)
    {

        if (actor.Visible || forcePop || actor.Fled)
        {
            preyUnit.Actor.SelfPrey = null;
            if (PlacedPrey(preyUnit.Actor))
            {
                preyUnit.Actor.Visible = true;
                preyUnit.Actor.Targetable = true;

                if (Config.DisorientedPrey)
                {
                    preyUnit.Actor.WasJustFreed = true;
                    preyUnit.Actor.Movement = Mathf.Min(Mathf.Max(2, (int)(.2f * preyUnit.Actor.CurrentMaxMovement())), 5);
                }
                else
                    preyUnit.Actor.Movement = preyUnit.Actor.CurrentMaxMovement();


                TacticalUtilities.UpdateActorLocations();
                preyUnit.Actor.UnitSprite.DisplayEscape();
            }
            else
            {
                Debug.Log("Couldn't place prey anywhere in a 5x5 ... killing unit");
                preyUnit.Unit.Health = -99;
                return;
            }

        }
        else
        {
            Actor_Unit predator = actor.SelfPrey?.Predator;
            if (predator != null)
            {
                var loc = actor.SelfPrey.Location;
                Prey prey = new Prey(preyUnit.Actor, predator, preyUnit.SubPrey);
                predator.PredatorComponent.AddPrey(prey, loc);
                preyUnit.Actor.SelfPrey = prey;
            }
            else
            {
                preyUnit.Actor.SelfPrey = null;
                Debug.Log($"Couldn't find predator for {unit.Name}, freeing sub prey");
                preyUnit.Actor.Visible = true;
                preyUnit.Actor.Targetable = true;
                TacticalUtilities.UpdateActorLocations();
                preyUnit.Actor.UnitSprite.DisplayEscape();
            }
        }
        RemovePrey(preyUnit);
        UpdateFullness();

    }

    internal void ShareTrait(Traits trait, Prey preyUnit, Traits maxTrait = Traits.Infiltrator)
    {
        if (trait < maxTrait && !TraitsMethods.IsRaceModifying(trait))
        {
            if (!unit.HasTrait(trait))
                unit.AddSharedTrait(trait);
            if (!preyUnit.SharedTraits.Contains(trait) && unit.HasSharedTrait(trait))
                preyUnit.SharedTraits.Add(trait);
        }
    }

    public void RefreshSharedTraits()
    {
        unit.ResetSharedTraits();
        foreach (Prey preyUnit in prey)
            foreach(Traits trait in preyUnit.SharedTraits)
                if (!unit.HasTrait(trait))
                    unit.AddSharedTrait(trait);
    }

    internal void OnSwallowCallbacks(Prey preyUnit)
    {
        var location = preyUnit.Location;
        foreach(IVoreCallback callback in PopulateCallbacks(preyUnit).OrderBy((vt) => vt.ProcessingPriority))
        {

            if(!callback.OnSwallow(preyUnit, actor, location))
                return;
        }
    }

    void AddPrey(Prey preyUnit, PreyLocation location)
    {
        OnSwallowCallbacks(preyUnit);
        switch (location)
        {
            case PreyLocation.breasts:
                breasts.Add(preyUnit);
                break;
            case PreyLocation.balls:
                balls.Add(preyUnit);
                break;
            case PreyLocation.stomach:
                stomach.Add(preyUnit);
                break;
            case PreyLocation.stomach2:
                stomach2.Add(preyUnit);
                break;
            case PreyLocation.womb:
                womb.Add(preyUnit);
                break;
            case PreyLocation.tail:
                tail.Add(preyUnit);
                break;
            case PreyLocation.anal:
                stomach.Add(preyUnit);
                break;
            case PreyLocation.leftBreast:
                leftBreast.Add(preyUnit);
                break;
            case PreyLocation.rightBreast:
                rightBreast.Add(preyUnit);
                break;
        }
        prey.Add(preyUnit);
        actor.UnitSprite.UpdateSprites(actor, true);
        UpdateAlivePrey();
    }
    
    void AddPrey(Prey preyUnit)
    {
        OnSwallowCallbacks(preyUnit);
        prey.Add(preyUnit);
        UpdateAlivePrey();
    }

    internal PreyLocation Location(Prey preyUnit)
    {
        if (womb.Contains(preyUnit) && unit.CanUnbirth)
        {
            return PreyLocation.womb;
        }
        if (breasts.Contains(preyUnit) && unit.CanBreastVore)
        {
            return PreyLocation.breasts;
        }
        if (balls.Contains(preyUnit) && unit.CanCockVore)
        {
            return PreyLocation.balls;
        }
        if (tail.Contains(preyUnit) && unit.CanTailVore)
        {
            return PreyLocation.tail;
        }
        if (stomach2.Contains(preyUnit) && unit.HasTrait(Traits.DualStomach))
        {
            return PreyLocation.stomach2;
        }
        if (leftBreast.Contains(preyUnit) && unit.CanBreastVore)
        {
            return PreyLocation.leftBreast;
        }
        if (rightBreast.Contains(preyUnit) && unit.CanBreastVore)
        {
            return PreyLocation.rightBreast;
        }
        else
        {
            return PreyLocation.stomach;
        }
    }

    internal void OnRemoveCallbacks(Prey preyUnit, bool remove = true)
    {
        if (prey.Contains(preyUnit))
        {
            var location = preyUnit.Location;
            if(remove)
                prey.Remove(preyUnit);
            foreach (IVoreCallback callback in PopulateCallbacks(preyUnit).OrderBy((vt) => vt.ProcessingPriority))
            {
                if (!callback.OnRemove(preyUnit, actor, location))
                    return;
            }
        }
    }

    private void RemovePrey(Prey preyUnit)
    {
        OnRemoveCallbacks(preyUnit);
        womb.Remove(preyUnit);
        breasts.Remove(preyUnit);
        balls.Remove(preyUnit);
        stomach.Remove(preyUnit);
        stomach2.Remove(preyUnit);
        tail.Remove(preyUnit);
        leftBreast.Remove(preyUnit);
        rightBreast.Remove(preyUnit);
        UpdateAlivePrey();
    }

    private bool PreyCanAutoEscape(Prey preyUnit)
    {
        foreach (Traits trait in preyUnit.Unit.GetTraits.Where(t => (TraitList.GetTrait(t) != null) && TraitList.GetTrait(t) is INoAutoEscape))
        {
            INoAutoEscape callback = (INoAutoEscape)TraitList.GetTrait(trait);
            if(!callback.CanEscape(preyUnit, actor))
            return false;
        }
        return true;
    }

    public void Digest(string feedType = "")
    {
        AlivePrey = 0;
        int totalHeal = 0;
        foreach (Prey preyUnit in prey.ToList())
        {
            if (((preyUnit.Location != PreyLocation.breasts || preyUnit.Location != PreyLocation.leftBreast || preyUnit.Location != PreyLocation.rightBreast) && feedType == "breastfeed") || (preyUnit.Location != PreyLocation.balls && feedType == "cumfeed"))
                break;
            if (unit.HasTrait(Traits.EnthrallingDepths) || preyUnit.Unit.GetStatusEffect(StatusEffectType.Hypnotized)?.Strength == unit.FixedSide)
            {
                preyUnit.Unit.ApplyStatusEffect(StatusEffectType.WillingPrey, 0, 3);
            }
            int preyDamage = CalculateDigestionDamage(preyUnit);
            if (preyUnit.Predator.Unit.HasTrait(Traits.Honeymaker) && preyUnit.Unit.IsDead && (preyUnit.Location == PreyLocation.breasts || preyUnit.Location == PreyLocation.leftBreast || preyUnit.Location == PreyLocation.rightBreast))
                preyDamage /= 2;
            if (preyUnit.TurnsDigested < preyUnit.Unit.TraitBoosts.DigestionImmunityTurns)
                preyDamage = 0;
            if (tail.Contains(preyUnit) && (unit.Race == Race.Succubi || unit.Race == Race.Terrorbird))//Moves prey in tail to stomach after a turn
            {
                if (preyUnit.TurnsBeingSwallowed >= 1)
                {
                    tail.Remove(preyUnit);
                    stomach.Add(preyUnit);
                    preyUnit.TurnsBeingSwallowed = 0;
                }
                else
                {
                    preyUnit.TurnsBeingSwallowed++;
                    preyDamage = 0;
                }
            }

            if (unit.HasTrait(Traits.DualStomach))
            {
                if (stomach.Contains(preyUnit))
                {
                    if (preyUnit.TurnsBeingSwallowed >= 2)
                    {
                        stomach.Remove(preyUnit);
                        stomach2.Add(preyUnit);
                    }
                    else
                    {
                        preyUnit.TurnsBeingSwallowed++;
                    }
                }
            }

            if (actor.PredatorComponent.IsUnitInPrey(preyUnit.Actor, PreyLocation.womb) && actor.PredatorComponent.birthStatBoost > 0 && preyUnit.Unit.GetApparentSide(unit) == unit.FixedSide && Config.KuroTenkoEnabled && Config.CumGestation)
            {
                while (actor.PredatorComponent.birthStatBoost > 0)
                {
                    int stat = UnityEngine.Random.Range(0, 8);
                    preyUnit.Unit.ModifyStat(stat, 1);
                    if (stat == 6)
                    {
                        preyUnit.Unit.Health += 2;
                    }
                    if (stat == 0)
                    {
                        preyUnit.Unit.Health += 1;
                    }
                    actor.PredatorComponent.birthStatBoost--;
                }
            }
            if (TacticalUtilities.IsPreyEndoTargetForUnit(preyUnit, unit))
            {
                if (unit.HasTrait(Traits.Endosoma))
                {
                    preyUnit.Unit.Stamina -= preyDamage;
                }
                if (unit.HasTrait(Traits.HealingBelly))
                    preyDamage = Math.Min(unit.MaxHealth / -10, -1);
                else
                    preyDamage = 0;
                if (unit.HasTrait(Traits.Extraction))
                {
                    var possibleTraits = unit.GetTraits.Where(s => !preyUnit.Unit.GetTraits.Contains(s) && State.AssimilateList.CanGet(s)).ToArray();

                    if (possibleTraits.Any())
                    {
                        var trait = possibleTraits[State.Rand.Next(possibleTraits.Length)];
                        preyUnit.Unit.AddPermanentTrait(trait);
                    }
                }
                if (unit.HasTrait(Traits.InfiniteAssimilation) && !preyUnit.Unit.HasTrait(Traits.InfiniteAssimilation) && Config.KuroTenkoEnabled)
                    preyUnit.Unit.AddPermanentTrait(Traits.InfiniteAssimilation);
                if (unit.HasTrait(Traits.Assimilate) && !preyUnit.Unit.HasTrait(Traits.Assimilate) && Config.KuroTenkoEnabled)
                    preyUnit.Unit.AddPermanentTrait(Traits.Assimilate);
                if (unit.HasTrait(Traits.AdaptiveBiology) && !preyUnit.Unit.HasTrait(Traits.AdaptiveBiology) && Config.KuroTenkoEnabled)
                    preyUnit.Unit.AddPermanentTrait(Traits.AdaptiveBiology);
                if (unit.HasTrait(Traits.Corruption) && !preyUnit.Unit.HasTrait(Traits.Corruption))
                {
                    preyUnit.Unit.AddPermanentTrait(Traits.Corruption);
                    if (!preyUnit.Unit.HasTrait(Traits.Untamable))
                        preyUnit.Unit.FixedSide = unit.FixedSide;
                    preyUnit.Unit.hiddenFixedSide = true;
                    preyUnit.Actor.sidesAttackedThisBattle = new List<int>();
                }
                //if (preyUnit.Unit.HasTrait(Traits.Shapeshifter) || preyUnit.Unit.HasTrait(Traits.Skinwalker))
                //{
                //    preyUnit.Unit.AcquireShape(unit);
                //}
                preyUnit.Unit.ReloadTraits();
                preyUnit.Unit.InitializeTraits();

            }
            else if (Config.FriendlyRegurgitation && unit.HasTrait(Traits.Greedy) == false && preyUnit.Unit.GetApparentSide(actor.Unit) == actor.Unit.FixedSide && TacticalUtilities.GetMindControlSide(preyUnit.Unit) == -1 && preyUnit.Unit.Health > 0 && preyUnit.Actor.Surrendered == false && PreyCanAutoEscape(preyUnit))
            {
                State.GameManager.TacticalMode.TacticalStats.RegisterRegurgitation(unit.Side);
                TacticalUtilities.Log.RegisterRegurgitated(unit, preyUnit.Unit, Location(preyUnit));
                FreePrey(preyUnit, false);
                continue;
            }

            if (preyDamage > 0)
                totalHeal += DigestOneUnit(preyUnit, preyDamage);
            else
                DigestOneUnit(preyUnit, preyDamage);
            if (unit.HasTrait(Traits.Growth))
            {
                unit.BaseScale += ((float)totalHeal / preyUnit.Unit.MaxHealth * .2d) * CalculateGrowthValue(preyUnit);
                if (unit.BaseScale > Config.GrowthCap)
                    unit.BaseScale = Config.GrowthCap;
            }
        }
        if (!(unit.Health < unit.MaxHealth))
        {
            totalHeal = 0;
        }
        if (totalHeal > 0)
        {
            unit.Heal(totalHeal);
            actor.UnitSprite.DisplayDamage(-totalHeal);
            TacticalUtilities.Log.RegisterHeal(unit, new int[] { totalHeal, 0 });
        }
        UpdateFullness();
    }

    float CalculateGrowthValue(Prey preyUnit)
    {
        float preyMass = preyUnit.Unit.TraitBoosts.BulkMultiplier * State.RaceSettings.GetBodySize(preyUnit.Unit.Race);
        float predMass = unit.TraitBoosts.BulkMultiplier * State.RaceSettings.GetBodySize(unit.Race);
        float sizeDiff = (preyUnit.Unit.GetScale(2) * preyMass) / (unit.GetScale(2) * predMass);
        float preyBoosts = (((preyUnit.Unit.TraitBoosts.Outgoing.Nutrition - 1) * .2f) + 1f) * preyUnit.Unit.TraitBoosts.Outgoing.GrowthRate;
        float predBoosts = (((unit.TraitBoosts.Incoming.Nutrition - 1) * .2f) + 1f) * unit.TraitBoosts.Incoming.GrowthRate * Config.GrowthMod;
        return sizeDiff * preyBoosts * predBoosts * TagConditionChecker.ApplyTagEffect(unit, preyUnit.Unit, UnitTagModifierEffect.GrowthRateMult);
    }

    int ApplySettingsToDamage(int incoming_damage, Prey preyUnit)
    {
        if (Config.DigestionFlatDmg >= 0.01f)
            return (int)(preyUnit.Unit.MaxHealth * Config.DigestionFlatDmg);

        float outgoing_damage_mod = Config.DigestionSpeedMult;
        outgoing_damage_mod += Config.DigestionRamp * (actor.RampStacks >= Config.DigestionRampCap && Config.DigestionRampCap > 0 ? Config.DigestionRampCap : (float)Math.Floor(actor.RampStacks));
        if (actor.BeingRubbed)
            outgoing_damage_mod *= Config.BellyRubEffMult;
        int outgoing_damage = (int)(incoming_damage * outgoing_damage_mod);

        if (Config.DigestionDamageDivision)
        {
            int prey_in_loc = PreyInLocation(preyUnit.Location, true);
            outgoing_damage /= prey_in_loc > 0 ? prey_in_loc : 1;
        }

        if (outgoing_damage > (int)(preyUnit.Unit.MaxHealth * Config.DigestionCap) && Config.DigestionCap > 0)
        {
            outgoing_damage = (int)(preyUnit.Unit.MaxHealth * Config.DigestionCap);
        }
        return outgoing_damage;
    }
    int CalculateDigestionDamage(Prey preyUnit)
    {
        if (preyUnit.TurnsDigested < preyUnit.Unit.TraitBoosts.DigestionImmunityTurns || preyUnit.Unit.HasTrait(Traits.TheGreatEscape))
            return 0;

        float usedCapacity = TotalCapacity() - FreeCap();
        float PredStomach = Mathf.Pow(unit.GetStat(Stat.Stomach) + 15, 1.5f);
        float PredVoracity = Mathf.Pow(unit.GetStat(Stat.Voracity) + 15, 1.5f);
        float preyEndurance = Mathf.Pow(preyUnit.Unit.GetStat(Stat.Endurance) + 15, 1.5f);
        float preyWill = Mathf.Pow(preyUnit.Unit.GetStat(Stat.Will) + 15, 1.5f);
        float predScore = 2 * (40 + PredStomach * 3 + PredVoracity) / (1f + ((usedCapacity - preyUnit.Actor.Bulk()) / usedCapacity));
        float preyScore = ((preyEndurance + preyWill * .75f) / (1 + preyUnit.Actor.BodySize() / 40) + 300) / (1 + preyUnit.TurnsDigested / 5f);
        predScore *= unit.TraitBoosts.Outgoing.DigestionRate;
        preyScore /= preyUnit.Unit.TraitBoosts.Incoming.DigestionRate;
        int damage = (int)Math.Round(predScore / preyScore);
        damage = (int)(damage * TagConditionChecker.ApplyTagEffect(unit, preyUnit.Unit, UnitTagModifierEffect.DigestionRateMult));
        damage = ApplySettingsToDamage(damage, preyUnit);
        if (unit.HasTrait(Traits.SleepItOff) && unit.GetStatusEffect(StatusEffectType.Sleeping) != null)
            damage *= 2;
        if (preyUnit.Unit.GetStatusEffect(StatusEffectType.Errosion) != null)
            damage += (int)(damage * (preyUnit.Unit.GetStatusEffect(StatusEffectType.Errosion).Strength / 2));
        if (unit.GetStatusEffect(StatusEffectType.Diluted) != null)
            damage -= (int)(damage * (unit.GetStatusEffect(StatusEffectType.Diluted).Strength * unit.GetStatusEffect(StatusEffectType.Diluted).Duration));
        if (damage < 1)
            damage = 1;

        return damage;
    }

    /// <summary>
    /// Returns the % of the stomach used.  i.e. 50% is .5, can be over 100%
    /// </summary>
    internal float UsageFraction => (TotalCapacity() - FreeCap()) / TotalCapacity();

    internal void FreeUnit(Actor_Unit target, bool forcePop = false)
    {
        var preyUnit = prey.Where(s => s.Actor == target).FirstOrDefault();
        if (preyUnit != null)
        {
            RemovePrey(preyUnit);
            FreePrey(preyUnit, forcePop);
        }

    }

    public void CreateSpawn(Race race, int side, float experience, bool forced = false)
    {
        Spawn spawnUnit = new Spawn(side,race,(int)(experience));
        Actor_Unit spawnActor = State.GameManager.TacticalMode.AddUnitToBattle(spawnUnit, actor);
        State.GameManager.TacticalMode.DirtyPack = true;
        if(forced)
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{InfoPanel.RaceSingular(spawnUnit)} Spawn <b>{spawnUnit.Name}</b> bursts from <b>{actor.Unit.Name}</b>'s body");
        else
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> has created a {InfoPanel.RaceSingular(spawnUnit)} Spawn <b>{spawnUnit.Name}</b>.");
        Prey preyref = new Prey(spawnActor, actor, spawnActor.PredatorComponent?.prey);
        spawnActor.UnitSprite.UpdateSprites(spawnActor, false);
        AddPrey(preyref);
        FreeUnit(preyref.Actor);
        if (!State.GameManager.TacticalMode.turboMode)
            actor.SetBirthMode();
        RemovePrey(preyref);
    }

    public void ResetTemplate()
    {
        if (template == null)
            return;
        foreach (Traits trait in template.SharedTraits)
            unit.RemoveSharedTrait(trait);
        RefreshSharedTraits();
        template = null;
    }

    internal bool CheckChangeRace(Prey preyUnit)
    {
        if (unit.HiddenRace == unit.Race && template == null)
        {
            return true;
        }
        if (!prey.Contains(template))
        {
            template = null;
            return true;
        }
        return false;
    }

    private Prey SelectPrey(bool isDead, bool random)
    {
        Prey bestPrey = null;
        List<Prey> preyList;
        if (isDead)
            preyList = deadPrey;
        else
            preyList = prey;
        if (preyList.Count > 0)
        {
            bestPrey = preyList[State.Rand.Next(preyList.Count)];
            if (random)
                return bestPrey;
            foreach (Prey preyUnit in preyList)
                if (preyUnit.Unit.Health > bestPrey.Unit.Health)
                    bestPrey = preyUnit;
        }
        return bestPrey;
    }

    public bool ChangeRaceAuto(bool isDead, bool random)
    {
        Prey preyUnit = SelectPrey(isDead, random);
        if (preyUnit != null){
            return TryChangeRace(preyUnit);
        }
        else
        {
            actor.RevertRace();
            return false;
        }
    }

    internal bool TryChangeRace(Prey preyUnit)
    {
        if (CheckChangeRace(preyUnit))
        {
            if (unit.HasTrait(Traits.Changeling) && !preyUnit.Unit.IsDead)
                return false;
            actor.ChangeRaceAny(preyUnit.Unit, false, true);
            template = preyUnit;
            foreach (Traits trait in preyUnit.Unit.GetTraits)
            {
                ShareTrait(trait, preyUnit);
            }
            return true;
        }
        return false;
    }

    internal void WeightGain(Prey preyUnit)
    {
        if (Location(preyUnit) == PreyLocation.balls)
        {
            if (unit.HasDick)
            {
                unit.DickSize = Math.Min(unit.DickSize + 1, Races.GetRace(unit).DickSizes - 1);
                if (Config.RaceSizeLimitsWeightGain && State.RaceSettings.GetOverrideDick(unit.Race))
                    unit.DickSize = Math.Min(unit.DickSize, State.RaceSettings.Get(unit.Race).MaxDick);
            }
        }
        else if (Location(preyUnit) == PreyLocation.breasts || Location(preyUnit) == PreyLocation.leftBreast || Location(preyUnit) == PreyLocation.rightBreast)
        {
            if (unit.HasBreasts)
            {
                unit.SetDefaultBreastSize(Math.Min(unit.DefaultBreastSize + 1, Races.GetRace(unit).BreastSizes - 1), unit.BreastSize == unit.DefaultBreastSize);
                if (Config.RaceSizeLimitsWeightGain && State.RaceSettings.GetOverrideBreasts(unit.Race))
                    unit.SetDefaultBreastSize(Math.Min(unit.DefaultBreastSize, State.RaceSettings.Get(unit.Race).MaxBoob));
            }
        }
        else
        {
            if (Config.AltVoreOralGain && State.Rand.NextDouble() < Config.WeightGainFraction)
            {
                if (unit.HasDick)
                {
                    unit.DickSize = Math.Min(unit.DickSize + 1, Races.GetRace(unit).DickSizes - 1);
                    if (Config.RaceSizeLimitsWeightGain && State.RaceSettings.GetOverrideDick(unit.Race))
                        unit.DickSize = Math.Min(unit.DickSize, State.RaceSettings.Get(unit.Race).MaxDick);
                }
            }

            if (Config.AltVoreOralGain && State.Rand.NextDouble() < Config.WeightGainFraction)
            {
                if (unit.HasBreasts)
                {
                    unit.SetDefaultBreastSize(Math.Min(unit.DefaultBreastSize + 1, Races.GetRace(unit).BreastSizes - 1), unit.BreastSize == unit.DefaultBreastSize);
                    if (Config.RaceSizeLimitsWeightGain && State.RaceSettings.GetOverrideBreasts(unit.Race))
                        unit.SetDefaultBreastSize(Math.Min(unit.DefaultBreastSize, State.RaceSettings.Get(unit.Race).MaxBoob));
                }
            }

            if (Races.GetRace(unit).WeightGainDisabled == false && State.Rand.NextDouble() < Config.WeightGainFraction)
            {
                unit.BodySize = Math.Max(Math.Min(unit.BodySize + 1, Races.GetRace(unit).BodySizes - 1), 0);
                if (Config.RaceSizeLimitsWeightGain && State.RaceSettings.GetOverrideWeight(unit.Race))
                    unit.BodySize = Math.Min(unit.BodySize, State.RaceSettings.Get(unit.Race).MaxWeight);
            }

            
        }

    }

    private int DigestOneUnit(Prey preyUnit, int preyDamage)
    {
        List<IVoreCallback> Callbacks = PopulateCallbacks(preyUnit).OrderBy((vt) => vt.ProcessingPriority).ToList();
        var location = preyUnit.Location;
        int totalHeal = 0;
        bool freshKill = false;
        if (unit.HasTrait(Traits.Extraction) && !TacticalUtilities.IsPreyEndoTargetForUnit(preyUnit, unit))
        {
            if (preyUnit.Unit.GetTraits.Any())
            {
                var trait = preyUnit.Unit.GetTraits[State.Rand.Next(preyUnit.Unit.GetTraits.Count)];
                var possibleTraits = preyUnit.Unit.GetTraits.Where(s => unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();
                if (possibleTraits.Any())
                {
                    trait = possibleTraits[State.Rand.Next(possibleTraits.Length)];
                    if (unit.HasTrait(Traits.SynchronizedEvolution))
                    {
                        RaceSettingsItem item = State.RaceSettings.Get(unit.Race);
                        item.RaceTraits.Add(trait);
                        UpdateRaceTraits();
                    }
                    else
                    {
                        unit.AddPermanentTrait(trait);
                        //process vore traits appropriately
                        if (TraitList.GetTrait(trait) is IVoreCallback callback)
                        {
                            if (callback.IsPredTrait == true)
                                callback.OnSwallow(preyUnit, actor, location);
                            else
                                callback.OnRemove(preyUnit, actor, location);
                        }
                        UpdateUnitTraits();
                    }
                }
                else
                {
                    unit.GiveRawExp(5);
                    actor.UnitSprite.DisplayDamage(5, false, true);
                }
                preyUnit.Unit.RemoveTrait(trait);
            }            
        }
        if (unit.HasTrait(Traits.Annihilation) && !TacticalUtilities.IsPreyEndoTargetForUnit(preyUnit, unit))
        {
            int prevLevelExp = preyUnit.Unit.GetExperienceRequiredForLevel(preyUnit.Unit.Level - 2);
            if (preyUnit.Unit.Level == 1 && preyUnit.Unit.Race != Race.Erin)
            {
                preyUnit.Unit.SetLevel(0);
                preyUnit.Unit.SetStatBaseAll(1);
                preyUnit.Unit.SetExp(0);
                preyUnit.Unit.Health = 0;
                preyUnit.Unit.SavedCopy = null;
                preyUnit.Unit.SavedVillage = null;
                preyUnit.Unit.RemoveTrait(Traits.Reincarnation);
                preyUnit.Unit.RemoveTrait(Traits.InfiniteReincarnation);
                preyUnit.Unit.RemoveTrait(Traits.Transmigration);
                preyUnit.Unit.RemoveTrait(Traits.InfiniteTransmigration);
                preyUnit.Unit.RemoveTrait(Traits.Eternal);
                preyUnit.Unit.RemoveTrait(Traits.LuckySurvival);
                preyUnit.Unit.RemoveTrait(Traits.Reformer);
                preyUnit.Unit.RemoveTrait(Traits.TheGreatEscape);
                preyUnit.Unit.RemoveTrait(Traits.DeathCheater);
                preyUnit.Unit.RemoveTrait(Traits.Respawner);
                preyUnit.Unit.RemoveTrait(Traits.RespawnerIII);
            }
            else
            {
                preyUnit.Unit.LevelDown();
                preyUnit.Unit.SetExp(preyUnit.Unit.Experience - (preyUnit.Unit.Experience - prevLevelExp));
                preyDamage = 0;
            }
            if (preyUnit.Unit.Level == 0) preyDamage = 1;
            actor.Unit.GiveRawExp(Math.Max(1, (int)(preyUnit.Unit.Experience - prevLevelExp)));
        }
        if (preyUnit.Unit.IsThisCloseToDeath(preyDamage))
        {
            if (preyUnit.Unit.HasTrait(Traits.Corruption))
            {
                actor.AddCorruption(preyUnit.Unit.GetStatTotal(), preyUnit.Unit.FixedSide);
                preyUnit.Unit.RemoveTrait(Traits.Corruption);
            }
            foreach (IVoreCallback callback in Callbacks)
            {
                if (!callback.OnFinishDigestion(preyUnit, actor, location))
                    return 0;
            }
            if (preyUnit.Unit.CanBeConverted() &&
             (Location(preyUnit) == PreyLocation.womb || Config.KuroTenkoConvertsAllTypes) &&
             (Config.KuroTenkoEnabled && (Config.UBConversion == UBConversion.Both || Config.UBConversion == UBConversion.ConversionOnly) || unit.HasTrait(Traits.PredConverter)) &&
             !unit.HasTrait(Traits.PredRebirther) &&
             !unit.HasTrait(Traits.PredGusher) && !(preyUnit.Unit.AtypicalBiology() && !unit.AtypicalBiology()) && !(!preyUnit.Unit.AtypicalBiology() && unit.AtypicalBiology() && State.Rand.Next(2) == 0))
            {
                preyUnit.Unit.Health = preyUnit.Unit.MaxHealth / 2;
                preyUnit.Actor.Movement = 0;
                preyUnit.ChangeSide(unit.Side);
                TacticalUtilities.Log.RegisterBirth(unit, preyUnit.Unit, 1f, Location(preyUnit), 2);
                FreeUnit(preyUnit.Actor);
                if (!State.GameManager.TacticalMode.turboMode)
                    actor.SetBirthMode();
                return 0;
            }
            State.GameManager.TacticalMode.TacticalStats.RegisterDigestion(unit.Side);
            TacticalUtilities.Log.RegisterDigest(unit, preyUnit.Unit, Location(preyUnit));
            if (!State.GameManager.TacticalMode.turboMode)
                actor.SetDigestionMode();
            if (State.GameManager.TacticalMode.turboMode == false && Config.DigestionSkulls)
                GameObject.Instantiate(State.GameManager.TacticalMode.SkullPrefab, new Vector3(actor.Position.x + UnityEngine.Random.Range(-0.2F, 0.2F), actor.Position.y + 0.1F + UnityEngine.Random.Range(-0.1F, 0.1F)), new Quaternion());
            Actor_Unit existingPredator = actor;
            freshKill = true;
            if (unit.HasTrait(Traits.DigestionConversion) && State.Rand.Next(2) == 0 && preyUnit.Unit.CanBeConverted() && !(preyUnit.Unit.AtypicalBiology() && !unit.AtypicalBiology()))
            {
                preyUnit.Unit.Health = preyUnit.Unit.MaxHealth / 2;
                preyUnit.Actor.Movement = 0;

                preyUnit.ChangeSide(unit.Side);
                State.GameManager.TacticalMode.TacticalStats.RegisterRegurgitation(unit.Side);
                TacticalUtilities.Log.RegisterBirth(unit, preyUnit.Unit, 0.5f, Location(preyUnit), 4);
                FreeUnit(preyUnit.Actor);
                return 0;
            }
            if (unit.HasTrait(Traits.DigestionRebirth) && State.Rand.Next(2) == 0 && preyUnit.Unit.CanBeConverted() && (Config.SpecialMercsCanConvert || unit.DetermineConversionRace() < Race.Selicia) && !(preyUnit.Unit.AtypicalBiology() && !unit.AtypicalBiology()))
            {
                //HandleShapeshifterRebirth(preyUnit);
                Race conversionRace = unit.DetermineConversionRace();
                if(unit.HasTrait(Traits.DigestionRebirth) && !unit.HasSharedTrait(Traits.DigestionRebirth))
                    conversionRace = unit.HiddenUnit.DetermineConversionRace();
                // use source race IF changeling already had this ability before transforming
                preyUnit.Unit.Health = preyUnit.Unit.MaxHealth / 2;
                preyUnit.ChangeSide(unit.Side);
                if ((!preyUnit.Unit.AtypicalBiology() && !unit.AtypicalBiology()) || (preyUnit.Unit.AtypicalBiology() && unit.AtypicalBiology())) // Only if the both units have AcellularBody or neither can they change the prey's race
                    preyUnit.ChangeRace(conversionRace);
                if (!preyUnit.Unit.AtypicalBiology() && unit.AtypicalBiology())
                    TacticalUtilities.Log.RegisterBirth(unit, preyUnit.Unit, 0.5f, Location(preyUnit), 4);
                else
                    TacticalUtilities.Log.RegisterBirth(unit, preyUnit.Unit, 0.5f, Location(preyUnit), 3);
                FreeUnit(preyUnit.Actor);
                return 0;
            }
            preyUnit.Actor.KilledByDigestion = true;
            if (preyUnit.Unit.HasTrait(Traits.CursedMark))
            {
                unit.AddPermanentTrait(Traits.CursedMark);
                preyUnit.Unit.RemoveTrait(Traits.CursedMark);
            }
            unit.DigestedUnits++;
            if (unit.HasTrait(Traits.EssenceAbsorption) && unit.DigestedUnits % 4 == 0)
                unit.GeneralStatIncrease(1);
            if (preyUnit.Unit.Race == Race.Asura)
                unit.EarnedMask = true;
            if (unit.HasTrait(Traits.TasteForBlood))
                actor.GiveRandomBoost();
            unit.EnemiesKilledThisBattle++;
            preyUnit.Unit.KilledBy = unit;
            preyUnit.Unit.Kill();
            foreach (var item in preyUnit.Unit.AllConditionalTraits.Keys.Where(t => t.trigger == TraitConditionTrigger.OnDeath || t.trigger == TraitConditionTrigger.All).ToList())
            {
                if (ConditionalTraitConditionChecker.TacticalTraitConditionActive(preyUnit.Actor, item))
                {
                    preyUnit.Unit.ActivateConditionalTrait(item.id);
                }
                else
                {
                    preyUnit.Unit.DeactivateConditionalTrait(item.id);
                }
            }
            for (int i = 0; i < 20; i++)
            {
                Actor_Unit next = TacticalUtilities.FindPredator(existingPredator);
                if (next == null)
                    break;
                existingPredator = next;
            }
            DigestionEffect(preyUnit, existingPredator);

            if (unit.HasTrait(Traits.MetabolicSurge))
            {
                unit.ApplyStatusEffect(StatusEffectType.Empowered, 1.0f, 5);
            }
            if (preyUnit.Unit.HasTrait(Traits.ExtraNutritious))
            {
                for (int i = 0; i < preyUnit.Unit.Level; i++)
                {
                    unit.RandomStatIncrease(1);
                }
            }
            if (preyUnit.Unit.GetStatusEffect(StatusEffectType.Respawns) != null && (preyUnit.Unit.HasTrait(Traits.Respawner) || preyUnit.Unit.HasTrait(Traits.RespawnerIII)))
            {
                var spawnLoc = TacticalUtilities.GetRandomTileForActor(preyUnit.Actor);
                if (spawnLoc == null)
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{preyUnit.Unit.Name} was unable to respawn!");
                else
                    TacticalUtilities.Resurrect((spawnLoc),preyUnit.Actor);
                    TacticalGraphicalEffects.CreateGenericMagic(spawnLoc, spawnLoc, preyUnit.Actor, TacticalGraphicalEffects.SpellEffectIcon.Resurrect);
                    preyUnit.Unit.Health = preyUnit.Unit.MaxHealth;
                    preyUnit.Unit.RemoveRespawns();
                    FreeUnit(preyUnit.Actor);
                    if (preyUnit.Unit.Race != Race.RwuMercenaries)
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"{preyUnit.Unit.Name} has respawned!");
                    else
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"Reinforcements have arrived!");
                return 0;
            }
        }

        if (preyUnit.Unit.IsDead == false)
        {
            preyUnit.Actor.SubtractHealth(preyDamage);
            preyUnit.TurnsSinceLastDamage = 0;
        }

        if (freshKill)
        {
            if (preyUnit.Unit.HasTrait(Traits.DyingStrike) && State.Rand.Next(4) == 0)
            {
                actor.Damage(preyUnit.Actor.WeaponDamageAgainstTarget(actor, false));
                if (State.Rand.Next(2) == 0)
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"As <b>{preyUnit.Unit.Name}</b> perishes {LogUtilities.GPPHe(preyUnit.Unit)} do{LogUtilities.EsIfSingular(preyUnit.Unit)} one last attack against <b>{actor.Unit.Name}</b> dealing <color=red>{preyUnit.Actor.WeaponDamageAgainstTarget(actor, false)}</color> damage!");
                else
                    State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"In {LogUtilities.GPPHis(preyUnit.Unit)} last moments <b>{preyUnit.Unit.Name}</b> strikes at {LogUtilities.GetRandomStringFrom($"{LogUtilities.GPPHis(preyUnit.Unit)} captor", $"<b>{actor.Unit.Name}</b>")} dealing <color=red>{preyUnit.Actor.WeaponDamageAgainstTarget(actor, false)}</color> damage!");
            }
            preyUnit.Actor.Unit.Health = 0;
            foreach (IVoreCallback callback in Callbacks)
            {
                if (!callback.OnDigestionKill(preyUnit, actor, location))
                    return 0;
            }
        }
            

        if (preyUnit.Unit.IsThisCloseToDeath(preyDamage))
        {
            TacticalUtilities.Log.RegisterNearDigestion(unit, preyUnit.Unit, Location(preyUnit));
        }
        else if (preyUnit.Unit.IsDead == false && State.Rand.Next(6) == 0 && preyDamage > 0)
        {
            TacticalUtilities.Log.LogDigestionRandom(unit, preyUnit.Unit, Location(preyUnit));
        }
        else if (preyUnit.Unit.IsDead == false && State.Rand.Next(6) == 0 && preyDamage == 0 && preyUnit.Unit.HasTrait(Traits.TheGreatEscape))
        {
            TacticalUtilities.Log.LogGreatEscapeKeep(unit, preyUnit.Unit, Location(preyUnit));
        }

        if (preyUnit.Unit.IsDead)
        {
            float speedFactor;
            speedFactor = (float)Math.Sqrt(actor.BodySize() / preyUnit.Actor.BodySize());

            speedFactor *= unit.TraitBoosts.Outgoing.AbsorptionRate;
            speedFactor *= preyUnit.Unit.TraitBoosts.Incoming.AbsorptionRate;
            speedFactor *= TagConditionChecker.ApplyTagEffect(unit, preyUnit.Unit, UnitTagModifierEffect.AbsorptionRateMult);

            if (speedFactor > 4f && speedFactor < 1000)
                speedFactor = 4f;
            speedFactor *= Config.AbsorbSpeedMult + (Config.AbsorbBoostDeadOnly && AlivePrey >= 1 ? 0 : Config.AbsorbRamp * (actor.RampStacks >= Config.DigestionRampCap && Config.DigestionRampCap > 0 ? Config.DigestionRampCap : (float)Math.Floor(actor.RampStacks)));

            if (Config.AbsorbRateDivision)
            {
                int prey_in_loc = PreyInLocation(preyUnit.Location, false);
                speedFactor /= prey_in_loc > 0 ? prey_in_loc : 1;

            }

            int healthReduction = (int)Math.Max(Math.Round(preyUnit.Unit.MaxHealth * speedFactor / 15), 1);
            if (healthReduction >= preyUnit.Unit.MaxHealth + preyUnit.Unit.Health)
                healthReduction = preyUnit.Unit.MaxHealth + preyUnit.Unit.Health + 1;
            preyUnit.Actor.SubtractHealth(healthReduction);
            
            totalHeal += Math.Max((int)(healthReduction / 2 * preyUnit.Unit.TraitBoosts.Outgoing.Nutrition * unit.TraitBoosts.Incoming.Nutrition), 1);
            totalHeal = (int)(totalHeal * TagConditionChecker.ApplyTagEffect(unit, preyUnit.Unit, UnitTagModifierEffect.NutritionMult));
            var baseManaGain = healthReduction * (preyUnit.Unit.TraitBoosts.Outgoing.ManaAbsorbHundreths + unit.TraitBoosts.Incoming.ManaAbsorbHundreths);
            baseManaGain += (int)TagConditionChecker.ApplyTagEffect(unit, preyUnit.Unit, UnitTagModifierEffect.ManaAbsorbHundreths);
            var totalManaGain = baseManaGain / 100 + (State.Rand.Next(100) < (baseManaGain % 100) ? 1 : 0);
            float resource_boost = (actor.RampStacks >= Config.DigestionRampCap && Config.DigestionRampCap > 0 ? Config.DigestionRampCap : (float)Math.Floor(actor.RampStacks));
            if (Config.AbsorbResourceModBoost == 1)
            {
                totalHeal = (int)(totalHeal * (Config.AbsorbResourceMod * resource_boost));
                unit.RestoreMana((int)(totalManaGain * (Config.AbsorbResourceMod * resource_boost)));
            }
            else if (Config.AbsorbResourceModBoost == 2)
            {
                totalHeal = (int)(totalHeal * (Config.AbsorbResourceMod + resource_boost));
                unit.RestoreMana((int)(totalManaGain * (Config.AbsorbResourceMod + resource_boost)));
            }
            else
            {
                totalHeal = (int)(totalHeal * Config.AbsorbResourceMod);
                unit.RestoreMana((int)(totalManaGain * Config.AbsorbResourceMod));
            }
            foreach (IVoreCallback callback in Callbacks)
            {
                if (!callback.OnAbsorption(preyUnit, actor, location))
                    return 0;
            }
            if (preyUnit.Unit.IsDeadAndOverkilledBy(healthReduction * 2) && preyUnit.SubPrey?.Count() > 0)
            {
                Prey[] aliveSubUnits = preyUnit.GetAliveSubPrey();
                for (int i = 0; i < aliveSubUnits.Length; i++)
                {
                    Prey newPrey = new Prey(aliveSubUnits[i].Actor, actor, aliveSubUnits[i].SubPrey);
                    AddPrey(newPrey, Location(preyUnit));
                    preyUnit.SubPrey.Remove(aliveSubUnits[i]);
                }
            }

            //(Ambi) Weight Gain at 80% Absorption
            if (preyUnit.Unit.IsDeadAndOverkilledBy(preyUnit.Unit.MaxHealth - preyUnit.Unit.MaxHealth/5))
            {
                if (Config.WeightGain)
                {
                    //Have prey activate WG so multi-prey works as expected
                    preyUnit.PredWeightGain();
                }
            }

            if (preyUnit.Unit.IsDeadAndOverkilledBy(preyUnit.Unit.MaxHealth))
            {
                if (actor.Infected)
                {
                    actor.Damage(totalHeal * 2);
                    totalHeal = 0;
                    CreateSpawn(actor.InfectedRace, actor.InfectedSide, unit.Experience/2, true);
                }
                foreach (IVoreCallback callback in Callbacks)
                {
                    if (!callback.OnFinishAbsorption(preyUnit, actor, location))
                        return 0;
                }
                if (preyUnit.Unit.CanBeConverted() &&
                 (Location(preyUnit) == PreyLocation.womb || Config.KuroTenkoConvertsAllTypes) &&
                 ((Config.KuroTenkoEnabled && (Config.UBConversion == UBConversion.Both || Config.UBConversion == UBConversion.RebirthOnly)) || unit.HasTrait(Traits.PredRebirther)) &&
                 (Config.SpecialMercsCanConvert || unit.DetermineConversionRace() < Race.Selicia) &&
                 !unit.HasTrait(Traits.PredGusher) && !(preyUnit.Unit.AtypicalBiology() && !unit.AtypicalBiology()) && !(!preyUnit.Unit.AtypicalBiology() && unit.AtypicalBiology() && State.Rand.Next(2) == 0))
                {
                    Race conversionRace = unit.DetermineConversionRace();
                    if(unit.HasTrait(Traits.PredRebirther) && !unit.HasSharedTrait(Traits.PredRebirther))
                        conversionRace = unit.HiddenUnit.DetermineConversionRace();
                    // use source race IF changeling already had this ability before transforming
                    preyUnit.Unit.Health = preyUnit.Unit.MaxHealth / 2;
                    preyUnit.ChangeSide(unit.Side);
                    if (preyUnit.Unit.Race != conversionRace && ((!preyUnit.Unit.AtypicalBiology() && !unit.AtypicalBiology()) || (preyUnit.Unit.AtypicalBiology() && unit.AtypicalBiology()))) // Only if the both units have AcellularBody or neither can they change the prey's race
                    {
                        //HandleShapeshifterRebirth(preyUnit);
                        preyUnit.ChangeRace(conversionRace);
                    }
                    if (!preyUnit.Unit.AtypicalBiology() && unit.AtypicalBiology())
                        TacticalUtilities.Log.RegisterBirth(unit, preyUnit.Unit, 1f, Location(preyUnit), 2);
                    else
                        TacticalUtilities.Log.RegisterBirth(unit, preyUnit.Unit, 1f, Location(preyUnit), 1);
                    FreeUnit(preyUnit.Actor);
                    if (!State.GameManager.TacticalMode.turboMode)
                        actor.SetBirthMode();
                    RemovePrey(preyUnit);
                    return 0;
                }
                else
                {
                    TacticalUtilities.Log.RegisterAbsorb(unit, preyUnit.Unit, Location(preyUnit));
                }
                unit.GiveScaledExp(8 * preyUnit.Unit.ExpMultiplier, unit.Level - preyUnit.Unit.Level, true);
                AbsorptionEffect(preyUnit, Location(preyUnit));
                if (!State.GameManager.TacticalMode.turboMode)
                    actor.SetAbsorbtionMode();
                CheckPredTraitAbsorption(preyUnit);
                //if (unit.HasTrait(Traits.Shapeshifter) || unit.HasTrait(Traits.Skinwalker))
                //{
                //    unit.AcquireShape(preyUnit.Unit);
                //}

                if (preyUnit.SubPrey?.Count() > 0) //Catches any dead prey that weren't already properly moved
                {
                    Prey[] subUnits = preyUnit.SubPrey.ToArray();
                    for (int i = 0; i < subUnits.Length; i++)
                    {
                        Prey newPrey = new Prey(subUnits[i].Actor, actor, subUnits[i].SubPrey);
                        AddPrey(newPrey, Location(preyUnit));
                        preyUnit.SubPrey.Remove(subUnits[i]);
                    }
                }
                RemovePrey(preyUnit);

            }
            else if (actor.Infected)
            {
                actor.Damage(totalHeal, false, false);
                totalHeal = 0;
            }

        }
        else
        {
            foreach (IVoreCallback callback in Callbacks)
            {
                if (!callback.OnDigestion(preyUnit, actor, location))
                    return 0;
            }
            preyUnit.TurnsDigested++;
            AlivePrey++;
            preyUnit.UpdateEscapeRate();
            float escapeMult = 1;
            if (FreeCap() < 0)
            {

                float cap = TotalCapacity();
                escapeMult = 1.4f + 2 * ((Fullness / cap) - 1);
                if (Config.OverfeedingDamage) actor.Damage((int)(FreeCap() * -1) / 2);
            }
            if (State.Rand.NextDouble() < preyUnit.EscapeRate * escapeMult && preyUnit.Actor.Surrendered == false)
            {
                if (stomach2.Contains(preyUnit))
                {
                    stomach.Add(preyUnit);
                    stomach2.Remove(preyUnit);
                    TacticalUtilities.Log.RegisterPartialEscape(unit, preyUnit.Unit, Location(preyUnit));
                    preyUnit.TurnsBeingSwallowed = -5; //Gets 7 turns before it gets forced down again, so it needs to escape twice in 7 turns
                }
                else
                {
                    State.GameManager.TacticalMode.TacticalStats.RegisterEscape(unit.Side);
                    AlivePrey--;
                    TacticalUtilities.Log.RegisterEscape(unit, preyUnit.Unit, Location(preyUnit));
                    FreePrey(preyUnit, false);
                }

            }
        }
        return totalHeal;
    }

    private void HandleShapeshifterRebirth(Prey preyUnit)
    {
        //if (preyUnit.Unit.HasTrait(Traits.Shapeshifter) || preyUnit.Unit.HasTrait(Traits.Skinwalker)) // preserve the unit as it is and rebirth a copy of it instead
        //{
        //    Unit clone = preyUnit.Unit.Clone();
        //    preyUnit.Unit.ShifterShapes.Add(clone);
        //    clone.ShifterShapes = preyUnit.Unit.ShifterShapes;
        //    preyUnit.Unit = clone;
        //}
    }

    //public List<Actor_Unit> Birth()
    //{
    //    List<Actor_Unit> released = new List<Actor_Unit>();
    //    foreach (Prey preyUnit in womb.ToList())
    //    {
    //        if (preyUnit.Unit.Health < (preyUnit.Unit.Level * -10) && Config.KuroTenkoEnabled)
    //        {
    //            int offset = 0 - (int)preyUnit.Unit.Experience;
    //            preyUnit.Unit.GiveExp(offset);
    //            Prey newPrey = null;
    //            if (!(preyUnit.Unit.ImmuneToDefections || preyUnit.Unit.Type == UnitType.Leader) && preyUnit.Unit.IsDead == false)
    //            {
    //                preyUnit.Unit.Side = unit.Side;
    //            }
    //            else
    //            {
    //                Unit newUnit = new Unit(unit.Side, unit.Race, 0, unit.Predator);
    //                Actor_Unit newActor = new Actor_Unit(preyUnit.Actor.Position, newUnit);
    //                newActor.UpdateBestWeapons();
    //                newActor.Visible = false;
    //                newActor.Targetable = false;
    //                State.GameManager.TacticalMode.DirtyPack = true;
    //                State.GameManager.TacticalMode.AddUnit(newActor);
    //                newActor.UnitSprite.UpdateSprites(newActor, false);

    //                newPrey = new Prey(newActor, actor, newActor.PredatorComponent?.prey);
    //                while (actor.PredatorComponent.birthStatBoost > 0)
    //                {
    //                    int stat = UnityEngine.Random.Range(0, 8);
    //                    newPrey.Unit.ModifyStat(stat, 1);
    //                    if (stat == 6)
    //                    {
    //                        preyUnit.Unit.Health += 2;
    //                    }
    //                    if (stat == 0)
    //                    {
    //                        preyUnit.Unit.Health += 1;
    //                    }
    //                    actor.PredatorComponent.birthStatBoost--;
    //                }
    //                AddPrey(preyUnit);
    //            }
    //            if (newPrey != null)
    //            {
    //                preyUnit.Actor.SubtractHealth(999);
    //                RemovePrey(preyUnit);
    //                TacticalUtilities.Log.RegisterBirth(unit, preyUnit.Unit, 1f, Location(preyUnit), 1);
    //                FreePrey(newPrey, true);
    //                released.Add(newPrey.Actor);
    //            }
    //            else
    //            {
    //                preyUnit.Actor.Surrendered = false;
    //                TacticalUtilities.Log.RegisterBirth(unit, preyUnit.Unit, 1f, Location(preyUnit), 1);
    //                FreePrey(preyUnit, true);
    //                released.Add(preyUnit.Actor);
    //            }
    //        }
    //    }
    //    UpdateFullness();
    //    return released;
    //}

    void CheckPredTraitAbsorption(Prey preyUnit)
    {
        bool updated = false;
        bool raceUpdated = true;
        if (unit.HasTrait(Traits.Extraction))
        {
            var possibleTraits = preyUnit.Unit.GetTraits.Where(s => unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();
            foreach (Traits trait in possibleTraits)
            {
                unit.AddPermanentTrait(trait);
                preyUnit.Unit.RemoveTrait(trait);
                updated = true;
            }
            foreach (Traits trait in preyUnit.Unit.GetTraits)
            {
                preyUnit.Unit.RemoveTrait(trait);
                unit.GiveRawExp(5);
            }
        }
        if (preyUnit.Unit.HasTrait(Traits.Donor))
        {
            int donorIndex = preyUnit.Unit.GetTraits.IndexOf(Traits.Donor);
            var donorTraits = preyUnit.Unit.GetTraits.SkipWhile((t, index) => index <= donorIndex);
            var possibleTraits = donorTraits.Where(s => unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();

            foreach (Traits trait in possibleTraits)
            {
                    unit.AddPermanentTrait(trait);
                    preyUnit.Unit.RemoveTrait(trait);
                    updated = true;
            }
        }
        if (unit.HasTrait(Traits.InfiniteAssimilation) || unit.HasTrait(Traits.Assimilate))
        {
            var possibleTraits = preyUnit.Unit.GetTraits.Where(s => unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();

            if (possibleTraits.Any())
            {
                if (unit.BaseTraitsCount >= 5 && unit.HasTrait(Traits.Assimilate))
                    unit.RemoveTrait(Traits.Assimilate);
                if (unit.HasTrait(Traits.SynchronizedEvolution))
                {
                    RaceSettingsItem item = State.RaceSettings.Get(unit.Race);
                    item.RaceTraits.Add(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                    UpdateRaceTraits();
                }
                else
                {
                    unit.AddPermanentTrait(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                    updated = true;
                }
            }
        }
        if (unit.HasTrait(Traits.AdaptiveBiology) && updated == false)
        {
            var possibleTraits = preyUnit.Unit.GetTraits.Where(s => unit.GetTraits.Contains(s) == false && State.AssimilateList.CanGet(s)).ToArray();

            if (possibleTraits.Any())
            {
                unit.AddTemporaryTrait(possibleTraits[State.Rand.Next(possibleTraits.Length)]);
                updated = true;
            }
        }
        if (updated) UpdateUnitTraits();
    }

    public void UpdateUnitTraits()
    {
        unit.ReloadTraits();
        unit.InitializeTraits();
    }

    public void UpdateRaceTraits()
    {
        if (State.World.Villages != null)
        {
            var units = StrategicUtilities.GetAllUnits();
            foreach (Unit unit in units)
            {
                unit.ReloadTraits();
            }
        }
        else
        {
            foreach (Actor_Unit actor in TacticalUtilities.Units)
            {
                actor.Unit.ReloadTraits();
                actor.Unit.InitializeTraits();
                actor.Unit.UpdateSpells();
            }
        }
        if (State.World.AllActiveEmpires != null)
        {
            foreach (Empire emp in State.World.AllActiveEmpires)
            {
                if (emp.Side > 300)
                    continue;
                var raceFlags = State.RaceSettings.GetRaceTraits(emp.ReplacedRace);
                if (raceFlags != null)
                {
                    if (raceFlags.Contains(Traits.Prey))
                        emp.CanVore = false;
                    else
                        emp.CanVore = true;
                }
            }
        }
    }

    private void DigestionEffect(Prey preyUnit, Actor_Unit existingPredator)
    {
        if (Config.BurpOnDigest)
        {
            if (stomach.Contains(preyUnit) || stomach2.Contains(preyUnit))
            {
                if (State.Rand.NextDouble() < Config.BurpFraction)
                {
                    if (actor.Unit.Race == Race.Taraluxia && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
                    {State.GameManager.SoundManager.PlayDigest(Location(preyUnit), actor);}
                    else if (actor.Unit.Race == Race.Taraluxia)
                    {
                        actor.SetBurpMode();
                        State.GameManager.SoundManager.PlayBurp(actor);
                    }
                    else
                    {
                        actor.SetBurpMode();
                        State.GameManager.SoundManager.PlayBurp(actor);
                    }
                }
                else
                    State.GameManager.SoundManager.PlayDigest(Location(preyUnit), actor);
            }
            else
                State.GameManager.SoundManager.PlayDigest(Location(preyUnit), actor);
        }
        else
        {
            State.GameManager.SoundManager.PlayDigest(Location(preyUnit), actor);
        }

        DefaultRaceData preyRace = Races.GetRace(preyUnit.Unit);

        if (Config.ClothingDiscards && !actor.Unit.HasTrait(Traits.TotalAbsorption))
        {
            if (preyRace.AllowedMainClothingTypes.Count >= preyUnit.Unit.ClothingType && preyUnit.Unit.ClothingType > 0)
            {
                int clothingType = preyRace.AllowedMainClothingTypes[preyUnit.Unit.ClothingType - 1].Type;
                int color;
                if (preyRace.AllowedMainClothingTypes[preyUnit.Unit.ClothingType - 1].DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }
            if (preyRace.AllowedWaistTypes.Count >= preyUnit.Unit.ClothingType2 && preyUnit.Unit.ClothingType2 > 0)
            {
                int clothingType2 = preyRace.AllowedWaistTypes[preyUnit.Unit.ClothingType2 - 1].Type;
                int color;
                if (preyRace.AllowedWaistTypes[preyUnit.Unit.ClothingType2 - 1].DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType2, color, preyUnit.Unit.Name);
            }
            if (preyRace.ExtraMainClothing1Types.Count >= preyUnit.Unit.ClothingExtraType1 && preyUnit.Unit.ClothingExtraType1 > 0)
            {
                int clothingType = preyRace.ExtraMainClothing1Types[preyUnit.Unit.ClothingExtraType1 - 1].Type;
                int color;
                if (preyRace.ExtraMainClothing1Types[preyUnit.Unit.ClothingExtraType1 - 1].DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }
            if (preyRace.ExtraMainClothing2Types.Count >= preyUnit.Unit.ClothingExtraType2 && preyUnit.Unit.ClothingExtraType2 > 0)
            {
                int clothingType = preyRace.ExtraMainClothing2Types[preyUnit.Unit.ClothingExtraType2 - 1].Type;
                int color;
                if (preyRace.ExtraMainClothing2Types[preyUnit.Unit.ClothingExtraType2 - 1].DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }
            if (preyRace.ExtraMainClothing3Types.Count >= preyUnit.Unit.ClothingExtraType3 && preyUnit.Unit.ClothingExtraType3 > 0)
            {
                int clothingType = preyRace.ExtraMainClothing3Types[preyUnit.Unit.ClothingExtraType3 - 1].Type;
                int color;
                if (preyRace.ExtraMainClothing3Types[preyUnit.Unit.ClothingExtraType3 - 1].DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }
            if (preyRace.ExtraMainClothing4Types.Count >= preyUnit.Unit.ClothingExtraType4 && preyUnit.Unit.ClothingExtraType4 > 0)
            {
                int clothingType = preyRace.ExtraMainClothing4Types[preyUnit.Unit.ClothingExtraType4 - 1].Type;
                int color;
                if (preyRace.ExtraMainClothing4Types[preyUnit.Unit.ClothingExtraType4 - 1].DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }
            if (preyRace.ExtraMainClothing5Types.Count >= preyUnit.Unit.ClothingExtraType5 && preyUnit.Unit.ClothingExtraType5 > 0)
            {
                int clothingType = preyRace.ExtraMainClothing5Types[preyUnit.Unit.ClothingExtraType5 - 1].Type;
                int color;
                if (preyRace.ExtraMainClothing5Types[preyUnit.Unit.ClothingExtraType5 - 1].DiscardUsesColor2)
                    color = preyUnit.Unit.ClothingColor2;
                else
                    color = preyUnit.Unit.ClothingColor;
                State.GameManager.TacticalMode.CreateDiscardedClothing(GetCurrentLocation(), preyUnit.Unit.Race, clothingType, color, preyUnit.Unit.Name);
            }
        }



    }

    private void AbsorptionEffect(Prey preyUnit, PreyLocation location)
    {
        if (location == PreyLocation.stomach || location == PreyLocation.stomach2)
        {
            if (!Config.BurpOnDigest && State.Rand.NextDouble() < Config.BurpFraction)
            {
                actor.SetBurpMode();
                State.GameManager.SoundManager.PlayBurp(actor);
            }
            else
            {
                State.GameManager.SoundManager.PlayAbsorb(location, actor);
            }
            if (Config.FartOnAbsorb && (location == PreyLocation.stomach || location == PreyLocation.stomach2 || location == PreyLocation.anal) && State.Rand.NextDouble() < Config.FartFraction)
            {
                State.GameManager.SoundManager.PlayFart(actor);
            }

            if (actor.Unit.HasTrait(Traits.TotalAbsorption) || preyUnit.Unit.Race == Race.Iliijiith) {}
            else if (Config.Scat && preyUnit.ScatDisabled == false)
            {
                State.GameManager.SoundManager.PlayAbsorb(location, actor);
                if ((preyUnit.Unit.Race == Race.Slimes || preyUnit.Unit.Race == Race.FeralSlime) && Config.CleanDisposal == false)
                {
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneTypes.SlimePile, preyUnit.Unit.Name, preyUnit.Unit.AccessoryColor);
                }
                else if (Config.CleanDisposal == true && actor.Unit.Race == Race.Tatltuae) {}
                else
                    State.GameManager.TacticalMode.CreateScat(GetCurrentLocation(), new ScatInfo(unit, preyUnit));
            }
            else
            {
                GenerateBones(preyUnit);
            }
        }
        else if (location == PreyLocation.balls)
        {
            State.GameManager.SoundManager.PlayAbsorb(location, actor);
            if (actor.Unit.HasTrait(Traits.TotalAbsorption) || preyUnit.Unit.Race == Race.Iliijiith) {}
            else if (Config.Cumstains)
            {
                if (unit.Race == Race.Selicia)
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneTypes.CumPuddle, preyUnit.Unit.Name, 0);
                else if (Config.CondomsForCV)
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneTypes.DisposedCondom, preyUnit.Unit.Name);
                else 
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneTypes.CumPuddle, preyUnit.Unit.Name);
            }
        }
        else if (location == PreyLocation.womb || (location == PreyLocation.breasts && unit.Race != Race.Kangaroos) || location == PreyLocation.leftBreast || location == PreyLocation.rightBreast)
        {
            State.GameManager.SoundManager.PlayAbsorb(location, actor);
            if (actor.Unit.HasTrait(Traits.TotalAbsorption) || preyUnit.Unit.Race == Race.Iliijiith) {}
            else if (Config.Cumstains)
            {
                if (unit.Race == Race.Selicia)
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneTypes.CumPuddle, preyUnit.Unit.Name, 0);
                else if (unit.Race == Race.Bees)
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneTypes.HoneyPuddle, preyUnit.Unit.Name);
                else
                    State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneTypes.CumPuddle, preyUnit.Unit.Name);
            }
        }
        else if (location == PreyLocation.tail && unit.Race == Race.Bees && Config.Cumstains)
        {
            State.GameManager.SoundManager.PlayAbsorb(location, actor);
            if (actor.Unit.HasTrait(Traits.TotalAbsorption) || preyUnit.Unit.Race == Race.Iliijiith) {}
            else
                State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), BoneTypes.HoneyPuddle, preyUnit.Unit.Name);
        }
    }

    private void GenerateBones(Prey preyUnit)
    {
        if (Config.Bones)
        {
            List<BoneInfo> bonesInfos = preyUnit.GetBoneTypes();
            foreach (BoneInfo bonesInfo in bonesInfos)
            {
                State.GameManager.TacticalMode.CreateMiscDiscard(GetCurrentLocation(), bonesInfo.boneTypes, bonesInfo.name, bonesInfo.accessoryColor);
            }
        }
    }

    internal void UpdateTransition()
    {
        if (StomachTransition.transitionTime < StomachTransition.transitionLength)
        {
            if (unit.Race == Race.FeralFrogs && actor.IsOralVoring && actor.IsOralVoringHalfOver == false)
                return;
            StomachTransition.transitionTime += Time.deltaTime;
            VisibleFullness = Mathf.Lerp(StomachTransition.transitionStart, StomachTransition.transitionEnd, StomachTransition.transitionTime / StomachTransition.transitionLength);
        }
        if (BallsTransition.transitionTime < BallsTransition.transitionLength)
        {
            BallsTransition.transitionTime += Time.deltaTime;
            BallsFullness = Mathf.Lerp(BallsTransition.transitionStart, BallsTransition.transitionEnd, BallsTransition.transitionTime / BallsTransition.transitionLength);
        }
        if (LeftBreastTransition.transitionTime < LeftBreastTransition.transitionLength)
        {
            LeftBreastTransition.transitionTime += Time.deltaTime;
            LeftBreastFullness = Mathf.Lerp(LeftBreastTransition.transitionStart, LeftBreastTransition.transitionEnd, LeftBreastTransition.transitionTime / LeftBreastTransition.transitionLength);
        }
        if (RightBreastTransition.transitionTime < RightBreastTransition.transitionLength)
        {
            RightBreastTransition.transitionTime += Time.deltaTime;
            RightBreastFullness = Mathf.Lerp(RightBreastTransition.transitionStart, RightBreastTransition.transitionEnd, RightBreastTransition.transitionTime / RightBreastTransition.transitionLength);
        }

    }

    internal void UpdateFullness()
    {
        float fullnessFactor = 2.25f / unit.GetScale(2);
        float fullness = 0;
        float stomachFullness = 0;
        float breastFullness = 0;
        float leftBreastFullness = 0;
        float rightBreastFullness = 0;
        float ballsFullness = 0;
        float tailFullness = 0;
        float stomach2ndFullness = 0;
        float wombFullness = 0;
        float exclusiveStomachFullness = 0;
        foreach (Prey preyUnit in prey.ToList()) //ToList to cover the rare case it needs to do the pop unit out of itself condition in the bulk function. (It has happened, once at least)
        {
            var location = Location(preyUnit);
            fullness += preyUnit.Actor.Bulk();
            if (location == PreyLocation.breasts)
            {
                // This condition is for kangaroo pouch vore, when proper pouch sprites are implemented, remove it
                if (actor.Unit.Race == Race.Kangaroos)
                    stomachFullness += preyUnit.Actor.Bulk();
                breastFullness += preyUnit.Actor.Bulk();
            }
            else if (location == PreyLocation.balls)
            {
                ballsFullness += preyUnit.Actor.Bulk();
            }
            else if (location == PreyLocation.stomach2)
            {
                stomach2ndFullness += preyUnit.Actor.Bulk();
            }
            else if (location == PreyLocation.tail)
            {
                tailFullness += preyUnit.Actor.Bulk();
            }
            else if (location == PreyLocation.leftBreast)
            {
                leftBreastFullness += preyUnit.Actor.Bulk();
            }
            else if (location == PreyLocation.rightBreast)
            {
                rightBreastFullness += preyUnit.Actor.Bulk();
            }
            else
            {
                if (location == PreyLocation.womb)
                    wombFullness += preyUnit.Actor.Bulk();
                else
                    exclusiveStomachFullness += preyUnit.Actor.Bulk();
                stomachFullness += preyUnit.Actor.Bulk();
            }

        }

        float stomachSize = State.RaceSettings.GetStomachSize(unit.Race);
        if (State.RaceSettings.Exists(unit.Race))
        {
            stomachSize = State.RaceSettings.GetStomachSize(unit.Race);
        }
        float newStomach = fullnessFactor * stomachFullness / stomachSize;
        if (newStomach > 0)
            StomachTransition = new Transition(Math.Abs(newStomach - VisibleFullness) / 4, VisibleFullness, newStomach);
        else
        {
            StomachTransition = new Transition(0, 0, 0);
            VisibleFullness = 0;
        }

        float newBalls = fullnessFactor * ballsFullness / stomachSize;
        if (newBalls > 0)
            BallsTransition = new Transition(Math.Abs(newBalls - BallsFullness) / 4, BallsFullness, newBalls);
        else
        {
            BallsTransition = new Transition(0, 0, 0);
            BallsFullness = 0;
        }

        Fullness = fullnessFactor * fullness / stomachSize;

        TailFullness = fullnessFactor * tailFullness / stomachSize;


        WombFullness = fullnessFactor * wombFullness / stomachSize;

        ExclusiveStomachFullness = fullnessFactor * exclusiveStomachFullness / stomachSize;


        Stomach2ndFullness = fullnessFactor * stomach2ndFullness / stomachSize;
        CombinedStomachFullness = fullnessFactor * (stomach2ndFullness + stomachFullness) / stomachSize;
        if (breastFullness <= 0) breastFullness = -1;
        BreastFullness = breastFullness;

        float newLeftBreast = fullnessFactor * leftBreastFullness / stomachSize;
        float newRightBreast = fullnessFactor * rightBreastFullness / stomachSize;
        if (Config.FairyBVType == FairyBVType.Shared)
        {
            newLeftBreast = (newLeftBreast + newRightBreast) / 2;
            newRightBreast = newLeftBreast;
        }
        if (newLeftBreast > 0)
            LeftBreastTransition = new Transition(Math.Abs(newLeftBreast - LeftBreastFullness) / 4, LeftBreastFullness, newLeftBreast);
        else
        {
            LeftBreastTransition = new Transition(0, 0, 0);
            LeftBreastFullness = 0;
        }
        if (newRightBreast > 0)
            RightBreastTransition = new Transition(Math.Abs(newRightBreast - RightBreastFullness) / 4, RightBreastFullness, newRightBreast);
        else
        {
            RightBreastTransition = new Transition(0, 0, 0);
            RightBreastFullness = 0;
        }

        var data = Races.GetRace(unit.Race);
        if (data.ExtendedBreastSprites == false)
            actor.Unit.SetBreastSize(unit.DefaultBreastSize + (int)(BreastFullness * 8));
        //actor.Unit.SetDickSize(unit.DefaultDickSize + (int)(BallsFullness * 8));
    }

    bool PlacedPrey(Actor_Unit prey)
    {
        Vec2i p = new Vec2i(0, 0);
        for (int i = 0; i < 8; i++)
        {
            p = actor.GetPos(i);
            if (TacticalUtilities.OpenTile(p.x, p.y, actor))
            {
                prey.SetPos(p);
                return true;
            }
        }
        for (int x = -2; x <= 2; x++) //Inefficient but should be rarely needed
        {
            for (int y = -2; y <= 2; y++)
            {
                p.x = actor.Position.x + x;
                p.y = actor.Position.y + y;
                if (TacticalUtilities.OpenTile(p.x, p.y, actor))
                {
                    prey.SetPos(p);
                    return true;
                }
            }
        }
        return false;
    }

    public string GetPreyInformation()
    {
        string ret = "";
        for (int x = 0; x < prey.Count; x++)
        {
            AddPreyInformation(ref ret, prey[x], 0);
        }
        return ret;
    }

    string AddPreyInformation(ref string ret, Prey prey, int indent)
    {
        if (indent > 12)
            return "";
        if (prey.Unit == null)
            return "";
        var loc = prey.Location.ToString();
        if (unit.Race == Race.Terrorbird && prey.Location == PreyLocation.tail)
            loc = "crop";
        if (unit.Race == Race.Kangaroos && prey.Location == PreyLocation.breasts)
            loc = "pouch";
        if (prey.Unit.IsDead == false && unit.HasTrait(Traits.DualStomach) && stomach.Contains(prey))
        {
            if (indent > 0) ret += $"L:{indent} ";
            ret += $"Pushing {prey.Unit.Name} deeper\n";
            if (Config.ExtraTacticalInfo)
            {
                prey.UpdateEscapeRate();
                if (unit.HasTrait(Traits.Endosoma) && unit.Side != prey.Unit.Side)
                    ret += $" loc: {loc}\n escape: {(prey.Unit.Stamina > 0 ? Math.Round(prey.EscapeRate * 100, 2) : 0)}%\n stamina: {(prey.Unit.Stamina > 0 ? Math.Round(prey.Unit.StamPct * 100, 1) : 0)}%\n";
                else
                    ret += $" loc: {loc}\n escape: {Math.Round(prey.EscapeRate * 100, 2)}%\n health: {Math.Round(prey.Unit.HealthPct * 100, 1)}%\n";

            }

        }
        else if (prey.Unit.IsDead == false)
        {
            if (indent > 0) ret += $"L:{indent} ";
            ret += $"Digesting {prey.Unit.Name}\n";
            if (Config.ExtraTacticalInfo)
            {
                prey.UpdateEscapeRate();
                if (unit.HasTrait(Traits.Endosoma) && unit.Side != prey.Unit.Side)
                    ret += $" loc: {loc}\n escape: {(prey.Unit.Stamina > 0 ? Math.Round(prey.EscapeRate * 100, 2) : 0)}%\n stamina: {(prey.Unit.Stamina > 0 ? Math.Round(prey.Unit.StamPct * 100, 1) : 0)}%\n";
                else
                    ret += $" loc: {loc}\n escape: {Math.Round(prey.EscapeRate * 100, 2)}%\n health: {Math.Round(prey.Unit.HealthPct * 100, 1)}%\n";
            }

        }
        else
        {
            if (indent > 0) ret += $"L:{indent}";
            ret += ($"Absorbing {prey.Unit.Name}\n");
            if (Config.ExtraTacticalInfo)
                ret += $" loc: {loc}\n absorbed: {Math.Round((float)-prey.Unit.Health / prey.Unit.MaxHealth * 100, 1)}%\n";
        }
        if (prey.SubPrey != null)
        {
            foreach (Prey sub in prey.SubPrey)
            {
                AddPreyInformation(ref ret, sub, indent + 1);
            }
        }
        return ret;
    }

    private void AddToStomach(Prey preyref, float v)
    {
        stomach.Add(preyref);
        if (actor.UnitSprite != null)
        {
            actor.UnitSprite.UpdateSprites(actor, true);
            actor.UnitSprite.AnimateBellyEnter();
        }
    }

    public bool UsePreferredVore(Actor_Unit target)
    {
        List<VoreType> allowedVoreTypes = State.RaceSettings.GetVoreTypes(unit.Race);

        switch (unit.PreferredVoreType)
        {
            case VoreType.All:
                break;
            case VoreType.Oral:
                if (allowedVoreTypes.Contains(VoreType.Oral))
                    return Consume(target, Devour, PreyLocation.stomach);
                break;
            case VoreType.Unbirth:
                if (allowedVoreTypes.Contains(VoreType.Unbirth) && CanUnbirth(target))
                    return Consume(target, Unbirth, PreyLocation.womb);
                break;
            case VoreType.CockVore:
                if (allowedVoreTypes.Contains(VoreType.CockVore) && CanCockVore(target))
                    return Consume(target, CockVore, PreyLocation.balls);
                break;
            case VoreType.BreastVore:
                if (allowedVoreTypes.Contains(VoreType.BreastVore) && CanBreastVore(target))
                    return Consume(target, BreastVore, PreyLocation.breasts);
                break;
            case VoreType.TailVore:
                if (allowedVoreTypes.Contains(VoreType.TailVore) && CanTailVore(target))
                    return Consume(target, TailVore, PreyLocation.tail);
                break;
            case VoreType.Anal:
                if (allowedVoreTypes.Contains(VoreType.Anal) && CanAnalVore(target))
                    return Consume(target, AnalVore, PreyLocation.anal);
                break;
        }
        if (State.GameManager.TacticalMode.turboMode) //When turboing, just pick the fast solution.
            return Consume(target, Devour, PreyLocation.stomach);

        WeightedList<VoreType> options = new WeightedList<VoreType>();

        List<VoreType> voreTypes = new List<VoreType>();
        if (allowedVoreTypes.Contains(VoreType.Oral) && Config.OralWeight > 0)
            options.Add(VoreType.Oral, Config.OralWeight);
        if (allowedVoreTypes.Contains(VoreType.Unbirth) && CanUnbirth(target) && Config.UnbirthWeight > 0 && (actor.BodySize() >= target.BodySize() * 3 || !actor.Unit.HasTrait(Traits.TightNethers)))
            options.Add(VoreType.Unbirth, Config.UnbirthWeight);
        if (allowedVoreTypes.Contains(VoreType.CockVore) && CanCockVore(target) && Config.CockWeight > 0 && (actor.BodySize() >= target.BodySize() * 3 || !actor.Unit.HasTrait(Traits.TightNethers)))
            options.Add(VoreType.CockVore, Config.CockWeight);
        if (allowedVoreTypes.Contains(VoreType.BreastVore) && CanBreastVore(target) && Config.BreastWeight > 0)
            options.Add(VoreType.BreastVore, Config.BreastWeight);
        if (allowedVoreTypes.Contains(VoreType.TailVore) && CanTailVore(target) && Config.TailWeight > 0)
            options.Add(VoreType.TailVore, Config.TailWeight);
        if (allowedVoreTypes.Contains(VoreType.Anal) && CanAnalVore(target) && Config.AnalWeight > 0)
            options.Add(VoreType.Anal, Config.AnalWeight);

        var type = options.GetResult();

        if (type == VoreType.All || type == VoreType.Oral)
            return Consume(target, Devour, PreyLocation.stomach);

        if (type == VoreType.Unbirth)
            return Consume(target, Unbirth, PreyLocation.womb);
        if (type == VoreType.CockVore)
            return Consume(target, CockVore, PreyLocation.balls);
        if (type == VoreType.BreastVore)
            return Consume(target, BreastVore, PreyLocation.breasts);
        if (type == VoreType.TailVore)
            return Consume(target, TailVore, PreyLocation.tail);
        if (type == VoreType.Anal)
            return Consume(target, AnalVore, PreyLocation.anal);
        return Consume(target, Devour, PreyLocation.stomach);
    }

    internal bool Devour(Actor_Unit target, float delay = 0)
    {
        if (delay == 0)
            return Consume(target, Devour, PreyLocation.stomach);
        else
            return Consume(target, Devour, PreyLocation.stomach, delay);
    }

    internal bool Unbirth(Actor_Unit target)
    {
        return Consume(target, Unbirth, PreyLocation.womb);
    }

    internal bool BreastVore(Actor_Unit target)
    {
        return Consume(target, BreastVore, PreyLocation.breasts);
    }

    internal bool CockVore(Actor_Unit target)
    {
        return Consume(target, CockVore, PreyLocation.balls);
    }

    internal bool TailVore(Actor_Unit target)
    {
        return Consume(target, TailVore, PreyLocation.tail);
    }

    internal bool AnalVore(Actor_Unit target)
    {
        return Consume(target, AnalVore, PreyLocation.anal);
    }

    internal bool MagicConsume(Spell spell, Actor_Unit target, PreyLocation preyLocation = PreyLocation.stomach)
    {
        bool sneakAttack = false;
        if (TacticalUtilities.SneakAttackCheck(actor.Unit, target.Unit) && (!actor.Unit.HasTrait(Traits.FriendlyStomach) || !actor.Unit.HasTrait(Traits.Endosoma)))
        {
            actor.Unit.hiddenFixedSide = false;
            sneakAttack = true;
            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=purple>{actor.Unit.Name} sneak-attacked {target.Unit.Name}!</color>");
        }
        if (actor.Unit.GetApparentSide() != target.Unit.GetApparentSide())
        {
            if (actor.sidesAttackedThisBattle == null)
                actor.sidesAttackedThisBattle = new List<int>();
            actor.sidesAttackedThisBattle.Add(target.Unit.GetApparentSide());
        }
        State.GameManager.TacticalMode.AITimer = Config.TacticalVoreDelay;
        if (State.GameManager.CurrentScene == State.GameManager.TacticalMode && State.GameManager.TacticalMode.IsPlayerInControl == false && State.GameManager.TacticalMode.turboMode == false)
            State.GameManager.CameraCall(target.Position);
        if (target.Unit == unit)
            return false;
        if (target.DefendSpellCheck(spell, actor, out float chance, mod: sneakAttack ? -0.3f : 0f) == false)
        {
            State.GameManager.TacticalMode.Log.RegisterSpellMiss(unit, target.Unit, spell.SpellType, chance);
            return false;
        }

        if (target.Bulk() <= FreeCap())
        {
            //actor.SetPredMode(preyType);
            float r = (float)State.Rand.NextDouble();
            float v = target.GetDevourChance(actor, skillBoost: actor.Unit.GetStat(Stat.Mind));
            if (r < v)
            {
                if (target.Unit.IsDead == false)
                    AlivePrey++;
                State.GameManager.TacticalMode.TacticalStats.RegisterVore(unit.Side);

                if (target.Unit.Side == unit.Side)
                    State.GameManager.TacticalMode.TacticalStats.RegisterAllyVore(unit.Side);
                if (!(target.Unit.FixedSide == unit.GetApparentSide(target.Unit)) || !unit.HasTrait(Traits.FriendlyStomach) || !unit.HasTrait(Traits.Endosoma))
                {
                    unit.GiveScaledExp(4 * target.Unit.ExpMultiplier, unit.Level - target.Unit.Level, true);
                }
                target.Visible = false;
                target.Targetable = false;
                State.GameManager.TacticalMode.Log.RegisterSpellHit(unit, target.Unit, spell.SpellType, 0, chance);
                State.GameManager.TacticalMode.DirtyPack = true;

                target.Movement = 0;
                Prey preyref = new Prey(target, actor, target.PredatorComponent?.prey);
                MagicDevour(target, v, preyref, preyLocation);
                AddPrey(preyref);
                UpdateFullness();
                return true;
            }
            else
            {
                State.GameManager.TacticalMode.Log.RegisterSpellMiss(unit, target.Unit, spell.SpellType, chance);
                return false;
            }


        }

        return false;

    }

    bool Consume(Actor_Unit target, Action<Actor_Unit, float, Prey, float> action, PreyLocation preyType, float delay = 0)
    {
        State.GameManager.TacticalMode.AITimer = Config.TacticalVoreDelay;
        int boost = 0;
        if (State.GameManager.CurrentScene == State.GameManager.TacticalMode && State.GameManager.TacticalMode.IsPlayerInControl == false && State.GameManager.TacticalMode.turboMode == false)
            State.GameManager.CameraCall(target.Position);
        if (TacticalUtilities.AppropriateVoreTarget(actor, target) == false)
        {
            return false;
        }
        if (unit.HasTrait(Traits.RangedVore))
        {
            int dist = target.Position.GetNumberOfMovesDistance(actor.Position);
            if (dist > 4)
                return false;
            boost = -3 * (dist - 1);
        }
        else if (target.Position.GetNumberOfMovesDistance(actor.Position) > 1)
        {
            return false;
        }
        if (actor.IsAllIn)
        {
            boost += 50;
        }
        else if (actor.Movement == 0)
        {
            return false;
        }
        if (target.Unit == unit)
            return false;
        if (unit.CanVore(preyType) != CanVore(preyType, target))
            return false;
        if (target.Bulk() <= FreeCap() && (actor.BodySize() >= target.BodySize() * 3 || !actor.Unit.HasTrait(Traits.TightNethers) || (preyType != PreyLocation.womb && preyType != PreyLocation.balls)))
        {
            bool bit = false;
            if (target.Unit.HasTrait(Traits.Dazzle) && target.Surrendered == false)
            {
                float chance = actor.WillCheckOdds(actor, target);
                if (State.Rand.NextDouble() < chance)
                {
                    actor.Movement = 0;
                    actor.UnitSprite.DisplayDazzle();
                    actor.Unit.ApplyStatusEffect(StatusEffectType.Shaken, 0.3f, 1);
                    TacticalUtilities.Log.RegisterDazzle(actor.Unit, target.Unit, chance);
                    return false;
                }
            }
            actor.SetPredMode(preyType);
            if (TacticalUtilities.SneakAttackCheck(unit, target.Unit) && (!actor.Unit.HasTrait(Traits.FriendlyStomach) || !actor.Unit.HasTrait(Traits.Endosoma)))
            {
                actor.Unit.hiddenFixedSide = false;
                boost += 3;
                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<color=purple>{actor.Unit.Name} sneak-attacked {target.Unit.Name}!</color>");
            }
            if (actor.Unit.GetApparentSide() != target.Unit.GetApparentSide())
            {
                if (actor.sidesAttackedThisBattle == null)
                    actor.sidesAttackedThisBattle = new List<int>();
                actor.sidesAttackedThisBattle.Add(target.Unit.GetApparentSide());
            }
            float r = (float)State.Rand.NextDouble();
            float v = target.GetDevourChance(actor, skillBoost: boost);
            if (r < v)
            {
                target = PreformNearbyTraitCheck(target);
                PerformConsume(target, action, preyType, v, delay);
                if (actor.Unit.FixedSide == TacticalUtilities.GetMindControlSide(target.Unit))
                {
                    StatusEffect charm = target.Unit.GetStatusEffect(StatusEffectType.Charmed);
                    if (charm != null)
                    {
                        target.Unit.StatusEffects.Remove(charm);                // betrayal dispels charm
                    }
                }
                if (!State.GameManager.TacticalMode.turboMode)
                {
                    if (preyType == PreyLocation.stomach && actor.Unit.Race == Race.Ryan)
                        actor.SetVoreSuccessMode();
                    if (actor.Unit.Race != Race.Ryan)
                        actor.SetVoreSuccessMode();
                }
                if (unit.HasTrait(Traits.Tenacious))
                    unit.RemoveTenacious();
                if (unit.HasTrait(Traits.FearsomeAppetite))
                {
                    foreach (Actor_Unit victim in TacticalUtilities.UnitsWithinTiles(actor.Position, 3).Where(s => s.Unit.IsEnemyOfSide(unit.Side)))
                    {
                        victim.Unit.ApplyStatusEffect(StatusEffectType.Shaken, 0.2f, 3);
                    }
                }
            }
            else
            {
                if (!State.GameManager.TacticalMode.turboMode)
                    actor.SetVoreFailMode();
                if (actor.Unit.HasTrait(Traits.Biter))
                {
                    int oldMP = actor.Movement;
                    actor.Attack(target, false, true, canKill: false);
                    actor.Movement = oldMP;
                    bit = true; //Used because killed units are considered to be surrendered, so this prevents a bite that kills a unit being counted as 2 AP
                }
                else
                {
                    target.UnitSprite.DisplayResist();
                    if (unit.HasTrait(Traits.Tenacious))
                        unit.AddTenacious();
                }

            }

            if (bit == false && (target.Surrendered || ((unit.HasTrait(Traits.FriendlyStomach) || unit.HasTrait(Traits.Endosoma)) && (target.Unit.FixedSide == unit.GetApparentSide(target.Unit))) || target.Unit.GetStatusEffect(StatusEffectType.Hypnotized)?.Strength == actor.Unit.FixedSide))
                actor.Movement = Math.Max(actor.Movement - 2, 0);
            else
            {
                SpendVoreMP();
            }



            return r < v;
        }
        return false;
    }

    private void SpendVoreMP()
    {
        if (unit.TraitBoosts.VoreAttacks > 1)
        {
            int movementFraction = 1 + actor.MaxMovement() / unit.TraitBoosts.VoreAttacks;
            if (actor.Movement > movementFraction)
                actor.Movement -= movementFraction;
            else
                actor.Movement = 0;
        }
        else
            actor.Movement = 0;
    }

    private Actor_Unit PreformNearbyTraitCheck(Actor_Unit target)
    {
        Actor_Unit ret = target;
        foreach (Actor_Unit checkedunit in TacticalUtilities.UnitsWithinTiles(actor.Position, 3))
        {
            if (!checkedunit.Unit.IsEnemyOfSide(target.Unit.Side))
            {
                if (checkedunit.Unit.HasTrait(Traits.EnviousPrey))
                {
                    if (State.Rand.Next(10) == 0)
                    {
                        checkedunit.UnitSprite.DisplayCharm();
                        checkedunit.Unit.ApplyStatusEffect(StatusEffectType.Charmed, actor.Unit.GetApparentSide(checkedunit.Unit), 3);
                        checkedunit.Unit.ApplyStatusEffect(StatusEffectType.Temptation, actor.Unit.GetApparentSide(checkedunit.Unit), 3);
                    }
                }
            }

            if (checkedunit.Unit.HasTrait(Traits.CompetetivePredator))
            {
                if (checkedunit.SelfPrey == null && State.Rand.Next(10) == 0)
                {
                    var targets = TacticalUtilities.UnitsWithinTiles(checkedunit.Position, 1).Where(u => checkedunit != u).ToList();
                    if (targets.Any())
                    {
                        var pot_target = targets[State.Rand.Next(0, targets.Count() - 1)];
                        checkedunit.PredatorComponent.UsePreferredVore(pot_target);
                    }
                }
            }

            if (checkedunit.Unit.HasTrait(Traits.CurseOfSacrifice))
            {
                if (checkedunit.Targetable && checkedunit != actor)
                {
                    if (State.Rand.Next(10) == 0)
                    {
                        checkedunit.SetPos(target.Position);
                        target.SetPos(checkedunit.Position);
                        ret = checkedunit;
                    }
                }
            }
        }
        return ret;
    }

    void PerformConsume(Actor_Unit target, Action<Actor_Unit, float, Prey, float> action, PreyLocation preyType, float odds = 1f, float delay = 0f)
    {
        if (target.Unit.IsDead == false)
            AlivePrey++;
        State.GameManager.TacticalMode.TacticalStats.RegisterVore(unit.Side);

        if (target.Unit.Side == unit.Side)
            State.GameManager.TacticalMode.TacticalStats.RegisterAllyVore(unit.Side);
        target.Visible = false;
        target.Targetable = false;
        State.GameManager.TacticalMode.DirtyPack = true;
        if (!(target.Unit.FixedSide == unit.GetApparentSide(target.Unit)) || !(unit.HasTrait(Traits.FriendlyStomach) || unit.HasTrait(Traits.Endosoma)))
        {
            unit.GiveScaledExp(4 * target.Unit.ExpMultiplier, unit.Level - target.Unit.Level, true);
        }
        target.Movement = 0;
        Prey preyref = new Prey(target, actor, target.PredatorComponent?.prey);
        action(target, odds, preyref, delay);
        AddPrey(preyref);
        UpdateFullness();
    }

    void MagicDevour(Actor_Unit target, float v, Prey preyref, PreyLocation preyLocation)
    {
        if (actor.Unit.Side == target.Unit.GetApparentSide() && !(actor.Unit.HasTrait(Traits.FriendlyStomach) || actor.Unit.HasTrait(Traits.Endosoma)))
        {
            actor.Unit.hiddenFixedSide = false;
        }
        //State.GameManager.SoundManager.PlaySwallow(PreyLocation.stomach, actor);
        //TacticalUtilities.Log.RegisterVore(unit, target.Unit, v);
        if (actor.Unit.FixedSide == TacticalUtilities.GetMindControlSide(target.Unit))
        {
            StatusEffect charm = target.Unit.GetStatusEffect(StatusEffectType.Charmed);
            if (charm != null)
            {
                target.Unit.StatusEffects.Remove(charm);                // betrayal dispels charm
            }
        }
        switch (preyLocation)
        {
            case PreyLocation.womb:
                AddToWomb(preyref, 1f);
                break;
            case PreyLocation.balls:
                AddToBalls(preyref, 1f);
                break;
            case PreyLocation.anal:
                AddToStomach(preyref, 1f);
                break;
            case PreyLocation.breasts:
                var data = Races.GetRace(unit.Race);
                if (data.ExtendedBreastSprites)
                {
                    if (Config.FairyBVType == FairyBVType.Picked && State.GameManager.TacticalMode.IsPlayerInControl)
                    {
                        var box = State.GameManager.CreateDialogBox();
                        box.SetData(() => { rightBreast.Add(preyref); UpdateFullness(); }, "Right", "Left", "Which breast should the prey be put in? (from your pov)", () => { leftBreast.Add(preyref); UpdateFullness(); });
                        return;
                    }
                    if (LeftBreastFullness < RightBreastFullness || State.Rand.Next(2) == 0)
                        leftBreast.Add(preyref);
                    else
                        rightBreast.Add(preyref);
                }
                else
                    breasts.Add(preyref);
                break;
            default:
                AddToStomach(preyref, 1f);
                break;
        }
        AddToStomach(preyref, v);
    }

    void Devour(Actor_Unit target, float v, Prey preyref, float delay)
    {
        if (delay > 0)
            MiscUtilities.DelayedInvoke(() => State.GameManager.SoundManager.PlaySwallow(PreyLocation.stomach, actor), delay);
        else
            State.GameManager.SoundManager.PlaySwallow(PreyLocation.stomach, actor);
        TacticalUtilities.Log.RegisterVore(unit, target.Unit, v);
        AddToStomach(preyref, v);
    }

    void Unbirth(Actor_Unit target, float v, Prey preyref, float delay)
    {
        State.GameManager.SoundManager.PlaySwallow(PreyLocation.womb, actor);
        TacticalUtilities.Log.RegisterUnbirth(unit, target.Unit, v);
        AddToWomb(preyref, v);
    }

    void BreastVore(Actor_Unit target, float v, Prey preyref, float delay)
    {
        State.GameManager.SoundManager.PlaySwallow(PreyLocation.breasts, actor);
        TacticalUtilities.Log.RegisterBreastVore(unit, target.Unit, v);
        var data = Races.GetRace(unit.Race);
        if (data.ExtendedBreastSprites)
        {
            if (Config.FairyBVType == FairyBVType.Picked && State.GameManager.TacticalMode.IsPlayerInControl)
            {
                var box = State.GameManager.CreateDialogBox();
                box.SetData(() => { rightBreast.Add(preyref); UpdateFullness(); }, "Right", "Left", "Which breast should the prey be put in? (from your pov)", () => { leftBreast.Add(preyref); UpdateFullness(); });
                return;
            }
            if (LeftBreastFullness < RightBreastFullness || State.Rand.Next(2) == 0)
                leftBreast.Add(preyref);
            else
                rightBreast.Add(preyref);
        }
        else
            breasts.Add(preyref);
    }

    void CockVore(Actor_Unit target, float v, Prey preyref, float delay)
    {
        State.GameManager.SoundManager.PlaySwallow(PreyLocation.balls, actor);
        TacticalUtilities.Log.RegisterCockVore(unit, target.Unit, v);
        balls.Add(preyref);
    }

    void TailVore(Actor_Unit target, float v, Prey preyref, float delay)
    {
        State.GameManager.SoundManager.PlaySwallow(PreyLocation.stomach, actor);
        TacticalUtilities.Log.RegisterTailVore(unit, target.Unit, v);
        tail.Add(preyref);
    }

    void AnalVore(Actor_Unit target, float v, Prey preyref, float delay)
    {
        State.GameManager.SoundManager.PlaySwallow(PreyLocation.anal, actor);
        TacticalUtilities.Log.RegisterAnalVore(unit, target.Unit, v);
        stomach.Add(preyref);
    }

    public bool CanFeed()
    {
        if (Config.KuroTenkoEnabled && (Config.FeedingType == FeedingType.Both || Config.FeedingType == FeedingType.BreastfeedOnly))
        {
            var race = Races.GetRace(actor.Unit.Race);
            if (actor.Unit.Race == Race.Kangaroos)
                return false;
            if (race.ExtendedBreastSprites)
            {
                foreach (Prey preyUnit in leftBreast)
                {
                    if (preyUnit.Unit.IsDead)
                        return true;
                }
                foreach (Prey preyUnit in rightBreast)
                {
                    if (preyUnit.Unit.IsDead)
                        return true;
                }
            }
            else
            {
                foreach (Prey preyUnit in breasts)
                {
                    if (preyUnit.Unit.IsDead)
                        return true;
                }
            }
        }
        return false;
    }

    public bool CanFeedCum()
    {
        if (Config.KuroTenkoEnabled && (Config.FeedingType == FeedingType.Both || Config.FeedingType == FeedingType.CumFeedOnly))
        {
            foreach (Prey preyUnit in balls)
            {
                if (preyUnit.Unit.IsDead)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CanSuckle()
    {
        return (Config.KuroTenkoEnabled && Config.FeedingType != FeedingType.None);
    }

    public bool CanTransfer()
    {
        if (Config.TransferAllowed == false || Config.KuroTenkoEnabled == false)
            return false;
        return GetTransfer() != null;
    }

    public bool CanVoreSteal(Actor_Unit target)
    {
        if (Config.TransferAllowed == false || Config.KuroTenkoEnabled == false)
            return false;
        return GetVoreSteal(target) != null;
    }

    private Prey GetTransfer()
    {
        foreach (Prey preyUnit in balls)
        {
            //Testing was done with isDead being required, transferring live prey may have its own issues
            //if (preyUnit.Unit.IsDead)
            //{
            return preyUnit;
            //}
        }
        return null;
    }

    public float GetTransferBulk()
    {
        foreach (Prey preyUnit in balls)
        {
            //Testing was done with isDead being required, transferring live prey may have its own issues
            //if (preyUnit.Unit.IsDead)
            //{
            return preyUnit.Actor.Bulk();
            //}
        }
        return 0;
    }

    public bool CanKissTransfer()
    {
        if (Config.TransferAllowed == false || Config.KuroTenkoEnabled == false)
            return false;
        return GetKissTransfer() != null;
    }

    private Prey GetKissTransfer()
    {
        foreach (Prey preyUnit in stomach)
        {
            if (preyUnit.Unit.IsDead == false)
            {
            return preyUnit;
            }
        }
        foreach (Prey preyUnit in stomach2)
        {
            if (preyUnit.Unit.IsDead == false)
            {
            return preyUnit;
            }
        }
        return null;
    }

    public float GetKissTransferBulk()
    {
        foreach (Prey preyUnit in stomach)
        {
            if (preyUnit.Unit.IsDead == false)
            {
            return preyUnit.Actor.Bulk();
            }
        }
        foreach (Prey preyUnit in stomach2)
        {
            if (preyUnit.Unit.IsDead == false)
            {
            return preyUnit.Actor.Bulk();
            }
        }
        return 0;
    }

    public float GetSuckleChance(Actor_Unit target, bool includeSecondaries = false)
    {
        if (target.Surrendered || actor.Unit.GetApparentSide() == target.Unit.FixedSide)
            return 1f;
        float predVoracity = Mathf.Pow(15 + actor.Unit.GetStat(Stat.Voracity), 1.5f);
        float predStrength = Mathf.Pow(15 + actor.Unit.GetStat(Stat.Strength), 1.5f);
        float preyStrength = Mathf.Pow(15 + target.Unit.GetStat(Stat.Strength), 1.5f);
        float preyWill = Mathf.Pow(15 + target.Unit.GetStat(Stat.Will), 1.5f);
        float attackerScore = (predVoracity + 2 * predStrength) * (actor.Unit.HealthPct + 1) * (30 + actor.BodySize()) / 16 / (1 + 2 * Mathf.Pow(actor.PredatorComponent.UsageFraction, 2));
        float defenderScore = 20 + (2 * (preyStrength + 2 * preyWill) * target.Unit.HealthPct * target.Unit.HealthPct * ((10 + target.BodySize()) / 2));

        float odds = attackerScore / (attackerScore + defenderScore) * 100;

        odds *= target.Unit.TraitBoosts.FlatHitReduction;

        if (includeSecondaries)
        {
            if (target.Unit.HasTrait(Traits.Dazzle))
            {
                odds *= 1 - target.WillCheckOdds(actor, target);
            }
        }
        return odds / 100;
    }

    public int[] GetSuckle(Actor_Unit target)
    {
        if (actor.Unit.Predator == false || target.Unit.Predator == false)
            return new int[] { 0, 0 };
        if (Config.KuroTenkoEnabled && Config.FeedingType != FeedingType.None)
        {
            if (target.PredatorComponent.PreyInLocation(PreyLocation.breasts, false) + target.PredatorComponent.PreyInLocation(PreyLocation.leftBreast, false) + target.PredatorComponent.PreyInLocation(PreyLocation.rightBreast, false) + target.PredatorComponent.PreyInLocation(PreyLocation.balls, false) == 0)
                return new int[] { 0, 0 };
            if (Config.SucklingPermission == SucklingPermission.AlwaysBreast || Config.FeedingType == FeedingType.BreastfeedOnly)
                return target.PredatorComponent.CalcFeedValue(actor, "breast").Item1;
            if (Config.SucklingPermission == SucklingPermission.AlwaysCock || Config.FeedingType == FeedingType.CumFeedOnly)
                return target.PredatorComponent.CalcFeedValue(actor, "cock").Item1;
            if (Config.SucklingPermission == SucklingPermission.Auto || Config.SucklingPermission == SucklingPermission.Random)
                return target.PredatorComponent.CalcFeedValue(actor, "any").Item1;

        }
        return new int[] { 0, 0, 0 };
    }

    public bool Suckle(Actor_Unit target)
    {
        if (target.Position.GetNumberOfMovesDistance(actor.Position) > 1 || target.Unit.Predator == false)
        {
            return false;
        }

        float r = (float)State.Rand.NextDouble();
        float v = GetSuckleChance(target);
        int[] suckle = { 0, 0, 0 };
        if (Config.SucklingPermission == SucklingPermission.AlwaysBreast)
            suckle = target.PredatorComponent.CalcFeedValue(actor, "breast").Item1;
        else if (Config.SucklingPermission == SucklingPermission.AlwaysCock)
            suckle = target.PredatorComponent.CalcFeedValue(actor, "cock").Item1;
        else if (Config.SucklingPermission == SucklingPermission.Random)
            if (State.Rand.Next(2) == 0)
                suckle = target.PredatorComponent.CalcFeedValue(actor, "breast").Item1;
            else
                suckle = target.PredatorComponent.CalcFeedValue(actor, "cock").Item1;
        else
            suckle = target.PredatorComponent.CalcFeedValue(actor, "any").Item1;
        if (suckle.Length < 3)
            return false;
        int halfMovement = 1 + actor.MaxMovement() / 2;
        if (actor.Movement > halfMovement)
            actor.Movement -= halfMovement;
        else
            actor.Movement = 0;
        int actorMaxHeal = actor.Unit.MaxHealth - actor.Unit.Health;
        if (r > v)
        {
            if (!target.Unit.HasBreasts && target.Unit.HasDick)
            {
                (suckle[2]) = (3);
            }
            switch (suckle[2])
            {
                case 0:
                    TacticalUtilities.Log.RegisterSuckleFail(unit, target.Unit, PreyLocation.breasts, v);
                    break;
                case 1:
                    TacticalUtilities.Log.RegisterSuckleFail(unit, target.Unit, PreyLocation.leftBreast, v);
                    break;
                case 2:
                    TacticalUtilities.Log.RegisterSuckleFail(unit, target.Unit, PreyLocation.rightBreast, v);
                    break;
                case 3:
                    TacticalUtilities.Log.RegisterSuckleFail(unit, target.Unit, PreyLocation.balls, v);
                    break;
            }
            return false;
        }
        else
        {
            actor.Unit.GiveRawExp(suckle[1]);
            actor.UnitSprite.DisplayDamage(suckle[1], false, true);
            if (!target.Unit.HasBreasts && target.Unit.HasDick)
            {
                (suckle[2]) = (3);
            }
            switch (suckle[2])
            {
                case 0:
                    TacticalUtilities.Log.RegisterSuckle(unit, target.Unit, PreyLocation.breasts, v);
                    TacticalUtilities.Log.RegisterHeal(actor.Unit, suckle, "breastfeeding", target.Unit.HasTrait(Traits.Honeymaker) ? "honey" : "none");
                    target.DigestCheck("breastfeed");
                    break;
                case 1:
                    TacticalUtilities.Log.RegisterSuckle(unit, target.Unit, PreyLocation.leftBreast, v);
                    TacticalUtilities.Log.RegisterHeal(actor.Unit, suckle, "breastfeeding", target.Unit.HasTrait(Traits.Honeymaker) ? "honey" : "none");
                    target.DigestCheck("breastfeed");
                    break;
                case 2:
                    TacticalUtilities.Log.RegisterSuckle(unit, target.Unit, PreyLocation.rightBreast, v);
                    TacticalUtilities.Log.RegisterHeal(actor.Unit, suckle, "breastfeeding", target.Unit.HasTrait(Traits.Honeymaker) ? "honey" : "none");
                    target.DigestCheck("breastfeed");
                    break;
                case 3:
                    TacticalUtilities.Log.RegisterSuckle(unit, target.Unit, PreyLocation.balls, v);
                    TacticalUtilities.Log.RegisterHeal(actor.Unit, suckle, "cumfeeding");
                    target.DigestCheck("cumfeed");
                    break;
            }
            actor.Unit.Heal(suckle[0]);
            if (target.Unit.HasTrait(Traits.Corruption))
            {
                actor.AddCorruption(unit.GetStatTotal() / 10, unit.FixedSide);
            }
            if (suckle[0] != 0)
                actor.UnitSprite.DisplayDamage(-suckle[0]);
            actor.UnitSprite.UpdateSprites(target, true);
            target.PredatorComponent.UpdateFullness();
            return true;
        }
    }

    public float GetVoreStealChance(Actor_Unit attacker, bool includeSecondaries = false)
    {
        Prey prey = GetVoreSteal(actor);
        if (prey == null)
        {
            return 0f;
        }
        if (attacker.Unit.Predator == false)
        {
            Debug.Log("This shouldn't have happened");
            return 0;
        }
        if (actor.Surrendered || actor.Unit.FixedSide == attacker.Unit.GetApparentSide(actor.Unit))
            return 1f;

        if (prey.Unit.HasTrait(Traits.Irresistable))
            return 1f;

        float recipientVoracity = Mathf.Pow(15 + attacker.Unit.GetStat(Stat.Voracity), 1.5f);
        float recipientStrength = Mathf.Pow(15 + attacker.Unit.GetStat(Stat.Strength), 1.5f);
        float donorVoracity = Mathf.Pow(15 + actor.Unit.GetStat(Stat.Voracity), 1.5f);
        float donorStrength = Mathf.Pow(15 + actor.Unit.GetStat(Stat.Strength), 1.5f);
        float donorWill = Mathf.Pow(15 + actor.Unit.GetStat(Stat.Will), 1.5f);
        float attackerScore = (recipientVoracity + recipientStrength) * (attacker.Unit.HealthPct + 1) * (30 + attacker.BodySize()) / 16 / (1 + 2 * Mathf.Pow(attacker.PredatorComponent.UsageFraction, 2)) - prey.Actor.Bulk();
        float defenderHealthPct = actor.Unit.HealthPct;
        float defenderScore = 20 + (2 * (donorStrength + 1.5f * donorVoracity + 2 * donorWill) * defenderHealthPct * defenderHealthPct * ((10 + actor.BodySize()) / 2));

        defenderScore /= actor.Unit.TraitBoosts.Incoming.VoreOddsMult;
        attackerScore *= attacker.Unit.TraitBoosts.Outgoing.VoreOddsMult;

        if (prey.Unit.HasTrait(Traits.GelatinousBody))
            attackerScore *= 1.15f;

        if (prey.Unit.HasTrait(Traits.MetalBody))
            attackerScore *= .7f;

        if (prey.Unit.HasTrait(Traits.Slippery))
            attackerScore *= 1.5f;

        if (attacker.Unit.HasTrait(Traits.AllOutFirstStrike) && attacker.HasAttackedThisCombat == false)
            attackerScore *= 3.25f;

        float odds = attackerScore / (attackerScore + defenderScore) * 100;

        odds *= actor.Unit.TraitBoosts.FlatHitReduction;
        odds *= TagConditionChecker.ApplyTagEffect(unit, attacker.Unit, UnitTagModifierEffect.VoreOddsMult);

        if (includeSecondaries)
        {
            if (actor.Unit.HasTrait(Traits.Dazzle))
            {
                odds *= 1 - actor.WillCheckOdds(attacker, actor);
            }
        }
        return odds / 100;
    }

    private Prey GetVoreSteal(Actor_Unit target)
    {
        if (actor.Unit.Predator == false || target.Unit.Predator == false)
            return null;

        if (target.Position.GetNumberOfMovesDistance(actor.Position) > 1)
        {
            return null;
        }

        if (State.RaceSettings.GetVoreTypes(actor.Unit.Race).Contains(VoreType.Oral) || State.RaceSettings.GetVoreTypes(actor.Unit.Race).Contains(VoreType.Unbirth))
        {
            foreach (Prey preyUnit in target.PredatorComponent.balls)
            {
                if (!preyUnit.Unit.IsDead)
                {
                    return preyUnit;
                }
            }

        }
        var data = Races.GetRace(target.Unit.Race);
        if (State.RaceSettings.GetVoreTypes(actor.Unit.Race).Contains(VoreType.Oral))
        {
            if (data.ExtendedBreastSprites)
            {
                foreach (Prey preyUnit in target.PredatorComponent.leftBreast)
                {
                    if (!preyUnit.Unit.IsDead)
                    {
                        return preyUnit;
                    }
                }
                foreach (Prey preyUnit in target.PredatorComponent.rightBreast)
                {
                    if (!preyUnit.Unit.IsDead)
                    {
                        return preyUnit;
                    }
                }
            }
            else
            {
                foreach (Prey preyUnit in target.PredatorComponent.breasts)
                {
                    if (!preyUnit.Unit.IsDead)
                    {
                        return preyUnit;
                    }
                }
            }

        }
        if (State.RaceSettings.GetVoreTypes(actor.Unit.Race).Contains(VoreType.Oral))
        {
            foreach (Prey preyUnit in target.PredatorComponent.stomach)
            {
                if (!preyUnit.Unit.IsDead)
                {
                    return preyUnit;
                }
            }

        }
        if (State.RaceSettings.GetVoreTypes(actor.Unit.Race).Contains(VoreType.Oral))
        {
            foreach (Prey preyUnit in target.PredatorComponent.stomach2)
            {
                if (!preyUnit.Unit.IsDead)
                {
                    return preyUnit;
                }
            }

        }
        if (State.RaceSettings.GetVoreTypes(actor.Unit.Race).Contains(VoreType.Oral))
        {
            foreach (Prey preyUnit in target.PredatorComponent.womb)
            {
                if (!preyUnit.Unit.IsDead)
                {
                    return preyUnit;
                }
            }

        }
        if (State.RaceSettings.GetVoreTypes(actor.Unit.Race).Contains(VoreType.Oral))
        {
            foreach (Prey preyUnit in target.PredatorComponent.tail)
            {
                if (!preyUnit.Unit.IsDead)
                {
                    return preyUnit;
                }
            }

        }
        return null;
    }

    private void TransferFail(Prey transfer)
    {
        FreePrey(transfer, true);
        foreach (Prey preyUnit in balls)
        {
            if (preyUnit.Unit.IsDead == false)
            {
                FreePrey(preyUnit, true);
                break;
            }
        }
        TacticalUtilities.Log.RegisterTransferFail(unit, transfer.Unit, .3f);
    }

    public float TransferBulk()
    {
        Prey transfer = GetTransfer();
        if (transfer == null)
        {
            return 999f;
        }
        return transfer.Actor.Bulk();
    }

    public float KissTransferBulk()
    {
        Prey transfer = GetKissTransfer();
        if (transfer == null)
        {
            return 999f;
        }
        return transfer.Actor.Bulk();
    }
    public float StealBulk()
    {
        Prey transfer = GetVoreSteal(actor);
        if (transfer == null)
        {
            return 999f;
        }
        return transfer.Actor.Bulk();
    }

    private Tuple<int[], List<Prey>> CalcFeedValue(Actor_Unit target, string feedType)
    {
        int healthUp = Math.Max(actor.Unit.MaxHealth / 16, 1);
        int healthUpCap = target.Unit.MaxHealth - target.Unit.Health;
        int expUp = 0;
        int location = 0;
        int[] tempVals = { 0, 0, 0 };
        var deadPrey = new List<Prey>();
        if (feedType == "breast" || feedType == "any")
        {
            var data = Races.GetRace(unit.Race);
            if (data.ExtendedBreastSprites)
            {
                int leftBreastHealth = 0;
                int rightBreastHealth = 0;
                int leftBreastEXP = 0;
                int rightBreastEXP = 0;
                foreach (Prey preyUnit in actor.PredatorComponent.leftBreast.ToList())
                    if (preyUnit.Unit.IsDead)
                    {
                        deadPrey.Add(preyUnit);
                        leftBreastHealth += heatlhBoostCalc(preyUnit, target);
                        leftBreastEXP += expBoostCalc(preyUnit);
                    }
                foreach (Prey preyUnit in actor.PredatorComponent.rightBreast.ToList())
                    if (preyUnit.Unit.IsDead)
                    {
                        deadPrey.Add(preyUnit);
                        rightBreastHealth += heatlhBoostCalc(preyUnit, target);
                        rightBreastEXP += expBoostCalc(preyUnit);
                    }
                if (target.Unit.HealthPct < 1.0f)
                {
                    if (leftBreastHealth >= rightBreastHealth)
                    {
                        healthUp += leftBreastHealth;
                        expUp += leftBreastEXP;
                        if (feedType == "any")
                            location = 1;
                    }
                    else
                    {
                        healthUp += rightBreastHealth;
                        expUp += rightBreastEXP;
                        if (feedType == "any")
                            location = 2;
                    }
                }
                else
                {
                    if (leftBreastEXP >= rightBreastEXP)
                    {
                        healthUp += leftBreastHealth;
                        expUp += leftBreastEXP;
                        if (feedType == "any")
                            location = 1;
                    }
                    else
                    {
                        healthUp += rightBreastHealth;
                        expUp += rightBreastEXP;
                        if (feedType == "any")
                            location = 2;
                    }
                }
            }
            else
                foreach (Prey preyUnit in actor.PredatorComponent.breasts.ToList())
                    if (preyUnit.Unit.IsDead)
                    {
                        deadPrey.Add(preyUnit);
                        healthUp += heatlhBoostCalc(preyUnit, target);
                        expUp += expBoostCalc(preyUnit);
                    }
        }
        if (feedType == "any")
        {
            tempVals[0] = healthUp;
            tempVals[1] = expUp;
            tempVals[2] = location;
        }
        if (feedType == "cock" || feedType == "any")
        {
            foreach (Prey preyUnit in actor.PredatorComponent.balls.ToList())
                if (preyUnit.Unit.IsDead)
                {
                    deadPrey.Add(preyUnit);
                    healthUp += heatlhBoostCalc(preyUnit, target);
                    expUp += expBoostCalc(preyUnit);
                }
            if (feedType == "any")
                location = 3;
        }
        if (feedType == "any")
        {
            if (target.Unit.HealthPct < 1.0f)
            {
                if (tempVals[0] >= healthUp)
                {
                    healthUp = tempVals[0];
                    expUp = tempVals[1];
                    location = tempVals[2];
                }
            }
            else
            {
                if (tempVals[1] >= expUp)
                {
                    healthUp = tempVals[0];
                    expUp = tempVals[1];
                    location = tempVals[2];
                }
            }
        }
        healthUp = Math.Min(healthUp, healthUpCap);
        if (!Config.OverhealEXP)
            expUp = 0;
        return new Tuple<int[], List<Prey>>(new int[] { healthUp, expUp, location }, deadPrey);
    }

    private int heatlhBoostCalc(Prey preyUnit, Actor_Unit target)
    {
        return Mathf.RoundToInt((preyUnit.Unit.MaxHealth / 10 + (((preyUnit.Unit.MaxHealth / 10) * 9) / 20) * Math.Max(0, preyUnit.Unit.Level - target.Unit.Level / (actor.Unit.HasTrait(Traits.WetNurse) ? 2 : 1)) * preyUnit.Unit.TraitBoosts.Outgoing.Nutrition));
    }

    private int expBoostCalc(Prey preyUnit)
    {
        int cap = State.RaceSettings.GetBodySize(preyUnit.Actor.Unit.Race);
        int bonus = Mathf.Clamp(preyUnit.Unit.Level, 1, cap);
        if (preyUnit.Predator.Unit.HasTrait(Traits.Honeymaker))
            bonus = Mathf.RoundToInt(bonus * (1f + (Mathf.Min((float)preyUnit.Unit.Level, (float)cap) / cap)));
        return bonus;
        // return Mathf.RoundToInt(Mathf.Clamp(preyUnit.Unit.Experience / 20, 0, (State.RaceSettings.GetBodySize(preyUnit.Actor.Unit.Race) * ((preyUnit.Predator.Unit.HasTrait(Traits.Honeymaker) ? 1 : 0) + 1))) * preyUnit.Unit.TraitBoosts.Outgoing.Nutrition);
    }

    public bool Feed(Actor_Unit target)
    {
        if (target.Unit.Side != actor.Unit.Side || target.Surrendered || target.Position.GetNumberOfMovesDistance(actor.Position) > 1 || actor.Movement == 0 || !CanFeed())
            return false;
        Tuple<int[], List<Prey>> digestion = CalcFeedValue(target, "breast");
        int[] nurseHeal = digestion.Item1;
        Prey deadPrey = digestion.Item2[0];
        TacticalUtilities.Log.RegisterFeed(unit, target.Unit, deadPrey.Unit, 1f);
        target.Unit.GiveRawExp(nurseHeal[1]);
        if (nurseHeal[0] > 0)
        {
            target.Unit.Heal(nurseHeal[0]);
            target.UnitSprite.DisplayDamage(-nurseHeal[0]);
        }
        else if (nurseHeal[1] > 0)
        {
            target.UnitSprite.DisplayDamage(nurseHeal[1], false, true);
        }
        target.UnitSprite.UpdateSprites(actor, true);
        if (nurseHeal[0] + nurseHeal[1] > 0)
            TacticalUtilities.Log.RegisterHeal(target.Unit, nurseHeal, "breastfeeding", actor.Unit.HasTrait(Traits.Honeymaker) ? "honey" : "none");
        actor.DigestCheck("breastfeed");
        if (unit.HasTrait(Traits.Corruption))
        {
            target.AddCorruption(unit.GetStatTotal() / 10, unit.FixedSide);
        }
        int halfMovement = 1 + actor.MaxMovement() / (2 + (actor.Unit.HasTrait(Traits.WetNurse) ? 1 : 0));
        if (actor.Movement > halfMovement)
            actor.Movement -= halfMovement;
        else
            actor.Movement = 0;
        UpdateFullness();
        return true;
    }

    public bool FeedCum(Actor_Unit target)
    {
        if (target.Unit.Side != actor.Unit.Side || target.Surrendered || target.Position.GetNumberOfMovesDistance(actor.Position) > 1 || actor.Movement == 0 || !CanFeedCum())
            return false;
        Tuple<int[], List<Prey>> digestion = CalcFeedValue(target, "cock");
        int[] nurseHeal = digestion.Item1;
        Prey deadPrey = digestion.Item2[0];
        TacticalUtilities.Log.RegisterCumFeed(unit, target.Unit, deadPrey.Unit, 1f);
        target.Unit.GiveRawExp(nurseHeal[1]);
        if (nurseHeal[0] > 0)
        {
            target.Unit.Heal(nurseHeal[0]);
            target.UnitSprite.DisplayDamage(-nurseHeal[0]);
        }
        else if (nurseHeal[1] > 0)
        {
            target.UnitSprite.DisplayDamage(nurseHeal[1], false, true);
        }
        target.UnitSprite.UpdateSprites(actor, true);
        if (nurseHeal[0] + nurseHeal[1] > 0)
            TacticalUtilities.Log.RegisterHeal(target.Unit, nurseHeal, "cumfeeding");
        actor.DigestCheck("cumfeed");
        if (unit.HasTrait(Traits.Corruption))
        {
            target.AddCorruption(unit.GetStatTotal()/10, unit.FixedSide);
        }
        int quarterMovement = 1 + actor.MaxMovement() / (4 + (actor.Unit.HasTrait(Traits.WetNurse) ? 1 : 0));
        if (actor.Movement > quarterMovement)
            actor.Movement -= quarterMovement;
        else
            actor.Movement = 0;
        UpdateFullness();
        return true;
    }

    private bool Transfer(Actor_Unit target, Prey preyUnit)
    {
        float r = (float)State.Rand.NextDouble();
        float v = target.GetSpecialChance(SpecialAction.CockVore);
        if (target.Position.GetNumberOfMovesDistance(actor.Position) > 1)
        {
            return false;
        }
        if (r > v && !preyUnit.Unit.IsDead)
        {
            return false;
        }
        Prey preyref = new Prey(preyUnit.Actor, target, preyUnit.Actor.PredatorComponent?.prey);
        if (target.Unit.HasVagina && RaceParameters.GetTraitData(target.Unit).AllowedVoreTypes.Contains(VoreType.Unbirth))
        {
            var box = State.GameManager.CreateDialogBox();
            box.SetData(() => TransferFinalize(target, preyUnit, preyref, PreyLocation.stomach), "Stomach", "Womb", "Transfer the prey to the womb or stomach?", () => TransferFinalize(target, preyUnit, preyref, PreyLocation.womb));
        }
        // Hopefully there's no circumstance where a vaginal trasfer is possible but an oral isn't
        //else if (target.Unit.HasBreasts && !target.Unit.HasDick)
        //{
        //    TransferFinalize(target, preyUnit, preyref, PreyLocation.womb);
        //}
        else
        {
            TransferFinalize(target, preyUnit, preyref, PreyLocation.stomach);
        }
        if (target.UnitSprite != null)
        {
            target.UnitSprite.UpdateSprites(actor, true);
            target.UnitSprite.AnimateBellyEnter();
        }
        return true;
    }

    private bool KissTransfer(Actor_Unit target, Prey preyUnit)
    {
        Prey preyref = new Prey(preyUnit.Actor, target, preyUnit.Actor.PredatorComponent?.prey);
        if (target.Position.GetNumberOfMovesDistance(actor.Position) > 1)
        {
            return false;
        }
        else
        {
            KissTransferFinalize(target, preyUnit, preyref, PreyLocation.stomach);
        }
        if (target.UnitSprite != null)
        {
            target.UnitSprite.UpdateSprites(actor, true);
            target.UnitSprite.AnimateBellyEnter();
        }
        return true;
    }

    private bool TransferFinalize(Actor_Unit recipient, Prey preyUnit, Prey preyref, PreyLocation destination)
    {
        // Empowerment handling
        if (destination == PreyLocation.womb && recipient.Unit.HasVagina && Config.CumGestation)
        {
            bool alreadyPregnant = false;
            Actor_Unit alreadyChild = null;
            foreach (Prey existingPrey in recipient.PredatorComponent.womb)
            {
                if ((existingPrey.Unit.IsDead && existingPrey.Unit.Side != recipient.Unit.Side) || (!existingPrey.Unit.IsDead && existingPrey.Unit.Side == recipient.Unit.Side))
                {
                    alreadyPregnant = true;
                    alreadyChild = existingPrey.Actor;
                }
            }
            if (alreadyPregnant)
            {
                recipient.PredatorComponent.birthStatBoost += preyUnit.Unit.Level;
                recipient.PredatorComponent.RemovePrey(preyUnit);
                actor.PredatorComponent.RemovePrey(preyUnit); int pool = 6;
                if (Config.CondomsForCV)
                {pool = 11;}
                var friendlies = TacticalUtilities.Units.Where(s => s.Unit.Side == unit.Side && s.Unit != recipient.Unit && s.Unit != unit && s.Visible && s.Targetable && s.Unit.IsDead == false).ToArray();//Random unit picker, using modified unimplemented code from the LogUtilities
                if (friendlies.Length == 0)
                {friendlies = null;}
                switch (State.Rand.Next(pool))//Great additional lines thanks to Tatltuae!
                {
                    case 0:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> pumps what remains of <b>{preyUnit.Unit.Name}</b> into <b>{LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)}</b> womb, providing nutrients to strengthen <b>{alreadyChild.Unit.Name}</b>.");
                        break;
                    case 1:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"The {LogUtilities.GetRandomStringFrom("cum", "jizz", "spooge", "goo", "thick fluid")} that was once <b>{preyUnit.Unit.Name}</b> travels up <b>{LogUtilities.ApostrophizeWithOrWithoutS(actor.Unit.Name)}</b> {LogUtilities.GetRandomStringFrom("shaft", "rod", "cock", "member", "dick")}, the former {LogUtilities.GetRaceDescSingl(preyUnit.Unit)} gushing up into {LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)} {PreyLocStrings.ToSyn(PreyLocation.womb)} where {LogUtilities.GPPHe(preyUnit.Unit)} {LogUtilities.IsAre(preyUnit.Unit)} promptly absorbed into <b>{alreadyChild.Unit.Name}</b>.");
                        break;
                    case 2:
                        if (!Config.LewdDialog)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> pumps what remains of <b>{preyUnit.Unit.Name}</b> into <b>{LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)}</b> womb, providing nutrients to strengthen <b>{alreadyChild.Unit.Name}</b>.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"As <b>{actor.Unit.Name}</b> and <b>{recipient.Unit.Name}</b> {LogUtilities.GetRandomStringFrom("have sex", "fuck")}, the liquid remains of <b>{preyUnit.Unit.Name}</b> shoot into <b>{LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}, where what little remains of <b>{preyUnit.Unit.Name}</b> is simply absorbed into <b>{alreadyChild.Unit.Name}</b>.");
                        break;
                    case 3:
                        if (!Config.LewdDialog)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"The {LogUtilities.GetRandomStringFrom("cum", "jizz", "spooge", "goo", "thick fluid")} that was once <b>{preyUnit.Unit.Name}</b> travels up <b>{LogUtilities.ApostrophizeWithOrWithoutS(actor.Unit.Name)}</b> {LogUtilities.GetRandomStringFrom("shaft", "rod", "cock", "member", "dick")}, the former {LogUtilities.GetRaceDescSingl(preyUnit.Unit)} gushing up into {LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)} {PreyLocStrings.ToSyn(PreyLocation.womb)} where {LogUtilities.GPPHe(preyUnit.Unit)} {LogUtilities.IsAre(preyUnit.Unit)} promptly absorbed into <b>{alreadyChild.Unit.Name}</b>.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{recipient.Unit.Name}</b> rides <b>{LogUtilities.ApostrophizeWithOrWithoutS(actor.Unit.Name)}</b> {LogUtilities.GetRandomStringFrom("shaft", "rod", "cock", "member", "dick")}, milking out a liqufied {LogUtilities.GetRaceDescSingl(preyUnit.Unit)}, who flows into the already occupied {PreyLocStrings.ToSyn(PreyLocation.womb)}, where {LogUtilities.GPPHis(preyUnit.Unit)} remains fuse with <b>{alreadyChild.Unit.Name}</b>.");
                        break;
                    case 4:
                        if (!Config.LewdDialog)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> pumps <b>{preyUnit.Unit.Name}</b> through {LogUtilities.GPPHis(actor.Unit)} {LogUtilities.GetRandomStringFrom("shaft", "rod", "cock", "member", "dick")} and into <b>{LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}, where {LogUtilities.GPPHis(preyUnit.Unit)} remains are used by <b>{alreadyChild.Unit.Name}</b> to fuel <b>{LogUtilities.GPPHis(alreadyChild.Unit)}</b> growth.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{recipient.Unit.Name}</b> pushes <b>{actor.Unit.Name}</b> down to the ground and fucks {LogUtilities.GPPHim(actor.Unit)} until the liquid remains of <b>{preyUnit.Unit.Name}</b> flow up into {LogUtilities.GPPHis(recipient.Unit)} {PreyLocStrings.ToSyn(PreyLocation.womb)}, where <b>{preyUnit.Unit.Name}</b> is absorbed into <b>{alreadyChild.Unit.Name}</b>.");
                        break;
                    case 5:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> pumps <b>{preyUnit.Unit.Name}</b> through {LogUtilities.GPPHis(actor.Unit)} {LogUtilities.GetRandomStringFrom("shaft", "rod", "cock", "member", "dick")} and into <b>{LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}, where {LogUtilities.GPPHis(preyUnit.Unit)} remains are used by <b>{alreadyChild.Unit.Name}</b> to fuel <b>{LogUtilities.GPPHis(alreadyChild.Unit)}</b> growth.");
                        break;
                    case 6:
                        if (!Config.LewdDialog)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> pumps <b>{preyUnit.Unit.Name}</b> through {LogUtilities.GPPHis(actor.Unit)} {LogUtilities.GetRandomStringFrom("shaft", "rod", "cock", "member", "dick")} and into <b>{LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}, where {LogUtilities.GPPHis(preyUnit.Unit)} remains are used by <b>{alreadyChild.Unit.Name}</b> to fuel <b>{LogUtilities.GPPHis(alreadyChild.Unit)}</b> growth.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"As <b>{actor.Unit.Name}</b> and <b>{recipient.Unit.Name}</b> have sex, an untimely squirm from <b>{alreadyChild.Unit.Name}</b> coinsides with <b>{actor.Unit.Name}</b> cumming hard, allowing the former {LogUtilities.GetRaceDescSingl(preyUnit.Unit)} <b>{preyUnit.Unit.Name}</b> to rip through the condom, flowing into <b>{LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)} before being absorbed into the regrowing <b>{alreadyChild.Unit.Name}</b>.");
                        break;
                    case 7:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"As <b>{actor.Unit.Name}</b> goes to have sex with <b>{recipient.Unit.Name}</b>, {LogUtilities.GPPHe(actor.Unit)} pull out a condom, before being told to put it away. Without that barrier of protection, the {LogUtilities.GetRandomStringFrom("cum", "jizz", "spooge", "goo", "thick fluid")} that was once <b>{preyUnit.Unit.Name}</b> spills into <b>{LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)} unimpeded, where it is easily absorbed into <b>{alreadyChild.Unit.Name}</b>.");
                        break;
                    case 8:
                        if (friendlies == null)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"As <b>{actor.Unit.Name}</b> and <b>{recipient.Unit.Name}</b> {LogUtilities.GetRandomStringFrom("have sex", "fuck")}, the liquid remains of <b>{preyUnit.Unit.Name}</b> shoot into <b>{LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}, where what little remains of <b>{preyUnit.Unit.Name}</b> is simply absorbed into <b>{alreadyChild.Unit.Name}</b>.");
                        else
                        {var i = friendlies[State.Rand.Next(friendlies.Length)].Unit;
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> slips on a condom and goes to have sex with <b>{recipient.Unit.Name}</b>. After they're done, <b>{actor.Unit.Name}</b> observes that former <b>{preyUnit.Unit.Name}</b> is leaking from 7 small holes in the condom. Knowing what happened, <b>{actor.Unit.Name}</b> shouts \"<b>{(i).Name}</b>! Not funny!\" Within <b>{LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}, the trickling streams of \"<b>{preyUnit.Unit.Name}</b>\" are absorbed by <b>{alreadyChild.Unit.Name}</b>.");}
                        break;
                    case 9:
                        if (friendlies == null)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"As <b>{actor.Unit.Name}</b> and <b>{recipient.Unit.Name}</b> {LogUtilities.GetRandomStringFrom("have sex", "fuck")}, the liquid remains of <b>{preyUnit.Unit.Name}</b> shoot into <b>{LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}, where what little remains of <b>{preyUnit.Unit.Name}</b> is simply absorbed into <b>{alreadyChild.Unit.Name}</b>.");
                        else
                        {var i = friendlies[State.Rand.Next(friendlies.Length)].Unit;
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"Before having sex, <b>{actor.Unit.Name}</b> looks for {LogUtilities.GPPHis(actor.Unit)} condom, but can't find it. \"I think I left it with <b>{(i).Name}</b>. Want me to go get it?\" <b>{recipient.Unit.Name}</b> responds \"Nah, I got a {LogUtilities.GetRaceDescSingl(alreadyChild.Unit)} in my {PreyLocStrings.ToSyn(PreyLocation.womb)}, they'll absorb everything.\" True to {LogUtilities.GPPHis(recipient.Unit)} word, the remains of <b>{preyUnit.Unit.Name}</b> are absorbed by <b>{alreadyChild.Unit.Name}</b>.");}
                        break;
                    case 10:
                        if (!Config.LewdDialog)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> pumps <b>{preyUnit.Unit.Name}</b> through {LogUtilities.GPPHis(actor.Unit)} {LogUtilities.GetRandomStringFrom("shaft", "rod", "cock", "member", "dick")} and into <b>{LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}, where {LogUtilities.GPPHis(preyUnit.Unit)} remains are used by <b>{alreadyChild.Unit.Name}</b> to fuel <b>{LogUtilities.GPPHis(alreadyChild.Unit)}</b> growth.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"As <b>{LogUtilities.ApostrophizeWithOrWithoutS(actor.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.balls)} shrinks, <b>{LogUtilities.ApostrophizeWithOrWithoutS(preyUnit.Unit.Name)}</b> liquified form fills the rubber barrier between <b>{LogUtilities.ApostrophizeWithOrWithoutS(actor.Unit.Name)}</b> {LogUtilities.GetRandomStringFrom("shaft", "rod", "cock", "member", "dick")} and <b>{LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}. As <b>{actor.Unit.Name}</b> pulls {LogUtilities.GPPHis(actor.Unit)} {LogUtilities.GetRandomStringFrom("shaft", "rod", "cock", "member", "dick")} back out, the condom is left behind, where it is pulled into <b>{LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}, and it is there that it pops, with <b>{preyUnit.Unit.Name}</b> becoming a shower of nutrients for <b>{alreadyChild.Unit.Name}</b>.");
                        break;
                    default:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{actor.Unit.Name}</b> pumps what remains of <b>{preyUnit.Unit.Name}</b> into <b>{LogUtilities.ApostrophizeWithOrWithoutS(recipient.Unit.Name)}</b> womb, providing nutrients to strengthen <b>{alreadyChild.Unit.Name}</b>.");
                        break;
                    }
                if (unit.HasTrait(Traits.Corruption) || preyUnit.Unit.HasTrait(Traits.Corruption))
                {
                    alreadyChild.Unit.hiddenFixedSide = true;
                    alreadyChild.sidesAttackedThisBattle = new List<int>();
                    alreadyChild.Unit.RemoveTrait(Traits.Corruption);
                    alreadyChild.Unit.AddPermanentTrait(Traits.Corruption);
                }
                if (!alreadyChild.Unit.HasTrait(Traits.Corruption))
                    alreadyChild.Unit.FixedSide = unit.FixedSide;
                UpdateFullness();
                unit.UpdateSpells();
                return true;
            }
        }
        if (preyUnit.Unit.IsDead == false)
        {
            recipient.PredatorComponent.AlivePrey++;
            actor.PredatorComponent.AlivePrey--;
        } else if (unit.HasTrait(Traits.Corruption))
        {
            recipient.AddCorruption(preyUnit.Unit.GetStatTotal()/2, unit.FixedSide);
        }
        else if (preyUnit.Unit.HasTrait(Traits.Corruption))
            recipient.AddCorruption(preyUnit.Unit.GetStatTotal() / 2, preyUnit.Unit.FixedSide);
        switch (destination)
        {
            case PreyLocation.womb:
                recipient.PredatorComponent?.AddToWomb(preyref, 1.0f);
                TacticalUtilities.Log.RegisterTransferSuccess(unit, recipient.Unit, preyUnit.Unit, 1.0f, PreyLocation.womb);
                break;
            case PreyLocation.stomach:
                recipient.PredatorComponent.AddToStomach(preyref, 1.0f);
                TacticalUtilities.Log.RegisterTransferSuccess(unit, recipient.Unit, preyUnit.Unit, 1.0f, PreyLocation.stomach);
                break;
            default:
                recipient.PredatorComponent.AddToStomach(preyref, 1.0f);
                TacticalUtilities.Log.RegisterTransferSuccess(unit, recipient.Unit, preyUnit.Unit, 1.0f, PreyLocation.stomach);
                break;
        }
        recipient.PredatorComponent.AddPrey(preyref);
        recipient.Unit.GiveScaledExp(4 * preyUnit.Unit.ExpMultiplier, recipient.Unit.Level - preyUnit.Unit.Level, true);
        if (preyUnit.Unit.IsDead && Config.NoScatForDeadTransfers)
            preyUnit.ScatDisabled = true;
        recipient.PredatorComponent.UpdateFullness();
        actor.PredatorComponent.RemovePrey(preyUnit);
        UpdateFullness();
        return true;
    }
	
    private bool KissTransferFinalize(Actor_Unit recipient, Prey preyUnit, Prey preyref, PreyLocation destination)
    {
        if (preyUnit.Unit.IsDead == false)
        {
            recipient.PredatorComponent.AlivePrey++;
            actor.PredatorComponent.AlivePrey--;
        } else if (unit.HasTrait(Traits.Corruption))
        {
            recipient.AddCorruption(preyUnit.Unit.GetStatTotal()/2, unit.FixedSide);
        }
        else if (preyUnit.Unit.HasTrait(Traits.Corruption))
            recipient.AddCorruption(preyUnit.Unit.GetStatTotal() / 2, preyUnit.Unit.FixedSide);
        switch (destination)
        {
            default:
                recipient.PredatorComponent.AddToStomach(preyref, 1.0f);
                TacticalUtilities.Log.RegisterKissTransfer(unit, recipient.Unit, preyUnit.Unit, 1.0f, PreyLocation.stomach);
                break;
        }
        recipient.PredatorComponent.AddPrey(preyref);
        recipient.Unit.GiveScaledExp(4 * preyUnit.Unit.ExpMultiplier, recipient.Unit.Level - preyUnit.Unit.Level, true);
        recipient.PredatorComponent.UpdateFullness();
        actor.PredatorComponent.RemovePrey(preyUnit);
        UpdateFullness();
        return true;
    }

    public bool TransferAttempt(Actor_Unit target)
    {
        if (actor.Unit.Predator == false || target.Unit.Predator == false)
            return false;
        if (target.Unit.Side != actor.Unit.Side || target.Surrendered)
        {
            return false;
        }
        if (target.Position.GetNumberOfMovesDistance(actor.Position) > 1)
        {
            return false;
        }
        if (actor.Movement == 0)
        {
            return false;
        }
        if (!CanTransfer())
        {
            return false;
        }
        Prey transfer = GetTransfer();
        if (target.PredatorComponent.FreeCap() < transfer.Actor.Bulk() && !(target.Unit == actor.Unit))
        {
            return false;
        }
        if (!Transfer(target, transfer))
            TransferFail(transfer);
        int thirdMovement = (int)Math.Ceiling(actor.MaxMovement() / 3.0f);
        if (actor.Movement > thirdMovement)
            actor.Movement -= thirdMovement;
        else
            actor.Movement = 0;
        return true;
    }

    public bool KissTransferAttempt(Actor_Unit target)
    {
        if (actor.Unit.Predator == false || target.Unit.Predator == false)
            return false;
        if (target.Unit.Side != actor.Unit.Side || target.Surrendered)
        {
            return false;
        }
        if (target.Position.GetNumberOfMovesDistance(actor.Position) > 1)
        {
            return false;
        }
        if (target == actor)
        {
            return false;
        }
        if (actor.Movement == 0)
        {
            return false;
        }
        if (!CanKissTransfer())
        {
            return false;
        }
        Prey transfer = GetKissTransfer();
        if (target.PredatorComponent.FreeCap() < transfer.Actor.Bulk() && !(target.Unit == actor.Unit))
        {
            return false;
        }
        if (!KissTransfer(target, transfer))
        {};
        int thirdMovement = (int)Math.Ceiling(actor.MaxMovement() / 3.0f);
        if (actor.Movement > thirdMovement)
            actor.Movement -= thirdMovement;
        else
            actor.Movement = 0;
        return true;
    }

    public bool VoreStealAttempt(Actor_Unit target)
    {
        if (target.Position.GetNumberOfMovesDistance(actor.Position) > 1)
        {
            return false;
        }
        if (target == actor)
        {
            return false;
        }
        if (actor.Movement == 0)
        {
            return false;
        }
        if (!CanVoreSteal(target))
        {
            return false;
        }
        Prey transfer = GetVoreSteal(target);
        if (transfer == null)
            return false;
        if (FreeCap() < transfer.Actor.Bulk() && target.Unit != actor.Unit)
        {
            return false;
        }
        if (!VoreSteal(target, transfer, transfer.Location))
            VoreStealFail(target, transfer, transfer.Location);
        int thirdMovement = (int)Math.Ceiling(actor.MaxMovement() / 3.0f);
        if (actor.Movement > thirdMovement)
            actor.Movement -= thirdMovement;
        else
            actor.Movement = 0;
        return true;
    }

    private bool VoreSteal(Actor_Unit target, Prey preyUnit, PreyLocation oldLocation)
    {
        float r = (float)State.Rand.NextDouble();
        float v = target.PredatorComponent.GetVoreStealChance(actor);
        if (r >= v)
        {
            return false;
        }
        Prey preyref = new Prey(preyUnit.Actor, actor, preyUnit.Actor.PredatorComponent?.prey);
        if (actor.Unit.HasVagina && RaceParameters.GetTraitData(actor.Unit).AllowedVoreTypes.Contains(VoreType.Unbirth) && oldLocation == PreyLocation.balls)
        {
            var box = State.GameManager.CreateDialogBox();
            box.SetData(() => VoreStealFinalize(target, preyUnit, preyref, PreyLocation.stomach, oldLocation), "Stomach", "Womb", "Transfer the prey to the womb or stomach?", () => VoreStealFinalize(target, preyUnit, preyref, PreyLocation.womb, oldLocation));
        }
        else
        {
            VoreStealFinalize(target, preyUnit, preyref, PreyLocation.stomach, oldLocation);
        }
        if (actor.UnitSprite != null)
        {
            actor.UnitSprite.UpdateSprites(actor, true);
            actor.UnitSprite.AnimateBellyEnter();
        }
        return true;
    }

    private void VoreStealFail(Actor_Unit target, Prey preyUnit, PreyLocation oldLocation)
    {
        TacticalUtilities.Log.RegisterVoreStealFail(target.Unit, actor.Unit, preyUnit.Unit, oldLocation);
    }

    private bool VoreStealFinalize(Actor_Unit donor, Prey preyUnit, Prey preyref, PreyLocation destination, PreyLocation oldLocation)
    {

        actor.PredatorComponent.AlivePrey++;
        donor.PredatorComponent.AlivePrey--;
        switch (destination)
        {
            case PreyLocation.womb:
                actor.PredatorComponent?.AddToWomb(preyref, 1.0f);
                TacticalUtilities.Log.RegisterVoreStealSuccess(donor.Unit, actor.Unit, preyUnit.Unit, 1.0f, PreyLocation.womb, oldLocation);
                break;
            case PreyLocation.stomach:
                actor.PredatorComponent.AddToStomach(preyref, 1.0f);
                TacticalUtilities.Log.RegisterVoreStealSuccess(donor.Unit, actor.Unit, preyUnit.Unit, 1.0f, PreyLocation.stomach, oldLocation);
                break;
            default:
                actor.PredatorComponent.AddToStomach(preyref, 1.0f);
                TacticalUtilities.Log.RegisterVoreStealSuccess(donor.Unit, actor.Unit, preyUnit.Unit, 1.0f, PreyLocation.stomach, oldLocation);
                break;
        }
        actor.PredatorComponent.AddPrey(preyref);
        actor.Unit.GiveScaledExp(4 * preyUnit.Unit.ExpMultiplier, actor.Unit.Level - preyUnit.Unit.Level, true);
        UpdateFullness();
        donor.PredatorComponent.RemovePrey(preyUnit);
        donor.PredatorComponent.UpdateFullness();
        return true;
    }

    private void AddToWomb(Prey preyref, float v)
    {
        womb.Add(preyref);
        if (actor.UnitSprite != null)
        {
            actor.UnitSprite.UpdateSprites(actor, true);
            actor.UnitSprite.AnimateBellyEnter();
        }
    }

    private void AddToBalls(Prey preyref, float v)
    {
        balls.Add(preyref);
        if (actor.UnitSprite != null)
        {
            actor.UnitSprite.UpdateSprites(actor, true);
            actor.UnitSprite.AnimateBalls(1f);
        }
    }

    internal void ForceConsume(Actor_Unit forcePrey, PreyLocation preyLocation)
    {
        if (forcePrey.Unit.IsDead == false)
            AlivePrey++;
        State.GameManager.TacticalMode.TacticalStats.RegisterVore(unit.Side);

        if (forcePrey.Unit.Side == unit.Side)
            State.GameManager.TacticalMode.TacticalStats.RegisterAllyVore(unit.Side);
        forcePrey.Visible = false;
        forcePrey.Targetable = false;
        State.GameManager.TacticalMode.DirtyPack = true;
        if (!(forcePrey.Unit.FixedSide == unit.GetApparentSide(forcePrey.Unit)) || !(unit.HasTrait(Traits.FriendlyStomach) || unit.HasTrait(Traits.Endosoma)))
        {
            unit.GiveScaledExp(4 * forcePrey.Unit.ExpMultiplier, unit.Level - forcePrey.Unit.Level, true);
        }
        forcePrey.Movement = 0;
        Prey preyref = new Prey(forcePrey, actor, forcePrey.PredatorComponent?.prey);
        switch (preyLocation)//Credits to Tatltuae for the additional lines. Coder's note: I would add Config.LewdDialog checks to these but seeing as the originals weren't exactly "platonic" I decided not to
        {
            case PreyLocation.womb:
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.womb, actor);
                AddToWomb(preyref, 1f);
                switch (State.Rand.Next(6))
                {
                    case 0:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> pries apart <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> vulva using {LogUtilities.GPPHis(forcePrey.Unit)} face, grabbing onto any bodypart {LogUtilities.GPPHe(forcePrey.Unit)} can find to slip {LogUtilities.GPPHimself(forcePrey.Unit)} all the way in, aided by the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} contractions of sudden arousal.");
                        break;
                    case 1:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> runs over to <b>{unit.Name}</b> before getting on the ground under the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(PreyLocation.womb)}. Before <b>{unit.Name}</b> can question this, <b>{forcePrey.Unit.Name}</b> bolts upright, forcing {LogUtilities.GPPHimself(forcePrey.Unit)} up into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}{LogUtilities.GetRandomStringFrom("!", ".")}");
                        break;
                    case 2:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. Running over, <b>{forcePrey.Unit.Name}</b> rapidly pushes {LogUtilities.GPPHis(forcePrey.Unit)} way into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}.");
                        break;
                    case 3:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"After being knocked to the ground by the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)}, <b>{unit.Name}</b> finds {LogUtilities.GPPHis(unit)} {PreyLocStrings.ToSyn(PreyLocation.womb)} being aggressivly licked by <b>{forcePrey.Unit.Name}</b>. This carries on for a few moments, before <b>{forcePrey.Unit.Name}</b> suddenly and rapidly shoves {LogUtilities.GPPHimself(forcePrey.Unit)} into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} womb.");
                        break;
                    case 4:
                        if (LogUtilities.ActorHumanoid(forcePrey.Unit))
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> slips behind <b>{unit.Name}</b> and kicks {LogUtilities.GPPHim(unit)} right in the {PreyLocStrings.ToSyn(PreyLocation.womb)}! Where most would assume this is an attack on the {LogUtilities.GetRaceDescSingl(unit)}, <b>{LogUtilities.ApostrophizeWithOrWithoutS(forcePrey.Unit.Name)}</b> next action, knocking <b>{unit.Name}</b> over and forcing {LogUtilities.GPPHimself(forcePrey.Unit)} further into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)} proves the action had other motavations.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> pries apart <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> vulva using {LogUtilities.GPPHis(forcePrey.Unit)} face, grabbing onto any bodypart {LogUtilities.GPPHe(forcePrey.Unit)} can find to slip {LogUtilities.GPPHimself(forcePrey.Unit)} all the way in, aided by the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} contractions of sudden arousal.");
                        break;
                    case 5:
                        if (LogUtilities.ActorHumanoid(forcePrey.Unit))
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sticks a finger up <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}. Then a hand. Then a whole arm. Then two hands. Then <b>{unit.Name}</b> pushes off the ground to force {LogUtilities.GPPHimself(forcePrey.Unit)} all the way up into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. Running over, <b>{forcePrey.Unit.Name}</b> rapidly pushes {LogUtilities.GPPHis(forcePrey.Unit)} way into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}.");
                        break;
                    default:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> pries apart <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> vulva using {LogUtilities.GPPHis(forcePrey.Unit)} face, grabbing onto any bodypart {LogUtilities.GPPHe(forcePrey.Unit)} can find to slip {LogUtilities.GPPHimself(forcePrey.Unit)} all the way in, aided by the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} contractions of sudden arousal.");
                        break;
                }
                break;
            case PreyLocation.balls:
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.balls, actor);
                AddToBalls(preyref, 1f);
                switch (State.Rand.Next(6))
                {
                    case 0:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sucks <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tip. As soon as {LogUtilities.GPPHe(forcePrey.Unit)} start{LogUtilities.SIfSingular(forcePrey.Unit)} sticking {LogUtilities.GPPHis(forcePrey.Unit)} tongue inside, however, it's more like the {LogUtilities.ApostrophizeWithOrWithoutS(InfoPanel.RaceSingular(unit))} throbbing member is doing the sucking, allowing <b>{forcePrey.Unit.Name}</b> to wiggle all the way into {LogUtilities.GPPHis(unit)} sack.");
                        break;
                    case 1:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"One moment, <b>{forcePrey.Unit.Name}</b> was just walking up to <b>{unit.Name}</b>, then, not even two seconds later, <b>{forcePrey.Unit.Name}</b> had already shoved half of {LogUtilities.GPPHis(forcePrey.Unit)} body down the shocked {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToCockSyn()}! Only three or so seconds after that, <b>{forcePrey.Unit.Name}</b> was fully in <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.balls)}.");
                        break;
                    case 2:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> strokes <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToCockSyn()}, getting it nice and {LogUtilities.GetRandomStringFrom("hard", "erect")}, before shoving {LogUtilities.GPPHimself(forcePrey.Unit)} down into the unprepared {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(PreyLocation.balls)}{LogUtilities.GetRandomStringFrom("!", ".")}");
                        break;
                    case 3:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> has spotted <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToCockSyn()}. In that instant, <b>{forcePrey.Unit.Name}</b> knows exactly what {LogUtilities.GPPHe(forcePrey.Unit)} must do. Without any warning, <b>{forcePrey.Unit.Name}</b> is shoving {LogUtilities.GPPHimself(forcePrey.Unit)} down <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToCockSyn()}, the lenth of the shaft bulging with the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(forcePrey.Unit))} every movement.");
                        break;
                    case 4:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> gives <b>{unit.Name}</b> a blowjob. As <b>{unit.Name}</b> nears ejaculation, <b>{forcePrey.Unit.Name}</b> pulls back and then dives head first down <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToCockSyn()}.");
                        break;
                    case 5:
                        if (LogUtilities.ActorHumanoid(forcePrey.Unit))
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sticks {LogUtilities.GPPHis(forcePrey.Unit)} hand down <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.balls)}. Then the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)} grabs some of the loose skin around the {PreyLocStrings.ToSyn(PreyLocation.balls)} from the inside, and uses it as a handhold to rapidly, and not particularaly pleasently, pull {LogUtilities.GPPHimself(forcePrey.Unit)} into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.balls)}.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sucks <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tip. As soon as {LogUtilities.GPPHe(forcePrey.Unit)} start{LogUtilities.SIfSingular(forcePrey.Unit)} sticking {LogUtilities.GPPHis(forcePrey.Unit)} tongue inside, however, it's more like the {LogUtilities.ApostrophizeWithOrWithoutS(InfoPanel.RaceSingular(unit))} throbbing member is doing the sucking, allowing <b>{forcePrey.Unit.Name}</b> to wiggle all the way into {LogUtilities.GPPHis(unit)} sack.");
                        break;
                    default:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sucks <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tip. As soon as {LogUtilities.GPPHe(forcePrey.Unit)} start{LogUtilities.SIfSingular(forcePrey.Unit)} sticking {LogUtilities.GPPHis(forcePrey.Unit)} tongue inside, however, it's more like the {LogUtilities.ApostrophizeWithOrWithoutS(InfoPanel.RaceSingular(unit))} throbbing member is doing the sucking, allowing <b>{forcePrey.Unit.Name}</b> to wiggle all the way into {LogUtilities.GPPHis(unit)} sack.");
                        break;
                }
                break;
            case PreyLocation.anal:
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.anal, actor);
                AddToStomach(preyref, 1f);
                switch (State.Rand.Next(6))
                {
                    case 0:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> starts by shoving one {(LogUtilities.ActorHumanoid(forcePrey.Unit) ? "arm" : "forelimb")} up <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> ass, then another. Inch by inch {LogUtilities.GPPHe(forcePrey.Unit)} vigorously squeez{LogUtilities.EsIfSingular(forcePrey.Unit)} {LogUtilities.GPPHimself(forcePrey.Unit)} into the anal depths.");
                        break;
                    case 1:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> jumps face-first into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.anal)}, yanking {LogUtilities.GPPHimself(forcePrey.Unit)} up and inside in a matter of moments.");
                        break;
                    case 2:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"When <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> back is turned, <b>{forcePrey.Unit.Name}</b> takes {LogUtilities.GPPHis(forcePrey.Unit)} chance, jamming {LogUtilities.GPPHis(forcePrey.Unit)} whole body up the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(PreyLocation.anal)}.");
                        break;
                    case 3:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. Without so much as an \"excuse me,\" the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)} unapologetically forces {LogUtilities.GPPHimself(forcePrey.Unit)} up the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(PreyLocation.anal)}.");
                        break;
                    case 4:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"Without giving the {LogUtilities.GetRaceDescSingl(unit)} any time to argue or protest, <b>{forcePrey.Unit.Name}</b> crawls up <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.anal)}.");
                        break;
                    case 5:
                        if (LogUtilities.ActorHumanoid(forcePrey.Unit))
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> slips behind <b>{unit.Name}</b> and punches {LogUtilities.GPPHim(unit)} right in the {PreyLocStrings.ToSyn(PreyLocation.anal)}! Where most would assume this is an attack on the {LogUtilities.GetRaceDescSingl(unit)}, <b>{LogUtilities.ApostrophizeWithOrWithoutS(forcePrey.Unit.Name)}</b> next action, forcing {LogUtilities.GPPHimself(forcePrey.Unit)} all the way inside <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.anal)} proves the action had other motavations.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> starts by shoving one {(LogUtilities.ActorHumanoid(forcePrey.Unit) ? "arm" : "forelimb")} up <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> ass, then another. Inch by inch {LogUtilities.GPPHe(forcePrey.Unit)} vigorously squeez{LogUtilities.EsIfSingular(forcePrey.Unit)} {LogUtilities.GPPHimself(forcePrey.Unit)} into the anal depths.");
                        break;
                    default:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> starts by shoving one {(LogUtilities.ActorHumanoid(forcePrey.Unit) ? "arm" : "forelimb")} up <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> ass, then another. Inch by inch {LogUtilities.GPPHe(forcePrey.Unit)} vigorously squeez{LogUtilities.EsIfSingular(forcePrey.Unit)} {LogUtilities.GPPHimself(forcePrey.Unit)} into the anal depths.");
                        break;
                }
                break;
            case PreyLocation.breasts:
                var data = Races.GetRace(unit.Race);
                if (data.ExtendedBreastSprites)
                {
                    State.GameManager.SoundManager.PlaySwallow(PreyLocation.breasts, actor);
                    if (Config.FairyBVType == FairyBVType.Picked && State.GameManager.TacticalMode.IsPlayerInControl)
                    {
                        var box = State.GameManager.CreateDialogBox();
                        box.SetData(() => { rightBreast.Add(preyref); UpdateFullness();
                            switch (State.Rand.Next(5))
                            {
                            case 0:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> grabs <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> right {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )} and shoves it into {LogUtilities.GPPHis(forcePrey.Unit)} face. Then, with a wet *shlorp* sound, <b>{LogUtilities.ApostrophizeWithOrWithoutS(forcePrey.Unit.Name)}</b> head disappears into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");
                                break;
                            case 1:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> latches onto <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> right {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, suckling for a moment or two, before forcing {LogUtilities.GPPHimself(forcePrey.Unit)} through the nipple, and vanishing into the boob beyond.");
                                break;
                            case 2:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. With a motion resembling a headbutt, <b>{forcePrey.Unit.Name}</b> slips {LogUtilities.GPPHis(forcePrey.Unit)} head into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> right {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");
                                break;
                            case 3:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sucks on <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> right {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}. As <b>{unit.Name}</b> relaxes to let it happen, <b>{forcePrey.Unit.Name}</b> pulls back and then dives head first into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> right {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}.");
                                break;
                            case 4:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> knocks <b>{unit.Name}</b> down onto {LogUtilities.GPPHis(unit)} back, before plunging into the center of <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> right {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}. Then {LogUtilities.GPPHe(forcePrey.Unit)} slowly feed{LogUtilities.SIfSingular(forcePrey.Unit)} the rest of {LogUtilities.GPPHimself(forcePrey.Unit)} into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}.");
                                break;
                            default:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. With a motion resembling a headbutt, <b>{forcePrey.Unit.Name}</b> slips {LogUtilities.GPPHis(forcePrey.Unit)} head into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> right {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");
                                break;
                            }}, "Right", "Left", "Which breast should the prey be put in? (from your pov)", () => { leftBreast.Add(preyref); UpdateFullness(); 
                            switch (State.Rand.Next(5))
                            {
                            case 0:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> grabs <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> left {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )} and shoves it into {LogUtilities.GPPHis(forcePrey.Unit)} face. Then, with a wet *shlorp* sound, <b>{LogUtilities.ApostrophizeWithOrWithoutS(forcePrey.Unit.Name)}</b> head disappears into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");
                                break;
                            case 1:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> latches onto <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> left {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, suckling for a moment or two, before forcing {LogUtilities.GPPHimself(forcePrey.Unit)} through the nipple, and vanishing into the boob beyond.");
                                break;
                            case 2:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. With a motion resembling a headbutt, <b>{forcePrey.Unit.Name}</b> slips {LogUtilities.GPPHis(forcePrey.Unit)} head into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> left {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");
                                break;
                            case 3:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sucks on <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> left {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}. As <b>{unit.Name}</b> relaxes to let it happen, <b>{forcePrey.Unit.Name}</b> pulls back and then dives head first into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> left {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}.");
                                break;
                            case 4:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> knocks <b>{unit.Name}</b> down onto {LogUtilities.GPPHis(unit)} back, before plunging into the center of <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> left {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}. Then {LogUtilities.GPPHe(forcePrey.Unit)} slowly feed{LogUtilities.SIfSingular(forcePrey.Unit)} the rest of {LogUtilities.GPPHimself(forcePrey.Unit)} into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}.");
                                break;
                            default:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. With a motion resembling a headbutt, <b>{forcePrey.Unit.Name}</b> slips {LogUtilities.GPPHis(forcePrey.Unit)} head into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> left {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");
                                break;
                            }});
                        break;
                    }
                    int breastSide = 1;
                    if (LeftBreastFullness < RightBreastFullness || State.Rand.Next(2) == 0)
                        leftBreast.Add(preyref);
                    else
                    {rightBreast.Add(preyref); breastSide = 2;}
                    switch (State.Rand.Next(5))
                    {
                        case 0:
                            {int bS = breastSide;
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> grabs <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {(bS == 1 ? "left" : "right")} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )} and shoves it into {LogUtilities.GPPHis(forcePrey.Unit)} face. Then, with a wet *shlorp* sound, <b>{LogUtilities.ApostrophizeWithOrWithoutS(forcePrey.Unit.Name)}</b> head disappears into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");}
                            break;
                        case 1:
                            {int bS = breastSide;
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> latches onto <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {(bS == 1 ? "left" : "right")} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, suckling for a moment or two, before forcing {LogUtilities.GPPHimself(forcePrey.Unit)} through the nipple, and vanishing into the boob beyond.");}
                            break;
                        case 2:
                            {int bS = breastSide;
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. With a motion resembling a headbutt, <b>{forcePrey.Unit.Name}</b> slips {LogUtilities.GPPHis(forcePrey.Unit)} head into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {(bS == 1 ? "left" : "right")} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");}
                            break;
                        case 3:
                            {int bS = breastSide;
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sucks on <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {(bS == 1 ? "left" : "right")} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}. As <b>{unit.Name}</b> relaxes to let it happen, <b>{forcePrey.Unit.Name}</b> pulls back and then dives head first into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {(bS == 1 ? "left" : "right")} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}.");}
                            break;
                        case 4:
                            {int bS = breastSide;
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> knocks <b>{unit.Name}</b> down onto {LogUtilities.GPPHis(unit)} back, before plunging into the center of <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {(bS == 1 ? "left" : "right")} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}. Then {LogUtilities.GPPHe(forcePrey.Unit)} slowly feed{LogUtilities.SIfSingular(forcePrey.Unit)} the rest of {LogUtilities.GPPHimself(forcePrey.Unit)} into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}.");}
                            break;
                        default:
                            {int bS = breastSide;
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. With a motion resembling a headbutt, <b>{forcePrey.Unit.Name}</b> slips {LogUtilities.GPPHis(forcePrey.Unit)} head into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {(bS == 1 ? "left" : "right")} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");}
                            break;
                    }
                    break;
                }                
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.breasts, actor);
                breasts.Add(preyref);
                switch (State.Rand.Next(6))
                {
                    case 0:
                        if (unit.Race == Race.Kangaroos)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> approaches <b>{unit.Name}</b> and abruptly yanks open {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")}. Before the {LogUtilities.GetRaceDescSingl(unit)} can comment on how frankly rude this is, <b>{forcePrey.Unit.Name}</b> has already forced {LogUtilities.GPPHis(forcePrey.Unit)} way inside.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"In just a few deft movements, <b>{forcePrey.Unit.Name}</b> crams {LogUtilities.GPPHimself(forcePrey.Unit)} into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tits.");
                        break;
                    case 1:
                        if (unit.Race == Race.Kangaroos)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"While <b>{unit.Name}</b> isn't paying attention, <b>{forcePrey.Unit.Name}</b> jumps into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} pouch. Somehow, <b>{unit.Name}</b> doesn't notice this, and carries on as {LogUtilities.GPPHe(unit)} {LogUtilities.WasWere(unit)}.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> charges into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.breasts)}, breifly motorboating {LogUtilities.GPPHim(unit)} before pushing extra hard and vanishing into the space between the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(PreyLocation.breasts)}.");
                        break;
                    case 2:
                        if (unit.Race == Race.Kangaroos)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> has already decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} {LogUtilities.GetRaceDescSingl(unit)} food. The only questions left are who and how? For the who, <b>{forcePrey.Unit.Name}</b> selects <b>{unit.Name}</b>. After some thought, the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)} decides to see what the inside of a {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")} is like. Before <b>{unit.Name}</b> can attempt to stop {LogUtilities.GPPHim(forcePrey.Unit)}, the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)} is already fully within {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")}.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"Using <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.breasts)} as handholds, <b>{forcePrey.Unit.Name}</b> pulls {LogUtilities.GPPHimself(forcePrey.Unit)} into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} cleavage.");
                        break;
                    case 3:
                        if (unit.Race == Race.Kangaroos)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"After forcing {LogUtilities.GPPHimself(forcePrey.Unit)} into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")}, <b>{forcePrey.Unit.Name}</b> is a touch surprised by the way the {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")}'s entrance seals above {LogUtilities.GPPHim(forcePrey.Unit)}. Unlike most force-feeders, <b>{forcePrey.Unit.Name}</b> doesn't necissarely want to be digested. So now, both <b>{unit.Name}</b> and <b>{forcePrey.Unit.Name}</b> get to be unhappy!");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> approaches <b>{unit.Name}</b> and abruptly pulls apart {LogUtilities.GPPHis(unit)} {PreyLocStrings.ToSyn(PreyLocation.breasts)}, before stuffing {LogUtilities.GPPHimself(forcePrey.Unit)} into the gap between.");
                        break;
                    case 4:
                        if (unit.Race == Race.Kangaroos)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> smacks <b>{unit.Name}</b> across the face. While {LogUtilities.GPPHeIs(unit)} dazed, <b>{forcePrey.Unit.Name}</b> climbs into {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")}. When <b>{unit.Name}</b> regains {LogUtilities.GPPHis(unit)} senses, {LogUtilities.GPPHe(unit)} say{LogUtilities.SIfSingular(unit)} \"You could have just asked.\"");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> grabs <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.breasts)} and shoves {LogUtilities.GPPHimself(forcePrey.Unit)} between {LogUtilities.GPPHis(unit)} {PreyLocStrings.ToSyn(PreyLocation.breasts)}! Much to <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> surprise, {LogUtilities.GPPHe(forcePrey.Unit)} sink{LogUtilities.SIfSingular(forcePrey.Unit)} into the soft flesh at the bottom, becoming living fat on <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.breasts)}.");
                        break;
                    case 5:
                        if (unit.Race == Race.Kangaroos)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> smacks <b>{unit.Name}</b> across the face. While {LogUtilities.GPPHeIs(unit)} dazed, <b>{forcePrey.Unit.Name}</b> climbs into {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")}.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> knocks <b>{unit.Name}</b> down onto {LogUtilities.GPPHis(unit)} back, before {LogUtilities.GetRandomStringFrom("jumping feet first", "diving head first")} into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.breasts)}, dissapearing with one quick *shlump* noise.");
                        break;
                    default:
                        if (unit.Race == Race.Kangaroos)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> approaches <b>{unit.Name}</b> and abruptly yanks open {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")}. Before the {LogUtilities.GetRaceDescSingl(unit)} can comment on how frankly rude this is, <b>{forcePrey.Unit.Name}</b> has already forced {LogUtilities.GPPHis(forcePrey.Unit)} way inside.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"In just a few deft movements, <b>{forcePrey.Unit.Name}</b> crams {LogUtilities.GPPHimself(forcePrey.Unit)} into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tits.");
                        break;
                }
                break;
            case PreyLocation.tail:
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.tail, actor);
                tail.Add(preyref);
                switch (State.Rand.Next(4))
                {
                    case 0:
                        if (unit.Race == Race.Youko)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> leaps into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> bundle of tails. Quickly {LogUtilities.GPPHe(forcePrey.Unit)} vanish{LogUtilities.EsIfSingular(forcePrey.Unit)} into the ball of fluff; feeling quite comfy.");
                        else if (unit.Race == Race.Terrorbird)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> leaps into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> gullet appearing to be stopped on {LogUtilities.GPPHis(forcePrey.Unit)} way to the bird's stomach, finding {LogUtilities.GPPHimself(forcePrey.Unit)} rather stuck in the bird's crop instead.");
                        else if (unit.Race == Race.Bees)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> manages to knock <b>{unit.Name}</b> out of the sky. Before the bee has a chance to get airborne again, <b>{forcePrey.Unit.Name}</b> has already forced {LogUtilities.GPPHis(forcePrey.Unit)} way into {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("stinger", "honey chamber", "honey-filled tail")}.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> forces {LogUtilities.GPPHimself(forcePrey.Unit)} up <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tail. Inch by inch {LogUtilities.GPPHe(forcePrey.Unit)} vigorously squeez{LogUtilities.EsIfSingular(forcePrey.Unit)} {LogUtilities.GPPHimself(forcePrey.Unit)} into the tail's depths.");
                        break;
                    case 1:
                        if (unit.Race == Race.Youko)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> charges <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> rear, grabbing the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} tails and quickly wrapping {LogUtilities.GPPHimself(forcePrey.Unit)} up in them.");
                        else if (unit.Race == Race.Terrorbird)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> walks up to <b>{unit.Name}</b>, pries {LogUtilities.GPPHis(unit)} beak open, and climbs in, stopping in the confused {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} crop.");
                        else if (unit.Race == Race.Bees)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> gets under <b>{unit.Name}</b>, and pries open {LogUtilities.GPPHis(unit)} stinger, before crawling up into the honey filled chamber within{LogUtilities.GetRandomStringFrom(".", $", much to <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> displeasure.")}");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> throws {LogUtilities.GPPHimself(forcePrey.Unit)} at <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tail, pulling open the tip and pushing {LogUtilities.GPPHimself(forcePrey.Unit)} inside before the {LogUtilities.GetRaceDescSingl(unit)} can stop {LogUtilities.GPPHim(forcePrey.Unit)}.");
                        break;
                    case 2:
                        if (unit.Race == Race.Youko)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"As <b>{unit.Name}</b> stands there, minding {LogUtilities.GPPHis(unit)} own business, <b>{forcePrey.Unit.Name}</b> comes up behind {LogUtilities.GPPHim(unit)} and forcibly wraps the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} tails around {LogUtilities.GPPHimself(forcePrey.Unit)}{LogUtilities.GetRandomStringFrom(".", $", and when <b>{unit.Name}</b> tries to unfurl {LogUtilities.GPPHis(unit)} tails, <b>{forcePrey.Unit.Name}</b> simply holds on tighter.")}");
                        else if (unit.Race == Race.Terrorbird)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> climbs up onto <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> back, as though to ride the {LogUtilities.GetRaceDescSingl(unit)}, but instead, {LogUtilities.GPPHe(forcePrey.Unit)} use{LogUtilities.SIfSingular(forcePrey.Unit)} {LogUtilities.GPPHis(forcePrey.Unit)} position to force open <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> beak, before quickly flipping {LogUtilities.GPPHimself(forcePrey.Unit)} over and into it, sliding down before {LogUtilities.GetRandomStringFrom($"suddenly finding {LogUtilities.GPPHimself(forcePrey.Unit)} stuck in the bird's crop, rather than the stomach", $"splaying out to stop {LogUtilities.GPPHis(forcePrey.Unit)} decent in the crop")}.");
                        else if (unit.Race == Race.Bees)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> baits <b>{unit.Name}</b> into trying to sting {LogUtilities.GPPHim(forcePrey.Unit)}, and then, slightly painfully for the {LogUtilities.GetRaceDescSingl(unit)}, pulls the stinger open and goes inside.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> wrestles <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tail, before prying open the tip and slipping inside.");
                        break;
                    case 3:
                        if (unit.Race == Race.Youko)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> slowly sneaks into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> bundle of tails. Before <b>{unit.Name}</b> can react {LogUtilities.GPPHis(unit)} tails have already wrapped up the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)} in their {LogUtilities.GetRandomStringFrom("comfy", "tight", "fluffly")} embrace, refusing to let go.");
                        else if (unit.Race == Race.Terrorbird)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> watches <b>{unit.Name}</b> closely, and then, seeing an opportunity, jumps for the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} open beak, sliding in and coming to a stop in {LogUtilities.GPPHis(unit)} crop.");
                        else if (unit.Race == Race.Bees)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sneaks up behind <b>{unit.Name}</b>, before shoving the {LogUtilities.GetRaceDescSingl(unit)} hard, knocking {LogUtilities.GPPHim(unit)} off balance. By the time {LogUtilities.GPPHe(unit)} {LogUtilities.HasHave(unit)} regained that balance, <b>{forcePrey.Unit.Name}</b> is mostly inside {LogUtilities.GPPHis(unit)} stinger.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{unit.Name}</b> stops as {LogUtilities.GPPHe(unit)} feel something at the tip of {LogUtilities.GPPHis(unit)} tail. Looking behind {LogUtilities.GPPHimself(unit)}, {LogUtilities.GPPHe(unit)} spot{LogUtilities.SIfSingular(unit)} <b>{forcePrey.Unit.Name}</b> messing with {LogUtilities.GPPHis(unit)} tail before- <i>shlorp</i> -forcing {LogUtilities.GPPHis(forcePrey.Unit)} way inside.");
                        break;
                    default:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> forces {LogUtilities.GPPHimself(forcePrey.Unit)} up <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tail. Inch by inch {LogUtilities.GPPHe(forcePrey.Unit)} vigorously squeez{LogUtilities.EsIfSingular(forcePrey.Unit)} {LogUtilities.GPPHimself(forcePrey.Unit)} into the tail's depths.");
                        break;
                }
                break;
            default:
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.stomach, actor);
                AddToStomach(preyref, 1f);
                switch (State.Rand.Next(12))
                {
                    case 0:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"At {LogUtilities.GPPHis(forcePrey.Unit)} first glimpse of the {(LogUtilities.ActorHumanoid(unit) ? "warrior's" : "beast's")} maw, <b>{forcePrey.Unit.Name}</b> dives right down {LogUtilities.GPPHis(unit)} gullet. One swallow reflex later, <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> belly has been filled.");
                        break;
                    case 1:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> walks up to <b>{unit.Name}</b> and pries {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("jaws", "maw", "mouth", "muzzle", "gob")} open before manually crawling down into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(PreyLocation.stomach)}, much to <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> confusion.");
                        break;
                    case 2:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"Noticing that {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("jaws", "maw", "mouth", "muzzle", "gob")} is slightly ajar, <b>{forcePrey.Unit.Name}</b> makes a running leap into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {LogUtilities.GetRandomStringFrom("jaws", "maw", "mouth", "muzzle", "gob")}, sliding quickly down into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(PreyLocation.stomach)}{LogUtilities.GetRandomStringFrom("!", ".")}");
                        break;
                    case 3:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. Without so much as an \"excuse me,\" the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)} unapologetically forces {LogUtilities.GPPHimself(forcePrey.Unit)} down the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {LogUtilities.GetRandomStringFrom("jaws", "maw", "mouth", "muzzle", "gob")}.");
                        break;
                    case 4:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> abruptly sticks {LogUtilities.GPPHis(forcePrey.Unit)} head in <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> mouth, looking around the inside for a few moments, before forcing {LogUtilities.GPPHis(forcePrey.Unit)} way down into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(forcePrey.Unit))} {PreyLocStrings.ToSyn(PreyLocation.stomach)}.");
                        break;
                    case 5:
                        if (LogUtilities.ActorHumanoid(forcePrey.Unit))
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"As <b>{forcePrey.Unit.Name}</b> walks over to <b>{unit.Name}</b>, {LogUtilities.GPPHe(forcePrey.Unit)} pick{LogUtilities.SIfSingular(forcePrey.Unit)} up a stick. Once at the {LogUtilities.GetRaceDescSingl(unit)}, {LogUtilities.GPPHe(forcePrey.Unit)} use{LogUtilities.SIfSingular(forcePrey.Unit)} the stick to force open <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {LogUtilities.GetRandomStringFrom("jaws", "maw", "mouth", "muzzle", "gob")}. By the time that {LogUtilities.GPPHe(unit)} can spit the stick out, the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)} is already in {LogUtilities.GPPHis(unit)} {PreyLocStrings.ToSyn(PreyLocation.stomach)}.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"At {LogUtilities.GPPHis(forcePrey.Unit)} first glimpse of the {(LogUtilities.ActorHumanoid(unit) ? "warrior's" : "beast's")} maw, <b>{forcePrey.Unit.Name}</b> dives right down {LogUtilities.GPPHis(unit)} gullet. One swallow reflex later, <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> belly has been filled.");
                        break;
                        //Below logs are generalized for potential future force feed compatability
                    case 6:
                        if (LogUtilities.ActorHumanoid(unit))
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"After <b>{forcePrey.Unit.Name}</b> finished forcing {LogUtilities.GPPHimself(forcePrey.Unit)} into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(preyLocation)}, <b>{unit.Name}</b> says simply; \"Rude. If you wanted in, you could've let me choose were to put you, or at least let me get some pleasure out of your journey in.\"");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"It all happened so fast. One moment, <b>{unit.Name}</b> was bracing {LogUtilities.GPPHimself(unit)} against a charging {LogUtilities.GetRaceDescSingl(forcePrey.Unit)}. The next, {LogUtilities.GPPHis(unit)} {PreyLocStrings.ToSyn(preyLocation)} was bulging out with <b>{forcePrey.Unit.Name}</b> stored inside.");
                        break;
                    case 7:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"It all happened so fast. One moment, <b>{unit.Name}</b> was bracing {LogUtilities.GPPHimself(unit)} against a charging {LogUtilities.GetRaceDescSingl(forcePrey.Unit)}. The next, {LogUtilities.GPPHis(unit)} {PreyLocStrings.ToSyn(preyLocation)} was bulging out with <b>{forcePrey.Unit.Name}</b> stored inside.");
                        break;
                    case 8:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> crams {LogUtilities.GPPHimself(forcePrey.Unit)} into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(preyLocation)}{LogUtilities.GetRandomStringFrom("!", ".")}");
                        break;
                    case 9:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"One moment, <b>{forcePrey.Unit.Name}</b> was outside of <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> body. The next, {LogUtilities.GPPHe(forcePrey.Unit)} {LogUtilities.WasWere(forcePrey.Unit)} inside. Notably, <b>{unit.Name}</b> was given no choice in this decision.");
                        break;
                    case 10:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> walks up to <b>{unit.Name}</b> and unceremoniously forces {LogUtilities.GPPHimself(forcePrey.Unit)} into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(preyLocation)}. <b>{unit.Name}</b>, for {LogUtilities.GPPHis(unit)} part stares at {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("bloated", "engorged", "enlarged", "bulging" )} {PreyLocStrings.ToSyn(preyLocation)} with utter confusion.");
                        break;
                    case 11:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> tackles <b>{unit.Name}</b>, and uses the moment of confusion to force {LogUtilities.GPPHimself(forcePrey.Unit)} into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(preyLocation)}.");
                        break;
                    default:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"At {LogUtilities.GPPHis(forcePrey.Unit)} first glimpse of the {(LogUtilities.ActorHumanoid(unit) ? "warrior's" : "beast's")} maw, <b>{forcePrey.Unit.Name}</b> dives right down {LogUtilities.GPPHis(unit)} gullet. One swallow reflex later, <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> belly has been filled.");
                        break;
                }
                break;
        }
        AddPrey(preyref);
        actor.SetPredMode(preyLocation);
        if (preyLocation == PreyLocation.stomach && actor.Unit.Race == Race.Ryan)
            actor.SetVoreSuccessMode();
        if (actor.Unit.Race != Race.Ryan)
            actor.SetVoreSuccessMode();
        UpdateFullness();
    }

    internal void ForceConsumeAuto(Actor_Unit forcePrey)
    {
        if (forcePrey.Unit.IsDead == false)
            AlivePrey++;
        State.GameManager.TacticalMode.TacticalStats.RegisterVore(unit.Side);

        if (forcePrey.Unit.Side == unit.Side)
            State.GameManager.TacticalMode.TacticalStats.RegisterAllyVore(unit.Side);
        forcePrey.Visible = false;
        forcePrey.Targetable = false;
        State.GameManager.TacticalMode.DirtyPack = true;
        if (!(forcePrey.Unit.FixedSide == unit.GetApparentSide(forcePrey.Unit)) || !(unit.HasTrait(Traits.FriendlyStomach) || unit.HasTrait(Traits.Endosoma)))
        {
            unit.GiveScaledExp(4 * forcePrey.Unit.ExpMultiplier, unit.Level - forcePrey.Unit.Level, true);
        }
        forcePrey.Movement = 0;
        Prey preyref = new Prey(forcePrey, actor, forcePrey.PredatorComponent?.prey);

        List<VoreType> allowedVoreTypes = State.RaceSettings.GetVoreTypes(unit.Race);

        if (State.GameManager.TacticalMode.turboMode) //When turboing, just pick the fast solution.
        { AddToStomach(preyref, 1f); return; }

        WeightedList<VoreType> options = new WeightedList<VoreType>();

        List<VoreType> voreTypes = new List<VoreType>();
        if (allowedVoreTypes.Contains(VoreType.Oral) && Config.OralWeight > 0)
            options.Add(VoreType.Oral, Config.OralWeight);
        if (allowedVoreTypes.Contains(VoreType.Unbirth) && CanUnbirth(forcePrey) && Config.UnbirthWeight > 0 && (actor.BodySize() >= forcePrey.BodySize() * 3 || !actor.Unit.HasTrait(Traits.TightNethers)))
            options.Add(VoreType.Unbirth, Config.UnbirthWeight);
        if (allowedVoreTypes.Contains(VoreType.CockVore) && CanCockVore(forcePrey) && Config.CockWeight > 0 && (actor.BodySize() >= forcePrey.BodySize() * 3 || !actor.Unit.HasTrait(Traits.TightNethers)))
            options.Add(VoreType.CockVore, Config.CockWeight);
        if (allowedVoreTypes.Contains(VoreType.BreastVore) && CanBreastVore(forcePrey) && Config.BreastWeight > 0)
            options.Add(VoreType.BreastVore, Config.BreastWeight);
        if (allowedVoreTypes.Contains(VoreType.TailVore) && CanTailVore(forcePrey) && Config.TailWeight > 0)
            options.Add(VoreType.TailVore, Config.TailWeight);
        if (allowedVoreTypes.Contains(VoreType.Anal) && CanAnalVore(forcePrey) && Config.AnalWeight > 0)
            options.Add(VoreType.Anal, Config.AnalWeight);

        var type = options.GetResult();
        PreyLocation loc = PreyLocation.stomach;
        switch (type)//Credits to Tatltuae for the additional lines. Coder's note: I would add Config.LewdDialog checks to these but seeing as the originals weren't exactly "platonic" I decided not to
        {
            case VoreType.Unbirth:
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.womb, actor);
                loc = PreyLocation.womb;
                AddToWomb(preyref, 1f);
                switch (State.Rand.Next(6))
                {
                    case 0:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> pries apart <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> vulva using {LogUtilities.GPPHis(forcePrey.Unit)} face, grabbing onto any bodypart {LogUtilities.GPPHe(forcePrey.Unit)} can find to slip {LogUtilities.GPPHimself(forcePrey.Unit)} all the way in, aided by the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} contractions of sudden arousal.");
                        break;
                    case 1:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> runs over to <b>{unit.Name}</b> before getting on the ground under the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(PreyLocation.womb)}. Before <b>{unit.Name}</b> can question this, <b>{forcePrey.Unit.Name}</b> bolts upright, forcing {LogUtilities.GPPHimself(forcePrey.Unit)} up into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}{LogUtilities.GetRandomStringFrom("!", ".")}");
                        break;
                    case 2:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. Running over, <b>{forcePrey.Unit.Name}</b> rapidly pushes {LogUtilities.GPPHis(forcePrey.Unit)} way into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}.");
                        break;
                    case 3:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"After being knocked to the ground by the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)}, <b>{unit.Name}</b> finds {LogUtilities.GPPHis(unit)} {PreyLocStrings.ToSyn(PreyLocation.womb)} being aggressivly licked by <b>{forcePrey.Unit.Name}</b>. This carries on for a few moments, before <b>{forcePrey.Unit.Name}</b> suddenly and rapidly shoves {LogUtilities.GPPHimself(forcePrey.Unit)} into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} womb.");
                        break;
                    case 4:
                        if (LogUtilities.ActorHumanoid(forcePrey.Unit))
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> slips behind <b>{unit.Name}</b> and kicks {LogUtilities.GPPHim(unit)} right in the {PreyLocStrings.ToSyn(PreyLocation.womb)}! Where most would assume this is an attack on the {LogUtilities.GetRaceDescSingl(unit)}, <b>{LogUtilities.ApostrophizeWithOrWithoutS(forcePrey.Unit.Name)}</b> next action, knocking <b>{unit.Name}</b> over and forcing {LogUtilities.GPPHimself(forcePrey.Unit)} further into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)} proves the action had other motavations.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> pries apart <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> vulva using {LogUtilities.GPPHis(forcePrey.Unit)} face, grabbing onto any bodypart {LogUtilities.GPPHe(forcePrey.Unit)} can find to slip {LogUtilities.GPPHimself(forcePrey.Unit)} all the way in, aided by the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} contractions of sudden arousal.");
                        break;
                    case 5:
                        if (LogUtilities.ActorHumanoid(forcePrey.Unit))
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sticks a finger up <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}. Then a hand. Then a whole arm. Then two hands. Then <b>{unit.Name}</b> pushes off the ground to force {LogUtilities.GPPHimself(forcePrey.Unit)} all the way up into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. Running over, <b>{forcePrey.Unit.Name}</b> rapidly pushes {LogUtilities.GPPHis(forcePrey.Unit)} way into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}.");
                        break;
                    default:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> pries apart <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> vulva using {LogUtilities.GPPHis(forcePrey.Unit)} face, grabbing onto any bodypart {LogUtilities.GPPHe(forcePrey.Unit)} can find to slip {LogUtilities.GPPHimself(forcePrey.Unit)} all the way in, aided by the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} contractions of sudden arousal.");
                        break;
                }
                break;
            case VoreType.CockVore:
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.balls, actor);
                loc = PreyLocation.balls;
                AddToBalls(preyref, 1f);
                switch (State.Rand.Next(6))
                {
                    case 0:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sucks <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tip. As soon as {LogUtilities.GPPHe(forcePrey.Unit)} start{LogUtilities.SIfSingular(forcePrey.Unit)} sticking {LogUtilities.GPPHis(forcePrey.Unit)} tongue inside, however, it's more like the {LogUtilities.ApostrophizeWithOrWithoutS(InfoPanel.RaceSingular(unit))} throbbing member is doing the sucking, allowing <b>{forcePrey.Unit.Name}</b> to wiggle all the way into {LogUtilities.GPPHis(unit)} sack.");
                        break;
                    case 1:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"One moment, <b>{forcePrey.Unit.Name}</b> was just walking up to <b>{unit.Name}</b>, then, not even two seconds later, <b>{forcePrey.Unit.Name}</b> had already shoved half of {LogUtilities.GPPHis(forcePrey.Unit)} body down the shocked {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToCockSyn()}! Only three or so seconds after that, <b>{forcePrey.Unit.Name}</b> was fully in <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.balls)}.");
                        break;
                    case 2:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> strokes <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToCockSyn()}, getting it nice and {LogUtilities.GetRandomStringFrom("hard", "erect")}, before shoving {LogUtilities.GPPHimself(forcePrey.Unit)} down into the unprepared {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(PreyLocation.balls)}{LogUtilities.GetRandomStringFrom("!", ".")}");
                        break;
                    case 3:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> has spotted <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToCockSyn()}. In that instant, <b>{forcePrey.Unit.Name}</b> knows exactly what {LogUtilities.GPPHe(forcePrey.Unit)} must do. Without any warning, <b>{forcePrey.Unit.Name}</b> is shoving {LogUtilities.GPPHimself(forcePrey.Unit)} down <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToCockSyn()}, the lenth of the shaft bulging with the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(forcePrey.Unit))} every movement.");
                        break;
                    case 4:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> gives <b>{unit.Name}</b> a blowjob. As <b>{unit.Name}</b> nears ejaculation, <b>{forcePrey.Unit.Name}</b> pulls back and then dives head first down <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToCockSyn()}.");
                        break;
                    case 5:
                        if (LogUtilities.ActorHumanoid(forcePrey.Unit))
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sticks {LogUtilities.GPPHis(forcePrey.Unit)} hand down <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.balls)}. Then the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)} grabs some of the loose skin around the {PreyLocStrings.ToSyn(PreyLocation.balls)} from the inside, and uses it as a handhold to rapidly, and not particularaly pleasently, pull {LogUtilities.GPPHimself(forcePrey.Unit)} into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.balls)}.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sucks <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tip. As soon as {LogUtilities.GPPHe(forcePrey.Unit)} start{LogUtilities.SIfSingular(forcePrey.Unit)} sticking {LogUtilities.GPPHis(forcePrey.Unit)} tongue inside, however, it's more like the {LogUtilities.ApostrophizeWithOrWithoutS(InfoPanel.RaceSingular(unit))} throbbing member is doing the sucking, allowing <b>{forcePrey.Unit.Name}</b> to wiggle all the way into {LogUtilities.GPPHis(unit)} sack.");
                        break;
                    default:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sucks <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tip. As soon as {LogUtilities.GPPHe(forcePrey.Unit)} start{LogUtilities.SIfSingular(forcePrey.Unit)} sticking {LogUtilities.GPPHis(forcePrey.Unit)} tongue inside, however, it's more like the {LogUtilities.ApostrophizeWithOrWithoutS(InfoPanel.RaceSingular(unit))} throbbing member is doing the sucking, allowing <b>{forcePrey.Unit.Name}</b> to wiggle all the way into {LogUtilities.GPPHis(unit)} sack.");
                        break;
                }
                break;
            case VoreType.Anal:
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.anal, actor);
                loc = PreyLocation.anal;
                AddToStomach(preyref, 1f);
                switch (State.Rand.Next(6))
                {
                    case 0:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> starts by shoving one {(LogUtilities.ActorHumanoid(forcePrey.Unit) ? "arm" : "forelimb")} up <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> ass, then another. Inch by inch {LogUtilities.GPPHe(forcePrey.Unit)} vigorously squeez{LogUtilities.EsIfSingular(forcePrey.Unit)} {LogUtilities.GPPHimself(forcePrey.Unit)} into the anal depths.");
                        break;
                    case 1:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> jumps face-first into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.anal)}, yanking {LogUtilities.GPPHimself(forcePrey.Unit)} up and inside in a matter of moments.");
                        break;
                    case 2:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"When <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> back is turned, <b>{forcePrey.Unit.Name}</b> takes {LogUtilities.GPPHis(forcePrey.Unit)} chance, jamming {LogUtilities.GPPHis(forcePrey.Unit)} whole body up the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(PreyLocation.anal)}.");
                        break;
                    case 3:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. Without so much as an \"excuse me,\" the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)} unapologetically forces {LogUtilities.GPPHimself(forcePrey.Unit)} up the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(PreyLocation.anal)}.");
                        break;
                    case 4:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"Without giving the {LogUtilities.GetRaceDescSingl(unit)} any time to argue or protest, <b>{forcePrey.Unit.Name}</b> crawls up <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.anal)}.");
                        break;
                    case 5:
                        if (LogUtilities.ActorHumanoid(forcePrey.Unit))
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> slips behind <b>{unit.Name}</b> and punches {LogUtilities.GPPHim(unit)} right in the {PreyLocStrings.ToSyn(PreyLocation.anal)}! Where most would assume this is an attack on the {LogUtilities.GetRaceDescSingl(unit)}, <b>{LogUtilities.ApostrophizeWithOrWithoutS(forcePrey.Unit.Name)}</b> next action, forcing {LogUtilities.GPPHimself(forcePrey.Unit)} all the way inside <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.anal)} proves the action had other motavations.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> starts by shoving one {(LogUtilities.ActorHumanoid(forcePrey.Unit) ? "arm" : "forelimb")} up <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> ass, then another. Inch by inch {LogUtilities.GPPHe(forcePrey.Unit)} vigorously squeez{LogUtilities.EsIfSingular(forcePrey.Unit)} {LogUtilities.GPPHimself(forcePrey.Unit)} into the anal depths.");
                        break;
                    default:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> starts by shoving one {(LogUtilities.ActorHumanoid(forcePrey.Unit) ? "arm" : "forelimb")} up <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> ass, then another. Inch by inch {LogUtilities.GPPHe(forcePrey.Unit)} vigorously squeez{LogUtilities.EsIfSingular(forcePrey.Unit)} {LogUtilities.GPPHimself(forcePrey.Unit)} into the anal depths.");
                        break;
                }
                break;
            case VoreType.BreastVore:
                var data = Races.GetRace(unit.Race);
                if (data.ExtendedBreastSprites)
                {
                    State.GameManager.SoundManager.PlaySwallow(PreyLocation.breasts, actor);
                    loc = PreyLocation.breasts;
                    if (Config.FairyBVType == FairyBVType.Picked && State.GameManager.TacticalMode.IsPlayerInControl)
                    {
                        var box = State.GameManager.CreateDialogBox();
                        box.SetData(() => { rightBreast.Add(preyref); UpdateFullness();
                            switch (State.Rand.Next(5))
                            {
                            case 0:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> grabs <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> right {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )} and shoves it into {LogUtilities.GPPHis(forcePrey.Unit)} face. Then, with a wet *shlorp* sound, <b>{LogUtilities.ApostrophizeWithOrWithoutS(forcePrey.Unit.Name)}</b> head disappears into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");
                                break;
                            case 1:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> latches onto <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> right {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, suckling for a moment or two, before forcing {LogUtilities.GPPHimself(forcePrey.Unit)} through the nipple, and vanishing into the boob beyond.");
                                break;
                            case 2:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. With a motion resembling a headbutt, <b>{forcePrey.Unit.Name}</b> slips {LogUtilities.GPPHis(forcePrey.Unit)} head into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> right {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");
                                break;
                            case 3:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sucks on <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> right {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}. As <b>{unit.Name}</b> relaxes to let it happen, <b>{forcePrey.Unit.Name}</b> pulls back and then dives head first into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> right {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}.");
                                break;
                            case 4:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> knocks <b>{unit.Name}</b> down onto {LogUtilities.GPPHis(unit)} back, before plunging into the center of <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> right {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}. Then {LogUtilities.GPPHe(forcePrey.Unit)} slowly feed{LogUtilities.SIfSingular(forcePrey.Unit)} the rest of {LogUtilities.GPPHimself(forcePrey.Unit)} into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}.");
                                break;
                            default:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. With a motion resembling a headbutt, <b>{forcePrey.Unit.Name}</b> slips {LogUtilities.GPPHis(forcePrey.Unit)} head into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> right {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");
                                break;
                            }}, "Right", "Left", "Which breast should the prey be put in? (from your pov)", () => { leftBreast.Add(preyref); UpdateFullness(); 
                            switch (State.Rand.Next(5))
                            {
                            case 0:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> grabs <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> left {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )} and shoves it into {LogUtilities.GPPHis(forcePrey.Unit)} face. Then, with a wet *shlorp* sound, <b>{LogUtilities.ApostrophizeWithOrWithoutS(forcePrey.Unit.Name)}</b> head disappears into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");
                                break;
                            case 1:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> latches onto <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> left {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, suckling for a moment or two, before forcing {LogUtilities.GPPHimself(forcePrey.Unit)} through the nipple, and vanishing into the boob beyond.");
                                break;
                            case 2:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. With a motion resembling a headbutt, <b>{forcePrey.Unit.Name}</b> slips {LogUtilities.GPPHis(forcePrey.Unit)} head into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> left {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");
                                break;
                            case 3:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sucks on <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> left {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}. As <b>{unit.Name}</b> relaxes to let it happen, <b>{forcePrey.Unit.Name}</b> pulls back and then dives head first into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> left {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}.");
                                break;
                            case 4:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> knocks <b>{unit.Name}</b> down onto {LogUtilities.GPPHis(unit)} back, before plunging into the center of <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> left {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}. Then {LogUtilities.GPPHe(forcePrey.Unit)} slowly feed{LogUtilities.SIfSingular(forcePrey.Unit)} the rest of {LogUtilities.GPPHimself(forcePrey.Unit)} into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}.");
                                break;
                            default:
                                State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. With a motion resembling a headbutt, <b>{forcePrey.Unit.Name}</b> slips {LogUtilities.GPPHis(forcePrey.Unit)} head into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> left {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");
                                break;
                            }});
                        break;
                    }
                    int breastSide = 1;
                    if (LeftBreastFullness < RightBreastFullness || State.Rand.Next(2) == 0)
                        leftBreast.Add(preyref);
                    else
                    {rightBreast.Add(preyref); breastSide = 2;}
                    switch (State.Rand.Next(5))
                    {
                        case 0:
                            {int bS = breastSide;
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> grabs <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {(bS == 1 ? "left" : "right")} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )} and shoves it into {LogUtilities.GPPHis(forcePrey.Unit)} face. Then, with a wet *shlorp* sound, <b>{LogUtilities.ApostrophizeWithOrWithoutS(forcePrey.Unit.Name)}</b> head disappears into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");}
                            break;
                        case 1:
                            {int bS = breastSide;
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> latches onto <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {(bS == 1 ? "left" : "right")} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, suckling for a moment or two, before forcing {LogUtilities.GPPHimself(forcePrey.Unit)} through the nipple, and vanishing into the boob beyond.");}
                            break;
                        case 2:
                            {int bS = breastSide;
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. With a motion resembling a headbutt, <b>{forcePrey.Unit.Name}</b> slips {LogUtilities.GPPHis(forcePrey.Unit)} head into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {(bS == 1 ? "left" : "right")} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");}
                            break;
                        case 3:
                            {int bS = breastSide;
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sucks on <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {(bS == 1 ? "left" : "right")} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}. As <b>{unit.Name}</b> relaxes to let it happen, <b>{forcePrey.Unit.Name}</b> pulls back and then dives head first into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {(bS == 1 ? "left" : "right")} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}.");}
                            break;
                        case 4:
                            {int bS = breastSide;
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> knocks <b>{unit.Name}</b> down onto {LogUtilities.GPPHis(unit)} back, before plunging into the center of <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {(bS == 1 ? "left" : "right")} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}. Then {LogUtilities.GPPHe(forcePrey.Unit)} slowly feed{LogUtilities.SIfSingular(forcePrey.Unit)} the rest of {LogUtilities.GPPHimself(forcePrey.Unit)} into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}.");}
                            break;
                        default:
                            {int bS = breastSide;
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. With a motion resembling a headbutt, <b>{forcePrey.Unit.Name}</b> slips {LogUtilities.GPPHis(forcePrey.Unit)} head into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {(bS == 1 ? "left" : "right")} {LogUtilities.GetRandomStringFrom( "breast", "mammary", "boob", "tit", "jug", "knocker" )}, followed shortly by the rest of {LogUtilities.GPPHis(forcePrey.Unit)} body.");}
                            break;
                    }
                    break;
                }                
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.breasts, actor);
                loc = PreyLocation.breasts;
                breasts.Add(preyref);
                switch (State.Rand.Next(6))
                {
                    case 0:
                        if (unit.Race == Race.Kangaroos)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> approaches <b>{unit.Name}</b> and abruptly yanks open {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")}. Before the {LogUtilities.GetRaceDescSingl(unit)} can comment on how frankly rude this is, <b>{forcePrey.Unit.Name}</b> has already forced {LogUtilities.GPPHis(forcePrey.Unit)} way inside.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"In just a few deft movements, <b>{forcePrey.Unit.Name}</b> crams {LogUtilities.GPPHimself(forcePrey.Unit)} into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tits.");
                        break;
                    case 1:
                        if (unit.Race == Race.Kangaroos)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"While <b>{unit.Name}</b> isn't paying attention, <b>{forcePrey.Unit.Name}</b> jumps into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} pouch. Somehow, <b>{unit.Name}</b> doesn't notice this, and carries on as {LogUtilities.GPPHe(unit)} {LogUtilities.WasWere(unit)}.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> charges into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.breasts)}, breifly motorboating {LogUtilities.GPPHim(unit)} before pushing extra hard and vanishing into the space between the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(PreyLocation.breasts)}.");
                        break;
                    case 2:
                        if (unit.Race == Race.Kangaroos)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> has already decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} {LogUtilities.GetRaceDescSingl(unit)} food. The only questions left are who and how? For the who, <b>{forcePrey.Unit.Name}</b> selects <b>{unit.Name}</b>. After some thought, the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)} decides to see what the inside of a {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")} is like. Before <b>{unit.Name}</b> can attempt to stop {LogUtilities.GPPHim(forcePrey.Unit)}, the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)} is already fully within {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")}.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"Using <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.breasts)} as handholds, <b>{forcePrey.Unit.Name}</b> pulls {LogUtilities.GPPHimself(forcePrey.Unit)} into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} cleavage.");
                        break;
                    case 3:
                        if (unit.Race == Race.Kangaroos)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"After forcing {LogUtilities.GPPHimself(forcePrey.Unit)} into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")}, <b>{forcePrey.Unit.Name}</b> is a touch surprised by the way the {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")}'s entrance seals above {LogUtilities.GPPHim(forcePrey.Unit)}. Unlike most force-feeders, <b>{forcePrey.Unit.Name}</b> doesn't necissarely want to be digested. So now, both <b>{unit.Name}</b> and <b>{forcePrey.Unit.Name}</b> get to be unhappy!");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> approaches <b>{unit.Name}</b> and abruptly pulls apart {LogUtilities.GPPHis(unit)} {PreyLocStrings.ToSyn(PreyLocation.breasts)}, before stuffing {LogUtilities.GPPHimself(forcePrey.Unit)} into the gap between.");
                        break;
                    case 4:
                        if (unit.Race == Race.Kangaroos)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> smacks <b>{unit.Name}</b> across the face. While {LogUtilities.GPPHeIs(unit)} dazed, <b>{forcePrey.Unit.Name}</b> climbs into {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")}. When <b>{unit.Name}</b> regains {LogUtilities.GPPHis(unit)} senses, {LogUtilities.GPPHe(unit)} say{LogUtilities.SIfSingular(unit)} \"You could have just asked.\"");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> grabs <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.breasts)} and shoves {LogUtilities.GPPHimself(forcePrey.Unit)} between {LogUtilities.GPPHis(unit)} {PreyLocStrings.ToSyn(PreyLocation.breasts)}! Much to <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> surprise, {LogUtilities.GPPHe(forcePrey.Unit)} sink{LogUtilities.SIfSingular(forcePrey.Unit)} into the soft flesh at the bottom, becoming living fat on <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.breasts)}.");
                        break;
                    case 5:
                        if (unit.Race == Race.Kangaroos)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> smacks <b>{unit.Name}</b> across the face. While {LogUtilities.GPPHeIs(unit)} dazed, <b>{forcePrey.Unit.Name}</b> climbs into {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")}.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> knocks <b>{unit.Name}</b> down onto {LogUtilities.GPPHis(unit)} back, before {LogUtilities.GetRandomStringFrom("jumping feet first", "diving head first")} into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.breasts)}, dissapearing with one quick *shlump* noise.");
                        break;
                    default:
                        if (unit.Race == Race.Kangaroos)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> approaches <b>{unit.Name}</b> and abruptly yanks open {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("lower torso", "pouch", "marsupium")}. Before the {LogUtilities.GetRaceDescSingl(unit)} can comment on how frankly rude this is, <b>{forcePrey.Unit.Name}</b> has already forced {LogUtilities.GPPHis(forcePrey.Unit)} way inside.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"In just a few deft movements, <b>{forcePrey.Unit.Name}</b> crams {LogUtilities.GPPHimself(forcePrey.Unit)} into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tits.");
                        break;
                }
                break;
            case VoreType.TailVore:
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.tail, actor);
                loc = PreyLocation.tail;
                tail.Add(preyref);
                switch (State.Rand.Next(4))
                {
                    case 0:
                        if (unit.Race == Race.Youko)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> leaps into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> bundle of tails. Quickly {LogUtilities.GPPHe(forcePrey.Unit)} vanish{LogUtilities.EsIfSingular(forcePrey.Unit)} into the ball of fluff; feeling quite comfy.");
                        else if (unit.Race == Race.Terrorbird)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> leaps into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> gullet appearing to be stopped on {LogUtilities.GPPHis(forcePrey.Unit)} way to the bird's stomach, finding {LogUtilities.GPPHimself(forcePrey.Unit)} rather stuck in the bird's crop instead.");
                        else if (unit.Race == Race.Bees)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> manages to knock <b>{unit.Name}</b> out of the sky. Before the bee has a chance to get airborne again, <b>{forcePrey.Unit.Name}</b> has already forced {LogUtilities.GPPHis(forcePrey.Unit)} way into {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("stinger", "honey chamber", "honey-filled tail")}.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> forces {LogUtilities.GPPHimself(forcePrey.Unit)} up <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tail. Inch by inch {LogUtilities.GPPHe(forcePrey.Unit)} vigorously squeez{LogUtilities.EsIfSingular(forcePrey.Unit)} {LogUtilities.GPPHimself(forcePrey.Unit)} into the tail's depths.");
                        break;
                    case 1:
                        if (unit.Race == Race.Youko)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> charges <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> rear, grabbing the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} tails and quickly wrapping {LogUtilities.GPPHimself(forcePrey.Unit)} up in them.");
                        else if (unit.Race == Race.Terrorbird)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> walks up to <b>{unit.Name}</b>, pries {LogUtilities.GPPHis(unit)} beak open, and climbs in, stopping in the confused {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} crop.");
                        else if (unit.Race == Race.Bees)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> gets under <b>{unit.Name}</b>, and pries open {LogUtilities.GPPHis(unit)} stinger, before crawling up into the honey filled chamber within{LogUtilities.GetRandomStringFrom(".", $", much to <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> displeasure.")}");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> throws {LogUtilities.GPPHimself(forcePrey.Unit)} at <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tail, pulling open the tip and pushing {LogUtilities.GPPHimself(forcePrey.Unit)} inside before the {LogUtilities.GetRaceDescSingl(unit)} can stop {LogUtilities.GPPHim(forcePrey.Unit)}.");
                        break;
                    case 2:
                        if (unit.Race == Race.Youko)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"As <b>{unit.Name}</b> stands there, minding {LogUtilities.GPPHis(unit)} own business, <b>{forcePrey.Unit.Name}</b> comes up behind {LogUtilities.GPPHim(unit)} and forcibly wraps the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} tails around {LogUtilities.GPPHimself(forcePrey.Unit)}{LogUtilities.GetRandomStringFrom(".", $", and when <b>{unit.Name}</b> tries to unfurl {LogUtilities.GPPHis(unit)} tails, <b>{forcePrey.Unit.Name}</b> simply holds on tighter.")}");
                        else if (unit.Race == Race.Terrorbird)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> climbs up onto <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> back, as though to ride the {LogUtilities.GetRaceDescSingl(unit)}, but instead, {LogUtilities.GPPHe(forcePrey.Unit)} use{LogUtilities.SIfSingular(forcePrey.Unit)} {LogUtilities.GPPHis(forcePrey.Unit)} position to force open <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> beak, before quickly flipping {LogUtilities.GPPHimself(forcePrey.Unit)} over and into it, sliding down before {LogUtilities.GetRandomStringFrom($"suddenly finding {LogUtilities.GPPHimself(forcePrey.Unit)} stuck in the bird's crop, rather than the stomach", $"splaying out to stop {LogUtilities.GPPHis(forcePrey.Unit)} decent in the crop")}.");
                        else if (unit.Race == Race.Bees)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> baits <b>{unit.Name}</b> into trying to sting {LogUtilities.GPPHim(forcePrey.Unit)}, and then, slightly painfully for the {LogUtilities.GetRaceDescSingl(unit)}, pulls the stinger open and goes inside.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> wrestles <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tail, before prying open the tip and slipping inside.");
                        break;
                    case 3:
                        if (unit.Race == Race.Youko)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> slowly sneaks into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> bundle of tails. Before <b>{unit.Name}</b> can react {LogUtilities.GPPHis(unit)} tails have already wrapped up the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)} in their {LogUtilities.GetRandomStringFrom("comfy", "tight", "fluffly")} embrace, refusing to let go.");
                        else if (unit.Race == Race.Terrorbird)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> watches <b>{unit.Name}</b> closely, and then, seeing an opportunity, jumps for the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} open beak, sliding in and coming to a stop in {LogUtilities.GPPHis(unit)} crop.");
                        else if (unit.Race == Race.Bees)
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> sneaks up behind <b>{unit.Name}</b>, before shoving the {LogUtilities.GetRaceDescSingl(unit)} hard, knocking {LogUtilities.GPPHim(unit)} off balance. By the time {LogUtilities.GPPHe(unit)} {LogUtilities.HasHave(unit)} regained that balance, <b>{forcePrey.Unit.Name}</b> is mostly inside {LogUtilities.GPPHis(unit)} stinger.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{unit.Name}</b> stops as {LogUtilities.GPPHe(unit)} feel something at the tip of {LogUtilities.GPPHis(unit)} tail. Looking behind {LogUtilities.GPPHimself(unit)}, {LogUtilities.GPPHe(unit)} spot{LogUtilities.SIfSingular(unit)} <b>{forcePrey.Unit.Name}</b> messing with {LogUtilities.GPPHis(unit)} tail before- <i>shlorp</i> -forcing {LogUtilities.GPPHis(forcePrey.Unit)} way inside.");
                        break;
                    default:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> forces {LogUtilities.GPPHimself(forcePrey.Unit)} up <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> tail. Inch by inch {LogUtilities.GPPHe(forcePrey.Unit)} vigorously squeez{LogUtilities.EsIfSingular(forcePrey.Unit)} {LogUtilities.GPPHimself(forcePrey.Unit)} into the tail's depths.");
                        break;
                }
                break;
            default:
                State.GameManager.SoundManager.PlaySwallow(PreyLocation.stomach, actor);
                AddToStomach(preyref, 1f);
                switch (State.Rand.Next(12))
                {
                    case 0:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"At {LogUtilities.GPPHis(forcePrey.Unit)} first glimpse of the {(LogUtilities.ActorHumanoid(unit) ? "warrior's" : "beast's")} maw, <b>{forcePrey.Unit.Name}</b> dives right down {LogUtilities.GPPHis(unit)} gullet. One swallow reflex later, <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> belly has been filled.");
                        break;
                    case 1:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> walks up to <b>{unit.Name}</b> and pries {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("jaws", "maw", "mouth", "muzzle", "gob")} open before manually crawling down into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(PreyLocation.stomach)}, much to <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> confusion.");
                        break;
                    case 2:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"Noticing that {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("jaws", "maw", "mouth", "muzzle", "gob")} is slightly ajar, <b>{forcePrey.Unit.Name}</b> makes a running leap into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {LogUtilities.GetRandomStringFrom("jaws", "maw", "mouth", "muzzle", "gob")}, sliding quickly down into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(PreyLocation.stomach)}{LogUtilities.GetRandomStringFrom("!", ".")}");
                        break;
                    case 3:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> spots <b>{unit.Name}</b> and decided that {LogUtilities.GPPHeIsAbbr(forcePrey.Unit)} got to go inside. Without so much as an \"excuse me,\" the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)} unapologetically forces {LogUtilities.GPPHimself(forcePrey.Unit)} down the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {LogUtilities.GetRandomStringFrom("jaws", "maw", "mouth", "muzzle", "gob")}.");
                        break;
                    case 4:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> abruptly sticks {LogUtilities.GPPHis(forcePrey.Unit)} head in <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> mouth, looking around the inside for a few moments, before forcing {LogUtilities.GPPHis(forcePrey.Unit)} way down into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(forcePrey.Unit))} {PreyLocStrings.ToSyn(PreyLocation.stomach)}.");
                        break;
                    case 5:
                        if (LogUtilities.ActorHumanoid(forcePrey.Unit))
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"As <b>{forcePrey.Unit.Name}</b> walks over to <b>{unit.Name}</b>, {LogUtilities.GPPHe(forcePrey.Unit)} pick{LogUtilities.SIfSingular(forcePrey.Unit)} up a stick. Once at the {LogUtilities.GetRaceDescSingl(unit)}, {LogUtilities.GPPHe(forcePrey.Unit)} use{LogUtilities.SIfSingular(forcePrey.Unit)} the stick to force open <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {LogUtilities.GetRandomStringFrom("jaws", "maw", "mouth", "muzzle", "gob")}. By the time that {LogUtilities.GPPHe(unit)} can spit the stick out, the {LogUtilities.GetRaceDescSingl(forcePrey.Unit)} is already in {LogUtilities.GPPHis(unit)} {PreyLocStrings.ToSyn(PreyLocation.stomach)}.");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"At {LogUtilities.GPPHis(forcePrey.Unit)} first glimpse of the {(LogUtilities.ActorHumanoid(unit) ? "warrior's" : "beast's")} maw, <b>{forcePrey.Unit.Name}</b> dives right down {LogUtilities.GPPHis(unit)} gullet. One swallow reflex later, <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> belly has been filled.");
                        break;
                        //Below logs are generalized for potential future force feed compatability
                    case 6:
                        if (LogUtilities.ActorHumanoid(unit))
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"After <b>{forcePrey.Unit.Name}</b> finished forcing {LogUtilities.GPPHimself(forcePrey.Unit)} into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(forcePrey.SelfPrey.Location)}, <b>{unit.Name}</b> says simply; \"Rude. If you wanted in, you could've let me choose were to put you, or at least let me get some pleasure out of your journey in.\"");
                        else
                            State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"It all happened so fast. One moment, <b>{unit.Name}</b> was bracing {LogUtilities.GPPHimself(unit)} against a charging {LogUtilities.GetRaceDescSingl(forcePrey.Unit)}. The next, {LogUtilities.GPPHis(unit)} {PreyLocStrings.ToSyn(forcePrey.SelfPrey.Location)} was bulging out with <b>{forcePrey.Unit.Name}</b> stored inside.");
                        break;
                    case 7:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"It all happened so fast. One moment, <b>{unit.Name}</b> was bracing {LogUtilities.GPPHimself(unit)} against a charging {LogUtilities.GetRaceDescSingl(forcePrey.Unit)}. The next, {LogUtilities.GPPHis(unit)} {PreyLocStrings.ToSyn(forcePrey.SelfPrey.Location)} was bulging out with <b>{forcePrey.Unit.Name}</b> stored inside.");
                        break;
                    case 8:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> crams {LogUtilities.GPPHimself(forcePrey.Unit)} into <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> {PreyLocStrings.ToSyn(forcePrey.SelfPrey.Location)}{LogUtilities.GetRandomStringFrom("!", ".")}");
                        break;
                    case 9:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"One moment, <b>{forcePrey.Unit.Name}</b> was outside of <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> body. The next, {LogUtilities.GPPHe(forcePrey.Unit)} {LogUtilities.WasWere(forcePrey.Unit)} inside. Notably, <b>{unit.Name}</b> was given no choice in this decision.");
                        break;
                    case 10:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> walks up to <b>{unit.Name}</b> and unceremoniously forces {LogUtilities.GPPHimself(forcePrey.Unit)} into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(forcePrey.SelfPrey.Location)}. <b>{unit.Name}</b>, for {LogUtilities.GPPHis(unit)} part stares at {LogUtilities.GPPHis(unit)} {LogUtilities.GetRandomStringFrom("bloated", "engorged", "enlarged", "bulging" )} {PreyLocStrings.ToSyn(forcePrey.SelfPrey.Location)} with utter confusion.");
                        break;
                    case 11:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"<b>{forcePrey.Unit.Name}</b> tackles <b>{unit.Name}</b>, and uses the moment of confusion to force {LogUtilities.GPPHimself(forcePrey.Unit)} into the {LogUtilities.ApostrophizeWithOrWithoutS(LogUtilities.GetRaceDescSingl(unit))} {PreyLocStrings.ToSyn(forcePrey.SelfPrey.Location)}.");
                        break;
                    default:
                        State.GameManager.TacticalMode.Log.RegisterMiscellaneous($"At {LogUtilities.GPPHis(forcePrey.Unit)} first glimpse of the {(LogUtilities.ActorHumanoid(unit) ? "warrior's" : "beast's")} maw, <b>{forcePrey.Unit.Name}</b> dives right down {LogUtilities.GPPHis(unit)} gullet. One swallow reflex later, <b>{LogUtilities.ApostrophizeWithOrWithoutS(unit.Name)}</b> belly has been filled.");
                        break;
                }
                break;
        }
        AddPrey(preyref);
        actor.SetPredMode(loc);
        if (loc == PreyLocation.stomach && actor.Unit.Race == Race.Ryan)
            actor.SetVoreSuccessMode();
        if (actor.Unit.Race != Race.Ryan)
            actor.SetVoreSuccessMode();
        UpdateFullness();
    }
}

