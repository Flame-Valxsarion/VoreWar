using System;
using System.Collections.Generic;
using UnityEngine;

class Jackals : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.HumansBodySprites1;
    readonly Sprite[] Sprites2 = SpriteDictionary.HumansBodySprites2;
    readonly Sprite[] Sprites3 = SpriteDictionary.HumansBodySprites3;
    readonly Sprite[] Sprites4 = SpriteDictionary.HumansVoreSprites;
    readonly Sprite[] Sprites5 = SpriteDictionary.HumansBodySprites4;
    readonly Sprite[] Sprites6 = SpriteDictionary.HumansBodySprites5;

    bool oversize = false;

    public Jackals()
    {
        BodySizes = 3;
        EarTypes = 4;
        TailTypes = 4;
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.RedSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.JackalSkin);
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.JackalSkin);
        BeardStyles = 0;
        BodyAccentTypes1 = 6; // eyebrows
        BodyAccentTypes3 = 2; // earrings
        //BodyAccentTypes4 = 3; // Navel Piercing
        SpecialAccessoryCount = 0;
        HairStyles = 36;
        MouthTypes = 12;
        FurCapable = true;
        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => FurryColor(s));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => FurryColor(s));
        BodyAccessory = new SpriteExtraInfo(22, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.JackalSkin, s.Unit.AccessoryColor)); // Ears
        BodyAccent = new SpriteExtraInfo(23, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.JackalSkin, s.Unit.ExtraColor1)); // Ear Color
        BodyAccent2 = new SpriteExtraInfo(3, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.JackalSkin, s.Unit.AccessoryColor)); // Tail
        BodyAccent3 = new SpriteExtraInfo(23, BodyAccentSprite3, WhiteColored); // Ear Accessory
        BodyAccent4 = null; // new SpriteExtraInfo(5, BodyAccentSprite4, WhiteColored); // Stomach Accessory;
        BodyAccent5 = new SpriteExtraInfo(0, BodyAccentSprite5, WhiteColored); // Back weapon sprite;
        BodyAccent6 = new SpriteExtraInfo(23, BodyAccentSprite6, WhiteColored); // Front weapon sprite;
        BodyAccent7 = null;
        BodyAccent8 = null;
        Mouth = new SpriteExtraInfo(7, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(21, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair3 = new SpriteExtraInfo(9, HairSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); // Eyebrows
        Eyes = new SpriteExtraInfo(8, EyesSprite, WhiteColored);
        SecondaryEyes = new SpriteExtraInfo(7, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(14, null, null, (s) => FurryColor(s));
        Weapon = new SpriteExtraInfo(16, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => FurryColor(s));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => FurryColor(s));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, null, (s) => DickColor(s));
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => FurryColor(s));


        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new GenericTop1(),
            new GenericTop2(),
            new GenericTop3(),
            new GenericTop5(),
            new TightGenericTop5(),
            new GenericTop6(),
            new BronzeBikini(),
            new WrapTop(),
            new PlainClothTop(),
            new MaleTop1(),
            new MaleTop2(),
        };
        AvoidedMainClothingTypes = 0;
        AvoidedEyeTypes = 0;
        AllowedWaistTypes = new List<MainClothing>()
        {
            new GenericBot4(),
            new GildedLoin(),
            new GenericBot5(),
            new GenericBot6(),
            new PlainClothSkirt(),
            new ClothSkirt(),
        };
        ExtraMainClothing1Types = new List<MainClothing>() // Legs
        {
            new LegRings(),
            new LegRings2(),
            new LegRings3(),
        };
        ExtraMainClothing2Types = new List<MainClothing>() // Arms
        {
            new ArmRings(),
            new WristRings(),
        };
        ExtraMainClothing3Types = new List<MainClothing>() // Neck
        {
            new NeckRing(),
            new GoldNecklaceWithColor(),
            new GoldNecklace(),
            new ColorNecklace(),
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing50Spaced);
    }

    static ColorSwapPalette FurryColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.JackalSkin, actor.Unit.AccessoryColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedSkin, actor.Unit.SkinColor);
    }

    static ColorSwapPalette DickColor(Actor_Unit actor)
    {
        if (Config.FurryGenitals && actor.IsErect() && actor.Unit.Furry)
            return null;
        return FurryColor(actor);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            AddOffset(Mouth, 0, -2 * .625f);
        }
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

        unit.EarType = State.Rand.Next(EarTypes);
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3);
        unit.BodyAccentType4 = State.Rand.Next(BodyAccentTypes4);
        unit.TailType = State.Rand.Next(TailTypes);
        unit.SkinColor = State.Rand.Next(SkinColors);
        unit.AccessoryColor = State.Rand.Next(AccessoryColors);
        unit.ExtraColor1 = State.Rand.Next(ExtraColors1);

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
            if (actor.IsAttacking) return Sprites[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12) + (actor.Unit.Furry ? 48 : 0)];
            return Sprites[0 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12) + (actor.Unit.Furry ? 48 : 0)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12) + (actor.Unit.Furry ? 48 : 0)];
            case 1:
                return Sprites[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12) + (actor.Unit.Furry ? 48 : 0)];
            case 2:
                return Sprites[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12) + (actor.Unit.Furry ? 48 : 0)];
            case 3:
                return Sprites[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12) + (actor.Unit.Furry ? 48 : 0)];
            case 4:
                return Sprites[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12) + (actor.Unit.Furry ? 48 : 0)];
            case 5:
                return Sprites[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12) + (actor.Unit.Furry ? 48 : 0)];
            case 6:
                return Sprites[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12) + (actor.Unit.Furry ? 48 : 0)];
            case 7:
                return Sprites[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12) + (actor.Unit.Furry ? 48 : 0)];
            default:
                return Sprites[0 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12) + (actor.Unit.Furry ? 48 : 0)];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            if (actor.IsEating)
            {
                return SpriteDictionary.JackalMain[22];
            }
            else if (actor.IsAttacking)
            {
                return SpriteDictionary.JackalMain[23];
            }
            else 
            {
                return SpriteDictionary.JackalMain[21];
            }

        }
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

    protected override Sprite AccessorySprite(Actor_Unit actor) => SpriteDictionary.JackalMain[0 + actor.Unit.EarType]; //ears

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => SpriteDictionary.JackalMain[4 + actor.Unit.EarType];
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => SpriteDictionary.JackalMain[8 + actor.Unit.TailType];

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Ears
    {
        if (actor.Unit.BodyAccentType3 == 0)
            return null;
        return SpriteDictionary.JackalJewel[31 + actor.Unit.EarType];
    }
    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Ears
    {
        if (actor.Unit.BodyAccentType4 == 0)
            return null;
        else if (actor.Unit.BodyAccentType4 == 1)
        {
            return SpriteDictionary.JackalJewel[35];
        }
        else
        {
            return SpriteDictionary.JackalJewel[47];
        }
    }



    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Extra weapon sprite
    {
        if (actor.Unit.HasWeapon == false)
        {
            return null;
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return null;
            case 1:
                return null;
            case 2:
                return null;
            case 3:
                return null;
            case 4:
                return null;
            case 5:
                return null;
            case 6:
                return SpriteDictionary.JackalMain[20];
            case 7:
                return null;
            default:
                return null;
        }
    }
    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Extra weapon sprite
    {
        if (actor.Unit.HasWeapon == false || actor.Surrendered)
        {
            return null;
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return null;
            case 1:
                return null;
            case 2:
                return SpriteDictionary.JackalMain[35];
            case 3:
                return null;
            case 4:
                return null;
            case 5:
                return null;
            case 6:
                return null;
            case 7:
                return null;
            default:
                return null;
        }
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

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            return Sprites2[69];
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
                    return SpriteDictionary.JackalMain[12];
                case 1:
                    return SpriteDictionary.JackalMain[13];
                case 2:
                    return SpriteDictionary.JackalMain[14];
                case 3:
                    return SpriteDictionary.JackalMain[15];
                case 4:
                    return SpriteDictionary.JackalMain[16];
                case 5:
                    return SpriteDictionary.JackalMain[17];
                case 6:
                    return SpriteDictionary.JackalMain[18];
                case 7:
                    return SpriteDictionary.JackalMain[19];
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
        bool use_furry = actor.Unit.Furry && Config.FurryGenitals;
        if (actor.IsErect())
        {
            if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
            {
                Dick.layer = 20;
                if (use_furry)
                {
                    return Sprites6[((!actor.Unit.HasBreasts) ? 37 : 1 + ((actor.Unit.BodySize > 1) ? 18 : 0)) + (actor.Unit.DickSize * 3)];
                }
                return Sprites5[1 + (actor.Unit.DickSize * 2) + ((actor.Unit.BodySize > 1) ? 12 : 0) + ((!actor.Unit.HasBreasts) ? 24 : 0)];

            }
            else
            {
                Dick.layer = 13;
                if (use_furry)
                {
                    return Sprites6[((!actor.Unit.HasBreasts) ? 38 : 2 + ((actor.Unit.BodySize > 1) ? 18 : 0)) + (actor.Unit.DickSize * 3)];
                }
                return Sprites5[0 + (actor.Unit.DickSize * 2) + ((actor.Unit.BodySize > 1) ? 12 : 0) + ((!actor.Unit.HasBreasts) ? 24 : 0)];
            }
        }

        Dick.layer = 11;
        if (use_furry)
        {
            return Sprites6[((!actor.Unit.HasBreasts) ? 36 : 0 + ((actor.Unit.BodySize > 1) ? 18 : 0)) + (actor.Unit.DickSize * 3)];
        }
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
            DiscardSprite = SpriteDictionary.HumenFundertops[57];
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
            if (Races.Jackals.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFundertops[56];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFundertops[0 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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
            DiscardSprite = SpriteDictionary.HumenFundertops[58];
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
            if (Races.Jackals.oversize)
            {
                clothing1.GetSprite = (s) => null;
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFundertops[8 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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
            DiscardSprite = SpriteDictionary.HumenFundertops[60];
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
            if (Races.Jackals.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFundertops[59];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFundertops[16 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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
    class GenericTop5 : MainClothing
    {
        public GenericTop5()
        {
            DiscardSprite = SpriteDictionary.HumenFundertops[64];
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
            if (Races.Jackals.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFundertops[63];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFundertops[32 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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


    class TightGenericTop5 : MainClothing
    {
        public TightGenericTop5()
        {
            DiscardSprite = SpriteDictionary.HumenFundertops[64];
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
            if (Races.Jackals.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFundertops[63];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.JackalClothes[36 + Math.Min(5,actor.Unit.BreastSize)];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.JackalMain[24];
                    clothing3.GetSprite = (s) => SpriteDictionary.JackalMain[29];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.JackalMain[25];
                    clothing3.GetSprite = (s) => SpriteDictionary.JackalMain[30];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.JackalMain[26];
                    clothing3.GetSprite = (s) => SpriteDictionary.JackalMain[31];
                }
                else if (actor.Unit.BreastSize == 6)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.JackalMain[27];
                    clothing3.GetSprite = (s) => SpriteDictionary.JackalMain[32];
                }
                else if (actor.Unit.BreastSize == 7)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.JackalMain[28];
                    clothing3.GetSprite = (s) => SpriteDictionary.JackalMain[33];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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
            DiscardSprite = SpriteDictionary.HumenFundertops[66];
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
            if (Races.Jackals.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFundertops[65];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFundertops[40 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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


    class PlainClothTop : MainClothing
    {
        public PlainClothTop()
        {
            DiscardSprite = SpriteDictionary.JackalClothes[86];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 70108;
            DiscardUsesPalettes = false;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Jackals.oversize)
            {
                clothing1.GetSprite = null;
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.JackalClothes[72 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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

    class WrapTop : MainClothing
    {
        public WrapTop()
        {
            DiscardSprite = SpriteDictionary.HumenFundertops[64];
            blocksBreasts = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 70106;
            DiscardUsesPalettes = false;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Jackals.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFundertops[63];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.JackalClothes[48 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = (s) => SpriteDictionary.JackalClothes[56];
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            //clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }

    class BronzeBikini : MainClothing
    {
        public BronzeBikini()
        {
            DiscardSprite = SpriteDictionary.JackalClothes[69];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing4 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            clothing5 = new SpriteExtraInfo(10, null, null);
            Type = 70105;
            DiscardUsesPalettes = false;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Jackals.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.JackalClothes[24 + Math.Min(5, actor.Unit.BreastSize)];
                clothing4.GetSprite = (s) => SpriteDictionary.JackalClothes[30 + Math.Min(5, actor.Unit.BreastSize)];
                clothing5.GetSprite = (s) => SpriteDictionary.JackalClothes[94 + (actor.Unit.BodySize > 1 ? 1 : 0)];
                if (actor.GetLeftBreastSize() > 1)
                {
                    clothing1.GetSprite = null;
                    clothing3.GetSprite = null;
                }
                if (actor.GetRightBreastSize() > 1)
                {
                    clothing4.GetSprite = null;
                    clothing2.GetSprite = null;
                }
                blocksBreasts = false;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.JackalClothes[24 + Math.Min(5, actor.Unit.BreastSize)];
                clothing4.GetSprite = (s) => SpriteDictionary.JackalClothes[30 + Math.Min(5, actor.Unit.BreastSize)];
                clothing5.GetSprite = (s) => SpriteDictionary.JackalClothes[94 + (actor.Unit.BodySize > 1 ? 1 : 0)];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.JackalMain[24];
                    clothing3.GetSprite = (s) => SpriteDictionary.JackalMain[29];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.JackalMain[25];
                    clothing3.GetSprite = (s) => SpriteDictionary.JackalMain[30];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.JackalMain[26];
                    clothing3.GetSprite = (s) => SpriteDictionary.JackalMain[31];
                }
                else if (actor.Unit.BreastSize == 6)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.JackalMain[27];
                    clothing3.GetSprite = (s) => SpriteDictionary.JackalMain[32];
                }
                else if (actor.Unit.BreastSize == 7)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.JackalMain[28];
                    clothing3.GetSprite = (s) => SpriteDictionary.JackalMain[33];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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

            clothing1.GetPalette = null;
            clothing4.GetPalette = null;
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }

    class MaleTop1 : MainClothing
    {
        public MaleTop1()
        {
            DiscardSprite = SpriteDictionary.HumenMundertops[16];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 70109;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            clothing1.GetSprite = (s) => SpriteDictionary.HumenMundertops[15];

            base.Configure(sprite, actor);
        }
    }

    class MaleTop2 : MainClothing
    {
        public MaleTop2()
        {
            DiscardSprite = SpriteDictionary.HumenMundertops[16];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 70110;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            clothing1.GetSprite = (s) => SpriteDictionary.HumenMundertops[15];

            base.Configure(sprite, actor);
        }
    }


    class FemaleOnePiece1 : MainClothing
    {
        public FemaleOnePiece1()
        {
            DiscardSprite = SpriteDictionary.HumenFOnePieces[81];
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
            if (Races.Jackals.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[51];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[43 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[42];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[41];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[40];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[39];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[38];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[37];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[21];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[20];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[19];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[18];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[17];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[16];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[15];
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
            DiscardSprite = SpriteDictionary.HumenFOnePieces[80];
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
            if (Races.Jackals.oversize)
            {
                clothing1.GetSprite = null;
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[52 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[36];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[35];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[34];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[33];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[32];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[31];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[30];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[29];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[28];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[27];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[26];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[25];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[24];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[23];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[22];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[14];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[13];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[12];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[11];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[10];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[9];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[8];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[7];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[6];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[5];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[4];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[3];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[2];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[1];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[0];
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
            DiscardSprite = SpriteDictionary.HumenFOnePieces[79];
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
            if (Races.Jackals.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[69];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[61 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[36];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[35];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[34];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[33];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[32];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[31];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[30];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[29];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[28];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[27];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[26];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[25];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[24];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[23];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[22];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[14];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[13];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[12];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[11];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[10];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[9];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[8];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[7];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[6];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[5];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[4];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[3];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[2];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[1];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[0];
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
            DiscardSprite = SpriteDictionary.HumenFOnePieces[82];
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
            if (Races.Jackals.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[78];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[70 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[42];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[41];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[40];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[39];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[38];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[37];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[21];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[20];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[19];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[18];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[17];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[16];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.HumenFOnePieces[15];
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
            DiscardSprite = SpriteDictionary.HumenUnderbottoms[6];
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
                    clothing1.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[0 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[3 + actor.Unit.BodySize];
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
            DiscardSprite = SpriteDictionary.HumenUnderbottoms[13];
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
                    clothing1.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[7 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[10 + actor.Unit.BodySize];
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
            DiscardSprite = SpriteDictionary.HumenUnderbottoms[26];
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
                    clothing1.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[62];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[63];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[20 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[23 + actor.Unit.BodySize];
            }

            base.Configure(sprite, actor);
        }
    }

    class GenericBot4 : MainClothing
    {
        public GenericBot4()
        {
            DiscardSprite = SpriteDictionary.HumenUnderbottoms[39];
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
                    clothing1.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[33 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[36 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class GildedLoin : MainClothing
    {
        public GildedLoin()
        {
            DiscardSprite = SpriteDictionary.HumenUnderbottoms[39];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            clothing3 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 70111;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 4)
                    clothing1.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;
            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.JackalClothes[0 + actor.Unit.BodySize];
                clothing3.GetSprite = (s) => SpriteDictionary.JackalClothes[6 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.JackalClothes[3 + actor.Unit.BodySize];
                clothing3.GetSprite = (s) => SpriteDictionary.JackalClothes[9 + actor.Unit.BodySize];
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
            DiscardSprite = SpriteDictionary.HumenUnderbottoms[52];
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
                    clothing1.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[46 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[49 + actor.Unit.BodySize];
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
            DiscardSprite = SpriteDictionary.HumenUnderbottoms[59];
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
                    clothing1.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[53 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.HumenUnderbottoms[56 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class ClothSkirt : MainClothing
    {
        public ClothSkirt()
        {
            DiscardSprite = SpriteDictionary.JackalClothes[87];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(15, null, WhiteColored); // main
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored); // bottom
            clothing3 = new SpriteExtraInfo(14, null, null); // Colored Top
            clothing4 = new SpriteExtraInfo(14, null, null); // Colored bottom
            Type = 70113;
            DiscardUsesPalettes = false;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.JackalClothes[57 + Math.Min(8, actor.GetStomachSize(31, 0.7f))];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.JackalClothes[42 + ((actor.Unit.BodySize > 1) ? 1 : 0)];
                }
                if (actor.Unit.Furry)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.JackalClothes[44 + ((actor.Unit.BodySize > 1) ? 1 : 0)];
                    clothing4.GetSprite = (s) => SpriteDictionary.JackalClothes[90 + ((actor.Unit.BodySize > 1) ? 1 : 0)];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.JackalClothes[46 + ((actor.Unit.BodySize > 1) ? 1 : 0)];
                    clothing4.GetSprite = (s) => SpriteDictionary.JackalClothes[92 + ((actor.Unit.BodySize > 1) ? 1 : 0)];
                }
                clothing3.GetSprite = (s) => SpriteDictionary.JackalClothes[88 + ((actor.Unit.BodySize > 1) ? 1 : 0)];

            }
            else
            {
                int spr = 18 + ((actor.Unit.BodySize > 1) ? 2 : 0) + (actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0);
                clothing1.GetSprite = (s) => SpriteDictionary.JackalClothes[spr];
                clothing2.GetSprite = null;
                clothing3.GetSprite = (s) => SpriteDictionary.JackalClothes[spr - 4];
                clothing4.GetSprite = null;
            }
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }
    class PlainClothSkirt : MainClothing
    {
        public PlainClothSkirt()
        {
            DiscardSprite = SpriteDictionary.JackalClothes[87];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(13, null, WhiteColored);
            Type = 70112;
            DiscardUsesPalettes = false;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int spr = 0;
            if (actor.Unit.HasBreasts)
            {
                spr = 80 + actor.Unit.BodySize + (actor.Unit.Furry ? 3 : 0);
            }
            else
            {
                spr = 22 + (((actor.Unit.Furry || actor.Unit.BodySize > 1)) ? 1 : 0);
            }
            clothing1.GetSprite = (s) => SpriteDictionary.JackalClothes[spr];

            base.Configure(sprite, actor);
        }
    }

    class WristRings : MainClothing
    {
        public WristRings()
        {
            DiscardSprite = SpriteDictionary.JackalJewel[48];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(13, null, WhiteColored);
            Type = 70118;
            DiscardUsesPalettes = false;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int sprmod = 0;
            if (actor.GetWeaponSprite() == 2)
            {
                sprmod = (actor.Unit.BodySize > 1 ? 1 : 0) + (actor.Unit.HasBreasts ? 55 : 53);
            }
            else
            {
                sprmod = (actor.Unit.BodySize > 1 ? 12 : 0) + (actor.IsAttacking ? 1 : 0) + (actor.Unit.HasBreasts ? 0 : 6);
            }
            clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[sprmod];

            base.Configure(sprite, actor);
        }
    }
    class ArmRings : MainClothing
    {
        public ArmRings()
        {
            DiscardSprite = SpriteDictionary.JackalJewel[48];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(13, null, WhiteColored);
            Type = 70117;
            DiscardUsesPalettes = false;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int sprmod = (actor.Unit.BodySize > 1 ? 12 : 0) + (actor.IsAttacking ? 1 : 0);
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[2 + sprmod];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[4 + sprmod];
            }
            base.Configure(sprite, actor);
        }
    }

    class NeckRing : MainClothing
    {
        public NeckRing()
        {
            DiscardSprite = SpriteDictionary.JackalJewel[50];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(5, null, WhiteColored);
            Type = 70119;
            DiscardUsesPalettes = false;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int sprmod = (actor.Unit.HasBreasts ? 12 : 0);
            clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[24 + sprmod];
            base.Configure(sprite, actor);
        }
    }
    class GoldNecklaceWithColor : MainClothing
    {
        public GoldNecklaceWithColor()
        {
            DiscardSprite = SpriteDictionary.JackalJewel[52];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(5, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(20, null, null);
            Type = 70120;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int sprmod = (actor.Unit.HasBreasts ? 12 : 0);
            clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[9 + sprmod];
            clothing2.GetSprite = (s) => SpriteDictionary.JackalJewel[8 + sprmod];
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class GoldNecklace : MainClothing
    {
        public GoldNecklace()
        {
            DiscardSprite = SpriteDictionary.JackalJewel[51];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, WhiteColored);
            Type = 70121;
            DiscardUsesPalettes = false;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int sprmod = (actor.Unit.HasBreasts ? 12 : 0);
            clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[10 + sprmod];
            base.Configure(sprite, actor);
        }
    }
    class ColorNecklace : MainClothing
    {
        public ColorNecklace ()
        {
            DiscardSprite = SpriteDictionary.JackalJewel[52];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            Type = 70122;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int sprmod = (actor.Unit.HasBreasts ? 12 : 0);
            clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[11 + sprmod];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class LegRings : MainClothing
    {
        public LegRings()
        {
            DiscardSprite = SpriteDictionary.JackalJewel[48];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(5, null, WhiteColored);
            Type = 70114;
            DiscardUsesPalettes = false;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int sprmod = (actor.Unit.BodySize > 1 ? 1 : 0);
            if (actor.Unit.Furry)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[43 + sprmod];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[25 + sprmod];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[37 + sprmod];
            }
            base.Configure(sprite, actor);
        }
    }
    class LegRings2 : MainClothing
    {
        public LegRings2()
        {
            DiscardSprite = SpriteDictionary.JackalJewel[48];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(5, null, WhiteColored);
            Type = 70115;
            DiscardUsesPalettes = false;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int sprmod = (actor.Unit.BodySize > 1 ? 1 : 0);
            if (actor.Unit.Furry)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[45 + sprmod];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[27 + sprmod];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[39 + sprmod];
            }
            base.Configure(sprite, actor);
        }
    }
    class LegRings3 : MainClothing
    {
        public LegRings3()
        {
            DiscardSprite = SpriteDictionary.JackalJewel[48];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(5, null, WhiteColored);
            Type = 70116;
            DiscardUsesPalettes = false;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int sprmod = (actor.Unit.BodySize > 1 ? 1 : 0);
            if (actor.Unit.Furry)
            {
                if (actor.Unit.BodySize > 1)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[42];
                }
                else
                {
                    if (actor.Unit.HasBreasts)
                    {
                        clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[29];
                    }
                    else
                    {
                        clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[41];
                    }
                }

            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[29 + sprmod];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.JackalJewel[41 + sprmod];
            }
            base.Configure(sprite, actor);
        }
    }
}
