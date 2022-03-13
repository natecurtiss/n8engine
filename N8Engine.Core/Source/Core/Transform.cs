using System.Numerics;
using N8Engine.Mathematics;
using N8Engine.Utilities;
using static System.Numerics.Matrix4x4;

namespace N8Engine;

public sealed class Transform : Component
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;
    public float Rotation { get; set; }

    public Transform AtPosition(Vector2 value)
    {
        Position = value;
        return this;
    }
    
    public Transform AtPosition(float x, float y)
    {
        Position = new(x, y);
        return this;
    }

    public Transform WithScale(Vector2 value)
    {
        Scale = value;
        return this;
    }
    
    public Transform WithScale(float x, float y)
    {
        Scale = new(x, y);
        return this;
    }
    
    public Transform WithRotation(float value)
    {
        Rotation = value;
        return this;
    }
    
    public Matrix4x4 ModelMatrix() => CreateScale(Scale.X, Scale.Y, 1f) * CreateRotationZ(Rotation.ToRadians()) * CreateTranslation(Position.X, Position.Y, 0f);
    public Bounds Bounds() => new(Position, Scale);
}