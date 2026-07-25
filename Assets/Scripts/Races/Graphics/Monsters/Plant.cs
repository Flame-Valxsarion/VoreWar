using System.Collections.Generic;
using UnityEngine;

class Plant : BlankSlate
{
    readonly Sprite[] Sprites = SpriteDictionary.Plant;
    public Plant()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.PlantSkin); // Body color
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.PlantSkin); // Face color
        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PlantSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(4, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PlantSkin, s.Unit.SkinColor));
        Mouth = new SpriteExtraInfo(5, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PlantSkin, s.Unit.AccessoryColor)); // Face
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PlantSkin, s.Unit.SkinColor));
        Belly = new SpriteExtraInfo(3, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PlantSkin, s.Unit.SkinColor));
        EyeTypes = 10;
    }

    protected override Sprite BodySprite(Actor_Unit actor) => Sprites[21];

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[1];
        return Sprites[0];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        int headType = (actor.Unit.EyeType * 2) + 1;
        if (actor.Unit.EyeType == 0)
        {
            if (actor.IsAttacking || actor.IsEating)
                return Sprites[2];
            return null;
        }
        if (actor.IsAttacking || actor.IsEating)
            headType++;
        return Sprites[headType];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        return Sprites[22];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false) && actor.GetStomachSize(15, 1) == 15)
            return SpriteDictionary.Plant[32];
        return SpriteDictionary.Plant[22 + actor.GetStomachSize(8)];
    }
}
