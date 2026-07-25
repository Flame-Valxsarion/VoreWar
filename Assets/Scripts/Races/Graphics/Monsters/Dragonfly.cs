using System.Collections.Generic;
using UnityEngine;

class Dragonfly : BlankSlate
{
    RaceFrameList frameListWings = new RaceFrameList(new int[3] { 0, 1, 2 }, new float[3] { .02f, .02f, .02f });

    public Dragonfly()
    {
        CanBeGender = new List<Gender>() { Gender.None };

        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Dragonfly);
        GentleAnimation = true;
        BodyAccent = new SpriteExtraInfo(2, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragonfly, s.Unit.SkinColor)); // Wings
        Body = new SpriteExtraInfo(1, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragonfly, s.Unit.SkinColor)); // Body
        Head = new SpriteExtraInfo(3, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragonfly, s.Unit.SkinColor)); // Head
        Belly = new SpriteExtraInfo(0, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragonfly, s.Unit.SkinColor)); // Belly
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(State.Rand.Next(0, 3), 0, true)};  // Wing controller. Index 0.
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // Belly
    {
        if (actor.Unit.Predator == false)
            return null;
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
        {
            if (actor.PredatorComponent.VisibleFullness > 3)
                return SpriteDictionary.Dragonfly[27];
        }

        if (!actor.HasBelly)
            return SpriteDictionary.Dragonfly[6];
        return SpriteDictionary.Dragonfly[7 + actor.GetStomachSize(19)];
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {

        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        return SpriteDictionary.Dragonfly[2];

    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (actor.IsOralVoring || actor.IsAttacking) return SpriteDictionary.Dragonfly[1];

        return SpriteDictionary.Dragonfly[0];
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

        return SpriteDictionary.Dragonfly[3 + frameListWings.frames[actor.AnimationController.frameLists[0].currentFrame]];
    }
}
