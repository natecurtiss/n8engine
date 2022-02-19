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
        result.WindowState = options._isFullscreen ? WindowState.Fullscreen : WindowState.Normal;
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
}