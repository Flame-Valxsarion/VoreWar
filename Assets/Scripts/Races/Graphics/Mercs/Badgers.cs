using System;
using System.Collections.Generic;
using UnityEngine;

class Badgers : DefaultRaceData
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.BadgersBodies;
    readonly Sprite[] Sprites4 = State.GameManager.SpriteDictionary.BadgersClothes;

    bool oversize = false;


    public Badgers()
    {
        BodySizes = 4;
        EyeTypes = 8;
        SpecialAccessoryCount = 5; // body patterns    
        HairStyles = 0;
        MouthTypes = 0;
        HairColors = 0;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.BadgersSkin);
        AccessoryColors = 0;
        BodyAccentTypes1 = 8; // ear types

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(3, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BadgersSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(22, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BadgersSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(21, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BadgersSkin, s.Unit.SkinColor)); // Ears
        BodyAccent = new SpriteExtraInfo(3, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BadgersSkin, s.Unit.SkinColor)); // Right Arm
        BodyAccent2 = new SpriteExtraInfo(4, BodyAccentSprite2, WhiteColored); // claws
        BodyAccent3 = new SpriteExtraInfo(4, BodyAccentSprite3, WhiteColored); // Right Arm claws
        BodyAccent4 = null;
        BodyAccent5 = null;
        BodyAccent6 = null;
        BodyAccent7 = null;
        Mouth = new SpriteExtraInfo(22, MouthSprite, WhiteColored);
        Hair = null;
        Hair2 = null;
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(23, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BadgersSkin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(6, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BadgersSkin, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BadgersSkin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BadgersSkin, s.Unit.SkinColor));

        AllowedClothingHatTypes = new List<ClothingAccessory>();


        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new GenericTop1(),
            new GenericTop2(),
            new GenericTop3(),
            new GenericTop4(),
            new GenericTop5(),
            new Natural(),
            new Tribal(),
        };
        AvoidedMainClothingTypes = 0;
        AvoidedEyeTypes = 0;
        AllowedWaistTypes = new List<MainClothing>()
        {
            new GenericBot1(),
            new GenericBot2(),
            new GenericBot3(),
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
        {
            AddOffset(Eyes, 0, 1 * .625f);
        }
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        //unit.AccessoryColor = unit.SkinColor;

        unit.SpecialAccessoryType = State.Rand.Next(SpecialAccessoryCount);
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
    }

    internal override int DickSizes => 8;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        int sat = Mathf.Clamp(actor.Unit.SpecialAccessoryType, 0, 4); //Protect against out of bounds from other unit types.  
        return Sprites[0 + actor.Unit.BodySize + 10 * sat];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[9 + 10 * actor.Unit.SpecialAccessoryType];
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[8 + 10 * actor.Unit.SpecialAccessoryType];
        return Sprites[7 + 10 * actor.Unit.SpecialAccessoryType];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // ears
    {
        return Sprites[77 + actor.Unit.BodyAccentType1];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // right arm
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return Sprites[4 + 10 * actor.Unit.SpecialAccessoryType];
                case 1:
                    return Sprites[5 + 10 * actor.Unit.SpecialAccessoryType];
                case 2:
                    return Sprites[5 + 10 * actor.Unit.SpecialAccessoryType];
                case 3:
                    return Sprites[6 + 10 * actor.Unit.SpecialAccessoryType];
                default:
                {
                    if (actor.IsAttacking)
                        return Sprites[5 + 10 * actor.Unit.SpecialAccessoryType];
                    else
                        return Sprites[4 + 10 * actor.Unit.SpecialAccessoryType];
                }
            }
        }
        else
        {
            if (actor.IsAttacking)
                return Sprites[5 + 10 * actor.Unit.SpecialAccessoryType];
            else
                return Sprites[4 + 10 * actor.Unit.SpecialAccessoryType];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites[50]; // claws

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Right arm claws
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return Sprites[51];
                case 1:
                    return Sprites[52];
                case 2:
                    return Sprites[52];
                case 3:
                    return Sprites[53];
                default:
                    if (actor.IsAttacking)
                        return Sprites[52];
                    else
                        return Sprites[51];
            }
        }
        else
        {
            if (actor.IsAttacking)
                return Sprites[52];
            else
                return Sprites[51];
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring)
            return Sprites[56];
        if (actor.IsAttacking || actor.IsEating)
            return Sprites[55];
        return Sprites[54];
    }
    protected override Sprite EyesSprite(Actor_Unit actor)
    {
            if (actor.IsOralVoring)
                return Sprites[69 + actor.Unit.EyeType];
            else
                return Sprites[61 + actor.Unit.EyeType];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        UnityEngine.Sprite[] bellyPat = State.GameManager.SpriteDictionary.BadgersVoreBlack; // Switch from black to white pattern for vore parts accordingly
        if (actor.Unit.SpecialAccessoryType == 0 || actor.Unit.SpecialAccessoryType == 2 || actor.Unit.SpecialAccessoryType == 4)
            bellyPat = State.GameManager.SpriteDictionary.BadgersVoreBlack;
        else if (actor.Unit.SpecialAccessoryType == 3)
            bellyPat = State.GameManager.SpriteDictionary.BadgersVoreWhite;
        else
            bellyPat = State.GameManager.SpriteDictionary.BadgersVoreWhite;
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(26, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 26)
            {
                AddOffset(Belly, 0, -12 * .625f);
                return bellyPat[90];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 26)
            {
                AddOffset(Belly, 0, -12 * .625f);
                return bellyPat[89];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 25)
            {
                AddOffset(Belly, 0, -12 * .625f);
                return bellyPat[88];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 24)
            {
                AddOffset(Belly, 0, -12 * .625f);
                return bellyPat[87];
            }
            switch (size)
            {
                case 24:
                    AddOffset(Belly, 0, -4 * .625f);
                    break;
                case 25:
                    AddOffset(Belly, 0, -9 * .625f);
                    break;
                case 26:
                    AddOffset(Belly, 0, -12 * .625f);
                    break;
            }

            return bellyPat[60 + size];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return Sprites[57];
                case 1:
                    return Sprites[58];
                case 2:
                    return Sprites[59];
                case 3:
                    return Sprites[60];
                default:
                    return null;
            }
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        UnityEngine.Sprite[] breastPat = State.GameManager.SpriteDictionary.BadgersVoreBlack; // Switch from black to white pattern for vore parts accordingly
        if (actor.Unit.SpecialAccessoryType == 0 || actor.Unit.SpecialAccessoryType == 2 || actor.Unit.SpecialAccessoryType == 4)
            breastPat = State.GameManager.SpriteDictionary.BadgersVoreBlack;
        else if (actor.Unit.SpecialAccessoryType == 3)
            breastPat = State.GameManager.SpriteDictionary.BadgersVoreBlack;
        else
            breastPat = State.GameManager.SpriteDictionary.BadgersVoreWhite;
        if (actor.Unit.HasBreasts == false)
            return null;
        oversize = false;
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(30 * 30, 1f));
            if (leftSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return breastPat[29];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return breastPat[28];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 26)
            {
                return breastPat[27];
            }

            if (leftSize > 26)
                leftSize = 26;

            return breastPat[0 + leftSize];
        }
        else
        {
            return breastPat[0 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        UnityEngine.Sprite[] breastPat = State.GameManager.SpriteDictionary.BadgersVoreBlack; // Switch from black to white pattern for vore parts accordingly
        if (actor.Unit.SpecialAccessoryType == 0 || actor.Unit.SpecialAccessoryType == 2 || actor.Unit.SpecialAccessoryType == 4)
            breastPat = State.GameManager.SpriteDictionary.BadgersVoreBlack;
        else if (actor.Unit.SpecialAccessoryType == 3)
            breastPat = State.GameManager.SpriteDictionary.BadgersVoreBlack;
        else
            breastPat = State.GameManager.SpriteDictionary.BadgersVoreWhite;
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(30 * 30, 1f));
            if (rightSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return breastPat[59];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return breastPat[58];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 26)
            {
                return breastPat[57];
            }

            if (rightSize > 26)
                rightSize = 26;

            return breastPat[30 + rightSize];
        }
        else
        {
            return breastPat[30 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(31 * 31, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(31 * 31, 1f)) < 16))
            {
                Dick.layer = 20;
                if (actor.IsCockVoring)
                {
                    return Sprites[101 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[85 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    return Sprites[109 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[93 + actor.Unit.DickSize];
                }
            }
        }

        Dick.layer = 11;
        return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        UnityEngine.Sprite[] ballPat = State.GameManager.SpriteDictionary.BadgersVoreBlack; // Switch from black to white pattern for vore parts accordingly
        if (actor.Unit.SpecialAccessoryType == 0 || actor.Unit.SpecialAccessoryType == 2 || actor.Unit.SpecialAccessoryType == 4)
            ballPat = State.GameManager.SpriteDictionary.BadgersVoreBlack;
        else if (actor.Unit.SpecialAccessoryType == 3)
            ballPat = State.GameManager.SpriteDictionary.BadgersVoreWhite;
        else
            ballPat = State.GameManager.SpriteDictionary.BadgersVoreWhite;
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect() && (actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(30 * 30, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(30 * 30, 1f)) < 16))
        {
            Balls.layer = 19;
        }
        else
        {
            Balls.layer = 10;
        }
        int size = actor.Unit.DickSize;
        int offset = actor.GetBallSize(27, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -27 * .625f);
            return ballPat[127];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return ballPat[126];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 26)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return ballPat[125];
        }
        else if (offset >= 25)
        {
            AddOffset(Balls, 0, -13 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -8 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -3 * .625f);
        }

        if (offset > 0)
            return ballPat[Math.Min(99 + offset, 124)];
        return ballPat[91 + size];
    }


    class GenericTop1 : MainClothing
    {
        public GenericTop1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Komodos4[48];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 61401;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Badgers.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[52];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[44 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }
    
    class GenericTop2 : MainClothing
    {
        public GenericTop2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Komodos4[58];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 61402;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Badgers.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[61];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[53 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }
    
    class GenericTop3 : MainClothing
    {
        public GenericTop3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Komodos4[68];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 61403;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Badgers.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[70];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[79];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[62 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[71 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop4 : MainClothing
    {
        public GenericTop4()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Komodos4[79];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 61404;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Badgers.oversize)
            {
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[88];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[89 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[80 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }
            
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop5 : MainClothing
    {
        public GenericTop5()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Komodos4[97];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 61405;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Badgers.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[105];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[97 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class Natural : MainClothing
    {
        public Natural()
        {
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(7, null, null);
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Badgers.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
		if (actor.Unit.SpecialAccessoryType == 1)
		{
                	clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[10 + actor.Unit.BreastSize];
		}
		else
		{
                	clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[2 + actor.Unit.BreastSize];
		}
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
	    }

	    if (actor.Unit.SpecialAccessoryType == 1 || actor.Unit.SpecialAccessoryType == 3)
	    {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[1];
	    }
	    else
	    {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[0];
	    }

        base.Configure(sprite, actor);
        }
    }

    class Tribal : MainClothing
    {
        public Tribal()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Komodos4[38];
            coversBreasts = false;
            Type = 61406;
            OccupiesAllSlots = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Badgers.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[43];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[35 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[31 + actor.Unit.BodySize];

            base.Configure(sprite, actor);
        }
    }

    class GenericBot1 : MainClothing
    {
        public GenericBot1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Komodos4[9];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 61407;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[23];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[25];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[24];
            }
            else clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[22];

            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[18 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot2 : MainClothing
    {
        public GenericBot2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Komodos4[19];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 61408;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[26];
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[18 + actor.Unit.BodySize];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot3 : MainClothing
    {
        public GenericBot3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Komodos4[24];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 61409;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BadgersClothes[27 + actor.Unit.BodySize];

            base.Configure(sprite, actor);
        }
    }

}
