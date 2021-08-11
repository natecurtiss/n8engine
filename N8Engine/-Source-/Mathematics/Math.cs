using System;

namespace N8Engine.Mathematics
{
    public static class Math
    {
        public static readonly float Infinity = float.MaxValue;
        public static readonly float NegativeInfinity = float.MinValue;
        
        public static float ClampedBetween(this float value, float minimum, float maximum)
        {
            var clampedValue = value;
            if (value > maximum) 
                clampedValue = maximum;
            else if (value < minimum)
                clampedValue = minimum;
            return clampedValue;
        }
        
        public static int ClampedBetween(this int value, int minimum, int maximum)
        {
            var clampedValue = value;
            if (value > maximum) 
                clampedValue = maximum;
            else if (value < minimum)
                clampedValue = minimum;
            return clampedValue;
        }
        
        public static float ClampedBetweenZeroAndOne(this float value) => value.ClampedBetween(0f, 1f);

        public static int ClampedBetweenZeroAndOne(this int value) => (int) ((float) value).ClampedBetweenZeroAndOne();
        
        public static float AbsoluteValue(this float value) =>  value >= 0 ? value : value * -1f;

        public static int AbsoluteValue(this int value) => (int) ((float) value).AbsoluteValue();

        public static float MinimumOf(params float[] values)
        {
            var minimumValue = Infinity;
            foreach (var value in values)
                if (value < minimumValue)
                    minimumValue = value;
            return minimumValue;
        }

        public static int MinimumOf(params int[] values) => (int) MinimumOf(Array.ConvertAll(values, integer => (float) integer));

        public static float MaximumOf(params float[] values)
        {
            var maximumValue = NegativeInfinity;
            foreach (var value in values)
                if (value > maximumValue)
                    maximumValue = value;
            return maximumValue;
        }
        
        public static int MaximumOf(params int[] values) => (int) MaximumOf(Array.ConvertAll(values, integer => (float) integer));
        
        public static int Rounded(this float value) => (int) System.Math.Round(value, 0);

        public static int RoundedDown(this float value) => (int) System.Math.Floor(value);

        public static int RoundedUp(this float value) => (int) System.Math.Ceiling(value);
        
        public static float SquareRoot(this float value) => (float) System.Math.Sqrt(value);
        
        public static bool IsWithinRange(this float value, float minimum, float maximum) => value.ClampedBetween(minimum, maximum) == value;
    }
}