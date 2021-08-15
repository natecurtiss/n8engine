using System;
using N8Engine;

namespace SampleProject
{
    internal static class Program
    {
        private static void Main() => Application.Start(OnLaunch, OnNewFrame);

        private static void OnLaunch() { }

        private static void OnNewFrame() => Console.Title = Application.FramesPerSecond.ToString();
    }
}