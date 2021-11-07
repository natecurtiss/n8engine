namespace N8Engine.SceneManagement
{
    public interface IScene<T>
    { 
        string Name { get; }
        int Index { get; internal set; }
        internal void Load();
        internal void Unload();
        internal void Add(T obj);
        internal void Remove(T obj);
        internal T[] ToArray();
    }
}