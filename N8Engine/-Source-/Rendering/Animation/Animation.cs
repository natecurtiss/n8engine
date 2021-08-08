namespace N8Engine.Rendering.Animation
{
    public abstract class Animation
    {
        private float _timer;
        private int _currentFrame;

        protected abstract Sprite[] Frames(SpriteRenderer spriteRenderer);
        
        protected abstract float TimeBetweenFrames();

        internal void Start(SpriteRenderer spriteRenderer) => spriteRenderer.Sprite = Frames(spriteRenderer)[0];

        internal void Tick(SpriteRenderer spriteRenderer, float deltaTime)
        {
            _timer -= deltaTime;
            if (_timer <= 0)
            {
                _currentFrame = _currentFrame + 1 >= Frames(spriteRenderer).Length ? 0 : _currentFrame + 1;
                spriteRenderer.Sprite = Frames(spriteRenderer)[_currentFrame];
                _timer = TimeBetweenFrames();
            }
        }

        internal void Reset()
        {
            _currentFrame = 0;
            _timer = TimeBetweenFrames();
        }
    }
}