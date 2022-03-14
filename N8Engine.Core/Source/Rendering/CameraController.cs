using System;
using N8Engine.InputSystem;
using N8Engine.SceneManagement;
using N8Engine.Utilities;

namespace N8Engine.Rendering;

public sealed class CameraController : Component
{
    readonly Input? _input;
    readonly float _speed;
    Camera? _camera;
    
    public CameraController(float speed)
    {
        _input = Game.Modules.Get<Input>();
        _speed = speed;
    }

    public override void Create(Scene scene) => _camera = scene.Modules.Get<Camera>();

    public override void Update(Frame frame)
    {
        if (_camera is null)
            throw new InvalidOperationException($"{nameof(_camera)} was null");

        if (_input is null)
            throw new InvalidOperationException($"{nameof(_input)} was null");
        
        _camera.Position += _input.Axis() * (_speed * frame.DeltaTime);  
    } 
}