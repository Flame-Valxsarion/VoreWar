using System;
using System.Collections.Generic;
using UnityEngine;

class OoviKat : DefaultRaceData
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.HumansBodySprites1;
    readonly Sprite[] Sprites2 = State.GameManager.SpriteDictionary.HumansBodySprites2;
    readonly Sprite[] Sprites3 = State.GameManager.SpriteDictionary.HumansBodySprites3;
    readonly Sprite[] Sprites4 = State.GameManager.SpriteDictionary.HumansVoreSprites;
    readonly Sprite[] Sprites5 = State.GameManager.SpriteDictionary.HumansBodySprites4;

    readonly Sprite[] OoviKatFemale = State.GameManager.SpriteDictionary.OoviKatFemale;
    readonly Sprite[] OoviKatMale = State.GameManager.SpriteDictionary.OoviKatMale;
    readonly Sprite[] OoviKatCustom = State.GameManager.SpriteDictionary.OoviKatCustomisation;

    bool oversize = false;

    public OoviKat()
    {
        BodySizes = 3;
        EyeTypes = 10;
        SpecialAccessoryCount = 9; //Flowers
        HeadTypes = 5; // Face Upper
        BodyAccentTypes1 = 6; // eyebrows
        BodyAccentTypes2 = 5; // Face Lower
        BodyAccentTypes3 = 4; // Shoulder Pattern
        BodyAccentTypes4 = 4; // Hand Pattern
        BodyAccentTypes5 = 4; // Feet Pattern
        BeardStyles = 4; // Thigh Pattern
        ExtraColors4 = 4; // Shin Pattern
        FurTypes = 4; // BodyPattern
        TailTypes = 4; // Tails
        VulvaTypes = 4; // Head Acc Type
        HairStyles = 36;
        MouthTypes = 12;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.OoviKatMain);
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.OoviKatBright);
        ExtraColors2 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AlrauneFoliage);
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.RedSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);


        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => FurryColor(s));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => FurryColor(s));
        BodyAccessory = new SpriteExtraInfo(7, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedSkin, s.Unit.SkinColor)); // Ears
        BodyAccent = new SpriteExtraInfo(7, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatMain, s.Unit.AccessoryColor)); // Face Upper
        BodyAccent2 = new SpriteExtraInfo(7, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatBright, s.Unit.ExtraColor1)); // Face Lower
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatMain, s.Unit.AccessoryColor)); // BodyPattern;
        BodyAccent4 = new SpriteExtraInfo(6, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatMain, s.Unit.AccessoryColor)); // ShoulderPattern;
        BodyAccent5 = new SpriteExtraInfo(7, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatBright, s.Unit.ExtraColor1)); // Hand Secondary Color;
        BodyAccent6 = new SpriteExtraInfo(8, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatBright, s.Unit.ExtraColor1)); // Hand Tertiary Color
        BodyAccent7 = new SpriteExtraInfo(9, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatMain, s.Unit.AccessoryColor)); // Hand Pattern
        BodyAccent8 = new SpriteExtraInfo(7, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatBright, s.Unit.ExtraColor1)); // Feet Secondary Color;
        BodyAccent9 = new SpriteExtraInfo(8, BodyAccentSprite9, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatMain, s.Unit.AccessoryColor)); // Feet Pattern;
        BodyAccent10 = new SpriteExtraInfo(9, BodyAccentSprite10, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatBright, s.Unit.ExtraColor1)); // Shin Pattern;
        BodyAccent11 = new SpriteExtraInfo(8, BodyAccentSprite11, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatMain, s.Unit.AccessoryColor)); // Leg Pattern;
        BodyAccent12 = new SpriteExtraInfo(9, BodyAccentSprite12, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatBright, s.Unit.ExtraColor1)); // Leg Pattern Bright;
        BodyAccent13 = new SpriteExtraInfo(0, BodyAccentSprite13, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedSkin, s.Unit.SkinColor)); // Tail Main;
        BodyAccent14 = new SpriteExtraInfo(1, BodyAccentSprite14, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatMain, s.Unit.AccessoryColor)); // Tail Secondary Color;
        BodyAccent15 = new SpriteExtraInfo(2, BodyAccentSprite15, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatBright, s.Unit.ExtraColor1)); // Tail Tertiary Color;
        BodyAccent16 = new SpriteExtraInfo(3, BodyAccentSprite16, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatBright, s.Unit.ExtraColor1)); // Tail Tertiary Color Bright;
        BodyAccent17 = null; //new SpriteExtraInfo(6, BodyAccentSprite17, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatBright, s.Unit.ExtraColor1)); // Stomach Color Bright;
        BodyAccent18 = null; //new SpriteExtraInfo(22, BodyAccentSprite18, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatBright, s.Unit.ExtraColor1)); // Hair Secondary Color;
        BodyAccent19 = null;// new SpriteExtraInfo(22, BodyAccentSprite19, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatBright, s.Unit.ExtraColor1)); // Back Hair Secondary Color;
        BodyAccent20 = new SpriteExtraInfo(7, BodyAccentSprite20, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatMain, s.Unit.AccessoryColor)); // Eye Sclera;
        Pussy = new SpriteExtraInfo(22, HeadAccBase, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatMain, s.Unit.AccessoryColor)); // head acc base;
        PussyIn = new SpriteExtraInfo(22, HeadAccColor1, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatBright, s.Unit.ExtraColor1)); // Head Acc color 1;
        Anus = new SpriteExtraInfo(22, HeadAccColor2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OoviKatBright, s.Unit.ExtraColor1)); // head acc color 2;
        Mouth = new SpriteExtraInfo(7, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(21, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair3 = new SpriteExtraInfo(9, HairSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); // Eyebrows
        Beard = null;
        Eyes = new SpriteExtraInfo(8, EyesSprite, WhiteColored);
        SecondaryEyes = new SpriteExtraInfo(7, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryAccessory = new SpriteExtraInfo(23, SecondaryAccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, s.Unit.ExtraColor2)); // Flower
        Belly = new SpriteExtraInfo(14, null, null, (s) => FurryColor(s));
        Weapon = new SpriteExtraInfo(6, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => FurryColor(s));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => FurryColor(s));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, null, (s) => FurryColor(s));
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => FurryColor(s));


        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new GenericTop1(),
            new GenericTop2(),
            new GenericTop3(),
            new GenericTop4(),
            new GenericTop5(),
            new GenericTop6(),
            new GenericTop7(),
            new MaleTop(),
            new MaleTop2(),
            new MaleTop3(),
            new MaleTop4(),
            new MaleTop5(),
            new MaleTop6(),
            new FemaleOnePiece1(),
            new FemaleOnePiece2(),
            new FemaleOnePiece3(),
            new FemaleOnePiece4(),
        };
        AvoidedMainClothingTypes = 3;
        AvoidedEyeTypes = 0;
        AllowedWaistTypes = new List<MainClothing>()
        {
            new GenericBot1(),
            new GenericBot2(),
            new GenericBot3(),
            new GenericBot4(),
            new GenericBot5(),
            new GenericBot6(),
            new BigLoin(),
            new Skirt(),
        };
        ExtraMainClothing1Types = new List<MainClothing>()
        {
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing50Spaced);
    }


    internal override void SetBaseOffsets(Actor_Unit actor)
    {

        if (actor.Unit.HasBreasts)
        {
            if (actor.Unit.BodySize > 1)
            {
                AddOffset(Balls, 0, 3 * .625f);
                AddOffset(Belly, 0, 1 * .625f);
            }
            else
            {
                AddOffset(Balls, 0, 3 * .625f);
                AddOffset(Belly, 0, 1 * .625f);
            }
        }
        else
        {
            if (actor.Unit.BodySize > 1)
            {
                AddOffset(Balls, 0, 1 * .625f);
                AddOffset(Belly, 0, 1 * .625f);
            }
            else
            {
                AddOffset(Balls, 0, 0);
                AddOffset(Belly, 0, 1 * .625f);
            }
        }

        if (actor.GetWeaponSprite() == 0 || actor.GetWeaponSprite() == 4 || actor.GetWeaponSprite() == 6)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, -1 * .625f, 0);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 1 * .625f, -1 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 0, -1 * .625f);
                }
            }
        }
        else if (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 3)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 0, -1 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 3 * .625f, -3 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 3 * .625f, -4 * .625f);
                }
            }
        }
        else if (actor.GetWeaponSprite() == 2)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, -1 * .625f, 2 * .625f);
                }
                else
                {
                    AddOffset(Weapon, -2 * .625f, 3 * .625f);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 0, 0);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
        }
        else if (actor.GetWeaponSprite() == 5 || actor.GetWeaponSprite() == 7)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 1 * .625f, -1 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 2 * .625f, -3 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 2 * .625f, -3 * .625f);
                }
            }
        }
    }


    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.ExtraColor1 = unit.AccessoryColor;

        unit.SpecialAccessoryType = State.Rand.Next(SpecialAccessoryCount);
        unit.BeardStyle = State.Rand.Next(BeardStyles);
        unit.HeadType = State.Rand.Next(HeadTypes);
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
        unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3);
        unit.BodyAccentType4 = State.Rand.Next(BodyAccentTypes4);
        unit.BodyAccentType5 = State.Rand.Next(BodyAccentTypes5);
        unit.FurType = State.Rand.Next(FurTypes);
        unit.VulvaType = State.Rand.Next(VulvaTypes);
        unit.TailType = State.Rand.Next(TailTypes);

        if (unit.HasDick && unit.HasBreasts)
        {
            if (Config.HermsOnlyUseFemaleHair)
                unit.HairStyle = State.Rand.Next(18);
            else
                unit.HairStyle = State.Rand.Next(HairStyles);
        }
        else if (unit.HasDick && Config.FemaleHairForMales)
            unit.HairStyle = State.Rand.Next(HairStyles);
        else if (unit.HasDick == false && Config.MaleHairForFemales)
            unit.HairStyle = State.Rand.Next(HairStyles);
        else
        {
            if (unit.HasDick)
            {
                unit.HairStyle = 18 + State.Rand.Next(18);
            }
            else
            {
                unit.HairStyle = State.Rand.Next(18);
            }
        }
    }

    internal override int DickSizes => 6;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return Sprites[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            return Sprites[0 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 1:
                return Sprites[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 2:
                return Sprites[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 3:
                return Sprites[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 4:
                return Sprites[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 5:
                return Sprites[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 6:
                return Sprites[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 7:
                return Sprites[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            default:
                return Sprites[0 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return Sprites2[4];
                }
                else
                {
                    return Sprites2[1];
                }
            }
            else
            {
                return Sprites2[7 + (actor.Unit.BodySize * 3)];
            }
        }
        else if (actor.IsAttacking)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return Sprites2[5];
                }
                else
                {
                    return Sprites2[2];
                }
            }
            else
            {
                return Sprites2[8 + (actor.Unit.BodySize * 3)];
            }
        }
        else
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return Sprites2[3];
                }
                else
                {
                    return Sprites2[0];
                }
            }
            else
            {
                return Sprites2[6 + (actor.Unit.BodySize * 3)];
            }

        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) //ears
    {
        if (actor.Unit.EarType == 0)
        {
            return null;
        }
        else if (actor.Unit.EarType == 0)
        {
            return Sprites3[0];

        }
        if (actor.Unit.HasBreasts)
        {
            return OoviKatFemale[188 + actor.Unit.EarType - 2];
        }
        return OoviKatMale[188 + actor.Unit.EarType - 2];

    }

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) // Flower
    {
        if (actor.Unit.SpecialAccessoryType == 8)
        {
            return null;
        }
        if (actor.Unit.HasBreasts)
        {
            return OoviKatFemale[160 + actor.Unit.SpecialAccessoryType];
        }
        return OoviKatMale[160 + actor.Unit.SpecialAccessoryType];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Face Upper
    {
        if (actor.Unit.HeadType == 4)
        {
            return null;
        }
        if (actor.Unit.HasBreasts)
        {
            return OoviKatFemale[172 + actor.Unit.HeadType];
        }
        return OoviKatMale[172 + actor.Unit.HeadType];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Face Lower
    {
        if (actor.Unit.BodyAccentType2 == 4)
        {
            return null;
        }
        if (actor.Unit.HasBreasts)
        {
            return OoviKatFemale[168 + actor.Unit.BodyAccentType2];
        }
        return OoviKatMale[168 + actor.Unit.BodyAccentType2];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Body Pattern
    {
        if (actor.Unit.HasBreasts)
        {
            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) return OoviKatFemale[3 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
                return OoviKatFemale[0 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
            }

            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return OoviKatFemale[2 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
                case 1:
                    return OoviKatFemale[3 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
                case 2:
                    return OoviKatFemale[1 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
                case 3:
                    return OoviKatFemale[3 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
                case 4:
                    return OoviKatFemale[2 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
                case 5:
                    return OoviKatFemale[1 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
                case 6:
                    return OoviKatFemale[2 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
                case 7:
                    return OoviKatFemale[1 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
                default:
                    return OoviKatFemale[0 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
            }
        }

        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return OoviKatMale[3 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
            return OoviKatMale[0 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return OoviKatMale[2 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
            case 1:
                return OoviKatMale[3 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
            case 2:
                return OoviKatMale[1 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
            case 3:
                return OoviKatMale[3 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
            case 4:
                return OoviKatMale[2 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
            case 5:
                return OoviKatMale[1 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
            case 6:
                return OoviKatMale[2 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
            case 7:
                return OoviKatMale[1 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
            default:
                return OoviKatMale[0 + (actor.Unit.BodySize * 4) + (actor.Unit.FurType * 16)];
        }
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Shoulder Pattern
    {
        if (actor.Unit.HasBreasts)
        {
            return OoviKatFemale[12 + (actor.Unit.BodySize >= 2 ? 2 : 0) + (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 3 ? 1 : 0) + actor.Unit.BodyAccentType3 * 16];
        }
        return OoviKatMale[12 + (actor.Unit.BodySize >= 2 ? 2 : 0) + (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 3 ? 1 : 0) + actor.Unit.BodyAccentType3 * 16];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Hand color
    {
        if (actor.Unit.HasBreasts)
        {
            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) return OoviKatFemale[107 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
                return OoviKatFemale[104 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            }

            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return OoviKatFemale[106 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
                case 1:
                    return OoviKatFemale[107 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
                case 2:
                    return OoviKatFemale[105 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
                case 3:
                    return OoviKatFemale[107 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
                case 4:
                    return OoviKatFemale[106 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
                case 5:
                    return OoviKatFemale[105 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
                case 6:
                    return OoviKatFemale[106 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
                case 7:
                    return OoviKatFemale[105 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
                default:
                    return OoviKatFemale[104 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            }
        }

        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return OoviKatMale[107 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            return OoviKatMale[104 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return OoviKatMale[106 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            case 1:
                return OoviKatMale[107 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            case 2:
                return OoviKatMale[105 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            case 3:
                return OoviKatMale[107 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            case 4:
                return OoviKatMale[106 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            case 5:
                return OoviKatMale[105 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            case 6:
                return OoviKatMale[106 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            case 7:
                return OoviKatMale[105 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            default:
                return OoviKatMale[104 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
        }
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Hand Tertiary Color
    {

        if (actor.Unit.HasBreasts)
        {
            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) return OoviKatFemale[127 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
                return OoviKatFemale[124 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
            }

            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return OoviKatFemale[126 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
                case 1:
                    return OoviKatFemale[127 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
                case 2:
                    return OoviKatFemale[125 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
                case 3:
                    return OoviKatFemale[127 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
                case 4:
                    return OoviKatFemale[126 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
                case 5:
                    return OoviKatFemale[125 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
                case 6:
                    return OoviKatFemale[126 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
                case 7:
                    return OoviKatFemale[125 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
                default:
                    return OoviKatFemale[124 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
            }
        }

        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return OoviKatMale[127 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
            return OoviKatMale[124 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return OoviKatMale[126 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
            case 1:
                return OoviKatMale[127 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
            case 2:
                return OoviKatMale[125 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
            case 3:
                return OoviKatMale[127 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
            case 4:
                return OoviKatMale[126 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
            case 5:
                return OoviKatMale[125 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
            case 6:
                return OoviKatMale[126 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
            case 7:
                return OoviKatMale[125 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
            default:
                return OoviKatMale[124 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
        }
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Hand pattern
    {

        if (actor.Unit.HasBreasts)
        {
            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) return OoviKatFemale[67 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
                return OoviKatFemale[64 + (actor.Unit.BodySize >= 2 ? 16 : 0)];
            }

            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return OoviKatFemale[66 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
                case 1:
                    return OoviKatFemale[67 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
                case 2:
                    return OoviKatFemale[65 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
                case 3:
                    return OoviKatFemale[67 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
                case 4:
                    return OoviKatFemale[66 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
                case 5:
                    return OoviKatFemale[65 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
                case 6:
                    return OoviKatFemale[66 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
                case 7:
                    return OoviKatFemale[65 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
                default:
                    return OoviKatFemale[64 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
            }
        }

        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return OoviKatMale[67 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
            return OoviKatMale[64 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return OoviKatMale[66 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
            case 1:
                return OoviKatMale[67 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
            case 2:
                return OoviKatMale[65 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
            case 3:
                return OoviKatMale[67 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
            case 4:
                return OoviKatMale[66 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
            case 5:
                return OoviKatMale[65 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
            case 6:
                return OoviKatMale[66 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
            case 7:
                return OoviKatMale[65 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
            default:
                return OoviKatMale[64 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.BodyAccentType4 * 4];
        }
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // Feet Secondary Color
    {
        if (actor.Unit.HasBreasts)
        {
            return OoviKatFemale[159];
        }
        return OoviKatMale[159];
    }

    protected override Sprite BodyAccentSprite9(Actor_Unit actor) // Feet Pattern
    {
        if (actor.Unit.HasBreasts)
        {
            return OoviKatFemale[96 + (actor.Unit.BodySize >= 2 ? 4 : 0) + actor.Unit.BodyAccentType5];
        }
        return OoviKatMale[96 + (actor.Unit.BodySize >= 2 ? 4 : 0) + actor.Unit.BodyAccentType5];
    }

    protected override Sprite BodyAccentSprite10(Actor_Unit actor) // Shin Pattern
    {
        if (actor.Unit.HasBreasts)
        {
            return OoviKatFemale[144 + (actor.Unit.BodySize * 4) + actor.Unit.ExtraColor4];
        }
        return OoviKatMale[144 + (actor.Unit.BodySize * 4) + actor.Unit.ExtraColor4];
    }

    protected override Sprite BodyAccentSprite11(Actor_Unit actor) // Leg Pattern
    {
        if (actor.Unit.HasBreasts)
        {
            return OoviKatFemale[112 + (actor.Unit.BodySize * 4) + actor.Unit.BeardStyle];
        }
        return OoviKatMale[112 + (actor.Unit.BodySize * 4) + actor.Unit.BeardStyle];
    }

    protected override Sprite BodyAccentSprite12(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts)
        {
            return OoviKatFemale[128 + (actor.Unit.BodySize * 4) + actor.Unit.BeardStyle];
        }
        return OoviKatMale[128 + (actor.Unit.BodySize * 4) + actor.Unit.BeardStyle];
    }

    protected override Sprite BodyAccentSprite13(Actor_Unit actor) // Tial 1
    {
        if (actor.Unit.HasBreasts)
        {
            return OoviKatFemale[192 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.TailType * 4];
        }
        return OoviKatMale[192 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.TailType * 4];
    }

    protected override Sprite BodyAccentSprite14(Actor_Unit actor) // Tail 2
    {
        if (actor.Unit.HasBreasts)
        {
            return OoviKatFemale[193 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.TailType * 4];
        }
        return OoviKatMale[193 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.TailType * 4];
    }

    protected override Sprite BodyAccentSprite15(Actor_Unit actor) // Tail 3
    {
        if (actor.Unit.HasBreasts)
        {
            return OoviKatFemale[194 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.TailType * 4];
        }
        return OoviKatMale[194 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.TailType * 4];
    }

    protected override Sprite BodyAccentSprite16(Actor_Unit actor) //Tail 4
    {
        if (actor.Unit.HasBreasts)
        {
            return OoviKatFemale[195 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.TailType * 4];
        }
        return OoviKatMale[195 + (actor.Unit.BodySize >= 2 ? 16 : 0) + actor.Unit.TailType * 4];
    }

    protected override Sprite BodyAccentSprite17(Actor_Unit actor) //Waist Color Bright
    {
        if (actor.Unit.HasBreasts)
        {
            return OoviKatFemale[156 + (actor.Unit.BodySize)];
        }
        return OoviKatMale[156 + (actor.Unit.BodySize)];
    }

    protected override Sprite BodyAccentSprite18(Actor_Unit actor) // Hair Secondary Color
    {
        return OoviKatCustom[12 + 2 * actor.Unit.HairStyle];
    }

    protected override Sprite BodyAccentSprite19(Actor_Unit actor) // Hair Back Secondary Color
    {
        return OoviKatCustom[13 + 2 * actor.Unit.HairStyle];
    }

    protected Sprite HeadAccBase(Actor_Unit actor)
    {
        return OoviKatCustom[0 + actor.Unit.VulvaType];
    }

    protected Sprite HeadAccColor1(Actor_Unit actor) 
    {
        return OoviKatCustom[4 + actor.Unit.VulvaType];
    }

    protected Sprite HeadAccColor2(Actor_Unit actor) 
    { 
        return OoviKatCustom[8 + actor.Unit.VulvaType];
    }

    protected override Sprite BodyAccentSprite20(Actor_Unit actor) // Eye Sclera
    {
        if (actor.Unit.EyeType >= 6)
        {
            if (actor.Unit.HasBreasts)
            {
                return OoviKatFemale[180 + actor.Unit.EyeType - 6];
            }
            return OoviKatMale[180 + actor.Unit.EyeType - 6];
        }
        return null;
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
            return null;
        else
            return Sprites3[108 + actor.Unit.MouthType];
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        return Sprites2[71 + 2 * actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        return Sprites2[72 + 2 * actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite3(Actor_Unit actor)
    {
        return Sprites3[120 + actor.Unit.BodyAccentType1];
    }

    protected override Sprite BeardSprite(Actor_Unit actor)
    {
        if (actor.Unit.BeardStyle == 6)
        {
            return null;
        }
        else
        {
            return Sprites3[126 + actor.Unit.BeardStyle];
        }
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            return Sprites2[69];
        }
        else if (actor.Unit.EyeType >= 6)
        {
            if (actor.Unit.HasBreasts)
            {
                return OoviKatFemale[176 + actor.Unit.EyeType - 6];
            }
            return OoviKatMale[176 + actor.Unit.EyeType - 6];
        }
        else
        {
            return Sprites3[24 + 4 * actor.Unit.EyeType + ((actor.IsAttacking || actor.IsEating) ? 0 : 2)];
        }
    }

    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            return null;
        }
        else if (actor.Unit.EyeType >= 6)
        {
            if (actor.Unit.HasBreasts)
            {
                return OoviKatFemale[184 + actor.Unit.EyeType - 6];
            }
            return OoviKatMale[184 + actor.Unit.EyeType - 6];
        }
        else
        {
            return Sprites3[25 + 4 * actor.Unit.EyeType + ((actor.IsAttacking || actor.IsEating) ? 0 : 2)];
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(31, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites4[105];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites4[104];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites4[103];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites4[102];
            }
            switch (size)
            {
                case 26:
                    AddOffset(Belly, 0, -14 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -17 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -20 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -25 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -27 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -32 * .625f);
                    break;
            }

            return Sprites4[70 + size];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return Sprites3[132];
                case 1:
                    return Sprites3[133];
                case 2:
                    return Sprites3[134];
                case 3:
                    return Sprites3[135];
                case 4:
                    return Sprites3[136];
                case 5:
                    return Sprites3[138];
                case 6:
                    return Sprites3[139];
                case 7:
                    return Sprites3[141];
                default:
                    return null;
            }
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        oversize = false;
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f));
            if (leftSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
            {
                return Sprites4[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return Sprites4[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return Sprites4[29];
            }

            if (leftSize > 28)
                leftSize = 28;

            return Sprites4[0 + leftSize];
        }
        else
        {
            return Sprites4[0 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f));
            if (rightSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
            {
                return Sprites4[63];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return Sprites4[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return Sprites4[61];
            }

            if (rightSize > 28)
                rightSize = 28;

            return Sprites4[32 + rightSize];
        }
        else
        {
            return Sprites4[32 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
            {
                Dick.layer = 20;
                return Sprites5[1 + (actor.Unit.DickSize * 2) + ((actor.Unit.BodySize > 1) ? 12 : 0) + ((!actor.Unit.HasBreasts) ? 24 : 0)];
            }
            else
            {
                Dick.layer = 13;
                return Sprites5[0 + (actor.Unit.DickSize * 2) + ((actor.Unit.BodySize > 1) ? 12 : 0) + ((!actor.Unit.HasBreasts) ? 24 : 0)];
            }
        }

        Dick.layer = 11;
        return Sprites5[0 + (actor.Unit.DickSize * 2) + ((actor.Unit.BodySize > 1) ? 12 : 0) + ((!actor.Unit.HasBreasts) ? 24 : 0)];
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect() && (actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
        {
            Balls.layer = 19;
        }
        else
        {
            Balls.layer = 10;
        }
        int size = actor.Unit.DickSize;
        int offset = actor.GetBallSize(28, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return Sprites4[141];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return Sprites4[140];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return Sprites4[139];
        }
        else if (offset >= 26)
        {
            AddOffset(Balls, 0, -22 * .625f);
        }
        else if (offset == 25)
        {
            AddOffset(Balls, 0, -16 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -13 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -11 * .625f);
        }
        else if (offset == 22)
        {
            AddOffset(Balls, 0, -10 * .625f);
        }
        else if (offset == 21)
        {
            AddOffset(Balls, 0, -7 * .625f);
        }
        else if (offset == 20)
        {
            AddOffset(Balls, 0, -6 * .625f);
        }
        else if (offset == 19)
        {
            AddOffset(Balls, 0, -4 * .625f);
        }
        else if (offset == 18)
        {
            AddOffset(Balls, 0, -1 * .625f);
        }

        if (offset > 0)
            return Sprites4[Math.Min(112 + offset, 138)];
        return Sprites4[106 + size];
    }


    class GenericTop1 : MainClothing
    {
        public GenericTop1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[57];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60001;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.OoviKat.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[56];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[0 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }
    class GenericTop2 : MainClothing
    {
        public GenericTop2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[58];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60002;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.OoviKat.oversize)
            {
                clothing1.GetSprite = (s) => null;
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[8 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }
    class GenericTop3 : MainClothing
    {
        public GenericTop3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[60];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60003;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.OoviKat.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[59];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[16 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }
    class GenericTop4 : MainClothing
    {
        public GenericTop4()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[62];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60004;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.OoviKat.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[61];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[24 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }
    class GenericTop5 : MainClothing
    {
        public GenericTop5()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[64];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60005;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.OoviKat.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[63];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[32 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }
    class GenericTop6 : MainClothing
    {
        public GenericTop6()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[66];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60006;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.OoviKat.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[65];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[40 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }
    class GenericTop7 : MainClothing
    {
        public GenericTop7()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[68];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60007;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.OoviKat.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[67];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[48 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }

    class MaleTop : MainClothing
    {
        public MaleTop()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[5];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 60008;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[0];

            base.Configure(sprite, actor);
        }
    }

    class MaleTop2 : MainClothing
    {
        public MaleTop2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[5];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 60009;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.HasBelly)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[4];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[1 + actor.Unit.BodySize];
            }

            base.Configure(sprite, actor);
        }
    }

    class MaleTop3 : MainClothing
    {
        public MaleTop3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[11];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 60010;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[6];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class MaleTop4 : MainClothing
    {
        public MaleTop4()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[11];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 60011;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.HasBelly)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[10];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[7 + actor.Unit.BodySize];
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class MaleTop5 : MainClothing
    {
        public MaleTop5()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[14];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 60012;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.BodySize == 2)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[13];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[12];
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class MaleTop6 : MainClothing
    {
        public MaleTop6()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[16];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 60013;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[15];

            base.Configure(sprite, actor);
        }
    }

    class Uniform1 : MainClothing
    {
        public Uniform1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUniform1[42];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(15, null, null);
            clothing3 = new SpriteExtraInfo(5, null, null);
            Type = 60025;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.OoviKat.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[6];
            }
            else if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BreastSize > 5)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[6];
                }
                else
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[0 + actor.Unit.BreastSize];
                }
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            if (actor.HasBelly)
            {
                if (actor.GetStomachSize(31, 0.7f) > 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[13 + 7 * actor.Unit.BodySize + 21 * (!actor.Unit.HasBreasts ? 1 : 0)];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[12 + 7 * actor.Unit.BodySize + 21 * (!actor.Unit.HasBreasts ? 1 : 0)];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 2)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[11 + 7 * actor.Unit.BodySize + 21 * (!actor.Unit.HasBreasts ? 1 : 0)];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 1)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[10 + 7 * actor.Unit.BodySize + 21 * (!actor.Unit.HasBreasts ? 1 : 0)];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 0)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[9 + 7 * actor.Unit.BodySize + 21 * (!actor.Unit.HasBreasts ? 1 : 0)];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[8 + 7 * actor.Unit.BodySize + 21 * (!actor.Unit.HasBreasts ? 1 : 0)];
                }
            }
            else
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[7 + 7 * actor.Unit.BodySize + 21 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }

            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform1[3 + 4 * actor.Unit.BodySize + 12 * (!actor.Unit.HasBreasts ? 1 : 0)];
                else clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform1[0 + 4 * actor.Unit.BodySize + 12 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }
            else if (actor.GetWeaponSprite() == 0 || actor.GetWeaponSprite() == 4 || actor.GetWeaponSprite() == 6)
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform1[2 + 4 * actor.Unit.BodySize + 12 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }
            else if (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 3)
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform1[3 + 4 * actor.Unit.BodySize + 12 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }
            else
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform1[1 + 4 * actor.Unit.BodySize + 12 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class FemaleOnePiece1 : MainClothing
    {
        public FemaleOnePiece1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFOnePieces[81];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            clothing4 = new SpriteExtraInfo(15, null, null);
            Type = 60014;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.OoviKat.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[51];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[43 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            if (actor.Unit.BodySize == 2)
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[42];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[41];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[40];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[39];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[38];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[37];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[21];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[20];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[19];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[18];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[17];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[16];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[15];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class FemaleOnePiece2 : MainClothing
    {
        public FemaleOnePiece2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFOnePieces[80];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            clothing4 = new SpriteExtraInfo(15, null, null);
            Type = 60015;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.OoviKat.oversize)
            {
                clothing1.GetSprite = null;
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[52 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            if (actor.Unit.BodySize == 2)
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[36];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[35];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[34];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[33];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[32];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[31];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[30];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[29];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[28];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[27];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[26];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[25];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[24];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[23];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[22];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[14];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[13];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[12];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[11];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[10];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[9];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[8];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[7];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[6];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[5];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[4];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[3];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[2];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[1];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[0];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class FemaleOnePiece3 : MainClothing
    {
        public FemaleOnePiece3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFOnePieces[79];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            clothing4 = new SpriteExtraInfo(15, null, null);
            Type = 60016;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.OoviKat.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[69];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[61 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            if (actor.Unit.BodySize == 2)
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[36];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[35];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[34];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[33];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[32];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[31];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[30];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[29];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[28];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[27];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[26];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[25];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[24];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[23];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[22];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[14];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[13];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[12];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[11];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[10];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[9];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[8];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[7];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[6];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[5];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[4];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[3];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[2];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[1];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[0];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class FemaleOnePiece4 : MainClothing
    {
        public FemaleOnePiece4()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFOnePieces[82];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            clothing4 = new SpriteExtraInfo(15, null, null);
            Type = 60017;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.OoviKat.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[78];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[70 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            if (actor.Unit.BodySize == 2)
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[42];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[41];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[40];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[39];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[38];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[37];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[21];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[20];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[19];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[18];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[17];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[16];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[15];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericBot1 : MainClothing
    {
        public GenericBot1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUnderbottoms[6];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 60018;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 4)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[0 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[3 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot2 : MainClothing
    {
        public GenericBot2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUnderbottoms[13];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 60019;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 4)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[7 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[10 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot3 : MainClothing
    {
        public GenericBot3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUnderbottoms[26];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 60020;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 4)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[62];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[63];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[20 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[23 + actor.Unit.BodySize];
            }

            base.Configure(sprite, actor);
        }
    }

    class GenericBot4 : MainClothing
    {
        public GenericBot4()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUnderbottoms[39];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 60021;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 4)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[33 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[36 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot5 : MainClothing
    {
        public GenericBot5()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUnderbottoms[52];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 60022;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 4)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[46 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[49 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot6 : MainClothing
    {
        public GenericBot6()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUnderbottoms[59];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 60023;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 4)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[53 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[56 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class Uniform2 : MainClothing
    {
        public Uniform2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUniform1[43];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, null);
            Type = 60024;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform1[24 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform1[33 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class BigLoin : MainClothing
    {
        public BigLoin()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenBigLoin[12];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, null);
            Type = 60026;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenBigLoin[0 + 2 * actor.Unit.BodySize + (actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenBigLoin[6 + 2 * actor.Unit.BodySize + (actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class Pants1 : MainClothing
    {
        public Pants1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenPants[28];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(13, null, WhiteColored);
            Type = 60027;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.DickSize > 0)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[24 + (actor.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)];
                }
                else clothing2.GetSprite = null;

                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[0 + 2 * actor.Unit.BodySize + (actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[6 + 2 * actor.Unit.BodySize + (actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[25 + (actor.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)];
            }

            base.Configure(sprite, actor);
        }
    }

    class Pants2 : MainClothing
    {
        public Pants2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenPants[33];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, null);
            clothing2 = new SpriteExtraInfo(13, null, null);
            Type = 60028;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.DickSize > 0)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[29 + (actor.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)];
                }
                else clothing2.GetSprite = null;

                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[12 + 2 * actor.Unit.BodySize + (actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[18 + 2 * actor.Unit.BodySize + (actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[30 + (actor.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class Skirt : MainClothing
    {
        public Skirt()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenSkirt[6];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, null);
            Type = 60029;
            femaleOnly = true;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenSkirt[0 + 2 * actor.Unit.BodySize + (actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    static ColorSwapPalette FurryColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedSkin, actor.Unit.SkinColor);
    }

}
