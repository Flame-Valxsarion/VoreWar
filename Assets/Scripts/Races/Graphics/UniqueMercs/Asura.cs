using System;
using System.Collections.Generic;
using UnityEngine;

class Asura : BlankSlate
{

    readonly Sprite[] Sprites = SpriteDictionary.Asura;
    RaceFrameList[] frameList = new RaceFrameList[]
    {
        new RaceFrameList(new int[7] { 74, 75, 76, 77, 78, 79, 80 }, new float[7] { .15f, .15f, .15f, .15f, .15f, .15f, .15f }),
        new RaceFrameList(new int[7] { 81, 82, 83, 84, 85, 86, 87 }, new float[7] { .15f, .15f, .15f, .15f, .15f, .15f, .15f }),
        new RaceFrameList(new int[7] { 88, 89, 90, 91, 92, 93, 94 }, new float[7] { .15f, .15f, .15f, .15f, .15f, .15f, .15f }),
        new RaceFrameList(new int[7] { 95, 96, 97, 98, 99, 100, 101 }, new float[7] { .15f, .15f, .15f, .15f, .15f, .15f, .15f }),
    };

    internal Asura()
    {
        CanBeGender = new List<Gender>() { Gender.Female };
        Body = new SpriteExtraInfo(2, BodySprite, WhiteColored);
        BodyAccessory = new SpriteExtraInfo(16, AccessorySprite, WhiteColored);
        Head = new SpriteExtraInfo(4, HeadSprite, WhiteColored);
        Belly = new SpriteExtraInfo(15, null, WhiteColored);


        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, WhiteColored);
        BodyAccent2 = new SpriteExtraInfo(6, BodyAccentSprite2, WhiteColored);
        BodyAccent3 = new SpriteExtraInfo(7, BodyAccentSprite3, WhiteColored);
        BodyAccent4 = new SpriteExtraInfo(7, BodyAccentSprite4, WhiteColored);
        BodyAccent5 = new SpriteExtraInfo(7, BodyAccentSprite5, WhiteColored);
        BodyAccent6 = new SpriteExtraInfo(7, BodyAccentSprite6, WhiteColored);
        BodyAccent7 = new SpriteExtraInfo(8, BodyAccentSprite7, WhiteColored);
        BodyAccent8 = new SpriteExtraInfo(3, BodyAccentSprite8, WhiteColored);
        BodyAccent9 = new SpriteExtraInfo(9, BodyAccentSprite9, WhiteColored);
        BodyAccent10 = new SpriteExtraInfo(9, BodyAccentSprite10, WhiteColored);

        BreastShadow = new SpriteExtraInfo(16, BreastsShadowSprite, WhiteColored);

        Hair = new SpriteExtraInfo(6, HairSprite, WhiteColored);
        Hair2 = new SpriteExtraInfo(6, HairSprite2, WhiteColored);
        Hair3 = new SpriteExtraInfo(6, HairSprite3, WhiteColored);
        Beard = new SpriteExtraInfo(6, BeardSprite, WhiteColored);
        SecondaryAccessory = new SpriteExtraInfo(1, SecondaryAccessorySprite, WhiteColored);
        Weapon = new SpriteExtraInfo(14, WeaponSprite, WhiteColored);
        Breasts = new SpriteExtraInfo(10, BreastsSprite, WhiteColored);

        BaseOutfit outfit = new BaseOutfit();
        ReindeerHorns horns = new ReindeerHorns();

        clothingColors = 1;
        AllowedMainClothingTypes = new List<MainClothing>() { outfit };

        AllowedClothingHatTypes = new List<ClothingAccessory>() { horns };


    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Asura";
        unit.ClothingAccessoryType = State.Rand.Next(2);
    }


    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Weapon, 0, 74 * .625f);
        AddOffset(SecondaryAccessory, 0, 59 * .625f);
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.IsAttacking && actor.Unit.ClothingType != 0)
            return Sprites[2];
        if (actor.IsAttacking && actor.Unit.ClothingType == 0)
            return Sprites[1];
        return Sprites[0];
    }

    protected override Sprite BreastsShadowSprite(Actor_Unit actor)
    {
        if (actor.TurnUsedShun > 0 && actor.TurnUsedShun == State.GameManager.TacticalMode.currentTurn)
            return Sprites[Math.Max(59 + actor.GetStomachSize(), 64)];
        return null;
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites[5];

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites[11];
        else
            return Sprites[9];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.SetActive(true);

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(15, 1) == 15)
            {
                belly.transform.localScale = new Vector3(1, 1, 1);
                AddOffset(Belly, 0, -24 * .625f);
                return SpriteDictionary.Asura[104];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                if (actor.GetStomachSize(15, .90f) == 15)
                {
                    belly.transform.localScale = new Vector3(1, 1, 1);
                    AddOffset(Belly, 0, -24 * .625f);
                    return SpriteDictionary.Asura[103];
                }
                else if (actor.GetStomachSize(15, 1.05f) == 15)
                {
                    belly.transform.localScale = new Vector3(1, 1, 1);
                    AddOffset(Belly, 0, -16 * .625f);
                    return SpriteDictionary.Asura[102];
                }
            }
            return Sprites[48 + actor.GetStomachSize()];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites[19];

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor)
    {
        if (actor.IsAttacking && actor.Unit.ClothingType != 0)
            return Sprites[37];
        return null;
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking && actor.Unit.ClothingType != 0)
            return Sprites[38];
        return null;
    }

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.ClothingType == 0)
            return Sprites[6];
        return Sprites[7];
    }
    internal override int BreastSizes => 1;

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (Config.FurryHandsAndFeet && actor.Unit.ClothingType == 0)
        {
            if (actor.IsAttacking)
                return Sprites[21];
            return Sprites[20];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (Config.FurryHandsAndFeet)
        {
            if (actor.Unit.ClothingType == 0)
            {
                return Sprites[22];
            }
            else
                return Sprites[27];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        if (Config.FurryHandsAndFeet && actor.Unit.ClothingType == 0)
        {
            if (actor.IsAttacking)
                return Sprites[29];
            return Sprites[28];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        if (Config.FurryHandsAndFeet)
        {
            if (actor.Unit.ClothingType == 0)
            {
                return Sprites[30];
            }
            else
                return null;
        }
        return null;
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) => Sprites[18];

    protected override Sprite BodyAccentSprite7(Actor_Unit actor)
    {
        if (Config.FurryHandsAndFeet && actor.Unit.ClothingType == 0)
        {
            if (actor.IsAttacking)
                return Sprites[34];
            return Sprites[33];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor)
    {
        if (Config.FurryHandsAndFeet)
        {
            return Sprites[35];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite9(Actor_Unit actor)
    {
        if (Config.FurryHandsAndFeet && actor.Unit.ClothingType == 0)
        {
            if (actor.IsAttacking)
                return Sprites[42];
            return Sprites[41];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite10(Actor_Unit actor)
    {
        if (Config.FurryHandsAndFeet)
        {
            return Sprites[43];
        }
        return null;
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[]
        { new AnimationController.FrameList(), new AnimationController.FrameList(), new AnimationController.FrameList(), new AnimationController.FrameList() };
        actor.AnimationController.frameLists[0].currentlyActive = true;
        actor.AnimationController.frameLists[1].currentlyActive = true;
        actor.AnimationController.frameLists[2].currentlyActive = true;
        actor.AnimationController.frameLists[3].currentlyActive = true;
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null)
            SetUpAnimations(actor);

        return ProcessAnimation(actor, 0);
    }

    private Sprite ProcessAnimation(Actor_Unit actor, int list)
    {
        if (actor.Unit.ClothingType == 0)
            return null;

        if (State.Rand.Next(120) == 0)
            actor.AnimationController.frameLists[list].currentlyActive = true;

        if (actor.AnimationController.frameLists[list].currentlyActive == false)
            return null;

        if (actor.AnimationController.frameLists[list].currentTime >= frameList[list].times[actor.AnimationController.frameLists[list].currentFrame])
        {
            actor.AnimationController.frameLists[list].currentFrame++;
            actor.AnimationController.frameLists[list].currentTime = 0f;

            if (actor.AnimationController.frameLists[list].currentFrame >= frameList[list].frames.Length)
            {
                actor.AnimationController.frameLists[list].currentFrame = 0;
                actor.AnimationController.frameLists[list].currentlyActive = false;
                return null;
            }
        }
        return SpriteDictionary.Asura[frameList[list].frames[actor.AnimationController.frameLists[list].currentFrame]];
    }

    protected override Sprite HairSprite2(Actor_Unit actor) => ProcessAnimation(actor, 1);

    protected override Sprite HairSprite3(Actor_Unit actor) => ProcessAnimation(actor, 2);

    protected override Sprite BeardSprite(Actor_Unit actor) => ProcessAnimation(actor, 3);

    class BaseOutfit : MainClothing
    {
        float timeDuplicate;
        float time;
        public BaseOutfit()
        {
            OccupiesAllSlots = true;
            blocksDick = false;
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            clothing3 = new SpriteExtraInfo(12, null, null);
            clothing4 = new SpriteExtraInfo(0, null, null);
            clothing5 = new SpriteExtraInfo(0, null, null);

            DiscardSprite = SpriteDictionary.Asura[39];

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Asura[s.IsAttacking ? 4 : 3];
            clothing2.GetSprite = (s) => SpriteDictionary.Asura[s.HasBelly ? 32 : 26];
            clothing3.GetSprite = (s) => SpriteDictionary.Asura[8];
            clothing4.GetSprite = (s) => SpriteDictionary.Asura[s.IsAttacking ? 25 : 24];
            clothing5.GetSprite = (s) =>
            {
                if (timeDuplicate != Time.time)
                    time -= Time.deltaTime;
                timeDuplicate = Time.time;
                if (time <= 0)
                    time = 1 + (float)State.Rand.NextDouble();
                if (time > .45f)
                    return null;
                if (time > .3f)
                    return SpriteDictionary.Asura[45];
                if (time > .15f)
                    return SpriteDictionary.Asura[46];
                else
                    return SpriteDictionary.Asura[47];

            };

            base.Configure(sprite, actor);
        }
    }

    class ReindeerHorns : ClothingAccessory
    {
        public ReindeerHorns()
        {
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            ReqWinterHoliday = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.ClothingType == 0)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.AsuraHoliday[0];
            }
            else
                clothing1.GetSprite = (s) => SpriteDictionary.AsuraHoliday[1];
            base.Configure(sprite, actor);
        }
    }



}

