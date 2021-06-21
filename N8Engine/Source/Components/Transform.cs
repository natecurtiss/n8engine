using N8Engine.Mathematics;

namespace N8Engine.Components
{
    public sealed class Transform : Component, INotAddableComponent, INotRemovableComponent
    {
        public Vector2 Position { get; set; }
    }
}