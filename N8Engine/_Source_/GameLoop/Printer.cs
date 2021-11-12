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

        void IModule.Initialize() => Debug.WriteLine(_onInitialized);
        void IModule.Update(Time time) => Debug.WriteLine(_onUpdated);
    }
}