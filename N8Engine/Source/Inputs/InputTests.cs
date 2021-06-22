using System;
using N8Engine.Mathematics;
using NUnit.Framework;

namespace N8Engine.Inputs
{
    public sealed class InputTests
    {
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

        [Test]
        public void TestDirectionalInput()
        {
            Vector2 __directionalInput = Input.DirectionalInputFrom(Key.A);
            Assert.IsTrue(__directionalInput == Vector2.Left);
        }
    }
}