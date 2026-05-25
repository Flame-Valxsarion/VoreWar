using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum TraitTier
{
    Harmful,
    Negative,
    Neutral,
    Common,
    Uncommon,
    Rare,
    Epic,
    Elite,
    Legendary,
    Cheat,
    Hidden,
}

public class TaggedTrait
{
    internal string name;
    internal string tier;
    internal TraitTier tierValue;
    internal List<string> tags;
    internal Traits traitEnum;
}

public class ExternalTraitHandler
{
    // Loads current Tagged Trait File
    public static Dictionary<Traits, TaggedTrait> TaggedTraitParser()
    {
        string readContents;
        FileStream traitJson = File.OpenRead(State.StorageDirectory + "\\taggedTraits.json");
        using (StreamReader streamReader = new StreamReader(traitJson))
        {
            readContents = streamReader.ReadToEnd();
        }
        Dictionary<Traits, TaggedTrait> taggedTraitsList = new Dictionary<Traits, TaggedTrait>();
        JObject results = JObject.Parse(readContents);
        IList<JToken> traitList = results["traits"].Children().ToList();
        try
        {
            foreach (JToken t in traitList)
            {
                TaggedTrait trait = new TaggedTrait();
                trait.name = t["name"].ToString();
                trait.tier = t["tier"].ToString();
                trait.tags = t["tags"].ToObject<List<string>>();
                trait.tierValue = (TraitTier)Enum.Parse(typeof(TraitTier), trait.tier);
                trait.traitEnum = (Traits)Enum.Parse(typeof(Traits), trait.name);
                taggedTraitsList.Add(trait.traitEnum, trait);
                //Debug.Log(taggedTraitsList.Count);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message + "you may have edited or removed the trait in the Enum file. The entry has been removed from the file. " +
                "If this error still appears for this trait, navigate and fix the entry in ./VoreWar/UserData/taggedTraits.json or delete the file so it refreshes." +
                "Cheers! ~CSC");
        }
        traitJson.Close();
        return taggedTraitsList;
    }

    // Loads Tagged Trait File from shared assets and updates current file with any trait additions.
    // Also deletes traits from the current file that no longer exist for whatever reason.
    public static Dictionary<Traits, TaggedTrait> TaggedTraitUpdater()
    {
        // Loads Contents from Streaming Assets same as parser.
        string readContents;
        FileStream traitJson = File.OpenRead($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}taggedTraits.json");
        using (StreamReader streamReader = new StreamReader(traitJson))
        {
            readContents = streamReader.ReadToEnd();
        }
        Dictionary<Traits, TaggedTrait> updatedTaggedTraitsList = new Dictionary<Traits, TaggedTrait>(State.TieredTraitsList);
        JObject results = JObject.Parse(readContents);
        IList<JToken> traitList = results["traits"].Children().ToList();
        try
        {
            foreach (JToken t in traitList)
            {
                TaggedTrait trait = new TaggedTrait();
                trait.name = t["name"].ToString();
                trait.tier = t["tier"].ToString();
                trait.tags = t["tags"].ToObject<List<string>>();
                trait.tierValue = (TraitTier)Enum.Parse(typeof(TraitTier), trait.tier);
                trait.traitEnum = (Traits)Enum.Parse(typeof(Traits), trait.name);
                // Add anything that is not in current list
                if (!updatedTaggedTraitsList.ContainsKey(trait.traitEnum))
                {
                    updatedTaggedTraitsList.Add(trait.traitEnum, trait);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message + "There was an issue while updating tagged traits.");
        }
        traitJson.Close();
        // Check if tagged trait file exists
        if (File.Exists($"{State.StorageDirectory}taggedTraits.json"))
        {
            //Create a backup
            File.Copy($"{State.StorageDirectory}taggedTraits.json", $"{State.StorageDirectory}taggedTraits.json.bak", true);
            //Delete current
            File.Delete($"{State.StorageDirectory}taggedTraits.json");
        }

        List<TaggedTrait> newTraitList = new List<TaggedTrait>();
        var rootObject = new RootObject();
        rootObject.traits = new List<TaggedTraitTempClass>();

        // load up list to be writen
        foreach (TaggedTrait trait in updatedTaggedTraitsList.Values)
        {
            TaggedTraitTempClass toBeAdded = new TaggedTraitTempClass();
            toBeAdded.name = trait.name;
            toBeAdded.tier = trait.tier;
            toBeAdded.tier = trait.tier.ToString();
            toBeAdded.tags = trait.tags;  
            toBeAdded.traitEnum = trait.traitEnum;
            toBeAdded.tierValue = trait.tierValue;
            rootObject.traits.Add(toBeAdded);
        }

        // Write new file
        using (StreamWriter file = File.CreateText(State.StorageDirectory + "\\taggedTraits.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, rootObject);
        }
        return updatedTaggedTraitsList;
    }

    public static void AppendTaggedTrait(List<TaggedTrait> newTrait)
    {
        string json = File.ReadAllText(State.StorageDirectory + "\\taggedTraits.json");
        var rootObject = new RootObject();
        JsonConvert.PopulateObject(json, rootObject);
        foreach (TaggedTrait trait in newTrait) 
        {
            TaggedTraitTempClass toBeAdded = new TaggedTraitTempClass();
            toBeAdded.name = trait.name;
            toBeAdded.tier = trait.tier;
            toBeAdded.tier = trait.tier.ToString();
            toBeAdded.traitEnum = trait.traitEnum;
            toBeAdded.tierValue = trait.tierValue;
            rootObject.traits.Add(toBeAdded);
        }
        using (StreamWriter file = File.CreateText(State.StorageDirectory + "\\taggedTraits.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, rootObject);
        }
    }

    public static void CustomTraitParser()
    {
        string readContents;
        string[] files = Directory.GetFiles(State.CustomTraitDirectory, "*.json", SearchOption.AllDirectories);
        List<CustomTraitBoost> customTraitsList = new List<CustomTraitBoost>();

        foreach (string file in files) 
        {
            using (StreamReader streamReader = new StreamReader(file))
            {
                readContents = streamReader.ReadToEnd();
            }
            JObject results = JObject.Parse(readContents);
            
            CustomTraitBoost traitBoost = new CustomTraitBoost();
            traitBoost.id = results["id"].ToObject<int>();
            traitBoost.name = results["name"].ToString();
            traitBoost.description = results["description"].ToString();
            traitBoost.tags = results["tags"].ToObject<List<string>>();
            traitBoost.tier = results["tier"].ToObject<TraitTier>();
            traitBoost.comps = results["comps"].ToObject<Dictionary<CustomTraitComp, float>>();

            State.CustomTraitList.Add(traitBoost);

        }
    }
    public static bool CustomTraitSaver(CustomTraitBoost trait)
    {
        bool modifying = false;
        foreach (var item in State.CustomTraitList)
        {
            if (item.name.ToLower() == trait.name.ToLower() && item.id == trait.id)
            {
                modifying = true;
                break;
            }
        }

        if (!modifying && File.Exists($"{State.CustomTraitDirectory}{trait.name}.json")) 
        { 
            return false;
        }

        using (StreamWriter sw = new StreamWriter($"{State.CustomTraitDirectory}{trait.name}.json"))
        {
            sw.WriteLine(JsonConvert.SerializeObject(trait));

        }
        return true;
    }

    public static void CustomTraitRemover(CustomTraitBoost trait)
    {
        if (File.Exists($"{State.CustomTraitDirectory}{trait.name}.json"))
            File.Delete($"{State.CustomTraitDirectory}{trait.name}.json");
    }

    public static void ConditionalTraitParser()
    {
        string readContents;
        string[] files = Directory.GetFiles(State.ConditionalTraitDirectory, "*.json", SearchOption.AllDirectories);
        List<ConditionalTraitContainer> customTraitsList = new List<ConditionalTraitContainer>();

        foreach (string file in files)
        {
            using (StreamReader streamReader = new StreamReader(file))
            {
                readContents = streamReader.ReadToEnd();
            }
            JObject results = JObject.Parse(readContents);

            ConditionalTraitContainer conditionalCont = new ConditionalTraitContainer();
            conditionalCont.OperationBlocks = new Dictionary<ConditionalTraitOperationBlock, TraitConditionLogicalOperator>();
            conditionalCont.id = results["id"].ToObject<int>();
            conditionalCont.name = results["name"].ToString();
            conditionalCont.classification = results["classification"].ToObject<TraitConditionalClassification>();
            conditionalCont.trigger = results["trigger"].ToObject<TraitConditionTrigger>();
            conditionalCont.associatedTrait = results["associatedTrait"].ToObject<Traits>();
            var opBlockKeys = results["operationBlocksKeys"].ToObject<List<OperationBlockTempClass>>();
            var opBlockValues = results["operationBlocksValues"].ToObject<List<TraitConditionLogicalOperator>>();
            int counter = 0;
            foreach (var opBlockKey in opBlockKeys)
            {
                ConditionalTraitOperationBlock opBlock = new ConditionalTraitOperationBlock();
                opBlock.summary = opBlockKey.summary;
                opBlock.compareOp = opBlockKey.compareOp;
                opBlock.compareValue = opBlockKey.compareValue;
                opBlock.filled = opBlockKey.filled;
                opBlock.conditionVariable = opBlockKey.conditionVariable;
                opBlock.arithmeticOperator = opBlockKey.arithmeticOperator;

                conditionalCont.OperationBlocks.Add(opBlock, opBlockValues[counter]);
                counter++;
            }

            State.ConditionalTraitList.Add(conditionalCont);

        }
    }
    public static bool ConditionalTraitSaver(ConditionalTraitContainer trait)
    {
        ConditionalTraitRemover(trait);

        var rootObject = new ConditionalTraitTempClass();
        rootObject.id = trait.id;
        rootObject.name = trait.name;
        rootObject.classification = trait.classification;
        rootObject.trigger = trait.trigger;
        rootObject.associatedTrait = trait.associatedTrait;
        rootObject.operationBlocksKeys = new List<OperationBlockTempClass>();
        rootObject.operationBlocksValues = new List<TraitConditionLogicalOperator>();

        foreach (var item in trait.OperationBlocks)
        {
            OperationBlockTempClass opBlock = new OperationBlockTempClass();
            opBlock.summary = item.Key.summary;
            opBlock.compareOp = item.Key.compareOp;
            opBlock.compareValue = item.Key.compareValue;
            opBlock.filled = item.Key.filled;
            opBlock.conditionVariable = item.Key.conditionVariable;
            opBlock.arithmeticOperator = item.Key.arithmeticOperator;

            rootObject.operationBlocksKeys.Add(opBlock);
            rootObject.operationBlocksValues.Add(item.Value);
        }

        using (StreamWriter file = File.CreateText($"{State.ConditionalTraitDirectory}{trait.name}.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, rootObject);
        }

        return true;

    }
    public static void ConditionalTraitRemover(ConditionalTraitContainer trait)
    {
        if (File.Exists($"{State.ConditionalTraitDirectory}{trait.name}.json"))
            File.Delete($"{State.ConditionalTraitDirectory}{trait.name}.json");
    }

    public static bool UnitTagSaver(UnitTag tag)
    {
        UnitTagRemover(tag);

        var rootObject = new UnitTagTempClass();
        rootObject.id = tag.id;
        rootObject.name = tag.name;
        rootObject.modifiers = new List<UnitTagModifierTempClass>();
        foreach (var item in tag.modifiers)
        {
            var modifier = new UnitTagModifierTempClass();
            modifier.id = item.id;
            modifier.tagEffect = item.tagEffect;
            modifier.tagCase = item.tagCase;
            modifier.target = item.target;
            modifier.modifierValue = item.modifierValue;
            modifier.targetValue = item.targetValue;
            rootObject.modifiers.Add(modifier);
        }
        using (StreamWriter sw = new StreamWriter($"{State.UnitTagDirectory} {tag.name}.json"))
        {
            sw.WriteLine(JsonConvert.SerializeObject(tag));

        }
        return true;
    }

    public static void UnitTagRemover(UnitTag tag)
    {
        if (File.Exists($"{State.UnitTagDirectory}{tag.name}.json"))
            File.Delete($"{State.UnitTagDirectory}{tag.name}.json");
    }

    public static void UnitTagParser()
    {
        try
        {
            string readContents;
            string[] files = Directory.GetFiles(State.UnitTagDirectory, "*.json", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                using (StreamReader streamReader = new StreamReader(file))
                {
                    readContents = streamReader.ReadToEnd();
                }
                JObject results = JObject.Parse(readContents);

                UnitTag loadedTag = new UnitTag();
                loadedTag.AssociatedTraits = new List<Traits>();
                loadedTag.id = results["id"].ToObject<int>();
                loadedTag.name = results["name"].ToString();
                loadedTag.modifiers = results["modifiers"].ToObject<List<UnitTagModifier>>();
                loadedTag.AssociatedTraits = results["AssociatedTraits"].ToObject<List<Traits>>();
                State.UnitTagList.Add(loadedTag);

            }
        }
        catch (Exception ex) 
        { 
        
        }
    }

    class RootObject
    {
        public List<TaggedTraitTempClass> traits { get; set; }
    }
    class TaggedTraitTempClass
    {
        public string name { get; set; }
        public string tier { get; set; }
        public List<string> tags { get; set; }
        public TraitTier tierValue { get; set; }
        public Traits traitEnum { get; set; }
    }


    class ConditionalTraitTempClass
    {
        public int id { get; set; }
        public string name { get; set; }
        public TraitConditionalClassification classification { get; set; }
        public bool active { get; set; }
        public TraitConditionTrigger trigger { get; set; }
        public Traits associatedTrait { get; set; }
        public List<OperationBlockTempClass> operationBlocksKeys { get; set; }
        public List<TraitConditionLogicalOperator> operationBlocksValues { get; set; }
    }
    class OperationBlockTempClass
    {
        public string summary { get; set; }
        public TraitConditionCompareOperator compareOp { get; set; }
        public int compareValue { get; set; }
        public bool filled { get; set; }
        public List<TraitCondition> conditionVariable { get; set; }
        public List<TraitConditionArithmeticOperator> arithmeticOperator { get; set; }
    }

    class UnitTagTempClass 
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<UnitTagModifierTempClass> modifiers { get; set; }
        public List<Traits> AssociatedTraits { get; set; }
    }

    class UnitTagModifierTempClass
    {
        public int id { get; set; }
        public UnitTagModifierEffect tagEffect { get; set; }
        public UnitTagModifierCase tagCase { get; set; }
        public UnitTagModifierTarget target { get; set; }
        public float modifierValue { get; set; }
        public int targetValue { get; set; }
    }
}
