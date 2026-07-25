using System;
using System.Collections.Generic;
using UnityEngine;
//TODO:
// recolor bulges on clothes
// add color selection
// add wobble to imp belly accent

class Imps : DefaultRaceData
{
    Sprite[] SpritesBase = SpriteDictionary.NewimpBase;
    Sprite[] SpritesVore = SpriteDictionary.NewimpVore;
    Sprite[] SpritesGloves = SpriteDictionary.NewimpGloves;
    Sprite[] SpritesLegs = SpriteDictionary.NewimpLegs;
    Sprite[] SpritesUBottoms = SpriteDictionary.NewimpUBottoms;
    Sprite[] SpritesUTops = SpriteDictionary.NewimpUTops;
    Sprite[] SpritesOBottoms = SpriteDictionary.NewimpOBottoms;
    Sprite[] SpritesOTops = SpriteDictionary.NewImpOTops;
    Sprite[] SpritesOnePieces = SpriteDictionary.NewimpOnePieces;
    Sprite[] SpritesOverOnePieces = SpriteDictionary.NewImpOverOnePieces;
    Sprite[] SpritesHats = SpriteDictionary.NewimpHats;


    internal List<MainClothing> AllClothing;

    bool oversize = false;

    internal Imps()
    {
        SpecialAccessoryCount = 0;
        ClothingShift = new Vector3(0, 0, 0);
        AvoidedEyeTypes = 0;
        AvoidedMouthTypes = 0;

        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
        HairStyles = 16;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Imp);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ImpDark);
        EyeTypes = 8;
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
        SecondaryEyeColors = 1;
        BodySizes = 2;
        AllowedWaistTypes = new List<MainClothing>();
        MouthTypes = 0;
        AvoidedMainClothingTypes = 0;
        BodyAccentTypes1 = 4;
        BodyAccentTypes2 = 4;
        BodyAccentTypes3 = 4;
        BodyAccentTypes4 = 3;

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing50Spaced);

        ExtendedBreastSprites = true;

        AllowedMainClothingTypes = new List<MainClothing>() //undertops
        {
            new NewImpLeotard(),
            new NewImpCasinoBunny(),
            new NewImpUndertop1(),
            new NewImpUndertop2(),
            new NewImpUndertop3(),
            new NewImpUndertop4(),
            new NewImpUndertop5(),
        };

        AllowedClothingHatTypes = new List<ClothingAccessory>();


        AllowedWaistTypes = new List<MainClothing>() //underbottoms
        {
            new ImpUBottom(0, 2, 45, 8, 9, SpriteDictionary.NewimpUBottoms, 8808, true),
            new ImpUBottom(9, 11, 45, 17, 9, SpriteDictionary.NewimpUBottoms, 8817, true),
            new ImpUBottom(18, 20, 45, 26, 9, SpriteDictionary.NewimpUBottoms, 8826, false, black: true),
            new ImpUBottom(27, 29, 45, 35, 9, SpriteDictionary.NewimpUBottoms, 8835, true),
            new ImpUBottom(36, 38, 45, 44, 9, SpriteDictionary.NewimpUBottoms, 8844, true),
        };

        ExtraMainClothing1Types = new List<MainClothing>() //Overbottoms
        {
            new ImpOBottom(0, 2, false, 45, 8, 15, SpriteDictionary.NewimpOBottoms, 8908, true),
            new ImpOBottom(9, 11, false, 45, 17, 15, SpriteDictionary.NewimpOBottoms, 8917, true),
            new ImpOBottom(18, 20, true, 45, 26, 15, SpriteDictionary.NewimpOBottoms, 8926, true),
            new ImpOBottom(27, 29, true, 49, 35, 15, SpriteDictionary.NewimpOBottoms, 8935, true),
            new ImpOBottomAlt(27, 29, true, 49, 35, 15, SpriteDictionary.NewimpOBottoms, 8935, true),
            new ImpOBottom(36, 38, false, 45, 44, 15, SpriteDictionary.NewimpOBottoms, 8944, true),
        };

        ExtraMainClothing2Types = new List<MainClothing>() //Special clothing
        {
            new NewImpOverOPFem(),
            new NewImpOverOPM(),
            new NewImpOverTop1(),
            new NewImpOverTop2(),
            new NewImpOverTop3(),
            new NewImpOverTop4(),
        };

        ExtraMainClothing3Types = new List<MainClothing>() //Legs
        {
            new GenericLegs(0, 4, 45, 9004, femaleOnly: true, blocksDick: false),
            new GenericLegs(5, 9, 45, 9009, femaleOnly: true, blocksDick: false),
            new GenericLegs(10, 14, 45, 9019, femaleOnly: true, blocksDick: false),
            new GenericLegsAlt(10, 14, 45, 9019, femaleOnly: true, blocksDick: false),
            new GenericLegs(15, 19, 45, 9015, femaleOnly: true, blocksDick: false),
            new GenericLegsAlt(15, 19, 45, 9015, femaleOnly: true, blocksDick: false),
            new GenericLegs(20, 24, 45, 9020, femaleOnly: true, blocksDick: false),
            new GenericLegs(2, 4, 45, 9002, maleOnly: true, blocksDick: true, black: true),
            new GenericLegs(7, 9, 45, 9007, maleOnly: true, blocksDick: true, black: true),
            new GenericLegs(12, 14, 45, 9012, maleOnly: true, blocksDick: true),
            new GenericLegsAlt(12, 14, 45, 9012, maleOnly: true, blocksDick: true),
            new GenericLegs(17, 19, 45, 9017, maleOnly: true, blocksDick: true),
            new GenericLegsAlt(17, 19, 45, 9017, maleOnly: true, blocksDick: true),
            new GenericLegs(22, 24, 45, 9022, maleOnly: true, blocksDick: false),

        };

        ExtraMainClothing4Types = new List<MainClothing>() //Gloves
        {
            new GenericGloves(0, 8, 9108),
            new GenericGlovesPlusSecond(9, 17, 9117),
            new GenericGlovesPlusSecondAlt(9, 17, 9117),
            new GenericGlovesPlusSecond(18, 26, 9126),
            new GenericGlovesPlusSecondAlt(18, 26, 9126),
            new GenericGloves(27, 35, 9135),

        };

        ExtraMainClothing5Types = new List<MainClothing>() //Hats
        {
            new Hat(0, 0, SpriteDictionary.NewimpHats, 666),
            new Hat(34, 0, SpriteDictionary.NewimpHats, 666),
            new HolidayHat(),
        };

        AllClothing = new List<MainClothing>();
        AllClothing.AddRange(AllowedMainClothingTypes);
        AllClothing.AddRange(ExtraMainClothing3Types);
        AllClothing.AddRange(ExtraMainClothing4Types);
        AllClothing.AddRange(ExtraMainClothing5Types);


        ColorSwapPalette ImpColor(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType1 > 1)
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, actor.Unit.AccessoryColor);
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, actor.Unit.SkinColor);
        }
        ColorSwapPalette ImpHorn(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType2 != 0)
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, actor.Unit.AccessoryColor);
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, actor.Unit.SkinColor);
        }

        ColorSwapPalette ImpBack(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType2 != 0)
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, actor.Unit.AccessoryColor);
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, actor.Unit.SkinColor);
        }
        ColorSwapPalette ImpWing(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType2 != 0)
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, actor.Unit.SkinColor);
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, actor.Unit.AccessoryColor);

        }

        ColorSwapPalette ImpBelly(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType1 != 3)
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, actor.Unit.SkinColor);
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, actor.Unit.AccessoryColor);

        }

        ColorSwapPalette ImpLeftTit(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType1 <= 1)
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, actor.Unit.SkinColor);
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, actor.Unit.AccessoryColor);

        }

        ColorSwapPalette ImpRightTit(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType1 == 2)
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, actor.Unit.AccessoryColor);
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, actor.Unit.SkinColor);

        }

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, s.Unit.SkinColor));
        BodyAccessory = null;
        BodyAccent = new SpriteExtraInfo(5, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, s.Unit.AccessoryColor));
        BodyAccent2 = new SpriteExtraInfo(7, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, s.Unit.AccessoryColor));
        BodyAccent3 = new SpriteExtraInfo(10, BodyAccentSprite3, null, (s) => ImpHorn(s));
        BodyAccent4 = new SpriteExtraInfo(1, BodyAccentSprite4, null, (s) => ImpBack(s));
        BodyAccent5 = new SpriteExtraInfo(1, BodyAccentSprite5, null, (s) => ImpWing(s));
        BodyAccent6 = new SpriteExtraInfo(18, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, s.Unit.AccessoryColor));
        BodyAccent7 = null;
        BodyAccent8 = null;
        BodyAccent9 = null;
        BodyAccent10 = null;
        Mouth = null;
        Hair = new SpriteExtraInfo(9, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(2, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Eyes = new SpriteExtraInfo(8, EyesSprite, WhiteColored);
        SecondaryEyes = new SpriteExtraInfo(7, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(17, null, null, (s) => ImpBelly(s));
        Weapon = new SpriteExtraInfo(4, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(19, BreastsSprite, null, (s) => ImpLeftTit(s));
        SecondaryBreasts = new SpriteExtraInfo(19, SecondaryBreastsSprite, null, (s) => ImpRightTit(s));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(14, DickSprite, null, (s) => ImpColor(s));
        Balls = new SpriteExtraInfo(13, BallsSprite, null, (s) => ImpColor(s));
    }

    internal override int BreastSizes => 7;
    internal override int DickSizes => 4;

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.SkinColor = State.Rand.Next(SkinColors);
        unit.EyeType = State.Rand.Next(EyeTypes);
        unit.HairStyle = State.Rand.Next(HairStyles);
        unit.HairColor = State.Rand.Next(HairColors);
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
        unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3);
        unit.BodyAccentType4 = State.Rand.Next(BodyAccentTypes4);
        unit.ClothingColor = State.Rand.Next(clothingColors);
        unit.ClothingColor2 = State.Rand.Next(clothingColors);
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => null;
    protected override Sprite BackWeaponSprite(Actor_Unit actor) => null;
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(32, 1.2f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
            {
                AddOffset(Belly, 0, -31 * .625f);
                return SpritesVore[105];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
            {
                AddOffset(Belly, 0, -30 * .625f);
                return SpritesVore[104];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size >= 30)
            {
                AddOffset(Belly, 0, -30 * .625f);
                return SpritesVore[103];
            }
            switch (size)
            {
                case 24:
                    AddOffset(Belly, 0, -3 * .625f);
                    break;
                case 25:
                    AddOffset(Belly, 0, -8 * .625f);
                    break;
                case 26:
                    AddOffset(Belly, 0, -9 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -13 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -16 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -23 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -25 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -27 * .625f);
                    break;
                case 32:
                    AddOffset(Belly, 0, -30 * .625f);
                    break;
            }

            //if (actor.PredatorComponent.OnlyOnePreyAndLiving() && size >= 8 && size <= 13)
            //    return SpritesVore[71];

            return SpritesVore[70 + size];
        }
        else
        {
            return null;
        }
    }
    protected override Sprite BodyAccentSprite(Actor_Unit actor) //Arm Bodypaint
    {
        if (actor.Unit.BodyAccentType1 == 0)
            return null;
        if (actor.Unit.BodyAccentType1 >= BodyAccentTypes1)
            actor.Unit.BodyAccentType1 = BodyAccentTypes1 - 1;
        int genderOffset = actor.Unit.HasBreasts ? 0 : 4;
        int attackingOffset = actor.IsAttacking ? 1 : 0;
        int weightMod = actor.Unit.BodySize * 2;
        return SpritesBase[8 + genderOffset + attackingOffset + weightMod + 8 * (actor.Unit.BodyAccentType1 - 1)];
    }
    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType2 == 0)
            return null;
        if (actor.Unit.BodyAccentType2 >= BodyAccentTypes2)
            actor.Unit.BodyAccentType2 = BodyAccentTypes2 - 1;
        return SpritesBase[36 + (actor.Unit.BodyAccentType2 - 1)];
    }
    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        int sprite = 136;
        sprite += actor.Unit.BodyAccentType3;
        if (actor.Unit.BodyAccentType3 == 0)
        {
            BodyAccent3.layer = 6;
            return SpritesBase[sprite];
        }
        else
        {
            BodyAccent3.layer = 9;
            return SpritesBase[sprite];
        }
    }
    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        int sprite = 140;
        sprite += actor.Unit.BodyAccentType4;
        return SpritesBase[sprite];
    }
    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType4 == 2)
        {
            return SpritesBase[143];
        }
        else
            return null;
    }
    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Animation syncing with belly handled in "CompleteSprite.cs" "Void UpdateSprite"
    {
        if (actor.Unit.BodyAccentType1 == 2)
        {
            if (actor.HasBelly)
            {
                int size = actor.GetStomachSize(32, 1.2f);
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    AddOffset(BodyAccent6, 0, -31 * .625f);
                    return SpritesVore[141];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    AddOffset(BodyAccent6, 0, -30 * .625f);
                    return SpritesVore[140];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size >= 30)
                {
                    AddOffset(BodyAccent6, 0, -30 * .625f);
                    return SpritesVore[139];
                }
                switch (size)
                {
                    case 24:
                        AddOffset(BodyAccent6, 0, -3 * .625f);
                        break;
                    case 25:
                        AddOffset(BodyAccent6, 0, -8 * .625f);
                        break;
                    case 26:
                        AddOffset(BodyAccent6, 0, -9 * .625f);
                        break;
                    case 27:
                        AddOffset(BodyAccent6, 0, -13 * .625f);
                        break;
                    case 28:
                        AddOffset(BodyAccent6, 0, -16 * .625f);
                        break;
                    case 29:
                        AddOffset(BodyAccent6, 0, -23 * .625f);
                        break;
                    case 30:
                        AddOffset(BodyAccent6, 0, -25 * .625f);
                        break;
                    case 31:
                        AddOffset(BodyAccent6, 0, -27 * .625f);
                        break;
                    case 32:
                        AddOffset(BodyAccent6, 0, -30 * .625f);
                        break;
                }

                //if (actor.PredatorComponent.OnlyOnePreyAndLiving() && size >= 8 && size <= 13)
                //    return SpritesVore[71];

                return SpritesVore[106 + size];
            }
            else
            {
                return null;
            }
        }
        else
            return null;
    }
    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BodySprite(Actor_Unit actor)
    {
        int weightMod = actor.Unit.BodySize * 2;
        if (weightMod < 0) //I can't believe I had to add this, but had an exception here.  
            weightMod = 0;
        if (actor.Unit.HasBreasts == true)
        {
            if (actor.IsAttacking)
                return SpritesBase[1 + weightMod];
            return SpritesBase[0 + weightMod];
        }
        else
        {
            if (actor.IsAttacking)
                return SpritesBase[5 + weightMod];
            return SpritesBase[4 + weightMod];
        }
    }
    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        int attackingOffset = actor.IsAttacking ? 1 : 0;
        int eatingOffset = actor.IsEating ? 2 : 0;
        int hurtOffset = (actor.Unit.IsDead && actor.Unit.Items != null) ? 3 : 0;
        return SpritesBase[32 + attackingOffset + eatingOffset + hurtOffset];
    }
    protected override Sprite BreastsShadowSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        oversize = false;
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(22 * 22, 1f));
            if (leftSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 22)
            {
                return SpritesVore[21];
            }
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 22)
            {
                return SpritesVore[20];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 20)
            {
                return SpritesVore[19];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 18)
            {
                return SpritesVore[18];
            }

            if (leftSize > 17)
                leftSize = 17;


            return SpritesVore[leftSize];
        }
        else
        {
            if (actor.Unit.DefaultBreastSize == 0)
                return SpritesVore[0];
            return SpritesVore[0 + actor.Unit.BreastSize];
        }
    }
    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(22 * 22, 1f));
            if (rightSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 22)
            {
                return SpritesVore[43];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 22)
            {
                return SpritesVore[42];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 20)
            {
                return SpritesVore[41];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 18)
            {
                return SpritesVore[40];
            }

            if (rightSize > 17)
                rightSize = 17;

            return SpritesVore[22 + rightSize];
        }
        else
        {
            if (actor.Unit.DefaultBreastSize == 0)
                return SpritesVore[22];
            return SpritesVore[22 + actor.Unit.BreastSize];
        }
    }
    internal override Color ClothingColor(Actor_Unit actor) => Color.white;
    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            if (actor.PredatorComponent?.VisibleFullness < .75f)
            {
                Dick.layer = 21;
                return SpritesBase[129 + 2 * actor.Unit.DickSize];
            }
            else
            {
                Dick.layer = 14;
                return SpritesBase[129 + 2 * actor.Unit.DickSize];
            }
        }

        Dick.layer = 14;
        return SpritesBase[128 + 2 * actor.Unit.DickSize];


    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        int size = actor.GetBallSize(22, .8f);
        int baseSize = (actor.Unit.DickSize + 1) / 3;

        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && size == 22)
        {
            AddOffset(Balls, 0, -24 * .625f);
            return SpritesVore[69];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 22)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return SpritesVore[68];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 20)
        {
            AddOffset(Balls, 0, -20 * .625f);
            return SpritesVore[67];
        }
        int combined = Math.Min(baseSize + size + 2, 22);
        if (combined == 22)
            AddOffset(Balls, 0, -10 * .625f);
        else if (combined >= 21)
            AddOffset(Balls, 0, -8 * .625f);
        else if (combined >= 20)
            AddOffset(Balls, 0, -6 * .625f);
        else if (combined >= 19)
            AddOffset(Balls, 0, -4 * .625f);
        else if (combined >= 18)
            AddOffset(Balls, 0, -1 * .625f);
        if (size > 0)
        {
            return SpritesVore[44 + combined];
        }
        return SpritesVore[44 + baseSize];
    }

    protected override Color EyeColor(Actor_Unit actor) => Color.white;
    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
        if (actor.Unit.IsDead && actor.Unit.Items != null)
            return null;
        else
        {
            int sprite = 72;
            sprite += actor.Unit.EyeType;
            return SpritesBase[sprite];
        }
    }
    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            int sprite = 80;
            sprite += actor.Unit.EyeType;
            return SpritesBase[sprite];
        }
        else
        {
            int sprite = 40;
            int attackingOffset = actor.IsAttacking ? 1 : 0;
            if (actor.Unit.EyeType > 8)
            {
                sprite += 2 * actor.Unit.EyeType;
                return SpritesBase[(sprite - 16) + attackingOffset];
            }
            else
            {
                sprite += 2 * actor.Unit.EyeType;
                return SpritesBase[sprite + attackingOffset];
            }
        }
    }
    // protected override Color HairColor(Actor_Unit actor) => Color.white;

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        if (actor.Unit.ClothingExtraType5 == 1)
        {
            return SpritesHats[2 + 2 * actor.Unit.HairStyle];
        }
        else if (actor.Unit.ClothingExtraType5 == 2)
        {
            return SpritesHats[36 + 2 * actor.Unit.HairStyle];
        }
        else
            return SpritesBase[96 + 2 * actor.Unit.HairStyle];
    }
    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        if (actor.Unit.ClothingExtraType5 == 1)
        {
            return SpritesHats[3 + 2 * actor.Unit.HairStyle];
        }
        else if (actor.Unit.ClothingExtraType5 == 2)
        {
            return SpritesHats[37 + 2 * actor.Unit.HairStyle];
        }
        else
            return SpritesBase[97 + 2 * actor.Unit.HairStyle];
    }
    protected override Sprite MouthSprite(Actor_Unit actor) => null;
    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            if (actor.GetWeaponSprite() == 5)
                return null;
            return SpritesBase[88 + actor.GetWeaponSprite()];
        }
        else
        {
            return null;
        }
    }
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => null;


    class GenericGloves : MainClothing
    {
        int start;
        Sprite[] sheet = SpriteDictionary.NewimpGloves;

        public GenericGloves(int start, int discard, int type)//int type
        {
            coversBreasts = false;
            blocksDick = false;
            this.start = start;
            DiscardSprite = sheet[discard];
            Type = type;
            clothing1 = new SpriteExtraInfo(18, null, null);
            FixedColor = false;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                int weightMod = actor.Unit.BodySize * 2;
                if (actor.IsAttacking)
                {
                    clothing1.GetSprite = (s) => sheet[start + 1 + weightMod];
                }
                else
                {
                    clothing1.GetSprite = (s) => sheet[start + weightMod];
                }
            }
            else
            {
                int weightMod = actor.Unit.BodySize * 2;
                if (actor.IsAttacking)
                {
                    clothing1.GetSprite = (s) => sheet[start + 5 + weightMod];
                }
                else
                {
                    clothing1.GetSprite = (s) => sheet[start + 4 + weightMod];
                }
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class GenericGlovesPlusSecond : MainClothing
    {
        int start;
        Sprite[] sheet = SpriteDictionary.NewimpGloves;

        public GenericGlovesPlusSecond(int start, int discard, int type) //int type
        {
            coversBreasts = false;
            blocksDick = false;
            this.start = start;
            DiscardSprite = sheet[discard];
            Type = type;
            clothing1 = new SpriteExtraInfo(18, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.ClothingColor));
            FixedColor = clothing1.GetColor != null;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                int weightMod = actor.Unit.BodySize * 2;
                if (actor.IsAttacking)
                {
                    clothing1.GetSprite = (s) => sheet[start + 1 + weightMod];
                }
                else
                {
                    clothing1.GetSprite = (s) => sheet[start + weightMod];
                }
            }
            else
            {
                int weightMod = actor.Unit.BodySize * 2;
                if (actor.IsAttacking)
                {
                    clothing1.GetSprite = (s) => sheet[start + 5 + weightMod];
                }
                else
                {
                    clothing1.GetSprite = (s) => sheet[start + 4 + weightMod];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericGlovesPlusSecondAlt : MainClothing
    {
        int start;
        Sprite[] sheet = SpriteDictionary.NewimpGloves;

        public GenericGlovesPlusSecondAlt(int start, int discard, int type) //int type
        {
            coversBreasts = false;
            blocksDick = false;
            this.start = start;
            DiscardSprite = sheet[discard];
            Type = type;
            clothing1 = new SpriteExtraInfo(18, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.ClothingColor2));
            FixedColor = clothing1.GetColor != null;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                int weightMod = actor.Unit.BodySize * 2;
                if (actor.IsAttacking)
                {
                    clothing1.GetSprite = (s) => sheet[start + 1 + weightMod];
                }
                else
                {
                    clothing1.GetSprite = (s) => sheet[start + weightMod];
                }
            }
            else
            {
                int weightMod = actor.Unit.BodySize * 2;
                if (actor.IsAttacking)
                {
                    clothing1.GetSprite = (s) => sheet[start + 5 + weightMod];
                }
                else
                {
                    clothing1.GetSprite = (s) => sheet[start + 4 + weightMod];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
            base.Configure(sprite, actor);
        }
    }
    class GenericLegs : MainClothing
    {
        int start;
        int bulge;
        bool black;
        Sprite[] sheet = SpriteDictionary.NewimpLegs;

        public GenericLegs(int start, int discard, int bulge, int type, bool maleOnly = false, bool femaleOnly = false, bool blocksDick = false, bool black = false)
        {
            coversBreasts = false;
            this.blocksDick = blocksDick;
            this.start = start;
            this.bulge = bulge;
            this.black = black;
            Type = type;
            DiscardSprite = sheet[discard];
            if (maleOnly)
                this.maleOnly = true;
            if (femaleOnly)
                this.femaleOnly = true;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(11, null, WhiteColored);
            FixedColor = clothing1.GetColor != null;


        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;
            clothing1.GetSprite = (s) => sheet[start + weightMod];
            if (actor.Unit.HasDick)
            {
                if (blocksDick == true)
                {
                    if (black == true)
                        clothing2.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[bulge + 4 + actor.Unit.DickSize];
                    else
                        clothing2.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[bulge + actor.Unit.DickSize];
                }
                else
                    clothing2.GetSprite = null;
            }
            else
                clothing2.GetSprite = null;
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }

    }

    class GenericLegsAlt : MainClothing
    {
        int start;
        int bulge;
        bool black;
        Sprite[] sheet = SpriteDictionary.NewimpLegs;

        public GenericLegsAlt(int start, int discard, int bulge, int type, bool maleOnly = false, bool femaleOnly = false, bool blocksDick = false, bool black = false)
        {
            coversBreasts = false;
            this.blocksDick = blocksDick;
            this.start = start;
            this.bulge = bulge;
            this.black = black;
            Type = type;
            DiscardSprite = sheet[discard];
            if (maleOnly)
                this.maleOnly = true;
            if (femaleOnly)
                this.femaleOnly = true;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(11, null, WhiteColored);
            FixedColor = clothing1.GetColor != null;


        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;
            clothing1.GetSprite = (s) => sheet[start + weightMod];
            if (actor.Unit.HasDick)
            {
                if (blocksDick == true)
                {
                    if (black == true)
                        clothing2.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[bulge + 4 + actor.Unit.DickSize];
                    else
                        clothing2.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[bulge + actor.Unit.DickSize];
                }
                else
                    clothing2.GetSprite = null;
            }
            else
                clothing2.GetSprite = null;
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
            base.Configure(sprite, actor);
        }
        //dongs
    }

    class ImpUBottom : MainClothing
    {
        int sprM;
        int sprF;
        int bulge;
        bool black;
        Sprite[] sheet;
        public ImpUBottom(int femaleSprite, int maleSprite, int bulge, int discard, int layer, Sprite[] sheet, int type, bool colored = false, bool black = false)
        {
            coversBreasts = false;
            blocksDick = true;
            sprF = femaleSprite;
            sprM = maleSprite;
            this.sheet = sheet;
            this.bulge = bulge;
            this.black = black;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(layer + 1, null, WhiteColored);
            DiscardSprite = sheet[discard];
            Type = type;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;
            if (actor.HasBelly)
            {
                if (actor.Unit.HasBreasts)
                    clothing1.GetSprite = (s) => sheet[sprF + 4 + weightMod];
                else
                    clothing1.GetSprite = (s) => sheet[sprM + 4 + weightMod];
                if (actor.Unit.HasDick)
                {
                    if (blocksDick == true)
                    {
                        if (black == true)
                            clothing2.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[bulge + 4 + actor.Unit.DickSize];
                        else
                            clothing2.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[bulge + actor.Unit.DickSize];
                    }
                    else
                        clothing2.GetSprite = null;
                }
                else
                    clothing2.GetSprite = null;
            }
            else
            {
                if (actor.Unit.HasBreasts)
                    clothing1.GetSprite = (s) => sheet[sprF + weightMod];
                else
                    clothing1.GetSprite = (s) => sheet[sprM + weightMod];
                if (actor.Unit.HasDick)
                {
                    if (blocksDick == true)
                    {
                        if (black == true)
                            clothing2.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[bulge + 4 + actor.Unit.DickSize];
                        else
                            clothing2.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[bulge + actor.Unit.DickSize];
                    }
                    else
                        clothing2.GetSprite = null;
                }
                else
                    clothing2.GetSprite = null;
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
            base.Configure(sprite, actor);

        }

    }
    class NewImpLeotard : MainClothing
    {
        public NewImpLeotard()
        {
            DiscardSprite = SpriteDictionary.NewimpOnePieces[74];
            Type = 11001;
            colorsBelly = false;
            blocksDick = true;
            coversBreasts = false;
            OccupiesAllSlots = true;
            DiscardUsesPalettes = true;
            clothing1 = new SpriteExtraInfo(16, null, null);
            clothing2 = new SpriteExtraInfo(20, null, null);
            clothing3 = new SpriteExtraInfo(13, null, null);
        }


        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;
            int bobs = actor.Unit.BreastSize;
            if (bobs > 7)
                bobs = 7;
            int size = actor.GetStomachSize(32, 1.2f);
            if (size > 7)
                size = 7;
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.HasDick)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[45 + actor.Unit.DickSize];
                }
                else clothing3.GetSprite = null;


                clothing2.GetSprite = (s) => SpriteDictionary.NewimpOnePieces[33 + bobs];
                clothing1.GetSprite = (s) => SpriteDictionary.NewimpOnePieces[41 + size + 8 * weightMod];

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                //bellyPalette = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SkinToClothing, actor.Unit.ClothingColor);
                base.Configure(sprite, actor);
            }
            else
            {
                if (actor.Unit.HasDick)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[45 + actor.Unit.DickSize];
                }
                else clothing3.GetSprite = null;
                clothing2.GetSprite = null;
                clothing1.GetSprite = (s) => SpriteDictionary.NewimpOnePieces[58 + size + 8 * weightMod];
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                base.Configure(sprite, actor);
            }
        }
    }

    class NewImpCasinoBunny : MainClothing
    {
        public NewImpCasinoBunny()
        {
            DiscardSprite = SpriteDictionary.NewimpOnePieces[74];
            Type = 11011;
            colorsBelly = false;
            blocksDick = true;
            OccupiesAllSlots = true;
            DiscardUsesPalettes = true;
            clothing1 = new SpriteExtraInfo(16, null, null);
            clothing2 = new SpriteExtraInfo(20, null, null);
            clothing3 = new SpriteExtraInfo(13, null, null);
            coversBreasts = false;
        }


        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;
            int bobs = actor.Unit.BreastSize;
            if (bobs > 7)
                bobs = 7;
            int size = actor.GetStomachSize(32, 1.2f);
            if (size > 5)
                size = 5;
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.HasDick)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[45 + actor.Unit.DickSize];
                }
                else clothing3.GetSprite = null;


                clothing2.GetSprite = (s) => SpriteDictionary.NewimpOnePieces[0 + bobs];
                clothing1.GetSprite = (s) => SpriteDictionary.NewimpOnePieces[8 + size + 6 * weightMod];

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                //bellyPalette = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SkinToClothing, actor.Unit.ClothingColor);
                base.Configure(sprite, actor);
            }
            else
            {

                clothing3.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[45 + actor.Unit.DickSize];
                clothing2.GetSprite = null;
                clothing1.GetSprite = (s) => SpriteDictionary.NewimpOnePieces[20 + size + 6 * weightMod];
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);

                base.Configure(sprite, actor);
            }
        }
    }

    class ImpOBottom : MainClothing
    {
        int sprM;
        int sprF;
        int bulge;
        bool showbulge;
        Sprite[] sheet;
        public ImpOBottom(int femaleSprite, int maleSprite, bool showbulge, int bulge, int discard, int layer, Sprite[] sheet, int type, bool colored = false)
        {
            coversBreasts = false;
            blocksDick = true;
            sprF = femaleSprite;
            sprM = maleSprite;
            this.sheet = sheet;
            this.bulge = bulge;
            this.showbulge = showbulge;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(layer + 1, null, WhiteColored);
            DiscardSprite = sheet[discard];
            Type = type;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;
            if (actor.HasBelly)
            {
                if (actor.Unit.HasBreasts)
                    clothing1.GetSprite = (s) => sheet[sprF + 4 + weightMod];
                else
                    clothing1.GetSprite = (s) => sheet[sprM + 4 + weightMod];
                if (actor.Unit.HasDick && showbulge == true)
                {
                    if (blocksDick == true)
                        clothing2.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[Math.Min(bulge + actor.Unit.DickSize, 52)];
                    else
                        clothing2.GetSprite = null;
                }
                else
                    clothing2.GetSprite = null;
            }
            else
            {
                if (actor.Unit.HasBreasts)
                    clothing1.GetSprite = (s) => sheet[sprF + weightMod];
                else
                    clothing1.GetSprite = (s) => sheet[sprM + weightMod];
                if (actor.Unit.HasDick)
                {
                    if (blocksDick == true && showbulge == true)
                        clothing2.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[Math.Min(bulge + actor.Unit.DickSize, 52)];
                    else
                        clothing2.GetSprite = null;
                }
                else
                    clothing2.GetSprite = null;
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);

        }

    }

    class ImpOBottomAlt : MainClothing
    {
        int sprM;
        int sprF;
        int bulge;
        bool showbulge;
        Sprite[] sheet;
        public ImpOBottomAlt(int femaleSprite, int maleSprite, bool showbulge, int bulge, int discard, int layer, Sprite[] sheet, int type, bool colored = false)
        {
            coversBreasts = false;
            blocksDick = true;
            sprF = femaleSprite;
            sprM = maleSprite;
            this.sheet = sheet;
            this.bulge = bulge;
            this.showbulge = showbulge;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(layer + 1, null, WhiteColored);
            DiscardSprite = sheet[discard];
            Type = type;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;
            if (actor.HasBelly)
            {
                if (actor.Unit.HasBreasts)
                    clothing1.GetSprite = (s) => sheet[sprF + 4 + weightMod];
                else
                    clothing1.GetSprite = (s) => sheet[sprM + 4 + weightMod];
                if (actor.Unit.HasDick && showbulge == true)
                {
                    if (blocksDick == true)
                        clothing2.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[Math.Min(bulge + actor.Unit.DickSize, 52)];
                    else
                        clothing2.GetSprite = null;
                }
                else
                    clothing2.GetSprite = null;
            }
            else
            {
                if (actor.Unit.HasBreasts)
                    clothing1.GetSprite = (s) => sheet[sprF + weightMod];
                else
                    clothing1.GetSprite = (s) => sheet[sprM + weightMod];
                if (actor.Unit.HasDick)
                {
                    if (blocksDick == true && showbulge == true)
                        clothing2.GetSprite = (s) => SpriteDictionary.NewimpUBottoms[Math.Min(bulge + actor.Unit.DickSize, 52)];
                    else
                        clothing2.GetSprite = null;
                }
                else
                    clothing2.GetSprite = null;
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
            base.Configure(sprite, actor);

        }

    }
    class Hat : MainClothing
    {
        int start;
        Sprite[] sheet = SpriteDictionary.NewimpHats;

        public Hat(int start, int discard, Sprite[] sheet, int type)
        {
            coversBreasts = false;
            blocksDick = false;
            this.sheet = sheet;
            this.start = start;
            DiscardSprite = sheet[discard];
            Type = type;
            clothing1 = new SpriteExtraInfo(22, null, null);
            clothing2 = new SpriteExtraInfo(2, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => sheet[start];
            clothing2.GetSprite = (s) => sheet[start + 1];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class NewImpUndertop1 : MainClothing
    {
        public NewImpUndertop1()
        {
            DiscardSprite = SpriteDictionary.NewimpUTops[8];
            Type = 11012;
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Imps.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.NewimpUTops[7];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.NewimpUTops[0 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);

            base.Configure(sprite, actor);
        }
    }

    class NewImpUndertop2 : MainClothing
    {
        public NewImpUndertop2()
        {
            DiscardSprite = SpriteDictionary.NewimpUTops[17];
            Type = 11013;
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Imps.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.NewimpUTops[16];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.NewimpUTops[9 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);

            base.Configure(sprite, actor);
        }
    }

    class NewImpUndertop3 : MainClothing
    {
        public NewImpUndertop3()
        {
            DiscardSprite = SpriteDictionary.NewimpUTops[26];
            Type = 11014;
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Imps.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.NewimpUTops[25];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.NewimpUTops[18 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);

            base.Configure(sprite, actor);
        }
    }

    class NewImpUndertop4 : MainClothing
    {
        public NewImpUndertop4()
        {
            DiscardSprite = SpriteDictionary.NewimpUTops[35];
            Type = 11015;
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Imps.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.NewimpUTops[34];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.NewimpUTops[27 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);

            base.Configure(sprite, actor);
        }
    }

    class NewImpUndertop5 : MainClothing
    {
        public NewImpUndertop5()
        {
            DiscardSprite = SpriteDictionary.NewimpUTops[75];
            Type = 11016;
            femaleOnly = false;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            clothing2 = new SpriteExtraInfo(16, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;
            int size = actor.GetStomachSize(32, 1.2f);
            if (size > 5)
                size = 5;
            if (Races.Imps.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.NewimpUTops[48 + (13 * weightMod)];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.NewimpUTops[42 + actor.Unit.BreastSize + (13 * weightMod)];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }
            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.NewimpUTops[36 + size + (13 * weightMod)];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.NewimpUTops[62 + size + (6 * weightMod)];
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);

            base.Configure(sprite, actor);
        }
    }
    class NewImpOverOPFem : MainClothing
    {
        public NewImpOverOPFem()
        {
            DiscardSprite = SpriteDictionary.NewImpOverOnePieces[23];
            Type = 11017;
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(20, null, null);
            clothing2 = new SpriteExtraInfo(16, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;
            int size = actor.GetStomachSize(32, 1.2f);
            if (size > 8)
                size = 8;
            int bobs = actor.Unit.BreastSize;
            if (bobs > 7)
                bobs = 7;
            {
                if (Races.Imps.oversize || actor.Unit.HasBreasts == false)
                {
                    clothing1.GetSprite = (s) => null;
                }
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.NewImpOverOnePieces[bobs];
            }
            clothing2.GetSprite = (s) => SpriteDictionary.NewImpOverOnePieces[7 + size + (8 * weightMod)];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
            base.Configure(sprite, actor);
        }
    }
    class NewImpOverOPM : MainClothing
    {
        public NewImpOverOPM()
        {
            DiscardSprite = SpriteDictionary.NewImpOverOnePieces[40];
            Type = 11018;
            maleOnly = true;
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(20, null, null);
            clothing2 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;
            int size = actor.GetStomachSize(32, 1.2f);
            if (size > 6)
                size = 6;

            if (actor.IsAttacking)
                clothing2.GetSprite = (s) => SpriteDictionary.NewImpOverOnePieces[25];
            else
                clothing2.GetSprite = (s) => SpriteDictionary.NewImpOverOnePieces[24];


            clothing1.GetSprite = (s) => SpriteDictionary.NewImpOverOnePieces[26 + size + (7 * weightMod)];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class NewImpOverTop1 : MainClothing
    {
        public NewImpOverTop1()
        {
            DiscardSprite = SpriteDictionary.NewImpOTops[3];
            Type = 11019;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(21, null, null);
            clothing2 = new SpriteExtraInfo(3, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;


            clothing1.GetSprite = (s) => SpriteDictionary.NewImpOTops[0 + weightMod];
            clothing2.GetSprite = (s) => SpriteDictionary.NewImpOTops[2];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class NewImpOverTop2 : MainClothing
    {
        public NewImpOverTop2()
        {
            DiscardSprite = SpriteDictionary.NewImpOTops[7];
            Type = 11020;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(21, null, null);
            clothing2 = new SpriteExtraInfo(3, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;


            clothing1.GetSprite = (s) => SpriteDictionary.NewImpOTops[4 + weightMod];
            clothing2.GetSprite = (s) => SpriteDictionary.NewImpOTops[6];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class NewImpOverTop3 : MainClothing
    {
        public NewImpOverTop3()
        {
            DiscardSprite = SpriteDictionary.NewImpOTops[11];
            Type = 11021;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(21, null, null);
            clothing2 = new SpriteExtraInfo(3, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;


            clothing1.GetSprite = (s) => SpriteDictionary.NewImpOTops[8 + weightMod];
            clothing2.GetSprite = (s) => SpriteDictionary.NewImpOTops[10];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class NewImpOverTop4 : MainClothing
    {
        public NewImpOverTop4()
        {
            DiscardSprite = SpriteDictionary.NewImpOTops[15];
            Type = 11022;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(21, null, null);
            clothing2 = new SpriteExtraInfo(3, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;


            clothing1.GetSprite = (s) => SpriteDictionary.NewImpOTops[12 + weightMod];
            clothing2.GetSprite = (s) => SpriteDictionary.NewImpOTops[14];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }


    class HolidayHat : MainClothing
    {
        public HolidayHat()
        {
            coversBreasts = false;
            blocksDick = false;
            ReqWinterHoliday = true;
            DiscardSprite = null;
            Type = 0;
            clothing1 = new SpriteExtraInfo(29, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.ImpGobHat[0];

            base.Configure(sprite, actor);
        }
    }
}
