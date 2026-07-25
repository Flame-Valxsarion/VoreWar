using System;
using System.Collections.Generic;
using UnityEngine;

class Demiants : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Demiants1;
    readonly Sprite[] Sprites2 = SpriteDictionary.Demiants2;
    readonly Sprite[] Sprites3 = SpriteDictionary.Demiants3;

    readonly DemiantRags Rags;

    bool oversize = false;

    public Demiants()
    {
        BodySizes = 3;
        EyeTypes = 8;
        SpecialAccessoryCount = 12; // antennae        
        HairStyles = 24;
        MouthTypes = 3;
        EyeColors = 0;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemiantSkin);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemiantSkin);

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(6, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.AccessoryColor)); // Lower Body (black)
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.SkinColor)); // Upper Body (White)
        BodyAccessory = new SpriteExtraInfo(2, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.AccessoryColor)); // Abdomen (black)
        BodyAccent = new SpriteExtraInfo(2, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.SkinColor)); // Abdomen 2 (White)
        BodyAccent2 = new SpriteExtraInfo(19, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.AccessoryColor)); // Antennae (black)
        BodyAccent3 = new SpriteExtraInfo(4, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.AccessoryColor)); // Upper Front Arms (black)
        BodyAccent4 = new SpriteExtraInfo(3, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.AccessoryColor)); // Lower Back Arms (black)
        Mouth = new SpriteExtraInfo(7, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(18, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Hair2 = null;
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(8, EyesSprite, WhiteColored);
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.SkinColor)); // Belly (white)
        Weapon = new SpriteExtraInfo(5, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.SkinColor)); // Breasts (white)
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, s.Unit.SkinColor)); // Balls (white)

        Rags = new DemiantRags();

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new GenericTop1(),
            new GenericTop2(),
            new GenericTop3(),
            new GenericTop4(),
            new GenericTop5(),
            new GenericTop6(),
            new MaleTop(),
            new MaleTop2(),
            new Natural(),
            new Cuirass(),
            Rags,
        };
        AvoidedMainClothingTypes = 1;
        AvoidedEyeTypes = 0;
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

        unit.AccessoryColor = unit.SkinColor;

        if (unit.HasDick && unit.HasBreasts)
        {
            if (Config.HermsOnlyUseFemaleHair)
                unit.HairStyle = State.Rand.Next(18);
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
                unit.HairStyle = 12 + State.Rand.Next(12);
            }
            else
            {
                unit.HairStyle = State.Rand.Next(18);
            }
        }

        if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(Rags);
            if (unit.ClothingType == 0) //Covers rags not in the list
                unit.ClothingType = AllowedMainClothingTypes.Count;
        }
    }

    internal override int DickSizes => 8;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor) // Lower Body(black)
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[6 + actor.Unit.BodySize];
        }
        else
        {
            return Sprites[9 + actor.Unit.BodySize];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Upper Body (White)
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[0 + actor.Unit.BodySize];
        }
        else
        {
            return Sprites[3 + actor.Unit.BodySize];
        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites[21 + actor.Unit.BodySize]; // Abdomen (black)

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites[18 + actor.Unit.BodySize]; // Abdomen 2 (White)

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Antennae (black)
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[36 + actor.Unit.SpecialAccessoryType];
        }
        else
        {
            return Sprites[48 + actor.Unit.SpecialAccessoryType];
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Upper Front Arms (black)
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return Sprites[17];
            return Sprites[124];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites[16];
            case 1:
                return Sprites[17];
            case 2:
                return Sprites[124];
            case 3:
                return Sprites[125];
            case 4:
                return Sprites[15];
            case 5:
                return Sprites[16];
            case 6:
                return Sprites[15];
            case 7:
                return Sprites[16];
            default:
                return Sprites[124];
        }
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Lower Back Arms (black)
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return Sprites[14];
            return Sprites[14];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites[12];
            case 1:
                return Sprites[12];
            case 2:
                return Sprites[13];
            case 3:
                return Sprites[14];
            case 4:
                return Sprites[14];
            case 5:
                return Sprites[14];
            case 6:
                return Sprites[14];
            case 7:
                return Sprites[14];
            default:
                return Sprites[14];
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites[32];
        return Sprites[33 + actor.Unit.MouthType];
    }

    protected override Sprite HairSprite(Actor_Unit actor) => Sprites[60 + actor.Unit.HairStyle];

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[24 + actor.Unit.EyeType];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(31, 0.8f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -32 * .625f);
                return Sprites2[99];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -32 * .625f);
                return Sprites2[98];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -32 * .625f);
                return Sprites2[97];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -32 * .625f);
                return Sprites2[96];
            }
            switch (size)
            {
                case 26:
                    AddOffset(Belly, 0, -13 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -17 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -20 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -24 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -27 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -32 * .625f);
                    break;
            }

            return Sprites2[64 + size];
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
            return Sprites[116 + actor.GetWeaponSprite()];
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
                return Sprites2[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return Sprites2[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return Sprites2[29];
            }

            if (leftSize > 28)
                leftSize = 28;

            return Sprites2[0 + leftSize];
        }
        else
        {
            return Sprites2[0 + actor.Unit.BreastSize];
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
                return Sprites2[63];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return Sprites2[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return Sprites2[61];
            }

            if (rightSize > 28)
                rightSize = 28;

            return Sprites2[32 + rightSize];
        }
        else
        {
            return Sprites2[32 + actor.Unit.BreastSize];
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
                    return Sprites[100 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[84 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    return Sprites[108 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[92 + actor.Unit.DickSize];
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
            AddOffset(Balls, 0, -21 * .625f);
            return Sprites2[137];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -19 * .625f);
            return Sprites2[136];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -17 * .625f);
            return Sprites2[135];
        }
        else if (offset >= 26)
        {
            AddOffset(Balls, 0, -15 * .625f);
        }
        else if (offset == 25)
        {
            AddOffset(Balls, 0, -11 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -9 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -8 * .625f);
        }
        else if (offset == 22)
        {
            AddOffset(Balls, 0, -5 * .625f);
        }
        else if (offset == 21)
        {
            AddOffset(Balls, 0, -4 * .625f);
        }
        else if (offset == 20)
        {
            AddOffset(Balls, 0, -2 * .625f);
        }

        if (offset > 0)
            return Sprites2[Math.Min(108 + offset, 134)];
        return Sprites2[100 + size];
    }


    class GenericTop1 : MainClothing
    {
        public GenericTop1()
        {
            DiscardSprite = SpriteDictionary.Avians4[24];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1524;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demiants.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[46];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[38 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1534;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demiants.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[55];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[47 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1544;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demiants.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[64];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[56 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 1555;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demiants.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[73];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[65 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[74];
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
            clothing1 = new SpriteExtraInfo(18, null, null);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            Type = 1574;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demiants.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[83];
                clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[92];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[75 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[84 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1588;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demiants.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[96 + actor.Unit.BreastSize];
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
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1579;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.HasBelly)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[107 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[104 + actor.Unit.BodySize];
            }
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class MaleTop2 : MainClothing
    {
        public MaleTop2()
        {
            DiscardSprite = SpriteDictionary.Avians4[79];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = false;
            clothing1 = new SpriteExtraInfo(18, null, null);
            Type = 1579;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[93 + actor.Unit.BodySize];
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
            if (Races.Demiants.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[0 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[8];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, actor.Unit.SkinColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
        }
    }

    class Cuirass : MainClothing
    {
        public Cuirass()
        {
            DiscardSprite = SpriteDictionary.Demiants3[138];
            coversBreasts = false;
            OccupiesAllSlots = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(7, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(7, null, WhiteColored);
            Type = 195;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Demiants.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[124];
            }
            else if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BreastSize < 2)
                {
                    breastSprite = null;
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[120];
                }
                else if (actor.Unit.BreastSize < 4)
                {
                    breastSprite = null;
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[121];
                }
                else if (actor.Unit.BreastSize < 6)
                {
                    breastSprite = null;
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[122];
                }
                else
                {
                    breastSprite = null;
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[123];
                }
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[125];
            }

            if (actor.HasBelly)
            {
                clothing2.GetSprite = null;
            }
            else
            {
                if (actor.Unit.HasBreasts)
                    clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[126 + actor.Unit.BodySize];
                else
                    clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[129 + actor.Unit.BodySize];
            }

            clothing3.GetSprite = (s) => SpriteDictionary.Demiants3[132 + actor.Unit.BodySize];

            if (actor.GetWeaponSprite() == 1)
            {
                clothing4.GetSprite = (s) => SpriteDictionary.Demiants3[136];
            }
            else if (actor.GetWeaponSprite() == 3)
            {
                clothing4.GetSprite = (s) => SpriteDictionary.Demiants3[137];
            }
            else
            {
                clothing4.GetSprite = (s) => SpriteDictionary.Demiants3[135];
            }

            base.Configure(sprite, actor);
        }
    }

    class DemiantRags : MainClothing
    {
        public DemiantRags()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Rags[23];
            inFrontOfDick = 2;
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
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[117];
                else if (actor.Unit.BreastSize < 6)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[118];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[119];

                clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[110 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[116];
                clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[113 + actor.Unit.BodySize];
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
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[15];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[17];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[16];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[9 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[12 + actor.Unit.BodySize];
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
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[25];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[27];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[26];
            }
            else clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[24];

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[18 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[21 + actor.Unit.BodySize];
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
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 1540;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[28];

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[18 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[21 + actor.Unit.BodySize];
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
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[35];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[37];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Demiants3[36];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[29 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demiants3[32 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }


}
