using System;

namespace N8Engine.Utilities
{
    public static class ArrayExtensions
    {
        public static void ForEach<T>(this T[] array, Action<T, int> callback)
        {
            for (var index = 0; index < array.Length; index++)
                callback(array[index], index);
        }        
        
        public static void ForEach<T>(this T[] array, Action<T> callback) => array.ForEach((item, _) => callback(item));
    }
}