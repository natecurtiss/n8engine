using N8Engine.Utilities;

namespace N8Engine;

public sealed class Modules : ServiceLocator<Module, ModuleNotFoundException>
{
    static readonly Modules _singleton = new();

    protected override ModuleNotFoundException WhenMissing<T>() => new($"Module of type {typeof(T)} not found!");

    public static T Get<T>() where T : Module => _singleton.Find<T>();
    internal static void Add<T>(T module) where T : Module => _singleton.Register(module);
    internal static void Remove<T>() where T : Module => _singleton.Deregister<T>();
}