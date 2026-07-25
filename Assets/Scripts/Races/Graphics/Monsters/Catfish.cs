using System.Collections.Generic;
using UnityEngine;

class Catfish : BlankSlate
{
    RaceFrameList frameListMouth = new RaceFrameList(new int[9] { 0, 1, 2, 1, 0, 1, 2, 1, 0 }, new float[9] { 1.2f, .6f, 1.2f, .6f, 1.2f, .6f, 1.2f, .6f, 1.2f });
    RaceFrameList frameListTail = new RaceFrameList(new int[9] { 0, 1, 2, 1, 0, 1, 2, 1, 0 }, new float[9] { .5f, .3f, .5f, .3f, .5f, .3f, .5f, .3f, .5f });

    readonly Sprite[] Sprites = SpriteDictionary.Catfish;

    public Catfish()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        SpecialAccessoryCount = 6; // barbels
        BodyAccentTypes1 = 8; // dorsal fins
        clothingColors = 0;
        GentleAnimation = true;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.CatfishSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ViperSkin);

        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(7, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, s.Unit.SkinColor)); // barbels
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, s.Unit.SkinColor)); // dorsal fins
        BodyAccent2 = new SpriteExtraInfo(5, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, s.Unit.SkinColor)); // barbels secondary
        BodyAccent3 = new SpriteExtraInfo(1, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, s.Unit.SkinColor)); // tail
        BodyAccent4 = new SpriteExtraInfo(4, BodyAccentSprite4, WhiteColored); // gills
        BodyAccent5 = new SpriteExtraInfo(4, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, s.Unit.SkinColor)); // pelvic fin
        Mouth = new SpriteExtraInfo(6, MouthSprite, WhiteColored);
        Eyes = new SpriteExtraInfo(8, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, s.Unit.EyeColor));
        SecondaryEyes = new SpriteExtraInfo(8, EyesSecondarySprite, WhiteColored);
        Belly = new SpriteExtraInfo(3, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CatfishSkin, s.Unit.SkinColor));

    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(0, 0, false),  // Mouth controller. Index 0.
            new AnimationController.FrameList(0, 0, false)}; // Tail controller. Index 1.
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.Unit.Predator && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) && actor.GetStomachSize(20) == 20)
        {
            AddOffset(Body, 0, 10 * .625f);
            AddOffset(Head, 0, 10 * .625f);
            AddOffset(BodyAccessory, 0, 10 * .625f);
            AddOffset(BodyAccent, 0, 10 * .625f);
            AddOffset(BodyAccent2, 0, 10 * .625f);
            AddOffset(BodyAccent3, 60 * .625f, 10 * .625f);
            AddOffset(BodyAccent4, 0, 8 * .625f);
            AddOffset(BodyAccent5, 0, 8 * .625f);
            AddOffset(Mouth, 0, 10 * .625f);
            AddOffset(Eyes, 0, 10 * .625f);
            AddOffset(SecondaryEyes, 0, 10 * .625f);
        }
        else if (actor.Unit.Predator && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) && actor.GetStomachSize(20, .8f) == 20)
        {
            AddOffset(Body, 0, 6 * .625f);
            AddOffset(Head, 0, 6 * .625f);
            AddOffset(BodyAccessory, 0, 6 * .625f);
            AddOffset(BodyAccent, 0, 6 * .625f);
            AddOffset(BodyAccent2, 0, 6 * .625f);
            AddOffset(BodyAccent3, 60 * .625f, 6 * .625f);
            AddOffset(BodyAccent4, 0, 4 * .625f);
            AddOffset(BodyAccent5, 0, 4 * .625f);
            AddOffset(Mouth, 0, 6 * .625f);
            AddOffset(Eyes, 0, 6 * .625f);
            AddOffset(SecondaryEyes, 0, 6 * .625f);
        }
        else if (actor.Unit.Predator && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) && actor.GetStomachSize(20, .9f) == 20)
        {
            AddOffset(Body, 0, 3 * .625f);
            AddOffset(Head, 0, 3 * .625f);
            AddOffset(BodyAccessory, 0, 3 * .625f);
            AddOffset(BodyAccent, 0, 3 * .625f);
            AddOffset(BodyAccent2, 0, 3 * .625f);
            AddOffset(BodyAccent3, 60 * .625f, 3 * .625f);
            AddOffset(BodyAccent4, 0, 1 * .625f);
            AddOffset(BodyAccent5, 0, 1 * .625f);
            AddOffset(Mouth, 0, 3 * .625f);
            AddOffset(Eyes, 0, 3 * .625f);
            AddOffset(SecondaryEyes, 0, 3 * .625f);
        }
        else if (actor.GetStomachSize(20) > 11)
        {
            AddOffset(BodyAccent3, 60 * .625f, 0);
            AddOffset(BodyAccent4, 1 * .625f, -2 * .625f);
            AddOffset(BodyAccent5, 1 * .625f, -2 * .625f);
        }
        else if (actor.GetStomachSize(20) > 7)
        {
            AddOffset(BodyAccent3, 60 * .625f, 0);
            AddOffset(BodyAccent4, 1 * .625f, -1 * .625f);
            AddOffset(BodyAccent5, 1 * .625f, -1 * .625f);
        }
        else if (actor.GetStomachSize(20) > 3)
        {
            AddOffset(BodyAccent3, 60 * .625f, 0);
            AddOffset(BodyAccent4, 1 * .625f, 0);
            AddOffset(BodyAccent5, 1 * .625f, 0);
        }
        else
        {
            AddOffset(BodyAccent3, 60 * .625f, 0);
        }
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        if (actor.HasBelly == false)
            return Sprites[0];
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            return Sprites[80];
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
        {
            if (actor.GetStomachSize(20, .8f) == 20)
                return Sprites[80];
            else if (actor.GetStomachSize(20, .9f) == 20)
                return Sprites[80];
        }
        return Sprites[60 + actor.GetStomachSize(20)];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (!actor.Targetable) return Sprites[4];

        if (actor.IsAttacking || actor.IsOralVoring)
        {
            actor.AnimationController.frameLists[0].currentlyActive = false;
            actor.AnimationController.frameLists[0].currentFrame = 0;
            actor.AnimationController.frameLists[0].currentTime = 0f;
            return Sprites[7];
        }

        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            if (actor.AnimationController.frameLists[0].currentTime >= frameListMouth.times[actor.AnimationController.frameLists[0].currentFrame])
            {
                actor.AnimationController.frameLists[0].currentFrame++;
                actor.AnimationController.frameLists[0].currentTime = 0f;

                if (actor.AnimationController.frameLists[0].currentFrame >= frameListMouth.frames.Length)
                {
                    actor.AnimationController.frameLists[0].currentlyActive = false;
                    actor.AnimationController.frameLists[0].currentFrame = 0;
                    actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            return Sprites[4 + frameListMouth.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        if (State.Rand.Next(800) == 0)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        return Sprites[4];
    }

    protected override Sprite MouthSprite(Actor_Unit actor) // Frame list functions handled by the Head Sprite method.
    {
        if (!actor.Targetable) return Sprites[8];

        if (actor.IsAttacking || actor.IsOralVoring)
        {
            actor.AnimationController.frameLists[0].currentlyActive = false;
            return Sprites[11];
        }

        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            return Sprites[8 + frameListMouth.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        return Sprites[8];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites[12 + actor.Unit.SpecialAccessoryType]; // Barbels

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites[28 + actor.Unit.BodyAccentType1]; // Dorsal fins

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites[18 + actor.Unit.SpecialAccessoryType]; // Barbels secondary

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // tail
    {
        if (!actor.Targetable) return Sprites[1];

        if (actor.IsAttacking || actor.IsOralVoring)
        {
            actor.AnimationController.frameLists[1].currentlyActive = false;
            actor.AnimationController.frameLists[1].currentFrame = 0;
            actor.AnimationController.frameLists[1].currentTime = 0f;
            return Sprites[1];
        }

        if (actor.AnimationController.frameLists[1].currentlyActive)
        {
            if (actor.AnimationController.frameLists[1].currentTime >= frameListTail.times[actor.AnimationController.frameLists[0].currentFrame])
            {
                actor.AnimationController.frameLists[1].currentFrame++;
                actor.AnimationController.frameLists[1].currentTime = 0f;

                if (actor.AnimationController.frameLists[1].currentFrame >= frameListTail.frames.Length)
                {
                    actor.AnimationController.frameLists[1].currentlyActive = false;
                    actor.AnimationController.frameLists[1].currentFrame = 0;
                    actor.AnimationController.frameLists[1].currentTime = 0f;
                }
            }

            return Sprites[1 + frameListTail.frames[actor.AnimationController.frameLists[1].currentFrame]];
        }

        if (State.Rand.Next(800) == 0)
        {
            actor.AnimationController.frameLists[1].currentlyActive = true;
        }

        return Sprites[1];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) => Sprites[26]; // Gills

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) => Sprites[27]; // Pelvic Fin

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[25];

    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => Sprites[24];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            return Sprites[59];
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
        {
            if (actor.GetStomachSize(20, .8f) == 20)
                return Sprites[58];
            else if (actor.GetStomachSize(20, .9f) == 20)
                return Sprites[57];
        }
        return Sprites[36 + actor.GetStomachSize(20)];
    }

}
