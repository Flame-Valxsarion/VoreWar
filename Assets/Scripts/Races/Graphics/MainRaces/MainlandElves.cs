using System;
using System.Collections.Generic;
using UnityEngine;

class MainlandElves : DefaultRaceData
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.HumansBodySprites1;
    readonly Sprite[] Sprites2 = State.GameManager.SpriteDictionary.HumansBodySprites2;
    readonly Sprite[] Sprites3 = State.GameManager.SpriteDictionary.HumansBodySprites3;
    readonly Sprite[] Sprites4 = State.GameManager.SpriteDictionary.HumansVoreSprites;
    readonly Sprite[] Sprites5 = State.GameManager.SpriteDictionary.HumansBodySprites4;

    bool oversize = false;

    public MainlandElves()
    {
        BodySizes = 3;
        EarTypes = 8;
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.RedSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
        BeardStyles = 0;
        BodyAccentTypes1 = 6; // eyebrows
        BodyAccentTypes2 = 3;
        SpecialAccessoryCount = 0;
        AccessoryColors = 0;
        HairStyles = 36;
        MouthTypes = 12;

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => FurryColor(s));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => FurryColor(s));
        BodyAccessory = new SpriteExtraInfo(22, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedSkin, s.Unit.SkinColor)); // Ears
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, WhiteColored); // Extra weapon sprite
        BodyAccent2 = null;
        BodyAccent3 = new SpriteExtraInfo(23, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedSkin, s.Unit.SkinColor)); // Ear Accessory
        BodyAccent5 = new SpriteExtraInfo(0, BodyAccentSprite5, WhiteColored); // Back weapon sprite;
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        Mouth = new SpriteExtraInfo(7, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(21, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair3 = new SpriteExtraInfo(9, HairSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); // Eyebrows
        Eyes = new SpriteExtraInfo(8, EyesSprite, WhiteColored);
        SecondaryEyes = new SpriteExtraInfo(7, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(14, null, null, (s) => FurryColor(s));
        Weapon = new SpriteExtraInfo(13, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => FurryColor(s));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => FurryColor(s));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, null, (s) => FurryColor(s));
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => FurryColor(s));


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
            new MaleTop3(),
            new MaleTop4(),
            new MaleTop5(),
            new MaleTop6(),
            new Uniform1(),
            new Uniform1WithHelmet(),
            new FemaleOnePiece1(),
            new FemaleOnePiece2(),
            new FemaleOnePiece3(),
            new FemaleOnePiece4(),
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
            new GenericBot6(),
            new BigLoin(),
            new Pants1(),
            new Pants2(),
            new Skirt(),
        };
        ExtraMainClothing1Types = new List<MainClothing>()
        {
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing50Spaced);
    }


    internal override void SetBaseOffsets(Actor_Unit actor)
    {

        if (actor.Unit.HasBreasts)
        {
            if (actor.Unit.BodySize > 1)
            {
                AddOffset(Balls, 0, 3 * .625f);
                AddOffset(Belly, 0, 1 * .625f);
            }
            else
            {
                AddOffset(Balls, 0, 3 * .625f);
                AddOffset(Belly, 0, 1 * .625f);
            }
        }
        else
        {
            if (actor.Unit.BodySize > 1)
            {
                AddOffset(Balls, 0, 1 * .625f);
                AddOffset(Belly, 0, 1 * .625f);
            }
            else
            {
                AddOffset(Balls, 0, 0);
                AddOffset(Belly, 0, 1 * .625f);
            }
        }

        if (actor.GetWeaponSprite() == 0 || actor.GetWeaponSprite() == 4 || actor.GetWeaponSprite() == 6)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, -1 * .625f, 0);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 1 * .625f, -1 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 0, -1 * .625f);
                }
            }
        }
        else if (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 3)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 0, -1 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 3 * .625f, -3 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 3 * .625f, -4 * .625f);
                }
            }
        }
        else if (actor.GetWeaponSprite() == 2)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, -1 * .625f, 2 * .625f);
                }
                else
                {
                    AddOffset(Weapon, -2 * .625f, 3 * .625f);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 0, 0);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
        }
        else if (actor.GetWeaponSprite() == 5 || actor.GetWeaponSprite() == 7)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 1 * .625f, -1 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 2 * .625f, -3 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 2 * .625f, -3 * .625f);
                }
            }
        }
    }


    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.EarType = State.Rand.Next(EarTypes);
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
    }

    internal override int DickSizes => 6;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return Sprites[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            return Sprites[0 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 1:
                return Sprites[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 2:
                return Sprites[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 3:
                return Sprites[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 4:
                return Sprites[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 5:
                return Sprites[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 6:
                return Sprites[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 7:
                return Sprites[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            default:
                return Sprites[0 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return Sprites2[4];
                }
                else
                {
                    return Sprites2[1];
                }
            }
            else
            {
                return Sprites2[7 + (actor.Unit.BodySize * 3)];
            }
        }
        else if (actor.IsAttacking)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return Sprites2[5];
                }
                else
                {
                    return Sprites2[2];
                }
            }
            else
            {
                return Sprites2[8 + (actor.Unit.BodySize * 3)];
            }
        }
        else
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return Sprites2[3];
                }
                else
                {
                    return Sprites2[0];
                }
            }
            else
            {
                return Sprites2[6 + (actor.Unit.BodySize * 3)];
            }

        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.MainlandElfParts[0 + actor.Unit.EarType]; //ears

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Extra weapon sprite
    {
        if (actor.Unit.HasWeapon == false)
        {
            return null;
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return null;
            case 1:
                return null;
            case 2:
                return null;
            case 3:
                return null;
            case 4:
                return null;
            case 5:
                return null;
            case 6:
                return null;
            case 7:
                return null;
            default:
                return null;
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Ears
    {
        if (actor.Unit.BodyAccentType1 == 0)
            return null;
        return State.GameManager.SpriteDictionary.MainlandElfParts[actor.Unit.EarType + actor.Unit.BodyAccentType2 * 8];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Extra weapon sprite
    {
        if (actor.Unit.HasWeapon == false)
        {
            return null;
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return null;
            case 1:
                return null;
            case 2:
                return null;
            case 3:
                return null;
            case 4:
                return State.GameManager.SpriteDictionary.MainlandElfParts[32];
            case 5:
                return null;
            case 6:
                return State.GameManager.SpriteDictionary.MainlandElfParts[33];
            case 7:
                return null;
            default:
                return null;
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
            return null;
        else
            return Sprites3[108 + actor.Unit.MouthType];
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        return Sprites2[71 + 2 * actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        return Sprites2[72 + 2 * actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite3(Actor_Unit actor)
    {
        return Sprites3[120 + actor.Unit.BodyAccentType1];
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            return Sprites2[69];
        }
        else
        {
            return Sprites3[24 + 4 * actor.Unit.EyeType + ((actor.IsAttacking || actor.IsEating) ? 0 : 2)];
        }
    }

    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            return null;
        }
        else
        {
            return Sprites3[25 + 4 * actor.Unit.EyeType + ((actor.IsAttacking || actor.IsEating) ? 0 : 2)];
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
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites4[105];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites4[104];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites4[103];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites4[102];
            }
            switch (size)
            {
                case 26:
                    AddOffset(Belly, 0, -14 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -17 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -20 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -25 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -27 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -32 * .625f);
                    break;
            }

            return Sprites4[70 + size];
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
                    return State.GameManager.SpriteDictionary.MainlandElfParts[24];
                case 1:
                    return State.GameManager.SpriteDictionary.MainlandElfParts[25];
                case 2:
                    return State.GameManager.SpriteDictionary.MainlandElfParts[28];
                case 3:
                    return State.GameManager.SpriteDictionary.MainlandElfParts[29];
                case 4:
                    return State.GameManager.SpriteDictionary.MainlandElfParts[26];
                case 5:
                    return State.GameManager.SpriteDictionary.MainlandElfParts[27];
                case 6:
                    return State.GameManager.SpriteDictionary.MainlandElfParts[30];
                case 7:
                    return State.GameManager.SpriteDictionary.MainlandElfParts[31];
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


    class GenericTop1 : MainClothing
    {
        public GenericTop1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[57];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60001;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.MainlandElves.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[56];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[0 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }
    class GenericTop2 : MainClothing
    {
        public GenericTop2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[58];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60002;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.MainlandElves.oversize)
            {
                clothing1.GetSprite = (s) => null;
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[8 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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

            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }
    class GenericTop3 : MainClothing
    {
        public GenericTop3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[60];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60003;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.MainlandElves.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[59];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[16 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }
    class GenericTop4 : MainClothing
    {
        public GenericTop4()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[62];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60004;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.MainlandElves.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[61];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[24 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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

            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }
    class GenericTop5 : MainClothing
    {
        public GenericTop5()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[64];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60005;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.MainlandElves.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[63];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[32 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }
    class GenericTop6 : MainClothing
    {
        public GenericTop6()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[66];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60006;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.MainlandElves.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[65];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[40 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }
    class GenericTop7 : MainClothing
    {
        public GenericTop7()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[68];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60007;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.MainlandElves.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[67];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[48 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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

            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            base.Configure(sprite, actor);
        }
    }

    class MaleTop : MainClothing
    {
        public MaleTop()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[5];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 60008;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[0];

            base.Configure(sprite, actor);
        }
    }

    class MaleTop2 : MainClothing
    {
        public MaleTop2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[5];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 60009;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.HasBelly)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[4];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[1 + actor.Unit.BodySize];
            }

            base.Configure(sprite, actor);
        }
    }

    class MaleTop3 : MainClothing
    {
        public MaleTop3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[11];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 60010;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[6];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class MaleTop4 : MainClothing
    {
        public MaleTop4()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[11];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 60011;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.HasBelly)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[10];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[7 + actor.Unit.BodySize];
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class MaleTop5 : MainClothing
    {
        public MaleTop5()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[14];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 60012;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.BodySize == 2)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[13];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[12];
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class MaleTop6 : MainClothing
    {
        public MaleTop6()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[16];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 60013;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[15];

            base.Configure(sprite, actor);
        }
    }

    class Uniform1 : MainClothing
    {
        public Uniform1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUniform1[42];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(18, null, null); // Chestplate
            clothing2 = new SpriteExtraInfo(12, null, null); // Cloth
            clothing3 = new SpriteExtraInfo(7, null, null); // Legs
            clothing4 = new SpriteExtraInfo(5, null, null); // arms
            Type = 60050;
            DiscardUsesPalettes = true;
            coversBreasts = false;
            OccupiesAllSlots = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BreastSize > 8)
                {
                    clothing1.GetSprite = (s) => null;
                }
                else
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[8 + actor.Unit.BreastSize];
                }
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[17];
            }

            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[2 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
                else clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[0 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            }
            else if (actor.GetWeaponSprite() == 0 || actor.GetWeaponSprite() == 4 || actor.GetWeaponSprite() == 6)
            {
                clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[0 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            }
            else if (actor.GetWeaponSprite() == 2 || actor.GetWeaponSprite() == 7)
            {
                clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[1 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            }
            else
            {
                clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[2 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            }
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[19];
            clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[3 + (actor.Unit.BodySize >= 2 ? 4 : 0)];

            base.Configure(sprite, actor);
        }
    }
    
    class Uniform1WithHelmet : MainClothing
    {
        public Uniform1WithHelmet()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUniform1[42];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(18, null, null); // Chestplate
            clothing2 = new SpriteExtraInfo(12, null, null); // Cloth
            clothing3 = new SpriteExtraInfo(7, null, null); // Legs
            clothing4 = new SpriteExtraInfo(5, null, null); // arms
            clothing5 = new SpriteExtraInfo(27, null, null); // arms
            Type = 60050;
            DiscardUsesPalettes = true;
            coversBreasts = false;
            OccupiesAllSlots = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BreastSize > 8)
                {
                    clothing1.GetSprite = (s) => null;
                }
                else
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[8 + actor.Unit.BreastSize];
                }
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[17];
            }

            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[2 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
                else clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[0 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            }
            else if (actor.GetWeaponSprite() == 0 || actor.GetWeaponSprite() == 4 || actor.GetWeaponSprite() == 6)
            {
                clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[0 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            }
            else if (actor.GetWeaponSprite() == 2 || actor.GetWeaponSprite() == 7)
            {
                clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[1 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            }
            else
            {
                clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[2 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            }
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[19];
            clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[3 + (actor.Unit.BodySize >= 2 ? 4 : 0)];
            clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.MainlandElfClothes[18];

            base.Configure(sprite, actor);
        }
    }

    class FemaleOnePiece1 : MainClothing
    {
        public FemaleOnePiece1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFOnePieces[81];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            clothing4 = new SpriteExtraInfo(15, null, null);
            Type = 60014;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.MainlandElves.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[51];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[43 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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
            if (actor.Unit.BodySize == 2)
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[42];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[41];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[40];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[39];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[38];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[37];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[21];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[20];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[19];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[18];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[17];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[16];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[15];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class FemaleOnePiece2 : MainClothing
    {
        public FemaleOnePiece2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFOnePieces[80];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            clothing4 = new SpriteExtraInfo(15, null, null);
            Type = 60015;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.MainlandElves.oversize)
            {
                clothing1.GetSprite = null;
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[52 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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
            if (actor.Unit.BodySize == 2)
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[36];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[35];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[34];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[33];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[32];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[31];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[30];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[29];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[28];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[27];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[26];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[25];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[24];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[23];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[22];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[14];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[13];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[12];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[11];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[10];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[9];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[8];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[7];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[6];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[5];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[4];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[3];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[2];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[1];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[0];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class FemaleOnePiece3 : MainClothing
    {
        public FemaleOnePiece3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFOnePieces[79];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            clothing4 = new SpriteExtraInfo(15, null, null);
            Type = 60016;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.MainlandElves.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[69];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[61 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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
            if (actor.Unit.BodySize == 2)
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[36];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[35];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[34];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[33];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[32];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[31];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[30];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[29];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[28];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[27];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[26];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[25];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[24];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[23];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[22];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[14];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[13];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[12];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[11];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[10];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[9];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[8];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[7];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[6];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[5];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[4];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[3];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[2];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[1];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[0];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class FemaleOnePiece4 : MainClothing
    {
        public FemaleOnePiece4()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFOnePieces[82];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            clothing4 = new SpriteExtraInfo(15, null, null);
            Type = 60017;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.MainlandElves.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[78];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[70 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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
            if (actor.Unit.BodySize == 2)
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[42];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[41];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[40];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[39];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[38];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[37];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[21];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[20];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[19];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[18];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[17];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[16];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[15];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericBot1 : MainClothing
    {
        public GenericBot1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUnderbottoms[6];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 60018;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 4)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[0 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[3 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot2 : MainClothing
    {
        public GenericBot2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUnderbottoms[13];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 60019;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 4)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[7 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[10 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot3 : MainClothing
    {
        public GenericBot3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUnderbottoms[26];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 60020;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 4)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[62];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[63];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[20 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[23 + actor.Unit.BodySize];
            }

            base.Configure(sprite, actor);
        }
    }

    class GenericBot4 : MainClothing
    {
        public GenericBot4()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUnderbottoms[39];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 60021;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 4)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[33 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[36 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot5 : MainClothing
    {
        public GenericBot5()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUnderbottoms[52];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 60022;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 4)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[46 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[49 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot6 : MainClothing
    {
        public GenericBot6()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUnderbottoms[59];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 60023;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 4)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[60];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[61];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[53 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[56 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class BigLoin : MainClothing
    {
        public BigLoin()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenBigLoin[12];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, null);
            Type = 60026;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenBigLoin[0 + 2 * actor.Unit.BodySize + (actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenBigLoin[6 + 2 * actor.Unit.BodySize + (actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class Pants1 : MainClothing
    {
        public Pants1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenPants[28];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(13, null, WhiteColored);
            Type = 60027;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.DickSize > 0)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[24 + (actor.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)];
                }
                else clothing2.GetSprite = null;

                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[0 + 2 * actor.Unit.BodySize + (actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[6 + 2 * actor.Unit.BodySize + (actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[25 + (actor.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)];
            }

            base.Configure(sprite, actor);
        }
    }

    class Pants2 : MainClothing
    {
        public Pants2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenPants[33];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, null);
            clothing2 = new SpriteExtraInfo(13, null, null);
            Type = 60028;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.DickSize > 0)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[29 + (actor.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)];
                }
                else clothing2.GetSprite = null;

                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[12 + 2 * actor.Unit.BodySize + (actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[18 + 2 * actor.Unit.BodySize + (actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenPants[30 + (actor.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class Skirt : MainClothing
    {
        public Skirt()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenSkirt[6];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, null);
            Type = 60029;
            femaleOnly = true;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenSkirt[0 + 2 * actor.Unit.BodySize + (actor.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    static ColorSwapPalette FurryColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedSkin, actor.Unit.SkinColor);
    }





































}
