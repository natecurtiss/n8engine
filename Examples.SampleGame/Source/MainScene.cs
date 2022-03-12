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
        Create("player_1")
            .AddComponent(new Transform().WithScale(150, 150).AtPosition(300, 0))
            .AddComponent(new Sprite("Assets/Textures/n8dev.png".Find()));
        Create("player_2")
            .AddComponent(new Transform().WithScale(100, 100))
            .AddComponent(new Sprite("Assets/Textures/n8dev.png".Find()));
        Create("camera")
            .AddComponent(new CameraController(1000f));
    }
}