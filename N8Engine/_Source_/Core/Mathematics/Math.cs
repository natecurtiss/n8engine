using System.Linq;
using SysMath = System.Math;

namespace N8Engine
{
    public static class Math
    {
        public static float Min(params float[] values) => values.Min();
        public static float Min(params int[] values) => values.Min();
        public static float Max(params float[] values) => values.Max();
        public static float Max(params int[] values) => values.Max();
        public static int Round(this float value) => (int) SysMath.Round(value, 0);
        public static int Floor(this float value) => (int) SysMath.Floor(value);
        public static int Ceil(this float value) => (int) SysMath.Ceiling(value);
        public static float Abs(this float value) =>  value >= 0 ? value : value * -1f;
        public static int Abs(this int value) => (int) ((float) value).Abs();
        public static float Sqrt(this float value) => (float) SysMath.Sqrt(value);
        public static float Clamp(this float value, float minimum, float maximum)
        {
            var clampedValue = value;
            if (value > maximum) 
                clampedValue = maximum;
            else if (value < minimum)
                clampedValue = minimum;
            return clampedValue;
        }
        public static int Clamp(this int value, int minimum, int maximum)
        {
            var clampedValue = value;
            if (value > maximum) 
                clampedValue = maximum;
            else if (value < minimum)
                clampedValue = minimum;
            return clampedValue;
        }
        public static bool IsWithin(this float value, float minimum, float maximum) => value.Clamp(minimum, maximum) == value;
        public static bool IsWithin(this int value, int minimum, int maximum) => value.Clamp(minimum, maximum) == value;
    }
}