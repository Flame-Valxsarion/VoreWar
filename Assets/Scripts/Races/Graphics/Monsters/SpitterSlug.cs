using System.Collections.Generic;
using UnityEngine;

class SpitterSlug : BlankSlate
{
    readonly Sprite[] Sprites = SpriteDictionary.SpitterSlug;

    public SpitterSlug()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SlugSkin);
        GentleAnimation = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(5, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor));
        BodyAccent = new SpriteExtraInfo(10, BodyAccentSprite, WhiteColored); // slime
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor)); // tail end
        Belly = new SpriteExtraInfo(3, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor));
        BodySize = new SpriteExtraInfo(2, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor)); // belly cover up

        clothingColors = 0;
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring || actor.IsAttacking)
            return Sprites[1];
        return Sprites[0];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking)
            return Sprites[3];
        else if (actor.IsOralVoring)
            return Sprites[4];
        return Sprites[2];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // slime
    {
        if (actor.IsAttacking)
            return Sprites[7];
        else if (actor.IsOralVoring)
            return Sprites[8];
        return Sprites[6];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites[23];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            return Sprites[21];
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            return Sprites[11 + actor.GetStomachSize(9)];
        return Sprites[11 + actor.GetStomachSize(9)];
    }

    protected override Sprite BodySizeSprite(Actor_Unit actor) // belly cover up
    {
        if (actor.HasBelly == false)
            return Sprites[22];
        return null;
    }

}
