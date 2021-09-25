using System;
using System.Collections.Generic;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class SpriteRenderer : Component
    {
        public Sprite Sprite { get; set; }
        public int SortingOrder { get; set; }
        public Flip ShouldFlip { get; set; }

        internal IEnumerable<Pixel> SpritePixels => ShouldFlip switch
        {
            Flip.None => Sprite.NotFlipped,
            Flip.Horizontal => Sprite.FlippedHorizontally,
            Flip.Vertical => Sprite.FlippedVertically,
            Flip.HorizontalAndVertical => Sprite.FlippedHorizontallyAndVertically,
            var _ => Sprite.NotFlipped
        };

        internal SpriteRenderer(GameObject gameObject) : base(gameObject) { }
    }
}