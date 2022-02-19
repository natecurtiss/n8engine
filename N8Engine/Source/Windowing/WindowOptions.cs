using N8Engine.Mathematics;

namespace N8Engine.Windowing;

public readonly struct WindowOptions
{
    internal readonly UIntVector Size;
    internal readonly int FramesPerSecond;

    public WindowOptions(UIntVector size, int fps)
    {
        Size = size;
        FramesPerSecond = fps;
    }
}