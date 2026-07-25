using System.Collections.Generic;
using UnityEngine;

class ViraeUltimae : DefaultRaceData
{
    internal ViraeUltimae()
    {
        SpecialAccessoryCount = 0;
        ClothingShift = new Vector3(0, 0, 0);
        AvoidedEyeTypes = 0;
        AvoidedMouthTypes = 0;

        HairColors = 1;
        HairStyles = 1;
        EyeTypes = 1;
        EyeColors = 1;
        SecondaryEyeColors = 1;
        BodySizes = 0;
        AllowedMainClothingTypes = new List<MainClothing>();
        AllowedWaistTypes = new List<MainClothing>();
        AllowedClothingHatTypes = new List<ClothingAccessory>();
        MouthTypes = 0;
        AvoidedMainClothingTypes = 0;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AabayxSkin); // Head color
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AabayxSkin);// Body color
        GentleAnimation = true;
        WeightGainDisabled = true;
        CanBeGender = new List<Gender>() { Gender.None };

        ExtendedBreastSprites = false;

        Body = new SpriteExtraInfo(3, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(5, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, s.Unit.AccessoryColor));
        BodyAccessory = null;
        BodyAccent = null;
        BodyAccent2 = null;
        BodyAccent3 = null;
        BodyAccent4 = null;
        BodyAccent5 = null;
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        BodyAccent9 = null;
        BodyAccent10 = null;
        Mouth = new SpriteExtraInfo(7, MouthSprite, WhiteColored);
        Hair = null;
        Hair2 = null;
        Eyes = null;
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(9, null, null,  (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, s.Unit.SkinColor));
        SecondaryBelly = null;
        Weapon = null;
        BackWeapon = null;
        BodySize = null;
        Breasts = null;
        BreastShadow = null;
        Dick = null;
        Balls = null;
    }
    internal override int BreastSizes => 1;
    internal override int DickSizes => 1;

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
    }

    protected override Sprite BodySprite(Actor_Unit actor) => SpriteDictionary.ViraeUltimae[2 + (actor.IsAttacking ? 1 : 0)];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        return actor.HasBelly ? SpriteDictionary.ViraeUltimae[4 + actor.GetStomachSize(27)] : null;
    }

    protected override Sprite HeadSprite(Actor_Unit actor) => SpriteDictionary.ViraeUltimae[0];

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return SpriteDictionary.ViraeUltimae[1];
        else return null;
    }


    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite AccessorySprite(Actor_Unit actor) => null;
    protected override Sprite BackWeaponSprite(Actor_Unit actor) => null;
    protected override Sprite SecondaryBellySprite(Actor_Unit actor) => null;
    protected override Sprite BodyAccentSprite(Actor_Unit actor) => null;
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => null;
    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsShadowSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsSprite(Actor_Unit actor) => null;
    internal override Color ClothingColor(Actor_Unit actor) => Color.white;
    protected override Sprite DickSprite(Actor_Unit actor) => null;
    protected override Color EyeColor(Actor_Unit actor) => Color.white;
    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => null;
    protected override Sprite EyesSprite(Actor_Unit actor) => null;
    protected override Color HairColor(Actor_Unit actor) => Color.white;
    protected override Sprite HairSprite(Actor_Unit actor) => null;
    protected override Sprite HairSprite2(Actor_Unit actor) => null;
    protected override Color ScleraColor(Actor_Unit actor) => Color.white;
    protected override Sprite WeaponSprite(Actor_Unit actor) => null;
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => null;


}

