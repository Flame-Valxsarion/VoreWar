using System.Collections.Generic;
using UnityEngine;

class Raiju : BlankSlate
{
    RaceFrameList frameListTail = new RaceFrameList(new int[4] { 0, 1, 2, 1 }, new float[4] { 2.0f, 2.0f, 2.0f, 2.0f });

    public Raiju()
    {
        GentleAnimation = true;
        CanBeGender = new List<Gender>() { Gender.Female, Gender.Male };

        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.RaijuSkin); // Most of Body
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.RaijuSkin); // For Body Pattern
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.RaijuSkin); // For Eyes

        BodyAccent = new SpriteExtraInfo(0, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RaijuSkin, s.Unit.SkinColor)); // Tail
        Body = new SpriteExtraInfo(1, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RaijuSkin, s.Unit.SkinColor)); // Lower Body
        BodyAccent2 = new SpriteExtraInfo(2, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RaijuSkin, s.Unit.SkinColor)); // Slit
        BodyAccent3 = new SpriteExtraInfo(3, BodyAccentSprite3, WhiteColored); // Slit Insides
        Balls = new SpriteExtraInfo(4, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RaijuSkin, s.Unit.SkinColor)); // Balls		
        Dick = new SpriteExtraInfo(5, DickSprite, WhiteColored); // Penis
        BodyAccent4 = new SpriteExtraInfo(6, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RaijuSkin, s.Unit.SkinColor)); // Arms
        Belly = new SpriteExtraInfo(7, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RaijuSkin, s.Unit.SkinColor)); // Belly
        BodyAccent5 = new SpriteExtraInfo(8, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RaijuSkin, s.Unit.SkinColor)); // Upper Body
        BodyAccent6 = new SpriteExtraInfo(9, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RaijuSkin, s.Unit.AccessoryColor)); // Pattern       
        // Layer 10 is alternative for the Dick
        Head = new SpriteExtraInfo(11, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RaijuSkin, s.Unit.SkinColor)); // Head
        Mouth = new SpriteExtraInfo(12, MouthSprite, WhiteColored); // Mouth
        Eyes = new SpriteExtraInfo(13, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RaijuSkin, s.Unit.EyeColor)); // Eyes

        BallsSizes = 4;
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        float scaleMod = actor.Unit.GetScale()*1.5f;
        actor.UnitSprite.GraphicsFolder.transform.localScale = new Vector3(scaleMod, scaleMod, 1); // Embiggify!
    }

    internal override int DickSizes => 4;
    internal override int BreastSizes => 1;

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] { new AnimationController.FrameList(State.Rand.Next(0, 4), State.Rand.Next(1, 20) / 10f, true) };  // Tail controller. Index 0.
    }

    internal override void SetBaseOffsets(Actor_Unit actor) // Offsets
    {
        int sizeBalls = actor.GetBallSize(45);
        int sizeBelly = actor.GetStomachSize(45);

        AddOffset(Head, 0, 15 * .625f);
        AddOffset(Mouth, 0, 15 * .625f);
        AddOffset(Eyes, 0, 15 * .625f);
        AddOffset(Body, 0, 15 * .625f);
        AddOffset(BodyAccent, 0, 15 * .625f);
        AddOffset(BodyAccent2, 0, 15 * .625f);
        AddOffset(BodyAccent3, 0, 15 * .625f);
        AddOffset(BodyAccent4, 0, 15 * .625f);
        AddOffset(BodyAccent5, 0, 15 * .625f);
        AddOffset(BodyAccent6, 0, 15 * .625f);
        AddOffset(Dick, 0, 15 * .625f);

        if (sizeBalls >= 26) AddOffset(Balls, 0, -42 * .41667f);
        else AddOffset(Balls, 0, -42 * .625f);

        if (sizeBelly >= 26) AddOffset(Belly, 0, -15 * .41667f);
        else AddOffset(Belly, 0, -15 * .625f);
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Tail
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        if (actor.AnimationController.frameLists[0].currentTime >= frameListTail.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
        {
            actor.AnimationController.frameLists[0].currentFrame++;
            actor.AnimationController.frameLists[0].currentTime = 0f;

            if (actor.AnimationController.frameLists[0].currentFrame >= frameListTail.frames.Length)
            {
                actor.AnimationController.frameLists[0].currentFrame = 0;
                actor.AnimationController.frameLists[0].currentTime = 0f;
            }
        }

        if (frameListTail.frames[actor.AnimationController.frameLists[0].currentFrame] == 0) return SpriteDictionary.Raiju[13];
        else if (frameListTail.frames[actor.AnimationController.frameLists[0].currentFrame] == 1) return null;
        else if (frameListTail.frames[actor.AnimationController.frameLists[0].currentFrame] == 2) return SpriteDictionary.Raiju[92];

        return null;
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Lower Body
    {
        return SpriteDictionary.Raiju[0];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Slit
    {
        if (actor.Unit.BreastSize < 0) return null;

        if (actor.IsUnbirthing) return SpriteDictionary.Raiju[15];
        return SpriteDictionary.Raiju[14];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Slit Insides
    {
        if (actor.Unit.BreastSize < 0) return null;

        if (actor.IsUnbirthing) return SpriteDictionary.Raiju[16];
        return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor) // Balls
    {
        if (Config.HideCocks || actor.Unit.DickSize < 0) return null;

        if (actor.GetBallSize(45) == 0) return SpriteDictionary.Raiju[36 + actor.Unit.DickSize];

        int size = actor.GetBallSize(45);

        if (size >= 44 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Raiju240[23];
        }

        else if (size >= 42 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Raiju240[22];
        }

        else if (size >= 40 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Raiju240[21];
        }

        else if (size >= 38 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Raiju240[20];
        }

        else if (size >= 36 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Raiju240[19];
        }

        else if (size >= 34 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Raiju240[18];
        }

        else if (size >= 32 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Raiju240[17];
        }

        else if (size >= 30 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Raiju240[16];
        }

        if (size > 29) size = 29;

        if (size >= 26) return SpriteDictionary.Raiju240[12 + size - 26];

        return SpriteDictionary.Raiju[40 + size];
    }

    protected override Sprite DickSprite(Actor_Unit actor) // Penis
    {
        if (actor.Unit.DickSize < 0 || Config.HideCocks) return null;

        if (actor.PredatorComponent?.VisibleFullness < .4f)
        {
            Dick.layer = 10;
            if (actor.IsCockVoring) return SpriteDictionary.Raiju[24 + actor.Unit.DickSize];
            if (actor.IsErect()) return SpriteDictionary.Raiju[20 + actor.Unit.DickSize];
            return null;
        }

        Dick.layer = 5;
        if (actor.IsCockVoring) return SpriteDictionary.Raiju[32 + actor.Unit.DickSize];
        if (actor.IsErect()) return SpriteDictionary.Raiju[28 + actor.Unit.DickSize];
        return null;
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Arms
    {
        if (actor.IsAttacking) return SpriteDictionary.Raiju[3];
        if (actor.IsUnbirthing || actor.IsOralVoring || actor.IsCockVoring) return SpriteDictionary.Raiju[4];
        return SpriteDictionary.Raiju[2];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // Belly
    {
        if (actor.HasBelly == false)
            return null;

        int size = actor.GetStomachSize(45);

        if (size >= 44 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false))
        {
            return SpriteDictionary.Raiju240[11];
        }

        else if (size >= 42 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
        {
            return SpriteDictionary.Raiju240[10];
        }

        else if (size >= 40 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
        {
            return SpriteDictionary.Raiju240[9];
        }

        else if (size >= 38 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
        {
            return SpriteDictionary.Raiju240[8];
        }

        else if (size >= 36 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
        {
            return SpriteDictionary.Raiju240[7];
        }

        else if (size >= 34 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
        {
            return SpriteDictionary.Raiju240[6];
        }

        else if (size >= 32 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
        {
            return SpriteDictionary.Raiju240[5];
        }

        else if (size >= 30 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
        {
            return SpriteDictionary.Raiju240[4];
        }

        if (size > 29) size = 29;

        if (size >= 26) return SpriteDictionary.Raiju240[0 + size - 26];

        return SpriteDictionary.Raiju[66 + size];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Upper Body
    {
        return SpriteDictionary.Raiju[1];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Pattern
    {
        if (actor.IsAttacking) return SpriteDictionary.Raiju[18];
        if (actor.IsUnbirthing || actor.IsOralVoring || actor.IsCockVoring) return SpriteDictionary.Raiju[19];
        return SpriteDictionary.Raiju[17];
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (actor.IsAttacking) return SpriteDictionary.Raiju[6];
        if (actor.IsUnbirthing || actor.IsOralVoring || actor.IsCockVoring) return SpriteDictionary.Raiju[7];
        return SpriteDictionary.Raiju[5];
    }

    protected override Sprite MouthSprite(Actor_Unit actor) // Mouth
    {
        if (actor.IsAttacking) return SpriteDictionary.Raiju[8];
        if (actor.IsUnbirthing || actor.IsOralVoring || actor.IsCockVoring) return SpriteDictionary.Raiju[9];
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor) // Eyes
    {
        if (actor.IsAttacking) return SpriteDictionary.Raiju[11];
        if (actor.IsUnbirthing || actor.IsOralVoring || actor.IsCockVoring) return SpriteDictionary.Raiju[12];
        return SpriteDictionary.Raiju[10];
    }
}
