using OdinSerializer;
using System.Collections.Generic;

class CustomizerData
{
    [OdinSerialize]
    public Race Race;
    [OdinSerialize]
    public int HairColor;
    [OdinSerialize]
    public int HairStyle;
    [OdinSerialize]
    public int SkinColor;
    [OdinSerialize]
    public int AccessoryColor;
    [OdinSerialize]
    public int EyeColor;
    [OdinSerialize]
    public int EyeType;
    [OdinSerialize]
    public int MouthType;
    [OdinSerialize]
    internal int BodySize;
    [OdinSerialize]
    internal bool BodySizeManuallyChanged;
    [OdinSerialize]
    internal int BreastSize;
    [OdinSerialize]
    internal int DickSize;
    [OdinSerialize]
    internal bool HasVagina;
    [OdinSerialize]
    internal int ClothingType;
    [OdinSerialize]
    internal int ClothingColor;
    [OdinSerialize]
    internal int ClothingColor2;
    [OdinSerialize]
    internal int ClothingColor3;
    [OdinSerialize]
    public string Name { get; set; }
    [OdinSerialize]
    public UnitType Type;
    [OdinSerialize]
    public bool NewGraphics;

    [OdinSerialize]
    public int ExtraColor1;
    [OdinSerialize]
    public int ExtraColor2;
    [OdinSerialize]
    public int ExtraColor3;
    [OdinSerialize]
    public int ExtraColor4;
    [OdinSerialize]
    internal int SpecialAccessoryType;
    [OdinSerialize]
    internal int ClothingType2;
    [OdinSerialize]
    internal bool Furry;

    [OdinSerialize]
    internal int ClothingExtraType1;
    [OdinSerialize]
    internal int ClothingExtraType2;
    [OdinSerialize]
    internal int ClothingExtraType3;
    [OdinSerialize]
    internal int ClothingExtraType4;
    [OdinSerialize]
    internal int ClothingExtraType5;

    [OdinSerialize]
    public int HeadType;
    [OdinSerialize]
    public int TailType;
    [OdinSerialize]
    public int FurType;
    [OdinSerialize]
    public int EarType;
    [OdinSerialize]
    public int BeardStyle;
    [OdinSerialize]
    public int BodyAccentType1;
    [OdinSerialize]
    public int BodyAccentType2;
    [OdinSerialize]
    public int BodyAccentType3;
    [OdinSerialize]
    public int BodyAccentType4;
    [OdinSerialize]
    public int BodyAccentType5;
    [OdinSerialize]
    public int BallsSize;
    [OdinSerialize]
    public int VulvaType;
    [OdinSerialize]
    public int BasicMeleeWeaponType;
    [OdinSerialize]
    public int AdvancedMeleeWeaponType;
    [OdinSerialize]
    public int BasicRangedWeaponType;
    [OdinSerialize]
    public int AdvancedRangedWeaponType;

    [OdinSerialize]
    public List<string> Pronouns;

    public void CopyFromUnit(Unit unit)
    {
        Race = unit.Race;
        HairColor = unit.HairColor;
        HairStyle = unit.HairStyle;
        SkinColor = unit.SkinColor;
        AccessoryColor = unit.AccessoryColor;
        EyeColor = unit.EyeColor;
        EyeType = unit.EyeType;
        MouthType = unit.MouthType;
        BodySize = unit.BodySize;
        BodySizeManuallyChanged = unit.BodySizeManuallyChanged;
        BreastSize = unit.DefaultBreastSize;
        DickSize = unit.DickSize;
        HasVagina = unit.HasVagina;
        ClothingType = unit.ClothingType;
        ClothingColor = unit.ClothingColor;
        ClothingColor2 = unit.ClothingColor2;
        ClothingColor3 = unit.ClothingColor3;

        ClothingExtraType1 = unit.ClothingExtraType1;
        ClothingExtraType2 = unit.ClothingExtraType2;
        ClothingExtraType3 = unit.ClothingExtraType3;
        ClothingExtraType4 = unit.ClothingExtraType4;
        ClothingExtraType5 = unit.ClothingExtraType5;

        Name = unit.Name;
        Type = unit.Type;
        ExtraColor1 = unit.ExtraColor1;
        ExtraColor2 = unit.ExtraColor2;
        ExtraColor3 = unit.ExtraColor3;
        ExtraColor4 = unit.ExtraColor4;
        SpecialAccessoryType = unit.SpecialAccessoryType;
        ClothingType2 = unit.ClothingType2;
        Furry = unit.Furry;
        NewGraphics = Config.NewGraphics;

        HeadType = unit.HeadType;
        TailType = unit.TailType;
        FurType = unit.FurType;
        EarType = unit.EarType;
        BeardStyle = unit.BeardStyle;

        BodyAccentType1 = unit.BodyAccentType1;
        BodyAccentType2 = unit.BodyAccentType2;
        BodyAccentType3 = unit.BodyAccentType3;
        BodyAccentType4 = unit.BodyAccentType4;
        BodyAccentType5 = unit.BodyAccentType5;
        BallsSize = unit.BallsSize;
        VulvaType = unit.VulvaType;
        BasicMeleeWeaponType = unit.BasicMeleeWeaponType;
        AdvancedMeleeWeaponType = unit.AdvancedMeleeWeaponType;
        BasicRangedWeaponType = unit.BasicRangedWeaponType;
        AdvancedRangedWeaponType = unit.AdvancedRangedWeaponType;

        Pronouns = unit.Pronouns;
    }

    public void CopyToUnit(Unit unit, bool includeName)
    {
        //Race = unit.Race;
        //Type = unit.Type;
        if (includeName)
            unit.Name = Name;

        unit.HairColor = HairColor;
        unit.HairStyle = HairStyle;
        unit.SkinColor = SkinColor;
        unit.AccessoryColor = AccessoryColor;
        unit.EyeColor = EyeColor;
        unit.EyeType = EyeType;
        unit.MouthType = MouthType;
        unit.BodySize = BodySize;
        unit.BodySizeManuallyChanged = BodySizeManuallyChanged;
        unit.SetDefaultBreastSize(BreastSize);
        unit.DickSize = DickSize;
        unit.HasVagina = HasVagina;
        unit.ClothingType = ClothingType;
        unit.ClothingColor = ClothingColor;
        unit.ClothingColor2 = ClothingColor2;
        unit.ClothingColor3 = ClothingColor3;
        unit.ExtraColor1 = ExtraColor1;
        unit.ExtraColor2 = ExtraColor2;
        unit.ExtraColor3 = ExtraColor3;
        unit.ExtraColor4 = ExtraColor4;
        unit.SpecialAccessoryType = SpecialAccessoryType;
        unit.ClothingType2 = ClothingType2;
        unit.Furry = Furry;

        unit.ClothingExtraType1 = ClothingExtraType1;
        unit.ClothingExtraType2 = ClothingExtraType2;
        unit.ClothingExtraType3 = ClothingExtraType3;
        unit.ClothingExtraType4 = ClothingExtraType4;
        unit.ClothingExtraType5 = ClothingExtraType5;

        unit.HeadType = HeadType;
        unit.TailType = TailType;
        unit.FurType = FurType;
        unit.EarType = EarType;
        unit.BeardStyle = BeardStyle;

        unit.BodyAccentType1 = BodyAccentType1;
        unit.BodyAccentType2 = BodyAccentType2;
        unit.BodyAccentType3 = BodyAccentType3;
        unit.BodyAccentType4 = BodyAccentType4;
        unit.BodyAccentType5 = BodyAccentType5;
        unit.BallsSize = BallsSize;
        unit.VulvaType = VulvaType;
        unit.BasicMeleeWeaponType = BasicMeleeWeaponType;
        unit.AdvancedMeleeWeaponType = AdvancedMeleeWeaponType;
        unit.BasicRangedWeaponType = BasicRangedWeaponType;
        unit.AdvancedRangedWeaponType = AdvancedRangedWeaponType;

        unit.Pronouns = Pronouns;

        var race = Races.GetRace(unit);
        if (HairColor >= race.HairColors) unit.HairColor = 0;
        if (HairStyle >= race.HairStyles) unit.HairStyle = 0;
        if (SkinColor >= race.SkinColors) unit.SkinColor = 0;
        if (AccessoryColor >= race.AccessoryColors) unit.AccessoryColor = 0;
        if (EyeColor >= race.EyeColors) unit.EyeColor = 0;
        if (EyeType >= race.EyeTypes) unit.EyeType = 0;
        if (BodySize >= race.BodySizes) unit.BodySize = 0;
        if (BreastSize >= race.BreastSizes) unit.SetDefaultBreastSize(0);
        if (DickSize >= race.DickSizes) unit.DickSize = 0;
        if (ClothingType > race.AllowedMainClothingTypes.Count) unit.ClothingType = 0;
        if (ClothingType2 > race.AllowedWaistTypes.Count) unit.ClothingType2 = 0;
        if (ClothingExtraType1 > race.ExtraMainClothing1Types.Count) unit.ClothingExtraType1 = 0;
        if (ClothingExtraType2 > race.ExtraMainClothing2Types.Count) unit.ClothingExtraType2 = 0;
        if (ClothingExtraType3 > race.ExtraMainClothing3Types.Count) unit.ClothingExtraType3 = 0;
        if (ClothingExtraType4 > race.ExtraMainClothing4Types.Count) unit.ClothingExtraType4 = 0;
        if (ClothingExtraType5 > race.ExtraMainClothing5Types.Count) unit.ClothingExtraType5 = 0;
        if (HeadType > race.HeadTypes) unit.HeadType = 0;




    }
}

