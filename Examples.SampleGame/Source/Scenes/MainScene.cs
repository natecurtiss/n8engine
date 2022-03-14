using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using N8Engine;
using N8Engine.InputSystem;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using N8Engine.Utilities;

namespace SampleGame;

sealed class MainScene : Scene
{
    public override string Name => "Main";

    [SuppressMessage("ReSharper.DPA", "DPA0003: Excessive memory allocations in LOH", MessageId = "type: SixLabors.ImageSharp.PixelFormats.Rgba32[]; size: 96MB")]
    protected override void Load()
    {
        var input = Game.Modules.Get<Input>();
        Modules.Get<Camera>().Zoom = 0.7f;

        Create("background_wall")
            .AddComponent(new Transform().WithScale(1920 * 2, 1080 * 2))
            .AddComponent(new Sprite("Assets/Textures/background_3.png".Find()));

        var scrolling = new Scrolling(1000f);
        var scrollSpeeds = new[] { 0f, 0.5f, 0.8f };
        var scrollPositions = new[] {-560f, -270, 0f};
        var duplicateScrollPositions = new[] {-3918f, 0, 3918f};
        var backgroundScales = new Vector2[] {new(3918, 395), new(3918, 518), new(3918, 743)};
        for (var s = 2; s >= 0; s--)
        {
            for (var d = 0; d < 3; d++)
            {
                Create($"background_{s}_{d}")
                    .AddComponent(new Parallax(scrollSpeeds[s], scrolling))
                    .AddComponent(new Sprite($"Assets/Textures/background_{s}.png".Find()))
                    .AddComponent(new Transform()
                        .AtPosition(duplicateScrollPositions[d], scrollPositions[s])
                        .WithScale(backgroundScales[s]));
            }
        }

        Create("player")
            .AddComponent(new PlayerStart(() => input.WasJustPressed(Key.Space)))
            .AddComponent(new Player(3f, () => input.WasJustPressed(Key.Space)))
            .AddComponent(new Body(-2500), out _)
            .AddComponent(new Sprite("Assets/Textures/player.png".Find()))
            .AddComponent(new Transform()
                .AtPosition(-100, 0)
                .WithScale(199, 178));
    }
}