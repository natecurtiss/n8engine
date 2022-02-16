namespace N8Engine;

public sealed class Modules : ServiceLocator<Module>
{
    public Module Get<T>() where T : Module => Locate<T>();
    internal void Add<T>(T module) where T : Module => Register(module);
    internal void Remove<T>(T module) where T : Module => Deregister<T>();
}