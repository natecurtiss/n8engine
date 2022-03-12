using System.Numerics;
using N8Engine;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using N8Engine.Utilities;

namespace SampleGame;

sealed class MainScene : Scene
{
    public override string Name => "Main Scene";

    protected override void Load()
    {
        Create("other")
            .AddComponent(new Transform(Vector2.Zero, Vector2.One * 0.2f))
            .AddComponent(new Sprite("Assets/Textures/n8dev.png".Find()))
            .AddComponent(new Movement(2f));
        Create("player")
            .AddComponent(new Transform(Vector2.One * 0.5f, Vector2.One * 0.1f))
            .AddComponent(new Sprite("Assets/Textures/n8dev.png".Find()));
    }
}