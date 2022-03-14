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

    public readonly bool IsResizable;
    readonly string _title;
    readonly uint _width;
    readonly uint _height;
    readonly int _fps;
    readonly WindowState _state;

    public WindowOptions(string title, uint width, uint height, int fps, WindowState state, bool isResizable)
    {
        _title = title;
        _width = width;
        _height = height;
        _fps = fps;
        _state = state;
        IsResizable = isResizable;
    }

    // TODO: When maximized and fullscreen set width and height to size of monitor.
    public WindowOptions WithTitle(string title) => new(title, _width, _height, _fps, _state, IsResizable);
    public WindowOptions WithSize(uint width, uint height) => new(_title, width, height, _fps, _state, IsResizable);
    public WindowOptions WithFps(int fps) => new(_title, _width, _height, fps, _state, IsResizable);
    public WindowOptions Fullscreen() => new(_title, _width, _height, _fps, WindowState.Fullscreen, IsResizable);
    public WindowOptions Maximized() => new(_title, _width, _height, _fps, WindowState.Maximized, IsResizable);
    public WindowOptions Windowed() => new(_title, _width, _height, _fps, WindowState.Windowed, IsResizable);
    public WindowOptions Resizable() => new(_title, _width, _height, _fps, _state, true);
    public WindowOptions NotResizable() => new(_title, _width, _height, _fps, _state, false);
}