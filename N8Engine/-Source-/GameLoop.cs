using System;
using System.Diagnostics;

namespace N8Engine
{
    internal static class GameLoop
    {
        public static event Action<float> OnPreUpdate;
        public static event Action<float> OnEarlyUpdate;
        public static event Action<float> OnUpdate;
        public static event Action<float> OnPostUpdate;
        public static event Action<float> OnPhysicsUpdate;
        public static event Action<float> OnLateUpdate;
        public static event Action OnPreRender;
        public static event Action OnRender;
        public static event Action OnPostRender;
        
        public static int TargetFramerate { get; set; } = 200;
        public static int FramesPerSecond { get; private set; }
        private static float UpdateRate => 1f / TargetFramerate;

        public static void Run()
        {
            var frames = 0;
            var timer = 0f;
            var previousTimeInMilliseconds = 0.0;
            var stopwatch = new Stopwatch();
            
            const float milliseconds_to_seconds = 1000f;
            const float one_second = 1f;

            stopwatch.Start();
            while (true)
            {
                var currentTimeInMilliseconds = stopwatch.ElapsedMilliseconds;
                var timePassed = (float) (currentTimeInMilliseconds - previousTimeInMilliseconds) / milliseconds_to_seconds;

                if (timePassed >= UpdateRate)
                {
                    frames++;
                    timer += timePassed;
                    if (timer >= one_second)
                    {
                        FramesPerSecond = frames;
                        frames = 0;
                        timer = 0f;
                    }
                    previousTimeInMilliseconds = currentTimeInMilliseconds;
                    InvokeEventsForCurrentFrame(UpdateRate);
                }
                // Console.ReadKey();
            }
        }

        private static void InvokeEventsForCurrentFrame(float deltaTime)
        {
            OnPreUpdate?.Invoke(deltaTime);
            OnEarlyUpdate?.Invoke(deltaTime);
            OnUpdate?.Invoke(deltaTime);
            OnPostUpdate?.Invoke(deltaTime);
            
            OnPhysicsUpdate?.Invoke(deltaTime);
            OnLateUpdate?.Invoke(deltaTime);

            OnPreRender?.Invoke();
            OnRender?.Invoke();
            OnPostRender?.Invoke();
        }
    }
}