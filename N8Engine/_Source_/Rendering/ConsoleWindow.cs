using System;
using System.Diagnostics;
using N8Engine.External;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    sealed class ConsoleWindow : Window, IModule
    {
        protected override IntPtr Handle => Terminal.Handle;

        public ConsoleWindow(string title, IntVector size)
        {
            SetTitle(title);
            Resize(size);
            DisableResizing();
            Hide();
        }
        
        void IModule.Initialize()
        {
            Debug.WriteLine("show");
            Show();
        }

        void IModule.Update(Time time)
        {
            
        }
    }
}