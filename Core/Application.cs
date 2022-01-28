namespace N8Engine;

public sealed class Application
{
    readonly Loop _loop;
    
    Application(int targetFps) => _loop = new Loop(targetFps, Modules.Update);

    public static Application WithFps(int targetFps) => new(targetFps);

    public Application WithModule<T>() where T : class, Module, new() => WithModule(new T());
    public Application WithModule<T>(T module) where T : class, Module
    {
        Modules.Add(module);
        return this;
    }
    
    public Application Start()
    {
        Modules.Initialize();
        _loop.Start();
        return this;
    }
}