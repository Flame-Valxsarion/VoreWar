using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public static class TaggedTraitUtilities
{
    public static Traits GetRandomTraitInTier(TraitTier tier)
    {
        List<TaggedTrait> traitsInTier = State.TieredTraitsList.Values.Where(t=> t.tierValue == tier).ToList();
        if (traitsInTier.Count <= 0)
        {
            return (Traits)(-1);
        }
        Traits returnTrait = traitsInTier[State.Rand.Next(traitsInTier.Count)].traitEnum;
        return returnTrait;
    }
}
