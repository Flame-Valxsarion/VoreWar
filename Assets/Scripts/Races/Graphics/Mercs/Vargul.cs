using System;
using System.Collections.Generic;
using UnityEngine;

class Vargul : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Vargul1;
    readonly Sprite[] Sprites2 = SpriteDictionary.Vargul2;
    readonly Sprite[] Sprites3 = SpriteDictionary.Vargul3;

    bool oversize = false;


    public Vargul()
    {
        BodySizes = 4;
        EyeTypes = 5;
        SpecialAccessoryCount = 6; // body patterns    
        HairStyles = 0;
        MouthTypes = 0;
        HairColors = 0;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.VargulSkin);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.VargulSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ViperSkin);
        BodyAccentTypes1 = 5; // ears
        BodyAccentTypes2 = 6; // head pattern
        BodyAccentTypes3 = 2; // mask on/off

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(19, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(20, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, s.Unit.SkinColor)); // Ears
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, s.Unit.SkinColor)); // Right Arm
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, s.Unit.SkinColor)); // Tail
        BodyAccent3 = new SpriteExtraInfo(2, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, s.Unit.AccessoryColor)); // Tail Pattern
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, s.Unit.AccessoryColor)); // Body Secondary Pattern
        BodyAccent5 = new SpriteExtraInfo(20, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, s.Unit.AccessoryColor)); // Head Secondary Pattern
        BodyAccent6 = new SpriteExtraInfo(21, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, s.Unit.AccessoryColor)); // Ears Pattern
        BodyAccent7 = new SpriteExtraInfo(2, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, s.Unit.AccessoryColor)); // Right Arm Secondary Pattern
        BodyAccent8 = new SpriteExtraInfo(6, BodyAccentSprite8, WhiteColored); // Claws
        BodyAccent9 = new SpriteExtraInfo(2, BodyAccentSprite9, WhiteColored); // Right Arm Claws
        BodyAccent10 = new SpriteExtraInfo(18, BodyAccentSprite10, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, s.Unit.SkinColor)); // Fur Collar
        Mouth = new SpriteExtraInfo(21, MouthSprite, WhiteColored);
        Hair = null;
        Hair2 = null;
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(21, EyesSprite, WhiteColored);
        SecondaryEyes = new SpriteExtraInfo(21, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.EyeColor));
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(3, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, s.Unit.SkinColor));

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
            new LightArmour(),
            new MediumArmour(),
            new HeavyArmour(),
        };
        AvoidedMainClothingTypes = 0;
        AvoidedEyeTypes = 0;
        AllowedWaistTypes = new List<MainClothing>()
        {
            new GenericBot1(),
            new GenericBot2(),
            new GenericBot3(),
            new ArmourBot1(),
            new ArmourBot2(),
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing50Spaced);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.AccessoryColor = unit.SkinColor;

        if (State.Rand.Next(2) > 0)
        {
            unit.SpecialAccessoryType = State.Rand.Next(SpecialAccessoryCount);
        }
        else
        {
            unit.SpecialAccessoryType = 0;
        }

        if (unit.SpecialAccessoryType == 5)
        {
            unit.BodyAccentType2 = 5;
        }
        else
        {
            unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2 - 1);
        }

        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType3 = 1;
    }

    internal override int DickSizes => 8;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[0 + actor.Unit.BodySize];
        }
        else
        {
            return Sprites[4 + actor.Unit.BodySize];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[22];
        else if (actor.IsAttacking || actor.IsEating)
            return Sprites[21];
        return Sprites[20];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites[50 + actor.Unit.BodyAccentType1]; //ears

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Right Arm
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return Sprites[16 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0)];
            return Sprites[8 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites[12 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0)];
            case 1:
                return Sprites[16 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0)];
            case 2:
                return Sprites[12 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0)];
            case 3:
                return Sprites[16 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0)];
            case 4:
                return Sprites[12 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0)];
            case 5:
                return Sprites[16 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0)];
            case 6:
                return Sprites[12 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0)];
            case 7:
                return Sprites[16 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0)];
            default:
                return Sprites[8 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0)];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites[30]; // Tail

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Tail Pattern
    {
        if (actor.Unit.SpecialAccessoryType == 5)
        {
            return null;
        }
        else
        {
            return Sprites[31];
        }
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Body Secondary Pattern
    {
        if (actor.Unit.SpecialAccessoryType == 5)
        {
            return null;
        }
        else
        {
            if (actor.Unit.HasBreasts)
            {
                return Sprites2[0 + actor.Unit.BodySize + (20 * actor.Unit.SpecialAccessoryType)];
            }
            else
            {
                return Sprites2[4 + actor.Unit.BodySize + (20 * actor.Unit.SpecialAccessoryType)];
            }
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Head Secondary Pattern
    {
        if (actor.Unit.BodyAccentType2 == 5)
            return null;
        else if (actor.IsOralVoring)
            return Sprites[62 + 3 * actor.Unit.BodyAccentType2];
        else if (actor.IsAttacking || actor.IsEating)
            return Sprites[61 + 3 * actor.Unit.BodyAccentType2];
        return Sprites[60 + 3 * actor.Unit.BodyAccentType2];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType2 == 5 || actor.Unit.BodyAccentType2 == 3)
        {
            return null;
        }
        else
        {
            return Sprites[55 + actor.Unit.BodyAccentType1];
        }
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Right Arm Pattern
    {
        if (actor.Unit.SpecialAccessoryType == 5)
        {
            return null;
        }

        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return Sprites2[16 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0) + (20 * actor.Unit.SpecialAccessoryType)];
            return Sprites2[8 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0) + (20 * actor.Unit.SpecialAccessoryType)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites2[12 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0) + (20 * actor.Unit.SpecialAccessoryType)];
            case 1:
                return Sprites2[16 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0) + (20 * actor.Unit.SpecialAccessoryType)];
            case 2:
                return Sprites2[12 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0) + (20 * actor.Unit.SpecialAccessoryType)];
            case 3:
                return Sprites2[16 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0) + (20 * actor.Unit.SpecialAccessoryType)];
            case 4:
                return Sprites2[12 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0) + (20 * actor.Unit.SpecialAccessoryType)];
            case 5:
                return Sprites2[16 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0) + (20 * actor.Unit.SpecialAccessoryType)];
            case 6:
                return Sprites2[12 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0) + (20 * actor.Unit.SpecialAccessoryType)];
            case 7:
                return Sprites2[16 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0) + (20 * actor.Unit.SpecialAccessoryType)];
            default:
                return Sprites2[8 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0) + (20 * actor.Unit.SpecialAccessoryType)];
        }
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) => Sprites[26]; // Claws

    protected override Sprite BodyAccentSprite9(Actor_Unit actor) // Right Arm Claws
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking)
            {
                return Sprites[29];
            }
            else
            {
                return Sprites[27];
            }
        }
        else
        {
            if (actor.IsAttacking)
            {
                return Sprites[29];
            }
            else
            {
                return Sprites[28];
            }
        }
    }

    protected override Sprite BodyAccentSprite10(Actor_Unit actor) // Fur Collar
    {
        if (actor.Unit.ClothingType == 9)
            return null;
        return Sprites[25];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[24];
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[23];
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[40 + actor.Unit.EyeType];

    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => Sprites[45 + actor.Unit.EyeType];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(26, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 26)
            {
                AddOffset(Belly, 0, -22 * .625f);
                return Sprites3[90];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 26)
            {
                AddOffset(Belly, 0, -22 * .625f);
                return Sprites3[89];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 25)
            {
                AddOffset(Belly, 0, -22 * .625f);
                return Sprites3[88];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 24)
            {
                AddOffset(Belly, 0, -22 * .625f);
                return Sprites3[87];
            }
            switch (size)
            {
                case 22:
                    AddOffset(Belly, 0, -4 * .625f);
                    break;
                case 23:
                    AddOffset(Belly, 0, -9 * .625f);
                    break;
                case 24:
                    AddOffset(Belly, 0, -14 * .625f);
                    break;
                case 25:
                    AddOffset(Belly, 0, -19 * .625f);
                    break;
                case 26:
                    AddOffset(Belly, 0, -22 * .625f);
                    break;
            }

            return Sprites3[60 + size];
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
            return Sprites[32 + actor.GetWeaponSprite()];
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
                return Sprites3[29];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return Sprites3[28];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 26)
            {
                return Sprites3[27];
            }

            if (leftSize > 26)
                leftSize = 26;

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
            int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(30 * 30, 1f));
            if (rightSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return Sprites3[59];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return Sprites3[58];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 26)
            {
                return Sprites3[57];
            }

            if (rightSize > 26)
                rightSize = 26;

            return Sprites3[30 + rightSize];
        }
        else
        {
            return Sprites3[30 + actor.Unit.BreastSize];
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
                    return Sprites[83 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[75 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    return Sprites[99 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[91 + actor.Unit.DickSize];
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
            AddOffset(Balls, 0, -24 * .625f);
            return Sprites3[127];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -19 * .625f);
            return Sprites3[126];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 26)
        {
            AddOffset(Balls, 0, -15 * .625f);
            return Sprites3[125];
        }
        else if (offset >= 25)
        {
            AddOffset(Balls, 0, -10 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -5 * .625f);
        }

        if (offset > 0)
            return Sprites3[Math.Min(99 + offset, 124)];
        return Sprites3[91 + size];
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
            if (Races.Vargul.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[61];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[53 + actor.Unit.BreastSize];
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
            if (Races.Vargul.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[70];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[62 + actor.Unit.BreastSize];
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
            if (Races.Vargul.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[79];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[71 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Vargul4[80];
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
            if (Races.Vargul.oversize)
            {
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul4[89];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[90 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul4[81 + actor.Unit.BreastSize];
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
            if (Races.Vargul.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[106];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[98 + actor.Unit.BreastSize];
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
            if (Races.Vargul.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[2 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            if (actor.Unit.BodySize < 3)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul4[0];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul4[1];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, actor.Unit.SkinColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.VargulSkin, actor.Unit.SkinColor);

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
            if (Races.Vargul.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[52];
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul4[36 + actor.Unit.BodySize];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[44 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul4[36 + actor.Unit.BodySize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul4[40 + actor.Unit.BodySize];
            }

            base.Configure(sprite, actor);
        }
    }

    class LightArmour : MainClothing
    {
        public LightArmour()
        {
            DiscardSprite = SpriteDictionary.Vargul5[38];
            coversBreasts = false;
            Type = 61802;
            FixedColor = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(0, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(3, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(22, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(7, null, null);
            clothing5 = new SpriteExtraInfo(7, null, null);
            clothing6 = new SpriteExtraInfo(12, null, null);
            clothing7 = new SpriteExtraInfo(19, null, WhiteColored);
            clothing8 = new SpriteExtraInfo(18, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType3 == 0)
            {
                clothing3.GetSprite = (s) => SpriteDictionary.Vargul5[2];
            }
            else
            {
                if (actor.IsOralVoring)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.Vargul5[5];
                }
                else if (actor.IsAttacking || actor.IsEating)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.Vargul5[4];
                }
                else
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.Vargul5[3];
                }
            }

            if (Races.Vargul.oversize)
            {
                if (actor.Unit.BodySize < 2)
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[6];
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[10];
                }
                else if (actor.Unit.BodySize > 2)
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[7];
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[11];
                }
                else
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[7];
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[10];
                }
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[1];
                clothing6.GetSprite = (s) => SpriteDictionary.Vargul5[14 + actor.Unit.BodySize];
                clothing7.GetSprite = null;
                clothing8.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize < 2)
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[6];
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[10];
                }
                else if (actor.Unit.BodySize > 2)
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[7];
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[11];
                }
                else
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[7];
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[10];
                }
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[1];
                clothing6.GetSprite = (s) => SpriteDictionary.Vargul5[14 + actor.Unit.BodySize];
                clothing7.GetSprite = (s) => SpriteDictionary.Vargul5[22 + actor.Unit.BreastSize];
                clothing8.GetSprite = (s) => SpriteDictionary.Vargul5[30 + actor.Unit.BodySize];
            }
            else
            {
                if (actor.Unit.BodySize < 2)
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[8];
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[12];
                }
                else
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[9];
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[13];
                }
                breastSprite = null;
                clothing2.GetSprite = null;
                clothing6.GetSprite = (s) => SpriteDictionary.Vargul5[18 + actor.Unit.BodySize];
                clothing7.GetSprite = null;
                clothing8.GetSprite = (s) => SpriteDictionary.Vargul5[34 + actor.Unit.BodySize];
            }

            clothing1.GetSprite = (s) => SpriteDictionary.Vargul5[0];

            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);
            clothing5.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);
            clothing6.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);
            clothing8.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);

            base.Configure(sprite, actor);
        }
    }

    class MediumArmour : MainClothing
    {
        public MediumArmour()
        {
            DiscardSprite = SpriteDictionary.Vargul5[39];
            coversBreasts = false;
            Type = 61803;
            FixedColor = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(0, null, null);
            clothing2 = new SpriteExtraInfo(22, null, null);
            clothing3 = new SpriteExtraInfo(7, null, null);
            clothing4 = new SpriteExtraInfo(7, null, null);
            clothing5 = new SpriteExtraInfo(11, null, null);
            clothing6 = new SpriteExtraInfo(15, null, null);
            clothing7 = new SpriteExtraInfo(19, null, WhiteColored);
            clothing8 = new SpriteExtraInfo(12, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.GetStomachSize(26, 0.7f) > 6)
            {
                if (actor.Unit.HasBreasts)
                {
                    clothing6.GetSprite = (s) => SpriteDictionary.Vargul5[82];
                }
                else
                {
                    clothing6.GetSprite = (s) => SpriteDictionary.Vargul5[83];
                }
                clothing6.layer = 13;
            }
            else if (actor.GetStomachSize(26, 0.7f) > 0)
            {
                if (actor.Unit.HasBreasts)
                {
                    clothing6.GetSprite = (s) => SpriteDictionary.Vargul5[74 + actor.Unit.BodySize];
                }
                else
                {
                    clothing6.GetSprite = (s) => SpriteDictionary.Vargul5[78 + actor.Unit.BodySize];
                }
                clothing6.layer = 15;
            }
            else
            {
                if (actor.Unit.HasBreasts)
                {
                    clothing6.GetSprite = (s) => SpriteDictionary.Vargul5[66 + actor.Unit.BodySize];
                }
                else
                {
                    clothing6.GetSprite = (s) => SpriteDictionary.Vargul5[70 + actor.Unit.BodySize];
                }
                clothing6.layer = 15;
            }

            if (Races.Vargul.oversize)
            {
                if (actor.Unit.BodySize < 2)
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[54];
                }
                else if (actor.Unit.BodySize > 2)
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[55];
                }
                else
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[54];
                }

                if (actor.HasBelly)
                {
                    clothing7.GetSprite = (s) => SpriteDictionary.Vargul5[107];
                    clothing8.GetSprite = (s) => SpriteDictionary.Vargul5[88 + actor.Unit.BodySize];
                }
                else
                {
                    clothing7.GetSprite = (s) => SpriteDictionary.Vargul5[107];
                    clothing8.GetSprite = (s) => SpriteDictionary.Vargul5[84 + actor.Unit.BodySize];
                }

                if (actor.Unit.BodyAccentType3 == 0)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[42];
                }
                else
                {
                    if (actor.IsOralVoring)
                    {
                        clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[45];
                    }
                    else if (actor.IsAttacking || actor.IsEating)
                    {
                        clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[44];
                    }
                    else
                    {
                        clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[43];
                    }
                }

                clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[56 + actor.Unit.BodySize];
            }
            else if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize < 2)
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[54];
                }
                else if (actor.Unit.BodySize > 2)
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[55];
                }
                else
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[54];
                }

                if (actor.HasBelly)
                {
                    clothing7.GetSprite = (s) => SpriteDictionary.Vargul5[100 + actor.Unit.BreastSize];
                    clothing8.GetSprite = (s) => SpriteDictionary.Vargul5[88 + actor.Unit.BodySize];
                }
                else
                {
                    clothing7.GetSprite = (s) => SpriteDictionary.Vargul5[92 + actor.Unit.BreastSize];
                    clothing8.GetSprite = (s) => SpriteDictionary.Vargul5[84 + actor.Unit.BodySize];
                }

                if (actor.Unit.BodyAccentType3 == 0)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[42];
                }
                else
                {
                    if (actor.IsOralVoring)
                    {
                        clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[45];
                    }
                    else if (actor.IsAttacking || actor.IsEating)
                    {
                        clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[44];
                    }
                    else
                    {
                        clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[43];
                    }
                }

                clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[56 + actor.Unit.BodySize];
            }
            else
            {
                if (actor.Unit.BodySize < 2)
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[54];
                }
                else
                {
                    clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[55];
                }

                if (actor.HasBelly)
                {
                    clothing7.GetSprite = null;
                    clothing8.GetSprite = (s) => SpriteDictionary.Vargul5[112 + actor.Unit.BodySize];
                }
                else
                {
                    clothing7.GetSprite = null;
                    clothing8.GetSprite = (s) => SpriteDictionary.Vargul5[108 + actor.Unit.BodySize];
                }

                if (actor.Unit.BodyAccentType3 == 0)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[151];
                }
                else
                {
                    if (actor.IsOralVoring)
                    {
                        clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[154];
                    }
                    else if (actor.IsAttacking || actor.IsEating)
                    {
                        clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[153];
                    }
                    else
                    {
                        clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[152];
                    }
                }

                breastSprite = null;
                clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[60 + actor.Unit.BodySize];
            }

            clothing1.GetSprite = (s) => SpriteDictionary.Vargul5[40];
            clothing3.GetSprite = (s) => SpriteDictionary.Vargul5[46 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0) + (actor.IsAttacking ? 4 : 0)];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);
            clothing5.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);
            clothing6.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);
            clothing8.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);

            base.Configure(sprite, actor);
        }
    }

    class HeavyArmour : MainClothing
    {
        public HeavyArmour()
        {
            DiscardSprite = SpriteDictionary.Vargul5[159];
            coversBreasts = false;
            Type = 61804;
            FixedColor = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(0, null, null);
            clothing2 = new SpriteExtraInfo(22, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(22, null, null);
            clothing4 = new SpriteExtraInfo(7, null, null);
            clothing5 = new SpriteExtraInfo(13, null, null);
            clothing6 = new SpriteExtraInfo(12, null, null);
            clothing7 = new SpriteExtraInfo(19, null, null);
            clothing8 = new SpriteExtraInfo(20, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.BodyAccentType3 == 0)
            {
                clothing3.GetSprite = null;
            }
            else
            {
                if (actor.IsOralVoring)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.Vargul5[122];
                }
                else if (actor.IsAttacking || actor.IsEating)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.Vargul5[121];
                }
                else
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.Vargul5[120];
                }
            }

            if (Races.Vargul.oversize)
            {
                if (actor.Unit.BodySize < 2)
                {
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[123];
                }
                else if (actor.Unit.BodySize > 2)
                {
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[124];
                }
                else
                {
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[123];
                }

                if (actor.HasBelly || (actor.GetBallSize(27, .8f) > 0) || actor.HasPreyInBreasts)
                {
                    clothing8.GetSprite = null;
                }
                else
                {
                    clothing8.GetSprite = (s) => SpriteDictionary.Vargul5[127 + actor.Unit.BodySize];
                }

                clothing7.layer = 21;
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul5[117];
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[116];
                clothing6.GetSprite = (s) => SpriteDictionary.Vargul5[135 + actor.Unit.BodySize];
                clothing7.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize < 2)
                {
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[123];
                }
                else if (actor.Unit.BodySize > 2)
                {
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[124];
                }
                else
                {
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[123];
                }

                if (actor.HasBelly || (actor.GetBallSize(27, .8f) > 0) || actor.HasPreyInBreasts)
                {
                    clothing8.GetSprite = null;
                }
                else
                {
                    clothing8.GetSprite = (s) => SpriteDictionary.Vargul5[127 + actor.Unit.BodySize];
                }

                clothing7.layer = 21;
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul5[117];
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[116];
                clothing6.GetSprite = (s) => SpriteDictionary.Vargul5[135 + actor.Unit.BodySize];
                clothing7.GetSprite = (s) => SpriteDictionary.Vargul5[143 + actor.Unit.BreastSize];
            }
            else
            {
                if (actor.Unit.BodySize < 2)
                {
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[125];
                }
                else
                {
                    clothing5.GetSprite = (s) => SpriteDictionary.Vargul5[126];
                }

                if (actor.HasBelly || (actor.GetBallSize(27, .8f) > 0) || actor.HasPreyInBreasts)
                {
                    clothing8.GetSprite = null;
                }
                else
                {
                    clothing8.GetSprite = (s) => SpriteDictionary.Vargul5[131 + actor.Unit.BodySize];
                }

                clothing7.layer = 19;
                breastSprite = null;
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul5[119];
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[118];
                clothing6.GetSprite = (s) => SpriteDictionary.Vargul5[139 + actor.Unit.BodySize];
                clothing7.GetSprite = (s) => SpriteDictionary.Vargul5[155 + actor.Unit.BodySize];
            }

            clothing4.GetSprite = (s) => SpriteDictionary.Vargul5[46 + (actor.Unit.BodySize > 1 ? 1 : 0) + (!actor.Unit.HasBreasts ? 2 : 0) + (actor.IsAttacking ? 4 : 0)];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);
            clothing5.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);
            clothing6.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);
            clothing7.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);
            clothing8.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);

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

            if (actor.Unit.BodySize < 3)
            {
                if (actor.Unit.DickSize > 0)
                {
                    if (actor.Unit.DickSize < 3)
                        clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[19];
                    else if (actor.Unit.DickSize > 5)
                        clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[21];
                    else
                        clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[20];
                }
                else clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[18];
            }
            else
            {
                if (actor.Unit.DickSize > 0)
                {
                    if (actor.Unit.DickSize < 3)
                        clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[23];
                    else if (actor.Unit.DickSize > 5)
                        clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[25];
                    else
                        clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[24];
                }
                else clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[22];
            }

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul4[10 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul4[14 + actor.Unit.BodySize];
            }

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

            if (actor.Unit.BodySize < 3)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[26];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[27];
            }

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul4[10 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul4[14 + actor.Unit.BodySize];
            }

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

            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[28 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Vargul4[32 + actor.Unit.BodySize];
            }

            base.Configure(sprite, actor);
        }
    }

    class ArmourBot1 : MainClothing
    {
        public ArmourBot1()
        {
            DiscardSprite = SpriteDictionary.Vargul5[41];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 61801;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.BodySize < 3)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Vargul5[64];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Vargul5[65];
                }
            }
            else
            {
                clothing1.GetSprite = null;
            }

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[56 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[60 + actor.Unit.BodySize];
            }

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);

            base.Configure(sprite, actor);
        }
    }

    class ArmourBot2 : MainClothing
    {
        public ArmourBot2()
        {
            DiscardSprite = SpriteDictionary.Vargul5[41];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 61801;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.BodySize < 3)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Vargul5[64];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Vargul5[65];
                }
            }
            else
            {
                clothing1.GetSprite = null;
            }

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[135 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Vargul5[139 + actor.Unit.BodySize];
            }

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ExtraColor1);

            base.Configure(sprite, actor);
        }
    }















}
