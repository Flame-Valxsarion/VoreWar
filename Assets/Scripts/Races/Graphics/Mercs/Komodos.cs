using System;
using System.Collections.Generic;
using UnityEngine;

class Komodos : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Komodos1;
    readonly Sprite[] Sprites2 = SpriteDictionary.Komodos2;
    readonly Sprite[] Sprites3 = SpriteDictionary.Komodos3;

    bool oversize = false;


    public Komodos()
    {
        BodySizes = 4;
        EyeTypes = 6;
        SpecialAccessoryCount = 5; // body patterns    
        HairStyles = 0;
        MouthTypes = 0;
        HairColors = 0;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.KomodosSkin);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.KomodosSkin);
        BodyAccentTypes1 = 11; // head shapes
        BodyAccentTypes2 = 6; // secondary body pattern
        BodyAccentTypes3 = 2; // head pattern on/off

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(1, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor)); // Tail
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor)); // Right Arm
        BodyAccent2 = new SpriteExtraInfo(4, BodyAccentSprite2, WhiteColored); // Toenails
        BodyAccent3 = new SpriteExtraInfo(8, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor)); // Head Shape
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.AccessoryColor)); // Body Secondary Pattern
        BodyAccent5 = new SpriteExtraInfo(7, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.AccessoryColor)); // Head Secondary Pattern
        BodyAccent6 = new SpriteExtraInfo(2, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.AccessoryColor)); // Tail Secondary Pattern
        BodyAccent7 = new SpriteExtraInfo(2, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.AccessoryColor)); // Right Arm Secondary Pattern
        Mouth = new SpriteExtraInfo(6, MouthSprite, WhiteColored);
        Hair = null;
        Hair2 = null;
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(5, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(14, null, null, (s) => KomodoColor(s));
        Weapon = new SpriteExtraInfo(3, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => KomodoColor(s));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => KomodoColor(s));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => KomodoColor(s));

        AllowedClothingHatTypes = new List<ClothingAccessory>();


        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new GenericTop1(),
            new GenericTop2(),
            new GenericTop3(),
            new GenericTop4(),
            new GenericTop5(),
            new Natural(),
            new Tribal(),
        };
        AvoidedMainClothingTypes = 0;
        AvoidedEyeTypes = 0;
        AllowedWaistTypes = new List<MainClothing>()
        {
            new GenericBot1(),
            new GenericBot2(),
            new GenericBot3(),
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
    }

    ColorSwapPalette KomodoColor(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 4)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosReversed, actor.Unit.SkinColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, actor.Unit.SkinColor);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
        {
            AddOffset(Eyes, 0, 1 * .625f);
        }
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.AccessoryColor = unit.SkinColor;

        if (State.Rand.Next(8) == 0)
        {
            unit.SpecialAccessoryType = (3 + State.Rand.Next(2));
        }
        else
        {
            unit.SpecialAccessoryType = State.Rand.Next(SpecialAccessoryCount - 2);
        }

        if (State.Rand.Next(2) == 0)
        {
            unit.BodyAccentType2 = (BodyAccentTypes2 - 1);
        }
        else
        {
            unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2 - 1);
        }

        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3);
    }

    internal override int DickSizes => 8;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        int sat = Mathf.Clamp(actor.Unit.SpecialAccessoryType, 0, 4); //Protect against out of bounds from other unit types.  
        if (actor.Unit.HasBreasts)
        {
            return Sprites[0 + actor.Unit.BodySize + 10 * sat];
        }
        else
        {
            return Sprites[4 + actor.Unit.BodySize + 10 * sat];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 4)
        {
            if (actor.IsOralVoring)
                return Sprites[61];
            if (actor.IsAttacking || actor.IsEating)
                return Sprites[58];
            return Sprites[55];
        }
        else if (actor.Unit.SpecialAccessoryType == 3)
        {
            if (actor.IsOralVoring)
                return Sprites[59];
            if (actor.IsAttacking || actor.IsEating)
                return Sprites[56];
            return Sprites[53];
        }
        else
        {
            if (actor.IsOralVoring)
                return Sprites[60];
            if (actor.IsAttacking || actor.IsEating)
                return Sprites[57];
            return Sprites[54];
        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // tail
    {
        if (actor.Unit.SpecialAccessoryType == 4)
        {
            return Sprites[52];
        }
        else if (actor.Unit.SpecialAccessoryType == 3)
        {
            return Sprites[50];
        }
        else
        {
            return Sprites[51];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // right arm
    {
        if (actor.IsAttacking)
        {
            return Sprites[9 + 10 * actor.Unit.SpecialAccessoryType];
        }
        else
        {
            return Sprites[8 + 10 * actor.Unit.SpecialAccessoryType];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites[64]; // toenails

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // head shape
    {
        if (actor.Unit.BodyAccentType1 == 10)
        {
            return null;
        }
        else if (actor.Unit.SpecialAccessoryType == 3)
        {
            return Sprites3[10 + actor.Unit.BodyAccentType1];
        }
        else
        {
            return Sprites3[0 + actor.Unit.BodyAccentType1];
        }
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Body Secondary Pattern
    {
        if (actor.Unit.BodyAccentType2 == 5)
        {
            return null;
        }
        else
        {
            return Sprites3[20 + actor.Unit.BodySize + 10 * actor.Unit.BodyAccentType2];
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Head Secondary Pattern
    {
        if (actor.Unit.BodyAccentType3 == 1 || actor.Unit.BodyAccentType2 == 5)
        {
            return null;
        }
        else if (actor.IsOralVoring)
        {
            return Sprites3[29 + 10 * actor.Unit.BodyAccentType2];
        }
        else if (actor.IsAttacking || actor.IsEating)
        {
            return Sprites3[28 + 10 * actor.Unit.BodyAccentType2];
        }
        else
        {
            return Sprites3[27 + 10 * actor.Unit.BodyAccentType2];
        }
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Tail Secondary Pattern
    {
        if (actor.Unit.BodyAccentType2 == 5)
        {
            return null;
        }
        else
        {
            return Sprites3[26 + 10 * actor.Unit.BodyAccentType2];
        }
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Right Arm Secondary Pattern
    {
        if (actor.Unit.BodyAccentType2 == 5)
        {
            return null;
        }
        else if (actor.IsAttacking)
        {
            return Sprites3[25 + 10 * actor.Unit.BodyAccentType2];
        }
        else
        {
            return Sprites3[24 + 10 * actor.Unit.BodyAccentType2];
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[63];
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[62];
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[65 + actor.Unit.EyeType];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(26, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 26)
            {
                AddOffset(Belly, 0, -12 * .625f);
                return Sprites2[90];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 26)
            {
                AddOffset(Belly, 0, -12 * .625f);
                return Sprites2[89];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 25)
            {
                AddOffset(Belly, 0, -12 * .625f);
                return Sprites2[88];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 24)
            {
                AddOffset(Belly, 0, -12 * .625f);
                return Sprites2[87];
            }
            switch (size)
            {
                case 24:
                    AddOffset(Belly, 0, -4 * .625f);
                    break;
                case 25:
                    AddOffset(Belly, 0, -9 * .625f);
                    break;
                case 26:
                    AddOffset(Belly, 0, -12 * .625f);
                    break;
            }

            return Sprites2[60 + size];
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
                    return Sprites[103];
                case 1:
                    return Sprites[104];
                case 2:
                    return Sprites[105];
                case 3:
                    return Sprites[106];
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
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(30 * 30, 1f));
            if (leftSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return Sprites2[29];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return Sprites2[28];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 26)
            {
                return Sprites2[27];
            }

            if (leftSize > 26)
                leftSize = 26;

            return Sprites2[0 + leftSize];
        }
        else
        {
            return Sprites2[0 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(30 * 30, 1f));
            if (rightSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return Sprites2[59];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return Sprites2[58];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 26)
            {
                return Sprites2[57];
            }

            if (rightSize > 26)
                rightSize = 26;

            return Sprites2[30 + rightSize];
        }
        else
        {
            return Sprites2[30 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(31 * 31, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(31 * 31, 1f)) < 16))
            {
                Dick.layer = 20;
                if (actor.IsCockVoring)
                {
                    return Sprites[79 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[71 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    return Sprites[95 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[87 + actor.Unit.DickSize];
                }
            }
        }

        Dick.layer = 11;
        return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect() && (actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(30 * 30, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(30 * 30, 1f)) < 16))
        {
            Balls.layer = 19;
        }
        else
        {
            Balls.layer = 10;
        }
        int size = actor.Unit.DickSize;
        int offset = actor.GetBallSize(27, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -27 * .625f);
            return Sprites2[127];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return Sprites2[126];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 26)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return Sprites2[125];
        }
        else if (offset >= 25)
        {
            AddOffset(Balls, 0, -13 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -8 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -3 * .625f);
        }

        if (offset > 0)
            return Sprites2[Math.Min(99 + offset, 124)];
        return Sprites2[91 + size];
    }


    class GenericTop1 : MainClothing
    {
        public GenericTop1()
        {
            DiscardSprite = SpriteDictionary.Komodos4[48];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 61401;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Komodos.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[47];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[39 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop2 : MainClothing
    {
        public GenericTop2()
        {
            DiscardSprite = SpriteDictionary.Komodos4[58];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 61402;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Komodos.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[57];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[49 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop3 : MainClothing
    {
        public GenericTop3()
        {
            DiscardSprite = SpriteDictionary.Komodos4[68];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 61403;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Komodos.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[67];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[59 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Komodos4[69];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop4 : MainClothing
    {
        public GenericTop4()
        {
            DiscardSprite = SpriteDictionary.Komodos4[79];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 61404;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Komodos.oversize)
            {
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => SpriteDictionary.Komodos4[78];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[80 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Komodos4[70 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop5 : MainClothing
    {
        public GenericTop5()
        {
            DiscardSprite = SpriteDictionary.Komodos4[97];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 61405;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Komodos.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[96];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[88 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class Natural : MainClothing
    {
        public Natural()
        {
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(7, null, null);
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Komodos.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[1 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }
            clothing2.GetSprite = (s) => SpriteDictionary.Komodos4[0];

            if (actor.Unit.SpecialAccessoryType == 4)
            {
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosReversed, actor.Unit.SkinColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosReversed, actor.Unit.SkinColor);
            }
            else
            {
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, actor.Unit.SkinColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, actor.Unit.SkinColor);
            }

            base.Configure(sprite, actor);
        }
    }

    class Tribal : MainClothing
    {
        public Tribal()
        {
            DiscardSprite = SpriteDictionary.Komodos4[38];
            coversBreasts = false;
            Type = 61406;
            OccupiesAllSlots = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Komodos.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[37];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[29 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Komodos4[25 + actor.Unit.BodySize];

            base.Configure(sprite, actor);
        }
    }

    class GenericBot1 : MainClothing
    {
        public GenericBot1()
        {
            DiscardSprite = SpriteDictionary.Komodos4[9];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 61407;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[15];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[17];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[16];
            }
            else clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[14];

            clothing2.GetSprite = (s) => SpriteDictionary.Komodos4[10 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot2 : MainClothing
    {
        public GenericBot2()
        {
            DiscardSprite = SpriteDictionary.Komodos4[19];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 61408;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[18];
            clothing2.GetSprite = (s) => SpriteDictionary.Komodos4[10 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot3 : MainClothing
    {
        public GenericBot3()
        {
            DiscardSprite = SpriteDictionary.Komodos4[24];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 61409;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            clothing1.GetSprite = (s) => SpriteDictionary.Komodos4[20 + actor.Unit.BodySize];

            base.Configure(sprite, actor);
        }
    }









}
