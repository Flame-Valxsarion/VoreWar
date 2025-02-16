using System.Collections.Generic;
using UnityEngine;

class Xelhilde : DefaultRaceData
{
    RaceFrameList frameListMelm = new RaceFrameList(new int[2] { 0, 1 }, new float[2] { .25f, .25f });

    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.Xelhilde;
    internal Xelhilde()
    {
        CanBeGender = new List<Gender>() { Gender.Female };
        SpecialAccessoryCount = 2;
        ClothingShift = new Vector3(0, 0, 0);
        AvoidedEyeTypes = 0;
        AvoidedMouthTypes = 0;

        HairColors = 1;
        HairStyles = 1;
        SkinColors = 1;
        AccessoryColors = 1;
        EyeTypes = 2;
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
        Head = new SpriteExtraInfo(5, HeadSprite, WhiteColored);
        BodyAccessory = null;
        BodyAccent =  new SpriteExtraInfo(6, BodyAccentSprite, WhiteColored);//Belly
        BodyAccent2 = null;//used for attack pose
        BodyAccent3 = null;
        BodyAccent4 = null;
        BodyAccent5 = null;
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        BodyAccent9 = null;
        BodyAccent10 = null;
        Mouth = null;
        Hair = null;
        Hair2 = null;
        Eyes = null;
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = null;
        SecondaryBelly = null;
        Weapon = new SpriteExtraInfo(13, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = null;
        BreastShadow = null;
        Dick = null;
        Balls = null;
    }
    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
		if (State.Rand.Next(5) == 1)
            unit.Name = "Vindicator of Mondfeld";
		else
            unit.Name = "Xelhilde";
        unit.EyeType = State.Rand.Next(0, EyeTypes);
        unit.SpecialAccessoryType = 1;//State.Rand.Next(0, SpecialAccessoryCount);
    }

    internal override int BreastSizes => 1;
    internal override int DickSizes => 1;

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(State.Rand.Next(0, 2), 0, true)};
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null)
            SetUpAnimations(actor);
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists[0].currentTime >= frameListMelm.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
        {
            actor.AnimationController.frameLists[0].currentFrame++;
            actor.AnimationController.frameLists[0].currentTime = 0f;

            if (actor.AnimationController.frameLists[0].currentFrame >= frameListMelm.frames.Length)
            {
                actor.AnimationController.frameLists[0].currentFrame = 0;
                actor.AnimationController.frameLists[0].currentTime = 0f;
            }
        }

        if (actor.IsAttacking)
            return null;
        else if (actor.IsOralVoring)
            return Sprites[4 + (5 * actor.Unit.SpecialAccessoryType)];
        else if (actor.HasBelly)
            return Sprites[2 + (5 * actor.Unit.SpecialAccessoryType) + frameListMelm.frames[actor.AnimationController.frameLists[0].currentFrame]];
        else
            switch (actor.Unit.EyeType)
            {
                case 0:
                    return Sprites[0 + (5 * actor.Unit.SpecialAccessoryType)];
                case 1:
                    return Sprites[1 + (5 * actor.Unit.SpecialAccessoryType)];
                default:
                    return Sprites[0 + (5 * actor.Unit.SpecialAccessoryType)];
            }
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsAttacking)
            switch (actor.Unit.BodyAccentType2)//Attack pose handler
            {
                case 0:
                {
                    AddOffset(Weapon, 0, 0);
                    return Sprites[0 + 10 + (5 * actor.Unit.SpecialAccessoryType)];
                }
                case 1:
                {
                    AddOffset(Weapon, 0, -24);//Lined up!
                    return Sprites[1 + 10 + (5 * actor.Unit.SpecialAccessoryType)];
                }
                case 2:
                {
                    AddOffset(Weapon, -18.5f, 0);//Lined up!
                    return Sprites[2 + 10 + (5 * actor.Unit.SpecialAccessoryType)];
                }
                default:
                {
                    AddOffset(Weapon, 0, 0);
                    return Sprites[0 + 10 + (5 * actor.Unit.SpecialAccessoryType)];
                }
            }
        else
            return Sprites[13 + (5 * actor.Unit.SpecialAccessoryType)];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)//Belly
    {
        if (actor.HasBelly == true)
        {
            if (actor.IsAttacking)
                switch (actor.Unit.BodyAccentType2)//Attack pose handler
                {
                    case 0:
                    {
                        return Sprites[0 + 23 + (actor.GetStomachSize(2) * 5)];
                    }
                    case 1:
                    {
                        return Sprites[1 + 23 + (actor.GetStomachSize(2) * 5)];
                    }
                    case 2:
                    {
                        return Sprites[2 + 23 + (actor.GetStomachSize(2) * 5)];
                    }
                    default:
                    {
                        return Sprites[0 + 23 + (actor.GetStomachSize(2) * 5)];
                    }
                }
            else
                return Sprites[26 + (actor.GetStomachSize(2) * 5)];
        }
        else
            return null;
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Surrendered == false)
        {
            if (actor.IsAttacking)
            {
                switch (actor.Unit.BodyAccentType2)//Attack pose handler
                {
                    case 0:
                        AddOffset(Weapon, 0, 0);
                        break;
                    case 1:
                        AddOffset(Weapon, 0, -24);//Lined up!
                        break;
                    case 2:
                        AddOffset(Weapon, -18.5f, 0);
                        break;
                    default:
                        AddOffset(Weapon, 0, 0);
                        break;
                }
                return Sprites[actor.Unit.BodyAccentType2 + 20];
            }
            else if (actor.HasBelly || actor.IsOralVoring)//Puts weapon on back when voring and full
            {
                AddOffset(Weapon, 0, 0);
                return Sprites[19];
            }
            else
            {
                AddOffset(Weapon, 0, 0);
                return Sprites[14];
            }
        }
        else
        {
            return null;
        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => null;
    protected override Sprite BackWeaponSprite(Actor_Unit actor) => null;
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) => null;
    protected override Sprite SecondaryBellySprite(Actor_Unit actor) => null;
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => null;
    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsShadowSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsSprite(Actor_Unit actor) => null;
    internal override Color ClothingColor(Actor_Unit actor) => Color.white;
    protected override Sprite DickSprite(Actor_Unit actor) => null;
    protected override Color EyeColor(Actor_Unit actor) => Color.white;
    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => null;
    protected override Sprite EyesSprite(Actor_Unit actor) => null;
    protected override Color HairColor(Actor_Unit actor) => Color.white;
    protected override Sprite HairSprite(Actor_Unit actor) => null;
    protected override Sprite HairSprite2(Actor_Unit actor) => null;
    protected override Sprite MouthSprite(Actor_Unit actor) => null;
    protected override Color ScleraColor(Actor_Unit actor) => Color.white;
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => null;


}

