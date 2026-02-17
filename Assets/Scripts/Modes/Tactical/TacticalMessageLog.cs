using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static LogUtilities;
using Random = UnityEngine.Random;

public class TacticalMessageLog
{
    [OdinSerialize]
    List<EventLog> events;

    public bool ShowOdds = false;
    public bool ShowHealing = true;
    public bool ShowSpells = true;
    public bool ShowMisses = true;
    public bool ShowWeaponCombat = true;
    public bool ShowInformational = true;
    public bool ShowPureFluff = true;

    public bool SimpleText = false;

    Unit defaultPrey;

    public TacticalMessageLog()
    {
        defaultPrey = new Unit(Race.Humans);
        defaultPrey.DefaultBreastSize = -1;
        defaultPrey.DickSize = -1;
        defaultPrey.Name = "[Redacted]";
        events = new List<EventLog>();
    }

    internal class EventLog
    {
        [OdinSerialize]
        internal MessageLogEvent Type;
        [OdinSerialize]
        internal float Odds;
        [OdinSerialize]
        internal Unit Target;
        [OdinSerialize]
        internal Unit Unit;
        [OdinSerialize]
        internal Unit Prey;
        [OdinSerialize]
        internal PreyLocation preyLocation;
        [OdinSerialize]
        internal PreyLocation oldLocation;
        [OdinSerialize]
        internal Weapon Weapon;
        [OdinSerialize]
        internal int Damage;
        [OdinSerialize]
        internal int Bonus;
        [OdinSerialize]
        internal int RebirthType;
        [OdinSerialize]
        internal string Message;
        [OdinSerialize]
        internal string Extra;
    }




    class SpellLog : EventLog
    {
        [OdinSerialize]
        internal SpellTypes SpellType;
    }

    internal enum MessageLogEvent
    {
        Hit,
        Miss,
        Devour,
        Unbirth,
        CockVore,
        BreastVore,
        BellyRub,
        BreastRub,
        BallMassage,
        Feed,
        Birth,
        TransferFail,
        TransferSuccess,
        KissTransfer,
        Resist,
        Kill,
        Digest,
        Absorb,
        Escape,
        Freed,
        Regurgitated,
        Heal,
        NewTurn,
        LowHealth,
        FeedCum,
        RandomDigestion = 25, //Done because of a removed category
        Miscellaneous,
        PartialEscape,
        TailVore,
        AnalVore,
        Dazzle,
        Block,
        SpellHit,
        SpellMiss,
        SpellKill,
        CurseExpires,
        DiminishmentExpires,
        TailRub,
        Suckle,
        SuckleFail,
        VoreStealFail,
        VoreStealSuccess,
        GreatEscapeKeep,
        GreatEscapeFlee,
        ManualRegurgitation,
        BladderVore,
    }

    public void RefreshListing()
    {
        StringBuilder sb = new StringBuilder();
        var validEvents = events.Where(s => EventValid(s));
        List<EventLog> last200;

        if (validEvents.Count() > 200)
        {
            last200 = validEvents.ToList().GetRange(validEvents.Count() - 200, 200);
        }
        else
            last200 = validEvents.ToList();

        foreach (EventLog action in last200)
        {
            sb.AppendLine(EventDescription(action));
        }
        State.GameManager.TacticalMode.LogUI.Text.text = sb.ToString();
        State.GameManager.TacticalMode.LogUI.Text.transform.Translate(new Vector3(0, 30000, 0));
    }

    internal string DebugDump()
    {
        StringBuilder sb = new StringBuilder();

        foreach (EventLog action in events)
        {
            sb.AppendLine(EventDescription(action));
        }
        return sb.ToString();
    }

    bool EventValid(EventLog test)
    {
        switch (test.Type)
        {
            case MessageLogEvent.Heal:
                if (ShowHealing == false) return false; break;
            case MessageLogEvent.Miss:
            case MessageLogEvent.Block:
                if (ShowMisses == false || ShowWeaponCombat == false) return false; break;
            case MessageLogEvent.Resist:
            case MessageLogEvent.Dazzle:
            case MessageLogEvent.SpellMiss:
                if (ShowMisses == false || ShowSpells == false) return false; break;
            case MessageLogEvent.SpellHit:
                if (ShowSpells == false) return false; break;
            case MessageLogEvent.LowHealth:
            case MessageLogEvent.Absorb:
            case MessageLogEvent.NewTurn:
                if (ShowInformational == false) return false; break;
            case MessageLogEvent.RandomDigestion:
            case MessageLogEvent.GreatEscapeKeep:
                if (ShowPureFluff == false) return false; break;
            case MessageLogEvent.Hit:
                if (ShowWeaponCombat == false) return false; break;

        }
        return true;
    }

    public void Clear()
    {
        events.Clear();
        State.GameManager.TacticalMode.LogUI.Text.text = "";
    }



    void UpdateListing()
    {
        if (events.Count > 4000)
            events.RemoveRange(0, 400);
        if (State.GameManager.TacticalMode.turboMode)
            return;
        if (EventValid(events.Last()) == false)
            return;
        State.GameManager.TacticalMode.LogUI.Text.text += EventDescription(events.Last()) + "\n";
        if (State.GameManager.TacticalMode.LogUI.Text.text.Length > 10000)
        {
            State.GameManager.TacticalMode.LogUI.Text.text = State.GameManager.TacticalMode.LogUI.Text.text.Substring(1000);
        }
        State.GameManager.TacticalMode.TacticalLogUpdated = true;
    }

    private string EventDescription(EventLog action)
    {
        string odds = "";
        if (ShowOdds && action.Odds > 0)
            odds = $" ({Math.Round(action.Odds * 100f, 2)}% success)";
        string msg;
        switch (action.Type)
        {
            case MessageLogEvent.Hit:
                return $"<b>{action.Unit.Name}</b> hit <b>{action.Target.Name}</b> with a {GetWeaponTrueName(action.Weapon, action.Unit)} for <color=red>{action.Damage}</color> points of damage.{odds}";
            case MessageLogEvent.Miss:
                msg = GenerateMissMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.Devour:
                msg = GenerateSwallowMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.Unbirth:
                msg = GenerateUBSwallowMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.CockVore:
                msg = GenerateCVSwallowMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.BreastVore:
                msg = GenerateBVSwallowMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.TailVore:
                msg = GenerateTVSwallowMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.AnalVore:
                msg = GenerateAVSwallowMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.BladderVore:
                msg = GenerateBldrVSwallowMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.BellyRub:
                return GenerateBellyRubMessage(action);
            case MessageLogEvent.BreastRub:
                return GenerateBreastRubMessage(action);
            case MessageLogEvent.TailRub:
                return GenerateTailRubMessage(action);
            case MessageLogEvent.BallMassage:
                return GenerateBallMassageMessage(action);
            case MessageLogEvent.TransferSuccess:
                return GetStoredMessage(StoredLogTexts.MessageTypes.TransferMessages, action);
            case MessageLogEvent.KissTransfer:
                return GetStoredMessage(StoredLogTexts.MessageTypes.KissTransferMessages, action);
            case MessageLogEvent.VoreStealSuccess:
                return GetStoredMessage(StoredLogTexts.MessageTypes.VoreStealMessages, action);
            //return $"<b>{action.Target.Name}</b> gently pushes down <b>{action.Unit.Name}</b> as {GPPHe(action.Target)} straddles {GPPHim(action.Unit)}. As {GPPHe(action.Target)} rides {GPPHim(action.Unit)}, {GPPHe(action.Unit)} cums, shooting {GPPHis(action.Unit)} prey straight into {GPPHis(action.Target)} {action.preyLocation.ToSyn()}.{odds}";
            case MessageLogEvent.TransferFail:
                return $"<b>{action.Unit.Name}</b> is a bit too quick, and {GPPHis(action.Unit)} prey gets partially released.";
            case MessageLogEvent.VoreStealFail:
            //Additional fail lines by Tatltuae
                if (action.Target.Race != Race.Iliijiith && action.Unit.Race == Race.Iliijiith) //Ultra generic Iliijiith VoreSteal Fail line
                return $"<b>{action.Unit.Name}</b> {GetRandomStringFrom("quickly", "rapidly")} approaches <b>{action.Target.Name}</b> and passes through {GPPHim(action.Target)} seemingly doing nothing, although {GPPHe(action.Target)} feels and odd coldness in {GPPHis(action.Target)} body as though something \"tried\" happening.";
                if (action.oldLocation == PreyLocation.bladder) //Generic bladder VoreSteal Fail line
                return $"<b>{action.Unit.Name}</b> {GetRandomStringFrom("squeezes tightly", "presses hard")} on <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> \"belly\" but no matter how hard {GPPHe(action.Unit)} tries the {ApostrophizeWithOrWithoutS(GetRaceDescSingl(action.Target))} {GetRandomStringFrom("steel ", "tough ", "")}bladder refuses to let <b>{action.Prey.Name}</b> go.";
                if (action.oldLocation == PreyLocation.breasts || action.oldLocation == PreyLocation.leftBreast || action.oldLocation == PreyLocation.rightBreast)
                {
                    if (action.Target.Race == Race.Kangaroos)
                        switch (State.Rand.Next(4))
                        {
                            case 0:
                                return $"<b>{action.Unit.Name}</b> attempts to pull open <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> pouch to get at <b>{action.Prey.Name}</b> only to fail as the pouch remains shut {GetRandomStringFrom("tight.", "suprisingly tight.", "tight!", "suprisingly tight!")}";
                            case 1:
                                return $"<b>{action.Unit.Name}</b> forces {GPPHis(action.Unit)} way into <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> already filled pouch, but before {GetRandomStringFrom($"{GPPHe(action.Unit)}", $"the {GetRaceDescSingl(action.Unit)}")} can cause any trouble, <b>{action.Target.Name}</b> {GetRandomStringFrom("pulls", "yanks")} <b>{action.Unit.Name}</b> out and {GetRandomStringFrom("tosses", "throws")} {GPPHim(action.Unit)} aside.";
                            case 2:
                                return $"Running up to the {GetRaceDescSingl(action.Target)}, <b>{action.Unit.Name}</b> sticks {GPPHis(action.Unit)} face into <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> pouch! After a few moments of struggle, <b>{action.Target.Name}</b> manages to push away the intruder.";
                            case 3:
                                return $"Running up to the {GetRaceDescSingl(action.Target)}, <b>{action.Unit.Name}</b> sticks {GPPHis(action.Unit)} face into <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> pouch! After a few moments of struggle, <b>{action.Target.Name}</b> manages to push away the intruder. \"Don't you know it's rude to look in a {ApostrophizeWithOrWithoutS(GetRaceDescSingl(action.Target))} pouch without asking?\" {GPPHe(action.Target)} call{SIfSingular(action.Target)} out, {GetRandomStringFrom("annoyed", "teasingly", "infuriated")}.";
                            default:
                                return $"<b>{action.Unit.Name}</b> attempts to pull open <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> pouch to get at <b>{action.Prey.Name}</b> only to fail as the pouch remains shut {GetRandomStringFrom("tight.", "suprisingly tight.", "tight!", "suprisingly tight!")}";
                        }
                    else
                        switch (State.Rand.Next(3))
                        {
                            case 0:
                                return $"<b>{action.Target.Name}</b> shoves <b>{action.Unit.Name}</b> off of {GPPHim(action.Target)} before {GPPHe(action.Unit)} can suck <b>{action.Prey.Name}</b> out of {GPPHis(action.Target)} breasts.";
                            case 1:
                                return $"<b>{action.Unit.Name}</b> tries to suck on <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> nipple to extract <b>{action.Prey.Name}</b> from within, but the {ApostrophizeWithOrWithoutS((GetRaceDescSingl(action.Prey)))} own struggles made getting the grip needed impossible.";
                            case 2:
                                return $"<b>{action.Target.Name}</b> struggles to fight off <b>{action.Unit.Name}</b> when {GPPHe(action.Unit)} start{SIfSingular(action.Unit)} sucking on {GPPHis(action.Target)} {PreyLocStrings.ToBreastSynPlural()}, but in the end <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> determination to keep <b>{action.Prey.Name}</b> for {GPPHimself(action.Unit)} triumphs over the {ApostrophizeWithOrWithoutS((GetRaceDescSingl(action.Unit)))} attempts to suckle {GPPHim(action.Prey)} out.";
                            default:
                                return $"<b>{action.Target.Name}</b> shoves <b>{action.Unit.Name}</b> off of {GPPHim(action.Target)} before {GPPHe(action.Unit)} can suck <b>{action.Prey.Name}</b> out of {GPPHis(action.Target)} breasts.";
                        }
                }
                else if (action.oldLocation == PreyLocation.stomach || action.oldLocation == PreyLocation.stomach2)
                {
                    if (action.Target.Race == Race.Iliijiith && action.Unit.Race != Race.Iliijiith)
                    return $"<b>{action.Unit.Name}</b> charges at <b>{action.Target.Name}</b> and bashes into it at full force however bounces off of its hard outer body{GetRandomStringFrom(".", ". \'Owww...\'", $". Although <b>{action.Target.Name}</b> doesn't have a face <b>{action.Unit.Name}</b> cannot help but feel like the Iliijiith is glareing at {GPPHim(action.Unit)}...")}";
                    else if (action.Target.Race == Race.Iliijiith && action.Unit.Race == Race.Iliijiith)
                    return $"<b>{action.Unit.Name}</b> {GetRandomStringFrom("quickly", "rapidly")} approaches <b>{action.Target.Name}</b> and merges with the other Iliijiith. After several movements of the two exomatter beings seemingly shifing in size, the two Iliijiith seperate, both appearing briefly slighty cracked, although still the original size they were before merging.";
                    else
                    switch (State.Rand.Next(4))
                    {
                        case 0:
                            if (action.Target.Race == Race.Aabayx && ActorHumanoid(action.Unit))
                                return $"<b>{action.Unit.Name}</b> carefully pries <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> triangular face open, before foolishly trying to reach in to pull out <b>{action.Prey.Name}</b>. In an instant, the Aabayx's face plates snap shut on {GPPHis(action.Unit)} arm, causing {GPPHim(action.Unit)} to yank it back in pain.";
                            else
                                return $"<b>{action.Unit.Name}</b> {GetRandomStringFrom("tackles", "headbutts", "charges into", "bashes")} <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> {GetRandomStringFrom("filled", "bulbus", "exposed")} belly, but <b>{action.Target.Name}</b> refuses to let go of <b>{action.Prey.Name}</b> that easily.";
                        case 1:
                            if (action.Target.Race == Race.Aabayx)
                                return $"<b>{action.Unit.Name}</b> {(ActorHumanoid(action.Unit) ? "punches" : "hits")} <b>{action.Target.Name}</b> in {GPPHis(action.Target)} singular eye, recoiling at the surprisingly hard material of the Aabayx's icosahedral head.";
                            else
                                return $"<b>{action.Unit.Name}</b> {GetRandomStringFrom("tackles", "headbutts", "charges into", "bashes")} <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> {GetRandomStringFrom("filled", "bulbus", "exposed")} belly, but <b>{action.Target.Name}</b> refuses to let go of <b>{action.Prey.Name}</b> that easily.";
                        case 2:
                            if (action.Target.Race == Race.Aabayx)
                                return $"<b>{action.Unit.Name}</b> {GetRandomStringFrom("tackles", "headbutts", "charges into", "bashes")} <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> {GetRandomStringFrom("filled", "bulbus", "exposed")} belly. A dull thud can be heard inside <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> icosahedral head as <b>{action.Prey.Name}</b> hits the inside before sliding back down into the Aabayx's {PreyLocStrings.ToSyn(PreyLocation.stomach)}.";
                            else
                                return $"<b>{action.Unit.Name}</b> rushes over and places {GPPHis(action.Unit)} lips firmly against <b>{action.Target.Name}</b>' {GetRandomStringFrom("lips", "mouth")}, but before {GPPHe(action.Unit)} can coax the {ApostrophizeWithOrWithoutS((GetRaceDescSingl(action.Target)))} stomach into giving up <b>{action.Prey.Name}</b>, <b>{action.Target.Name}</b> breaks off the kiss and pushes <b>{action.Unit.Name}</b> away.";
                        case 3:
                            if (action.Target.Race == Race.Aabayx)
                                return $"<b>{action.Unit.Name}</b> {GetRandomStringFrom("tackles", "headbutts", "charges into", "bashes")} <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> {GetRandomStringFrom("filled", "bulbus", "exposed")} belly. A dull thud can be heard inside <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> icosahedral head as <b>{action.Prey.Name}</b> hits the inside before sliding back down into the Aabayx's {PreyLocStrings.ToSyn(PreyLocation.stomach)}.";
                            else if (Config.BurpFraction > .1f)
                                return $"<b>{action.Unit.Name}</b> wraps {GPPHis(action.Unit)} arms around <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> body, pressing against {GPPHim(action.Target)} and crushing against {GPPHis(action.Target)} prey filled {PreyLocStrings.ToSyn(PreyLocation.stomach)}. Before long, <b>{action.Target.Name}</b> releases a loud burp straight in <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> face, forcing {GPPHim(action.Unit)} to let go and {GetRandomStringFrom("retreat.", "back away.")}";
                            else
                                return $"<b>{action.Unit.Name}</b> rushes over and places {GPPHis(action.Unit)} lips firmly against <b>{action.Target.Name}</b>' {GetRandomStringFrom("lips", "mouth")}, but before {GPPHe(action.Unit)} can coax the {ApostrophizeWithOrWithoutS((GetRaceDescSingl(action.Target)))} stomach into giving up <b>{action.Prey.Name}</b>, <b>{action.Target.Name}</b> breaks off the kiss and pushes <b>{action.Unit.Name}</b> away.";
                        default:
                            return $"<b>{action.Unit.Name}</b> {GetRandomStringFrom("tackles", "headbutts", "charges into", "bashes")} <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> {GetRandomStringFrom("filled", "bulbus", "exposed")} belly, but <b>{action.Target.Name}</b> refuses to let go of <b>{action.Prey.Name}</b> that easily.";
                    }
                }
                else if (action.oldLocation == PreyLocation.womb)
                switch (State.Rand.Next(4))
                {
                    case 0:
                        return $"<b>{action.Target.Name}</b> shoves <b>{action.Unit.Name}</b> off of {GPPHim(action.Target)} before {GPPHe(action.Unit)} can {GetRandomStringFrom("free", "liberate", "spring")} <b>{action.Prey.Name}</b> from {GPPHis(action.Target)} vagina.";
                    case 1:
                        return $"<b>{action.Unit.Name}</b> rams {GPPHis(action.Unit)} head up <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}, and tries to swallow <b>{action.Prey.Name}</b> but has to pull out early, lest {GPPHe(action.Unit)} get pulled in alongside the {GetRaceDescSingl(action.Prey)}.";
                    case 2:
                        if (Config.LewdDialog)
                            return $"Crouching down below <b>{action.Target.Name}</b>, <b>{action.Unit.Name}</b> ruthlessly licks all over {GPPHis(action.Target)} {PreyLocStrings.ToSyn(PreyLocation.womb)}! Not wanting to risk losing <b>{action.Prey.Name}</b>, <b>{action.Target.Name}</b>, with sadness in {GPPHis(action.Target)} eyes, pushes <b>{action.Unit.Name}</b> away from {GPPHim(action.Target)}.";
                        else
                            return $"<b>{action.Unit.Name}</b> rams {GPPHis(action.Unit)} head up <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}, and tries to swallow <b>{action.Prey.Name}</b> but has to pull out early, lest {GPPHe(action.Unit)} get pulled in alongside the {GetRaceDescSingl(action.Prey)}.";
                    case 3:
                        if (ActorHumanoid(action.Unit) && ActorHumanoid(action.Target) && Config.LewdDialog)
                            return $"<b>{action.Unit.Name}</b> reaches {GPPHis(action.Unit)} hand up into <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.womb)}, trying to grab <b>{action.Prey.Name}</b>. Feeling something, {GPPHe(action.Target)} pull{SIfSingular(action.Unit)} out, only to be holding what appears to be a dildo. Seeing this, <b>{action.Target.Name}</b> exclaims \"{GetRandomStringFrom("Hey, that's where that one went", "Wow, that one's been in there a while", "Never thought I'd see that one again", "Hey! Put that back in")}!\"";
                        else
                            return $"<b>{action.Target.Name}</b> shoves <b>{action.Unit.Name}</b> off of {GPPHim(action.Target)} before {GPPHe(action.Unit)} can {GetRandomStringFrom("free", "liberate", "spring")} <b>{action.Prey.Name}</b> from {GPPHis(action.Target)} vagina.";
                    default:
                        return $"<b>{action.Target.Name}</b> shoves <b>{action.Unit.Name}</b> off of {GPPHim(action.Target)} before {GPPHe(action.Unit)} can {GetRandomStringFrom("free", "liberate", "spring")} <b>{action.Prey.Name}</b> from {GPPHis(action.Target)} vagina.";
                }
                else if (action.oldLocation == PreyLocation.tail)
                {
                    if (action.Target.Race == Race.Terrorbird)
                        switch (State.Rand.Next(3))
                        {
                            case 0:
                                return $"<b>{action.Unit.Name}</b> {GetRandomStringFrom("headbutts", "bashes")} <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> crop attempting to release <b>{action.Prey.Name}</b>. After failing <b>{action.Target.Name}</b> simply stares {GetRandomStringFrom("soul-piercingly", "fearlessly", "daggers", "intimidatingly", "blanky")} with {GPPHis(action.Target)} eyes at <b>{action.Unit.Name}</b>.";
                            case 1:
                                return $"<b>{action.Unit.Name}</b> tries to bait <b>{action.Target.Name}</b> into trying to {GetRandomStringFrom("swallow", "eat")} {GPPHim(action.Unit)}, hoping to take <b>{action.Prey.Name}</b> for {GPPHimself(action.Unit)}, but the {GetRaceDescSingl(action.Target)} doesn't fall for it.";
                            case 2:
                                return $"After <b>{action.Unit.Name}</b> spends a few moments of pushing up against <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> bulging crop, it becomes apparent that the only direction <b>{action.Prey.Name}</b> is likely to go from here is down.";
                            default:
                                return $"<b>{action.Unit.Name}</b> {GetRandomStringFrom("headbutts", "bashes")} <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> crop attempting to release <b>{action.Prey.Name}</b>. After failing <b>{action.Target.Name}</b> simply stares {GetRandomStringFrom("soul-piercingly", "fearlessly", "daggers", "intimidatingly", "blanky")} with {GPPHis(action.Target)} eyes at <b>{action.Unit.Name}</b>.";
                        }
                    else
                        switch (State.Rand.Next(4))
                        {
                            case 0:
                                if (action.Target.Race == Race.Youko && ActorHumanoid(action.Unit))
                                    return $"<b>{action.Unit.Name}</b> sticks {GPPHis(action.Unit)} arm between the many fluffy tails of <b>{action.Target.Name}</b>, and even manages to get a grip on <b>{action.Prey.Name}</b>, but no matter how hard {GPPHe(action.Unit)} pull{SIfSingular(action.Unit)}, <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> tails hold firm.";
                                else if (action.Target.Race == Race.Youko)
                                    return $"<b>{action.Unit.Name}</b> {GetRandomStringFrom("punches", "kicks")} <b>{action.Target.Name}</b> hard, right where {GPPHis(action.Target)} tails all attach, causing them all to shift around, briefly exposing <b>{action.Prey.Name}</b> to the outside world once more, before resealing.";
                                else
                                    return $"<b>{action.Unit.Name}</b> grabs <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> tail and squeezes it like a tube of toothpaste, causing <b>{ApostrophizeWithOrWithoutS(action.Prey.Name)}</b> head to briefly poke out of the {ApostrophizeWithOrWithoutS((GetRaceDescSingl(action.Target)))} tailmaw, letting {GPPHim(action.Prey)} breath for a moment before <b>{action.Target.Name}</b> shakes <b>{action.Unit.Name}</b> off and pulls <b>{action.Prey.Name}</b> back inside.";
                            case 1:
                                if (action.Target.Race == Race.Youko)
                                    return $"<b>{action.Unit.Name}</b> {GetRandomStringFrom("punches", "kicks")} <b>{action.Target.Name}</b> hard, right where {GPPHis(action.Target)} tails all attach, causing them all to shift around, briefly exposing <b>{action.Prey.Name}</b> to the outside world once more, before resealing.";
                                else
                                    return $"<b>{action.Unit.Name}</b> slowly wraps {GPPHis(action.Unit)} mouth around <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> {GetRandomStringFrom("engorged", "swollen", "prey-filled")} tail, before a poorly(or well timed) struggle from both <b>{action.Prey.Name}</b> and <b>{action.Target.Name}</b> forces the {GetRaceDescSingl(action.Unit)} off of {GPPHis(action.Target)} tail.";
                            case 2:
                                if (action.Target.Race == Race.Youko)
                                    return $"<b>{action.Unit.Name}</b> sticks {GPPHis(action.Unit)} face between the many fluffy tails of <b>{action.Target.Name}</b>, but after a short few moments, has to pull out, the smothering tails making it hard to see or breathe.";
                                else
                                    return $"<b>{action.Unit.Name}</b> forces open <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> tailmaw, and sticks {GPPHis(action.Unit)} head in. After desperatly searching for a few moments, <b>{action.Unit.Name}</b> can feel contractions around {GPPHim(action.Unit)}, letting {GPPHim(action.Unit)} know it's time to get out of there.";
                            case 3:
                                if (action.Target.Race == Race.Youko && (State.Rand.Next(20)) == 1 && ActorHumanoid(action.Prey)) //5% chance of trickery!
                                    return $"<b>{action.Unit.Name}</b> sticks {GPPHis(action.Unit)} face between the many fluffy tails of <b>{action.Target.Name}</b>. Looking around, <b>{action.Unit.Name}</b> is amazed to see what looks to be a whole forest of Youko tails and fur, with <b>{action.Prey.Name}</b> casually sitting in the middle. With a wave, <b>{action.Prey.Name}</b> says \"<b>{action.Unit.Name}</b> yeh! Uoy era woh? Em nioj ot emoc? Ereh ni ecin etiuq s'ti.\" Slowly, <b>{action.Unit.Name}</b> pulls {GPPHis(action.Unit)} face out of <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> {GetRandomStringFrom("tails.", "tails, shuddering.", "tails, traumatized.", "tails, silently vowing to never speak of what they saw.")}";
                                else if (action.Target.Race == Race.Youko)
                                    return $"<b>{action.Unit.Name}</b> sticks {GPPHis(action.Unit)} face between the many fluffy tails of <b>{action.Target.Name}</b>, but after a short few moments, has to pull out, the smothering tails making it hard to see or breathe.";
                                else
                                    return $"<b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> tail shakes <b>{action.Unit.Name}</b> off before {GPPHe(action.Unit)} can {GetRandomStringFrom("free", "liberate")} <b>{action.Prey.Name}</b>.";
                            default:
                                return $"<b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> tail shakes <b>{action.Unit.Name}</b> off before {GPPHe(action.Unit)} can {GetRandomStringFrom("free", "liberate")} <b>{action.Prey.Name}</b>.";
                        }
                }
                else
                switch (State.Rand.Next(3))
                {
                    case 0:
                        return $"<b>{action.Target.Name}</b> shoves <b>{action.Unit.Name}</b> off of {GPPHim(action.Target)} before {GPPHe(action.Unit)} can suck <b>{action.Prey.Name}</b> out of {GPPHis(action.Target)} balls.";
                    case 1:
                        return $"<b>{action.Unit.Name}</b> sucks on <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> {PreyLocStrings.ToCockSyn()} for a bit until {GPPHe(action.Target)} cum{SIfSingular(action.Target)}, but much to <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> disappointment, <b>{action.Prey.Name}</b> remains locked in <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> {PreyLocStrings.ToSyn(PreyLocation.balls)}.";
                    case 2:
                        return $"<b>{action.Unit.Name}</b> knocks down <b>{action.Target.Name}</b> and begins sucking {GPPHis(action.Target)} rod, but even as the {GetRaceDescSingl(action.Target)} cums, {GPPHis(action.Target)} {PreyLocStrings.ToSyn(PreyLocation.balls)} hold tight to their prize; <b>{action.Prey.Name}</b>.";
                    default:
                        return $"<b>{action.Target.Name}</b> shoves <b>{action.Unit.Name}</b> off of {GPPHim(action.Target)} before {GPPHe(action.Unit)} can suck <b>{action.Prey.Name}</b> out of {GPPHis(action.Target)} balls.";
                }
            case MessageLogEvent.Feed:
                return GetStoredMessage(StoredLogTexts.MessageTypes.BreastFeedMessages, action);
            case MessageLogEvent.FeedCum:
                return GetStoredMessage(StoredLogTexts.MessageTypes.CumFeedMessages, action);
            case MessageLogEvent.Suckle:
                if (action.preyLocation == PreyLocation.breasts || action.preyLocation == PreyLocation.leftBreast || action.preyLocation == PreyLocation.rightBreast)
                    if (action.Unit == action.Target)
                        return $"<b>{action.Unit.Name}</b> {GetRandomStringFrom("grabs", "grips", "grasps")} {GPPHis(action.Target) + " own"} {GetRandomStringFrom("tits", "breasts")} and starts caringly sucking on them.{odds}";
                    else
                        if (State.Rand.Next(2) == 0)
                            return $"<b>{action.Unit.Name}</b> pins <b>{action.Target.Name}</b>, to the ground as {GPPHe(action.Unit)} starts sucking on {GPPHis(action.Target)} {GetRandomStringFrom("tits", "breasts")}!{odds}";
                        else
                            return $"<b>{action.Unit.Name}</b> hugs <b>{action.Target.Name}</b>, pinning {GPPHis(action.Target)} arms to {GPPHis(action.Target)} sides as {GPPHe(action.Unit)} starts sucking on {GPPHis(action.Target)} {GetRandomStringFrom("tits", "breasts")}!{odds}";
                else
                    if (action.Unit == action.Target)
                        return $"<b>{action.Unit.Name}</b> gets down and skillfully begins {GetRandomStringFrom("servicing", "sucking", "fellating")} {GPPHis(action.Target)} {GetRandomStringFrom("shaft", "rod", "dick", "rod", "dick")}.{odds}";
                    else
                        if (State.Rand.Next(2) == 0)
                            return $"<b>{action.Unit.Name}</b> catches <b>{action.Target.Name}</b> off guard and aggressively begins sucking {GPPHis(action.Target)} {GetRandomStringFrom("shaft", "rod", "dick", "rod", "dick")}.{odds}";
                        else
                            return $"<b>{action.Unit.Name}</b> knocks down <b>{action.Target.Name}</b> and begins sucking {GPPHis(action.Target)} {GetRandomStringFrom("shaft", "rod", "dick", "rod", "dick")}.{odds}";
            case MessageLogEvent.SuckleFail:
                if (action.preyLocation == PreyLocation.breasts || action.preyLocation == PreyLocation.leftBreast || action.preyLocation == PreyLocation.rightBreast)
                    return $"<b>{action.Unit.Name}</b> hugs <b>{action.Target.Name}</b>, but {GPPHe(action.Target)} breaks free from {GPPHis(action.Unit)} hold before {action.Unit.Name} can do anything!{odds}";
                else
                    return $"<b>{action.Unit.Name}</b> tries to knock down <b>{action.Target.Name}</b>, but {action.Target.Name} stands {GPPHis(action.Target)} ground!{odds}";
            case MessageLogEvent.Birth:
                msg = GenerateBirthMessage(action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.Resist:
                return $"<b>{action.Unit.Name}</b> tried to vore <b>{action.Target.Name}</b>, but was fought off.{odds}";
            case MessageLogEvent.Kill:
                return GenerateKillMessage(action);
            case MessageLogEvent.Digest:
                return GenerateDigestionDeathMessage(action);
            case MessageLogEvent.Absorb:
                return GenerateAbsorptionMessage(action);
            case MessageLogEvent.Escape:
                return GenerateEscapeMessage(action, odds);
            case MessageLogEvent.PartialEscape:
                return $"<b>{action.Target.Name}</b> escaped from <b>{action.Unit.Name}</b>'s second stomach, only to find {GPPHimself(action.Target)} back in the first stomach.{odds}";
            case MessageLogEvent.Freed:
                return (action.Unit.IsDead ? $"<b>{action.Target.Name}</b> was freed because <b>{action.Unit.Name}</b> died." : $"<b>{action.Target.Name}</b> was freed because <b>{action.Unit.Name}</b> surrendered.");
            //$"<b>{action.Target.Name}</b> sees insides of {action.preyLocation.ToSyn()} around him melting, only to find {GPPHimself(action.Target)} <b>{action.Unit.Name}</b>'s {action.preyLocation.ToSyn()}{odds}"
            case MessageLogEvent.Regurgitated:
                return $"<b>{action.Unit.Name}</b> hears {GPPHis(action.Unit)} comrade's plea for help and releases <b>{action.Target.Name}</b>.";
            case MessageLogEvent.Heal:
                if (new[] { "breastfeeding", "cumfeeding" }.Contains(action.Message))
                {
                    string message = "";
                    if (action.Damage != 0 && action.Bonus == 0)
                        message = $"<b>{action.Unit.Name}</b> <color=blue>healed {action.Damage}</color> from the milk.";
                    else if (action.Damage != 0 && action.Bonus != 0)
                        message = $"<b>{action.Unit.Name}</b> <color=blue>healed {action.Damage}</color> and <color=blue>gained {action.Bonus} experience</color> from the milk.";
                    else
                        message = $"<b>{action.Unit.Name}</b> <color=blue>gained {action.Bonus} experience</color> from the milk.";
                    if (action.Extra == "honey")
                        message = message.Replace("milk", "honey");
                    else if (action.Message == "cumfeeding")
                        message = message.Replace("milk", "cum");
                    return message;
                }
                return $"<b>{action.Unit.Name}</b> <color=blue>healed {action.Damage}</color> from absorbing {GPPHis(action.Unit)} prey.";
            case MessageLogEvent.NewTurn:
                return action.Message;
            case MessageLogEvent.LowHealth:
                return GenerateDigestionLowHealthMessage(action);
            case MessageLogEvent.Miscellaneous:
                return action.Message;
            case MessageLogEvent.RandomDigestion:
                return GenerateRandomDigestionMessage(action);
            case MessageLogEvent.Dazzle:
                return $"<b>{action.Unit.Name}</b> was dazzled by <b>{action.Target.Name}</b>, the distraction wasting {GPPHis(action.Unit)} turn.{odds}";
            case MessageLogEvent.Block:
                return $"<b>{action.Target.Name}</b> blocked <b>{action.Unit.Name}'s</b> {GetWeaponTrueName(action.Weapon, action.Unit)}, only taking <color=red>{action.Damage}</color> points of damage.{odds}";
            case MessageLogEvent.SpellHit:
                msg = GenerateSpellHitMessage((SpellLog)action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.SpellMiss:
                msg = GenerateSpellMissMessage((SpellLog)action);
                msg = msg += odds;
                return msg;
            case MessageLogEvent.SpellKill:
                string spellName = SpellList.SpellDict[((SpellLog)action).SpellType].Name;
                return $"<b>{action.Unit.Name}</b> killed <b>{action.Target.Name}</b> with the {spellName} spell.";
            case MessageLogEvent.CurseExpires:
                return GenerateCurseExpiringMessage(action);
            case MessageLogEvent.DiminishmentExpires:
                return GenerateDiminshmentExpiringMessage(action);
            case MessageLogEvent.GreatEscapeKeep:
                return GenerateGreatEscapeKeepMessage(action);
            case MessageLogEvent.GreatEscapeFlee:
                return GenerateGreatEscapeFleeMessage(action);
            case MessageLogEvent.ManualRegurgitation:
                return GenerateRegurgitationMessage(action);
                // return $"<b>{action.Unit.Name}</b> triggers my test message by regurgitating <b>{action.Target.Name}</b>.";
            default:
                return string.Empty;
        }
    }
    private string GenerateBreastRubMessage(EventLog action)
    {
        if (SimpleText)
            return $"<b>{action.Unit.Name}</b> massages {(action.Unit == action.Target ? GPPHis(action.Target) : "<b>" + action.Target.Name + "</b>'s")} full breasts.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.BreastRubMessages, action);
    }

    private string GenerateTailRubMessage(EventLog action)
    {
        if (SimpleText)
        {
            if (action.Unit.Race == Race.Terrorbird)
                return $"<b>{action.Unit.Name}</b> massages {(action.Unit == action.Target ? GPPHis(action.Target) : "<b>" + action.Target.Name + "</b>'s")} filled crop.";
            else
                return $"<b>{action.Unit.Name}</b> massages {(action.Unit == action.Target ? GPPHis(action.Target) : "<b>" + action.Target.Name + "</b>'s")} stuffed tail.";
        }
        return GetStoredMessage(StoredLogTexts.MessageTypes.TailRubMessages, action);
    }


    private string GenerateBallMassageMessage(EventLog action)
    {
        if (SimpleText)
            return $"<b>{action.Unit.Name}</b> massages {(action.Unit == action.Target ? GPPHis(action.Target) : "<b>" + action.Target.Name + "</b>'s")} full scrotum.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.BallMassageMessages, action);
    }

    string GenerateSpellHitMessage(SpellLog action)
    {
        var spell = SpellList.SpellDict[action.SpellType];
        switch (action.SpellType)
        {
            case SpellTypes.Shield:
                return $"<b>{action.Unit.Name}</b> buffed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            //$"<b>{action.Target.Name}</b> feels <b>{action.Unit.Name}</b>'s protective spell enveloping {GPPHim(action.Unit)}"
            case SpellTypes.Mending:
                return $"<b>{action.Unit.Name}</b> buffed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellTypes.Speed:
                return $"<b>{action.Unit.Name}</b> buffed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellTypes.Valor:
                return $"<b>{action.Unit.Name}</b> buffed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellTypes.Predation:
                return $"<b>{action.Unit.Name}</b> buffed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellTypes.Poison:
                return $"<b>{action.Unit.Name}</b> inflicted <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellTypes.PreysCurse:
                return $"<b>{action.Unit.Name}</b> debuffed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellTypes.Maw:
                return $"<b>{action.Unit.Name}</b> consumed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellTypes.Diminishment:
                return $"<b>{action.Unit.Name}</b> shrunk <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellTypes.GateMaw:
                return $"<b>{action.Unit.Name}</b> consumed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellTypes.Enlarge:
                return $"<b>{action.Unit.Name}</b> enlarged <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            case SpellTypes.Resurrection:
                return $"<b>{action.Unit.Name}</b> resurrected <b>{action.Target.Name}</b>.";
            case SpellTypes.AlraunePuff:
                return $"<b>{action.Unit.Name}</b>'s pollen cloud affected <b>{action.Target.Name}</b>.";
            case SpellTypes.ViperPoison:
                return $"<b>{action.Unit.Name}</b>'s poison spot poisoned <b>{action.Target.Name}</b>.";
            case SpellTypes.Web:
                return $"<b>{action.Unit.Name}</b> webbed <b>{action.Target.Name}</b>.";
            case SpellTypes.GlueBomb:
                return $"<b>{action.Unit.Name}</b>'s glue bomb affected <b>{action.Target.Name}</b>.";
            case SpellTypes.Petrify:
                return $"<b>{action.Unit.Name}</b> petrified <b>{action.Target.Name}</b>.";
            case SpellTypes.DivinitysEmbrace:
                return $"<b>{action.Unit.Name}</b> buffed <b>{action.Target.Name}</b> with the {spell.Name} spell.";
            default:
                return $"<b>{action.Unit.Name}</b> hit <b>{action.Target.Name}</b> with the {spell.Name} spell, dealing <color=red>{action.Damage}</color> damage.";
        }
    }

    string GenerateSpellMissMessage(SpellLog action)
    {
        if (action.SpellType == SpellTypes.None)
            return "";
        var spell = SpellList.SpellDict[action.SpellType];
        return $"<b>{action.Unit.Name}</b> failed to affect <b>{action.Target.Name}</b> with the {spell.Name} spell.";
    }

    private string GenerateMissMessage(EventLog action)
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                return $"<b>{action.Unit.Name}</b> missed <b>{action.Target.Name}</b> with {GPPHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}.";
            case 1:
                {
                    if (action.Weapon.Range > 1) return $"<b>{action.Unit.Name}</b> took a shot at <b>{action.Target.Name}</b> with {GPPHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}, but missed.";
                    else return $"<b>{action.Unit.Name}</b> struck at <b>{action.Target.Name}</b> with {GPPHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}, but went wide.";
                }
            default:
                return $"<b>{action.Target.Name}</b> dodged <b>{action.Unit.Name}</b>'s attempted attack with {GPPHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}.";
        }
    }

    /// <summary>
    /// Generates a message for the tactical log when a unit dies through damage from a weapon.
    /// </summary>
    private string GenerateKillMessage(EventLog action)
    {
        List<string> possibleLines = new List<string>();
        if (action.Unit.Race == Race.Firefly)
        {
            possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Delta one, one target down! I repeat- Oh wait... forgot about that...\"");
            possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Tell the devil of this universe that <b>{action.Unit.Name}</b> sent ya! Does this universe have a devil?\"");
            possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Gah! Bleh! Got blood on my face! I better not get some stupid infection from this!\"");
            possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Man, if I had my Wanzer right now. Oh wait... no one here knows what that is.\"");
            possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Heh. Arrow would have a ball if he was here right now... Dang it, now I'm depressed again.\"");
            possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"How does the ground taste?!\"");
            possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"The day I get home, I am so going to tell everyone about all this cool stuff I killed! No one would care or beleive me but it would be worth a shot!\"");
            possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"If you dare say 'That's gotta hurt!' I'll kill you! ... I'm talking to myself again...\"");
            if (Config.FourthWallBreakType == FourthWallBreakType.On || Config.FourthWallBreakType == FourthWallBreakType.FriendlyOnly)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"You tried. And you died. #$*&ing dumb- ... %*^#! %&@! %*#^! *%#*$! What?! I can't swear?! What #&@#$%&# is this?!\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Bet you are expecting some taunt or joke here. I know you are there... <b>player</b>.\"");
            }
            if (action.Target.Race == Race.Cats)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Your claws may be sharp. But mine are sharper!\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Aww, you don't look so happy. What's wrong? Cat got your tongue? HAH! ... Anyone? Come on it was funny in my head!\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Looks like the fox wins again! God, you suck at this.\"");
            }
            if (action.Target.Race == Race.Dogs)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Man, this is a canine eat canine world! Literally.\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Too much yapping and not enough fighting. Dogs are annoying no matter where I go.\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"And that's what you get for barking all night!\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Hey! Good news <b>{action.Target.Name}</b>! You don't have to go to the vet anymore! Because you are dead!\"");
            }
            if (action.Target.Race == Race.Foxes || action.Target.Race == Race.FeralFox)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Fox on fox violence! Why did I say that? Sounded coolor in my head.\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"You may be a fox. But I am a fire fox! ... Don't stare at me with your lifeless eyes!\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"It's illegal to hunt foxes where I'm from. Too bad this isn't my universe!\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Go back to your den little fox! I'm the big fox here!\"");
            }
            if (action.Target.Race == Race.Wolves || action.Target.Race == Race.FeralWolves)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Bah! There! Now quit your howling! I need to get sleep too you know!\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Seems the fox out maneuvered the wolf this time!\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Hah! You couldn't even blow my house down!\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"I'm not even going to tell you to shut up.\"");
            }
            if (action.Target.Race == Race.Bunnies)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Hop around <i>that</i>, you little... runt.\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"You are fast. I am faster! You are bunny. I am bunnier!... I think I'm drunk again.\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Another <i>bunny</i> bites the <i>dust</i>! I need a joke book...\"");
            }
            if (action.Target.Race == Race.Humans)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Great. Humans even plage this universe.\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"I am very surpised to see you aren't the dominant race here.\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Looks like your armor didn't stop that! Should have invented guns again!\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"How's that for domination?! I am kinda starting to like it here.\"");
            }
            if (action.Target.Race == Race.Umbreon)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Sorry friend. War is war. I'm just following orders...\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Why... Why must this happen again...\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"I didn't sign up for this...\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"This is the arena fights all over again...\"");
            }
            if (action.Target.Race == Race.Selicia)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Sad to see ya go. I kinda liked you.\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"I would spare you. But a job is a job. Nothing personal I swear.\"");
            }
            if (action.Target.Race == Race.Vision)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Well that was terrfing. Please stay dead.\"");
                if (Config.FourthWallBreakType == FourthWallBreakType.On || Config.FourthWallBreakType == FourthWallBreakType.FriendlyOnly) possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"How the hell did that thing even see... Wait. I can say 'hell' but not '%#$&'?! AUGH!\"");
            }
            if (action.Target.Race == Race.Ki)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Writing that down. 'No matter how small something is. It can still kill you.'\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Aww, you were kinda cute. Probably deadly, but still cute.\"");
            }
            if (action.Target.Race == Race.Scorch)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Is my tail on fire?! No? Whew...\"");
            }
            if (action.Target.Race == Race.Asura)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"You are a scarlet user aren't you. No wait... Forgot... Not my univesrse.\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"I am both in amazement and fear right now.\"");
            }
            if (action.Target.Race == Race.DRACO)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Hey you are like the KNines from my universe! But a dragon with a mouth! And huge!\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Draco The Robotic Dragon... Real original.\"");
            }
            if (action.Target.Race == Race.Zoey)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"What is so special about this shark?! All they do is swing their tail at people... and not wear clothes!\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Go back to the ocean! You- uh... uhhh... fish? ... This place is making me more stupid.\"");
            }
            if (action.Target.Race == Race.Cierihaka)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"That was a big ^%# dragon!\"");
                if (Config.FourthWallBreakType == FourthWallBreakType.On || Config.FourthWallBreakType == FourthWallBreakType.FriendlyOnly) possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"I just killed the dragon equilvent of a Dark Souls boss...\"");
            }
            if (action.Target.Race == Race.Zera)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"How many dragons are there?!\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"I AM THE DRAGON SLAYER! HAHAA!\"");
            }
            if (action.Target.Race == Race.Auri)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Is that a costume or are those ears and tail real? I hope they are real...\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Did you just try to kill me with a glorified stick? Well now I've seen everything.\"");
            }
            if (action.Target.Race == Race.Erin)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"I don't know if you are a cat, angel, or demigod. or all three.\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Hey someone else who doesn't use the strange power of this world!\"");
            }
            if (action.Target.Race == Race.Salix)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Hey look it's a magic mouse! That was a rhyme not a joke...\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"You do know foxes hunt mice right? This was never going to go your way.\"");
            }
            if (action.Target.Race == Race.Abakhanskya)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Ok time out for a second. I don't mean to fat shame you are anything, But you need to take a diet. I am genuinely concerned for your health... Even though I just killed you.\"");
                if (Config.FourthWallBreakType == FourthWallBreakType.On || Config.FourthWallBreakType == FourthWallBreakType.FriendlyOnly) possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Now THAT is some Attack On Titan shit right there.\"");
            }
            if (action.Target.Race == Race.Singularity)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Are...you half human half deer half taur? Never mind. I havn't seen everything.\"");
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"I don't see the use of a sweater. Plus isn't that hard to put on and get off with the horns? Or do you just never...\"");
            }
            if (action.Target.Race == Race.Feit)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Hey you looked cool. We should talk later at the merc camp.\"");
                if (Config.FourthWallBreakType == FourthWallBreakType.On || Config.FourthWallBreakType == FourthWallBreakType.FriendlyOnly) possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("killed", "struck down", "finished off", "dealt with")} <b>{action.Target.Name}</b> with his {GetWeaponTrueName(action.Weapon, action.Unit)}, \"Fluffy raptor dragon with feathers. Adding that to my Christmas wishlist!\"");
            }
        }
        if (action.Weapon.Range > 1) possibleLines.Add($"<b>{action.Target.Name}</b> was struck down by an accurate hit of <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> {GetWeaponTrueName(action.Weapon, action.Unit)}.");
        else possibleLines.Add($"<b>{action.Unit.Name}</b> struck <b>{action.Target.Name}</b> down with a skilled strike of {GPPHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}.");
        possibleLines.Add($"<b>{action.Target.Name}</b> was slain by <b>{action.Unit.Name}</b> wielding {GPPHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}.");
        possibleLines.Add($"<b>{action.Unit.Name}</b> put an end to <b>{action.Target.Name}</b> with {GPPHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}.");
        possibleLines.Add($"<b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> fight was brought to an end by <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> {GetWeaponTrueName(action.Weapon, action.Unit)}.");
        possibleLines.Add($"<b>{action.Target.Name}</b> was brought down by <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> {GetWeaponTrueName(action.Weapon, action.Unit)}.");
        possibleLines.Add($"<b>{action.Unit.Name}</b> killed <b>{action.Target.Name}</b> with {GPPHis(action.Unit)} {GetWeaponTrueName(action.Weapon, action.Unit)}.");

        return GetRandomStringFrom(possibleLines.ToArray());
    }

    private string GenerateSwallowMessage(EventLog action)  // Oral vore devouring messages.
    {
        if (SimpleText)
            return $"<b>{action.Unit.Name}</b> ate <b>{action.Target.Name}</b>.";

        return GetStoredMessage(StoredLogTexts.MessageTypes.SwallowMessages, action);


    }

    private string GenerateBVSwallowMessage(EventLog action)
    {
        if (SimpleText)
            return $"<b>{action.Unit.Name}</b> breast vores <b>{action.Target.Name}</b>.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.BreastVoreMessages, action);
    }

    string GenerateCurseExpiringMessage(EventLog action)
    {
        if (action.preyLocation == PreyLocation.stomach || action.preyLocation == PreyLocation.stomach2)
        {
            return GetRandomStringFrom(
                $"As the curse comes to an end, <b>{action.Target.Name}</b> tries and figures out where {GPPHeIs(action.Target)} and begins to scream in horror as {GPPHe(action.Target)} realize{SIfSingular(action.Target)} what’s happened.",
                $"<b>{action.Unit.Name}</b> begins to worry as the curse on {GPPHis(action.Unit)} meal wears off. Surprisingly though, <b>{action.Target.Name}</b> continues to massage {GPPHis(action.Unit)} belly walls with enthusiasm.",
                $"<b>{action.Unit.Name}</b>’s tummy goes from a smooth, gentle surface to a sudden mass of angry rippling as {GPPHis(action.Unit)} previously willing prey realizes {GPPHe(action.Target)} {HasHave(action.Target)} been tricked.",
                $"<b>{action.Target.Name}</b> confusedly asks where {GPPHeIs(action.Target)} as the spell breaks. <b>{action.Unit.Name}</b> tells {GPPHim(action.Target)} {GPPHeIsAbbr(action.Target)} exactly where {GPPHeIsAbbr(action.Target)} meant to be as {GPPHe(action.Unit)} lovingly embraces {GPPHis(action.Unit)} swollen stomach."
                );
        }
        else
        {
            return GetRandomStringFrom(
                $"As the curse comes to an end, <b>{action.Target.Name}</b> tries and figures out where {GPPHeIs(action.Target)} and begins to scream in horror as {GPPHe(action.Target)} realize{SIfSingular(action.Target)} what’s happened.",
                $"<b>{action.Target.Name}</b> confusedly wonders where {GPPHeIs(action.Target)} as the spell breaks. <b>{action.Unit.Name}</b> smugly knows {GPPHeIsAbbr(action.Target)} exactly where {GPPHeIsAbbr(action.Target)} meant to be~"
            );
        }
    }

    string GenerateDiminshmentExpiringMessage(EventLog action)
    {
        if (action.preyLocation == PreyLocation.stomach || action.preyLocation == PreyLocation.stomach2)
        {
            return GetRandomStringFrom(
                $"<b>{action.Unit.Name}</b>’s stomach expands violently as {GPPHis(action.Unit)} previously diminutive prey reverts to {GPPHis(action.Target)} regular size.",
                $"<b>{action.Unit.Name}</b> falls onto the ground as {GPPHis(action.Unit)} belly is suddenly filled with a full-sized {action.Target.Race}. {GPPHe(action.Unit)} rubs {GPPHis(action.Unit)} engorged gut before standing once more.",
                $"<b>{action.Unit.Name}</b> had been eagerly waiting for {GPPHis(action.Unit)} tiny meal to revert to its regular size. When {GPPHis(action.Unit)} gut finally expands, the air is filled with {GPPHis(action.Unit)} cries of pleasure and a great sloshing.",
                $"<b>{action.Unit.Name}</b>’s tummy nearly bursts as <b>{action.Target.Name}</b> reverts to {GPPHis(action.Target)} usual size."
                );
        }
        else
        {
            return GetRandomStringFrom(
                $"<b>{action.Unit.Name}</b>’s {PreyLocStrings.ToSyn(action.preyLocation)} expands violently as {GPPHis(action.Unit)} previously diminutive prey reverts to {GPPHis(action.Target)} regular size.",
                $"<b>{action.Unit.Name}</b> had been eagerly waiting for {GPPHis(action.Unit)} tiny meal to revert to its regular size. When {GPPHis(action.Unit)} {PreyLocStrings.ToSyn(action.preyLocation)} finally expands, the air is filled with {GPPHis(action.Unit)} cries of pleasure and a great sloshing."
            );
        }

    }

    private string GenerateEscapeMessage(EventLog action, string odds)
    {
        if (SimpleText)
        {
            return $"<b>{action.Target.Name}</b> escaped from <b>{action.Unit.Name}</b>'s {action.preyLocation.ToSyn()}.{odds}";
        }
        if (action.preyLocation == PreyLocation.stomach)
        {
            if (action.Target.Race < Race.Vagrants || action.Target.Race >= Race.Selicia) // Prey Humanoid
            {
                if (action.Unit.Race < Race.Vagrants || action.Unit.Race >= Race.Selicia) // Pred Humanoid
                    return GetRandomStringFrom(
                    $"From within <b>{action.Unit.Name}</b>’s gurgling gut, <b>{action.Target.Name}</b> remembers all the loved ones that would miss {GPPHim(action.Target)} and with this incentive forces {GPPHis(action.Target)} way out.{odds}",
                    $"<b>{action.Unit.Name}</b>’s stomach finds something particularly disagreeable with how <b>{action.Target.Name}</b> tastes. With a wretched gag, <b>{action.Target.Name}</b> is expelled from <b>{action.Unit.Name}</b>’s tummy.{odds}",
                    $"The rampant indigestion caused by <b>{action.Target.Name}</b>’s incessant struggles causes <b>{action.Unit.Name}</b> to reluctantly release {GPPHis(action.Unit)} stubborn prey.{odds}",
                    $"<b>{action.Target.Name}</b>’s determination proves greater than the strength of <b>{action.Unit.Name}</b>’s constitution as {GPPHe(action.Target)} free{SIfSingular(action.Target)} {GPPHimself(action.Unit)} from {GPPHis(action.Unit)} fleshy prison.{odds}",
                    $"<b>{action.Target.Name}</b> claws {GPPHis(action.Target)} way up <b>{action.Unit.Name}</b>’s throat and is able to pull {GPPHimself(action.Target)} free.{odds}",
                    $"<b>{action.Unit.Name}</b> can feel the tip of a weapon stabbing at {GPPHis(action.Unit)} insides. Panicking, the worried predator spits <b>{action.Target.Name}</b> up quickly.{odds}",
                    $"<b>{action.Target.Name}</b> tricks {GPPHis(action.Target)} would-be predator with a heartfelt sob story. <b>{action.Unit.Name}</b> believes it and naïvely lets the clever prey climb out of {GPPHis(action.Unit)} gullet.{odds}",
                    $"<b>{action.Target.Name}</b> becomes terrified as the acids begin to tear into {GPPHis(action.Target)} flesh and in a sudden bout of panic forces <b>{action.Unit.Name}</b> to throw {GPPHim(action.Target)} up.{odds}",
                    $"<b>{action.Unit.Name}</b> relaxes and arrogantly pats {GPPHis(action.Unit)} swollen belly while taunting {GPPHis(action.Unit)} prey; {GPPHeIsAbbr(action.Unit)} taken by surprise as <b>{action.Target.Name}</b> uses the moment of relaxation to fight {GPPHis(action.Target)} way out.{odds}",
                    $"<b>{action.Unit.Name}</b> watches with concern as {GPPHis(action.Unit)} belly suddenly lets out an angry roar. <b>{action.Target.Name}</b> had kept a number of inedible herbs for just this occasion and as they break down they force the belly to expel its contents.{odds}"
                    );
                else  // Pred Feral
                {
                    if (action.Unit.Race == Race.Iliijiith && action.Target.Race != Race.Iliijiith)
                    {
                        if (action.Target.HealthPct <= 0.3f)
                        return $"Cold, and unable to muster up any more struggle, <b>{action.Target.Name}</b> curls up, and waits for the end... Only to suddenly feel warmth again? Looking around, {GPPHe(action.Target)} see{SIfSingular(action.Target)} the battlefield again, <b>{action.Unit.Name}</b> nearby shaking slightly, small fracture lines in its central crystal slowly fading away.{odds}";
                        else
                        return GetRandomStringFrom(
                        $"<b>{action.Target.Name}</b> pushes up against the crystalline inside of <b>{action.Unit.Name}</b>, and watches as it seems to crack slightly, small fractures splintering out from the pressure {GPPHe(action.Target)} put{SIfSingular(action.Target)} on it. Pressing harder, the wall shatters, and <b>{action.Target.Name}</b> tumbles out{GetRandomStringFrom($".", $". Looking back, {GPPHe(action.Target)} watch{EsIfSingular(action.Target)} all the crystalline fragments flow back to <b>{action.Unit.Name}</b>, sliding back into place like parts of a puzzle.")}{odds}",
                        $"<b>{action.Unit.Name}</b> stumbles and shifts as small cracks form along its surface, before suddenly, with a flash of light, <b>{action.Target.Name}</b> is back!{odds}",
                        $"As <b>{action.Target.Name}</b> sits inside <b>{action.Unit.Name}</b>, the walls of the crystal suddenly start crushing in from all sides. <b>{action.Target.Name}</b> is certain this is it, until {GPPHe(action.Target)} simply phase through and land unscathed outside the now seemingly fractured Iliijiith.{odds}"
                        );
                    }
                    if (State.Rand.Next(2) == 0 && action.Unit.Race == Race.FeralEevee)
                        return GetRandomStringFrom(
                        $"As <b>{action.Target.Name}</b> pulls {GPPHimself(action.Target)} up <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> throat, the Eevee looks around for a playmate. As <b>{action.Target.Name}</b> slides back out the Eevee's mouth, {GPPHe(action.Unit)} get{SIfSingular(action.Unit)} excited, as a new playmate has been found!{odds}",
                        $"After forcefully prying {GPPHis(action.Target)} way out <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> mouth, <b>{action.Target.Name}</b> looks back at the Eevee, and actually feels kind of bad, and for a split moment considers going back in to make <b>{action.Unit.Name}</b> feel better.{odds}",
                        $"<b>{action.Unit.Name}</b> feels a pressure at {GPPHis(action.Unit)} anus, and does what any animal would do. Squats and tries to poop. Only, instead of poop, out comes a tired <b>{action.Target.Name}</b>, exhausted from forcing {GPPHis(action.Target)} way through the Eevee's bowels.{odds}",
                        $"<b>{action.Target.Name}</b> has lost track of time crawling up <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> throat. \"I swear the throat wasn't this long or bendy going down,\" {GPPHe(action.Target)} think{SIfSingular(action.Target)} to {GPPHimself(action.Target)}. As <b>{action.Unit.Name}</b> pulls {GPPHimself(action.Target)} out the small creature's behind, {GPPHe(action.Target)} realize{SIfSingular(action.Target)} {GPPHis(action.Target)} mistake, and blush.{odds}",
                        $"As <b>{action.Target.Name}</b> tries to escape, <b>{action.Unit.Name}</b> believes these attempts a game, and keeps {GetRandomStringFrom($"<b>{action.Target.Name}</b>", $"the {GetRaceDescSingl(action.Unit)}")} down in {GPPHis(action.Unit)} {PreyLocStrings.ToSyn(PreyLocation.stomach)} quite well. After a few {GetRandomStringFrom("rounds", "rounds(escape attempts)")}, <b>{action.Unit.Name}</b>, being a good sport, lets <b>{action.Target.Name}</b> win{GetRandomStringFrom(".", ", and eagerly awaits the next game.}")}{odds}",
                        $"As <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> {action.preyLocation.ToSyn()} begins to dissolve <b>{action.Target.Name}</b>, the {GetRaceDescSingl(action.Target)} lets out a wimper of pain. Feeling bad, <b>{action.Unit.Name}</b> lets {GPPHim(action.Target)} out.{odds}",
                        $"Inside <b>{action.Unit.Name}</b>, <b>{action.Target.Name}</b> is crying. Hearing the sounds, the Eevee lets <b>{action.Target.Name}</b> out, and tries to snuggle against {GPPHis(action.Target)} {GetRandomStringFrom("leg", "foot", "base")}.{odds}"
                        );
                    return GetRandomStringFrom(
                    $"From within <b>{action.Unit.Name}</b>’s gurgling gut, <b>{action.Target.Name}</b> remembers all the loved ones that would miss {GPPHim(action.Target)} and with this incentive forces {GPPHis(action.Target)} way out.{odds}",
                    $"<b>{action.Unit.Name}</b>’s stomach finds something particularly disagreeable with how <b>{action.Target.Name}</b> tastes. With a wretched gag, <b>{action.Target.Name}</b> is expelled from <b>{action.Unit.Name}</b>’s tummy.{odds}",
                    $"The rampant indigestion caused by <b>{action.Target.Name}</b>’s incessant struggles causes <b>{action.Unit.Name}</b> to reluctantly release {GPPHis(action.Unit)} stubborn prey.{odds}",
                    $"<b>{action.Target.Name}</b>’s determination proves greater than the strength of <b>{action.Unit.Name}</b>’s constitution as {GPPHe(action.Target)} free{SIfSingular(action.Target)} {GPPHimself(action.Unit)} from {GPPHis(action.Unit)} fleshy prison.{odds}",
                    $"<b>{action.Target.Name}</b> claws {GPPHis(action.Target)} way up <b>{action.Unit.Name}</b>’s throat and is able to pull {GPPHimself(action.Target)} free.{odds}",
                    $"<b>{action.Unit.Name}</b> can feel the tip of a weapon stabbing at {GPPHis(action.Unit)} insides. Panicking, the worried predator spits <b>{action.Target.Name}</b> up quickly.{odds}",
                    $"<b>{action.Target.Name}</b> becomes terrified as the acids begin to tear into {GPPHis(action.Target)} flesh and in a sudden bout of panic forces <b>{action.Unit.Name}</b> to throw {GPPHim(action.Target)} up.{odds}",
                    $"<b>{action.Unit.Name}</b> watches with concern as {GPPHis(action.Unit)} belly suddenly lets out an angry roar. <b>{action.Target.Name}</b> had kept a number of inedible herbs for just this occasion and as they break down they force the belly to expel its contents.{odds}"
                    );
                }
            }
            else // Prey Feral
            {
                if (action.Unit.Race < Race.Vagrants || action.Unit.Race >= Race.Selicia) // Pred Humanoid
                    return GetRandomStringFrom(
                    $"<b>{action.Unit.Name}</b>’s stomach finds something particularly disagreeable with how <b>{action.Target.Name}</b> tastes. With a wretched gag, <b>{action.Target.Name}</b> is expelled from <b>{action.Unit.Name}</b>’s tummy.{odds}",
                    $"The rampant indigestion caused by <b>{action.Target.Name}</b>’s incessant struggles causes <b>{action.Unit.Name}</b> to reluctantly release {GPPHis(action.Unit)} stubborn prey.{odds}",
                    $"<b>{action.Target.Name}</b>’s determination proves greater than the strength of <b>{action.Unit.Name}</b>’s constitution as {GPPHe(action.Target)} free{SIfSingular(action.Target)} {GPPHimself(action.Unit)} from {GPPHis(action.Unit)} fleshy prison.{odds}",
                    $"<b>{action.Target.Name}</b> claws {GPPHis(action.Target)} way up <b>{action.Unit.Name}</b>’s throat and is able to pull {GPPHimself(action.Target)} free.{odds}",
                    $"<b>{action.Target.Name}</b> becomes terrified as the acids begin to tear into {GPPHis(action.Target)} flesh and in a sudden bout of panic forces <b>{action.Unit.Name}</b> to throw {GPPHim(action.Target)} up.{odds}",
                    $"<b>{action.Unit.Name}</b> relaxes and arrogantly pats {GPPHis(action.Unit)} swollen belly while taunting {GPPHis(action.Unit)} prey; {GPPHeIsAbbr(action.Unit)} taken by surprise as <b>{action.Target.Name}</b> uses the moment of relaxation to fight {GPPHis(action.Target)} way out.{odds}",
                    $"<b>{action.Target.Name}</b>'s survival instincts take over, letting {GPPHim(action.Target)} channel a burst of near supernatural strength and setting {GPPHim(action.Target)} free.{odds}",
                    $"<b>{action.Target.Name}</b>'s natural built-in weapons proove too much to leave {GPPHim(action.Target)} contained. The irritated gut soon sets {GPPHim(action.Target)} free.{odds}"
                    );
                else  // Pred Feral
                    if (action.Unit.Race == Race.Iliijiith && action.Target.Race == Race.Iliijiith)
                    return $"Suddenly, <b>{action.Unit.Name}</b> turns into <b>{action.Target.Name}</b>, and then back to <b>{action.Unit.Name}</b>, and back- It flashes between being the two Iliijiith faster and faster until both remain, standing next to one another.{odds}";
                    else
                    return GetRandomStringFrom(
                    $"<b>{action.Unit.Name}</b>’s stomach finds something particularly disagreeable with how <b>{action.Target.Name}</b> tastes. With a wretched gag, <b>{action.Target.Name}</b> is expelled from <b>{action.Unit.Name}</b>’s tummy.{odds}",
                    $"The rampant indigestion caused by <b>{action.Target.Name}</b>’s incessant struggles causes <b>{action.Unit.Name}</b> to reluctantly release {GPPHis(action.Unit)} stubborn prey.{odds}",
                    $"<b>{action.Target.Name}</b>’s determination proves greater than the strength of <b>{action.Unit.Name}</b>’s constitution as {GPPHe(action.Target)} free{SIfSingular(action.Target)} {GPPHimself(action.Unit)} from {GPPHis(action.Unit)} fleshy prison.{odds}",
                    $"<b>{action.Target.Name}</b> claws {GPPHis(action.Target)} way up <b>{action.Unit.Name}</b>’s throat and is able to pull {GPPHimself(action.Target)} free.{odds}",
                    $"<b>{action.Target.Name}</b> becomes terrified as the acids begin to tear into {GPPHis(action.Target)} flesh and in a sudden bout of panic forces <b>{action.Unit.Name}</b> to throw {GPPHim(action.Target)} up.{odds}",
                    $"<b>{action.Target.Name}</b>'s survival instincts take over, letting {GPPHim(action.Target)} channel a burst of near supernatural strength and setting {GPPHim(action.Target)} free.{odds}",
                    $"<b>{action.Target.Name}</b>'s natural built-in weapons proove too much to leave {GPPHim(action.Target)} contained. The irritated gut soon sets {GPPHim(action.Target)} free.{odds}"
                    );
            }

        }
        else
        {
            if (action.preyLocation == PreyLocation.breasts && action.Unit.Race == Race.Kangaroos)
            {
                return GetRandomStringFrom(
                $"Just when all hope seemed lost, <b>{action.Target.Name}</b> manages to pry <b>{action.Unit.Name}</b>'s pouch entrance open, and clambers out, taking large breaths of fresh air. {odds}",
                $"In the chaos of battle, <b>{action.Unit.Name}</b> leans over, causing a crease to appear in {GPPHis(action.Unit)} pouch, forcing the pouch's entrance to unseal ever-so-slightly. <b>{action.Target.Name}</b> seizes the opportunity, clawing {GPPHis(action.Target)} way out, and taking several deep, victorious gulps of real air.{odds}",
                $"Rather suddenly, a blade pokes out of <b>{action.Unit.Name}</b>'s pouch's entrance, a knife or dagger of some kind. \"Let me out right now, or I'll carve your whole stupid pouch off,\" <b>{action.Target.Name}</b> angrily demands. <b>{action.Unit.Name}</b>, who would rather not be mutilated, caves and quickly pushes <b>{action.Target.Name}</b> out.{odds}",
                $"Rather suddenly, a blade pokes out of <b>{action.Unit.Name}</b>'s pouch's entrance, a knife or dagger of some kind. \"Let me out right now, or I'll carve your whole stupid pouch off,\" <b>{action.Target.Name}</b> angrily demands. Seeing no other option, <b>{action.Unit.Name}</b> opens {GPPHis(action.Unit)} pouch, and <b>{action.Target.Name}</b> quickly jumps out.{odds}",
                $"In the chaos of battle, <b>{action.Unit.Name}</b> leans over, allowing <b>{action.Target.Name}</b> an opportunity to escape, which {GPPHe(action.Target)} take{SIfSingular(action.Target)} quite happily.",
                $"As a blade pokes out of <b>{action.Unit.Name}</b>'s pouch's entrance, <b>{action.Target.Name}</b> demands to be let out. Within moments, <b>{action.Unit.Name}</b> complies."
                );
            }
            if (((action.preyLocation == PreyLocation.breasts) || ((action.preyLocation == PreyLocation.rightBreast || action.preyLocation == PreyLocation.leftBreast) && Config.FairyBVType == FairyBVType.Shared)) && State.Rand.Next(3) == 0) //Unique 'cleavage' vore messages by Tatltuae! Refer to StoredLogTexts.cs for explanation of the new cleavage vore messages
            {
                return GetRandomStringFrom(
                $"As the jiggling of <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> {GetRandomStringFrom("tits", "boobs", "breasts")} hit a peak, a hand suddenly stretches out from between them. This hand is soon followed by the rest of <b>{action.Target.Name}</b>, pulling {GPPHimself(action.Target)} out.{odds}",
                $"<b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> {GetRandomStringFrom("tits", "boobs", "breasts")} begin to bounce up and down, up and down, faster and faster until <b>{action.Target.Name}</b> is launched out from between them.{odds}",
                $"As <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> {GetRandomStringFrom("tits", "boobs", "breasts")} begin bouncing up in attempts to hit {GPPHis(action.Unit)} face, <b>{action.Unit.Name}</b> releases <b>{action.Target.Name}</b> rather than suffer \"death by sentient boob fat.\"{odds}"
                );
            }
            if (action.preyLocation == PreyLocation.tail)
            {
                if (action.Unit.Race == Race.Bees)
                {
                    return GetRandomStringFrom(
                    $"<b>{action.Unit.Name}</b> winces in pain, and {GPPHis(action.Unit)} stinger opens wide and a honey soaked <b>{action.Target.Name}</b> slides out{GetRandomStringFrom(".", ", victorious.", ", victorious!")}{odds}",
                    $"<b>{action.Target.Name}</b> manages to, despite the sweet honey holding {GPPHim(action.Target)} back, strike the inside of <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> stinger, forcing it to pull open as the sticky {GetRaceDescSingl(action.Target)} makes {GPPHis(action.Target)} escape{GetRandomStringFrom(".", "!")}{odds}",
                    $"<b>{action.Target.Name}</b> manages to, despite the sweet honey holding {GPPHim(action.Target)} back, strike the inside of <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> stinger, forcing {GPPHis(action.Target)} way back out of its opening.{odds}",
                    $"<b>{action.Unit.Name}</b> feels an intense pressure in the base of {GPPHis(action.Unit)} stinger, and soon <b>{action.Target.Name}</b> emerges, the stinger above sputtering out honey as <b>{action.Unit.Name}</b> tries to collect {GPPHimself(action.Unit)}.{odds}"
                    );
                }
                if (!(action.Unit.Race == Race.Youko) && !(action.Unit.Race == Race.Terrorbird))
                {
                    return GetRandomStringFrom(
                    $"The bulge <b>{action.Target.Name}</b> makes in <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> tail begins to shift downwards, and before the {GetRaceDescSingl(action.Unit)} can try to stop it, the tip of {GPPHis(action.Unit)} tail opens up as <b>{action.Target.Name}</b> squirms {GetRandomStringFrom("free", $"{GPPHis(action.Target)} way out")}.{odds}",
                    $"As <b>{action.Unit.Name}</b> makes {GPPHis(action.Unit)} way across the battlefield, an appendage suddenly forces its way from the tip of {GPPHis(action.Unit)} tail, and grabs at the ground. <b>{action.Unit.Name}</b> forces {GPPHis(action.Unit)} way forward anyways, and slowly <b>{action.Target.Name}</b> is extracted from the {ApostrophizeWithOrWithoutS(GetRaceDescSingl(action.Unit))} tail.{odds}",
                    $"Inside the {ApostrophizeWithOrWithoutS(GetRaceDescSingl(action.Unit))} tail, <b>{action.Unit.Name}</b> squirms and struggles, slowly forcing {GPPHis(action.Target)} way back to the entrance {GPPHe(action.Target)} {WasWere(action.Target)} pulled in through in the first place, before, with a glimmer of light, achieving victory, pulling {GPPHimself(action.Target)} out of <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> tail.{odds}"
                    );
                }
                if (action.Unit.Race == Race.Youko)
                {
                    return GetRandomStringFrom(
                    $"Suddenly, <b>{action.Target.Name}</b> appears from between <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> tails, and slowly pulls {GPPHimself(action.Target)} free of {GPPHis(action.Target)} soft fluffy prison.{odds}",
                    $"The tight clump of fur that are <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> {Math.Min((int)(action.Unit.GetStatTotal() - 85) / 15, 7) + 2} tails shake and shift until, with a soft <i>thud</i> <b>{action.Target.Name}</b> falls free onto the ground.{odds}",
                    $"As <b>{action.Target.Name}</b> squirms against the tails around {GPPHim(action.Target)}, {GPPHe(action.Target)} manage{SIfSingular(action.Target)} to find the point where all {Math.Min((int)(action.Unit.GetStatTotal() - 85) / 15, 7) + 2} connect to <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> body. A quick strike to that spot, and the {GetRaceDescSingl(action.Unit)} yelps as {GPPHis(action.Unit)} tails fan open, and <b>{action.Target.Name}</b> goes free.{odds}",
                    $"Spotting a hint of light between the tails holding {GPPHim(action.Target)} prisoner, <b>{action.Target.Name}</b> pushes {GPPHimself(action.Target)} in that direction hard, and with a quick <i>fwumpf</i> as {GPPHe(action.Target)} push past <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> tails, the {GetRaceDescSingl(action.Target)} is free.{odds}",
                    $"As <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> struggles against <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> tails intensify, {GPPHe(action.Target)} manage{SIfSingular(action.Target)} to throw enough tails off of {GPPHis(action.Target)} body to fall out of the {ApostrophizeWithOrWithoutS(GetRaceDescSingl(action.Unit))} grasp.{odds}"
                    );
                }
                if (action.Unit.Race == Race.Terrorbird)
                {
                    return GetRandomStringFrom(
                    $"<b>{action.Target.Name}</b> escaped from <b>{action.Unit.Name}</b>'s crop.{odds}",
                    $"From within <b>{action.Unit.Name}</b>’s crop, <b>{action.Target.Name}</b> remembers all the loved ones that would miss {GPPHim(action.Target)}, and with this incentive forces {GPPHis(action.Target)} way out.{odds}",
                    $"<b>{action.Unit.Name}</b> can feel the tip of a weapon stabbing at {GPPHis(action.Unit)} insides. Panicking, the worried predator spits <b>{action.Target.Name}</b> up quickly.{odds}"
                    );
                }
            }
            if (State.Rand.Next(4) != 0 && action.preyLocation == PreyLocation.bladder)
            {
                if (action.Unit.HasDick && ActorHumanoid(action.Target) && ActorHumanoid(action.Unit) && State.Rand.Next(3) == 0)
                    return $"With a kick to {GPPHis(action.Unit)} lower spine, <b>{action.Target.Name}</b> shoots out of <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> {PreyLocStrings.ToCockSyn()}{odds}";
                if (action.Unit.HasVagina && ActorHumanoid(action.Target) && ActorHumanoid(action.Unit) && State.Rand.Next(3) == 0)
                    return $"<b>{action.Target.Name}</b> manages to reach {GPPHis(action.Target)} arm out of <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> body, and uses the {ApostrophizeWithOrWithoutS(GetRaceDescSingl(action.Unit))} thighs as handholds whist pulling {GPPHimself(action.Target)} out.{odds}";
                if (action.Unit.HasDick && State.Rand.Next(2) != 0)
                    return GetRandomStringFrom(
                    $"<b>{action.Target.Name}</b> slowly and carefully extracts {GPPHimself(action.Target)} from <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> bladder, {GPPHis(action.Target)} head emerging from <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> {PreyLocStrings.ToCockSyn()}.{odds}",
                    $"Getting clever, <b>{action.Target.Name}</b> remains perfectly still for a minute. Believing {GPPHimself(action.Unit)} to have won, <b>{action.Unit.Name}</b> casually takes a piss, unwittingly pushing <b>{action.Target.Name}</b> back out {GPPHis(action.Unit)} {PreyLocStrings.ToCockSyn()}.{odds}"
                    );
                if (action.Unit.HasVagina && State.Rand.Next(2) != 0)
                    return GetRandomStringFrom(
                    $"Within <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> bladder, <b>{action.Target.Name}</b> stretches out, and forces {GPPHimself(action.Target)} out from <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> nethers.{odds}",
                    $"After a particularly vicious struggle, <b>{action.Target.Name}</b> manages to reemerge from <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> slit.{odds}"
                    );
                else
                    return GetRandomStringFrom(
                    $"<b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> constant movement within <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> bladder makes <b>{action.Unit.Name}</b> feel as though {GPPHe(action.Unit)} need{SIfSingular(action.Unit)} to go pee. As <b>{action.Unit.Name}</b> urinates, <b>{action.Target.Name}</b> is pissed out intact.{odds}",
                    $"<b>{action.Target.Name}</b> aggressively forces {GPPHis(action.Target)} way out through <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> urethra.{odds}",
                    $"Within <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> bladder, <b>{action.Target.Name}</b> orients {GPPHimself(action.Target)} upside down. With one mighty push, <b>{action.Target.Name}</b> goes straight back out the hole {GPPHe(action.Target)} came in through.{odds}"
                    );
            }
            return GetRandomStringFrom(
            $"<b>{action.Target.Name}</b> escaped from <b>{action.Unit.Name}</b>'s {action.preyLocation.ToSyn()}.{odds}",
            $"From within <b>{action.Unit.Name}</b>’s {action.preyLocation.ToSyn()}, <b>{action.Target.Name}</b> remembers all the loved ones that would miss {GPPHim(action.Target)}, and with this incentive forces {GPPHis(action.Target)} way out.{odds}",
            $"<b>{action.Target.Name}</b>’s determination proves greater than the strength of <b>{action.Unit.Name}</b>’s constitution as {GPPHe(action.Target)} free{SIfSingular(action.Target)} {GPPHimself(action.Target)} from {GPPHis(action.Target)} fleshy prison.{odds}",
            $"<b>{action.Unit.Name}</b> can feel the tip of a weapon stabbing at {GPPHis(action.Unit)} insides. Panicking, the worried predator spits <b>{action.Target.Name}</b> up quickly.{odds}",
            $"<b>{action.Target.Name}</b> tricks {GPPHis(action.Target)} would-be predator with a heartfelt sob story. <b>{action.Unit.Name}</b> believes it and naïvely lets the clever prey climb back out.{odds}"
            );
        }

    }

    private string GenerateRegurgitationMessage(EventLog action)
    {
        if (SimpleText)
        {
            return $"<b>{action.Unit.Name}</b> regurgitates <b>{action.Target.Name}</b>.";
        }
        List<string> possibleLines = new List<string>();
        if (action.Unit.Race == Race.Slimes)
        {
            possibleLines.Add($"As <b>{action.Unit.Name}</b> moves forward, {GPPHis(action.Unit)} slimey body contorts, leaving behind <b>{action.Target.Name}</b>, covered in goo, but otherwise alive.");
            if (action.Target.Race != Race.Slimes)
                possibleLines.Add($"For a moment, <b>{action.Unit.Name}</b> appears to be undergoing mitosis, splitting in half. Then, one half pulls itself off a slightly freaked out <b>{action.Target.Name}</b>, the other becoming <b>{action.Unit.Name}</b> once again.");
            else
                possibleLines.Add($"For a moment, <b>{action.Unit.Name}</b> appears to be undergoing mitosis, splitting in half. Then, one half begins to shift slightly as <b>{action.Target.Name}</b> becomes a seperate slime once more.");
            return GetRandomStringFrom(possibleLines.ToArray());
        }
        if (action.Unit.Race == Race.Iliijiith)
        {
            possibleLines.Add($"One of the flat faces of <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> geometric form suddenly vanishes for a moment, <b>{action.Target.Name}</b> falling out, the face reappearing behind {GPPHim(action.Target)}.");
            return GetRandomStringFrom(possibleLines.ToArray());
        }
        possibleLines.Add($"<b>{action.Unit.Name}</b> decides to {GetRandomStringFrom("release", "free", "regurgitate", "eject")} <b>{action.Target.Name}</b>.");//Generic unspecified line
        if (!(action.preyLocation == PreyLocation.tail && ((action.Unit.Race == Race.Youko) || (action.Unit.Race == Race.Terrorbird))) && !(action.preyLocation == PreyLocation.breasts && (action.Unit.Race == Race.Kangaroos)))//Exclude races that use repurposed vore locations from generic lines that specify the prey location
        {
        possibleLines.Add($"<b>{action.Unit.Name}</b> {GetRandomStringFrom("regurgitated", "released", "freed", "pushed out")} <b>{action.Target.Name}</b>{GetRandomStringFrom(".", $" from {GPPHis(action.Unit)} {PreyLocStrings.ToSyn(action.preyLocation)}.")}");
        possibleLines.Add($"<b>{action.Unit.Name}</b> decides to eject <b>{action.Target.Name}</b> from {GPPHis(action.Unit)} {PreyLocStrings.ToSyn(action.preyLocation)}.");
        possibleLines.Add($"As <b>{action.Unit.Name}</b> hears a gurgle{GetRandomStringFrom("", $" eminate from {GPPHis(action.Unit)} {PreyLocStrings.ToSyn(action.preyLocation)}")}, {GPPHe(action.Unit)} force{SIfSingular(action.Unit)} <b>{action.Target.Name}</b> out, not wishing to digest {GPPHim(action.Target)}.");
        }
        if (action.preyLocation == PreyLocation.stomach || action.preyLocation == PreyLocation.anal)
        {
            possibleLines.Add($"<b>{action.Target.Name}</b> was released from <b>{action.Unit.Name}</b>'s stomach.");
            possibleLines.Add($"With a great heave, <b>{action.Unit.Name}</b> {GetRandomStringFrom("vomits out", "spits out", "pukes up", "coughs up")} a still living <b>{action.Target.Name}</b>.");
            if (Config.Scat)
                possibleLines.Add($"As <b>{action.Unit.Name}</b>'s guts grumble, and the bulge <b>{action.Target.Name}</b> makes seems to move downwards, <b>{action.Unit.Name}</b> is briefly worried that {GPPHe(action.Unit)} killed <b>{action.Target.Name}</b>. <b>{action.Unit.Name}</b> hurridly does a series of clenches to force <b>{action.Target.Name}</b> out, and is relieved when, instead of shit, a perfectly healthy <b>{action.Target.Name}</b> slides out {GPPHis(action.Unit)} anus.");
            switch (action.preyLocation)
            {
                case PreyLocation.stomach:
                    possibleLines.Add($"<b>{action.Unit.Name}</b>, not wishing to {((action.Unit.HasTrait(Traits.FriendlyStomach) || action.Unit.HasTrait(Traits.Endosoma)) && action.Unit.Side == action.Target.Side ? $"carry around <b>{action.Target.Name}</b> any longer" : $"digest <b>{action.Target.Name}</b>")}, sticks a finger in {GPPHis(action.Unit)} throat and {GetRandomStringFrom("vomits out", "throws up", "coughs up")} {GetRandomStringFrom($"<b>{action.Target.Name}</b>", $"the {GetRaceDescSingl(action.Target)}")}.");
                    possibleLines.Add($"<b>{action.Target.Name}</b> was released back out <b>{action.Unit.Name}</b>'s mouth.");
                    possibleLines.Add($"<b>{action.Unit.Name}</b> pushes up on the bulge <b>{action.Target.Name}</b> makes in {GPPHis(action.Unit)} belly. It isn't long before <b>{action.Target.Name}</b> is pushed back out {GetRandomStringFrom($"the way {GPPHe(action.Target)} came in", $"<b>{action.Unit.Name}</b>'s mouth")}.");
                    possibleLines.Add($"<b>{action.Unit.Name}</b> pushes down on the bulge <b>{action.Target.Name}</b> makes in {GPPHis(action.Unit)} belly. For a moment, this appears to do nothing aside from cause <b>{action.Target.Name}</b> some discomfort. Then, <b>{action.Target.Name}</b> emerges intact from {GetRandomStringFrom($"<b>{action.Unit.Name}</b>", $"the {GetRaceDescSingl(action.Unit)}")}'s ass!");
                    possibleLines.Add($"<b>{action.Unit.Name}</b> pushes down on the bulge <b>{action.Target.Name}</b> makes in {GPPHis(action.Unit)} belly. For a moment, this appears to do nothing aside from cause <b>{action.Target.Name}</b> some discomfort. Then, <b>{action.Target.Name}</b> emerges intact from {GetRandomStringFrom($"<b>{action.Unit.Name}</b>", $"the {GetRaceDescSingl(action.Unit)}")}'s ass! Having completed a full tour through <b>{action.Unit.Name}</b>'s body, <b>{action.Target.Name}</b> simply stands there, confused.");
                    if (action.Unit.Race == Race.FeralEevee)
                    {
                        if (action.Unit.Side == action.Target.Side && ( action.Unit.HasTrait(Traits.FriendlyStomach) || action.Unit.HasTrait(Traits.Endosoma)))
                        {
                            possibleLines.Add($"<b>{action.Unit.Name}</b> spits <b>{action.Target.Name}</b> out, having thought of a game they could play! In the process of throwing {GPPHim(action.Target)} up, <b>{action.Unit.Name}</b> forgets this idea. Oops.");
                            possibleLines.Add($"Being done in <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> gut, <b>{action.Target.Name}</b> starts to slowly squirm, and <b>{action.Unit.Name}</b> swiftly coughs {GPPHim(action.Target)} up.");
                        }
                        if (RandomAlliedWarrior(action.Unit) != null && RandomAlliedWarrior(action.Unit) != action.Target) possibleLines.Add($"<b>{action.Unit.Name}</b> decides {GPPHe(action.Unit)} want to play with <b>{RandomAlliedWarrior(action.Unit).Name}</b>, and so runs off, pushing <b>{action.Target.Name}</b> out {GPPHis(action.Unit)} anus in the process.");
                        possibleLines.Add($"<b>{action.Unit.Name}</b> finally figures out where <b>{action.Target.Name}</b> is and {GetRandomStringFrom("hacks", "spits", "coughs")} {GPPHim(action.Target)} up, before giving {GPPHim(action.Target)} a quizical look as though to ask \"What were you doing in there?\"");
                        possibleLines.Add($"<b>{action.Unit.Name}</b> decides that <b>{action.Target.Name}</b> has been punished enough, and {GetRandomStringFrom("hacks", "spits", "coughs")} {GPPHim(action.Target)} back out.");
                        possibleLines.Add($"<b>{action.Unit.Name}</b> wants to play with someone, and, against all odds, actually remembers that they ate <b>{action.Target.Name}</b>, and lets them back out so that {GPPHe(action.Unit)} can play with the {GetRaceDescSingl(action.Target)}.");
                        possibleLines.Add($"<b>{action.Unit.Name}</b> releases <b>{action.Target.Name}</b>, wanting to play some more with {GetRandomStringFrom($"{GPPHim(action.Target)}", $"the {GetRaceDescSingl(action.Target)}")}.");
                    }
                    break;
                case PreyLocation.anal:
                    possibleLines.Add($"<b>{action.Target.Name}</b> was released back out <b>{action.Unit.Name}</b>'s asshole.");
                    possibleLines.Add($"<b>{action.Unit.Name}</b> pushes down on the bulge <b>{action.Target.Name}</b> makes in {GPPHis(action.Unit)} gut. It isn't long before <b>{action.Target.Name}</b> is pushed back out {GetRandomStringFrom($"the way {GPPHe(action.Target)} came in", $"<b>{action.Unit.Name}</b>'s anus")}.");
                    possibleLines.Add($"<b>{action.Unit.Name}</b> pushes up on the bulge <b>{action.Target.Name}</b> makes in {GPPHis(action.Unit)} gut. It isn't long before <b>{action.Target.Name}</b>'s face appears in the back of <b>{action.Unit.Name}</b>'s throat, before being promptly spat all the way out.");
                    possibleLines.Add($"<b>{action.Unit.Name}</b> pushes up on the bulge <b>{action.Target.Name}</b> makes in {GPPHis(action.Unit)} gut. It isn't long before <b>{action.Target.Name}</b>'s face appears in the back of <b>{action.Unit.Name}</b>'s throat, before being promptly spat all the way out. Having made it all the way through <b>{action.Unit.Name}</b> going the wrong way, {GetRandomStringFrom($"<b>{action.Target.Name}</b>", $"the {GetRaceDescSingl(action.Target)}")} shudders, usure what to do next.");
                    possibleLines.Add($"As <b>{action.Unit.Name}</b> clenches, <b>{action.Target.Name}</b> can feel {GPPHimself(action.Target)} being pulled back down into {GetRandomStringFrom($"<b>{action.Target.Name}</b>", $"the {GetRaceDescSingl(action.Target)}")}'s intestines. It isn't long before {GPPHeIs(action.Target)} pushed back out <b>{action.Unit.Name}</b>'s {GetRandomStringFrom("butt", "ass", "asshole", "anus", "rectum")}, smelly but alive.");
                    if (action.Unit.Race == Race.FeralEevee && action.Unit.Side == action.Target.Side && ( action.Unit.HasTrait(Traits.FriendlyStomach) || action.Unit.HasTrait(Traits.Endosoma)))
                    {
                        possibleLines.Add($"<b>{action.Unit.Name}</b> notices that <b>{action.Target.Name}</b> fell asleep. Not wanting to wake {GPPHim(action.Target)} with the battle, <b>{action.Unit.Name}</b> slowly slides <b>{action.Target.Name}</b> out {GPPHis(action.Unit)} anus.");
                        possibleLines.Add($"<b>{action.Unit.Name}</b> decides to empty {GPPHis(action.Unit)} \"friend storage(rectum),\" and slides <b>{action.Target.Name}</b> back out into the  battlefield.");
                    }
                    break;
                default:
                    return $"What the hell happened? The prey was in the stomach somewhere and now they're not. Message the devs on Discord, please.";
            }
        }
        else if (action.preyLocation == PreyLocation.balls)
        {
            possibleLines.Add($"<b>{action.Unit.Name}</b> reaches down and strokes {GPPHis(action.Unit)} throbbing cock. Once <b>{action.Unit.Name}</b> climaxes, alongside the expected cum emerges <b>{action.Target.Name}</b>{GetRandomStringFrom($"", $", sticky and wet but otherwise unharmed")}.");
            possibleLines.Add($"<b>{action.Unit.Name}</b> faps <b>{action.Target.Name}</b> out of {GPPHis(action.Unit)} {PreyLocStrings.ToSyn(action.preyLocation)}.");
            possibleLines.Add($"<b>{action.Unit.Name}</b> presses upwards on the underside of {GPPHis(action.Unit)} {PreyLocStrings.ToSyn(action.preyLocation)}, forcing <b>{action.Target.Name}</b> {GetRandomStringFrom($"out", $"to re-emerge from {GPPHis(action.Unit)} {PreyLocStrings.ToCockSyn()}")}.");
            possibleLines.Add($"<b>{action.Target.Name}</b> was released from <b>{action.Unit.Name}</b>'s balls.");
            possibleLines.Add($"As <b>{action.Unit.Name}</b> clenches, {GPPHis(action.Unit)} balls shrink inwards, showing the whole of <b>{action.Target.Name}</b>'s trapped form. Slowly, that form moves upwards, sliding up <b>{action.Unit.Name}</b>'s cock, before <b>{action.Target.Name}</b> is extruded from the tip.");
            possibleLines.Add($"After nearly tripping on {GPPHis(action.Unit)} own engorged {PreyLocStrings.ToSyn(action.preyLocation)}, <b>{action.Unit.Name}</b> decides enough is enough, and quickly {GetRandomStringFrom("faps", "forces", "pushes", "cums")} <b>{action.Target.Name}</b> out{GetRandomStringFrom("", $", not even checking if <b>{action.Target.Name}</b> survived or if {GPPHe(action.Target)} became a puddle of {GetRandomStringFrom($"{GetRaceDescSingl(action.Unit)} {PreyLocStrings.SpoogeAdjSyn()} {PreyLocStrings.ToFluid(PreyLocation.balls)}", $"{PreyLocStrings.SpoogeAdjSyn()} {PreyLocStrings.ToFluid(PreyLocation.balls)}", $"{PreyLocStrings.SpoogeAdjSyn()} {GetRaceDescSingl(action.Target)} batter")}")}.");
        }
        else if (action.preyLocation == PreyLocation.womb)
        {
            possibleLines.Add($"<b>{action.Unit.Name}</b> reaches down and rubs {GPPHis(action.Target)} soaking vagina. Once <b>{action.Unit.Name}</b> climaxes, alongside the expected {PreyLocStrings.ToFluid(action.preyLocation)} emerges <b>{action.Target.Name}</b>{GetRandomStringFrom($"", $", sticky and wet but otherwise unharmed")}.");
            possibleLines.Add($"<b>{action.Unit.Name}</b> decides to \"rebirth\" <b>{action.Target.Name}</b> into this world, sliding {GPPHim(action.Target)} out of {GPPHis(action.Unit)} {PreyLocStrings.ToSyn(action.preyLocation)}.");
            possibleLines.Add($"<b>{action.Unit.Name}</b> decides to push <b>{action.Target.Name}</b> back out of {GPPHis(action.Unit)} {PreyLocStrings.ToSyn(action.preyLocation)}, silently {GetRandomStringFrom($"hop", $"pray")}ing that {GPPHe(action.Unit)}'ll get to stick {GetRandomStringFrom($"<b>{action.Target.Name}</b>", $"{GPPHim(action.Target)}")} right back in.");
            possibleLines.Add($"<b>{action.Target.Name}</b> was released from <b>{action.Unit.Name}</b>'s womb.");
            if (action.Unit.Race == Race.FeralEevee)
            {
                if (action.Unit.Side == action.Target.Side && ( action.Unit.HasTrait(Traits.FriendlyStomach) || action.Unit.HasTrait(Traits.Endosoma)))
                {
                    possibleLines.Add($"<b>{action.Target.Name}</b> slides out of <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> vagina. Game over.");
                    possibleLines.Add($"<b>{action.Target.Name}</b> stops moving, and <b>{action.Unit.Name}</b> pushes {GPPHim(action.Target)} from {GPPHis(action.Unit)} vagina to check on them(<b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> fine, {GPPHe(action.Target)} just fell asleep). Game over.");
                    possibleLines.Add($"<b>{action.Target.Name}</b> accidently nicks part of <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> reproductive tract with {GPPHis(action.Target)} {(ActorHumanoid(action.Unit) ? "weapon" : "claws")}, and is expelled out the vagina as punishment. Game over.");
                }
                if (RandomAlliedWarrior(action.Unit) != null && RandomAlliedWarrior(action.Unit) != action.Target) possibleLines.Add($"<b>{action.Unit.Name}</b> decides {GPPHe(action.Unit)} want to play with <b>{RandomAlliedWarrior(action.Unit).Name}</b>, but finds that {GPPHis(action.Unit)} massive middle is in the way of that, so {GPPHe(action.Unit)} bear{SIfSingular(action.Unit)} down and \"birth{SIfSingular(action.Unit)}\" <b>{action.Target.Name}</b> to free up room.");
                possibleLines.Add($"<b>{action.Unit.Name}</b> looks at {GPPHimself(action.Unit)} and wonders when {GPPHe(action.Unit)} got so big. Feeling the weight in {GPPHis(action.Unit)} womb, <b>{action.Unit.Name}</b> thinks that {GPPHe(action.Unit)} must be pregnant, and \"birth{SIfSingular(action.Unit)}\" <b>{action.Target.Name}</b>.");
                possibleLines.Add($"<b>{action.Unit.Name}</b> releases <b>{action.Target.Name}</b>, wanting to play some more with {GetRandomStringFrom($"{GPPHim(action.Target)}", $"the {GetRaceDescSingl(action.Target)}")}.");
            }
        }
        else if (action.preyLocation == PreyLocation.breasts || action.preyLocation == PreyLocation.leftBreast || action.preyLocation == PreyLocation.rightBreast)
        {
            if (action.preyLocation == PreyLocation.breasts)
            {
                if (action.Unit.Race == Race.Kangaroos)
                {
                    possibleLines.Add($"<b>{action.Unit.Name}</b> pushs upwards on the bottom of {GPPHis(action.Unit)} pouch, forcing <b>{action.Target.Name}</b> out and onto the ground.");
                    possibleLines.Add($"After nearly falling over, <b>{action.Unit.Name}</b> unceremoniously dumps <b>{action.Target.Name}</b> out of {GPPHis(action.Unit)} pouch.");
                    possibleLines.Add($"<b>{action.Unit.Name}</b> feels a slight twitching in the muscles of {GPPHis(action.Unit)} pouch's entrance, and quickly pulls <b>{action.Target.Name}</b> out before {GPPHis(action.Unit)} pouch sealed for good.");
                }
                else
                {
                    possibleLines.Add($"<b>{action.Unit.Name}</b> squeezes {GPPHis(action.Unit)} {GetRandomStringFrom("squirming", "wriggling")} boobs, pushing out large amounts of milk, and one very wet <b>{action.Target.Name}</b>.");
                    possibleLines.Add($"After {GPPHis(action.Unit)} full breasts nearly tips {GPPHim(action.Unit)} over, <b>{action.Unit.Name}</b> decides to release <b>{action.Target.Name}</b>, in the process regaining {GPPHis(action.Unit)} balance.");
                    possibleLines.Add($"<b>{action.Target.Name}</b> was released from <b>{action.Unit.Name}</b>'s breasts.");
                }
            }
            else
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> squeezes {GPPHis(action.Unit)} {GetRandomStringFrom("squirming", "wriggling")} {(action.preyLocation == PreyLocation.leftBreast ? "left" : "right")} boob, pushing out large amounts of milk, and one very wet <b>{action.Target.Name}</b>.");
                possibleLines.Add($"After giving {GPPHimself(action.Unit)} a hearty slap on {GPPHis(action.Unit)} {(action.preyLocation == PreyLocation.leftBreast ? "left" : "right")} {GetRandomStringFrom("boob", "breast", "titty")}, <b>{action.Unit.Name}</b> sees <b>{action.Target.Name}</b>'s head poke out of {GPPHis(action.Unit)} nipple! After a moment, <b>{action.Unit.Name}</b> sighs and pulls <b>{action.Target.Name}</b> all the way out.");
                possibleLines.Add($"After {GPPHis(action.Unit)} full breast nearly tips {GPPHim(action.Unit)} over, <b>{action.Unit.Name}</b> decides to release <b>{action.Target.Name}</b>, in the process regaining {GPPHis(action.Unit)} balance.");
                possibleLines.Add($"<b>{action.Target.Name}</b> was released from <b>{action.Unit.Name}</b>'s {(action.preyLocation == PreyLocation.leftBreast ? "left" : "right")} breast.");
            }
        }
        else if (action.preyLocation == PreyLocation.tail)
        {
            if (!(action.Unit.Race == Race.Youko) && !(action.Unit.Race == Race.Terrorbird))
            {
                possibleLines.Add($"No longer able to tolerate the weight of <b>{action.Target.Name}</b> in {GPPHis(action.Unit)} tail, <b>{action.Unit.Name}</b> presses on the bulge {GPPHe(action.Target)} make{SIfSingular(action.Unit)}, and pushes {GPPHim(action.Target)} back out into the world.");
                possibleLines.Add($"{GetRandomStringFrom("Done with", "No longer wanting to hold onto")} <b>{action.Target.Name}</b>, <b>{action.Unit.Name}</b> starts to swing {GPPHis(action.Unit)} tail back and forth, slowly at first, then faster with each swing until the {GetRaceDescSingl(action.Target)} is forced out of the tail, landing with a wet splat on the ground.");
                possibleLines.Add($"<b>{action.Unit.Name}</b> clenches the muscles in {GPPHis(action.Unit)} tail, extruding <b>{action.Target.Name}</b> back into the world.");
                if (action.Unit.Side == action.Target.Side && ( action.Unit.HasTrait(Traits.FriendlyStomach) || action.Unit.HasTrait(Traits.Endosoma))) possibleLines.Add($"<b>{action.Unit.Name}</b> lightly taps on {GPPHis(action.Unit)} swollen tail. \"Hey, <b>{action.Target.Name}</b>? Time to come out.\" From inside, a muffled voice can be heard asking to stay in for just a little longer. Rolling {GPPHis(action.Unit)} eyes, <b>{action.Unit.Name}</b> pushes the half-asleep {GetRaceDescSingl(action.Target)} out of {GPPHis(action.Unit)} tail.");
                if (action.Unit.Race == Race.Bees)
                {
                    if (PotentialNextPrey(action.Unit) != null && PotentialNextPrey(action.Unit).Name != "You, the player") possibleLines.Add($"<b>{action.Unit.Name}</b> prepares to sting <b>{PotentialNextPrey(action.Unit).Name}</b>, but as {GPPHe(action.Unit)} do{EsIfSingular(action.Unit)} a quick {GetRandomStringFrom("practice", "warm-up")} sting, {GPPHis(action.Unit)} stinger bulges and opens wide as a honey {GetRandomStringFrom("soaked", "coated")} <b>{action.Target.Name}</b> is pushed out.");
                    possibleLines.Add($"<b>{action.Unit.Name}</b> begins to worry what melting {GetAorAN(GetRaceDescSingl(action.Target))} into {GPPHis(action.Unit)} honey might do to its flavor, and reluctantly the {GetRaceDescSingl(action.Unit)} pushes <b>{action.Target.Name}</b> back out of {GPPHis(action.Unit)} stinger.");
                    possibleLines.Add($"With a quick contraction in the {ApostrophizeWithOrWithoutS(GetRaceDescSingl(action.Unit))} abdomen, <b>{action.Target.Name}</b> is forced back out <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> stinger and onto the ground. <b>{action.Unit.Name}</b> quickly scoops a bit of the honey off of {GPPHim(action.Target)}, tasting it before saying \"{GetRandomStringFrom("needs more time in there.", "almost done, I see.", "Ooh, ready to eat properly!")}\" <b>{action.Unit.Name}</b> then leers over <b>{action.Target.Name}</b>, clearly readying to put {GPPHim(action.Target)} back in.");
                    possibleLines.Add($"<b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> stripy abdomen pulses for a moment as <b>{action.Target.Name}</b> is extruded from the stinger.");
                    if (action.Unit.Side == action.Target.Side && ( action.Unit.HasTrait(Traits.FriendlyStomach) || action.Unit.HasTrait(Traits.Endosoma)))
                    {
                        possibleLines.Add($"\"Okay, I think you've had enough honey for now...\" <b>{action.Unit.Name}</b> pushes <b>{action.Target.Name}</b> back out of {GPPHis(action.Unit)} stinger, the {GetRaceDescSingl(action.Target)} eagerly lapping up the last bits of honey that came out with {GPPHim(action.Target)}.");
                        possibleLines.Add($"<b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> stripy abdomen pulses for a moment as <b>{action.Target.Name}</b> is extruded from the stinger, <b>{action.Unit.Name}</b> smiling warmly at {GPPHim(action.Target)}.");
                        possibleLines.Add($"<b>{action.Target.Name}</b> pokes the inside of <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> abdomen, and in response <b>{action.Unit.Name}</b> pushes {GPPHim(action.Target)} back out of {GPPHis(action.Unit)} stinger{GetRandomStringFrom(".", $". \"Enjoy it in there?\" \"Yep!\"", $". \"Enjoy it in there?~\" \"Yep!~\"")}");
                    }
                }
            }
            if (action.Unit.Race == Race.Youko)
            {
                possibleLines.Add($"Bored of <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> constant movement and attempts to escape, <b>{action.Unit.Name}</b> decides to let {GPPHim(action.Target)} go, unfurling {GPPHis(action.Unit)} tails and dropping the {GetRaceDescSingl(action.Target)} unceremoniously on the ground.");
                possibleLines.Add($"<b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> bunched up tails pull apart, revealing <b>{action.Target.Name}</b>, who can't decide if {GPPHe(action.Target)} {IsAre(action.Target)} happy to be free or disappointed that the {ApostrophizeWithOrWithoutS(GetRaceDescSingl(action.Unit))} soft tails are no longer blanketing {GPPHim(action.Target)} on all sides.");
                possibleLines.Add($"Slowly, the clump of soft fur jailing <b>{action.Target.Name}</b> parts, as <b>{action.Unit.Name}</b> decides it's time to let the {GetRaceDescSingl(action.Target)} go free.");
                possibleLines.Add($"<b>{action.Unit.Name}</b> looks behind {GPPHimself(action.Unit)} and at {GPPHis(action.Unit)} bunched up tails. Sighing slightly, {GPPHe(action.Unit)} unbunch{SIfSingular(action.Unit)} them, and let{SIfSingular(action.Unit)} <b>{action.Target.Name}</b> fall out.");
                if (action.Unit.Side == action.Target.Side && ( action.Unit.HasTrait(Traits.FriendlyStomach) || action.Unit.HasTrait(Traits.Endosoma)))
                {
                    possibleLines.Add($"\"Ok, time to set you down, I need to stretch my tails a bit.\" <b>{action.Unit.Name}</b> carefully unfurls {GPPHis(action.Unit)} tails to free <b>{action.Target.Name}</b>, much to {GPPHis(action.Target)} dismay as the {GetRaceDescSingl(action.Target)} clings to <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> soft tails like someone being dragged from bed.");
                    possibleLines.Add($"<b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> tails twitch around <b>{action.Target.Name}</b> in frustration as {GPPHe(action.Unit)} hear{SIfSingular(action.Unit)} the sounds of snoring from <b>{action.Target.Name}</b> bound in {GPPHis(action.Unit)} tailfluff. In seconds, <b>{action.Target.Name}</b> is on the ground, <b>{action.Unit.Name}</b> standing over {GPPHim(action.Target)}. \"{GetRandomStringFrom("You can sleep in my tails when we aren't on a battlefield.", "Next time you want to sleep in my tails, ask first.", "And when did I say you could sleep back there?", "Get up.", "Why are you like this?")}\"");
                    possibleLines.Add($"Bored of holding <b>{action.Target.Name}</b> in {GPPHis(action.Unit)} tails, <b>{action.Unit.Name}</b> decides to let {GPPHim(action.Target)} go, unfurling {GPPHis(action.Unit)} tails and dropping the {GetRaceDescSingl(action.Target)} unceremoniously on the ground.");
                    possibleLines.Add($"<b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> bunched up tails pull apart, revealing <b>{action.Target.Name}</b>, disappointed that the {ApostrophizeWithOrWithoutS(GetRaceDescSingl(action.Unit))} soft tails are no longer blanketing {GPPHim(action.Target)} on all sides.");
                    possibleLines.Add($"Slowly, the clump of soft fur cradling <b>{action.Target.Name}</b> parts, as <b>{action.Unit.Name}</b> decides {GPPHe(action.Unit)} no longer want{SIfSingular(action.Unit)} to carry {GPPHim(action.Target)}.");
                }
            }
        }
        else if (action.preyLocation == PreyLocation.bladder)
        {
            possibleLines.Add($"<b>{action.Unit.Name}</b> feels as though {GPPHe(action.Unit)} need{SIfSingular(action.Unit)} to go pee. As <b>{action.Unit.Name}</b> urinates, <b>{action.Target.Name}</b> is pissed out intact.");
            if (action.Unit.HasDick)
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> decides that {GPPHe(action.Unit)} no longer need{SIfSingular(action.Unit)} <b>{action.Target.Name}</b> within {GPPHim(action.Unit)}, and uncerimoniously pees <b>{action.Target.Name}</b> out of {GPPHis(action.Unit)} {PreyLocStrings.ToCockSyn()}.");
                possibleLines.Add($"<b>{action.Unit.Name}</b> grabs {GPPHis(action.Unit)} {PreyLocStrings.ToCockSyn()} tightly, and pisses out a living {GetRaceDescSingl(action.Target)}.");
                possibleLines.Add($"<b>{action.Unit.Name}</b> slowly massages <b>{action.Target.Name}</b> out of {GPPHis(action.Unit)} {PreyLocStrings.ToCockSyn()}.");
            }
            if (action.Unit.HasVagina && ActorHumanoid(action.Unit))
            {
                possibleLines.Add($"<b>{action.Unit.Name}</b> reaches up {GPPHis(action.Unit)} slit, and yanks <b>{action.Target.Name}</b> from {GPPHis(action.Unit)} bladder.");
                possibleLines.Add($"As <b>{action.Unit.Name}</b> pushes <b>{action.Target.Name}</b> from {GPPHis(action.Unit)} slit, {GPPHe(action.Unit)} say{SIfSingular(action.Unit)} \"Your ride ends here, please get out of my bladder now.\"");
            }
        }
        return GetRandomStringFrom(possibleLines.ToArray());
    }

    private string GenerateBellyRubMessage(EventLog action)
    {
        return GetStoredMessage(StoredLogTexts.MessageTypes.BellyRubMessages, action);
    }

    private string GenerateUBSwallowMessage(EventLog action)
    {
        if (SimpleText)
            return $"<b>{action.Unit.Name}</b> unbirths <b>{action.Target.Name}</b>.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.UnbirthMessages, action);
    }

    private string GenerateBirthMessage(EventLog action)
    {
        if (SimpleText)
            return $"<b>{action.Unit.Name}</b> births <b>{action.Target.Name}</b>.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.RebirthMessages, action);
    }

    private string GenerateTVSwallowMessage(EventLog action)
    {
        if (SimpleText)
            return $"<b>{action.Unit.Name}</b> tail vores <b>{action.Target.Name}</b>.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.TailVoreMessages, action);
    }

    private string GenerateAVSwallowMessage(EventLog action)
    {
        if (SimpleText)
            return $"<b>{action.Unit.Name}</b> anal vores <b>{action.Target.Name}</b>.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.AnalVoreMessages, action);
    }

    private string GenerateCVSwallowMessage(EventLog action)
    {
        if (SimpleText)
            return $"<b>{action.Unit.Name}</b> cock vores <b>{action.Target.Name}</b>.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.CockVoreMessages, action);
    }

    private string GenerateBldrVSwallowMessage(EventLog action)
    {
        if (SimpleText)
            return $"<b>{action.Unit.Name}</b> bladder vores <b>{action.Target.Name}</b>.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.BladderMessages, action);
    }

    private string GenerateRandomDigestionMessage(EventLog action)
    {
        return GetStoredMessage(StoredLogTexts.MessageTypes.RandomDigestionMessages, action);

    }

    private string GenerateDigestionLowHealthMessage(EventLog action)
    {
        if (Config.Scat && (action.preyLocation == PreyLocation.stomach || action.preyLocation == PreyLocation.stomach2) && State.Rand.Next(5) == 0)
        {
            return GetRandomStringFrom(
                $"<b>{action.Target.Name}</b> is well on {GPPHis(action.Target)} way to becoming <b>{action.Unit.Name}</b>'s {PreyLocStrings.ScatSyn()}.",
                $"<b>{action.Target.Name}</b> is increasingly falling apart into a foul mess, waiting to be flushed into <b>{action.Unit.Name}</b>'s intestines.",
                $"<b>{action.Target.Name}</b> doesn’t have the fortitude left to resist {GPPHis(action.Target)} destiny as a {GetRaceDescSingl(action.Unit)}'s next {GetRandomStringFrom("dump", "crap", "shit", "bowel movement", "turd")} anymore.",
                $"<b>{action.Unit.Name}</b> can feel <b>{action.Target.Name}</b>'s struggles getting weaker, kindly reminding {GPPHim(action.Target)} that if {GPPHe(action.Target)} fail{SIfSingular(action.Target)} to escape {GPPHeIs(action.Target)} getting {PreyLocStrings.DigestedVerbSyn()} into {PreyLocStrings.ScatSyn()}.",
                $"<b>{action.Unit.Name}</b>’s {action.preyLocation.ToSyn()} rumbles ominously while telling <b>{action.Target.Name}</b> that {GPPHe(action.Unit)} will enjoy {GetRandomStringFrom("shitting", "crapping", "dumping", "squeezing", "pooping")} {GPPHim(action.Target)} out later.");
        }
        if (Config.HardVoreDialog && (action.preyLocation == PreyLocation.stomach || action.preyLocation == PreyLocation.stomach2) && Random.Range(0, 5) == 0)
        {
            string loc = action.preyLocation.ToSyn();
            string locs = (loc.EndsWith("s") ? "" : "s");
            GetRandomStringFrom($"<b>{action.Unit.Name}</b> hears {GPPHis(action.Unit)} {loc} gurgle{locs} intensely. {Capitalize(GPPHe(action.Unit))} feels <b>{action.Target.Name}</b> begin to slip under {GPPHis(action.Unit)} turbulent acids.",
                                $"<b>{action.Unit.Name}</b>'s {loc} glurt{locs} and blort{locs}, {GPPHis(action.Unit)} {GetPredDesc(action.Target)} prey starting to break down. <b>{action.Target.Name}</b> seems doomed.");
        }
        if (action.preyLocation == PreyLocation.breasts && action.Unit.Race == Race.Kangaroos)
        {
            return GetRandomStringFrom(

                $"With so little air in <b>{action.Unit.Name}</b>'s pouch, <b>{action.Target.Name}</b>'s mind has gone a little fuzzy. {Capitalize(GPPHeIs(action.Target))} now talking to {GPPHimself(action.Target)}.",
                $"With no breathable air left in <b>{action.Unit.Name}</b>'s pouch, <b>{action.Target.Name}</b>'s vision begins to grow dark and fuzzy around the edges. The end of <b>{action.Target.Name}</b> is nigh.",
                $"The lack of air within <b>{action.Unit.Name}</b>'s pouch has taken its toll on <b>{action.Target.Name}</b>, whose struggles have begun to slow.",
                $"As the O2 levels in <b>{action.Unit.Name}</b>'s pouch drop to critically low levels, <b>{action.Target.Name}</b> begins to hallucinate. Rather than continue to struggle, <b>{action.Target.Name}</b> decides that a better use of their little remaining oxygen is in having a conversation with these hallucinations.",
                $"<b>{action.Target.Name}</b>'s breathing has now replaced most of the O2 in <b>{action.Unit.Name}</b>'s pouch with CO2. With the air mixture so inhospitable, <b>{action.Target.Name}</b> falls into a coughing fit. As <b>{action.Unit.Name}</b>'s fellow soldiers look at {GPPHim(action.Unit)}, <b>{action.Unit.Name}</b> blushes, and smacks {GPPHis(action.Unit)} pouch a few times, hoping to {GetRandomStringFrom("rob", "drain")} <b>{action.Target.Name}</b> of the last of {GPPHis(action.Target)} strength."
            );
        }
        //Unique 'cleavage' vore messages by Tatltuae! Refer to StoredLogTexts.cs for explanation of the new cleavage vore messages
        if (((action.preyLocation == PreyLocation.breasts) || ((action.preyLocation == PreyLocation.rightBreast || action.preyLocation == PreyLocation.leftBreast) && Config.FairyBVType == FairyBVType.Shared)) && action.Unit.Race != Race.Kangaroos && State.Rand.Next(3) != 0)
        {
            switch (State.Rand.Next(3))
            {
                case 0:
                    return $"As <b>{action.Target.Name}</b> sways on <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> chest, {GPPHe(action.Target)} begin{SIfSingular(action.Target)} to feel somewhat... faded. As though a little less \"<b>{action.Target.Name}</b>\" exists with every sway.";
                case 1:
                    if (State.Rand.Next(2) == 1)
                    {return $"With each exertion of strength, <b>{action.Target.Name}</b> forgets a little more. Right now, {GPPHeIsAbbr(action.Target)} wondering \"what's my name? It's {GetRandomStringFrom("Boob Fat", "Titties")}, right?\"";}
                    else
                    {return $"With each exertion of strength, <b>{action.Target.Name}</b> forgets a little more. Right now, {GPPHeIsAbbr(action.Target)} wondering \"what's my name? It's <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> Breasts, right?\"";}
                default:
                    return $"As <b>{action.Target.Name}</b> continues to struggle, {GPPHe(action.Target)} find{SIfSingular(action.Target)} {GPPHimself(action.Target)} less and less able to remember anything about the world beyond <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> {GetRandomStringFrom("breasts", "boobs", "tits")}.";
            }
        }
        if (action.preyLocation == PreyLocation.bladder && State.Rand.Next(4) != 0)
        {
            switch (State.Rand.Next(3))
            {
                case 0:
                    return $"As <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> bladder fills with more smelly piss, <b>{action.Target.Name}</b> takes solace in the fact that {GPPHis(action.Target)} odorous existance seems to be drawing to a close.";
                case 1:
                    if (ActorHumanoid(action.Target) && ActorHumanoid(action.Unit) && Config.HardVoreDialog && State.Rand.Next(2) != 0)
                    {return $"As <b>{ApostrophizeWithOrWithoutS(action.Target.Name)}</b> struggles slow, <b>{action.Unit.Name}</b> taunts {GPPHis(action.Unit)} prey. \"{GetRandomStringFrom("Soon, you'll be pure liquid, and I'll leave you behind.", $"You're getting close, soon I'll get to {GetRandomStringFrom("pee", "piss")} you out.", "You know, half the fun of this is your struggling. Get back to it or I'll make your death WAY more painful.")}\"";}
                    else
                    {return $"As <b>{ApostrophizeWithOrWithoutS(action.Unit.Name)}</b> bladder fills with more smelly piss, <b>{action.Target.Name}</b> takes solace in the fact that {GPPHis(action.Target)} odorous existance seems to be drawing to a close.";}
                default:
                    return $"As the {PreyLocStrings.ToFluid(PreyLocation.bladder)} around <b>{action.Target.Name}</b> fizzes violently, the {GetRaceDescSingl(action.Target)} commits to one last struggle. Either {GPPHe(action.Target)} escape{SIfSingular(action.Target)}, or {GPPHe(action.Target)} become{SIfSingular(action.Target)} {PreyLocStrings.ToFluid(PreyLocation.bladder)}.";
            }
        }
        int ran = Random.Range(0, 9);
        switch (ran)
        {
            case 0:
                string loc = action.preyLocation.ToSyn();
                return $"<b>{action.Target.Name}</b> feels weak; <b>{action.Unit.Name}</b>'s {loc + (loc.EndsWith("s") ? " are" : " is")} overwhelming.";
            case 1:
                return $"<b>{action.Target.Name}</b> is about to give up fighting <b>{action.Unit.Name}</b>'s {action.preyLocation.ToSyn()}.";
            case 2:
                return $"<b>{action.Target.Name}</b> is fading in the {GetPredDesc(action.Unit)} {GetRaceDescSingl(action.Unit)}'s {action.preyLocation.ToSyn()}. <b>{action.Unit.Name}</b> licks {GPPHis(action.Unit)} lips smugly, feeling it happen.";
            case 3:
                return $"The struggles of <b>{action.Target.Name}</b> become weaker, {GPPHis(action.Target)} death imminent.";
            case 4:
                return $"<b>{action.Target.Name}</b> feels {GPPHis(action.Target)} body becoming soft and pliable.";
            case 5:
                return $"<b>{action.Target.Name}</b> clearly doesn’t have the strength to avoid {GPPHis(action.Target)} messy fate anymore.";
            case 6:
                return $"<b>{action.Target.Name}</b> whimpers, realizing {GPPHis(action.Target)} gurgly doom has arrived as <b>{action.Unit.Name}</b>'s {action.preyLocation.ToSyn()} readies to contract one last time.";
            case 7:
                return $"<b>{action.Unit.Name}</b> can feel <b>{action.Target.Name}</b> submitting to {GPPHis(action.Unit)} {action.preyLocation.ToSyn()}, licking {GPPHis(action.Unit)} lips in satisfaction.";
            default:
                return $"<b>{action.Target.Name}</b> has no strength left. Fears death in the {action.preyLocation.ToSyn()} of <b>{action.Unit.Name}</b>.";
        }
    }

    private string GenerateDigestionDeathMessage(EventLog action)
    {
        if (SimpleText)
            return $"<b>{action.Unit.Name}</b> digested <b>{action.Target.Name}</b>.";

        return GetStoredMessage(StoredLogTexts.MessageTypes.DigestionDeathMessages, action);
    }

    private string GenerateGreatEscapeKeepMessage(EventLog action)
    {
        if (SimpleText)
            return $"<b>{action.Unit.Name}</b> held <b>{action.Target.Name}</b> without digesting {GPPHim(action.Target)}.";

        return GetStoredMessage(StoredLogTexts.MessageTypes.GreatEscapeKeep, action);
    }

    private string GenerateGreatEscapeFleeMessage(EventLog action)
    {
        if (SimpleText)
            return $"<b>{action.Unit.Name}</b>'s prey <b>{action.Target.Name}</b> managed to escape and flee the map.";

        return GetStoredMessage(StoredLogTexts.MessageTypes.GreatEscapeFlee, action);
    }

    private string GenerateAbsorptionMessage(EventLog action)
    {
        if (SimpleText)
            return $"<b>{action.Unit.Name}</b> finished absorbing the leftover nutrients from <b>{action.Target.Name}</b>.";
        return GetStoredMessage(StoredLogTexts.MessageTypes.AbsorptionMessages, action);

    }

    string GetStoredMessage(StoredLogTexts.MessageTypes msgType, EventLog action)
    {
        List<StoredLogTexts.EventString> list = StoredLogTexts.Redirect(msgType);
        IEnumerable<StoredLogTexts.EventString> messages = list.Where(s => (s.ActorRace == action.Unit.Race || s.ActorRace == (Race)4000) && (s.TargetRace == action.Target.Race || s.TargetRace == (Race)4000) &&
        s.Conditional(action));

        if (messages.Any() == false)
        {
            return $"Couldn't find matching message {action.Unit.Name} {action.Type} {action.Target?.Name ?? ""}";
        }

        int priority = messages.Max(s => s.Priority);
        if (priority == 9 && State.Rand.Next(2) == 0)
            priority = 8;

        StoredLogTexts.EventString[] array = messages.Where(s => s.Priority == priority).ToArray();
        return array[State.Rand.Next(array.Length)].GetString(action);
    }

    public void RegisterHit(Unit Attacker, Unit Defender, Weapon weapon, int damage, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Hit,
            Unit = Attacker,
            Damage = damage,
            Target = Defender,
            Weapon = weapon,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterMiss(Unit Attacker, Unit Defender, Weapon weapon, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Miss,
            Unit = Attacker,
            Target = Defender,
            Weapon = weapon,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterVore(Unit predator, Unit prey, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Devour,
            Unit = predator,
            Target = prey,
            Odds = odds,
            preyLocation = PreyLocation.stomach,
        });
        UpdateListing();
    }

    public void RegisterUnbirth(Unit predator, Unit prey, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Unbirth,
            Unit = predator,
            Target = prey,
            Odds = odds,
            preyLocation = PreyLocation.womb,
        });
        UpdateListing();
    }

    public void RegisterCockVore(Unit predator, Unit prey, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.CockVore,
            Unit = predator,
            Target = prey,
            Odds = odds,
            preyLocation = PreyLocation.balls,
        });
        UpdateListing();
    }

    public void RegisterBreastVore(Unit predator, Unit prey, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.BreastVore,
            Unit = predator,
            Target = prey,
            Odds = odds,
            preyLocation = PreyLocation.breasts,
        });
        UpdateListing();
    }

    public void RegisterTailVore(Unit predator, Unit prey, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.TailVore,
            Unit = predator,
            Target = prey,
            Odds = odds,
            preyLocation = PreyLocation.tail,
        });
        UpdateListing();
    }

    public void RegisterAnalVore(Unit predator, Unit prey, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.AnalVore,
            Unit = predator,
            Target = prey,
            Odds = odds,
            preyLocation = PreyLocation.anal,
        });
        UpdateListing();
    }

    public void RegisterBladderVore(Unit predator, Unit prey, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.BladderVore,
            Unit = predator,
            Target = prey,
            Odds = odds,
            preyLocation = PreyLocation.bladder,
        });
        UpdateListing();
    }

    public void RegisterBellyRub(Unit rubber, Unit target, Unit prey, PreyLocation preyLoc, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.BellyRub,
            Unit = rubber,
            Target = target,
            Prey = prey ?? defaultPrey,
            Odds = odds,
            preyLocation = preyLoc,
        });
        UpdateListing();
    }

    public void RegisterBreastRub(Unit rubber, Unit target, Unit prey, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.BreastRub,
            Unit = rubber,
            Target = target,
            Prey = prey ?? defaultPrey,
            Odds = odds,
            preyLocation = PreyLocation.breasts,
        });
        UpdateListing();
    }

    public void RegisterTailRub(Unit rubber, Unit target, Unit prey, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.TailRub,
            Unit = rubber,
            Target = target,
            Prey = prey ?? defaultPrey,
            Odds = odds,
            preyLocation = PreyLocation.tail,
        });
        UpdateListing();
    }

    public void RegisterBallMassage(Unit rubber, Unit target, Unit prey, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.BallMassage,
            Unit = rubber,
            Target = target,
            Prey = prey ?? defaultPrey,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterFeed(Unit predator, Unit target, Unit prey, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Feed,
            Unit = predator,
            Target = target,
            Prey = prey ?? defaultPrey,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterCumFeed(Unit predator, Unit target, Unit prey, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.FeedCum,
            Unit = predator,
            Target = target,
            Prey = prey ?? defaultPrey,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterSuckle(Unit user, Unit target, PreyLocation location, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Suckle,
            Unit = user,
            Target = target,
            preyLocation = location,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterSuckleFail(Unit user, Unit target, PreyLocation location, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.SuckleFail,
            Unit = user,
            Target = target,
            preyLocation = location,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterBirth(Unit predator, Unit prey, float odds, PreyLocation location, int rebirthType)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Birth,
            Unit = predator,
            Target = prey,
            preyLocation = location,
            RebirthType = rebirthType,//1 = NormalRebirth|2 = NormalConvert|3 = DigestRebirth|4 = DigestConvert
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterTransferSuccess(Unit donor, Unit recipient, Unit donation, float odds, PreyLocation loc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.TransferSuccess,
            Unit = donor,
            Target = recipient,
            Prey = donation,
            Odds = odds,
            preyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterKissTransfer(Unit donor, Unit recipient, Unit donation, float odds, PreyLocation loc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.KissTransfer,
            Unit = donor,
            Target = recipient,
            Prey = donation,
            Odds = odds,
            preyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterTransferFail(Unit predator, Unit prey, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.TransferFail,
            Unit = predator,
            Target = prey,
            Odds = odds,
        });
        UpdateListing();
    }

    public void RegisterVoreStealSuccess(Unit donor, Unit recipient, Unit donation, float odds, PreyLocation loc, PreyLocation oldLoc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.VoreStealSuccess,
            Unit = recipient,
            Target = donor,
            Prey = donation,
            Odds = odds,
            preyLocation = loc,
            oldLocation = oldLoc,
        });
        UpdateListing();
    }

    public void RegisterVoreStealFail(Unit donor, Unit recipient, Unit donation, PreyLocation oldLoc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.VoreStealFail,
            Unit = recipient,
            Target = donor,
            Prey = donation,
            oldLocation = oldLoc,
        });
        UpdateListing();
    }

    public void RegisterResist(Unit predator, Unit prey, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Resist,
            Unit = predator,
            Target = prey,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterDazzle(Unit attacker, Unit target, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Dazzle,
            Unit = attacker,
            Target = target,
            Odds = odds
        });
        UpdateListing();
    }
    public void RegisterBlock(Unit Attacker, Unit Defender, Weapon weapon, int damage, float odds)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Block,
            Unit = Attacker,
            Damage = damage,
            Target = Defender,
            Weapon = weapon,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterSpellHit(Unit attacker, Unit target, SpellTypes type, int damage, float odds)
    {
        events.Add(new SpellLog
        {
            Type = MessageLogEvent.SpellHit,
            Unit = attacker,
            Target = target,
            Damage = damage,
            SpellType = type,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterSpellMiss(Unit attacker, Unit target, SpellTypes type, float odds)
    {
        events.Add(new SpellLog
        {
            Type = MessageLogEvent.SpellMiss,
            Unit = attacker,
            Target = target,
            SpellType = type,
            Odds = odds
        });
        UpdateListing();
    }

    public void RegisterSpellKill(Unit attacker, Unit target, SpellTypes type)
    {
        events.Add(new SpellLog
        {
            Type = MessageLogEvent.SpellKill,
            Unit = attacker,
            Target = target,
            SpellType = type
        });
        UpdateListing();
    }


    public void RegisterKill(Unit Attacker, Unit Defender, Weapon weapon)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Kill,
            Unit = Attacker,
            Target = Defender,
            Weapon = weapon
        });
        UpdateListing();
    }

    public void RegisterDigest(Unit predator, Unit prey, PreyLocation loc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Digest,
            Unit = predator,
            Target = prey,
            preyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterAbsorb(Unit predator, Unit prey, PreyLocation loc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Absorb,
            Unit = predator,
            Target = prey,
            preyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterPartialEscape(Unit predator, Unit prey, PreyLocation loc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.PartialEscape,
            Unit = predator,
            Target = prey,
            preyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterEscape(Unit predator, Unit prey, PreyLocation loc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Escape,
            Unit = predator,
            Target = prey,
            preyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterFreed(Unit predator, Unit prey, PreyLocation loc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Freed,
            Unit = predator,
            Target = prey,
            preyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterRegurgitated(Unit predator, Unit prey, PreyLocation loc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Regurgitated,
            Unit = predator,
            Target = prey,
            preyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterHeal(Unit unit, int[] amount, string type = "absorb", string extra = "none")
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Heal,
            Unit = unit,
            Damage = amount[0],
            Bonus = amount[1],
            Message = type,
            Extra = extra
        });
        UpdateListing();
    }

    public void RegisterNearDigestion(Unit predator, Unit prey, PreyLocation loc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.LowHealth,
            Unit = predator,
            Target = prey,
            preyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterCurseExpiration(Unit predator, Unit prey, PreyLocation loc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.CurseExpires,
            Unit = predator,
            Target = prey,
            preyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterDiminishmentExpiration(Unit predator, Unit prey, PreyLocation loc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.DiminishmentExpires,
            Unit = predator,
            Target = prey,
            preyLocation = loc,
        });
        UpdateListing();
    }

    public void LogDigestionRandom(Unit predator, Unit prey, PreyLocation loc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.RandomDigestion,
            Unit = predator,
            Target = prey,
            preyLocation = loc,
        });
        UpdateListing();
    }

    public void LogGreatEscapeKeep(Unit predator, Unit prey, PreyLocation loc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.GreatEscapeKeep,
            Unit = predator,
            Target = prey,
            preyLocation = loc,
        });
        UpdateListing();
    }

    public void LogGreatEscapeFlee(Unit predator, Unit prey, PreyLocation loc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.GreatEscapeFlee,
            Unit = predator,
            Target = prey,
            preyLocation = loc,
        });
        UpdateListing();
    }

    public void RegisterNewTurn(string name, int amount)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.NewTurn,
            Message = $"Turn {amount} - {name}"
        });
        UpdateListing();
    }

    public void RegisterMiscellaneous(string str)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.Miscellaneous,
            Message = str,
        });
        UpdateListing();
    }

    public void RegisterRegurgitate(Unit predator, Unit prey, PreyLocation loc)
    {
        events.Add(new EventLog
        {
            Type = MessageLogEvent.ManualRegurgitation,
            Unit = predator,
            Target = prey,
            preyLocation = loc,
        });
        UpdateListing();
    }

}
