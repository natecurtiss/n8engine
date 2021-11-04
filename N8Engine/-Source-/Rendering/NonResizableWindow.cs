using System;
using System.Drawing;
using N8Engine.External.User;
using N8Engine.Mathematics;

namespace N8Engine.Rendering
{
    sealed class NonResizableWindow : IWindow
    {
        event Action<Color> IWindow.OnBackgroundChanged
        {
            add => OnBackgroundChanged += value;
            remove => OnBackgroundChanged -= value;
        }
        event Action<Color> OnBackgroundChanged;
        
        readonly IntPtr _handle;
        Color _backgroundColor;
        
        Color IWindow.BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnBackgroundChanged?.Invoke(_backgroundColor);
            }
        }
        
        public NonResizableWindow(string title, IntVector size, IntPtr handle)
        {
            _handle = handle;
            UserWindow.SetTitle(_handle, title);
            UserWindow.Resize(_handle, size);
            UserWindow.DisableResizing(_handle);
            UserWindow.Hide(_handle);
        }

        void IWindow.Show() => UserWindow.Show(_handle);
    }
}