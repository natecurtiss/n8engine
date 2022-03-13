using GLWindowOptions = Silk.NET.Windowing.WindowOptions;
using GLWindowState = Silk.NET.Windowing.WindowState;

namespace N8Engine.Windowing;

readonly struct WindowOptions
{
    public static implicit operator GLWindowOptions(WindowOptions options)
    {
        var result = GLWindowOptions.Default;
        result.Title = options._title;
        result.Size = new((int) options._width, (int) options._height);
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
    readonly uint _width;
    readonly uint _height;
    readonly int _fps;
    readonly WindowState _state;

    public WindowOptions(string title, uint width, uint height, int fps, WindowState state)
    {
        _title = title;
        _width = width;
        _height = height;
        _fps = fps;
        _state = state;
    }

    public WindowOptions WithTitle(string title) => new(title, _width, _height, _fps, _state);
    public WindowOptions WithSize(uint width, uint height) => new(_title, width, height, _fps, _state);
    public WindowOptions WithFps(int fps) => new(_title, _width, _height, fps, _state);
    public WindowOptions Fullscreen() => new(_title, _width, _height, _fps, WindowState.Fullscreen);
    public WindowOptions Maximized() => new(_title, _width, _height, _fps, WindowState.Maximized);
    public WindowOptions Windowed() => new(_title, _width, _height, _fps, WindowState.Windowed);
}