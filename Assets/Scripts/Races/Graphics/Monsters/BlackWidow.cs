using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class BlackWidow : BlankSlate
{
    readonly Sprite[] Sprites = SpriteDictionary.BlackWidow;

    public BlackWidow()
    {
        CanBeGender = new List<Gender>() { Gender.Male, Gender.Female };
        GentleAnimation = true;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.BlackWidowSkin);

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BlackWidowSkin, s.Unit.SkinColor));
        Mouth = new SpriteExtraInfo(5, MouthSprite, WhiteColored); // Acid
        BodyAccent = new SpriteExtraInfo(8, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BlackWidowSkin, s.Unit.SkinColor)); // Left Front Leg
		BodyAccent2 = new SpriteExtraInfo(2, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BlackWidowSkin, s.Unit.SkinColor)); // Right Front Leg
		BodyAccent3 = new SpriteExtraInfo(6, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BlackWidowSkin, s.Unit.SkinColor)); // Left Legs
        BodyAccent4 = new SpriteExtraInfo(1, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BlackWidowSkin, s.Unit.SkinColor)); // Right Legs
        BodyAccent5 = new SpriteExtraInfo(7, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BlackWidowSkin, s.Unit.SkinColor)); // Left Maw Leg
        BodyAccent6 = new SpriteExtraInfo(3, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BlackWidowSkin, s.Unit.SkinColor)); // Right Maw Leg
        Belly = new SpriteExtraInfo(0, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BlackWidowSkin, s.Unit.SkinColor));

    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        float xoffset = 50 * .625f;
        float yoffset = 50 * .625f;
        AddOffset(Body, xoffset, yoffset);
        AddOffset(Mouth, xoffset, yoffset);
        AddOffset(BodyAccent, xoffset, yoffset);
        AddOffset(BodyAccent2, xoffset, yoffset);
        AddOffset(BodyAccent3, xoffset, yoffset);
        AddOffset(BodyAccent4, xoffset, yoffset);
        AddOffset(BodyAccent5, xoffset, yoffset);
        AddOffset(BodyAccent6, xoffset, yoffset);
        AddOffset(Belly, xoffset, yoffset);
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating)
        {
            return Sprites[1];
        }
        else
        {
            return Sprites[0];
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating)
        {
            return Sprites[3];
        }
        else
        {
            return Sprites[2];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Left Front Leg
    {
        if (actor.IsAttacking || actor.IsEating)
        {
            return Sprites[5];
        }
        else
        {
            return Sprites[4];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Right Front Leg
    {
        if (actor.IsAttacking || actor.IsEating)
        {
            return Sprites[12];
        }
        else
        {
            return Sprites[11];
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Left Legs
    {
        return Sprites[8];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Right Legs
    {
        return Sprites[13];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Left Maw Leg
    {
        if (actor.IsAttacking || actor.IsEating)
        {
            return Sprites[7];
        }
        else
        {
            return Sprites[6];
        }
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Right Maw Leg
    {
        if (actor.IsAttacking || actor.IsEating)
        {
            return Sprites[10];
        }
        else
        {
            return Sprites[9];
        }
    }


    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (!actor.HasBelly)
            return Sprites[14];

        int size = actor.GetStomachSize(59);

        if (size >= 59 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.BlackWidow[54];
        }

        else if (size >= 56 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.BlackWidow[53];
        }

        else if (size >= 53 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.BlackWidow[52];
        }

        else if (size >= 50 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.BlackWidow[51];
        }

        else if (size >= 47 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.BlackWidow[50];
        }

        else if (size >= 44 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.BlackWidow[49];
        }

        else if (size >= 41 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.BlackWidow[48];
        }

        else if (size >= 38 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.BlackWidow[47];
        }

        else if (size >= 35 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.BlackWidow[46];
        }

        else if (size >= 32 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.BlackWidow[45];
        }

        if (size > 29) size = 29;

        return SpriteDictionary.BlackWidow[15 + size];
    }
    
}
