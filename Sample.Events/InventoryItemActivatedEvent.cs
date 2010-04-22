using System;

namespace Sample.Events
{
    [Serializable]
    public class InventoryItemActivatedEvent : AggregateRootEvent
    {
        public InventoryItemActivatedEvent(Guid aggregateId) 
            : base(aggregateId)
        {
        }
    }
}
