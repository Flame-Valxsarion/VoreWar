using System;
using System.Collections.Generic;
using UnityEngine;

class Bees : DefaultRaceData
{
    RaceFrameList frameListWings = new RaceFrameList(new int[6] { 0, 1, 2, 3, 2, 1 }, new float[6] { .05f, .05f, .05f, .05f, .05f, .05f });

    readonly Sprite[] Sprites = SpriteDictionary.Bees1;
    readonly Sprite[] Sprites2 = SpriteDictionary.Bees2;
    readonly Sprite[] Sprites3 = SpriteDictionary.Bees3;

    readonly BeeLeader LeaderClothes;
    readonly BeeRags Rags;

    bool oversize = false;

    public Bees()
    {
        BodySizes = 4;
        EyeTypes = 8;
        SpecialAccessoryCount = 6; // antennae        
        HairStyles = 18;
        MouthTypes = 0;
        EyeColors = 0;
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.BeeNewSkin);
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.BeeNewSkin);

        ExtendedBreastSprites = true;

        Body = new SpriteExtraInfo(6, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(20, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(2, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, s.Unit.SkinColor)); // Abdomen
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, WhiteColored); // Wings
        BodyAccent2 = new SpriteExtraInfo(24, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, s.Unit.SkinColor)); // Antennae 1
        BodyAccent3 = new SpriteExtraInfo(24, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, s.Unit.AccessoryColor)); // Antennae 2
        BodyAccent4 = new SpriteExtraInfo(6, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, s.Unit.AccessoryColor)); // Body 2
        BodyAccent5 = new SpriteExtraInfo(20, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, s.Unit.AccessoryColor)); // Head 2
        BodyAccent6 = new SpriteExtraInfo(4, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, s.Unit.AccessoryColor)); // Arms
        BodyAccent7 = new SpriteExtraInfo(3, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, s.Unit.AccessoryColor)); // Legs
        BodyAccent8 = new SpriteExtraInfo(3, BodyAccentSprite8, WhiteColored); // Lower Claws
        BodyAccent9 = new SpriteExtraInfo(19, BodyAccentSprite9, WhiteColored); // Upper FLuff
        BodyAccent10 = new SpriteExtraInfo(9, BodyAccentSprite10, WhiteColored); // Lower Fluff
        Mouth = new SpriteExtraInfo(21, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(23, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Hair2 = null;
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(22, EyesSprite, WhiteColored);
        SecondaryEyes = null;
        SecondaryAccessory = new SpriteExtraInfo(4, SecondaryAccessorySprite, WhiteColored); // Upper Claws
        Belly = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(5, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = new SpriteExtraInfo(8, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, s.Unit.SkinColor));
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, s.Unit.SkinColor));
        BreastShadow = new SpriteExtraInfo(2, BreastsShadowSprite, WhiteColored); // abdomen extra
        Dick = new SpriteExtraInfo(11, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, s.Unit.SkinColor));

        LeaderClothes = new BeeLeader();
        Rags = new BeeRags();

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
            new Cuirass2(),
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
        };

        DiscardData = new List<MainClothing>()
        {
            new Cuirass(), LeaderClothes
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
    }

    internal List<MainClothing> DiscardData;

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        if (unit.HasDick && unit.HasBreasts)
        {
            if (Config.HermsOnlyUseFemaleHair)
                unit.HairStyle = State.Rand.Next(12);
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
                unit.HairStyle = 8 + State.Rand.Next(10);
            }
            else
            {
                unit.HairStyle = State.Rand.Next(12);
            }
        }

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

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(State.Rand.Next(0, 6), 0, true)};  // Wing controller. Index 0.
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        if (actor.Unit.HasBreasts)
            return Sprites[0];
        return Sprites[1];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites[5];
        return Sprites[4];
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // abdomen
    {
        int sizet = actor.GetTailSize(3);
        if (actor.IsTailVoring)
            return Sprites[34 + sizet];
        return Sprites[27 + sizet];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Wings
    {
        if (actor.AnimationController.frameLists[0].currentTime >= frameListWings.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false)
        {
            actor.AnimationController.frameLists[0].currentFrame++;
            actor.AnimationController.frameLists[0].currentTime = 0f;

            if (actor.AnimationController.frameLists[0].currentFrame >= frameListWings.frames.Length)
            {
                actor.AnimationController.frameLists[0].currentFrame = 0;
                actor.AnimationController.frameLists[0].currentTime = 0f;
            }
        }

        return Sprites[42 + frameListWings.frames[actor.AnimationController.frameLists[0].currentFrame]];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) => Sprites[54 + actor.Unit.SpecialAccessoryType]; // antennae

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) => Sprites[60 + actor.Unit.SpecialAccessoryType]; // antennae 2

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // body 2
    {
        if (actor.Unit.HasBreasts)
            return Sprites[2];
        return Sprites[3];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // head 2
    {
        if (actor.IsEating)
            return Sprites[7];
        return Sprites[6];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // arms
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return Sprites[18];
            return Sprites[14];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites[14];
            case 1:
                return Sprites[15];
            case 2:
                return Sprites[16];
            case 3:
                return Sprites[17];
            case 4:
                return Sprites[14];
            case 5:
                return Sprites[18];
            case 6:
                return Sprites[19];
            case 7:
                return Sprites[20];
            default:
                return Sprites[14];
        }
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // legs
    {
        if (actor.IsTailVoring)
            return Sprites[11];
        return Sprites[10];
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // lower claws
    {
        if (actor.IsTailVoring)
            return Sprites[13];
        return Sprites[12];
    }

    protected override Sprite BodyAccentSprite9(Actor_Unit actor) // upper fluff
    {
        if (actor.Unit.ClothingType == 10 || actor.Unit.ClothingType == 11)
            return Sprites3[134];
        return Sprites[26];
    }

    protected override Sprite BodyAccentSprite10(Actor_Unit actor) // lower fluff
    {
        if (actor.Unit.ClothingType == 12 || (actor.Unit.ClothingType2 == 4 && actor.Unit.ClothingType != 9 && actor.Unit.ClothingType != 13))
            return null;
        return Sprites[25];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return Sprites[9];
        return Sprites[8];
    }

    protected override Sprite HairSprite(Actor_Unit actor) => Sprites[66 + actor.Unit.HairStyle];

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[46 + actor.Unit.EyeType];

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor)
    {
        switch (actor.GetWeaponSprite())
        {
            case 0:
                return null;
            case 1:
                return null;
            case 2:
                return Sprites[21];
            case 3:
                return Sprites[22];
            case 4:
                return null;
            case 5:
                return null;
            case 6:
                return Sprites[23];
            case 7:
                return Sprites[24];
            default:
                return null;
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(31, 0.8f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -7 * .625f);
                return Sprites2[143];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -7 * .625f);
                return Sprites2[142];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -7 * .625f);
                return Sprites2[141];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -7 * .625f);
                return Sprites2[140];
            }
            switch (size)
            {
                case 30:
                    AddOffset(Belly, 0, -1 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -6 * .625f);
                    break;
            }

            return Sprites2[108 + size];
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
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return Sprites[84];
                case 1:
                    return Sprites[85];
                case 2:
                    return null;
                case 3:
                    return null;
                case 4:
                    return Sprites[86];
                case 5:
                    return Sprites[87];
                case 6:
                    return Sprites[88];
                case 7:
                    return null;
                default:
                    return null;
            }
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodySizeSprite(Actor_Unit actor)
    {
        if (actor.Unit.BodySize > 0)
        {
            if (actor.Unit.HasBreasts)
            {
                return Sprites[89 + actor.Unit.BodySize];
            }
            else
            {
                return Sprites[92 + actor.Unit.BodySize];
            }
        }
        return null;
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
                return Sprites2[69];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return Sprites2[68];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return Sprites2[67];
            }

            if (leftSize > 28)
                leftSize = 28;

            return Sprites2[38 + leftSize];
        }
        else
        {
            return Sprites2[38 + actor.Unit.BreastSize];
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
                return Sprites2[104];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return Sprites2[103];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return Sprites2[102];
            }

            if (rightSize > 28)
                rightSize = 28;

            return Sprites2[73 + rightSize];
        }
        else
        {
            return Sprites2[73 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite BreastsShadowSprite(Actor_Unit actor) // abdomen extra
    {
        int sizee = actor.GetTailSize(3);
        if (actor.IsTailVoring)
        {
            return Sprites[38 + sizee];
        }
        else if (actor.GetTailSize(3) >= 1)
        {
            return Sprites[30 + sizee];
        }
        return null;
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
                    return Sprites[112 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[96 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    return Sprites[120 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites[104 + actor.Unit.DickSize];
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
            return Sprites2[37];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            return Sprites2[36];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            return Sprites2[35];
        }

        if (offset > 0)
            return Sprites2[Math.Min(8 + offset, 34)];
        return Sprites2[size];
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
            if (Races.Bees.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[32];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[24 + actor.Unit.BreastSize];
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
            if (Races.Bees.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[41];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[33 + actor.Unit.BreastSize];
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
            if (Races.Bees.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[50];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[42 + actor.Unit.BreastSize];
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
            Type = 1555;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Bees.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[59];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[51 + actor.Unit.BreastSize];
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
            if (Races.Bees.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[68];
                clothing2.GetSprite = (s) => SpriteDictionary.Bees3[77];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[60 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Bees3[69 + actor.Unit.BreastSize];
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
            if (Races.Bees.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[79 + actor.Unit.BreastSize];
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
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[113];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[112];
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
            clothing1.GetSprite = (s) => SpriteDictionary.Bees3[78];
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
            if (Races.Bees.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[0 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Bees3[8];
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, actor.Unit.SkinColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.BeeNewSkin, actor.Unit.SkinColor);

            base.Configure(sprite, actor);
        }
    }

    class Cuirass : MainClothing
    {
        public Cuirass()
        {
            DiscardSprite = SpriteDictionary.Bees3[133];
            coversBreasts = false;
            blocksDick = false;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(25, null, WhiteColored);
            Type = 391;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Bees.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[115 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[123];
            }

            base.Configure(sprite, actor);
        }
    }

    class Cuirass2 : MainClothing
    {
        public Cuirass2()
        {
            DiscardSprite = SpriteDictionary.Bees3[133];
            coversBreasts = false;
            blocksDick = false;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(25, null, WhiteColored);
            Type = 391;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (Races.Bees.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[124 + actor.Unit.BreastSize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[132];
            }

            base.Configure(sprite, actor);
        }
    }

    class BeeRags : MainClothing
    {
        public BeeRags()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Rags[23];
            blocksDick = false;
            inFrontOfDick = 3;
            coversBreasts = false;
            Type = 207;
            OccupiesAllSlots = true;
            FixedColor = true;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(20, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BreastSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Bees3[90];
                else if (actor.Unit.BreastSize < 6)
                    clothing1.GetSprite = (s) => SpriteDictionary.Bees3[91];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Bees3[92];
            }
            else
            {
                clothing1.GetSprite = null;
            }

            if (actor.IsTailVoring)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Bees3[88];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Bees3[87];
            }

            clothing3.GetSprite = (s) => SpriteDictionary.Bees3[89];

            base.Configure(sprite, actor);
        }
    }

    class BeeLeader : MainClothing
    {
        public BeeLeader()
        {
            leaderOnly = true;
            DiscardSprite = SpriteDictionary.Bees3[114];
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(20, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(25, null, WhiteColored);
            Type = 390;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (Races.Bees.oversize)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.Bees3[104];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.Bees3[96 + actor.Unit.BreastSize];
                }
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[93];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Bees3[94];
                clothing2.GetSprite = (s) => SpriteDictionary.Bees3[95];
            }

            if (actor.GetWeaponSprite() == 3)
            {
                if (Races.Bees.oversize)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.Bees3[110];
                }
                else
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.Bees3[107];
                }
            }
            else if (actor.GetWeaponSprite() == 7)
            {
                if (Races.Bees.oversize)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.Bees3[109];
                }
                else
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.Bees3[106];
                }
            }
            else
            {
                if (Races.Bees.oversize)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.Bees3[108];
                }
                else
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.Bees3[105];
                }
            }
            clothing4.GetSprite = (s) => SpriteDictionary.Bees3[111];

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
                    clothing1.GetSprite = (s) => SpriteDictionary.Bees3[10];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Bees3[12];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Bees3[11];
            }
            else clothing1.GetSprite = null;

            clothing2.GetSprite = (s) => SpriteDictionary.Bees3[9];

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
                    clothing1.GetSprite = (s) => SpriteDictionary.Bees3[15];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Bees3[17];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Bees3[16];
            }
            else clothing1.GetSprite = (s) => SpriteDictionary.Bees3[14];

            clothing2.GetSprite = (s) => SpriteDictionary.Bees3[13];

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
            clothing1.GetSprite = (s) => SpriteDictionary.Bees3[18];
            clothing2.GetSprite = (s) => SpriteDictionary.Bees3[13];

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
                    clothing1.GetSprite = (s) => SpriteDictionary.Bees3[21];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Bees3[23];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Bees3[22];
            }
            else clothing1.GetSprite = null;

            if (actor.IsTailVoring)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Bees3[20];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Bees3[19];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }




}

