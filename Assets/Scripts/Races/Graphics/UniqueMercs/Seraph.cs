using System.Collections.Generic;
using UnityEngine;

class Seraph : BlankSlate
{
    readonly Sprite[] Sprites = SpriteDictionary.Seraph;
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
        int size = actor.GetStomachSize(49);
		
		if (size == 15)
		{
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
		}
		else if (size == 16)
		{
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
		}
		else if (size == 17)
		{
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
		}
		else if (size == 18)
		{
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
		}
		else if (size == 19)
		{
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
		}
		else if (size == 20)
		{
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
		}
		else if (size == 21)
		{
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
		}
		else
        {
            AddOffset(Belly, 0, 0);
        }

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
                return Sprites[49];
            }
            return Sprites[48];
        }
    
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor)
        {
            return Sprites[47];
        }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
        {
            if (actor.IsAbsorbing)
            {
                return Sprites[51];
            }
            return Sprites[50];
        }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Halo Toggle
    {
        if (actor.Unit.BodyAccentType1 == 1)
            switch (actor.Unit.BodyAccentType1)
            {
                case 1: if (actor.IsAttacking)
                            {
                                return Sprites[55];
                            }
                        else
                            return Sprites[54];
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
                case 1: return Sprites[52];
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
                case 1: return Sprites[53];
                case 2: return null;
                default:
                    return null;
            }
        else
            return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (!actor.HasBelly)
            return null;

        int size = actor.GetStomachSize(49);

        if (size >= 49 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
            return Sprites[46];
        }

        else if (size >= 46 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
            return Sprites[45];
        }

        else if (size >= 43 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
            return Sprites[44];
        }

        else if (size >= 40 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
            return Sprites[43];
        }

        else if (size >= 37 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
            return Sprites[42];
        }

        else if (size >= 34 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
            return Sprites[41];
        }

        else if (size >= 31 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
            return Sprites[40];
        }

        else if (size >= 28 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
            return Sprites[39];
        }

        else if (size >= 25 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
            return Sprites[38];
        }

        else if (size >= 22 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
			AddOffset(Belly, 50 * 0.625f, -50 * 0.625f);
            return Sprites[37];
        }

        if (size > 19) size = 19;

        return Sprites[17 + size];
    }
}

