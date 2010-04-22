using Sample.Commands;
using Sample.Domain;

namespace Sample.Server.CommandHandlers
{
    public class AdjustInventoryCountCommandHandler : CommandHandler<AdjustInventoryCountCommand>
    {
        readonly IRepository<InventoryItem> _repository;

        public AdjustInventoryCountCommandHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public override void Handle(AdjustInventoryCountCommand command)
        {
            InventoryItem inventoryItem = _repository.GetById(command.Id);

            inventoryItem.AdjustInventoryCount(command.AdjustBy);

            _repository.Save(inventoryItem);
        }
    }
}