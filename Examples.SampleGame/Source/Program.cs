using N8Engine;
using SampleGame;

new Game()
    .WithFirstScene(Scenes.Main)
    .WithWindowTitle("Flappo")
    .WithWindowSize(1920, 1080)
    .Maximized()
    .Start();