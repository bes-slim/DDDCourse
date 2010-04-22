using System;
using System.Collections.Generic;

namespace Sample.EventStorage
{
    public interface IEventProvider
    {
        Guid Id { get; }
        int Version { get; }
        IEnumerable<IEvent> GetChanges();
    }
}