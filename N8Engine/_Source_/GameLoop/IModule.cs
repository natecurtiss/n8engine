namespace N8Engine
{
    public interface IModule
    {
        internal void Initialize();
        internal void Update(Time time);
    }
}