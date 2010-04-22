using Sample.Commands;
using Sample.Domain;

namespace Sample.Server.CommandHandlers
{
    public class CreateInventoryItemCommandHandler : CommandHandler<CreateInventoryItemCommand>
    {
        readonly IRepository<InventoryItem> _repository;

        public CreateInventoryItemCommandHandler(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        public override void Handle(CreateInventoryItemCommand command)
        {
            var item = new InventoryItem(command.Id, command.Name, command.Description, command.Count, command.Active);

            _repository.Save(item);
        }
    }
}