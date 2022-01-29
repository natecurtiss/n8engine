using Example;
using N8Engine;

Application.New()
    .WithFps(60)
    .WithFloatingWindow(1280, 720, "-=n8engine=-")
    .WithLevels
    (
        new TestLevel()
    );