using System;
using System.Collections.Generic;
using UnityEngine;

class Succubi : DefaultRaceData
{
    public Succubi()
    {
        WeightGainDisabled = true;
        SpecialAccessoryCount = 3;
        EyeTypes = 3;
        MouthTypes = 3;
        HairStyles = 4;

        BodySizes = 0;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.OldImp);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.OldImpDark);
        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImp, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(3, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImp, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImpDark, s.Unit.AccessoryColor));
        BodyAccent = new SpriteExtraInfo(5, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpRedKey, s.Unit.AccessoryColor));
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImpDark, s.Unit.AccessoryColor));
        BodyAccent3 = new SpriteExtraInfo(0, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImpDark, s.Unit.AccessoryColor));
        BodyAccent4 = new SpriteExtraInfo(1, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HairRedKeyStrict, s.Unit.HairColor));
        BodyAccent5 = new SpriteExtraInfo(1, BodyAccentSprite5, WhiteColored);
        Mouth = new SpriteExtraInfo(5, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImp, s.Unit.SkinColor));
        Hair = new SpriteExtraInfo(6, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HairRedKeyStrict, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(4, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HairRedKeyStrict, s.Unit.HairColor));
        Hair3 = new SpriteExtraInfo(0, HairSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HairRedKeyStrict, s.Unit.HairColor));
        Eyes = new SpriteExtraInfo(5, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HairRedKeyStrict, s.Unit.HairColor));
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(13, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImp, s.Unit.SkinColor));
        Weapon = null;
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(15, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImp, s.Unit.SkinColor));
        BreastShadow = new SpriteExtraInfo(16, BreastsShadowSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpRedKey, s.Unit.AccessoryColor));
        ClothingShift = new Vector3(0, 32 * .625f, 0);
        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImp, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImp, s.Unit.SkinColor));


        AvoidedMainClothingTypes = 0;

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            ClothingTypes.BikiniTop,
            ClothingTypes.BeltTop,
            ClothingTypes.StrapTop,
            ClothingTypes.BlackTop,
            RaceSpecificClothing.SuccubusDress,
            RaceSpecificClothing.SuccubusLeotard
        };


        AllowedWaistTypes = new List<MainClothing>()
        {
            ClothingTypes.BikiniBottom,
            ClothingTypes.Loincloth,
        };
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Dick, 0, 30 * .625f);
        AddOffset(Balls, 0, 33 * .625f);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.BodySize = 1;
        unit.ClothingColor2 = State.Rand.Next(8);
    }
    internal override int BreastSizes => 4;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsUnbirthing || actor.IsAnalVoring)
        {
            return SpriteDictionary.Succubi[4];
        }
        else
        {
            //int sizeOffset = actor.PredatorComponent?.VisibleFullness > .25f ? 1 : 0;
            int sizeOffset = 1;
            int attackingOffset = actor.IsAttacking ? 2 : 0;
            return SpriteDictionary.Succubi[sizeOffset + attackingOffset];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.IsUnbirthing || actor.IsAnalVoring)
        {
            return SpriteDictionary.Succubi[9];
        }
        else
        {
            //int sizeOffset = actor.PredatorComponent?.VisibleFullness > .25f ? 1 : 0;
            int sizeOffset = 1;
            int attackingOffset = actor.IsAttacking ? 2 : 0;
            return SpriteDictionary.Succubi[5 + sizeOffset + attackingOffset];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor) => SpriteDictionary.Succubi[actor.IsOralVoring ? 21 : 20];

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => SpriteDictionary.Succubi[22];
    protected override Sprite BodyAccentSprite4(Actor_Unit actor) => SpriteDictionary.Succubi[23];

    protected override Sprite AccessorySprite(Actor_Unit actor) => SpriteDictionary.Succubi[24 + actor.Unit.SpecialAccessoryType];

    protected override Sprite EyesSprite(Actor_Unit actor) => SpriteDictionary.Succubi[27 + actor.Unit.EyeType];
    protected override Sprite MouthSprite(Actor_Unit actor) => actor.IsOralVoring ? null : SpriteDictionary.Succubi[30 + actor.Unit.MouthType];

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        int sizeOffset = actor.PredatorComponent?.TailFullness > 0 ? (1 + actor.GetTailSize(2, 1)) : 0;
        if (actor.IsTailVoring)
        {
            return SpriteDictionary.Succubi[37 + sizeOffset];
        }
        else
        {
            return SpriteDictionary.Succubi[33 + sizeOffset];
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        int sizeOffset = actor.PredatorComponent?.TailFullness > 0 ? 1 : 0;
        if (actor.IsTailVoring)
        {
            return SpriteDictionary.Succubi[41 + sizeOffset];
        }
        return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.SetActive(true);

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(15, 1) == 15)
            {
                belly.transform.localScale = new Vector3(1, 1, 1);
                return SpriteDictionary.Succubi[88];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(15, 1) == 15)
            {

                if (actor.GetStomachSize(15, 0.7f) == 15)
                    return SpriteDictionary.Succubi[91];
                else if (actor.GetStomachSize(15, 0.8f) == 15)
                    return SpriteDictionary.Succubi[90];
                else if (actor.GetStomachSize(15, 0.9f) == 15)
                    return SpriteDictionary.Succubi[89];
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
            return SpriteDictionary.Succubi[43 + actor.GetStomachSize()];
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
        if (actor.SquishedBreasts)
            return SpriteDictionary.Succubi[59 + actor.Unit.BreastSize];
        return SpriteDictionary.Succubi[63 + actor.Unit.BreastSize];
    }

    protected override Sprite BreastsShadowSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.SquishedBreasts)
            return SpriteDictionary.Succubi[67 + actor.Unit.BreastSize];
        return SpriteDictionary.Succubi[71 + actor.Unit.BreastSize];
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        int baseSize = actor.Unit.DickSize / 3;
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
        int combined = Math.Min(baseSize + ballOffset + 2, 20);
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

    protected override Sprite HairSprite(Actor_Unit actor) => SpriteDictionary.Succubi[Math.Min(75 + actor.Unit.HairStyle, 78)];
    protected override Sprite HairSprite2(Actor_Unit actor) => SpriteDictionary.Succubi[Math.Min(79 + actor.Unit.HairStyle, 82)];
    protected override Sprite HairSprite3(Actor_Unit actor) => SpriteDictionary.Succubi[Math.Min(83 + actor.Unit.HairStyle, 88)];



}
