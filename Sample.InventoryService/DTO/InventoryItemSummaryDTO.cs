using System;

namespace Sample.InventoryService.DTO
{
    public class InventoryItemSummaryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}