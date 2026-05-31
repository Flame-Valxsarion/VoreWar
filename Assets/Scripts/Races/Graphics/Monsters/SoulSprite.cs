using System.Collections.Generic;
using UnityEngine;

class SoulSprite : BlankSlate
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.SoulSprite;
    internal SoulSprite()
    {
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SoulSpriteSkin);
        
        CanBeGender = new List<Gender>() { Gender.None, Gender.Male, Gender.Female, Gender.Hermaphrodite };
        Head = new SpriteExtraInfo(9, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SoulSpriteSkin, s.Unit.SkinColor));
        HeadTypes = 6; // Facial Expressions
        Body = new SpriteExtraInfo(8, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SoulSpriteSkin, s.Unit.SkinColor));
        BodyAccent = new SpriteExtraInfo(10, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SoulSpriteSkin, s.Unit.SkinColor)); // Halo
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SoulSpriteSkin, s.Unit.SkinColor)); // Ears
        BodyAccentTypes1 = 2; // Halo
        BodyAccentTypes2 = 13; // Ears
        Belly = new SpriteExtraInfo(9, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SoulSpriteSkin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(3, WeaponSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SoulSpriteSkin, s.Unit.SkinColor)); // Arms in attack
        SecondaryAccessory = new SpriteExtraInfo(11, SecondaryAccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SoulSpriteSkin, s.Unit.SkinColor)); // Arms resting
        BodySize = new SpriteExtraInfo(7, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SoulSpriteSkin, s.Unit.SkinColor));
        BodySizes = 6;   

    }

    internal override void SetBaseOffsets(Actor_Unit actor) // Offset to give the floaty view
    {
        int offset = 0;
        AddOffset(Body, 0, offset * .625f);
        AddOffset(BodyAccent, 0, offset * .625f);
        AddOffset(BodyAccent2, 0, offset * .625f);
        AddOffset(Head, 0, offset * .625f);
        AddOffset(Belly, 0, offset * .625f);
        AddOffset(Weapon, 0, offset * .625f);
        AddOffset(SecondaryAccessory, 0, offset * .625f);

    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.BodySize = 0;
        unit.BodyAccentType1 = 1;
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
        unit.HeadType = State.Rand.Next(HeadTypes);
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
        {
            return Sprites[22];
        }
        if (actor.IsAnalVoring || actor.IsBeingRubbed)
        {
            return Sprites[21];
        }
        if (actor.IsAttacking)
        {
            return Sprites[20];
        }
        if (actor.Unit.IsDead || actor.Surrendered == true)
        {
            return Sprites[24];
        }
        if (actor.IsAbsorbing)
        {
            return Sprites[23];
        }
        if (actor.HasJustVored)
        {
            return Sprites[19];
        }
        return Sprites[13 + actor.Unit.HeadType];
    }

    protected override Sprite BodySprite(Actor_Unit actor)
        {
            return Sprites[25 + actor.Unit.BodySize];
        }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Halo Toggle
    {
        if (actor.Unit.BodyAccentType1 == 1)
            {
                return Sprites[33];
            }
            
            return null;
                
            
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Ears Customization
    {
        return Sprites[0 + actor.Unit.BodyAccentType2];
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
        {
            if (actor.IsAttacking)
            {
                return null;
            }
            return Sprites[31];
        }
    
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor)
        {
            if (actor.IsAttacking)
            {
                return Sprites[32];
            }
            return null;
        }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
            
        return actor.HasBelly ? State.GameManager.SpriteDictionary.SoulSprite[34 + actor.GetStomachSize(14)] : null;
    }
}

