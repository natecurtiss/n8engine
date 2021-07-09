using System;
using System.Collections.Generic;
using N8Engine.Mathematics;
using NUnit.Framework;

namespace N8Engine.Rendering
{
    internal sealed class RenderingTests
    {
        [Test]
        public void TestLoadN8SpriteFile()
        {
            N8SpriteFile __file = new(@"C:\Users\NateDawg\RiderProjects\N8Engine\N8Engine\Source\Rendering\dummy.n8sprite");
            Assert.AreEqual(ConsoleColor.Black, __file.Pixels[0].ForegroundColor);
        }
        
        [Test]
        public void TestSeparateCurrentLine()
        {
            N8SpriteFile __file = new();
            List<string> __separatedLines = __file.SeparateCurrentLine("{Test,Test}{OtherTest,SomeOtherTest}");
            Assert.AreEqual("Test,Test", __separatedLines[0]);
        }

        [Test]
        public void TestGetPixelFromPixelSet()
        {
            N8SpriteFile __file = new();
            Pixel __pixel = __file.GetPixelFromPixelSet("Black,White", Vector2.Zero);
            Assert.AreEqual(ConsoleColor.Black, __pixel.ForegroundColor);
            Assert.AreEqual(ConsoleColor.White, __pixel.BackgroundColor);
        }

        [Test]
        public void TestCenterOfN8SpriteFilePixels()
        {
            N8SpriteFile __file = new(@"C:\Users\NateDawg\RiderProjects\N8Engine\N8Engine\Source\Rendering\dummy.n8sprite");
            Assert.AreEqual(ConsoleColor.White, __file.CenterPixel.ForegroundColor);
        }
    }
}