using System.Collections.Generic;

namespace GameCore.Utils
{
    public class OnceCallEvent
    {
        private readonly Queue<Subscriber> _newSubscribers = new Queue<Subscriber>();
        private bool _alreadyCalled;

        public void Invoke()
        {
            _alreadyCalled = true;

            while (_newSubscribers.Count > 0)
            {
                var subscriber = _newSubscribers.Dequeue();
                subscriber.Invoke();
            }
        }

        public void Subscribe(Subscriber subscriber)
        {
            if (_alreadyCalled)
            {
                subscriber.Invoke();
                return;
            }
            _newSubscribers.Enqueue(subscriber);
        }

        public delegate void Subscriber();
    }
}
