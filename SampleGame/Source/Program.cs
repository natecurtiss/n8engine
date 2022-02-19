using N8Engine;
using SampleGame;

new Game()
    .WithFirstScene(Scenes.MainScene)
    .WithWindowTitle("Sample Game")
    .Maximized()
    .Start();