using System;
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
                Orientation.None => _normalSprite,
                Orientation.Horizontal => _normalSprite.FlippedHorizontally,
                Orientation.Vertical => _normalSprite.FlippedVertically,
                Orientation.HorizontalAndVertical => _normalSprite.FlippedHorizontallyAndVertically,
                var _ => _normalSprite    
            };
            set => _normalSprite = value;
        }
        public int SortingOrder { get; set; }
        public Orientation ShouldFlip { get; set; }

        internal SpriteRenderer(GameObject gameObject) : base(gameObject) { }
    }
}