using System.Collections.Generic;
using UnityEngine;

class Salamander : BlankSlate
{
    RaceFrameList frameListSalamanderFlame = new RaceFrameList(new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new float[10] { .1f, .1f, .1f, .1f, .1f, .1f, .1f, .1f, .1f, .1f });

    readonly Sprite[] Sprites = SpriteDictionary.Salamanders;

    public Salamander()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        EyeTypes = 6;
        SpecialAccessoryCount = 12; // Backside spikes/patterns
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SalamanderSkin); // Backside spikes/pattern color
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SalamanderSkin);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SalamanderSkin);
        GentleAnimation = true;
        WeightGainDisabled = true;

        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(4, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(7, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, s.Unit.AccessoryColor)); // Backside spikes/patterns
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, WhiteColored); // flame
        BodyAccent2 = new SpriteExtraInfo(3, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, s.Unit.SkinColor)); // Belly cover up
        BodyAccent3 = new SpriteExtraInfo(4, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, s.Unit.SkinColor)); // right back leg
        BodyAccent4 = new SpriteExtraInfo(1, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, s.Unit.SkinColor)); // left front leg
        BodyAccent5 = new SpriteExtraInfo(6, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, s.Unit.SkinColor)); // right front leg
        Mouth = new SpriteExtraInfo(5, MouthSprite, WhiteColored);
        Eyes = new SpriteExtraInfo(5, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, s.Unit.EyeColor));
        Belly = new SpriteExtraInfo(5, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, s.Unit.SkinColor));

        clothingColors = 0;
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(State.Rand.Next(0, 2), 0, true) };
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        return Sprites[32];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
            return Sprites[1];
        return Sprites[0];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites[10 + actor.Unit.SpecialAccessoryType]; // Backside spikes/patterns

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // flame animated
    {
        if (actor.AnimationController.frameLists[0].currentTime >= frameListSalamanderFlame.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
        {
            actor.AnimationController.frameLists[0].currentFrame++;
            actor.AnimationController.frameLists[0].currentTime = 0f;

            if (actor.AnimationController.frameLists[0].currentFrame >= frameListSalamanderFlame.frames.Length)
            {
                actor.AnimationController.frameLists[0].currentFrame = 0;
                actor.AnimationController.frameLists[0].currentTime = 0f;
            }
        }
        return Sprites[22 + frameListSalamanderFlame.frames[actor.AnimationController.frameLists[0].currentFrame]];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (actor.HasBelly == false)
            return Sprites[33];
        return null;
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // right back leg
    {
        if (actor.GetStomachSize(16) < 5)
            return Sprites[34];
        else if (actor.GetStomachSize(16) >= 5 && actor.GetStomachSize(16) < 9)
            return Sprites[35];
        else if (actor.GetStomachSize(16) >= 9 && actor.GetStomachSize(16) < 13)
            return Sprites[36];
        else if (actor.GetStomachSize(16) >= 13 && actor.GetStomachSize(16) < 16)
            return Sprites[37];
        else if (actor.GetStomachSize(16) == 16)
            return Sprites[38];
        return Sprites[34];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // left front leg
    {
        if (actor.GetStomachSize(16) < 5)
            return Sprites[39];
        else if (actor.GetStomachSize(16) >= 5 && actor.GetStomachSize(16) < 8)
            return Sprites[40];
        else if (actor.GetStomachSize(16) >= 8 && actor.GetStomachSize(16) < 10)
            return Sprites[41];
        else if (actor.GetStomachSize(16) >= 10 && actor.GetStomachSize(16) < 13)
            return Sprites[42];
        else if (actor.GetStomachSize(16) >= 13 && actor.GetStomachSize(16) < 15)
            return Sprites[43];
        else if (actor.GetStomachSize(16) >= 15)
            return Sprites[44];
        return Sprites[39];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // right front leg
    {
        if (actor.GetStomachSize(16) < 2)
            return Sprites[45];
        else if (actor.GetStomachSize(16) >= 2 && actor.GetStomachSize(16) < 5)
            return Sprites[46];
        else if (actor.GetStomachSize(16) >= 5 && actor.GetStomachSize(16) < 7)
            return Sprites[47];
        else if (actor.GetStomachSize(16) >= 7 && actor.GetStomachSize(16) < 9)
            return Sprites[48];
        else if (actor.GetStomachSize(16) >= 9 && actor.GetStomachSize(16) < 11)
            return Sprites[49];
        else if (actor.GetStomachSize(16) >= 11 && actor.GetStomachSize(16) < 13)
            return Sprites[50];
        else if (actor.GetStomachSize(16) >= 13 && actor.GetStomachSize(16) < 15)
            return Sprites[51];
        else if (actor.GetStomachSize(16) >= 15)
            return Sprites[52];
        return Sprites[45];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking)
            return Sprites[2];
        else if (actor.IsEating)
            return Sprites[3];
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[4 + actor.Unit.EyeType];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            return Sprites[72];
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
        {
            if (actor.GetStomachSize(16, .8f) == 16)
                return Sprites[71];
            else if (actor.GetStomachSize(16, .9f) == 16)
                return Sprites[70];
        }
        return Sprites[53 + actor.GetStomachSize(16)];
    }

}
