using JetBrains.Annotations;
using static N8Engine.Animation.Animation;

namespace N8Engine.Animation
{
    public sealed class Animator : Component
    {
        public Animation Animation { get; private set; } = Nothing;
        
        internal Animator(GameObject gameObject) : base(gameObject) { }

        public void ChangeAnimation([NotNull] Animation animation)
        {
            Animation = animation;
            Animation.OnChangedTo();
        }

        internal void Tick(float deltaTime) => Animation.Tick(GameObject, deltaTime);
    }
}