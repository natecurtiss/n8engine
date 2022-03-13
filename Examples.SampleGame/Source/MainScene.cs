using N8Engine;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using N8Engine.Utilities;

namespace SampleGame;

sealed class MainScene : Scene
{
    public override string Name => "Main";

    protected override void Load()
    {
        Create("player")
            .AddComponent(new Sprite("Assets/Textures/player.png".Find()))
            .AddComponent(new Transform()
                .AtPosition(-100f, 0f)
                .WithScale(199f, 178f))
            .AddComponent(new Player(100f));
    }
}