using System;
using N8Engine.Rendering;
using N8Engine.Inputs;
using N8Engine.Mathematics;
using N8Engine.Utilities;

namespace N8Engine
{
    internal sealed class DummyGameObject : GameObject
    {
        protected override void OnStart()
        {
            SpriteRenderer.Sprite = new Sprite
            (
                path: Dir.Project.Combine(single: "_sprites/sus.n8sprite"), //@"C:\Users\NateDawg\RiderProjects\N8Engine\N8Engine\Source\Temporary\sus.n8sprite",
                spriteRenderer: SpriteRenderer
            );
            Collider.Size = new Vector(x: 17, y: 18);
            Collider.Offset = Vector.Right * 2;
            Collider.DebugModeEnabled = true;
        }

        protected override void OnUpdate(in float deltaTime)
        {
            Console.Title = GameLoop.FramesPerSecond.ToString();
            Transform.Position += Input.MovementAxis * 30 * deltaTime;
        }
    }
}