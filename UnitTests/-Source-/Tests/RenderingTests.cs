using System;
using System.IO;
using N8Engine;
using N8Engine.Mathematics;
using N8Engine.Physics;
using N8Engine.Rendering;
using NUnit.Framework;

namespace UnitTests
{
    internal sealed class RenderingTests
    {
        [Test]
        public void TestGetPixelsFromN8SpriteFile()
        {
            var file = File.ReadAllLines(PathExtensions.PathToRootFolder + "\\UnitTests\\Sprites\\dummy.n8sprite");
            var data = new N8SpriteData(file);
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
            }, data.Pixels);
        }
    }
}