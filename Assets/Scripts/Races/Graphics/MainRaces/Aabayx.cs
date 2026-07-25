using System;
using System.Collections.Generic;
using UnityEngine;

class Aabayx : DefaultRaceData
{
    readonly Sprite[] Sprites5 = SpriteDictionary.HumansBodySprites4;
    readonly Sprite[] Sprites4 = SpriteDictionary.HumansVoreSprites;

    readonly AabayxRags Rags;

    public Aabayx()
    {
        BodySizes = 5;
        HairStyles = 0;
        HairColors = 0;
        BodyAccentTypes1 = 0; // eyebrows
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AabayxSkin);
        EyeTypes = 21;
        MouthTypes = 0;
        TailTypes = 16;
        SpecialAccessoryCount = 0;
        clothingColors = 0;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AabayxSkin); // Head color
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AabayxSkin);

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(5, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, s.Unit.AccessoryColor));
        BodyAccessory = null;
        BodyAccent = null;
        BodyAccent2 = null;
        BodyAccent3 = new SpriteExtraInfo(1, BodyAccentSprite3, WhiteColored); // Tail
        BodyAccent4 = null;
        BodyAccent5 = null;
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        Mouth = new SpriteExtraInfo(7, MouthSprite, WhiteColored); // Mouth
        Hair = null;
        Hair2 = null;
        Hair3 = null; // Eyebrows
        Beard = null;
        Eyes = new SpriteExtraInfo(8, EyesSprite, WhiteColored);
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(14, null, null,  (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(1, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = null;
        SecondaryBreasts = null;
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, s.Unit.SkinColor));

        Rags = new AabayxRags();
        
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new AabayxTop1(),
            new AabayxTop2(),
            new AabayxTop3(),
            new AabayxTop4(),
            new AabayxTop5(),
            new AabayxTop6(),
            new AabayxTop7(),
            Rags,
            RaceSpecificClothing.AabayxTopHighPriest,
        };
        AvoidedMainClothingTypes = 2;
        AllowedWaistTypes = new List<MainClothing>()
        {
            new AabayxPants1(),
            new AabayxPants2(),
            new AabayxPants3(),
            new AabayxPants4(),
            new AabayxPants5(),
            RaceSpecificClothing.AabayxPantsHighPriest,
        };
        ExtraMainClothing1Types = new List<MainClothing>()
        {
            new AabayxFacePaint1(),
            new AabayxFacePaint2(),
            new AabayxFacePaint3(),
            new AabayxFacePaint4(),
            new AabayxFacePaint5(),
            new AabayxFacePaint6(),
            new AabayxFacePaint7(),
        };
        AllowedClothingHatTypes = new List<ClothingAccessory>();
    }

    internal override int BreastSizes => 1;

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.TailType = State.Rand.Next(TailTypes);

        if (unit.Type == UnitType.Leader)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(RaceSpecificClothing.AabayxTopHighPriest);
            unit.ClothingType2 = 1 + AllowedWaistTypes.IndexOf(RaceSpecificClothing.AabayxPantsHighPriest);
        }

        if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(Rags);
            if (unit.ClothingType == 0) //Covers rags not in the list
                unit.ClothingType = AllowedMainClothingTypes.Count;
        }
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Dick, 0, 3 * .625f);
        AddOffset(Balls, 0, 2 * .625f);
    }

    //internal override void RandomCustom(Unit unit)
    //{
        //base.RandomCustom(unit);
        //if (unit.Type == UnitType.Leader)
        //    unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(RaceSpecificClothing.CatLeader);
    //}

    protected override Sprite BodySprite(Actor_Unit actor) 
    {
        if (actor.Unit.BodySize == 0)
            return SpriteDictionary.Aabayx[0 + (actor.IsAttacking ? 1 : 0)];
        if (actor.Unit.BodySize == 1)
            return SpriteDictionary.Aabayx[4 + (actor.IsAttacking ? 1 : 0)];
        if (actor.Unit.BodySize == 2)
            return SpriteDictionary.Aabayx[6 + (actor.IsAttacking ? 1 : 0)];
        if (actor.Unit.BodySize == 3)
            return SpriteDictionary.Aabayx[8 + (actor.IsAttacking ? 1 : 0)];
        if (actor.Unit.BodySize == 4)
            return SpriteDictionary.Aabayx[10 + (actor.IsAttacking ? 1 : 0)];
        else return null;
    }
    protected override Sprite HeadSprite(Actor_Unit actor) => SpriteDictionary.Aabayx[2];
    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return SpriteDictionary.Aabayx[3];
        else return null;
    }
    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return null;
        else return SpriteDictionary.Aabayx[12 + actor.Unit.EyeType];
    }
    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        return SpriteDictionary.Aabayx[71 + actor.Unit.TailType];
    }
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        int size = actor.GetStomachSize(21); // Credits to Yonell for Selicia sprites
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 21)
        {
            return SpriteDictionary.Aabayx[57];
        }
        else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 21)
        {
            return SpriteDictionary.Aabayx[56];
        }
        else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 20 || size == 19 )
        {
            return SpriteDictionary.Aabayx[55];
        }
        return actor.HasBelly ? SpriteDictionary.Aabayx[33 + actor.GetStomachSize(21)] : null;
    }
    internal override int DickSizes => 6;
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
    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            if (actor.Unit.Type == UnitType.Leader)
                return SpriteDictionary.AabayxLeader[5 + actor.GetWeaponSprite()];
            return SpriteDictionary.Aabayx[99 + actor.GetWeaponSprite()];
        }
        else
        {
            return null;
        }
    }

    //##################
    //#### CLOTHING ####
    //##################

    class AabayxTop1 : MainClothing
    {
        public AabayxTop1()
        {
            DiscardSprite = SpriteDictionary.Aabayx[65];
            coversBreasts = false;
            blocksDick = false;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(15, null, null);
            Type = 60701;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Aabayx[107 + (actor.IsAttacking ? 1 : 0)];
            base.Configure(sprite, actor);
        }
    }
    class AabayxTop2 : MainClothing
    {
        public AabayxTop2()
        {
            DiscardSprite = SpriteDictionary.Aabayx[123];
            coversBreasts = false;
            blocksDick = false;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(15, null, null);
            Type = 60702;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Aabayx[109 + (actor.IsAttacking ? 1 : 0)];
            base.Configure(sprite, actor);
        }
    }
    class AabayxTop3 : MainClothing
    {
        public AabayxTop3()
        {
            DiscardSprite = SpriteDictionary.Aabayx[124];
            coversBreasts = false;
            blocksDick = false;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(15, null, null);
            Type = 60703;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Aabayx[111 + (actor.IsAttacking ? 1 : 0)];
            base.Configure(sprite, actor);
        }
    }
    class AabayxTop4 : MainClothing
    {
        public AabayxTop4()
        {
            DiscardSprite = SpriteDictionary.Aabayx[125];
            coversBreasts = false;
            blocksDick = false;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(15, null, null);
            Type = 60704;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Aabayx[113 + (actor.IsAttacking ? 1 : 0)];
            base.Configure(sprite, actor);
        }
    }
    class AabayxTop5 : MainClothing
    {
        public AabayxTop5()
        {
            DiscardSprite = SpriteDictionary.Aabayx[126];
            coversBreasts = false;
            blocksDick = false;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(15, null, null);
            Type = 60705;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Aabayx[115 + (actor.IsAttacking ? 1 : 0)];
            base.Configure(sprite, actor);
        }
    }
    class AabayxTop6 : MainClothing
    {
        public AabayxTop6()
        {
            DiscardSprite = SpriteDictionary.Aabayx[127];
            coversBreasts = false;
            blocksDick = false;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(15, null, null);
            Type = 60706;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Aabayx[117 + (actor.IsAttacking ? 1 : 0)];
            base.Configure(sprite, actor);
        }
    }
    class AabayxTop7 : MainClothing
    {
        public AabayxTop7()
        {
            DiscardSprite = SpriteDictionary.Aabayx[128];
            coversBreasts = false;
            blocksDick = false;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(15, null, null);
            Type = 60707;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Aabayx[119 + (actor.IsAttacking ? 1 : 0)];
            base.Configure(sprite, actor);
        }
    }
    class AabayxRags : MainClothing
    {
        public AabayxRags()
        {
            DiscardSprite = SpriteDictionary.Aabayx[64];
            coversBreasts = false;
            blocksDick = false;
            FixedColor = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(15, null, null);
            clothing2 = new SpriteExtraInfo(13, null, null);
            Type = 60708;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Aabayx[122];
            clothing2.GetSprite = (s) => SpriteDictionary.Aabayx[121];
            base.Configure(sprite, actor);
        }
    }
    class AabayxPants1 : MainClothing
    {
        public AabayxPants1()
        {
            DiscardSprite = SpriteDictionary.Aabayx[58];
            coversBreasts = false;
            blocksDick = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(13, null, null);
            Type = 60709;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Aabayx[66];
            base.Configure(sprite, actor);
        }
    }
    class AabayxPants2 : MainClothing
    {
        public AabayxPants2()
        {
            DiscardSprite = SpriteDictionary.Aabayx[59];
            coversBreasts = false;
            blocksDick = false;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(13, null, null);
            Type = 60710;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Aabayx[67];
            base.Configure(sprite, actor);
        }
    }
    class AabayxPants3 : MainClothing
    {
        public AabayxPants3()
        {
            DiscardSprite = SpriteDictionary.Aabayx[60];
            coversBreasts = false;
            blocksDick = false;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(13, null, null);
            Type = 60711;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Aabayx[68];
            base.Configure(sprite, actor);
        }
    }
    class AabayxPants4 : MainClothing
    {
        public AabayxPants4()
        {
            DiscardSprite = SpriteDictionary.Aabayx[61];
            coversBreasts = false;
            blocksDick = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(13, null, null);
            Type = 60712;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Aabayx[69];
            base.Configure(sprite, actor);
        }
    }
    class AabayxPants5 : MainClothing
    {
        public AabayxPants5()
        {
            DiscardSprite = SpriteDictionary.Aabayx[62];
            coversBreasts = false;
            blocksDick = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(13, null, null);
            Type = 60713;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Aabayx[70];
            base.Configure(sprite, actor);
        }
    }
    class AabayxFacePaint1 : MainClothing
    {
        public AabayxFacePaint1()
        {
            DiscardSprite = null;
            leaderOnly = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, actor.Unit.EyeColor);
            clothing1.GetSprite = (s) =>
            {
                if (actor.IsOralVoring)
                    return SpriteDictionary.AabayxFacePaint[7];
                else 
                    return SpriteDictionary.AabayxFacePaint[0];
            };
            base.Configure(sprite, actor);
        }
    }
    class AabayxFacePaint2 : MainClothing
    {
        public AabayxFacePaint2()
        {
            DiscardSprite = null;
            leaderOnly = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, actor.Unit.EyeColor);
            clothing1.GetSprite = (s) =>
            {
                if (actor.IsOralVoring)
                    return SpriteDictionary.AabayxFacePaint[8];
                else 
                    return SpriteDictionary.AabayxFacePaint[1];
            };
            base.Configure(sprite, actor);
        }
    }
    class AabayxFacePaint3 : MainClothing
    {
        public AabayxFacePaint3()
        {
            DiscardSprite = null;
            leaderOnly = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, actor.Unit.EyeColor);
            clothing1.GetSprite = (s) =>
            {
                if (actor.IsOralVoring)
                    return SpriteDictionary.AabayxFacePaint[9];
                else 
                    return SpriteDictionary.AabayxFacePaint[2];
            };
            base.Configure(sprite, actor);
        }
    }
    class AabayxFacePaint4 : MainClothing
    {
        public AabayxFacePaint4()
        {
            DiscardSprite = null;
            leaderOnly = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, actor.Unit.EyeColor);
            clothing1.GetSprite = (s) =>
            {
                if (actor.IsOralVoring)
                    return SpriteDictionary.AabayxFacePaint[10];
                else 
                    return SpriteDictionary.AabayxFacePaint[3];
            };
            base.Configure(sprite, actor);
        }
    }
    class AabayxFacePaint5 : MainClothing
    {
        public AabayxFacePaint5()
        {
            DiscardSprite = null;
            leaderOnly = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, actor.Unit.EyeColor);
            clothing1.GetSprite = (s) =>
            {
                if (actor.IsOralVoring)
                    return SpriteDictionary.AabayxFacePaint[11];
                else 
                    return SpriteDictionary.AabayxFacePaint[4];
            };
            base.Configure(sprite, actor);
        }
    }
    class AabayxFacePaint6 : MainClothing
    {
        public AabayxFacePaint6()
        {
            DiscardSprite = null;
            leaderOnly = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, actor.Unit.EyeColor);
            clothing1.GetSprite = (s) =>
            {
                if (actor.IsOralVoring)
                    return SpriteDictionary.AabayxFacePaint[12];
                else 
                    return SpriteDictionary.AabayxFacePaint[5];
            };
            base.Configure(sprite, actor);
        }
    }
    class AabayxFacePaint7 : MainClothing
    {
        public AabayxFacePaint7()
        {
            DiscardSprite = null;
            leaderOnly = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, actor.Unit.EyeColor);
            clothing1.GetSprite = (s) =>
            {
                if (actor.IsOralVoring)
                    return null;
                else 
                    return SpriteDictionary.AabayxFacePaint[6];
            };
            base.Configure(sprite, actor);
        }
    }
}
