using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is a skyshark.
/// </summary>
class Sharks : BlankSlate
{
    public Sharks()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        SkinColors = ColorMap.SharkColorCount;
        AccessoryColors = ColorMap.SharkBellyColorCount;
        ExtraColors1 = ColorMap.SlimeColorCount;

        Body = new SpriteExtraInfo(2, BodySprite, (s) => ColorMap.GetSharkColor(s.Unit.SkinColor));
        Eyes = new SpriteExtraInfo(3, EyesSprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, (s) => ColorMap.GetSharkBellyColor(s.Unit.AccessoryColor));
        Mouth = new SpriteExtraInfo(5, MouthSprite, WhiteColored);
        BodyAccent2 = new SpriteExtraInfo(0, BodyAccentSprite2, (s) => ColorMap.GetSharkColor(s.Unit.SkinColor));
        Belly = new SpriteExtraInfo(1, null, (s) => ColorMap.GetSharkBellyColor(s.Unit.AccessoryColor));
        Head = new SpriteExtraInfo(6, HeadSprite, (s) => ColorMap.GetSlimeColor(s.Unit.ExtraColor1));

        EyeTypes = 3;
        SpecialAccessoryCount = 2;
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
            return SpriteDictionary.Shark[2];
        return SpriteDictionary.Shark[0];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
            return SpriteDictionary.Shark[3];
        return SpriteDictionary.Shark[1];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (!actor.Targetable) return SpriteDictionary.Shark[4 + actor.Unit.SpecialAccessoryType];
        if (State.Rand.Next(200) == 0) actor.SetAnimationMode(1, 1.5f);
        int specialMode = actor.CheckAnimationFrame();
        if (specialMode == 1)
            return SpriteDictionary.Shark[6 + actor.Unit.SpecialAccessoryType];
        return SpriteDictionary.Shark[4 + actor.Unit.SpecialAccessoryType];
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => SpriteDictionary.Shark[9 + actor.Unit.EyeType];

    protected override Sprite MouthSprite(Actor_Unit actor) { if (actor.IsEating || actor.IsAttacking) return SpriteDictionary.Shark[8]; else return null; }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return SpriteDictionary.Shark[12];

        int size = actor.GetStomachSize(22);

        if (size >= 21 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
            return SpriteDictionary.Shark[30];
        }

        if (size >= 19 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.Shark[31];
        }

        if (size >= 17 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.Shark[32];
        }
        if (size > 16) size = 16;
        return SpriteDictionary.Shark[12 + size];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.Unit.Level > 15)
            return SpriteDictionary.Shark[29];
        else
            return null;
    }

}

