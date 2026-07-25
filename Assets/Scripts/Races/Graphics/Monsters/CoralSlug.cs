using System.Collections.Generic;
using UnityEngine;

class CoralSlug : BlankSlate
{
    readonly Sprite[] Sprites = SpriteDictionary.CoralSlug;

    public CoralSlug()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SlugSkin);
        GentleAnimation = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(5, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor));
        BodyAccent = new SpriteExtraInfo(10, BodyAccentSprite, WhiteColored); // acid
        Belly = new SpriteExtraInfo(3, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor));
        BodySize = new SpriteExtraInfo(2, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor)); // belly cover up

        clothingColors = 0;
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.HasBelly == false)
            return Sprites[5];
        else if (actor.GetStomachSize(9) < 3)
            return Sprites[5];
        else if (actor.GetStomachSize(9) >= 3 && actor.GetStomachSize(9) < 5)
            return Sprites[6];
        else if (actor.GetStomachSize(9) >= 5 && actor.GetStomachSize(9) < 7)
            return Sprites[7];
        else if (actor.GetStomachSize(9) >= 7 && actor.GetStomachSize(9) < 9)
            return Sprites[8];
        else
            return Sprites[9];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking)
            return Sprites[1];
        else if (actor.IsOralVoring)
            return Sprites[2];
        return Sprites[0];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking)
            return Sprites[3];
        else
            return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            return Sprites[20];
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            return Sprites[10 + actor.GetStomachSize(9)];
        return Sprites[10 + actor.GetStomachSize(9)];
    }

    protected override Sprite BodySizeSprite(Actor_Unit actor) // belly cover up
    {
        if (actor.HasBelly == false)
            return Sprites[4];
        return null;
    }

}
