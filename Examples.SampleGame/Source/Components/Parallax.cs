using System.Numerics;
using N8Engine;
using N8Engine.SceneManagement;

namespace SampleGame;

sealed class Parallax : Component
{
    // Values 0-1 work best.
    readonly float _speed;
    readonly Scrolling _scrolling;
    
    Transform _transform;
    float _length;
    float _startingPosition;

    public Parallax(float speed, Scrolling scrolling)
    {
        _speed = speed;
        _scrolling = scrolling;
    }

    public override void Create(GameObject gameObject, Scene scene)
    {
        _transform = gameObject.GetComponent<Transform>();
        _length = _transform.Bounds().Size.X;
        _startingPosition = _transform.Position.X;
    }

    public override void LateUpdate(Frame frame)
    {
        _transform.Position -= new Vector2(_scrolling.Speed * frame.DeltaTime * (1 - _speed), 0f);
        var distance = _startingPosition - _transform.Position.X;
        if (distance > _length)
            _transform.Position = new(_startingPosition, _transform.Position.Y);
    }
}