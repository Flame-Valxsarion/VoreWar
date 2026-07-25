using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class FeralHorses : BlankSlate
{
    //readonly Sprite[] Sprites = SpriteDictionary.FeralHorses;

    public FeralHorses()
    {
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.FeralHorseSkin); // Main body, legs, head, tail upper
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.FeralHorseSkin);
        GentleAnimation = true;
        WeightGainDisabled = true;
        CanBeGender = new List<Gender>() { Gender.Male, Gender.Female, Gender.Hermaphrodite };
        TailTypes = 6;
        HairStyles = 6;

        Body = new SpriteExtraInfo(10, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.SkinColor)); // Body
        BodyAccent8 = new SpriteExtraInfo(11, BodyAccentSprite8, null, null); // Face
        BodyAccessory = new SpriteExtraInfo(13, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.HairColor)); // Mane
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.SkinColor)); // right hind leg
        BodyAccent2 = new SpriteExtraInfo(8, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.HairColor)); // tail
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.SkinColor)); // sheath
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.SkinColor)); // belly cover
        BodyAccent5 = new SpriteExtraInfo(4, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.SkinColor)); // left hind leg
        BodyAccent6 = new SpriteExtraInfo(1, BodyAccentSprite6, null, null); // right hind hoof
        BodyAccent7 = new SpriteExtraInfo(4, BodyAccentSprite7, null, null); // left hind hoof
        Belly = new SpriteExtraInfo(7, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.SkinColor));
        Dick = new SpriteExtraInfo(4, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(3, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.SkinColor));

    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.TailType = State.Rand.Next(TailTypes);
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // Mane
    {
        return SpriteDictionary.FeralHorses[10 + actor.Unit.HairStyle + (actor.IsAttacking ? 6 : 0)];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Left Hind leg
    {
        return SpriteDictionary.FeralHorses[5];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Right Hind Hoof
    {
        return SpriteDictionary.FeralHorses[6];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Belly Cover
    {
        if (actor.HasBelly)
            return null;
        else
            return SpriteDictionary.FeralHorses[8];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) //Sheath
    {
        if (actor.Unit.DickSize < 0) return null;
        if (Config.HideCocks) return null;
		
		if (actor.IsErect()) return SpriteDictionary.FeralHorses[28];

		else return SpriteDictionary.FeralHorses[29];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Tail
    { 
        return SpriteDictionary.FeralHorses[22 + actor.Unit.TailType];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Right Hind Leg
    {
        return SpriteDictionary.FeralHorses[4];
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Left Hind Hoof
    {
        return SpriteDictionary.FeralHorses[7];
    }
    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {
        if (actor.IsAttacking)
            return SpriteDictionary.FeralHorses[1];
        if (actor.IsOralVoring)
            return SpriteDictionary.FeralHorses[1];
        else
            return SpriteDictionary.FeralHorses[0];
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // Face
    {
        if (actor.IsAttacking)
            return SpriteDictionary.FeralHorses[3]; 
        if (actor.IsOralVoring)
            return SpriteDictionary.FeralHorses[3]; 
        return SpriteDictionary.FeralHorses[2]; 
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.GetStomachSize(31) > 4)
        {
            Belly.layer = 9;
        }
        else
        {
            Belly.layer = 6;
        }

        if (!actor.HasBelly)
            return null;

        int size = actor.GetStomachSize(31);

        if ( size >= 31 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
			AddOffset(Belly, -23 * .625f, -7 * .625f);
            return SpriteDictionary.FeralHorses[54];
        }

        if (size >= 29 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
			AddOffset(Belly, -23 * .625f, -7 * .625f);
            return SpriteDictionary.FeralHorses[53];
        }

        if (size >= 27 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
			AddOffset(Belly, -23 * .625f, -7 * .625f);
            return SpriteDictionary.FeralHorses[52];
        }

        if (size >= 25 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
			AddOffset(Belly, -23 * .625f, -7 * .625f);
            return SpriteDictionary.FeralHorses[51];
        }

        if (size >= 23 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
			AddOffset(Belly, -23 * .625f, -7 * .625f);
            return SpriteDictionary.FeralHorses[50];
        }

        if (size > 19) size = 19;

        return SpriteDictionary.FeralHorses[30 + size];
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.DickSize < 0) return null;
        if (Config.HideCocks) return null;

        if (actor.IsCockVoring) return SpriteDictionary.FeralHorses[9];
		if (actor.IsErect()) return SpriteDictionary.FeralHorses[9];

        return null;
    }
    
    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == true && !Config.HideCocks && actor.PredatorComponent?.BallsFullness > 0)
		{
			AddOffset(Balls, -48 * .625f, 0 * .625f);
        }
		else return null;

        int size = actor.GetBallSize(30);

        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false)
        {
            return SpriteDictionary.FeralHorses[79];
        }

        else if (size >= 29 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.FeralHorses[78];
        }

        else if (size >= 27 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.FeralHorses[77];
        }

        else if (size >= 25 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.FeralHorses[76];
        }

        else if (size >= 23 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.FeralHorses[75];
        }

        else if (size >= 21 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.FeralHorses[74];
        }

        if (size > 19) size = 19;
        return SpriteDictionary.FeralHorses[55 + size];
    }
}