using System.Collections.Generic;
using UnityEngine;

class FeralEevee : BlankSlate
{
    internal FeralEevee()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EeveeEqualeonSkin);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EeveeEqualeonSkin);
        GentleAnimation = true;

        CanBeGender = new List<Gender>() { Gender.None };
        GentleAnimation = true;

        Body = new SpriteExtraInfo(0, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.AccessoryColor)); //Fluff
        Mouth = new SpriteExtraInfo(8, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.HairColor));
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.SkinColor)); // legs
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, WhiteColored);
        BodyAccent3 = null;
        BodyAccent4 = null;
        BodyAccent5 = null;
        Head = null;
        Eyes = new SpriteExtraInfo(7, EyesSprite, WhiteColored);
        Belly = new SpriteExtraInfo(2, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.SkinColor));
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        int offset = -25;
        AddOffset(Body, 0, offset * .625f);
        AddOffset(BodyAccessory, 0, offset * .625f);
        AddOffset(BodyAccent, 0, offset * .625f);
        AddOffset(Mouth, 0, offset * .625f);
        AddOffset(BodyAccent2, 0, offset * .625f);
        AddOffset(Eyes, 0, offset * .625f);
        AddOffset(Belly, 0, offset * .625f);

    }

    internal override void RandomCustom(Unit unit)
    {
        unit.SkinColor = State.Rand.Next(SkinColors);
        unit.AccessoryColor = State.Rand.Next(AccessoryColors);
    }
    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (!actor.HasBelly)
            return State.GameManager.SpriteDictionary.FeralEevee[0];
        int size = actor.GetStomachSize(33);
        if (size >= 24)
        {
            return State.GameManager.SpriteDictionary.FeralEevee[47];
        }
        if (size >= 6)
        {
            return State.GameManager.SpriteDictionary.FeralEevee[12];
        }
        return State.GameManager.SpriteDictionary.FeralEevee[10];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (!actor.HasBelly)
            return null;

        int size = actor.GetStomachSize(33);
        if (size >= 24)
        {
            return State.GameManager.SpriteDictionary.FeralEevee[48];
        }
        if (size >= 6)
        {
            return State.GameManager.SpriteDictionary.FeralEevee[13];
        }
        return State.GameManager.SpriteDictionary.FeralEevee[11];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
        {
            return State.GameManager.SpriteDictionary.FeralEevee[4];
        }
        else 
        {
            return State.GameManager.SpriteDictionary.FeralEevee[3];
        }
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.IsBeingRubbed)
        {
            return State.GameManager.SpriteDictionary.FeralEevee[7];
        }
        if (actor.Unit.IsDead)
        {
            return State.GameManager.SpriteDictionary.FeralEevee[8];
        }
        return State.GameManager.SpriteDictionary.FeralEevee[2];
    }
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        int size = actor.GetStomachSize(33);

        if (!actor.HasBelly)
            return null;

        if (size >= 33 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralEevee[48];
        }

        if (size >= 32 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralEevee[45];
        }

        if (size >= 31 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralEevee[44];
        }

        if (size > 29) size = 29;

        return State.GameManager.SpriteDictionary.FeralEevee[14 + size];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.FeralEevee[1];
}

