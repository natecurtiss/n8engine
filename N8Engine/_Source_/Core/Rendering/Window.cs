using System;
using N8Engine.External;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    public sealed class Window : IModule
    {
        public readonly IntVector Size;
        internal readonly IntPtr Handle = ExtConsole.Handle;

        Type IModule.Type => GetType(); 

        internal Window(string title, IntVector size)
        {
            Size = size;
            ExtWindow.SetTitle(Handle, title);
            ExtWindow.Resize(Handle, size);
            ExtWindow.DisableResizing(Handle);
            ExtWindow.Hide(Handle);
        }
        
        void IModule.Initialize() => ExtWindow.Show(Handle);
        void IModule.Update(Time time) { }
    }
}