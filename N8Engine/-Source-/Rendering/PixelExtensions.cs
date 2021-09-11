using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using N8Engine.Mathematics;
using N8Engine.Utilities;

namespace N8Engine.Rendering
{
    internal static class PixelExtensions
    {
        public static void Offset(this Pixel[] pixels, IntegerVector offset) => pixels.ForEach(pixel => pixel.Position += offset);

        public static void AdjustToPivot(this Pixel[] pixels, IntegerVector size, Pivot pivot) => pixels.ForEach(pixel => pixel.Position.AdjustToPivot(size, pivot));

        public static Pixel[] Flipped(this Pixel[] pixels, Orientation flipOrientation)
        {
            var flippedPixels = new Pixel[pixels.Length];
            var flipScale = flipOrientation switch
            {
                Orientation.Horizontal => new IntegerVector(-1, 1),
                Orientation.Vertical => new IntegerVector(1, -1),
                Orientation.HorizontalAndVertical => new IntegerVector(-1, -1),
                _ => IntegerVector.One
            };
            pixels.ForEach((pixel, index) =>
            {
                var flippedPixel = new Pixel(pixel.Color, pixel.Position * flipScale);
                flippedPixels[index] = flippedPixel;
            });
            return flippedPixels;
        }
    }
}