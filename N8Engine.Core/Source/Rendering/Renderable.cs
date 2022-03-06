using System.Numerics;

namespace N8Engine.Rendering;

interface Renderable
{
    Camera Camera { get; }
    Texture Texture { get; }
    Vector2 Position { get; }
    Vector2 Scale { get; }
    float Rotation { get; }
}