namespace N8Engine.Rendering
{
    public abstract class Animation
    {
        float _timer;
        int _currentFrame;
        Sprite[] _cachedFrames;

        protected abstract Sprite[] Frames { get; }
        protected abstract float TimeBetweenFrames { get; }

        internal Sprite[] CachedFrames => _cachedFrames ??= Frames;

        internal void Tick(SpriteRenderer spriteRenderer, float deltaTime)
        {
            _timer -= deltaTime;
            if (_timer <= 0)
            {
                _currentFrame = _currentFrame + 1 >= CachedFrames.Length ? 0 : _currentFrame + 1;
                spriteRenderer.Sprite = CachedFrames[_currentFrame];
                _timer = TimeBetweenFrames;
            }
        }

        internal void Reset()
        {
            _currentFrame = 0;
            _timer = TimeBetweenFrames;
        }
    }
}