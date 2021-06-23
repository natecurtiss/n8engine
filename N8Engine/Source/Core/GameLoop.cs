using System;

namespace N8Engine.Core
{
    /// <summary>
    /// The class that handles the game loop.
    /// </summary>
    public static class GameLoop
    {
        /// <summary>
        /// The frames per second of the application.
        /// </summary>
        public static int FramesPerSecond { get; private set; } = TARGET_FRAMERATE;
        
        /// <summary>
        /// Invoked every frame before rendering.
        /// </summary>
        internal static event Action<float> OnUpdate; 
        /// <summary>
        /// Invoked every frame when objects should render (after OnUpdate).
        /// </summary>
        internal static event Action OnRender;

        /// <summary>
        /// The target framerate of the application.
        /// </summary>
        private const int TARGET_FRAMERATE = 60;
        /// <summary>
        /// The amount of times per second the loop will update; based off of the target framerate.
        /// </summary>
        private const float UPDATE_RATE = 1f / TARGET_FRAMERATE;
        /// <summary>
        /// True when the game loop is running.
        /// </summary>
        private static bool _isRunning;

        /// <summary>
        /// Starts running the game loop.
        /// </summary>
        internal static void Run()
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

        /// <summary>
        /// Stops running the game loop.
        /// </summary>
        private static void Stop() => _isRunning = false;
    }
}