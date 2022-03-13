using System.Numerics;
using N8Engine.SceneManagement;
using N8Engine.Utilities;

namespace N8Engine.Rendering;

public sealed class Sprite : Component
{
    internal readonly Shader Shader;
    internal readonly Texture Texture;
    
    SpriteRenderer _spriteRenderer;
    Transform _transform;

    public Sprite(string path)
    {
        var gl = Game.Modules.Get<Graphics>().Lib;
        Shader = new(gl, "Assets/Shaders/sprite.vert".Find(), "Assets/Shaders/sprite.frag".Find());
        Texture = new(gl, path);
    }

    public override void Create(GameObject gameObject, Scene scene)
    {
        _transform = gameObject.GetComponent<Transform>();
        _spriteRenderer = scene.Modules.Get<SpriteRenderer>();
    }

    public override void Destroy()
    {
        Shader.Dispose();
        Texture.Dispose();
    }

    public override void Render() => _spriteRenderer.AddToRenderQueue(this);

    public Matrix4x4 ModelMatrix() => _transform.ModelMatrix();
}