using System;
using System.Collections.Generic;
using UnityEngine;

class Equines : DefaultRaceData
{
    Sprite[] SpritesBase = SpriteDictionary.Horse;
    Sprite[] SpritesClothes = SpriteDictionary.HorseClothing;
    Sprite[] SpritesAdd1 = SpriteDictionary.HorseExtras1;

    bool oversize = false;

    internal Equines()
    {
        SpecialAccessoryCount = 0;
        ClothingShift = new Vector3(0, 0, 0);
        AvoidedEyeTypes = 0;
        AvoidedMouthTypes = 0;

        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
        HairStyles = 15;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.HorseSkin);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.HorseSkin);
        EyeTypes = 4;
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
        SecondaryEyeColors = 1;
        BodySizes = 0;
        AllowedMainClothingTypes = new List<MainClothing>();
        AllowedWaistTypes = new List<MainClothing>();
        AllowedClothingHatTypes = new List<ClothingAccessory>();
        MouthTypes = 0;
        AvoidedMainClothingTypes = 0;
        TailTypes = 6;
        BodyAccentTypes3 = 5;
        BodyAccentTypes4 = 5;
        BodyAccentTypes5 = 2;


        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing50Spaced);

        ExtendedBreastSprites = true;

        AllowedMainClothingTypes = new List<MainClothing>() //undertops
        {
             new HorseUndertop1(),
             new HorseUndertop2(),
             new HorseUndertop3(),
             new HorseUndertop4(),
             new HorseUndertopM1(),
             new HorseUndertopM2(),
             new HorseUndertopM3(),
        };

        AllowedWaistTypes = new List<MainClothing>() //underbottoms
        {
            new HorseUBottom(2, 0, 30, 5, 9, SpriteDictionary.HorseClothing, 76105, true),
            new HorseUBottom(7, 5, 30, 9, 9, SpriteDictionary.HorseClothing, 76109, true),
            new HorseUBottom(17, 15, 30, 19, 9, SpriteDictionary.HorseClothing, 76119, true),
            new HorseUBottom(22, 20, 30, 24, 9, SpriteDictionary.HorseClothing, 76124, true),
            new HorseUBottom(27, 25, 14, 29, 9, SpriteDictionary.HorseClothing, 76129, true, true),
        };

        ExtraMainClothing1Types = new List<MainClothing>() //Overtops
        {
            new HorsePoncho(),
            new HorseNecklace(),
        };

        ExtraMainClothing2Types = new List<MainClothing>() //Overbottoms
        {
            new HorseOBottom1(),
            new HorseOBottom2(),
            new HorseOBottom3(),
        };

        ColorSwapPalette LegTuft(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType3 >= 2)
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, actor.Unit.AccessoryColor);
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, actor.Unit.SkinColor);
        }

        ColorSwapPalette SpottedBelly(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType5 == 1)
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, actor.Unit.AccessoryColor);
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, actor.Unit.SkinColor);
        }

        ColorSwapPalette TailBit(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType3 == 5)
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, actor.Unit.SkinColor);
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
        }

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, s.Unit.SkinColor)); //body
        Head = new SpriteExtraInfo(5, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, s.Unit.SkinColor)); //head
        BodyAccessory = new SpriteExtraInfo(2, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); //tail
        BodyAccent = null;
        BodyAccent2 = null;
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, s.Unit.AccessoryColor)); //limb spots
        BodyAccent4 = new SpriteExtraInfo(6, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, s.Unit.AccessoryColor)); //head spots
        BodyAccent5 = new SpriteExtraInfo(5, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HorseSkin, s.Unit.AccessoryColor)); //belly spots, also color breasts/belly/dick
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = new SpriteExtraInfo(6, BodyAccentSprite8, null, (s) => LegTuft(s)); //leg tuft
        BodyAccent9 = new SpriteExtraInfo(3, BodyAccentSprite9, null, (s) => TailBit(s)); //tail bit
        BodyAccent10 = new SpriteExtraInfo(5, BodyAccentSprite10, null, null); //leg hoof;
        Mouth = null;
        Hair = new SpriteExtraInfo(21, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); //forward hair;
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); //back hair
        Eyes = new SpriteExtraInfo(6, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor)); //eyes;
        SecondaryEyes = null;
        SecondaryAccessory = new SpriteExtraInfo(3, SecondaryAccessorySprite, WhiteColored); //bow bit
        Belly = new SpriteExtraInfo(17, null, null, (s) => SpottedBelly(s)); //belly
        Weapon = new SpriteExtraInfo(12, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(19, BreastsSprite, null, (s) => SpottedBelly(s));
        SecondaryBreasts = new SpriteExtraInfo(19, SecondaryBreastsSprite, null, (s) => SpottedBelly(s));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(14, DickSprite, null, (s) => SpottedBelly(s)); //cocc
        Balls = new SpriteExtraInfo(13, BallsSprite, null, (s) => SpottedBelly(s)); //balls
        WholeBodyOffset = new Vector2(0, 16 * .625f);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3);
        unit.BodyAccentType4 = State.Rand.Next(BodyAccentTypes4);
        unit.BodyAccentType5 = State.Rand.Next(BodyAccentTypes5);

        unit.HairStyle = State.Rand.Next(HairStyles);
        unit.TailType = State.Rand.Next(TailTypes);

    }

    internal override int BreastSizes => 7;
    internal override int DickSizes => 3;

    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        if (actor.Unit.TailType <= 3)
            return SpritesBase[180 + actor.Unit.TailType];
        else if (actor.Unit.TailType == 4)
            return SpritesBase[184];
        else if (actor.Unit.TailType == 5)
            return SpritesBase[186];
        else
            return SpritesBase[188];
    }
    protected override Sprite BackWeaponSprite(Actor_Unit actor) => null;

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(29, 1.2f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                return SpritesBase[89];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                return SpritesBase[88];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size >= 27)
            {
                return SpritesBase[87];
            }

            int combined = Math.Min(size, 26);
            return SpritesBase[60 + combined];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType3 == 0)
            return null;
        else
        {
            int Hasweaponoffset = actor.Unit.HasWeapon ? 1 : 0;
            if (actor.Unit.HasBreasts == false)
            {
                if (actor.IsAttacking)
                    return SpritesBase[214 + (6 * actor.Unit.BodyAccentType3)];
                else
                    return SpritesBase[212 + (6 * actor.Unit.BodyAccentType3) + Hasweaponoffset];
            }
            else
            {
                if (actor.IsAttacking)
                    return SpritesBase[217 + (6 * actor.Unit.BodyAccentType3)];
                else
                    return SpritesBase[215 + (6 * actor.Unit.BodyAccentType3) + Hasweaponoffset];
            }
        }
    }
    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType4 == 0)
            return null;
        else
        {
            if (actor.Unit.HasBreasts == true)
            {
                int attackoffset = actor.IsAttacking ? 1 : 0;
                int eatoffset = actor.IsEating ? 1 : 0;
                return SpritesBase[200 + (4 * actor.Unit.BodyAccentType4) + attackoffset + eatoffset];
            }
            else
            {
                int attackoffset = actor.IsAttacking ? 1 : 0;
                int eatoffset = actor.IsEating ? 1 : 0;
                return SpritesBase[198 + (4 * actor.Unit.BodyAccentType4) + attackoffset + eatoffset];
            }
        }
    }
    protected override Sprite BodyAccentSprite5(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType5 == 0)
            return null;
        else
        {
            if (actor.Unit.HasBreasts == true)
            {
                return SpritesBase[201];
            }
            else
            {
                return SpritesBase[200];
            }
        }
    }
    protected override Sprite BodyAccentSprite8(Actor_Unit actor)
    {
        return SpritesBase[22];
    }
    protected override Sprite BodyAccentSprite9(Actor_Unit actor)
    {
        if (actor.Unit.TailType <= 3)
            return null;
        else if (actor.Unit.TailType == 4)
            return SpritesBase[185];
        else if (actor.Unit.TailType == 5)
            return SpritesBase[187];
        else
            return SpritesBase[189];
    }
    protected override Sprite BodyAccentSprite10(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == true)
        {
            return SpritesBase[21];
        }
        else
        {
            return SpritesBase[20];
        }
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) => null;
    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BodySprite(Actor_Unit actor)
    {
        int Hasweaponoffset = actor.Unit.HasWeapon ? 1 : 0;
        if (actor.Unit.HasBreasts == true)
        {
            if (actor.IsAttacking)
                return SpritesBase[12];
            else
                return SpritesBase[10 + Hasweaponoffset];
        }
        else
        {
            if (actor.IsAttacking)
                return SpritesBase[2];
            else
                return SpritesBase[0 + Hasweaponoffset];
        }
    }
    protected override Sprite BreastsShadowSprite(Actor_Unit actor) => null;
    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        oversize = false;
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(29 * 29, 1f));
            if (leftSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 29)
            {
                return SpritesBase[119];
            }
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 29)
            {
                return SpritesBase[118];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 27)
            {
                return SpritesBase[117];
            }


            if (leftSize > 26)
                leftSize = 26;


            return SpritesBase[90 + leftSize];
        }
        else
        {
            if (actor.Unit.DefaultBreastSize == 0)
                return SpritesBase[90];
            return SpritesBase[90 + actor.Unit.BreastSize];
        }
    }
    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        oversize = false;
        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(29 * 29, 1f));
            if (rightSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 29)
            {
                return SpritesBase[149];
            }
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 29)
            {
                return SpritesBase[148];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 27)
            {
                return SpritesBase[147];
            }


            if (rightSize > 26)
                rightSize = 26;


            return SpritesBase[120 + rightSize];
        }
        else
        {
            if (actor.Unit.DefaultBreastSize == 0)
                return SpritesBase[120];
            return SpritesBase[120 + actor.Unit.BreastSize];
        }
    }
    internal override Color ClothingColor(Actor_Unit actor) => Color.white;

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;


        if ((actor.PredatorComponent?.VisibleFullness < .26f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(29 * 29, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(29 * 29, 1f)) < 16))
        {
            Dick.layer = 24;
            if (actor.IsCockVoring)
            {
                return SpritesBase[25 + 2 * actor.Unit.DickSize];
            }
            else
            {
                if (Config.FurryGenitals)
                {
                    if (actor.IsErect())
                        return SpritesBase[25 + 2 * actor.Unit.DickSize];
                    else
                        return SpritesBase[23];
                }
                else
                {
                    if (actor.IsErect())
                        return SpritesBase[25 + 2 * actor.Unit.DickSize];
                    else
                        return SpritesBase[24 + 2 * actor.Unit.DickSize];
                }
            }
        }
        else
        {
            Dick.layer = 14;
            if (actor.IsCockVoring)
            {
                return SpritesBase[24 + 2 * actor.Unit.DickSize];
            }
            else
            {
                if (Config.FurryGenitals)
                    return SpritesBase[23];
                else
                    return SpritesBase[24 + 2 * actor.Unit.DickSize];
            }
        }

    }
    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        int size = actor.GetBallSize(29, .8f);
        int baseSize = (actor.Unit.DickSize + 1) / 3;

        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && size == 29)
        {
            return SpritesBase[59];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 29)
        {
            return SpritesBase[58];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 27)
        {
            return SpritesBase[57];
        }
        int combined = Math.Min(baseSize + size + 2, 26);
        if (size > 0)
        {
            return SpritesBase[30 + combined];
        }
        return SpritesBase[30 + baseSize];
    }
    protected override Color EyeColor(Actor_Unit actor) => Color.white;
    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => null;
    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == true)
        {
            if (actor.Unit.IsDead && actor.Unit.Items != null)
                return SpritesBase[9];
            else
                return SpritesBase[5 + actor.Unit.EyeType];
        }
        else
        {
            if (actor.Unit.IsDead && actor.Unit.Items != null)
                return SpritesBase[19];
            else
                return SpritesBase[15 + actor.Unit.EyeType];
        }
    }
    protected override Color HairColor(Actor_Unit actor) => Color.white;
    protected override Sprite HairSprite(Actor_Unit actor)
    {
        return SpritesBase[150 + 2 * actor.Unit.HairStyle];
    }
    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        return SpritesBase[151 + 2 * actor.Unit.HairStyle];
    }
    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        int attackingOffset = actor.IsAttacking ? 1 : 0;
        int eatingOffset = actor.IsEating ? 1 : 0;
        if (actor.Unit.HasBreasts == true)
        {
            return SpritesBase[13 + attackingOffset + eatingOffset];
        }
        else
        {
            return SpritesBase[3 + attackingOffset + eatingOffset];
        }
    }
    protected override Sprite MouthSprite(Actor_Unit actor) => null;
    protected override Color ScleraColor(Actor_Unit actor) => Color.white;
    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            int weaponSprite = actor.GetWeaponSprite();
            switch (weaponSprite)
            {
                case 0:
                    return SpritesBase[190];
                case 1:
                    return SpritesBase[191];
                case 2:
                    return SpritesBase[192];
                case 3:
                    return SpritesBase[193];
                case 4:
                    return SpritesBase[194];
                case 5:
                    return null;
                case 6:
                    return SpritesBase[197];
                case 7:
                    return SpritesBase[199];
            }

            return null;
        }
        else
        {
            return null;
        }
    }
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            int weaponSprite2 = actor.GetWeaponSprite();
            switch (weaponSprite2)
            {
                case 0:
                    return null;
                case 1:
                    return null;
                case 2:
                    return null;
                case 3:
                    return null;
                case 4:
                    return null;
                case 5:
                    return null;
                case 6:
                    return SpritesBase[198];
                case 7:
                    return null;
            }

            return null;
        }
        else
        {
            return null;
        }
    }

    class HorseUndertop1 : MainClothing
    {
        public HorseUndertop1()
        {
            DiscardSprite = SpriteDictionary.HorseClothing[47];
            Type = 76147;
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Equines.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[47];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[40 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class HorseUndertop2 : MainClothing
    {
        public HorseUndertop2()
        {
            DiscardSprite = SpriteDictionary.HorseClothing[48];
            Type = 76148;
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Equines.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[48 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }
    class HorseUndertop3 : MainClothing
    {
        public HorseUndertop3()
        {
            DiscardSprite = SpriteDictionary.HorseClothing[56];
            Type = 76156;
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Equines.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[56 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class HorseUndertop4 : MainClothing
    {
        public HorseUndertop4()
        {
            DiscardSprite = SpriteDictionary.HorseExtras1[8];
            Type = 76208;
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Equines.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.HorseExtras1[7];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.HorseExtras1[0 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }
    class HorseUndertopM1 : MainClothing
    {
        public HorseUndertopM1()
        {
            DiscardSprite = SpriteDictionary.HorseClothing[36];
            Type = 76136;
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int size = actor.GetStomachSize(32, 1.2f);
            if (size >= 6)
                clothing1.GetSprite = (s) => SpriteDictionary.HorseExtras1[17];
            else
                clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[36];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class HorseUndertopM2 : MainClothing
    {
        public HorseUndertopM2()
        {
            DiscardSprite = SpriteDictionary.HorseClothing[37];
            Type = 76137;
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int size = actor.GetStomachSize(32, 1.2f);
            if (size >= 6)
                clothing1.GetSprite = (s) => SpriteDictionary.HorseExtras1[18];
            else
                clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[37];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class HorseUndertopM3 : MainClothing
    {
        public HorseUndertopM3()
        {
            DiscardSprite = SpriteDictionary.HorseClothing[38];
            Type = 76138;
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(20, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            int size = actor.GetStomachSize(32, 1.2f);
            int weightMod = size > 6 ? 1 : 0;

            clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[38 + weightMod];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class HorsePoncho : MainClothing
    {
        public HorsePoncho()
        {
            DiscardSprite = SpriteDictionary.HorseClothing[33];
            Type = 76133;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(21, null, null);
            clothing2 = new SpriteExtraInfo(3, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;


            clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[33];
            clothing2.GetSprite = (s) => SpriteDictionary.HorseClothing[34];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class HorseNecklace : MainClothing
    {
        public HorseNecklace()
        {
            DiscardSprite = SpriteDictionary.HorseClothing[35];
            Type = 76135;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(21, null, null);
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            int weightMod = actor.Unit.BodySize;


            clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[35];

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }
    class HorseUBottom : MainClothing
    {
        int sprM;
        int sprF;
        int bulge;
        bool black;
        Sprite[] sheet;
        public HorseUBottom(int femaleSprite, int maleSprite, int bulge, int discard, int layer, Sprite[] sheet, int type, bool colored = false, bool black = false)
        {
            coversBreasts = false;
            blocksDick = true;
            sprF = femaleSprite;
            sprM = maleSprite;
            this.sheet = sheet;
            this.bulge = bulge;
            this.black = black;
            clothing1 = new SpriteExtraInfo(layer, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(layer + 1, null, WhiteColored);
            DiscardSprite = sheet[discard];
            Type = type;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.HasBelly)
            {
                if (actor.Unit.HasBreasts)
                    clothing1.GetSprite = (s) => sheet[sprF + 1];
                else
                    clothing1.GetSprite = (s) => sheet[sprM + 1];
                if (actor.Unit.HasDick)
                {
                    if (blocksDick == true)
                    {
                        if (black == true)
                            clothing2.GetSprite = (s) => SpriteDictionary.HorseExtras1[bulge + actor.Unit.DickSize];
                        else
                            clothing2.GetSprite = (s) => SpriteDictionary.HorseClothing[bulge + actor.Unit.DickSize];
                    }
                    else
                        clothing2.GetSprite = null;
                }
                else
                    clothing2.GetSprite = null;
            }
            else
            {
                if (actor.Unit.HasBreasts)
                    clothing1.GetSprite = (s) => sheet[sprF];
                else
                    clothing1.GetSprite = (s) => sheet[sprM];
                if (actor.Unit.HasDick)
                {
                    if (blocksDick == true)
                    {
                        if (black == true)
                            clothing2.GetSprite = (s) => SpriteDictionary.HorseExtras1[bulge + actor.Unit.DickSize];
                        else
                            clothing2.GetSprite = (s) => SpriteDictionary.HorseClothing[bulge + actor.Unit.DickSize];
                    }
                    else
                        clothing2.GetSprite = null;
                }
                else
                    clothing2.GetSprite = null;
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);

        }

    }

    class HorseOBottom1 : MainClothing
    {
        int sprM;
        int sprF;
        int bulge;
        bool black;
        Sprite[] sheet;
        public HorseOBottom1()
        {
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(15, null, WhiteColored);
            DiscardSprite = SpriteDictionary.HorseClothing[14];
            Type = 76114;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.HasBelly)
            {
                if (actor.Unit.HasBreasts)
                    clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[13];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[11];
            }
            else
            {
                if (actor.Unit.HasBreasts)
                    clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[12];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[10];
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);

        }

    }
    class HorseOBottom2 : MainClothing
    {
        int sprM;
        int sprF;
        int bulge;
        bool black;
        Sprite[] sheet;
        public HorseOBottom2()
        {
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(15, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(16, null, WhiteColored);
            DiscardSprite = SpriteDictionary.HorseClothing[68];
            Type = 76168;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            {
                if (actor.HasBelly)
                {
                    if (actor.Unit.HasBreasts)
                        clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[67];
                    else
                        clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[65];
                }
                else
                {
                    if (actor.Unit.HasBreasts)
                        clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[66];
                    else
                        clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[64];
                }
            }
            if (actor.Unit.HasDick)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.HorseExtras1[Math.Min(14 + actor.Unit.DickSize, 17)];
            }
            else
                clothing2.GetSprite = null;
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);

        }

    }
    class HorseOBottom3 : MainClothing
    {
        int sprM;
        int sprF;
        int bulge;
        bool black;
        Sprite[] sheet;
        public HorseOBottom3()
        {
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(15, null, WhiteColored);
            DiscardSprite = SpriteDictionary.HorseClothing[73];
            Type = 76173;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.HasBelly)
            {
                if (actor.Unit.HasBreasts)
                    clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[72];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[70];
            }
            else
            {
                if (actor.Unit.HasBreasts)
                    clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[71];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.HorseClothing[69];
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);

        }

    }
}

