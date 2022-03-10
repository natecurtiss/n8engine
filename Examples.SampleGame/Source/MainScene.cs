using System.Numerics;
using N8Engine;
using N8Engine.SceneManagement;

namespace SampleGame;

sealed class MainScene : Scene
{
    public override string Name => "Main Scene";

    protected override void Load()
    {
        Create("player")
            .AddComponent(new Transform(Vector2.Zero, Vector2.One * 1000))
            .AddComponent(new Player());
    }
}