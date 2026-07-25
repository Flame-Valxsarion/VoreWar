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
        return SpriteDictionary.Nectar[0];
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (actor.IsOralVoring) return SpriteDictionary.Nectar[2];
		return SpriteDictionary.Nectar[1];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Right Foreleg
    {
        if (actor.IsAttacking) return SpriteDictionary.Nectar[5];
		return SpriteDictionary.Nectar[4];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Left Hindleg
    {
		return SpriteDictionary.Nectar[3];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Underbelly
    {
		return SpriteDictionary.Nectar[6];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Right hindleg
    {
		return SpriteDictionary.Nectar[7];
    }



    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // Belly
    {
        if (actor.HasBelly == false)
            return null;

        int size = actor.GetStomachSize(53); //overly high for better belly progression due to high base stomach size stat

        if (size >= 18 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.Nectar[36];
        }

        if (size >= 12 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            if (size >= 18) return SpriteDictionary.Nectar[35];
            if (size >= 17) return SpriteDictionary.Nectar[34];
            if (size >= 16) return SpriteDictionary.Nectar[33];
            if (size >= 15) return SpriteDictionary.Nectar[32];
            if (size >= 14) return SpriteDictionary.Nectar[31];
            if (size >= 13) return SpriteDictionary.Nectar[30];
            if (size >= 12) return SpriteDictionary.Nectar[29];
        }
        if (size > 20) size = 20;
        return SpriteDictionary.Nectar[8 + size];
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
                return SpriteDictionary.Nectar[74];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 31)
            {
                return SpriteDictionary.Nectar[73];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return SpriteDictionary.Nectar[72];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 25)
            {
                return SpriteDictionary.Nectar[71];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 22)
            {
                return SpriteDictionary.Nectar[70];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 19)
            {
                return SpriteDictionary.Nectar[69];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 16)
            {
                return SpriteDictionary.Nectar[68];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 13)
            {
                return SpriteDictionary.Nectar[67];
            }

            if (leftSize > 9) leftSize = 9;
            
            return SpriteDictionary.Nectar[57 + leftSize];
        }
        else
        {
            return SpriteDictionary.Nectar[56];
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
                return SpriteDictionary.Nectar[55];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 31)
            {
                return SpriteDictionary.Nectar[54];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return SpriteDictionary.Nectar[53];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 25)
            {
                return SpriteDictionary.Nectar[52];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 22)
            {
                return SpriteDictionary.Nectar[51];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 19)
            {
                return SpriteDictionary.Nectar[50];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 16)
            {
                return SpriteDictionary.Nectar[49];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 13)
            {
                return SpriteDictionary.Nectar[48];
            }

            if (rightSize > 9) rightSize = 9;

            return SpriteDictionary.Nectar[38 + rightSize];
        }
        else
        {
            return SpriteDictionary.Nectar[37];
        }
    }








}

