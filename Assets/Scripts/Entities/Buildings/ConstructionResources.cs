using OdinSerializer;
using System.Collections.Generic;

public enum ConstructionResourceType
{
    wood,
    stone,
    ores,
    naturalmaterials,
    prefabs,
    manastones
}
public class ConstructionResources
{
    [OdinSerialize]
    public int Wood = 0;
    [OdinSerialize]
    public int Stone = 0;
    [OdinSerialize]
    public int Ores = 0;
    [OdinSerialize]
    public int NaturalMaterials = 0;
    [OdinSerialize]
    public int Prefabs = 0;
    [OdinSerialize]
    public int ManaStones = 0;

    public ConstructionResources()
    {
        Wood = 0;
        Stone = 0;
        Ores = 0;
        NaturalMaterials = 0;
        Prefabs = 0;
        ManaStones = 0;
    }
    public ConstructionResources(int wood = 0, int stones = 0, int nm = 0, int ores = 0, int prefabs = 0, int ms = 0)
    {
        Wood = wood;
        Stone = stones;
        Ores = ores;
        NaturalMaterials = nm;
        Prefabs = prefabs;
        ManaStones = ms;
    }
    public void Reset()
    {
        Wood = 0;
        Stone = 0;
        Ores = 0;
        NaturalMaterials = 0;
        Prefabs = 0;
        ManaStones = 0;
    }

    public void AddResource(ConstructionResourceType type, int amount)
    {
        switch (type)
        {
            case ConstructionResourceType.wood:
                Wood += amount; break;
            case ConstructionResourceType.stone:
                Stone += amount; break;
            case ConstructionResourceType.ores:
                Ores += amount; break;
            case ConstructionResourceType.naturalmaterials:
                NaturalMaterials += amount; break;
            case ConstructionResourceType.prefabs:
                Prefabs += amount; break;
            case ConstructionResourceType.manastones:
                ManaStones += amount; break;
            default:
                break;
        }
    }

    public void SpendResource(ConstructionResourceType type, int amount)
    {
        switch (type)
        {
            case ConstructionResourceType.wood:
                Wood -= amount; break;
            case ConstructionResourceType.stone:
                Stone -= amount; break;
            case ConstructionResourceType.ores:
                Ores -= amount; break;
            case ConstructionResourceType.naturalmaterials:
                NaturalMaterials -= amount; break;
            case ConstructionResourceType.prefabs:
                Prefabs += amount; break;
            case ConstructionResourceType.manastones:
                ManaStones -= amount; break;
            default:
                break;
        }
    }

    /// <summary>
    /// Checks if caller has resources needed of required.
    /// </summary>
    /// <returns></returns>
    public bool CanBuildWithCurrentResources(ConstructionResources required)
    {
        if (!HasNeededResource(ConstructionResourceType.wood, required.Wood)) return false;
        if (!HasNeededResource(ConstructionResourceType.stone, required.Stone)) return false;
        if (!HasNeededResource(ConstructionResourceType.ores, required.Ores)) return false;
        if (!HasNeededResource(ConstructionResourceType.naturalmaterials, required.NaturalMaterials)) return false;
        if (!HasNeededResource(ConstructionResourceType.prefabs, required.Prefabs)) return false;
        if (!HasNeededResource(ConstructionResourceType.manastones, required.ManaStones)) return false;
        return true;
    }
    public void SpendProvidedResources(ConstructionResources required)
    {
        Wood -= required.Wood;
        Stone -= required.Stone;
        Ores -= required.Ores;
        NaturalMaterials -= required.NaturalMaterials;
        Prefabs -= required.Prefabs;
        ManaStones -= required.ManaStones;
    }

    public bool HasNeededResource(ConstructionResourceType type, int amount)
    {
        switch (type)
        {
            case ConstructionResourceType.wood:
                return Wood >= amount;
            case ConstructionResourceType.stone:
                return Stone >= amount;
            case ConstructionResourceType.ores:
                return Ores >= amount;
            case ConstructionResourceType.naturalmaterials:
                return NaturalMaterials >= amount;
            case ConstructionResourceType.prefabs:
                return Prefabs >= amount;
            case ConstructionResourceType.manastones:
                return ManaStones >= amount;
            default:
                break;
        }
        return false;
    }

    public void SetResources(int wood, int stones, int nm, int ores, int prefabs, int ms)
    {
        Wood = wood;
        Stone = stones;
        Ores = ores;
        NaturalMaterials = nm;
        Prefabs = prefabs;
        ManaStones = ms;
    }

    public Dictionary<ConstructionResourceType, int> NeededResources(ConstructionResources required)
    {
        Dictionary<ConstructionResourceType, int> req = new Dictionary<ConstructionResourceType, int>();
        if (!HasNeededResource(ConstructionResourceType.wood, required.Wood))
        {
            req.Add(ConstructionResourceType.wood, Wood - required.Wood);
        }
        if (!HasNeededResource(ConstructionResourceType.stone, required.Stone))
        {
            req.Add(ConstructionResourceType.stone, Stone - required.Stone);
        }
        if (!HasNeededResource(ConstructionResourceType.ores, required.Ores))
        {
            req.Add(ConstructionResourceType.ores, Ores - required.Ores);
        }
        if (!HasNeededResource(ConstructionResourceType.naturalmaterials, required.NaturalMaterials))
        {
            req.Add(ConstructionResourceType.naturalmaterials, NaturalMaterials - required.NaturalMaterials);
        }
        if (!HasNeededResource(ConstructionResourceType.prefabs, required.Prefabs))
        {
            req.Add(ConstructionResourceType.prefabs, Prefabs - required.Prefabs);
        }
        if (!HasNeededResource(ConstructionResourceType.manastones, required.ManaStones))
        {
            req.Add(ConstructionResourceType.manastones, ManaStones - required.ManaStones);
        }
        return req;
    }

    public int ResourceCountFromType(ConstructionResourceType type)
    {
        switch (type)
        {
            case ConstructionResourceType.wood:
                return Wood;
            case ConstructionResourceType.stone:
                return Stone;
            case ConstructionResourceType.ores:
                return Ores;
            case ConstructionResourceType.naturalmaterials:
                return NaturalMaterials;
            case ConstructionResourceType.prefabs:
                return Prefabs;
            case ConstructionResourceType.manastones:
                return ManaStones;
            default:
                break;
        }
        return 0;
    }
}

