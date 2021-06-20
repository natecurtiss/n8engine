using System;
using N8Engine.Core;
using N8Engine.Rendering;

Application.Initialize();
Application.Run();

namespace N8Engine.Core
{
    internal static class Application
    {
        public static int FramesPerSecond { get; private set; } = TARGET_FRAME_RATE;
        
        public static event Action<float> OnUpdate; 
        public static event Action OnRender;

        private const int TARGET_FRAME_RATE = 60;
        private const float UPDATE_RATE = 1f / TARGET_FRAME_RATE;
        private static bool _isRunning;

        public static void Initialize() => Window.Initialize();

        public static void Run()
        {
            int __frames = 0;
            float __fpsCounterTime = 0f;
            DateTime __previousTime = DateTime.Now;
            
            _isRunning = true;
            while (_isRunning)
            {
                DateTime __currentTime = DateTime.Now;
                float __timePassed = Convert.ToSingle(__currentTime.Subtract(__previousTime).TotalSeconds);
                
                if (__timePassed >= UPDATE_RATE)
                {
                    __frames++;
                    __fpsCounterTime += __timePassed;
                    if (__fpsCounterTime >= 1)
                    {
                        FramesPerSecond = __frames;
                        __frames = 0;
                        __fpsCounterTime = 0f;
                    }
                    __previousTime = __currentTime;
                    
                    OnUpdate?.Invoke(__timePassed);
                    OnRender?.Invoke();
                }
            }
        }

        private static void Stop() => _isRunning = false;
    }
}