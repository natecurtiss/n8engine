using N8Engine.Mathematics;

namespace N8Engine
{
    internal static class DummyGame
    {
        public static void Start()
        {
            GameObject __first = GameObject.Create<DummyGameObject>();
            __first.Transform.Position += Vector.Right * 100;
            GameObject __second = GameObject.Create<DummyGameObject2>();
        }
    }
}