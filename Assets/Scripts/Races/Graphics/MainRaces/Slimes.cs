using System.Collections.Generic;
using UnityEngine;

class Slimes : DefaultRaceData
{
    public Slimes()
    {
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SlimeMain);
        EyeTypes = 3;
        EyeColors = 1;
        HairStyles = 12;
        HairColors = 3;
        BodySizes = 2;
        //MouthTypes = 0;

        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, s.Unit.AccessoryColor));
        Head = null;
        BodyAccessory = new SpriteExtraInfo(7, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, 3 * s.Unit.AccessoryColor + s.Unit.HairColor));
        BodyAccent = new SpriteExtraInfo(7, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, 3 * s.Unit.AccessoryColor + s.Unit.HairColor));
        BodyAccent2 = new SpriteExtraInfo(7, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, 3 * s.Unit.AccessoryColor + s.Unit.HairColor));
        BodyAccent3 = null;
        BodyAccent4 = null;
        Mouth = new SpriteExtraInfo(4, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, s.Unit.AccessoryColor));
        Hair = new SpriteExtraInfo(6, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, 3 * s.Unit.AccessoryColor + s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, 3 * s.Unit.AccessoryColor + s.Unit.HairColor));
        Eyes = new SpriteExtraInfo(4, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, s.Unit.AccessoryColor));
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, s.Unit.AccessoryColor));
        Weapon = new SpriteExtraInfo(8, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = new SpriteExtraInfo(6, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, s.Unit.AccessoryColor));
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, s.Unit.AccessoryColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, s.Unit.AccessoryColor));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, s.Unit.AccessoryColor));


        AvoidedMainClothingTypes = 1;
        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            ClothingTypes.BeltTop,
            ClothingTypes.Leotard,
            ClothingTypes.BlackTop,
            RaceSpecificClothing.RainCoat,
            ClothingTypes.Rags,
        };
        AllowedWaistTypes = new List<MainClothing>()
        {
            ClothingTypes.Loincloth,
        };


    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Mouth, 0, 2.5f);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        if (unit.HasDick && unit.HasBreasts)
        {
            if (Config.HermsOnlyUseFemaleHair)
                unit.HairStyle = State.Rand.Next(8);
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
                unit.HairStyle = 5 + State.Rand.Next(7);
            }
            else
            {
                unit.HairStyle = State.Rand.Next(8);
            }
        }
    }

    internal Material GetSlimeAccentMaterial(Actor_Unit actor)
    {
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, 3 * actor.Unit.AccessoryColor + actor.Unit.HairColor).colorSwapMaterial;
    }
    protected override Sprite AccessorySprite(Actor_Unit actor) => SpriteDictionary.Slimes[18];
    protected override Sprite BodySprite(Actor_Unit actor) => SpriteDictionary.Slimes[actor.GetSimpleBodySprite()];
    protected override Sprite BodySizeSprite(Actor_Unit actor) => actor.GetBodyWeight() == 1 ? SpriteDictionary.Slimes[3] : null;
    protected override Sprite BodyAccentSprite(Actor_Unit actor) => SpriteDictionary.Slimes[4 + actor.GetBodyWeight()];
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => SpriteDictionary.Slimes[6 + (actor.IsAttacking ? 1 : 0)];

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (SlimeWeapon(actor))
        {
            Weapon.GetColor = null;
            Weapon.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeSub, 3 * s.Unit.AccessoryColor + s.Unit.HairColor);
        }
        else
        {
            Weapon.GetColor = WhiteColored;
            Weapon.GetPalette = null;
        }

        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            return SpriteDictionary.Slimes[8 + actor.GetWeaponSprite()];
        }
        else
        {
            return null;
        }
    }
    bool SlimeWeapon(Actor_Unit actor)
    {
        return actor.GetWeaponSprite() > 1 && actor.GetWeaponSprite() < 6;
    }

    protected override Sprite HairSprite(Actor_Unit actor) => SpriteDictionary.Slimes[20 + actor.Unit.HairStyle];

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle == 1)
            return SpriteDictionary.Slimes[32];
        if (actor.Unit.HairStyle == 3)
            return SpriteDictionary.Slimes[33];
        if (actor.Unit.HairStyle == 2 || actor.Unit.HairStyle == 4 || actor.Unit.HairStyle == 7)
            return SpriteDictionary.Slimes[34];
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => SpriteDictionary.Slimes[35 + actor.Unit.EyeType];

    protected override Sprite BreastsSprite(Actor_Unit actor) => actor.Unit.HasBreasts ? SpriteDictionary.Slimes[38 + actor.Unit.BreastSize] : null;

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.SetActive(true);

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(15, 1) == 15)
            {
                belly.transform.localScale = new Vector3(1, 1, 1);
                AddOffset(Belly, 0, -25 * .625f);
                return SpriteDictionary.Slimes[69];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                if (actor.GetStomachSize(15, .75f) == 15)
                {
                    belly.transform.localScale = new Vector3(1, 1, 1);
                    AddOffset(Belly, 0, -25 * .625f);
                    return SpriteDictionary.Slimes[68];
                }
                else if (actor.GetStomachSize(15, .875f) == 15)
                {
                    belly.transform.localScale = new Vector3(1, 1, 1);
                    AddOffset(Belly, 0, -25 * .625f);
                    return SpriteDictionary.Slimes[67];
                }



            }

            if (actor.PredatorComponent.VisibleFullness > 4)
            {
                float extraCap = actor.PredatorComponent.VisibleFullness - 4;
                float xScale = Mathf.Min(1 + (extraCap / 5), 1.8f);
                float yScale = Mathf.Min(1 + (extraCap / 40), 1.1f);
                belly.transform.localScale = new Vector3(xScale, yScale, 1);
            }
            else
                belly.transform.localScale = new Vector3(1, 1, 1);
            return SpriteDictionary.Slimes[51 + actor.GetStomachSize()];
        }
        else
        {
            return null;
        }
    }


}
