using System;
using System.Collections.Generic;

namespace N8Engine.Utilities;

public class ServiceLocator<TS, TE> where TE : Exception
{
    protected readonly Dictionary<Type, TS> Services = new();

    public int Count => Services.Count;

    protected virtual TE WhenMissing<T>() where T : TS => throw new ServiceNotFoundException($"Service of type {typeof(T)} not found!");

    public void Register<TType>(TType service) where TType : TS
    {
        if (Services.ContainsKey(typeof(TType)))
            Services[typeof(TType)] = service;
        else
            Services.Add(typeof(TType), service);
    }

    public void Deregister<T>() where T : TS
    {
        if (!Services.Remove(typeof(T)))
            throw WhenMissing<T>();
    }

    public T Find<T>() where T : TS
    {
        if (!Services.ContainsKey(typeof(T)))
            throw WhenMissing<T>();
        return (T) Services[typeof(T)];
    }
}