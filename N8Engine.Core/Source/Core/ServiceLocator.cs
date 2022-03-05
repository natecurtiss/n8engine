using System;
using System.Collections.Generic;

namespace N8Engine;

public abstract class ServiceLocator<TService>
{
    readonly Dictionary<Type, TService> _services = new();

    public int Count => _services.Count;
    
    protected void Register<TType>(TType service) where TType : TService
    {
        if (_services.ContainsKey(typeof(TType)))
            _services[typeof(TType)] = service;
        else
            _services.Add(typeof(TType), service);
    }

    protected bool Deregister<TType>() where TType : TService => _services.Remove(typeof(TType));

    protected TType Locate<TType>() where TType : TService => (TType) _services[typeof(TType)];
}