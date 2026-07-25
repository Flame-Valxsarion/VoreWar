using System.Collections.Generic;
using UnityEngine;

class BlankSlate : DefaultRaceData
{
    internal BlankSlate()
    {
        SpecialAccessoryCount = 0;
        ClothingShift = new Vector3(0, 0, 0);
        AvoidedEyeTypes = 0;
        AvoidedMouthTypes = 0;

        HairColors = 1;
        HairStyles = 1;
        SkinColors = 1;
        AccessoryColors = 1;
        EyeTypes = 1;
        EyeColors = 1;
        SecondaryEyeColors = 1;
        BodySizes = 0;
        AllowedMainClothingTypes = new List<MainClothing>();
        AllowedWaistTypes = new List<MainClothing>();
        AllowedClothingHatTypes = new List<ClothingAccessory>();
        MouthTypes = 0;
        AvoidedMainClothingTypes = 0;

        ExtendedBreastSprites = false;

        Body = null;
        Head = null;
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
        BodyAccent11 = null;
        BodyAccent12 = null;
        BodyAccent13 = null;
        BodyAccent14 = null;
        BodyAccent15 = null;
        BodyAccent16 = null;
        BodyAccent17 = null;
        BodyAccent18 = null;
        BodyAccent19 = null;
        BodyAccent20 = null;
        Mouth = null;
        Hair = null;
        Hair2 = null;
        Eyes = null;
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

    protected override Sprite AccessorySprite(Actor_Unit actor) => null;
    protected override Sprite BackWeaponSprite(Actor_Unit actor) => null;
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) => null;
    protected override Sprite SecondaryBellySprite(Actor_Unit actor) => null;
    protected override Sprite BodyAccentSprite(Actor_Unit actor) => null;
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => null;
    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BodySprite(Actor_Unit actor) => null;
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
    protected override Sprite MouthSprite(Actor_Unit actor) => null;
    protected override Color ScleraColor(Actor_Unit actor) => Color.white;
    protected override Sprite WeaponSprite(Actor_Unit actor) => null;
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => null;


}

