using System.Diagnostics.CodeAnalysis;
using N8Engine.Components;

namespace N8Engine.Core
{
    [SuppressMessage("ReSharper", "RedundantAssignment")]
    public abstract class Object
    {
        internal bool IsDestroyed { get; private set; }

        public static void Destroy(GameObject gameObject)
        {
            gameObject.DestroyAllComponents();
            gameObject.IsDestroyed = true;
        }

        public static void Destroy<T>(T component) where T : Component
        {
            component.GameObject.RemoveComponent(component);
            component.IsDestroyed = true;
        }
    }
}