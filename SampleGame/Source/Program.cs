using N8Engine;
using SampleGame;

new Game()
    .WithWindowTitle("Sample Game")
    .WithFps(60)
    .WithFirstScene(Scenes.MainScene)
    .Start();