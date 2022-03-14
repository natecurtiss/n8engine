using System;
using N8Engine;

namespace SampleGame;

sealed class Player : Component
{
    readonly float _jump;
    readonly Func<bool> _whenToJump;
    
    Transform _transform;
    Body _body;
    
    public bool IsEnabled { get; set; }

    public Player(float jump, Func<bool> whenToJump)
    {
        _jump = jump;
        _whenToJump = whenToJump;
    }

    public override void Create(GameObject gameObject)
    {
        _transform = gameObject.GetComponent<Transform>();
        _body = gameObject.GetComponent<Body>();
        Events.OnPlayerStart.Add(Enable);
    }

    public override void Destroy() => Events.OnPlayerStart.Remove(Enable);

    public override void Update(Frame frame)
    {
        if (!IsEnabled)
            return;
        if (_whenToJump())
            _body.Velocity = new(0, _jump);
        _transform.Position += _body.Velocity;
    }

    void Enable()
    {
        IsEnabled = true;
        _body.UseGravity = true;
    }
}