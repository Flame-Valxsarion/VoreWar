using System;
using System.Collections.Generic;
using TaurusClothes;
using UnityEngine;

class Taurus : DefaultRaceData
{
    public Taurus()
    {
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);
        BodySizes = 0;
        MouthTypes = 0;

        BeardStyles = 3;

        EyeTypes = 4;
        HairStyles = 11;
        FurCapable = true;



        Body = new SpriteExtraInfo(2, BodySprite, null, FurryColor);
        Head = new SpriteExtraInfo(3, HeadSprite, null, FurryColor);
        BodyAccessory = new SpriteExtraInfo(2, AccessorySprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        BodyAccent2 = new SpriteExtraInfo(5, BodyAccentSprite2, WhiteColored);
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        BodyAccent5 = new SpriteExtraInfo(5, BodyAccentSprite5, WhiteColored);
        Mouth = new SpriteExtraInfo(4, MouthSprite, null, FurryColor);
        Hair = new SpriteExtraInfo(6, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Hair3 = new SpriteExtraInfo(7, HairSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Beard = new SpriteExtraInfo(4, BeardSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Eyes = new SpriteExtraInfo(4, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = new SpriteExtraInfo(4, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        SecondaryAccessory = new SpriteExtraInfo(4, SecondaryAccessorySprite, WhiteColored);
        Belly = new SpriteExtraInfo(15, null, null, FurryBellyColor);
        Weapon = new SpriteExtraInfo(12, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = new SpriteExtraInfo(3, BodySizeSprite, null, FurryBellyColor);
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, FurryBellyColor);
        BreastShadow = null;
        Dick = new SpriteExtraInfo(9, DickSprite, null, FurryColor);
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => FurryColor(s));

        AvoidedMainClothingTypes = 0;
        //RestrictedClothingTypes = 0;
        //clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            TaurusClothingTypes.Overall,
            TaurusClothingTypes.Shirt,
            TaurusClothingTypes.Bikini,
            TaurusClothingTypes.LeaderOutfit,
            TaurusClothingTypes.HolidayOutfit,
        };
        AllowedWaistTypes = new List<MainClothing>()
        {
            TaurusClothingTypes.BikiniBottom,
            TaurusClothingTypes.Loincloth,
            TaurusClothingTypes.OverallBottom
        };
        AllowedClothingHatTypes = new List<ClothingAccessory>()
        {
            TaurusClothingTypes.Hat,
            TaurusClothingTypes.HolidayHat,
        };
        AllowedClothingAccessoryTypes = new List<ClothingAccessory>()
        {
            TaurusClothingTypes.CowBell,
        };

    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        if (unit.Type == UnitType.Leader)
        {
            unit.ClothingHatType = 1;
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(TaurusClothingTypes.LeaderOutfit);
        }
        else
            unit.ClothingHatType = 0;

        if (unit.ClothingType == 5)
            unit.ClothingHatType = 2;

        if (unit.HasDick && unit.HasBreasts)
        {
            if (Config.HermsOnlyUseFemaleHair)
                unit.HairStyle = State.Rand.Next(7);
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
                unit.HairStyle = 7 + State.Rand.Next(4);
            }
            else
            {
                unit.HairStyle = State.Rand.Next(7);
            }
        }


    }

    internal override int BreastSizes => 5;
    internal override int DickSizes => 5;

    ColorSwapPalette FurryColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, actor.Unit.AccessoryColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, actor.Unit.SkinColor);
    }

    ColorSwapPalette FurryBellyColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
            return ColorPaletteMap.FurryBellySwap;
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, actor.Unit.SkinColor);
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        int sprite = actor.IsAttacking ? 1 : 0;
        if (actor.GetWeaponSprite() == 2)
            sprite += 2;
        if (actor.Unit.HasBreasts == false)
            sprite += 9;

        return SpriteDictionary.Cows[sprite];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.GetWeaponSprite() == 2)
        {
            if ((Config.FurryHandsAndFeet || actor.Unit.Furry) == false)
                return SpriteDictionary.Cows[20];
            return SpriteDictionary.Cows[actor.Unit.HasBreasts ? 21 : 22];
        }
        if ((Config.FurryHandsAndFeet || actor.Unit.Furry) == false)
            return null;
        int sprite = actor.IsAttacking ? 5 : 4;
        if (actor.Unit.HasBreasts == false)
            sprite += 9;
        return SpriteDictionary.Cows[sprite];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if ((Config.FurryHandsAndFeet || actor.Unit.Furry) == false || Config.FurryFluff == false)
            return null;
        if (actor.GetWeaponSprite() == 2)
            return SpriteDictionary.Cows[actor.Unit.HasBreasts ? 23 : 24];
        int sprite = actor.IsAttacking ? 7 : 6;
        if (actor.Unit.HasBreasts == false)
            sprite += 9;
        return SpriteDictionary.Cows[sprite];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) => SpriteDictionary.Cows[18];

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) => SpriteDictionary.Cows[12];

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) => (Config.FurryHandsAndFeet || actor.Unit.Furry || Config.FurryFluff == false) ? SpriteDictionary.Cows[19] : null;

    protected override Sprite AccessorySprite(Actor_Unit actor) => SpriteDictionary.Cows[3];
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => SpriteDictionary.Cows[25];

    protected override Sprite BodySizeSprite(Actor_Unit actor) => SpriteDictionary.Cows[actor.Unit.HasBreasts ? 8 : 17];

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.SquishedBreasts)
            return SpriteDictionary.Cows[Math.Max(114 + actor.Unit.BreastSize, 115)];
        return SpriteDictionary.Cows[110 + actor.Unit.BreastSize];
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect() && (Config.CockVoreHidesClothes == true || actor.Unit.ClothingType2 != 2)) // disables erections appearing through Loincloth
        {
            if (actor.PredatorComponent?.VisibleFullness < .5f)
            {
                Dick.layer = 18;
                if (actor.Unit.DickSize == 4)
                    return SpriteDictionary.Cows[123];
                else if (actor.Unit.DickSize == 3)
                    return SpriteDictionary.Cows[121];
                return SpriteDictionary.Cows[29 + actor.Unit.DickSize];
            }
            else
            {
                Dick.layer = 12;
                if (actor.Unit.DickSize == 4)
                    return SpriteDictionary.Cows[122];
                else if (actor.Unit.DickSize == 3)
                    return SpriteDictionary.Cows[120];
                return SpriteDictionary.Cows[26 + actor.Unit.DickSize];
            }
        }

        Dick.layer = 9;
        if (actor.Unit.DickSize == 4)
            return SpriteDictionary.Cows[122];
        else if (actor.Unit.DickSize == 3)
            return SpriteDictionary.Cows[119];
        return SpriteDictionary.Cows[26 + actor.Unit.DickSize];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        int sprite = 32;
        if (actor.Unit.Furry)
            sprite += 4;
        if (actor.Unit.HasBreasts)
            sprite += 2;
        if (actor.IsOralVoring)
            sprite += 1;
        return SpriteDictionary.Cows[sprite];

    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            int weaponSprite = actor.GetWeaponSprite();
            switch (weaponSprite)
            {
                case 1:
                    if (actor.Unit.DickSize < 0)
                        AddOffset(Weapon, 0, 0);
                    else
                        AddOffset(Weapon, 5 * .625f, 1 * .625f);
                    break;
                case 3:
                    if (actor.Unit.DickSize < 0)
                        AddOffset(Weapon, 0, 11 * .625f);
                    else
                        AddOffset(Weapon, 5 * .625f, 12 * .625f);
                    break;
                case 5:
                    AddOffset(Weapon, 2 * .625f, 0);
                    break;
                case 7:
                    AddOffset(Weapon, 11 * .625f, 0);
                    break;
                default:
                    AddOffset(Weapon, 0, 0);
                    break;
            }

            return SpriteDictionary.Cows[40 + actor.GetWeaponSprite()];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            if (actor.Unit.HasBreasts)
                return SpriteDictionary.Cows[63];
            return SpriteDictionary.Cows[76];
        }
        int sprite = 48;
        sprite += 3 * actor.Unit.EyeType;
        if (actor.Unit.HasBreasts == false)
        {
            sprite += 16;
            if (sprite > 80)
                sprite = 80;
        }

        return SpriteDictionary.Cows[sprite];
    }

    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return null;
        }
        int sprite = 50;
        sprite += 3 * actor.Unit.EyeType;
        if (actor.Unit.HasBreasts == false)
        {
            sprite += 16;
            if (sprite > 82)
                sprite = 82;
        }

        return SpriteDictionary.Cows[sprite];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return null;
        }
        int sprite = 49;
        sprite += 3 * actor.Unit.EyeType;
        if (actor.Unit.HasBreasts == false)
        {
            sprite += 16;
            if (sprite > 81)
                sprite = 81;
        }

        return SpriteDictionary.Cows[sprite];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.SetActive(true);

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(11, .95f) == 11)
            {
                belly.transform.localScale = new Vector3(1, 1, 1);
                return SpriteDictionary.CowsSeliciaBelly[1];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(11, .95f) == 11)
            {
                belly.transform.localScale = new Vector3(1, 1, 1);
                return SpriteDictionary.CowsSeliciaBelly[0];
            }

            if (actor.PredatorComponent.VisibleFullness > 4)
            {
                float extraCap = actor.PredatorComponent.VisibleFullness - 4;
                float xScale = Mathf.Min(1 + (extraCap / 5), 1.8f);
                float yScale = Mathf.Min(1 + (extraCap / 40), 1.1f);
                belly.transform.localScale = new Vector3(xScale, yScale, 1);
            }
            else
                belly.transform.localScale = new Vector3(1, 1, 1);
            return SpriteDictionary.Cows[98 + actor.GetStomachSize(11, .95f)];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite HairSprite(Actor_Unit actor) => SpriteDictionary.Cows[77 + actor.Unit.HairStyle];
    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle <= 6)
            return SpriteDictionary.Cows[90 + actor.Unit.HairStyle];
        return null;
    }
    protected override Sprite HairSprite3(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle == 6)
            return SpriteDictionary.Cows[97];
        return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        //if (actor.Unit.Furry && Config.FurryGenitals)
        //{
        //    int size = actor.Unit.DickSize;
        //    int offset = (int)((actor.PredatorComponent?.BallsFullness ?? 0) * 3);
        //    if (offset > 0)
        //        return SpriteDictionary.FurryDicks[Math.Min(12 + offset, 23)];
        //    return SpriteDictionary.FurryDicks[size];
        //}

        int baseSize = 2;
        if (actor.Unit.DickSize == 4)
            baseSize = 8;
        int ballOffset = actor.GetBallSize(21, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 21)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return State.GameManager.SpriteDictionary.Balls[24];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 21)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return State.GameManager.SpriteDictionary.Balls[23];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 20)
        {
            AddOffset(Balls, 0, -15 * .625f);
            return State.GameManager.SpriteDictionary.Balls[22];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 19)
        {
            AddOffset(Balls, 0, -14 * .625f);
            return State.GameManager.SpriteDictionary.Balls[21];
        }
        int combined = Math.Min(baseSize + ballOffset, 20);
        if (combined == 21)
            AddOffset(Balls, 0, -14 * .625f);
        else if (combined == 20)
            AddOffset(Balls, 0, -12 * .625f);
        else if (combined >= 17 && combined <= 19)
            AddOffset(Balls, 0, -8 * .625f);
        if (ballOffset > 0)
        {
            return State.GameManager.SpriteDictionary.Balls[combined];
        }

        return State.GameManager.SpriteDictionary.Balls[baseSize];

    }

    protected override Sprite BeardSprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
            return null;
        if (actor.Unit.BeardStyle > 0)
            return SpriteDictionary.Cows[87 + actor.Unit.BeardStyle];
        return null;
    }

}

