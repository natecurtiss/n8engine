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

        public virtual void OnDebugLog(string message) => System.Diagnostics.Debug.WriteLine(message);

        public virtual void OnExceptionThrown(Exception exception)
        {
            Console.Clear();
            throw exception;
        }
    }

}