using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace N8Engine.Rendering
{
    internal readonly struct N8SpriteFile
    {
        public static implicit operator N8SpriteFile(in string path) => new(path);

        private readonly Color[] _colors;

        public N8SpriteFile(in string path) : this()
        {
            string __fileText = File.ReadAllText(path);
            _colors = ReadFile(__fileText);
        }

        public Color[] ReadFile(in string fileText)
        {
            List<string> __colorSets = fileText.Split('{').ToList();
            __colorSets.RemoveAll(s => s == string.Empty);
            List<Color> __colors = new();
            for (int __i = 0; __i < __colorSets.Count; __i++)
            {
                __colorSets[__i] = __colorSets[__i].Replace("}", string.Empty);
                ConsoleColor ToConsoleColor(in string colorName) => (ConsoleColor) Enum.Parse(typeof(ConsoleColor), colorName);
                ConsoleColor __foregroundColor = ToConsoleColor(__colorSets[__i].Split(',')[0]);
                ConsoleColor __backgroundColor = ToConsoleColor(__colorSets[__i].Split(',')[1]);
                __colors.Add(new Color(__foregroundColor, __backgroundColor));
            }
            return __colors.ToArray();
        }
    }
}