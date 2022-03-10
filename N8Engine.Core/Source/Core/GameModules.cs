using N8Engine.Utilities;

namespace N8Engine;

public sealed class GameModules : ServiceLocator<GameModule, ModuleNotFoundException>
{
    internal GameModules() { }

    protected override ModuleNotFoundException ServiceNotFoundException<T>() => new($"Module of type {typeof(T)} not found!");

    public T Get<T>() where T : GameModule => Locate<T>();
    internal void Add<T>(T module) where T : GameModule => Register(module);
    internal void Remove<T>() where T : GameModule => Deregister<T>();
}