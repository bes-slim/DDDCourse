using System;

namespace Sample.Events
{
    [Serializable]
    public class InventoryItemCountAdjustedEvent : AggregateRootEvent
    {
        public InventoryItemCountAdjustedEvent(Guid aggregateId, int adjustedBy)
            : base(aggregateId)
        {
            AdjustedBy = adjustedBy;
        }

        public int AdjustedBy { get; private set; }
    }
}