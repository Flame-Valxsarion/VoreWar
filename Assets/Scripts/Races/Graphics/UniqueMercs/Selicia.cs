using System.Collections.Generic;
using UnityEngine;

class Selicia : BlankSlate
{
    const float BellyScale = 0.9f;
    public Selicia()
    {
        CanBeGender = new List<Gender>() { Gender.Female };
        GentleAnimation = true;
        Body = new SpriteExtraInfo(5, BodySprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, WhiteColored);
        BodyAccent2 = new SpriteExtraInfo(2, BodyAccentSprite2, WhiteColored);
        Head = new SpriteExtraInfo(10, HeadSprite, WhiteColored);
        BreastShadow = new SpriteExtraInfo(4, BreastsShadowSprite, WhiteColored);
        Belly = new SpriteExtraInfo(3, null, WhiteColored);
        BodySize = new SpriteExtraInfo(4, BodySizeSprite, WhiteColored);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Selicia";

    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        int size = actor.GetStomachSize(14, BellyScale);
        const float defaultYOffset = 20 * .625f;
        if (size == 14)
        {
            AddOffset(Body, 0, defaultYOffset + 0);
            AddOffset(Head, 16 * .625f, defaultYOffset + 16 * .625f);
            AddOffset(BodyAccent, 0, defaultYOffset + 0);
            AddOffset(BreastShadow, 0, defaultYOffset + 0);
            AddOffset(Belly, 0, defaultYOffset);
            AddOffset(BodyAccent2, 0, defaultYOffset);
            AddOffset(BodySize, 0, defaultYOffset);
        }
        else if (size == 13)
        {
            AddOffset(Body, 0, defaultYOffset + -8 * .625f);
            AddOffset(Head, 16 * .625f, defaultYOffset + 8 * .625f);
            AddOffset(BodyAccent, 0, defaultYOffset + -8 * .625f);
            AddOffset(BreastShadow, 0, defaultYOffset + -8 * .625f);
            AddOffset(Belly, 0, defaultYOffset);
            AddOffset(BodyAccent2, 0, defaultYOffset);
            AddOffset(BodySize, 0, defaultYOffset);
        }
        else if (size == 12)
        {
            AddOffset(Body, 0, defaultYOffset + -16 * .625f);
            AddOffset(Head, 16 * .625f, defaultYOffset + 0);
            AddOffset(BodyAccent, 0, defaultYOffset + -16 * .625f);
            AddOffset(BreastShadow, 0, defaultYOffset + -16 * .625f);
            AddOffset(Belly, 0, defaultYOffset);
            AddOffset(BodyAccent2, 0, defaultYOffset);
            AddOffset(BodySize, 0, defaultYOffset);
        }
        else
        {
            AddOffset(Body, 0, defaultYOffset);
            AddOffset(Head, 0, defaultYOffset);
            AddOffset(BodyAccent, 0, defaultYOffset);
            AddOffset(BreastShadow, 0, defaultYOffset);
            AddOffset(Belly, 0, defaultYOffset);
            AddOffset(BodyAccent2, 0, defaultYOffset);
            AddOffset(BodySize, 0, defaultYOffset);
        }
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        int size = actor.GetStomachSize(14, BellyScale);
        Belly.layer = 6;
        if (size >= 12)
        {
            Belly.layer = 3;
            return SpriteDictionary.Selicia[5];
        }
        if (size >= 5)
        {
            return SpriteDictionary.Selicia[2];
        }

        if (actor.IsAttacking || actor.IsEating)
            return SpriteDictionary.Selicia[2];
        return SpriteDictionary.Selicia[1];


    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        int size = actor.GetStomachSize(14, BellyScale);
        if (actor.GetStomachSize(14, BellyScale) < 5)
        {
            if (actor.IsAttacking || actor.IsEating)
            {
                BodyAccent.layer = 7;
                return SpriteDictionary.Selicia[3];
            }
            BodyAccent.layer = 1;
            return SpriteDictionary.Selicia[0];
        }
        if (size < 9)
        {
            BodyAccent.layer = 7;
            return SpriteDictionary.Selicia[3];
        }
        if (size < 12)
        {
            BodyAccent.layer = 7;
            return SpriteDictionary.Selicia[4];
        }


        return null;
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (actor.HasBelly)
        {
            if (actor.IsAttacking || actor.IsEating || actor.GetStomachSize(14, BellyScale) >= 5)
                return null;
            return SpriteDictionary.Selicia[40 + actor.GetStomachSize(14, BellyScale)];
        }


        return null;
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsUnbirthing)
            return SpriteDictionary.Selicia[7];
        if (actor.IsOralVoring)
            return SpriteDictionary.Selicia[8];
        if (actor.GetStomachSize(14, BellyScale) < 5)
            return SpriteDictionary.Selicia[6];

        if (State.Rand.Next(450) == 0) actor.SetAnimationMode(1, .75f);
        int specialMode = actor.CheckAnimationFrame();
        if (specialMode == 1)
            return SpriteDictionary.Selicia[9];

        return SpriteDictionary.Selicia[7];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                return SpriteDictionary.Selicia[33];
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                if (actor.GetStomachSize(14, BellyScale * 0.7f) == 14)
                    return SpriteDictionary.Selicia[32];
                else if (actor.GetStomachSize(14, BellyScale * 0.8f) == 14)
                    return SpriteDictionary.Selicia[31];
                else if (actor.GetStomachSize(14, BellyScale * 0.9f) == 14)
                    return SpriteDictionary.Selicia[30];
            }
            if (actor.IsAttacking || actor.IsEating || actor.GetStomachSize(14, BellyScale) >= 5)
                return SpriteDictionary.Selicia[15 + actor.GetStomachSize(14, BellyScale)];
            return SpriteDictionary.Selicia[10 + actor.GetStomachSize(14, BellyScale)];
        }

        return null;
    }

    protected override Sprite BreastsShadowSprite(Actor_Unit actor)
    {
        if (actor.GetStomachSize(14, BellyScale) >= 12)
            return SpriteDictionary.Selicia[34];
        return null;
    }

    protected override Sprite BodySizeSprite(Actor_Unit actor)
    {
        if (actor.GetStomachSize(14, BellyScale) < 5)
            return SpriteDictionary.Selicia[45];
        return null;
    }

}

