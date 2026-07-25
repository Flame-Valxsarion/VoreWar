using System;
using System.Collections.Generic;
using UnityEngine;

class Olivia : DefaultRaceData
{
    readonly Sprite[] Sprite1 = SpriteDictionary.Olivia;
    readonly Sprite[] Sprite2 = SpriteDictionary.OliviaVoreSprites;

    internal Olivia()//Character by Supra on Discord
    {
        CanBeGender = new List<Gender>() { Gender.Female, Gender.Gynomorph };
        SpecialAccessoryCount = 2;
        BodyAccentTypes1 = 2;
        ClothingShift = new Vector3(0, 0, 0);
        AvoidedEyeTypes = 0;
        AvoidedMouthTypes = 0;

        HairColors = 1;
        HairStyles = 1;
        SkinColors = 1;
        AccessoryColors = 1;
        EyeTypes = 1;
        EyeColors = 1;
        SecondaryEyeColors = 1;
        BodySizes = 3;
        AllowedMainClothingTypes = new List<MainClothing>();
        AllowedWaistTypes = new List<MainClothing>();
        AllowedClothingHatTypes = new List<ClothingAccessory>();
        MouthTypes = 0;
        AvoidedMainClothingTypes = 0;

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(4, BodySprite, WhiteColored);
        Head = new SpriteExtraInfo(6, HeadSprite, WhiteColored);
        BodyAccessory = new SpriteExtraInfo(7, AccessorySprite, WhiteColored);//Shirt
        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, WhiteColored);//Pants
        BodyAccent2 = new SpriteExtraInfo(18, BodyAccentSprite2, WhiteColored);//Necklace
        BodyAccent3 = null;
        BodyAccent4 = null;
        BodyAccent5 = null;
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        BodyAccent9 = null;
        BodyAccent10 = null;
        Mouth = null;
        Hair = new SpriteExtraInfo(2, HairSprite, WhiteColored);
        Hair2 = new SpriteExtraInfo(2, HairSprite2, WhiteColored);//Tail
        Eyes = null;
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(15, null, WhiteColored);
        SecondaryBelly = null;
        Weapon = new SpriteExtraInfo(5, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, WhiteColored);//
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, WhiteColored);//
        BreastShadow = null;
        Dick = new SpriteExtraInfo(12, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(11, BallsSprite, WhiteColored);
    }

    //internal override void SetBaseOffsets(Actor_Unit actor)

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Olivia";
        unit.SpecialAccessoryType = 1;
        unit.BodyAccentType1 = 1;
        unit.BodySize = 0;
    }
    internal override int BreastSizes => 1;
    internal override int DickSizes => 1;


    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprite1[1];
        else
            return Sprite1[0];
    }
    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsAttacking)
            return Sprite1[15 + actor.Unit.BodySize];
        else
            return Sprite1[8 + actor.Unit.BodySize];
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        return Sprite1[5];
    }

    protected override Sprite HairSprite2(Actor_Unit actor)//Tail
    {
        return Sprite1[4];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor)//shirt
    {
        if (actor.PredatorComponent?.LeftBreastFullness > 0.1 || actor.PredatorComponent?.RightBreastFullness > 0.1)
            return null;
        else if (actor.Unit.SpecialAccessoryType == 1)
        {
            if (actor.IsAttacking)
                return Sprite1[18 + actor.Unit.BodySize];
            else
                return Sprite1[11 + actor.Unit.BodySize];
        }
        else
            return null;
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)//Pants
    {
        int offset = actor.GetBallSize(28, .8f);
        if (offset > 0)
            return null;
        if (actor.Unit.BodyAccentType1 == 1)
            return Sprite1[23 + actor.Unit.BodySize];
        else
            return null;
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)//Necklace
    {
        actor.Unit.Pronouns = new List<string> { "she", "her", "her", "hers", "herself", "singular" };//ensures regurardless of gender Pronouns remain the same
        return Sprite1[6];
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking && actor.Unit.BodySize > 2)
            return Sprite1[22];
        else if (actor.IsAttacking)
            return Sprite1[21];
        else
            return null;
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        int offset = actor.GetBallSize(28, .8f);
        if (actor.Unit.HasDick == false || actor.Unit.HasDick == true && actor.Unit.BodyAccentType1 == 1 && offset <= 0)
            return null;
        if (actor.Unit.BodySize == 2)
            return Sprite1[30];
        return Sprite1[28];
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect() && (actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
        {
            Balls.layer = 11;
        }
        else
        {
            Balls.layer = 11;
        }
        int size = actor.Unit.DickSize;
        int offset = actor.GetBallSize(28, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return Sprite2[141];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return Sprite2[140];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return Sprite2[139];
        }
        else if (offset >= 26)
        {
            AddOffset(Balls, 0, -22 * .625f);
        }
        else if (offset == 25)
        {
            AddOffset(Balls, 0, -16 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -13 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -11 * .625f);
        }
        else if (offset == 22)
        {
            AddOffset(Balls, 0, -10 * .625f);
        }
        else if (offset == 21)
        {
            AddOffset(Balls, 0, -7 * .625f);
        }
        else if (offset == 20)
        {
            AddOffset(Balls, 0, -6 * .625f);
        }
        else if (offset == 19)
        {
            AddOffset(Balls, 0, -4 * .625f);
        }
        else if (offset == 18)
        {
            AddOffset(Balls, 0, -1 * .625f);
        }

        if (offset > 0)
            return Sprite2[Math.Min(112 + offset, 138)];
        if (actor.Unit.HasDick == true && actor.Unit.BodyAccentType1 == 1)
            return Sprite1[32];
        if (actor.Unit.BodySize == 2)
            return Sprite1[31];
        return Sprite1[29];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(31, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprite2[105];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprite2[104];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprite2[103];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprite2[102];
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
                    AddOffset(Belly, 0, -32 * .625f);
                    break;
            }

            return Sprite2[70 + size];
        }
        else
        {
            return null;
        }
    }


    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f));
            if (leftSize > actor.Unit.DefaultBreastSize)
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
            {
                return Sprite2[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return Sprite2[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return Sprite2[29];
            }

            if (leftSize > 28)
                leftSize = 28;

            return Sprite2[0 + leftSize];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f));
            if (rightSize > actor.Unit.DefaultBreastSize)
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
            {
                return Sprite2[63];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return Sprite2[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return Sprite2[61];
            }

            if (rightSize > 28)
                rightSize = 28;

            return Sprite2[32 + rightSize];
        }
        else
        {
            return null;
        }
    }
    protected override Sprite BackWeaponSprite(Actor_Unit actor) => null;
    protected override Sprite SecondaryBellySprite(Actor_Unit actor) => null;
    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsShadowSprite(Actor_Unit actor) => null;
    internal override Color ClothingColor(Actor_Unit actor) => Color.white;
    protected override Color EyeColor(Actor_Unit actor) => Color.white;
    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => null;
    protected override Sprite EyesSprite(Actor_Unit actor) => null;
    protected override Color HairColor(Actor_Unit actor) => Color.white;
    protected override Sprite MouthSprite(Actor_Unit actor) => null;
    protected override Color ScleraColor(Actor_Unit actor) => Color.white;
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => null;


}

