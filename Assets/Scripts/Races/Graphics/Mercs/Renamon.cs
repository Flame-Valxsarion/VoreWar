using System;
using System.Collections.Generic;
using UnityEngine;

class Renamon : DefaultRaceData
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.RenamonBodies;
    readonly Sprite[] Sprites2 = State.GameManager.SpriteDictionary.RenamonClothes1;
    readonly Sprite[] Sprites3 = State.GameManager.SpriteDictionary.RenamonClothes2;
    readonly Sprite[] Sprites4 = State.GameManager.SpriteDictionary.RenamonClothes3;
    readonly Sprite[] Sprites5 = State.GameManager.SpriteDictionary.RenamonVore;

    bool oversize = false;


    public Renamon()
    {
        BodySizes = 4;
        EyeTypes = 12;
        MouthTypes = 8;
        SpecialAccessoryCount = 16; // ears     
        BodyAccentTypes1 = 8; // Head types
        BodyAccentTypes2 = 6; // Leg markings
        BodyAccentTypes3 = 13; // chest fluff
        BodyAccentTypes4 = 12; // tail types
        HairStyles = 0;
        EyeColors = 0;
        AccessoryColors = 0;
        HairColors = 0;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.RenamonSkin);

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(5, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(22, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(21, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor)); // Ears
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor)); // Arms
        BodyAccent2 = new SpriteExtraInfo(6, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor)); // Leg markings
        BodyAccent3 = new SpriteExtraInfo(21, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor)); // Chest fluff
        BodyAccent4 = new SpriteExtraInfo(1, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor)); // Tails
        BodyAccent5 = new SpriteExtraInfo(5, BodyAccentSprite5, WhiteColored); // Melee Attack Sprite
        BodyAccent6 = new SpriteExtraInfo(2, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor)); // Arm fluff
        BodyAccent7 = new SpriteExtraInfo(23, BodyAccentSprite7, WhiteColored); // Mouth details
        BodyAccent8 = null;
        Mouth = new SpriteExtraInfo(23, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor));
        Eyes = new SpriteExtraInfo(24, EyesSprite, WhiteColored);
        SecondaryAccessory = new SpriteExtraInfo(24, SecondaryAccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor)); // Eyebrows
        SecondaryEyes = null;
        Hair = null;
        Hair2 = null;
        Belly = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor));
        Weapon = null;
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor));

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new PurpleTop1(),
            new PurpleTop2(),
            new PurpleTop3(),
            new GenericTop1(),
            new GenericTop2(),
            new GenericTop3(),
            new GenericTop4(),
            new GenericTop5(),
            new MaleTop(),
            new Natural(),
            new Commissioned(),
            new BunnySuit(),
            new Leotard(),
            new Lingerie(),
            new BlindfoldSFW(),
            new BlindfoldNSFW(),
            new Christmas()
        };
        AvoidedMainClothingTypes = 1;
        AvoidedEyeTypes = 0;
        AllowedWaistTypes = new List<MainClothing>()
        {
            new PurpleBot1(),
            new PurpleBot2(),
            new GenericBot1(),
            new GenericBot2(),
            new GenericBot3()
        };
        ExtraMainClothing1Types = new List<MainClothing>() //Gloves
        {
            new PurpleGloves1(),
            new PurpleGloves2(),
            new PurpleGloves3(),
            new GenericGloves1()
        };
        AllowedClothingHatTypes = new List<ClothingAccessory>()
        {};

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.IsEating)
        {
            AddOffset(Eyes, 0, 1 * .625f);
            AddOffset(SecondaryAccessory, 0, 1 * .625f);
        }
        if (actor.Unit.BodySize == 3)
        {
            AddOffset(Dick, 0, -1 * .625f);
            AddOffset(Balls, 0, -1 * .625f);
        }
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
        unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3);
        unit.BodyAccentType4 = State.Rand.Next(BodyAccentTypes4);

    }

    internal override int DickSizes => 8;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        return Sprites[0 + actor.Unit.BodySize];
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (actor.IsEating)
            return Sprites[62 + actor.Unit.BodyAccentType1 * 3];
        else if (actor.IsAttacking)
            return Sprites[61 + actor.Unit.BodyAccentType1 * 3];
        return Sprites[60 + actor.Unit.BodyAccentType1 * 3];
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[84 + actor.Unit.EyeType];

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => Sprites[96 + actor.Unit.EyeType]; //Eyebrows

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites[108 + actor.Unit.SpecialAccessoryType]; // Ears

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return null;
        else if (actor.IsAttacking)
            return Sprites[132];
        return Sprites[124 + actor.Unit.MouthType];
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Mouth details
    {
        if (actor.IsEating)
            return Sprites[138];
        else if (actor.IsAttacking)
            return Sprites[137];
        switch (actor.Unit.MouthType)
        {
            case 0:
                return Sprites[133];
            case 1:
                return Sprites[133];
            case 2:
                return Sprites[134];
            case 3:
                return Sprites[133];
            case 4:
                return Sprites[135];
            case 5:
                return Sprites[133];
            case 6:
                return Sprites[133];
            case 7:
                return Sprites[136];
            default:
                return Sprites[133];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Arms
    {
        if (actor.IsAttacking)
        {
            if (actor.Unit.BodySize < 2)
                return Sprites[6];
            return Sprites[7];
        }
        else
        {
            if (actor.Unit.BodySize < 2)
                return Sprites[4];
            return Sprites[5];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Leg markings
    {
        return Sprites[24 + actor.Unit.BodySize + (4 * actor.Unit.BodyAccentType2)];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Chest fluff
    {
        //if (actor.Unit.HidesFluff == true) //Redundant so unsused
        if (actor.Unit.BodyAccentType3 == 0)
            return null;
        return Sprites[47 + actor.Unit.BodyAccentType3];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Tails
    {
        return Sprites [12 + actor.Unit.BodyAccentType4];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Melee Attack Sprite
    {
        if (actor.IsAttacking)
            return Sprites[171];
        return null;
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Arm fluff
    {
        if (actor.IsAttacking)
        {
            if (actor.Unit.BodySize < 2)
                return Sprites[10];
            return Sprites[11];
        }
        else
        {
            if (actor.Unit.BodySize < 2)
                return Sprites[8];
            return Sprites[9];
        }
    }


    protected override Sprite BodyAccentSprite8(Actor_Unit actor)
    {
        return null;
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
                AddOffset(Belly, 0, -25 * .625f);
                return Sprites5[99];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -25 * .625f);
                return Sprites5[98];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -25 * .625f);
                return Sprites5[97];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -25 * .625f);
                return Sprites5[96];
            }
            switch (size)
            {
                case 25:
                    AddOffset(Belly, 0, -1 * .625f);
                    break;
                case 26:
                    AddOffset(Belly, 0, -3 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -5 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -8 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -11 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -15 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -22 * .625f);
                    break;
            }

            return Sprites5[64 + size];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        return null;
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
                return Sprites5[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return Sprites5[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return Sprites5[29];
            }

            if (leftSize > 28)
                leftSize = 28;

            return Sprites5[0 + leftSize];
        }
        else
        {
            return Sprites5[0 + actor.Unit.BreastSize];
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
                return Sprites5[63];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return Sprites5[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return Sprites5[61];
            }

            if (rightSize > 28)
                rightSize = 28;

            return Sprites5[32 + rightSize];
        }
        else
        {
            return Sprites5[32 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
/*
        if (Dick.GetPalette == null)
        {
            Dick.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, s.Unit.SkinColor);
            Dick.GetColor = null;
        }
*/
        if (actor.IsErect())
        {
            if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
            {
                Dick.layer = 24;
                if (actor.IsCockVoring)
                {
                    return Sprites[155 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[139 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    return Sprites[163 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[147 + actor.Unit.DickSize];
                }
            }
        }

        Dick.layer = 11;
        return Sprites[147 + actor.Unit.DickSize];
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect() && actor.HasBelly && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
        {
            Balls.layer = 19;
        }
        else
        {
            Balls.layer = 10;
        }
        int size = actor.Unit.DickSize;
        int offset = actor.GetBallSize(30, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 30)
        {
            AddOffset(Balls, 0, -29 * .625f);
            return Sprites5[139];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 30)
        {
            AddOffset(Balls, 0, -23 * .625f);
            return Sprites5[138];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 29)
        {
            AddOffset(Balls, 0, -19 * .625f);
            return Sprites5[137];
        }
        else if (offset >= 28)
        {
            AddOffset(Balls, 0, -16 * .625f);
        }
        else if (offset == 27)
        {
            AddOffset(Balls, 0, -11 * .625f);
        }
        else if (offset == 26)
        {
            AddOffset(Balls, 0, -7 * .625f);
        }
        else if (offset == 25)
        {
            AddOffset(Balls, 0, -3 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -1 * .625f);
        }

        if (offset > 0)
            return Sprites5[Math.Min(108 + offset, 136)];
        return Sprites5[100+size];
    }


    class PurpleTop1 : MainClothing
    {
        public PurpleTop1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes1[127];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 4621;
            DiscardUsesAltPalettes = true; //needs to use RenamonSkin rather than clothing Colors!
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            AltPaletteColor = actor.Unit.SkinColor;
            if (Races.Renamon.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[49];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[40];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[41 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[32 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
        }
    }

    class PurpleTop2 : MainClothing
    {
        public PurpleTop2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes2[119];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 4622;
            DiscardUsesAltPalettes = true; //needs to use RenamonSkin rather than clothing Colors!
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            AltPaletteColor = actor.Unit.SkinColor;
            if (Races.Renamon.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[35];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[44];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[27 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[36 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
        }
    }

    class PurpleTop3 : MainClothing
    {
        public PurpleTop3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes3[120];
            femaleOnly = true;
            coversBreasts = false;
        HidesFluff = true; // Should hide chest fluff I think since I did assign it to bodyaccent3 but check just in case
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 4623;
            DiscardUsesAltPalettes = true; //needs to use RenamonSkin rather than clothing Colors!
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            AltPaletteColor = actor.Unit.SkinColor;
            if (Races.Renamon.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[119];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[111 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop1 : MainClothing
    {
        public GenericTop1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes3[49];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 4624;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Renamon.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[48];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[40 + actor.Unit.BreastSize];
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
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes3[59];
            femaleOnly = true;
            coversBreasts = false;
        HidesFluff = true; // Should hide chest fluff I think since I did assign it to bodyaccent3 but check just in case
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 4625;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Renamon.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[58];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[50 + actor.Unit.BreastSize];
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
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes3[69];
            femaleOnly = true;
            coversBreasts = false;
        HidesFluff = true; // Should hide chest fluff I think since I did assign it to bodyaccent3 but check just in case
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 4626;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Renamon.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[68];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[60 + actor.Unit.BreastSize];
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

    class GenericTop4 : MainClothing
    {
        public GenericTop4()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes3[80];
            femaleOnly = true;
            coversBreasts = false;
        HidesFluff = true; // Should hide chest fluff I think since I did assign it to bodyaccent3 but check just in case
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 4627;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Renamon.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[78];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[70 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[79];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop5 : MainClothing
    {
        public GenericTop5()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes3[89];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 4628;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Renamon.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[81 + actor.Unit.BreastSize];
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

    class MaleTop : MainClothing
    {
        public MaleTop()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes3[129];
            maleOnly = true;
            coversBreasts = false;
        HidesFluff = true; // Should hide chest fluff I think since I did assign it to bodyaccent3 but check just in case
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 4629;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.HasBelly)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[125 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[121 + actor.Unit.BodySize];
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
            if (Races.Renamon.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[2 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

        if (actor.Unit.BodySize > 2)
        {
        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[1];
        }
        else
        {
        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[0];
        }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
        }
    }

    class Commissioned : MainClothing
    {
        public Commissioned()
        {
            // Disables gloves from showing since already uses gloves
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes1[128];
            coversBreasts = false;
            HidesFluff = true; // Should hide chest fluff I think since I did assign it to bodyaccent3 but check just in case
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing5 = new SpriteExtraInfo(4, null, WhiteColored);
            Type = 4631;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            actor.Unit.ClothingExtraType1 = 0;
            if (Races.Renamon.oversize)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[80];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[72 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing2.GetSprite = null;
            }

            if (actor.HasBelly)
            {
        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[58 + actor.Unit.BodySize];
        }
        else
        {
        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[54 + actor.Unit.BodySize];
        }

        if (actor.Unit.BodySize > 2)
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[69];
            else if (actor.Unit.DickSize > 5)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[71];
            else
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[70];
        }
        else clothing4.GetSprite = null;
        }
        else
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[66];
            else if (actor.Unit.DickSize > 5)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[68];
            else
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[67];
        }
        else clothing4.GetSprite = null;
        }

        if (actor.IsAttacking)
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[53];
        }
        else
        {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[52];
        }
        }
        else
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[51];
        }
        else
        {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[50];
        }
        }
            clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[62 + actor.Unit.BodySize];

            base.Configure(sprite, actor);
	}
    }

    class BunnySuit : MainClothing
    {
        public BunnySuit()
        {
            // Disables gloves from showing since already uses gloves
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes2[117];
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing5 = new SpriteExtraInfo(4, null, WhiteColored);
            clothing6 = new SpriteExtraInfo(25, null, WhiteColored);
            Type = 4632;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            actor.Unit.ClothingExtraType1 = 0;
            if (Races.Renamon.oversize)
            {
                clothing2.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[118 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing2.GetSprite = null;
            }

            if (actor.HasBelly)
            {
        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[110 + actor.Unit.BodySize];
        }
        else
        {
        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[95 + actor.Unit.BodySize];
        }

        if (actor.Unit.BodySize > 2)
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[107];
            else if (actor.Unit.DickSize > 5)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[109];
            else
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[108];
        }
        else clothing4.GetSprite = null;
        }
        else
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[104];
            else if (actor.Unit.DickSize > 5)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[106];
            else
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[105];
        }
        else clothing4.GetSprite = null;
        }

        if (actor.IsAttacking)
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[103];
        }
        else
        {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[102];
        }
        }
        else
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[101];
        }
        else
        {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[100];
        }
        }
            clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[114 + actor.Unit.BodySize];
            clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[99];

            base.Configure(sprite, actor);
	}
    }

    class Leotard : MainClothing
    {
        public Leotard()
        {
            // Disables gloves from showing since already uses gloves
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes2[121];
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(12, null, null);
            clothing2 = new SpriteExtraInfo(18, null, null);
            clothing3 = new SpriteExtraInfo(7, null, null);
            clothing4 = new SpriteExtraInfo(13, null, null);
            clothing5 = new SpriteExtraInfo(4, null, null);
            clothing6 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing7 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing8 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing9 = new SpriteExtraInfo(4, null, WhiteColored);
            Type = 4633;
            FixedColor = true;
            DiscardUsesAltPalettes = true; //needs to use RenamonSkin rather than clothingColors!
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            actor.Unit.ClothingExtraType1 = 0;
            AltPaletteColor = actor.Unit.SkinColor;
            if (Races.Renamon.oversize)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[99];
                clothing7.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[108];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[91 + actor.Unit.BreastSize];
                clothing7.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[100 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing2.GetSprite = null;
                clothing7.GetSprite = null;
            }

            if (actor.HasBelly)
            {
        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[77 + actor.Unit.BodySize];
        clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[81 + actor.Unit.BodySize];
        }
        else
        {
        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[69 + actor.Unit.BodySize];
        clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[73 + actor.Unit.BodySize];
        }

        if (actor.Unit.BodySize > 2)
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[88];
            else if (actor.Unit.DickSize > 5)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[90];
            else
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[89];
        }
        else clothing4.GetSprite = null;
        }
        else
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[85];
            else if (actor.Unit.DickSize > 5)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[87];
            else
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[86];
        }
        else clothing4.GetSprite = null;
        }

        if (actor.IsAttacking)
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[64];
                    clothing9.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[68];
        }
        else
        {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[63];
                    clothing9.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[67];
        }
        }
        else
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[62];
                    clothing9.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[66];
        }
        else
        {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[61];
                    clothing9.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[65];
        }
        }
            clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[109 + actor.Unit.BodySize];
            clothing8.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[113 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            clothing5.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
	}
    }

    class Lingerie : MainClothing
    {
        public Lingerie()
        {
            // Disables gloves from showing since already uses gloves
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes3[19];
            coversBreasts = false;
            femaleOnly = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(4, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(13, null, WhiteColored);
            Type = 4634;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            actor.Unit.ClothingExtraType1 = 0;
            if (Races.Renamon.oversize)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[18];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[10 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing2.GetSprite = null;
            }

        if (actor.Unit.BodySize > 2)
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[7];
            else if (actor.Unit.DickSize > 5)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[9];
            else
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[8];
        }
        else clothing4.GetSprite = null;
        }
        else
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[4];
            else if (actor.Unit.DickSize > 5)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[6];
            else
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[5];
        }
        else clothing4.GetSprite = null;
        }

        if (actor.IsAttacking)
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[23];
        }
        else
        {
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[22];
        }
        }
        else
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[21];
        }
        else
        {
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[20];
        }
        }
        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[0 + actor.Unit.BodySize];

            base.Configure(sprite, actor);
	}
    }

    class BlindfoldSFW : MainClothing
    {
        public BlindfoldSFW()
        {
            // Disables gloves from showing since already uses gloves
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes3[110];
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(25, null, null);
            clothing2 = new SpriteExtraInfo(25, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(4, null, null);
            clothing4 = new SpriteExtraInfo(4, null, WhiteColored);
            clothing5 = new SpriteExtraInfo(7, null, null);
            clothing6 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing7 = new SpriteExtraInfo(18, null, null);
            clothing8 = new SpriteExtraInfo(7, null, null);
            Type = 4635;
            DiscardUsesAltPalettes = true; //needs to use RenamonSkin rather than clothingColors!
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            actor.Unit.ClothingExtraType1 = 0;
            AltPaletteColor = actor.Unit.SkinColor;
            if (Races.Renamon.oversize)
            {
                clothing7.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing7.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[2 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing7.GetSprite = null;
            }

        if (actor.Unit.BodySize > 2)
        {
        clothing8.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[1];
        }
        else
        {
        clothing8.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[0];
        }

        if (actor.IsAttacking)
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[93];
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[103];
        }
        else
        {
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[92];
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[102];
        }
        }
        else
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[91];
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[101];
        }
        else
        {
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[90];
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[100];
        }
        }

        if (actor.IsOralVoring)
        {
        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[99];
        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[109];
        }
        else
        {
        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[98];
        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[108];
        }
            clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[94 + actor.Unit.BodySize];
            clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[104 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            clothing5.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            clothing7.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            clothing8.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
	}
    }

    class BlindfoldNSFW : MainClothing
    {
        public BlindfoldNSFW()
        {
            // Disables gloves from showing since already uses gloves
            // Programed to behave like the SFW version if "SFW mode" (HideBreasts and/or HideCocks) respectively are on
            
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes3[110];
            if (Config.HideCocks) blocksDick = true;
            else blocksDick = false;
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(25, null, null);
            clothing2 = new SpriteExtraInfo(25, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(4, null, null);
            clothing4 = new SpriteExtraInfo(4, null, WhiteColored);
            clothing5 = new SpriteExtraInfo(7, null, null);
            clothing6 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing7 = new SpriteExtraInfo(18, null, null);
            clothing8 = new SpriteExtraInfo(7, null, null);
            Type = 4635;
            DiscardUsesAltPalettes = true; //needs to use RenamonSkin rather than clothingColors!
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
        actor.Unit.ClothingExtraType1 = 0;
        AltPaletteColor = actor.Unit.SkinColor;
        if (Races.Renamon.oversize)
        {
            clothing7.GetSprite = null;
        }
        else if (actor.Unit.HasBreasts && Config.HideBreasts)
        {
            if (actor.Unit.BodySize > 2)
            {
            clothing8.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[1];
            }
            else
            {
            clothing8.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[0];
            }
            clothing7.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[2 + actor.Unit.BreastSize];
        }
        else
        {
            clothing7.GetSprite = null;
        }
        if (actor.IsAttacking)
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[93];
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[103];
        }
        else
        {
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[92];
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[102];
        }
        }
        else
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[91];
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[101];
        }
        else
        {
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[90];
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[100];
        }
        }

        if (actor.IsOralVoring)
        {
        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[99];
        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[109];
        }
        else
        {
        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[98];
        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[108];
        }
            clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[94 + actor.Unit.BodySize];
            clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[104 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            clothing5.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            clothing7.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            clothing8.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
        }
    }

    class Christmas : MainClothing
    {
        public Christmas()
        {
            // Disables gloves from showing since already uses gloves
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes2[118];
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(25, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing5 = new SpriteExtraInfo(4, null, WhiteColored);
            Type = 4636;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            actor.Unit.ClothingExtraType1 = 0;
            if (Races.Renamon.oversize)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[16];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[8 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing2.GetSprite = null;
            }

        if (actor.Unit.BodySize > 2)
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[24];
            else if (actor.Unit.DickSize > 5)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[26];
            else
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[25];
        }
        else clothing4.GetSprite = null;
        }
        else if (actor.Unit.BodySize > 1)
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[21];
            else if (actor.Unit.DickSize > 5)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[23];
            else
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[22];
        }
        else clothing4.GetSprite = null;
        }
        else
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[18];
            else if (actor.Unit.DickSize > 5)
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[20];
            else
            clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[19];
        }
        else clothing4.GetSprite = null;
        }

        if (actor.IsAttacking)
        {
            if (actor.Unit.BodySize > 1)
            {
                clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[3];
            }
            else
            {
                clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[2];
            }
        }
        else
        {
            if (actor.Unit.BodySize > 1)
            {
                clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[1];
            }
            else
            {
                clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[0];
            }
        }

            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[4 + actor.Unit.BodySize];
            clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[17];

            base.Configure(sprite, actor);
	}
    }

    class PurpleBot1 : MainClothing
    {
        public PurpleBot1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes1[126];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 4641;
            DiscardUsesAltPalettes = true; //needs to use RenamonSkin rather than clothingColors!
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

        AltPaletteColor = actor.Unit.SkinColor;
        if (actor.Unit.BodySize > 2)
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[28];
            else if (actor.Unit.DickSize > 5)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[30];
            else
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[29];
        }
        else clothing1.GetSprite = null;
        }
        else if (actor.Unit.BodySize > 1)
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[25];
            else if (actor.Unit.DickSize > 5)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[27];
            else
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[26];
        }
        else clothing1.GetSprite = null;
        }
        else
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[22];
            else if (actor.Unit.DickSize > 5)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[24];
            else
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[23];
        }
        else clothing1.GetSprite = null;
        }
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[18 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            base.Configure(sprite, actor);
        }
    }

    class PurpleBot2 : MainClothing
    {
        public PurpleBot2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes1[129];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 4642;
            DiscardUsesAltPalettes = true; //needs to use RenamonSkin rather than clothingColors!
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

        AltPaletteColor = actor.Unit.SkinColor;
        if (actor.Unit.BodySize > 2)
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[88];
            else if (actor.Unit.DickSize > 5)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[90];
            else
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[89];
        }
        else clothing1.GetSprite = null;
        }
        else
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[85];
            else if (actor.Unit.DickSize > 5)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[87];
            else
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[86];
        }
        else clothing1.GetSprite = null;
        }
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[81 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot1 : MainClothing
    {
        public GenericBot1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes2[120];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 4643;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
        if (actor.Unit.BodySize > 2)
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[58];
            else if (actor.Unit.DickSize > 5)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[60];
            else
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[59];
        }
        else clothing1.GetSprite = null;
        }
        else if (actor.Unit.BodySize > 1)
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[55];
            else if (actor.Unit.DickSize > 5)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[57];
            else
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[56];
        }
        else clothing1.GetSprite = null;
        }
        else if (actor.Unit.BodySize > 0)
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[52];
            else if (actor.Unit.DickSize > 5)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[54];
            else
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[53];
        }
        else clothing1.GetSprite = null;
        }
        else
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[49];
            else if (actor.Unit.DickSize > 5)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[51];
            else
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[50];
        }
        else clothing1.GetSprite = null;
        }
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[45 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot2 : MainClothing
    {
        public GenericBot2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes3[34];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 4644;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
        if (actor.Unit.BodySize > 2)
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[31];
            else if (actor.Unit.DickSize > 5)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[33];
            else
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[32];
        }
        else clothing1.GetSprite = null;
        }
        else
        {
            if (actor.Unit.DickSize > 0)
        {
            if (actor.Unit.DickSize < 3)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[28];
            else if (actor.Unit.DickSize > 5)
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[30];
            else
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[29];
        }
        else clothing1.GetSprite = null;
        }
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[24 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot3 : MainClothing
    {
        public GenericBot3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes3[39];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, null);
            Type = 4645;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes3[35 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class PurpleGloves1 : MainClothing
    {
        public PurpleGloves1()
        {
        DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes1[31];
        coversBreasts = false;
        blocksDick = false;
            clothing1 = new SpriteExtraInfo(4, null, null);
            clothing2 = new SpriteExtraInfo(4, null, WhiteColored);
            Type = 4651;
            DiscardUsesAltPalettes = true; //needs to use RenamonSkin rather than clothingColors!
	}

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
        AltPaletteColor = actor.Unit.SkinColor;
        if (actor.IsAttacking)
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[17];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[13];
        }
        else
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[16];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[12];
        }
        }
        else
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[15];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[11];
        }
        else
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[14];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[10];
        }
        }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
	}
    }

    class PurpleGloves2 : MainClothing
    {
        public PurpleGloves2()
        {
        DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes1[31];
        coversBreasts = false;
        blocksDick = false;
            clothing1 = new SpriteExtraInfo(4, null, null);
            Type = 4651;
            DiscardUsesAltPalettes = true; //needs to use RenamonSkin rather than clothingColors!
	}

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
        AltPaletteColor = actor.Unit.SkinColor;
        if (actor.IsAttacking)
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[94];
        }
        else
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[93];
        }
        }
        else
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[92];
        }
        else
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[91];
        }
        }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
	}
    }

    class PurpleGloves3 : MainClothing
    {
        public PurpleGloves3()
        {
        DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes1[31];
        coversBreasts = false;
        blocksDick = false;
            clothing1 = new SpriteExtraInfo(4, null, null);
            clothing2 = new SpriteExtraInfo(4, null, WhiteColored);
            Type = 4651;
            DiscardUsesAltPalettes = true; //needs to use RenamonSkin rather than clothingColors!
	}

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
        AltPaletteColor = actor.Unit.SkinColor;
        if (actor.IsAttacking)
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[64];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[68];
        }
        else
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[63];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[67];
        }
        }
        else
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[62];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[66];
        }
        else
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[61];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[65];
        }
        }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RenamonSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
	}
    }

    class GenericGloves1 : MainClothing
    {
        public GenericGloves1()
        {
        DiscardSprite = State.GameManager.SpriteDictionary.RenamonClothes2[126];
        coversBreasts = false;
        blocksDick = false;
            clothing1 = new SpriteExtraInfo(4, null, null);
            clothing2 = new SpriteExtraInfo(4, null, WhiteColored);
            Type = 4652;
            DiscardUsesPalettes = true;
	}

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
        if (actor.IsAttacking)
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[125];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[13];
        }
        else
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[124];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[12];
        }
        }
        else
        {
        if (actor.Unit.BodySize > 1)
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[123];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[11];
        }
        else
        {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes2[122];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.RenamonClothes1[10];
        }
        }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
	}
    }



}
