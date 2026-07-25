using System;
using System.Collections.Generic;
using UnityEngine;

class FungalDryad : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.DryadSprites1;
    readonly Sprite[] Sprites2 = SpriteDictionary.DryadSprites2;
    readonly Sprite[] Sprites3 = SpriteDictionary.DryadSprites3;
    readonly Sprite[] Sprites4 = SpriteDictionary.DryadSprites4;
    readonly Sprite[] Sprites5 = SpriteDictionary.DryadSprites5;
    readonly Sprite[] Sprites6 = State.GameManager.SpriteDictionary.NiplessVoreSprites;

    bool oversize = false;

    public FungalDryad()
    {
        CanBeGender = new List<Gender>() { Gender.Female};
        BodySizes = 3;
        SpecialAccessoryCount = 3; //Mushroom Hats
        HairStyles = 6;
        MouthTypes = 9;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DryadMushroom); // Trunk
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DryadMushroom);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DryadMushroom);
        BodyAccentTypes1 = 3; // Mushroom Damage
        BodyAccentTypes2 = 2; // Spots Y/N
        BodyAccentTypes3 = 3; // Spot Type
        BodyAccentTypes4 = 6; // Under Mushrooms
        BeardStyles = 0;

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMushroom, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMushroom, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(22, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMushroom, s.Unit.AccessoryColor)); // Mushroom Hat
        BodyAccent = new SpriteExtraInfo(0, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMushroom, s.Unit.AccessoryColor)); // Mushroom Hat Bottom
        BodyAccent2 = new SpriteExtraInfo(23, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMushroom, s.Unit.ExtraColor1)); // Mushroom Hat Spots
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMushroom, s.Unit.SkinColor)); // Right Arm
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMushroom, s.Unit.SkinColor)); // Left Arm
        BodyAccent5 = new SpriteExtraInfo(10, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMushroom, s.Unit.AccessoryColor)); // Under Mushroom
        BodyAccent6 = new SpriteExtraInfo(10, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMushroom, s.Unit.AccessoryColor)); // Under Mushroom Color;
        BodyAccent7 = new SpriteExtraInfo(10, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMushroom, s.Unit.ExtraColor1)); // Under Mushroom Spots;
        BodyAccent8 = new SpriteExtraInfo(17, BodyAccentSprite8, WhiteColored); // Right Arm Swipe;
        BodyAccent9 = new SpriteExtraInfo(17, BodyAccentSprite9, WhiteColored); // Left Arm Swipe;
        Mouth = new SpriteExtraInfo(7, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(21, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.SkinColor));
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.SkinColor));
        Hair3 = null; // Eyebrows
        Beard = null;
        Eyes = null;
        SecondaryEyes = new SpriteExtraInfo(7, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMushroom, s.Unit.SkinColor));
        Weapon = null;
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMushroom, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DryadMushroom, s.Unit.SkinColor));
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
        AddOffset(Breasts, 0, humanPartOffset * .625f);
        AddOffset(SecondaryBreasts, 0, humanPartOffset * .625f);
        AddOffset(SecondaryEyes, 0, humanPartOffset * .625f);
        AddOffset(Belly, 0, humanPartOffset * .625f);
    }


    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.HairStyle = State.Rand.Next(HairStyles);
        unit.BodySize = State.Rand.Next(BodySizes);

        unit.SpecialAccessoryType = State.Rand.Next(SpecialAccessoryCount);
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
        unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3);
        unit.BodyAccentType4 = State.Rand.Next(BodyAccentTypes4);
    }

    internal override int DickSizes => 6;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        return Sprites[12 + actor.Unit.BodySize];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
        {
            return Sprites5[5];
        }
        return Sprites5[4];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // Mushrooom Hat Front
    {
        switch (actor.Unit.SpecialAccessoryType)
        {
            case 0:
                return Sprites3[18 + actor.Unit.BodyAccentType1];
            case 1:
                return Sprites3[21 + actor.Unit.BodyAccentType1];
            case 2:
                return Sprites3[4];
            default:
                return null;
        }
    } 
    protected override Sprite BodyAccentSprite(Actor_Unit actor)  // Mushrooom Hat Front
    {
        switch (actor.Unit.SpecialAccessoryType)
        {
            case 0:
                return Sprites3[24 + actor.Unit.BodyAccentType1];
            case 1:
                return Sprites3[27 + actor.Unit.BodyAccentType1];
            case 2:
                return Sprites3[5];
            default:
                return null;
        }
    }
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Spots
    {
        if (actor.Unit.BodyAccentType2 == 0)
        {
            return null;
        }
        switch (actor.Unit.SpecialAccessoryType)
        {
            case 0:
                return Sprites3[30 + actor.Unit.BodyAccentType3];
            case 1:
                return Sprites3[33 + actor.Unit.BodyAccentType3];
            case 2:
                return Sprites3[10 + actor.Unit.BodyAccentType3 + (actor.Unit.BodyAccentType3 == 2 ? 6 : 0)];
            default:
                return null;
        }
    }


    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Right Arm
    {
        if (actor.IsAttacking)
        {
            BodyAccent4.layer = 16;
        }
        else
        {
            BodyAccent4.layer = 5;
        }
        return Sprites[19 + (actor.IsAttacking ? 6 : 0) + (actor.Unit.BodySize >= 1 ? 6 : 0)];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Right Arm
    {
        if (actor.IsAttacking)
        {
            BodyAccent4.layer = 15;
        }
        else 
        {
            BodyAccent4.layer = 5;
        }
        return Sprites[18 + (actor.IsAttacking ? 6 : 0) + (actor.Unit.BodySize >= 1 ? 6 : 0)];
    }


    protected override Sprite BodyAccentSprite5(Actor_Unit actor) => Sprites4[18 + actor.Unit.BodyAccentType4]; // Under Mushroom
    protected override Sprite BodyAccentSprite6(Actor_Unit actor) => Sprites4[24 + actor.Unit.BodyAccentType4]; // Under Mushroom Color
    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Under Mushroom Spots
    {
        if (actor.Unit.BodyAccentType2 == 0)
        {
            return null;
        }
        return Sprites4[30 + actor.Unit.BodyAccentType4];
    }  
    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // Right Arm Swipe
    {
        if (actor.IsAttacking)
        {
            return Sprites5[11];
        }
        return null;
    }
    protected override Sprite BodyAccentSprite9(Actor_Unit actor) // Left Arm Swipe
    {
        if (actor.IsAttacking)
        {
            return Sprites5[10];
        }
        return null;
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
            return null;
        else if (actor.Unit.MouthType <= 4)
            return Sprites5[12 + actor.Unit.MouthType];
        else 
            return Sprites5[24 + actor.Unit.MouthType - 5];
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 2)
        {
            return null;
        }
        return Sprites2[24 + actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        return null;
    }

    protected override Sprite HairSprite3(Actor_Unit actor)
    {
        return SpriteDictionary.HumansBodySprites3[120 + actor.Unit.BodyAccentType3];
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            return Sprites2[69];
        }
        else
        {
            return SpriteDictionary.HumansBodySprites3[24 + 4 * actor.Unit.EyeType + ((actor.IsAttacking || actor.IsEating) ? 0 : 2)];
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
            return SpriteDictionary.HumansBodySprites3[25 + 4 * actor.Unit.EyeType + ((actor.IsAttacking || actor.IsEating) ? 0 : 2)];
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
