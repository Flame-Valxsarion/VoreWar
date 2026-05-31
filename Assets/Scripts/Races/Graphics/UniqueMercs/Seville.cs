using System.Collections.Generic;
using UnityEngine;

class Seville : BlankSlate
{
    RaceFrameList SevilleHeadOV = new RaceFrameList(new int[5] { 0, 1, 2, 3, 4 }, new float[5] { .35f, .4f, .4f, .45f, .75f });
    RaceFrameList SevilleUBandAttack = new RaceFrameList(new int[4] { 0, 1, 2, 3 }, new float[4] { .25f, .4f, .5f, .5f });
    RaceFrameList SevilleTailUB = new RaceFrameList(new int[4] { 0, 1, 2, 3 }, new float[4] { .25f, .4f, .5f, .5f });
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
            return State.GameManager.SpriteDictionary.Seville[13];
        if (actor.IsUnbirthing || actor.IsAttacking)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
            if (actor.AnimationController.frameLists[0].currentTime >= SevilleUBandAttack.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
            {
                actor.AnimationController.frameLists[0].currentFrame++;
                actor.AnimationController.frameLists[0].currentTime = 0f;
                if (actor.AnimationController.frameLists[0].currentFrame >= SevilleUBandAttack.frames.Length)
                {
                    actor.AnimationController.frameLists[0].currentlyActive = false;
                    actor.AnimationController.frameLists[0].currentFrame = 0;
                    actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }
            return State.GameManager.SpriteDictionary.Seville[23 + SevilleUBandAttack.frames[actor.AnimationController.frameLists[0].currentFrame]];
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
            return State.GameManager.SpriteDictionary.Seville[13 + SevilleHeadOV.frames[actor.AnimationController.frameLists[0].currentFrame]];
        } 
        if (actor.PredatorComponent?.VisibleFullness > 2)
            return State.GameManager.SpriteDictionary.Seville[27];
        return State.GameManager.SpriteDictionary.Seville[1];
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.HasBelly == false)
            return State.GameManager.SpriteDictionary.Seville[3];
        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? true)
        {
            if (actor.PredatorComponent.VisibleFullness > 2)
                return State.GameManager.SpriteDictionary.Seville[7];
        }
        return actor.HasBelly ? State.GameManager.SpriteDictionary.Seville[4 + actor.GetStomachSize(3)] : State.GameManager.SpriteDictionary.Seville[3];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) //tail/vagina
    {
        if (actor.IsUnbirthing)
        {
            actor.AnimationController.frameLists[1].currentlyActive = true;
            if (actor.AnimationController.frameLists[1].currentTime >= SevilleTailUB.times[actor.AnimationController.frameLists[1].currentFrame] && actor.Unit.IsDead == false)
            {
                actor.AnimationController.frameLists[1].currentFrame++;
                actor.AnimationController.frameLists[1].currentTime = 0f;
                if (actor.AnimationController.frameLists[1].currentFrame >= SevilleTailUB.frames.Length)
                {
                    actor.AnimationController.frameLists[1].currentlyActive = false;
                    actor.AnimationController.frameLists[1].currentFrame = 0;
                    actor.AnimationController.frameLists[1].currentTime = 0f;
                }
            }
            return State.GameManager.SpriteDictionary.Seville[18 + SevilleTailUB.frames[actor.AnimationController.frameLists[0].currentFrame]];
        } 
        return State.GameManager.SpriteDictionary.Seville[2];
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
                return State.GameManager.SpriteDictionary.Seville[33 + SevilleHeadOV.frames[actor.AnimationController.frameLists[0].currentFrame]];
            }
            else
                return State.GameManager.SpriteDictionary.Seville[33];
        }
        else
            return null;
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Lower garments
    {
        if (actor.Unit.SpecialAccessoryType == 1)
        {
            if (actor.HasBelly == false)
                return State.GameManager.SpriteDictionary.Seville[28];
            if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? true)
            {
                if (actor.PredatorComponent.VisibleFullness > 2)
                    return State.GameManager.SpriteDictionary.Seville[32];
            }
            return actor.HasBelly ? State.GameManager.SpriteDictionary.Seville[29 + actor.GetStomachSize(3)] : State.GameManager.SpriteDictionary.Seville[28];
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
                return State.GameManager.SpriteDictionary.Seville[12];
        }

        return actor.HasBelly ? State.GameManager.SpriteDictionary.Seville[8 + actor.GetStomachSize(3)] : null;
    }
}

