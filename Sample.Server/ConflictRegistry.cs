using System;
using System.Collections.Generic;
using Sample.Commands;
using Sample.Events;
using Sample.EventStorage;

namespace Sample.Server
{
    public interface IConflictRegistry
    {
        bool IsConflicting(IEvent @event, ICommand command);
        bool IsConflicting(IEnumerable<IEvent> @events, ICommand command);
    }

    public class ConflictRegistry : IConflictRegistry
    {
        readonly Dictionary<Type, IList<Type>> _conflictRegistry = new Dictionary<Type, IList<Type>>();

        public ConflictRegistry()
        {
            _conflictRegistry.Add(typeof(InventoryItemActivatedEvent), new List<Type> { typeof(ActivateInventoryItemCommand) });
        }

        public bool IsConflicting(IEvent @event, ICommand command)
        {
            return _conflictRegistry.ContainsKey(@event.GetType()) ? _conflictRegistry[@event.GetType()].Contains(command.GetType()) : false;
        }

        public bool IsConflicting(IEnumerable<IEvent> @events, ICommand command)
        {
            foreach (var e in events)
            {
                if (IsConflicting(e, command))
                    return true;
            }

            return false;
        }
    }
}