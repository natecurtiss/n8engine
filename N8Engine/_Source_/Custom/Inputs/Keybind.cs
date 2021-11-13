namespace N8Engine.InputSystem
{
    public readonly struct Keybind
    {
        public static implicit operator Keybind(Key key) => new(key);
        public static implicit operator Keybind(Key[] keys) => new(keys);

        public static readonly Keybind Left = new(Key.LeftArrow, Key.A);
        public static readonly Keybind Right = new(Key.RightArrow, Key.D);
        public static readonly Keybind Down = new(Key.DownArrow, Key.S);
        public static readonly Keybind Up = new(Key.UpArrow, Key.W);
        
        public readonly Key[] Keys;
        public Keybind(params Key[] keys) => Keys = keys;
    }

    public static class KeybindExtensions
    {
        public static bool IsReleased(this Input input, Keybind keybind)
        {
            foreach (var key in keybind.Keys)
                if (input.IsReleased(key))
                    return true;
            return false;
        }
        
        public static bool IsPressed(this Input input, Keybind keybind)
        {
            foreach (var key in keybind.Keys)
                if (input.IsPressed(key))
                    return true;
            return false;
        }
        
        public static bool WasJustReleased(this Input input, Keybind keybind)
        {
            foreach (var key in keybind.Keys)
                if (input.WasJustReleased(key))
                    return true;
            return false;
        }
        
        public static bool WasJustPressed(this Input input, Keybind keybind)
        {
            foreach (var key in keybind.Keys)
                if (input.WasJustPressed(key))
                    return true;
            return false;
        }
    }
}