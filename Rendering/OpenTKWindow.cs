using OpenTK.Windowing.Desktop;

namespace N8Engine.Rendering;

public sealed class OpenTKWindow : GameWindow, Window
{
    public OpenTKWindow(int width, int height, string title) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
    {
        CenterWindow();
        Size = new(width, height);
        Title = title;
        Run();
    }
    
    void Module.Initialize() { }
    void Module.Update(Frame frame) { }
}