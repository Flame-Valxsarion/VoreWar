using UnityEngine;

class Gazelle : BlankSlate
{

    readonly Sprite[] Sprites = SpriteDictionary.Gazelle1;
    readonly Sprite[] Sprites2 = SpriteDictionary.Gazelle2;

    public Gazelle()
    {
        SpecialAccessoryCount = 8; // ears
        BodyAccentTypes1 = 8; // fur patterns
        BodyAccentTypes2 = 10; // horns (for males)
        TailTypes = 6;
        clothingColors = 0;
        GentleAnimation = true;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.GazelleSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);

        Body = new SpriteExtraInfo(10, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(11, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(13, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, s.Unit.SkinColor)); // ears
        BodyAccent = new SpriteExtraInfo(7, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, s.Unit.SkinColor)); // legs1
        BodyAccent2 = new SpriteExtraInfo(0, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, s.Unit.SkinColor)); // legs2
        BodyAccent3 = new SpriteExtraInfo(10, BodyAccentSprite3, WhiteColored); // hoof1
        BodyAccent4 = new SpriteExtraInfo(0, BodyAccentSprite4, WhiteColored); // hoof2
        BodyAccent5 = new SpriteExtraInfo(7, BodyAccentSprite5, WhiteColored); // hoof3
        BodyAccent6 = new SpriteExtraInfo(8, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, s.Unit.SkinColor)); // tail
        BodyAccent7 = new SpriteExtraInfo(15, BodyAccentSprite7, WhiteColored); // horns
        BodyAccent8 = new SpriteExtraInfo(2, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, s.Unit.SkinColor)); // sheath
        BodyAccent9 = new SpriteExtraInfo(4, BodyAccentSprite9, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, s.Unit.SkinColor)); // belly cover
        Mouth = new SpriteExtraInfo(13, MouthSprite, WhiteColored);
        Eyes = new SpriteExtraInfo(13, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        Belly = new SpriteExtraInfo(6, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, s.Unit.SkinColor));
        Dick = new SpriteExtraInfo(3, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(1, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GazelleSkin, s.Unit.SkinColor));

    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Balls, -30 * .625f, -45 * .625f);
        AddOffset(Belly, -10 * .625f, -40 * .625f);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
        unit.TailType = State.Rand.Next(TailTypes);
    }

    protected override Sprite BodySprite(Actor_Unit actor) => Sprites[0 + actor.Unit.BodyAccentType1];

    protected override Sprite HeadSprite(Actor_Unit actor) => Sprites[24 + ((actor.IsAttacking || actor.IsEating) ? 1 : 0) + 2 * actor.Unit.BodyAccentType1];

    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        if ((actor.Unit.BodyAccentType1 == 1 || actor.Unit.BodyAccentType1 == 5) && actor.Unit.SpecialAccessoryType == 3)
        {
            return Sprites[58];
        }
        else if ((actor.Unit.BodyAccentType1 == 1 || actor.Unit.BodyAccentType1 == 5) && actor.Unit.SpecialAccessoryType == 5)
        {
            return Sprites[59];
        }
        else
        {
            return Sprites[50 + actor.Unit.SpecialAccessoryType];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites[9 + actor.Unit.BodyAccentType1 * 2];

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites[8 + actor.Unit.BodyAccentType1 * 2];

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) => Sprites[40];

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) => Sprites[41];

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) => Sprites[42];

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) => Sprites[43 + actor.Unit.TailType];

    protected override Sprite BodyAccentSprite7(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == true)
        {
            return Sprites[60 + actor.Unit.BodyAccentType2];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor)
    {
        if (Config.HideCocks) return null;

        if (actor.Unit.HasDick == true)
        {
            return Sprites2[31];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite9(Actor_Unit actor) => Sprites[49];

    protected override Sprite MouthSprite(Actor_Unit actor) => Sprites[71 + ((actor.IsAttacking || actor.IsEating) ? 1 : 0)];

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[70];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.GetStomachSize(27) > 9)
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
            return Sprites2[30];
        if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
        {
            if (actor.GetStomachSize(27, .8f) == 27)
                return Sprites2[29];
            else if (actor.GetStomachSize(27, .9f) == 27)
                return Sprites2[28];
        }
        return Sprites2[0 + actor.GetStomachSize(27)];
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect())
        {
            return Sprites2[32];
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
                return Sprites2[66];
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
            {
                if (actor.GetBallSize(30, .8f) == 30)
                    return Sprites2[65];
                else if (actor.GetBallSize(30, .9f) == 30)
                    return Sprites2[64];
            }
            return Sprites2[33 + actor.GetBallSize(30)];
        }
        return Sprites2[33];
    }

















}
