using System.Collections.Generic;

namespace N8Engine;

public sealed class Modules : ServiceLocator<Module>
{
    internal Modules() { }

    internal new int Count => base.Count;
    
    public T Get<T>() where T : Module
    {
        try
        {
            return Locate<T>();
        }
        catch (KeyNotFoundException)
        {
            throw new ModuleNotFoundException($"Module of type {typeof(T)} not found!");
        }
    }

    internal void Add<T>(T module) where T : Module => Register(module);
    internal void Remove<T>() where T : Module
    {
        if (!Deregister<T>())
            throw new ModuleNotFoundException($"Module of type {typeof(T)} does not exist to remove!");
    }
}