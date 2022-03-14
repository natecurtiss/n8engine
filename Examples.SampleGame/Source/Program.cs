using N8Engine;
using SampleGame;

new Game()
    .WithFirstScene(Scenes.Main)
    .WithWindowTitle("Flappo")
    .WithWindowSize(1500, 800)
    .Start();