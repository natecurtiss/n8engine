using System;
using System.Drawing;
using System.Text;
using N8Engine.External;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class Renderer : IModule
    {
        const int CHARACTERS_PER_PIXEL = 2;
        readonly string _pixel = new(' ', CHARACTERS_PER_PIXEL);
        readonly StringBuilder _output = new();
        readonly Color _background = Color.Black;
        readonly short _fontSize;
        
        RenderedPixel[,] _pixels;
        IntVector _consoleSize;
        
        Type IModule.Type => GetType();
        
        internal Renderer(short fontSize) => _fontSize = fontSize;

        void IModule.Initialize()
        {
            ExtConsole.EnableAnsiEscapeSequences();
            ExtConsole.SetFont("Consolas", _fontSize);
            ExtConsole.DisableQuickEditMode();
            ExtConsole.RemoveScrollbar();

            var window = Modules.Get<Window>();
            ExtWindow.Resize(window.Handle, window.Size);
            
            ExtConsole.HideCursor();

            _consoleSize = new(Console.WindowWidth, Console.WindowHeight);
            _pixels = new RenderedPixel[_consoleSize.X, _consoleSize.Y];
            ClearScreen();
        }
        void IModule.Update(Time time) => Display();

        public void Render(IRenderable renderable, Vector pos, int sortingOrder)
        {
            foreach (var pixel in renderable.Pixels)
            {
                var worldPos = pixel.LocalPosition + (IntVector) pos;
                var screenPos = WorldToScreen(worldPos);
                if (!IsOnScreen(screenPos)) 
                    continue;
                var newPixel = pixel.AsRendered(sortingOrder);
                var oldPixel = _pixels[screenPos.X, screenPos.Y];
                if (newPixel.CanRenderAbove(oldPixel))
                    _pixels[screenPos.X, screenPos.Y] = newPixel;
            }
        }

        void Display()
        {
            _output.Clear();
            for (var y = 0; y < _pixels.GetLength(1); y++)
                for (var x = 0; x < _pixels.GetLength(0); x += CHARACTERS_PER_PIXEL)
                {
                    _output.Append(ExtConsole.MoveCursor(new(x, y)));
                    var pixel = _pixels[x, y];
                    if (pixel.IsClear)
                    {
                        _output.Append(ExtConsole.SetColor(_background));
                    }
                    else
                    {
                        var color = pixel.Color;
                        _output.Append(ExtConsole.SetColor(color));
                    }
                    _output.Append(_pixel);
                }
            _output.Append(ExtConsole.MoveCursor(IntVector.Zero));
            Console.Write(_output.ToString());
            
            ClearScreen();
        }

        void ClearScreen()
        {
            for (var y = 0; y < _pixels.GetLength(1); y++)
                for (var x = 0; x < _pixels.GetLength(0); x++)
                    _pixels[x, y] = RenderedPixel.Empty();
        }

        IntVector WorldToScreen(IntVector worldPos)
        {
            var screenPos = new IntVector(worldPos.X, -worldPos.Y);
            screenPos.X *= CHARACTERS_PER_PIXEL;
            screenPos.X += _consoleSize.X / 2;
            screenPos.Y += _consoleSize.Y / 2;
            return screenPos;
        }

        bool IsOnScreen(IntVector screenPos) => 
            screenPos.X.IsWithin(0, _consoleSize.X) && 
            screenPos.Y.IsWithin(0, _consoleSize.Y);
    }
}