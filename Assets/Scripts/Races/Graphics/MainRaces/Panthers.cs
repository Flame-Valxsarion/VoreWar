using System;
using System.Collections.Generic;
using UnityEngine;


class Panthers : BlankSlate
{
    Sprite[] SpritesBase = SpriteDictionary.PantherBase;
    Sprite[] SpritesVore = SpriteDictionary.PantherVoreParts;


    enum ColorStyle
    {
        InnerWear,
        OuterWear,
        Other,
        None
    }

    internal List<MainClothing> AllClothing;

    bool oversize = false;

    public Panthers()
    {

        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(25, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, s.Unit.SkinColor));
        Dick = new SpriteExtraInfo(14, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(13, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, s.Unit.SkinColor));
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherBodyPaint, s.Unit.AccessoryColor));
        BodyAccent2 = new SpriteExtraInfo(3, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherBodyPaint, s.Unit.AccessoryColor));
        BodyAccent3 = new SpriteExtraInfo(3, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherBodyPaint, s.Unit.AccessoryColor));
        BodyAccent4 = new SpriteExtraInfo(3, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherBodyPaint, s.Unit.AccessoryColor));
        BodyAccent5 = new SpriteExtraInfo(26, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherBodyPaint, s.Unit.AccessoryColor));
        BodyAccent6 = new SpriteExtraInfo(1, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(12, WeaponSprite, WhiteColored);
        BodySize = new SpriteExtraInfo(23, BodySizeSprite, WhiteColored); //Weapon Flash
        Hair = new SpriteExtraInfo(27, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(0, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherHair, s.Unit.HairColor));
        Hair3 = new SpriteExtraInfo(27, HairSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, s.Unit.ClothingColor3));
        Breasts = new SpriteExtraInfo(19, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(19, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, s.Unit.SkinColor));

        Belly = new SpriteExtraInfo(16, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, s.Unit.SkinColor));

        EyeTypes = 2;
        HairStyles = 14;

        BodyAccentTypes1 = 4;
        BodyAccentTypes2 = 4;
        BodyAccentTypes3 = 3;
        BodyAccentTypes4 = 5;
        BodyAccentTypes5 = 4;

        ExtendedBreastSprites = true;

        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.PantherHair);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.PantherSkin);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.PantherBodyPaint);

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.PantherClothes);

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new GenericFemaleTop(0, 10, 20, SpriteDictionary.PantherFTops, 801, ColorStyle.InnerWear),
            new BeltTop(20),
            new GenericFemaleTop(19, 29, 20, SpriteDictionary.PantherFTops, 803, ColorStyle.InnerWear),
            new GenericFemaleTop(24, 34, 20, SpriteDictionary.PantherFTops, 804, ColorStyle.None),
            new GenericFemaleTop(39, 49, 20, SpriteDictionary.PantherFTops, 805, ColorStyle.InnerWear),
            new GenericFemaleTop(44, 54, 20, SpriteDictionary.PantherFTops, 806, ColorStyle.None),
            new GenericFemaleTop(59, 64, 20, SpriteDictionary.PantherFTops, 807, ColorStyle.InnerWear),
            new Simple(0, 5, 20, SpriteDictionary.PantherMTops, 808, ColorStyle.None, maleOnly: true),
            new Simple(1, 6, 20, SpriteDictionary.PantherMTops, 809, ColorStyle.InnerWear, maleOnly: true),
            new Simple(2, 7, 20, SpriteDictionary.PantherMTops, 810, ColorStyle.InnerWear, maleOnly: true),
            new Simple(3, 8, 20, SpriteDictionary.PantherMTops, 811, ColorStyle.None, maleOnly: true),
            new Simple(4, 9, 20, SpriteDictionary.PantherMTops, 812, ColorStyle.None, maleOnly: true),
            new GenericOnepiece(0,5, 18, false),
            new GenericOnepiece(9,14, 18, false),
            new GenericOnepiece(52,57, 69, true),
            new GenericOnepiece(60,65, 69, false),
        };

        AllowedWaistTypes = new List<MainClothing>() //Bottoms
        {
            new GenericBottom(0, 0, 12, 6, 8, SpriteDictionary.PantherBottoms, 840, true),
            new GenericBottom(1, 1, 12, 7, 8, SpriteDictionary.PantherBottoms, 841, true),
            new GenericBottom(2, 2, -2, 8, 8, SpriteDictionary.PantherBottoms, 842, true),
            new GenericBottom(3, 17, 14, 9, 8, SpriteDictionary.PantherBottoms, 843, false),
            new GenericBottom(16, 4, 12, 10, 8, SpriteDictionary.PantherBottoms, 844, true),
            new GenericBottom(5, 5, 12, 11, 8, SpriteDictionary.PantherBottoms, 845, true),
        };

        ExtraMainClothing1Types = new List<MainClothing>() //Overtops
        {
            new GenericFemaleTop(0, 5, 21, SpriteDictionary.PantherFOvertops, 830, ColorStyle.OuterWear),
            new SimpleAttack(20, 21, 22, 21, SpriteDictionary.PantherFOvertops, 831, ColorStyle.OuterWear, femaleOnly: true),
            new GenericFemaleTop(10, 15, 21, SpriteDictionary.PantherFOvertops, 832, ColorStyle.OuterWear),
            new BoneTop(21),
            new Simple(0, 6, 21, SpriteDictionary.PantherMOvertops, 834, ColorStyle.None, maleOnly: true),
            new Simple(1, 7, 21, SpriteDictionary.PantherMOvertops, 835, ColorStyle.OuterWear, maleOnly: true),
            new SimpleAttack(2, 4, 8, 21, SpriteDictionary.PantherMOvertops, 836, ColorStyle.OuterWear, maleOnly: true),
            new SimpleAttack(3, 5, 9, 21, SpriteDictionary.PantherMOvertops, 837, ColorStyle.OuterWear, maleOnly: true),

        };

        ExtraMainClothing2Types = new List<MainClothing>() //Overbottoms
        {
            new Simple(0, 10, 11, SpriteDictionary.PantherOverBottoms, 860, ColorStyle.OuterWear, blocksDick: true),
            new OverbottomTwoTone(1, 2, 3, 11, 11, 861),
            new Simple(4, 12, 11, SpriteDictionary.PantherOverBottoms, 862, ColorStyle.OuterWear, blocksDick: true),
            new OverbottomTwoTone(5, 5, 6, 13, 11, 863, blocksDick: true),
            new Simple(7, 14, 15, SpriteDictionary.PantherOverBottoms, 864, ColorStyle.None, blocksDick: false),
            new Simple(8, 15, 11, SpriteDictionary.PantherOverBottoms, 865, ColorStyle.None, femaleOnly: true, blocksDick: true),
            new Simple(9, 16, 11, SpriteDictionary.PantherOverBottoms, 866, ColorStyle.None, maleOnly: true, blocksDick: true),
        };

        ExtraMainClothing3Types = new List<MainClothing>() //Hats
        {
            new GenericItem(0, 2, 4, 28, SpriteDictionary.PantherHats, 888, ColorStyle.None),
            new GenericItem(1, 3, 5, 28, SpriteDictionary.PantherHats, 889, ColorStyle.Other),
            new SantaHat()
        };

        ExtraMainClothing4Types = new List<MainClothing>() //Gloves
        {
            new GenericGlovesPlusSecond(0, 890),
            new GenericGloves(9, 891),
            new GenericGloves(14, 892),
            new GenericGloves(19, 893),
            new GenericGloves(24, 894),
            new GenericGloves(29, 895),
            new GenericGloves(34, 896),
        };

        ExtraMainClothing5Types = new List<MainClothing>() //Legs
        {
            new GenericItem(0, 1, 2, 9, SpriteDictionary.PantherLegs, 901, ColorStyle.None),
            new GenericItem(3, 4, 5, 9, SpriteDictionary.PantherLegs, 902, ColorStyle.None),
            new GenericItem(6, 7, 8, 9, SpriteDictionary.PantherLegs, 903, ColorStyle.None),
            new GenericItem(9, 10, 11, 9, SpriteDictionary.PantherLegs, 904, ColorStyle.None),
            new GenericItem(12, 13, 14, 9, SpriteDictionary.PantherLegs, 905, ColorStyle.None),
        };

        AllClothing = new List<MainClothing>();
        AllClothing.AddRange(AllowedMainClothingTypes);
        AllClothing.AddRange(AllowedWaistTypes);
        AllClothing.AddRange(ExtraMainClothing1Types);
        AllClothing.AddRange(ExtraMainClothing2Types);
        AllClothing.AddRange(ExtraMainClothing3Types);
        AllClothing.AddRange(ExtraMainClothing4Types);
        AllClothing.AddRange(ExtraMainClothing5Types);

    }

    internal override int BreastSizes => 8;
    internal override int DickSizes => 4;

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        if (State.Rand.Next(10) == 0)
            unit.EyeType = 1;
        else
            unit.EyeType = 0;
        unit.ClothingColor = State.Rand.Next(clothingColors);
        unit.ClothingColor2 = State.Rand.Next(clothingColors);
        unit.ClothingColor3 = State.Rand.Next(clothingColors);
    }
    protected override Sprite BodySprite(Actor_Unit actor)
    {
        int attackingOffset = actor.IsAttacking ? 2 : 0;
        int GenderOffset = actor.Unit.HasBreasts ? 0 : 1;
        return SpritesBase[GenderOffset + attackingOffset];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        int attackingOffset = (actor.IsAttacking || actor.IsEating) ? 2 : 0;
        int GenderOffset = actor.Unit.HasBreasts ? 0 : 1;
        return SpritesBase[4 + GenderOffset + attackingOffset + (4 * actor.Unit.EyeType)];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor)
    {
        return SpritesBase[20 + (actor.Unit.HasBreasts ? 0 : 1)];
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            int weaponSprite = actor.GetWeaponSprite();
            int genderMod = actor.Unit.HasBreasts ? 0 : 1;
            switch (weaponSprite)
            {
                case 0:
                    return SpritesBase[22 + genderMod];
                case 1:
                    return SpritesBase[24 + genderMod];
                case 2:
                    return SpritesBase[28 + genderMod];
                case 3:
                    if (genderMod == 0)
                        AddOffset(Weapon, 4 * .625f, 0);
                    else
                        AddOffset(Weapon, 5 * .625f, 0);
                    return SpritesBase[30 + genderMod];
                case 4:
                    return SpritesBase[34 + genderMod];
                case 5:
                    return null;
                case 6:
                    return SpritesBase[40 + genderMod];
                case 7:
                    return null;
            }

            return null;
        }
        else
        {
            return null;
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            return SpritesBase[16 + actor.Unit.DickSize];
        }

        Dick.layer = 14;
        return SpritesBase[12 + actor.Unit.DickSize];
    }

    protected override Sprite BodySizeSprite(Actor_Unit actor) //Weapon flash
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            int weaponSprite = actor.GetWeaponSprite();
            int genderMod = actor.Unit.HasBreasts ? 0 : 1;
            switch (weaponSprite)
            {
                case 1:
                    return SpritesBase[26 + genderMod];
                case 3:
                    return SpritesBase[32 + genderMod];
            }

            return null;
        }
        else
        {
            return null;
        }
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        switch (actor.Unit.HairStyle)
        {
            case 0:
                return SpritesBase[46];
            case 1:
                return SpritesBase[49];
            case 2:
                return SpritesBase[51];
            case 3:
                return SpritesBase[54];
            case 4:
                return SpritesBase[57];
            case 5:
                return SpritesBase[60];
            case 6:
                return SpritesBase[62];
            case 7:
                return SpritesBase[64];
            case 8:
                return SpritesBase[65];
            case 9:
                return SpritesBase[66];
            case 10:
                return SpritesBase[68];
            case 11:
                return SpritesBase[70];
            case 12:
                return SpritesBase[72];
            case 13:
                return SpritesBase[73];
        }
        return null;
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        switch (actor.Unit.HairStyle)
        {
            case 0:
                return SpritesBase[48];
            case 2:
                return SpritesBase[53];
            case 3:
                return SpritesBase[56];
            case 4:
                return SpritesBase[59];
            case 10:
                return SpritesBase[69];
        }
        return null;
    }

    protected override Sprite HairSprite3(Actor_Unit actor)
    {
        switch (actor.Unit.HairStyle)
        {
            case 0:
                return SpritesBase[47];
            case 1:
                return SpritesBase[50];
            case 2:
                return SpritesBase[52];
            case 3:
                return SpritesBase[55];
            case 4:
                return SpritesBase[58];
            case 5:
                return SpritesBase[61];
            case 6:
                return SpritesBase[63];
            case 9:
                return SpritesBase[67];
            case 11:
                return SpritesBase[71];
        }
        return null;
    }

    /// <summary>
    /// Left Breast Sprite
    /// </summary>
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
                return SpritesVore[35];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return SpritesVore[34];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return SpritesVore[33];
            }

            if (leftSize > 28)
                leftSize = 28;


            return SpritesVore[4 + leftSize];
        }
        else
        {
            if (actor.Unit.DefaultBreastSize == 0)
                return SpritesVore[0];
            if (actor.SquishedBreasts && actor.Unit.BreastSize < 6 && actor.Unit.BreastSize >= 4)
                return SpritesVore[-2 + actor.Unit.BreastSize];
            return SpritesVore[3 + actor.Unit.BreastSize];
        }
    }

    /// <summary>
    /// Right Breast Sprite
    /// </summary>
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
                return SpritesVore[70];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return SpritesVore[69];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return SpritesVore[68];
            }

            if (rightSize > 28)
                rightSize = 28;

            return SpritesVore[39 + rightSize];
        }
        else
        {
            if (actor.Unit.DefaultBreastSize == 0)
                return SpritesVore[140];
            if (actor.SquishedBreasts && actor.Unit.BreastSize < 6 && actor.Unit.BreastSize >= 4)
                return SpritesVore[33 + actor.Unit.BreastSize];
            return SpritesVore[38 + actor.Unit.BreastSize];
        }
    }


    protected override Sprite BodyAccentSprite(Actor_Unit actor) //Arm Bodypaint
    {
        if (actor.Unit.BodyAccentType1 == 0)
            return null;
        if (actor.Unit.BodyAccentType1 >= BodyAccentTypes1)
            actor.Unit.BodyAccentType1 = BodyAccentTypes1 - 1;
        int genderOffset = actor.Unit.HasBreasts ? 0 : 1;
        int attackingOffset = actor.IsAttacking ? 2 : 0;
        return SpritesBase[74 + genderOffset + attackingOffset + 8 * (actor.Unit.BodyAccentType1 - 1)];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) //Shoulder Bodypaint
    {
        if (actor.Unit.BodyAccentType2 == 0)
            return null;
        if (actor.Unit.BodyAccentType2 >= BodyAccentTypes2)
            actor.Unit.BodyAccentType2 = BodyAccentTypes2 - 1;
        int genderOffset = actor.Unit.HasBreasts ? 0 : 1;
        int attackingOffset = actor.IsAttacking ? 2 : 0;
        return SpritesBase[78 + genderOffset + attackingOffset + 8 * (actor.Unit.BodyAccentType2 - 1)];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) //Feet Bodypaint
    {
        if (actor.Unit.BodyAccentType3 == 0)
            return null;
        if (actor.Unit.BodyAccentType3 >= BodyAccentTypes3)
            actor.Unit.BodyAccentType3 = BodyAccentTypes3 - 1;
        int genderOffset = actor.Unit.HasBreasts ? 0 : 1;
        return SpritesBase[98 + genderOffset + 2 * (actor.Unit.BodyAccentType3 - 1)];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) //Thigh Bodypaint
    {
        if (actor.Unit.BodyAccentType4 == 0)
            return null;
        if (actor.Unit.BodyAccentType4 >= BodyAccentTypes4)
            actor.Unit.BodyAccentType4 = BodyAccentTypes4 - 1;
        int genderOffset = actor.Unit.HasBreasts ? 0 : 1;
        return SpritesBase[102 + genderOffset + 2 * (actor.Unit.BodyAccentType4 - 1)];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) //Face Bodypaint
    {
        if (actor.Unit.BodyAccentType5 == 0)
            return null;
        if (actor.Unit.BodyAccentType5 >= BodyAccentTypes5)
            actor.Unit.BodyAccentType5 = BodyAccentTypes5 - 1;
        int genderOffset = actor.Unit.HasBreasts ? 0 : 1;
        int attackingOffset = actor.IsAttacking ? 2 : 0;
        return SpritesBase[110 + genderOffset + attackingOffset + 4 * (actor.Unit.BodyAccentType5 - 1)];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(32, 1.2f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
            {
                AddOffset(Belly, 0, -22 * .625f);
                return SpritesVore[145];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
            {
                AddOffset(Belly, 0, -22 * .625f);
                return SpritesVore[144];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -22 * .625f);
                return SpritesVore[143];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -22 * .625f);
                return SpritesVore[142];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -22 * .625f);
                return SpritesVore[141];
            }
            size = actor.GetStomachSize(29, 0.8f);
            switch (size)
            {
                case 24:
                    AddOffset(Belly, 0, -10 * .625f);
                    break;
                case 25:
                    AddOffset(Belly, 0, -13 * .625f);
                    break;
                case 26:
                    AddOffset(Belly, 0, -16 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -19 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -22 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -27 * .625f);
                    break;
            }

            if (actor.PredatorComponent.OnlyOnePreyAndLiving() && size >= 8 && size <= 13)
                return SpritesVore[71];

            return SpritesVore[74 + size];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.Unit.Predator == false)
            return SpritesVore[107 + actor.Unit.BallsSize];

        int size = actor.GetBallSize(31, .8f);
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) && size == 31)
        {
            AddOffset(Balls, 0, -19 * .625f);
            return SpritesVore[139];
        }
        else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) && size == 31)
        {
            AddOffset(Balls, 0, -16 * .625f);
            return SpritesVore[138];
        }
        else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) && size == 30)
        {
            AddOffset(Balls, 0, -15 * .625f);
            return SpritesVore[137];
        }
        if (size > 29)
            size = 29;
        switch (size)
        {
            case 26:
                AddOffset(Balls, 0, -2 * .625f);
                break;
            case 27:
                AddOffset(Balls, 0, -4 * .625f);
                break;
            case 28:
                AddOffset(Balls, 0, -7 * .625f);
                break;
            case 29:
                AddOffset(Balls, 0, -9 * .625f);
                break;
            case 30:
                AddOffset(Balls, 0, -10 * .625f);
                break;
            case 31:
                AddOffset(Balls, 0, -13 * .625f);
                break;
        }
        return SpritesVore[107 + size];
    }


    static void SetPaletteToColor(SpriteExtraInfo clothing, ColorStyle style)
    {
        switch (style)
        {
            case ColorStyle.InnerWear:
                clothing.GetColor = null;
                clothing.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, s.Unit.ClothingColor);
                break;
            case ColorStyle.OuterWear:
                clothing.GetColor = null;
                clothing.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, s.Unit.ClothingColor2);
                break;
            case ColorStyle.Other:
                clothing.GetColor = null;
                clothing.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, s.Unit.ClothingColor3);
                break;
            case ColorStyle.None:
                clothing.GetColor = WhiteColored;
                clothing.GetPalette = null;
                break;
        }
    }


    class Simple : MainClothing
    {
        int spr;
        Sprite[] sheet;

        public Simple(int sprite, int discard, int layer, Sprite[] sheet, int type, ColorStyle color, bool maleOnly = false, bool femaleOnly = false, bool blocksDick = false)
        {
            coversBreasts = false;
            this.blocksDick = blocksDick;
            spr = sprite;
            DiscardSprite = sheet[discard];
            this.sheet = sheet;
            Type = type;
            if (maleOnly)
                this.maleOnly = true;
            if (femaleOnly)
                this.femaleOnly = true;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);
            SetPaletteToColor(clothing1, color);
            FixedColor = clothing1.GetColor != null;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => sheet[spr];
            base.Configure(sprite, actor);
        }
    }

    class SimpleAttack : MainClothing
    {
        int spr;
        int attacksprite;
        Sprite[] sheet;

        public SimpleAttack(int sprite, int attacksprite, int discard, int layer, Sprite[] sheet, int type, ColorStyle color, bool maleOnly = false, bool femaleOnly = false)
        {
            spr = sprite;
            coversBreasts = false;
            blocksDick = false;
            this.attacksprite = attacksprite;
            DiscardSprite = sheet[discard];
            this.sheet = sheet;
            Type = type;
            if (maleOnly)
                this.maleOnly = true;
            if (femaleOnly)
                this.femaleOnly = true;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);
            SetPaletteToColor(clothing1, color);
            FixedColor = clothing1.GetColor != null;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.IsAttacking)
                clothing1.GetSprite = (s) => sheet[attacksprite];
            else
                clothing1.GetSprite = (s) => sheet[spr];
            base.Configure(sprite, actor);
        }
    }

    class GenericFemaleTop : MainClothing
    {
        int firstRowStart;
        int secondRowStart;
        Sprite[] sheet;
        public GenericFemaleTop(int firstRow, int secondRow, int layer, Sprite[] sheet, int type, ColorStyle color)
        {
            coversBreasts = false;
            blocksDick = false;
            this.sheet = sheet;
            firstRowStart = firstRow;
            secondRowStart = secondRow;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);
            DiscardSprite = sheet[secondRow + 4];
            Type = type;
            femaleOnly = true;
            SetPaletteToColor(clothing1, color);
            FixedColor = clothing1.GetColor != null;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (Races.Panthers.oversize)
                    clothing1.GetSprite = (s) => sheet[secondRowStart + 3];
                else if (actor.Unit.BreastSize < 3)
                    clothing1.GetSprite = (s) => sheet[firstRowStart + actor.Unit.BreastSize];
                else if (actor.Unit.BreastSize < 6)
                {
                    actor.SquishedBreasts = true;
                    clothing1.GetSprite = (s) => sheet[secondRowStart + actor.Unit.BreastSize - 3];
                }
                else // if (actor.Unit.BreastSize < 8)
                    clothing1.GetSprite = (s) => sheet[firstRowStart + actor.Unit.BreastSize - 3];
            }
            else
            {
                clothing1.GetSprite = (s) => sheet[firstRowStart];
            }
            base.Configure(sprite, actor);
        }
    }

    class GenericBottom : MainClothing
    {
        int sprM;
        int sprF;
        int bulge;
        Sprite[] sheet;
        public GenericBottom(int femaleSprite, int maleSprite, int bulge, int discard, int layer, Sprite[] sheet, int type, bool colored)
        {
            coversBreasts = false;
            blocksDick = true;
            sprF = femaleSprite;
            sprM = maleSprite;
            this.sheet = sheet;
            this.bulge = bulge;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(layer + 1, null, WhiteColored);
            DiscardSprite = sheet[discard];
            Type = type;
            if (colored)
            {
                SetPaletteToColor(clothing1, ColorStyle.InnerWear);
                SetPaletteToColor(clothing2, ColorStyle.InnerWear);
                clothing1.GetColor = null;
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, s.Unit.ClothingColor);
                clothing2.GetColor = null;
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, s.Unit.ClothingColor);
            }
            FixedColor = clothing1.GetColor != null;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasDick)
                clothing1.GetSprite = (s) => sheet[sprM];
            else
                clothing1.GetSprite = (s) => sheet[sprF];
            if (actor.Unit.HasDick && bulge > 0)
            {
                if (actor.Unit.DickSize > 2)
                    clothing2.GetSprite = (s) => sheet[bulge + 1];
                else
                    clothing2.GetSprite = (s) => sheet[bulge + 1];
            }
            else
                clothing2.GetSprite = null;
            base.Configure(sprite, actor);

        }
    }

    class BoneTop : MainClothing
    {
        Sprite[] sheet = SpriteDictionary.PantherFOvertops;
        public BoneTop(int layer)
        {
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(layer + 1, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, s.Unit.ClothingColor2));
            DiscardSprite = sheet[22];
            femaleOnly = true;
            Type = 870;
            FixedColor = clothing1.GetColor != null;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {


            if (actor.Unit.HasBreasts)
            {
                if (Races.Panthers.oversize)
                    clothing1.GetSprite = (s) => sheet[31];
                else if (actor.Unit.BreastSize < 3)
                    clothing1.GetSprite = (s) => sheet[23 + actor.Unit.BreastSize];
                else if (actor.Unit.BreastSize < 6)
                {
                    actor.SquishedBreasts = true;
                    clothing1.GetSprite = (s) => sheet[28 + actor.Unit.BreastSize - 3];
                }
                else //if (actor.Unit.BreastSize < 8)
                    clothing1.GetSprite = (s) => sheet[23 + actor.Unit.BreastSize - 3];
            }
            else
            {
                clothing1.GetSprite = (s) => sheet[18];
            }
            if (actor.IsAttacking)
                clothing2.GetSprite = (s) => sheet[21];
            else
                clothing2.GetSprite = (s) => sheet[20];
            base.Configure(sprite, actor);
        }

    }

    class BeltTop : MainClothing
    {
        Sprite[] sheet = SpriteDictionary.PantherFTops;
        public BeltTop(int layer)
        {
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);
            DiscardSprite = sheet[15 + 3];
            Type = 802;
            femaleOnly = true;
            FixedColor = clothing1.GetColor != null;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {


            if (actor.Unit.HasBreasts)
            {
                if (Races.Panthers.oversize)
                    clothing1.GetSprite = null;
                else if (actor.Unit.BreastSize < 3)
                    clothing1.GetSprite = (s) => sheet[5 + actor.Unit.BreastSize];
                else if (actor.Unit.BreastSize < 6)
                {
                    actor.SquishedBreasts = true;
                    clothing1.GetSprite = (s) => sheet[15 + actor.Unit.BreastSize - 3];
                }
                else //if (actor.Unit.BreastSize < 8)
                    clothing1.GetSprite = (s) => sheet[5 + actor.Unit.BreastSize - 3];

            }
            else
            {
                clothing1.GetSprite = (s) => sheet[5];
            }
            base.Configure(sprite, actor);
        }
    }

    class GenericItem : MainClothing
    {
        int sprM;
        int sprF;
        Sprite[] sheet = SpriteDictionary.PantherHats;

        public GenericItem(int femaleSprite, int maleSprite, int discard, int layer, Sprite[] sheet, int type, ColorStyle color)
        {
            coversBreasts = false;
            blocksDick = false;
            this.sheet = sheet;
            sprM = maleSprite;
            sprF = femaleSprite;
            DiscardSprite = sheet[discard];
            Type = type;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);
            SetPaletteToColor(clothing1, color);
            FixedColor = clothing1.GetColor != null;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
                clothing1.GetSprite = (s) => sheet[sprF];
            else
                clothing1.GetSprite = (s) => sheet[sprM];
            base.Configure(sprite, actor);
        }
    }

    class SantaHat : MainClothing
    {
        Sprite[] sheet = SpriteDictionary.PantherHats;

        public SantaHat()
        {
            coversBreasts = false;
            blocksDick = false;
            DiscardSprite = null;
            Type = 484841;
            clothing1 = new SpriteExtraInfo(28, null, WhiteColored);
            FixedColor = true;
            ReqWinterHoliday = true;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            clothing1.GetSprite = (s) => sheet[6];
            base.Configure(sprite, actor);
        }
    }

    class OverbottomTwoTone : MainClothing
    {
        int spr;
        int sprB;
        Sprite[] sheet = SpriteDictionary.PantherOverBottoms;

        public OverbottomTwoTone(int sprite, int bellySprite, int secondSprite, int discard, int layer, int type, bool blocksDick = false)
        {
            coversBreasts = false;
            spr = sprite;
            sprB = bellySprite;
            DiscardSprite = sheet[discard];
            Type = type;
            this.blocksDick = blocksDick;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(layer, null, WhiteColored);
            SetPaletteToColor(clothing1, ColorStyle.OuterWear);
            SetPaletteToColor(clothing2, ColorStyle.Other);
            clothing2.GetSprite = (s) => sheet[secondSprite];
            FixedColor = clothing1.GetColor != null;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.HasBelly)
                clothing1.GetSprite = (s) => sheet[sprB];
            else
                clothing1.GetSprite = (s) => sheet[spr];
            base.Configure(sprite, actor);
        }
    }

    class GenericGloves : MainClothing
    {
        int start;
        Sprite[] sheet = SpriteDictionary.PantherGloves;

        public GenericGloves(int start, int type)
        {
            coversBreasts = false;
            blocksDick = false;
            this.start = start;
            DiscardSprite = sheet[start + 4];
            Type = type;
            clothing1 = new SpriteExtraInfo(14, null, WhiteColored);
            FixedColor = clothing1.GetColor != null;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.IsAttacking)
                    clothing1.GetSprite = (s) => sheet[start + 1];
                else
                    clothing1.GetSprite = (s) => sheet[start];
            }
            else
            {
                if (actor.IsAttacking)
                    clothing1.GetSprite = (s) => sheet[start + 3];
                else
                    clothing1.GetSprite = (s) => sheet[start + 2];
            }

            base.Configure(sprite, actor);
        }
    }

    class GenericGlovesPlusSecond : MainClothing
    {
        int start;
        Sprite[] sheet = SpriteDictionary.PantherGloves;

        public GenericGlovesPlusSecond(int start, int type)
        {
            coversBreasts = false;
            blocksDick = false;
            this.start = start;
            DiscardSprite = sheet[start + 4];
            Type = type;
            clothing1 = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, s.Unit.ClothingColor2));
            clothing2 = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, s.Unit.ClothingColor3));
            FixedColor = clothing1.GetColor != null;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.IsAttacking)
                {
                    clothing1.GetSprite = (s) => sheet[start + 1];
                    clothing2.GetSprite = (s) => sheet[start + 5];
                }
                else
                {
                    clothing1.GetSprite = (s) => sheet[start];
                    clothing2.GetSprite = (s) => sheet[start + 4];
                }
            }
            else
            {
                if (actor.IsAttacking)
                {
                    clothing1.GetSprite = (s) => sheet[start + 3];
                    clothing2.GetSprite = (s) => sheet[start + 7];
                }
                else
                {
                    clothing1.GetSprite = (s) => sheet[start + 2];
                    clothing2.GetSprite = (s) => sheet[start + 6];
                }
            }

            base.Configure(sprite, actor);
        }
    }

    class GenericOnepiece : MainClothing
    {
        int firstRowStart;
        int secondRowStart;
        int finalStart;
        bool noPlusBreast;
        Sprite[] sheet = SpriteDictionary.PantherOnePiece;

        public GenericOnepiece(int firstRowStart, int secondRowStart, int finalStart, bool noPlusBreast)
        {
            coversBreasts = false;
            blocksDick = true;
            femaleOnly = true;
            this.firstRowStart = firstRowStart;
            this.secondRowStart = secondRowStart;
            this.finalStart = finalStart;
            this.noPlusBreast = noPlusBreast;
            DiscardSprite = sheet[finalStart];
            clothing1 = new SpriteExtraInfo(20, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, s.Unit.ClothingColor));
            clothing2 = new SpriteExtraInfo(8, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, s.Unit.ClothingColor));
            OccupiesAllSlots = true;
            FixedColor = clothing1.GetColor != null;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (Races.Panthers.oversize)
                {
                    if (noPlusBreast)
                        clothing1.GetSprite = null;
                    else
                        clothing1.GetSprite = (s) => sheet[secondRowStart + 3];
                }
                else if (actor.Unit.BreastSize < 3)
                    clothing1.GetSprite = (s) => sheet[firstRowStart + actor.Unit.BreastSize];
                else if (actor.Unit.BreastSize < 6)
                {
                    actor.SquishedBreasts = true;
                    clothing1.GetSprite = (s) => sheet[secondRowStart + actor.Unit.BreastSize - 3];
                }
                else if (actor.Unit.BreastSize < 8)
                    clothing1.GetSprite = (s) => sheet[firstRowStart + actor.Unit.BreastSize - 3];
            }
            else
            {
                clothing1.GetSprite = (s) => sheet[firstRowStart];
            }
            if (actor.HasBelly)
            {
                clothing2.GetSprite = (s) => sheet[finalStart + 1 + actor.GetStomachSize(32)];
                clothing2.layer = 17;
            }
            else
            {
                clothing2.GetSprite = (s) => sheet[finalStart + 1];
                clothing2.layer = 8;
            }

            base.Configure(sprite, actor);
        }
    }
}


