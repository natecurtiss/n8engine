using N8Engine;

namespace N8Engine.Debugging
{
    /// <summary>
    /// Methods that make debugging code easier.
    /// </summary>
    public static class Debug
    {
        /// <summary>
        /// The output of all debug messages.
        /// </summary>
        private static IDebugOutput _debugOutput;
        
        /// <summary>
        /// Initializes the <see cref="Debug"/> class - called internally by <see cref="Application"> Application. </see>
        /// </summary>
        internal static void Initialize() => _debugOutput = new DebugWriteToFile();

        /// <summary>
        /// Prints a message to a text file.
        /// </summary>
        /// <param name="message"> The message to print. </param>
        public static void Log(in object message) => _debugOutput.Write(message.ToString());
    }
}