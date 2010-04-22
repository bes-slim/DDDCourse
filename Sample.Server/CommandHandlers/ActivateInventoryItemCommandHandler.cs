using System;
using System.Collections.Generic;
using Sample.Commands;
using Sample.Domain;
using Sample.EventStorage;

namespace Sample.Server.CommandHandlers
{
    public class ActivateInventoryItemCommandHandler : CommandHandler<ActivateInventoryItemCommand>
    {
        readonly IRepository<InventoryItem> _repository;
        readonly IConflictRegistry _conflictRegistry;

        public ActivateInventoryItemCommandHandler(IRepository<InventoryItem> repository, IConflictRegistry conflictRegistry)
        {
            _repository = repository;
            _conflictRegistry = conflictRegistry;
        }

        public override void Handle(ActivateInventoryItemCommand command)
        {
            IEnumerable<IEvent> nonAppliedEvents = _repository.GetNonAppliedEvents(command.Id, command.Version);

            bool conflicting = _conflictRegistry.IsConflicting(nonAppliedEvents, command);
            if (conflicting)
                throw new Exception();

            InventoryItem inventoryItem = _repository.GetById(command.Id);
            inventoryItem.LoadFromHistory(nonAppliedEvents);

            inventoryItem.Activate();

            _repository.Save(inventoryItem);
        }
    }
}