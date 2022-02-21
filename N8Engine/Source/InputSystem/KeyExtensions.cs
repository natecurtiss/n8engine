using System;
using GLKey = Silk.NET.Input.Key;

namespace N8Engine.InputSystem;

static class KeyExtensions
{
    public static Key AsKey(this GLKey glKey)
    {
        try
        {
            var key = (Key) (int) glKey;
            return key;
        }
        catch (Exception)
        {
            return Key.Unknown;
        }
    }
}