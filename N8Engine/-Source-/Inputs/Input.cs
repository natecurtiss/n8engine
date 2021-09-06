using System;
using System.Collections.Generic;
using System.Linq;
using N8Engine.Native;

namespace N8Engine.Inputs
{
    /// <summary>
    /// Just like <see cref="Application">Application,</see>
    /// I suggest going to your search engine of choice to find out what "input" is in the context of a game engine if you do not already know.
    /// </summary>
    /// <seealso cref="Key"/>
    public static class Input
    {
        private enum KeyState
        {
            IsPressed, 
            IsReleased, 
            WasJustPressed, 
            WasJustReleased
        }
        
        private static readonly Dictionary<Key, KeyState> _keys = new();
        private static readonly IReadOnlyCollection<Key> _allKeys = Enum.GetValues<Key>();
        
        internal static void Initialize()
        {
            foreach (var key in _allKeys) 
                _keys.Add(key, KeyState.IsReleased);
            GameLoop.OnPreUpdate += OnPreUpdate;
        }
        
        /// <summary>
        /// True if the <see cref="Key"/> is currently not being pressed (the opposite of <see cref="IsPressed">IsPressed.</see>)
        /// </summary>
        public static bool IsReleased(this Key key) => _keys[key] == KeyState.IsReleased;
        
        /// <summary>
        /// True if the <see cref="Key"/> is currently being pressed (the opposite of <see cref="IsReleased">IsReleased.</see>)
        /// </summary>
        public static bool IsPressed(this Key key) => _keys[key] == KeyState.IsPressed;
        
        /// <summary>
        /// True if the<see cref="Key"/> was just released in the current frame.
        /// </summary>
        public static bool WasJustReleased(this Key key) => _keys[key] == KeyState.WasJustReleased;
        
        /// <summary>
        /// True if the <see cref="Key"/> was just pressed in the current frame.
        /// </summary>
        public static bool WasJustPressed(this Key key) => _keys[key] == KeyState.WasJustPressed;

        private static void OnPreUpdate(float deltaTime)
        {
            foreach (var key in _allKeys)
            {
                var previousKeyState = _keys[key];
                var newKeyState = KeyState.IsReleased;
                var isKeyDownNow = ConsoleInputHelper.IsKeyDown(key);
                newKeyState = previousKeyState switch
                {
                    KeyState.IsPressed => isKeyDownNow ? KeyState.IsPressed : KeyState.WasJustReleased,
                    KeyState.IsReleased => isKeyDownNow ? KeyState.WasJustPressed : KeyState.IsReleased,
                    KeyState.WasJustPressed => isKeyDownNow ? KeyState.IsPressed : KeyState.WasJustReleased,
                    KeyState.WasJustReleased => isKeyDownNow ? KeyState.WasJustPressed : KeyState.IsReleased,
                    var _ => newKeyState
                };
                _keys[key] = newKeyState;
            }
        }
    }
}