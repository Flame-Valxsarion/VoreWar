using OdinSerializer;
using System;
using UnityEngine;

public class Vec2i : IEquatable<Vec2i>
{

    [OdinSerialize]
    public int x;
    [OdinSerialize]
    public int y;

    public Vec2i(int xin, int yin)
    {
        x = xin;
        y = yin;
    }

    public float GetDistance(Vec2i p)
    {
        float xe = Math.Abs(x - p.x);
        float ye = Math.Abs(y - p.y);
        xe = xe * xe;
        ye = ye * ye;

        return (float)Math.Sqrt(xe + ye);
    }

    public static Vec2i operator +(Vec2i a, Vec2i b)
    {
        return new Vec2i(a.x + b.x, a.y + b.y);
    }

    public int GetNumberOfMovesDistance(Vec2i p) => Math.Max(Math.Abs(p.x - x), Math.Abs(p.y - y));

    internal int GetNumberOfMovesDistance(Vec2 p) => Math.Max(Math.Abs(p.x - x), Math.Abs(p.y - y));

    public int GetNumberOfMovesDistance(int altX, int altY) => Math.Max(Math.Abs(altX - x), Math.Abs(altY - y));

    public bool Matches(Vec2i other)
    {
        return x == other.x && y == other.y;
    }

    public bool Matches(int otherX, int otherY)
    {
        return x == otherX && y == otherY;
    }

    public static implicit operator Vector2(Vec2i v)
    {
        return new Vector2(v.x, v.y);
    }
    
    public bool Equals(Vec2i that) => this.x == that?.x && this.y == that?.y;
    
    public override int GetHashCode() => 1000 * x + y; // Hash duplication can occur when the range of y is greater than 1000; overflows can occur when x > 2000000.
    public override string ToString() => "(" + this.x + ", " + this.y + ")";
}
