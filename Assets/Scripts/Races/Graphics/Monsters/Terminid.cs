using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Terminid : BlankSlate
{
    readonly Sprite[] Sprites = SpriteDictionary.Terminid;

    public Terminid()
    {
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.TerminidSkin);
        GentleAnimation = true;
        WeightGainDisabled = true;
        CanBeGender = new List<Gender>() { Gender.None };
        BodyAccentTypes1 = 5;

        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerminidSkin, s.Unit.SkinColor)); // Belly Cover
        Head = new SpriteExtraInfo(4, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerminidSkin, s.Unit.SkinColor)); // Head
        BodyAccent = new SpriteExtraInfo(5, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerminidSkin, s.Unit.SkinColor)); // HeadPlates
        BodyAccent2 = new SpriteExtraInfo(6, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerminidSkin, s.Unit.SkinColor)); // Rightside appendages
        BodyAccent3 = new SpriteExtraInfo(1, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerminidSkin, s.Unit.SkinColor)); // Leftside appendages
        Belly = new SpriteExtraInfo(3, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerminidSkin, s.Unit.SkinColor)); // Abdomen

    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // HeadPlates
    {
        return SpriteDictionary.Terminid[4 + actor.Unit.BodyAccentType1];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Rightside appendages
    {
        return SpriteDictionary.Terminid[9];
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Belly Cover
    {
        if (actor.HasBelly)
            return null;
        else
            return SpriteDictionary.Terminid[0];
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.GetStomachSize(37) > 13 )
        {
            AddOffset(BodyAccent3, 10, 0 * .625f);
            AddOffset(Belly, -25, 0 * .625f);
        }
        else
        {
            AddOffset(BodyAccent3, 10, 0 * .625f);
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (actor.IsAttacking)
            return SpriteDictionary.Terminid[2];
        if (actor.IsOralVoring)
            return SpriteDictionary.Terminid[3];
        else
            return SpriteDictionary.Terminid[1];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Leftside appendages
    {
        if (actor.IsAttacking)
            return SpriteDictionary.Terminid[11];
        return SpriteDictionary.Terminid[10]; 
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (!actor.HasBelly)
            return null;

        int size = actor.GetStomachSize(37);

        if ( size >= 37 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
            return SpriteDictionary.Terminid[37];
        }

        if (size >= 35 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.Terminid[36];
        }

        if (size >= 32 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.Terminid[35];
        }

        if (size >= 29 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.Terminid[34];
        }

        if (size >= 26 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.Terminid[33];
        }

        if (size >= 23 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.Terminid[32];
        }

        if (size > 19) size = 19;
        return SpriteDictionary.Terminid[12 + size];
    }
}