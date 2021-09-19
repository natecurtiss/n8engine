using System;
using N8Engine.Mathematics;
using N8Engine.Utilities;
using static N8Engine.Mathematics.IntegerVector;

namespace N8Engine.Rendering
{
    internal static class PixelExtensions
    {
        public static void Offset(this Pixel[] pixels, IntegerVector offset) => pixels.ForEach(pixel => pixel.Position += offset);

        public static void AdjustToPivot(this Pixel[] pixels, IntegerVector size, Pivot pivot) => pixels.ForEach(pixel => pixel.Position = pixel.Position.AdjustedToPivot(size, pivot));

        public static Pixel[] Flipped(this Pixel[] pixels, Flip flip, IntegerVector size)
        {
            var flippedPixels = new Pixel[pixels.Length];
            var flipScale = flip switch
            {
                Flip.Horizontal => new IntegerVector(-1, 1),
                Flip.Vertical => new IntegerVector(1, -1),
                Flip.HorizontalAndVertical => new IntegerVector(-1, -1),
                var _ => One
            };
            var flipOffset = flip switch
            {
                Flip.Horizontal => new IntegerVector(size.X, 0),
                Flip.Vertical => new IntegerVector(0, size.Y),
                Flip.HorizontalAndVertical => size,
                var _ => Zero
            };
            pixels.ForEach((pixel, index) =>
            {
                var flippedPixel = new Pixel(pixel.Color, pixel.Position * flipScale + flipOffset);
                flippedPixels[index] = flippedPixel;
            });
            return flippedPixels;
        }
    }
}