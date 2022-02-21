using N8Engine.Mathematics;
using GLWindowOptions = Silk.NET.Windowing.WindowOptions;
using GLWindowState = Silk.NET.Windowing.WindowState;

namespace N8Engine.Windowing;

readonly struct WindowOptions
{
    public static implicit operator GLWindowOptions(WindowOptions options)
    {
        var result = GLWindowOptions.Default;
        result.Title = options._title;
        result.Size = new((int) options._size.X, (int) options._size.Y);
        result.FramesPerSecond = options._fps;
        result.WindowState = options._state switch
        {
            WindowState.Fullscreen => GLWindowState.Fullscreen,
            WindowState.Maximized => GLWindowState.Maximized,
            WindowState.Windowed => GLWindowState.Normal,
            _ => result.WindowState
        };
        return result;
    }

    readonly string _title;
    readonly UIntVector _size;
    readonly int _fps;
    readonly WindowState _state;

    public WindowOptions(string title, UIntVector size, int fps, WindowState state)
    {
        _title = title;
        _size = size;
        _fps = fps;
        _state = state;
    }

    public WindowOptions WithTitle(string title) => new(title, _size, _fps, _state);
    public WindowOptions WithSize(UIntVector size) => new(_title, size, _fps, _state);
    public WindowOptions WithFps(int fps) => new(_title, _size, fps, _state);
    public WindowOptions Fullscreen() => new(_title, _size, _fps, WindowState.Fullscreen);
    public WindowOptions Maximized() => new(_title, _size, _fps, WindowState.Maximized);
    public WindowOptions Windowed() => new(_title, _size, _fps, WindowState.Windowed);
}