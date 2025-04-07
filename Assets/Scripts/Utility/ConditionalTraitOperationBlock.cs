using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class ConditionalTraitOperationBlock
{
    public string summary;

    internal TraitConditionCompareOperator compareOp;
    internal int compareValue = -1;
    internal bool filled = false;

    internal List<TraitCondition> conditionVariable = new List<TraitCondition>();
    internal List<TraitConditionArithmeticOperator> arithmeticOperator = new List<TraitConditionArithmeticOperator>();
}