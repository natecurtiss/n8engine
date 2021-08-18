using System;
using N8Engine;

namespace SampleProject
{
    internal static class Program
    {
        private static void Main() => Application.Start(6, false, OnLaunch, OnNewFrame);

        private static void OnLaunch() { }

        private static void OnNewFrame() => Console.Title = Application.FramesPerSecond.ToString();
    }
}