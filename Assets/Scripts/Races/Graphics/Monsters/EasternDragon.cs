using System.Collections.Generic;
using UnityEngine;

class EasternDragon : BlankSlate
{
    RaceFrameList frameListEyes = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .2f, .2f, .3f, .2f, .2f });
    RaceFrameList frameListTongue = new RaceFrameList(new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 }, new float[8] { 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f });

    public EasternDragon()
    {
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EasternDragon); // Main body, legs, head, tail upper
        GentleAnimation = true;
        WeightGainDisabled = true;
        CanBeGender = new List<Gender>() { Gender.Male, Gender.Female };

        Body = new SpriteExtraInfo(5, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, (s.Unit.SkinColor))); // Body
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, (s.Unit.SkinColor))); // Head
        BodyAccent = new SpriteExtraInfo(0, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, (s.Unit.SkinColor))); ; // Tail
        BodyAccent2 = new SpriteExtraInfo(2, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, (s.Unit.SkinColor))); ; // Sheath/SnatchBase
        BodyAccent3 = new SpriteExtraInfo(3, BodyAccentSprite3, WhiteColored); // Snatch
        BodyAccent4 = new SpriteExtraInfo(4, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, (s.Unit.SkinColor))); ; // Womb
        Mouth = new SpriteExtraInfo(8, MouthSprite, WhiteColored); // Inner Mouth
        BodySize = new SpriteExtraInfo(7, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, (s.Unit.SkinColor))); ; // Horns
        Dick = new SpriteExtraInfo(3, DickSprite, WhiteColored); // Dick, CV, UB
        Belly = new SpriteExtraInfo(8, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, (s.Unit.SkinColor))); ; // Belly
        Balls = new SpriteExtraInfo(1, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EasternDragon, (s.Unit.SkinColor))); ; // Balls
        SecondaryAccessory = new SpriteExtraInfo(9, SecondaryAccessorySprite, WhiteColored); // Tongue

        BodySizes = 4; // Horn types
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Balls, 128 * .3125f, 0);
        AddOffset(Dick, 128 * .3125f, 0);
        AddOffset(BodyAccent2, 128 * .3125f, 0);
        AddOffset(BodyAccent3, 128 * .3125f, 0);
        AddOffset(BodyAccent4, 128 * .3125f, 0);
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(0, 0, false),  // Eye controller. Index 0.
            new AnimationController.FrameList(0, 0, false)}; // Tongue controller. Index 1.
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        float scaleMod = actor.Unit.GetScale()*1.6f;
        actor.UnitSprite.GraphicsFolder.transform.localScale = new Vector3(scaleMod, scaleMod, 1); // Largeness activated!
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Tail
    {
        if (!Config.HideCocks && (actor.PredatorComponent?.BallsFullness > 0 || actor.IsErect()))
        {
            AddOffset(BodyAccent, 128 * .3125f, 0);
            return SpriteDictionary.EasternDragon[38];
        }
        if (actor.PredatorComponent?.WombFullness > 0)
        {
            AddOffset(BodyAccent, 128 * .3125f, 0);
            return SpriteDictionary.EasternDragon[38];
        }
        if (actor.IsCockVoring || actor.IsUnbirthing)
        {
            AddOffset(BodyAccent, 128 * .3125f, 0);
            return SpriteDictionary.EasternDragon[38];
        }
        AddOffset(BodyAccent, 0, 0);
        return SpriteDictionary.EasternDragon[18];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Sheath/SnatchBase
    {
        if (actor.Unit.GetGender() == Gender.Male)
        {
            if (actor.Unit.DickSize < 0) return null;
            if (Config.HideCocks) return null;
            if (actor.PredatorComponent?.BallsFullness > 0 || actor.IsCockVoring || actor.IsErect())
            {
                return SpriteDictionary.EasternDragon[39];
            }
        }
        else
        {
            if (actor.PredatorComponent?.WombFullness > 0 || actor.IsUnbirthing)
            {
                return SpriteDictionary.EasternDragon[67];
            }
        }
        return null;
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Snatch
    {
        if (actor.IsUnbirthing) return SpriteDictionary.EasternDragon[68];
        return null;
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Womb
    {
        if (actor.GetWombSize(17) > 0)
        {
            int sprite = actor.GetWombSize(17, 0.8f);

            if (sprite == 17 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.womb) ?? false))
            {
                return SpriteDictionary.EasternDragon[86];
            }

            if (sprite == 17 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.womb) ?? false))
            {
                return SpriteDictionary.EasternDragon[85];
            }

            if (sprite == 16 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.womb) ?? false))
            {
                return SpriteDictionary.EasternDragon[84];
            }

            return SpriteDictionary.EasternDragon[68 + sprite];
        }
        return null;
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        return SpriteDictionary.EasternDragon[0];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return SpriteDictionary.EasternDragon[5];
        return null;
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (!actor.Targetable) return SpriteDictionary.EasternDragon[3];

        if (actor.IsOralVoring)
        {
            actor.AnimationController.frameLists[0].currentlyActive = false;
            actor.AnimationController.frameLists[0].currentFrame = 0;
            actor.AnimationController.frameLists[0].currentTime = 0f;
            return SpriteDictionary.EasternDragon[4];
        }

        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            if (actor.AnimationController.frameLists[0].currentTime >= frameListEyes.times[actor.AnimationController.frameLists[0].currentFrame])
            {
                actor.AnimationController.frameLists[0].currentFrame++;
                actor.AnimationController.frameLists[0].currentTime = 0f;

                if (actor.AnimationController.frameLists[0].currentFrame >= frameListEyes.frames.Length)
                {
                    actor.AnimationController.frameLists[0].currentlyActive = false;
                    actor.AnimationController.frameLists[0].currentFrame = 0;
                    actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            return SpriteDictionary.EasternDragon[1 + frameListEyes.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        if (State.Rand.Next(750) == 0)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        return SpriteDictionary.EasternDragon[1];
    }

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) // Tongue
    {
        if (!actor.Targetable) return null;

        if (actor.IsAttacking || actor.IsOralVoring)
        {
            actor.AnimationController.frameLists[1].currentlyActive = false;
            actor.AnimationController.frameLists[1].currentFrame = 0;
            actor.AnimationController.frameLists[1].currentTime = 0f;
            return null;
        }

        if (actor.AnimationController.frameLists[1].currentlyActive)
        {
            if (actor.AnimationController.frameLists[1].currentTime >= frameListTongue.times[actor.AnimationController.frameLists[1].currentFrame])
            {
                actor.AnimationController.frameLists[1].currentFrame++;
                actor.AnimationController.frameLists[1].currentTime = 0f;

                if (actor.AnimationController.frameLists[1].currentFrame >= frameListTongue.frames.Length)
                {
                    actor.AnimationController.frameLists[1].currentlyActive = false;
                    actor.AnimationController.frameLists[1].currentFrame = 0;
                    actor.AnimationController.frameLists[1].currentTime = 0f;
                }
            }

            return SpriteDictionary.EasternDragon[10 + frameListTongue.frames[actor.AnimationController.frameLists[1].currentFrame]];
        }

        if (actor.PredatorComponent?.VisibleFullness > 0 && State.Rand.Next(1200) == 0)
        {
            actor.AnimationController.frameLists[1].currentlyActive = true;
        }

        return null;
    }

    protected override Sprite BodySizeSprite(Actor_Unit actor) => SpriteDictionary.EasternDragon[6 + actor.Unit.BodySize]; // One of four horn options.

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.PredatorComponent?.ExclusiveStomachFullness > 0)
        {
            belly.SetActive(true);
            int sprite = actor.GetExclusiveStomachSize(16, 0.8f);

            if (sprite == 16 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false))
            {
                AddOffset(Belly, 0, 0 * .625f);
                return SpriteDictionary.EasternDragon[37];
            }
            else if (sprite == 16 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
            {
                AddOffset(Belly, 0, 0 * .625f);
                return SpriteDictionary.EasternDragon[36];
            }
            else if (sprite == 15 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
            {
                AddOffset(Belly, 0, 0 * .625f);
                return SpriteDictionary.EasternDragon[35];
            }
            if (sprite >= 14)
                return SpriteDictionary.EasternDragon[34];
            return SpriteDictionary.EasternDragon[20 + sprite];
        }
        else
        {
            if (actor.GetExclusiveStomachSize(1) == 0)
                return SpriteDictionary.EasternDragon[19];
            return SpriteDictionary.EasternDragon[20 + actor.GetExclusiveStomachSize(14, 0.8f)];
        }
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.DickSize < 0) return null;
        if (Config.HideCocks) return null;

        if (actor.PredatorComponent?.BallsFullness == 0)
            return null;
        if (actor.PredatorComponent?.BallsFullness > 0 || actor.IsCockVoring)
        {
            int sprite = actor.GetBallSize(24, 0.8f);

            if (sprite == 24 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false))
            {
                return SpriteDictionary.EasternDragon[66];
            }
            else if (sprite == 24 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
            {
                return SpriteDictionary.EasternDragon[65];
            }
            else if (sprite == 23 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
            {
                return SpriteDictionary.EasternDragon[64];
            }
            if (sprite >= 22)
                return SpriteDictionary.EasternDragon[63];
            if (sprite <= 1)
                return SpriteDictionary.EasternDragon[42];
            return SpriteDictionary.EasternDragon[41 + sprite];
        }
        return null;
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.DickSize < 0) return null;
        if (Config.HideCocks) return null;

        if (actor.IsCockVoring) return SpriteDictionary.EasternDragon[41];
        if (actor.IsErect()) return SpriteDictionary.EasternDragon[40];

        return null;
    }
}

