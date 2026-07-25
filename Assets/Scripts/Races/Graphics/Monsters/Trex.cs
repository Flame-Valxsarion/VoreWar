using System.Collections.Generic;
using UnityEngine;

class Trex : BlankSlate
{
    public Trex()
    {
        GentleAnimation = true;
        WeightGainDisabled = true;
        CanBeGender = new List<Gender>() { Gender.Female, Gender.Male };

        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.TrexSkin);
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.TrexSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.RaijuSkin);

        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.ExtraColor1)); // Tail Top
        BodyAccent2 = new SpriteExtraInfo(2, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Tail Bottom
        BodyAccent3 = new SpriteExtraInfo(12, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.ExtraColor1)); // Right Leg
        BodyAccent4 = new SpriteExtraInfo(11, BodyAccentSprite4, WhiteColored); // Right Leg Claws
        BodyAccent5 = new SpriteExtraInfo(14, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.ExtraColor1)); // Right Arm
        BodyAccent6 = new SpriteExtraInfo(13, BodyAccentSprite6, WhiteColored); // Right Arm Claws
        BodyAccent7 = new SpriteExtraInfo(8, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Belly Cover
        BodyAccent8 = new SpriteExtraInfo(1, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.ExtraColor1)); // Left Side
        BodyAccent9 = new SpriteExtraInfo(0, BodyAccentSprite9, WhiteColored); // Left Side Claws
        BodyAccent10 = new SpriteExtraInfo(6, BodyAccentSprite10, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Sheath
        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, WhiteColored); // Sheath Insides
        Body = new SpriteExtraInfo(10, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Chest
        Balls = new SpriteExtraInfo(4, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Balls		
        Dick = new SpriteExtraInfo(7, DickSprite, WhiteColored); // Penis
        Belly = new SpriteExtraInfo(9, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.SkinColor)); // Belly
        Head = new SpriteExtraInfo(15, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.ExtraColor1)); // Head
        Mouth = new SpriteExtraInfo(16, MouthSprite, WhiteColored); // Mouth
        Eyes = new SpriteExtraInfo(17, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TrexSkin, s.Unit.EyeColor)); // Eyes

    }

    internal override void RunFirst(Actor_Unit actor)
    {
        float scaleMod = actor.Unit.GetScale()*1.3f;
        actor.UnitSprite.GraphicsFolder.transform.localScale = new Vector3(scaleMod, scaleMod, 1); // Embiggify!
    }

    internal override void SetBaseOffsets(Actor_Unit actor) // Offsets
    {
        AddOffset(Eyes, -20, 50 * .625f);
        AddOffset(Head, -20, 50 * .625f);
        AddOffset(Mouth, -20, 50 * .625f);
        AddOffset(Body, -20, 50 * .625f);
        AddOffset(BodyAccent, -20, 50 * .625f);
        AddOffset(BodyAccent2, -20, 50 * .625f);
        AddOffset(BodyAccent3, -20, 50 * .625f);
        AddOffset(BodyAccent4, -20, 50 * .625f);
        AddOffset(BodyAccent5, -20, 50 * .625f);
        AddOffset(BodyAccent6, -20, 50 * .625f);
        AddOffset(BodyAccent7, -20, 50 * .625f);
        AddOffset(BodyAccent8, -20, 50 * .625f);
        AddOffset(BodyAccent9, -20, 50 * .625f);
        AddOffset(BodyAccent10, -20, 50 * .625f);
        AddOffset(BodyAccessory, -20, 50 * .625f);
        AddOffset(Belly, 10, 50 * .625f);
        AddOffset(Dick, -20, 50 * .625f);
        AddOffset(Balls, -20, 50 * .625f);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
	    unit.ExtraColor1 = unit.SkinColor;
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Tail Top
    {
        int size = actor.GetBallSize(54);

		if (actor.Unit.BallsSize >= 24) return SpriteDictionary.Trex[26];
		
		else if (actor.Unit.BallsSize >= 15) return SpriteDictionary.Trex[25];
       
 	    else return SpriteDictionary.Trex[24];
    }
	
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Tail Bottom
    {
        int size = actor.GetBallSize(54);

		if (actor.Unit.BallsSize >= 24) return SpriteDictionary.Trex[29];
		
		else if (actor.Unit.BallsSize >= 15) return SpriteDictionary.Trex[28];
        
		else return SpriteDictionary.Trex[27];
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (actor.IsOralVoring) return SpriteDictionary.Trex[14];
		if (actor.IsAttacking) return SpriteDictionary.Trex[13];
		return SpriteDictionary.Trex[12];
    }

    protected override Sprite MouthSprite(Actor_Unit actor) // Mouth
    {
        if (actor.IsOralVoring) return SpriteDictionary.Trex[17];
		if (actor.IsAttacking) return SpriteDictionary.Trex[16];
		return SpriteDictionary.Trex[15];
    }

    protected override Sprite EyesSprite(Actor_Unit actor) // Eyes
    {
        if (actor.IsOralVoring) return SpriteDictionary.Trex[109];
		if (actor.IsAttacking) return SpriteDictionary.Trex[108];
		return SpriteDictionary.Trex[107];
    }

    protected override Sprite DickSprite(Actor_Unit actor) // Dick
    {
        if (actor.Unit.DickSize < 0 || Config.HideCocks) return null;

        if (actor.IsCockVoring) return SpriteDictionary.Trex[19];
        if (actor.IsErect()) return SpriteDictionary.Trex[18];
        return null;
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Chest
    {
        return SpriteDictionary.Trex[1];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Right Leg
    {
        return SpriteDictionary.Trex[0];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Right Leg Claws
    {
        return SpriteDictionary.Trex[2];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Right Arm
    {
        if (actor.IsAttacking) return SpriteDictionary.Trex[4];
		return SpriteDictionary.Trex[3];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Right Arm Claws
    {
        if (actor.IsAttacking) return SpriteDictionary.Trex[6];
		return SpriteDictionary.Trex[5];
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Belly Cover
    {
        if (actor.HasBelly == false) return SpriteDictionary.Trex[7];
		return null;
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // Left Side
    {
        if (actor.IsAttacking) return SpriteDictionary.Trex[9];
		return SpriteDictionary.Trex[8];
    }

    protected override Sprite BodyAccentSprite9(Actor_Unit actor) // Left Side Claws
    {
        if (actor.IsAttacking) return SpriteDictionary.Trex[11];
		return SpriteDictionary.Trex[10];
    }

    protected override Sprite BodyAccentSprite10(Actor_Unit actor) // Sheath
    {
        if (actor.Unit.BreastSize < 0) return SpriteDictionary.Trex[20];
		return SpriteDictionary.Trex[22];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // Sheath Insides
    {
        if (actor.Unit.BreastSize < 0) return SpriteDictionary.Trex[21];
		return SpriteDictionary.Trex[23];
    }

    protected override Sprite BallsSprite(Actor_Unit actor) // Balls
    {
        if (Config.HideCocks || actor.Unit.DickSize < 0) return null;

        if (actor.GetBallSize(54) == 0) return SpriteDictionary.Trex[68];

        int size = actor.GetBallSize(54);

        if (size >= 54 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Trex[106];
        }

        else if (size >= 51 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Trex[105];
        }

        else if (size >= 48 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Trex[104];
        }

        else if (size >= 45 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Trex[103];
        }

        else if (size >= 42 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Trex[102];
        }

        else if (size >= 39 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Trex[101];
        }

        else if (size >= 36 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Trex[100];
        }

        else if (size >= 33 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return SpriteDictionary.Trex[99];
        }

        if (size > 29) size = 29;
        return SpriteDictionary.Trex[69 + size];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // Belly
    {
        if (actor.HasBelly == false)
            return null;

        int size = actor.GetStomachSize(54);

        if (size >= 54 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.Trex[67];
        }

        else if (size >= 51 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.Trex[66];
        }

        else if (size >= 48 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.Trex[65];
        }

        else if (size >= 45 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.Trex[64];
        }

        else if (size >= 42 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.Trex[63];
        }

        else if (size >= 39 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.Trex[62];
        }

        else if (size >= 36 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.Trex[61];
        }

        else if (size >= 33 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) ?? false))
        {
            return SpriteDictionary.Trex[60];
        }

        if (size > 29) size = 29;
        return SpriteDictionary.Trex[30 + size];
    }
}
