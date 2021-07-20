using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine
{
    public sealed class DummyGameObject2 : GameObject
    {
        protected override void OnStart()
        {
            SpriteRenderer.Sprite = new Sprite
            (
                @"C:\Users\NateDawg\RiderProjects\N8Engine\N8Engine\Source\Temporary\sus.n8sprite",
                SpriteRenderer
            );
            
            Collider.Size = new Vector(34, 18);
            Collider.Offset = Vector.Right * 3;
            Collider.DebugModeEnabled = true;
        }
    }
}