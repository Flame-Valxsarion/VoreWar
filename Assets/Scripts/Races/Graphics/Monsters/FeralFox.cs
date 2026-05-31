using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class FeralFox : BlankSlate
{
    internal override int BreastSizes => 1;
    internal override int DickSizes => 3;

    public FeralFox()
    {
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.FeralFoxSkin); // Just the one
        GentleAnimation = true;
        WeightGainDisabled = true;
        CanBeGender = new List<Gender>() { Gender.Male, Gender.Female, Gender.Hermaphrodite }; // !!! All genders or just male?
        TailTypes = 10; // Tail
        EyeTypes = 5; // Eyes
		HairStyles = 5; // Heads
        MouthTypes = 10; // Ears

        Body = new SpriteExtraInfo(8, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralFoxSkin, s.Unit.SkinColor)); // Body
        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralFoxSkin, s.Unit.SkinColor)); // Right hind leg
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralFoxSkin, s.Unit.SkinColor)); // Left hind leg
        BodyAccent3 = new SpriteExtraInfo(1, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralFoxSkin, s.Unit.SkinColor)); // Tail
        BodyAccent4 = new SpriteExtraInfo(9, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralFoxSkin, s.Unit.SkinColor)); // Head
        BodyAccent5 = new SpriteExtraInfo(10, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralFoxSkin, s.Unit.SkinColor)); // Ears
		BodyAccent6 = new SpriteExtraInfo(10, BodyAccentSprite6, WhiteColored); // Mouth                                                                            <- Mouth that way
		BodyAccent7 = new SpriteExtraInfo(3, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralFoxSkin, s.Unit.SkinColor)); // Sheath
		Eyes = new SpriteExtraInfo(10, EyesSprite, WhiteColored); // Eyes                                                                         <- Eyes here
        Belly = new SpriteExtraInfo(5, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralFoxSkin, s.Unit.SkinColor)); // Belly
        Dick = new SpriteExtraInfo(4, DickSprite, WhiteColored); // Dick                                                                        <- Dickings
        Balls = new SpriteExtraInfo(2, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralFoxSkin, s.Unit.SkinColor)); // Balls
        // Layer 7 is for belly sizes 9 and up.
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
		
        int temp = State.Rand.Next(50);

        if (temp >= 5)
        {
            unit.EyeType = 0;
        }
        else if (temp >= 4)
        {
            unit.EyeType = 1;
        }
        else if (temp >= 3)
        {
            unit.EyeType = 2;
        }
        else if (temp >= 2)
        {
            unit.EyeType = 3;
        }
        else
        {
            unit.EyeType = 4;
        }
        unit.TailType = State.Rand.Next(10);
    }
	
	internal override void SetBaseOffsets(Actor_Unit actor)
    {  
        int size = actor.GetStomachSize(52);
		
		if (size == 20)
		{
			AddOffset(Balls, -10 * .5f, 3 * .5f);
			AddOffset(Belly, 12 * .5f, -59 * .5f);
			AddOffset(Body, 0, 3 * .5f);
			AddOffset(Eyes, 0, 3 * .5f);
			AddOffset(Dick, 0, 3 * .5f);
			AddOffset(BodyAccent, 0, 3 * .5f);
			AddOffset(BodyAccent2, 0, 3 * .5f);
			AddOffset(BodyAccent3, 0, 3 * .5f);
			AddOffset(BodyAccent4, 0, 3 * .5f);
			AddOffset(BodyAccent5, 0, 3 * .5f);
			AddOffset(BodyAccent6, 0, 3 * .5f);
			AddOffset(BodyAccent7, 0, 3 * .5f);			
		}
		else if (size == 21)
		{
			AddOffset(Balls, -10 * .5f, 7 * .5f);
			AddOffset(Belly, 12 * .5f, -55 * .5f);
			AddOffset(Body, 0, 7 * .5f);
			AddOffset(Eyes, 0, 7 * .5f);
			AddOffset(Dick, 0, 7 * .5f);
			AddOffset(BodyAccent, 0, 7 * .5f);
			AddOffset(BodyAccent2, 0, 7 * .5f);
			AddOffset(BodyAccent3, 0, 7 * .5f);
			AddOffset(BodyAccent4, 0, 7 * .5f);
			AddOffset(BodyAccent5, 0, 7 * .5f);
			AddOffset(BodyAccent6, 0, 7 * .5f);
			AddOffset(BodyAccent7, 0, 7 * .5f);		
		}
		else if (size == 22)
		{
			AddOffset(Balls, -10 * .5f, 12 * .5f);
			AddOffset(Belly, 12 * .5f, -50 * .5f);
			AddOffset(Body, 0, 12 * .5f);
			AddOffset(Eyes, 0, 12 * .5f);
			AddOffset(Dick, 0, 12 * .5f);
			AddOffset(BodyAccent, 0, 12 * .5f);
			AddOffset(BodyAccent2, 0, 12 * .5f);
			AddOffset(BodyAccent3, 0, 12 * .5f);
			AddOffset(BodyAccent4, 0, 12 * .5f);
			AddOffset(BodyAccent5, 0, 12 * .5f);
			AddOffset(BodyAccent6, 0, 12 * .5f);
			AddOffset(BodyAccent7, 0, 12 * .5f);		
		}
		else if (size == 23)
		{
			AddOffset(Balls, -10 * .5f, 17 * .5f);
			AddOffset(Belly, 12 * .5f, -45 * .5f);
			AddOffset(Body, 0, 17 * .5f);
			AddOffset(Eyes, 0, 17 * .5f);
			AddOffset(Dick, 0, 17 * .5f);
			AddOffset(BodyAccent, 0, 17 * .5f);
			AddOffset(BodyAccent2, 0, 17 * .5f);
			AddOffset(BodyAccent3, 0, 17 * .5f);
			AddOffset(BodyAccent4, 0, 17 * .5f);
			AddOffset(BodyAccent5, 0, 17 * .5f);
			AddOffset(BodyAccent6, 0, 17 * .5f);
			AddOffset(BodyAccent7, 0, 17 * .5f);		
		}	
		else if (size == 24)
		{
			AddOffset(Balls, -10 * .5f, 23 * .5f);
			AddOffset(Belly, 12 * .5f, -39 * .5f);
			AddOffset(Body, 0, 23 * .5f);
			AddOffset(Eyes, 0, 23 * .5f);
			AddOffset(Dick, 0, 23 * .5f);
			AddOffset(BodyAccent, 0, 23 * .5f);
			AddOffset(BodyAccent2, 0, 23 * .5f);
			AddOffset(BodyAccent3, 0, 23 * .5f);
			AddOffset(BodyAccent4, 0, 23 * .5f);
			AddOffset(BodyAccent5, 0, 23 * .5f);
			AddOffset(BodyAccent6, 0, 23 * .5f);
			AddOffset(BodyAccent7, 0, 23 * .5f);		
		}	
		else if (size == 25)
		{
			AddOffset(Balls, -10 * .5f, 29 * .5f);
			AddOffset(Belly, 12 * .5f, -33 * .5f);
			AddOffset(Body, 0, 29 * .5f);
			AddOffset(Eyes, 0, 29 * .5f);
			AddOffset(Dick, 0, 29 * .5f);
			AddOffset(BodyAccent, 0, 29 * .5f);
			AddOffset(BodyAccent2, 0, 29 * .5f);
			AddOffset(BodyAccent3, 0, 29 * .5f);
			AddOffset(BodyAccent4, 0, 29 * .5f);
			AddOffset(BodyAccent5, 0, 29 * .5f);
			AddOffset(BodyAccent6, 0, 29 * .5f);
			AddOffset(BodyAccent7, 0, 29 * .5f);		
		}	
		else if (size == 26)
		{
			AddOffset(Balls, -10 * .5f, 35 * .5f);
			AddOffset(Belly, 12 * .5f, -27 * .5f);
			AddOffset(Body, 0, 35 * .5f);
			AddOffset(Eyes, 0, 35 * .5f);
			AddOffset(Dick, 0, 35 * .5f);
			AddOffset(BodyAccent, 0, 35 * .5f);
			AddOffset(BodyAccent2, 0, 35 * .5f);
			AddOffset(BodyAccent3, 0, 35 * .5f);
			AddOffset(BodyAccent4, 0, 35 * .5f);
			AddOffset(BodyAccent5, 0, 35 * .5f);
			AddOffset(BodyAccent6, 0, 35 * .5f);
			AddOffset(BodyAccent7, 0, 35 * .5f);		
		}	
		else if (size == 27)
		{
			AddOffset(Balls, -10 * .5f, 42 * .5f);
			AddOffset(Belly, 12 * .5f, -20 * .5f);
			AddOffset(Body, 0, 42 * .5f);
			AddOffset(Eyes, 0, 42 * .5f);
			AddOffset(Dick, 0, 42 * .5f);
			AddOffset(BodyAccent, 0, 42 * .5f);
			AddOffset(BodyAccent2, 0, 42 * .5f);
			AddOffset(BodyAccent3, 0, 42 * .5f);
			AddOffset(BodyAccent4, 0, 42 * .5f);
			AddOffset(BodyAccent5, 0, 42 * .5f);
			AddOffset(BodyAccent6, 0, 42 * .5f);
			AddOffset(BodyAccent7, 0, 42 * .5f);		
		}	
		else if (size == 28)
		{
			AddOffset(Balls, -10 * .5f, 50 * .5f);
			AddOffset(Belly, 12 * .5f, -12 * .5f);
			AddOffset(Body, 0, 50 * .5f);
			AddOffset(Eyes, 0, 50 * .5f);
			AddOffset(Dick, 0, 50 * .5f);
			AddOffset(BodyAccent, 0, 50 * .5f);
			AddOffset(BodyAccent2, 0, 50 * .5f);
			AddOffset(BodyAccent3, 0, 50 * .5f);
			AddOffset(BodyAccent4, 0, 50 * .5f);
			AddOffset(BodyAccent5, 0, 50 * .5f);
			AddOffset(BodyAccent6, 0, 50 * .5f);
			AddOffset(BodyAccent7, 0, 50 * .5f);		
		}	
		else if (size == 29)
		{
			AddOffset(Balls, -10 * .5f, 58 * .5f);
			AddOffset(Belly, 12 * .5f, -4 * .5f);
			AddOffset(Body, 0, 58 * .5f);
			AddOffset(Eyes, 0, 58 * .5f);
			AddOffset(Dick, 0, 58 * .5f);
			AddOffset(BodyAccent, 0, 58 * .5f);
			AddOffset(BodyAccent2, 0, 58 * .5f);
			AddOffset(BodyAccent3, 0, 58 * .5f);
			AddOffset(BodyAccent4, 0, 58 * .5f);
			AddOffset(BodyAccent5, 0, 58 * .5f);
			AddOffset(BodyAccent6, 0, 58 * .5f);
			AddOffset(BodyAccent7, 0, 58 * .5f);		
		}	
		else if (size >= 30)
		{
			AddOffset(Balls, -10 * .5f, 60 * .5f);
			AddOffset(Belly, 12 * .5f, -2 * .5f);
			AddOffset(Body, 0, 60 * .5f);
			AddOffset(Eyes, 0, 60 * .5f);
			AddOffset(Dick, 0, 60 * .5f);
			AddOffset(BodyAccent, 0, 60 * .5f);
			AddOffset(BodyAccent2, 0, 60 * .5f);
			AddOffset(BodyAccent3, 0, 60 * .5f);
			AddOffset(BodyAccent4, 0, 60 * .5f);
			AddOffset(BodyAccent5, 0, 60 * .5f);
			AddOffset(BodyAccent6, 0, 60 * .5f);
			AddOffset(BodyAccent7, 0, 60 * .5f);	
		}
		else
        {
            AddOffset(Balls, -25 * .5f, 0); // Meant to shift the balls 10 pixels to the left on the sprite sheet. The .5f is the sprite's width dividing 100, which is the default value. So 100 / 200 is 0.5 which turns to .5f (f for Float).
            AddOffset(Belly, 6, -62 * .5f); // Meant to shift the bellies 50 pixels down on the sprite sheet. The .5f is the sprite's width dividing 100, which is the default value. So 100 / 200 is 0.5 which turns to .5f (f for Float).
        }
	}
	
	protected override Sprite BodySprite(Actor_Unit actor) // Body
    {
        if (actor.GetStomachSize(52) >= 20)
            return State.GameManager.SpriteDictionary.FeralFox[3];
        else if (actor.GetStomachSize(52) >= 4)
            return State.GameManager.SpriteDictionary.FeralFox[2];
        else if (actor.GetStomachSize(52) >= 1)
            return State.GameManager.SpriteDictionary.FeralFox[1];
        else
            return State.GameManager.SpriteDictionary.FeralFox[0];
    }
	
    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Right Hind Leg
    {
		if (actor.GetStomachSize(52) >= 20) return State.GameManager.SpriteDictionary.FeralFox[5];
        else return State.GameManager.SpriteDictionary.FeralFox[4];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Left Hind leg
    {
        return State.GameManager.SpriteDictionary.FeralFox[6];
    }
	
    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Tail
    { 
        return State.GameManager.SpriteDictionary.FeralFox[50 + actor.Unit.TailType];
    }
	
	protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Head
    { 
        if (actor.IsOralVoring || actor.IsAttacking) return State.GameManager.SpriteDictionary.FeralFox[25 + actor.Unit.HairStyle];
		else return State.GameManager.SpriteDictionary.FeralFox[20 + actor.Unit.HairStyle];    
    }
	
	protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Ears
    { 
        return State.GameManager.SpriteDictionary.FeralFox[30 + actor.Unit.MouthType];
    }
	
    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Mouth
    {
		if (actor.IsOralVoring || actor.IsAttacking) return State.GameManager.SpriteDictionary.FeralFox[9];
		else return State.GameManager.SpriteDictionary.FeralFox[8];        
    }
	
	protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Sheath
    {
		if (actor.Unit.DickSize < 0) return null;
        if (Config.HideCocks) return null;

		else return State.GameManager.SpriteDictionary.FeralFox[7];
    }
	
	protected override Sprite EyesSprite(Actor_Unit actor) // Eyes
	{
        if (actor.IsOralVoring || actor.IsAttacking) return State.GameManager.SpriteDictionary.FeralFox[15 + actor.Unit.EyeType];
		else return State.GameManager.SpriteDictionary.FeralFox[10 + actor.Unit.EyeType]; 
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.GetStomachSize(52) >= 9) // I went for having two stomach sizes for each Selicia sprite to stop them from passing so fast during digestion, hence a number larger than the amount of sprites.
        {
            Belly.layer = 7;
        }
        else
        {
            Belly.layer = 5;
        }

        if (!actor.HasBelly)
            return null;

        int size = actor.GetStomachSize(52);

        if ( size >= 52 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralFox[96];
        }

        if (size >= 49 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralFox[95];
        }

        if (size >= 46 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralFox[94];
        }

        if (size >= 43 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralFox[93];
        }

        if (size >= 40 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralFox[92];
        }
		
		if (size >= 37 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralFox[91];
        }
		
		if (size >= 34 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralFox[90];
        }

        if (size > 29) size = 29;

        return State.GameManager.SpriteDictionary.FeralFox[60 + size];
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.DickSize < 0) return null;
        if (Config.HideCocks) return null;

		if (actor.IsErect()) return State.GameManager.SpriteDictionary.FeralFox[97 + actor.Unit.DickSize];

        return null;
    }
    
    protected override Sprite BallsSprite(Actor_Unit actor)
    {
		if (actor.Unit.HasDick == false || Config.HideCocks) return null;
		
        if (actor.Unit.HasDick == false || Config.HideCocks || actor.PredatorComponent?.BallsFullness == 0)
		{
			return null;
        }

        int size = actor.GetBallSize(52);

        if (size >= 52 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralFox[139];
        }

        if (size >= 49 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralFox[138];
        }

        if (size >= 46 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralFox[137];
        }

        if (size >= 43 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralFox[136];
        }

        if (size >= 40 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralFox[135];
        }

        if (size >= 37 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralFox[134];
        }
		
		if (size >= 34 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.FeralFox[133];
        }

        if (size > 29) size = 29;
        return State.GameManager.SpriteDictionary.FeralFox[103 + size];
    }
}