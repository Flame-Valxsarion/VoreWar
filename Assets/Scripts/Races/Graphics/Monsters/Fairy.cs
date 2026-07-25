using System;
using System.Collections.Generic;
using UnityEngine;

enum FairyType : int
{
    Spring,
    Summer,
    Fall,
    Winter
}

static class FairyUtil
{

    internal static void SetSeason(Unit unit, FairyType season)
    {
        unit.BodyAccentType1 = (int)season;
        switch (season)
        {
            case FairyType.Spring:
                unit.InnateSpells = new List<SpellTypes>() { SpellTypes.Speed };
                break;
            case FairyType.Summer:
                unit.InnateSpells = new List<SpellTypes>() { SpellTypes.Valor };
                break;
            case FairyType.Fall:
                unit.InnateSpells = new List<SpellTypes>() { SpellTypes.Predation };
                break;
            case FairyType.Winter:
                unit.InnateSpells = new List<SpellTypes>() { SpellTypes.Shield };
                break;
        }
    }
    internal static FairyType GetSeason(Unit unit) => (FairyType)unit.BodyAccentType1;

    internal static ColorSwapPalette GetClothesColor(Actor_Unit actor)
    {
        switch (GetSeason(actor.Unit))
        {
            case FairyType.Spring:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairySpringClothes, actor.Unit.ClothingColor);
            case FairyType.Summer:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairySummerClothes, actor.Unit.ClothingColor);
            case FairyType.Fall:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairyFallClothes, actor.Unit.ClothingColor);
            default:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairyWinterClothes, actor.Unit.ClothingColor);
        }
    }
}

class Fairy : BlankSlate
{
    readonly Sprite[] Sprites = SpriteDictionary.Fairy;
    readonly Sprite[] Sprites240 = SpriteDictionary.Fairy240;

    const float GeneralSizeMod = 0.8f;

    RaceFrameList SpringWings = new RaceFrameList(new int[3] { 91, 92, 93 }, new float[3] { .2f, .2f, .2f });
    RaceFrameList SummerWings = new RaceFrameList(new int[3] { 94, 95, 96 }, new float[3] { .2f, .2f, .2f });
    RaceFrameList FallWings = new RaceFrameList(new int[3] { 97, 98, 99 }, new float[3] { .2f, .2f, .2f });
    RaceFrameList WinterWings = new RaceFrameList(new int[3] { 100, 101, 102 }, new float[3] { .2f, .2f, .2f });
    RaceFrameList SpringWingsEnc = new RaceFrameList(new int[3] { 103, 104, 105 }, new float[3] { .2f, .2f, .2f });
    RaceFrameList SummerWingsEnc = new RaceFrameList(new int[3] { 106, 107, 108 }, new float[3] { .2f, .2f, .2f });
    RaceFrameList FallWingsEnc = new RaceFrameList(new int[3] { 109, 110, 111 }, new float[3] { .2f, .2f, .2f });
    RaceFrameList WinterWingsEnc = new RaceFrameList(new int[3] { 112, 113, 114 }, new float[3] { .2f, .2f, .2f });

    bool Encumbered;
    bool VeryEncumbered;
    FairyType Season;

    public Fairy()
    {

        GentleAnimation = true;
        Hair = new SpriteExtraInfo(6, HairSprite, null, GetHairColor);
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, GetHairColor);

        Breasts = new SpriteExtraInfo(14, BreastsSprite, null, GetSkinColor);
        SecondaryBreasts = new SpriteExtraInfo(14, SecondaryBreastsSprite, null, GetSkinColor);

        Dick = new SpriteExtraInfo(12, DickSprite, null, GetSkinColor);
        Balls = new SpriteExtraInfo(11, BallsSprite, null, GetSkinColor);

        Belly = new SpriteExtraInfo(13, null, null, GetSkinColor);

        Body = new SpriteExtraInfo(2, BodySprite, null, GetSkinColor);
        BodyAccent = new SpriteExtraInfo(16, BodyAccentSprite, null, GetSkinColor);
        BodyAccent2 = new SpriteExtraInfo(4, BodyAccentSprite2, null, GetSkinColor);
        BodyAccent3 = new SpriteExtraInfo(0, BodyAccentSprite3, WhiteColored);

        ExtendedBreastSprites = true;

        HairStyles = 8;
        BodyAccentTypes1 = 4;
        SkinColors = 3;
        HairColors = 5;
        clothingColors = 5;
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            nightie,
            onePiece,
            twoPiece,
            dress,
            loincloth
        };
        AllowedClothingAccessoryTypes = new List<ClothingAccessory>()
        {
            sleeves,
            bracelets
        };
        AllowedClothingHatTypes = new List<ClothingAccessory>()
        {
            leggings,
            sandals
        };

    }

    internal override int BreastSizes => 3;
    internal override int DickSizes => 2;

    Nightie nightie = new Nightie();
    OnePiece onePiece = new OnePiece();
    TwoPiece twoPiece = new TwoPiece();
    Dress dress = new Dress();
    Loincloth loincloth = new Loincloth();

    Sleeves sleeves = new Sleeves();
    Bracelets bracelets = new Bracelets();
    Leggings leggings = new Leggings();
    Sandals sandals = new Sandals();

    internal override void RunFirst(Actor_Unit actor)
    {
        Season = (FairyType)actor.Unit.BodyAccentType1;
        Encumbered = actor.PredatorComponent?.Fullness > 0; // Not 100% accurate, but saves effort
        VeryEncumbered = actor.GetRootedStomachSize(19, GeneralSizeMod) > 16;
        base.RunFirst(actor);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.HairStyle = State.Rand.Next(7);
        FairyUtil.SetSeason(unit, (FairyType)State.Rand.Next(4));
    }


    ColorSwapPalette GetHairColor(Actor_Unit actor)
    {
        switch (Season)
        {
            case FairyType.Spring:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairySpringClothes, actor.Unit.HairColor);
            case FairyType.Summer:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairySummerClothes, actor.Unit.HairColor);
            case FairyType.Fall:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairyFallClothes, actor.Unit.HairColor);
            default:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairyWinterClothes, actor.Unit.HairColor);
        }
    }



    ColorSwapPalette GetSkinColor(Actor_Unit actor)
    {
        switch (Season)
        {
            case FairyType.Spring:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairySpringSkin, actor.Unit.SkinColor);
            case FairyType.Summer:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairySummerSkin, actor.Unit.SkinColor);
            case FairyType.Fall:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairyFallSkin, actor.Unit.SkinColor);
            default:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairyWinterSkin, actor.Unit.SkinColor);
        }
    }


    protected override Sprite HairSprite(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle < 3)
            return Sprites[2 * actor.Unit.HairStyle];
        if (actor.Unit.HairStyle > 3)
            return SpriteDictionary.FairyExtraHair[Math.Min(actor.Unit.HairStyle - 4, 3)];
        switch (Season)
        {
            case FairyType.Spring:
                return Sprites[6];
            case FairyType.Summer:
                return Sprites[8];
            case FairyType.Fall:
                return Sprites[7];
            case FairyType.Winter:
                return Sprites[10];
        }
        return null;
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle < 3)
            return Sprites[1 + 2 * actor.Unit.HairStyle];
        if (actor.Unit.HairStyle > 3)
            return null;
        if (Season == FairyType.Summer)
            return Sprites[9];
        return null;
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (VeryEncumbered)
        {
            if (actor.IsAttacking)
                return Sprites[205];
            return Sprites[204];
        }
        if (Encumbered)
        {
            if (actor.IsAttacking)
                return Sprites[87];
            return Sprites[84];
        }
        else
        {
            if (actor.IsAttacking)
                return Sprites[85];
            return Sprites[82];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (VeryEncumbered)
        {
            if (actor.IsAttacking)
                return Sprites[206];
            return null;
        }
        if (Encumbered)
        {
            if (actor.IsAttacking)
                return Sprites[88];
        }
        else
        {
            if (actor.IsAttacking)
                return Sprites[86];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (VeryEncumbered && actor.IsEating)
        {
            if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false)
                return Sprites[207];
        }
        if (Encumbered)
            return Sprites[83];
        return null;
    }


    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(State.Rand.Next(0, 3), 0, true)};
    }

    /// <summary>
    /// Wings
    /// </summary>
    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        if (actor.AnimationController.frameLists[0].currentTime >= SpringWings.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
        {
            actor.AnimationController.frameLists[0].currentFrame++;
            actor.AnimationController.frameLists[0].currentTime = 0f;

            if (actor.AnimationController.frameLists[0].currentFrame >= SpringWings.frames.Length)
            {
                actor.AnimationController.frameLists[0].currentFrame = 0;
                actor.AnimationController.frameLists[0].currentTime = 0f;
            }
        }

        if (Encumbered)
        {
            switch (Season)
            {
                case FairyType.Spring:
                    return SpriteDictionary.Fairy[SpringWingsEnc.frames[actor.AnimationController.frameLists[0].currentFrame]];
                case FairyType.Summer:
                    return SpriteDictionary.Fairy[SummerWingsEnc.frames[actor.AnimationController.frameLists[0].currentFrame]];
                case FairyType.Fall:
                    return SpriteDictionary.Fairy[FallWingsEnc.frames[actor.AnimationController.frameLists[0].currentFrame]];
                default:
                    return SpriteDictionary.Fairy[WinterWingsEnc.frames[actor.AnimationController.frameLists[0].currentFrame]];
            }
        }
        else
        {
            switch (Season)
            {
                case FairyType.Spring:
                    return SpriteDictionary.Fairy[SpringWings.frames[actor.AnimationController.frameLists[0].currentFrame]];
                case FairyType.Summer:
                    return SpriteDictionary.Fairy[SummerWings.frames[actor.AnimationController.frameLists[0].currentFrame]];
                case FairyType.Fall:
                    return SpriteDictionary.Fairy[FallWings.frames[actor.AnimationController.frameLists[0].currentFrame]];
                default:
                    return SpriteDictionary.Fairy[WinterWings.frames[actor.AnimationController.frameLists[0].currentFrame]];
            }
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.GetBallSize(19, GeneralSizeMod) > 4)
        {
            return Sprites[119 + actor.Unit.DickSize];
        }
        else
        {
            if (Encumbered)
                return Sprites[117 + actor.Unit.DickSize];
            return Sprites[115 + actor.Unit.DickSize];
        }
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.PredatorComponent?.BallsFullness > 0)
        {
            int ballSize = actor.GetBallSize(17, GeneralSizeMod);
            //AddOffset(Balls, 0, -10 * .625f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
            {
                AddOffset(Balls, 0, -10 * .625f);
                return Sprites240[17];
            }

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) && ballSize == 17)
            {
                AddOffset(Balls, 0, -10 * .625f);
                return Sprites240[16];
            }

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) && ballSize == 16)
            {
                AddOffset(Balls, 0, -10 * .625f);
                return Sprites240[15];
            }

            if (ballSize >= 16)
            {
                AddOffset(Balls, 0, -10 * .625f);
                return Sprites240[14];
            }

            if (ballSize == 15)
                AddOffset(Balls, 0, -24 * .625f);
            return Sprites[123 + ballSize];
        }
        else
        {
            if (Encumbered)
                return Sprites[123 + actor.Unit.DickSize];
            return Sprites[121 + actor.Unit.DickSize];
        }
    }

    /// <summary>
    /// Left Breast Sprite / Combo sprite
    /// </summary>
    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt(actor.GetLeftBreastSize(21 * 21, GeneralSizeMod));

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast))
            {
                AddOffset(Breasts, -34 * .625f, -57 * .625f);
                return Sprites240[8];
            }

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize == 21)
            {
                AddOffset(Breasts, -34 * .625f, -57 * .625f);
                return Sprites240[7];
            }

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize > 20)
            {
                AddOffset(Breasts, -34 * .625f, -57 * .625f);
                return Sprites240[6];
            }


            if (leftSize == 21)
            {
                AddOffset(Breasts, -34 * .625f, -57 * .625f);
                return Sprites240[5];
            }

            if (leftSize == 20)
            {
                AddOffset(Breasts, 0, -12 * .625f);
                return Sprites240[4];
            }

            if (leftSize == 19)
            {
                AddOffset(Breasts, 0, -12 * .625f);
                return Sprites[146 + leftSize];
            }

            return Sprites[146 + leftSize];
        }
        else if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            return Sprites[146];
        }
        else
        {
            if (Encumbered)
                return Sprites[140 + actor.Unit.BreastSize];
            else
                return Sprites[143 + actor.Unit.BreastSize];
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
            int rightSize = (int)Math.Sqrt(actor.GetRightBreastSize(21 * 21, GeneralSizeMod));
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast))
            {
                AddOffset(SecondaryBreasts, 34 * .625f, -57 * .625f);
                return Sprites240[13];
            }

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize == 21)
            {
                AddOffset(SecondaryBreasts, 34 * .625f, -57 * .625f);
                return Sprites240[12];
            }

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize > 20)
            {
                AddOffset(SecondaryBreasts, 34 * .625f, -57 * .625f);
                return Sprites240[11];
            }

            if (rightSize == 21)
            {
                AddOffset(SecondaryBreasts, 34 * .625f, -57 * .625f);
                return Sprites240[10];
            }

            if (rightSize == 20)
            {
                AddOffset(SecondaryBreasts, 0, -12 * .625f);
                return Sprites240[9];
            }

            if (rightSize == 19)
            {
                AddOffset(SecondaryBreasts, 0, -12 * .625f);
                return Sprites[166 + rightSize];
            }

            return Sprites[166 + rightSize];
        }
        else if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            return Sprites[166];
        }
        return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.SetActive(true);

            belly.transform.localScale = new Vector3(1, 1, 1);

            int bellySprite = actor.GetRootedStomachSize(18, GeneralSizeMod);

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                return Sprites240[3];

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && bellySprite == 18)
                return Sprites240[2];

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && bellySprite > 17)
                return Sprites240[1];

            if (bellySprite == 18)
                return Sprites240[0];
            return Sprites[186 + actor.GetRootedStomachSize(18, GeneralSizeMod)];
        }
        else
        {
            return null;
        }
    }

    class Nightie : MainClothing
    {

        public Nightie()
        {
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(17, null, null);
            clothing2 = new SpriteExtraInfo(18, null, null);
            clothing3 = new SpriteExtraInfo(18, null, null);
            DiscardSprite = SpriteDictionary.Fairy[20];
            clothing1.GetPalette = FairyUtil.GetClothesColor;
            clothing2.GetPalette = FairyUtil.GetClothesColor;
            clothing3.GetPalette = FairyUtil.GetClothesColor;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.HasBelly || actor.GetLeftBreastSize(19) > 2 || actor.GetRightBreastSize(19) > 2)
            {
                clothing1.layer = 5;
                clothing2.layer = 6;
                clothing3.layer = 6;
            }
            else
            {
                clothing1.layer = 17;
                clothing2.layer = 18;
                clothing3.layer = 18;
            }
            int mainSprite = 11;
            if (actor.HasBelly == false) mainSprite = 11;
            else if (actor.GetRootedStomachSize(19, GeneralSizeMod) == 0) mainSprite = 12;
            else mainSprite = 13;

            if (actor.Unit.HasBreasts && (Math.Sqrt(actor.GetLeftBreastSize(21 * 21, GeneralSizeMod)) > 3 || Math.Sqrt(actor.GetRightBreastSize(21 * 21, GeneralSizeMod)) > 3) == false)
            {
                int encumMod = 0;
                if (actor.PredatorComponent?.Fullness > 0)
                {
                    encumMod = 3;
                }
                int leftSprite = 14 + actor.Unit.BreastSize + encumMod;
                int rightSprite = 21 + actor.Unit.BreastSize + encumMod;
                clothing2.GetSprite = (s) => SpriteDictionary.Fairy[leftSprite];
                clothing3.GetSprite = (s) => SpriteDictionary.Fairy[rightSprite];
            }
            else
            {
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing1.GetSprite = (s) => SpriteDictionary.Fairy[mainSprite];
            base.Configure(sprite, actor);
        }
    }

    class OnePiece : MainClothing
    {

        public OnePiece()
        {
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(17, null, null);
            clothing2 = new SpriteExtraInfo(18, null, null);
            clothing3 = new SpriteExtraInfo(18, null, null);
            DiscardSprite = SpriteDictionary.Fairy[20];
            clothing1.GetPalette = FairyUtil.GetClothesColor;
            clothing2.GetPalette = FairyUtil.GetClothesColor;
            clothing3.GetPalette = FairyUtil.GetClothesColor;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.HasBelly || actor.HasPreyInBreasts)
            {
                clothing1.layer = 5;
                clothing2.layer = 6;
                clothing3.layer = 6;
            }
            else
            {
                clothing1.layer = 17;
                clothing2.layer = 18;
                clothing3.layer = 18;
            }
            int mainSprite = 27;
            if (actor.HasBelly == false) mainSprite = 27;
            else if (actor.GetRootedStomachSize(19, GeneralSizeMod) == 0) mainSprite = 28;
            else mainSprite = 29;

            if (actor.Unit.HasBreasts && (Math.Sqrt(actor.GetLeftBreastSize(21 * 21, GeneralSizeMod)) > 3 || Math.Sqrt(actor.GetRightBreastSize(21 * 21, GeneralSizeMod)) > 3) == false)
            {
                int encumMod = 0;
                if (actor.PredatorComponent?.Fullness > 0)
                {
                    encumMod = 3;
                }
                int leftSprite = 30 + actor.Unit.BreastSize + encumMod;
                int rightSprite = 37 + actor.Unit.BreastSize + encumMod;
                if (actor.PredatorComponent?.LeftBreastFullness > 0)
                    leftSprite = 36;
                if (actor.PredatorComponent?.RightBreastFullness > 0)
                    rightSprite = 43;

                clothing2.GetSprite = (s) => SpriteDictionary.Fairy[leftSprite];
                clothing3.GetSprite = (s) => SpriteDictionary.Fairy[rightSprite];
            }
            else
            {
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing1.GetSprite = (s) => SpriteDictionary.Fairy[mainSprite];
            base.Configure(sprite, actor);
        }
    }

    class TwoPiece : MainClothing
    {

        public TwoPiece()
        {
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(17, null, null);
            clothing2 = new SpriteExtraInfo(18, null, null);
            clothing3 = new SpriteExtraInfo(18, null, null);
            DiscardSprite = SpriteDictionary.Fairy[20];
            clothing1.GetPalette = FairyUtil.GetClothesColor;
            clothing2.GetPalette = FairyUtil.GetClothesColor;
            clothing3.GetPalette = FairyUtil.GetClothesColor;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.HasBelly || actor.HasPreyInBreasts)
            {
                clothing1.layer = 5;
                clothing2.layer = 6;
                clothing3.layer = 6;
            }
            else
            {
                clothing1.layer = 17;
                clothing2.layer = 18;
                clothing3.layer = 18;
            }
            if (actor.HasBelly == false) clothing1.GetSprite = (s) => SpriteDictionary.Fairy[44];
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts && (Math.Sqrt(actor.GetLeftBreastSize(21 * 21, GeneralSizeMod)) > 3 || Math.Sqrt(actor.GetRightBreastSize(21 * 21, GeneralSizeMod)) > 3) == false)
            {
                int encumMod = 0;
                if (actor.PredatorComponent?.Fullness > 0)
                {
                    encumMod = 3;
                }
                int leftSprite = 45 + actor.Unit.BreastSize + encumMod;
                int rightSprite = 52 + actor.Unit.BreastSize + encumMod;
                if (actor.PredatorComponent?.LeftBreastFullness > 0 || actor.PredatorComponent?.RightBreastFullness > 0)
                {
                    leftSprite = 51;
                    rightSprite = 0;
                }

                clothing2.GetSprite = (s) => SpriteDictionary.Fairy[leftSprite];
                if (rightSprite == 0)
                    clothing3.GetSprite = null;
                else
                    clothing3.GetSprite = (s) => SpriteDictionary.Fairy[rightSprite];
            }
            else
            {
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            base.Configure(sprite, actor);
        }
    }

    class Dress : MainClothing
    {

        public Dress()
        {
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(17, null, null);
            clothing2 = new SpriteExtraInfo(18, null, null);
            DiscardSprite = SpriteDictionary.Fairy[20];
            clothing1.GetPalette = FairyUtil.GetClothesColor;
            clothing2.GetPalette = FairyUtil.GetClothesColor;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.HasBelly || actor.HasPreyInBreasts)
            {
                clothing1.layer = 5;
                clothing2.layer = 6;
            }
            else
            {
                clothing1.layer = 17;
                clothing2.layer = 18;
            }
            bool oversize = actor.GetLeftBreastSize(19) > 2 || actor.GetRightBreastSize(19) > 2 || actor.GetRootedStomachSize(19) > 2;


            if (oversize)
                clothing1.GetSprite = (s) => SpriteDictionary.Fairy[62];
            else if (actor.HasBelly)
                clothing1.GetSprite = (s) => SpriteDictionary.Fairy[60 + Math.Min(actor.GetRootedStomachSize(19, GeneralSizeMod), 1)];
            else
                clothing1.GetSprite = (s) => SpriteDictionary.Fairy[59];

            if (actor.Unit.HasBreasts && (Math.Sqrt(actor.GetLeftBreastSize(21 * 21, GeneralSizeMod)) > 3 || Math.Sqrt(actor.GetRightBreastSize(21 * 21, GeneralSizeMod)) > 3) == false)
            {
                int encumMod = 0;
                if (actor.PredatorComponent?.Fullness > 0)
                {
                    encumMod = 3;
                }
                clothing2.GetSprite = (s) => SpriteDictionary.Fairy[65 + actor.Unit.BreastSize + encumMod];
            }
            else
            {
                clothing2.GetSprite = null;
            }

            base.Configure(sprite, actor);
        }
    }

    class Loincloth : MainClothing
    {

        public Loincloth()
        {
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, null);
            clothing2 = new SpriteExtraInfo(18, null, null);
            DiscardSprite = SpriteDictionary.Fairy[20];
            clothing1.GetPalette = FairyUtil.GetClothesColor;
            clothing2.GetPalette = FairyUtil.GetClothesColor;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.HasBelly || actor.HasPreyInBreasts)
            {
                clothing1.layer = 5;
                clothing2.layer = 6;
            }
            else
            {
                clothing1.layer = 17;
                clothing2.layer = 18;
            }
            clothing1.GetSprite = (s) => SpriteDictionary.Fairy[63];

            if (actor.Unit.HasBreasts && (Math.Sqrt(actor.GetLeftBreastSize(21 * 21, GeneralSizeMod)) > 3 || Math.Sqrt(actor.GetRightBreastSize(21 * 21, GeneralSizeMod)) > 3) == false)
            {
                int encumMod = 0;
                if (actor.PredatorComponent?.Fullness > 0)
                {
                    encumMod = 3;
                }
                clothing2.GetSprite = (s) => SpriteDictionary.Fairy[65 + actor.Unit.BreastSize + encumMod];
            }
            else
            {
                clothing2.GetSprite = null;
            }

            base.Configure(sprite, actor);
        }
    }

    class Sleeves : ClothingAccessory
    {
        public Sleeves()
        {
            clothing1 = new SpriteExtraInfo(10, null, null);
            clothing1.GetPalette = FairyUtil.GetClothesColor;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.layer = 10;
            if (actor.PredatorComponent?.Fullness > 0)
            {
                if (actor.IsAttacking)
                {
                    clothing1.layer = 18;
                    clothing1.GetSprite = (s) => SpriteDictionary.Fairy[74];
                }
                clothing1.GetSprite = (s) => SpriteDictionary.Fairy[72];
            }
            else
            {
                if (actor.IsAttacking)
                {
                    clothing1.layer = 18;
                    clothing1.GetSprite = (s) => SpriteDictionary.Fairy[73];
                }
                clothing1.GetSprite = (s) => SpriteDictionary.Fairy[71];
            }
            base.Configure(sprite, actor);
        }
    }

    class Bracelets : ClothingAccessory
    {
        public Bracelets()
        {
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.layer = 10;
            if (actor.PredatorComponent?.Fullness > 0)
            {
                if (actor.IsAttacking)
                {
                    clothing1.layer = 18;
                    clothing1.GetSprite = (s) => SpriteDictionary.Fairy[77];
                }

                clothing1.GetSprite = (s) => SpriteDictionary.Fairy[76];
            }
            else
            {
                if (actor.IsAttacking)
                {
                    clothing1.layer = 18;
                    clothing1.GetSprite = (s) => SpriteDictionary.Fairy[77];
                }
                clothing1.GetSprite = (s) => SpriteDictionary.Fairy[75];
            }
            base.Configure(sprite, actor);
        }

    }

    class Leggings : ClothingAccessory
    {
        public Leggings()
        {
            clothing1 = new SpriteExtraInfo(8, null, WhiteColored);
            clothing1.GetPalette = FairyUtil.GetClothesColor;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.PredatorComponent?.Fullness > 0)
                clothing1.GetSprite = (s) => SpriteDictionary.Fairy[79];
            else
                clothing1.GetSprite = (s) => SpriteDictionary.Fairy[78];
            base.Configure(sprite, actor);
        }

    }

    class Sandals : ClothingAccessory
    {
        public Sandals()
        {
            clothing1 = new SpriteExtraInfo(8, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.PredatorComponent?.Fullness > 0)
                clothing1.GetSprite = (s) => SpriteDictionary.Fairy[81];
            else
                clothing1.GetSprite = (s) => SpriteDictionary.Fairy[80];
            base.Configure(sprite, actor);
        }

    }

}
