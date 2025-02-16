using System.Collections.Generic;
using UnityEngine;

class BoomBunnies : DefaultRaceData
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.BoomBunnies;
    internal BoomBunnies()
    {
        CanBeGender = new List<Gender>() { Gender.Female, Gender.Male };
        SpecialAccessoryCount = 0;
        ClothingShift = new Vector3(0, 0, 0);
        AvoidedEyeTypes = 2;
        AvoidedMouthTypes = 1;

        HairColors = 1;
        HairStyles = 1;
        SkinColors = 1;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
        EyeTypes = 9;
        EyeColors = 1;
        SecondaryEyeColors = 1;
        BodySizes = 0;
        AllowedMainClothingTypes = new List<MainClothing>();
        AllowedWaistTypes = new List<MainClothing>();
        AllowedClothingHatTypes = new List<ClothingAccessory>();
        MouthTypes = 8;
        AvoidedMainClothingTypes = 0;

        ExtendedBreastSprites = false;

        Body = new SpriteExtraInfo(3, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.AccessoryColor));//Hind legs+belly

        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.AccessoryColor));
        BodyAccessory = null;
        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.AccessoryColor));//fore legs;for punching
        BodyAccent2 = new SpriteExtraInfo(4, BodyAccentSprite2, WhiteColored);//Fuse
        BodyAccent3 = new SpriteExtraInfo(6, BodyAccentSprite3, WhiteColored);//Ears
        BodyAccent4 = new SpriteExtraInfo(4, BodyAccentSprite4, WhiteColored);//Cotton ball
        BodyAccent5 = null;
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        BodyAccent9 = null;
        BodyAccent10 = null;
        Mouth = new SpriteExtraInfo(10, MouthSprite, WhiteColored);
        Eyes = new SpriteExtraInfo(10, EyesSprite, WhiteColored);
        Hair = null;
        Hair2 = null;
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = null;
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

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        return Sprites[4 + (actor.GetStomachSize(10))];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[1];
        return Sprites[0];
    }
    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking)
            return Sprites[3];
        return Sprites[2];
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return null;
        return Sprites[15 + actor.Unit.EyeType];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[33];
        return Sprites[24 + actor.Unit.MouthType];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        return Sprites[32];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        return Sprites[34 + (actor.GetStomachSize(10))];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[46];
        return Sprites[45];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => null;
    protected override Sprite BackWeaponSprite(Actor_Unit actor) => null;
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) => null;
    protected override Sprite SecondaryBellySprite(Actor_Unit actor) => null;
    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsShadowSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsSprite(Actor_Unit actor) => null;
    internal override Color ClothingColor(Actor_Unit actor) => Color.white;
    protected override Sprite DickSprite(Actor_Unit actor) => null;
    protected override Color EyeColor(Actor_Unit actor) => Color.white;
    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => null;
    protected override Color HairColor(Actor_Unit actor) => Color.white;
    protected override Sprite HairSprite(Actor_Unit actor) => null;
    protected override Sprite HairSprite2(Actor_Unit actor) => null;
    protected override Color ScleraColor(Actor_Unit actor) => Color.white;
    protected override Sprite WeaponSprite(Actor_Unit actor) => null;
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => null;


}

