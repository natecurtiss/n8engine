using N8Engine.InputSystem;
using N8Engine.SceneManagement;

namespace N8Engine.Rendering;

public sealed class CameraController : Component
{
    readonly Input _input;
    readonly float _speed;
    Camera _camera;
    
    public CameraController(float speed)
    {
        _input = Game.Modules.Get<Input>();
        _speed = speed;
    }

    public override void Create(Scene scene) => _camera = scene.Modules.Get<Camera>();
    public override void Update(Frame frame) => _camera.Position += _input.Axis() * (_speed * frame.DeltaTime);
}