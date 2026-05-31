using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Centaur : TaurHumanHalf
{
    //readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.CentaurParts;
    int HumanBodyOffsetX = 25;
    int HumanBodyOffsetY = 49;
    public Centaur()
    {
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.FeralHorseSkin); // Main body, legs, head, tail upper
        GentleAnimation = true;
        WeightGainDisabled = true;
        TailTypes = 6;
        EarTypes = 4;

        BodyAccessory = new SpriteExtraInfo(10, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.AccessoryColor)); // Horse Body
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.AccessoryColor)); // right hind leg
        BodyAccent2 = new SpriteExtraInfo(8, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); // tail
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.AccessoryColor)); // sheath
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.AccessoryColor)); // belly cover
        BodyAccent5 = new SpriteExtraInfo(4, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.AccessoryColor)); // left hind leg
        BodyAccent6 = new SpriteExtraInfo(1, BodyAccentSprite6, null, null); // right hind hoof
        BodyAccent7 = new SpriteExtraInfo(4, BodyAccentSprite7, null, null); // left hind hoof
        BodyAccent8 = new SpriteExtraInfo(11, BodyAccentSprite8, null, null); // Front Hooves
        BodyAccent9 = new SpriteExtraInfo(25, BodyAccentSprite9, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.AccessoryColor)); // Ears
        SecondaryBelly = new SpriteExtraInfo(7, SecondaryBellySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.AccessoryColor)); // Second Stomach
        Dick = new SpriteExtraInfo(4, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(3, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.AccessoryColor));
        
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new GenericTop1(),
            new GenericTop2(),
            new GenericTop3(),
            new GenericTop4(),
            new GenericTop5(),
            new GenericTop6(),
            new GenericTop7(),
            new MaleTop(),
            new MaleTop3(),
            new MaleTop5(),
            new MaleTop6(),
        };
    }
    internal override int BreastSizes => 8;

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.TailType = State.Rand.Next(TailTypes);
        unit.EarType = State.Rand.Next(EarTypes);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        OffsetAllHumanParts(actor, HumanBodyOffsetX, HumanBodyOffsetY);
        AddOffset(BodyAccent9, HumanBodyOffsetX * .625f, HumanBodyOffsetY * .625f);
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // Mane
    {
        if (actor.IsAttacking)
            return State.GameManager.SpriteDictionary.CentaurParts[1];
        if (actor.IsOralVoring)
            return State.GameManager.SpriteDictionary.CentaurParts[1];
        else
            return State.GameManager.SpriteDictionary.CentaurParts[0];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Left Hind leg
    {
        return State.GameManager.SpriteDictionary.CentaurParts[5];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Right Hind Hoof
    {
        return State.GameManager.SpriteDictionary.CentaurParts[6];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Belly Cover
    {
        if (actor.HasBelly)
            return null;
        else
            return State.GameManager.SpriteDictionary.CentaurParts[8];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) //Sheath
    {
        if (actor.Unit.DickSize < 0) return null;
        if (Config.HideCocks) return null;
		
		if (actor.IsErect()) return State.GameManager.SpriteDictionary.CentaurParts[28];

		else return State.GameManager.SpriteDictionary.CentaurParts[29];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Tail
    { 
        return State.GameManager.SpriteDictionary.CentaurParts[22 + actor.Unit.TailType];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Right Hind Leg
    {
        return State.GameManager.SpriteDictionary.CentaurParts[4];
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Left Hind Hoof
    {
        return State.GameManager.SpriteDictionary.CentaurParts[7];
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // Face
    {
        if (actor.IsAttacking)
            return State.GameManager.SpriteDictionary.CentaurParts[3]; 
        if (actor.IsOralVoring)
            return State.GameManager.SpriteDictionary.CentaurParts[3]; 
        return State.GameManager.SpriteDictionary.CentaurParts[2]; 
    }
    protected override Sprite BodyAccentSprite9(Actor_Unit actor) // Face
    {
        if (actor.Unit.EarType == 3)
            return null; 
        return State.GameManager.SpriteDictionary.CentaurTorsoAddOns[8 + actor.Unit.EarType];
    }

    protected override Sprite SecondaryBellySprite(Actor_Unit actor) // Second Stomach
    {
        int size = actor.GetStomach2Size(31, 0.7f) + actor.GetWombSize(31, 0.7f);
        if (size < 1) return State.GameManager.SpriteDictionary.CentaurParts[8];

        if (size > 4)
        {
            SecondaryBelly.layer = 9;
        }
        else
        {
            SecondaryBelly.layer = 6;
        }

        if ( size >= 31 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
			AddOffset(SecondaryBelly, -23 * .625f, -7 * .625f);
            return State.GameManager.SpriteDictionary.CentaurParts[54];
        }

        if (size >= 29 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
			AddOffset(SecondaryBelly, -23 * .625f, -7 * .625f);
            return State.GameManager.SpriteDictionary.CentaurParts[53];
        }

        if (size >= 27 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
			AddOffset(SecondaryBelly, -23 * .625f, -7 * .625f);
            return State.GameManager.SpriteDictionary.CentaurParts[52];
        }

        if (size >= 25 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
			AddOffset(SecondaryBelly, -23 * .625f, -7 * .625f);
            return State.GameManager.SpriteDictionary.CentaurParts[51];
        }

        if (size >= 23 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
			AddOffset(SecondaryBelly, -23 * .625f, -7 * .625f);
            return State.GameManager.SpriteDictionary.CentaurParts[50];
        }

        if (size > 19) size = 19;

        return State.GameManager.SpriteDictionary.CentaurParts[30 + size];
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.DickSize < 0) return null;
        if (Config.HideCocks) return null;

        if (actor.IsCockVoring) return State.GameManager.SpriteDictionary.CentaurParts[9];
		if (actor.IsErect()) return State.GameManager.SpriteDictionary.CentaurParts[9];

        return null;
    }
    
    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == true && !Config.HideCocks && actor.PredatorComponent?.BallsFullness > 0)
		{
			AddOffset(Balls, -48 * .625f, 0 * .625f);
        }
		else return null;

        int size = actor.GetBallSize(30);

        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false)
        {
            return State.GameManager.SpriteDictionary.CentaurParts[79];
        }

        else if (size >= 29 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.CentaurParts[78];
        }

        else if (size >= 27 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.CentaurParts[77];
        }

        else if (size >= 25 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.CentaurParts[76];
        }

        else if (size >= 23 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.CentaurParts[75];
        }

        else if (size >= 21 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.CentaurParts[74];
        }

        if (size > 19) size = 19;
        return State.GameManager.SpriteDictionary.CentaurParts[55 + size];
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return State.GameManager.SpriteDictionary.CentaurTorsoAddOns[0];
                case 1:
                    return State.GameManager.SpriteDictionary.CentaurTorsoAddOns[1];
                case 2:
                    return State.GameManager.SpriteDictionary.CentaurTorsoAddOns[4];
                case 3:
                    return State.GameManager.SpriteDictionary.CentaurTorsoAddOns[5];
                case 4:
                    return State.GameManager.SpriteDictionary.CentaurTorsoAddOns[2];
                case 5:
                    return State.GameManager.SpriteDictionary.CentaurTorsoAddOns[3];
                case 6:
                    return State.GameManager.SpriteDictionary.CentaurTorsoAddOns[6];
                case 7:
                    return State.GameManager.SpriteDictionary.CentaurTorsoAddOns[7];
                default:
                    return null;
            }
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {

        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return State.GameManager.SpriteDictionary.TaurTorso[3 + (actor.Unit.HasBreasts ? 0 : 4)];
            return State.GameManager.SpriteDictionary.TaurTorso[0 + (actor.Unit.HasBreasts ? 0 : 4)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return State.GameManager.SpriteDictionary.TaurTorso[1 + (actor.Unit.HasBreasts ? 0 : 4)];
            case 1:
                return State.GameManager.SpriteDictionary.TaurTorso[2 + (actor.Unit.HasBreasts ? 0 : 4)];
            case 2:
                return State.GameManager.SpriteDictionary.TaurTorso[1 + (actor.Unit.HasBreasts ? 0 : 4)];
            case 3:
                return State.GameManager.SpriteDictionary.TaurTorso[2 + (actor.Unit.HasBreasts ? 0 : 4)];
            case 4:
                return State.GameManager.SpriteDictionary.TaurTorso[2 + (actor.Unit.HasBreasts ? 0 : 4)];
            case 5:
                return State.GameManager.SpriteDictionary.TaurTorso[1 + (actor.Unit.HasBreasts ? 0 : 4)];
            case 6:
                return State.GameManager.SpriteDictionary.TaurTorso[2 + (actor.Unit.HasBreasts ? 0 : 4)];
            case 7:
                return State.GameManager.SpriteDictionary.TaurTorso[3 + (actor.Unit.HasBreasts ? 0 : 4)];
            default:
                return State.GameManager.SpriteDictionary.TaurTorso[0 + (actor.Unit.HasBreasts ? 0 : 4)];
        }
    }
    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        oversize = false;
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f));
            if (leftSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[29];
            }

            if (leftSize > 28)
                leftSize = 28;

            return State.GameManager.SpriteDictionary.HumansVoreSprites[0 + leftSize];
        }
        else
        {
            return State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f));
            if (rightSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[63];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[61];
            }

            if (rightSize > 28)
                rightSize = 28;

            return State.GameManager.SpriteDictionary.HumansVoreSprites[32 + rightSize];
        }
        else
        {
            return State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
        }
    }



    class GenericTop1 : MainClothing
    {
        public GenericTop1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[57];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60001;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Centaur.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[56];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[0 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            clothing1.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing1.YOffset = (Races.Centaur.HumanBodyOffsetY - 1) * .625f;
            clothing2.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing2.YOffset = (Races.Centaur.HumanBodyOffsetY) * .625f;
            clothing3.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing3.YOffset = (Races.Centaur.HumanBodyOffsetY) * .625f;

            base.Configure(sprite, actor);
        }
    }
    class GenericTop2 : MainClothing
    {
        public GenericTop2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[58];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60002;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Centaur.oversize)
            {
                clothing1.GetSprite = (s) => null;
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[8 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            clothing1.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing1.YOffset = (Races.Centaur.HumanBodyOffsetY - 1) * .625f;
            clothing2.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing2.YOffset = (Races.Centaur.HumanBodyOffsetY) * .625f;
            clothing3.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing3.YOffset = (Races.Centaur.HumanBodyOffsetY) * .625f;

            base.Configure(sprite, actor);
        }
    }
    class GenericTop3 : MainClothing
    {
        public GenericTop3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[60];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60003;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Centaur.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[59];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[16 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            clothing1.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing1.YOffset = (Races.Centaur.HumanBodyOffsetY - 1) * .625f;
            clothing2.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing2.YOffset = (Races.Centaur.HumanBodyOffsetY) * .625f;
            clothing3.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing3.YOffset = (Races.Centaur.HumanBodyOffsetY) * .625f;

            base.Configure(sprite, actor);
        }
    }
    class GenericTop4 : MainClothing
    {
        public GenericTop4()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[62];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60004;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Centaur.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[61];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[24 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            clothing1.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing1.YOffset = (Races.Centaur.HumanBodyOffsetY - 1) * .625f;
            clothing2.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing2.YOffset = (Races.Centaur.HumanBodyOffsetY) * .625f;
            clothing3.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing3.YOffset = (Races.Centaur.HumanBodyOffsetY) * .625f;

            base.Configure(sprite, actor);
        }
    }
    class GenericTop5 : MainClothing
    {
        public GenericTop5()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[64];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60005;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Centaur.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[63];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[32 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            clothing1.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing1.YOffset = (Races.Centaur.HumanBodyOffsetY - 1) * .625f;
            clothing2.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing2.YOffset = (Races.Centaur.HumanBodyOffsetY) * .625f;
            clothing3.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing3.YOffset = (Races.Centaur.HumanBodyOffsetY) * .625f;

            base.Configure(sprite, actor);
        }
    }
    class GenericTop6 : MainClothing
    {
        public GenericTop6()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[66];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60006;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Centaur.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[65];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[40 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            clothing1.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing1.YOffset = (Races.Centaur.HumanBodyOffsetY - 1) * .625f;
            clothing2.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing2.YOffset = (Races.Centaur.HumanBodyOffsetY) * .625f;
            clothing3.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing3.YOffset = (Races.Centaur.HumanBodyOffsetY) * .625f;

            base.Configure(sprite, actor);
        }
    }
    class GenericTop7 : MainClothing
    {
        public GenericTop7()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFundertops[68];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 60007;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Centaur.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[67];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[48 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }

            clothing2.GetPalette = (s) => FurryColor(s);
            clothing3.GetPalette = (s) => FurryColor(s);

            clothing1.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing1.YOffset = (Races.Centaur.HumanBodyOffsetY - 1) * .625f;
            clothing2.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing2.YOffset = (Races.Centaur.HumanBodyOffsetY) * .625f;
            clothing3.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing3.YOffset = (Races.Centaur.HumanBodyOffsetY) * .625f;

            base.Configure(sprite, actor);
        }
    }

    class MaleTop : MainClothing
    {
        public MaleTop()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[5];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 60008;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[0];

            clothing1.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing1.YOffset = (Races.Centaur.HumanBodyOffsetY - 1) * .625f;

            base.Configure(sprite, actor);
        }
    }

    class MaleTop2 : MainClothing
    {
        public MaleTop2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[5];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 60009;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.HasBelly)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[4];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[1 + actor.Unit.BodySize];
            }

            clothing1.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing1.YOffset = (Races.Centaur.HumanBodyOffsetY - 1) * .625f;

            base.Configure(sprite, actor);
        }
    }

    class MaleTop3 : MainClothing
    {
        public MaleTop3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[11];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 60010;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[6];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            clothing1.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing1.YOffset = (Races.Centaur.HumanBodyOffsetY - 1) * .625f;

            base.Configure(sprite, actor);
        }
    }

    class MaleTop4 : MainClothing
    {
        public MaleTop4()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[11];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 60011;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.HasBelly)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[10];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[7 + actor.Unit.BodySize];
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            clothing1.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing1.YOffset = (Races.Centaur.HumanBodyOffsetY - 1) * .625f;

            base.Configure(sprite, actor);
        }
    }

    class MaleTop5 : MainClothing
    {
        public MaleTop5()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[14];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 60012;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.BodySize == 2)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[13];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[12];
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            clothing1.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing1.YOffset = (Races.Centaur.HumanBodyOffsetY - 1) * .625f;

            base.Configure(sprite, actor);
        }
    }

    class MaleTop6 : MainClothing
    {
        public MaleTop6()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenMundertops[16];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 60013;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[15];

            clothing1.XOffset = (Races.Centaur.HumanBodyOffsetX) * .625f;
            clothing1.YOffset = (Races.Centaur.HumanBodyOffsetY - 1) * .625f;

            base.Configure(sprite, actor);
        }
    }
}