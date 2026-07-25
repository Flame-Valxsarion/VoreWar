using System.Collections.Generic;
using UnityEngine;

class DarkSwallower : BlankSlate
{
    RaceFrameList frameListTail = new RaceFrameList(new int[8] { 0, 1, 2, 3, 4, 3, 2, 1 }, new float[8] { 1.2f, 1f, 1f, 1f, 1.2f, 1f, 1f, 1f });
    RaceFrameList frameListFins = new RaceFrameList(new int[4] { 0, 1, 2, 1 }, new float[4] { 1f, .8f, 1f, .8f });

    public DarkSwallower()
    {
        EyeTypes = 6;
        CanBeGender = new List<Gender>() { Gender.None };
        SkinColors = ColorMap.SharkColorCount;

        Body = new SpriteExtraInfo(3, BodySprite, (s) => ColorMap.GetSharkColor(s.Unit.SkinColor)); // Body, open mouth base.
        Eyes = new SpriteExtraInfo(4, EyesSprite, WhiteColored); // Eyes.
        BodyAccent = new SpriteExtraInfo(0, BodyAccentSprite, (s) => ColorMap.GetSharkColor(s.Unit.SkinColor)); // Tail.
        Mouth = new SpriteExtraInfo(5, MouthSprite, WhiteColored); // Mouth inside.
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, (s) => ColorMap.GetSharkColor(s.Unit.SkinColor)); // Sidefins.
        BodyAccent3 = new SpriteExtraInfo(1, BodyAccentSprite3, (s) => ColorMap.GetSharkColor(s.Unit.SkinColor)); // Bellyfins.
        Belly = new SpriteExtraInfo(2, null, (s) => ColorMap.GetSharkColor(s.Unit.SkinColor)); // Belly.     
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[]
        { new AnimationController.FrameList(State.Rand.Next(0, 8), State.Rand.Next(1, 7) / 10f, true), // Tail controller. Index 0.
          new AnimationController.FrameList(State.Rand.Next(0, 4), State.Rand.Next(1, 9) / 10f, true)}; // Fin controller. Index 1.
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        if (actor.IsEating || actor.IsAttacking)
            return SpriteDictionary.DarkSwallower[1];
        return SpriteDictionary.DarkSwallower[0];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (!actor.Targetable) return SpriteDictionary.DarkSwallower[9];

        if (actor.AnimationController.frameLists[0].currentTime >= frameListTail.times[actor.AnimationController.frameLists[0].currentFrame])
        {
            actor.AnimationController.frameLists[0].currentFrame++;
            actor.AnimationController.frameLists[0].currentTime = 0f;

            if (actor.AnimationController.frameLists[0].currentFrame >= frameListTail.frames.Length)
            {
                actor.AnimationController.frameLists[0].currentFrame = 0;
            }
        }

        return SpriteDictionary.DarkSwallower[9 + frameListTail.frames[actor.AnimationController.frameLists[0].currentFrame]];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (!actor.Targetable) return SpriteDictionary.DarkSwallower[14];

        if (actor.AnimationController.frameLists[1].currentTime >= frameListFins.times[actor.AnimationController.frameLists[1].currentFrame])
        {
            actor.AnimationController.frameLists[1].currentFrame++;
            actor.AnimationController.frameLists[1].currentTime = 0f;

            if (actor.AnimationController.frameLists[1].currentFrame >= frameListFins.frames.Length)
            {
                actor.AnimationController.frameLists[1].currentFrame = 0;
            }
        }

        return SpriteDictionary.DarkSwallower[14 + frameListFins.frames[actor.AnimationController.frameLists[1].currentFrame]];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (!actor.Targetable) return SpriteDictionary.DarkSwallower[17];

        if (actor.AnimationController.frameLists[0].currentFrame % 2 == 0) return SpriteDictionary.DarkSwallower[17];
        return SpriteDictionary.DarkSwallower[18];
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => SpriteDictionary.DarkSwallower[2 + actor.Unit.EyeType];

    protected override Sprite MouthSprite(Actor_Unit actor) => (actor.IsAttacking || actor.IsEating) ? SpriteDictionary.DarkSwallower[8] : null;

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return SpriteDictionary.DarkSwallower[19];

        int size = actor.GetStomachSize(29);

        if (size >= 28 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
            return SpriteDictionary.DarkSwallower[44];
        }

        if (size >= 26 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.DarkSwallower[43];
        }

        if (size >= 24 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.DarkSwallower[42];
        }

        if (size >= 22 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return SpriteDictionary.DarkSwallower[41];
        }

        if (size > 21) size = 21;

        return SpriteDictionary.DarkSwallower[19 + size];
    }
}

