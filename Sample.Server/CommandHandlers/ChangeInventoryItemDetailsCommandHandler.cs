using Sample.Commands;
using Sample.Domain;

namespace Sample.Server.CommandHandlers
{
    public class ChangeInventoryItemDetailsCommandHandler : CommandHandler<ChangeInventoryItemDetailsCommand>
    {
        readonly IRepository<InventoryItem> _repository;

        public ChangeInventoryItemDetailsCommandHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public override void Handle(ChangeInventoryItemDetailsCommand command)
        {
            InventoryItem inventoryItem = _repository.GetById(command.Id);

            inventoryItem.ChangeDetails(command.Name, command.Description);

            _repository.Save(inventoryItem);
        }
    }
}