using System.Collections.Generic;
using System.IO;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class Sprite
    {
        public string Path { get; set; }
        public Vector2 Pivot { get; set; } = Vector2.One / 2f;
        internal IEnumerable<SpriteCell> SpriteData
        {
            get
            {
                string[] __lines = File.ReadAllLines(Path);
                List<SpriteCell> __cells = new();
                SpriteCell __lastSpriteCell = new();
                for (int __y = 0; __y < __lines.Length; __y++)
                {
                    int __xValue = 0;
                    for (int __x = 0; __x < __lines[__x].Length; __x++)
                    {
                        string __color = __lines[__x][__y].ToString();
                        __cells.Add(new SpriteCell(__color, new Vector2(__x, __y)));
                        __xValue = __x;
                    }
                    __lastSpriteCell = new SpriteCell("/n", new Vector2(__xValue + 1, __y));
                    __cells.Add(__lastSpriteCell);
                }
                __cells.Remove(__lastSpriteCell);
                return __cells;
            }
        }
        
        public Sprite(in string path)
        {
            Path = path;
        }
    }
}