using N8Engine;
using N8Engine.Rendering;
using N8Engine.SceneManagement;
using SampleProject;

new Game<SampleGame>().Start();

namespace SampleProject
{
    sealed class SampleGame : Launcher
    {
        public override string WindowTitle => "sample game";
        public override ScreenResolution WindowSize => new(0.75f, 0.75f);

        public override int TargetFramerate => 60;
        public override Scene[] Scenes => new Scene[]
        {
            new SampleScene()
        };
    }
}