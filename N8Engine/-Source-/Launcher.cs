using System;
using N8Engine.SceneManagement;

namespace N8Engine
{
    public abstract class Launcher
    {
        /// <summary>
        /// The list of scenes in the game that can be loaded with the <see cref="SceneManager"/> by <see cref="string">name</see> or <see cref="int">index in the array.</see>
        /// </summary>
        public abstract Scene[] Scenes { get; }
        /// <summary>
        /// The font size the console should write text/pixels at.
        /// I find starting at 7 and adjusting from there is a good way to find your game's ideal pixel size.
        /// </summary>
        public abstract short FontSize { get; }

        /// <summary>
        /// Called whenever <see cref="Debug.Log">Debug.Log</see> is invoked.
        /// By default this just uses <see cref="System.Diagnostics.Debug.WriteLine(object?)">System.Diagnostics.Debug.WriteLine(),</see>
        /// but you're able to override this if you want it redirected to a file or whatever.
        /// </summary>
        public virtual void OnDebugLog(string message) => System.Diagnostics.Debug.WriteLine(message);

        // /// <summary>
        // /// Called whenever an exception is thrown in the program.
        // /// By default this clears and writes to the <see cref="Console">Console,</see>
        // /// but you can override this to redirect to a file or whatever.
        // /// </summary>
        // public virtual void OnExceptionThrown(Exception exception)
        // {
        //     Console.Clear();
        //     throw exception;
        // }
    }

}