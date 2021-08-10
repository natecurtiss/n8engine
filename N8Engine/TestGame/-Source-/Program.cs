using System;
using N8Engine;

namespace TestGame
{
    internal static class Program
    {
        private static void Main()
        {
            // Application.UseExternalErrorConsole = false;
            Application.Start(OnLaunch, OnNewFrame);
        }

        private static void OnLaunch() { }

        private static void OnNewFrame() => Console.Title = Application.FramesPerSecond.ToString();
    }
}