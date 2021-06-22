using System;
using N8Engine.Core;
using N8Engine.Mathematics;

namespace N8Engine.Inputs
{
    internal static class Input
    {
        public static event Action<Key> OnKeyPressed;
        public static event Action<Vector2> OnDirectionalInput;

        public static void Initialize() => GameLoop.OnUpdate += Update;

        private static void Update(float f) => CheckInput();

        private static void CheckInput()
        {
            foreach (Key __key in Console.ReadKey(true).AsKeys())
            {
                OnKeyPressed?.Invoke(__key);
                Vector2 __directionalInput = DirectionalInputFrom(__key);
                OnDirectionalInput?.Invoke(__directionalInput);
            }
        }

        internal static Vector2 DirectionalInputFrom(in Key key) =>
            new()
            {
                X = key switch
                {
                    Key.LeftArrow or Key.A => -1f,
                    Key.RightArrow or Key.D => 1f,
                    _ => 0f
                },
                Y = key switch
                {
                    Key.DownArrow or Key.S => -1f,
                    Key.UpArrow or Key.W => 1f,
                    _ => 0f
                }
            };
    }
}