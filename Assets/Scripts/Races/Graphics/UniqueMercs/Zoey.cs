using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class Zoey : BlankSlate
{
    readonly Sprite[] Sprites = SpriteDictionary.Zoey;

    internal BodyState bodyState = BodyState.Normal;

    RaceFrameList SpinEffect = new RaceFrameList(new int[2] { 25, 19 }, new float[2] { .375f, .375f });

    internal enum BodyState
    {
        Normal,
        HighBelly,
        SideBelly,
        SpinAttack
    }

    public Zoey()
    {
        CanBeGender = new List<Gender>() { Gender.Female };
        GentleAnimation = true;
        Body = new SpriteExtraInfo(1, BodySprite, WhiteColored);
        Head = new SpriteExtraInfo(5, HeadSprite, WhiteColored);
        Hair = new SpriteExtraInfo(7, HairSprite, WhiteColored);
        Hair2 = new SpriteExtraInfo(6, HairSprite2, WhiteColored);
        Belly = new SpriteExtraInfo(2, null, WhiteColored);
        Breasts = new SpriteExtraInfo(10, BreastsSprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(0, BodyAccentSprite, WhiteColored);
        BodyAccent2 = new SpriteExtraInfo(14, BodyAccentSprite2, WhiteColored);
        clothingColors = 0;

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new ZoeyTop(),
        };

    }

    internal override int BreastSizes => 5;

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(0, 0, false)};
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null || actor.AnimationController.frameLists.Count() == 0) SetUpAnimations(actor);

        if (actor.AnimationController?.frameLists[0].currentlyActive ?? false)
        {
            if (actor.GetStomachSize(19, 1) >= 17)
                bodyState = BodyState.SideBelly;
            else
                bodyState = BodyState.SpinAttack;
        }
        else if (actor.GetStomachSize(19, 1) >= 18)
            bodyState = BodyState.HighBelly;
        else
            bodyState = BodyState.Normal;
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        ClothingShift = new Vector3(0, 0);
        switch (bodyState)
        {
            case BodyState.HighBelly:
                if (actor.GetStomachSize(19, 1) == 19)
                {
                    AddOffset(Head, 0, 30 * .625f);
                    AddOffset(Hair, 0, 30 * .625f);
                    AddOffset(Hair2, 0, 30 * .625f);
                    AddOffset(Breasts, 0, 30 * .625f);
                    ClothingShift = new Vector3(0, 30 * .625f);
                }
                else //18
                {
                    AddOffset(Body, 0, -14 * .625f);
                    AddOffset(Head, 0, 16 * .625f);
                    AddOffset(Hair, 0, 16 * .625f);
                    AddOffset(Hair2, 0, 16 * .625f);
                    AddOffset(Breasts, 0, 16 * .625f);
                    ClothingShift = new Vector3(0, 16 * .625f);
                }
                break;
            case BodyState.SideBelly:
                if (actor.GetStomachSize(19, 1) == 19)
                    AddOffset(Breasts, -5 * .625f, 12 * .625f);
                else if (actor.GetStomachSize(19, 1) == 18)
                {
                    AddOffset(Breasts, -2 * .625f, 0);
                    AddOffset(Body, 0, -16 * .625f);
                    AddOffset(Head, 0, -16 * .625f);
                    AddOffset(Hair, 0, -16 * .625f);
                    AddOffset(BodyAccent, 0, -16 * .625f);
                    AddOffset(BodyAccent2, 0, -16 * .625f);
                }
                else
                {
                    AddOffset(Breasts, -5 * .625f, 0);
                    AddOffset(Body, 0, -32 * .625f);
                    AddOffset(Head, 0, -32 * .625f);
                    AddOffset(Hair, 0, -32 * .625f);
                    AddOffset(BodyAccent, 0, -32 * .625f);
                    AddOffset(BodyAccent2, 0, -32 * .625f);
                }

                break;
        }
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Zoey";
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        Body.layer = 1;
        switch (bodyState)
        {
            case BodyState.HighBelly:
                if (actor.IsAttacking == false)
                    return Sprites[9];
                else
                    return Sprites[10];
            case BodyState.SideBelly:
                Body.layer = 4;
                return Sprites[12];
            case BodyState.SpinAttack:
                return Sprites[6];
            default:
                if (actor.IsAttacking == false)
                    return Sprites[0];
                else
                    return Sprites[1];

        }

    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        switch (bodyState)
        {
            case BodyState.SpinAttack:
            case BodyState.SideBelly:
                return null;
            default:
                if (actor.IsOralVoring)
                    return Sprites[3];
                if (actor.PredatorComponent?.Fullness > 2)
                {
                    if (State.Rand.Next(650) == 0) actor.SetAnimationMode(1, .5f);
                    int specialMode = actor.CheckAnimationFrame();
                    if (specialMode == 1)
                        return Sprites[5];
                    return Sprites[4];
                }
                return Sprites[2];

        }

    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        switch (bodyState)
        {
            case BodyState.SpinAttack:
            case BodyState.SideBelly:
                return null;
            default:
                return Sprites[7];
        }

    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        switch (bodyState)
        {
            case BodyState.SpinAttack:
            case BodyState.SideBelly:
                return null;
            default:
                return Sprites[8];
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {

        if (actor.HasBelly)
        {
            switch (bodyState)
            {
                case BodyState.SpinAttack:
                case BodyState.SideBelly:
                    if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(19, 1) == 19)
                        return Sprites[72];
                    else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                    {
                        if (actor.GetStomachSize(19, 0.7f) == 19)
                            return Sprites[78];
                        else if (actor.GetStomachSize(19, 0.8f) == 19)
                            return Sprites[77];
                        else if (actor.GetStomachSize(19, 0.9f) == 19)
                            return Sprites[76];
                    }
                    return Sprites[52 + actor.GetStomachSize(19)];
                default:
                    if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(19, 1) == 19)
                        return Sprites[51];
                    else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                    {
                        if (actor.GetStomachSize(19, 0.7f) == 19)
                            return Sprites[75];
                        else if (actor.GetStomachSize(19, 0.8f) == 19)
                            return Sprites[74];
                        else if (actor.GetStomachSize(19, 0.9f) == 19)
                            return Sprites[73];
                    }
                    return Sprites[31 + actor.GetStomachSize(19)];
            }

        }
        return null;
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        switch (bodyState)
        {
            case BodyState.HighBelly:
                return Sprites[11];
            case BodyState.SideBelly:
                return Sprites[13];
        }
        return null;
    }

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (bodyState == BodyState.SideBelly)
            Breasts.layer = 3;
        else
            Breasts.layer = 10;
        switch (bodyState)
        {
            case BodyState.SpinAttack:
            case BodyState.SideBelly:
                if (actor.PredatorComponent.VisibleFullness > 1)
                    return Sprites[26 + actor.Unit.BreastSize];
                return Sprites[20 + actor.Unit.BreastSize];
            default:
                return Sprites[14 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            if (actor.AnimationController.frameLists[0].currentTime >= SpinEffect.times[actor.AnimationController.frameLists[0].currentFrame])
            {
                actor.AnimationController.frameLists[0].currentFrame++;
                actor.AnimationController.frameLists[0].currentTime = 0f;

                if (actor.AnimationController.frameLists[0].currentFrame >= SpinEffect.frames.Length)
                {
                    actor.AnimationController.frameLists[0].currentlyActive = false;
                    actor.AnimationController.frameLists[0].currentFrame = 0;
                    actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }
        }
        else
            return null;
        return Sprites[SpinEffect.frames[actor.AnimationController.frameLists[0].currentFrame]];
    }

    class ZoeyTop : MainClothing
    {
        public ZoeyTop()
        {
            DiscardSprite = null;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(11, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(7, null, WhiteColored);
            Type = 1417;
            ReqWinterHoliday = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            var state = Races.Zoey.bodyState;
            //Sweater
            switch (state)
            {
                case BodyState.SpinAttack:
                    if (actor.Unit.BreastSize == 4 | actor.GetStomachSize(19, 1) >= 4)
                        clothing1.GetSprite = (s) => SpriteDictionary.ZoeyHoliday[6];
                    else
                        clothing1.GetSprite = (s) => SpriteDictionary.ZoeyHoliday[4];
                    break;
                case BodyState.SideBelly:
                    clothing1.GetSprite = (s) => SpriteDictionary.ZoeyHoliday[5];
                    break;
                default:
                    if (actor.Unit.BreastSize == 4 | actor.GetStomachSize(19, 1) >= 4)
                        clothing1.GetSprite = (s) => SpriteDictionary.ZoeyHoliday[24 + (actor.IsAttacking ? 1 : 0)];
                    else
                        clothing1.GetSprite = (s) => SpriteDictionary.ZoeyHoliday[22 + (actor.IsAttacking ? 1 : 0)];
                    break;
            }

            //Boobs
            if (state == BodyState.SideBelly)
                clothing2.layer = 4;
            else
                clothing2.layer = 12;
            switch (state)
            {
                case BodyState.SpinAttack:
                case BodyState.SideBelly:
                    if (actor.PredatorComponent?.VisibleFullness > 1)
                        clothing2.GetSprite = (s) => SpriteDictionary.ZoeyHoliday[15 + Math.Min(actor.Unit.BreastSize, 3)];
                    else
                        clothing2.GetSprite = (s) => SpriteDictionary.ZoeyHoliday[11 + Math.Min(actor.Unit.BreastSize, 3)];
                    break;
                default:
                    if (actor.Unit.BreastSize == 4 | actor.GetStomachSize(19, 1) >= 4)
                        clothing2.GetSprite = (s) => SpriteDictionary.ZoeyHoliday[24];
                    else
                        clothing2.GetSprite = (s) => SpriteDictionary.ZoeyHoliday[7 + Math.Min(actor.Unit.BreastSize, 3)];
                    break;
            }

            switch (state)
            {
                case BodyState.SpinAttack:
                    clothing3.GetSprite = (s) => SpriteDictionary.ZoeyHoliday[20];
                    break;
                case BodyState.SideBelly:
                    clothing3.GetSprite = (s) => SpriteDictionary.ZoeyHoliday[21];
                    break;
                default:
                    clothing3.GetSprite = (s) => SpriteDictionary.ZoeyHoliday[19];
                    break;
            }
            switch (state)
            {
                case BodyState.HighBelly:
                    clothing4.GetSprite = (s) => SpriteDictionary.ZoeyHoliday[2 + (actor.IsAttacking ? 1 : 0)];
                    break;
                case BodyState.SideBelly:
                    clothing4.GetSprite = null;
                    break;
                case BodyState.SpinAttack:
                    clothing4.GetSprite = null;
                    break;
                default:
                    clothing4.GetSprite = (s) => SpriteDictionary.ZoeyHoliday[0 + (actor.IsAttacking ? 1 : 0)];
                    break;
            }
            base.Configure(sprite, actor);
        }

    }
}
