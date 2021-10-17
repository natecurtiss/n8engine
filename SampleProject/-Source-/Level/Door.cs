using N8Engine;
using N8Engine.Animation;
using N8Engine.Mathematics;
using N8Engine.Physics;
using N8Engine.SceneManagement;

namespace SampleProject
{
    public sealed class Door : GameObject
    {
        private readonly DoorSproutFromTheGround _sproutAnimation = Animation.Create<DoorSproutFromTheGround>();
        
        protected override void OnStart()
        {
            SpriteRenderer.SortingOrder = -1;
            SpriteRenderer.Sprite = new(SpritesFolder.Path + "door.png");
            Collider.Size = new Vector(8f, 10f);
            Collider.IsTrigger = true;
            EventManager.OnKeyCollected.AddListener(SproutUpFromTheGround);
        }

        protected override void OnDestroy() => EventManager.OnKeyCollected.RemoveListener(SproutUpFromTheGround);

        public override void OnTriggeredBy(Collider otherTrigger)
        {
            if (otherTrigger.GameObject.Is<ICanEnterTheDoor>())
                SceneManager.LoadNextScene();
        }

        private void SproutUpFromTheGround() => Animator.ChangeAnimation(_sproutAnimation);
    }

    public sealed class DoorSproutFromTheGround : FreeAnimation
    {
        private const float SPEED = 0.2f;
        private const float DISTANCE = 10f;

        protected override Sequence[] Keyframes => new[]
        {
            new Sequence()
                .Do(door => door.Transform.Position += Vector.Up * SPEED)
                .Repeat((DISTANCE / SPEED).Rounded())
        };
        protected override bool ShouldLoop => false;
    }
}