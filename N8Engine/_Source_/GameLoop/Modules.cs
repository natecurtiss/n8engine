using System;
using System.Collections.Generic;

namespace N8Engine
{
    // TODO: exceptions.
    public static class Modules
    {
        static readonly Dictionary<Type, IModule> _dict = new();

        public static T Get<T>() where T : class, IModule, new()
        {
            var type = typeof(T);
            return _dict[type] as T;
        }

        public static void Add<T>(out T service) where T : class, IModule, new()
        {
            var type = typeof(T);
            service = new T();
            if (_dict.ContainsKey(type))
                _dict.Remove(type);
            _dict.Add(type, service);
        }
        
        public static void Remove<T>() where T : class, IModule, new()
        {
            var type = typeof(T);
            if (_dict.ContainsKey(type))
                _dict.Remove(type);
        }
    }
}