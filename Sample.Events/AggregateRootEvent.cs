using System;
using Sample.EventStorage;

namespace Sample.Events
{
    [Serializable]
    public class AggregateRootEvent : IEvent
    {
        public Guid AggregateId { get; private set; }

        public AggregateRootEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}