using System.Collections.Generic;
using UnityEngine;

class FeralEqualeon : BlankSlate
{
    internal FeralEqualeon()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EeveeEqualeonSkin);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EeveeEqualeonSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EqualeonEyes);
        GentleAnimation = true;
        HairStyles = 11;

        CanBeGender = new List<Gender>() { Gender.None };
        GentleAnimation = true;

        Body = new SpriteExtraInfo(0, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(1, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.AccessoryColor)); //Fluff
        Mouth = new SpriteExtraInfo(8, MouthSprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.SkinColor)); // legs
        BodyAccent2 = new SpriteExtraInfo(5, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.SkinColor)); // legsfluff
        BodyAccent3 = new SpriteExtraInfo(8, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.AccessoryColor)); // other eye stuff
        BodyAccent4 = new SpriteExtraInfo(8, BodyAccentSprite4, WhiteColored); // blush
        BodyAccent5 = null;
        Head = null;
        Hair = new SpriteExtraInfo(8, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.AccessoryColor));
        Eyes = new SpriteExtraInfo(8, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EqualeonEyes, s.Unit.EyeColor));
        SecondaryEyes = new SpriteExtraInfo(8, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.SkinColor));
        Belly = new SpriteExtraInfo(2, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.SkinColor));
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.HasBelly == true)
        {
            int size = actor.GetStomachSize(27);

            if (size >= 27 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false))
            {
                AddOffset(Body, 0, 22 * .625f);
                AddOffset(BodyAccessory, 0, 22 * .625f);
                AddOffset(BodyAccent, 0, 22 * .625f);
                AddOffset(BodyAccent2, 0, 22 * .625f);
                AddOffset(BodyAccent3, 0, 22 * .625f);
                AddOffset(BodyAccent4, 0, 22 * .625f);
                AddOffset(Mouth, 0, 22 * .625f);
                AddOffset(Eyes, 0, 22 * .625f);
                AddOffset(SecondaryEyes, 0, 22 * .625f);
                AddOffset(Hair, 0, 22 * .625f);
            }
            else if (size >= 24 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
            {
                AddOffset(Body, 0, 19 * .625f);
                AddOffset(BodyAccessory, 0, 19 * .625f);
                AddOffset(BodyAccent, 0, 19 * .625f);
                AddOffset(BodyAccent2, 0, 19 * .625f);
                AddOffset(BodyAccent3, 0, 19 * .625f);
                AddOffset(BodyAccent4, 0, 19 * .625f);
                AddOffset(Mouth, 0, 19 * .625f);
                AddOffset(Eyes, 0, 19 * .625f);
                AddOffset(SecondaryEyes, 0, 19 * .625f);
                AddOffset(Hair, 0, 19 * .625f);
            }
            else if (size >= 23)
            {
                AddOffset(Body, 0, 9 * .625f);
                AddOffset(BodyAccessory, 0, 9 * .625f);
                AddOffset(BodyAccent, 0, 9 * .625f);
                AddOffset(BodyAccent2, 0, 9 * .625f);
                AddOffset(BodyAccent3, 0, 9 * .625f);
                AddOffset(BodyAccent4, 0, 9 * .625f);
                AddOffset(Mouth, 0, 9 * .625f);
                AddOffset(Eyes, 0, 9 * .625f);
                AddOffset(SecondaryEyes, 0, 9 * .625f);
                AddOffset(Hair, 0, 9 * .625f);
            }
            else
            {
                AddOffset(Body, 0, 0 * .625f);
                AddOffset(BodyAccessory, 0, 0 * .625f);
                AddOffset(BodyAccent, 0, 0 * .625f);
                AddOffset(BodyAccent2, 0, 0 * .625f);
                AddOffset(BodyAccent3, 0, 0 * .625f);
                AddOffset(BodyAccent4, 0, 0 * .625f);
                AddOffset(Mouth, 0, 0 * .625f);
                AddOffset(Eyes, 0, 0 * .625f);
                AddOffset(SecondaryEyes, 0, 0 * .625f);
                AddOffset(Hair, 0, 0 * .625f);
            }
        }
    }

    internal override void RandomCustom(Unit unit)
    {
        unit.SkinColor = State.Rand.Next(SkinColors);
        unit.AccessoryColor = State.Rand.Next(AccessoryColors);
        unit.EyeColor = State.Rand.Next(EyeColors);
        unit.HairStyle = State.Rand.Next(HairStyles);
    }
    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (!actor.HasBelly)
            return State.GameManager.SpriteDictionary.FeralEqualeon[0];
        return State.GameManager.SpriteDictionary.FeralEqualeon[22];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (!actor.HasBelly)
            return null;

        return State.GameManager.SpriteDictionary.FeralEqualeon[24];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (!actor.HasBelly)
            return null;

        return State.GameManager.SpriteDictionary.FeralEqualeon[25];
    }
    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (actor.IsBeingRubbed)
        {
            return State.GameManager.SpriteDictionary.FeralEqualeon[5];
        }
        if (actor.Unit.IsDead)
        {
            return State.GameManager.SpriteDictionary.FeralEqualeon[4];
        }
        return null;
    }
    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        if (actor.IsBeingRubbed)
        {
            return State.GameManager.SpriteDictionary.FeralEqualeon[7];
        }

        return null;
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
        {
            return State.GameManager.SpriteDictionary.FeralEqualeon[9];
        }
        else 
        {
            return State.GameManager.SpriteDictionary.FeralEqualeon[8];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.FeralEqualeon[(actor.IsAttacking || actor.IsEating) ? 1 : 0];
    protected override Sprite HairSprite(Actor_Unit actor) => actor.Unit.HairStyle == 12 ? null : State.GameManager.SpriteDictionary.FeralEqualeon[11 + actor.Unit.HairStyle];
    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.IsBeingRubbed)
        {
            return null;
        }
        if (actor.Unit.IsDead)
        {
            return null;
        }
        return State.GameManager.SpriteDictionary.FeralEqualeon[2];
    }
    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
        if (actor.IsBeingRubbed)
        {
            return State.GameManager.SpriteDictionary.FeralEqualeon[6];
        }
        if (actor.Unit.IsDead)
        {
            return State.GameManager.SpriteDictionary.FeralEqualeon[3];
        }
        return null;
    }
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        int size = actor.GetStomachSize(27);

        if (!actor.HasBelly)
            return null;

        if (size >= 27 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralEqualeon[52];
        }

        if (size >= 26 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralEqualeon[51];
        }

        if (size >= 25 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralEqualeon[50];
        }

        if (size > 23) size = 23;

        return State.GameManager.SpriteDictionary.FeralEqualeon[26 + size];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        if (!actor.HasBelly)
            return State.GameManager.SpriteDictionary.FeralEqualeon[1];
        return State.GameManager.SpriteDictionary.FeralEqualeon[23];
    }
}

