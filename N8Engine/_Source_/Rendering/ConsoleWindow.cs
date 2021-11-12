using System;
using N8Engine.External;
using Console = N8Engine.External.Console;

namespace N8Engine.Rendering
{
    public sealed class ConsoleWindow : IModule
    {
        readonly IntPtr _handle;
        
        public ConsoleWindow(string title, uint width, uint height)
        {
            Window.SetTitle(Console.Handle, title);
            Window.Resize(Console.Handle, size);
            Window.DisableResizing(Console.Handle);
            Window.Hide(Console.Handle);
        }
        
        void IModule.Initialize()
        {
            
        }
        
        void IModule.Update(Time time)
        {
            
        }
    }
}