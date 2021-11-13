using System;
using System.Collections.Generic;
using N8Engine.External;

namespace N8Engine.InputSystem
{
    public sealed class Input : IModule
    {
        enum KeyState
        {
            IsPressed, 
            IsReleased, 
            WasJustPressed, 
            WasJustReleased
        }
        
        readonly Dictionary<Key, KeyState> _keyStates = new();
        readonly IEnumerable<Key> _allKeys = Enum.GetValues<Key>();
        
        Type IModule.Type => GetType();
        
        void IModule.Initialize()
        {
            foreach (var key in _allKeys) 
                _keyStates.Add(key, KeyState.IsReleased);
        }
        
        void IModule.Update(Time time)
        {
            foreach (var key in _allKeys)
            {
                var oldState = _keyStates[key];
                var newState = KeyState.IsReleased;
                var isDown = ExtInput.IsDown(key);
                newState = oldState switch
                {
                    KeyState.IsPressed => isDown ? KeyState.IsPressed : KeyState.WasJustReleased,
                    KeyState.IsReleased => isDown ? KeyState.WasJustPressed : KeyState.IsReleased,
                    KeyState.WasJustPressed => isDown ? KeyState.IsPressed : KeyState.WasJustReleased,
                    KeyState.WasJustReleased => isDown ? KeyState.WasJustPressed : KeyState.IsReleased,
                    var _ => newState
                };
                _keyStates[key] = newState;
            }
        }
        
        public bool IsReleased(Key key) => _keyStates[key] == KeyState.IsReleased;
        public bool IsPressed(Key key) => _keyStates[key] == KeyState.IsPressed;
        public bool WasJustReleased(Key key) => _keyStates[key] == KeyState.WasJustReleased;
        public bool WasJustPressed(Key key) => _keyStates[key] == KeyState.WasJustPressed;
    }
}