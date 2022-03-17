using System;
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
    Scene _scene;
    string _name;

    public Sprite(string path)
    {
        // TODO: Find a better way of doing this.
        var gl = Modules.Get<Graphics>().Lib;
        Shader = new(gl, "Assets/Shaders/sprite.vert".Find(), "Assets/Shaders/sprite.frag".Find());
        Texture = new(gl, path);
    }

    public override void Create(GameObject gameObject, Scene scene)
    {
        _scene = scene;
        _name = gameObject.Name;
        _transform = gameObject.GetComponent<Transform>();
        _spriteRenderer = scene.Get<SpriteRenderer>();
    }

    public override void Destroy()
    {
        Shader.Dispose();
        Texture.Dispose();
    }

    public override void Render()
    {
        try
        {
            _spriteRenderer.AddToRenderQueue(this);
        }
        catch (Exception)
        {
            Console.WriteLine(_name + " name " + _scene.Name);
            throw;
        }
    }

    public Matrix4x4 ModelMatrix() => _transform.ModelMatrix();
}