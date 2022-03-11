using System;

namespace N8Engine.Utilities;

public static class MathExtensions
{
    public static float ToRadians(this float degrees) => MathF.PI / 180f * degrees;
    public static float ToDegrees(this float radians) => 180f / MathF.PI * radians;
}