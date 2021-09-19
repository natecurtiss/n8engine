using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class SpriteRenderer : Component
    {
        private Sprite _normalSprite;

        public Sprite Sprite
        {
            get => ShouldFlip switch
            {
                Flip.None => _normalSprite,
                Flip.Horizontal => _normalSprite.FlippedHorizontally,
                Flip.Vertical => _normalSprite.FlippedVertically,
                Flip.HorizontalAndVertical => _normalSprite.FlippedHorizontallyAndVertically,
                var _ => _normalSprite    
            };
            set => _normalSprite = value;
        }
        public int SortingOrder { get; set; }
        public Flip ShouldFlip { get; set; }

        internal SpriteRenderer(GameObject gameObject) : base(gameObject) { }
    }
}