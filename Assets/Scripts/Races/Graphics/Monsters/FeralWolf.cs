using System.Collections.Generic;
using UnityEngine;

class FeralWolf : BlankSlate
{
    public FeralWolf()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        SkinColors = 6;
        HairColors = 6;
        GentleAnimation = true;

        Body = new SpriteExtraInfo(0, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfFur, s.Unit.SkinColor));
        Hair = new SpriteExtraInfo(1, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfMane, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(6, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfMane, s.Unit.HairColor));
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfFur, s.Unit.SkinColor));
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, WhiteColored);
        BodyAccent3 = new SpriteExtraInfo(2, BodyAccentSprite3, WhiteColored);
        Head = new SpriteExtraInfo(7, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfFur, s.Unit.SkinColor));
        Eyes = new SpriteExtraInfo(7, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfMane, s.Unit.HairColor));
        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfFur, s.Unit.SkinColor));
        Belly = new SpriteExtraInfo(2, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfFur, s.Unit.SkinColor));

    }

    protected override Sprite BodySprite(Actor_Unit actor) => SpriteDictionary.FeralWolf[4];
    protected override Sprite HairSprite(Actor_Unit actor) => SpriteDictionary.FeralWolf[6];
    protected override Sprite HairSprite2(Actor_Unit actor) => SpriteDictionary.FeralWolf[7];
    protected override Sprite BodyAccentSprite(Actor_Unit actor) => SpriteDictionary.FeralWolf[5];
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => SpriteDictionary.FeralWolf[8];
    protected override Sprite BodyAccentSprite3(Actor_Unit actor) => SpriteDictionary.FeralWolf[9];
    protected override Sprite HeadSprite(Actor_Unit actor) => SpriteDictionary.FeralWolf[(actor.IsAttacking || actor.IsEating) ? 1 : 0];
    protected override Sprite EyesSprite(Actor_Unit actor) => SpriteDictionary.FeralWolf[(actor.IsAttacking || actor.IsEating) ? 3 : 2];
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false) && actor.GetStomachSize(15, 1) == 15)
            return SpriteDictionary.FeralWolf[16];
        return SpriteDictionary.FeralWolf[10 + actor.GetStomachSize(4)];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false) && actor.GetStomachSize(15, 1) == 15)
            return null;
        if (actor.GetStomachSize(4) == 4)
            return SpriteDictionary.FeralWolf[15];
        return null;
    }
}

