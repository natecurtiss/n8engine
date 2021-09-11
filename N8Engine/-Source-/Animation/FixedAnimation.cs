using System;
using N8Engine.Mathematics;

namespace N8Engine.Animation
{
    public abstract class FixedAnimation : Animation
    {
        private float _timeRemainingUntilNextKeyframe;
        private int _elapsedKeyframes;

        protected abstract Action<GameObject, float>[] Keyframes { get; }
        protected abstract float TimeBetweenEachKeyframe { get; }
        protected abstract bool ShouldLoop { get; }

        internal override void OnChangedTo() => Reset();

        internal override void Tick(GameObject gameObject, float deltaTime)
        {
            if (_elapsedKeyframes >= Keyframes.Length)
                if (ShouldLoop) 
                    Reset();
                else 
                    return;

            _timeRemainingUntilNextKeyframe = (_timeRemainingUntilNextKeyframe - deltaTime).KeptAboveZero();
            if (_timeRemainingUntilNextKeyframe == 0f)
            {
                _elapsedKeyframes++;
                Keyframes[_elapsedKeyframes](gameObject, deltaTime);
                _timeRemainingUntilNextKeyframe = TimeBetweenEachKeyframe;
            }
        }

        private void Reset()
        {
            _timeRemainingUntilNextKeyframe = TimeBetweenEachKeyframe;
            _elapsedKeyframes = 0;
        }
    }
}