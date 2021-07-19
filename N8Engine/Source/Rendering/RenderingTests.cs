using System;
using N8Engine.Mathematics;
using NUnit.Framework;

namespace N8Engine.Rendering
{
    internal sealed class RenderingTests
    {
        [Test]
        public void TestGetPixelsFromN8SpriteFile()
        {
            var file = new N8SpriteFile();
            var pixels = file.GetPixels(new[]
            {
                "{Green,Green}{Clear,Clear}{Green,Green}",
                "{Black,White},{Black,Black}{Clear,Clear}",
                "{Black,White},{Black,Black}{Clear,Clear}"
            }).ToArray();
            Assert.AreEqual(new Pixel[]
            {
                new(ConsoleColor.Black, ConsoleColor.White, new Vector(-2,-1)),
                new(ConsoleColor.Black, ConsoleColor.White, new Vector(-1,-1)),
                new(ConsoleColor.Black, ConsoleColor.Black, new Vector(0,-1)),
                new(ConsoleColor.Black, ConsoleColor.Black, new Vector(1,-1)),
                new(ConsoleColor.Black, ConsoleColor.White, new Vector(-2,0)),
                new(ConsoleColor.Black, ConsoleColor.White, new Vector(-1,0)),
                new(ConsoleColor.Black, ConsoleColor.Black, new Vector(0,0)),
                new(ConsoleColor.Black, ConsoleColor.Black, new Vector(1,0)),
                new(ConsoleColor.Green, ConsoleColor.Green, new Vector(-2,1)),
                new(ConsoleColor.Green, ConsoleColor.Green, new Vector(-1,1)),
                new(ConsoleColor.Green, ConsoleColor.Green, new Vector(2,1)),
                new(ConsoleColor.Green, ConsoleColor.Green, new Vector(3,1)),
            }, pixels);
        }

        [Test]
        public void TestRectangleDebugPixels()
        {
            var debugRectangle = new DebugRectangle(new Vector(3, 3), (StaticObject) Vector.Zero);
            var rectangleDebugPixels = debugRectangle.Pixels;
            foreach (var pixel in rectangleDebugPixels) Console.WriteLine(pixel + '\n');
            Assert.AreEqual
            (
                new[]
                {
                    "{Green,Green}{Green,Green}{Green,Green}",
                    "{Green,Green}{Clear,Clear}{Green,Green}",
                    "{Green,Green}{Green,Green}{Green,Green}"
                },
                rectangleDebugPixels
            );
        }
    }
}