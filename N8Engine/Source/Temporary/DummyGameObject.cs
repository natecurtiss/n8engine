using System;
using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine
{
    internal sealed class DummyGameObject : GameObject
    {
        private int _frames;
        
        protected override void OnStart()
        {
            Sprite = new Sprite(@"C:\Users\NateDawg\RiderProjects\N8Engine\N8Engine\Source\Temporary\sus.n8sprite");
        }

        protected override void OnUpdate(in float deltaTime)
        {
            _frames++;
            if (_frames == 3)
            {
                
            }
            Console.Title = GameLoop.FramesPerSecond.ToString();
            Position += Vector2.Left * deltaTime * -30;
        }
    }
}