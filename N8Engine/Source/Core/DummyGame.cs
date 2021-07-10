namespace N8Engine
{
    internal static class DummyGame
    {
        public static void Start()
        {
            GameObject.Create<DummyGameObject>();
        }
    }
}