using System.Collections.Generic;
using UnityEngine;

class Whisp : BlankSlate
{
    public Whisp()
    {
        CanBeGender = new List<Gender>() { Gender.None };

        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MermenSkin);
        Body = new SpriteExtraInfo(1, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenSkin, s.Unit.SkinColor)); // Body
    }
    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {

        return SpriteDictionary.Whisp[0];

    }

}
