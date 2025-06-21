using System;
using System.Collections.Generic;
using TacticalBuildings;
using TaurusClothes;
using UnityEngine;

class Equaleon : DefaultRaceData
{
    readonly EeveeRags Rags;

    public Equaleon()
    {
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EeveeEqualeonSkin);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EeveeEqualeonSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EqualeonEyes);
        EyeTypes = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EqualeonEyes);
        BodySizes = 0;
        BodyAccentTypes2 = 9;
        EarTypes = 8;
        HairStyles = 10;
        clothingColors = 3;

        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(24, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(25, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.SkinColor)); // Ears
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, WhiteColored); // rub blush
        BodyAccent2 = new SpriteExtraInfo(12, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.AccessoryColor)); // Ear Fluff
        BodyAccent3 = new SpriteExtraInfo(3, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.AccessoryColor)); // Hand Fluff
        BodyAccent4 = new SpriteExtraInfo(3, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.AccessoryColor)); // Feet Fluff 
        BodyAccent5 = null; 
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        Mouth = new SpriteExtraInfo(4, MouthSprite, WhiteColored);        
        Eyes = new SpriteExtraInfo(4, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EqualeonEyes, s.Unit.EyeColor));
        SecondaryEyes = new SpriteExtraInfo(4, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EqualeonEyes, s.Unit.EyeType));
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.SkinColor));
        Beard = null; 
        Weapon = new SpriteExtraInfo(12, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null; //new SpriteExtraInfo(3, BodySizeSprite, null, FurryBellyColor);
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(9, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.SkinColor));
        Hair = new SpriteExtraInfo(26, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonSkin, s.Unit.AccessoryColor));
        Hair2 = null;
        Hair3 = null;

        AvoidedMainClothingTypes = 1;
        Rags = new EeveeRags();
        //RestrictedClothingTypes = 0;
        //clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new Belt(),
            new Strap(),
            new Leotard(),
            new Outfit1(),
            new Outfit2(),
            Rags,
        };

        AllowedWaistTypes = new List<MainClothing>()
        {
            new Loincloth(),
            new Shorts(),
        };
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.EarType = State.Rand.Next(EarTypes);
    }

    internal override int BreastSizes => 6;
    internal override int DickSizes => 7;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        int sprite = actor.IsAttacking ? 21 : 20;

        return State.GameManager.SpriteDictionary.Equaleon[sprite];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => actor.BeingRubbed ? State.GameManager.SpriteDictionary.Eevee[26] : null;

    protected override Sprite AccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Equaleon[0 + actor.Unit.EarType];

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f));

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

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            if (actor.PredatorComponent?.VisibleFullness < .75f)
            {
                Dick.layer = 21;
                return State.GameManager.SpriteDictionary.Umbreon2[35 + (actor.Unit.DickSize * 2)];
            }
            else
            {
                Dick.layer = 14;
                return State.GameManager.SpriteDictionary.Umbreon2[36 + (actor.Unit.DickSize * 2)];
            }
        }

        return null;
    }
    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => State.GameManager.SpriteDictionary.Equaleon[22 + actor.Unit.EarType];
    protected override Sprite BodyAccentSprite3(Actor_Unit actor) => State.GameManager.SpriteDictionary.Equaleon[actor.IsAttacking ? 34 : 33];
    protected override Sprite BodyAccentSprite4(Actor_Unit actor) => State.GameManager.SpriteDictionary.Equaleon[actor.IsAttacking ? 36 : 35];
    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        if (actor.HasBelly)
        {
            int size = actor.GetStomachSize(31, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return null;
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return null;
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return null;
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return null;
            }
            switch (size)
            {
                case 26:
                    AddOffset(Belly, 0, -14 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -17 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -20 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -25 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -27 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -32 * .625f);
                    break;
            }

            return State.GameManager.SpriteDictionary.Eevee[70 + size];
        }
        else
        {
            return null;
        }
    }
    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        int sprite = 36;
        return State.GameManager.SpriteDictionary.Equaleon[sprite];

    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return State.GameManager.SpriteDictionary.Eevee[124];
                case 1:
                    return State.GameManager.SpriteDictionary.Eevee[125];
                case 2:
                    return State.GameManager.SpriteDictionary.Eevee[126];
                case 3:
                    return State.GameManager.SpriteDictionary.Eevee[127];
                case 4:
                    return State.GameManager.SpriteDictionary.Eevee[128];
                case 5:
                    return State.GameManager.SpriteDictionary.Eevee[129];
                case 6:
                    return State.GameManager.SpriteDictionary.Eevee[130];
                case 7:
                    return State.GameManager.SpriteDictionary.Eevee[131];
                default:
                    return null;
            }
        }
        else
        {
            return null;
        }
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Equaleon[31];
    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Equaleon[32];

    protected override Sprite BeardSprite(Actor_Unit actor) //nose
    {
        return State.GameManager.SpriteDictionary.Eevee[10];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
        {
            return State.GameManager.SpriteDictionary.Equaleon[9];
        }
        return State.GameManager.SpriteDictionary.Eevee[1];
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        return State.GameManager.SpriteDictionary.Equaleon[10 + actor.Unit.HairStyle];
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(31, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return State.GameManager.SpriteDictionary.Eevee[67];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return State.GameManager.SpriteDictionary.Eevee[66];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return State.GameManager.SpriteDictionary.Eevee[65];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return State.GameManager.SpriteDictionary.Eevee[64];
            }
            switch (size)
            {
                case 26:
                    AddOffset(Belly, 0, -14 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -17 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -20 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -25 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -27 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -32 * .625f);
                    break;
            }

            return State.GameManager.SpriteDictionary.Eevee[36 + size];
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

        int baseSize = actor.Unit.DickSize;
        int ballOffset = actor.GetBallSize(21, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 21)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return State.GameManager.SpriteDictionary.Umbreon2[30];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 21)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return State.GameManager.SpriteDictionary.Umbreon2[29];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 20)
        {
            AddOffset(Balls, 0, -15 * .625f);
            return State.GameManager.SpriteDictionary.Umbreon2[28];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .8f) == 19)
        {
            AddOffset(Balls, 0, -14 * .625f);
            return State.GameManager.SpriteDictionary.Umbreon2[27];
        }
        int combined = Math.Min(baseSize + ballOffset, 26);
        if (combined == 27)
            AddOffset(Balls, 0, -14 * .625f);
        else if (combined == 26)
            AddOffset(Balls, 0, -12 * .625f);
        else if (combined >= 23 && combined <= 25)
            AddOffset(Balls, 0, -8 * .625f);
        if (ballOffset > 0)
        {
            return State.GameManager.SpriteDictionary.Umbreon2[combined];
        }

        return State.GameManager.SpriteDictionary.Umbreon2[baseSize];
    }

    class Glasses1 : MainClothing
    {
        public Glasses1()
        {
            Type = 860037;
            clothing1 = new SpriteExtraInfo(17, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonClothing, s.Unit.ClothingColor2)); //Frame
            clothing2 = new SpriteExtraInfo(18, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonClothing, s.Unit.ClothingColor3)); //Lense
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[30];
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[31];
            base.Configure(sprite, actor);
        }
    }
    class Glasses1lensless: MainClothing
    {
        public Glasses1lensless()
        {
            Type = 860037;
            clothing1 = new SpriteExtraInfo(17, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonClothing, s.Unit.ClothingColor2)); //Frame
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[30];
            base.Configure(sprite, actor);
        }
    }
    class Glasses2 : MainClothing
    {
        public Glasses2()
        {
            Type = 860037;
            clothing1 = new SpriteExtraInfo(17, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonClothing, s.Unit.ClothingColor2)); //Frame
            clothing2 = new SpriteExtraInfo(18, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonClothing, s.Unit.ClothingColor3)); //Lense
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[33];
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[34];
            base.Configure(sprite, actor);
        }
    }
    class Glasses2lensless: MainClothing
    {
        public Glasses2lensless()
        {
            Type = 860037;
            clothing1 = new SpriteExtraInfo(17, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonClothing, s.Unit.ClothingColor2)); //Frame
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[33];
            base.Configure(sprite, actor);
        }
    }


    class Glasses3 : MainClothing
    {
        public Glasses3()
        {
            Type = 860037;
            clothing1 = new SpriteExtraInfo(17, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonClothing, s.Unit.ClothingColor2)); //Frame
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[32];
            base.Configure(sprite, actor);
        }
    }

    class Glasses4 : MainClothing
    {
        public Glasses4()
        {
            Type = 860037;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored); //Frame
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[35];
            base.Configure(sprite, actor);
        }
    }
    class Outfit1 : MainClothing
    {
        public Outfit1()
        {
            blocksBreasts = true;
            blocksDick = true;
            OccupiesAllSlots = true;
            Type = 860037;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonClothing, s.Unit.ClothingColor)); //Frame
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[actor.IsAttacking ? 69 : 68];
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[70];
            base.Configure(sprite, actor);
        }
    }
    class Outfit2 : MainClothing
    {
        public Outfit2()
        {
            blocksBreasts = true;
            blocksDick = true;
            OccupiesAllSlots = true;
            Type = 860037;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonClothing, s.Unit.ClothingColor)); //Frame
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[actor.IsAttacking ? 74 : 73];
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[actor.IsAttacking ? 76 : 75];
            base.Configure(sprite, actor);
        }
    }
    class Loincloth : MainClothing
    {
        public Loincloth()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Eevee[79];
            Type = 860037;
            clothing1 = new SpriteExtraInfo(17, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[78];
            base.Configure(sprite, actor);
        }
    }

    class Shorts : MainClothing
    {
        public Shorts()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Eevee[83];
            blocksDick = true;
            Type = 860040;
            clothing1 = new SpriteExtraInfo(17, null, null);
            clothing2 = new SpriteExtraInfo(18, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[80];
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 4)
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[81];
                else
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[82];
            }
            else clothing2.GetSprite = null;
            base.Configure(sprite, actor);
        }
    }
    class Belt : MainClothing
    {
        public Belt()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Eevee[93];
            Type = 860041;
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[84 + actor.Unit.BreastSize];
            base.Configure(sprite, actor);
        }
    }
    class Strap : MainClothing
    {
        public Strap()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Eevee[103];
            Type = 860042;
            femaleOnly = true;
            clothing1 = new SpriteExtraInfo(17, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonClothing, s.Unit.ClothingColor));
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[94 + actor.Unit.BreastSize];
            base.Configure(sprite, actor);
        }
    }
    class Leotard : MainClothing
    {
        public Leotard()
        {
            blocksBreasts = true;
            blocksDick = true;
            OccupiesAllSlots = true;
            Type = 860037;
            clothing1 = new SpriteExtraInfo(17, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonClothing, s.Unit.ClothingColor));
            clothing2 = new SpriteExtraInfo(17, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonClothing, s.Unit.ClothingColor));
            clothing3 = new SpriteExtraInfo(17, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EeveeEqualeonClothing, s.Unit.ClothingColor));
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts == false)
            {
                clothing3.GetSprite = (s) => null;
            }
            else
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[107 + actor.Unit.BreastSize];
            }
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[104];
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 4)
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[105];
                else
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[106];
            }
            else clothing2.GetSprite = null;
            base.Configure(sprite, actor);
        }
    }
    class EeveeRags : MainClothing
    {
        public EeveeRags()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Eevee[123];
            OccupiesAllSlots = true;
            Type = 860045;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts == false || actor.Unit.BreastSize <= 2)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[117];
            }
            else 
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[117 + actor.Unit.BreastSize - 2];
            }
            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.Eevee[116];
            base.Configure(sprite, actor);
        }
    }
}

