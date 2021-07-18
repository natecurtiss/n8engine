using System;
using N8Engine.Rendering;
using N8Engine.Inputs;
using N8Engine.Mathematics;

namespace N8Engine
{
    internal sealed class DummyGameObject : GameObject
    {
        protected override void OnStart()
        {
            SpriteRenderer.Sprite = new Sprite
            (
                @"C:\Users\NateDawg\RiderProjects\N8Engine\N8Engine\Source\Temporary\sus.n8sprite",
                SpriteRenderer
            );
            Collider.Size = new Vector(17, 18);
            Collider.Offset = Vector.Right * 2;
            Collider.DebugModeEnabled = true;
        }

        protected override void OnUpdate(float deltaTime)
        {
            Console.Title = GameLoop.FramesPerSecond.ToString();
            Transform.Position += Input.MovementAxis * 30 * deltaTime;
        }
    }
}