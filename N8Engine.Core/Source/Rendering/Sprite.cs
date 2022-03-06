using System.Numerics;
using N8Engine.Mathematics;
using N8Engine.SceneManagement;
using N8Engine.Windowing;
using Silk.NET.OpenGL;

namespace N8Engine.Rendering;

public sealed class Sprite : Component, Renderable
{
    readonly Renderer _renderer;
    readonly Camera _camera;
    readonly Transform _transform;
    readonly Texture _texture;

    Camera Renderable.Camera => _camera;
    Texture Renderable.Texture => _texture;
    Vector2 Renderable.Position => _transform.Position;
    Vector2 Renderable.Scale => _transform.Scale;
    float Renderable.Rotation => _transform.Rotation;

    public Sprite(Scene scene, GameObject gameObject, string path)
    {
        _renderer = scene.Renderer;
        _camera = scene.Camera;
        _transform = gameObject.GetComponent<Transform>();
        _texture = new(path);
    }

    public override void Destroy() => _texture.Dispose();

    internal override void Render()
    {
        _renderer.Draw(this);
    }
}