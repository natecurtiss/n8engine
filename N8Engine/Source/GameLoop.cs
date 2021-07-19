using System;
using System.Threading;
using System.Timers;
using N8Engine.Debugging;
using Timer = System.Timers.Timer;

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
        private const int MAX_FRAMERATE = 120;
        /// <summary>
        /// The amount of times per second the <see cref="GameLoop"/> will update - based off of
        /// <see cref="MAX_FRAMERATE">TARGET_FRAMERATE.</see>
        /// </summary>
        private const float UPDATE_RATE = 1f / MAX_FRAMERATE;

        /// <summary>
        /// Invoked every frame before rendering.
        /// </summary>
        public static event Action<float> OnUpdate;
        
        public static event Action<float> OnPrePhysicsUpdate;
        
        public static event Action<float> OnPostPhysicsUpdate;

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
        public static int FramesPerSecond = MAX_FRAMERATE;

        /// <summary>
        /// Starts running the <see cref="GameLoop">GameLoop.</see>
        /// </summary>
        public static void Run()
        {
            var frames = 0;
            var fpsCounterTime = 0f;
            var previousDateTime = DateTime.Now;
            while (true)
            {
                var currentDateTime = DateTime.Now;
                var timePassed = Convert.ToSingle(currentDateTime.Subtract(previousDateTime).TotalSeconds);

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

                    previousDateTime = currentDateTime;

                    OnUpdate?.Invoke(timePassed);
                    OnPrePhysicsUpdate?.Invoke(timePassed);
                    OnPostPhysicsUpdate?.Invoke(timePassed);
                    Debug.Log("something");
                    OnPreRender?.Invoke();
                    OnRender?.Invoke();
                    OnPostRender?.Invoke();
                }
            }
        }
    }
}