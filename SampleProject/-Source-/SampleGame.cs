using N8Engine;
using N8Engine.SceneManagement;

namespace SampleProject
{
    sealed class SampleGame : Launcher
    {
        public override string WindowTitle => "sample game";
        public override ScreenResolution WindowSize => new(0.5f, 0.5f);
        
        public override Scene[] Scenes { get; }
    }
}