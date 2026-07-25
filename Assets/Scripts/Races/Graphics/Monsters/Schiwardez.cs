using System.Collections.Generic;
using UnityEngine;

class Schiwardez : BlankSlate
{
    public Schiwardez()
    {
        GentleAnimation = true;
        CanBeGender = new List<Gender>() { Gender.Male };
        SkinColors = ColorMap.SchiwardezColorCount;
        Body = new SpriteExtraInfo(4, BodySprite, (s) => ColorMap.GetSchiwardezColor(s.Unit.SkinColor)); // Body
        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, (s) => ColorMap.GetSchiwardezColor(s.Unit.SkinColor)); // Rear Closer Legs
        BodyAccent2 = new SpriteExtraInfo(0, BodyAccentSprite2, (s) => ColorMap.GetSchiwardezColor(s.Unit.SkinColor)); // Far Legs
        BodyAccent3 = new SpriteExtraInfo(2, BodyAccentSprite3, (s) => ColorMap.GetSchiwardezColor(s.Unit.SkinColor)); // Sheath
        BodyAccent4 = new SpriteExtraInfo(7, BodyAccentSprite4, (s) => ColorMap.GetSchiwardezColor(s.Unit.SkinColor)); // Tail
        BodyAccent5 = new SpriteExtraInfo(8, BodyAccentSprite5, WhiteColored); // Mouth
        BodyAccent6 = new SpriteExtraInfo(6, BodyAccentSprite6, (s) => ColorMap.GetSchiwardezColor(s.Unit.SkinColor)); // Front Closer Leg
        BodyAccent7 = new SpriteExtraInfo(5, BodyAccentSprite7, (s) => ColorMap.GetSchiwardezColor(s.Unit.SkinColor)); // Belly
        Balls = new SpriteExtraInfo(1, BallsSprite, (s) => ColorMap.GetSchiwardezColor(s.Unit.SkinColor)); // Balls
        Dick = new SpriteExtraInfo(3, DickSprite, WhiteColored); // Dick
        Head = new SpriteExtraInfo(9, HeadSprite, (s) => ColorMap.GetSchiwardezColor(s.Unit.SkinColor)); // Head
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Balls, -125 * .5f, 0);
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {
        if (actor.GetBallSize(24) > 17) return SpriteDictionary.Schiwardez[1];

        return SpriteDictionary.Schiwardez[0];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Tail
    {
        if (actor.GetBallSize(24) > 17) return SpriteDictionary.Schiwardez[36];
        if (actor.GetBallSize(24) > 14) return SpriteDictionary.Schiwardez[35];
        if (actor.GetBallSize(24) > 12) return SpriteDictionary.Schiwardez[34];

        return SpriteDictionary.Schiwardez[33];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Rear Closer Leg
    {
        return SpriteDictionary.Schiwardez[3];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Far Legs
    {
        return SpriteDictionary.Schiwardez[2];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Sheath
    {
        return SpriteDictionary.Schiwardez[8];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Mouth
    {
        if (actor.IsAttacking || actor.IsOralVoring) return SpriteDictionary.Schiwardez[38];
        return null;
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Front Closer Leg
    {
        return SpriteDictionary.Schiwardez[39];
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Belly (Added by Tatltuae)
    {
        if (actor.HasBelly == true && (actor.GetBallSize(24) > 17))
        {
            return SpriteDictionary.Schiwardez[59 + (actor.GetStomachSize(20))];
        }
        else if (actor.HasBelly == true)
        {
            return SpriteDictionary.Schiwardez[39 + (actor.GetStomachSize(20))];
        }
        else
            return null;
    }

    protected override Sprite DickSprite(Actor_Unit actor) // Dick
    {
        if (actor.IsCockVoring) return SpriteDictionary.Schiwardez[7];
        if (actor.IsErect()) return SpriteDictionary.Schiwardez[6];
        return null;
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (actor.IsAttacking || actor.IsOralVoring) return SpriteDictionary.Schiwardez[37];
        if (actor.GetBallSize(24) > 0) return SpriteDictionary.Schiwardez[5];
        return SpriteDictionary.Schiwardez[4];
    }

    protected override Sprite BallsSprite(Actor_Unit actor) // Balls
    {
        if (actor.GetBallSize(24) == 0 && Config.HideCocks == false) return SpriteDictionary.Schiwardez[9];

        int size = actor.GetBallSize(24);

        if (size == 24 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Schiwardez[32];
        }

        else if (size >= 23 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Schiwardez[31];
        }

        else if (size >= 21 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Schiwardez[30];
        }

        else if (size >= 19 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Schiwardez[29];
        }

        if (size > 18) size = 18;

        return SpriteDictionary.Schiwardez[8 + size];
    }

}
