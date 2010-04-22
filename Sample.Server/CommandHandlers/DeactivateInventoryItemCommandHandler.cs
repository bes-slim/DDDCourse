using Sample.Commands;
using Sample.Domain;

namespace Sample.Server.CommandHandlers
{
    public class DeactivateInventoryItemCommandHandler : CommandHandler<DeactivateInventoryItemCommand>
    {
        readonly IRepository<InventoryItem> _repository;

        public DeactivateInventoryItemCommandHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public override void Handle(DeactivateInventoryItemCommand command)
        {
            InventoryItem inventoryItem = _repository.GetById(command.Id);

            inventoryItem.Deactivate();

            _repository.Save(inventoryItem);
        }
    }
}