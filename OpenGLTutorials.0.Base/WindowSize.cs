using System.Numerics;

namespace OpenGLTutorials;

public interface WindowSize
{
    public Vector2 Value => new(Width, Height);
    int Width { get; }    
    int Height { get; }    
}