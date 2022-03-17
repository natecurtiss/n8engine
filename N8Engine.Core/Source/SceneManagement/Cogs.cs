namespace N8Engine.SceneManagement;

interface Cogs
{
    void Add<T>(T cog) where T : Cog;
    void Remove<T>() where T : Cog;
}