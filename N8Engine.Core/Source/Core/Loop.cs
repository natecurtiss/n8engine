using System;
using N8Engine.Windowing;

namespace N8Engine;

interface Loop
{
    event Action OnStart;
    event Action<Frame> OnUpdate;
    event Action OnRender;
}