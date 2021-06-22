using N8Engine.Core;
using N8Engine.Objects;
using N8Engine.Rendering;

Game.Start();

namespace N8Engine.Core
{
    internal static class Game
    {
        public static void Start()
        {
            Window.Initialize();
            GameObject __gameObject = new DummyInputGameObject();
            __gameObject.Initialize();
            GameLoop.Run();
        }
    }
}