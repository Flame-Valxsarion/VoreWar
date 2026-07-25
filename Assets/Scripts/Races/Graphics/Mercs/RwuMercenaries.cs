using System.Collections.Generic;
using UnityEngine;

class RwuMercenaries : BlankSlate
{
    readonly Sprite[] Sprites1 = SpriteDictionary.RwuMercenaries1;
    readonly Sprite[] Sprites2 = SpriteDictionary.RwuMercenaries2;
    readonly Sprite[] Sprites3 = SpriteDictionary.RwuMercenaries3;
    readonly Sprite[] Sprites4 = SpriteDictionary.RwuMercenaries4;
    readonly Sprite[] Sprites5 = SpriteDictionary.RwuMercenaries5;
    readonly Sprite[] Sprites6 = SpriteDictionary.RwuMercenaries6;
    readonly Sprite[] Sprites7 = SpriteDictionary.RwuMercenaries7;
    readonly Sprite[] Sprites8a = SpriteDictionary.RwuMercenaries8a;
    readonly Sprite[] Sprites8b = SpriteDictionary.RwuMercenaries8b;

    internal RwuMercenaries()
    {
        SpecialAccessoryCount = 0;
        AvoidedEyeTypes = 0;
        AvoidedMouthTypes = 0;

        HairColors = 1;
        HairStyles = 2;//Standard armor scheme;for sharks
        SkinColors = 1;
        EyeTypes = 4;//Race
        EyeColors = 1;
        SecondaryEyeColors = 1;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);
        BodySizes = 2;//Simple Light set and Heavy set
        BodyAccentTypes1 = 1;//Armor set
        BodyAccentTypes2 = 7;//Race Detail/Accessories
        BodyAccentTypes5 = 1;//Claws;for lizards
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            cape1,
            cape2
        };
        AllowedWaistTypes = new List<MainClothing>();
        AllowedClothingHatTypes = new List<ClothingAccessory>();
        AvoidedMainClothingTypes = 0;
        MouthTypes = 2;


        ExtendedBreastSprites = false;

        Body = new SpriteExtraInfo(3, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));//Race Detail/Accessories
        Head = new SpriteExtraInfo(6, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, s.Unit.ClothingColor));//Armor
        BodyAccessory = new SpriteExtraInfo(11, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, s.Unit.ClothingColor));//Over-Faulding
        BodyAccent = new SpriteExtraInfo(16, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, s.Unit.ClothingColor));//Breasts
        BodyAccent2 = new SpriteExtraInfo(7, BodyAccentSprite4, WhiteColored); //Claws
        BodyAccent3 = null;
        BodyAccent4 = null;
        BodyAccent5 = null;
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        BodyAccent9 = null;
        BodyAccent10 = null;
        Mouth = new SpriteExtraInfo(6, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, s.Unit.ClothingColor));//Helmet
        Hair = null;
        Hair2 = null;
        Eyes = null;
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(15, null, WhiteColored);
        SecondaryBelly = null;
        Weapon = new SpriteExtraInfo(13, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = null;
        Dick = null;
        Balls = new SpriteExtraInfo(9, BallsSprite, WhiteColored);
    }
    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
        unit.EyeType = State.Rand.Next(0, EyeTypes);
        unit.BodySize = State.Rand.Next(2);
        unit.HairStyle = (0);
        unit.ClothingColor = (17);
        unit.ClothingType = State.Rand.Next(3);
    }
    internal override int BreastSizes => 5;
    internal override int DickSizes => 1;

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
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
            switch (actor.Unit.EyeType)
            {
                case 0:
                    return Sprites1[29 + actor.GetStomachSize(14)];
                case 1:
                    return Sprites3[29 + actor.GetStomachSize(14)];
                case 2:
                    return Sprites5[29 + actor.GetStomachSize(14)];
                case 3:
                    return Sprites7[29 + actor.GetStomachSize(14)];
                default:
                    return Sprites1[29 + actor.GetStomachSize(14)];
            }
        }
        else
        {
            return null;
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        switch (actor.Unit.EyeType)
        {
            case 0:
                return Sprites2[16 + actor.Unit.MouthType];
            case 1:
                return Sprites4[16 + actor.Unit.MouthType];
            case 2:
                return Sprites6[16 + actor.Unit.MouthType];
            case 3:
                return Sprites8a[16 + actor.Unit.MouthType];
            default:
                return Sprites2[16 + actor.Unit.MouthType];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        switch (actor.Unit.EyeType)
        {
            case 0:
                if (actor.IsAttacking)
                    return Sprites2[1 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
                return Sprites2[0 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
            case 1:
                if (actor.IsAttacking)
                    return Sprites4[1 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
                return Sprites4[0 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
            case 2:
                if (actor.IsAttacking)
                    return Sprites6[1 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
                return Sprites6[0 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
            case 3:
                if (actor.Unit.HairStyle == 1)
                {
                    if (actor.IsAttacking)
                        return Sprites8b[1 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
                    return Sprites8b[0 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
                }
                if (actor.IsAttacking)
                    return Sprites8a[1 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
                return Sprites8a[0 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
            default:
                if (actor.IsAttacking)
                    return Sprites2[1 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
                return Sprites2[0 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        switch (actor.Unit.EyeType)
        {
            case 0:
                return Sprites1[22 + actor.Unit.BodyAccentType2];
            case 1:
                return Sprites3[22 + actor.Unit.BodyAccentType2];
            case 2:
                return Sprites5[22 + actor.Unit.BodyAccentType2];
            case 3:
                return Sprites7[22 + actor.Unit.BodyAccentType2];
            default:
                return Sprites1[22 + actor.Unit.BodyAccentType2];
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        switch (actor.Unit.EyeType)
        {
            case 0:
                return Sprites2[2 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
            case 1:
                return Sprites4[2 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
            case 2:
                return Sprites6[2 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
            case 3:
                if (actor.Unit.HairStyle == 1)
                    return Sprites8b[2 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
                return Sprites8a[2 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
            default:
                return Sprites2[2 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
        }
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        switch (actor.Unit.EyeType)
        {
            case 0:
                return Sprites1[14 + (actor.IsAttacking ? 1 : 0)];
            case 1:
                return Sprites3[14 + (actor.IsAttacking ? 1 : 0)];
            case 2:
                return Sprites5[14 + (actor.IsAttacking ? 1 : 0)];
            case 3:
                return Sprites7[14 + (actor.IsAttacking ? 1 : 0)];
            default:
                return Sprites1[14 + (actor.IsAttacking ? 1 : 0)];
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        if (actor.Unit.BreastSize == 0)
            return null;
        switch (actor.Unit.EyeType)
        {
            case 0:
                if (actor.Unit.HasBreasts)
                    return Sprites2[17 + actor.Unit.BreastSize];
                return null;
            case 1:
                if (actor.Unit.HasBreasts)
                    return Sprites4[17 + actor.Unit.BreastSize];
                return null;
            case 2:
                if (actor.Unit.HasBreasts)
                    return Sprites6[17 + actor.Unit.BreastSize];
                return null;
            case 3:
                if (actor.Unit.HasBreasts)
                {
                    if (actor.Unit.HairStyle == 1)
                        return Sprites8b[17 + actor.Unit.BreastSize];
                    return Sprites8a[17 + actor.Unit.BreastSize];
                }
                return null;
            default:
                if (actor.Unit.HasBreasts)
                    return Sprites2[17 + actor.Unit.BreastSize];
                return null;
        }

    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.PredatorComponent?.BallsFullness > 0)
        {
            if ((actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls)) || (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls)))
            {
        switch (actor.Unit.EyeType)
                {
            case 0:
                return Sprites1[45 + actor.GetBallSize(7)];
            case 1:
                return Sprites3[45 + actor.GetBallSize(7)];
            case 2:
                return Sprites5[45 + actor.GetBallSize(7)];
            case 3:
                return Sprites7[45 + actor.GetBallSize(7)];
            default:
                return Sprites1[45 + actor.GetBallSize(7)];
                }
            }
        switch (actor.Unit.EyeType)
            {
            case 0:
                return Sprites1[45 + actor.GetBallSize(7)];
            case 1:
                return Sprites3[45 + actor.GetBallSize(7)];
            case 2:
                return Sprites5[45 + actor.GetBallSize(7)];
            case 3:
                return Sprites7[45 + actor.GetBallSize(7)];
            default:
                return Sprites1[45 + actor.GetBallSize(7)];
            }
        }
        return null;
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
        switch (actor.Unit.EyeType)
            {
            case 0:
                return Sprites1[53 + actor.GetWeaponSprite()];
            case 1:
                return Sprites3[53 + actor.GetWeaponSprite()];
            case 2:
                return Sprites5[53 + actor.GetWeaponSprite()];
            case 3:
                return Sprites7[53 + actor.GetWeaponSprite()];
            default:
                return Sprites1[53 + actor.GetWeaponSprite()];
            }
        }
        else
        {
            return null;
        }
    }

    Cape1 cape1 = new Cape1();
    Cape2 cape2 = new Cape2();

    class Cape1 : MainClothing
    {
        public Cape1()
        {
            coversBreasts = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.EyeType == 0)
                clothing1 = new SpriteExtraInfo(17, null, null);
            else
                clothing1 = new SpriteExtraInfo(1, null, null);
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
            switch (actor.Unit.EyeType)
            {
                case 0:
                    clothing1.GetSprite = (s) => SpriteDictionary.RwuMercenaries2[6];
                    break;
                case 1:
                    clothing1.GetSprite = (s) => SpriteDictionary.RwuMercenaries4[6];
                    break;
                case 2:
                    clothing1.GetSprite = (s) => SpriteDictionary.RwuMercenaries6[6];
                    break;
                case 3:
                    clothing1.GetSprite = (s) => SpriteDictionary.RwuMercenaries8a[6];
                    break;
                default:
                    clothing1.GetSprite = (s) => SpriteDictionary.RwuMercenaries2[6];
                    break;
            }
            base.Configure(sprite, actor);
        }
    }

    class Cape2 : MainClothing
    {
        public Cape2()
        {
            coversBreasts = true;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(17, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.EyeType == 0)
                clothing1 = new SpriteExtraInfo(17, null, null);
            else
                clothing1 = new SpriteExtraInfo(1, null, null);
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, actor.Unit.ClothingColor);
            switch (actor.Unit.EyeType)
            {
                case 0:
                    clothing1.GetSprite = (s) => SpriteDictionary.RwuMercenaries2[13];
                    break;
                case 1:
                    clothing1.GetSprite = (s) => SpriteDictionary.RwuMercenaries4[13];
                    break;
                case 2:
                    clothing1.GetSprite = (s) => SpriteDictionary.RwuMercenaries6[13];
                    break;
                case 3:
                    clothing1.GetSprite = (s) => SpriteDictionary.RwuMercenaries8a[13];
                    break;
                default:
                    clothing1.GetSprite = (s) => SpriteDictionary.RwuMercenaries2[13];
                    break;
            }
            base.Configure(sprite, actor);
        }
    }


    protected override Sprite AccessorySprite(Actor_Unit actor) => null;
    protected override Sprite BackWeaponSprite(Actor_Unit actor) => null;
    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BodySprite(Actor_Unit actor) => null;
    internal override Color ClothingColor(Actor_Unit actor) => Color.white;
    protected override Sprite DickSprite(Actor_Unit actor) => null;
    protected override Color EyeColor(Actor_Unit actor) => Color.white;
    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => null;
    protected override Sprite EyesSprite(Actor_Unit actor) => null;
    protected override Color HairColor(Actor_Unit actor) => Color.white;
    protected override Sprite HairSprite(Actor_Unit actor) => null;
    protected override Sprite HairSprite2(Actor_Unit actor) => null;
    protected override Color ScleraColor(Actor_Unit actor) => Color.white;
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => null;


}

