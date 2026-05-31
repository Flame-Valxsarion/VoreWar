using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Taraluxia;

class Konane : BlankSlate
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.Konane;

    bool SpecialAttack = false;
    RaceFrameList KonaneSwallowHead = new RaceFrameList(new int[2] {14,15}, new float[2] { .25f, .25f});
    RaceFrameList KonaneSwallowChest = new RaceFrameList(new int[2] {16,17}, new float[2] { .5f, .25f});

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
        BodyAccent = new SpriteExtraInfo(0, BodyAccentSprite, WhiteColored); // Right Wing
        BodyAccent2 = new SpriteExtraInfo(0, BodyAccentSprite2, WhiteColored); // Left Wing
        BodyAccent3 = new SpriteExtraInfo(7, BodyAccentSprite3, WhiteColored); // Right Arm
        BodyAccent4 = new SpriteExtraInfo(3, BodyAccentSprite4, WhiteColored); // Left Arm
        BodyAccent5 = new SpriteExtraInfo(5, BodyAccentSprite5, WhiteColored); // Chest
        clothingColors = 0;
    }

    internal override int BreastSizes => 5;
    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[]
        {
            new AnimationController.FrameList(0, 0, false),
            new AnimationController.FrameList(0, 0, false),
        };
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
        if (actor.IsAttacking || SpecialAttack)
        {
            return Sprites[13];
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
            return Sprites[14];
        return Sprites[12];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        int size = actor.GetStomachSize(27);

        if (!actor.HasBelly)
            return null;
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(27, 1) == 27)
            return Sprites[45];
        if (size > 19 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false)) size = 19;

        return Sprites[18 + size];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Right WIng
    {
        if (SpecialAttack)
        {
            return Sprites[3];
        }
        return Sprites[2];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Left Wing
    {
        if (SpecialAttack)
        {
            return Sprites[5];
        }
        return Sprites[4];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Right Arm
    {
        if (actor.IsAttacking) 
        {
            if (SpecialAttack)
            {
                return Sprites[11];
            }
            return Sprites[11];
        }
        return Sprites[10];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Left Arm
    {
        if (actor.IsAttacking)
        {
            if (SpecialAttack)
            {
                return Sprites[8];
            }
            return Sprites[7];
        }
        return Sprites[6];
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
        return Sprites[16];
    }
}
