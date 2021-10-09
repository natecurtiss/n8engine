using N8Engine.Rendering;

namespace N8Engine.Animation
{
    public abstract class SingleFrameAnimation : Animation
    {
        private Sprite _cachedFrame;
        
        protected override Sequence[] Keyframes => new[]
        {
            new Sequence().Do(gameObject => gameObject.SpriteRenderer.Sprite = _cachedFrame)
        };
        
        protected abstract Sprite Sprite { get; }

        private protected override void OnInitialized() => _cachedFrame = Sprite;
    }
}