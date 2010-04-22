using System;
using System.Collections.Generic;
using Sample.Domain;
using Sample.EventStorage;

namespace Sample.Server
{
    public class Repository<T> : IRepository<T> where T : IAggregateRoot
    {
        readonly IEventStorage _eventStorage;
        readonly IEventAggregator _eventAggregator;

        public Repository(IEventStorage eventStorage, IEventAggregator eventAggregator)
        {
            _eventStorage = eventStorage;
            _eventAggregator = eventAggregator;
        }

        public void Save(T aggregateRoot)
        {
            _eventStorage.Save(aggregateRoot);

            _eventAggregator.SendMessages(aggregateRoot.GetChanges());

            aggregateRoot.ClearChanges();
        }

        public T GetById(Guid id)
        {
            IEnumerable<IEvent> events = _eventStorage.GetAllEventsForEventProvider(id);

            var aggregateRoot = (T)Activator.CreateInstance(typeof(T), id);
            aggregateRoot.LoadFromHistory(events);

            return aggregateRoot;
        }

        public IEnumerable<IEvent> GetNonAppliedEvents(Guid id, int version)
        {
            return _eventStorage.GetEventsFromVersionForEventProvider(id, version);
        }
    }
}