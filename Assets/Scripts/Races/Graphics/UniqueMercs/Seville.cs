using System.Collections.Generic;
using UnityEngine;

class Seville : BlankSlate
{
    RaceFrameList SevilleHeadOV = new RaceFrameList(new int[5] {0,1,2,3,4}, new float[5] { .35f, .3f, .3f, .4f, .65f });
    RaceFrameList SevilleUBandAttack = new RaceFrameList(new int[4] { 0, 1, 2, 3 }, new float[4] { .25f, .4f, .5f, .5f });
    RaceFrameList SevilleTailUB = new RaceFrameList(new int[9] {0,1,2,3,4,3,2,1,0}, new float[9] { .1f, .1f, .1f, .1f, .7f, .1f, .1f, .1f, .1f });
    public Seville()
    {
        CanBeGender = new List<Gender>() { Gender.Female };
        GentleAnimation = true;
        Head = new SpriteExtraInfo(7, HeadSprite, WhiteColored);
        Body = new SpriteExtraInfo(4, BodySprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, WhiteColored); //tail
        BodyAccent2 = new SpriteExtraInfo(8, BodyAccentSprite2, WhiteColored); // Upper garments
        BodyAccent3 = new SpriteExtraInfo(6, BodyAccentSprite3, WhiteColored); // Lower garments
        Belly = new SpriteExtraInfo(5, null, WhiteColored);
        clothingColors = 0;
        SpecialAccessoryCount = 2;
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[]
        {
            new AnimationController.FrameList(0, 0, true),
            new AnimationController.FrameList(0, 0, true),
            new AnimationController.FrameList(0, 0, true),
        };
    }
    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null)
            SetUpAnimations(actor);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Seville";
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return SpriteDictionary.Seville[13];
        if (actor.IsUnbirthing || actor.IsAttacking)
        {
            actor.AnimationController.frameLists[1].currentlyActive = true;
            if (actor.AnimationController.frameLists[1].currentTime >= SevilleUBandAttack.times[actor.AnimationController.frameLists[1].currentFrame] && actor.Unit.IsDead == false)
            {
                actor.AnimationController.frameLists[1].currentFrame++;
                actor.AnimationController.frameLists[1].currentTime = 0f;
                if (actor.AnimationController.frameLists[1].currentFrame >= SevilleUBandAttack.frames.Length)
                {
                    actor.AnimationController.frameLists[1].currentlyActive = false;
                    actor.AnimationController.frameLists[1].currentFrame = 0;
                    actor.AnimationController.frameLists[1].currentTime = 0f;
                }
            }
            return SpriteDictionary.Seville[23 + SevilleUBandAttack.frames[actor.AnimationController.frameLists[1].currentFrame]];
        } 
        if (actor.HasJustVored) // Handled uniquely for this unit to only work with oral vore successes. Check "actor.SetVoreSuccessMode();" in PredatorComponent.cs
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
            if (actor.AnimationController.frameLists[0].currentTime >= SevilleHeadOV.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
            {
                actor.AnimationController.frameLists[0].currentFrame++;
                actor.AnimationController.frameLists[0].currentTime = 0f;
                if (actor.AnimationController.frameLists[0].currentFrame >= SevilleHeadOV.frames.Length)
                {
                    actor.AnimationController.frameLists[0].currentlyActive = false;
                    actor.AnimationController.frameLists[0].currentFrame = 0;
                    actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }
            return SpriteDictionary.Seville[13 + SevilleHeadOV.frames[actor.AnimationController.frameLists[0].currentFrame]];
        } 
        if (actor.PredatorComponent?.VisibleFullness > 2)
            return SpriteDictionary.Seville[27];
        return SpriteDictionary.Seville[1];
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.HasBelly == false)
            return SpriteDictionary.Seville[3];
        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? true)
        {
            if (actor.PredatorComponent.VisibleFullness > 2)
                return SpriteDictionary.Seville[7];
        }
        return actor.HasBelly ? SpriteDictionary.Seville[4 + actor.GetStomachSize(3)] : SpriteDictionary.Seville[3];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) //tail/vagina
    {
        if (actor.IsUnbirthing || actor.IsAnalVoring)
        {
            actor.AnimationController.frameLists[2].currentlyActive = true;
            if (actor.AnimationController.frameLists[2].currentTime >= SevilleTailUB.times[actor.AnimationController.frameLists[2].currentFrame] && actor.Unit.IsDead == false)
            {
                actor.AnimationController.frameLists[2].currentFrame++;
                actor.AnimationController.frameLists[2].currentTime = 0f;
                if (actor.AnimationController.frameLists[2].currentFrame >= SevilleTailUB.frames.Length)
                {
                    actor.AnimationController.frameLists[2].currentlyActive = false;
                    actor.AnimationController.frameLists[2].currentFrame = 0;
                    actor.AnimationController.frameLists[2].currentTime = 0f;
                }
            }
            return SpriteDictionary.Seville[18 + SevilleTailUB.frames[actor.AnimationController.frameLists[2].currentFrame]];
        } 
        return SpriteDictionary.Seville[2];
    }


    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Upper garments
    {
        if (actor.Unit.SpecialAccessoryType == 1)
        {
            if (actor.HasJustVored) // Handled uniquely for this unit to only work with oral vore successes. Check "actor.SetVoreSuccessMode();" in PredatorComponent.cs
            {
                actor.AnimationController.frameLists[0].currentlyActive = true;
                if (actor.AnimationController.frameLists[0].currentTime >= SevilleHeadOV.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
                {
                    actor.AnimationController.frameLists[0].currentFrame++;
                    actor.AnimationController.frameLists[0].currentTime = 0f;
                    if (actor.AnimationController.frameLists[0].currentFrame >= SevilleHeadOV.frames.Length)
                    {
                        actor.AnimationController.frameLists[0].currentlyActive = false;
                        actor.AnimationController.frameLists[0].currentFrame = 0;
                        actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }
                return SpriteDictionary.Seville[33 + SevilleHeadOV.frames[actor.AnimationController.frameLists[0].currentFrame]];
            }
            else
                return SpriteDictionary.Seville[33];
        }
        else
            return null;
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Lower garments
    {
        if (actor.Unit.SpecialAccessoryType == 1)
        {
            if (actor.HasBelly == false)
                return SpriteDictionary.Seville[28];
            if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? true)
            {
                if (actor.PredatorComponent.VisibleFullness > 2)
                    return SpriteDictionary.Seville[32];
            }
            return actor.HasBelly ? SpriteDictionary.Seville[29 + actor.GetStomachSize(3)] : SpriteDictionary.Seville[28];
        }
        else
            return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? true)
        {
            if (actor.PredatorComponent.VisibleFullness > 2)
                return SpriteDictionary.Seville[12];
        }

        return actor.HasBelly ? SpriteDictionary.Seville[8 + actor.GetStomachSize(3)] : null;
    }
}

