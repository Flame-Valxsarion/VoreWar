using System;
using System.Collections.Generic;

/*
A class implementing the classic "marbles in a bag" probability scenario (where "marble" stands for whatever type you use as the parameter for the WeightedList).

Technically, not a List, but a Set; "WeightedList" as a class name is fit to be replaced in the next clean sweep of code.

Internally, there's a lot of mention of "marbles", but in function calls, you deal with "entries" (which are active) and "set asides" (which are inactive).

Exceptions:
This class will throw a new InvalidOperationException on construction, if the class is type-parametered with a class that doesn't implement IEquatable (all value-types are fine, as they aren't classes).
This class will also throw a new InvalidOperationException when any method call — except Add() — tries to modify anything for a result that is not in the WeightedList at all.

Other edge cases:
When adding, moving, or removing marbles, you can pass negative numbers; the function will execute opposite the function's name (e.g., removing 3 marbles when you call Add(result, -3)).
When you move or remove more marbles from a location than there are present in that location, the function will move or remove only as many marbles as are present.
Any attempt to Draw from a WeightedList with no results in it at all will return a default(T).
Any attempt to Draw from a WeightedList when are results, but no results with "marbles in the bag" will return one result (from the set of results) picked at random.
*/

public class WeightedList<T>
{
    private class MarbleSet
    {
        public T result;
        private int _bag;
        public int marbles_in_bag { get => _bag; set => _bag = Math.Max(value, 0); }
        private int _aside;
        public int marbles_set_aside { get => _aside; set => _aside = Math.Max(value, 0); }
        
        public MarbleSet(T initResult, int initWeight = 0)
        {
            result = initResult;
            marbles_in_bag = initWeight;
            marbles_set_aside = 0;
        }
    }
    
    private List<MarbleSet> bag;
    
    public WeightedList()
    {
        if (typeof(T).IsValueType || new List<Type>(typeof(T).GetInterfaces()).Contains(typeof(IEquatable<T>))) // Awkward, but in short: resolves to TRUE for value-types, and for classes that implement IEquatable.
            bag = new List<MarbleSet>();
        else
            throw new InvalidOperationException("Cannot create WeightedList<" + typeof(T).Name + ">; type parameter T needs to be a value-type, or implement IEquatable.");
    }

    private int? FindIndex(T result)
    {
        for (int index = 0; index < bag.Count; ++index)
        {
            T indexResult = bag[index].result;
            if (EqualityComparer<T>.Default.Equals(indexResult, result))
                return index;
        }
        
        return null;
    }

    public void Add(T result, int weight = 1)
    {
        int? index = FindIndex(result);
        if (index is int i)
            bag[i].marbles_in_bag += weight;
        else
            bag.Add(new MarbleSet(result, weight));
    }
    public void SetAside(T result, int weight = 1)
    {
        if (weight < 0)
        {
            Replace(result, -weight);
            return;
        }
        
        int? index = FindIndex(result);
        if (index is int i)
        {
            weight = Math.Min(weight, bag[i].marbles_in_bag);
            bag[i].marbles_in_bag -= weight;
            bag[i].marbles_set_aside += weight;
        }
        else
        {
            throw new InvalidOperationException("In WeightedList<" + typeof(T).Name + ">, attempted to set aside " + weight + " entries of result " + result.ToString() + " when that result is not present.");
        }
    }
    public void SetAsideAll(T result)
    {
        int? index = FindIndex(result);
        if (index is int i)
        {
            bag[i].marbles_set_aside += bag[i].marbles_in_bag;
            bag[i].marbles_in_bag = 0;
        }
        else
        {
            throw new InvalidOperationException("In WeightedList<" + typeof(T).Name + ">, attempted to set aside all entries of result " + result.ToString() + " when that result is not present.");
        }
    }
    public void Replace(T result, int weight = 1)
    {
        if (weight < 0)
        {
            SetAside(result, -weight);
            return;
        }
        
        int? index = FindIndex(result);
        if (index is int i)
        {
            weight = Math.Min(weight, bag[i].marbles_set_aside);
            bag[i].marbles_set_aside -= weight;
            bag[i].marbles_in_bag += weight;
        }
        else
        {
            throw new InvalidOperationException("In WeightedList<" + typeof(T).Name + ">, attempted to replace " + weight + " set asides of result " + result.ToString() + " when that result is not present.");
        }
    }
    public void ReplaceAll(T result)
    {
        int? index = FindIndex(result);
        if (index is int i)
        {
            bag[i].marbles_in_bag += bag[i].marbles_set_aside;
            bag[i].marbles_set_aside = 0;
        }
        else
        {
            throw new InvalidOperationException("In WeightedList<" + typeof(T).Name + ">, attempted to replace all set asides of result " + result.ToString() + " when that result is not present.");
        }
    }
    public void Discard(T result, int weight = 1)
    {
        int? index = FindIndex(result);
        if (index is int i)
            bag[i].marbles_in_bag -= weight;
        else
            throw new InvalidOperationException("In WeightedList<" + typeof(T).Name + ">, attempted to discard " + weight + " entries of result " + result.ToString() + " when that result is not present.");
    }
    public void DiscardAll(T result)
    {
        int? index = FindIndex(result);
        if (index is int i)
            bag[i].marbles_in_bag = 0;
        else
            throw new InvalidOperationException("In WeightedList<" + typeof(T).Name + ">, attempted to remove all entries of result " + result.ToString() + " when that result is not present.");
    }
    public void DiscardSetAside(T result, int weight = 1)
    {
        int? index = FindIndex(result);
        if (index is int i)
            bag[i].marbles_set_aside -= weight;
        else
            throw new InvalidOperationException("In WeightedList<" + typeof(T).Name + ">, attempted to discard " + weight + " set asides of result " + result.ToString() + " when that result is not present.");
    }
    public void DiscardAllSetAside(T result)
    {
        int? index = FindIndex(result);
        if (index is int i)
            bag[i].marbles_set_aside = 0;
        else
            throw new InvalidOperationException("In WeightedList<" + typeof(T).Name + ">, attempted to discard all set asides of result " + result.ToString() + " when that result is not present.");
    }
    
    public void Replenish()
    {
        foreach (MarbleSet marble in bag)
        {
            marble.marbles_in_bag += marble.marbles_set_aside;
            marble.marbles_set_aside = 0;
        }
    }
    
    public void RemoveResult(T result)
    {
        int? index = FindIndex(result);
        if (index is int i)
            bag.RemoveAt(i);
        else
            throw new InvalidOperationException("In WeightedList<" + typeof(T).Name + ">, attempted to remove result " + result.ToString() + " when that result is not present.");
    }
    
    public void Clear()
    {
        bag.Clear();
    }
    
    private int? GetResultIndex()
    {
        int count = bag.Count;
        if (count < 1)
            return null;
        if (count == 1)
            return 0;
        
        int totalweight = 0;
        foreach (MarbleSet marble in bag)
        {
            int marbles = marble.marbles_in_bag;
            if (marbles > 0)
                totalweight += marble.marbles_in_bag;
        }
        if (totalweight == 0)
            return State.Rand.Next(bag.Count); // If no result is represented with a positive weight, just pick one at random.
        
        int roll = State.Rand.Next(totalweight);
        int accumulator = 0;
        for (int index = 0; index < count; index++)
        {
            int marbles = bag[index].marbles_in_bag;
            if (marbles > 0)
            {
                accumulator += marbles;
                if (roll < accumulator)
                    return index;
            }
        }
        
        return null;
    }
    
    public bool Contains(T result)
    {
        return FindIndex(result) != null;
    }
    public int? GetResultEntryCount(T result)
    {
        int? index = FindIndex(result);
        if (index is int i)
            return bag[i].marbles_in_bag;
        else
            return null;
    }
    public int? GetResultSetAsideCount(T result)
    {
        int? index = FindIndex(result);
        if (index is int i)
            return bag[i].marbles_set_aside;
        else
            return null;
    }
    public double? GetResultProbability(T result)
    {
        int? index = FindIndex(result);
        if (index is int i)
        {
            int count = GetTotalEntryCount();
            if (count > 0)
                return ((double)bag[i].marbles_in_bag) / count;
            else
                return 1d / bag.Count;
        }
        else
            return null;
    }
    
    public List<T> ResultList()
    {
        List<T> retval = new List<T>();
        foreach (MarbleSet marble in bag)
        {
            retval.Add(marble.result);
        }
        return retval;
    }
    public int GetTotalEntryCount()
    {
        int retval = 0;
        foreach (MarbleSet marble in bag)
        {
            retval += marble.marbles_in_bag;
        }
        return retval;
    }
    public int GetTotalSetAsideCount()
    {
        int retval = 0;
        foreach (MarbleSet marble in bag)
        {
            retval += marble.marbles_set_aside;
        }
        return retval;
    }
    
    public T DrawResult()
    {
        int? index = GetResultIndex();
        if (index is int i)
            return bag[i].result;
        else
            return default(T);
    }
    public T DrawResultAndSetAside()
    {
        int? index = GetResultIndex();
        if (index is int i)
        {
            bag[i].marbles_set_aside += 1;
            bag[i].marbles_in_bag -= 1;
            return bag[i].result;
        }
        else
            return default(T);
    }
    public T DrawResultAndSetAsideAll()
    {
        int? index = GetResultIndex();
        if (index is int i)
        {
            bag[i].marbles_set_aside += bag[i].marbles_in_bag;
            bag[i].marbles_in_bag = 0;
            return bag[i].result;
        }
        else
            return default(T);
    }
    public T DrawResultAndDiscard()
    {
        int? index = GetResultIndex();
        if (index is int i)
        {
            bag[i].marbles_in_bag -= 1;
            return bag[i].result;
        }
        else
            return default(T);
    }
    public T DrawResultAndDiscardAll()
    {
        int? index = GetResultIndex();
        if (index is int i)
        {
            bag[i].marbles_in_bag = 0;
            return bag[i].result;
        }
        else
            return default(T);
    }
    public T DrawResultAndRemoveResult()
    {
        int? index = GetResultIndex();
        if (index is int i)
        {
            T retval = bag[i].result;
            bag.RemoveAt(i);
            return retval;
        }
        else
            return default(T);
    }

    public override string ToString()
    {
        if (bag.Count == 0)
            return "Empty WeightedList<" + typeof(T).Name + ">";
        
        bool emptyflag = false;
        int count = GetTotalEntryCount();
        if (count == 0)
        {
            emptyflag = true;
            count = bag.Count;
        }
        
        string retval = "";
        for (int i = 0; i < bag.Count; ++i)
        {
            if (i > 0)
                retval += "|";
            
            retval += "<" + bag[i].result.ToString() + ">";
            
            double probability;
            if (emptyflag)
                probability = 100d / count;
            else
                probability = 100d * bag[i].marbles_in_bag / count;
            int significance = (int)Math.Log(probability, 10);
            retval += "(" + probability.ToString("F" + (1 - significance)) + "%)";
        }
        return retval;
    }
    public string ToStringVerbose()
    {
        if (bag.Count == 0)
            return "Empty WeightedList<" + typeof(T).Name + ">";
        
        string retval = "";
        for (int i = 0; i < bag.Count; ++i)
        {
            if (i > 0)
                retval += "|";
            
            retval += "<" + bag[i].result.ToString() + ">";
            retval += "(" + bag[i].marbles_in_bag + "," + bag[i].marbles_set_aside + ")";
        }
        return retval;
    }
}
