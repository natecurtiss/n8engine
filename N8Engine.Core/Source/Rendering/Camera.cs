using System.Numerics;
using N8Engine.SceneManagement;
using N8Engine.Utilities;
using N8Engine.Windowing;

namespace N8Engine.Rendering;

public sealed class Camera : Cog
{
    public Vector2 Position { get; set; }
    public float Zoom { get; set; } = 1f;

    readonly WindowSize _windowSize;

    internal Camera(WindowSize windowSize) => _windowSize = windowSize;

    public Bounds Bounds()
    {
        var size = new Vector2(_windowSize.Width, _windowSize.Height);
        size /= Zoom;
        return new(Position, size);
    }

    internal Matrix4x4 ProjectionMatrix()
    {
        var left = Position.X - _windowSize.Width / 2f;
        var right = Position.X + _windowSize.Width / 2f;
        var top = Position.Y + _windowSize.Height / 2f;
        var bottom = Position.Y - _windowSize.Height / 2f;

        var orthographicMatrix = Matrix4x4.CreateOrthographicOffCenter(left, right, bottom, top, 0.01f, 100f);
        var zoomMatrix = Matrix4x4.CreateScale(Zoom);
        return orthographicMatrix * zoomMatrix;
    }
    
    void Cog.OnSceneLoad(Scene scene) { }
    void Cog.OnSceneUpdate() { }
    void Cog.OnSceneRender() { }
    void Cog.OnSceneUnload() { }
}