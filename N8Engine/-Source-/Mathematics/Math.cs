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
        /// Returns the smallest of all the values passed in.
        /// </summary>
        public static int MinimumOf(params int[] values) => (int) MinimumOf(Array.ConvertAll(values, integer => (float) integer));
        /// <summary>
        /// Returns the largest of all the values passed in.
        /// </summary>
        public static int MaximumOf(params int[] values) => (int) MaximumOf(Array.ConvertAll(values, integer => (float) integer));
        
        //TODO add extension methods as normal methods here.
    }
}