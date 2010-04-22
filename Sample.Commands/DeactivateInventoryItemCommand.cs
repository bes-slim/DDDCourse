using System;

namespace Sample.Commands
{
    [Serializable]
    public class DeactivateInventoryItemCommand : Command
    {
        public DeactivateInventoryItemCommand(Guid id, int version) 
            : base(id, version)
        {
        }
    }
}