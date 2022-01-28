using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace N8Engine.Rendering;

public sealed class OpenTKWindow : GameWindow, Window
{
    public OpenTKWindow(int width, int height, string title) : base(GameWindowSettings.Default, NativeWindowSettings.Default) { }

    protected override void OnLoad()
    {
        base.OnLoad();
        CenterWindow();
        Size = new(1920, 1080);
    }

    protected override void OnResize(ResizeEventArgs args)
    {
        base.OnResize(args);
        GL.Viewport(0, 0, Size.X, Size.Y);
    }

    void Module.Initialize() => Run();
    void Module.Update(Frame frame) { }
}