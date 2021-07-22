using N8Engine.Mathematics;

namespace N8Engine
{
    internal static class DummyGame
    {
        public static void Start()
        {
            var firstGameObject = GameObject.Create<DummyGameObject>();
            firstGameObject.Transform.Position += Vector.Right * 100;
            var secondGameObject = GameObject.Create<DummyGameObject2>();
        }
    }
}