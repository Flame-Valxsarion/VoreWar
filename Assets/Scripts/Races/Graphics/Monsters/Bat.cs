using System.Collections.Generic;
using UnityEngine;

class Bat : BlankSlate
{
    RaceFrameList frameListWings = new RaceFrameList(new int[2] { 0, 1 }, new float[2] { .2f, .2f });

    public Bat()
    {
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Bat);
        CanBeGender = new List<Gender>() { Gender.Female, Gender.Male };
        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Bat, s.Unit.SkinColor)); // Wings
        Body = new SpriteExtraInfo(0, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Bat, s.Unit.SkinColor)); // Body
        Head = new SpriteExtraInfo(2, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Bat, s.Unit.SkinColor)); // Head
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Bat, s.Unit.SkinColor)); // Privates
        Belly = new SpriteExtraInfo(5, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Bat, s.Unit.SkinColor)); // Belly
        Balls = new SpriteExtraInfo(3, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Bat, s.Unit.SkinColor)); // Balls
        Dick = new SpriteExtraInfo(4, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Bat, s.Unit.SkinColor)); // Dick
    }

    internal override int DickSizes => 1;
    internal override int BreastSizes => 1;

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(State.Rand.Next(0, 2), 0, true)};  // Wing controller. Index 0.
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Wings
    {
        if (actor.AnimationController.frameLists[0].currentTime >= frameListWings.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
        {
            actor.AnimationController.frameLists[0].currentFrame++;
            actor.AnimationController.frameLists[0].currentTime = 0f;

            if (actor.AnimationController.frameLists[0].currentFrame >= frameListWings.frames.Length)
            {
                actor.AnimationController.frameLists[0].currentFrame = 0;
                actor.AnimationController.frameLists[0].currentTime = 0f;
            }
        }

        return SpriteDictionary.Bat[5 + frameListWings.frames[actor.AnimationController.frameLists[0].currentFrame]];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Privates
    {
        if (!actor.Unit.HasDick)
        {
            if (actor.IsAnalVoring) return SpriteDictionary.Bat[7];

            if (actor.IsUnbirthing) return SpriteDictionary.Bat[9];

            return SpriteDictionary.Bat[8];
        }
        return null;
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        if (actor.IsUnbirthing || actor.IsAnalVoring) return SpriteDictionary.Bat[1];

        return SpriteDictionary.Bat[0];
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (actor.IsOralVoring || actor.IsAttacking) return SpriteDictionary.Bat[4];

        if (actor.HasBelly) return SpriteDictionary.Bat[3];

        return SpriteDictionary.Bat[2];
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick)
        {
            if (actor.Unit.Predator == false)
                return SpriteDictionary.Bat[31];

            if (actor.PredatorComponent.BallsFullness <= 0) return SpriteDictionary.Bat[28];

            int sprite = actor.GetBallSize(21);

            if (sprite >= 20 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false))
            {
                return SpriteDictionary.Bat[49];
            }
            else if (sprite >= 18 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
            {
                return SpriteDictionary.Bat[48];
            }
            else if (sprite >= 16 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
            {
                return SpriteDictionary.Bat[47];
            }
            if (sprite >= 15)
                return SpriteDictionary.Bat[46];
            return SpriteDictionary.Bat[31 + sprite];
        }

        return null;
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick)
        {
            if (actor.IsCockVoring) return SpriteDictionary.Bat[29];

            if (actor.IsErect()) return SpriteDictionary.Bat[30];

            return SpriteDictionary.Bat[28];
        }

        return null;
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.Unit.Predator == false)
            return null;
        if (actor.HasBelly)
        {
            belly.SetActive(true);
            int sprite = actor.GetStomachSize(22);

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) || actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.womb))
            {
                if (sprite >= 21)
                {
                    return SpriteDictionary.Bat[27];
                }
            }

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) || actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.womb))
            {
                if (sprite >= 19)
                {
                    return SpriteDictionary.Bat[26];
                }
                else if (sprite >= 17)
                {
                    return SpriteDictionary.Bat[25];
                }
                else if (sprite >= 15)
                {
                    return SpriteDictionary.Bat[24];
                }
            }

            if (sprite >= 15)
                return SpriteDictionary.Bat[23];
            return SpriteDictionary.Bat[9 + sprite];
        }
        return null;
    }

    internal override void RandomCustom(Unit unit)
    {
        unit.SkinColor = State.Rand.Next(0, SkinColors);
    }
}
