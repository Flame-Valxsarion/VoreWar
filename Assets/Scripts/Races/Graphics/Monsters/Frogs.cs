using System.Collections.Generic;
using UnityEngine;

class Frogs : BlankSlate
{
    enum Position
    {
        Front,
        Pouncing,
        Standing
    }
    Position position;
    readonly Sprite[] Sprites = SpriteDictionary.Frogs;


    public Frogs()
    {

        CanBeGender = new List<Gender>() { Gender.Female };
        Body = new SpriteExtraInfo(3, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Frog, s.Unit.AccessoryColor));
        Eyes = new SpriteExtraInfo(4, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        Mouth = new SpriteExtraInfo(4, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Frog, s.Unit.AccessoryColor));

        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Frog, s.Unit.AccessoryColor));
        BodyAccent2 = new SpriteExtraInfo(5, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Frog, s.Unit.AccessoryColor));
        BodyAccent3 = new SpriteExtraInfo(9, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Frog, s.Unit.AccessoryColor));
        BodyAccent4 = new SpriteExtraInfo(8, BodyAccentSprite4, WhiteColored);

        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Frog);

        EyeTypes = 4;
        MouthTypes = 4;
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.IsPouncingFrog)
            position = Position.Pouncing;
        else if (actor.IsEating)
            position = Position.Standing;
        else
            position = Position.Front;
        base.RunFirst(actor);
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        switch (position)
        {
            case Position.Front:
                return Sprites[0];
            case Position.Pouncing:
                return Sprites[32];
            case Position.Standing:
                return Sprites[10];
        }
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        switch (position)
        {
            case Position.Front:
                return Sprites[1 + actor.Unit.EyeType];
            case Position.Pouncing:
                return null;
            case Position.Standing:
                return Sprites[17 + actor.Unit.EyeType];
        }
        return null;
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        switch (position)
        {
            case Position.Front:
                return Sprites[5 + actor.Unit.MouthType];
            case Position.Pouncing:
                return null;
            case Position.Standing:
                return Sprites[21 + actor.Unit.MouthType];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) //Belly
    {
        switch (position)
        {
            case Position.Front:
                return null;
            case Position.Pouncing:
                return null;
            case Position.Standing:
                if (actor.HasBelly)
                {
                    return Sprites[13 + actor.GetStomachSize(3, .75f)];
                }
                break;
        }
        return null;
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) //Butt
    {
        switch (position)
        {
            case Position.Front:
                if (actor.HasBelly)
                {
                    return Sprites[33 + actor.GetStomachSize(3, .75f)];
                }
                break;
            case Position.Pouncing:
                if (actor.HasBelly)
                {
                    return Sprites[27 + actor.GetStomachSize(3, .75f)];
                }
                break;
            case Position.Standing:
                if (actor.HasBelly)
                {
                    return Sprites[11 + actor.GetStomachSize(2, .75f)];
                }
                break;
        }
        return null;
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) //Swallow/Type
    {
        if (actor.IsOralVoring == false)
            return null;
        if (actor.IsOralVoringHalfOver == false)
            return null;
        switch (position)
        {
            case Position.Front:
                if (actor.HasBelly)
                {
                    return Sprites[9];
                }
                break;
            case Position.Pouncing:
                return null;
            case Position.Standing:
                if (actor.HasBelly)
                {
                    return Sprites[26];
                }
                break;
        }
        return null;
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) //Tongue
    {
        if (position == Position.Front && actor.IsAttacking)
            return Sprites[38];
        if (actor.IsOralVoring == false)
            return null;
        if (actor.IsOralVoringHalfOver)
            return null;
        switch (position)
        {
            case Position.Front:
                if (actor.HasBelly)
                {
                    return Sprites[38];
                }
                break;
            case Position.Pouncing:
                return null;
            case Position.Standing:
                if (actor.HasBelly && actor.GetStomachSize(3, .75f) != 3)
                {
                    return Sprites[25];
                }
                break;
        }
        return null;
    }
}

