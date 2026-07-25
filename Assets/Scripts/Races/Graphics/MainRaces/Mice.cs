using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

class Mice : DefaultRaceData
{
    readonly Sprite[] Sprites = SpriteDictionary.Mice1;
    readonly Sprite[] Sprites2 = SpriteDictionary.Mice2;
    readonly Sprite[] Sprites3 = SpriteDictionary.MiceFBottoms;
    readonly Sprite[] Sprites4 = SpriteDictionary.MiceFTops;
    readonly Sprite[] Sprites5 = SpriteDictionary.MiceMBottoms;
    readonly Sprite[] Sprites6 = SpriteDictionary.MiceMTops;
    readonly Sprite[] Sprites7 = SpriteDictionary.MiceVore1;
    readonly Sprite[] Sprites8 = SpriteDictionary.MiceVore2;
    readonly Sprite[] Sprites9 = SpriteDictionary.MiceVore3;

    readonly MiceRags Rags;

    bool oversize = false;


    public Mice()
    {
        BodySizes = 4;
        EyeTypes = 6;
        HairStyles = 25;
        MouthTypes = 6;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MiceSkin); // Fur colors
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Skin); // Skin colors for demi form
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair); 
        EarTypes = 4;
        TailTypes = 4;
        BodyAccentTypes1 = 2; // Face pattern [1: large; 2: small]
        BodyAccentTypes2 = 3; // Chest pattern [1: none; 2: chest only; 3: chest thighs short]
        BodyAccentTypes3 = 7; // hands/feet pattern [1: none; 2: short both; 3: long both; 4: short feet; 5: long feet; 6: short hands; 7: long hands]
        BodyAccentTypes4 = 3; // ear damage
        BodyAccentTypes5 = 3; // ear damage

        ExtendedBreastSprites = true;
        FurCapable = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => FurryColor(s));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => FurryColor(s));
        BodyAccessory = new SpriteExtraInfo(2, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, s.Unit.AccessoryColor)); // Tail type
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, null, (s) => FurryColor(s)); // Right Arm
        BodyAccent2 = new SpriteExtraInfo(3, BodyAccentSprite2, null, (s) => FurryColor(s)); // leg type
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, s.Unit.AccessoryColor)); // pattern feet
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, s.Unit.AccessoryColor)); // pattern hands
        BodyAccent5 = new SpriteExtraInfo(5, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, s.Unit.AccessoryColor)); // pattern chest
        BodyAccent6 = new SpriteExtraInfo(5, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, s.Unit.AccessoryColor)); // pattern thighs
        BodyAccent7 = new SpriteExtraInfo(7, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, s.Unit.AccessoryColor)); // pattern face
        BodyAccent8 = new SpriteExtraInfo(22, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, s.Unit.AccessoryColor)); // left ear
        BodyAccent9 = new SpriteExtraInfo(22, BodyAccentSprite9, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, s.Unit.AccessoryColor)); // right ear
        Mouth = new SpriteExtraInfo(8, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(21, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(0, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(8, EyesSprite, WhiteColored);
        SecondaryEyes = new SpriteExtraInfo(7, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(14, null, null, (s) => FurryColor(s));
        Weapon = new SpriteExtraInfo(8, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => FurryColor(s));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => FurryColor(s));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, null, (s) => FurryColor(s));
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => FurryColor(s));

        Rags = new MiceRags();

        AllowedMainClothingTypes = new List<MainClothing>()
        {
            new GenericTop1(),
            new GenericTop2(),
            new GenericTop3(),
            new GenericTop4(),
            new GenericTop5(),
            new GenericTop6(),
            new GenericTop7(),
            new MaleLowClass(),
            new MaleHighClass(),
            new MaleLightArmor(),
            new MaleHeavyArmor(),
            new FemaleTunic(),
            new FemaleDress(),
            new FemaleLightArmor(),
            new FemaleHeavyArmor(),
            new FemalePriestess(),
            new Natural(),
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
            new GenericBot5(),
            new Loincloth(),
        };
        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
    }

    ColorSwapPalette FurryColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, actor.Unit.AccessoryColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceHumanSkin, actor.Unit.SkinColor);
    }
    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        AddOffset(Belly, 0, -1 * .625f);
        if (!actor.Unit.HasBreasts)
        {
            //AddOffset(BodyAccent2, 2 * .625f, 0);
            //AddOffset(Weapon, 2 * .625f, 0);
        }
        if (actor.Unit.Furry)
        {
            AddOffset(Eyes, 0, 1 * .625f);
            AddOffset(SecondaryEyes, 0, 1 * .625f);
        }
        if (actor.Unit.Furry && Config.FurryGenitals)
        {
            AddOffset(Dick, 0, -2 * .625f);
            AddOffset(Balls, 0, -2 * .625f);
        }
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.EarType = State.Rand.Next(EarTypes);
        unit.TailType = State.Rand.Next(TailTypes);
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
        unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3);
        unit.BodyAccentType4 = State.Rand.Next(BodyAccentTypes4);
        unit.BodyAccentType5 = State.Rand.Next(BodyAccentTypes5);

        unit.ClothingExtraType1 = 0;

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
                unit.HairStyle = 12 + State.Rand.Next(13);
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

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts)
        {
            return Sprites[0 + actor.Unit.BodySize];
        }
        else
        {
            return Sprites[12 + actor.Unit.BodySize];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head - Skin
    {
        if (actor.Unit.Furry)
        {
            if (actor.IsEating)
            {
                return Sprites[23];
            }
            else
            {
                return Sprites[11];
            }
        }
        else
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.IsEating)
                {
                    return Sprites[9];
                }
                else
                {
                    return Sprites[8];
                }
            }
            else
            {
                if (actor.IsEating)
                {
                    return Sprites[21];
                }
                else
                {
                    return Sprites[20];
                }
            }
        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => Sprites[44 + actor.Unit.TailType]; //tail

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Right Arm
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return Sprites[6 + (!actor.Unit.HasBreasts ? 12 : 0)];
            return Sprites[4 + (!actor.Unit.HasBreasts ? 12 : 0)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites[6 + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 1:
                return Sprites[7 + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 2:
                return Sprites[5 + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 3:
                return Sprites[7 + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 4:
                return Sprites[6 + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 5:
                return Sprites[5 + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 6:
                return Sprites[6 + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 7:
                return Sprites[5 + (!actor.Unit.HasBreasts ? 12 : 0)];
            default:
                return Sprites[4 + (!actor.Unit.HasBreasts ? 12 : 0)];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Leg type
    {
        if (actor.Unit.Furry)
        {
            if (actor.Unit.BodySize > 2)
            {
                return Sprites[37];
            }
            else
            {
                return Sprites[36];
            }
        }
        else
        {
            return Sprites[24 + actor.Unit.BodySize];
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // feet Pattern
    {
        if (!actor.Unit.Furry || actor.Unit.BodyAccentType3 == 1 || actor.Unit.BodyAccentType3 >= 6) //Changed to >= to hopefully prevent a rare exception
        {
            return null;
        }
        if (actor.Unit.BodyAccentType3 == 4)
        {
            return Sprites[39];
        }
        else
        {
            return Sprites[38];
        }
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // hand Pattern
    {
        if (!actor.Unit.Furry || actor.Unit.BodyAccentType3 <= 3)
        {
            return null;
        }
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return Sprites[51 + (actor.Unit.BodyAccentType3 % 2 == 0 ? 4 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            return Sprites[48 + (actor.Unit.BodyAccentType3 % 2 == 0 ? 4 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites[50 + (actor.Unit.BodyAccentType3 % 2 == 0 ? 4 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 1:
                return Sprites[51 + (actor.Unit.BodyAccentType3 % 2 == 0 ? 4 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 2:
                return Sprites[49 + (actor.Unit.BodyAccentType3 % 2 == 0 ? 4 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 3:
                return Sprites[51 + (actor.Unit.BodyAccentType3 % 2 == 0 ? 4 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 4:
                return Sprites[50 + (actor.Unit.BodyAccentType3 % 2 == 0 ? 4 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 5:
                return Sprites[49 + (actor.Unit.BodyAccentType3 % 2 == 0 ? 4 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 6:
                return Sprites[50 + (actor.Unit.BodyAccentType3 % 2 == 0 ? 4 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            case 7:
                return Sprites[49 + (actor.Unit.BodyAccentType3 % 2 == 0 ? 4 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
            default:
                return Sprites[48 + (actor.Unit.BodyAccentType3 % 2 == 0 ? 4 : 0) + (!actor.Unit.HasBreasts ? 12 : 0)];
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Chest patern
    {
        if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
        {
            return null;
        }
        else
        {
            if (actor.Unit.HasBreasts)
            {
                return Sprites[28 + actor.Unit.BodySize];
            }
            else
            {
                return Sprites[40 + actor.Unit.BodySize];
            }
        }
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // thighs Pattern
    {
        if (actor.Unit.BodyAccentType2 != 2 || !actor.Unit.Furry)
        {
            return null;
        }
        return Sprites[56 + actor.Unit.BodySize + (!actor.Unit.HasBreasts ? 12 : 0)]; 
    } 

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // face patern
    {
        if (!actor.Unit.Furry || actor.Unit.BodyAccentType2 == 1)
        {
            return null;
        }
        else
        {
            if (actor.IsEating)
                return Sprites[34 + actor.Unit.BodyAccentType1];
            else
                return Sprites[32 + actor.Unit.BodyAccentType1];
        }
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // left ear
    {
        return Sprites[72 + (6 * actor.Unit.EarType) + actor.Unit.BodyAccentType4];
    }
    protected override Sprite BodyAccentSprite9(Actor_Unit actor) // right ear
    {
        return Sprites[75 + (6 * actor.Unit.EarType) + actor.Unit.BodyAccentType5];
    }
    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
            return null;
        else
            return SpriteDictionary.HumansBodySprites3[108 + actor.Unit.MouthType];
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle == 24)
        {
            return null;
        }
        else
        {
            return Sprites2[0 + actor.Unit.HairStyle];
        }
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle == 24)
        {
            return null;
        }
        else
        {
            return Sprites2[24 + actor.Unit.HairStyle];
        }
    }


protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            return Sprites2[69];
        }
        else
        {
            return Sprites2[48 + 4 * actor.Unit.EyeType + ((actor.IsAttacking || actor.IsEating) ? 0 : 2)];
        }
    }

    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            return null;
        }
        else
        {
            return Sprites2[49 + 4 * actor.Unit.EyeType + ((actor.IsAttacking || actor.IsEating) ? 0 : 2)];
        }
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
                if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                {
                    return Sprites8[105];
                }
                return Sprites9[105];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -29 * .625f);
                if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                {
                    return Sprites8[104];
                }
                return Sprites9[104];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -29 * .625f);
                if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                {
                    return Sprites8[103];
                }
                return Sprites9[103];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -29 * .625f);
                if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                {
                    return Sprites8[102];
                }
                return Sprites9[102];
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
            if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
            {
                return Sprites8[70 + size];
            }
            return Sprites9[70 + size];
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
            return Sprites[96 + actor.GetWeaponSprite()];
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
                if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                {
                    return Sprites8[31];
                }
                return Sprites9[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                {
                    return Sprites8[30];
                }
                return Sprites9[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                {
                    return Sprites8[29];
                }
                return Sprites9[29];
            }

            if (leftSize > 28)
                leftSize = 28;

            if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
            {
                return Sprites8[0 + leftSize];
            }
            return Sprites9[0 + leftSize];
        }
        else
        {
            if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
            {
                return Sprites8[0 + actor.Unit.BreastSize];
            }
            return Sprites9[0 + actor.Unit.BreastSize];
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
                if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                {
                    return Sprites8[63];
                }
                return Sprites9[63];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                {
                    return Sprites8[62];
                }
                return Sprites9[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                {
                    return Sprites8[61];
                }
                return Sprites9[61];
            }

            if (rightSize > 28)
                rightSize = 28;

            if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
            {
                return Sprites8[32 + rightSize];
            }
            return Sprites9[32 + rightSize];
        }
        else
        {
            if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
            {
                return Sprites8[32 + actor.Unit.BreastSize];
            }
            return Sprites9[32 + actor.Unit.BreastSize];
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
                        return Sprites7[54 + actor.Unit.DickSize];
                    }
                    else
                    {
                        return Sprites7[38 + actor.Unit.DickSize];
                    }
                }
                else
                {
                    Dick.layer = 13;
                    if (actor.IsCockVoring)
                    {
                        return Sprites7[62 + actor.Unit.DickSize];
                    }
                    else
                    {
                        return Sprites7[46 + actor.Unit.DickSize];
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
                    return Sprites7[86 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites7[70 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    return Sprites7[94 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites7[78 + actor.Unit.DickSize];
                }
            }
        }

        Dick.layer = 11;
        return Sprites7[78 + actor.Unit.DickSize];
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
            AddOffset(Balls, 0, -20 * .625f);
            if (actor.Unit.BodyAccentType2 == 1 && actor.Unit.Furry)
                return Sprites7[176];
            return Sprites7[139 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -20 * .625f);
            if (actor.Unit.BodyAccentType2 == 1 && actor.Unit.Furry)
                return Sprites7[175];
            return Sprites7[138 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -20 * .625f);
            if (actor.Unit.BodyAccentType2 == 1 && actor.Unit.Furry)
                return Sprites7[174];
            return Sprites7[137 - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)];
        }
        else if (offset >= 26)
        {
            AddOffset(Balls, 0, -20 * .625f);
        }
        else if (offset == 25)
        {
            AddOffset(Balls, 0, -12 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -10 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -9 * .625f);
        }
        else if (offset == 22)
        {
            AddOffset(Balls, 0, -10 * .625f);
        }
        else if (offset == 21)
        {
            AddOffset(Balls, 0, -7 * .625f);
        }
        else if (offset == 20)
        {
            AddOffset(Balls, 0, -6 * .625f);
        }
        else if (offset == 19)
        {
            AddOffset(Balls, 0, -4 * .625f);
        }
        else if (offset == 18)
        {
            AddOffset(Balls, 0, -1 * .625f);
        }
        if (actor.Unit.BodyAccentType2 == 1 && actor.Unit.Furry)
        {
            if (offset > 0)
                return Sprites7[Math.Min(147 + offset, 173)];
            return Sprites7[139 + size];
        }
        else
        {
            if (offset > 0)
                return Sprites7[Math.Min(110 + offset, 136) - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)];
            return Sprites7[102 + size - ((actor.Unit.Furry && Config.FurryGenitals) ? 102 : 0)];
        }        
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
            if (Races.Mice.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[56];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[48 + actor.Unit.BreastSize];
                clothing1.YOffset = -1 * .625f;
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
            if (Races.Mice.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[65];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[57 + actor.Unit.BreastSize];
                clothing1.YOffset = -1 * .625f;
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
            if (Races.Mice.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[74];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[66 + actor.Unit.BreastSize];
                clothing1.YOffset = -1 * .625f;
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
            clothing1.YOffset = -3 * .625f;
            clothing2.YOffset = -3 * .625f;

            if (Races.Mice.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[80];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[72 + actor.Unit.BreastSize];
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[81];
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
            clothing1.YOffset = -4 * .625f;
            clothing2.YOffset = -4 * .625f;

            if (Races.Mice.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[90];
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[99];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[82 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Sharks5[91 + actor.Unit.BreastSize];

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
            clothing1.YOffset = -3 * .625f;

            if (Races.Mice.oversize)
            {
                clothing1.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Sharks5[104 + actor.Unit.BreastSize];
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
            if (Races.Mice.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[95];
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[87 + actor.Unit.BreastSize];
                clothing1.YOffset = -1 * .625f;
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

    class MaleLowClass : MainClothing
    {
        public MaleLowClass()
        {
            DiscardSprite = SpriteDictionary.Avians4[79];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored); //shirt
            clothing2 = new SpriteExtraInfo(6, null, WhiteColored); //pants top
            clothing3 = new SpriteExtraInfo(6, null, WhiteColored); //pants bottom
            clothing4 = new SpriteExtraInfo(7, null, WhiteColored); //shoes
            clothing6 = new SpriteExtraInfo(17, null, WhiteColored); //right arm
            Type = 1579;
            DiscardUsesPalettes = true;
            SwapedClothing = 12;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.HasBelly)
            {
                if (actor.GetStomachSize(31, 0.7f) > 4)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[17];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 3)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[16];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 2)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[3 + (actor.Unit.BodySize * 4)];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 0)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[2 + (actor.Unit.BodySize * 4)];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[1 + (actor.Unit.BodySize * 4)];
                }
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[0 + (actor.Unit.BodySize * 4)];
            }
            clothing2.GetSprite = (s) => SpriteDictionary.MiceMBottoms[11 + (actor.Unit.BodySize)];
            if (actor.Unit.Furry)
            {
                clothing3.GetSprite = (s) => SpriteDictionary.MiceMBottoms[19 + (actor.Unit.BodySize > 2 ? 1: 0)];
                clothing4.GetSprite = (s) => SpriteDictionary.MiceMBottoms[29];
            }
            else
            {
                clothing3.GetSprite = (s) => SpriteDictionary.MiceMBottoms[15 + (actor.Unit.BodySize)];
                clothing4.GetSprite = (s) => SpriteDictionary.MiceMBottoms[21 + (actor.Unit.BodySize > 2 ? 1 : 0)];
            }
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[90];
                    break;
                case 1:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[92];
                    break;
                case 2:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[91];
                    break;
                case 3:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[92];
                    break;
                case 4:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[90];
                    break;
                case 5:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[91];
                    break;
                case 6:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[90];
                    break;
                case 7:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[91];
                    break;
                default:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[90];
                    break;
            }

            base.Configure(sprite, actor);
        }
    }

    class MaleHighClass : MainClothing
    {
        public MaleHighClass()
        {
            DiscardSprite = SpriteDictionary.Avians4[79];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);//shirt
            clothing2 = new SpriteExtraInfo(19, null, null);        //cloak color
            clothing3 = new SpriteExtraInfo(6, null, WhiteColored); //pants upper
            clothing4 = new SpriteExtraInfo(6, null, WhiteColored); //pants lower
            clothing5 = new SpriteExtraInfo(7, null, WhiteColored); //shoes
            clothing6 = new SpriteExtraInfo(17, null, WhiteColored); //right arm
            clothing7 = new SpriteExtraInfo(19, null, WhiteColored); //cloak brown
            Type = 1579;
            DiscardUsesPalettes = true;
            SwapedClothing = 13;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.HasBelly)
            {
                if (actor.GetStomachSize(31, 0.7f) > 4)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[35];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 3)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[34];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 2)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[21 + (actor.Unit.BodySize * 4)];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 0)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[20 + (actor.Unit.BodySize * 4)];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[19 + (actor.Unit.BodySize * 4)];
                }
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[18 + (actor.Unit.BodySize * 4)];
            }
            clothing2.GetSprite = (s) => SpriteDictionary.MiceMTops[96];
            clothing7.GetSprite = (s) => SpriteDictionary.MiceMTops[106];
            clothing3.GetSprite = (s) => SpriteDictionary.MiceMBottoms[11 + actor.Unit.BodySize];
            if (actor.Unit.Furry)
            {
                clothing4.GetSprite = (s) => SpriteDictionary.MiceMBottoms[19 + (actor.Unit.BodySize > 2 ? 1 : 0)];
                clothing5.GetSprite = (s) => SpriteDictionary.MiceMBottoms[30];
            }
            else
            {
                clothing4.GetSprite = (s) => SpriteDictionary.MiceMBottoms[15 + (actor.Unit.BodySize)];
                clothing5.GetSprite = (s) => SpriteDictionary.MiceMBottoms[33 + (actor.Unit.BodySize > 2 ? 1 : 0)];
            }

            switch (actor.GetWeaponSprite())
            {
                case 0:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[93];
                    break;
                case 1:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[94];
                    break;
                case 2:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[94];
                    break;
                case 3:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[94];
                    break;
                case 4:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[93];
                    break;
                case 5:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[94];
                    break;
                case 6:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[93];
                    break;
                case 7:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[94];
                    break;
                default:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[93];
                    break;
            }

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }
    class MaleLightArmor : MainClothing
    {
        public MaleLightArmor()
        {
            DiscardSprite = SpriteDictionary.Avians4[79];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = true;
            OccupiesAllSlots = true;
            clothing2 = new SpriteExtraInfo(6, null, WhiteColored); //bodysuit top
            clothing3 = new SpriteExtraInfo(6, null, WhiteColored); //bodysuit bottom
            clothing4 = new SpriteExtraInfo(7, null, WhiteColored); //shoes
            clothing6 = new SpriteExtraInfo(18, null, WhiteColored); //right arm 2
            Type = 1579;
            DiscardUsesPalettes = true;
            SwapedClothing = 14;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.HasBelly)
            {
                if (actor.GetStomachSize(31, 0.7f) > 4)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceMTops[89];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceMTops[88];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 2)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceMTops[75 + (actor.Unit.BodySize * 4)];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 0)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceMTops[74 + (actor.Unit.BodySize * 4)];
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceMTops[73 + (actor.Unit.BodySize * 4)];
                }
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.MiceMTops[72 + (actor.Unit.BodySize * 4)];
            }
            if (actor.Unit.Furry)
            {
                clothing3.GetSprite = (s) => SpriteDictionary.MiceMBottoms[27];
                clothing4.GetSprite = (s) => SpriteDictionary.MiceMBottoms[28];
            }
            else
            {
                clothing3.GetSprite = (s) => SpriteDictionary.MiceMBottoms[23 + actor.Unit.BodySize];
                clothing4.GetSprite = (s) => SpriteDictionary.MiceMBottoms[21 + (actor.Unit.BodySize > 2 ? 1 : 0)];
            }
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[103];
                    break;
                case 1:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[105]; 
                    break;
                case 2:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[104];
                    break;
                case 3:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[105];
                    break;
                case 4:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[103];
                    break;
                case 5:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[104];
                    break;
                case 6:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[103];
                    break;
                case 7:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[104];
                    break;
                default:
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[103];
                    break;
            }
            base.Configure(sprite, actor);
        }
    }

    class MaleHeavyArmor : MainClothing
    {
        public MaleHeavyArmor()
        {
            DiscardSprite = SpriteDictionary.Avians4[79];
            maleOnly = true;
            coversBreasts = false;
            blocksDick = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(18, null, null); //shirt
            clothing2 = new SpriteExtraInfo(6, null, WhiteColored); //pants top
            clothing3 = new SpriteExtraInfo(6, null, WhiteColored); //pants bottom
            clothing4 = new SpriteExtraInfo(7, null, WhiteColored); //shoes
            clothing6 = new SpriteExtraInfo(17, null, null); //right arm
            clothing7 = new SpriteExtraInfo(17, null, WhiteColored); //right arm armor
            clothing8 = new SpriteExtraInfo(17, null, WhiteColored); //armor
            clothing9 = new SpriteExtraInfo(7, null, WhiteColored); //armor bottom
            Type = 1579;
            DiscardUsesPalettes = true;
            SwapedClothing = 15;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
            clothing6.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
            if (actor.HasBelly)
            {
                if (actor.GetStomachSize(31, 0.7f) > 4)
                {
                    clothing8.GetSprite = (s) => SpriteDictionary.MiceMTops[53];
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[71];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 3)
                {
                    clothing8.GetSprite = (s) => SpriteDictionary.MiceMTops[52];
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[70];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 2)
                {
                    clothing8.GetSprite = (s) => SpriteDictionary.MiceMTops[39 + (actor.Unit.BodySize * 4)];
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[57 + (actor.Unit.BodySize * 4)];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 0)
                {
                    clothing8.GetSprite = (s) => SpriteDictionary.MiceMTops[38 + (actor.Unit.BodySize * 4)];
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[56 + (actor.Unit.BodySize * 4)];
                }
                else
                {
                    clothing8.GetSprite = (s) => SpriteDictionary.MiceMTops[37 + (actor.Unit.BodySize * 4)];
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[55 + (actor.Unit.BodySize * 4)];
                }
            }
            else
            {
                clothing8.GetSprite = (s) => SpriteDictionary.MiceMTops[36 + (actor.Unit.BodySize * 4)];
                clothing1.GetSprite = (s) => SpriteDictionary.MiceMTops[54 + (actor.Unit.BodySize * 4)];
            }
            clothing2.GetSprite = (s) => SpriteDictionary.MiceMBottoms[0 + (actor.Unit.BodySize)];
            if (actor.Unit.Furry)
            {
                clothing3.GetSprite = (s) => SpriteDictionary.MiceMBottoms[8 + (actor.Unit.BodySize > 2 ? 1 : 0)];
                clothing4.GetSprite = null;
            }
            else
            {
                clothing3.GetSprite = (s) => SpriteDictionary.MiceMBottoms[4 + (actor.Unit.BodySize)];
                clothing4.GetSprite = (s) => SpriteDictionary.MiceMBottoms[10];
            }
            clothing9.GetSprite = (s) => SpriteDictionary.MiceMBottoms[35 + (actor.Unit.BodySize)];
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    clothing7.GetSprite = (s) => SpriteDictionary.MiceMTops[97];
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[100];
                    break;
                case 1:
                    clothing7.GetSprite = (s) => SpriteDictionary.MiceMTops[99];
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[102];
                    break;
                case 2:
                    clothing7.GetSprite = (s) => SpriteDictionary.MiceMTops[98];
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[101];
                    break;
                case 3:
                    clothing7.GetSprite = (s) => SpriteDictionary.MiceMTops[99];
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[102];
                    break;
                case 4:
                    clothing7.GetSprite = (s) => SpriteDictionary.MiceMTops[97];
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[100];
                    break;
                case 5:
                    clothing7.GetSprite = (s) => SpriteDictionary.MiceMTops[98];
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[101];
                    break;
                case 6:
                    clothing7.GetSprite = (s) => SpriteDictionary.MiceMTops[97];
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[100];
                    break;
                case 7:
                    clothing7.GetSprite = (s) => SpriteDictionary.MiceMTops[98];
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[101];
                    break;
                default:
                    clothing7.GetSprite = (s) => SpriteDictionary.MiceMTops[97];
                    clothing6.GetSprite = (s) => SpriteDictionary.MiceMTops[100];
                    break;
            }

            base.Configure(sprite, actor);
        }
    }

    class FemaleTunic : MainClothing
    {
        public FemaleTunic()
        {
            DiscardSprite = SpriteDictionary.Avians4[79];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(16, null, WhiteColored); //belly changed
            clothing2 = new SpriteExtraInfo(19, null, WhiteColored); //breast changed
            clothing3 = new SpriteExtraInfo(7, null, WhiteColored); //right arm
            clothing4 = new SpriteExtraInfo(6, null, WhiteColored); //pants upper
            clothing5 = new SpriteExtraInfo(6, null, WhiteColored); //pants lower
            clothing6 = new SpriteExtraInfo(7, null, WhiteColored); //shoes
            clothing7 = new SpriteExtraInfo(15, null, null); //alt breast 1
            clothing8 = new SpriteExtraInfo(15, null, null); //alt breast 2
            clothing9 = new SpriteExtraInfo(7, null, WhiteColored); //right hand
            Type = 1579;
            DiscardUsesPalettes = false;
            SwapedClothing = 8;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing2.layer = 19;
            if (actor.HasBelly)
            {
                if (actor.GetStomachSize(31, 0.7f) > 4)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceFBottoms[17];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 3)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceFBottoms[16];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 2)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceFBottoms[3 + (actor.Unit.BodySize * 4)];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 0)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceFBottoms[2 + (actor.Unit.BodySize * 4)];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceFBottoms[1 + (actor.Unit.BodySize * 4)];
                }
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.MiceFBottoms[0 + (actor.Unit.BodySize * 4)];
            }
            if (Races.Mice.oversize)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.MiceFTops[8];
                blocksBreasts = false;
                clothing7.GetSprite = null;
                clothing8.GetSprite = null;
                clothing2.layer = 12;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing2.GetSprite = (s) => SpriteDictionary.MiceFTops[Math.Min(0 + actor.Unit.BreastSize, 7)];
                if (actor.Unit.BreastSize == 3)
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing7.GetSprite = (s) => SpriteDictionary.MiceVore2[64];
                        clothing8.GetSprite = (s) => SpriteDictionary.MiceVore2[67];
                    }
                    else
                    {
                        clothing7.GetSprite = (s) => SpriteDictionary.MiceVore3[64];
                        clothing8.GetSprite = (s) => SpriteDictionary.MiceVore3[67];
                    }
                    
                }
                else if (actor.Unit.BreastSize == 4)
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing7.GetSprite = (s) => SpriteDictionary.MiceVore2[65];
                        clothing8.GetSprite = (s) => SpriteDictionary.MiceVore2[68];
                    }
                    else
                    {
                        clothing7.GetSprite = (s) => SpriteDictionary.MiceVore3[65];
                        clothing8.GetSprite = (s) => SpriteDictionary.MiceVore3[68];
                    }
                    
                }
                else if (actor.Unit.BreastSize == 5)
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing7.GetSprite = (s) => SpriteDictionary.MiceVore2[66];
                        clothing8.GetSprite = (s) => SpriteDictionary.MiceVore2[69];
                    }
                    else
                    {
                        clothing7.GetSprite = (s) => SpriteDictionary.MiceVore3[66];
                        clothing8.GetSprite = (s) => SpriteDictionary.MiceVore3[69];
                    }
                    
                }
                else
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing7.GetSprite = (s) => SpriteDictionary.MiceVore2[0 + actor.Unit.BreastSize];
                        clothing8.GetSprite = (s) => SpriteDictionary.MiceVore2[32 + actor.Unit.BreastSize];
                    }
                    else
                    {
                        clothing7.GetSprite = (s) => SpriteDictionary.MiceVore3[0 + actor.Unit.BreastSize];
                        clothing8.GetSprite = (s) => SpriteDictionary.MiceVore3[32 + actor.Unit.BreastSize];
                    }
                    
                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
            }
            clothing4.GetSprite = (s) => SpriteDictionary.MiceFBottoms[96 + actor.Unit.BodySize];
            if (actor.Unit.Furry)
            {
                clothing5.GetSprite = (s) => SpriteDictionary.MiceFBottoms[104 + (actor.Unit.BodySize > 2 ? 1 : 0)];
            }
            else
            {
                clothing5.GetSprite = (s) => SpriteDictionary.MiceFBottoms[100 + actor.Unit.BodySize];
            }

            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[10];

                }
                else
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[Math.Min(12 + actor.Unit.BreastSize,21)];
                    clothing9.GetSprite = (s) => SpriteDictionary.MiceFTops[21];
                }
            }

            switch (actor.GetWeaponSprite())
            {
                case 0:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[Math.Min(12 + actor.Unit.BreastSize, 21)];
                    clothing9.GetSprite = (s) => SpriteDictionary.MiceFTops[22];
                    break;
                case 1:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[10];
                    break;
                case 2:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[9];
                    break;
                case 3:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[10];
                    break;
                case 4:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[Math.Min(12 + actor.Unit.BreastSize, 21)];
                    clothing9.GetSprite = (s) => SpriteDictionary.MiceFTops[22]; 
                    break;
                case 5:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[9];
                    break;
                case 6:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[Math.Min(12 + actor.Unit.BreastSize, 21)];
                    clothing9.GetSprite = (s) => SpriteDictionary.MiceFTops[22]; 
                    break;
                case 7:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[9];
                    break;
                default:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[Math.Min(12 + actor.Unit.BreastSize, 21)];
                    clothing9.GetSprite = (s) => SpriteDictionary.MiceFTops[22];
                    break;
            }
            if (actor.Unit.Furry)
            {
                clothing6.GetSprite = null;
                clothing7.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, actor.Unit.AccessoryColor);
                clothing8.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, actor.Unit.AccessoryColor);
            }
            else
            {
                clothing6.GetSprite = (s) => SpriteDictionary.MiceMBottoms[21 + (actor.Unit.BodySize > 2 ? 1 : 0)];
                clothing7.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceHumanSkin, actor.Unit.SkinColor);
                clothing8.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceHumanSkin, actor.Unit.SkinColor);
            }

            base.Configure(sprite, actor);
        }
    }

    class FemaleDress : MainClothing
    {
        public FemaleDress()
        {
            DiscardSprite = SpriteDictionary.Avians4[79];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(15, null, WhiteColored); //belly changed
            clothing2 = new SpriteExtraInfo(19, null, WhiteColored); //breast changed
            clothing3 = new SpriteExtraInfo(16, null, WhiteColored); //right arm
            clothing4 = new SpriteExtraInfo(6, null, WhiteColored); //shoes
            Type = 1579;
            DiscardUsesPalettes = false;
            SwapedClothing = 9;

        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.HasBelly)
            {
                if (actor.GetStomachSize(31, 0.7f) > 4)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceFBottoms[35];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 3)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceFBottoms[34];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 2)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceFBottoms[21 + (actor.Unit.BodySize * 4)];
                }
                else if (actor.GetStomachSize(31, 0.7f) > 0)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceFBottoms[20 + (actor.Unit.BodySize * 4)];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.MiceFBottoms[19 + (actor.Unit.BodySize * 4)];
                }
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.MiceFBottoms[18 + (actor.Unit.BodySize * 4)];
            }          
            if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing2.GetSprite = (s) => SpriteDictionary.MiceFTops[24 + (actor.Unit.BreastSize > 2 ? actor.Unit.BreastSize - 1 : 0)];
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }
            
            if (actor.Unit.Furry)
            {
                clothing4.GetSprite = null;
            }
            else
            {
                clothing4.GetSprite = (s) => SpriteDictionary.MiceFBottoms[95];
            }

            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[34];

                }
                else
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[31];
                }
            }

            switch (actor.GetWeaponSprite())
            {
                case 0:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[33]; // grip down
                    break;
                case 1:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[34]; // arm up
                    break;
                case 2:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[32]; // grip up
                    break;
                case 3:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[34]; // arm up
                    break;
                case 4:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[33]; // grip down
                    break;
                case 5:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[32]; // grip up
                    break;
                case 6:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[33]; // grip down
                    break;
                case 7:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[32]; // grip up
                    break;
                default:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[31]; // arm down
                    break;
            }
            if (Races.Mice.oversize)
            {
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
                blocksBreasts = false;
            }
            base.Configure(sprite, actor);
        }
    }

    class FemaleLightArmor : MainClothing
    {
        public FemaleLightArmor()
        {
            DiscardSprite = SpriteDictionary.Avians4[79];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(19, null, WhiteColored); //breast changed
            clothing2 = new SpriteExtraInfo(10, null, WhiteColored); //arms
            clothing3 = new SpriteExtraInfo(5, null, WhiteColored); //pants
            clothing4 = new SpriteExtraInfo(6, null, WhiteColored); //shoes
            clothing5 = new SpriteExtraInfo(15, null, null); //alt breast 1
            clothing6 = new SpriteExtraInfo(15, null, null); //alt breast 2
            clothing7 = new SpriteExtraInfo(7, null, WhiteColored); //belt
            clothing8 = new SpriteExtraInfo(7, null, WhiteColored); //pants lower
            clothing9 = new SpriteExtraInfo(6, null, WhiteColored); //waist armor
            Type = 1579;
            DiscardUsesPalettes = false;
            SwapedClothing = 10;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (Races.Mice.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.MiceFTops[44];
                blocksBreasts = false;
                clothing5.GetSprite = null;
                clothing6.GetSprite = null;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.MiceFTops[36 + actor.Unit.BreastSize + (actor.Unit.BodySize == 3  && actor.Unit.BreastSize <= 2 ? 12 : 0)];
                if (actor.Unit.BreastSize == 3)
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore2[64];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore2[67];
                    }
                    else
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore3[64];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore3[67];
                    }

                }
                else if (actor.Unit.BreastSize == 4)
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore2[65];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore2[68];
                    }
                    else
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore3[65];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore3[68];
                    }

                }
                else if (actor.Unit.BreastSize == 5)
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore2[66];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore2[69];
                    }
                    else
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore3[66];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore3[69];
                    }

                }
                else
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore2[0 + actor.Unit.BreastSize];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore2[32 + actor.Unit.BreastSize];
                    }
                    else
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore3[0 + actor.Unit.BreastSize];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore3[32 + actor.Unit.BreastSize];
                    }

                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing5.GetSprite = null;
                clothing6.GetSprite = null;
            }
            if (actor.HasBelly)
            {
                clothing7.GetSprite = (s) => SpriteDictionary.MiceFBottoms[94];
            }
            else
            {
                clothing7.GetSprite = (s) => SpriteDictionary.MiceFBottoms[90 + actor.Unit.BodySize];
            }
            clothing3.GetSprite = (s) => SpriteDictionary.MiceFBottoms[96 + actor.Unit.BodySize];
            clothing9.GetSprite = (s) => SpriteDictionary.MiceFBottoms[86 + actor.Unit.BodySize];
            if (actor.Unit.Furry)
            {
                clothing4.GetSprite = null;
                clothing8.GetSprite = (s) => SpriteDictionary.MiceFBottoms[104 + (actor.Unit.BodySize > 2 ? 1 : 0)];
            }
            else
            {
                clothing4.GetSprite = (s) => SpriteDictionary.MiceMBottoms[31];
                clothing8.GetSprite = (s) => SpriteDictionary.MiceFBottoms[100 + actor.Unit.BodySize];
            }

            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[54];

                }
                else
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[51];
                }
            }

            switch (actor.GetWeaponSprite())
            {
                case 0:
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceFTops[53]; // grip down
                    break;
                case 1:
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceFTops[54]; // arm up
                    break;
                case 2:
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceFTops[52]; // grip up
                    break;
                case 3:
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceFTops[54]; // arm up
                    break;
                case 4:
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceFTops[53]; // grip down
                    break;
                case 5:
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceFTops[52]; // grip up
                    break;
                case 6:
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceFTops[53]; // grip down
                    break;
                case 7:
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceFTops[52]; // grip up
                    break;
                default:
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceFTops[51]; // arm down
                    break;
            }
            if (actor.Unit.Furry)
            {
                clothing5.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, actor.Unit.AccessoryColor);
                clothing6.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, actor.Unit.AccessoryColor);
            }
            else
            {
                clothing5.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceHumanSkin, actor.Unit.SkinColor);
                clothing6.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceHumanSkin, actor.Unit.SkinColor);
            }
            base.Configure(sprite, actor);
        }
    }

    class FemaleHeavyArmor : MainClothing
    {
        public FemaleHeavyArmor()
        {
            DiscardSprite = SpriteDictionary.Avians4[79];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(19, null, WhiteColored); //breast changed
            clothing2 = new SpriteExtraInfo(15, null, WhiteColored); //belly changed
            clothing3 = new SpriteExtraInfo(10, null, WhiteColored); //arms
            clothing4 = new SpriteExtraInfo(13, null, WhiteColored); //pants
            clothing5 = new SpriteExtraInfo(16, null, null); //alt breast 1
            clothing6 = new SpriteExtraInfo(16, null, null); //alt breast 2
            clothing7 = new SpriteExtraInfo(6, null, WhiteColored); //underarmor top
            clothing8 = new SpriteExtraInfo(5, null, WhiteColored); //underarmor bottom
            clothing9 = new SpriteExtraInfo(6, null, WhiteColored); //shoes
            Type = 1579;
            DiscardUsesPalettes = false;
            SwapedClothing = 11;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing8.GetSprite = (s) => SpriteDictionary.MiceFBottoms[58 + Math.Min(actor.GetStomachSize(31, 0.7f), 66)];
            if (actor.HasBelly)
            {
                if (actor.GetStomachSize(31, 0.7f) > 4)
                {
                    clothing2.GetSprite = null;
                    clothing8.layer = 15;
                }
                else if (actor.GetStomachSize(31, 0.7f) > 3)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceFBottoms[52];
                    clothing8.layer = 15;
                }
                else if (actor.GetStomachSize(31, 0.7f) > 2)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceFBottoms[39 + (actor.Unit.BodySize * 4)];
                    clothing8.layer = 15;
                }
                else if (actor.GetStomachSize(31, 0.7f) > 0)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceFBottoms[38 + (actor.Unit.BodySize * 4)];
                    clothing8.layer = 15;
                }
                else
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.MiceFBottoms[37 + (actor.Unit.BodySize * 4)];
                }
            }
            else
            {
                clothing8.layer = 5;
                clothing2.GetSprite = (s) => SpriteDictionary.MiceFBottoms[36 + (actor.Unit.BodySize * 4)];
            }
            if (Races.Mice.oversize)
            {
                clothing1.GetSprite = null;
                clothing7.GetSprite = (s) => SpriteDictionary.MiceFTops[66];
                blocksBreasts = false;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.MiceFTops[72 + (actor.Unit.BreastSize > 2 ? actor.Unit.BreastSize - 1 : 0)];
                clothing7.GetSprite = (s) => SpriteDictionary.MiceFTops[60 + (actor.Unit.BreastSize > 2 ? actor.Unit.BreastSize - 1 : 0)];
                if (actor.Unit.BreastSize == 3)
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore2[64];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore2[67];
                    }
                    else
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore3[64];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore3[67];
                    }

                }
                else if (actor.Unit.BreastSize == 4)
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore2[65];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore2[68];
                    }
                    else
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore3[65];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore3[68];
                    }

                }
                else if (actor.Unit.BreastSize == 5)
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore2[66];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore2[69];
                    }
                    else
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore3[66];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore3[69];
                    }

                }
                else
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore2[0 + actor.Unit.BreastSize];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore2[32 + actor.Unit.BreastSize];
                    }
                    else
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore3[0 + actor.Unit.BreastSize];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore3[32 + actor.Unit.BreastSize];
                    }

                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }
            clothing4.GetSprite = (s) => SpriteDictionary.MiceFBottoms[54 + actor.Unit.BodySize];
            if (actor.Unit.Furry)
            {
                clothing9.GetSprite = null;
            }
            else
            {
                clothing9.GetSprite = (s) => SpriteDictionary.MiceFBottoms[53];
            }

            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[70];

                }
                else
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[67];
                }
            }

            switch (actor.GetWeaponSprite())
            {
                case 0:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[69]; // grip down
                    break;
                case 1:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[70]; // arm up
                    break;
                case 2:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[68]; // grip up
                    break;
                case 3:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[70]; // arm up
                    break;
                case 4:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[69]; // grip down
                    break;
                case 5:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[68]; // grip up
                    break;
                case 6:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[69]; // grip down
                    break;
                case 7:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[68]; // grip up
                    break;
                default:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[67]; // arm down
                    break;
            }
            if (actor.Unit.Furry)
            {
                clothing5.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, actor.Unit.AccessoryColor);
                clothing6.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, actor.Unit.AccessoryColor);
            }
            else
            {
                clothing5.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceHumanSkin, actor.Unit.SkinColor);
                clothing6.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceHumanSkin, actor.Unit.SkinColor);
            }
            base.Configure(sprite, actor);
        }
    }

    class FemalePriestess : MainClothing
    {
        public FemalePriestess()
        {
            DiscardSprite = SpriteDictionary.Avians4[79];
            femaleOnly = true;
            coversBreasts = false;
            blocksDick = true;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(19, null, WhiteColored); //breast changed
            clothing2 = new SpriteExtraInfo(16, null, WhiteColored); //belly changed
            clothing3 = new SpriteExtraInfo(10, null, WhiteColored); //arms
            clothing4 = new SpriteExtraInfo(6, null, WhiteColored); //shoes
            clothing5 = new SpriteExtraInfo(16, null, null); //alt breast 1
            clothing6 = new SpriteExtraInfo(16, null, null); //alt breast 2
            clothing7 = new SpriteExtraInfo(17, null, null); //breast changed 2
            clothing8 = new SpriteExtraInfo(13, null, null); //skirt
            Type = 1579;
            DiscardUsesPalettes = false;
            SwapedClothing = 0;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            clothing7.layer = 17;
            if (actor.HasBelly)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.MiceFBottoms[72 + Math.Min(actor.GetStomachSize(31, 0.7f), 11)];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.MiceFBottoms[72 + actor.Unit.BodySize];
            }
            if (Races.Mice.oversize)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.MiceFTops[92];
                clothing7.GetSprite = null;
                blocksBreasts = false;
                clothing7.layer = 15;
            }
            else if (actor.Unit.HasBreasts)
            {
                blocksBreasts = true;
                clothing1.GetSprite = (s) => SpriteDictionary.MiceFTops[84 + actor.Unit.BreastSize];
                clothing7.GetSprite = (s) => SpriteDictionary.MiceFTops[96 + actor.Unit.BreastSize];
                if (actor.Unit.BreastSize == 3)
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore2[64];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore2[67];
                    }
                    else
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore3[64];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore3[67];
                    }

                }
                else if (actor.Unit.BreastSize == 4)
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore2[65];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore2[68];
                    }
                    else
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore3[65];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore3[68];
                    }

                }
                else if (actor.Unit.BreastSize == 5)
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore2[66];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore2[69];
                    }
                    else
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore3[66];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore3[69];
                    }

                }
                else
                {
                    if (actor.Unit.BodyAccentType2 == 1 || !actor.Unit.Furry)
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore2[0 + actor.Unit.BreastSize];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore2[32 + actor.Unit.BreastSize];
                    }
                    else
                    {
                        clothing5.GetSprite = (s) => SpriteDictionary.MiceVore3[0 + actor.Unit.BreastSize];
                        clothing6.GetSprite = (s) => SpriteDictionary.MiceVore3[32 + actor.Unit.BreastSize];
                    }

                }
            }
            else
            {
                blocksBreasts = true;
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }
            if (actor.Unit.Furry)
            {
                clothing4.GetSprite = (s) => SpriteDictionary.MiceFBottoms[67];
            }
            else
            {
                clothing4.GetSprite = (s) => SpriteDictionary.MiceFBottoms[84 + (actor.Unit.BodySize > 2 ? 1 : 0)];
            }

            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking)
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[111];

                }
                else
                {
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[108];
                }
            }
            clothing8.GetSprite = (s) => SpriteDictionary.MiceFBottoms[68 + actor.Unit.BodySize];
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[110]; // grip down
                    break;
                case 1:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[111]; // arm up
                    break;
                case 2:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[109]; // grip up
                    break;
                case 3:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[111]; // arm up
                    break;
                case 4:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[110]; // grip down
                    break;
                case 5:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[109]; // grip up
                    break;
                case 6:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[110]; // grip down
                    break;
                case 7:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[109]; // grip up
                    break;
                default:
                    clothing3.GetSprite = (s) => SpriteDictionary.MiceFTops[108]; // arm down
                    break;
            }
            if (actor.Unit.Furry)
            {
                clothing5.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, actor.Unit.AccessoryColor);
                clothing6.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, actor.Unit.AccessoryColor);
            }
            else
            {
                clothing5.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceHumanSkin, actor.Unit.SkinColor);
                clothing6.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceHumanSkin, actor.Unit.SkinColor);
            }
            clothing7.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
            clothing8.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, actor.Unit.ClothingColor);
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
            if (Races.Mice.oversize)
            {
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[0];
                clothing2.YOffset = 0;
            }
            else if (actor.Unit.HasBreasts)
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Deer4[2 + actor.Unit.BreastSize];
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[0];
                clothing2.YOffset = 0;
                clothing1.YOffset = -1;
            }
            else
            {
                breastSprite = null;
                clothing1.GetSprite = null;
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[1];
            }
            if (!actor.Unit.Furry)
            {
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }

            if (actor.Unit.BodyAccentType2 == 1)
            {
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, actor.Unit.AccessoryColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, actor.Unit.AccessoryColor);
            }
            else
            {
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, actor.Unit.AccessoryColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MiceSkin, actor.Unit.AccessoryColor);
            }
            

            base.Configure(sprite, actor);
        }
    }

   
    class MiceRags : MainClothing
    {
        public MiceRags()
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
            clothing3 = new SpriteExtraInfo(11, null, WhiteColored);
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BreastSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Mice1[113];
                else if (actor.Unit.BreastSize < 6)
                    clothing1.GetSprite = (s) => SpriteDictionary.Mice1[114];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Mice1[115];

                clothing2.GetSprite = (s) => SpriteDictionary.Mice1[104 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => SpriteDictionary.Mice1[112];
                clothing2.GetSprite = (s) => SpriteDictionary.Mice1[108 + actor.Unit.BodySize];
            }
            clothing3.GetSprite = (s) => SpriteDictionary.Mice1[116 + (actor.Unit.Furry ? 1 : 0)];

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
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[20];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[22];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[21];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[12 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[16 + actor.Unit.BodySize];
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
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[32];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[34];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[33];
            }
            else clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[31];

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[20 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[24 + actor.Unit.BodySize];
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
            clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[35];

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[20 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[24 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class GenericBot4 : MainClothing
    {
        public GenericBot4()
        {
            DiscardSprite = SpriteDictionary.Cockatrice3[47];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(13, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, WhiteColored);
            Type = 61602;
            FixedColor = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {

            if (actor.Unit.DickSize > 0)
            {
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[44];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[46];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Cockatrice3[45];
            }
            else clothing1.GetSprite = null;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[28 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[32 + actor.Unit.BodySize];
            }
            base.Configure(sprite, actor);
        }
    }

    class GenericBot5 : MainClothing
    {
        public GenericBot5()
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
                clothing1.YOffset = -1 * .625f;
                if (actor.Unit.DickSize < 3)
                    clothing1.GetSprite = (s) => SpriteDictionary.Deer4[44];
                else if (actor.Unit.DickSize > 5)
                    clothing1.GetSprite = (s) => SpriteDictionary.Deer4[46];
                else
                    clothing1.GetSprite = (s) => SpriteDictionary.Deer4[45];
            }
            else clothing1.GetSprite = null;
            clothing1.YOffset = 0;

            if (actor.Unit.HasBreasts)
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[36 + actor.Unit.BodySize];
            }
            else
            {
                clothing2.GetSprite = (s) => SpriteDictionary.Deer4[40 + actor.Unit.BodySize];
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            base.Configure(sprite, actor);
        }
    }

    class Loincloth : MainClothing
    {
        public Loincloth()
        {
            DiscardSprite = SpriteDictionary.DeerLeaderClothes[66];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(12, null, null);
            Type = 61705;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[1 + 2 * actor.Unit.BodySize];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[0 + 2 * actor.Unit.BodySize];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[9 + 2 * actor.Unit.BodySize];
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[8 + 2 * actor.Unit.BodySize];
                }
            }

            clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            base.Configure(sprite, actor);
        }
    }

    class DeerLeader3 : MainClothing
    {
        public DeerLeader3()
        {
            leaderOnly = true;
            DiscardSprite = SpriteDictionary.DeerLeaderClothes[48];
            coversBreasts = false;
            clothing1 = new SpriteExtraInfo(15, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(12, null, null);
            clothing3 = new SpriteExtraInfo(15, null, null);
            Type = 61704;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.HasBelly)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[1 + 2 * actor.Unit.BodySize];

                    if (actor.GetStomachSize(31, 0.7f) < 4)
                    {
                        clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[17 + 2 * actor.Unit.BodySize];
                        clothing3.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[33 + 2 * actor.Unit.BodySize];
                    }
                    else
                    {
                        clothing1.GetSprite = null;
                        clothing3.GetSprite = null;
                    }
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[16 + 2 * actor.Unit.BodySize];
                    clothing2.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[0 + 2 * actor.Unit.BodySize];
                    clothing3.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[32 + 2 * actor.Unit.BodySize];
                }
            }
            else
            {
                if (actor.HasBelly)
                {
                    clothing2.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[9 + 2 * actor.Unit.BodySize];

                    if (actor.GetStomachSize(31, 0.7f) < 4)
                    {
                        clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[25 + 2 * actor.Unit.BodySize];
                        clothing3.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[41 + 2 * actor.Unit.BodySize];
                    }
                    else
                    {
                        clothing1.GetSprite = null;
                        clothing3.GetSprite = null;
                    }
                }
                else
                {
                    clothing1.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[24 + 2 * actor.Unit.BodySize];
                    clothing2.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[8 + 2 * actor.Unit.BodySize];
                    clothing3.GetSprite = (s) => SpriteDictionary.DeerLeaderClothes[40 + 2 * actor.Unit.BodySize];
                }
            }

            clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            clothing3.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerLeaf, actor.Unit.AccessoryColor);

            base.Configure(sprite, actor);
        }
    }

}
