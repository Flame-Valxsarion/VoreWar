using DriderClothing;
using System.Collections.Generic;
using UnityEngine;


class Driders : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Driders;
    readonly float yOffset = 30 * .625f;
    readonly DriderLeader LeaderClothes;
    public Driders()
    {
        BodySizes = 5;
        EyeTypes = 8;
        HairStyles = 10;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DriderSkin); // abdomen and legs colors
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DriderEyes); // drider special eyes colors
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DriderEyes); // abdomen patterns colors

        Body = new SpriteExtraInfo(3, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(7, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderSkin, s.Unit.AccessoryColor));
        BodyAccessory = new SpriteExtraInfo(2, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderSkin, s.Unit.AccessoryColor)); //abdomen
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderSkin, s.Unit.AccessoryColor)); //Back Legs
        BodyAccent2 = new SpriteExtraInfo(5, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderSkin, s.Unit.AccessoryColor)); //Back of front Legs
        BodyAccent3 = new SpriteExtraInfo(7, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderEyes, s.Unit.EyeColor)); // extra Eyes
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor)); //eyebrow
        BodyAccent5 = new SpriteExtraInfo(18, BodyAccentSprite5, WhiteColored); //Extra Leg Accessories
        BodyAccent6 = new SpriteExtraInfo(17, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderSkin, s.Unit.AccessoryColor)); //Front of front Legs
        BodyAccent7 = new SpriteExtraInfo(7, BodyAccentSprite7, WhiteColored); //For Spider Web attack animation???
        Mouth = new SpriteExtraInfo(4, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        Hair = new SpriteExtraInfo(6, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Hair2 = null;
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(5, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderEyes, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = new SpriteExtraInfo(3, SecondaryAccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderEyes, s.Unit.ExtraColor1)); // abdomen patterns
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(6, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, s.Unit.SkinColor));

        LeaderClothes = new DriderLeader();


        ClothingShift = new Vector3(0, yOffset, 0);
        AvoidedMainClothingTypes = 2;
        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            ClothingTypes.BikiniTop,
            ClothingTypes.BeltTop,
            ClothingTypes.StrapTop,
            ClothingTypes.BlackTop,
            ClothingTypes.Rags,
            LeaderClothes
        };
        AllowedWaistTypes = new List<MainClothing>()
        {
            ClothingTypes.Loincloth,
        };
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        if (unit.Type == UnitType.Leader)
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(LeaderClothes);
        if (unit.HasDick && unit.HasBreasts)
        {
            if (Config.HermsOnlyUseFemaleHair)
                unit.HairStyle = State.Rand.Next(5);
            else
                unit.HairStyle = State.Rand.Next(HairStyles);
        }
        else if (unit.HasDick && Config.FemaleHairForMales)
            unit.HairStyle = State.Rand.Next(HairStyles);
        else if (unit.HasDick == false && Config.MaleHairForFemales)
            unit.HairStyle = State.Rand.Next(HairStyles);
        else
        {
            if (unit.HasDick)
            {
                unit.HairStyle = 5 + State.Rand.Next(5);
            }
            else
            {
                unit.HairStyle = State.Rand.Next(5);
            }
        }
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Mouth, 0, yOffset);
        AddOffset(Breasts, 0, yOffset);
        AddOffset(Dick, 0, yOffset);
        AddOffset(Belly, 0, yOffset);
        AddOffset(Balls, 0, yOffset);
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[0 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodySize)];
        }
        else
        {
            if (actor.Unit.BodySize < 1)
            {
                return Sprites[0 + (actor.IsAttacking ? 1 : 0)];
            }
            else
            {
                return Sprites[8 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodySize)];
            }
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites[23];
        return null;
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // abdomen
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[18 + actor.Unit.BodySize];
        }
        else
        {
            if (actor.Unit.BodySize < 2)
            {
                return Sprites[18];
            }
            else
            {
                return Sprites[17 + actor.Unit.BodySize];
            }
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites[24]; // back legs

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites[25 + actor.GetSimpleBodySprite()]; // back of front legs

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) => Sprites[36]; // extra eyes

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) => Sprites[37]; // Eyebrow

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // extra leg accessorries
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            return Sprites[61 + actor.GetSimpleBodySprite() + (3 * (actor.GetWeaponSprite() / 2))];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) => Sprites[98 + actor.GetSimpleBodySprite()]; // front of front legs

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) => null; // leaving space for spider web animation
    //it should first start with appeareance of sprite 73 (open spinneret) then it should change into sprite 74 (web gathering in the spinneret). After that it should shoot the web (sprite 75)
    // additionaly bodyaccentsprite2, 5 and 6 should also switch accordingly to "voring" settings if possible. No need for headsprite to appear though

    protected override Sprite HairSprite(Actor_Unit actor) => Sprites[38 + actor.Unit.HairStyle];

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[28 + actor.Unit.EyeType];

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) // abdomen patterns
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[48 + actor.Unit.BodySize];
        }
        else
        {
            if (actor.Unit.BodySize < 2)
            {
                return Sprites[48];
            }
            else
            {
                return Sprites[47 + actor.Unit.BodySize];
            }
        }
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            return Sprites[53 + actor.GetWeaponSprite()];
        }
        else
        {
            return null;
        }
    }
}

namespace DriderClothing
{
    class DriderLeader : MainClothing
    {
        public DriderLeader()
        {
            leaderOnly = true;
            FixedColor = true;
            blocksDick = false;
            inFrontOfDick = 1;
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(10, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(11, null, WhiteColored);
            OccupiesAllSlots = true;
            DiscardSprite = SpriteDictionary.Driders[96];
            Type = 236;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.HasBreasts)
            {
                DiscardSprite = SpriteDictionary.Driders[97];
                Type = 237;
                clothing1.GetSprite = (s) => SpriteDictionary.Driders[76];
                clothing2.GetSprite = (s) => SpriteDictionary.Driders[87 + actor.Unit.BreastSize];
                clothing3.GetSprite = (s) => SpriteDictionary.Driders[82 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Driders[95];
                clothing2.GetSprite = null;
                clothing3.GetSprite = (s) => SpriteDictionary.Driders[77 + actor.Unit.BodySize];
            }

            base.Configure(sprite, actor);
        }
    }
}