using System.Collections.Generic;
using UnityEngine;

class Seraph : BlankSlate
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.Seraph;
    internal Seraph()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        Head = new SpriteExtraInfo(12, HeadSprite, WhiteColored);
        Body = new SpriteExtraInfo(8, BodySprite, WhiteColored);
        BodyAccessory = new SpriteExtraInfo(10, AccessorySprite, WhiteColored); // Leg overlay
        BodyAccentTypes1 = 2; // Halo
        BodyAccentTypes2 = 2; // Wings1
        BodyAccentTypes3 = 2; // Wings2
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, WhiteColored);
        BodyAccent2 = new SpriteExtraInfo(11, BodyAccentSprite2, WhiteColored);
        BodyAccent3 = new SpriteExtraInfo(2, BodyAccentSprite3, WhiteColored);
        BodyAccent4 = new SpriteExtraInfo(2, BodyAccentSprite4, WhiteColored); // Tail
        Belly = new SpriteExtraInfo(9, null, WhiteColored);
        Weapon = new SpriteExtraInfo(3, WeaponSprite, WhiteColored); // Left (Attacking) Arm
        SecondaryAccessory = new SpriteExtraInfo(11, SecondaryAccessorySprite, WhiteColored); // Right Arm
        clothingColors = 0;
        BodySize = new SpriteExtraInfo(6, BodySizeSprite, WhiteColored);
        BodySizes = 5;        
    }

    internal override void SetBaseOffsets(Actor_Unit actor) // Offset to give the floaty view
    {
        int offset = 85;
        AddOffset(Body, 0, offset * .625f);
        AddOffset(BodyAccent, 0, offset * .625f);
        AddOffset(BodyAccent2, 0, offset * .625f);
        AddOffset(BodyAccent3, 0, offset * .625f);
        AddOffset(Head, 0, offset * .625f);
        AddOffset(Belly, 0, offset * .625f);
        AddOffset(BodyAccessory, 0, offset * .625f);
        AddOffset(Weapon, 0, offset * .625f);
        AddOffset(SecondaryAccessory, 0, offset * .625f);
        AddOffset(BodyAccent4, 0, offset * .625f);

    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Seraph";
        unit.BodySize = 0;
        unit.BodyAccentType1 = 1;
        unit.BodyAccentType2 = 1;
        unit.BodyAccentType3 = 1;
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
        {
            return Sprites[13];
        }
        if (actor.IsAnalVoring || actor.IsBeingRubbed)
        {
            return Sprites[12];
        }
        if (actor.IsAttacking)
        {
            return Sprites[14];
        }
        if (actor.Unit.IsDead || actor.Surrendered == true)
        {
            return Sprites[15];
        }
        if (actor.IsAbsorbing)
        {
            return Sprites[11];
        }
        if (actor.HasJustVored)
        {
            return Sprites[16];
        }
        return Sprites[10];
    }

    protected override Sprite BodySprite(Actor_Unit actor)
        {
            return Sprites[0 + actor.Unit.BodySize];
        }
    
    protected override Sprite AccessorySprite(Actor_Unit actor)
        {
            return Sprites[5 + actor.Unit.BodySize];
        }

    protected override Sprite WeaponSprite(Actor_Unit actor)
        {
            if (actor.IsAttacking)
            {
                return Sprites[34];
            }
            return Sprites[33];
        }
    
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor)
        {
            return Sprites[32];
        }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
        {
            if (actor.IsAbsorbing)
            {
                return Sprites[36];
            }
            return Sprites[35];
        }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Halo Toggle
    {
        if (actor.Unit.BodyAccentType1 == 1)
            switch (actor.Unit.BodyAccentType1)
            {
                case 1: if (actor.IsAttacking)
                            {
                                return Sprites[40];
                            }
                        else
                            return Sprites[39];
                case 2: return null;
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
                case 1: return Sprites[37];
                case 2: return null;
                default:
                    return null;
            }
        else
            return null;
    }

    

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Wings2 Toggle
    {
        if (actor.Unit.BodyAccentType3 == 1)
            switch (actor.Unit.BodyAccentType3)
            {
                case 1: return Sprites[38];
                case 2: return null;
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
        ///if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false)
        ///{
        ///    if (actor.PredatorComponent.VisibleFullness > 3)
        ///        return State.GameManager.SpriteDictionary.Seraph[10];
        ///}

        return actor.HasBelly ? State.GameManager.SpriteDictionary.Seraph[18 + actor.GetStomachSize(13)] : null;
    }
}

