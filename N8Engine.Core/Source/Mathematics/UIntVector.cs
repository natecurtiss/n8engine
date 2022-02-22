using System;

namespace N8Engine.Mathematics;

public struct UIntVector : IEquatable<UIntVector>
{
    public static bool operator ==(UIntVector l, UIntVector r) => l.Equals(r);
    public static bool operator !=(UIntVector l, UIntVector r) => !(l == r);
    public static UIntVector operator +(UIntVector l, UIntVector r) => new(l.X + r.X, l.Y + r.Y);
    public static UIntVector operator +(UIntVector v) => v;
    public static UIntVector operator -(UIntVector l, UIntVector r) => new(l.X - r.X, l.Y - r.Y);
    public static UIntVector operator *(UIntVector v, uint m) => new(v.X * m, v.Y * m);
    public static UIntVector operator *(uint m, UIntVector v) => new(v.X * m, v.Y * m);
    public static UIntVector operator *(UIntVector l, UIntVector r) => new(l.X * r.X, l.Y * r.Y);
    public static UIntVector operator /(UIntVector v, uint d) => new(v.X / d, v.Y / d);
    public static UIntVector operator /(UIntVector l, UIntVector r) => new(l.X / r.X, l.Y / r.Y);

    public static readonly UIntVector Zero = new();
    public static readonly UIntVector One = new(1);
    public static readonly UIntVector Up = new(0, 1);
    public static readonly UIntVector Right = new(1, 0);

    readonly uint _x;
    readonly uint _y;

    public uint X
    {
        get => _x;
        set => this = new(value, _y);
    }
    
    public uint Y
    {
        get => _y;
        set => this = new(_x, value);
    }
    
    public UIntVector(uint x, uint y)
    {
        _x = x;
        _y = y;
    }

    public UIntVector(uint both)
    {
        _x = both;
        _y = both;
    }

    public override bool Equals(object obj) => obj is UIntVector other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(X, Y);
    public override string ToString() => $"({X.ToString()},{Y.ToString()})";
    public bool Equals(UIntVector other) => X.Equals(other.X) && Y.Equals(other.Y);
}