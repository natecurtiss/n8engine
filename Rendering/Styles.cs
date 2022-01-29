namespace N8Engine.Rendering;

public enum Styles
{
    None = 0,
    Titlebar = 1,
    Resize = 2,
    Close = 4,
    Fullscreen = 8,
    Default = Close | Resize | Titlebar
}