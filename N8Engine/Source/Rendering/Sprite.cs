using System.Collections.Generic;
using System.IO;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class Sprite
    {
        internal Vector2 Pivot { get; private set; }
        internal IEnumerable<SpriteCell> Cells;

        public Sprite(in string path, in Vector2 pivot)
        {
            Pivot = pivot;
            // TODO pivots
            string[] __lines = File.ReadAllLines(path);
            List<SpriteCell> __cells = new();
            SpriteCell __lastSpriteCell = new();
            for (int __y = 0; __y < __lines.Length; __y++)
            {
                int __xValue = 0;
                for (int __x = 0; __x < __lines[__y].Length; __x++)
                {
                    string __color = __lines[__x][__y].ToString();
                    __cells.Add(new SpriteCell(__color, new Vector2(__x, __y)));
                    __xValue = __x;
                }
                __lastSpriteCell = new SpriteCell("/n", new Vector2(__xValue + 1, __y));
                __cells.Add(__lastSpriteCell);
            }
            __cells.Remove(__lastSpriteCell);
            Cells = __cells;
        }
    }
}