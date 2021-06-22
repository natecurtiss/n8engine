using System;
using N8Engine.Rendering;

namespace N8Engine.Objects
{
    public sealed class DummyInputGameObject : GameObject
    {
        protected override Sprite RenderSprite() => null;

        protected override void OnUpdate(in float deltaTime)
        {
            Console.WriteLine(Console.ReadKey().Key);
        }
    }
}