using System;

namespace Sample.Commands
{
    [Serializable]
    public class ChangeInventoryItemDetailsCommand : Command
    {
        public ChangeInventoryItemDetailsCommand(Guid id, int version, string name, string description) 
            : base(id, version)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}