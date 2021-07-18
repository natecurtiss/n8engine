using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    internal readonly struct N8SpriteFile
    {
        public static implicit operator N8SpriteFile(string path) => new(path);

        private const int NUMBER_OF_PIXELS = 2;
        private readonly string _path;

        private N8SpriteFile(string path) : this() => _path = path;
        
        public List<Pixel> GetPixels() => GetPixels(File.ReadAllLines(_path));

        public List<Pixel> GetPixels(string[] fileText)
        {
            var originalPixels = GetPixelsNotRelativeToCenterPixel(fileText);
            var newPixels = new List<Pixel>();
            for (var i = 0; i < originalPixels.Count; i++)
            {
                var pixel = originalPixels[i];
                pixel.Position = GetLocalPositionRelativeToCenterPixel(originalPixels, pixel);
                newPixels.Add(pixel);
            }

            return newPixels;
        }

        private List<Pixel> GetPixelsNotRelativeToCenterPixel(string[] fileText)
        {
            var pixels = new List<Pixel>();
            var lineIndexFlippedUpsideDown = 0;
            for (var lineIndex = fileText.Length - 1; lineIndex >= 0; lineIndex--)
            {
                var currentLine = fileText[lineIndex];
                var pixelSetsInLine = SeparateCurrentLine(currentLine);

                for (var pixel = 0; pixel < pixelSetsInLine.Count; pixel++)
                {
                    var currentPixelSet = pixelSetsInLine[pixel];
                    var currentFirstPixel = GetPixelFromPixelSet
                    (
                        currentPixelSet,
                        new Vector(pixel * NUMBER_OF_PIXELS, lineIndexFlippedUpsideDown)
                    );
                    if (currentFirstPixel.HasValue)
                        pixels.Add(currentFirstPixel.Value);

                    var currentSecondPixel = GetPixelFromPixelSet
                    (
                        currentPixelSet,
                        new Vector(pixel * NUMBER_OF_PIXELS + 1, lineIndexFlippedUpsideDown)
                    );
                    if (currentSecondPixel.HasValue)
                        pixels.Add(currentSecondPixel.Value);
                }

                lineIndexFlippedUpsideDown++;
            }

            return pixels;
        }
        
        private List<string> SeparateCurrentLine(string currentLine)
        {
            var currentLinePixels = currentLine.Split('{').ToList();
            currentLinePixels.RemoveAll(s => s == string.Empty);
            for (var x = 0; x < currentLinePixels.Count; x++)
                currentLinePixels[x] = currentLinePixels[x].Replace("}", string.Empty);
            return currentLinePixels;
        }

        private Pixel? GetPixelFromPixelSet(string pixelSet, Vector position)
        {
            ConsoleColor AsConsoleColor(string color) => (ConsoleColor) Enum.Parse(typeof(ConsoleColor), color);
            var foregroundColorName = pixelSet.Split(',')[0];
            var backgroundColorName = pixelSet.Split(',')[1];
            if (foregroundColorName == "Clear" && backgroundColorName == "Clear") return null;
            
            var foregroundColor =  AsConsoleColor(foregroundColorName);
            var backgroundColor =  AsConsoleColor(backgroundColorName);
            return new Pixel(foregroundColor, backgroundColor, position);
        }

        private Vector GetCenterPixelPosition(List<Pixel> pixels)
        {
            var height = pixels.Last().Position.Y;
            var width = pixels.Last().Position.X;
            var centerY = (int) (height / 2f);
            var centerX = (int) (width / 2f);
            var center = new Vector(centerX, centerY);
            return center;
        }

        private Vector GetLocalPositionRelativeToCenterPixel(List<Pixel> allPixels, Pixel thisPixel) => 
            thisPixel.Position - GetCenterPixelPosition(allPixels);
    }
}