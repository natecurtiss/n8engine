using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine
{
    public sealed class SpriteRenderer : Component
    {
        public override bool CanHaveMultiple => false;
        public Sprite Sprite { get; set; }

        private protected override void OnRenderUpdate(IRenderer renderer) => renderer.Render(Sprite, GameObject.Transform.Position);
    }

    public static partial class GameObjectExtensions
    {
        public static GameObject SetSprite(this GameObject gameObject, string path)
        {
            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.Sprite = new Sprite(path);
            return gameObject;
        }
        
        public static GameObject SetSprite(this GameObject gameObject, string path, Pivot pivot)
        {
            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.Sprite = new Sprite(path, pivot);
            return gameObject;
        }
    }
}