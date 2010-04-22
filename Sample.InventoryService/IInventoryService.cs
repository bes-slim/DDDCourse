using System;
using System.Collections.Generic;
using Sample.InventoryService.DTO;

namespace Sample.InventoryService
{
    public interface IInventoryService
    {
        IEnumerable<InventoryItemSummaryDTO> GetSummaries();
        InventoryItemDTO GetInventoryItem(Guid id);
    }
}