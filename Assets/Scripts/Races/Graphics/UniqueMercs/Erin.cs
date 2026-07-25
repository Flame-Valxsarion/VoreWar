using System.Collections.Generic;
using UnityEngine;

class Erin : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Erin;
    internal Erin()
    {

        CanBeGender = new List<Gender>() { Gender.Female };
        SpecialAccessoryCount = 0;
        ClothingShift = new Vector3(0, 0, 0);
        AvoidedEyeTypes = 0;
        AvoidedMouthTypes = 0;

        clothingColors = 1;

        HairColors = 1;
        HairStyles = 1;
        SkinColors = 1;
        AccessoryColors = 1;
        EyeTypes = 1;
        EyeColors = 1;
        SecondaryEyeColors = 1;
        BodySizes = 0;
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new ErinTop(),
        };
        AllowedWaistTypes = new List<MainClothing>() //Bottoms
        {
            new ErinSkirt(),
        };
        ExtraMainClothing1Types = new List<MainClothing>() //Over
        {
            new ErinPantie(),
        };
        ExtraMainClothing2Types = new List<MainClothing>() //Stocking
        {
            new ErinStocking(),
        };

        ExtraMainClothing3Types = new List<MainClothing>() //Hat
        {
            new ErinShoes(),
        };

        AllowedClothingHatTypes = new List<ClothingAccessory>();

        MouthTypes = 0;
        AvoidedMainClothingTypes = 0;

        ExtendedBreastSprites = false;

        Body = new SpriteExtraInfo(5, BodySprite, WhiteColored);
        BodyAccessory = new SpriteExtraInfo(2, AccessorySprite, WhiteColored);
        Head = new SpriteExtraInfo(6, HeadSprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, WhiteColored);
        BodyAccent2 = new SpriteExtraInfo(7, BodyAccentSprite2, WhiteColored);
        BodyAccent3 = null;
        BodyAccent4 = null;
        BodyAccent5 = null;
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        BodyAccent9 = null;
        BodyAccent10 = null;
        Mouth = null;
        Hair = new SpriteExtraInfo(13, HairSprite, WhiteColored);
        Hair2 = new SpriteExtraInfo(1, HairSprite2, WhiteColored);
        Eyes = null;
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = null;
        Weapon = null;
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(8, BreastsSprite, WhiteColored);
        SecondaryBreasts = new SpriteExtraInfo(8, SecondaryBreastsSprite, WhiteColored);
        BreastShadow = null;
        Dick = null;
        Balls = null;
    }
    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Erin";
        unit.ClothingAccessoryType = State.Rand.Next(2);
    }
    internal override int BreastSizes => 1;
    internal override int DickSizes => 1;

    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        return Sprites[7];
    }
    protected override Sprite BackWeaponSprite(Actor_Unit actor) => null;
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) => null;
    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        return Sprites[5];
    }
    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        return Sprites[6];
    }
    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsAttacking)
            return Sprites[1];
        return Sprites[0];
    }
    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[3];
        if (actor.Unit.IsDead && actor.Unit.Items != null) //Second part checks for a not fully initialized unit, so that she doesn't have the dead face when you view her race info
            return Sprites[4];
        else
            return Sprites[2];
    }
    protected override Sprite BreastsShadowSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        return Sprites[20];
    }
    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        return Sprites[21];
    }
    internal override Color ClothingColor(Actor_Unit actor) => Color.white;
    protected override Sprite DickSprite(Actor_Unit actor) => null;
    protected override Color EyeColor(Actor_Unit actor) => Color.white;
    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => null;
    protected override Sprite EyesSprite(Actor_Unit actor) => null;
    protected override Color HairColor(Actor_Unit actor) => Color.white;
    protected override Sprite HairSprite(Actor_Unit actor)
    {
        return Sprites[8];
    }
    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        return Sprites[9];
    }
    protected override Sprite MouthSprite(Actor_Unit actor) => null;
    protected override Color ScleraColor(Actor_Unit actor) => Color.white;
    protected override Sprite WeaponSprite(Actor_Unit actor) => null;
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => null;


}

class ErinTop : MainClothing
{
    public ErinTop()
    {
        coversBreasts = false;
        blocksDick = false;
        clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
        clothing2 = new SpriteExtraInfo(11, null, WhiteColored);
        Type = 9764;
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        if (actor.IsAttacking)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Erin[15];
        }
        else
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Erin[14];
        }
        clothing2.GetSprite = (s) => SpriteDictionary.Erin[16];

        base.Configure(sprite, actor);
    }

}
class ErinPantie : MainClothing
{
    public ErinPantie()
    {
        coversBreasts = false;
        blocksDick = true;
        clothing1 = new SpriteExtraInfo(9, null, WhiteColored);
        Type = 9764;
    }
    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        clothing1.GetSprite = (s) => SpriteDictionary.Erin[13];
        base.Configure(sprite, actor);


    }


}
class ErinSkirt : MainClothing
{
    public ErinSkirt()
    {
        coversBreasts = false;
        blocksDick = true;
        clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
        Type = 9764;
    }
    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        clothing1.GetSprite = (s) => SpriteDictionary.Erin[12];
        base.Configure(sprite, actor);


    }


}
class ErinStocking : MainClothing
{
    public ErinStocking()
    {
        coversBreasts = false;
        blocksDick = false;
        clothing1 = new SpriteExtraInfo(9, null, WhiteColored);
        Type = 9764;
    }
    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        clothing1.GetSprite = (s) => SpriteDictionary.Erin[11];
        base.Configure(sprite, actor);


    }


}
class ErinShoes : MainClothing
{
    public ErinShoes()
    {
        coversBreasts = false;
        blocksDick = false;
        clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
        Type = 9764;
    }
    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        clothing1.GetSprite = (s) => SpriteDictionary.Erin[10];
        base.Configure(sprite, actor);


    }


}
