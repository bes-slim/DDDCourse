using System;
using System.Collections.Generic;

namespace Sample.EventStorage
{
    public interface IEventStorage
    {
        void Save(IEventProvider eventProvider);
        IEnumerable<IEvent> GetAllEventsForEventProvider(Guid id);
        IEnumerable<IEvent> GetEventsFromVersionForEventProvider(Guid id, int version);
    }
}