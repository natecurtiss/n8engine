using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using N8Engine.Mathematics;

namespace N8Engine.Animation
{
    public abstract class Animation
    {
        internal static readonly Animation Nothing = Animation.Create<EmptyAnimation>();
        
        private int _currentKeyframeIndex;
        private float _keyframeTimer;
        private bool _isDone;
        private Sequence _allKeyframes;
        
        protected abstract Sequence[] Keyframes { get; }
        protected virtual bool ShouldLoop { get; } = false;

        private Keyframe CurrentKeyframe => _allKeyframes[_currentKeyframeIndex];
        private bool IsOnLastKeyframe => _currentKeyframeIndex + 1 == _allKeyframes.Length;

        public static T Create<T>() where T : Animation, new()
        {
            var animation = new T();
            animation.Initialize();
            return animation;
        }
        
        private protected virtual void OnInitialized() { }

        internal void OnChangedTo()
        {
            _currentKeyframeIndex.Reset();
            _keyframeTimer = CurrentKeyframe.Delay;
            _isDone = false;
        }

        internal void Tick(GameObject gameObject, float deltaTime)
        {
            if (_isDone) return;
            
            _keyframeTimer = (_keyframeTimer - deltaTime).KeptAbove(0f);
            if (_keyframeTimer == 0)
            {
                CurrentKeyframe.Execute(gameObject, deltaTime);
                NextKeyframe();
            }
        }

        private void Initialize()
        {
            _allKeyframes = Sequence.Merge(Keyframes);
            OnInitialized();
        }

        private void NextKeyframe()
        {
            if (IsOnLastKeyframe)
            {
                if (ShouldLoop)
                {
                    _currentKeyframeIndex.Reset();
                    _keyframeTimer = CurrentKeyframe.Delay;
                }
                else
                {
                    _isDone = true;
                }
            }
            else
            {
                _currentKeyframeIndex += 1;
                _keyframeTimer = CurrentKeyframe.Delay;
            }
        }
    }
}