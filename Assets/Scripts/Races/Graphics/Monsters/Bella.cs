using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Bella : BlankSlate
{
    readonly Sprite[] Body1 = SpriteDictionary.BellaBody1;
    readonly Sprite[] Head1 = SpriteDictionary.BellaHead1;
    readonly Sprite[] Arm1 = SpriteDictionary.BellaArm1;
    readonly Sprite[] Body2 = SpriteDictionary.BellaBody2;
    readonly Sprite[] Head2 = SpriteDictionary.BellaHead2;
    readonly Sprite[] Arm2 = SpriteDictionary.BellaArm2;
    readonly Sprite[] Robe1 = SpriteDictionary.BellaRobe1;
    readonly Sprite[] Robe2 = SpriteDictionary.BellaRobe2;

    public Bella()
    {
        FixedGender = true;
        CanBeGender = new List<Gender>() { Gender.Female};
        SkinColors = 1;
        EyeTypes = 1;
        GentleAnimation = false;
        BodySizes = 2;
        BodyAccentTypes2 = 2;

        Body = new SpriteExtraInfo(2, BodySprite, WhiteColored); // body, vore bellies, boobs
        Head = new SpriteExtraInfo(4, HeadSprite, WhiteColored);
        //Belly = new SpriteExtraInfo(4, null, WhiteColored); 
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, WhiteColored); // Arm + staff
        BodyAccent2 = new SpriteExtraInfo(3, BodyAccentSprite2, WhiteColored); // Arm + staff

        clothingColors = 0;
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {
        int stomachSize = actor.GetStomachSize(4, 1);

        //Debug.Log("BodySize: " + actor.Unit.BodySize + " StomachSize:" + stomachSize);

        //Sprites , Weight 1
        if (actor.Unit.BodySize == 0)
        {
            if(actor.HasBelly == true)
                return Body1[1 + stomachSize]; // 4 bellies total

            return Body1[0];  
        }

        //Sprites , Weight 2
        if (actor.Unit.BodySize == 1)
        {
            if (actor.HasBelly == true)
                return Body2[1 + stomachSize];
            return Body2[0];
        }

        
        return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) //Belly and Boob
    {
        return null;
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring == true)
        {
            if (actor.HasBelly == true) //If vore sucess
                return Head1[1];
            return Head1[2]; // if vore fail
        }

        return Head1[0];


        //no variations in head by weight
        /*
        //Weight 1
        if (actor.Unit.BodySize == 0)
        {
            if (actor.IsOralVoring == true)
            {
                if (actor.HasBelly == false) //If vore sucess
                    return Head1[1];
                return Head1[2]; // if vore fail
            }

            return Head1[0];
        }

        //Weight 2
        if (actor.Unit.BodySize == 1)
        {
            if (actor.IsOralVoring == true)
            {
                if (actor.HasBelly == false) //If vore sucess
                    return Head2[1];
                return Head2[2]; // if vore fail
            }

            return Head2[0];
        }
        */

        return null;
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) //Arm and staff
    {

        //Weight 1
        if (actor.Unit.BodySize == 0)
        {
            if (actor.IsAttacking == true)
                return Arm1[1];
            return Arm1[0];
        }

        //Weight 2
        if (actor.Unit.BodySize == 1)
        {
            if (actor.IsAttacking == true)
                return Arm2[1];
            return Arm2[0];
        }

        return null;
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) //Robe
    {

        //Debug.Log(" bodyAccent1 value:" + actor.Unit.BodyAccentType2);
        //Debug.Log("BodySize: " + actor.Unit.BodySize + " StomachSize:" + stomachSize);

        if(actor.Unit.BodyAccentType2 == 1)
        {
            int stomachSize = actor.GetStomachSize(4, 1);

            //Sprites , Weight 1
            if (actor.Unit.BodySize == 0)
            {
                if (actor.HasBelly == true)
                    return Robe1[1 + stomachSize]; // 4 bellies total

                return Robe1[0];
            }

            //Sprites , Weight 2
            if (actor.Unit.BodySize == 1)
            {
                if (actor.HasBelly == true)
                    return Robe2[1 + stomachSize];
                return Robe2[0];
            }
        }


        return null;
    }


    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Bella";
    }
}



