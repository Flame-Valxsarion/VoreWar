using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Skapa : BlankSlate
{
    readonly Sprite[] SkapaSprites = SpriteDictionary.Skapa;
    bool SkapaFrontSide;
    bool SkapaFrontDirect;

    public Skapa()
    {
        CanBeGender = new List<Gender>() { Gender.Hermaphrodite };
        GentleAnimation = true;
        WeightGainDisabled = true;

        Head = new SpriteExtraInfo(6, HeadSprite, WhiteColored); // Head
        Body = new SpriteExtraInfo(5, BodySprite, WhiteColored); // Upper Body
        BodyAccent = new SpriteExtraInfo(0, BodyAccentSprite, WhiteColored); // Lower Body
        BodyAccent2 = new SpriteExtraInfo(8, BodyAccentSprite2, WhiteColored); // Crotch
        Belly = new SpriteExtraInfo(3, null, WhiteColored); // Belly
        Dick = new SpriteExtraInfo(2, DickSprite, WhiteColored); // Dick
        Balls = new SpriteExtraInfo(1, BallsSprite, WhiteColored); // Balls

        TailTypes = 2;
    }
    
    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Skapa";
        unit.TailType = (0);
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        int size = actor.GetStomachSize(45);
        int ballsSize = actor.GetBallSize(45);

        SkapaFrontSide = false;
        SkapaFrontDirect = false;
        actor.Unit.DickSize = 0;//Forces Hermaphrodite regurardless of herm spawn percentages
        actor.Unit.SetDefaultBreastSize(0);//Forces Hermaphrodite regurardless of herm spawn percentages
        actor.Unit.HasVagina = Config.HermsCanUB;//Forces Hermaphrodite regurardless of herm spawn percentages
        actor.Unit.Pronouns = new List<string> { "she", "her", "her", "hers", "herself", "singular" };//ensures regurardless of gender Pronouns remain the same

        if (actor.Unit.TailType == 1)
        {
            SkapaFrontSide = false;
            SkapaFrontDirect = false;
        }
        else if (size < 10 && ballsSize < 5)  // FrontSide
        {
            SkapaFrontSide = true;
            SkapaFrontDirect = false;
        }
        else if (size >= 10 && ballsSize == 0)  // DirectFront
        {
            SkapaFrontDirect = true;
            SkapaFrontSide = false;
        }
        else // BackView
        {
        }            
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Belly, 0, 0 * .5f);
        AddOffset(Balls, 0, 0 * .5f);
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (SkapaFrontSide)  // FrontSide
        {
            if (actor.IsAttacking) return SkapaSprites[1];
            if (actor.IsOralVoring || actor.IsCockVoring || actor.IsUnbirthing) return SkapaSprites[2];
            return SkapaSprites[0];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            if (actor.IsAttacking) return SkapaSprites[4];
            if (actor.IsOralVoring || actor.IsCockVoring || actor.IsUnbirthing) return SkapaSprites[5];
            return SkapaSprites[3];
        }
        else // BackView
        {
            if (actor.IsAttacking) return SkapaSprites[7];
            if (actor.IsOralVoring || actor.IsCockVoring || actor.IsUnbirthing) return SkapaSprites[8];
            return SkapaSprites[6];
        }
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Upper Body
    {
        int size = actor.GetStomachSize(45);
        if (SkapaFrontSide)  // FrontSide
        {
            return SkapaSprites[9];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            if (size < 10) return SkapaSprites[10];
            return SkapaSprites[11];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Lower Body
    {
        if (SkapaFrontSide)  // FrontSide
        {
            return SkapaSprites[12];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            return SkapaSprites[13];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Crotch
    {
        if (SkapaFrontSide)  // FrontSide
        {
            return null;
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            if (actor.IsUnbirthing) return SkapaSprites[15];
            if (actor.IsAnalVoring) return SkapaSprites[16];
            return SkapaSprites[14];
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // Belly
    {
        int size = actor.GetStomachSize(45);
        int ballsSize = actor.GetBallSize(45);
        int resize = actor.GetStomachSize(5);

        if (!actor.HasBelly) return null;

        if (SkapaFrontSide)  // FrontSide
        {
            if (size > 0) return SkapaSprites[30 + size];
            return null;
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                AddOffset(Belly, 0, -5 * .5f);
                if (size >= 45) return SkapaSprites[53];
            }
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.womb))
            {
                AddOffset(Belly, 0, -5 * .5f);
                if (size >= 45) return SkapaSprites[53];
            }
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                if (size >= 44)
                {
                    AddOffset(Belly, 0, -5 * .5f);
                    return SkapaSprites[52];
                }

                if (size >= 40)
                {
                    AddOffset(Belly, 0, -5 * .5f);
                    return SkapaSprites[51];
                }
                if (size >= 36)
                {
                    AddOffset(Belly, 0, -5 * .5f);
                    return SkapaSprites[50];
                }
                if (size >= 32)
                {
                    AddOffset(Belly, 0, -5 * .5f);
                    return SkapaSprites[49];
                }
                if (size >= 28)
                {
                    AddOffset(Belly, 0, -5 * .5f);
                    return SkapaSprites[48];
                }
                if (size >= 24)
                {
                    AddOffset(Belly, 0, -5 * .5f);
                    return SkapaSprites[47];
                }
                if (size >= 20)
                {
                    AddOffset(Belly, 0, -5 * .5f);
                    return SkapaSprites[46];
                }
            }
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.womb))
            {
                if (size >= 44)
                {
                    AddOffset(Belly, 0, -5 * .5f);
                    return SkapaSprites[52];
                }

                if (size >= 40)
                {
                    AddOffset(Belly, 0, -5 * .5f);
                    return SkapaSprites[51];
                }
                if (size >= 36)
                {
                    AddOffset(Belly, 0, -5 * .5f);
                    return SkapaSprites[50];
                }
                if (size >= 32)
                {
                    AddOffset(Belly, 0, -5 * .5f);
                    return SkapaSprites[49];
                }
                if (size >= 28)
                {
                    AddOffset(Belly, 0, -5 * .5f);
                    return SkapaSprites[48];
                }
                if (size >= 24)
                {
                    AddOffset(Belly, 0, -5 * .5f);
                    return SkapaSprites[47];
                }
                if (size >= 20)
                {
                    AddOffset(Belly, 0, -5 * .5f);
                    return SkapaSprites[46];
                }
            }

            //if (size >= 15) size = 15;
            if (size >= 15) AddOffset(Belly, 0, -5 * .5f);
            else AddOffset(Belly, 0, 0 * .5f);
            return SkapaSprites[40 + resize];
        }
        else // BackView
        {
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                if (size >= 45)
                {
                    AddOffset(Belly, 0, -20 * .5f);
                    return SkapaSprites[77];
                }
            }
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.womb))
            {
                if (size >= 45)
                {
                    AddOffset(Belly, 0, -20 * .5f);
                    return SkapaSprites[77];
                }
            }
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {

                if (size >= 44)
                {
                    AddOffset(Belly, 0, -20 * .5f);
                    return SkapaSprites[76];
                }
                if (size >= 40)
                {
                    AddOffset(Belly, 0, -20 * .5f);
                    return SkapaSprites[75];
                }
                if (size >= 36)
                {
                    AddOffset(Belly, 0, -20 * .5f);
                    return SkapaSprites[74];
                }
                if (size >= 32)
                {
                    AddOffset(Belly, 0, -20 * .5f);
                    return SkapaSprites[73];
                }
                if (size >= 28)
                {
                    AddOffset(Belly, 0, -20 * .5f);
                    return SkapaSprites[72];
                }
                if (size >= 24)
                {
                    AddOffset(Belly, 0, -20 * .5f);
                    return SkapaSprites[71];
                }
                if (size >= 20)
                {
                    AddOffset(Belly, 0, -20 * .5f);
                    return SkapaSprites[70];
                }
            }
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.womb))
            {
                AddOffset(Belly, 0, -20 * .5f);
                if (size >= 44) return SkapaSprites[76];
                if (size >= 40) return SkapaSprites[75];
                if (size >= 36) return SkapaSprites[74];
                if (size >= 32) return SkapaSprites[73];
                if (size >= 28) return SkapaSprites[72];
                if (size >= 24) return SkapaSprites[71];
                if (size >= 20) return SkapaSprites[70];
            }
            if (size >= 15) AddOffset(Belly, 0, -20 * .5f);
            else if (size == 14) AddOffset(Belly, 0, -15 * .5f);
            else if (size == 13) AddOffset(Belly, 0, -10 * .5f);
            else if (size == 12) AddOffset(Belly, 0, -5 * .5f);
            else AddOffset(Belly, 0, 0 * .5f);
            if (size >= 15) size = 15;
            return SkapaSprites[54 + size];
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor) // Dick
    {
        Dick.layer = 2;
        int size = actor.GetStomachSize(45);
        int ballsSize = actor.GetBallSize(45);

        if (!actor.IsErect() && !actor.IsCockVoring) return null;

        if (SkapaFrontSide)  // FrontSide
        {
            if (size == 0)
            {
                if (actor.IsCockVoring)
                {
                    if (ballsSize <= 1) return SkapaSprites[20];
                    if (ballsSize == 2) return SkapaSprites[21];
                    return SkapaSprites[22];
                }
                else
                {
                    if (ballsSize <= 1) return SkapaSprites[17];
                    if (ballsSize == 2) return SkapaSprites[18];
                    return SkapaSprites[19];
                }
            }
            else
            {
                if (actor.IsCockVoring)
                {
                    if (ballsSize <= 1) return SkapaSprites[26];
                    if (ballsSize == 2) return SkapaSprites[27];
                    return SkapaSprites[28];
                }
                else
                {
                    if (ballsSize <= 1) return SkapaSprites[23];
                    if (ballsSize == 2) return SkapaSprites[24];
                    return SkapaSprites[25];
                }
            }
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            Dick.layer = 4;
            return SkapaSprites[29];
        }
    }

    protected override Sprite BallsSprite(Actor_Unit actor) // Balls
    {
        Balls.layer = 1;
        int ballsSize = actor.GetBallSize(45);
        if (SkapaFrontSide)  // FrontSide
        {
            if (ballsSize > 0)
            {
                return SkapaSprites[79 + ballsSize];
            }
            return SkapaSprites[78];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            Balls.layer = 8;
            if (ballsSize > 0)
            {
                if (ballsSize >= 45 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    AddOffset(Balls, 0, -35 * .5f);
                    return SkapaSprites[108];
                }

                if (ballsSize >= 44 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    AddOffset(Balls, 0, -35 * .5f);
                    return SkapaSprites[107];
                }
                if (ballsSize >= 40 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    AddOffset(Balls, 0, -35 * .5f);
                    return SkapaSprites[106];
                }
                if (ballsSize >= 36 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    AddOffset(Balls, 0, -35 * .5f);
                    return SkapaSprites[105];
                }
                if (ballsSize >= 32 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    AddOffset(Balls, 0, -35 * .5f);
                    return SkapaSprites[104];
                }
                if (ballsSize >= 28 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    AddOffset(Balls, 0, -35 * .5f);
                    return SkapaSprites[103];
                }
                if (ballsSize >= 24 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    AddOffset(Balls, 0, -35 * .5f);
                    return SkapaSprites[102];
                }
                if (ballsSize >= 20 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    AddOffset(Balls, 0, -35 * .5f);
                    return SkapaSprites[101];
                }

                if (ballsSize >= 15) AddOffset(Balls, 0, -35 * .5f);
                else if (ballsSize == 14) AddOffset(Balls, 0, -30 * .5f);
                else if (ballsSize == 13) AddOffset(Balls, 0, -25 * .5f);
                else if (ballsSize == 12) AddOffset(Balls, 0, -20 * .5f);
                else if (ballsSize == 11) AddOffset(Balls, 0, -15 * .5f);
                else if (ballsSize == 10) AddOffset(Balls, 0, -10 * .5f);
                else AddOffset(Balls, 0, 0 * .5f);
                if (ballsSize > 15) ballsSize = 15;
                return SkapaSprites[85 + ballsSize];
            }
            return SkapaSprites[84];
        }
    }
}