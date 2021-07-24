using System;
using System.Collections.Generic;
using N8Engine.Mathematics;
using N8Engine.Native;

namespace N8Engine.Inputs
{
    /// <summary>
    /// The input system for the application.
    /// </summary>
    public static class Input
    {
        private static readonly Key[] _allKeys = (Key[])Enum.GetValues(typeof(Key));
        private static readonly List<Key> _keysUpThisFrame = new();
        private static readonly List<Key> _keysDownThisFrame = new();
        private static readonly List<Key> _keysPressedUpThisFrame = new();
        private static readonly List<Key> _keysPressedDownThisFrame = new();
        
        private static List<Key> _keysUpLastFrame = new();
        private static List<Key> _keysDownLastFrame = new();

        internal static void Initialize() => GameLoop.OnPreUpdate += OnPreUpdate;

        public static Vector MovementAxis
        {
            get
            {
                var horizontalInput = 0f;
                if (Key.D.IsDown() || Key.RightArrow.IsDown())
                    horizontalInput = 1f;
                else if (Key.A.IsDown() || Key.LeftArrow.IsDown())
                    horizontalInput = -1f;
                
                var verticalInput = 0f;
                if (Key.W.IsDown() || Key.UpArrow.IsDown())
                    verticalInput  = -1f;
                else if (Key.S.IsDown() || Key.DownArrow.IsDown())
                    verticalInput = 1f;

                var axisInput = new Vector(horizontalInput, verticalInput);
                axisInput = axisInput.Normalized;
                return new Vector(axisInput.X, axisInput.Y * 0.5f);
            }
        }
        
        public static bool IsUp(this Key key) => _keysUpThisFrame.Contains(key);
        
        public static bool IsDown(this Key key) => _keysDownThisFrame.Contains(key);

        public static bool WasPressedUpThisFrame(this Key key) => _keysPressedUpThisFrame.Contains(key);

        public static bool WasPressedDownThisFrame(this Key key) => _keysPressedDownThisFrame.Contains(key);
        
        private static void OnPreUpdate(float deltaTime)
        {
            _keysUpLastFrame = new List<Key>(_keysUpThisFrame);
            _keysDownLastFrame = new List<Key>(_keysDownThisFrame);
            _keysUpThisFrame.Clear();
            _keysDownThisFrame.Clear();
            _keysPressedUpThisFrame.Clear();
            _keysPressedDownThisFrame.Clear();
            
            foreach (var key in _allKeys)
            {
                var isKeyDown = ConsoleInput.GetKeyDown(key);
                if (isKeyDown)
                {
                    _keysDownThisFrame.Add(key);
                    var wasKeyDownLastFrame = _keysDownLastFrame.Contains(key);
                    if (!wasKeyDownLastFrame)
                        _keysPressedDownThisFrame.Add(key);
                }
                else
                {
                    _keysUpThisFrame.Add(key);
                    var wasKeyUpLastFrame = _keysUpLastFrame.Contains(key);
                    if (!wasKeyUpLastFrame)
                        _keysPressedUpThisFrame.Add(key);
                }
            }
        }
    }
}