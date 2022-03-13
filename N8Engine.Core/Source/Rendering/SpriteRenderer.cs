using System.Collections.Generic;
using N8Engine.SceneManagement;
using Silk.NET.OpenGL;

namespace N8Engine.Rendering;

public sealed class SpriteRenderer : SceneModule
{
    readonly List<Sprite> _sprites = new();
    readonly float[] _vertices =
    { 
        //X     Y     Z     U   V
         0.5f,  0.5f, 0.0f, 1f, 0f,
         0.5f, -0.5f, 0.0f, 1f, 1f,
        -0.5f, -0.5f, 0.0f, 0f, 1f,
        -0.5f,  0.5f, 0.0f, 0f, 0f
    };
    readonly uint[] _indices =
    {
        0, 1, 3,
        1, 2, 3
    };
    readonly GL _gl;
    
    Camera _camera;
    BufferObject<float> _vbo;
    BufferObject<uint> _ebo;
    VertexArrayObject<float, uint> _vao;

    internal SpriteRenderer(GL gl) => _gl = gl;

    public void AddToRenderQueue(Sprite sprite) => _sprites.Add(sprite);

    void SceneModule.OnSceneLoad(Scene scene)
    {
        _camera = scene.Modules.Get<Camera>();
        
        _vbo = new(_gl, _vertices, BufferTargetARB.ArrayBuffer);
        _ebo = new(_gl, _indices, BufferTargetARB.ElementArrayBuffer);
        _vao = new(_gl, _vbo, _ebo);

        _vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
        _vao.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);
    }

    void SceneModule.OnSceneUpdate() { }

    unsafe void SceneModule.OnSceneRender()
    {
        _gl.Clear(ClearBufferMask.ColorBufferBit);
        _vao.Bind();

        foreach (var sprite in _sprites)
        {
            sprite.Shader.Use();
            sprite.Texture.Bind();
            sprite.Shader.SetUniform("uTexture0", 0);
            sprite.Shader.SetUniform("uModel", sprite.ModelMatrix());
            sprite.Shader.SetUniform("uProjection", _camera.ProjectionMatrix());
            _gl.DrawElements(PrimitiveType.Triangles, (uint) _indices.Length, DrawElementsType.UnsignedInt, null);
        }
        
        _sprites.Clear();
    }

    void SceneModule.OnSceneUnload()
    {
        _vbo.Dispose();
        _ebo.Dispose();
        _vao.Dispose();
    }
}