using System;
using System.Drawing;
using N8Engine.External;
using N8Engine.External.Console;
using N8Engine.External.User;
using N8Engine.Internal;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    sealed class Window : IWindow
    {
        readonly IntPtr _handle;
        readonly IInternalEvents _internalEvents;
        
        public Window(string title, IntVector size, IntPtr handle, IInternalEvents internalEvents)
        {
            _handle = handle;
            _internalEvents = internalEvents;
            UserWindow.SetTitle(_handle, title);
            UserWindow.Resize(_handle, size);
            UserWindow.DisableResizing(_handle);
            UserWindow.Hide(_handle);
            _internalEvents.OnInternalStart += OnStart;
        }

        void OnStart()
        {
            UserWindow.Show(_handle);
            _internalEvents.OnInternalStart -= OnStart;
        }
    }
}