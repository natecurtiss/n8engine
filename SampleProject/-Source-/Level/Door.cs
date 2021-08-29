using N8Engine;
using N8Engine.Rendering;

namespace SampleProject
{
    public sealed class Door : GameObject
    {
        bool _isAnimating;
        
        protected override void OnStart()
        {
            SpriteRenderer.SortingOrder = -1;
            SpriteRenderer.Sprite = new Sprite(SpritesFolder.Path + "door.n8sprite");
            EventManager.OnKeyCollected.AddListener(SproutUpFromTheGround);
        }

        protected override void OnDestroy() => EventManager.OnKeyCollected.RemoveListener(SproutUpFromTheGround);

        protected override void OnUpdate(float deltaTime)
        {
            if (_isAnimating)
                Transform.Position += 
        }

        void SproutUpFromTheGround() => _isAnimating = true;
    }
}