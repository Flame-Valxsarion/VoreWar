using System.Collections.Generic;
using UnityEngine;

class Ryan : BlankSlate // Sprite by Micadi Character by Legoshi
{
    readonly Sprite[] Sprites1 = State.GameManager.SpriteDictionary.Ryan;
    readonly Sprite[] Sprites2 = State.GameManager.SpriteDictionary.RyanVore;

    internal Ryan()
    {
        CanBeGender = new List<Gender>() { Gender.Male };
        GentleAnimation = true;
        WeightGainDisabled = false;
        SpecialAccessoryCount = 2;
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
        BodySizes = 4;
        AllowedMainClothingTypes = new List<MainClothing>();
        AllowedWaistTypes = new List<MainClothing>();
        AllowedClothingHatTypes = new List<ClothingAccessory>();
        MouthTypes = 0;
        AvoidedMainClothingTypes = 0;

        ExtendedBreastSprites = false;

        Body =  new SpriteExtraInfo(11, BodySprite, WhiteColored); // Upper Body
        Head =  new SpriteExtraInfo(15, HeadSprite, WhiteColored); // Head... Nuff said
        BodyAccessory = new SpriteExtraInfo(12, AccessorySprite, WhiteColored); // Chestplate
        BodyAccent = new SpriteExtraInfo(5, BodyAccentSprite, WhiteColored); // Lower Body
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, WhiteColored); // Tail
        BodyAccent3 = new SpriteExtraInfo(16, BodyAccentSprite3, WhiteColored); // Left arm
        BodyAccent4 = new SpriteExtraInfo(2, BodyAccentSprite4, WhiteColored); // Right arm
        BodyAccent5 = new SpriteExtraInfo(10, BodyAccentSprite5, WhiteColored); // Left Leg
        BodyAccent6 = new SpriteExtraInfo(4, BodyAccentSprite6, WhiteColored); // Upper right leg
        BodyAccent7 = new SpriteExtraInfo(7, BodyAccentSprite7, WhiteColored); // Lower right leg
        BodyAccent8 = new SpriteExtraInfo(14, BodyAccentSprite8, WhiteColored); // Gulp~
        Belly = new SpriteExtraInfo(9, null, WhiteColored); // Yup sure is~ 9 change to layer 13 when at 6-19 and Sel
        Weapon = new SpriteExtraInfo(3, WeaponSprite, WhiteColored); // Pokey Stick
        Dick = new SpriteExtraInfo(8, DickSprite, WhiteColored); // Other pokey stick~
        Balls = new SpriteExtraInfo(6, BallsSprite, WhiteColored); // Balls~
    }

    internal override int BreastSizes => 1;
    internal override int DickSizes => 1;

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Captain Ryan";
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        base.RunFirst(actor);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Belly, 0, 0 * .417f);
        if (actor.Unit.BodySize == 3)
            AddOffset(Balls, -1 * .417f, -1 * .417f);
        else
            AddOffset(Balls, 0, 0 * .417f);
    }



    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 0) // No armor
        {
            if (actor.IsOralVoring)
                return Sprites1[44];
            if (actor.IsAttacking)
                return Sprites1[43];
            else
                return Sprites1[42];
        }
        else // Armor
        {
            if (actor.IsOralVoring)
                return Sprites1[47];
            if (actor.IsAttacking)
                return Sprites1[46];
            else
                return Sprites1[45];
        }
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        return Sprites1[0 + actor.Unit.BodySize];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 0 || actor.HasBelly == true) // No armor
            return null;
        else // Armor
            return Sprites1[73 + actor.Unit.BodySize];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        return Sprites1[4 + actor.Unit.BodySize];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 0) // No armor
            return Sprites1[8 + actor.Unit.BodySize];
        else // Armor
            return Sprites1[12 + actor.Unit.BodySize];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 0) // No armor
            return Sprites1[16 + actor.Unit.BodySize];
        else // Armor
            return Sprites1[20 + actor.Unit.BodySize];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        if (actor.IsAttacking)
        {
            if (actor.Unit.SpecialAccessoryType == 0) // No armor
                return Sprites1[32 + actor.Unit.BodySize];
            else // Armor
                return Sprites1[36 + actor.Unit.BodySize];
        }
        else
        {
            if (actor.Unit.SpecialAccessoryType == 0) // No armor
                return Sprites1[24 + actor.Unit.BodySize];
            else // Armor
                return Sprites1[28 + actor.Unit.BodySize];
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 0) // No armor
            return Sprites1[48 + actor.Unit.BodySize];
        else // Armor
            return Sprites1[52 + actor.Unit.BodySize];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 0) // No armor
            return Sprites1[64 + actor.Unit.BodySize];
        else // Armor
            return Sprites1[68 + actor.Unit.BodySize];
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 0) // No armor
            return Sprites1[56 + actor.Unit.BodySize];
        else // Armor
            return Sprites1[60 + actor.Unit.BodySize];
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor)
    {
        if (actor.HasJustVored) // Handled uniquely for this unit to only work with ral vore successes check "actor.SetVoreSuccessMode();" in PredatorComponent.cs
            return Sprites1[72];
        else
            return null;
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Surrendered)
            return null;
        if (actor.IsAttacking)
            return Sprites1[41];
        else
            return Sprites1[40];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        int size = actor.GetStomachSize(18, 0.8f);
        if (actor.HasBelly == true)
        {
            if (size <= 5)
                Belly.layer = 9;
            else
                Belly.layer = 13;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                AddOffset(Belly, 0, 0 * .417f);
                Belly.layer = 13;
                if (size >= 18) return Sprites2[29];
            }
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                AddOffset(Belly, 0, 0 * .417f);
                Belly.layer = 13;
                if (size >= 18) return Sprites2[28];
                if (size >= 17) return Sprites2[27];
                if (size >= 16) return Sprites2[26];
                if (size >= 15) return Sprites2[25];
                if (size >= 14) return Sprites2[24];
                if (size >= 13) return Sprites2[23];
                if (size >= 12) return Sprites2[22];
                if (size >= 11) return Sprites2[21];
                if (size >= 10) return Sprites2[20];
            }
            return Sprites2[0 + size];
        }
        else
            return null;
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
            if (actor.HasBelly || actor.Unit.SpecialAccessoryType != 0)
                return null;
            if (actor.IsCockVoring)
                return Sprites1[78];
            if (actor.IsErect())
                return Sprites1[77];
            return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.PredatorComponent?.BallsFullness == 0)
            return null;
        if (actor.PredatorComponent?.BallsFullness > 0)
        {
            int ballSize = actor.GetBallSize(18, 0.8f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
            {
                AddOffset(Balls, 10 * .417f, 0 * .417f);
                if (ballSize >= 18) return Sprites2[60];
            }
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
            {
                AddOffset(Balls, 10 * .417f, 0 * .417f);
                if (ballSize >= 18) return Sprites2[59];
                if (ballSize >= 17) return Sprites2[58];
                if (ballSize >= 16) return Sprites2[57];
                if (ballSize >= 15) return Sprites2[56];
                if (ballSize >= 14) return Sprites2[55];
                if (ballSize >= 13) return Sprites2[54];
                if (ballSize >= 12) return Sprites2[53];
                if (ballSize >= 11) return Sprites2[52];
                if (ballSize >= 10) return Sprites2[51];
            }
            return Sprites2[31 + ballSize];
        }
        else
            return Sprites2[30];
    }



}

