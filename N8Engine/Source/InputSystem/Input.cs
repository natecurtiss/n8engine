using System;
using System.Collections.Generic;

namespace N8Engine.InputSystem;

public sealed class Input : Module
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

    internal Input()
    {
        foreach (var key in _allKeys) 
            _keyStates.Add(key, KeyState.IsReleased);
    }

    internal void UpdateKey(Key key, bool isDown)
    {
        var oldState = _keyStates[key];
        var newState = KeyState.IsReleased;
        newState = oldState switch
        {
            KeyState.IsPressed => isDown ? KeyState.IsPressed : KeyState.WasJustReleased,
            KeyState.IsReleased => isDown ? KeyState.WasJustPressed : KeyState.IsReleased,
            KeyState.WasJustPressed => isDown ? KeyState.IsPressed : KeyState.WasJustReleased,
            KeyState.WasJustReleased => isDown ? KeyState.WasJustPressed : KeyState.IsReleased,
            _ => newState
        };
        _keyStates[key] = newState;
    }
    
    public bool IsReleased(Key key) => _keyStates[key] == KeyState.IsReleased || WasJustReleased(key);
    public bool IsPressed(Key key) => _keyStates[key] == KeyState.IsPressed || WasJustPressed(key);
    public bool WasJustReleased(Key key) => _keyStates[key] == KeyState.WasJustReleased;
    public bool WasJustPressed(Key key) => _keyStates[key] == KeyState.WasJustPressed;
}