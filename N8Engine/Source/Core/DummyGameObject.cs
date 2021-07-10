using System;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine
{
    public sealed class DummyGameObject : GameObject
    {
        protected override void OnStart()
        {
            Sprite = new Sprite(@"C:\Users\NateDawg\RiderProjects\N8Engine\N8Engine\Source\Core\sus.n8sprite");
        }

        protected override void OnUpdate(in float deltaTime)
        {
            Console.Title = GameLoop.FramesPerSecond.ToString();
            Position += Vector2.Left * deltaTime;
        }
    }
}