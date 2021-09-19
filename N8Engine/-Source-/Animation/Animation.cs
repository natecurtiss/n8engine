using N8Engine.Mathematics;

namespace N8Engine.Animation
{
    public abstract partial class Animation
    {
        internal static Animation Nothing => new EmptyAnimation();
        
        private int _currentKeyframeIndex;
        private float _keyframeTimer;
        private bool _isDone;
        
        protected abstract bool ShouldLoop { get; }
        protected abstract Keyframe[] Keyframes { get; }
        
        private Keyframe CurrentKeyframe => Keyframes[_currentKeyframeIndex];
        private bool IsOnLastKeyframe => _currentKeyframeIndex + 1 == Keyframes.Length;

        internal void OnChangedTo()
        {
            _currentKeyframeIndex = 0;
            _keyframeTimer = CurrentKeyframe.ExecutionDelay;
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

        private void NextKeyframe()
        {
            if (IsOnLastKeyframe)
            {
                if (ShouldLoop)
                {
                    _currentKeyframeIndex = 0;
                    _keyframeTimer = CurrentKeyframe.ExecutionDelay;
                }
                else
                {
                    _isDone = true;
                }
            }
            else
            {
                _currentKeyframeIndex += 1;
                _keyframeTimer = CurrentKeyframe.ExecutionDelay;
            }
        }
    }
}