using System;
using N8Engine.External;
using N8Engine.Mathematics;
using static N8Engine.External.Console;
using Console = N8Engine.External.Console;

namespace N8Engine.Rendering
{
    sealed class GameWindow : Window, IModule
    {
        protected override IntPtr Handle => Console.Handle;

        public GameWindow(string title, IntVector size)
        {
            SetTitle(title);
            Resize(size);
            DisableResizing();
            DisableQuickEditMode();
            RemoveScrollbar();
            HideCursor();
            Hide();
        }
        
        void IModule.Initialize() => Show();

        void IModule.Update(Time time)
        {
            
        }
    }
}