using System;
using System.Collections.Generic;
using TaurusClothes;
using UnityEngine;

class Bears : DefaultRaceData
{
    public Bears()
    {
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);
        BodySizes = 0;
        MouthTypes = 3;
        BodyAccentTypes1 = 2; // panda
        EarTypes = 2;
        EyeTypes = 7;
        HairStyles = 0;

        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        Head = new SpriteExtraInfo(3, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        BodyAccessory = new SpriteExtraInfo(2, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor)); // ears
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, WhiteColored); // panda
        BodyAccent2 = new SpriteExtraInfo(4, BodyAccentSprite2, WhiteColored); // panda ears
        BodyAccent3 = new SpriteExtraInfo(18, BodyAccentSprite3, WhiteColored); // axe head
        BodyAccent4 = null;
        BodyAccent5 = null;
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        Mouth = new SpriteExtraInfo(5, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        Eyes = new SpriteExtraInfo(5, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        Weapon = new SpriteExtraInfo(12, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null; //new SpriteExtraInfo(3, BodySizeSprite, null, FurryBellyColor);
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        Hair = null;
        Hair2 = null;
        Hair3 = null;

        AvoidedMainClothingTypes = 0;
        //RestrictedClothingTypes = 0;
        //clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            TaurusClothingTypes.Shirt,
            new Bikini(),
            new Leathers()
        };
        AllowedWaistTypes = new List<MainClothing>()
        {
            TaurusClothingTypes.BikiniBottom,
            TaurusClothingTypes.Loincloth,
        };
        AllowedClothingHatTypes = new List<ClothingAccessory>()
        {
            new Hat(),
            MainAccessories.SantaHat
        };
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.ClothingHatType = State.Rand.Next(2);
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.EarType = State.Rand.Next(EarTypes);
    }

    internal override int BreastSizes => 5;
    internal override int DickSizes => 5;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        int sprite = actor.IsAttacking ? 1 : 0;
        if (actor.GetWeaponSprite() == 0 || actor.GetWeaponSprite() == 2)
            sprite = 2;
        if (actor.Unit.HasBreasts == false)
            sprite += 3;

        return State.GameManager.SpriteDictionary.Bears[sprite];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType1 == 0)
        {
            return null;
        }
        int sprite = actor.IsAttacking ? 7 : 6;
        if (actor.GetWeaponSprite() == 0 || actor.GetWeaponSprite() == 2)
            sprite = 8;

        return State.GameManager.SpriteDictionary.Bears[sprite];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType1 == 1)
        {
            return State.GameManager.SpriteDictionary.Bears[65 + actor.Unit.EarType];
        }
        return null;
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if(actor.Unit.HasWeapon && actor.Surrendered == false && actor.GetWeaponSprite() == 2)
        {
            return State.GameManager.SpriteDictionary.Bears[67];
        }
        return null;
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Bears[11 + actor.Unit.EarType];

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.SquishedBreasts)
            return State.GameManager.SpriteDictionary.Bears[Math.Min(60 + actor.Unit.BreastSize, 64)];
        return State.GameManager.SpriteDictionary.Bears[55 + actor.Unit.BreastSize];
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            if (actor.PredatorComponent?.VisibleFullness < .5f)
            {
                Dick.layer = 18;
                return State.GameManager.SpriteDictionary.Bears[38 + actor.Unit.DickSize];
            }
            else
            {
                Dick.layer = 12;
                return State.GameManager.SpriteDictionary.Bears[16 + actor.Unit.DickSize];
            }
        }

        Dick.layer = 9;
        return State.GameManager.SpriteDictionary.Bears[16 + actor.Unit.DickSize];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        int sprite = 9;
        if (actor.IsOralVoring)
            sprite += 1;
        return State.GameManager.SpriteDictionary.Bears[sprite];

    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.GetWeaponSprite() == 5 || actor.GetWeaponSprite() == 7)
        {
            return null;
        }
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            return State.GameManager.SpriteDictionary.Bears[29 + actor.GetWeaponSprite()];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        return State.GameManager.SpriteDictionary.Bears[22 + actor.Unit.EyeType];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {

        return State.GameManager.SpriteDictionary.Bears[13 + actor.Unit.MouthType];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.SetActive(true);

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(11, .95f) == 11)
            {
                belly.transform.localScale = new Vector3(1, 1, 1);
                return State.GameManager.SpriteDictionary.CowsSeliciaBelly[1];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize(11, .95f) == 11)
            {
                belly.transform.localScale = new Vector3(1, 1, 1);
                return State.GameManager.SpriteDictionary.CowsSeliciaBelly[0];
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
            return State.GameManager.SpriteDictionary.Bears[43 + actor.GetStomachSize(11, .95f)];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        //if (actor.Unit.Furry && Config.FurryGenitals)
        //{
        //    int size = actor.Unit.DickSize;
        //    int offset = (int)((actor.PredatorComponent?.BallsFullness ?? 0) * 3);
        //    if (offset > 0)
        //        return State.GameManager.SpriteDictionary.FurryDicks[Math.Min(12 + offset, 23)];
        //    return State.GameManager.SpriteDictionary.FurryDicks[size];
        //}

        int baseSize = 2;
        if (actor.Unit.DickSize == 4)
            baseSize = 8;
        int ballOffset = actor.GetBallSize(21, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 21)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return State.GameManager.SpriteDictionary.Balls[24];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 21)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return State.GameManager.SpriteDictionary.Balls[23];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 20)
        {
            AddOffset(Balls, 0, -15 * .625f);
            return State.GameManager.SpriteDictionary.Balls[22];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 19)
        {
            AddOffset(Balls, 0, -14 * .625f);
            return State.GameManager.SpriteDictionary.Balls[21];
        }
        int combined = Math.Min(baseSize + ballOffset, 20);
        if (combined == 21)
            AddOffset(Balls, 0, -14 * .625f);
        else if (combined == 20)
            AddOffset(Balls, 0, -12 * .625f);
        else if (combined >= 17 && combined <= 19)
            AddOffset(Balls, 0, -8 * .625f);
        if (ballOffset > 0)
        {
            return State.GameManager.SpriteDictionary.Balls[combined];
        }

        return State.GameManager.SpriteDictionary.Balls[baseSize];

    }

    class Leathers : MainClothing
    {
        public Leathers()
        {
            blocksBreasts = true;            
            blocksDick = false;
            Type = 80002;
            clothing1 = new SpriteExtraInfo(17, null, null); //Shirt
            clothing2 = new SpriteExtraInfo(10, null, null); //Skirt
            OccupiesAllSlots = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int spr = 0;
            if (actor.Unit.HasBreasts == false)
            {
                spr = 10 + (actor.IsAttacking ? 1 : 0);
            }               
            else
            {
                spr += actor.IsAttacking ? 5 : 0;
                spr += + actor.Unit.BreastSize;
            }


            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BearsClothes[spr];
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.BearsClothes[12];
            base.Configure(sprite, actor);
        }
    }

    class Bikini : MainClothing
    {
        public Bikini()
        {
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            Type = 82;
            clothing1 = new SpriteExtraInfo(17, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts == false)
                return;


            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BearsClothes[15 + actor.Unit.BreastSize];
            actor.SquishedBreasts = true;
            base.Configure(sprite, actor);
        }
    }

    class Hat : ClothingAccessory
    {
        public Hat()
        {
            clothing1 = new SpriteExtraInfo(6, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.BearsClothes[13];

            base.Configure(sprite, actor);
        }
    }
}

