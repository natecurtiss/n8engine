using N8Engine;
using N8Engine.InputSystem;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using N8Engine.Utilities;
using static SampleGame.Events;

namespace SampleGame;

sealed class MainScene : Scene
{
    public override string Name => "Main";

    protected override void Load()
    {
        var input = Game.Modules.Get<Input>();
        Modules.Get<Camera>().Zoom = 0.7f;

        Create("player")
            .AddComponent(new PlayerStart(() => input.WasJustPressed(Key.Space)))
            .AddComponent(new Player(3f, () => input.WasJustPressed(Key.Space)))
            .AddComponent(new Body(-2500), out _)
            .AddComponent(new Sprite("Assets/Textures/player.png".Find()))
            .AddComponent(new Transform()
                .AtPosition(-100f, 0f)
                .WithScale(199f, 178f));
        
        var scroll = new[] { 0f, 0.4f, 0.8f, 1f };
        Create("background_0")
            .AddComponent(new Sprite("Assets/Textures/background_0.png".Find()))
            .AddComponent(new Transform()
                .AtPosition(100f, 0f)
                .WithScale(199f, 178f));
    }
}