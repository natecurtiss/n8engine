using System;

namespace N8Engine.Animation
{
    public abstract class FreeAnimation : Animation
    {
        private Keyframe[] _orderedKeyframes;
        private float _elapsedTime;
        private int _currentKeyframeIndex;
        
        protected abstract Keyframe[] Keyframes { get; }
        
        private float Duration => _orderedKeyframes[^1].Time;
        private Keyframe CurrentKeyFrame => _orderedKeyframes[_currentKeyframeIndex];
        
        internal override void OnChangedTo() => _currentKeyframeIndex = 0;

        private protected override void OnInitialize()
        {
            _orderedKeyframes = new Keyframe[Keyframes.Length];
            Array.Copy(Keyframes, _orderedKeyframes, Keyframes.Length);
            Array.Sort(_orderedKeyframes, (first, second) => first.Time.CompareTo(second.Time));
        }

        internal override void Tick(GameObject gameObject, float deltaTime)
        {
            if (_elapsedTime >= Duration) return;
            
            _elapsedTime += deltaTime;
            if (_elapsedTime >= CurrentKeyFrame.Time)
            {
                CurrentKeyFrame.OnTick(gameObject, deltaTime);
                if (_elapsedTime >= Duration && ShouldLoop)
                {
                    _currentKeyframeIndex = 0;
                    _elapsedTime = 0f;
                }
            }
        }
    }
}