using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

class Yordles : DefaultRaceData
{
    readonly Sprite[] YordlesBodies = State.GameManager.SpriteDictionary.YordlesBodies;
    readonly Sprite[] YordlesVoreSkin = State.GameManager.SpriteDictionary.YordlesVoreSkin;
    readonly Sprite[] YordlesVoreFur = State.GameManager.SpriteDictionary.YordlesVoreFur;
    readonly Sprite[] YordlesCustomization1 = State.GameManager.SpriteDictionary.YordlesCustomization1;
    readonly Sprite[] YordlesCustomization2 = State.GameManager.SpriteDictionary.YordlesCustomization2;

    readonly YordleLeader LeaderClothes;
    readonly YordleRags YorRags;

    bool oversize = false;


    public Yordles()
    {
        BodySizes = 3;
        EyeTypes = 9; // males and females have different eyes
        MouthTypes = 10;
        SpecialAccessoryCount = 20; // ears 
        BeardStyles = 13; // Beards/cheek fluff; first beard should be for none sprite and be most likely in generation (first 5 after none are unisex (since they are cheek fluff), last 7 are males only)
        BodyAccentTypes1 = 12; // Eyebrows (first 5 unisex, last 7 males only)
        BodyAccentTypes2 = 9; // Moustaches (first moustache should be for none sprite and most likely in generation, males only!)
        HairStyles = 32; //hair types(first 22 female only, next 6 unisex, last 4 male only)
        BodyAccentTypes3 = 13; // Headfluff (if no hair present and males only! - basically replacement for furry bodies for hair, first should be for none sprite)
        // whole hair thing is a bit hard, if unit.BodyAccentType4 = 0 or 1 for males we use normal hair, otherwise headfluff, while for females we always use normal hairstyles
        BodyAccentTypes4 = 9; // body pattern types
        BodyAccentTypes5 = 14; // head pattern types

        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.YordleSkin);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.YordleSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.YordleEyes);

        ExtendedBreastSprites = true;
        FurCapable = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(22, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, s.Unit.SkinColor));
        BodyAccessory = new SpriteExtraInfo(2, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, s.Unit.SkinColor)); // Ears
        BodyAccent = new SpriteExtraInfo(28, BodyAccentSprite, null, (s) => FurryColor(s)); // moustache
        BodyAccent2 = new SpriteExtraInfo(24, BodyAccentSprite2, null, (s) => FurryColor(s)); // beards/cheek fluff
        BodyAccent3 = new SpriteExtraInfo(25, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, s.Unit.SkinColor)); ; // Hair Fluff
        BodyAccent4 = null; 
        BodyAccent5 = null;
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        Mouth = new SpriteExtraInfo(23, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(25, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, s.Unit.HairColor));
        Hair3 = new SpriteExtraInfo(27, HairSprite3, null, (s) => FurryColor(s)); // Eyebrows
        Beard = null;
        Eyes = new SpriteExtraInfo(23, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleEyes, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = new SpriteExtraInfo(4, SecondaryAccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, s.Unit.SkinColor)); // Arm
        Belly = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(6, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, s.Unit.SkinColor));

        LeaderClothes = new YordleLeader();
        YorRags = new YordleRags();

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
            YorRags,
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
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
    }

    static ColorSwapPalette FurryColor(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType5 == 0)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, actor.Unit.SkinColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, actor.Unit.HairColor);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts)
        {
            AddOffset(Dick, 0, 1 * .625f);
            AddOffset(Balls, 0, 1 * .625f);
        }
        else
        {
            AddOffset(Weapon, 0 * .625f, 0);
            if (actor.GetWeaponSprite() == 3 || actor.GetWeaponSprite() == 5 || actor.GetWeaponSprite() == 7)
            {
                AddOffset(Weapon, 0, -1 * .625f);
            }
        }
        if (actor.Unit.BodySize >= 2)
            AddOffset(Weapon, 1 * .625f, 0);

    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.HairColor = unit.SkinColor;

        unit.BodyAccentType1 = State.Rand.Next(5 + (unit.HasBreasts ? 0 : 7));
        unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3);
        unit.BodyAccentType4 = State.Rand.Next(BodyAccentTypes4);

        List<int> head_patterns = new List<int>();

        if (unit.BodyAccentType4 == 0)
        {
            head_patterns.Add(0);
        }
        else if (unit.BodyAccentType4 == 1)
        {
            head_patterns.Add(0);
            head_patterns.Add(1);
        }
        else if (unit.BodyAccentType4 >= 2 && unit.BodyAccentType4 <= 6)
        {
            head_patterns.Add(2);
            head_patterns.Add(3);
            head_patterns.Add(4);
            head_patterns.Add(7);
            head_patterns.Add(12);
        }
        else
        {
            head_patterns.Add(2);
            head_patterns.Add(3);
            head_patterns.Add(4);
            head_patterns.Add(5);
            head_patterns.Add(6);
            head_patterns.Add(7);
            head_patterns.Add(8);
            head_patterns.Add(9);
            head_patterns.Add(10);
            head_patterns.Add(11);
            head_patterns.Add(12);
            head_patterns.Add(13);
        }

        unit.BodyAccentType5 = head_patterns[State.Rand.Next(head_patterns.Count)];


        if (State.Rand.Next(2) == 0)
        {
            unit.BeardStyle = 0; // 50% chance for no beard/cheek fluff
            unit.BodyAccentType2 = 0; // 50% chance for no moustache
        }
        else if (!unit.HasBreasts) // males can have moustaches and beards/cheekfluff regardless of fur
        {
            unit.BeardStyle = State.Rand.Next(2) == 0 ? State.Rand.Next(BeardStyles) : State.Rand.Next(BeardStyles - 7); //50% chance for beard or cheek fluff, 50% for just cheek fluff 
            unit.BodyAccentType2 = State.Rand.Next(2) == 0 ? State.Rand.Next(BodyAccentTypes2) : 0; // 50% chance for moustache            
        }
        else if (unit.BodyAccentType4 <= 1)  // non furred females cant have cheek fluff
        {
            unit.BeardStyle = 0;
            unit.BodyAccentType2 = 0;
        }
        else
        {
            unit.BeardStyle = State.Rand.Next(BeardStyles - 7);  // females dont get male beards but still can get cheek fluff if furry body
            unit.BodyAccentType2 = 0;
        }

        unit.HairColor = unit.SkinColor; // for initial generation

        if (unit.HasDick && unit.HasBreasts)
        {
            if (Config.HermsOnlyUseFemaleHair)
                unit.HairStyle = State.Rand.Next(28);
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
                unit.HairStyle = 22 + State.Rand.Next(10);
            }
            else
            {
                unit.HairStyle = State.Rand.Next(28);
            }
        }

        if (unit.Type == UnitType.Leader)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(LeaderClothes);

        }

        if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(YorRags);
            if (unit.ClothingType == 0) //Covers rags not in the list
                unit.ClothingType = AllowedMainClothingTypes.Count;
        }
    }

    internal override int DickSizes => 6;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor) => YordlesBodies[0 + actor.Unit.BodySize + (actor.Unit.BodyAccentType4 * 14) + (actor.Unit.HasBreasts ? 0 : 6)];


    protected override Sprite HeadSprite(Actor_Unit actor) => YordlesCustomization1[(actor.Unit.BodyAccentType5 * 3) + (actor.IsAttacking ? 1 : (actor.IsOralVoring ? 2 : 0))];

    protected override Sprite AccessorySprite(Actor_Unit actor) => YordlesCustomization2[(actor.Unit.BodyAccentType5 == 0 ? 0 : 20) + actor.Unit.SpecialAccessoryType]; //ears

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // moustache
    {
        if (actor.Unit.BodyAccentType2 == 0 || actor.Unit.HasBreasts)
        {
            return null;
        }

        if (actor.Unit.BodyAccentType5 == 0)
        {
            return YordlesCustomization1[108 + (actor.Unit.BodyAccentType2 * 2) + (actor.IsOralVoring ? 1 : 0)];
        }
        else if (actor.Unit.BodyAccentType5 == 2 ||
            actor.Unit.BodyAccentType5 == 3 ||
            actor.Unit.BodyAccentType5 == 4 ||
            actor.Unit.BodyAccentType5 == 7 ||
            actor.Unit.BodyAccentType5 == 9 ||
            actor.Unit.BodyAccentType5 == 11)
        {
            if (actor.Unit.BodyAccentType2 > 8)
            {
                actor.Unit.BodyAccentType2 = 8;
            }
            return YordlesCustomization1[140 + (actor.Unit.BodyAccentType2 * 2) + (actor.IsOralVoring ? 1 : 0)];
        }
        else
        {
            return YordlesCustomization1[124 + (actor.Unit.BodyAccentType2 * 2) + (actor.IsOralVoring ? 1 : 0)];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // beard/cheek fluff
    {
        if (actor.Unit.BeardStyle == 0)
        {
            return null;
        }
        if (actor.Unit.BodyAccentType5 == 0)
        {
            if (actor.Unit.HasBreasts)
            {
                return YordlesCustomization2[132 + actor.Unit.BeardStyle % 5];
            }
            if (actor.Unit.BeardStyle >= 5)
                return YordlesCustomization2[137 + (actor.Unit.BeardStyle - 5) * 2 + (actor.IsOralVoring ? 1 : 0)];
            else
                return YordlesCustomization2[132 + actor.Unit.BeardStyle];


        }
        else if (actor.Unit.BodyAccentType5 == 5 ||
            actor.Unit.BodyAccentType5 == 6 ||
            actor.Unit.BodyAccentType5 == 9 ||
            actor.Unit.BodyAccentType5 == 11 ||
            actor.Unit.BodyAccentType5 == 13)
        {
            if (actor.Unit.HasBreasts)
            {
                return YordlesCustomization2[170 + actor.Unit.BeardStyle % 5];
            }
            if (actor.Unit.BeardStyle >= 5)
                return YordlesCustomization2[175 + (actor.Unit.BeardStyle - 5) * 2 + (actor.IsOralVoring ? 1 : 0)];
            else
                return YordlesCustomization2[170 + actor.Unit.BeardStyle];
        }
        else
        {
            if (actor.Unit.HasBreasts)
            {
                return YordlesCustomization2[151 + actor.Unit.BeardStyle % 5];
            }
            if (actor.Unit.BeardStyle >= 5)
                return YordlesCustomization2[156 + (actor.Unit.BeardStyle - 5) * 2 + (actor.IsOralVoring ? 1 : 0)];
            else
                return YordlesCustomization2[151 + actor.Unit.BeardStyle];
        }
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // Alternative Legs
    {
        if (actor.Unit.BodyAccentType3 == 1)
        {
            return null;
        }
        else if (actor.Unit.HasBreasts)
        {
            return YordlesCustomization2[88 + actor.Unit.BodySize];
        }
        else
        {
            return YordlesCustomization2[92 + actor.Unit.BodySize];
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return YordlesCustomization1[71];
        else if (actor.IsAttacking)
            return YordlesCustomization1[70];
        return YordlesCustomization1[60 + actor.Unit.MouthType];
    }
    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Head Fluff
    {
        if (!actor.Unit.HasBreasts && actor.Unit.BodyAccentType5 >= 1)
        {
            if (actor.Unit.BodyAccentType5 == 0)
            {
                return null;
            }

            if ((actor.Unit.BodyAccentType5 >= 1 && actor.Unit.BodyAccentType5 <= 5) ||
                actor.Unit.BodyAccentType5 == 12)
            {
                return YordlesCustomization2[108 + actor.Unit.BodyAccentType3];
            }
            else
            {
                return YordlesCustomization2[108 + actor.Unit.BodyAccentType3];
            }
        }
        return null;
    }
    protected override Sprite HairSprite(Actor_Unit actor)
    {
        if (!actor.Unit.HasBreasts && actor.Unit.BodyAccentType5 >= 1)
        {
            return null;
        }
        return YordlesCustomization2[48 + actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        if (!actor.Unit.HasBreasts && actor.Unit.BodyAccentType5 >= 1)
        {
            return null;
        }
        if (actor.Unit.HairStyle > 27)
        {
            return null;
        }
        else
        {
            return YordlesCustomization2[80 + actor.Unit.HairStyle];
        }
    }

    protected override Sprite HairSprite3(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType5 == 0)
        {
            return YordlesCustomization1[72 + actor.Unit.BodyAccentType1];
        }
        else if (actor.Unit.BodyAccentType5 == 2 || 
            actor.Unit.BodyAccentType5 == 3 || 
            actor.Unit.BodyAccentType5 == 4 || 
            actor.Unit.BodyAccentType5 == 7 || 
            actor.Unit.BodyAccentType5 == 9 || 
            actor.Unit.BodyAccentType5 == 11)
        {
            return YordlesCustomization1[96 + actor.Unit.BodyAccentType1];
        }
        else 
        {
            return YordlesCustomization1[84 + actor.Unit.BodyAccentType1];
        }
    }

    protected override Sprite EyesSprite(Actor_Unit actor) => YordlesCustomization1[(actor.Unit.HasBreasts ? 42 : 51) + actor.Unit.EyeType];

    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) //arm
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking)
                return YordlesBodies[5 + (actor.Unit.BodyAccentType4 * 14)];
            return YordlesBodies[3 + (actor.Unit.BodyAccentType4 * 14)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return YordlesBodies[4 + (actor.Unit.BodyAccentType4 * 14)];
            case 1:
                return YordlesBodies[5 + (actor.Unit.BodyAccentType4 * 14)];
            case 2:
                return YordlesBodies[5 + (actor.Unit.BodyAccentType4 * 14)];
            case 3:
                return YordlesBodies[5 + (actor.Unit.BodyAccentType4 * 14)];
            case 4:
                return YordlesBodies[5 + (actor.Unit.BodyAccentType4 * 14)];
            case 5:
                return YordlesBodies[6 + (actor.Unit.BodyAccentType4 * 14)];
            case 6:
                return YordlesBodies[5 + (actor.Unit.BodyAccentType4 * 14)];
            case 7:
                return YordlesBodies[6 + (actor.Unit.BodyAccentType4 * 14)];
            default:
                return YordlesBodies[0 + (actor.Unit.BodyAccentType4 * 14)];
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(24, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 24)
            {
                AddOffset(Belly, 0, -21 * .625f);
                if (actor.Unit.BodyAccentType4 <= 1)
                {
                    return YordlesVoreSkin[135];
                }
                return YordlesVoreFur[135];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 24)
            {
                AddOffset(Belly, 0, -20 * .625f);
                if (actor.Unit.BodyAccentType4 <= 1)
                {
                    return YordlesVoreSkin[134];
                }
                return YordlesVoreFur[134];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 23)
            {
                AddOffset(Belly, 0, -20 * .625f);
                if (actor.Unit.BodyAccentType4 <= 1)
                {
                    return YordlesVoreSkin[133];
                }
                return YordlesVoreFur[133];
            }
            switch (size)
            {
                case 18:
                    AddOffset(Belly, 0, -1 * .625f);
                    break;
                case 19:
                    AddOffset(Belly, 0, -3 * .625f);
                    break;
                case 20:
                    AddOffset(Belly, 0, -6 * .625f);
                    break;
                case 21:
                    AddOffset(Belly, 0, -9 * .625f);
                    break;
                case 22:
                    AddOffset(Belly, 0, -12 * .625f);
                    break;
                case 23:
                    AddOffset(Belly, 0, -15 * .625f);
                    break;
                case 24:
                    AddOffset(Belly, 0, -19 * .625f);
                    break;
            }
            if (actor.Unit.BodyAccentType4 <= 1)
            {
                return YordlesVoreSkin[108 + size];
            }
            return YordlesVoreFur[108 + size];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        int sprite = actor.GetWeaponSprite();

        if (sprite == 1 || sprite == 3 || sprite == 5 || sprite == 7)
        {
            Weapon.layer = 29;
        }
        else
        {
            Weapon.layer = 6;
        }
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            return YordlesCustomization2[40 + sprite];
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
                if (actor.Unit.BodyAccentType4 <= 1)
                {
                    return YordlesVoreSkin[80];
                }
                return YordlesVoreFur[80];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                if (actor.Unit.BodyAccentType4 <= 1)
                {
                    return YordlesVoreSkin[79];
                }
                return YordlesVoreFur[79];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                if (actor.Unit.BodyAccentType4 <= 1)
                {
                    return YordlesVoreSkin[78];
                }
                return YordlesVoreFur[78];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 26)
            {
                if (actor.Unit.BodyAccentType4 <= 1)
                {
                    return YordlesVoreSkin[77];
                }
                return YordlesVoreFur[77];
            }

            if (leftSize > 28)
                leftSize = 28;

            if (actor.Unit.BodyAccentType4 <= 1)
            {
                return YordlesVoreSkin[54 + leftSize];
            }
            return YordlesVoreFur[54 + leftSize];
        }
        else
        {
            if (actor.Unit.BodyAccentType4 <= 1)
            {
                return YordlesVoreSkin[54 + actor.Unit.BreastSize];
            }
            return YordlesVoreFur[54 + actor.Unit.BreastSize];
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
                if (actor.Unit.BodyAccentType4 <= 1)
                {
                    return YordlesVoreSkin[107];
                }
                return YordlesVoreFur[107];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                if (actor.Unit.BodyAccentType4 <= 1)
                {
                    return YordlesVoreSkin[106];
                }
                return YordlesVoreFur[106];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                if (actor.Unit.BodyAccentType4 <= 1)
                {
                    return YordlesVoreSkin[105];
                }
                return YordlesVoreFur[105];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 26)
            {
                if (actor.Unit.BodyAccentType4 <= 1)
                {
                    return YordlesVoreSkin[104];
                }
                return YordlesVoreFur[104];
            }

            if (rightSize > 28)
                rightSize = 28;
            if (actor.Unit.BodyAccentType4 <= 1)
            {
                return YordlesVoreSkin[81 + rightSize];
            }
            return YordlesVoreFur[81 + rightSize];
        }
        else
        {
            if (actor.Unit.BodyAccentType4 <= 1)
            {
                return YordlesVoreSkin[81 + actor.Unit.BreastSize];
            }
            return YordlesVoreFur[81 + actor.Unit.BreastSize];
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
                Dick.layer = 24;
                if (actor.IsCockVoring)
                {
                    if (actor.Unit.BodyAccentType4 <= 1)
                    {
                        return YordlesVoreSkin[18 + actor.Unit.DickSize];
                    }
                    return YordlesVoreFur[18 + actor.Unit.DickSize];
                }
                else
                {
                    if (actor.Unit.BodyAccentType4 <= 1)
                    {
                        return YordlesVoreSkin[6 + actor.Unit.DickSize];
                    }
                    return YordlesVoreFur[6 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    if (actor.Unit.BodyAccentType4 <= 1)
                    {
                        return YordlesVoreSkin[12 + actor.Unit.DickSize];
                    }
                    return YordlesVoreFur[12 + actor.Unit.DickSize];
                }
                else
                {
                    if (actor.Unit.BodyAccentType4 <= 1)
                    {
                        return YordlesVoreSkin[0 + actor.Unit.DickSize];
                    }
                    return YordlesVoreFur[0 + actor.Unit.DickSize];
                }
            }
        }
        Dick.layer = 11;
        return YordlesVoreFur[0 + actor.Unit.DickSize];
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect() && actor.HasBelly && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
        {
            Balls.layer = 19;
        }
        else
        {
            Balls.layer = 10;
        }
        int size = actor.Unit.DickSize;
        int offset = actor.GetBallSize(22, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 22)
        {
            AddOffset(Balls, 0, -20 * .625f);
            if (actor.Unit.BodyAccentType4 <= 1)
            {
                return YordlesVoreSkin[53];
            }
            return YordlesVoreFur[53];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 22)
        {
            AddOffset(Balls, 0, -18 * .625f);
            if (actor.Unit.BodyAccentType4 <= 1)
            {
                return YordlesVoreSkin[52];
            }
            return YordlesVoreFur[52];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 21)
        {
            AddOffset(Balls, 0, -15 * .625f);
            if (actor.Unit.BodyAccentType4 <= 1)
            {
                return YordlesVoreSkin[51];
            }
            return YordlesVoreFur[51];
        }
        else if (offset >= 20)
        {
            AddOffset(Balls, 0, -11 * .625f);
        }
        else if (offset == 19)
        {
            AddOffset(Balls, 0, -7 * .625f);
        }
        else if (offset == 18)
        {
            AddOffset(Balls, 0, -4 * .625f);
        }
        else if (offset == 17)
        {
            AddOffset(Balls, 0, -2 * .625f);
        }
        else if (offset == 16)
        {
            AddOffset(Balls, 0, -1 * .625f);
        }

        if (offset > 0)
        {
            if (actor.Unit.BodyAccentType4 <= 1)
            {
                return YordlesVoreSkin[Math.Min(30 + offset, 50)];
            }
            return YordlesVoreFur[Math.Min(30 + offset, 50)];
        }
        if (actor.Unit.BodyAccentType4 <= 1)
        {
            return YordlesVoreSkin[24 + size];
        }
        return YordlesVoreFur[24 + size];
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
            if (Races.Yordles.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[57];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[49 + actor.Unit.BreastSize];
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
            if (Races.Yordles.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[66];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[58 + actor.Unit.BreastSize];
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
            if (Races.Yordles.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[75];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[67 + actor.Unit.BreastSize];
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
            if (Races.Yordles.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[84];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[76 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[85];
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
            if (Races.Yordles.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[94];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[103];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[86 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[95 + actor.Unit.BreastSize];
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
            if (Races.Yordles.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[107 + actor.Unit.BreastSize];
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
            if (Races.Yordles.oversize)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[139];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[131 + actor.Unit.BreastSize];
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
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[118 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[115 + actor.Unit.BodySize];
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
            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[104 + actor.Unit.BodySize];
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
            if (Races.Yordles.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodyAccentType4 == 0 || actor.Unit.BodyAccentType4 == 1)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[2 + actor.Unit.BreastSize];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[0];
                }
                else
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[12 + actor.Unit.BreastSize];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[10];
                }
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                if (actor.Unit.BodyAccentType4 == 0 || actor.Unit.BodyAccentType4 == 1)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[1];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[11];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, actor.Unit.SkinColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.YordleSkin, actor.Unit.SkinColor);
            base.Configure(sprite, actor);
        }
    }

    class YordleRags : MainClothing
    {
        public YordleRags()
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
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[128];
                else if (actor.Unit.BreastSize < 6)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[129];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[130];

                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[121 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[127];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[124 + actor.Unit.BodySize];
            }

            base.Configure(sprite, actor);
        }
    }

    // You might really want to look at this code and fix it up, I am not a coder so coding this without ability to test is hard
    class YordleLeader : MainClothing
    {
        public YordleLeader()
        {
            leaderOnly = true;
            DiscardSprite = State.GameManager.SpriteDictionary.YordleClothes2[37];
            coversBreasts = false;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            clothing3 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing4 = new SpriteExtraInfo(19, null, WhiteColored);
            clothing5 = new SpriteExtraInfo(20, null, WhiteColored);
            clothing6 = new SpriteExtraInfo(21, null, WhiteColored);
            clothing7 = new SpriteExtraInfo(26, null, WhiteColored);
            Type = 90101;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (Races.Yordles.oversize)
                {
                    clothing4.GetSprite = null;
                }
                else
                {
                    clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[16 + actor.Unit.BreastSize];
                }
                clothing1.YOffset = 0 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[0 + actor.Unit.BodySize];
            }
            else
            {
                breastSprite = null;
                clothing1.YOffset = -1 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[3 + actor.Unit.BodySize];
                clothing4.GetSprite = null;
            }

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 2)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[38];
                else if (actor.Unit.DickSize > 3)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[40];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[39];
            }
            else clothing1.GetSprite = null;

            if (actor.HasBelly)
            {
                clothing3.GetSprite = null;
            }
            else
            {
                clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[10 + actor.Unit.BodySize + (!actor.Unit.HasBreasts ? 3 : 0)];
            }

            if (actor.GetWeaponSprite() == 0 || (actor.Unit.HasWeapon == false && !actor.IsAttacking))
            {
                if (actor.Unit.BodySize == 2)
                {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[8 + (!actor.Unit.HasBreasts ? 1 : 0)];
                    clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[27 + (!actor.Unit.HasBreasts ? 6 : 0)];
                }
                else
                {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[6 + (!actor.Unit.HasBreasts ? 1 : 0)];
                    clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[24];
                }
            }
            else if (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 2 || actor.GetWeaponSprite() == 4 || actor.GetWeaponSprite() == 6 || (actor.Unit.HasWeapon == false && actor.IsAttacking))
            {
                if (actor.Unit.BodySize == 2)
                {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[8 + (!actor.Unit.HasBreasts ? 1 : 0)];
                    clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[28 + (!actor.Unit.HasBreasts ? 6 : 0)];
                }
                else
                {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[6 + (!actor.Unit.HasBreasts ? 1 : 0)];
                    clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[25 + (!actor.Unit.HasBreasts ? 6 : 0)];
                }
            }
            else
            {
                if (actor.Unit.BodySize == 2)
                {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[8 + (!actor.Unit.HasBreasts ? 1 : 0)];
                    clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[29 + (!actor.Unit.HasBreasts ? 6 : 0)];
                }
                else
                {
                    clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[6 + (!actor.Unit.HasBreasts ? 1 : 0)];
                    clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[26 + (!actor.Unit.HasBreasts ? 6 : 0)];
                }
            }

            clothing7.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes2[36];

            base.Configure(sprite, actor);
        }
    }

    class GenericBot1 : MainClothing
    {
        public GenericBot1()
        {
            DiscardSprite = State.GameManager.SpriteDictionary.Avians3[121];
            coversBreasts = false;
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, null);
            Type = 1521;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.YOffset = 0 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[20 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.YOffset = -1 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[23 + actor.Unit.BodySize];
            }

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 2)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[26];
                else if (actor.Unit.DickSize > 3)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[28];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[27];
            }
            else clothing1.GetSprite = null;

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
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 1537;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 2)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[36];
                else if (actor.Unit.DickSize > 3)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[38];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[37];
            }
            else clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[35];

            if (actor.Unit.HasBreasts)
            {
                clothing1.YOffset = 0 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[29 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.YOffset = -1 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[32 + actor.Unit.BodySize];
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
            blocksDick = true;
            clothing1 = new SpriteExtraInfo(13, null, null);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 1540;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                clothing1.YOffset = 0 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[29 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.YOffset = -1 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[32 + actor.Unit.BodySize];
            }

            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[39];
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
            blocksDick = true;
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
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[46];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[48];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[47];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing1.YOffset = 0 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[40 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.YOffset = -1 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[43 + actor.Unit.BodySize];
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
            blocksDick = true;
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
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[46];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[48];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[47];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing1.YOffset = 0 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[140 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.YOffset = -1 * .625f;
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.YordleClothes1[143 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

}
