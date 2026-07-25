using System;
using System.Collections.Generic;
using UnityEngine;

class BikiniTop : MainClothing
{
    public BikiniTop()
    {
        DiscardSprite = State.GameManager.SpriteDictionary.BikiniTop[9];
        femaleOnly = true;
        coversBreasts = false;
        blocksDick = false;
        clothing1 = new SpriteExtraInfo(17, null, null);
        Type = 205;
        DiscardUsesPalettes = true;
    }


    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts)
        {
            int breastMod = 0;
            if (actor.Unit.Race == Race.Succubi)
                breastMod = 3;
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BikiniTop[actor.Unit.BreastSize + breastMod];
            actor.SquishedBreasts = true;
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
        }
        else
        {
            breastSprite = null;
            clothing1.GetSprite = null;
        }

        base.Configure(sprite, actor);
    }
}

class StrapTop : MainClothing
{
    public StrapTop()
    {
        DiscardSprite = State.GameManager.SpriteDictionary.Straps[9];
        femaleOnly = true;
        coversBreasts = false;
        blocksDick = false;
        Type = 204;
        clothing1 = new SpriteExtraInfo(17, null, null);
        DiscardUsesPalettes = true;
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {

        if (actor.Unit.HasBreasts)
        {
            int breastMod = 0;
            if (actor.Unit.Race == Race.Succubi)
                breastMod = 3;
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Straps[actor.Unit.BreastSize + breastMod];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
            actor.SquishedBreasts = true;
        }
        else
        {
            breastSprite = null;
            clothing1.GetSprite = null;
        }

        base.Configure(sprite, actor);
    }
}

class BeltTop : MainClothing
{
    public BeltTop()
    {
        DiscardSprite = State.GameManager.SpriteDictionary.Belts[9];
        femaleOnly = true;
        coversBreasts = false;
        blocksDick = false;
        Type = 203;
        clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
        FixedColor = true;
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        if (actor.Unit.Race == Race.Lizards && actor.IsAnalVoring || actor.IsUnbirthing)
            {
            blocksDick = false;
            clothing1.GetSprite = (s) => null;
            }
        else if (actor.Unit.HasBreasts)
        {
            int breastMod = 0;
            if (actor.Unit.Race == Race.Succubi)
                breastMod = 3;
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Belts[actor.Unit.BreastSize + breastMod];
            actor.SquishedBreasts = true;
        }
        else
        {
            breastSprite = null;
            clothing1.GetSprite = null;
        }

        base.Configure(sprite, actor);
    }
}




class BikiniBottom : MainClothing
{
    public BikiniBottom()
    {
        DiscardSprite = State.GameManager.SpriteDictionary.BikiniBottom[12];
        Type = 201;
        clothing1 = new SpriteExtraInfo(9, null, null);
        clothing2 = new SpriteExtraInfo(10, null, null);
        coversBreasts = false;
        DiscardUsesPalettes = true;
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        int spr = 0;
        if (actor.Unit.Race == Race.Lizards && actor.IsAnalVoring || actor.IsUnbirthing)
            {
            blocksDick = false;
            clothing1.GetSprite = (s) => null;
            }
        else if (actor.Unit.Race == Race.Lizards)
            {
            blocksDick = true;
            spr = 8;
            }
        else if (actor.Unit.Race == Race.Harpies)
            spr = 9;
        else if (actor.Unit.Race == Race.Lamia)
            spr = 3 + (actor.Unit.HasBreasts ? 0 : 4);
        else
        {
            if (actor.GetBodyWeight() > 0)
                spr = (actor.Unit.HasBreasts ? 0 : 4) + actor.Unit.BodySize;
            if (spr > 7)
                spr = 7;
        }

        if (actor.Unit.DickSize > 2)
        {
            if (actor.Unit.Race == Race.Lizards && actor.IsAnalVoring || actor.IsUnbirthing)
            {
                blocksDick = false;
                clothing2.GetSprite = (s) => null;
            }
            else if (actor.Unit.DickSize > 4)
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.BikiniBottom[11];
            else
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.BikiniBottom[10];
        }
        else clothing2.GetSprite = null;

        clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
        clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);



        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BikiniBottom[spr];
        base.Configure(sprite, actor);
    }
}

class Shorts : MainClothing
{
    public Shorts()
    {
        DiscardSprite = State.GameManager.SpriteDictionary.Shorts[12];
        Type = 202;
        FixedColor = true;
        clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
        clothing2 = new SpriteExtraInfo(11, null, WhiteColored);
        coversBreasts = false;
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        int spr = 0;
        if (actor.Unit.Race == Race.Lizards && actor.IsAnalVoring || actor.IsUnbirthing)
            {
            blocksDick = false;
            clothing1.GetSprite = (s) => null;
            }
        else if (actor.Unit.Race == Race.Lizards)
            {
            blocksDick = true;
            spr = 8;
            }
        else if (actor.Unit.Race == Race.Harpies)
            spr = 9;
        else
        {
            if (actor.GetBodyWeight() > 0)
                spr = (actor.Unit.HasBreasts ? 0 : 4) + actor.Unit.BodySize;
            if (spr > 7)
                spr = 7;
        }

        if (actor.Unit.DickSize > 2)
        {
            if (actor.Unit.DickSize > 4)
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Shorts[11];
            else
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Shorts[10];
        }
        else clothing2.GetSprite = null;

        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Shorts[spr];
        base.Configure(sprite, actor);
    }
}

class Loincloth : MainClothing
{
    public Loincloth()
    {
        DiscardSprite = State.GameManager.SpriteDictionary.Loincloths[10];
        Type = 200;
        blocksDick = false;
        inFrontOfDick = 1;
        DiscardUsesPalettes = true;
        clothing1 = new SpriteExtraInfo(10, null, null);
        coversBreasts = false;
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        int spr = 0;
        if (actor.Unit.Race == Race.Lizards && actor.IsAnalVoring || actor.IsUnbirthing)
            {
            clothing1.GetSprite = (s) => null;
            }
        else if (actor.Unit.Race == Race.Lizards)
            spr = 8;
        else if (actor.Unit.Race == Race.Harpies)
            spr = 9;
        else if (actor.Unit.Race == Race.Lamia)
            spr = 3 + (actor.Unit.HasBreasts ? 0 : 4);
        else
        {
            if (actor.GetBodyWeight() > 0)
                spr = (actor.Unit.HasBreasts ? 0 : 4) + actor.Unit.BodySize;
            if (spr > 7)
                spr = 7;
        }
        clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);


        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Loincloths[spr];
        base.Configure(sprite, actor);
    }
}

class Leotard : MainClothing
{
    public Leotard()
    {
        DiscardSprite = State.GameManager.SpriteDictionary.Leotards[9];
        Type = 206;
        colorsBelly = true;
        //blocksBreasts = true;
        OccupiesAllSlots = true;
        DiscardUsesPalettes = true;
        clothing1 = new SpriteExtraInfo(10, null, null);
        clothing2 = new SpriteExtraInfo(17, null, null);
        clothing3 = new SpriteExtraInfo(11, null, null);

    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        int spr = 0;
        if (actor.Unit.Race == Race.Lizards)
            {
            blocksDick = true;
            spr = 8;
            }
        else if (actor.Unit.Race == Race.Harpies)
            spr = 9;
        else
        {
            if (actor.GetBodyWeight() > 0)
                spr = (actor.Unit.HasBreasts ? 0 : 4) + actor.Unit.BodySize;
            if (spr > 7)
                spr = 7;
        }

        if (actor.Unit.DickSize > 2)
        {
            if (actor.Unit.Race == Race.Lizards && actor.IsAnalVoring || actor.IsUnbirthing)
            {
                blocksDick = false;
                clothing3.GetSprite = (s) => null;
            }
            else if (actor.Unit.DickSize > 4)
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.Leotards[11];
            else
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.Leotards[10];
        }
        else clothing3.GetSprite = null;

        clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
        clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
        clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
        bellyPalette = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SkinToClothing, actor.Unit.ClothingColor);

        if (actor.Unit.Race == Race.Lizards && actor.IsAnalVoring || actor.IsUnbirthing)
            {
            blocksDick = false;
            clothing1.GetSprite = (s) => null;
            }
        else clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Leotards[spr];
        
        if (actor.Unit.BreastSize >= 0)
        {
            if (actor.Unit.Race == Race.Lizards && actor.IsAnalVoring || actor.IsUnbirthing)
            {
                blocksDick = false;
                clothing2.GetSprite = (s) => null;
            }
            else clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Leotards[12 + actor.Unit.BreastSize];
        }
        else
            clothing2.GetSprite = null;
        base.Configure(sprite, actor);
    }
}

class Rags : MainClothing
{
    public Rags()
    {
        DiscardSprite = State.GameManager.SpriteDictionary.Rags[23];
        blocksDick = false;
        inFrontOfDick = 1;
        coversBreasts = false;
        Type = 207;
        OccupiesAllSlots = true;
        FixedColor = true;
        clothing1 = new SpriteExtraInfo(11, null, WhiteColored);
        clothing2 = new SpriteExtraInfo(10, null, WhiteColored);
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        int spr = 0;
        if (actor.Unit.Race == Race.Lizards)
        {
            spr = 8;
            sprite.ChangeOffset(SpriteType.Clothing2, new Vector2(0, 2.5f));
        }
        else if (actor.Unit.Race == Race.Harpies)
            spr = 9;
        else if (actor.Unit.Race == Race.Lamia)
            spr = 3 + (actor.Unit.HasBreasts ? 0 : 4);
        else if (actor.Unit.Race == Race.Imps || actor.Unit.Race == Race.Goblins)
            spr = 10;
        else
        {
            if (actor.GetBodyWeight() > 0)
                spr = (actor.Unit.HasBreasts ? 0 : 4) + actor.Unit.BodySize;
            if (spr > 7)
                spr = 7;
        }

        if ((blocksDick || inFrontOfDick > 0) && Config.CockVoreHidesClothes && actor.PredatorComponent?.BallsFullness > 0)       
            clothing1.GetSprite = null;        
        else
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Rags[spr];

        clothing2.layer = 10;
        if (actor.Unit.Race == Race.Imps || actor.Unit.Race == Race.Goblins)
        {
            int spr2 = 21 + actor.Unit.BreastSize; //-1 to 1
            if (spr2 > 22)
                spr2 = 22;
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Rags[spr2];
            clothing2.layer = 18;
        }
        else if (actor.Unit.Race == Race.Lizards && actor.IsAnalVoring || actor.IsUnbirthing)
            {
            blocksDick = false;
            clothing2.GetSprite = (s) => null;
            }
        else if (actor.Unit.BreastSize >= 0)
        {
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Rags[11 + actor.Unit.BreastSize];
            clothing2.layer = 18;
        }
        else
        {
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Rags[11];
        }

        base.ConfigureIgnoreHidingRules(sprite, actor);
    }
}

class BlackTop : MainClothing
{
    public BlackTop()
    {
        DiscardSprite = null;
        blocksBreasts = true;
        blocksDick = false;
        Type = 208;
        FixedColor = true;
        clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        switch (actor.Unit.Race)
        {
            case Race.Goblins:
            case Race.Imps:
                if (actor.Unit.Race == Race.Goblins && actor.Unit.BreastSize > 0)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BlackTop[3 + actor.Unit.BreastSize];
                }
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BlackTop[16 + Math.Min(actor.Unit.BreastSize, 1)];
                break;
            default:
                if (actor.Unit.HasBreasts)
                {
                    int breastMod = 0;
                    if (actor.Unit.Race == Race.Succubi)
                        breastMod = 3;
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BlackTop[6 + actor.Unit.BreastSize + breastMod];
                }
                else
                {
                    if (actor.GetBodyWeight() <= 0)
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BlackTop[6];
                    else
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BlackTop[Math.Max(Math.Min(actor.GetBodyWeight() - 1, 2), 0)];
                }
                break;
        }
        if (actor.Unit.Race == Race.Lizards)
            sprite.ChangeOffset(SpriteType.Clothing, new Vector2(0, 2.5f));
        base.Configure(sprite, actor);
    }
}

class FemaleVillager : MainClothing
{
    public FemaleVillager()
    {
        DiscardSprite = null;
        Type = 209;
        blocksBreasts = true;
        femaleOnly = true;
        OccupiesAllSlots = true;
        HidesFluff = true;
        FixedColor = true;
        clothing1 = new SpriteExtraInfo(10, null, null);
        clothing2 = new SpriteExtraInfo(10, null, null);
        clothing3 = new SpriteExtraInfo(10, null, null);
        clothing4 = new SpriteExtraInfo(17, null, null);
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
        clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
        clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
        clothing1.GetSprite = (s) => SpriteDictionary.FemaleVillager[actor.IsAttacking ? 1 : 0];
        clothing2.GetSprite = (s) => SpriteDictionary.FemaleVillager[2];
        clothing3.GetSprite = (s) => SpriteDictionary.FemaleVillager[3 + actor.GetBodyWeight()];
        if (actor.Unit.HasBreasts)
            clothing4.GetSprite = (s) => SpriteDictionary.FemaleVillager[8 + actor.Unit.BreastSize];
        else
            clothing4.GetSprite = null;
        base.Configure(sprite, actor);
    }
}

class MaleVillager : MainClothing
{
    public MaleVillager()
    {
        DiscardSprite = null;
        Type = 210;
        blocksBreasts = true;
        OccupiesAllSlots = true;
        HidesFluff = true;
        maleOnly = true;
        FixedColor = true;
        clothing1 = new SpriteExtraInfo(10, null, null);
        clothing2 = new SpriteExtraInfo(10, null, WhiteColored);
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        int attackFactor = actor.IsAttacking ? 0 : 4;
        clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
        clothing1.GetSprite = (s) => SpriteDictionary.MaleVillager[Mathf.Min(actor.GetBodyWeight(), 3) + attackFactor];
        if (actor.Unit.DickSize >= 2)
            clothing2.GetSprite = (s) => SpriteDictionary.MaleVillager[8];
        else
            clothing2.GetSprite = null;
        base.Configure(sprite, actor);
    }
}

class MalePlate : MainClothing
{
    public MalePlate()
    {
        DiscardSprite = null;
        Type = 211;
        blocksBreasts = true;
        OccupiesAllSlots = true;
        HidesFluff = true;
        maleOnly = true;
        FixedColor = true;
        clothing1 = new SpriteExtraInfo(10, null, null);
        clothing2 = new SpriteExtraInfo(10, null, WhiteColored);
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        int attackFactor = actor.IsAttacking ? 0 : 4;
        clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
        clothing1.GetSprite = (s) => SpriteDictionary.MalePlate[Mathf.Min(actor.GetBodyWeight(), 3) + attackFactor];
        if (actor.Unit.DickSize >= 2)
            clothing2.GetSprite = (s) => SpriteDictionary.MalePlate[8];
        else
            clothing2.GetSprite = null;
        base.Configure(sprite, actor);
    }
}

class FemalePlate : MainClothing
{
    public FemalePlate()
    {
        DiscardSprite = null;
        Type = 212;
        blocksBreasts = true;
        femaleOnly = true;
        OccupiesAllSlots = true;
        HidesFluff = true;
        FixedColor = true;
        clothing1 = new SpriteExtraInfo(10, null, null);
        clothing2 = new SpriteExtraInfo(10, null, null);
        clothing3 = new SpriteExtraInfo(10, null, null);
        clothing4 = new SpriteExtraInfo(17, null, null);
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
        clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
        clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
        clothing4.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
        clothing1.GetSprite = (s) => SpriteDictionary.FemalePlate[actor.IsAttacking ? 1 : 0];
        clothing2.GetSprite = (s) => SpriteDictionary.FemalePlate[2];
        clothing3.GetSprite = (s) => SpriteDictionary.FemalePlate[3 + actor.GetBodyWeight()];
        if (actor.Unit.HasBreasts)
            clothing4.GetSprite = (s) => SpriteDictionary.FemalePlate[8 + actor.Unit.BreastSize];
        else
            clothing4.GetSprite = null;
        base.Configure(sprite, actor);
    }
}
static class ClothingTypes
{
    internal static BikiniTop BikiniTop = new BikiniTop();
    internal static BikiniBottom BikiniBottom = new BikiniBottom();
    internal static StrapTop StrapTop = new StrapTop();
    internal static BeltTop BeltTop = new BeltTop();
    internal static Shorts Shorts = new Shorts();
    internal static Loincloth Loincloth = new Loincloth();
    internal static Leotard Leotard = new Leotard();
    internal static Rags Rags = new Rags();
    internal static BlackTop BlackTop = new BlackTop();
    internal static FemaleVillager FemaleVillager = new FemaleVillager();
    internal static MaleVillager MaleVillager = new MaleVillager();
    internal static MalePlate MalePlate = new MalePlate();
    internal static FemalePlate FemalePlate = new FemalePlate();

    internal static List<MainClothing> All = new List<MainClothing>()
    {
        BikiniTop,
        BikiniBottom,
        StrapTop,
        BeltTop,
        Shorts,
        Loincloth,
        Leotard,
        Rags,
        BlackTop,
        FemaleVillager,
        MaleVillager,
        FemalePlate,
        MalePlate,
    };
}

