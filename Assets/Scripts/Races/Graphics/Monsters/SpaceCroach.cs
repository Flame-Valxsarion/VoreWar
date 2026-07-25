using System.Collections.Generic;
using UnityEngine;

class SpaceCroach : BlankSlate
{
    public SpaceCroach()
    {
        GentleAnimation = true;
        CanBeGender = new List<Gender>() { Gender.Male, Gender.Female };
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SpaceCroachSkin);

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SpaceCroachSkin, s.Unit.SkinColor)); // Body & Head
        Eyes = new SpriteExtraInfo(5, EyesSprite, WhiteColored); // Expression & Teeth
        BodyAccent = new SpriteExtraInfo(0, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SpaceCroachSkin, s.Unit.SkinColor)); // Background Legs
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SpaceCroachSkin, s.Unit.SkinColor)); // Background Arms
        BodyAccent3 = new SpriteExtraInfo(2, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SpaceCroachSkin, s.Unit.SkinColor)); // Abdomen
        BodyAccent4 = new SpriteExtraInfo(6, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SpaceCroachSkin, s.Unit.SkinColor)); // Foreground Legs
        BodyAccent5 = new SpriteExtraInfo(7, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SpaceCroachSkin, s.Unit.SkinColor)); // Foreground Arms
        Belly = new SpriteExtraInfo(3, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SpaceCroachSkin, s.Unit.SkinColor)); // Belly
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.GetStomachSize(44) > 20) 
        {   
            if (actor.IsEating) return SpriteDictionary.SpaceCroach[5];
            else if (actor.IsAttacking) return SpriteDictionary.SpaceCroach[4];
            else return SpriteDictionary.SpaceCroach[3];
        }
        else         
        {   
            if (actor.IsEating) return SpriteDictionary.SpaceCroach[2];
            else if (actor.IsAttacking) return SpriteDictionary.SpaceCroach[1];
            else return SpriteDictionary.SpaceCroach[0];
        }
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.GetStomachSize(44) > 20)     
        {   
            if (actor.IsEating) return SpriteDictionary.SpaceCroach[11];
            else if (actor.IsAttacking) return SpriteDictionary.SpaceCroach[10];
            else return SpriteDictionary.SpaceCroach[9];
        }
        else         
        {   
            if (actor.IsEating) return SpriteDictionary.SpaceCroach[8];
            else if (actor.IsAttacking) return SpriteDictionary.SpaceCroach[7];
            else return SpriteDictionary.SpaceCroach[6];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.GetStomachSize(44) > 20)  return SpriteDictionary.SpaceCroach[18];
        else if (actor.GetStomachSize(44) >= 13) return SpriteDictionary.SpaceCroach[17];
        else if (actor.GetStomachSize(44) >= 7) return SpriteDictionary.SpaceCroach[16];
        else return SpriteDictionary.SpaceCroach[15];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (actor.GetStomachSize(44) > 20) return null;
        else if (actor.IsEating||actor.IsAttacking) return SpriteDictionary.SpaceCroach[20];
        else return SpriteDictionary.SpaceCroach[19];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (actor.GetStomachSize(44) > 20) return null;
        else return SpriteDictionary.SpaceCroach[25];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        if (actor.GetStomachSize(44) > 20) return null;
        else if (actor.GetStomachSize(44) >= 13) return SpriteDictionary.SpaceCroach[14];
        else if (actor.GetStomachSize(44) >= 7) return SpriteDictionary.SpaceCroach[13];
        else return SpriteDictionary.SpaceCroach[12];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        if (actor.GetStomachSize(44) > 20)         
        {   
            if (actor.IsEating || actor.IsAttacking) return SpriteDictionary.SpaceCroach[24];
            else return SpriteDictionary.SpaceCroach[23];
        }
        else         
        {   
            if (actor.IsEating || actor.IsAttacking) return SpriteDictionary.SpaceCroach[22];
            else return SpriteDictionary.SpaceCroach[21];
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;

            int size = actor.GetStomachSize(44);

        if (size > 20)         
        {   
            AddOffset(Belly, 0, -30 * .5f);
        }
        else
        {   
            AddOffset(Belly, 0, 0 * .5f);
        }

        if (size >= 44 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
            return SpriteDictionary.SpaceCroach[63];
        }

        if (size >= 42 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.SpaceCroach[62];
        }

        if (size >= 40 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.SpaceCroach[61];
        }

        if (size >= 38 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.SpaceCroach[60];
        }

        if (size >= 36 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.SpaceCroach[59];
        }

        if (size >= 34 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.SpaceCroach[58];
        }

        if (size >= 32 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.SpaceCroach[57];
        }

        if (size >= 30 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.SpaceCroach[56];
        }

        if (size > 29) size = 29;
        return SpriteDictionary.SpaceCroach[25 + size];
    }
}

