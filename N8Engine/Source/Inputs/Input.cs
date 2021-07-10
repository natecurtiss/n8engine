using System;
using N8Engine.Mathematics;

namespace N8Engine.Inputs
{
    /// <summary>
    /// The input system for the application.
    /// </summary>
    internal static class Input
    {
        /// <summary>
        /// Invoked when a key is pressed.
        /// </summary>
        public static event Action<Key> OnKeyPressed;
        /// <summary>
        /// Invoked when a direction (WASD or arrow keys) is inputted.
        /// </summary>
        public static event Action<Vector2> OnDirectionalInput;

        /// <summary>
        /// Initializes the input system.
        /// </summary>
        public static void Initialize()
        {
            //GameLoop.OnUpdate += Update;
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="f"> A useless parameter that exists for the sake of subscribing the GameLoop.OnUpdate event. </param>
        private static void Update(float f) => CheckInput();

        /// <summary>
        /// Checks the input for the current frame.
        /// </summary>
        private static void CheckInput()
        {
            foreach (Key __key in Console.ReadKey(true).AsKeys())
            {
                OnKeyPressed?.Invoke(__key);
                Vector2 __directionalInput = DirectionalInputFrom(__key);
                OnDirectionalInput?.Invoke(__directionalInput);
            }
        }

        /// <summary>
        /// Returns a vector that holds the directional input (WASD or arrow keys) from the key passed in.
        /// </summary>
        /// <param name="key"> The key passed in. </param>
        /// <returns> A vector that holds directional input (WASD or arrow keys) from the key passed in. </returns>
        internal static Vector2 DirectionalInputFrom(in Key key) =>
            new()
            {
                X = key switch
                {
                    Key.LeftArrow or Key.A => -1,
                    Key.RightArrow or Key.D => 1,
                    _ => 0
                },
                Y = key switch
                {
                    Key.DownArrow or Key.S => -1,
                    Key.UpArrow or Key.W => 1,
                    _ => 0
                }
            };
    }
}