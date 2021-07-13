using System;
using N8Engine.Mathematics;
using N8Engine.Native;

namespace N8Engine.Inputs
{
    /// <summary>
    /// The input system for the application.
    /// </summary>
    public static class Input
    {
        /// <summary>
        /// Invoked when a <see cref="Key"/> is pressed.
        /// </summary>
        internal static event Action<Key, float> OnKeyPressed;
        /// <summary>
        /// Invoked when a direction (WASD or arrow keys) is inputted.
        /// </summary>
        internal static event Action<Vector2, float> OnDirectionalInput;

        public static bool GetKeyDown(in Key key) => ConsoleInput.GetKeyDown(key);

        /// <summary>
        /// Returns a vector that holds the directional input (WASD or arrow keys) from the key passed in.
        /// </summary>
        /// <param name="key"> The key passed in. </param>
        /// <returns> A vector that holds directional input (WASD or arrow keys) from the key passed in. </returns>
        internal static Vector2 DirectionalInputFrom(in Key key)
        {
            float __x = key switch
            {
                Key.LeftArrow or Key.A => -1,
                Key.RightArrow or Key.D => 1,
                _ => 0
            };
            float __y = key switch
            {
                Key.DownArrow or Key.S => -1,
                Key.UpArrow or Key.W => 1,
                _ => 0
            };
            return new Vector2(__x, __y);
        }
    }
}