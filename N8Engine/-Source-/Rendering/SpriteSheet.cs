using System;
using System.Drawing;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class SpriteSheet
    {
        private readonly Sprite[,] _sprites;
        
        public SpriteSheet(string path, IntegerVector cellSize, IntegerVector offset = default)
        {
            var image = new Bitmap(path);
            var imageSize = new IntegerVector(image.Width, image.Height);
            var cells = imageSize / cellSize;
            _sprites = new Sprite[cells.X, cells.Y];
            
            for (var row = 0; row < cells.Y; row++)
            {
                for (var column = 0; column < cells.X; column++)
                {
                    var cutoutArea = new Rectangle(column * cellSize.X, row * cellSize.Y, cellSize.X, cellSize.Y);
                    Debug.Log(column * cellSize.X + " " + row * cellSize.Y);
                    var slicedImage = image.Clone(cutoutArea, image.PixelFormat);
                    _sprites[column, row] = new Sprite(slicedImage, offset);
                }
            }
        }

        public Sprite Take(int x, int y) => _sprites[x, y];
    }
}