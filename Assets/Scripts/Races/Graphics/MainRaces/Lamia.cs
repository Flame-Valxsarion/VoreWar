using System;
using System.Collections.Generic;
using UnityEngine;

class Lamia : DefaultRaceData
{
    readonly float xOffset = -1.875f; //3 pixels * 5/8
    readonly float yOffset = 3.75f;
    bool Selicia = false;
    public Lamia()
    {
        EyeTypes = 5;
        BodySizes = 4;
        SpecialAccessoryCount = 2;
        MouthTypes = 3;
        BodyAccentTypes1 = 3; // eyebrows
        BodyAccentTypes2 = 3; // Hoods
        BodyAccentTypes3 = 2; // head patterns

        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
        ExtraColors2 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.OldImpDark);
        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => FurryColor(s));
        Head = new SpriteExtraInfo(4, HeadSprite, null, (s) => FurryColor(s));
        BodyAccessory = new SpriteExtraInfo(3, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        BodyAccent = new SpriteExtraInfo(7, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.ExtraColor1));
        BodyAccent2 = new SpriteExtraInfo(7, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImpDark, s.Unit.ExtraColor2));
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        BodyAccent5 = new SpriteExtraInfo(5, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        BodyAccent6 = new SpriteExtraInfo(3, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor)); // Neck Scales
        BodyAccent7 = new SpriteExtraInfo(3, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor)); // Hood
        BodyAccent8 = new SpriteExtraInfo(5, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.ExtraColor2)); // Head Mark
        BodyAccent9 = new SpriteExtraInfo(5, BodyAccentSprite9, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.ExtraColor1)); // Body Scales
        BodyAccent10 = new SpriteExtraInfo(3, BodyAccentSprite10, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.ExtraColor1)); // hood inner Scales
        Mouth = new SpriteExtraInfo(7, MouthSprite, null, (s) => FurryColor(s));
        Hair = new SpriteExtraInfo(6, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Hair3 = new SpriteExtraInfo(9, HairSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Eyes = new SpriteExtraInfo(6, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = new SpriteExtraInfo(6, EyesSecondarySprite, WhiteColored);
        SecondaryAccessory = new SpriteExtraInfo(3, SecondaryAccessorySprite, WhiteColored);
        Belly = new SpriteExtraInfo(15, null, null, (s) => FurryColorInner(s));
        Weapon = new SpriteExtraInfo(1, WeaponSprite, WhiteColored);
        BackWeapon = new SpriteExtraInfo(0, BackWeaponSprite, WhiteColored);
        BodySize = new SpriteExtraInfo(4, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, (s) => FurryColorInner(s));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => FurryColor(s));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => FurryColorInner(s));

        FurCapable = true;

        ClothingShift = new Vector3(xOffset, yOffset, 0);
        AvoidedMainClothingTypes = 2;
        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            ClothingTypes.BikiniTop,
            ClothingTypes.BeltTop,
            ClothingTypes.StrapTop,
            ClothingTypes.Leotard,
            ClothingTypes.BlackTop,
            ClothingTypes.Rags,
            RaceSpecificClothing.Toga,
        };
        AllowedWaistTypes = new List<MainClothing>()
        {
            ClothingTypes.BikiniBottom,
            ClothingTypes.Loincloth,
        };

    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.Unit.Predator == false)
            Selicia = false;
        else
            Selicia = (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach)
                 || actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.womb)
                 || actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach2))
                 && (actor.GetCombinedStomachSize() == 15);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Body, xOffset, yOffset);
        AddOffset(Head, xOffset, yOffset);
        AddOffset(Mouth, xOffset, yOffset);
        AddOffset(Hair, xOffset, yOffset);
        AddOffset(Hair2, xOffset, yOffset);
        AddOffset(BodyAccent3, xOffset, yOffset);
        AddOffset(BodyAccent4, xOffset, yOffset);
        AddOffset(BodyAccent5, xOffset, yOffset);
        AddOffset(Weapon, xOffset + 1.25f, yOffset + 1.25f);
        AddOffset(BackWeapon, xOffset, yOffset);
        if (Selicia == false)
            AddOffset(Belly, xOffset, yOffset);
        AddOffset(Breasts, xOffset, yOffset);
        AddOffset(Dick, xOffset, yOffset + 2.5f);
        AddOffset(Balls, xOffset, yOffset + 2.5f);
        AddOffset(Eyes, 0, -1 * .625f);
        if (actor.Unit.GetGender() != Gender.Male)
            AddOffset(SecondaryAccessory, 0, -1 * .625f);
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return State.GameManager.SpriteDictionary.LamiaScales[24 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodySize) + (actor.Unit.HasBreasts ? 0 : 8)];
        }
        return State.GameManager.SpriteDictionary.Scylla[24 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodySize) + (actor.Unit.HasBreasts ? 0 : 8)];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return State.GameManager.SpriteDictionary.LamiaScales[0];
        }
        int eatingOffset = actor.IsEating ? 1 : 0;
        int genderOffset = actor.Unit.HasBreasts ? 0 : 2;
        return State.GameManager.SpriteDictionary.Lamia[18 + eatingOffset + genderOffset];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Lamia[0];

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            if (actor.IsOralVoring)
            {
                return State.GameManager.SpriteDictionary.LamiaScales[17];
            }
            return State.GameManager.SpriteDictionary.LamiaScales[8 + actor.Unit.MouthType];
        }

        if (BaseBody)
        {
            if (actor.Unit.HasBreasts)
                AddOffset(Mouth, 0, 0);
            else
                AddOffset(Mouth, 0, -.625f);
        }


        return actor.IsEating == false ? State.GameManager.SpriteDictionary.Mouths[actor.Unit.MouthType] : null;
    }

    protected override Sprite HairSprite(Actor_Unit actor) 
    {
        if (actor.Unit.Furry)
        {
            return null;
        }
        return State.GameManager.SpriteDictionary.Hair[actor.Unit.HairStyle];
    } 
    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return null;
        }
        if (actor.Unit.HairStyle == 1)
            return State.GameManager.SpriteDictionary.Hair[HairStyles];
        else if (actor.Unit.HairStyle == 2)
            return State.GameManager.SpriteDictionary.Hair[HairStyles + 1];
        else if (actor.Unit.HairStyle == 5)
            return State.GameManager.SpriteDictionary.Hair[HairStyles + 3];
        else if (actor.Unit.HairStyle == 6 || actor.Unit.HairStyle == 7)
            return State.GameManager.SpriteDictionary.Hair[HairStyles + 2];
        return null;
    }
    protected override Sprite HairSprite3(Actor_Unit actor)
    {
        if (!actor.Unit.Furry)
        {
            return null;
        }
        return State.GameManager.SpriteDictionary.LamiaScales[5 + actor.Unit.BodyAccentType1];

    }

    protected override Sprite EyesSprite(Actor_Unit actor) 
    {
        if (actor.Unit.Furry)
        {
            if (actor.Unit.EyeType <= 1)
            {
                return State.GameManager.SpriteDictionary.LamiaScales[22 + actor.Unit.EyeType];
            }
            return null;
        }
        return State.GameManager.SpriteDictionary.Lamia[5 + actor.Unit.EyeType % 3];
    }

    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return State.GameManager.SpriteDictionary.LamiaScales[11 + actor.Unit.EyeType];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (Selicia) return State.GameManager.SpriteDictionary.Lamia[16];
        return State.GameManager.SpriteDictionary.Lamia[1];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (Selicia) return State.GameManager.SpriteDictionary.Lamia[17];

        int bonusCap = 0;
        if (actor.Unit.Predator && actor.PredatorComponent.TailFullness > 0)
            bonusCap = 1 + actor.GetTailSize(2);
        if (Config.LamiaUseTailAsSecondBelly && actor.Unit.Predator)
            return State.GameManager.SpriteDictionary.Lamia[Math.Min(bonusCap + (actor.PredatorComponent?.Stomach2ndFullness > 0 ? (11 + actor.GetStomach2Size(2)) : 10), 13)];

        return State.GameManager.SpriteDictionary.Lamia[Math.Min(10 + actor.Unit.BodySize + bonusCap, 13)];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return null;
        }
        switch (actor.Unit.SpecialAccessoryType)
        {
            default:
            case 0:
                int eatingOffset = actor.IsEating ? 1 : 0;
                int genderOffset = actor.Unit.HasBreasts ? 0 : 2;
                return State.GameManager.SpriteDictionary.Lamia[22 + eatingOffset + genderOffset];
            case 1:
                return null;
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return null;
        }
        int eatingOffset = actor.IsEating ? 1 : 0;
        int genderOffset = actor.Unit.HasBreasts ? 0 : 2;
        return State.GameManager.SpriteDictionary.Lamia[26 + eatingOffset + genderOffset];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor)
    {
        if (!actor.Unit.Furry)
        {
            return null;
        }
        return State.GameManager.SpriteDictionary.LamiaScales[1];
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor)
    {
        if (!actor.Unit.Furry)
        {
            return null;
        }
        return State.GameManager.SpriteDictionary.LamiaScales[2 + actor.Unit.BodyAccentType2];
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor)
    {
        if (!actor.Unit.Furry || actor.Unit.BodyAccentType3 == 0)
        {
            return null;
        }
        return State.GameManager.SpriteDictionary.LamiaScales[16];
    }

    protected override Sprite BodyAccentSprite9(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return State.GameManager.SpriteDictionary.LamiaScales[40 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodySize) + (actor.Unit.HasBreasts ? 0 : 8)];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite10(Actor_Unit actor)
    {
        if (!actor.Unit.Furry)
        {
            return null;
        }
        return State.GameManager.SpriteDictionary.LamiaScales[19 + actor.Unit.BodyAccentType2];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (Selicia) return State.GameManager.SpriteDictionary.Lamia[15];
        if (!Config.LamiaUseTailAsSecondBelly)
        {
            if (actor.HasBelly)
            {
                belly.SetActive(true);

                if (actor.PredatorComponent.CombinedStomachFullness > 4)
                {
                    float extraCap = actor.PredatorComponent.CombinedStomachFullness - 4;
                    float xScale = Mathf.Min(1 + (extraCap / 5), 1.8f);
                    float yScale = Mathf.Min(1 + (extraCap / 40), 1.1f);
                    belly.transform.localScale = new Vector3(xScale, yScale, 1);
                }
                else
                    belly.transform.localScale = new Vector3(1, 1, 1);
                return State.GameManager.SpriteDictionary.Bellies[actor.GetCombinedStomachSize()];
            }
            else
            {
                return null;
            }
        }
        if (actor.HasBelly)
        {
            belly.SetActive(true);

            if (actor.PredatorComponent.VisibleFullness > 4)
            {
                float extraCap = actor.PredatorComponent.VisibleFullness - 4;
                float xScale = Mathf.Min(1 + (extraCap / 5), 1.8f);
                float yScale = Mathf.Min(1 + (extraCap / 40), 1.1f);
                belly.transform.localScale = new Vector3(xScale, yScale, 1);
            }
            else
                belly.transform.localScale = new Vector3(1, 1, 1);
            return State.GameManager.SpriteDictionary.Bellies[actor.GetStomachSize()];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            if (actor.Unit.HasDick == false)
                return null;

            int size = actor.Unit.DickSize;
            if (size >= 6)
            {
                size = 6;
            }
            if (actor.IsErect())
            {
                if (actor.HasBelly == false)
                {
                    Dick.layer = 18;
                    return State.GameManager.SpriteDictionary.LamiaScalesBits[size];
                }
                else
                {
                    Dick.layer = 12;
                    return State.GameManager.SpriteDictionary.LamiaScalesBits[size];
                }
            }

            Dick.layer = 9;
            return State.GameManager.SpriteDictionary.LamiaScalesBits[size];
        }
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            if (actor.HasBelly == false)
            {
                Dick.layer = 18;
                return State.GameManager.SpriteDictionary.ErectDicks[actor.Unit.DickSize];
            }
            else
            {
                Dick.layer = 12;
                return State.GameManager.SpriteDictionary.Dicks[actor.Unit.DickSize];
            }
        }

        Dick.layer = 9;
        return State.GameManager.SpriteDictionary.Dicks[actor.Unit.DickSize];
    }

    protected override Sprite BodySizeSprite(Actor_Unit actor)
    {
        if (Selicia)
        {
            return State.GameManager.SpriteDictionary.Lamia[14];
        }
        int bonusCap = 0;
        if (actor.Unit.Predator && actor.PredatorComponent.TailFullness > 0)
            bonusCap = 1 + actor.GetTailSize(2);

        if (Config.LamiaUseTailAsSecondBelly && actor.Unit.Predator)
        {
            if (actor.PredatorComponent.Stomach2ndFullness > 0 || actor.PredatorComponent.TailFullness > 0)
                return State.GameManager.SpriteDictionary.Lamia[Math.Min(2 + actor.GetStomach2Size(2) + bonusCap, 4)];
            return State.GameManager.SpriteDictionary.Lamia[1];
        }
        else
        {
            int effectiveSize = Math.Min(actor.Unit.BodySize + bonusCap, 3);
            if (effectiveSize == 0)
                return null;
            else
                return State.GameManager.SpriteDictionary.Lamia[1 + effectiveSize];
        }

    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return State.GameManager.SpriteDictionary.LamiaScalesBits[0];
        }
        var sprite = base.BallsSprite(actor);
        return sprite;
    }

    static ColorSwapPalette FurryColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, actor.Unit.AccessoryColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, actor.Unit.SkinColor);
    }

    static ColorSwapPalette FurryColorInner(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, actor.Unit.ExtraColor1);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, actor.Unit.SkinColor);
    }
}

