using N8Engine.Mathematics;
using N8Engine.Physics;
using N8Engine.Rendering;

namespace N8Engine
{
    public abstract class Component
    {
        public readonly GameObject GameObject;
        public readonly Transform Transform;
        public readonly Collider Collider;
        public readonly PhysicsBody PhysicsBody;
        public readonly SpriteRenderer SpriteRenderer;
        public readonly AnimationPlayer AnimationPlayer;

        internal Component(GameObject gameObject)
        {
            GameObject = gameObject;
            Transform = gameObject.Transform;
            PhysicsBody = gameObject.PhysicsBody;
            Collider = gameObject.Collider;
            SpriteRenderer = gameObject.SpriteRenderer;
            AnimationPlayer = gameObject.AnimationPlayer;
        }
    }
}