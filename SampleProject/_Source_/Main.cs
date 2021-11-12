using N8Engine;
using SampleProject;

Application.Build
(
    60, 
    "n8engine",
    new(1920, 1080),
    new SampleScene()
).Start();