using System;
using N8Engine.InputSystem;
using N8Engine.SceneManagement;

namespace SampleGame;

sealed class MainScene : Scene
{
    public override void Load()
    {
        Create("player")
            .AddComponent(new Player())
            .AddComponent(new InputDebugger(key => Console.WriteLine(key)));
    }
}