using System;
using System.Collections.Generic;
using UnityEngine;

class AntQueen : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.AntQueen1;
    readonly Sprite[] Sprites2 = SpriteDictionary.AntQueen2;

    readonly AntLeader LeaderClothes;

    bool oversize = false;

    public AntQueen()
    {
        CanBeGender = new List<Gender>() { Gender.Female, Gender.Hermaphrodite };
        BodySizes = 3;
        EyeTypes = 8;
        SpecialAccessoryCount = 12; // antennae        
        HairStyles = 14;
        MouthTypes = 3;
        EyeColors = 0;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemiantSkin);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemiantSkin);

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(6, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.AccessoryColor)); // Lower Body (black)
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.SkinColor)); // Upper Body (White)
        BodyAccessory = new SpriteExtraInfo(2, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.AccessoryColor)); // Abdomen (black)
        BodyAccent = new SpriteExtraInfo(2, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.SkinColor)); // Abdomen 2 (White)
        BodyAccent2 = new SpriteExtraInfo(20, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.AccessoryColor)); // Antennae (black)
        BodyAccent3 = new SpriteExtraInfo(4, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.AccessoryColor)); // Upper Front Arms (black)
        BodyAccent4 = new SpriteExtraInfo(3, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.AccessoryColor)); // Lower Back Arms (black)
        Mouth = new SpriteExtraInfo(7, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(18, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Hair2 = null;
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(8, EyesSprite, WhiteColored);
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.SkinColor)); // Belly (white)
        Weapon = new SpriteExtraInfo(5, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.SkinColor)); // Breasts (white)
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.SkinColor)); // Balls (white)

        LeaderClothes = new AntLeader();

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            LeaderClothes,
        };
        AvoidedMainClothingTypes = 1;
        AvoidedEyeTypes = 0;
        AllowedWaistTypes = new List<MainClothing>()
        {
        };

        clothingColors = 0;
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.AccessoryColor = unit.SkinColor;

        unit.HairStyle = State.Rand.Next(14);

        if (unit.Type == UnitType.Leader)
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(LeaderClothes);
    }

    internal override int DickSizes => 8;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor) => Sprites[3 + actor.Unit.BodySize]; // Lower Body(black)

    protected override Sprite HeadSprite(Actor_Unit actor) => Sprites[0 + actor.Unit.BodySize]; // Upper Body (White)

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites[13]; // Abdomen (black)

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites[12]; // Abdomen 2 (White)

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites[22 + actor.Unit.SpecialAccessoryType]; // Antennae (black)

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Upper Front Arms (black)
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return Sprites[11];
            return Sprites[7];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites[8];
            case 1:
                return Sprites[10];
            case 2:
                return Sprites[8];
            case 3:
                return Sprites[10];
            case 4:
                return Sprites[9];
            case 5:
                return Sprites[11];
            case 6:
                return Sprites[9];
            case 7:
                return Sprites[11];
            default:
                return Sprites[7];
        }
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) => Sprites[6]; // Lower Back Arms (black)

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites[48];
        return Sprites[49 + actor.Unit.MouthType];
    }

    protected override Sprite HairSprite(Actor_Unit actor) => Sprites[34 + actor.Unit.HairStyle];

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[14 + actor.Unit.EyeType];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(29, 0.8f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -26 * .625f);
                return Sprites2[95];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -26 * .625f);
                return Sprites2[94];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 28)
            {
                AddOffset(Belly, 0, -26 * .625f);
                return Sprites2[93];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 27)
            {
                AddOffset(Belly, 0, -26 * .625f);
                return Sprites2[92];
            }
            switch (size)
            {
                case 24:
                    AddOffset(Belly, 0, -7 * .625f);
                    break;
                case 25:
                    AddOffset(Belly, 0, -11 * .625f);
                    break;
                case 26:
                    AddOffset(Belly, 0, -14 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -18 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -21 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -26 * .625f);
                    break;
            }

            return Sprites2[62 + size];
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
                    return Sprites[84];
                case 1:
                    return Sprites[85];
                case 2:
                    return Sprites[84];
                case 3:
                    return Sprites[85];
                case 4:
                    return Sprites[86];
                case 5:
                    return Sprites[87];
                case 6:
                    return Sprites[86];
                case 7:
                    return Sprites[87];
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
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(31 * 31, 1f));
            if (leftSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 31)
            {
                return Sprites2[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 29)
            {
                return Sprites2[29];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 27)
            {
                return Sprites2[28];
            }

            if (leftSize > 27)
                leftSize = 27;

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
            int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(31 * 31, 1f));
            if (rightSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 31)
            {
                return Sprites2[61];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 29)
            {
                return Sprites2[60];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 27)
            {
                return Sprites2[59];
            }

            if (rightSize > 27)
                rightSize = 27;

            return Sprites2[31 + rightSize];
        }
        else
        {
            return Sprites2[31 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(31 * 31, 1f)) < 15) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(31 * 31, 1f)) < 15))
            {
                Dick.layer = 20;
                if (actor.IsCockVoring)
                {
                    return Sprites[68 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[52 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    return Sprites[76 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[60 + actor.Unit.DickSize];
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
        if (actor.IsErect() && (actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(31 * 31, 1f)) < 15) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(31 * 31, 1f)) < 15))
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
            AddOffset(Balls, 0, -17 * .625f);
            return Sprites2[132];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -15 * .625f);
            return Sprites2[131];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 26)
        {
            AddOffset(Balls, 0, -13 * .625f);
            return Sprites2[130];
        }
        else if (offset >= 25)
        {
            AddOffset(Balls, 0, -11 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -7 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -5 * .625f);
        }
        else if (offset == 22)
        {
            AddOffset(Balls, 0, -4 * .625f);
        }
        else if (offset == 21)
        {
            AddOffset(Balls, 0, -1 * .625f);
        }

        if (offset > 0)
            return Sprites2[Math.Min(104 + offset, 129)];
        return Sprites2[96 + size];
    }

    class AntLeader : MainClothing
    {
        public AntLeader()
        {
            DiscardSprite = SpriteDictionary.AntQueen1[104];
            coversBreasts = false;
            OccupiesAllSlots = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(19, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(6, null, WhiteColored);
            Type = 199;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.AntQueen.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.AntQueen1[96];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.AntQueen1[Mathf.Min(88 + actor.Unit.BreastSize, 96)];
            }

            clothing2.GetSprite = (s) => SpriteDictionary.AntQueen1[97 + actor.Unit.BodySize];

            clothing3.GetSprite = (s) => SpriteDictionary.AntQueen1[100];

            if (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 3)
            {
                clothing4.GetSprite = (s) => SpriteDictionary.AntQueen1[102];
            }
            else if (actor.GetWeaponSprite() == 5 || actor.GetWeaponSprite() == 7)
            {
                clothing4.GetSprite = (s) => SpriteDictionary.AntQueen1[103];
            }
            else
            {
                clothing4.GetSprite = (s) => SpriteDictionary.AntQueen1[101];
            }

            base.Configure(sprite, actor);
        }
    }
}

