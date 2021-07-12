using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using N8Engine.Mathematics;

namespace N8Engine.Inputs
{
    /// <summary>
    /// The input system for the application.
    /// </summary>
    internal static class Input
    {
        /// <summary>
        /// Invoked when a <see cref="Key"/> is pressed.
        /// </summary>
        public static event Action<Key, float> OnKeyPressed;
        /// <summary>
        /// Invoked when a direction (WASD or arrow keys) is inputted.
        /// </summary>
        public static event Action<Vector2, float> OnDirectionalInput;

        private static Key _lastKeyPressed;
        private static Vector2 _lastDirectionInputted;

        /// <summary>
        /// Initializes the input system - called internally by <see cref="Application">Application.</see>
        /// </summary>
        public static void Initialize() => GameLoop.OnUpdate += _ => CheckInput();

        /// <summary>
        /// Returns a vector that holds the directional input (WASD or arrow keys) from the key passed in.
        /// </summary>
        /// <param name="key"> The key passed in. </param>
        /// <returns> A vector that holds directional input (WASD or arrow keys) from the key passed in. </returns>
        public static Vector2 DirectionalInputFrom(in Key key)
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
        
        /// <summary>
        /// Checks for <see cref="Key"/> and directional input from the user.
        /// </summary>
        private static void CheckInput()
        {
            if (!Console.KeyAvailable)
            {
                _lastKeyPressed = Key.None;
                _lastDirectionInputted = Vector2.Zero;
                return;
            }
            
            foreach (Key __key in Console.ReadKey(true).AsKeys())
            {
                if (__key != _lastKeyPressed)
                    OnKeyPressed?.Invoke(__key, GameLoop.DeltaTime);
                Vector2 __directionalInput = DirectionalInputFrom(__key);
                if (__directionalInput != _lastDirectionInputted)
                {
                    Debug.Log(__directionalInput + " " + _lastDirectionInputted);
                    OnDirectionalInput?.Invoke(__directionalInput, GameLoop.DeltaTime);
                }
                _lastKeyPressed = __key;
                _lastDirectionInputted = __directionalInput;
            }
        }
    }
}