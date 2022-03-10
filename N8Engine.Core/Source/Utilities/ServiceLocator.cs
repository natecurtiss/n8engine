using System;
using System.Collections.Generic;

namespace N8Engine.Utilities;

public abstract class ServiceLocator<TService, TServiceNotFoundException> where TServiceNotFoundException : Exception
{
    protected readonly Dictionary<Type, TService> Services = new();

    public int Count => Services.Count;

    protected abstract TServiceNotFoundException ServiceNotFoundException<T>() where T : TService;

    public void Add<TType>(TType service) where TType : TService
    {
        if (Services.ContainsKey(typeof(TType)))
            Services[typeof(TType)] = service;
        else
            Services.Add(typeof(TType), service);
    }

    public void Remove<T>() where T : TService
    {
        if (!Services.Remove(typeof(T)))
            throw ServiceNotFoundException<T>();
    }

    public T Get<T>() where T : TService
    {
        if (!Services.ContainsKey(typeof(T)))
            throw ServiceNotFoundException<T>();
        return (T) Services[typeof(T)];
    }
}