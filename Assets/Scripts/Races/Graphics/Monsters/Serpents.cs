using System;
using System.Collections.Generic;
using UnityEngine;

class Serpents : BlankSlate
{
    public Serpents()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
        EyeTypes = 4;
        AvoidedEyeTypes = 1;
        GentleAnimation = true;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(9, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, s.Unit.SkinColor));
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, s.Unit.SkinColor));
        Mouth = new SpriteExtraInfo(10, MouthSprite, WhiteColored);
        Eyes = new SpriteExtraInfo(5, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryAccessory = new SpriteExtraInfo(1, SecondaryAccessorySprite, WhiteColored);
        Belly = new SpriteExtraInfo(8, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, s.Unit.SkinColor));
        Weapon = null;
        BodySize = new SpriteExtraInfo(7, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.SkinColor));
    }


    protected override Sprite BodySprite(Actor_Unit actor) => SpriteDictionary.Serpents[(actor.IsAttacking || actor.IsEating) ? 2 : 0];
    protected override Sprite BodyAccentSprite(Actor_Unit actor) => SpriteDictionary.Serpents[(actor.IsAttacking || actor.IsEating) ? 3 : 1];

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.GetSimpleBodySprite() == 0 && actor.Targetable)
        {
            if (State.Rand.Next(400) == 0)
                actor.SetAnimationMode(2, .25f);
        }
        int animationFrame = actor.CheckAnimationFrame();
        if (animationFrame == 2)
            return SpriteDictionary.Serpents[6];
        else if (animationFrame == 1)
            return SpriteDictionary.Serpents[7];
        return actor.IsEating ? SpriteDictionary.Serpents[5] : null;
    }
    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        return actor.IsEating ? SpriteDictionary.Serpents[4] : null;
    }

    protected override Sprite BodySizeSprite(Actor_Unit actor)
    {
        if (actor.HasBelly == false)
            return null;
        return SpriteDictionary.Serpents[8 + actor.GetStomachSize(3, 3)];

    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        return SpriteDictionary.Serpents[12 + actor.GetStomachSize(3, 3)];
    }


    protected override Sprite EyesSprite(Actor_Unit actor) => SpriteDictionary.Serpents[Math.Min(16 + actor.Unit.EyeType, 19)];

}

