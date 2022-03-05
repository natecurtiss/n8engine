using System;
using Silk.NET.OpenGL;

namespace N8Engine;

interface Loop
{
    event Action<Frame> OnUpdate;
    event Action<GL> OnRender;

}