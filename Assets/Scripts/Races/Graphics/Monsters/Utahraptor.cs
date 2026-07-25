using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Utahraptor : BlankSlate
{
    readonly Sprite[] Utahraptor200Sprites = SpriteDictionary.Utahraptor200;
    readonly Sprite[] Utahraptor240ASprites = SpriteDictionary.Utahraptor240A;
    readonly Sprite[] Utahraptor240BSprites = SpriteDictionary.Utahraptor240B;
    bool SkapaFrontSide;
    bool SkapaFrontDirect;

    public Utahraptor()
    {
        CanBeGender = new List<Gender>() { Gender.Male, Gender.Female, Gender.Hermaphrodite };
        GentleAnimation = true;
        WeightGainDisabled = true;

        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.TrexSkin);
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.TrexSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.RaijuSkin);

        Body = new SpriteExtraInfo(12, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Chest
        Head = new SpriteExtraInfo(20, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Head
        Eyes = new SpriteExtraInfo(18, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.EyeColor)); // Eyes
        Mouth = new SpriteExtraInfo(19, MouthSprite, WhiteColored); // Mouth
        BodyAccent = new SpriteExtraInfo(13, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.ExtraColor1)); // Body Patterns
		BodyAccent2 = new SpriteExtraInfo(3, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Left Leg
		BodyAccent3 = new SpriteExtraInfo(1, BodyAccentSprite3, WhiteColored); // Left Leg Claws
        BodyAccent4 = new SpriteExtraInfo(4, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Left Arm
        BodyAccent5 = new SpriteExtraInfo(2, BodyAccentSprite5, WhiteColored); // Left Arm Claws
        BodyAccent6 = new SpriteExtraInfo(23, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Right Arm
        BodyAccent7 = new SpriteExtraInfo(22, BodyAccentSprite7, WhiteColored); // Right Arm Claws
        BodyAccent8 = new SpriteExtraInfo(9, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Belly Cover
        BodyAccent9 = new SpriteExtraInfo(7, BodyAccentSprite9, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Sheath
        BodyAccent10 = new SpriteExtraInfo(5, BodyAccentSprite10, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Crotch
        BodyAccessory = new SpriteExtraInfo(17, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Genitals Outsides
        SecondaryAccessory = new SpriteExtraInfo(16, SecondaryAccessorySprite, WhiteColored); // Genitals Insides
		Hair = new SpriteExtraInfo(11, HairSprite, WhiteColored); // Right Leg Claws
		Hair2 = new SpriteExtraInfo(21, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.ExtraColor1)); //Head Patterns
        Balls = new SpriteExtraInfo(6, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Balls		
        Dick = new SpriteExtraInfo(8, DickSprite, WhiteColored); // Penis
        Belly = new SpriteExtraInfo(10, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Belly

        BodyAccentTypes1 = 6;
        TailTypes = 2;
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
	    unit.ExtraColor1 =  unit.SkinColor;
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        int size = actor.GetStomachSize(59);
        int ballsSize = actor.GetBallSize(59);

        if (actor.Unit.TailType == 1)
        {
            SkapaFrontSide = false;
            SkapaFrontDirect = false;
        }
        else if (size < 12 && ballsSize < 12) // FrontSide
        {
            SkapaFrontSide = true;
            SkapaFrontDirect = false;
	    }
        else if (size >= 12 && ballsSize == 0) // DirectFront
        {
            SkapaFrontDirect = true;
            SkapaFrontSide = false;
	    }
        else // BackView
        {
	        SkapaFrontSide = false;
            SkapaFrontDirect = false;
        }            
    }

    internal override void SetBaseOffsets(Actor_Unit actor) // Offsets
    {
        int sizeBalls = actor.GetBallSize(59);
        int sizeBelly = actor.GetStomachSize(59);

        if (SkapaFrontSide)  // FrontSide
        {
            AddOffset(Balls, 0, 0 * .625f);
            AddOffset(Belly, 0, 0 * .625f);
        }
        else if (SkapaFrontDirect)  // DirectFront
        {
            AddOffset(Balls, 0, -50 * .625f);
            if (sizeBelly >= 12) AddOffset(Belly, 0, -50 * .625f);
            else AddOffset(Belly, 0, 0 * .625f);
        }
        else // BackView
        {
			if (actor.Unit.TailType == 1) AddOffset(Balls, 0, -50 * .625f);
			else if (sizeBalls > 12 || (!SkapaFrontSide && !SkapaFrontDirect)) AddOffset(Balls, 0, -50 * .625f);
            if (sizeBelly >= 12 || (!SkapaFrontSide && !SkapaFrontDirect)) AddOffset(Belly, 0, -50 * .625f);
            else AddOffset(Belly, 0, 0 * .625f);
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (SkapaFrontSide)  // FrontSide
        {
            if (actor.IsAttacking) return Utahraptor200Sprites[7];
            if (actor.IsOralVoring || actor.IsCockVoring || actor.IsUnbirthing) return Utahraptor200Sprites[8];
            return Utahraptor200Sprites[6];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            if (actor.IsAttacking) return Utahraptor240ASprites[9];
            if (actor.IsOralVoring || actor.IsCockVoring || actor.IsUnbirthing) return Utahraptor240ASprites[10];
            return Utahraptor240ASprites[8];
        }
        else // BackView
        {
            if (actor.IsAttacking) return Utahraptor240ASprites[12];
            if (actor.IsOralVoring || actor.IsCockVoring || actor.IsUnbirthing) return Utahraptor240ASprites[13];
            return Utahraptor240ASprites[11];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Body Patterns
    {
        if (SkapaFrontSide)  // FrontSide
        {
            if (actor.Unit.BodyAccentType1 == 0) return null;
		    return Utahraptor200Sprites[52 + actor.Unit.BodyAccentType1];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            return null;
        }
    }
	
    protected override Sprite HairSprite2(Actor_Unit actor) // Head Patterns
    {
        if (SkapaFrontSide)  // FrontSide
        {
            if (actor.Unit.BodyAccentType1 == 0) return null;
			return Utahraptor200Sprites[57 + actor.Unit.BodyAccentType1];

            if (actor.IsOralVoring || actor.IsAttacking) 
		    {
                return Utahraptor200Sprites[62 + actor.Unit.BodyAccentType1];
            }
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            return null;
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor) // Mouth
    {
        if (SkapaFrontSide)  // FrontSide
        {
            if (actor.IsAttacking) return Utahraptor200Sprites[16];
            if (actor.IsOralVoring || actor.IsCockVoring || actor.IsUnbirthing) return Utahraptor200Sprites[17];
            return Utahraptor200Sprites[15];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            if (actor.IsAttacking) return Utahraptor240ASprites[19];
            if (actor.IsOralVoring || actor.IsCockVoring || actor.IsUnbirthing) return Utahraptor240ASprites[20];
            return Utahraptor240ASprites[18];
        }
        else // BackView
        {
            if (actor.IsAttacking) return Utahraptor240ASprites[22];
            if (actor.IsOralVoring || actor.IsCockVoring || actor.IsUnbirthing) return Utahraptor240ASprites[23];
            return Utahraptor240ASprites[21];
        }
    }

    protected override Sprite EyesSprite(Actor_Unit actor) // Eyes
    {
        if (SkapaFrontSide)  // FrontSide
        {
            if (actor.IsAttacking || actor.IsOralVoring || actor.IsCockVoring || actor.IsUnbirthing) return Utahraptor200Sprites[19];
            return Utahraptor200Sprites[18];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            if (actor.IsAttacking || actor.IsOralVoring || actor.IsCockVoring || actor.IsUnbirthing) return Utahraptor240ASprites[15];
            return Utahraptor240ASprites[14];
        }
        else // BackView
        {
            if (actor.IsAttacking || actor.IsOralVoring || actor.IsCockVoring || actor.IsUnbirthing) return Utahraptor240ASprites[17];
            return Utahraptor240ASprites[16];
        }
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {
        int sizeBalls = actor.GetBallSize(59);
        int sizeBelly = actor.GetStomachSize(59);

        if (SkapaFrontSide)  // FrontSide
        {
            return Utahraptor200Sprites[0];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            if (actor.IsAttacking) return Utahraptor240ASprites[1];
            return Utahraptor240ASprites[0];
        }
        else // BackView
        {
            if (sizeBelly >= 12) return Utahraptor240ASprites[3];
            return Utahraptor240ASprites[2];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Left Leg
    {
        if (SkapaFrontSide)  // FrontSide
        {
            return Utahraptor200Sprites[5];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Left Leg Claws
    {
        if (SkapaFrontSide)  // FrontSide
        {
            return Utahraptor200Sprites[14];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Left Arm
    {
        if (SkapaFrontSide)  // FrontSide
        {
            if (actor.IsAttacking) return Utahraptor200Sprites[4];
            return Utahraptor200Sprites[3];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Left Arm Claws
    {
        if (SkapaFrontSide)  // FrontSide
        {
            if (actor.IsAttacking) return Utahraptor200Sprites[13];
            return Utahraptor200Sprites[12];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Right Arm
    {
        if (SkapaFrontSide)  // FrontSide
        {
            if (actor.IsAttacking) return Utahraptor200Sprites[2];
            return Utahraptor200Sprites[1];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Right Arm Claws
    {
        int sizeBalls = actor.GetBallSize(59);
        int sizeBelly = actor.GetStomachSize(59);

        if (SkapaFrontSide)  // FrontSide
        {
            if (actor.IsAttacking) return Utahraptor200Sprites[11];
            return Utahraptor200Sprites[10];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            if (actor.IsAttacking) return Utahraptor240ASprites[5];
            return Utahraptor240ASprites[4];
        }
        else // BackView
        {
			if (sizeBalls >= 12) return null;
            if (sizeBelly >= 12) return Utahraptor240ASprites[7];
            return Utahraptor240ASprites[6];
        }
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // Belly Cover
    {
        if (SkapaFrontSide)  // FrontSide
        {
            if (actor.HasBelly == false) return Utahraptor200Sprites[20];
		    return null;
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite9(Actor_Unit actor) // Sheath
    {
        if (SkapaFrontSide)  // FrontSide
        {
			if (actor.Unit.HasDick == false && actor.Unit.HasBreasts == true)
			{
			    if (actor.IsUnbirthing) return Utahraptor200Sprites[24];
                return Utahraptor200Sprites[23];
			}
			if (actor.Unit.HasDick == true && actor.Unit.HasBreasts == false)
			{
                if (!actor.IsErect() && !actor.IsCockVoring) return Utahraptor200Sprites[22];
                return null;
			}
			else
			{
                if (!actor.IsErect() && !actor.IsCockVoring) return Utahraptor200Sprites[22];
                return null;
			}
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite10(Actor_Unit actor) // Crotch
    {
        if (SkapaFrontSide)  // FrontSide
        {
            return Utahraptor200Sprites[21];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            return null;
        }
    }
	
    protected override Sprite AccessorySprite(Actor_Unit actor) // Genitals Outsides
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
			if (actor.Unit.HasDick == false && actor.Unit.HasBreasts == true)
			{
                if (actor.IsAnalVoring) return Utahraptor240ASprites[26];
			    if (actor.IsUnbirthing) return Utahraptor240ASprites[25];
                return Utahraptor240ASprites[24];
			}
			if (actor.Unit.HasDick == true && actor.Unit.HasBreasts == false)
			{
                if (actor.IsAnalVoring) return Utahraptor240ASprites[28];
                return Utahraptor240ASprites[27];
			}
			else
			{
                if (actor.IsAnalVoring) return Utahraptor240ASprites[26];
			    if (actor.IsUnbirthing) return Utahraptor240ASprites[25];
                return Utahraptor240ASprites[24];
			}
        }
    }

    protected override Sprite HairSprite(Actor_Unit actor) // Genitals Insides
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
			if (actor.Unit.HasDick == false && actor.Unit.HasBreasts == true)
			{
                if (actor.IsAnalVoring) return Utahraptor240ASprites[31];
			    if (actor.IsUnbirthing) return Utahraptor240ASprites[30];
                return Utahraptor240ASprites[29];
			}
			if (actor.Unit.HasDick == true && actor.Unit.HasBreasts == false)
			{
                if (actor.IsAnalVoring) return Utahraptor240ASprites[33];
                return Utahraptor240ASprites[32];
			}
			else
			{
                if (actor.IsAnalVoring) return Utahraptor240ASprites[31];
			    if (actor.IsUnbirthing) return Utahraptor240ASprites[30];
                return Utahraptor240ASprites[29];
			}
        }
    }

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) // Right Leg Claws
    {
        if (SkapaFrontSide)  // FrontSide
        {
            return Utahraptor200Sprites[9];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            return null;
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // Belly
    {
        int size = actor.GetStomachSize(59);
        int ballsSize = actor.GetBallSize(59);

        if (!actor.HasBelly) return null;

        if (SkapaFrontSide)  // FrontSide
        {
            if (actor.HasBelly) return Utahraptor200Sprites[40 + size];
            return null;
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            if (size >= 59 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[80];
            }
            else if (size >= 56 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[79];
            }
            else if (size >= 53 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[78];
            }
            else if (size >= 50 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[77];
            }
            else if (size >= 47 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[76];
            }
            else if (size >= 44 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[75];
            }
            else if (size >= 41 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[74];
            }
            else if (size >= 38 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[73];
            }
            else if (size >= 35 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[72];
            }
            else if (size >= 32 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[71];
            }
			if (size >= 29) size = 29;
            return Utahraptor240BSprites[41 + size];
        }
        else // BackView
        {
            if (size >= 59 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[120];
            }
            else if (size >= 56 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[119];
            }
            else if (size >= 53 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[118];
            }
            else if (size >= 50 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[117];
            }
            else if (size >= 47 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[116];
            }
            else if (size >= 44 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[115];
            }
            else if (size >= 41 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[114];
            }
            else if (size >= 38 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[113];
            }
            else if (size >= 35 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[112];
            }
            else if (size >= 32 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
            {
            return Utahraptor240BSprites[111];
            }
            if (size >= 29) size = 29;
            return Utahraptor240BSprites[81 + size];
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor) // Dick
    {
        Dick.layer = 8;
        int size = actor.GetStomachSize(59);
        int ballsSize = actor.GetBallSize(59);

        if (Config.HideCocks || actor.Unit.DickSize < 0) return null;

        if (SkapaFrontSide)  // FrontSide
        {
			if (actor.IsCockVoring) return Utahraptor200Sprites[26];
			return Utahraptor200Sprites[25];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
            Dick.layer = 14;
			if (actor.IsCockVoring) return Utahraptor240ASprites[35];
			return Utahraptor240ASprites[34];
        }
    }

    protected override Sprite BallsSprite(Actor_Unit actor) // Balls
    {
        if (Config.HideCocks || actor.Unit.DickSize < 0) return null;
        Balls.layer = 6;
        int ballsSize = actor.GetBallSize(59);
        int size = actor.GetStomachSize(59);
        if (SkapaFrontSide)  // FrontSide
        {
            Balls.layer = 6;
            if (ballsSize < 12 && size < 12)
            {
                return Utahraptor200Sprites[28 + ballsSize];
            }
            return Utahraptor200Sprites[27];
        }
        if (SkapaFrontDirect)  // DirectFront
        {
            return null;
        }
        else // BackView
        {
			Balls.layer = 15;
            if (ballsSize > 0)
            {
                if (ballsSize >= 59 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    return Utahraptor240BSprites[40];
                }
                else if (ballsSize >= 56 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    return Utahraptor240BSprites[39];
                }
                else if (ballsSize >= 53 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    return Utahraptor240BSprites[38];
                }
                else if (ballsSize >= 50 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    return Utahraptor240BSprites[37];
                }
                else if (ballsSize >= 47 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    return Utahraptor240BSprites[36];
                }
                else if (ballsSize >= 44 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    return Utahraptor240BSprites[35];
                }
                else if (ballsSize >= 41 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    return Utahraptor240BSprites[34];
                }
                else if (ballsSize >= 38 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    return Utahraptor240BSprites[33];
                }
                else if (ballsSize >= 35 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    return Utahraptor240BSprites[32];
                }
                else if (ballsSize >= 32 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    return Utahraptor240BSprites[31];
                }
                if (ballsSize > 29) ballsSize = 29;
                return Utahraptor240BSprites[1 + ballsSize];
            }
			return Utahraptor240BSprites[0];
        }
    }
}