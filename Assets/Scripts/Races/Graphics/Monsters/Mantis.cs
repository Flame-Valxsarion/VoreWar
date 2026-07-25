using System.Collections.Generic;
using UnityEngine;

class Mantis : BlankSlate
{
    RaceFrameList frameListScythesDefault = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .2f, .5f, 1.5f, .5f, .2f });
    RaceFrameList frameListScythesEating = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .2f, .5f, 1.5f, .5f, .2f });

    enum Position
    {
        Default,
        Eating
    }
    Position position;
    readonly Sprite[] Sprites = SpriteDictionary.Mantis;

    public Mantis()
    {
        CanBeGender = new List<Gender>() { Gender.Female, Gender.Male };
        BodySizes = 5;
        EyeTypes = 6;
        SpecialAccessoryCount = 9; // antennae
        BodyAccentTypes1 = 6; // wings
        BodyAccentTypes2 = 6; // spine
        clothingColors = 0;
        GentleAnimation = true;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MantisSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MantisSkin);

        Body = new SpriteExtraInfo(3, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(8, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(9, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, s.Unit.SkinColor)); // antennae
        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, s.Unit.SkinColor)); // wings
        BodyAccent2 = new SpriteExtraInfo(5, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, s.Unit.SkinColor)); // spine (only default position)
        BodyAccent3 = new SpriteExtraInfo(10, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, s.Unit.SkinColor)); // scythes (default position)
        BodyAccent4 = new SpriteExtraInfo(10, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, s.Unit.SkinColor)); // scythes (eating position)
        BodyAccent5 = new SpriteExtraInfo(10, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, s.Unit.SkinColor)); // scythes (attacking)
        BodyAccent6 = new SpriteExtraInfo(5, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, s.Unit.SkinColor)); // thorax
        BodyAccent7 = new SpriteExtraInfo(2, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, s.Unit.SkinColor)); // left legs
        BodyAccent8 = new SpriteExtraInfo(7, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, s.Unit.SkinColor)); // right legs
        Mouth = new SpriteExtraInfo(9, MouthSprite, WhiteColored);
        Eyes = new SpriteExtraInfo(8, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, s.Unit.EyeColor));
        SecondaryEyes = new SpriteExtraInfo(8, EyesSecondarySprite, WhiteColored);
        Belly = new SpriteExtraInfo(4, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MantisSkin, s.Unit.SkinColor));

    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.HasBelly)
            position = Position.Eating;
        else
            position = Position.Default;
        base.RunFirst(actor);
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(0, 0, false),  // Scythes in Default position controller. Index 0.
            new AnimationController.FrameList(0, 0, false)}; // Scythes in Eating position controller. Index 1.
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.EyeColor = unit.SkinColor;
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(BodyAccent5, -15 * .625f, 15 * .625f);

        if (actor.GetStomachSize(17) > 16)
        {
            AddOffset(BodyAccent7, -5 * .625f, 0);
            AddOffset(BodyAccent8, 5 * .625f, 0);
        }
        else if (actor.GetStomachSize(17) > 14)
        {
            AddOffset(BodyAccent7, -4 * .625f, 0);
            AddOffset(BodyAccent8, 4 * .625f, 0);
        }
        else if (actor.GetStomachSize(17) > 12)
        {
            AddOffset(BodyAccent7, -3 * .625f, 0);
            AddOffset(BodyAccent8, 3 * .625f, 0);
        }
        else if (actor.GetStomachSize(17) > 10)
        {
            AddOffset(BodyAccent7, -2 * .625f, 0);
            AddOffset(BodyAccent8, 2 * .625f, 0);
        }
        else if (actor.GetStomachSize(17) > 8)
        {
            AddOffset(BodyAccent7, -1 * .625f, 0);
            AddOffset(BodyAccent8, 1 * .625f, 0);
        }
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        switch (position)
        {
            case Position.Default:
                return Sprites[64 + actor.Unit.BodySize];
            case Position.Eating:
                return Sprites[59 + actor.Unit.BodySize];
        }
        return base.BodySprite(actor);
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        switch (position)
        {
            case Position.Default:
                if (actor.IsOralVoring)
                    return Sprites[3];
                return Sprites[2];
            case Position.Eating:
                if (actor.IsOralVoring)
                    return Sprites[1];
                return Sprites[0];
        }
        return base.HeadSprite(actor);
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // Antennae
    {
        switch (position)
        {
            case Position.Default:
                return Sprites[33 + actor.Unit.SpecialAccessoryType];
            case Position.Eating:
                return Sprites[24 + actor.Unit.SpecialAccessoryType];
            default:
                return Sprites[33 + actor.Unit.SpecialAccessoryType];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Wings
    {
        switch (position)
        {
            case Position.Default:
                BodyAccent.layer = 6;
                return Sprites[75 + actor.Unit.BodyAccentType1];
            case Position.Eating:
                BodyAccent.layer = 1;
                return Sprites[69 + actor.Unit.BodyAccentType1];
            default:
                return Sprites[75 + actor.Unit.BodyAccentType1];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Spine
    {
        if (position == Position.Default)
            return Sprites[81 + actor.Unit.BodyAccentType2];
        return null;
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Scythes (default position)
    {
        if (!actor.Targetable) return Sprites[48];

        if (actor.IsAttacking || actor.IsOralVoring || position == Position.Eating)
        {
            actor.AnimationController.frameLists[0].currentlyActive = false;
            actor.AnimationController.frameLists[0].currentFrame = 0;
            actor.AnimationController.frameLists[0].currentTime = 0f;
            return null;
        }

        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            if (actor.AnimationController.frameLists[0].currentTime >= frameListScythesDefault.times[actor.AnimationController.frameLists[0].currentFrame])
            {
                actor.AnimationController.frameLists[0].currentFrame++;
                actor.AnimationController.frameLists[0].currentTime = 0f;

                if (actor.AnimationController.frameLists[0].currentFrame >= frameListScythesDefault.frames.Length)
                {
                    actor.AnimationController.frameLists[0].currentlyActive = false;
                    actor.AnimationController.frameLists[0].currentFrame = 0;
                    actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            return Sprites[48 + frameListScythesDefault.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        if (State.Rand.Next(600) == 0)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        return Sprites[48];

    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Scythes (eating position)
    {
        if (!actor.Targetable) return null;

        if (actor.IsAttacking || actor.IsOralVoring || position == Position.Default)
        {
            actor.AnimationController.frameLists[1].currentlyActive = false;
            actor.AnimationController.frameLists[1].currentFrame = 0;
            actor.AnimationController.frameLists[1].currentTime = 0f;
            return null;
        }

        if (actor.AnimationController.frameLists[1].currentlyActive)
        {
            if (actor.AnimationController.frameLists[1].currentTime >= frameListScythesEating.times[actor.AnimationController.frameLists[1].currentFrame])
            {
                actor.AnimationController.frameLists[1].currentFrame++;
                actor.AnimationController.frameLists[1].currentTime = 0f;

                if (actor.AnimationController.frameLists[1].currentFrame >= frameListScythesEating.frames.Length)
                {
                    actor.AnimationController.frameLists[1].currentlyActive = false;
                    actor.AnimationController.frameLists[1].currentFrame = 0;
                    actor.AnimationController.frameLists[1].currentTime = 0f;
                }
            }

            return Sprites[45 + frameListScythesEating.frames[actor.AnimationController.frameLists[1].currentFrame]];
        }

        if (State.Rand.Next(600) == 0)
        {
            actor.AnimationController.frameLists[1].currentlyActive = true;
        }

        return Sprites[45];

    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Scythes (attacking)
    {
        switch (position)
        {
            case Position.Default:
                if (actor.IsAttacking || actor.IsOralVoring)
                    return Sprites[52];
                return null;
            case Position.Eating:
                if (actor.IsAttacking || actor.IsOralVoring)
                    return Sprites[51];
                return null;
            default:
                return null;
        }
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Thorax
    {
        switch (position)
        {
            case Position.Default:
                return Sprites[58];
            case Position.Eating:
                return Sprites[57];
            default:
                return Sprites[58];
        }
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Left Legs
    {
        switch (position)
        {
            case Position.Default:
                return Sprites[55];
            case Position.Eating:
                return Sprites[53];
            default:
                return Sprites[55];
        }
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // Right Legs
    {
        switch (position)
        {
            case Position.Default:
                BodyAccent8.layer = 7;
                return Sprites[56];
            case Position.Eating:
                BodyAccent8.layer = 2;
                return Sprites[54];
            default:
                return Sprites[56];
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        switch (position)
        {
            case Position.Default:
                if (actor.IsOralVoring)
                    return Sprites[5];
                return null;
            case Position.Eating:
                if (actor.IsOralVoring)
                    return Sprites[4];
                return null;
            default:
                return null;
        }
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        switch (position)
        {
            case Position.Default:
                return Sprites[18 + actor.Unit.EyeType];
            case Position.Eating:
                if (actor.IsOralVoring)
                    return Sprites[12 + actor.Unit.EyeType];
                return Sprites[6 + actor.Unit.EyeType];
            default:
                return Sprites[18 + actor.Unit.EyeType];
        }
    }

    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
        switch (position)
        {
            case Position.Default:
                return Sprites[44];
            case Position.Eating:
                if (actor.IsOralVoring)
                    return Sprites[43];
                return Sprites[42];
            default:
                return Sprites[44];
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.Unit.Predator == false || actor.HasBelly == false)
            return null;
        if (position == Position.Eating)
        {
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(17, 1) == 17)
                return Sprites[107];
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                if (actor.GetStomachSize(17, .8f) == 17)
                    return Sprites[106];
                else if (actor.GetStomachSize(17, .9f) == 17)
                    return Sprites[105];
            }
            return Sprites[87 + actor.GetStomachSize(17)];
        }
        return null;
    }

}
