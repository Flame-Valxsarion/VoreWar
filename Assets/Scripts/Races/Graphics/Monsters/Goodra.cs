using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Goodra : BlankSlate
{
    readonly Sprite[] Sprites = SpriteDictionary.Goodra;

    public Goodra()
    {
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.GoodraSkin);
        EyeTypes = 4;
        GentleAnimation = true;
        /*
        Body = new SpriteExtraInfo(1, BodySprite, WhiteColored); // Body
        Belly = new SpriteExtraInfo(2, null, WhiteColored); // Belly
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, WhiteColored); // Leg
        Eyes = new SpriteExtraInfo(4, EyesSprite, WhiteColored); // Face
        Hair = new SpriteExtraInfo(5, HeadSprite, WhiteColored); // Attack Frame
        */

        Body = new SpriteExtraInfo(1, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GoodraSkin, s.Unit.SkinColor)); // Body
        Belly = new SpriteExtraInfo(2, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GoodraSkin, s.Unit.SkinColor)); // Belly
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GoodraSkin, s.Unit.SkinColor)); // Leg
        Eyes = new SpriteExtraInfo(4, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GoodraSkin, s.Unit.SkinColor)); // Face
        Hair = new SpriteExtraInfo(5, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GoodraSkin, s.Unit.SkinColor)); // Attack Frame
        clothingColors = 0;
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {
        if (actor.IsAttacking)
            return Sprites[2];
        if (actor.IsOralVoring)
            return Sprites[1];
        return Sprites[0];
    }

    protected override Sprite EyesSprite(Actor_Unit actor) // Face
    {
        if (actor.IsOralVoring)
            return null;
        return Sprites[4+actor.Unit.EyeType];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites[9]; // Leg

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // Belly
    {
        if (actor.HasBelly == false)
            return null;
        return Sprites[10 + actor.GetStomachSize(6)];
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Attack Antenna
    {
        if (actor.IsAttacking)
            return Sprites[3];
        return null;
    }

}
