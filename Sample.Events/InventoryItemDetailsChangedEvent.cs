using System;

namespace Sample.Events
{
    [Serializable]
    public class InventoryItemDetailsChangedEvent : AggregateRootEvent
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public InventoryItemDetailsChangedEvent(Guid aggregateId, string name, string description)
            : base(aggregateId)
        {
            Name = name;
            Description = description;
        }
    }
}