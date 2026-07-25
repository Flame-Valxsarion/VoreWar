using System;
using System.Collections.Generic;
using UnityEngine;

class EarthDryad : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.DryadSprites1;
    readonly Sprite[] Sprites2 = SpriteDictionary.DryadSprites2;
    readonly Sprite[] Sprites3 = SpriteDictionary.DryadSprites3;
    readonly Sprite[] Sprites4 = SpriteDictionary.DryadSprites4;
    readonly Sprite[] Sprites5 = SpriteDictionary.DryadSprites5;
    readonly Sprite[] Sprites6 = SpriteDictionary.HumansVoreSprites;

    bool oversize = false;

    public EarthDryad()
    {
        CanBeGender = new List<Gender>() { Gender.Female};
        BodySizes = 3;
        EyeTypes = 4;
        SpecialAccessoryCount = 4; //Pattern
        HairStyles = 5;
        MouthTypes = 5;
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DryadTrunk); // Horns
        ExtraColors2 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DryadLeaves); // Leaves
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DryadMudPattern); // Pattern
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DryadMud);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
        BodyAccentTypes1 = 6; // Horns
        BodyAccentTypes2 = 2; // Leaves On/Off
        BodyAccentTypes3 = 4; // eyebrows
        BeardStyles = 0;

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMud, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMud, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(7, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMudPattern, s.Unit.AccessoryColor)); // Pattern
        BodyAccent = new SpriteExtraInfo(22, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadTrunk, s.Unit.ExtraColor1)); // Horns
        BodyAccent2 = new SpriteExtraInfo(23, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadLeaves, s.Unit.ExtraColor2)); // Horn Leaves
        BodyAccent3 = new SpriteExtraInfo(8, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMud, s.Unit.SkinColor)); // Arms
        BodyAccent4 = null;
        BodyAccent5 = null; 
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        Mouth = new SpriteExtraInfo(7, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(21, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair3 = new SpriteExtraInfo(9, HairSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); // Eyebrows
        Beard = null;
        Eyes = new SpriteExtraInfo(8, EyesSprite, WhiteColored);
        SecondaryEyes = new SpriteExtraInfo(7, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMud, s.Unit.SkinColor));
        Weapon = null;
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMud, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMud, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = null;
        Balls = null;


        AllowedMainClothingTypes = new List<MainClothing>()
        {
        };
        AvoidedEyeTypes = 0;
        AllowedWaistTypes = new List<MainClothing>()
        {
        };
        ExtraMainClothing1Types = new List<MainClothing>()
        {
        };
    }


    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        int humanPartOffset = 23;
        AddOffset(Head, 0, humanPartOffset * .625f);
        AddOffset(Breasts, 0, humanPartOffset * .625f);
        AddOffset(SecondaryBreasts, 0, humanPartOffset * .625f);
        AddOffset(Belly, 0, humanPartOffset * .625f);
    }


    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.HairStyle = State.Rand.Next(HairStyles);
        unit.BodySize = State.Rand.Next(BodySizes);

        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
        unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3);
        unit.BodyAccentType4 = State.Rand.Next(BodyAccentTypes4);
    }

    internal override int DickSizes => 6;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        return Sprites[0 + actor.Unit.BodySize];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
        {
            if (actor.Unit.BodySize > 1)
            {
                return SpriteDictionary.HumansBodySprites2[4];
            }
            else
            {
                return SpriteDictionary.HumansBodySprites2[1];
            }
        }
        else if (actor.IsAttacking)
        {
            if (actor.Unit.BodySize > 1)
            {
                return SpriteDictionary.HumansBodySprites2[5];
            }
            else
            {
                return SpriteDictionary.HumansBodySprites2[2];
            }
        }
        else
        {
            if (actor.Unit.BodySize > 1)
            {
                return SpriteDictionary.HumansBodySprites2[3];
            }
            else
            {
                return SpriteDictionary.HumansBodySprites2[0];
            }
        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites3[0 + actor.Unit.SpecialAccessoryType + (actor.Unit.BodySize >= 1 ? 6 : 0)]; // Pattern
    protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites2[0 + actor.Unit.BodyAccentType1]; // Horns
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => actor.Unit.BodyAccentType2 == 1 ? null : Sprites2[6 + actor.Unit.BodyAccentType1]; // Horn Leaves


    protected override Sprite BodyAccentSprite3(Actor_Unit actor) => Sprites[20 + (actor.IsAttacking ? 6 : 0) + (actor.Unit.BodySize >= 1 ? 1 : 0)]; // Arms

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
            return null;
        else
            return Sprites5[12 + actor.Unit.MouthType];
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle <= 2)
        {
            return Sprites2[30 + 2 * (actor.Unit.HairStyle)];
        }
        if (actor.Unit.HairStyle == 3)
        {
            return Sprites2[18];
        }
        else
        {
            return Sprites2[20];
        }
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle <= 2)
        {
            return Sprites2[31 + 2 * (actor.Unit.HairStyle)];
        }
        if (actor.Unit.HairStyle == 3)
        {
            return Sprites2[19];
        }
        else
        {
            return Sprites2[21];
        }
    }

    protected override Sprite HairSprite3(Actor_Unit actor)
    {
        return Sprites5[18 + actor.Unit.BodyAccentType3];
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            return SpriteDictionary.HumansBodySprites3[69];
        }
        else
        {
            return Sprites5[0 + actor.Unit.EyeType];
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
            return Sprites5[6 + actor.Unit.EyeType];
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
                return Sprites6[105];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites6[104];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites6[103];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites6[102];
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

            return Sprites6[70 + size];
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
                return Sprites6[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return Sprites6[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return Sprites6[29];
            }

            if (leftSize > 28)
                leftSize = 28;

            return Sprites6[0 + leftSize];
        }
        else
        {
            return Sprites6[0 + actor.Unit.BreastSize];
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
                return Sprites6[63];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return Sprites6[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return Sprites6[61];
            }

            if (rightSize > 28)
                rightSize = 28;

            return Sprites6[32 + rightSize];
        }
        else
        {
            return Sprites6[32 + actor.Unit.BreastSize];
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
            return Sprites6[141];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return Sprites6[140];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return Sprites6[139];
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
            return Sprites6[Math.Min(112 + offset, 138)];
        return Sprites6[106 + size];
    }


}
