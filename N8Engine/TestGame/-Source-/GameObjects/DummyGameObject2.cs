using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace TestGame
{
    internal sealed class DummyGameObject2 : GameObject
    {
        private const float MAXIMUM_Y_POSITION = 50f;
        private const float SPEED = 10f;
        private Vector _direction = Vector.Up;
        
        protected override void OnStart()
        {
            SpriteRenderer.Sprite = new Sprite
            (
                PathExtensions.PathToRootFolder + "\\TestGame\\Sprites\\sus.n8sprite",
                SpriteRenderer,
                Vector.One / 2f
            );
            
            Collider.Size = new Vector(20, 15);
            Collider.Offset = Vector.Right * 3;
            // Collider.IsDebugModeEnabled = true;
        }

        protected override void OnUpdate(float deltaTime)
        {
            return;
            Collider.Velocity = _direction * SPEED;
            if
            (
                _direction == Vector.Up && Transform.Position.Y >= MAXIMUM_Y_POSITION ||
                _direction == Vector.Down && Transform.Position.Y <= -MAXIMUM_Y_POSITION
            ) _direction *= -1f;
        }
    }
}