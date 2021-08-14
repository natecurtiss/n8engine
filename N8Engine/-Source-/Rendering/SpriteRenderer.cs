namespace N8Engine.Rendering
{
    public sealed class SpriteRenderer : Component
    {
        public Sprite Sprite { get; set; }
        public int SortingOrder { get; set; }

        internal SpriteRenderer(GameObject gameObject) : base(gameObject) { }
    }
}