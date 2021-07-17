using N8Engine.Mathematics;
using N8Engine.Rendering;

namespace N8Engine.Physics
{
    public sealed class Collider
    {
        public bool DebugModeEnabled { get; set; }
        public Vector Size
        {
            get => Rectangle.Size;
            set
            {
                if (Rectangle.Size == value) return;
                Rectangle __rectangle = Rectangle;
                __rectangle.Size = value;
                Rectangle = __rectangle;
                _debugVisualization = new Sprite(new N8SpriteFile().GetPixels(DebugVisualizationPixelData).ToArray());
            }
        }
        public Vector Offset { get; set; }

        internal Rectangle Rectangle { get; set; }
        
        internal Sprite DebugVisualization => _debugVisualization ??= new Sprite(new N8SpriteFile().GetPixels(DebugVisualizationPixelData).ToArray());
        private Sprite _debugVisualization;

        internal string[] DebugVisualizationPixelData
        {
            get
            {
                const string __color = "{Green,Green}";
                string[] __pixels = new string[(int)Size.Y]; // TODO add thickness
                for (int __topRowPixel = 0; __topRowPixel < Size.X; __topRowPixel++) 
                    __pixels[0] += __color;
                for (int __bottomRowPixel = 0; __bottomRowPixel < Size.X; __bottomRowPixel++) 
                    __pixels[(int)Size.Y - 1] += __color;
                for (int __line = 1; __line < Size.Y - 1; __line++)
                {
                    for (int __pixel = 0; __pixel < Size.X; __pixel++)
                        if (__pixel == 0 || __pixel == (int)Size.X - 1)
                            __pixels[__line] += __color;
                        else
                            __pixels[__line] += "{Clear,Clear}";
                }

                return __pixels;
            }
        }
    }
}