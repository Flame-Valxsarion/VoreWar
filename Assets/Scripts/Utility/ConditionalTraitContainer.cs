using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class ConditionalTraitContainer
{
    public int id = -1;
    public string name;
    public TraitConditionalClassification classification;
    public TraitConditionTrigger trigger;

    public Traits associatedTrait;

    public Dictionary<ConditionalTraitOperationBlock, TraitConditionLogicalOperator> OperationBlocks;
}