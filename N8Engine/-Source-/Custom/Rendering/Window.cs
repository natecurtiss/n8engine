namespace N8Engine.Rendering;

public sealed class Window : Module
{
    // TODO: double buffer + swap this.
    readonly char[] _screen;
    readonly int _chars = "\u001b[38;2;000;000;000m".Length;

    public Window(uint width, uint height, string title)
    {
        _screen = new char[width * height * _chars];
    }

    public void Write(uint x, uint y, in char[] color)
    {
        if (color.Length != _chars)
            throw new ArgumentException($"Color {color} is not in a valid format! (Expected length {_chars}, found length {color.Length}.)");
        for (var c = 0; c < color.Length; c++)
            _screen[(x + 1) * (y + 1) * (c + 1) - 3] = color[c];
    }
        
    void Module.Initialize()
    {
        
    }
    
    void Module.Update(Frame frame)
    {
        
    }
}