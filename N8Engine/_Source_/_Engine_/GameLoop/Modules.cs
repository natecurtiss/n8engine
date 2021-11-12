using System;
using System.Collections.Generic;

namespace N8Engine
{
    // TODO: exceptions.
    public static class Modules
    {
        static readonly Dictionary<Type, IModule> _dict = new();
        static bool _wasInitialized;

        public static T Get<T>() where T : class, IModule
        {
            var type = typeof(T);
            return _dict[type] as T;
        }

        public static void Add<T>(out T module) where T : class, IModule, new()
        {
            var type = typeof(T);
            module = new T();
            if (_wasInitialized) module.Initialize();
            if (_dict.ContainsKey(type))
                _dict.Remove(type);
            _dict.Add(type, module);
        }
        
        public static void Add(IModule module)
        {
            var type = module.Type;
            if (_wasInitialized) module.Initialize();
            if (_dict.ContainsKey(type))
                _dict.Remove(type);
            _dict.Add(type, module);
        }
        
        public static void Remove<T>() where T : class, IModule
        {
            var type = typeof(T);
            if (_dict.ContainsKey(type))
                _dict.Remove(type);
        }

        public static void Initialize()
        {
            if (_wasInitialized) return;
            _wasInitialized = true;
            foreach (var module in _dict.Values)
                module.Initialize();
        }

        public static void Update(Time time)
        {
            foreach (var module in _dict.Values)
                module.Update(time);
        }
    }
}