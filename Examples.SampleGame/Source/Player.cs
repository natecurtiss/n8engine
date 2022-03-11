using N8Engine;
using N8Engine.InputSystem;

namespace SampleGame;

sealed class Player : Component
{
    readonly Input _input;
    readonly float _speed;
    Transform _transform;

    public Player(float speed)
    {
        _input = Game.Modules.Get<Input>();
        _speed = speed;
    }

    public override void Create(GameObject gameObject) => _transform = gameObject.GetComponent<Transform>();
    public override void Update(Frame frame) => _transform.Position += _speed * frame.DeltaTime * _input.Axis();
}