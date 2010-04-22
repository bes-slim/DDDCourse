using System;
using System.Collections.Generic;
using System.Threading;
using Sample.EventStorage;

namespace Sample.Server.EventAggregator
{
    public class EventAggregatorImpl : IEventAggregator
    {
        private readonly SynchronizationContext _context;
        private readonly List<object> _listeners = new List<object>();
        private readonly object _locker = new object();

        public EventAggregatorImpl(SynchronizationContext context)
        {
            _context = context;
        }

        public void SendMessage<T>() where T : new()
        {
            SendMessage(new T());
        }

        public void SendMessage<T>(T message)
        {
            SendAction(() => All().CallOnEach<IListener<T>>(x => x.Handle(message)));
        }

        public void SendMessages<T>(IEnumerable<T> messages)
        {
            foreach (var message in messages)
            {
                T msg = message;
                SendAction(() => All().CallOnEach<IListener<T>>(x => x.Handle(msg)));
            }
        }

        public void AddListener(object listener)
        {
            WithinLock(() =>
                {
                    if (_listeners.Contains(listener))
                        return;

                    _listeners.Add(listener);
                });
        }

        public void RemoveListener(object listener)
        {
            WithinLock(() => _listeners.Remove(listener));
        }

        private object[] All()
        {
            lock (_locker)
            {
                return _listeners.ToArray();
            }
        }

        private void WithinLock(Action action)
        {
            lock (_locker)
            {
                action();
            }
        }

        protected virtual void SendAction(Action action)
        {
            _context.Send(state => action(), null);
        }

        public bool HasListener(object listener)
        {
            return _listeners.Contains(listener);
        }

        public void RemoveAllListeners()
        {
            _listeners.Clear();
        }

        public void RemoveAllListeners(Predicate<object> filter)
        {
            _listeners.RemoveAll(filter);
        }   
    }
}