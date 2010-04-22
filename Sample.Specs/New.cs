using System;
using Sample.Events;

namespace Sample.Specs
{
    public static class New
    {
        public static InventoryItemActivatedEvent InventoryItemActivatedEvent
        {
            get { return new InventoryItemActivatedEvent(Guid.NewGuid()); }
        }

        public static InventoryItemDeactivatedEvent InventoryItemDeactivatedEvent
        {
            get { return new InventoryItemDeactivatedEvent(Guid.NewGuid()); }
        }
    }
}