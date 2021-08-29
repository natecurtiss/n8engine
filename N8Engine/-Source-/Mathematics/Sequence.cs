using System;

namespace N8Engine.Mathematics
{
    public sealed class Sequence
    {
        readonly Action<float> _onTick;
        readonly float _duration;

        Action _onComplete;
        float _timeRemaining;
        
        public bool IsPlaying { get; private set; }
        
        public Sequence(Action<float> onTick, float duration)
        {
            _onTick = onTick;
            _duration = duration;
        }

        public void Play()
        {
            IsPlaying = true;
            _timeRemaining = _duration;
            SequenceManager.Add(this);
        }
        
        public void Kill()
        {
            IsPlaying = false;
            SequenceManager.Remove(this);
            _onComplete?.Invoke();
        }

        public Sequence OnComplete(Action onComplete)
        {
            _onComplete += onComplete;
            return this;
        }

        internal void Tick(float deltaTime)
        {
            _onTick(deltaTime);
            _timeRemaining -= deltaTime;
            if (_timeRemaining <= 0f) Kill();
        }
    }
}