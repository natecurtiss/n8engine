using System;
using System.Drawing;
using System.Text;
using N8Engine.External.Console;
using N8Engine.Mathematics;
using N8Engine.External.User;

namespace N8Engine.Rendering
{
    sealed class ConsoleRenderer : IRenderer
    {
        const int NUMBER_OF_CHARACTERS_PER_PIXEL = 2;
        
        readonly string _pixel = new('▒', NUMBER_OF_CHARACTERS_PER_PIXEL);
        readonly string _empty = new(' ', NUMBER_OF_CHARACTERS_PER_PIXEL);
        readonly IntVector _screenSize;
        readonly Color[,] _pixels;
        readonly StringBuilder _output = new();

        Color _backgroundColor = Color.Black;

        public ConsoleRenderer(short fontSize, IntPtr windowHandle, IntVector windowSize)
        {
            // TODO: fix this.
            ConsoleQuickEditMode.Disable();
            ConsoleMode.EnableAnsiEscapeSequences();
            ConsoleFont.SetTo("Consolas", fontSize);
            UserWindow.Resize(windowHandle, windowSize);
            
            _screenSize = new IntVector(Console.WindowWidth, Console.WindowHeight);
            // TODO: make this cross-platform.
            Console.SetBufferSize(_screenSize.X, _screenSize.Y);
            Console.CursorVisible = false;

            _pixels = new Color[_screenSize.X, _screenSize.Y];
            WriteInitialPixels();
        }

        // TODO: call this from a sprite renderer component.
        void IRenderer.Render(IRenderable renderable, IntVector objectPosition)
        {
            var offset = WorldToScreen(objectPosition);
            for (var i = 0; i < renderable.Pixels.Length; i++)
            {
                var pixel = renderable.Pixels[i];
                    
                var screenPos = pixel.LocalPosition + offset;
                if (IsOnScreen(screenPos))
                {
                    if (IsTransparent(pixel.Color))
                        pixel = pixel.With(_backgroundColor);
                    _pixels[screenPos.X, screenPos.Y] = pixel.Color;
                }
                i++;
            }
        }

        void IRenderer.ChangeBackground(Color color)
        {
            for (var y = 0; y < _pixels.GetLength(1); y++)
            {
                for (var x = 0; x < _pixels.GetLength(0); x++)
                    if (IsBackground(x, y)) AddPixel(x, y, color);
            }
            _backgroundColor = color;
        }
        
        void IRenderer.DisplayPixels()
        {
            _output.Clear();
            _output.MoveCursorTo(IntVector.Zero);
            for (var y = 0; y < _pixels.GetLength(1); y++)
            {
                for (var x = 0; x < _pixels.GetLength(0); x++)
                {
                    _output.MoveCursorTo(new IntVector(x, y));
                    if (IsBackground(x, y))
                    {
                        _output.Append(_empty);
                    }
                    else
                    {
                        _output.ChangeColorTo(_pixels[x, y]);
                        _output.Append(_pixel);
                    }
                }
            }
            Console.Write(_output.ToString());
        }

        void WriteInitialPixels()
        {
            for (var y = 0; y < _pixels.GetLength(1); y++)
            {
                for (var x = 0; x < _pixels.GetLength(0); x++)
                    AddPixel(x, y, _backgroundColor);
            }
        }

        void AddPixel(int x, int y, Color color) => _pixels[x, y] = color;
        
        bool IsBackground(int x, int y) => _pixels[x, y] == _backgroundColor;

        IntVector WorldToScreen(IntVector worldPos)
        {
            var screenPos = new IntVector(worldPos.X, -worldPos.Y);
            screenPos.X *= NUMBER_OF_CHARACTERS_PER_PIXEL;
            screenPos.X += _screenSize.X / 2;
            screenPos.Y += _screenSize.Y / 2;
            return screenPos;
        }

        // TODO: might need to subtract one or two from the upper bounds of each.
        bool IsOnScreen(IntVector screenPos) => screenPos.X.IsWithin(0, _screenSize.X) && screenPos.Y.IsWithin(0, _screenSize.Y);

        bool IsTransparent(Color color) => color.A != 1f;
    }
}