using GLKey = Silk.NET.Input.Key;

namespace N8Engine.InputSystem;

static class KeyExtensions
{
    public static Key AsKey(this GLKey key) => (Key) (int) key;
}