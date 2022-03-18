using System;
using N8Engine.InputSystem;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using N8Engine.Windowing;

namespace N8Engine;

readonly struct GameStart
{
    public static readonly GameStart Default = new((loop, ticks, windowOptions, firstScene) =>
    {
        var window = new Window(windowOptions);

        window.OnLoad += () =>
        {
            var gl = window.CreateGL();
            var input = new Input();
            var sceneManager = new SceneManager(loop, window, m =>
            {
                m.Add(new Camera(window));
                m.Add(new SpriteRenderer(gl));
            }, m =>
            {
                m.Remove<Camera>();
                m.Remove<SpriteRenderer>();
            });
            Modules.Add(input);
            Modules.Add(sceneManager);
            Modules.Add<Graphics>(gl);

            sceneManager.Load(firstScene);
            ticks.Start();
            window.OnUpdate += ticks.Update;
            window.OnRender += ticks.Render;
            window.OnKeyDown += key => input.UpdateKey(key, true);
            window.OnKeyUp += key => input.UpdateKey(key, false);
        };
        window.Run();
    });
    readonly Action<Loop, Ticks, WindowOptions, Scene> _onStart;

    public GameStart() => _onStart = (_, _, _, _) => { };
    public GameStart(Action<Loop, Ticks, WindowOptions, Scene> onStart) => _onStart = onStart;
    
    public void Start(Loop loop, Ticks ticks, WindowOptions windowOptions, Scene firstScene) => _onStart(loop, ticks, windowOptions, firstScene);
}