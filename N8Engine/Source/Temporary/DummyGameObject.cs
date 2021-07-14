using System;
using N8Engine.Rendering;

namespace N8Engine
{
    internal sealed class DummyGameObject : GameObject
    {
        protected override void OnStart() => 
            Sprite = new Sprite(@"C:\Users\NateDawg\RiderProjects\N8Engine\N8Engine\Source\Temporary\sus.n8sprite");

        protected override void OnUpdate(in float deltaTime)
        {
            Console.Title = GameLoop.FramesPerSecond.ToString();
            Position += Input.MovementAxis * 50 * deltaTime;
        }
    }
}