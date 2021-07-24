using System;
using N8Engine.Mathematics;
using N8Engine.Native;
using NUnit.Framework;
using Shared;

namespace N8Engine.Rendering
{
    internal sealed class RenderingTests
    {
        [Test]
        public void TestGetPixelsFromN8SpriteFile()
        {
            var file = new N8SpriteFile($"{PathExtensions.PathToRootFolder}\\N8Engine\\-Source-\\Rendering\\dummy.n8sprite");
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
            }, file.Pixels);
        }
    }
}