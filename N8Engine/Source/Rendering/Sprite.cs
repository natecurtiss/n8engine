using System.Collections.Generic;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class Sprite
    {
        private readonly N8SpriteFile _file;
        
        public int SortingOrder { get; set; }

        public Sprite(in string path, in int sortingOrder)
        {
            _file = path;
            SortingOrder = sortingOrder;
        }
    }
}