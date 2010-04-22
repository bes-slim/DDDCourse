using System;

namespace Sample.Events
{
    [Serializable]
    public class InventoryItemDeactivatedEvent : AggregateRootEvent
    {
        public InventoryItemDeactivatedEvent(Guid aggregateId) 
            : base(aggregateId)
        {
        }
    }
}
