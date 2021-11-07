using System.Drawing;
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
        public override int FontSize => 1;
        public override Color BackgroundColor => Color.Black;

        public override int TargetFramerate => 60;
        // TODO: I hate this so much I probably should refactor at some point.
        public override IScene<GameObject>[] Scenes => new IScene<GameObject>[]
        {
            Scene.Create<SampleScene>("level 1")
        };
    }
}