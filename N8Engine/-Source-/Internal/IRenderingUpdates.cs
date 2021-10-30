using System;

namespace N8Engine.Internal
{
    interface IRenderingUpdates
    {
        event Action OnPreRender;
        event Action OnRender;
        event Action OnPostRender;
    }
}