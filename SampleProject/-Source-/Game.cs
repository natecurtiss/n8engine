using System;
using N8Engine;
using N8Engine.SceneManagement;

namespace SampleProject
{
    internal sealed class Game : Launcher
    {
        public override Scene[] Scenes => new Scene[]
        {
            new Level1Scene()
        };
        public override short CameraSize => 6;
        public override string PathToDebugLogsFile => "../../../.logs";

        private static void Main() => Application.Start(new Game());

        public override void OnLaunched() { }

        public override void OnEveryFrame() => Console.Title = Application.FramesPerSecond.ToString();
    }
}