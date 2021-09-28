using System;

namespace N8Engine.Animation
{
    public abstract partial class Animation
    {
        protected readonly struct Keyframe
        {
            public static implicit operator Keyframe(Delay delay) => new(delay.Value);
            public static implicit operator Keyframe(Key key) => new(0f, key.Value);

            internal readonly float ExecutionDelay;
            private readonly Action<GameObject, float> _key;

            private Keyframe(float executionDelay, Action<GameObject, float> key = default)
            {
                ExecutionDelay = executionDelay;
                _key = key;
            }

            internal void Execute(GameObject gameObject, float deltaTime) => _key?.Invoke(gameObject, deltaTime);

            public override string ToString() => $"(delay: {ExecutionDelay}, key: {_key})";
        }

        protected readonly struct Delay
        {
            internal readonly float Value;
            
            internal Delay(float value) => Value = value;
        }

        protected readonly struct Key
        {
            internal readonly Action<GameObject, float> Value;
            
            internal Key(Action<GameObject, float> value) => Value = value;
        }

        protected Delay Wait(float delay) => new(delay);
        protected Key Do(Action action) => new((_, _) => action());
        protected Key Do(Action<GameObject> action) => new((gameObject, _) => action(gameObject));
        protected Key Do(Action<GameObject, float> action) => new(action);
    }
}