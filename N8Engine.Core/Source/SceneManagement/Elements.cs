namespace N8Engine.SceneManagement;

interface Elements
{
    void Add<T>(T cog) where T : Element;
    void Remove<T>() where T : Element;
}