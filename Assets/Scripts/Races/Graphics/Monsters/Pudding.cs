using System.Collections.Generic;
using UnityEngine;

class Pudding : BlankSlate
{
    internal Pudding()
    {
        SpecialAccessoryCount = 3; // Top Type
        BodyAccentTypes1 = 5; // Cream
        BodyAccentTypes2 = 6; // Ears
        BodyAccentTypes3 = 8; // Topping
        MouthTypes = 6;
        EyeTypes = 8;

        CanBeGender = new List<Gender>() { Gender.None };
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.PuddingSkin);
        GentleAnimation = true;

        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PuddingSkin, s.Unit.SkinColor)); // Ears
        BodyAccent = new SpriteExtraInfo(5, BodyAccentSprite, null, null); // Top
        BodyAccent2 = new SpriteExtraInfo(9, BodyAccentSprite2, WhiteColored); // Topping
        BodyAccent3 = new SpriteExtraInfo(6, BodyAccentSprite3, WhiteColored); // Cream
        BodyAccent4 = new SpriteExtraInfo(12, BodyAccentSprite4, null, null); // Mouth2
        BodyAccent5 = new SpriteExtraInfo(13, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PuddingSkin, s.Unit.SkinColor)); // Mouth3
        Belly = new SpriteExtraInfo(2, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PuddingSkin, s.Unit.SkinColor));
        Mouth = new SpriteExtraInfo(10, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PuddingSkin, s.Unit.SkinColor));
        Eyes = new SpriteExtraInfo(21, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PuddingSkin, s.Unit.SkinColor));

    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        int offset = 0;
        switch (actor.GetStomachSize(25))
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
                break;
            case 8:
                offset = 3;
                break;
            case 9:
                offset = 5;
                break;
            case 10:
                offset = 10;
                break;
            case 11:
                offset = 17;
                break;
            case 12:
                offset = 23;
                break;
            case 13:
                offset = 28;
                break;
            case 14:
                offset = 32;
                break;
            case 15:
                offset = 37;
                break;
            case 16:
                offset = 40;
                break;
            case 17:
                offset = 45;
                break;
            case 18:
                offset = 48;
                break;
            case 19:
                offset = 53;
                break;
            case 20:
                offset = 56;
                break;
            case 21:
                offset = 59;
                break;
            case 22:
                offset = 62;
                break;
            case 23:
                offset = 66;
                break;
            case 24:
                offset = 69;
                break;
            default:
                break;
        }

        AddOffset(BodyAccessory, 0, offset * .625f);
        AddOffset(BodyAccent, 0, offset * .625f);
        AddOffset(BodyAccent2, 0, offset * .625f);
        AddOffset(BodyAccent3, 0, offset * .625f);
        AddOffset(BodyAccent4, 0, offset * .625f);
        AddOffset(BodyAccent5, 0, offset * .625f);
        AddOffset(Mouth, 0, offset * .625f);
        AddOffset(Eyes, 0, offset * .625f);
    }

    internal override void RandomCustom(Unit unit)
    {
        unit.SkinColor = State.Rand.Next(SkinColors);
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
        unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3);
        unit.SpecialAccessoryType = State.Rand.Next(SpecialAccessoryCount);
        unit.MouthType = State.Rand.Next(MouthTypes);
        unit.EyeType = State.Rand.Next(EyeTypes);
    }
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return State.GameManager.SpriteDictionary.Pudding[0];

        return State.GameManager.SpriteDictionary.Pudding[0 + actor.GetStomachSize(25)];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 2)
        {
            return State.GameManager.SpriteDictionary.Pudding[27];
        }
        return null;
    }
    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 0)
        {
            return State.GameManager.SpriteDictionary.Pudding[42 + actor.Unit.BodyAccentType2];
        }
        return null;
    }
    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType != 0 && actor.Unit.BodyAccentType3 >= 4)
        {
            return State.GameManager.SpriteDictionary.Pudding[52 + actor.Unit.BodyAccentType3 - 4];
        }
        return State.GameManager.SpriteDictionary.Pudding[56 + actor.Unit.BodyAccentType3];
    }
    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType1 == 4 || actor.Unit.SpecialAccessoryType == 0)
        {
            return null;
        }
        return State.GameManager.SpriteDictionary.Pudding[48 + actor.Unit.BodyAccentType1];

    }
    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
        {
            return State.GameManager.SpriteDictionary.Pudding[25];
        }
        return null;

    }
    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
        {
            return State.GameManager.SpriteDictionary.Pudding[26];
        }
        return null;

    }
    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
            return null;

        return State.GameManager.SpriteDictionary.Pudding[28 + actor.Unit.EyeType];
    }
    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
            return null;

        return State.GameManager.SpriteDictionary.Pudding[36 + actor.Unit.MouthType];
    }
}

