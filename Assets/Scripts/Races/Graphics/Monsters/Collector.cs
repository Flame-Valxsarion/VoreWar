using System.Collections.Generic;
using UnityEngine;

class Collector : BlankSlate
{
    public Collector()
    {
        SkinColors = ColorMap.LizardColorCount; // Majority of the body
        EyeColors = ColorMap.EyeColorCount; // Eyes
        ExtraColors1 = ColorMap.LizardColorCount; // Plates, claws
        ExtraColors2 = ColorMap.WyvernColorCount; // Flesh in mouth, dicks
        CanBeGender = new List<Gender>() { Gender.Male };
        GentleAnimation = true;

        Body = new SpriteExtraInfo(0, BodySprite, (s) => ColorMap.GetLizardColor(s.Unit.SkinColor)); // Body
        BodyAccent = new SpriteExtraInfo(2, BodyAccentSprite, (s) => ColorMap.GetLizardColor(s.Unit.ExtraColor1)); // Plates
        BodyAccent2 = new SpriteExtraInfo(4, BodyAccentSprite2, (s) => ColorMap.GetLizardColor(s.Unit.SkinColor)); // Closer Legs
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, (s) => ColorMap.GetLizardColor(s.Unit.ExtraColor1)); // Closer Legs Claws
        Head = new SpriteExtraInfo(6, HeadSprite, (s) => ColorMap.GetWyvernColor(s.Unit.ExtraColor2)); // Mouth Parts
        Hair = new SpriteExtraInfo(7, HairSprite, WhiteColored); // Teeth
        Eyes = new SpriteExtraInfo(8, EyesSprite, (s) => ColorMap.GetEyeColor(s.Unit.EyeColor)); // Eyes
        Dick = new SpriteExtraInfo(1, DickSprite, (s) => ColorMap.GetWyvernColor(s.Unit.ExtraColor2)); // Dicks
        Belly = new SpriteExtraInfo(3, null, (s) => ColorMap.GetLizardColor(s.Unit.SkinColor)); // Belly
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating) return SpriteDictionary.Collector[1];
        return SpriteDictionary.Collector[0];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating) return SpriteDictionary.Collector[5];
        return SpriteDictionary.Collector[4];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        return SpriteDictionary.Collector[2];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        return SpriteDictionary.Collector[3];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating) return SpriteDictionary.Collector[7];
        return null;
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating) return SpriteDictionary.Collector[8];
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating) return null;
        return SpriteDictionary.Collector[6];
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (Config.ErectionsFromVore && actor.HasBelly) return SpriteDictionary.Collector[20];
        return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false)
        {
            if (actor.PredatorComponent.VisibleFullness > 4)
                return SpriteDictionary.Collector[19];
        }
        if (actor.HasBelly == false)
            return null;
        return SpriteDictionary.Collector[9 + actor.GetStomachSize(9, .8f)];
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.SkinColor = State.Rand.Next(0, SkinColors);
        unit.EyeColor = State.Rand.Next(0, EyeColors);
    }
}

