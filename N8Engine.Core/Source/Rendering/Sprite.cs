using N8Engine.Mathematics;
using N8Engine.SceneManagement;
using N8Engine.Windowing;

namespace N8Engine.Rendering;

public sealed class Sprite : Component
{
    readonly SpriteRenderer _spriteRenderer;
    readonly Transform _transform;

    public Sprite(Scene scene, GameObject gameObject)
    {
        _spriteRenderer = scene.SpriteRenderer;
        _transform = gameObject.GetComponent<Transform>();
    }

    internal override void Render(WindowRendering rendering)
    {
        _spriteRenderer.Draw();
    }
}