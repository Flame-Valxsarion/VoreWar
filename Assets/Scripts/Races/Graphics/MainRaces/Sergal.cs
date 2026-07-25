using System;
using System.Collections.Generic;
using UnityEngine;

class Sergal : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Sergal;
    public Sergal()
    {
        EyeTypes = 4;
        HairStyles = 8;
        BodySizes = 0;
        MouthTypes = 0;
        SkinColors = 0;

        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        Head = new SpriteExtraInfo(4, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        BodyAccessory = null;
        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        BodyAccent2 = null;
        BodyAccent3 = null;
        BodyAccent4 = null;
        Mouth = null;
        Hair = new SpriteExtraInfo(6, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Hair2 = null;
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(5, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        Weapon = new SpriteExtraInfo(14, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        BreastShadow = null;
        SecondaryBreasts = null;
        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new BaseOutfit(),
            new SergalBikiniTop(),
            new SergalStrapTop(),
            new SergalBlackTop(),
            new SergalRags()
        };
        AvoidedMainClothingTypes = 1;
        AvoidedEyeTypes = 0;
        AllowedWaistTypes = new List<MainClothing>()
        {
            new SergalBikiniBottom(),
            new SergalLoincloth(),
            new SergalShorts()
        };
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        if (Config.HairMatchesFur)
            unit.HairColor = unit.AccessoryColor;
    }

    internal override int BreastSizes => 10;

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Dick, 0, 12 * .625f);
        AddOffset(Balls, 0, 12 * .625f);
        actor.Unit.Furry = true;
    }

    protected override Sprite BodySprite(Actor_Unit actor) => Sprites[0];

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites[1];
        return null;
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.IsAttacking)
            if (actor.BestRanged != null)
                return Sprites[3];
            else return Sprites[65];
        return Sprites[2];

    }

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[4 + actor.Unit.EyeType];

    protected override Sprite HairSprite(Actor_Unit actor) => Sprites[8 + actor.Unit.HairStyle];

    protected override Sprite BreastsSprite(Actor_Unit actor) => actor.Unit.HasBreasts ? Sprites[16 + actor.Unit.BreastSize] : null;

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;

        int size = actor.GetStomachSize(18);
        if (size == 18 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
            return Sprites[45];
        return Sprites[26 + size];
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {

        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            return Sprites[46 + actor.GetWeaponSprite()];
        }
        else
        {
            return null;
        }

    }

    class BaseOutfit : MainClothing
    {

        public BaseOutfit()
        {
            OccupiesAllSlots = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(13, null, null);
            clothing3 = new SpriteExtraInfo(13, null, null);
            clothing4 = new SpriteExtraInfo(6, null, null);
            clothing5 = new SpriteExtraInfo(0, null, null);

            DiscardSprite = SpriteDictionary.Asura[39];

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FurStrict, s.Unit.AccessoryColor);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, s.Unit.ClothingColor);
            clothing1.GetSprite = (s) =>
            {
                clothing1.layer = 13;
                coversBreasts = true;
                if (actor.Unit.BreastSize <= 2)
                    return SpriteDictionary.Sergal[59];
                else if (actor.Unit.BreastSize >= 7)
                {
                    coversBreasts = false;
                    sprite.ChangeLayer(SpriteType.Breasts, 16);
                    return SpriteDictionary.Sergal[64];
                }
                clothing1.layer = 17;
                return SpriteDictionary.Sergal[57 + actor.Unit.BreastSize];
            };
            clothing2.GetSprite = (s) => SpriteDictionary.Sergal[54];
            clothing3.GetSprite = (s) =>
            {
                if (actor.IsErect())
                    return null;
                return SpriteDictionary.Sergal[55];

            };

            clothing4.GetSprite = (s) =>
            {
                if (actor.IsAttacking)
                    if (actor.BestRanged != null)
                        return SpriteDictionary.Sergal[58];
                    else return SpriteDictionary.Sergal[57];
                return SpriteDictionary.Sergal[56];
            };

            base.Configure(sprite, actor);
        }
    }

    class SergalBikiniTop : MainClothing
    {
        public SergalBikiniTop()
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
                clothing1.GetSprite = (s) => SpriteDictionary.SergalClothing[10 + actor.Unit.BreastSize];
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

    class SergalBlackTop : MainClothing
    {
        public SergalBlackTop()
        {
            DiscardSprite = null;
            blocksDick = false;
            Type = 208;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.SergalClothing[actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.SergalClothing[0];
            }
            base.Configure(sprite, actor);
        }
    }

    class SergalStrapTop : MainClothing
    {
        public SergalStrapTop()
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
                int spr = actor.Unit.BreastSize;
                if (actor.Unit.BreastSize < 2)
                    clothing1.GetSprite = (s) => SpriteDictionary.SergalClothing[20];
                else if (actor.Unit.BreastSize < 4)
                    clothing1.GetSprite = (s) => SpriteDictionary.SergalClothing[21];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.SergalClothing[18 + actor.Unit.BreastSize];

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

    class SergalBikiniBottom : MainClothing
    {
        public SergalBikiniBottom()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.BikiniBottom[12];
            Type = 201;
            clothing1 = new SpriteExtraInfo(9, null, null);
            coversBreasts = false;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 3)
                clothing1.GetSprite = (s) => SpriteDictionary.SergalClothing[29];
            else
                clothing1.GetSprite = (s) => SpriteDictionary.SergalClothing[28];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class SergalShorts : MainClothing
    {
        public SergalShorts()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Shorts[12];
            Type = 202;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
            coversBreasts = false;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 2)
            {
                if (actor.Unit.DickSize > 4)
                    clothing1.GetSprite = (s) => SpriteDictionary.SergalClothing[39];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.SergalClothing[38];
            }
            clothing1.GetSprite = (s) => SpriteDictionary.SergalClothing[37];

            base.Configure(sprite, actor);
        }
    }
    class SergalLoincloth : MainClothing
    {
        public SergalLoincloth()
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

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
            clothing1.GetSprite = (s) => SpriteDictionary.SergalClothing[36];
            base.Configure(sprite, actor);
        }
    }
    class SergalRags : MainClothing
    {
        public SergalRags()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Rags[23];
            blocksDick = false;
            inFrontOfDick = 2;
            coversBreasts = false;
            Type = 207;
            OccupiesAllSlots = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(11, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(10, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            clothing1.GetSprite = (s) => SpriteDictionary.SergalClothing[34];

            clothing2.layer = 10;

            if (actor.Unit.BreastSize >= 0)
            {
                clothing2.YOffset = 0;
                if (actor.Unit.BreastSize < 2)
                    clothing2.GetSprite = (s) => SpriteDictionary.SergalClothing[30];
                else
                    clothing2.GetSprite = (s) => SpriteDictionary.SergalClothing[Math.Min(29 + actor.Unit.BreastSize, 33)];
                clothing2.layer = 18;
            }
            else
            {
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Rags[11];
                clothing2.YOffset = 16 * .625f;
            }

            base.ConfigureIgnoreHidingRules(sprite, actor);
        }
    }
}
