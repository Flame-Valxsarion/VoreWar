using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class RandomizeList
{
    public int id = -1;
    public string name;
    public float chance;
    public int count;
    public int level;
    [AllowEditing]
    internal List<Traits> RandomTraits;

    internal void Save()
    {
       
    }

    public override string ToString()
    {
        string str = id + ", " + name + ", " + chance.ToString("N", new CultureInfo("en-US")) + ", " + count + ", " + level + ", ";
        RandomTraits.ForEach(rt => str += (int)rt + "|");
        str = str.Remove(str.Length - 1);
        return str;
    }

}