using System;
using System.Collections.Generic;
using UnityEngine;
//TODO:
// recolor bulges on clothes
// add color selection
// add wobble to imp belly accent

class Goblins : DefaultRaceData
{
    Sprite[] SpritesBase = SpriteDictionary.Gobbo;
    Sprite[] SpritesVore = SpriteDictionary.Gobbovore;
    Sprite[] SpritesGloves = SpriteDictionary.Gobbglove;
    Sprite[] SpritesLegs = SpriteDictionary.Gobleggo;
    Sprite[] SpritesUBottoms = SpriteDictionary.Gobbunderbottoms;
    Sprite[] SpritesUTops = SpriteDictionary.Gobundertops;
    Sprite[] SpritesOBottoms = SpriteDictionary.Gobboverbottoms;
    Sprite[] SpritesOTops = SpriteDictionary.Gobbovertops;
    Sprite[] SpritesOnePieces = SpriteDictionary.Gobbunderonepieces;
    Sprite[] SpritesOverOnePieces = SpriteDictionary.Gobboveronepieces;
    Sprite[] SpritesHats = SpriteDictionary.Gobbohat;


    internal List<MainClothing> AllClothing;

    bool oversize = false;

    internal Goblins()
    {
        SpecialAccessoryCount = 0;
        ClothingShift = new Vector3(0, 0, 0);
        AvoidedEyeTypes = 0;
        AvoidedMouthTypes = 0;

        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
        HairStyles = 16;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Goblins);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ImpDark);
        EyeTypes = 8;
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
        SecondaryEyeColors = 1;
        BodySizes = 2;
        AllowedWaistTypes = new List<MainClothing>();
        MouthTypes = 3;
        AvoidedMainClothingTypes = 0;
        BodyAccentTypes1 = 4;
        BodyAccentTypes2 = 4;


        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing50Spaced);

        ExtendedBreastSprites = true;

        AllowedMainClothingTypes = new List<MainClothing>() //undertops
        {
            new GobboLeotard(),
            new GobboCasinoBunny(),
            new GobboUndertop1(),
            new GobboUndertop2(),
            new GobboUndertop3(),
            new GobboUndertop4(),
            new GobboUndertop5(),
        };

        AllowedClothingHatTypes = new List<ClothingAccessory>();


        AllowedWaistTypes = new List<MainClothing>() //underbottoms
        {
            new ImpUBottom(0, 2, 45, 8, 9, SpriteDictionary.Gobbunderbottoms, 12045, true),
            new ImpUBottom(9, 11, 45, 17, 9, SpriteDictionary.Gobbunderbottoms, 12046, true),
            new ImpUBottom(18, 20, 45, 26, 9, SpriteDictionary.Gobbunderbottoms, 12047, false, black: true),
            new ImpUBottom(27, 29, 45, 35, 9, SpriteDictionary.Gobbunderbottoms, 12048, true),
            new ImpUBottom(36, 38, 45, 44, 9, SpriteDictionary.Gobbunderbottoms, 12049, true),
        };

        ExtraMainClothing1Types = new List<MainClothing>() //Overbottoms
        {
            new ImpOBottom(0, 2, false, 45, 8, 14, SpriteDictionary.Gobboverbottoms, 12050, true),
            new ImpOBottom(9, 11, false, 45, 17, 14, SpriteDictionary.Gobboverbottoms, 12051, true),
            new ImpOBottom(18, 20, true, 45, 26, 14, SpriteDictionary.Gobboverbottoms, 12052, true),
            new ImpOBottom(27, 29, true, 49, 35, 14, SpriteDictionary.Gobboverbottoms, 12053, true),
            new ImpOBottomAlt(27, 29, true, 49, 35, 14, SpriteDictionary.Gobboverbottoms, 12053, true),
            new ImpOBottom(36, 38, false, 45, 44, 14, SpriteDictionary.Gobboverbottoms, 12054, true),
        };

        ExtraMainClothing2Types = new List<MainClothing>() //Special clothing
        {
            new GobboOverOPFem(),
            //new GobboOverOPM(),
            new GobboOverTop1(),
            new GobboOverTop2(),
            new GobboOverTop3(),
            new GobboOverTop4(),
        };

        ExtraMainClothing3Types = new List<MainClothing>() //Legs
        {
            new GenericLegs(0, 4, 45, 12055, femaleOnly: true, blocksDick: false),
            new GenericLegs(5, 9, 45, 12056, femaleOnly: true, blocksDick: false),
            new GenericLegs(10, 14, 45, 12057, femaleOnly: true, blocksDick: false),
            new GenericLegsAlt(10, 14, 45, 12057, femaleOnly: true, blocksDick: false),
            new GenericLegs(15, 19, 45, 12058, femaleOnly: true, blocksDick: false),
            new GenericLegsAlt(15, 19, 45, 12058, femaleOnly: true, blocksDick: false),
            new GenericLegs(20, 24, 45, 12059, femaleOnly: true, blocksDick: false),
            new GenericLegs(2, 4, 45, 12060, maleOnly: true, blocksDick: true, black: true),
            new GenericLegs(7, 9, 45, 12061, maleOnly: true, blocksDick: true, black: true),
            new GenericLegs(12, 14, 45, 12062, maleOnly: true, blocksDick: true),
            new GenericLegs(17, 19, 45, 12063, maleOnly: true, blocksDick: true),
            new GenericLegs(22, 24, 45, 12064, maleOnly: true, blocksDick: false),

        };

        ExtraMainClothing4Types = new List<MainClothing>() //Gloves
        {
            new GenericGloves(0, 8, 12065),
            new GenericGlovesPlusSecond(9, 17, 12066),
            new GenericGlovesPlusSecondAlt(9, 17, 12066),
            new GenericGlovesPlusSecond(18, 26, 12067),
            new GenericGlovesPlusSecondAlt(18, 26, 12067),
            new GenericGloves(27, 35, 12068),

        };

        ExtraMainClothing5Types = new List<MainClothing>() //Hats
        {
            new Hat(0, 0, SpriteDictionary.Gobbohat, 12069),
            new Hat(34, 0, SpriteDictionary.Gobbohat, 12070),
            new HolidayHat(),
        };

        AllClothing = new List<MainClothing>();
        AllClothing.AddRange(AllowedMainClothingTypes);
        AllClothing.AddRange(ExtraMainClothing3Types);
        AllClothing.AddRange(ExtraMainClothing4Types);
        AllClothing.AddRange(ExtraMainClothing5Types);


        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, s.Unit.SkinColor));
        BodyAccessory = null;
        BodyAccent = new SpriteExtraInfo(5, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, s.Unit.SkinColor));
        BodyAccent2 = new SpriteExtraInfo(7, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, s.Unit.SkinColor));
        BodyAccent3 = null;
        BodyAccent4 = null;
        BodyAccent5 = null;
        BodyAccent6 = null;
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
        Belly = new SpriteExtraInfo(16, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(6, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(19, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(19, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(14, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(13, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, s.Unit.SkinColor));
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
                AddOffset(Belly, 0, -23 * .625f);
                return SpritesVore[105];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
            {
                AddOffset(Belly, 0, -22 * .625f);
                return SpritesVore[104];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size >= 30)
            {
                AddOffset(Belly, 0, -22 * .625f);
                return SpritesVore[103];
            }
            switch (size)
            {
                case 24:
                    AddOffset(Belly, 0, 0 * .625f);
                    break;
                case 25:
                    AddOffset(Belly, 0, 0 * .625f);
                    break;
                case 26:
                    AddOffset(Belly, 0, -1 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -5 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -8 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -15 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -17 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -19 * .625f);
                    break;
                case 32:
                    AddOffset(Belly, 0, -22 * .625f);
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
        return SpritesBase[32 + actor.Unit.BodyAccentType1];
    }
    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        return SpritesBase[36 + actor.Unit.BodyAccentType2];
    }
    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BodySprite(Actor_Unit actor)
    {
        int weightMod = Math.Min(actor.Unit.BodySize * 2, 2);
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
        if (actor.Unit.HasBreasts == true)
        {
            return SpritesBase[8 + attackingOffset + eatingOffset + hurtOffset + (8 * actor.Unit.MouthType)];
        }
        else
        {
            return SpritesBase[12 + attackingOffset + eatingOffset + hurtOffset + (8 * actor.Unit.MouthType)];
        }
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
            AddOffset(Balls, 0, -19 * .625f);
            return SpritesVore[69];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 22)
        {
            AddOffset(Balls, 0, -17 * .625f);
            return SpritesVore[68];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 20)
        {
            AddOffset(Balls, 0, -14 * .625f);
            return SpritesVore[67];
        }
        int combined = Math.Min(baseSize + size + 2, 22);
        if (combined == 22)
            AddOffset(Balls, 0, -2 * .625f);
        else if (combined >= 21)
            AddOffset(Balls, 0, -2 * .625f);
        else if (combined >= 20)
            AddOffset(Balls, 0, -1 * .625f);
        else if (combined >= 19)
            AddOffset(Balls, 0, 1 * .625f);
        else if (combined >= 18)
            AddOffset(Balls, 0, 0 * .625f);
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
            if (actor.Unit.HasBreasts == true)
            {
                int sprite = 72;
                sprite += actor.Unit.EyeType;
                return SpritesBase[sprite];
            }
            else
            {
                int sprite = 136;
                sprite += actor.Unit.EyeType;
                return SpritesBase[sprite];
            }
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
        if (actor.Unit.HasBreasts == true)
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
        else
        {
            int sprite = 56;
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
        Sprite[] sheet = SpriteDictionary.Gobbglove;

        public GenericGloves(int start, int discard, int type)//int type
        {
            coversBreasts = false;
            blocksDick = false;
            this.start = start;
            DiscardSprite = sheet[discard];
            Type = type;
            clothing1 = new SpriteExtraInfo(5, null, null);
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
        Sprite[] sheet = SpriteDictionary.Gobbglove;

        public GenericGlovesPlusSecond(int start, int discard, int type) //int type
        {
            coversBreasts = false;
            blocksDick = false;
            this.start = start;
            DiscardSprite = sheet[discard];
            Type = type;
            clothing1 = new SpriteExtraInfo(5, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.ClothingColor));
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
        Sprite[] sheet = SpriteDictionary.Gobbglove;

        public GenericGlovesPlusSecondAlt(int start, int discard, int type) //int type
        {
            coversBreasts = false;
            blocksDick = false;
            this.start = start;
            DiscardSprite = sheet[discard];
            Type = type;
            clothing1 = new SpriteExtraInfo(5, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.ClothingColor));
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
        Sprite[] sheet = SpriteDictionary.Gobleggo;

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
                        clothing2.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[bulge + 4 + actor.Unit.DickSize];
                    else
                        clothing2.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[bulge + actor.Unit.DickSize];
                    if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                        clothing2.YOffset = 2 * .625f;
                    else
                        clothing2.YOffset = 0;
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
        Sprite[] sheet = SpriteDictionary.Gobleggo;

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
                        clothing2.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[bulge + 4 + actor.Unit.DickSize];
                    else
                        clothing2.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[bulge + actor.Unit.DickSize];
                    if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                        clothing2.YOffset = 2 * .625f;
                    else
                        clothing2.YOffset = 0;
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
                            clothing2.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[bulge + 4 + actor.Unit.DickSize];
                        else
                            clothing2.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[bulge + actor.Unit.DickSize];
                        if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                            clothing2.YOffset = 2 * .625f;
                        else
                            clothing2.YOffset = 0;
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
                            clothing2.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[bulge + 4 + actor.Unit.DickSize];
                        else
                            clothing2.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[bulge + actor.Unit.DickSize];
                        if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                            clothing2.YOffset = 2 * .625f;
                        else
                            clothing2.YOffset = 0;
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
    class GobboLeotard : MainClothing
    {
        public GobboLeotard()
        {
            DiscardSprite = SpriteDictionary.Gobbunderonepieces[74];
            Type = 12071;
            colorsBelly = true;
            blocksDick = true;
            OccupiesAllSlots = true;
            DiscardUsesPalettes = true;
            clothing1 = new SpriteExtraInfo(17, null, null);
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
                    clothing3.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[45 + actor.Unit.DickSize];
                    if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                        clothing3.YOffset = 2 * .625f;
                    else
                        clothing3.YOffset = 0;
                }
                else clothing3.GetSprite = null;

                if (bobs == 7)
                    clothing2.GetSprite = null;
                else
                    clothing2.GetSprite = (s) => SpriteDictionary.Gobbunderonepieces[33 + bobs];
                clothing1.GetSprite = (s) => SpriteDictionary.Gobbunderonepieces[41 + size + 8 * weightMod];

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                bellyPalette = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, actor.Unit.SkinColor);
                base.Configure(sprite, actor);
            }
            else
            {
                clothing1 = new SpriteExtraInfo(13, null, null);
                clothing2 = new SpriteExtraInfo(20, null, null);
                clothing3 = new SpriteExtraInfo(16, null, null);
                clothing3.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[45 + actor.Unit.DickSize];
                if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                    clothing3.YOffset = 2 * .625f;
                else
                    clothing3.YOffset = 0;
                clothing2.GetSprite = null;
                clothing1.GetSprite = (s) => SpriteDictionary.Gobbunderonepieces[57 + size + 9 * weightMod];
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                bellyPalette = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SkinToClothing, actor.Unit.ClothingColor);
                base.Configure(sprite, actor);
            }
        }
    }

    class GobboCasinoBunny : MainClothing
    {
        public GobboCasinoBunny()
        {
            DiscardSprite = SpriteDictionary.Gobbunderonepieces[74];
            Type = 12072;
            colorsBelly = true;
            blocksDick = true;
            OccupiesAllSlots = true;
            DiscardUsesPalettes = true;
            clothing1 = new SpriteExtraInfo(17, null, null);
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
            if (size > 5)
                size = 5;
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.HasDick)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[45 + actor.Unit.DickSize];
                    if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                        clothing3.YOffset = 2 * .625f;
                    else
                        clothing3.YOffset = 0;
                }
                else clothing3.GetSprite = null;



                clothing2.GetSprite = (s) => SpriteDictionary.Gobbunderonepieces[0 + bobs];
                clothing1.GetSprite = (s) => SpriteDictionary.Gobbunderonepieces[8 + size + 6 * weightMod];

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                bellyPalette = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Goblins, actor.Unit.SkinColor);
                base.Configure(sprite, actor);
            }
            else
            {
                clothing1 = new SpriteExtraInfo(13, null, null);
                clothing2 = new SpriteExtraInfo(20, null, null);
                clothing3 = new SpriteExtraInfo(16, null, null);
                clothing3.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[45 + actor.Unit.DickSize];
                if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                    clothing3.YOffset = 2 * .625f;
                else
                    clothing3.YOffset = 0;
                clothing2.GetSprite = null;
                clothing1.GetSprite = (s) => SpriteDictionary.Gobbunderonepieces[20 + size + 6 * weightMod];
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
                bellyPalette = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SkinToClothing, actor.Unit.ClothingColor);
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
                        clothing2.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[bulge + actor.Unit.DickSize];
                    else
                        clothing2.GetSprite = null;
                    if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                        clothing2.YOffset = 2 * .625f;
                    else
                        clothing2.YOffset = 0;
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
                        clothing2.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[bulge + actor.Unit.DickSize];
                    else
                        clothing2.GetSprite = null;
                    if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                        clothing2.YOffset = 2 * .625f;
                    else
                        clothing2.YOffset = 0;
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
                        clothing2.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[bulge + actor.Unit.DickSize];
                    else
                        clothing2.GetSprite = null;
                    if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                        clothing2.YOffset = 2 * .625f;
                    else
                        clothing2.YOffset = 0;
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
                        clothing2.GetSprite = (s) => SpriteDictionary.Gobbunderbottoms[bulge + actor.Unit.DickSize];
                    else
                        clothing2.GetSprite = null;
                    if (actor.Unit.GetGender() == Gender.Hermaphrodite)
                        clothing2.YOffset = 2 * .625f;
                    else
                        clothing2.YOffset = 0;
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
        Sprite[] sheet = SpriteDictionary.Gobbohat;

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

    class GobboUndertop1 : MainClothing
    {
        public GobboUndertop1()
        {
            DiscardSprite = SpriteDictionary.Gobundertops[8];
            Type = 12073;
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Goblins.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobundertops[7];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobundertops[0 + actor.Unit.BreastSize];
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

    class GobboUndertop2 : MainClothing
    {
        public GobboUndertop2()
        {
            DiscardSprite = SpriteDictionary.Gobundertops[17];
            Type = 12074;
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Goblins.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobundertops[16];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobundertops[9 + actor.Unit.BreastSize];
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

    class GobboUndertop3 : MainClothing
    {
        public GobboUndertop3()
        {
            DiscardSprite = SpriteDictionary.Gobundertops[26];
            Type = 12075;
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Goblins.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobundertops[18 + actor.Unit.BreastSize];
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

    class GobboUndertop4 : MainClothing
    {
        public GobboUndertop4()
        {
            DiscardSprite = SpriteDictionary.Gobundertops[35];
            Type = 12076;
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Goblins.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobundertops[34];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobundertops[27 + actor.Unit.BreastSize];
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

    class GobboUndertop5 : MainClothing
    {
        public GobboUndertop5()
        {
            DiscardSprite = SpriteDictionary.Gobundertops[75];
            Type = 12077;
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
            if (Races.Goblins.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobundertops[48 + (13 * weightMod)];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobundertops[42 + actor.Unit.BreastSize + (13 * weightMod)];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }
            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Gobundertops[36 + size + (13 * weightMod)];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Gobundertops[62 + size + (6 * weightMod)];
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor2);

            base.Configure(sprite, actor);
        }
    }
    class GobboOverOPFem : MainClothing
    {
        public GobboOverOPFem()
        {
            DiscardSprite = SpriteDictionary.Gobboveronepieces[23];
            Type = 12078;
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(20, null, null);
            clothing2 = new SpriteExtraInfo(18, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;
            int size = actor.GetStomachSize(32, 1.2f);
            if (size > 7)
                size = 7;
            int bobs = actor.Unit.BreastSize;
            if (bobs > 7)
                bobs = 7;
            {
                if (Races.Goblins.oversize || actor.Unit.HasBreasts == false)
                {
                    clothing1.GetSprite = (s) => null;
                }
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Gobboveronepieces[0 + bobs];
            }
            clothing2.GetSprite = (s) => SpriteDictionary.Gobboveronepieces[7 + size + (8 * weightMod)];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class GobboOverOPM : MainClothing
    {
        public GobboOverOPM()
        {
            DiscardSprite = SpriteDictionary.Gobboveronepieces[40];
            Type = 12079;
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
                clothing2.GetSprite = (s) => SpriteDictionary.Gobboveronepieces[25];
            else
                clothing2.GetSprite = (s) => SpriteDictionary.Gobboveronepieces[24];


            clothing1.GetSprite = (s) => SpriteDictionary.Gobboveronepieces[26 + size + (7 * weightMod)];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class GobboOverTop1 : MainClothing
    {
        public GobboOverTop1()
        {
            DiscardSprite = SpriteDictionary.Gobbovertops[3];
            Type = 12080;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(21, null, null);
            clothing2 = new SpriteExtraInfo(3, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;

            if (actor.Unit.HasBreasts == true)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobbovertops[0 + weightMod];
                clothing2.GetSprite = (s) => SpriteDictionary.Gobbovertops[2];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobbovertops[16 + weightMod];
                clothing2.GetSprite = (s) => SpriteDictionary.Gobbovertops[18];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class GobboOverTop2 : MainClothing
    {
        public GobboOverTop2()
        {
            DiscardSprite = SpriteDictionary.Gobbovertops[7];
            Type = 12081;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(21, null, null);
            clothing2 = new SpriteExtraInfo(3, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;

            if (actor.Unit.HasBreasts == true)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobbovertops[4 + weightMod];
                clothing2.GetSprite = (s) => SpriteDictionary.Gobbovertops[6];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobbovertops[20 + weightMod];
                clothing2.GetSprite = (s) => SpriteDictionary.Gobbovertops[22];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class GobboOverTop3 : MainClothing
    {
        public GobboOverTop3()
        {
            DiscardSprite = SpriteDictionary.Gobbovertops[11];
            Type = 12082;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(21, null, null);
            clothing2 = new SpriteExtraInfo(3, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;


            if (actor.Unit.HasBreasts == true)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobbovertops[8 + weightMod];
                clothing2.GetSprite = (s) => SpriteDictionary.Gobbovertops[10];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobbovertops[24 + weightMod];
                clothing2.GetSprite = (s) => SpriteDictionary.Gobbovertops[26];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class GobboOverTop4 : MainClothing
    {
        public GobboOverTop4()
        {
            DiscardSprite = SpriteDictionary.Gobbovertops[15];
            Type = 12083;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(21, null, null);
            clothing2 = new SpriteExtraInfo(3, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;

            if (actor.Unit.HasBreasts == true)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobbovertops[12 + weightMod];
                clothing2.GetSprite = (s) => SpriteDictionary.Gobbovertops[14];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Gobbovertops[28 + weightMod];
                clothing2.GetSprite = (s) => SpriteDictionary.Gobbovertops[30];
            }

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
            clothing1.GetSprite = (s) => SpriteDictionary.ImpGobHat[1];

            base.Configure(sprite, actor);
        }
    }
}
