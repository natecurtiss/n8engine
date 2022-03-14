using System;
using System.Collections.Generic;
using System.Linq;

namespace N8Engine.InputSystem;

// TODO: Fix this.
public sealed class Input : GameModule
{
    enum KeyState
    {
        IsPressed, 
        IsReleased, 
        WasJustPressed, 
        WasJustReleased
    }

    public event Action<Key>? OnKeyPress;
    public event Action<Key>? OnKeyRelease;
    
    readonly Dictionary<Key, KeyState> _keyStates = new();
    readonly IEnumerable<Key> _allKeys = Enum.GetValues<Key>();

    internal Input()
    {
        foreach (var key in _allKeys)
        {
            if (key != Key.Any && key != Key.Unknown)
                _keyStates.Add(key, KeyState.IsReleased);
        }
    }

    internal void UpdateKey(Key key, bool isDown)
    {
        if (key == Key.Unknown)
            return;
        
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
        
        if (newState == KeyState.WasJustPressed)
            OnKeyPress?.Invoke(key);
        else if (newState == KeyState.WasJustReleased) 
            OnKeyRelease?.Invoke(key);
    }
    
    public bool IsReleased(Key key)
    {
        if (key == Key.Unknown)
            return false;
        if (key == Key.Any)
        {
            var keys = _keyStates.Values;
            if (keys.Any(state => state is KeyState.IsReleased or KeyState.WasJustReleased))
                return true;
        }
        else
        {
            return _keyStates[key] == KeyState.IsReleased || WasJustReleased(key);
        }
        return false;
    }

    public bool IsPressed(Key key)
    {
        if (key == Key.Unknown)
            return false;
        if (key == Key.Any)
        {
            var keys = _keyStates.Values;
            if (keys.Any(state => state is KeyState.IsPressed or KeyState.WasJustPressed))
                return true;
        }
        else
        {
            return _keyStates[key] == KeyState.IsPressed || WasJustPressed(key);
        }
        return false;
    }

    public bool WasJustReleased(Key key)
    {
        if (key == Key.Unknown)
            return false;
        if (key == Key.Any)
        {
            var keys = _keyStates.Values;
            if (keys.Any(state => state == KeyState.WasJustReleased))
                return true;
        }
        else
        {
            return _keyStates[key] == KeyState.WasJustReleased;
        }
        return false;
    }

    public bool WasJustPressed(Key key)
    {
        if (key == Key.Unknown)
            return false;
        if (key == Key.Any)
        {
            var keys = _keyStates.Values;
            if (keys.Any(state => state == KeyState.WasJustPressed))
                return true;
        }
        else
        {
            return _keyStates[key] == KeyState.WasJustPressed;
        }
        return false;
    }
}