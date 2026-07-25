using UnityEngine;

class Dragon : BlankSlate
{
    enum Position
    {
        Down,
        Standing,
        StandingCrouch
    }
    Position position;
    readonly Sprite[] Sprites = SpriteDictionary.Dragon;

    public Dragon()
    {
        GentleAnimation = true;

        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, s.Unit.AccessoryColor));
        Head = new SpriteExtraInfo(8, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, s.Unit.AccessoryColor));
        BodyAccent = new SpriteExtraInfo(5, BodyAccentSprite, WhiteColored);
        BodyAccent2 = new SpriteExtraInfo(17, BodyAccentSprite2, WhiteColored);
        BodyAccent3 = new SpriteExtraInfo(3, BodyAccentSprite3, WhiteColored);
        BodyAccent4 = new SpriteExtraInfo(17, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, s.Unit.AccessoryColor));
        BodyAccent5 = new SpriteExtraInfo(6, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, s.Unit.AccessoryColor));
        BodyAccent6 = new SpriteExtraInfo(5, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, s.Unit.AccessoryColor));
        BodyAccent7 = new SpriteExtraInfo(4, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, s.Unit.AccessoryColor));
        BodyAccent8 = new SpriteExtraInfo(5, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, s.Unit.AccessoryColor));

        BodyAccessory = new SpriteExtraInfo(9, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, s.Unit.AccessoryColor));
        Belly = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, s.Unit.AccessoryColor));
        BodySize = new SpriteExtraInfo(7, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, s.Unit.AccessoryColor));
        Dick = new SpriteExtraInfo(15, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, s.Unit.AccessoryColor));
        Balls = new SpriteExtraInfo(11, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Dragon, s.Unit.AccessoryColor));
        SpecialAccessoryCount = 3;

        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Dragon);
        clothingColors = 0;
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.IsAttacking || actor.IsEating)
            position = Position.StandingCrouch;
        else if (actor.HasBelly || actor.PredatorComponent?.BallsFullness > 0)
            position = Position.Standing;
        else
            position = Position.Down;
        base.RunFirst(actor);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Head, 0, 25 * .625f);
        AddOffset(Body, 0, 25 * .625f);
        AddOffset(BodyAccent, 0, 25 * .625f);
        AddOffset(BodyAccent2, 0, 25 * .625f);
        AddOffset(BodyAccent3, 0, 25 * .625f);
        AddOffset(BodyAccent4, 0, 25 * .625f);
        AddOffset(BodyAccent5, 0, 25 * .625f);
        AddOffset(BodyAccent6, 0, 25 * .625f);
        AddOffset(BodyAccent7, 0, 25 * .625f);
        AddOffset(BodyAccent8, 0, 25 * .625f);
        AddOffset(BodyAccessory, 0, 25 * .625f);
        AddOffset(BodySize, 0, 25 * .625f);
        AddOffset(Belly, 0, 25 * .625f);
        AddOffset(Dick, 0, 25 * .625f);
        AddOffset(Balls, 0, 25 * .625f);
    }


    protected override Sprite BodySprite(Actor_Unit actor)
    {
        switch (position)
        {
            case Position.Down:
                return Sprites[0];
            case Position.Standing:
                return Sprites[1];
            case Position.StandingCrouch:
                return Sprites[2];
        }
        return base.BodySprite(actor);
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        switch (position)
        {
            case Position.Down:
                return Sprites[3];
            case Position.Standing:
                if (actor.IsOralVoring)
                    return Sprites[5];
                return Sprites[4];
            case Position.StandingCrouch:
                if (actor.IsOralVoring)
                    return Sprites[7];
                return Sprites[6];
        }
        return base.BodySprite(actor);

    }

    /// <summary>
    /// Top Foot Claws
    /// </summary>
    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        switch (position)
        {
            case Position.Down:
                return Sprites[11];
            case Position.Standing:
                return Sprites[8];
            default:
                return Sprites[9];
        }
    }
    /// <summary>
    /// Arm Claws
    /// </summary>
    /// 
    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        switch (position)
        {
            case Position.Standing:
                if (actor.IsAttacking)
                    return Sprites[13];
                return Sprites[12];
            case Position.StandingCrouch:
                if (actor.IsAttacking)
                    return Sprites[15];
                return Sprites[14];
            default:
                return null;
        }
    }

    /// <summary>
    /// Low Tier Claws
    /// </summary>
    /// 
    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (position == Position.Down)
            return Sprites[10];
        return null;
    }

    /// <summary>
    /// Arms
    /// </summary>
    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        if (position == Position.Down)
            return null;
        if (position == Position.Standing)
        {
            if (actor.IsAttacking)
                return Sprites[17];
            return Sprites[16];
        }
        else
        {
            if (actor.IsAttacking)
                return Sprites[19];
            return Sprites[18];
        }
    }

    /// <summary>
    /// Wings
    /// </summary>
    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        switch (position)
        {
            case Position.Standing:
                if (actor.IsAttacking)
                    return Sprites[37];
                return Sprites[36];
            case Position.StandingCrouch:
                if (actor.IsAttacking)
                    return Sprites[38];
                return Sprites[39];
            default:
                return Sprites[35];
        }
    }

    /// <summary>
    /// Tail Spines
    /// </summary>
    protected override Sprite BodyAccentSprite6(Actor_Unit actor)
    {
        switch (position)
        {
            case Position.Standing:
                return Sprites[41];
            case Position.StandingCrouch:
                return Sprites[42];
            default:
                return Sprites[40];
        }
    }

    /// <summary>
    /// Tail Sprite
    /// </summary>
    protected override Sprite BodyAccentSprite7(Actor_Unit actor)
    {
        switch (position)
        {
            case Position.Standing:
                return Sprites[44];
            case Position.StandingCrouch:
                return Sprites[45];
            default:
                return null;
        }
    }

    /// <summary>
    /// Standing Tail scales
    /// </summary>
    protected override Sprite BodyAccentSprite8(Actor_Unit actor)
    {
        if (position == Position.Standing)
            return Sprites[49];
        return null;
    }



    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        int sprite = 20 + 5 * actor.Unit.SpecialAccessoryType;
        switch (position)
        {
            case Position.Standing:
                if (actor.IsOralVoring)
                    return Sprites[sprite + 1];
                return Sprites[sprite + 2];
            case Position.StandingCrouch:
                if (actor.IsOralVoring)
                    return Sprites[sprite + 3];
                return Sprites[sprite + 4];
            default:
                return Sprites[sprite];
        }
    }


    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.Unit.Predator == false || actor.HasBelly == false)
            return null;
        if (position == Position.Standing || position == Position.StandingCrouch)
        {
            Belly.layer = 16;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                return Sprites[69];
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                if (actor.GetStomachSize(17, 1.4f) == 17)
                    return Sprites[68];
                else if (actor.GetStomachSize(17, 1.6f) == 17)
                    return Sprites[67];
            }
            else
            {
                if (position == Position.StandingCrouch && actor.GetStomachSize(16, 1.75f) >= 12)
                {
                    AddOffset(Belly, 0, -1 * .625f);
                }
                if (position == Position.StandingCrouch && actor.GetStomachSize(16, 1.75f) < 12)
                {
                    AddOffset(Belly, 0, -23 * .625f);
                }
            }
            return Sprites[50 + actor.GetStomachSize(16, 1.75f)];
        }
        return null;
    }

    /// <summary>
    /// Body Scales
    /// </summary>
    protected override Sprite BodySizeSprite(Actor_Unit actor)
    {
        switch (position)
        {

            case Position.Standing:
                return Sprites[47];
            case Position.StandingCrouch:
                return Sprites[48];
            default:
                return Sprites[46];
        }
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false || position == Position.Down)
            return null;
        if (actor.PredatorComponent?.BallsFullness > 0)
        {
            //AddOffset(Balls, 0, 21 * .625f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                return Sprites[91];
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
            {
                if (actor.GetBallSize(14, 1.4f) == 14)
                    return Sprites[90];
                else if (actor.GetBallSize(14, 1.6f) == 14)
                    return Sprites[89];
            }
            return Sprites[75 + actor.GetBallSize(13, 1.75f)];
        }
        return Sprites[75];
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (position == Position.Down)
            return null;
        if (actor.GetStomachSize(16) > 1)
            return null;
        if (actor.Unit.DickSize >= 0)
        {
            return Sprites[73 + actor.Unit.DickSize];
        }
        else
        {
            if (actor.IsUnbirthing)
                return Sprites[72];
            return Sprites[71];

        }
    }


}
