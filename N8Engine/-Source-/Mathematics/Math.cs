using System;

namespace N8Engine.Mathematics
{
    /// <summary>
    /// Pretty sure you didn't enjoy this in school (but maybe I'm wrong.)
    /// </summary>
    public static class Math
    {
        /// <summary>
        /// b i g
        /// </summary>
        public const int INFINITY = int.MaxValue;
        /// <summary>
        /// s m a l l
        /// </summary>
        public const int NEGATIVE_INFINITY = int.MinValue;

        /// <summary>
        /// Returns the value clamped inclusively between minimum and maximum values.
        /// </summary>
        public static float ClampedBetween(this float value, float minimum, float maximum)
        {
            var clampedValue = value;
            if (value > maximum) 
                clampedValue = maximum;
            else if (value < minimum)
                clampedValue = minimum;
            return clampedValue;
        }
        
        /// <summary>
        /// Returns the value clamped inclusively between minimum and maximum values.
        /// </summary>
        public static int ClampedBetween(this int value, int minimum, int maximum)
        {
            var clampedValue = value;
            if (value > maximum) 
                clampedValue = maximum;
            else if (value < minimum)
                clampedValue = minimum;
            return clampedValue;
        }
        
        /// <summary>
        /// Returns the value clamped inclusively between 0 and 1.
        /// </summary>
        public static float ClampedBetweenZeroAndOne(this float value) => value.ClampedBetween(0f, 1f);

        /// <summary>
        /// Returns the value clamped inclusively between 0 and 1.
        /// </summary>
        public static int ClampedBetweenZeroAndOne(this int value) => (int) ((float) value).ClampedBetweenZeroAndOne();
        
        /// <summary>
        /// Returns the absolute value of the float (shocking, I know.)
        /// </summary>
        public static float AbsoluteValue(this float value) =>  value >= 0 ? value : value * -1f;

        /// <summary>
        /// Returns the absolute value of the integer (shocking, I know.)
        /// </summary>
        public static int AbsoluteValue(this int value) => (int) ((float) value).AbsoluteValue();

        /// <summary>
        /// Returns the smallest of all the values passed in.
        /// </summary>
        public static float MinimumOf(params float[] values)
        {
            var minimumValue = (float) INFINITY;
            foreach (var value in values)
                if (value < minimumValue)
                    minimumValue = value;
            return minimumValue;
        }

        /// <summary>
        /// Returns the smallest of all the values passed in.
        /// </summary>
        public static int MinimumOf(params int[] values) => (int) MinimumOf(Array.ConvertAll(values, integer => (float) integer));

        /// <summary>
        /// Returns the largest of all the values passed in.
        /// </summary>
        public static float MaximumOf(params float[] values)
        {
            var maximumValue = (float) NEGATIVE_INFINITY;
            foreach (var value in values)
                if (value > maximumValue)
                    maximumValue = value;
            return maximumValue;
        }
        
        /// <summary>
        /// Returns the largest of all the values passed in.
        /// </summary>
        public static int MaximumOf(params int[] values) => (int) MaximumOf(Array.ConvertAll(values, integer => (float) integer));
        
        /// <summary>
        /// Returns the value rounded to the nearest integer.
        /// </summary>
        public static int Rounded(this float value) => (int) System.Math.Round(value, 0);

        /// <summary>
        /// Returns the value rounded to the nearest integer that is less than it (aka: floored.)
        /// </summary>
        public static int RoundedDown(this float value) => (int) System.Math.Floor(value);

        /// <summary>
        /// Returns the value rounded to the nearest integer that is greater than the it (aka: ceiling.)
        /// </summary>
        public static int RoundedUp(this float value) => (int) System.Math.Ceiling(value);
        
        /// <summary>
        /// You'll never guess what this does...it returns the square root of the value passed in, wow!
        /// </summary>
        public static float SquareRooted(this float value) => (float) System.Math.Sqrt(value);
        
        /// <summary>
        /// Returns true if the value is greater than or equal to the minimum value passed in
        /// and less than or equal to the maximum value passed in.
        /// </summary>
        public static bool IsWithin(this float value, float minimum, float maximum) => value.ClampedBetween(minimum, maximum) == value;

        public static float KeepAboveZero(this ref float value) => value = value.KeptAboveZero();

        public static float KeepBelowZero(this ref float value) => value = value.KeptBelowZero();

        public static float KeepAbove(this ref float value, float threshold) => value = value.KeptAbove(threshold);
 
        public static float KeepBelow(this ref float value, float threshold) => value = value.KeptBelow(threshold);

        public static float KeptAboveZero(this float value) => MaximumOf(value, 0f);

        public static float KeptBelowZero(this float value) => MinimumOf(value, 0f);

        public static float KeptAbove(this float value, float threshold) => MaximumOf(value, threshold);
        
        public static float KeptBelow(this float value, float threshold) => MinimumOf(value, threshold);
        
        public static int KeepAboveZero(this ref int value) => value = value.KeptAboveZero();

        public static int KeepBelowZero(this ref int value) => value = value.KeptBelowZero();

        public static int KeepAbove(this ref int value, int threshold) => value = value.KeptAbove(threshold);
 
        public static int KeepBelow(this ref int value, int threshold) => value = value.KeptBelow(threshold);

        public static int KeptAboveZero(this int value) => MaximumOf(value, 0);

        public static int KeptBelowZero(this int value) => MinimumOf(value, 0);

        public static int KeptAbove(this int value, int threshold) => MaximumOf(value, threshold);
        
        public static int KeptBelow(this int value, int threshold) => MinimumOf(value, threshold);
        
        public static float Reset(this ref float value) => value = 0f;
        
        public static float Reset(this ref int value) => value = 0;
        
        // TODO: the ref methods here might not work.
    }
}