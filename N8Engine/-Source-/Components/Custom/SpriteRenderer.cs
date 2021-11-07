using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine
{
    public sealed class SpriteRenderer : Component
    {
        public override bool CanHaveMultiple => false;
        public Sprite Sprite { get; set; }

        protected override void OnRender(IRenderer renderer)
        {
            Services.Debug.Log("render");
            renderer.Render(Sprite, GameObject.Transform.Position);
        }
    }

    public static partial class GameObjectExtensions
    {
        public static GameObject SetSprite(this GameObject gameObject, Sprite sprite)
        {
            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.Sprite = sprite;
            return gameObject;
        }

        public static GameObject SetSprite(this GameObject gameObject, string path) => gameObject.SetSprite(new Sprite(path));
        public static GameObject SetSprite(this GameObject gameObject, string path, Pivot pivot) => gameObject.SetSprite(new Sprite(path, pivot));
    }
}