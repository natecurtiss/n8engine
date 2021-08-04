namespace N8Engine.Rendering.Animation
{
    public sealed class AnimationPlayer
    {
        private readonly SpriteRenderer _spriteRenderer;
        private Animation _animation;

        public Animation Animation
        {
            get => _animation;
            set
            {
                if (value == _animation) return;
                _animation?.Reset();
                _animation = value;
            }
        }
        public bool IsPlaying { get; private set; }
        
        public void Play() => IsPlaying = true;

        public void Stop() => IsPlaying = false;

        internal AnimationPlayer(GameObject gameObject)
        {
            _spriteRenderer = gameObject.SpriteRenderer;
            GameLoop.OnPostUpdate += Tick;
        }

        internal void Destroy() => GameLoop.OnPostUpdate -= Tick;

        private void Tick(float deltaTime)
        {
            if (!IsPlaying) return;
            Animation?.Tick(_spriteRenderer, deltaTime);
        }
    }
}