using System;
using System.Diagnostics;

namespace N8Engine
{
    public sealed class Printer : IModule
    {
        readonly string _onInitialized;
        readonly string _onUpdated;
        
        public Printer(string onInitialized, string onUpdated)
        {
            _onInitialized = onInitialized;
            _onUpdated = onUpdated;
        }

        void IInitializable.Initialize() => Debug.WriteLine(_onInitialized);
        void IUpdateable.Update(Time time) => Debug.WriteLine(_onUpdated);
    }
}