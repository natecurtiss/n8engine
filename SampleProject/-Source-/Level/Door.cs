using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class Door : GameObject
    {
        const float SPROUT_DISTANCE = -9f;
        const float SPROUT_DURATION = 0.6f;

        protected override void OnStart()
        {
            SpriteRenderer.SortingOrder = -1;
            SpriteRenderer.Sprite = new Sprite(SpritesFolder.Path + "door.n8sprite");
            EventManager.OnKeyCollected.AddListener(SproutUpFromTheGround);
        }

        protected override void OnDestroy() => EventManager.OnKeyCollected.RemoveListener(SproutUpFromTheGround);

        void SproutUpFromTheGround() => Transform.MoveInDirection(Direction.Up, SPROUT_DISTANCE, SPROUT_DURATION);
    }
}