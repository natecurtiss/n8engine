using N8Engine;
using SampleGame;

new Game()
    .WithFirstScene(Scenes.Main)
    .WithWindowTitle("Sample Game")
    .WithWindowSize(1920, 1080)
    .Start();