using System.Collections.Generic;

namespace Sample.Bus
{
    public interface IBus
    {
        void Publish(IMessage message);
        void Publish(IEnumerable<IMessage> messages);
    }
}