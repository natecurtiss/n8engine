using N8Engine.Mathematics;
using N8Engine.Physics;
using N8Engine.Rendering;

namespace N8Engine
{
    public sealed class DummyGameObject2 : GameObject
    {
        protected override void OnStart()
        {
            Sprite = new Sprite(@"C:\Users\NateDawg\RiderProjects\N8Engine\N8Engine\Source\Temporary\sus.n8sprite");
            BoxCollider __boxCollider = Collider.Create<BoxCollider>(this);
            __boxCollider.Size = new Vector(50, 50);
        }
    }
}