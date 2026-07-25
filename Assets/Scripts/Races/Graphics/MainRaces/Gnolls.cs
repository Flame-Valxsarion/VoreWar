using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Gnolls : DefaultRaceData
{
    readonly Sprite[] BodySprites = SpriteDictionary.GnollsBodyParts;
    readonly Sprite[] VoreSprites = SpriteDictionary.GnollsVoreParts;
    readonly Sprite[] ClothingSprites = SpriteDictionary.GnollClothes;
    readonly GnollLeader LeaderClothes;
    readonly GnollRags Rags;

    int RandomExpression = 0;
    int Hairstyle = 0;

    bool oversize = false;

    internal override int BreastSizes => 8;
    internal override int DickSizes => 8;

    public Gnolls()
    {
        BodySizes = 0;
        MouthTypes = 7;
        EarTypes = 12;
        HairStyles = 15;
        AccessoryColors = 0;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.GnollSkin);
		HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.GnollSkin);
        EyeTypes = 6;
        TailTypes = 2;
        BodyAccentTypes1 = 4; // Facial Fur Patterns
        BodyAccentTypes2 = 5; // Body Fur Patterns
        ExtendedBreastSprites = true;

        BodyAccent4 = new SpriteExtraInfo(0, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GnollSkin, s.Unit.SkinColor)); // Tail
        Body = new SpriteExtraInfo(1, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GnollSkin, s.Unit.SkinColor)); // You got a fine one~  Yes, you!  Who else?
        SecondaryAccessory = new SpriteExtraInfo(2, SecondaryAccessorySprite, null, null); // Claws For Scratchies
        BodyAccessory = new SpriteExtraInfo(2, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GnollSkin, s.Unit.SkinColor)); // Body Fur Pattern
        Balls = new SpriteExtraInfo(3, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GnollSkin, s.Unit.SkinColor)); // Critical damage
        Dick = new SpriteExtraInfo(4, DickSprite, null, null); // Strange cylindrical object
        Belly = new SpriteExtraInfo(9, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GnollSkin, s.Unit.SkinColor)); // Snuggle into it
        Breasts = new SpriteExtraInfo(13, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GnollSkin, s.Unit.SkinColor)); // Left Booba
        SecondaryBreasts = new SpriteExtraInfo(13, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GnollSkin, s.Unit.SkinColor)); // Right Booba
        Head = new SpriteExtraInfo(15, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GnollSkin, s.Unit.SkinColor)); // Yes, please! (sorry...)
        BodyAccent = new SpriteExtraInfo(16, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GnollSkin, s.Unit.SkinColor)); // Face Fur Pattern
        Mouth = new SpriteExtraInfo(17, MouthSprite, null, null);
        Eyes = new SpriteExtraInfo(18, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor)); // Iris
        SecondaryEyes = new SpriteExtraInfo(19, EyesSecondarySprite, null, null); // Sclera
        Hair = new SpriteExtraInfo(20, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GnollSkin, s.Unit.HairColor)); // Main Hair
        BodyAccent2 = new SpriteExtraInfo(21, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GnollSkin, s.Unit.SkinColor)); // Outer Ear Fur
        BodyAccent3 = new SpriteExtraInfo(22, BodyAccentSprite3, null, null); // Inner Ear
        Hair2 = new SpriteExtraInfo(23, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GnollSkin, s.Unit.HairColor)); // Front Hair
        Weapon = new SpriteExtraInfo(8, WeaponSprite, WhiteColored);

        LeaderClothes = new GnollLeader();
        Rags = new GnollRags();

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
            new Special1(),
            Rags,
            LeaderClothes,
        };
		
        AvoidedMainClothingTypes = 2;
        AllowedWaistTypes = new List<MainClothing>()
        {
            new GenericBot1(),
            new GenericBot2(),
            new GenericBot3(),
            new GenericBot4(),
            new GenericBot5(),
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.HairColor = unit.SkinColor;
        unit.HairStyle = State.Rand.Next(HairStyles);
        unit.EyeType = State.Rand.Next(EyeTypes);
        unit.EarType = State.Rand.Next(EarTypes);
        unit.TailType = State.Rand.Next(TailTypes);
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);

        if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(Rags);
            if (unit.ClothingType == 0) //Covers rags not in the list
                unit.ClothingType = AllowedMainClothingTypes.Count;
        }
        if (unit.Type == UnitType.Leader)
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(LeaderClothes);

        if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(Rags);
            if (unit.ClothingType == 0) //Covers rags not in the list
                unit.ClothingType = AllowedMainClothingTypes.Count;
        }
        if (unit.Type == UnitType.Leader)
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(LeaderClothes);
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        int RandomExpression = State.Rand.Next(3);
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        return BodySprites[76 + actor.Unit.EyeType];
    }

    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
        return BodySprites[70 + actor.Unit.EyeType];
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        int Hairstyle = actor.Unit.HairStyle;
        return BodySprites[108 + actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle == 3) return BodySprites[82];
        if (actor.Unit.HairStyle == 9) return BodySprites[83];
        else return null;
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring) return BodySprites[44];
        if (actor.IsUnbirthing || actor.IsAnalVoring)
        {
            if (RandomExpression == 0) return BodySprites[46];
            if (RandomExpression == 1) return BodySprites[47];
            if (RandomExpression == 2) return BodySprites[50];
            else return BodySprites[45 + (actor.Unit.MouthType)]; //Shouldn't happen, but let's see
        }
        if (actor.IsAttacking) 
        {
            if (RandomExpression == 0) return BodySprites[45];
            if (RandomExpression == 1) return BodySprites[46];
            if (RandomExpression == 2) return BodySprites[48];
            else return BodySprites[45 + (actor.Unit.MouthType)];
        }
        else return BodySprites[45 + (actor.Unit.MouthType)];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring) return BodySprites[36];
        if (actor.IsUnbirthing || actor.IsAnalVoring)
        {
            if (RandomExpression == 0) return BodySprites[38];
            if (RandomExpression == 1) return BodySprites[39];
            if (RandomExpression == 2) return BodySprites[42];
            else return BodySprites[37 + (actor.Unit.MouthType)]; //Shouldn't happen, but let's see
        }
        if (actor.IsAttacking) 
        {
            if (RandomExpression == 0) return BodySprites[37];
            if (RandomExpression == 1) return BodySprites[38];
            if (RandomExpression == 2) return BodySprites[40];
            else return BodySprites[37 + (actor.Unit.MouthType)];
        }
        else return BodySprites[37 + (actor.Unit.MouthType)];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Face Fur Pattern
    {
        if (actor.Unit.BodyAccentType1 == 0) return null;
        if (actor.Unit.BodyAccentType1 == 1)
        {
            if (actor.IsOralVoring) return BodySprites[52];
            if (actor.IsUnbirthing || actor.IsAnalVoring)
            {
                if (RandomExpression == 0) return BodySprites[54];
                if (RandomExpression == 1) return BodySprites[55];
                if (RandomExpression == 2) return BodySprites[58];
                else return BodySprites[53 + (actor.Unit.MouthType)]; //Shouldn't happen, but let's see
            }
            if (actor.IsAttacking) 
            {
                if (RandomExpression == 0) return BodySprites[53];
                if (RandomExpression == 1) return BodySprites[54];
                if (RandomExpression == 2) return BodySprites[56];
                else return BodySprites[53 + (actor.Unit.MouthType)];
            }
            else return BodySprites[53 + (actor.Unit.MouthType)];
        }
        if (actor.Unit.BodyAccentType1 == 2)
        {
            if (actor.IsOralVoring) return BodySprites[60];
            if (actor.IsUnbirthing || actor.IsAnalVoring)
            {
                if (RandomExpression == 0) return BodySprites[62];
                if (RandomExpression == 1) return BodySprites[63];
                if (RandomExpression == 2) return BodySprites[66];
                else return BodySprites[61 + (actor.Unit.MouthType)]; //Shouldn't happen, but let's see
            }
            if (actor.IsAttacking) 
            {
                if (RandomExpression == 0) return BodySprites[61];
                if (RandomExpression == 1) return BodySprites[62];
                if (RandomExpression == 2) return BodySprites[64];
                else return BodySprites[61 + (actor.Unit.MouthType)];
            }
            else return BodySprites[61 + (actor.Unit.MouthType)];
        }
        if (actor.Unit.BodyAccentType1 == 3) return BodySprites[68];
        else return null;
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Outer Ear Fur
    {
        return BodySprites[84 + actor.Unit.EarType];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Inner Ear
    {
        return BodySprites[96 + actor.Unit.EarType];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Tail
    {
        if (actor.Unit.TailType == 0) return BodySprites[69];
        else return BodySprites[139];
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts) // Fem
        {
            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) return BodySprites[2];
                return BodySprites[0];
            }

            switch (actor.GetWeaponSprite())
                {
                case 0:
                    return BodySprites[1];
                case 1:
                    return BodySprites[2];
                case 2:
                    return BodySprites[1];
                case 3:
                    return BodySprites[2];
                case 4:
                    return BodySprites[1];
                case 5:
                    return BodySprites[2];
                case 6:
                    return BodySprites[0];
                case 7:
                    return BodySprites[1];
                default:
                    return null;
            }
        }
        else // Masc
        {
            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) return BodySprites[5];
                return BodySprites[3];
            }

            switch (actor.GetWeaponSprite())
                {
                case 0:
                    return BodySprites[4];
                case 1:
                    return BodySprites[5];
                case 2:
                    return BodySprites[4];
                case 3:
                    return BodySprites[5];
                case 4:
                    return BodySprites[4];
                case 5:
                    return BodySprites[5];
                case 6:
                    return BodySprites[3];
                case 7:
                    return BodySprites[4];
                default:
                    return null;
            }
        }
    }


    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(36, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 36)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return VoreSprites[99];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 35)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return VoreSprites[98];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 34)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return VoreSprites[97];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 33)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return VoreSprites[96];
            }
            if (size > 31)
            {
                size = 31;
            }

            switch (size)
            {
                case 21:
                    AddOffset(Belly, 0, -2 * .625f);
                    break;
                case 22:
                    AddOffset(Belly, 0, -4 * .625f);
                    break;
                case 23:
                    AddOffset(Belly, 0, -5 * .625f);
                    break;
                case 24:
                    AddOffset(Belly, 0, -6 * .625f);
                    break;
                case 25:
                    AddOffset(Belly, 0, -7 * .625f);
                    break;
                case 26:
                    AddOffset(Belly, 0, -10 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -15 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -20 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -23 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -29 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -35 * .625f);
                    break;
            }

            return VoreSprites[64 + size];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // Body Fur Pattern
    {
        if (actor.Unit.BodyAccentType2 == 0) return null;
        if (actor.Unit.BodyAccentType2 == 1)
        {
            if (actor.Unit.HasBreasts) // Fem
            {
                if (actor.Unit.HasWeapon == false)
                {
                    if (actor.IsAttacking) return BodySprites[14];
                    return BodySprites[12];
                }

                switch (actor.GetWeaponSprite())
                    {
                    case 0:
                        return BodySprites[13];
                    case 1:
                        return BodySprites[14];
                    case 2:
                        return BodySprites[13];
                    case 3:
                        return BodySprites[14];
                    case 4:
                        return BodySprites[13];
                    case 5:
                        return BodySprites[14];
                    case 6:
                        return BodySprites[12];
                    case 7:
                        return BodySprites[13];
                    default:
                        return null;
                }
            }
            else // Masc
            {
                if (actor.Unit.HasWeapon == false)
                {
                    if (actor.IsAttacking) return BodySprites[17];
                    return BodySprites[15];
                }

                switch (actor.GetWeaponSprite())
                    {
                    case 0:
                        return BodySprites[16];
                    case 1:
                        return BodySprites[17];
                    case 2:
                        return BodySprites[16];
                    case 3:
                        return BodySprites[17];
                    case 4:
                        return BodySprites[16];
                    case 5:
                        return BodySprites[17];
                    case 6:
                        return BodySprites[15];
                    case 7:
                        return BodySprites[16];
                    default:
                        return null;
                }
            }
        }
        if (actor.Unit.BodyAccentType2 == 2)
        {
            if (actor.Unit.HasBreasts) // Fem
            {
                if (actor.Unit.HasWeapon == false)
                {
                    if (actor.IsAttacking) return BodySprites[20];
                    return BodySprites[18];
                }

                switch (actor.GetWeaponSprite())
                    {
                    case 0:
                        return BodySprites[19];
                    case 1:
                        return BodySprites[20];
                    case 2:
                        return BodySprites[19];
                    case 3:
                        return BodySprites[20];
                    case 4:
                        return BodySprites[19];
                    case 5:
                        return BodySprites[20];
                    case 6:
                        return BodySprites[18];
                    case 7:
                        return BodySprites[19];
                    default:
                        return null;
                }
            }
            else // Masc
            {
                if (actor.Unit.HasWeapon == false)
                {
                    if (actor.IsAttacking) return BodySprites[23];
                    return BodySprites[21];
                }

                switch (actor.GetWeaponSprite())
                    {
                    case 0:
                        return BodySprites[22];
                    case 1:
                        return BodySprites[23];
                    case 2:
                        return BodySprites[22];
                    case 3:
                        return BodySprites[23];
                    case 4:
                        return BodySprites[22];
                    case 5:
                        return BodySprites[23];
                    case 6:
                        return BodySprites[21];
                    case 7:
                        return BodySprites[22];
                    default:
                        return null;
                }
            }
        }
        if (actor.Unit.BodyAccentType2 == 3)
        {
            if (actor.Unit.HasBreasts) // Fem
            {
                if (actor.Unit.HasWeapon == false)
                {
                    if (actor.IsAttacking) return BodySprites[26];
                    return BodySprites[24];
                }

                switch (actor.GetWeaponSprite())
                    {
                    case 0:
                        return BodySprites[25];
                    case 1:
                        return BodySprites[26];
                    case 2:
                        return BodySprites[25];
                    case 3:
                        return BodySprites[26];
                    case 4:
                        return BodySprites[25];
                    case 5:
                        return BodySprites[26];
                    case 6:
                        return BodySprites[24];
                    case 7:
                        return BodySprites[25];
                    default:
                        return null;
                }
            }
            else // Masc
            {
                if (actor.Unit.HasWeapon == false)
                {
                    if (actor.IsAttacking) return BodySprites[29];
                    return BodySprites[27];
                }

                switch (actor.GetWeaponSprite())
                    {
                    case 0:
                        return BodySprites[28];
                    case 1:
                        return BodySprites[29];
                    case 2:
                        return BodySprites[28];
                    case 3:
                        return BodySprites[29];
                    case 4:
                        return BodySprites[28];
                    case 5:
                        return BodySprites[29];
                    case 6:
                        return BodySprites[27];
                    case 7:
                        return BodySprites[28];
                    default:
                        return null;
                }
            }
        }
        if (actor.Unit.BodyAccentType2 == 4)
        {
           if (actor.Unit.HasBreasts) // Fem
            {
                if (actor.Unit.HasWeapon == false)
                {
                    if (actor.IsAttacking) return BodySprites[32];
                    return BodySprites[30];
                }

                switch (actor.GetWeaponSprite())
                    {
                    case 0:
                        return BodySprites[31];
                    case 1:
                        return BodySprites[32];
                    case 2:
                        return BodySprites[31];
                    case 3:
                        return BodySprites[32];
                    case 4:
                        return BodySprites[31];
                    case 5:
                        return BodySprites[32];
                    case 6:
                        return BodySprites[30];
                    case 7:
                        return BodySprites[31];
                    default:
                        return null;
                }
            }
            else // Masc
            {
                if (actor.Unit.HasWeapon == false)
                {
                    if (actor.IsAttacking) return BodySprites[35];
                    return BodySprites[33];
                }

                switch (actor.GetWeaponSprite())
                    {
                    case 0:
                        return BodySprites[34];
                    case 1:
                        return BodySprites[35];
                    case 2:
                        return BodySprites[34];
                    case 3:
                        return BodySprites[35];
                    case 4:
                        return BodySprites[34];
                    case 5:
                        return BodySprites[35];
                    case 6:
                        return BodySprites[33];
                    case 7:
                        return BodySprites[34];
                    default:
                        return null;
                }
            }
        }
        else return null; // Take THAT!  Stupid Console errors...
    }

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) // Claws
    {
        if (actor.Unit.HasBreasts) // Fem
        {
            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) return BodySprites[8];
                return BodySprites[6];
            }

            switch (actor.GetWeaponSprite())
                {
                case 0:
                    return BodySprites[7];
                case 1:
                    return BodySprites[8];
                case 2:
                    return BodySprites[7];
                case 3:
                    return BodySprites[8];
                case 4:
                    return BodySprites[7];
                case 5:
                    return BodySprites[8];
                case 6:
                    return BodySprites[6];
                case 7:
                    return BodySprites[7];
                default:
                    return null;
            }
        }
        else // Masc
        {
            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) return BodySprites[11];
                return BodySprites[9];
            }

            switch (actor.GetWeaponSprite())
                {
                case 0:
                    return BodySprites[10];
                case 1:
                    return BodySprites[11];
                case 2:
                    return BodySprites[10];
                case 3:
                    return BodySprites[11];
                case 4:
                    return BodySprites[10];
                case 5:
                    return BodySprites[11];
                case 6:
                    return BodySprites[9];
                case 7:
                    return BodySprites[10];
                default:
                    return null;
            }
        }
    }

    protected override Sprite BreastsSprite(Actor_Unit actor) // Left Breast
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
                return VoreSprites[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return VoreSprites[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return VoreSprites[29];
            }

            if (leftSize > 28)
                leftSize = 28;

            return VoreSprites[0 + leftSize];
        }
        else
        {
            return VoreSprites[0 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor) // Right Breast
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
                return VoreSprites[63];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return VoreSprites[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return VoreSprites[61];
            }

            if (rightSize > 28)
                rightSize = 28;

            return VoreSprites[32 + rightSize];
        }
        else
        {
            return VoreSprites[32 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor) // A Cylinder
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            // Huge Bellies/Breasts (Down)
            if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
            {
                Dick.layer = 21;
                if (actor.IsCockVoring)
                {
                    return VoreSprites[154 + actor.Unit.DickSize];
                }
                else
                {
                    return VoreSprites[138 + actor.Unit.DickSize];
                }
            }
            // Normal (Up)
            else
            {
                Dick.layer = 4;
                if (actor.IsCockVoring)
                {
                    return VoreSprites[162 + actor.Unit.DickSize];
                }
                else
                {
                    return VoreSprites[146 + actor.Unit.DickSize];
                }
            }
        }
        else return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect() && (actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
        {
            //Balls.layer = 19;
        }
        else
        {
            //Balls.layer = 10;
        }
        int size = actor.Unit.DickSize;
        int offset = actor.GetBallSize(28, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -27 * .625f);
            return VoreSprites[137];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -27 * .625f);
            return VoreSprites[136];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -27 * .625f);
            return VoreSprites[135];
        }
        else if (offset >= 26)
        {
            AddOffset(Balls, 0, -27 * .625f);
        }
        else if (offset == 25)
        {
            AddOffset(Balls, 0, -18 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -16 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -15 * .625f);
        }
        else if (offset == 22)
        {
            AddOffset(Balls, 0, -12 * .625f);
        }
        else if (offset == 21)
        {
            AddOffset(Balls, 0, -11 * .625f);
        }
        else if (offset == 20)
        {
            AddOffset(Balls, 0, -9 * .625f);
        }
        else if (offset == 19)
        {
            AddOffset(Balls, 0, -7 * .625f);
        }
        else if (offset == 18)
        {
            AddOffset(Balls, 0, -6 * .625f);
        }
        else if (offset == 17)
        {
            AddOffset(Balls, 0, -4 * .625f);
        }
        else if (offset == 16)
        {
            AddOffset(Balls, 0, -2 * .625f);
        }

        if (offset > 0)
            return VoreSprites[Math.Min(108 + offset, 134)];
        return VoreSprites[100 + size];
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            if (actor.Unit.HasBreasts) return BodySprites[123 + actor.GetWeaponSprite()];
            else return BodySprites[131 + actor.GetWeaponSprite()];
        }
        else return null;
    }

    /// CLOTHING ///
    
    class GenericTop1 : MainClothing
    {
        public GenericTop1()
        {
            DiscardSprite = SpriteDictionary.Avians4[24];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(14, null, null);
            Type = 1524;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Gnolls.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[8];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[0 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(14, null, null);
            Type = 1534;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Gnolls.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[17];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[9 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(14, null, null);
            Type = 1544;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Gnolls.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[26];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[18 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(14, null, null);
            clothing2 = new SpriteExtraInfo(15, null, WhiteColored);
            Type = 1555;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (Races.Gnolls.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[35];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[27 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[36];
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
            clothing1 = new SpriteExtraInfo(14, null, null);
            clothing2 = new SpriteExtraInfo(15, null, WhiteColored);
            Type = 1574;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (Races.Gnolls.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[45];
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[54];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[37 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[46 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(14, null, null);
            Type = 1588;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (Races.Gnolls.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[55 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(14, null, null);
            Type = 1544;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Gnolls.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[71];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[63 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(14, null, null);
            Type = 1579;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.HasBelly)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[72];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[73];
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
            clothing1 = new SpriteExtraInfo(14, null, null);
            Type = 1579;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[74];
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
            clothing1 = new SpriteExtraInfo(14, null, null);
            clothing2 = new SpriteExtraInfo(7, null, null);
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Gnolls.oversize)
            {
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[75];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[77 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[75];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[76];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GnollSkin, actor.Unit.SkinColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GnollSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
        }
	}

    class Special1 : MainClothing
    {
        public Special1()
        {
            coversBreasts = false;
            OccupiesAllSlots = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(14, null, WhiteColored);//Breast element
            clothing2 = new SpriteExtraInfo(7, null, WhiteColored);//Bottom
            clothing3 = new SpriteExtraInfo(11, null, WhiteColored);//Top
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Gnolls.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[129];
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[114];
				
				if (actor.Unit.HasWeapon == false)
				{
					if (actor.IsAttacking) clothing3.GetSprite = (s) => SpriteDictionary.GnollClothes[120];
					else clothing3.GetSprite = (s) => SpriteDictionary.GnollClothes[118];
				}
				else if (actor.GetWeaponSprite() == 6)
				{
					clothing3.GetSprite = (s) => SpriteDictionary.GnollClothes[118];
				}
				else if (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 3 || actor.GetWeaponSprite() == 5 )
				{
					clothing3.GetSprite = (s) => SpriteDictionary.GnollClothes[120];
				}
				else
				{
					clothing3.GetSprite = (s) => SpriteDictionary.GnollClothes[119];
				}
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[121 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[114];
				
				if (actor.Unit.HasWeapon == false)
				{
					if (actor.IsAttacking) clothing3.GetSprite = (s) => SpriteDictionary.GnollClothes[120];
					else clothing3.GetSprite = (s) => SpriteDictionary.GnollClothes[118];
				}
				else if (actor.GetWeaponSprite() == 6)
				{
					clothing3.GetSprite = (s) => SpriteDictionary.GnollClothes[118];
				}
				else if (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 3 || actor.GetWeaponSprite() == 5 )
				{
					clothing3.GetSprite = (s) => SpriteDictionary.GnollClothes[120];
				}
				else
				{
					clothing3.GetSprite = (s) => SpriteDictionary.GnollClothes[119];
				}
            }
            else
            {
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[113];
				
				if (actor.Unit.HasWeapon == false)
				{
					if (actor.IsAttacking) clothing3.GetSprite = (s) => SpriteDictionary.GnollClothes[117];
					else clothing3.GetSprite = (s) => SpriteDictionary.GnollClothes[115];
				}
				else if (actor.GetWeaponSprite() == 6)
				{
					clothing3.GetSprite = (s) => SpriteDictionary.GnollClothes[115];
				}
				else if (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 3 || actor.GetWeaponSprite() == 5 )
				{
					clothing3.GetSprite = (s) => SpriteDictionary.GnollClothes[117];
				}
				else
				{
					clothing3.GetSprite = (s) => SpriteDictionary.GnollClothes[116];
				}
            }

            base.Configure(sprite, actor);
        }
    }
	
    class GnollRags : MainClothing
    {
        public GnollRags()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Rags[23];
            blocksDick = true;
            inFrontOfDick = 2;
            coversBreasts = false;
            Type = 207;
            OccupiesAllSlots = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(14, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(7, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BreastSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[87];
                else if (actor.Unit.BreastSize < 6)
                    clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[88];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[89];

                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[85];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[90];
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[86];
            }

            base.Configure(sprite, actor);
        }
	}

    class GnollLeader : MainClothing
    {
        public GnollLeader()
        {
            leaderOnly = true;
            coversBreasts = false;
            OccupiesAllSlots = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(14, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(7, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Gnolls.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[140];
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[131];
			}
            else if (actor.Unit.HasBreasts)
            {
                if (actor.HasBelly)
                {
					if (actor.Unit.BreastSize < 2)
						clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[135];
					else if (actor.Unit.BreastSize < 5)
						clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[137];
					else
						clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[139];

					clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[131];
				}
				else
				{
					if (actor.Unit.BreastSize < 3)
						clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[134];
					else if (actor.Unit.BreastSize < 6)
						clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[136];
					else
						clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[138];

					clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[131];
				}
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[130];
				
                if (actor.HasBelly)
                {
					clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[133];
				}
				else
				{
					clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[132];
				}
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
            clothing1 = new SpriteExtraInfo(7, null, null);
            clothing2 = new SpriteExtraInfo(6, null, null);
            Type = 1521;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[93];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[95];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[94];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
				clothing1.YOffset = 1 * .625f;
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[91];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[92];
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
            clothing1 = new SpriteExtraInfo(7, null, null);
            clothing2 = new SpriteExtraInfo(6, null, WhiteColored);
            Type = 1537;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[99];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[101];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[100];
            }
            else clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[98];

            if (actor.Unit.HasBreasts)
            {
				clothing1.YOffset = 1 * .625f;
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[96];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[97];
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
            clothing1 = new SpriteExtraInfo(7, null, null);
            clothing2 = new SpriteExtraInfo(6, null, WhiteColored);
            Type = 1540;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[102];

            if (actor.Unit.HasBreasts)
            {
				clothing1.YOffset = 1 * .625f;
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[96];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[97];
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
            clothing1 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(6, null, WhiteColored);
            Type = 61602;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[105];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[107];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[106];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
				clothing1.YOffset = 1 * .625f;
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[103];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[104];
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
            clothing1 = new SpriteExtraInfo(7, null, null);
            clothing2 = new SpriteExtraInfo(6, null, null);
            Type = 1521;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                clothing1.YOffset = -1 * .625f;
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[110];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[112];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.GnollClothes[111];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
				clothing1.YOffset = 1 * .625f;
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[108];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.GnollClothes[109];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
}
