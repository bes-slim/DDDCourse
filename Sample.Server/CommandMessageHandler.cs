using System;
using Sample.Bus;
using Sample.EventStorage;

namespace Sample.Server
{
    public class CommandMessageHandler : IMessageHandler
    {
        readonly IEventAggregator _eventAggregator;

        public CommandMessageHandler(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Handle(IMessage message)
        {
            Console.WriteLine("Received command: {0}", message.GetType().Name);

            _eventAggregator.SendMessage(message);
        }
    }
}