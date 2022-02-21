using N8Engine.SceneManagement;

namespace N8Engine.InputSystem;

public sealed class InputDebugger : Component
{
    readonly Action<Key> _onInput;
    Input _input = default!;

    public InputDebugger(Action<Key> onInput) => _onInput = onInput;
    
    public override void Create(GameObject gameObject, Scene scene)
    {
        _input = Game.Modules.Get<Input>();
        _input.OnKeyPress += KeyDown;
    }

    public override void Destroy() => _input.OnKeyPress -= KeyDown;

    void KeyDown(Key key) => _onInput(key);
}