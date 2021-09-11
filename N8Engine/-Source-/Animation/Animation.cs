using N8Engine.Mathematics;

namespace N8Engine.Animation
{
    public abstract partial class Animation
    {
        public static readonly Animation Nothing = new EmptyAnimation();
        
        public static T Create<T>() where T : Animation, new()
        {
            var animation = new T();
            animation.OnInitialize();
            return animation;
        }

        internal virtual void OnChangedTo() { }
        private protected virtual void OnInitialize() { }

        internal abstract void Tick(GameObject gameObject, float deltaTime);
    }
}