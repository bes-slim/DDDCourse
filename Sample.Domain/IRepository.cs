using System;
using System.Collections.Generic;
using Sample.EventStorage;

namespace Sample.Domain
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        void Save(T aggregateRoot);
        T GetById(Guid id);
        IEnumerable<IEvent> GetNonAppliedEvents(Guid id, int version);
    }
}