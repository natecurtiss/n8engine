using System;

namespace N8Engine
{
    public sealed class Event
    {
        event Action OnInvoked;

        public void AddListener(Action listener) => OnInvoked += listener;

        public void RemoveListener(Action listener) => OnInvoked -= listener;

        public void Invoke() => OnInvoked?.Invoke();
    }
    
    public sealed class Event<T>
    {
        event Action<T> OnInvoked;

        public void AddListener(Action<T> listener) => OnInvoked += listener;

        public void RemoveListener(Action<T> listener) => OnInvoked -= listener;

        public void Invoke(T t) => OnInvoked?.Invoke(t);
    }
    
    public sealed class Event<T1, T2>
    {
        event Action<T1, T2> OnInvoked;

        public void AddListener(Action<T1, T2> listener) => OnInvoked += listener;

        public void RemoveListener(Action<T1, T2> listener) => OnInvoked -= listener;

        public void Invoke(T1 t1, T2 t2) => OnInvoked?.Invoke(t1, t2);
    }
}