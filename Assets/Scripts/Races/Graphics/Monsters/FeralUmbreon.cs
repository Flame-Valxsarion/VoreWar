using System.Collections.Generic;
using UnityEngine;

class FeralUmbreon : BlankSlate
{
    internal FeralUmbreon()
    {
        CanBeGender = new List<Gender>() { Gender.Male, Gender.Female };
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UmbreonSkin);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UmbreonSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UmbreonSkin);
        GentleAnimation = true;

        Body = new SpriteExtraInfo(0, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.AccessoryColor)); //Ring
        Mouth = new SpriteExtraInfo(8, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.AccessoryColor));
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.SkinColor)); // legs
        BodyAccent2 = new SpriteExtraInfo(4, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.SkinColor)); // back legs
        BodyAccent3 = new SpriteExtraInfo(4, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.SkinColor));
        BodyAccent4 = null;
        BodyAccent5 = null;
        BodyAccent6 = null;
        Head = null;
        Eyes = new SpriteExtraInfo(7, EyesSprite, WhiteColored);
        SecondaryEyes = new SpriteExtraInfo(7, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.EyeColor));
        Belly = new SpriteExtraInfo(2, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UmbreonSkin, s.Unit.SkinColor));
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {

    }

    internal override void RandomCustom(Unit unit)
    {
        unit.SkinColor = State.Rand.Next(SkinColors);
        unit.AccessoryColor = State.Rand.Next(AccessoryColors);
        unit.EyeColor = State.Rand.Next(EyeColors);
    }
    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (!actor.HasBelly)
            return State.GameManager.SpriteDictionary.FeralUmbreon[0];

        return State.GameManager.SpriteDictionary.FeralUmbreon[1];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => actor.HasBelly? State.GameManager.SpriteDictionary.FeralUmbreon[2] : null;
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => actor.HasBelly? State.GameManager.SpriteDictionary.FeralUmbreon[3] : null;  
    protected override Sprite BodyAccentSprite3(Actor_Unit actor) => actor.IsBeingRubbed? State.GameManager.SpriteDictionary.FeralUmbreon[7] : null;  

    protected override Sprite MouthSprite(Actor_Unit actor) => (actor.IsAttacking || actor.IsEating) ? State.GameManager.SpriteDictionary.FeralUmbreon[5] : null;
    protected override Sprite EyesSprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.FeralUmbreon[(actor.Unit.IsDead ? 10 : (actor.IsBeingRubbed ? 11 : 9))];
    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => (actor.Unit.IsDead ? null : (actor.IsBeingRubbed ? null : State.GameManager.SpriteDictionary.FeralUmbreon[6]));
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        int size = actor.GetStomachSize(8);

        if (!actor.HasBelly)
            return null;

        if (size >= 8 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralUmbreon[19];
        }

        if (size >= 7 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralUmbreon[18];
        }

        if (size >= 6 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralUmbreon[17];
        }

        if (size > 4) size = 4;

        return State.GameManager.SpriteDictionary.FeralUmbreon[12 + size];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.FeralUmbreon[4];
}

