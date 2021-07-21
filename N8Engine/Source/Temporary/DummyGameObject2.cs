using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine
{
    public sealed class DummyGameObject2 : GameObject
    {
        private const float MAXIMUM_Y_POSITION = 50f;
        private const float SPEED = 10f;
        private Vector _direction = Vector.Up;
        
        protected override void OnStart()
        {
            SpriteRenderer.Sprite = new Sprite
            (
                @"C:\Users\NateDawg\RiderProjects\N8Engine\N8Engine\Source\Temporary\sus.n8sprite",
                SpriteRenderer
            );
            
            Collider.Size = new Vector(25, 14);
            Collider.Offset = Vector.Right * 3;
            // Collider.IsDebugModeEnabled = true;
        }

        protected override void OnUpdate(float deltaTime)
        {
            // Collider.Velocity = _direction * SPEED;
            if
            (
                _direction == Vector.Up && Transform.Position.Y >= MAXIMUM_Y_POSITION ||
                _direction == Vector.Down && Transform.Position.Y <= -MAXIMUM_Y_POSITION
            ) _direction *= -1f;
        }
    }
}