using System;
using System.Collections.Generic;
using UnityEngine;

class Demibats : DefaultRaceData
{
    RaceFrameList frameListDemibatWings = new RaceFrameList(new int[4] { 0, 1, 0, 2 }, new float[4] { .15f, .25f, .15f, .25f });

    readonly Sprite[] Sprites = SpriteDictionary.Demibats1;
    readonly Sprite[] Sprites2 = SpriteDictionary.Demibats2;
    readonly Sprite[] Sprites3 = SpriteDictionary.Demibats3;

    readonly DemibatLeader LeaderClothes;
    readonly DemibatRags Rags;

    bool oversize = false;

    public Demibats()
    {
        BodySizes = 4;
        EyeTypes = 6;
        SpecialAccessoryCount = 17; // ears        
        HairStyles = 24;
        MouthTypes = 4;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemibatSkin); // Fur colors
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemibatHumanSkin); // Skin colors for demi form
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ViperSkin);
        BodyAccentTypes1 = 4; // collar fur

        ExtendedBreastSprites = true;
        FurCapable = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatSkin, s.Unit.AccessoryColor)); // fur part of demi form or full furry form
        Head = new SpriteExtraInfo(4, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatHumanSkin, s.Unit.SkinColor)); // human part of demi form
        BodyAccessory = new SpriteExtraInfo(21, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatSkin, s.Unit.AccessoryColor)); // ears
        BodyAccent = new SpriteExtraInfo(5, BodyAccentSprite, null, (s) => FurryColor(s)); // mouth external part
        BodyAccent2 = new SpriteExtraInfo(4, BodyAccentSprite2, WhiteColored); //lower claws
        BodyAccent3 = new SpriteExtraInfo(19, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatSkin, s.Unit.AccessoryColor)); // collar fur
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => FurryColor(s)); // eyebrows
        BodyAccent5 = new SpriteExtraInfo(3, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatSkin, s.Unit.AccessoryColor)); // wings main
        BodyAccent6 = new SpriteExtraInfo(3, BodyAccentSprite6, null, (s) => FurryColor(s)); // arms main
        BodyAccent7 = new SpriteExtraInfo(3, BodyAccentSprite7, WhiteColored); // thumbs
        Mouth = new SpriteExtraInfo(5, MouthSprite, WhiteColored); // mouth teeths/internal
        Hair = new SpriteExtraInfo(20, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor)); // hair part below ears
        Hair2 = new SpriteExtraInfo(22, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor)); // hair part above ears
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(5, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, s.Unit.EyeColor));
        SecondaryEyes = new SpriteExtraInfo(5, EyesSecondarySprite, WhiteColored); // white/black sclera
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(14, null, null, (s) => FurryColor(s));
        Weapon = new SpriteExtraInfo(6, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => FurryColor(s));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => FurryColor(s));
        Dick = new SpriteExtraInfo(11, DickSprite, null, (s) => FurryColor(s));
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => FurryColor(s));

        LeaderClothes = new DemibatLeader();
        Rags = new DemibatRags();

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new GenericTop1(),
            new GenericTop2(),
            new GenericTop3(),
            new GenericTop4(),
            new GenericTop5(),
            new GenericTop6(),
            new GenericTop7(),
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
        };

        AllowedClothingHatTypes = new List<ClothingAccessory>()
        {
            new BatHat()
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
    }

    ColorSwapPalette FurryColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatSkin, actor.Unit.AccessoryColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatHumanSkin, actor.Unit.SkinColor);
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[] {
            new AnimationController.FrameList(State.Rand.Next(0, 4), 0, true)};  // Wing controller. Index 0.
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        if (unit.Type == UnitType.Leader)
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(LeaderClothes);
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
                unit.HairStyle = 10 + State.Rand.Next(14);
            }
            else
            {
                unit.HairStyle = State.Rand.Next(18);
            }
        }

        unit.BodyAccentType1 = 0;

        if (Config.WinterActive())
        {
            if (State.Rand.Next(2) == 0)
            {
                unit.ClothingHatType = 1;
            }
            else
                unit.ClothingHatType = 0;
        }

        if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(Rags);
            if (unit.ClothingType == 0) //Covers rags not in the list
                unit.ClothingType = AllowedMainClothingTypes.Count;
        }

    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (!(actor.Unit.Furry && Config.FurryGenitals))
        {
            AddOffset(Balls, 0, 1 * .625f);
        }

        if (actor.Unit.Furry && (actor.BestRanged != null) && !(actor.Unit.BodyAccentType1 == 3))
        {
            AddOffset(Weapon, 0, 2 * .625f);
        }
    }

    internal override int DickSizes => 8;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.AnimationController.frameLists == null) SetUpAnimations(actor);

        if (actor.Unit.HasBreasts)
        {
            return Sprites[0 + actor.Unit.BodySize + (actor.IsAttacking ? 4 : 0) + (actor.Unit.Furry ? 16 : 0)];
        }
        else
        {
            return Sprites[8 + actor.Unit.BodySize + (actor.IsAttacking ? 4 : 0) + (actor.Unit.Furry ? 16 : 0)];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return null;
        }
        else if (actor.Unit.HasBreasts)
        {
            return Sprites[32 + actor.Unit.BodySize + (actor.IsAttacking ? 4 : 0)];
        }
        else
        {
            return Sprites[40 + actor.Unit.BodySize + (actor.IsAttacking ? 4 : 0)];
        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites[107 + actor.Unit.SpecialAccessoryType]; //ears

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // mouth external part
    {
        if (actor.IsEating || actor.IsAttacking)
            return Sprites[71];
        return Sprites[67 + actor.Unit.MouthType];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // lower claws
    {
        if (actor.IsAttacking)
            return Sprites[61];
        return Sprites[60];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)  // collar fur // lower claws
    {
        if (actor.Unit.BodyAccentType1 == 3 || (!actor.Unit.Furry && (actor.Unit.ClothingType == 3 || actor.Unit.ClothingType == 8 || actor.Unit.ClothingType == 9 || (actor.Unit.ClothingType == 12 && !actor.Unit.HasBreasts))))
            return null;
        return Sprites[101 + actor.Unit.BodyAccentType1 + (actor.Unit.Furry ? 3 : 0)]; ;
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) => Sprites[88 + actor.Unit.EyeType + (actor.Unit.Furry ? 6 : 0)]; // eyebrows

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // wings main animated
    {
        if (!actor.Targetable) return Sprites[48];

        if (actor.IsAttacking)
        {
            actor.AnimationController.frameLists[0].currentlyActive = false;
            actor.AnimationController.frameLists[0].currentFrame = 0;
            actor.AnimationController.frameLists[0].currentTime = 0f;
            return Sprites[49];
        }
        else
        {
            actor.AnimationController.frameLists[0].currentlyActive = true;
        }

        if (actor.AnimationController.frameLists[0].currentTime >= frameListDemibatWings.times[actor.AnimationController.frameLists[0].currentFrame] && actor.Unit.IsDead == false && actor.IsAttacking == false)
        {
            actor.AnimationController.frameLists[0].currentFrame++;
            actor.AnimationController.frameLists[0].currentTime = 0f;

            if (actor.AnimationController.frameLists[0].currentFrame >= frameListDemibatWings.frames.Length)
            {
                actor.AnimationController.frameLists[0].currentFrame = 0;
                actor.AnimationController.frameLists[0].currentTime = 0f;
            }
        }
        return Sprites[48 + frameListDemibatWings.frames[actor.AnimationController.frameLists[0].currentFrame]];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // arms main animated, Frame list functions handled by the BodyAccentSprite5 method.
    {
        if (actor.IsAttacking)
        {
            return Sprites[52 + (actor.Unit.Furry ? 3 : 0)];
        }
        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            return Sprites[51 + frameListDemibatWings.frames[actor.AnimationController.frameLists[0].currentFrame] + (actor.Unit.Furry ? 3 : 0)];
        }
        return Sprites[51 + (actor.Unit.Furry ? 3 : 0)];
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // thumbs animated, Frame list functions handled by the BodyAccentSprite5 method.
    {
        if (actor.IsAttacking)
        {
            return Sprites[58];
        }
        if (actor.AnimationController.frameLists[0].currentlyActive)
        {
            return Sprites[57 + frameListDemibatWings.frames[actor.AnimationController.frameLists[0].currentFrame]];
        }
        return Sprites[57];
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
            return Sprites[73 + (actor.Unit.Furry ? 2 : 0)];
        return Sprites[72 + (actor.Unit.Furry ? 2 : 0)];
    }

    protected override Sprite HairSprite(Actor_Unit actor) => Sprites2[0 + actor.Unit.HairStyle];

    protected override Sprite HairSprite2(Actor_Unit actor) => Sprites2[24 + actor.Unit.HairStyle];

    protected override Sprite EyesSprite(Actor_Unit actor) => Sprites[100];

    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => Sprites[76 + actor.Unit.EyeType + (actor.Unit.Furry ? 6 : 0)];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(31, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -31 * .625f);
                return Sprites3[99];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -31 * .625f);
                return Sprites3[98];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -31 * .625f);
                return Sprites3[97];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -31 * .625f);
                return Sprites3[96];
            }
            switch (size)
            {
                case 26:
                    AddOffset(Belly, 0, -5 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -10 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -15 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -18 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -24 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -30 * .625f);
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
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    Weapon.layer = 6;
                    return Sprites[124];
                case 1:
                    Weapon.layer = 6;
                    return Sprites[125];
                case 2:
                    Weapon.layer = 6;
                    return Sprites[126];
                case 3:
                    Weapon.layer = 6;
                    return Sprites[127];
                case 4:
                    Weapon.layer = 23;
                    return Sprites[128 + (actor.Unit.BodyAccentType1 == 3 || (!actor.Unit.Furry && (actor.Unit.ClothingType == 3 || actor.Unit.ClothingType == 8 || actor.Unit.ClothingType == 9 || (actor.Unit.ClothingType == 12 && !actor.Unit.HasBreasts))) ? 5 : 0)];
                case 5:
                    Weapon.layer = 23;
                    return Sprites[129 + (actor.Unit.BodyAccentType1 == 3 || (!actor.Unit.Furry && (actor.Unit.ClothingType == 3 || actor.Unit.ClothingType == 8 || actor.Unit.ClothingType == 9 || (actor.Unit.ClothingType == 12 && !actor.Unit.HasBreasts))) ? 5 : 0)];
                case 6:
                    Weapon.layer = 23;
                    return Sprites[130 + (actor.Unit.BodyAccentType1 == 3 || (!actor.Unit.Furry && (actor.Unit.ClothingType == 3 || actor.Unit.ClothingType == 8 || actor.Unit.ClothingType == 9 || (actor.Unit.ClothingType == 12 && !actor.Unit.HasBreasts))) ? 5 : 0)];
                case 7:
                    Weapon.layer = 23;
                    return Sprites[131 + (actor.Unit.BodyAccentType1 == 3 || (!actor.Unit.Furry && (actor.Unit.ClothingType == 3 || actor.Unit.ClothingType == 8 || actor.Unit.ClothingType == 9 || (actor.Unit.ClothingType == 12 && !actor.Unit.HasBreasts))) ? 5 : 0)];
                default:
                    Weapon.layer = 6;
                    return null;
            }
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

        if (actor.Unit.Furry && Config.FurryGenitals)
        {
            Dick.GetPalette = null;
            Dick.GetColor = WhiteColored;

            if (actor.IsErect())
            {
                if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
                {
                    Dick.layer = 24;
                    if (actor.IsCockVoring)
                    {
                        return Sprites2[96 + actor.Unit.DickSize];
                    }
                    else
                    {
                        return Sprites2[80 + actor.Unit.DickSize];
                    }
                }
                else
                {
                    Dick.layer = 13;
                    if (actor.IsCockVoring)
                    {
                        return Sprites2[104 + actor.Unit.DickSize];
                    }
                    else
                    {
                        return Sprites2[88 + actor.Unit.DickSize];
                    }
                }
            }

            Dick.layer = 11;
            return null;
        }

        if (Dick.GetPalette == null)
        {
            Dick.GetPalette = (s) => FurryColor(s);
            Dick.GetColor = null;
        }

        if (actor.IsErect())
        {
            if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
            {
                Dick.layer = 24;
                if (actor.IsCockVoring)
                {
                    return Sprites2[72 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites2[64 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    return Sprites2[56 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites2[48 + actor.Unit.DickSize];
                }
            }
        }

        Dick.layer = 11;
        return Sprites2[48 + actor.Unit.DickSize];
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
            AddOffset(Balls, 0, -18 * .625f);
            return Sprites3[137 + ((actor.Unit.Furry && Config.FurryGenitals) ? 38 : 0)];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return Sprites3[136 + ((actor.Unit.Furry && Config.FurryGenitals) ? 38 : 0)];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return Sprites3[135 + ((actor.Unit.Furry && Config.FurryGenitals) ? 38 : 0)];
        }
        else if (offset >= 26)
        {
            AddOffset(Balls, 0, -15 * .625f);
        }
        else if (offset == 25)
        {
            AddOffset(Balls, 0, -9 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -7 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -6 * .625f);
        }
        else if (offset == 22)
        {
            AddOffset(Balls, 0, -3 * .625f);
        }
        else if (offset == 21)
        {
            AddOffset(Balls, 0, -2 * .625f);
        }

        if (offset > 0)
            return Sprites3[Math.Min(108 + offset, 134) + ((actor.Unit.Furry && Config.FurryGenitals) ? 38 : 0)];
        return Sprites3[100 + size + ((actor.Unit.Furry && Config.FurryGenitals) ? 38 : 0)];
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
            if (Races.Demibats.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[31];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[23 + actor.Unit.BreastSize];
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
            if (Races.Demibats.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[40];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[32 + actor.Unit.BreastSize];
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
            if (Races.Demibats.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[49];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[41 + actor.Unit.BreastSize];
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
            if (Races.Demibats.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[58];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[50 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Demibats4[59];
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
            if (Races.Demibats.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[68];
                clothing2.GetSprite = (s) => SpriteDictionary.Demibats4[77];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[60 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Demibats4[69 + actor.Unit.BreastSize];
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
            if (Races.Demibats.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[81 + actor.Unit.BreastSize];
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
            if (Races.Demibats.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[124];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[116 + actor.Unit.BreastSize];
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
                if (actor.Unit.BodySize == 3)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[94];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[93];
                }
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[89 + actor.Unit.BodySize];
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
            if (actor.Unit.BodySize == 3)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[80];
            }
            else if (actor.Unit.BodySize == 2)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[79];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[78];
            }
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
            if (Races.Demibats.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats2[112 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Demibats2[120];

            if (actor.Unit.Furry)
            {
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatSkin, actor.Unit.AccessoryColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatSkin, actor.Unit.AccessoryColor);
            }
            else
            {
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatHumanSkin, actor.Unit.SkinColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemibatHumanSkin, actor.Unit.SkinColor);
            }

            base.Configure(sprite, actor);
        }
    }

    class DemibatRags : MainClothing
    {
        public DemibatRags()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Rags[23];
            blocksDick = false;
            inFrontOfDick = 2;
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

            if (actor.Unit.Furry)
            {
                clothing3.GetSprite = (s) => SpriteDictionary.Demibats4[115];
            }
            else
            {
                clothing3.GetSprite = (s) => SpriteDictionary.Demibats4[111];
            }

            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BreastSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[112];
                else if (actor.Unit.BreastSize < 6)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[113];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[114];

                clothing2.GetSprite = (s) => SpriteDictionary.Demibats4[95 + actor.Unit.BodySize + (actor.IsAttacking ? 4 : 0)];
            }
            else
            {
                clothing1.GetSprite = (s) => null;
                clothing2.GetSprite = (s) => SpriteDictionary.Demibats4[103 + actor.Unit.BodySize + (actor.IsAttacking ? 4 : 0)];
            }

            base.Configure(sprite, actor);
        }
    }

    class DemibatLeader : MainClothing
    {
        public DemibatLeader()
        {
            leaderOnly = true;
            DiscardSprite = SpriteDictionary.Demibats4[153];
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 61501;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (Races.Demibats.oversize)
            {
                clothing1.GetSprite = (s) => null;
                clothing3.GetSprite = (s) => SpriteDictionary.Demibats4[133 + actor.Unit.BodySize + (actor.IsAttacking ? 4 : 0)];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[125 + actor.Unit.BreastSize];
                clothing3.GetSprite = (s) => SpriteDictionary.Demibats4[133 + actor.Unit.BodySize + (actor.IsAttacking ? 4 : 0)];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing3.GetSprite = (s) => SpriteDictionary.Demibats4[141 + actor.Unit.BodySize];
            }

            if (actor.HasBelly)
            {
                clothing2.GetSprite = (s) => null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demibats4[145 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demibats4[149 + actor.Unit.BodySize];
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
                    clothing1.GetSprite = (s) => SpriteDictionary.Demibats2[129 + (actor.Unit.BodySize == 3 ? 3 : 0)];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demibats2[131 + (actor.Unit.BodySize == 3 ? 3 : 0)];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Demibats2[130 + (actor.Unit.BodySize == 3 ? 3 : 0)];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demibats2[121 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demibats2[125 + actor.Unit.BodySize];
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
                    clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[1];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[3];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[2];
            }
            else clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[0];

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demibats2[135 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demibats2[139 + actor.Unit.BodySize];
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
            clothing1.GetSprite = (s) => SpriteDictionary.Demibats2[143];

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demibats2[135 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demibats2[139 + actor.Unit.BodySize];
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
                    clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[20];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[22];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Demibats4[21];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demibats4[4 + actor.Unit.BodySize + (actor.IsAttacking ? 4 : 0)];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Demibats4[12 + actor.Unit.BodySize + (actor.IsAttacking ? 4 : 0)];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }


    class BatHat : ClothingAccessory
    {
        public BatHat()
        {
            clothing1 = new SpriteExtraInfo(28, null, null);
            ReqWinterHoliday = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetSprite = (s) => SpriteDictionary.HatBat;
            base.Configure(sprite, actor);
        }
    }

}
