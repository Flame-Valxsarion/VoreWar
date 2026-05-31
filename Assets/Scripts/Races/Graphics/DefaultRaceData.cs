using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

abstract class DefaultRaceData
{
    internal SpriteExtraInfo Body;
    internal SpriteExtraInfo Head;
    internal SpriteExtraInfo BodyAccessory;
    internal SpriteExtraInfo BodyAccent;
    internal SpriteExtraInfo BodyAccent2;
    internal SpriteExtraInfo BodyAccent3;
    internal SpriteExtraInfo BodyAccent4;
    internal SpriteExtraInfo BodyAccent5;
    internal SpriteExtraInfo BodyAccent6;
    internal SpriteExtraInfo BodyAccent7;
    internal SpriteExtraInfo BodyAccent8;
    internal SpriteExtraInfo BodyAccent9;
    internal SpriteExtraInfo BodyAccent10;
    internal SpriteExtraInfo BodyAccent11;
    internal SpriteExtraInfo BodyAccent12;
    internal SpriteExtraInfo BodyAccent13;
    internal SpriteExtraInfo BodyAccent14;
    internal SpriteExtraInfo BodyAccent15;
    internal SpriteExtraInfo BodyAccent16;
    internal SpriteExtraInfo BodyAccent17;
    internal SpriteExtraInfo BodyAccent18;
    internal SpriteExtraInfo BodyAccent19;
    internal SpriteExtraInfo BodyAccent20;
    internal SpriteExtraInfo Mouth;
    internal SpriteExtraInfo Hair;
    internal SpriteExtraInfo Hair2;
    internal SpriteExtraInfo Hair3;
    internal SpriteExtraInfo Beard;
    internal SpriteExtraInfo Eyes;
    internal SpriteExtraInfo SecondaryEyes;
    internal SpriteExtraInfo SecondaryAccessory;
    internal SpriteExtraInfo Belly;
    internal SpriteExtraInfo SecondaryBelly;
    internal SpriteExtraInfo Weapon;
    /// <summary>
    /// A special sprite intended for having weapons attached to the back when dual wielding, rotates sprite and positions it based on the normal weapon sprites
    /// </summary>
    internal SpriteExtraInfo BackWeapon;
    internal SpriteExtraInfo BodySize;
    internal SpriteExtraInfo Breasts;
    internal SpriteExtraInfo SecondaryBreasts;
    internal SpriteExtraInfo BreastShadow;
    internal SpriteExtraInfo Dick;
    internal SpriteExtraInfo Balls;
    internal SpriteExtraInfo Pussy;
    internal SpriteExtraInfo PussyIn;
    internal SpriteExtraInfo Anus;
    internal SpriteExtraInfo AnusIn;


    public struct RaceFrameList
    {
        public int[] frames;
        public float[] times;

        public RaceFrameList(int[] fra, float[] tim)
        {
            frames = fra;
            times = tim;
        }
    }

    internal int HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.NormalHair);
    internal int HairStyles = 15;
    internal virtual int BreastSizes => Config.AllowHugeBreasts ? State.GameManager.SpriteDictionary.Breasts.Length : State.GameManager.SpriteDictionary.Breasts.Length - 3;
    internal virtual int DickSizes => Config.AllowHugeDicks ? State.GameManager.SpriteDictionary.Dicks.Length : State.GameManager.SpriteDictionary.Dicks.Length - 3;
    internal int SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Skin);
    internal int AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.NormalHair);
    internal int EyeTypes = 8;
    internal int AvoidedEyeTypes = 2;
    internal int EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
    internal int SecondaryEyeColors = 1;
    internal int BodySizes = 5;
    internal int SpecialAccessoryCount = 0;
    internal int BeardStyles = 0;

    internal Vector2 WholeBodyOffset = new Vector2();

    internal int MouthTypes = State.GameManager.SpriteDictionary.Mouths.Length;
    internal int AvoidedMouthTypes = 1;
    /// <summary>Whether a unit has hands, only related to old clothing types</summary>
    internal bool HasHands = true;
    /// <summary>Whether a unit has normal legs, only related to old clothing types</summary>
    internal bool HasNormalLegs = true;

    internal Vector3 ClothingShift = new Vector3(0, 0, 0);
    /// <summary>Whether a unit can spawn as furry, and adds the furry option to the customizer</summary>
    internal bool FurCapable = false;
    internal bool FixedGender = false;
    internal List<Gender> CanBeGender = new List<Gender>() { Gender.Female, Gender.Male, Gender.Hermaphrodite, Gender.Gynomorph, Gender.Maleherm, Gender.Andromorph, Gender.Agenic };

    /// <summary>Whether a unit has the breast vore system, with extended sizes and the two sides being independent.</summary>
    internal bool ExtendedBreastSprites = false;

    /// <summary>Whether a unit uses the gentler version of the stomach wobble (1/2 to 1/3rd the motion)</summary>
    internal bool GentleAnimation = false;

    internal bool BaseBody = false;

    internal bool WeightGainDisabled = false;

    internal int ExtraColors1 = 0;
    internal int ExtraColors2 = 0;
    internal int ExtraColors3 = 0;
    internal int ExtraColors4 = 0;

    internal int HeadTypes = 0;
    internal int TailTypes = 0;
    internal int FurTypes = 0;
    internal int EarTypes = 0;
    internal int BodyAccentTypes1 = 0;
    internal int BodyAccentTypes2 = 0;
    internal int BodyAccentTypes3 = 0;
    internal int BodyAccentTypes4 = 0;
    internal int BodyAccentTypes5 = 0;
    internal int BallsSizes = 0;
    internal int VulvaTypes = 0;
    internal int BasicMeleeWeaponTypes = 1;
    internal int AdvancedMeleeWeaponTypes = 1;
    internal int BasicRangedWeaponTypes = 1;
    internal int AdvancedRangedWeaponTypes = 1;

    /// <summary>The total number of main clothing types plus one additional number for the blank clothing slot</summary>
    internal int MainClothingTypesCount => AllowedMainClothingTypes.Count() + 1;
    /// <summary>The number of clothing slots that are excluded from the random generator</summary>
    internal int AvoidedMainClothingTypes = 3;

    internal int clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
    internal List<MainClothing> AllowedMainClothingTypes = new List<MainClothing>()
    {
        ClothingTypes.BikiniTop,
        ClothingTypes.BeltTop,
        ClothingTypes.StrapTop,
        ClothingTypes.Leotard,
        ClothingTypes.BlackTop,
        ClothingTypes.MalePlate,
        ClothingTypes.FemalePlate,
        ClothingTypes.Rags,
        ClothingTypes.FemaleVillager,
        ClothingTypes.MaleVillager,
    };
    /// <summary>The total number of waist clothing types plus one additional number for the blank clothing slot</summary>
    internal int WaistClothingTypesCount => AllowedWaistTypes.Count() + 1;
    internal List<MainClothing> AllowedWaistTypes = new List<MainClothing>()
    {
        ClothingTypes.BikiniBottom,
        ClothingTypes.Loincloth,
        ClothingTypes.Shorts,
    };
    /// <summary>The total number of hat types plus one additional number for the blank clothing slot</summary>
    internal int ClothingHatTypesCount => AllowedClothingHatTypes.Count() + 1;
    internal List<ClothingAccessory> AllowedClothingHatTypes = new List<ClothingAccessory> { MainAccessories.SantaHat };
    /// <summary>The total number of accessory types plus one additional number for the blank clothing slot</summary>
    internal int ClothingAccessoryTypesCount => AllowedClothingAccessoryTypes.Count() + 1;
    internal List<ClothingAccessory> AllowedClothingAccessoryTypes = new List<ClothingAccessory>();

    //internal int ClothingAccessory2TypesCount => AllowedClothingAccessory2Types.Count() + 1;
    //internal List<ClothingAccessory> AllowedClothingAccessory2Types = new List<ClothingAccessory>();

    internal int ExtraMainClothing1Count => ExtraMainClothing1Types.Count() + 1;
    internal List<MainClothing> ExtraMainClothing1Types = new List<MainClothing>();

    internal int ExtraMainClothing2Count => ExtraMainClothing2Types.Count() + 1;
    internal List<MainClothing> ExtraMainClothing2Types = new List<MainClothing>();

    internal int ExtraMainClothing3Count => ExtraMainClothing3Types.Count() + 1;
    internal List<MainClothing> ExtraMainClothing3Types = new List<MainClothing>();

    internal int ExtraMainClothing4Count => ExtraMainClothing4Types.Count() + 1;
    internal List<MainClothing> ExtraMainClothing4Types = new List<MainClothing>();

    internal int ExtraMainClothing5Count => ExtraMainClothing5Types.Count() + 1;
    internal List<MainClothing> ExtraMainClothing5Types = new List<MainClothing>();

    //internal int ExtraMainClothing6Count => ExtraMainClothing6Types.Count() + 1;
    //internal List<MainClothing> ExtraMainClothing6Types = new List<MainClothing>(); 

    //internal int ExtraMainClothing7Count => ExtraMainClothing7Types.Count() + 1;
    //internal List<MainClothing> ExtraMainClothing7Types = new List<MainClothing>();


    //protected Func<Actor_Unit, ColorSwapPalette> BallsDefaultPalette;

    protected DefaultRaceData()
    {
        Body = new SpriteExtraInfo(2, BodySprite, null, (s) => FurryColor(s));
        Head = new SpriteExtraInfo(4, HeadSprite, null, (s) => FurryColor(s));
        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        BodyAccent = new SpriteExtraInfo(6, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        BodyAccent2 = new SpriteExtraInfo(6, BodyAccentSprite2, WhiteColored);
        BodyAccent3 = new SpriteExtraInfo(7, BodyAccentSprite3, WhiteColored);
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Mouth = new SpriteExtraInfo(5, MouthSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Mouth, s.Unit.SkinColor));
        Hair = new SpriteExtraInfo(6, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(1, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, s.Unit.HairColor));
        Hair3 = null;
        Beard = null;
        Eyes = new SpriteExtraInfo(5, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = new SpriteExtraInfo(1, SecondaryAccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));
        Belly = new SpriteExtraInfo(15, null, null, (s) => FurryBellyColor(s));
        SecondaryBelly = null;
        Weapon = new SpriteExtraInfo(1, WeaponSprite, WhiteColored);
        BackWeapon = new SpriteExtraInfo(0, BackWeaponSprite, WhiteColored);
        BodySize = new SpriteExtraInfo(3, BodySizeSprite, null, (s) => ColorPaletteMap.FurryBellySwap);
        Breasts = new SpriteExtraInfo(16, BreastsSprite, null, (s) => FurryBellyColor(s));
        BreastShadow = null;
        SecondaryBreasts = null;
        Dick = new SpriteExtraInfo(9, DickSprite, null, (s) => FurryColor(s));
        Balls = new SpriteExtraInfo(8, BallsSprite, null, (s) => FurryColor(s));
    }

    protected void AddOffset(SpriteExtraInfo sprite, float xOffset, float yOffset)
    {
        sprite.XOffset += xOffset;
        sprite.YOffset += yOffset;
    }

    internal virtual void SetBaseOffsets(Actor_Unit actor)
    {

    }

    internal virtual void RandomCustom(Unit unit)
    {
        if (BodySizes > 0)
        {
            if (State.RaceSettings.GetOverrideWeight(unit.Race))
            {
                int min = State.RaceSettings.Get(unit.Race).MinWeight;
                int max = State.RaceSettings.Get(unit.Race).MaxWeight;
                unit.BodySize = min + State.Rand.Next(max - min);
                unit.BodySize = Mathf.Clamp(unit.BodySize, 0, BodySizes - 1);
            }
            else
                unit.BodySize = Mathf.Min(Config.DefaultStartingWeight, BodySizes - 1);
        }
        else
            unit.BodySize = 0;

        if (HairStyles == 15)
        {
            if (unit.HasDick && unit.HasBreasts)
            {
                if (Config.HermsOnlyUseFemaleHair)
                    unit.HairStyle = State.Rand.Next(8);
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
                    unit.HairStyle = 8 + State.Rand.Next(7);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(8);
                }
            }
        }
        else //Failsafe incase you forget to override it
        {
            unit.HairStyle = State.Rand.Next(HairStyles);
        }

        unit.HairColor = State.Rand.Next(HairColors);
        unit.AccessoryColor = State.Rand.Next(AccessoryColors);

        if (Config.ExtraRandomHairColors)
        {
            if (HairColors == ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.NormalHair))
            {
                unit.HairColor = State.Rand.Next(HairColors);
            }
            if (AccessoryColors == ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur))
            {
                unit.AccessoryColor = State.Rand.Next(AccessoryColors);
            }
        }
        else
        {
            if (HairColors == ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.NormalHair))
            {
                unit.HairColor = State.Rand.Next(ColorPaletteMap.MixedHairColors);
            }
            if (AccessoryColors == ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur))
            {
                unit.AccessoryColor = State.Rand.Next(ColorPaletteMap.MixedHairColors);
            }
        }

        unit.SkinColor = State.Rand.Next(SkinColors);
        unit.EyeColor = State.Rand.Next(EyeColors);
        unit.ExtraColor1 = State.Rand.Next(ExtraColors1);
        unit.ExtraColor2 = State.Rand.Next(ExtraColors2);
        unit.ExtraColor3 = State.Rand.Next(ExtraColors3);
        unit.ExtraColor4 = State.Rand.Next(ExtraColors4);
        unit.EyeType = State.Rand.Next(Math.Max(EyeTypes - AvoidedEyeTypes, 0));
        unit.ClothingColor = State.Rand.Next(ColorMap.ClothingColorCount);
        unit.MouthType = State.Rand.Next(Math.Max(MouthTypes - AvoidedMouthTypes, 0));
        unit.SpecialAccessoryType = State.Rand.Next(SpecialAccessoryCount);
        unit.BasicMeleeWeaponType = State.Rand.Next(BasicMeleeWeaponTypes);

        if (unit.HasDick && unit.HasBreasts == false)
            unit.BeardStyle = State.Rand.Next(BeardStyles);

        if (ClothingAccessoryTypesCount > 1)
        {
            unit.ClothingAccessoryType = State.Rand.Next(ClothingAccessoryTypesCount);
            for (int i = 0; i < 50; i++)
            {
                if (unit.ClothingAccessoryType > 0)
                {
                    if (AllowedClothingAccessoryTypes[unit.ClothingAccessoryType - 1].CanWear(unit))
                        break;
                }
                unit.ClothingAccessoryType = State.Rand.Next(ClothingAccessoryTypesCount);
            }

            if (unit.ClothingAccessoryType > 0 && AllowedClothingAccessoryTypes[unit.ClothingAccessoryType - 1].CanWear(unit) == false)
                unit.ClothingAccessoryType = 0;
        }

        if (ClothingHatTypesCount > 1)
        {

            if (AllowedClothingHatTypes.Contains(MainAccessories.SantaHat) && Config.WinterActive())
            {
                if (State.Rand.Next(2) == 0)
                {
                    unit.ClothingHatType = 1;
                }
            }
            else
            {
                unit.ClothingHatType = State.Rand.Next(ClothingHatTypesCount);
                for (int i = 0; i < 50; i++)
                {
                    if (unit.ClothingHatType > 0)
                    {
                        if (AllowedClothingHatTypes[unit.ClothingHatType - 1].CanWear(unit))
                            break;
                    }
                    unit.ClothingHatType = State.Rand.Next(ClothingHatTypesCount);
                }

                if (unit.ClothingHatType > 0 && AllowedClothingHatTypes[unit.ClothingHatType - 1].CanWear(unit) == false)
                    unit.ClothingHatType = 0;
            }
            
        }



        if (MainClothingTypesCount > 1)
        {
            float fraction = State.RaceSettings.GetOverrideClothed(unit.Race) ? State.RaceSettings.Get(unit.Race).clothedFraction : Config.ClothedFraction;
            if (State.Rand.NextDouble() < fraction)
            {
                unit.ClothingType = State.Rand.Next(Mathf.Max(MainClothingTypesCount - AvoidedMainClothingTypes, 0));
                for (int i = 0; i < 50; i++)
                {
                    if (unit.ClothingType > 0)
                    {
                        if (AllowedMainClothingTypes[unit.ClothingType - 1].CanWear(unit))
                            break;
                    }
                    unit.ClothingType = State.Rand.Next(Mathf.Max(MainClothingTypesCount - AvoidedMainClothingTypes, 0));
                }
                if (unit.ClothingType > 0 && AllowedMainClothingTypes[unit.ClothingType - 1].CanWear(unit) == false)
                    unit.ClothingType = 0;
                if (WaistClothingTypesCount > 0)
                {
                    unit.ClothingType2 = State.Rand.Next(WaistClothingTypesCount);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingType2 > 0)
                        {
                            if (AllowedWaistTypes[unit.ClothingType2 - 1].CanWear(unit))
                                break;
                        }
                        unit.ClothingType2 = State.Rand.Next(WaistClothingTypesCount);
                    }
                    if (unit.ClothingType2 > 0 && AllowedWaistTypes[unit.ClothingType2 - 1].CanWear(unit) == false)
                        unit.ClothingType2 = 0;
                }
                if (ExtraMainClothing1Count > 0)
                {
                    unit.ClothingExtraType1 = State.Rand.Next(ExtraMainClothing1Count);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingExtraType1 > 0)
                        {
                            if (ExtraMainClothing1Types[unit.ClothingExtraType1 - 1].CanWear(unit))
                                break;
                        }
                        unit.ClothingExtraType1 = State.Rand.Next(ExtraMainClothing1Count);
                    }
                    if (unit.ClothingExtraType1 > 0 && ExtraMainClothing1Types[unit.ClothingExtraType1 - 1].CanWear(unit) == false)
                        unit.ClothingExtraType1 = 0;
                }
                if (ExtraMainClothing2Count > 0)
                {
                    unit.ClothingExtraType2 = State.Rand.Next(ExtraMainClothing2Count);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingExtraType2 > 0)
                        {
                            if (ExtraMainClothing2Types[unit.ClothingExtraType2 - 1].CanWear(unit))
                                break;
                        }
                        unit.ClothingExtraType2 = State.Rand.Next(ExtraMainClothing2Count);
                    }
                    if (unit.ClothingExtraType2 > 0 && ExtraMainClothing2Types[unit.ClothingExtraType2 - 1].CanWear(unit) == false)
                        unit.ClothingExtraType2 = 0;
                }
                if (ExtraMainClothing3Count > 0)
                {
                    unit.ClothingExtraType3 = State.Rand.Next(ExtraMainClothing3Count);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingExtraType3 > 0)
                        {
                            if (ExtraMainClothing3Types[unit.ClothingExtraType3 - 1].CanWear(unit))
                                break;
                        }
                        unit.ClothingExtraType3 = State.Rand.Next(ExtraMainClothing3Count);
                    }
                    if (unit.ClothingExtraType3 > 0 && ExtraMainClothing3Types[unit.ClothingExtraType3 - 1].CanWear(unit) == false)
                        unit.ClothingExtraType3 = 0;
                }
                if (ExtraMainClothing4Count > 0)
                {
                    unit.ClothingExtraType4 = State.Rand.Next(ExtraMainClothing4Count);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingExtraType4 > 0)
                        {
                            if (ExtraMainClothing4Types[unit.ClothingExtraType4 - 1].CanWear(unit))
                                break;
                        }
                        unit.ClothingExtraType4 = State.Rand.Next(ExtraMainClothing4Count);
                    }
                    if (unit.ClothingExtraType4 > 0 && ExtraMainClothing1Types[unit.ClothingExtraType4 - 1].CanWear(unit) == false)
                        unit.ClothingExtraType4 = 0;
                }
                if (ExtraMainClothing5Count > 0)
                {
                    unit.ClothingExtraType5 = State.Rand.Next(ExtraMainClothing5Count);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingExtraType5 > 0)
                        {
                            if (ExtraMainClothing5Types[unit.ClothingExtraType5 - 1].CanWear(unit))
                                break;
                        }
                        unit.ClothingExtraType5 = State.Rand.Next(ExtraMainClothing5Count);
                    }
                    if (unit.ClothingExtraType5 > 0 && ExtraMainClothing5Types[unit.ClothingExtraType5 - 1].CanWear(unit) == false)
                        unit.ClothingExtraType5 = 0;
                }
                if (Config.AllowTopless)
                    if (State.Rand.Next(5) == 0) unit.ClothingType = 0;
                if (unit.Race == Race.Lizards && Config.LizardsHaveNoBreasts)
                {
                    unit.ClothingType = 0;
                }
            }
            else
                unit.ClothingType = 0;
            if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
            {
                unit.ClothingType = 1 + AllowedMainClothingTypes.IndexOf(ClothingTypes.Rags);
                if (unit.ClothingType == 0) //Covers rags not in the list
                {
                    if (AllowedMainClothingTypes.Last()?.ReqWinterHoliday == false) //Avoid bugs where the winter holiday is the last.
                        unit.ClothingType = AllowedMainClothingTypes.Count;
                }
            }
        }
        else
            unit.ClothingType = 0;

        if (FurCapable)
        {
            var raceStats = State.RaceSettings.Get(unit.Race);
            float furryFraction;
            if (raceStats.OverrideFurry)
                furryFraction = raceStats.furryFraction;
            else
                furryFraction = Config.FurryFraction;
            unit.Furry = State.Rand.NextDouble() < furryFraction;
        }
        else
            unit.Furry = false;



        if (unit.HasDick)
        {
            if (State.RaceSettings.GetOverrideDick(unit.Race))
            {
                int min = State.RaceSettings.Get(unit.Race).MinDick;
                int max = State.RaceSettings.Get(unit.Race).MaxDick;
                unit.DickSize = min + State.Rand.Next(max - min);
                unit.DickSize = Mathf.Clamp(unit.DickSize, 0, DickSizes - 1);
            }
            else
                unit.DickSize = Mathf.Clamp(State.Rand.Next(DickSizes) + Config.CockSizeModifier * DickSizes / 6, 0, DickSizes - 1);
        }

        if (unit.HasBreasts)
        {
            if (State.RaceSettings.GetOverrideBreasts(unit.Race))
            {
                int min = State.RaceSettings.Get(unit.Race).MinBoob;
                int max = State.RaceSettings.Get(unit.Race).MaxBoob;
                unit.SetDefaultBreastSize(min + State.Rand.Next(max - min));
                unit.SetDefaultBreastSize(Mathf.Clamp(unit.DefaultBreastSize, 0, BreastSizes - 1));
            }
            else
            {
                if (unit.HasDick)
                    unit.SetDefaultBreastSize(Mathf.Clamp(State.Rand.Next(BreastSizes) + Config.HermBreastSizeModifier * BreastSizes / 6, 0, BreastSizes - 1));
                else
                    unit.SetDefaultBreastSize(Mathf.Clamp(State.Rand.Next(BreastSizes) + Config.BreastSizeModifier * BreastSizes / 6, 0, BreastSizes - 1));
            }
        }

        if (Config.HairMatchesFur && FurCapable)
            unit.HairColor = unit.AccessoryColor;

    }

    ColorSwapPalette FurryColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, actor.Unit.AccessoryColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, actor.Unit.SkinColor);
    }

    ColorSwapPalette FurryBellyColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
            return ColorPaletteMap.FurryBellySwap;
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, actor.Unit.SkinColor);
    }

    protected virtual Color BodyColor(Actor_Unit actor) => ColorMap.GetSkinColor(actor.Unit.SkinColor);
    protected virtual Color HairColor(Actor_Unit actor) => ColorMap.GetHairColor(actor.Unit.HairColor);
    protected virtual Color BodyAccessoryColor(Actor_Unit actor) => ColorMap.GetBodyAccesoryColor(actor.Unit.AccessoryColor);
    protected virtual Color EyeColor(Actor_Unit actor) => ColorMap.GetEyeColor(actor.Unit.EyeColor);
    protected virtual Color ScleraColor(Actor_Unit actor) => Color.white;
    internal virtual Color ClothingColor(Actor_Unit actor) => ColorMap.GetClothingColor(actor.Unit.ClothingColor);

    protected virtual Sprite BodySprite(Actor_Unit actor)
    {
        int attackingOffset = actor.IsAttacking ? 1 : 0;
        if (actor.Unit.BodySize == 0)
            return State.GameManager.SpriteDictionary.Bodies[attackingOffset];
        int GenderOffset = actor.Unit.HasBreasts ? 0 : 8;

        return actor.HasBodyWeight ? State.GameManager.SpriteDictionary.Legs[(actor.Unit.BodySize - 1) * 2 + GenderOffset + attackingOffset] : null;

    }

    protected virtual Sprite HeadSprite(Actor_Unit actor)
    {
        int eatingOffset = actor.IsEating ? 1 : 0;

        if (actor.Unit.Furry)
            return State.GameManager.SpriteDictionary.Bodies[6 + eatingOffset];
        else if (actor.Unit.BreastSize >= 0)
            return State.GameManager.SpriteDictionary.Bodies[2 + eatingOffset];
        else
            return State.GameManager.SpriteDictionary.Bodies[4 + eatingOffset];

    }

    protected virtual Sprite HairSprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Hair[actor.Unit.HairStyle];
    protected virtual Sprite HairSprite2(Actor_Unit actor)
    {
        if (actor.Unit.HairStyle == 1)
            return State.GameManager.SpriteDictionary.Hair[HairStyles];
        else if (actor.Unit.HairStyle == 2)
            return State.GameManager.SpriteDictionary.Hair[HairStyles + 1];
        else if (actor.Unit.HairStyle == 5)
            return State.GameManager.SpriteDictionary.Hair[HairStyles + 3];
        else if (actor.Unit.HairStyle == 6 || actor.Unit.HairStyle == 7)
            return State.GameManager.SpriteDictionary.Hair[HairStyles + 2];
        return null;
    }
    protected virtual Sprite HairSprite3(Actor_Unit actor) => null;

    protected virtual Sprite AccessorySprite(Actor_Unit actor) => null;
    protected virtual Sprite MouthSprite(Actor_Unit actor)
    {
        if (BaseBody)
        {
            if (actor.Unit.HasBreasts)
                AddOffset(Mouth, 0, 0);
            else
                AddOffset(Mouth, 0, -.625f);
        }


        return actor.IsEating == false ? State.GameManager.SpriteDictionary.Mouths[actor.Unit.MouthType] : null;
    }

    protected virtual Sprite SecondaryAccessorySprite(Actor_Unit actor) => null;
    internal virtual Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.SetActive(true);

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize() == 15)
            {
                belly.transform.localScale = new Vector3(1, 1, 1);
                AddOffset(Belly, 0, -30 * .625f);
                return State.GameManager.SpriteDictionary.Bellies[17];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && actor.GetStomachSize() == 15)
            {
                belly.transform.localScale = new Vector3(1, 1, 1);
                AddOffset(Belly, 0, -30 * .625f);
                return State.GameManager.SpriteDictionary.Bellies[16];
            }

            if (actor.PredatorComponent.VisibleFullness > 4)
            {
                float extraCap = actor.PredatorComponent.VisibleFullness - 4;
                float xScale = Mathf.Min(1 + (extraCap / 5), 1.8f);
                float yScale = Mathf.Min(1 + (extraCap / 40), 1.1f);
                belly.transform.localScale = new Vector3(xScale, yScale, 1);
            }
            else
                belly.transform.localScale = new Vector3(1, 1, 1);
            return State.GameManager.SpriteDictionary.Bellies[actor.GetStomachSize()];
        }
        else
        {
            return null;
        }
    }

    protected virtual Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            if (actor.Unit.BasicMeleeWeaponType == 1 && (actor.GetWeaponSprite() == 0 || actor.GetWeaponSprite() == 1))
            {
                    if (actor.GetWeaponSprite() == 0)
                return State.GameManager.SpriteDictionary.Weapons[8];
                    else
                return State.GameManager.SpriteDictionary.Weapons[9];
            }
            return State.GameManager.SpriteDictionary.Weapons[actor.GetWeaponSprite()];
        }
        else
        {
            return null;
        }
    }
    protected virtual Sprite BackWeaponSprite(Actor_Unit actor)
    {
        return null;
    }

    protected virtual Sprite BodySizeSprite(Actor_Unit actor) => actor.Unit.Furry ? State.GameManager.SpriteDictionary.FurryTorsos[Mathf.Clamp(actor.GetBodyWeight(), 0, 3)] : null;

    protected virtual Sprite EyesSprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Eyes[Math.Min(actor.Unit.EyeType, EyeTypes - 1)];
    protected virtual Sprite EyesSecondarySprite(Actor_Unit actor) => null;
    protected virtual Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.SquishedBreasts && actor.Unit.BreastSize >= 3 && actor.Unit.BreastSize <= 6)
            return State.GameManager.SpriteDictionary.SquishedBreasts[actor.Unit.BreastSize - 3];
        return State.GameManager.SpriteDictionary.Breasts[actor.Unit.BreastSize];
    }

    protected virtual Sprite BreastsShadowSprite(Actor_Unit actor) => null;
    protected virtual Sprite BeardSprite(Actor_Unit actor) => null;
    protected virtual Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.Unit.Furry && Config.FurryGenitals)
        {
            if (actor.IsErect() == false)
                return null;
            int type = 0;
            if (actor.IsCockVoring)
                type = 5;
            else
            {
                switch (actor.Unit.Race)
                {
                    case Race.Bunnies:
                        type = 0;
                        break;
                    case Race.Foxes:
                    case Race.Dogs:
                    case Race.Wolves:
                        type = 1;
                        break;
                    case Race.Cats:
                    case Race.Tigers:
                        type = 2;
                        break;
                }
            }

            Dick.GetPalette = null;
            Dick.GetColor = WhiteColored;
            if (actor.PredatorComponent?.VisibleFullness < .50f)
            {
                Dick.layer = 18;
                return State.GameManager.SpriteDictionary.FurryDicks[24 + type];
            }
            else
            {
                Dick.layer = 12;
                return State.GameManager.SpriteDictionary.FurryDicks[30 + type];
            }


        }

        if (Dick.GetPalette == null)
        {
            Dick.GetPalette = (s) => FurryColor(s);
            Dick.GetColor = null;
        }

        //if (actor.IsErect() && !Config.LamiaUseTailAsSecondBelly && actor.Unit.HasTrait(Traits.DualStomach))
        //{
        //    if (actor.PredatorComponent?.CombinedStomachFullness < .50f)
        //    {
        //        Dick.layer = 18;
        //        return State.GameManager.SpriteDictionary.ErectDicks[actor.Unit.DickSize];
        //    }
        //    else
        //    {
        //        Dick.layer = 12;
        //        return State.GameManager.SpriteDictionary.Dicks[actor.Unit.DickSize];
        //    }
        //}

        if (actor.IsErect())
        {
            if (actor.PredatorComponent?.VisibleFullness < .50f)
            {
                Dick.layer = 18;
                return State.GameManager.SpriteDictionary.ErectDicks[actor.Unit.DickSize];
            }
            else
            {
                Dick.layer = 12;
                return State.GameManager.SpriteDictionary.Dicks[actor.Unit.DickSize];
            }
        }

        Dick.layer = 9;
        return State.GameManager.SpriteDictionary.Dicks[actor.Unit.DickSize];
    }
    protected virtual Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        AddOffset(Balls, 0, 0);
        if (actor.Unit.Furry && Config.FurryGenitals)
        {
            int size = actor.Unit.DickSize;
            int offset = actor.GetBallSize(18, .8f);
            if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 18)
            {
                AddOffset(Balls, 0, -23 * .625f);
                return State.GameManager.SpriteDictionary.FurryDicks[42];
            }
            else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && offset == 18)
            {
                AddOffset(Balls, 0, -23 * .625f);
                return State.GameManager.SpriteDictionary.FurryDicks[41];
            }
            else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && offset == 17)
            {
                AddOffset(Balls, 0, -20 * .625f);
                return State.GameManager.SpriteDictionary.FurryDicks[40];
            }
            else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && offset == 16)
            {
                AddOffset(Balls, 0, -19 * .625f);
                return State.GameManager.SpriteDictionary.FurryDicks[39];
            }
            else if (offset >= 15)
            {
                AddOffset(Balls, 0, -16 * .625f);
            }
            else if (offset <= 14 && offset >= 13)
            {
                AddOffset(Balls, 0, -13 * .625f);
            }
            else if (offset == 12)
            {
                AddOffset(Balls, 0, -8 * .625f);
            }
            else if (offset == 11)
            {
                AddOffset(Balls, 0, -5 * .625f);
            }
            else if (offset == 10)
            {
                AddOffset(Balls, 0, -1 * .625f);
            }

            if (offset > 0 && offset <= 12)
                return State.GameManager.SpriteDictionary.FurryDicks[Math.Min(11 + offset, 23)];
            else if (offset > 12)
                return State.GameManager.SpriteDictionary.FurryDicks[Math.Min(23 + offset, 38)];
            return State.GameManager.SpriteDictionary.FurryDicks[size];
        }
        int baseSize = actor.Unit.DickSize / 3;
        int ballOffset = actor.GetBallSize(21, 1);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .9f) == 21)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return State.GameManager.SpriteDictionary.Balls[24];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .9f) == 21)
        {
            AddOffset(Balls, 0, -18 * .625f);
            return State.GameManager.SpriteDictionary.Balls[23];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .9f) == 20)
        {
            AddOffset(Balls, 0, -15 * .625f);
            return State.GameManager.SpriteDictionary.Balls[22];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && actor.GetBallSize(21, .9f) == 19)
        {
            AddOffset(Balls, 0, -14 * .625f);
            return State.GameManager.SpriteDictionary.Balls[21];
        }
        int combined = Math.Min(baseSize + ballOffset + 3, 20);
        if (combined == 21)
            AddOffset(Balls, 0, -14 * .625f);
        else if (combined == 20)
            AddOffset(Balls, 0, -12 * .625f);
        else if (combined >= 17 && combined <= 19)
            AddOffset(Balls, 0, -8 * .625f);
        if (ballOffset > 0)
        {
            return State.GameManager.SpriteDictionary.Balls[combined];
        }

        return State.GameManager.SpriteDictionary.Balls[baseSize];

    }

    protected virtual Sprite BodyAccentSprite(Actor_Unit actor)
    {
        int thinOffset = actor.Unit.BodySize < 2 ? 8 : 0;
        return (Config.FurryHandsAndFeet || actor.Unit.Furry) ? State.GameManager.SpriteDictionary.FurryHandsAndFeet[thinOffset + (actor.IsAttacking ? 1 : 0)] : null;
    }

    protected virtual Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        int thinOffset = actor.Unit.BodySize < 2 ? 8 : 0;
        return (Config.FurryHandsAndFeet || actor.Unit.Furry) ? State.GameManager.SpriteDictionary.FurryHandsAndFeet[2 + thinOffset + (actor.IsAttacking ? 1 : 0)] : null;
    }

    protected virtual Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (Config.FurryFluff == false)
            return null;
        int thinOffset = actor.Unit.BodySize < 2 ? 8 : 0;
        return (Config.FurryHandsAndFeet || actor.Unit.Furry) ? State.GameManager.SpriteDictionary.FurryHandsAndFeet[4 + thinOffset + (actor.IsAttacking ? 1 : 0)] : null;
    }
    protected virtual Sprite BodyAccentSprite4(Actor_Unit actor) => State.GameManager.SpriteDictionary.Eyebrows[Math.Min(actor.Unit.EyeType, State.GameManager.SpriteDictionary.Eyebrows.Length - 1)];

    protected virtual Sprite BodyAccentSprite5(Actor_Unit actor) => null;
    protected virtual Sprite BodyAccentSprite6(Actor_Unit actor) => null;
    protected virtual Sprite BodyAccentSprite7(Actor_Unit actor) => null;
    protected virtual Sprite BodyAccentSprite8(Actor_Unit actor) => null;
    protected virtual Sprite BodyAccentSprite9(Actor_Unit actor) => null;
    protected virtual Sprite BodyAccentSprite10(Actor_Unit actor) => null;
    protected virtual Sprite BodyAccentSprite11(Actor_Unit actor) => null;
    protected virtual Sprite BodyAccentSprite12(Actor_Unit actor) => null;
    protected virtual Sprite BodyAccentSprite13(Actor_Unit actor) => null;
    protected virtual Sprite BodyAccentSprite14(Actor_Unit actor) => null;
    protected virtual Sprite BodyAccentSprite15(Actor_Unit actor) => null;
    protected virtual Sprite BodyAccentSprite16(Actor_Unit actor) => null;
    protected virtual Sprite BodyAccentSprite17(Actor_Unit actor) => null;
    protected virtual Sprite BodyAccentSprite18(Actor_Unit actor) => null;
    protected virtual Sprite BodyAccentSprite19(Actor_Unit actor) => null;
    protected virtual Sprite BodyAccentSprite20(Actor_Unit actor) => null;
    protected virtual Sprite SecondaryBreastsSprite(Actor_Unit actor) => null;
    protected virtual Sprite SecondaryBellySprite(Actor_Unit actor) => null;

    internal virtual void RunFirst(Actor_Unit actor)
    {

    }

    protected static Color WhiteColored(Actor_Unit actor) => Color.white;

}
