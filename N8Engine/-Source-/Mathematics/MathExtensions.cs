﻿using SysMath = System.Math;
using static N8Engine.Mathematics.Math;

namespace N8Engine.Mathematics
{
    /// <summary>
    /// Extension methods relating to math lol.
    /// </summary>
    public static class MathExtensions
    {
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
        /// Returns the value rounded to the nearest integer.
        /// </summary>
        public static int Rounded(this float value) => (int) SysMath.Round(value, 0);
        /// <summary>
        /// Returns the value rounded to the nearest integer that is less than it (aka: floored.)
        /// </summary>
        public static int RoundedDown(this float value) => (int) SysMath.Floor(value);
        /// <summary>
        /// Returns the value rounded to the nearest integer that is greater than the it (aka: ceiling.)
        /// </summary>
        public static int RoundedUp(this float value) => (int) SysMath.Ceiling(value);
        
        /// <summary>
        /// You'll never guess what this does...it returns the square root of the value passed in, wow!
        /// </summary>
        public static float SquareRooted(this float value) => (float) SysMath.Sqrt(value);
        
        /// <summary>
        /// Returns true if the value is greater than or equal to the minimum value passed in
        /// and less than or equal to the maximum value passed in.
        /// </summary>
        public static bool IsWithin(this float value, float minimum, float maximum) => value.ClampedBetween(minimum, maximum) == value;
        public static bool IsWithin(this int value, int minimum, int maximum) => value.ClampedBetween(minimum, maximum) == value;

        public static float KeptAbove(this float value, float threshold) => MaximumOf(value, threshold);
        public static float KeptBelow(this float value, float threshold) => MinimumOf(value, threshold);
        public static float KeptAboveZero(this float value) => MaximumOf(value, 0f);
        public static float KeptBelowZero(this float value) => MinimumOf(value, 0f);
        
        public static int KeptAbove(this int value, int threshold) => MaximumOf(value, threshold);
        public static int KeptBelow(this int value, int threshold) => MinimumOf(value, threshold);
        public static int KeptAboveZero(this int value) => MaximumOf(value, 0);
        public static int KeptBelowZero(this int value) => MinimumOf(value, 0);
    }
}