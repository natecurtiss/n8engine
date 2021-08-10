﻿using System.IO;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class Sprite
    {
        internal Pixel[] Pixels { get; private set; }

        public Sprite(string path, SpriteRenderer spriteRenderer, Vector offset = default, bool shouldFlipHorizontally = false, bool shouldFlipVertically = false)
        {
            var file = File.ReadAllLines(path);
            var data = new N8SpriteData(file);
            Pixels = data.Pixels.ToArray();
            CreateSprite(spriteRenderer.SortingOrder, offset, shouldFlipHorizontally, shouldFlipVertically);
        }

        internal Sprite(Pixel[] pixels)
        {
            Pixels = pixels;
            CreateSprite((int) Math.Infinity);
        }

        private void CreateSprite(int sortingOrder, Vector offset = default, bool shouldFlipHorizontally = false, bool shouldFlipVertically = false)
        {
            OffsetPixels(offset);
            if (shouldFlipHorizontally) FlipPixelsHorizontally();
            if (shouldFlipVertically) FlipPixelsVertically();
            
            for (var i = 0; i < Pixels.Length; i++) 
                Pixels[i].SortingOrder = sortingOrder;
        }

        private void OffsetPixels(Vector offset)
        {
            var pixels = new Pixel[Pixels.Length];
            for (var i = 0; i < Pixels.Length; i++)
            {
                var pixel = Pixels[i];
                pixels[i] = new Pixel(pixel.ForegroundColor, pixel.BackgroundColor, pixel.Position + new Vector(offset.X, -offset.Y), pixel.IsBackground);
            }
            Pixels = pixels;
        }

        private void FlipPixelsHorizontally()
        {
            var pixels = new Pixel[Pixels.Length];
            for (var i = 0; i < Pixels.Length; i++)
            {
                var pixel = Pixels[i];
                pixels[i] = new Pixel(pixel.ForegroundColor, pixel.BackgroundColor, pixel.Position * new Vector(-1f, 1f), pixel.IsBackground);
            }
            Pixels = pixels;
        }

        private void FlipPixelsVertically()
        {
            var pixels = new Pixel[Pixels.Length];
            for (var i = 0; i < Pixels.Length; i++)
            {
                var pixel = Pixels[i];
                pixels[i] = new Pixel(pixel.ForegroundColor, pixel.BackgroundColor, pixel.Position * new Vector(1f, -1f), pixel.IsBackground);
            }
            Pixels = pixels;
        }
    }
}