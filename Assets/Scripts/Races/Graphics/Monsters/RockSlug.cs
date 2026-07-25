using System.Collections.Generic;
using UnityEngine;

class RockSlug : BlankSlate
{
    readonly Sprite[] Sprites = SpriteDictionary.RockSlug;

    public RockSlug()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SlugSkin);
        GentleAnimation = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(5, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor));
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor)); // tail end
        Belly = new SpriteExtraInfo(3, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor));
        BodySize = new SpriteExtraInfo(2, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor)); // belly cover up

        clothingColors = 0;
    }

    protected override Sprite BodySprite(Actor_Unit actor) => Sprites[3];

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking)
            return Sprites[1];
        else if (actor.IsOralVoring)
            return Sprites[2];
        return Sprites[0];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites[4];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            return Sprites[18];
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            return Sprites[6 + actor.GetStomachSize(11)];
        return Sprites[6 + actor.GetStomachSize(11)];
    }

    protected override Sprite BodySizeSprite(Actor_Unit actor) // belly cover up
    {
        if (actor.HasBelly == false)
            return Sprites[5];
        return null;
    }

}
