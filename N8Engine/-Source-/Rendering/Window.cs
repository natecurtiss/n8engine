using System;
using System.Drawing;
using N8Engine.External;
using N8Engine.External.User;
using N8Engine.Internal;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class Window
    {
        readonly IntPtr _handle;
        readonly IInternalEvents _internalEvents;
        
        internal Window(string title, IntVector size, IntPtr handle, IInternalEvents internalEvents)
        {
            _handle = handle;
            _internalEvents = internalEvents;
            Console.Title = title;
            UserWindow.Resize(size);
            UserWindow.Hide(handle);
            _internalEvents.OnInternalStart += OnStart;
        }

        void OnStart()
        {
            UserWindow.Show(_handle);
            _internalEvents.OnInternalStart -= OnStart;
        }
    }
}