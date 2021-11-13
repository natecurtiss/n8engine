using N8Engine.Rendering;

namespace N8Engine
{
    public sealed class SpriteRenderer : Component
    {
        Renderer _renderer;
        public Sprite Sprite { get; set; }
        public int SortingOrder { get; set; }

        public SpriteRenderer(Sprite sprite = default, int sortingOrder = 0)
        {
            Sprite = sprite;
            SortingOrder = sortingOrder;
        }
        
        protected override void OnStart() => _renderer = Modules.Get<Renderer>();
        protected override void OnEnd() => Sprite = null;

        protected override void OnRender()
        {
            if (Sprite != null)
                _renderer.Render(Sprite, GameObject.Position, SortingOrder);
        }
    }
}