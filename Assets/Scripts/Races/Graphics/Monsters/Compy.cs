using System.Collections.Generic;
using UnityEngine;

class Compy : BlankSlate
{
    RaceFrameList frameListTail = new RaceFrameList(new int[8] { 2, 1, 0, 1, 2, 3, 4, 3 }, new float[8] { 0.5f, 0.4f, 0.8f, 0.4f, 0.4f, 0.4f, 0.8f, 0.4f });

    public Compy()
    {
        CanBeGender = new List<Gender>() { Gender.Male };
        SkinColors = ColorMap.WyvernColorCount;
        Body = new SpriteExtraInfo(3, BodySprite, (s) => ColorMap.GetWyvernColor(s.Unit.SkinColor));
        Dick = new SpriteExtraInfo(2, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(0, BallsSprite, (s) => ColorMap.GetWyvernColor(s.Unit.SkinColor));
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, (s) => ColorMap.GetWyvernColor(s.Unit.SkinColor)); // Tail
    }

    internal override void RandomCustom(Unit unit)
    {
        unit.SkinColor = State.Rand.Next(SkinColors);
        unit.DickSize = 1;
        unit.DefaultBreastSize = -1;
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[]
        {
            new AnimationController.FrameList(0, 0, false)  // Tail controller
        };
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        if (actor.IsAttacking || actor.IsOralVoring) return SpriteDictionary.Compy[1];

        return SpriteDictionary.Compy[0];
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.IsCockVoring) return SpriteDictionary.Compy[3];

        if (actor.GetBallSize(30) > 0) return SpriteDictionary.Compy[2];

        return null;

    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.GetBallSize(30) == 0) return SpriteDictionary.Compy[4];

        int size = actor.GetBallSize(30);

        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && size >= 30)
            return SpriteDictionary.Compy[30];
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 28)
            return SpriteDictionary.Compy[29];
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 26)
            return SpriteDictionary.Compy[28];
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 24)
            return SpriteDictionary.Compy[27];
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 22)
            return SpriteDictionary.Compy[26];

        if (size >= 21) size = 21;

        return SpriteDictionary.Compy[3 + size];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (!actor.Targetable) return SpriteDictionary.Compy[33];

        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            if (actor.AnimationController.frameLists[0].currentTime >= frameListTail.times[actor.AnimationController.frameLists[0].currentFrame])
            {
                actor.AnimationController.frameLists[0].currentFrame++;
                actor.AnimationController.frameLists[0].currentTime = 0f;

                if (actor.AnimationController.frameLists[0].currentFrame >= frameListTail.frames.Length)
                {
                    actor.AnimationController.frameLists[0].currentlyActive = false;
                    actor.AnimationController.frameLists[0].currentFrame = 0;
                    actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            return SpriteDictionary.Compy[31 + frameListTail.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        if (actor.GetBallSize(30) > 0)
        {
            if (State.Rand.Next(300) == 0)
            {
                actor.AnimationController.frameLists[0].currentlyActive = true;
            }
        }

        else if (State.Rand.Next(1500) == 0)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        return SpriteDictionary.Compy[33];
    }
}
