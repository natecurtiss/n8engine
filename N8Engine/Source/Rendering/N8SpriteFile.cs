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

        private readonly string _path;

        public List<Pixel> Pixels
        {
            get
            {
                string[] __fileText = File.ReadAllLines(_path);

                List<Pixel> __pixels = new();
                for (int __line = 0; __line < __fileText.Length; __line++)
                {
                    string __currentLine = __fileText[__line];
                    List<string> __pixelSetsInLine = SeparateCurrentLine(__currentLine);

                    for (int __pixel = 0; __pixel < __pixelSetsInLine.Count; __pixel++)
                    {
                        string __currentPixelSet = __pixelSetsInLine[__pixel];
                        Pixel __currentPixel = GetPixelFromPixelSet(__currentPixelSet, new Vector2(__pixel, __line));
                        __pixels.Add(__currentPixel);
                    }
                }
                return __pixels;
            }
        }

        public Pixel CenterPixel => Pixels[((float) Pixels.Count / 2).Rounded()];

        public N8SpriteFile(in string path) : this() => _path = path;

        public List<string> SeparateCurrentLine(in string currentLine)
        {
            List<string> __currentLinePixels = currentLine.Split('{').ToList();
            __currentLinePixels.RemoveAll(s => s == string.Empty);
            for (int __x = 0; __x < __currentLinePixels.Count; __x++)
                __currentLinePixels[__x] = __currentLinePixels[__x].Replace("}", string.Empty);
            return __currentLinePixels;
        }

        public Pixel GetPixelFromPixelSet(in string pixelSet, in Vector2 position)
        {
            ConsoleColor AsConsoleColor(in string color) => (ConsoleColor) Enum.Parse(typeof(ConsoleColor), color);
            ConsoleColor __foregroundColor = AsConsoleColor(pixelSet.Split(',')[0]);
            ConsoleColor __backgroundColor = AsConsoleColor(pixelSet.Split(',')[1]);
            return new Pixel(__foregroundColor, __backgroundColor, position);
        }
    }
}