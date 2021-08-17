namespace N8Engine.Rendering.Animation
{
    public abstract class Animation
    {
        private float _timer;
        private int _currentFrame;
        private Sprite[] _cachedFrames;

        protected abstract Sprite[] Frames { get; }
        protected abstract float TimeBetweenFrames { get; }
        
        internal void Tick(SpriteRenderer spriteRenderer, float deltaTime)
        {
            var frames = _cachedFrames ??= Frames;
            _timer -= deltaTime;
            if (_timer <= 0)
            {
                _currentFrame = _currentFrame + 1 >= frames.Length ? 0 : _currentFrame + 1;
                spriteRenderer.Sprite = frames[_currentFrame];
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