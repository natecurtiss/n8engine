using JetBrains.Annotations;

namespace N8Engine.Animation
{
    public sealed class Animator : Component
    {
        public Animation Animation { get; private set; } = Animation.Nothing;
        
        internal Animator(GameObject gameObject) : base(gameObject) { }

        public void ChangeAnimation([NotNull] Animation animation)
        {
            if (Animation == animation) return;
            Animation = animation;
            Animation.OnChangedTo();
        }

        internal void Tick(float deltaTime) => Animation.Tick(GameObject, deltaTime);
    }
}