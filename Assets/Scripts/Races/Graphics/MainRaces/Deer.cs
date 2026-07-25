using System;
using System.Collections.Generic;
using UnityEngine;

class Deer : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Deer1;
    readonly Sprite[] Sprites2 = SpriteDictionary.Deer2;
    readonly Sprite[] Sprites3 = SpriteDictionary.Deer3;
    readonly Sprite[] Sprites4 = SpriteDictionary.Cockatrice2;
    readonly Sprite[] Sprites5 = SpriteDictionary.Deer4;

    readonly DeerLeader1 LeaderClothes1;
    readonly DeerLeader2 LeaderClothes2;
    readonly DeerLeader3 LeaderClothes3;
    readonly DeerRags Rags;

    bool oversize = false;


    public Deer()
    {
        BodySizes = 4;
        EyeTypes = 5;
        SpecialAccessoryCount = 12; // ears     
        HairStyles = 25;
        MouthTypes = 6;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DeerLeaf);
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DeerSkin);
        BodyAccentTypes1 = 12; // antlers
        BodyAccentTypes2 = 7; // pattern types
        BodyAccentTypes3 = 2; // leg types

        ExtendedBreastSprites = true;
        FurCapable = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(20, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor)); // Ears
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor)); // Right Arm
        BodyAccent2 = new SpriteExtraInfo(2, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor)); // Right Hand
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor)); // Body Pattern
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor)); // Arm Pattern
        BodyAccent5 = new SpriteExtraInfo(7, BodyAccentSprite5, WhiteColored); // Nose
        BodyAccent6 = new SpriteExtraInfo(6, BodyAccentSprite6, WhiteColored); // Hoofs
        BodyAccent7 = new SpriteExtraInfo(8, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor)); // Mouth external
        BodyAccent8 = new SpriteExtraInfo(7, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor)); // alternative legs
        Mouth = new SpriteExtraInfo(7, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(21, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(0, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair3 = new SpriteExtraInfo(8, HairSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); // Eyebrows
        Beard = null;
        Eyes = new SpriteExtraInfo(7, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = new SpriteExtraInfo(22, SecondaryAccessorySprite, WhiteColored); // Antlers
        Belly = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(3, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor));

        LeaderClothes1 = new DeerLeader1();
        LeaderClothes2 = new DeerLeader2();
        LeaderClothes3 = new DeerLeader3();
        Rags = new DeerRags();

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
            new Cuirass(),
            new Special1(),
            new Special2(),
            Rags,
            LeaderClothes1,
            LeaderClothes2
        };
        AvoidedMainClothingTypes = 3;
        AvoidedEyeTypes = 0;
        AllowedWaistTypes = new List<MainClothing>()
        {
            new GenericBot1(),
            new GenericBot2(),
            new GenericBot3(),
            new GenericBot4(),
            new GenericBot5(),
            new Loincloth(),
            LeaderClothes3
        };
        ExtraMainClothing1Types = new List<MainClothing>()
        {
            new Scarf(),
            new Necklace(),
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Belly, 0, -1 * .625f);
        if (!actor.Unit.HasBreasts)
        {
            AddOffset(BodyAccent2, 2 * .625f, 0);
            AddOffset(Weapon, 2 * .625f, 0);
        }
        if (actor.Unit.Furry)
        {
            AddOffset(Eyes, 0, 1 * .625f);
        }
        if (actor.Unit.Furry && Config.FurryGenitals)
        {
            AddOffset(Dick, 0, -3 * .625f);
            AddOffset(Balls, 0, -3 * .625f);
        }
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

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

        unit.ClothingExtraType1 = 0;

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
            unit.ClothingType2 = 1 + AllowedWaistTypes.IndexOf(LeaderClothes3);
            if (unit.HasBreasts)
            {
                unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(LeaderClothes1);
            }
            else
            {
                unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(LeaderClothes2);
            }
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
            return Sprites[0 + actor.Unit.BodySize];
        }
        else
        {
            return Sprites[12 + actor.Unit.BodySize];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head - Skin
    {
        if (actor.Unit.Furry)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.IsEating)
                {
                    return Sprites[53];
                }
                else
                {
                    return Sprites[52];
                }
            }
            else
            {
                if (actor.IsEating)
                {
                    return Sprites[55];
                }
                else
                {
                    return Sprites[54];
                }
            }
        }
        else
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.IsEating)
                {
                    return Sprites[49];
                }
                else
                {
                    return Sprites[48];
                }
            }
            else
            {
                if (actor.IsEating)
                {
                    return Sprites[51];
                }
                else
                {
                    return Sprites[50];
                }
            }
        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites[24 + actor.Unit.SpecialAccessoryType]; //ears

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Right Arm
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return Sprites[6 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            return Sprites[4 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites[5 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 1:
                return Sprites[6 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 2:
                return Sprites[5 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 3:
                return Sprites[6 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 4:
                return Sprites[4 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 5:
                return Sprites[5 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 6:
                return Sprites[4 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 7:
                return Sprites[5 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            default:
                return Sprites[4 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Right Hand
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return Sprites[23];
            return Sprites[10];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites[22];
            case 1:
                return Sprites[23];
            case 2:
                return Sprites[22];
            case 3:
                return Sprites[23];
            case 4:
                return Sprites[11];
            case 5:
                return Sprites[22];
            case 6:
                return Sprites[11];
            case 7:
                return Sprites[22];
            default:
                return Sprites[10];
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Body Pattern
    {
        if (actor.Unit.BodyAccentType2 >= 6) //Changed to >= to hopefully prevent a rare exception
        {
            return null;
        }
        else if (actor.Unit.HasBreasts)
        {
            return Sprites2[0 + actor.Unit.BodySize + 20 * actor.Unit.BodyAccentType2];
        }
        else
        {
            return Sprites2[10 + actor.Unit.BodySize + 20 * actor.Unit.BodyAccentType2];
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
            if (actor.IsAttacking) return Sprites2[6 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 10 : 0) + 20 * actor.Unit.BodyAccentType2];
            return Sprites2[4 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 10 : 0) + 20 * actor.Unit.BodyAccentType2];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites2[5 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 10 : 0) + 20 * actor.Unit.BodyAccentType2];
            case 1:
                return Sprites2[6 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 10 : 0) + 20 * actor.Unit.BodyAccentType2];
            case 2:
                return Sprites2[5 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 10 : 0) + 20 * actor.Unit.BodyAccentType2];
            case 3:
                return Sprites2[6 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 10 : 0) + 20 * actor.Unit.BodyAccentType2];
            case 4:
                return Sprites2[4 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 10 : 0) + 20 * actor.Unit.BodyAccentType2];
            case 5:
                return Sprites2[5 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 10 : 0) + 20 * actor.Unit.BodyAccentType2];
            case 6:
                return Sprites2[4 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 10 : 0) + 20 * actor.Unit.BodyAccentType2];
            case 7:
                return Sprites2[5 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 10 : 0) + 20 * actor.Unit.BodyAccentType2];
            default:
                return Sprites2[4 + (actor.Unit.BodySize > 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 10 : 0) + 20 * actor.Unit.BodyAccentType2];
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Nose
    {
        if (actor.Unit.Furry)
        {
            if (actor.Unit.HasBreasts)
            {
                return Sprites[136];
            }
            else
            {
                return Sprites[137];
            }
        }
        else
        {
            return Sprites[135];
        }
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) => Sprites[134]; // hoofs

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // mouth external
    {
        if (actor.Unit.Furry)
        {
            if (actor.IsEating)
                return Sprites[61];
            else if (actor.IsAttacking)
                return Sprites[63];
            return Sprites[70];
        }
        else
        {
            if (actor.IsEating)
                return Sprites[57];
            else if (actor.IsAttacking)
                return Sprites[59];
            return Sprites[64 + actor.Unit.MouthType];
        }
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // Alternative Legs
    {
        if (actor.Unit.BodyAccentType3 == 1)
        {
            return null;
        }
        else if (actor.Unit.HasBreasts)
        {
            return Sprites5[88 + actor.Unit.BodySize];
        }
        else
        {
            return Sprites5[92 + actor.Unit.BodySize];
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            if (actor.IsEating)
                return Sprites[60];
            else if (actor.IsAttacking)
                return Sprites[62];
            return null;
        }
        else
        {
            if (actor.IsEating)
                return Sprites[56];
            else if (actor.IsAttacking)
                return Sprites[58];
            return null;
        }
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle == 24 || actor.Unit.Furry)
        {
            return null;
        }
        else
        {
            return Sprites[84 + actor.Unit.HairStyle];
        }
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle == 24 || actor.Unit.Furry)
        {
            return null;
        }
        else
        {
            return Sprites[108 + actor.Unit.HairStyle];
        }
    }

    protected override Sprite HairSprite3(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return null;
        }
        else
        {
            if (actor.Unit.HasBreasts)
            {
                return Sprites[132];
            }
            else
            {
                return Sprites[133];
            }
        }
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[71 + actor.Unit.EyeType];

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) //antlers
    {
        if (!actor.Unit.HasDick)
        {
            return null;
        }
        else
        {
            return Sprites[36 + actor.Unit.BodyAccentType1];
        }
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
            return Sprites[76 + actor.GetWeaponSprite()];
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

        if (actor.Unit.Furry && Config.FurryGenitals)
        {
            Dick.GetPalette = null;
            Dick.GetColor = WhiteColored;

            if (actor.IsErect())
            {
                if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
                {
                    Dick.layer = 24;
                    if (actor.IsCockVoring)
                    {
                        return Sprites3[54 + actor.Unit.DickSize];
                    }
                    else
                    {
                        return Sprites3[38 + actor.Unit.DickSize];
                    }
                }
                else
                {
                    Dick.layer = 13;
                    if (actor.IsCockVoring)
                    {
                        return Sprites3[62 + actor.Unit.DickSize];
                    }
                    else
                    {
                        return Sprites3[46 + actor.Unit.DickSize];
                    }
                }
            }

            Dick.layer = 11;
            return null;
        }

        if (Dick.GetPalette == null)
        {
            Dick.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor);
            Dick.GetColor = null;
        }

        if (actor.IsErect())
        {
            if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
            {
                Dick.layer = 24;
                if (actor.IsCockVoring)
                {
                    return Sprites3[86 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites3[70 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    return Sprites3[94 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites3[78 + actor.Unit.DickSize];
                }
            }
        }

        Dick.layer = 11;
        return Sprites3[78 + actor.Unit.DickSize];
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
            AddOffset(Balls, 0, -18 * .625f);
            return Sprites3[139 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return Sprites3[138 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return Sprites3[137 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)];
        }
        else if (offset >= 26)
        {
            AddOffset(Balls, 0, -15 * .625f);
        }
        else if (offset == 25)
        {
            AddOffset(Balls, 0, -9 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -7 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -6 * .625f);
        }
        else if (offset == 22)
        {
            AddOffset(Balls, 0, -3 * .625f);
        }
        else if (offset == 21)
        {
            AddOffset(Balls, 0, -2 * .625f);
        }

        if (offset > 0)
            return Sprites3[Math.Min(110 + offset, 136) - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)];
        return Sprites3[102 + size - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)];
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
            if (Races.Deer.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[56];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[48 + actor.Unit.BreastSize];
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
            if (Races.Deer.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[65];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[57 + actor.Unit.BreastSize];
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
            if (Races.Deer.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[74];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[66 + actor.Unit.BreastSize];
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
            clothing1.YOffset = -2 * .625f;
            clothing2.YOffset = -2 * .625f;

            if (Races.Deer.oversize)
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
            clothing1.YOffset = -2 * .625f;
            clothing2.YOffset = -2 * .625f;

            if (Races.Deer.oversize)
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
            clothing1.YOffset = -2 * .625f;

            if (Races.Deer.oversize)
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
            if (Races.Deer.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[95];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[87 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1579;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.HasBelly)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[83 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Deer4[84 + actor.Unit.BodySize];
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
            clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[75 + actor.Unit.BodySize];
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
            if (Races.Deer.oversize)
            {
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[0];
                clothing2.YOffset = 0;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Deer4[2 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[0];
                clothing2.YOffset = 0;
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[1];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, actor.Unit.SkinColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
        }
    }

    class Cuirass : MainClothing
    {
        public Cuirass()
        {
            DiscardSprite = SpriteDictionary.Deer4[47];
            coversBreasts = false;
            OccupiesAllSlots = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(7, null, WhiteColored);
            Type = 61701;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Deer.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Deer4[64];
            }
            else if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BreastSize < 2)
                {
                    breastSprite = null;
                    clothing1.GetSprite = (s) => SpriteDictionary.Deer4[60];
                }
                else if (actor.Unit.BreastSize < 4)
                {
                    breastSprite = null;
                    clothing1.GetSprite = (s) => SpriteDictionary.Deer4[61];
                }
                else if (actor.Unit.BreastSize < 6)
                {
                    breastSprite = null;
                    clothing1.GetSprite = (s) => SpriteDictionary.Deer4[62];
                }
                else
                {
                    breastSprite = null;
                    clothing1.GetSprite = (s) => SpriteDictionary.Deer4[63];
                }
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Deer4[83];
            }

            if (actor.HasBelly)
            {
                clothing2.GetSprite = null;
            }
            else
            {
                if (actor.Unit.HasBreasts)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.Deer4[73 + actor.Unit.BodySize];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.Deer4[77 + actor.Unit.BodySize];
                }
            }

            if (actor.Unit.HasBreasts)
            {
                clothing3.GetSprite = (s) => SpriteDictionary.Deer4[65 + actor.Unit.BodySize];
            }
            else
            {
                clothing3.GetSprite = (s) => SpriteDictionary.Deer4[69 + actor.Unit.BodySize];
            }

            if (actor.GetWeaponSprite() == 1)
            {
                clothing4.GetSprite = (s) => SpriteDictionary.Deer4[82];
            }
            else
            {
                clothing4.GetSprite = (s) => SpriteDictionary.Deer4[81];
            }

            base.Configure(sprite, actor);
        }
    }

    class Special1 : MainClothing
    {
        public Special1()
        {
            DiscardSprite = SpriteDictionary.Deer4[104];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 61708;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Deer.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Deer4[103];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.Deer4[96 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[50];
                    clothing3.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[51];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[52];
                    clothing3.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[53];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[54];
                    clothing3.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[55];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.Cockatrice2[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => SpriteDictionary.Cockatrice2[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, actor.Unit.SkinColor);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
        }
    }

    class Special2 : MainClothing
    {
        public Special2()
        {
            DiscardSprite = SpriteDictionary.Deer4[107];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 61709;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Deer4[108 + actor.Unit.BodySize];

            base.Configure(sprite, actor);
        }
    }

    class DeerRags : MainClothing
    {
        public DeerRags()
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
                    clothing1.GetSprite = (s) => SpriteDictionary.Deer4[57];
                else if (actor.Unit.BreastSize < 6)
                    clothing1.GetSprite = (s) => SpriteDictionary.Deer4[58];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Deer4[59];

                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[48 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Deer4[56];
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[52 + actor.Unit.BodySize];
            }

            base.Configure(sprite, actor);
        }
    }

    class DeerLeader1 : MainClothing
    {
        public DeerLeader1()
        {
            leaderOnly = true;
            DiscardSprite = SpriteDictionary.DeerLeaderClothes[49];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 61702;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Deer.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[63];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[Math.Min(56 + actor.Unit.BreastSize, 66)];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[50];
                    clothing3.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[51];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[52];
                    clothing3.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[53];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[54];
                    clothing3.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[55];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.Cockatrice2[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => SpriteDictionary.Cockatrice2[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, actor.Unit.SkinColor);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
        }
    }

    class DeerLeader2 : MainClothing
    {
        public DeerLeader2()
        {
            leaderOnly = true;
            DiscardSprite = SpriteDictionary.DeerLeaderClothes[65];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 61703;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[64];

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
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[20];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[22];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[21];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[12 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[16 + actor.Unit.BodySize];
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
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[32];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[34];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[33];
            }
            else clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[31];

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[20 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[24 + actor.Unit.BodySize];
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
            clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[35];

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[20 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[24 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot4 : MainClothing
    {
        public GenericBot4()
        {
            DiscardSprite = SpriteDictionary.Cockatrice3[47];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 61602;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[44];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[46];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[45];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[28 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[32 + actor.Unit.BodySize];
            }
            base.Configure(sprite, actor);
        }
    }

    class GenericBot5 : MainClothing
    {
        public GenericBot5()
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
                clothing1.YOffset = -1 * .625f;
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Deer4[44];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Deer4[46];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Deer4[45];
            }
            else clothing1.GetSprite = null;
            clothing1.YOffset = 0;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[36 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[40 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class Loincloth : MainClothing
    {
        public Loincloth()
        {
            DiscardSprite = SpriteDictionary.DeerLeaderClothes[66];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, null);
            Type = 61705;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[1 + 2 * actor.Unit.BodySize];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[0 + 2 * actor.Unit.BodySize];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[9 + 2 * actor.Unit.BodySize];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[8 + 2 * actor.Unit.BodySize];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class DeerLeader3 : MainClothing
    {
        public DeerLeader3()
        {
            leaderOnly = true;
            DiscardSprite = SpriteDictionary.DeerLeaderClothes[48];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(15, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, null);
            clothing3 = new SpriteExtraInfo(15, null, null);
            Type = 61704;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.HasBelly)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[1 + 2 * actor.Unit.BodySize];

                    if (actor.GetStomachSize(31, 0.7f) < 4)
                    {
                        clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[17 + 2 * actor.Unit.BodySize];
                        clothing3.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[33 + 2 * actor.Unit.BodySize];
                    }
                    else
                    {
                        clothing1.GetSprite = null;
                        clothing3.GetSprite = null;
                    }
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[16 + 2 * actor.Unit.BodySize];
                    clothing2.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[0 + 2 * actor.Unit.BodySize];
                    clothing3.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[32 + 2 * actor.Unit.BodySize];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[9 + 2 * actor.Unit.BodySize];

                    if (actor.GetStomachSize(31, 0.7f) < 4)
                    {
                        clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[25 + 2 * actor.Unit.BodySize];
                        clothing3.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[41 + 2 * actor.Unit.BodySize];
                    }
                    else
                    {
                        clothing1.GetSprite = null;
                        clothing3.GetSprite = null;
                    }
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[24 + 2 * actor.Unit.BodySize];
                    clothing2.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[8 + 2 * actor.Unit.BodySize];
                    clothing3.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[40 + 2 * actor.Unit.BodySize];
                }
            }

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerLeaf, actor.Unit.AccessoryColor);

            base.Configure(sprite, actor);
        }
    }

    class Scarf : MainClothing
    {
        public Scarf()
        {
            DiscardSprite = SpriteDictionary.Deer4[106];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(19, null, null);
            Type = 61706;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Deer4[105];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class Necklace : MainClothing
    {
        public Necklace()
        {
            DiscardSprite = SpriteDictionary.Deer4[11];
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(19, null, WhiteColored);
            Type = 61707;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Deer4[10];
            base.Configure(sprite, actor);
        }
    }



}
