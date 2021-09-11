using N8Engine.Mathematics;

namespace N8Engine.Animation
{
    public abstract class RepeatingAnimation : Animation
    {
        private float _timeRemainingUntilNextKeyframe;
        private int _elapsedKeyframes;

        protected abstract float TimeBetweenEachKeyframe { get; }
        protected virtual int NumberOfKeyframes { get; } = 0;

        protected abstract void OnEveryKeyframe(GameObject gameObject, float deltaTime);

        internal override void OnChangedTo() => Reset();

        internal override void Tick(GameObject gameObject, float deltaTime)
        {
            if (_elapsedKeyframes >= NumberOfKeyframes)
                if (ShouldLoop) 
                    Reset();
                else 
                    return;

            _timeRemainingUntilNextKeyframe = (_timeRemainingUntilNextKeyframe - deltaTime).KeptAboveZero();
            if (_timeRemainingUntilNextKeyframe == 0f)
            {
                _elapsedKeyframes++;
                OnEveryKeyframe(gameObject, deltaTime);
                Reset();
            }
        }

        private void Reset()
        {
            _timeRemainingUntilNextKeyframe = TimeBetweenEachKeyframe;
            _elapsedKeyframes = 0;
        }
    }
}