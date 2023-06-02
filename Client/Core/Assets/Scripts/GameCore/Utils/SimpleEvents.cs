using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;

namespace GameCore.Utils
{
    public class BaseSimpleEvent<T>
        where T : Delegate
    {
        protected readonly HashSet<T> _subscribers = new HashSet<T>();

        protected IEnumerable<T> Subscribers => _subscribers;

        public void Subscribe(params T[] subscriber)
        {
            _subscribers.AddRange(subscriber);
        }

        public void Unsubscribe(params T[] subscribers)
        {
            foreach (var subscriber in subscribers)
            {
                _subscribers.Remove(subscriber);
            }
        }

        public void Unsubscribe(object someObject)
        {
            var toRemove = _subscribers.Where(subscriber => subscriber.Target == someObject).ToArray();
            Unsubscribe(toRemove);
        }

        public void Clear()
        {
            _subscribers.Clear();
        }
    }

    public sealed class SimpleEvent : BaseSimpleEvent<SimpleEvent.Subscriber>
    {
        public void Invoke()
        {
            foreach (var subscriber in Subscribers)
            {
                subscriber.Invoke();
            }
        }

        public delegate void Subscriber();
    }

    public sealed class SimpleEvent<T> : BaseSimpleEvent<SimpleEvent<T>.Subscriber>
    {
        public void Invoke(T data)
        {
            foreach (var subscriber in Subscribers)
            {
                subscriber.Invoke(data);
            }
        }

        public delegate void Subscriber(T data);
    }

    public sealed class SimpleEvent<T1, T2> : BaseSimpleEvent<SimpleEvent<T1, T2>.Subscriber>
    {
        public void Invoke(T1 data1, T2 data2)
        {
            foreach (var subscriber in Subscribers)
            {
                subscriber.Invoke(data1, data2);
            }
        }

        public delegate void Subscriber(T1 data1, T2 data2);
    }
}
