using N8Engine;
using SampleGame;

new Game()
    .WithFirstScene(Scenes.Main)
    .WithWindowTitle("Flappo")
    .WithWindowSize(1300, 700)
    .WithNotResizableWindow()
    .Run();