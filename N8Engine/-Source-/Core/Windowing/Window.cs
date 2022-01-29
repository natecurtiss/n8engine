using C = System.Console;

namespace N8Engine;

abstract class Window
{
    // TODO: double buffer + swap this.
    readonly char[] _buffer;
    readonly string _chars = "\u001b[38;2;000;000;000m";
    readonly string _reset = "\u001b[0;0H";

    protected Window(uint width, uint height, string title)
    {
        _buffer = new char[width * height * _chars.Length + _reset.Length];
        for (var c = 0; c < _reset.Length; c++)
            _buffer[c] = _reset[c];
        C.Title = title;
    }

    public void Set(uint x, uint y, in char[] color)
    {
        if (color.Length != _chars.Length)
            throw new ArgumentException($"Color {color} is not in a valid format! (Expected length {_chars}, found length {color.Length}.)");
        for (var c = 0; c < color.Length; c++)
            _buffer[(x + 1) * (y + 1) * (c + 1) - 3 + _reset.Length] = color[c];
    }

    public void Write() => C.Write(_buffer);
}