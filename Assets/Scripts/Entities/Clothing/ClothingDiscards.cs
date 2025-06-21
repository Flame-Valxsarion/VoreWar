using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class ClothingDiscards
{
    [OdinSerialize]
    internal Vec2i location;
    [OdinSerialize]
    internal int type;
    [OdinSerialize]
    internal Race race;
    [OdinSerialize]
    internal int color;
    [OdinSerialize]
    internal int sortOrder;
    [OdinSerialize]
    int angle;
    [OdinSerialize]
    internal string name;

    static List<MainClothing> AllClothes = new List<MainClothing>();

    public ClothingDiscards(Vec2i location, Race race, int type, int color, int sortOrder, string name)
    {
        this.location = location;
        this.race = race;
        this.type = type; // When setting up clothes for a new race this is what the "Type =" is used to identify. If a newer race's type matches an older race's "Type" number in the GenerateSpritePrefab list below the list will prioritise the earliest instance of that "Type" number resulting is using that sprite over the newer instance.
        this.color = color;
        this.name = name;
        this.sortOrder = sortOrder;
        angle = State.Rand.Next(40) - 20;
    }

    internal void GenerateSpritePrefab(Transform folder)
    {
        var sprite = Object.Instantiate(State.GameManager.DiscardedClothing, new Vector3(location.x - .5f + Random.Range(0, 1f), location.y - .5f + Random.Range(0, 1f)), Quaternion.Euler(0, 0, angle), folder).GetComponent<SpriteRenderer>();

        if (AllClothes.Any() == false)
        {
            AllClothes = new List<MainClothing>();
            AllClothes.AddRange(ClothingTypes.All);
            AllClothes.AddRange(RaceSpecificClothing.All);
            AllClothes.AddRange(TaurusClothes.TaurusClothingTypes.All);
            AllClothes.AddRange(CruxClothing.CruxClothingTypes.All);
            AllClothes.Add(new KangarooClothes.Loincloth1());
            AllClothes.Add(new KangarooClothes.Loincloth2()); //3 and 4 are unneeded because they share with 2
            AllClothes.AddRange(Races.Bees.DiscardData);
            AllClothes.AddRange(Races.Panthers.AllClothing);
            AllClothes.AddRange(Races.Hippos.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.Hippos.AllowedWaistTypes);
            AllClothes.AddRange(Races.Mermen.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.Mermen.AllowedWaistTypes);
            AllClothes.AddRange(Races.Vipers.AllowedWaistTypes);
            AllClothes.AddRange(Races.Demifrogs.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.Demifrogs.AllowedWaistTypes);
            AllClothes.AddRange(Races.Imps.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.Imps.AllowedWaistTypes);
            AllClothes.AddRange(Races.Imps.ExtraMainClothing1Types);
            AllClothes.AddRange(Races.Imps.ExtraMainClothing2Types);
            AllClothes.AddRange(Races.Imps.ExtraMainClothing3Types);
            AllClothes.AddRange(Races.Imps.ExtraMainClothing4Types);
            AllClothes.AddRange(Races.Imps.ExtraMainClothing5Types);
            AllClothes.AddRange(Races.Goblins.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.Goblins.AllowedWaistTypes);
            AllClothes.AddRange(Races.Goblins.ExtraMainClothing1Types);
            AllClothes.AddRange(Races.Goblins.ExtraMainClothing2Types);
            AllClothes.AddRange(Races.Goblins.ExtraMainClothing3Types);
            AllClothes.AddRange(Races.Goblins.ExtraMainClothing4Types);
            AllClothes.AddRange(Races.Goblins.ExtraMainClothing5Types);
            AllClothes.AddRange(Races.Demisharks.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.Demisharks.AllowedWaistTypes);
            AllClothes.AddRange(Races.Demisharks.ExtraMainClothing1Types);
            AllClothes.AddRange(Races.Komodos.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.Komodos.AllowedWaistTypes);
            AllClothes.AddRange(Races.Demibats.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.Cockatrice.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.Cockatrice.AllowedWaistTypes);
            AllClothes.AddRange(Races.Deer.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.Deer.AllowedWaistTypes);
            AllClothes.AddRange(Races.Deer.ExtraMainClothing1Types);
            AllClothes.AddRange(Races.Humans.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.Humans.AllowedWaistTypes);
            AllClothes.AddRange(Races.Vargul.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.Vargul.AllowedWaistTypes);
            AllClothes.AddRange(Races.Aabayx.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.Aabayx.AllowedWaistTypes);
            AllClothes.AddRange(Races.Lupine.AllowedMainClothingTypes);
            AllClothes.AddRange(Races.Lupine.AllowedWaistTypes);
            AllClothes = AllClothes.Distinct().ToList();
        }
        var clothingType = AllClothes.Where(s => s.Type == type).FirstOrDefault();
        if (clothingType == null)
            return;
        if (clothingType.FixedColor == false)
        {
            if (clothingType.DiscardUsesPalettes)
                sprite.GetComponentInChildren<SpriteRenderer>().material = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, color).colorSwapMaterial;
            else
                sprite.color = ColorMap.GetClothingColor(color);
        }

        sprite.sprite = clothingType.DiscardSprite;
        sprite.sortingOrder = sortOrder;
    }
}

