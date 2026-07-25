using System.Collections.Generic;
using UnityEngine;

class Vagrants : BlankSlate
{
    Sprite[] Sprites;
    readonly Sprite[][] VagrantSprites =
    {
        SpriteDictionary.Vagrants,
        SpriteDictionary.Vagrants2,
        SpriteDictionary.Vagrants3
    };

    public Vagrants()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        SkinColors = 3;

        Body = new SpriteExtraInfo(1, BodySprite, WhiteColored);
        BodyAccessory = new SpriteExtraInfo(4, AccessorySprite, WhiteColored); // tentacles
        BodyAccent = null;
        BodyAccent2 = null;
        Mouth = null;
        Hair = null;
        Hair2 = null;
        Eyes = null;
        SecondaryEyes = null;
        SecondaryAccessory = new SpriteExtraInfo(3, SecondaryAccessorySprite, WhiteColored); // underneath
        Belly = new SpriteExtraInfo(2, null, WhiteColored);
        Weapon = null;
        BackWeapon = null;
        BodySize = null;
        Breasts = null;
        BreastShadow = null;
        Dick = null;
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        base.RunFirst(actor);
        Sprites = VagrantSprites[Mathf.Clamp(actor.Unit.SkinColor, 0, 2)];
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Body, 0, 60 * .625f);
        AddOffset(BodyAccessory, 0, 60 * .625f);
        AddOffset(SecondaryAccessory, 0, 60 * .625f);
        AddOffset(Belly, 0, 60 * .625f);
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating)
        {
            return Sprites[28];
        }
        else
        {
            return Sprites[5];
        }
    }

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) // underneath
    {
        if (actor.IsAttacking || actor.IsEating)
        {
            return Sprites[1];
        }
        else
        {
            return Sprites[0];
        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // tentacles
    {
        if (actor.IsEating)
        {
            return Sprites[3];
        }
        else if (actor.IsAttacking)
        {
            return Sprites[4];
        }
        else
        {
            return Sprites[2];
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            if (actor.IsAttacking || actor.IsEating)
            {
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
                    return Sprites[50];
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
                {
                    if (actor.GetStomachSize(16, .60f) == 16)
                        return Sprites[49];
                    else if (actor.GetStomachSize(16, .70f) == 16)
                        return Sprites[48];
                    else if (actor.GetStomachSize(16, .80f) == 16)
                        return Sprites[47];
                    else if (actor.GetStomachSize(16, .90f) == 16)
                        return Sprites[46];
                }
                return Sprites[29 + actor.GetStomachSize(16)];
            }
            else
            {
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
                    return Sprites[27];
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
                {
                    if (actor.GetStomachSize(16, .60f) == 16)
                        return Sprites[26];
                    else if (actor.GetStomachSize(16, .70f) == 16)
                        return Sprites[25];
                    else if (actor.GetStomachSize(16, .80f) == 16)
                        return Sprites[24];
                    else if (actor.GetStomachSize(16, .90f) == 16)
                        return Sprites[23];
                }
                return Sprites[6 + actor.GetStomachSize(16)];
            }
        }
        else
        {
            return null;
        }
    }

}
