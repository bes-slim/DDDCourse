using System;

namespace Sample.Commands
{
    [Serializable]
    public class ActivateInventoryItemCommand : Command
    {
        public ActivateInventoryItemCommand(Guid id, int version) 
            : base(id, version)
        {
        }
    }
}