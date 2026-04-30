using System;
using System.Collections.Generic;
using UnityEngine;

class Tatltuae : DefaultRaceData
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.Tatltuae;
    readonly Sprite[] Sprites2 = State.GameManager.SpriteDictionary.TatltuaeHackleVore;
    bool facingFront = true;
    internal Tatltuae()
    {
        CanBeGender = new List<Gender>() { Gender.Male };
        SpecialAccessoryCount = 3;
        ClothingShift = new Vector3(0, 0, 0);
        AvoidedEyeTypes = 0;
        AvoidedMouthTypes = 0;
        BodyAccentTypes1 = 2; // Shirt Toggle
        BodyAccentTypes2 = 2; // Pants Toggle
        BodyAccentTypes3 = 2; // Glasses Toggle
        BodyAccentTypes4 = 2; // Hat Toggle

        HairColors = 1;
        HairStyles = 1;
        SkinColors = 1;
        AccessoryColors = 1;
        EyeTypes = 1;
        EyeColors = 1;
        SecondaryEyeColors = 1;
        BodySizes = 0;
        AllowedMainClothingTypes = new List<MainClothing>();
        AllowedWaistTypes = new List<MainClothing>();
        AllowedClothingHatTypes = new List<ClothingAccessory>();
        MouthTypes = 0;
        AvoidedMainClothingTypes = 0;

        ExtendedBreastSprites = false;

        Body = new SpriteExtraInfo(3, BodySprite, WhiteColored);
        Head = new SpriteExtraInfo(7, HeadSprite, WhiteColored);
        BodyAccessory = null;
        BodyAccent = new SpriteExtraInfo(5, BodyAccentSprite, WhiteColored); // Shirt
        BodyAccent2 = new SpriteExtraInfo(4, BodyAccentSprite2, WhiteColored); // Pants
        BodyAccent3 = new SpriteExtraInfo(9, BodyAccentSprite3, WhiteColored); // Glasses
        BodyAccent4 = new SpriteExtraInfo(10, BodyAccentSprite4, WhiteColored); // Hat
        BodyAccent5 = new SpriteExtraInfo(6, BodyAccentSprite5, WhiteColored); // Hackles!!!
        BodyAccent6 = null;
        Mouth = null;
        Hair = null;
        Hair2 = null;
        Eyes = new SpriteExtraInfo(8, EyesSprite, WhiteColored);
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(12, null, WhiteColored);
        SecondaryBelly = null;
        Weapon = null;
        BackWeapon = null;
        BodySize = null;
        Breasts = null;
        BreastShadow = null;
        Dick = new SpriteExtraInfo(9, DickSprite, WhiteColored); 
        Balls = new SpriteExtraInfo(8, BallsSprite, WhiteColored); 
    }

    internal override int BreastSizes => 1;
    internal override int DickSizes => 1;

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Tatltuae";
        unit.BodyAccentType1 = 1;
        unit.BodyAccentType2 = 1;
        unit.BodyAccentType3 = State.Rand.Next(2);
        unit.BodyAccentType4 = State.Rand.Next(2);
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.IsAnalVoring)
            facingFront = false;
        else if (actor.IsOralVoring || actor.IsAttacking || actor.IsCockVoring || actor.IsBeingRubbed)
            facingFront = true;
        else
            facingFront = true;
        base.RunFirst(actor);
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (facingFront)
        {
            if (actor.IsAttacking)
            {
                if (actor.Unit.BodyAccentType4 == 1)
                    return Sprites[15];
                return Sprites[1];
            }
            else
                return Sprites[0];
        }
        else
            return Sprites[24];
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (facingFront)
        {
            if (actor.Unit.IsDead || actor.IsBeingHurt || actor.Surrendered == true)
                return Sprites[17];
            if (actor.IsBeingRubbed)
                return Sprites[16];
            else
                return null;
        }
        else
            return null;
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (facingFront)
        {
            if (actor.IsAttacking && actor.IsOralVoring)
                return Sprites[5];
            if (actor.IsAttacking)
                return Sprites[3];
            if (actor.IsOralVoring)
                return Sprites[4];
            else
                return Sprites[2];
        }
        else
            return Sprites[23];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // shirt
    {
        if (facingFront)
        {
            if (actor.Unit.BodyAccentType1 == 1) // Toggle
                switch (actor.Unit.SpecialAccessoryType)
                {
                    case 1: // Formal
                    {
                    if (actor.HasBelly == true)
                    {
                        if (actor.IsAttacking)
                            return Sprites[13];
                        else
                            return Sprites[11];
                    }
                    if (actor.IsAttacking)
                        return Sprites[12];
                    else
                        return Sprites[10];
                    }
                    case 2: // Casual
                    {
                    if (actor.HasBelly == true)
                        return null;
                    else
                        return Sprites[7];
                    }
                    default:
                        return null;
                }
            else
                return null;
        }
        else
            return null;
    }


    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // pants
    {
        if (facingFront)
        {
            if (actor.Unit.BodyAccentType2 == 1) // Toggle
                switch (actor.Unit.SpecialAccessoryType)
                {
                    case 1: // Formal
                    {
                    if (actor.PredatorComponent?.BallsFullness > 0 || actor.IsErect() == true)
                        return null;
                    else
                        return Sprites[9];
                    }
                    case 2: // Casual
                    {
                    if (actor.PredatorComponent?.BallsFullness > 0 || actor.IsErect() == true)
                        return null;
                    else
                        return Sprites[8];
                    }
                    default:
                        return null;
                }
            else
                return null;
        }
        else
            return null;
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // glasses
    {
        if (actor.Unit.BodyAccentType3 == 1 && facingFront) // Toggle
            return Sprites[6];
        else
            return null;
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // glasses
    {
        if (actor.Unit.BodyAccentType4 == 1 && facingFront) // Toggle
            return Sprites[14];
        else
            return null;
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // hackles
    {
        int sizet = actor.GetTailSize(2);
        if (actor.Unit.Predator == false || actor.PredatorComponent?.TailFullness == 0 || !facingFront)
            return null;
        else
            {
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.tail) && sizet == 2)
                {
                    AddOffset(Belly, 0, -34 * .625f);
                    return Sprites2[5];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.tail) && sizet == 2)
                {
                    AddOffset(Belly, 0, -34 * .625f);
                    return Sprites2[4];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.tail) && sizet == 1)
                {
                    AddOffset(Belly, 0, -34 * .625f);
                    return Sprites2[3];
                }
                else
                    return Sprites2[(sizet)];
            }
    }


    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)

    {
        if (facingFront)
        {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            Belly.layer = 12;
            int size = actor.GetStomachSize(31, 0.8f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -34 * .625f);
                return Sprites[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -34 * .625f);
                return Sprites[61];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -34 * .625f);
                return Sprites[60];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -34 * .625f);
                return Sprites[59];
            }
            switch (size)
            {
                case 26:
                    AddOffset(Belly, 0, -14 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -17 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -20 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -25 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -27 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -33 * .625f);
                    break;
            }

                return Sprites[27 + size];
            }
            else
            {
                return null;
            }
        }
        else
        {
            if (actor.HasBelly)
            {
                belly.transform.localScale = new Vector3(1, 1, 1);
                if (actor.PredatorComponent.VisibleFullness > 4)
                {
                    float extraCap = actor.PredatorComponent.VisibleFullness - 4;
                    float xScale = Mathf.Min(1 + (extraCap / 5), 1.8f);
                    float yScale = Mathf.Min(1 + (extraCap / 40), 1.1f);
                    belly.transform.localScale = new Vector3(xScale, yScale, 1);
                }
                belly.SetActive(true);
                Belly.layer = 5;
                int size = actor.GetStomachSize(30, 0.8f);
                return Sprites[Math.Min(95 + size, 110)];
            }
            else
            {
                return null;
            }
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (facingFront)
        {
            if (actor.Unit.BodyAccentType2 == 1 && actor.PredatorComponent?.BallsFullness == 0f && actor.IsErect() == false && (actor.Unit.SpecialAccessoryType == 1 || actor.Unit.SpecialAccessoryType == 2))
                return null;
            if (actor.IsErect())
            {
                if (actor.PredatorComponent?.VisibleFullness < .75f)
                {
                    Dick.layer = 19;
                    if (actor.IsCockVoring)
                    {
                        return Sprites[20];
                    }
                    else
                    {
                        return Sprites[19];
                    }
                }
                else
                {
                    Dick.layer = 11;
                    if (actor.IsCockVoring)
                    {
                        return Sprites[22];
                    }
                    else
                    {
                        return Sprites[21];
                    }
                }
            }
            Dick.layer = 9;
            return null;
        }
        else
        {
            Dick.layer = 6;
            return Sprites[26];
        }
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (facingFront)
        {
            if (actor.Unit.HasDick == false)
                return null;
            if (actor.Unit.BodyAccentType2 == 1 && actor.PredatorComponent?.BallsFullness == 0 && actor.IsErect() == false && (actor.Unit.SpecialAccessoryType == 1 || actor.Unit.SpecialAccessoryType == 2))
                return null;
            if (actor.IsErect() && (actor.PredatorComponent?.VisibleFullness < .75f))
            {
                Balls.layer = 18;
            }
            else
            {
                Balls.layer = 8;
            }
            int offset = actor.GetBallSize(27, .8f);
            if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 27)
            {
                AddOffset(Balls, 0, -19 * .625f);
                return Sprites[94];
            }
            else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                AddOffset(Balls, 0, -17 * .625f);
                return Sprites[93];
            }
            else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 26)
            {
                AddOffset(Balls, 0, -15 * .625f);
                return Sprites[92];
            }
            else if (offset >= 25)
            {
                AddOffset(Balls, 0, -9 * .625f);
            }
            else if (offset == 24)
            {
                AddOffset(Balls, 0, -3 * .625f);
            }
            else if (offset == 23)
            {
                AddOffset(Balls, 0, -3 * .625f);
            }
            else if (offset == 22)
            {
                AddOffset(Balls, 0, -2 * .625f);
            }
//            else if (offset == 21)
//            {
//                AddOffset(Balls, 0, -1 * .625f);
//            }
//            else if (offset == 20)
//            {
//                AddOffset(Balls, 0, -1 * .625f);
//            }
//            else if (offset == 20)
//            {
//                AddOffset(Balls, 0, -1 * .625f);
//            }
//            else if (offset == 19)
//            {
//                AddOffset(Balls, 0, -1 * .625f);
//            }

            if (offset > 0)
                return Sprites[Math.Min(64 + offset, 90)];
            return Sprites[63];
        }
        else
        {
            Balls.layer = 8;
            int offset = actor.GetBallSize(15, .8f);
            if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 15)
            {
                AddOffset(Balls, 0, -25 * .625f);
                return Sprites[129];
            }
            else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 15)
            {
                AddOffset(Balls, 0, -23 * .625f);
                return Sprites[128];
            }
            else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 14)
            {
                AddOffset(Balls, 0, -21 * .625f);
                return Sprites[127];
            }
            else if (offset >= 13)
            {
                AddOffset(Balls, 0, -10 * .625f);
            }
            else if (offset == 12)
            {
                AddOffset(Balls, 0, -8 * .625f);
            }
            else if (offset == 11)
            {
                AddOffset(Balls, 0, -6 * .625f);
            }
            else if (offset == 10)
            {
                AddOffset(Balls, 0, -4 * .625f);
            }
            if (offset > 0)
            {
                return Sprites[Math.Min(110 + offset, 125)];
            }
            return Sprites[25];
        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => null;
    protected override Sprite BackWeaponSprite(Actor_Unit actor) => null;
    protected override Sprite SecondaryBellySprite(Actor_Unit actor) => null;
    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsShadowSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsSprite(Actor_Unit actor) => null;
    internal override Color ClothingColor(Actor_Unit actor) => Color.white;
    protected override Color EyeColor(Actor_Unit actor) => Color.white;
    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => null;
    protected override Color HairColor(Actor_Unit actor) => Color.white;
    protected override Sprite HairSprite(Actor_Unit actor) => null;
    protected override Sprite HairSprite2(Actor_Unit actor) => null;
    protected override Sprite MouthSprite(Actor_Unit actor) => null;
    protected override Color ScleraColor(Actor_Unit actor) => Color.white;
    protected override Sprite WeaponSprite(Actor_Unit actor) => null;
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => null;


}

