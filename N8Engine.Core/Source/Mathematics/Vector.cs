using System;

namespace N8Engine.Mathematics;

[Obsolete("Use System.Numerics.Vector2 instead")]
public struct Vector : IEquatable<Vector>
{
    public static bool operator ==(Vector l, Vector r) => l.Equals(r);
    public static bool operator !=(Vector l, Vector r) => !(l == r);
    public static Vector operator +(Vector l, Vector r) => new(l.X + r.X, l.Y + r.Y);
    public static Vector operator +(Vector v) => v;
    public static Vector operator -(Vector l, Vector r) => new(l.X - r.X, l.Y - r.Y);
    public static Vector operator -(Vector v) => new(-v.X, -v.Y);
    public static Vector operator *(Vector v, float m) => new(v.X * m, v.Y * m);
    public static Vector operator *(float m, Vector v) => new(v.X * m, v.Y * m);
    public static Vector operator *(Vector l, Vector r) => new(l.X * r.X, l.Y * r.Y);
    public static Vector operator /(Vector v, float d) => new(v.X / d, v.Y / d);
    public static Vector operator /(Vector l, Vector r) => new(l.X / r.X, l.Y / r.Y);

    public static readonly Vector Zero = new();
    public static readonly Vector One = new(1f);
    public static readonly Vector Up = new(0f, 1f);
    public static readonly Vector Down = new(0f, -1f);
    public static readonly Vector Left = new(-1f, 0f);
    public static readonly Vector Right = new(1f, 0f);

    readonly float _x;
    readonly float _y;

    public float X
    {
        get => _x;
        set => this = new(value, _y);
    }
    
    public float Y
    {
        get => _y;
        set => this = new(_x, value);
    }
    
    public Vector(float x, float y)
    {
        _x = x;
        _y = y;
    }

    public Vector(float both)
    {
        _x = both;
        _y = both;
    }

    public override bool Equals(object obj) => obj is Vector other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(X, Y);
    public override string ToString() => $"({X.ToString()},{Y.ToString()})";
    public bool Equals(Vector other) => X.Equals(other.X) && Y.Equals(other.Y);
}