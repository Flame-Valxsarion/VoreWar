using System.Collections.Generic;
using UnityEngine;

class Cherub : BlankSlate
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.Cherub;
    internal Cherub()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        Head = new SpriteExtraInfo(5, HeadSprite, WhiteColored);
        Body = new SpriteExtraInfo(7, BodySprite, WhiteColored);
        BodyAccentTypes1 = 2; // Halo
        BodyAccentTypes2 = 2; // Wings
        BodyAccent = new SpriteExtraInfo(2, BodyAccentSprite, WhiteColored);
        BodyAccent2 = new SpriteExtraInfo(3, BodyAccentSprite2, WhiteColored);
        Belly = new SpriteExtraInfo(8, null, WhiteColored);
        clothingColors = 0;
        BodySize = new SpriteExtraInfo(5, BodySizeSprite, WhiteColored);
        BodySizes = 4;        
    }

    internal override void SetBaseOffsets(Actor_Unit actor) // Offset to give the floaty view
    {
        int offset = 20;
        AddOffset(Body, 0, offset * .625f);
        AddOffset(BodyAccent, 0, offset * .625f);
        AddOffset(BodyAccent2, 0, offset * .625f);
        AddOffset(Head, 0, offset * .625f);
        AddOffset(Belly, 0, offset * .625f);

    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Cherub";
        unit.BodySize = 0;
        unit.BodyAccentType1 = 1;
        unit.BodyAccentType2 = 1;
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
        {
            return Sprites[2];
        }
        if (actor.IsAnalVoring || actor.IsBeingRubbed)
        {
            return Sprites[6];
        }
        if (actor.IsAttacking)
        {
            return Sprites[1];
        }
        if (actor.Unit.IsDead || actor.Surrendered == true)
        {
            return Sprites[5];
        }
        if (actor.IsAbsorbing)
        {
            return Sprites[4];
        }
        if (actor.HasJustVored)
        {
            return Sprites[3];
        }
        return Sprites[0];
    }

    protected override Sprite BodySprite(Actor_Unit actor)
        {
            if (actor.IsAttacking)
                return Sprites[13 + actor.Unit.BodySize];
            return Sprites[9 + actor.Unit.BodySize];
        }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Halo Toggle
    {
        if (actor.Unit.BodyAccentType1 == 1)
            switch (actor.Unit.BodyAccentType1)
            {
                case 1: return Sprites[8];
                case 2: return Sprites[26];
                default:
                    return null;
            }
        else
            return null;
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Wings Toggle
    {
        if (actor.Unit.BodyAccentType2 == 1)
            switch (actor.Unit.BodyAccentType2)
            {
                case 1: return Sprites[7];
                case 2: return Sprites[26];
                default:
                    return null;
            }
        else
            return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        /// if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false)
        ///{
        ///    if (actor.PredatorComponent.VisibleFullness > 3)
        ///        return State.GameManager.SpriteDictionary.Cherub[10];
        ///}

        return actor.HasBelly ? State.GameManager.SpriteDictionary.Cherub[18 + actor.GetStomachSize(7)] : null;
    }
}

