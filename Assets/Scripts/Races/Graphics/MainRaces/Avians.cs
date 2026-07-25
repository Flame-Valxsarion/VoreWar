using System;
using System.Collections.Generic;
using UnityEngine;

class Avians : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Avians1;
    readonly Sprite[] Sprites2 = SpriteDictionary.Avians2;
    readonly Sprite[] Sprites3 = SpriteDictionary.Avians3;
    readonly Sprite[] Sprites4 = SpriteDictionary.Avians4;
    readonly AvianLeader LeaderClothes;
    readonly AvianRags Rags;

    bool oversize = false;

    public Avians()
    {
        BodySizes = 4;
        HairStyles = 12;
        EyeTypes = 6;
        SpecialAccessoryCount = 4; // feather patterns
        HairColors = 0;
        MouthTypes = 0;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin); // claws color (black)
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin); ; // beak color (black)
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin); // primary feather colors (white)
        ExtraColors2 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin); // secondary feather colors (grey)
        BodyAccentTypes1 = 4; // wings
        TailTypes = 16;

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(6, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.ExtraColor1)); // body (white/ primary)
        Head = new SpriteExtraInfo(14, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.ExtraColor1)); // head primary (white)
        BodyAccessory = new SpriteExtraInfo(17, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.AccessoryColor)); // beak
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.ExtraColor1)); // wings primary (white)
        BodyAccent2 = new SpriteExtraInfo(1, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.ExtraColor2)); // wings secondary (grey)
        BodyAccent3 = new SpriteExtraInfo(2, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.ExtraColor1)); // tail primary (white)
        BodyAccent4 = new SpriteExtraInfo(2, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.ExtraColor2)); // tail secondary (grey)
        BodyAccent5 = new SpriteExtraInfo(3, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.SkinColor)); // feet (black)
        BodyAccent6 = new SpriteExtraInfo(3, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.SkinColor)); // claws (black)
        BodyAccent7 = new SpriteExtraInfo(6, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.ExtraColor2)); // legs (grey/ secondary)
        BodyAccent8 = new SpriteExtraInfo(6, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.ExtraColor2)); // arms (grey/ secondary)
        Mouth = new SpriteExtraInfo(17, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(14, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.ExtraColor2)); // head secondary (grey)
        Hair2 = null;
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(15, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = new SpriteExtraInfo(16, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.ExtraColor2)); // eyebrows (grey/secondary)
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(11, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.ExtraColor1)); // belly primary
        Weapon = new SpriteExtraInfo(4, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(12, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.ExtraColor1)); // breasts primary
        SecondaryBreasts = new SpriteExtraInfo(12, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.ExtraColor1));
        Dick = new SpriteExtraInfo(8, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(7, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, s.Unit.ExtraColor1)); // balls primary

        LeaderClothes = new AvianLeader();
        Rags = new AvianRags();

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new GenericTop1(),
            new GenericTop2(),
            new GenericTop3(),
            new GenericTop4(),
            new GenericTop5(),
            new GenericTop6(),
            new MaleTop(),
            new Natural(),
            Rags,
            LeaderClothes
        };
        AvoidedMainClothingTypes = 2;

        AllowedWaistTypes = new List<MainClothing>()
        {
            new GenericBot1(),
            new GenericBot2(),
            new GenericBot3(),
            new GenericBot4(),
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);

        unit.AccessoryColor = unit.SkinColor;

        unit.HairStyle = State.Rand.Next(HairStyles);

        unit.TailType = State.Rand.Next(TailTypes);

        if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(Rags);
            if (unit.ClothingType == -1) //Covers rags not in the list
                unit.ClothingType = 1;
        }

        if (unit.Type == UnitType.Leader)
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(LeaderClothes);
    }

    internal override int DickSizes => 8;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor) // body (white/ primary)
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[0 + actor.Unit.BodySize];
        }
        else
        {
            return Sprites[4 + actor.Unit.BodySize];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor) => Sprites2[0 + actor.Unit.HairStyle + 12 * actor.Unit.SpecialAccessoryType]; // head primary (white)

    protected override Sprite AccessorySprite(Actor_Unit actor) // beak
    {
        if (actor.IsEating)
            return Sprites2[108 + actor.Unit.HairStyle];
        return Sprites2[96 + actor.Unit.HairStyle];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // wings primary (white)
    {
        if (actor.IsAttacking)
        {
            return Sprites[37 + actor.Unit.BodyAccentType1];
        }
        else
        {
            return Sprites[30 + actor.Unit.BodyAccentType1];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // wings secondary (grey)
    {
        if (actor.IsAttacking)
        {
            return Sprites[40 + actor.Unit.BodyAccentType1];
        }
        else
        {
            return Sprites[33 + actor.Unit.BodyAccentType1];
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // tail primary (white)
    {
        if (actor.Unit.TailType < 8)
        {
            return Sprites[44 + actor.Unit.TailType];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // tail secondary (grey)
    {
        if (actor.Unit.TailType >= 8)
        {
            return Sprites[44 + actor.Unit.TailType];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // feet (black)
    {
        if (actor.Unit.BodySize >= 2)
        {
            return Sprites[29];
        }
        else
        {
            return Sprites[28];
        }
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // claws (black)
    {
        if (actor.IsAttacking)
        {
            if (actor.Unit.HasBreasts)
            {
                return Sprites[26];
            }
            else
            {
                return Sprites[27];
            }
        }
        else
        {
            if (actor.Unit.BodySize >= 2)
            {
                return Sprites[25];
            }
            else
            {
                return Sprites[24];
            }
        }
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // legs (grey/ secondary)
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[8 + actor.Unit.BodySize];
        }
        else
        {
            return Sprites[12 + actor.Unit.BodySize];
        }
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // arms (grey/ secondary)
    {
        if (actor.IsAttacking)
        {
            BodyAccent8.layer = 3;
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize >= 2)
                {
                    return Sprites[21];
                }
                else
                {
                    return Sprites[20];
                }
            }
            else
            {
                BodyAccent8.layer = 3;
                if (actor.Unit.BodySize >= 2)
                {
                    return Sprites[23];
                }
                else
                {
                    return Sprites[22];
                }
            }
        }
        else
        {
            BodyAccent8.layer = 6;
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize >= 2)
                {
                    return Sprites[17];
                }
                else
                {
                    return Sprites[16];
                }
            }
            else
            {
                if (actor.Unit.BodySize >= 2)
                {
                    return Sprites[19];
                }
                else
                {
                    return Sprites[18];
                }
            }
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites2[120 + actor.Unit.HairStyle];
        return null;
    }

    protected override Sprite HairSprite(Actor_Unit actor) => Sprites2[48 + actor.Unit.HairStyle + 12 * Math.Min(actor.Unit.SpecialAccessoryType, 4)]; // head secondary (grey)

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites2[132 + actor.Unit.EyeType];

    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => Sprites2[138 + actor.Unit.EyeType]; // eyebrows (grey/secondary)

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(31, 0.8f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -34 * .625f);
                return Sprites3[96];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -34 * .625f);
                return Sprites3[143];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -34 * .625f);
                return Sprites3[142];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -34 * .625f);
                return Sprites3[141];
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
                    AddOffset(Belly, 0, -33 * .625f);
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
            return Sprites[60 + actor.GetWeaponSprite()];
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
                Dick.layer = 18;
                if (actor.IsCockVoring)
                {
                    return Sprites[84 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[68 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 10;
                if (actor.IsCockVoring)
                {
                    return Sprites[92 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[76 + actor.Unit.DickSize];
                }
            }
        }

        Dick.layer = 8;
        return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect() && (actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
        {
            Balls.layer = 17;
        }
        else
        {
            Balls.layer = 7;
        }
        int size = actor.Unit.DickSize;
        int offset = actor.GetBallSize(28, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -23 * .625f);
            return Sprites[137];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -21 * .625f);
            return Sprites[136];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -19 * .625f);
            return Sprites[135];
        }
        else if (offset >= 26)
        {
            AddOffset(Balls, 0, -17 * .625f);
        }
        else if (offset == 25)
        {
            AddOffset(Balls, 0, -13 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -11 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -10 * .625f);
        }
        else if (offset == 22)
        {
            AddOffset(Balls, 0, -7 * .625f);
        }
        else if (offset == 21)
        {
            AddOffset(Balls, 0, -6 * .625f);
        }
        else if (offset == 20)
        {
            AddOffset(Balls, 0, -4 * .625f);
        }
        else if (offset == 19)
        {
            AddOffset(Balls, 0, -1 * .625f);
        }

        if (offset > 0)
            return Sprites[Math.Min(108 + offset, 134)];
        return Sprites[100 + size];
    }



    class GenericTop1 : MainClothing
    {
        public GenericTop1()
        {
            DiscardSprite = SpriteDictionary.Avians4[24];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            Type = 1524;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Avians.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Avians4[23];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Avians4[15 + actor.Unit.BreastSize];
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
            DiscardSprite = SpriteDictionary.Avians4[34];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            Type = 1534;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Avians.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Avians4[33];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Avians4[25 + actor.Unit.BreastSize];
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
            DiscardSprite = SpriteDictionary.Avians4[44];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            Type = 1544;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Avians.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Avians4[43];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Avians4[35 + actor.Unit.BreastSize];
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
            DiscardSprite = SpriteDictionary.Avians4[55];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(13, null, WhiteColored);
            Type = 1555;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Avians.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Avians4[53];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Avians4[45 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Avians4[54];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class GenericTop5 : MainClothing
    {
        public GenericTop5()
        {
            DiscardSprite = SpriteDictionary.Avians4[74];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(13, null, WhiteColored);
            Type = 1574;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Avians.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Avians4[64];
                clothing2.GetSprite = (s) => SpriteDictionary.Avians4[73];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Avians4[56 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Avians4[65 + actor.Unit.BreastSize];
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
            DiscardSprite = SpriteDictionary.Avians4[88];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            Type = 1588;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Avians.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Avians4[80 + actor.Unit.BreastSize];
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

    class MaleTop : MainClothing
    {
        public MaleTop()
        {
            DiscardSprite = SpriteDictionary.Avians4[79];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(13, null, null);
            Type = 1579;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Avians4[75 + actor.Unit.BodySize];
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
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(9, null, null);
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Avians.oversize)
            {
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => SpriteDictionary.Avians3[105];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Avians3[97 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Avians3[105];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => SpriteDictionary.Avians3[106];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ExtraColor1);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ExtraColor1);

            base.Configure(sprite, actor);
        }
    }

    class AvianRags : MainClothing
    {
        public AvianRags()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Rags[23];
            blocksDick = false;
            inFrontOfDick = 2;
            coversBreasts = false;
            Type = 207;
            OccupiesAllSlots = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(9, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(15, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BreastSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Avians4[100];
                else if (actor.Unit.BreastSize < 6)
                    clothing1.GetSprite = (s) => SpriteDictionary.Avians4[101];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Avians4[102];

                clothing2.GetSprite = (s) => SpriteDictionary.Avians4[89 + actor.Unit.BodySize];
                clothing3.GetSprite = (s) => SpriteDictionary.Avians4[99];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Avians4[97];
                clothing2.GetSprite = (s) => SpriteDictionary.Avians4[93 + actor.Unit.BodySize];
                clothing3.GetSprite = (s) => SpriteDictionary.Avians4[98];
            }
            base.Configure(sprite, actor);
        }
    }

    class AvianLeader : MainClothing
    {
        public AvianLeader()
        {
            leaderOnly = true;
            DiscardSprite = SpriteDictionary.Avians4[139];
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(9, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(10, null, WhiteColored);
            Type = 1539;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (Races.Avians.oversize)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Avians4[111];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Avians4[103 + actor.Unit.BreastSize];
                }
                clothing2.GetSprite = (s) => SpriteDictionary.Avians4[112 + actor.Unit.BodySize];
                clothing3.GetSprite = (s) => SpriteDictionary.Avians4[120 + actor.Unit.HairStyle];
                clothing4.GetSprite = null;
            }
            else
            {
                if (actor.Unit.DickSize < 3)
                    clothing4.GetSprite = (s) => SpriteDictionary.Avians4[136];
                else if (actor.Unit.DickSize > 5)
                    clothing4.GetSprite = (s) => SpriteDictionary.Avians4[138];
                else
                    clothing4.GetSprite = (s) => SpriteDictionary.Avians4[137];

                clothing1.GetSprite = (s) => SpriteDictionary.Avians4[132 + actor.Unit.BodySize];
                clothing2.GetSprite = (s) => SpriteDictionary.Avians4[116 + actor.Unit.BodySize];
                clothing3.GetSprite = null;
            }
            base.Configure(sprite, actor);
        }
    }

    class GenericBot1 : MainClothing
    {
        public GenericBot1()
        {
            DiscardSprite = SpriteDictionary.Avians3[121];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(10, null, null);
            clothing2 = new SpriteExtraInfo(9, null, null);
            Type = 1521;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.DickSize > 0)
                {
                    if (actor.Unit.DickSize < 3)
                        clothing1.GetSprite = (s) => SpriteDictionary.Avians3[115];
                    else if (actor.Unit.DickSize > 5)
                        clothing1.GetSprite = (s) => SpriteDictionary.Avians3[117];
                    else
                        clothing1.GetSprite = (s) => SpriteDictionary.Avians3[116];
                }
                else clothing1.GetSprite = null;

                clothing2.GetSprite = (s) => SpriteDictionary.Avians3[107 + actor.Unit.BodySize];
            }
            else
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Avians3[118];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Avians3[120];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Avians3[119];

                clothing2.GetSprite = (s) => SpriteDictionary.Avians3[111 + actor.Unit.BodySize];
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
            DiscardSprite = SpriteDictionary.Avians3[137];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(10, null, null);
            clothing2 = new SpriteExtraInfo(9, null, WhiteColored);
            Type = 1537;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.DickSize > 0)
                {
                    if (actor.Unit.DickSize < 3)
                        clothing1.GetSprite = (s) => SpriteDictionary.Avians3[131];
                    else if (actor.Unit.DickSize > 5)
                        clothing1.GetSprite = (s) => SpriteDictionary.Avians3[133];
                    else
                        clothing1.GetSprite = (s) => SpriteDictionary.Avians3[132];
                }
                else clothing1.GetSprite = (s) => SpriteDictionary.Avians3[130];

                clothing2.GetSprite = (s) => SpriteDictionary.Avians3[122 + actor.Unit.BodySize];
            }
            else
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Avians3[134];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Avians3[136];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Avians3[135];

                clothing2.GetSprite = (s) => SpriteDictionary.Avians3[126 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot3 : MainClothing
    {
        public GenericBot3()
        {
            DiscardSprite = SpriteDictionary.Avians3[140];
            coversBreasts = false;
            blocksDick = false;
            inFrontOfDick = 1;
            clothing1 = new SpriteExtraInfo(10, null, null);
            clothing2 = new SpriteExtraInfo(9, null, WhiteColored);
            Type = 1540;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Avians3[138];
                clothing2.GetSprite = (s) => SpriteDictionary.Avians3[122 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Avians3[139];
                clothing2.GetSprite = (s) => SpriteDictionary.Avians3[126 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot4 : MainClothing
    {
        public GenericBot4()
        {
            DiscardSprite = SpriteDictionary.Avians4[14];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(10, null, null);
            clothing2 = new SpriteExtraInfo(9, null, null);
            Type = 1514;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.DickSize > 0)
                {
                    if (actor.Unit.DickSize < 3)
                        clothing1.GetSprite = (s) => SpriteDictionary.Avians4[8];
                    else if (actor.Unit.DickSize > 5)
                        clothing1.GetSprite = (s) => SpriteDictionary.Avians4[10];
                    else
                        clothing1.GetSprite = (s) => SpriteDictionary.Avians4[9];
                }
                else clothing1.GetSprite = null;

                clothing2.GetSprite = (s) => SpriteDictionary.Avians4[0 + actor.Unit.BodySize];
            }
            else
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Avians4[11];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Avians4[13];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Avians4[12];

                clothing2.GetSprite = (s) => SpriteDictionary.Avians4[4 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }











}
