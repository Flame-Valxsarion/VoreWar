using System;
using System.Collections.Generic;
using UnityEngine;

class Demisharks : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Sharks1;
    readonly Sprite[] Sprites2 = SpriteDictionary.Sharks2;
    readonly Sprite[] Sprites3 = SpriteDictionary.Sharks3;
    readonly Sprite[] Sprites4 = SpriteDictionary.Sharks4;
    readonly Sprite[] Sprites7 = SpriteDictionary.Sharks7;

    readonly DemisharkLeader LeaderClothes;
    readonly DemisharkRags Rags;

    bool oversize = false;


    public Demisharks()
    {
        BodySizes = 4;
        EyeTypes = 9;
        SpecialAccessoryCount = 12; // ears     
        HairStyles = 25;
        MouthTypes = 0;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SharkSkin);
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MermenHair);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SharkSkin);
        BodyAccentTypes1 = 6; // body types
        BodyAccentTypes2 = 7; // pattern types
        BodyAccentTypes3 = 2; // nose types
        TailTypes = 12;

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(21, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, s.Unit.SkinColor)); // Ears (main)
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, s.Unit.SkinColor)); // Right Arm
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, s.Unit.SkinColor)); // Tail
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, s.Unit.AccessoryColor)); // Body Pattern
        BodyAccent4 = new SpriteExtraInfo(2, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, s.Unit.AccessoryColor)); // Arm Pattern
        BodyAccent5 = new SpriteExtraInfo(2, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, s.Unit.AccessoryColor)); // Tail Pattern
        BodyAccent6 = new SpriteExtraInfo(7, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenHair, s.Unit.HairColor)); // Eyebrow
        Mouth = new SpriteExtraInfo(6, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(20, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(0, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenHair, s.Unit.HairColor));
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(7, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = new SpriteExtraInfo(7, SecondaryAccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, s.Unit.SkinColor)); // Ears (secondary)
        Belly = new SpriteExtraInfo(14, null, null, (s) => SharkColor(s));
        Weapon = new SpriteExtraInfo(3, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => SharkColor(s));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => SharkColor(s));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, null, (s) => SharkColor(s));
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => SharkColor(s));

        LeaderClothes = new DemisharkLeader();
        Rags = new DemisharkRags();

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new GenericTop1(),
            new GenericTop2(),
            new GenericTop3(),
            new GenericTop4(),
            new GenericTop5(),
            new GenericTop6(),
            new GenericTop7(),
            new MaleTop(),
            new MaleTop2(),
            new Natural(),
            new PirateTop1(),
            new PirateTop2(),
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
            new PirateBot1(),
            new PirateBot2(),
            new PirateBot3(),
        };
        ExtraMainClothing1Types = new List<MainClothing>() // hats
        {
            new PirateHat1(),
            new PirateHat2(),
            new PirateHat3(),
        };

        AllowedClothingHatTypes = new List<ClothingAccessory>();

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
    }

    ColorSwapPalette SharkColor(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType1 == 0)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkReversed, actor.Unit.SkinColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, actor.Unit.SkinColor);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (!actor.Unit.HasBreasts)
        {
            AddOffset(BodyAccent, 1 * .625f, 0);
            AddOffset(BodyAccent4, 1 * .625f, 0);
            AddOffset(Weapon, 1 * .625f, 0);
        }
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.AccessoryColor = unit.SkinColor;

        if (State.Rand.Next(3) == 0)
        {
            unit.BodyAccentType2 = (BodyAccentTypes2 - 1);
        }
        else
        {
            unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2 - 1);
        }

        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3);
        unit.TailType = State.Rand.Next(TailTypes);

        if (unit.HasDick && unit.HasBreasts)
        {
            if (Config.HermsOnlyUseFemaleHair)
                unit.HairStyle = State.Rand.Next(18);
            else
                unit.HairStyle = State.Rand.Next(HairStyles);
        }
        else if (unit.HasDick && Config.FemaleHairForMales)
            unit.HairStyle = State.Rand.Next(HairStyles);
        else if (unit.HasDick == false && Config.MaleHairForFemales)
            unit.HairStyle = State.Rand.Next(HairStyles);
        else
        {
            if (unit.HasDick)
            {
                unit.HairStyle = 12 + State.Rand.Next(13);
            }
            else
            {
                unit.HairStyle = State.Rand.Next(18);
            }
        }

        if (unit.Type == UnitType.Leader)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(LeaderClothes);
            unit.ClothingExtraType1 = 3;
        }
        else if (State.Rand.Next(3) == 0)
        {
            unit.ClothingExtraType1 = State.Rand.Next(3);
        }
        else
        {
            unit.ClothingExtraType1 = 0;
        }

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
        if (actor.Unit.HasBreasts)
        {
            return Sprites[0 + actor.Unit.BodySize + 22 * actor.Unit.BodyAccentType1];
        }
        else
        {
            return Sprites[11 + actor.Unit.BodySize + 22 * actor.Unit.BodyAccentType1];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites[10 + 11 * actor.Unit.BodyAccentType3 + 22 * actor.Unit.BodyAccentType1];
        if (actor.IsAttacking)
            return Sprites[9 + 11 * actor.Unit.BodyAccentType3 + 22 * actor.Unit.BodyAccentType1];
        return Sprites[8 + 11 * actor.Unit.BodyAccentType3 + 22 * actor.Unit.BodyAccentType1];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites3[12 + actor.Unit.SpecialAccessoryType]; //ears

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Right Arm
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return Sprites[7 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType1];
            return Sprites[4 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType1];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites[5 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType1];
            case 1:
                return Sprites[6 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType1];
            case 2:
                return Sprites[5 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType1];
            case 3:
                return Sprites[6 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType1];
            case 4:
                return Sprites[5 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType1];
            case 5:
                return Sprites[7 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType1];
            case 6:
                return Sprites[5 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType1];
            case 7:
                return Sprites[6 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType1];
            default:
                return Sprites[4 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType1];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites3[84 + actor.Unit.TailType + 12 * (actor.Unit.BodyAccentType1 == 0 ? 1 : 0)]; // tail

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Body Pattern
    {
        if (actor.Unit.BodyAccentType2 == 6)
        {
            return null;
        }
        else if (actor.Unit.HasBreasts)
        {
            return Sprites2[0 + actor.Unit.BodySize + 22 * actor.Unit.BodyAccentType2];
        }
        else
        {
            return Sprites2[11 + actor.Unit.BodySize + 22 * actor.Unit.BodyAccentType2];
        }
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Arm Pattern
    {
        if (actor.Unit.BodyAccentType2 == 6)
        {
            return null;
        }
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return Sprites2[7 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType2];
            return Sprites2[4 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType2];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites2[4 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType2];
            case 1:
                return Sprites2[5 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType2];
            case 2:
                return Sprites2[4 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType2];
            case 3:
                return Sprites2[5 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType2];
            case 4:
                return Sprites2[4 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType2];
            case 5:
                return Sprites2[6 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType2];
            case 6:
                return Sprites2[4 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType2];
            case 7:
                return Sprites2[5 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType2];
            default:
                return Sprites2[4 + 11 * (actor.Unit.BodySize > 1 ? 1 : 0) + 22 * actor.Unit.BodyAccentType2];
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Tail Pattern
    {
        if (actor.Unit.BodyAccentType2 == 1)
        {
            return Sprites2[7 + 11 * actor.Unit.TailType];
        }
        else if (actor.Unit.BodyAccentType2 == 2)
        {
            return Sprites2[8 + 11 * actor.Unit.TailType];
        }
        else if (actor.Unit.BodyAccentType2 == 4)
        {
            return Sprites2[9 + 11 * actor.Unit.TailType];
        }
        else if (actor.Unit.BodyAccentType2 == 5)
        {
            return Sprites2[10 + 11 * actor.Unit.TailType];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) => Sprites3[11]; // eyebrow

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites3[1];
        else if (actor.IsAttacking)
            return Sprites3[0];
        return null;
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle == 24)
        {
            return null;
        }
        else if (actor.Unit.ClothingExtraType1 == 3)
        {
            return Sprites7[60 + actor.Unit.HairStyle];
        }
        else if (actor.Unit.ClothingExtraType1 == 1)
        {
            return Sprites7[12 + actor.Unit.HairStyle];
        }
        else if (actor.Unit.ClothingExtraType1 == 2)
        {
            return Sprites7[36 + actor.Unit.HairStyle];
        }
        else if (actor.Unit.HairStyle < 12)
        {
            return Sprites3[36 + actor.Unit.HairStyle];
        }
        else
        {
            return Sprites3[48 + actor.Unit.HairStyle];
        }
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle > 14)
        {
            return null;
        }
        else if (actor.Unit.HairStyle < 12)
        {
            return Sprites3[48 + actor.Unit.HairStyle];
        }
        else
        {
            return Sprites3[60 + actor.Unit.HairStyle];
        }
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites3[2 + actor.Unit.EyeType];

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => Sprites3[24 + actor.Unit.SpecialAccessoryType]; //ears

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(31, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -29 * .625f);
                return Sprites4[99];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -29 * .625f);
                return Sprites4[98];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -29 * .625f);
                return Sprites4[97];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -29 * .625f);
                return Sprites4[96];
            }
            switch (size)
            {
                case 26:
                    AddOffset(Belly, 0, -3 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -8 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -13 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -16 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -22 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -28 * .625f);
                    break;
            }

            return Sprites4[64 + size];
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
            if (actor.GetWeaponSprite() < 4 && actor.IsAttacking)
            {
                Weapon.layer = 20;
                return Sprites3[75 + actor.GetWeaponSprite()];
            }
            else
            {
                Weapon.layer = 3;
                return Sprites3[75 + actor.GetWeaponSprite()];
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
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f));
            if (leftSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
            {
                return Sprites4[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return Sprites4[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return Sprites4[29];
            }

            if (leftSize > 28)
                leftSize = 28;

            return Sprites4[0 + leftSize];
        }
        else
        {
            return Sprites4[0 + actor.Unit.BreastSize];
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
                return Sprites4[63];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return Sprites4[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return Sprites4[61];
            }

            if (rightSize > 28)
                rightSize = 28;

            return Sprites4[32 + rightSize];
        }
        else
        {
            return Sprites4[32 + actor.Unit.BreastSize];
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
                Dick.layer = 20;
                if (actor.IsCockVoring)
                {
                    return Sprites3[132 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites3[124 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    return Sprites3[116 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites3[108 + actor.Unit.DickSize];
                }
            }
        }

        Dick.layer = 11;
        return Sprites3[108 + actor.Unit.DickSize];
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
            AddOffset(Balls, 0, -21 * .625f);
            return Sprites4[137];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -19 * .625f);
            return Sprites4[136];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -17 * .625f);
            return Sprites4[135];
        }
        else if (offset >= 26)
        {
            AddOffset(Balls, 0, -15 * .625f);
        }
        else if (offset == 25)
        {
            AddOffset(Balls, 0, -11 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -9 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -8 * .625f);
        }
        else if (offset == 22)
        {
            AddOffset(Balls, 0, -5 * .625f);
        }
        else if (offset == 21)
        {
            AddOffset(Balls, 0, -4 * .625f);
        }
        else if (offset == 20)
        {
            AddOffset(Balls, 0, -2 * .625f);
        }

        if (offset > 0)
            return Sprites4[Math.Min(108 + offset, 134)];
        return Sprites4[100 + size];
    }


    class GenericTop1 : MainClothing
    {
        public GenericTop1()
        {
            DiscardSprite = SpriteDictionary.Avians4[24];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1524;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demisharks.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[53];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[45 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1534;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demisharks.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[62];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[54 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1544;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demisharks.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[71];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[63 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 1555;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demisharks.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[80];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[72 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[81];
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
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 1574;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demisharks.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[90];
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[99];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[82 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[91 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1588;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demisharks.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[104 + actor.Unit.BreastSize];
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

    class GenericTop7 : MainClothing
    {
        public GenericTop7()
        {
            DiscardSprite = SpriteDictionary.Avians4[44];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1544;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demisharks.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[140];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[132 + actor.Unit.BreastSize];
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

    class PirateTop1 : MainClothing
    {
        public PirateTop1()
        {
            DiscardSprite = SpriteDictionary.Sharks6[68];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(16, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 61301;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demisharks.oversize || actor.Unit.BreastSize == 7)
            {
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks6[48];
                clothing3.GetSprite = (s) => SpriteDictionary.Sharks6[67];
            }
            else if (actor.Unit.HasBreasts)
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[51 + actor.Unit.BodySize];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks7[84 + actor.Unit.BodySize];
                }
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks6[48];
                clothing3.GetSprite = (s) => SpriteDictionary.Sharks6[60 + actor.Unit.BreastSize];
            }
            else
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[56 + actor.Unit.BodySize];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks7[88 + actor.Unit.BodySize];
                }
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks6[49];
                clothing3.GetSprite = null;
            }

            base.Configure(sprite, actor);
        }
    }

    class PirateTop2 : MainClothing
    {
        public PirateTop2()
        {
            DiscardSprite = SpriteDictionary.Sharks6[109];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(15, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(19, null, WhiteColored);
            Type = 61302;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demisharks.oversize || actor.Unit.BreastSize == 7)
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[74 + (3 * actor.Unit.BodySize)];
                    clothing1.layer = 16;
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[73 + (3 * actor.Unit.BodySize)];
                    clothing1.layer = 16;
                }
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[74 + (3 * actor.Unit.BodySize)];
                    clothing1.layer = 18;
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[72 + (3 * actor.Unit.BodySize)];
                    clothing1.layer = 18;
                }
                clothing3.GetSprite = (s) => SpriteDictionary.Sharks6[102 + actor.Unit.BreastSize];
            }
            else
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[86 + (3 * actor.Unit.BodySize)];
                    clothing1.layer = 18;
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[84 + (3 * actor.Unit.BodySize)];
                    clothing1.layer = 18;
                }
                clothing3.GetSprite = null;
            }

            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) clothing2.GetSprite = (s) => SpriteDictionary.Sharks6[98 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
                else clothing2.GetSprite = (s) => SpriteDictionary.Sharks6[96 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }
            else if (actor.GetWeaponSprite() == 5)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks6[98 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }
            else if (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 3 || actor.GetWeaponSprite() == 7)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks6[97 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks6[96 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }

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
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1579;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.HasBelly)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[116 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[112 + actor.Unit.BodySize];
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
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1579;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[100 + actor.Unit.BodySize];
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
            if (Races.Demisharks.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[2 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[1];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[0];
            }

            if (actor.Unit.BodyAccentType1 == 0)
            {
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkReversed, actor.Unit.SkinColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkReversed, actor.Unit.SkinColor);
            }
            else
            {
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, actor.Unit.SkinColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, actor.Unit.SkinColor);
            }

            base.Configure(sprite, actor);
        }
    }

    class DemisharkRags : MainClothing
    {
        public DemisharkRags()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Rags[23];
            blocksDick = false;
            inFrontOfDick = 2;
            coversBreasts = false;
            Type = 207;
            OccupiesAllSlots = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BreastSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[129];
                else if (actor.Unit.BreastSize < 6)
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[130];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[131];

                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[120 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[128];
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[124 + actor.Unit.BodySize];
            }

            base.Configure(sprite, actor);
        }
    }

    class DemisharkLeader : MainClothing
    {
        public DemisharkLeader()
        {
            leaderOnly = true;
            DiscardSprite = SpriteDictionary.Sharks6[118];
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(15, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(19, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(2, null, WhiteColored);
            clothing5 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 61303;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.GetStomachSize(31) > 9)
            {
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
                if (actor.Unit.HasBreasts)
                {
                    clothing5.GetSprite = (s) => SpriteDictionary.Sharks6[36 + actor.Unit.BodySize];
                }
                else
                {
                    clothing5.GetSprite = (s) => SpriteDictionary.Sharks6[44 + actor.Unit.BodySize];
                }
            }
            else if (Races.Demisharks.oversize || actor.Unit.BreastSize == 7)
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[120 + (2 * actor.Unit.BodySize)];
                    clothing5.GetSprite = (s) => SpriteDictionary.Sharks6[36 + actor.Unit.BodySize];
                    clothing1.layer = 16;
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[120 + (2 * actor.Unit.BodySize)];
                    clothing5.GetSprite = (s) => SpriteDictionary.Sharks6[32 + actor.Unit.BodySize];
                    clothing1.layer = 16;
                }
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[120 + (2 * actor.Unit.BodySize)];
                    clothing5.GetSprite = (s) => SpriteDictionary.Sharks6[36 + actor.Unit.BodySize];
                    clothing1.layer = 18;
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[119 + (2 * actor.Unit.BodySize)];
                    clothing5.GetSprite = (s) => SpriteDictionary.Sharks6[32 + actor.Unit.BodySize];
                    clothing1.layer = 18;
                }
                clothing3.GetSprite = (s) => SpriteDictionary.Sharks6[110 + actor.Unit.BreastSize];
            }
            else
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[131 + (2 * actor.Unit.BodySize)];
                    clothing5.GetSprite = (s) => SpriteDictionary.Sharks6[44 + actor.Unit.BodySize];
                    clothing1.layer = 18;
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[130 + (2 * actor.Unit.BodySize)];
                    clothing5.GetSprite = (s) => SpriteDictionary.Sharks6[40 + actor.Unit.BodySize];
                    clothing1.layer = 18;
                }
                clothing3.GetSprite = null;
            }

            if (actor.GetStomachSize(31) > 9)
            {
                clothing2.GetSprite = null;
            }
            else if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) clothing2.GetSprite = (s) => SpriteDictionary.Sharks6[129 + 11 * (!actor.Unit.HasBreasts ? 1 : 0)];
                else clothing2.GetSprite = (s) => SpriteDictionary.Sharks6[127 + 11 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }
            else if (actor.GetWeaponSprite() == 5)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks6[129 + 11 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }
            else if (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 3 || actor.GetWeaponSprite() == 7)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks6[128 + 11 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks6[127 + 11 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }

            if (actor.GetStomachSize(31) > 9)
            {
                clothing4.GetSprite = null;
            }
            else
            {
                clothing4.GetSprite = (s) => SpriteDictionary.Sharks6[117];
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
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[18];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[20];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[19];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[10 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[14 + actor.Unit.BodySize];
            }

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
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[30];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[32];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[31];
            }
            else clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[29];

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[21 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[25 + actor.Unit.BodySize];
            }

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
            clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[33];

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[21 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[25 + actor.Unit.BodySize];
            }

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
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[42];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[44];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[43];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[34 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[38 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class PirateBot1 : MainClothing
    {
        public PirateBot1()
        {
            DiscardSprite = SpriteDictionary.Sharks6[69];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 61304;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[33];

            if (actor.Unit.HasBreasts)
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[4 + actor.Unit.BodySize];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[0 + actor.Unit.BodySize];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[12 + actor.Unit.BodySize];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[8 + actor.Unit.BodySize];
                }
            }

            base.Configure(sprite, actor);
        }
    }

    class PirateBot2 : MainClothing
    {
        public PirateBot2()
        {
            DiscardSprite = SpriteDictionary.Sharks6[70];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 61305;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[33];

            if (actor.Unit.HasBreasts)
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[20 + actor.Unit.BodySize];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[16 + actor.Unit.BodySize];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[28 + actor.Unit.BodySize];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[24 + actor.Unit.BodySize];
                }
            }

            base.Configure(sprite, actor);
        }
    }

    class PirateBot3 : MainClothing
    {
        public PirateBot3()
        {
            DiscardSprite = SpriteDictionary.Sharks6[71];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 61306;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[33];

            if (actor.Unit.HasBreasts)
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[36 + actor.Unit.BodySize];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[32 + actor.Unit.BodySize];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[44 + actor.Unit.BodySize];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Sharks6[40 + actor.Unit.BodySize];
                }
            }

            base.Configure(sprite, actor);
        }
    }

    class PirateHat1 : MainClothing
    {
        public PirateHat1()
        {
            coversBreasts = false;
            blocksDick = false;
            DiscardSprite = SpriteDictionary.Sharks7[4];
            Type = 61307;
            clothing1 = new SpriteExtraInfo(22, null, null);
            clothing2 = new SpriteExtraInfo(1, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HairStyle > 17)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks7[2];
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks7[3];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks7[0];
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks7[1];
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class PirateHat2 : MainClothing
    {
        public PirateHat2()
        {
            coversBreasts = false;
            blocksDick = false;
            DiscardSprite = SpriteDictionary.Sharks7[7];
            Type = 61308;
            clothing1 = new SpriteExtraInfo(22, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(1, null, WhiteColored);
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            clothing1.GetSprite = (s) => SpriteDictionary.Sharks7[5];
            clothing2.GetSprite = (s) => SpriteDictionary.Sharks7[6];


            base.Configure(sprite, actor);
        }
    }

    class PirateHat3 : MainClothing
    {
        public PirateHat3()
        {
            leaderOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(22, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(1, null, WhiteColored);
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HairStyle > 17)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks7[10];
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks7[11];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks7[8];
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks7[9];
            }

            base.Configure(sprite, actor);
        }
    }

}
