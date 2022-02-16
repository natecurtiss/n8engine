using System;
using System.Diagnostics;

namespace N8Engine;

sealed class Loop
{
    readonly float _secondsPerUpdate;

    internal Loop(int targetFps) => _secondsPerUpdate = 1f / targetFps;

    public void Run()
    {
        var timer = new Stopwatch();
        var last = 0f;
        timer.Start();
        while (true)
        {
            var didUpdate = false;
            var current = ToSeconds(timer.ElapsedMilliseconds);
            var elapsed = current - last;
            var time = new Frame(elapsed);
            while (elapsed >= _secondsPerUpdate)
            {
                didUpdate = true;
                elapsed = Math.Min(elapsed - _secondsPerUpdate, 0f);
                // Update.
            }
            if (didUpdate) last = current - elapsed;
        }
    }
    
    float ToSeconds(long milliseconds) => milliseconds / 1000f;
}