using System;

namespace N8Engine.Internal
{
    interface IRenderingEvents
    {
        event Action OnPreRender;
        event Action OnRender;
        event Action OnPostRender;
    }
}