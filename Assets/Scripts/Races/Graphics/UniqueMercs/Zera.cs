using System;
using System.Collections.Generic;
using UnityEngine;

class Zera : BlankSlate
{
    readonly Sprite[] SpritesMain = SpriteDictionary.Zera240;
    readonly Sprite[] SpritesFrontBelly = SpriteDictionary.ZeraFrontBelly;
    readonly Sprite[] SpritesBelly = SpriteDictionary.ZeraBelly;
    readonly Sprite[] SpritesBalls = SpriteDictionary.ZeraBalls;

    int[] BallsLow = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 9, 10, 11, 12, 17, 18, 19, 20, 21, 22, 35, 34, 33, 32 }; //8 is cut out so the lengths match
    int[] BallsMedium = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 13, 14, 15, 16, 23, 24, 25, 20, 21, 22, 35, 34, 33, 32 };
    int[] BallsHigh = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 13, 14, 15, 16, 26, 27, 28, 29, 30, 31, 35, 34, 33, 32 };

    int StomachSize;

    enum BodyStateType
    {
        First,
        Second,
        Third,

    }

    BodyStateType BodyState;


    public Zera()
    {
        CanBeGender = new List<Gender>() { Gender.Male };
        GentleAnimation = true;
        Head = new SpriteExtraInfo(5, HeadSprite, WhiteColored);
        Belly = new SpriteExtraInfo(2, null, WhiteColored);
        Body = new SpriteExtraInfo(-1, BodySprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(7, BodyAccentSprite, WhiteColored);
        BodyAccent2 = new SpriteExtraInfo(6, BodyAccentSprite2, WhiteColored);
        BodyAccent3 = new SpriteExtraInfo(4, BodyAccentSprite3, WhiteColored);
        BodyAccent4 = new SpriteExtraInfo(3, BodyAccentSprite4, WhiteColored);
        BodyAccent5 = new SpriteExtraInfo(1, BodyAccentSprite5, WhiteColored);
        BodyAccent6 = new SpriteExtraInfo(0, BodyAccentSprite6, WhiteColored);
        BodyAccent7 = new SpriteExtraInfo(1, BodyAccentSprite7, WhiteColored);
        Dick = new SpriteExtraInfo(8, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(9, BallsSprite, WhiteColored);
        clothingColors = 0;
        TailTypes = 2;

        //BodyAccentTypes1 = 19;
        //BodyAccentTypes2 = 35;

    }
    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Zera";
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        StomachSize = actor.GetStomachSize(19, 1);
        //StomachSize = actor.Unit.BodyAccentType1;
        if (StomachSize >= 7 && actor.PredatorComponent?.BallsFullness == 0)
        {
            BodyState = BodyStateType.Third;
        }
        else if (StomachSize >= 7 || actor.Unit.TailType == 1 || actor.PredatorComponent?.BallsFullness > 0)
        {
            BodyState = BodyStateType.Second;
        }
        else
        {
            BodyState = BodyStateType.First;
        }

        base.RunFirst(actor);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (StomachSize >= 10 && BodyState == BodyStateType.Second)
        {
            float offset = 110 * .41666667f;
            AddOffset(Head, 0, offset);
            AddOffset(Body, 0, offset);
            AddOffset(BodyAccent, 0, offset);
            AddOffset(BodyAccent2, 0, offset);
            AddOffset(BodyAccent3, 0, offset);
            AddOffset(BodyAccent4, 0, offset);
            AddOffset(BodyAccent5, 0, offset);
            AddOffset(BodyAccent6, 0, offset);
            AddOffset(BodyAccent7, 0, offset);
            AddOffset(Balls, 0, offset);
            AddOffset(Dick, 0, offset);
        }
        else if (StomachSize >= 7 && BodyState == BodyStateType.Second)
            AddOffset(Belly, 0, -62 * .41666667f);

        base.SetBaseOffsets(actor);
    }


    protected override Sprite BodySprite(Actor_Unit actor)
    {
        switch (BodyState)
        {
            case BodyStateType.Third:
                return null;
            case BodyStateType.Second:
                return null;
            default:
                return SpritesMain[0];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        switch (BodyState)
        {
            case BodyStateType.Third:
                if (actor.IsOralVoring)
                    return SpritesMain[36];
                return SpritesMain[35];
            case BodyStateType.Second:
                if (actor.IsOralVoring)
                    return SpritesMain[22];
                return SpritesMain[21];
            default:
                if (actor.IsOralVoring)
                    return SpritesMain[5];
                return SpritesMain[4];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        switch (BodyState)
        {
            case BodyStateType.Third:
                return null;
            case BodyStateType.Second:
                return SpritesMain[20];
            default:
                return SpritesMain[3];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        switch (BodyState)
        {
            case BodyStateType.Third:
                return SpritesMain[38];
            case BodyStateType.Second:
                if (StomachSize > 10)
                {
                    AddOffset(BodyAccent2, 0, -23 * .41666f);
                    return SpritesMain[24];
                }

                return SpritesMain[19];
            default:
                return SpritesMain[9];
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        switch (BodyState)
        {
            case BodyStateType.Third:
                return SpritesMain[34];
            case BodyStateType.Second:
                if (StomachSize > 10)
                    return null;
                return SpritesMain[23];
            default:
                return SpritesMain[2];
        }
    }


    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        switch (BodyState)
        {
            case BodyStateType.Third:
                return SpritesMain[37];
            case BodyStateType.Second:
                return null;
            default:
                return SpritesMain[1];
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        switch (BodyState)
        {
            case BodyStateType.Third:
                return null;
            case BodyStateType.Second:
                return SpritesMain[17];
            default:
                return SpritesMain[8];
        }
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor)
    {
        switch (BodyState)
        {
            case BodyStateType.Third:
                return null;
            case BodyStateType.Second:
                return SpritesMain[18];
            default:
                return SpritesMain[6];
        }
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor)
    {
        switch (BodyState)
        {
            case BodyStateType.Third:
                return null;
            case BodyStateType.Second:
                return null;
            default:
                return SpritesMain[7];
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        if (StomachSize < 7 && BodyState == BodyStateType.Second)
        {
            if (StomachSize < 2)
                return null;
            return SpritesMain[27 + StomachSize];
        }

        if (BodyState == BodyStateType.Third)
        {
            if (StomachSize == 19 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                SetThirdOffset(59 * .416667f);
                return SpritesFrontBelly[12];
            }

            if (StomachSize > 16 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) == false)
                StomachSize = 16;
            if (StomachSize > 11)
                SetThirdOffset(59 * .416667f);
            else
                AddOffset(Belly, 0, -80 * .625f);
            return SpritesFrontBelly[Math.Min(StomachSize - 7, 11)];
        }

        if (StomachSize < 7)
            return SpritesMain[10 + StomachSize];
        else if (StomachSize < 10)
        {
            return SpritesMain[18 + StomachSize];
        }
        else
        {

            if (StomachSize == 19 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                return SpritesBelly[9];
            }

            if (StomachSize == 19 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) == false)
            {
                StomachSize = 18;
            }


            if (StomachSize > 17 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) == false)
                StomachSize = 17;

            return SpritesBelly[StomachSize - 10];
        }

        void SetThirdOffset(float y)
        {
            AddOffset(Head, 0, y);
            AddOffset(BodyAccent2, 0, y);
            AddOffset(BodyAccent3, 0, y);
            AddOffset(BodyAccent4, 0, y);
        }

    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (BodyState != BodyStateType.Second)
            return null;
        return SpritesMain[28];
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (BodyState != BodyStateType.Second)
            return null;
        int ballIndex = actor.GetBallSize(21, 1);
        int ballSprite;
        //int ballSprite = actor.Unit.BodyAccentType2;

        if (actor.Unit.Predator)
        {
            if (ballIndex > 18 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) == false)
                ballIndex = 18;
            if (ballIndex == 21 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) == false)
                ballIndex = 20;
        }




        if (StomachSize >= 10)
            ballSprite = BallsHigh[ballIndex];
        else if (StomachSize >= 7)
            ballSprite = BallsMedium[ballIndex];
        else
            ballSprite = BallsLow[ballIndex];


        if (ballSprite <= 8)
        {

        }
        else if (ballSprite <= 12)
            AddOffset(Balls, 80 * .625f, 0);
        else if (ballSprite <= 31)
            AddOffset(Balls, 80 * .625f, -80 * .625f);
        else if (ballSprite == 32)
            AddOffset(Balls, 160 * .625f, -102.4f * .625f);
        else
            AddOffset(Balls, 160 * .625f, -81.6f * .625f);

        return SpritesBalls[ballSprite];
    }


}

