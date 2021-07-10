using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace N8Engine.Rendering
{
    internal static class NonBlockingConsoleWriter
    {
        private static readonly BlockingCollection<string> _messagesToWrite = new();

        public static void Initialize()
        {
            Thread __thread = new(Run)
            {
                IsBackground = true
            };
            __thread.Start();
        }

        public static void Write(in string message) => _messagesToWrite.Add(message);

        [SuppressMessage("ReSharper", "FunctionNeverReturns")]
        private static void Run()
        {
            while (true)
                if (!_messagesToWrite.IsCompleted)
                    Console.Write(_messagesToWrite.Take());
        }
    }
}