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
            N8SpriteFile __file = new();
            Pixel[] __pixels = __file.GetPixels(new[]
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
            }, __pixels);
        }

        [Test]
        public void TestRectangleDebugPixels()
        {
            DebugRectangle __debugRectangle = new()
            {
                Size = new Vector(3, 3)
            };
            string[] __rectangleDebugPixels = __debugRectangle.Pixels;
            foreach (string __pixel in __rectangleDebugPixels) Console.WriteLine(__pixel + '\n');
            Assert.AreEqual
            (
                new[]
                {
                    "{Green,Green}{Green,Green}{Green,Green}",
                    "{Green,Green}{Clear,Clear}{Green,Green}",
                    "{Green,Green}{Green,Green}{Green,Green}"
                },
                __rectangleDebugPixels
            );
        }
    }
}