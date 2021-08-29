using System;
using N8Engine;
using N8Engine.SceneManagement;

namespace SampleProject
{
    sealed class Game : Launcher
    {
        public override Scene[] Scenes => new Scene[]
        {
            new Level1Scene()
        };
        public override short FontSize => 6;

        static void Main() => Application.Start(new Game());
    }
}