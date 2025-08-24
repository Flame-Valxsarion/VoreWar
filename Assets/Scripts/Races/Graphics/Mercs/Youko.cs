using UnityEngine;
using System.Collections.Generic;
using System;

class Youko : Humans, IVoreRestrictions
{
    readonly Sprite[] Sprites2 = State.GameManager.SpriteDictionary.HumansBodySprites2;
    readonly Sprite[] Sprites3 = State.GameManager.SpriteDictionary.HumansBodySprites3;
    readonly Sprite[] Tails = State.GameManager.SpriteDictionary.YoukoTails;

    bool oversize = false;

    public Youko()
    {
        FurCapable = true;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);
        BodyAccessory = new SpriteExtraInfo(7, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, s.Unit.AccessoryColor)); // Ears
        SecondaryAccessory = new SpriteExtraInfo(1, SecondaryAccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor)); // Tail;
        Beard = null;
        BeardStyles = 0;

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
            new MaleTop2(),
            new MaleTop3(),
            new MaleTop4(),
            new MaleTop5(),
            new MaleTop6(),
            new Uniform1(),
            new FemaleOnePiece1(),
            new FemaleOnePiece2(),
            new FemaleOnePiece3(),
            new FemaleOnePiece4(),
        };
    }

    private int GetNumTails(Actor_Unit actor)
    {
        int StatTotal = actor.Unit.GetStatTotal();
        if (StatTotal < 85)
            return 0;
        return Math.Min((int)(StatTotal - 85) / 15, 7) + 1;

    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites3[8]; //ears
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor)
    {
        int tailCount = GetNumTails(actor);
        if (actor.Unit.Predator && actor.PredatorComponent.TailFullness > 0)
        {
            if(tailCount >= 7)
                return Tails[10];
            return Tails[9];
        }
        return Tails[tailCount];
    }
    protected override Sprite BeardSprite(Actor_Unit actor)
    {
        return null;
    }
    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        oversize = false;
        if (actor.PredatorComponent?.RightBreastFullness > 0 || actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            if (actor.Unit.HasBreasts == false){}
            else
            {
                int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f));
                if (rightSize > actor.Unit.DefaultBreastSize)
                    oversize = true;
                int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f));
                if (leftSize > actor.Unit.DefaultBreastSize)
                    oversize = true;
            }
        }
        if (actor.IsEating)
        {
            if (actor.Unit.Furry)
                return Sprites2[47];
            else if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return Sprites2[4];
                }
                else
                {
                    return Sprites2[1];
                }
            }
            else
            {
                return Sprites2[7 + (actor.Unit.BodySize * 3)];
            }
        }
        else if (actor.IsAttacking)
        {
            if (actor.Unit.Furry)
                return Sprites2[49];
            else if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return Sprites2[5];
                }
                else
                {
                    return Sprites2[2];
                }
            }
            else
            {
                return Sprites2[8 + (actor.Unit.BodySize * 3)];
            }
        }
        else
        {
            if (actor.Unit.Furry)
                return Sprites2[45];
            else if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return Sprites2[3];
                }
                else
                {
                    return Sprites2[0];
                }
            }
            else
            {
                return Sprites2[6 + (actor.Unit.BodySize * 3)];
            }

        }
    }

    public bool CheckVore(Actor_Unit actor, Actor_Unit target, PreyLocation location)
    {
        if(location == PreyLocation.tail)
        {
            int tailCount = GetNumTails(actor);
            if ((target != null) && (actor.PredatorComponent.TailFullness < 1))
                if ((float)target.Bulk() < (actor.PredatorComponent.TotalCapacity() / 2))
                    return (tailCount >= 4);
                else
                    return (tailCount >= 7);
            return (tailCount >= 4) && (actor.PredatorComponent.TailFullness < 1);
        }
        return true;
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
            if (Races.Youko.oversize)
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
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;

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
            if (Races.Youko.oversize)
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

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;

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
            if (Races.Youko.oversize)
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
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;

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
            if (Races.Youko.oversize)
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

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;

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
            if (Races.Youko.oversize)
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
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;

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
            if (Races.Youko.oversize)
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
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;

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
            if (Races.Youko.oversize)
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

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;

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

            base.Configure(sprite, actor);
        }
    }

    class Uniform1 : MainClothing
    {
        public Uniform1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenUniform1[42];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(15, null, null);
            clothing3 = new SpriteExtraInfo(5, null, null);
            Type = 60025;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Youko.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[6];
            }
            else if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BreastSize > 5)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[6];
                }
                else
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[0 + actor.Unit.BreastSize];
                }
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            if (actor.HasBelly)
            {
                if (actor.GetStomachSize(31, 0.7f) > 4)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[13 + 7 * actor.Unit.BodySize + 21 * (!actor.Unit.HasBreasts ? 1 : 0)];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 3)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[12 + 7 * actor.Unit.BodySize + 21 * (!actor.Unit.HasBreasts ? 1 : 0)];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 2)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[11 + 7 * actor.Unit.BodySize + 21 * (!actor.Unit.HasBreasts ? 1 : 0)];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 1)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[10 + 7 * actor.Unit.BodySize + 21 * (!actor.Unit.HasBreasts ? 1 : 0)];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 0)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[9 + 7 * actor.Unit.BodySize + 21 * (!actor.Unit.HasBreasts ? 1 : 0)];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[8 + 7 * actor.Unit.BodySize + 21 * (!actor.Unit.HasBreasts ? 1 : 0)];
                }
            }
            else
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform2[7 + 7 * actor.Unit.BodySize + 21 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }

            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform1[3 + 4 * actor.Unit.BodySize + 12 * (!actor.Unit.HasBreasts ? 1 : 0)];
                else clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform1[0 + 4 * actor.Unit.BodySize + 12 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }
            else if (actor.GetWeaponSprite() == 0 || actor.GetWeaponSprite() == 4 || actor.GetWeaponSprite() == 6)
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform1[2 + 4 * actor.Unit.BodySize + 12 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }
            else if (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 3)
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform1[3 + 4 * actor.Unit.BodySize + 12 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }
            else
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUniform1[1 + 4 * actor.Unit.BodySize + 12 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class FemaleOnePiece1 : MainClothing
    {
        public FemaleOnePiece1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFOnePieces[81];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            clothing4 = new SpriteExtraInfo(15, null, null);
            Type = 60014;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Youko.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[51];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[43 + actor.Unit.BreastSize];
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
            if (actor.Unit.BodySize == 2)
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[42];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[41];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[40];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[39];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[38];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[37];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[21];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[20];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[19];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[18];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[17];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[16];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[15];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class FemaleOnePiece2 : MainClothing
    {
        public FemaleOnePiece2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFOnePieces[80];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            clothing4 = new SpriteExtraInfo(15, null, null);
            Type = 60015;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Youko.oversize)
            {
                clothing1.GetSprite = null;
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[52 + actor.Unit.BreastSize];
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
            if (actor.Unit.BodySize == 2)
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[36];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[35];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[34];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[33];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[32];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[31];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[30];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[29];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[28];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[27];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[26];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[25];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[24];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[23];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[22];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[14];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[13];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[12];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[11];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[10];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[9];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[8];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[7];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[6];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[5];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[4];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[3];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[2];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[1];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[0];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class FemaleOnePiece3 : MainClothing
    {
        public FemaleOnePiece3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFOnePieces[79];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            clothing4 = new SpriteExtraInfo(15, null, null);
            Type = 60016;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Youko.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[69];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[61 + actor.Unit.BreastSize];
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
            if (actor.Unit.BodySize == 2)
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[36];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[35];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[34];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[33];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[32];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[31];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[30];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[29];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[28];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[27];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[26];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[25];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[24];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[23];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[22];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[14];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[13];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[12];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[11];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[10];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[9];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[8];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[7];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[6];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[5];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[4];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[3];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[2];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[1];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[0];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class FemaleOnePiece4 : MainClothing
    {
        public FemaleOnePiece4()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.HumenFOnePieces[82];
            blocksBreasts = true;
            coversBreasts = false;
            femaleOnly = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            clothing4 = new SpriteExtraInfo(15, null, null);
            Type = 60017;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Youko.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[78];
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[70 + actor.Unit.BreastSize];
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
            if (actor.Unit.BodySize == 2)
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[42];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[41];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[40];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[39];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[38];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[37];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    if (actor.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[21];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[20];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[19];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[18];
                    }
                    else if (actor.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[17];
                    }
                    else
                    {
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[16];
                    }
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFOnePieces[15];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);;
            clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }



}
