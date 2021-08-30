namespace N8Engine.Rendering
{
    public sealed class AnimationPlayer : Component
    {
        private readonly SpriteRenderer _spriteRenderer;
        private Animation _animation;

        public bool IsPlaying { get; private set; }
        public Animation Animation
        {
            get => _animation;
            set
            {
                if (value == _animation) return;
                _animation?.Reset();
                _animation = value;
                _spriteRenderer.Sprite = _animation.CachedFrames[0];
            }
        }

        internal AnimationPlayer(GameObject gameObject) : base(gameObject) => _spriteRenderer = gameObject.SpriteRenderer;

        public void Play() => IsPlaying = true;

        public void Stop() => IsPlaying = false;

        internal void Tick(float deltaTime)
        {
            if (!IsPlaying) return;
            Animation?.Tick(_spriteRenderer, deltaTime);
        }
    }
}