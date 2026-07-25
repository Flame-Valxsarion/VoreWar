using System;
using System.Collections.Generic;
using UnityEngine;

class Demifrogs : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Demifrogs1;
    readonly Sprite[] Sprites2 = SpriteDictionary.Demifrogs2;
    readonly Sprite[] Sprites3 = SpriteDictionary.Demifrogs3;
    readonly Sprite[] Sprites3A = SpriteDictionary.Demifrogs3alt;

    readonly DemifrogLeader LeaderClothes;
    readonly DemifrogRags Rags;

    bool oversize = false;


    public Demifrogs()
    {
        BodySizes = 4;
        EyeTypes = 0;
        SpecialAccessoryCount = 8; // primary pattern types      
        HairStyles = 0;
        MouthTypes = 0;
        HairColors = 0;
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemifrogSkin);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemifrogSkin);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemifrogSkin); // Secondary pattern Colors
        BodyAccentTypes1 = 13; // secondary pattern types
        BodyAccentTypes2 = 2; // colored genitals/tits switch

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(1, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, s.Unit.SkinColor)); //Skin
        Head = new SpriteExtraInfo(5, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, s.Unit.SkinColor)); //Skin
        BodyAccessory = new SpriteExtraInfo(2, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, s.Unit.SkinColor)); // Primary Pattern (body)
        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, s.Unit.SkinColor)); // Primary Pattern (head)
        BodyAccent2 = new SpriteExtraInfo(3, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, s.Unit.AccessoryColor)); // Secondary Pattern (body)
        BodyAccent3 = new SpriteExtraInfo(7, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, s.Unit.AccessoryColor)); // Secondary Pattern (head)
        BodyAccent4 = new SpriteExtraInfo(2, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, s.Unit.SkinColor)); // Tertiary Pattern (genitals)
        BodyAccent5 = new SpriteExtraInfo(18, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, s.Unit.SkinColor)); // Tertiary Pattern (breasts)
        Mouth = new SpriteExtraInfo(21, MouthSprite, WhiteColored);
        Hair = null;
        Hair2 = null;
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(8, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(5, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, s.Unit.SkinColor));

        LeaderClothes = new DemifrogLeader();
        Rags = new DemifrogRags();

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new GenericTop1(),
            new GenericTop2(),
            new GenericTop3(),
            new GenericTop4(),
            new GenericTop5(),
            new GenericTop6(),
            new MaleTop(),
            new MaleTop2(),
            new Natural(),
            new Tribal(),
            Rags,
            LeaderClothes
        };
        AvoidedMainClothingTypes = 2;
        AvoidedEyeTypes = 0;
        AllowedWaistTypes = new List<MainClothing>()
        {
            new GenericBot1(),
            new GenericBot2(),
            new GenericBot3(),
            new GenericBot4(),
            new TribalBot(),
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.AccessoryColor = unit.SkinColor;

        if (State.Rand.Next(10) == 0)
        {
            unit.SpecialAccessoryType = (SpecialAccessoryCount - 1);
        }
        else
        {
            unit.SpecialAccessoryType = State.Rand.Next(SpecialAccessoryCount - 1);
        }

        if (State.Rand.Next(3) == 0)
        {
            unit.BodyAccentType1 = (BodyAccentTypes1 - 1);
        }
        else
        {
            unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1 - 1);
        }

        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);

        if (unit.Type == UnitType.Leader)
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(LeaderClothes);

        if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(Rags);
            if (unit.ClothingType == 0) //Covers rags not in the list
                unit.ClothingType = AllowedMainClothingTypes.Count;
        }
    }

    internal override int DickSizes => 8;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        return Sprites[0 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodySize)];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites[11];
        else if (actor.IsAttacking)
            return Sprites[10];
        return Sprites[9];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType >= 7)
        {
            if (actor.Unit.SpecialAccessoryType > 7)
                actor.Unit.SpecialAccessoryType = 7;
            return null;
        }


        return Sprites[36 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodySize) + (8 * actor.Unit.SpecialAccessoryType)];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 7)
            return null;
        return Sprites[15 + (actor.IsEating ? 2 : (actor.IsAttacking ? 1 : 0)) + (3 * actor.Unit.SpecialAccessoryType)];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType1 == 12)
            return null;
        return Sprites2[0 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodySize) + (8 * actor.Unit.BodyAccentType1)];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType1 == 12)
            return null;
        return Sprites2[96 + (actor.IsEating ? 2 : (actor.IsAttacking ? 1 : 0)) + (3 * actor.Unit.BodyAccentType1)];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 6 || actor.Unit.HasDick == true || actor.Unit.BodyAccentType2 == 1)
            return null;
        else if (actor.Unit.BodySize > 2)
            return Sprites[139];
        return Sprites[138];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 6 || actor.Unit.HasBreasts == false || actor.PredatorComponent?.LeftBreastFullness > 0 || actor.PredatorComponent?.RightBreastFullness > 0 || actor.Unit.BodyAccentType2 == 1)
            return null;
        return Sprites2[132 + actor.Unit.BreastSize];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[14];
        else if (actor.IsEating)
            return Sprites[13];
        else if (actor.IsAttacking)
            return Sprites[12];
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[8];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(28, 0.6f);

            if (actor.Unit.SpecialAccessoryType == 6)
            {

                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                {
                    AddOffset(Belly, 0, -26 * .625f);
                    return Sprites3A[94];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                {
                    AddOffset(Belly, 0, -26 * .625f);
                    return Sprites3A[93];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 27)
                {
                    AddOffset(Belly, 0, -26 * .625f);
                    return Sprites3A[92];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                {
                    AddOffset(Belly, 0, -26 * .625f);
                    return Sprites3A[91];
                }
                switch (size)
                {
                    case 23:
                        AddOffset(Belly, 0, -5 * .625f);
                        break;
                    case 24:
                        AddOffset(Belly, 0, -7 * .625f);
                        break;
                    case 25:
                        AddOffset(Belly, 0, -12 * .625f);
                        break;
                    case 26:
                        AddOffset(Belly, 0, -16 * .625f);
                        break;
                    case 27:
                        AddOffset(Belly, 0, -20 * .625f);
                        break;
                    case 28:
                        AddOffset(Belly, 0, -25 * .625f);
                        break;
                }

                return Sprites3A[62 + size];
            }
            else
            {
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                {
                    AddOffset(Belly, 0, -26 * .625f);
                    return Sprites3[94];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                {
                    AddOffset(Belly, 0, -26 * .625f);
                    return Sprites3[93];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 27)
                {
                    AddOffset(Belly, 0, -26 * .625f);
                    return Sprites3[92];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                {
                    AddOffset(Belly, 0, -26 * .625f);
                    return Sprites3[91];
                }
                switch (size)
                {
                    case 23:
                        AddOffset(Belly, 0, -5 * .625f);
                        break;
                    case 24:
                        AddOffset(Belly, 0, -7 * .625f);
                        break;
                    case 25:
                        AddOffset(Belly, 0, -12 * .625f);
                        break;
                    case 26:
                        AddOffset(Belly, 0, -16 * .625f);
                        break;
                    case 27:
                        AddOffset(Belly, 0, -20 * .625f);
                        break;
                    case 28:
                        AddOffset(Belly, 0, -25 * .625f);
                        break;
                }

                return Sprites3[62 + size];
            }
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
                    if (actor.Unit.BodySize == 3)
                        return Sprites[125];
                    return Sprites[124];
                case 1:
                    return Sprites[126];
                case 2:
                    if (actor.Unit.BodySize == 3)
                        return Sprites[128];
                    return Sprites[127];
                case 3:
                    return Sprites[129];
                case 4:
                    if (actor.Unit.BodySize == 3)
                        return Sprites[131];
                    return Sprites[130];
                case 5:
                    return Sprites[132];
                case 6:
                    return Sprites[133 + actor.Unit.BodySize];
                case 7:
                    return Sprites[137];
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

        if (actor.Unit.SpecialAccessoryType == 6)
        {
            if (actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(31 * 31, 1f));
                if (leftSize > actor.Unit.DefaultBreastSize)
                    oversize = true;
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 31)
                {
                    return Sprites3A[30];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 29)
                {
                    return Sprites3A[29];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 27)
                {
                    return Sprites3A[28];
                }

                if (leftSize > 27)
                    leftSize = 27;

                return Sprites3A[0 + leftSize];
            }
            else
            {
                return Sprites3A[0 + actor.Unit.BreastSize];
            }
        }
        else
        {
            if (actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(31 * 31, 1f));
                if (leftSize > actor.Unit.DefaultBreastSize)
                    oversize = true;
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 31)
                {
                    return Sprites3[30];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 29)
                {
                    return Sprites3[29];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 27)
                {
                    return Sprites3[28];
                }

                if (leftSize > 27)
                    leftSize = 27;

                return Sprites3[0 + leftSize];
            }
            else
            {
                return Sprites3[0 + actor.Unit.BreastSize];
            }
        }
    }

    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;

        if (actor.Unit.SpecialAccessoryType == 6)
        {

            if (actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(31 * 31, 1f));
                if (rightSize > actor.Unit.DefaultBreastSize)
                    oversize = true;
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 31)
                {
                    return Sprites3A[61];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 29)
                {
                    return Sprites3A[60];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 27)
                {
                    return Sprites3A[59];
                }

                if (rightSize > 27)
                    rightSize = 27;

                return Sprites3A[31 + rightSize];
            }
            else
            {
                return Sprites3A[31 + actor.Unit.BreastSize];
            }
        }
        else
        {
            if (actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(31 * 31, 1f));
                if (rightSize > actor.Unit.DefaultBreastSize)
                    oversize = true;
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 31)
                {
                    return Sprites3[61];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 29)
                {
                    return Sprites3[60];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 27)
                {
                    return Sprites3[59];
                }

                if (rightSize > 27)
                    rightSize = 27;

                return Sprites3[31 + rightSize];
            }
            else
            {
                return Sprites3[31 + actor.Unit.BreastSize];
            }

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
                    return Sprites[108 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[92 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    return Sprites[116 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[100 + actor.Unit.DickSize];
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


        if (actor.Unit.SpecialAccessoryType == 6)
        {

            if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
            {
                AddOffset(Balls, 0, -22 * .625f);
                return Sprites3A[132];
            }
            else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                AddOffset(Balls, 0, -20 * .625f);
                return Sprites3A[131];
            }
            else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                AddOffset(Balls, 0, -18 * .625f);
                return Sprites3A[130];
            }
            else if (offset >= 26)
            {
                AddOffset(Balls, 0, -16 * .625f);
            }
            else if (offset == 25)
            {
                AddOffset(Balls, 0, -12 * .625f);
            }
            else if (offset == 24)
            {
                AddOffset(Balls, 0, -10 * .625f);
            }
            else if (offset == 23)
            {
                AddOffset(Balls, 0, -9 * .625f);
            }
            else if (offset == 22)
            {
                AddOffset(Balls, 0, -6 * .625f);
            }
            else if (offset == 21)
            {
                AddOffset(Balls, 0, -5 * .625f);
            }
            else if (offset == 20)
            {
                AddOffset(Balls, 0, -3 * .625f);
            }

            if (offset > 0)
                return Sprites3A[Math.Min(103 + offset, 129)];
            return Sprites3A[95 + size];
        }
        else
        {

            if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
            {
                AddOffset(Balls, 0, -22 * .625f);
                return Sprites3[132];
            }
            else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                AddOffset(Balls, 0, -20 * .625f);
                return Sprites3[131];
            }
            else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                AddOffset(Balls, 0, -18 * .625f);
                return Sprites3[130];
            }
            else if (offset >= 26)
            {
                AddOffset(Balls, 0, -16 * .625f);
            }
            else if (offset == 25)
            {
                AddOffset(Balls, 0, -12 * .625f);
            }
            else if (offset == 24)
            {
                AddOffset(Balls, 0, -10 * .625f);
            }
            else if (offset == 23)
            {
                AddOffset(Balls, 0, -9 * .625f);
            }
            else if (offset == 22)
            {
                AddOffset(Balls, 0, -6 * .625f);
            }
            else if (offset == 21)
            {
                AddOffset(Balls, 0, -5 * .625f);
            }
            else if (offset == 20)
            {
                AddOffset(Balls, 0, -3 * .625f);
            }

            if (offset > 0)
                return Sprites3[Math.Min(103 + offset, 129)];
            return Sprites3[95 + size];
        }
    }

    class GenericTop1 : MainClothing
    {
        public GenericTop1()
        {
            DiscardSprite = SpriteDictionary.Avians4[24];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(19, null, null);
            Type = 1524;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demifrogs.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[56];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[48 + actor.Unit.BreastSize];
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
            DiscardSprite = SpriteDictionary.Avians4[34];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(19, null, null);
            Type = 1534;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demifrogs.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[65];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[57 + actor.Unit.BreastSize];
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
            DiscardSprite = SpriteDictionary.Avians4[44];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(19, null, null);
            Type = 1544;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demifrogs.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[74];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[66 + actor.Unit.BreastSize];
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
            DiscardSprite = SpriteDictionary.Avians4[55];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(19, null, null);
            clothing2 = new SpriteExtraInfo(19, null, WhiteColored);
            Type = 1555;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demifrogs.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[83];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[75 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Demifrogs4[84];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop5 : MainClothing
    {
        public GenericTop5()
        {
            DiscardSprite = SpriteDictionary.Avians4[74];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(19, null, null);
            clothing2 = new SpriteExtraInfo(19, null, WhiteColored);
            Type = 1574;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demifrogs.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[93];
                clothing2.GetSprite = (s) => SpriteDictionary.Demifrogs4[102];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[85 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Demifrogs4[94 + actor.Unit.BreastSize];
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

    class GenericTop6 : MainClothing
    {
        public GenericTop6()
        {
            DiscardSprite = SpriteDictionary.Avians4[88];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(19, null, null);
            Type = 1588;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demifrogs.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[107 + actor.Unit.BreastSize];
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
            DiscardSprite = SpriteDictionary.Avians4[79];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(19, null, null);
            Type = 1579;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.HasBelly)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[119 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[115 + actor.Unit.BodySize];
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class MaleTop2 : MainClothing
    {
        public MaleTop2()
        {
            DiscardSprite = SpriteDictionary.Avians4[79];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(19, null, null);
            Type = 1579;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[103 + actor.Unit.BodySize];
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
            clothing1 = new SpriteExtraInfo(19, null, null);
            clothing2 = new SpriteExtraInfo(7, null, null);
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demifrogs.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.SpecialAccessoryType == 6)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[12 + actor.Unit.BreastSize];
                }
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[2 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            if (actor.Unit.BodySize > 2)
            {
                if (actor.Unit.SpecialAccessoryType == 6)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.Demifrogs4[11];
                }
                else
                    clothing2.GetSprite = (s) => SpriteDictionary.Demifrogs4[1];
            }
            else
            {
                if (actor.Unit.SpecialAccessoryType == 6)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.Demifrogs4[10];
                }
                else
                    clothing2.GetSprite = (s) => SpriteDictionary.Demifrogs4[0];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, actor.Unit.SkinColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
        }
    }

    class Tribal : MainClothing
    {
        public Tribal()
        {
            DiscardSprite = SpriteDictionary.Demifrogs4[143];
            coversBreasts = false;
            Type = 1176;
            OccupiesAllSlots = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(19, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demifrogs.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[138];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[131 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Demifrogs4[139 + actor.Unit.BodySize];

            base.Configure(sprite, actor);
        }
    }

    class DemifrogRags : MainClothing
    {
        public DemifrogRags()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Rags[23];
            blocksDick = false;
            inFrontOfDick = 2;
            coversBreasts = false;
            Type = 207;
            OccupiesAllSlots = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(19, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BreastSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[127];
                else if (actor.Unit.BreastSize < 6)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[128];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[129];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[130];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Demifrogs4[123 + actor.Unit.BodySize];

            base.Configure(sprite, actor);
        }
    }

    class DemifrogLeader : MainClothing
    {
        public DemifrogLeader()
        {
            leaderOnly = true;
            DiscardSprite = SpriteDictionary.Demifrogs5[20];
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(20, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(19, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(4, null, WhiteColored);
            clothing5 = new SpriteExtraInfo(0, null, WhiteColored);
            clothing6 = new SpriteExtraInfo(9, null, WhiteColored);
            Type = 1177;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs5[0 + actor.Unit.BodySize];
            clothing2.GetSprite = (s) => SpriteDictionary.Demifrogs5[4];
            clothing4.GetSprite = (s) => SpriteDictionary.Demifrogs5[10 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodySize)];
            clothing5.GetSprite = (s) => SpriteDictionary.Demifrogs5[18];
            clothing6.GetSprite = (s) => SpriteDictionary.Demifrogs5[19];

            if (actor.Unit.HasBreasts)
            {
                if (Races.Demifrogs.oversize)
                {
                    clothing3.GetSprite = null;
                }
                else if (actor.Unit.BreastSize < 3)
                {
                    clothing3.GetSprite = null;
                }
                else
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.Demifrogs5[2 + actor.Unit.BreastSize];
                }
            }
            else
            {
                clothing3.GetSprite = null;
            }

            base.Configure(sprite, actor);
        }
    }

    class GenericBot1 : MainClothing
    {
        public GenericBot1()
        {
            DiscardSprite = SpriteDictionary.Avians3[121];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 1521;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[24];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[26];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[25];
            }
            else clothing1.GetSprite = null;

            clothing2.GetSprite = (s) => SpriteDictionary.Demifrogs4[20 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot2 : MainClothing
    {
        public GenericBot2()
        {
            DiscardSprite = SpriteDictionary.Avians3[137];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 1537;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[32];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[34];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[33];
            }
            else clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[31];

            clothing2.GetSprite = (s) => SpriteDictionary.Demifrogs4[27 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot3 : MainClothing
    {
        public GenericBot3()
        {
            DiscardSprite = SpriteDictionary.Avians3[140];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 1540;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[35];
            clothing2.GetSprite = (s) => SpriteDictionary.Demifrogs4[27 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot4 : MainClothing
    {
        public GenericBot4()
        {
            DiscardSprite = SpriteDictionary.Avians4[14];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 1514;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[45];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[47];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[46];
            }
            else clothing1.GetSprite = null;

            clothing2.GetSprite = (s) => SpriteDictionary.Demifrogs4[41 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class TribalBot : MainClothing
    {
        public TribalBot()
        {
            DiscardSprite = SpriteDictionary.Demifrogs4[40];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 1178;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            clothing1.GetSprite = (s) => SpriteDictionary.Demifrogs4[36 + actor.Unit.BodySize];

            base.Configure(sprite, actor);
        }
    }
























}
