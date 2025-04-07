using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

struct AcceptibleTargets
{
    internal AbilityTargets[] Targets;

    public AcceptibleTargets(AbilityTargets[] targets, int minRange, int maxRange)
    {
        Targets = targets;

    }
}



class TargetedTacticalAction
{
    internal string Name;
    internal bool RequiresPred;
    internal Predicate<Actor_Unit> AppearConditional;
    internal int MinimumMP;
    internal int ManaCost;
    internal Color ButtonColor;
    internal Action OnClicked;
    internal Func<Actor_Unit, Actor_Unit, bool> OnExecute;
    internal Func<Actor_Unit, Vec2i, bool> OnExecuteLocation;

    public TargetedTacticalAction(string name, bool requiresPred, Predicate<Actor_Unit> conditional, Action onClicked, Func<Actor_Unit, Actor_Unit, bool> onExecute, Func<Actor_Unit, Vec2i, bool> onExecuteLocation = null, int minimumMp = 1, Color color = default, int manaCost = 0)
    {
        Name = name;
        RequiresPred = requiresPred;
        AppearConditional = conditional;
        OnClicked = onClicked;
        OnExecute = onExecute;
        OnExecuteLocation = onExecuteLocation;
        MinimumMP = minimumMp;
        ManaCost = manaCost;
        if (color == default)
            ButtonColor = new Color(.669f, .753f, 1);
        else
            ButtonColor = color;
    }
}

class UntargetedTacticalAction
{
    internal Color ButtonColor;
    internal string Name;
    internal Action OnClicked;
    internal Predicate<Actor_Unit> AppearConditional;

    public UntargetedTacticalAction(string name, Action onClicked, Predicate<Actor_Unit> conditional, Color color = default)
    {
        Name = name;
        OnClicked = onClicked;
        if (color == default)
            ButtonColor = new Color(.669f, .753f, 1);
        else
            ButtonColor = color;
        AppearConditional = conditional;
    }
}

static class TacticalActionList
{
    static internal List<TargetedTacticalAction> TargetedActions;
    static internal List<UntargetedTacticalAction> UntargetedActions;

    static internal Dictionary<SpecialAction, TargetedTacticalAction> TargetedDictionary;

    static TacticalActionList()
    {
        TargetedDictionary = new Dictionary<SpecialAction, TargetedTacticalAction>();
        TargetedActions = new List<TargetedTacticalAction>();
        UntargetedActions = new List<UntargetedTacticalAction>();

        TargetedActions.Add(new TargetedTacticalAction(
            name: "Belly Rub",
            requiresPred: false,
            conditional: (a) => true,
            onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.BellyRub),
            onExecute: (a, t) => { return t.Unit.Predator ? a.BellyRub(t) : false; }));
        TargetedDictionary[SpecialAction.BellyRub] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
            name: "Unbirth",
            requiresPred: true,
            conditional: (a) => a.Unit.CanUnbirth && State.RaceSettings.GetVoreTypes(a.Unit.Race).Contains(VoreType.Unbirth),
            onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.Unbirth),
            onExecute: (a, t) => a.PredatorComponent.Unbirth(t)));
        TargetedDictionary[SpecialAction.Unbirth] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
            name: "Breast Vore",
            requiresPred: true,
            conditional: (a) => a.Unit.CanBreastVore && State.RaceSettings.GetVoreTypes(a.Unit.Race).Contains(VoreType.BreastVore),
            onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.BreastVore),
            onExecute: (a, t) => a.PredatorComponent.BreastVore(t)));
        TargetedDictionary[SpecialAction.BreastVore] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
            name: "Cock Vore",
            requiresPred: true,
            conditional: (a) => a.Unit.CanCockVore && State.RaceSettings.GetVoreTypes(a.Unit.Race).Contains(VoreType.CockVore),
            onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.CockVore),
            onExecute: (a, t) => a.PredatorComponent.CockVore(t)));
        TargetedDictionary[SpecialAction.CockVore] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
          name: "Anal Vore",
          requiresPred: true,
          conditional: (a) => a.Unit.CanAnalVore && State.RaceSettings.GetVoreTypes(a.Unit.Race).Contains(VoreType.Anal),
          onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.AnalVore),
          onExecute: (a, t) => a.PredatorComponent.AnalVore(t)));
        TargetedDictionary[SpecialAction.AnalVore] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
            name: "Tail Vore",
            requiresPred: true,
            conditional: (a) => a.Unit.CanTailVore && State.RaceSettings.GetVoreTypes(a.Unit.Race).Contains(VoreType.TailVore),
            onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.TailVore),
            onExecute: (a, t) => a.PredatorComponent.TailVore(t)));
        TargetedDictionary[SpecialAction.TailVore] = TargetedActions.Last();

        //TargetedActions.Add(new TargetedTacticalAction(
        //   name: "Crop Vore",
        //   requiresPred: true,
        //   conditional: (a) => a.Unit.Race == Race.Terrorbird,
        //   onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.TailVore),
        //   onExecute: (a, t) => a.PredatorComponent.TailVore(t)));

        //Actions.Add(new TacticalAction("Transfer", true, () => State.GameManager.TacticalMode.ButtonCallback(25), 

        TargetedActions.Add(new TargetedTacticalAction(
            name: "Transfer",
            requiresPred: true,
            conditional: (a) => a.PredatorComponent?.CanTransfer() ?? false,
            onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.Transfer),
            onExecute: (a, t) => a.PredatorComponent.TransferAttempt(t)));
        TargetedDictionary[SpecialAction.Transfer] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
            name: "Kiss Transfer",
            requiresPred: true,
            conditional: (a) => a.PredatorComponent?.CanKissTransfer() ?? false,
            onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.KissTransfer),
            onExecute: (a, t) => a.PredatorComponent.KissTransferAttempt(t)));
        TargetedDictionary[SpecialAction.KissTransfer] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
            name: "Vore Steal",
            requiresPred: true,
            conditional: (a) => Config.TransferAllowed == true && Config.KuroTenkoEnabled == true && a.Unit.Predator,
            onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.StealVore),
            onExecute: (a, t) => a.PredatorComponent.VoreStealAttempt(t)));
        TargetedDictionary[SpecialAction.StealVore] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
            name: "Breastfeed",
            requiresPred: true,
            conditional: (a) => a.PredatorComponent?.CanFeed() ?? false,
            onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.BreastFeed),
            onExecute: (a, t) => { return t.Unit.Side == a.Unit.Side ? a.PredatorComponent.Feed(t) : false; }));
        TargetedDictionary[SpecialAction.BreastFeed] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
            name: "Suckle",
            requiresPred: true,
            conditional: (a) => a.PredatorComponent?.CanSuckle() ?? false,
            onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.Suckle),
            onExecute: (a, t) => a.PredatorComponent.Suckle(t)));
        TargetedDictionary[SpecialAction.Suckle] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
            name: "Cum Feed",
            requiresPred: true,
            conditional: (a) => a.PredatorComponent?.CanFeedCum() ?? false,
            onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.CumFeed),
            onExecute: (a, t) => { return t.Unit.Side == a.Unit.Side && t.Unit != a.Unit ? a.PredatorComponent.FeedCum(t) : false; }));
        TargetedDictionary[SpecialAction.CumFeed] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
            name: "Melee Pounce",
            requiresPred: false,
            conditional: (a) => a.Unit.HasTrait(Traits.Pounce),
            onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.PounceMelee),
            onExecute: (a, t) => a.MeleePounce(t),
            minimumMp: 2));

        TargetedDictionary[SpecialAction.PounceMelee] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
            name: "Vore Pounce",
            requiresPred: true,
            conditional: (a) => a.Unit.HasTrait(Traits.Pounce) && a.Unit.Predator,
            onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.PounceVore),
            onExecute: (a, t) => a.VorePounce(t),
            minimumMp: 2));
        TargetedDictionary[SpecialAction.PounceVore] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
           name: "Shun Goku Satsu",
           requiresPred: false,
           conditional: (a) => a.Unit.HasTrait(Traits.ShunGokuSatsu) && a.TurnUsedShun + 3 <= State.GameManager.TacticalMode.currentTurn,
           onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.ShunGokuSatsu),
           onExecute: (a, t) => a.ShunGokuSatsu(t),
           minimumMp: 1));
        TargetedDictionary[SpecialAction.ShunGokuSatsu] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
            name: "Regurgitate",
            requiresPred: true,
            conditional: (a) => a.PredatorComponent?.AlivePrey > 0 && a.Unit.HasTrait(Traits.Greedy) == false,
            onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.Regurgitate),
            onExecute: null,
            onExecuteLocation: (a, l) => a.Regurgitate(a, l),
            minimumMp: 1));
        TargetedDictionary[SpecialAction.Regurgitate] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
          name: "Tail Strike",
          requiresPred: false,
          conditional: (a) => a.Unit.HasTrait(Traits.TailStrike),
          onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.TailStrike),
          onExecute: (a, t) => a.TailStrike(t),
          minimumMp: 1));
        TargetedDictionary[SpecialAction.TailStrike] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
          name: "Giant Sweep",
          requiresPred: false,
          conditional: (a) => a.Unit.HasTrait(Traits.Legendary),
          onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.GiantSweep),
          onExecute: (a, t) => a.SweepAttack(true),
          manaCost: 40,
          minimumMp: 1));
        TargetedDictionary[SpecialAction.GiantSweep] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
          name: "SweepingSwallow",
          requiresPred: false,
          conditional: (a) => a.Unit.HasTrait(Traits.Legendary),
          onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.SweepingSwallow),
          onExecute: (a, t) => a.SweepAttack(false),
          manaCost: 40,
          minimumMp: 1));
        TargetedDictionary[SpecialAction.SweepingSwallow] = TargetedActions.Last();

        TargetedActions.Add(new TargetedTacticalAction(
            name: "All-In Vore",
            requiresPred: true,
            conditional: (a) => a.Unit.HasTrait(Traits.AllIn) && a.Unit.Predator,
            onClicked: () => State.GameManager.TacticalMode.TrySetSpecialMode(SpecialAction.AllInVore),
            onExecute: (a, t) => a.AllInVore(t),
            minimumMp: 2));
        TargetedDictionary[SpecialAction.AllInVore] = TargetedActions.Last();


        //UntargetedActions.Add(new UntargetedTacticalAction("Shapeshift", () => State.GameManager.TacticalMode.ButtonCallback(16), (a) => a.Unit.ShifterShapes != null && a.Unit.ShifterShapes.Count > 1));
        UntargetedActions.Add(new UntargetedTacticalAction("Flee", () => State.GameManager.TacticalMode.ButtonCallback(10), (a) => true));
        UntargetedActions.Add(new UntargetedTacticalAction("Surrender", () => State.GameManager.TacticalMode.ButtonCallback(9), (a) => true, new Color(.9f, .65f, .65f)));
        UntargetedActions.Add(new UntargetedTacticalAction("Reveal", () => State.GameManager.TacticalMode.ButtonCallback(15), (a) => a.Unit.hiddenFixedSide && TacticalUtilities.PlayerCanSeeTrueSide(a.Unit)));
        UntargetedActions.Add(new UntargetedTacticalAction("Defect", () => State.GameManager.TacticalMode.ButtonCallback(14), (a) => a.allowedToDefect));
    }
}

