namespace N8Engine.Inputs
{
    public interface IInput
    {
        bool IsReleased(Key key);
        bool IsPressed(Key key);
        bool WasJustReleased(Key key);
        bool WasJustPressed(Key key);

        internal void CheckInput();
    }
}