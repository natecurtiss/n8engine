using JetBrains.Annotations;

namespace N8Engine.Animation
{
    /// <summary>
    /// The <see cref="Component"/> of a <see cref="GameObject"/> responsible for playing <see cref="Animation">Animations.</see>
    /// </summary>
    public sealed class Animator : Component
    {
        /// <summary>
        /// The <see cref="Animation"/> that is currently playing.
        /// </summary>
        /// <seealso cref="ChangeAnimation"/>
        public Animation Animation { get; private set; } = Animation.Nothing;
        
        /// <summary>
        /// Creates a new <see cref="Animator">Animator.</see> Don't call this from anywhere other than <see cref="GameObject">GameObject.</see>
        /// </summary>
        /// <param name="gameObject"> The <see cref="GameObject"/> the <see cref="Animator"/> is attached to. </param>
        internal Animator(GameObject gameObject) : base(gameObject) { }

        /// <summary>
        /// Changes the <see cref="Animation">Animation currently playing.</see> Don't pass in null to this-instead use <see cref="N8Engine.Animation.Animation.Nothing">Animation.Nothing.</see>
        /// </summary>
        /// <param name="animation"> The <see cref="Animation"/> to change to (cannot be null). </param>
        public void ChangeAnimation([NotNull] Animation animation)
        {
            if (Animation == animation) return;
            Animation = animation;
            Animation.OnChangedTo();
        }

        /// <summary>
        /// Called every frame by the <see cref="GameObject">Animator's GameObject.</see>
        /// </summary>
        /// <param name="deltaTime"> The time since the last frame. </param>
        internal void Tick(float deltaTime) => Animation.Tick(GameObject, deltaTime);
    }
}