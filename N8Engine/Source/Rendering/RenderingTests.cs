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
            Assert.AreEqual(ConsoleColor.Black, __file.PixelsNotRelativeToCenterPixel[0].ForegroundColor);
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
            Assert.AreEqual
            (
                __file.PixelsNotRelativeToCenterPixel[12].Position, 
                __file.GetCenterPixelPosition(__file.PixelsNotRelativeToCenterPixel)
            );
        }

        [Test]
        public void TestGetLocalPositionRelativeToCenterPixel()
        {
            N8SpriteFile __file = new();
            List<Pixel> __allPixels = new()
            {
                new Pixel(ConsoleColor.Black, ConsoleColor.Black, Vector2.Zero),
                new Pixel(ConsoleColor.Black, ConsoleColor.Black, new Vector2(0, 1)),
                new Pixel(ConsoleColor.Black, ConsoleColor.Black, new Vector2(0, 2)),
            };
            Vector2 __localPosition = __file.GetLocalPositionRelativeToCenterPixel(__allPixels, __allPixels[0]);
            Assert.AreEqual(Vector2.Down, __localPosition);
        }
    }
}