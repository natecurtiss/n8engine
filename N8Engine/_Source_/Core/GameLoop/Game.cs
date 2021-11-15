using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;

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
                var didUpdate = false;
                var current = ToSeconds(timer.ElapsedMilliseconds);
                var elapsed = current - last;
                var time = new Time(elapsed);
                while (elapsed >= _secondsPerUpdate)
                {
                    didUpdate = true;
                    elapsed = Math.Min(elapsed - _secondsPerUpdate, 0f);
                    Debug.WriteLine("update");
                    Modules.Update(time);
                    Debug.WriteLine("update 2");
                }
                if (didUpdate) last = current - elapsed;
            }
        }

        float ToSeconds(long milliseconds) => milliseconds / 1000f;
    }
}