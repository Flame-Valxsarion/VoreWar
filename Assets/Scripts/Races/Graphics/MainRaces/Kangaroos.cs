using KangarooClothes;
using System;
using System.Collections.Generic;
using UnityEngine;

class Kangaroos : DefaultRaceData
{
    //    The order the sprites should be assembled to stop anything from clashing, aside of the clothing and bellies, is from bottom to front:

    //Body, either base or battle.
    //Arm, if with battle body.
    //Fatness mod, if applicable.
    //Ears, Expression.
    //PG patch if option on.
    //Pouch, if female or herm.
    //Testes, if male or herm and they are on.The diaper loincloth would look weird with the bigger testes though, so nuts should be off if that is used.
    //Gloves, Body Armor, Helmet, Bracelet, Clothing, if worn.
    //Weapon, if equipped.Will occlude the gloves, so has to go after.
    //The fuzz/tail mod. Will partially occlude the anklets of the Body Armor, so has to go after that.
    //Footwear.Occludes few pixels of some of the tail mods, so has to go after those.
    //The gauntlet proxy Bone blade.Will slightly occlude the Bracelet, so has to go after that.
    //Belly.Will, depending on size, occlude a lot of things, so has to go near the end.May also require having clothing off.
    //Leader's necklace. Will occlude the Body Armor.
    //Open mouth. Will occlude part of the expression, some of the Fatness patch, some of Leader's necklace and a sliver of the Body Armor, so has to go after those.
    public Kangaroos()
    {
        HairColors = 0;
        HairStyles = 9; //Ears
        EyeTypes = 23;
        SpecialAccessoryCount = 6; //Lower accessory
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Kangaroo);
        AccessoryColors = 0;
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);

        MouthTypes = 0;

        BodySizes = 4;

        AllowedWaistTypes = new List<MainClothing>();

        AvoidedMainClothingTypes = 1;
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new Loincloth1(),
            new Loincloth2(),
            new Loincloth3(),
            new Loincloth4(),
        };

        Body = new SpriteExtraInfo(1, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kangaroo, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kangaroo, s.Unit.SkinColor));
        BodyAccent = new SpriteExtraInfo(5, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kangaroo, s.Unit.SkinColor));
        BodyAccent2 = new SpriteExtraInfo(12, BodyAccentSprite2, WhiteColored);
        BodyAccent4 = new SpriteExtraInfo(12, BodyAccentSprite4, WhiteColored);
        BodyAccent5 = new SpriteExtraInfo(12, BodyAccentSprite5, WhiteColored);
        BodyAccent3 = new SpriteExtraInfo(13, BodyAccentSprite3, WhiteColored);
        Head = new SpriteExtraInfo(12, HeadSprite, WhiteColored);
        Mouth = new SpriteExtraInfo(14, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(3, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kangaroo, s.Unit.SkinColor));
        Hair2 = null;
        Eyes = new SpriteExtraInfo(2, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kangaroo, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(10, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = new SpriteExtraInfo(4, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kangaroo, s.Unit.SkinColor));
        Breasts = null;
        BreastShadow = null;
        Dick = new SpriteExtraInfo(8, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(9, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kangaroo, s.Unit.SkinColor));

        AvoidedMouthTypes = 0;
        AvoidedEyeTypes = 6;
    }

    internal override int DickSizes => 6;
    internal override int BreastSizes => 1;

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        if (unit.Type == UnitType.Mercenary)
        {
            if (State.Rand.Next(3) == 0)
                unit.EyeType = 22 - State.Rand.Next(3);
        }
        else
        {
            unit.HairStyle = State.Rand.Next(Math.Max(HairStyles - 2, 0));
            unit.SkinColor = State.Rand.Next(Math.Max(SkinColors - 4, 0));
        }
    }

    protected override Sprite BodySprite(Actor_Unit actor) => SpriteDictionary.Kangaroos[(actor.Unit.HasWeapon || actor.IsAttacking) ? 1 : 0];

    protected override Sprite MouthSprite(Actor_Unit actor) => actor.IsEating ? SpriteDictionary.Kangaroos[2] : null;

    protected override Sprite EyesSprite(Actor_Unit actor) => SpriteDictionary.Kangaroos[6 + actor.Unit.EyeType];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            Belly.layer = 15;
            belly.SetActive(true);

            if (actor.PredatorComponent.VisibleFullness > 4)
            {
                float extraCap = actor.PredatorComponent.VisibleFullness - 4;
                float xScale = Mathf.Min(1 + (extraCap / 5), 1.8f);
                float yScale = Mathf.Min(1 + (extraCap / 40), 1.1f);
                belly.transform.localScale = new Vector3(xScale, yScale, 1);
            }
            else
                belly.transform.localScale = new Vector3(1, 1, 1);
            int sprite = actor.GetStomachSize(19, .8f);
            if (actor.Unit.HasBreasts)
            {
                if (sprite == 19 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                    return SpriteDictionary.Kangaroos[136];
                if (sprite <= 15)
                    return SpriteDictionary.Kangaroos[78 + sprite];
                return SpriteDictionary.Kangaroos[132 - 16 + sprite];
            }
            if (sprite == 19 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                return SpriteDictionary.Kangaroos[131];
            if (sprite <= 15)
                return SpriteDictionary.Kangaroos[62 + sprite];
            return SpriteDictionary.Kangaroos[127 - 16 + sprite];
        }
        else
        {
            Belly.layer = 7;
            if (actor.Unit.HasBreasts)
                return SpriteDictionary.Kangaroos[3];
            return null;
        }
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.DickSize < 0) return null;

        if (actor.Unit.DickSize >= 0)
        {
            if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false)
            {
                if (actor.PredatorComponent.BallsFullness > 3)
                {
                    return SpriteDictionary.Kangaroos[148];
                }
            }

            if (actor.PredatorComponent?.BallsFullness > 0)
            {
                return SpriteDictionary.Kangaroos[137 + actor.GetBallSize(10)];
            }
            return SpriteDictionary.Kangaroos[50 + actor.Unit.DickSize];
        }

        return null;
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.DickSize >= 0 && actor.IsErect())
            return SpriteDictionary.Kangaroos[56 + actor.Unit.DickSize];
        return null;
    }

    protected override Sprite HairSprite(Actor_Unit actor) => SpriteDictionary.Kangaroos[41 + actor.Unit.HairStyle];

    protected override Sprite AccessorySprite(Actor_Unit actor) => SpriteDictionary.Kangaroos[98 + actor.Unit.SpecialAccessoryType];

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon == false || actor.Surrendered)
            return null;
        switch (actor.GetWeaponSprite())
        {
            case 0:
                return SpriteDictionary.Kangaroos[33];
            case 1:
                return SpriteDictionary.Kangaroos[35];
            case 2:
                return SpriteDictionary.Kangaroos[36];
            case 3:
                return SpriteDictionary.Kangaroos[37];
            case 4:
                return SpriteDictionary.Kangaroos[30];
            case 5:
                return null;
            case 6:
                return SpriteDictionary.Kangaroos[38];
            case 7:
                return SpriteDictionary.Kangaroos[40];
            default:
                return null;
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking)
                return SpriteDictionary.Kangaroos[34];
            else return null;
        }
        switch (actor.GetWeaponSprite())
        {
            case 0:
                return SpriteDictionary.Kangaroos[32];
            case 1:
                return SpriteDictionary.Kangaroos[34];
            case 2:
                return SpriteDictionary.Kangaroos[29];
            case 3:
                return SpriteDictionary.Kangaroos[39];
            case 4:
                return SpriteDictionary.Kangaroos[29];
            case 5:
                return SpriteDictionary.Kangaroos[31];
            case 6:
                return SpriteDictionary.Kangaroos[29];
            case 7:
                return SpriteDictionary.Kangaroos[39];
            default:
                return null;
        }
    }

    protected override Sprite BodySizeSprite(Actor_Unit actor)
    {
        if (actor.Unit.BodySize > 0)
            return SpriteDictionary.Kangaroos[103 + actor.Unit.BodySize];
        return null;
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        Accessory acc = null;
        if (actor.Unit.Items == null || actor.Unit.Items.Length < 1)
            return null;
        if (actor.Unit.Items[0] is Accessory)
            acc = (Accessory)actor.Unit.Items[0];
        return SpriteForAccessory(actor, ref acc);
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        Accessory acc = null;
        if (actor.Unit.Items == null || actor.Unit.Items.Length < 2)
            return null;
        if (actor.Unit.Items[1] is Accessory)
            acc = (Accessory)actor.Unit.Items[1];
        return SpriteForAccessory(actor, ref acc);
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        Accessory acc = null;
        if (actor.Unit.Items == null || actor.Unit.Items.Length < 3)
            return null;
        if (actor.Unit.Items[2] is Accessory)
            acc = (Accessory)actor.Unit.Items[2];
        return SpriteForAccessory(actor, ref acc);
    }

    private static Sprite SpriteForAccessory(Actor_Unit actor, ref Accessory acc)
    {
        if (acc == null)
            return null;
        if (acc == State.World.ItemRepository.GetItem(ItemType.BodyArmor))
        {
            if (actor.Unit.EyeType % 2 == 0)
                return SpriteDictionary.Kangaroos[107];
            return SpriteDictionary.Kangaroos[108];
        }
        if (acc == State.World.ItemRepository.GetItem(ItemType.Helmet))
            return SpriteDictionary.Kangaroos[109];
        if (acc == State.World.ItemRepository.GetItem(ItemType.Shoes))
            return SpriteDictionary.Kangaroos[116];
        if (acc == State.World.ItemRepository.GetItem(ItemType.Gloves))
        {
            if (actor.Unit.HasWeapon == false || actor.Surrendered)
                return SpriteDictionary.Kangaroos[117];
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return SpriteDictionary.Kangaroos[120];
                case 1:
                    return SpriteDictionary.Kangaroos[121];
                case 2:
                    return SpriteDictionary.Kangaroos[118];
                case 3:
                    return SpriteDictionary.Kangaroos[122];
                case 4:
                    return SpriteDictionary.Kangaroos[118];
                case 5:
                    return SpriteDictionary.Kangaroos[119];
                case 6:
                    return SpriteDictionary.Kangaroos[118];
                case 7:
                    return SpriteDictionary.Kangaroos[122];
                default:
                    return SpriteDictionary.Kangaroos[117];
            }
        }
        if (acc == State.World.ItemRepository.GetItem(ItemType.Gauntlet))
            return SpriteDictionary.Kangaroos[5];
        return null;
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (actor.Unit.Type == UnitType.Leader)
            return SpriteDictionary.Kangaroos[115];
        return null;
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.Unit.Level > 15)
            return SpriteDictionary.Kangaroos[114];
        else if (actor.Unit.Level > 10)
            return SpriteDictionary.Kangaroos[113];
        else if (actor.Unit.Level > 7)
            return SpriteDictionary.Kangaroos[112];
        else if (actor.Unit.Level > 5)
            return SpriteDictionary.Kangaroos[111];
        else if (actor.Unit.Level > 3)
            return SpriteDictionary.Kangaroos[110];
        return null;
    }

}


namespace KangarooClothes
{
    class Loincloth1 : MainClothing
    {
        public Loincloth1()
        {
            Type = 755;
            OccupiesAllSlots = true;
            DiscardSprite = SpriteDictionary.Kangaroos[123];
            clothing1 = new SpriteExtraInfo(10, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
            clothing1.GetSprite = (s) => SpriteDictionary.Kangaroos[94];
            base.Configure(sprite, actor);
        }
    }
    class Loincloth2 : MainClothing
    {
        public Loincloth2()
        {
            Type = 756;
            OccupiesAllSlots = true;
            DiscardSprite = SpriteDictionary.Kangaroos[124];
            clothing1 = new SpriteExtraInfo(10, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
            clothing1.GetSprite = (s) => SpriteDictionary.Kangaroos[95];
            base.Configure(sprite, actor);
        }
    }
    class Loincloth3 : MainClothing
    {
        public Loincloth3()
        {
            Type = 756;
            OccupiesAllSlots = true;
            DiscardSprite = SpriteDictionary.Kangaroos[124];
            clothing1 = new SpriteExtraInfo(10, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
            clothing1.GetSprite = (s) => SpriteDictionary.Kangaroos[96];
            base.Configure(sprite, actor);
        }
    }
    class Loincloth4 : MainClothing
    {
        public Loincloth4()
        {
            Type = 756;
            OccupiesAllSlots = true;
            FixedColor = true;
            DiscardSprite = SpriteDictionary.Kangaroos[124];
            clothing1 = new SpriteExtraInfo(10, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            clothing1.GetSprite = (s) => SpriteDictionary.Kangaroos[97];
            base.Configure(sprite, actor);
        }
    }
}

