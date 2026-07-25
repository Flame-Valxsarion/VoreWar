using System.Collections.Generic;
using UnityEngine;

class Harpies : DefaultRaceData
{
    public Harpies()
    {
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);
        SpecialAccessoryCount = 3;
        BodySizes = 0;
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);
        ExtraColors2 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);
        ExtraColors3 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);

        BodyAccentTypes1 = 2;


        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        //Head = null;
        BodyAccessory = new SpriteExtraInfo(7, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        BodyAccent = new SpriteExtraInfo(7, BodyAccentSprite, WhiteColored);
        BodyAccent2 = new SpriteExtraInfo(3, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.ExtraColor1));
        BodyAccent3 = new SpriteExtraInfo(1, BodyAccentSprite3, WhiteColored);
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.HairColor));
        BodyAccent5 = new SpriteExtraInfo(0, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.ExtraColor2));
        //Mouth = null;
        Hair = new SpriteExtraInfo(6, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.HairColor));
        //Eyes = new SpriteExtraInfo(4, EyesSprite, null, (s) => ColorPaletteMap.EyeColorSwaps[s.Unit.EyeColor]);
        SecondaryEyes = null;
        SecondaryAccessory = new SpriteExtraInfo(5, SecondaryAccessorySprite, WhiteColored);
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(7, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = new SpriteExtraInfo(-1, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.ExtraColor3));
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));

        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));

        AvoidedMainClothingTypes = 1;
        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            ClothingTypes.BikiniTop,
            ClothingTypes.BeltTop,
            ClothingTypes.StrapTop,
            ClothingTypes.Leotard,
            ClothingTypes.BlackTop,
            ClothingTypes.Rags,
        };
        AllowedWaistTypes = new List<MainClothing>()
        {
            ClothingTypes.BikiniBottom,
            ClothingTypes.Loincloth,
            ClothingTypes.Shorts,
        };
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.GetSimpleBodySprite() != 0)
        {
            AddOffset(Head, 0, 10);
            AddOffset(Breasts, 0, 10);
            AddOffset(BodyAccessory, 0, 10);
            AddOffset(Eyes, 0, 10);
            AddOffset(Mouth, 0, 10);
            AddOffset(Belly, 0, 10);
            AddOffset(Dick, 0, 10);
            AddOffset(Balls, 0, 10);
            AddOffset(Hair, 0, 10);
            AddOffset(Hair2, 0, 10);
            ClothingShift = new Vector3(0, 10, 0);
        }
        else
        {
            AddOffset(Head, 0, 0);
            AddOffset(Breasts, 0, 0);
            AddOffset(BodyAccessory, 0, 0);
            AddOffset(Eyes, 0, 0);
            AddOffset(Mouth, 0, 0);
            AddOffset(Belly, 0, 0);
            AddOffset(Dick, 0, 0);
            AddOffset(Balls, 0, 0);
            AddOffset(Hair, 0, 0);
            AddOffset(Hair2, 0, 0);
            ClothingShift = new Vector3(0, 0, 0);
        }
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.BodyAccentType1 = 0;

        if (Config.ExtraRandomHairColors)
        {
            if (HairColors == ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur))
            {
                unit.HairColor = State.Rand.Next(HairColors);
                unit.AccessoryColor = State.Rand.Next(HairColors);
                unit.ExtraColor1 = State.Rand.Next(HairColors);
                unit.ExtraColor2 = State.Rand.Next(HairColors);
                unit.ExtraColor3 = State.Rand.Next(HairColors);
            }
        }
        else
        {
            if (HairColors == ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur))
            {
                unit.HairColor = State.Rand.Next(ColorPaletteMap.MixedHairColors);
                unit.AccessoryColor = State.Rand.Next(ColorPaletteMap.MixedHairColors);
                unit.ExtraColor1 = State.Rand.Next(ColorPaletteMap.MixedHairColors);
                unit.ExtraColor2 = State.Rand.Next(ColorPaletteMap.MixedHairColors);
                unit.ExtraColor3 = State.Rand.Next(ColorPaletteMap.MixedHairColors);
            }
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) => SpriteDictionary.Harpies[24 + actor.GetSimpleBodySprite()]; //Feathers 2
    protected override Sprite BodySprite(Actor_Unit actor) => SpriteDictionary.Harpies[actor.GetSimpleBodySprite()]; //Torsos

    protected override Sprite BodySizeSprite(Actor_Unit actor) => SpriteDictionary.Harpies[actor.Unit.BodyAccentType1 == 1 ? 38 + actor.GetSimpleBodySprite() : 29 + actor.GetSimpleBodySprite()]; //Feathers 3
    protected override Sprite AccessorySprite(Actor_Unit actor) => SpriteDictionary.Harpies[32 + actor.Unit.SpecialAccessoryType]; //Head Accessory
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => SpriteDictionary.Harpies[6 + actor.GetSimpleBodySprite()]; //Leg Scales
    protected override Sprite BodyAccentSprite(Actor_Unit actor) => SpriteDictionary.Harpies[3 + actor.GetSimpleBodySprite()]; //Base Claws
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => SpriteDictionary.Harpies[21 + actor.GetSimpleBodySprite()]; //Feathers 1
    protected override Sprite BodyAccentSprite3(Actor_Unit actor) => SpriteDictionary.Harpies[35 + actor.GetSimpleBodySprite()]; //Fluff

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon)
        {
            return SpriteDictionary.Harpies[9 + actor.GetSimpleBodySprite() + (3 * (actor.GetWeaponSprite() / 2))];
        }
        else
        {
            return SpriteDictionary.Harpies[3 + actor.GetSimpleBodySprite()];
        }
    }
}

