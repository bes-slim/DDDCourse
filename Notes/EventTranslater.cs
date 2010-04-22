using System.Collections.Generic;
using Sample.EventStorage;

namespace Notes
{
    public class EventTranslater
    {
        public IEvent Translate(IEvent @event)
        {
            return @event;
        }

        public IEnumerable<IEvent> Translate(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                yield return Translate(@event);
            }
        }
    }
}