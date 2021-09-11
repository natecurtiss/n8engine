using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Physics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class DoorKey : GameObject
    {
        protected override void OnStart()
        {
            SpriteRenderer.Sprite = new(SpritesFolder.Path + "key.n8sprite");
            Collider.Size = new Vector(10f, 7f);
            Collider.IsTrigger = true;
        }

        public override void OnTriggeredBy(Collider otherTrigger)
        {
            if (otherTrigger.GameObject.Is<ICanCollectAKey>())
            {
                EventManager.OnKeyCollected.Invoke();
                Destroy();
            }
        }
    }
}