using System;
using N8Engine.Rendering;

namespace N8Engine.Internal
{
    // TODO: maybe make this a public class down the road if IRenderer gets exposed.
    sealed class RenderingEvents
    {
        public event Action<IRenderer> OnPreRender;
        public event Action OnRender;

        public void Invoke(IRenderer renderer)
        {
            OnPreRender?.Invoke(renderer);
            OnRender?.Invoke();
        }
    }
}