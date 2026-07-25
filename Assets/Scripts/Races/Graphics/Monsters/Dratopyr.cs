using System.Collections.Generic;
using UnityEngine;

class Dratopyr : BlankSlate
{
    RaceFrameList frameListTail = new RaceFrameList(new int[8] { 2, 1, 0, 1, 2, 3, 4, 3 }, new float[8] { 0.55f, 0.55f, 0.75f, 0.55f, 0.55f, 0.55f, 0.75f, 0.55f });
    RaceFrameList frameListEyes = new RaceFrameList(new int[3] { 1, 2, 1 }, new float[3] { 0.3f, 0.3f, 0.3f });
    RaceFrameList frameListShake = new RaceFrameList(new int[5] { 0, 1, 0, 2, 0 }, new float[5] { 0.6f, 0.6f, 0.6f, 0.6f, 0.6f });
    RaceFrameList frameListWings = new RaceFrameList(new int[4] { 0, 1, 2, 1 }, new float[4] { 0.3f, 0.3f, 0.3f, 0.3f });
    RaceFrameList frameListEars = new RaceFrameList(new int[18] { 0, 1, 2, 1, 0, 1, 0, 1, 2, 1, 2, 1, 0, 1, 2, 1, 2, 1 }, new float[18] { 2.2f, 0.3f, 0.5f, 0.2f, 0.8f, 0.3f, 1.5f, 0.9f, 1.3f, 0.6f, 0.4f, 0.3f, 2.2f, 1.5f, 0.6f, 0.3f, 0.8f, 0.2f });

    internal override int BreastSizes => 2;
    internal override int DickSizes => 2;

    public Dratopyr()
    {
        SkinColors = ColorMap.DratopyrMainColorCount; // Majority of the unit
        AccessoryColors = ColorMap.DratopyrWingColorCount; // Wing Webbing
        ExtraColors1 = ColorMap.DratopyrFleshColorCount; // Flesh
        ExtraColors2 = ColorMap.DratopyrEyeColorCount; // Eye "Whites"
        EyeColors = ColorMap.EyeColorCount; // Eyes
        GentleAnimation = true;

        WeightGainDisabled = true;

        CanBeGender = new List<Gender>() { Gender.Male, Gender.Female };

        Body = new SpriteExtraInfo(9, BodySprite, (s) => ColorMap.GetDratopyrMainColor(s.Unit.SkinColor)); // Upper Body & Arms
        BodyAccent = new SpriteExtraInfo(0, BodyAccentSprite, (s) => ColorMap.GetDratopyrMainColor(s.Unit.SkinColor)); // Tail
        BodyAccent2 = new SpriteExtraInfo(3, BodyAccentSprite2, (s) => ColorMap.GetDratopyrMainColor(s.Unit.SkinColor)); // Legs
        BodyAccent3 = new SpriteExtraInfo(1, BodyAccentSprite3, (s) => ColorMap.GetDratopyrWingColor(s.Unit.ExtraColor1)); // Wing Webbing
        BodyAccent4 = new SpriteExtraInfo(2, BodyAccentSprite4, (s) => ColorMap.GetDratopyrMainColor(s.Unit.SkinColor)); // Wing Bones
        BodyAccent5 = new SpriteExtraInfo(7, BodyAccentSprite5, (s) => ColorMap.GetDratopyrEyeColor(s.Unit.ExtraColor2)); // Eye Whites
        Eyes = new SpriteExtraInfo(8, EyesSprite, (s) => ColorMap.GetEyeColor(s.Unit.EyeColor)); // Eye Iris
        BodyAccent6 = new SpriteExtraInfo(9, BodyAccentSprite6, (s) => ColorMap.GetDratopyrMainColor(s.Unit.SkinColor)); // Eyelids
        Head = new SpriteExtraInfo(10, HeadSprite, (s) => ColorMap.GetDratopyrMainColor(s.Unit.SkinColor)); // Head
        Hair = new SpriteExtraInfo(11, HairSprite, (s) => ColorMap.GetDratopyrMainColor(s.Unit.SkinColor)); // Ears
        Mouth = new SpriteExtraInfo(12, MouthSprite, (s) => ColorMap.GetDratopyrFleshColor(s.Unit.ExtraColor1)); // Inner Mouth
        BodyAccent7 = new SpriteExtraInfo(13, BodyAccentSprite7, WhiteColored); // Teeth
        Balls = new SpriteExtraInfo(4, BallsSprite, (s) => ColorMap.GetDratopyrMainColor(s.Unit.SkinColor)); // Balls
        BodyAccent8 = new SpriteExtraInfo(5, BodyAccentSprite8, (s) => ColorMap.GetDratopyrMainColor(s.Unit.SkinColor)); // Sheath
        Dick = new SpriteExtraInfo(10, DickSprite, (s) => ColorMap.GetDratopyrFleshColor(s.Unit.ExtraColor1)); // Dick
        Belly = new SpriteExtraInfo(7, null, (s) => ColorMap.GetDratopyrMainColor(s.Unit.SkinColor)); // Belly
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(0, 0, false),  // Tail controller. Index 0.
            new AnimationController.FrameList(0, 0, false), // Eye controller. Index 1.
            new AnimationController.FrameList(0, 0, false), // Shimmyshake controller. Index 2.
            new AnimationController.FrameList(State.Rand.Next(0, 3), 0, true), // Wing controller. Index 3.
            new AnimationController.FrameList(State.Rand.Next(0, 17), 0, true)}; // Ear controller. Index 4.
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Balls, 0, -80 * .625f);
        AddOffset(Belly, 0, -80 * .625f);
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body and arms.
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        if (actor.IsAttacking || actor.IsCockVoring || actor.IsUnbirthing)
            return SpriteDictionary.Dratopyr[22];
        return SpriteDictionary.Dratopyr[21];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Tail sprites.
    {
        if (!actor.Targetable) return SpriteDictionary.Dratopyr[28];

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

            return SpriteDictionary.Dratopyr[26 + frameListTail.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        if (State.Rand.Next(250) == 0)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        return SpriteDictionary.Dratopyr[28];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Legs
    {
        if (!actor.Targetable) return SpriteDictionary.Dratopyr[17];

        if (actor.IsAttacking || actor.IsOralVoring || actor.IsCockVoring || actor.IsUnbirthing)
        {
            actor.AnimationController.frameLists[2].currentlyActive = false;
            actor.AnimationController.frameLists[2].currentFrame = 0;
            actor.AnimationController.frameLists[2].currentTime = 0f;

            return SpriteDictionary.Dratopyr[20];
        }

        else if (actor.AnimationController.frameLists[2].currentlyActive)
        {
            if (actor.AnimationController.frameLists[2].currentTime >= frameListShake.times[actor.AnimationController.frameLists[2].currentFrame])
            {
                actor.AnimationController.frameLists[2].currentFrame++;
                actor.AnimationController.frameLists[2].currentTime = 0f;

                if (actor.AnimationController.frameLists[2].currentFrame >= frameListShake.frames.Length)
                {
                    actor.AnimationController.frameLists[2].currentlyActive = false;
                    actor.AnimationController.frameLists[2].currentFrame = 0;
                    actor.AnimationController.frameLists[2].currentTime = 0f;
                }
            }

            return SpriteDictionary.Dratopyr[17 + frameListShake.frames[actor.AnimationController.frameLists[2].currentFrame]];
        }

        else
        {
            if (State.Rand.Next(350) == 0)
            {
                actor.AnimationController.frameLists[2].currentlyActive = true;
            }
        }

        return SpriteDictionary.Dratopyr[17];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Wing membranes
    {
        if (!actor.Targetable) return SpriteDictionary.Dratopyr[8];

        if (actor.AnimationController.frameLists[3].currentTime >= frameListWings.times[actor.AnimationController.frameLists[3].currentFrame])
        {
            actor.AnimationController.frameLists[3].currentFrame++;
            actor.AnimationController.frameLists[3].currentTime = 0f;

            if (actor.AnimationController.frameLists[3].currentFrame >= frameListWings.frames.Length)
            {
                actor.AnimationController.frameLists[3].currentFrame = 0;
                actor.AnimationController.frameLists[3].currentTime = 0f;
            }
        }

        return SpriteDictionary.Dratopyr[8 + frameListWings.frames[actor.AnimationController.frameLists[3].currentFrame]];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Wing bones
    {
        if (!actor.Targetable) return SpriteDictionary.Dratopyr[11];
        return SpriteDictionary.Dratopyr[11 + frameListWings.frames[actor.AnimationController.frameLists[3].currentFrame]];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Eyewhites
    {
        return SpriteDictionary.Dratopyr[25];
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => SpriteDictionary.Dratopyr[2]; // Eyes

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Eyelids
    {
        if (!actor.Targetable) return SpriteDictionary.Dratopyr[4];

        if (actor.AnimationController.frameLists[1].currentlyActive)
        {
            if (actor.AnimationController.frameLists[1].currentTime >= frameListEyes.times[actor.AnimationController.frameLists[1].currentFrame])
            {
                actor.AnimationController.frameLists[1].currentFrame++;
                actor.AnimationController.frameLists[1].currentTime = 0f;

                if (actor.AnimationController.frameLists[1].currentFrame >= frameListEyes.frames.Length)
                {
                    actor.AnimationController.frameLists[1].currentlyActive = false;
                    actor.AnimationController.frameLists[1].currentFrame = 0;
                    actor.AnimationController.frameLists[1].currentTime = 0f;
                }
            }

            if (frameListEyes.frames[actor.AnimationController.frameLists[1].currentFrame] == 0) return null;

            return SpriteDictionary.Dratopyr[2 + frameListEyes.frames[actor.AnimationController.frameLists[1].currentFrame]];
        }

        if (State.Rand.Next(400) == 0)
        {
            actor.AnimationController.frameLists[1].currentlyActive = true;
        }

        return null;
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // The head.
    {
        if (actor.IsOralVoring) return SpriteDictionary.Dratopyr[5];

        if (actor.GetBallSize(22) > 0) return SpriteDictionary.Dratopyr[1];

        return SpriteDictionary.Dratopyr[0];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return SpriteDictionary.Dratopyr[6];
        return null;
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Teeth
    {
        if (actor.IsOralVoring)
            return SpriteDictionary.Dratopyr[7];
        return null;
    }

    protected override Sprite HairSprite(Actor_Unit actor) // Ears
    {
        if (actor.IsOralVoring) return null;

        if (!actor.Targetable) return SpriteDictionary.Dratopyr[14];

        if (actor.AnimationController.frameLists[4].currentTime >= frameListEars.times[actor.AnimationController.frameLists[4].currentFrame])
        {
            actor.AnimationController.frameLists[4].currentFrame++;
            actor.AnimationController.frameLists[4].currentTime = 0f;

            if (actor.AnimationController.frameLists[4].currentFrame >= frameListEars.frames.Length)
            {
                actor.AnimationController.frameLists[4].currentFrame = 0;
                actor.AnimationController.frameLists[4].currentTime = 0f;
            }
        }

        return SpriteDictionary.Dratopyr[14 + frameListEars.frames[actor.AnimationController.frameLists[4].currentFrame]];
    }

    protected override Sprite BallsSprite(Actor_Unit actor) // Balls
    {
        if (actor.Unit.DickSize == -1) return null;
        if (Config.HideCocks) return null;

        int shake = frameListShake.frames[actor.AnimationController.frameLists[2].currentFrame];
        int ballSize = actor.GetBallSize(21, 0.6f);

        if (!actor.Targetable) shake = 0;

        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false)
        {
            return SpriteDictionary.Dratopyr[99 + shake];
        }

        else if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false)
        {

            if (ballSize > 19) return SpriteDictionary.Dratopyr[96 + shake];
            if (ballSize > 17) return SpriteDictionary.Dratopyr[93 + shake];
            if (ballSize > 15) return SpriteDictionary.Dratopyr[90 + shake];
            if (ballSize > 13) return SpriteDictionary.Dratopyr[87 + shake];
        }

        if (ballSize > 13) ballSize = 13;

        return SpriteDictionary.Dratopyr[45 + (ballSize * 3) + shake];
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // Sheath
    {
        if (actor.Unit.DickSize >= 0)
        {
            if (actor.GetStomachSize(23, 0.7f) > 14)
            {
                if (!actor.Targetable) return SpriteDictionary.Dratopyr[34];

                return SpriteDictionary.Dratopyr[34 + frameListShake.frames[actor.AnimationController.frameLists[2].currentFrame]];
            }
            else
            {
                if (!actor.Targetable) return SpriteDictionary.Dratopyr[31];

                return SpriteDictionary.Dratopyr[31 + frameListShake.frames[actor.AnimationController.frameLists[2].currentFrame]];
            }
        }
        return null;
    }

    protected override Sprite DickSprite(Actor_Unit actor) // Dick + CV
    {
        if (actor.Unit.DickSize >= 0)
        {

            if (actor.GetStomachSize(23, 0.7f) > 1)
            {
                Dick.layer = 6;

                if (actor.IsCockVoring) return SpriteDictionary.Dratopyr[44];

                if (actor.IsErect())
                {
                    if (!actor.Targetable) return SpriteDictionary.Dratopyr[41];

                    if (actor.AnimationController.frameLists[2].currentlyActive)
                    {
                        return SpriteDictionary.Dratopyr[41 + frameListShake.frames[actor.AnimationController.frameLists[2].currentFrame]];
                    }

                    return SpriteDictionary.Dratopyr[41];
                }
            }
            else
            {
                Dick.layer = 10;

                if (actor.IsCockVoring) return SpriteDictionary.Dratopyr[40];

                if (actor.IsErect())
                {
                    if (!actor.Targetable) return SpriteDictionary.Dratopyr[37];

                    if (actor.AnimationController.frameLists[2].currentlyActive)
                    {
                        return SpriteDictionary.Dratopyr[37 + frameListShake.frames[actor.AnimationController.frameLists[2].currentFrame]];
                    }

                    return SpriteDictionary.Dratopyr[37];
                }
            }
        }

        if (actor.Unit.DickSize == -1)
        {
            Dick.layer = 6;
            if (actor.IsUnbirthing) return SpriteDictionary.Dratopyr[172];
            else return SpriteDictionary.Dratopyr[171];
        }

        return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        int bellySize = actor.GetStomachSize(23, 0.7f);
        int shake = frameListShake.frames[actor.AnimationController.frameLists[2].currentFrame];

        if (!actor.Targetable) shake = 0;

        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false)
        {
            return SpriteDictionary.Dratopyr[168 + shake];
        }

        else if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false)
        {

            if (bellySize > 22) return SpriteDictionary.Dratopyr[165 + shake];
            if (bellySize > 21) return SpriteDictionary.Dratopyr[162 + shake];
            if (bellySize > 20) return SpriteDictionary.Dratopyr[159 + shake];
            if (bellySize > 19) return SpriteDictionary.Dratopyr[156 + shake];
        }

        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.womb) ?? false)
        {
            return SpriteDictionary.Dratopyr[168 + shake];
        }

        else if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.womb) ?? false)
        {

            if (bellySize > 22) return SpriteDictionary.Dratopyr[165 + shake];
            if (bellySize > 21) return SpriteDictionary.Dratopyr[162 + shake];
            if (bellySize > 20) return SpriteDictionary.Dratopyr[159 + shake];
            if (bellySize > 19) return SpriteDictionary.Dratopyr[156 + shake];
        }

        if (bellySize > 18) bellySize = 18;

        return SpriteDictionary.Dratopyr[102 + (bellySize * 3) + shake];
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        int rand = State.Rand.Next(100);

        if (rand < 92) unit.ExtraColor2 = 0;
        else if (rand < 97) unit.ExtraColor2 = 1;
        else unit.ExtraColor2 = 2;
    }
}
