using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Nectar : BlankSlate
{

    bool oversize = false;

    public Nectar()
    {
        ExtendedBreastSprites = true;
        CanBeGender = new List<Gender>() { Gender.Female };
        GentleAnimation = true;
        Body = new SpriteExtraInfo(9, BodySprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, WhiteColored);
        BodyAccent2 = new SpriteExtraInfo(5, BodyAccentSprite2, WhiteColored);
        BodyAccent3 = new SpriteExtraInfo(2, BodyAccentSprite3, WhiteColored);
        BodyAccent4 = new SpriteExtraInfo(1, BodyAccentSprite4, WhiteColored);
        Breasts = new SpriteExtraInfo(8, BreastsSprite, WhiteColored);
        SecondaryBreasts = new SpriteExtraInfo(7, SecondaryBreastsSprite, WhiteColored);
        Head = new SpriteExtraInfo(10, HeadSprite, WhiteColored);
        Belly = new SpriteExtraInfo(4, null, WhiteColored);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Nectar";
    }

    internal override void RunFirst(Actor_Unit actor)
    {
		int size = actor.GetStomachSize(53);

		if (size > 9)
		{
			Belly.layer = 6;
		}
	}

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {
        return State.GameManager.SpriteDictionary.Nectar[0];
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (actor.IsOralVoring) return State.GameManager.SpriteDictionary.Nectar[2];
		return State.GameManager.SpriteDictionary.Nectar[1];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Right Foreleg
    {
        if (actor.IsAttacking) return State.GameManager.SpriteDictionary.Nectar[5];
		return State.GameManager.SpriteDictionary.Nectar[4];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Left Hindleg
    {
		return State.GameManager.SpriteDictionary.Nectar[3];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Underbelly
    {
		return State.GameManager.SpriteDictionary.Nectar[6];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Right hindleg
    {
		return State.GameManager.SpriteDictionary.Nectar[7];
    }



    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // Belly
    {
        if (actor.HasBelly == false)
            return null;

        int size = actor.GetStomachSize(53);

        if (size >= 53 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.Nectar[36];
        }

        else if (size >= 50 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.Nectar[35];
        }

        else if (size >= 45 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.Nectar[34];
        }

        else if (size >= 44 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.Nectar[33];
        }

        else if (size >= 41 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.Nectar[32];
        }

        else if (size >= 38 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.Nectar[31];
        }

        else if (size >= 35 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.Nectar[30];
        }

        else if (size >= 32 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return State.GameManager.SpriteDictionary.Nectar[29];
        }

        if (size > 28) size = 28;
        return State.GameManager.SpriteDictionary.Nectar[8 + size];
    }

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = actor.GetLeftBreastSize(34);

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 34)
            {
                return State.GameManager.SpriteDictionary.Nectar[74];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 31)
            {
                return State.GameManager.SpriteDictionary.Nectar[73];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return State.GameManager.SpriteDictionary.Nectar[72];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 25)
            {
                return State.GameManager.SpriteDictionary.Nectar[71];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 22)
            {
                return State.GameManager.SpriteDictionary.Nectar[70];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 19)
            {
                return State.GameManager.SpriteDictionary.Nectar[69];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 16)
            {
                return State.GameManager.SpriteDictionary.Nectar[68];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 13)
            {
                return State.GameManager.SpriteDictionary.Nectar[67];
            }

            if (leftSize > 9) leftSize = 9;
            
            return State.GameManager.SpriteDictionary.Nectar[57 + leftSize];
        }
        else
        {
            return State.GameManager.SpriteDictionary.Nectar[56];
        }
    }

    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize = actor.GetRightBreastSize(34);

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 34)
            {
                return State.GameManager.SpriteDictionary.Nectar[55];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 31)
            {
                return State.GameManager.SpriteDictionary.Nectar[54];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return State.GameManager.SpriteDictionary.Nectar[53];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 25)
            {
                return State.GameManager.SpriteDictionary.Nectar[52];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 22)
            {
                return State.GameManager.SpriteDictionary.Nectar[51];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 19)
            {
                return State.GameManager.SpriteDictionary.Nectar[50];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 16)
            {
                return State.GameManager.SpriteDictionary.Nectar[49];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 13)
            {
                return State.GameManager.SpriteDictionary.Nectar[48];
            }

            if (rightSize > 9) rightSize = 9;

            return State.GameManager.SpriteDictionary.Nectar[38 + rightSize];
        }
        else
        {
            return State.GameManager.SpriteDictionary.Nectar[37];
        }
    }








}

