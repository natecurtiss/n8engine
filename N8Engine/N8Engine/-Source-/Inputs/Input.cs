using System;
using System.Collections.Generic;
using N8Engine.Mathematics;
using N8Engine.Native;

namespace N8Engine.Inputs
{
    public static class Input
    {
        private static readonly Dictionary<Key, KeyState> _keys = new();
        private enum KeyState { IsPressed, IsReleased, WasJustPressed, WasJustReleased }
        
        internal static void Initialize()
        {
            foreach (var key in (Key[]) Enum.GetValues(typeof(Key))) _keys.Add(key, KeyState.IsReleased);
            GameLoop.OnPreUpdate += OnPreUpdate;
        }
        
        public static bool IsReleased(this Key key) => _keys[key] == KeyState.IsReleased;
        
        public static bool IsPressed(this Key key) => _keys[key] == KeyState.IsPressed;
        
        public static bool WasJustReleased(this Key key) => _keys[key] == KeyState.WasJustReleased;
        
        public static bool WasJustPressed(this Key key) => _keys[key] == KeyState.WasJustPressed;
        
        private static void OnPreUpdate(float deltaTime)
        {
            foreach (var key in _keys.Keys)
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
                    _ => newKeyState
                };
                _keys[key] = newKeyState;
            }
        }
    }
}