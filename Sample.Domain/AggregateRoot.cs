using System;
using System.Collections.Generic;
using System.Linq;
using Sample.EventStorage;
using Sample.Utilities;

namespace Sample.Domain
{
    public class AggregateRoot : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public int Version { get; private set; }

        readonly List<IEvent> _events;
        readonly Dictionary<Type, Action<IEvent>> _eventHandlers;

        public AggregateRoot(Guid id)
        {
            Id = id;
            _events = new List<IEvent>();
            _eventHandlers = new Dictionary<Type, Action<IEvent>>();
        }

        public IEnumerable<IEvent> GetChanges()
        {
            return _events;
        }

        public void LoadFromHistory(IEnumerable<IEvent> events)
        {
            Version += events.Count();

            events.ForEach(ApplyEventInternal);
        }

        public void ClearChanges()
        {
            _events.Clear();
        }

        protected void RegisterHandler<T>(Action<T> handler) where T : IEvent
        {
            _eventHandlers.Add(typeof(T), handler.Cast<IEvent, T>());
        }

        protected void ApplyEvent(IEvent e)
        {
            ApplyEventInternal(e);
            _events.Add(e);
        }

        void ApplyEventInternal(IEvent e)
        {
            Action<IEvent> handler;
            if (!_eventHandlers.TryGetValue(e.GetType(), out handler))
                throw new InvalidOperationException(string.Format("Cannot find event handler for {0}.", e.GetType().Name));

            handler(e);
        }
    }
}