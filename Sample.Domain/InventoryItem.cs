using System;
using Sample.Events;

namespace Sample.Domain
{
    public class InventoryItem : AggregateRoot
    {
        bool _isActive;
        int _count;
        string _description;
        string _name;

        public InventoryItem(Guid id)
            :base(id)
        {
            RegisterHandler<InventoryItemCountAdjustedEvent>(ApplyInventoryItemCountAdjustedEvent);
            RegisterHandler<InventoryItemActivatedEvent>(ApplyInventoryItemActivatedEvent);
            RegisterHandler<InventoryItemDeactivatedEvent>(ApplyInventoryItemDeactivatedEvent);
            RegisterHandler<InventoryItemCreatedEvent>(ApplyInventoryItemCreatedEvent);
            RegisterHandler<InventoryItemDetailsChangedEvent>(ApplyInventoryItemDetailsChangedEvent);
        }

        public InventoryItem(Guid id, string name, string description, int count, bool isActive)
            :this(id)
        {
            ApplyEvent(new InventoryItemCreatedEvent(id, name, description, count, isActive));
        }

        public void Activate()
        {
            if (_isActive)
                throw new InvalidOperationException();

            ApplyEvent(new InventoryItemActivatedEvent(Id));
        }

        public void Deactivate()
        {
            if (!_isActive)
                throw new InvalidOperationException();

            ApplyEvent(new InventoryItemDeactivatedEvent(Id));
        }

        public void AdjustInventoryCount(int adjustBy)
        {
            ApplyEvent(new InventoryItemCountAdjustedEvent(Id, adjustBy));
        }

        public void ChangeDetails(string name, string description)
        {
            ApplyEvent(new InventoryItemDetailsChangedEvent(Id, name, description));
        }

        void ApplyInventoryItemActivatedEvent(InventoryItemActivatedEvent e)
        {
            _isActive = true;
        }

        void ApplyInventoryItemDeactivatedEvent(InventoryItemDeactivatedEvent e)
        {
            _isActive = false;
        }

        void ApplyInventoryItemCountAdjustedEvent(InventoryItemCountAdjustedEvent e)
        {
            _count += e.AdjustedBy;
        }

        void ApplyInventoryItemCreatedEvent(InventoryItemCreatedEvent e)
        {
            _isActive = e.IsActive;
            _count = e.Count;
            _name = e.Name;
            _description = e.Description;
        }

        void ApplyInventoryItemDetailsChangedEvent(InventoryItemDetailsChangedEvent e)
        {
            _name = e.Name;
            _description = e.Description;
        }
    }
}
