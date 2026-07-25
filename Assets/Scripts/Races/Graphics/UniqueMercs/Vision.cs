using System.Collections.Generic;
using UnityEngine;

class Vision : BlankSlate
{
    public Vision()
    {
        CanBeGender = new List<Gender>() { Gender.Male };
        GentleAnimation = true;
        Body = new SpriteExtraInfo(5, BodySprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, WhiteColored);
        Belly = new SpriteExtraInfo(3, null, WhiteColored);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Vision";
    }

    protected override Sprite BodySprite(Actor_Unit actor) => SpriteDictionary.Vision[actor.IsOralVoring ? 1 : 0];

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => SpriteDictionary.Vision[2];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false)
        {
            if (actor.PredatorComponent.VisibleFullness > 3)
                return SpriteDictionary.Vision[9];
        }

        return actor.HasBelly ? SpriteDictionary.Vision[3 + actor.GetStomachSize(5)] : null;
    }
}

