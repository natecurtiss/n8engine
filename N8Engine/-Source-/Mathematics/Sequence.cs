using System;

namespace N8Engine.Mathematics
{
    public sealed class Sequence
    {
        readonly Action<float> _onTick;
        readonly float _duration;

        float _timeRemaining;
        
        public Sequence(Action<float> onTick, float duration)
        {
            _onTick = onTick;
            _duration = duration;
        }

        public void Play()
        {
            _timeRemaining = _duration;
            SequenceManager.Add(this);
        }

        public void Reset() => _timeRemaining = _duration;

        public void Resume() => SequenceManager.Add(this);
        
        public void Pause() => SequenceManager.Remove(this);

        internal void Tick(float deltaTime)
        {
            _timeRemaining -= deltaTime;
            _onTick(deltaTime);
            if (_timeRemaining <= 0f) SequenceManager.Remove(this);
        }
    }
}