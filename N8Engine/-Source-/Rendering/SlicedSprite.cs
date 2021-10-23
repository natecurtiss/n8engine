using System;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public abstract class SlicedSprite
    {
        protected abstract string PathToFile { get; }
        protected abstract IntegerVector CellSize { get; }

        
    }
}