using System.Collections.Generic;
using UnityEngine;

class FeralSlime : BlankSlate
{
    internal FeralSlime()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SlimeMain); ;
        GentleAnimation = true;

        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, s.Unit.SkinColor));
        BodyAccent = new SpriteExtraInfo(10, BodyAccentSprite, null, null);
        Belly = new SpriteExtraInfo(2, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, s.Unit.SkinColor));

    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.IsAttacking)
        {
            AddOffset(Belly, 0, 10);
            AddOffset(BodyAccessory, 0, 10);
            AddOffset(BodyAccent, 0, 10);
        }
    }

    internal override void RandomCustom(Unit unit)
    {
        unit.SkinColor = State.Rand.Next(SkinColors);
    }
    //protected override Sprite BodySprite(Actor_Unit actor) => SpriteDictionary.FeralSlime[0];
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return SpriteDictionary.FeralSlime[0];

        int size = actor.GetStomachSize(8);

        if (size >= 8 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
            return SpriteDictionary.FeralSlime[7];
        }
        if (size >= 7 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.FeralSlime[6];
        }
        return SpriteDictionary.FeralSlime[0 + actor.GetStomachSize(6)];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.HasBelly == false)
            return SpriteDictionary.FeralSlime[8];

        int size = actor.GetStomachSize(8);

        if (size >= 8 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
            return SpriteDictionary.FeralSlime[15];
        }
        if (size >= 7 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.FeralSlime[14];
        }
        return SpriteDictionary.FeralSlime[8 + actor.GetStomachSize(6)];
    }
    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        if (actor.HasBelly == false)
            return null;
        int size = actor.GetStomachSize(24, 0.7f);
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 24)
        {
            return SpriteDictionary.FeralSlime[39];
        }
        else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 24)
        {
            return SpriteDictionary.FeralSlime[38];
        }
        else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 23)
        {
            return SpriteDictionary.FeralSlime[37];
        }
        else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 22)
        {
            return SpriteDictionary.FeralSlime[36];
        }
        return SpriteDictionary.FeralSlime[16 + actor.GetStomachSize(20, 0.7f)];
    }
}

