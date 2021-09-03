using System;
using System.Collections.Generic;
using System.Linq;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    internal readonly struct N8SpriteData
    {
        private readonly IntegerVector _dimensions;
        private readonly string[] _dataText;

        public List<Pixel> Pixels
        {
            get
            {
                var pixels = new List<Pixel>();
                for (var line = _dimensions.Y; line >= 0; line--)
                {
                    var currentLine = _dataText[line];
                    var pixelsInCurrentLine = SeparateLine(currentLine);
                    for (var pixel = 0; pixel <  _dimensions.X; pixel++)
                    {
                        var currentPixel = pixelsInCurrentLine[pixel];
                        var newPixel = SeparatePixel(currentPixel, new IntegerVector(pixel, line));
                        if (newPixel.HasValue) pixels.Add(newPixel.Value);
                    }
                }
                for (var pixel = 0; pixel < pixels.Count; pixel++)
                {
                    var newPixel = pixels[pixel];
                    newPixel.Position = LocalPositionRelativeToCenterPixel(pixels, newPixel);
                    pixels[pixel] = newPixel;
                }
                return pixels;
            }
        }

        public N8SpriteData(string[] dataText) : this()
        {
            _dataText = dataText;
            _dimensions = new IntegerVector(SeparateLine(_dataText[0]).Count, _dataText.Length - 1);
        }

        private List<string> SeparateLine(string line)
        {
            var pixels = line.Split('{').ToList();
            pixels.RemoveAll(pixel => pixel == string.Empty);
            for (var i = 0; i < pixels.Count; i++)
                pixels[i] = pixels[i].Replace("}", string.Empty);
            return pixels;
        }

        private Pixel? SeparatePixel(string pixelSet, IntegerVector position)
        {
            var foregroundColorName = pixelSet.Split(',')[0];
            var backgroundColorName = pixelSet.Split(',')[1];
            if (foregroundColorName == "Clear" && backgroundColorName == "Clear") return null;
            
            static ConsoleColor AsConsoleColor(string color) => Enum.Parse<ConsoleColor>(color);
            var foregroundColor = AsConsoleColor(foregroundColorName);
            var backgroundColor = AsConsoleColor(backgroundColorName);
            return new Pixel(foregroundColor, backgroundColor, position);
        }

        private IntegerVector LocalPositionRelativeToCenterPixel(IReadOnlyCollection<Pixel> allPixels, Pixel pixel) => pixel.Position - CenterPixelPositionOf(allPixels);

        private IntegerVector CenterPixelPositionOf(IReadOnlyCollection<Pixel> pixels)
        {
            var height = pixels.Last().Position.Y;
            var width = pixels.Last().Position.X;
            var centerY = (int) (height / 2f);
            var centerX = (int) (width / 2f);
            var center = new IntegerVector(centerX, centerY);
            return center;
        }
    }
}