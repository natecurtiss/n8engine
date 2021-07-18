namespace N8Engine.Mathematics
{
    public static class Math
    {
        /// <summary>
        /// Get the value of a float clamped between minimum and maximum values.
        /// </summary>
        /// <param name="value"> The float passed in. </param>
        /// <param name="minimum"> The minimum value. </param>
        /// <param name="maximum"> The maximum value. </param>
        /// <returns> The float clamped between the minimum and maximum. </returns>
        public static float Clamped(this in float value, in float minimum, in float maximum)
        {
            var clampedValue = value;
            if (value > maximum) 
                clampedValue = maximum;
            else if (value < minimum)
                clampedValue = minimum;
            return clampedValue;
        }
        
        /// <summary>
        /// Get the value of an int clamped between minimum and maximum values.
        /// </summary>
        /// <param name="value"> The int passed in. </param>
        /// <param name="minimum"> The minimum value. </param>
        /// <param name="maximum"> The maximum value. </param>
        /// <returns> The float clamped between the minimum and maximum. </returns>
        public static int Clamped(this in int value, in int minimum, in int maximum)
        {
            var clampedValue = value;
            if (value > maximum) 
                clampedValue = maximum;
            else if (value < minimum)
                clampedValue = minimum;
            return clampedValue;
        }

        /// <summary>
        /// Returns the value of a float clamped between 0 and 1.
        /// </summary>
        /// <param name="value"> The float passed in. </param>
        /// <returns> The float clamped between 0 and 1. </returns>
        public static float Clamped01(this in float value) => value.Clamped(0f, 1f);
        
        /// <summary>
        /// Returns the value of an int clamped between 0 and 1.
        /// </summary>
        /// <param name="value"> The int passed in. </param>
        /// <returns> The float clamped between 0 and 1. </returns>
        public static int Clamped01(this in int value) => value.Clamped(0, 1);

        /// <summary>
        /// Gets the absolute value of a float.
        /// </summary>
        /// <param name="value"> The float passed in. </param>
        /// <returns> The absolute value of the float. </returns>
        public static float AbsoluteValue(this in float value) =>  value >= 0 ? value : value * -1f;

        /// <summary>
        /// Gets the absolute value of an int.
        /// </summary>
        /// <param name="value"> The int passed in. </param>
        /// <returns> The absolute value of the int. </returns>
        public static int AbsoluteValue(this in int value) =>  value >= 0 ? value : value * -1;

        /// <summary>
        /// Gets the smallest of the two floats passed in.
        /// </summary>
        /// <param name="first"> The first float passed in. </param>
        /// <param name="second"> The second float passed in. </param>
        /// <returns> The smallest of the two floats passed in. </returns>
        public static float Minimum(in float first, in float second) => first < second ? first : second;
        
        /// <summary>
        /// Gets the smallest of the two ints passed in.
        /// </summary>
        /// <param name="first"> The first int passed in. </param>
        /// <param name="second"> The second int passed in. </param>
        /// <returns> The smallest of the two ints passed in. </returns>
        public static int Minimum(in int first, in int second) => first < second ? first : second;

        /// <summary>
        /// Gets the largest of the two floats passed in.
        /// </summary>
        /// <param name="first"> The first float passed in. </param>
        /// <param name="second"> The second float passed in. </param>
        /// <returns> The largest of the two floats passed in. </returns>
        public static float Maximum(in float first, in float second) => first > second ? first : second;
        
        /// <summary>
        /// Gets the largest of the two ints passed in.
        /// </summary>
        /// <param name="first"> The first int passed in. </param>
        /// <param name="second"> The second int passed in. </param>
        /// <returns> The largest of the two ints passed in. </returns>
        public static int Maximum(in int first, in int second) => first > second ? first : second;

        public static int Rounded(this in float value) => (int) System.Math.Round(value, 0);

        public static int Floored(this in float value) => (int) System.Math.Floor(value);

        public static int Ceiling(this in float value) => (int)System.Math.Ceiling(value);
        
        public static float SquareRoot(this in float value) => (float) System.Math.Sqrt(value);

        public static bool IsWithinRange(this in float value, in float minimum, in float maximum) => value.Clamped(minimum, maximum) == value;
    }
}