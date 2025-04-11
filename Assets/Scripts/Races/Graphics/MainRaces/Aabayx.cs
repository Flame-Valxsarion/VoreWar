using System;
using System.Collections.Generic;
using UnityEngine;

class Aabayx : DefaultRaceData
{
    readonly Sprite[] Sprites5 = State.GameManager.SpriteDictionary.HumansBodySprites4;
    readonly Sprite[] Sprites4 = State.GameManager.SpriteDictionary.HumansVoreSprites;
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
        
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new AabayxTop1(),
            new AabayxTop2(),
            new AabayxTop3(),
            new AabayxTop4(),
            new AabayxTop5(),
            new AabayxTop6(),
            new AabayxTop7(),
        };
        AvoidedMainClothingTypes = 0;
        AllowedWaistTypes = new List<MainClothing>()
        {
            new AabayxPants1(),
            new AabayxPants2(),
            new AabayxPants3(),
            new AabayxPants4(),
            new AabayxPants5(),
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
            return State.GameManager.SpriteDictionary.Aabayx[0 + (actor.IsAttacking ? 1 : 0)];
        if (actor.Unit.BodySize == 1)
            return State.GameManager.SpriteDictionary.Aabayx[4 + (actor.IsAttacking ? 1 : 0)];
        if (actor.Unit.BodySize == 2)
            return State.GameManager.SpriteDictionary.Aabayx[6 + (actor.IsAttacking ? 1 : 0)];
        if (actor.Unit.BodySize == 3)
            return State.GameManager.SpriteDictionary.Aabayx[8 + (actor.IsAttacking ? 1 : 0)];
        if (actor.Unit.BodySize == 4)
            return State.GameManager.SpriteDictionary.Aabayx[10 + (actor.IsAttacking ? 1 : 0)];
        else return null;
    }
    protected override Sprite HeadSprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Aabayx[2];
    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return State.GameManager.SpriteDictionary.Aabayx[3];
        else return null;
    }
    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return null;
        else return State.GameManager.SpriteDictionary.Aabayx[12 + actor.Unit.EyeType];
    }
    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        return State.GameManager.SpriteDictionary.Aabayx[60 + actor.Unit.TailType];
    }
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        return actor.HasBelly ? State.GameManager.SpriteDictionary.Aabayx[33 + actor.GetStomachSize(21)] : null;
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
            return State.GameManager.SpriteDictionary.Aabayx[88 + actor.GetWeaponSprite()];
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
            DiscardSprite = null;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(15, null, null);
            Type = 60018;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Aabayx[96 + (actor.IsAttacking ? 1 : 0)];
            base.Configure(sprite, actor);
        }
    }
    class AabayxTop2 : MainClothing
    {
        public AabayxTop2()
        {
            DiscardSprite = null;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(15, null, null);
            Type = 60018;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Aabayx[98 + (actor.IsAttacking ? 1 : 0)];
            base.Configure(sprite, actor);
        }
    }
    class AabayxTop3 : MainClothing
    {
        public AabayxTop3()
        {
            DiscardSprite = null;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(15, null, null);
            Type = 60018;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Aabayx[100 + (actor.IsAttacking ? 1 : 0)];
            base.Configure(sprite, actor);
        }
    }
    class AabayxTop4 : MainClothing
    {
        public AabayxTop4()
        {
            DiscardSprite = null;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(15, null, null);
            Type = 60018;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Aabayx[102 + (actor.IsAttacking ? 1 : 0)];
            base.Configure(sprite, actor);
        }
    }
    class AabayxTop5 : MainClothing
    {
        public AabayxTop5()
        {
            DiscardSprite = null;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(15, null, null);
            Type = 60018;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Aabayx[104 + (actor.IsAttacking ? 1 : 0)];
            base.Configure(sprite, actor);
        }
    }
    class AabayxTop6 : MainClothing
    {
        public AabayxTop6()
        {
            DiscardSprite = null;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(15, null, null);
            Type = 60018;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Aabayx[106 + (actor.IsAttacking ? 1 : 0)];
            base.Configure(sprite, actor);
        }
    }
    class AabayxTop7 : MainClothing
    {
        public AabayxTop7()
        {
            DiscardSprite = null;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(15, null, null);
            Type = 60018;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Aabayx[108 + (actor.IsAttacking ? 1 : 0)];
            base.Configure(sprite, actor);
        }
    }
    class AabayxPants1 : MainClothing
    {
        public AabayxPants1()
        {
            DiscardSprite = null;
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(13, null, null);
            Type = 60018;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Aabayx[55];
            base.Configure(sprite, actor);
        }
    }
    class AabayxPants2 : MainClothing
    {
        public AabayxPants2()
        {
            DiscardSprite = null;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            Type = 60018;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Aabayx[56];
            base.Configure(sprite, actor);
        }
    }
    class AabayxPants3 : MainClothing
    {
        public AabayxPants3()
        {
            DiscardSprite = null;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            Type = 60018;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Aabayx[57];
            base.Configure(sprite, actor);
        }
    }
    class AabayxPants4 : MainClothing
    {
        public AabayxPants4()
        {
            DiscardSprite = null;
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(13, null, null);
            Type = 60018;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Aabayx[58];
            base.Configure(sprite, actor);
        }
    }
    class AabayxPants5 : MainClothing
    {
        public AabayxPants5()
        {
            DiscardSprite = null;
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(13, null, null);
            Type = 60018;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Aabayx[59];
            base.Configure(sprite, actor);
        }
    }
    class AabayxFacePaint1 : MainClothing
    {
        public AabayxFacePaint1()
        {
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
                    return State.GameManager.SpriteDictionary.AabayxFacePaint[7];
                else 
                    return State.GameManager.SpriteDictionary.AabayxFacePaint[0];
            };
            base.Configure(sprite, actor);
        }
    }
    class AabayxFacePaint2 : MainClothing
    {
        public AabayxFacePaint2()
        {
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
                    return State.GameManager.SpriteDictionary.AabayxFacePaint[8];
                else 
                    return State.GameManager.SpriteDictionary.AabayxFacePaint[1];
            };
            base.Configure(sprite, actor);
        }
    }
    class AabayxFacePaint3 : MainClothing
    {
        public AabayxFacePaint3()
        {
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
                    return State.GameManager.SpriteDictionary.AabayxFacePaint[9];
                else 
                    return State.GameManager.SpriteDictionary.AabayxFacePaint[2];
            };
            base.Configure(sprite, actor);
        }
    }
    class AabayxFacePaint4 : MainClothing
    {
        public AabayxFacePaint4()
        {
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
                    return State.GameManager.SpriteDictionary.AabayxFacePaint[10];
                else 
                    return State.GameManager.SpriteDictionary.AabayxFacePaint[3];
            };
            base.Configure(sprite, actor);
        }
    }
    class AabayxFacePaint5 : MainClothing
    {
        public AabayxFacePaint5()
        {
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
                    return State.GameManager.SpriteDictionary.AabayxFacePaint[11];
                else 
                    return State.GameManager.SpriteDictionary.AabayxFacePaint[4];
            };
            base.Configure(sprite, actor);
        }
    }
    class AabayxFacePaint6 : MainClothing
    {
        public AabayxFacePaint6()
        {
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
                    return State.GameManager.SpriteDictionary.AabayxFacePaint[12];
                else 
                    return State.GameManager.SpriteDictionary.AabayxFacePaint[5];
            };
            base.Configure(sprite, actor);
        }
    }
    class AabayxFacePaint7 : MainClothing
    {
        public AabayxFacePaint7()
        {
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
                    return State.GameManager.SpriteDictionary.AabayxFacePaint[6];
            };
            base.Configure(sprite, actor);
        }
    }
}
