using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    internal readonly struct N8SpriteFile
    {
        public static implicit operator N8SpriteFile(in string path) => new(path);

        private const int NUMBER_OF_PIXELS = 2;
        private readonly string _path;

        private N8SpriteFile(in string path) : this() => _path = path;
        
        public List<Pixel> GetPixels() => GetPixels(File.ReadAllLines(_path));

        public List<Pixel> GetPixels(in string[] fileText)
        {
            List<Pixel> __originalPixels = GetPixelsNotRelativeToCenterPixel(fileText);
            List<Pixel> __newPixels = new();
            for (int __i = 0; __i < __originalPixels.Count; __i++)
            {
                Pixel __pixel = __originalPixels[__i];
                __pixel.Position = GetLocalPositionRelativeToCenterPixel(__originalPixels, __pixel);
                __newPixels.Add(__pixel);
            }

            return __newPixels;
        }

        private List<Pixel> GetPixelsNotRelativeToCenterPixel(in string[] fileText)
        {
            List<Pixel> __pixels = new();
            int __flippedLine = 0;
            for (int __line = fileText.Length - 1; __line >= 0; __line--)
            {
                string __currentLine = fileText[__line];
                List<string> __pixelSetsInLine = SeparateCurrentLine(__currentLine);

                for (int __pixel = 0; __pixel < __pixelSetsInLine.Count; __pixel++)
                {
                    string __currentPixelSet = __pixelSetsInLine[__pixel];
                    Pixel? __currentFirstPixel = GetPixelFromPixelSet
                    (
                        __currentPixelSet,
                        new Vector(__pixel * NUMBER_OF_PIXELS, __flippedLine)
                    );
                    if (__currentFirstPixel.HasValue)
                        __pixels.Add(__currentFirstPixel.Value);

                    Pixel? __currentSecondPixel = GetPixelFromPixelSet
                    (
                        __currentPixelSet,
                        new Vector(__pixel * NUMBER_OF_PIXELS + 1, __flippedLine)
                    );
                    if (__currentSecondPixel.HasValue)
                        __pixels.Add(__currentSecondPixel.Value);
                }

                __flippedLine++;
            }

            return __pixels;
        }
        
        private List<string> SeparateCurrentLine(in string currentLine)
        {
            List<string> __currentLinePixels = currentLine.Split('{').ToList();
            __currentLinePixels.RemoveAll(s => s == string.Empty);
            for (int __x = 0; __x < __currentLinePixels.Count; __x++)
                __currentLinePixels[__x] = __currentLinePixels[__x].Replace("}", string.Empty);
            return __currentLinePixels;
        }

        private Pixel? GetPixelFromPixelSet(in string pixelSet, in Vector position)
        {
            ConsoleColor AsConsoleColor(in string color) => (ConsoleColor) Enum.Parse(typeof(ConsoleColor), color);
            string __foregroundColorName = pixelSet.Split(',')[0];
            string __backgroundColorName = pixelSet.Split(',')[1];
            if (__foregroundColorName == "Clear" && __backgroundColorName == "Clear") return null;
            
            ConsoleColor __foregroundColor =  AsConsoleColor(__foregroundColorName);
            ConsoleColor __backgroundColor =  AsConsoleColor(__backgroundColorName);
            return new Pixel(__foregroundColor, __backgroundColor, position);
        }

        private Vector GetCenterPixelPosition(in List<Pixel> pixels)
        {
            float __height = pixels.Last().Position.Y;
            float __width = pixels.Last().Position.X;
            int __centerY = (int) (__height / 2f);
            int __centerX = (int) (__width / 2f);
            Vector __center = new(__centerX, __centerY);
            return __center;
        }

        private Vector GetLocalPositionRelativeToCenterPixel(in List<Pixel> allPixels, in Pixel thisPixel) => 
            thisPixel.Position - GetCenterPixelPosition(allPixels);
    }
}