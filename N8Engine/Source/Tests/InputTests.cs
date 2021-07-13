using System;
using N8Engine.Inputs;
using N8Engine.Mathematics;
using NUnit.Framework;

namespace N8Engine.Tests
{
    /// <summary>
    /// Unit tests for the Input classes.
    /// </summary>
    internal sealed class InputTests
    {
        /// <summary>
        /// Tests conversions to and from a ConsoleKeyInfo and a key.
        /// </summary>
        [Test]
        public void TestConsoleKeyInfoConversionToKey()
        {
            ConsoleKeyInfo __consoleKeyInfo = new(' ', ConsoleKey.A, false, false, false);
            Key[] __keys = __consoleKeyInfo.AsKeys();
            Assert.IsTrue(__keys[0] == Key.A);
            
            __consoleKeyInfo = new ConsoleKeyInfo(' ', ConsoleKey.Escape, false, false, false);
            __keys = __consoleKeyInfo.AsKeys();
            Assert.IsTrue(__keys[0] == Key.Esc);
            
            __consoleKeyInfo = new ConsoleKeyInfo(' ', ConsoleKey.D1, false, false, false);
            __keys = __consoleKeyInfo.AsKeys();
            Assert.IsTrue(__keys[0] == Key.One);
            
            __consoleKeyInfo = new ConsoleKeyInfo(' ', ConsoleKey.RightArrow, true, false, false);
            __keys = __consoleKeyInfo.AsKeys();
            Assert.IsTrue(__keys[0] == Key.RightArrow && __keys[1] == Key.Shift);
        }
    }
}