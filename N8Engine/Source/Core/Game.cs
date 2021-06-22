using N8Engine.Core;
using N8Engine.Inputs;
using N8Engine.Rendering;

Game.Start();

namespace N8Engine.Core
{
    internal static class Game
    {
        public static void Start()
        {
            Window.Initialize();
            Input.Initialize();
            GameLoop.Run();
        }
    }
}