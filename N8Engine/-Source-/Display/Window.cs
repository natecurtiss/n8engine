using System;
using System.Drawing;
using N8Engine.External;
using N8Engine.External.User;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class Window
    {
        readonly IntPtr _handle;
        
        internal Window(string title, IntVector size, IntPtr handle)
        {
            _handle = handle;
            Console.Title = title;
            UserWindow.Resize(size);
            UserWindow.Hide(handle);
        }

        internal void Show() => UserWindow.Show(_handle);
    }
}