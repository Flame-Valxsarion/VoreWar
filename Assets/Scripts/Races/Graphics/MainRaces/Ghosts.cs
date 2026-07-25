using System;
using System.Collections.Generic;
using UnityEngine;

class Ghosts : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Ghosts1;
    readonly Sprite[] Sprites2 = SpriteDictionary.Ghosts2;
    readonly Sprite[] Sprites3 = SpriteDictionary.Ghosts3;
    readonly Sprite[] Sprites4 = SpriteDictionary.GhostsVoreSprites;
    readonly Sprite[] Sprites5 = SpriteDictionary.HumansBodySprites4;

    bool oversize = false;

    public Ghosts()
    {
        BodySizes = 6;
        SpecialAccessoryCount = 0;
        HairStyles = 9;
        MouthTypes = 6;
        AccessoryColors = 0;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.GhostSkin);
        BodyAccentTypes1 = 6; // eyebrows
        EyeTypes = 7;

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GhostSkin, s.Unit.SkinColor)); //Upper Body
        Head = null; 
        BodyAccessory = new SpriteExtraInfo(7, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GhostSkin, s.Unit.SkinColor)); // LowerBody
        BodyAccent = null;
        BodyAccent2 = null;
        BodyAccent3 = null;
        BodyAccent4 = null;
        BodyAccent5 = null;
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        Mouth = new SpriteExtraInfo(7, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GhostSkin, s.Unit.SkinColor));
        Hair = new SpriteExtraInfo(21, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GhostSkin, s.Unit.SkinColor));
        Hair2 = null;
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(8, EyesSprite, WhiteColored);
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GhostSkin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(6, WeaponSprite, null, (s) => WeaponColor(s));
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GhostSkin, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GhostSkin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GhostSkin, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GhostSkin, s.Unit.SkinColor));


        AllowedMainClothingTypes = new List<MainClothing>()
        {
        };
        AvoidedMainClothingTypes = 3;
        AvoidedEyeTypes = 0;
        AllowedWaistTypes = new List<MainClothing>()
        {
        };
        ExtraMainClothing1Types = new List<MainClothing>()
        {
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing50Spaced);
    }


    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Dick, 0, 13 * .625f);
    }


    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

    }

    internal override int DickSizes => 6;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsAttacking) return Sprites[2 + (actor.Unit.BodySize >= 3 ? 4 : 0)];
        if (actor.IsEating) return Sprites[1 + (actor.Unit.BodySize >= 3 ? 4 : 0)];
        return Sprites[0 + (actor.Unit.BodySize >= 3 ? 4 : 0)];
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

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites[14 + actor.Unit.BodySize]; //ears

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
                return Sprites3[137];
            case 5:
                return null;
            case 6:
                return Sprites3[140];
            case 7:
                return null;
            default:
                return null;
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Extra weapon sprite 2
    {
        if (actor.Unit.HasWeapon == false)
        {
            return null;
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                BodyAccent2.layer = 22;
                return Sprites3[143];
            case 1:
                BodyAccent2.layer = 22;
                return Sprites3[143];
            case 2:
                BodyAccent2.layer = 22;
                return Sprites3[143];
            case 3:
                BodyAccent2.layer = 22;
                return Sprites3[143];
            case 4:
                BodyAccent2.layer = 0;
                return Sprites3[142];
            case 5:
                BodyAccent2.layer = 0;
                return Sprites3[142];
            case 6:
                BodyAccent2.layer = 0;
                return Sprites3[142];
            case 7:
                BodyAccent2.layer = 0;
                return Sprites3[142];
            default:
                return null;
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites3[1 + 2 * actor.Unit.MouthType];
        else
            return Sprites3[0 + 2 * actor.Unit.MouthType];
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        return Sprites2[2 + actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        return Sprites2[72 + 2 * actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite3(Actor_Unit actor)
    {
        return Sprites3[120 + actor.Unit.BodyAccentType1];
    }

    protected override Sprite BeardSprite(Actor_Unit actor)
    {
        if (actor.Unit.BeardStyle == 6)
        {
            return null;
        }
        else
        {
            return Sprites3[126 + actor.Unit.BeardStyle];
        }
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        return Sprites2[11 + actor.Unit.EyeType];
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
                    return Sprites[13];
                case 1:
                    return Sprites[7];
                case 2:
                    return Sprites[12];
                case 3:
                    return Sprites[3];
                case 4:
                    return Sprites[9];
                case 5:
                    return Sprites[10];
                case 6:
                    return Sprites[8];
                case 7:
                    return Sprites[11];
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

    static ColorSwapPalette WeaponColor(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GhostSkin, actor.Unit.SkinColor);
                case 1:
                    return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GhostSkin, actor.Unit.SkinColor);
                case 2:
                    return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GhostSkin, actor.Unit.SkinColor);
                case 3:
                    return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GhostSkin, actor.Unit.SkinColor);
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
        else
        {
            return null;
        }
    }
}
