using System.Messaging;

namespace Sample.Bus
{
    public class MsmqMessageReceiver
    {
        readonly IMessageHandler _messageHandler;
        readonly MessageQueue _messageQueue;

        public MsmqMessageReceiver(string messageQueuePath, IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
            _messageQueue = new MessageQueue(messageQueuePath)
                                {
                                    Formatter = new BinaryMessageFormatter()
                                };
            _messageQueue.ReceiveCompleted += MessageQueueReceiveCompleted;
        }

        public void Start()
        {
            _messageQueue.BeginReceive();
        }

        void MessageQueueReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var messageQueue = (MessageQueue) sender;
            try
            {
                Message message = messageQueue.EndReceive(e.AsyncResult);
            
                _messageHandler.Handle((IMessage) message.Body);
            }
            finally
            {
                messageQueue.BeginReceive();                
            }
        }
    }
}