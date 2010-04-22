using System.Collections.Generic;
using System.Messaging;
using Sample.Utilities;

namespace Sample.Bus
{
    public class MsmqBus : IBus
    {
        readonly MessageQueue _messageQueue;

        public MsmqBus(string messageQueuePath)
        {
            _messageQueue = new MessageQueue(messageQueuePath)
                                {
                                    Formatter = new BinaryMessageFormatter()
                                };
        }

        public void Publish(IMessage message)
        {
            _messageQueue.Send(message);
        }

        public void Publish(IEnumerable<IMessage> messages)
        {
            messages.ForEach(Publish);
        }
    }
}