using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Earthworms : BlankSlate
{
    RaceFrameList frameListHeadIdle = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .5f, .5f, 1.5f, .5f, .5f });

    enum Position
    {
        Underground,
        Aboveground
    }
    Position position;
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.Earthworms;

    public Earthworms()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        clothingColors = 0;
        GentleAnimation = true;
        WeightGainDisabled = true;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EarthwormSkin);

        Body = new SpriteExtraInfo(5, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(7, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, WhiteColored); // rocks
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, s.Unit.SkinColor)); // belly support
        BodyAccent2 = new SpriteExtraInfo(2, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, s.Unit.SkinColor)); // belly cover
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, s.Unit.SkinColor)); // tail
        BodyAccent4 = new SpriteExtraInfo(3, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, s.Unit.SkinColor)); // tail connector
        Mouth = new SpriteExtraInfo(8, MouthSprite, WhiteColored);
        Belly = new SpriteExtraInfo(4, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EarthwormSkin, s.Unit.SkinColor));

    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (!actor.HasAttackedThisCombat)
            position = Position.Underground;
        else
            position = Position.Aboveground;
        base.RunFirst(actor);
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(0, 0, false) };  // Index 0.
    }

	internal override void SetBaseOffsets(Actor_Unit actor)
    {  
        int size = actor.GetStomachSize(50);
		
		if (size == 22)
		{
			AddOffset(Belly, 0 * .625f, 0 * .625f);
			AddOffset(BodyAccent3, 0 * .625f, 0 * .625f);
		}
		else if (size == 23)
		{
			AddOffset(Belly, 0 * .625f, 0 * .625f);
			AddOffset(BodyAccent3, 1 * .625f, 0 * .625f);
			AddOffset(BodyAccent4, 1 * .625f, 0 * .625f);
		}
		else if (size == 24)
		{
			AddOffset(Belly, 0 * .625f, 0 * .625f);
			AddOffset(BodyAccent3, 5 * .625f, 0 * .625f);
			AddOffset(BodyAccent4, 5 * .625f, 0 * .625f);
		}
		else if (size == 25)
		{
			AddOffset(Belly, 0 * .625f, 0 * .625f);
			AddOffset(BodyAccent3, 10 * .625f, 0 * .625f);
			AddOffset(BodyAccent4, 10 * .625f, 0 * .625f);
		}
		else if (size == 26)
		{
			AddOffset(Belly, 0 * .625f, 0 * .625f);
			AddOffset(BodyAccent3, 15 * .625f, 0 * .625f);
			AddOffset(BodyAccent4, 15 * .625f, 0 * .625f);
		}
		else if (size == 27)
		{
			AddOffset(Belly, 0 * .625f, 0 * .625f);
			AddOffset(BodyAccent3, 20 * .625f, 0 * .625f);
			AddOffset(BodyAccent4, 20 * .625f, 0 * .625f);
		}
		else if (size == 28)
		{
			AddOffset(Belly, 20 * .625f, 0 * .625f);
			AddOffset(BodyAccent3, 26 * .625f, 0 * .625f);
			AddOffset(BodyAccent4, 26 * .625f, 0 * .625f);
		}
		else if (size == 29)
		{
			AddOffset(Belly, 20 * .625f, 0 * .625f);
			AddOffset(BodyAccent3, 32 * .625f, 0 * .625f);
			AddOffset(BodyAccent4, 32 * .625f, 0 * .625f);
		}
		else if (size >= 30)
		{
			AddOffset(Belly, 20 * .625f, 0 * .625f);
			AddOffset(BodyAccent3, 35 * .625f, 0 * .625f);
			AddOffset(BodyAccent4, 35 * .625f, 0 * .625f);
		}
		else
        {
            AddOffset(Belly, 0, -48 * .625f);
        }
	}

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        if (!actor.Targetable) return Sprites[4];

        switch (position)
        {
            case Position.Underground:
                if (actor.IsEating || actor.IsAttacking)
                    return Sprites[1];
                return Sprites[0];
            case Position.Aboveground:
                return Sprites[4];
        }
        return base.BodySprite(actor);
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (!actor.Targetable) return Sprites[8];

        if (actor.IsAttacking || actor.IsEating || position == Position.Underground)
        {
            actor.AnimationController.frameLists[0].currentlyActive = false;
            actor.AnimationController.frameLists[0].currentFrame = 0;
            actor.AnimationController.frameLists[0].currentTime = 0f;

            if (actor.IsEating || actor.IsAttacking)
            {
                if (position == Position.Underground)
                    return Sprites[16];
                return Sprites[11];
            }
            else
            {
                if (position == Position.Underground)
                    return null;
                return Sprites[8];
            }
        }

        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            if (actor.AnimationController.frameLists[0].currentTime >= frameListHeadIdle.times[actor.AnimationController.frameLists[0].currentFrame])
            {
                actor.AnimationController.frameLists[0].currentFrame++;
                actor.AnimationController.frameLists[0].currentTime = 0f;

                if (actor.AnimationController.frameLists[0].currentFrame >= frameListHeadIdle.frames.Length)
                {
                    actor.AnimationController.frameLists[0].currentlyActive = false;
                    actor.AnimationController.frameLists[0].currentFrame = 0;
                    actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            return Sprites[8 + frameListHeadIdle.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }

        if (State.Rand.Next(600) == 0)
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        return Sprites[8];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // Rocks
    {
        if (!actor.Targetable) return null;

        switch (position)
        {
            case Position.Underground:
                if (actor.IsEating || actor.IsAttacking)
                    return Sprites[3];
                return Sprites[2];
            case Position.Aboveground:
                return null;
            default:
                return null;
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Belly support
    {
        if (!actor.Targetable) return null;

        if (position == Position.Aboveground)
            return Sprites[6];
        return null;
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Belly cover
    {
        if (!actor.Targetable) return Sprites[7];

        if (position == Position.Aboveground && actor.HasBelly == false)
            return Sprites[7];
        return null;
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Tail
    {
        if(position == Position.Underground)
        {
            return null;
        }
        int size = actor.GetStomachSize(50);

		if (size >= 23)
		{
            return Sprites[57];
		}
        return Sprites[5];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Tail Connector
    {
        int size = actor.GetStomachSize(50);
		
		if (size >= 23)
		{
            return Sprites[56];
		}
        return null;
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (!actor.Targetable) return Sprites[12];

        switch (position)
        {
            case Position.Underground:
                if (actor.IsEating || actor.IsAttacking)
                    return Sprites[17];
                return null;
            case Position.Aboveground:
                if (actor.IsEating || actor.IsAttacking)
                    return Sprites[15];
                return Sprites[12 + frameListHeadIdle.frames[actor.AnimationController.frameLists[0].currentFrame]];
            default:
                return null;
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        if (position == Position.Aboveground)
        {
			int size = actor.GetStomachSize(50);
			
			if ( size >= 50 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
			{
				return State.GameManager.SpriteDictionary.Earthworms[55];
			}

			if (size >= 50 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
			{
				return State.GameManager.SpriteDictionary.Earthworms[54];
			}

			if (size >= 47 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
			{
				return State.GameManager.SpriteDictionary.Earthworms[53];
			}

			if (size >= 44 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
			{
				return State.GameManager.SpriteDictionary.Earthworms[52];
			}

			if (size >= 41 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
			{
				return State.GameManager.SpriteDictionary.Earthworms[51];
			}

			if (size >= 38 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
			{
				return State.GameManager.SpriteDictionary.Earthworms[50];
			}

			if (size >= 35 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
			{
				return State.GameManager.SpriteDictionary.Earthworms[49];
			}

			if (size >= 32 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
			{
				return State.GameManager.SpriteDictionary.Earthworms[48];
			}
			
			if (size > 29) size = 29;
            
			return State.GameManager.SpriteDictionary.Earthworms[18 + size];
        }
        return null;
    }

}
