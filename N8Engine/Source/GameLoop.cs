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
        private const int TARGET_FRAMERATE = 60;
        /// <summary>
        /// The amount of times per second the <see cref="GameLoop"/> will update - based off of
        /// <see cref="TARGET_FRAMERATE">TARGET_FRAMERATE.</see>
        /// </summary>
        private const float UPDATE_RATE = 1f / TARGET_FRAMERATE;

        /// <summary>
        /// Invoked every frame before rendering.
        /// </summary>
        public static event Action<float> OnUpdate;
        public static event Action<float> OnLateUpdate;
        /// <summary>
        /// Invoked every frame after <see cref="OnUpdate"/> and before rendering.
        /// </summary>
        public static event Action OnPhysicsUpdate;
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
        internal static void Run()
        {
            int __frames = 0;
            float __fpsCounterTime = 0f;
            DateTime __previousTime = DateTime.Now;
            while (true)
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
                    OnLateUpdate?.Invoke(__timePassed);
                    OnPhysicsUpdate?.Invoke();
                    OnPreRender.Invoke();
                    OnRender.Invoke();
                    OnPostRender.Invoke();
                }
            }
        }
    }
}