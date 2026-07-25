using UnityEngine;

class FeralLizards : BlankSlate
{
    RaceFrameList frameListTongue = new RaceFrameList(new int[3] { 0, 1, 2 }, new float[3] { 0.5f, 0.2f, 0.3f });

    readonly Sprite[] Sprites = SpriteDictionary.FeralLizards;

    public FeralLizards()
    {
        SpecialAccessoryCount = 10; // body pattern
        BodyAccentTypes1 = 2; // teeths on/off
        clothingColors = 0;
        GentleAnimation = true;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.KomodosSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);

        Body = new SpriteExtraInfo(10, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(11, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(12, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor)); // body pattern
        BodyAccent = new SpriteExtraInfo(7, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor)); // legs1
        BodyAccent2 = new SpriteExtraInfo(0, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor)); // legs2
        BodyAccent3 = new SpriteExtraInfo(10, BodyAccentSprite3, WhiteColored); // claws1
        BodyAccent4 = new SpriteExtraInfo(0, BodyAccentSprite4, WhiteColored); // claws2
        BodyAccent5 = new SpriteExtraInfo(7, BodyAccentSprite5, WhiteColored); // claws3
        BodyAccent6 = new SpriteExtraInfo(14, BodyAccentSprite6, WhiteColored); // tongue animation
        BodyAccent7 = new SpriteExtraInfo(14, BodyAccentSprite7, WhiteColored); // teeth
        BodyAccent8 = new SpriteExtraInfo(2, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor)); // sheath
        BodyAccent9 = new SpriteExtraInfo(4, BodyAccentSprite9, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor)); // belly cover
        Mouth = new SpriteExtraInfo(13, MouthSprite, WhiteColored);
        Eyes = new SpriteExtraInfo(13, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        Belly = new SpriteExtraInfo(6, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor));
        Dick = new SpriteExtraInfo(3, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(1, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor));

    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Balls, -30 * .625f, 0);
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(0, 0, false)};  // Tongue controller. Index 0.
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.BodyAccentType1 = 0;
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        return Sprites[0];
    }


    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[8];
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[7];
        return Sprites[6];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // body pattern
    {
        if (actor.Unit.SpecialAccessoryType == 9)
        {
            return null;
        }
        else
        {
            return Sprites[14 + actor.Unit.SpecialAccessoryType];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites[1]; // legs1

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites[2]; // legs2

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) => Sprites[3]; // claws1

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) => Sprites[5]; // claws2

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) => Sprites[4]; // claws3

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // tongue animation
    {
        if (!actor.Targetable) return null;

        if (actor.IsAttacking || actor.IsEating)
        {
            actor.AnimationController.frameLists[0].currentlyActive = false;
            actor.AnimationController.frameLists[0].currentFrame = 0;
            actor.AnimationController.frameLists[0].currentTime = 0f;
            return null;
        }

        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            if (actor.AnimationController.frameLists[0].currentTime >= frameListTongue.times[actor.AnimationController.frameLists[0].currentFrame])
            {
                actor.AnimationController.frameLists[0].currentFrame++;
                actor.AnimationController.frameLists[0].currentTime = 0f;

                if (actor.AnimationController.frameLists[0].currentFrame >= frameListTongue.frames.Length)
                {
                    actor.AnimationController.frameLists[0].currentlyActive = false;
                    actor.AnimationController.frameLists[0].currentFrame = 0;
                    actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            return Sprites[87 + frameListTongue.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        if (actor.PredatorComponent?.VisibleFullness > 0 && State.Rand.Next(900) == 0)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        return null;
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // teeth
    {
        if (actor.Unit.BodyAccentType1 == 1)
        {
            return null;
        }
        else
        {
            if (actor.IsOralVoring)
                return Sprites[86];
            if (actor.IsAttacking || actor.IsEating)
                return Sprites[85];
            return null;
        }
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // sheath
    {
        if (Config.HideCocks) return null;

        if (actor.Unit.HasDick == true)
        {
            return Sprites[23];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite9(Actor_Unit actor) => Sprites[9];

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[13];
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[12];
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[11];
        return Sprites[10];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.GetStomachSize(26) > 9)
        {
            Belly.layer = 9;
        }
        else
        {
            Belly.layer = 6;
        }

        if (actor.HasBelly == false)
            return null;
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
            return Sprites[54];
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
        {
            if (actor.GetStomachSize(26, .8f) == 26)
                return Sprites[53];
            else if (actor.GetStomachSize(26, .9f) == 26)
                return Sprites[52];
        }
        return Sprites[25 + actor.GetStomachSize(26)];
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect())
        {
            return Sprites[24];
        }
        return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.PredatorComponent?.BallsFullness > 0)
        {
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                return Sprites[84];
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
            {
                if (actor.GetBallSize(26, .8f) == 26)
                    return Sprites[83];
                else if (actor.GetBallSize(26, .9f) == 26)
                    return Sprites[82];
            }
            return Sprites[55 + actor.GetBallSize(26)];
        }
        return Sprites[55];
    }

















}
