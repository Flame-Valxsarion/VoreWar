using System.Collections.Generic;
using UnityEngine;

class Cake : BlankSlate
{
    public Cake()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        ExtraColors1 = ColorMap.PastelColorCount;
        ExtraColors2 = ColorMap.PastelColorCount;
        ExtraColors3 = ColorMap.PastelColorCount;
        ExtraColors4 = ColorMap.PastelColorCount;

        Body = new SpriteExtraInfo(0, BodySprite, (s) => ColorMap.GetPastelColor(s.Unit.ExtraColor1)); // Body.
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, (s) => ColorMap.GetPastelColor(s.Unit.ExtraColor2)); // Ring.
        BodyAccent2 = new SpriteExtraInfo(2, BodyAccentSprite2, (s) => ColorMap.GetPastelColor(s.Unit.ExtraColor3)); // The balls around the top.
        BodyAccent3 = new SpriteExtraInfo(3, BodyAccentSprite3, (s) => ColorMap.GetPastelColor(s.Unit.ExtraColor4)); // Candles.
        Mouth = new SpriteExtraInfo(4, MouthSprite, WhiteColored); // Mouth.
        Head = new SpriteExtraInfo(5, HeadSprite, WhiteColored); // Teeth.
        Eyes = new SpriteExtraInfo(6, EyesSprite, WhiteColored); // Flames.
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
            return SpriteDictionary.Cake[1];
        return SpriteDictionary.Cake[0];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => SpriteDictionary.Cake[4];
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => SpriteDictionary.Cake[2];
    protected override Sprite BodyAccentSprite3(Actor_Unit actor) => SpriteDictionary.Cake[3];

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
        {
            if (Config.Bones || Config.ScatBones) return SpriteDictionary.Cake[6];
            else return SpriteDictionary.Cake[5];
        }
        return null;
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
            return SpriteDictionary.Cake[8];
        return SpriteDictionary.Cake[7];
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.HasBelly == false)
            return null;
        return SpriteDictionary.Cake[9 + actor.GetStomachSize(7)];
    }
}

