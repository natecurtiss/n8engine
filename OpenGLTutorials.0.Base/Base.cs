using Silk.NET.Input;
using Silk.NET.Windowing;

namespace OpenGLTutorials;

public abstract class Base : WindowSize
{
    protected IWindow Window = null!;
    int WindowSize.Width => Window.Size.X;
    int WindowSize.Height => Window.Size.Y;

    public void Start()
    {
        var options = WindowOptions.Default;
        options.Size = new(800, 600);
        options.Title = "Like and Subscribe to Keep my Lights on.";

        Window = Silk.NET.Windowing.Window.Create(options);

        Window.Load += OnLoad;
        Window.Update += OnUpdate;
        Window.Render += OnRender;

        Window.Run();
    }
    
    protected virtual void OnLoad()
    {
        var input = Window.CreateInput();
        foreach (var keyboard in input.Keyboards)
            keyboard.KeyDown += OnKeyDown;
    }

    protected virtual void OnUpdate(double dt) { }

    protected virtual void OnRender(double dt) { }

    protected virtual void OnKeyDown(IKeyboard keyboard, Key key, int i)
    {
        if (key == Key.Escape)
            Window.Close();
    }
}