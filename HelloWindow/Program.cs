using Silk.NET.Input;
using Silk.NET.Windowing;
using static Silk.NET.Windowing.Window;
using static Silk.NET.Windowing.WindowOptions;

namespace HelloWindow
{
    static class Program
    {
        static IWindow _window = null!;

        static void Main()
        {
            var options = Default;
            options.Size = new(800, 600);
            options.Title = "Hello Window!";

            _window = Create(options);
            
            _window.Load += OnLoad;
            _window.Update += OnUpdate;
            _window.Render += OnRender;
            
            _window.Run();
        }


        static void OnLoad()
        {
            var input = _window.CreateInput();
            foreach (var keyboard in input.Keyboards) 
                keyboard.KeyDown += KeyDown;
        }

        static void OnRender(double dt)
        {
            
        }

        static void OnUpdate(double dt)
        {
            
        }

        static void KeyDown(IKeyboard keyboard, Key key, int i)
        {
            if (key == Key.Escape) 
                _window.Close();
        }
    }
}