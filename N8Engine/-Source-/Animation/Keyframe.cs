using System;

namespace N8Engine.Animation
{
    internal readonly struct Keyframe
    {
        public readonly float Delay;
        private readonly Action<GameObject, float> _action;
        
        public Keyframe(float delay, Action<GameObject, float> action)
        {
            Delay = delay;
            _action = action;
        }

        public void Execute(GameObject gameObject, float deltaTime) => _action(gameObject, deltaTime);
    }
}