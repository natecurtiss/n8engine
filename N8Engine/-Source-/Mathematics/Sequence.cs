using System;

namespace N8Engine.Mathematics
{
    public sealed class Sequence
    {
        private readonly Action<float> _onTick;
        private readonly float _duration;

        private Action _onComplete;
        private float _timeRemaining;

        public Sequence(Action<float> onTick, float duration)
        {
            _onTick = onTick;
            _duration = duration;
        }

        public void Play()
        {
            SequenceManager.Add(this);
            _timeRemaining = _duration;
        }
        
        public void Kill()
        {
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