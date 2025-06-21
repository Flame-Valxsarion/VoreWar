using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ColorPaletteMap
{
    internal enum SwapType
    {
        NormalHair,
        HairRedKeyStrict,
        WildHair,
        UniversalHair,
        Fur,
        FurStrict,
        Skin,
        RedSkin,
        RedFur,
        Mouth,
        EyeColor,
        LizardMain,
        LizardLight,
        SlimeMain,
        SlimeSub,
        Imp,
        ImpDark,
        ImpRedKey,
        OldImp,
        OldImpDark,
        Goblins,
        CrypterWeapon,
        Clothing,
        ClothingStrict,
        ClothingStrictRedKey,
        Clothing50Spaced,
        SkinToClothing,
        Kangaroo,
        FeralWolfMane,
        FeralWolfFur,
        Alligator,
        Crux,
        BeeNewSkin,
        DriderSkin,
        DriderEyes,
        AlrauneSkin,
        AlrauneHair,
        AlrauneFoliage,
        DemibatSkin,
        DemibatHumanSkin,
        MermenSkin,
        MermenHair,
        AviansSkin,
        DemiantSkin,
        DemifrogSkin,
        SharkSkin,
        DeerSkin,
        DeerLeaf,
        SharkReversed,
        Puca,
        PucaBalls,
        Hamsters,
        HamstersBalls,
        HippoSkin,
        ViperSkin,
        KomodosSkin,
        KomodosReversed,
        CockatriceSkin,
        Harvester,
        Bat,
        Kobold,
        Frog,
        Dragon,
        Dragonfly,
        FairySpringSkin,
        FairySpringClothes,
        FairySummerSkin,
        FairySummerClothes,
        FairyFallSkin,
        FairyFallClothes,
        FairyWinterSkin,
        FairyWinterClothes,
        Ant,
        GryphonSkin,
        SlugSkin,
        PantherSkin,
        PantherHair,
        PantherBodyPaint,
        PantherClothes,
        SalamanderSkin,
        MantisSkin,
        EasternDragon,
        CatfishSkin,
        GazelleSkin,
        EarthwormSkin,
        HorseSkin,
        TerrorbirdSkin,
        VargulSkin,
        FeralLionsFur,
        FeralLionsEyes,
        FeralLionsMane,
	    GoodraSkin,
        AabayxSkin,
        MiceSkin,
        MiceHumanSkin,
        FeralHorseSkin,
        WyvernMatron,
        FeralFoxSkin,
        TerminidSkin,
        FeralOrcaSkin,
        OtachiSkin,
        GnollSkin,
        UmbreonSkin,
        UmbreonExt,
        UmbreonClothes,
        UmbreonArmor,
        EeveeEqualeonSkin,
        EeveeEqualeonExt,
        EeveeEqualeonClothing,
        EqualeonEyes,
        PlantSkin,
        ViiselSkin,
        LupineSkin,
        LupineReversed,
    }

    static Dictionary<SwapType, List<ColorSwapPalette>> Swaps;

    internal static ColorSwapPalette Default;

    static List<Color> SlimeBaseColor;
    static List<Color> ClothingBaseColor;

    internal static int MixedHairColors;

    internal static ColorSwapPalette FurryBellySwap;

    internal static ColorSwapPalette GetPalette(SwapType swap, int index)
    {
        Swaps.TryGetValue(swap, out var list);
        if (list == null)
            return Default;
        if (index < list.Count)
            return list[index];
        return list[0];
    }

    internal static int GetPaletteCount(SwapType swap)
    {
        Swaps.TryGetValue(swap, out var list);
        if (list == null)
            return 0;
        return list.Count;
    }

    /// <summary>
    /// Used to get the base slime color for slime scat
    /// </summary>
    internal static Color GetSlimeBaseColor(int index)
    {
        return SlimeBaseColor[index];
    }

    internal static Color GetClothingBaseColor(int index)
    {
        return ClothingBaseColor[index];
    }


    static ColorPaletteMap()
    {
        Default = new ColorSwapPalette(new Dictionary<int, Color>());

        Swaps = new Dictionary<SwapType, List<ColorSwapPalette>>();

        List<ColorSwapPalette> NormalHairSwaps = WireUp(SwapType.NormalHair);
        List<ColorSwapPalette> HairRedKeyStrictSwaps = WireUp(SwapType.HairRedKeyStrict);
        List<ColorSwapPalette> ClothingSwaps = WireUp(SwapType.Clothing);
        List<ColorSwapPalette> Clothing50SpacedSwaps = WireUp(SwapType.Clothing50Spaced);
        List<ColorSwapPalette> ClothingSwapsStrict = WireUp(SwapType.ClothingStrict);
        List<ColorSwapPalette> ClothingSwapsStrictRedKey = WireUp(SwapType.ClothingStrictRedKey);
        List<ColorSwapPalette> FurSwaps = WireUp(SwapType.Fur);
        List<ColorSwapPalette> FurStrictSwaps = WireUp(SwapType.FurStrict);
        List<ColorSwapPalette> WildHairSwaps = WireUp(SwapType.WildHair);
        List<ColorSwapPalette> UniversalHairSwaps = WireUp(SwapType.UniversalHair);
        List<ColorSwapPalette> SkinColorSwaps = WireUp(SwapType.Skin);
        List<ColorSwapPalette> RedFurColorSwaps = WireUp(SwapType.RedFur);
        List<ColorSwapPalette> RedSkinColorSwaps = WireUp(SwapType.RedSkin);
        List<ColorSwapPalette> SkinToClothingSwaps = WireUp(SwapType.SkinToClothing);
        List<ColorSwapPalette> MouthColorSwaps = WireUp(SwapType.Mouth);
        List<ColorSwapPalette> EyeColorSwaps = WireUp(SwapType.EyeColor);
        List<ColorSwapPalette> LizardMainSwaps = WireUp(SwapType.LizardMain);
        List<ColorSwapPalette> LizardLightSwaps = WireUp(SwapType.LizardLight);
        List<ColorSwapPalette> SlimeMainSwaps = WireUp(SwapType.SlimeMain);
        List<ColorSwapPalette> SlimeSubPalettes = WireUp(SwapType.SlimeSub);
        List<ColorSwapPalette> ImpSwaps = WireUp(SwapType.Imp);
        List<ColorSwapPalette> ImpRedKey = WireUp(SwapType.ImpRedKey);
        List<ColorSwapPalette> ImpDarkSwaps = WireUp(SwapType.ImpDark);
        List<ColorSwapPalette> OldImpSwaps = WireUp(SwapType.OldImp);
        List<ColorSwapPalette> OldImpDarkSwaps = WireUp(SwapType.OldImpDark);
        List<ColorSwapPalette> GoblinSwaps = WireUp(SwapType.Goblins);
        List<ColorSwapPalette> KangarooSwaps = WireUp(SwapType.Kangaroo);
        List<ColorSwapPalette> FeralWolfMane = WireUp(SwapType.FeralWolfMane);
        List<ColorSwapPalette> FeralWolfFur = WireUp(SwapType.FeralWolfFur);
        List<ColorSwapPalette> AlligatorSwaps = WireUp(SwapType.Alligator);
        List<ColorSwapPalette> CruxSwaps = WireUp(SwapType.Crux);
        List<ColorSwapPalette> BeeNewSkinSwaps = WireUp(SwapType.BeeNewSkin);
        List<ColorSwapPalette> DriderSkinSwaps = WireUp(SwapType.DriderSkin);
        List<ColorSwapPalette> DriderEyesSwaps = WireUp(SwapType.DriderEyes);
        List<ColorSwapPalette> AlrauneSkinSwaps = WireUp(SwapType.AlrauneSkin);
        List<ColorSwapPalette> AlrauneHairSwaps = WireUp(SwapType.AlrauneHair);
        List<ColorSwapPalette> AlrauneFoliageSwaps = WireUp(SwapType.AlrauneFoliage);
        List<ColorSwapPalette> DemibatSkinSwaps = WireUp(SwapType.DemibatSkin);
        List<ColorSwapPalette> DemibatHumanSkinSwaps = WireUp(SwapType.DemibatHumanSkin);
        List<ColorSwapPalette> MermenSkinSwaps = WireUp(SwapType.MermenSkin);
        List<ColorSwapPalette> MermenHairSwaps = WireUp(SwapType.MermenHair);
        List<ColorSwapPalette> AviansSkinSwaps = WireUp(SwapType.AviansSkin);
        List<ColorSwapPalette> DemiantSkinSwaps = WireUp(SwapType.DemiantSkin);
        List<ColorSwapPalette> DemifrogSkinSwaps = WireUp(SwapType.DemifrogSkin);
        List<ColorSwapPalette> SharkSkinSwaps = WireUp(SwapType.SharkSkin);
        List<ColorSwapPalette> SharkReversedSwaps = WireUp(SwapType.SharkReversed);
        List<ColorSwapPalette> DeerSkinSwaps = WireUp(SwapType.DeerSkin);
        List<ColorSwapPalette> DeerLeafSwaps = WireUp(SwapType.DeerLeaf);
        List<ColorSwapPalette> PucaSwaps = WireUp(SwapType.Puca);
        List<ColorSwapPalette> PucaBallSwaps = WireUp(SwapType.PucaBalls);
        List<ColorSwapPalette> HamstersSwaps = WireUp(SwapType.Hamsters);
        List<ColorSwapPalette> HamstersBallSwaps = WireUp(SwapType.HamstersBalls);
        List<ColorSwapPalette> HippoSkinSwaps = WireUp(SwapType.HippoSkin);
        List<ColorSwapPalette> ViperSkinSwaps = WireUp(SwapType.ViperSkin);
        List<ColorSwapPalette> KomodosSkinSwaps = WireUp(SwapType.KomodosSkin);
        List<ColorSwapPalette> KomodosReversedSwaps = WireUp(SwapType.KomodosReversed);
        List<ColorSwapPalette> CockatriceSkinSwaps = WireUp(SwapType.CockatriceSkin);
        List<ColorSwapPalette> HarvesterSwaps = WireUp(SwapType.Harvester);
        List<ColorSwapPalette> CrypterWeaponSwap = WireUp(SwapType.CrypterWeapon);
        List<ColorSwapPalette> BatSwaps = WireUp(SwapType.Bat);
        List<ColorSwapPalette> KoboldSwaps = WireUp(SwapType.Kobold);
        List<ColorSwapPalette> FrogSwaps = WireUp(SwapType.Frog);
        List<ColorSwapPalette> DragonSwaps = WireUp(SwapType.Dragon);
        List<ColorSwapPalette> DragonflySwaps = WireUp(SwapType.Dragonfly);
        List<ColorSwapPalette> FairySpringSkin = WireUp(SwapType.FairySpringSkin);
        List<ColorSwapPalette> FairySpringClothes = WireUp(SwapType.FairySpringClothes);
        List<ColorSwapPalette> FairySummerSkin = WireUp(SwapType.FairySummerSkin);
        List<ColorSwapPalette> FairySummerClothes = WireUp(SwapType.FairySummerClothes);
        List<ColorSwapPalette> FairyFallSkin = WireUp(SwapType.FairyFallSkin);
        List<ColorSwapPalette> FairyFallClothes = WireUp(SwapType.FairyFallClothes);
        List<ColorSwapPalette> FairyWinterSkin = WireUp(SwapType.FairyWinterSkin);
        List<ColorSwapPalette> FairyWinterClothes = WireUp(SwapType.FairyWinterClothes);
        List<ColorSwapPalette> AntSwaps = WireUp(SwapType.Ant);
        List<ColorSwapPalette> GryphonSkinSwaps = WireUp(SwapType.GryphonSkin);
        List<ColorSwapPalette> SlugSkinSwaps = WireUp(SwapType.SlugSkin);
        List<ColorSwapPalette> PantherSkinSwaps = WireUp(SwapType.PantherSkin);
        List<ColorSwapPalette> PantherHairSwaps = WireUp(SwapType.PantherHair);
        List<ColorSwapPalette> PantherBodyPaintSwaps = WireUp(SwapType.PantherBodyPaint);
        List<ColorSwapPalette> PantherClothesSwaps = WireUp(SwapType.PantherClothes);
        List<ColorSwapPalette> SalamanderSkinSwaps = WireUp(SwapType.SalamanderSkin);
        List<ColorSwapPalette> MantisSkinSwaps = WireUp(SwapType.MantisSkin);
        List<ColorSwapPalette> EasternDragon = WireUp(SwapType.EasternDragon);
        List<ColorSwapPalette> CatfishSkinSwaps = WireUp(SwapType.CatfishSkin);
        List<ColorSwapPalette> GazelleSkinSwaps = WireUp(SwapType.GazelleSkin);
        List<ColorSwapPalette> EarthwormSkinSwaps = WireUp(SwapType.EarthwormSkin);
        List<ColorSwapPalette> HorseSkinSwaps = WireUp(SwapType.HorseSkin);
        List<ColorSwapPalette> TerrorbirdSkinSwaps = WireUp(SwapType.TerrorbirdSkin);
        List<ColorSwapPalette> VargulSkinSwaps = WireUp(SwapType.VargulSkin);
        List<ColorSwapPalette> FeralLionsFurSwaps = WireUp(SwapType.FeralLionsFur);
        List<ColorSwapPalette> FeralLionsEyesSwaps = WireUp(SwapType.FeralLionsEyes);
        List<ColorSwapPalette> FeralLionsManeSwaps = WireUp(SwapType.FeralLionsMane);
	    List<ColorSwapPalette> GoodraSkinSwaps = WireUp(SwapType.GoodraSkin);
        List<ColorSwapPalette> AabayxSkinSwaps = WireUp(SwapType.AabayxSkin);
        List<ColorSwapPalette> MiceSkinSwaps = WireUp(SwapType.MiceSkin);
        List<ColorSwapPalette> MiceHumanSkinSwaps = WireUp(SwapType.MiceHumanSkin);
        List<ColorSwapPalette> LupineSkinSwaps = WireUp(SwapType.LupineSkin);
        List<ColorSwapPalette> LupineReverseSwaps = WireUp(SwapType.LupineReversed);

        List<ColorSwapPalette> FeralHorseSkinSwaps = WireUp(SwapType.FeralHorseSkin);
        List<ColorSwapPalette> WyvernMatronSwaps = WireUp(SwapType.WyvernMatron);
        List<ColorSwapPalette> FeralFoxSkinSwaps = WireUp(SwapType.FeralFoxSkin);
        List<ColorSwapPalette> TerminidSkinSwaps = WireUp(SwapType.TerminidSkin);
        List<ColorSwapPalette> FeralOrcaSkinSwaps = WireUp(SwapType.FeralOrcaSkin);
        List<ColorSwapPalette> OtachiSkinSwaps = WireUp(SwapType.OtachiSkin);
        List<ColorSwapPalette> GnollSkinSwaps = WireUp(SwapType.GnollSkin);
        List<ColorSwapPalette> UmbreonSkinSwaps = WireUp(SwapType.UmbreonSkin);
        List<ColorSwapPalette> UmbreonExtSwaps = WireUp(SwapType.UmbreonExt);
        List<ColorSwapPalette> UmbreonClothesSwaps = WireUp(SwapType.UmbreonClothes);
        List<ColorSwapPalette> UmbreonArmorSwaps = WireUp(SwapType.UmbreonArmor);
        List<ColorSwapPalette> EeveeEqualeonSkinSwaps = WireUp(SwapType.EeveeEqualeonSkin);
        List<ColorSwapPalette> EeveeEqualeonExtSwaps = WireUp(SwapType.EeveeEqualeonExt);
        List<ColorSwapPalette> EeveeEqualeonClothingSwaps = WireUp(SwapType.EeveeEqualeonClothing);
        List<ColorSwapPalette> EqualeonEyesSwaps = WireUp(SwapType.EqualeonEyes);
        List<ColorSwapPalette> PlantSkinSwaps = WireUp(SwapType.PlantSkin);
        List<ColorSwapPalette> ViiselSkinSwaps = WireUp(SwapType.ViiselSkin);

        int[] NormalIndexes = { 81, 153, 198, 229, 255 };
        Texture2D map = State.GameManager.PaletteDictionary.SimpleHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(NormalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            NormalHairSwaps.Add(swap);
            swapDict = RedReversed(map, pixelY);
            swap = new ColorSwapPalette(swapDict, null, 3);
            HairRedKeyStrictSwaps.Add(swap);
            MixedHairColors = NormalHairSwaps.Count();
        }
        map = State.GameManager.PaletteDictionary.WildHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(NormalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            NormalHairSwaps.Add(swap);
            swapDict = RedReversed(map, pixelY);
            swap = new ColorSwapPalette(swapDict, null, 3);
            HairRedKeyStrictSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.SimpleHair;
        bool[] clear = new bool[256];
        clear[84] = true;
        clear[142] = true;
        clear[158] = true;
        clear[196] = true;
        clear[203] = true;
        clear[254] = true;
        clear[0] = true;
        clear[50] = true;
        clear[100] = true;
        clear[150] = true;
        clear[200] = true;
        clear[250] = true;

        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(NormalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, clear);
            FurSwaps.Add(swap);
        }
        map = State.GameManager.PaletteDictionary.WildHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(NormalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, clear);
            FurSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.SimpleHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(NormalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, null, 2);
            FurStrictSwaps.Add(swap);
        }
        map = State.GameManager.PaletteDictionary.WildHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(NormalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, null, 2);
            FurStrictSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.WildHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(NormalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            WildHairSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.UniversalHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            UniversalHairSwaps.Add(swap);
        }
        map = State.GameManager.PaletteDictionary.Skin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(NormalIndexes, map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            SkinColorSwaps.Add(swap);
            clear = new bool[256];
            clear[84] = true;
            clear[142] = true;
            clear[196] = true;
            clear[255] = true;
            swap = new ColorSwapPalette(swapDict, clear);
            MouthColorSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Skin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            RedSkinColorSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.SimpleHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            RedFurColorSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Skin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            RedSkinColorSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Eyes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = Color.clear,
                [NormalIndexes[0]] = map.GetPixel(3, pixelY),
                [NormalIndexes[1]] = map.GetPixel(2, pixelY),
                [NormalIndexes[2]] = map.GetPixel(1, pixelY),
                [NormalIndexes[3]] = map.GetPixel(0, pixelY),
                [NormalIndexes[4]] = Color.clear,
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            EyeColorSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Lizards;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = NormalReversed(NormalIndexes, map, pixelY);
            clear = new bool[256];
            clear[84] = true;
            clear[142] = true;
            clear[196] = true;
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, clear);
            LizardMainSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Lizards;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [NormalIndexes[0]] = map.GetPixel(9, pixelY),
                [NormalIndexes[1]] = map.GetPixel(8, pixelY),
                [NormalIndexes[2]] = map.GetPixel(7, pixelY),
                [NormalIndexes[3]] = map.GetPixel(6, pixelY),
                [NormalIndexes[4]] = map.GetPixel(5, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            LizardLightSwaps.Add(swap);
        }


        map = State.GameManager.PaletteDictionary.Lizards;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [63] = map.GetPixel(8, pixelY),
                [91] = map.GetPixel(7, pixelY),
                [95] = map.GetPixel(5, pixelY),
                [99] = map.GetPixel(6, pixelY),
                [152] = map.GetPixel(4, pixelY),
                [225] = map.GetPixel(3, pixelY),
                [237] = map.GetPixel(2, pixelY),
                [245] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, null, 2);
            KoboldSwaps.Add(swap);

        }

        map = State.GameManager.PaletteDictionary.Lizards;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [152] = map.GetPixel(4, pixelY),
                [215] = map.GetPixel(6, pixelY),
                [225] = map.GetPixel(3, pixelY),
                [237] = map.GetPixel(2, pixelY),
                [242] = map.GetPixel(5, pixelY),
                [245] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            clear = new bool[256];
            clear[246] = true;
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, clear, 2);
            DragonSwaps.Add(swap);

        }

        map = State.GameManager.PaletteDictionary.Lizards;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            if (pixelY == 0) //Skip the peachy one
                continue;
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(4, pixelY),
                [158] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            FrogSwaps.Add(swap);
        }

        SlimeBaseColor = new List<Color>();
        map = State.GameManager.PaletteDictionary.Slimes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [NormalIndexes[0]] = map.GetPixel(0, pixelY),
                [NormalIndexes[1]] = map.GetPixel(1, pixelY),
                [NormalIndexes[2]] = map.GetPixel(2, pixelY),
                [NormalIndexes[3]] = map.GetPixel(3, pixelY),
                [NormalIndexes[4]] = map.GetPixel(4, pixelY),
            };
            Dictionary<int, Color> subSwapDict1 = new Dictionary<int, Color>
            {
                [NormalIndexes[0]] = map.GetPixel(5, pixelY),
                [NormalIndexes[1]] = map.GetPixel(6, pixelY),
                [NormalIndexes[2]] = map.GetPixel(7, pixelY),
                [NormalIndexes[3]] = map.GetPixel(8, pixelY),
                [NormalIndexes[4]] = map.GetPixel(9, pixelY),
            };
            Dictionary<int, Color> subSwapDict2 = new Dictionary<int, Color>
            {
                [NormalIndexes[0]] = map.GetPixel(10, pixelY),
                [NormalIndexes[1]] = map.GetPixel(11, pixelY),
                [NormalIndexes[2]] = map.GetPixel(12, pixelY),
                [NormalIndexes[3]] = map.GetPixel(13, pixelY),
                [NormalIndexes[4]] = map.GetPixel(14, pixelY),
            };
            Dictionary<int, Color> subSwapDict3 = new Dictionary<int, Color>
            {
                [NormalIndexes[0]] = map.GetPixel(15, pixelY),
                [NormalIndexes[1]] = map.GetPixel(16, pixelY),
                [NormalIndexes[2]] = map.GetPixel(17, pixelY),
                [NormalIndexes[3]] = map.GetPixel(18, pixelY),
                [NormalIndexes[4]] = map.GetPixel(19, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            ColorSwapPalette swap2 = new ColorSwapPalette(subSwapDict1);
            ColorSwapPalette swap3 = new ColorSwapPalette(subSwapDict2);
            ColorSwapPalette swap4 = new ColorSwapPalette(subSwapDict3);
            SlimeMainSwaps.Add(swap);
            SlimeSubPalettes.Add(swap2);
            SlimeSubPalettes.Add(swap3);
            SlimeSubPalettes.Add(swap4);
            SlimeBaseColor.Add(map.GetPixel(3, pixelY));
        }

        map = State.GameManager.PaletteDictionary.Imps;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            clear = new bool[256];
            clear[232] = true;
            clear[239] = true;
            clear[251] = true;
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, null, 0);
            ImpSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Imps;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [81] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            OldImpSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.ImpsDark;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            ImpDarkSwaps.Add(swap);
            swapDict = RedReversed(map, pixelY);
            swap = new ColorSwapPalette(swapDict);
            ImpRedKey.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.ImpsDark;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [81] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            OldImpDarkSwaps.Add(swap);
        }

        {
            map = State.GameManager.PaletteDictionary.FurryBelly;
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [NormalIndexes[0]] = map.GetPixel(4, 0),
                [NormalIndexes[1]] = map.GetPixel(3, 0),
                [NormalIndexes[2]] = map.GetPixel(2, 0),
                [NormalIndexes[3]] = map.GetPixel(1, 0),
                [NormalIndexes[4]] = map.GetPixel(0, 0),
            };
            FurryBellySwap = new ColorSwapPalette(swapDict);
        }

        map = State.GameManager.PaletteDictionary.FairySkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = RedReversed(map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, null, 2);
            if (pixelY < 2) FairySpringSkin.Add(swap);
            else if (pixelY < 5) FairySummerSkin.Add(swap);
            else if (pixelY < 8) FairyFallSkin.Add(swap);
            else FairyWinterSkin.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.FairyClothes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [123] = map.GetPixel(3, pixelY),
                [189] = map.GetPixel(2, pixelY),
                [233] = map.GetPixel(1, pixelY),
                [244] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, null, 2);
            if (pixelY < 5) FairyWinterClothes.Add(swap);
            else if (pixelY < 10) FairyFallClothes.Add(swap);
            else if (pixelY < 14) FairySummerClothes.Add(swap);
            else FairySpringClothes.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Goblins;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            GoblinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Lizards;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [152] = map.GetPixel(4, pixelY),
                [225] = map.GetPixel(2, pixelY),
                [237] = map.GetPixel(1, pixelY),
                [245] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, null, 3);
            CrypterWeaponSwap.Add(swap);
        }

        ClothingBaseColor = new List<Color>();
        map = State.GameManager.PaletteDictionary.SimpleHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = RedReversed(map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            ClothingBaseColor.Add(map.GetPixel(1, pixelY));
            ClothingSwaps.Add(swap);
            swap = new ColorSwapPalette(swapDict, null, 1);
            ClothingSwapsStrict.Add(swap);
            swapDict = RedReversed(map, pixelY);
            clear = new bool[256];
            clear[251] = true; //This is to avoid the succbus yellow buckles that are 251
            swap = new ColorSwapPalette(swapDict, clear, 1);
            ClothingSwapsStrictRedKey.Add(swap);
            swapDict = NormalReversed(NormalIndexes, map, pixelY);
            swap = new ColorSwapPalette(swapDict);
            SkinToClothingSwaps.Add(swap);
            swapDict = new Dictionary<int, Color>()
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            swap = new ColorSwapPalette(swapDict, null, 0);
            Clothing50SpacedSwaps.Add(swap);
        }
        map = State.GameManager.PaletteDictionary.Lizards;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = RedReversed(map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            ClothingBaseColor.Add(map.GetPixel(1, pixelY));
            ClothingSwaps.Add(swap);
            swap = new ColorSwapPalette(swapDict, null, 1);
            ClothingSwapsStrict.Add(swap);
            swapDict = RedReversed(map, pixelY);
            swap = new ColorSwapPalette(swapDict, clear, 1);
            ClothingSwapsStrictRedKey.Add(swap);
            swapDict = NormalReversed(NormalIndexes, map, pixelY);
            swap = new ColorSwapPalette(swapDict);
            SkinToClothingSwaps.Add(swap);
            swapDict = new Dictionary<int, Color>()
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            swap = new ColorSwapPalette(swapDict, null, 0);
            Clothing50SpacedSwaps.Add(swap);
        }
        map = State.GameManager.PaletteDictionary.Slimes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = Red(map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            ClothingBaseColor.Add(map.GetPixel(1, pixelY));
            ClothingSwaps.Add(swap);
            swap = new ColorSwapPalette(swapDict, null, 1);
            ClothingSwapsStrict.Add(swap);
            swapDict = Red(map, pixelY);
            swap = new ColorSwapPalette(swapDict, clear, 1);
            ClothingSwapsStrictRedKey.Add(swap);
            swapDict = Normal(NormalIndexes, map, pixelY);
            swap = new ColorSwapPalette(swapDict);
            SkinToClothingSwaps.Add(swap);
            swapDict = new Dictionary<int, Color>()
            {
                [50] = map.GetPixel(0, pixelY),
                [100] = map.GetPixel(1, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(3, pixelY),
                [250] = map.GetPixel(4, pixelY),
            };
            swap = new ColorSwapPalette(swapDict, null, 0);
            Clothing50SpacedSwaps.Add(swap);
        }
        map = State.GameManager.PaletteDictionary.WildHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = RedReversed(map, pixelY);
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            ClothingBaseColor.Add(map.GetPixel(1, pixelY));
            ClothingSwaps.Add(swap);
            swap = new ColorSwapPalette(swapDict, null, 1);
            ClothingSwapsStrict.Add(swap);
            swapDict = RedReversed(map, pixelY);
            swap = new ColorSwapPalette(swapDict, clear, 1);
            ClothingSwapsStrictRedKey.Add(swap);
            swapDict = NormalReversed(NormalIndexes, map, pixelY);
            swap = new ColorSwapPalette(swapDict);
            SkinToClothingSwaps.Add(swap);
            swapDict = new Dictionary<int, Color>()
            {
                [50] = map.GetPixel(4, pixelY),
                [100] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            swap = new ColorSwapPalette(swapDict, null, 0);
            Clothing50SpacedSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Kangaroo;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [81] = map.GetPixel(10, pixelY),
                [89] = map.GetPixel(5, pixelY),
                [102] = map.GetPixel(9, pixelY),
                [128] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(8, pixelY),
                [168] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(7, pixelY),
                [217] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(6, pixelY),
                [242] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            KangarooSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.FeralWolfMane;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [91] = map.GetPixel(2, pixelY),
                [95] = map.GetPixel(0, pixelY),
                [99] = map.GetPixel(1, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 4);
            FeralWolfMane.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.FeralWolfFur;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [152] = map.GetPixel(3, pixelY),
                [225] = map.GetPixel(2, pixelY),
                [236] = map.GetPixel(1, pixelY),
                [244] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            FeralWolfFur.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Alligators;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [28] = map.GetPixel(4, pixelY),
                [51] = map.GetPixel(8, pixelY),
                [57] = map.GetPixel(3, pixelY),
                [92] = map.GetPixel(2, pixelY),
                [128] = map.GetPixel(7, pixelY),
                [166] = map.GetPixel(6, pixelY),
                [179] = map.GetPixel(1, pixelY),
                [204] = map.GetPixel(5, pixelY),
                [217] = map.GetPixel(0, pixelY),
                [256] = Color.white
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            AlligatorSwaps.Add(swap);
        }


        map = State.GameManager.PaletteDictionary.Crux;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [19] = map.GetPixel(5, pixelY),
                [33] = map.GetPixel(6, pixelY),
                [38] = map.GetPixel(0, pixelY),
                [64] = map.GetPixel(7, pixelY),
                [77] = map.GetPixel(1, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [115] = map.GetPixel(9, pixelY),
                [128] = map.GetPixel(2, pixelY),
                [135] = map.GetPixel(10, pixelY),
                [191] = map.GetPixel(3, pixelY),
                [196] = map.GetPixel(11, pixelY),
                [217] = map.GetPixel(4, pixelY),
                [230] = map.GetPixel(12, pixelY)
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            CruxSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.BeeNewSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(11, pixelY),
                [22] = map.GetPixel(10, pixelY),
                [42] = map.GetPixel(9, pixelY),
                [62] = map.GetPixel(8, pixelY),
                [92] = map.GetPixel(7, pixelY),
                [113] = map.GetPixel(6, pixelY),
                [133] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [175] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            BeeNewSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.DriderSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [33] = map.GetPixel(7, pixelY),
                [46] = map.GetPixel(13, pixelY),
                [51] = map.GetPixel(6, pixelY),
                [74] = map.GetPixel(12, pixelY),
                [81] = map.GetPixel(5, pixelY),
                [98] = map.GetPixel(11, pixelY),
                [113] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [166] = map.GetPixel(10, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [235] = map.GetPixel(9, pixelY),
                [240] = map.GetPixel(8, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            DriderSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.DriderEyes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [33] = map.GetPixel(4, pixelY),
                [51] = map.GetPixel(3, pixelY),
                [113] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            DriderEyesSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.AlrauneSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [81] = map.GetPixel(4, pixelY),
                [84] = map.GetPixel(7, pixelY),
                [142] = map.GetPixel(6, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [196] = map.GetPixel(5, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            AlrauneSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.AlrauneHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [81] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            AlrauneHairSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.AlrauneFoliage;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [25] = map.GetPixel(8, pixelY),
                [33] = map.GetPixel(7, pixelY),
                [42] = map.GetPixel(6, pixelY),
                [81] = map.GetPixel(5, pixelY),
                [113] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            AlrauneFoliageSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.DemibatSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(13, pixelY),
                [32] = map.GetPixel(12, pixelY),
                [47] = map.GetPixel(11, pixelY),
                [64] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [109] = map.GetPixel(7, pixelY),
                [123] = map.GetPixel(6, pixelY),
                [138] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            DemibatSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Skin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            DemibatHumanSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.MermenSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(13, pixelY),
                [25] = map.GetPixel(12, pixelY),
                [33] = map.GetPixel(11, pixelY),
                [42] = map.GetPixel(10, pixelY),
                [62] = map.GetPixel(9, pixelY),
                [81] = map.GetPixel(8, pixelY),
                [97] = map.GetPixel(7, pixelY),
                [113] = map.GetPixel(6, pixelY),
                [133] = map.GetPixel(5, pixelY),
                [143] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            MermenSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.MermenHair;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [81] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            MermenHairSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.AviansSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(16, pixelY),
                [17] = map.GetPixel(15, pixelY),
                [32] = map.GetPixel(14, pixelY),
                [47] = map.GetPixel(13, pixelY),
                [62] = map.GetPixel(12, pixelY),
                [81] = map.GetPixel(11, pixelY),
                [86] = map.GetPixel(10, pixelY),
                [92] = map.GetPixel(9, pixelY),
                [103] = map.GetPixel(8, pixelY),
                [113] = map.GetPixel(7, pixelY),
                [133] = map.GetPixel(6, pixelY),
                [153] = map.GetPixel(5, pixelY),
                [175] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            AviansSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.DemiantSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(10, pixelY),
                [17] = map.GetPixel(9, pixelY),
                [32] = map.GetPixel(8, pixelY),
                [47] = map.GetPixel(7, pixelY),
                [62] = map.GetPixel(6, pixelY),
                [81] = map.GetPixel(5, pixelY),
                [120] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            DemiantSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.DemifrogSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(13, pixelY),
                [17] = map.GetPixel(12, pixelY),
                [32] = map.GetPixel(11, pixelY),
                [40] = map.GetPixel(20, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [55] = map.GetPixel(19, pixelY),
                [62] = map.GetPixel(9, pixelY),
                [70] = map.GetPixel(18, pixelY),
                [81] = map.GetPixel(4, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [109] = map.GetPixel(7, pixelY),
                [123] = map.GetPixel(6, pixelY),
                [138] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [175] = map.GetPixel(17, pixelY),
                [186] = map.GetPixel(16, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [213] = map.GetPixel(15, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [242] = map.GetPixel(14, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            DemifrogSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.SharkSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(12, pixelY),
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [109] = map.GetPixel(7, pixelY),
                [123] = map.GetPixel(6, pixelY),
                [138] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            SharkSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.SharkSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(12, pixelY),
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(4, pixelY),
                [96] = map.GetPixel(3, pixelY),
                [109] = map.GetPixel(2, pixelY),
                [123] = map.GetPixel(1, pixelY),
                [138] = map.GetPixel(0, pixelY),
                [153] = map.GetPixel(9, pixelY),
                [198] = map.GetPixel(8, pixelY),
                [214] = map.GetPixel(7, pixelY),
                [229] = map.GetPixel(6, pixelY),
                [255] = map.GetPixel(5, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            SharkReversedSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.DeerSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(12, pixelY),
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [109] = map.GetPixel(7, pixelY),
                [123] = map.GetPixel(6, pixelY),
                [138] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            DeerSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.DeerLeaf;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [100] = map.GetPixel(2, pixelY),
                [150] = map.GetPixel(1, pixelY),
                [200] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            DeerLeafSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Puca;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [5] = map.GetPixel(7, pixelY),
                [23] = map.GetPixel(6, pixelY),
                [70] = map.GetPixel(11, pixelY),
                [84] = map.GetPixel(5, pixelY),
                [105] = map.GetPixel(10, pixelY),
                [140] = map.GetPixel(9, pixelY),
                [142] = map.GetPixel(4, pixelY),
                [152] = map.GetPixel(3, pixelY),
                [191] = map.GetPixel(8, pixelY),
                [225] = map.GetPixel(2, pixelY),
                [236] = map.GetPixel(1, pixelY),
                [244] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 4);
            PucaSwaps.Add(swap);

            swapDict = new Dictionary<int, Color>
            {
                [NormalIndexes[0]] = map.GetPixel(7, pixelY),
                [NormalIndexes[1]] = map.GetPixel(6, pixelY),
                [NormalIndexes[2]] = map.GetPixel(5, pixelY),
                [NormalIndexes[3]] = map.GetPixel(4, pixelY),
            };
            swap = new ColorSwapPalette(swapDict);
            PucaBallSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Hamsters;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [5] = map.GetPixel(7, pixelY),
                [23] = map.GetPixel(6, pixelY),
                [70] = map.GetPixel(11, pixelY),
                [84] = map.GetPixel(5, pixelY),
                [105] = map.GetPixel(10, pixelY),
                [140] = map.GetPixel(9, pixelY),
                [142] = map.GetPixel(4, pixelY),
                [152] = map.GetPixel(3, pixelY),
                [191] = map.GetPixel(8, pixelY),
                [225] = map.GetPixel(2, pixelY),
                [236] = map.GetPixel(1, pixelY),
                [244] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 4);
            HamstersSwaps.Add(swap);

            swapDict = new Dictionary<int, Color>
            {
                [NormalIndexes[0]] = map.GetPixel(7, pixelY),
                [NormalIndexes[1]] = map.GetPixel(6, pixelY),
                [NormalIndexes[2]] = map.GetPixel(5, pixelY),
                [NormalIndexes[3]] = map.GetPixel(4, pixelY),
            };
            swap = new ColorSwapPalette(swapDict);
            HamstersBallSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.HippoSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [33] = map.GetPixel(11, pixelY),
                [42] = map.GetPixel(10, pixelY),
                [62] = map.GetPixel(9, pixelY),
                [81] = map.GetPixel(8, pixelY),
                [84] = map.GetPixel(7, pixelY),
                [142] = map.GetPixel(6, pixelY),
                [153] = map.GetPixel(5, pixelY),
                [178] = map.GetPixel(4, pixelY),
                [196] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            HippoSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.ViperSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(13, pixelY),
                [22] = map.GetPixel(12, pixelY),
                [42] = map.GetPixel(11, pixelY),
                [62] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [97] = map.GetPixel(8, pixelY),
                [113] = map.GetPixel(7, pixelY),
                [133] = map.GetPixel(6, pixelY),
                [153] = map.GetPixel(5, pixelY),
                [175] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            ViperSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.KomodosSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(12, pixelY),
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [109] = map.GetPixel(7, pixelY),
                [123] = map.GetPixel(6, pixelY),
                [138] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            KomodosSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.CockatriceSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [81] = map.GetPixel(9, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [109] = map.GetPixel(7, pixelY),
                [123] = map.GetPixel(6, pixelY),
                [138] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            CockatriceSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.KomodosSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(12, pixelY),
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(4, pixelY),
                [96] = map.GetPixel(3, pixelY),
                [109] = map.GetPixel(2, pixelY),
                [123] = map.GetPixel(1, pixelY),
                [138] = map.GetPixel(0, pixelY),
                [153] = map.GetPixel(9, pixelY),
                [198] = map.GetPixel(8, pixelY),
                [214] = map.GetPixel(7, pixelY),
                [229] = map.GetPixel(6, pixelY),
                [255] = map.GetPixel(5, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            KomodosReversedSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.VargulSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [109] = map.GetPixel(7, pixelY),
                [123] = map.GetPixel(6, pixelY),
                [138] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            VargulSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Harvester;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(0, pixelY),
                [5] = map.GetPixel(1, pixelY),
                [9] = map.GetPixel(2, pixelY),
                [16] = map.GetPixel(3, pixelY),
                [61] = map.GetPixel(4, pixelY),
                [68] = map.GetPixel(5, pixelY),
                [94] = map.GetPixel(6, pixelY),
                [97] = map.GetPixel(7, pixelY),
                [122] = map.GetPixel(8, pixelY),
                [129] = map.GetPixel(9, pixelY),
                [172] = map.GetPixel(10, pixelY),
                [176] = map.GetPixel(11, pixelY),
                [204] = map.GetPixel(12, pixelY),
                [217] = map.GetPixel(13, pixelY),
                [255] = map.GetPixel(14, pixelY)
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            HarvesterSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Bats;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(0, pixelY),
                [5] = map.GetPixel(1, pixelY),
                [9] = map.GetPixel(2, pixelY),
                [16] = map.GetPixel(3, pixelY),
                [61] = map.GetPixel(4, pixelY),
                [68] = map.GetPixel(5, pixelY),
                [94] = map.GetPixel(6, pixelY),
                [97] = map.GetPixel(7, pixelY),
                [122] = map.GetPixel(8, pixelY),
                [129] = map.GetPixel(9, pixelY),
                [172] = map.GetPixel(10, pixelY),
                [176] = map.GetPixel(11, pixelY),
                [204] = map.GetPixel(12, pixelY),
                [217] = map.GetPixel(13, pixelY),
                [255] = map.GetPixel(14, pixelY)
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            BatSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Dragonfly;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(0, pixelY),
                [10] = map.GetPixel(1, pixelY),
                [20] = map.GetPixel(2, pixelY),
                [30] = map.GetPixel(3, pixelY),
                [120] = map.GetPixel(4, pixelY),
                [150] = map.GetPixel(5, pixelY),
                [160] = map.GetPixel(6, pixelY),
                [180] = map.GetPixel(7, pixelY),
                [200] = map.GetPixel(8, pixelY),
                [255] = map.GetPixel(9, pixelY)
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            DragonflySwaps.Add(swap);
        }


        map = State.GameManager.PaletteDictionary.Ant;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(0, pixelY),
                [10] = map.GetPixel(1, pixelY),
                [20] = map.GetPixel(2, pixelY),
                [30] = map.GetPixel(3, pixelY),
                [200] = map.GetPixel(4, pixelY),
                [230] = map.GetPixel(5, pixelY),
                [255] = map.GetPixel(6, pixelY)
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            AntSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.GryphonSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [25] = map.GetPixel(11, pixelY),
                [33] = map.GetPixel(10, pixelY),
                [42] = map.GetPixel(9, pixelY),
                [81] = map.GetPixel(8, pixelY),
                [113] = map.GetPixel(7, pixelY),
                [133] = map.GetPixel(6, pixelY),
                [153] = map.GetPixel(5, pixelY),
                [198] = map.GetPixel(4, pixelY),
                [206] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            GryphonSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.SlugSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [25] = map.GetPixel(15, pixelY),
                [33] = map.GetPixel(14, pixelY),
                [42] = map.GetPixel(13, pixelY),
                [62] = map.GetPixel(12, pixelY),
                [81] = map.GetPixel(11, pixelY),
                [97] = map.GetPixel(10, pixelY),
                [113] = map.GetPixel(9, pixelY),
                [133] = map.GetPixel(8, pixelY),
                [153] = map.GetPixel(7, pixelY),
                [168] = map.GetPixel(6, pixelY),
                [183] = map.GetPixel(5, pixelY),
                [198] = map.GetPixel(4, pixelY),
                [206] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            SlugSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Panthers;
        for (int pixelY = 0; pixelY < 8; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(0, pixelY),
                [100] = map.GetPixel(1, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(3, pixelY),
                [250] = map.GetPixel(4, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            PantherClothesSwaps.Add(swap);
        }
        for (int pixelY = 8; pixelY < 11; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(0, pixelY),
                [100] = map.GetPixel(1, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(3, pixelY),
                [250] = map.GetPixel(4, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            PantherBodyPaintSwaps.Add(swap);
        }
        for (int pixelY = 11; pixelY < 16; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(0, pixelY),
                [100] = map.GetPixel(1, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(3, pixelY),
                [250] = map.GetPixel(4, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            PantherHairSwaps.Add(swap);
        }
        for (int pixelY = 16; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(0, pixelY),
                [100] = map.GetPixel(1, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(3, pixelY),
                [250] = map.GetPixel(4, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            PantherSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.SalamanderSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(14, pixelY),
                [25] = map.GetPixel(13, pixelY),
                [33] = map.GetPixel(12, pixelY),
                [42] = map.GetPixel(11, pixelY),
                [62] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [113] = map.GetPixel(8, pixelY),
                [133] = map.GetPixel(7, pixelY),
                [153] = map.GetPixel(6, pixelY),
                [183] = map.GetPixel(5, pixelY),
                [198] = map.GetPixel(4, pixelY),
                [206] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            SalamanderSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.Horsepalettes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(0, pixelY),
                [100] = map.GetPixel(1, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(3, pixelY),
                [250] = map.GetPixel(4, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            HorseSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.MantisSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(14, pixelY),
                [17] = map.GetPixel(13, pixelY),
                [32] = map.GetPixel(12, pixelY),
                [47] = map.GetPixel(11, pixelY),
                [62] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [92] = map.GetPixel(8, pixelY),
                [103] = map.GetPixel(7, pixelY),
                [113] = map.GetPixel(6, pixelY),
                [133] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [175] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [214] = map.GetPixel(1, pixelY),
                [229] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            MantisSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.EasternDragon;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(0, pixelY),
                [20] = map.GetPixel(5, pixelY),
                [30] = map.GetPixel(1, pixelY),
                [40] = map.GetPixel(6, pixelY),
                [50] = map.GetPixel(2, pixelY),
                [60] = map.GetPixel(7, pixelY),
                [75] = map.GetPixel(3, pixelY),
                [80] = map.GetPixel(8, pixelY),
                [90] = map.GetPixel(10, pixelY),
                [100] = map.GetPixel(4, pixelY),
                [110] = map.GetPixel(9, pixelY),
                [130] = map.GetPixel(11, pixelY),
                [140] = map.GetPixel(14, pixelY),
                [160] = map.GetPixel(15, pixelY),
                [180] = map.GetPixel(12, pixelY),
                [220] = map.GetPixel(13, pixelY)
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            EasternDragon.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.CatfishSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(11, pixelY),
                [17] = map.GetPixel(10, pixelY),
                [32] = map.GetPixel(9, pixelY),
                [47] = map.GetPixel(8, pixelY),
                [62] = map.GetPixel(7, pixelY),
                [81] = map.GetPixel(6, pixelY),
                [103] = map.GetPixel(5, pixelY),
                [133] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [175] = map.GetPixel(2, pixelY),
                [198] = map.GetPixel(1, pixelY),
                [229] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            CatfishSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.GazelleSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(12, pixelY),
                [22] = map.GetPixel(11, pixelY),
                [42] = map.GetPixel(10, pixelY),
                [62] = map.GetPixel(9, pixelY),
                [92] = map.GetPixel(8, pixelY),
                [103] = map.GetPixel(7, pixelY),
                [113] = map.GetPixel(6, pixelY),
                [133] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [175] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            GazelleSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.EarthwormSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(6, pixelY),
                [47] = map.GetPixel(5, pixelY),
                [81] = map.GetPixel(4, pixelY),
                [153] = map.GetPixel(3, pixelY),
                [198] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            EarthwormSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.TerrorbirdSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(4, pixelY),
                [96] = map.GetPixel(3, pixelY),
                [109] = map.GetPixel(2, pixelY),
                [123] = map.GetPixel(1, pixelY),
                [138] = map.GetPixel(0, pixelY),
                [153] = map.GetPixel(9, pixelY),
                [198] = map.GetPixel(8, pixelY),
                [214] = map.GetPixel(7, pixelY),
                [229] = map.GetPixel(6, pixelY),
                [255] = map.GetPixel(5, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            TerrorbirdSkinSwaps.Add(swap);
        }
		
		map = State.GameManager.PaletteDictionary.GoodraSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [34] = map.GetPixel(26, pixelY),
                [42] = map.GetPixel(25, pixelY),
                [43] = map.GetPixel(18, pixelY),
                [52] = map.GetPixel(24, pixelY),
                [53] = map.GetPixel(17, pixelY),
                [66] = map.GetPixel(16, pixelY),
                [75] = map.GetPixel(22, pixelY),
                [80] = map.GetPixel(9, pixelY),
                [89] = map.GetPixel(23, pixelY),
                [91] = map.GetPixel(14, pixelY),
                [93] = map.GetPixel(21, pixelY),
                [99] = map.GetPixel(8, pixelY),
                [103] = map.GetPixel(15, pixelY),
                [112] = map.GetPixel(34, pixelY),
                [113] = map.GetPixel(13, pixelY),
                [115] = map.GetPixel(20, pixelY),
                [123] = map.GetPixel(7, pixelY),
                [130] = map.GetPixel(5, pixelY),
                [135] = map.GetPixel(12, pixelY),
                [139] = map.GetPixel(33, pixelY),
                [140] = map.GetPixel(11, pixelY),
                [147] = map.GetPixel(19, pixelY),
                [154] = map.GetPixel(6, pixelY),
                [162] = map.GetPixel(4, pixelY),
                [168] = map.GetPixel(10, pixelY),
                [173] = map.GetPixel(32, pixelY),
                [194] = map.GetPixel(31, pixelY),
                [201] = map.GetPixel(3, pixelY),
                [202] = map.GetPixel(29, pixelY),
                [205] = map.GetPixel(1, pixelY),
                [215] = map.GetPixel(2, pixelY),
                [251] = map.GetPixel(28, pixelY),
                [252] = map.GetPixel(27, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            GoodraSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.AabayxSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [1] = map.GetPixel(0, pixelY),
                [27] = map.GetPixel(1, pixelY),
                [113] = map.GetPixel(2, pixelY),
                [164] = map.GetPixel(3, pixelY),
                [255] = map.GetPixel(4, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            AabayxSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.MiceSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(12, pixelY),
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [81] = map.GetPixel(9, pixelY),
                [96] = map.GetPixel(8, pixelY),
                [109] = map.GetPixel(7, pixelY),
                [123] = map.GetPixel(6, pixelY),
                [138] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            MiceSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.MiceHumanSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [62] = map.GetPixel(4, pixelY),
                [96] = map.GetPixel(3, pixelY),
                [109] = map.GetPixel(2, pixelY),
                [123] = map.GetPixel(1, pixelY),
                [153] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            MiceHumanSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.LupineSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [45] = map.GetPixel(11, pixelY),
                [60] = map.GetPixel(10, pixelY),
                [90] = map.GetPixel(9, pixelY),
                [120] = map.GetPixel(8, pixelY),
                [135] = map.GetPixel(7, pixelY),
                [150] = map.GetPixel(6, pixelY),
                [165] = map.GetPixel(5, pixelY),
                [180] = map.GetPixel(4, pixelY),
                [210] = map.GetPixel(3, pixelY),
                [225] = map.GetPixel(2, pixelY),
                [240] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
                
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            LupineSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.LupineReverse;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [45] = map.GetPixel(11, pixelY),
                [60] = map.GetPixel(10, pixelY),
                [90] = map.GetPixel(9, pixelY),
                [120] = map.GetPixel(8, pixelY),
                [135] = map.GetPixel(7, pixelY),
                [150] = map.GetPixel(6, pixelY),
                [165] = map.GetPixel(5, pixelY),
                [180] = map.GetPixel(4, pixelY),
                [210] = map.GetPixel(3, pixelY),
                [225] = map.GetPixel(2, pixelY),
                [240] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            LupineReverseSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.FeralLionsFur;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = Color.black,
                [132] = map.GetPixel(8, pixelY),
                [136] = map.GetPixel(7, pixelY),
                [203] = map.GetPixel(6, pixelY),
                [219] = map.GetPixel(5, pixelY),
                [224] = map.GetPixel(4, pixelY),
                [235] = map.GetPixel(3, pixelY),
                [236] = map.GetPixel(2, pixelY),
                [240] = map.GetPixel(1, pixelY),
                [251] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 0);
            FeralLionsFurSwaps.Add(swap);
        }
        map = State.GameManager.PaletteDictionary.FeralLionsEyes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [253] = map.GetPixel(0, pixelY),
                [140] = map.GetPixel(1, pixelY),
                [254] = map.GetPixel(2, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict, maxClearRange: 5);
            FeralLionsEyesSwaps.Add(swap);
        }
        map = State.GameManager.PaletteDictionary.FeralLionsMane;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [87] = map.GetPixel(2, pixelY),
                [91] = map.GetPixel(1, pixelY),
                [108] = map.GetPixel(0, pixelY),

            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            FeralLionsManeSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.FeralHorseSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [81] = map.GetPixel(8, pixelY),
                [96] = map.GetPixel(7, pixelY),
                [109] = map.GetPixel(6, pixelY),
                [123] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            FeralHorseSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.WyvernMatron;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [26] = map.GetPixel(10, pixelY),
                [51] = map.GetPixel(9, pixelY),
                [64] = map.GetPixel(4, pixelY),
                [77] = map.GetPixel(8, pixelY),
                [89] = map.GetPixel(14, pixelY),
                [102] = map.GetPixel(3, pixelY),
                [126] = map.GetPixel(7, pixelY),
                [153] = map.GetPixel(2, pixelY),
                [166] = map.GetPixel(13, pixelY),
                [179] = map.GetPixel(6, pixelY),
                [191] = map.GetPixel(5, pixelY),
                [204] = map.GetPixel(1, pixelY),
                [217] = map.GetPixel(12, pixelY),
                [230] = map.GetPixel(0, pixelY),
                [255] = map.GetPixel(11, pixelY)
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            WyvernMatronSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.FeralFoxSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [15] = map.GetPixel(13, pixelY),
                [40] = map.GetPixel(12, pixelY),
                [55] = map.GetPixel(11, pixelY),
                [70] = map.GetPixel(10, pixelY),
                [85] = map.GetPixel(9, pixelY),
                [110] = map.GetPixel(8, pixelY),
                [130] = map.GetPixel(7, pixelY),
                [150] = map.GetPixel(6, pixelY),
                [170] = map.GetPixel(5, pixelY),
                [185] = map.GetPixel(4, pixelY),
                [210] = map.GetPixel(3, pixelY),
                [225] = map.GetPixel(2, pixelY),
                [240] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            FeralFoxSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.TerminidSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [25] = map.GetPixel(9, pixelY),
                [55] = map.GetPixel(8, pixelY),
                [75] = map.GetPixel(7, pixelY),
                [95] = map.GetPixel(6, pixelY),
                [125] = map.GetPixel(5, pixelY),
                [145] = map.GetPixel(4, pixelY),
                [195] = map.GetPixel(3, pixelY),
                [215] = map.GetPixel(2, pixelY),
                [235] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            TerminidSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.FeralOrcaSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(0, pixelY),
                [34] = map.GetPixel(1, pixelY),
                [50] = map.GetPixel(2, pixelY),
                [73] = map.GetPixel(3, pixelY),
                [151] = map.GetPixel(4, pixelY),
                [182] = map.GetPixel(5, pixelY),
                [199] = map.GetPixel(6, pixelY),
                [255] = map.GetPixel(7, pixelY),
                
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            FeralOrcaSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.OtachiSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [10] = map.GetPixel(13, pixelY),
                [30] = map.GetPixel(12, pixelY),
                [50] = map.GetPixel(11, pixelY),
                [70] = map.GetPixel(10, pixelY),
                [90] = map.GetPixel(9, pixelY),
                [110] = map.GetPixel(8, pixelY),
                [130] = map.GetPixel(7, pixelY),
                [150] = map.GetPixel(6, pixelY),
                [170] = map.GetPixel(5, pixelY),
                [180] = map.GetPixel(4, pixelY),
                [190] = map.GetPixel(3, pixelY),
                [210] = map.GetPixel(2, pixelY),
                [230] = map.GetPixel(1, pixelY),
                [250] = map.GetPixel(0, pixelY),
                
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            OtachiSkinSwaps.Add(swap);
        }
        
        map = State.GameManager.PaletteDictionary.GnollSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [0] = map.GetPixel(12, pixelY),
                [32] = map.GetPixel(11, pixelY),
                [47] = map.GetPixel(10, pixelY),
                [62] = map.GetPixel(9, pixelY),
                [81] = map.GetPixel(8, pixelY),
                [96] = map.GetPixel(7, pixelY),
                [109] = map.GetPixel(6, pixelY),
                [123] = map.GetPixel(5, pixelY),
                [153] = map.GetPixel(4, pixelY),
                [198] = map.GetPixel(3, pixelY),
                [214] = map.GetPixel(2, pixelY),
                [229] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(0, pixelY),
                
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            GnollSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.UmbreonSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [5] = map.GetPixel(0, pixelY),
                [63] = map.GetPixel(1, pixelY),
                [75] = map.GetPixel(9, pixelY),
                [120] = map.GetPixel(7, pixelY),
                [126] = map.GetPixel(2, pixelY),
                [175] = map.GetPixel(8, pixelY),
                [189] = map.GetPixel(3, pixelY),
                [200] = map.GetPixel(4, pixelY),
                [225] = map.GetPixel(5, pixelY),
                [255] = map.GetPixel(6, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            UmbreonSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.UmbreonExt;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [50] = map.GetPixel(0, pixelY),
                [100] = map.GetPixel(1, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [200] = map.GetPixel(3, pixelY),
                [250] = map.GetPixel(4, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            UmbreonExtSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.UmbreonClothes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [42] = map.GetPixel(0, pixelY),
                [66] = map.GetPixel(1, pixelY),
                [101] = map.GetPixel(2, pixelY),
                [123] = map.GetPixel(3, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            UmbreonClothesSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.UmbreonArmor;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [70] = map.GetPixel(9, pixelY),
                [84] = map.GetPixel(12, pixelY),
                [66] = map.GetPixel(0, pixelY),
                [91] = map.GetPixel(7, pixelY),
                [112] = map.GetPixel(8, pixelY),
                [116] = map.GetPixel(6, pixelY),
                [140] = map.GetPixel(5, pixelY),
                [150] = map.GetPixel(2, pixelY),
                [186] = map.GetPixel(3, pixelY),
                [158] = map.GetPixel(4, pixelY),
                [200] = map.GetPixel(11, pixelY),
                [224] = map.GetPixel(1, pixelY),
                [255] = map.GetPixel(10, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            UmbreonArmorSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.EeveeEqualeonSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [5] = map.GetPixel(0, pixelY),
                [50] = map.GetPixel(1, pixelY),
                [85] = map.GetPixel(2, pixelY),
                [130] = map.GetPixel(3, pixelY),
                [175] = map.GetPixel(4, pixelY),
                [200] = map.GetPixel(5, pixelY),
                [255] = map.GetPixel(6, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            EeveeEqualeonSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.EeveeEqualeonExt;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [15] = map.GetPixel(6, pixelY),
                [100] = map.GetPixel(4, pixelY),
                [152] = map.GetPixel(0, pixelY),
                [190] = map.GetPixel(5, pixelY),
                [225] = map.GetPixel(1, pixelY),
                [236] = map.GetPixel(2, pixelY),
                [244] = map.GetPixel(3, pixelY),

            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            EeveeEqualeonExtSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.EeveeEqualeonClothing;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [1] = map.GetPixel(1, pixelY),
                [2] = map.GetPixel(0, pixelY),
                [50] = map.GetPixel(6, pixelY),
                [100] = map.GetPixel(5, pixelY),
                [150] = map.GetPixel(4, pixelY),
                [200] = map.GetPixel(3, pixelY),
                [250] = map.GetPixel(2, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            EeveeEqualeonClothingSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.EqualeonEyes;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [75] = map.GetPixel(0, pixelY),
                [137] = map.GetPixel(1, pixelY),
                [258] = map.GetPixel(2, pixelY),

            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            EqualeonEyesSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.PlantSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [3] = map.GetPixel(0, pixelY),
                [34] = map.GetPixel(1, pixelY),
                [50] = map.GetPixel(2, pixelY),
                [85] = map.GetPixel(3, pixelY),
                [6] = map.GetPixel(4, pixelY),
                [71] = map.GetPixel(5, pixelY),
                [101] = map.GetPixel(6, pixelY),
                [25] = map.GetPixel(7, pixelY),
                [55] = map.GetPixel(8, pixelY),
                [111] = map.GetPixel(9, pixelY),
                [75] = map.GetPixel(10, pixelY),
                [42] = map.GetPixel(11, pixelY),
                [255] = map.GetPixel(12, pixelY),
                [153] = map.GetPixel(13, pixelY),
                [9] = map.GetPixel(14, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            PlantSkinSwaps.Add(swap);
        }

        map = State.GameManager.PaletteDictionary.ViiselSkin;
        for (int pixelY = 0; pixelY < map.height; pixelY++)
        {
            Dictionary<int, Color> swapDict = new Dictionary<int, Color>
            {
                [55] = map.GetPixel(0, pixelY),
                [85] = map.GetPixel(1, pixelY),
                [111] = map.GetPixel(2, pixelY),
                [130] = map.GetPixel(3, pixelY),
                [150] = map.GetPixel(4, pixelY),
                [167] = map.GetPixel(5, pixelY),
                [232] = map.GetPixel(6, pixelY),
                [240] = map.GetPixel(7, pixelY),
                [255] = map.GetPixel(8, pixelY),
            };
            ColorSwapPalette swap = new ColorSwapPalette(swapDict);
            ViiselSkinSwaps.Add(swap);
        }
    }

    private static Dictionary<int, Color> Red(Texture2D map, int pixelY)
    {
        return new Dictionary<int, Color>
        {
            [152] = map.GetPixel(0, pixelY),
            [225] = map.GetPixel(1, pixelY),
            [236] = map.GetPixel(2, pixelY),
            [244] = map.GetPixel(3, pixelY),
            [250] = map.GetPixel(4, pixelY),
        };
    }

    private static Dictionary<int, Color> RedReversed(Texture2D map, int pixelY)
    {
        return new Dictionary<int, Color>
        {
            [152] = map.GetPixel(4, pixelY),
            [225] = map.GetPixel(3, pixelY),
            [237] = map.GetPixel(2, pixelY),
            [245] = map.GetPixel(1, pixelY),
            [250] = map.GetPixel(0, pixelY),
        };
    }

    private static Dictionary<int, Color> Normal(int[] NormalIndexes, Texture2D map, int pixelY)
    {
        return new Dictionary<int, Color>
        {
            [NormalIndexes[0]] = map.GetPixel(0, pixelY),
            [NormalIndexes[1]] = map.GetPixel(1, pixelY),
            [NormalIndexes[2]] = map.GetPixel(2, pixelY),
            [NormalIndexes[3]] = map.GetPixel(3, pixelY),
            [NormalIndexes[4]] = map.GetPixel(4, pixelY)
        };
    }

    private static Dictionary<int, Color> NormalReversed(int[] NormalIndexes, Texture2D map, int pixelY)
    {
        return new Dictionary<int, Color>
        {
            [NormalIndexes[0]] = map.GetPixel(4, pixelY),
            [NormalIndexes[1]] = map.GetPixel(3, pixelY),
            [NormalIndexes[2]] = map.GetPixel(2, pixelY),
            [NormalIndexes[3]] = map.GetPixel(1, pixelY),
            [NormalIndexes[4]] = map.GetPixel(0, pixelY)
        };
    }

    static List<ColorSwapPalette> WireUp(SwapType swapType)
    {
        List<ColorSwapPalette> palette = new List<ColorSwapPalette>();
        Swaps[swapType] = palette;
        return palette;
    }
}
