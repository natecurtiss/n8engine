using System;
using N8Engine.External;
using N8Engine.Mathematics;
using static System.Console;
using static N8Engine.External.Terminal;

namespace N8Engine.Rendering
{
    sealed class GameWindow : Window, IModule
    {
        protected override IntPtr Handle => Terminal.Handle;

        public GameWindow(string title, IntVector size)
        {
            SetTitle(title);
            Resize(size);
            DisableResizing();
            Hide();
            DisableQuickEditMode();
            RemoveScrollbar();
            CursorVisible = false;
        }
        
        void IModule.Initialize() => Show();

        void IModule.Update(Time time)
        {
            
        }
    }
}