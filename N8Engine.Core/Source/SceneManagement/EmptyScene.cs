namespace N8Engine.SceneManagement;

public sealed class EmptyScene : Scene
{
    public override string Name => "Empty Scene";
    protected override void Load() { }
}