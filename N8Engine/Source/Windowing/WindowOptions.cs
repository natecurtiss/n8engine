using N8Engine.Mathematics;
using Silk.NET.Windowing;
using GLWindowOptions = Silk.NET.Windowing.WindowOptions;

namespace N8Engine.Windowing;

readonly struct WindowOptions
{
    public static implicit operator GLWindowOptions(WindowOptions options)
    {
        var result = GLWindowOptions.Default;
        result.Title = options._title;
        result.Size = new((int) options._size.X, (int) options._size.Y);
        result.FramesPerSecond = options._fps;
        if (options._isFullscreen)
            result.WindowState = WindowState.Fullscreen;
        return result;
    }

    readonly string _title;
    readonly UIntVector _size;
    readonly int _fps;
    readonly bool _isFullscreen;

    public WindowOptions(string title, UIntVector size, int fps, bool isFullscreen)
    {
        _title = title;
        _size = size;
        _fps = fps;
        _isFullscreen = isFullscreen;
    }

    public WindowOptions WithTitle(string title) => new(title, _size, _fps, _isFullscreen);
    public WindowOptions WithSize(UIntVector size) => new(_title, size, _fps, _isFullscreen);
    public WindowOptions WithFps(int fps) => new(_title, _size, fps, _isFullscreen);
    public WindowOptions Fullscreen() => new(_title, _size, _fps, true);
    public WindowOptions Windowed() => new(_title, _size, _fps, false);
}