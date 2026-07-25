using System.Collections.Generic;
using UnityEngine;

class Abakhanskya : BlankSlate
{
    RaceFrameList AbaIdle = new RaceFrameList(new int[4] { 0, 1, 2, 1 }, new float[4] { .8f, .25f, .8f, .25f });
    RaceFrameList AbaSwallowHead = new RaceFrameList(new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 7, 6 }, new float[10] { .15f, .15f, .15f, .15f, .2f, .3f, .3f, .3f, .3f, .4f });
    RaceFrameList AbaSwallowBelly = new RaceFrameList(new int[8] { 0, 0, 1, 2, 3, 4, 5, 6 }, new float[8] { 0.01f, 1.0f, .25f, .25f, .25f, .25f, .50f, .6f });
    
    readonly Sprite[] Sprites = SpriteDictionary.Abakhanskya;

    public Abakhanskya()
    {
        CanBeGender = new List<Gender>() { Gender.Female };
        BodySizes = 8;

        GentleAnimation = true;

        Body = new SpriteExtraInfo(2, BodySprite, WhiteColored); //Back Spines
        Head = new SpriteExtraInfo(4, HeadSprite, WhiteColored); //Its her head, man...
        Breasts = new SpriteExtraInfo(8, BreastsSprite, WhiteColored); //Boob 1
        SecondaryBreasts = new SpriteExtraInfo(2, SecondaryBreastsSprite, WhiteColored); //Boob Deux
        BodyAccessory = new SpriteExtraInfo(1, AccessorySprite, WhiteColored); //Back Spines
        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, WhiteColored); //Left Arm
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, WhiteColored); //Right Arm
        BodyAccent3 = new SpriteExtraInfo(2, BodyAccentSprite3, WhiteColored); // Tail
        BodyAccent4 = new SpriteExtraInfo(3, BodyAccentSprite4, WhiteColored); // Legs
        BodyAccent5 = new SpriteExtraInfo(1, BodyAccentSprite5, WhiteColored); // Shadow
        BodyAccent6 = new SpriteExtraInfo(7, BodyAccentSprite6, WhiteColored); // Swallowed belly bulge
        Belly = new SpriteExtraInfo(5, null, WhiteColored); // You know, the reason we're all here?

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
        unit.Name = "Abakhanskya";
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        AddOffset(Body, 35, 50 * .85f);
        return Sprites[31];
    }

    //protected override Sprite AccessorySprite(Actor_Unit actor)
    //{
    //    return Sprites[31];
    //}

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        AddOffset(Head, 35, 50 * .85f);
        if (actor.IsOralVoring) return Sprites[38];
        if (actor.HasJustVored)
        {
            actor.AnimationController.frameLists[1].currentlyActive = true;
            if (actor.AnimationController.frameLists[1].currentTime >= AbaSwallowHead.times[actor.AnimationController.frameLists[1].currentFrame] && actor.Unit.IsDead == false)
            {
                actor.AnimationController.frameLists[1].currentFrame++;
                actor.AnimationController.frameLists[1].currentTime = 0f;
                if (actor.AnimationController.frameLists[1].currentFrame >= AbaSwallowHead.frames.Length)
                {
                    actor.AnimationController.frameLists[1].currentlyActive = false;
                    actor.AnimationController.frameLists[1].currentFrame = 0;
                    actor.AnimationController.frameLists[1].currentTime = 0f;
                }
            }
            return Sprites[38 + AbaSwallowHead.frames[actor.AnimationController.frameLists[1].currentFrame]];
        } 
        if (actor.AnimationController.frameLists[0].currentTime >= AbaIdle.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
        {
            actor.AnimationController.frameLists[0].currentFrame++;
            actor.AnimationController.frameLists[0].currentTime = 0f;

            if (actor.AnimationController.frameLists[0].currentFrame >= AbaIdle.frames.Length)
            {
                actor.AnimationController.frameLists[0].currentFrame = 0;
                actor.AnimationController.frameLists[0].currentTime = 0f;
            }
        }
        return Sprites[35 + AbaIdle.frames[actor.AnimationController.frameLists[0].currentFrame]];
    }

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        AddOffset(Breasts, 35, 50 * .85f);
        if (actor.IsOralVoring)
            {
            if (actor.Unit.BodySize >= 7)
                return Sprites[9];
            else
                return Sprites[6];
            }
        if (actor.Unit.BodySize >= 7)
            {
                if (actor.AnimationController.frameLists[0].currentTime >= AbaIdle.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
                {
                    actor.AnimationController.frameLists[0].currentFrame++;
                    actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (actor.AnimationController.frameLists[0].currentFrame >= AbaIdle.frames.Length)
                    {
                        actor.AnimationController.frameLists[0].currentFrame = 0;
                        actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }
                return Sprites[9 + AbaIdle.frames[actor.AnimationController.frameLists[0].currentFrame]];
            }
            else
            {
                if (actor.AnimationController.frameLists[0].currentTime >= AbaIdle.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
                {
                    actor.AnimationController.frameLists[0].currentFrame++;
                    actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (actor.AnimationController.frameLists[0].currentFrame >= AbaIdle.frames.Length)
                    {
                        actor.AnimationController.frameLists[0].currentFrame = 0;
                        actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }
                return Sprites[6 + AbaIdle.frames[actor.AnimationController.frameLists[0].currentFrame]];
            }
    }

    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        AddOffset(SecondaryBreasts, 35, 50 * .85f);
        if (actor.IsOralVoring)
            {
            if (actor.Unit.BodySize >= 7)
                return Sprites[3];
            else
                return Sprites[0];
            }
        if (actor.Unit.BodySize >= 7)
            {
                if (actor.AnimationController.frameLists[0].currentTime >= AbaIdle.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
                {
                    actor.AnimationController.frameLists[0].currentFrame++;
                    actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (actor.AnimationController.frameLists[0].currentFrame >= AbaIdle.frames.Length)
                    {
                        actor.AnimationController.frameLists[0].currentFrame = 0;
                        actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }
                return Sprites[3 + AbaIdle.frames[actor.AnimationController.frameLists[0].currentFrame]];
            }
            else
            {
                if (actor.AnimationController.frameLists[0].currentTime >= AbaIdle.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
                {
                    actor.AnimationController.frameLists[0].currentFrame++;
                    actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (actor.AnimationController.frameLists[0].currentFrame >= AbaIdle.frames.Length)
                    {
                        actor.AnimationController.frameLists[0].currentFrame = 0;
                        actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }
                return Sprites[0 + AbaIdle.frames[actor.AnimationController.frameLists[0].currentFrame]];
            }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Left Arm
    {
        AddOffset(BodyAccent, 35, 50 * .85f);
        if (actor.IsAttacking)
            return Sprites[24];
        else
            {
                if (actor.AnimationController.frameLists[0].currentTime >= AbaIdle.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
                {
                    actor.AnimationController.frameLists[0].currentFrame++;
                    actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (actor.AnimationController.frameLists[0].currentFrame >= AbaIdle.frames.Length)
                    {
                        actor.AnimationController.frameLists[0].currentFrame = 0;
                        actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }
                return Sprites[24 + AbaIdle.frames[actor.AnimationController.frameLists[0].currentFrame]];
            }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) //Right Arm
    {
        AddOffset(BodyAccent2, 35, 50 * .85f);
        if (actor.IsAttacking)
            return Sprites[30];
        else
            {
                if (actor.AnimationController.frameLists[0].currentTime >= AbaIdle.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
                {
                    actor.AnimationController.frameLists[0].currentFrame++;
                    actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (actor.AnimationController.frameLists[0].currentFrame >= AbaIdle.frames.Length)
                    {
                        actor.AnimationController.frameLists[0].currentFrame = 0;
                        actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }
                return Sprites[27 + AbaIdle.frames[actor.AnimationController.frameLists[0].currentFrame]];
            }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Tail
    {
        AddOffset(BodyAccent3, 35, 50 * .85f);
        if (actor.Unit.BodySize >= 3)
            return Sprites[23];
        else
            return Sprites[22];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Leggies
    {
        AddOffset(BodyAccent4, 35, 50 * .85f);
        if (actor.Unit.BodySize >= 5)
            return Sprites[21];
        else
            return Sprites[20];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Shadow
    {
        AddOffset(BodyAccent5, 35, 50 * .85f);
        int size = actor.GetStomachSize(21);
        if (size >= 10)
            return Sprites[34];
        else if (size >= 5)
            return Sprites[33];
        else if (size < 5) 
            return Sprites[32];
        else return Sprites[32];
    }
    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Swallow Bulge
    {
        AddOffset(BodyAccent6, 35, 50 * .85f);
        if (actor.HasJustVored)
            {
                actor.AnimationController.frameLists[2].currentlyActive = true;
                if (actor.AnimationController.frameLists[2].currentTime >= AbaSwallowBelly.times[actor.AnimationController.frameLists[2].currentFrame] && actor.Unit.IsDead == false)
                {
                    actor.AnimationController.frameLists[2].currentFrame++;
                    actor.AnimationController.frameLists[2].currentTime = 0f;
                    if (actor.AnimationController.frameLists[2].currentFrame >= AbaSwallowBelly.frames.Length)
                    {
                        actor.AnimationController.frameLists[2].currentlyActive = false;
                        actor.AnimationController.frameLists[2].currentFrame = 0;
                        actor.AnimationController.frameLists[2].currentTime = 0f;
                    }
                }
                return Sprites[47 + AbaSwallowBelly.frames[actor.AnimationController.frameLists[2].currentFrame]];
            }
        else return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // O R B
    {
        AddOffset(Belly, 35, 50 * .85f);
        int size = actor.GetStomachSize(21);
        if (actor.HasBelly == false)
            {
            //belly.SetActive(false);
            return Sprites[12];
            }
        else if (actor.HasBelly == true)
            {
            //belly.SetActive(true);
            return Sprites[12 + actor.GetStomachSize(7)];
            }
        else return null;
    }
}

