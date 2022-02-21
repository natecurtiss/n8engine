using System.Collections.Generic;

namespace N8Engine;

public sealed class Modules : ServiceLocator<Module>
{
    internal Modules() { }
    
    public T Get<T>() where T : Module
    {
        try
        {
            return Locate<T>();
        }
        catch (KeyNotFoundException)
        {
            throw new MissingModuleException($"Module of type {typeof(T)} not found!");
        }
    }

    internal void Add<T>(T module) where T : Module => Register(module);
    internal void Remove<T>() where T : Module
    {
        try
        {
            Deregister<T>();
        }
        catch (KeyNotFoundException)
        {
            throw new MissingModuleException($"Module of type {typeof(T)} does not exist to remove!");
        }
    }
}