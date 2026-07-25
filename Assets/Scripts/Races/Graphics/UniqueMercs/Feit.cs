using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Feit : BlankSlate
{
    const float BellyScale = 1.0f;
    public Feit()
    {
        CanBeGender = new List<Gender>() { Gender.Female };
		GentleAnimation = true;
        Body = new SpriteExtraInfo(3, BodySprite, WhiteColored);
		Head = new SpriteExtraInfo(4, HeadSprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, WhiteColored);
        Belly = new SpriteExtraInfo(2, null, WhiteColored);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Feit";
    }


    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        AddOffset(Head, 0, 20 * .85f);
		if ((actor.GetStomachSize(38) <= 11) && (actor.IsOralVoring || actor.IsAttacking)) return SpriteDictionary.Feit[4];
		else if ((actor.GetStomachSize(38) >= 12) && (actor.IsOralVoring || actor.IsAttacking)) return SpriteDictionary.Feit[6];
		else if (actor.GetStomachSize(38) >= 12) return SpriteDictionary.Feit[5];
        else return SpriteDictionary.Feit[3];
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {
        AddOffset(Body, 0, 20 * .85f);
		if (actor.GetStomachSize(38) >= 12) return SpriteDictionary.Feit[2];
		else if (actor.GetStomachSize(38) >= 1) return SpriteDictionary.Feit[1];
        else return SpriteDictionary.Feit[0];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Left Side
    {
        AddOffset(BodyAccent, 0, 20 * .85f);
		if (actor.GetStomachSize(38) >= 12) return SpriteDictionary.Feit[8];
        else return SpriteDictionary.Feit[7];
    }


    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        AddOffset(Belly, 0, 20 * .85f);
        if (!actor.HasBelly)
            return null;

        int size = actor.GetStomachSize(38, BellyScale);

        if ( size >= 38 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
            return SpriteDictionary.Feit[35];
        }

        if (size >= 35 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.Feit[34];
        }

        if (size >= 32 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.Feit[33];
        }

        if (size >= 29 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.Feit[32];
        }

        if (size >= 26 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.Feit[31];
        }

        if (size >= 23 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.Feit[30];
        }

        if (size >= 21 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.Feit[29];
        }

        if (size > 19) size = 19;
        return SpriteDictionary.Feit[9 + size];
    }
}

