using N8Engine.Mathematics;

namespace N8Engine.Components
{
    public sealed class Transform : Component, INotAddableComponent, INotRemoveableComponent
    {
        public Vector2 Position { get; set; }
    }
}