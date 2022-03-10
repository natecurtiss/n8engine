using System;
using System.Collections.Generic;

namespace N8Engine.Utilities;

public abstract class ServiceLocator<TService, TServiceNotFoundException> where TServiceNotFoundException : Exception
{
    readonly Dictionary<Type, TService> _services = new();

    public int Count => _services.Count;

    protected abstract TServiceNotFoundException ServiceNotFoundException<T>() where T : TService;

    protected void Register<TType>(TType service) where TType : TService
    {
        if (_services.ContainsKey(typeof(TType)))
            _services[typeof(TType)] = service;
        else
            _services.Add(typeof(TType), service);
    }

    protected void Deregister<T>() where T : TService
    {
        if (!_services.Remove(typeof(T)))
            throw ServiceNotFoundException<T>();
    }

    protected T Locate<T>() where T : TService
    {
        if (!_services.ContainsKey(typeof(T)))
            throw ServiceNotFoundException<T>();
        return (T) _services[typeof(T)];
    }
}