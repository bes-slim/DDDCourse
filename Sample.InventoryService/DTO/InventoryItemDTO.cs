using System;

namespace Sample.InventoryService.DTO
{
    public class InventoryItemDTO
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public bool Active { get; set; }
    }
}