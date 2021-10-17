using System;

namespace N8Engine.Animation
{
    /// <summary>
    /// A structure representing an <see cref="Animation"/> keyframe.
    /// </summary>
    internal readonly struct Keyframe
    {
        /// <summary>
        /// The amount of time to wait before executing.
        /// </summary>
        public readonly float Delay;
        private readonly Action<GameObject, float> _action;
        
        /// <summary>
        /// Creates a keyframe with <see cref="Delay"/> and an action to execute.
        /// </summary>
        /// <param name="delay"> The amount of time to wait before executing. </param>
        /// <param name="action"> The action to execute after the delay. </param>
        public Keyframe(float delay, Action<GameObject, float> action)
        {
            Delay = delay;
            _action = action;
        }

        /// <summary>
        /// Executes the action passed into the constructor.
        /// </summary>
        /// <param name="gameObject"> The <see cref="GameObject"/> the <see cref="Animator">Animation's Animator</see> is attached to. </param>
        /// <param name="deltaTime"> The amount of time since the previous frame. </param>
        public void Execute(GameObject gameObject, float deltaTime) => _action(gameObject, deltaTime);
    }
}