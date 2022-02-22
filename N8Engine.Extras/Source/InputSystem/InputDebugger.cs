using N8Engine.SceneManagement;

namespace N8Engine.InputSystem;

public sealed class InputDebugger : Component
{
    readonly Action<Key> _onInput;
    readonly Input _input;

    public InputDebugger(Action<Key> onInput)
    {
        _onInput = onInput;
        _input = Game.Modules.Get<Input>();
        _input.OnKeyPress += KeyDown;
    }

    public override void Destroy() => _input.OnKeyPress -= KeyDown;

    void KeyDown(Key key) => _onInput(key);
}