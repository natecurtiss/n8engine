using N8Engine;
using N8Engine.InputSystem;

namespace SampleGame;

sealed class Player : Component
{
    readonly Input _input;
    readonly float _jump;
    Transform _transform;

    public Player(float jump)
    {
        _input = Game.Modules.Get<Input>();
        _jump = jump;
    }

    public override void Create(GameObject gameObject) => _transform = gameObject.GetComponent<Transform>();
}