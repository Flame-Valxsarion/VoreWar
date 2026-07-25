using System.Collections.Generic;
using UnityEngine;

class Cierihaka : BlankSlate
{
    readonly Sprite[] Sprites = SpriteDictionary.Cierihaka;

    public Cierihaka()
    {
        CanBeGender = new List<Gender>() { Gender.Female };

        Body = new SpriteExtraInfo(5, BodySprite, WhiteColored);
        Head = new SpriteExtraInfo(6, HeadSprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(0, BodyAccentSprite, WhiteColored);
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, WhiteColored);
        BodyAccent3 = new SpriteExtraInfo(2, BodyAccentSprite3, WhiteColored);
        BodyAccent4 = new SpriteExtraInfo(3, BodyAccentSprite4, WhiteColored);

    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Cierihaka";
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        return Sprites[1];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[2];
        else if (actor.HasBelly)
            return Sprites[3];
        return null;
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        return Sprites[5];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        return Sprites[4];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        return Sprites[6];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        var sprite = 7 + actor.GetStomachSize(5, 1);
        if (sprite == 12 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
            sprite = 13;
        return Sprites[sprite];

    }

}

