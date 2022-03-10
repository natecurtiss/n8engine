using N8Engine.Utilities;

namespace N8Engine;

public sealed class GameModules : ServiceLocator<GameModule, GameModuleNotFoundException>
{
    internal GameModules() { }
    protected override GameModuleNotFoundException ServiceNotFoundException<T>() => new($"Module of type {typeof(T)} not found!");
}