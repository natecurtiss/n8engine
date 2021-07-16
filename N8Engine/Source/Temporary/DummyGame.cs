using N8Engine.Mathematics;

namespace N8Engine
{
    internal static class DummyGame
    {
        public static void Start()
        {
            GameObject __first = GameObject.Create<DummyGameObject>();
            GameObject __other = GameObject.Create<DummyGameObject2>();
            __first.Position += Vector.Right * 100;
            __other.Position += Vector.Left * 100;
        }
    }
}