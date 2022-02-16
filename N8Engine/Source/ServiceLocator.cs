using System;
using System.Collections.Generic;

namespace N8Engine;

// TODO: Add exceptions.
public abstract class ServiceLocator<TService>
{
    readonly Dictionary<Type, TService> _services;

    protected void Add<TType>(TType service) where TType : TService
    {
        if (_services.ContainsKey(typeof(TType)))
            _services[typeof(TType)] = service;
        else
            _services.Add(typeof(TType), service);
    }

    protected void Remove<TType>() where TType : TService => _services.Remove(typeof(TType));

    protected TType Get<TType>() where TType : TService => (TType) _services[typeof(TType)];
}