using N8Engine;

namespace SampleProject
{
    public sealed class Door : GameObject
    {
        private const float SPROUT_DISTANCE = -9f;
        private const float SPROUT_DURATION = 0.6f;

        protected override void OnStart()
        {
            SpriteRenderer.SortingOrder = -1;
            SpriteRenderer.Sprite = new(SpritesFolder.Path + "door.n8sprite");
            EventManager.OnKeyCollected.AddListener(SproutUpFromTheGround);
        }

        protected override void OnDestroy() => EventManager.OnKeyCollected.RemoveListener(SproutUpFromTheGround);

        private void SproutUpFromTheGround() { }
    }
}