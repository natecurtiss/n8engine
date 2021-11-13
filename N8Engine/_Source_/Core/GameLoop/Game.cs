using System.Collections.Generic;
using System.Diagnostics;

namespace N8Engine
{
    public sealed class Game
    {
        readonly float _secondsPerUpdate;

        internal Game(int targetFps, params IModule[] modules)
        {
            _secondsPerUpdate = 1f / targetFps;
            foreach (var module in modules)
                Modules.Add(module);
        }

        public void Start()
        {
            Modules.Initialize();
            var timer = new Stopwatch();
            var last = 0f;
            timer.Start();
            while (true)
            {
                var current = ToSeconds(timer.ElapsedMilliseconds);
                var elapsed = current - last;
                var time = new Time(elapsed);
                while (elapsed >= _secondsPerUpdate)
                {
                    elapsed = Math.Min(elapsed - _secondsPerUpdate, 0f);
                    Modules.Update(time);
                }
                last = current - elapsed;
            }
        }

        float ToSeconds(long milliseconds) => milliseconds / 1000f;
    }
}