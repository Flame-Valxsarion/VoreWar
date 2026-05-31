using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

class DemiDragons : DefaultRaceData
{
    readonly Sprite[] SpritesBelliesBalls = State.GameManager.SpriteDictionary.DemidragonBelliesBalls;
    readonly Sprite[] SpritesBodies1 = State.GameManager.SpriteDictionary.DemidragonBodies;
    readonly Sprite[] SpritesBodies2 = State.GameManager.SpriteDictionary.DemidragonBodies2;
    readonly Sprite[] SpritesBreasts = State.GameManager.SpriteDictionary.DemidragonBreasts;
    readonly Sprite[] SpritesClothes = State.GameManager.SpriteDictionary.DemidragonClothes;
    readonly Sprite[] SpritesClothes2 = State.GameManager.SpriteDictionary.DemidragonClothes2;
    readonly Sprite[] SpritesClothes3 = State.GameManager.SpriteDictionary.DemidragonClothes3;
    readonly Sprite[] SpritesCustomisation1 = State.GameManager.SpriteDictionary.DemidragonCustomisation1;
    readonly Sprite[] SpritesCustomisation2 = State.GameManager.SpriteDictionary.DemidragonCustomisation2;
    readonly Sprite[] SpritesCustomisation3 = State.GameManager.SpriteDictionary.DemidragonCustomisation3;
    readonly Sprite[] SpritesPatterns1 = State.GameManager.SpriteDictionary.DemidragonPatterns;
    readonly Sprite[] SpritesPatterns2 = State.GameManager.SpriteDictionary.DemidragonPatterns2;
    readonly DemidragonLeader LeaderClothes;
    readonly DemidragonRags Rags;

    bool oversize = false;

    public DemiDragons()
    {
        BodySizes = 3;
        EyeTypes = 8;
        EarTypes = 21;
        SpecialAccessoryCount = 7; // Demi body patterns
        BodyAccentTypes1 = 10; // Wings
        BodyAccentTypes2 = 20; // Horns
        BodyAccentTypes3 = 33; // Anthro Hair
        BodyAccentTypes4 = 8; // Anthro Eyes
        BasicMeleeWeaponTypes = 2; // Weapon Types
        AdvancedMeleeWeaponTypes = 2; // Weapon Types
        FurTypes = 2; // Segmetation 
        TailTypes = 10; // Tails       
        HairStyles = 36;
        MouthTypes = 12;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemidragonSkin); // Outer Skin Color
        ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemidragonSkin); // Inner Skin Color
        ExtraColors2 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemidragonSkin); // Claw/Horn Color
        ExtraColors3 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemidragonSkin); // Internal Color
        ExtraColors4 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemidragonSkin); // Pattern Color
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.RedSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.RaijuSkin);
        BodyAccentTypes5 = 6; // eyebrows

        ExtendedBreastSprites = true;
        FurCapable = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => FurryColorInner(s)); // Body/Inner body
        Head = new SpriteExtraInfo(20, HeadSprite, null, (s) => FurryColorOuter(s)); // Head/Outer Head
        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.AccessoryColor)); // Outer Body
        BodyAccent = new SpriteExtraInfo(5, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.ExtraColor2)); // Claws
        BodyAccent2 = new SpriteExtraInfo(4, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.AccessoryColor)); // Teeth/Nose
        BodyAccent3 = new SpriteExtraInfo(6, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.ExtraColor4)); // Body Pattern
        BodyAccent4 = new SpriteExtraInfo(22, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.ExtraColor4)); // Head Patten
        BodyAccent5 = new SpriteExtraInfo(25, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.AccessoryColor)); // Ear Inner
        BodyAccent6 = new SpriteExtraInfo(24, BodyAccentSprite6, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.AccessoryColor)); // Ear Outer
        BodyAccent7 = new SpriteExtraInfo(23, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.ExtraColor2)); // Lower Horns
        BodyAccent8 = new SpriteExtraInfo(27, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.ExtraColor2)); // Upper Horns
        BodyAccent9 = new SpriteExtraInfo(21, BodyAccentSprite9, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.ExtraColor1)); // Inner Head
        BodyAccent10 = new SpriteExtraInfo(2, BodyAccentSprite10, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.AccessoryColor)); // Outer wings
        BodyAccent11 = new SpriteExtraInfo(2, BodyAccentSprite11, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.AccessoryColor)); // Inner wings
        BodyAccent12 = new SpriteExtraInfo(2, BodyAccentSprite12, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.ExtraColor2)); // Claw wings
        BodyAccent13 = new SpriteExtraInfo(1, BodyAccentSprite13, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.AccessoryColor)); // Outer Tail
        BodyAccent14 = new SpriteExtraInfo(1, BodyAccentSprite14, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.AccessoryColor)); // Inner Tail
        BodyAccent15 = new SpriteExtraInfo(17, BodyAccentSprite15, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.ExtraColor3)); // left nipple
        Beard = new SpriteExtraInfo(17, BeardSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.ExtraColor3)); // Right Nipple
        Mouth = new SpriteExtraInfo(21, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(26, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(3, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); // Back hair
        Hair3 = new SpriteExtraInfo(9, HairSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); // Eyebrows
        Eyes = new SpriteExtraInfo(21, EyesSprite, WhiteColored);
        SecondaryEyes = new SpriteExtraInfo(22, EyesSecondarySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RaijuSkin, s.Unit.EyeColor));
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(14, null, null, (s) => FurryColorInner(s));
        Weapon = new SpriteExtraInfo(6, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => FurryColorInner(s));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => FurryColorInner(s));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, null, (s) => FurryColorInner(s, true));
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => FurryColorInner(s, true));
        Pussy = new SpriteExtraInfo(4, PussySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, s.Unit.ExtraColor3));


        LeaderClothes = new DemidragonLeader();
        Rags = new DemidragonRags();

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
            new GenericBot5(),
        };

        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
    }

    static ColorSwapPalette FurryColorInner(Actor_Unit actor, bool bits = false)
    {
        if (bits && actor.Unit.Furry)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, actor.Unit.ExtraColor3);
        if (actor.Unit.Furry)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, actor.Unit.ExtraColor1);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedSkin, actor.Unit.SkinColor);
    }

    static ColorSwapPalette FurryColorOuter(Actor_Unit actor, bool bits = false)
    {
        if (actor.Unit.Furry)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, actor.Unit.AccessoryColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedSkin, actor.Unit.SkinColor);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts)
        {
            if (actor.Unit.BodySize > 1)
            {
                AddOffset(Balls, 0, 3 * .625f);
                AddOffset(Belly, 0, 1 * .625f);
            }
            else
            {
                AddOffset(Balls, 0, 3 * .625f);
                AddOffset(Belly, 0, 1 * .625f);
            }
        }
        else
        {
            if (actor.Unit.BodySize > 1)
            {
                AddOffset(Balls, 0, 1 * .625f);
                AddOffset(Belly, 0, 1 * .625f);
            }
            else
            {
                AddOffset(Balls, 0, 0);
                AddOffset(Belly, 0, 1 * .625f);
            }
        }
        if (actor.Unit.Furry)
        {
            if (!actor.Unit.HasBreasts) 
            {
                AddOffset(Weapon, 2 * .625f, 0 * .625f);
            }
            if (actor.Unit.BodySize >= 2)
            {
                AddOffset(Weapon, 1 * .625f, 0 * .625f);
            }

            if (actor.GetWeaponSprite() == 2)
            {
                if (actor.Unit.AdvancedMeleeWeaponType == 1)
                {
                    AddOffset(Weapon, 10 * .625f, 0 * .625f);
                }
            }

            if (actor.GetWeaponSprite() == 3)
            {
                if (actor.Unit.AdvancedMeleeWeaponType == 0)
                {
                    AddOffset(Weapon, 0 * .625f, 19 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 0 * .625f, 10 * .625f);
                }
            }
        }
        else
        {
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    if (actor.Unit.HasBreasts)
                    {
                        if (actor.Unit.BodySize < 2)
                        {
                            AddOffset(Weapon, 1 * .625f, 0 * .625f);
                        }
                        else
                        {
                            AddOffset(Weapon, 1* .625f, 0 * .625f);
                        }
                    }
                    else
                    {
                        if (actor.Unit.BodySize < 2)                        
                            AddOffset(Weapon, 0 * .625f, -2 * .625f);                       
                        else
                            AddOffset(Weapon, 1 * .625f, -2 * .625f);
                    }
                    break;
                case 1:
                    if (actor.Unit.HasBreasts)
                    {
                        if (actor.Unit.BodySize < 2)
                            AddOffset(Weapon, 1 * .625f, -1 * .625f);
                    }
                    else
                    {
                        if (actor.Unit.BodySize < 2)
                            AddOffset(Weapon, 3 * .625f, -4 * .625f);
                        else
                            AddOffset(Weapon, 3 * .625f, -3 * .625f);
                    }
                    break;
                case 2:
                    if (actor.Unit.AdvancedMeleeWeaponType == 1)
                    {
                        if (actor.Unit.HasBreasts)
                        {
                            if (actor.Unit.BodySize < 2)
                                AddOffset(Weapon, 1 * .625f, 0 * .625f);

                        }
                        else
                        {
                            if (actor.Unit.BodySize < 2)
                                AddOffset(Weapon, 0 * .625f, -2 * .625f);
                            else
                                AddOffset(Weapon, 1 * .625f, -2 * .625f);
                        }
                        break;
                    }
                    else
                    {
                        if (actor.Unit.HasBreasts)
                        {
                            if (actor.Unit.BodySize >= 2)
                                AddOffset(Weapon, 1 * .625f, -1 * .625f);
                        }
                        else
                        {
                            if (actor.Unit.BodySize < 2)
                                AddOffset(Weapon, 0 * .625f, -2 * .625f);
                            else
                                AddOffset(Weapon, 2 * .625f, -3 * .625f);
                        }
                    }
                    break;
                case 3:
                    if (actor.Unit.AdvancedMeleeWeaponType == 1)
                    {
                        if (actor.Unit.HasBreasts)
                        {
                            if (actor.Unit.BodySize < 2)
                                AddOffset(Weapon, 1 * .625f, -1 * .625f);
                        }
                        else
                        {
                            if (actor.Unit.BodySize < 2)
                                AddOffset(Weapon, 3 * .625f, -4 * .625f);
                            else
                                AddOffset(Weapon, 3 * .625f, -3 * .625f);
                        }
                    }
                    else
                    {
                        if (actor.Unit.HasBreasts)
                        {
                            if (actor.Unit.BodySize < 2)
                                AddOffset(Weapon, 0 * .625f, -1 * .625f);
                        }
                        else
                        {
                            AddOffset(Weapon, 3 * .625f, -4 * .625f);
                        }
                    }
                    break;
                case 4:
                    if (actor.Unit.HasBreasts)
                    {
                        if (actor.Unit.BodySize < 2)
                            AddOffset(Weapon, 1 * .625f, 0 * .625f);
                    }
                    else
                    {
                        AddOffset(Weapon, 2 * .625f, -1 * .625f);
                    }
                    break;
                case 5:
                    if (actor.Unit.HasBreasts)
                    {
                        if (actor.Unit.BodySize < 2)
                            AddOffset(Weapon, 1 * .625f, -1 * .625f);
                    }
                    else
                    {
                        AddOffset(Weapon, 2 * .625f, -3 * .625f);
                    }
                    break; ;
                case 6:
                    if (actor.Unit.HasBreasts)
                    {
                        if (actor.Unit.BodySize < 2)
                            AddOffset(Weapon, 1 * .625f, 0 * .625f);
                    }
                    else
                    {
                        AddOffset(Weapon, 2 * .625f, -1 * .625f);
                    }
                    break;
                case 7:
                    if (actor.Unit.HasBreasts)
                    {
                        if (actor.Unit.BodySize < 2)
                            AddOffset(Weapon, 1 * .625f, -1 * .625f);
                    }
                    else
                    {
                        AddOffset(Weapon, 2 * .625f, -3 * .625f);
                    }
                    break;
                default:
                    AddOffset(Weapon, 0 * .625f, 0 * .625f);
                    break;
            }
        }                         
    }


    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.ExtraColor1 = unit.AccessoryColor;
        unit.ExtraColor2 = unit.AccessoryColor;
        unit.ExtraColor3 = unit.AccessoryColor;
        unit.ExtraColor4 = unit.AccessoryColor;

        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
        unit.BodyAccentType3 = State.Rand.Next(BodyAccentTypes3);
        unit.BodyAccentType4 = State.Rand.Next(BodyAccentTypes4);
        unit.BodyAccentType5 = State.Rand.Next(BodyAccentTypes5);
        unit.FurType = State.Rand.Next(FurTypes);
        unit.SpecialAccessoryType = State.Rand.Next(SpecialAccessoryCount);
        unit.EarType = State.Rand.Next(EarTypes);
        unit.TailType = State.Rand.Next(TailTypes);
       
        if (unit.Furry)
        {
            int styles = 33;
            if (unit.HasDick && unit.HasBreasts)
            {
                if (Config.HermsOnlyUseFemaleHair)
                    unit.HairStyle = State.Rand.Next(23);
                else
                    unit.HairStyle = State.Rand.Next(styles);
            }
            else if (unit.HasDick && Config.FemaleHairForMales)
                unit.HairStyle = State.Rand.Next(styles);
            else if (unit.HasDick == false && Config.MaleHairForFemales)
                unit.HairStyle = State.Rand.Next(styles);
            else
            {
                if (unit.HasDick)
                {
                    unit.HairStyle = 17 + State.Rand.Next(15);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(23);
                }
            }
        }
        else
        {
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
                    unit.HairStyle = 18 + State.Rand.Next(18);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(18);
                }
            }

            if (unit.HasBreasts)
            {
                unit.BeardStyle = 6;
            }
            else
            {
                unit.BeardStyle = State.Rand.Next(6);
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

        if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
        {
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(Rags);
            if (unit.ClothingType == 0) //Covers rags not in the list
                unit.ClothingType = AllowedMainClothingTypes.Count;
        }
        if (unit.Type == UnitType.Leader)
            unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(LeaderClothes);
    }

    internal override int DickSizes => 6;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return AnthroBodySprite(actor);
        }
        return DemiBodySprite(actor);        
    }

    protected Sprite AnthroBodySprite(Actor_Unit actor)
    {
        return SpritesBodies1[40 + actor.Unit.BodySize + (actor.Unit.FurType * 6) + (actor.Unit.HasBreasts ? 0 : 3)];
    }

    protected Sprite DemiBodySprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return State.GameManager.SpriteDictionary.HumansBodySprites1[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            return State.GameManager.SpriteDictionary.HumansBodySprites1[0 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return State.GameManager.SpriteDictionary.HumansBodySprites1[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 1:
                return State.GameManager.SpriteDictionary.HumansBodySprites1[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 2:
                if (actor.Unit.AdvancedMeleeWeaponType == 1)
                {
                    return State.GameManager.SpriteDictionary.HumansBodySprites1[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
                }
                return State.GameManager.SpriteDictionary.HumansBodySprites1[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 3:
                if (actor.Unit.AdvancedMeleeWeaponType == 1)
                {
                    return State.GameManager.SpriteDictionary.HumansBodySprites1[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
                }
                return State.GameManager.SpriteDictionary.HumansBodySprites1[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 4:
                return State.GameManager.SpriteDictionary.HumansBodySprites1[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 5:
                return State.GameManager.SpriteDictionary.HumansBodySprites1[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 6:
                return State.GameManager.SpriteDictionary.HumansBodySprites1[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 7:
                return State.GameManager.SpriteDictionary.HumansBodySprites1[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            default:
                return State.GameManager.SpriteDictionary.HumansBodySprites1[0 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return AnthroHeadSprite(actor);
        }
        return DemiHeadSprite(actor);
    }

    protected Sprite AnthroHeadSprite(Actor_Unit actor)
    {

        if (actor.IsEating)
        {
            return SpritesBodies1[56];
        }
        else if (actor.IsAttacking)
        {
            return SpritesBodies1[55];

        }
        else
        {
            return SpritesBodies1[54];
        }
    }

    protected Sprite DemiHeadSprite(Actor_Unit actor)
    {

        if (actor.IsEating)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return State.GameManager.SpriteDictionary.HumansBodySprites2[4];
                }
                else
                {
                    return State.GameManager.SpriteDictionary.HumansBodySprites2[1];
                }
            }
            else
            {
                return State.GameManager.SpriteDictionary.HumansBodySprites2[7 + (actor.Unit.BodySize * 3)];
            }
        }
        else if (actor.IsAttacking)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return State.GameManager.SpriteDictionary.HumansBodySprites2[5];
                }
                else
                {
                    return State.GameManager.SpriteDictionary.HumansBodySprites2[2];
                }
            }
            else
            {
                return State.GameManager.SpriteDictionary.HumansBodySprites2[8 + (actor.Unit.BodySize * 3)];
            }
        }
        else
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return State.GameManager.SpriteDictionary.HumansBodySprites2[3];
                }
                else
                {
                    return State.GameManager.SpriteDictionary.HumansBodySprites2[0];
                }
            }
            else
            {
                return State.GameManager.SpriteDictionary.HumansBodySprites2[6 + (actor.Unit.BodySize * 3)];
            }

        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // Outer Body
    {
        if (actor.Unit.Furry)
        {
            return AnthroAccessorySprite(actor);
        }
        return DemiAccessorySprite(actor);

        
    } 
    protected Sprite AnthroAccessorySprite(Actor_Unit actor) 
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return SpritesBodies1[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            return SpritesBodies1[0 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return SpritesBodies1[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 1:
                return SpritesBodies1[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 2:
                if (actor.Unit.AdvancedMeleeWeaponType == 1)
                {
                    return SpritesBodies1[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
                }
                return SpritesBodies1[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 3:
                return SpritesBodies1[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 4:
                return SpritesBodies1[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 5:
                return SpritesBodies1[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 6:
                return SpritesBodies1[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 7:
                return SpritesBodies1[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            default:
                return SpritesBodies1[0 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
        }
    }

    protected Sprite DemiAccessorySprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return SpritesBodies2[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            return SpritesBodies2[0 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return SpritesBodies2[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 1:
                return SpritesBodies2[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 2:
                if (actor.Unit.AdvancedMeleeWeaponType == 1)
                {
                    return SpritesBodies2[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
                }
                return SpritesBodies2[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 3:
                if (actor.Unit.AdvancedMeleeWeaponType == 1)
                {
                    return SpritesBodies2[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
                }
                return SpritesBodies2[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 4:
                return SpritesBodies2[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 5:
                return SpritesBodies2[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 6:
                return SpritesBodies2[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 7:
                return SpritesBodies2[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            default:
                return SpritesBodies2[0 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Claws
    {
        int value = 24;

        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking)
                value += 3 + (actor.Unit.BodySize > 1 ? 4 : 0) + (actor.Unit.HasBreasts ? 0 : 12);
            else
                value += (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12);
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                value += 1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12);
                break;
            case 1:
                value += 3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12);
                break;
            case 2:
                if (actor.Unit.AdvancedMeleeWeaponType == 1)
                {
                    value += 1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12);                    
                }
                else
                {
                    value += 2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12);
                }
                break;
            case 3:
                value += 3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12);
                break;
            case 4:
                value += 1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12);
                break;
            case 5:
                value += 2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12);
                break;
            case 6:
                value += 1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12);
                break;
            case 7:
                value += 2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12);
                break;
            default:
                value += 0 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12);
                break;
        }

        if (actor.Unit.Furry)
        {

            if (actor.Unit.HasWeapon == false)
            {
                if (actor.IsAttacking) return SpritesBodies1[27 + (actor.Unit.BodySize >= 2 ? 4 : 0) + (actor.Unit.HasBreasts ? 0 : 8)];
                return SpritesBodies1[24 + (actor.Unit.BodySize >= 2 ? 4 : 0) + (actor.Unit.HasBreasts ? 0 : 8)];
            }

            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return SpritesBodies1[25 + (actor.Unit.BodySize >= 2 ? 4 : 0) + (actor.Unit.HasBreasts ? 0 : 8)];
                case 1:
                    return SpritesBodies1[27 + (actor.Unit.BodySize >= 2 ? 4 : 0) + (actor.Unit.HasBreasts ? 0 : 8)];
                case 2:
                    if (actor.Unit.AdvancedMeleeWeaponType == 1)
                    {
                        return SpritesBodies1[25 + (actor.Unit.BodySize >= 2 ? 4 : 0) + (actor.Unit.HasBreasts ? 0 : 8)];
                    }
                    return SpritesBodies1[26 + (actor.Unit.BodySize >= 2 ? 4 : 0) + (actor.Unit.HasBreasts ? 0 : 8)];
                case 3:
                    return SpritesBodies1[27 + (actor.Unit.BodySize >= 2 ? 4 : 0) + (actor.Unit.HasBreasts ? 0 : 8)];
                case 4:
                    return SpritesBodies1[25 + (actor.Unit.BodySize >= 2 ? 4 : 0) + (actor.Unit.HasBreasts ? 0 : 8)];
                case 5:
                    return SpritesBodies1[26 + (actor.Unit.BodySize >= 2 ? 4 : 0) + (actor.Unit.HasBreasts ? 0 : 8)];
                case 6:
                    return SpritesBodies1[25 + (actor.Unit.BodySize >= 2 ? 4 : 0) + (actor.Unit.HasBreasts ? 0 : 8)];
                case 7:
                    return SpritesBodies1[26 + (actor.Unit.BodySize >= 2 ? 4 : 0) + (actor.Unit.HasBreasts ? 0 : 8)];
                default:
                    return SpritesBodies1[24 + (actor.Unit.BodySize >= 2 ? 4 : 0) + (actor.Unit.HasBreasts ? 0 : 8)];
            }
        }


        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return SpritesBodies2[27 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            return SpritesBodies2[24 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return SpritesBodies2[25 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 1:
                return SpritesBodies2[27 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 2:
                if (actor.Unit.AdvancedMeleeWeaponType == 1)
                {
                    return SpritesBodies2[25 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
                }
                return SpritesBodies2[26 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 3:
                return SpritesBodies2[27 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 4:
                return SpritesBodies2[25 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 5:
                return SpritesBodies2[26 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 6:
                return SpritesBodies2[25 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 7:
                return SpritesBodies2[26 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            default:
                return SpritesBodies2[24 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // 
    {
        if (actor.Unit.Furry == false)
        {
            return null;
        }

        if (actor.IsEating)
        {
            return SpritesBodies1[62];
        }
        else if (actor.IsAttacking)
        {
            return SpritesBodies1[61];

        }
        else
        {
            return SpritesBodies1[60];
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Body pattern
    {
        int value = 0;
        if ((actor.Unit.SpecialAccessoryType == 6))
        {
            return null;
        }
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking)
                value += 2 + (actor.Unit.BodySize * 3) + (actor.Unit.HasBreasts ? 0 : 9);
            else
                value += (actor.Unit.BodySize * 3) + (actor.Unit.HasBreasts ? 0 : 9);
        }
        else
        {
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    value += 0 + (actor.Unit.BodySize * 3) + (actor.Unit.HasBreasts ? 0 : 9);
                    break;
                case 1:
                    value += 2 + (actor.Unit.BodySize * 3) + (actor.Unit.HasBreasts ? 0 : 9);
                    break;
                case 2:
                    if (actor.Unit.AdvancedMeleeWeaponType == 1)
                    {
                        value += 0 + (actor.Unit.BodySize * 3) + (actor.Unit.HasBreasts ? 0 : 9);
                    }
                    else
                    {
                        value += 1 + (actor.Unit.BodySize * 3) + (actor.Unit.HasBreasts ? 0 : 9);
                    }
                    break;
                case 3:
                    value += 2 + (actor.Unit.BodySize * 3) + (actor.Unit.HasBreasts ? 0 : 9);
                    break;
                case 4:
                    value += 0 + (actor.Unit.BodySize * 3) + (actor.Unit.HasBreasts ? 0 : 9);
                    break;
                case 5:
                    value += 1 + (actor.Unit.BodySize * 3) + (actor.Unit.HasBreasts ? 0 : 9);
                    break;
                case 6:
                    value += 0 + (actor.Unit.BodySize * 3) + (actor.Unit.HasBreasts ? 0 : 9);
                    break;
                case 7:
                    value += 1 + (actor.Unit.BodySize * 3) + (actor.Unit.HasBreasts ? 0 : 9);
                    break;
                default:
                    value += 0 + (actor.Unit.BodySize * 3) + (actor.Unit.HasBreasts ? 0 : 9);
                    break;
            }
        }         
        if (actor.Unit.Furry)
        {
            value += actor.Unit.SpecialAccessoryType * 18;
            return SpritesPatterns1[value];
        }
        value += (actor.Unit.SpecialAccessoryType % 4) * 24;
        return SpritesPatterns2[value];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Head Pattern
    {
        int value = 0;
        if (actor.Unit.SpecialAccessoryType == 6)
        {
            return null;
        }
        // Anthro
        if (actor.Unit.Furry)
        {
            switch (actor.Unit.SpecialAccessoryType)
            {
                case 1:
                    value = 0;
                    break; 
                case 2:
                case 5:
                    value = 1;
                    break;
                case 3:
                    value = 2;
                    break;
                case 4:
                case 6:
                     value = 3;
                    break;
                default:
                    break;
            }
            if (actor.IsEating)
            {
                return SpritesPatterns1[109 + (value * 2)];
            }
            else
            {
                return SpritesPatterns1[108 + (value * 2)];
            }
        }

        value = ((actor.Unit.SpecialAccessoryType % 4) * 24) + (actor.IsOralVoring ? 1 : 0);
        if (actor.Unit.BodySize >= 2)
        {
            if (actor.Unit.HasBreasts)
            {
                return SpritesPatterns2[20 + value];
            }
            else
            {
                return SpritesPatterns2[22 + value];
            }
        }

        return SpritesPatterns2[18 + value];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Inner Ear
    {
        if (actor.Unit.EarType >= 16)
        {
            return null;
        }
        if (actor.Unit.Furry)
        {
            return SpritesCustomisation2[20 + actor.Unit.EarType];
        }
        if ((actor.Unit.EarType == 2 || actor.Unit.EarType == 11) && actor.Unit.HasBreasts && actor.Unit.BodySize >= 2)
        {
            switch (actor.Unit.EarType)
            {
                case 2:
                    return SpritesCustomisation3[109];
                case 11:
                    return SpritesCustomisation3[110];
                default:
                    break;
            }
        }
        return SpritesCustomisation3[90 + actor.Unit.EarType];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Outer Ear
    {
        if (actor.Unit.EarType == 21)
        {
            return null;
        }
        if (actor.Unit.Furry)
        {
            return SpritesCustomisation2[0 + actor.Unit.EarType];
        }
        if ((actor.Unit.EarType == 2 || actor.Unit.EarType == 11 || actor.Unit.EarType == 19) && actor.Unit.HasBreasts && actor.Unit.BodySize >= 2)
        {
            switch (actor.Unit.EarType)
            {
                case 2:
                    return SpritesCustomisation3[106];
                case 11:
                    return SpritesCustomisation3[107];
                case 19:
                    return SpritesCustomisation3[108];
                default:
                    break;
            }
        }
        return SpritesCustomisation3[70 + actor.Unit.EarType];
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Lower Horns
    {
        if (actor.Unit.BodyAccentType2 >= 9)
        {
            return null;
        }
        if (actor.Unit.Furry)
        {
            return SpritesCustomisation2[36 + actor.Unit.BodyAccentType2];
        }
        return SpritesCustomisation3[48 + actor.Unit.BodyAccentType2];
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // Upper Horns
    {
        if (actor.Unit.BodyAccentType2 <= 6)
        {
            return null;
        }
        if (actor.Unit.Furry)
        {
            BodyAccent8.layer = 23;
            return SpritesCustomisation2[36 + actor.Unit.BodyAccentType2];
        }
        else
        {
            BodyAccent8.layer = 27;
            return SpritesCustomisation3[48 + actor.Unit.BodyAccentType2];
        }

    }

    protected override Sprite BodyAccentSprite9(Actor_Unit actor) // Inner Head
    {
        if (actor.Unit.Furry == false)
        {
            return null;
        }

        if (actor.IsEating)
        {
            return SpritesBodies1[59];
        }
        else if (actor.IsAttacking)
        {
            return SpritesBodies1[58];

        }
        else
        {
            return SpritesBodies1[57];
        }
    }

    protected override Sprite BodyAccentSprite10(Actor_Unit actor) // Outer Wings
    {
        if (actor.Unit.Furry)
        {
            return SpritesCustomisation1[0 + actor.Unit.BodyAccentType1];
        }
        return SpritesCustomisation3[0 + actor.Unit.BodyAccentType1];
    }

    protected override Sprite BodyAccentSprite11(Actor_Unit actor) // Inner Wings
    {
        if (actor.Unit.Furry)
        {
            return SpritesCustomisation1[10 + actor.Unit.BodyAccentType1];
        }
        return SpritesCustomisation3[10 + actor.Unit.BodyAccentType1];
    }

    protected override Sprite BodyAccentSprite12(Actor_Unit actor) // Claw Wings
    {
        if (actor.Unit.Furry)
        {
            return SpritesCustomisation1[20 + actor.Unit.BodyAccentType1];
        }
        return SpritesCustomisation3[20 + actor.Unit.BodyAccentType1];
    }

    protected override Sprite BodyAccentSprite13(Actor_Unit actor) // Outer Tails
    {
        if (actor.Unit.Furry)
        {
            return SpritesCustomisation1[30 + actor.Unit.TailType];
        }
        return SpritesCustomisation3[30 + actor.Unit.TailType];
    }

    protected override Sprite BodyAccentSprite14(Actor_Unit actor) // Inner Tails
    {
        if (actor.Unit.TailType >= 9)
        {
            return null;
        }
        if (actor.Unit.Furry)
        {
            if (actor.Unit.FurType == 0)
            {
                return SpritesCustomisation1[40 + actor.Unit.TailType];
            }
            return SpritesCustomisation1[48 + actor.Unit.TailType];

        }
        return SpritesCustomisation3[40 + actor.Unit.TailType];
    }

    protected override Sprite BodyAccentSprite15(Actor_Unit actor) // Left Nipple
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.Unit.Furry)
        {
            if (actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f));
                if (leftSize > actor.Unit.DefaultBreastSize)
                    oversize = true;
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    return SpritesBreasts[95];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    return SpritesBreasts[94];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    return SpritesBreasts[93];
                }

                if (leftSize > 28)
                    leftSize = 28;

                return SpritesBreasts[64 + leftSize];
            }
            else
            {
                return SpritesBreasts[64 + actor.Unit.BreastSize];
            }
        }
        return null;
    }

    protected override Sprite BeardSprite(Actor_Unit actor) // Right Nipple
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.Unit.Furry)
        {
            if (actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f));
                if (rightSize > actor.Unit.DefaultBreastSize)
                    oversize = true;
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    return SpritesBreasts[127];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    return SpritesBreasts[126];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    return SpritesBreasts[125];
                }

                if (rightSize > 28)
                    rightSize = 28;

                return SpritesBreasts[96 + rightSize];
            }
            else
            {
                return SpritesBreasts[96 + actor.Unit.BreastSize];
            }
        }
        return null;
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            Mouth.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemidragonSkin, actor.Unit.ExtraColor3);
            if (actor.IsEating)
                return SpritesBodies1[64];
            else if (actor.IsAttacking)
                return SpritesBodies1[63];
            else
                return null;
        }
        if (actor.IsEating || actor.IsAttacking)
            return null;
        else
            return State.GameManager.SpriteDictionary.HumansBodySprites3[108 + actor.Unit.MouthType];
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            int style = actor.Unit.HairStyle % 33;
            if (style == 32)
            {
                return null;
            }
            return SpritesCustomisation1[56 + style];

        }
        return State.GameManager.SpriteDictionary.HumansBodySprites2[71 + 2 * actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            int style = actor.Unit.HairStyle % 33;
            if (style == 32)
            {
                return null;
            }
            if (style <= 25)
            {
                return SpritesCustomisation1[88 + style];
            }
            return null;

        }
        return State.GameManager.SpriteDictionary.HumansBodySprites2[72 + 2 * actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite3(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return null;
        }
        return State.GameManager.SpriteDictionary.HumansBodySprites3[120 + actor.Unit.BodyAccentType5];
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            if (actor.IsOralVoring)
            {
                return SpritesCustomisation2[66 + actor.Unit.EyeType];
            }
            return SpritesCustomisation2[58 + actor.Unit.EyeType];
        }
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            return State.GameManager.SpriteDictionary.HumansBodySprites2[69];
        }
        else
        {
            return State.GameManager.SpriteDictionary.HumansBodySprites3[24 + 4 * (actor.Unit.EyeType % 6) + ((actor.IsAttacking || actor.IsEating) ? 0 : 2)];
        }
    }

    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            if (actor.IsOralVoring)
            {
                return SpritesCustomisation2[82 + actor.Unit.EyeType];
            }
            return SpritesCustomisation2[74 + actor.Unit.EyeType];
        }
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            return State.GameManager.SpriteDictionary.HumansBodySprites2[69];
        }
        else
        {
            return State.GameManager.SpriteDictionary.HumansBodySprites3[25 + 4 * (actor.Unit.EyeType % 6) + ((actor.IsAttacking || actor.IsEating) ? 0 : 2)];
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.Unit.Furry)
        {
            if (actor.HasBelly)
            {
                if (actor.Unit.FurType == 0)
                {
                    belly.transform.localScale = new Vector3(1, 1, 1);
                    belly.SetActive(true);
                    int size = actor.GetStomachSize(31, 0.7f);
                    if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                    {
                        AddOffset(Belly, 0, -29 * .625f);
                        return SpritesBelliesBalls[75];
                    }
                    else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                    {
                        AddOffset(Belly, 0, -29 * .625f);
                        return SpritesBelliesBalls[74];
                    }
                    else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                    {
                        AddOffset(Belly, 0, -29 * .625f);
                        return SpritesBelliesBalls[73];
                    }
                    else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                    {
                        AddOffset(Belly, 0, -29 * .625f);
                        return SpritesBelliesBalls[72];
                    }
                    switch (size)
                    {
                        case 25:
                            AddOffset(Belly, 0, -1 * .625f);
                            break;
                        case 26:
                            AddOffset(Belly, 0, -3 * .625f);
                            break;
                        case 27:
                            AddOffset(Belly, 0, -6 * .625f);
                            break;
                        case 28:
                            AddOffset(Belly, 0, -10 * .625f);
                            break;
                        case 29:
                            AddOffset(Belly, 0, -15 * .625f);
                            break;
                        case 30:
                            AddOffset(Belly, 0, -20 * .625f);
                            break;
                        case 31:
                            AddOffset(Belly, 0, -25 * .625f);
                            break;
                    }

                    return SpritesBelliesBalls[40 + size];
                }
                else
                {
                    belly.transform.localScale = new Vector3(1, 1, 1);
                    belly.SetActive(true);
                    int size = actor.GetStomachSize(31, 0.7f);
                    if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                    {
                        AddOffset(Belly, 0, -29 * .625f);
                        return SpritesBelliesBalls[111];
                    }
                    else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                    {
                        AddOffset(Belly, 0, -29 * .625f);
                        return SpritesBelliesBalls[110];
                    }
                    else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                    {
                        AddOffset(Belly, 0, -29 * .625f);
                        return SpritesBelliesBalls[109];
                    }
                    else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                    {
                        AddOffset(Belly, 0, -29 * .625f);
                        return SpritesBelliesBalls[108];
                    }
                    switch (size)
                    {
                        case 25:
                            AddOffset(Belly, 0, -1 * .625f);
                            break;
                        case 26:
                            AddOffset(Belly, 0, -3 * .625f);
                            break;
                        case 27:
                            AddOffset(Belly, 0, -6 * .625f);
                            break;
                        case 28:
                            AddOffset(Belly, 0, -10 * .625f);
                            break;
                        case 29:
                            AddOffset(Belly, 0, -15 * .625f);
                            break;
                        case 30:
                            AddOffset(Belly, 0, -20 * .625f);
                            break;
                        case 31:
                            AddOffset(Belly, 0, -25 * .625f);
                            break;
                    }

                    return SpritesBelliesBalls[76 + size];
                }
            }
            else
            {
                return null;
            }
        }
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(31, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return State.GameManager.SpriteDictionary.HumansVoreSprites[105];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return State.GameManager.SpriteDictionary.HumansVoreSprites[104];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return State.GameManager.SpriteDictionary.HumansVoreSprites[103];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return State.GameManager.SpriteDictionary.HumansVoreSprites[102];
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

            return State.GameManager.SpriteDictionary.HumansVoreSprites[70 + size];
        }
        else
        {
            return null;
        }
    }

    internal Sprite BellySpriteAnthro(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(31, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -29 * .625f);
                return SpritesBelliesBalls[75];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -29 * .625f);
                return SpritesBelliesBalls[74];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -29 * .625f);
                return SpritesBelliesBalls[73];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -29 * .625f);
                return SpritesBelliesBalls[72];
            }
            switch (size)
            {
                case 25:
                    AddOffset(Belly, 0, -1 * .625f);
                    break;
                case 26:
                    AddOffset(Belly, 0, -3 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -6 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -10 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -15 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -20 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -25 * .625f);
                    break;
            }

            return SpritesBelliesBalls[40 + size];
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
            if (actor.Unit.Furry)
            {
                switch (actor.GetWeaponSprite())
                {
                    case 0:
                        Weapon.layer = 6;
                        if (actor.Unit.BasicMeleeWeaponType == 1)
                        {
                            return SpritesCustomisation2[126];
                        }
                        return SpritesCustomisation2[122];
                    case 1:
                        Weapon.layer = 21;
                        if (actor.Unit.BasicMeleeWeaponType == 1)
                        {
                            return SpritesCustomisation2[127];
                        }
                        return SpritesCustomisation2[123];
                    case 2:
                        Weapon.layer = 6;
                        if (actor.Unit.AdvancedMeleeWeaponType == 1)
                        {
                            return SpritesCustomisation2[128];
                        }
                        return SpritesCustomisation2[124];
                    case 3:
                        Weapon.layer = 21;
                        if (actor.Unit.AdvancedMeleeWeaponType == 1)
                        {
                            return SpritesCustomisation2[129];
                        }
                        return SpritesCustomisation2[125];
                    case 4:
                        Weapon.layer = 6;
                        return SpritesCustomisation2[130];
                    case 5:
                        Weapon.layer = 21;
                        return SpritesCustomisation2[131];
                    case 6:
                        Weapon.layer = 6;
                        return SpritesCustomisation2[132];
                    case 7:
                        Weapon.layer = 21;
                        return SpritesCustomisation2[133];
                    default:
                        return null;
                }
            }
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    Weapon.layer = 6;
                    if (actor.Unit.BasicMeleeWeaponType == 1)
                    {
                        return SpritesCustomisation3[115];
                    }
                    return SpritesCustomisation3[111];
                case 1:
                    Weapon.layer = 21;
                    if (actor.Unit.BasicMeleeWeaponType == 1)
                    {
                        return SpritesCustomisation3[116];
                    }
                    return SpritesCustomisation3[112];
                case 2:
                    Weapon.layer = 6;
                    if (actor.Unit.AdvancedMeleeWeaponType == 1)
                    {
                        return SpritesCustomisation3[117];
                    }
                    return SpritesCustomisation3[113];
                case 3:
                    Weapon.layer = 21;
                    if (actor.Unit.AdvancedMeleeWeaponType == 1)
                    {
                        return SpritesCustomisation3[118];
                    }
                    return SpritesCustomisation3[114];
                case 4:
                    Weapon.layer = 6;
                    return SpritesCustomisation3[119];
                case 5:
                    Weapon.layer = 21;
                    return SpritesCustomisation3[120];
                case 6:
                    Weapon.layer = 6;
                    return SpritesCustomisation3[121];
                case 7:
                    Weapon.layer = 21;
                    return SpritesCustomisation3[122];
                default:
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
        if (actor.Unit.Furry)
        {
            if (actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f));
                if (leftSize > actor.Unit.DefaultBreastSize)
                    oversize = true;
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    return SpritesBreasts[31];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    return SpritesBreasts[30];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    return SpritesBreasts[29];
                }

                if (leftSize > 28)
                    leftSize = 28;

                return SpritesBreasts[0 + leftSize];
            }
            else
            {
                return SpritesBreasts[0 + actor.Unit.BreastSize];
            }
        }
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f));
            if (leftSize > actor.Unit.DefaultBreastSize)
                oversize = true;
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
        oversize = false;
        if (actor.Unit.Furry)
        {
            if (actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f));
                if (rightSize > actor.Unit.DefaultBreastSize)
                    oversize = true;
                if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    return SpritesBreasts[63];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    return SpritesBreasts[62];
                }
                else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    return SpritesBreasts[61];
                }

                if (rightSize > 28)
                    rightSize = 28;

                return SpritesBreasts[32 + rightSize];
            }
            else
            {
                return SpritesBreasts[32 + actor.Unit.BreastSize];
            }
        }
        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f));
            if (rightSize > actor.Unit.DefaultBreastSize)
                oversize = true;
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
        if (actor.Unit.Furry)
        {
            if (actor.IsErect())
            {
                if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
                {
                    Dick.layer = 20;
                    if (actor.IsCockVoring)
                    {
                        return SpritesCustomisation2[114 + actor.Unit.DickSize];
                    }
                    else
                    {
                        return SpritesCustomisation2[106 + actor.Unit.DickSize];
                    }
                }
                else
                {
                    Dick.layer = 13;
                    if (actor.IsCockVoring)
                    {
                        return SpritesCustomisation2[98 + actor.Unit.DickSize];
                    }
                    else
                    {
                        return SpritesCustomisation2[90 + actor.Unit.DickSize];
                    }
                }
            }

            Dick.layer = 11;
            return null;
        }
        if (actor.IsErect())
        {
            if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
            {
                Dick.layer = 20;
                return State.GameManager.SpriteDictionary.HumansBodySprites4[1 + (actor.Unit.DickSize * 2) + ((actor.Unit.BodySize > 1) ? 12 : 0) + ((!actor.Unit.HasBreasts) ? 24 : 0)];
            }
            else
            {
                Dick.layer = 13;
                return State.GameManager.SpriteDictionary.HumansBodySprites4[0 + (actor.Unit.DickSize * 2) + ((actor.Unit.BodySize > 1) ? 12 : 0) + ((!actor.Unit.HasBreasts) ? 24 : 0)];
            }
        }

        Dick.layer = 11;
        return State.GameManager.SpriteDictionary.HumansBodySprites4[0 + (actor.Unit.DickSize * 2) + ((actor.Unit.BodySize > 1) ? 12 : 0) + ((!actor.Unit.HasBreasts) ? 24 : 0)];
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
        int offset = actor.GetBallSize(30, .8f);
        if (actor.Unit.Furry) 
        {
            if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 30)
            {
                AddOffset(Balls, 0, -38 * .625f);
                return SpritesBelliesBalls[39];
            }
            else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 30)
            {
                AddOffset(Balls, 0, -32 * .625f);
                return SpritesBelliesBalls[38];
            }
            else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 29)
            {
                AddOffset(Balls, 0, -28 * .625f);
                return SpritesBelliesBalls[37];
            }
            else if (offset >= 28)
            {
                AddOffset(Balls, 0, -24 * .625f);
            }
            else if (offset == 27)
            {
                AddOffset(Balls, 0, -19 * .625f);
            }
            else if (offset == 26)
            {
                AddOffset(Balls, 0, -14 * .625f);
            }
            else if (offset == 25)
            {
                AddOffset(Balls, 0, -8 * .625f);
            }
            else if (offset == 24)
            {
                AddOffset(Balls, 0, -4 * .625f);
            }
            else if (offset == 23)
            {
                AddOffset(Balls, 0, -2 * .625f);
            }

            if (offset > 0)
                return SpritesBelliesBalls[Math.Min(8 + offset, 36)];
            return SpritesBelliesBalls[size];
        }

        offset = actor.GetBallSize(28, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return State.GameManager.SpriteDictionary.HumansVoreSprites[141];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return State.GameManager.SpriteDictionary.HumansVoreSprites[140];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return State.GameManager.SpriteDictionary.HumansVoreSprites[139];
        }
        else if (offset >= 26)
        {
            AddOffset(Balls, 0, -22 * .625f);
        }
        else if (offset == 25)
        {
            AddOffset(Balls, 0, -16 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -13 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -11 * .625f);
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

        if (offset > 0)
            return State.GameManager.SpriteDictionary.HumansVoreSprites[Math.Min(112 + offset, 138)];
        return State.GameManager.SpriteDictionary.HumansVoreSprites[106 + size];
    }

    protected Sprite PussySprite(Actor_Unit actor)
    {
        if (!actor.Unit.Furry)
        {
            return null;
        }
        return SpritesBodies1[52 + (actor.Unit.BodySize >= 2 ? 1 : 0)];
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
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null); Type = 1524;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.Furry)
            {
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
                if (Races.DemiDragons.oversize)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[54];

                }
                else if (actor.Unit.HasBreasts)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[46 + actor.Unit.BreastSize];
                }
                else
                {
                    blocksBreasts = true;
                    breastSprite = null;
                    clothing1.GetSprite = null;
                    clothing2.GetSprite = null;
                    clothing3.GetSprite = null;
                }

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);

            }
            else 
            {
                if (Races.DemiDragons.oversize)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[56];
                    blocksBreasts = false;
                    clothing2.GetSprite = null;
                    clothing3.GetSprite = null;
                }
                else if (actor.Unit.HasBreasts)
                {
                    blocksBreasts = true;
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[0 + actor.Unit.BreastSize];
                    if (actor.Unit.BreastSize == 3)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                    }
                    else if (actor.Unit.BreastSize == 4)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                    }
                    else if (actor.Unit.BreastSize == 5)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                    }
                    else
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
                clothing2.GetPalette = (s) => FurryColorInner(s);
                clothing3.GetPalette = (s) => FurryColorInner(s);
            }

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
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null); Type = 1534;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.Furry)
            {
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
                if (Races.DemiDragons.oversize)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[63];
                }
                else if (actor.Unit.HasBreasts)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[55 + actor.Unit.BreastSize];
                }
                else
                {
                    breastSprite = null;
                    clothing1.GetSprite = null;
                }

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            }
            else
            {
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
                if (Races.DemiDragons.oversize)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[59];
                    blocksBreasts = false;
                    clothing2.GetSprite = null;
                    clothing3.GetSprite = null;
                }
                else if (actor.Unit.HasBreasts)
                {
                    blocksBreasts = true;
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[16 + actor.Unit.BreastSize];
                    clothing2.GetPalette = (s) => FurryColorInner(s);
                    clothing3.GetPalette = (s) => FurryColorInner(s);
                    if (actor.Unit.BreastSize == 3)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                    }
                    else if (actor.Unit.BreastSize == 4)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                    }
                    else if (actor.Unit.BreastSize == 5)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                    }
                    else
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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
            }


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
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 1544;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.Furry)
            {
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
                if (Races.DemiDragons.oversize)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[72];
                }
                else if (actor.Unit.HasBreasts)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[64 + actor.Unit.BreastSize];
                }
                else
                {
                    breastSprite = null;
                    clothing1.GetSprite = null;
                }

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            }
            else
            {

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
                clothing2.GetPalette = (s) => FurryColorInner(s);
                clothing3.GetPalette = (s) => FurryColorInner(s);
                if (Races.DemiDragons.oversize)
                {
                    clothing1.GetSprite = (s) => null;
                    blocksBreasts = false;
                    clothing2.GetSprite = null;
                    clothing3.GetSprite = null;
                }
                else if (actor.Unit.HasBreasts)
                {
                    blocksBreasts = true;
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[24 + actor.Unit.BreastSize];
                    if (actor.Unit.BreastSize == 3)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                    }
                    else if (actor.Unit.BreastSize == 4)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                    }
                    else if (actor.Unit.BreastSize == 5)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                    }
                    else
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
                    }
                }
            }


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
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 1555;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.Furry)
            {
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
                if (Races.DemiDragons.oversize)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[81];
                }
                else if (actor.Unit.HasBreasts)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[73 + actor.Unit.BreastSize];
                }
                else
                {
                    breastSprite = null;
                    clothing1.GetSprite = null;
                    clothing2.GetSprite = null;
                }

                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[82];
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            }
            else
            {

                if (Races.DemiDragons.oversize)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[63];
                    blocksBreasts = false;
                    clothing2.GetSprite = null;
                    clothing3.GetSprite = null;
                }
                else if (actor.Unit.HasBreasts)
                {
                    blocksBreasts = true;
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[32 + actor.Unit.BreastSize];
                    if (actor.Unit.BreastSize == 3)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                    }
                    else if (actor.Unit.BreastSize == 4)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                    }
                    else if (actor.Unit.BreastSize == 5)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                    }
                    else
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
                clothing2.GetPalette = (s) => FurryColorInner(s);
                clothing3.GetPalette = (s) => FurryColorInner(s);
            }


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
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 1574;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.Furry)
            {
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
                blocksBreasts = false;
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
                if (Races.DemiDragons.oversize)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[91];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[142];
                }
                else if (actor.Unit.HasBreasts)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[83 + actor.Unit.BreastSize];
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[92 + actor.Unit.BreastSize];
                }
                else
                {
                    breastSprite = null;
                    clothing1.GetSprite = null;
                    clothing2.GetSprite = null;
                }
                clothing2.GetPalette = (s) => null;
            }
            else
            {
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
                if (Races.DemiDragons.oversize)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[65];
                    blocksBreasts = false;
                    clothing2.GetSprite = null;
                    clothing3.GetSprite = null;
                }
                else if (actor.Unit.HasBreasts)
                {
                    blocksBreasts = true;
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[40 + actor.Unit.BreastSize];
                    if (actor.Unit.BreastSize == 3)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                    }
                    else if (actor.Unit.BreastSize == 4)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                    }
                    else if (actor.Unit.BreastSize == 5)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                    }
                    else
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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

                clothing2.GetPalette = (s) => FurryColorInner(s);
                clothing3.GetPalette = (s) => FurryColorInner(s);
            }

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
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null);
            Type = 1588;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.Furry)
            {
                if (Races.DemiDragons.oversize)
                {
                    clothing1.GetSprite = null;
                }
                else if (actor.Unit.HasBreasts)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[103 + actor.Unit.BreastSize];
                }
                else
                {
                    breastSprite = null;
                    clothing1.GetSprite = null;
                }

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            }
            else
            {
                clothing1.GetPalette = (s) => null;
                if (Races.DemiDragons.oversize)
                {
                    clothing1.GetSprite = (s) => null;
                    blocksBreasts = false;
                    clothing2.GetSprite = null;
                    clothing3.GetSprite = null;
                }
                else if (actor.Unit.HasBreasts)
                {
                    blocksBreasts = true;
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[8 + actor.Unit.BreastSize];
                    if (actor.Unit.BreastSize == 3)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                    }
                    else if (actor.Unit.BreastSize == 4)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                    }
                    else if (actor.Unit.BreastSize == 5)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                    }
                    else
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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

                clothing2.GetPalette = (s) => FurryColorInner(s);
                clothing3.GetPalette = (s) => FurryColorInner(s);
            }

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
            clothing1 = new SpriteExtraInfo(18, null, WhiteColored);
            clothing2 = new SpriteExtraInfo(17, null, null);
            clothing3 = new SpriteExtraInfo(17, null, null); 
            Type = 1544;
            DiscardUsesPalettes = true;
        }

        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.Furry)
            {
                clothing2.GetSprite = null;
                clothing3.GetSprite = null;
                if (Races.DemiDragons.oversize)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[135];
                }
                else if (actor.Unit.HasBreasts)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[127 + actor.Unit.BreastSize];
                }
                else
                {
                    breastSprite = null;
                    clothing1.GetSprite = null;
                }

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            }
            else
            {
                if (Races.DemiDragons.oversize)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[67];
                    blocksBreasts = false;
                    clothing2.GetSprite = null;
                    clothing3.GetSprite = null;
                }
                else if (actor.Unit.HasBreasts)
                {
                    blocksBreasts = true;
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenFundertops[48 + actor.Unit.BreastSize];
                    if (actor.Unit.BreastSize == 3)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[64];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[67];
                    }
                    else if (actor.Unit.BreastSize == 4)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[65];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[68];
                    }
                    else if (actor.Unit.BreastSize == 5)
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[66];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[69];
                    }
                    else
                    {
                        clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
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

                clothing2.GetPalette = (s) => null;
                clothing3.GetPalette = (s) => null;
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
            if (actor.Unit.Furry)
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[114 + actor.Unit.BodySize];
                }
                else
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[111 + actor.Unit.BodySize];
                }
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[0];

            }
 

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
            if (actor.Unit.Furry)
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[100 + actor.Unit.BodySize];
                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            }
            else
            {
                if (actor.HasBelly)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[4];
                }
                else
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenMundertops[1 + actor.Unit.BodySize];
                }
            }
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
            if (actor.Unit.Furry)
            {
                if (Races.DemiDragons.oversize)
                {
                    clothing1.GetSprite = null;
                }
                else if (actor.Unit.HasBreasts)
                {
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[2 + actor.Unit.BreastSize];
                }
                else
                {
                    breastSprite = null;
                    clothing1.GetSprite = null;
                }

                if (actor.Unit.BodySize > 1)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[1];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[0];
                }
            }
            else
            {
                clothing1.GetSprite = null;
                clothing2.GetSprite = null;
            }
            clothing1.GetPalette = (s) => FurryColorInner(s);
            clothing2.GetPalette = (s) => FurryColorInner(s);

            base.Configure(sprite, actor);
        }
    }

    class DemidragonRags : MainClothing
    {
        public DemidragonRags()
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
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[124];
                else if (actor.Unit.BreastSize < 6)
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[125];
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[126];

                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[117 + actor.Unit.BodySize];
            }
            else
            {
                clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[123];
                clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[120 + actor.Unit.BodySize];
            }

            base.Configure(sprite, actor);
        }
    }

    class DemidragonLeader : MainClothing
    {
        public DemidragonLeader()
        {
            inFrontOfDick = true;
            coversBreasts = false;
            Type = 200700;
            OccupiesAllSlots = true;
            clothing1 = new SpriteExtraInfo(5, null, WhiteColored); // Shoes
            clothing2 = new SpriteExtraInfo(18, null, WhiteColored); // Robe low
            clothing3 = new SpriteExtraInfo(19, null, WhiteColored); // Robe high
            clothing4 = new SpriteExtraInfo(19, null, WhiteColored); // Robe Breast
            clothing5 = new SpriteExtraInfo(20, null, WhiteColored); // Robe Waist
            clothing6 = new SpriteExtraInfo(12, null, WhiteColored); // Robe Arm
            clothing7 = new SpriteExtraInfo(20, null, WhiteColored); // Mantle
            clothing8 = new SpriteExtraInfo(30, null, WhiteColored); // Headwear
            clothing9 = new SpriteExtraInfo(5, null, WhiteColored); // Ring
        }
        public override void Configure(CompleteSprite sprite, Actor_Unit actor)
        {
            if (actor.Unit.Furry)
            {
                if (actor.GetBallSize(30, .8f) >= 9)
                    clothing1.GetSprite = null;
                else
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[0 + (actor.Unit.BodySize >= 2 ? 1 : 0)];

                clothing5.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[17 + (actor.Unit.HasBreasts ? 0 : 3) + actor.Unit.BodySize];
                clothing7.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[43 + (actor.Unit.HasBreasts ? 0 : 2) + (actor.Unit.BodySize >= 2 ? 1 : 0)];
                clothing8.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[47 + (actor.Unit.HasBreasts ? 1 : 0)];
                clothing9.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[50 + (actor.Unit.HasBreasts ? 0 : 2) + (actor.Unit.BodySize >= 2 ? 1 : 0)];

                if (actor.Unit.HasWeapon == false)
                {
                    if (actor.IsAttacking) clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[2 + (actor.Unit.HasBreasts ? 0 : 6) + (actor.Unit.BodySize >= 2 ? 3 : 0)];
                    clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[23 + (actor.Unit.HasBreasts ? 0 : 6) + (actor.Unit.BodySize >= 2 ? 3 : 0)];
                }
                else
                {
                    switch (actor.GetWeaponSprite())
                    {
                        case 0:
                            clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[23 + (actor.Unit.HasBreasts ? 0 : 6) + (actor.Unit.BodySize >= 2 ? 3 : 0)];
                            break;
                        case 1:
                            clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[25 + (actor.Unit.HasBreasts ? 0 : 6) + (actor.Unit.BodySize >= 2 ? 3 : 0)];
                            break;
                        case 2:
                            if (actor.Unit.BodyAccentType5 == 1)
                            {
                                clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[23 + (actor.Unit.HasBreasts ? 0 : 6) + (actor.Unit.BodySize >= 2 ? 3 : 0)];
                                break;
                            }
                            clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[24 + (actor.Unit.HasBreasts ? 0 : 6) + (actor.Unit.BodySize >= 2 ? 3 : 0)];
                            break;
                        case 3:
                            clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[25 + (actor.Unit.HasBreasts ? 0 : 6) + (actor.Unit.BodySize >= 2 ? 3 : 0)];
                            break;
                        case 4:
                            clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[23 + (actor.Unit.HasBreasts ? 0 : 6) + (actor.Unit.BodySize >= 2 ? 3 : 0)];
                            break;
                        case 5:
                            clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[24 + (actor.Unit.HasBreasts ? 0 : 6) + (actor.Unit.BodySize >= 2 ? 3 : 0)];
                            break;
                        case 6:
                            clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[23 + (actor.Unit.HasBreasts ? 0 : 6) + (actor.Unit.BodySize >= 2 ? 3 : 0)];
                            break;
                        case 7:
                            clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[24 + (actor.Unit.HasBreasts ? 0 : 6) + (actor.Unit.BodySize >= 2 ? 3 : 0)];
                            break;
                        default:
                            clothing6.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[23 + (actor.Unit.HasBreasts ? 0 : 6) + (actor.Unit.BodySize >= 2 ? 3 : 0)];
                            break;
                    }
                }
                    

                if (actor.Unit.HasBreasts)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[2 + actor.Unit.BodySize];
                    if (actor.HasBelly)
                    {
                        clothing3.GetSprite = null;                    
                    }
                    else
                    {
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[8 + actor.Unit.BodySize];
                    }
                    if(actor.HasPreyInBreasts)
                        clothing4.GetSprite = null;
                    else
                        clothing4.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[35 + actor.Unit.BreastSize];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[5 + actor.Unit.BodySize];
                    if (actor.HasBelly)
                    {
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[14 + actor.Unit.BodySize];
                    }
                    else
                    {
                        clothing3.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes2[11 + actor.Unit.BodySize];
                    }
                    clothing4.GetSprite = null;
                }
            }
            else
            {

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
            if (actor.Unit.Furry)
            {
                if (actor.Unit.HasBreasts)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[10 + actor.Unit.BodySize];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[13 + actor.Unit.BodySize];
                }

                if (actor.Unit.DickSize > 0)
                {
                    if (actor.Unit.DickSize < 3)
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[16 + ((actor.Unit.BodySize) * 3)];
                    else if (actor.Unit.DickSize > 5)
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[18 + ((actor.Unit.BodySize) * 3)];
                    else
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[17 + ((actor.Unit.BodySize) * 3)];
                }
                else clothing1.GetSprite = null;

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            }
            else
            {
                if (actor.Unit.DickSize > 0)
                {
                    if (actor.Unit.DickSize < 4)
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[60];
                    else
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[61];
                }
                else clothing1.GetSprite = null;

                if (actor.Unit.HasBreasts)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[0 + actor.Unit.BodySize];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[3 + actor.Unit.BodySize];
                }

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            }
            
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
            if (actor.Unit.Furry)
            {

                if (actor.Unit.DickSize > 0)
                {
                    if (actor.Unit.BodySize > 1)
                    {
                        clothing1.YOffset = -1 * .625f;
                        if (actor.Unit.DickSize < 3)
                            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[33];
                        else if (actor.Unit.DickSize > 5)
                            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[35];
                        else
                            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[34];
                    }
                    else
                    {
                        clothing1.YOffset = 0 * .625f;
                        if (actor.Unit.DickSize < 3)
                            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[33];
                        else if (actor.Unit.DickSize > 5)
                            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[35];
                        else
                            clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[34];
                    }
                }
                else
                {
                    if (actor.Unit.BodySize > 1)
                    {
                        clothing1.YOffset = 0 * .625f;
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[32];
                    }
                    else
                    {
                        clothing1.YOffset = 0 * .625f;
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[31];
                    }
                }

                if (actor.Unit.HasBreasts)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[25 + actor.Unit.BodySize];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[28 + actor.Unit.BodySize];
                }

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            }
            else
            {
                if (actor.Unit.DickSize > 0)
                {
                    if (actor.Unit.DickSize < 4)
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[60];
                    else
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[61];
                }
                else clothing1.GetSprite = null;

                if (actor.Unit.HasBreasts)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[7 + actor.Unit.BodySize];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[10 + actor.Unit.BodySize];
                }

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            }
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
            if (actor.Unit.Furry)
            {
                if (actor.Unit.HasBreasts)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[25 + actor.Unit.BodySize];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[28 + actor.Unit.BodySize];
                }

                if (actor.Unit.BodySize > 1)
                {
                    clothing1.YOffset = -1 * .625f;
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[36];
                }
                else
                {
                    clothing1.YOffset = 0 * .625f;
                    clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[36];
                }

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            }
            else
            {

                if (actor.Unit.DickSize > 0)
                {
                    if (actor.Unit.DickSize < 4)
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[62];
                    else
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[63];
                }
                else clothing1.GetSprite = null;

                if (actor.Unit.HasBreasts)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[20 + actor.Unit.BodySize];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[23 + actor.Unit.BodySize];
                }

            }

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
            if (actor.Unit.Furry)
            {

                if (actor.Unit.BodySize > 1)
                {
                    clothing1.YOffset = -1 * .625f;
                }
                else
                {
                    clothing1.YOffset = 0 * .625f;
                }

                if (actor.Unit.DickSize > 0)
                {
                    if (actor.Unit.DickSize < 3)
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[43];
                    else if (actor.Unit.DickSize > 5)
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[45];
                    else
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[44];
                }
                else clothing1.GetSprite = null;

                if (actor.Unit.HasBreasts)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[37 + actor.Unit.BodySize];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[40 + actor.Unit.BodySize];
                }

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            }
            else
            {
                if (actor.Unit.DickSize > 0)
                {
                    if (actor.Unit.DickSize < 4)
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[60];
                    else
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[61];
                }
                else clothing1.GetSprite = null;

                if (actor.Unit.HasBreasts)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[33 + actor.Unit.BodySize];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[36 + actor.Unit.BodySize];
                }

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            }
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
            if (actor.Unit.Furry)
            {
                if (actor.Unit.BodySize > 1)
                {
                    clothing1.YOffset = -1 * .625f;
                }
                else
                {
                    clothing1.YOffset = 0 * .625f;
                }

                if (actor.Unit.DickSize > 0)
                {
                    if (actor.Unit.DickSize < 3)
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[43];
                    else if (actor.Unit.DickSize > 5)
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[44];
                    else
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[46];
                }
                else clothing1.GetSprite = null;

                if (actor.Unit.HasBreasts)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[136 + actor.Unit.BodySize];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.DemidragonClothes[139 + actor.Unit.BodySize];
                }

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, actor.Unit.ClothingColor);
            }
            else
            {
                if (actor.Unit.DickSize > 0)
                {
                    if (actor.Unit.DickSize < 4)
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[60];
                    else
                        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[61];
                }
                else clothing1.GetSprite = null;

                if (actor.Unit.HasBreasts)
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[46 + actor.Unit.BodySize];
                }
                else
                {
                    clothing2.GetSprite = (s) => State.GameManager.SpriteDictionary.HumenUnderbottoms[49 + actor.Unit.BodySize];
                }

                clothing1.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
                clothing2.GetPalette = (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, actor.Unit.ClothingColor);
            }
            
            base.Configure(sprite, actor);
        }
    }

}
