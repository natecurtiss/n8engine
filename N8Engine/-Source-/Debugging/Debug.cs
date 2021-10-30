using JetBrains.Annotations;

namespace N8Engine.Debugging
{
    public sealed class Debug
    {
        [NotNull]
        readonly ILogger _logger;
        
        internal Debug([CanBeNull] ILogger logger) => _logger = logger ?? GetDefaultLogger();

        public void Log([CanBeNull] object message)
        {
            var text = message == null ? "null" : message.ToString();
            _logger.Write(text);
        }
        
        [NotNull, MustUseReturnValue]
        ILogger GetDefaultLogger() => new DebugConsoleLogger();
    }
}
