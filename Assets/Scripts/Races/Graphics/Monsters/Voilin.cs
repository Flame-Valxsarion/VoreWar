using System.Collections.Generic;
using UnityEngine;

class Voilin : BlankSlate
{
    public Voilin()
    {
        EyeTypes = 3;
        SkinColors = ColorMap.ExoticColorCount; // Under belly, head
        EyeColors = ColorMap.EyeColorCount; // Eye & Spine Colour
        ExtraColors1 = ColorMap.ExoticColorCount; // Plates
        ExtraColors2 = ColorMap.ExoticColorCount; // Limbs
        CanBeGender = new List<Gender>() { Gender.None };
        GentleAnimation = true;

        Body = new SpriteExtraInfo(0, BodySprite, (s) => ColorMap.GetExoticColor(s.Unit.ExtraColor2)); // Base Body
        BodyAccent = new SpriteExtraInfo(2, BodyAccentSprite, (s) => ColorMap.GetExoticColor(s.Unit.ExtraColor2)); // Closer Legs, Head
        BodyAccent2 = new SpriteExtraInfo(3, BodyAccentSprite2, (s) => ColorMap.GetExoticColor(s.Unit.ExtraColor1)); // Back Plates
        BodyAccent3 = new SpriteExtraInfo(4, BodyAccentSprite3, (s) => ColorMap.GetExoticColor(s.Unit.SkinColor)); // Below Head
        Hair = new SpriteExtraInfo(5, HairSprite, WhiteColored); // Teeth
        Eyes = new SpriteExtraInfo(6, EyesSprite, (s) => ColorMap.GetEyeColor(s.Unit.EyeColor)); // Eyes, Spines
        Belly = new SpriteExtraInfo(1, null, (s) => ColorMap.GetExoticColor(s.Unit.SkinColor)); // Belly
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        int size = actor.GetStomachSize(23);

        if (size > 11)
        {
            return SpriteDictionary.Voilin[6];
        }
        if (size > 8)
        {
            return SpriteDictionary.Voilin[3];
        }

        return SpriteDictionary.Voilin[0];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        int size = actor.GetStomachSize(23);

        if (size > 11)
        {
            if (actor.IsAttacking || actor.IsEating) return SpriteDictionary.Voilin[8];
            return SpriteDictionary.Voilin[7];
        }
        if (size > 8)
        {
            if (actor.IsAttacking || actor.IsEating) return SpriteDictionary.Voilin[5];
            return SpriteDictionary.Voilin[4];
        }

        if (actor.IsAttacking || actor.IsEating) return SpriteDictionary.Voilin[2];
        return SpriteDictionary.Voilin[1];

    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        int size = actor.GetStomachSize(23);

        if (size > 14)
        {
            AddOffset(BodyAccent2, 0, (29 + ((size - 15) * 2)) * .625f);
            AddOffset(BodyAccent3, 0, (12 + ((size - 15) * 2)) * .625f);
            AddOffset(Hair, 0, (29 + ((size - 15) * 2)) * .625f);
            AddOffset(Eyes, 0, (29 + ((size - 15) * 2)) * .625f);
            AddOffset(Body, 0, (12 + ((size - 15) * 2)) * .625f);
            AddOffset(BodyAccent, 0, (12 + ((size - 15) * 2)) * .625f);
        }

        else if (size > 11)
        {
            AddOffset(BodyAccent2, 0, (17 + ((size - 12) * 4)) * .625f);
            AddOffset(BodyAccent3, 0, (size - 12) * 4 * .625f);
            AddOffset(Hair, 0, (17 + ((size - 12) * 4)) * .625f);
            AddOffset(Eyes, 0, (17 + ((size - 12) * 4)) * .625f);
            AddOffset(Body, 0, (size - 12) * 4 * .625f);
            AddOffset(BodyAccent, 0, (size - 12) * 4 * .625f);
        }

        else if (size > 8)
        {
            AddOffset(BodyAccent2, 0, 10 * .625f);
            AddOffset(BodyAccent3, 0, 0 * .625f);
            AddOffset(Hair, 0, 10 * .625f);
            AddOffset(Eyes, 0, 10 * .625f);
            AddOffset(Body, 0, 0 * .625f);
            AddOffset(BodyAccent, 0, 0 * .625f);
        }

        else
        {
            AddOffset(BodyAccent2, 0, 0 * .625f);
            AddOffset(BodyAccent3, 0, 0 * .625f);
            AddOffset(Hair, 0, 0 * .625f);
            AddOffset(Eyes, 0, 0 * .625f);
            AddOffset(Body, 0, 0 * .625f);
            AddOffset(BodyAccent, 0, 0 * .625f);
        }

    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        return SpriteDictionary.Voilin[14];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        int size = actor.GetStomachSize(23);

        if (size > 11)
        {
            if (actor.IsAttacking || actor.IsEating) return SpriteDictionary.Voilin[21];
            return SpriteDictionary.Voilin[20];
        }
        if (size > 8)
        {
            if (actor.IsAttacking || actor.IsEating) return SpriteDictionary.Voilin[19];
            return SpriteDictionary.Voilin[18];
        }

        if (actor.IsAttacking || actor.IsEating) return SpriteDictionary.Voilin[16];
        return SpriteDictionary.Voilin[15];

    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating) return SpriteDictionary.Voilin[13];
        return SpriteDictionary.Voilin[12];
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        return SpriteDictionary.Voilin[9 + actor.Unit.EyeType];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        int size = actor.GetStomachSize(23);

        if (size == 0) return null;

        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false) && size >= 22)
            return SpriteDictionary.Voilin[40];
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false) && size >= 20)
            return SpriteDictionary.Voilin[39];
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false) && size >= 18)
            return SpriteDictionary.Voilin[38];
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false) && size >= 16)
            return SpriteDictionary.Voilin[37];

        if (size > 15) size = 15;

        return SpriteDictionary.Voilin[21 + size];

    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.SkinColor = State.Rand.Next(0, SkinColors);
        unit.EyeColor = State.Rand.Next(0, EyeColors);
        unit.ExtraColor1 = State.Rand.Next(0, ExtraColors1);
        unit.ExtraColor2 = State.Rand.Next(0, ExtraColors2);
    }
}
