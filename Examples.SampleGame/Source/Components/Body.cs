using System.Numerics;
using N8Engine;

namespace SampleGame;

sealed class Body : Component
{
    readonly float _gravity;
    
    public Vector2 Velocity { get; set; }
    public bool UseGravity { get; set; }

    public Body(float gravity, bool useGravity = true)
    {
        _gravity = gravity;
        UseGravity = useGravity;
    }

    public override void EarlyUpdate(Frame frame)
    {
        if (!UseGravity)
            return;
        var vel = Velocity;
        // V = 1/2g * t^2
        vel.Y += _gravity * frame.DeltaTime * frame.DeltaTime;
        Velocity = vel;
    }
}