using Silk.NET.Input;
using Silk.NET.Windowing;

namespace OpenGL.HelloWindow;

static class Program
{
    static IWindow _window = default!;

    static void Main()
    {
        var options = WindowOptions.Default;
        options.Size = new(800, 600);
        options.Title = "LearnOpenGL with Silk.NET";

        _window = Window.Create(options);

        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;

        _window.Run();
    }


    static void OnLoad()
    {
        var input = _window.CreateInput();
        for (var i = 0; i < input.Keyboards.Count; i++)
            input.Keyboards[i].KeyDown += OnKeyDown;
    }

    static void OnUpdate(double dt) { }

    static void OnRender(double dt) { }

    static void OnKeyDown(IKeyboard keyboard, Key key, int i)
    {
        if (key == Key.Escape)
            _window.Close();
    }
}