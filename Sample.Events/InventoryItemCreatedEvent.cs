using System;

namespace Sample.Events
{
    [Serializable]
    public class InventoryItemCreatedEvent : AggregateRootEvent
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Count { get; private set; }
        public bool IsActive { get; private set; }

        public InventoryItemCreatedEvent(Guid aggregateId, string name, string description, int count, bool isActive)
            : base(aggregateId)
        {
            Name = name;
            Description = description;
            Count = count;
            IsActive = isActive;
        }
    }
}