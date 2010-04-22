using System.Collections.Generic;
using Sample.EventStorage;

namespace Sample.Domain
{
    public interface IAggregateRoot : IEventProvider
    {
        void LoadFromHistory(IEnumerable<IEvent> events);
        void ClearChanges();
    }
}