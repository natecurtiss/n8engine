namespace N8Engine;

public static class Modules
{
    static readonly Dictionary<Type, Module> _dict = new();
    static bool _wasInitialized;

    public static T Get<T>() where T : class, Module
    {
        var type = typeof(T);
        if (_dict[type] is not T module)
            throw new MissingModuleException($"There is no module of type {type}!");
        return module;
    }

    public static void Add<T>(out T module) where T : class, Module, new()
    {
        var type = typeof(T);
        module = new T();
        if (_wasInitialized) module.Initialize();
        if (_dict.ContainsKey(type))
            _dict.Remove(type);
        _dict.Add(type, module);
    }
    
    public static void Add<T>(T module) where T : class, Module
    {
        var type = typeof(T);
        if (_wasInitialized) module.Initialize();
        if (_dict.ContainsKey(type))
            _dict.Remove(type);
        _dict.Add(type, module);
    }
    
    public static void Remove<T>() where T : class, Module
    {
        var type = typeof(T);
        if (_dict.ContainsKey(type))
            _dict.Remove(type);
    }

    internal static void Initialize()
    {
        if (_wasInitialized) return;
        _wasInitialized = true;
        foreach (var module in _dict.Values)
            module.Initialize();
    }

    internal static void Update(Frame frame)
    {
        foreach (var module in _dict.Values)
            module.Update(frame);
    }
}