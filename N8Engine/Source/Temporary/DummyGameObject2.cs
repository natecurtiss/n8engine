using N8Engine.Rendering;
using N8Engine.Utilities;

namespace N8Engine
{
    public sealed class DummyGameObject2 : GameObject
    {
        protected override void OnStart()
        {
            SpriteRenderer.Sprite = new Sprite
            (
                path: Dir.Project.Combine(single: "_sprites/sus.n8sprite"), //@"C:\Users\NateDawg\RiderProjects\N8Engine\N8Engine\Source\Temporary\sus.n8sprite",
                spriteRenderer: SpriteRenderer
            );
        }
    }
}