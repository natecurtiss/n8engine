using Silk.NET.Input;
using Silk.NET.Windowing;

namespace OpenGLTutorials;

public abstract class Base
{
    protected IWindow _window = default!;

    public virtual void Start()
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
    
    void OnLoad()
    {
        var input = _window.CreateInput();
        foreach (var keyboard in input.Keyboards)
            keyboard.KeyDown += OnKeyDown;
    }

    protected virtual void OnUpdate(double dt) { }

    protected virtual void OnRender(double dt) { }

    protected virtual void OnKeyDown(IKeyboard keyboard, Key key, int i)
    {
        if (key == Key.Escape)
            _window.Close();
    }
}