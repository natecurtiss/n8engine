using System;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    internal static class PixelExtensions
    {
        public static void ForEach(this Pixel[] pixels, Action<Pixel> callback)
        {
            for (var index = 0; index < pixels.Length; index++)
                callback(pixels[index]);
        }

        public static void Offset(this Pixel[] pixels, IntegerVector offset) => pixels.ForEach(pixel => pixel.Position += offset);

        public static void Center(this Pixel[] pixels, IntegerVector size) => pixels.ForEach(pixel => ((Vector) pixel.Position).AdjustToPivot(size, Pivot.Center));
    }
}