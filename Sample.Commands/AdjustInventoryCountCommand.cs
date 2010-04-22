using System;

namespace Sample.Commands
{
    [Serializable]
    public class AdjustInventoryCountCommand : Command
    {
        public AdjustInventoryCountCommand(Guid id, int version, int adjustBy) 
            : base(id, version)
        {
            AdjustBy = adjustBy;
        }

        public int AdjustBy { get; private set; }
    }
}