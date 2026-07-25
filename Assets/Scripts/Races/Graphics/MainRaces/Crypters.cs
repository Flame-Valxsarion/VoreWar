using System.Collections.Generic;
using UnityEngine;

class Crypters : DefaultRaceData
{
    public Crypters()
    {
        Color bellyColor = new Color(.2519f, .2519f, .3584f);

        EyeTypes = 4;
        MouthTypes = 4;
        BodySizes = 7;
        HairStyles = base.HairStyles + 4;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);

        WeightGainDisabled = true;

        Body = new SpriteExtraInfo(2, BodySprite, WhiteColored);
        Head = new SpriteExtraInfo(3, HeadSprite, WhiteColored);
        BodyAccessory = new SpriteExtraInfo(3, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        BodyAccent = new SpriteExtraInfo(7, BodyAccentSprite, WhiteColored);
        BodyAccent2 = new SpriteExtraInfo(7, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        BodyAccent3 = new SpriteExtraInfo(7, BodyAccentSprite3, WhiteColored);
        BodyAccent4 = new SpriteExtraInfo(7, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, s.Unit.AccessoryColor));
        Mouth = new SpriteExtraInfo(4, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(12, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        //Hair = new SpriteExtraInfo(6, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        //Hair2 = null;
        Eyes = new SpriteExtraInfo(4, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = new SpriteExtraInfo(3, SecondaryAccessorySprite, WhiteColored);
        Belly = new SpriteExtraInfo(15, null, (s) => bellyColor);
        Weapon = new SpriteExtraInfo(1, WeaponSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CrypterWeapon, s.Unit.AccessoryColor));
        BackWeapon = null;
        BodySize = new SpriteExtraInfo(6, BodySizeSprite, WhiteColored);
        Breasts = null;

        Dick = null;
        Balls = null;

        AvoidedMainClothingTypes = 0;
        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
        AllowedMainClothingTypes = new List<MainClothing>();
        AllowedWaistTypes = new List<MainClothing>();

        AvoidedMouthTypes = 0;
        AvoidedEyeTypes = 0;




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
            unit.HairStyle = State.Rand.Next(15);
        else if (unit.HasDick == false && Config.MaleHairForFemales)
            unit.HairStyle = State.Rand.Next(15);
        else
        {
            if (unit.HasDick)
            {
                unit.HairStyle = 8 + State.Rand.Next(7);
            }
            else
            {
                unit.HairStyle = State.Rand.Next(8);
            }
        }
    }

    internal override int DickSizes => 1;
    internal override int BreastSizes => 1;


    protected override Sprite BodySprite(Actor_Unit actor) => SpriteDictionary.Crypters[0];

    protected override Sprite HeadSprite(Actor_Unit actor) => SpriteDictionary.Crypters[2];

    protected override Sprite AccessorySprite(Actor_Unit actor) => SpriteDictionary.Crypters[28];

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => SpriteDictionary.Crypters[4];

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => SpriteDictionary.Crypters[5];

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) => SpriteDictionary.Crypters[6 + (actor.IsEating ? 1 : 0)];

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) => SpriteDictionary.Crypters[36];

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle < 15)
        {
            AddOffset(Hair, 0, 1.25f);
            AddOffset(Hair2, 0, 1.25f);
            return base.HairSprite(actor);
        }

        AddOffset(Hair, 0, 0);
        AddOffset(Hair2, 0, 0);
        return SpriteDictionary.Crypters[12 + actor.Unit.HairStyle - 15];
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle == 1)
            return State.GameManager.SpriteDictionary.Hair[HairStyles - 4];
        else if (actor.Unit.HairStyle == 2)
            return State.GameManager.SpriteDictionary.Hair[HairStyles + 1 - 4];
        else if (actor.Unit.HairStyle == 5)
            return State.GameManager.SpriteDictionary.Hair[HairStyles + 3 - 4];
        else if (actor.Unit.HairStyle == 6 || actor.Unit.HairStyle == 7)
            return State.GameManager.SpriteDictionary.Hair[HairStyles + 2 - 4];
        return null;
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        return SpriteDictionary.Crypters[8 + actor.Unit.EyeType];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.Unit.MouthType > 3) //Defending against a weird exception.
            actor.Unit.MouthType = 3;
        return SpriteDictionary.Crypters[37 + (2 * actor.Unit.MouthType) + (actor.IsEating ? 1 : 0)];
    }

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor)
    {
        return SpriteDictionary.Crypters[3];
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            return SpriteDictionary.Crypters[19 + actor.GetWeaponSprite()];
        }
        else
        {
            return SpriteDictionary.Crypters[17 + (actor.IsAttacking ? 1 : 0)];
        }
    }

    protected override Sprite BodySizeSprite(Actor_Unit actor)
    {
        return SpriteDictionary.Crypters[29 + actor.Unit.BodySize];
    }
}

