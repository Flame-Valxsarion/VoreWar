using System;
using System.Collections.Generic;
using UnityEngine;

class Hippos : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Hippos;
    readonly Sprite[] Sprites2 = SpriteDictionary.Hippos2;
    readonly Sprite[] Sprites3 = SpriteDictionary.Hippos3;

    bool oversize = false;

    public Hippos()
    {
        BodySizes = 5;
        HairStyles = 0;
        EyeTypes = 5;
        SpecialAccessoryCount = 8; // ears  
        HairStyles = 0;
        MouthTypes = 0;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.HippoSkin);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.HippoSkin); // tattoo/warpaint colors
        BodyAccentTypes1 = 6; // left arm tattoo/warpaint
        BodyAccentTypes2 = 6; // right arm tattoo/warpaint
        BodyAccentTypes3 = 6; // head tattoo/warpaint
        BodyAccentTypes4 = 6; // legs tattoo/warpaint
        clothingColors = 0;

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(3, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(22, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(2, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, s.Unit.SkinColor)); // ears
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, s.Unit.AccessoryColor)); // left arm tattoo/warpaint
        BodyAccent2 = new SpriteExtraInfo(4, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, s.Unit.AccessoryColor)); // right arm tattoo/warpaint
        BodyAccent3 = new SpriteExtraInfo(23, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, s.Unit.AccessoryColor)); // head tattoo/warpaint
        BodyAccent4 = new SpriteExtraInfo(4, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, s.Unit.AccessoryColor)); // legs tattoo/warpaint
        BodyAccent5 = new SpriteExtraInfo(24, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, s.Unit.SkinColor));  // eyebrows
        BodyAccent6 = new SpriteExtraInfo(1, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, s.Unit.SkinColor));  // left arm
        Mouth = null;
        Hair = null;
        Hair2 = null;
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(25, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(2, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(16, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, s.Unit.SkinColor));

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new HipposTop1(),
            new HipposTop2(),
            new HipposTop3(),
            new Natural(),
        };
        AllowedWaistTypes = new List<MainClothing>()
        {
            new HipposBot1(),
            new HipposBot2(),
            new HipposBot3(),
            new HipposBot4(),
        };
        AllowedClothingHatTypes = new List<ClothingAccessory>()
        {
            new HipposHeadband1(),
            new HipposHeadband2(),
            new HipposHeadband3(),
            new HipposHeadband4(),
            new HipposHeadband5(),
            new HipposHeadband6(),
            new HipposHeadband7(),
            new HipposHeadband8(),
        };
        AllowedClothingAccessoryTypes = new List<ClothingAccessory>()
        {
            new HipposNecklace1(),
            new HipposNecklace2(),
            new HipposNecklace3(),
            new HipposNecklace4(),
            new HipposNecklace5(),
            new HipposNecklace6(),
            new HipposNecklace7(),
            new HipposNecklace8(),
        };
        AvoidedMainClothingTypes = 0;
        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.HippoSkin);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        if (State.Rand.Next(5) == 0)
        {
            unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1 - 1);
        }
        else
        {
            unit.BodyAccentType1 = (BodyAccentTypes1 - 1);
        }
        if (State.Rand.Next(5) == 0)
        {
            unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2 - 1);
        }
        else
        {
            unit.BodyAccentType2 = (BodyAccentTypes2 - 1);
        }
        if (State.Rand.Next(5) == 0)
        {
            unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3 - 1);
        }
        else
        {
            unit.BodyAccentType3 = (BodyAccentTypes3 - 1);
        }
        if (State.Rand.Next(5) == 0)
        {
            unit.BodyAccentType4 = State.Rand.Next(BodyAccentTypes4 - 1);
        }
        else
        {
            unit.BodyAccentType4 = (BodyAccentTypes4 - 1);
        }
    }

    internal override int DickSizes => 8;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[0 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodySize)];
        }
        else
        {
            return Sprites[10 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodySize)];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites[21];
        return Sprites[20];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites[22 + actor.Unit.SpecialAccessoryType]; //ears

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites2[0 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodyAccentType1)]; // left arm tattoo/warpaint

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites2[12 + actor.Unit.BodyAccentType2]; // right arm tattoo/warpaint

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) => Sprites2[18 + (actor.IsEating ? 1 : 0) + (2 * actor.Unit.BodyAccentType3)]; // head tattoo/warpaint

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) => Sprites2[30 + actor.Unit.BodyAccentType4]; // legs tattoo/warpaint

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) => Sprites[40 + (actor.IsEating ? 1 : 0) + (2 * actor.Unit.EyeType)]; // eyebrows

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) => Sprites[120 + (actor.IsAttacking ? 1 : 0)]; // left arm

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[30 + (actor.IsEating ? 1 : 0) + (2 * actor.Unit.EyeType)];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(26, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 26)
            {
                AddOffset(Belly, 0, -9 * .625f);
                return Sprites3[94];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 26)
            {
                AddOffset(Belly, 0, -9 * .625f);
                return Sprites3[93];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 25)
            {
                AddOffset(Belly, 0, -9 * .625f);
                return Sprites3[92];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 24)
            {
                AddOffset(Belly, 0, -9 * .625f);
                return Sprites3[91];
            }
            switch (size)
            {
                case 24:
                    AddOffset(Belly, 0, -3 * .625f);
                    break;
                case 25:
                    AddOffset(Belly, 0, -6 * .625f);
                    break;
                case 26:
                    AddOffset(Belly, 0, -9 * .625f);
                    break;
            }

            return Sprites3[64 + size];
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
            return Sprites[74 + actor.GetWeaponSprite()];
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
                return Sprites3[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return Sprites3[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return Sprites3[29];
            }

            if (leftSize > 28)
                leftSize = 28;

            return Sprites3[0 + leftSize];
        }
        else
        {
            return Sprites3[0 + actor.Unit.BreastSize];
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
                return Sprites3[63];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return Sprites3[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return Sprites3[61];
            }

            if (rightSize > 28)
                rightSize = 28;

            return Sprites3[32 + rightSize];
        }
        else
        {
            return Sprites3[32 + actor.Unit.BreastSize];
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
                Dick.layer = 18;
                if (actor.IsCockVoring)
                {
                    return Sprites[82 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[66 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 12;
                if (actor.IsCockVoring)
                {
                    return Sprites[50 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[58 + actor.Unit.DickSize];
                }
            }
        }

        Dick.layer = 9;
        return Sprites[58 + actor.Unit.DickSize];
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect() && (actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
        {
            Balls.layer = 17;
        }
        else
        {
            Balls.layer = 8;
        }
        int baseSize = (actor.Unit.DickSize + 1) / 3;
        int ballOffset = actor.GetBallSize(26, .9f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && ballOffset == 26)
        {
            AddOffset(Balls, 0, -26 * .625f);
            return Sprites[119];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && ballOffset == 26)
        {
            AddOffset(Balls, 0, -21 * .625f);
            return Sprites[118];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && ballOffset >= 24)
        {
            AddOffset(Balls, 0, -17 * .625f);
            return Sprites[117];
        }
        int combined = Math.Min(baseSize + ballOffset + 2, 26);
        if (combined == 26)
            AddOffset(Balls, 0, -13 * .625f);
        else if (combined == 25)
            AddOffset(Balls, 0, -8 * .625f);
        else if (combined == 24)
            AddOffset(Balls, 0, -4 * .625f);
        if (ballOffset > 0)
        {
            return Sprites[90 + combined];
        }
        return Sprites[90 + baseSize];
    }

    class HipposTop1 : MainClothing
    {
        public HipposTop1()
        {
            DiscardSprite = SpriteDictionary.Hippos2[60];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            Type = 84260;
            FixedColor = true;
        }
        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Hippos.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[52 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }
            base.Configure(sprite, actor);
        }
    }

    class HipposTop2 : MainClothing
    {
        public HipposTop2()
        {
            DiscardSprite = SpriteDictionary.Hippos2[87];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            Type = 84287;
            FixedColor = true;
        }
        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Hippos.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[79 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[103];
            }
            base.Configure(sprite, actor);
        }
    }

    class HipposTop3 : MainClothing
    {
        public HipposTop3()
        {
            DiscardSprite = SpriteDictionary.Hippos2[96];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            Type = 84296;
            FixedColor = true;
        }
        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Hippos.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[88 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[104];
            }
            base.Configure(sprite, actor);
        }
    }

    class Natural : MainClothing
    {
        public Natural()
        {
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(17, null, null);
            clothing2 = new SpriteExtraInfo(10, null, null);
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Hippos.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Hippos3[96 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Hippos3[95];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, actor.Unit.SkinColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
        }
    }

    class HipposBot1 : MainClothing
    {
        public HipposBot1()
        {
            DiscardSprite = SpriteDictionary.Hippos2[66];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
            Type = 84266;
            FixedColor = true;
        }
        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[61 + actor.Unit.BodySize];
            base.Configure(sprite, actor);
        }
    }

    class HipposBot2 : MainClothing
    {
        public HipposBot2()
        {
            DiscardSprite = SpriteDictionary.Hippos2[72];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
            Type = 84272;
            FixedColor = true;
        }
        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[67 + actor.Unit.BodySize];
            base.Configure(sprite, actor);
        }
    }

    class HipposBot3 : MainClothing
    {
        public HipposBot3()
        {
            DiscardSprite = SpriteDictionary.Hippos2[78];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
            Type = 84278;
            FixedColor = true;
        }
        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[73 + actor.Unit.BodySize];
            base.Configure(sprite, actor);
        }
    }

    class HipposBot4 : MainClothing
    {
        public HipposBot4()
        {
            DiscardSprite = SpriteDictionary.Hippos2[102];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
            Type = 84302;
            FixedColor = true;
        }
        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[97 + actor.Unit.BodySize];
            base.Configure(sprite, actor);
        }
    }

    class HipposHeadband1 : ClothingAccessory
    {
        public HipposHeadband1()
        {
            clothing1 = new SpriteExtraInfo(26, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[44];
            base.Configure(sprite, actor);
        }
    }

    class HipposHeadband2 : ClothingAccessory
    {
        public HipposHeadband2()
        {
            clothing1 = new SpriteExtraInfo(26, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[45];
            base.Configure(sprite, actor);
        }
    }

    class HipposHeadband3 : ClothingAccessory
    {
        public HipposHeadband3()
        {
            clothing1 = new SpriteExtraInfo(26, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[46];
            base.Configure(sprite, actor);
        }
    }

    class HipposHeadband4 : ClothingAccessory
    {
        public HipposHeadband4()
        {
            clothing1 = new SpriteExtraInfo(26, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[47];
            base.Configure(sprite, actor);
        }
    }

    class HipposHeadband5 : ClothingAccessory
    {
        public HipposHeadband5()
        {
            clothing1 = new SpriteExtraInfo(26, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[48];
            base.Configure(sprite, actor);
        }
    }

    class HipposHeadband6 : ClothingAccessory
    {
        public HipposHeadband6()
        {
            clothing1 = new SpriteExtraInfo(26, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[49];
            base.Configure(sprite, actor);
        }
    }

    class HipposHeadband7 : ClothingAccessory
    {
        public HipposHeadband7()
        {
            clothing1 = new SpriteExtraInfo(26, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[50];
            base.Configure(sprite, actor);
        }
    }

    class HipposHeadband8 : ClothingAccessory
    {
        public HipposHeadband8()
        {
            clothing1 = new SpriteExtraInfo(26, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[51];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }


    class HipposNecklace1 : ClothingAccessory
    {
        public HipposNecklace1()
        {
            clothing1 = new SpriteExtraInfo(21, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[36];
            base.Configure(sprite, actor);
        }
    }

    class HipposNecklace2 : ClothingAccessory
    {
        public HipposNecklace2()
        {
            clothing1 = new SpriteExtraInfo(21, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[37];
            base.Configure(sprite, actor);
        }
    }

    class HipposNecklace3 : ClothingAccessory
    {
        public HipposNecklace3()
        {
            clothing1 = new SpriteExtraInfo(21, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[38];
            base.Configure(sprite, actor);
        }
    }

    class HipposNecklace4 : ClothingAccessory
    {
        public HipposNecklace4()
        {
            clothing1 = new SpriteExtraInfo(21, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[39];
            base.Configure(sprite, actor);
        }
    }

    class HipposNecklace5 : ClothingAccessory
    {
        public HipposNecklace5()
        {
            clothing1 = new SpriteExtraInfo(21, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[40];
            base.Configure(sprite, actor);
        }
    }

    class HipposNecklace6 : ClothingAccessory
    {
        public HipposNecklace6()
        {
            clothing1 = new SpriteExtraInfo(21, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[41];
            base.Configure(sprite, actor);
        }
    }

    class HipposNecklace7 : ClothingAccessory
    {
        public HipposNecklace7()
        {
            clothing1 = new SpriteExtraInfo(21, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[42];
            base.Configure(sprite, actor);
        }
    }

    class HipposNecklace8 : ClothingAccessory
    {
        public HipposNecklace8()
        {
            clothing1 = new SpriteExtraInfo(21, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Hippos2[43];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
}