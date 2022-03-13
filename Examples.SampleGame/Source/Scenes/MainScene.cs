using N8Engine;
using N8Engine.InputSystem;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using N8Engine.Utilities;
using static SampleGame.Events;

namespace SampleGame;

sealed class MainScene : Scene
{
    Player _player;
    public override string Name => "Main";

    protected override void Load()
    {
        var input = Game.Modules.Get<Input>();
        var debug = Game.Modules.Get<Debug>();
        Modules.Get<Camera>().Zoom = 0.7f;

        Create("player")
            .AddComponent(new PlayerStart(() => input.WasJustPressed(Key.Space)))
            .AddComponent(new Player(3f, () => input.WasJustPressed(Key.Space)), out _player)
            .AddComponent(new Body(-2500), out _)
            .AddComponent(new Sprite("Assets/Textures/player.png".Find()))
            .AddComponent(new Transform()
                .AtPosition(-100f, 0f)
                .WithScale(199f, 178f))
            .AddComponent(new InputDebugger(key =>
            {
                if (key == Key.Space)
                    debug.Log("Space key pressed.");
            }));

        OnPlayerStart.Add(_player.Enable);
    }

    protected override void Unload() => OnPlayerStart.Remove(_player.Enable);
}