using JetBrains.Annotations;

namespace N8Engine.Animation
{
    public sealed class Animator : Component
    {
        private Animation _animation;
        
        internal Animator(GameObject gameObject) : base(gameObject) { }

        public void ChangeAnimation([NotNull] Animation animation) => _animation = animation;

        internal void Tick(float deltaTime) => _animation.Tick(GameObject, deltaTime);
    }
}