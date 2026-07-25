using System.Collections.Generic;
using UnityEngine;

class Scylla : DefaultRaceData
{
    public Scylla()
    {
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
        BodySizes = 4;

        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        //Head = null;
        BodyAccessory = new SpriteExtraInfo(5, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        BodyAccent = new SpriteExtraInfo(7, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        BodyAccent2 = new SpriteExtraInfo(7, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        BodyAccent3 = null;
        //Mouth = null;
        //Hair = new SpriteExtraInfo(6, HairSprite, null, (s) => ColorPaletteMap.SlimeSubPalettes[s.Unit.HairColor]);
        //Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.SlimeSubPalettes[s.Unit.HairColor]);
        //Eyes = new SpriteExtraInfo(4, EyesSprite, null, (s) => ColorPaletteMap.EyeColorSwaps[s.Unit.EyeColor]);
        SecondaryEyes = null;
        SecondaryAccessory = new SpriteExtraInfo(5, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(1, WeaponSprite, WhiteColored);
        //BackWeapon = null;
        BodySize = new SpriteExtraInfo(6, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        BreastShadow = null;
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
            ClothingTypes.Loincloth,
        };

    }

    protected override Sprite BodySprite(Actor_Unit actor) => SpriteDictionary.Scylla[24 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodySize) + (actor.Unit.HasBreasts ? 0 : 8)]; //Torsos
    protected override Sprite BodySizeSprite(Actor_Unit actor) => SpriteDictionary.Scylla[3 + actor.Unit.BodySize + (actor.Unit.HasBreasts ? 0 : 7)]; //Fins
    protected override Sprite AccessorySprite(Actor_Unit actor) => SpriteDictionary.Scylla[2]; //Head Fins
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => SpriteDictionary.Scylla[8 + (actor.IsAttacking ? 1 : 0)]; //Arm Scales
    protected override Sprite BodyAccentSprite(Actor_Unit actor) => SpriteDictionary.Scylla[0]; //Front Tents
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => SpriteDictionary.Scylla[1]; //Rear Tents
    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            int sprite = actor.GetWeaponSprite();
            if (sprite == 5)
                return null;
            if (sprite > 5)
                sprite--;
            return SpriteDictionary.Scylla[15 + sprite];
        }
        else
        {
            return null;
        }
    }

}

