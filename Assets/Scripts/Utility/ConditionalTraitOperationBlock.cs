using Newtonsoft.Json;
using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class ConditionalTraitOperationBlock
{
    [OdinSerialize]
    public string summary;

    [OdinSerialize]
    internal TraitConditionCompareOperator compareOp;

    [OdinSerialize]
    internal float compareValue = -1;

    [OdinSerialize]
    internal bool filled = false;


    [OdinSerialize]
    internal List<TraitCondition> conditionVariable = new List<TraitCondition>();

    [OdinSerialize]
    internal List<TraitConditionArithmeticOperator> arithmeticOperator = new List<TraitConditionArithmeticOperator>();
}