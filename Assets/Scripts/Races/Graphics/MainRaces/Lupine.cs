using System;
using System.Collections.Generic;
using UnityEngine;

class Lupine : DefaultRaceData
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.Lupine1;
    readonly Sprite[] Sprites2 = State.GameManager.SpriteDictionary.Lupine2;
    readonly Sprite[] Sprites3 = State.GameManager.SpriteDictionary.LupineVoreSprites;

    bool oversize = false;

    readonly LupineLeader LeaderClothes;
    readonly LupineRags Rags;

    public Lupine()
    {
        BodySizes = 3;
        EyeTypes = 4; // split into 3 sprites: Eyes (pupils), SecondaryEyes (sclera), SecondaryAccessory (iris)
        SpecialAccessoryCount = 16; // ears
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LupineSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LupineSkin); // for Sclera
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LupineSkin); // for Iris
        HairColors = 0;
        HairStyles = 16; // refers to cheek fluff rather than actual hair
        MouthTypes = 0;
        BodyAccentTypes1 = 6; // body patterns
        BodyAccentTypes2 = 5; // arm patterns
        BodyAccentTypes3 = 5; // leg patterns
        BodyAccentTypes4 = 10; // head patterns
        TailTypes = 12;
        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(3, BodySprite, null, (s) => LupineColor(s));
        Head = new SpriteExtraInfo(21, HeadSprite, null, (s) => LupineColor(s));
        BodyAccessory = new SpriteExtraInfo(25, AccessorySprite, null, (s) => LupineColor(s)); // Ears
        BodyAccent = new SpriteExtraInfo(5, BodyAccentSprite, null, (s) => LupineColor(s)); // Body Pattern
        BodyAccent2 = new SpriteExtraInfo(5, BodyAccentSprite2, null, (s) => LupineColor(s)); // Arm Pattern
        BodyAccent3 = new SpriteExtraInfo(4, BodyAccentSprite3, null, (s) => LupineColor(s)); // Leg Pattern
        BodyAccent4 = new SpriteExtraInfo(22, BodyAccentSprite4, null, (s) => LupineColor(s)); // Head Pattern
        BodyAccent5 = new SpriteExtraInfo(3, BodyAccentSprite5, null, (s) => LupineColor(s)); // Right Arm
        BodyAccent6 = new SpriteExtraInfo(5, BodyAccentSprite6, WhiteColored); // claws
        BodyAccent7 = new SpriteExtraInfo(1, BodyAccentSprite7, null, (s) => LupineColor(s)); // Tail
        Mouth = new SpriteExtraInfo(21, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(24, HairSprite, null, (s) => LupineColor(s));
        Hair2 = null;
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(23, EyesSprite, WhiteColored); // pupils
        SecondaryEyes = new SpriteExtraInfo(23, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LupineSkin, s.Unit.EyeColor)); // sclera
        SecondaryAccessory = new SpriteExtraInfo(23, SecondaryAccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LupineSkin, s.Unit.AccessoryColor)); // Iris
        Belly = new SpriteExtraInfo(14, null, null, (s) => LupineVoreColor(s));
        Weapon = new SpriteExtraInfo(6, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => LupineVoreColor(s));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => LupineVoreColor(s));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => LupineVoreColor(s));


        LeaderClothes = new LupineLeader();
        Rags = new LupineRags();

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new GenericTop1(),
            new GenericTop2(),
            new GenericTop3(),
            new GenericTop4(),
            new GenericTop5(),
            new GenericTop6(),
            new GenericTop7(),
            new GenericTop8(),
            new MaleTop(),
            new MaleTop2(),
            new Natural(),
            Rags,
            LeaderClothes
        };
        AvoidedMainClothingTypes = 2;
        AvoidedEyeTypes = 0;
        AllowedWaistTypes = new List<MainClothing>()
        {
            new GenericBot1(),
            new GenericBot2(),
            new GenericBot3(),
            new GenericBot4(),
            new GenericBot5(),
            new GenericBot6(),
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
    }


    ColorSwapPalette LupineColor(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType1 == 0)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LupineReversed, actor.Unit.SkinColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LupineSkin, actor.Unit.SkinColor);
    }
    ColorSwapPalette LupineVoreColor(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType1 == 1)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LupineReversed, actor.Unit.SkinColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LupineSkin, actor.Unit.SkinColor);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {

    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
        unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3);
        unit.BodyAccentType4 = State.Rand.Next(BodyAccentTypes4);

        if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(Rags);
            if (unit.ClothingType == 0) //Covers rags not in the list
                unit.ClothingType = AllowedMainClothingTypes.Count;
        }
        if (unit.Type == UnitType.Leader)
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(LeaderClothes);

        if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(Rags);
            if (unit.ClothingType == 0) //Covers rags not in the list
                unit.ClothingType = AllowedMainClothingTypes.Count;
        }
        if (unit.Type == UnitType.Leader)
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(LeaderClothes);
    }

    internal override int DickSizes => 8;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[3 + actor.Unit.BodySize];
        }
        else
        {
            return Sprites[0 + actor.Unit.BodySize];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        int spr = actor.Unit.HasBreasts ? 25 : 22;
        if (actor.IsAttacking) spr += 1;
        if (actor.IsOralVoring) spr += 2;
        return Sprites[spr];
    }


    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        int spr = actor.Unit.HasBreasts ? 31 : 28;
        if (actor.IsAttacking) spr += 1;
        if (actor.IsOralVoring) spr += 2;
        return Sprites[spr];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites[98 + actor.Unit.EarType]; //ears

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Body Pattern
    {
        if (actor.Unit.BodyAccentType1 <= 1)
            return null;
        return Sprites2[(actor.Unit.HasBreasts ? 12 : 0) + actor.Unit.BodySize + ((actor.Unit.BodyAccentType1 - 2) * 3)];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Arm Pattern
    {
        if (actor.Unit.BodyAccentType2 == 0)
            return null;

        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return Sprites2[(actor.Unit.HasBreasts ? 42 : 26) + ((actor.Unit.BodyAccentType2 - 1) * 4)];
                case 1:
                    return Sprites2[(actor.Unit.HasBreasts ? 43 : 27) + ((actor.Unit.BodyAccentType2 - 1) * 4)];
                case 2:
                    return Sprites2[(actor.Unit.HasBreasts ? 41 : 25) + ((actor.Unit.BodyAccentType2 - 1) * 4)];
                case 3:
                    return Sprites2[(actor.Unit.HasBreasts ? 42 : 26) + ((actor.Unit.BodyAccentType2 - 1) * 4)];
                case 4:
                    return Sprites2[(actor.Unit.HasBreasts ? 41 : 25) + ((actor.Unit.BodyAccentType2 - 1) * 4)];
                case 5:
                    return Sprites2[(actor.Unit.HasBreasts ? 42 : 26) + ((actor.Unit.BodyAccentType2 - 1) * 4)];
                case 6:
                    return Sprites2[(actor.Unit.HasBreasts ? 41 : 25) + ((actor.Unit.BodyAccentType2 - 1) * 4)];
                case 7:
                    return Sprites2[(actor.Unit.HasBreasts ? 42 : 26) + ((actor.Unit.BodyAccentType2 - 1) * 4)];
                default:
                    return null;
            }
        }
        else
        {
            return Sprites2[(actor.Unit.HasBreasts ? 40 : 0) + ((actor.Unit.BodyAccentType2 - 1) * 4)];
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Leg Pattern
    {
        if (actor.Unit.BodyAccentType3 <= 1)
            return null;
        return Sprites2[(actor.Unit.HasBreasts ? 64 : 56) + (actor.Unit.BodySize >= 2 ? 1 : 0) + ((actor.Unit.BodyAccentType3 - 1) * 2)];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Head Pattern
    {
        if (actor.Unit.BodyAccentType4 == 0)
            return null;
        int spr = actor.Unit.HasBreasts ? 99 : 72;
        if (actor.IsAttacking) spr += 1;
        if (actor.IsOralVoring) spr += 2;
        return Sprites2[spr + ((actor.Unit.BodyAccentType4 - 1) * 3)];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Right arm
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return Sprites[(actor.Unit.HasBreasts ? 12 : 8)];
                case 1:
                    return Sprites[(actor.Unit.HasBreasts ? 13 : 9)];
                case 2:
                    return Sprites[(actor.Unit.HasBreasts ? 11 : 7)];
                case 3:
                    return Sprites[(actor.Unit.HasBreasts ? 12 : 8)];
                case 4:
                    return Sprites[(actor.Unit.HasBreasts ? 11 : 7)];
                case 5:
                    return Sprites[(actor.Unit.HasBreasts ? 12 : 8)];
                case 6:
                    return Sprites[(actor.Unit.HasBreasts ? 11 : 7)];
                case 7:
                    return Sprites[(actor.Unit.HasBreasts ? 12 : 8)];
                default:
                    return null;
            }
        }
        else
        {
            return Sprites[(actor.Unit.HasBreasts ? 10 : 6)];
        }

    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // claws
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return Sprites[(actor.Unit.HasBreasts ? 20 : 16)];
                case 1:
                    return Sprites[(actor.Unit.HasBreasts ? 21 : 17)];
                case 2:
                    return Sprites[(actor.Unit.HasBreasts ? 19 : 15)];
                case 3:
                    return Sprites[(actor.Unit.HasBreasts ? 20 : 16)];
                case 4:
                    return Sprites[(actor.Unit.HasBreasts ? 19 : 15)];
                case 5:
                    return Sprites[(actor.Unit.HasBreasts ? 20 : 16)];
                case 6:
                    return Sprites[(actor.Unit.HasBreasts ? 19 : 15)];
                case 7:
                    return Sprites[(actor.Unit.HasBreasts ? 20 : 16)];
                default:
                    return null;
            }
        }
        else
        {
            return Sprites[(actor.Unit.HasBreasts ? 18 : 14)];
        }

    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // tail
    {
        return Sprites[114 + actor.Unit.TailType];
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        return Sprites[83 + actor.Unit.HairStyle];
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        int spr = 50 + actor.Unit.EyeType * 3;
        if (actor.IsAttacking) spr += 1;
        if (actor.IsOralVoring) spr += 2;
        return Sprites[spr];
    }

    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
        int spr = 62 + actor.Unit.EyeType * 3;
        if (actor.IsAttacking) spr += 1;
        if (actor.IsOralVoring) spr += 2;
        return Sprites[spr];
    }
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor)
    {
        if (actor.Unit.EyeType == 2)
            return null;
        int spr = 74 + (actor.Unit.EyeType > 2 ? actor.Unit.EyeType  - 1: actor.Unit.EyeType) * 3;
        if (actor.IsAttacking) spr += 1;
        if (actor.IsOralVoring) spr += 2;
        return Sprites[spr];
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
                AddOffset(Belly, 0, -29 * .625f);
                return Sprites3[99];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -29 * .625f);
                return Sprites3[98];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -29 * .625f);
                return Sprites3[97];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -29 * .625f);
                return Sprites3[96];
            }
            switch (size)
            {
                case 26:
                    AddOffset(Belly, 0, -3 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -8 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -13 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -16 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -22 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -28 * .625f);
                    break;
            }

            return Sprites3[64 + size];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            return Sprites[(actor.Unit.HasBreasts ? 42 : 34) + actor.GetWeaponSprite()];
        }
        else
        {
            return null;
        }
    }
    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        oversize = false;
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f));
            if (leftSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
            {
                return Sprites3[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return Sprites3[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return Sprites3[29];
            }

            if (leftSize > 28)
                leftSize = 28;

            return Sprites3[0 + leftSize];
        }
        else
        {
            return Sprites3[0 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f));
            if (rightSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
            {
                return Sprites3[63];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return Sprites3[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return Sprites3[61];
            }

            if (rightSize > 28)
                rightSize = 28;

            return Sprites3[32 + rightSize];
        }
        else
        {
            return Sprites3[32 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
            {
                Dick.layer = 20;
                if (actor.IsCockVoring)
                {
                    return Sprites3[154 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites3[138 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    return Sprites3[162 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites3[146 + actor.Unit.DickSize];
                }
            }
        }

        Dick.layer = 11;
        return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect() && (actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
        {
            Balls.layer = 19;
        }
        else
        {
            Balls.layer = 10;
        }
        int size = actor.Unit.DickSize;
        int offset = actor.GetBallSize(28, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -16 * .625f);
            return Sprites3[139 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -16 * .625f);
            return Sprites3[138 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -16 * .625f);
            return Sprites3[137 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)];
        }
        else if (offset >= 26)
        {
            AddOffset(Balls, 0, -16 * .625f);
        }
        else if (offset == 25)
        {
            AddOffset(Balls, 0, -8 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -6 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -5 * .625f);
        }
        else if (offset == 22)
        {
            AddOffset(Balls, 0, -3 * .625f);
        }
        else if (offset == 21)
        {
            AddOffset(Balls, 0, -1 * .625f);
        }

        if (offset > 0)
            return Sprites3[Math.Min(108 + offset, 134)];
        return Sprites3[100 + size];
    }


    class GenericTop1 : MainClothing
    {
        public GenericTop1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Avians4[24];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1524;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Lupine.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[48];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[40 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop2 : MainClothing
    {
        public GenericTop2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Avians4[34];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1534;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Lupine.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[57];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[49 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop3 : MainClothing
    {
        public GenericTop3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Avians4[44];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1544;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Lupine.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[66];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[58 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop4 : MainClothing
    {
        public GenericTop4()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Avians4[55];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 1555;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Lupine.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[75];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[67 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[76];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop5 : MainClothing
    {
        public GenericTop5()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Avians4[74];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 1574;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Lupine.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[85];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[94];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[77 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[86 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop6 : MainClothing
    {
        public GenericTop6()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Avians4[88];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1588;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Lupine.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[98 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop7 : MainClothing
    {
        public GenericTop7()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Avians4[44];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1544;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Lupine.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[130];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[122 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop8 : MainClothing
    {
        public GenericTop8()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.LupineClothes2[49];
            coversBreasts = false;
            blocksDick = false;
            Type = 47558;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(15, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(15, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Lupine.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[32];
                clothing2.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[24 + actor.Unit.BreastSize];

                if (actor.HasBelly)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[12 + actor.Unit.BodySize];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[6 + actor.Unit.BodySize];
                }
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;

                if (actor.HasBelly)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[15 + actor.Unit.BodySize];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[9 + actor.Unit.BodySize];
                }
            }

            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[19 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
                else clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[18 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }
            else if (actor.GetWeaponSprite() == 1)
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[20 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }
            else if (actor.GetWeaponSprite() == 2 || actor.GetWeaponSprite() == 4 || actor.GetWeaponSprite() == 6)
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[18 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }
            else
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[19 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
            }

            base.Configure(sprite, actor);
        }
    }

    class MaleTop : MainClothing
    {
        public MaleTop()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Avians4[79];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1579;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.HasBelly)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[109 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[106 + actor.Unit.BodySize];
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class MaleTop2 : MainClothing
    {
        public MaleTop2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Avians4[79];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1579;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[95 + actor.Unit.BodySize];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class Natural : MainClothing
    {
        public Natural()
        {
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(7, null, null);
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Lupine.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[2 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[1];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[0];
            }

            if (actor.Unit.BodyAccentType1 == 1)
            {
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LupineReversed, actor.Unit.SkinColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LupineReversed, actor.Unit.SkinColor);
            }
            else
            {
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LupineSkin, actor.Unit.SkinColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LupineSkin, actor.Unit.SkinColor);
            }

            base.Configure(sprite, actor);
        }
    }

    class LupineRags : MainClothing
    {
        public LupineRags()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Rags[23];
            blocksDick = false;
            inFrontOfDick = true;
            coversBreasts = false;
            Type = 207;
            OccupiesAllSlots = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BreastSize < 3)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[119];
                else if (actor.Unit.BreastSize < 6)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[120];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[121];

                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[112 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[118];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[115 + actor.Unit.BodySize];
            }

            base.Configure(sprite, actor);
        }
    }

    class LupineLeader : MainClothing
    {
        public LupineLeader()
        {
            leaderOnly = true;
            DiscardSprite = State.GameManager.SpriteDictionary.LupineClothes2[50];
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(15, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(15, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing5 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing6 = new SpriteExtraInfo(19, null, WhiteColored);
            clothing7 = new SpriteExtraInfo(2, null, WhiteColored);
            Type = 47559;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[33];
                else if (actor.Unit.DickSize > 5)
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[35];
                else
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[34];
            }
            else clothing4.GetSprite = null;

            if (Races.Lupine.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[32];
                clothing2.GetSprite = null;
                clothing4.YOffset = -1 * .625f;
                clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[0 + actor.Unit.BodySize];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[24 + actor.Unit.BreastSize];
                clothing4.YOffset = -1 * .625f;
                clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[0 + actor.Unit.BodySize];

                if (actor.HasBelly)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[12 + actor.Unit.BodySize];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[6 + actor.Unit.BodySize];
                }
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing4.YOffset = 0 * .625f;
                clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[3 + actor.Unit.BodySize];

                if (actor.HasBelly)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[15 + actor.Unit.BodySize];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[9 + actor.Unit.BodySize];
                }
            }

            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking)
                {
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[19 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];

                    if ((actor.GetStomachSize(31) > 17) || (Races.Lupine.oversize))
                    {
                        clothing6.GetSprite = null;
                        clothing7.GetSprite = null;
                    }
                    else
                    {
                        clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[37 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
                        clothing7.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[43 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
                    }
                }
                else
                {
                    clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[18 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];

                    if ((actor.GetStomachSize(31) > 17) || (Races.Lupine.oversize))
                    {
                        clothing6.GetSprite = null;
                        clothing7.GetSprite = null;
                    }
                    else
                    {
                        clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[36 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
                        clothing7.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[42 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
                    }
                }
            }
            else if (actor.GetWeaponSprite() == 1)
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[20 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];

                if ((actor.GetStomachSize(31) > 17) || (Races.Lupine.oversize))
                {
                    clothing6.GetSprite = null;
                    clothing7.GetSprite = null;
                }
                else
                {
                    clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[38 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
                    clothing7.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[44 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
                }
            }
            else if (actor.GetWeaponSprite() == 2 || actor.GetWeaponSprite() == 4 || actor.GetWeaponSprite() == 6)
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[18 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];

                if ((actor.GetStomachSize(31) > 17) || (Races.Lupine.oversize))
                {
                    clothing6.GetSprite = null;
                    clothing7.GetSprite = null;
                }
                else
                {
                    clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[36 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
                    clothing7.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[42 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
                }
            }
            else
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[19 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];

                if ((actor.GetStomachSize(31) > 17) || (Races.Lupine.oversize))
                {
                    clothing6.GetSprite = null;
                    clothing7.GetSprite = null;
                }
                else
                {
                    clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[37 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
                    clothing7.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[43 + 3 * (!actor.Unit.HasBreasts ? 1 : 0)];
                }
            }

            base.Configure(sprite, actor);
        }




    }

    class GenericBot1 : MainClothing
    {
        public GenericBot1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Avians3[121];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 1521;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[16];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[18];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[17];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing1.YOffset = -1 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[10 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.YOffset = 0 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[13 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot2 : MainClothing
    {
        public GenericBot2()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Avians3[137];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 1537;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[26];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[28];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[27];
            }
            else clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[25];

            if (actor.Unit.HasBreasts)
            {
                clothing1.YOffset = -1 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[19 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.YOffset = 0 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[22 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot3 : MainClothing
    {
        public GenericBot3()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Avians3[140];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 1540;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[29];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[19 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[30];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[22 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot4 : MainClothing
    {
        public GenericBot4()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Avians4[14];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 1514;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[37];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[39];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[38];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing1.YOffset = -1 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[31 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.YOffset = 0 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[34 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot5 : MainClothing
    {
        public GenericBot5()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Avians4[14];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 1514;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[37];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[39];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[38];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing1.YOffset = -1 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[131 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.YOffset = 0 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes[134 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot6 : MainClothing
    {
        public GenericBot6()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.LupineClothes2[48];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 47557;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[33];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[35];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[34];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing1.YOffset = -1 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[0 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.YOffset = 0 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.LupineClothes2[3 + actor.Unit.BodySize];
            }

            base.Configure(sprite, actor);
        }
    }
}
