using System;
using UnityEngine;

class Monitors : BlankSlate
{
    RaceFrameList frameListTongue = new RaceFrameList(new int[7] { 0, 1, 2, 1, 2, 1, 0 }, new float[7] { 0.1f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.3f });

    readonly Sprite[] Sprites = SpriteDictionary.Monitors;

    public Monitors()
    {
        BodySizes = 3;
        SpecialAccessoryCount = 7; // body pattern
        clothingColors = 0;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.KomodosSkin);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.KomodosSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.AccessoryColor)); // body pattern
        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor)); // right arm
        BodyAccent2 = new SpriteExtraInfo(7, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.AccessoryColor)); // right arm pattern
        BodyAccent3 = new SpriteExtraInfo(7, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.AccessoryColor)); // head pattern
        BodyAccent4 = new SpriteExtraInfo(7, BodyAccentSprite4, WhiteColored); // claws
        BodyAccent5 = new SpriteExtraInfo(7, BodyAccentSprite5, WhiteColored); // right arm claws
        BodyAccent6 = new SpriteExtraInfo(9, BodyAccentSprite6, WhiteColored); // tongue
        BodyAccent7 = new SpriteExtraInfo(6, BodyAccentSprite7, WhiteColored); // slit inside
        BodyAccent8 = new SpriteExtraInfo(5, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor)); // slit outside
        Mouth = new SpriteExtraInfo(8, MouthSprite, WhiteColored);
        Eyes = new SpriteExtraInfo(8, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        Belly = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, s.Unit.SkinColor));
        Dick = new SpriteExtraInfo(11, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(10, BallsSprite, WhiteColored);

    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(0, 0, false)};  // Tongue controller. Index 0.
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.AccessoryColor = unit.SkinColor;
    }

    internal override int DickSizes => 6;
    internal override int BreastSizes => 1; // (no breasts)


    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        return Sprites[0 + actor.Unit.BodySize];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[7];
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[6];
        return Sprites[5];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // body pattern
    {
        if (actor.Unit.SpecialAccessoryType == 6)
        {
            return null;
        }
        else
        {
            return Sprites[15 + actor.Unit.BodySize + (7 * actor.Unit.SpecialAccessoryType)];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // right arm
    {
        if (actor.IsAttacking)
        {
            return Sprites[4];
        }
        else
        {
            return Sprites[3];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // right arm pattern
    {
        if (actor.Unit.SpecialAccessoryType == 6)
        {
            return null;
        }
        else if (actor.IsAttacking)
        {
            return Sprites[19 + (7 * actor.Unit.SpecialAccessoryType)];
        }
        else
        {
            return Sprites[18 + (7 * actor.Unit.SpecialAccessoryType)];
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // head pattern
    {
        if (actor.Unit.SpecialAccessoryType == 6)
        {
            return null;
        }
        else if (actor.IsOralVoring)
        {
            return Sprites[21 + (7 * actor.Unit.SpecialAccessoryType)];
        }
        else
        {
            return Sprites[20 + (7 * actor.Unit.SpecialAccessoryType)];
        }
    }
    protected override Sprite BodyAccentSprite4(Actor_Unit actor) => Sprites[12]; // claws

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // right arm claws
    {
        if (actor.IsAttacking)
        {
            return Sprites[14];
        }
        else
        {
            return Sprites[13];
        }
    }

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

            return Sprites[57 + frameListTongue.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        if (State.Rand.Next(900) == 0)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        return null;
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // slit inside
    {
        if (Config.HideCocks) return null;

        if (actor.Unit.HasDick == false)
        {
            if (actor.IsUnbirthing)
                return Sprites[62];
            return null;
        }
        else
        {
            if (actor.IsErect() || actor.IsCockVoring)
                return Sprites[65];
            return null;
        }
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // slit outside
    {
        if (Config.HideCocks) return null;

        if (actor.Unit.HasDick == false)
        {
            if (actor.IsUnbirthing)
                return Sprites[61];
            return Sprites[60];
        }
        else
        {
            if (actor.IsErect() || actor.IsCockVoring)
                return Sprites[64];
            return Sprites[63];
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[9];
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[8];
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[11];
        return Sprites[10];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(29, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -13 * .625f);
                return Sprites[153];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -13 * .625f);
                return Sprites[152];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 28)
            {
                AddOffset(Belly, 0, -13 * .625f);
                return Sprites[151];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 27)
            {
                AddOffset(Belly, 0, -13 * .625f);
                return Sprites[150];
            }
            switch (size)
            {
                case 26:
                    AddOffset(Belly, 0, -2 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -4 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -7 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -11 * .625f);
                    break;
            }

            return Sprites[120 + size];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            if (actor.PredatorComponent?.VisibleFullness < 1.35f)
            {
                Dick.layer = 20;
                if (actor.IsCockVoring)
                {
                    return Sprites[72 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[66 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    return Sprites[84 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[78 + actor.Unit.DickSize];
                }
            }
        }

        Dick.layer = 11;
        return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect() && (actor.PredatorComponent?.VisibleFullness < 1.35f))
        {
            Balls.layer = 19;
        }
        else
        {
            Balls.layer = 10;
        }

        int offset = actor.GetBallSize(28, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -27 * .625f);
            return Sprites[119];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -27 * .625f);
            return Sprites[118];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -27 * .625f);
            return Sprites[117];
        }
        else if (offset >= 26)
        {
            AddOffset(Balls, 0, -27 * .625f);
        }
        else if (offset == 25)
        {
            AddOffset(Balls, 0, -18 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -16 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -15 * .625f);
        }
        else if (offset == 22)
        {
            AddOffset(Balls, 0, -12 * .625f);
        }
        else if (offset == 21)
        {
            AddOffset(Balls, 0, -11 * .625f);
        }
        else if (offset == 20)
        {
            AddOffset(Balls, 0, -9 * .625f);
        }
        else if (offset == 19)
        {
            AddOffset(Balls, 0, -7 * .625f);
        }
        else if (offset == 18)
        {
            AddOffset(Balls, 0, -5 * .625f);
        }
        else if (offset == 17)
        {
            AddOffset(Balls, 0, -3 * .625f);
        }

        if (offset > 0)
            return Sprites[Math.Min(90 + offset, 116)];
        return null;
    }








}
