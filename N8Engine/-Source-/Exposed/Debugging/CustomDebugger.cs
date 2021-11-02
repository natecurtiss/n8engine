namespace N8Engine.Debugging
{
    public sealed class CustomDebugger : IDebugger
    {
        readonly ILogger _logger;
        
        internal CustomDebugger(ILogger logger) => _logger = logger ?? GetDefaultLogger();

        public void Log(object message)
        {
            var text = message == null ? "null" : message.ToString();
            _logger.Write(text);
        }
        
        ILogger GetDefaultLogger() => new DebugConsoleLogger();
    }
}
