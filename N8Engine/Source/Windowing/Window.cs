using System;
using N8Engine.InputSystem;
using Silk.NET.Input;
using static Silk.NET.Windowing.Window;
using GLWindow = Silk.NET.Windowing.IWindow;

namespace N8Engine.Windowing;

public sealed class Window
{
    public event Action OnLoad;
    public event Action<Frame> OnUpdate;
    public event Action OnRender;
    public event Action<InputSystem.Key> OnKeyDown;
    public event Action<InputSystem.Key> OnKeyUp;
    
    readonly GLWindow _window;    
    
    internal Window(WindowOptions options)
    {
        _window = Create(options);
        _window.Load += () =>
        {
            SetUpInput();
            OnLoad?.Invoke();
        };
        _window.Update += fps => OnUpdate?.Invoke(new((float) fps));
        _window.Render += _ => OnRender?.Invoke();
        
        _window.Run(() => { });
    }

    void SetUpInput()
    {
        var input = _window.CreateInput();
        foreach (var keyboard in input.Keyboards)
        {
            keyboard.KeyDown += (_, glKey, _) => OnKeyDown?.Invoke(glKey.AsKey());
            keyboard.KeyUp += (_, glKey, _) => OnKeyUp?.Invoke(glKey.AsKey());
        }
    }
}