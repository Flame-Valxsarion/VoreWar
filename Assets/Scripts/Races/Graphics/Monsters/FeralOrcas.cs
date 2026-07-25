using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class FeralOrcas : BlankSlate
{
	readonly Sprite[] Sprites = SpriteDictionary.FeralOrcas;

    public FeralOrcas()
    {
        CanBeGender = new List<Gender>() { Gender.Male, Gender.Female };
        BodyAccentTypes1 = 7; // dorsal fins
        clothingColors = 0;
        GentleAnimation = true;
        WeightGainDisabled = true;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.FeralOrcaSkin);
        EyeColors = ColorMap.EyeColorCount;

        Body = new SpriteExtraInfo(1, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralOrcaSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralOrcaSkin, s.Unit.SkinColor));
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralOrcaSkin, s.Unit.SkinColor)); // dorsal fins
        BodyAccent2 = new SpriteExtraInfo(2, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralOrcaSkin, s.Unit.SkinColor)); // tail
        BodyAccent3 = new SpriteExtraInfo(4, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralOrcaSkin, s.Unit.SkinColor)); // pelvic fin
        Mouth = new SpriteExtraInfo(6, MouthSprite, WhiteColored);
        Eyes = new SpriteExtraInfo(8, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        Belly = new SpriteExtraInfo(3, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralOrcaSkin, s.Unit.SkinColor));
        EyeTypes = 6;

    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (!actor.Targetable) return Sprites[2];

        if (actor.IsAttacking || actor.IsOralVoring)
        {
            return Sprites[3];
        }
        return Sprites[2];
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
		if (actor.IsAttacking || actor.IsOralVoring)
		{
			AddOffset(Head, 1, 0 * .625f);
			AddOffset(Eyes, 2, 2 * .625f);
		}
		else
		{
			AddOffset(Head, 1, 0 * .625f);
			AddOffset(Eyes, 1, 0 * .625f);
		}
    }

    protected override Sprite BodySprite(Actor_Unit actor) => Sprites[5];

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites[7 + actor.Unit.BodyAccentType1]; // Dorsal fins
    
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites[6]; // tail

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) => Sprites[34]; // Pelvic Fin

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[35 + actor.Unit.EyeType];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (!actor.HasBelly)
            return null;

        int size = actor.GetStomachSize(25);

        if (size >= 25 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.FeralOrcas[33];
        }

        if (size >= 24 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.FeralOrcas[32];
        }

        if (size >= 22 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.FeralOrcas[31];
        }

        if (size >= 20 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.FeralOrcas[30];
        }

        if (size >= 18 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.FeralOrcas[29];
        }

        if (size >= 16 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.FeralOrcas[28];
        }

        if (size > 14) size = 14;

        return SpriteDictionary.FeralOrcas[14 + size];
    }

}
