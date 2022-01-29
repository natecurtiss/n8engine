namespace N8Engine;

public static class Modules
{
    static readonly Dictionary<Type, Module> _dict = new();

    public static T Get<T>() where T : class, Module
    {
        var type = typeof(T);
        if (_dict[type] is not T module)
            throw new MissingModuleException($"There is no module of type {type} to get!");
        return module;
    }

    internal static void Add<T>(out T module) where T : class, Module, new()
    {
        var type = typeof(T);
        module = new T();
        if (_dict.ContainsKey(type))
            _dict.Remove(type);
        _dict.Add(type, module);
    }
    
    internal static void Add<T>(T module) where T : class, Module
    {
        var type = typeof(T);
        if (_dict.ContainsKey(type))
            _dict.Remove(type);
        _dict.Add(type, module);
    }
    
    internal static void Remove<T>() where T : class, Module
    {
        var type = typeof(T);
        if (_dict.ContainsKey(type))
            _dict.Remove(type);
        else
            throw new MissingModuleException($"There is no module of type {type} to remove!");
    }

    internal static void Update(Frame frame)
    {
        foreach (var module in _dict.Values)
            module.Update(frame);
    }
}