using System.Collections.Generic;
using UnityEngine;

class Raptor : BlankSlate
{
    RaceFrameList frameListTail = new RaceFrameList(new int[24] { 0, 4, 5, 6, 5, 4, 0, 3, 2, 1, 2, 3, 0, 4, 5, 6, 5, 4, 0, 3, 2, 1, 2, 3 }, new float[24] { 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f });

    public Raptor()
    {
        GentleAnimation = true;
        CanBeGender = new List<Gender>() { Gender.Male };
        SkinColors = ColorMap.LizardColorCount;
        ExtraColors1 = ColorMap.LizardColorCount;
        Body = new SpriteExtraInfo(8, BodySprite, (s) => ColorMap.GetLizardColor(s.Unit.SkinColor));
        Belly = new SpriteExtraInfo(7, null, (s) => ColorMap.GetLizardColor(s.Unit.SkinColor));
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, (s) => ColorMap.GetLizardColor(s.Unit.SkinColor)); // Legs
        BodyAccent2 = new SpriteExtraInfo(10, BodyAccentSprite2, (s) => ColorMap.GetLizardColor(s.Unit.ExtraColor1)); // Body Stripes
        BodyAccent3 = new SpriteExtraInfo(4, BodyAccentSprite3, (s) => ColorMap.GetLizardColor(s.Unit.ExtraColor1)); // Leg Stripes
        BodyAccent4 = new SpriteExtraInfo(1, BodyAccentSprite4, (s) => ColorMap.GetLizardColor(s.Unit.SkinColor)); // Tail
        BodyAccent5 = new SpriteExtraInfo(2, BodyAccentSprite5, (s) => ColorMap.GetLizardColor(s.Unit.ExtraColor1)); // Tail Stripes
        BodyAccent6 = new SpriteExtraInfo(0, BodyAccentSprite6, (s) => ColorMap.GetLizardColor(s.Unit.SkinColor)); // Balls
        Eyes = new SpriteExtraInfo(5, EyesSprite, (s) => ColorMap.GetLizardColor(s.Unit.ExtraColor1));
        Mouth = new SpriteExtraInfo(9, MouthSprite, WhiteColored);
        Dick = new SpriteExtraInfo(6, DickSprite, WhiteColored);
    }

    internal override void RandomCustom(Unit unit)
    {
        unit.SkinColor = State.Rand.Next(SkinColors);
        unit.ExtraColor1 = State.Rand.Next(ExtraColors1);
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

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.HasBelly == false)
        {
            AddOffset(Dick, 0, 0 * .3125f);
            AddOffset(BodyAccent, 0, 0 * .3125f);
            AddOffset(BodyAccent2, 0, 0 * .3125f);
            AddOffset(BodyAccent3, 0, 0 * .3125f);
            AddOffset(BodyAccent4, 0, 0 * .3125f);
            AddOffset(BodyAccent5, 0, 0 * .3125f);
            AddOffset(Body, 0, 0 * .3125f);
            AddOffset(Eyes, 0, 0 * .3125f);
            AddOffset(Mouth, 0, 0 * .3125f);
        }

        else
        {
            int size = actor.GetStomachSize(24, 2);

            if (size == 24 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false))
            {
                AddOffset(Dick, 0, 176 * .3125f);
                AddOffset(BodyAccent, 0, 176 * .3125f);
                AddOffset(BodyAccent2, 0, 176 * .3125f);
                AddOffset(BodyAccent3, 0, 176 * .3125f);
                AddOffset(BodyAccent4, 0, 176 * .3125f);
                AddOffset(BodyAccent5, 0, 176 * .3125f);
                AddOffset(Body, 0, 176 * .3125f);
                AddOffset(Eyes, 0, 176 * .3125f);
                AddOffset(Mouth, 0, 176 * .3125f);
            }

            else if (size >= 23 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
            {
                AddOffset(Dick, 0, 168 * .3125f);
                AddOffset(BodyAccent, 0, 168 * .3125f);
                AddOffset(BodyAccent2, 0, 168 * .3125f);
                AddOffset(BodyAccent3, 0, 168 * .3125f);
                AddOffset(BodyAccent4, 0, 168 * .3125f);
                AddOffset(BodyAccent5, 0, 168 * .3125f);
                AddOffset(Body, 0, 168 * .3125f);
                AddOffset(Eyes, 0, 168 * .3125f);
                AddOffset(Mouth, 0, 168 * .3125f);
            }

            else if (size == 22 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
            {
                AddOffset(Dick, 0, 152 * .3125f);
                AddOffset(BodyAccent, 0, 152 * .3125f);
                AddOffset(BodyAccent2, 0, 152 * .3125f);
                AddOffset(BodyAccent3, 0, 152 * .3125f);
                AddOffset(BodyAccent4, 0, 152 * .3125f);
                AddOffset(BodyAccent5, 0, 152 * .3125f);
                AddOffset(Body, 0, 152 * .3125f);
                AddOffset(Eyes, 0, 152 * .3125f);
                AddOffset(Mouth, 0, 152 * .3125f);
            }

            else if (size >= 21)
            {
                AddOffset(Dick, 0, 144 * .3125f);
                AddOffset(BodyAccent, 0, 144 * .3125f);
                AddOffset(BodyAccent2, 0, 144 * .3125f);
                AddOffset(BodyAccent3, 0, 144 * .3125f);
                AddOffset(BodyAccent4, 0, 144 * .3125f);
                AddOffset(BodyAccent5, 0, 144 * .3125f);
                AddOffset(Body, 0, 144 * .3125f);
                AddOffset(Eyes, 0, 144 * .3125f);
                AddOffset(Mouth, 0, 144 * .3125f);
            }

            else if (size == 20)
            {
                AddOffset(Dick, 0, 128 * .3125f);
                AddOffset(BodyAccent, 0, 128 * .3125f);
                AddOffset(BodyAccent2, 0, 128 * .3125f);
                AddOffset(BodyAccent3, 0, 128 * .3125f);
                AddOffset(BodyAccent4, 0, 128 * .3125f);
                AddOffset(BodyAccent5, 0, 128 * .3125f);
                AddOffset(Body, 0, 128 * .3125f);
                AddOffset(Eyes, 0, 128 * .3125f);
                AddOffset(Mouth, 0, 128 * .3125f);
            }

            else if (size == 19)
            {
                AddOffset(Dick, 0, 112 * .3125f);
                AddOffset(BodyAccent, 0, 112 * .3125f);
                AddOffset(BodyAccent2, 0, 112 * .3125f);
                AddOffset(BodyAccent3, 0, 112 * .3125f);
                AddOffset(BodyAccent4, 0, 112 * .3125f);
                AddOffset(BodyAccent5, 0, 112 * .3125f);
                AddOffset(Body, 0, 112 * .3125f);
                AddOffset(Eyes, 0, 112 * .3125f);
                AddOffset(Mouth, 0, 112 * .3125f);
            }

            else if (size == 18)
            {
                AddOffset(Dick, 0, 96 * .3125f);
                AddOffset(BodyAccent, 0, 96 * .3125f);
                AddOffset(BodyAccent2, 0, 96 * .3125f);
                AddOffset(BodyAccent3, 0, 96 * .3125f);
                AddOffset(BodyAccent4, 0, 96 * .3125f);
                AddOffset(BodyAccent5, 0, 96 * .3125f);
                AddOffset(Body, 0, 96 * .3125f);
                AddOffset(Eyes, 0, 96 * .3125f);
                AddOffset(Mouth, 0, 96 * .3125f);
            }

            else if (size == 17)
            {
                AddOffset(Dick, 0, 80 * .3125f);
                AddOffset(BodyAccent, 0, 80 * .3125f);
                AddOffset(BodyAccent2, 0, 80 * .3125f);
                AddOffset(BodyAccent3, 0, 80 * .3125f);
                AddOffset(BodyAccent4, 0, 80 * .3125f);
                AddOffset(BodyAccent5, 0, 80 * .3125f);
                AddOffset(Body, 0, 80 * .3125f);
                AddOffset(Eyes, 0, 80 * .3125f);
                AddOffset(Mouth, 0, 80 * .3125f);
            }

            else if (size == 16)
            {
                AddOffset(Dick, 0, 64 * .3125f);
                AddOffset(BodyAccent, 0, 64 * .3125f);
                AddOffset(BodyAccent2, 0, 64 * .3125f);
                AddOffset(BodyAccent3, 0, 64 * .3125f);
                AddOffset(BodyAccent4, 0, 64 * .3125f);
                AddOffset(BodyAccent5, 0, 64 * .3125f);
                AddOffset(Body, 0, 64 * .3125f);
                AddOffset(Eyes, 0, 64 * .3125f);
                AddOffset(Mouth, 0, 64 * .3125f);
            }

            else if (size == 15)
            {
                AddOffset(Dick, 0, 48 * .3125f);
                AddOffset(BodyAccent, 0, 48 * .3125f);
                AddOffset(BodyAccent2, 0, 48 * .3125f);
                AddOffset(BodyAccent3, 0, 48 * .3125f);
                AddOffset(BodyAccent4, 0, 48 * .3125f);
                AddOffset(BodyAccent5, 0, 48 * .3125f);
                AddOffset(Body, 0, 48 * .3125f);
                AddOffset(Eyes, 0, 48 * .3125f);
                AddOffset(Mouth, 0, 48 * .3125f);
            }

            else if (size == 14)
            {
                AddOffset(Dick, 0, 32 * .3125f);
                AddOffset(BodyAccent, 0, 32 * .3125f);
                AddOffset(BodyAccent2, 0, 32 * .3125f);
                AddOffset(BodyAccent3, 0, 32 * .3125f);
                AddOffset(BodyAccent4, 0, 32 * .3125f);
                AddOffset(BodyAccent5, 0, 32 * .3125f);
                AddOffset(Body, 0, 32 * .3125f);
                AddOffset(Eyes, 0, 32 * .3125f);
                AddOffset(Mouth, 0, 32 * .3125f);
            }

            else if (size == 13)
            {
                AddOffset(Dick, 0, 24 * .3125f);
                AddOffset(BodyAccent, 0, 24 * .3125f);
                AddOffset(BodyAccent2, 0, 24 * .3125f);
                AddOffset(BodyAccent3, 0, 24 * .3125f);
                AddOffset(BodyAccent4, 0, 24 * .3125f);
                AddOffset(BodyAccent5, 0, 24 * .3125f);
                AddOffset(Body, 0, 24 * .3125f);
                AddOffset(Eyes, 0, 24 * .3125f);
                AddOffset(Mouth, 0, 24 * .3125f);
            }

            else if (size == 12)
            {
                AddOffset(Dick, 0, 16 * .3125f);
                AddOffset(BodyAccent, 0, 16 * .3125f);
                AddOffset(BodyAccent2, 0, 16 * .3125f);
                AddOffset(BodyAccent3, 0, 16 * .3125f);
                AddOffset(BodyAccent4, 0, 16 * .3125f);
                AddOffset(BodyAccent5, 0, 16 * .3125f);
                AddOffset(Body, 0, 16 * .3125f);
                AddOffset(Eyes, 0, 16 * .3125f);
                AddOffset(Mouth, 0, 16 * .3125f);
            }

            else if (size == 11)
            {
                AddOffset(Dick, 0, 8 * .3125f);
                AddOffset(BodyAccent, 0, 8 * .3125f);
                AddOffset(BodyAccent2, 0, 8 * .3125f);
                AddOffset(BodyAccent3, 0, 8 * .3125f);
                AddOffset(BodyAccent4, 0, 8 * .3125f);
                AddOffset(BodyAccent5, 0, 8 * .3125f);
                AddOffset(Body, 0, 8 * .3125f);
                AddOffset(Eyes, 0, 8 * .3125f);
                AddOffset(Mouth, 0, 8 * .3125f);
            }

            else
            {
                AddOffset(Dick, 0, 0 * .3125f);
                AddOffset(BodyAccent, 0, 0 * .3125f);
                AddOffset(BodyAccent2, 0, 0 * .3125f);
                AddOffset(BodyAccent3, 0, 0 * .3125f);
                AddOffset(BodyAccent4, 0, 0 * .3125f);
                AddOffset(BodyAccent5, 0, 0 * .3125f);
                AddOffset(Body, 0, 0 * .3125f);
                AddOffset(Eyes, 0, 0 * .3125f);
                AddOffset(Mouth, 0, 0 * .3125f);
            }
        }
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        if (actor.IsAttacking || actor.IsOralVoring) return SpriteDictionary.Raptor[1];

        if (actor.GetBallSize(16) > 0) return SpriteDictionary.Raptor[3];

        if (actor.HasBelly) return SpriteDictionary.Raptor[2];

        return SpriteDictionary.Raptor[0];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsOralVoring) return SpriteDictionary.Raptor[7];

        if (actor.GetBallSize(16) > 0) return SpriteDictionary.Raptor[9];

        if (actor.HasBelly) return SpriteDictionary.Raptor[8];

        return SpriteDictionary.Raptor[6];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsOralVoring) return SpriteDictionary.Raptor[48];

        return SpriteDictionary.Raptor[4];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsOralVoring) return SpriteDictionary.Raptor[49];

        return SpriteDictionary.Raptor[10];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        return SpriteDictionary.Raptor[11];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        if (!actor.Targetable) return null;

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

            if (frameListTail.frames[actor.AnimationController.frameLists[0].currentFrame] == 0) return null;

            return SpriteDictionary.Raptor[11 + frameListTail.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        if (actor.HasBelly || actor.GetBallSize(18) > 0)
        {
            if (State.Rand.Next(300) == 0)
            {
                actor.AnimationController.frameLists[0].currentlyActive = true;
            }
        }

        else if (State.Rand.Next(1200) == 0)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        return null;
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        if (!actor.Targetable) return null;

        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            if (frameListTail.frames[actor.AnimationController.frameLists[0].currentFrame] == 0) return null;

            return SpriteDictionary.Raptor[17 + frameListTail.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        return SpriteDictionary.Raptor[5];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Balls
    {
        if (actor.GetBallSize(24, 2) == 0 && Config.HideCocks == false) return SpriteDictionary.Raptor[52];

        int size = actor.GetBallSize(24, 2);

        if (size == 24 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Raptor[75];
        }

        else if (size >= 23 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Raptor[74];
        }

        else if (size == 22 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Raptor[73];
        }

        if (size > 21) size = 21;

        return SpriteDictionary.Raptor[51 + size];
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.IsCockVoring) return SpriteDictionary.Raptor[51];

        if (actor.IsErect()) return SpriteDictionary.Raptor[50];

        return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false) return null;

        int size = actor.GetStomachSize(24, 2);

        if (size == 24 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false))
        {
            return SpriteDictionary.Raptor[47];
        }

        else if (size >= 23 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
        {
            return SpriteDictionary.Raptor[46];
        }

        else if (size == 22 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
        {
            return SpriteDictionary.Raptor[45];
        }

        if (size > 21) size = 21;

        return SpriteDictionary.Raptor[23 + size];
    }
}
