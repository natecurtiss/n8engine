using System.Numerics;
using N8Engine.Utilities;
using static System.Numerics.Matrix4x4;

namespace N8Engine;

public sealed class Transform : Component
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;
    public float Rotation { get; set; }

    public Transform() { }
    public Transform(Vector2 position) => Position = position;
    public Transform(Vector2 position, Vector2 scale) : this(position) => Scale = scale;
    public Transform(Vector2 position, Vector2 scale, float rotation) : this(position, scale) => Rotation = rotation;
    
    public Matrix4x4 ViewMatrix() => CreateScale(Scale.X, Scale.Y, 1f) * CreateRotationZ(Rotation.ToRadians()) * CreateTranslation(Position.X, Position.Y, 0f);
}