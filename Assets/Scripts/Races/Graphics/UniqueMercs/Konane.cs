using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Taraluxia;

class Konane : BlankSlate
{
    readonly Sprite[] Sprites = SpriteDictionary.Konane;

    RaceFrameList KonaneSwallowHead = new RaceFrameList(new int[3] {16,18,17}, new float[3] { .15f, .85f, 1f});
    RaceFrameList KonaneSwallowChest = new RaceFrameList(new int[6] {23,19,20,21,22,23}, new float[6] { .25f, .25f, .1f, .1f, .1f, 1.2f});
    RaceFrameList KonaneIdle = new RaceFrameList(new int[3] {13,14,12}, new float[3] { .1f, .1f, .5f});

    public Konane()
    {
        CanBeGender = new List<Gender>() { Gender.Male };
        GentleAnimation = true;
        Body = new SpriteExtraInfo(2, BodySprite, WhiteColored);
        Head = new SpriteExtraInfo(6, HeadSprite, WhiteColored);
        Hair = null;
        Hair2 = null;
        Belly = new SpriteExtraInfo(4, null, WhiteColored);
        Breasts = null;
        BodyAccessory = new SpriteExtraInfo(0, AccessorySprite, WhiteColored); //tail
        BodyAccent = new SpriteExtraInfo(0, BodyAccentSprite, WhiteColored);   // Right Wing
        BodyAccent2 = new SpriteExtraInfo(0, BodyAccentSprite2, WhiteColored); // Left Wing
        BodyAccent3 = new SpriteExtraInfo(7, BodyAccentSprite3, WhiteColored); // Right Arm
        BodyAccent4 = new SpriteExtraInfo(3, BodyAccentSprite4, WhiteColored); // Left Arm
        BodyAccent5 = new SpriteExtraInfo(5, BodyAccentSprite5, WhiteColored); // Chest
        BodyAccent6 = new SpriteExtraInfo(3, BodyAccentSprite6, WhiteColored); // Left Claw
        clothingColors = 0;
    }

    internal override int BreastSizes => 5;
    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[]
        {
            new AnimationController.FrameList(0, 0, false),
            new AnimationController.FrameList(0, 0, false),
            new AnimationController.FrameList(0, 0, false),
        };
        actor.AnimationController.frameLists[2].currentlyActive = false;
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null || actor.AnimationController.frameLists.Count() == 0) SetUpAnimations(actor);
    }
    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(BodyAccent2, 25 * .625f, 0);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Konane";
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        return Sprites[0];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        return Sprites[1];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsSpecialAttacking)
        {
            return Sprites[15];
        }
        if (actor.HasJustVored) //Swallow Animation
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
            if (actor.AnimationController.frameLists[0].currentlyActive)
            {
                if (actor.AnimationController.frameLists[0].currentTime >= KonaneSwallowHead.times[actor.AnimationController.frameLists[0].currentFrame])
                {
                    actor.AnimationController.frameLists[0].currentFrame++;
                    actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (actor.AnimationController.frameLists[0].currentFrame >= KonaneSwallowHead.frames.Length)
                    {
                        actor.AnimationController.frameLists[0].currentFrame = 0;
                        actor.AnimationController.frameLists[0].currentTime = 0f;
                        actor.AnimationController.frameLists[0].currentlyActive = false;
                    }
                }
            }
            else
                return null;
            return Sprites[KonaneSwallowHead.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }
        if (actor.IsOralVoring)
            return Sprites[16];
        if (State.Rand.Next(2000) == 0)
        {
            actor.AnimationController.frameLists[2].currentlyActive = true;
        }
        if (actor.AnimationController.frameLists[2].currentlyActive == false)
        {
            return Sprites[12];
        }
        if (actor.AnimationController.frameLists[2].currentlyActive)
            {
                if (actor.AnimationController.frameLists[2].currentTime >= KonaneIdle.times[actor.AnimationController.frameLists[2].currentFrame])
                {
                    actor.AnimationController.frameLists[2].currentFrame++;
                    actor.AnimationController.frameLists[2].currentTime = 0f;

                    if (actor.AnimationController.frameLists[2].currentFrame >= KonaneIdle.frames.Length)
                    {
                        actor.AnimationController.frameLists[2].currentFrame = 0;
                        actor.AnimationController.frameLists[2].currentTime = 0f;
                        actor.AnimationController.frameLists[2].currentlyActive = false;
                    }
                }
            }
            return Sprites[KonaneIdle.frames[actor.AnimationController.frameLists[2].currentFrame]];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        int size = actor.GetStomachSize(27);
        if (!actor.HasBelly)
            return null;
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size >= 18)
            return Sprites[45];
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size >= 12)
        {
            if (size >= 18) return Sprites[44];
            if (size >= 17) return Sprites[43];
            if (size >= 16) return Sprites[42];
            if (size >= 15) return Sprites[41];
            if (size >= 14) return Sprites[40];
            if (size >= 13) return Sprites[39];
            if (size >= 12) return Sprites[38];
        }
        if (size > 19)
            size = 19;
        return Sprites[18 + size];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Right WIng
    {
        if (actor.IsSpecialAttacking)
        {
            return Sprites[3];
        }
        return Sprites[2];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Left Wing
    {
        if (actor.IsSpecialAttacking)
        {
            return Sprites[5];
        }
        return Sprites[4];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Right Arm
    {
        if (actor.IsSpecialAttacking)
        {
            return Sprites[11];
        }
        if (actor.IsAttacking) 
        {
            return Sprites[11];
        }
        return Sprites[10];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Left Arm
    {
        if (actor.IsSpecialAttacking)
        {
            return Sprites[8];
        }
        if (actor.IsAttacking)
        {
            return Sprites[7];
        }
        return Sprites[6];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Left Claw
    {
        if (actor.IsSpecialAttacking)
        {
            return null;
        }
        if (actor.IsAttacking)
        {
            return null;
        }
        return Sprites[9];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // chest
    {
        if (actor.HasJustVored) //Swallow Animation
        {
            actor.AnimationController.frameLists[1].currentlyActive = true;
            if (actor.AnimationController.frameLists[1].currentlyActive)
            {
                if (actor.AnimationController.frameLists[1].currentTime >= KonaneSwallowChest.times[actor.AnimationController.frameLists[1].currentFrame])
                {
                    actor.AnimationController.frameLists[1].currentFrame++;
                    actor.AnimationController.frameLists[1].currentTime = 0f;

                    if (actor.AnimationController.frameLists[1].currentFrame >= KonaneSwallowChest.frames.Length)
                    {
                        actor.AnimationController.frameLists[1].currentFrame = 0;
                        actor.AnimationController.frameLists[1].currentTime = 0f;
                        actor.AnimationController.frameLists[1].currentlyActive = false;
                    }
                }
            }
            else
                return null;
            return Sprites[KonaneSwallowChest.frames[actor.AnimationController.frameLists[1].currentFrame]];
        }
        return Sprites[23];
    }
}
