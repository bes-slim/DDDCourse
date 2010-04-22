using System;

namespace Sample.Commands
{
    [Serializable]
    public class CreateInventoryItemCommand : Command
    {
        public CreateInventoryItemCommand(Guid id, string name, string description, int count, bool active) 
            : base(id, 0)
        {
            Name = name;
            Description = description;
            Count = count;
            Active = active;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Count { get; private set; }
        public bool Active { get; private set; }
    }
}