using UnityEngine;

class Gryphon : BlankSlate
{
    enum Position
    {
        Standing,
        Sitting
    }
    Position position;
    readonly Sprite[] Sprites = SpriteDictionary.Gryphon;
    readonly Sprite[] SpritesAlt = SpriteDictionary.Griffin;

    public Gryphon()
    {
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.GryphonSkin);
        GentleAnimation = true;

        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(4, HeadSprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, s.Unit.SkinColor)); // right wing
        BodyAccent2 = new SpriteExtraInfo(17, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, s.Unit.SkinColor)); // left wing
        BodyAccent3 = new SpriteExtraInfo(14, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, s.Unit.SkinColor)); // left side legs (only sitting)
        BodyAccent4 = new SpriteExtraInfo(3, BodyAccentSprite4, WhiteColored); // right claw (or both in standing)
        BodyAccent5 = new SpriteExtraInfo(15, BodyAccentSprite5, WhiteColored); // left claw (only sitting)
        BodyAccent6 = new SpriteExtraInfo(10, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, s.Unit.SkinColor)); // right ball (only sitting)
        BodyAccent7 = new SpriteExtraInfo(11, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, s.Unit.SkinColor)); // dick base
        BodyAccent8 = new SpriteExtraInfo(16, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, s.Unit.SkinColor)); // exra feather patch (only Griffin)
        Belly = new SpriteExtraInfo(9, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, s.Unit.SkinColor));
        BodySize = new SpriteExtraInfo(7, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, s.Unit.SkinColor)); // belly cover up
        Dick = new SpriteExtraInfo(12, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(13, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GryphonSkin, s.Unit.SkinColor));

        SpecialAccessoryCount = 2;
        clothingColors = 0;
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.HasBelly || actor.PredatorComponent?.BallsFullness > 0)
            position = Position.Sitting;
        else
            position = Position.Standing;
        base.RunFirst(actor);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.Unit.Predator && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(16) == 16)
        {
            AddOffset(Body, 0, 30 * .625f);
            AddOffset(Head, 0, 30 * .625f);
            AddOffset(BodyAccent, 0, 30 * .625f);
            AddOffset(BodyAccent2, 0, 30 * .625f);
            AddOffset(BodyAccent3, 0, 30 * .625f);
            AddOffset(BodyAccent4, 0, 30 * .625f);
            AddOffset(BodyAccent5, 0, 30 * .625f);
            AddOffset(BodyAccent6, 15 * .625f, 12 * .625f);
            AddOffset(BodyAccent7, 0, 30 * .625f);
            AddOffset(BodyAccent8, 0, 30 * .625f);
            AddOffset(Dick, 0, 30 * .625f);
            AddOffset(Balls, 20 * .625f, 10 * .625f);
        }
        else if (actor.Unit.Predator && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(16, .8f) == 16)
        {
            AddOffset(Body, 0, 20 * .625f);
            AddOffset(Head, 0, 20 * .625f);
            AddOffset(BodyAccent, 0, 20 * .625f);
            AddOffset(BodyAccent2, 0, 20 * .625f);
            AddOffset(BodyAccent3, 0, 20 * .625f);
            AddOffset(BodyAccent4, 0, 20 * .625f);
            AddOffset(BodyAccent5, 0, 20 * .625f);
            AddOffset(BodyAccent6, 15 * .625f, 2 * .625f);
            AddOffset(BodyAccent7, 0, 20 * .625f);
            AddOffset(BodyAccent8, 0, 20 * .625f);
            AddOffset(Dick, 0, 20 * .625f);
            AddOffset(Balls, 20 * .625f, 0);
        }
        else if (actor.Unit.Predator && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(16, .9f) == 16)
        {
            AddOffset(Body, 0, 10 * .625f);
            AddOffset(Head, 0, 10 * .625f);
            AddOffset(BodyAccent, 0, 10 * .625f);
            AddOffset(BodyAccent2, 0, 10 * .625f);
            AddOffset(BodyAccent3, 0, 10 * .625f);
            AddOffset(BodyAccent4, 0, 10 * .625f);
            AddOffset(BodyAccent5, 0, 10 * .625f);
            AddOffset(BodyAccent6, 15 * .625f, -8 * .625f);
            AddOffset(BodyAccent7, 0, 10 * .625f);
            AddOffset(BodyAccent8, 0, 10 * .625f);
            AddOffset(Dick, 0, 10 * .625f);
            AddOffset(Balls, 20 * .625f, -10 * .625f);
        }
        else
        {
            AddOffset(Balls, 20 * .625f, -20 * .625f);
            AddOffset(BodyAccent6, 15 * .625f, -18 * .625f);
        }
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 0)
        {
            switch (position)
            {
                case Position.Standing:
                    if (actor.IsOralVoring || actor.IsAttacking)
                        return Sprites[1];
                    return Sprites[0];
                case Position.Sitting:
                    if (actor.IsOralVoring || actor.IsAttacking)
                        return Sprites[3];
                    return Sprites[2];
            }
            return base.BodySprite(actor);
        }
        else
        {
            switch (position)
            {
                case Position.Standing:
                    if (actor.IsOralVoring || actor.IsAttacking)
                        return SpritesAlt[1];
                    return SpritesAlt[0];
                case Position.Sitting:
                    if (actor.IsOralVoring || actor.IsAttacking)
                        return SpritesAlt[3];
                    return SpritesAlt[2];
            }
            return base.BodySprite(actor);
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 0)
        {
            switch (position)
            {
                case Position.Standing:
                    if (actor.IsOralVoring || actor.IsAttacking)
                        return Sprites[11];
                    return Sprites[10];
                case Position.Sitting:
                    if (actor.IsOralVoring || actor.IsAttacking)
                        return Sprites[13];
                    return Sprites[12];
            }
            return base.HeadSprite(actor);
        }
        else
        {
            switch (position)
            {
                case Position.Standing:
                    if (actor.IsOralVoring || actor.IsAttacking)
                        return SpritesAlt[11];
                    return SpritesAlt[10];
                case Position.Sitting:
                    if (actor.IsOralVoring || actor.IsAttacking)
                        return SpritesAlt[13];
                    return SpritesAlt[12];
            }
            return base.HeadSprite(actor);
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // right wing
    {
        if (actor.Unit.SpecialAccessoryType == 0)
        {
            switch (position)
            {
                case Position.Standing:
                    if (actor.IsOralVoring || actor.IsAttacking)
                        return Sprites[7];
                    return Sprites[5];
                case Position.Sitting:
                    return Sprites[9];
                default:
                    return Sprites[5];
            }
        }
        else
        {
            switch (position)
            {
                case Position.Standing:
                    if (actor.IsOralVoring || actor.IsAttacking)
                        return SpritesAlt[7];
                    return SpritesAlt[5];
                case Position.Sitting:
                    return SpritesAlt[9];
                default:
                    return SpritesAlt[5];
            }
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // left wing
    {
        if (actor.Unit.SpecialAccessoryType == 0)
        {
            switch (position)
            {
                case Position.Standing:
                    if (actor.IsOralVoring || actor.IsAttacking)
                        return Sprites[6];
                    return Sprites[4];
                case Position.Sitting:
                    return Sprites[8];
                default:
                    return Sprites[4];
            }
        }
        else
        {
            switch (position)
            {
                case Position.Standing:
                    if (actor.IsOralVoring || actor.IsAttacking)
                        return SpritesAlt[6];
                    return SpritesAlt[4];
                case Position.Sitting:
                    return SpritesAlt[8];
                default:
                    return SpritesAlt[4];
            }
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // left side legs (only sitting)
    {
        if (actor.Unit.SpecialAccessoryType == 0)
        {
            if (position == Position.Sitting)
                return Sprites[14];
            return null;
        }
        else
        {
            if (position == Position.Sitting)
                return SpritesAlt[14];
            return null;
        }
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // right claw (or both in standing)
    {
        if (actor.Unit.SpecialAccessoryType == 0)
        {
            switch (position)
            {
                case Position.Standing:
                    return Sprites[15];
                case Position.Sitting:
                    return Sprites[17];
                default:
                    return Sprites[15];
            }
        }
        else
        {
            switch (position)
            {
                case Position.Standing:
                    return SpritesAlt[15];
                case Position.Sitting:
                    return SpritesAlt[17];
                default:
                    return SpritesAlt[15];
            }
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // left claw (only sitting)
    {
        if (actor.Unit.SpecialAccessoryType == 0)
        {
            if (position == Position.Sitting)
                return Sprites[16];
            return null;
        }
        else
        {
            if (position == Position.Sitting)
                return SpritesAlt[16];
            return null;
        }
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // right ball (only sitting)
    {
        if (actor.Unit.HasDick == false || position == Position.Standing)
            return null;
        if (actor.GetBallSize(10, 1.5f) > 5)
        {
            BodyAccent6.layer = 1;
            if (actor.PredatorComponent?.BallsFullness > 0)
            {
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                    return Sprites[47];
                return Sprites[36 + actor.GetBallSize(10, 1.5f)];
            }
            return Sprites[36];
        }
        if (actor.GetStomachSize(16) < 3)
        {
            BodyAccent6.layer = 10;
            if (actor.PredatorComponent?.BallsFullness > 0)
            {
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                    return Sprites[47];
                return Sprites[36 + actor.GetBallSize(10, 1.5f)];
            }
            return Sprites[36];
        }
        else
        {
            BodyAccent6.layer = 5;
            if (actor.PredatorComponent?.BallsFullness > 0)
            {
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                    return Sprites[47];
                return Sprites[36 + actor.GetBallSize(10, 1.5f)];
            }
            return Sprites[36];
        }
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // dick base
    {
        if (actor.Unit.HasDick == true)
        {
            BodyAccent7.layer = 11;
            if (position == Position.Standing)
                return Sprites[48];
            else
            {
                if (actor.HasBelly == true)
                {
                    if (actor.GetStomachSize(16) == 0)
                        return Sprites[49];
                    else if (actor.GetStomachSize(16) == 1)
                        return Sprites[50];
                    else if (actor.GetStomachSize(16) > 1 && actor.GetStomachSize(16) < 4)
                        return Sprites[51];
                    else if (actor.GetStomachSize(16) >= 4 && actor.GetStomachSize(16) < 7)
                        return Sprites[52];
                    else if (actor.GetStomachSize(16) >= 7 && actor.GetStomachSize(16) < 11)
                        return Sprites[53];
                    else if (actor.GetStomachSize(16) >= 11)
                    {
                        BodyAccent7.layer = 6;
                        return Sprites[53];
                    }
                    return null;
                }
                return Sprites[49];
            }
        }
        return null;
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // right claw (or both in standing)
    {
        if (actor.Unit.SpecialAccessoryType == 0)
        {
            return null;
        }
        else
        {
            switch (position)
            {
                case Position.Standing:
                    return SpritesAlt[18];
                case Position.Sitting:
                    return SpritesAlt[19];
                default:
                    return SpritesAlt[18];
            }
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.Unit.Predator == false || actor.HasBelly == false)
            return null;
        if (position == Position.Sitting)
        {
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(16, 1) == 16)
                return Sprites[35];
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                if (actor.GetStomachSize(16, .8f) == 16)
                    return Sprites[61];
                else if (actor.GetStomachSize(16, .9f) == 16)
                    return Sprites[60];
            }
            return Sprites[18 + actor.GetStomachSize(16)];
        }
        return null;
    }

    protected override Sprite BodySizeSprite(Actor_Unit actor) // belly cover up in case of gryphon having used only cock vore option
    {
        if (position == Position.Sitting && actor.HasBelly == false)
            return Sprites[59];
        return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        int sz = actor.GetStomachSize(16);
        int bz = actor.GetBallSize(10, 1.5f);
        if (actor.Unit.HasDick == false || position == Position.Standing)
            return null;
        if (actor.GetStomachSize(16) < 12 || sz < bz * 2)
        {
            Balls.layer = 13;
            if (actor.PredatorComponent?.BallsFullness > 0)
            {
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                    return Sprites[47];
                return Sprites[36 + actor.GetBallSize(10, 1.5f)];
            }
            return Sprites[36];
        }
        else
        {
            Balls.layer = 8;
            if (actor.PredatorComponent?.BallsFullness > 0)
            {
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                    return Sprites[47];
                return Sprites[36 + actor.GetBallSize(10, 1.5f)];
            }
            return Sprites[36];
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false || position == Position.Standing)
            return null;
        if (actor.IsErect())
        {
            Dick.layer = 12;
            if (actor.GetStomachSize(16) == 0)
                return Sprites[54];
            else if (actor.GetStomachSize(16) == 1)
                return Sprites[55];
            else if (actor.GetStomachSize(16) > 1 && actor.GetStomachSize(16) < 4)
                return Sprites[56];
            else if (actor.GetStomachSize(16) >= 4 && actor.GetStomachSize(16) < 7)
                return Sprites[57];
            else if (actor.GetStomachSize(16) >= 7 && actor.GetStomachSize(16) < 11)
                return Sprites[58];
            else if (actor.GetStomachSize(16) >= 11)
            {
                Dick.layer = 7;
                return Sprites[58];
            }
            return null;
        }
        return null;
    }

}
