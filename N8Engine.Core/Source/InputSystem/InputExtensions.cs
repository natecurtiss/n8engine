using System.Numerics;
using N8Engine.Utilities;
using static N8Engine.InputSystem.Key;

namespace N8Engine.InputSystem;

// TODO: Unit tests.
public static class InputExtensions
{
    public static Vector2 Axis(this Input input) => new Vector2(input.Horizontal(), input.Vertical()).Normalized();

    public static float Horizontal(this Input input)
    {
        bool isDown(Key key) => input.IsPressed(key);
        return 
            isDown(LeftArrow) || isDown(A) ? -1f : 
            isDown(RightArrow) || isDown(D) ? 1f : 0f;
    }
    
    public static float Vertical(this Input input)
    {
        bool isDown(Key key) => input.IsPressed(key);
        return 
            isDown(DownArrow) || isDown(S) ? -1f : 
            isDown(UpArrow) || isDown(W) ? 1f : 0f;
    }
    
}