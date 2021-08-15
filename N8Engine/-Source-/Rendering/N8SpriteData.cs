using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    internal readonly struct N8SpriteData
    {
        public readonly Vector Dimensions;
        private readonly string[] _dataText;

        public List<Pixel> Pixels
        {
            get
            {
                var pixels = new List<Pixel>();
                var lineFlippedUpsideDown = 0;
                for (var line = (int) Dimensions.Y; line >= 0; line--)
                {
                    var currentLine = _dataText[line];
                    var pixelsInCurrentLine = SeparateLine(currentLine);
                    for (var pixel = 0; pixel < (int) Dimensions.X; pixel++)
                    {
                        var currentPixel = pixelsInCurrentLine[pixel];
                        for (var fractionOfAPixel = 0; fractionOfAPixel < Renderer.NUMBER_OF_PIXELS; fractionOfAPixel++)
                        {
                            var fractionOfCurrentPixel = SeparatePixel
                            (
                                currentPixel,
                                new Vector(pixel * Renderer.NUMBER_OF_PIXELS + fractionOfAPixel, lineFlippedUpsideDown)
                            );
                            if (fractionOfCurrentPixel.HasValue) pixels.Add(fractionOfCurrentPixel.Value);
                        }
                    }
                    lineFlippedUpsideDown++;
                }
                for (var i = 0; i < pixels.Count; i++)
                {
                    var pixel = pixels[i];
                    pixels[i] = new Pixel(pixel.ForegroundColor, pixel.BackgroundColor, LocalPositionRelativeToCenterPixel(pixels, pixel));
                }
                return pixels;
            }
        }

        public N8SpriteData(string[] dataText) : this()
        {
            _dataText = dataText;
            Dimensions = new Vector(SeparateLine(_dataText[0]).Count, _dataText.Length - 1);
        }

        private List<string> SeparateLine(string line)
        {
            var pixels = line.Split('{').ToList();
            pixels.RemoveAll(pixel => pixel == string.Empty);
            for (var i = 0; i < pixels.Count; i++)
                pixels[i] = pixels[i].Replace("}", string.Empty);
            return pixels;
        }

        private Pixel? SeparatePixel(string pixelSet, Vector position)
        {
            var foregroundColorName = pixelSet.Split(',')[0];
            var backgroundColorName = pixelSet.Split(',')[1];
            if (foregroundColorName == "Clear" && backgroundColorName == "Clear") return null;
            
            static ConsoleColor AsConsoleColor(string color) => (ConsoleColor) Enum.Parse(typeof(ConsoleColor), color);
            var foregroundColor = AsConsoleColor(foregroundColorName);
            var backgroundColor = AsConsoleColor(backgroundColorName);
            return new Pixel(foregroundColor, backgroundColor, position);
        }
        
        private Vector LocalPositionRelativeToCenterPixel(IReadOnlyCollection<Pixel> allPixels, Pixel pixel) => pixel.Position - CenterPixelPositionOf(allPixels);

        private Vector CenterPixelPositionOf(IReadOnlyCollection<Pixel> pixels)
        {
            var height = pixels.Last().Position.Y;
            var width = pixels.Last().Position.X;
            var centerY = (int) (height / 2f);
            var centerX = (int) (width / 2f);
            var center = new Vector(centerX, centerY);
            return center;
        }
    }
}