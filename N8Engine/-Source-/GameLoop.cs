using System;
using System.Diagnostics;
using N8Engine.Debugging;

namespace N8Engine
{
    /// <summary>
    /// The class that handles the game loop.
    /// </summary>
    internal static class GameLoop
    {
        /// <summary>
        /// The target framerate of the application.
        /// </summary>
        private const int TARGET_FRAMERATE = 60;
        /// <summary>
        /// The amount of times per second the <see cref="GameLoop"/> will update - based off of
        /// <see cref="TARGET_FRAMERATE">TARGET_FRAMERATE.</see>
        /// </summary>
        private const float UPDATE_RATE = 1f / TARGET_FRAMERATE;

        public static event Action<float> OnPreUpdate;
        /// <summary>
        /// Invoked every frame before rendering.
        /// </summary>
        public static event Action<float> OnUpdate;
        public static event Action<float> OnPostUpdate;

        public static event Action<float> OnPhysicsUpdate;

        /// <summary>
        /// Invoked every frame before <see cref="OnRender"/> and after <see cref="OnUpdate">OnUpdate.</see>
        /// </summary>
        public static event Action OnPreRender;
        /// <summary>
        /// Invoked every frame after <see cref="OnPreRender"/> and before <see cref="OnPostRender">OnPostRender.</see>
        /// </summary>
        public static event Action OnRender;
        /// <summary>
        /// Invoked every frame after <see cref="OnRender">OnRender.</see>
        /// </summary>
        public static event Action OnPostRender;

        /// <summary>
        /// The frames per second of the application.
        /// </summary>
        public static int FramesPerSecond = TARGET_FRAMERATE;

        /// <summary>
        /// Starts running the <see cref="GameLoop">GameLoop.</see>
        /// </summary>
        public static void Run()
        {
            var frames = 0;
            var fpsCounterTime = 0f;
            var previousTimeInMilliseconds = 0.0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                var currentTimeInMilliseconds = stopwatch.ElapsedMilliseconds;
                var timePassed = (float) (currentTimeInMilliseconds - previousTimeInMilliseconds) / 1000f;

                if (timePassed >= UPDATE_RATE)
                {
                    frames++;
                    fpsCounterTime += timePassed;
                    if (fpsCounterTime >= 1)
                    {
                        FramesPerSecond = frames;
                        frames = 0;
                        fpsCounterTime = 0f;
                    }

                    previousTimeInMilliseconds = currentTimeInMilliseconds;

                    OnPreUpdate?.Invoke(timePassed);
                    OnUpdate?.Invoke(timePassed);
                    OnPostUpdate?.Invoke(timePassed);
                    OnPhysicsUpdate?.Invoke(timePassed);
                    OnPreRender?.Invoke();
                    OnRender?.Invoke();
                    OnPostRender?.Invoke();
                }
            }
        }
    }
}