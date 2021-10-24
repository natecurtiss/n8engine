using N8Engine.Mathematics;

namespace N8Engine.Animation
{
    /// <summary>
    /// A base class representing an animation used by an <see cref="Animator">Animator.</see> Doesn't really make sense for you to override this unless you're modifying the engine's source.
    /// </summary>
    /// <seealso cref="FreeAnimation"/> <seealso cref="FrameByFrameAnimation"/> <seealso cref="SingleFrameAnimation"/>
    public abstract class Animation
    {
        /// <summary>
        /// A representation of nothing; used in place of null in <see cref="Animator.Play">Animator.ChangeAnimation()</see> when you want to not have any <see cref="Animation"/> playing.
        /// </summary>
        public static readonly Animation Nothing = Animation.Create<EmptyAnimation>();
        
        private int _currentKeyframeIndex;
        private float _keyframeTimer;
        private bool _isDone;
        private Sequence _cachedKeyframes;
        
        /// <summary>
        /// The user-defined <see cref="Sequence">Keyframes</see> of the <see cref="Animation">Animation.</see>
        /// </summary>
        protected abstract Sequence[] Keyframes { get; }
        /// <summary>
        /// Loops back to the start of the <see cref="Keyframes">Animation's Keyframes</see> every time after finishing the last one if true.
        /// </summary>
        protected abstract bool ShouldLoop { get; }

        private Keyframe CurrentKeyframe => _cachedKeyframes[_currentKeyframeIndex];
        private bool IsOnLastKeyframe => _currentKeyframeIndex + 1 == _cachedKeyframes.Length;
        
        /// <summary>
        /// Returns a new instance of an <see cref="Animation">Animation.</see> Do this instead of calling the constructor.
        /// </summary>
        /// <typeparam name="T"> The type of <see cref="Animation"/> to create. </typeparam>
        public static T Create<T>() where T : Animation, new()
        {
            var animation = new T();
            Debug.Log("ionit");
            animation.Initialize();
            return animation;
        }

        /// <summary>
        /// Called on the first frame of the <see cref="Animation">Animation's</see> lifetime.
        /// </summary>
        private protected virtual void OnInitialized() { }
        
        /// <summary>
        /// Called after <see cref="OnChangedTo">OnChangedTo().</see>
        /// </summary>
        private protected virtual void Reset() { }

        /// <summary>
        /// Called after the <see cref="Animation"/> is passed into <see cref="Animator.Play">Animator.ChangeAnimation().</see>
        /// </summary>
        internal void OnChangedTo()
        {
            _currentKeyframeIndex = 0;
            _keyframeTimer = CurrentKeyframe.Delay;
            _isDone = false;
            Reset();
        }

        /// <summary>
        /// Called every frame by the <see cref="Animator"/>
        /// </summary>
        /// <param name="gameObject"> The <see cref="GameObject"/> the <see cref="Animator"/> is attached to. </param>
        /// <param name="deltaTime"> The time since the last frame. </param>
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
            _cachedKeyframes = Sequence.Merge(Keyframes);
            Debug.Log(_cachedKeyframes.Length + " " + ShouldLoop);
            OnInitialized();
        }

        private void NextKeyframe()
        {
            if (IsOnLastKeyframe)
            {
                if (ShouldLoop)
                {
                    _currentKeyframeIndex = 0;
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