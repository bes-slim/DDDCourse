using System.Collections.Generic;
using System.Linq;
using Sample.EventStorage;

namespace Sample.Specs
{
    public static class No
    {
        public static IEnumerable<IEvent> Changes
        {
            get { return Enumerable.Empty<IEvent>(); }
        }
    }
}