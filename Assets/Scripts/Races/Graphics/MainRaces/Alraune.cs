using AlrauneClothing;
using System.Collections.Generic;
using UnityEngine;


class Alraune : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Alraune;
    readonly float yOffset = 10 * .625f;
    readonly AlrauneLeader LeaderClothes;
    AlrauneRags Rags;
    public Alraune()
    {
        BodySizes = 4;
        HairStyles = 12;
        SpecialAccessoryCount = 16;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AlrauneFoliage); // head flower and upper petals
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AlrauneHair);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AlrauneSkin);
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AlrauneFoliage); // lower petals and base roots
        BodyAccentTypes1 = 9; // upper petals
        BodyAccentTypes2 = 10; // lower petals
        BodyAccentTypes3 = 8; // base roots

        Body = new SpriteExtraInfo(3, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(7, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(7, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, s.Unit.AccessoryColor)); // head flower
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, s.Unit.AccessoryColor)); // upper petals
        BodyAccent2 = new SpriteExtraInfo(2, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, s.Unit.ExtraColor1)); //lower petals
        BodyAccent3 = new SpriteExtraInfo(1, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, s.Unit.ExtraColor1)); // base roots
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneHair, s.Unit.HairColor)); // eyebrows
        BodyAccent5 = new SpriteExtraInfo(7, BodyAccentSprite5, WhiteColored); // christmas head flower
        BodyAccent6 = new SpriteExtraInfo(2, BodyAccentSprite6, WhiteColored); // christmas lower petals
        BodyAccent7 = new SpriteExtraInfo(1, BodyAccentSprite7, WhiteColored); // christmas base roots
        Mouth = new SpriteExtraInfo(4, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneSkin, s.Unit.SkinColor));
        Hair = new SpriteExtraInfo(6, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneHair, s.Unit.HairColor));
        Hair2 = null;
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(5, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(15, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneSkin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(12, WeaponSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, s.Unit.ClothingColor));
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneSkin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneSkin, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneSkin, s.Unit.SkinColor));

        LeaderClothes = new AlrauneLeader();

        Rags = new AlrauneRags();

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new AlrauneLeafs(),
            new AlrauneVines1(),
            new AlrauneVines2(),
            new AlrauneMoss(),
            new AlrauneChristmas(),
            Rags,
            LeaderClothes
        };
        AllowedWaistTypes = new List<MainClothing>()
        {
        };

        AllowedClothingHatTypes = new List<ClothingAccessory>();
        AvoidedMainClothingTypes = 2;
        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AlrauneFoliage);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(Rags);
            if (unit.ClothingType == -1) //Covers rags not in the list
                unit.ClothingType = 1;
        }

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
                unit.HairStyle = 5 + State.Rand.Next(7);
            }
            else
            {
                unit.HairStyle = State.Rand.Next(5);
            }
        }

        if (State.Rand.Next(2) == 0)
            unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1 - 1);
        else
        {
            unit.BodyAccentType1 = (BodyAccentTypes1 - 1);
        }

        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
        unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(BodyAccent4, 0, yOffset);
        AddOffset(Mouth, 0, yOffset);
        AddOffset(Eyes, 0, yOffset);
        AddOffset(Belly, 0, yOffset);
        AddOffset(Breasts, 0, yOffset);
        AddOffset(Dick, 0, yOffset);
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
            return Sprites[8 + (actor.IsAttacking ? 1 : 0) + (2 * actor.Unit.BodySize)];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites[16];
        return null;
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // head flower currently only for females and herms
    {
        if (actor.Unit.HasBreasts && actor.Unit.ClothingType != 5)
        {
            return Sprites[44 + actor.Unit.SpecialAccessoryType];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // upper petals
    {
        if (actor.Unit.ClothingType != 5)
        {
            return Sprites[17 + actor.Unit.BodyAccentType1];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // lower petals
    {
        if (actor.Unit.ClothingType != 5)
        {
            return Sprites[26 + actor.Unit.BodyAccentType2];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // base roots
    {
        if (actor.Unit.ClothingType != 5)
        {
            return Sprites[36 + actor.Unit.BodyAccentType3];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // christmas head flower
    {
        if (actor.Unit.ClothingType == 5)
        {
            return SpriteDictionary.AlrauneChristmas[12];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // christmas lower petals
    {
        if (actor.Unit.ClothingType == 5)
        {
            return SpriteDictionary.AlrauneChristmas[1];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // christmas base roots
    {
        if (actor.Unit.ClothingType == 5)
        {
            return SpriteDictionary.AlrauneChristmas[0];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite HairSprite(Actor_Unit actor) => Sprites[60 + actor.Unit.HairStyle];

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            return Sprites[72 + actor.GetWeaponSprite()];
        }
        else
        {
            return null;
        }
    }
}

namespace AlrauneClothing
{
    class AlrauneLeafs : MainClothing
    {
        public AlrauneLeafs()
        {
            DiscardSprite = null;
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(17, null, null);
            clothing2 = new SpriteExtraInfo(10, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Alraune[84 + actor.Unit.BreastSize];
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, actor.Unit.ClothingColor);
            }
            else
            {
                clothing1.GetSprite = null;
            }


            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[80];
                else if (actor.Unit.DickSize > 5)
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[82];
                else
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[81];
            }
            else clothing2.GetSprite = (s) => SpriteDictionary.Alraune[83];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }

    class AlrauneVines1 : MainClothing
    {
        public AlrauneVines1()
        {
            DiscardSprite = null;
            coversBreasts = false;
            blocksDick = false;
            inFrontOfDick = 2;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(17, null, null);
            clothing2 = new SpriteExtraInfo(10, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Alraune[96 + actor.Unit.BreastSize];
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, actor.Unit.ClothingColor);
            }
            else
            {
                clothing1.GetSprite = null;
            }


            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[92];
                else if (actor.Unit.DickSize > 5)
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[94];
                else
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[93];
            }
            else clothing2.GetSprite = (s) => SpriteDictionary.Alraune[95];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }

    class AlrauneVines2 : MainClothing
    {
        public AlrauneVines2()
        {
            DiscardSprite = null;
            coversBreasts = false;
            blocksDick = false;
            inFrontOfDick = 2;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(17, null, null);
            clothing2 = new SpriteExtraInfo(10, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Alraune[108 + actor.Unit.BreastSize];
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, actor.Unit.ClothingColor);
            }
            else
            {
                clothing1.GetSprite = null;
            }


            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[104];
                else if (actor.Unit.DickSize > 5)
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[106];
                else
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[105];
            }
            else clothing2.GetSprite = (s) => SpriteDictionary.Alraune[107];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }

    class AlrauneMoss : MainClothing
    {
        public AlrauneMoss()
        {
            DiscardSprite = null;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(17, null, null);
            clothing2 = new SpriteExtraInfo(10, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Alraune[120 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Alraune[120];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, actor.Unit.ClothingColor);

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[116];
                else if (actor.Unit.DickSize > 5)
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[118];
                else
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[117];
            }
            else clothing2.GetSprite = (s) => SpriteDictionary.Alraune[119];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }

    class AlrauneChristmas : MainClothing
    {
        public AlrauneChristmas()
        {
            DiscardSprite = null;
            coversBreasts = false;
            OccupiesAllSlots = true;
            ReqWinterHoliday = true;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(10, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(17, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.AlrauneChristmas[2 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = null;
            }

            clothing2.GetSprite = (s) => SpriteDictionary.AlrauneChristmas[10];

            clothing3.GetSprite = (s) => SpriteDictionary.AlrauneChristmas[11];

            base.Configure(sprite, actor);
        }

    }

    class AlrauneRags : MainClothing
    {
        public AlrauneRags()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Rags[23];
            OccupiesAllSlots = true;
            Type = 207;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(17, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(10, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(18, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Alraune[144 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = null;
            }

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[140];
                else if (actor.Unit.DickSize > 5)
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[142];
                else
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[141];
            }
            else clothing2.GetSprite = (s) => SpriteDictionary.Alraune[143];

            clothing3.GetSprite = (s) => SpriteDictionary.Alraune[152];

            base.Configure(sprite, actor);
        }

    }

    class AlrauneLeader : MainClothing
    {
        public AlrauneLeader()
        {
            leaderOnly = true;
            DiscardSprite = null;
            OccupiesAllSlots = true;
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(17, null, null);
            clothing2 = new SpriteExtraInfo(10, null, null);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Alraune[132 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Alraune[132];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, actor.Unit.ClothingColor);

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[128];
                else if (actor.Unit.DickSize > 5)
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[130];
                else
                    clothing2.GetSprite = (s) => SpriteDictionary.Alraune[129];
            }
            else clothing2.GetSprite = (s) => SpriteDictionary.Alraune[131];

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }

    }
}
