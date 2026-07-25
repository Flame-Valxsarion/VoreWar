using System;
using System.Collections.Generic;
using UnityEngine;

class Ki : BlankSlate
{
    const float pixelOffset = .3125f;
    RaceFrameList frameListTail = new RaceFrameList(new int[9] { 1, 2, 0, 3, 4, 3, 0, 2, 1 }, new float[9] { 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f });
    RaceFrameList frameListFap = new RaceFrameList(new int[27] { 0, 1, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 1, 0 }, new float[27] { 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.3f, 0.3f, 0.3f, 0.3f, 0.2f, 0.2f, 0.15f, 0.15f, 0.15f, 0.15f, 0.15f, 0.15f, 0.15f, 0.15f, 0.15f, 0.2f, 0.25f, 0.3f, 0.35f, 0.4f, 0.4f, });

    internal override int DickSizes => 1;

    public Ki()
    {
        GentleAnimation = true;
        CanBeGender = new List<Gender>() { Gender.Male };
        Body = new SpriteExtraInfo(2, BodySprite, WhiteColored); // Body on all poses.
        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, WhiteColored); // Front legs on large belly sizes.
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, WhiteColored); // Either rear body or tail, depending on pose.
        Dick = new SpriteExtraInfo(3, DickSprite, WhiteColored); // Sheath or CV devour sprite. 
        Belly = new SpriteExtraInfo(4, null, WhiteColored);
        Balls = new SpriteExtraInfo(0, BallsSprite, WhiteColored); // Only used with CV.
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Ki";
        unit.PreferredVoreType = VoreType.Oral;
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        float bodyAccent = 0;
        float bodyAccent2 = 0;
        float body = 0;

        if (actor.GetBallSize(9, 0.48f) > 0)
        {
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) && actor.GetBallSize(9, .48f) == 9)
            {
                bodyAccent2 = 110;
                body = 110;
            }
            else switch (actor.GetBallSize(9, 0.48f))
                {
                    case 1: { bodyAccent2 = 0; body = 0; break; }
                    case 2: { bodyAccent2 = 6; body = 6; break; }
                    case 3: { bodyAccent2 = 12; body = 12; break; }
                    case 4: { bodyAccent2 = 31; body = 31; break; }
                    case 5: { bodyAccent2 = 59; body = 59; break; }
                    case 6: { bodyAccent2 = 77; body = 77; break; }
                    case 7: { bodyAccent2 = 78; body = 78; break; }
                    default: { bodyAccent2 = 100; body = 100; break; }
                }
        }
        else
        {
            int bellySize = BellySize(actor);
            switch (bellySize)
            {
                case 2: { bodyAccent = 2; body = 2; break; }
                case 3: { bodyAccent = 5; body = 5; break; }
                case 4: { bodyAccent = 8; body = 8; break; }
                case 5: { bodyAccent = 12; body = 12; break; }
                case 6: { bodyAccent = 16; body = 16; break; }
                case 7: { bodyAccent = 22; body = 22; break; }
                case 8: { bodyAccent = 28; body = 28; break; }
                case 9: { bodyAccent = 35; body = 35; break; }
                case 10: { bodyAccent = 44; body = 44; break; }
                case 11: { bodyAccent = 50; body = 50; break; }
                case 12: { bodyAccent = 58; body = 58; break; }
                case 13: { bodyAccent = 70; body = 70; break; }
                case 14: { bodyAccent2 = 0; bodyAccent = 0; body = 0; break; }
                case 15: { bodyAccent2 = 10; bodyAccent = 10; body = 10; break; }
                case 16: { bodyAccent2 = 22; bodyAccent = 22; body = 22; break; }
                case 17: { bodyAccent2 = 34; bodyAccent = 34; body = 34; break; }
                case 18: { bodyAccent2 = 34; bodyAccent = 34; body = 34; break; }
                case 19: { bodyAccent2 = 34; bodyAccent = 34; body = 34; break; }
                case 20: { bodyAccent2 = 35; bodyAccent = 35; body = 35; break; }
                case 21: { bodyAccent2 = 54; bodyAccent = 54; body = 54; break; }
            }
        }
        AddOffset(Body, 0, body * pixelOffset);
        AddOffset(BodyAccent, 0, bodyAccent * pixelOffset);
        AddOffset(BodyAccent2, 0, bodyAccent2 * pixelOffset);

    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(0, 0, false),  // Tail controller. Index 0.
            new AnimationController.FrameList(0, 0, false)}; // Fap controller. Index 1.
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        if (actor.GetBallSize(9, 0.48f) > 0)
        {
            if (!actor.Targetable) return SpriteDictionary.Ki[31];

            if (actor.AnimationController.frameLists[1].currentlyActive)
            {
                if (actor.AnimationController.frameLists[1].currentTime >= frameListFap.times[actor.AnimationController.frameLists[0].currentFrame])
                {
                    actor.AnimationController.frameLists[1].currentFrame++;
                    actor.AnimationController.frameLists[1].currentTime = 0f;

                    if (actor.AnimationController.frameLists[1].currentFrame >= frameListFap.frames.Length)
                    {
                        actor.AnimationController.frameLists[1].currentlyActive = false;
                        actor.AnimationController.frameLists[1].currentFrame = 0;
                        actor.AnimationController.frameLists[1].currentTime = 0f;
                    }
                }

                return SpriteDictionary.Ki[31 + frameListFap.frames[actor.AnimationController.frameLists[1].currentFrame]];
            }

            if (actor.PredatorComponent?.BallsFullness > 0 && State.Rand.Next(800) == 0)
            {
                actor.AnimationController.frameLists[1].currentlyActive = true;
            }

            return SpriteDictionary.Ki[31];
        }

        int bellySize = BellySize(actor);
        if (bellySize > 13)
        {
            if (actor.IsOralVoring) return SpriteDictionary.Ki[7];
            return SpriteDictionary.Ki[6];
        }

        if (actor.IsOralVoring) return SpriteDictionary.Ki[1];
        return SpriteDictionary.Ki[0];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        int bellySize = BellySize(actor);
        if (bellySize > 13) return null;
        if (bellySize > 9) return SpriteDictionary.Ki[4];
        if (bellySize > 0) return SpriteDictionary.Ki[2];
        return null;
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (actor.GetBallSize(9, 0.48f) > 0)
        {
            if (!actor.Targetable) return SpriteDictionary.Ki[35];

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

                switch (frameListTail.frames[actor.AnimationController.frameLists[0].currentFrame])
                {
                    case 0: return null;
                    default: return SpriteDictionary.Ki[34 + frameListTail.frames[actor.AnimationController.frameLists[0].currentFrame]];
                }
            }

            if (State.Rand.Next(500) == 0)
            {
                actor.AnimationController.frameLists[0].currentlyActive = true;
            }

            return SpriteDictionary.Ki[35];
        }

        int bellySize = BellySize(actor);
        if (bellySize > 13) return SpriteDictionary.Ki[5];
        return null;
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.GetBallSize(9, 0.48f) >= 1) return null;
        if (actor.IsCockVoring) return SpriteDictionary.Ki[30];
        if (actor.HasBelly) return null;
        if (!Config.HideCocks && (actor.PredatorComponent?.VisibleFullness ?? 0) == 0) return SpriteDictionary.Ki[3];
        return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {

        if (actor.GetBallSize(9, 0.48f) > 0) return null;

        if (actor.Unit.Predator == false || actor.PredatorComponent.VisibleFullness == 0)
        {
            return null;
        }



        int bellySize = BellySize(actor);

        if (bellySize > 13)
            Body.layer = 5;
        else
            Body.layer = 2;

        switch (bellySize)
        {
            case 0: return SpriteDictionary.Ki[8];
            case 1: return SpriteDictionary.Ki[9];
            case 2: return SpriteDictionary.Ki[10];
            case 3: return SpriteDictionary.Ki[11];
            case 4: return SpriteDictionary.Ki[12];
            case 5: return SpriteDictionary.Ki[13];
            case 6: return SpriteDictionary.Ki[14];
            case 7: return SpriteDictionary.Ki[15];
            case 8: return SpriteDictionary.Ki[16];
            case 9: return SpriteDictionary.Ki[17];
            case 10: return SpriteDictionary.Ki[18];
            case 11: return SpriteDictionary.Ki[19];
            case 12: return SpriteDictionary.Ki[20];
            case 13: return SpriteDictionary.Ki[21];
            case 14: return SpriteDictionary.Ki[22];
            case 15: return SpriteDictionary.Ki[23];
            case 16: return SpriteDictionary.Ki[24];
            case 17: return SpriteDictionary.Ki[25];
            case 18: return SpriteDictionary.Ki[26];
            case 19: return SpriteDictionary.Ki[27];
            case 20: return SpriteDictionary.Ki[28];
            case 21: return SpriteDictionary.Ki[29];
            default: return null;
        }
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.GetBallSize(9, 0.48f) <= 0) return null;
        else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) && actor.GetBallSize(9, .48f) == 9)
        {
            return SpriteDictionary.Ki[47];
        }
        else switch (actor.GetBallSize(9, 0.48f))
            {
                case 1: return SpriteDictionary.Ki[39];
                case 2: return SpriteDictionary.Ki[40];
                case 3: return SpriteDictionary.Ki[41];
                case 4: return SpriteDictionary.Ki[42];
                case 5: return SpriteDictionary.Ki[43];
                case 6: return SpriteDictionary.Ki[44];
                case 7: return SpriteDictionary.Ki[45];
                default: return SpriteDictionary.Ki[46];
            }
    }

    private static int BellySize(Actor_Unit actor)
    {
        if (actor.Unit.Predator == false)
            return 0;
        int bellySize = actor.GetStomachSize(21, 0.66f);

        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true))
        {

        }
        else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false))
        {
            bellySize = Math.Min(bellySize, 20);
        }
        else
            bellySize = Math.Min(bellySize, 17);
        return bellySize;
    }
}

