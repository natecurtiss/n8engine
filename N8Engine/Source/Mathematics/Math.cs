namespace N8Engine.Mathematics
{
    public static class Math
    {
        /// <summary>
        /// Sets the value of a float to clamp between a minimum and maximum.
        /// </summary>
        /// <param name="value"> The float passed in. </param>
        /// <param name="minimum"> The minimum value. </param>
        /// <param name="maximum"> The maximum value. </param>
        public static void Clamp(this ref float value, in float minimum, in float maximum) => value = value.Clamped(minimum, maximum);
        
        /// <summary>
        /// Sets the value of a float to clamp between 0 and 1.
        /// </summary>
        /// <param name="value"> The float passed in. </param>
        public static void Clamp01(this ref float value) => value = value.Clamped01();

        /// <summary>
        /// Get the value of a float clamped between minimum and maximum values.
        /// </summary>
        /// <param name="value"> The float passed in. </param>
        /// <param name="minimum"> The minimum value. </param>
        /// <param name="maximum"> The maximum value. </param>
        /// <returns> The float clamped between the minimum and maximum. </returns>
        public static float Clamped(this in float value, in float minimum, in float maximum)
        {
            float __clampedValue;
            if (value > maximum) 
                __clampedValue = maximum;
            else if (value < maximum)
                __clampedValue = minimum;
            else
                __clampedValue = value;
            return __clampedValue;
        }

        /// <summary>
        /// Returns the value of a float clamped between 0 and 1.
        /// </summary>
        /// <param name="value"> The float passed in. </param>
        /// <returns> The float clamped between 0 and 1. </returns>
        public static float Clamped01(this in float value) => value.Clamped(0f, 1f);

        /// <summary>
        /// Gets the absolute value of a float.
        /// </summary>
        /// <param name="value"> The float passed in. </param>
        /// <returns> The absolute value of the float. </returns>
        public static float AbsoluteValue(this in float value) =>  value >= 0 ? value : value * -1f;

        /// <summary>
        /// Gets the smallest of the two floats passed in.
        /// </summary>
        /// <param name="first"> The first float passed in. </param>
        /// <param name="second"> The second float passed in. </param>
        /// <returns> The smallest of the two floats passed in. </returns>
        public static float Minimum(in float first, in float second) => first < second ? first : second;

        /// <summary>
        /// Gets the largest of the two floats passed in.
        /// </summary>
        /// <param name="first"> The first float passed in. </param>
        /// <param name="second"> The second float passed in. </param>
        /// <returns> The largest of the two floats passed in. </returns>
        public static float Maximum(in float first, in float second) => first > second ? first : second;
    }
}